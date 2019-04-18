using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SqlServer.Server;
using System.Data;
using System.Collections;

namespace gloAuditTrail
{
    //public class ClsPAccountLog
    //{

    //    public ClsPAccountLog()
    //    {

    //    }

    //    private Int64 _nID;

    //    public Int64 nID
    //    {
    //        get { return _nID; }
    //        set { _nID = value; }
    //    }

    //    private Int64 _nPAccountID;
                    
    //    public Int64 nPAccountID
    //    {
    //        get { return _nPAccountID; }
    //        set { _nPAccountID = value; }
    //    }

    //    private Int64 _nAccountPatientID;

    //    public Int64 AccountPatientID
    //    {
    //        get { return _nAccountPatientID; }
    //        set { _nAccountPatientID = value; }
    //    }


    //    private DateTime  _dtCloseDate;

    //    public DateTime  dtCloseDate
    //    {
    //        get { return _dtCloseDate; }
    //        set { _dtCloseDate = value; }
    //    }

    //    private string _sLogType;

    //    public string sLogType
    //    {
    //        get { return _sLogType; }
    //        set { _sLogType = value; }
    //    }

    //    private string _sLog;

    //    public string sLog
    //    {
    //        get { return _sLog; }
    //        set { _sLog = value; }
    //    }

    //    private decimal  _dAmount;

    //    public decimal  dAmount
    //    {
    //        get { return _dAmount; }
    //        set { _dAmount = value; }
    //    }


    //    private decimal _dBalance;

    //    public decimal dBalance
    //    {
    //        get { return _dBalance; }
    //        set { _dBalance = value; }
    //    }


    //    private decimal _dPatDue;

    //    public decimal dPatDue
    //    {
    //        get { return _dPatDue; }
    //        set { _dPatDue = value; }
    //    }


    //    private Int64 _nUserID;

    //    public Int64 nUserID
    //    {
    //        get { return _nUserID; }
    //        set { _nUserID = value; }
    //    }

    //    private string _sUserName;

    //    public string sUserName
    //    {
    //        get { return _sUserName; }
    //        set { _sUserName = value; }
    //    }

    //    private DateTime _dtDateTime;

    //    public DateTime dtDateTime
    //    {
    //        get { return _dtDateTime; }
    //        set { _dtDateTime = value; }
    //    }

    //    private Int32  _nClaimNo;

    //    public Int32 nClaimNo
    //    {
    //        get { return _nClaimNo; }
    //        set { _nClaimNo = value; }
    //    }

    //    private Int64 _nMstTransactionId;

    //    public Int64 nMstTransactionId
    //    {
    //        get { return _nMstTransactionId; }
    //        set { _nMstTransactionId = value; }
    //    }

    //    private Int64 _nMstTransactionDtlId;

    //    public Int64 nMstTransactionDtlId
    //    {
    //        get { return _nMstTransactionDtlId; }
    //        set { _nMstTransactionDtlId = value; }
    //    }

    //    private Int64 _nTrackTransactionId;

    //    public Int64 nTrackTransactionId
    //    {
    //        get { return _nTrackTransactionId; }
    //        set { _nTrackTransactionId = value; }
    //    }

    //    private Int64 _nTrackTransactionDtlId;

    //    public Int64 nTrackTransactionDtlId
    //    {
    //        get { return _nTrackTransactionDtlId; }
    //        set { _nTrackTransactionDtlId = value; }

    //    }

    //    private Int64 _nCreditId;

    //    public Int64 nCreditId
    //    {
    //        get { return _nCreditId; }
    //        set { _nCreditId = value; }
    //    }


    //    private Int64 _nDebitId;

    //    public Int64 nDebitId
    //    {
    //        get { return _nDebitId; }
    //        set { _nDebitId = value; }
    //    }

    //    private Int64 _nEobId;

    //    public Int64 nEobId
    //    {
    //        get { return _nEobId; }
    //        set { _nEobId = value; }
    //    }

    //    private Int64 _nEobDtlId;

    //    public Int64 nEobDtlId
    //    {
    //        get { return _nEobDtlId; }
    //        set { _nEobDtlId = value; }
    //    }

