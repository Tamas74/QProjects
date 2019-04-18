using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using gloAuditTrail;
using gloBilling.gloPriorAuthorization;
using gloSettings;
using C1.Win.C1FlexGrid;
using System.Collections;




namespace gloBilling
{
    public class clsCaseSetup 
    {

        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private Int64 _nCaseID = 0;
        private DataTable _CaseData = new DataTable();
        private DataTable _Diagnosis = new DataTable();
        private DataTable _Insurences = new DataTable();
        public Int64 nCaseID
        {
            get { return _nCaseID; }
            set { _nCaseID = value; }
        }
        public DataTable CaseData
        {
            get { return _CaseData; }
            set { _CaseData = value; }
        }
        public DataTable Diagnosis
        {
            get { return _Diagnosis; }
            set { _Diagnosis = value; }
        }
        public DataTable CurrentCaseInsurences
        {
            get { return _Insurences; }
            set { _Insurences = value; }
        }
        public clsCaseSetup()
        {
            
        }    
        #region "Method"
        public void GetCasesClaimsNCharges(Int64 nCaseId,Int64 _PatientID, out DataSet dsClaimsNCharges)
       {
           gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
           gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
           dsClaimsNCharges = new DataSet();
        //   DataTable dtTotalReserves;
         //  int cntr;

           try
           {
               //Code Added by Sai Krishna for PAF 2011-06-28(yyyy-mm-dd)
               oParameters.Add("@nCaseID", nCaseId, ParameterDirection.Input, SqlDbType.BigInt);
               oParameters.Add("@nPatientID", _PatientID, ParameterDirection.Input, SqlDbType.BigInt);
               oParameters.Add("@nClinicID", appSettings.Get("ClinicID"), ParameterDirection.Input, SqlDbType.BigInt);
               //oParameters.Add("@ZeroBalFlag", IsZeroFlag, ParameterDirection.Input, SqlDbType.Bit);
               //oParameters.Add("@SortByFlag", nSort, ParameterDirection.Input, SqlDbType.Int);
               oDB.Connect(false);
               oDB.Retrive("Patient_Cases_View_Claims_Charges", oParameters, out dsClaimsNCharges);
               oDB.Disconnect();
               dsClaimsNCharges.Tables[0].TableName = "Claims_Charges";


           }
           catch (Exception ex)
           {
               gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
           }
           finally
           {
               if (oDB != null) { oDB.Dispose(); }
           }

       }
        public static DataSet CasesData(Int64 PatientID,Int64 nCaseID)
       {
           gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
           gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
           DataSet dtSet = new DataSet();
           try
           {

               oParameters.Clear();
               oParameters.Add("@nPatientID", PatientID, ParameterDirection.Input, SqlDbType.BigInt);
               oParameters.Add("@nCaseID", nCaseID, ParameterDirection.Input, SqlDbType.BigInt);
               oDB.Connect(false);
               oDB.Retrive("BL_UP_CasesData", oParameters, out dtSet);
               oDB.Disconnect();


           }
           catch (Exception ex)
           {
               gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
           }
           finally
           {
               if (oDB != null)
                   oDB.Dispose();
               if (oParameters != null)
                   oParameters.Dispose();
           }
           return dtSet;
       }
        public Boolean CheckPriorAuthorization(Int64 nPatientID)
       {
           Boolean _isPriorAuthExists = false;
           gloDatabaseLayer.DBLayer oDB = null;
           try
           {
               object count = null;

               oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
               string sqlstring = "SELECT count(nPatientID) FROM  PriorAuthorization_Mst WITH (NOLOCK) WHERE nPatientID=" + nPatientID + " AND bIsActive=1";
               oDB.Connect(false);
               count = oDB.ExecuteScalar_Query(sqlstring);
               oDB.Disconnect();
               if (Convert.ToInt64(count) > 0)
               {
                   _isPriorAuthExists = true;
               }

           }
           catch (Exception ex)
           {
               gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
           }
           finally
           {
               if (oDB != null)
               {
                   oDB.Dispose();
               }
           }
           return _isPriorAuthExists;
       }
        public Boolean getPriorAuthorizationStatus(Int64 _PaID)
       {
           Boolean _isPriorAuthExists = false;
           gloDatabaseLayer.DBLayer oDB = null;
           try
           {
               object count = null;
               oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
               string sqlstring = "SELECT count(nPriorAuthorizationID) FROM  PriorAuthorization_Mst WITH (NOLOCK) WHERE nPriorAuthorizationID=" + _PaID;
               oDB.Connect(false);
               count = oDB.ExecuteScalar_Query(sqlstring);
               oDB.Disconnect();
               if (Convert.ToInt64(count) > 0)
               {
                   _isPriorAuthExists = true;
               }
           }
           catch (Exception ex)
           {
               gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
           }
           finally
           {
               if (oDB != null)
               {
                   oDB.Dispose();
               }
           }
           return _isPriorAuthExists;

       }
        public bool SaveCaes(DataTable dtCases, DataTable dtCasesDiag, DataTable dtCasesIns)
       {
           object _error = null;

           Hashtable oParam = new Hashtable();
           Boolean bResult = true;
           gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
           gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
           string strErrorMessage = string.Empty;
           string sTransResult = "";
           try
           {
               if (dtCases != null && dtCases.Rows.Count > 0)
               {
                   oParameters.Clear();
                   oDB.Connect(false);
                   oParameters.Add("@tvpCaesMst", dtCases, ParameterDirection.Input, SqlDbType.Structured);
                   oParameters.Add("@tvpCaesDiag", dtCasesDiag, ParameterDirection.Input, SqlDbType.Structured);
                   oParameters.Add("@tvpCaesIns", dtCasesIns, ParameterDirection.Input, SqlDbType.Structured);
                   oParameters.Add("@nCaseID", 0, ParameterDirection.Output, SqlDbType.BigInt);
                   //oDB.Execute("Patient_CaseMst_Save_TVP", oParameters, out _error);
                   oParam = oDB.Execute("Patient_CaseMst_Save_TVP", oParameters, true);
                   if (oParam != null)
                       _nCaseID = Convert.ToInt64(oParam["@nCaseID"]);
                   if (_nCaseID != 0)
                   {
                       _CaseData  = dtCases;
                       _Diagnosis = dtCasesDiag;
                      _Insurences = dtCasesIns;
                   }

                   oDB.Disconnect();

                   if (_error != null)
                   {
                       sTransResult = Convert.ToString(_error);
                       if (sTransResult.Trim() == "")
                       {
                           bResult = true;
                       }
                       else
                       {
                           bResult = false;
                       }
                   }
                   else
                   {
                       sTransResult = "";
                       bResult = false;
                   }

               }
           }
           catch (gloDatabaseLayer.DBException dbEx)
           {
               dbEx.ERROR_Log(dbEx.Message);
           }
           catch (Exception ex)
           {
               gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
           }
           finally
           {
               if (oDB != null) { oDB.Dispose(); }
               if (oParameters != null) { oParameters.Dispose(); }
           }

           return bResult;
       }
        public bool IsCaseExistForPatient(Int64 nCurrentCaseID, string CaseName,Int64 nPatientID)
       {
           Object CaseNameCount = new Object();
           gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
           string strQuery = "";
           string strCasename = "";
           if (CaseName.Trim().Contains("'"))
               strCasename = CaseName.Replace("'", "''");
           else
               strCasename = CaseName.Trim();
           int Count = 0;
           bool Result = false;
           try
           {

               strQuery = "Select COUNT(sCaseName) From Patient_Cases_Mst where nPatientId =" + nPatientID + " and sCaseName ='" + strCasename + "' AND nCaseId!= " + nCurrentCaseID;

               oDB.Connect(false);
               CaseNameCount = oDB.ExecuteScalar_Query(strQuery);
               oDB.Disconnect();
               if (CaseNameCount != null)
               {
                   Count = Convert.ToInt16(CaseNameCount);
               }
               if (Count > 0)
                   Result = true;

           }
           catch (gloDatabaseLayer.DBException dbex)
           {
               dbex.ERROR_Log(dbex.ToString());

           }

           finally
           {
               if (oDB != null) { oDB.Dispose(); }
           }
           return Result;
       }
        public static DataSet GetRefferringProviderData(Int64 nPatientID)
       {
           gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
           gloDatabaseLayer.DBParameters oParameters = null;
           DataSet dsChargesData = new DataSet();

           try
           {
               oParameters = new gloDatabaseLayer.DBParameters();              
               oParameters.Add("@nPatientId", nPatientID, ParameterDirection.Input, SqlDbType.BigInt);    
               oDB.Connect(false);
               oDB.Retrive("BL_GET_RefferalForCase", oParameters, out dsChargesData);
               oDB.Disconnect();

           }
           catch (Exception ex)
           {
               gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

           }
           finally
           {
               if (oDB != null) { oDB.Dispose(); }
               if (oParameters != null) { oParameters.Dispose(); }
           }

           return dsChargesData;
       }

