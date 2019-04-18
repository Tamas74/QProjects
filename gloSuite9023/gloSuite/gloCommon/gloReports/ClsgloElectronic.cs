using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;
using System.Collections;

namespace gloReports
{
    public class ClsgloElectronic : IDisposable
    {

        #region " Constructor & Destructor "

        public ClsgloElectronic(String DatabaseConnectionString)
        {
            _databaseconnectionstring = DatabaseConnectionString;

            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 0; }
            }
            else
            { _ClinicID = 0; }

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
        //Start       abhisekh 19/03/2010-------------------
        ~ClsgloElectronic()
        {
          //Dispose(false);
            ClearALL();  
        }
        
        void ClearALL()
        {
            Dispose(false);
        }
        //End       abhisekh 19/03/2010-------------------
        #endregion " Constructor & Destructor "

        #region " Private variables "

        private string _databaseconnectionstring = "";

        DataTable dtTemp;
        private Int64 _ClinicID = 0;
       

        string _sqlQuery = string.Empty;
        Decimal _PatientDue = 0;
        Decimal _InsurenceDue = 0;
        Decimal _TotalCharges = 0;
        Decimal _TotalPaymentsAndAdjustmnets = 0;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationSettings.AppSettings;
        string _strPatientName = string.Empty;
        string _strPatientCode = string.Empty;
        string _strPatientAddress = string.Empty;
        string _strPracticePhone = string.Empty;
        string _strClinicMsg1 = string.Empty;
        string _strClinicMsg2 = string.Empty;
        string _strClinicPhone = string.Empty;
       
        string _sStatementNotes = string.Empty;

        

        #endregion " Private variables "



