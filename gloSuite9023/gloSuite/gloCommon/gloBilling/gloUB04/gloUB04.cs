using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace gloBilling
{
    
        public class UB04Transaction //:Transaction
        {
           
            #region "Constructor & Destructor"

            public UB04Transaction(Int64 MasterTransactionID, Int64 TransactionID)
            {
                _Transaction = new TransactionEDI();       
                RevenueCode = "";
                TypeofBilling = "";
                AdmissionHour = "";
                Frequencytypecode = "";
                Facilitytypecode = "";
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

            ~UB04Transaction()
            {
                Dispose(false);
            }

            #endregion

            #region " Declarations "
      
            private DateTime _MaxDOS;
            private DateTime _MinDOS;         
            private string _AdmissionTypeCode = "";
            private string _AdmissionSource = "";
            private string _DischargeHour = "";
            private string _DischargeStatus = "";                
            private TransactionEDI _Transaction = null;

            #endregion " Declarations "

            #region "Properties"
                       
            public DateTime MaxDOS
            {
                 get { return _MaxDOS; }
                 set { _MaxDOS = value; }
            }

            public DateTime MinDOS
            {
                 get { return _MinDOS; }
                 set { _MinDOS = value; }
            }
           
            public string AdmissionTypeCode
            {
                get { return _AdmissionTypeCode; }
                set { _AdmissionTypeCode = value; }
            }

            public string AdmissionSource
            {
                get { return _AdmissionSource; }
                set { _AdmissionSource = value; }
            }


            public string DischargeHour
            {
                get { return _DischargeHour; }
                set { _DischargeHour = value; }
            }

            public string AdmissionHour
            {
                get;
                set;
            }

            public string DischargeStatus
            {
                get { return _DischargeStatus; }
                set { _DischargeStatus = value; }
            }


            private Boolean bAssignedTransaction = true;
            public TransactionEDI Transaction
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

            public string RevenueCode { get; set; }

            public string TypeofBilling { get; set; }

            public string Facilitytypecode{get;set;}

            public string Frequencytypecode{get;set;}

            public DataTable dtConditioncodes { get; set;}

            public DataTable dtValuecodes { get; set; }

            public DataTable dtOccurencecodes { get; set; }

            public DataTable dtOccurencespancodes { get; set; }
                
            #endregion
          
        }

        public class gloUB04
	{

          #region "Constructor & Destructor"

            public gloUB04()
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

            ~gloUB04()
            {
                Dispose(false);
            }

            #endregion

		  #region "Private Methods"
        
            public UB04Transaction GetUBClaim(Int64 MasterTransactionID, Int64 TransactionID)
            {
                UB04Transaction objUB04 = new UB04Transaction(MasterTransactionID,TransactionID);

                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloSettings.AppSettings.ConnectionStringPM);
                gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();

                DataSet dsUBClaimData = new DataSet();              
                gloClaimManager objClaimManager=new gloClaimManager(gloSettings.AppSettings.ConnectionStringPM,gloSettings.AppSettings.ConnectionStringEMR);
                try
                {
                    gloBilling objBilling=new gloBilling(gloSettings.AppSettings.ConnectionStringPM,gloSettings.AppSettings.ConnectionStringEMR);
                    objUB04.Transaction = objClaimManager.GetChargesClaimDetails_EDI(TransactionID, gloSettings.AppSettings.ClinicID);                                                                   
                    oDBParameters.Add("@nTransactionMasterID", objUB04.Transaction.TransactionMasterID, ParameterDirection.Input, SqlDbType.BigInt);
                    oDBParameters.Add("@nTransactionID", objUB04.Transaction.TransactionID, ParameterDirection.Input, SqlDbType.BigInt);
                    oDB.Connect(false); 
                    oDB.Retrive("BL_SELECT_UB_CLAIM_SETTING_EDI", oDBParameters, out dsUBClaimData);
                    oDB.Disconnect();
                    dsUBClaimData.Tables[0].TableName = "UBClaimData";
                    dsUBClaimData.Tables[1].TableName = "UBSetting";

                    dsUBClaimData.Tables[2].TableName = "ConditionCodes";
                    dsUBClaimData.Tables[3].TableName = "ValueCodes";
                    dsUBClaimData.Tables[4].TableName = "Occurencecodes";
                    dsUBClaimData.Tables[5].TableName = "OccurenceSpanCodes";

                    DataTable dtTrans = dsUBClaimData.Tables["UBClaimData"];
                    DataTable dtUBAdminSetting = dsUBClaimData.Tables["UBSetting"];

                    DataTable dtConditionCodes = dsUBClaimData.Tables["ConditionCodes"];
                    DataTable dtValueCodes = dsUBClaimData.Tables["ValueCodes"];
                    DataTable dtOccurencecodes = dsUBClaimData.Tables["Occurencecodes"];
                    DataTable dtOccurenceSpanCodes = dsUBClaimData.Tables["OccurenceSpanCodes"];

                    if(dtTrans!=null && dtTrans.Rows.Count>0)
                    {                      
                       objUB04.AdmissionTypeCode=Convert.ToString(dtTrans.Rows[0]["sAdmissionTypeCode"]);
                       objUB04.AdmissionSource=Convert.ToString(dtTrans.Rows[0]["sAdmissionSourceCode"]);                     
                       objUB04.DischargeStatus=Convert.ToString(dtTrans.Rows[0]["sDischargeStatusCode"]);                                                                  
                    }
                  
                    #region MaxDOS AND MinDOS

                    
                      DateTime _MaxDate = DateTime.MinValue;
                      DateTime _MinDate = DateTime.MaxValue;

                        if (objUB04.Transaction.Lines != null)
                        {
                            for (int _Count = 0; _Count < objUB04.Transaction.Lines.Count; _Count++)
                            {
                                
                                if (_MaxDate.Date < objUB04.Transaction.Lines[_Count].DateServiceFrom.Date)
                                {
                                    _MaxDate = objUB04.Transaction.Lines[_Count].DateServiceFrom;
                                }
                                if (_MaxDate.Date < objUB04.Transaction.Lines[_Count].DateServiceTill.Date)
                                {
                                    _MaxDate = objUB04.Transaction.Lines[_Count].DateServiceTill;
                                }


                                if (_MinDate.Date > objUB04.Transaction.Lines[_Count].DateServiceFrom.Date)
                                {
                                    _MinDate = objUB04.Transaction.Lines[_Count].DateServiceFrom;
                                }
                                if (_MinDate.Date > objUB04.Transaction.Lines[_Count].DateServiceTill.Date)
                                {
                                    _MinDate = objUB04.Transaction.Lines[_Count].DateServiceTill;
                                }
                            }
                            if (_MaxDate != DateTime.MinValue)
                            {
                                objUB04.MaxDOS = _MaxDate;
                            }
                            if (_MinDate != DateTime.MaxValue)
                            {
                                objUB04.MinDOS = _MinDate;
                            }
                        }
                    #endregion 

                    #region "Admin Setting"

                        if (dtUBAdminSetting != null && dtUBAdminSetting.Rows.Count > 0)
                        {
                            for (int i = 0; dtUBAdminSetting.Rows.Count > i; i++)
                            {                              
                                if (dtUBAdminSetting.Rows[i]["sSettingsName"].ToString().Trim() == "UB04_TypeOfBill")
                                {
                                    if (objUB04.TypeofBilling.ToString().Trim() == "")
                                    {
                                        objUB04.TypeofBilling = dtUBAdminSetting.Rows[i]["sSettingsValue"].ToString().Trim();
                                        string Facilitytypecode, Frequencytypecode = "";
                                        GetTypeofBill(objUB04.TypeofBilling,out Facilitytypecode ,out Frequencytypecode );
                                        objUB04.Facilitytypecode=Facilitytypecode;
                                        objUB04.Frequencytypecode = Frequencytypecode;
                                    }
                                }
                                if (dtUBAdminSetting.Rows[i]["sSettingsName"].ToString().Trim() == "UB04_AdmisionType")
                                {
                                    if (objUB04.AdmissionTypeCode.ToString().Trim() == "")
                                    {
                                        objUB04.AdmissionTypeCode = dtUBAdminSetting.Rows[i]["sSettingsValue"].ToString().Trim();
                                    }
                                }
                                if (dtUBAdminSetting.Rows[i]["sSettingsName"].ToString().Trim() == "UB04_AdmisionSource")
                                {
                                    if (objUB04.AdmissionSource.ToString().Trim() == "")
                                    {
                                        objUB04.AdmissionSource = dtUBAdminSetting.Rows[i]["sSettingsValue"].ToString().Trim();
                                    }
                                }

                                if (dtUBAdminSetting.Rows[i]["sSettingsName"].ToString().Trim() == "UB04_DischargeStatus")
                                {
                                    if (objUB04.DischargeStatus.ToString().Trim() == "")
                                    {
                                        objUB04.DischargeStatus = dtUBAdminSetting.Rows[i]["sSettingsValue"].ToString().Trim();
                                    }
                                }

                                if (dtUBAdminSetting.Rows[i]["sSettingsName"].ToString().Trim() == "UB04_Admissiontime")
                                {
                                    if (objUB04.AdmissionHour.ToString().Trim() == "")
                                    {
                                        objUB04.AdmissionHour = dtUBAdminSetting.Rows[i]["sSettingsValue"].ToString().Trim();
                                    }
                                }

                                if (dtUBAdminSetting.Rows[i]["sSettingsName"].ToString().Trim() == "UB04_Dischargetime")
                                {
                                    if (objUB04.DischargeHour.ToString().Trim() == "")
                                    {
                                        objUB04.DischargeHour = dtUBAdminSetting.Rows[i]["sSettingsValue"].ToString().Trim();
                                    }
                                }


                            }
                        }
                        #endregion

                    #region " More UB Data "

                        objUB04.dtConditioncodes = dtConditionCodes;
                        objUB04.dtValuecodes = dtValueCodes;
                        objUB04.dtOccurencecodes = dtOccurencecodes;
                        objUB04.dtOccurencespancodes = dtOccurenceSpanCodes;

                    #endregion

                    #region "Type of Bill Override"

                        if (dtTrans != null && dtTrans.Rows.Count > 0)
                        {
                            objUB04.TypeofBilling = Convert.ToString(dtTrans.Rows[0]["sTypeOfBill"]).Trim();
                            string Facilitytypecode, Frequencytypecode = "";
                            GetTypeofBill(objUB04.TypeofBilling, out Facilitytypecode, out Frequencytypecode);
                            if (Facilitytypecode != null && Facilitytypecode.Trim() != "")
                            {
                                objUB04.Facilitytypecode = Facilitytypecode;
                            }
                            if (Frequencytypecode != null && Frequencytypecode.Trim() != "")
                            {
                                objUB04.Frequencytypecode = Frequencytypecode;
                            }
                        }

                    #endregion

                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                }
                finally 
                {
                    if (dsUBClaimData != null) dsUBClaimData.Dispose();
                    if (oDBParameters != null) oDBParameters.Dispose();
                    if (oDB != null) oDB.Dispose();
                }
                return objUB04;
            }

            private bool GetTypeofBill(string Typeofbill, out string TypeCode, out string TypeBillcode)
            {
                TypeCode = "";
                TypeBillcode = "";
                bool _result = false;
                try
                {

                    if (Typeofbill.Trim().Length >= 4)
                    {
                        TypeCode = Typeofbill.Substring(1, 2);
                        TypeBillcode = Typeofbill.Substring(3);
                        _result = true;
                    }
                    else if (Typeofbill.Trim().Length == 3)
                    {
                        TypeCode = Typeofbill.Substring(0, 2);
                        TypeBillcode = Typeofbill.Substring(2);
                        _result = true;
                    }
                    else if (Typeofbill.Trim().Length == 2)
                    {
                        TypeCode = Typeofbill.Substring(0, 1);
                        TypeBillcode = Typeofbill.Substring(1);
                        _result = true;
                    }
                    else if (Typeofbill.Trim().Length == 1)
                    {
                        TypeCode = "";
                        TypeBillcode = Typeofbill.Substring(0);
                        _result = true;
                    }

                }
                catch //(Exception ex)
                {
                    _result = false;
                }
                return _result;
            }

          #endregion

	}
    
}