    //    private Int64 _nCreditDtlId;

    //    public Int64 nCreditDtlId
    //    {
    //        get { return _nCreditDtlId; }
    //        set { _nCreditDtlId = value; }

    //    }

    //    private Int64 _nReserveId;

    //    public Int64 nReserveId
    //    {
    //        get { return _nReserveId; }
    //        set { _nReserveId = value; }
    //    }

    //    private Int64 _nRefundID;

    //    public Int64 nRefundID
    //    {
    //        get { return _nRefundID; }
    //        set { _nRefundID = value; }
    //    }

    //    private Int64 _nBatchId;

    //    public Int64 nBatchId
    //    {
    //        get { return _nBatchId; }
    //        set { _nBatchId = value; }
    //    }

    //    private Int64 _nNoteID;

    //    public Int64 nNoteID
    //    {
    //        get { return _nNoteID; }
    //        set { _nNoteID = value; }
    //    }

    //    private Int64 _nVoidNoteID;

    //    public Int64 nVoidNoteID
    //    {
    //        get { return _nVoidNoteID; }
    //        set { _nVoidNoteID = value; }

    //    }

    //    private Int64 _nInsuranceId;

    //    public Int64 nInsuranceId
    //    {
    //        get { return _nInsuranceId; }
    //        set { _nInsuranceId = value; }
    //    }

    //}
    
    
    
    public class ClsPACCOUNTLOG
    {
        public Int64 nID;
        public Int64 nPAccountID;
        public Int64 nAccountPatientID;
        public DateTime dtCloseDate;
        public Int32 nLogTypeId;
        public String sLogType;
        public String sLog;
        public Decimal dAmount;
        public Decimal dBalance;
        public Decimal dPatDue;
        public Int64 nUserID;
        public String sUserName;
        public DateTime dtDateTime;
        public Int64 nClaimNo;
        public String sActivityBy;
        public Int64 nMstTransactionId;
        public Int64 nMstTransactionDtlId;
        public Int64 nTrackTransactionId;
        public Int64 nTrackTransactionDtlId;
        public Int64 nCreditId;
        public Int64 nDebitId;
        public Int64 nEobId;
        public Int64 nEobDtlId;
        public Int64 nCreditDtlId;
        public Int64 nReserveId;
        public Int64 nRefundID;
        public Int64 nBatchId;
        public Int64 nNoteID;
        public Int64 nVoidNoteID;
        public Int64 nInsuranceId;

    }

    public class ClsPACCOUNTLOGCollection : List<ClsPACCOUNTLOG>, IEnumerable<SqlDataRecord>
    {
        public string _DatabaseConnection="";
        public ClsPACCOUNTLOGCollection(string DatabaseConnection)
        {
            _DatabaseConnection = DatabaseConnection;
        }