        public void GenerateElectonicClaimFile(ArrayList PatientIDs, string StatementDate, Int64 CriteriaID, string FilePath)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            FileStream fs = new FileStream(FilePath, FileMode.Create, FileAccess.Write);
            StreamWriter m_streamWriter = new StreamWriter(fs);
            m_streamWriter.BaseStream.Seek(0, SeekOrigin.End);
            try
            {
                oDB.Connect(false);

                //------------------------------gloPMApplicationVersion---------------------//

                #region "gloPMApplicationVersion"
                try
                {
                    _sqlQuery = " SELECT ISNULL(sSettingsName,'') AS sSettingsName,ISNULL(sSettingsValue,'') AS sSettingsValue FROM Settings " +
                                " WHERE sSettingsName = 'gloPMApplicationVersion' ";
                    dtTemp = new DataTable();
                    oDB.Retrive_Query(_sqlQuery, out dtTemp);

                    string _settingsvalue = string.Empty;
                    if (dtTemp.Rows.Count > 0)
                    {
                        _settingsvalue = dtTemp.Rows[0]["sSettingsValue"].ToString();
                    }

                    m_streamWriter.WriteLine("@StartStatementFile");
                    m_streamWriter.WriteLine("@StatementFileVersion|1");
                    m_streamWriter.WriteLine("@StatementFileGenerateDate|" + DateTime.Now.ToShortDateString());
                    m_streamWriter.WriteLine("@StatementFileComment|This file was sent from gloStream gloPM " + _settingsvalue + "");
                }
                catch (Exception Ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), false);
                    Ex = null;
                }
                #endregion "gloPMApplicationVersion"
                
                //------------------------------gloPMApplicationVersion---------------------//


                // For Each Patient 
                for (int j = 0; j < PatientIDs.Count; j++)
                {
                    _sqlQuery = string.Empty;
                    _strPatientName = string.Empty;
                    _strPatientCode = string.Empty;
                    _strPatientAddress = string.Empty;
                    _strPracticePhone = string.Empty;
                    _strClinicMsg1 = string.Empty;
                    _strClinicMsg2 = string.Empty;
                    _strClinicPhone = string.Empty;
                    Int64 PatientID = Convert.ToInt64(PatientIDs[j].ToString());


                    m_streamWriter.WriteLine("@StartStatement");
                    m_streamWriter.WriteLine("@StatementDate|" + StatementDate);

                    
                    #region "Clinic Information "

                    try
                    {

                        _sqlQuery = " SELECT [nClinicID],[sClinicName] ,[sAddress1] ,[sAddress2]" +
                                    " ,[sStreet],[sCity],[sState],[sZIP],ISNULL(   replace(replace(replace(replace(Clinic_MST.sphoneno,'(',''),')',''),'-',''),' ',''), '') AS [sPhoneNo],[sMobileNo],[sFAX]" +
                                    " ,[sEmail],[sURL],[sTAXID],[imgClinicLogo] ,[sContactPersonName]" +
                                    " ,[sContactPersonAddress1],[sContactPersonAddress2],[sContactPersonPhone]" +
                                    " ,[sContactPersonFAX] ,[sContactPersonEmail] ,[sContactPersonMobile]" +
                                    " ,[sSiteID] ,[sExternalCode] ,[sClinicNPI]" +
                                    " FROM [Clinic_MST] where nClinicID=" + _ClinicID;

                        dtTemp = new DataTable();
                        oDB.Retrive_Query(_sqlQuery, out dtTemp);

                        string _strPracticeName = string.Empty;
                        string _strPracticeAddress = string.Empty;
                        string _strPracticeWebsite = string.Empty;
                        string _strPracticeEMail = string.Empty;

                        if (dtTemp != null && dtTemp.Rows.Count > 0)
                        {
                            _strPracticeName = dtTemp.Rows[0]["sClinicName"].ToString();
                            _strPracticeAddress = dtTemp.Rows[0]["sAddress1"].ToString() + "|" + dtTemp.Rows[0]["sAddress2"].ToString() + "|" + dtTemp.Rows[0]["sCity"].ToString() + "|" + dtTemp.Rows[0]["sState"].ToString() + "|" + dtTemp.Rows[0]["sZIP"].ToString();
                            _strPracticePhone = dtTemp.Rows[0]["sPhoneNo"].ToString();
                            _strPracticeWebsite = dtTemp.Rows[0]["sURL"].ToString();
                            _strPracticeEMail = dtTemp.Rows[0]["sEmail"].ToString();
                            if (_strPracticePhone != "")
                            {
                                _strPracticePhone = "(" + _strPracticePhone.Substring(0, 3) + ") " + _strPracticePhone.Substring(3, 3) + "-" + _strPracticePhone.Substring(6, _strPracticePhone.Length - 6);
                            }
                            else
                            {
                                _strPracticePhone = dtTemp.Rows[0]["sPhoneNo"].ToString();
                            }
                        }

                        m_streamWriter.WriteLine("@PracticeName|" + _strPracticeName);
                        m_streamWriter.WriteLine("@PracticeAddress|" + _strPracticeAddress);
                        m_streamWriter.WriteLine("@PracticePhone|" + _strPracticePhone);
                        m_streamWriter.WriteLine("@PracticeWebsite|" + _strPracticeWebsite);
                        m_streamWriter.WriteLine("@PracticeEMail|" + _strPracticeEMail);
                        m_streamWriter.WriteLine("@PracticeMisc|");
                    }
                    catch (Exception Ex)
                    {
                        gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), false);
                        Ex = null;
                    }
                    #endregion "Clinic Information "


                    #region "Remit Information"
                    try
                    {
                        _sqlQuery = " SELECT sRemitName, CASE sRemitAddress1 WHEN '' THEN ' ' WHEN NULL THEN ' ' ELSE sRemitAddress1 END AS sRemitAddress1," +
                                                    " CASE sRemitAddress2 WHEN '' THEN ' ' WHEN NULL THEN ' ' ELSE sRemitAddress2 END AS sRemitAddress2," +
                                                    " CASE sRemitCity WHEN '' THEN ' ' WHEN NULL THEN ' ' ELSE sRemitCity END AS sRemitCity," +
                                                    " CASE sRemitState WHEN '' THEN ' ' WHEN NULL THEN ' ' ELSE sRemitState END AS sRemitState," +
                                                    " CASE sRemitZip WHEN '' THEN ' ' WHEN NULL THEN '' ELSE sRemitZip END AS sRemitZip," +
                                                    " bitIsPendingInsurance,  sClinicMessage1, sClinicMessage2, bitIsGuarantorIndicator, nClinicId " +
                                                    " FROM RPT_PatStatementCriteria_Display  where nStatementCriteriaID= " + CriteriaID + " AND nClinicID = " + _ClinicID;

                        dtTemp = new DataTable();
                        oDB.Retrive_Query(_sqlQuery, out dtTemp);

                        //Get remitte Information
                        string _strRemitName = string.Empty;
                        string _strRemitAddress = string.Empty;
                        if (dtTemp != null && dtTemp.Rows.Count > 0)
                        {
                            _strRemitName = dtTemp.Rows[0]["sRemitName"].ToString();
                            _strRemitAddress = dtTemp.Rows[0]["sRemitAddress1"].ToString() + "|" + dtTemp.Rows[0]["sRemitAddress2"].ToString() + "|" + dtTemp.Rows[0]["sRemitCity"].ToString() + "|" + dtTemp.Rows[0]["sRemitState"].ToString() + "|" + dtTemp.Rows[0]["sRemitZip"].ToString();
                            _strClinicMsg1 = dtTemp.Rows[0]["sClinicMessage1"].ToString();
                            _strClinicMsg2 = dtTemp.Rows[0]["sClinicMessage2"].ToString();
                        }

                        m_streamWriter.WriteLine("@RemitToName|" + _strRemitName);
                        m_streamWriter.WriteLine("@RemitToAddress|" + _strRemitAddress);

                    }
                    catch (Exception Ex)
                    {
                        gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), false);
                        Ex = null;
                    }

                    #endregion  "Remit Information"


                    #region "Patient Name And Address"
                    try
                    {
                        _sqlQuery = "SELECT [nPatientID] ,[sPatientCode] ,[sFirstName],[sMiddleName] ,[sLastName], sAddressLine1 + '|' + sAddressLine2 + '|' + sCity + '|' + sState + '|'+ sZip As PatientAddress " +
                                    " FROM [dbo].[Patient] where nClinicID=" + _ClinicID + "AND  nPatientID=" + PatientID;

                        dtTemp = new DataTable();
                        oDB.Retrive_Query(_sqlQuery, out dtTemp);

                        if (dtTemp != null && dtTemp.Rows.Count > 0)
                        {
                            _strPatientName = dtTemp.Rows[0]["sFirstName"].ToString() + "|" + dtTemp.Rows[0]["sLastName"].ToString();
                            _strPatientCode = dtTemp.Rows[0]["sPatientCode"].ToString();
                            _strPatientAddress = dtTemp.Rows[0]["PatientAddress"].ToString();
                        }
                    }
                    catch (Exception Ex)
                    {
                        gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), false);
                        Ex = null;
                    }

                    #endregion
                    

                    #region "Guarantor Information"

                    try
                    {

                        // It select guarantor when guarantor having priority like Primary,secondary,Tertiaury & inactive 
                        _sqlQuery = "select top(1) nPatientContactID,isnull(Patient_OtherContacts.sFirstName,'') as GuarantorFName,isnull(Patient_OtherContacts.sMiddleName,'') as GuarantorMName, " +
                                    " isnull(Patient_OtherContacts.sLastName,'') as GuarantorLName, " +
                                    " CASE Patient_OtherContacts.sAddressLine1 WHEN '' THEN '' WHEN NULL THEN '' ELSE Patient_OtherContacts.sAddressLine1 END AS GuarantorAdd1,  " +
                                    " CASE Patient_OtherContacts.sAddressLine2 WHEN '' THEN '' WHEN NULL THEN '' ELSE Patient_OtherContacts.sAddressLine2 END AS GuarantorAdd2, " +
                                    " CASE Patient_OtherContacts.sCity WHEN '' THEN '' WHEN NULL THEN '-' ELSE Patient_OtherContacts.sCity END AS GuarantorCity, " +
                                    " CASE Patient_OtherContacts.sState WHEN '' THEN '' WHEN NULL THEN '-' ELSE Patient_OtherContacts.sState END AS GuarantorState, " +
                                    " CASE Patient_OtherContacts.sZIP WHEN '' THEN '' WHEN NULL THEN '' ELSE Patient_OtherContacts.sZIP END AS GuarantorZip, " +
                                    " Patient_OtherContacts.nGuarantorAsPatientID AS GuarantorAccountNo, " +
                                    " Patient.sPatientCode As nPatientCode from Patient_OtherContacts Inner Join Patient On Patient.nPatientId = Patient_OtherContacts.nPatientId " +
                                    " WHERE  Patient_OtherContacts.nClinicID=" + _ClinicID + " AND Patient_OtherContacts.nPatientID=" + PatientID + " order by  Patient_OtherContacts.nPatientContactTypeFlag,Patient_OtherContacts.nPatientContactID";// + " and nPatientContactTypeFlag =4 ";
                        dtTemp = new DataTable();
                        oDB.Retrive_Query(_sqlQuery, out dtTemp);

                        string _strGuarantorName = string.Empty;
                        string _strGuarantorAddress = string.Empty;

                        if (dtTemp != null && dtTemp.Rows.Count > 0)
                        {
                            _strGuarantorName = dtTemp.Rows[0]["GuarantorFName"].ToString() + "|" + dtTemp.Rows[0]["GuarantorMName"].ToString() + "|" + dtTemp.Rows[0]["GuarantorLName"].ToString();
                            _strGuarantorAddress = dtTemp.Rows[0]["GuarantorAdd1"].ToString() + "|" + dtTemp.Rows[0]["GuarantorAdd2"].ToString() + "|" + dtTemp.Rows[0]["GuarantorCity"].ToString() + "|" + dtTemp.Rows[0]["GuarantorState"].ToString() + "|" + dtTemp.Rows[0]["GuarantorZip"].ToString();
                        }
                        else
                        {
                            _strGuarantorName = _strPatientName;
                            _strGuarantorAddress = _strPatientAddress;
                        }

                        m_streamWriter.WriteLine("@GuarantorAccountNumber|" + _strPatientCode);
                        m_streamWriter.WriteLine("@GuarantorName|" + _strGuarantorName.Trim());
                        m_streamWriter.WriteLine("@GuarantorAddress|" + _strGuarantorAddress.Trim());
                    }
                    catch (Exception Ex)
                    {
                        gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), false);
                        Ex = null;
                    }

                    #endregion "Guarantor Information"


                    #region "Patient Name and Last Remit"

                    dtTemp = new DataTable();
                    oDBParameters = new gloDatabaseLayer.DBParameters();
                    oDBParameters.Add("@nPatientID", PatientID, ParameterDirection.Input, SqlDbType.BigInt);
                    oDBParameters.Add("@nDate", gloDateMaster.gloDate.DateAsNumber(StatementDate), ParameterDirection.Input, SqlDbType.BigInt);
                    oDBParameters.Add("@nClinicID", _ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                    oDB.Retrive("BL_ElectronicStatement_LastPatientRemit", oDBParameters, out dtTemp);


                    string _strLastPatientPaymentDate = string.Empty;
                    string _strLastPatientPaymentAmt = string.Empty;
                    if (dtTemp.Rows.Count > 0)
                    {
                        _strLastPatientPaymentDate = Convert.ToString(dtTemp.Rows[0]["Closedate"]);
                        _strLastPatientPaymentAmt = Convert.ToString(dtTemp.Rows[0]["nCheckAmount"]);
                    }
                    
                    m_streamWriter.WriteLine("@Patient|" + _strPatientName);
                    m_streamWriter.WriteLine("@LastPatientPayment|" + _strLastPatientPaymentDate + "|" + _strLastPatientPaymentAmt);

                    #endregion  "Patient Name and Last Remit"
                    
                   
                    DataTable dt = new DataTable();

                    DataTable dtChargeLines = new DataTable();

                    oDBParameters = new gloDatabaseLayer.DBParameters();
                    oDBParameters.Add("@nPatientID", PatientID, ParameterDirection.Input, SqlDbType.BigInt);
                    oDBParameters.Add("@nDate", gloDateMaster.gloDate.DateAsNumber(StatementDate), ParameterDirection.Input, SqlDbType.BigInt);
                    oDBParameters.Add("@nClinicID", _ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                    oDB.Retrive("BL_ElectronicStatement_ChargeLineDetails", oDBParameters, out dtChargeLines);


                    //For Each Charge Line
                    _PatientDue = 0;
                    _InsurenceDue = 0;
                    _TotalCharges = 0;
                    _TotalPaymentsAndAdjustmnets = 0;
                    for (int i = 0; i < dtChargeLines.Rows.Count; i++)
                    {
                        
                        #region "Charge Line"
                        //----------------------------- Charge Line Start --------------------------------//

                        
                            m_streamWriter.WriteLine(" @ChargeLineStart");
                            m_streamWriter.WriteLine("   @Date|" + Convert.ToString(dtChargeLines.Rows[i]["sFromDate"]));
                            m_streamWriter.WriteLine("   @Patient|" + _strPatientName);

                            string NextPartyType = "";
                            if (Convert.ToString(dtChargeLines.Rows[i]["NextParty"]) == "2")
                                NextPartyType = "I";
                            else
                                NextPartyType = "P";

                            m_streamWriter.WriteLine("   @Responsibility|" + Convert.ToString(NextPartyType));
                            m_streamWriter.WriteLine("   @Procedure|" + Convert.ToString(dtChargeLines.Rows[i]["sCPTCode"]));
                            m_streamWriter.WriteLine("   @Description|" + Convert.ToString(dtChargeLines.Rows[i]["sCPTDescription"]));
                            m_streamWriter.WriteLine("   @Amount|" + Convert.ToString(dtChargeLines.Rows[i]["dTotal"]));
                            m_streamWriter.WriteLine("   @ChargeTotalPatPaid|" + Convert.ToString(dtChargeLines.Rows[i]["PatientPaid"]));
                            m_streamWriter.WriteLine("   @ChargeTotalInsPaid|" + Convert.ToString(dtChargeLines.Rows[i]["InsurancePaid"]));
                            m_streamWriter.WriteLine("   @ChargeTotalAdj|" + Convert.ToString(dtChargeLines.Rows[i]["Adjustments"]));

                            if (NextPartyType == "P")
                            {
                                m_streamWriter.WriteLine("   @ChargeInsPending|" + "0.00");
                                m_streamWriter.WriteLine("   @ChargePatientDue|" + Convert.ToString(dtChargeLines.Rows[i]["Pending"]));
                                _PatientDue += Convert.ToDecimal(dtChargeLines.Rows[i]["Pending"]);
                            }
                            else
                            {
                                m_streamWriter.WriteLine("   @ChargeInsPending|" + Convert.ToString(dtChargeLines.Rows[i]["Pending"]));
                                m_streamWriter.WriteLine("   @ChargePatientDue|" + "0.00");
                                _InsurenceDue += Convert.ToDecimal(dtChargeLines.Rows[i]["Pending"]);
                            }

                            m_streamWriter.WriteLine(" @ChargeLineEnd");

                            if (Convert.ToString(dtChargeLines.Rows[i]["dTotal"]) != "")
                            _TotalCharges += Convert.ToDecimal(dtChargeLines.Rows[i]["dTotal"]);
                        //----------------------------- Charge Line End --------------------------------//
                        #endregion "Charge Line" 

                        #region "Payment and Adjustments"
                        
                        DataTable dtPaymentLines = new DataTable();
                        oDBParameters = new gloDatabaseLayer.DBParameters();

                        oDBParameters.Add("@nBillingTransactionID", Convert.ToInt64(dtChargeLines.Rows[i]["nBillingTransactionID"]), ParameterDirection.Input, SqlDbType.BigInt);
                        oDBParameters.Add("@nBillingTransactionDetailID", Convert.ToInt64(dtChargeLines.Rows[i]["nBillingTransactionDetailID"]), ParameterDirection.Input, SqlDbType.BigInt);
                        oDBParameters.Add("@nPatientID", PatientID, ParameterDirection.Input, SqlDbType.BigInt);
                        oDBParameters.Add("@nDate", gloDateMaster.gloDate.DateAsNumber(StatementDate), ParameterDirection.Input, SqlDbType.BigInt);
                        oDBParameters.Add("@nClinicID", _ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                        oDB.Retrive("BL_ElectronicStatement_PaymentLineDetails", oDBParameters, out dtPaymentLines);

                       
                        //----------------------------- Payment Line Start --------------------------------//
                        string PaymentParty = "";
                        for (int k = 0; k < dtPaymentLines.Rows.Count; k++)
                        {
                            if (Convert.ToString(dtPaymentLines.Rows[k]["PaymentType"]) == "Payment")
                            {
                                m_streamWriter.WriteLine(" @PaymentLineStart");
                                m_streamWriter.WriteLine("   @Date|" + Convert.ToString(dtPaymentLines.Rows[k]["CloseDate"]));

                                PaymentParty = "";
                                if (Convert.ToString(dtPaymentLines.Rows[k]["Source"]) == "2")
                                    PaymentParty = "I";
                                else
                                    PaymentParty = "P";

                                m_streamWriter.WriteLine("   @Source|" + PaymentParty);
                                m_streamWriter.WriteLine("   @Description|" + Convert.ToString(dtPaymentLines.Rows[k]["sDescription"]));
                                m_streamWriter.WriteLine("   @Amount|" + Convert.ToString(dtPaymentLines.Rows[k]["nAmount"]));
                                m_streamWriter.WriteLine(" @PaymentLineEnd");
                                if (Convert.ToString(dtPaymentLines.Rows[k]["nAmount"]) != "")
                                _TotalPaymentsAndAdjustmnets += Convert.ToDecimal(dtPaymentLines.Rows[k]["nAmount"]);
                            }
                        }
                        //----------------------------- Payment Line End --------------------------------//

                        //----------------------------- Adjustment Line Start------------------------------//
                        
                        for (int k = 0; k < dtPaymentLines.Rows.Count; k++)
                        {
                            if (Convert.ToString(dtPaymentLines.Rows[k]["PaymentType"]) == "Adjustment")
                            {
                                m_streamWriter.WriteLine(" @AdjustmentLineStart");
                                m_streamWriter.WriteLine("   @Date|" + Convert.ToString(dtPaymentLines.Rows[k]["CloseDate"]));

                                PaymentParty = "";
                                if (Convert.ToString(dtPaymentLines.Rows[k]["Source"]) == "2")
                                    PaymentParty = "I";
                                else
                                    PaymentParty = "P";

                                m_streamWriter.WriteLine("   @Source|" + PaymentParty);
                                m_streamWriter.WriteLine("   @Description|" + Convert.ToString(dtPaymentLines.Rows[k]["sDescription"]));
                                m_streamWriter.WriteLine("   @Amount|" + Convert.ToString(dtPaymentLines.Rows[k]["nAmount"]));
                                m_streamWriter.WriteLine(" @AdjustmentLineEnd");
                                if (Convert.ToString(dtPaymentLines.Rows[k]["nAmount"])!="")
                                _TotalPaymentsAndAdjustmnets += Convert.ToDecimal(dtPaymentLines.Rows[k]["nAmount"]);
                            }
                        }
                        //----------------------------- Adjustment Line End------------------------------//

                        #endregion "Payment and Adjustments"

                        #region "Notes"


                        DataTable dtNoteLines = new DataTable();
                        oDBParameters = new gloDatabaseLayer.DBParameters();
                        oDBParameters.Add("@nBillingTransactionID", Convert.ToInt64(dtChargeLines.Rows[i]["nBillingTransactionID"]), ParameterDirection.Input, SqlDbType.BigInt);
                        oDBParameters.Add("@nBillingTransactionDetailID", Convert.ToInt64(dtChargeLines.Rows[i]["nBillingTransactionDetailID"]), ParameterDirection.Input, SqlDbType.BigInt);
                        oDBParameters.Add("@nDate", gloDateMaster.gloDate.DateAsNumber(StatementDate), ParameterDirection.Input, SqlDbType.BigInt);
                        oDB.Retrive("BL_ElectronicStatement_NoteLineDetails", oDBParameters, out dtNoteLines);

                        //------------------------------------Line Notes Start-----------------------------//
                        for (int k = 0; k < dtNoteLines.Rows.Count; k++)
                        {
                            m_streamWriter.WriteLine(" @NoteLineStart");
                            m_streamWriter.WriteLine("   @Date|" + Convert.ToString(dtNoteLines.Rows[k]["LineNoteCloseDate"]));
                            m_streamWriter.WriteLine("   @Description|" + Convert.ToString(dtNoteLines.Rows[k]["LineNote"]));
                            m_streamWriter.WriteLine(" @NoteLineEnd");
                        }
                        //------------------------------------Line Notes End-----------------------------//

                        #endregion "Notes"

                    } ////For Each Charge Line End

                    #region "Statement Totals"

                    DataTable dtStatementSummary = new DataTable();
                    oDBParameters = new gloDatabaseLayer.DBParameters();
                    oDBParameters.Add("@nPatientID", PatientID, ParameterDirection.Input, SqlDbType.BigInt);
                    oDBParameters.Add("@nDate", gloDateMaster.gloDate.DateAsNumber(StatementDate), ParameterDirection.Input, SqlDbType.BigInt);
                    oDBParameters.Add("@nClinicID", _ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                    oDB.Retrive("BL_GET_PATIENT_BALANCE", oDBParameters, out dtStatementSummary);

                    //------------------------------------ Statement Totals --------------------------------------------
                    m_streamWriter.WriteLine("@StatementTotalChargeAmount|" + Convert.ToDecimal(_TotalCharges).ToString("#0.00"));
                    m_streamWriter.WriteLine("@StatementTotalPaymentsandAdjustments|" + Convert.ToDecimal(_TotalPaymentsAndAdjustmnets).ToString("#0.00"));
                    m_streamWriter.WriteLine("@StatementTotalChargeInsPending|" + Convert.ToDecimal(_InsurenceDue).ToString("#0.00"));
                    m_streamWriter.WriteLine("@StatementTotalChargePatientDue|" + Convert.ToDecimal(_PatientDue).ToString("#0.00"));


                    if (dtStatementSummary.Rows.Count > 0)
                    {
                        m_streamWriter.WriteLine("@StatementAvailablePayments|" + Convert.ToDecimal(dtStatementSummary.Rows[0]["AvailableReserve"]));
                        m_streamWriter.WriteLine("@StatementTotalPatientDue|" + (Convert.ToDecimal(dtStatementSummary.Rows[0]["PatientDue"]) - Convert.ToDecimal(dtStatementSummary.Rows[0]["AvailableReserve"]))); //Convert.ToString(dtStatementSummary.Rows[0][""])
                        //------------------------------------ Statement Totals End --------------------------------------------
                    }

                    #endregion  "Statement Totals"

                    #region "Aging Buckets"
                    //------------------------------------Aging Bucket Start------------------------------------------------
                    DataTable dtAgingBucket = new DataTable();
                    try
                    {

                        oDBParameters = new gloDatabaseLayer.DBParameters();
                        oDBParameters.Add("@nPatientID", PatientID, ParameterDirection.Input, SqlDbType.BigInt);
                        oDBParameters.Add("@nEndDate", gloDateMaster.gloDate.DateAsNumber(StatementDate), ParameterDirection.Input, SqlDbType.BigInt);
                        oDBParameters.Add("@nClinicID", 1, ParameterDirection.Input, SqlDbType.BigInt);

                        oDB.Retrive("BL_SELECT_AgingBuckets", oDBParameters, out dtAgingBucket);

                        if (dtAgingBucket != null && dtAgingBucket.Rows.Count > 0)
                        {
                            m_streamWriter.WriteLine("@AgingBucketLabel1|0-30");
                            m_streamWriter.WriteLine("@AgingBucketValue1|" + dtAgingBucket.Rows[0][0].ToString());
                            m_streamWriter.WriteLine("@AgingBucketLabel2|31-60");
                            m_streamWriter.WriteLine("@AgingBucketValue2|" + dtAgingBucket.Rows[0][1].ToString());
                            m_streamWriter.WriteLine("@AgingBucketLabel3|61-90");
                            m_streamWriter.WriteLine("@AgingBucketValue3|" + dtAgingBucket.Rows[0][2].ToString());
                            m_streamWriter.WriteLine("@AgingBucketLabel4|91-120");
                            m_streamWriter.WriteLine("@AgingBucketValue4|" + dtAgingBucket.Rows[0][3].ToString());
                            m_streamWriter.WriteLine("@AgingBucketLabel5|120+");
                            m_streamWriter.WriteLine("@AgingBucketValue5|" + dtAgingBucket.Rows[0][4].ToString());
                        }

                    }
                    catch (Exception Ex)
                    {
                        gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), false);
                        Ex = null;
                    }

                    
                    //------------------------------------Aging Bucket End------------------------------------------------

                    #endregion

                    #region "Clinic Messages"

                    DataTable dtPSNotes = new DataTable();
                    try
                    {
                        _sqlQuery = "SELECT sStatementNote FROM Patient_Statement_Notes WHERE nPatientID = " + PatientID + " AND nClinicID = " + _ClinicID + " AND ( nfromdate <= " + gloDateMaster.gloDate.DateAsNumber(StatementDate) + " AND nToDate >= " + gloDateMaster.gloDate.DateAsNumber(StatementDate) + ")";
                        oDB.Retrive_Query(_sqlQuery, out dtPSNotes);

                        if (dtPSNotes != null && dtPSNotes.Rows.Count > 0)
                        {
                            _sStatementNotes = string.Empty;
                            for (int i = 0; i < dtPSNotes.Rows.Count; i++)
                            {
                                _sStatementNotes = _sStatementNotes + dtPSNotes.Rows[i]["sStatementNote"].ToString();
                            }
                        }
                        else
                        {
                            _sStatementNotes = string.Empty;
                        }

                        m_streamWriter.WriteLine("@AccountMessage1|" + _strClinicMsg1);
                        m_streamWriter.WriteLine("@AccountMessage2|" + _sStatementNotes);

                    }
                    catch (Exception Ex)
                    {
                        gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), true);
                    } 
                    #endregion

                    #region "Future Appointments"
                    //-----------------------------Future Appointment Start--------------------------------------------//
                    DataTable dtFutureAppointment = new DataTable();
                    try
                    {
                        _sqlQuery = " SELECT DISTINCT ISNULL(Patient.sFirstName,'')  + '|' + ISNULL(Patient.sLastName,'') sPatientName, ISNULL(AS_Appointment_MST.sASBaseDesc,'') AS sProviderName, " +
                                   " dbo.CONVERT_TO_DATE( AS_Appointment_DTL.dtStartDate) AS dtStartDate, dbo.CONVERT_TO_TIME( AS_Appointment_DTL.dtStartTime) AS dtStartTime, " +
                                   " ISNULL(AS_Appointment_DTL.sLocationName,'') AS sLocationName, ISNULL(AB_Location.sAddressLine1,'') + '|' + ISNULL(AB_Location.sAddressLine2,'') + '|'+ ISNULL(AB_Location.sCity,'') + '|'+ ISNULL(AB_Location.sState,'') + '|' + ISNULL(AB_Location.sZIP,'') As LocationAddress " +
                                   " FROM AS_Appointment_MST INNER JOIN  AS_Appointment_DTL ON AS_Appointment_MST.nMSTAppointmentID = AS_Appointment_DTL.nMSTAppointmentID INNER JOIN " +
                                   " Patient ON AS_Appointment_MST.nPatientID = Patient.nPatientID INNER JOIN " +
                                   " AB_Location ON AS_Appointment_DTL.nLocationID = AB_Location.nLocationID WHERE CONVERT(DATETIME,CONVERT(VARCHAR,AS_Appointment_DTL.dtStartDate),101) > CONVERT(DATETIME,CONVERT(VARCHAR, " + gloDateMaster.gloDate.DateAsNumber(StatementDate) + " ),101) AND  Patient.nPatientID = " + PatientID + " AND Patient.nClinicID =" + _ClinicID +
                                   " AND (AS_Appointment_DTL.nUsedStatus <> 5 AND AS_Appointment_DTL.nUsedStatus <>6 AND AS_Appointment_DTL.nUsedStatus <>7 ) AND AS_Appointment_DTL.nASBaseFlag = 1 ";
                        oDB.Retrive_Query(_sqlQuery, out dtFutureAppointment);

                        for (int l = 0; l < dtFutureAppointment.Rows.Count; l++)
                        {
                            m_streamWriter.WriteLine("@FutureAppointmentStart");
                            m_streamWriter.WriteLine("@Date|" + dtFutureAppointment.Rows[l]["dtStartDate"].ToString());
                            m_streamWriter.WriteLine("@Time|" + dtFutureAppointment.Rows[l]["dtStartTime"].ToString());
                            m_streamWriter.WriteLine("@PatientName|" + _strPatientName);
                            m_streamWriter.WriteLine("@LocationAddress|" + dtFutureAppointment.Rows[l]["LocationAddress"].ToString());
                            m_streamWriter.WriteLine("@LocationPhone|" + _strPracticePhone);
                            m_streamWriter.WriteLine("@AppointmentProvider|" + dtFutureAppointment.Rows[l]["sProviderName"].ToString());
                            m_streamWriter.WriteLine("@FutureAppointmentEnd");
                        }
                    }
                    catch (Exception Ex)
                    {
                        gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), false);
                        Ex = null;
                    }
                    //-----------------------------Future Appointment End--------------------------------------------//

                    #endregion

                    m_streamWriter.WriteLine("@EndStatement");
                
                } // For Each Patient end
                m_streamWriter.WriteLine("@EndStatementFile");
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); }
                m_streamWriter.Flush();
                m_streamWriter.Close();
            }

        }
    }
}
