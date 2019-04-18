using System;
using System.Collections.Generic;
using System.Text;
using gloBilling.Common;
using System.Windows.Forms;
using System.Collections;
using System.Data;
using gloBilling.Collections;

namespace gloBilling
{
    public class clsgloResend : IDisposable
    {
            #region "Constructor & Destructor"

            public clsgloResend()
            {
                _databaseconnectionstring = appSettings["DataBaseConnectionString"].ToString();
                _emrdatabaseconnectionstring = string.Empty;


                #region " Retrieve ClinicID from AppSettings "

                if (appSettings["ClinicID"] != null)
                {
                    if (appSettings["ClinicID"] != "")
                    { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                    else { _ClinicID = 0; }
                }
                else
                { _ClinicID = 0; }

                #endregion

                #region " Retrive UserID from appSettings "

                if (appSettings["UserID"] != null)
                {
                    if (appSettings["UserID"] != "")
                    {
                        _UserID = Convert.ToInt64(appSettings["UserID"]);
                    }
                }
                else
                {
                    _UserID = 0;
                }

                #endregion

                #region " Retrive UserName from appSettings "

                if (appSettings["UserName"] != null)
                {
                    if (appSettings["UserName"] != "")
                    {
                        _UserName = Convert.ToString(appSettings["UserName"]);
                    }
                }
                else
                {
                    _UserName = "";
                }

                #endregion

                #region " Retrieve MessageBoxCaption from AppSettings "

                if (appSettings["MessageBOXCaption"] != null)
                {
                    if (appSettings["MessageBOXCaption"] != "")
                    {
                        _messageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                    }
                    else
                    {
                        _messageBoxCaption = "";
                    }
                }
                else
                { _messageBoxCaption = ""; }

                #endregion
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

            ~clsgloResend()
            {
                Dispose(false);
            }

            #endregion

            #region "Variables"

            public Int64 nBatchID = 0;
            public string sBatchName = "";
            public Int64 nTransactionID = 0;
            public Int64 nClinicID = 0;
            System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

            //SaveElectronicClaim:-UserID,ClinicID
            private string _AlphaIIValidation = "";
            bool _ShowMessageForValidation = false;
            Int64 _ClinicID = 0;
            Int64 _UserID = 0;
            string _UserName = "";
            private bool _IsCheckInvalidICD9 = false;
            private string _AlphaIIServerName = "";
            private string _AlphaIIDatabase = "";
            private string _AlphaIIUserName = "";
            private string _AlphaIIPassword = "";
            private string _AlphaIIAuthentication = "";
            //private string _AlphaIIValidation = "";
            ArrayList oClaimNoArray = null;
            private bool _IsScrubber = false;
            private bool _IsAplhaIIValidated = true;
            string _databaseconnectionstring =  "";
            string _emrdatabaseconnectionstring = "";
            string _messageBoxCaption = "";
            #endregion "Variables"


            public bool ResendClaim(Int64 _nTransactionID, Transaction oTransaction, bool _IsHold,string sClaimRemittanceRefNo) //Check for Otransaction
            {
                bool _result = false;
             //   object retVal = null;
                if (oTransaction.Transaction_Status == TransactionStatus.SendToClaimManager || oTransaction.Transaction_Status == TransactionStatus.SendToClearingHouse)
                {

                    gloDatabaseLayer.DBLayer ODB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                    gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
                    try
                    {

                        oDBParameters.Add("@TransactionMasterID", oTransaction.TransactionMasterID, ParameterDirection.Input, SqlDbType.BigInt);
                        oDBParameters.Add("@TransactionID", oTransaction.TransactionID, ParameterDirection.Input, SqlDbType.BigInt);
                        oDBParameters.Add("@IsPaymentDone", false, ParameterDirection.Input, SqlDbType.Bit);
                        oDBParameters.Add("@EOBPaymentID", 0, ParameterDirection.Input, SqlDbType.BigInt);
                        oDBParameters.Add("@EOBID", 0, ParameterDirection.Input, SqlDbType.BigInt);
                        oDBParameters.Add("@ClaimRemittanceReferenceNo", sClaimRemittanceRefNo, ParameterDirection.Input, SqlDbType.VarChar);
                        oDBParameters.Add("@Message", 0, ParameterDirection.Output, SqlDbType.BigInt);
                        ODB.Connect(false);
                        Hashtable oOutPut = ODB.Execute("BL_SplitTransactionClaim_ERA", oDBParameters, true);

                        String sql_query = "update BL_Transaction_Claim_MST WITH (READPAST) set nStatus=" + TransactionStatus.Resent.GetHashCode() + " , nClaimStatus=" + ClaimStatus.Close.GetHashCode() + "  where nTransactionID = " + oTransaction.TransactionID + " and nTransactionMasterID = " + oTransaction.TransactionMasterID + " and nClinicID=" + _ClinicID;
                        ODB.Execute_Query(sql_query);
                        ODB.Disconnect();

                        #region "Claim Insurance Follow Up Operations"

                        Int64 nNewTransactionID = 0;
                        if (oOutPut["@Message"] != null && Convert.ToString(oOutPut["@Message"]) != string.Empty)
                        {
                            Int64.TryParse(Convert.ToString(oOutPut["@Message"]), out nNewTransactionID);
                        }

                        if (nNewTransactionID != 0) 
                        {
                            // CL_FollowUpCode.ClearInsuranceClaimFollowUp(nNewTransactionID, oTransaction.TransactionID); 
                            ODB.Connect(false);
                            sql_query = "update BL_Transaction_Claim_MST WITH (READPAST) set nStatus=" + TransactionStatus.Queue.GetHashCode() + " , nClaimStatus=" + ClaimStatus.Open.GetHashCode() + "  where nTransactionID = " + nNewTransactionID + " and nTransactionMasterID = " + oTransaction.TransactionMasterID + " and nClinicID=" + _ClinicID;
                            ODB.Execute_Query(sql_query);
                            ODB.Disconnect();
                            CL_FollowUpCode objCL_FollowUpCode = new CL_FollowUpCode();
                            objCL_FollowUpCode.DeleteFollowUpSchedule(oTransaction.TransactionID);
                            objCL_FollowUpCode.DeleteFollowUpSchedule(nNewTransactionID);
                            objCL_FollowUpCode.Dispose();
                            objCL_FollowUpCode = null;
                        }

                        #endregion

                        _result = true;
                    }
                    catch (Exception ex)
                    {
                        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                    }
                    finally
                    {
                        if (ODB != null) { ODB.Dispose(); ODB = null; }
                    }
                }
                return _result;
            }

            public bool ResendClaim(Int64 _nTransactionID, Int64 TransactionMasterID, TransactionStatus Transaction_Status, string sClaimRemittanceRefNo) //Check for Otransaction
            {
                bool _result = false;             
                if (Transaction_Status == TransactionStatus.SendToClaimManager || Transaction_Status == TransactionStatus.SendToClearingHouse)
                {

                    gloDatabaseLayer.DBLayer ODB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                    gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
                    try
                    {

                        oDBParameters.Add("@TransactionMasterID", TransactionMasterID, ParameterDirection.Input, SqlDbType.BigInt);
                        oDBParameters.Add("@TransactionID", _nTransactionID, ParameterDirection.Input, SqlDbType.BigInt);
                        oDBParameters.Add("@IsPaymentDone", false, ParameterDirection.Input, SqlDbType.Bit);
                        oDBParameters.Add("@EOBPaymentID", 0, ParameterDirection.Input, SqlDbType.BigInt);
                        oDBParameters.Add("@EOBID", 0, ParameterDirection.Input, SqlDbType.BigInt);
                        oDBParameters.Add("@ClaimRemittanceReferenceNo", sClaimRemittanceRefNo, ParameterDirection.Input, SqlDbType.VarChar);
                        oDBParameters.Add("@Message", 0, ParameterDirection.Output, SqlDbType.BigInt);
                        ODB.Connect(false);
                        Hashtable oOutPut = ODB.Execute("BL_SplitTransactionClaim_ERA", oDBParameters, true);
                        String sql_query = "update BL_Transaction_Claim_MST WITH (READPAST) set nStatus=" + TransactionStatus.Resent.GetHashCode() + " , nClaimStatus=" + ClaimStatus.Close.GetHashCode() + "  where nTransactionID = " + _nTransactionID + " and nTransactionMasterID = " + TransactionMasterID + " and nClinicID=" + _ClinicID;
                        ODB.Execute_Query(sql_query);
                        ODB.Disconnect();
                        #region "Claim Insurance Follow Up Operations"

                        Int64 nNewTransactionID = 0;
                        if (oOutPut["@Message"] != null && Convert.ToString(oOutPut["@Message"]) != string.Empty)
                        {
                            Int64.TryParse(Convert.ToString(oOutPut["@Message"]), out nNewTransactionID);
                        }

                        if (nNewTransactionID != 0) 
                        {
                            CL_FollowUpCode objCL_FollowUpCode = new CL_FollowUpCode();
                            objCL_FollowUpCode.DeleteFollowUpSchedule(_nTransactionID);
                            objCL_FollowUpCode.DeleteFollowUpSchedule(nNewTransactionID);
                            objCL_FollowUpCode.Dispose();
                            objCL_FollowUpCode = null;
                        }

                        #endregion
                        _result = true;
                    }
                    catch (Exception ex)
                    {
                        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                    }
                    finally
                    {
                        if (ODB != null) { ODB.Dispose(); ODB = null; }
                    }
                }
                return _result;
            }


 //       public bool ResendClaimOld(Int64 _nTransactionID,Transaction oTransaction,bool _IsHold) //Check for Otransaction
 //       {
 //           //Check clinic ID
            
 //           gloDatabaseLayer.DBLayer ODB = null;
 //            gloBilling ogloBilling=new gloBilling(_databaseconnectionstring,"");
 //            ClsBL_Hold oClsBL_Hold = new ClsBL_Hold(_databaseconnectionstring, _emrdatabaseconnectionstring);
 //            //oTransaction=ogloBilling.GetChargesClaimDetails(_nTransactionID, 1);
 //           DataTable dtBatch = null;
 //           DataTable dtBatchDetail = null;
 //           Int64 _BatchID = 0;
 //           string _BatchName = "";
            
 //           ArrayList _trnsIds = new ArrayList();
 //           gloGeneralItem.gloItems oTrnIds = new gloGeneralItem.gloItems();
 //           gloGeneralItem.gloItem oTrnID;
 //           string _EDIFileName = String.Empty;
 //           bool _result = false;


 //           //Temp Code//
 //           //gloBilling ogloBilling=new gloBilling(_databaseconnectionstring,"");
 //           //oTransaction = ogloBilling.GetChargesClaimDetails(_nTransactionID,1);
 //           try
 //           {
 //               #region GET BATCH DETAILS

 //               string _sqlQuery = "";
 //               ODB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
 //               _sqlQuery = "Select nBatchID,nTransactionID from BL_Transaction_Batch_dtl WITH (NOLOCK) where nBatchID=(Select top 1 nBatchID from BL_Transaction_Batch_dtl where nTransactionID=" + oTransaction.TransactionID + " Order by nBatchID)";
 //               ODB.Connect(false);
 //               ODB.Retrive_Query(_sqlQuery, out dtBatchDetail);
 //               if (dtBatchDetail != null && dtBatchDetail.Rows.Count > 0)
 //               {
 //                   _BatchID = Convert.ToInt64(dtBatchDetail.Rows[0]["nBatchID"]);
 //               }

 //               DataTable dtClaimNo=new DataTable();
 //               for (int i = 0; i < dtBatchDetail.Rows.Count; i++)
 //               {
 //                   _trnsIds.Add(Convert.ToInt64(dtBatchDetail.Rows[i]["nTransactionID"]));
 //                   oTrnID = new gloGeneralItem.gloItem();

 //                   oTrnID.ID = Convert.ToInt64(dtBatchDetail.Rows[i]["nTransactionID"]); // TRANSACTION ID //

 //                   dtClaimNo=getClaimNo(Convert.ToInt64(dtBatchDetail.Rows[i]["nTransactionID"]));

 //                   if(dtClaimNo!=null && dtClaimNo.Rows.Count>0)
 //                   {
 //                       oTrnID.Code = Convert.ToString(dtClaimNo.Rows[0]["nClaimNo"]); // CLAIM NUMBER // 
 //                       oTrnID.Description = Convert.ToString(dtClaimNo.Rows[0]["nSubClaimno"]); // SUBCLAIM NUMBER //
 //                   }

                    
 //                   oTrnIds.Add(oTrnID);
 //               }

 //               _sqlQuery = " Select sBatchName from dbo.BL_Transaction_Batch WITH (NOLOCK) where nbatchID=" + _BatchID + "";
 //               ODB.Retrive_Query(_sqlQuery, out dtBatch);
 //               if (dtBatch != null && dtBatch.Rows.Count > 0)
 //               {
 //                   _BatchName = Convert.ToString(dtBatch.Rows[0]["sBatchName"]);
 //               }

 //               #endregion 

                
 //               #region "RESEND SENDTOCLAIMMANAGER"

 //               #region Commneted Code "
 //               /*
 //               if (Convert.ToString(oTransaction.Transaction_Status) == TransactionStatus.SendToClaimManager.ToString().Trim())
 //               {

 //                        #region "GENERATE EDI FILE"

 //                       if (_trnsIds.Count > 0)
 //                       { 

 //                           #region "REMOVE SELECTED ID"


 //                       if (oTransaction.TransactionID > 0)
 //                       {
 //                           _trnsIds.Remove(Convert.ToInt64(oTransaction.TransactionID));
 //                           for (int i = 0; i <= oTrnIds.Count - 1; i++)
 //                           {
 //                               if (oTrnIds[i].ID == oTransaction.TransactionID)
 //                               {
 //                                   oTrnIds.RemoveAt(i);
 //                                   break;
 //                               }
 //                           }
 //                       }


 //                       #endregion

 //                           bool _Validateresult = false;
 //                           if (_trnsIds.Count > 0)
 //                           {
 //                                _Validateresult = ValidateEDIData(_trnsIds, true);
 //                           }
 //                           else
 //                           {
 //                               UpdateTransactionStatus(oTransaction.TransactionID, TransactionStatus.Queue);//Single Claim Condition.

 //                               DeleteElectronicClaim(_BatchID);
 //                               deleteEDIFilesFromDirectory(_BatchName);

 //                               //We are deleting this because Zero Count Means Batch will not have any Claims so it shoukd get deleted.
 //                               //If in future we do the logic for Status in EDI generation in resend the this should be reviewed.
 //                               string _strquery1 = String.Empty;
 //                               _strquery1 = "Delete BL_Transaction_Batch_DTL where nBatchID=" + _BatchID + " and nTransactionID=" + oTransaction.TransactionID + " and nClinicID=" + _ClinicID + " ";
 //                               ODB.Connect(false);
 //                               ODB.Execute_Query(_strquery1);

 //                               DataTable dtNewBacth = new DataTable();
 //                               _strquery1 = "Select nBatchID from BL_Transaction_Batch_DTL where nBatchID =" + _BatchID + " and nClinicID=" + _ClinicID + "";
 //                               ODB.Retrive_Query(_strquery1, out dtNewBacth);

                               
 //                               if (dtNewBacth != null && dtNewBacth.Rows.Count == 0)
 //                               {
 //                                   _strquery1 = "Delete BL_Transaction_Batch where nBatchID=" + _BatchID + " and nClinicID=" + _ClinicID + " ";
 //                                   ODB.Execute_Query(_strquery1);
 //                               }

 //                               _result = true;
 //                           }



 //                           if (_trnsIds.Count!=0 && _Validateresult==true)
 //                           {
 //                               UpdateTransactionStatus(oTransaction.TransactionID, TransactionStatus.Queue);

 //                               #region Delete Claim From Batch



 //                               string _strquery = "Delete BL_Transaction_Batch_DTL where nBatchID=" + _BatchID + " and nTransactionID=" + oTransaction.TransactionID + " and nClinicID=" + _ClinicID + " ";
 //                               ODB.Connect(false);
 //                               ODB.Execute_Query(_strquery);

 //                               DataTable dtNewBacth = new DataTable();
 //                               _strquery = "Select nBatchID from BL_Transaction_Batch_DTL where nBatchID =" + _BatchID + " and nClinicID=" + _ClinicID + "";
 //                               ODB.Retrive_Query(_strquery, out dtNewBacth);

 //                               if (dtNewBacth != null && dtNewBacth.Rows.Count == 0)
 //                               {
 //                                   _strquery = "Delete BL_Transaction_Batch where nBatchID=" + _BatchID + " and nClinicID=" + _ClinicID + " ";
 //                                   ODB.Execute_Query(_strquery);
 //                               }

 //                               #endregion


                               
 //                               // FOR SECONADARY EDI GENERATION //

 //                               gloClaimManager ogloEDIGeneration = new gloClaimManager(_databaseconnectionstring, _emrdatabaseconnectionstring);

 //                               ogloEDIGeneration.LoadEDIObject();

 //                               //Check for Responsibility..Primary/Secondary.
 //                               if (oTransaction.ResponsibilityNo==1)
 //                                   _EDIFileName = ogloEDIGeneration.EDI837Generation_New(_trnsIds, _BatchName, false);
 //                               else if (oTransaction.ResponsibilityNo > 1)
 //                                   _EDIFileName = ogloEDIGeneration.EDI837GenerationForSecondary_New(_trnsIds, _BatchName, false);

 //                               if (_EDIFileName.Trim() != "")
 //                               {
 //                                   if (System.IO.File.Exists(_EDIFileName) == true)
 //                                   {
 //                                       UpdateBatchSentCounter(_BatchID, _ClinicID);
 //                                   }

 //                                   #region " Save the file on Server As per name"
 //                                   //20100414 Mahesh Nawal
 //                                   if (getCopyEDIFiles() == 1)
 //                                   {
 //                                       string _ServerPath = GetServerPath();
 //                                       string _BaseFolder = "Claim Management";
 //                                       string _OutInFolder = "OutBox";
 //                                       string _ClaimFolder = "837P Claim submission";
 //                                       string _ElectroniPaperFolder = "Electronic";
 //                                       string _BatchFolderName = _BatchName.Trim();
 //                                       string _claimFolderPath = "";

 //                                       _claimFolderPath = _ServerPath + "\\" + _BaseFolder + "\\" + _OutInFolder + "\\" + _ClaimFolder + "\\" + _ElectroniPaperFolder + "\\" + _BatchFolderName;
 //                                       if (System.IO.Directory.Exists(_claimFolderPath) == false)
 //                                       {
 //                                           System.IO.Directory.CreateDirectory(_claimFolderPath);
 //                                       }
 //                                       string _FileName = "";// oFile.Name;
 //                                       int i = 1;
 //                                       bool _DocNameFound = true;
 //                                       _FileName = _BatchName + "_" + i.ToString() + "." + "txt";
 //                                       while (_DocNameFound == true)
 //                                       {
 //                                           _DocNameFound = System.IO.File.Exists(_claimFolderPath + "\\" + _FileName);
 //                                           if (_DocNameFound == true)
 //                                           {
 //                                               ////Commented For 5040
 //                                               //i++;
 //                                               //_FileName = _BatchName + "_" + i.ToString() + "." + "txt";
 //                                               ////End comment
 //                                               ////20100421
 //                                               try
 //                                               {
 //                                                   System.IO.File.Delete(_claimFolderPath + "\\" + _FileName);
 //                                               }
 //                                               catch { }
 //                                           }
 //                                       }
 //                                       System.IO.File.Copy(_EDIFileName, _claimFolderPath + "\\" + _FileName);
 //                                   }
 //                                   #endregion

 //                                   // SAVE E-CLAIM TO DATABASE //
 //                                   Int64 _ElectronicClaimID = 0;
 //                                   _ElectronicClaimID = SaveElectronicClaim(_BatchID, _BatchName, _EDIFileName, TransactionStatus.SendToClaimManager);
 //                                   if (_ElectronicClaimID > 0)
 //                                   {
 //                                       for (int _counter = 0; _counter < _trnsIds.Count; _counter++)
 //                                       {
 //                                           //UpdateTransactionStatus(Convert.ToInt64(_trnsIds[_counter]), TransactionStatus.SendToClaimManager);
 //                                           SaveElectronicClaimDetail(_ElectronicClaimID, Convert.ToInt64(oTrnIds[_counter].Code), oTrnIds[_counter].Description, Convert.ToInt64(oTrnIds[_counter].ID));
 //                                       }
 //                                   }
 //                               }
 //                               _result = true;
 //                               _EDIFileName = "";
 //                           }
 //                       }
 //                       else
 //                       {
                        
                            
 //                       }
 //                       #endregion
                    
 //               }
 //*/
 //               #endregion

 //               #endregion



 //               #region "RESEND SENDTOCLEARINGHOUSE"

 //               //Dont Delete Batch.
 //               //User may upload manually (Non gatway users).So treat both status as same.
 //               if (oTransaction.Transaction_Status== TransactionStatus.SendToClaimManager || oTransaction.Transaction_Status== TransactionStatus.SendToClearingHouse)
 //               {

 //                       Int64 _tranIdResend = Convert.ToInt64(oTransaction.TransactionID);

 //                       //Get Transaction Details

 //                       Transaction _Transaction = null;
 //                       ogloBilling = new gloBilling(_databaseconnectionstring, "");
 //                       _Transaction = ogloBilling.GetChargesClaimDetails(_tranIdResend, _ClinicID);

 //                       string sMainClaimNo = string.Empty;
 //                       if (_Transaction.SubClaimNo.Contains("-") == false)
 //                       { sMainClaimNo = _Transaction.SubClaimNo; }
 //                       else
 //                       { sMainClaimNo = _Transaction.MainClaimNo; }


 //                       if (_Transaction.SubClaimNo.Trim() != String.Empty)
 //                       { _Transaction.ParentClaimNo = _Transaction.ClaimNo.ToString() + "-" + _Transaction.SubClaimNo; }
 //                       else
 //                       { _Transaction.ParentClaimNo = _Transaction.ClaimNo.ToString(); }



 //                       //New Sub-Claim No
 //                       _Transaction.SubClaimNo = ogloBilling.GetSubClaimNo(_Transaction.TransactionMasterID);
 //                       _Transaction.ParentTransactionID = _Transaction.TransactionID;


 //                       ODB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
 //                       ODB.Connect(false);

 //                       string sql_query = String.Empty;//"Update BL_EOB_NextAction set sNextActionCode='R' , sNextActionDescription='REBILL' where nBillingTransactionID = " + _Transaction.TransactionMasterID + " and nClinicID=" + _ClinicID;
 //                       //ODB.Execute_Query(sql_query);

 //                       if (_IsHold == false)
 //                       {
 //                           sql_query = "update BL_Transaction_Claim_MST WITH (READPAST) set nStatus=" + TransactionStatus.Resent.GetHashCode() + " , nClaimStatus=" + ClaimStatus.Close.GetHashCode() + "  where nTransactionID = " + _Transaction.TransactionID + " and nTransactionMasterID = " + _Transaction.TransactionMasterID + " and nClinicID=" + _ClinicID;
 //                           ODB.Execute_Query(sql_query);
 //                       }
 //                       else 
 //                       {
 //                           sql_query = "";
 //                           sql_query = "update BL_Transaction_Claim_MST WITH (READPAST) set nStatus=" + TransactionStatus.Resent.GetHashCode() + " , nClaimStatus=" + ClaimStatus.Close.GetHashCode() + "  where nTransactionID = " + _Transaction.TransactionID + " and nTransactionMasterID = " + _Transaction.TransactionMasterID + " and nClinicID=" + _ClinicID;
 //                           ODB.Execute_Query(sql_query);
 //                       }

 //                       #region " UnHold Parent Claim if _IsResend is false"

 //                       if (_IsHold == true && _Transaction.Hold != null)
 //                       {
 //                           _Transaction.Hold.HoldModified = true;
 //                           //_Transaction.Hold.HoldModDateTime = DateTime.Now;
 //                           _Transaction.Hold.IsHold = false;
 //                           oClsBL_Hold.HoldUnholdClaim(_Transaction.Hold, _Transaction.TransactionMasterID, _Transaction.TransactionID);
 //                       } 

 //                       #endregion

 //                       _Transaction.TransactionID = 0;

 //                       for (int j = 0; j < _Transaction.Lines.Count; j++)
 //                       {
 //                           _Transaction.Lines[j].ParentTransactionID = _Transaction.ParentTransactionID;
 //                           _Transaction.Lines[j].ParentTransactionDetailID = _Transaction.Lines[j].TransactionDetailID;
 //                           _Transaction.Lines[j].TransactionDetailID = 0;
 //                           _Transaction.Lines[j].LineNotes = null;
 //                       }
 //                       //new transaction send to charges tab(Batch form)
 //                       _Transaction.Transaction_Status = TransactionStatus.Queue;
                        
 //                       //20100430 Done for Hold claim Again holding so New claim should be open.
 //                       _Transaction.ClaimStatus = ClaimStatus.Open;

 //                       #region " Feeding Hold Information for Child Claim if _IsResend is false"

 //                       if (_IsHold == true)
 //                       {
 //                           _Transaction.Hold.HoldModified = true;
 //                           _Transaction.Hold.HoldModDateTime = DateTime.Now;
 //                           _Transaction.Hold.IsHold = true;

 //                       }

 //                       #endregion

 //                       _Transaction.TransactionUserID = this._UserID;
 //                       _Transaction.TransactionUserName = this._UserName;
 //                       ogloBilling.AddTransactionClaim(_Transaction, _ClinicID);

 //                       #region " Update BL_EOB_NextAction with new Tracking ID "

 //                       if (_Transaction.Lines != null && _Transaction.Lines.Count > 0)
 //                       {
 //                           for (int l = 0; l < _Transaction.Lines.Count; l++)
 //                           {
 //                               _sqlQuery = " Update BL_EOB_NextAction WITH (READPAST) set " +
 //                                                                      " sSubClaimNo = '" + _Transaction.SubClaimNo + "'," +
 //                                                                      "  nTrackMstTrnID= " + _Transaction.TransactionID + "," +
 //                                                                      "  nTrackMstTrnDetailID=" + _Transaction.Lines[l].TransactionDetailID +
 //                                                                      " where nBillingTransactionID = " + _Transaction.TransactionMasterID +
 //                                                                      " and nBillingTransactionDetailID = " + _Transaction.Lines[l].TransactionMasterDetailID + "";
 //                               ODB.Execute_Query(_sqlQuery);
 //                           }
 //                       }
 //                       #endregion " Update BL_EOB_NextAction with new Tracking ID "

 //                       //Main Claim
 //                       if (_Transaction.TransactionID > 0)
 //                       {

 //                           if (_Transaction.SubClaimNo.Contains("-") == true)
 //                           {
 //                               sql_query = "update dbo.BL_Transaction_Claim_MST WITH (READPAST) set sMainClaimNo = '" + sMainClaimNo.ToString() + "' where nTransactionID =  " + _Transaction.TransactionID;
 //                           }
 //                           else
 //                           {
 //                               sql_query = "update dbo.BL_Transaction_Claim_MST WITH (READPAST) set sMainClaimNo = '" + _Transaction.SubClaimNo + "' where nTransactionID =  " + _Transaction.TransactionID;
 //                           }

 //                           ODB.Execute_Query(sql_query);


 //                           //gloPM5060 Tracking History
 //                           ogloBilling.SaveTransactionTrackHistory(_Transaction.TransactionMasterID, _Transaction.TransactionID, _UserID, _UserName);

 //                           ////if (_IsResend == false)
 //                           ////{
 //                           ////    sql_query = "update BL_Transaction_Claim_MST set bIs=" + TransactionStatus.Resent.GetHashCode() + " , nClaimStatus=" + ClaimStatus.Close.GetHashCode() + "  where nTransactionID = " + _Transaction.TransactionID + " and nTransactionMasterID = " + _Transaction.TransactionMasterID + " and nClinicID=" + _ClinicID;
 //                           ////    ODB.Execute_Query(sql_query);
 //                           ////}


 //                       }

 //                       _result = true;
 //                       _Transaction.Dispose();
                    
 //               }
 //               #endregion

 //               //Take a look for other Status also.May be we should handle it in Modify Charges.
 //           }
 //           catch (Exception ex)
 //           {
 //               gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
 //           }
 //           finally
 //           {
 //               if (ODB != null) { ODB.Dispose(); ODB = null; }
 //               if (dtBatch != null) { dtBatch.Dispose(); }
 //               if (dtBatchDetail != null) { dtBatchDetail.Dispose(); }
 //               //if (_trnsIds != null) { _trnsIds.Dispose(); }
 //               if (oTrnIds != null) { oTrnIds.Dispose(); }
 //               //if (oTrnID != null) { oTrnID.Dispose(); }
 //               if (ogloBilling != null) { ogloBilling.Dispose();}
 //               if (oClsBL_Hold != null) { oClsBL_Hold.Dispose(); }
 //           }
 //           return _result;
 //       }
        
        public Int32 getCopyEDIFiles()
        {
            gloSettings.GeneralSettings oSetting = new gloSettings.GeneralSettings(_databaseconnectionstring);
            DataTable dtversion = null;
            dtversion = oSetting.GetSetting("COPY_EDI_FILES", 0);
            try
            {
                if (dtversion != null && dtversion.Rows.Count > 0)
                {
                    return Convert.ToInt32(dtversion.Rows[0]["sSettingsValue"]);
                }
                else
                {
                    return 1;
                }
            }
            catch
            {
                return 1;
            }
            finally
            {

                if (dtversion != null)
                {
                    dtversion.Dispose();
                    dtversion = null;
                }
                oSetting.Dispose();
                oSetting = null;
            }
        }

        public string GetServerPath()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            Object retVal = null;
          //  string _serverPath = "";
            string _sqlQuery = "";
            string _isValidPath = "";

            try
            {
                oDB.Connect(false);
                _sqlQuery = "SELECT sSettingsValue FROM Settings WITH (NOLOCK) WHERE UPPER(sSettingsName) = 'SERVERPATH'";
                retVal = oDB.ExecuteScalar_Query(_sqlQuery);
                if (retVal != null && retVal != DBNull.Value)
                {
                    _isValidPath = Convert.ToString(retVal);
                    try
                    {
                        if (System.IO.Directory.Exists(_isValidPath) == false)
                        { _isValidPath = ""; }
                    }
                    catch //(Exception ex)
                    { _isValidPath = ""; }
                }
                else
                { _isValidPath = ""; }
                oDB.Disconnect();
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); }
                if (retVal != null) { retVal = null; }
            }
            return _isValidPath;
        }

