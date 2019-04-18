using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace gloBilling.gloERA
{
    #region " Enumeration "
    public enum enum_CASReasonType
    {
        None = 0,
        Coins = 1,
        Copay = 2,
        Deduct = 3,
        PrevPaid = 4,
        WH = 5,
        WO = 6,
        Other = 7
    }

    public enum enum_PayAction
    {
        None = 0,
        PostTransfer = 1,
        PostNoTransfer = 2,
        StopPost = 3,
        Denial = 4
    }
    #endregion

    public class ERAPayer : IDisposable
    {
        #region " Constructor & Destructor "

        private bool disposed = false;

        public ERAPayer()
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

                }
            }
            disposed = true;
        }

        ~ERAPayer()
        {
            Dispose(false);
        }

        #endregion

        #region " Variable Declaration "
        
        private string _DataBaseConnectionString = "";
        private Int64 _ClinicID = 1;
        private Int64 _UserID = 0;
        private string _MessageBoxCaption = "";

        private gloDatabaseLayer.DBLayer oDB = null;
        private gloDatabaseLayer.DBParameters oDBPara = null;
        string _TempString = "";
      //  Int64 _TempID = 0;
        #endregion

        #region " Properties "
        public Int64 SettingID { get; set; }
        public string PayerID { get; set; }
        public bool IsActive { get; set; }
        public bool UseClaimStatus { get; set; }
        public bool PostSecondaryAdjust { get; set; }
        public enum_PayAction ZeroPaidBilled { get; set; }
        public enum_PayAction ZeroPaidNotBilled { get; set; }
        public enum_PayAction PaidNotZero { get; set; }
        private List<CASLine> _CASLines = new List<CASLine>();
        public List<CASLine> CASLines { get { return _CASLines; } set { CASLines = _CASLines; } }       
        #endregion

        #region " Public Methods "

        public DataTable GetERAPayerList()
        {
            DataTable _dt = null;
            try
            {
                if (OpenConnection(true))
                {
                    oDBPara.Add("@nClinicID", _ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                    oDB.Retrive("ERA_GetPayerList", oDBPara, out _dt);
                    CloseConnection();
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            return _dt;
        }

        public DataTable GetDefaultCASCodes()
        {
            DataTable _dt = null;
            try
            {
                if (OpenConnection(true))
                {
                    oDBPara.Clear();
                    oDBPara.Add("@nClinicID", _ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                    oDB.Retrive("ERA_GetDefaultCASCodes", oDBPara, out _dt);
                    CloseConnection();
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            return _dt;
        }

        public Int64 SavePayer()
        {
            if (CASLines.Count == 0)
                return 0;

            Object oResult = null;
            Int64 _ResultID = 0;
            try
            {
                if (OpenConnection(true))
                {
                    oDBPara.Clear();
                    oDBPara.Add("@nPayerSettingID", SettingID, ParameterDirection.InputOutput, SqlDbType.BigInt);
                    oDBPara.Add("@sERAPayerID", PayerID, ParameterDirection.Input, SqlDbType.VarChar);
                    oDBPara.Add("@bUseClaimStatus", UseClaimStatus, ParameterDirection.Input, SqlDbType.Bit);
                    oDBPara.Add("@bPostSecondaryAdjs", PostSecondaryAdjust, ParameterDirection.Input, SqlDbType.Bit);
                    oDBPara.Add("@bIsActivated", IsActive, ParameterDirection.Input, SqlDbType.Bit);
                    oDBPara.Add("@nZeroPaidBilled", ZeroPaidBilled.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                    oDBPara.Add("@nZeroPaidNotBilled", ZeroPaidNotBilled.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                    oDBPara.Add("@nPaidNotZero", PaidNotZero.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                    oDBPara.Add("@nClinicId", _ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                    oDBPara.Add("@nUserId", _UserID, ParameterDirection.Input, SqlDbType.BigInt);
                    oDB.Execute("ERA_INUP_PAYER", oDBPara, out oResult);

                    if (oResult != null && oResult.ToString() != "")
                        _ResultID = Convert.ToInt64(oResult);
                    else
                        return 0;

                    for (int i = 0; i < CASLines.Count; i++)
                    {
                        oDBPara.Clear();
                        oDBPara.Add("@nPayerSettingID", _ResultID, ParameterDirection.Input, SqlDbType.BigInt);
                        oDBPara.Add("@sERAPayerID", PayerID, ParameterDirection.Input, SqlDbType.VarChar);
                        oDBPara.Add("@sGroupCode", CASLines[i].GroupCode, ParameterDirection.Input, SqlDbType.VarChar);
                        oDBPara.Add("@sReasonCode", CASLines[i].ReasonCode, ParameterDirection.Input, SqlDbType.VarChar);
                        oDBPara.Add("@nCASType", CASLines[i].CASReasonType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                        oDBPara.Add("@nPaidAction", CASLines[i].PaidAction.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                        oDBPara.Add("@nZeroPaidAction", CASLines[i].ZeroPaidAction.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                        oDBPara.Add("@bIsDefault", CASLines[i].IsDefault, ParameterDirection.Input, SqlDbType.Bit);
                        oDBPara.Add("@nClinicId", _ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                        oDBPara.Add("@nUserId", _UserID, ParameterDirection.Input, SqlDbType.BigInt);
                        oDB.Execute("ERA_IN_PayerReasonCodes", oDBPara);
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
            }
            return _ResultID;
        }

        public bool DeletePayer(Int64 nSettingID)
        {
            bool _Result = false;
            try
            {
                if (OpenConnection(true))
                {
                    oDBPara.Clear();
                    oDBPara.Add("@nSettingID", nSettingID, ParameterDirection.Input, SqlDbType.BigInt);
                    oDBPara.Add("@nUserID", _UserID, ParameterDirection.Input, SqlDbType.BigInt);
                    oDB.Execute("ERA_DeletePayer", oDBPara);
                    CloseConnection();
                }
                _Result = true;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            return _Result;
        }

        public ERAPayer GetPayer(Int64 nSettingID)
        {
            DataTable _dt;
            CASLine _CASLine = null;
            try
            {
                if (OpenConnection(true))
                {
                    oDBPara.Clear();
                    oDBPara.Add("@nSettingID", nSettingID, ParameterDirection.Input, SqlDbType.BigInt);
                    oDBPara.Add("@nClinicID", _ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                    oDB.Retrive("ERA_GetPayer", oDBPara, out _dt);
                    if (_dt != null && _dt.Rows.Count > 0)
                    {
                        this.PayerID = _dt.Rows[0]["sERAPayerID"].ToString();
                        this.IsActive = Convert.ToBoolean(_dt.Rows[0]["bIsActivated"]);
                        this.UseClaimStatus = Convert.ToBoolean(_dt.Rows[0]["bUseClaimStatus"]);
                        this.PostSecondaryAdjust = Convert.ToBoolean(_dt.Rows[0]["bPostSecondaryAdjs"]);
                        this.ZeroPaidBilled = (enum_PayAction)Convert.ToInt16(_dt.Rows[0]["nZeroPaidBilled"]);
                        this.ZeroPaidNotBilled = (enum_PayAction)Convert.ToInt16(_dt.Rows[0]["nZeroPaidNotBilled"]);
                        this.PaidNotZero = (enum_PayAction)Convert.ToInt16(_dt.Rows[0]["nPaidNotZero"]);


                        _dt.Dispose();
                        _dt = null;
                        oDB.Retrive("ERA_GetPayerCAS", oDBPara, out _dt);
                        if (_dt != null && _dt.Rows.Count > 0)
                        {
                            for (int iRow = 0; iRow < _dt.Rows.Count; iRow++)
                            {
                                _CASLine = new CASLine();
                                _CASLine.CASID = Convert.ToInt64(_dt.Rows[iRow]["nReasonCodeID"].ToString());
                                _CASLine.GroupCode = _dt.Rows[iRow]["sGroupCode"].ToString();
                                _CASLine.ReasonCode = _dt.Rows[iRow]["sReasonCode"].ToString();
                                _CASLine.CASReasonType = (enum_CASReasonType)Convert.ToInt16(_dt.Rows[iRow]["nCASType"]);
                                _CASLine.PaidAction = (enum_PayAction)Convert.ToInt16(_dt.Rows[iRow]["nPaidAction"]);
                                _CASLine.ZeroPaidAction = (enum_PayAction)Convert.ToInt16(_dt.Rows[iRow]["nZeroPaidAction"]);
                                _CASLine.IsDefault = Convert.ToBoolean(_dt.Rows[iRow]["bIsDefault"].ToString());
                                this.CASLines.Add(_CASLine);
                            }
                        }
                        
                    }

                    CloseConnection();
                }
                
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            return this;
        }

        public bool IsPayerPresent(string sPayerID, Int64 nSettingID)
        {
            bool _Result = false;
            Object oResult = null;
            try
            {
                if(OpenConnection(false))
                {
                    _TempString = "SELECT nPayerSettingID FROM ERA_Payer WHERE sERAPayerID = '" + sPayerID.Replace("'","''") + "' AND nPayerSettingID <> " + nSettingID;
                    oResult = oDB.ExecuteScalar_Query(_TempString);
                    CloseConnection();
                    if (oResult != null && oResult.ToString() != "")
                        if (Convert.ToInt64(oResult) > 0)
                            return true;                    
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            return _Result;
        }

        public bool IsReasonCodePresent(string _sReasonCode, string _sGroupCode)
        {
            bool _Result = false;
            DataTable _dt = null;
            try
            {
                if (OpenConnection(true))
                {
                    oDBPara.Clear();
                    oDBPara.Add("@sReasonCode", _sReasonCode, ParameterDirection.Input, SqlDbType.VarChar);
                    oDBPara.Add("@sReasonGroupCode", _sGroupCode, ParameterDirection.Input, SqlDbType.VarChar);
                    oDBPara.Add("@nClinicID", _ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                    oDB.Retrive("ERA_GetReasonCode", oDBPara, out _dt);
                    CloseConnection();
                
                    if (_dt != null && _dt.Rows.Count > 0)
                            _Result= true;
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

        #endregion
                
    }

    public class CASLine
    {
        #region " Properties "
        public Int64 CASID { get; set; }
        public string GroupCode { get; set; }
        public string ReasonCode { get; set; }
        public enum_CASReasonType CASReasonType { get; set; }
        public enum_PayAction PaidAction { get; set; }
        public enum_PayAction ZeroPaidAction { get; set; }
        public bool IsDefault { get; set; }
        #endregion 
    }
}
