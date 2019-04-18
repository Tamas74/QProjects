using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TriArqEDIRealTimeClaimStatus.EDI_277;
using Edidev.FrameworkEDI;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;


namespace TriArqEDIRealTimeClaimStatus
{
    public class cls_EDI277_ResponseProcessing
    {
        //private string _MessageBoxCaption;
        private string _DataBaseConnectionString;
        //private string _EDIDataBaseConnectionString;
        private gloDatabaseLayer.DBLayer oDB;
        private gloDatabaseLayer.DBParameters oDBPara;

        string ClaimNumber = string.Empty;
        ediDocument ediDoc = null;
        ediSchemas oSchemas = null;
        string seffilepath_277_005010X214_SemRef = string.Empty;
        string x277ResponseFilePath = string.Empty;
        string TempFolderpath = gloSettings.FolderSettings.AppTempFolderPath + "EDIFiles";

        public StringBuilder parsedResponse = null;

        Cls_277X212_ISA o277_ISA;
        Cls_277X212_GS o277_GS;
        Cls_277X212_ST o277_ST;
        Cls_277X212_BHT o277_BHT;
        Cls_277X212_HL o277_HL;
        Cls_277X212_NM o277_NM;
        Cls_277X212_PER o277_PER;
        Cls_277X212_TRN o277_TRN;
        Cls_277X212_SVC o277_SVC;
        Cls_277X212_STC o277_STC;
        Cls_277X212_REF o277_REF;
        Cls_277X212_DTP o277_DTP;

        //string sHlQlfr = "";
        //string sQlfr = "";

        long ERAFileID = 0;
        long ISAID = 0;

        long CurrentSTID = 0;
        long CurrentHLId = 0;
        long CurrentTRNId = 0;
        long CurrentSVCId = 0;

        long SourceHLId = 0;
        long ReceiverHLId = 0;
        long ProviderHLId = 0;
        long SubscriberHLId = 0;
        long DependentHLId = 0;

        int SegmentCounter = 0;
        int UniqueIdCounter = 0;
        DataTable dtUniqueIDs = null;

        DataTable dtISA, dtGS, dtST, dtBHT, dtHL, dtNM, dtPER, dtTRN, dtSVC, dtSTC, dtREF, dtDTP = null;

        private enum Segment
        {
            ISA = 1,
            GS = 2,
            ST = 3,
            BHT = 4,
            HL = 5,
            NM = 6,
            PER = 7,
            TRN = 8,
            SVC = 9,
            STC = 10,
            REF = 11,
            DTP = 12
        }

        public cls_EDI277_ResponseProcessing()
        {
            _DataBaseConnectionString = @"Server=glosvr02\sql2008r2;Database=glo9000_DEV;Trusted_Connection=yes;";
            //_EDIDataBaseConnectionString = @"Server=glosvr02\sql2008r2;Database=gloClaimData;Trusted_Connection=yes;";
            seffilepath_277_005010X214_SemRef = AppDomain.CurrentDomain.BaseDirectory + "\\SEF\\277_005010X212.SemRef.SEF";
           
        }

        public cls_EDI277_ResponseProcessing(string ConString)
        {
            _DataBaseConnectionString = ConString;
            seffilepath_277_005010X214_SemRef = AppDomain.CurrentDomain.BaseDirectory + "\\SEF\\277_005010X212.SemRef.SEF";
        }


        public void ParseEDI277(string filePath)
        {
            x277ResponseFilePath = filePath;
            LoadEDISchema();
            Process277X212ResponseFile();
            dtUniqueIDs = getUniQueIDS();
            SetIDforSegments();
            RemoveUnwantedColumnFromERATables();
            Save277X212InDB();
        }

        public bool ParseEDI277String(string fileString)
        {
            bool isFileParsed = false;
            Create277FileByString(fileString);
            LoadEDISchema();
            Process277X212ResponseFile();
            dtUniqueIDs = getUniQueIDS();
            if (dtUniqueIDs != null)
            {
                if (dtUniqueIDs.Rows.Count > 0)
                {
                    SetIDforSegments();
                    RemoveUnwantedColumnFromERATables();
                    isFileParsed = Save277X212InDB();

                }
            }
            return isFileParsed;
        }

        public cls_277CA_RealTimeClaimStatus ParseEDI277String_RCM(string fileString,string _ClaimNumber="")
        {
            //bool isFileParsed = false;
            ClaimNumber = _ClaimNumber;
            cls_277CA_RealTimeClaimStatus oClaimStatus = new cls_277CA_RealTimeClaimStatus();
            Create277FileByString(fileString);
            LoadEDISchema();
            Process277X212ResponseFile();
            dtUniqueIDs = getUniQueIDS();
            if (dtUniqueIDs != null)
            {
                if (dtUniqueIDs.Rows.Count > 0)
                {
                    SetIDforSegments();
                    RemoveUnwantedColumnFromERATables();
                    oClaimStatus = ProcessDataForRCM();
                    oClaimStatus.ResponseFilePath = x277ResponseFilePath;
                }
            }
            return oClaimStatus;
        }

        public cls_277CA_RealTimeClaimStatus Parse277RealTimeCSIString(string fileString, string _ClaimNumber = "", long CSIRequestID = 0)
        {
            bool isFileParsed = false;
            ClaimNumber = _ClaimNumber;
            cls_277CA_RealTimeClaimStatus oClaimStatus = new cls_277CA_RealTimeClaimStatus();

            long CSIResponseID = SaveCSIResponse(Convert.ToInt32(clsGeneral.RequestType.RealTime), CSIRequestID);
            ERAFileID = SaveCSIResponseFile(fileString, CSIResponseID);
            oClaimStatus.ResponseId = CSIResponseID;
            oClaimStatus.ResponseFileId = ERAFileID;

            Create277FileByString(fileString);
            oClaimStatus.ResponseFilePath = x277ResponseFilePath;

            LoadEDISchema();
            Process277X212ResponseFile();
            dtUniqueIDs = getUniQueIDS();
            if (dtUniqueIDs != null)
            {
                if (dtUniqueIDs.Rows.Count > 0)
                {
                    SetIDforSegments();
                    RemoveUnwantedColumnFromERATables();
                    oClaimStatus = ProcessDataForRCM();  //Code to get Response Details from Response file
                    isFileParsed = Save277X212InDB(); //Save Response file Segment Wise
                }
            }
            return oClaimStatus;
        }

        private cls_277CA_RealTimeClaimStatus ProcessDataForRCM()
        {
            long InfoSourceHLId = 0;
            string PayerName = "";
            string PayerID="";
           
            long InfoReceiverHLId = 0;
            string ClinicName = "";
            string ClinicTIN="";

            long providerHLId = 0;
            string providerName = "";
            string ProviderNPI = "";

            long patientHLId = 0;
            string patientName = "";
            string PatientID = "";

            string StatusCode = "";
            string StatusCodeDesc = "";
            string StatusCategoryCode = "";
            string StatusCategoryCodeDesc = "";
            string StatusEffectiveDate = "";
            string StatusMessage = "";
            
            cls_277CA_RealTimeClaimStatus oClaimStatus = new cls_277CA_RealTimeClaimStatus();

            if (dtHL != null)
            {
                if (dtHL.Rows.Count > 0)
                {
                    for (int i = 0; i <= dtHL.Rows.Count - 1; i++)
                    {
                        switch (Convert.ToString(dtHL.Rows[i]["HL03_LevelCode"]))
                        {
                            case "20": InfoSourceHLId = Convert.ToInt64(dtHL.Rows[i]["HLID"]); break;
                            case "21": InfoReceiverHLId = Convert.ToInt64(dtHL.Rows[i]["HLID"]); break;
                            case "19": providerHLId = Convert.ToInt64(dtHL.Rows[i]["HLID"]); break;
                            case "22": patientHLId = Convert.ToInt64(dtHL.Rows[i]["HLID"]); break;
                        }
                    }
                }
            }


            if (dtNM != null)
            {
                if (dtNM.Rows.Count > 0)
                {
                    for (int i = 0; i <= dtNM.Rows.Count - 1; i++)
                    {
                        if (Convert.ToString(dtNM.Rows[i]["NM_HLID"]) == Convert.ToString(InfoSourceHLId))
                        {
                            PayerName = Convert.ToString(dtNM.Rows[i]["NM103_LastName"]);
                            if (Convert.ToString(dtNM.Rows[i]["NM108_IdentificationCodeQualifier"]) == "PI")
                                PayerID = Convert.ToString(dtNM.Rows[i]["NM109_IdentificationCode"]);
                        }
                        else if (Convert.ToString(dtNM.Rows[i]["NM_HLID"]) == Convert.ToString(InfoReceiverHLId))
                        {
                            ClinicName = Convert.ToString(dtNM.Rows[i]["NM103_LastName"]);
                            if (Convert.ToString(dtNM.Rows[i]["NM108_IdentificationCodeQualifier"]) == "46")
                                ClinicTIN = Convert.ToString(dtNM.Rows[i]["NM109_IdentificationCode"]);
                        }
                        else if (Convert.ToString(dtNM.Rows[i]["NM_HLID"]) == Convert.ToString(providerHLId))
                        {
                            providerName = Convert.ToString(dtNM.Rows[i]["NM103_LastName"]);
                            if (Convert.ToString(dtNM.Rows[i]["NM108_IdentificationCodeQualifier"]) == "XX")
                                ProviderNPI = Convert.ToString(dtNM.Rows[i]["NM109_IdentificationCode"]);
                        }
                        else if (Convert.ToString(dtNM.Rows[i]["NM_HLID"]) == Convert.ToString(patientHLId))
                        {
                            patientName = Convert.ToString(dtNM.Rows[i]["NM103_LastName"]) + ", " + Convert.ToString(dtNM.Rows[i]["NM104_FirstName"]);
                            if (Convert.ToString(dtNM.Rows[i]["NM108_IdentificationCodeQualifier"]) == "MI")
                                PatientID = Convert.ToString(dtNM.Rows[i]["NM109_IdentificationCode"]);
                        }
                    }
                }
            }

            if (dtSTC != null)
            {
                if (dtSTC.Rows.Count > 0)
                {
                    for (int i = 0; i <= dtSTC.Rows.Count - 1; i++)
                    {
                        if (Convert.ToString(dtSTC.Rows[i]["STC_SVCID"]) == "0")
                        {
                            StatusCode = Convert.ToString(dtSTC.Rows[i]["STC01_2_StatusCode"]);
                            StatusCategoryCode = Convert.ToString(dtSTC.Rows[i]["STC01_1_StatusCategoryCode"]);
                            StatusEffectiveDate = Convert.ToString(clsGeneral.DateAsDate(Convert.ToInt64(dtSTC.Rows[i]["STC02_StatusInfoEffecticeDate"])));
                            StatusMessage = Convert.ToString(dtSTC.Rows[i]["STC12_Message"]);
                        }
                    }
                }
            }
            
           
            DataTable dtStatus = getClaimStatus(StatusCode,StatusCategoryCode);
            if (dtStatus != null)
            {
                if (dtStatus.Rows.Count > 0)
                {
                    StatusCategoryCodeDesc = Convert.ToString(dtStatus.Rows[0]["StatusCategory"]);
                    StatusCodeDesc = Convert.ToString(dtStatus.Rows[0]["ClaimStatus"]);
                }
            }


            oClaimStatus.PayerId = PayerID;
            oClaimStatus.PayerName = PayerName;
            oClaimStatus.StatusCategoryCode = StatusCategoryCode;
            oClaimStatus.StatusCategoryCodeDesc = StatusCategoryCodeDesc;
            oClaimStatus.StatusCode = StatusCode;
            oClaimStatus.StatusCodeDesc = StatusCodeDesc;
            oClaimStatus.StatusEffectiveDate = StatusEffectiveDate;
            oClaimStatus.StatusMessge = StatusMessage;

            return oClaimStatus;
        }


