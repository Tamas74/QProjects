using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using gloBilling.Common;
using System.Windows.Forms;
using System.Collections;

using gloSettings;
using gloBilling.Payment;
using System.Data.SqlClient;
using gloBilling.Collections;

namespace gloBilling
{

    public class ClaimStructure
    {
        private CSClaims _Claims;
        private Transaction _Transaction;
        private string _databaseconnectionstring = "";

        #region "Constructor & Destructor"

        public ClaimStructure(string DatabaseConnectionString)
        {
            _Claims = new CSClaims();
            _Transaction = new Transaction();
            _databaseconnectionstring = DatabaseConnectionString;
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
                    _Claims.Dispose();
                    if (bAssignedTransaction)
                    {
                        if (_Transaction != null)
                        {
                            
                            _Transaction.Dispose();
                            _Transaction = null;
                           
                        }
                        bAssignedTransaction = false;
                    }
                }
            }
            disposed = true;
        }

        ~ClaimStructure()
        {
            Dispose(false);
        }

        #endregion

        #region Property Procedures of Transaction Class
        private Boolean bAssignedTransaction = true;
        public Transaction Transaction
        {
            get { return _Transaction; }
            set 
            {
                if (bAssignedTransaction)
                {
                    if (_Transaction != null)
                    {

                        _Transaction.Dispose();
                        _Transaction = null;
                       
                    }
                    bAssignedTransaction = false;
                }
                _Transaction = value; 
            }
        }

        public CSClaims CSClaims
        {
            set { _Claims = value; }
            get { return _Claims; }
        }

        #endregion


        public bool SplitClaim(Boolean NewClaimNo)
        {
            bool _result = false;

            gloBilling ogloBilling = new gloBilling(_databaseconnectionstring, _databaseconnectionstring);
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            String _sqlQuery = "";
            String _DetailIDMasterList = "0";
            String _DetailIDList = "0";
            try
            {
                for (int i = 0; i < _Transaction.Lines.Count; i++)
                {
                    _DetailIDMasterList += "," + _Transaction.Lines[i].TransactionMasterDetailID;
                }
                for (int i = 0; i < _Transaction.Lines.Count; i++)
                {
                    _DetailIDList += "," + _Transaction.Lines[i].TransactionDetailID;
                }


                oDB.Connect(false);
                if (_Claims != null && _Claims.Count > 0)
                {
                    #region " Update Parent Claims "

                    //Close old Claim and Change Transaction Status to Insurance Paid
                    //Unhold Main Claim
                    //_sqlQuery = "Update BL_Transaction_Claim_MST set nStatus=" + TransactionStatus.InsurancePaid.GetHashCode() + ", nClaimStatus = " + ClaimStatus.Close.GetHashCode() + " where nTransactionMasterID = " + _Transaction.TransactionMasterID + " and nTransactionID= " + _Transaction.TransactionID + " and nStatus<>" + TransactionStatus.Rejected.GetHashCode();
                    _sqlQuery = "Update BL_Transaction_Claim_MST WITH(READPAST) set nClaimStatus = " + ClaimStatus.Close.GetHashCode() + " where nTransactionMasterID = " + _Transaction.TransactionMasterID + " and nTransactionID= " + _Transaction.TransactionID + " and nStatus<>" + TransactionStatus.Rejected.GetHashCode();
                    //_sqlQuery = "Update BL_Transaction_Claim_MST set nStatus=" + TransactionStatus.InsurancePaid.GetHashCode() + ", nClaimStatus = " + ClaimStatus.Close.GetHashCode() + " where nTransactionMasterID = " + _SplitClaimDetails.TransactionMasterID + "  and nStatus<>" + TransactionStatus.Rejected.GetHashCode();                    

                    oDB.Execute_Query(_sqlQuery);

                  //  Int16 _NextAction = 0;

                    //Update bIsSplitted for sub-claim
                    _sqlQuery = "update BL_Transaction_Claim_Lines WITH(READPAST) set bIsSplitted = 1 " +
                                     " where nTransactionMasterID=" + _Transaction.TransactionMasterID + " and nTransactionID =" + _Transaction.TransactionID + "   and nTransactionDetailID in (" + _DetailIDList + ")";
                    oDB.Execute_Query(_sqlQuery);

                    #endregion " Update Parent Claims "

                    for (int i = 0; i < _Claims.Count; i++)
                    {
                        string sMainClaimNo = string.Empty;

                        Transaction oTransactionMaster = null;
                        //Get Transaction information
                        //Create Sub-Claim from Main Claim
                        oTransactionMaster = ogloBilling.GetChargesClaimDetails(_Transaction.TransactionID, _Transaction.ClinicID);

                        if (_Transaction.ClaimNo == 0)
                        { _Transaction.ClaimNo = oTransactionMaster.ClaimNo; }

                        if (oTransactionMaster.SubClaimNo.Contains("-") == false)
                        { sMainClaimNo = oTransactionMaster.SubClaimNo; }
                        else
                        { sMainClaimNo = oTransactionMaster.MainClaimNo; }

                        oTransactionMaster.TransactionID = 0;

                        oTransactionMaster.TransactionMasterID = _Transaction.TransactionMasterID;
                        //Set Parent Claim ID
                        oTransactionMaster.ParentTransactionID = _Transaction.TransactionID;
                        //Set Parent Claim No                                    
                        if (_Transaction.SubClaimNo.Trim() != String.Empty)
                        { oTransactionMaster.ParentClaimNo = _Transaction.ClaimNo.ToString() + "-" + _Transaction.SubClaimNo; }
                        else
                        { oTransactionMaster.ParentClaimNo = _Transaction.ClaimNo.ToString(); }
                        //New Sub-Claim No

                        oTransactionMaster.SubClaimNo = GetSubClaimNo(_Transaction.TransactionMasterID);


                        //Set sub-claim status to open
                        oTransactionMaster.ClaimStatus = ClaimStatus.Open;
                        oTransactionMaster.Transaction_Status = TransactionStatus.Queue; ;


                        #region " HOLD SUB CLAIM "
                        if (oTransactionMaster.Hold != null && oTransactionMaster.Hold.IsHold == true)
                        {
                            if (i == 0)
                            {
                                _sqlQuery = "Update BL_Transaction_Claim_MST WITH(READPAST) set nStatus=" + TransactionStatus.InsurancePaid.GetHashCode() + ", nClaimStatus = " + ClaimStatus.Close.GetHashCode() + " where nTransactionMasterID = " + _Transaction.TransactionMasterID + " and nTransactionID in (select nParentTransactionID from dbo.BL_Transaction_Claim_MST where nTransactionID = " + _Transaction.TransactionID + " and nTransactionMasterID = " + _Transaction.TransactionMasterID + ")";
                                oDB.Execute_Query(_sqlQuery);
                            }
                            oTransactionMaster.Hold.HoldModified = true;
                        }

                        #endregion " HOLD SUB CLAIM "


                        //Remove Service line which are not part of sub-claim
                        for (int j = oTransactionMaster.Lines.Count - 1; j >= 0; j--)
                        {
                            oTransactionMaster.Lines[j].AnesthesiaID = 0;

                            oTransactionMaster.Lines[j].IsLineSplitted = false;
                            bool _found = false;
                            for (int k = 0; k < _Claims[i].CSClaimLines.Count; k++)
                            {
                                if (_Claims[i].CSClaimLines[k].CPT == oTransactionMaster.Lines[j].CPTCode &&
                                    _Claims[i].CSClaimLines[k].TransactionMasterDetailID == oTransactionMaster.Lines[j].TransactionMasterDetailID &&
                                    _Claims[i].CSClaimLines[k].TransactionDetailID == oTransactionMaster.Lines[j].TransactionDetailID)
                                {
                                    _found = true;
                                    break;
                                }
                            }


                            if (!_found)
                            {
                                oTransactionMaster.Lines.RemoveAt(j);
                            }
                            else
                            {
                                //Set Service Line Parent information for Sub-Claim
                                //oTransactionMaster.Lines[j].TransactionMasterDetailID = oTransactionMaster.Lines[j].TransactionDetailID;                                        
                                oTransactionMaster.Lines[j].ParentTransactionID = oTransactionMaster.TransactionID;
                                oTransactionMaster.Lines[j].ParentTransactionDetailID = oTransactionMaster.Lines[j].TransactionDetailID;
                                oTransactionMaster.Lines[j].TransactionDetailID = 0;
                            }
                        }



                        #region " Add records to transaction tracking tables"
                        //Add Sub-Claim to Tracking Table
                        ogloBilling.AddTransactionClaim(oTransactionMaster, _Transaction.ClinicID);

                        #region "Carry Forward Claim Insurance Follow Up to Child Claim"

                        CL_FollowUpCode.CarryForwardClaimFollowupToChild(oTransactionMaster.TransactionID, _Transaction.TransactionID);
                        
                        #endregion "Carry Forward Claim Insurance Follow Up to Child Claim"

                        #endregion " Add records to transaction tracking tables"

                        #region " Update BL_EOB_NextAction with new Tracking ID "

                        if (oTransactionMaster.Lines != null && oTransactionMaster.Lines.Count > 0)
                        {
                            for (int l = 0; l < oTransactionMaster.Lines.Count; l++)
                            {
                                _sqlQuery = " Update BL_EOB_NextAction WITH(READPAST) set " +
                                                " sNextActionCode = 'B'," +
                                                " sNextActionDescription = 'BILL'," +
                                                " sSubClaimNo = '" + oTransactionMaster.SubClaimNo + "'," +
                                                "  nTrackMstTrnID= " + oTransactionMaster.TransactionID + "," +
                                                "  nTrackMstTrnDetailID=" + oTransactionMaster.Lines[l].TransactionDetailID +
                                                " where nBillingTransactionID = " + oTransactionMaster.TransactionMasterID +
                                                " and nBillingTransactionDetailID = " + oTransactionMaster.Lines[l].TransactionMasterDetailID + "";
                                oDB.Execute_Query(_sqlQuery);

                                ogloBilling.InsertNextActionHistory(oTransactionMaster.ClaimNo, oTransactionMaster.SubClaimNo, oTransactionMaster.TransactionMasterID, oTransactionMaster.Lines[l].TransactionMasterDetailID, oTransactionMaster.TransactionID, oTransactionMaster.Lines[l].TransactionDetailID, oTransactionMaster.InsuranceID, oTransactionMaster.InsuranceName, oTransactionMaster.ResponsibilityNo, PayerMode.Insurance, oTransactionMaster.ContactID, oTransactionMaster.Lines[l].Total, oTransactionMaster.ClinicID, "B", "BILL", oTransactionMaster.TransactionDate);
                            }
                        }
                        if (oTransactionMaster.TransactionID > 0)
                        {

                            _sqlQuery = "update dbo.BL_Transaction_Claim_MST WITH(READPAST) set sMainClaimNo = '" + sMainClaimNo.ToString() + "' where nTransactionID =  " + oTransactionMaster.TransactionID;
                            oDB.Execute_Query(_sqlQuery);
                        }
                        #endregion " Update BL_EOB_NextAction with new Tracking ID "

                        #region " Add records to transaction tracking history tables (5060)"
                        //Add Sub-Claim to Tracking history Table
                        ogloBilling.SaveTransactionTrackHistory(oTransactionMaster.TransactionMasterID, oTransactionMaster.TransactionID, oTransactionMaster.TransactionUserID, oTransactionMaster.TransactionUserName);
                        #endregion " Add records to transaction tracking history tables"


                    }

                    //Unhold Parent Claim
                    //Date : 01-Feb-2011
                    _sqlQuery = "Update BL_Transaction_Claim_MST WITH (READPAST) set bIsHold=0 where nTransactionMasterID = " + _Transaction.TransactionMasterID + " and nTransactionID= " + _Transaction.TransactionID;
                    oDB.Execute_Query(_sqlQuery);

                    #region "Clearing Follow-up for master claim"

                    CL_FollowUpCode.ClearInsuranceClaimFollowUp(_Transaction.TransactionID);    

                    #endregion

                    _result = true;
                }
                else
                {
                    _result = false;
                }
            }
            catch //(Exception ex)
            {
                _result = false;
            }
            finally
            {
                if (ogloBilling != null)
                    ogloBilling.Dispose();
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
            }
            return _result;
        }

        //public bool SplitClaim()
        //{
        //    bool _result = false;

        //    gloBilling ogloBilling = new gloBilling(_databaseconnectionstring, _databaseconnectionstring);
        //    gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
        //    String _sqlQuery = "";
        //    String _DetailIDMasterList = "0";
        //    String _DetailIDList = "0";
        //    try
        //    {




        //        for (int i = 0; i < _Transaction.Lines.Count; i++)
        //        {
        //            _DetailIDMasterList += "," + _Transaction.Lines[i].TransactionMasterDetailID;
        //        }
        //        for (int i = 0; i < _Transaction.Lines.Count; i++)
        //        {
        //            _DetailIDList += "," + _Transaction.Lines[i].TransactionDetailID;
        //        }


        //        oDB.Connect(false);
        //        if (_Claims != null && _Claims.Count > 0)
        //        {                   

        //            for (int i = 0; i < _Claims.Count; i++)
        //            {
        //                string sMainClaimNo = string.Empty;

        //                Transaction oTransactionMaster = null;
        //                //Get Transaction information
        //                //Create Sub-Claim from Main Claim
        //                oTransactionMaster = (Transaction)_Transaction.Clone();

        //                oTransactionMaster.ClaimNo = oTransactionMaster.ClaimNo + 1;
        //                oTransactionMaster.TransactionMasterID = 0;
        //                oTransactionMaster.TransactionID = 0;


        //                //Set sub-claim status to open
        //                oTransactionMaster.ClaimStatus = ClaimStatus.Open;
        //                oTransactionMaster.Transaction_Status = TransactionStatus.Queue; ;


        //                //Remove Service line which are not part of sub-claim
        //                for (int j = oTransactionMaster.Lines.Count - 1; j >= 0; j--)
        //                {
        //                    oTransactionMaster.Lines[j].IsLineSplitted = false;

        //                    oTransactionMaster.Lines[j].TransactionId = 0;
        //                    oTransactionMaster.Lines[j].TransactionLineId = 0;
        //                    oTransactionMaster.Lines[j].TransactionMasterDetailID = 0;
        //                    oTransactionMaster.Lines[j].TransactionMasterID = 0;
        //                    oTransactionMaster.Lines[j].ParentTransactionID = 0;
        //                    oTransactionMaster.Lines[j].ParentTransactionDetailID = 0;
        //                    oTransactionMaster.Lines[j].TransactionDetailID = 0;

        //                    bool _found = false;
        //                    for (int k = 0; k < _Claims[i].CSClaimLines.Count; k++)
        //                    {
        //                        if (_Claims[i].CSClaimLines[k].CPT == oTransactionMaster.Lines[j].CPTCode)
        //                        {
        //                            _found = true;
        //                            break;
        //                        }
        //                    }
        //                    if (!_found)
        //                    {
        //                        oTransactionMaster.Lines.RemoveAt(j);
        //                    }                            
        //                }



        //                #region " Add records to transaction tracking tables"
        //                //Add Sub-Claim to Tracking Table
        //                ogloBilling.AddTransactionClaim(oTransactionMaster, _Transaction.ClinicID);
        //                #endregion " Add records to transaction tracking tables"

        //                #region " Update BL_EOB_NextAction with new Tracking ID "

        //                if (oTransactionMaster.Lines != null && oTransactionMaster.Lines.Count > 0)
        //                {
        //                    for (int l = 0; l < oTransactionMaster.Lines.Count; l++)
        //                    {
        //                        _sqlQuery = " Update BL_EOB_NextAction set " +
        //                                        " sSubClaimNo = '" + oTransactionMaster.SubClaimNo + "'," +
        //                                        "  nTrackMstTrnID= " + oTransactionMaster.TransactionID + "," +
        //                                        "  nTrackMstTrnDetailID=" + oTransactionMaster.Lines[l].TransactionDetailID +
        //                                        " where nBillingTransactionID = " + oTransactionMaster.TransactionMasterID +
        //                                        " and nBillingTransactionDetailID = " + oTransactionMaster.Lines[l].TransactionMasterDetailID + "";
        //                        oDB.Execute_Query(_sqlQuery);
        //                    }
        //                }
        //                if (oTransactionMaster.TransactionID > 0)
        //                {

        //                    _sqlQuery = "update dbo.BL_Transaction_Claim_MST set sMainClaimNo = '" + sMainClaimNo.ToString() + "' where nTransactionID =  " + oTransactionMaster.TransactionID;
        //                    oDB.Execute_Query(_sqlQuery);
        //                }
        //                #endregion " Update BL_EOB_NextAction with new Tracking ID "

        //                #region " Add records to transaction tracking history tables (5060)"
        //                //Add Sub-Claim to Tracking history Table
        //                ogloBilling.SaveTransactionTrackHistory(oTransactionMaster.TransactionMasterID, oTransactionMaster.TransactionID, oTransactionMaster.TransactionUserID, oTransactionMaster.TransactionUserName);
        //                #endregion " Add records to transaction tracking history tables"

        //                oTransactionMaster = null;
        //            }
        //            _result = true;
        //        }
        //        else
        //        {
        //            _result = false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _result = false;
        //    }
        //    finally
        //    {
        //        if (ogloBilling != null)
        //            ogloBilling.Dispose();
        //        if (oDB != null)
        //        {
        //            oDB.Disconnect();
        //            oDB.Dispose();
        //        }
        //    }
        //    return _result;
        //}


        private String GetSubClaimNo(Int64 TransactionMasterID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            String _sqlQuery = "Select isnull(MAX(Convert(Numeric(18,0),(case when ISNULL(nSubClaimNo,'')='' then '0' else nSubClaimNo end))),0)+1 from BL_Transaction_Claim_MST WITH(NOLOCK) where nTransactionMasterId=" + TransactionMasterID + "";
            Object _val = null;
            String _result = String.Empty;
            try
            {
                oDB.Connect(false);

                _val = oDB.ExecuteScalar_Query(_sqlQuery);

                if (_val != null && Convert.ToString(_val).Trim() != "")
                {
                    _result = Convert.ToString(_val);
                }
                oDB.Disconnect();
            }
            catch
            {
                _result = String.Empty;
            }
            finally
            {
                oDB.Dispose();
            }
            return _result;
        }

    }

    public class CSClaim
    {
        private CSClaimLines _Lines;

        #region "Constructor & Destructor"

        public CSClaim()
        {
            _Lines = new CSClaimLines();
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
                    _Lines.Dispose();
                }
            }
            disposed = true;
        }

        ~CSClaim()
        {
            Dispose(false);
        }

        #endregion

        #region Property Procedures of Transaction Class

        public Int64 TransactionMasterID
        {
            get;
            set;
        }

        public Int64 TransactionID
        {
            get;
            set;
        }

        public Int64 ClaimNo
        {
            get;
            set;
        }

        public String SubClaimNo
        {
            get;
            set;
        }

        public CSClaimLines CSClaimLines
        {
            get { return _Lines; }
            set { _Lines = value; }
        }

        #endregion
    }

    public class CSClaims
    {

        #region "Constructor & Destructor"

        public CSClaims()
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

                }
            }
            disposed = true;
        }


        ~CSClaims()
        {
            Dispose(false);
        }
        #endregion

        #region " Declarations "
        protected ArrayList _innerlist;
        #endregion " Declarations "

        #region "Public Methods"
        // Methods Add, Remove, Count , Item of TransactionLine
        public int Count
        {
            get { return _innerlist.Count; }
        }

        public void Add(CSClaim item)
        {
            _innerlist.Add(item);
        }

        public bool Remove(CSClaim item)
        //Remark - Work Remining for comparision
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

        public CSClaim this[int index]
        {
            get
            { return (CSClaim)_innerlist[index]; }
        }

        public bool Contains(CSClaim item)
        {
            return _innerlist.Contains(item);
        }

        public int IndexOf(CSClaim item)
        {
            return _innerlist.IndexOf(item);
        }

        public void CopyTo(CSClaim[] array, int index)
        {
            _innerlist.CopyTo(array, index);
        }

        #endregion "Public Methods"
    }

    public class CSClaimLine
    {
        #region "Constructor & Destructor"

        public CSClaimLine()
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

        ~CSClaimLine()
        {
            Dispose(false);
        }

        #endregion

        #region Property Procedures of Transaction Class

        public Int64 TransactionMasterDetailID
        {
            get;
            set;
        }

        public Int64 TransactionDetailID
        {
            get;
            set;
        }

        public String CPT
        {
            get;
            set;
        }



        #endregion
    }

    public class CSClaimLines
    {

        #region "Constructor & Destructor"

        public CSClaimLines()
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

                }
            }
            disposed = true;
        }


        ~CSClaimLines()
        {
            Dispose(false);
        }
        #endregion

        #region " Declarations "
        protected ArrayList _innerlist;
        #endregion " Declarations "

        #region "Public Methods"
        // Methods Add, Remove, Count , Item of TransactionLine
        public int Count
        {
            get { return _innerlist.Count; }
        }

        public void Add(CSClaimLine item)
        {
            _innerlist.Add(item);
        }

        public bool Remove(CSClaimLine item)
        //Remark - Work Remining for comparision
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

        public CSClaimLine this[int index]
        {
            get
            { return (CSClaimLine)_innerlist[index]; }
        }

        public bool Contains(CSClaimLine item)
        {
            return _innerlist.Contains(item);
        }

        public int IndexOf(CSClaimLine item)
        {
            return _innerlist.IndexOf(item);
        }

        public void CopyTo(CSClaimLine[] array, int index)
        {
            _innerlist.CopyTo(array, index);
        }
        #endregion "Public Methods"
    }

    public class SplitClaimDetails
    {
        #region "Constructor & Destructor"

        public SplitClaimDetails()
        {
            _Lines = new SplitClaimLines();
        }

        public SplitClaimDetails(Int64 claimNumber, string subClaimNumber)
        {
            SetClaimDetails(claimNumber, subClaimNumber);
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
                    _Lines.Dispose();
                }
            }
            disposed = true;
        }

        ~SplitClaimDetails()
        {
            Dispose(false);
        }

        #endregion

        #region " Declarations "
        private bool _bReturn = false;
        private Int64 _TransactionMasterID = 0;
        private Int64 _TransactionID = 0;

        private Int64 _ClaimNo = 0;
        private String _SubClaimNo = String.Empty;

        private Int64 _clinicID = 0;

        private SplitClaimLines _Lines;

        private Boolean _IsPaymentDone = true;

        private Boolean _IsPending = false;

        private Int64 _nEOBPaymentID = 0;

        private Int64 _nEOBID = 0;

        private bool _UseExtSqlConnection = false;
        private System.Data.SqlClient.SqlConnection _ExtSqlConnection = null;
        private System.Data.SqlClient.SqlTransaction _ExtSqlTransaction = null;
        private bool _ExtTransactionErrorValue = false;
        private string _ExtTransactionErrorMsg = String.Empty;

        //Added by Subashish_b on 06/Jan /2011 (integration made on date-10/May/2011) for declaring variable for PAF Values
        private Int64 _nPAccountID = 0;
        private Int64 _nGuarantorID = 0;
        private Int64 _nAccountPatientID = 0;
        private Int64 _nPatientID = 0;
        //End

        #endregion " Declarations "

        #region Property Procedures of Transaction Class

        public Int64 TransactionMasterID
        {
            get { return _TransactionMasterID; }
            set { _TransactionMasterID = value; }
        }

        public Int64 TransactionID
        {
            get { return _TransactionID; }
            set { _TransactionID = value; }
        }

        public Int64 ClaimNo
        {
            get { return _ClaimNo; }
            set { _ClaimNo = value; }
        }

        public String SubClaimNo
        {
            get { return _SubClaimNo; }
            set { _SubClaimNo = value; }
        }

        public SplitClaimLines Lines
        {
            get { return _Lines; }
            set { _Lines = value; }
        }

        public Int64 ClinicID
        {
            get { return _clinicID; }
            set { _clinicID = value; }
        }

        public Boolean IsPaymentDone
        {
            get { return _IsPaymentDone; }
            set { _IsPaymentDone = value; }
        }

        public Boolean IsPending
        {
            get { return _IsPending; }
            set { _IsPending = value; }
        }

        public Int64 EOBPaymentID
        { get { return _nEOBPaymentID; } set { _nEOBPaymentID = value; } }
        public Int64 EOBID
        { get { return _nEOBID; } set { _nEOBID = value; } }

        public string ClaimDisplayNo
        {
            get
            {
                string _claim = InsurancePayment.GetFormattedClaimPaymentNumber(Convert.ToString(ClaimNo));
                if (!String.IsNullOrEmpty(SubClaimNo))
                {
                    _claim = string.Concat(_claim, "-", SubClaimNo);
                }
                return _claim;
            }
        }

        #region " Newly added Properties"

        bool _IsClaimVoided = true;
        public bool IsClaimVoided
        {
            get { return _IsClaimVoided; }
            set { _IsClaimVoided = value; }
        }

        bool _IsClaimBatched = false;
        public bool IsClaimBatched
        {
            get { return _IsClaimBatched; }
            set { _IsClaimBatched = value; }
        }

        bool _IsClaimExist = false;
        public bool IsClaimExist
        {
            get { return _IsClaimExist; }
            set { _IsClaimExist = value; }
        }

        bool _HasChildClaims = false;
        public bool HasChildClaims
        {
            get { return _HasChildClaims; }
            set { _HasChildClaims = value; }
        }

        int _CurrentResponsibleParty = 0;
        public int CurrentResponsibleParty
        {
            get { return _CurrentResponsibleParty; }
            set { _CurrentResponsibleParty = value; }
        }

        bool _ValidateBatch = false;
        public bool ValidateBatch
        {
            get { return _ValidateBatch; }
            set { _ValidateBatch = value; }
        }

        string _claimRemittanceRef = string.Empty;
        public string ClaimRemittanceReferenceNo
        {
            get { return _claimRemittanceRef; }
            set { _claimRemittanceRef = value; }
        }

        bool _IsClaimOnHold = false;
        public bool IsClaimOnHold
        {
            get { return _IsClaimOnHold; }
            set { _IsClaimOnHold = value; }
        }

        bool _IsClaimMarkedReplacement = false;
        public bool IsClaimMarkedReplacement
        {
            get { return _IsClaimMarkedReplacement; }
            set { _IsClaimMarkedReplacement = value; }
        }

        int _TransactionStatus = 0;
        public int ClaimStatus
        {
            get { return _TransactionStatus; }
            set { _TransactionStatus = value; }
        }

        Int64 _TransactionDate = 0;
        public Int64 BillingTransactionDate
        {
            get { return _TransactionDate; }
            set { _TransactionDate = value; }
        }

        #endregion

        public bool UseExtSqlConnection
        { get { return _UseExtSqlConnection; } set { _UseExtSqlConnection = value; } }
        public System.Data.SqlClient.SqlConnection ExtSqlConnection
        { get { return _ExtSqlConnection; } set { _ExtSqlConnection = value; } }
        public System.Data.SqlClient.SqlTransaction ExtSqlTransaction
        { get { return _ExtSqlTransaction; } set { _ExtSqlTransaction = value; } }
        public bool ExtTransactionErrorValue
        { get { return _ExtTransactionErrorValue; } set { _ExtTransactionErrorValue = value; } }
        public string ExtTransactionErrorMsg
        { get { return _ExtTransactionErrorMsg; } set { _ExtTransactionErrorMsg = value; } }

        //Added by Subashish_b on 06/Jan /2011 (integration made on date-10/May/2011) for  creating properties to store PAF Values
        public Int64 PAccountID
        {
            get { return _nPAccountID; }
            set { _nPAccountID = value; }
        }

        public Int64 GuarantorID
        {
            get { return _nGuarantorID; }
            set { _nGuarantorID = value; }
        }

        public Int64 AccountPatientID
        {
            get { return _nAccountPatientID; }
            set { _nAccountPatientID = value; }
        }

        public Int64 PatientID
        {
            get { return _nPatientID; }
            set { _nPatientID = value; }
        }
        // End

        #endregion Property Procedures of Transaction Class

        #region " Methods and Procedures "

        private void SetClaimDetails(Int64 claimNumber, string subClaimNumber)
        {
           
                _ClaimNo = claimNumber;
                _SubClaimNo = subClaimNumber;

                try
                {
                    if (claimNumber != 0)
                    {
                        DataRow _claimDetails = InsurancePayment.GetClaimDetails(claimNumber, subClaimNumber);

                        if (_claimDetails != null)
                        {
                            _IsClaimExist = true;
                            _IsClaimVoided = Convert.ToBoolean(_claimDetails["bIsVoid"]);

                            _TransactionStatus = Convert.ToInt32(_claimDetails["nStatus"]);
                            _CurrentResponsibleParty = Convert.ToInt32(_claimDetails["CurrentPartyNo"]);
                            string _internal_subClaimNo = Convert.ToString(_claimDetails["nSubClaimNo"]);

                            if (_CurrentResponsibleParty.Equals(InsuranceTypeFlag.Primary.GetHashCode()) && (string.IsNullOrEmpty(_internal_subClaimNo)))
                            {
                                _ValidateBatch = true;
                                if (_TransactionStatus.Equals(TransactionStatus.Batch.GetHashCode())
                                    || _TransactionStatus.Equals(TransactionStatus.InsurancePaid.GetHashCode())
                                    || _TransactionStatus.Equals(TransactionStatus.SendToClaimManager.GetHashCode())
                                    || _TransactionStatus.Equals(TransactionStatus.SendToClearingHouse.GetHashCode())
                                    || _TransactionStatus.Equals(TransactionStatus.Resent.GetHashCode())) // checked for Resent & Rebill functionality 5040
                                {
                                    _IsClaimBatched = true;
                                }
                            }
                            else
                            {
                                _ValidateBatch = false;
                            }
                            _TransactionID = Convert.ToInt64(_claimDetails["nTransactionID"]);
                            _TransactionMasterID = Convert.ToInt64(_claimDetails["nTransactionMasterID"]);
                            _HasChildClaims = InsurancePayment.IsClaimHasChilds(_TransactionID);

                            _IsClaimOnHold = Convert.ToBoolean(_claimDetails["bIsHold"]);
                            _IsClaimMarkedReplacement = Convert.ToBoolean(_claimDetails["bIsReplacementClaim"]);

                            if (_claimDetails["nTransactionDate"] != null) { _TransactionDate = Convert.ToInt64(_claimDetails["nTransactionDate"]); }

                            //_InsuranceCompanyID = Convert.ToInt64(_claimDetails["InsuranceID"]);

                            //...Code written on 20100510 by Sagar Ghodke 
                            //....Code written to skip the restriction for insurance payment which needed the 
                            //....claim to be batched.
                            _ValidateBatch = false;
                            //....End code on 20100510 by Sagar Ghodke


                            //Added by Subashish_b on 25/Jan /2011 (integration made on date-10/May/2011) for  getting the Paf Values from _claimDetails Object
                            _nPAccountID = Convert.ToInt64(_claimDetails["nPAccountID"]);
                            _nGuarantorID = Convert.ToInt64(_claimDetails["nGuarantorID"]);
                            _nAccountPatientID = Convert.ToInt64(_claimDetails["nAccountPatientID"]);
                            _nPatientID = Convert.ToInt64(_claimDetails["nPatientID"]);
                            //End
                        }

                    }
                    else
                    {
                        _IsClaimExist = true;
                    }
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                }
            
        }


        public DataTable GetSubclaims()
        {
         //   DataTable _dtTmp = null;
            DataTable _NextAction = null;
            _NextAction = new DataTable();
            _NextAction.Columns.Add(new DataColumn("PartyNo", System.Type.GetType("System.Int32")));
            _NextAction.Columns.Add(new DataColumn("NextAction", System.Type.GetType("System.String")));
            _NextAction.Columns.Add(new DataColumn("Party", System.Type.GetType("System.String")));
            DataRow dr = null;
            if (this.Lines != null && this.Lines.Count > 0)
            {
                for (int i = 0; i < Lines.Count; i++)
                {
                    DataRow[] drArray = _NextAction.Select("PartyNo = " + this.Lines[i].ResponsibilityNo + " and NextAction = '" + this.Lines[i].NextActionCode + "'");
                    if (drArray.GetUpperBound(0) < 0)
                    {
                        dr = _NextAction.NewRow();
                        dr["PartyNo"] = this.Lines[i].ResponsibilityNo;
                        dr["NextAction"] = this.Lines[i].NextActionCode;
                        dr["Party"] = this.Lines[i].Party;
                        _NextAction.Rows.Add(dr);
                    }
                }
            }
            return _NextAction;
        }


        public bool GenerateNewClaimOnRespTransfer(string _databaseconnectionstring, string _emrdatabaseconnectionstring, Transaction oTransaction, Transaction oInitialTransaction, int _TempParty, string _InsTransferCloseDate, string _NextActionCode, string _NextActionDescription, Int64 _ContactID,out Int64 nNewTransID)
        {
            string _sqlQuery = "";
            bool _result = false;
            gloDatabaseLayer.DBLayer ODB = null;
            Int64 nTransactionID = 0;
            gloBilling ogloBilling = new gloBilling(_databaseconnectionstring, "");
            ClsBL_Hold oClsBL_Hold = new ClsBL_Hold(_databaseconnectionstring, _emrdatabaseconnectionstring);
            Transaction _Transaction = null;
            string sql_query = String.Empty;
            bool bIsParentOnHold = false;
            try
            {

               // ogloBilling = new gloBilling(_databaseconnectionstring, "");
                _Transaction = ogloBilling.GetChargesClaimDetails(Convert.ToInt64(oTransaction.TransactionID), oTransaction.ClinicID);


                _Transaction.Hold = oTransaction.Hold;
               

                string sMainClaimNo = string.Empty;
                if (_Transaction.SubClaimNo.Contains("-") == false)
                { sMainClaimNo = _Transaction.SubClaimNo; }
                else
                { sMainClaimNo = _Transaction.MainClaimNo; }


                if (_Transaction.SubClaimNo.Trim() != String.Empty)
                { _Transaction.ParentClaimNo = _Transaction.ClaimNo.ToString() + "-" + _Transaction.SubClaimNo; }
                else
                { _Transaction.ParentClaimNo = _Transaction.ClaimNo.ToString(); }


                //New Sub-Claim No
                _Transaction.SubClaimNo = ogloBilling.GetSubClaimNo(oTransaction.TransactionMasterID);
                _Transaction.ParentTransactionID = oTransaction.TransactionID;


                if (_Transaction.ResponsibilityType == PayerMode.Insurance && oInitialTransaction.ResponsibilityType == PayerMode.Insurance)
                {
                    string _Strquery = "Select nBatchID from Bl_Transaction_Batch_DTL WITH(NOLOCK)  where nTransactionMasterID=" + oInitialTransaction.TransactionMasterID + "  and nClinicID=" + oInitialTransaction.ClinicID + "";
                    ODB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                    ODB.Connect(false);
                    DataTable _dtBatch = null;
                    ODB.Retrive_Query(_Strquery, out _dtBatch);
                    ODB.Disconnect();
                    if (_dtBatch != null && _dtBatch.Rows.Count > 0)
                    {
                        _Transaction.Transaction_Status = TransactionStatus.Queue;
                    }
                    else
                    {
                        _Transaction.Transaction_Status = TransactionStatus.Queue;
                    }

                    if (_dtBatch != null)
                    {
                        _dtBatch.Dispose();
                        _dtBatch = null;
                    }
                }
                else
                {
                    _Transaction.Transaction_Status = TransactionStatus.Queue;
                }





                #region "BATCH"

                if (_Transaction.ResponsibilityType == PayerMode.Self && (oInitialTransaction.Transaction_Status == TransactionStatus.None || oInitialTransaction.Transaction_Status == TransactionStatus.Batch || oInitialTransaction.Transaction_Status == TransactionStatus.Queue))
                {
                    DeleteBatch(oInitialTransaction.TransactionMasterID, oInitialTransaction.TransactionID, oInitialTransaction.ClinicID);

                    //ODB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                    //ODB.Connect(false);
                    //Int64 _BatchID = 0;
                    //DataTable _dtBatch = new DataTable();
                    //string _strquery ="";
                    //_strquery = "Select nBatchID from Bl_Transaction_Batch_DTL  where nTransactionMasterID=" + oTransaction.TransactionMasterID + "   and nClinicID=" + oTransaction.ClinicID + "";
                    //ODB.Retrive_Query(_strquery, out _dtBatch);
                    //if (_dtBatch != null && _dtBatch.Rows.Count > 0)
                    //{
                    //    _BatchID = Convert.ToInt64(_dtBatch.Rows[0]["nBatchID"]);
                    //}



                    // _strquery = " Delete BL_Transaction_Batch_DTL where nTransactionID IN " +
                    //                 " ( SELECT     BL_Transaction_Claim_MST.nTransactionID " +
                    //                 " FROM         BL_Transaction_Claim_MST INNER JOIN " +
                    //                 " BL_Transaction_Batch_DTL ON " +
                    //                 " BL_Transaction_Claim_MST.nTransactionMasterID = BL_Transaction_Batch_DTL.nTransactionMasterID " +
                    //                 " where  ISNULL(BL_Transaction_Claim_MST.nStatus,0)=3) and BL_Transaction_Batch_DTL.nTransactionMasterID=" + _Transaction.TransactionMasterID + "  ";
                    //ODB.Execute_Query(_strquery);



                    //if (_BatchID > 0)
                    //{

                    //    _strquery = "Select Count(nBatchID) from Bl_Transaction_Batch_DTL where nBatchID=" + _BatchID + "";
                    //    Object _objCount = ODB.ExecuteScalar_Query(_strquery);

                    //    if (_objCount != null && Convert.ToInt64(_objCount) == 0)
                    //    {
                    //        _strquery = "Delete Bl_Transaction_Batch where nBatchID=" + _BatchID + "";
                    //        ODB.Execute_Query(_strquery);
                    //    }
                    //}

                #endregion

                }
                else if(_Transaction.ResponsibilityType == PayerMode.Self && (oInitialTransaction.Transaction_Status == TransactionStatus.SendToClaimManager || oInitialTransaction.Transaction_Status == TransactionStatus.SendToClearingHouse))
                {
                    ODB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                    ODB.Connect(false);
                    _sqlQuery = "Update BL_Transaction_Claim_MST WITH(READPAST) set nClaimStatus = 2,nStatus = " + TransactionStatus.InsurancePaid.GetHashCode() + " where nTransactionMasterID = " + oInitialTransaction.TransactionMasterID + " and nTransactionID= " + oInitialTransaction.TransactionID;
                    ODB.Execute_Query(_sqlQuery);
                }
                #region " UnHold Parent Claim "
                if (oTransaction.Hold != null)
                {
                    if (oTransaction.Hold.IsHold == true)
                    {
                        bIsParentOnHold = true;
                        _Transaction.Hold.HoldModified = true;
                        //_Transaction.Hold.HoldModDateTime = DateTime.Now;
                        _Transaction.Hold.IsHold = false;
                        oClsBL_Hold.HoldUnholdClaim(_Transaction.Hold, _Transaction.TransactionMasterID, _Transaction.TransactionID);
                    }
                }
                #endregion

                _Transaction.TransactionID = 0;

                for (int j = 0; j < _Transaction.Lines.Count; j++)
                {
                    _Transaction.Lines[j].ParentTransactionID = _Transaction.ParentTransactionID;
                    _Transaction.Lines[j].ParentTransactionDetailID = _Transaction.Lines[j].TransactionDetailID;
                    _Transaction.Lines[j].TransactionDetailID = 0;
                    _Transaction.Lines[j].AnesthesiaID = 0;
                    _Transaction.Lines[j].LineNotes = null;
                    _Transaction.Lines[j].InsuranceID = oTransaction.Lines[j].InsuranceID;
                    _Transaction.Lines[j].InsuranceSelfMode = oTransaction.Lines[j].InsuranceSelfMode;
                }


                _Transaction.ClaimStatus = (ClaimStatus)1;

                nTransactionID = ogloBilling.AddTransactionClaim(_Transaction, _clinicID);

                #region " Feeding Hold Information for Child Claim "

                //if (oTransaction.Hold != null)
                //{
                if (bIsParentOnHold == true)
                {
                    _Transaction.Hold.HoldModified = true;
                    _Transaction.Hold.HoldModDateTime = DateTime.Now;
                    _Transaction.Hold.IsHold = true;
                    oClsBL_Hold.HoldUnholdClaim(_Transaction.Hold, _Transaction.TransactionMasterID, _Transaction.TransactionID);

                }
                //}

                #endregion

                #region " Closing Previous Transaction "

                if (nTransactionID > 0)
                {
                    ODB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                    ODB.Connect(false);

                    if (oTransaction.IsResend)
                    {
                        _sqlQuery = "Update BL_Transaction_Claim_MST WITH(READPAST) set nClaimStatus = 2,nStatus = " + TransactionStatus.Resent.GetHashCode() + " where nTransactionMasterID = " + oTransaction.TransactionMasterID + " and nTransactionID= " + oTransaction.TransactionID;
                    }
                    else
                    {
                        _sqlQuery = "Update BL_Transaction_Claim_MST WITH(READPAST) set nClaimStatus = 2  where nTransactionMasterID = " + oTransaction.TransactionMasterID + " and nTransactionID= " + oTransaction.TransactionID;
                    }

                    ODB.Execute_Query(_sqlQuery);

                    #region "Revert back Initial Transactions Responsiblity details"

                    _sqlQuery = "Update BL_Transaction_Claim_MST WITH(READPAST) SET nInsuranceID = " + oInitialTransaction.InsuranceID + ", nContactID= " + oInitialTransaction.ContactID + ", nResponsibilityNo= " + oInitialTransaction.ResponsibilityNo + " ,nResponsibilityType= '" + oInitialTransaction.ResponsibilityType.GetHashCode() + "' WHERE nTransactionMasterID = " + oInitialTransaction.TransactionMasterID + " and nTransactionID = " + oInitialTransaction.TransactionID;
                    ODB.Execute_Query(_sqlQuery);

                    #endregion

                }

                #endregion " Closing Previous Transaction "


                if (_Transaction != null && _Transaction.Lines != null)
                {
                    for (int i = 0; i < _Transaction.Lines.Count; i++)
                    {
                        if (_Transaction.Lines[i].InsuranceSelfMode == PayerMode.Self)
                        {
                            //ogloBilling.UpdateNextParty(_Transaction.ClaimNo, _Transaction.SubClaimNo, _Transaction.TransactionMasterID, _Transaction.Lines[i].TransactionMasterDetailID, _Transaction.TransactionID, _Transaction.Lines[i].TransactionDetailID, _Transaction.PatientID, PayerMode.Self.ToString().ToUpper(), _TempParty, PayerMode.Self, 0, _Transaction.Lines[i].Total, _Transaction.ClinicID, _NextActionCode, _NextActionDescription, gloDateMaster.gloDate.DateAsNumber(_InsTransferCloseDate));
                            ogloBilling.UpdateNextParty(_Transaction.ClaimNo, _Transaction.SubClaimNo, _Transaction.TransactionMasterID, _Transaction.Lines[i].TransactionMasterDetailID, _Transaction.TransactionID, _Transaction.Lines[i].TransactionDetailID, _Transaction.PatientID, PayerMode.Self.ToString(), _TempParty, PayerMode.Self, 0, _Transaction.Lines[i].Total, _Transaction.ClinicID, _NextActionCode, _NextActionDescription, gloDateMaster.gloDate.DateAsNumber(_InsTransferCloseDate));
                        }
                        else if (_Transaction.Lines[i].InsuranceSelfMode == PayerMode.BadDebt)
                        {
                            // ogloBilling.UpdateNextParty(MasterTransaction.ClaimNo, MasterTransaction.SubClaimNo, MasterTransaction.TransactionMasterID, MasterTransaction.Lines[i].TransactionMasterDetailID, MasterTransaction.TransactionID, MasterTransaction.Lines[i].TransactionDetailID, MasterTransaction.PatientID, oPatientControl.PatientName, _TempParty, PayerMode.Self, 0, MasterTransaction.Lines[i].Total, MasterTransaction.ClinicID, _NextActionCode, _NextActionDescription, gloDateMaster.gloDate.DateAsNumber(mskClaimDate.Text));
                            string collectionAgency = "";
                            collectionAgency = ogloBilling.GetCollectionAgencyname(_ContactID);
                            ogloBilling.UpdateNextParty(_Transaction.ClaimNo, _Transaction.SubClaimNo, _Transaction.TransactionMasterID, _Transaction.Lines[i].TransactionMasterDetailID, _Transaction.TransactionID, _Transaction.Lines[i].TransactionDetailID, _Transaction.Lines[i].InsuranceID, collectionAgency, _TempParty, PayerMode.BadDebt, _ContactID, _Transaction.Lines[i].Charges, _Transaction.Lines[i].ClinicID, _NextActionCode, _NextActionDescription, gloDateMaster.gloDate.DateAsNumber(_InsTransferCloseDate));
                        }
                        else
                        {
                            //ogloBilling.UpdateNextParty(_Transaction.ClaimNo, _Transaction.SubClaimNo, _Transaction.TransactionMasterID, _Transaction.Lines[i].TransactionMasterDetailID, _Transaction.TransactionID, _Transaction.Lines[i].TransactionDetailID, _Transaction.Lines[i].InsuranceID, _Transaction.Lines[i].InsuranceName.ToUpper(), _TempParty, PayerMode.Insurance, _ContactID, _Transaction.Lines[i].Charges, _Transaction.Lines[i].ClinicID, _NextActionCode, _NextActionDescription, gloDateMaster.gloDate.DateAsNumber(_InsTransferCloseDate));
                            ogloBilling.UpdateNextParty(_Transaction.ClaimNo, _Transaction.SubClaimNo, _Transaction.TransactionMasterID, _Transaction.Lines[i].TransactionMasterDetailID, _Transaction.TransactionID, _Transaction.Lines[i].TransactionDetailID, _Transaction.Lines[i].InsuranceID, _Transaction.Lines[i].InsuranceName, _TempParty, PayerMode.Insurance, _ContactID, _Transaction.Lines[i].Charges, _Transaction.Lines[i].ClinicID, _NextActionCode, _NextActionDescription, gloDateMaster.gloDate.DateAsNumber(_InsTransferCloseDate));
                        }
                    }
                }

                #region " Update BL_EOB_NextAction with new Tracking ID "

                if (_Transaction.Lines != null && _Transaction.Lines.Count > 0)
                {
                    for (int l = 0; l < _Transaction.Lines.Count; l++)
                    {
                        _sqlQuery = " Update BL_EOB_NextAction WITH(READPAST) set " +
                                                               " sSubClaimNo = '" + _Transaction.SubClaimNo + "'," +
                                                               "  nTrackMstTrnID= " + _Transaction.TransactionID + "," +
                                                               "  nTrackMstTrnDetailID=" + _Transaction.Lines[l].TransactionDetailID +
                                                               " where nBillingTransactionID = " + _Transaction.TransactionMasterID +
                                                               " and nBillingTransactionDetailID = " + _Transaction.Lines[l].TransactionMasterDetailID + "";
                        ODB.Execute_Query(_sqlQuery);
                    }
                }
                #endregion " Update BL_EOB_NextAction with new Tracking ID "

                //Main Claim
                if (_Transaction.TransactionID > 0)
                {

                    if (_Transaction.SubClaimNo.Contains("-") == true)
                    {
                        sql_query = "update dbo.BL_Transaction_Claim_MST WITH(READPAST) set sMainClaimNo = '" + sMainClaimNo.ToString() + "' ,nEOBPaymentID = 0 ,nEOBID = 0 where nTransactionID =  " + _Transaction.TransactionID;
                    }
                    else
                    {
                        sql_query = "update dbo.BL_Transaction_Claim_MST WITH(READPAST) set sMainClaimNo = '" + _Transaction.SubClaimNo + "' ,nEOBPaymentID = 0 ,nEOBID = 0 where nTransactionID =  " + _Transaction.TransactionID;
                    }

                    #region "Carry Forward Claim Insurance Follow Up to Child Claim"

                    CL_FollowUpCode.ClearInsuranceClaimFollowUp(_Transaction.TransactionID, oTransaction.TransactionID); 

                    #endregion

                    ODB.Execute_Query(sql_query);

                    ogloBilling.SaveTransactionTrackHistory(_Transaction.TransactionMasterID, _Transaction.TransactionID, _Transaction.TransactionUserID, _Transaction.TransactionUserName);

                }
                               
                _result = true;
                
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
            finally
            {
                if (ogloBilling != null) { ogloBilling.Dispose(); }
                if (oClsBL_Hold != null) { oClsBL_Hold.Dispose(); }
                if (ODB != null) { ODB.Disconnect(); ODB.Dispose(); }
                if (_Transaction != null) { _Transaction.Dispose(); }

            }

            nNewTransID = _Transaction.TransactionID;
            return _result;
        }


        public bool GenerateNewClaimOnRespTransfer(string _databaseconnectionstring, Int64 EOBPaymentID, string _InsTransferCloseDate, bool _RevertInvidually)
        {
           
            bool _result = false;
            bool _bIsHoldPreviousClaim = false;
            bool _bIsPaymentmade = false;
            gloDatabaseLayer.DBLayer ODB = null;
            gloBilling ogloBilling = new gloBilling(_databaseconnectionstring, "");
            ClsBL_Hold oClsBL_Hold = new ClsBL_Hold(_databaseconnectionstring, "");
            Transaction _Transaction = null;
            Transaction _NewTransaction = null;

            DataTable _dt = null;
            DataTable _dtVoidedTransaction = null;

            string _sqlQuery = "";
            string sql_query = String.Empty;
            string sNextActionCode = "";
            string sNextActionDesc = "";
            string sClaim = "";

            Int64 _nLatestTransactionID = 0;
            Int64 _nOlderTransactionID = 0;
            Int64 nTransactionID = 0;
            Int64 nPreInsuranceID = 0;
            Int64 nPreContactID = 0;
            Int16 nPreResponsiblityNo = 0;
            Int16 nResponsibilityType = 0;
            Int64 nBackwardVoidedTransactionID = 0;


            try
            {

                _bReturn = false;

                //Collecting Transaction ID of Voided Payment
                ODB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                ODB.Connect(false);

                _sqlQuery = "SELECT DISTINCT nTrackTransactionID from EOB WHERE isnull(nTrackTransactionID,0)<> 0 and nCreditID = " + EOBPaymentID + "";

                ODB.Retrive_Query(_sqlQuery, out _dtVoidedTransaction);

                if (_dtVoidedTransaction != null && _dtVoidedTransaction.Rows.Count > 0)
                {
                    for (int iCount = 0; iCount <= _dtVoidedTransaction.Rows.Count - 1; iCount++)
                    {
                        _bIsHoldPreviousClaim = false;
                        _nOlderTransactionID = Convert.ToInt64(_dtVoidedTransaction.Rows[iCount][0]);

                        //Check for Pament Exist in Forward.
                        _bIsPaymentmade = IsForwardPaymentMade(_nOlderTransactionID, _databaseconnectionstring);
                        if (_RevertInvidually) { _bIsPaymentmade = false; }

                        if (!_bIsPaymentmade)
                        {
                            //Get the all the previous claims before voiding this payment
                            GetBackwardVoidedClaim(_nOlderTransactionID, _databaseconnectionstring, ref nBackwardVoidedTransactionID);

                            //Collecting Necessary Information's of Transaction against Voided Payment
                            _sqlQuery = "SELECT nTransactionMasterID,nTransactionID,nClaimNo,nSubClaimNo,nInsuranceID,nContactID,nResponsibilityNo,nResponsibilityType,nClinicID FROM BL_Transaction_Claim_MST WITH(NOLOCK) WHERE nTransactionID = " + (nBackwardVoidedTransactionID != 0 ? nBackwardVoidedTransactionID : _nOlderTransactionID);
                            ODB.Retrive_Query(_sqlQuery, out _dt);

                            nPreInsuranceID = Convert.ToInt64(_dt.Rows[0]["nInsuranceID"]);
                            nPreContactID = Convert.ToInt64(_dt.Rows[0]["nContactID"]);
                            nPreResponsiblityNo = Convert.ToInt16(_dt.Rows[0]["nResponsibilityNo"]);
                            nResponsibilityType = Convert.ToInt16(_dt.Rows[0]["nResponsibilityType"]);
                            _clinicID = Convert.ToInt16(_dt.Rows[0]["nClinicID"]);

                            //Collecting Latest Claims Against Transaction ID

                            sClaim = string.Empty;

                            if (_RevertInvidually)
                            {
                                //To get the current claim which is to be voided - Get Latest Voided Claims 
                                GetLatestVoidedClaims(_nOlderTransactionID, _databaseconnectionstring, ref sClaim);
                            }
                            else
                            {
                                //To get the current claim which is to be voided - Get Latest claims
                                GetLatestClaims(_nOlderTransactionID, _databaseconnectionstring, ref sClaim);
                            }

                            if (sClaim != "" && sClaim != null)
                            {
                                String[] sClaims = sClaim.Split(new Char[] { ',' });
                                foreach (string sTransactionID in sClaims)
                                {
                                    _nLatestTransactionID = Convert.ToInt64(sTransactionID);

                                    //Collecting NextActionCode and Desc against Latest Claim
                                    _sqlQuery = "SELECT sNextActionCode,sNextActionDescription from BL_EOB_NextAction WITH(NOLOCK) WHERE nTrackMstTrnID = " + _nLatestTransactionID;
                                    ODB.Retrive_Query(_sqlQuery, out _dt);
                                    if (_dt != null && _dt.Rows.Count > 0)
                                    {
                                        sNextActionCode = Convert.ToString(_dt.Rows[0]["sNextActionCode"]);
                                        sNextActionDesc = Convert.ToString(_dt.Rows[0]["sNextActionDescription"]);
                                    }
                                    if (ogloBilling != null)
                                    {
                                        ogloBilling = new gloBilling(_databaseconnectionstring, "");
                                    }
                                    _Transaction = ogloBilling.GetChargesClaimDetails(_nLatestTransactionID, AppSettings.ClinicID);

                                    #region " UnHold Parent Claim "

                                    if (_Transaction.Hold != null)
                                    {
                                        if (_Transaction.Hold.IsHold == true)
                                        {
                                            _bIsHoldPreviousClaim = true;
                                            _Transaction.Hold.HoldModified = true;
                                            //_Transaction.Hold.HoldModDateTime = DateTime.Now;
                                            _Transaction.Hold.IsHold = false;
                                            oClsBL_Hold.HoldUnholdClaim(_Transaction.Hold, _Transaction.TransactionMasterID, _Transaction.TransactionID);
                                        }

                                    }

                                    #endregion

                                    string sMainClaimNo = string.Empty;
                                    if (_Transaction.SubClaimNo.Contains("-") == false)
                                    { sMainClaimNo = _Transaction.SubClaimNo; }
                                    else
                                    { sMainClaimNo = _Transaction.MainClaimNo; }

                                    if (_Transaction.SubClaimNo.Trim() != String.Empty)
                                    { _Transaction.ParentClaimNo = _Transaction.ClaimNo.ToString() + "-" + _Transaction.SubClaimNo; }
                                    else
                                    { _Transaction.ParentClaimNo = _Transaction.ClaimNo.ToString(); }


                                    _Transaction.SubClaimNo = ogloBilling.GetSubClaimNo(_Transaction.TransactionMasterID);

                                    _Transaction.ParentTransactionID = _Transaction.TransactionID;

                                    _Transaction.InsuranceID = nPreInsuranceID;
                                    _Transaction.ContactID = nPreContactID;
                                    _Transaction.ResponsibilityNo = nPreResponsiblityNo;
                                    _Transaction.ResponsibilityType = (PayerMode)nResponsibilityType;

                                    _Transaction.ClaimStatus = (ClaimStatus)1;

                                    #region "BATCH"

                                    //Checking Old Claim was Batched or Not
                                    _sqlQuery = " SELECT nBatchID FROM BL_Transaction_Batch_DTL WITH(NOLOCK) where nTransactionID = " + _nOlderTransactionID;
                                    ODB.Retrive_Query(_sqlQuery, out _dt);

                                    if (_dt.Rows.Count > 0)
                                    {
                                        _Transaction.Transaction_Status = TransactionStatus.None;
                                    }
                                    else
                                    {
                                        _Transaction.Transaction_Status = TransactionStatus.Queue;
                                    }

                                    #endregion

                                    _Transaction.TransactionID = 0;

                                    for (int j = 0; j < _Transaction.Lines.Count; j++)
                                    {
                                        _Transaction.Lines[j].ParentTransactionID = _Transaction.ParentTransactionID;
                                        _Transaction.Lines[j].ParentTransactionDetailID = _Transaction.Lines[j].TransactionDetailID;
                                        _Transaction.Lines[j].TransactionDetailID = 0;
                                        _Transaction.Lines[j].LineNotes = null;
                                        _Transaction.Lines[j].InsuranceID = _Transaction.InsuranceID;
                                        _Transaction.Lines[j].InsuranceSelfMode = _Transaction.Lines[j].InsuranceSelfMode;
                                    }

                                    //Generating New Split Claim.
                                    nTransactionID = ogloBilling.AddTransactionClaim(_Transaction, _clinicID);

                                    //Collecting Newly generated Claim into Object for Further Processing
                                    _NewTransaction = ogloBilling.GetChargesClaimDetails(nTransactionID, _clinicID);

                                    if (_Transaction.IsVoid && !_NewTransaction.IsVoid)
                                    {
                                        sql_query = "update BL_Transaction_Claim_MST WITH (READPAST) set bIsVoid = '" + _Transaction.IsVoid + "' ,dtVoidDate = '" + _Transaction.VoidedDate + "' ,nVoidUserID = " + _Transaction.VoidByID + " ,nVoidCloseDate = " + _Transaction.nVoidedDate + " , nVoidTrayID = " + _Transaction.VoidedTrayID + ",sVoidUserName = '" + _Transaction.VoidByName + "' where nTransactionID =  " + _Transaction.TransactionID;
                                        ODB.Execute_Query(sql_query);
                                    }

                                    #region " Feeding Hold Information for Child Claim "

                                    if (_bIsHoldPreviousClaim)
                                    {
                                        _NewTransaction.Hold = _Transaction.Hold;
                                        _NewTransaction.Hold.HoldModified = true;
                                        _NewTransaction.Hold.HoldModDateTime = DateTime.Now;
                                        _NewTransaction.Hold.IsHold = true;
                                        oClsBL_Hold.HoldUnholdClaim(_NewTransaction.Hold, _NewTransaction.TransactionMasterID, _NewTransaction.TransactionID);

                                    }

                                    #endregion

                                    #region " Closing Previous Transaction "

                                    if (nTransactionID > 0)
                                    {
                                        _sqlQuery = "Update BL_Transaction_Claim_MST WITH(READPAST) set nClaimStatus = 2,nStatus = " + TransactionStatus.None.GetHashCode() + "  where nTransactionMasterID = " + _Transaction.TransactionMasterID + " and nTransactionID= " + _nLatestTransactionID;
                                        ODB.Execute_Query(_sqlQuery);
                                    }

                                    #endregion " Closing Previous Transaction "


                                    if (_NewTransaction != null && _NewTransaction.Lines != null)
                                    {
                                        DataTable _dtEOBNAHist = new DataTable();
                                        Int64 nRespTransferDate = 0;
                                        _sqlQuery = " SELECT MAX(ISNULL(nCloseDate,0)) AS nCloseDate FROM BL_EOB_NextAction_HST WITH(NOLOCK) WHERE nBillingTransactionID = " + _NewTransaction.TransactionMasterID;
                                        ODB.Retrive_Query(_sqlQuery, out _dtEOBNAHist);
                                        if (_dtEOBNAHist != null && _dtEOBNAHist.Rows.Count > 0)
                                        {
                                            if (_dtEOBNAHist.Rows[0]["nCloseDate"].ToString() != "")
                                            {
                                                nRespTransferDate = (Convert.ToInt64(_dtEOBNAHist.Rows[0]["nCloseDate"]) >= gloDateMaster.gloDate.DateAsNumber(_InsTransferCloseDate) ? Convert.ToInt32(_dtEOBNAHist.Rows[0]["nCloseDate"]) : gloDateMaster.gloDate.DateAsNumber(_InsTransferCloseDate));
                                            }
                                        }

                                        for (int i = 0; i < _NewTransaction.Lines.Count; i++)
                                        {
                                            if (_NewTransaction.ResponsibilityType == PayerMode.Self)
                                            {
                                                //ogloBilling.UpdateNextParty(_NewTransaction.ClaimNo, _NewTransaction.SubClaimNo, _NewTransaction.TransactionMasterID, _NewTransaction.Lines[i].TransactionMasterDetailID, _NewTransaction.TransactionID, _NewTransaction.Lines[i].TransactionDetailID, _NewTransaction.PatientID, PayerMode.Self.ToString().ToUpper(), _NewTransaction.ResponsibilityNo, PayerMode.Self, 0, _NewTransaction.Lines[i].Total, _NewTransaction.ClinicID, sNextActionCode, sNextActionDesc, nRespTransferDate);
                                                ogloBilling.UpdateNextParty(_NewTransaction.ClaimNo, _NewTransaction.SubClaimNo, _NewTransaction.TransactionMasterID, _NewTransaction.Lines[i].TransactionMasterDetailID, _NewTransaction.TransactionID, _NewTransaction.Lines[i].TransactionDetailID, _NewTransaction.PatientID, PayerMode.Self.ToString(), _NewTransaction.ResponsibilityNo, PayerMode.Self, 0, _NewTransaction.Lines[i].Total, _NewTransaction.ClinicID, sNextActionCode, sNextActionDesc, nRespTransferDate);
                                            }
                                            else if (_NewTransaction.Lines[i].InsuranceSelfMode == PayerMode.BadDebt)
                                            {
                                                string collectionAgency = "";
                                                collectionAgency = ogloBilling.GetCollectionAgencyname(_NewTransaction.ContactID);
                                                ogloBilling.UpdateNextParty(_NewTransaction.ClaimNo, _NewTransaction.SubClaimNo, _NewTransaction.TransactionMasterID, _NewTransaction.Lines[i].TransactionMasterDetailID, _NewTransaction.TransactionID, _NewTransaction.Lines[i].TransactionDetailID, _NewTransaction.InsuranceID, collectionAgency, _NewTransaction.ResponsibilityNo, PayerMode.BadDebt, _NewTransaction.ContactID, _NewTransaction.Lines[i].Charges, _NewTransaction.Lines[i].ClinicID, sNextActionCode, sNextActionDesc, nRespTransferDate);
                                            }
                                            else
                                            {
                                                //ogloBilling.UpdateNextParty(_NewTransaction.ClaimNo, _NewTransaction.SubClaimNo, _NewTransaction.TransactionMasterID, _NewTransaction.Lines[i].TransactionMasterDetailID, _NewTransaction.TransactionID, _NewTransaction.Lines[i].TransactionDetailID, _NewTransaction.InsuranceID, _NewTransaction.Lines[i].InsuranceName.ToUpper(), _NewTransaction.ResponsibilityNo, PayerMode.Insurance, _NewTransaction.ContactID, _NewTransaction.Lines[i].Charges, _NewTransaction.Lines[i].ClinicID, sNextActionCode, sNextActionDesc, nRespTransferDate);
                                                ogloBilling.UpdateNextParty(_NewTransaction.ClaimNo, _NewTransaction.SubClaimNo, _NewTransaction.TransactionMasterID, _NewTransaction.Lines[i].TransactionMasterDetailID, _NewTransaction.TransactionID, _NewTransaction.Lines[i].TransactionDetailID, _NewTransaction.InsuranceID, _NewTransaction.Lines[i].InsuranceName, _NewTransaction.ResponsibilityNo, PayerMode.Insurance, _NewTransaction.ContactID, _NewTransaction.Lines[i].Charges, _NewTransaction.Lines[i].ClinicID, sNextActionCode, sNextActionDesc, nRespTransferDate);
                                            }
                                            
                                            #region "Update EOB Next Action --- Needs To be Discussed"

                                            //_sqlQuery = "";
                                            //_sqlQuery = " Update EOB_EXT WITH (READPAST) set " +
                                            //                                       " sNextAction = '" + sNextActionCode + "'," +
                                            //                                       " sNextParty= '" + _NewTransaction.InsuranceName + "'," +
                                            //                                       " nNextPartyID=" + _NewTransaction.InsuranceID + "";

                                            //ODB.Execute_Query(_sqlQuery);

                                            #endregion
                                        }
                                                                               
                                    }

                                    #region " Update BL_EOB_NextAction with new Tracking ID "

                                    if (_NewTransaction.Lines != null && _NewTransaction.Lines.Count > 0)
                                    {
                                        for (int l = 0; l < _NewTransaction.Lines.Count; l++)
                                        {
                                            _sqlQuery = " Update BL_EOB_NextAction WITH (READPAST) set " +
                                                                                   " sSubClaimNo = '" + _NewTransaction.SubClaimNo + "'," +
                                                                                   "  nTrackMstTrnID= " + _NewTransaction.TransactionID + "," +
                                                                                   "  nTrackMstTrnDetailID=" + _NewTransaction.Lines[l].TransactionDetailID +
                                                                                   " where nBillingTransactionID = " + _NewTransaction.TransactionMasterID +
                                                                                   " and nBillingTransactionDetailID = " + _NewTransaction.Lines[l].TransactionMasterDetailID + "";
                                            ODB.Execute_Query(_sqlQuery);





                                        }
                                    }

                                    #endregion " Update BL_EOB_NextAction with new Tracking ID "

                                    //Main Claim
                                    if (_Transaction.TransactionID > 0)
                                    {

                                        if (_Transaction.SubClaimNo.Contains("-") == true)
                                        {
                                            sql_query = "update dbo.BL_Transaction_Claim_MST WITH (READPAST) set sMainClaimNo = '" + sMainClaimNo.ToString() + "' ,nEOBPaymentID = 0 ,nEOBID = 0 where nTransactionID =  " + _Transaction.TransactionID;
                                        }
                                        else
                                        {
                                            sql_query = "update dbo.BL_Transaction_Claim_MST WITH (READPAST) set sMainClaimNo = '" + _Transaction.SubClaimNo + "' ,nEOBPaymentID = 0 ,nEOBID = 0 where nTransactionID =  " + _Transaction.TransactionID;
                                        }

                                        ODB.Execute_Query(sql_query);

                                        ogloBilling.SaveTransactionTrackHistory(_NewTransaction.TransactionMasterID, _NewTransaction.TransactionID, _NewTransaction.TransactionUserID, _NewTransaction.TransactionUserName);

                                    }
                                }
                            }
                        }
                    }
                }

                _result = true;

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
            finally
            {
                if (ogloBilling != null) { ogloBilling.Dispose(); }
                if (oClsBL_Hold != null) { oClsBL_Hold.Dispose(); }
                if (ODB != null) { ODB.Disconnect(); ODB.Dispose(); }
                if (_Transaction != null) { _Transaction.Dispose(); }
                if (_NewTransaction != null) { _NewTransaction.Dispose(); }
                if (_dt != null)
                {
                    _dt.Dispose();
                    _dt = null;
                }
                if(_dtVoidedTransaction!=null)
                {
                    _dtVoidedTransaction.Dispose();
                    _dtVoidedTransaction=null;
                }

            }

            return _result;
        }


        private TransactionStatus getLastClaimStatus(Int64 _nOlderTransactionID, string _databaseconnectionstring)
        {
            TransactionStatus tranStatus = TransactionStatus.None;
            string _sqlQuery = ""; 
            gloDatabaseLayer.DBLayer ODB = null;
            DataTable _dt =null;

            try
            {
                _sqlQuery = "SELECT ISNULL(nStatus,0) AS nStatus FROM BL_CMSEDI_ElectronicClaim WITH(NOLOCK) WHERE nBatchID = " + _nOlderTransactionID;
                ODB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                ODB.Connect(false);
                ODB.Retrive_Query(_sqlQuery, out _dt);
                ODB.Disconnect();
                if (_dt != null && _dt.Rows.Count > 0)
                {
                    tranStatus = (TransactionStatus)_dt.Rows[0]["nStatus"];
                }
                else
                {
                    _sqlQuery = "SELECT ISNULL(nStatus,0) AS nStatus FROM BL_CMSEDI_Claim_Send WITH(NOLOCK) WHERE nBatchID = " + _nOlderTransactionID;
                    ODB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                    ODB.Connect(false);
                    ODB.Retrive_Query(_sqlQuery, out _dt);
                    ODB.Disconnect();
                    if (_dt != null && _dt.Rows.Count > 0)
                    {
                        tranStatus = TransactionStatus.SendToClaimManager;
                    }
                    else
                    {
                        tranStatus = TransactionStatus.Batch;
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (ODB != null) { ODB.Dispose(); }
                
                if (_dt != null)
                {
                    _dt.Dispose();
                    _dt = null;
                }
            }
            return tranStatus;
        }

        public void GetLatestVoidedClaims(Int64 nTransactionID, string _databaseconnectionstring, ref string sReturnClaim)
        {
            string _sqlQuery = "";
            gloDatabaseLayer.DBLayer ODB = null;
            DataTable _dt = null;
            DataTable _dtEOB = null;

            try
            {
                _sqlQuery = "SELECT ISNULL(nTransactionID,0) AS nTransactionID FROM bl_transaction_claim_mst WITH(NOLOCK) WHERE nParentTransactionID = " + nTransactionID;
                ODB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                ODB.Connect(false);
                ODB.Retrive_Query(_sqlQuery, out _dt);

                if (_dt.Rows.Count > 0 && Convert.ToInt64(_dt.Rows[0][0]) > 0)
                {
                    foreach (DataRow _dr in _dt.Rows)
                    {
                        //_sqlQuery = "SELECT nTrackTrnID from BL_EOBPayment_dtl WHERE isnull(nTrackTrnID,0) = " + Convert.ToInt64(_dr[0]) + " AND ISNULL(bIsPaymentVoid,0) = 0";
                        _sqlQuery = "SELECT nTrackTrnID from BL_EOBPayment_dtl WITH(NOLOCK) WHERE isnull(nTrackTrnID,0) = " + Convert.ToInt64(_dr[0]) + " AND ISNULL(bIsPaymentVoid,0) = 0 AND nPaymentType IN("
                        +(int)EOBPaymentType.InsuracePayment + "," + (int)EOBPaymentType.InsuraceRefund + "," + (int)EOBPaymentType.InsuraceReserverd + "," + (int)EOBPaymentType.InsuranceCorrection + ")";
                        ODB.Retrive_Query(_sqlQuery, out _dtEOB);

                        if (_dtEOB.Rows.Count == 0)
                        {
                            GetLatestVoidedClaims(Convert.ToInt64(_dr[0]), _databaseconnectionstring, ref sReturnClaim);
                        }
                    }
                }
                else
                {
                    if (sReturnClaim != "")
                    {
                        sReturnClaim += "," + Convert.ToString(nTransactionID);
                    }
                    else
                    {
                        sReturnClaim = Convert.ToString(nTransactionID);
                    }
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (ODB != null) { ODB.Disconnect(); ODB.Dispose(); }
              
                if (_dt != null)
                {
                    _dt.Dispose();
                    _dt = null;
                }


                if (_dtEOB != null)
                {
                    _dtEOB.Dispose();
                    _dtEOB = null;
                }
            }
        }

        public void GetBackwardVoidedClaim(Int64 nTransactionID, string _databaseconnectionstring, ref Int64 nReturnClaim)
        {
            string _sqlQuery = "";
            gloDatabaseLayer.DBLayer ODB = null;
            DataTable _dt = null;
            DataTable _dtEOB = null;

            try
            {
                _sqlQuery = "SELECT nParentTransactionID FROM bl_transaction_claim_mst WITH(NOLOCK) WHERE nTransactionID = " + nTransactionID;
                ODB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                ODB.Connect(false);
                ODB.Retrive_Query(_sqlQuery, out _dt);


                if (_dt.Rows.Count > 0 && Convert.ToInt64(_dt.Rows[0][0]) > 0)
                {
                    //_sqlQuery = "SELECT nTrackTrnID from BL_EOBPayment_dtl WHERE isnull(nTrackTrnID,0) = " + Convert.ToInt64(_dt.Rows[0][0]) + " AND ISNULL(bIsPaymentVoid,0) = 1";

                    _sqlQuery = "SELECT nTrackTrnID from BL_EOBPayment_dtl WITH(NOLOCK) WHERE isnull(nTrackTrnID,0) = " + Convert.ToInt64(_dt.Rows[0][0]) + " AND ISNULL(bIsPaymentVoid,0) = 1 AND nPaymentType IN(" 
                                + (int)EOBPaymentType.InsuracePayment + "," + (int)EOBPaymentType.InsuraceRefund + "," + (int)EOBPaymentType.InsuraceReserverd + "," +(int)EOBPaymentType.InsuranceCorrection + ")";
                    ODB.Retrive_Query(_sqlQuery, out _dtEOB);

                    if (_dtEOB.Rows.Count > 0)
                    {
                        foreach (DataRow _dr in _dtEOB.Rows)
                        {
                            nReturnClaim = Convert.ToInt64(_dr[0]);
                            GetBackwardVoidedClaim(Convert.ToInt64(_dr[0]), _databaseconnectionstring, ref nReturnClaim);
                        }
                    }
                    //else
                    //{
                    //    GetBackwardVoidedClaim(Convert.ToInt64(_dt.Rows[0][0]), _databaseconnectionstring, ref nReturnClaim);
                    //}

                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
            finally
            {
                if (ODB != null) { ODB.Disconnect(); ODB.Dispose(); }
                if (_dt != null)
                {
                    _dt.Dispose();
                    _dt = null;
                }


                if (_dtEOB != null)
                {
                    _dtEOB.Dispose();
                    _dtEOB = null;
                }
            }

        }

        public void GetLatestClaims(Int64 nTransactionID, string _databaseconnectionstring, ref string sClaims)
        {
            string _sqlQuery = "";
            gloDatabaseLayer.DBLayer ODB = null;
            DataTable _dt =null;

            try
            {
                _sqlQuery = "SELECT nTransactionID FROM bl_transaction_claim_mst WITH(NOLOCK) WHERE nParentTransactionID = " + nTransactionID;
                ODB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                ODB.Connect(false);
                ODB.Retrive_Query(_sqlQuery, out _dt);

                if (_dt.Rows.Count > 0)
                {
                    foreach (DataRow _dr in _dt.Rows)
                    {
                        GetLatestClaims(Convert.ToInt64(_dr["nTransactionID"]), _databaseconnectionstring, ref sClaims);
                    }
                }
                else
                {
                    if (sClaims == "")
                    {
                        sClaims = Convert.ToString(nTransactionID);
                    }
                    else
                    {
                        sClaims += "," + Convert.ToString(nTransactionID);
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (ODB != null) { ODB.Disconnect(); ODB.Dispose(); }
                if (_dt != null)
                {
                    _dt.Dispose();
                    _dt = null;
                }

            }

        }

        public bool IsForwardPaymentMade(Int64 nTransactionID, string _databaseconnectionstring)
        {
            string _sqlQuery = "";
            gloDatabaseLayer.DBLayer ODB = null;
            DataTable _dt = null;
            DataTable _dtEOBPayment = null;
            bool bReturn = false;
            try
            {
                if (!_bReturn)
                {
                    //Collecting Transaction ID of Voided Claim
                    _sqlQuery = "SELECT nTransactionID FROM bl_transaction_claim_mst WITH(NOLOCK) WHERE nParentTransactionID = " + nTransactionID;
                    ODB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                    ODB.Connect(false);
                    ODB.Retrive_Query(_sqlQuery, out _dt);

                    if (_dt.Rows.Count > 0)
                    {
                        for (int iCount = 0; iCount <= _dt.Rows.Count - 1; iCount++)
                        {
                            _sqlQuery = "SELECT 1 FROM bl_eobPayment_dtl WITH(NOLOCK) WHERE nTrackTrnID =  " + Convert.ToInt64(_dt.Rows[iCount][0]) + " AND ISNULL(bIsPaymentVoid,0) = 0 AND nPaymentType IN(" 
                                + (int)EOBPaymentType.InsuracePayment + "," + (int)EOBPaymentType.InsuraceRefund + "," + (int)EOBPaymentType.InsuraceReserverd + "," +(int)EOBPaymentType.InsuranceCorrection + ")";
                            ODB.Retrive_Query(_sqlQuery, out _dtEOBPayment);

                            if (_dtEOBPayment.Rows.Count != 0)
                            {
                                _bReturn = true;
                                bReturn = true;

                            }
                            else
                            {
                                IsForwardPaymentMade(Convert.ToInt64(_dt.Rows[iCount][0]), _databaseconnectionstring);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                bReturn = false;
            }
            finally
            {
                if (ODB != null) { ODB.Disconnect(); ODB.Dispose(); }

                if (_dt != null)
                {
                    _dt.Dispose();
                    _dt = null;
                }
                if (_dtEOBPayment != null)
                {
                    _dtEOBPayment.Dispose();
                    _dtEOBPayment = null;
                }
            }
            return bReturn;
        }

        bool _result = true;
        public bool DeleteBatch(Int64 TransactionMasterID, Int64 TransactionID, Int64 ClinicID)
        {
            gloDatabaseLayer.DBLayer ODB = new gloDatabaseLayer.DBLayer(gloSettings.AppSettings.ConnectionStringPM);
            DataTable _dtBatch = null;
            DataTable _dtClaim = null;
            try
            {

              
                Object _objCount = null;
                string _Strquery = "";
                Int64 _BatchID = 0;

                ODB.Connect(false);
                if (_result == true)
                {
                    _Strquery = "Select nBatchID from Bl_Transaction_Batch_DTL WITH(NOLOCK)  where nTransactionMasterID=" + TransactionMasterID + "  and  nTransactionID=" + TransactionID + " and nClinicID=" + ClinicID + "";
                    ODB.Retrive_Query(_Strquery, out _dtBatch);
                    if (_dtBatch != null && _dtBatch.Rows.Count > 0)
                    {
                        _BatchID = Convert.ToInt64(_dtBatch.Rows[0]["nBatchID"]);
                    }

                    if (_BatchID > 0)
                    {
                        // _Strquery = "Delete Bl_Transaction_Batch_DTL where nTransactionMasterID=" + TransactionMasterID + " and  nTransactionID=" + TransactionID + " and nClinicID=" + ClinicID + "";

                        _Strquery = " Delete BL_Transaction_Batch_DTL where nTransactionID IN " +
                                             " ( SELECT     BL_Transaction_Claim_MST.nTransactionID " +
                                             " FROM         BL_Transaction_Claim_MST WITH(NOLOCK) INNER JOIN " +
                                             " BL_Transaction_Batch_DTL WITH(NOLOCK) ON " +
                                             " BL_Transaction_Claim_MST.nTransactionMasterID = BL_Transaction_Batch_DTL.nTransactionMasterID " +
                                             " where  ISNULL(BL_Transaction_Claim_MST.nStatus,0)=3  and BL_Transaction_Claim_MST.nClinicID=" + ClinicID + ") and BL_Transaction_Batch_DTL.nTransactionID=" + TransactionID + "  ";

                        ODB.Execute_Query(_Strquery);

                        _Strquery = "Select Count(nBatchID) from Bl_Transaction_Batch_DTL WITH(NOLOCK) where nBatchID=" + _BatchID + "";
                        _objCount = ODB.ExecuteScalar_Query(_Strquery);

                        if (_objCount != null && Convert.ToInt64(_objCount) == 0)
                        {
                            _Strquery = "Delete Bl_Transaction_Batch where nBatchID=" + _BatchID + "";
                            ODB.Execute_Query(_Strquery);
                        }
                    }
                    _Strquery = "Select nTransactionMasterID,nParentTransactionID,nClinicID from BL_Transaction_Claim_Mst WITH(NOLOCK)  where nTransactionID=" + TransactionID + " and nClinicID=" + ClinicID + " and (nStatus=" + TransactionStatus.Batch.GetHashCode() + " or nStatus=" + TransactionStatus.Queue.GetHashCode() + "  or nStatus=" + TransactionStatus.None.GetHashCode() + ")  ";
                        ODB.Retrive_Query(_Strquery, out _dtClaim);
                        if (_dtClaim != null && _dtClaim.Rows.Count > 0)
                        {


                        _result = DeleteBatch(Convert.ToInt64(_dtClaim.Rows[0]["nTransactionMasterID"]), Convert.ToInt64(_dtClaim.Rows[0]["nParentTransactionID"]), (Convert.ToInt64(_dtClaim.Rows[0]["nClinicID"])));
                    }
                    else
                    {
                        _result = false;
                    }

                }
            }
            catch //(Exception ex)
            {
                _result = false;
            }
            finally
            {
                if (ODB != null)
                {
                    ODB.Disconnect();
                    ODB.Dispose();
                }
                if (_dtBatch != null)
                {
                    _dtBatch.Dispose();
                    _dtBatch = null;
                }
                if (_dtClaim != null)
                {
                    _dtClaim.Dispose();
                    _dtClaim = null;
                }

            }
            return _result;
        }


        #endregion
    }

    public class SplitClaimLine
    {
        #region "Constructor & Destructor"

        public SplitClaimLine()
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

        ~SplitClaimLine()
        {
            Dispose(false);
        }

        #endregion

        #region " Declarations "

        private Int64 _TransactionMasterDetailID = 0;
        private Int64 _TransactionDetailId = 0;
        private String _NextActionCode = String.Empty;
        private int _ResponsibilityNo = 0;
        private Int64 _InsuranceId = 0;
        private Int64 _ContactID = 0;
        private String _Party = String.Empty;
        #endregion " Declarations "

        #region Property Procedures of Transaction Class

        public Int64 TransactionMasterDetailID
        {
            get { return _TransactionMasterDetailID; }
            set { _TransactionMasterDetailID = value; }
        }

        public Int64 TransactionDetailID
        {
            get { return _TransactionDetailId; }
            set { _TransactionDetailId = value; }
        }

        public String NextActionCode
        {
            get { return _NextActionCode; }
            set { _NextActionCode = value; }
        }

        public int ResponsibilityNo
        {
            get { return _ResponsibilityNo; }
            set { _ResponsibilityNo = value; }
        }
        public Int64 InsuranceId
        {
            get { return _InsuranceId; }
            set { _InsuranceId = value; }
        }
        public Int64 ContactID
        {
            get { return _ContactID; }
            set { _ContactID = value; }
        }

        public String Party
        {
            get { return _Party; }
            set { _Party = value; }
        }

        #endregion Property Procedures of Transaction Class
    }

    public class SplitClaimLines
    {

        #region "Constructor & Destructor"

        public SplitClaimLines()
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

                }
            }
            disposed = true;
        }


        ~SplitClaimLines()
        {
            Dispose(false);
        }
        #endregion

        #region " Declarations "
        protected ArrayList _innerlist;
        #endregion " Declarations "

        #region "Public Methods"
        // Methods Add, Remove, Count , Item of TransactionLine
        public int Count
        {
            get { return _innerlist.Count; }
        }

        public void Add(SplitClaimLine item)
        {
            _innerlist.Add(item);
        }

        public bool Remove(SplitClaimLine item)
        //Remark - Work Remining for comparision
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

        public SplitClaimLine this[int index]
        {
            get
            { return (SplitClaimLine)_innerlist[index]; }
        }

        public bool Contains(SplitClaimLine item)
        {
            return _innerlist.Contains(item);
        }

        public int IndexOf(SplitClaimLine item)
        {
            return _innerlist.IndexOf(item);
        }

        public void CopyTo(SplitClaimLine[] array, int index)
        {
            _innerlist.CopyTo(array, index);
        }
        #endregion "Public Methods"
    }

    public class gloSplitClaim
    {
        #region "Constructor & Destructor"

        public gloSplitClaim(string DatabaseConnectionString)
        {
            _databaseconnectionstring = DatabaseConnectionString;
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

        ~gloSplitClaim()
        {
            Dispose(false);
        }

        #endregion

        #region " Declarations "

        private string _databaseconnectionstring = "";
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

        #endregion " Declarations "

        #region " Public Methods "

        public bool SplitTransactionClaim(SplitClaimDetails _SplitClaimDetails, DataTable dtHoldInfo)
        {
            bool _result = false;

            gloBilling ogloBilling = new gloBilling(_databaseconnectionstring, _databaseconnectionstring);

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            String _sqlQuery = "";
            DataTable dtNextAction = null;
        //    object _retVal = null;
            String _DetailIDMasterList = "0";
            String _DetailIDList = "0";
            _SplitClaimDetails.IsPending = false;
            try
            {
                oDB.Connect(false);

                if (_SplitClaimDetails.Lines != null && _SplitClaimDetails.Lines.Count > 0)
                {
                    for (int i = 0; i < _SplitClaimDetails.Lines.Count; i++)
                    {
                        _DetailIDMasterList += "," + _SplitClaimDetails.Lines[i].TransactionMasterDetailID;
                    }
                    for (int i = 0; i < _SplitClaimDetails.Lines.Count; i++)
                    {
                        _DetailIDList += "," + _SplitClaimDetails.Lines[i].TransactionDetailID;
                    }

                    if (_SplitClaimDetails.IsPaymentDone == false && IsSameClaim(_SplitClaimDetails) == true)
                    {
                        _sqlQuery = "select nNextActionPartyNumber from BL_EOB_NextAction WITH(NOLOCK) where nBillingTransactionID = " + _SplitClaimDetails.TransactionMasterID + " and nBillingTransactionDetailID in (" + _DetailIDMasterList + ") and sNextActionCode in ('T','B','R','P','N') group by nNextActionPartyNumber ";
                        oDB.Retrive_Query(_sqlQuery, out dtNextAction);

                        if (dtNextAction != null && dtNextAction.Rows.Count > 0)
                        {

                            #region " Update Claims "
                            String _MainClaimNo = String.Empty;
                            DataTable dtClaim = null;
                            _sqlQuery = "select nClaimNo,nSubClaimNo,isnull(nParentClaimNo,'') as nParentClaimNo, " +
                                              "  isnull(sMainClaimNo,'') as sMainClaimNo from dbo.BL_Transaction_Claim_MST WITH(NOLOCK) " +
                                              " where nTransactionMasterID=" + _SplitClaimDetails.TransactionMasterID + " and nTransactionID =" + _SplitClaimDetails.TransactionID;
                            oDB.Retrive_Query(_sqlQuery, out dtClaim);
                            if (dtClaim != null && dtClaim.Rows.Count > 0)
                            {
                                if (Convert.ToString(dtClaim.Rows[0]["nSubClaimNo"]).Contains("-") == true)
                                { _MainClaimNo = Convert.ToString(dtClaim.Rows[0]["sMainClaimNo"]); }
                                else
                                { _MainClaimNo = Convert.ToString(dtClaim.Rows[0]["nSubClaimNo"]); }
                            }
                            dtClaim = null;


                            if (_SplitClaimDetails.Lines[0].InsuranceId == 0)
                            {
                                _sqlQuery = "update BL_Transaction_Claim_MST WITH(READPAST) set sMainClaimNo = '" + _MainClaimNo + "',nInsuranceID = 0 ,nContactID=0, nResponsibilityType= " + PayerMode.Self.GetHashCode() + " ,nResponsibilityNo=" + _SplitClaimDetails.Lines[0].ResponsibilityNo.ToString() + ",nEOBPaymentID=" + _SplitClaimDetails.EOBPaymentID.ToString() + ",nEOBID=" + _SplitClaimDetails.EOBID.ToString() +
                                                 " where nTransactionMasterID=" + _SplitClaimDetails.TransactionMasterID + " and nTransactionID =" + _SplitClaimDetails.TransactionID;
                            }
                            else
                            {
                                _sqlQuery = "update BL_Transaction_Claim_MST WITH(READPAST) set sMainClaimNo = '" + _MainClaimNo + "',nInsuranceID = " + _SplitClaimDetails.Lines[0].InsuranceId + " ,nContactID=" + _SplitClaimDetails.Lines[0].ContactID + ", nResponsibilityType=  " + PayerMode.Insurance.GetHashCode() + ",nResponsibilityNo=" + _SplitClaimDetails.Lines[0].ResponsibilityNo.ToString() + ",nEOBPaymentID=" + _SplitClaimDetails.EOBPaymentID.ToString() + ",nEOBID=" + _SplitClaimDetails.EOBID.ToString() +
                                                 " where nTransactionMasterID=" + _SplitClaimDetails.TransactionMasterID + " and nTransactionID =" + _SplitClaimDetails.TransactionID;
                            }

                            oDB.Execute_Query(_sqlQuery);

                            //Pending Claims
                            if (_SplitClaimDetails.IsPending == true)
                            {
                                _sqlQuery = "update BL_Transaction_Claim_MST WITH(READPAST) set nStatus=" + TransactionStatus.Pending.GetHashCode() +
                                                " where nTransactionMasterID=" + _SplitClaimDetails.TransactionMasterID + " and nTransactionID =" + _SplitClaimDetails.TransactionID;
                                oDB.Execute_Query(_sqlQuery);
                            }
                            #endregion " Update Parent Claims "
                        }
                    }
                    else
                    {
                        #region "Create Claim Batch"

                        //_sqlQuery = " select Top 1  nTransactionID from dbo.BL_Transaction_Claim_MST " +
                        //                " where nTransactionMasterID =  " + MasterTransactionID +
                        //                " and nClaimStatus = " + ClaimStatus.Open.GetHashCode() + " order by nTransactionID desc ";

                        //_retVal = oDB.ExecuteScalar_Query(_sqlQuery);

                        //if (_retVal != null && Convert.ToInt64(_retVal) > 0)
                        //{
                        //    Int64 _nTransactionID = Convert.ToInt64(_retVal);
                        //    Transaction _oTransaction= null;
                        //    _oTransaction = GetChargesClaimDetails(_nTransactionID, nClinicID);
                        //    CreateClaimBatch(_oTransaction);
                        //}

                        #endregion "Create Claim Batch"

                        //Select Party which are changed
                        _sqlQuery = "select nNextActionPartyNumber from BL_EOB_NextAction WITH(NOLOCK) where nBillingTransactionID = " + _SplitClaimDetails.TransactionMasterID + " and nBillingTransactionDetailID in (" + _DetailIDMasterList + ") and sNextActionCode in ('T','B','R','P','N') group by nNextActionPartyNumber ";
                        oDB.Retrive_Query(_sqlQuery, out dtNextAction);

                        if (dtNextAction != null)
                        {

                            #region " Update Parent Claims "

                            //Close old Claim and Change Transaction Status to Insurance Paid
                            //Unhold Main Claim
                            _sqlQuery = "Update BL_Transaction_Claim_MST WITH (READPAST) set nStatus=" + TransactionStatus.InsurancePaid.GetHashCode() + ", nClaimStatus = " + ClaimStatus.Close.GetHashCode() + " where nTransactionMasterID = " + _SplitClaimDetails.TransactionMasterID + " and nTransactionID= " + _SplitClaimDetails.TransactionID + " and nStatus<>" + TransactionStatus.Rejected.GetHashCode();
                            //_sqlQuery = "Update BL_Transaction_Claim_MST set nStatus=" + TransactionStatus.InsurancePaid.GetHashCode() + ", nClaimStatus = " + ClaimStatus.Close.GetHashCode() + " where nTransactionMasterID = " + _SplitClaimDetails.TransactionMasterID + "  and nStatus<>" + TransactionStatus.Rejected.GetHashCode();                    

                            oDB.Execute_Query(_sqlQuery);

                            Int16 _NextAction = 0;

                            //Update bIsSplitted for sub-claim
                            _sqlQuery = "update BL_Transaction_Claim_Lines WITH (READPAST) set bIsSplitted = 1 " +
                                             " where nTransactionMasterID=" + _SplitClaimDetails.TransactionMasterID + " and nTransactionID =" + _SplitClaimDetails.TransactionID + "   and nTransactionDetailID in (" + _DetailIDList + ")";
                            oDB.Execute_Query(_sqlQuery);

                            #endregion " Update Parent Claims "

                            #region " Create new sub-claims "

                            //Split Claims (Create New Claims)
                            for (int i = 0; i < dtNextAction.Rows.Count; i++)
                            {

                                _NextAction = Convert.ToInt16(dtNextAction.Rows[i]["nNextActionPartyNumber"]);

                                DataTable dtNextActionCode = null;

                                _sqlQuery = "select sNextActionCode from BL_EOB_NextAction WITH(NOLOCK) where nBillingTransactionID = " + _SplitClaimDetails.TransactionMasterID + " and nBillingTransactionDetailID in (" + _DetailIDMasterList + ") and nNextActionPartyNumber=" + _NextAction + " and sNextActionCode in ('T','B','R','P','N') group by sNextActionCode ";
                                oDB.Retrive_Query(_sqlQuery, out dtNextActionCode);

                                String _NextActionCode = String.Empty;

                                for (int k = 0; k < dtNextActionCode.Rows.Count; k++)
                                {
                                    string sMainClaimNo = string.Empty;
                                    _NextActionCode = Convert.ToString(dtNextActionCode.Rows[k]["sNextActionCode"]);

                                    #region " Transaction Information "

                                    Transaction oTransactionMaster = null;
                                    //Get Transaction information
                                    //Create Sub-Claim from Main Claim
                                    oTransactionMaster = ogloBilling.GetChargesClaimDetails(_SplitClaimDetails.TransactionID, _SplitClaimDetails.ClinicID);

                                    #region " CLAIM REMITTANCE REF#"
                                    if (i == 0)
                                    {
                                        //if (_NextActionCode == "R")
                                        //{
                                        InsurancePayment.UpdateClaimRemittanceRefNo(ref _SplitClaimDetails, oTransactionMaster);
                                        //}
                                    }
                                    #endregion " CLAIM REMITTANCE REF#"



                                    if (_SplitClaimDetails.ClaimNo == 0)
                                    { _SplitClaimDetails.ClaimNo = oTransactionMaster.ClaimNo; }

                                    if (oTransactionMaster.SubClaimNo.Contains("-") == false)
                                    { sMainClaimNo = oTransactionMaster.SubClaimNo; }
                                    else
                                    { sMainClaimNo = oTransactionMaster.MainClaimNo; }
                                    oTransactionMaster.TransactionID = 0;
                                    oTransactionMaster.TransactionMasterID = _SplitClaimDetails.TransactionMasterID;
                                    //Set Parent Claim ID
                                    oTransactionMaster.ParentTransactionID = _SplitClaimDetails.TransactionID;
                                    //Set Parent Claim No                                    
                                    if (_SplitClaimDetails.SubClaimNo.Trim() != String.Empty)
                                    { oTransactionMaster.ParentClaimNo = _SplitClaimDetails.ClaimNo.ToString() + "-" + _SplitClaimDetails.SubClaimNo; }
                                    else
                                    { oTransactionMaster.ParentClaimNo = _SplitClaimDetails.ClaimNo.ToString(); }
                                    //New Sub-Claim No
                                    bool _IsSplit;
                                    oTransactionMaster.SubClaimNo = GetSubClaimNo(_SplitClaimDetails.TransactionMasterID, _SplitClaimDetails, oTransactionMaster, out _IsSplit);

                                    //Pending
                                    //Pending Claims


                                    //Set Next Insurance Party and Responsibility Information for Sub-Claim
                                    DataTable dtTrnDetails = null;
                                    _sqlQuery = "select nBillingTransactionDetailID,nNextActionPatientInsID,nNextActionContactID,nNextActionPartyNumber from BL_EOB_NextAction WITH(NOLOCK) where sNextActionCode ='" + _NextActionCode + "' and nNextActionPartyNumber = " + _NextAction + " and nBillingTransactionID = " + _SplitClaimDetails.TransactionMasterID + " and nBillingTransactionDetailID in (" + _DetailIDMasterList + ") order by nNextActionPartyNumber,sNextActionCode ";
                                    oDB.Retrive_Query(_sqlQuery, out dtTrnDetails);


                                    #region " Re-bill Claim "
                                    if (_NextActionCode == "R")
                                    {
                                        oTransactionMaster.IsRebill = true;
                                    }
                                    else
                                    {
                                        oTransactionMaster.IsRebill = false;
                                    }
                                    #endregion " Re-bill Claim "

                                    #endregion " Transaction Information "

                                    #region " Service Lines Information "

                                    if (dtTrnDetails != null && dtTrnDetails.Rows.Count > 0)
                                    {
                                        oTransactionMaster.InsuranceID = Convert.ToInt64(dtTrnDetails.Rows[0]["nNextActionPatientInsID"]);
                                        oTransactionMaster.ContactID = Convert.ToInt64(dtTrnDetails.Rows[0]["nNextActionContactID"]);
                                        oTransactionMaster.ResponsibilityNo = Convert.ToInt16(dtTrnDetails.Rows[0]["nNextActionPartyNumber"]);
                                        //Set Responsibility Type
                                        //Insurance ID is 0[Zero] for Self Calim
                                        if (oTransactionMaster.InsuranceID == 0)
                                        { oTransactionMaster.ResponsibilityType = PayerMode.Self; }
                                        else
                                        { oTransactionMaster.ResponsibilityType = PayerMode.Insurance; }

                                        //Set sub-claim status to open
                                        oTransactionMaster.ClaimStatus = ClaimStatus.Open;

                                        if (_SplitClaimDetails.IsPending == true)
                                        //Set Transaction Status to Pending so it can not be dispaly on ChargeTab of Batch Form
                                        { oTransactionMaster.Transaction_Status = TransactionStatus.Pending; }
                                        else
                                        {
                                            if (_NextActionCode == "N")
                                            //Set Transaction Status to None so it can not be dispaly on ChargeTab of Batch Form
                                            { oTransactionMaster.Transaction_Status = TransactionStatus.None; }
                                            else
                                            //Set Transaction Status to Queue so it can be dispaly on ChargeTab of Batch Form
                                            { oTransactionMaster.Transaction_Status = TransactionStatus.Queue; }
                                        }


                                        #region " HOLD SUB CLAIM "
                                        try
                                        {
                                            if (oTransactionMaster.Hold != null && oTransactionMaster.Hold.IsHold == true && dtHoldInfo != null)
                                            {
                                                if (i == 0)
                                                {
                                                    _sqlQuery = "Update BL_Transaction_Claim_MST WITH (READPAST) set nStatus=" + TransactionStatus.InsurancePaid.GetHashCode() + ", nClaimStatus = " + ClaimStatus.Close.GetHashCode() + " where nTransactionMasterID = " + _SplitClaimDetails.TransactionMasterID + " and nTransactionID in (select nParentTransactionID from dbo.BL_Transaction_Claim_MST WITH(NOLOCK) where nTransactionID = " + _SplitClaimDetails.TransactionID + " and nTransactionMasterID = " + _SplitClaimDetails.TransactionMasterID + ")";
                                                    oDB.Execute_Query(_sqlQuery);
                                                }
                                                if (dtHoldInfo.Rows.Count > 1)
                                                {
                                                    DataRow[] dr = dtHoldInfo.Select("PartyNo=" + oTransactionMaster.ResponsibilityNo.ToString() + " and NextAction='" + _NextActionCode + "' and IsHold=false");
                                                    if (dr.Length != -1 && dr.GetUpperBound(0) != -1)
                                                    {
                                                        oTransactionMaster.Hold.IsHold = false;
                                                        oTransactionMaster.Hold.HoldReason = "";
                                                        oTransactionMaster.Hold = null;
                                                    }
                                                    else
                                                    {
                                                        oTransactionMaster.Hold.HoldModified = true;
                                                    }
                                                }
                                                if (dtHoldInfo.Rows.Count == 1)
                                                {
                                                    oTransactionMaster.Hold.HoldModified = true;
                                                }
                                            }
                                        }
                                        catch (Exception)
                                        {
                                        }
                                        #endregion " HOLD SUB CLAIM "


                                        //Remove Service line which are not part of sub-claim
                                        for (int j = oTransactionMaster.Lines.Count - 1; j >= 0; j--)
                                        {
                                            oTransactionMaster.Lines[j].IsLineSplitted = false;


                                            DataRow[] dr = dtTrnDetails.Select("nBillingTransactionDetailID = " + oTransactionMaster.Lines[j].TransactionMasterDetailID);
                                            if (dr.Length == 0)
                                            {
                                                oTransactionMaster.Lines.RemoveAt(j);
                                            }
                                            else
                                            {
                                                //Set Service Line Parent information for Sub-Claim
                                                //oTransactionMaster.Lines[j].TransactionMasterDetailID = oTransactionMaster.Lines[j].TransactionDetailID;                                        
                                                oTransactionMaster.Lines[j].ParentTransactionID = oTransactionMaster.TransactionID;
                                                oTransactionMaster.Lines[j].ParentTransactionDetailID = oTransactionMaster.Lines[j].TransactionDetailID;
                                                oTransactionMaster.Lines[j].TransactionDetailID = 0;
                                            }
                                        }
                                    }
                                    #endregion " Service Lines Information "

                                    #region " Add records to transaction tracking tables"
                                    //Add Sub-Claim to Tracking Table
                                    ogloBilling.AddTransactionClaim(oTransactionMaster, _SplitClaimDetails.ClinicID);
                                    #endregion " Add records to transaction tracking tables"

                                    #region " Update BL_EOB_NextAction with new Tracking ID "

                                    if (oTransactionMaster.Lines != null && oTransactionMaster.Lines.Count > 0)
                                    {
                                        for (int l = 0; l < oTransactionMaster.Lines.Count; l++)
                                        {
                                            _sqlQuery = " Update BL_EOB_NextAction WITH (READPAST) set " +
                                                            " sSubClaimNo = '" + oTransactionMaster.SubClaimNo + "'," +
                                                            "  nTrackMstTrnID= " + oTransactionMaster.TransactionID + "," +
                                                            "  nTrackMstTrnDetailID=" + oTransactionMaster.Lines[l].TransactionDetailID +
                                                            " where nBillingTransactionID = " + oTransactionMaster.TransactionMasterID +
                                                            " and nBillingTransactionDetailID = " + oTransactionMaster.Lines[l].TransactionMasterDetailID + "";
                                            oDB.Execute_Query(_sqlQuery);
                                        }
                                    }
                                    if (oTransactionMaster.TransactionID > 0)
                                    {
                                        if (oTransactionMaster.SubClaimNo.Contains("-") == true || _SplitClaimDetails.SubClaimNo.Contains("-") == true)
                                        {
                                            _sqlQuery = "update dbo.BL_Transaction_Claim_MST WITH (READPAST) set sMainClaimNo = '" + sMainClaimNo.ToString() + "',nEOBPaymentID=" + _SplitClaimDetails.EOBPaymentID.ToString() + ",nEOBID=" + _SplitClaimDetails.EOBID.ToString() + " where nTransactionID =  " + oTransactionMaster.TransactionID;
                                        }
                                        else
                                        {
                                            _sqlQuery = "update dbo.BL_Transaction_Claim_MST WITH (READPAST) set sMainClaimNo = '" + _SplitClaimDetails.SubClaimNo.ToString() + "',nEOBPaymentID=" + _SplitClaimDetails.EOBPaymentID.ToString() + ",nEOBID=" + _SplitClaimDetails.EOBID.ToString() + " where nTransactionID =  " + oTransactionMaster.TransactionID;
                                        }

                                        oDB.Execute_Query(_sqlQuery);
                                    }
                                    #endregion " Update BL_EOB_NextAction with new Tracking ID "

                                    #region " Add records to transaction tracking history tables (5060)"
                                    //Add Sub-Claim to Tracking history Table
                                    ogloBilling.SaveTransactionTrackHistory(oTransactionMaster.TransactionMasterID, oTransactionMaster.TransactionID, oTransactionMaster.TransactionUserID, oTransactionMaster.TransactionUserName);
                                    #endregion " Add records to transaction tracking history tables"

                                }
                                dtNextActionCode.Dispose();
                                dtNextActionCode = null;
                            }

                            #endregion " Create new sub-claims "
                        }

                        _sqlQuery = "Update BL_Transaction_Claim_MST WITH (READPAST) set bIsHold=0 where nTransactionMasterID = " + _SplitClaimDetails.TransactionMasterID + " and nTransactionID= " + _SplitClaimDetails.TransactionID;
                        oDB.Execute_Query(_sqlQuery);

                    }
                    oDB.Disconnect();
                    _result = true;
                }
            }
            catch
            {
                _result = false;
            }
            finally
            {
                ogloBilling.Dispose();
                ogloBilling = null;
                oDB.Dispose();
                oDB = null;

                if (dtNextAction != null)
                {
                    dtNextAction.Dispose();
                    dtNextAction = null;
                }

            }
            return _result;
        }

        public bool SplitTransactionClaim_ERA(ref SplitClaimDetails _SplitClaimDetails, DataTable dtHoldInfo)
        {
            bool _result = false;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloBilling ogloBilling = new gloBilling(_databaseconnectionstring, _databaseconnectionstring);
            String _sqlQuery = "";
            DataTable dtNextAction = null;
         //   object _retVal = null;
            String _DetailIDMasterList = "0";
            String _DetailIDList = "0";
            _SplitClaimDetails.IsPending = false;
            Exception _customException = null;

            try
            {

                if (_SplitClaimDetails.UseExtSqlConnection == false)
                { oDB.Connect(false); }

                if (_SplitClaimDetails.Lines != null && _SplitClaimDetails.Lines.Count > 0)
                {
                    for (int i = 0; i < _SplitClaimDetails.Lines.Count; i++)
                    {
                        _DetailIDMasterList += "," + _SplitClaimDetails.Lines[i].TransactionMasterDetailID;
                    }
                    for (int i = 0; i < _SplitClaimDetails.Lines.Count; i++)
                    {
                        _DetailIDList += "," + _SplitClaimDetails.Lines[i].TransactionDetailID;
                    }

                    if (_SplitClaimDetails.IsPaymentDone == false && IsSameClaim(_SplitClaimDetails) == true)
                    {
                        _sqlQuery = "select nNextActionPartyNumber from BL_EOB_NextAction WITH (NOLOCK) where nBillingTransactionID = " + _SplitClaimDetails.TransactionMasterID + " and nBillingTransactionDetailID in (" + _DetailIDMasterList + ") and sNextActionCode in ('T','B','R','P','N') group by nNextActionPartyNumber ";

                        if (_SplitClaimDetails.UseExtSqlConnection == false)
                        {
                            oDB.Retrive_Query(_sqlQuery, out dtNextAction);
                        }
                        else
                        {
                            using (SqlCommand _sqlCommand = new SqlCommand())
                            {
                                _sqlCommand.Connection = _SplitClaimDetails.ExtSqlConnection;
                                _sqlCommand.Transaction = _SplitClaimDetails.ExtSqlTransaction;
                                _sqlCommand.CommandType = CommandType.Text;
                                _sqlCommand.CommandText = _sqlQuery;

                                SqlDataAdapter _dataAdapter = new SqlDataAdapter(_sqlCommand);
                                DataSet _dataset = new DataSet();
                                _dataAdapter.Fill(_dataset);
                                if (_dataset.Tables[0] != null)
                                { dtNextAction = _dataset.Tables[0]; }
                                if (_dataAdapter != null) { _dataAdapter.Dispose(); }
                                if (_dataset != null) { _dataset.Dispose(); }
                            }
                        }

                        if (dtNextAction != null && dtNextAction.Rows.Count > 0)
                        {
                            #region " Update Claims "

                            String _MainClaimNo = String.Empty;
                            DataTable dtClaim = null;

                            _sqlQuery = "select nClaimNo,nSubClaimNo,isnull(nParentClaimNo,'') as nParentClaimNo, " +
                                              "  isnull(sMainClaimNo,'') as sMainClaimNo from dbo.BL_Transaction_Claim_MST WITH (NOLOCK) " +
                                              " where nTransactionMasterID=" + _SplitClaimDetails.TransactionMasterID + " and nTransactionID =" + _SplitClaimDetails.TransactionID + "  ";

                            if (_SplitClaimDetails.UseExtSqlConnection == false)
                            {
                                oDB.Retrive_Query(_sqlQuery, out dtClaim);
                            }
                            else
                            {
                                using (SqlCommand _sqlCommand = new SqlCommand())
                                {
                                    _sqlCommand.Connection = _SplitClaimDetails.ExtSqlConnection;
                                    _sqlCommand.Transaction = _SplitClaimDetails.ExtSqlTransaction;
                                    _sqlCommand.CommandType = CommandType.Text;
                                    _sqlCommand.CommandText = _sqlQuery;

                                    SqlDataAdapter _dataAdapter = new SqlDataAdapter(_sqlCommand);
                                    DataSet _dataset = new DataSet();
                                    _dataAdapter.Fill(_dataset);
                                    if (_dataset.Tables[0] != null)
                                    { dtClaim = _dataset.Tables[0]; }
                                    if (_dataAdapter != null) { _dataAdapter.Dispose(); }
                                    if (_dataset != null) { _dataset.Dispose(); }
                                }
                            }

                            if (dtClaim != null && dtClaim.Rows.Count > 0)
                            {
                                if (Convert.ToString(dtClaim.Rows[0]["nSubClaimNo"]).Contains("-") == true)
                                { _MainClaimNo = Convert.ToString(dtClaim.Rows[0]["sMainClaimNo"]); }
                                else
                                { _MainClaimNo = Convert.ToString(dtClaim.Rows[0]["nSubClaimNo"]); }
                            }
                            dtClaim = null;


                            if (_SplitClaimDetails.Lines[0].InsuranceId == 0)
                            {
                                _sqlQuery = "update BL_Transaction_Claim_MST WITH(READPAST) set sMainClaimNo = '" + _MainClaimNo + "',nInsuranceID = 0 ,nContactID=0, nResponsibilityType= " + PayerMode.Self.GetHashCode() + " ,nResponsibilityNo=" + _SplitClaimDetails.Lines[0].ResponsibilityNo.ToString() + ",nEOBPaymentID=" + _SplitClaimDetails.EOBPaymentID.ToString() + ",nEOBID=" + _SplitClaimDetails.EOBID.ToString() +
                                                 " where nTransactionMasterID=" + _SplitClaimDetails.TransactionMasterID + " and nTransactionID =" + _SplitClaimDetails.TransactionID + " ";
                            }
                            else
                            {
                                _sqlQuery = "update BL_Transaction_Claim_MST WITH(READPAST) set sMainClaimNo = '" + _MainClaimNo + "',nInsuranceID = " + _SplitClaimDetails.Lines[0].InsuranceId + " ,nContactID=" + _SplitClaimDetails.Lines[0].ContactID + ", nResponsibilityType=  " + PayerMode.Insurance.GetHashCode() + ",nResponsibilityNo=" + _SplitClaimDetails.Lines[0].ResponsibilityNo.ToString() + ",nEOBPaymentID=" + _SplitClaimDetails.EOBPaymentID.ToString() + ",nEOBID=" + _SplitClaimDetails.EOBID.ToString() +
                                                 " where nTransactionMasterID=" + _SplitClaimDetails.TransactionMasterID + " and nTransactionID =" + _SplitClaimDetails.TransactionID + " ";
                            }

                            if (_SplitClaimDetails.UseExtSqlConnection == false)
                            {
                                oDB.Execute_Query(_sqlQuery);
                            }
                            else
                            {
                                using (SqlCommand _sqlCommand = new SqlCommand())
                                {
                                    _sqlCommand.Connection = _SplitClaimDetails.ExtSqlConnection;
                                    _sqlCommand.Transaction = _SplitClaimDetails.ExtSqlTransaction;
                                    _sqlCommand.CommandType = CommandType.Text;
                                    _sqlCommand.CommandText = _sqlQuery;
                                    _sqlCommand.ExecuteNonQuery();
                                }
                            }

                            //Pending Claims
                            if (_SplitClaimDetails.IsPending == true)
                            {
                                _sqlQuery = "update BL_Transaction_Claim_MST WITH(READPAST) set nStatus=" + TransactionStatus.Pending.GetHashCode() +
                                                " where nTransactionMasterID=" + _SplitClaimDetails.TransactionMasterID + " and nTransactionID =" + _SplitClaimDetails.TransactionID + " ";
                                if (_SplitClaimDetails.UseExtSqlConnection == false)
                                { oDB.Execute_Query(_sqlQuery); }
                                else
                                {
                                    using (SqlCommand _sqlCommand = new SqlCommand())
                                    {
                                        _sqlCommand.Connection = _SplitClaimDetails.ExtSqlConnection;
                                        _sqlCommand.Transaction = _SplitClaimDetails.ExtSqlTransaction;
                                        _sqlCommand.CommandType = CommandType.Text;
                                        _sqlCommand.CommandText = _sqlQuery;
                                        _sqlCommand.ExecuteNonQuery();
                                    }
                                }
                            }
                            #endregion " Update Parent Claims "
                        }
                    }
                    else
                    {
                        #region "Create Claim Batch"

                        //_sqlQuery = " select Top 1  nTransactionID from dbo.BL_Transaction_Claim_MST " +
                        //                " where nTransactionMasterID =  " + MasterTransactionID +
                        //                " and nClaimStatus = " + ClaimStatus.Open.GetHashCode() + " order by nTransactionID desc ";

                        //_retVal = oDB.ExecuteScalar_Query(_sqlQuery);

                        //if (_retVal != null && Convert.ToInt64(_retVal) > 0)
                        //{
                        //    Int64 _nTransactionID = Convert.ToInt64(_retVal);
                        //    Transaction _oTransaction= null;
                        //    _oTransaction = GetChargesClaimDetails(_nTransactionID, nClinicID);
                        //    CreateClaimBatch(_oTransaction);
                        //}

                        #endregion "Create Claim Batch"

                        //Select Party which are changed
                        _sqlQuery = "select nNextActionPartyNumber from BL_EOB_NextAction WITH (NOLOCK) where nBillingTransactionID = " + _SplitClaimDetails.TransactionMasterID + " and nBillingTransactionDetailID in (" + _DetailIDMasterList + ") and sNextActionCode in ('T','B','R','P','N') group by nNextActionPartyNumber ";

                        if (_SplitClaimDetails.UseExtSqlConnection == false)
                        {
                            oDB.Retrive_Query(_sqlQuery, out dtNextAction);
                        }
                        else
                        {
                            using (SqlCommand _sqlCommand = new SqlCommand())
                            {
                                _sqlCommand.Connection = _SplitClaimDetails.ExtSqlConnection;
                                _sqlCommand.Transaction = _SplitClaimDetails.ExtSqlTransaction;
                                _sqlCommand.CommandType = CommandType.Text;
                                _sqlCommand.CommandText = _sqlQuery;

                                SqlDataAdapter _dataAdapter = new SqlDataAdapter(_sqlCommand);
                                DataSet _dataset = new DataSet();
                                _dataAdapter.Fill(_dataset);
                                if (_dataset.Tables[0] != null)
                                { dtNextAction = _dataset.Tables[0]; }
                                if (_dataAdapter != null) { _dataAdapter.Dispose(); }
                                if (_dataset != null) { _dataset.Dispose(); }
                            }
                        }


                        if (dtNextAction != null)
                        {

                            #region " Update Parent Claims "

                            //Close old Claim and Change Transaction Status to Insurance Paid
                            //Unhold Main Claim
                            _sqlQuery = "Update BL_Transaction_Claim_MST WITH(READPAST) set nStatus=" + TransactionStatus.InsurancePaid.GetHashCode() + ", nClaimStatus = " + ClaimStatus.Close.GetHashCode() + " where nTransactionMasterID = " + _SplitClaimDetails.TransactionMasterID + " and nTransactionID= " + _SplitClaimDetails.TransactionID + " and nStatus<>" + TransactionStatus.Rejected.GetHashCode() + "  ";
                            //_sqlQuery = "Update BL_Transaction_Claim_MST set nStatus=" + TransactionStatus.InsurancePaid.GetHashCode() + ", nClaimStatus = " + ClaimStatus.Close.GetHashCode() + " where nTransactionMasterID = " + _SplitClaimDetails.TransactionMasterID + "  and nStatus<>" + TransactionStatus.Rejected.GetHashCode();                    

                            if (_SplitClaimDetails.UseExtSqlConnection == false)
                            {
                                oDB.Execute_Query(_sqlQuery);
                            }
                            else
                            {
                                using (SqlCommand _sqlCommand = new SqlCommand())
                                {
                                    _sqlCommand.Connection = _SplitClaimDetails.ExtSqlConnection;
                                    _sqlCommand.Transaction = _SplitClaimDetails.ExtSqlTransaction;
                                    _sqlCommand.CommandType = CommandType.Text;
                                    _sqlCommand.CommandText = _sqlQuery;
                                    _sqlCommand.ExecuteNonQuery();
                                }
                            }


                            Int16 _NextAction = 0;

                            //Update bIsSplitted for sub-claim
                            _sqlQuery = "update BL_Transaction_Claim_Lines WITH(READPAST) set bIsSplitted = 1 " +
                                             " where nTransactionMasterID=" + _SplitClaimDetails.TransactionMasterID + " and nTransactionID =" + _SplitClaimDetails.TransactionID + "   and nTransactionDetailID in (" + _DetailIDList + ") ";

                            if (_SplitClaimDetails.UseExtSqlConnection == false)
                            {
                                oDB.Execute_Query(_sqlQuery);
                            }
                            else
                            {
                                using (SqlCommand _sqlCommand = new SqlCommand())
                                {
                                    _sqlCommand.Connection = _SplitClaimDetails.ExtSqlConnection;
                                    _sqlCommand.Transaction = _SplitClaimDetails.ExtSqlTransaction;
                                    _sqlCommand.CommandType = CommandType.Text;
                                    _sqlCommand.CommandText = _sqlQuery;
                                    _sqlCommand.ExecuteNonQuery();
                                }
                            }

                            #endregion " Update Parent Claims "

                            #region " Create new sub-claims "

                            //Split Claims (Create New Claims)
                            for (int i = 0; i < dtNextAction.Rows.Count; i++)
                            {

                                _NextAction = Convert.ToInt16(dtNextAction.Rows[i]["nNextActionPartyNumber"]);

                                DataTable dtNextActionCode = null;

                                _sqlQuery = "select sNextActionCode from BL_EOB_NextAction WITH (NOLOCK) where nBillingTransactionID = " + _SplitClaimDetails.TransactionMasterID + " and nBillingTransactionDetailID in (" + _DetailIDMasterList + ") and nNextActionPartyNumber=" + _NextAction + " and sNextActionCode in ('T','B','R','P','N') group by sNextActionCode ";

                                if (_SplitClaimDetails.UseExtSqlConnection == false)
                                {
                                    oDB.Retrive_Query(_sqlQuery, out dtNextActionCode);
                                }
                                else
                                {
                                    using (SqlCommand _sqlCommand = new SqlCommand())
                                    {
                                        _sqlCommand.Connection = _SplitClaimDetails.ExtSqlConnection;
                                        _sqlCommand.Transaction = _SplitClaimDetails.ExtSqlTransaction;
                                        _sqlCommand.CommandType = CommandType.Text;
                                        _sqlCommand.CommandText = _sqlQuery;

                                        SqlDataAdapter _dataAdapter = new SqlDataAdapter(_sqlCommand);
                                        DataSet _dataset = new DataSet();
                                        _dataAdapter.Fill(_dataset);
                                        if (_dataset.Tables[0] != null)
                                        { dtNextActionCode = _dataset.Tables[0]; }
                                        if (_dataAdapter != null) { _dataAdapter.Dispose(); }
                                        if (_dataset != null) { _dataset.Dispose(); }
                                    }
                                }


                                String _NextActionCode = String.Empty;

                                for (int k = 0; k < dtNextActionCode.Rows.Count; k++)
                                {
                                    string sMainClaimNo = string.Empty;
                                    _NextActionCode = Convert.ToString(dtNextActionCode.Rows[k]["sNextActionCode"]);

                                    #region " Transaction Information "

                                    Transaction oTransactionMaster = null;
                                    //Get Transaction information
                                    //Create Sub-Claim from Main Claim
                                    oTransactionMaster = ogloBilling.GetChargesClaimDetails(ref _SplitClaimDetails);

                                    if (_SplitClaimDetails.ExtTransactionErrorValue == true)
                                    {
                                        _customException = new Exception(_SplitClaimDetails.ExtTransactionErrorMsg);
                                        throw _customException;
                                    }

                                    #region " CLAIM REMITTANCE REF#"
                                    if (i == 0)
                                    {
                                        InsurancePayment.UpdateClaimRemittanceRefNo(ref _SplitClaimDetails, oTransactionMaster);
                                        if (_SplitClaimDetails.ExtTransactionErrorValue == true)
                                        {
                                            _customException = new Exception(_SplitClaimDetails.ExtTransactionErrorMsg);
                                            throw _customException;
                                        }
                                    }
                                    #endregion " CLAIM REMITTANCE REF#"



                                    if (_SplitClaimDetails.ClaimNo == 0)
                                    { _SplitClaimDetails.ClaimNo = oTransactionMaster.ClaimNo; }

                                    if (oTransactionMaster.SubClaimNo.Contains("-") == false)
                                    { sMainClaimNo = oTransactionMaster.SubClaimNo; }
                                    else
                                    { sMainClaimNo = oTransactionMaster.MainClaimNo; }
                                    oTransactionMaster.TransactionID = 0;
                                    oTransactionMaster.TransactionMasterID = _SplitClaimDetails.TransactionMasterID;
                                    //Set Parent Claim ID
                                    oTransactionMaster.ParentTransactionID = _SplitClaimDetails.TransactionID;
                                    //Set Parent Claim No                                    
                                    if (_SplitClaimDetails.SubClaimNo.Trim() != String.Empty)
                                    { oTransactionMaster.ParentClaimNo = _SplitClaimDetails.ClaimNo.ToString() + "-" + _SplitClaimDetails.SubClaimNo; }
                                    else
                                    { oTransactionMaster.ParentClaimNo = _SplitClaimDetails.ClaimNo.ToString(); }
                                    //New Sub-Claim No
                                    bool _IsSplit;
                                    oTransactionMaster.SubClaimNo = GetSubClaimNo(_SplitClaimDetails.TransactionMasterID, _SplitClaimDetails, oTransactionMaster, out _IsSplit);

                                    if (_SplitClaimDetails.ExtTransactionErrorValue == true)
                                    {
                                        _customException = new Exception(_SplitClaimDetails.ExtTransactionErrorMsg);
                                        throw _customException;
                                    }

                                    //Pending
                                    //Pending Claims


                                    //Set Next Insurance Party and Responsibility Information for Sub-Claim
                                    DataTable dtTrnDetails = null;

                                    _sqlQuery = "select nBillingTransactionDetailID,nNextActionPatientInsID,nNextActionContactID,nNextActionPartyNumber from BL_EOB_NextAction WITH (NOLOCK) where sNextActionCode ='" + _NextActionCode + "' and nNextActionPartyNumber = " + _NextAction + " and nBillingTransactionID = " + _SplitClaimDetails.TransactionMasterID + " and nBillingTransactionDetailID in (" + _DetailIDMasterList + ") order by nNextActionPartyNumber,sNextActionCode ";
                                    if (_SplitClaimDetails.UseExtSqlConnection == false)
                                    {
                                        oDB.Retrive_Query(_sqlQuery, out dtTrnDetails);
                                    }
                                    else
                                    {
                                        using (SqlCommand _sqlCommand = new SqlCommand())
                                        {
                                            _sqlCommand.Connection = _SplitClaimDetails.ExtSqlConnection;
                                            _sqlCommand.Transaction = _SplitClaimDetails.ExtSqlTransaction;
                                            _sqlCommand.CommandType = CommandType.Text;
                                            _sqlCommand.CommandText = _sqlQuery;

                                            SqlDataAdapter _dataAdapter = new SqlDataAdapter(_sqlCommand);
                                            DataSet _dataset = new DataSet();
                                            _dataAdapter.Fill(_dataset);
                                            if (_dataset.Tables[0] != null)
                                            { dtTrnDetails = _dataset.Tables[0]; }
                                            if (_dataAdapter != null) { _dataAdapter.Dispose(); }
                                            if (_dataset != null) { _dataset.Dispose(); }
                                        }
                                    }



                                    #region " Re-bill Claim "
                                    if (_NextActionCode == "R")
                                    {
                                        oTransactionMaster.IsRebill = true;
                                    }
                                    else
                                    {
                                        oTransactionMaster.IsRebill = false;
                                    }
                                    #endregion " Re-bill Claim "

                                    #endregion " Transaction Information "

                                    #region " Service Lines Information "

                                    if (dtTrnDetails != null && dtTrnDetails.Rows.Count > 0)
                                    {
                                        oTransactionMaster.InsuranceID = Convert.ToInt64(dtTrnDetails.Rows[0]["nNextActionPatientInsID"]);
                                        oTransactionMaster.ContactID = Convert.ToInt64(dtTrnDetails.Rows[0]["nNextActionContactID"]);
                                        oTransactionMaster.ResponsibilityNo = Convert.ToInt16(dtTrnDetails.Rows[0]["nNextActionPartyNumber"]);
                                        //Set Responsibility Type
                                        //Insurance ID is 0[Zero] for Self Calim
                                        if (oTransactionMaster.InsuranceID == 0)
                                        { oTransactionMaster.ResponsibilityType = PayerMode.Self; }
                                        else
                                        { oTransactionMaster.ResponsibilityType = PayerMode.Insurance; }

                                        //Set sub-claim status to open
                                        oTransactionMaster.ClaimStatus = ClaimStatus.Open;

                                        if (_SplitClaimDetails.IsPending == true)
                                        //Set Transaction Status to Pending so it can not be dispaly on ChargeTab of Batch Form
                                        { oTransactionMaster.Transaction_Status = TransactionStatus.Pending; }
                                        else
                                        {
                                            if (_NextActionCode == "N")
                                            //Set Transaction Status to None so it can not be dispaly on ChargeTab of Batch Form
                                            { oTransactionMaster.Transaction_Status = TransactionStatus.None; }
                                            else
                                            //Set Transaction Status to Queue so it can be dispaly on ChargeTab of Batch Form
                                            { oTransactionMaster.Transaction_Status = TransactionStatus.Queue; }
                                        }


                                        #region " HOLD SUB CLAIM "
                                        try
                                        {
                                            if (oTransactionMaster.Hold != null && oTransactionMaster.Hold.IsHold == true && dtHoldInfo != null)
                                            {
                                                if (i == 0)
                                                {
                                                    _sqlQuery = "Update BL_Transaction_Claim_MST WITH(READPAST) set nStatus=" + TransactionStatus.InsurancePaid.GetHashCode() + ", nClaimStatus = " + ClaimStatus.Close.GetHashCode() + " where nTransactionMasterID = " + _SplitClaimDetails.TransactionMasterID + " and nTransactionID in (select nParentTransactionID from dbo.BL_Transaction_Claim_MST WITH (NOLOCK) where nTransactionID = " + _SplitClaimDetails.TransactionID + " and nTransactionMasterID = " + _SplitClaimDetails.TransactionMasterID + ") ";

                                                    if (_SplitClaimDetails.UseExtSqlConnection == false)
                                                    {
                                                        oDB.Execute_Query(_sqlQuery);
                                                    }
                                                    else
                                                    {
                                                        using (SqlCommand _sqlCommand = new SqlCommand())
                                                        {
                                                            _sqlCommand.Connection = _SplitClaimDetails.ExtSqlConnection;
                                                            _sqlCommand.Transaction = _SplitClaimDetails.ExtSqlTransaction;
                                                            _sqlCommand.CommandType = CommandType.Text;
                                                            _sqlCommand.CommandText = _sqlQuery;
                                                            _sqlCommand.ExecuteNonQuery();
                                                        }
                                                    }
                                                }
                                                if (dtHoldInfo.Rows.Count > 1)
                                                {
                                                    DataRow[] dr = dtHoldInfo.Select("PartyNo=" + oTransactionMaster.ResponsibilityNo.ToString() + " and NextAction='" + _NextActionCode + "' and IsHold=false");
                                                    if (dr.Length != -1 && dr.GetUpperBound(0) != -1)
                                                    {
                                                        oTransactionMaster.Hold.IsHold = false;
                                                        oTransactionMaster.Hold.HoldReason = "";
                                                        oTransactionMaster.Hold = null;
                                                    }
                                                    else
                                                    {
                                                        oTransactionMaster.Hold.HoldModified = true;
                                                    }
                                                }
                                                if (dtHoldInfo.Rows.Count == 1)
                                                {
                                                    oTransactionMaster.Hold.HoldModified = true;
                                                }
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            throw ex;
                                        }
                                        #endregion " HOLD SUB CLAIM "


                                        //Remove Service line which are not part of sub-claim
                                        for (int j = oTransactionMaster.Lines.Count - 1; j >= 0; j--)
                                        {
                                            oTransactionMaster.Lines[j].IsLineSplitted = false;


                                            DataRow[] dr = dtTrnDetails.Select("nBillingTransactionDetailID = " + oTransactionMaster.Lines[j].TransactionMasterDetailID);
                                            if (dr.Length == 0)
                                            {
                                                oTransactionMaster.Lines.RemoveAt(j);
                                            }
                                            else
                                            {
                                                //Set Service Line Parent information for Sub-Claim
                                                //oTransactionMaster.Lines[j].TransactionMasterDetailID = oTransactionMaster.Lines[j].TransactionDetailID;                                        
                                                oTransactionMaster.Lines[j].ParentTransactionID = oTransactionMaster.TransactionID;
                                                oTransactionMaster.Lines[j].ParentTransactionDetailID = oTransactionMaster.Lines[j].TransactionDetailID;
                                                oTransactionMaster.Lines[j].TransactionDetailID = 0;
                                            }
                                        }
                                    }
                                    #endregion " Service Lines Information "

                                    #region " Add records to transaction tracking tables"

                                    //Add Sub-Claim to Tracking Table
                                    if (_SplitClaimDetails.UseExtSqlConnection == false)
                                    {
                                        ogloBilling.AddTransactionClaim(oTransactionMaster, _SplitClaimDetails.ClinicID);
                                    }
                                    else
                                    {
                                        oTransactionMaster.UseExtSqlConnection = _SplitClaimDetails.UseExtSqlConnection;
                                        oTransactionMaster.ExtSqlConnection = _SplitClaimDetails.ExtSqlConnection;
                                        oTransactionMaster.ExtSqlTransaction = _SplitClaimDetails.ExtSqlTransaction;
                                        ogloBilling.AddTransactionClaim(oTransactionMaster, _SplitClaimDetails.ClinicID);
                                    }

                                    #endregion " Add records to transaction tracking tables"

                                    #region " Update BL_EOB_NextAction with new Tracking ID "

                                    if (oTransactionMaster.Lines != null && oTransactionMaster.Lines.Count > 0)
                                    {
                                        for (int l = 0; l < oTransactionMaster.Lines.Count; l++)
                                        {
                                            _sqlQuery = " Update BL_EOB_NextAction WITH(READPAST) set " +
                                                            " sSubClaimNo = '" + oTransactionMaster.SubClaimNo + "'," +
                                                            "  nTrackMstTrnID= " + oTransactionMaster.TransactionID + "," +
                                                            "  nTrackMstTrnDetailID=" + oTransactionMaster.Lines[l].TransactionDetailID +
                                                            " where nBillingTransactionID = " + oTransactionMaster.TransactionMasterID +
                                                            " and nBillingTransactionDetailID = " + oTransactionMaster.Lines[l].TransactionMasterDetailID + " ";
                                            if (_SplitClaimDetails.UseExtSqlConnection == false)
                                            {
                                                oDB.Execute_Query(_sqlQuery);
                                            }
                                            else
                                            {
                                                using (SqlCommand _sqlCommand = new SqlCommand())
                                                {
                                                    _sqlCommand.Connection = _SplitClaimDetails.ExtSqlConnection;
                                                    _sqlCommand.Transaction = _SplitClaimDetails.ExtSqlTransaction;
                                                    _sqlCommand.CommandType = CommandType.Text;
                                                    _sqlCommand.CommandText = _sqlQuery;
                                                    _sqlCommand.ExecuteNonQuery();
                                                }
                                            }
                                        }
                                    }
                                    if (oTransactionMaster.TransactionID > 0)
                                    {
                                        if (oTransactionMaster.SubClaimNo.Contains("-") == true || _SplitClaimDetails.SubClaimNo.Contains("-") == true)
                                        {
                                            _sqlQuery = "update dbo.BL_Transaction_Claim_MST WITH(READPAST) set sMainClaimNo = '" + sMainClaimNo.ToString() + "',nEOBPaymentID=" + _SplitClaimDetails.EOBPaymentID.ToString() + ",nEOBID=" + _SplitClaimDetails.EOBID.ToString() + " where nTransactionID =  " + oTransactionMaster.TransactionID + " ";
                                        }
                                        else
                                        {
                                            _sqlQuery = "update dbo.BL_Transaction_Claim_MST WITH(READPAST) set sMainClaimNo = '" + _SplitClaimDetails.SubClaimNo.ToString() + "',nEOBPaymentID=" + _SplitClaimDetails.EOBPaymentID.ToString() + ",nEOBID=" + _SplitClaimDetails.EOBID.ToString() + " where nTransactionID =  " + oTransactionMaster.TransactionID + "  ";
                                        }

                                        if (_SplitClaimDetails.UseExtSqlConnection == false)
                                        {
                                            oDB.Execute_Query(_sqlQuery);
                                        }
                                        else
                                        {
                                            using (SqlCommand _sqlCommand = new SqlCommand())
                                            {
                                                _sqlCommand.Connection = _SplitClaimDetails.ExtSqlConnection;
                                                _sqlCommand.Transaction = _SplitClaimDetails.ExtSqlTransaction;
                                                _sqlCommand.CommandType = CommandType.Text;
                                                _sqlCommand.CommandText = _sqlQuery;
                                                _sqlCommand.ExecuteNonQuery();
                                            }
                                        }

                                    }
                                    #endregion " Update BL_EOB_NextAction with new Tracking ID "

                                    #region " Add records to transaction tracking history tables (5060)"
                                    //Add Sub-Claim to Tracking history Table

                                    if (_SplitClaimDetails.UseExtSqlConnection == true)
                                    {
                                        oTransactionMaster.UseExtSqlConnection = true;
                                        oTransactionMaster.ExtSqlConnection = _SplitClaimDetails.ExtSqlConnection;
                                        oTransactionMaster.ExtSqlTransaction = _SplitClaimDetails.ExtSqlTransaction;
                                    }

                                    bool _Errorflag = false;
                                    if (_SplitClaimDetails.UseExtSqlConnection == false)
                                    {
                                        ogloBilling.SaveTransactionTrackHistory(oTransactionMaster.TransactionMasterID, oTransactionMaster.TransactionID, oTransactionMaster.TransactionUserID, oTransactionMaster.TransactionUserName);
                                    }
                                    else
                                    {
                                        ogloBilling.SaveTransactionTrackHistory(oTransactionMaster.TransactionMasterID, oTransactionMaster.TransactionID, oTransactionMaster.TransactionUserID, oTransactionMaster.TransactionUserName, oTransactionMaster.ExtSqlConnection, oTransactionMaster.ExtSqlTransaction, true, out _Errorflag);
                                        if (_Errorflag == true)
                                        {
                                            _customException = new Exception("Error saving tracking history");
                                            throw _customException;
                                        }
                                    }

                                    #endregion " Add records to transaction tracking history tables"

                                }
                                dtNextActionCode.Dispose();
                                dtNextActionCode = null;
                            }

                            #endregion " Create new sub-claims "
                        }


                        _sqlQuery = "Update BL_Transaction_Claim_MST WITH(READPAST) set bIsHold=0 where nTransactionMasterID = " + _SplitClaimDetails.TransactionMasterID + " and nTransactionID= " + _SplitClaimDetails.TransactionID + " ";
                        if (_SplitClaimDetails.UseExtSqlConnection == false)
                        {
                            oDB.Execute_Query(_sqlQuery);
                        }
                        else
                        {
                            using (SqlCommand _sqlCommand = new SqlCommand())
                            {
                                _sqlCommand.Connection = _SplitClaimDetails.ExtSqlConnection;
                                _sqlCommand.Transaction = _SplitClaimDetails.ExtSqlTransaction;
                                _sqlCommand.CommandType = CommandType.Text;
                                _sqlCommand.CommandText = _sqlQuery;
                                _sqlCommand.ExecuteNonQuery();
                            }
                        }



                    }
                    oDB.Disconnect();
                    _result = true;
                }
            }
            catch (Exception ex)
            {
                _SplitClaimDetails.ExtTransactionErrorValue = true;
                _SplitClaimDetails.ExtTransactionErrorMsg = ex.ToString();
                _result = false;
                throw ex;
            }
            finally
            {
                ogloBilling.Dispose();
                oDB.Dispose();
                if (dtNextAction != null)
                {
                    dtNextAction.Dispose();
                    dtNextAction = null;
                }
            }
            return _result;
        }

        public bool SplitTransactionClaim(SplitClaimDetails _SplitClaimDetails)
        {
            bool _result = false;

            gloBilling ogloBilling = new gloBilling(_databaseconnectionstring, _databaseconnectionstring);

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            String _sqlQuery = "";
            DataTable dtNextAction = null;
         //   object _retVal = null;
            String _DetailIDMasterList = "0";
            String _DetailIDList = "0";
            _SplitClaimDetails.IsPending = false;
            try
            {
                oDB.Connect(false);

                if (_SplitClaimDetails.Lines != null && _SplitClaimDetails.Lines.Count > 0)
                {
                    for (int i = 0; i < _SplitClaimDetails.Lines.Count; i++)
                    {
                        _DetailIDMasterList += "," + _SplitClaimDetails.Lines[i].TransactionMasterDetailID;
                    }
                    for (int i = 0; i < _SplitClaimDetails.Lines.Count; i++)
                    {
                        _DetailIDList += "," + _SplitClaimDetails.Lines[i].TransactionDetailID;
                    }

                    if (_SplitClaimDetails.IsPaymentDone == false && IsSameClaim(_SplitClaimDetails) == true)
                    {
                        _sqlQuery = "select nNextActionPartyNumber from BL_EOB_NextAction WITH(NOLOCK) where nBillingTransactionID = " + _SplitClaimDetails.TransactionMasterID + " and nBillingTransactionDetailID in (" + _DetailIDMasterList + ") and sNextActionCode in ('T','B','R','P','N') group by nNextActionPartyNumber ";
                        oDB.Retrive_Query(_sqlQuery, out dtNextAction);

                        if (dtNextAction != null && dtNextAction.Rows.Count > 0)
                        {

                            #region " Update Claims "
                            String _MainClaimNo = String.Empty;
                            DataTable dtClaim = null;
                            _sqlQuery = "select nClaimNo,nSubClaimNo,isnull(nParentClaimNo,'') as nParentClaimNo, " +
                                              "  isnull(sMainClaimNo,'') as sMainClaimNo from dbo.BL_Transaction_Claim_MST WITH(NOLOCK) " +
                                              " where nTransactionMasterID=" + _SplitClaimDetails.TransactionMasterID + " and nTransactionID =" + _SplitClaimDetails.TransactionID;
                            oDB.Retrive_Query(_sqlQuery, out dtClaim);
                            if (dtClaim != null && dtClaim.Rows.Count > 0)
                            {
                                if (Convert.ToString(dtClaim.Rows[0]["nSubClaimNo"]).Contains("-") == true)
                                { _MainClaimNo = Convert.ToString(dtClaim.Rows[0]["sMainClaimNo"]); }
                                else
                                { _MainClaimNo = Convert.ToString(dtClaim.Rows[0]["nSubClaimNo"]); }
                            }
                            dtClaim = null;


                            if (_SplitClaimDetails.Lines[0].InsuranceId == 0)
                            {
                                _sqlQuery = "update BL_Transaction_Claim_MST WITH(READPAST) set sMainClaimNo = '" + _MainClaimNo + "',nInsuranceID = 0 ,nContactID=0, nResponsibilityType= " + PayerMode.Self.GetHashCode() + " ,nResponsibilityNo=" + _SplitClaimDetails.Lines[0].ResponsibilityNo.ToString() +
                                                 " where nTransactionMasterID=" + _SplitClaimDetails.TransactionMasterID + " and nTransactionID =" + _SplitClaimDetails.TransactionID;
                            }
                            else
                            {
                                _sqlQuery = "update BL_Transaction_Claim_MST WITH(READPAST) set sMainClaimNo = '" + _MainClaimNo + "',nInsuranceID = " + _SplitClaimDetails.Lines[0].InsuranceId + " ,nContactID=" + _SplitClaimDetails.Lines[0].ContactID + ", nResponsibilityType=  " + PayerMode.Insurance.GetHashCode() + ",nResponsibilityNo=" + _SplitClaimDetails.Lines[0].ResponsibilityNo.ToString() +
                                                 " where nTransactionMasterID=" + _SplitClaimDetails.TransactionMasterID + " and nTransactionID =" + _SplitClaimDetails.TransactionID;
                            }

                            oDB.Execute_Query(_sqlQuery);

                            //Pending Claims
                            if (_SplitClaimDetails.IsPending == true)
                            {
                                _sqlQuery = "update BL_Transaction_Claim_MST WITH(READPAST) set nStatus=" + TransactionStatus.Pending.GetHashCode() +
                                                " where nTransactionMasterID=" + _SplitClaimDetails.TransactionMasterID + " and nTransactionID =" + _SplitClaimDetails.TransactionID;
                                oDB.Execute_Query(_sqlQuery);
                            }
                            #endregion " Update Parent Claims "
                        }
                    }
                    else
                    {
                        #region "Create Claim Batch"

                        //_sqlQuery = " select Top 1  nTransactionID from dbo.BL_Transaction_Claim_MST " +
                        //                " where nTransactionMasterID =  " + MasterTransactionID +
                        //                " and nClaimStatus = " + ClaimStatus.Open.GetHashCode() + " order by nTransactionID desc ";

                        //_retVal = oDB.ExecuteScalar_Query(_sqlQuery);

                        //if (_retVal != null && Convert.ToInt64(_retVal) > 0)
                        //{
                        //    Int64 _nTransactionID = Convert.ToInt64(_retVal);
                        //    Transaction _oTransaction= null;
                        //    _oTransaction = GetChargesClaimDetails(_nTransactionID, nClinicID);
                        //    CreateClaimBatch(_oTransaction);
                        //}

                        #endregion "Create Claim Batch"

                        //Select Party which are changed
                        _sqlQuery = "select nNextActionPartyNumber from BL_EOB_NextAction WITH(NOLOCK) where nBillingTransactionID = " + _SplitClaimDetails.TransactionMasterID + " and nBillingTransactionDetailID in (" + _DetailIDMasterList + ") and sNextActionCode in ('T','B','R','P','N') group by nNextActionPartyNumber ";
                        oDB.Retrive_Query(_sqlQuery, out dtNextAction);

                        if (dtNextAction != null)
                        {

                            #region " Update Parent Claims "

                            //Close old Claim and Change Transaction Status to Insurance Paid
                            _sqlQuery = "Update BL_Transaction_Claim_MST WITH(READPAST) set nStatus=" + TransactionStatus.InsurancePaid.GetHashCode() + ", nClaimStatus = " + ClaimStatus.Close.GetHashCode() + " where nTransactionMasterID = " + _SplitClaimDetails.TransactionMasterID + " and nTransactionID= " + _SplitClaimDetails.TransactionID + " and nStatus<>" + TransactionStatus.Rejected.GetHashCode();
                            //_sqlQuery = "Update BL_Transaction_Claim_MST set nStatus=" + TransactionStatus.InsurancePaid.GetHashCode() + ", nClaimStatus = " + ClaimStatus.Close.GetHashCode() + " where nTransactionMasterID = " + _SplitClaimDetails.TransactionMasterID + "  and nStatus<>" + TransactionStatus.Rejected.GetHashCode();                    

                            oDB.Execute_Query(_sqlQuery);

                            Int16 _NextAction = 0;

                            //Update bIsSplitted for sub-claim
                            _sqlQuery = "update BL_Transaction_Claim_Lines WITH(READPAST) set bIsSplitted = 1 " +
                                             " where nTransactionMasterID=" + _SplitClaimDetails.TransactionMasterID + " and nTransactionID =" + _SplitClaimDetails.TransactionID + "   and nTransactionDetailID in (" + _DetailIDList + ")";
                            oDB.Execute_Query(_sqlQuery);

                            #endregion " Update Parent Claims "

                            #region " Create new sub-claims "

                            //Split Claims (Create New Claims)
                            for (int i = 0; i < dtNextAction.Rows.Count; i++)
                            {

                                _NextAction = Convert.ToInt16(dtNextAction.Rows[i]["nNextActionPartyNumber"]);

                                DataTable dtNextActionCode = null;

                                _sqlQuery = "select sNextActionCode from BL_EOB_NextAction WITH(NOLOCK) where nBillingTransactionID = " + _SplitClaimDetails.TransactionMasterID + " and nBillingTransactionDetailID in (" + _DetailIDMasterList + ") and nNextActionPartyNumber=" + _NextAction + " and sNextActionCode in ('T','B','R','P','N') group by sNextActionCode ";
                                oDB.Retrive_Query(_sqlQuery, out dtNextActionCode);

                                String _NextActionCode = String.Empty;

                                for (int k = 0; k < dtNextActionCode.Rows.Count; k++)
                                {
                                    string sMainClaimNo = string.Empty;
                                    _NextActionCode = Convert.ToString(dtNextActionCode.Rows[k]["sNextActionCode"]);

                                    #region " Transaction Information "

                                    Transaction oTransactionMaster = null;
                                    //Get Transaction information
                                    //Create Sub-Claim from Main Claim
                                    oTransactionMaster = ogloBilling.GetChargesClaimDetails(_SplitClaimDetails.TransactionID, _SplitClaimDetails.ClinicID);

                                    #region " CLAIM REMITTANCE REF#"
                                    if (i == 0)
                                    {
                                        //if (_NextActionCode == "R")
                                        //{
                                        InsurancePayment.UpdateClaimRemittanceRefNo(ref _SplitClaimDetails, oTransactionMaster);
                                        //}
                                    }
                                    #endregion " CLAIM REMITTANCE REF#"


                                    if (_SplitClaimDetails.ClaimNo == 0)
                                    { _SplitClaimDetails.ClaimNo = oTransactionMaster.ClaimNo; }

                                    if (oTransactionMaster.SubClaimNo.Contains("-") == false)
                                    { sMainClaimNo = oTransactionMaster.SubClaimNo; }
                                    else
                                    { sMainClaimNo = oTransactionMaster.MainClaimNo; }
                                    oTransactionMaster.TransactionID = 0;
                                    oTransactionMaster.TransactionMasterID = _SplitClaimDetails.TransactionMasterID;
                                    //Set Parent Claim ID
                                    oTransactionMaster.ParentTransactionID = _SplitClaimDetails.TransactionID;
                                    //Set Parent Claim No                                    
                                    if (_SplitClaimDetails.SubClaimNo.Trim() != String.Empty)
                                    { oTransactionMaster.ParentClaimNo = _SplitClaimDetails.ClaimNo.ToString() + "-" + _SplitClaimDetails.SubClaimNo; }
                                    else
                                    { oTransactionMaster.ParentClaimNo = _SplitClaimDetails.ClaimNo.ToString(); }
                                    //New Sub-Claim No
                                    bool _IsSplit;
                                    oTransactionMaster.SubClaimNo = GetSubClaimNo(_SplitClaimDetails.TransactionMasterID, _SplitClaimDetails, oTransactionMaster, out _IsSplit);

                                    //Pending
                                    //Pending Claims


                                    //Set Next Insurance Party and Responsibility Information for Sub-Claim
                                    DataTable dtTrnDetails =null;
                                    _sqlQuery = "select nBillingTransactionDetailID,nNextActionPatientInsID,nNextActionContactID,nNextActionPartyNumber from BL_EOB_NextAction WITH(NOLOCK) where sNextActionCode ='" + _NextActionCode + "' and nNextActionPartyNumber = " + _NextAction + " and nBillingTransactionID = " + _SplitClaimDetails.TransactionMasterID + " and nBillingTransactionDetailID in (" + _DetailIDMasterList + ") order by nNextActionPartyNumber,sNextActionCode ";
                                    oDB.Retrive_Query(_sqlQuery, out dtTrnDetails);


                                    #region " Re-bill Claim "
                                    if (_NextActionCode == "R")
                                    {
                                        oTransactionMaster.IsRebill = true;
                                    }
                                    else
                                    {
                                        oTransactionMaster.IsRebill = false;
                                    }
                                    #endregion " Re-bill Claim "

                                    #endregion " Transaction Information "

                                    #region " Service Lines Information "

                                    if (dtTrnDetails != null && dtTrnDetails.Rows.Count > 0)
                                    {
                                        oTransactionMaster.InsuranceID = Convert.ToInt64(dtTrnDetails.Rows[0]["nNextActionPatientInsID"]);
                                        oTransactionMaster.ContactID = Convert.ToInt64(dtTrnDetails.Rows[0]["nNextActionContactID"]);
                                        oTransactionMaster.ResponsibilityNo = Convert.ToInt16(dtTrnDetails.Rows[0]["nNextActionPartyNumber"]);
                                        //Set Responsibility Type
                                        //Insurance ID is 0[Zero] for Self Calim
                                        if (oTransactionMaster.InsuranceID == 0)
                                        { oTransactionMaster.ResponsibilityType = PayerMode.Self; }
                                        else
                                        { oTransactionMaster.ResponsibilityType = PayerMode.Insurance; }

                                        //Set sub-claim status to open
                                        oTransactionMaster.ClaimStatus = ClaimStatus.Open;

                                        if (_SplitClaimDetails.IsPending == true)
                                        //Set Transaction Status to Pending so it can not be dispaly on ChargeTab of Batch Form
                                        { oTransactionMaster.Transaction_Status = TransactionStatus.Pending; }
                                        else
                                        {
                                            if (_NextActionCode == "N")
                                            //Set Transaction Status to None so it can not be dispaly on ChargeTab of Batch Form
                                            { oTransactionMaster.Transaction_Status = TransactionStatus.None; }
                                            else
                                            //Set Transaction Status to Queue so it can be dispaly on ChargeTab of Batch Form
                                            { oTransactionMaster.Transaction_Status = TransactionStatus.Queue; }
                                        }




                                        //Remove Service line which are not part of sub-claim
                                        for (int j = oTransactionMaster.Lines.Count - 1; j >= 0; j--)
                                        {
                                            oTransactionMaster.Lines[j].IsLineSplitted = false;


                                            DataRow[] dr = dtTrnDetails.Select("nBillingTransactionDetailID = " + oTransactionMaster.Lines[j].TransactionMasterDetailID);
                                            if (dr.Length == 0)
                                            {
                                                oTransactionMaster.Lines.RemoveAt(j);
                                            }
                                            else
                                            {
                                                //Set Service Line Parent information for Sub-Claim
                                                //oTransactionMaster.Lines[j].TransactionMasterDetailID = oTransactionMaster.Lines[j].TransactionDetailID;                                        
                                                oTransactionMaster.Lines[j].ParentTransactionID = oTransactionMaster.TransactionID;
                                                oTransactionMaster.Lines[j].ParentTransactionDetailID = oTransactionMaster.Lines[j].TransactionDetailID;
                                                oTransactionMaster.Lines[j].TransactionDetailID = 0;
                                            }
                                        }
                                    }
                                    #endregion " Service Lines Information "

                                    #region " Add records to transaction tracking tables"
                                    //Add Sub-Claim to Tracking Table
                                    ogloBilling.AddTransactionClaim(oTransactionMaster, _SplitClaimDetails.ClinicID);
                                    #endregion " Add records to transaction tracking tables"



                                    #region " Update BL_EOB_NextAction with new Tracking ID "

                                    if (oTransactionMaster.Lines != null && oTransactionMaster.Lines.Count > 0)
                                    {
                                        for (int l = 0; l < oTransactionMaster.Lines.Count; l++)
                                        {
                                            _sqlQuery = " Update BL_EOB_NextAction WITH(READPAST) set " +
                                                            " sSubClaimNo = '" + oTransactionMaster.SubClaimNo + "'," +
                                                            "  nTrackMstTrnID= " + oTransactionMaster.TransactionID + "," +
                                                            "  nTrackMstTrnDetailID=" + oTransactionMaster.Lines[l].TransactionDetailID +
                                                            " where nBillingTransactionID = " + oTransactionMaster.TransactionMasterID +
                                                            " and nBillingTransactionDetailID = " + oTransactionMaster.Lines[l].TransactionMasterDetailID + "";
                                            oDB.Execute_Query(_sqlQuery);
                                        }
                                    }
                                    if (oTransactionMaster.TransactionID > 0)
                                    {
                                        if (oTransactionMaster.SubClaimNo.Contains("-") == true || _SplitClaimDetails.SubClaimNo.Contains("-") == true)
                                        {
                                            _sqlQuery = "update dbo.BL_Transaction_Claim_MST WITH(READPAST) set sMainClaimNo = '" + sMainClaimNo.ToString() + "' where nTransactionID =  " + oTransactionMaster.TransactionID;
                                        }
                                        else
                                        {
                                            _sqlQuery = "update dbo.BL_Transaction_Claim_MST WITH(READPAST) set sMainClaimNo = '" + _SplitClaimDetails.SubClaimNo.ToString() + "' where nTransactionID =  " + oTransactionMaster.TransactionID;
                                        }

                                        oDB.Execute_Query(_sqlQuery);
                                    }
                                    #endregion " Update BL_EOB_NextAction with new Tracking ID "

                                }
                                dtNextActionCode.Dispose();
                                dtNextActionCode = null;
                            }

                            #endregion " Create new sub-claims "
                        }
                    }
                    oDB.Disconnect();
                    _result = true;
                }
            }
            catch
            {
                _result = false;
            }
            finally
            {
                ogloBilling.Dispose();
                ogloBilling = null;
                oDB.Dispose();
                oDB = null;
                if (dtNextAction != null)
                {
                    dtNextAction.Dispose();
                    dtNextAction = null;
                }

            }
            return _result;
        }

        public DataTable GetSubClaims(SplitClaimDetails _SplitClaimDetails)
        {
            DataTable dtSubClaims = null;
            DataRow drSubClaims = null;
            String _DetailIDMasterList = "0";
            DataTable dtNextAction = new DataTable();
         //   String _sqlQuery = "";
            try
            {
                for (int i = 0; i < _SplitClaimDetails.Lines.Count; i++)
                { _DetailIDMasterList += "," + _SplitClaimDetails.Lines[i].TransactionMasterDetailID; }
                dtSubClaims = new DataTable("SplitDetails");
                dtSubClaims.Columns.Add(new DataColumn("IsHold", System.Type.GetType("System.Boolean")));
                dtSubClaims.Columns.Add(new DataColumn("SubClaimNo", System.Type.GetType("System.String")));
                dtSubClaims.Columns.Add(new DataColumn("PartyNo", System.Type.GetType("System.Int32")));
                dtSubClaims.Columns.Add(new DataColumn("NextAction", System.Type.GetType("System.String")));
                dtSubClaims.Columns.Add(new DataColumn("Party", System.Type.GetType("System.String")));

                dtNextAction = _SplitClaimDetails.GetSubclaims();

                DataRow[] dr = dtNextAction.Select("", "PartyNo,NextAction");
                string subclaimno = GetSubClaimNo(_SplitClaimDetails.TransactionMasterID);
                if (dtNextAction != null)
                {
                    for (int i = 0; i <= dr.GetUpperBound(0); i++)
                    {
                        drSubClaims = dtSubClaims.NewRow();
                        drSubClaims["IsHold"] = false;
                        drSubClaims["SubClaimNo"] = _SplitClaimDetails.ClaimNo.ToString("00000") + "-" + subclaimno;
                        drSubClaims["PartyNo"] = Convert.ToInt32(dr[i]["PartyNo"]);

                        //string _next = Convert.ToString(dr[i]["NextAction"]);

                        //if (_next.Equals("R"))
                        //{ drSubClaims["NextAction"] = "Rebilling"; }
                        //else if (_next.Equals("B"))
                        //{ drSubClaims["NextAction"] = "Billing"; }
                        //else if (_next.Equals("P"))
                        //{ drSubClaims["NextAction"] = "Pending"; }
                        //else if (_next.Equals("N"))
                        //{ drSubClaims["NextAction"] = "None"; }

                        drSubClaims["NextAction"] = Convert.ToString(dr[i]["NextAction"]);
                        drSubClaims["Party"] = Convert.ToString(dr[i]["Party"]);
                        dtSubClaims.Rows.Add(drSubClaims);

                        Int32 _subClaim = 0;
                        if (Int32.TryParse(subclaimno, out _subClaim) == true)
                        { subclaimno = Convert.ToString(_subClaim + 1); }
                    }
                }
                dtNextAction = null;

            }
            catch (Exception)
            {


            }
            finally
            {

            }
            return dtSubClaims;
        }

        private String GetSubClaimNo(Int64 TransactionMasterID, SplitClaimDetails _SplitClaimDetails, Transaction oTransactionMaster, out bool IsSplit)
        {

            #region " Declaration "

            bool _IsSameClaim = false;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            String _sqlQuery = String.Empty;
            Object _val = null;
            String _result = String.Empty;

            int _cntRebill = 0;
            int _cntBill = 0;
            int _cntPending = 0;
            int _cntNone = 0;
            int _PartyNo = 0;

            #endregion " Declaration "

            #region " Check New Sub Claim No is required or No "
            //If user selects same action and same party for all service lines then don’t create new sub-claim no. 
            if (_SplitClaimDetails.Lines.Count > 0)
            {
                _PartyNo = _SplitClaimDetails.Lines[0].ResponsibilityNo;

                if (_SplitClaimDetails.Lines.Count == oTransactionMaster.Lines.Count)
                {
                    for (int i = 0; i < _SplitClaimDetails.Lines.Count; i++)
                    {
                        #region " check insurance party "
                        if (_PartyNo != _SplitClaimDetails.Lines[i].ResponsibilityNo)
                        {
                            _cntRebill = -1;
                            _cntBill = -1;
                            _cntPending = -1;
                            _cntNone = -1;
                            break;
                        }
                        #endregion " check insurance party "

                        if (_SplitClaimDetails.Lines[i].NextActionCode.ToUpper() == "B")  //Billed
                        { _cntBill = _cntBill + 1; }
                        else if (_SplitClaimDetails.Lines[i].NextActionCode.ToUpper() == "R") //Re-bill
                        { _cntRebill = _cntRebill + 1; }
                        else if (_SplitClaimDetails.Lines[i].NextActionCode.ToUpper() == "P") //Pending
                        { _cntPending = _cntPending + 1; }
                        else if (_SplitClaimDetails.Lines[i].NextActionCode.ToUpper() == "N") //None
                        { _cntNone = _cntNone + 1; }
                    }
                    if (_cntBill == oTransactionMaster.Lines.Count || _cntRebill == oTransactionMaster.Lines.Count || _cntPending == oTransactionMaster.Lines.Count || _cntNone == oTransactionMaster.Lines.Count)
                    {
                        _IsSameClaim = true;
                        if (_cntPending == oTransactionMaster.Lines.Count)
                        { _SplitClaimDetails.IsPending = true; }
                    }
                }
            }
            #endregion " Check New Sub Claim No is required or No "

            #region "Get Sub-Claim No"
            try
            {
                oDB.Connect(false);

                if (_IsSameClaim == false)
                {
                    #region " New Sub Clim No "
                    _sqlQuery = "Select isnull(MAX(Convert(Numeric(18,0),(case when ISNULL(nSubClaimNo,'')='' then '0' else nSubClaimNo end))),0)+1 from BL_Transaction_Claim_MST WITH (NOLOCK) where nTransactionMasterId=" + TransactionMasterID + " ";

                    if (_SplitClaimDetails.UseExtSqlConnection == false)
                    {
                        _val = oDB.ExecuteScalar_Query(_sqlQuery);
                    }
                    else
                    {
                        using (SqlCommand _sqlCommand = new SqlCommand())
                        {
                            _sqlCommand.Connection = _SplitClaimDetails.ExtSqlConnection;
                            _sqlCommand.Transaction = _SplitClaimDetails.ExtSqlTransaction;
                            _sqlCommand.CommandType = CommandType.Text;
                            _sqlCommand.CommandText = _sqlQuery;
                            _val = _sqlCommand.ExecuteScalar();
                        }
                    }

                    if (_val != null && Convert.ToString(_val).Trim() != "")
                    {
                        _result = Convert.ToString(_val);
                    }
                    #endregion " New Sub Clim No "
                }
                else
                {
                    #region " Same Clim No "
                    _sqlQuery = "Select isnull(MIN(Convert(Numeric(18,0),(case when ISNULL(nSubClaimNo,'')='' then '0' else nSubClaimNo end))),0)-1 from BL_Transaction_Claim_MST WITH (NOLOCK) where nTransactionMasterId=" + TransactionMasterID + " ";

                    if (_SplitClaimDetails.UseExtSqlConnection == false)
                    {
                        _val = oDB.ExecuteScalar_Query(_sqlQuery);
                    }
                    else
                    {
                        using (SqlCommand _sqlCommand = new SqlCommand())
                        {
                            _sqlCommand.Connection = _SplitClaimDetails.ExtSqlConnection;
                            _sqlCommand.Transaction = _SplitClaimDetails.ExtSqlTransaction;
                            _sqlCommand.CommandType = CommandType.Text;
                            _sqlCommand.CommandText = _sqlQuery;
                            _val = _sqlCommand.ExecuteScalar();
                        }
                    }

                    if (_val != null && Convert.ToString(_val).Trim() != "")
                    {
                        _result = Convert.ToString(_val);
                    }
                    #endregion " Same Clim No "
                }
                oDB.Disconnect();
            }
            catch (Exception ex)
            {
                _result = String.Empty;
                _SplitClaimDetails.ExtTransactionErrorValue = true;
                _SplitClaimDetails.ExtTransactionErrorMsg = ex.ToString();
                throw ex;
            }
            finally
            {
                oDB.Dispose();
            }
            #endregion "Get Sub-Claim No"
            IsSplit = _IsSameClaim;
            return _result;
        }

        private bool IsSameClaim(SplitClaimDetails _SplitClaimDetails)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            bool _IsSameClaim = false;
            int _cntRebill = 0;
            int _cntBill = 0;
            int _cntPending = 0;
            int _cntNone = 0;
            int _PartyNo = 0;
            String _sqlQuery = String.Empty;
            Object _val = null;
            int cntLines = 0;

            try
            {
                _sqlQuery = "Select count(nTransactionDetailID) from BL_Transaction_Claim_Lines WITH (NOLOCK) where nTransactionMasterID =" + _SplitClaimDetails.TransactionMasterID + " and nTransactionID = " + _SplitClaimDetails.TransactionID + " ";

                if (_SplitClaimDetails.UseExtSqlConnection == false)
                {
                    oDB.Connect(false);
                    _val = oDB.ExecuteScalar_Query(_sqlQuery);
                }
                else
                {
                    using (SqlCommand _sqlCommand = new SqlCommand())
                    {
                        _sqlCommand.Connection = _SplitClaimDetails.ExtSqlConnection;
                        _sqlCommand.Transaction = _SplitClaimDetails.ExtSqlTransaction;
                        _sqlCommand.CommandType = CommandType.Text;
                        _sqlCommand.CommandText = _sqlQuery;
                        _val = _sqlCommand.ExecuteScalar();
                    }
                }

                if (_val != null && Convert.ToString(_val).Trim() != "")
                {
                    cntLines = Convert.ToInt32(_val);
                }
                oDB.Disconnect();

                #region " Check New Sub Claim No is required or No "
                //If user selects same action and same party for all service lines then don’t create new sub-claim no. 
                if (_SplitClaimDetails.Lines.Count > 0)
                {
                    _PartyNo = _SplitClaimDetails.Lines[0].ResponsibilityNo;

                    if (_SplitClaimDetails.Lines.Count == cntLines)
                    {
                        for (int i = 0; i < _SplitClaimDetails.Lines.Count; i++)
                        {
                            #region " check insurance party "
                            if (_PartyNo != _SplitClaimDetails.Lines[0].ResponsibilityNo)
                            {
                                _cntRebill = -1;
                                _cntBill = -1;
                                _cntPending = -1;
                                _cntNone = -1;
                                break;
                            }
                            #endregion " check insurance party "

                            if (_SplitClaimDetails.Lines[i].NextActionCode.ToUpper() == "B")  //Billed
                            { _cntBill = _cntBill + 1; }
                            else if (_SplitClaimDetails.Lines[i].NextActionCode.ToUpper() == "R") //Re-bill
                            { _cntRebill = _cntRebill + 1; }
                            else if (_SplitClaimDetails.Lines[i].NextActionCode.ToUpper() == "P") //Pending
                            { _cntPending = _cntPending + 1; }
                            else if (_SplitClaimDetails.Lines[i].NextActionCode.ToUpper() == "N") //None
                            { _cntNone = _cntNone + 1; }
                        }
                        if (_cntPending == cntLines)   //_cntBill == cntLines || _cntRebill == cntLines ||
                        {
                            _IsSameClaim = true;
                            if (_cntPending == cntLines)
                            { _SplitClaimDetails.IsPending = true; }
                        }
                    }
                }
                #endregion " Check New Sub Claim No is required or No "
            }
            catch (Exception ex)
            {
                //gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                throw ex;
            }

            return _IsSameClaim;
        }

        private String GetSubClaimNo(Int64 TransactionMasterID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            String _sqlQuery = "Select isnull(MAX(Convert(Numeric(18,0),(case when ISNULL(nSubClaimNo,'')='' then '0' else nSubClaimNo end))),0)+1 from BL_Transaction_Claim_MST WITH(NOLOCK) where nTransactionMasterId=" + TransactionMasterID + "";
            Object _val = null;
            String _result = String.Empty;
            try
            {
                oDB.Connect(false);

                _val = oDB.ExecuteScalar_Query(_sqlQuery);

                if (_val != null && Convert.ToString(_val).Trim() != "")
                {
                    _result = Convert.ToString(_val);
                }
                oDB.Disconnect();
            }
            catch
            {
                _result = String.Empty;
            }
            finally
            {
                oDB.Dispose();
            }
            return _result;
        }

        #endregion "Public Methods "
    }

}