        public void OpenSetupCases(Int64 PatientID, Int64 CaseID, Form frmOwner)  //function added for bugid 83503
        {
            frmSetupCases ofrmSetupCases = new frmSetupCases(PatientID, CaseID);

            //17-Nov-15 Aniket: Resolving Bug #90191: gloEMR: OB vitals- View OB vitals winow looses focus and does not allow to perform any operation, user has to kill application
            ofrmSetupCases.ShowDialog(frmOwner);
            if (ofrmSetupCases != null)
            {
                ofrmSetupCases.Close();
                ofrmSetupCases.Dispose();
                ofrmSetupCases = null;
            }
        }

        static public void OpenSetupCasesDialog(Int64 PatientID,Int64 CaseID, Form frmOwner)  //function added for bugid 83503
        {
            frmSetupCases  ofrmSetupCases =new frmSetupCases(PatientID,CaseID);
            
            //17-Nov-15 Aniket: Resolving Bug #90191: gloEMR: OB vitals- View OB vitals winow looses focus and does not allow to perform any operation, user has to kill application
            ofrmSetupCases.ShowDialog(frmOwner);  
            if(ofrmSetupCases!=null)
            {
            ofrmSetupCases.Close();
                ofrmSetupCases.Dispose();
                ofrmSetupCases=null; 
            }
        }
    
        
        
        #endregion 
    }
}