        private long SaveCSIResponse(int CSIFileType,long CSIRequestId)
        {
            //INUP_CSI_Response
            long ResponseId = 0;            
            object _result = null;
            try
            {
                if (OpenConnection(true))
                {
                    oDBPara.Clear();
                    oDBPara.Add("@CSIResponseId", 0, ParameterDirection.Output, SqlDbType.BigInt);
                    oDBPara.Add("@CSIResponseDateTime", DateTime.Now, ParameterDirection.Input, SqlDbType.DateTime);
                    oDBPara.Add("@ResponseType", CSIFileType, ParameterDirection.Input, SqlDbType.Int);
                    oDBPara.Add("@CSIRequestId", CSIRequestId, ParameterDirection.Input, SqlDbType.BigInt);
                    oDBPara.Add("@LoginSessionId", 1, ParameterDirection.Input, SqlDbType.BigInt);
                    oDBPara.Add("@LoginUserId", gloGlobal.gloPMGlobal.UserID, ParameterDirection.Input, SqlDbType.BigInt);
                    oDBPara.Add("@CreatedOn", DateTime.Now, ParameterDirection.Input, SqlDbType.DateTime);
                    oDB.Execute("INUP_CSI_Response", oDBPara, out _result);
                    if (Convert.ToString(_result) != "" && _result != null)
                    {
                        ResponseId = Convert.ToInt64(_result);
                    }
                    CloseConnection();
                }
            }
            catch (Exception ex)
            {
                clsQEDILogs.ExceptionLog(ex, false);
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            return ResponseId;
        }

        private long SaveCSIResponseFile(string CSIFileString,long ResponseID)
        {
            //INUP_CSI_ResponseFile
            long ResponseFileId = 0;            
            object _result = null;
            try
            {
                if (OpenConnection(true))
                {
                    oDBPara.Clear();
                    oDBPara.Add("@CSIResponseFileId", 0, ParameterDirection.Output, SqlDbType.BigInt);
                    oDBPara.Add("@CSIResponseId", ResponseID, ParameterDirection.Input, SqlDbType.BigInt);
                    oDBPara.Add("@CSIResponseFile", CSIFileString, ParameterDirection.Input, SqlDbType.VarChar);
                    oDBPara.Add("@LoginUserId", gloGlobal.gloPMGlobal.UserID, ParameterDirection.Input, SqlDbType.BigInt);
                    oDBPara.Add("@CreatedOn", DateTime.Now, ParameterDirection.Input, SqlDbType.DateTime);
                    oDB.Execute("INUP_CSI_ResponseFile", oDBPara, out _result);
                    if ((_result != null) && (Convert.ToString(_result) != ""))
                    {
                        ResponseFileId = Convert.ToInt64(_result);
                    }
                    CloseConnection();
                }
            }
            catch (Exception ex)
            {
                clsQEDILogs.ExceptionLog(ex, false);
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            return ResponseFileId;
        }


        private void Create277FileByString(string fileString)
        {
           // string filePath = AppDomain.CurrentDomain.BaseDirectory + "\\277_5010X212" + DateTime.Now.ToString("MMddyyyyhhmmsstt") + ".X12";
            string filePath = TempFolderpath + "\\TRIARQ_277_ClaimNumber_" + ClaimNumber + "_" + DateTime.Now.ToString("MMddyyyyhhmmsstt") + ".X12";
            System.IO.File.WriteAllText(filePath, fileString);
            x277ResponseFilePath = filePath;
        }

        private void LoadEDISchema()
        {
            try
            {
                ediDocument.Set(ref ediDoc, new ediDocument());
                ediSchemas.Set(ref oSchemas, (ediSchemas)ediDoc.GetSchemas());
                oSchemas.EnableStandardReference = false;
                ediDoc.CursorType = DocumentCursorTypeConstants.Cursor_ForwardOnly;
                ediDoc.ImportSchema(seffilepath_277_005010X214_SemRef, SchemaTypeIDConstants.Schema_Standard_Exchange_Format);

                //ediDoc.LoadEdi(AppDomain.CurrentDomain.BaseDirectory + "\\Sample277File\\20170919_6.277");
                //oSchemas.set_Option(SchemasOptionIDConstants.OptSchemas_SetOnDemand, 1);
             
            }
            catch (Edidev.FrameworkEDI.ediException ediEx)
            {
                ShowException(ediEx.ToString());
            }
            catch (System.Runtime.CompilerServices.RuntimeWrappedException Rex)
            {
                string _strEx = string.Empty;
                ediException oException = null;
                oException = (ediException)Rex.WrappedException;
                _strEx = oException.get_Description();

                ShowException(_strEx);

            }
            catch (Exception ex)
            {
                ShowException(ex.ToString());
                clsQEDILogs.ExceptionLog(ActivityModule.EDI277Operation, ActivityType.InitializeEDi, ex, ActivityOutCome.Failure);
            }
        }

        private void Process277X212ResponseFile()
        {
            string responseFilePath = string.Empty;
            ediDataSegment oSegment = null;

            string sSegmentID;
            string sLoopSection;
            int nArea;
            //string sValue;
            string sHlQlfr = "";
            //string sQlfr = "";

            try
            {
                responseFilePath = x277ResponseFilePath;

                if (System.IO.File.Exists(responseFilePath) == true)
                {
                    if (ediDoc != null)
                    {
                        ediDoc.LoadEdi(responseFilePath);
                        ediDataSegment.Set(ref oSegment, (ediDataSegment)ediDoc.FirstDataSegment);
                        while (oSegment != null)
                        {
                            sSegmentID = oSegment.ID;
                            sLoopSection = oSegment.LoopSection;
                            nArea = oSegment.Area;

                            if (nArea == 0)
                            {
                                if (sLoopSection == "")
                                {
                                    if (sSegmentID == "ISA")
                                    {
                                        #region "ISA"

                                        if (o277_ISA == null)
                                        {
                                            o277_ISA = new Cls_277X212_ISA();
                                            SegmentCounter++;
                                        }
                                        o277_ISA.ISA01_AuthorInfoQual = oSegment.get_DataElementValue(1, 0);     //Authorization Information Qualifier
                                        o277_ISA.ISA02_AuthorInfo = oSegment.get_DataElementValue(2, 0);     //Authorization Information
                                        o277_ISA.ISA03_SecurityInfoQual = oSegment.get_DataElementValue(3, 0);     //Security Information Qualifier
                                        o277_ISA.ISA04_SecurityInfo = oSegment.get_DataElementValue(4, 0);     //Security Information
                                        o277_ISA.ISA05_IntrChngIDQual = oSegment.get_DataElementValue(5, 0);     //Interchange ID Qualifier
                                        o277_ISA.ISA06_IntrChngSenderID = oSegment.get_DataElementValue(6, 0);     //Interchange Sender ID
                                        o277_ISA.ISA07_IntrChngIDQual = oSegment.get_DataElementValue(7, 0);     //Interchange ID Qualifier
                                        o277_ISA.ISA08_IntrChngReceiverID = oSegment.get_DataElementValue(8, 0);     //Interchange Receiver ID
                                        o277_ISA.ISA09_IntrChngDate = oSegment.get_DataElementValue(9, 0);     //Interchange Date
                                        o277_ISA.ISA10_IntrChngTime = oSegment.get_DataElementValue(10, 0);     //Interchange Time
                                        o277_ISA.ISA11_IntrChngRepetitionSeparator= oSegment.get_DataElementValue(11, 0);     //Repetition Separator
                                        o277_ISA.ISA12_IntrChngControlVersionNo = oSegment.get_DataElementValue(12, 0);     //Interchange Control Version Number
                                        o277_ISA.ISA13_IntrChngControlNo = oSegment.get_DataElementValue(13, 0); 		// Interchange Control Number (I12) 
                                        o277_ISA.ISA14_AckwRequested = oSegment.get_DataElementValue(14, 0);     //Acknowledgment Requested
                                        o277_ISA.ISA15_UsageIndicator = oSegment.get_DataElementValue(15, 0);     //Usage Indicator
                                        o277_ISA.ISA16_ComponentElementSeparator = oSegment.get_DataElementValue(16, 0);     //Component Element Separator

                                        #endregion
                                    }
                                    else if (sSegmentID == "GS")
                                    {
                                        #region "GS"

                                        if (o277_GS == null)
                                        {
                                            o277_GS = new Cls_277X212_GS();
                                            SegmentCounter++;
                                        }
                                        o277_GS.GS01_FunctionalIdCode= oSegment.get_DataElementValue(1, 0);     //Functional Identifier Code
                                        o277_GS.GS02_SenderCode= oSegment.get_DataElementValue(2, 0); 		// Application Sender's Code (142) 
                                        o277_GS.GS03_ReceiverCode = oSegment.get_DataElementValue(3, 0); 		// Application Receiver's Code (124) 
                                        o277_GS.GS04_FunctionalGroupDate = oSegment.get_DataElementValue(4, 0);     //Date
                                        o277_GS.GS05_FunctionalGroupTime = oSegment.get_DataElementValue(5, 0);     //Time
                                        o277_GS.GS06_GroupControlNumber = oSegment.get_DataElementValue(6, 0); 		// Group Control Number (28) 
                                        o277_GS.GS07_ResponsibleAgencyCode = oSegment.get_DataElementValue(7, 0);     //Responsible Agency Code
                                        o277_GS.GS08_VersionIDCode = oSegment.get_DataElementValue(8, 0);     //Version / Release / Industry Identifier Code

                                        if (o277_ISA != null)
                                        {
                                            o277_ISA.ISA_GS = o277_GS;
                                        }
                                        #endregion
                                    }
                                }
                            }
                            else if (nArea == 1)
                            {
                                if (sLoopSection == "")
                                {
                                    if (sSegmentID == "ST")
                                    {
                                        #region "ST"

                                        if (o277_ST == null)
                                        {
                                            o277_ST = new Cls_277X212_ST();
                                            SegmentCounter++;
                                        }
                                        o277_ST.ST01_IdentifierCode = oSegment.get_DataElementValue(1, 0);     //Transaction Set Identifier Code
                                        o277_ST.ST02_TransactioNSetControlNumber = oSegment.get_DataElementValue(2, 0);     //Transaction Set Control Number
                                        o277_ST.ST03_ConventionReference = oSegment.get_DataElementValue(3, 0);    //Implementation Convention Reference

                                        #endregion
                                    }
                                    else if (sSegmentID == "BHT")   //Beginning of Hierarchical Transaction
                                    {
                                        #region "BHT"

                                        if (o277_BHT == null)
                                        {
                                            o277_BHT = new Cls_277X212_BHT();
                                            SegmentCounter++;
                                        }
                                        o277_BHT.BHT01_StructureCode = oSegment.get_DataElementValue(1, 0);     //Hierarchical Structure Code
                                        o277_BHT.BHT02_PurposeCode = oSegment.get_DataElementValue(2, 0); 		// Transaction Set Purpose Code (353) 
                                        o277_BHT.BHT03_ReferenceIdentification = oSegment.get_DataElementValue(3, 0); 		// Reference Identification (127) 
                                        o277_BHT.BHT04_TransactionDate = oSegment.get_DataElementValue(4, 0); 		// Date (373) 
                                        o277_BHT.BHT05_TransactionTime = oSegment.get_DataElementValue(5, 0);     //Time
                                        o277_BHT.BHT06_transactionTypeCode = oSegment.get_DataElementValue(6, 0);     //Transaction Type Code
                                        if (o277_ST != null)
                                        {
                                            o277_ST.ST_BHT = o277_BHT;
                                        }

                                        o277_BHT.Dispose();
                                        o277_BHT = null;
                                        #endregion
                                    }//sSegmentID

                                }
                            }
                            else if (nArea == 2)
                            {
                                if (sLoopSection == "HL" && sSegmentID == "HL")
                                {
                                    sHlQlfr = oSegment.get_DataElementValue(3, 0);
                                }
                                else if (sLoopSection == "" && sSegmentID == "SE")
                                {
                                    if (o277_ST != null)
                                    {
                                        if (o277_HL != null)
                                        {
                                            if (o277_TRN != null)
                                            {
                                                if (o277_HL.HL_TRN == null) { o277_HL.HL_TRN = new List<Cls_277X212_TRN>(); }
                                                o277_HL.HL_TRN.Add(o277_TRN);
                                                o277_TRN.Dispose();
                                                o277_TRN = null;
                                            }

                                            if (o277_ST.ST_HL == null) { o277_ST.ST_HL = new List<Cls_277X212_HL>(); }
                                            o277_ST.ST_HL.Add(o277_HL);
                                            o277_HL.Dispose();
                                            o277_HL = null;
                                            if (o277_ISA != null)
                                            {
                                                if (o277_ISA.ISA_ST == null) { o277_ISA.ISA_ST = new List<Cls_277X212_ST>(); }
                                                o277_ISA.ISA_ST.Add(o277_ST);
                                                o277_ST.Dispose();
                                                o277_ST = null;
                                            }
                                        }
                                    }
                                }


                                if (sHlQlfr == "20")    //2000A INFORMATION SOURCE LEVEL
                                {
                                    #region "2000A INFORMATION SOURCEL EVEL"
                                    if (sLoopSection == "HL")
                                    {
                                        if (sSegmentID == "HL") //Information Source Level
                                        {
                                            if (o277_HL == null)
                                            {
                                                o277_HL = new Cls_277X212_HL();
                                                SegmentCounter++;
                                            }
                                            o277_HL.HL01_HLSegmentId = oSegment.get_DataElementValue(1, 0);   //Hierarchical ID Number
                                            o277_HL.HL02_HLParentId = oSegment.get_DataElementValue(2, 0);    //Hierarchical Parent ID Number
                                            o277_HL.HL03_LevelCode = oSegment.get_DataElementValue(3, 0);     //Hierarchical Level Code
                                            o277_HL.HL04_ChildCode = oSegment.get_DataElementValue(4, 0);     //Hierarchical Child Code

                                        }//Segment ID

                                    }
                                    else if (sLoopSection == "HL;NM1")   //2100A PAYER NAME
                                    {
                                        if (sSegmentID == "NM1")    //Payer Name
                                        {
                                            if (o277_NM == null)
                                            {
                                                o277_NM = new Cls_277X212_NM();
                                                SegmentCounter++;
                                            }
                                            o277_NM.NM101_EntityIdCode = oSegment.get_DataElementValue(1, 0);     //Entity Identifier Code
                                            o277_NM.NM102_EntityTypeQualifier = oSegment.get_DataElementValue(2, 0);     //Entity Type Qualifier
                                            o277_NM.NM103_LastName = oSegment.get_DataElementValue(3, 0); 		// Name Last or Organization Name (1035) 
                                            o277_NM.NM108_IdentificationCodeQualifier = oSegment.get_DataElementValue(8, 0);     //Identification Code Qualifier
                                            o277_NM.NM109_IdentificationCode = oSegment.get_DataElementValue(9, 0); 		// Identification Code (67) 

                                            if (o277_HL != null)
                                            {
                                                o277_HL.HL_NM = o277_NM;
                                            }
                                            o277_NM.Dispose();
                                            o277_NM = null;
                                        }

                                        else if (sSegmentID == "PER") 		// Payer Contact Information
                                        {
                                            if (o277_PER == null)
                                            {
                                                o277_PER = new Cls_277X212_PER();
                                                SegmentCounter++;
                                            }
                                            o277_PER.PER01_ContactFunctionCode = oSegment.get_DataElementValue(1, 0); 		// Contact Function Code (366) 
                                            o277_PER.PER02_Name = oSegment.get_DataElementValue(2, 0); 		// Name (93) 
                                            o277_PER.PER03_CommNumberQualifier = oSegment.get_DataElementValue(3, 0); 		// Communication Number Qualifier (365) 
                                            o277_PER.PER04_CommNumber = oSegment.get_DataElementValue(4, 0);
                                            o277_PER.PER05_CommNumberQualifier = oSegment.get_DataElementValue(5, 0);
                                            o277_PER.PER06_CommNumber = oSegment.get_DataElementValue(6, 0);
                                            o277_PER.PER07_CommNumberQualifier = oSegment.get_DataElementValue(7, 0);
                                            o277_PER.PER08_CommNumber = oSegment.get_DataElementValue(8, 0);
                                            if (o277_HL != null)
                                            {
                                                o277_HL.HL_PER = o277_PER;
                                            }
                                            o277_PER.Dispose();
                                            o277_PER = null;
                                        }// sSegmentID == "PER" 

                                    }// sLoopSection



                                    #endregion
                                }

                                else if (sHlQlfr == "21")   //2000B INFORMATION RECEIVER LEVEL
                                {
                                    //ADD Previous HL to ST
                                    #region "2000B INFORMATION RECEIVER LEVEL"
                                    if (sLoopSection == "HL")
                                    {
                                        if (sSegmentID == "HL") //Information Receiver Level
                                        {
                                            //ADD LOOP 2000A
                                            if (o277_ST != null)
                                            {
                                                if (o277_ST.ST_HL == null) { o277_ST.ST_HL = new List<Cls_277X212_HL>(); }
                                                if (o277_HL != null)
                                                {
                                                    o277_ST.ST_HL.Add(o277_HL);
                                                    o277_HL.Dispose();
                                                    o277_HL = null;
                                                }
                                            }
                                            if (o277_HL == null)
                                            {
                                                o277_HL = new Cls_277X212_HL();
                                                SegmentCounter++;
                                            }
                                            o277_HL.HL01_HLSegmentId = oSegment.get_DataElementValue(1, 0);     //Hierarchical ID Number
                                            o277_HL.HL02_HLParentId = oSegment.get_DataElementValue(2, 0);     //Hierarchical Parent ID Number
                                            o277_HL.HL03_LevelCode = oSegment.get_DataElementValue(3, 0);     //Hierarchical Level Code
                                            o277_HL.HL04_ChildCode = oSegment.get_DataElementValue(4, 0);     //Hierarchical Child Code
                                        }//Segment ID

                                    }
                                    else if (sLoopSection == "HL;NM1")  //2100B INFORMATION RECEIVER NAME
                                    {
                                        if (sSegmentID == "NM1")    //Information Receiver Name
                                        {
                                            if (o277_NM == null)
                                            {
                                                o277_NM = new Cls_277X212_NM();
                                                SegmentCounter++;
                                            }
                                            o277_NM.NM101_EntityIdCode = oSegment.get_DataElementValue(1, 0);     //Entity Identifier Code
                                            o277_NM.NM102_EntityTypeQualifier = oSegment.get_DataElementValue(2, 0);     //Entity Type Qualifier
                                            o277_NM.NM103_LastName = oSegment.get_DataElementValue(3, 0); 		// Name Last or Organization Name (1035) 
                                            o277_NM.NM104_FirstName = oSegment.get_DataElementValue(4, 0);     //Name First
                                            o277_NM.NM105_MiddleName = oSegment.get_DataElementValue(5, 0);     //Name Middle
                                            o277_NM.NM108_IdentificationCodeQualifier = oSegment.get_DataElementValue(8, 0);     //Identification Code Qualifier
                                            o277_NM.NM109_IdentificationCode = oSegment.get_DataElementValue(9, 0); 		// Identification Code (67) 
                                            if (o277_HL != null)
                                            {
                                                o277_HL.HL_NM = o277_NM;
                                            }
                                            o277_NM.Dispose();
                                            o277_NM = null;
                                        }
                                    }
                                    else if (sLoopSection == "HL;TRN")  //2200B INFORMATION RECEIVER TRACE IDENTIFIER
                                    {
                                        if (sSegmentID == "TRN")    //Information Receiver Trace Identifier
                                        {
                                            if (o277_TRN == null)
                                            {
                                                o277_TRN = new Cls_277X212_TRN();
                                                SegmentCounter++;
                                            }
                                            o277_TRN.TRN01_TraceTypeCode = oSegment.get_DataElementValue(1, 0);     //Trace Type Code
                                            o277_TRN.TRN02_ReferenceIdentification = oSegment.get_DataElementValue(2, 0); 		// Reference Identification (127) 
                                        }
                                        else if (sSegmentID == "STC")   //Information Receiver Status Information
                                        {
                                            if (o277_STC == null)
                                            {
                                                o277_STC = new Cls_277X212_STC();
                                                SegmentCounter++;
                                            }
                                            o277_STC.STC01_1_StatusCategoryCode = oSegment.get_DataElementValue(1, 1);     //Industry Code
                                            o277_STC.STC01_2_StatusCode = oSegment.get_DataElementValue(1, 2);     //Industry Code
                                            o277_STC.STC02_StatusInfoEffecticeDate = oSegment.get_DataElementValue(2, 0);     //Date
                                            o277_STC.STC10_1_IndustryCode = oSegment.get_DataElementValue(10, 1); 		// Industry Code (1271) 
                                            o277_STC.STC10_2_IndustryCode = oSegment.get_DataElementValue(10, 2); 		// Industry Code (1271) 
                                            o277_STC.STC10_3_EntityIDCode = oSegment.get_DataElementValue(10, 3); 		// Entity Identifier Code (98) 
                                            o277_STC.STC11_1_IndustryCode = oSegment.get_DataElementValue(11, 1); 		// Industry Code (1271) 
                                            o277_STC.STC11_2_IndustryCode = oSegment.get_DataElementValue(11, 2); 		// Industry Code (1271) 
                                            o277_STC.STC11_3_EntityIDCode = oSegment.get_DataElementValue(11, 3); 		// Entity Identifier Code (98) 
                                            if (o277_TRN != null)
                                            {
                                                if (o277_TRN.TRN_STC == null) { o277_TRN.TRN_STC = new List<Cls_277X212_STC>(); }
                                                o277_TRN.TRN_STC.Add(o277_STC);
                                            }
                                            o277_STC.Dispose();
                                            o277_STC = null;
                                        }//SegmentID

                                    }//sLoopSection
                                    #endregion
                                }

                                else if (sHlQlfr == "19")   //2000C SERVICE PROVIDER LEVEL
                                {
                                    #region "2000C SERVICE PROVIDER LEVEL"

                                    if (sLoopSection == "HL")
                                    {
                                        if (sSegmentID == "HL") //Service Provider Level
                                        {
                                            //ADD LOOP 2000B
                                            if (o277_ST != null)
                                            {
                                                if (o277_HL != null)
                                                {
                                                    if (o277_TRN != null)
                                                    {
                                                        if (o277_HL.HL_TRN == null) { o277_HL.HL_TRN = new List<Cls_277X212_TRN>(); }
                                                        o277_HL.HL_TRN.Add(o277_TRN);
                                                        o277_TRN.Dispose();
                                                        o277_TRN = null;
                                                    }

                                                    if (o277_ST.ST_HL == null) { o277_ST.ST_HL = new List<Cls_277X212_HL>(); }
                                                    o277_ST.ST_HL.Add(o277_HL);
                                                    o277_HL.Dispose();
                                                    o277_HL = null;
                                                }
                                            }
                                            if (o277_HL == null)
                                            {
                                                o277_HL = new Cls_277X212_HL();
                                                SegmentCounter++;
                                            }
                                            o277_HL.HL01_HLSegmentId = oSegment.get_DataElementValue(1, 0);     //Hierarchical ID Number
                                            o277_HL.HL02_HLParentId = oSegment.get_DataElementValue(2, 0);     //Hierarchical Parent ID Number
                                            o277_HL.HL03_LevelCode = oSegment.get_DataElementValue(3, 0);     //Hierarchical Level Code
                                            o277_HL.HL04_ChildCode = oSegment.get_DataElementValue(4, 0);     //Hierarchical Child Code

                                        }//Segment ID
                                    }
                                    else if (sLoopSection == "HL;NM1")  //2100B INFORMATION RECEIVER NAME
                                    {
                                        if (sSegmentID == "NM1")    //Information Receiver Name
                                        {
                                            if (o277_NM == null)
                                            {
                                                o277_NM = new Cls_277X212_NM();
                                                SegmentCounter++;
                                            }
                                            o277_NM.NM101_EntityIdCode = oSegment.get_DataElementValue(1, 0);     //Entity Identifier Code
                                            o277_NM.NM102_EntityTypeQualifier = oSegment.get_DataElementValue(2, 0);     //Entity Type Qualifier
                                            o277_NM.NM103_LastName = oSegment.get_DataElementValue(3, 0); 		// Name Last or Organization Name (1035) 
                                            o277_NM.NM104_FirstName = oSegment.get_DataElementValue(4, 0);     //Name First
                                            o277_NM.NM105_MiddleName = oSegment.get_DataElementValue(5, 0);     //Name Middle
                                            o277_NM.NM107_Suffix = oSegment.get_DataElementValue(7, 0);     //Name Suffix
                                            o277_NM.NM108_IdentificationCodeQualifier = oSegment.get_DataElementValue(8, 0);     //Identification Code Qualifier
                                            o277_NM.NM109_IdentificationCode = oSegment.get_DataElementValue(9, 0); 		// Identification Code (67) 
                                            if (o277_HL != null)
                                            {
                                                o277_HL.HL_NM = o277_NM;
                                            }
                                            o277_NM.Dispose();
                                            o277_NM = null;
                                        }
                                    }
                                    else if (sLoopSection == "HL;TRN")  //2200B INFORMATION RECEIVER TRACE IDENTIFIER
                                    {
                                        if (sSegmentID == "TRN")    //Information Receiver Trace Identifier
                                        {
                                            if (o277_TRN == null)
                                            {
                                                o277_TRN = new Cls_277X212_TRN();
                                                SegmentCounter++;
                                            }
                                            o277_TRN.TRN01_TraceTypeCode = oSegment.get_DataElementValue(1, 0);     //Trace Type Code
                                            o277_TRN.TRN02_ReferenceIdentification = oSegment.get_DataElementValue(2, 0); 		// Reference Identification (127) 
                                        }
                                        else if (sSegmentID == "STC")   //Information Receiver Status Information
                                        {
                                            if (o277_STC == null)
                                            {
                                                o277_STC = new Cls_277X212_STC();
                                                SegmentCounter++;
                                            }
                                            o277_STC.STC01_1_StatusCategoryCode = oSegment.get_DataElementValue(1, 1);     //Industry Code
                                            o277_STC.STC01_2_StatusCode = oSegment.get_DataElementValue(1, 2);     //Industry Code
                                            o277_STC.STC01_3_EntityIdentifierCode = oSegment.get_DataElementValue(1, 3); 		// Entity Identifier Code (98)
                                            o277_STC.STC02_StatusInfoEffecticeDate = oSegment.get_DataElementValue(2, 0);     //Date
                                            o277_STC.STC10_1_IndustryCode = oSegment.get_DataElementValue(10, 1); 		// Industry Code (1271) 
                                            o277_STC.STC10_2_IndustryCode = oSegment.get_DataElementValue(10, 2); 		// Industry Code (1271) 
                                            o277_STC.STC10_3_EntityIDCode = oSegment.get_DataElementValue(10, 3); 		// Entity Identifier Code (98) 
                                            o277_STC.STC11_1_IndustryCode = oSegment.get_DataElementValue(11, 1); 		// Industry Code (1271) 
                                            o277_STC.STC11_2_IndustryCode = oSegment.get_DataElementValue(11, 2); 		// Industry Code (1271) 
                                            o277_STC.STC11_3_EntityIDCode = oSegment.get_DataElementValue(11, 3); 		// Entity Identifier Code (98) 
                                            if (o277_TRN != null)
                                            {
                                                if (o277_TRN.TRN_STC == null) { o277_TRN.TRN_STC = new List<Cls_277X212_STC>(); }
                                                o277_TRN.TRN_STC.Add(o277_STC);
                                                o277_STC.Dispose();
                                                o277_STC = null;
                                            }
                                        }//SegmentID

                                    }//sLoopSection
                                    #endregion
                                }

                                else if (sHlQlfr == "22")   //2000D SUBSCRIBER LEVEL
                                {
                                    #region "2000D SUBSCRIBER LEVEL"
                                    if (sLoopSection == "HL")
                                    {
                                        if (sSegmentID == "HL") //Subscriber Level
                                        {

                                            //ADD LOOP 2000C
                                            if (o277_ST != null)
                                            {
                                                if (o277_HL != null)
                                                {
                                                    if (o277_TRN != null)
                                                    {
                                                        if (o277_HL.HL_TRN == null) { o277_HL.HL_TRN = new List<Cls_277X212_TRN>(); }
                                                        o277_HL.HL_TRN.Add(o277_TRN);
                                                        o277_TRN.Dispose();
                                                        o277_TRN = null;
                                                    }

                                                    if (o277_ST.ST_HL == null) { o277_ST.ST_HL = new List<Cls_277X212_HL>(); }
                                                    o277_ST.ST_HL.Add(o277_HL);
                                                    o277_HL.Dispose();
                                                    o277_HL = null;
                                                }
                                            }
                                            if (o277_HL == null)
                                            {
                                                o277_HL = new Cls_277X212_HL();
                                                SegmentCounter++;
                                            }

                                            o277_HL.HL01_HLSegmentId = oSegment.get_DataElementValue(1, 0);     //Hierarchical ID Number
                                            o277_HL.HL02_HLParentId = oSegment.get_DataElementValue(2, 0);     //Hierarchical Parent ID Number
                                            o277_HL.HL03_LevelCode = oSegment.get_DataElementValue(3, 0);     //Hierarchical Level Code
                                            o277_HL.HL04_ChildCode = oSegment.get_DataElementValue(4, 0);     //Hierarchical Child Code

                                        }//Segment ID

                                    }
                                    else if (sLoopSection == "HL;NM1")  //2100D SUBSCRIBER NAME
                                    {
                                        if (sSegmentID == "NM1")    //Subscriber Name
                                        {
                                            if (o277_NM == null)
                                            {
                                                o277_NM = new Cls_277X212_NM();
                                                SegmentCounter++;
                                            }
                                            o277_NM.NM101_EntityIdCode = oSegment.get_DataElementValue(1, 0);     //Entity Identifier Code
                                            o277_NM.NM102_EntityTypeQualifier = oSegment.get_DataElementValue(2, 0);     //Entity Type Qualifier
                                            o277_NM.NM103_LastName = oSegment.get_DataElementValue(3, 0); 		// Name Last or Organization Name (1035) 
                                            o277_NM.NM104_FirstName = oSegment.get_DataElementValue(4, 0);     //Name First
                                            o277_NM.NM105_MiddleName = oSegment.get_DataElementValue(5, 0);     //Name Middle
                                            o277_NM.NM107_Suffix = oSegment.get_DataElementValue(7, 0);     //Name Suffix
                                            o277_NM.NM108_IdentificationCodeQualifier = oSegment.get_DataElementValue(8, 0);     //Identification Code Qualifier
                                            o277_NM.NM109_IdentificationCode = oSegment.get_DataElementValue(9, 0); 		// Identification Code (67) 
                                            if (o277_HL != null)
                                            {
                                                o277_HL.HL_NM = o277_NM;
                                            }
                                            o277_NM.Dispose();
                                            o277_NM = null;
                                        }

                                    }//sLoopSection

                                    //2200D CLAIM STATUS TRACKING NUMBER 
                                    Proc_ClaimStatus(ref oSegment, ref sSegmentID, ref sLoopSection);
                                    #endregion
                                }
                                else if (sHlQlfr == "23")   //2000E DEPENDENT LEVEL
                                {
                                    #region "2000E SUBSCRIBER LEVEL"
                                    if (sLoopSection == "HL")
                                    {
                                        if (sSegmentID == "HL") //Subscriber Level
                                        {

                                            //ADD LOOP 2000C
                                            if (o277_ST != null)
                                            {
                                                if (o277_HL != null)
                                                {
                                                    if (o277_TRN != null)
                                                    {
                                                        if (o277_HL.HL_TRN == null) { o277_HL.HL_TRN = new List<Cls_277X212_TRN>(); }
                                                        o277_HL.HL_TRN.Add(o277_TRN);
                                                        o277_TRN.Dispose();
                                                        o277_TRN = null;
                                                    }

                                                    if (o277_ST.ST_HL == null) { o277_ST.ST_HL = new List<Cls_277X212_HL>(); }
                                                    o277_ST.ST_HL.Add(o277_HL);
                                                    o277_HL.Dispose();
                                                    o277_HL = null;
                                                }
                                            }
                                            if (o277_HL == null)
                                            {
                                                o277_HL = new Cls_277X212_HL();
                                                SegmentCounter++;
                                            }

                                            o277_HL.HL01_HLSegmentId = oSegment.get_DataElementValue(1, 0);     //Hierarchical ID Number
                                            o277_HL.HL02_HLParentId = oSegment.get_DataElementValue(2, 0);     //Hierarchical Parent ID Number
                                            o277_HL.HL03_LevelCode = oSegment.get_DataElementValue(3, 0);     //Hierarchical Level Code
                                            o277_HL.HL04_ChildCode = oSegment.get_DataElementValue(4, 0);     //Hierarchical Child Code

                                        }//Segment ID


                                    }//sLoopSection
                                    else if (sLoopSection == "HL;NM1")  //2100D SUBSCRIBER NAME
                                    {
                                        if (sSegmentID == "NM1")    //Subscriber Name
                                        {
                                            if (o277_NM == null)
                                            {
                                                o277_NM = new Cls_277X212_NM();
                                                SegmentCounter++;
                                            }
                                            o277_NM.NM101_EntityIdCode = oSegment.get_DataElementValue(1, 0);     //Entity Identifier Code
                                            o277_NM.NM102_EntityTypeQualifier = oSegment.get_DataElementValue(2, 0);     //Entity Type Qualifier
                                            o277_NM.NM103_LastName = oSegment.get_DataElementValue(3, 0); 		// Name Last or Organization Name (1035) 
                                            o277_NM.NM104_FirstName = oSegment.get_DataElementValue(4, 0);     //Name First
                                            o277_NM.NM105_MiddleName = oSegment.get_DataElementValue(5, 0);     //Name Middle
                                            o277_NM.NM107_Suffix = oSegment.get_DataElementValue(7, 0);     //Name Suffix
                                            if (o277_HL != null)
                                            {
                                                o277_HL.HL_NM = o277_NM;
                                            }
                                            o277_NM.Dispose();
                                            o277_NM = null;
                                        }
                                    }

                                    Proc_ClaimStatus(ref oSegment, ref sSegmentID, ref sLoopSection);
                                    #endregion
                                }
                            }

                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oSegment.Next());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //ShowException(ex.Message);
                clsQEDILogs.ExceptionLog(ActivityModule.EDI277Operation, ActivityType.ParseEDI, ex, ActivityOutCome.Failure);
            }
            finally
            {
                if (ediDoc != null)
                {
                    ediDoc.Close();
                    ediDoc.Dispose();
                    ediDoc = null;
                }
            }
        }

        private void Proc_ClaimStatus(ref ediDataSegment oSegment, ref string sSegmentID, ref string sLoopSection)
        {
            //string sValue;
            //string sQlfr = "";
            try
            {

                //2200D / 2200E CLAIM STATUS TRACKING NUMBER
                if (sLoopSection == "HL;TRN")
                {
                    #region "2200D / 2200E CLAIM STATUS TRACKING NUMBER"
                    if (sSegmentID == "TRN")    //Claim Status Tracking Number
                    {
                        if (o277_TRN == null)
                        {
                            o277_TRN = new Cls_277X212_TRN();
                            SegmentCounter++;
                        }
                        o277_TRN.TRN01_TraceTypeCode = oSegment.get_DataElementValue(1, 0);     //Trace Type Code
                        o277_TRN.TRN02_ReferenceIdentification = oSegment.get_DataElementValue(2, 0); 		// Reference Identification (127) 
                    }

                    else if (sSegmentID == "STC")   //Claim Level Status Information
                    {
                        if (o277_STC == null)
                        {
                            o277_STC = new Cls_277X212_STC();
                            SegmentCounter++;
                        }
                        o277_STC.STC01_1_StatusCategoryCode = oSegment.get_DataElementValue(1, 1); 		// Industry Code (1271) 
                        o277_STC.STC01_2_StatusCode = oSegment.get_DataElementValue(1, 2); 		// Industry Code (1271) 
                        o277_STC.STC01_3_EntityIdentifierCode = oSegment.get_DataElementValue(1, 3); 		// Entity Identifier Code (98) 
                        o277_STC.STC01_4_CodeListQualifier = oSegment.get_DataElementValue(1, 4); 		// Code List Qualifier Code (1270) 
                        o277_STC.STC02_StatusInfoEffecticeDate = oSegment.get_DataElementValue(2, 0); 		// Date (373) 
                        o277_STC.STC04_TotalClaimChargeAmount = oSegment.get_DataElementValue(4, 0); 		// Monetary Amount (782) 
                        o277_STC.STC05_TotalClaimChargeAmount = oSegment.get_DataElementValue(5, 0); 		// Monetary Amount (782) 
                        o277_STC.STC06_Date = oSegment.get_DataElementValue(6, 0); 		// Date (373) 
                        o277_STC.STC08_Date = oSegment.get_DataElementValue(8, 0); 		// Date (373) 
                        o277_STC.STC09_CheckNumber = oSegment.get_DataElementValue(9, 0); 		// Check Number (429) 
                        o277_STC.STC10_1_IndustryCode = oSegment.get_DataElementValue(10, 1); 		// Industry Code (1271) 
                        o277_STC.STC10_2_IndustryCode = oSegment.get_DataElementValue(10, 2); 		// Industry Code (1271) 
                        o277_STC.STC10_3_EntityIDCode = oSegment.get_DataElementValue(10, 3); 		// Entity Identifier Code (98) 
                        o277_STC.STC10_4_CodeListQualifierCode = oSegment.get_DataElementValue(10, 4); 		// Code List Qualifier Code (1270) 
                        o277_STC.STC11_1_IndustryCode = oSegment.get_DataElementValue(11, 1); 		// Industry Code (1271) 
                        o277_STC.STC11_2_IndustryCode = oSegment.get_DataElementValue(11, 2); 		// Industry Code (1271) 
                        o277_STC.STC11_3_EntityIDCode = oSegment.get_DataElementValue(11, 3); 		// Entity Identifier Code (98) 
                        o277_STC.STC11_4_CodeListQualifierCode = oSegment.get_DataElementValue(11, 4); 		// Code List Qualifier Code (1270) 
                        if (o277_TRN != null)
                        {
                            if (o277_TRN.TRN_STC == null) { o277_TRN.TRN_STC = new List<Cls_277X212_STC>(); }
                            o277_TRN.TRN_STC.Add(o277_STC);
                            o277_STC.Dispose();
                            o277_STC = null;
                        }


                    }//Segment ID

                    else if (sSegmentID == "REF") 		// Reference Identification 
                    {
                        if (o277_REF == null)
                        {
                            o277_REF = new Cls_277X212_REF();
                            SegmentCounter++;
                        }
                        o277_REF.REF01_TypeCode = oSegment.get_DataElementValue(1, 0);
                        o277_REF.REF02_ID = oSegment.get_DataElementValue(2, 0);
                        if (o277_TRN != null)
                        {
                            if (o277_TRN.TRN_REF == null) { o277_TRN.TRN_REF = new List<Cls_277X212_REF>(); }
                            o277_TRN.TRN_REF.Add(o277_REF);
                            o277_REF.Dispose();
                            o277_REF = null;
                        }
                    }
                    else if (sSegmentID == "DTP") 		//Claim Service Date 
                    {
                        if (o277_DTP == null)
                        {
                            o277_DTP = new Cls_277X212_DTP();
                            SegmentCounter++;
                        }
                        o277_DTP.DTP01_DateQualifier = oSegment.get_DataElementValue(1, 0); 		// Date/Time Qualifier (374) 
                        o277_DTP.DTP02_DateFormatQualifier = oSegment.get_DataElementValue(2, 0); 		// Date Time Period Format Qualifier (1250) 
                        o277_DTP.DTP03_Date = oSegment.get_DataElementValue(3, 0); 		// Date Time Period (1251) 
                        if (o277_TRN != null)
                        {
                            o277_TRN.TRN_DTP = o277_DTP;
                            o277_DTP.Dispose();
                            o277_DTP = null;
                        }
                    }// sSegmentID == "DTP" 
                    #endregion
                }

                //2220D / 2220E SERVICE LINE INFORMATION
                else if (sLoopSection == "HL;TRN;SVC")
                {
                    #region "2220D / 2220E SERVICE LINE INFORMATION"
                    if (sSegmentID == "SVC")    //Service Line Information
                    {
                        if (o277_SVC == null)
                        {
                            o277_SVC = new Cls_277X212_SVC();
                            SegmentCounter++;
                        }
                        o277_SVC.SVC01_1_ServiceIDQualifier = oSegment.get_DataElementValue(1, 1);     //Product/Service ID Qualifier
                        o277_SVC.SVC01_2_ServiceID = oSegment.get_DataElementValue(1, 2);     //Product/Service ID
                        o277_SVC.SVC01_3_Modifier = oSegment.get_DataElementValue(1, 3); 		// Procedure Modifier (1339) 
                        o277_SVC.SVC01_4_Modifier = oSegment.get_DataElementValue(1, 4); 		// Procedure Modifier (1339) 
                        o277_SVC.SVC01_5_Modifier = oSegment.get_DataElementValue(1, 5); 		// Procedure Modifier (1339) 
                        o277_SVC.SVC01_6_Modifier = oSegment.get_DataElementValue(1, 6); 		// Procedure Modifier (1339) 
                        o277_SVC.SVC02_Amount = oSegment.get_DataElementValue(2, 0); 		// Monetary Amount (782) 
                        o277_SVC.SVC03_Amount = oSegment.get_DataElementValue(3, 0);     //Monetary Amount
                        o277_SVC.SVC04_ProductID = oSegment.get_DataElementValue(4, 0);     //Product/Service ID
                        o277_SVC.SVC05_Quantity = oSegment.get_DataElementValue(7, 0);     //Quantity
                    }
                    else if (sSegmentID == "STC")   //Service Line Status Information
                    {
                        if (o277_STC == null)
                        {
                            o277_STC = new Cls_277X212_STC();
                            SegmentCounter++;
                        }
                        o277_STC.STC01_1_StatusCategoryCode = oSegment.get_DataElementValue(1, 1); 		// Industry Code (1271) 
                        o277_STC.STC01_2_StatusCode = oSegment.get_DataElementValue(1, 2); 		// Industry Code (1271) 
                        o277_STC.STC01_3_EntityIdentifierCode = oSegment.get_DataElementValue(1, 3); 		// Entity Identifier Code (98) 
                        o277_STC.STC01_4_CodeListQualifier = oSegment.get_DataElementValue(1, 4); 		// Code List Qualifier Code (1270) 
                        o277_STC.STC02_StatusInfoEffecticeDate = oSegment.get_DataElementValue(2, 0); 		// Date (373) 
                        o277_STC.STC10_1_IndustryCode = oSegment.get_DataElementValue(10, 1); 		// Industry Code (1271) 
                        o277_STC.STC10_2_IndustryCode = oSegment.get_DataElementValue(10, 2); 		// Industry Code (1271) 
                        o277_STC.STC10_3_EntityIDCode = oSegment.get_DataElementValue(10, 3); 		// Entity Identifier Code (98) 
                        o277_STC.STC10_4_CodeListQualifierCode = oSegment.get_DataElementValue(10, 4); 		// Code List Qualifier Code (1270) 
                        o277_STC.STC11_1_IndustryCode = oSegment.get_DataElementValue(11, 1); 		// Industry Code (1271) 
                        o277_STC.STC11_2_IndustryCode = oSegment.get_DataElementValue(11, 2); 		// Industry Code (1271) 
                        o277_STC.STC11_3_EntityIDCode = oSegment.get_DataElementValue(11, 3); 		// Entity Identifier Code (98) 
                        o277_STC.STC11_4_CodeListQualifierCode = oSegment.get_DataElementValue(11, 4); 		// Code List Qualifier Code (1270) 
                        if (o277_SVC != null)
                        {
                            if (o277_SVC.SVC_STC == null) { o277_SVC.SVC_STC = new List<Cls_277X212_STC>(); }
                            o277_SVC.SVC_STC.Add(o277_STC);
                            o277_STC.Dispose();
                            o277_STC = null;
                        }
                    }

                    else if (sSegmentID == "REF") 		//Service Line Item Identification 
                    {
                        if (o277_REF == null)
                        {
                            o277_REF = new Cls_277X212_REF();
                            SegmentCounter++;
                        }
                        o277_REF.REF01_TypeCode = oSegment.get_DataElementValue(1, 0);
                        o277_REF.REF02_ID = oSegment.get_DataElementValue(2, 0); 		// Reference Identification (127) 
                        if (o277_SVC != null)
                        {
                            o277_SVC.SVC_REF = o277_REF;
                            o277_REF.Dispose();
                            o277_REF = null;
                        }
                    }

                    else if (sSegmentID == "DTP")   //Service Line Date
                    {
                        if (o277_DTP == null)
                        {
                            o277_DTP = new Cls_277X212_DTP();
                            SegmentCounter++;
                        }
                        o277_DTP.DTP01_DateQualifier = oSegment.get_DataElementValue(1, 0);     //Date/Time Qualifier
                        o277_DTP.DTP02_DateFormatQualifier = oSegment.get_DataElementValue(2, 0);     //Date Time Period Format Qualifier
                        o277_DTP.DTP03_Date = oSegment.get_DataElementValue(3, 0); 		// Date Time Period (1251) 
                        if (o277_SVC != null)
                        {
                            o277_SVC.SVC_DTP = o277_DTP;
                            o277_DTP.Dispose();
                            o277_DTP = null;
                        }

                        if (o277_SVC != null)
                        {
                            if (o277_TRN != null)
                            {
                                if (o277_TRN.TRN_SVC == null) { o277_TRN.TRN_SVC = new List<Cls_277X212_SVC>(); }
                                o277_TRN.TRN_SVC.Add(o277_SVC);

                                o277_SVC.Dispose();
                                o277_SVC = null;

                                if (o277_HL != null)
                                {
                                    if (o277_HL.HL_TRN == null) { o277_HL.HL_TRN = new List<Cls_277X212_TRN>(); }
                                    o277_HL.HL_TRN.Add(o277_TRN);
                                    o277_TRN.Dispose();
                                    o277_TRN = null;
                                }
                            }
                        }
                    }//sSegmentID
                    #endregion

                }//sLoopSection
            }
            catch (Exception ex)
            {
                clsQEDILogs.ExceptionLog(ActivityModule.EDI277Operation, ActivityType.ParseEDI, ex, ActivityOutCome.Failure);
            }
        }//Proc_ClaimStatus

        private void SetIDforSegments()
        {
            try
            {
                if (dtUniqueIDs != null)
                {
                    if (dtUniqueIDs.Rows.Count > 0)
                    {
                        ISAID = Convert.ToInt64(dtUniqueIDs.Rows[UniqueIdCounter][0]);
                        o277_ISA.ERAFileID = ERAFileID;
                        o277_ISA.ISAID = ISAID;
                        UniqueIdCounter++;
                        CreateISADataTable();

                        o277_ISA.ISA_GS.ERAFileID = ERAFileID;
                        o277_ISA.ISA_GS.GS_ISAID = ISAID;
                        o277_ISA.ISA_GS.GSID = Convert.ToInt64(dtUniqueIDs.Rows[UniqueIdCounter][0]);
                        CreateGSDatatable(o277_ISA.ISA_GS);
                        UniqueIdCounter++;
                        for (int st = 0; st < o277_ISA.ISA_ST.Count; st++)
                        {

                            o277_ISA.ISA_ST[st].ERAFileID = ERAFileID;
                            o277_ISA.ISA_ST[st].ST_ISAID = ISAID;
                            CurrentSTID = Convert.ToInt64(dtUniqueIDs.Rows[UniqueIdCounter][0]);
                            UniqueIdCounter++;
                            o277_ISA.ISA_ST[st].STID = CurrentSTID;
                            CreateSTDatatable(o277_ISA.ISA_ST[st]);

                            o277_ISA.ISA_ST[st].ST_BHT.ERAFileID = ERAFileID;
                            o277_ISA.ISA_ST[st].ST_BHT.BHT_ISAID = ISAID;
                            o277_ISA.ISA_ST[st].ST_BHT.BHT_STID = CurrentSTID;
                            o277_ISA.ISA_ST[st].ST_BHT.BHTID = Convert.ToInt64(dtUniqueIDs.Rows[UniqueIdCounter][0]);
                            UniqueIdCounter++;
                            CreateBHTDatatable(o277_ISA.ISA_ST[st].ST_BHT);

                            for (int hl = 0; hl < o277_ISA.ISA_ST[st].ST_HL.Count; hl++)
                            {
                                o277_ISA.ISA_ST[st].ST_HL[hl].ERAFileID = ERAFileID;
                                o277_ISA.ISA_ST[st].ST_HL[hl].HL_ISAID = ISAID;
                                o277_ISA.ISA_ST[st].ST_HL[hl].HL_STID = CurrentSTID;
                                CurrentHLId = Convert.ToInt64(dtUniqueIDs.Rows[UniqueIdCounter][0]);
                                UniqueIdCounter++;
                                o277_ISA.ISA_ST[st].ST_HL[hl].HLID = CurrentHLId;
                                switch (Convert.ToString(o277_ISA.ISA_ST[st].ST_HL[hl].HL03_LevelCode))
                                {
                                    case "20": SourceHLId = CurrentHLId;  //Source
                                        o277_ISA.ISA_ST[st].ST_HL[hl].ParentHLID = 0;
                                        break;
                                    case "21": ReceiverHLId = CurrentHLId;                                  //Reveiver
                                        o277_ISA.ISA_ST[st].ST_HL[hl].ParentHLID = SourceHLId;
                                        break;
                                    case "19": ProviderHLId = CurrentHLId;                                  //Provider
                                        o277_ISA.ISA_ST[st].ST_HL[hl].ParentHLID = ReceiverHLId;
                                        break;
                                    case "22": SubscriberHLId = CurrentHLId;                               //Subcriber
                                        o277_ISA.ISA_ST[st].ST_HL[hl].ParentHLID = ProviderHLId;
                                        break;
                                    case "23": DependentHLId = CurrentHLId;
                                        o277_ISA.ISA_ST[st].ST_HL[hl].ParentHLID = SubscriberHLId;   //Dependent
                                        break;
                                }
                                CreateHLDatatable(o277_ISA.ISA_ST[st].ST_HL[hl]);

                                o277_ISA.ISA_ST[st].ST_HL[hl].HL_NM.ERAFileID = ERAFileID;
                                o277_ISA.ISA_ST[st].ST_HL[hl].HL_NM.NM_ISAID = ISAID;
                                o277_ISA.ISA_ST[st].ST_HL[hl].HL_NM.NM_STID = CurrentSTID;
                                o277_ISA.ISA_ST[st].ST_HL[hl].HL_NM.NM_HLID = CurrentHLId;
                                o277_ISA.ISA_ST[st].ST_HL[hl].HL_NM.NMID = Convert.ToInt64(dtUniqueIDs.Rows[UniqueIdCounter][0]);
                                UniqueIdCounter++;
                                CreateNMDatatable(o277_ISA.ISA_ST[st].ST_HL[hl].HL_NM);

                                if (o277_ISA.ISA_ST[st].ST_HL[hl].HL_PER != null)
                                {
                                    o277_ISA.ISA_ST[st].ST_HL[hl].HL_PER.ERAFileID = ERAFileID;
                                    o277_ISA.ISA_ST[st].ST_HL[hl].HL_PER.PER_ISAID = ISAID;
                                    o277_ISA.ISA_ST[st].ST_HL[hl].HL_PER.PER_STID = CurrentSTID;
                                    o277_ISA.ISA_ST[st].ST_HL[hl].HL_PER.PER_HLID = CurrentHLId;
                                    o277_ISA.ISA_ST[st].ST_HL[hl].HL_PER.PERID = Convert.ToInt64(dtUniqueIDs.Rows[UniqueIdCounter][0]);
                                    UniqueIdCounter++;
                                    CreatePERDatatable(o277_ISA.ISA_ST[st].ST_HL[hl].HL_PER);
                                }

                                if (o277_ISA.ISA_ST[st].ST_HL[hl].HL_TRN != null)
                                {
                                    for (int trn = 0; trn < o277_ISA.ISA_ST[st].ST_HL[hl].HL_TRN.Count; trn++)
                                    {
                                        o277_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].ERAFileID = ERAFileID;
                                        o277_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_ISAID = ISAID;
                                        o277_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_STID = CurrentSTID;
                                        o277_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_HLID = CurrentHLId;
                                        CurrentTRNId = Convert.ToInt64(dtUniqueIDs.Rows[UniqueIdCounter][0]);
                                        UniqueIdCounter++;
                                        o277_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRNID = CurrentTRNId;
                                        CreateTRNDatatable(o277_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn]);

                                        if (o277_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_STC != null)
                                        {
                                            for (int stc = 0; stc < o277_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_STC.Count; stc++)
                                            {
                                                o277_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_STC[stc].ERAFileID = ERAFileID;
                                                o277_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_STC[stc].STC_ISAID = ISAID;
                                                o277_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_STC[stc].STC_STID = CurrentSTID;
                                                o277_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_STC[stc].STC_HLID = CurrentHLId;
                                                o277_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_STC[stc].STC_TRNID = CurrentTRNId;
                                                o277_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_STC[stc].STCID = Convert.ToInt64(dtUniqueIDs.Rows[UniqueIdCounter][0]);
                                                UniqueIdCounter++;
                                                CreateSTCDatatable(o277_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_STC[stc]);
                                            }
                                        }

                                        if (o277_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_REF != null)
                                        {
                                            for (int reff = 0; reff < o277_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_REF.Count; reff++)
                                            {
                                                o277_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_REF[reff].ERAFileID = ERAFileID;
                                                o277_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_REF[reff].REF_ISAID = ISAID;
                                                o277_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_REF[reff].REF_STID = CurrentSTID;
                                                o277_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_REF[reff].REF_HLID = CurrentHLId;
                                                o277_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_REF[reff].REF_TRNID = CurrentTRNId;
                                                o277_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_REF[reff].REFID = Convert.ToInt64(dtUniqueIDs.Rows[UniqueIdCounter][0]);
                                                UniqueIdCounter++;
                                                CreateREFDatatable(o277_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_REF[reff]);
                                            }
                                        }

                                        if (o277_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_DTP != null)
                                        {
                                            o277_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_DTP.ERAFileID = ERAFileID;
                                            o277_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_DTP.DTP_ISAID = ISAID;
                                            o277_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_DTP.DTP_STID = CurrentSTID;
                                            o277_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_DTP.DTP_HLID = CurrentHLId;
                                            o277_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_DTP.DTP_TRNID = CurrentTRNId;
                                            o277_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_DTP.DTPID = Convert.ToInt64(dtUniqueIDs.Rows[UniqueIdCounter][0]);
                                            UniqueIdCounter++;
                                            CreateDTPDatatable(o277_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_DTP);
                                        }

                                        if (o277_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_SVC != null)
                                        {
                                            for (int trnsvc = 0; trnsvc < o277_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_SVC.Count; trnsvc++)
                                            {
                                                o277_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_SVC[trnsvc].ERAFileID = ERAFileID;
                                                o277_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_SVC[trnsvc].SVC_ISAID = ISAID;
                                                o277_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_SVC[trnsvc].SVC_STID = CurrentSTID;
                                                o277_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_SVC[trnsvc].SVC_HLID = CurrentHLId;
                                                o277_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_SVC[trnsvc].SVC_TRNID = CurrentTRNId;
                                                CurrentSVCId = Convert.ToInt64(dtUniqueIDs.Rows[UniqueIdCounter][0]);
                                                o277_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_SVC[trnsvc].SVCID = CurrentSVCId;
                                                UniqueIdCounter++;
                                                CreateSVCDatatable(o277_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_SVC[trnsvc]);

                                                if (o277_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_SVC[trnsvc].SVC_STC != null)
                                                {
                                                    for (int trnsvcstc = 0; trnsvcstc < o277_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_SVC[trnsvc].SVC_STC.Count; trnsvcstc++)
                                                    {
                                                        o277_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_SVC[trnsvc].SVC_STC[trnsvcstc].ERAFileID = ERAFileID;
                                                        o277_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_SVC[trnsvc].SVC_STC[trnsvcstc].STC_ISAID = ISAID;
                                                        o277_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_SVC[trnsvc].SVC_STC[trnsvcstc].STC_STID = CurrentSTID;
                                                        o277_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_SVC[trnsvc].SVC_STC[trnsvcstc].STC_HLID = CurrentHLId;
                                                        o277_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_SVC[trnsvc].SVC_STC[trnsvcstc].STC_TRNID = CurrentTRNId;
                                                        o277_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_SVC[trnsvc].SVC_STC[trnsvcstc].STC_SVCID = CurrentSVCId;
                                                        o277_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_SVC[trnsvc].SVC_STC[trnsvcstc].STCID = Convert.ToInt64(dtUniqueIDs.Rows[UniqueIdCounter][0]);
                                                        UniqueIdCounter++;
                                                        CreateSTCDatatable(o277_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_SVC[trnsvc].SVC_STC[trnsvcstc]);
                                                    }
                                                }

                                                if (o277_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_SVC[trnsvc].SVC_REF != null)
                                                {
                                                    o277_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_SVC[trnsvc].SVC_REF.ERAFileID = ERAFileID;
                                                    o277_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_SVC[trnsvc].SVC_REF.REF_ISAID = ISAID;
                                                    o277_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_SVC[trnsvc].SVC_REF.REF_STID = CurrentSTID;
                                                    o277_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_SVC[trnsvc].SVC_REF.REF_HLID = CurrentHLId;
                                                    o277_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_SVC[trnsvc].SVC_REF.REF_TRNID = CurrentTRNId;
                                                    o277_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_SVC[trnsvc].SVC_REF.REF_SVCID = CurrentSVCId;
                                                    o277_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_SVC[trnsvc].SVC_REF.REFID = Convert.ToInt64(dtUniqueIDs.Rows[UniqueIdCounter][0]);
                                                    UniqueIdCounter++;
                                                    CreateREFDatatable(o277_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_SVC[trnsvc].SVC_REF);
                                                }

                                                if (o277_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_SVC[trnsvc].SVC_DTP != null)
                                                {
                                                    o277_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_SVC[trnsvc].SVC_DTP.ERAFileID = ERAFileID;
                                                    o277_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_SVC[trnsvc].SVC_DTP.DTP_ISAID = ISAID;
                                                    o277_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_SVC[trnsvc].SVC_DTP.DTP_STID = CurrentSTID;
                                                    o277_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_SVC[trnsvc].SVC_DTP.DTP_HLID = CurrentHLId;
                                                    o277_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_SVC[trnsvc].SVC_DTP.DTP_TRNID = CurrentTRNId;
                                                    o277_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_SVC[trnsvc].SVC_DTP.DTP_SVCID = CurrentSVCId;
                                                    o277_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_SVC[trnsvc].SVC_DTP.DTPID = Convert.ToInt64(dtUniqueIDs.Rows[UniqueIdCounter][0]);
                                                    UniqueIdCounter++;
                                                    CreateDTPDatatable(o277_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_SVC[trnsvc].SVC_DTP);
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
               // ShowException(ex.ToString());
                clsQEDILogs.ExceptionLog(ActivityModule.EDI277Operation, ActivityType.GenerateSegmentIDs, ex, ActivityOutCome.Failure);
            }
        }

        private bool Save277X212InDB()
        {

            SqlConnection conn = new System.Data.SqlClient.SqlConnection(_DataBaseConnectionString);
            conn.Open();
            //SqlTransaction tran = conn.BeginTransaction(IsolationLevel.ReadCommitted);
            SqlBulkCopy bulkCopy = null;
            try
            {
                bulkCopy = new System.Data.SqlClient.SqlBulkCopy(_DataBaseConnectionString);
                bulkCopy.BulkCopyTimeout = 0;


                if (dtISA != null && dtISA.Rows.Count > 0)
                {
                    bulkCopy.DestinationTableName = "dbo.EDIResponse_277Segment_ISA";
                    bulkCopy.ColumnMappings.Clear();
                    bulkCopy.ColumnMappings.Add("ERAFileID", "ERAFileID");
                    bulkCopy.ColumnMappings.Add("ISAID", "ISAID");
                    bulkCopy.ColumnMappings.Add("ISA01_AuthorInfoQual", "ISA01_AuthorInfoQual");
                    bulkCopy.ColumnMappings.Add("ISA02_AuthorInfo", "ISA02_AuthorInfo");
                    bulkCopy.ColumnMappings.Add("ISA03_SecurityInfoQual", "ISA03_SecurityInfoQual");
                    bulkCopy.ColumnMappings.Add("ISA04_SecurityInfo", "ISA04_SecurityInfo");
                    bulkCopy.ColumnMappings.Add("ISA05_IntrChngIDQual", "ISA05_IntrChngIDQual");
                    bulkCopy.ColumnMappings.Add("ISA06_IntrChngSenderID", "ISA06_IntrChngSenderID");
                    bulkCopy.ColumnMappings.Add("ISA07_IntrChngIDQual", "ISA07_IntrChngIDQual");
                    bulkCopy.ColumnMappings.Add("ISA08_IntrChngReceiverID", "ISA08_IntrChngReceiverID");
                    bulkCopy.ColumnMappings.Add("ISA09_IntrChngDate", "ISA09_IntrChngDate");
                    bulkCopy.ColumnMappings.Add("ISA10_IntrChngTime", "ISA10_IntrChngTime");
                    bulkCopy.ColumnMappings.Add("ISA11_IntrChngRepetitionSeparator", "ISA11_IntrChngRepetitionSeparator");
                    bulkCopy.ColumnMappings.Add("ISA12_IntrChngControlVersionNo", "ISA12_IntrChngControlVersionNo");
                    bulkCopy.ColumnMappings.Add("ISA13_IntrChngControlNo", "ISA13_IntrChngControlNo");
                    bulkCopy.ColumnMappings.Add("ISA14_AckwRequested", "ISA14_AckwRequested");
                    bulkCopy.ColumnMappings.Add("ISA15_UsageIndicator", "ISA15_UsageIndicator");
                    bulkCopy.ColumnMappings.Add("ISA16_ComponentElementSeparator", "ISA16_ComponentElementSeparator");
                    bulkCopy.WriteToServer(dtISA);
                }

                if (dtGS != null && dtGS.Rows.Count > 0)
                {
                    bulkCopy.DestinationTableName = "dbo.EDIResponse_277Segment_GS";
                    bulkCopy.ColumnMappings.Clear();
                    bulkCopy.ColumnMappings.Add("ERAFileID", "ERAFileID");
                    bulkCopy.ColumnMappings.Add("GS_ISAID", "ISAID");
                    bulkCopy.ColumnMappings.Add("GSID", "GSID");
                    bulkCopy.ColumnMappings.Add("GS01_FunctionalIdCode", "GS01_StatusNotification");
                    bulkCopy.ColumnMappings.Add("GS02_SenderCode", "GS02_SenderID");
                    bulkCopy.ColumnMappings.Add("GS03_ReceiverCode", "GS03_SiteId");
                    bulkCopy.ColumnMappings.Add("GS04_FunctionalGroupDate", "GS04_FunctionalGroupDate");
                    bulkCopy.ColumnMappings.Add("GS05_FunctionalGroupTime", "GS05_FunctionalGroupTime");
                    bulkCopy.ColumnMappings.Add("GS06_GroupControlNumber", "GS06_GroupControlNumber");
                    bulkCopy.ColumnMappings.Add("GS07_ResponsibleAgencyCode", "GS07_AccreditedStandardsCode");
                    bulkCopy.ColumnMappings.Add("GS08_VersionIDCode", "GS08_EDITechicalReportTypeCode");
                    bulkCopy.WriteToServer(dtGS);
                }

                if (dtST != null && dtST.Rows.Count > 0)
                {
                    bulkCopy.DestinationTableName = "dbo.EDIResponse_277Segment_ST";
                    bulkCopy.ColumnMappings.Clear();
                    bulkCopy.ColumnMappings.Add("ERAFileID", "ERAFileID");
                    bulkCopy.ColumnMappings.Add("ST_ISAID", "ISAID");
                    bulkCopy.ColumnMappings.Add("STID", "STID");
                    bulkCopy.ColumnMappings.Add("ST01_IdentifierCode", "ST01_IdentifierCode");
                    bulkCopy.ColumnMappings.Add("ST02_TransactioNSetControlNumber", "ST02_TransactioNSetControlNumber");
                    bulkCopy.ColumnMappings.Add("ST03_ConventionReference", "ST03_ConventionReference");
                    bulkCopy.WriteToServer(dtST);
                }


                if (dtBHT != null && dtBHT.Rows.Count > 0)
                {
                    bulkCopy.DestinationTableName = "dbo.EDIResponse_277Segment_BHT";
                    bulkCopy.ColumnMappings.Clear();
                    bulkCopy.ColumnMappings.Add("ERAFileID", "ERAFileID");
                    bulkCopy.ColumnMappings.Add("BHT_ISAID", "ISAID");
                    bulkCopy.ColumnMappings.Add("BHT_STID", "STID");
                    bulkCopy.ColumnMappings.Add("BHTID", "BHTID");
                    bulkCopy.ColumnMappings.Add("BHT01_StructureCode", "BHT01_StructureCode");
                    bulkCopy.ColumnMappings.Add("BHT02_PurposeCode", "BHT02_PurposeCode");
                    bulkCopy.ColumnMappings.Add("BHT03_ReferenceIdentification", "BHT03_ReferenceIdentification");
                    bulkCopy.ColumnMappings.Add("BHT04_TransactionDate", "BHT04_TransactionDate");
                    bulkCopy.ColumnMappings.Add("BHT05_TransactionTime", "BHT05_TransactionTime");
                    bulkCopy.ColumnMappings.Add("BHT06_transactionTypeCode", "BHT06_transactionTypeCode");
                    bulkCopy.WriteToServer(dtBHT);
                }

                if (dtHL != null && dtHL.Rows.Count > 0)
                {
                    bulkCopy.DestinationTableName = "dbo.EDIResponse_277Segment_HL";
                    bulkCopy.ColumnMappings.Clear();
                    bulkCopy.ColumnMappings.Add("ERAFileID", "ERAFileID");
                    bulkCopy.ColumnMappings.Add("HL_ISAID", "ISAID");
                    bulkCopy.ColumnMappings.Add("HL_STID", "STID");
                    bulkCopy.ColumnMappings.Add("ParentHLID", "ParentHLID");
                    bulkCopy.ColumnMappings.Add("HLID", "HLID");
                    bulkCopy.ColumnMappings.Add("HL01_HLSegmentId", "HL01_HLSegmentId");
                    bulkCopy.ColumnMappings.Add("HL02_HLParentId", "HL02_HLParentId");
                    bulkCopy.ColumnMappings.Add("HL03_LevelCode", "HL03_LevelCode");
                    bulkCopy.ColumnMappings.Add("HL04_ChildCode", "HL04_ChildCode");
                    bulkCopy.WriteToServer(dtHL);
                }

                if (dtNM != null && dtNM.Rows.Count > 0)
                {
                    bulkCopy.DestinationTableName = "dbo.EDIResponse_277Segment_NM";
                    bulkCopy.ColumnMappings.Clear();
                    bulkCopy.ColumnMappings.Add("ERAFileID", "ERAFileID");
                    bulkCopy.ColumnMappings.Add("NM_ISAID", "ISAID");
                    bulkCopy.ColumnMappings.Add("NM_STID", "STID");
                    bulkCopy.ColumnMappings.Add("NM_HLID", "HLID");
                    bulkCopy.ColumnMappings.Add("NMID", "NMID");
                    bulkCopy.ColumnMappings.Add("NM101_EntityIdCode", "NM101_EntityIdCode");
                    bulkCopy.ColumnMappings.Add("NM102_EntityTypeQualifier", "NM102_EntityTypeQualifier");
                    bulkCopy.ColumnMappings.Add("NM103_LastName", "NM103_LastName");
                    bulkCopy.ColumnMappings.Add("NM104_FirstName", "NM104_FirstName");
                    bulkCopy.ColumnMappings.Add("NM105_MiddleName", "NM105_MiddleName");
                    bulkCopy.ColumnMappings.Add("NM106_Prefix", "NM106_Prefix");
                    bulkCopy.ColumnMappings.Add("NM107_Suffix", "NM107_Suffix");
                    bulkCopy.ColumnMappings.Add("NM108_IdentificationCodeQualifier", "NM108_IdentificationCodeQualifier");
                    bulkCopy.ColumnMappings.Add("NM109_IdentificationCode", "NM109_IdentificationCode");
                    bulkCopy.ColumnMappings.Add("NM110_EntityRelationShipCode", "NM110_EntityRelationShipCode");
                    bulkCopy.ColumnMappings.Add("NM111_EntityIdentifierCode", "NM111_EntityIdentifierCode");
                    bulkCopy.ColumnMappings.Add("NM112_SecondLastName", "NM112_SecondLastName");
                    bulkCopy.WriteToServer(dtNM);
                }

                if (dtPER != null && dtPER.Rows.Count > 0)
                {
                    bulkCopy.DestinationTableName = "dbo.EDIResponse_277Segment_PER";
                    bulkCopy.ColumnMappings.Clear();
                    bulkCopy.ColumnMappings.Add("ERAFileID", "ERAFileID");
                    bulkCopy.ColumnMappings.Add("PER_ISAID", "ISAID");
                    bulkCopy.ColumnMappings.Add("PER_STID", "STID");
                    bulkCopy.ColumnMappings.Add("PER_HLID", "HLID");
                    bulkCopy.ColumnMappings.Add("PERID", "PERID");
                    bulkCopy.ColumnMappings.Add("PER01_ContactFunctionCode", "PER01_ContactFunctionCode");
                    bulkCopy.ColumnMappings.Add("PER02_Name", "PER02_Name");
                    bulkCopy.ColumnMappings.Add("PER03_CommNumberQualifier", "PER03_CommNumberQualifier");
                    bulkCopy.ColumnMappings.Add("PER04_CommNumber", "PER04_CommNumber");
                    bulkCopy.ColumnMappings.Add("PER05_CommNumberQualifier", "PER05_CommNumberQualifier");
                    bulkCopy.ColumnMappings.Add("PER06_CommNumber", "PER06_CommNumber");
                    bulkCopy.ColumnMappings.Add("PER07_CommNumberQualifier", "PER07_CommNumberQualifier");
                    bulkCopy.ColumnMappings.Add("PER08_CommNumber", "PER08_CommNumber");
                    bulkCopy.ColumnMappings.Add("PER09_ContactInquiryReference", "PER09_ContactInquiryReference");
                    bulkCopy.WriteToServer(dtPER);
                }

                if (dtTRN != null && dtTRN.Rows.Count > 0)
                {
                    bulkCopy.DestinationTableName = "dbo.EDIResponse_277Segment_TRN";
                    bulkCopy.ColumnMappings.Clear();
                    bulkCopy.ColumnMappings.Add("ERAFileID", "ERAFileID");
                    bulkCopy.ColumnMappings.Add("TRN_ISAID", "ISAID");
                    bulkCopy.ColumnMappings.Add("TRN_STID", "STID");
                    bulkCopy.ColumnMappings.Add("TRN_HLID", "HLID");
                    bulkCopy.ColumnMappings.Add("TRNID", "TRNID");
                    bulkCopy.ColumnMappings.Add("TRN01_TraceTypeCode", "TRN01_TraceTypeCode");
                    bulkCopy.ColumnMappings.Add("TRN02_ReferenceIdentification", "TRN02_TransactionUniqueNumber");
                    bulkCopy.ColumnMappings.Add("TRN03_OriginatingCompanyId", "TRN03_OriginatingCompanyId");
                    bulkCopy.ColumnMappings.Add("TRN04_ReferenceID", "TRN04_ReferenceID");
                    bulkCopy.WriteToServer(dtTRN);
                }

                if (dtSVC != null && dtSVC.Rows.Count > 0)
                {
                    bulkCopy.DestinationTableName = "dbo.EDIResponse_277Segment_SVC";
                    bulkCopy.ColumnMappings.Clear();
                    bulkCopy.ColumnMappings.Add("ERAFileID", "ERAFileID");
                    bulkCopy.ColumnMappings.Add("SVC_ISAID", "ISAID");
                    bulkCopy.ColumnMappings.Add("SVC_STID", "STID");
                    bulkCopy.ColumnMappings.Add("SVC_HLID", "HLID");
                    bulkCopy.ColumnMappings.Add("SVC_TRNID", "TRNID");
                    bulkCopy.ColumnMappings.Add("SVCID", "SVCID");
                    bulkCopy.ColumnMappings.Add("SVC01_1_ServiceIDQualifier", "SVC01_1_ServiceIDQualifier");
                    bulkCopy.ColumnMappings.Add("SVC01_2_ServiceID", "SVC01_2_ServiceID");
                    bulkCopy.ColumnMappings.Add("SVC01_3_Modifier", "SVC01_3_Modifier");
                    bulkCopy.ColumnMappings.Add("SVC01_4_Modifier", "SVC01_4_Modifier");
                    bulkCopy.ColumnMappings.Add("SVC01_5_Modifier", "SVC01_5_Modifier");
                    bulkCopy.ColumnMappings.Add("SVC01_6_Modifier", "SVC01_6_Modifier");
                    bulkCopy.ColumnMappings.Add("SVC01_7_Description", "SVC01_7_Description");
                    bulkCopy.ColumnMappings.Add("SVC01_8_ServiceId", "SVC01_8_ServiceId");
                    bulkCopy.ColumnMappings.Add("SVC02_Amount", "SVC02_Amount");
                    bulkCopy.ColumnMappings.Add("SVC03_Amount", "SVC03_Amount");
                    bulkCopy.ColumnMappings.Add("SVC04_ProductID", "SVC04_ProductID");
                    bulkCopy.ColumnMappings.Add("SVC05_Quantity", "SVC05_Quantity");
                    bulkCopy.ColumnMappings.Add("SVC06_1_ServiceIDQualifier", "SVC06_1_ServiceIDQualifier");
                    bulkCopy.ColumnMappings.Add("SVC06_2_ServiceID", "SVC06_2_ServiceID");
                    bulkCopy.ColumnMappings.Add("SVC06_3_Modifier", "SVC06_3_Modifier");
                    bulkCopy.ColumnMappings.Add("SVC06_4_Modifier", "SVC06_4_Modifier");
                    bulkCopy.ColumnMappings.Add("SVC06_5_Modifier", "SVC06_5_Modifier");
                    bulkCopy.ColumnMappings.Add("SVC06_6_Modifier", "SVC06_6_Modifier");
                    bulkCopy.ColumnMappings.Add("SVC06_7_Description", "SVC06_7_Description");
                    bulkCopy.ColumnMappings.Add("SVC06_8_ServiceId", "SVC06_8_ServiceId");
                    bulkCopy.ColumnMappings.Add("SVC07_Quantity", "SVC07_Quantity");
                    bulkCopy.WriteToServer(dtSVC);
                }

                if (dtSTC != null && dtSTC.Rows.Count > 0)
                {
                    bulkCopy.DestinationTableName = "dbo.EDIResponse_277Segment_STC";
                    bulkCopy.ColumnMappings.Clear();
                    bulkCopy.ColumnMappings.Add("ERAFileID", "ERAFileID");
                    bulkCopy.ColumnMappings.Add("STC_ISAID", "ISAID");
                    bulkCopy.ColumnMappings.Add("STC_STID", "STID");
                    bulkCopy.ColumnMappings.Add("STC_HLID", "HLID");
                    bulkCopy.ColumnMappings.Add("STC_TRNID", "TRNID");
                    bulkCopy.ColumnMappings.Add("STC_SVCID", "SVCID");
                    bulkCopy.ColumnMappings.Add("STCID", "STCID");
                    bulkCopy.ColumnMappings.Add("STC01_1_StatusCategoryCode", "STC01_1_StatusCategoryCode");
                    bulkCopy.ColumnMappings.Add("STC01_2_StatusCode", "STC01_2_StatusCode");
                    bulkCopy.ColumnMappings.Add("STC01_3_EntityIdentifierCode", "STC01_3_EntityIdentifierCode");
                    bulkCopy.ColumnMappings.Add("STC01_4_CodeListQualifier", "STC01_4_CodeListQualifier");
                    bulkCopy.ColumnMappings.Add("STC02_StatusInfoEffecticeDate", "STC02_StatusInfoEffecticeDate");
                    bulkCopy.ColumnMappings.Add("STC03_StatusInfoActionCode", "STC03_StatusInfoActionCode");
                    bulkCopy.ColumnMappings.Add("STC04_TotalClaimChargeAmount", "STC04_TotalClaimChargeAmount");
                    bulkCopy.ColumnMappings.Add("STC05_TotalClaimChargeAmount", "STC05_TotalClaimChargeAmount");
                    bulkCopy.ColumnMappings.Add("STC06_Date", "STC06_Date");
                    bulkCopy.ColumnMappings.Add("STC07_PaymentMethodCode", "STC07_PaymentMethodCode");
                    bulkCopy.ColumnMappings.Add("STC08_Date", "STC08_Date");
                    bulkCopy.ColumnMappings.Add("STC09_CheckNumber", "STC09_CheckNumber");
                    bulkCopy.ColumnMappings.Add("STC10_1_IndustryCode", "STC10_1_IndustryCode");
                    bulkCopy.ColumnMappings.Add("STC10_2_IndustryCode", "STC10_2_IndustryCode");
                    bulkCopy.ColumnMappings.Add("STC10_3_EntityIDCode", "STC10_3_EntityIDCode");
                    bulkCopy.ColumnMappings.Add("STC10_4_CodeListQualifierCode", "STC10_4_CodeListQualifierCode");
                    bulkCopy.ColumnMappings.Add("STC11_1_IndustryCode", "STC11_1_IndustryCode");
                    bulkCopy.ColumnMappings.Add("STC11_2_IndustryCode", "STC11_2_IndustryCode");
                    bulkCopy.ColumnMappings.Add("STC11_3_EntityIDCode", "STC11_3_EntityIDCode");
                    bulkCopy.ColumnMappings.Add("STC11_4_CodeListQualifierCode", "STC11_4_CodeListQualifierCode");
                    bulkCopy.ColumnMappings.Add("STC12_Message", "STC12_Message");
                    bulkCopy.WriteToServer(dtSTC);
                }

                if (dtREF != null && dtREF.Rows.Count > 0)
                {
                    bulkCopy.DestinationTableName = "dbo.EDIResponse_277Segment_REF";
                    bulkCopy.ColumnMappings.Clear();
                    bulkCopy.ColumnMappings.Add("ERAFileID", "ERAFileID");
                    bulkCopy.ColumnMappings.Add("REF_ISAID", "ISAID");
                    bulkCopy.ColumnMappings.Add("REF_STID", "STID");
                    bulkCopy.ColumnMappings.Add("REF_HLID", "HLID");
                    bulkCopy.ColumnMappings.Add("REF_TRNID", "TRNID");
                    bulkCopy.ColumnMappings.Add("REF_SVCID", "SVCID");
                    bulkCopy.ColumnMappings.Add("REFID", "REFID");
                    bulkCopy.ColumnMappings.Add("REF01_TypeCode", "REF01_TypeCode");
                    bulkCopy.ColumnMappings.Add("REF02_ID", "REF02_ID");
                    bulkCopy.ColumnMappings.Add("REF03_Description", "REF03_Description");
                    bulkCopy.ColumnMappings.Add("REF04_1_ReferenceIDQualifier", "REF04_1_ReferenceIDQualifier");
                    bulkCopy.ColumnMappings.Add("REF04_2_ReferenceID", "REF04_2_ReferenceID");
                    bulkCopy.ColumnMappings.Add("REF04_3_ReferenceIDQualifier", "REF04_3_ReferenceIDQualifier");
                    bulkCopy.ColumnMappings.Add("REF04_4_ReferenceID", "REF04_4_ReferenceID");
                    bulkCopy.ColumnMappings.Add("REF04_5_ReferenceIDQualifier", "REF04_5_ReferenceIDQualifier");
                    bulkCopy.ColumnMappings.Add("REF04_6_ReferenceID", "REF04_6_ReferenceID");
                    bulkCopy.WriteToServer(dtREF);
                }

                if (dtDTP != null && dtDTP.Rows.Count > 0)
                {
                    bulkCopy.DestinationTableName = "dbo.EDIResponse_277Segment_DTP";
                    bulkCopy.ColumnMappings.Clear();
                    bulkCopy.ColumnMappings.Add("ERAFileID", "ERAFileID");
                    bulkCopy.ColumnMappings.Add("DTP_ISAID", "ISAID");
                    bulkCopy.ColumnMappings.Add("DTP_STID", "STID");
                    bulkCopy.ColumnMappings.Add("DTP_HLID", "HLID");
                    bulkCopy.ColumnMappings.Add("DTP_TRNID", "TRNID");
                    bulkCopy.ColumnMappings.Add("DTP_SVCID", "SVCID");
                    bulkCopy.ColumnMappings.Add("DTPID", "DTPID");
                    bulkCopy.ColumnMappings.Add("DTP01_DateQualifier", "DTP01_DateQualifier");
                    bulkCopy.ColumnMappings.Add("DTP02_DateFormatQualifier", "DTP02_DateFormatQualifier");
                    bulkCopy.ColumnMappings.Add("DTP03_Date", "DTP03_Date");
                    bulkCopy.WriteToServer(dtDTP);
                }

                //tran.Commit();
                bulkCopy.Close();
                bulkCopy = null;
                return true;

            }
            catch (Exception ex)
            {
                clsQEDILogs.ExceptionLog(ActivityModule.EDI277Operation, ActivityType.SaveToDatabase, ex, ActivityOutCome.Failure);
                return false;
                //throw ex;
                //tran.Rollback();
            }
            finally
            {
                if (conn != null) { conn.Dispose(); conn = null; }
                //if (tran != null) { tran.Dispose(); tran = null; }
                bulkCopy = null;

            }


            
        }

        private DataTable getUniQueIDS()
        {
            DataTable dt = null;
            try
            {
                if (SegmentCounter > 0)
                {
                    if (OpenConnection(true))
                    {
                        oDBPara.Clear();
                        oDBPara.Add("@IDCount", SegmentCounter, ParameterDirection.Input, SqlDbType.BigInt);
                        oDBPara.Add("@SingleRow", 1, ParameterDirection.Input, SqlDbType.Bit);
                        oDB.Retrive("gsp_GetUniqueIDs", oDBPara, out dt);
                        CloseConnection();
                    }
                }
            }
            catch (Exception ex)
            {
                // gloAuditTrail.gloAuditTrail.ExceptionLog("Error in getUniQueIDS : " + ex.ToString(), false);
                clsQEDILogs.ExceptionLog(ActivityModule.EDI277Operation, ActivityType.FetchDatabase, ex, ActivityOutCome.Failure);
                return null;
            }
            return dt;
        }

        private DataTable getClaimStatus(string StatusCode,string StatucCategoryCode)
        {
            DataTable dt = null;
            try
            {
                if (SegmentCounter > 0)
                {
                    if (OpenConnection(true))
                    {
                        oDBPara.Clear();
                        oDBPara.Add("@StatusCategoryCode", StatucCategoryCode, ParameterDirection.Input, SqlDbType.VarChar);
                        oDBPara.Add("@StatusCode", StatusCode, ParameterDirection.Input, SqlDbType.VarChar);
                        oDB.Retrive("gsp_GET_RealTImeClaimStatus", oDBPara, out dt);
                        CloseConnection();
                    }
                }
            }
            catch (Exception ex)
            {
                // gloAuditTrail.gloAuditTrail.ExceptionLog("Error in getUniQueIDS : " + ex.ToString(), false);
                clsQEDILogs.ExceptionLog(ActivityModule.EDI277Operation, ActivityType.FetchDatabase, ex, ActivityOutCome.Failure);
                return null;
            }
            return dt;
        }
       
        private bool OpenConnection(bool withParameters)
        {
            bool _Result = false;
            try
            {
                if (_DataBaseConnectionString != "")
                {
                    oDB = new gloDatabaseLayer.DBLayer(_DataBaseConnectionString);
                    oDB.Connect(false);
                    if (withParameters)
                        oDBPara = new gloDatabaseLayer.DBParameters();
                    _Result = true;
                }
            }
            catch (Exception ex)
            {
                //gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                clsQEDILogs.ExceptionLog(ActivityModule.EDI277Operation, ActivityType.GeneralOperation, ex, ActivityOutCome.Failure);
            }
            return _Result;
        }

        private void CloseConnection()
        {
            if (oDB != null)
            {
                oDB.Disconnect();
                oDB.Dispose();
                oDB = null;
            }
            if (oDBPara != null)
            {
                oDBPara.Dispose();
                oDBPara = null;
            }
        }


        private void CreateISADataTable()
        {
            Cls_277X212_ISAs oISAs = new Cls_277X212_ISAs();
            if (o277_ISA != null)
            {
                oISAs.Add(o277_ISA);
            }
            Cls_277X212_ISA[] oISA = new Cls_277X212_ISA[oISAs.Count];
            oISAs.CopyTo(oISA, 0);
            if (oISA != null)
            {
                var _test = (from r in oISA select r).ToList();
                if (dtISA == null)
                {
                    dtISA = ConvertToDataTable(_test);
                }
                else
                {
                    AddToDataTable(_test, Segment.ISA);
                }
                oISA = null;
            }

        }

        private void CreateGSDatatable(Cls_277X212_GS _oGS)
        {
            Cls_277X212_GSs oGSs = new Cls_277X212_GSs();
            if (_oGS != null)
            {
                oGSs.Add(_oGS);
            }

            Cls_277X212_GS[] oGS = new Cls_277X212_GS[oGSs.Count];
            oGSs.CopyTo(oGS, 0);
            if (oGS != null)
            {
                var _test = (from r in oGS select r).ToList();

                if (dtGS == null)
                {
                    dtGS = ConvertToDataTable(_test);
                }
                else
                {
                    AddToDataTable(_test, Segment.GS);
                }
                oGS = null;
            }
        }

        private void CreateSTDatatable(Cls_277X212_ST _oST)
        {
            Cls_277X212_STs oGSs = new Cls_277X212_STs();
            if (_oST != null)
            {
                oGSs.Add(_oST);
            }

            Cls_277X212_ST[] oST = new Cls_277X212_ST[oGSs.Count];
            oGSs.CopyTo(oST, 0);
            if (oST != null)
            {
                var _test = (from r in oST select r).ToList();
                if (dtST == null)
                {
                    dtST = ConvertToDataTable(_test);
                }
                else
                {
                    AddToDataTable(_test, Segment.ST);
                }
                oST = null;
            }


        }

        private void CreateBHTDatatable(Cls_277X212_BHT _oBHT)
        {
            Cls_277X212_BHTs oBHTs = new Cls_277X212_BHTs();
            if (_oBHT != null)
            {
                oBHTs.Add(_oBHT);
            }

            Cls_277X212_BHT[] oBHT = new Cls_277X212_BHT[oBHTs.Count];
            oBHTs.CopyTo(oBHT, 0);
            if (oBHT != null)
            {
                var _test = (from r in oBHT select r).ToList();
                if (dtBHT == null)
                {
                    dtBHT = ConvertToDataTable(_test);
                }
                else
                {
                    AddToDataTable(_test, Segment.BHT);
                }
                oBHT = null;
            }
        }

        private void CreateHLDatatable(Cls_277X212_HL _oHL)
        {
            Cls_277X212_HLs oHLs = new Cls_277X212_HLs();
            if (_oHL != null)
            {
                oHLs.Add(_oHL);
            }

            Cls_277X212_HL[] oHL = new Cls_277X212_HL[oHLs.Count];
            oHLs.CopyTo(oHL, 0);
            if (oHL != null)
            {
                var _test = (from r in oHL select r).ToList();
                if (dtHL == null)
                {
                    dtHL = ConvertToDataTable(_test);
                }
                else
                {
                    AddToDataTable(_test, Segment.HL);
                }
                oHL = null;
            }



        }

        private void CreateNMDatatable(Cls_277X212_NM _oNM)
        {
            Cls_277X212_NMs oNMs = new Cls_277X212_NMs();
            if (_oNM != null)
            {
                oNMs.Add(_oNM);
            }

            Cls_277X212_NM[] oNM = new Cls_277X212_NM[oNMs.Count];
            oNMs.CopyTo(oNM, 0);
            if (oNM != null)
            {
                var _test = (from r in oNM select r).ToList();
                if (dtNM == null)
                {
                    dtNM = ConvertToDataTable(_test);
                }
                else
                {
                    AddToDataTable(_test, Segment.NM);
                }
                oNM = null;
            }
        }

        private void CreatePERDatatable(Cls_277X212_PER _oPER)
        {
            Cls_277X212_PERs oPERs = new Cls_277X212_PERs();
            if (_oPER != null)
            {
                oPERs.Add(_oPER);
            }

            Cls_277X212_PER[] oPER = new Cls_277X212_PER[oPERs.Count];
            oPERs.CopyTo(oPER, 0);
            if (oPER != null)
            {
                var _test = (from r in oPER select r).ToList();
                if (dtPER == null)
                {
                    dtPER = ConvertToDataTable(_test);
                }
                else
                {
                    AddToDataTable(_test, Segment.PER);
                }
                oPER = null;
            }
        }

        private void CreateTRNDatatable(Cls_277X212_TRN _oTRN)
        {
            Cls_277X212_TRNs oTRNs = new Cls_277X212_TRNs();
            if (_oTRN != null)
            {
                oTRNs.Add(_oTRN);
            }

            Cls_277X212_TRN[] oTRN = new Cls_277X212_TRN[oTRNs.Count];
            oTRNs.CopyTo(oTRN, 0);
            if (oTRN != null)
            {
                var _test = (from r in oTRN select r).ToList();
                if (dtTRN == null)
                {
                    dtTRN = ConvertToDataTable(_test);
                }
                else
                {
                    AddToDataTable(_test, Segment.TRN);
                }
                oTRN = null;
            }

        }

        private void CreateSVCDatatable(Cls_277X212_SVC _oSVC)
        {
            Cls_277X212_SVCs oSVCs = new Cls_277X212_SVCs();
            if (_oSVC != null)
            {
                oSVCs.Add(_oSVC);
            }

            Cls_277X212_SVC[] oSVC = new Cls_277X212_SVC[oSVCs.Count];
            oSVCs.CopyTo(oSVC, 0);
            if (oSVC != null)
            {
                var _test = (from r in oSVC select r).ToList();
                if (dtSVC == null)
                {
                    dtSVC = ConvertToDataTable(_test);
                }
                else
                {
                    AddToDataTable(_test, Segment.SVC);
                }
                oSVC = null;
            }
        }

        private void CreateSTCDatatable(Cls_277X212_STC _oSTC)
        {
            Cls_277X212_STCs oSTCs = new Cls_277X212_STCs();
            if (_oSTC != null)
            {
                oSTCs.Add(_oSTC);
            }

            Cls_277X212_STC[] oSTC = new Cls_277X212_STC[oSTCs.Count];
            oSTCs.CopyTo(oSTC, 0);
            if (oSTC != null)
            {
                var _test = (from r in oSTC select r).ToList();
                if (dtSTC == null)
                {
                    dtSTC = ConvertToDataTable(_test);
                }
                else
                {
                    AddToDataTable(_test, Segment.STC);
                }
                oSTC = null;
            }
        }

        private void CreateREFDatatable(Cls_277X212_REF _oREF)
        {
            Cls_277X212_REFs oREFs = new Cls_277X212_REFs();
            if (_oREF != null)
            {
                oREFs.Add(_oREF);
            }

            Cls_277X212_REF[] oREF = new Cls_277X212_REF[oREFs.Count];
            oREFs.CopyTo(oREF, 0);
            if (oREF != null)
            {
                var _test = (from r in oREF select r).ToList();
                if (dtREF == null)
                {
                    dtREF = ConvertToDataTable(_test);
                }
                else
                {
                    AddToDataTable(_test, Segment.REF);
                }

                oREF = null;
            }
        }

        private void CreateDTPDatatable(Cls_277X212_DTP _oDTP)
        {
            Cls_277X212_DTPs oDTPs = new Cls_277X212_DTPs();
            if (_oDTP != null)
            {
                oDTPs.Add(_oDTP);
            }

            Cls_277X212_DTP[] oDTP = new Cls_277X212_DTP[oDTPs.Count];
            oDTPs.CopyTo(oDTP, 0);
            if (oDTP != null)
            {
                var _test = (from r in oDTP select r).ToList();
                if (dtDTP == null)
                {
                    dtDTP = ConvertToDataTable(_test);
                }
                else
                {
                    AddToDataTable(_test, Segment.DTP);
                }
                oDTP = null;
            }
        }

        private void RemoveUnwantedColumnFromERATables()
        {
            if (dtISA != null)
            {
                dtISA.Columns.Remove("ISA_GS");
                dtISA.Columns.Remove("ISA_ST");
            }

            if (dtST != null)
            {
                dtST.Columns.Remove("ST_BHT");
                dtST.Columns.Remove("ST_HL");
            }

            if (dtHL != null)
            {
                dtHL.Columns.Remove("HL_NM");
                dtHL.Columns.Remove("HL_PER");
                dtHL.Columns.Remove("HL_TRN");
            }

            if (dtTRN != null)
            {

                dtTRN.Columns.Remove("TRN_STC");
                dtTRN.Columns.Remove("TRN_REF");
                dtTRN.Columns.Remove("TRN_DTP");
                dtTRN.Columns.Remove("TRN_SVC");
            }

            if (dtSVC != null)
            {
                dtSVC.Columns.Remove("SVC_STC");
                dtSVC.Columns.Remove("SVC_REF");
                dtSVC.Columns.Remove("SVC_DTP");
            }
        }

      
        DataTable ConvertToDataTable<TSource>(IEnumerable<TSource> source)
        {
            var props = typeof(TSource).GetProperties();

            var dt = new DataTable();
            dt.Columns.AddRange(
              props.Select(p => new DataColumn(p.Name, p.PropertyType)).ToArray()
            );

            source.ToList().ForEach(
              i => dt.Rows.Add(props.Select(p => p.GetValue(i, null)).ToArray())
            );

            return dt;
        }

        void AddToDataTable<TSource>(IEnumerable<TSource> source, Segment eSegment)
        {
            var props = typeof(TSource).GetProperties();

            switch (eSegment)
            {
                //dtISA, dtGS, dtST, dtBHT, dtHL, dtNM, dtPER, dtTRN, dtSVC, dtSTC, dtREF, dtDTP
                case Segment.ISA:
                    source.ToList().ForEach(i => dtISA.Rows.Add(props.Select(p => p.GetValue(i, null)).ToArray()));
                    break;
                case Segment.GS:
                    source.ToList().ForEach(i => dtGS.Rows.Add(props.Select(p => p.GetValue(i, null)).ToArray()));
                    break;
                case Segment.ST:
                    source.ToList().ForEach(i => dtST.Rows.Add(props.Select(p => p.GetValue(i, null)).ToArray()));
                    break;
                case Segment.BHT:
                    source.ToList().ForEach(i => dtBHT.Rows.Add(props.Select(p => p.GetValue(i, null)).ToArray()));
                    break;
                case Segment.HL:
                    source.ToList().ForEach(i => dtHL.Rows.Add(props.Select(p => p.GetValue(i, null)).ToArray()));
                    break;
                case Segment.NM:
                    source.ToList().ForEach(i => dtNM.Rows.Add(props.Select(p => p.GetValue(i, null)).ToArray()));
                    break;
                case Segment.PER:
                    source.ToList().ForEach(i => dtPER.Rows.Add(props.Select(p => p.GetValue(i, null)).ToArray()));
                    break;
                case Segment.TRN:
                    source.ToList().ForEach(i => dtTRN.Rows.Add(props.Select(p => p.GetValue(i, null)).ToArray()));
                    break;
                case Segment.SVC:
                    source.ToList().ForEach(i => dtSVC.Rows.Add(props.Select(p => p.GetValue(i, null)).ToArray()));
                    break;
                case Segment.STC:
                    source.ToList().ForEach(i => dtSTC.Rows.Add(props.Select(p => p.GetValue(i, null)).ToArray()));
                    break;
                case Segment.REF:
                    source.ToList().ForEach(i => dtREF.Rows.Add(props.Select(p => p.GetValue(i, null)).ToArray()));
                    break;
                case Segment.DTP:
                    source.ToList().ForEach(i => dtDTP.Rows.Add(props.Select(p => p.GetValue(i, null)).ToArray()));
                    break;
            }
        }

        private void ShowException(string exceptionMessage)
        {
            try
            {
                MessageBox.Show(exceptionMessage, "277 Response Parse", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
            }
        }
    }
}