        private Int64 SaveElectronicClaim(Int64 nBatchID, string sBatchName, string s837FilePath, TransactionStatus enmStatus)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters;
            gloEDIGeneration oEDI = new gloEDIGeneration("", 0, 0); // PARAMETERS WILL NOT USED IN THIS SECTION // 
            Int64 _Result = 0;

            try
            {
                object oResult;
                oDBParameters = new gloDatabaseLayer.DBParameters();
                oDBParameters.Add("@nID", 0, ParameterDirection.InputOutput, SqlDbType.BigInt);
                oDBParameters.Add("@nBatchID", nBatchID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@sBatchName", sBatchName, ParameterDirection.Input, SqlDbType.VarChar);
                oDBParameters.Add("@i837File", oEDI.ConvertFileToBinary(s837FilePath), ParameterDirection.Input, SqlDbType.Image);
                oDBParameters.Add("@i997File", DBNull.Value, ParameterDirection.Input, SqlDbType.Image);
                oDBParameters.Add("@nStatus", enmStatus.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                oDBParameters.Add("@dtCreatedDate", DateTime.Now, ParameterDirection.Input, SqlDbType.DateTime);
                oDBParameters.Add("@nUserID", 1, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@nClinicID", 1, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@MachineID", oDB.GetPrefixTransactionID(0), ParameterDirection.Input, SqlDbType.BigInt);

                oDB.Connect(false);
                oDB.Execute("BL_INUP_CMSEDI_Claim", oDBParameters, out oResult);
                oDB.Disconnect();

                if (oResult != null && oResult.ToString() != "")
                    _Result = Convert.ToInt64(oResult);

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); oDB = null; }
                if (oEDI != null)
                {
                    oEDI.Dispose();
                    oEDI = null;
                }
            }
            return _Result;
        }

        private Int64 SaveElectronicClaimDetail(Int64 nMasterID, Int64 nClaimNumber, string sSubClaimNumber, Int64 nBillingTransactionID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters;
            gloEDIGeneration oEDI = new gloEDIGeneration("", 0, 0); // PARAMETERS WILL NOT USED IN THIS SECTION // 
            Int64 _Result = 0;

            try
            {
                object oResult;
                oDBParameters = new gloDatabaseLayer.DBParameters();
                oDBParameters.Add("@nID", nMasterID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@nDetailID", 0, ParameterDirection.InputOutput, SqlDbType.BigInt);
                oDBParameters.Add("@nClaimNo", nClaimNumber, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@sSubClaimNo", sSubClaimNumber, ParameterDirection.Input, SqlDbType.VarChar);
                oDBParameters.Add("@nBillingTransactionID", nBillingTransactionID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@nRemitID", 0, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@i276File", DBNull.Value, ParameterDirection.Input, SqlDbType.Image);
                oDBParameters.Add("@s276FileName", DBNull.Value, ParameterDirection.Input, SqlDbType.VarChar);
                oDBParameters.Add("@n277ID", 0, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@n997ID", 0, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@nClinicID", 1, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@MachineID", oDB.GetPrefixTransactionID(0), ParameterDirection.Input, SqlDbType.BigInt);

                oDB.Connect(false);
                oDB.Execute("BL_INUP_CMSEDI_Claim_DTL", oDBParameters, out oResult);
                oDB.Disconnect();

                if (oResult != null && oResult.ToString() != "")
                    _Result = Convert.ToInt64(oResult);

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); oDB = null; }
                if (oEDI != null)
                {
                    oEDI.Dispose();
                    oEDI = null;
                }
            }
            return _Result;
        }

        private DataTable getClaimNo(Int64 nTransactionID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dtClaim = null;
            string _sqlQuery = String.Empty;
            try
            {
                _sqlQuery = "Select nSubClaimno,nClaimNo,nTransactionID from dbo.BL_Transaction_Claim_MST WITH (NOLOCK) where nTransactionID=" + nTransactionID + "";
                oDB.Connect(false);
                oDB.Retrive_Query(_sqlQuery, out dtClaim);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); }
            }
            return dtClaim;
        }

        private bool DeleteElectronicClaim(Int64 nBatchID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            bool _Result = false;
            try
            {
                string _Query = " DELETE FROM BL_CMSEDI_ElectronicClaim_DTL WHERE nID = (SELECT nID FROM BL_CMSEDI_ElectronicClaim WITH (NOLOCK) WHERE nBatchID = " + nBatchID + ");	" +
                    " DELETE FROM BL_CMSEDI_ElectronicClaim WHERE nBatchID = " + nBatchID + "";
                oDB.Connect(false);
                oDB.Execute_Query(_Query);
                oDB.Disconnect();

                _Result = true;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); oDB = null; }
            }
            return _Result;

        }

        private void deleteEDIFilesFromDirectory(string sBatchName)
        {
            try
            {
                if (getCopyEDIFiles() == 1)
                {
                    string _ServerPath = GetServerPath();
                    string _BaseFolder = "Claim Management";
                    string _OutInFolder = "OutBox";
                    string _ClaimFolder = "837P Claim submission";
                    string _ElectroniPaperFolder = "Electronic";
                    string _BatchFolderName = sBatchName;//trvBatch.SelectedNode.Text.Trim();
                    string _claimFolderPath = "";

                    _claimFolderPath = _ServerPath + "\\" + _BaseFolder + "\\" + _OutInFolder + "\\" + _ClaimFolder + "\\" + _ElectroniPaperFolder + "\\" + _BatchFolderName;
                    if (System.IO.Directory.Exists(_claimFolderPath))
                    {
                        System.IO.DirectoryInfo oDirectoryInfo = new System.IO.DirectoryInfo(_claimFolderPath);
                        System.IO.FileInfo[] oFileArray = oDirectoryInfo.GetFiles("*");
                        foreach (System.IO.FileInfo ofile in oFileArray)
                        {
                            ofile.Delete();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public void UpdateTransactionStatus(Int64 _nTransactionID, TransactionStatus _oStatus)
        {
            gloDatabaseLayer.DBLayer ODB = null;
            try
            {
                ODB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                ODB.Connect(false);
                string _strquery = "Update BL_Transaction_Claim_MST WITH (READPAST) set nStatus='" + _oStatus.GetHashCode() + "' where nTransactionID='" + _nTransactionID + "' and nStatus!=" + TransactionStatus.SendToClearingHouse.GetHashCode() + "  and nStatus!=" + TransactionStatus.Resent.GetHashCode() + "";
                ODB.Execute_Query(_strquery);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (ODB != null)
                {
                    ODB.Disconnect();
                    ODB.Dispose();
                }
            }
        }

        private bool ValidateEDIData(ArrayList SelectedTrans, bool IsEDIgeneration)
        {
            DataTable dtClearingHouse = null;
            DataTable dtSubmitter = new DataTable();
          //  DataTable dtReceiver = new DataTable();
          //  DataTable dtBillingProvider = new DataTable();
         //   DataTable dtRenderingProvider = new DataTable();
            DataTable dtFacility = null;
            DataTable dtPatientInsurances = null;
            gloBilling ogloBilling = new gloBilling(_databaseconnectionstring, "");
            string _Message = "";
            Transaction oTransaction = null;
            string strMissingText = "";
            string _MessageHeader = "";
            string _FilePath = gloSettings.FolderSettings.AppTempFolderPath;
            if (oClaimNoArray != null)
            {
                oClaimNoArray.Clear();
            }
            oClaimNoArray = new ArrayList();
            string _messageBoxCaption = "gloPM";

            try
            {
                _MessageHeader += "";

                //Get the Settings for Alpha II
                GetAlphaIISettings();

                if (_AlphaIIValidation == "None")
                {
                    //if (_IsValidateButtonClick == false)
                    //{
                    //    if (_ShowMessageForValidation == true)
                    //    {
                    //        if (MessageBox.Show("You have not selected any validation setting, claims may go with invalid data. Do you want to continue?  ", "", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No)
                    //        {
                    //            return false;
                    //        }
                    //    }
                    //    else
                    //    {
                    //        // return false;
                    //    }
                    //}
                    //else
                    //{
                        if (_ShowMessageForValidation == true)
                        {
                            MessageBox.Show("You have not selected any validation setting, claims may go with invalid data.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    //}
                }
                else if (_AlphaIIValidation == "Alpha2")
                {
                    if (!ValidateConnectionString())
                    {
                        MessageBox.Show("Connection for Alpha II cannot be establish, please do the setting from gloPM Admin.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return false;
                    }
                }
                //Get Clearing House Information in Datatable
                //dtClearingHouse = new DataTable();
                dtClearingHouse = ogloBilling.GetClearingHouseSettings();
                if (dtClearingHouse == null || dtClearingHouse.Rows.Count < 1)
                {
                    MessageBox.Show("Clearing House information is not present.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
                if (SelectedTrans != null)
                {
                    if (SelectedTrans.Count > 0)
                    {
                        for (int i = 0; i < SelectedTrans.Count; i++)
                        {
                  //          oTransaction = new Transaction();
                           // TransactionLine oTransLine = null;
                            //MaheshB Not Used//oTransaction = ogloBilling.GetTransactionClaimDetails(Convert.ToInt64(SelectedTrans[i]), _ClinicID);
                            //PramodN Method
                            oTransaction = ogloBilling.GetChargesClaimDetails(Convert.ToInt64(SelectedTrans[i]), _ClinicID);
                            if (oTransaction != null)
                            {
                                if (oTransaction.Lines.Count > 0)
                                {
                                    //Get Submitter Information in Datatable
                    //                dtSubmitter = new DataTable();
                                    if (dtSubmitter != null)
                                    {
                                        dtSubmitter.Dispose();
                                        dtSubmitter = null;
                                    }
                                    dtSubmitter = ogloBilling.GetSubmitterInfo(Convert.ToInt64(_ClinicID), oTransaction.ProviderID);
                                    if (dtSubmitter == null || dtSubmitter.Rows.Count < 1)
                                    {
                                        MessageBox.Show("Submitter/Provider information is not present.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        return false;
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Transaction Lines are not there in selected transaction(s).  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return false;
                                }
                                oTransaction.Dispose();
                                oTransaction = null;
                            }
                        }
                    }
                }
                if (dtSubmitter == null || dtSubmitter.Rows.Count < 1)
                {
                    MessageBox.Show("Submitter/Provider information is not present.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
                if (dtClearingHouse != null)
                {
                    dtClearingHouse.Dispose();
                    dtClearingHouse = null;
                }
                #region " Clearing House "
                //ISA and GS Settings
                if (Convert.ToString(dtClearingHouse.Rows[0]["sSubmitterID"]).Trim() == "")
                {
                    if (GetValidationFieldsSettings("Sender ID"))
                        strMissingText += "Sender ID" + Environment.NewLine + "" + Environment.NewLine + "";
                }
                if (Convert.ToString(dtClearingHouse.Rows[0]["sReceiverID"]).Trim() == "")
                {
                    if (GetValidationFieldsSettings("Receiver ID"))
                        strMissingText += "Receiver ID" + Environment.NewLine + "";
                }
                if (Convert.ToString(dtClearingHouse.Rows[0]["sSenderCode"]).Trim() == "")
                {
                    if (GetValidationFieldsSettings("Sender Code"))
                        strMissingText += "Sender Code" + Environment.NewLine + "";
                }
                if (Convert.ToString(dtClearingHouse.Rows[0]["sVenderIDCode"]).Trim() == "")
                {
                    if (GetValidationFieldsSettings("Receiver Code"))
                        strMissingText += "Receiver Code" + Environment.NewLine + "";
                }
                #endregion " Clearing House "

                #region " Submitter "
                //Submitter
                if (Convert.ToString(dtSubmitter.Rows[0]["SubmitterName"]).Trim() == "")
                {
                    if (GetValidationFieldsSettings("Submitter Name"))
                        strMissingText += "Submitter Name" + Environment.NewLine + "";
                }
                if (Convert.ToString(dtSubmitter.Rows[0]["SubmitterContactName"]).Trim() == "")
                {
                    if (GetValidationFieldsSettings("Submitter Contact Person Name"))
                        strMissingText += "Submitter Contact Person Name" + Environment.NewLine + "";
                }
                if (Convert.ToString(dtSubmitter.Rows[0]["SubmitterPhone"]).Trim() == "")
                {
                    if (GetValidationFieldsSettings("Submitter Contact Person Number"))
                        strMissingText += "Submitter Contact Person Number" + Environment.NewLine + "";
                }
                if (Convert.ToString(dtSubmitter.Rows[0]["SubmitterCity"]).Trim() == "")
                {
                    if (GetValidationFieldsSettings("Submitter City"))
                        strMissingText += "Submitter City" + Environment.NewLine + "";
                }
                if (Convert.ToString(dtSubmitter.Rows[0]["SubmitterState"]).Trim() == "")
                {
                    if (GetValidationFieldsSettings("Submitter State"))
                        strMissingText += "Submitter State" + Environment.NewLine + "";
                }
                if (Convert.ToString(dtSubmitter.Rows[0]["SubmitterZIP"]).Trim() == "")
                {
                    if (GetValidationFieldsSettings("Submitter Zip"))
                        strMissingText += "Submitter Zip" + Environment.NewLine + "";
                }
                //if (_SubmitterETIN == "")
                //{
                //    if (GetValidationFieldsSettings("Submitter ETIN"))
                //    strMissingText += "Submitter ETIN" + Environment.NewLine + "";
                //}
                if (Convert.ToString(dtSubmitter.Rows[0]["SubmitterAddress1"]).Trim() + " " + Convert.ToString(dtSubmitter.Rows[0]["SubmitterAddress2"]).Trim() == "")
                {
                    if (GetValidationFieldsSettings("Submitter Address"))
                        strMissingText += "Submitter Address" + Environment.NewLine + "";
                }
                #endregion " Submitter "

                if (strMissingText.Trim() != "")
                {
                    _MessageHeader = _MessageHeader + strMissingText;
                }
                else
                {
                    _MessageHeader = "";
                }


                if (SelectedTrans != null)
                {
                    if (SelectedTrans.Count > 0)
                    {
                        gloAppointmentBook.Books.Resource oResource = new gloAppointmentBook.Books.Resource(_databaseconnectionstring);
                        gloPatient.gloPatient ogloPatient = new gloPatient.gloPatient(_databaseconnectionstring);
                        gloSettings.GeneralSettings oSettings = new gloSettings.GeneralSettings(_databaseconnectionstring);
 
                        for (int i = 0; i < SelectedTrans.Count; i++)
                        {
                            string strMessage = "";
                          //  oTransaction = new Transaction();
                           // TransactionLine oTransLine = null;
                            oTransaction = ogloBilling.GetChargesClaimDetails(Convert.ToInt64(SelectedTrans[i]), _ClinicID);
                            string _ClaimMessageHeader = "";
                            gloAppointmentBook.Books.Provider _Provider = null;
                            gloPatient.Patient oPatient = null;
                         //   gloPatient.Referrals oReferral = new gloPatient.Referrals();
                           
                            Object _objResult = null;
                            bool _IsClaimNumberAdded = false;
                            string strBillingSetting = "";

                            if (oTransaction != null)
                            {
                                if (oTransaction.Lines.Count > 0)
                                {
                                    if (Convert.ToInt64(oTransaction.ProviderID) != 0 && oTransaction.ProviderID.ToString() != "")
                                    {
                                        _Provider = oResource.GetProviderDetail(Convert.ToInt64(oTransaction.ProviderID));
                                        if (_Provider == null)
                                        {
                                            MessageBox.Show("Provider information is not present.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            return false;
                                        }
                                        
                                        //dtPatientInsurances = ogloPatient.getPatientInsurances(oTransaction.PatientID);
                                        dtPatientInsurances = ogloPatient.getTransactionInsurances(oTransaction.TransactionMasterID, oTransaction.ResponsibilityNo);
                                        oPatient = ogloPatient.GetPatient(oTransaction.PatientID);
                                        if (oPatient == null)
                                        {
                                            //MessageBox.Show("Patient demographic information is not present for claim number " + FormattedClaimNumberGeneration(Convert.ToString(oTransaction.ClaimNo)) + ".  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            MessageBox.Show("Patient demographic information is not present for claim number " + oTransaction.ClaimNumber + ".  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            return false;
                                        }
                                        if (dtPatientInsurances == null || dtPatientInsurances.Rows.Count < 1)
                                        {
                                            //MessageBox.Show("Patient " + oPatient.DemographicsDetail.PatientFirstName + " " + oPatient.DemographicsDetail.PatientLastName + " Insurance details are missing for claim number " + FormattedClaimNumberGeneration(Convert.ToString(oTransaction.ClaimNo)) + ".  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            if (_IsClaimNumberAdded == false)
                                            {
                                                // oClaimNoArray.Add(oTransaction.ClaimNo);

                                                if (GetValidationFieldsSettings("Subscriber Last Name"))
                                                    strMessage += "Subscriber Last Name" + Environment.NewLine + "";

                                                // if (GetValidationFieldsSettings("Subscriber Relationship"))
                                                strMessage += "Subscriber Relationship" + Environment.NewLine + "";


                                                if (GetValidationFieldsSettings("Insurance Plan Type"))
                                                    strMessage += "Plan Type" + Environment.NewLine + "";


                                                if (GetValidationFieldsSettings("Subscriber First Name"))
                                                    strMessage += "Subscriber First Name" + Environment.NewLine + "";


                                                //     if (GetValidationFieldsSettings("Subscriber Insurance ID"))
                                                strMessage += "Insurance ID" + Environment.NewLine + "";


                                                if (GetValidationFieldsSettings("Subscriber Address"))
                                                    strMessage += "Subscriber Address" + Environment.NewLine + "";

                                                if (GetValidationFieldsSettings("Subscriber Group ID"))
                                                    strMessage += "Subscriber Group ID" + Environment.NewLine + "";

                                                if (GetValidationFieldsSettings("Subscriber City"))
                                                    strMessage += "Subscriber City" + Environment.NewLine + "";

                                                if (GetValidationFieldsSettings("Subscriber State"))
                                                    strMessage += "Subscriber State" + Environment.NewLine + "";

                                                if (GetValidationFieldsSettings("Subscriber Zip"))
                                                    strMessage += "Subscriber Zip" + Environment.NewLine + "";

                                                //    if (GetValidationFieldsSettings("Subscriber Date of Birth"))
                                                strMessage += "Subscriber Date of Birth" + Environment.NewLine + "";

                                                //   if (GetValidationFieldsSettings("Subscriber Gender"))
                                                strMessage += "Subscriber Gender" + Environment.NewLine + "";

                                                if (GetValidationFieldsSettings("Payer Name"))
                                                    strMessage += "Payer/Insurance Name" + Environment.NewLine + "";

                                                //20100416 Urgent Outage of 5030.
                                                if (GetValidationFieldsSettings("Payer ID"))
                                                    strMessage += "Payer ID" + Environment.NewLine + "";

                                                if (GetValidationFieldsSettings("Payer Address"))
                                                    strMessage += "Payer Address" + Environment.NewLine + "";

                                                if (GetValidationFieldsSettings("Payer City"))
                                                    strMessage += "Payer City" + Environment.NewLine + "";

                                                if (GetValidationFieldsSettings("Payer State"))
                                                    strMessage += "Payer State" + Environment.NewLine + "";

                                                if (GetValidationFieldsSettings("Payer Zip"))
                                                    strMessage += "Payer Zip" + Environment.NewLine + "";

                                                _IsClaimNumberAdded = true;
                                            }
                                            if (IsEDIgeneration)
                                            {
                                                //return false;
                                            }
                                        }
                                        dtFacility = ogloBilling.GetFacilityInfo(oTransaction.FacilityCode, oTransaction.ProviderID);
                                        //if (dtFacility == null && dtFacility.Rows.Count < 1)
                                        //{
                                        //    MessageBox.Show("Facility information is not present.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        //    return false;
                                        //}


                                    }

                                    _ClaimMessageHeader = " " + Environment.NewLine + "For Patient: " + oPatient.DemographicsDetail.PatientFirstName.Trim() + " " + oPatient.DemographicsDetail.PatientLastName.Trim() + "  and Claim Number: " + oTransaction.ClaimNumber + " " + Environment.NewLine + "" + Environment.NewLine + "";


                                    gloEDIGeneration ogloEDIGeneration = null;
                                    //if (trvBatch.SelectedNode != null)
                                    //{
                                    //    if (Convert.ToString(_) != "")
                                    //    {
                                    //        ogloEDIGeneration = new gloEDIGeneration(_databaseconnectionstring, this.UserID, Convert.ToInt64(trvBatch.SelectedNode.Tag));
                                    //    }
                                    //    else
                                    //    {
                                    //        ogloEDIGeneration = new gloEDIGeneration(_databaseconnectionstring, UserID, 0);
                                    //    }
                                    //}
                                    //else
                                    //{
                                        ogloEDIGeneration = new gloEDIGeneration(_databaseconnectionstring, _UserID, 0);
                                    //}
                                    string _strMessage1 = "";
                                    for (int j = 0; j < oTransaction.Lines.Count; j++)
                                    {
                                        if (_IsCheckInvalidICD9 == true)
                                        {

                                            #region " ICD9 Validation "
                                            if (Convert.ToString(oTransaction.Lines[j].Dx1Code).Trim() != "")
                                            {
                                                if (ogloEDIGeneration.IsValidICD9Code(Convert.ToString(oTransaction.Lines[j].Dx1Code.Trim())) == false)
                                                {
                                                    if (IsDiagnosisExist(_strMessage1, oTransaction.Lines[j].Dx1Code.Trim()) == false)
                                                    {
                                                        _strMessage1 += " " + Convert.ToString(oTransaction.Lines[j].Dx1Code.Trim()) + ", ";
                                                    }
                                                }
                                            }
                                            if (Convert.ToString(oTransaction.Lines[j].Dx2Code).Trim() != "")
                                            {
                                                if (ogloEDIGeneration.IsValidICD9Code(Convert.ToString(oTransaction.Lines[j].Dx2Code.Trim())) == false)
                                                {
                                                    if (IsDiagnosisExist(_strMessage1, oTransaction.Lines[j].Dx2Code.Trim()) == false)
                                                    {
                                                        _strMessage1 += " " + Convert.ToString(oTransaction.Lines[j].Dx2Code.Trim()) + ", ";
                                                    }
                                                }
                                            }
                                            if (Convert.ToString(oTransaction.Lines[j].Dx3Code).Trim() != "")
                                            {
                                                if (ogloEDIGeneration.IsValidICD9Code(Convert.ToString(oTransaction.Lines[j].Dx3Code.Trim())) == false)
                                                {
                                                    if (IsDiagnosisExist(_strMessage1, oTransaction.Lines[j].Dx3Code.Trim()) == false)
                                                    {
                                                        _strMessage1 += " " + Convert.ToString(oTransaction.Lines[j].Dx3Code.Trim()) + ", ";
                                                    }
                                                }
                                            }
                                            if (Convert.ToString(oTransaction.Lines[j].Dx4Code).Trim() != "")
                                            {
                                                if (ogloEDIGeneration.IsValidICD9Code(Convert.ToString(oTransaction.Lines[j].Dx4Code.Trim())) == false)
                                                {
                                                    if (IsDiagnosisExist(_strMessage1, oTransaction.Lines[j].Dx4Code.Trim()) == false)
                                                    {
                                                        _strMessage1 += " " + Convert.ToString(oTransaction.Lines[j].Dx4Code.Trim()) + ", ";
                                                    }
                                                }
                                            }

                                            #endregion " ICD9 Validation "
                                        }
                                    }
                                    ogloEDIGeneration.Dispose();
                                    ogloEDIGeneration = null;
                                    if (_strMessage1.Trim() != "")
                                    {
                                        _strMessage1 = _strMessage1.Substring(0, _strMessage1.Length - 1);
                                        strMessage += "Invalid ICD9's " + _strMessage1 + Environment.NewLine + "";
                                        if (IsEDIgeneration)
                                        {
                                            //return false;
                                        }
                                    }

                                }
                            }

                            #region " Billing Provider "
                            //Billing Provider
                            if (_Provider != null)
                            {
                                oSettings.GetSetting("BillingSetting", oTransaction.ProviderID, _ClinicID, out _objResult);
                                if (_objResult != null)
                                {
                                    // |Company|Practice|Business"
                                    strBillingSetting = Convert.ToString(_objResult);
                                }


                                if (strBillingSetting == "Clinic")
                                {

                                    #region " Get Clinic Information "

                                    string _ClinicName = String.Empty;
                                    string _ClinicAddress = String.Empty;
                                    string _ClinicCity = String.Empty;
                                    string _ClinicState = String.Empty;
                                    string _ClinicZip = String.Empty;
                                    string _ClinicNPI = String.Empty;
                                    string _ClinicTaxID = String.Empty;

                                    gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                                    oDB.Connect(false);
                                    DataTable dtClinic = null;
                                    string _sqlQuery1 = "Select ISNULL(sClinicName,'') as sClinicName,ISNULL(sAddress1,'') as sAddress1,ISNULL(sAddress2,'') as sAddress2,ISNULL(sStreet,'') as sStreet,ISNULL(sCity,'') as sCity,ISNULL(sState,'') as sState,ISNULL(sZIP,'') as sZIP,ISNULL(sPhoneNo,'') as sPhoneNo,ISNULL(sClinicNPI,'') as sClinicNPI, " +
                                                "sTAXID as nClinicTaxId from Clinic_MST WITH (NOLOCK) where nClinicID=" + _ClinicID;
                                    oDB.Retrive_Query(_sqlQuery1, out dtClinic);
                                    if (dtClinic != null && dtClinic.Rows.Count > 0)
                                    {
                                        _ClinicName = Convert.ToString(dtClinic.Rows[0]["sClinicName"]);
                                        _ClinicAddress = Convert.ToString(dtClinic.Rows[0]["sAddress1"]) + " " + Convert.ToString(dtClinic.Rows[0]["sAddress2"]);
                                        _ClinicCity = Convert.ToString(dtClinic.Rows[0]["sCity"]);
                                        _ClinicState = Convert.ToString(dtClinic.Rows[0]["sState"]);
                                        _ClinicZip = Convert.ToString(dtClinic.Rows[0]["sZIP"]);
                                        _ClinicTaxID = Convert.ToString(dtClinic.Rows[0]["nClinicTaxId"]);
                                        _ClinicNPI = Convert.ToString(dtClinic.Rows[0]["sClinicNPI"]);
                                    }
                                    dtClinic.Dispose();
                                    dtClinic = null;
                                    oDB.Dispose();
                                    oDB = null;

                                    if (_ClinicName.Trim() == "")
                                    {
                                        strMessage += "Clinic Name " + Environment.NewLine + "";
                                    }
                                    if (_ClinicCity.Trim() == "")
                                    {
                                        strMessage += "Clinic City" + Environment.NewLine + "";
                                    }
                                    if (_ClinicState.Trim() == "")
                                    {
                                        strMessage += "Clinic State" + Environment.NewLine + "";
                                    }
                                    if (_ClinicAddress.Trim() == "")
                                    {
                                        strMessage += "Clinic Address" + Environment.NewLine + "";
                                    }
                                    if (_ClinicZip.Trim() == "")
                                    {
                                        strMessage += "Clinic Zip" + Environment.NewLine + "";
                                    }
                                    if (_ClinicNPI.Trim() == "")
                                    {
                                        strMessage += "Clinic NPI" + Environment.NewLine + "";
                                    }
                                    if (_ClinicTaxID.Trim() == "")
                                    {
                                        strMessage += "Clinic Tax ID" + Environment.NewLine + "";
                                    }
                                    #endregion



                                }
                                else
                                {
                                    string _BillingAddress = "";
                                    string _BillingCity = "";
                                    string _BillingState = "";
                                    string _BillingZIP = "";
                                    string _BillingNPI = "";
                                    //MaheshB 20100302
                                    string _BillingSecIdentification = "";

                                    switch (strBillingSetting)
                                    {
                                        case "Business":
                                            {
                                                _BillingAddress = _Provider.BMAddress1;
                                                _BillingCity = _Provider.BMCity;
                                                _BillingState = _Provider.BMState;
                                                _BillingZIP = _Provider.BMZIP;
                                                _BillingNPI = _Provider.NPI;
                                                if (_Provider.EmployerID.Trim().Replace("*", "") != "")
                                                {
                                                    _BillingSecIdentification = _Provider.EmployerID.Trim().Replace("*", "");
                                                }
                                                else if (_Provider.SSN.Trim().Replace("*", "") != "")
                                                {
                                                    _BillingSecIdentification = _Provider.SSN.Trim().Replace("*", "");
                                                }
                                            } break;
                                        case "Practice":
                                            {
                                                _BillingAddress = _Provider.BPracAddress1;
                                                _BillingCity = _Provider.BPracCity;
                                                _BillingState = _Provider.BPracState;
                                                _BillingZIP = _Provider.BPracZIP;
                                                _BillingNPI = _Provider.NPI;
                                                if (_Provider.EmployerID.Trim().Replace("*", "") != "")
                                                {
                                                    _BillingSecIdentification = _Provider.EmployerID.Trim().Replace("*", "");
                                                }
                                                else if (_Provider.SSN.Trim().Replace("*", "") != "")
                                                {
                                                    _BillingSecIdentification = _Provider.SSN.Trim().Replace("*", "");
                                                }
                                            } break;
                                        case "Company":
                                            {
                                                _BillingAddress = _Provider.CompanyAddress1;
                                                _BillingCity = _Provider.CompanyCity;
                                                _BillingState = _Provider.CompanyState;
                                                _BillingZIP = _Provider.CompanyZip;
                                                _BillingNPI = _Provider.CompanyNPI;
                                                _BillingSecIdentification = _Provider.CompanyTaxID.Trim().Replace("*", "");
                                            } break;



                                        default:
                                            _BillingAddress = _Provider.BMAddress1;
                                            _BillingCity = _Provider.BMCity;
                                            _BillingState = _Provider.BMState;
                                            _BillingZIP = _Provider.BMZIP;
                                            _BillingNPI = _Provider.NPI;
                                            if (_Provider.EmployerID.Trim().Replace("*", "") != "")
                                            {
                                                _BillingSecIdentification = _Provider.EmployerID.Trim().Replace("*", "");
                                            }
                                            else if (_Provider.SSN.Trim().Replace("*", "") != "")
                                            {
                                                _BillingSecIdentification = _Provider.SSN.Trim().Replace("*", "");
                                            }
                                            break;
                                    }

                                    if (_Provider.FirstName.Trim() == "")
                                    {
                                        if (GetValidationFieldsSettings("Billing Provider First Name"))
                                            strMessage += "Billing Provider First Name" + Environment.NewLine + "";
                                    }
                                    if (_Provider.LastName.Trim() == "")
                                    {
                                        if (GetValidationFieldsSettings("Billing Provider Last Name"))
                                            strMessage += "Billing Provider Last Name" + Environment.NewLine + "";
                                    }
                                    if (_Provider.MiddleName.Trim() == "")
                                    {
                                        if (GetValidationFieldsSettings("Billing Provider Middle Name"))
                                            strMessage += "Billing Provider Middle Name" + Environment.NewLine + "";
                                    }
                                    if (_BillingCity.Trim() == "")
                                    {
                                        if (GetValidationFieldsSettings("Billing Provider City"))
                                            strMessage += "Billing Provider City" + Environment.NewLine + "";
                                    }
                                    if (_BillingState.Trim() == "")
                                    {
                                        if (GetValidationFieldsSettings("Billing Provider State"))
                                            strMessage += "Billing Provider State" + Environment.NewLine + "";
                                    }
                                    if (_BillingAddress.Trim() == "")
                                    {
                                        if (GetValidationFieldsSettings("Billing Provider Address"))
                                            strMessage += "Billing Provider Address" + Environment.NewLine + "";
                                    }
                                    if (_BillingZIP.Trim() == "")
                                    {
                                        if (GetValidationFieldsSettings("Billing Provider Zip"))
                                            strMessage += "Billing Provider Zip" + Environment.NewLine + "";
                                    }
                                    if (_BillingNPI.Trim() == "")
                                    {
                                        //if (GetValidationFieldsSettings("Billing Provider NPI"))
                                        if (strBillingSetting.Trim() == "Company")
                                        {
                                            strMessage += strBillingSetting + "Company NPI" + Environment.NewLine + "";
                                        }
                                        else
                                        {
                                            strMessage += strBillingSetting + "Billing Provider NPI" + Environment.NewLine + "";
                                        }
                                    }
                                    if (_Provider.SSN.Trim() == "")
                                    {
                                        //strMessage += "Billing Provider SSN" + Environment.NewLine + "";
                                    }
                                    if (_Provider.EmployerID.Trim() == "")
                                    {
                                        if (GetValidationFieldsSettings("Billing Provider Employer ID"))
                                            strMessage += "Billing Provider Employer ID" + Environment.NewLine + "";
                                    }
                                    if (_Provider.StateMedicalNo.Trim() == "")
                                    {
                                        //strMessage += "Billing Provider State Medical No" + Environment.NewLine + "";
                                    }
                                    if (_Provider.Taxonomy.Trim() == "")
                                    {
                                        if (GetValidationFieldsSettings("Billing Provider Taxonomy"))
                                            strMessage += "Billing Provider Taxonomy" + Environment.NewLine + "";
                                    }
                                    //MaheshB 20100302
                                    if (strBillingSetting.Trim() == "Company")
                                    {
                                        if (_BillingSecIdentification.Trim() == "")
                                        {
                                            //20100416 Urgent Outage 5031
                                            if (GetValidationFieldsSettings("Billing Provider Company Tax ID"))
                                                strMessage += "Company Tax ID " + Environment.NewLine + "";
                                        }

                                    }
                                    else
                                    {
                                        if (_BillingSecIdentification.Trim() == "")
                                        {

                                            strMessage += "Billing Provider EmployerID and Billing Provider SSN " + Environment.NewLine + "";
                                        }

                                    }
                                }
                            }

                            #endregion " Billing Provider "

                            #region " Facility "
                            //Facility Information
                            if (oTransaction.FacilityCode.Trim() != "")
                            {
                                if (dtFacility != null && dtFacility.Rows.Count > 0)
                                {
                                    if (Convert.ToString(dtFacility.Rows[0]["FacilityName"]).Trim() == "")
                                    {
                                        if (GetValidationFieldsSettings("Facility Name"))
                                            strMessage += "Facility Name" + Environment.NewLine + "";
                                    }
                                    if (Convert.ToString(dtFacility.Rows[0]["FacilityAddress1"]).Trim() == "")
                                    {
                                        if (GetValidationFieldsSettings("Facility Address"))
                                            strMessage += "Facility Address" + Environment.NewLine + "";
                                    }
                                    if (Convert.ToString(dtFacility.Rows[0]["FacilityCity"]).Trim() == "")
                                    {
                                        //   if (GetValidationFieldsSettings("Facility City"))
                                        strMessage += "Facility City" + Environment.NewLine + "";
                                    }
                                    if (Convert.ToString(dtFacility.Rows[0]["FacilityState"]).Trim() == "")
                                    {
                                        // if (GetValidationFieldsSettings("Facility State"))
                                        strMessage += "Facility State" + Environment.NewLine + "";
                                    }
                                    if (Convert.ToString(dtFacility.Rows[0]["FacilityZip"]).Trim() == "")
                                    {
                                        //  if (GetValidationFieldsSettings("Facility Zip"))
                                        strMessage += "Facility Zip" + Environment.NewLine + "";
                                    }
                                    if (Convert.ToString(dtFacility.Rows[0]["FacilityNPI"]).Trim() == "")
                                    {
                                        if (GetValidationFieldsSettings("Facility NPI"))
                                            strMessage += "Facility NPI" + Environment.NewLine + "";
                                    }
                                }
                            }

                            //Receiver
                            //if (_ReceiverName == "")
                            //{
                            //    if (GetValidationFieldsSettings("Receiver Name"))
                            //    strMessage += "Receiver Name" + Environment.NewLine + "";
                            //}
                            //if (_ReceiverETIN == "")
                            //{
                            //     if (GetValidationFieldsSettings("Receiver ETIN"))
                            //    strMessage += "Receiver ETIN" + Environment.NewLine + "";
                            //}
                            #endregion " Facility "

                            #region " Subscriber "
                            //Subscriber
                            if (dtPatientInsurances != null && dtPatientInsurances.Rows.Count > 0)
                            {
                                for (int _InsRow = 0; _InsRow < dtPatientInsurances.Rows.Count; _InsRow++)
                                {
                                    #region " Primary Insurance "
                                    if (_InsRow == 0)
                                    {
                                        if (Convert.ToString(dtPatientInsurances.Rows[_InsRow]["SubLName"]).Trim() == "")
                                        {
                                            if (GetValidationFieldsSettings("Subscriber Last Name"))
                                                strMessage += "Subscriber Last Name" + Environment.NewLine + "";
                                        }
                                        //if (_SubscriberInsurancePST == "")
                                        //{
                                        //    // if (GetValidationFieldsSettings("Subscriber Insurance Type"))
                                        //    //strMessage += "Subscriber Insurance Type(P/S/T)" + Environment.NewLine + "";
                                        //}
                                        if (Convert.ToString(dtPatientInsurances.Rows[_InsRow]["RelationshipCode"]).Trim() == "")
                                        {
                                            //if (GetValidationFieldsSettings("Subscriber Relationship"))
                                            strMessage += "Subscriber Relationship" + Environment.NewLine + "";
                                        }
                                        if (Convert.ToString(dtPatientInsurances.Rows[_InsRow]["InsuranceTypeCode"]).Trim() == "")
                                        {
                                            //20100416 Urgent Outage 5031
                                            if (GetValidationFieldsSettings("Plan Type"))
                                                strMessage += "Plan Type" + Environment.NewLine + "";
                                        }
                                        if (Convert.ToString(dtPatientInsurances.Rows[_InsRow]["SubFName"]).Trim() == "")
                                        {
                                            if (GetValidationFieldsSettings("Subscriber First Name"))
                                                strMessage += "Subscriber First Name" + Environment.NewLine + "";
                                        }
                                        //if (_SubscriberMName == "")
                                        //{
                                        //    if (GetValidationFieldsSettings("Subscriber Middle Name"))
                                        //    // strMessage += "Subscriber Middle Name"+Environment.NewLine+"";
                                        //}
                                        if (Convert.ToString(dtPatientInsurances.Rows[_InsRow]["sSubscriberID"]).Trim() == "")
                                        {
                                            // if (GetValidationFieldsSettings("Subscriber Insurance ID"))
                                            strMessage += "Insurance ID" + Environment.NewLine + "";
                                        }
                                        if (Convert.ToString(dtPatientInsurances.Rows[_InsRow]["SubscriberAddr1"]).Trim() == "")
                                        {
                                            if (GetValidationFieldsSettings("Subscriber Address"))
                                                strMessage += "Subscriber Address" + Environment.NewLine + "";
                                        }
                                        if (Convert.ToString(dtPatientInsurances.Rows[_InsRow]["sGroup"]).Trim() == "")
                                        {
                                            if (GetValidationFieldsSettings("Subscriber Group ID"))
                                                strMessage += "Subscriber Group ID" + Environment.NewLine + "";
                                        }
                                        if (Convert.ToString(dtPatientInsurances.Rows[_InsRow]["SubscriberCity"]).Trim() == "")
                                        {
                                            if (GetValidationFieldsSettings("Subscriber City"))
                                                strMessage += "Subscriber City" + Environment.NewLine + "";
                                        }
                                        if (Convert.ToString(dtPatientInsurances.Rows[_InsRow]["SubscriberState"]).Trim() == "")
                                        {
                                            if (GetValidationFieldsSettings("Subscriber State"))
                                                strMessage += "Subscriber State" + Environment.NewLine + "";
                                        }
                                        if (Convert.ToString(dtPatientInsurances.Rows[_InsRow]["SubscriberZip"]).Trim() == "")
                                        {
                                            if (GetValidationFieldsSettings("Subscriber Zip"))
                                                strMessage += "Subscriber Zip" + Environment.NewLine + "";
                                        }
                                        if (Convert.ToString(dtPatientInsurances.Rows[_InsRow]["dtDOB"]).Trim() == "")
                                        {
                                            //if (GetValidationFieldsSettings("Subscriber Date of Birth"))
                                            strMessage += "Subscriber Date of Birth" + Environment.NewLine + "";
                                        }
                                        if (Convert.ToString(dtPatientInsurances.Rows[_InsRow]["sSubscriberGender"]).Trim() == "")
                                        {
                                            //if (GetValidationFieldsSettings("Subscriber Gender"))
                                            strMessage += "Subscriber Gender" + Environment.NewLine + "";
                                        }
                                        //if (dtPatientInsurances.Rows[_InsRow]["InsuranceName"].ToString().ToUpper().Contains("MEDICARE"))

                                        //20100416 Urgent Outage of 5031.
                                        //if (dtPatientInsurances.Rows[_InsRow]["InsuranceTypeCode"].ToString().ToUpper() == "MA" || dtPatientInsurances.Rows[_InsRow]["InsuranceTypeCode"].ToString().ToUpper() == "MB")
                                        //{
                                        //    if (dtPatientInsurances.Rows[_InsRow]["sInsuranceFlag"].ToString().Trim() != "Primary")
                                        //    {
                                        //        if (dtPatientInsurances.Rows[_InsRow]["InsTypeCodeMedicare"].ToString().Trim() == "0" || dtPatientInsurances.Rows[_InsRow]["InsTypeCodeMedicare"].ToString().Trim() == "" || dtPatientInsurances.Rows[_InsRow]["InsTypeCodeMedicare"].ToString().Trim() == string.Empty )
                                        //        {
                                        //              strMessage += "Medicare Secondary Type" + Environment.NewLine + "";
                                        //        }
                                        //    }
                                        //    else
                                        //    {
                                        //        //make it sure...
                                        //        //if (dtPatientInsurances.Rows[_InsRow]["InsTypeCodeDefault"].ToString().Trim() == "0" || dtPatientInsurances.Rows[_InsRow]["InsTypeCodeDefault"].ToString().Trim() == "")
                                        //        //{
                                        //        //    strMessage += "Default Insurance Type Code" + Environment.NewLine + "";
                                        //        //}
                                        //    }
                                        //}
                                        //else
                                        //{
                                        //    //if (dtPatientInsurances.Rows[_InsRow]["InsTypeCodeDefault"].ToString() == "0" || dtPatientInsurances.Rows[_InsRow]["InsTypeCodeDefault"].ToString().Trim() =="")
                                        //    //{
                                        //    //    strMessage += "Default Insurance Type Code" + Environment.NewLine + "";
                                        //    //}
                                        //}
                                        if (dtPatientInsurances.Rows[_InsRow]["InsTypeCodeDefault"].ToString() == "0" || dtPatientInsurances.Rows[_InsRow]["InsTypeCodeDefault"].ToString().Trim() == "")
                                        {
                                            //20100416 Urgent Outage 5031
                                            if (GetValidationFieldsSettings("Insurance Type"))
                                                strMessage += "Insurance Type Code" + Environment.NewLine + "";
                                        }

                                        //Payer
                                        if (Convert.ToString(dtPatientInsurances.Rows[_InsRow]["InsuranceName"]).Trim() == "")
                                        {
                                            if (GetValidationFieldsSettings("Payer Name"))
                                                strMessage += "Payer/Insurance Name" + Environment.NewLine + "";
                                        }
                                        if (Convert.ToString(dtPatientInsurances.Rows[_InsRow]["PayerID"]).Trim() == "")
                                        {
                                            //20100416 Urgent Outage of 5030.
                                            if (GetValidationFieldsSettings("Payer ID"))
                                                strMessage += "Payer ID" + Environment.NewLine + "";
                                        }
                                        if (Convert.ToString(dtPatientInsurances.Rows[_InsRow]["PayerAddress1"]).Trim() == "")
                                        {
                                            if (GetValidationFieldsSettings("Payer Address"))
                                                strMessage += "Payer Address" + Environment.NewLine + "";
                                        }
                                        if (Convert.ToString(dtPatientInsurances.Rows[_InsRow]["PayerCity"]).Trim() == "")
                                        {
                                            if (GetValidationFieldsSettings("Payer City"))
                                                strMessage += "Payer City" + Environment.NewLine + "";
                                        }
                                        if (Convert.ToString(dtPatientInsurances.Rows[_InsRow]["PayerState"]).Trim() == "")
                                        {
                                            if (GetValidationFieldsSettings("Payer State"))
                                                strMessage += "Payer State" + Environment.NewLine + "";
                                        }
                                        if (Convert.ToString(dtPatientInsurances.Rows[_InsRow]["PayerZip"]).Trim() == "")
                                        {
                                            if (GetValidationFieldsSettings("Payer Zip"))
                                                strMessage += "Payer Zip" + Environment.NewLine + "";
                                        }

                                    }

                                    #endregion " Primary Insurance "

                                    #region " Secondary Insurance "
                                    if (_InsRow == 1)
                                    {
                                        //Other Insurance
                                        if (Convert.ToString(dtPatientInsurances.Rows[_InsRow]["SubLName"]).Trim() == "")
                                        {
                                            if (GetValidationFieldsSettings("Secondary Insurance Subscriber Last Name"))
                                                strMessage += "Secondary Insurance Subscriber Last Name" + Environment.NewLine + "";
                                        }
                                        //if (_OtherInsurancePST == "")
                                        //{
                                        //     if (GetValidationFieldsSettings("Secondary Insurance Type"))
                                        //    //strMessage += "Secondary Insurance Type" + Environment.NewLine + "";
                                        //}
                                        if (Convert.ToString(dtPatientInsurances.Rows[_InsRow]["InsuranceTypeCode"]).Trim() == "")
                                        {
                                            //20100416 Urgent Outage of 5030.
                                            if (GetValidationFieldsSettings("Secondary Plan Type"))
                                                strMessage += "Secondary Insurance Plan Type" + Environment.NewLine + "";
                                        }
                                        if (Convert.ToString(dtPatientInsurances.Rows[_InsRow]["RelationshipCode"]).Trim() == "")
                                        {
                                            // if (GetValidationFieldsSettings("Secondary Insurance Subscriber Relationship"))
                                            strMessage += "Secondary Insurance Subscriber Relationship" + Environment.NewLine + "";
                                        }
                                        if (Convert.ToString(dtPatientInsurances.Rows[_InsRow]["sSubscriberID"]).Trim() == "")
                                        {
                                            // if (GetValidationFieldsSettings("Secondary Insurance ID"))
                                            strMessage += "Secondary Insurance ID" + Environment.NewLine + "";
                                        }
                                        if (Convert.ToString(dtPatientInsurances.Rows[_InsRow]["sGroup"]).Trim() == "")
                                        {
                                            if (GetValidationFieldsSettings("Secondary Insurance Group ID"))
                                                strMessage += "Secondary Insurance Group ID" + Environment.NewLine + "";
                                        }
                                        if (Convert.ToString(dtPatientInsurances.Rows[_InsRow]["SubscriberAddr1"]).Trim() == "")
                                        {
                                            if (GetValidationFieldsSettings("Secondary Insurance Address"))
                                                strMessage += "Secondary Insurance Address" + Environment.NewLine + "";
                                        }
                                        if (Convert.ToString(dtPatientInsurances.Rows[_InsRow]["SubFName"]).Trim() == "")
                                        {
                                            if (GetValidationFieldsSettings("Secondary Insurance Subscriber First Name"))
                                                strMessage += "Secondary Insurance Subscriber First Name" + Environment.NewLine + "";
                                        }
                                        //if (_OtherInsuranceSubscriberMName == "")
                                        //{
                                        //    if (GetValidationFieldsSettings("Secondary Insurance Subscriber Middle Name"))
                                        //        strMessage += "Secondary Insurance Subscriber Middle Name" + Environment.NewLine + "";
                                        //}
                                        if (Convert.ToString(dtPatientInsurances.Rows[_InsRow]["InsuranceName"]) == "")
                                        {
                                            if (GetValidationFieldsSettings("Secondary Insurance Name"))
                                                strMessage += "Secondary Insurance Name" + Environment.NewLine + "";
                                        }
                                        if (Convert.ToString(dtPatientInsurances.Rows[_InsRow]["PayerID"]).Trim() == "")
                                        {
                                            //20100416 Urgent Outage of 5030.
                                            if (GetValidationFieldsSettings("Secondary Insurance Payer ID"))
                                                strMessage += "Secondary Insurance Payer ID" + Environment.NewLine + "";
                                        }
                                        if (Convert.ToString(dtPatientInsurances.Rows[_InsRow]["SubscriberCity"]).Trim() == "")
                                        {
                                            if (GetValidationFieldsSettings("Secondary Insurance City"))
                                                strMessage += "Secondary Insurance City" + Environment.NewLine + "";
                                        }
                                        if (Convert.ToString(dtPatientInsurances.Rows[_InsRow]["SubscriberState"]).Trim() == "")
                                        {
                                            if (GetValidationFieldsSettings("Secondary Insurance State"))
                                                strMessage += "Secondary Insurance State" + Environment.NewLine + "";
                                        }
                                        if (Convert.ToString(dtPatientInsurances.Rows[_InsRow]["SubscriberZip"]).Trim() == "")
                                        {
                                            if (GetValidationFieldsSettings("Secondary Insurance Zip"))
                                                strMessage += "Secondary Insurance Zip" + Environment.NewLine + "";
                                        }
                                        if (Convert.ToString(dtPatientInsurances.Rows[_InsRow]["dtDOB"]).Trim() == "")
                                        {
                                            // if (GetValidationFieldsSettings("Secondary Insurance Subscriber Date of Birth"))
                                            strMessage += "Secondary Insurance Subscriber Date of Birth" + Environment.NewLine + "";
                                        }
                                        if (Convert.ToString(dtPatientInsurances.Rows[_InsRow]["sSubscriberGender"]).Trim() == "")
                                        {
                                            // if (GetValidationFieldsSettings("Secondary Insurance Subscriber Gender"))
                                            strMessage += "Secondary Insurance Subscriber Gender" + Environment.NewLine + "";
                                        }
                                        if (Convert.ToString(dtPatientInsurances.Rows[_InsRow]["InsTypeCodeDefault"]).Trim() == "0" || Convert.ToString(dtPatientInsurances.Rows[_InsRow]["InsTypeCodeDefault"]).Trim() == string.Empty || Convert.ToString(dtPatientInsurances.Rows[_InsRow]["InsTypeCodeDefault"]).Trim() == "")
                                        {
                                            //20100416 Urgent Outage 5031
                                            if (GetValidationFieldsSettings("Secondary Insurance Type"))
                                                strMessage += "Secondary Insurance Type Code" + Environment.NewLine + "";
                                        }
                                    }

                                    #endregion " Secondary Insurance "

                                    #region " Tertiary Insurance "
                                    if (_InsRow == 2)
                                    {
                                        //Other Insurance
                                        if (Convert.ToString(dtPatientInsurances.Rows[_InsRow]["SubLName"]).Trim() == "")
                                        {
                                            if (GetValidationFieldsSettings("Tertiary Insurance Subscriber Last Name"))
                                                strMessage += "Tertiary Insurance Subscriber Last Name" + Environment.NewLine + "";
                                        }
                                        //if (_OtherInsurancePST == "")
                                        //{
                                        //     if (GetValidationFieldsSettings("Tertiary Insurance Type"))
                                        //    //strMessage += "Secondary Insurance Type" + Environment.NewLine + "";
                                        //}
                                        if (Convert.ToString(dtPatientInsurances.Rows[_InsRow]["InsuranceTypeCode"]).Trim() == "")
                                        {
                                            //20100416 Urgent Outage 5031
                                            if (GetValidationFieldsSettings("Tertiary Plan Type"))
                                                strMessage += "Tertiary Plan Type" + Environment.NewLine + "";
                                        }
                                        if (Convert.ToString(dtPatientInsurances.Rows[_InsRow]["RelationshipCode"]).Trim() == "")
                                        {
                                            // if (GetValidationFieldsSettings("Tertiary Insurance Subscriber Relationship"))
                                            strMessage += "Tertiary Insurance Subscriber Relationship" + Environment.NewLine + "";
                                        }
                                        if (Convert.ToString(dtPatientInsurances.Rows[_InsRow]["sSubscriberID"]).Trim() == "")
                                        {
                                            // if (GetValidationFieldsSettings("Tertiary Insurance ID"))
                                            strMessage += "Tertiary Insurance ID" + Environment.NewLine + "";
                                        }
                                        if (Convert.ToString(dtPatientInsurances.Rows[_InsRow]["sGroup"]).Trim() == "")
                                        {
                                            if (GetValidationFieldsSettings("Tertiary Insurance Group ID"))
                                                strMessage += "Tertiary Insurance Group ID" + Environment.NewLine + "";
                                        }
                                        if (Convert.ToString(dtPatientInsurances.Rows[_InsRow]["SubscriberAddr1"]).Trim() == "")
                                        {
                                            if (GetValidationFieldsSettings("Tertiary Insurance Address"))
                                                strMessage += "Tertiary Insurance Address" + Environment.NewLine + "";
                                        }
                                        if (Convert.ToString(dtPatientInsurances.Rows[_InsRow]["SubFName"]).Trim() == "")
                                        {
                                            if (GetValidationFieldsSettings("Tertiary Insurance Subscriber First Name"))
                                                strMessage += "Tertiary Insurance Subscriber First Name" + Environment.NewLine + "";
                                        }
                                        //if (_OtherInsuranceSubscriberMName == "")
                                        //{
                                        //    if (GetValidationFieldsSettings("Tertiary Insurance Subscriber Middle Name"))
                                        //        strMessage += "Tertiary Insurance Subscriber Middle Name" + Environment.NewLine + "";
                                        //}
                                        if (Convert.ToString(dtPatientInsurances.Rows[_InsRow]["InsuranceName"]) == "")
                                        {
                                            if (GetValidationFieldsSettings("Tertiary Insurance Name"))
                                                strMessage += "Tertiary Insurance Name" + Environment.NewLine + "";
                                        }
                                        if (Convert.ToString(dtPatientInsurances.Rows[_InsRow]["PayerID"]).Trim() == "")
                                        {
                                            //20100416 Urgent Outage of 5030.
                                            if (GetValidationFieldsSettings("Tertiary Insurance Payer ID"))
                                                strMessage += "Tertiary Insurance Payer ID" + Environment.NewLine + "";
                                        }
                                        if (Convert.ToString(dtPatientInsurances.Rows[_InsRow]["SubscriberCity"]).Trim() == "")
                                        {
                                            if (GetValidationFieldsSettings("Tertiary Insurance City"))
                                                strMessage += "Tertiary Insurance City" + Environment.NewLine + "";
                                        }
                                        if (Convert.ToString(dtPatientInsurances.Rows[_InsRow]["SubscriberState"]).Trim() == "")
                                        {
                                            if (GetValidationFieldsSettings("Tertiary Insurance State"))
                                                strMessage += "Tertiary Insurance State" + Environment.NewLine + "";
                                        }
                                        if (Convert.ToString(dtPatientInsurances.Rows[_InsRow]["SubscriberZip"]).Trim() == "")
                                        {
                                            if (GetValidationFieldsSettings("Tertiary Insurance Zip"))
                                                strMessage += "Tertiary Insurance Zip" + Environment.NewLine + "";
                                        }
                                        if (Convert.ToString(dtPatientInsurances.Rows[_InsRow]["dtDOB"]).Trim() == "")
                                        {
                                            // if (GetValidationFieldsSettings("Tertiary Insurance Subscriber Date of Birth"))
                                            strMessage += "Tertiary Insurance Subscriber Date of Birth" + Environment.NewLine + "";
                                        }
                                        if (Convert.ToString(dtPatientInsurances.Rows[_InsRow]["sSubscriberGender"]).Trim() == "")
                                        {
                                            //   if (GetValidationFieldsSettings("Tertiary Insurance Subscriber Gender"))
                                            strMessage += "Tertiary Insurance Subscriber Gender" + Environment.NewLine + "";
                                        }
                                        if (Convert.ToString(dtPatientInsurances.Rows[_InsRow]["InsTypeCodeDefault"]).Trim() == "0" || Convert.ToString(dtPatientInsurances.Rows[_InsRow]["InsTypeCodeDefault"]).Trim() == string.Empty || Convert.ToString(dtPatientInsurances.Rows[_InsRow]["InsTypeCodeDefault"]).Trim() == "")
                                        {
                                            //20100416 Urgent Outage 5031
                                            if (GetValidationFieldsSettings("Tertiary Insurance Type"))
                                                strMessage += "Tertiary Insurance Type Code" + Environment.NewLine + "";
                                        }
                                    }

                                    #endregion " Tertiary Insurance "
                                }
                            }
                            #endregion " Subscriber "

                            #region " Patient Information "

                            //Patient Information
                            if (oPatient != null)
                            {
                                if (Convert.ToString(oTransaction.ClaimNumber).Trim() == "")
                                {
                                    strMessage += "Patient Account No" + Environment.NewLine + "";
                                }
                                if (oPatient.DemographicsDetail.PatientLastName.Trim() == "")
                                {
                                    //if (GetValidationFieldsSettings("Patient Last Name"))
                                    strMessage += "Patient Last Name" + Environment.NewLine + "";
                                }
                                if (oPatient.DemographicsDetail.PatientFirstName.Trim() == "")
                                {
                                    if (GetValidationFieldsSettings("Patient First Name"))
                                        strMessage += "Patient First Name" + Environment.NewLine + "";
                                }
                                if (oPatient.DemographicsDetail.PatientMiddleName.Trim() == "")
                                {
                                    if (GetValidationFieldsSettings("Patient Middle Name"))
                                        strMessage += "Patient Middle Name" + Environment.NewLine + "";
                                }
                                if (oPatient.DemographicsDetail.PatientSSN.Trim() == "")
                                {
                                    if (GetValidationFieldsSettings("Patient SSN"))
                                        strMessage += "Patient SSN" + Environment.NewLine + "";
                                }
                                if (oPatient.DemographicsDetail.PatientGender.Trim() == "")
                                {
                                    // if (GetValidationFieldsSettings("Patient Gender"))
                                    strMessage += "Patient Gender" + Environment.NewLine + "";
                                }
                                if (Convert.ToString(gloDateMaster.gloDate.DateAsNumber(oPatient.DemographicsDetail.PatientDOB.ToShortDateString())).Trim() == "")
                                {
                                    //if (GetValidationFieldsSettings("Patient Date of Birth"))
                                    strMessage += "Patient Date of Birth" + Environment.NewLine + "";
                                }
                                if (oPatient.DemographicsDetail.PatientAddress1.Trim() == "")
                                {
                                    if (GetValidationFieldsSettings("Patient Address"))
                                        strMessage += "Patient Address" + Environment.NewLine + "";
                                }
                                if (oPatient.DemographicsDetail.PatientCity.Trim() == "")
                                {
                                    //  if (GetValidationFieldsSettings("Patient City"))
                                    strMessage += "Patient City" + Environment.NewLine + "";
                                }
                                if (oPatient.DemographicsDetail.PatientState.Trim() == "")
                                {
                                    // if (GetValidationFieldsSettings("Patient State"))
                                    strMessage += "Patient State" + Environment.NewLine + "";
                                }
                                if (oPatient.DemographicsDetail.PatientZip.Trim() == "")
                                {
                                    //   if (GetValidationFieldsSettings("Patient Zip"))
                                    strMessage += "Patient Zip" + Environment.NewLine + "";
                                }
                            }

                            #endregion " Patient Information "

                            #region " Rendering Provider "
                            if (_Provider != null)
                            {
                                _Provider.Dispose();
                                _Provider = null;
                            }
                            _Provider = oResource.GetProviderDetail(oTransaction.Lines[0].RefferingProviderId);

                            if (_Provider != null)
                            {
                                // 20100416 Urgent Outage of 5030.  
                                if (_Provider.LastName.Trim() == "")
                                {
                                    if (GetValidationFieldsSettings("Rendering Provider Last Name"))
                                        strMessage += "Rendering Provider Last Name" + Environment.NewLine + "";
                                }
                                if (_Provider.FirstName.Trim() == "")
                                {
                                    if (GetValidationFieldsSettings("Rendering Provider First Name"))
                                        strMessage += "Rendering Provider First Name" + Environment.NewLine + "";
                                }
                                if (_Provider.NPI.Trim() == "")
                                {
                                    if (GetValidationFieldsSettings("Rendering Provider NPI"))
                                        strMessage += "Rendering Provider NPI" + Environment.NewLine + "";
                                }
                                if (_Provider.Taxonomy.Trim() == "")
                                {
                                    if (GetValidationFieldsSettings("Rendering Provider Taxonomy Code"))
                                        strMessage += "Rendering Provider Taxonomy Code" + Environment.NewLine + "";
                                }
                            }

                            //Prior Authorization Number
                            //if (_PriorAuthorizationNo == "")
                            //{
                            //    //strMessage += "Prior Authorization Number" + Environment.NewLine + "";
                            //}

                            #endregion " Rendering Provider "

                            #region " Referring Provider "
                          
                            if (oTransaction.ReferralProviderID > 0)
                            {
                                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                                DataTable dtReferral = null;
                                string _sqlQuery = "";

                                oDB.Connect(false);

                                //_sqlQuery = " SELECT ISNULL(nContactId,0) AS nContactId, " +
                                //            " ISNULL(sName,'') AS sName,  " +
                                //            " ISNULL(sContact,'') AS sContact,   " +
                                //            " ISNULL(sAddressLine1,'') AS sAddressLine1,   " +
                                //            " ISNULL(sAddressLine2,'') AS sAddressLine2,   " +
                                //            " ISNULL(sCity,'') AS sCity,   " +
                                //            " ISNULL(sState,'') AS sState,   " +
                                //            " ISNULL(sZIP,'') AS sZIP,   " +
                                //            " ISNULL(sPhone,'') AS sPhone,   " +
                                //            " ISNULL(sFax,'') AS sFax,   " +
                                //            " ISNULL(sEmail,'') AS sEmail,   " +
                                //            " ISNULL(sURL,'') AS sURL,   " +
                                //            " ISNULL(sMobile,'') AS sMobile,   " +
                                //            " ISNULL(sPager,'') AS sPager,   " +
                                //            " ISNULL(sNotes,'') AS sNotes,   " +
                                //            " ISNULL(sFirstName,'') AS sFirstName,   " +
                                //            " ISNULL(sMiddleName,'') AS sMiddleName,   " +
                                //            " ISNULL(sLastName,'') AS sLastName,   " +
                                //            " ISNULL(sGender,'') AS sGender,   " +
                                //            " ISNULL(sTaxonomy,'') AS sTaxonomy,   " +
                                //            " ISNULL(sTaxonomyDesc,'') AS sTaxonomyDesc,   " +
                                //            " ISNULL(sTaxID,'') AS sTaxID,   " +
                                //            " ISNULL(sUPIN,'') AS sUPIN,   " +
                                //            " ISNULL(sNPI,'') AS sNPI,   " +
                                //            " ISNULL(sDegree,'') AS sDegree   " +
                                //            " FROM  Patient_DTL " +
                                //            " WHERE (nContactFlag = 3) AND (nPatientID = " + oTransaction.PatientID + ") AND (nPatientDetailID = " + oTransaction.ReferralProviderID + ") AND ISNULL(nClinicID,1)=" + ClinicID + "";

                                //20100322 Take the Data from Contact MST 

                                _sqlQuery = "SELECT ISNULL(Contacts_MST.sFirstName,'') AS sFirstName, " +
                                             "ISNULL(Contacts_MST.sMiddleName,'') AS sMiddleName , ISNULL(Contacts_MST.sLastName,'') AS sLastName ,ISNULL(Contacts_MST.sGender,'') AS  sGender ,  " +
                                             "ISNULL(Contacts_Physician_DTL.sTaxonomy,'') AS sTaxonomy , ISNULL(Contacts_Physician_DTL.sTaxonomyDesc,'') AS sTaxonomyDesc, " +
                                             "ISNULL(Contacts_Physician_DTL.sTaxID,'') AS sTaxID,ISNULL(Contacts_Physician_DTL.sNPI,'') AS sNPI  " +
                                             "FROM Contacts_MST WITH (NOLOCK) left outer join Contacts_Physician_DTL WITH (NOLOCK) ON Contacts_MST.nContactID = Contacts_Physician_DTL.nContactID  " +
                                             " WHERE  Contacts_MST.nContactID =(SELECT ISNULL(nContactId,0) from Patient_DTL WITH (NOLOCK) " +
                                             " WHERE (nContactFlag = 3) AND (nPatientID = " + oTransaction.PatientID + ") AND (nPatientDetailID = " + oTransaction.ReferralProviderID + ") AND ISNULL(nClinicID,1)=" + _ClinicID + ") AND ISNULL(Contacts_MST.nClinicID,1)=" + _ClinicID + " AND ISNULL(bIsBlocked,0)= 0";




                                oDB.Retrive_Query(_sqlQuery, out dtReferral);
                                oDB.Disconnect();
                                oDB.Dispose();
                                oDB = null;
                                if (dtReferral != null && dtReferral.Rows.Count > 0)
                                {
                                    //2310B Referring PROVIDER
                                    //NM1 Referring PROVIDER NAME

                                    if (dtReferral.Rows[0]["sLastName"].ToString().Trim().Replace("*", "") == "")
                                    {
                                        if (GetValidationFieldsSettings("Referring Provider Last Name"))
                                            strMessage += "Referring Provider Last Name" + Environment.NewLine + "";
                                    }
                                    if (dtReferral.Rows[0]["sFirstName"].ToString().Trim().Replace("*", "") == "")
                                    {
                                        if (GetValidationFieldsSettings("Referring Provider First Name"))
                                            strMessage += "Referring Provider First Name" + Environment.NewLine + "";
                                    }
                                    if (dtReferral.Rows[0]["sMiddleName"].ToString().Trim().Replace("*", "") == "")
                                    {
                                        //if (GetValidationFieldsSettings("Referring Provider Last Name"))
                                        //strMessage += "Referring Provider Last Name" + Environment.NewLine + "";
                                    }
                                    if (dtReferral.Rows[0]["sNPI"].ToString().Trim().Replace("*", "") == "")
                                    {
                                        if (GetValidationFieldsSettings("Referring Provider NPI"))
                                            strMessage += "Referring Provider NPI" + Environment.NewLine + "";
                                    }
                                    if (dtReferral.Rows[0]["sTaxonomy"].ToString().Trim().Replace("*", "") == "")
                                    {
                                        //20100414 Urgent Outage
                                        if (GetValidationFieldsSettings("Referring Provider Taxonomy"))
                                            strMessage += "Referring Provider Taxonomy" + Environment.NewLine + "";
                                    }
                                    if (dtReferral.Rows[0]["sTaxID"].ToString().Trim().Replace("*", "") == "")
                                    {
                                        if (GetValidationFieldsSettings("Referring Provider TaxID"))
                                            strMessage += "Referring Provider TaxID" + Environment.NewLine + "";
                                    }
                                }
                                else
                                {
                                    strMessage += "Referring Provider Information" + Environment.NewLine + "";
                                }
                                if (dtReferral != null)
                                {
                                    dtReferral.Dispose();
                                    dtReferral = null;
                                }

                            }
                            else
                            {
                                //20100414 Urgent Outage
                                gloAppointmentBook.Books.Provider _BillingProvider = oResource.GetProviderDetail(Convert.ToInt64(oTransaction.ProviderID));

                                if (_BillingProvider != null)
                                {
                                    if (_BillingProvider.LastName.ToString().Trim().Replace("*", "") == "")
                                    {
                                        if (GetValidationFieldsSettings("Referring Provider Last Name"))
                                            strMessage += "Billing Provider Last Name" + Environment.NewLine + "";
                                    }
                                    if (_BillingProvider.FirstName.ToString().Trim().Replace("*", "") == "")
                                    {
                                        if (GetValidationFieldsSettings("Referring Provider First Name"))
                                            strMessage += "Billing Provider First Name" + Environment.NewLine + "";
                                    }
                                    if (_BillingProvider.MiddleName.ToString().Trim().Replace("*", "") == "")
                                    {
                                        //if (GetValidationFieldsSettings("Referring Provider Last Name"))
                                        //strMessage += "Referring Provider Last Name" + Environment.NewLine + "";
                                    }
                                    if (_BillingProvider.NPI.ToString().Trim().Replace("*", "") == "")
                                    {
                                        if (GetValidationFieldsSettings("Referring Provider NPI"))
                                            strMessage += "Billing Provider NPI" + Environment.NewLine + "";
                                    }
                                    if (_BillingProvider.Taxonomy.ToString().Trim().Replace("*", "") == "")
                                    {
                                        if (GetValidationFieldsSettings("Referring Provider Taxonomy"))
                                            strMessage += "Billing Provider Taxonomy" + Environment.NewLine + "";
                                    }
                                    if (_BillingProvider.EmployerID.ToString().Trim().Replace("*", "") == "")
                                    {
                                        if (_BillingProvider.SSN.ToString().Trim().Replace("*", "") == "")
                                        {
                                            if (GetValidationFieldsSettings("Referring Provider TaxID"))
                                                strMessage += "Billing Provider EmployerID and Billing Provider SSN" + Environment.NewLine + "";
                                        }
                                    }
                                    _BillingProvider.Dispose();
                                    _BillingProvider = null;
                                }

                            }

                            #endregion " Referring Provider "

                            if (_AlphaIIValidation == "Alpha2")
                            {
                                string _strMessage = "";
                                if (ValidateConnectionString())
                                {
                                    for (int _index = 0; _index <= oTransaction.Lines.Count - 1; _index++)
                                    {
                                        if (oTransaction.Lines[_index].Dx1Code.Trim() != "")
                                        {
                                            if (ValidateUsingAlphaII(oTransaction.Lines[_index].Dx1Code, oTransaction.Lines[_index].DateServiceFrom) == false)
                                            {
                                                if (IsDiagnosisExist(_strMessage, oTransaction.Lines[_index].Dx1Code.Trim()) == false)
                                                {
                                                    _strMessage += " " + oTransaction.Lines[_index].Dx1Code.Trim() + ",";
                                                }
                                            }
                                        }
                                        if (oTransaction.Lines[_index].Dx2Code.Trim() != "")
                                        {
                                            if (ValidateUsingAlphaII(oTransaction.Lines[_index].Dx2Code, oTransaction.Lines[_index].DateServiceFrom) == false)
                                            {
                                                if (IsDiagnosisExist(_strMessage, oTransaction.Lines[_index].Dx2Code.Trim()) == false)
                                                {
                                                    _strMessage += " " + oTransaction.Lines[_index].Dx2Code.Trim() + ",";
                                                }
                                            }
                                        }
                                        if (oTransaction.Lines[_index].Dx3Code.Trim() != "")
                                        {
                                            if (ValidateUsingAlphaII(oTransaction.Lines[_index].Dx3Code, oTransaction.Lines[_index].DateServiceFrom) == false)
                                            {
                                                if (IsDiagnosisExist(_strMessage, oTransaction.Lines[_index].Dx3Code.Trim()) == false)
                                                {
                                                    _strMessage += " " + oTransaction.Lines[_index].Dx3Code.Trim() + ",";
                                                }
                                            }
                                        }
                                        if (oTransaction.Lines[_index].Dx4Code.Trim() != "")
                                        {
                                            if (ValidateUsingAlphaII(oTransaction.Lines[_index].Dx4Code, oTransaction.Lines[_index].DateServiceFrom) == false)
                                            {
                                                if (IsDiagnosisExist(_strMessage, oTransaction.Lines[_index].Dx4Code.Trim()) == false)
                                                {
                                                    _strMessage += " " + oTransaction.Lines[_index].Dx4Code.Trim() + ",";
                                                }
                                            }
                                        }

                                    }
                                    if (_strMessage.Trim() != "")
                                    {
                                        _strMessage = _strMessage.Substring(0, _strMessage.Length - 1);
                                        strMessage += "Invalid ICD9's by Alpha II: " + _strMessage + Environment.NewLine + "";
                                        if (IsEDIgeneration)
                                        {
                                            //return false;
                                        }
                                    }
                                    if (_IsClaimNumberAdded == false)
                                    {

                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Connection for Alpha II cannot be establish, please do the setting from gloPM Admin.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return false;
                                }
                            }

                            if (strMessage.Trim() != "")
                            {
                                oClaimNoArray.Add(oTransaction.ClaimNumber);
                                _MessageHeader += _ClaimMessageHeader + strMessage;
                            }
                            if (oTransaction != null)
                            {
                                oTransaction.Dispose();
                                oTransaction = null;
                            }
                            if (_Provider != null)
                            {
                                _Provider.Dispose();
                                _Provider = null;
                            }
                            if (oPatient != null)
                            {
                                oPatient.Dispose();
                                oPatient = null;
                            }

                            if( dtPatientInsurances!= null )
                            {
                                dtPatientInsurances.Dispose();
                                dtPatientInsurances = null;
                            }
                            if (dtFacility != null)
                            {
                                dtFacility.Dispose();
                                dtFacility = null;
                            }

                        }

                        oResource.Dispose();
                        oResource=null;
                        
                        ogloPatient.Dispose();
                        ogloPatient = null ;
                        
                        oSettings.Dispose();
                        oSettings=null;
                        

                    }
                    if (_MessageHeader != "")
                    {
                        _Message = "";
                        _Message = _MessageHeader;
                    }
                }

                if (_Message.Trim() != "")
                {
                    string _Header = "Following fields are missing in database:" + Environment.NewLine + "" + Environment.NewLine + "";
                    _Header += _Message;
                    _FilePath = _FilePath + "EDIValidation.txt";
                    System.IO.StreamWriter oStreamWriter = new System.IO.StreamWriter(_FilePath, false);
                    oStreamWriter.WriteLine(_Header);
                    oStreamWriter.Close();
                    oStreamWriter.Dispose();
                    System.Diagnostics.Process.Start(_FilePath);
                    return false;
                }
                else
                {
                    //if (_bSendingToBatch == false)
                    //{
                    //    MessageBox.Show("All data is present and valid.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //}
                    return true;
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return false;
            }
            finally
            {
                if (dtClearingHouse != null) { dtClearingHouse.Dispose(); }
                if (dtSubmitter != null) { dtSubmitter.Dispose(); }
                if (ogloBilling != null) { ogloBilling.Dispose(); }
                if (dtFacility != null) { dtFacility.Dispose(); }
                if (dtPatientInsurances != null) { dtPatientInsurances.Dispose(); }
            }

        }


        private void UpdateBatchSentCounter(Int64 BatchId, Int64 ClinicId)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string _sqlQuery = "";

            try
            {
                oDB.Connect(false);
                _sqlQuery = " UPDATE BL_Transaction_Batch WITH (READPAST) SET nBatchSend = (ISNULL(nBatchSend,0) +1) " +
                " WHERE nBatchID = " + BatchId + " AND nClinicID = " + _ClinicID + "";
                oDB.Execute_Query(_sqlQuery);
                oDB.Disconnect();
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); }
            }
        }

        private bool GetValidationFieldsSettings(string SettingName)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string _strSQL = "";
            object _result = null;
            bool _IsSettingPresent = false;
            try
            {
                oDB.Connect(false);
                _strSQL = "SELECT  sSettingsValue FROM BL_Settings_EDI WITH (NOLOCK) WHERE sSettingsName='" + SettingName + "'";
                _result = oDB.ExecuteScalar_Query(_strSQL);
                if (_result != null && Convert.ToString(_result) != "")
                {
                    _IsSettingPresent = Convert.ToBoolean(_result);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); }
            }
            return _IsSettingPresent;
        }

        private void GetAlphaIISettings()
        {
            gloSettings.GeneralSettings ogloSettings = new gloSettings.GeneralSettings(_databaseconnectionstring);
            object value = null;
            try
            {
                ogloSettings.GetSetting("AlphaII SQL Server Name", out value);
                if (value != null && Convert.ToString(value).Trim() != "")
                {
                    _AlphaIIServerName = Convert.ToString(value.ToString());
                    value = null;
                }

                ogloSettings.GetSetting("AlphaII Database Name", out value);
                if (value != null && Convert.ToString(value).Trim() != "")
                {
                    _AlphaIIDatabase = Convert.ToString(value.ToString());
                    value = null;
                }

                ogloSettings.GetSetting("AlphaII Authentication", out value);
                if (value != null && Convert.ToString(value).Trim() != "")
                {
                    _AlphaIIAuthentication = Convert.ToString(value.ToString());
                    value = null;
                }
                ogloSettings.GetSetting("AlphaII User Name", out value);
                if (value != null && Convert.ToString(value).Trim() != "")
                {
                    _AlphaIIUserName = Convert.ToString(value.ToString());
                    value = null;
                }
                ogloSettings.GetSetting("AlphaII Password", out value);
                if (value != null && Convert.ToString(value).Trim() != "")
                {
                    _AlphaIIPassword = Convert.ToString(value.ToString());
                    value = null;
                }
                ogloSettings.GetSetting("ClaimValidationSetting", out value);
                if (value != null && Convert.ToString(value).Trim() != "")
                {
                    _AlphaIIValidation = Convert.ToString(value.ToString());
                    value = null;
                }
                ogloSettings.GetSetting("IsCheckInvalidICD9", out value);
                if (value != null && Convert.ToString(value) != "")
                {
                    _IsCheckInvalidICD9 = Convert.ToBoolean(value);
                    value = null;
                }
                ogloSettings.GetSetting("IsUseScrubber", out value);
                if (value != null && Convert.ToString(value) != "")
                {
                    _IsScrubber = Convert.ToBoolean(value);
                    value = null;
                }
                ogloSettings.GetSetting("ShowMessageIfNoValidation", out value);
                if (value != null && Convert.ToString(value) != "")
                {
                    _ShowMessageForValidation = Convert.ToBoolean(value);
                    value = null;
                }


            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (ogloSettings != null) { ogloSettings.Dispose(); }
                value = null;
            }
        }

        private bool ValidateConnectionString()
        {
            Boolean _Result = false;
            System.Data.SqlClient.SqlConnection _connection = null;

            try
            {
                string _connstring = "";

                if (_AlphaIIAuthentication.ToUpper() == "WINDOWS")
                {
                    //_connstring = "Server=" + _AlphaIIServerName + ";Database=" + _AlphaIIDatabase + ";Integrated Security=SSPI; Connection Timeout = 0";
                    _connstring = "Integrated Security=SSPI; Persist Security Info=False; Data Source=" + _AlphaIIServerName + "; Initial Catalog=" + _AlphaIIDatabase + "; Connection Timeout = 0";
                    //Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=CodeWizard;Data Source=GLOINT
                }
                else
                {
                    //_connstring = "Server=" + _AlphaIIServerName + ";Database=" + _AlphaIIDatabase + ";Uid=" + _AlphaIIUserName + ";Pwd=" + _AlphaIIPassword + ";";
                    _connstring = "Persist Security Info=False;Data Source=" + _AlphaIIServerName + ";Initial Catalog=" + _AlphaIIDatabase + ";User ID=" + _AlphaIIUserName + ";Pwd=" + _AlphaIIPassword + ";";
                    // Persist Security Info=False;User ID=sa;Initial Catalog=CodeWizard;Data Source=GLOINT
                }

                _connection = new System.Data.SqlClient.SqlConnection();
                _connection.ConnectionString = _connstring;
                _connection.Open();
                _connection.Close();
                _Result = true;
            }
            catch //(Exception ex)
            {
                _Result = false;
            }
            finally
            {
                if (_connection != null)
                {
                    _connection.Dispose();
                    _connection = null;
                }
            }
            //if (_Result == false)
            //{
            //    MessageBox.Show("Connection can not established with given parameter, please verify it", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}

            return _Result;
        }

        private bool IsDiagnosisExist(string strMessage, string DxCode)
        {
            string[] strDxList = null;
            bool _IsExist = false;
            try
            {
                if (strMessage.Trim() != "")
                {
                    strDxList = strMessage.Trim().Split(',');
                    if (strDxList != null && strDxList.Length > 0)
                    {
                        for (int i = 0; i < strDxList.Length; i++)
                        {
                            if (strDxList[i].Trim().ToUpper() == DxCode.Trim().ToUpper())
                            {
                                _IsExist = true;
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            return _IsExist;
        }

        private bool ValidateUsingAlphaII(string DxCode, DateTime DateOfService)
        {
            try
            {
                string ConnectionString = "";

                ConnectionString = GetAlphaIIConnectionString();
                if (ConnectionString != "")
                {
                    //AlphaII.CodeWizard.DataAccess.Common.ConnectionString = ConnectionString;
                    //_IsAplhaIIValidated = GetValidCode(DxCode.Trim(), DateOfService, out  _IsAplhaIIValidated);
                    AlphaII.CodeWizard.Configuration.DatabaseConfiguration oDatabaseConfiguration = new AlphaII.CodeWizard.Configuration.DatabaseConfiguration();
                    oDatabaseConfiguration.MsSqlServer = _AlphaIIServerName;
                    oDatabaseConfiguration.MsSqlDatabase = _AlphaIIDatabase;
                    oDatabaseConfiguration.MsSqlUserId = _AlphaIIUserName;
                    oDatabaseConfiguration.MsSqlPassword = _AlphaIIPassword;
                    oDatabaseConfiguration.MsSqlPersistSecurity = false;
                    if (_AlphaIIAuthentication.ToUpper() == "WINDOWS")
                    {
                        oDatabaseConfiguration.MsSqlIntegratedSecurity = true;
                    }
                    else if (_AlphaIIAuthentication.ToUpper() == "SQL")
                    {
                        oDatabaseConfiguration.MsSqlIntegratedSecurity = false;
                    }
                    oDatabaseConfiguration.Save();
                    AlphaII.CodeWizard.Coding oCoding = new AlphaII.CodeWizard.Coding();
                    _IsAplhaIIValidated = oCoding.ValidateDiagnosisCode(DxCode.Trim(), DateOfService);
                    string strDxDesc = oCoding.GetDiagnosisDescription(DxCode.Trim(), DateOfService, AlphaII.CodeWizard.Objects.DescriptionType.Long);
                    oCoding = null;
                    oDatabaseConfiguration = null;

                }
                //else
                //{
                //    MessageBox.Show("Connection for Alpha II cannot be establish, please do the setting from gloPM Admin.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                //}

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
                Application.DoEvents();
                return true;
            }
            finally
            {

            }
            return _IsAplhaIIValidated;
        }

        private bool AlphaIIDiagnosisValidation(TransactionLines oTransactionLines, string ClaimNo)
        {
            string _strMessage = "";
            string _strMessageFiller = "is";
            bool _result = true;
            try
            {
                if (oTransactionLines != null && oTransactionLines.Count > 0)
                {
                    for (int i = 0; i <= oTransactionLines.Count - 1; i++)
                    {
                        if (oTransactionLines[i].Dx1Code.Trim() != "")
                        {
                            if (ValidateUsingAlphaII(oTransactionLines[i].Dx1Code, oTransactionLines[i].DateServiceFrom) == false)
                            {
                                _strMessage += " " + oTransactionLines[i].Dx1Code.Trim() + ",";
                            }
                        }
                        if (oTransactionLines[i].Dx2Code.Trim() != "")
                        {
                            if (ValidateUsingAlphaII(oTransactionLines[i].Dx2Code, oTransactionLines[i].DateServiceFrom) == false)
                            {
                                _strMessage += " " + oTransactionLines[i].Dx2Code.Trim() + ",";
                            }
                        }
                        if (oTransactionLines[i].Dx3Code.Trim() != "")
                        {
                            if (ValidateUsingAlphaII(oTransactionLines[i].Dx3Code, oTransactionLines[i].DateServiceFrom) == false)
                            {
                                _strMessage += " " + oTransactionLines[i].Dx3Code.Trim() + ",";
                            }
                        }
                        if (oTransactionLines[i].Dx4Code.Trim() != "")
                        {
                            if (ValidateUsingAlphaII(oTransactionLines[i].Dx4Code, oTransactionLines[i].DateServiceFrom) == false)
                            {
                                _strMessage += " " + oTransactionLines[i].Dx4Code.Trim() + ",";
                            }
                        }

                    }
                    if (_strMessage.Trim() != "")
                    {
                        if (_strMessage.Trim().Length > 7)
                        {
                            _strMessageFiller = "";
                            _strMessageFiller = "are";
                        }
                        _strMessage = _strMessage.Substring(0, _strMessage.Length - 1);
                        MessageBox.Show("The Diagnosis code(s) " + _strMessage.Trim() + " " + _strMessageFiller + " not valid in claim number " + ClaimNo + ".  ", "gloPM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        _result = false;
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            return _result;
        }

        private string GetAlphaIIConnectionString()
        {
            string _connstring = "";
            try
            {
                if (_AlphaIIAuthentication.ToUpper() == "WINDOWS")
                {
                    if (_AlphaIIServerName != "" && _AlphaIIDatabase != "")
                    {
                        _connstring = "Integrated Security=SSPI; Persist Security Info=False; Data Source=" + _AlphaIIServerName + "; Initial Catalog=" + _AlphaIIDatabase + "; Connection Timeout = 0";
                        //Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=CodeWizard;Data Source=GLOINT

                    }
                    else
                    {
                        return "";
                    }
                }
                else
                {
                    if (_AlphaIIServerName != "" && _AlphaIIDatabase != "" && _AlphaIIUserName != "")//&& _AlphaIIPassword != "")
                    {
                        _connstring = "Persist Security Info=False;Data Source=" + _AlphaIIServerName + ";Initial Catalog=" + _AlphaIIDatabase + ";User ID=" + _AlphaIIUserName + ";Pwd=" + _AlphaIIPassword + ";";
                        // Persist Security Info=False;User ID=sa;Initial Catalog=CodeWizard;Data Source=GLOINT
                    }
                    else
                    {
                        return "";
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "gloPM", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            return _connstring;
        }
    }

    public class clsgloResendNote : IDisposable
    {
        #region "Constructor & Destructor"

        public clsgloResendNote()
        {
            _databaseconnectionstring = appSettings["DataBaseConnectionString"].ToString();
            _emrdatabaseconnectionstring = string.Empty;


            #region " Retrieve ClinicID from AppSettings "

            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 0; }
            }
            else
            { _ClinicID = 0; }

            #endregion

            #region " Retrive UserID from appSettings "

            if (appSettings["UserID"] != null)
            {
                if (appSettings["UserID"] != "")
                {
                    _UserID = Convert.ToInt64(appSettings["UserID"]);
                }
            }
            else
            {
                _UserID = 0;
            }

            #endregion

            #region " Retrive UserName from appSettings "

            if (appSettings["UserName"] != null)
            {
                if (appSettings["UserName"] != "")
                {
                    _UserName = Convert.ToString(appSettings["UserName"]);
                }
            }
            else
            {
                _UserName = "";
            }

            #endregion

            #region " Retrieve MessageBoxCaption from AppSettings "

            if (appSettings["MessageBOXCaption"] != null)
            {
                if (appSettings["MessageBOXCaption"] != "")
                {
                    _messageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    _messageBoxCaption = "";
                }
            }
            else
            { _messageBoxCaption = ""; }

            #endregion
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

        ~clsgloResendNote()
        {
            Dispose(false);
        }

        #endregion

        #region "Variables"

        public Int64 nBatchID = 0;
        public string sBatchName = "";
        public Int64 nTransactionID = 0;
        public Int64 nClinicID = 0;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;


        Int64 _ClinicID = 0;
        Int64 _UserID = 0;
        string _UserName = "";
        
        string _databaseconnectionstring = "";
        string _emrdatabaseconnectionstring = "";
        string _messageBoxCaption = "";
      

        #endregion "Variables"

        #region "PROPERTY"


        #endregion

    }

    public class gloPendingClaimBilling : IDisposable
    {
        #region "Constructor & Destructor"

            public gloPendingClaimBilling()
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

            ~gloPendingClaimBilling()
            {
                Dispose(false);
            }

            #endregion

        #region " Public and Private variable declaration "
        #endregion " Public and Private variable declaration "

        #region " Public methods "

            public Int64 BillPendingClaim(Int64 transactionId, Int64 transactionmasterId, Int64 dtRespTransferCloseDate)
            {
                gloDatabaseLayer.DBLayer dbLayer = null;
                gloDatabaseLayer.DBParameters dbParameters = null;
                Int64 newtransactionId = 0;
                Hashtable returnValue = null;

                try
                {
                    dbParameters = new gloDatabaseLayer.DBParameters();
                    dbParameters.Add("@TransactionMasterID", transactionmasterId, ParameterDirection.Input, SqlDbType.BigInt);
                    dbParameters.Add("@TransactionID", transactionId, ParameterDirection.Input, SqlDbType.BigInt);
                    dbParameters.Add("@NextActionCode", "B", ParameterDirection.Input, SqlDbType.VarChar);
                    dbParameters.Add("@NextActionDesc", "Bill", ParameterDirection.Input, SqlDbType.VarChar);
                    dbParameters.Add("@RespTransferCloseDate", dtRespTransferCloseDate, ParameterDirection.Input, SqlDbType.BigInt);
                    dbParameters.Add("@UserID", gloGlobal.gloPMGlobal.UserID, ParameterDirection.Input, SqlDbType.BigInt);
                    dbParameters.Add("@UserName", gloGlobal.gloPMGlobal.UserName, ParameterDirection.Input, SqlDbType.VarChar);
                    dbParameters.Add("@OutNewTransactionID", transactionmasterId, ParameterDirection.Input, SqlDbType.BigInt); //NUMERIC(18, 0) = 0 OUTPUT 

                    dbLayer = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);

                    dbLayer.Connect(false);
                    returnValue = dbLayer.Execute("gsp_BillPendingClaim", dbParameters, true);
                    dbLayer.Disconnect();

                    if (returnValue["@OutNewTransactionID"] != null && Convert.ToString(returnValue["@OutNewTransactionID"]) != string.Empty)
                    {
                        Int64.TryParse(Convert.ToString(returnValue["@OutNewTransactionID"]), out newtransactionId);
                    }

                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (returnValue != null) { returnValue = null; }
                    if (dbParameters != null) { dbParameters.Clear(); dbParameters.Dispose(); dbParameters = null; }
                    if (dbLayer != null) { dbLayer.Disconnect(); dbLayer.Dispose(); dbLayer = null; }
                }

                return newtransactionId;
            }

        #endregion " Public methods "
    }
  
}