        IEnumerator<SqlDataRecord> IEnumerable<SqlDataRecord>.GetEnumerator()
        {
        var sdr = new SqlDataRecord(
        new SqlMetaData("nID", SqlDbType.BigInt),
        new SqlMetaData("nPAccountID", SqlDbType.BigInt),
        new SqlMetaData("nAccountPatientID", SqlDbType.BigInt),
        new SqlMetaData("dtCloseDate", SqlDbType.Date),
        new SqlMetaData("nLogTypeId", SqlDbType.Int),
        new SqlMetaData("sLogType", SqlDbType.VarChar,50),
        new SqlMetaData("sLog", SqlDbType.VarChar,8000),
        new SqlMetaData("dAmount", SqlDbType.Decimal),
        new SqlMetaData("dBalance", SqlDbType.Decimal),
        new SqlMetaData("dPatDue", SqlDbType.Decimal),
        new SqlMetaData("nUserID", SqlDbType.BigInt),
        new SqlMetaData("sUserName", SqlDbType.VarChar,50),
        new SqlMetaData("dtDateTime", SqlDbType.DateTime),
        new SqlMetaData("nClaimNo", SqlDbType.BigInt),
        new SqlMetaData("sActivityBy", SqlDbType.VarChar,50),
        new SqlMetaData("nMstTransactionId", SqlDbType.BigInt),
        new SqlMetaData("nMstTransactionDtlId", SqlDbType.BigInt),
        new SqlMetaData("nTrackTransactionId", SqlDbType.BigInt),
        new SqlMetaData("nTrackTransactionDtlId", SqlDbType.BigInt),
        new SqlMetaData("nCreditId", SqlDbType.BigInt),
        new SqlMetaData("nDebitId", SqlDbType.BigInt),
        new SqlMetaData("nEobId", SqlDbType.BigInt),
        new SqlMetaData("nEobDtlId", SqlDbType.BigInt),
        new SqlMetaData("nCreditDtlId", SqlDbType.BigInt),
        new SqlMetaData("nReserveId", SqlDbType.BigInt),
        new SqlMetaData("nRefundID", SqlDbType.BigInt),
        new SqlMetaData("nBatchId", SqlDbType.BigInt),
        new SqlMetaData("nNoteID", SqlDbType.BigInt),
        new SqlMetaData("nVoidNoteID", SqlDbType.BigInt),
        new SqlMetaData("nInsuranceId", SqlDbType.BigInt));

            foreach (ClsPACCOUNTLOG cpt in this)
            {
                sdr.SetInt64(0, cpt.nID);
                sdr.SetInt64(1, cpt.nPAccountID);
                sdr.SetInt64(2, cpt.nAccountPatientID);
                sdr.SetDateTime(3, cpt.dtCloseDate);
                sdr.SetInt32(4, cpt.nLogTypeId);
                sdr.SetString(5, cpt.sLogType);
                sdr.SetString(6, cpt.sLog);
                sdr.SetDecimal(7, cpt.dAmount);
                sdr.SetDecimal(8, cpt.dBalance);
                sdr.SetDecimal(9, cpt.dPatDue);
                sdr.SetInt64(10, cpt.nUserID);
                sdr.SetString(11, cpt.sUserName);
                sdr.SetDateTime(12, cpt.dtDateTime);
                sdr.SetInt64(13, cpt.nClaimNo);
                sdr.SetString(14, cpt.sActivityBy);
                sdr.SetInt64(15, cpt.nMstTransactionId);
                sdr.SetInt64(16, cpt.nMstTransactionDtlId);
                sdr.SetInt64(17, cpt.nTrackTransactionId);
                sdr.SetInt64(18, cpt.nTrackTransactionDtlId);
                sdr.SetInt64(19, cpt.nCreditId);
                sdr.SetInt64(20, cpt.nDebitId);
                sdr.SetInt64(21, cpt.nEobId);
                sdr.SetInt64(22, cpt.nEobDtlId);
                sdr.SetInt64(23, cpt.nCreditDtlId);
                sdr.SetInt64(24, cpt.nReserveId);
                sdr.SetInt64(25, cpt.nRefundID);
                sdr.SetInt64(26, cpt.nBatchId);
                sdr.SetInt64(27, cpt.nNoteID);
                sdr.SetInt64(28, cpt.nVoidNoteID);
                sdr.SetInt64(29, cpt.nInsuranceId);
                yield return sdr;
            }
        }


        public bool SAVE_PA_Account_Log(ClsPACCOUNTLOGCollection ClsPACCOUNTLOGCollection)
        {
            Int64 _nId = 0;
            bool _result = false;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_DatabaseConnection);
            gloDatabaseLayer.DBParameters oParameters = null;
            try
            {
                oParameters = new gloDatabaseLayer.DBParameters();
                oParameters.Add("@PA_ACCOUNT_LOG", ClsPACCOUNTLOGCollection, ParameterDirection.Input, SqlDbType.Structured);
                oParameters.Add("@nID", 0, ParameterDirection.Output, SqlDbType.BigInt);
                oDB.Connect(false);
                Hashtable oResult = oDB.Execute("ADD_PA_Account_Log", oParameters, true);
                _nId =Convert.ToInt64(oResult["@nID"]);
                oResult.Clear();
                oResult = null;
                if (_nId > 0)
                    _result = true;
                else
                    _result = false;
               
            }
            catch (Exception ex)
            {                
                gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); }
                if (oParameters != null) { oParameters.Dispose(); }
            }
            return _result;
        }

    }

    
}
