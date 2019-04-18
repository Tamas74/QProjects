using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;
using System.Collections;
using gloGlobal;

namespace gloBilling.Statement
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
        DataTable dtClinicInfo;
        private Int64 _ClinicID = 0;
       

        private bool _isBusinessCenterEnable = false;
        private Int64 _nBusinessCenterID = 0;
        string _sqlQuery = string.Empty;
        Decimal _PatientDue = 0;
        Decimal _InsurenceDue = 0;
        Decimal _TotalCharges = 0;
        Decimal _TotalPaymentsAndAdjustmnets = 0;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        string _strPatientName = string.Empty;
        string _strPatientCode = string.Empty;
        string _strPatientAddress = string.Empty;
        string _strPracticePhone = string.Empty;
        string _strClinicMsg1 = string.Empty;
        string _strClinicMsg2 = string.Empty;
        string _strClinicPhone = string.Empty;
       
        string _sStatementNotes = string.Empty;
        public bool IsBusinessCenterEnable
        {
            get { return _isBusinessCenterEnable; }
            set { _isBusinessCenterEnable = value; }
        }
        public Int64 BusinessCenterID
        {
            get { return _nBusinessCenterID; }
            set { _nBusinessCenterID = value; }
        }
        

        #endregion " Private variables "

        #region "Methods"

        public int GetStatementCount(Int64 AccountID, Int32 Statementdate)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dtTemp = new DataTable();
            object statementcount = null;
            int iCount = 0;
            try
            {
                oDB.Connect(false);
                _sqlQuery = "select dbo.PA_Get_StatementCount_V2(" + AccountID + ",'" + gloDateMaster.gloDate.DateAsDate(Statementdate) + "')";
                statementcount = oDB.ExecuteScalar_Query(_sqlQuery);
                iCount = Convert.ToInt32(statementcount);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return 0;
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); }
                if (statementcount != null) { statementcount = null; }
                dtTemp.Dispose();
            }

            return iCount;
        }

        /// <summary>
        /// code changed by SaiKrishna. AccountIDs parameter added.This method is called when PAF feature is off
        /// </summary>
        /// <param name="PatientIDs"></param>
        /// <param name="StatementDate"></param>
        /// <param name="CriteriaID"></param>
        /// <param name="FilePath"></param>
        public void GenerateElectonicClaimFile(ArrayList PatientIDs, string StatementDate, Int64 CriteriaID, string FilePath, ArrayList AccountIDs)
        {            
            DataSet _dsLocal;
            DataTable dtBillingInfo;
            Int64 _CriteriaId = 0;

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            FileStream fs = new FileStream(FilePath, FileMode.Create, FileAccess.Write);
            StreamWriter m_streamWriter = new StreamWriter(fs);
            m_streamWriter.BaseStream.Seek(0, SeekOrigin.End);
            try
            {
                oDB.Connect(false);
                dtTemp = new DataTable();
                dtClinicInfo = new DataTable();
                dtBillingInfo = new DataTable();
                _dsLocal = new DataSet();
                             

                #region "FETCH ELECTRONIC STATEMENT HEADER INFORMATION"

                gloStatment ObjClsgloStatment = new gloStatment();
                if (IsBusinessCenterEnable && BusinessCenterID > 0)
                { _CriteriaId = ObjClsgloStatment.GetBusinessCenterDisplaySettings(BusinessCenterID); }
                else { _CriteriaId = ObjClsgloStatment.GetStatmentCriteriaID(); }
     
                oDBParameters.Clear();
                oDBParameters.Add("@CriteriaID", _CriteriaId, ParameterDirection.Input, SqlDbType.BigInt);               
                oDB.Retrive("PA_GET_ELECTRONIC_STATEMENT_HEADER", oDBParameters, out _dsLocal);
                                    
                if (_dsLocal.Tables.Count >= 1)
                {
                    dtTemp = _dsLocal.Tables[0];
                }
                if (_dsLocal.Tables.Count >= 2)
                {
                    dtClinicInfo = _dsLocal.Tables[1];
                }
                if (_dsLocal.Tables.Count >= 3)
                {
                    dtBillingInfo = _dsLocal.Tables[2];
                }
                
                #endregion "FETCH ELECTRONIC STATEMENT HEADER INFORMATION"
                               
                #region "gloPMApplicationVersion"
                try
                {
                    string _settingsvalue = string.Empty;
                    if (dtTemp.Rows.Count > 0)
                    {
                        _settingsvalue = dtTemp.Rows[0]["sSettingsValue"].ToString();
                    }

                    m_streamWriter.WriteLine("@StartStatementFile");
                    m_streamWriter.WriteLine("@StatementFileVersion|1");
                    m_streamWriter.WriteLine("@StatementFileGenerateDate|" + DateTime.Now.ToString("MM/dd/yyyy"));
                    m_streamWriter.WriteLine("@StatementFileComment|This file was sent from gloStream gloPM " + _settingsvalue + "");
                }
                catch (Exception Ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), false);
                    Ex = null;
                }
                #endregion "gloPMApplicationVersion"
                              
                // For Each Patient 
                for (int j = 0; j < PatientIDs.Count; j++)
                {

                    Int64 PatientID = Convert.ToInt64(PatientIDs[j].ToString());
                    //gloStatment ObjClsgloStatment = new gloStatment();

                    //if (!ObjClsgloStatment.ExcludeFromStatment(PatientID))
                    //{

                        _sqlQuery = string.Empty;
                        _strPatientName = string.Empty;
                        _strPatientCode = string.Empty;
                        _strPatientAddress = string.Empty;
                        _strPracticePhone = string.Empty;
                        _strClinicMsg1 = string.Empty;
                        _strClinicMsg2 = string.Empty;
                        _strClinicPhone = string.Empty;
                        string _strPracticeName = string.Empty;
                        string _strPracticeAddress = string.Empty;
                        string _strPracticeWebsite = string.Empty;
                        string _strPracticeEMail = string.Empty;

                        Int64 AccountID = Convert.ToInt64(AccountIDs[j].ToString());


                        m_streamWriter.WriteLine("@StartStatement");
                        m_streamWriter.WriteLine("@StatementDate|" + Convert.ToDateTime(StatementDate).ToString("MM/dd/yyyy"));

                        #region "Clinic Information "

                        if (dtClinicInfo != null && dtClinicInfo.Rows.Count > 0)
                        {
                            _strPracticeName = dtClinicInfo.Rows[0]["sClinicName"].ToString();
                            _strPracticeAddress = dtClinicInfo.Rows[0]["sAddress1"].ToString() + "|" + dtClinicInfo.Rows[0]["sAddress2"].ToString() + "|" + dtClinicInfo.Rows[0]["sCity"].ToString() + "|" + dtClinicInfo.Rows[0]["sState"].ToString() + "|" + dtClinicInfo.Rows[0]["sZIP"].ToString();
                            _strPracticePhone = dtClinicInfo.Rows[0]["sPhoneNo"].ToString();
                            _strPracticeWebsite = dtClinicInfo.Rows[0]["sURL"].ToString();
                            _strPracticeEMail = dtClinicInfo.Rows[0]["sEmail"].ToString();
                        }


                        if (_strPracticePhone != "")
                        {
                            if (dtClinicInfo != null && dtClinicInfo.Rows.Count > 0)
                            {
                                _strPracticePhone = dtClinicInfo.Rows[0]["sPhoneNo"].ToString();
                                if (_strPracticePhone != "")
                                    _strPracticePhone = "(" + _strPracticePhone.Substring(0, 3) + ") " + _strPracticePhone.Substring(3, 3) + "-" + _strPracticePhone.Substring(6, _strPracticePhone.Length - 6);
                            }
                        }
                        else
                        {
                            if (dtBillingInfo != null && dtBillingInfo.Rows.Count > 0)
                            {
                                _strPracticePhone = dtBillingInfo.Rows[0]["sBillingContactPhone"].ToString();
                                if (_strPracticePhone != "")
                                    _strPracticePhone = "(" + _strPracticePhone.Substring(0, 3) + ") " + _strPracticePhone.Substring(3, 3) + "-" + _strPracticePhone.Substring(6, _strPracticePhone.Length - 6);
                            }
                        }

                        if (_strPracticeEMail != "")
                        {
                            if (dtClinicInfo != null && dtClinicInfo.Rows.Count > 0)
                            { _strPracticeEMail = dtClinicInfo.Rows[0]["sEmail"].ToString(); }
                        }
                        else
                        {
                            if (dtBillingInfo != null && dtBillingInfo.Rows.Count > 0)
                            { _strPracticeEMail = dtBillingInfo.Rows[0]["sBillingEmail"].ToString(); }
                        }

                        if (_strPracticeWebsite != "")
                        {
                            if (dtClinicInfo != null && dtClinicInfo.Rows.Count > 0)
                            { _strPracticeWebsite = dtClinicInfo.Rows[0]["sURL"].ToString(); }
                        }
                        else
                        {
                            if (dtBillingInfo != null && dtBillingInfo.Rows.Count > 0)
                            { _strPracticeWebsite = dtBillingInfo.Rows[0]["sBillingURL"].ToString(); }
                        }


                        m_streamWriter.WriteLine("@PracticeName|" + _strPracticeName);
                        m_streamWriter.WriteLine("@PracticeAddress|" + _strPracticeAddress);
                        m_streamWriter.WriteLine("@PracticePhone|" + _strPracticePhone);
                        m_streamWriter.WriteLine("@PracticeWebsite|" + _strPracticeWebsite);
                        m_streamWriter.WriteLine("@PracticeEMail|" + _strPracticeEMail);
                        m_streamWriter.WriteLine("@PracticeMisc|");

                        #endregion "Clinic Information "

                        #region "Remit Information"
                        try
                        {
                            int _StatementCount = 0;
                            _StatementCount = GetStatementCount(AccountID, gloDateMaster.gloDate.DateAsNumber(StatementDate));

                            //if (_StatementCount > 0)
                            //{
                            //    _StatementCount = _StatementCount - 1;
                            //}

                            dtTemp = new DataTable();
                            _sqlQuery = "EXECUTE BL_Statement_RemitInfo_Ret " + _CriteriaId + "," + _StatementCount + "," + _ClinicID + "," + PatientID + "," + AccountID;
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
                            _sqlQuery = "SELECT [nPatientID] ,[sPatientCode] ,[sFirstName],[sMiddleName] ,[sLastName], sAddressLine1 + '|' + sAddressLine2 + '|' + sCity + '|' + sState + '|'+ sZip + '|'+ sCountry As PatientAddress " +
                                        " FROM [dbo].[Patient] WITH (NOLOCK) where nClinicID=" + _ClinicID + "AND  nPatientID=" + PatientID;

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
                            //_sqlQuery = "select top(1) nPatientContactID,isnull(Patient_OtherContacts.sFirstName,'') as GuarantorFName,isnull(Patient_OtherContacts.sMiddleName,'') as GuarantorMName, " +
                            //            " isnull(Patient_OtherContacts.sLastName,'') as GuarantorLName, " +
                            //            " CASE Patient_OtherContacts.sAddressLine1 WHEN '' THEN '' WHEN NULL THEN '' ELSE Patient_OtherContacts.sAddressLine1 END AS GuarantorAdd1,  " +
                            //            " CASE Patient_OtherContacts.sAddressLine2 WHEN '' THEN '' WHEN NULL THEN '' ELSE Patient_OtherContacts.sAddressLine2 END AS GuarantorAdd2, " +
                            //            " CASE Patient_OtherContacts.sCity WHEN '' THEN '' WHEN NULL THEN '-' ELSE Patient_OtherContacts.sCity END AS GuarantorCity, " +
                            //            " CASE Patient_OtherContacts.sState WHEN '' THEN '' WHEN NULL THEN '-' ELSE Patient_OtherContacts.sState END AS GuarantorState, " +
                            //            " CASE Patient_OtherContacts.sZIP WHEN '' THEN '' WHEN NULL THEN '' ELSE Patient_OtherContacts.sZIP END AS GuarantorZip, " +
                            //            " Patient_OtherContacts.nGuarantorAsPatientID AS GuarantorAccountNo, " +
                            //            " Patient.sPatientCode As nPatientCode from Patient_OtherContacts WITH (NOLOCK) Inner Join Patient WITH (NOLOCK) On Patient.nPatientId = Patient_OtherContacts.nPatientId " +
                            //            " WHERE  Patient_OtherContacts.nClinicID=" + _ClinicID + " AND Patient_OtherContacts.nPatientID=" + PatientID + " order by  Patient_OtherContacts.nPatientContactTypeFlag,Patient_OtherContacts.nPatientContactID";// + " and nPatientContactTypeFlag =4 ";

                            //code added by SaiKrishna 
                            _sqlQuery = "Select isnull(sFirstName,'') as GuarantorFName,isnull(sMiddleName,'') as GuarantorMName, " +
                                        " isnull(sLastName,'') as GuarantorLName, " +
                                        " CASE sAddressLine1 WHEN '' THEN '' WHEN NULL THEN '' ELSE sAddressLine1 END AS GuarantorAdd1,  " +
                                        " CASE sAddressLine2 WHEN '' THEN '' WHEN NULL THEN '' ELSE sAddressLine2 END AS GuarantorAdd2, " +
                                        " CASE sCity WHEN '' THEN '' WHEN NULL THEN '-' ELSE sCity END AS GuarantorCity, " +
                                        " CASE sState WHEN '' THEN '' WHEN NULL THEN '-' ELSE sState END AS GuarantorState, " +
                                        " CASE sZIP WHEN '' THEN '' WHEN NULL THEN '' ELSE sZIP END AS GuarantorZip, " +
                                        " CASE sCountry WHEN '' THEN '' WHEN NULL THEN '' ELSE sCountry END AS GuarantorCountry , " +
                                        " sAccountNo As nPatientCode " +
                                        " From PA_Accounts " +
                                        " WHERE nPAccountID = " + AccountID + " and nClinicID=" + _ClinicID;

                            dtTemp = new DataTable();
                            oDB.Retrive_Query(_sqlQuery, out dtTemp);

                            string _strGuarantorName = string.Empty;
                            string _strGuarantorAddress = string.Empty;

                            if (dtTemp != null && dtTemp.Rows.Count > 0)
                            {
                                _strGuarantorName = dtTemp.Rows[0]["GuarantorFName"].ToString() + "|" + dtTemp.Rows[0]["GuarantorMName"].ToString() + "|" + dtTemp.Rows[0]["GuarantorLName"].ToString();
                                _strGuarantorAddress = dtTemp.Rows[0]["GuarantorAdd1"].ToString() + "|" + dtTemp.Rows[0]["GuarantorAdd2"].ToString() + "|" + dtTemp.Rows[0]["GuarantorCity"].ToString() + "|" + dtTemp.Rows[0]["GuarantorState"].ToString() + "|" + dtTemp.Rows[0]["GuarantorZip"].ToString() + "|" + dtTemp.Rows[0]["GuarantorCountry"].ToString();
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
                        oDBParameters.Clear();
                        oDBParameters.Add("@nPatientID", 0, ParameterDirection.Input, SqlDbType.BigInt);
                        oDBParameters.Add("@dtDate", StatementDate, ParameterDirection.Input, SqlDbType.DateTime);
                        oDBParameters.Add("@nPAccountID", AccountID, ParameterDirection.Input, SqlDbType.BigInt);
                        oDB.Retrive("PA_BL_ElectronicStatement_LastPatientRemit_V2", oDBParameters, out dtTemp);



                        string _strLastPatientPaymentDate = string.Empty;
                        string _strLastPatientPaymentAmt = string.Empty;
                        if (dtTemp.Rows.Count > 0)
                        {
                            _strLastPatientPaymentDate = Convert.ToString(dtTemp.Rows[0]["Closedate"]);
                            _strLastPatientPaymentAmt = Convert.ToString(dtTemp.Rows[0]["dReceiptAmount"]);
                        }

                        m_streamWriter.WriteLine("@Patient|" + _strPatientName);
                        m_streamWriter.WriteLine("@LastPatientPayment|" + _strLastPatientPaymentDate + "|" + _strLastPatientPaymentAmt);

                        #endregion  "Patient Name and Last Remit"


                        string _sActionCode = "";
                        Decimal _InsurancePaid = 0;

                        DataTable dtChargeLines = new DataTable();
                        DataTable dtPaymentLines = new DataTable();
                        DataTable dtNoteLines = new DataTable();
                        _dsLocal = new DataSet();

                        oDBParameters.Clear();
                        oDBParameters.Add("@nPatientID", PatientID, ParameterDirection.Input, SqlDbType.BigInt);
                        oDBParameters.Add("@dtDate", StatementDate, ParameterDirection.Input, SqlDbType.DateTime);
                        oDBParameters.Add("@nPAccountID", AccountID, ParameterDirection.Input, SqlDbType.BigInt);
                        oDBParameters.Add("@nCriteriaId", _CriteriaId, ParameterDirection.Input, SqlDbType.BigInt);
                        oDB.Retrive("PA_BL_ElectronicStatement_ChargeLineDetails_V2", oDBParameters, out _dsLocal);

                        if (_dsLocal.Tables.Count >= 1)
                        {
                            dtNoteLines = _dsLocal.Tables[0];
                        }

                        if (_dsLocal.Tables.Count >= 2)
                        {
                            dtChargeLines = _dsLocal.Tables[1];
                        }

                        if (_dsLocal.Tables.Count >= 3)
                        {
                            dtPaymentLines = _dsLocal.Tables[2];
                        }


                        //For Each Charge Line
                        _PatientDue = 0;
                        _InsurenceDue = 0;
                        _TotalCharges = 0;
                        _TotalPaymentsAndAdjustmnets = 0;
                        for (int i = 0; i < dtChargeLines.Rows.Count; i++)
                        {

                            #region "Charge Line"
                            //----------------------------- Charge Line Start --------------------------------//

                            _sActionCode = Convert.ToString(dtChargeLines.Rows[i]["ActionCode"]);
                            _InsurancePaid = Convert.ToDecimal(dtChargeLines.Rows[i]["InsurancePaid"]);
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

                            //----------------------------- Payment Line Start --------------------------------//
                            DataTable dtTempPaymentLines = new DataTable();

                            //IEnumerable<DataRow> result = from PaymentLines in dtPaymentLines.AsEnumerable()
                            //                                          where PaymentLines.Field<Int64>("nBillingTransactionDetailID").Equals(Convert.ToInt64(dtChargeLines.Rows[i]["nBillingTransactionDetailID"]))
                            //                                          && PaymentLines.Field<Int64>("nBillingTransactionID").Equals(Convert.ToString(dtChargeLines.Rows[i]["nBillingTransactionID"]))
                            //                                          select PaymentLines;
                            //dtTempPaymentLines.Clear();
                            ////result.CopyToDataTable(dtTempPaymentLines, LoadOption.OverwriteChanges);
                            //dtTempPaymentLines = result.CopyToDataTable();

                            dtTempPaymentLines = dtPaymentLines.Clone();
                            DataRow[] filteredPaymentLines = dtPaymentLines.Select("nBillingTransactionDetailID = " + Convert.ToInt64(dtChargeLines.Rows[i]["nBillingTransactionDetailID"]) + " AND nBillingTransactionID = " + Convert.ToInt64(dtChargeLines.Rows[i]["nBillingTransactionID"]));
                            foreach (DataRow filteredPaymentLine in filteredPaymentLines)
                            {
                                dtTempPaymentLines.ImportRow(filteredPaymentLine);
                            }
                            string PaymentParty = "";
                            for (int k = 0; k < dtTempPaymentLines.Rows.Count; k++)
                            {
                                if (Convert.ToString(dtTempPaymentLines.Rows[k]["PaymentType"]) == "Payment")
                                {
                                    m_streamWriter.WriteLine(" @PaymentLineStart");
                                    m_streamWriter.WriteLine("   @Date|" + Convert.ToDateTime(dtTempPaymentLines.Rows[k]["CloseDate"]).ToString("MM/dd/yyyy"));

                                    PaymentParty = "";
                                    if (Convert.ToString(dtTempPaymentLines.Rows[k]["Source"]) == "2")
                                        PaymentParty = "I";
                                    else
                                        PaymentParty = "P";

                                    m_streamWriter.WriteLine("   @Source|" + PaymentParty);
                                    m_streamWriter.WriteLine("   @Description|" + Convert.ToString(dtTempPaymentLines.Rows[k]["sDescription"]));
                                    m_streamWriter.WriteLine("   @Amount|" + Convert.ToString(dtTempPaymentLines.Rows[k]["nAmount"]));
                                    m_streamWriter.WriteLine(" @PaymentLineEnd");

                                    if (Convert.ToString(dtTempPaymentLines.Rows[k]["nAmount"]) != "")
                                        _TotalPaymentsAndAdjustmnets += Convert.ToDecimal(dtTempPaymentLines.Rows[k]["nAmount"]);
                                }
                                if (Convert.ToString(dtTempPaymentLines.Rows[k]["PaymentType"]) == "Adjustment")
                                {
                                    m_streamWriter.WriteLine(" @AdjustmentLineStart");
                                    m_streamWriter.WriteLine("   @Date|" + Convert.ToDateTime(dtTempPaymentLines.Rows[k]["CloseDate"]).ToString("MM/dd/yyyy"));

                                    PaymentParty = "";
                                    if (Convert.ToString(dtTempPaymentLines.Rows[k]["Source"]) == "2")
                                        PaymentParty = "I";
                                    else
                                        PaymentParty = "P";

                                    m_streamWriter.WriteLine("   @Source|" + PaymentParty);
                                    m_streamWriter.WriteLine("   @Description|" + Convert.ToString(dtTempPaymentLines.Rows[k]["sDescription"]));
                                    m_streamWriter.WriteLine("   @Amount|" + Convert.ToString(dtTempPaymentLines.Rows[k]["nAmount"]));
                                    m_streamWriter.WriteLine(" @AdjustmentLineEnd");
                                    if (Convert.ToString(dtTempPaymentLines.Rows[k]["nAmount"]) != "")
                                        _TotalPaymentsAndAdjustmnets += Convert.ToDecimal(dtTempPaymentLines.Rows[k]["nAmount"]);
                                }
                            }
                            //----------------------------- Payment Line End --------------------------------//

                            #endregion "Payment and Adjustments"

                            #region "Notes"

                            //------------------------------------Line Notes Start-----------------------------//
                            DataTable dtTempNoteLines = new DataTable();
                            dtTempNoteLines = dtNoteLines.Clone();
                            dtTempNoteLines.Clear();
                            DataRow[] filteredNoteLines = dtNoteLines.Select("nBillingTransactionDetailID = " + Convert.ToInt64(dtChargeLines.Rows[i]["nBillingTransactionDetailID"]) + " AND nBillingTransactionID = " + Convert.ToInt64(dtChargeLines.Rows[i]["nBillingTransactionID"]));
                            foreach (DataRow filteredNoteLine in filteredNoteLines)
                            {
                                dtTempNoteLines.ImportRow(filteredNoteLine);
                            }
                            //EnumerableRowCollection<DataRow> NoteLinesResult = from NoteLines in dtNoteLines.AsEnumerable()
                            //                                                   where NoteLines.Field<Int64>("nBillingTransactionDetailID").Equals(Convert.ToInt64(dtChargeLines.Rows[i]["nBillingTransactionDetailID"]))
                            //                                                    && NoteLines.Field<Int64>("nBillingTransactionID").Equals(Convert.ToInt64(dtChargeLines.Rows[i]["nBillingTransactionID"]))
                            //                                                   select NoteLines;
                            //dtTempNoteLines.Clear();
                            //NoteLinesResult.CopyToDataTable(dtTempNoteLines, LoadOption.OverwriteChanges);

                            for (int k = 0; k < dtTempNoteLines.Rows.Count; k++)
                            {
                                if (Convert.ToString(dtTempNoteLines.Rows[k]["LineNote"]) != "")
                                {
                                    if (_sActionCode == "R" && _InsurancePaid == 0)
                                    { break; }
                                    else
                                    {
                                            m_streamWriter.WriteLine(" @NoteLineStart");
                                            m_streamWriter.WriteLine("   @Date|" + Convert.ToString(dtTempNoteLines.Rows[k]["LineNoteCloseDate"]));
                                            m_streamWriter.WriteLine("   @Description|" + Convert.ToString(dtTempNoteLines.Rows[k]["LineNote"]));
                                            m_streamWriter.WriteLine(" @NoteLineEnd");
                                        
                                    }
                                }
                            }
                            //------------------------------------Line Notes End-----------------------------//

                            #endregion "Notes"

                        } ////For Each Charge Line End

                        #region "Fetch Ageing Bucket Info and patient account balance"

                        DataTable dtStatementSummary = new DataTable();
                        DataTable dtAgingBucket = new DataTable();

                        oDBParameters.Clear();
                        if (PatientID == 0)
                        {
                            oDBParameters.Add("@nPatientID", DBNull.Value, ParameterDirection.Input, SqlDbType.BigInt);
                        }
                        else
                        {
                            oDBParameters.Add("@nPatientID", PatientID, ParameterDirection.Input, SqlDbType.BigInt);
                        }
                        oDBParameters.Add("@dtDate", gloDateMaster.gloDate.DateAsDate(gloDateMaster.gloDate.DateAsNumber(StatementDate)), ParameterDirection.Input, SqlDbType.DateTime);
                        oDBParameters.Add("@nClinicID", _ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                        oDBParameters.Add("@nPAccountID", AccountID, ParameterDirection.Input, SqlDbType.BigInt);
                        oDB.Retrive("PA_BL_SELECT_AgingBuckets_V2", oDBParameters, out _dsLocal);

                        if (_dsLocal.Tables.Count >= 1)
                        {
                            dtAgingBucket = _dsLocal.Tables[0];
                        }
                        if (_dsLocal.Tables.Count >= 2)
                        {
                            dtStatementSummary = _dsLocal.Tables[1];
                        }

                        #region "Statement Totals"

                        m_streamWriter.WriteLine("@StatementTotalChargeAmount|" + Convert.ToDecimal(_TotalCharges).ToString("#0.00"));
                        m_streamWriter.WriteLine("@StatementTotalPaymentsandAdjustments|" + Convert.ToDecimal(_TotalPaymentsAndAdjustmnets).ToString("#0.00"));
                        m_streamWriter.WriteLine("@StatementTotalChargeInsPending|" + Convert.ToDecimal(_InsurenceDue).ToString("#0.00"));
                        m_streamWriter.WriteLine("@StatementTotalChargePatientDue|" + Convert.ToDecimal(_PatientDue).ToString("#0.00"));


                        if (dtStatementSummary.Rows.Count > 0)
                        {
                            m_streamWriter.WriteLine("@StatementAvailablePayments|" + Convert.ToDecimal(dtStatementSummary.Rows[0]["AvailableReserve"]));
                            m_streamWriter.WriteLine("@StatementTotalPatientDue|" + (Convert.ToDecimal(dtStatementSummary.Rows[0]["PatientDue"]) - Convert.ToDecimal(dtStatementSummary.Rows[0]["AvailableReserve"]))); //Convert.ToString(dtStatementSummary.Rows[0][""])

                        }

                        #endregion  "Statement Totals"

                        #region "Aging Buckets"

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

                        #endregion

                        #endregion "Fetch Ageing Bucket Info and patient account balance"

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
                                       " FROM AS_Appointment_MST WITH (NOLOCK) INNER JOIN  AS_Appointment_DTL WITH (NOLOCK) ON AS_Appointment_MST.nMSTAppointmentID = AS_Appointment_DTL.nMSTAppointmentID INNER JOIN " +
                                       " Patient WITH (NOLOCK) ON AS_Appointment_MST.nPatientID = Patient.nPatientID INNER JOIN " +
                                       " AB_Location WITH (NOLOCK) ON AS_Appointment_DTL.nLocationID = AB_Location.nLocationID WHERE CONVERT(DATETIME,CONVERT(VARCHAR,AS_Appointment_DTL.dtStartDate),101) > CONVERT(DATETIME,CONVERT(VARCHAR, " + gloDateMaster.gloDate.DateAsNumber(StatementDate) + " ),101) AND  Patient.nPatientID = " + PatientID + " AND Patient.nClinicID =" + _ClinicID +
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

                    //}
                }// For Each Patient end
                m_streamWriter.WriteLine("@EndStatementFile");
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                ex = null;
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); }
                if (oDBParameters != null) { oDBParameters.Dispose(); }
                m_streamWriter.Flush();
                m_streamWriter.Close();
            }

        }

        /// <summary>
        /// This method is called When PAF feature is on.
        /// </summary>
        /// <param name="PatientIDs"></param>
        /// <param name="StatementDate"></param>
        /// <param name="CriteriaID"></param>
        /// <param name="FilePath"></param>
        /// <param name="AccountIDs"></param>
        public void GenerateElectonicClaimFileWithVersion2(ArrayList PatientIDs, string StatementDate, Int64 CriteriaID, string FilePath, ArrayList AccountIDs, bool IsIndividual)
        {
            SqlConnection oConnection = new SqlConnection();
            //SqlCommand sqlCmd = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter();
            DataSet _dsLocal;
            DataTable dtBillingInfo;
            Int64 _CriteriaId = 0;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            FileStream fs = new FileStream(FilePath, FileMode.Create, FileAccess.Write);
            StreamWriter m_streamWriter = new StreamWriter(fs);
            m_streamWriter.BaseStream.Seek(0, SeekOrigin.End);
            try
            {
                oDB.Connect(false);
                dtTemp = new DataTable();
                dtClinicInfo = new DataTable();
                dtBillingInfo = new DataTable();
                _dsLocal = new DataSet();
                              
                oConnection.ConnectionString = _databaseconnectionstring;

                #region "FETCH ELECTRONIC STATEMENT HEADER INFORMATION"
                
                gloStatment ObjClsgloStatment = new gloStatment();
                if (IsBusinessCenterEnable && BusinessCenterID > 0)
                { _CriteriaId = ObjClsgloStatment.GetBusinessCenterDisplaySettings(BusinessCenterID); }
                else { _CriteriaId = ObjClsgloStatment.GetStatmentCriteriaID(); }

                oDBParameters.Clear();
                oDBParameters.Add("@CriteriaID", _CriteriaId, ParameterDirection.Input, SqlDbType.BigInt);              
                oDB.Retrive("PA_GET_ELECTRONIC_STATEMENT_HEADER", oDBParameters, out _dsLocal);
              

                if (_dsLocal.Tables.Count >= 1)
                {
                    dtTemp = _dsLocal.Tables[0];
                }
                if (_dsLocal.Tables.Count >= 2)
                {
                    dtClinicInfo = _dsLocal.Tables[1];
                }
                if (_dsLocal.Tables.Count >= 3)
                {
                    dtBillingInfo = _dsLocal.Tables[2];
                }

                #endregion "FETCH ELECTRONIC STATEMENT HEADER INFORMATION"

                #region "gloPMApplicationVersion"
                try
                {
                    string _settingsvalue = string.Empty;
                    if (dtTemp != null && dtTemp.Rows.Count > 0)
                    {
                        _settingsvalue = dtTemp.Rows[0]["sSettingsValue"].ToString();
                    }

                    m_streamWriter.WriteLine("@StartStatementFile");
                    m_streamWriter.WriteLine("@StatementFileVersion|2");
                    m_streamWriter.WriteLine("@StatementFileGenerateDate|" + DateTime.Now.ToString("MM/dd/yyyy"));
                    m_streamWriter.WriteLine("@StatementFileComment|This file was sent from gloStream gloPM " + _settingsvalue + "");
                }
                catch (Exception Ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), false);
                    Ex = null;
                }
                #endregion "gloPMApplicationVersion"

                //------------------------------gloPMApplicationVersion---------------------//

                // For Each Account
                for (int j = 0; j < AccountIDs.Count; j++)
                {
                    Int64 AccountID = Convert.ToInt64(AccountIDs[j].ToString());
                    //gloStatment ObjClsgloStatment = new gloStatment();

                    //if (!ObjClsgloStatment.ExcludeFromStatment(AccountID))
                    //{
                        //Get Clinic Information
                        string _strPracticeName = string.Empty;
                        string _strPracticeAddress = string.Empty;
                        string _strPracticePhone = string.Empty;
                        string _strPracticeWebsite = string.Empty;
                        string _strPracticeEMail = string.Empty;

                        //Get remitte Information
                        string _strRemitName = string.Empty;
                        string _strRemitAddress = string.Empty;
                        _strClinicMsg1 = string.Empty;
                        _strClinicMsg2 = string.Empty;

                        //Get Patient Information
                        _strPatientName = string.Empty;
                        _strPatientCode = string.Empty;
                        _strPatientAddress = string.Empty;

                        StringBuilder strBuilder = new StringBuilder();

                        m_streamWriter.WriteLine("@StartStatement");
                        m_streamWriter.WriteLine("@StatementDate|" + Convert.ToDateTime(StatementDate).ToString("MM/dd/yyyy"));

                        #region "Clinic Information "

                        if (dtClinicInfo != null && dtClinicInfo.Rows.Count > 0)
                        {
                            _strPracticeName = dtClinicInfo.Rows[0]["sClinicName"].ToString();
                            _strPracticeAddress = dtClinicInfo.Rows[0]["sAddress1"].ToString() + "|" + dtClinicInfo.Rows[0]["sAddress2"].ToString() + "|" + dtClinicInfo.Rows[0]["sCity"].ToString() + "|" + dtClinicInfo.Rows[0]["sState"].ToString() + "|" + dtClinicInfo.Rows[0]["sZIP"].ToString();
                            _strPracticePhone = dtClinicInfo.Rows[0]["sPhoneNo"].ToString();
                            _strPracticeWebsite = dtClinicInfo.Rows[0]["sURL"].ToString();
                            _strPracticeEMail = dtClinicInfo.Rows[0]["sEmail"].ToString();
                        }


                        if (_strPracticePhone != "")
                        {
                            if (dtClinicInfo != null && dtClinicInfo.Rows.Count > 0)
                            {
                                _strPracticePhone = dtClinicInfo.Rows[0]["sPhoneNo"].ToString();
                                if (_strPracticePhone != "")
                                    _strPracticePhone = "(" + _strPracticePhone.Substring(0, 3) + ") " + _strPracticePhone.Substring(3, 3) + "-" + _strPracticePhone.Substring(6, _strPracticePhone.Length - 6);
                            }
                        }
                        else
                        {
                            if (dtBillingInfo != null && dtBillingInfo.Rows.Count > 0)
                            {
                                _strPracticePhone = dtBillingInfo.Rows[0]["sBillingContactPhone"].ToString();
                                if (_strPracticePhone != "")
                                    _strPracticePhone = "(" + _strPracticePhone.Substring(0, 3) + ") " + _strPracticePhone.Substring(3, 3) + "-" + _strPracticePhone.Substring(6, _strPracticePhone.Length - 6);
                            }
                        }

                        if (_strPracticeEMail != "")
                        {
                            if (dtClinicInfo != null && dtClinicInfo.Rows.Count > 0)
                            { _strPracticeEMail = dtClinicInfo.Rows[0]["sEmail"].ToString(); }
                        }
                        else
                        {
                            if (dtBillingInfo != null && dtBillingInfo.Rows.Count > 0)
                            { _strPracticeEMail = dtBillingInfo.Rows[0]["sBillingEmail"].ToString(); }
                        }

                        if (_strPracticeWebsite != "")
                        {
                            if (dtClinicInfo != null && dtClinicInfo.Rows.Count > 0)
                            { _strPracticeWebsite = dtClinicInfo.Rows[0]["sURL"].ToString(); }
                        }
                        else
                        {
                            if (dtBillingInfo != null && dtBillingInfo.Rows.Count > 0)
                            { _strPracticeWebsite = dtBillingInfo.Rows[0]["sBillingURL"].ToString(); }
                        }

                        m_streamWriter.WriteLine("@PracticeName|" + _strPracticeName);
                        m_streamWriter.WriteLine("@PracticeAddress|" + _strPracticeAddress);
                        m_streamWriter.WriteLine("@PracticePhone|" + _strPracticePhone);
                        m_streamWriter.WriteLine("@PracticeWebsite|" + _strPracticeWebsite);
                        m_streamWriter.WriteLine("@PracticeEMail|" + _strPracticeEMail);
                        m_streamWriter.WriteLine("@PracticeMisc|");

                        #endregion "Clinic Information "

                        #region "Remit Information"
                        try
                        {
                            int _StatementCount = 0;
                            _StatementCount = GetStatementCount(AccountID, gloDateMaster.gloDate.DateAsNumber(StatementDate));

                            //if (_StatementCount > 0)
                            //{
                            //    _StatementCount = _StatementCount - 1;
                            //}
                            dtTemp = new DataTable();
                            _sqlQuery = "EXECUTE BL_Statement_RemitInfo_Ret " + _CriteriaId + "," + _StatementCount + "," + _ClinicID + "," + PatientIDs[j] + "," + AccountID;

                            oDB.Retrive_Query(_sqlQuery, out dtTemp);


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

                        #region "Guarantor Information"

                        try
                        {


                            _sqlQuery = "Select isnull(sFirstName,'') as GuarantorFName,isnull(sMiddleName,'') as GuarantorMName, " +
                                        " isnull(sLastName,'') as GuarantorLName, " +
                                        " CASE sAddressLine1 WHEN '' THEN '' WHEN NULL THEN '' ELSE sAddressLine1 END AS GuarantorAdd1,  " +
                                        " CASE sAddressLine2 WHEN '' THEN '' WHEN NULL THEN '' ELSE sAddressLine2 END AS GuarantorAdd2, " +
                                        " CASE sCity WHEN '' THEN '' WHEN NULL THEN '-' ELSE sCity END AS GuarantorCity, " +
                                        " CASE sState WHEN '' THEN '' WHEN NULL THEN '-' ELSE sState END AS GuarantorState, " +
                                        " CASE sZIP WHEN '' THEN '' WHEN NULL THEN '' ELSE sZIP END AS GuarantorZip, " +
                                        " CASE sCountry WHEN '' THEN '' WHEN NULL THEN '' ELSE sCountry END AS GuarantorCountry , " +
                                        " sAccountNo As nPatientCode, sAccountDesc " +
                                        " From PA_Accounts " +
                                        " WHERE nPAccountID = " + AccountID + " and nClinicID=" + _ClinicID;

                            dtTemp = new DataTable();
                            oDB.Retrive_Query(_sqlQuery, out dtTemp);

                            string _strGuarantorName = string.Empty;
                            string _strAccountDescription = string.Empty;
                            string _strGuarantorAddress = string.Empty;

                            if (dtTemp != null && dtTemp.Rows.Count > 0)
                            {
                                _strPatientCode = dtTemp.Rows[0]["nPatientCode"].ToString();
                                _strAccountDescription = dtTemp.Rows[0]["sAccountDesc"].ToString();
                                _strGuarantorName = dtTemp.Rows[0]["GuarantorFName"].ToString() + "|" + dtTemp.Rows[0]["GuarantorMName"].ToString() + "|" + dtTemp.Rows[0]["GuarantorLName"].ToString();
                                _strGuarantorAddress = dtTemp.Rows[0]["GuarantorAdd1"].ToString() + "|" + dtTemp.Rows[0]["GuarantorAdd2"].ToString() + "|" + dtTemp.Rows[0]["GuarantorCity"].ToString() + "|" + dtTemp.Rows[0]["GuarantorState"].ToString() + "|" + dtTemp.Rows[0]["GuarantorZip"].ToString() + "|" + dtTemp.Rows[0]["GuarantorCountry"].ToString();
                            }
                            m_streamWriter.WriteLine("@AccountNumber|" + _strPatientCode);
                            // m_streamWriter.WriteLine("@AccountDesc|" + _strAccountDescription);
                            m_streamWriter.WriteLine("@GuarantorName|" + _strGuarantorName.Trim());
                            m_streamWriter.WriteLine("@GuarantorAddress|" + _strGuarantorAddress.Trim());
                        }
                        catch (Exception Ex)
                        {
                            gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), false);
                            Ex = null;
                        }

                        #endregion "Guarantor Information"

                        #region "Last Account Remit"

                        dtTemp = new DataTable();
                        oDBParameters = new gloDatabaseLayer.DBParameters();
                        oDBParameters.Add("@nPatientID", 0, ParameterDirection.Input, SqlDbType.BigInt);
                        oDBParameters.Add("@dtDate", gloDateMaster.gloDate.DateAsDate(gloDateMaster.gloDate.DateAsNumber(StatementDate)), ParameterDirection.Input, SqlDbType.DateTime);
                        oDBParameters.Add("@nPAccountID", AccountID, ParameterDirection.Input, SqlDbType.BigInt);
                        oDB.Retrive("PA_BL_ElectronicStatement_LastPatientRemit_V2", oDBParameters, out dtTemp);

                        string _strLastAccountPaymentDate = string.Empty;
                        string _strLastAccountPaymentAmt = string.Empty;
                        if (dtTemp.Rows.Count > 0)
                        {
                            _strLastAccountPaymentDate = Convert.ToString(dtTemp.Rows[0]["Closedate"]);
                            _strLastAccountPaymentAmt = Convert.ToString(dtTemp.Rows[0]["dReceiptAmount"]);
                        }


                        m_streamWriter.WriteLine("@LastAccountPayment|" + _strLastAccountPaymentDate + "|" + _strLastAccountPaymentAmt);

                        #endregion  "Last Account Remit"

                        Decimal _StatementTotalPatientDue = 0;
                        Decimal _StatementTotalInsuranceDue = 0;
                        Decimal _StatementTotalCharges = 0;
                        Decimal _StatementTotalPaymentsAndAdjustmnets = 0;
                        //Get patients on Account
                        DataTable dtAccPatients = new DataTable();
                        oDBParameters.Clear();
                        oDBParameters.Add("@nPAccountId", AccountID, ParameterDirection.Input, SqlDbType.BigInt);
                        if (!IsIndividual)
                        {
                            oDBParameters.Add("@nPatientId", 0, ParameterDirection.Input, SqlDbType.BigInt);
                        }
                        else
                        {
                            oDBParameters.Add("@nPatientId", PatientIDs[j], ParameterDirection.Input, SqlDbType.BigInt);
                        }
                        oDB.Retrive("PA_Select_Accounts_Patients", oDBParameters, out dtAccPatients);

                        if (dtAccPatients != null && dtAccPatients.Rows.Count > 0)
                        {

                            for (int gIndex = 0; gIndex < dtAccPatients.Rows.Count; gIndex++)
                            {
                                
                                Int64 nLPatientID = Convert.ToInt64(dtAccPatients.Rows[gIndex]["PatientID"].ToString());
                                Int64 nLAccountID = Convert.ToInt64(dtAccPatients.Rows[gIndex]["PAccountId"].ToString());
 
                                if (GetPatientAccountBalance(nLPatientID, nLAccountID, Convert.ToDateTime(StatementDate)) != 0)
                                {
                                    Int64 PatientID = Convert.ToInt64(dtAccPatients.Rows[gIndex]["PatientID"].ToString());

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

                                    m_streamWriter.WriteLine("@PatientLoopStart");
                                    m_streamWriter.WriteLine("@Patient|" + _strPatientName);

                                    string _sActionCode = "";
                                    Decimal _InsurancePaid = 0;

                                    DataTable dtChargeLines = new DataTable();
                                    DataTable dtPaymentLines = new DataTable();
                                    DataTable dtNoteLines = new DataTable();
                                    _dsLocal = new DataSet();

                                    oDBParameters.Clear();
                                    oDBParameters.Add("@nPatientID", PatientID, ParameterDirection.Input, SqlDbType.BigInt);
                                    oDBParameters.Add("@dtDate", StatementDate, ParameterDirection.Input, SqlDbType.DateTime);
                                    oDBParameters.Add("@nPAccountID", AccountID, ParameterDirection.Input, SqlDbType.BigInt);
                                    oDBParameters.Add("@nCriteriaId", _CriteriaId, ParameterDirection.Input, SqlDbType.BigInt);
                                    oDB.Retrive("PA_BL_ElectronicStatement_ChargeLineDetails_V2", oDBParameters, out _dsLocal);


                                    if (_dsLocal.Tables.Count >= 1)
                                    {
                                        dtNoteLines = _dsLocal.Tables[0];
                                    }

                                    if (_dsLocal.Tables.Count >= 2)
                                    {
                                        dtChargeLines = _dsLocal.Tables[1];
                                    }

                                    if (_dsLocal.Tables.Count >= 3)
                                    {
                                        dtPaymentLines = _dsLocal.Tables[2];
                                    }


                                    //For Each Charge Line

                                    _PatientDue = 0;
                                    _InsurenceDue = 0;
                                    _TotalCharges = 0;
                                    _TotalPaymentsAndAdjustmnets = 0;


                                    for (int i = 0; i < dtChargeLines.Rows.Count; i++)
                                    {

                                        #region "Charge Line"
                                        //----------------------------- Charge Line Start --------------------------------//

                                        _sActionCode = Convert.ToString(dtChargeLines.Rows[i]["ActionCode"]);
                                        _InsurancePaid = Convert.ToDecimal(dtChargeLines.Rows[i]["InsurancePaid"]);
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

                                        //----------------------------- Payment Line Start --------------------------------//
                                        DataTable dtTempPaymentLines = new DataTable();

                                        //IEnumerable<DataRow> result = from PaymentLines in dtPaymentLines.AsEnumerable()
                                        //                                          where PaymentLines.Field<Int64>("nBillingTransactionDetailID").Equals(Convert.ToInt64(dtChargeLines.Rows[i]["nBillingTransactionDetailID"]))
                                        //                                          && PaymentLines.Field<Int64>("nBillingTransactionID").Equals(Convert.ToString(dtChargeLines.Rows[i]["nBillingTransactionID"]))
                                        //                                          select PaymentLines;
                                        //dtTempPaymentLines.Clear();
                                        ////result.CopyToDataTable(dtTempPaymentLines, LoadOption.OverwriteChanges);
                                        //dtTempPaymentLines = result.CopyToDataTable();

                                        dtTempPaymentLines = dtPaymentLines.Clone();
                                        DataRow[] filteredPaymentLines = dtPaymentLines.Select("nBillingTransactionDetailID = " + Convert.ToInt64(dtChargeLines.Rows[i]["nBillingTransactionDetailID"]) + " AND nBillingTransactionID = " + Convert.ToInt64(dtChargeLines.Rows[i]["nBillingTransactionID"]));
                                        foreach (DataRow filteredPaymentLine in filteredPaymentLines)
                                        {
                                            dtTempPaymentLines.ImportRow(filteredPaymentLine);
                                        }

                                        string PaymentParty = "";
                                        for (int k = 0; k < dtTempPaymentLines.Rows.Count; k++)
                                        {
                                            if (Convert.ToString(dtTempPaymentLines.Rows[k]["PaymentType"]) == "Payment")
                                            {
                                                m_streamWriter.WriteLine(" @PaymentLineStart");
                                                m_streamWriter.WriteLine("   @Date|" + Convert.ToDateTime(dtTempPaymentLines.Rows[k]["CloseDate"]).ToString("MM/dd/yyyy"));

                                                PaymentParty = "";
                                                if (Convert.ToString(dtTempPaymentLines.Rows[k]["Source"]) == "2")
                                                    PaymentParty = "I";
                                                else
                                                    PaymentParty = "P";

                                                m_streamWriter.WriteLine("   @Source|" + PaymentParty);
                                                m_streamWriter.WriteLine("   @Description|" + Convert.ToString(dtTempPaymentLines.Rows[k]["sDescription"]));
                                                m_streamWriter.WriteLine("   @Amount|" + Convert.ToString(dtTempPaymentLines.Rows[k]["nAmount"]));
                                                m_streamWriter.WriteLine(" @PaymentLineEnd");

                                                if (Convert.ToString(dtTempPaymentLines.Rows[k]["nAmount"]) != "")
                                                    _TotalPaymentsAndAdjustmnets += Convert.ToDecimal(dtTempPaymentLines.Rows[k]["nAmount"]);
                                            }
                                            if (Convert.ToString(dtTempPaymentLines.Rows[k]["PaymentType"]) == "Adjustment")
                                            {
                                                m_streamWriter.WriteLine(" @AdjustmentLineStart");
                                                m_streamWriter.WriteLine("   @Date|" + Convert.ToDateTime(dtTempPaymentLines.Rows[k]["CloseDate"]).ToString("MM/dd/yyyy"));

                                                PaymentParty = "";
                                                if (Convert.ToString(dtTempPaymentLines.Rows[k]["Source"]) == "2")
                                                    PaymentParty = "I";
                                                else
                                                    PaymentParty = "P";

                                                m_streamWriter.WriteLine("   @Source|" + PaymentParty);
                                                m_streamWriter.WriteLine("   @Description|" + Convert.ToString(dtTempPaymentLines.Rows[k]["sDescription"]));
                                                m_streamWriter.WriteLine("   @Amount|" + Convert.ToString(dtTempPaymentLines.Rows[k]["nAmount"]));
                                                m_streamWriter.WriteLine(" @AdjustmentLineEnd");
                                                if (Convert.ToString(dtTempPaymentLines.Rows[k]["nAmount"]) != "")
                                                    _TotalPaymentsAndAdjustmnets += Convert.ToDecimal(dtTempPaymentLines.Rows[k]["nAmount"]);
                                            }
                                        }
                                        //----------------------------- Payment Line End --------------------------------//

                                        #endregion "Payment and Adjustments"

                                        #region "Notes"

                                        //------------------------------------Line Notes Start-----------------------------//
                                        DataTable dtTempNoteLines = new DataTable();
                                        dtTempNoteLines = dtNoteLines.Clone();
                                        dtTempNoteLines.Clear();
                                        DataRow[] filteredNoteLines = dtNoteLines.Select("nBillingTransactionDetailID = " + Convert.ToInt64(dtChargeLines.Rows[i]["nBillingTransactionDetailID"]) + " AND nBillingTransactionID = " + Convert.ToInt64(dtChargeLines.Rows[i]["nBillingTransactionID"]));
                                        foreach (DataRow filteredNoteLine in filteredNoteLines)
                                        {
                                            dtTempNoteLines.ImportRow(filteredNoteLine);
                                        }
                                        //EnumerableRowCollection<DataRow> NoteLinesResult = from NoteLines in dtNoteLines.AsEnumerable()
                                        //                                                   where NoteLines.Field<Int64>("nBillingTransactionDetailID").Equals(Convert.ToInt64(dtChargeLines.Rows[i]["nBillingTransactionDetailID"]))
                                        //                                                    && NoteLines.Field<Int64>("nBillingTransactionID").Equals(Convert.ToInt64(dtChargeLines.Rows[i]["nBillingTransactionID"]))
                                        //                                                   select NoteLines;
                                        //dtTempNoteLines.Clear();
                                        //NoteLinesResult.CopyToDataTable(dtTempNoteLines, LoadOption.OverwriteChanges);

                                        for (int k = 0; k < dtTempNoteLines.Rows.Count; k++)
                                        {
                                            if (Convert.ToString(dtTempNoteLines.Rows[k]["LineNote"]) != "")
                                            {
                                                if (_sActionCode == "R" && _InsurancePaid == 0)
                                                { break; }
                                                else
                                                {
                                                    m_streamWriter.WriteLine(" @NoteLineStart");
                                                    m_streamWriter.WriteLine("   @Date|" + Convert.ToString(dtTempNoteLines.Rows[k]["LineNoteCloseDate"]));
                                                    m_streamWriter.WriteLine("   @Description|" + Convert.ToString(dtTempNoteLines.Rows[k]["LineNote"]));
                                                    m_streamWriter.WriteLine(" @NoteLineEnd");
                                                }
                                            }
                                        }
                                        //------------------------------------Line Notes End-----------------------------//

                                        #endregion "Notes"

                                    } //For Each Charge Line End

                                    #region "Patient Totals"

                                    //------------------------------------ Patient Totals --------------------------------------------
                                    m_streamWriter.WriteLine("@PatientTotalChargeAmount|" + Convert.ToDecimal(_TotalCharges).ToString("#0.00"));
                                    m_streamWriter.WriteLine("@PatientTotalPaymentsandAdjustments|" + Convert.ToDecimal(_TotalPaymentsAndAdjustmnets).ToString("#0.00"));
                                    m_streamWriter.WriteLine("@PatientTotalChargeInsPending|" + Convert.ToDecimal(_InsurenceDue).ToString("#0.00"));
                                    m_streamWriter.WriteLine("@PatientTotalChargePatientDue|" + Convert.ToDecimal(_PatientDue).ToString("#0.00"));

                                    #endregion  "Patient Totals"

                                    m_streamWriter.WriteLine("@PatientLoopEnd");


                                    _StatementTotalPatientDue += _PatientDue;
                                    _StatementTotalInsuranceDue += _InsurenceDue;
                                    _StatementTotalPaymentsAndAdjustmnets += _TotalPaymentsAndAdjustmnets;
                                    _StatementTotalCharges += _TotalCharges;

                                    #region "Future Appointments"
                                    //-----------------------------Future Appointment Start--------------------------------------------//
                                    DataTable dtFutureAppointment = new DataTable();
                                    try
                                    {
                                        _sqlQuery = " SELECT Top 1 ISNULL(Patient.sFirstName,'')  + '|' + ISNULL(Patient.sLastName,'') sPatientName, ISNULL(AS_Appointment_MST.sASBaseDesc,'') AS sProviderName, " +
                                                   " dbo.CONVERT_TO_DATE( AS_Appointment_DTL.dtStartDate) AS dtStartDate, dbo.CONVERT_TO_TIME( AS_Appointment_DTL.dtStartTime) AS dtStartTime, " +
                                                   " ISNULL(AS_Appointment_DTL.sLocationName,'') AS sLocationName, ISNULL(AB_Location.sAddressLine1,'') + '|' + ISNULL(AB_Location.sAddressLine2,'') + '|'+ ISNULL(AB_Location.sCity,'') + '|'+ ISNULL(AB_Location.sState,'') + '|' + ISNULL(AB_Location.sZIP,'') As LocationAddress " +
                                                   " FROM AS_Appointment_MST INNER JOIN  AS_Appointment_DTL ON AS_Appointment_MST.nMSTAppointmentID = AS_Appointment_DTL.nMSTAppointmentID INNER JOIN " +
                                                   " Patient ON AS_Appointment_MST.nPatientID = Patient.nPatientID INNER JOIN " +
                                                   " AB_Location ON AS_Appointment_DTL.nLocationID = AB_Location.nLocationID WHERE AS_Appointment_DTL.dtStartDate > " + gloDateMaster.gloDate.DateAsNumber(StatementDate) + " AND  Patient.nPatientID = " + PatientID + " AND Patient.nClinicID =" + _ClinicID +
                                                   " AND (AS_Appointment_DTL.nUsedStatus <> 5 AND AS_Appointment_DTL.nUsedStatus <>6 AND AS_Appointment_DTL.nUsedStatus <>7 ) AND AS_Appointment_DTL.nASBaseFlag = 1 Order by AS_Appointment_DTL.dtStartDate, AS_Appointment_DTL.dtStartTime ";
                                        oDB.Retrive_Query(_sqlQuery, out dtFutureAppointment);

                                        for (int l = 0; l < dtFutureAppointment.Rows.Count; l++)
                                        {

                                            strBuilder.Append("@FutureAppointmentStart" + Environment.NewLine);
                                            strBuilder.Append(" @Date|" + dtFutureAppointment.Rows[l]["dtStartDate"].ToString() + Environment.NewLine);
                                            strBuilder.Append(" @Time|" + dtFutureAppointment.Rows[l]["dtStartTime"].ToString() + Environment.NewLine);
                                            strBuilder.Append(" @PatientName|" + _strPatientName + Environment.NewLine);
                                            strBuilder.Append(" @LocationAddress|" + dtFutureAppointment.Rows[l]["LocationAddress"].ToString() + Environment.NewLine);
                                            strBuilder.Append(" @LocationPhone|" + _strPracticePhone + Environment.NewLine);
                                            strBuilder.Append(" @AppointmentProvider|" + dtFutureAppointment.Rows[l]["sProviderName"].ToString() + Environment.NewLine);
                                            strBuilder.Append("@FutureAppointmentEnd");

                                        }
                                    }
                                    catch (Exception Ex)
                                    {
                                        gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), false);
                                        Ex = null;
                                    }
                                    //-----------------------------Future Appointment End--------------------------------------------//

                                    #endregion
                                }
                            }//For Each Patient on Account

                        }

                        #region "Fetch Ageing Bucket Info and patient account balance"

                        DataTable dtStatementSummary = new DataTable();
                        DataTable dtAgingBucket = new DataTable();

                        oDBParameters.Clear();
                        if (Convert.ToInt64(PatientIDs[j]) == 0)
                        {
                            oDBParameters.Add("@nPatientID", DBNull.Value, ParameterDirection.Input, SqlDbType.BigInt);
                        }
                        else
                        {
                            oDBParameters.Add("@nPatientID", PatientIDs[j], ParameterDirection.Input, SqlDbType.BigInt);
                        }
                        oDBParameters.Add("@dtDate", gloDateMaster.gloDate.DateAsDate(gloDateMaster.gloDate.DateAsNumber(StatementDate)), ParameterDirection.Input, SqlDbType.DateTime);
                        oDBParameters.Add("@nClinicID", _ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                        oDBParameters.Add("@nPAccountID", AccountID, ParameterDirection.Input, SqlDbType.BigInt);

                        oDB.Retrive("PA_BL_SELECT_AgingBuckets_V2", oDBParameters, out _dsLocal);


                        if (_dsLocal.Tables.Count >= 1)
                        {
                            dtAgingBucket = _dsLocal.Tables[0];
                        }
                        if (_dsLocal.Tables.Count >= 2)
                        {
                            dtStatementSummary = _dsLocal.Tables[1];
                        }

                        #region "Statement Totals"

                        m_streamWriter.WriteLine("@StatementTotalChargeAmount|" + Convert.ToDecimal(_StatementTotalCharges).ToString("#0.00"));
                        m_streamWriter.WriteLine("@StatementTotalPaymentsandAdjustments|" + Convert.ToDecimal(_StatementTotalPaymentsAndAdjustmnets).ToString("#0.00"));
                        m_streamWriter.WriteLine("@StatementTotalChargeInsPending|" + Convert.ToDecimal(_StatementTotalInsuranceDue).ToString("#0.00"));
                        m_streamWriter.WriteLine("@StatementTotalChargePatientDue|" + Convert.ToDecimal(_StatementTotalPatientDue).ToString("#0.00"));


                        if (dtStatementSummary.Rows.Count > 0)
                        {
                            m_streamWriter.WriteLine("@StatementAvailablePayments|" + Convert.ToDecimal(dtStatementSummary.Rows[0]["AvailableReserve"]));
                            m_streamWriter.WriteLine("@StatementTotalPatientDue|" + (Convert.ToDecimal(dtStatementSummary.Rows[0]["PatientDue"]) - Convert.ToDecimal(dtStatementSummary.Rows[0]["AvailableReserve"]))); //Convert.ToString(dtStatementSummary.Rows[0][""])

                        }

                        #endregion  "Statement Totals"

                        #region "Aging Buckets"

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


                        #endregion

                        #endregion "Fetch Ageing Bucket Info and patient account balance"

                        #region "Clinic Messages"

                        DataTable dtPSNotes = new DataTable();
                        try
                        {
                            Int64 nPatientId = 0;

                            if (Convert.ToInt64(PatientIDs[j]) == 0)
                            {
                                nPatientId = GetAccountOwnerID(Convert.ToInt64(AccountIDs[j]));
                            }
                            else
                            {
                                nPatientId = Convert.ToInt64(PatientIDs[j]);
                            }

                            _sqlQuery = "SELECT sStatementNote FROM Patient_Statement_Notes WHERE nPatientID = " + nPatientId + " AND nClinicID = " + _ClinicID + " AND ( nfromdate <= " + gloDateMaster.gloDate.DateAsNumber(StatementDate) + " AND nToDate >= " + gloDateMaster.gloDate.DateAsNumber(StatementDate) + ")";
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

                        //write appointments to stream
                        m_streamWriter.WriteLine(strBuilder);

                        #region "Last Account Remit"

                        dtTemp = new DataTable();
                        oDBParameters = new gloDatabaseLayer.DBParameters();
                        oDBParameters.Add("@nPatientID", 0, ParameterDirection.Input, SqlDbType.BigInt);
                        oDBParameters.Add("@dtDate", gloDateMaster.gloDate.DateAsDate(gloDateMaster.gloDate.DateAsNumber(StatementDate)), ParameterDirection.Input, SqlDbType.DateTime);
                        oDBParameters.Add("@nPAccountID", AccountID, ParameterDirection.Input, SqlDbType.BigInt);
                        oDB.Retrive("PA_BL_ElectronicStatement_LastPatientRemit_V2", oDBParameters, out dtTemp);


                        string _LastAccountPaymentAmt = string.Empty;
                        string _StrCheckNumber = string.Empty;
                        string _StrCheckDate = string.Empty;

                        if (dtTemp.Rows.Count > 0)
                        {
                            _LastAccountPaymentAmt = Convert.ToString(dtTemp.Rows[0]["dReceiptAmount"]);
                            _StrCheckNumber = Convert.ToString(dtTemp.Rows[0]["sReceiptNo"]);
                            _StrCheckDate = Convert.ToString(dtTemp.Rows[0]["CloseDate"]);

                        }

                        m_streamWriter.WriteLine("@LastAccountPaymentsStart");
                        m_streamWriter.WriteLine(" @Date|" + _StrCheckDate);
                        m_streamWriter.WriteLine(" @ReferenceNumber|" + _StrCheckNumber);
                        m_streamWriter.WriteLine(" @Amount|" + _LastAccountPaymentAmt);
                        m_streamWriter.WriteLine("@LastAccountPaymentsEnd");

                        #endregion  "Last Account Remit"

                        m_streamWriter.WriteLine("@EndStatement");
                    //}
                }
                m_streamWriter.WriteLine("@EndStatementFile");

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); }
                if (oDBParameters != null) { oDBParameters.Dispose(); }
                //if (sqlCmd != null)
                //{
                //    sqlCmd.Parameters.Clear();
                //    sqlCmd.Dispose();
                //    sqlCmd = null;
                //}
                m_streamWriter.Flush();
                m_streamWriter.Close();
            }

        }

        public Int64 GetAccountOwnerID(Int64 nAccountID)
        {

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dtTemp = new DataTable();
            object nID = null;
            Int64 nAccountOwmerID = 0;
            try
            {
                oDB.Connect(false);
                _sqlQuery = "SELECT TOP(1) nPatientID FROM PA_Accounts_Patients WHERE nPAccountID = " + nAccountID + " AND bIsOwnAccount = 1 ";
                nID = oDB.ExecuteScalar_Query(_sqlQuery);
                if (nID != null && Convert.ToString(nID) != "")
                {
                    nAccountOwmerID = Convert.ToInt64(nID);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return 0;
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); }
                if (nID != null) { nID = null; }
                dtTemp.Dispose();
            }

            return nAccountOwmerID;

        }

        
        public decimal GetPatientAccountBalance(Int64 nPatientID, Int64 nPAccountID, DateTime dtCloseDate)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloPMGlobal.DatabaseConnectionString);
            DataTable _dtPatientAccountBalance = new DataTable();
            decimal Balance = 0;
            try
            {
                string _sqlQuery = "";

                _sqlQuery = "SELECT ISNULL(Balance,0) AS Balance FROM dbo.GetPatientStatementBalances(" + nPatientID + "," + nPAccountID + ",'" + dtCloseDate +"')";

                oDB.Connect(false);
                oDB.Retrive_Query(_sqlQuery, out _dtPatientAccountBalance);
                oDB.Disconnect();
                if (_dtPatientAccountBalance != null && _dtPatientAccountBalance.Rows.Count > 0)
                {
                    Balance = Convert.ToDecimal(_dtPatientAccountBalance.Rows[0]["Balance"].ToString());
                }
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
            }
            return Balance;
        }
        #endregion
    }
}
