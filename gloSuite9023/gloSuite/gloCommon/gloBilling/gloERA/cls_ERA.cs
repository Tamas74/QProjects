using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Edidev.FrameworkEDI;
using System.Windows.Forms;
using System.Collections;
using System.Data;
using System.Linq;
using System.Data.SqlClient;


namespace gloBilling.gloERA
{

    #region " Enumerations "

    public enum enumERAFileStatus
    {
        None = 0,
        UnProcessed = 1,
        Processed = 2,
        Finished = 3
    }

    public enum enumCheckStatus
    {
        None = 0,
        Ready = 1,
        Posted = 2,
        MarkedDeleted = 3,
        InProcess = 4,
        Hold = 5
    }

    public enum enumReportType
    {
        None = 0,
        PostingReport = 1
    }

    public enum enumReportFormat
    {
        None = 0,
        RTF = 1,
        PDF = 2
    }

    public enum enumNoteType
    {
        None = 0,
        Check = 1
    }

    public enum enumREF_Type
    {
        None = 0,
        ReceiverID = 1,
        VersionID = 2,
        AdditionalPayerID = 3,
        AdditionalPayeeID = 4,
        OtherClaimRelatedID = 5,
        RenderingProviderID = 6,
        ServiceID = 7,
        RenderingProviderInfo = 8,
    }

    public enum enumDTM_Type
    {
        None = 0,
        ProductionDate = 1,
        ClaimDate = 2,
        ServiceDate = 3,
    }

    public enum enumAMT_Type
    {
        None = 0,
        ClaimSupplementalInfo = 1,
        ServiceSupplementalAmount = 2
    }

    public enum enumNM1_Type
    {
        None = 0,
        PatientName = 1,
        InsuredName = 2,
        CorrectedPatientOrInsuredName = 3,
        ServiceProviderName = 4,
        CrossoverCarrierName = 5,
        CorrectedPriorityPayerName = 6
    }

    public enum enumCAS_Type
    {
        None = 0,
        ClaimAdjustment = 1,
        ServiceAdjustment = 2
    }

    public enum enumPER_Type
    {
        None = 0,
        PayerContactInfo = 1,
        ClaimContactInfo = 2,
        PayerTechnicalInfo =3,
        PayerWebSiteInfo =4,
    }

    public enum enumPayerPayee_Type
    {
        None = 0,
        Payer = 1,
        Payee = 2
    }
    #endregion

    #region " ID SET "
    class IDSet
    {
        #region " Constructor & Destructor "

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

        ~IDSet()
        {
            Dispose(false);
        }

        #endregion

        #region " Private Variables "

        private Int64 nERAFileID;
        private Int64 nISAID;
        private Int64 nBPRID;
        private Int64 nCLPID;
        private Int64 nSVCID;
        private Int64 nPayerPayeeID;

        #endregion

        #region " Public Properties "

        public Int64 ERAFileID
        {
            get { return nERAFileID; }
            set { nERAFileID = value; }
        }
        public Int64 ISAID
        {
            get { return nISAID; }
            set { nISAID = value; }
        }
        public Int64 BPRID
        {
            get { return nBPRID; }
            set { nBPRID = value; }
        }
        public Int64 CLPID
        {
            get { return nCLPID; }
            set { nCLPID = value; }
        }
        public Int64 SVCID
        {
            get { return nSVCID; }
            set { nSVCID = value; }
        }
        public Int64 PayerPayeeID
        {
            get { return nPayerPayeeID; }
            set { nPayerPayeeID = value; }
        }

        #endregion
    }
    #endregion

    #region " gloERA "

    public class gloERA : IDisposable
    {
        #region " Constructor & Destructor "

        private bool disposed = false;

