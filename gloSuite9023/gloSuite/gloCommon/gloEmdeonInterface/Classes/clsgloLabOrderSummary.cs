using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Net;
using System.Xml;
using System.IO;
using gloPatient;
using System.Data;
using System.Data.SqlClient;


namespace gloEmdeonInterface.Classes
{
    class clsgloLabOrderSummary
    {


        #region "Constructor & Distructor"

        public clsgloLabOrderSummary()
        {
            objclsGenral = new clsGeneral(); //Update Log...
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

        ~clsgloLabOrderSummary()
        {
            Dispose(false);
            if (this.objclsGenral != null)
            {
                this.objclsGenral.Dispose();
            }
        }

        #endregion
        # region EmdeonLabOrder

        private clsGeneral objclsGenral;
        private string _gloLabOrder = string.Empty;
        private string _ConnectionString;
        private string _strProvidername = "";
        private string _strProviderExternalCode = "";

        private System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        public string GetPatientInformation(Patient oPatient, long PatientID)
        {
            try
            {
                //to get confriguration settings..
                if (appSettings != null)
                {
                    _ConnectionString = Convert.ToString(appSettings["DataBaseConnectionString"]);
                }
                if (ConfirmNull(oPatient.DemographicsDetail.PatientCode.ToString()))
                {
                    if (ConfirmNull(oPatient.DemographicsDetail.PatientCode.ToString()))
                    {
                        _gloLabOrder = "&P_ACT=" + oPatient.DemographicsDetail.PatientCode.ToString();
                    }
                    if ((ConfirmNull(oPatient.DemographicsDetail.PatientAddress1.ToString())) == true)
                    {
                        _gloLabOrder = _gloLabOrder + "&P_ADR=" + oPatient.DemographicsDetail.PatientAddress1.ToString();
                    }
                    DateTime dt = Convert.ToDateTime(oPatient.DemographicsDetail.PatientDOB);
                    string _date = dt.Date.Date.ToString("MM/dd/yyyy");
                    _gloLabOrder = _gloLabOrder + "&P_DOB=" + _date;

                    if ((ConfirmNull(oPatient.DemographicsDetail.PatientCity.ToString())) == true)
                    {
                        _gloLabOrder = _gloLabOrder + "&P_CIT=" + oPatient.DemographicsDetail.PatientCity.ToString();
                    }
                    _gloLabOrder = _gloLabOrder + "&P_FNM=" + oPatient.DemographicsDetail.PatientFirstName.ToString();
                    _gloLabOrder = _gloLabOrder + "&P_LNM=" + oPatient.DemographicsDetail.PatientLastName.ToString();

                    if ((ConfirmNull(oPatient.DemographicsDetail.PatientMiddleName.ToString())) == true)
                    {
                        _gloLabOrder = _gloLabOrder + "&P_MID=" + oPatient.DemographicsDetail.PatientMiddleName.ToString();
                    }
                    if ((ConfirmNull(oPatient.DemographicsDetail.PatientGender.ToString())) == true)
                    {

                        _gloLabOrder = _gloLabOrder + "&P_SEX=" + GetGender(oPatient.DemographicsDetail.PatientGender.ToString()).ToString();
                    }

                    if ((ConfirmNull(oPatient.DemographicsDetail.PatientSSN.ToString())) == true)
                    {
                        _gloLabOrder = _gloLabOrder + "&P_SSN=" + oPatient.DemographicsDetail.PatientSSN.ToString();
                    }
                    if ((ConfirmNull(oPatient.DemographicsDetail.PatientState.ToString())) == true)
                    {
                        _gloLabOrder = _gloLabOrder + "&P_STA=" + oPatient.DemographicsDetail.PatientState.ToString();
                    }
                    if ((ConfirmNull(oPatient.DemographicsDetail.PatientPhone.ToString())) == true)
                    {
                        _gloLabOrder = _gloLabOrder + "&P_PHN=" + oPatient.DemographicsDetail.PatientPhone.ToString();
                    }
                    if ((ConfirmNull(oPatient.DemographicsDetail.PatientZip.ToString())) == true)
                    {
                        if (oPatient.DemographicsDetail.PatientZip.Length > 4)
                        {
                            _gloLabOrder = _gloLabOrder + "&P_ZIP=" + oPatient.DemographicsDetail.PatientZip.ToString();
                        }
                        else
                        {
                            string _Pzip = oPatient.DemographicsDetail.PatientZip.ToString();
                            _Pzip = string.Concat("0", _Pzip);
                            _gloLabOrder = _gloLabOrder + "&P_ZIP=" + _Pzip.ToString();
                        }
                    }
                    if ((ConfirmNull(oPatient.OccupationDetail.PatientWorkPhone.ToString())) == true)
                    {
                        _gloLabOrder = _gloLabOrder + "&P_PHW=" + oPatient.OccupationDetail.PatientWorkPhone.ToString();
                    }
                    if ((ConfirmNull(oPatient.DemographicsDetail.PatientAddress2.ToString())) == true)
                    {
                        _gloLabOrder = _gloLabOrder + "&P_AD2=" + oPatient.DemographicsDetail.PatientAddress2.ToString();
                    }
                    if (oPatient.InsuranceDetails.InsurancesDetails.Count > 0)
                    {
                        if (ConfirmNull(oPatient.InsuranceDetails.InsurancesDetails[0].RelationshipName.ToString()))
                        {
                            string _rel = GetgeneralRelationshipType(oPatient.InsuranceDetails.InsurancesDetails[0].RelationshipName.ToString());
                            _gloLabOrder = _gloLabOrder + "&P_REL=" + _rel.ToString();
                        }
                    }
                    //*********************************************************************
                    //Guarantor Infromation....Information
                    //**********************************************************************
                    if (oPatient.PatientGuarantors.Count > 0)
                    {
                        if ((ConfirmNull(oPatient.PatientGuarantors[0].AddressLine1.ToString())) == true)
                        {
                            _gloLabOrder = _gloLabOrder + "&G_ADR=" + oPatient.PatientGuarantors[0].AddressLine1.ToString();

                        }
                        if ((ConfirmNull(oPatient.PatientGuarantors[0].AddressLine2.ToString())) == true)
                        {
                            _gloLabOrder = _gloLabOrder + "&G_AD2=" + oPatient.PatientGuarantors[0].AddressLine2.ToString();

                        }
                        if ((ConfirmNull(oPatient.PatientGuarantors[0].Phone.ToString())) == true)
                        {
                            _gloLabOrder = _gloLabOrder + "&G_PHN=" + oPatient.PatientGuarantors[0].Phone.ToString();

                        }
                        if ((ConfirmNull(oPatient.PatientGuarantors[0].City.ToString())) == true)
                        {
                            _gloLabOrder = _gloLabOrder + "&G_CIT=" + oPatient.PatientGuarantors[0].City.ToString();
                        
                        }
                        if ((ConfirmNull(oPatient.PatientGuarantors[0].Gender.ToString())) == true)
                        {
                            _gloLabOrder = _gloLabOrder + "&G_SEX=" + GetGender(oPatient.PatientGuarantors[0].Gender.ToString()).ToString();
                        
                        }
                        if ((ConfirmNull(oPatient.PatientGuarantors[0].Phone.ToString())) == true)
                        {
                            _gloLabOrder = _gloLabOrder + "&G_PHW=" + oPatient.PatientGuarantors[0].Phone.ToString();
                        }
                        if ((ConfirmNull(oPatient.PatientGuarantors[0].LastName.ToString())) == true)
                        {
                            _gloLabOrder = _gloLabOrder + "&G_LNM=" + oPatient.PatientGuarantors[0].LastName.ToString();
                        }
                        if ((ConfirmNull(oPatient.PatientGuarantors[0].MiddleName.ToString())) == true)
                        {
                            _gloLabOrder = _gloLabOrder + "&G_MID=" + oPatient.PatientGuarantors[0].MiddleName.ToString();
                        }
                        if ((ConfirmNull(oPatient.PatientGuarantors[0].Relation.ToString())) == true)
                        {
                            _gloLabOrder = _gloLabOrder + "&G_REL=" + GetgeneralRelationshipType(oPatient.PatientGuarantors[0].Relation.ToString()).ToString();

                        }
                        if ((ConfirmNull(oPatient.PatientGuarantors[0].SSN.ToString())) == true)
                        {
                            _gloLabOrder = _gloLabOrder + "&G_SSN=" + oPatient.PatientGuarantors[0].SSN.ToString();
                        }
                        if ((ConfirmNull(oPatient.PatientGuarantors[0].State.ToString())) == true)
                        {
                            _gloLabOrder = _gloLabOrder + "&G_STA=" + oPatient.PatientGuarantors[0].State.ToString();
                        }
                        if ((ConfirmNull(oPatient.PatientGuarantors[0].Zip.ToString())) == true)
                        {
                            if (oPatient.PatientGuarantors[0].Zip.Length > 4)
                            {
                                _gloLabOrder = _gloLabOrder + "&G_ZIP=" + oPatient.PatientGuarantors[0].Zip.ToString();
                            }
                            else
                            {
                                string _Pzip = oPatient.PatientGuarantors[0].Zip.ToString();
                                _Pzip = string.Concat("0", _Pzip);
                                _gloLabOrder = _gloLabOrder + "&G_ZIP=" + _Pzip.ToString();
                            }

                        }
                        if ((ConfirmNull(oPatient.PatientGuarantors[0].FirstName.ToString())) == true)
                        {
                            _gloLabOrder = _gloLabOrder + "&G_FNM=" + oPatient.PatientGuarantors[0].FirstName.ToString();
                        }

                        //Problem : 00000280
                        //Description : When we launch emdeon lab interface for a patient whose guarantor's DOB is NULL then clinician error occurs as "Dates prior to 1/1/1753 are not permitted". 
                        //Change : If guarantors DOB is NULL then exclude that from login URL.
                        if (!oPatient.PatientGuarantors[0].DOB.Equals(default(DateTime)))
                        {
                            dt = Convert.ToDateTime(oPatient.PatientGuarantors[0].DOB);
                            _date = dt.Date.Date.ToString("MM/dd/yyyy");
                            _gloLabOrder = _gloLabOrder + "&G_DOB=" + _date;
                        }
                    }
                    if (clsEmdeonGeneral.gloLab_BillingType == "t")
                    {
                        if (oPatient.InsuranceDetails.InsurancesDetails.Count > 0)
                        {
                            string _tempIns = "";
                            _tempIns = sendInsurance(clsEmdeonGeneral.gloLab_insuranceIndex, clsEmdeonGeneral.gloLab_SecondaryinsuranceIndex, clsEmdeonGeneral.gloLab_TertiaryinsuranceIndex , oPatient);
                            if (ConfirmNull(_tempIns.ToString()))
                            {
                                _gloLabOrder = _gloLabOrder + _tempIns;
                            }

                        }
                    }

                    if (oPatient.DemographicsDetail.PatientProviderID != 0)
                    {
                        bool _ProviderStatus = GetProviderName(oPatient.DemographicsDetail.PatientProviderID);
                        if (_ProviderStatus == true)
                        {
                            if (ConfirmNull(_strProviderExternalCode.ToString()))
                            {
                                if (ConfirmNull(_strProvidername.ToString()))
                                {
                                    _gloLabOrder = _gloLabOrder + "&O_Phyid=" + _strProviderExternalCode.ToString();
                                }
                            }
                        }
                    }
                    clsEmdeonGeneral.gloLab_BillingType = ""; // Making.. empty.. for next launch


                    //string _strICD9 = GetICD9Codes(PatientID);
                    //if (ConfirmNull(_strICD9.ToString()))
                    //{
                    //    _gloLabOrder = _gloLabOrder + "&O_ICD9=" + _strICD9;
                    //}



                    //*******************************************************************************
                    //Code to check setting  - ‘Preselect diagnosis while placing EMDEON Orders’

                    if (clsEmdeonGeneral.blnEmdeonPreselectDiagnosis)
                    {


                        string _strICD = null;
                        DataSet dsICDCodes = GetICDCodes(PatientID);

                        if (dsICDCodes != null)
                        {
                            if (dsICDCodes.Tables.Count > 0 && dsICDCodes.Tables[0].Rows.Count > 0)
                            {
                                for (int i = 0; i <= dsICDCodes.Tables[0].Rows.Count - 1; i++)
                                {
                                    _strICD = _strICD + "," + dsICDCodes.Tables[0].Rows[i][0];

                                }
                                _strICD = _strICD.Trim(',');

                                if (!string.IsNullOrEmpty(_strICD))
                                {
                                    _gloLabOrder = _gloLabOrder + "&O_ICD9=" + _strICD;
                                }
                            }

                            _strICD = null;

                            if (dsICDCodes.Tables.Count > 1 && dsICDCodes.Tables[1].Rows.Count > 0)
                            {
                                for (int i = 0; i <= dsICDCodes.Tables[1].Rows.Count - 1; i++)
                                {
                                    _strICD = _strICD + "," + dsICDCodes.Tables[1].Rows[i][0];

                                }

                                _strICD = _strICD.Trim(',');
                                
                                if (!string.IsNullOrEmpty(_strICD))
                                {
                                    _gloLabOrder = _gloLabOrder + "&O_ICD10=" + _strICD;
                                }
                            }
                        }

                    }
                    //*******************************************************************************


                    //Added by madan on 20100426-- for selecting the desired lab in OrderSummary screen.
                    string strLabName = getLabName();
                    if (ConfirmNull(strLabName.ToString()))
                    {
                        _gloLabOrder = _gloLabOrder + "&O_Lab=" + strLabName.ToString().Trim();

                    }
                    _gloLabOrder = _gloLabOrder.Replace("#", "%23");
                }

            }
            catch (Exception ex)
            {
    
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            return _gloLabOrder;

        }

        #endregion EmdeonLabOrder

        #region Methods
        //conforming String is not Null;
        private bool ConfirmNull(string strValue)
        {
            clsGeneral objclsGeneral = new clsGeneral();
            bool blnCheck = false;
            try
            {
                if (strValue != null && strValue.ToString().Trim().Length != 0 && strValue.ToString() != "")
                {
                    blnCheck = true;

                }
            }
            catch (Exception ex)
            {

                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            return blnCheck;
        }
        //Insurance RelationType:
        private string GetInsuranceRelationshipType(string relType)
        {
            string _relationType = string.Empty;
            try
            {
                switch (relType.ToLower())
                {
                    case "self":
                        _relationType = "SE";
                        break;
                    case "spouse":
                        _relationType = "SP";
                        break;
                    case "child":
                        _relationType = "CH";
                        break;
                    default:
                        _relationType = "OT";
                        break;
                }
            }
            catch (Exception ex)
            {

                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                _relationType = string.Empty;
            }
            return _relationType;
        }
        //General RelationType:
        private string GetgeneralRelationshipType(string relType)
        {
            string _relationType = string.Empty;
            try
            {
                switch (relType.ToLower())
                {
                    case "self":
                        _relationType = "1";
                        break;
                    case "spouse":
                        _relationType = "2";
                        break;
                    case "child":
                        _relationType = "CH";
                        break;
                    default:
                        _relationType = "8";
                        break;
                }
            }
            catch (Exception ex)
            {

                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                _relationType = string.Empty;
            }
            return _relationType;
        }
        //Gender Check....
        private string GetGender(string _gender)
        {
            try
            {
                switch (_gender)
                {
                    case "Male":
                        _gender = "M";
                        break;
                    case "Female":
                        _gender = "F";
                        break;
                    case "Other":
                        _gender = "U";
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {

                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                _gender = string.Empty;
            }
            return _gender;
        }

        private DataSet GetICDCodes(long _PatientID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_ConnectionString);
            gloDatabaseLayer.DBParameters oDBParameters = null;
            DataSet dsICDCodes = new DataSet();

            try
            {
                oDBParameters = new gloDatabaseLayer.DBParameters();

                oDBParameters.Add("@PatientID", _PatientID, ParameterDirection.Input, SqlDbType.BigInt);

                oDB.Connect(false);
                oDB.Retrive("GetICDCodes",oDBParameters, out dsICDCodes);
                oDB.Disconnect();
            }
            catch (SqlException ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
            finally
            {
                if (oDB != null)
                { oDB.Dispose(); oDB = null; }

                if (oDBParameters != null)
                { oDBParameters.Dispose(); oDBParameters = null; }
            }

            return dsICDCodes;
        }

        private string GetICD9Codes(long _PatientID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_ConnectionString);
            DataTable dtOrder;
            object objOut;
            string _getValues = "";
            try
            {
                oDB.Connect(false);
                if (_PatientID > 0)
                {
                    string strQuery = "Select count(*) from Lab_Order_MST where labom_PatientID='" + _PatientID + "'";
                    objOut = oDB.ExecuteScalar_Query(strQuery);
                    if (objOut != null && Convert.ToInt64(objOut) > 0)
                    {
                        //Modified by madan to get top 6 ICD's for the patient.
                        string QryStr = @"DECLARE @icdS TABLE (
                                        RowID INT IDENTITY, labodtl_Code VARCHAR(255), labom_TransactionDate DAtetime)
                                        INSERT INTO @icdS 
                                        SELECT top 100.tab1.labodtl_Code,labom_TransactionDate --, tab1.labodtl_OrderID
                                        FROM         Lab_Order_MST AS tab3 INNER JOIN
                                        Lab_Order_TestDtl AS tab2 
                                        ON (tab3.labom_OrderID = tab2.labotd_OrderID) 
                                        AND (tab3.labom_dgloLabOrderID IS NOT NULL) 
                                        AND (tab3.labom_ExternalCode IS NOT NULL)
                                        INNER JOIN
                                        Lab_Order_TestDtl_DiagCPT AS tab1 
                                        ON tab2.labotd_TestID = tab1.labodtl_TestID 
                                        AND tab2.labotd_OrderID = tab1.labodtl_OrderID WHERE     
                                        (tab3.labom_PatientID = '" + _PatientID + "') ORDER BY labom_TransactionDate DESC SELECT DISTINCT TOP 6 labodtl_Code,RowID from @icdS ORDER BY RowID";

                        oDB.Retrive_Query(QryStr, out dtOrder);

                        if (dtOrder != null && dtOrder.Rows.Count > 0)
                        {
                            for (int j = 0; j <= dtOrder.Rows.Count - 1; j++)
                            {
                                if (_getValues == "")
                                {
                                    _getValues = _getValues + dtOrder.Rows[j][0].ToString();
                                }
                                else
                                {
                                    _getValues += "," + dtOrder.Rows[j][0].ToString();
                                }
                            }
                        }
                        else
                        { _getValues = string.Empty; }
                    }
                    else
                    {
                        _getValues = string.Empty;
                    }
                }
                else
                { _getValues = string.Empty; }

                oDB.Disconnect();

                dtOrder = null;
                objOut = null;

            }
            catch (SqlException ex)
            {

                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            catch (Exception ex)
            {

                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                _getValues = string.Empty;
            }
            finally
            {
                if (oDB != null)
                { oDB.Dispose(); }
            }
            return _getValues;
        }

        private string GetTestCodes(long _PatientID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_ConnectionString);
            DataTable dtOrder = new DataTable();
            string _getValues = "";
            try
            {
                oDB.Connect(false);
                //string strQuery = "";-- commented to fix the warnings. by madan on 20100520
                string QryStr = "Select DISTINCT labtm_Code from Lab_Test_Mst ta1,Lab_Order_TestDtl ta2,Lab_Order_MST ta3"
                                + " Where ta3.labom_OrderID=ta2.labotd_OrderID"
                                + " and ta2.labotd_OrderID=ta1.labtm_ID"
                                + " AND labom_dgloLabOrderID IS NOT NULL"
                                + " AND labom_ExternalCode IS NOT NULL"
                                + " AND ta3.labom_PatientID ='" + _PatientID + "'";
                oDB.Retrive_Query(QryStr, out dtOrder);

                for (int j = 0; j <= dtOrder.Rows.Count - 1; j++)
                {
                    if (_getValues == "")
                    {
                        _getValues = _getValues + dtOrder.Rows[j][0].ToString();
                    }
                    else
                    {
                        _getValues += "," + dtOrder.Rows[j][0].ToString();
                    }

                }
                oDB.Disconnect();
            }
            catch (SqlException ex)
            {

                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            catch (Exception ex)
            {

                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                _getValues = string.Empty;
            }
            return _getValues;
        }
        //Method added for retriving provider name according to provider id
        private bool GetProviderName(Int64 _providerID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_ConnectionString);
            bool _result = false;
            DataTable oDataTable = new DataTable();
            try
            {
                oDB.Connect(false);
                string strQuery = "select sFirstName,sMiddleName,sLastName,sgloLabId from  dbo.Provider_MST where nProviderID=" + _providerID + "";
                oDB.Retrive_Query(strQuery, out oDataTable);
                if ((oDataTable != null))
                {
                    if (oDataTable.Rows.Count > 0)
                    {
                        if (!(oDataTable.Rows[0]["sFirstName"] == System.DBNull.Value))
                        {
                            _strProvidername = oDataTable.Rows[0]["sFirstName"] + ",";
                        }
                        if (!(oDataTable.Rows[0]["sMiddleName"] == System.DBNull.Value))
                        {
                            _strProvidername = _strProvidername + oDataTable.Rows[0]["sMiddleName"] + ",";
                        }
                        if (!(oDataTable.Rows[0]["sLastName"] == System.DBNull.Value))
                        {
                            _strProvidername = _strProvidername + oDataTable.Rows[0]["sLastName"];
                        }
                        if (!(oDataTable.Rows[0]["sgloLabId"] == System.DBNull.Value))
                        {
                            _strProviderExternalCode = Convert.ToString(oDataTable.Rows[0]["sgloLabId"]);
                        }
                        _result = true;
                    }
                    else
                    {
                        _result = false;
                    }
                }
                else
                {
                    _result = false;
                }
                oDB.Disconnect();
            }
            catch (Exception ex)
            {
                _result = false;
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);

            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
            }
            return _result;
        }
        private string sendInsurance(int _insuranceFlag, int _insuranceFlagSecondary, int _insuranceFlagTertiary, Patient oPatient)
        {
            string _gloInsurance = "";
            DateTime dt;
            string _date = "";

            try
            {
                #region "Primary Insurance"

                    // _gloInsurance = _gloInsurance + "&I_INS=" + oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlag].PayerID.ToString(); / commnted line of ocde by manoj 20120524 to sent ncontactID insted of PayerId to Emdeon version= 7005

                    _gloInsurance = _gloInsurance + "&I_INS=" + oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlag].ContactID.ToString(); // added line of ocde by manoj 20120524 to sent ncontactID insted of PayerId to Emdeon version= 7005

                    if (ConfirmNull(oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlag].AddrressLine1.ToString()))
                    {
                        _gloInsurance = _gloInsurance + "&I_ADR=" + oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlag].AddrressLine1.ToString();
                    }
                    if (ConfirmNull(oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlag].AddrressLine2.ToString()))
                    {
                        _gloInsurance = _gloInsurance + "&I_AD2=" + oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlag].AddrressLine2.ToString();
                    }
                    if (ConfirmNull(oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlag].City.ToString()))
                    {
                        _gloInsurance = _gloInsurance + "&I_CTY=" + oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlag].City.ToString();
                    }
                    if (ConfirmNull(oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlag].InsuranceName.ToString()))
                    {
                        _gloInsurance = _gloInsurance + "&I_NAM=" + oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlag].InsuranceName.ToString();
                    }
                    if (ConfirmNull(oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlag].State.ToString()))
                    {
                        _gloInsurance = _gloInsurance + "&I_STA=" + oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlag].State.ToString();
                    }
                    if (ConfirmNull(oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlag].ZIP.ToString()))
                    {
                        if (oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlag].ZIP.Length > 4)
                        {
                            _gloInsurance = _gloInsurance + "&I_ZIP=" + oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlag].ZIP.ToString();
                        }
                        else
                        {
                            string _Pzip = oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlag].ZIP.ToString();
                            _Pzip = string.Concat("0", _Pzip);
                            _gloInsurance = _gloInsurance + "&I_ZIP=" + _Pzip.ToString();
                        }
                    }
                    //*********************************************************************
                    //Insurance Infromation....Information
                    //**********************************************************************
                    //Check for Subscriber Relationship - Subcriber as company changes by Manoj Jadhav  on 20120529 version 7005 
                    if (oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlag].IsCompnay)
                    {
                        _gloInsurance = _gloInsurance + "&I_REL=" + "EM";
                    }
                    else if (ConfirmNull(oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlag].RelationshipName.ToString()))
                    {
                        _gloInsurance = _gloInsurance + "&I_REL=" + GetInsuranceRelationshipType(oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlag].RelationshipName.ToString()).ToString();
                    }
                    _gloInsurance = _gloInsurance + "&I_COB=" + oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlag].InsuranceFlag.ToString();

                    if (ConfirmNull(oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlag].Group.ToString()))
                    {
                        _gloInsurance = _gloInsurance + "&I_GRP=" + oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlag].Group.ToString();
                    }
                    if (ConfirmNull(oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlag].SubscriberAddr1.ToString()))
                    {
                        _gloInsurance = _gloInsurance + "&I_IADR=" + oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlag].SubscriberAddr1.ToString();
                    }
                    if (ConfirmNull(oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlag].SubscriberAddr2.ToString()))
                    {
                        _gloInsurance = _gloInsurance + "&I_IADR2=" + oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlag].SubscriberAddr2.ToString();
                    }
                    //Check for Subscriber Date of Birth - Subcriber as company changes by Manoj Jadhav  on 20120529 version 7005 
                    if (oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlag].IsCompnay == false &&  ConfirmNull(oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlag].DOB.ToString()))
                    {
                        dt = Convert.ToDateTime(oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlag].DOB);
                        _date = dt.Date.Date.ToString("MM/dd/yyyy");
                        _gloInsurance = _gloInsurance + "&I_IDOB=" + _date;
                    }
                    if (ConfirmNull(oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlag].SubscriberCity.ToString()))
                    {
                        _gloInsurance = _gloInsurance + "&I_ICIT=" + oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlag].SubscriberCity.ToString();
                    }
                    if (ConfirmNull(oPatient.OccupationDetail.EmployerName.ToString()))
                    {
                        _gloInsurance = _gloInsurance + "&I_EMP=" + oPatient.OccupationDetail.EmployerName.ToString();
                    }
                    //Check for Subscriber First Name- Subcriber as company changes by Manoj Jadhav  on 20120529 version 7005 
                    if (oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlag].IsCompnay == true && ConfirmNull(oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlag].SubscriberCompanyLName.ToString()))
                    {
                        _gloInsurance = _gloInsurance + "&I_IFNM=" + oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlag].SubscriberCompanyLName.ToString();
                    }
                    else if ( oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlag].IsCompnay == false && ConfirmNull(oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlag].SubscriberFName.ToString()))
                    {
                        _gloInsurance = _gloInsurance + "&I_IFNM=" + oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlag].SubscriberFName.ToString();
                    }
                    //Check for Subscriber Last Name- Subcriber as company changes by Manoj Jadhav  on 20120529 version 7005 
                    if (oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlag].IsCompnay == true && ConfirmNull(oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlag].SubscriberCompanyLName.ToString()))
                    {
                        _gloInsurance = _gloInsurance + "&I_ILNM=" + oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlag].SubscriberCompanyLName.ToString();
                    }
                    else if (oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlag].IsCompnay == false && ConfirmNull(oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlag].SubscriberLName.ToString()))
                    {
                        _gloInsurance = _gloInsurance + "&I_ILNM=" + oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlag].SubscriberLName.ToString();
                    }
                    //Check for Subscriber Middle Name- Subcriber as company changes by Manoj Jadhav  on 20120529 version 7005 
                    if (oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlag].IsCompnay == false && ConfirmNull(oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlag].SubscriberMName.ToString()))
                    {
                        _gloInsurance = _gloInsurance + "&I_IMID=" + oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlag].SubscriberMName.ToString();
                    }
                    //Check for Subscriber Gender- Subcriber as company changes by Manoj Jadhav  on 20120529 version 7005 
                    if (oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlag].IsCompnay == false && ConfirmNull(oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlag].SubscriberGender.ToString()))
                    {
                        _gloInsurance = _gloInsurance + "&I_ISEX=" + GetGender(oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlag].SubscriberGender.ToString()).ToString();
                    }
                    if (ConfirmNull(oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlag].SubscriberState.ToString()))
                    {
                        _gloInsurance = _gloInsurance + "&I_ISTA=" + oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlag].SubscriberState.ToString();
                    }
                    if (ConfirmNull(oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlag].Phone.ToString()))
                    {
                        _gloInsurance = _gloInsurance + "&I_IPHN=" + oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlag].Phone.ToString();
                    }
                    if (ConfirmNull(oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlag].SubscriberZip.ToString()))
                    {
                        if (oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlag].SubscriberZip.Length > 4)
                        {
                            _gloInsurance = _gloInsurance + "&I_IZIP=" + oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlag].SubscriberZip.ToString();
                        }
                        else
                        {
                            string _Pzip = oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlag].SubscriberZip.ToString();
                            _Pzip = string.Concat("0", _Pzip);
                            _gloInsurance = _gloInsurance + "&I_IZIP=" + _Pzip.ToString();
                        }
                    }
                    if (ConfirmNull(oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlag].SubscriberID.ToString()))
                    {
                        _gloInsurance = _gloInsurance + "&I_IID=" + oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlag].SubscriberID.ToString();
                    }

                #endregion ""

                #region "Secondary Insurance"
                    if (_insuranceFlagSecondary != -1)
                    {
                        _gloInsurance = _gloInsurance + "&I_INS2=" + oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlagSecondary].ContactID.ToString();

                        if (ConfirmNull(oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlagSecondary].AddrressLine1.ToString()))
                        {
                            _gloInsurance = _gloInsurance + "&I_ADR2=" + oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlagSecondary].AddrressLine1.ToString();
                        }
                        if (ConfirmNull(oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlagSecondary].AddrressLine2.ToString()))
                        {
                            _gloInsurance = _gloInsurance + "&I_AD22=" + oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlagSecondary].AddrressLine2.ToString();
                        }
                        if (ConfirmNull(oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlagSecondary].City.ToString()))
                        {
                            _gloInsurance = _gloInsurance + "&I_CTY2=" + oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlagSecondary].City.ToString();
                        }
                        if (ConfirmNull(oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlagSecondary].InsuranceName.ToString()))
                        {
                            _gloInsurance = _gloInsurance + "&I_NAM2=" + oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlagSecondary].InsuranceName.ToString();
                        }
                        if (ConfirmNull(oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlagSecondary].State.ToString()))
                        {
                            _gloInsurance = _gloInsurance + "&I_STA2=" + oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlagSecondary].State.ToString();
                        }
                        if (ConfirmNull(oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlagSecondary].ZIP.ToString()))
                        {
                            if (oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlagSecondary].ZIP.Length > 4)
                            {
                                _gloInsurance = _gloInsurance + "&I_ZIP2=" + oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlagSecondary].ZIP.ToString();
                            }
                            else
                            {
                                string _Pzip = oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlagSecondary].ZIP.ToString();
                                _Pzip = string.Concat("0", _Pzip);
                                _gloInsurance = _gloInsurance + "&I_ZIP2=" + _Pzip.ToString();
                            }
                        }
                        //***************************************
                        //Insurance Infromation....Information
                        //***************************************

                        if (oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlagSecondary].IsCompnay)
                        {
                            _gloInsurance = _gloInsurance + "&I_REL2=" + "EM";
                        }
                        else if (ConfirmNull(oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlagSecondary].RelationshipName.ToString()))
                        {
                            _gloInsurance = _gloInsurance + "&I_REL2=" + GetInsuranceRelationshipType(oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlagSecondary].RelationshipName.ToString()).ToString();
                        }
                        _gloInsurance = _gloInsurance + "&I_COB2=" + oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlagSecondary].InsuranceFlag.ToString();

                        if (ConfirmNull(oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlagSecondary].Group.ToString()))
                        {
                            _gloInsurance = _gloInsurance + "&I_GRP2=" + oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlagSecondary].Group.ToString();
                        }
                        if (ConfirmNull(oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlagSecondary].SubscriberAddr1.ToString()))
                        {
                            //11-May-15 Aniket: Changed the following segment from IADR2 to IADR21 as per the mail with the subject 'Secondary Insurance From gloStream to Emdeon to Labcorp   (External Email'
                            _gloInsurance = _gloInsurance + "&I_IADR21=" + oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlagSecondary].SubscriberAddr1.ToString();
                        }
                        if (ConfirmNull(oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlagSecondary].SubscriberAddr2.ToString()))
                        {
                            _gloInsurance = _gloInsurance + "&I_IADR22=" + oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlagSecondary].SubscriberAddr2.ToString();
                        }
                        //Check for Subscriber Date of Birth - Subcriber
                        if (oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlagSecondary].IsCompnay == false && ConfirmNull(oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlagSecondary].DOB.ToString()))
                        {
                            dt = Convert.ToDateTime(oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlagSecondary].DOB);
                            _date = dt.Date.Date.ToString("MM/dd/yyyy");
                            _gloInsurance = _gloInsurance + "&I_IDOB2=" + _date;
                        }
                        if (ConfirmNull(oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlagSecondary].SubscriberCity.ToString()))
                        {
                            _gloInsurance = _gloInsurance + "&I_ICIT2=" + oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlagSecondary].SubscriberCity.ToString();
                        }
                        if (ConfirmNull(oPatient.OccupationDetail.EmployerName.ToString()))
                        {
                            _gloInsurance = _gloInsurance + "&I_EMP2=" + oPatient.OccupationDetail.EmployerName.ToString();
                        }
                        //Check for Subscriber First Name- Subcriber as company 
                        if (oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlagSecondary].IsCompnay == true && ConfirmNull(oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlagSecondary].SubscriberCompanyLName.ToString()))
                        {
                            _gloInsurance = _gloInsurance + "&I_IFNM2=" + oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlagSecondary].SubscriberCompanyLName.ToString();
                        }
                        else if (oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlagSecondary].IsCompnay == false && ConfirmNull(oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlagSecondary].SubscriberFName.ToString()))
                        {
                            _gloInsurance = _gloInsurance + "&I_IFNM2=" + oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlagSecondary].SubscriberFName.ToString();
                        }
                        //Check for Subscriber Last Name- Subcriber as company 
                        if (oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlagSecondary].IsCompnay == true && ConfirmNull(oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlagSecondary].SubscriberCompanyLName.ToString()))
                        {
                            _gloInsurance = _gloInsurance + "&I_ILNM2=" + oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlagSecondary].SubscriberCompanyLName.ToString();
                        }
                        else if (oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlagSecondary].IsCompnay == false && ConfirmNull(oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlagSecondary].SubscriberLName.ToString()))
                        {
                            _gloInsurance = _gloInsurance + "&I_ILNM2=" + oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlagSecondary].SubscriberLName.ToString();
                        }
                        //Check for Subscriber Middle Name- Subcriber as company changes by Manoj Jadhav  on 20120529 version 7005 
                        if (oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlagSecondary].IsCompnay == false && ConfirmNull(oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlagSecondary].SubscriberMName.ToString()))
                        {
                            _gloInsurance = _gloInsurance + "&I_IMID2=" + oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlagSecondary].SubscriberMName.ToString();
                        }
                        //Check for Subscriber Gender- Subcriber as company 
                        if (oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlagSecondary].IsCompnay == false && ConfirmNull(oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlagSecondary].SubscriberGender.ToString()))
                        {
                            _gloInsurance = _gloInsurance + "&I_ISEX2=" + GetGender(oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlagSecondary].SubscriberGender.ToString()).ToString();
                        }
                        if (ConfirmNull(oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlagSecondary].SubscriberState.ToString()))
                        {
                            _gloInsurance = _gloInsurance + "&I_ISTA2=" + oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlagSecondary].SubscriberState.ToString();
                        }
                        if (ConfirmNull(oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlagSecondary].Phone.ToString()))
                        {
                            _gloInsurance = _gloInsurance + "&I_IPHN2=" + oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlagSecondary].Phone.ToString();
                        }
                        if (ConfirmNull(oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlagSecondary].SubscriberZip.ToString()))
                        {
                            if (oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlagSecondary].SubscriberZip.Length > 4)
                            {
                                _gloInsurance = _gloInsurance + "&I_IZIP2=" + oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlagSecondary].SubscriberZip.ToString();
                            }
                            else
                            {
                                string _Pzip = oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlagSecondary].SubscriberZip.ToString();
                                _Pzip = string.Concat("0", _Pzip);
                                _gloInsurance = _gloInsurance + "&I_IZIP2=" + _Pzip.ToString();
                            }
                        }
                        if (ConfirmNull(oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlagSecondary].SubscriberID.ToString()))
                        {
                            _gloInsurance = _gloInsurance + "&I_IID2=" + oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlagSecondary].SubscriberID.ToString();
                        }
                    }
                    #endregion ""

                #region "Tertiary Insurance"

                    if (_insuranceFlagTertiary != -1)
                    {
                        _gloInsurance = _gloInsurance + "&I_INS3=" + oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlagTertiary].ContactID.ToString();

                        if (ConfirmNull(oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlagTertiary].AddrressLine1.ToString()))
                        {
                            _gloInsurance = _gloInsurance + "&I_ADR3=" + oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlagTertiary].AddrressLine1.ToString();
                        }
                        if (ConfirmNull(oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlagTertiary].AddrressLine2.ToString()))
                        {
                            _gloInsurance = _gloInsurance + "&I_AD23=" + oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlagTertiary].AddrressLine2.ToString();
                        }
                        if (ConfirmNull(oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlagTertiary].City.ToString()))
                        {
                            _gloInsurance = _gloInsurance + "&I_CTY3=" + oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlagTertiary].City.ToString();
                        }
                        if (ConfirmNull(oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlagTertiary].InsuranceName.ToString()))
                        {
                            _gloInsurance = _gloInsurance + "&I_NAM3=" + oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlagTertiary].InsuranceName.ToString();
                        }
                        if (ConfirmNull(oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlagTertiary].State.ToString()))
                        {
                            _gloInsurance = _gloInsurance + "&I_STA3=" + oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlagTertiary].State.ToString();
                        }
                        if (ConfirmNull(oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlagTertiary].ZIP.ToString()))
                        {
                            if (oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlagTertiary].ZIP.Length > 4)
                            {
                                _gloInsurance = _gloInsurance + "&I_ZIP3=" + oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlagTertiary].ZIP.ToString();
                            }
                            else
                            {
                                string _Pzip = oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlagTertiary].ZIP.ToString();
                                _Pzip = string.Concat("0", _Pzip);
                                _gloInsurance = _gloInsurance + "&I_ZIP3=" + _Pzip.ToString();
                            }
                        }
                        //**************************************
                        //Insurance Infromation....Information
                        //**************************************

                        if (oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlagTertiary].IsCompnay)
                        {
                            _gloInsurance = _gloInsurance + "&I_REL3=" + "EM";
                        }
                        else if (ConfirmNull(oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlagTertiary].RelationshipName.ToString()))
                        {
                            _gloInsurance = _gloInsurance + "&I_REL3=" + GetInsuranceRelationshipType(oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlagTertiary].RelationshipName.ToString()).ToString();
                        }
                        _gloInsurance = _gloInsurance + "&I_COB3=" + oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlagTertiary].InsuranceFlag.ToString();

                        if (ConfirmNull(oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlagTertiary].Group.ToString()))
                        {
                            _gloInsurance = _gloInsurance + "&I_GRP3=" + oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlagTertiary].Group.ToString();
                        }
                        if (ConfirmNull(oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlagTertiary].SubscriberAddr1.ToString()))
                        {
                            _gloInsurance = _gloInsurance + "&I_IADR3=" + oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlagTertiary].SubscriberAddr1.ToString();
                        }
                        if (ConfirmNull(oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlagTertiary].SubscriberAddr2.ToString()))
                        {
                            _gloInsurance = _gloInsurance + "&I_IADR32=" + oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlagTertiary].SubscriberAddr2.ToString();
                        }
                        //Check for Subscriber Date of Birth - Subcriber
                        if (oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlagTertiary].IsCompnay == false && ConfirmNull(oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlagTertiary].DOB.ToString()))
                        {
                            dt = Convert.ToDateTime(oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlagTertiary].DOB);
                            _date = dt.Date.Date.ToString("MM/dd/yyyy");
                            _gloInsurance = _gloInsurance + "&I_IDOB3=" + _date;
                        }
                        if (ConfirmNull(oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlagTertiary].SubscriberCity.ToString()))
                        {
                            _gloInsurance = _gloInsurance + "&I_ICIT3=" + oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlagTertiary].SubscriberCity.ToString();
                        }
                        if (ConfirmNull(oPatient.OccupationDetail.EmployerName.ToString()))
                        {
                            _gloInsurance = _gloInsurance + "&I_EMP3=" + oPatient.OccupationDetail.EmployerName.ToString();
                        }
                        //Check for Subscriber First Name- Subcriber as company 
                        if (oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlagTertiary].IsCompnay == true && ConfirmNull(oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlagTertiary].SubscriberCompanyLName.ToString()))
                        {
                            _gloInsurance = _gloInsurance + "&I_IFNM3=" + oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlagTertiary].SubscriberCompanyLName.ToString();
                        }
                        else if (oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlagTertiary].IsCompnay == false && ConfirmNull(oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlagTertiary].SubscriberFName.ToString()))
                        {
                            _gloInsurance = _gloInsurance + "&I_IFNM3=" + oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlagTertiary].SubscriberFName.ToString();
                        }
                        //Check for Subscriber Last Name- Subcriber as company 
                        if (oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlagTertiary].IsCompnay == true && ConfirmNull(oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlagTertiary].SubscriberCompanyLName.ToString()))
                        {
                            _gloInsurance = _gloInsurance + "&I_ILNM3=" + oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlagTertiary].SubscriberCompanyLName.ToString();
                        }
                        else if (oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlagTertiary].IsCompnay == false && ConfirmNull(oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlagTertiary].SubscriberLName.ToString()))
                        {
                            _gloInsurance = _gloInsurance + "&I_ILNM3=" + oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlagTertiary].SubscriberLName.ToString();
                        }
                        //Check for Subscriber Middle Name- Subcriber as company changes by Manoj Jadhav  on 20120529 version 7005 
                        if (oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlagTertiary].IsCompnay == false && ConfirmNull(oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlagTertiary].SubscriberMName.ToString()))
                        {
                            _gloInsurance = _gloInsurance + "&I_IMID3=" + oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlagTertiary].SubscriberMName.ToString();
                        }
                        //Check for Subscriber Gender- Subcriber as company 
                        if (oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlagTertiary].IsCompnay == false && ConfirmNull(oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlagTertiary].SubscriberGender.ToString()))
                        {
                            _gloInsurance = _gloInsurance + "&I_ISEX3=" + GetGender(oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlagTertiary].SubscriberGender.ToString()).ToString();
                        }
                        if (ConfirmNull(oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlagTertiary].SubscriberState.ToString()))
                        {
                            _gloInsurance = _gloInsurance + "&I_ISTA3=" + oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlagTertiary].SubscriberState.ToString();
                        }
                        if (ConfirmNull(oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlagTertiary].Phone.ToString()))
                        {
                            _gloInsurance = _gloInsurance + "&I_IPHN3=" + oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlagTertiary].Phone.ToString();
                        }
                        if (ConfirmNull(oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlagTertiary].SubscriberZip.ToString()))
                        {
                            if (oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlagTertiary].SubscriberZip.Length > 4)
                            {
                                _gloInsurance = _gloInsurance + "&I_IZIP3=" + oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlagTertiary].SubscriberZip.ToString();
                            }
                            else
                            {
                                string _Pzip = oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlagTertiary].SubscriberZip.ToString();
                                _Pzip = string.Concat("0", _Pzip);
                                _gloInsurance = _gloInsurance + "&I_IZIP3=" + _Pzip.ToString();
                            }
                        }
                        if (ConfirmNull(oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlagTertiary].SubscriberID.ToString()))
                        {
                            _gloInsurance = _gloInsurance + "&I_IID3=" + oPatient.InsuranceDetails.InsurancesDetails[_insuranceFlagTertiary].SubscriberID.ToString();
                        }
                    }
                    #endregion ""

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                return "";
            }
            return _gloInsurance;

        }

       

        //Added by madan for getting labname  on 20100426
        private string getLabName()
        {
            string _strLabName = string.Empty;

            gloDatabaseLayer.DBLayer objDbLayer = new gloDatabaseLayer.DBLayer(_ConnectionString);
            object _objResult = new object();
            string strQry = string.Empty;
            try
            {
                objDbLayer.Connect(false);

                strQry = "select sSettingsValue from settings where sSettingsName='GLOLAB LAB NAME'";
                _objResult = objDbLayer.ExecuteScalar_Query(strQry);
                if (_objResult != null)
                {
                    _strLabName = Convert.ToString(_objResult);
                }
                else
                {
                    _strLabName = string.Empty;
                }
                objDbLayer.Disconnect();

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                _strLabName = string.Empty;
            }
            finally
            {
                if (objDbLayer != null)
                {
                    objDbLayer.Dispose();
                }
                strQry = string.Empty;
                _objResult = null;
            }
            return _strLabName;

        } 
        #endregion
      
    }


}