        public gloERA()
        {
            _ClinicID = gloGlobal.gloPMGlobal.ClinicID;
            _DataBaseConnectionString = gloGlobal.gloPMGlobal.DatabaseConnectionString;
            _UserID = gloGlobal.gloPMGlobal.UserID;
            _MessageBoxCaption = gloGlobal.gloPMGlobal.MessageBoxCaption; 

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
                    if (oDB != null) { oDB.Dispose(); oDB = null; }
                    if (oDBPara != null) { oDBPara.Dispose(); oDBPara = null; }

                    if (EdiDoc != null) { EdiDoc.Dispose(); EdiDoc = null; }
                    if (oSchemas != null) { oSchemas.Dispose(); oSchemas = null; }
                    if (oSchema != null) { oSchema.Dispose(); oSchema = null; }
                    if (oAck != null) { oAck.Dispose(); oAck = null; }
                    if (oSegment != null) { oSegment.Dispose(); oSegment = null; }

                }
            }
            disposed = true;
        }

        ~gloERA()
        {
            Dispose(false);
        }

        #endregion

        #region " Variable Declarations "

        
        private string _DataBaseConnectionString;
        private Int64 _ClinicID = 1;
        private Int64 _UserID;
        private string _MessageBoxCaption;

        private gloDatabaseLayer.DBLayer oDB;
        private gloDatabaseLayer.DBParameters oDBPara;
        string _TempStr;

        ediDocument EdiDoc = null;
        ediSchemas oSchemas = null;
        ediSchema oSchema = null;
        ediAcknowledgment oAck = null;
        ediDataSegment oSegment = null;
        Int64 _IDcount = 0;
        DataTable dtUniqueIDs = null;
        int nUniqueIDsIndex = 0;

        DataTable dtISA, dtBPR,dtTRN,dtREF,dtDTM,dtPLB,dtpayer,dtCLP,dtCAS,dtNM1,dtMIA,dtMOA,dtPER,dtAMT,dtSVC,dtLQ,dtRDM = null;
        string _FileVersion = "";
        #endregion

        #region " Public Methods "

        public Int64 SaveERAFile(ERAFile oERAFile)
        {
            Object oResult;
            Int64 _Result = 0;
            try
            {

                if (OpenConnection(true))
                {
                    oDBPara.Clear();
                    oDBPara.Add("@nFileID", oERAFile.FileID, ParameterDirection.InputOutput, SqlDbType.BigInt);
                    oDBPara.Add("@sFileName", oERAFile.FileName, ParameterDirection.Input, SqlDbType.VarChar);
                    oDBPara.Add("@nSplitCount", oERAFile.SplitCount, ParameterDirection.Input, SqlDbType.BigInt);
                    oDBPara.Add("@sOriginalFileName", oERAFile.OriginalFileName, ParameterDirection.Input, SqlDbType.VarChar);
                    oDBPara.Add("@iBinaryFile", Supporting.ConvertFileToBinary(oERAFile.FilePath), ParameterDirection.Input, SqlDbType.Image);
                    oDBPara.Add("@dtImportDate", DateTime.Now, ParameterDirection.Input, SqlDbType.DateTime);
                    oDBPara.Add("@nStatus", enumERAFileStatus.UnProcessed.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                    oDBPara.Add("@bBlocked", false, ParameterDirection.Input, SqlDbType.Bit);
                    oDBPara.Add("@nUserID", _UserID, ParameterDirection.Input, SqlDbType.BigInt);
                    oDBPara.Add("@nClinicID", _ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                    oDBPara.Add("@sFileVersion", oERAFile.FileVersion, ParameterDirection.Input, SqlDbType.VarChar);

                    oDB.Execute("ERA_IN_File", oDBPara, out oResult);
                    if (oResult != null && oResult.ToString() != "")
                        _Result = Convert.ToInt64(oResult);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                oResult = null;

                CloseConnection();

                if (oERAFile != null)
                {
                    oERAFile.Dispose();
                    oERAFile = null;
                }
            }
            return _Result;
        }

        public bool DeleteParsedData(Int64 nFileID)
        {
            try
            {
                if (OpenConnection(true))
                {
                    oDBPara.Clear();
                    oDBPara.Add("@nERAFileID", nFileID, ParameterDirection.Input, SqlDbType.BigInt);
                    oDB.Execute("ERA_DeleteParsedData", oDBPara);
                    CloseConnection();
                    SetFileStatus(nFileID, enumERAFileStatus.UnProcessed);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return false;
            }
            return true;
        }

        public string ImportERAFiles(gloGeneralItem.gloItems oFileNames, Int64 _FileID, string _SystemERAFileName, ref ProgressBar oProgress, ref Label oLabel)
        {
            ISAs oISAs = null;
            ERAFile oERAFile = null;
            ArrayList _arr = new ArrayList();
            string _Temp = "";
            bool _Conflict = false;
            string _ReturnMessage = "";
           
            string _ErrorFileSave = "";
            string _ErrorParse = "";
            string _FileNameAutoGenerated = "";
            string _FileName;
            //gloAuditTrail.gloAuditTrail.ExceptionLog("ImportERAFiles start", false);
            oProgress.Maximum = oFileNames.Count * 4;
            oProgress.Value = 0;
            oLabel.Text = "";


            if (oFileNames != null && oFileNames.Count > 0)
            {
                
                //FileStream oFileRead = null;
                LoadEDISchema();
                #region "Process ERA File"

                for (int iFile = 0; iFile < oFileNames.Count; iFile++)
                {
                    //StreamReader oReader = null;
                    //try
                    //{
                    //    File.SetAttributes(oFileNames[iFile].Description, FileAttributes.Normal);
                    //    oFileRead = new FileStream(oFileNames[iFile].Description, FileMode.Open);
                    //    oReader = new StreamReader(oFileRead);
                    //    string[] _FileData = oReader.ReadToEnd().ToString().Split('~');
                    //    if (_FileData.Length >= 2)
                    //    {
                    //        string _sFileVersion = _FileData[1].Split('*')[8];
                    //        {
                    //            if (_sFileVersion.Contains("5010"))
                    //            {
                    //                _FileVersion = "5010";
                    //            }
                    //            else if (_sFileVersion.Contains("4010"))
                    //            {
                    //                _FileVersion = "4010";
                    //            }

                    //        }

                    //    }

                    //}
                    //catch (Exception ex)
                    //{
                    //    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                    //}
                    //finally
                    //{

                    //    if (oFileRead != null)
                    //    {
                    //        oFileRead.Close();
                    //        oFileRead.Dispose();
                    //    }
                    //    if (oReader != null)
                    //    {
                    //        oReader.Close();
                    //        oReader.Dispose();
                    //    }
                    //}
                    if (_SystemERAFileName == "")
                    {
                        if (oFileNames[iFile].Code == "")
                            continue;

                        _FileName = Path.GetFileName(oFileNames[iFile].Code);
                    }
                    else
                        _FileName = _SystemERAFileName;

                    #region " VALIDATE DUPLICATE ERA FILE "

                    RefreshProgress(ref oProgress, ref oLabel, "Validating " + _FileName + "   ");

                    #region " FIND IN DATABASE "

                    _Conflict = false; // Reset conflict flag for this file //

                    _Temp = IsDuplicateFile(_FileName);
                    if (_Temp != "")
                        _Conflict = true;

                    if (_Conflict)
                    {
                        _ReturnMessage = _ReturnMessage + DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt") + "   ERA file " + _FileName + " may have already been imported." + Environment.NewLine;

                        if (MessageBox.Show("ERA file " + _FileName + " may have already been imported.\n" +
                            "\nDo you want to continue processing file?",
                            _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                            == DialogResult.No)
                        {
                            if (_SystemERAFileName == "")
                                if (MessageBox.Show("Do you want to delete ERA file " + _FileName + "?", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                    Supporting.DeleteFile(oFileNames[iFile].Code);
                            continue;
                        }
                    }

                    #endregion

                    #endregion

                    #region " READ ERA FILE "
                    RefreshProgress(ref oProgress, ref oLabel, "Reading " + _FileName + "   ");

                    //gloAuditTrail.gloAuditTrail.ExceptionLog("ReadRemittances  start", false);
                    oISAs = ReadRemittances(oFileNames[iFile].Description);
                    //gloAuditTrail.gloAuditTrail.ExceptionLog("ReadRemittances  end", false);
                    #endregion

                    #region " SAVE ERA FILE "

                    RefreshProgress(ref oProgress, ref oLabel, "Saving " + _FileName + "   ");

                    // IF FILE IMPORTED FROM USER'S DISK THEN SAVE IN DATABASE //
                    if (_SystemERAFileName == "")
                    {
                        if (oFileNames[iFile].ID <= 1)
                            _FileNameAutoGenerated = GenerateNewFileName();


                        oERAFile = new ERAFile();
                        oERAFile.FileName = _FileNameAutoGenerated;
                        oERAFile.SplitCount = oFileNames[iFile].ID;
                        oERAFile.OriginalFileName = _FileName;
                        oERAFile.FilePath = oFileNames[iFile].Description;
                        oERAFile.FileVersion = _FileVersion;
                        _FileID = SaveERAFile(oERAFile);

                        _FileVersion = "";
                    }

                    #endregion

                    #region " COMMENTED : VALIDATE ISA09, ISA10, ISA13 FOR DUPLICATION  "

                    //RefreshProgress(ref oProgress, ref oLabel, "Validating " + _FileName + "   ");

                    //_Conflict = false; // Reset conflict flag for this file //

                    //if (oISAs != null)
                    //{

                    //    #region " FIRST FIND FOR MULTIPLE ISA IN SAME FILE "
                    //    _arr.Clear();
                    //    int j = 0;
                    //    string _ICDate = "";
                    //    string _ICTime = "";
                    //    if (oISAs.Count > 1)
                    //    {
                    //        for (j = 0; j < oISAs.Count; j++)
                    //        {
                    //            _ICDate = oISAs[j].ISA09_IntrChngDate;
                    //            _ICTime = oISAs[j].ISA10_IntrChngTime;

                    //            // IN IDEAL CONDITION PADDING WILL NOT REQUIRE, ITS JUST PRECAUTION TO HANDLE ERROR //
                    //            if (_ICDate.Length < 6)
                    //                _ICDate = _ICDate.PadLeft(6, '0');

                    //            if (_ICTime.Length < 4)
                    //                _ICTime = _ICTime.PadLeft(4, '0');

                    //            _Temp = _ICDate + _ICTime + oISAs[j].ISA13_IntrChngControlNo;
                    //            if (_arr.Contains(_Temp))
                    //            {
                    //                _Conflict = true;
                    //                break;
                    //            }
                    //            _arr.Add(_Temp);
                    //        }

                    //        if (_Conflict)
                    //        {
                    //            _ReturnMessage = _ReturnMessage + DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt") + "   ERA file " + _FileName + " might contain duplicate data." + Environment.NewLine; ;


                    //            if (MessageBox.Show("ERA file " + _FileName + " might contain duplicate data,\n" +
                    //                "Date : " + _ICDate.Substring(2, 2) + "/" + _ICDate.Substring(4, 2) + "/" + _ICDate.Substring(0, 2) +
                    //                ", Time : " + _ICTime.Substring(0, 2) + ":" + _ICTime.Substring(2, 2) +
                    //                ", Control Number : " + oISAs[j].ISA13_IntrChngControlNo +
                    //                " are same in this ERA File.\n\nDo you want to continue processing file?",
                    //                _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    //                == DialogResult.No)
                    //            {
                    //                // USER MIGHT WANT TO REVIEW THIS FILE AND IMPORT IT AGAIN //
                    //                //if (MessageBox.Show("Do you want to delete ERA file " + _FileName + "?", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    //                //    Supporting.DeleteFile(sFileNames[iFile]);
                    //                if (_SystemERAFileName == "")
                    //                    MessageBox.Show("Click on ERA Files button to View/Delete/Process ERA File " + _FileName + ".", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //                continue;
                    //            }
                    //        }
                    //    }
                    //    #endregion

                    //    #region " FIND IN DATABASE "
                    //    if (!_Conflict) // if already conflict found above, then don't ask for further confirmation //
                    //    {
                    //        for (j = 0; j < oISAs.Count; j++)
                    //        {
                    //            _ICDate = oISAs[j].ISA09_IntrChngDate;
                    //            _ICTime = oISAs[j].ISA10_IntrChngTime;

                    //            _Temp = IsDuplicateISA(_ICDate, _ICTime, oISAs[j].ISA13_IntrChngControlNo);
                    //            if (_Temp != "")
                    //            {
                    //                _Conflict = true;
                    //                break;
                    //            }
                    //        }

                    //        if (_Conflict)
                    //        {

                    //            _ReturnMessage = _ReturnMessage + DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt") + "   ERA file " + _FileName + " might be a duplicate file to ERA file " + _Temp + "." + Environment.NewLine;

                    //            // IN IDEAL CONDITION PADDING WILL NOT REQUIRE, ITS JUST PRECAUTION TO HANDLE ERROR //
                    //            if (_ICDate.Length < 6)
                    //                _ICDate = _ICDate.PadLeft(6, '0');

                    //            if (_ICTime.Length < 4)
                    //                _ICTime = _ICTime.PadLeft(4, '0');

                    //            if (MessageBox.Show("ERA file " + _FileName + " might be a duplicate file to ERA file " + _Temp + ",\n" +
                    //                "Date : " + _ICDate.Substring(2, 2) + "/" + _ICDate.Substring(4, 2) + "/" + _ICDate.Substring(0, 2) +
                    //                ", Time : " + _ICTime.Substring(0, 2) + ":" + _ICTime.Substring(2, 2) +
                    //                ", Control Number : " + oISAs[j].ISA13_IntrChngControlNo +
                    //                " are same for both ERA Files.\n\nDo you want to continue processing file?",
                    //                _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    //                == DialogResult.No)
                    //            {
                    //                // USER MIGHT WANT TO REVIEW DUPLICATE FILE //
                    //                //if (MessageBox.Show("Do you want to delete ERA file " + _FileName + "?", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    //                //    Supporting.DeleteFile(sFileNames[iFile]);
                    //                if (_SystemERAFileName == "")
                    //                    MessageBox.Show("Click on ERA Files button to View/Delete/Process ERA File " + _FileName + ".", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //                continue;
                    //            }
                    //        }
                    //    }
                    //    #endregion

                    //}

                    #endregion

                    #region " SAVE PARSED DATA "

                    RefreshProgress(ref oProgress, ref oLabel, "Parsing " + _FileName + "   ");

                    if (_FileID > 0 && oISAs!=null)
                    {
                        if (SaveParsedFile(oISAs, _FileID))
                        {
                            #region " SET FILE STATUS TO PROCESSED "
                            SetFileStatus(_FileID, enumERAFileStatus.Processed);
                            #endregion
                        }
                        else
                        {
                            #region " DELETE PARTIALLY SAVED RECORDS "
                            DeleteParsedData(_FileID);
                            #endregion

                            #region " CREATE WARNING MESSAGE FOR PARSING ERROR IF ANY "
                            if (_ErrorParse.Contains(_FileName) == false)
                            {
                                if (_ErrorParse == "")
                                    _ErrorParse = _FileName;
                                else
                                    _ErrorParse = _ErrorParse + ", " + _FileName;
                            }
                            #endregion
                        }
                    }
                    else
                    {
                        #region " CREATE WARNING MESSAGE FOR FILE SAVING ERROR IF ANY "
                        if (_ErrorFileSave == "")
                            _ErrorFileSave = _FileName;
                        else
                            _ErrorFileSave = _ErrorFileSave + ", " + _FileName;
                        #endregion
                    }
                    Application.DoEvents();
                    #endregion

                }
                #endregion "Process ERA File"

                #region " Dispose Objects "
                if (oISAs != null) { oISAs.Dispose(); oISAs = null; }
                if (oERAFile != null) { oERAFile.Dispose(); oERAFile = null; }
                if (_arr != null) { _arr.Clear(); _arr = null; }
                //if (_5010Files != null) { _5010Files.Dispose(); _5010Files = null; }
                //if (_4010Files != null) { _4010Files.Dispose(); _4010Files = null; }

                if (EdiDoc != null) { EdiDoc.Dispose(); EdiDoc = null; }
                if (oSegment != null) { oSegment.Dispose(); oSegment = null; }
                if (oAck != null) { oAck.Dispose(); oAck = null; }
                if (oSchemas != null) { oSchemas.Dispose(); oSchemas = null; }
                if (oSchema != null) { oSchema.Dispose(); oSchema = null; }

                #endregion   

                #region 
                int Counter = 0;
                string _BASE_FOLDER = "Claim Management";
                string _INBOX_FOLDER = "InBox";
                string _CLAIM_FOLDER = "835 Remittance Advice";
                string _ARCHIVE = "835 Remittance Advice Archive";
                string NewArchivePath = null;
                string FileName = null;
                string _ServerPath = GetServerPath();
                if (_FileID > 0)
                {                    
                    for (int iFile = 0; iFile < oFileNames.Count; iFile++)
                    {
                        //        Previous Code
                        //        if (File.Exists(oFileNames[iFile].Code))
                        //        Supporting.DeleteFile(oFileNames[iFile].Code);
                        FileName = string.Empty;
                        NewArchivePath = string.Empty;
                        if (File.Exists(oFileNames[iFile].Code))                                //Check source File Exists or Not
                        {
                            try
                            {                                
                                string ArchivePath = _ServerPath + "\\" + _BASE_FOLDER + "\\" + _INBOX_FOLDER + "\\" + _ARCHIVE;
                                FileName = Path.GetFileName(oFileNames[iFile].Code);
                                if (!Directory.Exists(ArchivePath))                             //Create the New Archive Folder if Not Exists
                                {
                                    Directory.CreateDirectory(ArchivePath);
                                }
                                string date = DateTime.Now.ToString("yyyy-MM-dd");
                                string DestPath = ArchivePath + "\\" + date;
                                if (!Directory.Exists(DestPath))                                //Create the Todays Date Folder in Archive if Not Exists            
                                {
                                    Directory.CreateDirectory(DestPath);
                                }
                                DestPath = Path.Combine(DestPath, FileName);
                                if (File.Exists(DestPath))                                      //Check the File Exists at Destination Folder
                                {
                                    NewArchivePath = string.Empty;
                                    NewArchivePath = _ServerPath + "\\" + _BASE_FOLDER + "\\" + _INBOX_FOLDER + "\\" + _CLAIM_FOLDER; //Set Source Path 
                                    DestPath = string.Empty;
                                    DestPath = _ServerPath + "\\" + _BASE_FOLDER + "\\" + _INBOX_FOLDER + "\\" + _ARCHIVE + "\\" + date; //Set Destination Path

                                    do
                                    {
                                        string Fname;
                                        Counter++;
                                        Fname = Path.GetFileNameWithoutExtension(oFileNames[iFile].Code) + "-Copy" + Counter + Path.GetExtension(oFileNames[iFile].Code);
                                        if (Counter > 1)                                        //If Once the Copy File is Created Once Before
                                        {
                                            DestPath = string.Empty;
                                            DestPath = _ServerPath + "\\" + _BASE_FOLDER + "\\" + _INBOX_FOLDER + "\\" + _ARCHIVE + "\\" + date;
                                            DestPath = Path.Combine(DestPath, Fname);

                                            NewArchivePath = string.Empty;
                                            NewArchivePath = _ServerPath + "\\" + _BASE_FOLDER + "\\" + _INBOX_FOLDER + "\\" + _CLAIM_FOLDER;
                                            NewArchivePath = Path.Combine(NewArchivePath, Fname);
                                        }
                                        else                                                    //Set Path to create Copy File for First Time
                                        {
                                            DestPath = Path.Combine(DestPath, Fname);
                                            NewArchivePath = Path.Combine(NewArchivePath, Fname);
                                        }
                                    } while (File.Exists(DestPath));                            //Check if the Copy File is present at Destination ie Archive Folder
                                    File.Copy(oFileNames[iFile].Code, NewArchivePath, true);    //Copy File Created
                                    File.Delete(oFileNames[iFile].Code);                        //Deleted the Original File
                                    File.Move(NewArchivePath, DestPath);                        //Moved the Copy File 
                                }
                                else
                                    File.Move(oFileNames[iFile].Code, DestPath);                //Move the Folder if File is not Present at all
                            }
                            catch (Exception ex)
                            {
                                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
                            }
                        }
                    }
                }
                #endregion

            }
            #region "commented code"
            
            //gloGeneralItem.gloItems _5010Files=new gloGeneralItem.gloItems();
            //gloGeneralItem.gloItems _4010Files = new gloGeneralItem.gloItems();
            //_5010Files = oFileNames;
            //for (int iFile = 0; iFile < oFileNames.Count; iFile++)
            //{

            //    FileStream oFileRead = null;
            //    StreamReader oReader = null;
            //    try
            //    {
            //        File.SetAttributes(oFileNames[iFile].Description, FileAttributes.Normal);
            //        oFileRead = new FileStream(oFileNames[iFile].Description, FileMode.Open);
            //        oReader = new StreamReader(oFileRead);
            //        string[] _FileData = oReader.ReadToEnd().ToString().Split('~');
            //        if (_FileData.Length >= 2)
            //        {
            //            string _sFileVersion = _FileData[1].Split('*')[8];
            //            // if (_FileData[2].Split('*')[0] = "GS")
            //            {
            //                if (_sFileVersion.Contains("5010"))
            //                {
            //                    _5010Files.Add(oFileNames[iFile]);
            //                }
            //                else if (_sFileVersion.Contains("4010"))
            //                {
            //                    _4010Files.Add(oFileNames[iFile]);
            //                }

            //            }

            //        }

            //    }
            //    catch(Exception ex)
            //    {
            //        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            //    }
            //    finally
            //    {

            //        if (oFileRead != null)
            //        {
            //            oFileRead.Close();
            //            oFileRead.Dispose();
            //        }
            //        if (oReader != null)
            //        {
            //            oReader.Close();
            //            oReader.Dispose();
            //        }
            //    }
            //}
            //if (_4010Files != null && _4010Files.Count > 0)
            //{
            //    string _FileVersion = "4010";
            //    LoadEDISchema(_FileVersion);
            //    #region "Process ERA File"

            //    for (int iFile = 0; iFile < _4010Files.Count; iFile++)
            //    {

            //        if (_SystemERAFileName == "")
            //        {
            //            if (_4010Files[iFile].Code == "")
            //                continue;

            //            _FileName = Path.GetFileName(_4010Files[iFile].Code);
            //        }
            //        else
            //            _FileName = _SystemERAFileName;

            //        #region " VALIDATE DUPLICATE ERA FILE "

            //        RefreshProgress(ref oProgress, ref oLabel, "Validating " + _FileName + "   ");

            //        #region " FIND IN DATABASE "

            //        _Conflict = false; // Reset conflict flag for this file //

            //        _Temp = IsDuplicateFile(_FileName);
            //        if (_Temp != "")
            //            _Conflict = true;

            //        if (_Conflict)
            //        {
            //            _ReturnMessage = _ReturnMessage + DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt") + "   ERA file " + _FileName + " may have already been imported." + Environment.NewLine;

            //            if (MessageBox.Show("ERA file " + _FileName + " may have already been imported.\n" +
            //                "\nDo you want to continue processing file?",
            //                _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            //                == DialogResult.No)
            //            {
            //                if (_SystemERAFileName == "")
            //                    if (MessageBox.Show("Do you want to delete ERA file " + _FileName + "?", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            //                        Supporting.DeleteFile(_4010Files[iFile].Code);
            //                continue;
            //            }
            //        }

            //        #endregion

            //        #endregion

            //        #region " READ ERA FILE "
            //        RefreshProgress(ref oProgress, ref oLabel, "Reading " + _FileName + "   ");


            //        oISAs = ReadRemittances(_4010Files[iFile].Description);

            //        #endregion

            //        #region " SAVE ERA FILE "

            //        RefreshProgress(ref oProgress, ref oLabel, "Saving " + _FileName + "   ");

            //        // IF FILE IMPORTED FROM USER'S DISK THEN SAVE IN DATABASE //
            //        if (_SystemERAFileName == "")
            //        {
            //            if (_4010Files[iFile].ID <= 1)
            //                _FileNameAutoGenerated = GenerateNewFileName();


            //            oERAFile = new ERAFile();
            //            oERAFile.FileName = _FileNameAutoGenerated;
            //            oERAFile.SplitCount = _4010Files[iFile].ID;
            //            oERAFile.OriginalFileName = _FileName;
            //            oERAFile.FilePath = _4010Files[iFile].Description;
            //            oERAFile.FileVersion = _FileVersion;
            //            _FileID = SaveERAFile(oERAFile);

            //            // DELETE ERA FILE IMMEDIATELY AFTER SUCCESSFULLY STORED //
            //            if (_FileID > 0)
            //                if (File.Exists(_4010Files[iFile].Code))
            //                    Supporting.DeleteFile(_4010Files[iFile].Code);
            //        }

            //        #endregion

            //        #region " COMMENTED : VALIDATE ISA09, ISA10, ISA13 FOR DUPLICATION  "

            //        //RefreshProgress(ref oProgress, ref oLabel, "Validating " + _FileName + "   ");

            //        //_Conflict = false; // Reset conflict flag for this file //

            //        //if (oISAs != null)
            //        //{

            //        //    #region " FIRST FIND FOR MULTIPLE ISA IN SAME FILE "
            //        //    _arr.Clear();
            //        //    int j = 0;
            //        //    string _ICDate = "";
            //        //    string _ICTime = "";
            //        //    if (oISAs.Count > 1)
            //        //    {
            //        //        for (j = 0; j < oISAs.Count; j++)
            //        //        {
            //        //            _ICDate = oISAs[j].ISA09_IntrChngDate;
            //        //            _ICTime = oISAs[j].ISA10_IntrChngTime;

            //        //            // IN IDEAL CONDITION PADDING WILL NOT REQUIRE, ITS JUST PRECAUTION TO HANDLE ERROR //
            //        //            if (_ICDate.Length < 6)
            //        //                _ICDate = _ICDate.PadLeft(6, '0');

            //        //            if (_ICTime.Length < 4)
            //        //                _ICTime = _ICTime.PadLeft(4, '0');

            //        //            _Temp = _ICDate + _ICTime + oISAs[j].ISA13_IntrChngControlNo;
            //        //            if (_arr.Contains(_Temp))
            //        //            {
            //        //                _Conflict = true;
            //        //                break;
            //        //            }
            //        //            _arr.Add(_Temp);
            //        //        }

            //        //        if (_Conflict)
            //        //        {
            //        //            _ReturnMessage = _ReturnMessage + DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt") + "   ERA file " + _FileName + " might contain duplicate data." + Environment.NewLine; ;


            //        //            if (MessageBox.Show("ERA file " + _FileName + " might contain duplicate data,\n" +
            //        //                "Date : " + _ICDate.Substring(2, 2) + "/" + _ICDate.Substring(4, 2) + "/" + _ICDate.Substring(0, 2) +
            //        //                ", Time : " + _ICTime.Substring(0, 2) + ":" + _ICTime.Substring(2, 2) +
            //        //                ", Control Number : " + oISAs[j].ISA13_IntrChngControlNo +
            //        //                " are same in this ERA File.\n\nDo you want to continue processing file?",
            //        //                _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            //        //                == DialogResult.No)
            //        //            {
            //        //                // USER MIGHT WANT TO REVIEW THIS FILE AND IMPORT IT AGAIN //
            //        //                //if (MessageBox.Show("Do you want to delete ERA file " + _FileName + "?", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            //        //                //    Supporting.DeleteFile(sFileNames[iFile]);
            //        //                if (_SystemERAFileName == "")
            //        //                    MessageBox.Show("Click on ERA Files button to View/Delete/Process ERA File " + _FileName + ".", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        //                continue;
            //        //            }
            //        //        }
            //        //    }
            //        //    #endregion

            //        //    #region " FIND IN DATABASE "
            //        //    if (!_Conflict) // if already conflict found above, then don't ask for further confirmation //
            //        //    {
            //        //        for (j = 0; j < oISAs.Count; j++)
            //        //        {
            //        //            _ICDate = oISAs[j].ISA09_IntrChngDate;
            //        //            _ICTime = oISAs[j].ISA10_IntrChngTime;

            //        //            _Temp = IsDuplicateISA(_ICDate, _ICTime, oISAs[j].ISA13_IntrChngControlNo);
            //        //            if (_Temp != "")
            //        //            {
            //        //                _Conflict = true;
            //        //                break;
            //        //            }
            //        //        }

            //        //        if (_Conflict)
            //        //        {

            //        //            _ReturnMessage = _ReturnMessage + DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt") + "   ERA file " + _FileName + " might be a duplicate file to ERA file " + _Temp + "." + Environment.NewLine;

            //        //            // IN IDEAL CONDITION PADDING WILL NOT REQUIRE, ITS JUST PRECAUTION TO HANDLE ERROR //
            //        //            if (_ICDate.Length < 6)
            //        //                _ICDate = _ICDate.PadLeft(6, '0');

            //        //            if (_ICTime.Length < 4)
            //        //                _ICTime = _ICTime.PadLeft(4, '0');

            //        //            if (MessageBox.Show("ERA file " + _FileName + " might be a duplicate file to ERA file " + _Temp + ",\n" +
            //        //                "Date : " + _ICDate.Substring(2, 2) + "/" + _ICDate.Substring(4, 2) + "/" + _ICDate.Substring(0, 2) +
            //        //                ", Time : " + _ICTime.Substring(0, 2) + ":" + _ICTime.Substring(2, 2) +
            //        //                ", Control Number : " + oISAs[j].ISA13_IntrChngControlNo +
            //        //                " are same for both ERA Files.\n\nDo you want to continue processing file?",
            //        //                _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            //        //                == DialogResult.No)
            //        //            {
            //        //                // USER MIGHT WANT TO REVIEW DUPLICATE FILE //
            //        //                //if (MessageBox.Show("Do you want to delete ERA file " + _FileName + "?", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            //        //                //    Supporting.DeleteFile(sFileNames[iFile]);
            //        //                if (_SystemERAFileName == "")
            //        //                    MessageBox.Show("Click on ERA Files button to View/Delete/Process ERA File " + _FileName + ".", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        //                continue;
            //        //            }
            //        //        }
            //        //    }
            //        //    #endregion

            //        //}

            //        #endregion

            //        #region " SAVE PARSED DATA "

            //        RefreshProgress(ref oProgress, ref oLabel, "Parsing " + _FileName + "   ");

            //        if (_FileID > 0)
            //        {
            //            if (SaveParsedFile(oISAs, _FileID))
            //            {
            //                #region " SET FILE STATUS TO PROCESSED "
            //                SetFileStatus(_FileID, enumERAFileStatus.Processed);
            //                #endregion
            //            }
            //            else
            //            {
            //                #region " DELETE PARTIALLY SAVED RECORDS "
            //                DeleteParsedData(_FileID);
            //                #endregion

            //                #region " CREATE WARNING MESSAGE FOR PARSING ERROR IF ANY "
            //                if (_ErrorParse.Contains(_FileName) == false)
            //                {
            //                    if (_ErrorParse == "")
            //                        _ErrorParse = _FileName;
            //                    else
            //                        _ErrorParse = _ErrorParse + ", " + _FileName;
            //                }
            //                #endregion
            //            }
            //        }
            //        else
            //        {
            //            #region " CREATE WARNING MESSAGE FOR FILE SAVING ERROR IF ANY "
            //            if (_ErrorFileSave == "")
            //                _ErrorFileSave = _FileName;
            //            else
            //                _ErrorFileSave = _ErrorFileSave + ", " + _FileName;
            //            #endregion
            //        }

            //        #endregion
            //        Application.DoEvents();

            //    }
            //    #endregion "Process ERA File"
            //}
            #endregion "commented code"
            #region " SHOW WARNING MESSAGES IF ANY "
            if (_ErrorFileSave != "" || _ErrorParse != "")
            {
                string _Message = "";

                if (_ErrorFileSave != "")
                {
                    if (_ErrorFileSave.Contains(","))
                        _Message = _Message + "ERA files " + _ErrorFileSave + " was not imported.\nPlease import ERA files again.\n\n";
                    else
                        _Message = _Message + "ERA file " + _ErrorFileSave + " was not imported.\nPlease import ERA file again.\n\n";
                }

                if (_ErrorParse != "")
                {
                    if (_ErrorParse.Contains(","))
                        _Message = _Message + "ERA files " + _ErrorParse + " was unable to be processed. Please try to process ERA files again.";
                    else
                        _Message = _Message + "ERA file " + _ErrorParse + " was unable to be processed. Please try to process ERA file again.";

                    if (_SystemERAFileName == "")
                        _Message = _Message + "\nTo process the ERA file again, select the ERA Files button, find the ERA file and select the Process button.";
                }

                MessageBox.Show(_Message, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            #endregion
                        
            //gloAuditTrail.gloAuditTrail.ExceptionLog("ImportERAFiles end", false);
            return _ReturnMessage;
        }

        private string GetServerPath()
        {

            Object retVal = null;
            string _isValidPath = "";

            try
            {
                if (OpenConnection(false))
                {
                    _TempStr = "SELECT sSettingsValue FROM Settings WITH(NOLOCK) WHERE UPPER(sSettingsName) = 'SERVERPATH'";
                    retVal = oDB.ExecuteScalar_Query(_TempStr);
                    CloseConnection();

                    if (retVal != null && retVal != DBNull.Value)
                    {
                        _isValidPath = Convert.ToString(retVal);
                        if (System.IO.Directory.Exists(_isValidPath) == false)
                        { _isValidPath = ""; }
                    }
                    else
                    { _isValidPath = ""; }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                retVal = null;
            }
            return _isValidPath;
        }

        public bool SetFileStatus(Int64 nFileID, enumERAFileStatus eStatus)
        {
            try
            {
                if (OpenConnection(false))
                {
                    string _Query = "UPDATE ERA_Files SET nStatus = " + eStatus.GetHashCode() + " WHERE nERAFileID = " + nFileID + "";
                    oDB.Execute_Query(_Query);
                    CloseConnection();
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                DeleteParsedData(nFileID);
                return false;
            }
            return true;
        }

        public DataTable GetERAFile(Int64 nFileID)
        {
            DataTable dtFile = null;
            try
            {
                if (OpenConnection(false))
                {
                    _TempStr = "SELECT sFileName, sOriginalFileName, iBinaryFile, dtImportDate, nStatus  FROM ERA_FILES WHERE nERAFileID = " + nFileID;
                    oDB.Retrive_Query(_TempStr, out dtFile);
                    CloseConnection();
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            return dtFile;
        }

        public DataTable GetERANotes(enumNoteType _NoteType, Int64 nReferenceID)
        {
            DataTable _dt = null;
            try
            {
                if (OpenConnection(true))
                {
                    oDBPara.Clear();
                    oDBPara.Add("@nNoteType", _NoteType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                    oDBPara.Add("@nReferenceID", nReferenceID, ParameterDirection.Input, SqlDbType.BigInt);
                    oDBPara.Add("@nClinicID", _ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                    oDB.Retrive("ERA_GetNotes", oDBPara, out _dt);
                    CloseConnection();
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            return _dt;
        }

        public Int64 SaveERANote(Int64 nNoteID, enumNoteType _NoteType, Int64 nReferenceID, string sNoteDescription, DateTime _NoteDate)
        {
            Object oResult = null;
            Int64 _Result = 0;
            try
            {
                if (OpenConnection(true))
                {
                    oDBPara.Clear();
                    oDBPara.Add("@nNoteID", nNoteID, ParameterDirection.InputOutput, SqlDbType.BigInt);
                    oDBPara.Add("@nNoteType", _NoteType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                    oDBPara.Add("@nReferenceID", nReferenceID, ParameterDirection.Input, SqlDbType.BigInt);
                    oDBPara.Add("@sDescription", sNoteDescription, ParameterDirection.Input, SqlDbType.VarChar);
                    oDBPara.Add("@nNoteDate", gloDateMaster.gloDate.DateAsNumber(_NoteDate.ToString()), ParameterDirection.Input, SqlDbType.BigInt);
                    oDBPara.Add("@nUserId", _UserID, ParameterDirection.Input, SqlDbType.BigInt);
                    oDBPara.Add("@nClinicId", _ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                    oDB.Execute("ERA_INUP_NOTES", oDBPara, out oResult);
                    CloseConnection();
                    if (oResult != null && oResult.ToString() != "")
                    {
                       _Result = Convert.ToInt64(oResult);
                       if(nNoteID ==0)
                           gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Billing, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.Add, "ERA Note Added ", 0, nReferenceID, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
                       else
                           gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Billing, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.Add, "ERA Note Modified ", 0, nReferenceID, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
                    }
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            return _Result;
        }

        #endregion

        #region " Private Methods "

        #region " Open/Close Database Connection "

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
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
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

        #endregion

        #region " Save Methods for each Segment "
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

        //public class ISAClassReader : IDataReader
        //{
        //    private List<ISA> _objectList = null;
        //    public ISAClassReader(List<ISA> obj)
        //    {
        //        _objectList = obj;
        //    }

        //}
        //ISAClassReader reader = new ISAClassReader(_test);
        private void SaveISA(ISAs oISAs, Int64 _FileID)
        {
            //Object oResult;
            IDSet oIDSet = null;
            try
            {
                for (int i = 0; i < oISAs.Count; i++)
                {

                    oIDSet = new IDSet();
                    oIDSet.ERAFileID = _FileID;

                    #region " Save ISA "
                    
                    oISAs[i].ERAFileID = _FileID;
                    oISAs[i].ISAID = Convert.ToInt64(dtUniqueIDs.Rows[nUniqueIDsIndex][0]);
                    nUniqueIDsIndex++;

                    // CREATE ID SET //
                    oIDSet.ISAID = oISAs[i].ISAID;

                    // SAVE BPRs OF ISA // 
                    if (oISAs[i].oBPRs.Count > 0)
                        SaveBPR(oISAs[i].oBPRs, oIDSet);

                    //oDBPara.Clear();
                    //oDBPara.Add("@nISAID", 0, ParameterDirection.InputOutput, SqlDbType.BigInt);
                    //oDBPara.Add("@nERAFileID", _FileID, ParameterDirection.Input, SqlDbType.BigInt);
                    //oDBPara.Add("@sISA01_AuthorInfoQual", oISAs[i].ISA01_AuthorInfoQual, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sISA02_AuthorInfo", oISAs[i].ISA02_AuthorInfo, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sISA03_SecurityInfoQual", oISAs[i].ISA03_SecurityInfoQual, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sISA04_SecurityInfo", oISAs[i].ISA04_SecurityInfo, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sISA05_IntrChngIDQual", oISAs[i].ISA05_IntrChngIDQual, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sISA06_IntrChngSenderID", oISAs[i].ISA06_IntrChngSenderID, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sISA07_IntrChngIDQual", oISAs[i].ISA07_IntrChngIDQual, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sISA08_IntrChngReceiverID", oISAs[i].ISA08_IntrChngReceiverID, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sISA09_IntrChngDate", oISAs[i].ISA09_IntrChngDate, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sISA10_IntrChngTime", oISAs[i].ISA10_IntrChngTime, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sISA11_IntrChngControlStandardsID", oISAs[i].ISA11_IntrChngControlStandardsID, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sISA12_IntrChngControlVersionNo", oISAs[i].ISA12_IntrChngControlVersionNo, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sISA13_IntrChngControlNo", oISAs[i].ISA13_IntrChngControlNo, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sISA14_AckwRequested", oISAs[i].ISA14_AckwRequested, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sISA15_UsageIndicator", oISAs[i].ISA15_UsageIndicator, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sISA16_ComponentElementSeparator", oISAs[i].ISA16_ComponentElementSeparator, ParameterDirection.Input, SqlDbType.VarChar);

                    //oDB.Execute("ERA_IN_ISA", oDBPara, out oResult);
                    //if (oResult != null && oResult.ToString() != "")
                    //    oISAs[i].ISAID = Convert.ToInt64(oResult);

                    #endregion

                   
                }

                ISA[] oISA = new ISA[oISAs.Count];
                oISAs.CopyTo(oISA, 0);
                if (oISA != null)
                {
                    var _test = (from r in oISA select r).ToList();
                    dtISA = ConvertToDataTable(_test);
                    oISA = null;
                }


                
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                throw ex;
            }
            finally
            {
                //oResult = null;
                if (oIDSet != null)
                {
                    oIDSet.Dispose();
                    oIDSet = null;
                }
            }
        }

        private void SaveBPR(BPRs oBPRs, IDSet oIDSet)
        {
            //Object oResult;
            try
            {
                for (int i = 0; i < oBPRs.Count; i++)
                {

                    oIDSet.CLPID = 0;
                    oIDSet.PayerPayeeID = 0;
                    oIDSet.SVCID = 0;

                    #region " Save BPR "

                    oBPRs[i].ERAFileID = oIDSet.ERAFileID;
                    oBPRs[i].ISAID = oIDSet.ISAID;
                    oBPRs[i].BPRID = Convert.ToInt64(dtUniqueIDs.Rows[nUniqueIDsIndex][0]);
                    nUniqueIDsIndex++;

                    //oDBPara.Clear();
                    //oDBPara.Add("@nBPRID", 0, ParameterDirection.InputOutput, SqlDbType.BigInt);
                    //oDBPara.Add("@nERAFileID", oIDSet.ERAFileID, ParameterDirection.Input, SqlDbType.BigInt);
                    //oDBPara.Add("@nISAID", oIDSet.ISAID, ParameterDirection.Input, SqlDbType.BigInt);
                    //oDBPara.Add("@sBPR01_TransHandlingCode", oBPRs[i].BPR01_TransHandlingCode, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sBPR02_Amount", oBPRs[i].BPR02_Amount, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sBPR03_CreditDebitFlagCode", oBPRs[i].BPR03_CreditDebitFlagCode, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sBPR04_PaymentMethodCode", oBPRs[i].BPR04_PaymentMethodCode, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sBPR05_PaymentFormatCode", oBPRs[i].BPR05_PaymentFormatCode, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sBPR06_DFI_IDNoQual", oBPRs[i].BPR06_DFI_IDNoQual, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sBPR07_DFI_IDNo", oBPRs[i].BPR07_DFI_IDNo, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sBPR08_AccNoQual", oBPRs[i].BPR08_AccNoQual, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sBPR09_AccNo", oBPRs[i].BPR09_AccNo, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sBPR10_OriginatingCompanyID", oBPRs[i].BPR10_OriginatingCompanyID, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sBPR11_OriginatingCompanySuppCode", oBPRs[i].BPR11_OriginatingCompanySuppCode, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sBPR12_DFI_IDNoQual", oBPRs[i].BPR12_DFI_IDNoQual, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sBPR13_DFI_IDNo", oBPRs[i].BPR13_DFI_IDNo, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sBPR14_AccNoQual", oBPRs[i].BPR14_AccNoQual, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sBPR15_AccNo", oBPRs[i].BPR15_AccNo, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sBPR16_Date", oBPRs[i].BPR16_Date, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sBPR17_BusinessFunc_Code", oBPRs[i].BPR17_BusinessFunc_Code, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sBPR18_DFI_IDNoQual", oBPRs[i].BPR18_DFI_IDNoQual, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sBPR19_DFI_IDNo", oBPRs[i].BPR19_DFI_IDNo, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sBPR20_AccNoQual", oBPRs[i].BPR20_AccNoQual, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sBPR21_AccNo", oBPRs[i].BPR21_AccNo, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@nCheckStatus", enumCheckStatus.Ready.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);

                    //oDB.Execute("ERA_IN_BPR", oDBPara, out oResult);
                    //if (oResult != null && oResult.ToString() != "")
                    //    oBPRs[i].BPRID = Convert.ToInt64(oResult);

                    #endregion

                    // CREATE ID SET //
                    oIDSet.BPRID = oBPRs[i].BPRID;

                    // SAVE TRN //
                    if (oBPRs[i].oTRNs.Count > 0)
                        SaveTRN(oBPRs[i].oTRNs, oIDSet);

                    // SAVE REF //
                    if (oBPRs[i].oREFs.Count > 0)
                        SaveREF(oBPRs[i].oREFs, oIDSet);

                    // SAVE DTM //
                    if (oBPRs[i].oDTMs.Count > 0)
                        SaveDTM(oBPRs[i].oDTMs, oIDSet);

                    // SAVE PLB //
                    if (oBPRs[i].oPLBs.Count > 0)
                        SavePLB(oBPRs[i].oPLBs, oIDSet);

                    // SAVE PAYER PAYEE INFO //
                    if (oBPRs[i].oPayerPayeeIdents.Count > 0)
                        SavePayerPayeeIdent(oBPRs[i].oPayerPayeeIdents, oIDSet);

                    // SAVE CLP //
                    if (oBPRs[i].oCLPs.Count > 0)
                        SaveCLP(oBPRs[i].oCLPs, oIDSet);

                    if (oBPRs[i].oRDMs.Count > 0)
                        SaveRDM(oBPRs[i].oRDMs, oIDSet);
                }

                BPR[] oBPR = new BPR[oBPRs.Count];
                oBPRs.CopyTo(oBPR, 0);
                if (oBPR != null)
                {
                    var _test = (from r in oBPR select r).ToList();
                    DataTable dt = ConvertToDataTable(_test);

                    if (dtBPR != null && dtBPR.Rows.Count > 0)
                        dtBPR.Merge(dt);
                    else
                        dtBPR = dt;

                    if (dt != null) { dt.Dispose(); dt = null; }
                    oBPR = null;
                }

                
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                throw ex;
            }
        }

        private void SaveTRN(TRNs oTRNs, IDSet oIDSet)
        {
            try
            {
                for (int i = 0; i < oTRNs.Count; i++)
                {
                    #region " Save TRN "
                    oTRNs[i].ERAFileID=oIDSet.ERAFileID;
                    oTRNs[i].ISAID=oIDSet.ISAID;
                    oTRNs[i].BPRID = oIDSet.BPRID;
                    oTRNs[i].TRNID = Convert.ToInt64(dtUniqueIDs.Rows[nUniqueIDsIndex][0]);
                    nUniqueIDsIndex++;

                    //oDBPara.Clear();
                    //oDBPara.Add("@nTRNID", 0, ParameterDirection.InputOutput, SqlDbType.BigInt);
                    //oDBPara.Add("@nERAFileID", oIDSet.ERAFileID, ParameterDirection.Input, SqlDbType.BigInt);
                    //oDBPara.Add("@nISAID", oIDSet.ISAID, ParameterDirection.Input, SqlDbType.BigInt);
                    //oDBPara.Add("@nBPRID", oIDSet.BPRID, ParameterDirection.Input, SqlDbType.BigInt);
                    //oDBPara.Add("@sTRN01_TraceTypeCode", oTRNs[i].TRN01_TraceTypeCode, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sTRN02_Ref_ID", oTRNs[i].TRN02_Ref_ID, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sTRN03_OriginatingCompanyID", oTRNs[i].TRN03_OriginatingCompanyID, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sTRN04_Ref_ID", oTRNs[i].TRN04_Ref_ID, ParameterDirection.Input, SqlDbType.VarChar);

                    //oDB.Execute("ERA_IN_TRN", oDBPara);

                    #endregion
                }
                TRN[] oTRN = new TRN[oTRNs.Count];
                oTRNs.CopyTo(oTRN, 0);
                if (oTRN != null)
                {
                    var _test = (from r in oTRN select r).ToList();
                    DataTable dt = ConvertToDataTable(_test);

                    if (dtTRN != null && dtTRN.Rows.Count > 0)
                        dtTRN.Merge(dt);
                    else
                        dtTRN = dt;

                    if (dt != null) { dt.Dispose(); dt = null; }
                    oTRN = null;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                throw ex;
            }
        }

        private void SaveREF(REFs oREFs, IDSet oIDSet)
        {
            try
            {
                for (int i = 0; i < oREFs.Count; i++)
                {
                    #region " Save REF "

                    oREFs[i].ERAFileID = oIDSet.ERAFileID;
                    oREFs[i].ISAID = oIDSet.ISAID;
                    oREFs[i].BPRID = oIDSet.BPRID;
                    oREFs[i].PayerPayeeID = oIDSet.PayerPayeeID;
                    oREFs[i].CLPID = oIDSet.CLPID;
                    oREFs[i].SVCID = oIDSet.SVCID;
                    oREFs[i].REFID = Convert.ToInt64(dtUniqueIDs.Rows[nUniqueIDsIndex][0]);
                    nUniqueIDsIndex++;
                    
                    //oDBPara.Clear();
                    //oDBPara.Add("@nREFID", 0, ParameterDirection.InputOutput, SqlDbType.BigInt);
                    //oDBPara.Add("@nERAFileID", oIDSet.ERAFileID, ParameterDirection.Input, SqlDbType.BigInt);
                    //oDBPara.Add("@nISAID", oIDSet.ISAID, ParameterDirection.Input, SqlDbType.BigInt);
                    //oDBPara.Add("@nBPRID", oIDSet.BPRID, ParameterDirection.Input, SqlDbType.BigInt);
                    //oDBPara.Add("@nPayerPayeeID", oIDSet.PayerPayeeID, ParameterDirection.Input, SqlDbType.BigInt);
                    //oDBPara.Add("@nCLPID", oIDSet.CLPID, ParameterDirection.Input, SqlDbType.BigInt);
                    //oDBPara.Add("@nSVCID", oIDSet.SVCID, ParameterDirection.Input, SqlDbType.BigInt);
                    //oDBPara.Add("@sREF01_Ref_IDQual", oREFs[i].REF01_Ref_IDQual, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sREF02_Ref_ID", oREFs[i].REF02_Ref_ID, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sREF03_Desc", oREFs[i].REF03_Desc, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sREF04_Ref_ID", oREFs[i].REF04_Ref_ID, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@nREFType", oREFs[i].REFType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);

                    //oDB.Execute("ERA_IN_REF", oDBPara);

                    #endregion
                }
                REF[] oREF = new REF[oREFs.Count];
                oREFs.CopyTo(oREF, 0);
                if (oREF != null)
                {
                    var _test = (from r in oREF select r).ToList();
                    DataTable dt = ConvertToDataTable(_test);
                    if (dtREF != null && dtREF.Rows.Count > 0)
                        dtREF.Merge(dt);
                    else
                        dtREF = dt;

                    if (dt != null) { dt.Dispose(); dt = null; }
                    oREF = null;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                throw ex;
            }
        }

        private void SaveDTM(DTMs oDTMs, IDSet oIDSet)
        {
            try
            {
                for (int i = 0; i < oDTMs.Count; i++)
                {
                    #region " Save DTM "

                    oDTMs[i].ERAFileID = oIDSet.ERAFileID;
                    oDTMs[i].ISAID = oIDSet.ISAID;
                    oDTMs[i].BPRID = oIDSet.BPRID;
                    oDTMs[i].CLPID = oIDSet.CLPID;
                    oDTMs[i].SVCID = oIDSet.SVCID;
                    oDTMs[i].DTMID = Convert.ToInt64(dtUniqueIDs.Rows[nUniqueIDsIndex][0]);
                    nUniqueIDsIndex++;

                    //oDBPara.Clear();
                    //oDBPara.Add("@nDTMID", 0, ParameterDirection.InputOutput, SqlDbType.BigInt);
                    //oDBPara.Add("@nERAFileID", oIDSet.ERAFileID, ParameterDirection.Input, SqlDbType.BigInt);
                    //oDBPara.Add("@nISAID", oIDSet.ISAID, ParameterDirection.Input, SqlDbType.BigInt);
                    //oDBPara.Add("@nBPRID", oIDSet.BPRID, ParameterDirection.Input, SqlDbType.BigInt);
                    //oDBPara.Add("@nCLPID", oIDSet.CLPID, ParameterDirection.Input, SqlDbType.BigInt);
                    //oDBPara.Add("@nSVCID", oIDSet.SVCID, ParameterDirection.Input, SqlDbType.BigInt);
                    //oDBPara.Add("@sDTM01_DateTimeQual", oDTMs[i].DTM01_DateTimeQual, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sDTM02_Date", oDTMs[i].DTM02_Date, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sDTM03_Time", oDTMs[i].DTM03_Time, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sDTM04_TimeCode", oDTMs[i].DTM04_TimeCode, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sDTM05_DateTimePeriodFormatQual", oDTMs[i].DTM05_DateTimePeriodFormatQual, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sDTM06_DateTimePeriod", oDTMs[i].DTM06_DateTimePeriod, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@nDTMType", oDTMs[i].DTMType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);

                    //oDB.Execute("ERA_IN_DTM", oDBPara);

                    #endregion
                }
                DTM[] oDTM = new DTM[oDTMs.Count];
                oDTMs.CopyTo(oDTM, 0);
                if (oDTM != null)
                {
                    var _test = (from r in oDTM select r).ToList();
                    DataTable dt = ConvertToDataTable(_test);
                    if (dtDTM != null && dtDTM.Rows.Count > 0)
                        dtDTM.Merge(dt);
                    else
                        dtDTM = dt;

                    if (dt != null) { dt.Dispose(); dt = null; }
                    oDTM = null;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                throw ex;
            }
        }

        private void SaveCLP(CLPs oCLPs, IDSet oIDSet)
        {
       //     Object oResult;
            try
            {
                for (int i = 0; i < oCLPs.Count; i++)
                {

                    oIDSet.SVCID = 0;
                    oIDSet.PayerPayeeID = 0;

                    #region " Save CLP "

                    oCLPs[i].ERAFileID = oIDSet.ERAFileID;
                    oCLPs[i].ISAID = oIDSet.ISAID;
                    oCLPs[i].BPRID = oIDSet.BPRID;
                    oCLPs[i].CLPID = Convert.ToInt64(dtUniqueIDs.Rows[nUniqueIDsIndex][0]);
                    oCLPs[i].ClaimFileIndex = i + 1;
                    nUniqueIDsIndex++;

                    //oDBPara.Clear();
                    //oDBPara.Add("@nCLPID", 0, ParameterDirection.InputOutput, SqlDbType.BigInt);
                    //oDBPara.Add("@nERAFileID", oIDSet.ERAFileID, ParameterDirection.Input, SqlDbType.BigInt);
                    //oDBPara.Add("@nISAID", oIDSet.ISAID, ParameterDirection.Input, SqlDbType.BigInt);
                    //oDBPara.Add("@nBPRID", oIDSet.BPRID, ParameterDirection.Input, SqlDbType.BigInt);
                    //oDBPara.Add("@sCLP01_ClaimSubmitterID", oCLPs[i].CLP01_ClaimSubmitterID, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sCLP02_ClaimStatusCode", oCLPs[i].CLP02_ClaimStatusCode, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sCLP03_Amount", oCLPs[i].CLP03_Amount, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sCLP04_Amount", oCLPs[i].CLP04_Amount, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sCLP05_Amount", oCLPs[i].CLP05_Amount, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sCLP06_ClaimFilingIndicatorCode", oCLPs[i].CLP06_ClaimFilingIndicatorCode, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sCLP07_Ref_ID", oCLPs[i].CLP07_Ref_ID, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sCLP08_FacilityCodeValue", oCLPs[i].CLP08_FacilityCodeValue, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sCLP09_ClaimFrequencyTypeCode", oCLPs[i].CLP09_ClaimFrequencyTypeCode, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sCLP10_PatientStatusCode", oCLPs[i].CLP10_PatientStatusCode, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sCLP11_DiagRelatedGroupCode", oCLPs[i].CLP11_DiagRelatedGroupCode, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sCLP12_Qty", oCLPs[i].CLP12_Qty, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sCLP13_Percent", oCLPs[i].CLP13_Percent, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@nClaimFileIndex", i + 1, ParameterDirection.Input, SqlDbType.BigInt);

                    //oDB.Execute("ERA_IN_CLP", oDBPara, out oResult);
                    //if (oResult != null && oResult.ToString() != "")
                    //    oCLPs[i].CLPID = Convert.ToInt64(oResult);

                    #endregion

                    // SET ID SET //
                    oIDSet.CLPID = oCLPs[i].CLPID;

                    // SAVE CAS //
                    if (oCLPs[i].oCASs.Count > 0)
                        SaveCAS(oCLPs[i].oCASs, oIDSet);

                    // SAVE NM1 //
                    if (oCLPs[i].oNM1s.Count > 0)
                        SaveNM1(oCLPs[i].oNM1s, oIDSet);

                    // SAVE MIA //
                    if (oCLPs[i].oMIAs.Count > 0)
                        SaveMIA(oCLPs[i].oMIAs, oIDSet);

                    // SAVE MOA //
                    if (oCLPs[i].oMOAs.Count > 0)
                        SaveMOA(oCLPs[i].oMOAs, oIDSet);

                    // SAVE REF //
                    if (oCLPs[i].oREFs.Count > 0)
                        SaveREF(oCLPs[i].oREFs, oIDSet);

                    // SAVE DTM //
                    if (oCLPs[i].oDTMs.Count > 0)
                        SaveDTM(oCLPs[i].oDTMs, oIDSet);

                    // SAVE PER //
                    if (oCLPs[i].oPERs.Count > 0)
                        SavePER(oCLPs[i].oPERs, oIDSet);

                    // SAVE AMT //
                    if (oCLPs[i].oAMTs.Count > 0)
                        SaveAMT(oCLPs[i].oAMTs, oIDSet);

                    // SAVE SVC //
                    if (oCLPs[i].oSVCs.Count > 0)
                        SaveSVC(oCLPs[i].oSVCs, oIDSet);

                }
                CLP[] oCLP = new CLP[oCLPs.Count];
                oCLPs.CopyTo(oCLP, 0);
                if (oCLP != null)
                {
                    var _test = (from r in oCLP select r).ToList();
                    DataTable dt = ConvertToDataTable(_test);
                    if (dtCLP != null && dtCLP.Rows.Count > 0)
                        dtCLP.Merge(dt);
                    else
                        dtCLP = dt;
                    if (dt != null) { dt.Dispose(); dt = null; }
                    oCLP = null;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                throw ex;
            }
        }

        private void SavePLB(PLBs oPLBs, IDSet oIDSet)
        {
            try
            {
                for (int i = 0; i < oPLBs.Count; i++)
                {
                    #region " Save PLB "

                    oPLBs[i].ERAFileID = oIDSet.ERAFileID;
                    oPLBs[i].ISAID = oIDSet.ISAID;
                    oPLBs[i].BPRID = oIDSet.BPRID;
                    oPLBs[i].PLBID = Convert.ToInt64(dtUniqueIDs.Rows[nUniqueIDsIndex][0]);
                    nUniqueIDsIndex++;

                    //oDBPara.Clear();
                    //oDBPara.Add("@nPLBID", 0, ParameterDirection.InputOutput, SqlDbType.BigInt);
                    //oDBPara.Add("@nERAFileID", oIDSet.ERAFileID, ParameterDirection.Input, SqlDbType.BigInt);
                    //oDBPara.Add("@nISAID", oIDSet.ISAID, ParameterDirection.Input, SqlDbType.BigInt);
                    //oDBPara.Add("@nBPRID", oIDSet.BPRID, ParameterDirection.Input, SqlDbType.BigInt);
                    //oDBPara.Add("@sPLB01_Ref_ID", oPLBs[i].PLB01_Ref_ID, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sPLB02_Date", oPLBs[i].PLB02_Date, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sPLB03_AdjustID", oPLBs[i].PLB03_AdjustID, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sPLB03_1_AdjustReasonCode", oPLBs[i].PLB03_1_AdjustReasonCode, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sPLB03_2_Ref_ID", oPLBs[i].PLB03_2_Ref_ID, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sPLB04_Amount", oPLBs[i].PLB04_Amount, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sPLB05_AdjustID", oPLBs[i].PLB05_AdjustID, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sPLB05_1_AdjustReasonCode", oPLBs[i].PLB05_1_AdjustReasonCode, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sPLB05_2_Ref_ID", oPLBs[i].PLB05_2_Ref_ID, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sPLB06_Amount", oPLBs[i].PLB06_Amount, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sPLB07_AdjustID", oPLBs[i].PLB07_AdjustID, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sPLB07_1_AdjustReasonCode", oPLBs[i].PLB07_1_AdjustReasonCode, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sPLB07_2_Ref_ID", oPLBs[i].PLB07_2_Ref_ID, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sPLB08_Amount", oPLBs[i].PLB08_Amount, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sPLB09_AdjustID", oPLBs[i].PLB09_AdjustID, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sPLB09_1_AdjustReasonCode", oPLBs[i].PLB09_1_AdjustReasonCode, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sPLB09_2_Ref_ID", oPLBs[i].PLB09_2_Ref_ID, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sPLB10_Amount", oPLBs[i].PLB10_Amount, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sPLB11_AdjustID", oPLBs[i].PLB11_AdjustID, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sPLB11_1_AdjustReasonCode", oPLBs[i].PLB11_1_AdjustReasonCode, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sPLB11_2_Ref_ID", oPLBs[i].PLB11_2_Ref_ID, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sPLB12_Amount", oPLBs[i].PLB12_Amount, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sPLB13_AdjustID", oPLBs[i].PLB13_AdjustID, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sPLB13_1_AdjustReasonCode", oPLBs[i].PLB13_1_AdjustReasonCode, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sPLB13_2_Ref_ID", oPLBs[i].PLB13_2_Ref_ID, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sPLB14_Amount", oPLBs[i].PLB14_Amount, ParameterDirection.Input, SqlDbType.VarChar);

                    //oDB.Execute("ERA_IN_PLB", oDBPara);

                    #endregion
                }
                PLB[] oPLB = new PLB[oPLBs.Count];
                oPLBs.CopyTo(oPLB, 0);
                if (oPLB != null)
                {
                    var _test = (from r in oPLB select r).ToList();
                    DataTable dt = ConvertToDataTable(_test);
                    if (dtPLB != null && dtPLB.Rows.Count > 0)
                        dtPLB.Merge(dt);
                    else
                        dtPLB = dt;

                    if (dt != null) { dt.Dispose(); dt = null; }
                    oPLB = null;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                throw ex;
            }
        }

        private void SavePayerPayeeIdent(PayerPayeeIdents oPayerPayeeIdents, IDSet oIDSet)
        {
          //  Object oResult;
            try
            {
                for (int i = 0; i < oPayerPayeeIdents.Count; i++)
                {

                    oIDSet.PayerPayeeID = 0;

                    #region " Save PayerPayee "

                    oPayerPayeeIdents[i].ERAFileID = oIDSet.ERAFileID;
                    oPayerPayeeIdents[i].ISAID = oIDSet.ISAID;
                    oPayerPayeeIdents[i].BPRID = oIDSet.BPRID;
                    oPayerPayeeIdents[i].PayerPayeeID = Convert.ToInt64(dtUniqueIDs.Rows[nUniqueIDsIndex][0]);
                    nUniqueIDsIndex++;

                    //oDBPara.Clear();
                    //oDBPara.Add("@nPayerPayeeID", 0, ParameterDirection.InputOutput, SqlDbType.BigInt);
                    //oDBPara.Add("@nERAFileID", oIDSet.ERAFileID, ParameterDirection.Input, SqlDbType.BigInt);
                    //oDBPara.Add("@nISAID", oIDSet.ISAID, ParameterDirection.Input, SqlDbType.BigInt);
                    //oDBPara.Add("@nBPRID", oIDSet.BPRID, ParameterDirection.Input, SqlDbType.BigInt);
                    //oDBPara.Add("@sN101_EntityIDCode", oPayerPayeeIdents[i].N101_EntityIDCode, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sN102_Name", oPayerPayeeIdents[i].N102_Name, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sN103_IDCodeQual", oPayerPayeeIdents[i].N103_IDCodeQual, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sN104_IDCode", oPayerPayeeIdents[i].N104_IDCode, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sN105_EntityRelationCode", oPayerPayeeIdents[i].N105_EntityRelationCode, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sN106_EntityIDCode", oPayerPayeeIdents[i].N106_EntityIDCode, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sN301_AddrInfo", oPayerPayeeIdents[i].N301_AddrInfo, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sN302_AddrInfo", oPayerPayeeIdents[i].N302_AddrInfo, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sN401_CityName", oPayerPayeeIdents[i].N401_CityName, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sN402_StateOrProvinceCode", oPayerPayeeIdents[i].N402_StateOrProvinceCode, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sN403_PostalCode", oPayerPayeeIdents[i].N403_PostalCode, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sN404_CountryCode", oPayerPayeeIdents[i].N404_CountryCode, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sN405_LocationQual", oPayerPayeeIdents[i].N405_LocationQual, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sN406_LocationID", oPayerPayeeIdents[i].N406_LocationID, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sN407_CountrySubDivisionCode", oPayerPayeeIdents[i].N407_CountrySubDivisionCode, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@nPayerPayeeType", oPayerPayeeIdents[i].PayerPayeeType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);

                    //oDB.Execute("ERA_IN_PayerPayeeIdentification", oDBPara, out oResult);
                    //if (oResult != null && oResult.ToString() != "")
                    //    oPayerPayeeIdents[i].PayerPayeeID = Convert.ToInt64(oResult);

                    #endregion

                    // SET ID SET //
                    oIDSet.PayerPayeeID = oPayerPayeeIdents[i].PayerPayeeID;

                    // SAVE PAYER CONTACT INFO //
                    if (oPayerPayeeIdents[i].oPERs.Count > 0)
                        SavePER(oPayerPayeeIdents[i].oPERs, oIDSet);

                    // SAVE ADDITIONAL REF INFO //
                    if (oPayerPayeeIdents[i].oREFs.Count > 0)
                        SaveREF(oPayerPayeeIdents[i].oREFs, oIDSet);
                }
                PayerPayeeIdent[] oPayer = new PayerPayeeIdent[oPayerPayeeIdents.Count];
                oPayerPayeeIdents.CopyTo(oPayer, 0);
                
                if (oPayer != null)
                {
                    var _test = (from r in oPayer select r).ToList();
                    DataTable dt = ConvertToDataTable(_test);
                    if (dtpayer != null && dtpayer.Rows.Count > 0)
                        dtpayer.Merge(dt);
                    else
                        dtpayer = dt;

                    if (dt != null) { dt.Dispose(); dt = null; }
                    oPayer = null;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                throw ex;
            }
        }

        private void SaveCAS(CASs oCASs, IDSet oIDSet)
        {
            try
            {
                for (int i = 0; i < oCASs.Count; i++)
                {
                    #region " Save CAS "

                    oCASs[i].ERAFileID = oIDSet.ERAFileID;
                    oCASs[i].ISAID = oIDSet.ISAID;
                    oCASs[i].BPRID = oIDSet.BPRID;
                    oCASs[i].CLPID = oIDSet.CLPID;
                    oCASs[i].SVCID = oIDSet.SVCID;
                    oCASs[i].CASID = Convert.ToInt64(dtUniqueIDs.Rows[nUniqueIDsIndex][0]);
                    nUniqueIDsIndex++;

                    //oDBPara.Clear();
                    //oDBPara.Add("@nCASID", 0, ParameterDirection.InputOutput, SqlDbType.BigInt);
                    //oDBPara.Add("@nERAFileID", oIDSet.ERAFileID, ParameterDirection.Input, SqlDbType.BigInt);
                    //oDBPara.Add("@nISAID", oIDSet.ISAID, ParameterDirection.Input, SqlDbType.BigInt);
                    //oDBPara.Add("@nBPRID", oIDSet.BPRID, ParameterDirection.Input, SqlDbType.BigInt);
                    //oDBPara.Add("@nCLPID", oIDSet.CLPID, ParameterDirection.Input, SqlDbType.BigInt);
                    //oDBPara.Add("@nSVCID", oIDSet.SVCID, ParameterDirection.Input, SqlDbType.BigInt);
                    //oDBPara.Add("@sCAS01_ClaimAdjustGroupCode", oCASs[i].CAS01_ClaimAdjustGroupCode, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sCAS02_ClaimAdjustReasonCode", oCASs[i].CAS02_ClaimAdjustReasonCode, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sCAS03_Amount", oCASs[i].CAS03_Amount, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sCAS04_Qty", oCASs[i].CAS04_Qty, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sCAS05_ClaimAdjustReasonCode", oCASs[i].CAS05_ClaimAdjustReasonCode, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sCAS06_Amount", oCASs[i].CAS06_Amount, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sCAS07_Qty", oCASs[i].CAS07_Qty, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sCAS08_ClaimAdjustReasonCode", oCASs[i].CAS08_ClaimAdjustReasonCode, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sCAS09_Amount", oCASs[i].CAS09_Amount, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sCAS10_Qty", oCASs[i].CAS10_Qty, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sCAS11_ClaimAdjustReasonCode", oCASs[i].CAS11_ClaimAdjustReasonCode, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sCAS12_Amount", oCASs[i].CAS12_Amount, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sCAS13_Qty", oCASs[i].CAS13_Qty, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sCAS14_ClaimAdjustReasonCode", oCASs[i].CAS14_ClaimAdjustReasonCode, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sCAS15_Amount", oCASs[i].CAS15_Amount, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sCAS16_Qty", oCASs[i].CAS16_Qty, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sCAS17_ClaimAdjustReasonCode", oCASs[i].CAS17_ClaimAdjustReasonCode, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sCAS18_Amount", oCASs[i].CAS18_Amount, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sCAS19_Qty", oCASs[i].CAS19_Qty, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@nCASType", oCASs[i].CASType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);

                    //oDB.Execute("ERA_IN_CAS", oDBPara);

                    #endregion
                }
                CAS[] oCAS = new CAS[oCASs.Count];
                oCASs.CopyTo(oCAS, 0);
                if (oCAS != null)
                {
                    var _test = (from r in oCAS select r).ToList();
                    DataTable dt = ConvertToDataTable(_test);
                    if (dtCAS != null && dtCAS.Rows.Count > 0)
                        dtCAS.Merge(dt);
                    else
                        dtCAS = dt;
                    oCAS = null;
                    if (dt != null) { dt.Dispose(); dt = null; }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                throw ex;
            }
        }

        private void SaveNM1(NM1s oNM1s, IDSet oIDSet)
        {
            try
            {
                for (int i = 0; i < oNM1s.Count; i++)
                {
                    #region " Save NM1 "

                    oNM1s[i].ERAFileID = oIDSet.ERAFileID;
                    oNM1s[i].ISAID = oIDSet.ISAID;
                    oNM1s[i].BPRID = oIDSet.BPRID;
                    oNM1s[i].CLPID = oIDSet.CLPID;
                    oNM1s[i].NM1ID = Convert.ToInt64(dtUniqueIDs.Rows[nUniqueIDsIndex][0]);
                    nUniqueIDsIndex++;

                    //oDBPara.Clear();
                    //oDBPara.Add("@nNM1ID", 0, ParameterDirection.InputOutput, SqlDbType.BigInt);
                    //oDBPara.Add("@nERAFileID", oIDSet.ERAFileID, ParameterDirection.Input, SqlDbType.BigInt);
                    //oDBPara.Add("@nISAID", oIDSet.ISAID, ParameterDirection.Input, SqlDbType.BigInt);
                    //oDBPara.Add("@nBPRID", oIDSet.BPRID, ParameterDirection.Input, SqlDbType.BigInt);
                    //oDBPara.Add("@nCLPID", oIDSet.CLPID, ParameterDirection.Input, SqlDbType.BigInt);
                    //oDBPara.Add("@sNM101_EntityIDCode", oNM1s[i].NM101_EntityIDCode, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sNM102_EntityTypeQual", oNM1s[i].NM102_EntityTypeQual, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sNM103_NameLastOrOrgName", oNM1s[i].NM103_NameLastOrOrgName, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sNM104_NameFirst", oNM1s[i].NM104_NameFirst, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sNM105_NameMiddle", oNM1s[i].NM105_NameMiddle, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sNM106_NamePrefix", oNM1s[i].NM106_NamePrefix, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sNM107_NameSuffix", oNM1s[i].NM107_NameSuffix, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sNM108_IDCodeQual", oNM1s[i].NM108_IDCodeQual, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sNM109_IDCode", oNM1s[i].NM109_IDCode, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sNM110_EntityRelationCode", oNM1s[i].NM110_EntityRelationCode, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sNM111_EntityIDCode", oNM1s[i].NM111_EntityIDCode, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@nNM1Type", oNM1s[i].NM1Type.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);

                    //oDB.Execute("ERA_IN_NM1", oDBPara);

                    #endregion
                }
                NM1[] oNM1 = new NM1[oNM1s.Count];
                oNM1s.CopyTo(oNM1, 0);
                if (oNM1 != null)
                {
                    var _test = (from r in oNM1 select r).ToList();
                    DataTable dt = ConvertToDataTable(_test);
                    if (dtNM1 != null && dtNM1.Rows.Count > 0)
                        dtNM1.Merge(dt);
                    else
                        dtNM1 = dt;
                    if (dt != null) { dt.Dispose(); dt = null; }
                    oNM1 = null;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                throw ex;
            }
        }

        private void SaveSVC(SVCs oSVCs, IDSet oIDSet)
        {
            //Object oResult;
            try
            {
                for (int i = 0; i < oSVCs.Count; i++)
                {
                    #region " Save SVC "

                    oSVCs[i].ERAFileID = oIDSet.ERAFileID;
                    oSVCs[i].ISAID = oIDSet.ISAID;
                    oSVCs[i].BPRID = oIDSet.BPRID;
                    oSVCs[i].CLPID = oIDSet.CLPID;
                    oSVCs[i].SVCID = Convert.ToInt64(dtUniqueIDs.Rows[nUniqueIDsIndex][0]);
                    oSVCs[i].ChargeIndex = i + 1;
                    nUniqueIDsIndex++;

                    //oDBPara.Clear();
                    //oDBPara.Add("@nSVCID", 0, ParameterDirection.InputOutput, SqlDbType.BigInt);
                    //oDBPara.Add("@nERAFileID", oIDSet.ERAFileID, ParameterDirection.Input, SqlDbType.BigInt);
                    //oDBPara.Add("@nISAID", oIDSet.ISAID, ParameterDirection.Input, SqlDbType.BigInt);
                    //oDBPara.Add("@nBPRID", oIDSet.BPRID, ParameterDirection.Input, SqlDbType.BigInt);
                    //oDBPara.Add("@nCLPID", oIDSet.CLPID, ParameterDirection.Input, SqlDbType.BigInt);
                    //oDBPara.Add("@sSVC01_CompositeMedicalProc", oSVCs[i].SVC01_CompositeMedicalProc, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sSVC01_1_ProductServiceIDQual", oSVCs[i].SVC01_1_ProductServiceIDQual, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sSVC01_2_ProductServiceID", oSVCs[i].SVC01_2_ProductServiceID, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sSVC01_3_ProcMod", oSVCs[i].SVC01_3_ProcMod, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sSVC01_4_ProcMod", oSVCs[i].SVC01_4_ProcMod, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sSVC01_5_ProcMod", oSVCs[i].SVC01_5_ProcMod, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sSVC01_6_ProcMod", oSVCs[i].SVC01_6_ProcMod, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sSVC01_7_Desc", oSVCs[i].SVC01_7_Desc, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sSVC02_Amount", oSVCs[i].SVC02_Amount, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sSVC03_Amount", oSVCs[i].SVC03_Amount, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sSVC04_ProductServiceID", oSVCs[i].SVC04_ProductServiceID, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sSVC05_Qty", oSVCs[i].SVC05_Qty, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sSVC06_CompositeMedicalProc", oSVCs[i].SVC06_CompositeMedicalProc, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sSVC06_1_ProductServiceIDQual", oSVCs[i].SVC06_1_ProductServiceIDQual, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sSVC06_2_ProductServiceID", oSVCs[i].SVC06_2_ProductServiceID, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sSVC06_3_ProcMod", oSVCs[i].SVC06_3_ProcMod, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sSVC06_4_ProcMod", oSVCs[i].SVC06_4_ProcMod, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sSVC06_5_ProcMod", oSVCs[i].SVC06_5_ProcMod, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sSVC06_6_ProcMod", oSVCs[i].SVC06_6_ProcMod, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sSVC06_7_Desc", oSVCs[i].SVC06_7_Desc, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sSVC07_Qty", oSVCs[i].SVC07_Qty, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@nChargeIndex", i + 1, ParameterDirection.Input, SqlDbType.BigInt);

                    //oDB.Execute("ERA_IN_SVC", oDBPara, out oResult);
                    //if (oResult != null && oResult.ToString() != "")
                    //    oSVCs[i].SVCID = Convert.ToInt64(oResult);

                    #endregion

                    // SET ID SET //
                    oIDSet.SVCID = oSVCs[i].SVCID;

                    // SAVE CAS //
                    if (oSVCs[i].oCASs.Count > 0)
                        SaveCAS(oSVCs[i].oCASs, oIDSet);

                    // SAVE DTM //
                    if (oSVCs[i].oDTMs.Count > 0)
                        SaveDTM(oSVCs[i].oDTMs, oIDSet);

                    // SAVE REF //
                    if (oSVCs[i].oREFs.Count > 0)
                        SaveREF(oSVCs[i].oREFs, oIDSet);

                    // SAVE AMT //
                    if (oSVCs[i].oAMTs.Count > 0)
                        SaveAMT(oSVCs[i].oAMTs, oIDSet);

                    // SAVE LQ //
                    if (oSVCs[i].oLQs.Count > 0)
                        SaveLQ(oSVCs[i].oLQs, oIDSet);

                }
                SVC[] oSVC = new SVC[oSVCs.Count];
                oSVCs.CopyTo(oSVC, 0);
                if (oSVC != null)
                {
                    var _test = (from r in oSVC select r).ToList();
                    DataTable dt = ConvertToDataTable(_test);
                    if (dtSVC != null && dtSVC.Rows.Count > 0)
                        dtSVC.Merge(dt);
                    else
                        dtSVC = dt;
                    if (dt != null) { dt.Dispose(); dt = null; }
                    oSVC = null;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                throw ex;
            }
        }

        private void SaveMIA(MIAs oMIAs, IDSet oIDSet)
        {
            try
            {
                for (int i = 0; i < oMIAs.Count; i++)
                {
                    #region " Save MIA "

                    oMIAs[i].ERAFileID = oIDSet.ERAFileID;
                    oMIAs[i].ISAID = oIDSet.ISAID;
                    oMIAs[i].BPRID = oIDSet.BPRID;
                    oMIAs[i].CLPID = oIDSet.CLPID;
                    oMIAs[i].MIAID = Convert.ToInt64(dtUniqueIDs.Rows[nUniqueIDsIndex][0]);
                    nUniqueIDsIndex++;

                    //oDBPara.Clear();
                    //oDBPara.Add("@nMIAID", 0, ParameterDirection.InputOutput, SqlDbType.BigInt);
                    //oDBPara.Add("@nERAFileID", oIDSet.ERAFileID, ParameterDirection.Input, SqlDbType.BigInt);
                    //oDBPara.Add("@nISAID", oIDSet.ISAID, ParameterDirection.Input, SqlDbType.BigInt);
                    //oDBPara.Add("@nBPRID", oIDSet.BPRID, ParameterDirection.Input, SqlDbType.BigInt);
                    //oDBPara.Add("@nCLPID", oIDSet.CLPID, ParameterDirection.Input, SqlDbType.BigInt);
                    //oDBPara.Add("@sMIA01_Qty", oMIAs[i].MIA01_Qty, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sMIA02_Qty", oMIAs[i].MIA02_Qty, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sMIA03_Qty", oMIAs[i].MIA03_Qty, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sMIA04_Amount", oMIAs[i].MIA04_Amount, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sMIA05_Ref_ID", oMIAs[i].MIA05_Ref_ID, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sMIA06_Amount", oMIAs[i].MIA06_Amount, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sMIA07_Amount", oMIAs[i].MIA07_Amount, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sMIA08_Amount", oMIAs[i].MIA08_Amount, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sMIA09_Amount", oMIAs[i].MIA09_Amount, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sMIA10_Amount", oMIAs[i].MIA10_Amount, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sMIA11_Amount", oMIAs[i].MIA11_Amount, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sMIA12_Amount", oMIAs[i].MIA12_Amount, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sMIA13_Amount", oMIAs[i].MIA13_Amount, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sMIA14_Amount", oMIAs[i].MIA14_Amount, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sMIA15_Qty", oMIAs[i].MIA15_Qty, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sMIA16_Amount", oMIAs[i].MIA16_Amount, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sMIA17_Amount", oMIAs[i].MIA17_Amount, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sMIA18_Amount", oMIAs[i].MIA18_Amount, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sMIA19_Amount", oMIAs[i].MIA19_Amount, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sMIA20_Ref_ID", oMIAs[i].MIA20_Ref_ID, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sMIA21_Ref_ID", oMIAs[i].MIA21_Ref_ID, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sMIA22_Ref_ID", oMIAs[i].MIA22_Ref_ID, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sMIA23_Ref_ID", oMIAs[i].MIA23_Ref_ID, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sMIA24_Amount", oMIAs[i].MIA24_Amount, ParameterDirection.Input, SqlDbType.VarChar);

                    //oDB.Execute("ERA_IN_MIA", oDBPara);

                    #endregion
                }
                MIA[] oMIA = new MIA[oMIAs.Count];
                oMIAs.CopyTo(oMIA, 0);
                if (oMIA != null)
                {
                    var _test = (from r in oMIA select r).ToList();
                    DataTable dt = ConvertToDataTable(_test);
                    if (dtMIA != null && dtMIA.Rows.Count > 0)
                        dtMIA.Merge(dt);
                    else
                        dtMIA = dt;
                    if (dt != null) { dt.Dispose(); dt = null; }
                    oMIA = null;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                throw ex;
            }
        }

        private void SaveMOA(MOAs oMOAs, IDSet oIDSet)
        {
            try
            {
                for (int i = 0; i < oMOAs.Count; i++)
                {
                    #region " Save MOA "

                    oMOAs[i].ERAFileID = oIDSet.ERAFileID;
                    oMOAs[i].ISAID = oIDSet.ISAID;
                    oMOAs[i].BPRID = oIDSet.BPRID;
                    oMOAs[i].CLPID = oIDSet.CLPID;
                    oMOAs[i].MOAID = Convert.ToInt64(dtUniqueIDs.Rows[nUniqueIDsIndex][0]);
                    nUniqueIDsIndex++;

                    //oDBPara.Clear();
                    //oDBPara.Add("@nMOAID", 0, ParameterDirection.InputOutput, SqlDbType.BigInt);
                    //oDBPara.Add("@nERAFileID", oIDSet.ERAFileID, ParameterDirection.Input, SqlDbType.BigInt);
                    //oDBPara.Add("@nISAID", oIDSet.ISAID, ParameterDirection.Input, SqlDbType.BigInt);
                    //oDBPara.Add("@nBPRID", oIDSet.BPRID, ParameterDirection.Input, SqlDbType.BigInt);
                    //oDBPara.Add("@nCLPID", oIDSet.CLPID, ParameterDirection.Input, SqlDbType.BigInt);
                    //oDBPara.Add("@sMOA01_Percent", oMOAs[i].MOA01_Percent, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sMOA02_Amount", oMOAs[i].MOA02_Amount, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sMOA03_Ref_ID", oMOAs[i].MOA03_Ref_ID, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sMOA04_Ref_ID", oMOAs[i].MOA04_Ref_ID, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sMOA05_Ref_ID", oMOAs[i].MOA05_Ref_ID, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sMOA06_Ref_ID", oMOAs[i].MOA06_Ref_ID, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sMOA07_Ref_ID", oMOAs[i].MOA07_Ref_ID, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sMOA08_Amount", oMOAs[i].MOA08_Amount, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sMOA09_Amount", oMOAs[i].MOA09_Amount, ParameterDirection.Input, SqlDbType.VarChar);

                    //oDB.Execute("ERA_IN_MOA", oDBPara);

                    #endregion
                }
                MOA[] oMOA = new MOA[oMOAs.Count];
                oMOAs.CopyTo(oMOA, 0);
                if (oMOA != null)
                {
                    var _test = (from r in oMOA select r).ToList();
                    DataTable dt = ConvertToDataTable(_test);
                    if (dtMOA != null && dtMOA.Rows.Count > 0)
                        dtMOA.Merge(dt);
                    else
                        dtMOA = dt;
                    if (dt != null) { dt.Dispose(); dt = null; }
                    oMOA = null;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                throw ex;
            }
        }

        private void SavePER(PERs oPERs, IDSet oIDSet)
        {
            try
            {
                for (int i = 0; i < oPERs.Count; i++)
                {
                    #region " Save PER "

                    oPERs[i].ERAFileID = oIDSet.ERAFileID;
                    oPERs[i].ISAID = oIDSet.ISAID;
                    oPERs[i].BPRID = oIDSet.BPRID;
                    oPERs[i].CLPID = oIDSet.CLPID;
                    oPERs[i].PayerPayeeID = oIDSet.PayerPayeeID;
                    oPERs[i].PERID = Convert.ToInt64(dtUniqueIDs.Rows[nUniqueIDsIndex][0]);
                    nUniqueIDsIndex++;

                    //oDBPara.Clear();
                    //oDBPara.Add("@nPERID", 0, ParameterDirection.InputOutput, SqlDbType.BigInt);
                    //oDBPara.Add("@nERAFileID", oIDSet.ERAFileID, ParameterDirection.Input, SqlDbType.BigInt);
                    //oDBPara.Add("@nISAID", oIDSet.ISAID, ParameterDirection.Input, SqlDbType.BigInt);
                    //oDBPara.Add("@nBPRID", oIDSet.BPRID, ParameterDirection.Input, SqlDbType.BigInt);
                    //oDBPara.Add("@nPayerPayeeID", oIDSet.PayerPayeeID, ParameterDirection.Input, SqlDbType.BigInt);
                    //oDBPara.Add("@nCLPID", oIDSet.CLPID, ParameterDirection.Input, SqlDbType.BigInt);
                    //oDBPara.Add("@sPER01_Cont_Func_Code", oPERs[i].PER01_Cont_Func_Code, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sPER02_Name", oPERs[i].PER02_Name, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sPER03_CommNoQual", oPERs[i].PER03_CommNoQual, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sPER04_CommNo", oPERs[i].PER04_CommNo, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sPER05_CommNoQual", oPERs[i].PER05_CommNoQual, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sPER06_CommNo", oPERs[i].PER06_CommNo, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sPER07_CommNoQual", oPERs[i].PER07_CommNoQual, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sPER08_CommNo", oPERs[i].PER08_CommNo, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sPER09_Cont_Inq_Ref", oPERs[i].PER09_Cont_Inq_Ref, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@nPERType", oPERs[i].PERType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);

                    //oDB.Execute("ERA_IN_PER", oDBPara);

                    #endregion
                }
                PER[] oPER = new PER[oPERs.Count];
                oPERs.CopyTo(oPER, 0);
                if (oPER != null)
                {
                    var _test = (from r in oPER select r).ToList();
                    DataTable dt = ConvertToDataTable(_test);
                    if (dtPER != null && dtPER.Rows.Count > 0)
                        dtPER.Merge(dt);
                    else
                        dtPER = dt;

                    if (dt != null) { dt.Dispose(); dt = null; }
                    oPER = null;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                throw ex;
            }
        }


        private void SaveRDM(RDMs oRDMs, IDSet oIDSet)
        {
            try
            {
                for (int i = 0; i < oRDMs.Count; i++)
                {
                    #region " Save PER "

                    oRDMs[i].ERAFileID = oIDSet.ERAFileID;
                    oRDMs[i].ISAID = oIDSet.ISAID;
                    oRDMs[i].BPRID = oIDSet.BPRID;
                    oRDMs[i].RDMID = Convert.ToInt64(dtUniqueIDs.Rows[nUniqueIDsIndex][0]);
                    nUniqueIDsIndex++;

                    //oDBPara.Clear();
                    //oDBPara.Add("@nRDMID", 0, ParameterDirection.InputOutput, SqlDbType.BigInt);
                    //oDBPara.Add("@nERAFileID", oIDSet.ERAFileID, ParameterDirection.Input, SqlDbType.BigInt);
                    //oDBPara.Add("@nISAID", oIDSet.ISAID, ParameterDirection.Input, SqlDbType.BigInt);
                    //oDBPara.Add("@nBPRID", oIDSet.BPRID, ParameterDirection.Input, SqlDbType.BigInt);

                    //oDBPara.Add("@sRDM01_ReportTranCode", oRDMs[i].RDM01_ReportTranCode, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sRDM02_Name", oRDMs[i].RDM02_Name, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sRDM03_CommunicationNumber", oRDMs[i].RDM03_CommunicationNumber, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sRDM04_REF_ID", oRDMs[i].RDM04_REF_ID, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sRDM05_REF_ID", oRDMs[i].RDM05_REF_ID, ParameterDirection.Input, SqlDbType.VarChar);
                    
                    //oDB.Execute("ERA_IN_RDM", oDBPara);

                    #endregion
                }
                RDM[] oRDM = new RDM[oRDMs.Count];
                oRDMs.CopyTo(oRDM, 0);
                if (oRDM != null)
                {
                    var _test = (from r in oRDM select r).ToList();
                    DataTable dt = ConvertToDataTable(_test);
                    if (dtRDM != null && dtRDM.Rows.Count > 0)
                        dtRDM.Merge(dt);
                    else
                        dtRDM = dt;
                    if (dt != null) { dt.Dispose(); dt = null; }
                    oRDM = null;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                throw ex;
            }
        }


        private void SaveAMT(AMTs oAMTs, IDSet oIDSet)
        {
            try
            {
                for (int i = 0; i < oAMTs.Count; i++)
                {
                    #region " Save AMT "

                    oAMTs[i].ERAFileID = oIDSet.ERAFileID;
                    oAMTs[i].ISAID = oIDSet.ISAID;
                    oAMTs[i].BPRID = oIDSet.BPRID;
                    oAMTs[i].CLPID = oIDSet.CLPID;
                    oAMTs[i].SVCID = oIDSet.SVCID;
                    oAMTs[i].AMTID = Convert.ToInt64(dtUniqueIDs.Rows[nUniqueIDsIndex][0]);
                    nUniqueIDsIndex++;

                    //oDBPara.Clear();
                    //oDBPara.Add("@nAMTID", 0, ParameterDirection.InputOutput, SqlDbType.BigInt);
                    //oDBPara.Add("@nERAFileID", oIDSet.ERAFileID, ParameterDirection.Input, SqlDbType.BigInt);
                    //oDBPara.Add("@nISAID", oIDSet.ISAID, ParameterDirection.Input, SqlDbType.BigInt);
                    //oDBPara.Add("@nBPRID", oIDSet.BPRID, ParameterDirection.Input, SqlDbType.BigInt);
                    //oDBPara.Add("@nCLPID", oIDSet.CLPID, ParameterDirection.Input, SqlDbType.BigInt);
                    //oDBPara.Add("@nSVCID", oIDSet.SVCID, ParameterDirection.Input, SqlDbType.BigInt);
                    //oDBPara.Add("@sAMT01_AmountQualCode", oAMTs[i].AMT01_AmountQualCode, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sAMT02_Amount", oAMTs[i].AMT02_Amount, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sAMT03_CreditDebitFlagCode", oAMTs[i].AMT03_CreditDebitFlagCode, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@nAMTType", oAMTs[i].AMTType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);

                    //oDB.Execute("ERA_IN_AMT", oDBPara);

                    #endregion
                }
                AMT[] oAMT = new AMT[oAMTs.Count];
                oAMTs.CopyTo(oAMT, 0);
                if (oAMT != null)
                {
                    var _test = (from r in oAMT select r).ToList();
                    DataTable dt = ConvertToDataTable(_test);
                    if (dtAMT != null && dtAMT.Rows.Count > 0)
                        dtAMT.Merge(dt);
                    else
                        dtAMT = dt;
                    if (dt != null) { dt.Dispose(); dt = null; }
                    oAMT = null;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                throw ex;
            }
        }

        private void SaveLQ(LQs oLQs, IDSet oIDSet)
        {
            try
            {
                for (int i = 0; i < oLQs.Count; i++)
                {
                    #region " Save LQ "

                    oLQs[i].ERAFileID = oIDSet.ERAFileID;
                    oLQs[i].ISAID = oIDSet.ISAID;
                    oLQs[i].BPRID = oIDSet.BPRID;
                    oLQs[i].CLPID = oIDSet.CLPID;
                    oLQs[i].SVCID = oIDSet.SVCID;
                    oLQs[i].LQID = Convert.ToInt64(dtUniqueIDs.Rows[nUniqueIDsIndex][0]);
                    nUniqueIDsIndex++;

                    //oDBPara.Clear();
                    //oDBPara.Add("@nLQID", 0, ParameterDirection.InputOutput, SqlDbType.BigInt);
                    //oDBPara.Add("@nERAFileID", oIDSet.ERAFileID, ParameterDirection.Input, SqlDbType.BigInt);
                    //oDBPara.Add("@nISAID", oIDSet.ISAID, ParameterDirection.Input, SqlDbType.BigInt);
                    //oDBPara.Add("@nBPRID", oIDSet.BPRID, ParameterDirection.Input, SqlDbType.BigInt);
                    //oDBPara.Add("@nCLPID", oIDSet.CLPID, ParameterDirection.Input, SqlDbType.BigInt);
                    //oDBPara.Add("@nSVCID", oIDSet.SVCID, ParameterDirection.Input, SqlDbType.BigInt);
                    //oDBPara.Add("@sLQ01_CodeListQualCode", oLQs[i].LQ01_CodeListQualCode, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sLQ02_IndustryCode", oLQs[i].LQ02_IndustryCode, ParameterDirection.Input, SqlDbType.VarChar);

                    //oDB.Execute("ERA_IN_LQ", oDBPara);

                    #endregion
                }
                LQ[] oLQ = new LQ[oLQs.Count];
                oLQs.CopyTo(oLQ, 0);
                if (oLQ != null)
                {
                    var _test = (from r in oLQ select r).ToList();
                    DataTable dt = ConvertToDataTable(_test);
                    if (dtLQ != null && dtLQ.Rows.Count > 0)
                        dtLQ.Merge(dt);
                    else
                        dtLQ = dt;
                    if (dt != null) { dt.Dispose(); dt = null; }
                    oLQ = null;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                throw ex;
            }
        }

        #endregion

        private bool SaveParsedFile(ISAs oISAs, Int64 nFileID)
        {
            //gloAuditTrail.gloAuditTrail.ExceptionLog("SaveParsedFile  start", false);
            bool _Result = false;
            try
            {
                if (!OpenConnection(true))
                    return false;

                if (oISAs == null || oISAs.Count == 0)
                    return false;

                dtUniqueIDs = getUniQueIDS();
                if (dtUniqueIDs != null && dtUniqueIDs.Rows.Count > 0 )
                {
                 

                    SaveISA(oISAs, nFileID);
                    //gloAuditTrail.gloAuditTrail.ExceptionLog("SaveERAFileDatainTables  start", false);
                    SaveERAFileDatainTables();
                    //gloAuditTrail.gloAuditTrail.ExceptionLog("SaveERAFileDatainTables  end", false);
                    _Result = true;
                    //gloAuditTrail.gloAuditTrail.ExceptionLog("SaveParsedFile  end", false);
                    

                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog("Error for saving ERA file of ERAFileID:" + nFileID + ", Error Description : " + ex.ToString(), false);
            }
            finally
            {
                CloseConnection();
            }
            return _Result;
        }

        private bool SaveERAFileDatainTables()
        {
            
                SqlConnection conn = new System.Data.SqlClient.SqlConnection(_DataBaseConnectionString);
                conn.Open();
               //SqlTransaction tran = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                SqlBulkCopy bulkCopy = null; 
                try
                {
                    bulkCopy = new System.Data.SqlClient.SqlBulkCopy(_DataBaseConnectionString);
                    bulkCopy.BulkCopyTimeout = 0;
                    

                    if (dtTRN != null && dtTRN.Rows.Count > 0)
                    {
                        bulkCopy.DestinationTableName = "dbo.ERA_TRN";
                        bulkCopy.ColumnMappings.Clear();
                        bulkCopy.ColumnMappings.Add("TRNID", "nTRNID");
                        bulkCopy.ColumnMappings.Add("ERAFileID", "nERAFileID");
                        bulkCopy.ColumnMappings.Add("ISAID", "nISAID");
                        bulkCopy.ColumnMappings.Add("BPRID", "nBPRID");
                        bulkCopy.ColumnMappings.Add("TRN01_TraceTypeCode", "sTRN01_TraceTypeCode");
                        bulkCopy.ColumnMappings.Add("TRN02_Ref_ID", "sTRN02_Ref_ID");
                        bulkCopy.ColumnMappings.Add("TRN03_OriginatingCompanyID", "sTRN03_OriginatingCompanyID");
                        bulkCopy.ColumnMappings.Add("TRN04_Ref_ID", "sTRN04_Ref_ID");
                        bulkCopy.WriteToServer(dtTRN);
                    }

                    if (dtREF != null && dtREF.Rows.Count > 0)
                    {
                        bulkCopy.DestinationTableName = "dbo.ERA_REF";
                        bulkCopy.ColumnMappings.Clear();
                        bulkCopy.ColumnMappings.Add("REFID", "nREFID");
                        bulkCopy.ColumnMappings.Add("ERAFileID", "nERAFileID");
                        bulkCopy.ColumnMappings.Add("ISAID", "nISAID");
                        bulkCopy.ColumnMappings.Add("BPRID", "nBPRID");
                        bulkCopy.ColumnMappings.Add("PayerPayeeID", "nPayerPayeeID");
                        bulkCopy.ColumnMappings.Add("CLPID", "nCLPID");
                        bulkCopy.ColumnMappings.Add("SVCID", "nSVCID");
                        bulkCopy.ColumnMappings.Add("REF01_Ref_IDQual", "sREF01_Ref_IDQual");
                        bulkCopy.ColumnMappings.Add("REF02_Ref_ID", "sREF02_Ref_ID");
                        bulkCopy.ColumnMappings.Add("REF03_Desc", "sREF03_Desc");
                        bulkCopy.ColumnMappings.Add("REF04_Ref_ID", "sREF04_Ref_ID");
                        bulkCopy.ColumnMappings.Add("REFType", "nREFType");
                        bulkCopy.WriteToServer(dtREF);
                    }

                    if (dtDTM != null && dtDTM.Rows.Count > 0)
                    {
                        bulkCopy.DestinationTableName = "dbo.ERA_DTM";
                        bulkCopy.ColumnMappings.Clear();
                        bulkCopy.ColumnMappings.Add("DTMID", "nDTMID");
                        bulkCopy.ColumnMappings.Add("ERAFileID", "nERAFileID");
                        bulkCopy.ColumnMappings.Add("ISAID", "nISAID");
                        bulkCopy.ColumnMappings.Add("BPRID", "nBPRID");
                        bulkCopy.ColumnMappings.Add("CLPID", "nCLPID");
                        bulkCopy.ColumnMappings.Add("SVCID", "nSVCID");
                        bulkCopy.ColumnMappings.Add("DTM01_DateTimeQual", "sDTM01_DateTimeQual");
                        bulkCopy.ColumnMappings.Add("DTM02_Date", "sDTM02_Date");
                        bulkCopy.ColumnMappings.Add("DTM03_Time", "sDTM03_Time");
                        bulkCopy.ColumnMappings.Add("DTM04_TimeCode", "sDTM04_TimeCode");
                        bulkCopy.ColumnMappings.Add("DTM05_DateTimePeriodFormatQual", "sDTM05_DateTimePeriodFormatQual");
                        bulkCopy.ColumnMappings.Add("DTM06_DateTimePeriod", "sDTM06_DateTimePeriod");
                        bulkCopy.ColumnMappings.Add("DTMType", "nDTMType");
                        bulkCopy.WriteToServer(dtDTM);
                    }

                    if (dtPLB != null && dtPLB.Rows.Count > 0)
                    {
                        bulkCopy.DestinationTableName = "dbo.ERA_PLB";
                        bulkCopy.ColumnMappings.Clear();
                        bulkCopy.ColumnMappings.Add("PLBID", "nPLBID");
                        bulkCopy.ColumnMappings.Add("ERAFileID", "nERAFileID");
                        bulkCopy.ColumnMappings.Add("ISAID", "nISAID");
                        bulkCopy.ColumnMappings.Add("BPRID", "nBPRID");
                        bulkCopy.ColumnMappings.Add("PLB01_Ref_ID", "sPLB01_Ref_ID");
                        bulkCopy.ColumnMappings.Add("PLB02_Date", "sPLB02_Date");
                        bulkCopy.ColumnMappings.Add("PLB03_AdjustID", "sPLB03_AdjustID");
                        bulkCopy.ColumnMappings.Add("PLB03_1_AdjustReasonCode", "sPLB03_1_AdjustReasonCode");
                        bulkCopy.ColumnMappings.Add("PLB03_2_Ref_ID", "sPLB03_2_Ref_ID");
                        bulkCopy.ColumnMappings.Add("PLB04_Amount", "sPLB04_Amount");
                        bulkCopy.ColumnMappings.Add("PLB05_AdjustID", "sPLB05_AdjustID");
                        bulkCopy.ColumnMappings.Add("PLB05_1_AdjustReasonCode", "sPLB05_1_AdjustReasonCode");
                        bulkCopy.ColumnMappings.Add("PLB05_2_Ref_ID", "sPLB05_2_Ref_ID");
                        bulkCopy.ColumnMappings.Add("PLB06_Amount", "sPLB06_Amount");
                        bulkCopy.ColumnMappings.Add("PLB07_AdjustID", "sPLB07_AdjustID");
                        bulkCopy.ColumnMappings.Add("PLB07_1_AdjustReasonCode", "sPLB07_1_AdjustReasonCode");
                        bulkCopy.ColumnMappings.Add("PLB07_2_Ref_ID", "sPLB07_2_Ref_ID");
                        bulkCopy.ColumnMappings.Add("PLB08_Amount", "sPLB08_Amount");
                        bulkCopy.ColumnMappings.Add("PLB09_AdjustID", "sPLB09_AdjustID");
                        bulkCopy.ColumnMappings.Add("PLB09_1_AdjustReasonCode", "sPLB09_1_AdjustReasonCode");
                        bulkCopy.ColumnMappings.Add("PLB09_2_Ref_ID", "sPLB09_2_Ref_ID");
                        bulkCopy.ColumnMappings.Add("PLB10_Amount", "sPLB10_Amount");
                        bulkCopy.ColumnMappings.Add("PLB11_AdjustID", "sPLB11_AdjustID");
                        bulkCopy.ColumnMappings.Add("PLB11_1_AdjustReasonCode", "sPLB11_1_AdjustReasonCode");
                        bulkCopy.ColumnMappings.Add("PLB11_2_Ref_ID", "sPLB11_2_Ref_ID");
                        bulkCopy.ColumnMappings.Add("PLB12_Amount", "sPLB12_Amount");
                        bulkCopy.ColumnMappings.Add("PLB13_AdjustID", "sPLB13_AdjustID");
                        bulkCopy.ColumnMappings.Add("PLB13_1_AdjustReasonCode", "sPLB13_1_AdjustReasonCode");
                        bulkCopy.ColumnMappings.Add("PLB13_2_Ref_ID", "sPLB13_2_Ref_ID");
                        bulkCopy.ColumnMappings.Add("PLB14_Amount", "sPLB14_Amount");
                        bulkCopy.WriteToServer(dtPLB);
                    }

                    if (dtpayer != null && dtpayer.Rows.Count > 0)
                    {
                        bulkCopy.DestinationTableName = "dbo.ERA_PayerPayeeIdentification";
                        bulkCopy.ColumnMappings.Clear();
                        bulkCopy.ColumnMappings.Add("PayerPayeeID", "nPayerPayeeID");
                        bulkCopy.ColumnMappings.Add("ERAFileID", "nERAFileID");
                        bulkCopy.ColumnMappings.Add("ISAID", "nISAID");
                        bulkCopy.ColumnMappings.Add("BPRID", "nBPRID");
                        bulkCopy.ColumnMappings.Add("N101_EntityIDCode", "sN101_EntityIDCode");
                        bulkCopy.ColumnMappings.Add("N102_Name", "sN102_Name");
                        bulkCopy.ColumnMappings.Add("N103_IDCodeQual", "sN103_IDCodeQual");
                        bulkCopy.ColumnMappings.Add("N104_IDCode", "sN104_IDCode");
                        bulkCopy.ColumnMappings.Add("N105_EntityRelationCode", "sN105_EntityRelationCode");
                        bulkCopy.ColumnMappings.Add("N106_EntityIDCode", "sN106_EntityIDCode");
                        bulkCopy.ColumnMappings.Add("N301_AddrInfo", "sN301_AddrInfo");
                        bulkCopy.ColumnMappings.Add("N302_AddrInfo", "sN302_AddrInfo");
                        bulkCopy.ColumnMappings.Add("N401_CityName", "sN401_CityName");
                        bulkCopy.ColumnMappings.Add("N402_StateOrProvinceCode", "sN402_StateOrProvinceCode");
                        bulkCopy.ColumnMappings.Add("N403_PostalCode", "sN403_PostalCode");
                        bulkCopy.ColumnMappings.Add("N404_CountryCode", "sN404_CountryCode");
                        bulkCopy.ColumnMappings.Add("N405_LocationQual", "sN405_LocationQual");
                        bulkCopy.ColumnMappings.Add("N406_LocationID", "sN406_LocationID");
                        bulkCopy.ColumnMappings.Add("N407_CountrySubDivisionCode", "sN407_CountrySubDivisionCode");
                        bulkCopy.ColumnMappings.Add("PayerPayeeType", "nPayerPayeeType");
                        bulkCopy.WriteToServer(dtpayer);
                    }

                    if (dtCLP != null && dtCLP.Rows.Count > 0)
                    {
                        bulkCopy.DestinationTableName = "dbo.ERA_CLP";
                        bulkCopy.ColumnMappings.Clear();
                        bulkCopy.ColumnMappings.Add("CLPID", "nCLPID");
                        bulkCopy.ColumnMappings.Add("ERAFileID", "nERAFileID");
                        bulkCopy.ColumnMappings.Add("ISAID", "nISAID");
                        bulkCopy.ColumnMappings.Add("BPRID", "nBPRID");
                        bulkCopy.ColumnMappings.Add("CLP01_ClaimSubmitterID", "sCLP01_ClaimSubmitterID");
                        bulkCopy.ColumnMappings.Add("CLP02_ClaimStatusCode", "sCLP02_ClaimStatusCode");
                        bulkCopy.ColumnMappings.Add("CLP03_Amount", "sCLP03_Amount");
                        bulkCopy.ColumnMappings.Add("CLP04_Amount", "sCLP04_Amount");
                        bulkCopy.ColumnMappings.Add("CLP05_Amount", "sCLP05_Amount");
                        bulkCopy.ColumnMappings.Add("CLP06_ClaimFilingIndicatorCode", "sCLP06_ClaimFilingIndicatorCode");
                        bulkCopy.ColumnMappings.Add("CLP07_Ref_ID", "sCLP07_Ref_ID");
                        bulkCopy.ColumnMappings.Add("CLP08_FacilityCodeValue", "sCLP08_FacilityCodeValue");
                        bulkCopy.ColumnMappings.Add("CLP09_ClaimFrequencyTypeCode", "sCLP09_ClaimFrequencyTypeCode");
                        bulkCopy.ColumnMappings.Add("CLP10_PatientStatusCode", "sCLP10_PatientStatusCode");
                        bulkCopy.ColumnMappings.Add("CLP11_DiagRelatedGroupCode", "sCLP11_DiagRelatedGroupCode");
                        bulkCopy.ColumnMappings.Add("CLP12_Qty", "sCLP12_Qty");
                        bulkCopy.ColumnMappings.Add("CLP13_Percent", "sCLP13_Percent");
                        bulkCopy.ColumnMappings.Add("ClaimFileIndex", "nClaimFileIndex");
                        bulkCopy.WriteToServer(dtCLP);
                    }

                    if (dtCAS != null && dtCAS.Rows.Count > 0)
                    {
                        bulkCopy.DestinationTableName = "dbo.ERA_CAS";
                        bulkCopy.ColumnMappings.Clear();
                        bulkCopy.ColumnMappings.Add("CASID", "nCASID");
                        bulkCopy.ColumnMappings.Add("ERAFileID", "nERAFileID");
                        bulkCopy.ColumnMappings.Add("ISAID", "nISAID");
                        bulkCopy.ColumnMappings.Add("BPRID", "nBPRID");
                        bulkCopy.ColumnMappings.Add("CLPID", "nCLPID");
                        bulkCopy.ColumnMappings.Add("SVCID", "nSVCID");
                        bulkCopy.ColumnMappings.Add("CAS01_ClaimAdjustGroupCode", "sCAS01_ClaimAdjustGroupCode");
                        bulkCopy.ColumnMappings.Add("CAS02_ClaimAdjustReasonCode", "sCAS02_ClaimAdjustReasonCode");
                        bulkCopy.ColumnMappings.Add("CAS03_Amount", "sCAS03_Amount");
                        bulkCopy.ColumnMappings.Add("CAS04_Qty", "sCAS04_Qty");
                        bulkCopy.ColumnMappings.Add("CAS05_ClaimAdjustReasonCode", "sCAS05_ClaimAdjustReasonCode");
                        bulkCopy.ColumnMappings.Add("CAS06_Amount", "sCAS06_Amount");
                        bulkCopy.ColumnMappings.Add("CAS07_Qty", "sCAS07_Qty");
                        bulkCopy.ColumnMappings.Add("CAS08_ClaimAdjustReasonCode", "sCAS08_ClaimAdjustReasonCode");
                        bulkCopy.ColumnMappings.Add("CAS09_Amount", "sCAS09_Amount");
                        bulkCopy.ColumnMappings.Add("CAS10_Qty", "sCAS10_Qty");
                        bulkCopy.ColumnMappings.Add("CAS11_ClaimAdjustReasonCode", "sCAS11_ClaimAdjustReasonCode");
                        bulkCopy.ColumnMappings.Add("CAS12_Amount", "sCAS12_Amount");
                        bulkCopy.ColumnMappings.Add("CAS13_Qty", "sCAS13_Qty");
                        bulkCopy.ColumnMappings.Add("CAS14_ClaimAdjustReasonCode", "sCAS14_ClaimAdjustReasonCode");
                        bulkCopy.ColumnMappings.Add("CAS15_Amount", "sCAS15_Amount");
                        bulkCopy.ColumnMappings.Add("CAS16_Qty", "sCAS16_Qty");
                        bulkCopy.ColumnMappings.Add("CAS17_ClaimAdjustReasonCode", "sCAS17_ClaimAdjustReasonCode");
                        bulkCopy.ColumnMappings.Add("CAS18_Amount", "sCAS18_Amount");
                        bulkCopy.ColumnMappings.Add("CAS19_Qty", "sCAS19_Qty");
                        bulkCopy.ColumnMappings.Add("CASType", "nCASType");
                        bulkCopy.WriteToServer(dtCAS);
                    }

                    if (dtNM1 != null && dtNM1.Rows.Count > 0)
                    {
                        bulkCopy.DestinationTableName = "dbo.ERA_NM1";
                        bulkCopy.ColumnMappings.Clear();
                        bulkCopy.ColumnMappings.Add("NM1ID", "nNM1ID");
                        bulkCopy.ColumnMappings.Add("ERAFileID", "nERAFileID");
                        bulkCopy.ColumnMappings.Add("ISAID", "nISAID");
                        bulkCopy.ColumnMappings.Add("BPRID", "nBPRID");
                        bulkCopy.ColumnMappings.Add("CLPID", "nCLPID");
                        bulkCopy.ColumnMappings.Add("NM101_EntityIDCode", "sNM101_EntityIDCode");
                        bulkCopy.ColumnMappings.Add("NM102_EntityTypeQual", "sNM102_EntityTypeQual");
                        bulkCopy.ColumnMappings.Add("NM103_NameLastOrOrgName", "sNM103_NameLastOrOrgName");
                        bulkCopy.ColumnMappings.Add("NM104_NameFirst", "sNM104_NameFirst");
                        bulkCopy.ColumnMappings.Add("NM105_NameMiddle", "sNM105_NameMiddle");
                        bulkCopy.ColumnMappings.Add("NM106_NamePrefix", "sNM106_NamePrefix");
                        bulkCopy.ColumnMappings.Add("NM107_NameSuffix", "sNM107_NameSuffix");
                        bulkCopy.ColumnMappings.Add("NM108_IDCodeQual", "sNM108_IDCodeQual");
                        bulkCopy.ColumnMappings.Add("NM109_IDCode", "sNM109_IDCode");
                        bulkCopy.ColumnMappings.Add("NM110_EntityRelationCode", "sNM110_EntityRelationCode");
                        bulkCopy.ColumnMappings.Add("NM111_EntityIDCode", "sNM111_EntityIDCode");
                        bulkCopy.ColumnMappings.Add("NM1Type", "nNM1Type");
                        bulkCopy.WriteToServer(dtNM1);
                    }

                    if (dtMIA != null && dtMIA.Rows.Count > 0)
                    {
                        bulkCopy.DestinationTableName = "dbo.ERA_MIA";
                        bulkCopy.ColumnMappings.Clear();
                        bulkCopy.ColumnMappings.Add("MIAID", "nMIAID");
                        bulkCopy.ColumnMappings.Add("ERAFileID", "nERAFileID");
                        bulkCopy.ColumnMappings.Add("ISAID", "nISAID");
                        bulkCopy.ColumnMappings.Add("BPRID", "nBPRID");
                        bulkCopy.ColumnMappings.Add("CLPID", "nCLPID");
                        bulkCopy.ColumnMappings.Add("MIA01_Qty", "sMIA01_Qty");
                        bulkCopy.ColumnMappings.Add("MIA02_Qty", "sMIA02_Qty");
                        bulkCopy.ColumnMappings.Add("MIA03_Qty", "sMIA03_Qty");
                        bulkCopy.ColumnMappings.Add("MIA04_Amount", "sMIA04_Amount");
                        bulkCopy.ColumnMappings.Add("MIA05_Ref_ID", "sMIA05_Ref_ID");
                        bulkCopy.ColumnMappings.Add("MIA06_Amount", "sMIA06_Amount");
                        bulkCopy.ColumnMappings.Add("MIA07_Amount", "sMIA07_Amount");
                        bulkCopy.ColumnMappings.Add("MIA08_Amount", "sMIA08_Amount");
                        bulkCopy.ColumnMappings.Add("MIA09_Amount", "sMIA09_Amount");
                        bulkCopy.ColumnMappings.Add("MIA10_Amount", "sMIA10_Amount");
                        bulkCopy.ColumnMappings.Add("MIA11_Amount", "sMIA11_Amount");
                        bulkCopy.ColumnMappings.Add("MIA12_Amount", "sMIA12_Amount");
                        bulkCopy.ColumnMappings.Add("MIA13_Amount", "sMIA13_Amount");
                        bulkCopy.ColumnMappings.Add("MIA14_Amount", "sMIA14_Amount");
                        bulkCopy.ColumnMappings.Add("MIA15_Qty", "sMIA15_Qty");
                        bulkCopy.ColumnMappings.Add("MIA16_Amount", "sMIA16_Amount");
                        bulkCopy.ColumnMappings.Add("MIA17_Amount", "sMIA17_Amount");
                        bulkCopy.ColumnMappings.Add("MIA18_Amount", "sMIA18_Amount");
                        bulkCopy.ColumnMappings.Add("MIA19_Amount", "sMIA19_Amount");
                        bulkCopy.ColumnMappings.Add("MIA20_Ref_ID", "sMIA20_Ref_ID");
                        bulkCopy.ColumnMappings.Add("MIA21_Ref_ID", "sMIA21_Ref_ID");
                        bulkCopy.ColumnMappings.Add("MIA22_Ref_ID", "sMIA22_Ref_ID");
                        bulkCopy.ColumnMappings.Add("MIA23_Ref_ID", "sMIA23_Ref_ID");
                        bulkCopy.ColumnMappings.Add("MIA24_Amount", "sMIA24_Amount");
                        bulkCopy.WriteToServer(dtMIA);
                    }

                    if (dtMOA != null && dtMOA.Rows.Count > 0)
                    {
                        bulkCopy.DestinationTableName = "dbo.ERA_MOA";
                        bulkCopy.ColumnMappings.Clear();
                        bulkCopy.ColumnMappings.Add("MOAID", "nMOAID");
                        bulkCopy.ColumnMappings.Add("ERAFileID", "nERAFileID");
                        bulkCopy.ColumnMappings.Add("ISAID", "nISAID");
                        bulkCopy.ColumnMappings.Add("BPRID", "nBPRID");
                        bulkCopy.ColumnMappings.Add("CLPID", "nCLPID");
                        bulkCopy.ColumnMappings.Add("MOA01_Percent", "sMOA01_Percent");
                        bulkCopy.ColumnMappings.Add("MOA02_Amount", "sMOA02_Amount");
                        bulkCopy.ColumnMappings.Add("MOA03_Ref_ID", "sMOA03_Ref_ID");
                        bulkCopy.ColumnMappings.Add("MOA04_Ref_ID", "sMOA04_Ref_ID");
                        bulkCopy.ColumnMappings.Add("MOA05_Ref_ID", "sMOA05_Ref_ID");
                        bulkCopy.ColumnMappings.Add("MOA06_Ref_ID", "sMOA06_Ref_ID");
                        bulkCopy.ColumnMappings.Add("MOA07_Ref_ID", "sMOA07_Ref_ID");
                        bulkCopy.ColumnMappings.Add("MOA08_Amount", "sMOA08_Amount");
                        bulkCopy.ColumnMappings.Add("MOA09_Amount", "sMOA09_Amount");
                        bulkCopy.WriteToServer(dtMOA);
                    }

                    if (dtPER != null && dtPER.Rows.Count > 0)
                    {
                        bulkCopy.DestinationTableName = "dbo.ERA_PER";
                        bulkCopy.ColumnMappings.Clear();
                        bulkCopy.ColumnMappings.Add("PERID", "nPERID");
                        bulkCopy.ColumnMappings.Add("ERAFileID", "nERAFileID");
                        bulkCopy.ColumnMappings.Add("ISAID", "nISAID");
                        bulkCopy.ColumnMappings.Add("BPRID", "nBPRID");
                        bulkCopy.ColumnMappings.Add("CLPID", "nCLPID");
                        bulkCopy.ColumnMappings.Add("PayerPayeeID", "nPayerPayeeID");
                        bulkCopy.ColumnMappings.Add("PER01_Cont_Func_Code", "sPER01_Cont_Func_Code");
                        bulkCopy.ColumnMappings.Add("PER02_Name", "sPER02_Name");
                        bulkCopy.ColumnMappings.Add("PER03_CommNoQual", "sPER03_CommNoQual");
                        bulkCopy.ColumnMappings.Add("PER04_CommNo", "sPER04_CommNo");
                        bulkCopy.ColumnMappings.Add("PER05_CommNoQual", "sPER05_CommNoQual");
                        bulkCopy.ColumnMappings.Add("PER06_CommNo", "sPER06_CommNo");
                        bulkCopy.ColumnMappings.Add("PER07_CommNoQual", "sPER07_CommNoQual");
                        bulkCopy.ColumnMappings.Add("PER08_CommNo", "sPER08_CommNo");
                        bulkCopy.ColumnMappings.Add("PER09_Cont_Inq_Ref", "sPER09_Cont_Inq_Ref");
                        bulkCopy.ColumnMappings.Add("PERType", "nPERType");
                        bulkCopy.WriteToServer(dtPER);
                    }

                    if (dtAMT != null && dtAMT.Rows.Count > 0)
                    {
                        bulkCopy.DestinationTableName = "dbo.ERA_AMT";
                        bulkCopy.ColumnMappings.Clear();
                        bulkCopy.ColumnMappings.Add("AMTID", "nAMTID");
                        bulkCopy.ColumnMappings.Add("ERAFileID", "nERAFileID");
                        bulkCopy.ColumnMappings.Add("ISAID", "nISAID");
                        bulkCopy.ColumnMappings.Add("BPRID", "nBPRID");
                        bulkCopy.ColumnMappings.Add("CLPID", "nCLPID");
                        bulkCopy.ColumnMappings.Add("SVCID", "nSVCID");
                        bulkCopy.ColumnMappings.Add("AMT01_AmountQualCode", "sAMT01_AmountQualCode");
                        bulkCopy.ColumnMappings.Add("AMT02_Amount", "sAMT02_Amount");
                        bulkCopy.ColumnMappings.Add("AMT03_CreditDebitFlagCode", "sAMT03_CreditDebitFlagCode");
                        bulkCopy.ColumnMappings.Add("AMTType", "nAMTType");
                        bulkCopy.WriteToServer(dtAMT);
                    }

                    if (dtSVC != null && dtSVC.Rows.Count > 0)
                    {
                        bulkCopy.DestinationTableName = "dbo.ERA_SVC";
                        bulkCopy.ColumnMappings.Clear();
                        bulkCopy.ColumnMappings.Add("SVCID", "nSVCID");
                        bulkCopy.ColumnMappings.Add("ERAFileID", "nERAFileID");
                        bulkCopy.ColumnMappings.Add("ISAID", "nISAID");
                        bulkCopy.ColumnMappings.Add("BPRID", "nBPRID");
                        bulkCopy.ColumnMappings.Add("CLPID", "nCLPID");
                        bulkCopy.ColumnMappings.Add("SVC01_CompositeMedicalProc", "sSVC01_CompositeMedicalProc");
                        bulkCopy.ColumnMappings.Add("SVC01_1_ProductServiceIDQual", "sSVC01_1_ProductServiceIDQual");
                        bulkCopy.ColumnMappings.Add("SVC01_2_ProductServiceID", "sSVC01_2_ProductServiceID");
                        bulkCopy.ColumnMappings.Add("SVC01_3_ProcMod", "sSVC01_3_ProcMod");
                        bulkCopy.ColumnMappings.Add("SVC01_4_ProcMod", "sSVC01_4_ProcMod");
                        bulkCopy.ColumnMappings.Add("SVC01_5_ProcMod", "sSVC01_5_ProcMod");
                        bulkCopy.ColumnMappings.Add("SVC01_6_ProcMod", "sSVC01_6_ProcMod");
                        bulkCopy.ColumnMappings.Add("SVC01_7_Desc", "sSVC01_7_Desc");
                        bulkCopy.ColumnMappings.Add("SVC02_Amount", "sSVC02_Amount");
                        bulkCopy.ColumnMappings.Add("SVC03_Amount", "sSVC03_Amount");
                        bulkCopy.ColumnMappings.Add("SVC04_ProductServiceID", "sSVC04_ProductServiceID");
                        bulkCopy.ColumnMappings.Add("SVC05_Qty", "sSVC05_Qty");
                        bulkCopy.ColumnMappings.Add("SVC06_CompositeMedicalProc", "sSVC06_CompositeMedicalProc");
                        bulkCopy.ColumnMappings.Add("SVC06_1_ProductServiceIDQual", "sSVC06_1_ProductServiceIDQual");
                        bulkCopy.ColumnMappings.Add("SVC06_2_ProductServiceID", "sSVC06_2_ProductServiceID");
                        bulkCopy.ColumnMappings.Add("SVC06_3_ProcMod", "sSVC06_3_ProcMod");
                        bulkCopy.ColumnMappings.Add("SVC06_4_ProcMod", "sSVC06_4_ProcMod");
                        bulkCopy.ColumnMappings.Add("SVC06_5_ProcMod", "sSVC06_5_ProcMod");
                        bulkCopy.ColumnMappings.Add("SVC06_6_ProcMod", "sSVC06_6_ProcMod");
                        bulkCopy.ColumnMappings.Add("SVC06_7_Desc", "sSVC06_7_Desc");
                        bulkCopy.ColumnMappings.Add("SVC07_Qty", "sSVC07_Qty");
                        bulkCopy.ColumnMappings.Add("ChargeIndex", "nChargeIndex");
                        bulkCopy.WriteToServer(dtSVC);
                    }

                    if (dtLQ != null && dtLQ.Rows.Count > 0)
                    {
                        bulkCopy.DestinationTableName = "dbo.ERA_LQ";
                        bulkCopy.ColumnMappings.Clear();
                        bulkCopy.ColumnMappings.Add("LQID", "nLQID");
                        bulkCopy.ColumnMappings.Add("ERAFileID", "nERAFileID");
                        bulkCopy.ColumnMappings.Add("ISAID", "nISAID");
                        bulkCopy.ColumnMappings.Add("BPRID", "nBPRID");
                        bulkCopy.ColumnMappings.Add("CLPID", "nCLPID");
                        bulkCopy.ColumnMappings.Add("SVCID", "nSVCID");
                        bulkCopy.ColumnMappings.Add("LQ01_CodeListQualCode", "sLQ01_CodeListQualCode");
                        bulkCopy.ColumnMappings.Add("LQ02_IndustryCode", "sLQ02_IndustryCode");
                        bulkCopy.WriteToServer(dtLQ);
                    }

                    if (dtRDM != null && dtRDM.Rows.Count > 0)
                    {
                        bulkCopy.DestinationTableName = "dbo.ERA_RDM";
                        bulkCopy.ColumnMappings.Clear();
                        bulkCopy.ColumnMappings.Add("RDMID", "nRDMID");
                        bulkCopy.ColumnMappings.Add("ERAFileID", "nERAFileID");
                        bulkCopy.ColumnMappings.Add("ISAID", "nISAID");
                        bulkCopy.ColumnMappings.Add("BPRID", "nBPRID");
                        bulkCopy.ColumnMappings.Add("RDM01_ReportTranCode", "sRDM01_ReportTranCode");
                        bulkCopy.ColumnMappings.Add("RDM02_Name", "sRDM02_Name");
                        bulkCopy.ColumnMappings.Add("RDM03_CommunicationNumber", "sRDM03_CommunicationNumber");
                        bulkCopy.ColumnMappings.Add("RDM04_REF_ID", "sRDM04_REF_ID");
                        bulkCopy.ColumnMappings.Add("RDM05_REF_ID", "sRDM05_REF_ID");
                        bulkCopy.WriteToServer(dtBPR);
                    }
                   
                    if (dtBPR != null && dtBPR.Rows.Count > 0)
                    {
                        bulkCopy.DestinationTableName = "dbo.ERA_BPR";
                        bulkCopy.ColumnMappings.Clear();
                        bulkCopy.ColumnMappings.Add("BPRID", "nBPRID");
                        bulkCopy.ColumnMappings.Add("ERAFileID", "nERAFileID");
                        bulkCopy.ColumnMappings.Add("ISAID", "nISAID");
                        bulkCopy.ColumnMappings.Add("BPR01_TransHandlingCode", "sBPR01_TransHandlingCode");
                        bulkCopy.ColumnMappings.Add("BPR02_Amount", "sBPR02_Amount");
                        bulkCopy.ColumnMappings.Add("BPR03_CreditDebitFlagCode", "sBPR03_CreditDebitFlagCode");
                        bulkCopy.ColumnMappings.Add("BPR04_PaymentMethodCode", "sBPR04_PaymentMethodCode");
                        bulkCopy.ColumnMappings.Add("BPR05_PaymentFormatCode", "sBPR05_PaymentFormatCode");
                        bulkCopy.ColumnMappings.Add("BPR06_DFI_IDNoQual", "sBPR06_DFI_IDNoQual");
                        bulkCopy.ColumnMappings.Add("BPR07_DFI_IDNo", "sBPR07_DFI_IDNo");
                        bulkCopy.ColumnMappings.Add("BPR08_AccNoQual", "sBPR08_AccNoQual");
                        bulkCopy.ColumnMappings.Add("BPR09_AccNo", "sBPR09_AccNo");
                        bulkCopy.ColumnMappings.Add("BPR10_OriginatingCompanyID", "sBPR10_OriginatingCompanyID");
                        bulkCopy.ColumnMappings.Add("BPR11_OriginatingCompanySuppCode", "sBPR11_OriginatingCompanySuppCode");
                        bulkCopy.ColumnMappings.Add("BPR12_DFI_IDNoQual", "sBPR12_DFI_IDNoQual");
                        bulkCopy.ColumnMappings.Add("BPR13_DFI_IDNo", "sBPR13_DFI_IDNo");
                        bulkCopy.ColumnMappings.Add("BPR14_AccNoQual", "sBPR14_AccNoQual");
                        bulkCopy.ColumnMappings.Add("BPR15_AccNo", "sBPR15_AccNo");
                        bulkCopy.ColumnMappings.Add("BPR16_Date", "sBPR16_Date");
                        bulkCopy.ColumnMappings.Add("BPR17_BusinessFunc_Code", "sBPR17_BusinessFunc_Code");
                        bulkCopy.ColumnMappings.Add("BPR18_DFI_IDNoQual", "sBPR18_DFI_IDNoQual");
                        bulkCopy.ColumnMappings.Add("BPR19_DFI_IDNo", "sBPR19_DFI_IDNo");
                        bulkCopy.ColumnMappings.Add("BPR20_AccNoQual", "sBPR20_AccNoQual");
                        bulkCopy.ColumnMappings.Add("BPR21_AccNo", "sBPR21_AccNo");
                        bulkCopy.ColumnMappings.Add("CheckStatus", "nCheckStatus");
                        bulkCopy.WriteToServer(dtBPR);
                    }

                    if (dtISA != null && dtISA.Rows.Count > 0)
                    {
                        bulkCopy.DestinationTableName = "dbo.ERA_ISA";
                        bulkCopy.ColumnMappings.Clear();
                        bulkCopy.ColumnMappings.Add("ISAID", "nISAID");
                        bulkCopy.ColumnMappings.Add("ERAFileID", "nERAFileID");
                        bulkCopy.ColumnMappings.Add("ISA01_AuthorInfoQual", "sISA01_AuthorInfoQual");
                        bulkCopy.ColumnMappings.Add("ISA02_AuthorInfo", "sISA02_AuthorInfo");
                        bulkCopy.ColumnMappings.Add("ISA03_SecurityInfoQual", "sISA03_SecurityInfoQual");
                        bulkCopy.ColumnMappings.Add("ISA04_SecurityInfo", "sISA04_SecurityInfo");
                        bulkCopy.ColumnMappings.Add("ISA05_IntrChngIDQual", "sISA05_IntrChngIDQual");
                        bulkCopy.ColumnMappings.Add("ISA06_IntrChngSenderID", "sISA06_IntrChngSenderID");
                        bulkCopy.ColumnMappings.Add("ISA07_IntrChngIDQual", "sISA07_IntrChngIDQual");
                        bulkCopy.ColumnMappings.Add("ISA08_IntrChngReceiverID", "sISA08_IntrChngReceiverID");
                        bulkCopy.ColumnMappings.Add("ISA09_IntrChngDate", "sISA09_IntrChngDate");
                        bulkCopy.ColumnMappings.Add("ISA10_IntrChngTime", "sISA10_IntrChngTime");
                        bulkCopy.ColumnMappings.Add("ISA11_IntrChngControlStandardsID", "sISA11_IntrChngControlStandardsID");
                        bulkCopy.ColumnMappings.Add("ISA12_IntrChngControlVersionNo", "sISA12_IntrChngControlVersionNo");
                        bulkCopy.ColumnMappings.Add("ISA13_IntrChngControlNo", "sISA13_IntrChngControlNo");
                        bulkCopy.ColumnMappings.Add("ISA14_AckwRequested", "sISA14_AckwRequested");
                        bulkCopy.ColumnMappings.Add("ISA15_UsageIndicator", "sISA15_UsageIndicator");
                        bulkCopy.ColumnMappings.Add("ISA16_ComponentElementSeparator", "sISA16_ComponentElementSeparator");
                        bulkCopy.WriteToServer(dtISA);
                    }
                    //tran.Commit();
                    bulkCopy.Close();
                    bulkCopy = null;

                   
                }
                catch (Exception ex)
                {
                    //gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                    throw ex;
                    //tran.Rollback();
                }
                finally
                {
                    if (conn != null) { conn.Dispose(); conn = null; }
                    //if (tran != null) { tran.Dispose(); tran = null; }
                    bulkCopy = null;
                    if (dtISA != null) { dtISA.Dispose(); dtISA = null; }
                    if (dtTRN != null) { dtTRN.Dispose(); dtTRN = null; }
                    if (dtBPR != null) { dtBPR.Dispose(); dtBPR = null; }
                    if (dtREF != null) { dtREF.Dispose(); dtREF = null; }
                    if (dtDTM != null) { dtDTM.Dispose(); dtDTM = null; }
                    if (dtPLB != null) { dtPLB.Dispose(); dtPLB = null; }
                    if (dtpayer != null) { dtpayer.Dispose(); dtpayer = null; }
                    if (dtCLP != null) { dtCLP.Dispose(); dtCLP = null; }
                    if (dtCAS != null) { dtCAS.Dispose(); dtCAS = null; }
                    if (dtNM1 != null) { dtNM1.Dispose(); dtNM1 = null; }
                    if (dtMIA != null) { dtMIA.Dispose(); dtMIA = null; }
                    if (dtMOA != null) { dtMOA.Dispose(); dtMOA = null; }
                    if (dtPER != null) { dtPER.Dispose(); dtPER = null; }
                    if (dtAMT != null) { dtAMT.Dispose(); dtAMT = null; }
                    if (dtSVC != null) { dtSVC.Dispose(); dtSVC = null; }
                    if (dtLQ != null) { dtLQ.Dispose(); dtLQ = null; }
                    if (dtRDM != null) { dtRDM.Dispose(); dtRDM = null; }
                    _IDcount = 0;
                    nUniqueIDsIndex = 0;
                }

            
            return true;
        }

        private DataTable getUniQueIDS()
        {
            DataTable dt = null;
            try
            {
                if (_IDcount > 0)
                {
                    if (OpenConnection(true))
                    {
                        oDBPara.Clear();
                        oDBPara.Add("@IDCount", _IDcount, ParameterDirection.Input, SqlDbType.BigInt);
                        oDBPara.Add("@SingleRow", 1, ParameterDirection.Input, SqlDbType.Bit);
                        oDB.Retrive("gsp_GetUniqueIDs", oDBPara, out dt);
                        CloseConnection();
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog("Error in getUniQueIDS : " + ex.ToString(), false);
                return null;
            }
            return dt;
        }


        private void LoadEDISchema()
        {
            string sPath = "";
            //string sEntity = "";
            //string sQlfr = "";
            string sSefFile = "";
            //   string sEdiFile = "";
            try
            {
                EdiDoc = new ediDocument();
                //ediDocument.Set(ref EdiDoc, new ediDocument());    // EdiDoc = new ediDocument();(
                EdiDoc.CursorType = DocumentCursorTypeConstants.Cursor_ForwardOnly;

                sPath = AppDomain.CurrentDomain.BaseDirectory;
                // sEdiFile = "835.X12";
                //if (FileVersion == "5010")
                //sSefFile = "835_005010X221A1.SemRef.SEF";
                // else
                    
                // Disabling the internal standard reference library to makes sure that 
                // FREDI uses only the SEF file provided
                ediSchemas.Set(ref oSchemas, EdiDoc.GetSchemas());    
                //oSchemas = (ediSchemas) EdiDoc.GetSchemas();
                oSchemas.EnableStandardReference = false;
                //oSchemas.set_Option(SchemasOptionIDConstants.OptSchemas_VersionRestrict, 1);
                oSchemas.set_Option(SchemasOptionIDConstants.OptSchemas_SetOnDemand, 1);

                //Import sef file
                sSefFile = "835_005010X221A1.SemRef.SEF";
                EdiDoc.ImportSchema(sPath + sSefFile, 0);
                sSefFile = "835_X091A1.SEF";
                EdiDoc.ImportSchema(sPath + sSefFile, 0);
                //5060
                //ediSchema.Set(ref oSchema, (ediSchema)EdiDoc.LoadSchema(sPath + sSefFile, 0));

                // This makes certain that the EDI file must use the same version SEF file, otherwise
                // the process will stop.
               // oSchemas.set_Option(SchemasOptionIDConstants.OptSchemas_VersionRestrict, 1);

                // By setting the cursor type to ForwardOnly, FREDI does not load the entire file into memory, which
                // improves performance when processing larger EDI files.
                

                // If an acknowledgment file has to be generated, an acknowledgment object must be created, and its 
                // property must be enabled before loading the EDI file.
                //oAck = (ediAcknowledgment)EdiDoc.GetAcknowledgment();
                //oAck.EnableFunctionalAcknowledgment = true;

                //// Set the starting point of the control numbers in the acknowledgment
                //oAck.set_Property(AcknowledgmentPropertyIDConstants.PropertyAck_StartInterchangeControlNum, 1001);
                //oAck.set_Property(AcknowledgmentPropertyIDConstants.PropertyAck_StartGroupControlNum, 1);
                //oAck.set_Property(AcknowledgmentPropertyIDConstants.PropertyAck_StartTransactionSetControlNum, 1);

                //// Error codes that are not automatically mapped to an acknowlegment error number by FREDI can be set by
                //// using the MapDataElementLevelError method.
                //oAck.MapDataElementLevelError(13209, 5, "", "", "", "");

                // All SEF files required for processing the EDI files must be provided by calling the LoadSchema.  
                // The "LoadSchema" method does not actually load the SEF files at this time, but are actually 
                // loaded during the LoadEdi method.
                //EdiDoc.LoadSchema(sSefFile, 0);    //for EDI (810) file
                //EdiDoc.LoadSchema("997_X12-4010.SEF", 0);    //for Ack (997) file

            }
            catch (System.Runtime.CompilerServices.RuntimeWrappedException Rex)
            {
                string _strEx = "";
                ediException oException = null;
                oException = (ediException)Rex.WrappedException;
                _strEx = oException.get_Description();
                gloAuditTrail.gloAuditTrail.ExceptionLog(_strEx, true);
                //_result = "";
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private ISAs ReadRemittances(string EDIFileName)
        {
            //gloAuditTrail.gloAuditTrail.ExceptionLog("ReadRemittances start", false);
            #region " Segment Variable Declaration "

            ISAs oISAs = null;

            ISA oISA = null;
            BPR oBPR = null;
            TRN oTRN = null;
            PLB oPLB = null;
            CLP oCLP = null;
            PayerPayeeIdent oPayerPayeeIdent = null;
            MIA oMIA = null;
            MOA oMOA = null;
            REF oREF = null;
            RDM oRDM = null;
            DTM oDTM = null;
            SVC oSVC = null;
            AMT oAMT = null;
            NM1 oNM1 = null;
            CAS oCAS = null;
            LQ oLQ = null;
            PER oPER = null;


            #endregion

            #region " Variable Declaration "

            string sSegmentID = "";
            string sLoopSection = "";
            string sLXID = "";
            string sEntity = "";
           // string sEdiFile = "";
            int nArea = 0;
            string sValue = "";
            Int32 _nArea2RowCount = 0;
            //int Area2rowIndex = 0;
         //   int rowIndex = 0;
            Int32 _SegmentNo = 1;
            int i = 0;
            int _Index = 0;
            string _Temp = "";

            #endregion

            try
            {
                //EdiDoc.LoadSchema("997_X12-4010.SEF", 0);    //for Ack (997) file                

                //EdiDoc = new ediDocument();

                //// If an acknowledgment file has to be generated, an acknowledgment object must be created, and its 
                //// property must be enabled before loading the EDI file.
                //oAck = (ediAcknowledgment)EdiDoc.GetAcknowledgment();
                //oAck.EnableFunctionalAcknowledgment = true;

                ////// Set the starting point of the control numbers in the acknowledgment
                //oAck.set_Property(AcknowledgmentPropertyIDConstants.PropertyAck_StartInterchangeControlNum, 1001);
                //oAck.set_Property(AcknowledgmentPropertyIDConstants.PropertyAck_StartGroupControlNum, 1);
                //oAck.set_Property(AcknowledgmentPropertyIDConstants.PropertyAck_StartTransactionSetControlNum, 1);

                //Set the cursor type to write an acknowledgement file.
                //EdiDoc.CursorType = DocumentCursorTypeConstants.Cursor_ForwardWrite;

                ////// Error codes that are not automatically mapped to an acknowlegment error number by FREDI can be set by
                ////// using the MapDataElementLevelError method.
                //oAck.MapDataElementLevelError(13209, 5, "", "", "", "");

                if (EdiDoc == null)
                    return null;

                if (EDIFileName.Trim() != "")
                {
                    EdiDoc.LoadEdi(EDIFileName);
                }
                else
                {
                    return oISAs;
                }


                // Gets the first data segment in the EDI files
                //ediDataSegment.Set(ref oSegment, (ediDataSegment)EdiDoc.FirstDataSegment);  //oSegment = (ediDataSegment) EdiDoc.FirstDataSegment
                oSegment = EdiDoc.FirstDataSegment;
                if (oSegment != null)
                {
                    sSegmentID = oSegment.ID;
                    sLoopSection = oSegment.LoopSection;
                }

                // This loop iterates though the EDI file a segment at a time
                while (oSegment != null)
                {
                    // A segment is identified by its Area number, Loop section and segment id.                  
                    nArea = oSegment.Area;

                    if (nArea == 0)
                    {
                        if (sLoopSection == "")
                        {
                            if (sSegmentID == "ISA")
                            {
                                // map data elements of ISA segment in here
                                #region " ISA Segment "

                                oISA = new ISA();
                                oISA.ISAID = 2;
                                oISA.ISA01_AuthorInfoQual = oSegment.get_DataElementValue(1);    //Authorization Information Qualifier
                                oISA.ISA02_AuthorInfo = oSegment.get_DataElementValue(2);    //Authorization Information
                                oISA.ISA03_SecurityInfoQual = oSegment.get_DataElementValue(3);    //Security Information Qualifier
                                oISA.ISA04_SecurityInfo = oSegment.get_DataElementValue(4);    //Security Information
                                oISA.ISA05_IntrChngIDQual = oSegment.get_DataElementValue(5);    //Interchange ID Qualifier
                                oISA.ISA06_IntrChngSenderID = oSegment.get_DataElementValue(6);    //Interchange Sender ID
                                oISA.ISA07_IntrChngIDQual = oSegment.get_DataElementValue(7);    //Interchange ID Qualifier
                                oISA.ISA08_IntrChngReceiverID = oSegment.get_DataElementValue(8);    //Interchange Receiver ID
                                oISA.ISA09_IntrChngDate = oSegment.get_DataElementValue(9);    //Interchange Date
                                oISA.ISA10_IntrChngTime = oSegment.get_DataElementValue(10);   //Interchange Time
                                oISA.ISA11_IntrChngControlStandardsID = oSegment.get_DataElementValue(11);   //Interchange Control Standards Identifier
                                oISA.ISA12_IntrChngControlVersionNo = oSegment.get_DataElementValue(12);   //Interchange Control Version Number
                                oISA.ISA13_IntrChngControlNo = oSegment.get_DataElementValue(13);   //Interchange Control Number
                                oISA.ISA14_AckwRequested = oSegment.get_DataElementValue(14);   //Acknowledgment Requested
                                oISA.ISA15_UsageIndicator = oSegment.get_DataElementValue(15);   //Usage Indicator
                                oISA.ISA16_ComponentElementSeparator = oSegment.get_DataElementValue(16);   //Component Element Separator

                                if (oISAs == null) oISAs = new ISAs();
                                oISAs.Add(oISA);
                                _IDcount += 1;
                                //_LatestSegment = SEG_ISA;

                                #endregion " ISA Segment "
                            }
                            else if (sSegmentID == "GS")
                            {
                                #region " GS Segment "
                                // map data elements of GS segment in here
                                //sValue = oSegment.get_DataElementValue(1);     //Functional Identifier Code
                                //sValue = oSegment.get_DataElementValue(2);    //Application Sender's Code
                                //sValue = oSegment.get_DataElementValue(3);    //Application Receiver's Code
                                //sValue = oSegment.get_DataElementValue(4);   //Date
                                //sValue = oSegment.get_DataElementValue(5);   //Time
                                //sValue = oSegment.get_DataElementValue(6);  //Group Control Number
                                //sValue = oSegment.get_DataElementValue(7);  //Responsible Agency Code
                                sValue = oSegment.get_DataElementValue(8);   //Version / Rele
                                if ((sValue.Contains("4010") || sValue.Contains("5010")) == false)
                                {
                                    oISAs = null;
                                    return oISAs; 
                                }
                                if (sValue.Contains("5010"))
                                {
                                    System.Text.RegularExpressions.Regex pattern = new System.Text.RegularExpressions.Regex("[a-zA-Z0-9]");
                                    if(pattern.IsMatch(oISA.ISA11_IntrChngControlStandardsID ))
                                    {
                                        oISAs = null;
                                        return oISAs; 
                                    }
                                    _FileVersion = "5010";
                                }
                                else if (sValue.Contains("4010"))
                                {
                                    _FileVersion = "4010";
                                }
                                #endregion " GS Segment "
                            }

                           
                        }
                    }
                    else if (nArea == 1)
                    {
                        if (sLoopSection == "")
                        {
                            if (sSegmentID == "ST")
                            {
                                #region " ST Segment "
                                // map data element of ST segment in here
                                //sValue = oSegment.get_DataElementValue(1);     //Transaction Set Identifier Code
                                //sValue = oSegment.get_DataElementValue(2);     //Transaction Set Control Number
                                #endregion " ST Segment "
                            }
                            else if (sSegmentID == "BPR")
                            {
                                #region " BPR Segment "
                                //if (oSegment.get_DataElementValue(1).ToString() == "I")//It was "C" changed to "I"
                                //{
                                oBPR = new BPR();
                                oBPR.BPR01_TransHandlingCode = oSegment.get_DataElementValue(1);
                                oBPR.BPR02_Amount = oSegment.get_DataElementValue(2);  //Monetary Amount
                                oBPR.BPR03_CreditDebitFlagCode = oSegment.get_DataElementValue(3);  ////////Credit/Debit Flag Code
                                oBPR.BPR04_PaymentMethodCode = oSegment.get_DataElementValue(4);   //Payment Method Code
                                oBPR.BPR05_PaymentFormatCode = oSegment.get_DataElementValue(5);    //Payment Format Code
                                oBPR.BPR06_DFI_IDNoQual = oSegment.get_DataElementValue(6);  //(DFI) ID Number Qualifier
                                oBPR.BPR07_DFI_IDNo = oSegment.get_DataElementValue(7);
                                oBPR.BPR08_AccNoQual = oSegment.get_DataElementValue(8);    //Account Number Qualifier
                                oBPR.BPR09_AccNo = oSegment.get_DataElementValue(9);
                                oBPR.BPR10_OriginatingCompanyID = oSegment.get_DataElementValue(10);
                                oBPR.BPR11_OriginatingCompanySuppCode = oSegment.get_DataElementValue(11);   //Originating Company Supplemental Code
                                oBPR.BPR12_DFI_IDNoQual = oSegment.get_DataElementValue(12);   //(DFI) ID Number Qualifier
                                oBPR.BPR13_DFI_IDNo = oSegment.get_DataElementValue(13);
                                oBPR.BPR14_AccNoQual = oSegment.get_DataElementValue(14);     //Account Number Qualifier
                                oBPR.BPR15_AccNo = oSegment.get_DataElementValue(15);
                                oBPR.BPR16_Date = oSegment.get_DataElementValue(16);
                                oBPR.BPR17_BusinessFunc_Code = oSegment.get_DataElementValue(17);
                                oBPR.BPR18_DFI_IDNoQual = oSegment.get_DataElementValue(18);
                                oBPR.BPR19_DFI_IDNo = oSegment.get_DataElementValue(19);
                                oBPR.BPR20_AccNoQual = oSegment.get_DataElementValue(20);
                                oBPR.BPR21_AccNo = oSegment.get_DataElementValue(21);
                                oBPR.CheckStatus = enumCheckStatus.Ready;
                                oISA.oBPRs.Add(oBPR);
                                _IDcount += 1;
                                //_LatestSegment = SEG_BPR;

                                //}
                                #endregion " BPR Segment "
                            }
                            else if (sSegmentID == "TRN")
                            {
                                #region " TRN Segment "
                                oTRN = new TRN();
                                oTRN.TRN01_TraceTypeCode = oSegment.get_DataElementValue(1);
                                oTRN.TRN02_Ref_ID = oSegment.get_DataElementValue(2);
                                oTRN.TRN03_OriginatingCompanyID = oSegment.get_DataElementValue(3);
                                oTRN.TRN04_Ref_ID = oSegment.get_DataElementValue(4);

                                oBPR.oTRNs.Add(oTRN);
                                _IDcount += 1;
                                #endregion " TRN Segment "
                            }
                            else if (sSegmentID == "DTM")
                            {
                                #region " DTM Segment "
                                oDTM = new DTM();
                                oDTM.DTM01_DateTimeQual = oSegment.get_DataElementValue(1);    //Date/Time Qualifier
                                oDTM.DTM02_Date = oSegment.get_DataElementValue(2);
                                oDTM.DTM03_Time = oSegment.get_DataElementValue(3);
                                oDTM.DTM04_TimeCode = oSegment.get_DataElementValue(4);
                                oDTM.DTM05_DateTimePeriodFormatQual = oSegment.get_DataElementValue(5);
                                oDTM.DTM06_DateTimePeriod = oSegment.get_DataElementValue(6);

                                oDTM.DTMType = enumDTM_Type.ProductionDate;
                                oBPR.oDTMs.Add(oDTM);
                                _IDcount += 1;
                                #endregion " DTM Segment "
                            }
                            else if (sSegmentID == "REF")
                            {
                                #region " Receiver REF Segment "
                                oREF = new REF();

                                oREF.REF01_Ref_IDQual = oSegment.get_DataElementValue(1);
                                oREF.REF02_Ref_ID = oSegment.get_DataElementValue(2);
                                oREF.REF03_Desc = oSegment.get_DataElementValue(3);
                                oREF.REF04_Ref_ID = oSegment.get_DataElementValue(4);

                                oREF.REFType = enumREF_Type.ReceiverID;
                                oBPR.oREFs.Add(oREF);
                                _IDcount += 1;
                                #endregion " Payer REF Segment "
                            }

                            

                        } // sLoopSection == ""

                        else if (sLoopSection == "N1")
                        {
                            if (sSegmentID == "N1")
                            {
                                sEntity = oSegment.get_DataElementValue(1); //get loop entity qualifier to identity each N1 loop instances
                            }

                            if (sEntity == "PR") //payer information
                            {
                                if (sSegmentID == "N1")
                                {
                                    #region " Payer N1 Segment "
                                    oPayerPayeeIdent = new PayerPayeeIdent();
                                    oPayerPayeeIdent.PayerPayeeType = enumPayerPayee_Type.Payer;
                                    

                                    oPayerPayeeIdent.N101_EntityIDCode = oSegment.get_DataElementValue(1);  // Entity Identifier Code (98) 
                                    oPayerPayeeIdent.N102_Name = oSegment.get_DataElementValue(2);
                                    oPayerPayeeIdent.N103_IDCodeQual = oSegment.get_DataElementValue(3);  // Identification Code Qualifier (66) 
                                    oPayerPayeeIdent.N104_IDCode = oSegment.get_DataElementValue(4);  // Identification Code (67) 
                                    oPayerPayeeIdent.N105_EntityRelationCode = oSegment.get_DataElementValue(5);  // Entity Relationship Code (706) 
                                    oPayerPayeeIdent.N106_EntityIDCode = oSegment.get_DataElementValue(6);  // Entity Identifier Code (98)

                                    oBPR.oPayerPayeeIdents.Add(oPayerPayeeIdent);
                                    _IDcount += 1;
                                    #endregion " Payer N1 Segment "
                                }
                                else if (sSegmentID == "N3")
                                {
                                    #region " Payer N3 Segment "
                                    oPayerPayeeIdent.N301_AddrInfo = oSegment.get_DataElementValue(1);
                                    oPayerPayeeIdent.N302_AddrInfo = oSegment.get_DataElementValue(2);   // Address Information (166) 
                                    #endregion " Payer N3 Segment "
                                }
                                else if (sSegmentID == "N4")
                                {
                                    #region " Payer N4 Segment "
                                    oPayerPayeeIdent.N401_CityName = oSegment.get_DataElementValue(1);
                                    oPayerPayeeIdent.N402_StateOrProvinceCode = oSegment.get_DataElementValue(2);
                                    oPayerPayeeIdent.N403_PostalCode = oSegment.get_DataElementValue(3);
                                    oPayerPayeeIdent.N404_CountryCode = oSegment.get_DataElementValue(4);  // Country Code (26) 
                                    oPayerPayeeIdent.N405_LocationQual = oSegment.get_DataElementValue(5); // Location Qualifier (309) 
                                    oPayerPayeeIdent.N406_LocationID = oSegment.get_DataElementValue(6);  // Location Identifier (310) 
                                    oPayerPayeeIdent.N407_CountrySubDivisionCode = oSegment.get_DataElementValue(7);  
                                    #endregion " Payer N4 Segment "
                                }
                                else if (sSegmentID == "PER")
                                {
                                    #region " Payer Contact Info Segment "
                                    oPER = new PER();
                                    oPER.PER01_Cont_Func_Code = oSegment.get_DataElementValue(1);
                                    oPER.PER02_Name = oSegment.get_DataElementValue(2);
                                    oPER.PER03_CommNoQual = oSegment.get_DataElementValue(3);
                                    oPER.PER04_CommNo = oSegment.get_DataElementValue(4);
                                    oPER.PER05_CommNoQual = oSegment.get_DataElementValue(5);
                                    oPER.PER06_CommNo = oSegment.get_DataElementValue(6);
                                    oPER.PER07_CommNoQual = oSegment.get_DataElementValue(7);
                                    oPER.PER08_CommNo = oSegment.get_DataElementValue(8);
                                    oPER.PER09_Cont_Inq_Ref = oSegment.get_DataElementValue(9);
                                    if(oPER.PER01_Cont_Func_Code == "CX")
                                        oPER.PERType = enumPER_Type.PayerContactInfo;
                                    else if (oPER.PER01_Cont_Func_Code == "BL")
                                        oPER.PERType = enumPER_Type.PayerTechnicalInfo;
                                    else if (oPER.PER01_Cont_Func_Code == "IC")
                                        oPER.PERType = enumPER_Type.PayerWebSiteInfo;
                                    oPayerPayeeIdent.oPERs.Add(oPER);
                                    _IDcount += 1;
                                    #endregion
                                }
                                else if (sSegmentID == "REF")
                                {
                                    #region " Payer REF Segment "
                                    oREF = new REF();

                                    oREF.REF01_Ref_IDQual = oSegment.get_DataElementValue(1);
                                    oREF.REF02_Ref_ID = oSegment.get_DataElementValue(2);
                                    oREF.REF03_Desc = oSegment.get_DataElementValue(3);
                                    oREF.REF04_Ref_ID = oSegment.get_DataElementValue(4);

                                    oREF.REFType = enumREF_Type.AdditionalPayerID;
                                    oPayerPayeeIdent.oREFs.Add(oREF);
                                    _IDcount += 1;

                                    #endregion " Payer REF Segment "
                                }
                            }
                            else if (sEntity == "PE")//payee information
                            {
                                if (sSegmentID == "N1")
                                {
                                    #region " Payee N1 Segment "

                                    oPayerPayeeIdent = new PayerPayeeIdent();
                                    oPayerPayeeIdent.PayerPayeeType = enumPayerPayee_Type.Payee;
                                    oBPR.oPayerPayeeIdents.Add(oPayerPayeeIdent);
                                    _IDcount += 1;
                                    oPayerPayeeIdent.N101_EntityIDCode = oSegment.get_DataElementValue(1);   // Entity Identifier Code (98) 
                                    oPayerPayeeIdent.N102_Name = oSegment.get_DataElementValue(2);
                                    oPayerPayeeIdent.N103_IDCodeQual = oSegment.get_DataElementValue(3);   //Identification Code Qualifier (66) 
                                    oPayerPayeeIdent.N104_IDCode = oSegment.get_DataElementValue(4);//It could be NPI of Provider
                                    oPayerPayeeIdent.N105_EntityRelationCode = oSegment.get_DataElementValue(5); // Entity Relationship Code (706) 
                                    oPayerPayeeIdent.N106_EntityIDCode = oSegment.get_DataElementValue(6); // Entity Identifier Code (98) 
                                    #endregion " Payee N1 Segment "
                                }
                                else if (sSegmentID == "N3")
                                {
                                    #region " Payee N3 Segment "
                                    oPayerPayeeIdent.N301_AddrInfo = oSegment.get_DataElementValue(1);
                                    oPayerPayeeIdent.N302_AddrInfo = oSegment.get_DataElementValue(2);   // Address Information (166) 
                                    #endregion " Payee N3 Segment "
                                }
                                else if (sSegmentID == "N4")
                                {
                                    #region " Payee N4 Segment "
                                    oPayerPayeeIdent.N401_CityName = oSegment.get_DataElementValue(1); // City
                                    oPayerPayeeIdent.N402_StateOrProvinceCode = oSegment.get_DataElementValue(2); // State
                                    oPayerPayeeIdent.N403_PostalCode = oSegment.get_DataElementValue(3); // Zip
                                    oPayerPayeeIdent.N404_CountryCode = oSegment.get_DataElementValue(4);  // Country Code (26) 
                                    oPayerPayeeIdent.N405_LocationQual = oSegment.get_DataElementValue(5); // Location Qualifier (309) 
                                    oPayerPayeeIdent.N406_LocationID = oSegment.get_DataElementValue(6);  // Location Identifier (310) 
                                    oPayerPayeeIdent.N407_CountrySubDivisionCode = oSegment.get_DataElementValue(7);  
                                    #endregion " Payee N4 Segment "
                                }
                                else if (sSegmentID == "RDM")
                                {
                                    #region " Receiver REF Segment "
                                    oRDM = new RDM();

                                    oRDM.RDM01_ReportTranCode = oSegment.get_DataElementValue(1);
                                    oRDM.RDM02_Name = oSegment.get_DataElementValue(2);
                                    oRDM.RDM03_CommunicationNumber = oSegment.get_DataElementValue(3);
                                    oRDM.RDM04_REF_ID = oSegment.get_DataElementValue(4);
                                    oRDM.RDM05_REF_ID = oSegment.get_DataElementValue(5);

                                    oBPR.oRDMs.Add(oRDM);
                                    _IDcount += 1;
                                    #endregion " Payer REF Segment "
                                }
                            }//sEntity
                            
                        }//sLoopSection

                    } // nArea == 1

                    else if (nArea == 2)
                    {
                        if (sSegmentID == "LX")
                        {
                            sLXID = oSegment.get_DataElementValue(1);
                        }

                        //if (sLXID == "1" || sLXID == "0")//It was "961221" changed to "1"
                        //{
                        if (sLoopSection == "LX")
                        {
                            if (sSegmentID == "TS3")
                            {
                                #region " TS3 Segment "
                                string str = oSegment.get_DataElementValue(16);
                                string str1 = oSegment.get_DataElementValue(19);
                                sValue = oSegment.get_DataElementValue(16);//oClaim.TotalCoinsuranceAmount = "10";//Added By MaheshB
                                sValue = oSegment.get_DataElementValue(19);//oClaim.TotalDeductibleAmount = "2";
                                //list835Data.Items.Add("HospProviderNo (LX):  " + oSegment.get_DataElementValue(1));
                                //list835Data.Items.Add("InFacilityType:  " + oSegment.get_DataElementValue(2));
                                //list835Data.Items.Add("InpatientClaim:  " + oSegment.get_DataElementValue(4));
                                //list835Data.Items.Add("InTotalCharges:  " + oSegment.get_DataElementValue(5));
                                //list835Data.Items.Add("InPaidAmount:  " + oSegment.get_DataElementValue(9));
                                //list835Data.Items.Add("InAdjustment:  " + oSegment.get_DataElementValue(11));
                                #endregion " TS3 Segment "
                            }
                            else if (sSegmentID == "TS2")
                            {
                                #region " TS2 Segment "
                                //list835Data.Items.Add("DiagRelatedGroupAmnt:  " + oSegment.get_DataElementValue(1));
                                //list835Data.Items.Add("FedSpecAmnt:  " + oSegment.get_DataElementValue(2));
                                //list835Data.Items.Add("DisproportionShareAmnt:  " + oSegment.get_DataElementValue(4));
                                //list835Data.Items.Add("CapitalAmnt:  " + oSegment.get_DataElementValue(5));
                                //list835Data.Items.Add("IndirectMedEduAmnt:  " + oSegment.get_DataElementValue(6));
                                #endregion " TS2 Segment "
                            }
                        }
                        else if (sLoopSection == "LX;CLP")
                        {
                            if (sSegmentID == "CLP")
                            {
                                _Index = 0;
                                if (_nArea2RowCount == 0)
                                {
                                    _nArea2RowCount++;
                                    i++;
                                }
                                #region " CLP Segment "
                                oCLP = new CLP();
                                try
                                {
                                    if (gloGlobal.gloPMGlobal.IsUseClaimPrefix && gloGlobal.gloPMGlobal.sClaimPrefix != "" && Convert.ToString(oSegment.get_DataElementValue(1)).Contains(gloGlobal.gloPMGlobal.sClaimPrefix))
                                    {
                                        oCLP.CLP01_ClaimSubmitterID = Convert.ToString(oSegment.get_DataElementValue(1)).Substring(gloGlobal.gloPMGlobal.sClaimPrefix.Length, (Convert.ToString(oSegment.get_DataElementValue(1)).Length - gloGlobal.gloPMGlobal.sClaimPrefix.Length));
                                    }
                                    else
                                    {
                                        oCLP.CLP01_ClaimSubmitterID = oSegment.get_DataElementValue(1); // Claim Number
                                    }
                                }
                                catch
                                {
                                    oCLP.CLP01_ClaimSubmitterID = oSegment.get_DataElementValue(1);
                                }
                                oCLP.CLP02_ClaimStatusCode = oSegment.get_DataElementValue(2);  // Claim Status Code (1029) 
                                oCLP.CLP03_Amount = oSegment.get_DataElementValue(3); // Total Claim Amount
                                oCLP.CLP04_Amount = oSegment.get_DataElementValue(4); // Claim Payement Amount
                                oCLP.CLP05_Amount = oSegment.get_DataElementValue(5);   // Monetary Amount (782) 
                                oCLP.CLP06_ClaimFilingIndicatorCode = oSegment.get_DataElementValue(6);   // Claim Filing Indicator Code (1032) 
                                oCLP.CLP07_Ref_ID = oSegment.get_DataElementValue(7); // Payer Control Number
                                oCLP.CLP08_FacilityCodeValue = oSegment.get_DataElementValue(8);
                                oCLP.CLP09_ClaimFrequencyTypeCode = oSegment.get_DataElementValue(9);
                                oCLP.CLP10_PatientStatusCode = oSegment.get_DataElementValue(10);   // Patient Status Code (1352) 
                                oCLP.CLP11_DiagRelatedGroupCode = oSegment.get_DataElementValue(11);  // Diagnosis Related Group (DRG) Code (1354) 
                                oCLP.CLP12_Qty = oSegment.get_DataElementValue(12);  //Quantity (380) 
                                oCLP.CLP13_Percent = oSegment.get_DataElementValue(13); // Percent (954) 

                                oBPR.oCLPs.Add(oCLP);
                                _IDcount += 1;
                                //_LatestSegment = SEG_CLP;
                                #endregion " CLP Segment "
                            }
                            else if (sSegmentID == "CAS")
                            {
                                #region " CAS Segment "

                                oCAS = new CAS();
                                oCAS.CAS01_ClaimAdjustGroupCode = oSegment.get_DataElementValue(1);
                                oCAS.CAS02_ClaimAdjustReasonCode = oSegment.get_DataElementValue(2);
                                oCAS.CAS03_Amount = oSegment.get_DataElementValue(3);
                                oCAS.CAS04_Qty = oSegment.get_DataElementValue(4);
                                oCAS.CAS05_ClaimAdjustReasonCode = oSegment.get_DataElementValue(5);
                                oCAS.CAS06_Amount = oSegment.get_DataElementValue(6);
                                oCAS.CAS07_Qty = oSegment.get_DataElementValue(7);
                                oCAS.CAS08_ClaimAdjustReasonCode = oSegment.get_DataElementValue(8);
                                oCAS.CAS09_Amount = oSegment.get_DataElementValue(9);
                                oCAS.CAS10_Qty = oSegment.get_DataElementValue(10);
                                oCAS.CAS11_ClaimAdjustReasonCode = oSegment.get_DataElementValue(11);
                                oCAS.CAS12_Amount = oSegment.get_DataElementValue(12);
                                oCAS.CAS13_Qty = oSegment.get_DataElementValue(13);
                                oCAS.CAS14_ClaimAdjustReasonCode = oSegment.get_DataElementValue(14);
                                oCAS.CAS15_Amount = oSegment.get_DataElementValue(15);
                                oCAS.CAS16_Qty = oSegment.get_DataElementValue(16);
                                oCAS.CAS17_ClaimAdjustReasonCode = oSegment.get_DataElementValue(17);
                                oCAS.CAS18_Amount = oSegment.get_DataElementValue(18);
                                oCAS.CAS19_Qty = oSegment.get_DataElementValue(19);

                                oCAS.CASType = enumCAS_Type.ClaimAdjustment;
                                oCLP.oCASs.Add(oCAS);
                                _IDcount += 1;

                                #endregion " CAS Segment "
                            }
                            else if (sSegmentID == "NM1")
                            {
                                #region " Patient NM1 Segment "
                                oNM1 = new NM1();
                                oNM1.NM101_EntityIDCode = oSegment.get_DataElementValue(1);   // Entity Identifier Code (98) 
                                oNM1.NM102_EntityTypeQual = oSegment.get_DataElementValue(2);   // Entity Type Qualifier (1065) 
                                oNM1.NM103_NameLastOrOrgName = oSegment.get_DataElementValue(3); // Patient Last name
                                oNM1.NM104_NameFirst = oSegment.get_DataElementValue(4); // Patient Last name
                                oNM1.NM105_NameMiddle = oSegment.get_DataElementValue(5); // Patient Last name
                                oNM1.NM106_NamePrefix = oSegment.get_DataElementValue(6);  // Name Prefix (1038) 
                                oNM1.NM107_NameSuffix = oSegment.get_DataElementValue(7);   // Name Suffix (1039) 
                                oNM1.NM108_IDCodeQual = oSegment.get_DataElementValue(8);  // Identification Code Qualifier (66) 
                                oNM1.NM109_IDCode = oSegment.get_DataElementValue(9); // PatientID
                                oNM1.NM110_EntityRelationCode = oSegment.get_DataElementValue(10); // Entity Relationship Code (706) 
                                oNM1.NM111_EntityIDCode = oSegment.get_DataElementValue(11); // Entity Identifier Code (98) 

                                _Temp = oNM1.NM101_EntityIDCode;

                                if (_Temp == "QC")
                                    oNM1.NM1Type = enumNM1_Type.PatientName;
                                else if (_Temp == "IL")
                                    oNM1.NM1Type = enumNM1_Type.InsuredName;
                                else if (_Temp == "74")
                                    oNM1.NM1Type = enumNM1_Type.CorrectedPatientOrInsuredName;
                                else if (_Temp == "82")
                                    oNM1.NM1Type = enumNM1_Type.ServiceProviderName;
                                else if (_Temp == "TT")
                                    oNM1.NM1Type = enumNM1_Type.CrossoverCarrierName;
                                else if (_Temp == "PR")
                                    oNM1.NM1Type = enumNM1_Type.CorrectedPriorityPayerName;

                                _Temp = "";
                                oCLP.oNM1s.Add(oNM1);
                                _IDcount += 1;

                                #endregion " Patient NM1 Segment "
                            }
                            else if (sSegmentID == "MIA")
                            {
                                #region " MIA Segment "
                                oMIA = new MIA();
                                oMIA.MIA01_Qty = oSegment.get_DataElementValue(1);
                                oMIA.MIA02_Qty = oSegment.get_DataElementValue(2);
                                oMIA.MIA03_Qty = oSegment.get_DataElementValue(3);
                                oMIA.MIA04_Amount = oSegment.get_DataElementValue(4);
                                oMIA.MIA05_Ref_ID = oSegment.get_DataElementValue(5);
                                oMIA.MIA06_Amount = oSegment.get_DataElementValue(6);
                                oMIA.MIA07_Amount = oSegment.get_DataElementValue(7);
                                oMIA.MIA08_Amount = oSegment.get_DataElementValue(8);
                                oMIA.MIA09_Amount = oSegment.get_DataElementValue(9);
                                oMIA.MIA10_Amount = oSegment.get_DataElementValue(10);
                                oMIA.MIA11_Amount = oSegment.get_DataElementValue(11);
                                oMIA.MIA12_Amount = oSegment.get_DataElementValue(12);
                                oMIA.MIA13_Amount = oSegment.get_DataElementValue(13);
                                oMIA.MIA14_Amount = oSegment.get_DataElementValue(14);
                                oMIA.MIA15_Qty = oSegment.get_DataElementValue(15);
                                oMIA.MIA16_Amount = oSegment.get_DataElementValue(16);
                                oMIA.MIA17_Amount = oSegment.get_DataElementValue(17);
                                oMIA.MIA18_Amount = oSegment.get_DataElementValue(18);
                                oMIA.MIA19_Amount = oSegment.get_DataElementValue(19);
                                oMIA.MIA20_Ref_ID = oSegment.get_DataElementValue(20);
                                oMIA.MIA21_Ref_ID = oSegment.get_DataElementValue(21);
                                oMIA.MIA22_Ref_ID = oSegment.get_DataElementValue(22);
                                oMIA.MIA23_Ref_ID = oSegment.get_DataElementValue(23);
                                oMIA.MIA24_Amount = oSegment.get_DataElementValue(24);
                                oCLP.oMIAs.Add(oMIA);
                                _IDcount += 1;
                                #endregion
                            }
                            else if (sSegmentID == "MOA")
                            {
                                #region " MOA Segment "
                                oMOA = new MOA();
                                oMOA.MOA01_Percent = oSegment.get_DataElementValue(1);
                                oMOA.MOA02_Amount = oSegment.get_DataElementValue(2);
                                oMOA.MOA03_Ref_ID = oSegment.get_DataElementValue(3);
                                oMOA.MOA04_Ref_ID = oSegment.get_DataElementValue(4);
                                oMOA.MOA05_Ref_ID = oSegment.get_DataElementValue(5);
                                oMOA.MOA06_Ref_ID = oSegment.get_DataElementValue(6);
                                oMOA.MOA07_Ref_ID = oSegment.get_DataElementValue(7);
                                oMOA.MOA08_Amount = oSegment.get_DataElementValue(8);
                                oMOA.MOA09_Amount = oSegment.get_DataElementValue(9);
                                oCLP.oMOAs.Add(oMOA);
                                _IDcount += 1;
                                #endregion
                            }
                            else if (sSegmentID == "REF")//Rendering Provider IDENTIFICATION or Other claim related identification
                            {
                                #region " Rendering Provider / Other Claim Related Info REF Segment "

                                oREF = new REF();
                                oREF.REF01_Ref_IDQual = oSegment.get_DataElementValue(1);
                                oREF.REF02_Ref_ID = oSegment.get_DataElementValue(2);
                                oREF.REF03_Desc = oSegment.get_DataElementValue(3);
                                oREF.REF04_Ref_ID = oSegment.get_DataElementValue(4);

                                _Temp = oREF.REF01_Ref_IDQual;

                                if (_Temp == "1L" || _Temp == "1W" || _Temp == "9A" || _Temp == "9C" || _Temp == "A6" ||
                                    _Temp == "BB" || _Temp == "CE" || _Temp == "EA" || _Temp == "F8" || _Temp == "G1" ||
                                    _Temp == "G3" || _Temp == "IG" || _Temp == "SY")
                                {
                                    oREF.REFType = enumREF_Type.OtherClaimRelatedID;
                                }
                                else if (_Temp == "1A" || _Temp == "1B" || _Temp == "1C" || _Temp == "1D" || _Temp == "1G" ||
                                    _Temp == "1H" || _Temp == "D3" || _Temp == "G2")
                                {
                                    oREF.REFType = enumREF_Type.RenderingProviderID;
                                }

                                _Temp = "";
                                oCLP.oREFs.Add(oREF);
                                _IDcount += 1;
                                #endregion

                            }
                            else if (sSegmentID == "DTM")
                            {
                                #region " Claim Dates DTM Segment "
                                oDTM = new DTM();
                                oDTM.DTM01_DateTimeQual = oSegment.get_DataElementValue(1);
                                oDTM.DTM02_Date = oSegment.get_DataElementValue(2);
                                oDTM.DTM03_Time = oSegment.get_DataElementValue(3);
                                oDTM.DTM04_TimeCode = oSegment.get_DataElementValue(4);
                                oDTM.DTM05_DateTimePeriodFormatQual = oSegment.get_DataElementValue(5);
                                oDTM.DTM06_DateTimePeriod = oSegment.get_DataElementValue(6);
                                oDTM.DTMType = enumDTM_Type.ClaimDate;

                                oCLP.oDTMs.Add(oDTM);
                                _IDcount += 1;
                                #endregion " Claim Dates DTM Segment "
                            }
                            else if (sSegmentID == "AMT")
                            {
                                #region " Claim Supplemental Info AMT Segment "
                                oAMT = new AMT();
                                oAMT.AMT01_AmountQualCode = oSegment.get_DataElementValue(1);
                                oAMT.AMT02_Amount = oSegment.get_DataElementValue(2);
                                oAMT.AMT03_CreditDebitFlagCode = oSegment.get_DataElementValue(3);

                                oAMT.AMTType = enumAMT_Type.ClaimSupplementalInfo;
                                oCLP.oAMTs.Add(oAMT);
                                _IDcount += 1;
                                #endregion " Claim Supplemental Info AMT Segment "
                            }
                            else if (sSegmentID == "PER")
                            {
                                #region " Claim Contact Information "
                                oPER = new PER();
                                oPER.PER01_Cont_Func_Code = oSegment.get_DataElementValue(1);
                                oPER.PER02_Name = oSegment.get_DataElementValue(2);
                                oPER.PER03_CommNoQual = oSegment.get_DataElementValue(3);
                                oPER.PER04_CommNo = oSegment.get_DataElementValue(4);
                                oPER.PER05_CommNoQual = oSegment.get_DataElementValue(5);
                                oPER.PER06_CommNo = oSegment.get_DataElementValue(6);
                                oPER.PER07_CommNoQual = oSegment.get_DataElementValue(7);
                                oPER.PER08_CommNo = oSegment.get_DataElementValue(8);
                                oPER.PER09_Cont_Inq_Ref = oSegment.get_DataElementValue(9);
                                oPER.PERType = enumPER_Type.ClaimContactInfo;
                                oCLP.oPERs.Add(oPER);
                                _IDcount += 1;
                                #endregion
                            }
                        }
                        else if (sLoopSection == "LX;CLP;SVC")
                        {
                            if (sSegmentID == "SVC")
                            {
                                _Index++;
                                _nArea2RowCount = 0;

                                #region " Claim Service Line SVC Segment "

                                oSVC = new SVC();
                                oSVC.SVC01_CompositeMedicalProc = oSegment.get_DataElementValue(1);
                                oSVC.SVC01_1_ProductServiceIDQual = oSegment.get_DataElementValue(1, 1);
                                oSVC.SVC01_2_ProductServiceID = oSegment.get_DataElementValue(1, 2); // Service Procedure code
                                oSVC.SVC01_3_ProcMod = oSegment.get_DataElementValue(1, 3); // Service Modifier 1
                                oSVC.SVC01_4_ProcMod = oSegment.get_DataElementValue(1, 4); // Service Modifier 2
                                oSVC.SVC01_5_ProcMod = oSegment.get_DataElementValue(1, 5);
                                oSVC.SVC01_6_ProcMod = oSegment.get_DataElementValue(1, 6);
                                oSVC.SVC01_7_Desc = oSegment.get_DataElementValue(1, 7);
                                oSVC.SVC02_Amount = oSegment.get_DataElementValue(2);
                                oSVC.SVC03_Amount = oSegment.get_DataElementValue(3);
                                oSVC.SVC04_ProductServiceID = oSegment.get_DataElementValue(4);
                                oSVC.SVC05_Qty = oSegment.get_DataElementValue(5);
                                oSVC.SVC06_CompositeMedicalProc = oSegment.get_DataElementValue(6);
                                oSVC.SVC06_1_ProductServiceIDQual = oSegment.get_DataElementValue(6, 1);
                                oSVC.SVC06_2_ProductServiceID = oSegment.get_DataElementValue(6, 2);
                                oSVC.SVC06_3_ProcMod = oSegment.get_DataElementValue(6, 3);
                                oSVC.SVC06_4_ProcMod = oSegment.get_DataElementValue(6, 4);
                                oSVC.SVC06_5_ProcMod = oSegment.get_DataElementValue(6, 5);
                                oSVC.SVC06_6_ProcMod = oSegment.get_DataElementValue(6, 6);
                                oSVC.SVC06_7_Desc = oSegment.get_DataElementValue(6, 7);
                                oSVC.SVC07_Qty = oSegment.get_DataElementValue(7);

                                oCLP.oSVCs.Add(oSVC);
                                _IDcount += 1;
                                #endregion " Claim Service Line SVC Segment "

                            }
                            else if (sSegmentID == "DTM")
                            {
                                #region " Claim Service Line Date DTM Segment "

                                oDTM = new DTM();
                                oDTM.DTM01_DateTimeQual = oSegment.get_DataElementValue(1);
                                oDTM.DTM02_Date = oSegment.get_DataElementValue(2);
                                oDTM.DTM03_Time = oSegment.get_DataElementValue(3);
                                oDTM.DTM04_TimeCode = oSegment.get_DataElementValue(4);
                                oDTM.DTM05_DateTimePeriodFormatQual = oSegment.get_DataElementValue(5);
                                oDTM.DTM06_DateTimePeriod = oSegment.get_DataElementValue(6);
                                oDTM.DTMType = enumDTM_Type.ServiceDate;

                                oSVC.oDTMs.Add(oDTM);
                                _IDcount += 1;
                                #endregion " Claim Service Line Date DTM Segment "
                            }
                            else if (sSegmentID == "CAS")
                            {
                                #region " Claim Service Line CAS Segment "

                                oCAS = new CAS();
                                oCAS.CAS01_ClaimAdjustGroupCode = oSegment.get_DataElementValue(1);
                                oCAS.CAS02_ClaimAdjustReasonCode = oSegment.get_DataElementValue(2);
                                oCAS.CAS03_Amount = oSegment.get_DataElementValue(3);
                                oCAS.CAS04_Qty = oSegment.get_DataElementValue(4);
                                oCAS.CAS05_ClaimAdjustReasonCode = oSegment.get_DataElementValue(5);
                                oCAS.CAS06_Amount = oSegment.get_DataElementValue(6);
                                oCAS.CAS07_Qty = oSegment.get_DataElementValue(7);
                                oCAS.CAS08_ClaimAdjustReasonCode = oSegment.get_DataElementValue(8);
                                oCAS.CAS09_Amount = oSegment.get_DataElementValue(9);
                                oCAS.CAS10_Qty = oSegment.get_DataElementValue(10);
                                oCAS.CAS11_ClaimAdjustReasonCode = oSegment.get_DataElementValue(11);
                                oCAS.CAS12_Amount = oSegment.get_DataElementValue(12);
                                oCAS.CAS13_Qty = oSegment.get_DataElementValue(13);
                                oCAS.CAS14_ClaimAdjustReasonCode = oSegment.get_DataElementValue(14);
                                oCAS.CAS15_Amount = oSegment.get_DataElementValue(15);
                                oCAS.CAS16_Qty = oSegment.get_DataElementValue(16);
                                oCAS.CAS17_ClaimAdjustReasonCode = oSegment.get_DataElementValue(17);
                                oCAS.CAS18_Amount = oSegment.get_DataElementValue(18);
                                oCAS.CAS19_Qty = oSegment.get_DataElementValue(19);

                                oCAS.CASType = enumCAS_Type.ServiceAdjustment;

                                oSVC.oCASs.Add(oCAS);
                                _IDcount += 1;

                                #endregion " Claim Service Line CAS Segment "
                            }
                            else if (sSegmentID == "AMT")
                            {
                                #region " Claim Service Line AMT Segment "

                                oAMT = new AMT();
                                oAMT.AMT01_AmountQualCode = oSegment.get_DataElementValue(1);
                                oAMT.AMT02_Amount = oSegment.get_DataElementValue(2);
                                oAMT.AMT03_CreditDebitFlagCode = oSegment.get_DataElementValue(3);

                                oAMT.AMTType = enumAMT_Type.ServiceSupplementalAmount;
                                oSVC.oAMTs.Add(oAMT);
                                _IDcount += 1;
                                #endregion " Claim Service Line AMT Segment "
                            }
                            else if (sSegmentID == "REF")
                            {
                                #region " Claim Service Line REF Segment "

                                oREF = new REF();
                                oREF.REF01_Ref_IDQual = oSegment.get_DataElementValue(1);
                                oREF.REF02_Ref_ID = oSegment.get_DataElementValue(2);
                                oREF.REF03_Desc = oSegment.get_DataElementValue(3);
                                oREF.REF04_Ref_ID = oSegment.get_DataElementValue(4);

                                _Temp = oREF.REF01_Ref_IDQual;

                                if (_Temp == "1S" || _Temp == "6R" || _Temp == "BB" || _Temp == "E9" || _Temp == "G1" ||
                                    _Temp == "G3" || _Temp == "LU" || _Temp == "RB")
                                {
                                    oREF.REFType = enumREF_Type.ServiceID;
                                }
                                else if (_Temp == "1A" || _Temp == "1B" || _Temp == "1C" || _Temp == "1D" || _Temp == "1G" ||
                                   _Temp == "1H" || _Temp == "1J" || _Temp == "HPI" || _Temp == "SY" || _Temp == "TJ")
                                {
                                    oREF.REFType = enumREF_Type.RenderingProviderInfo;
                                }

                                _Temp = "";
                                oSVC.oREFs.Add(oREF);
                                _IDcount += 1;
                                #endregion " Claim Service Line REF Segment "
                            }
                            else if (sSegmentID == "LQ")
                            {
                                #region " LQ Segment "
                                oLQ = new LQ();
                                oLQ.LQ01_CodeListQualCode = oSegment.get_DataElementValue(1);
                                oLQ.LQ02_IndustryCode = oSegment.get_DataElementValue(2);

                                oSVC.oLQs.Add(oLQ);
                                _IDcount += 1;
                                #endregion " LQ Segment "
                            }
                        }

                        #region " Commented Code "
                        //}
                        //else if (sLXID == "")// It was 961213
                        //{
                        //    #region " Commented Code "
                        //    //if (sLoopSection == "LX")
                        //    //{
                        //    //    if (sSegmentID == "TS3")
                        //    //    {
                        //    //        //list835Data.Items.Add("HospProviderNo (LX):  " + oSegment.get_DataElementValue(1));
                        //    //        //list835Data.Items.Add("InFacilityType:  " + oSegment.get_DataElementValue(2));
                        //    //        //list835Data.Items.Add("InpatientClaim:  " + oSegment.get_DataElementValue(4));
                        //    //        //list835Data.Items.Add("InTotalCharges:  " + oSegment.get_DataElementValue(5));
                        //    //        //list835Data.Items.Add("InPaidAmount:  " + oSegment.get_DataElementValue(9));
                        //    //        //list835Data.Items.Add("InAdjustment:  " + oSegment.get_DataElementValue(11));
                        //    //    }
                        //    //    else if (sSegmentID == "TS2")
                        //    //    {
                        //    //        //list835Data.Items.Add("DiagRelatedGroupAmnt:  " + oSegment.get_DataElementValue(1));
                        //    //        //list835Data.Items.Add("FedSpecAmnt:  " + oSegment.get_DataElementValue(2));
                        //    //        //list835Data.Items.Add("DisproportionShareAmnt:  " + oSegment.get_DataElementValue(4));
                        //    //        //list835Data.Items.Add("CapitalAmnt:  " + oSegment.get_DataElementValue(5));
                        //    //        //list835Data.Items.Add("IndirectMedEduAmnt:  " + oSegment.get_DataElementValue(6));
                        //    //    }
                        //    //}
                        //    //else if (sLoopSection == "LX;CLP")
                        //    //{
                        //    //    if (sSegmentID == "CLP")
                        //    //    {
                        //    //        oPatientRemit = new PatientRemit();
                        //    //        if (_nArea2RowCount == 0)
                        //    //        {
                        //    //            C1Claim835Data.Rows.Add();
                        //    //            rowIndex = C1Claim835Data.Rows.Count - 1;
                        //    //            _nArea2RowCount++;
                        //    //            i++;
                        //    //        }
                        //    //        oPatientRemit.PatientControlNo = oSegment.get_DataElementValue(1);
                        //    //        oPatientRemit.ClaimStatus = oSegment.get_DataElementValue(2);
                        //    //        oPatientRemit.TotalClaimAmount = oSegment.get_DataElementValue(3);
                        //    //        oPatientRemit.ClaimPaymentAmount = oSegment.get_DataElementValue(4);
                        //    //        sValue = oSegment.get_DataElementValue(5);   // Monetary Amount (782) 
                        //    //        sValue = oSegment.get_DataElementValue(6);   // Claim Filing Indicator Code (1032) 
                        //    //        oPatientRemit.PayerControlNumber = oSegment.get_DataElementValue(7);
                        //    //        sValue = oSegment.get_DataElementValue(10);   // Patient Status Code (1352) 
                        //    //        sValue = oSegment.get_DataElementValue(11);  // Diagnosis Related Group (DRG) Code (1354) 
                        //    //        sValue = oSegment.get_DataElementValue(12);  //Quantity (380) 
                        //    //        sValue = oSegment.get_DataElementValue(13); // Percent (954) 
                        //    //    }
                        //    //    else if (sSegmentID == "CAS")
                        //    //    {
                        //    //        if (oSegment.get_DataElementValue(1) == "CO")
                        //    //        {
                        //    //            sValue = oSegment.get_DataElementValue(2);  // Claim Adjustment Reason Code (1034) 
                        //    //            oPatientRemit.ContractualObligation = oSegment.get_DataElementValue(3);
                        //    //            sValue = oSegment.get_DataElementValue(4);   // Quantity (380) 
                        //    //        }
                        //    //        else if (oSegment.get_DataElementValue(1) == "PR")
                        //    //        {
                        //    //            sValue = oSegment.get_DataElementValue(2);  // 
                        //    //            oPatientRemit.PatientResposibility = oSegment.get_DataElementValue(3);
                        //    //            sValue = oSegment.get_DataElementValue(4);   // 
                        //    //        }
                        //    //        else if (oSegment.get_DataElementValue(1) == "PI")
                        //    //        {
                        //    //            sValue = oSegment.get_DataElementValue(2);  // 
                        //    //            oPatientRemit.ContractualObligation = oSegment.get_DataElementValue(3);
                        //    //            sValue = oSegment.get_DataElementValue(4);   // 
                        //    //        }
                        //    //        else if (oSegment.get_DataElementValue(1) == "OA")
                        //    //        {
                        //    //            sValue = oSegment.get_DataElementValue(2);  // 
                        //    //            oPatientRemit.OtherAdjustments = oSegment.get_DataElementValue(3);
                        //    //            sValue = oSegment.get_DataElementValue(4);   // 
                        //    //        }

                        //    //    }
                        //    //    else if (sSegmentID == "NM1")
                        //    //    {
                        //    //        if (oSegment.get_DataElementValue(1) == "QC")
                        //    //        {
                        //    //            sValue = oSegment.get_DataElementValue(1);   // Entity Identifier Code (98) 
                        //    //            sValue = oSegment.get_DataElementValue(2);   // Entity Type Qualifier (1065) 
                        //    //            oPatientRemit.PatientLName=oSegment.get_DataElementValue(3).ToString() + " " + oSegment.get_DataElementValue(4).ToString() + " " + oSegment.get_DataElementValue(5);
                        //    //            sValue = oSegment.get_DataElementValue(6);  // Name Prefix (1038) 
                        //    //            sValue = oSegment.get_DataElementValue(7);   // Name Suffix (1039) 
                        //    //            sValue = oSegment.get_DataElementValue(8);  // Identification Code Qualifier (66) 
                        //    //            oPatientRemit.PatientID=oSegment.get_DataElementValue(9).ToString();
                        //    //            sValue = oSegment.get_DataElementValue(10); // Entity Relationship Code (706) 
                        //    //            sValue = oSegment.get_DataElementValue(11); // Entity Identifier Code (98) 
                        //    //        }
                        //    //        if (oSegment.get_DataElementValue(1) == "IL")
                        //    //        {
                        //    //            sValue = oSegment.get_DataElementValue(1);   // Entity Identifier Code (98) 
                        //    //            sValue = oSegment.get_DataElementValue(2);   // Entity Type Qualifier (1065) 
                        //    //            oPatientRemit.SubscriberLName = oSegment.get_DataElementValue(3).ToString() + " " + oSegment.get_DataElementValue(4).ToString() + " " + oSegment.get_DataElementValue(5);
                        //    //            sValue = oSegment.get_DataElementValue(6);  // Name Prefix (1038) 
                        //    //            sValue = oSegment.get_DataElementValue(7);   // Name Suffix (1039) 
                        //    //            sValue = oSegment.get_DataElementValue(8);  // Identification Code Qualifier (66) 
                        //    //            oPatientRemit.SubscriberID = oSegment.get_DataElementValue(9).ToString();
                        //    //            sValue = oSegment.get_DataElementValue(10); // Entity Relationship Code (706) 
                        //    //            sValue = oSegment.get_DataElementValue(11); // Entity Identifier Code (98) 
                        //    //        }
                        //    //    }
                        //    //    else if (sSegmentID == "MIA")
                        //    //    {
                        //    //        if (Convert.ToInt32((oSegment.get_DataElementValue(1))) == 0)
                        //    //        {
                        //    //        }
                        //    //    }
                        //    //    else if (sSegmentID == "REF")//Rendering Provider IDENTIFICATION or Other claim related identification
                        //    //    {
                        //    //        if (oSegment.get_DataElementValue(1) == "1A")//Blue Cross Provider Number
                        //    //        {
                        //    //            oSegment.get_DataElementValue(2).ToString();
                        //    //        }
                        //    //        else if (oSegment.get_DataElementValue(1) == "1B")//Blue Shield Provider Number
                        //    //        {
                        //    //            oSegment.get_DataElementValue(2).ToString();
                        //    //        }
                        //    //        else if (oSegment.get_DataElementValue(1) == "1C")//Medicare Provider Number
                        //    //        {
                        //    //            oSegment.get_DataElementValue(2).ToString();
                        //    //        }
                        //    //        else if (oSegment.get_DataElementValue(1) == "1D")//Medicaid Provider Number
                        //    //        {
                        //    //            oSegment.get_DataElementValue(2).ToString();
                        //    //        }
                        //    //        else if (oSegment.get_DataElementValue(1) == "1G")//Provider UPIN Number
                        //    //        {
                        //    //            oSegment.get_DataElementValue(2).ToString();
                        //    //        }
                        //    //        else if (oSegment.get_DataElementValue(1) == "1H")//CHAMPUS Identification Number
                        //    //        {
                        //    //            oSegment.get_DataElementValue(2).ToString();
                        //    //        }
                        //    //        else if (oSegment.get_DataElementValue(1) == "G2")//Provider Commercial Number
                        //    //        {
                        //    //            oSegment.get_DataElementValue(2).ToString();
                        //    //        }

                        //    //        //For Claim Related Identification
                        //    //        else if (oSegment.get_DataElementValue(1) == "1L")//Group or Policy Number
                        //    //        {
                        //    //            oSegment.get_DataElementValue(2).ToString();
                        //    //        }
                        //    //        else if (oSegment.get_DataElementValue(1) == "1W")//Member identification number
                        //    //        {
                        //    //            oSegment.get_DataElementValue(2).ToString();
                        //    //        }
                        //    //        else if (oSegment.get_DataElementValue(1) == "9A")//Repriced claim reference number
                        //    //        {
                        //    //            oSegment.get_DataElementValue(2).ToString();
                        //    //        }
                        //    //        else if (oSegment.get_DataElementValue(1) == "9C")//Adjusted Repriced claim reference number
                        //    //        {
                        //    //            oSegment.get_DataElementValue(2).ToString();
                        //    //        }
                        //    //        else if (oSegment.get_DataElementValue(1) == "A6")//Employee identification number
                        //    //        {
                        //    //            oSegment.get_DataElementValue(2).ToString();
                        //    //        }
                        //    //        else if (oSegment.get_DataElementValue(1) == "BB")//Authorization Number
                        //    //        {
                        //    //            oSegment.get_DataElementValue(2).ToString();
                        //    //        }
                        //    //        else if (oSegment.get_DataElementValue(1) == "EA")//Medical Record identification Number
                        //    //        {
                        //    //            oSegment.get_DataElementValue(2).ToString();
                        //    //        }
                        //    //        else if (oSegment.get_DataElementValue(1) == "F8")//Original Reference Number
                        //    //        {
                        //    //            oSegment.get_DataElementValue(2).ToString();
                        //    //        }
                        //    //        else if (oSegment.get_DataElementValue(1) == "G1")//Prior Authorization Number
                        //    //        {
                        //    //            oSegment.get_DataElementValue(2).ToString();
                        //    //        }
                        //    //        else if (oSegment.get_DataElementValue(1) == "IG")//Insurance Policy Number
                        //    //        {
                        //    //            oSegment.get_DataElementValue(2).ToString();
                        //    //        }
                        //    //        else if (oSegment.get_DataElementValue(1) == "SY")//Social Security Number
                        //    //        {
                        //    //            oSegment.get_DataElementValue(2).ToString();
                        //    //        }
                        //    //    }
                        //    //    else if (sSegmentID == "DTM")
                        //    //    {
                        //    //        if (oSegment.get_DataElementValue(1) == "232")
                        //    //        {
                        //    //            if (oSegment.get_DataElementValue(2) != "")
                        //    //            {
                        //    //                oPatientRemit.ClaimStartDate = gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(oSegment.get_DataElementValue(2).ToString())).ToShortDateString();
                        //    //            }
                        //    //        }
                        //    //        else if (oSegment.get_DataElementValue(1) == "233")
                        //    //        {
                        //    //            if (oSegment.get_DataElementValue(3) != "")
                        //    //            {
                        //    //                oPatientRemit.ClaimEndDate = gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(oSegment.get_DataElementValue(3).ToString())).ToShortDateString();
                        //    //            }

                        //    //        }
                        //    //    }
                        //    //    else if (sSegmentID == "AMT")
                        //    //    {
                        //    //        if (oSegment.get_DataElementValue(1) == "AU")
                        //    //        {
                        //    //            oPatientRemit.CoverageAmount = oSegment.get_DataElementValue(2).ToString();
                        //    //        }
                        //    //        else if (oSegment.get_DataElementValue(1) == "I")
                        //    //        {
                        //    //            oPatientRemit.InterestAmount = oSegment.get_DataElementValue(2).ToString();
                        //    //        }
                        //    //        else if (oSegment.get_DataElementValue(1) == "T")
                        //    //        {
                        //    //            oPatientRemit.TaxAmount = oSegment.get_DataElementValue(2).ToString();
                        //    //        }
                        //    //        else if (oSegment.get_DataElementValue(1) == "F5")
                        //    //        {
                        //    //            oPatientRemit.PatientPaidAmount = oSegment.get_DataElementValue(2).ToString();
                        //    //        }
                        //    //    }
                        //    //    else if (sSegmentID == "MIA")
                        //    //    {
                        //    //        if (Convert.ToInt32((oSegment.get_DataElementValue(1))) == 0)
                        //    //        {
                        //    //        }
                        //    //    }

                        //    //}
                        //    //else if (sLoopSection == "LX;CLP;SVC")
                        //    //{
                        //    //    if (sSegmentID == "SVC")
                        //    //    {
                        //    //        oPatientRemitServiceLine = new PatientRemitServiceLine();
                        //    //        _nArea2RowCount = 0;
                        //    //        if (oSegment.get_DataElementValue(1, 1) == "HC")
                        //    //        {
                        //    //            sValue = oSegment.get_DataElementValue(1, 1);
                        //    //            oPatientRemitServiceLine.ServiceProcedureCode = oSegment.get_DataElementValue(1, 2).ToString();
                        //    //            oPatientRemitServiceLine.ServiceModifier1 = oSegment.get_DataElementValue(1, 3).ToString();
                        //    //            oPatientRemitServiceLine.ServiceModifier2 = oSegment.get_DataElementValue(1, 4).ToString();
                        //    //            oPatientRemitServiceLine.LineItemAmount = oSegment.get_DataElementValue(2).ToString();
                        //    //            oPatientRemitServiceLine.LineProviderPaymentAmount = oSegment.get_DataElementValue(3).ToString();
                        //    //        }
                        //    //    }
                        //    //    else if (sSegmentID == "DTM")
                        //    //    {
                        //    //        if (oSegment.get_DataElementValue(1) == "472")
                        //    //        {
                        //    //            if (oSegment.get_DataElementValue(1) != "")
                        //    //            {
                        //    //                oPatientRemitServiceLine.ServiceDate = gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(oSegment.get_DataElementValue(2))).ToShortDateString();
                        //    //            }
                        //    //        }
                        //    //    }
                        //    //    else if (sSegmentID == "CAS")
                        //    //    {
                        //    //        if (oSegment.get_DataElementValue(1) == "CO")
                        //    //        {
                        //    //            sValue = oSegment.get_DataElementValue(2);  // Claim Adjustment Reason Code (1034) 
                        //    //            oPatientRemitServiceLine.ServiceLineContractualObligation = oSegment.get_DataElementValue(3).ToString();
                        //    //            sValue = oSegment.get_DataElementValue(4);   // Quantity (380) 
                        //    //        }
                        //    //        else if (oSegment.get_DataElementValue(1) == "PR")
                        //    //        {
                        //    //            sValue = oSegment.get_DataElementValue(2);  // 
                        //    //            oPatientRemitServiceLine.ServiceLinePatientResponsibility = oSegment.get_DataElementValue(3).ToString();
                        //    //            sValue = oSegment.get_DataElementValue(4);   // 
                        //    //        }
                        //    //        else if (oSegment.get_DataElementValue(1) == "PI")
                        //    //        {
                        //    //            sValue = oSegment.get_DataElementValue(2);  // 
                        //    //            oPatientRemitServiceLine.ServiceLinePayerInitiatedReduction = oSegment.get_DataElementValue(3).ToString();
                        //    //            sValue = oSegment.get_DataElementValue(4);   // 
                        //    //        }
                        //    //        else if (oSegment.get_DataElementValue(1) == "OA")
                        //    //        {
                        //    //            sValue = oSegment.get_DataElementValue(2);  // 
                        //    //            oPatientRemitServiceLine.ServiceLineOtherAdjustments = oSegment.get_DataElementValue(3).ToString();
                        //    //            sValue = oSegment.get_DataElementValue(4);   // 
                        //    //        }
                        //    //    }
                        //    //    else if (sSegmentID == "AMT")
                        //    //    {
                        //    //        if (oSegment.get_DataElementValue(1) == "B6")
                        //    //        {
                        //    //            oPatientRemitServiceLine.LineAllowedAmount = oSegment.get_DataElementValue(2).ToString();
                        //    //        }
                        //    //    }
                        //    //    else if (sSegmentID == "REF")
                        //    //    {
                        //    //        if (oSegment.get_DataElementValue(1) == "6R")
                        //    //        {
                        //    //            oPatientRemitServiceLine.ServiceProviderControlNo = oSegment.get_DataElementValue(2);
                        //    //        }
                        //    //        if (oSegment.get_DataElementValue(1) == "LU")
                        //    //        {
                        //    //            oPatientRemitServiceLine.ServiceProcedureCode = oSegment.get_DataElementValue(2);
                        //    //        }
                        //    //    }

                        //    //}
                        //    #endregion " Commented Code "
                        #endregion
                        //}

                    }//Area==2
                    else if (nArea == 3)
                    {
                        if (sLoopSection == "")
                        {
                            if (sSegmentID == "PLB")
                            {
                                #region " PLB Segment "

                                oPLB = new PLB();
                                oPLB.PLB01_Ref_ID = oSegment.get_DataElementValue(1);
                                oPLB.PLB02_Date = oSegment.get_DataElementValue(2);
                                oPLB.PLB03_AdjustID = oSegment.get_DataElementValue(3);
                                oPLB.PLB03_1_AdjustReasonCode = oSegment.get_DataElementValue(3, 1);
                                oPLB.PLB03_2_Ref_ID = oSegment.get_DataElementValue(3, 2);
                                oPLB.PLB04_Amount = oSegment.get_DataElementValue(4);
                                oPLB.PLB05_AdjustID = oSegment.get_DataElementValue(5);
                                oPLB.PLB05_1_AdjustReasonCode = oSegment.get_DataElementValue(5, 1);
                                oPLB.PLB05_2_Ref_ID = oSegment.get_DataElementValue(5, 2);
                                oPLB.PLB06_Amount = oSegment.get_DataElementValue(6);
                                oPLB.PLB07_AdjustID = oSegment.get_DataElementValue(7);
                                oPLB.PLB07_1_AdjustReasonCode = oSegment.get_DataElementValue(7, 1);
                                oPLB.PLB07_2_Ref_ID = oSegment.get_DataElementValue(7, 2);
                                oPLB.PLB08_Amount = oSegment.get_DataElementValue(8);
                                oPLB.PLB09_AdjustID = oSegment.get_DataElementValue(9);
                                oPLB.PLB09_1_AdjustReasonCode = oSegment.get_DataElementValue(9, 1);
                                oPLB.PLB09_2_Ref_ID = oSegment.get_DataElementValue(9, 2);
                                oPLB.PLB10_Amount = oSegment.get_DataElementValue(10);
                                oPLB.PLB11_AdjustID = oSegment.get_DataElementValue(11);
                                oPLB.PLB11_1_AdjustReasonCode = oSegment.get_DataElementValue(11, 1);
                                oPLB.PLB11_2_Ref_ID = oSegment.get_DataElementValue(11, 2);
                                oPLB.PLB12_Amount = oSegment.get_DataElementValue(12);
                                oPLB.PLB13_AdjustID = oSegment.get_DataElementValue(13);
                                oPLB.PLB13_1_AdjustReasonCode = oSegment.get_DataElementValue(13, 1);
                                oPLB.PLB13_2_Ref_ID = oSegment.get_DataElementValue(13, 2);
                                oPLB.PLB14_Amount = oSegment.get_DataElementValue(14);

                                oBPR.oPLBs.Add(oPLB);
                                _IDcount += 1;
                                #endregion " PLB Segment "
                            }
                        }
                    }

                    //Get next segment
                    //Use the set method of the object to dispose of the previous instance of the object before instantiation
                    //ediDataSegment.Set(ref oSegment, (ediDataSegment)oSegment.Next());
                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oSegment.Next());
                                       
                    if (oSegment != null)
                    {
                        sSegmentID = oSegment.ID.Trim();
                        sLoopSection = oSegment.LoopSection;
                        _SegmentNo = _SegmentNo + 1;
                    }
                }

                // Checks the 997 acknowledgment file just created.
                // The 997 file is an EDI file, so the logic to read the 997 Functional Acknowledgment file is similar
                // to translating any other EDI file.
                #region " Read Acknowledgement which got created "
                //// Gets the first segment of the 997 acknowledgment file
                //ediDataSegment.Set(ref oSegment, (ediDataSegment)oAck.GetFirst997DataSegment());	//oSegment = (ediDataSegment) oAck.GetFirst997DataSegment();
                //while (oSegment != null)
                //{
                //    nArea = oSegment.Area;
                //    sLoopSection = oSegment.LoopSection;
                //    sSegmentID = oSegment.ID;

                //    if (nArea == 1)
                //    {
                //        if (sLoopSection == "")
                //        {
                //            if (sSegmentID == "AK9")
                //            {
                //                if (oSegment.get_DataElementValue(1, 0) == "R")
                //                {
                //                    //MessageBox.Show("Rejected",_messageBoxCaption,MessageBoxButtons.OK,MessageBoxIcon.Information);
                //                }
                //            }
                //        }	// sLoopSection == ""
                //    }	//nArea == 1
                //    ediDataSegment.Set(ref oSegment, (ediDataSegment)oSegment.Next());	//oSegment = (ediDataSegment) oSegment.Next();
                //}	//oSegment != null
                ////save the acknowledgment
                //FileInfo oFile = new FileInfo(EDIFileName);
                //string _FileName = oFile.Name.Replace(".RMT", "_997.txt");

                //string sDirectoryPath = gloPMClaimGeneral.PM_ClaimManagement_OutBox_997Acknowledgement;
                //if (System.IO.Directory.Exists(sDirectoryPath) == false) { System.IO.Directory.CreateDirectory(sDirectoryPath); }
                //oAck.GetEdiString();
                //oAck.Save(sDirectoryPath + "\\" + _FileName);
                ////System.Collections.ArrayList _remittances = (System.Collections.ArrayList)oPatientRemittances;

                #endregion " Read Acknowledgement which got created "
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(
                    "Error at Segment : " + sSegmentID +
                    ", Segment Number : " + _SegmentNo.ToString() +
                    ", Area : " + nArea.ToString() +
                    ", Loop : " + sLoopSection + ex.ToString(), false);

                oISAs = null;
               
            }
            finally
            {
                //#region " Dispose Objects "
                //if (EdiDoc != null) { EdiDoc.Cancel(); }
                //if (oSegment != null) { oSegment.Dispose(); oSegment = null; }
                //if (oAck != null) { oAck.Dispose(); oAck = null; }
                //if (oSchemas != null) { oSchemas.Dispose(); oSchemas = null; }
                //if (oSchema != null) { oSchema.Dispose(); oSchema = null; }

                //#endregion
            }
            //gloAuditTrail.gloAuditTrail.ExceptionLog("ReadRemittances end", false);
            return oISAs;
        }

        private string IsDuplicateISA(string sISA09, string sISA10, string sISA13)
        {
            string _Result = "";
            Object oResult;
            try
            {
                if (OpenConnection(false))
                {
                    string _Query = "SELECT DBO.ERA_GetFileName(ERA_Files.nERAFileID) " +
                        " FROM ERA_ISA INNER JOIN ERA_Files ON ERA_ISA.nERAFileID = ERA_Files.nERAFileID WHERE " +
                        " sISA09_IntrChngDate = '" + sISA09.Replace("'", "''") + "' AND " +
                        " sISA10_IntrChngTime = '" + sISA10.Replace("'", "''") + "' AND " +
                        " sISA13_IntrChngControlNo = '" + sISA13.Replace("'", "''") + "'";

                    oResult = oDB.ExecuteScalar_Query(_Query);
                    if (oResult != null)
                        _Result = oResult.ToString();
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                CloseConnection();
                oResult = null;
            }
            return _Result;
        }

        private string IsDuplicateFile(string sFileName)
        {
            string _Result = "";
            Object oResult;
            try
            {
                if (OpenConnection(true))
                {
                    if (sFileName != "")
                    {
                        //sFileName = sFileName.Substring(0, sFileName.Length - 4);
                        //string _Query = "SELECT TOP(1) ISNULL(sOriginalFileName,'') FROM ERA_FILES WHERE sOriginalFileName LIKE '" + sFileName.Replace("'", "''") + "%'";

                        oDBPara.Clear();
                        oDBPara.Add("@FileName", sFileName, ParameterDirection.Input, SqlDbType.VarChar);
                        oResult = oDB.ExecuteScalar("ERA_GetDuplicateFile", oDBPara);
                        if (oResult != null)
                            _Result = oResult.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                CloseConnection();
                oResult = null;
            }
            return _Result;
        }

        public string GenerateNewFileName()
        {
            string _Result = "";
            Object oResult;
            try
            {
                if (OpenConnection(false))
                {
                    oResult = oDB.ExecuteScalar("ERA_GenerateFileName");
                    if (oResult != null)
                        _Result = oResult.ToString();
                    CloseConnection();
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            return _Result;
        }

        private void RefreshProgress(ref ProgressBar oProgress, ref Label oLabel, String sProgressText)
        {
            oProgress.Increment(1);
            oLabel.Text = sProgressText;

            oProgress.Refresh();
            oLabel.Refresh();
            oProgress.Parent.Refresh();
            oProgress.Parent.Invalidate();
            oProgress.Parent.Update();

            foreach (Form oForm in Application.OpenForms)
            {
                if (oForm.Name == "frmDashBoardMain" || oForm.Name == "frmERAPayment" || oForm.Name == "frmERAFiles")
                {
                    oForm.Invalidate();
                    oForm.Refresh();
                    oForm.Update();
                }
            }
        }

        #endregion
    }

    #endregion

    #region " Supporting "
    public static class Supporting
    {
        #region " Public Methods "

        public static byte[] ConvertFileToBinary(string sFileName)
        {
            if (File.Exists(sFileName) == false)
                return null;

            try
            {
                FileStream oFile = default(FileStream);
                BinaryReader oReader = default(BinaryReader);

                //'Please uncomment the following line of code to read the file, even the file is in use by same or another process
                //oFile = New FileStream(strFileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite, 8, FileOptions.Asynchronous)

                //'To read the file only when it is not in use by any process
                oFile = new FileStream(sFileName, FileMode.Open, FileAccess.Read);

                oReader = new BinaryReader(oFile);
                byte[] bytesRead = oReader.ReadBytes(Convert.ToInt32(oFile.Length));

                oFile.Flush();
                oFile.Close();
                
                oReader.Close();

                oFile.Dispose();
                oReader.Dispose();

                oFile = null;
                oReader = null;

                return bytesRead;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                return null;
            }

        }

        public static string ConvertBinaryToFile(object cntFromDB, string sFileName)
        {
            if ((cntFromDB != null))
            {
                if (cntFromDB == System.DBNull.Value == false)
                {
                    byte[] content = (byte[])cntFromDB;
                  //  MemoryStream stream = new MemoryStream(content);

                    string _FilePath = GenerateTempFileName(sFileName);

                    System.IO.FileStream oFile = new System.IO.FileStream(_FilePath, System.IO.FileMode.Create);
                    //stream.WriteTo(oFile);
                    oFile.Write(content, 0, content.Length);
                    //...** Disposing and free memory resources 
                    try
                    {
                        oFile.Flush(); oFile.Close(); oFile.Dispose(); oFile = null;
                        //stream.Flush(); stream.Close(); stream.Dispose(); stream = null;
                        //Array.Clear(content, 0, content.Length);
                        content = null;
                    }
                    catch //(Exception ex)
                    {/*Blank catch*/ }
                    
                    return _FilePath;
                }
                else
                {
                    return null;
                }
            }
            else
            {

                return null;
            }
        }

        public static void DeleteFile(string sFileName)
        {
            if (File.Exists(sFileName))
            {
                File.SetAttributes(sFileName, FileAttributes.Normal);
                File.Delete(sFileName);
            }
        }

        public static string GenerateTempFileName(string sFileName)
        {
            string _FileName = "";
            if (sFileName.Trim() == "")
                _FileName = "ERA " + gloGlobal.clsFileExtensions.GetUniqueDateString("MMddyyyyHHmmssffff") + ".txt";//DateTime.Now.ToString("MM dd yyyy - hh mm ss tt") + " " + DateTime.Now.Millisecond.ToString() + System.Guid.NewGuid().ToString() + ".txt";DateTime.Now.ToString("MM dd yyyy - hh mm ss tt") + " " + DateTime.Now.Millisecond.ToString() + ".txt";DateTime.Now.ToString("MM dd yyyy - hh mm ss tt") + " " + DateTime.Now.Millisecond.ToString() + ".txt";
            else
                _FileName = sFileName.ToUpper().Replace(".RMT", ".txt");

            if (!_FileName.ToUpper().EndsWith(".TXT"))
                _FileName = _FileName + ".txt";

            
            string _AppTempFolder = gloSettings.FolderSettings.AppTempFolderPath;

            string _FullPath = _AppTempFolder + _FileName;

            if (Directory.Exists(_AppTempFolder) == false)
            {
                Directory.CreateDirectory(_AppTempFolder);
            }

            while (File.Exists(_FullPath))
            {
                if (sFileName == "")
                    _FullPath = _AppTempFolder + "ERA" + gloGlobal.clsFileExtensions.GetUniqueDateString("MMddyyyyHHmmssffff") + ".txt";//DateTime.Now.ToString("MM dd yyyy - hh mm ss tt") + " " + DateTime.Now.Millisecond.ToString() + System.Guid.NewGuid().ToString() + ".txt";DateTime.Now.ToString("MM dd yyyy - hh mm ss tt") + " " + DateTime.Now.Millisecond.ToString() + ".txt";
                else
                {
                    _FullPath = _AppTempFolder + sFileName.ToUpper().Replace(".RMT", ".txt");
                    if (!_FileName.ToUpper().EndsWith(".TXT"))
                        _FileName = _FileName + ".txt";
                    break;
                }
            }
            return _FullPath;
        }
        #endregion
    }
    #endregion

    #region " ERA File "

    public class ERAFile : IDisposable
    {
        #region " Constructor & Destructor "

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
                    if (_ISAs != null) { _ISAs.Dispose(); _ISAs = null; }
                }
            }
            disposed = true;
        }

        ~ERAFile()
        {
            Dispose(false);
        }

        #endregion

        #region " Private Variables "

        private Int64 _FileID;
        private String _FileName;
        private Int64 _SplitCount;
        private String _OriginalFileName;
        private String _FilePath;
        private DateTime _ImportDate;
        private enumERAFileStatus _Status;
        private bool _Deleted;
        private String _FileVersion;
        private ISAs _ISAs;

        #endregion

        #region " Public Properties "
        public Int64 FileID
        {
            get { return _FileID; }
            set { _FileID = value; }
        }
        public String FileName
        {
            get { return _FileName; }
            set { _FileName = value; }
        }
        public Int64 SplitCount
        {
            get { return _SplitCount; }
            set { _SplitCount = value; }
        }
        public String OriginalFileName
        {
            get { return _OriginalFileName; }
            set { _OriginalFileName = value; }
        }
        public String FilePath
        {
            get { return _FilePath; }
            set { _FilePath = value; }
        }
        public DateTime ImportDate
        {
            get { return _ImportDate; }
            set { _ImportDate = value; }
        }
        public enumERAFileStatus Status
        {
            get { return _Status; }
            set { _Status = value; }
        }
        public bool Deleted
        {
            get { return _Deleted; }
            set { _Deleted = value; }
        }
        public String FileVersion
        {
            get { return _FileVersion; }
            set { _FileVersion = value; }
        }
        public ISAs oISAs
        {
            get { return _ISAs; }
            set { _ISAs = value; }
        }
        #endregion
    }

    #endregion

    #region " Segment Classes "

    #region " ISA "

    public class ISA : IDisposable
    {
        #region " Constructor & Destructor "

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
                    if (_BPRs != null) { _BPRs.Dispose(); _BPRs = null; }
                }
            }
            disposed = true;
        }

        ~ISA()
        {
            Dispose(false);
        }

        #endregion

        #region " Private Variables "
        private Int64 nISAID;
        private Int64 nERAFileID;
        private string sISA01_AuthorInfoQual;
        private string sISA02_AuthorInfo;
        private string sISA03_SecurityInfoQual;
        private string sISA04_SecurityInfo;
        private string sISA05_IntrChngIDQual;
        private string sISA06_IntrChngSenderID;
        private string sISA07_IntrChngIDQual;
        private string sISA08_IntrChngReceiverID;
        private string sISA09_IntrChngDate;
        private string sISA10_IntrChngTime;
        private string sISA11_IntrChngControlStandardsID;
        private string sISA12_IntrChngControlVersionNo;
        private string sISA13_IntrChngControlNo;
        private string sISA14_AckwRequested;
        private string sISA15_UsageIndicator;
        private string sISA16_ComponentElementSeparator;

        private BPRs _BPRs = new BPRs();
        #endregion

        #region " Public Properties "

        public Int64 ISAID
        {
            get { return nISAID; }
            set { nISAID = value; }
        }
        public Int64 ERAFileID
        {
            get { return nERAFileID; }
            set { nERAFileID = value; }
        }
        public string ISA01_AuthorInfoQual
        {
            get { return sISA01_AuthorInfoQual; }
            set { sISA01_AuthorInfoQual = value; }
        }
        public string ISA02_AuthorInfo
        {
            get { return sISA02_AuthorInfo; }
            set { sISA02_AuthorInfo = value; }
        }
        public string ISA03_SecurityInfoQual
        {
            get { return sISA03_SecurityInfoQual; }
            set { sISA03_SecurityInfoQual = value; }
        }
        public string ISA04_SecurityInfo
        {
            get { return sISA04_SecurityInfo; }
            set { sISA04_SecurityInfo = value; }
        }
        public string ISA05_IntrChngIDQual
        {
            get { return sISA05_IntrChngIDQual; }
            set { sISA05_IntrChngIDQual = value; }
        }
        public string ISA06_IntrChngSenderID
        {
            get { return sISA06_IntrChngSenderID; }
            set { sISA06_IntrChngSenderID = value; }
        }
        public string ISA07_IntrChngIDQual
        {
            get { return sISA07_IntrChngIDQual; }
            set { sISA07_IntrChngIDQual = value; }
        }
        public string ISA08_IntrChngReceiverID
        {
            get { return sISA08_IntrChngReceiverID; }
            set { sISA08_IntrChngReceiverID = value; }
        }
        public string ISA09_IntrChngDate
        {
            get { return sISA09_IntrChngDate; }
            set { sISA09_IntrChngDate = value; }
        }
        public string ISA10_IntrChngTime
        {
            get { return sISA10_IntrChngTime; }
            set { sISA10_IntrChngTime = value; }
        }
        public string ISA11_IntrChngControlStandardsID
        {
            get { return sISA11_IntrChngControlStandardsID; }
            set { sISA11_IntrChngControlStandardsID = value; }
        }
        public string ISA12_IntrChngControlVersionNo
        {
            get { return sISA12_IntrChngControlVersionNo; }
            set { sISA12_IntrChngControlVersionNo = value; }
        }
        public string ISA13_IntrChngControlNo
        {
            get { return sISA13_IntrChngControlNo; }
            set { sISA13_IntrChngControlNo = value; }
        }
        public string ISA14_AckwRequested
        {
            get { return sISA14_AckwRequested; }
            set { sISA14_AckwRequested = value; }
        }
        public string ISA15_UsageIndicator
        {
            get { return sISA15_UsageIndicator; }
            set { sISA15_UsageIndicator = value; }
        }
        public string ISA16_ComponentElementSeparator
        {
            get { return sISA16_ComponentElementSeparator; }
            set { sISA16_ComponentElementSeparator = value; }
        }

        public BPRs oBPRs
        {
            get { return _BPRs; }
            set { _BPRs = value; }
        }
        #endregion
    }

    public class ISAs : IDisposable
    {
        protected ArrayList _innerlist;

        #region " Constructor & Distructor "

        public ISAs()
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
                    _innerlist = null;
                }
            }
            disposed = true;
        }
        ~ISAs()
        {
            Dispose(false);
        }

        #endregion

        #region " Methods Add, Remove, Count, Item "

        public int Count
        {
            get { return _innerlist.Count; }
        }
        public void Add(ISA item)
        {
            _innerlist.Add(item);
        }
        public bool Remove(ISA item)
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
        public ISA this[int index]
        {
            get
            { return (ISA)_innerlist[index]; }
        }
        public bool Contains(ISA item)
        {
            return _innerlist.Contains(item);
        }
        public int IndexOf(ISA item)
        {
            return _innerlist.IndexOf(item);
        }
        public void CopyTo(ISA[] array, int index)
        {
            _innerlist.CopyTo(array, index);
        }

        #endregion

    }

    #endregion

    #region " BPR "

    public class BPR : IDisposable
    {
        #region " Constructor & Destructor "

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
                    if (_TRNs != null) { _TRNs.Dispose(); _TRNs = null; }
                    if (_REFs != null) { _REFs.Dispose(); _REFs = null; }
                    if (_DTMs != null) { _DTMs.Dispose(); _DTMs = null; }
                    if (_CLPs != null) { _CLPs.Dispose(); _CLPs = null; }
                    if (_PLBs != null) { _PLBs.Dispose(); _PLBs = null; }
                    if (_PayerPayeeIdents != null) { _PayerPayeeIdents.Dispose(); _PayerPayeeIdents = null; }
                }
            }
            disposed = true;
        }

        ~BPR()
        {
            Dispose(false);
        }

        #endregion

        #region " Private Variables "

        private Int64 nBPRID;
        private Int64 nERAFileID;
        private Int64 nISAID;
        private string sBPR01_TransHandlingCode;
        private string sBPR02_Amount;
        private string sBPR03_CreditDebitFlagCode;
        private string sBPR04_PaymentMethodCode;
        private string sBPR05_PaymentFormatCode;
        private string sBPR06_DFI_IDNoQual;
        private string sBPR07_DFI_IDNo;
        private string sBPR08_AccNoQual;
        private string sBPR09_AccNo;
        private string sBPR10_OriginatingCompanyID;
        private string sBPR11_OriginatingCompanySuppCode;
        private string sBPR12_DFI_IDNoQual;
        private string sBPR13_DFI_IDNo;
        private string sBPR14_AccNoQual;
        private string sBPR15_AccNo;
        private string sBPR16_Date;
        private string sBPR17_BusinessFunc_Code;
        private string sBPR18_DFI_IDNoQual;
        private string sBPR19_DFI_IDNo;
        private string sBPR20_AccNoQual;
        private string sBPR21_AccNo;
        private enumCheckStatus _CheckStatus;

        private TRNs _TRNs = new TRNs();
        private REFs _REFs = new REFs();
        private DTMs _DTMs = new DTMs();
        private CLPs _CLPs = new CLPs();
        private PLBs _PLBs = new PLBs();
        private RDMs _RDMs = new RDMs();

        private PayerPayeeIdents _PayerPayeeIdents = new PayerPayeeIdents();

        #endregion

        #region " Public Properties "

        public Int64 BPRID
        {
            get { return nBPRID; }
            set { nBPRID = value; }
        }
        public Int64 ERAFileID
        {
            get { return nERAFileID; }
            set { nERAFileID = value; }
        }
        public Int64 ISAID
        {
            get { return nISAID; }
            set { nISAID = value; }
        }
        public string BPR01_TransHandlingCode
        {
            get { return sBPR01_TransHandlingCode; }
            set { sBPR01_TransHandlingCode = value; }
        }
        public string BPR02_Amount
        {
            get { return sBPR02_Amount; }
            set { sBPR02_Amount = value; }
        }
        public string BPR03_CreditDebitFlagCode
        {
            get { return sBPR03_CreditDebitFlagCode; }
            set { sBPR03_CreditDebitFlagCode = value; }
        }
        public string BPR04_PaymentMethodCode
        {
            get { return sBPR04_PaymentMethodCode; }
            set { sBPR04_PaymentMethodCode = value; }
        }
        public string BPR05_PaymentFormatCode
        {
            get { return sBPR05_PaymentFormatCode; }
            set { sBPR05_PaymentFormatCode = value; }
        }
        public string BPR06_DFI_IDNoQual
        {
            get { return sBPR06_DFI_IDNoQual; }
            set { sBPR06_DFI_IDNoQual = value; }
        }
        public string BPR07_DFI_IDNo
        {
            get { return sBPR07_DFI_IDNo; }
            set { sBPR07_DFI_IDNo = value; }
        }
        public string BPR08_AccNoQual
        {
            get { return sBPR08_AccNoQual; }
            set { sBPR08_AccNoQual = value; }
        }
        public string BPR09_AccNo
        {
            get { return sBPR09_AccNo; }
            set { sBPR09_AccNo = value; }
        }
        public string BPR10_OriginatingCompanyID
        {
            get { return sBPR10_OriginatingCompanyID; }
            set { sBPR10_OriginatingCompanyID = value; }
        }
        public string BPR11_OriginatingCompanySuppCode
        {
            get { return sBPR11_OriginatingCompanySuppCode; }
            set { sBPR11_OriginatingCompanySuppCode = value; }
        }
        public string BPR12_DFI_IDNoQual
        {
            get { return sBPR12_DFI_IDNoQual; }
            set { sBPR12_DFI_IDNoQual = value; }
        }
        public string BPR13_DFI_IDNo
        {
            get { return sBPR13_DFI_IDNo; }
            set { sBPR13_DFI_IDNo = value; }
        }
        public string BPR14_AccNoQual
        {
            get { return sBPR14_AccNoQual; }
            set { sBPR14_AccNoQual = value; }
        }
        public string BPR15_AccNo
        {
            get { return sBPR15_AccNo; }
            set { sBPR15_AccNo = value; }
        }
        public string BPR16_Date
        {
            get { return sBPR16_Date; }
            set { sBPR16_Date = value; }
        }
        public string BPR17_BusinessFunc_Code
        {
            get { return sBPR17_BusinessFunc_Code; }
            set { sBPR17_BusinessFunc_Code = value; }
        }
        public string BPR18_DFI_IDNoQual
        {
            get { return sBPR18_DFI_IDNoQual; }
            set { sBPR18_DFI_IDNoQual = value; }
        }
        public string BPR19_DFI_IDNo
        {
            get { return sBPR19_DFI_IDNo; }
            set { sBPR19_DFI_IDNo = value; }
        }
        public string BPR20_AccNoQual
        {
            get { return sBPR20_AccNoQual; }
            set { sBPR20_AccNoQual = value; }
        }
        public string BPR21_AccNo
        {
            get { return sBPR21_AccNo; }
            set { sBPR21_AccNo = value; }
        }
        public enumCheckStatus CheckStatus
        {
            get { return _CheckStatus; }
            set { _CheckStatus = value; }
        }

        public TRNs oTRNs
        {
            get { return _TRNs; }
            set { _TRNs = value; }
        }
        public REFs oREFs
        {
            get { return _REFs; }
            set { _REFs = value; }
        }
        public DTMs oDTMs
        {
            get { return _DTMs; }
            set { _DTMs = value; }
        }
        public CLPs oCLPs
        {
            get { return _CLPs; }
            set { _CLPs = value; }
        }
        public PLBs oPLBs
        {
            get { return _PLBs; }
            set { _PLBs = value; }
        }
        public RDMs oRDMs
        {
            get { return _RDMs; }
            set { _RDMs = value; }
        }
        public PayerPayeeIdents oPayerPayeeIdents
        {
            get { return _PayerPayeeIdents; }
            set { _PayerPayeeIdents = value; }
        }

        #endregion
    }

    public class BPRs : IDisposable
    {
        protected ArrayList _innerlist;

        #region " Constructor & Distructor "

        public BPRs()
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
                    _innerlist = null;
                }
            }
            disposed = true;
        }
        ~BPRs()
        {
            Dispose(false);
        }

        #endregion

        #region " Methods Add, Remove, Count, Item "

        public int Count
        {
            get { return _innerlist.Count; }
        }
        public void Add(BPR item)
        {
            _innerlist.Add(item);
        }
        public bool Remove(BPR item)
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
        public BPR this[int index]
        {
            get
            { return (BPR)_innerlist[index]; }
        }
        public bool Contains(BPR item)
        {
            return _innerlist.Contains(item);
        }
        public int IndexOf(BPR item)
        {
            return _innerlist.IndexOf(item);
        }
        public void CopyTo(BPR[] array, int index)
        {
            _innerlist.CopyTo(array, index);
        }

        #endregion

    }

    #endregion

    #region " TRN "

    public class TRN : IDisposable
    {
        #region " Constructor & Destructor "

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

        ~TRN()
        {
            Dispose(false);
        }

        #endregion

        #region " Private Variables "
        private Int64 nTRNID;
        private Int64 nERAFileID;
        private Int64 nISAID;
        private Int64 nBPRID;
        private string sTRN01_TraceTypeCode;
        private string sTRN02_Ref_ID;
        private string sTRN03_OriginatingCompanyID;
        private string sTRN04_Ref_ID;

        #endregion

        #region " Public Properties "

        public Int64 TRNID
        {
            get { return nTRNID; }
            set { nTRNID = value; }
        }
        public Int64 ERAFileID
        {
            get { return nERAFileID; }
            set { nERAFileID = value; }
        }
        public Int64 ISAID
        {
            get { return nISAID; }
            set { nISAID = value; }
        }
        public Int64 BPRID
        {
            get { return nBPRID; }
            set { nBPRID = value; }
        }
        public string TRN01_TraceTypeCode
        {
            get { return sTRN01_TraceTypeCode; }
            set { sTRN01_TraceTypeCode = value; }
        }
        public string TRN02_Ref_ID
        {
            get { return sTRN02_Ref_ID; }
            set { sTRN02_Ref_ID = value; }
        }
        public string TRN03_OriginatingCompanyID
        {
            get { return sTRN03_OriginatingCompanyID; }
            set { sTRN03_OriginatingCompanyID = value; }
        }
        public string TRN04_Ref_ID
        {
            get { return sTRN04_Ref_ID; }
            set { sTRN04_Ref_ID = value; }
        }


        #endregion
    }

    public class TRNs : IDisposable
    {
        protected ArrayList _innerlist;

        #region " Constructor & Distructor "

        public TRNs()
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
                    _innerlist = null;
                }
            }
            disposed = true;
        }
        ~TRNs()
        {
            Dispose(false);
        }

        #endregion

        #region " Methods Add, Remove, Count, Item "

        public int Count
        {
            get { return _innerlist.Count; }
        }
        public void Add(TRN item)
        {
            _innerlist.Add(item);
        }
        public bool Remove(TRN item)
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
        public TRN this[int index]
        {
            get
            { return (TRN)_innerlist[index]; }
        }
        public bool Contains(TRN item)
        {
            return _innerlist.Contains(item);
        }
        public int IndexOf(TRN item)
        {
            return _innerlist.IndexOf(item);
        }
        public void CopyTo(TRN[] array, int index)
        {
            _innerlist.CopyTo(array, index);
        }

        #endregion

    }

    #endregion

    #region " PLB "

    public class PLB : IDisposable
    {
        #region " Constructor & Destructor "

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

        ~PLB()
        {
            Dispose(false);
        }

        #endregion

        #region " Private Variables "

        private Int64 nPLBID;
        private Int64 nERAFileID;
        private Int64 nISAID;
        private Int64 nBPRID;
        private string sPLB01_Ref_ID;
        private string sPLB02_Date;
        private string sPLB03_AdjustID;
        private string sPLB03_1_AdjustReasonCode;
        private string sPLB03_2_Ref_ID;
        private string sPLB04_Amount;
        private string sPLB05_AdjustID;
        private string sPLB05_1_AdjustReasonCode;
        private string sPLB05_2_Ref_ID;
        private string sPLB06_Amount;
        private string sPLB07_AdjustID;
        private string sPLB07_1_AdjustReasonCode;
        private string sPLB07_2_Ref_ID;
        private string sPLB08_Amount;
        private string sPLB09_AdjustID;
        private string sPLB09_1_AdjustReasonCode;
        private string sPLB09_2_Ref_ID;
        private string sPLB10_Amount;
        private string sPLB11_AdjustID;
        private string sPLB11_1_AdjustReasonCode;
        private string sPLB11_2_Ref_ID;
        private string sPLB12_Amount;
        private string sPLB13_AdjustID;
        private string sPLB13_1_AdjustReasonCode;
        private string sPLB13_2_Ref_ID;
        private string sPLB14_Amount;

        #endregion

        #region " Public Properties "

        public Int64 PLBID
        {
            get { return nPLBID; }
            set { nPLBID = value; }
        }
        public Int64 ERAFileID
        {
            get { return nERAFileID; }
            set { nERAFileID = value; }
        }
        public Int64 ISAID
        {
            get { return nISAID; }
            set { nISAID = value; }
        }
        public Int64 BPRID
        {
            get { return nBPRID; }
            set { nBPRID = value; }
        }
        public string PLB01_Ref_ID
        {
            get { return sPLB01_Ref_ID; }
            set { sPLB01_Ref_ID = value; }
        }
        public string PLB02_Date
        {
            get { return sPLB02_Date; }
            set { sPLB02_Date = value; }
        }
        public string PLB03_AdjustID
        {
            get { return sPLB03_AdjustID; }
            set { sPLB03_AdjustID = value; }
        }
        public string PLB03_1_AdjustReasonCode
        {
            get { return sPLB03_1_AdjustReasonCode; }
            set { sPLB03_1_AdjustReasonCode = value; }
        }
        public string PLB03_2_Ref_ID
        {
            get { return sPLB03_2_Ref_ID; }
            set { sPLB03_2_Ref_ID = value; }
        }
        public string PLB04_Amount
        {
            get { return sPLB04_Amount; }
            set { sPLB04_Amount = value; }
        }
        public string PLB05_AdjustID
        {
            get { return sPLB05_AdjustID; }
            set { sPLB05_AdjustID = value; }
        }
        public string PLB05_1_AdjustReasonCode
        {
            get { return sPLB05_1_AdjustReasonCode; }
            set { sPLB05_1_AdjustReasonCode = value; }
        }
        public string PLB05_2_Ref_ID
        {
            get { return sPLB05_2_Ref_ID; }
            set { sPLB05_2_Ref_ID = value; }
        }
        public string PLB06_Amount
        {
            get { return sPLB06_Amount; }
            set { sPLB06_Amount = value; }
        }
        public string PLB07_AdjustID
        {
            get { return sPLB07_AdjustID; }
            set { sPLB07_AdjustID = value; }
        }
        public string PLB07_1_AdjustReasonCode
        {
            get { return sPLB07_1_AdjustReasonCode; }
            set { sPLB07_1_AdjustReasonCode = value; }
        }
        public string PLB07_2_Ref_ID
        {
            get { return sPLB07_2_Ref_ID; }
            set { sPLB07_2_Ref_ID = value; }
        }
        public string PLB08_Amount
        {
            get { return sPLB08_Amount; }
            set { sPLB08_Amount = value; }
        }
        public string PLB09_AdjustID
        {
            get { return sPLB09_AdjustID; }
            set { sPLB09_AdjustID = value; }
        }
        public string PLB09_1_AdjustReasonCode
        {
            get { return sPLB09_1_AdjustReasonCode; }
            set { sPLB09_1_AdjustReasonCode = value; }
        }
        public string PLB09_2_Ref_ID
        {
            get { return sPLB09_2_Ref_ID; }
            set { sPLB09_2_Ref_ID = value; }
        }
        public string PLB10_Amount
        {
            get { return sPLB10_Amount; }
            set { sPLB10_Amount = value; }
        }
        public string PLB11_AdjustID
        {
            get { return sPLB11_AdjustID; }
            set { sPLB11_AdjustID = value; }
        }
        public string PLB11_1_AdjustReasonCode
        {
            get { return sPLB11_1_AdjustReasonCode; }
            set { sPLB11_1_AdjustReasonCode = value; }
        }
        public string PLB11_2_Ref_ID
        {
            get { return sPLB11_2_Ref_ID; }
            set { sPLB11_2_Ref_ID = value; }
        }
        public string PLB12_Amount
        {
            get { return sPLB12_Amount; }
            set { sPLB12_Amount = value; }
        }
        public string PLB13_AdjustID
        {
            get { return sPLB13_AdjustID; }
            set { sPLB13_AdjustID = value; }
        }
        public string PLB13_1_AdjustReasonCode
        {
            get { return sPLB13_1_AdjustReasonCode; }
            set { sPLB13_1_AdjustReasonCode = value; }
        }
        public string PLB13_2_Ref_ID
        {
            get { return sPLB13_2_Ref_ID; }
            set { sPLB13_2_Ref_ID = value; }
        }
        public string PLB14_Amount
        {
            get { return sPLB14_Amount; }
            set { sPLB14_Amount = value; }
        }


        #endregion
    }

    public class PLBs : IDisposable
    {
        protected ArrayList _innerlist;

        #region " Constructor & Distructor "

        public PLBs()
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
                    _innerlist = null;
                }
            }
            disposed = true;
        }
        ~PLBs()
        {
            Dispose(false);
        }

        #endregion

        #region " Methods Add, Remove, Count, Item "

        public int Count
        {
            get { return _innerlist.Count; }
        }
        public void Add(PLB item)
        {
            _innerlist.Add(item);
        }
        public bool Remove(PLB item)
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
        public PLB this[int index]
        {
            get
            { return (PLB)_innerlist[index]; }
        }
        public bool Contains(PLB item)
        {
            return _innerlist.Contains(item);
        }
        public int IndexOf(PLB item)
        {
            return _innerlist.IndexOf(item);
        }
        public void CopyTo(PLB[] array, int index)
        {
            _innerlist.CopyTo(array, index);
        }

        #endregion

    }

    #endregion

    #region " CLP "

    public class CLP : IDisposable
    {
        #region " Constructor & Destructor "

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
                    if (_CASs != null) { _CASs.Dispose(); _CASs = null; }
                    if (_NM1s != null) { _NM1s.Dispose(); _NM1s = null; }
                    if (_SVCs != null) { _SVCs.Dispose(); _SVCs = null; }
                    if (_MIAs != null) { _MIAs.Dispose(); _MIAs = null; }
                    if (_MOAs != null) { _MOAs.Dispose(); _MOAs = null; }
                    if (_REFs != null) { _REFs.Dispose(); _REFs = null; }
                    if (_DTMs != null) { _DTMs.Dispose(); _DTMs = null; }
                    if (_PERs != null) { _PERs.Dispose(); _PERs = null; }
                    if (_AMTs != null) { _AMTs.Dispose(); _AMTs = null; }
                }
            }
            disposed = true;
        }

        ~CLP()
        {
            Dispose(false);
        }

        #endregion

        #region " Private Variables "

        private Int64 nCLPID;
        private Int64 nERAFileID;
        private Int64 nISAID;
        private Int64 nBPRID;
        private string sCLP01_ClaimSubmitterID;
        private string sCLP02_ClaimStatusCode;
        private string sCLP03_Amount;
        private string sCLP04_Amount;
        private string sCLP05_Amount;
        private string sCLP06_ClaimFilingIndicatorCode;
        private string sCLP07_Ref_ID;
        private string sCLP08_FacilityCodeValue;
        private string sCLP09_ClaimFrequencyTypeCode;
        private string sCLP10_PatientStatusCode;
        private string sCLP11_DiagRelatedGroupCode;
        private string sCLP12_Qty;
        private string sCLP13_Percent;

        private CASs _CASs = new CASs();
        private NM1s _NM1s = new NM1s();
        private SVCs _SVCs = new SVCs();
        private MIAs _MIAs = new MIAs();
        private MOAs _MOAs = new MOAs();
        private REFs _REFs = new REFs();
        private DTMs _DTMs = new DTMs();
        private PERs _PERs = new PERs();
        private AMTs _AMTs = new AMTs();

        private int nClaimFileIndex;
        #endregion

        #region " Public Properties "

        public Int64 CLPID
        {
            get { return nCLPID; }
            set { nCLPID = value; }
        }
        public Int64 ERAFileID
        {
            get { return nERAFileID; }
            set { nERAFileID = value; }
        }
        public Int64 ISAID
        {
            get { return nISAID; }
            set { nISAID = value; }
        }
        public Int64 BPRID
        {
            get { return nBPRID; }
            set { nBPRID = value; }
        }
        public string CLP01_ClaimSubmitterID
        {
            get { return sCLP01_ClaimSubmitterID; }
            set { sCLP01_ClaimSubmitterID = value; }
        }
        public string CLP02_ClaimStatusCode
        {
            get { return sCLP02_ClaimStatusCode; }
            set { sCLP02_ClaimStatusCode = value; }
        }
        public string CLP03_Amount
        {
            get { return sCLP03_Amount; }
            set { sCLP03_Amount = value; }
        }
        public string CLP04_Amount
        {
            get { return sCLP04_Amount; }
            set { sCLP04_Amount = value; }
        }
        public string CLP05_Amount
        {
            get { return sCLP05_Amount; }
            set { sCLP05_Amount = value; }
        }
        public string CLP06_ClaimFilingIndicatorCode
        {
            get { return sCLP06_ClaimFilingIndicatorCode; }
            set { sCLP06_ClaimFilingIndicatorCode = value; }
        }
        public string CLP07_Ref_ID
        {
            get { return sCLP07_Ref_ID; }
            set { sCLP07_Ref_ID = value; }
        }
        public string CLP08_FacilityCodeValue
        {
            get { return sCLP08_FacilityCodeValue; }
            set { sCLP08_FacilityCodeValue = value; }
        }
        public string CLP09_ClaimFrequencyTypeCode
        {
            get { return sCLP09_ClaimFrequencyTypeCode; }
            set { sCLP09_ClaimFrequencyTypeCode = value; }
        }
        public string CLP10_PatientStatusCode
        {
            get { return sCLP10_PatientStatusCode; }
            set { sCLP10_PatientStatusCode = value; }
        }
        public string CLP11_DiagRelatedGroupCode
        {
            get { return sCLP11_DiagRelatedGroupCode; }
            set { sCLP11_DiagRelatedGroupCode = value; }
        }
        public string CLP12_Qty
        {
            get { return sCLP12_Qty; }
            set { sCLP12_Qty = value; }
        }
        public string CLP13_Percent
        {
            get { return sCLP13_Percent; }
            set { sCLP13_Percent = value; }
        }

        public CASs oCASs
        {
            get { return _CASs; }
            set { _CASs = value; }
        }
        public NM1s oNM1s
        {
            get { return _NM1s; }
            set { _NM1s = value; }
        }
        public SVCs oSVCs
        {
            get { return _SVCs; }
            set { _SVCs = value; }
        }
        public MIAs oMIAs
        {
            get { return _MIAs; }
            set { _MIAs = value; }
        }
        public MOAs oMOAs
        {
            get { return _MOAs; }
            set { _MOAs = value; }
        }
        public REFs oREFs
        {
            get { return _REFs; }
            set { _REFs = value; }
        }
        public DTMs oDTMs
        {
            get { return _DTMs; }
            set { _DTMs = value; }
        }
        public PERs oPERs
        {
            get { return _PERs; }
            set { _PERs = value; }
        }
        public AMTs oAMTs
        {
            get { return _AMTs; }
            set { _AMTs = value; }
        }

        public int ClaimFileIndex
        {
            get { return nClaimFileIndex; }
            set { nClaimFileIndex = value; }
        }
        #endregion
    }

    public class CLPs : IDisposable
    {
        protected ArrayList _innerlist;

        #region " Constructor & Distructor "

        public CLPs()
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
                    _innerlist = null;
                }
            }
            disposed = true;
        }
        ~CLPs()
        {
            Dispose(false);
        }

        #endregion

        #region " Methods Add, Remove, Count, Item "

        public int Count
        {
            get { return _innerlist.Count; }
        }
        public void Add(CLP item)
        {
            _innerlist.Add(item);
        }
        public bool Remove(CLP item)
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
        public CLP this[int index]
        {
            get
            { return (CLP)_innerlist[index]; }
        }
        public bool Contains(CLP item)
        {
            return _innerlist.Contains(item);
        }
        public int IndexOf(CLP item)
        {
            return _innerlist.IndexOf(item);
        }
        public void CopyTo(CLP[] array, int index)
        {
            _innerlist.CopyTo(array, index);
        }

        #endregion

    }

    #endregion

    #region " PayerPayeeIdentification "

    public class PayerPayeeIdent : IDisposable
    {
        #region " Constructor & Destructor "

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
                    if (_REFs != null) { _REFs.Dispose(); _REFs = null; }
                    if (_PERs != null) { _PERs.Dispose(); _PERs = null; }
                }
            }
            disposed = true;
        }

        ~PayerPayeeIdent()
        {
            Dispose(false);
        }

        #endregion

        #region " Private Variables "

        private Int64 nPayerPayeeID;
        private Int64 nERAFileID;
        private Int64 nISAID;
        private Int64 nBPRID;
        private string sN101_EntityIDCode = "";
        private string sN102_Name = "";
        private string sN103_IDCodeQual = "";
        private string sN104_IDCode = "";
        private string sN105_EntityRelationCode = "";
        private string sN106_EntityIDCode = "";
        private string sN301_AddrInfo = "";
        private string sN302_AddrInfo = "";
        private string sN401_CityName = "";
        private string sN402_StateOrProvinceCode = "";
        private string sN403_PostalCode = "";
        private string sN404_CountryCode = "";
        private string sN405_LocationQual = "";
        private string sN406_LocationID = "";
        private string sN407_CountrySubDivisionCode = "";
        private string sN112_NameLastOrg = "";
        private enumPayerPayee_Type _PayerPayeeType;

        private REFs _REFs = new REFs();
        private PERs _PERs = new PERs();

        #endregion

        #region " Public Properties "

        public Int64 PayerPayeeID
        {
            get { return nPayerPayeeID; }
            set { nPayerPayeeID = value; }
        }
        public Int64 ERAFileID
        {
            get { return nERAFileID; }
            set { nERAFileID = value; }
        }
        public Int64 ISAID
        {
            get { return nISAID; }
            set { nISAID = value; }
        }
        public Int64 BPRID
        {
            get { return nBPRID; }
            set { nBPRID = value; }
        }
        public string N101_EntityIDCode
        {
            get { return sN101_EntityIDCode; }
            set { sN101_EntityIDCode = value; }
        }
        public string N102_Name
        {
            get { return sN102_Name; }
            set { sN102_Name = value; }
        }
        public string N103_IDCodeQual
        {
            get { return sN103_IDCodeQual; }
            set { sN103_IDCodeQual = value; }
        }
        public string N104_IDCode
        {
            get { return sN104_IDCode; }
            set { sN104_IDCode = value; }
        }
        public string N105_EntityRelationCode
        {
            get { return sN105_EntityRelationCode; }
            set { sN105_EntityRelationCode = value; }
        }
        public string N106_EntityIDCode
        {
            get { return sN106_EntityIDCode; }
            set { sN106_EntityIDCode = value; }
        }
        public string N301_AddrInfo
        {
            get { return sN301_AddrInfo; }
            set { sN301_AddrInfo = value; }
        }
        public string N302_AddrInfo
        {
            get { return sN302_AddrInfo; }
            set { sN302_AddrInfo = value; }
        }
        public string N401_CityName
        {
            get { return sN401_CityName; }
            set { sN401_CityName = value; }
        }
        public string N402_StateOrProvinceCode
        {
            get { return sN402_StateOrProvinceCode; }
            set { sN402_StateOrProvinceCode = value; }
        }
        public string N403_PostalCode
        {
            get { return sN403_PostalCode; }
            set { sN403_PostalCode = value; }
        }
        public string N404_CountryCode
        {
            get { return sN404_CountryCode; }
            set { sN404_CountryCode = value; }
        }
        public string N405_LocationQual
        {
            get { return sN405_LocationQual; }
            set { sN405_LocationQual = value; }
        }
        public string N406_LocationID
        {
            get { return sN406_LocationID; }
            set { sN406_LocationID = value; }
        }
        public string N407_CountrySubDivisionCode
        {
            get { return sN407_CountrySubDivisionCode; }
            set { sN407_CountrySubDivisionCode = value; }
        }

        public string N112_NameLastOrg
        {
            get { return sN112_NameLastOrg; }
            set { sN112_NameLastOrg = value; }
        }
        public enumPayerPayee_Type PayerPayeeType
        {
            get { return _PayerPayeeType; }
            set { _PayerPayeeType = value; }
        }

        public REFs oREFs
        {
            get { return _REFs; }
            set { _REFs = value; }
        }

        public PERs oPERs
        {
            get { return _PERs; }
            set { _PERs = value; }
        }
        #endregion
    }

    public class PayerPayeeIdents : IDisposable
    {
        protected ArrayList _innerlist;

        #region " Constructor & Distructor "

        public PayerPayeeIdents()
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
                    _innerlist = null;
                }
            }
            disposed = true;
        }
        ~PayerPayeeIdents()
        {
            Dispose(false);
        }

        #endregion

        #region " Methods Add, Remove, Count, Item "

        public int Count
        {
            get { return _innerlist.Count; }
        }
        public void Add(PayerPayeeIdent item)
        {
            _innerlist.Add(item);
        }
        public bool Remove(PayerPayeeIdent item)
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
        public PayerPayeeIdent this[int index]
        {
            get
            { return (PayerPayeeIdent)_innerlist[index]; }
        }
        public bool Contains(PayerPayeeIdent item)
        {
            return _innerlist.Contains(item);
        }
        public int IndexOf(PayerPayeeIdent item)
        {
            return _innerlist.IndexOf(item);
        }
        public void CopyTo(PayerPayeeIdent[] array, int index)
        {
            _innerlist.CopyTo(array, index);
        }

        #endregion

    }

    #endregion

    #region " MIA "

    public class MIA : IDisposable
    {
        #region " Constructor & Destructor "

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

        ~MIA()
        {
            Dispose(false);
        }

        #endregion

        #region " Private Variables "

        private Int64 nMIAID;
        private Int64 nERAFileID;
        private Int64 nISAID;
        private Int64 nBPRID;
        private Int64 nCLPID;
        private string sMIA01_Qty;
        private string sMIA02_Qty;
        private string sMIA03_Qty;
        private string sMIA04_Amount;
        private string sMIA05_Ref_ID;
        private string sMIA06_Amount;
        private string sMIA07_Amount;
        private string sMIA08_Amount;
        private string sMIA09_Amount;
        private string sMIA10_Amount;
        private string sMIA11_Amount;
        private string sMIA12_Amount;
        private string sMIA13_Amount;
        private string sMIA14_Amount;
        private string sMIA15_Qty;
        private string sMIA16_Amount;
        private string sMIA17_Amount;
        private string sMIA18_Amount;
        private string sMIA19_Amount;
        private string sMIA20_Ref_ID;
        private string sMIA21_Ref_ID;
        private string sMIA22_Ref_ID;
        private string sMIA23_Ref_ID;
        private string sMIA24_Amount;

        #endregion

        #region " Public Properties "

        public Int64 MIAID
        {
            get { return nMIAID; }
            set { nMIAID = value; }
        }
        public Int64 ERAFileID
        {
            get { return nERAFileID; }
            set { nERAFileID = value; }
        }
        public Int64 ISAID
        {
            get { return nISAID; }
            set { nISAID = value; }
        }
        public Int64 BPRID
        {
            get { return nBPRID; }
            set { nBPRID = value; }
        }
        public Int64 CLPID
        {
            get { return nCLPID; }
            set { nCLPID = value; }
        }
        public string MIA01_Qty
        {
            get { return sMIA01_Qty; }
            set { sMIA01_Qty = value; }
        }
        public string MIA02_Qty
        {
            get { return sMIA02_Qty; }
            set { sMIA02_Qty = value; }
        }
        public string MIA03_Qty
        {
            get { return sMIA03_Qty; }
            set { sMIA03_Qty = value; }
        }
        public string MIA04_Amount
        {
            get { return sMIA04_Amount; }
            set { sMIA04_Amount = value; }
        }
        public string MIA05_Ref_ID
        {
            get { return sMIA05_Ref_ID; }
            set { sMIA05_Ref_ID = value; }
        }
        public string MIA06_Amount
        {
            get { return sMIA06_Amount; }
            set { sMIA06_Amount = value; }
        }
        public string MIA07_Amount
        {
            get { return sMIA07_Amount; }
            set { sMIA07_Amount = value; }
        }
        public string MIA08_Amount
        {
            get { return sMIA08_Amount; }
            set { sMIA08_Amount = value; }
        }
        public string MIA09_Amount
        {
            get { return sMIA09_Amount; }
            set { sMIA09_Amount = value; }
        }
        public string MIA10_Amount
        {
            get { return sMIA10_Amount; }
            set { sMIA10_Amount = value; }
        }
        public string MIA11_Amount
        {
            get { return sMIA11_Amount; }
            set { sMIA11_Amount = value; }
        }
        public string MIA12_Amount
        {
            get { return sMIA12_Amount; }
            set { sMIA12_Amount = value; }
        }
        public string MIA13_Amount
        {
            get { return sMIA13_Amount; }
            set { sMIA13_Amount = value; }
        }
        public string MIA14_Amount
        {
            get { return sMIA14_Amount; }
            set { sMIA14_Amount = value; }
        }
        public string MIA15_Qty
        {
            get { return sMIA15_Qty; }
            set { sMIA15_Qty = value; }
        }
        public string MIA16_Amount
        {
            get { return sMIA16_Amount; }
            set { sMIA16_Amount = value; }
        }
        public string MIA17_Amount
        {
            get { return sMIA17_Amount; }
            set { sMIA17_Amount = value; }
        }
        public string MIA18_Amount
        {
            get { return sMIA18_Amount; }
            set { sMIA18_Amount = value; }
        }
        public string MIA19_Amount
        {
            get { return sMIA19_Amount; }
            set { sMIA19_Amount = value; }
        }
        public string MIA20_Ref_ID
        {
            get { return sMIA20_Ref_ID; }
            set { sMIA20_Ref_ID = value; }
        }
        public string MIA21_Ref_ID
        {
            get { return sMIA21_Ref_ID; }
            set { sMIA21_Ref_ID = value; }
        }
        public string MIA22_Ref_ID
        {
            get { return sMIA22_Ref_ID; }
            set { sMIA22_Ref_ID = value; }
        }
        public string MIA23_Ref_ID
        {
            get { return sMIA23_Ref_ID; }
            set { sMIA23_Ref_ID = value; }
        }
        public string MIA24_Amount
        {
            get { return sMIA24_Amount; }
            set { sMIA24_Amount = value; }
        }



        #endregion
    }

    public class MIAs : IDisposable
    {
        protected ArrayList _innerlist;

        #region " Constructor & Distructor "

        public MIAs()
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
                    _innerlist = null;
                }
            }
            disposed = true;
        }
        ~MIAs()
        {
            Dispose(false);
        }

        #endregion

        #region " Methods Add, Remove, Count, Item "

        public int Count
        {
            get { return _innerlist.Count; }
        }
        public void Add(MIA item)
        {
            _innerlist.Add(item);
        }
        public bool Remove(MIA item)
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
        public MIA this[int index]
        {
            get
            { return (MIA)_innerlist[index]; }
        }
        public bool Contains(MIA item)
        {
            return _innerlist.Contains(item);
        }
        public int IndexOf(MIA item)
        {
            return _innerlist.IndexOf(item);
        }
        public void CopyTo(MIA[] array, int index)
        {
            _innerlist.CopyTo(array, index);
        }

        #endregion

    }

    #endregion

    #region " MOA "

    public class MOA : IDisposable
    {
        #region " Constructor & Destructor "

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

        ~MOA()
        {
            Dispose(false);
        }

        #endregion

        #region " Private Variables "

        private Int64 nMOAID;
        private Int64 nERAFileID;
        private Int64 nISAID;
        private Int64 nBPRID;
        private Int64 nCLPID;
        private string sMOA01_Percent;
        private string sMOA02_Amount;
        private string sMOA03_Ref_ID;
        private string sMOA04_Ref_ID;
        private string sMOA05_Ref_ID;
        private string sMOA06_Ref_ID;
        private string sMOA07_Ref_ID;
        private string sMOA08_Amount;
        private string sMOA09_Amount;

        #endregion

        #region " Public Properties "

        public Int64 MOAID
        {
            get { return nMOAID; }
            set { nMOAID = value; }
        }
        public Int64 ERAFileID
        {
            get { return nERAFileID; }
            set { nERAFileID = value; }
        }
        public Int64 ISAID
        {
            get { return nISAID; }
            set { nISAID = value; }
        }
        public Int64 BPRID
        {
            get { return nBPRID; }
            set { nBPRID = value; }
        }
        public Int64 CLPID
        {
            get { return nCLPID; }
            set { nCLPID = value; }
        }
        public string MOA01_Percent
        {
            get { return sMOA01_Percent; }
            set { sMOA01_Percent = value; }
        }
        public string MOA02_Amount
        {
            get { return sMOA02_Amount; }
            set { sMOA02_Amount = value; }
        }
        public string MOA03_Ref_ID
        {
            get { return sMOA03_Ref_ID; }
            set { sMOA03_Ref_ID = value; }
        }
        public string MOA04_Ref_ID
        {
            get { return sMOA04_Ref_ID; }
            set { sMOA04_Ref_ID = value; }
        }
        public string MOA05_Ref_ID
        {
            get { return sMOA05_Ref_ID; }
            set { sMOA05_Ref_ID = value; }
        }
        public string MOA06_Ref_ID
        {
            get { return sMOA06_Ref_ID; }
            set { sMOA06_Ref_ID = value; }
        }
        public string MOA07_Ref_ID
        {
            get { return sMOA07_Ref_ID; }
            set { sMOA07_Ref_ID = value; }
        }
        public string MOA08_Amount
        {
            get { return sMOA08_Amount; }
            set { sMOA08_Amount = value; }
        }
        public string MOA09_Amount
        {
            get { return sMOA09_Amount; }
            set { sMOA09_Amount = value; }
        }


        #endregion
    }

    public class MOAs : IDisposable
    {
        protected ArrayList _innerlist;

        #region " Constructor & Distructor "

        public MOAs()
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
                    _innerlist = null;
                }
            }
            disposed = true;
        }
        ~MOAs()
        {
            Dispose(false);
        }

        #endregion

        #region " Methods Add, Remove, Count, Item "

        public int Count
        {
            get { return _innerlist.Count; }
        }
        public void Add(MOA item)
        {
            _innerlist.Add(item);
        }
        public bool Remove(MOA item)
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
        public MOA this[int index]
        {
            get
            { return (MOA)_innerlist[index]; }
        }
        public bool Contains(MOA item)
        {
            return _innerlist.Contains(item);
        }
        public int IndexOf(MOA item)
        {
            return _innerlist.IndexOf(item);
        }
        public void CopyTo(MOA[] array, int index)
        {
            _innerlist.CopyTo(array, index);
        }

        #endregion

    }

    #endregion

    #region " REF "

    public class REF : IDisposable
    {
        #region " Constructor & Destructor "

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

        ~REF()
        {
            Dispose(false);
        }

        #endregion

        #region " Private Variables "

        private Int64 nREFID;
        private Int64 nERAFileID;
        private Int64 nISAID;
        private Int64 nBPRID;
        private Int64 nPayerPayeeID;
        private Int64 nCLPID;
        private Int64 nSVCID;
        private string sREF01_Ref_IDQual;
        private string sREF02_Ref_ID;
        private string sREF03_Desc;
        private string sREF04_Ref_ID;
        private enumREF_Type _REFType;

        #endregion

        #region " Public Properties "

        public Int64 REFID
        {
            get { return nREFID; }
            set { nREFID = value; }
        }
        public Int64 ERAFileID
        {
            get { return nERAFileID; }
            set { nERAFileID = value; }
        }
        public Int64 ISAID
        {
            get { return nISAID; }
            set { nISAID = value; }
        }
        public Int64 BPRID
        {
            get { return nBPRID; }
            set { nBPRID = value; }
        }
        public Int64 PayerPayeeID
        {
            get { return nPayerPayeeID; }
            set { nPayerPayeeID = value; }
        }
        public Int64 CLPID
        {
            get { return nCLPID; }
            set { nCLPID = value; }
        }
        public Int64 SVCID
        {
            get { return nSVCID; }
            set { nSVCID = value; }
        }
        public string REF01_Ref_IDQual
        {
            get { return sREF01_Ref_IDQual; }
            set { sREF01_Ref_IDQual = value; }
        }
        public string REF02_Ref_ID
        {
            get { return sREF02_Ref_ID; }
            set { sREF02_Ref_ID = value; }
        }
        public string REF03_Desc
        {
            get { return sREF03_Desc; }
            set { sREF03_Desc = value; }
        }
        public string REF04_Ref_ID
        {
            get { return sREF04_Ref_ID; }
            set { sREF04_Ref_ID = value; }
        }
        public enumREF_Type REFType
        {
            get { return _REFType; }
            set { _REFType = value; }
        }

        #endregion
    }

    public class REFs : IDisposable
    {
        protected ArrayList _innerlist;

        #region " Constructor & Distructor "

        public REFs()
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
                    _innerlist = null;
                }
            }
            disposed = true;
        }
        ~REFs()
        {
            Dispose(false);
        }

        #endregion

        #region " Methods Add, Remove, Count, Item "

        public int Count
        {
            get { return _innerlist.Count; }
        }
        public void Add(REF item)
        {
            _innerlist.Add(item);
        }
        public bool Remove(REF item)
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
        public REF this[int index]
        {
            get
            { return (REF)_innerlist[index]; }
        }
        public bool Contains(REF item)
        {
            return _innerlist.Contains(item);
        }
        public int IndexOf(REF item)
        {
            return _innerlist.IndexOf(item);
        }
        public void CopyTo(REF[] array, int index)
        {
            _innerlist.CopyTo(array, index);
        }

        #endregion

    }

    #endregion


    #region " RDM "

    public class RDM : IDisposable
    {
        #region " Constructor & Destructor "

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

        ~RDM()
        {
            Dispose(false);
        }

        #endregion

        #region " Private Variables "

        private Int64 nRDMID;
        private Int64 nERAFileID;
        private Int64 nISAID;
        private Int64 nBPRID;        
        
        private string sRDM01_ReportTranCode;
        private string sRDM02_Name;
        private string sRDM03_CommunicationNumber;
        private string sRDM04_REF_ID;
        private string sRDM05_REF_ID;

        #endregion

        #region " Public Properties "

        public Int64 RDMID
        {
            get { return nRDMID; }
            set { nRDMID = value; }
        }
        public Int64 ERAFileID
        {
            get { return nERAFileID; }
            set { nERAFileID = value; }
        }
        public Int64 ISAID
        {
            get { return nISAID; }
            set { nISAID = value; }
        }
        public Int64 BPRID
        {
            get { return nBPRID; }
            set { nBPRID = value; }
        }

        public string RDM01_ReportTranCode
        {
            get { return sRDM01_ReportTranCode; }
            set { sRDM01_ReportTranCode = value; }
        }
        public string RDM02_Name
        {
            get { return sRDM02_Name; }
            set { sRDM02_Name = value; }
        }
        public string RDM03_CommunicationNumber
        {
            get { return sRDM03_CommunicationNumber; }
            set { sRDM03_CommunicationNumber = value; }
        }
        public string RDM04_REF_ID
        {
            get { return sRDM04_REF_ID; }
            set { sRDM04_REF_ID = value; }
        }
        public string RDM05_REF_ID
        {
            get { return sRDM05_REF_ID; }
            set { sRDM05_REF_ID = value; }
        }

        #endregion
    }

    public class RDMs : IDisposable
    {
        protected ArrayList _innerlist;

        #region " Constructor & Distructor "

        public RDMs()
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
                    _innerlist = null;
                }
            }
            disposed = true;
        }
        ~RDMs()
        {
            Dispose(false);
        }

        #endregion

        #region " Methods Add, Remove, Count, Item "

        public int Count
        {
            get { return _innerlist.Count; }
        }
        public void Add(RDM item)
        {
            _innerlist.Add(item);
        }
        public bool Remove(RDM item)
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
        public RDM this[int index]
        {
            get
            { return (RDM)_innerlist[index]; }
        }
        public bool Contains(RDM item)
        {
            return _innerlist.Contains(item);
        }
        public int IndexOf(RDM item)
        {
            return _innerlist.IndexOf(item);
        }
        public void CopyTo(RDM[] array, int index)
        {
            _innerlist.CopyTo(array, index);
        }

        #endregion

    }

    #endregion

    #region " DTM "

    public class DTM : IDisposable
    {
        #region " Constructor & Destructor "

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

        ~DTM()
        {
            Dispose(false);
        }

        #endregion

        #region " Private Variables "

        private Int64 nDTMID;
        private Int64 nERAFileID;
        private Int64 nISAID;
        private Int64 nBPRID;
        private Int64 nCLPID;
        private Int64 nSVCID;
        private string sDTM01_DateTimeQual;
        private string sDTM02_Date;
        private string sDTM03_Time;
        private string sDTM04_TimeCode;
        private string sDTM05_DateTimePeriodFormatQual;
        private string sDTM06_DateTimePeriod;
        private enumDTM_Type _DTMType;

        #endregion

        #region " Public Properties "

        public Int64 DTMID
        {
            get { return nDTMID; }
            set { nDTMID = value; }
        }
        public Int64 ERAFileID
        {
            get { return nERAFileID; }
            set { nERAFileID = value; }
        }
        public Int64 ISAID
        {
            get { return nISAID; }
            set { nISAID = value; }
        }
        public Int64 BPRID
        {
            get { return nBPRID; }
            set { nBPRID = value; }
        }
        public Int64 CLPID
        {
            get { return nCLPID; }
            set { nCLPID = value; }
        }
        public Int64 SVCID
        {
            get { return nSVCID; }
            set { nSVCID = value; }
        }
        public string DTM01_DateTimeQual
        {
            get { return sDTM01_DateTimeQual; }
            set { sDTM01_DateTimeQual = value; }
        }
        public string DTM02_Date
        {
            get { return sDTM02_Date; }
            set { sDTM02_Date = value; }
        }
        public string DTM03_Time
        {
            get { return sDTM03_Time; }
            set { sDTM03_Time = value; }
        }
        public string DTM04_TimeCode
        {
            get { return sDTM04_TimeCode; }
            set { sDTM04_TimeCode = value; }
        }
        public string DTM05_DateTimePeriodFormatQual
        {
            get { return sDTM05_DateTimePeriodFormatQual; }
            set { sDTM05_DateTimePeriodFormatQual = value; }
        }
        public string DTM06_DateTimePeriod
        {
            get { return sDTM06_DateTimePeriod; }
            set { sDTM06_DateTimePeriod = value; }
        }
        public enumDTM_Type DTMType
        {
            get { return _DTMType; }
            set { _DTMType = value; }
        }

        #endregion
    }

    public class DTMs : IDisposable
    {
        protected ArrayList _innerlist;

        #region " Constructor & Distructor "

        public DTMs()
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
                    _innerlist = null;
                }
            }
            disposed = true;
        }
        ~DTMs()
        {
            Dispose(false);
        }

        #endregion

        #region " Methods Add, Remove, Count, Item "

        public int Count
        {
            get { return _innerlist.Count; }
        }
        public void Add(DTM item)
        {
            _innerlist.Add(item);
        }
        public bool Remove(DTM item)
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
        public DTM this[int index]
        {
            get
            { return (DTM)_innerlist[index]; }
        }
        public bool Contains(DTM item)
        {
            return _innerlist.Contains(item);
        }
        public int IndexOf(DTM item)
        {
            return _innerlist.IndexOf(item);
        }
        public void CopyTo(DTM[] array, int index)
        {
            _innerlist.CopyTo(array, index);
        }

        #endregion

    }

    #endregion

    #region " SVC "

    public class SVC : IDisposable
    {
        #region " Constructor & Destructor "

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
                    if (_CASs != null) { _CASs.Dispose(); _CASs = null; }
                    if (_DTMs != null) { _DTMs.Dispose(); _DTMs = null; }
                    if (_REFs != null) { _REFs.Dispose(); _REFs = null; }
                    if (_AMTs != null) { _AMTs.Dispose(); _AMTs = null; }
                    if (_LQs != null) { _LQs.Dispose(); _LQs = null; }
                }
            }
            disposed = true;
        }

        ~SVC()
        {
            Dispose(false);
        }

        #endregion

        #region " Private Variables "

        private Int64 nSVCID;
        private Int64 nERAFileID;
        private Int64 nISAID;
        private Int64 nBPRID;
        private Int64 nCLPID;
        private string sSVC01_CompositeMedicalProc;
        private string sSVC01_1_ProductServiceIDQual;
        private string sSVC01_2_ProductServiceID;
        private string sSVC01_3_ProcMod;
        private string sSVC01_4_ProcMod;
        private string sSVC01_5_ProcMod;
        private string sSVC01_6_ProcMod;
        private string sSVC01_7_Desc;
        private string sSVC02_Amount;
        private string sSVC03_Amount;
        private string sSVC04_ProductServiceID;
        private string sSVC05_Qty;
        private string sSVC06_CompositeMedicalProc;
        private string sSVC06_1_ProductServiceIDQual;
        private string sSVC06_2_ProductServiceID;
        private string sSVC06_3_ProcMod;
        private string sSVC06_4_ProcMod;
        private string sSVC06_5_ProcMod;
        private string sSVC06_6_ProcMod;
        private string sSVC06_7_Desc;
        private string sSVC07_Qty;

        private CASs _CASs = new CASs();
        private DTMs _DTMs = new DTMs();
        private REFs _REFs = new REFs();
        private AMTs _AMTs = new AMTs();
        private LQs _LQs = new LQs();
        private int nChargeIndex;
        #endregion

        #region " Public Properties "

        public Int64 SVCID
        {
            get { return nSVCID; }
            set { nSVCID = value; }
        }
        public Int64 ERAFileID
        {
            get { return nERAFileID; }
            set { nERAFileID = value; }
        }
        public Int64 ISAID
        {
            get { return nISAID; }
            set { nISAID = value; }
        }
        public Int64 BPRID
        {
            get { return nBPRID; }
            set { nBPRID = value; }
        }
        public Int64 CLPID
        {
            get { return nCLPID; }
            set { nCLPID = value; }
        }
        public string SVC01_CompositeMedicalProc
        {
            get { return sSVC01_CompositeMedicalProc; }
            set { sSVC01_CompositeMedicalProc = value; }
        }
        public string SVC01_1_ProductServiceIDQual
        {
            get { return sSVC01_1_ProductServiceIDQual; }
            set { sSVC01_1_ProductServiceIDQual = value; }
        }
        public string SVC01_2_ProductServiceID
        {
            get { return sSVC01_2_ProductServiceID; }
            set { sSVC01_2_ProductServiceID = value; }
        }
        public string SVC01_3_ProcMod
        {
            get { return sSVC01_3_ProcMod; }
            set { sSVC01_3_ProcMod = value; }
        }
        public string SVC01_4_ProcMod
        {
            get { return sSVC01_4_ProcMod; }
            set { sSVC01_4_ProcMod = value; }
        }
        public string SVC01_5_ProcMod
        {
            get { return sSVC01_5_ProcMod; }
            set { sSVC01_5_ProcMod = value; }
        }
        public string SVC01_6_ProcMod
        {
            get { return sSVC01_6_ProcMod; }
            set { sSVC01_6_ProcMod = value; }
        }
        public string SVC01_7_Desc
        {
            get { return sSVC01_7_Desc; }
            set { sSVC01_7_Desc = value; }
        }
        public string SVC02_Amount
        {
            get { return sSVC02_Amount; }
            set { sSVC02_Amount = value; }
        }
        public string SVC03_Amount
        {
            get { return sSVC03_Amount; }
            set { sSVC03_Amount = value; }
        }
        public string SVC04_ProductServiceID
        {
            get { return sSVC04_ProductServiceID; }
            set { sSVC04_ProductServiceID = value; }
        }
        public string SVC05_Qty
        {
            get { return sSVC05_Qty; }
            set { sSVC05_Qty = value; }
        }
        public string SVC06_CompositeMedicalProc
        {
            get { return sSVC06_CompositeMedicalProc; }
            set { sSVC06_CompositeMedicalProc = value; }
        }
        public string SVC06_1_ProductServiceIDQual
        {
            get { return sSVC06_1_ProductServiceIDQual; }
            set { sSVC06_1_ProductServiceIDQual = value; }
        }
        public string SVC06_2_ProductServiceID
        {
            get { return sSVC06_2_ProductServiceID; }
            set { sSVC06_2_ProductServiceID = value; }
        }
        public string SVC06_3_ProcMod
        {
            get { return sSVC06_3_ProcMod; }
            set { sSVC06_3_ProcMod = value; }
        }
        public string SVC06_4_ProcMod
        {
            get { return sSVC06_4_ProcMod; }
            set { sSVC06_4_ProcMod = value; }
        }
        public string SVC06_5_ProcMod
        {
            get { return sSVC06_5_ProcMod; }
            set { sSVC06_5_ProcMod = value; }
        }
        public string SVC06_6_ProcMod
        {
            get { return sSVC06_6_ProcMod; }
            set { sSVC06_6_ProcMod = value; }
        }
        public string SVC06_7_Desc
        {
            get { return sSVC06_7_Desc; }
            set { sSVC06_7_Desc = value; }
        }
        public string SVC07_Qty
        {
            get { return sSVC07_Qty; }
            set { sSVC07_Qty = value; }
        }

        public CASs oCASs
        {
            get { return _CASs; }
            set { _CASs = value; }
        }
        public DTMs oDTMs
        {
            get { return _DTMs; }
            set { _DTMs = value; }
        }
        public REFs oREFs
        {
            get { return _REFs; }
            set { _REFs = value; }
        }
        public AMTs oAMTs
        {
            get { return _AMTs; }
            set { _AMTs = value; }
        }
        public LQs oLQs
        {
            get { return _LQs; }
            set { _LQs = value; }
        }

        public int ChargeIndex
        {
            get { return nChargeIndex; }
            set { nChargeIndex = value; }
        }
        

        #endregion
    }

    public class SVCs : IDisposable
    {
        protected ArrayList _innerlist;

        #region " Constructor & Distructor "

        public SVCs()
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
                    _innerlist = null;
                }
            }
            disposed = true;
        }
        ~SVCs()
        {
            Dispose(false);
        }

        #endregion

        #region " Methods Add, Remove, Count, Item "

        public int Count
        {
            get { return _innerlist.Count; }
        }
        public void Add(SVC item)
        {
            _innerlist.Add(item);
        }
        public bool Remove(SVC item)
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
        public SVC this[int index]
        {
            get
            { return (SVC)_innerlist[index]; }
        }
        public bool Contains(SVC item)
        {
            return _innerlist.Contains(item);
        }
        public int IndexOf(SVC item)
        {
            return _innerlist.IndexOf(item);
        }
        public void CopyTo(SVC[] array, int index)
        {
            _innerlist.CopyTo(array, index);
        }

        #endregion

    }

    #endregion

    #region " AMT "

    public class AMT : IDisposable
    {
        #region " Constructor & Destructor "

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

        ~AMT()
        {
            Dispose(false);
        }

        #endregion

        #region " Private Variables "

        private Int64 nAMTID;
        private Int64 nERAFileID;
        private Int64 nISAID;
        private Int64 nBPRID;
        private Int64 nCLPID;
        private Int64 nSVCID;
        private string sAMT01_AmountQualCode;
        private string sAMT02_Amount;
        private string sAMT03_CreditDebitFlagCode;
        private enumAMT_Type _AMTType;

        #endregion

        #region " Public Properties "

        public Int64 AMTID
        {
            get { return nAMTID; }
            set { nAMTID = value; }
        }
        public Int64 ERAFileID
        {
            get { return nERAFileID; }
            set { nERAFileID = value; }
        }
        public Int64 ISAID
        {
            get { return nISAID; }
            set { nISAID = value; }
        }
        public Int64 BPRID
        {
            get { return nBPRID; }
            set { nBPRID = value; }
        }
        public Int64 CLPID
        {
            get { return nCLPID; }
            set { nCLPID = value; }
        }
        public Int64 SVCID
        {
            get { return nSVCID; }
            set { nSVCID = value; }
        }
        public string AMT01_AmountQualCode
        {
            get { return sAMT01_AmountQualCode; }
            set { sAMT01_AmountQualCode = value; }
        }
        public string AMT02_Amount
        {
            get { return sAMT02_Amount; }
            set { sAMT02_Amount = value; }
        }
        public string AMT03_CreditDebitFlagCode
        {
            get { return sAMT03_CreditDebitFlagCode; }
            set { sAMT03_CreditDebitFlagCode = value; }
        }
        public enumAMT_Type AMTType
        {
            get { return _AMTType; }
            set { _AMTType = value; }
        }

        #endregion
    }

    public class AMTs : IDisposable
    {
        protected ArrayList _innerlist;

        #region " Constructor & Distructor "

        public AMTs()
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
                    _innerlist = null;
                }
            }
            disposed = true;
        }
        ~AMTs()
        {
            Dispose(false);
        }

        #endregion

        #region " Methods Add, Remove, Count, Item "

        public int Count
        {
            get { return _innerlist.Count; }
        }
        public void Add(AMT item)
        {
            _innerlist.Add(item);
        }
        public bool Remove(AMT item)
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
        public AMT this[int index]
        {
            get
            { return (AMT)_innerlist[index]; }
        }
        public bool Contains(AMT item)
        {
            return _innerlist.Contains(item);
        }
        public int IndexOf(AMT item)
        {
            return _innerlist.IndexOf(item);
        }
        public void CopyTo(AMT[] array, int index)
        {
            _innerlist.CopyTo(array, index);
        }

        #endregion

    }

    #endregion

    #region " NM1 "

    public class NM1 : IDisposable
    {
        #region " Constructor & Destructor "

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

        ~NM1()
        {
            Dispose(false);
        }

        #endregion

        #region " Private Variables "

        private Int64 nNM1ID;
        private Int64 nERAFileID;
        private Int64 nISAID;
        private Int64 nBPRID;
        private Int64 nCLPID;
        private string sNM101_EntityIDCode;
        private string sNM102_EntityTypeQual;
        private string sNM103_NameLastOrOrgName;
        private string sNM104_NameFirst;
        private string sNM105_NameMiddle;
        private string sNM106_NamePrefix;
        private string sNM107_NameSuffix;
        private string sNM108_IDCodeQual;
        private string sNM109_IDCode;
        private string sNM110_EntityRelationCode;
        private string sNM111_EntityIDCode;
        private enumNM1_Type _NM1Type;

        #endregion

        #region " Public Properties "

        public Int64 NM1ID
        {
            get { return nNM1ID; }
            set { nNM1ID = value; }
        }
        public Int64 ERAFileID
        {
            get { return nERAFileID; }
            set { nERAFileID = value; }
        }
        public Int64 ISAID
        {
            get { return nISAID; }
            set { nISAID = value; }
        }
        public Int64 BPRID
        {
            get { return nBPRID; }
            set { nBPRID = value; }
        }
        public Int64 CLPID
        {
            get { return nCLPID; }
            set { nCLPID = value; }
        }
        public string NM101_EntityIDCode
        {
            get { return sNM101_EntityIDCode; }
            set { sNM101_EntityIDCode = value; }
        }
        public string NM102_EntityTypeQual
        {
            get { return sNM102_EntityTypeQual; }
            set { sNM102_EntityTypeQual = value; }
        }
        public string NM103_NameLastOrOrgName
        {
            get { return sNM103_NameLastOrOrgName; }
            set { sNM103_NameLastOrOrgName = value; }
        }
        public string NM104_NameFirst
        {
            get { return sNM104_NameFirst; }
            set { sNM104_NameFirst = value; }
        }
        public string NM105_NameMiddle
        {
            get { return sNM105_NameMiddle; }
            set { sNM105_NameMiddle = value; }
        }
        public string NM106_NamePrefix
        {
            get { return sNM106_NamePrefix; }
            set { sNM106_NamePrefix = value; }
        }
        public string NM107_NameSuffix
        {
            get { return sNM107_NameSuffix; }
            set { sNM107_NameSuffix = value; }
        }
        public string NM108_IDCodeQual
        {
            get { return sNM108_IDCodeQual; }
            set { sNM108_IDCodeQual = value; }
        }
        public string NM109_IDCode
        {
            get { return sNM109_IDCode; }
            set { sNM109_IDCode = value; }
        }
        public string NM110_EntityRelationCode
        {
            get { return sNM110_EntityRelationCode; }
            set { sNM110_EntityRelationCode = value; }
        }
        public string NM111_EntityIDCode
        {
            get { return sNM111_EntityIDCode; }
            set { sNM111_EntityIDCode = value; }
        }
        public enumNM1_Type NM1Type
        {
            get { return _NM1Type; }
            set { _NM1Type = value; }
        }

        #endregion
    }

    public class NM1s : IDisposable
    {
        protected ArrayList _innerlist;

        #region " Constructor & Distructor "

        public NM1s()
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
                    _innerlist = null;
                }
            }
            disposed = true;
        }
        ~NM1s()
        {
            Dispose(false);
        }

        #endregion

        #region " Methods Add, Remove, Count, Item "

        public int Count
        {
            get { return _innerlist.Count; }
        }
        public void Add(NM1 item)
        {
            _innerlist.Add(item);
        }
        public bool Remove(NM1 item)
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
        public NM1 this[int index]
        {
            get
            { return (NM1)_innerlist[index]; }
        }
        public bool Contains(NM1 item)
        {
            return _innerlist.Contains(item);
        }
        public int IndexOf(NM1 item)
        {
            return _innerlist.IndexOf(item);
        }
        public void CopyTo(NM1[] array, int index)
        {
            _innerlist.CopyTo(array, index);
        }

        #endregion

    }

    #endregion

    #region " CAS "

    public class CAS : IDisposable
    {
        #region " Constructor & Destructor "

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

        ~CAS()
        {
            Dispose(false);
        }

        #endregion

        #region " Private Variables "

        private Int64 nCASID;
        private Int64 nERAFileID;
        private Int64 nISAID;
        private Int64 nBPRID;
        private Int64 nCLPID;
        private Int64 nSVCID;
        private string sCAS01_ClaimAdjustGroupCode;
        private string sCAS02_ClaimAdjustReasonCode;
        private string sCAS03_Amount;
        private string sCAS04_Qty;
        private string sCAS05_ClaimAdjustReasonCode;
        private string sCAS06_Amount;
        private string sCAS07_Qty;
        private string sCAS08_ClaimAdjustReasonCode;
        private string sCAS09_Amount;
        private string sCAS10_Qty;
        private string sCAS11_ClaimAdjustReasonCode;
        private string sCAS12_Amount;
        private string sCAS13_Qty;
        private string sCAS14_ClaimAdjustReasonCode;
        private string sCAS15_Amount;
        private string sCAS16_Qty;
        private string sCAS17_ClaimAdjustReasonCode;
        private string sCAS18_Amount;
        private string sCAS19_Qty;
        private enumCAS_Type _CASType;

        #endregion

        #region " Public Properties "

        public Int64 CASID
        {
            get { return nCASID; }
            set { nCASID = value; }
        }
        public Int64 ERAFileID
        {
            get { return nERAFileID; }
            set { nERAFileID = value; }
        }
        public Int64 ISAID
        {
            get { return nISAID; }
            set { nISAID = value; }
        }
        public Int64 BPRID
        {
            get { return nBPRID; }
            set { nBPRID = value; }
        }
        public Int64 CLPID
        {
            get { return nCLPID; }
            set { nCLPID = value; }
        }
        public Int64 SVCID
        {
            get { return nSVCID; }
            set { nSVCID = value; }
        }
        public string CAS01_ClaimAdjustGroupCode
        {
            get { return sCAS01_ClaimAdjustGroupCode; }
            set { sCAS01_ClaimAdjustGroupCode = value; }
        }
        public string CAS02_ClaimAdjustReasonCode
        {
            get { return sCAS02_ClaimAdjustReasonCode; }
            set { sCAS02_ClaimAdjustReasonCode = value; }
        }
        public string CAS03_Amount
        {
            get { return sCAS03_Amount; }
            set { sCAS03_Amount = value; }
        }
        public string CAS04_Qty
        {
            get { return sCAS04_Qty; }
            set { sCAS04_Qty = value; }
        }
        public string CAS05_ClaimAdjustReasonCode
        {
            get { return sCAS05_ClaimAdjustReasonCode; }
            set { sCAS05_ClaimAdjustReasonCode = value; }
        }
        public string CAS06_Amount
        {
            get { return sCAS06_Amount; }
            set { sCAS06_Amount = value; }
        }
        public string CAS07_Qty
        {
            get { return sCAS07_Qty; }
            set { sCAS07_Qty = value; }
        }
        public string CAS08_ClaimAdjustReasonCode
        {
            get { return sCAS08_ClaimAdjustReasonCode; }
            set { sCAS08_ClaimAdjustReasonCode = value; }
        }
        public string CAS09_Amount
        {
            get { return sCAS09_Amount; }
            set { sCAS09_Amount = value; }
        }
        public string CAS10_Qty
        {
            get { return sCAS10_Qty; }
            set { sCAS10_Qty = value; }
        }
        public string CAS11_ClaimAdjustReasonCode
        {
            get { return sCAS11_ClaimAdjustReasonCode; }
            set { sCAS11_ClaimAdjustReasonCode = value; }
        }
        public string CAS12_Amount
        {
            get { return sCAS12_Amount; }
            set { sCAS12_Amount = value; }
        }
        public string CAS13_Qty
        {
            get { return sCAS13_Qty; }
            set { sCAS13_Qty = value; }
        }
        public string CAS14_ClaimAdjustReasonCode
        {
            get { return sCAS14_ClaimAdjustReasonCode; }
            set { sCAS14_ClaimAdjustReasonCode = value; }
        }
        public string CAS15_Amount
        {
            get { return sCAS15_Amount; }
            set { sCAS15_Amount = value; }
        }
        public string CAS16_Qty
        {
            get { return sCAS16_Qty; }
            set { sCAS16_Qty = value; }
        }
        public string CAS17_ClaimAdjustReasonCode
        {
            get { return sCAS17_ClaimAdjustReasonCode; }
            set { sCAS17_ClaimAdjustReasonCode = value; }
        }
        public string CAS18_Amount
        {
            get { return sCAS18_Amount; }
            set { sCAS18_Amount = value; }
        }
        public string CAS19_Qty
        {
            get { return sCAS19_Qty; }
            set { sCAS19_Qty = value; }
        }
        public enumCAS_Type CASType
        {
            get { return _CASType; }
            set { _CASType = value; }
        }

        #endregion
    }

    public class CASs : IDisposable
    {
        protected ArrayList _innerlist;

        #region " Constructor & Distructor "

        public CASs()
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
                    _innerlist = null;
                }
            }
            disposed = true;
        }
        ~CASs()
        {
            Dispose(false);
        }

        #endregion

        #region " Methods Add, Remove, Count, Item "

        public int Count
        {
            get { return _innerlist.Count; }
        }
        public void Add(CAS item)
        {
            _innerlist.Add(item);
        }
        public bool Remove(CAS item)
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
        public CAS this[int index]
        {
            get
            { return (CAS)_innerlist[index]; }
        }
        public bool Contains(CAS item)
        {
            return _innerlist.Contains(item);
        }
        public int IndexOf(CAS item)
        {
            return _innerlist.IndexOf(item);
        }
        public void CopyTo(CAS[] array, int index)
        {
            _innerlist.CopyTo(array, index);
        }

        #endregion

    }

    #endregion

    #region " LQ "

    public class LQ : IDisposable
    {
        #region " Constructor & Destructor "

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

        ~LQ()
        {
            Dispose(false);
        }

        #endregion

        #region " Private Variables "

        private Int64 nLQID;
        private Int64 nERAFileID;
        private Int64 nISAID;
        private Int64 nBPRID;
        private Int64 nCLPID;
        private Int64 nSVCID;
        private string sLQ01_CodeListQualCode;
        private string sLQ02_IndustryCode;

        #endregion

        #region " Public Properties "

        public Int64 LQID
        {
            get { return nLQID; }
            set { nLQID = value; }
        }
        public Int64 ERAFileID
        {
            get { return nERAFileID; }
            set { nERAFileID = value; }
        }
        public Int64 ISAID
        {
            get { return nISAID; }
            set { nISAID = value; }
        }
        public Int64 BPRID
        {
            get { return nBPRID; }
            set { nBPRID = value; }
        }
        public Int64 CLPID
        {
            get { return nCLPID; }
            set { nCLPID = value; }
        }
        public Int64 SVCID
        {
            get { return nSVCID; }
            set { nSVCID = value; }
        }
        public string LQ01_CodeListQualCode
        {
            get { return sLQ01_CodeListQualCode; }
            set { sLQ01_CodeListQualCode = value; }
        }
        public string LQ02_IndustryCode
        {
            get { return sLQ02_IndustryCode; }
            set { sLQ02_IndustryCode = value; }
        }


        #endregion
    }

    public class LQs : IDisposable
    {
        protected ArrayList _innerlist;

        #region " Constructor & Distructor "

        public LQs()
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
                    _innerlist = null;
                }
            }
            disposed = true;
        }
        ~LQs()
        {
            Dispose(false);
        }

        #endregion

        #region " Methods Add, Remove, Count, Item "

        public int Count
        {
            get { return _innerlist.Count; }
        }
        public void Add(LQ item)
        {
            _innerlist.Add(item);
        }
        public bool Remove(LQ item)
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
        public LQ this[int index]
        {
            get
            { return (LQ)_innerlist[index]; }
        }
        public bool Contains(LQ item)
        {
            return _innerlist.Contains(item);
        }
        public int IndexOf(LQ item)
        {
            return _innerlist.IndexOf(item);
        }
        public void CopyTo(LQ[] array, int index)
        {
            _innerlist.CopyTo(array, index);
        }

        #endregion

    }

    #endregion

    #region " PER "

    public class PER : IDisposable
    {
        #region " Constructor & Destructor "

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

        ~PER()
        {
            Dispose(false);
        }

        #endregion

        #region " Private Variables "

        private Int64 nPERID;
        private Int64 nERAFileID;
        private Int64 nISAID;
        private Int64 nBPRID;
        private Int64 nPayerPayeeID;
        private Int64 nCLPID;
        private string sPER01_Cont_Func_Code;
        private string sPER02_Name;
        private string sPER03_CommNoQual;
        private string sPER04_CommNo;
        private string sPER05_CommNoQual;
        private string sPER06_CommNo;
        private string sPER07_CommNoQual;
        private string sPER08_CommNo;
        private string sPER09_Cont_Inq_Ref;
        private enumPER_Type nPERType;

        #endregion

        #region " Public Properties "

        public Int64 PERID
        {
            get { return nPERID; }
            set { nPERID = value; }
        }
        public Int64 ERAFileID
        {
            get { return nERAFileID; }
            set { nERAFileID = value; }
        }
        public Int64 ISAID
        {
            get { return nISAID; }
            set { nISAID = value; }
        }
        public Int64 BPRID
        {
            get { return nBPRID; }
            set { nBPRID = value; }
        }
        public Int64 PayerPayeeID
        {
            get { return nPayerPayeeID; }
            set { nPayerPayeeID = value; }
        }
        public Int64 CLPID
        {
            get { return nCLPID; }
            set { nCLPID = value; }
        }
        public string PER01_Cont_Func_Code
        {
            get { return sPER01_Cont_Func_Code; }
            set { sPER01_Cont_Func_Code = value; }
        }
        public string PER02_Name
        {
            get { return sPER02_Name; }
            set { sPER02_Name = value; }
        }
        public string PER03_CommNoQual
        {
            get { return sPER03_CommNoQual; }
            set { sPER03_CommNoQual = value; }
        }
        public string PER04_CommNo
        {
            get { return sPER04_CommNo; }
            set { sPER04_CommNo = value; }
        }
        public string PER05_CommNoQual
        {
            get { return sPER05_CommNoQual; }
            set { sPER05_CommNoQual = value; }
        }
        public string PER06_CommNo
        {
            get { return sPER06_CommNo; }
            set { sPER06_CommNo = value; }
        }
        public string PER07_CommNoQual
        {
            get { return sPER07_CommNoQual; }
            set { sPER07_CommNoQual = value; }
        }
        public string PER08_CommNo
        {
            get { return sPER08_CommNo; }
            set { sPER08_CommNo = value; }
        }
        public string PER09_Cont_Inq_Ref
        {
            get { return sPER09_Cont_Inq_Ref; }
            set { sPER09_Cont_Inq_Ref = value; }
        }
        public enumPER_Type PERType
        {
            get { return nPERType; }
            set { nPERType = value; }
        }


        #endregion
    }

    public class PERs : IDisposable
    {
        protected ArrayList _innerlist;

        #region " Constructor & Destructor "

        public PERs()
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
                    _innerlist = null;
                }
            }
            disposed = true;
        }
        ~PERs()
        {
            Dispose(false);
        }

        #endregion

        #region " Methods Add, Remove, Count, Item "

        public int Count
        {
            get { return _innerlist.Count; }
        }
        public void Add(PER item)
        {
            _innerlist.Add(item);
        }
        public bool Remove(PER item)
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
        public PER this[int index]
        {
            get
            { return (PER)_innerlist[index]; }
        }
        public bool Contains(PER item)
        {
            return _innerlist.Contains(item);
        }
        public int IndexOf(PER item)
        {
            return _innerlist.IndexOf(item);
        }
        public void CopyTo(PER[] array, int index)
        {
            _innerlist.CopyTo(array, index);
        }

        #endregion

    }

    #endregion

    #endregion
}
