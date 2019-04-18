using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using gloEMRGeneralLibrary.gloEMRDatabase;
using System.Windows.Forms;  
namespace gloCommunity.Classes
{
    class clsBillingconf
    {

        public DataTable GetICD9()
        {

             DataBaseLayer oDB = new DataBaseLayer();
            try
            {

             string strQuery = "SELECT 'False' as [Select], ISNULL(ICD9.nICD9ID,0) AS nICD9ID, ISNULL(ICD9.sICD9Code,'') AS Code," +
                   "ISNULL(ICD9.sDescription,'') AS Description,ISNULL(ICD9.nSpecialtyID,0) AS nSpecialtyID," +
                   "ICD9.bInActive,CASE ISNULL(bInActive,0) WHEN 0 THEN 'Active'WHEN 1 THEN 'Inactive' END AS Status," +
                   " ISNULL(Specialty_MST.sDescription,'') as specialty "+
                    " , '' AS [Taxonomy Code], '' AS [Taxonomy Description], '' AS [Classification] " +
                    " , '' AS [Type Code], '' AS [Type Description]  ,'' as CPTCode , '' as NDCCode,'' as [Statement Description],'' as Category "+
                    ", '' as [Category Type],'' as [Code Type],'' as [Modifier1 Code],'' as [Modifier2 Code],'' as  [Modifier3 Code],'' as  [Modifier4 Code],'' as [CPT Drug],0.0 as Charges " +
                    " , 0.0 as Allowed,'' as [Revenue Code] ,'' as [Record Type],'ICD9' as [Billing Category],0 AS nUnits,'false' AS bIsTaxable,0 AS nRate,0 as  nClinicFee,'false' AS bIsUseFromFeeSchedule " +
                   " FROM ICD9 WITH (NOLOCK) " +
                   " LEFT OUTER JOIN Specialty_MST WITH (NOLOCK) ON ICD9.nSpecialtyID = Specialty_MST.nSpecialtyID where (ICD9.bIsBlocked = 'false' OR ICD9.bIsBlocked IS NULL)  AND ISNULL(ICD9.nClinicID,1) = " + clsGeneral.gClinicID ;
              DataTable dt = oDB.GetDataTable_Query(strQuery);
              return dt;
            }
            catch(Exception ex)
            {
                //clsGeneral.UpdateLog("Error  while Getting ICD9 from class BillingConf : " + ex.Message.ToString());  


                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                
                return null;
            }

            finally
            {
                oDB.Dispose();
                oDB = null;
            }
          }

        public DataTable GetCPT()
        {
            DataBaseLayer oDB = new DataBaseLayer();
            try
            {
                string strQuery = "Select 'False' as [Select], nCPTID, ISNULL(sCPTCode,'') AS CPTCode,ISNULL(sDescription,'') AS Description,ISNULL(sStatementDesc,'') AS [Statement Description], ISNULL(sSpecialityCode,'')AS specialty," +
                "ISNULL(sCategoryType,'')  + case when ISNULL(sCategoryDesc,'') <>'' then '-' + ISNULL(sCategoryDesc,'')  else '' end AS Category," +
                "ISNULL(sCodeTypeCode,'')  + case when ISNULL(sCodeTypeDesc,'') <>'' then '-' + ISNULL(sCodeTypeDesc,'')  else '' end AS [Code Type]," +
                "ISNULL(sModifier1Code,'') + case when ISNULL(sModifier1Desc,'')<>'' then '-' + ISNULL(sModifier1Desc,'') else '' end AS [Modifier1 Code]," +
                "ISNULL(sModifier2Code,'') + case when ISNULL(sModifier2Desc,'')<>'' then '-' + ISNULL(sModifier2Desc,'') else '' end AS [Modifier2 Code]," +
                "ISNULL(sModifier3Code,'') + case when ISNULL(sModifier3Desc,'')<>'' then '-' + ISNULL(sModifier3Desc,'') else '' end AS [Modifier3 Code]," +
                "ISNULL(sModifier4Code,'') + case when ISNULL(sModifier4Desc,'')<>'' then '-' + ISNULL(sModifier4Desc,'') else '' end AS [Modifier4 Code]," +
                " ISNULL(nUnits,0) AS nUnits , CASE  ISNULL(bIsCPTDrug,'false') WHEN 'FALSE' THEN 'NO' WHEN 'TRUE' THEN 'YES' END  AS [CPT Drug], ISNULL(sNDCCode,'') AS NDCCode, " +
                "ISNULL(bIsTaxable,'false') AS bIsTaxable, ISNULL(nRate,0) AS nRate, ISNULL(nCharges,0) AS Charges, ISNULL(nAllowed,0) AS Allowed, " +
                "ISNULL(nClinicFee,0) AS nClinicFee, ISNULL(bInactive, 'false') AS bInactive,ISNULL(sRevenueCode,'')as [Revenue Code], "+
                  "  '' AS [Taxonomy Code], '' AS [Taxonomy Description], '' AS [Classification] ," +

                "  '' AS [Type Code], '' AS [Type Description] ,'' as [Record Type],'CPT' as [Billing Category], '' as [Category Type],'' as Status,'' as Code, '' AS [Type Code], ISNULL(bIsUseFromFeeSchedule,'false') AS bIsUseFromFeeSchedule  " +
                
                " FROM CPT_MST WITH (NOLOCK) WHERE nClinicID = "+clsGeneral.gClinicID+"";

              
                
//sCPTCode,
//sDescription,
//nSpecialtyID,
//nCategoryID,
//nClinicID,
//bIsBlocked,
//sSpecialityCode,
//sCategoryType,
//sCategoryDesc,
//sCodeTypeCode,
//sCodeTypeDesc,
//sModifier1Code,sModifier1Desc,
//sModifier2Code,sModifier2Desc,
//sModifier3Code,sModifier3Desc,
//sModifier4Code,sModifier4Desc,
//nUnits,bIsCPTDrug,sNDCCode,
//bIsTaxable,nRate,
//nCharges,nAllowed,nClinicFee,
//bInActive,bIsUseFromFeeSchedule,sStatementDesc,sRevenueCode
                
                DataTable dt = oDB.GetDataTable_Query(strQuery);
                return dt;

             }
            catch(Exception ex)

            {
                //clsGeneral.UpdateLog("Error  while Getting CPT from class BillingConf : " + ex.Message.ToString());  


                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                return null;
            }

            finally
            {
                oDB.Dispose();
                oDB = null;
            }

        }
        public DataTable GetModifiers()
        {
            DataBaseLayer oDB = new DataBaseLayer();
            try
            {
                string strQuery = "SELECT 'False' as [Select], ISNULL(nModifierID,0) AS nModifierID,ISNULL(sModifierCode,'') AS Code,ISNULL(sDescription,'') AS Description " +
                 " , '' AS [Taxonomy Code], '' AS [Taxonomy Description], '' AS [Classification] " +
                 ", '' AS [Type Code], '' AS [Type Description]  ,'' as CPTCode , '' as NDCCode,'' as [Statement Description],'' as specialty,'' as Category  , '' as [Category Type],'' as [Code Type],'' as [Modifier1 Code],'' as [Modifier2 Code],'' as  [Modifier3 Code],'' as  [Modifier4 Code],'' as [CPT Drug],0.0 as Charges , 0.0 as Allowed,'' as [Revenue Code] ,'' as [Record Type],'Modifier' as [Billing Category] ,'' as Status,0 AS nUnits,'false' AS bIsTaxable,0 AS nRate,0 as  nClinicFee,'false' AS bIsUseFromFeeSchedule " +
                 " FROM Modifier_MST WITH (NOLOCK) where bIsBlocked = '" + false + "' AND nClinicID = " + clsGeneral.gClinicID + "";
              
                DataTable dt = oDB.GetDataTable_Query(strQuery);
                return dt;

             }
            catch(Exception ex)
            {
                //clsGeneral.UpdateLog("Error  while Getting Modifiers from class BillingConf : " + ex.Message.ToString());  


                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                return null;
            }

            finally
            {
                oDB.Dispose();
                oDB = null;
            }

        }
        public DataTable GetCategory()
        {
          
            DBParameter oParamater = default(DBParameter);
            DataBaseLayer oDB = new DataBaseLayer();
            try
            {
                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.VarChar;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@nClinicID";
                oParamater.Value = clsGeneral.gClinicID ;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;
                
                string strQuery = "gsp_glocomm_BL_Fill_categoryTypes";
                DataTable dt = oDB.GetDataTable(strQuery);
              
                 return dt;

            }
            catch(Exception ex)
            {
                //clsGeneral.UpdateLog("Error  while Getting Category from class BillingConf : " + ex.Message.ToString());  


                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                return null;
            }

            finally
            {
                oDB.Dispose();
                oDB = null;
            }


        }
        public DataTable GetSpeciality()
        {
            DataBaseLayer oDB = new DataBaseLayer();
            try
            {


                string strQuery = "SELECT 'False' as [Select], nSpecialtyID,ISNULL(sCode,'') AS Code, ISNULL(sDescription,'') AS Description, ISNULL(sTaxonomyCode,'') AS [Taxonomy Code], ISNULL(sTaxonomyDesc,'') AS [Taxonomy Description], ISNULL(sTaxonomyClassification,'') AS [Classification], ISNULL(nClinicID,0) AS nClinicID, ISNULL(bIsBlocked,'false') AS bIsBlocked , " +
                " '' as Status, '' AS [Type Code], '' AS [Type Description]  ,'' as CPTCode , '' as NDCCode,'' as [Statement Description],'' as specialty,'' as Category  , '' as [Category Type],'' as [Code Type],'' as [Modifier1 Code],'' as [Modifier2 Code],'' as  [Modifier3 Code],'' as  [Modifier4 Code],'' as [CPT Drug],0.0 as Charges , 0.0 as Allowed,'' as [Revenue Code] ,'' as [Record Type],'Specialty' as [Billing Category],0 AS nUnits,'false' AS bIsTaxable,0 AS nRate,0 as  nClinicFee,'false' AS bIsUseFromFeeSchedule  " +
                " FROM Specialty_MST WITH (NOLOCK) where nClinicID = " + clsGeneral.gClinicID + "";
                
                DataTable dt = oDB.GetDataTable_Query(strQuery);
            return dt;

            }
            catch(Exception ex)
            {
                //clsGeneral.UpdateLog("Error  while Getting Speciality from class BillingConf : " + ex.Message.ToString());  


                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                return null;
            }

            finally
            {
                oDB.Dispose();
                oDB = null;
            }
        }
        public DataTable GetPatRel()
        {
            //return null;

        
            System.Data.DataTable _result = new System.Data.DataTable();
            System.Data.DataTable _result1 = new System.Data.DataTable();
            System.Data.DataTable _result2 = new System.Data.DataTable();
            System.Data.DataTable _result3 = new System.Data.DataTable();
            System.Data.DataTable _result4 = new System.Data.DataTable();

            DataBaseLayer oDB = new DataBaseLayer(); 
            try
            {
                //Code added on 10/04/2008 -by Sagar Ghodke for implementing ClinicID;
                string _sqlQuery = "SELECT  'False' as [Select], nPatientRelID, sRelationshipCode as Code,sRelationshipDesc as Description, bIsBlock, nClinicID,bIsSystem, case ISNULL(bIsSystem,0)  when 0 then 'User' when 1 then 'System' end AS [Record Type] "+
                          " , '' AS [Taxonomy Code], '' AS [Taxonomy Description], '' AS [Classification] " +
                          ", '' AS [Type Code], '' AS [Type Description]  ,'' as CPTCode , '' as NDCCode,'' as [Statement Description],'' as specialty,'' as Category  , '' as [Category Type],'' as [Code Type],'' as [Modifier1 Code],'' as [Modifier2 Code],'' as  [Modifier3 Code],'' as  [Modifier4 Code],'' as [CPT Drug],0.0 as Charges , 0.0 as Allowed,'' as [Revenue Code] ,'PatientRelation' as [Billing Category] ,'' as Status ,0 AS nUnits,'false' AS bIsTaxable,0 AS nRate,0 as  nClinicFee,'false' AS bIsUseFromFeeSchedule   " +
                          " FROM patientrelationship WHERE bIsBlock = 0 and sRelationshipCode='18' and ( bissystem is Null or bissystem =0 ) ";//Self
              
                _result1 = oDB.GetDataTable_Query(_sqlQuery);
                
                _sqlQuery = "SELECT 'False' as [Select], nPatientRelID,  sRelationshipCode as Code,sRelationshipDesc as Description, bIsBlock, nClinicID,bIsSystem, case ISNULL(bIsSystem,0)  when 0 then 'User' when 1 then 'System' end AS  [Record Type] "+
                           " , '' AS [Taxonomy Code], '' AS [Taxonomy Description], '' AS [Classification] " +
                           ", '' AS [Type Code], '' AS [Type Description]  ,'' as CPTCode , '' as NDCCode,'' as [Statement Description],'' as specialty,'' as Category  , '' as [Category Type],'' as [Code Type],'' as [Modifier1 Code],'' as [Modifier2 Code],'' as  [Modifier3 Code],'' as  [Modifier4 Code],'' as [CPT Drug],0.0 as Charges , 0.0 as Allowed,'' as [Revenue Code] ,'PatientRelation' as [Billing Category] ,'' as Status,0 AS nUnits,'false' AS bIsTaxable,0 AS nRate,0 as  nClinicFee,'false' AS bIsUseFromFeeSchedule   " +
                           " FROM patientrelationship WHERE bIsBlock = 0 and sRelationshipCode='01' and ( bissystem is Null or bissystem =0 )";//Spouse
                
                _result2 = oDB.GetDataTable_Query(_sqlQuery);
                
                _sqlQuery = "SELECT  'False' as [Select], nPatientRelID,  sRelationshipCode as Code,sRelationshipDesc as Description, bIsBlock, nClinicID,bIsSystem, case ISNULL(bIsSystem,0)  when 0 then 'User' when 1 then 'System' end AS  [Record Type] "+
                 " , '' AS [Taxonomy Code], '' AS [Taxonomy Description], '' AS [Classification] " +
                 ", '' AS [Type Code], '' AS [Type Description]  ,'' as CPTCode , '' as NDCCode,'' as [Statement Description],'' as specialty,'' as Category  , '' as [Category Type],'' as [Code Type],'' as [Modifier1 Code],'' as [Modifier2 Code],'' as  [Modifier3 Code],'' as  [Modifier4 Code],'' as [CPT Drug],0.0 as Charges , 0.0 as Allowed,'' as [Revenue Code] ,'PatientRelation' as [Billing Category] ,'' as Status,0 AS nUnits,'false' AS bIsTaxable,0 AS nRate,0 as  nClinicFee ,'false' AS bIsUseFromFeeSchedule  " +
                 " FROM patientrelationship WHERE bIsBlock = 0 and sRelationshipCode='19' and ( bissystem is Null or bissystem =0 )";//Child

                
                _result3 = oDB.GetDataTable_Query(_sqlQuery);
              
                _sqlQuery = "SELECT 'False' as [Select], nPatientRelID,  sRelationshipCode as Code,sRelationshipDesc as Description, bIsBlock, nClinicID,bIsSystem, case ISNULL(bIsSystem,0)  when 0 then 'User' when 1 then 'System' end AS  [Record Type]"+
                       " , '' AS [Taxonomy Code], '' AS [Taxonomy Description], '' AS [Classification] " +
                       ", '' AS [Type Code], '' AS [Type Description]  ,'' as CPTCode , '' as NDCCode,'' as [Statement Description],'' as specialty,'' as Category  , '' as [Category Type],'' as [Code Type],'' as [Modifier1 Code],'' as [Modifier2 Code],'' as  [Modifier3 Code],'' as  [Modifier4 Code],'' as [CPT Drug],0.0 as Charges , 0.0 as Allowed,'' as [Revenue Code] ,'PatientRelation' as [Billing Category] ,'' as Status ,0 AS nUnits,'false' AS bIsTaxable,0 AS nRate,0 as  nClinicFee ,'false' AS bIsUseFromFeeSchedule " +
                       " FROM patientrelationship WHERE bIsBlock = 0 and sRelationshipCode<>'18' and sRelationshipCode<>'01' and sRelationshipCode<>'19' and ( bissystem is Null or bissystem =0 ) Order by sRelationshipDesc";
              
                _result4 =  oDB.GetDataTable_Query(_sqlQuery);
               
                //string _sqlQuery = "SELECT nPatientRelID, sRelationship, bIsBlock, nClinicID FROM patientrelationship WHERE bIsBlock = 0 and nClinicID = " + ClinicID + " ";//added by Sandip Darade
                //
                if (_result1 != null)
                {
                    if (_result1.Rows.Count > 0)
                        _result.Merge(_result1);
                }
                if (_result2 != null)
                {
                    if (_result2.Rows.Count > 0)
                        _result.Merge(_result2);
                }
                if (_result3 != null)
                {
                    if (_result3.Rows.Count > 0)
                        _result.Merge(_result3);
                }
                if (_result4 != null)
                {
                    if (_result4.Rows.Count > 0)
                        _result.Merge(_result4);
                }


            }
            catch (gloDatabaseLayer.DBException DBErr)
            {
                DBErr.ToString();
                DBErr = null;
            }
            catch (Exception ex)
            {
                //clsGeneral.UpdateLog("Error  while Getting PatientRelation from class BillingConf : " + ex.Message.ToString());  


                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                //ex.ToString();
                //ex = null;
            }
            finally
            {
              
                oDB.Dispose();
                oDB = null;
            }
            return _result;
       



        }
        public DataTable GetPlanType()
        {
            DataBaseLayer oDB = new DataBaseLayer();
            try
            {
                string strQuery = "SELECT 'False' as [Select], nInsuranceTypeID,ISNULL(sInsuranceTypeCode,'') AS [Type Code], ISNULL(sInsuranceTypeDesc,'') AS [Type Description] " +
                    " ,'' as Code,'' as CPTCode , '' as NDCCode,'' as Description,'' as [Statement Description],'' as specialty,'' as Category "+
                    " , '' as [Category Type],'' as [Code Type],'' as [Modifier1 Code],'' as [Modifier2 Code],'' as  [Modifier3 Code],'' as  [Modifier4 Code],'' as [CPT Drug],0.0 as Charges " +
                    " , 0.0 as Allowed,'' as [Revenue Code] , '' as [Taxonomy Code],'' as [Taxanomy Description],'' as Classification,'' as [Record Type],'PlanType' as [Billing Category] ,'' as Status,0 AS nUnits,'false' AS bIsTaxable,0 AS nRate,0 as  nClinicFee,'false' AS bIsUseFromFeeSchedule  " +
                    " FROM InsuranceType_MST WITH (NOLOCK) where nClinicID = "+clsGeneral.gClinicID+"";
                DataTable dt = oDB.GetDataTable_Query(strQuery);
                return dt;

            }
            catch (Exception ex)
            {
                //clsGeneral.UpdateLog("Error  while Getting PlanType from class BillingConf : " + ex.Message.ToString());  


                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                return null;
            }

            finally
            {
                oDB.Dispose();
                oDB = null;
            }
        }


        public string InsertCPT( string CPTCode, string Description, string SpecialityCode, string CategoryType, string Categorydesc, string CodeTypeCode, string CodeTypeDesc, string Modifier1Code, string Modifier2Code, string Modifier3Code, string Modifier4Code, double  Units, string IsCPTDrug, string NDCCode, string  IsTaxable, double  Rate, double Charges, double Allowed,string IsUseFromFeeSchedule, double ClinicFee, string Inactive, Int64  _ClinicID, string StatementDesc,string RevenueCode,DataRow drspec=null)
        {

            if (drspec != null)
            {

                string inserted = InsertSpeciality(drspec["Classification"].ToString(), drspec["Taxonomy Code"].ToString(), drspec["Taxonomy Description"].ToString(), drspec["Code"].ToString(), drspec["Description"].ToString(), clsGeneral.gClinicID, false);

            }
            
            
            
            string Modifier1Desc = "";
            string Modifier2Desc = "";
            string Modifier3Desc = "";
            string Modifier4Desc = "";
            string msg = "";
            DBParameter oParamater = default(DBParameter);
              DataBaseLayer oDB = new DataBaseLayer();
            try
            {
                //oParamater = new DBParameter();
                //oParamater.DataType = SqlDbType.BigInt;
                //oParamater.Direction = ParameterDirection.Input;
                //oParamater.Name = "@CPTID";
                //oParamater.Value = CPTID;
                //oDB.DBParametersCol.Add(oParamater);
                //oParamater = null;


                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.VarChar;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@sCPTCode";
                oParamater.Value = CPTCode;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

           
                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.VarChar;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@sDescription";
                oParamater.Value =Description;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.VarChar;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@sSpecialityCode";
                oParamater.Value =SpecialityCode;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;



                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.VarChar;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@sCategoryType";
                oParamater.Value = CategoryType;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;



                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.VarChar;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@sCategoryDesc";
                oParamater.Value = Categorydesc;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.VarChar;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@sCodeTypeCode";
                oParamater.Value = CodeTypeCode;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.VarChar;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@sCodeTypeDesc";
                oParamater.Value = CodeTypeDesc;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.VarChar;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@sModifier1Code";
                oParamater.Value = Modifier1Code;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.VarChar;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@sModifier1Desc";
                oParamater.Value = Modifier1Desc;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;


                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.VarChar;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@sModifier2Code";
                oParamater.Value = Modifier2Code;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.VarChar;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@sModifier2Desc";
                oParamater.Value = Modifier2Desc;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.VarChar;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@sModifier3Code";
                oParamater.Value = Modifier3Code;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.VarChar;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@sModifier3Desc";
                oParamater.Value = Modifier3Desc;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;


                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.VarChar;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@sModifier4Code";
                oParamater.Value = Modifier4Code;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.VarChar;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@sModifier4Desc";
                oParamater.Value = Modifier4Desc;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.Decimal;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@nUnits";
                oParamater.Value = Units;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;
                bool blniscptdrg = true; 
                if (IsCPTDrug == "NO")
                
                    blniscptdrg = false;  
               
                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.Bit;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@bIsCPTDrug";
                oParamater.Value = blniscptdrg;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.VarChar;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@sNDCCode";
                oParamater.Value = NDCCode;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;


                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.Bit;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@bIsTaxable";
                oParamater.Value =IsTaxable;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;


                oParamater = new DBParameter();
                oParamater.DataType = System.Data.SqlDbType.Decimal;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@nRate";
                oParamater.Value = Rate;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;


                oParamater = new DBParameter();
                oParamater.DataType = System.Data.SqlDbType.Decimal;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@nCharges";
                oParamater.Value = Charges;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                oParamater = new DBParameter();
                oParamater.DataType = System.Data.SqlDbType.Decimal;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@nAllowed";
                oParamater.Value = Allowed;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.Bit;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@bIsUseFromFeeSchedule";
                oParamater.Value = false; //IsUseFromFeeSchedule;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.Decimal;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@nClinicFee";
                oParamater.Value = ClinicFee;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.Bit;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@bInactive";
                oParamater.Value = Inactive;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;


                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.BigInt;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@nClinicID";
                oParamater.Value = _ClinicID;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.VarChar;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@sStatementDesc";
                oParamater.Value = StatementDesc;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.VarChar;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@sRevenueCode";
                oParamater.Value = RevenueCode;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

              //  gsp_glocomm_BL_Fill_InUpCPT_MST
                string strQuery = "gsp_glocomm_BL_Fill_InUpCPT_MST";
               msg = oDB.GetDataValue(strQuery).ToString();

            
            }
            catch (Exception ex)
            {
                //clsGeneral.UpdateLog("Error  while Inserting CPT in DB in class BillingConf : " + ex.Message.ToString());  


                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);

                return "";
            }
          
            object _intresult = 0;
            return msg;

        }



        public string InsertModifier(string ModifierCode, string Description, Int64 ClinicID)
        {

           string msg = "";

            DBParameter oParamater = default(DBParameter);
            DataBaseLayer oDB = new DataBaseLayer();
            try
            {
                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.VarChar;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@ModifierCode";
                oParamater.Value = ModifierCode;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;
           


                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.Decimal;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@ClinicID";
                oParamater.Value = clsGeneral.gClinicID;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.VarChar;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@Description";
                oParamater.Value = Description;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                string strQuery = "gsp_glocomm_InUpModifier_MST";
                msg = oDB.GetDataValue(strQuery).ToString();

                //  return dt;
                return msg;

            }
            catch (Exception ex)
            {
                //clsGeneral.UpdateLog("glocommError  while Inserting Modifier in class BillingConf : " + ex.Message.ToString());  


                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                return msg= "Error Occured";
            }
        }



        public string  InsertICD9(string ICD9Code,string Description,string spDescription, Int64 ClinicID,bool Status,DataRow drspec)
        {
            string msg="";

            if (drspec != null)
            {
            
            string inserted=    InsertSpeciality (drspec["Classification"].ToString(), drspec["Taxonomy Code"].ToString(), drspec["Taxonomy Description"].ToString(), drspec["Code"].ToString(), drspec["Description"].ToString(),clsGeneral.gClinicID,false);

            }
            
            DBParameter oParamater = default(DBParameter);
            DataBaseLayer oDB = new DataBaseLayer();
            try
            {
                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.BigInt;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@ICD9ID";
                oParamater.Value = 0;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.VarChar;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@ICD9Code";
                oParamater.Value =ICD9Code;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.VarChar;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@Description";
                oParamater.Value = Description;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;



                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.VarChar;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@specDescription";
                oParamater.Value =spDescription;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;



                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.Decimal;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@nClinicID";
                oParamater.Value = clsGeneral.gClinicID;  
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.Bit;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@bisActive";
                oParamater.Value = Status;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;



              string strQuery = "gsp_glocomm_BL_Fill_InsertICD9";
               msg = oDB.GetDataValue(strQuery).ToString() ;

               //  return dt;
             return msg;
             }
            catch (Exception exp)
            {
               // clsGeneral.UpdateLog("glocommError  while Inserting ICD9 in class BillingConf : " + exp.Message.ToString());  


                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, exp.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                return msg = exp.ToString(); 
            }
        
        }

        public string InsertCategory(string CategoryDescription,string CategoryType,Int64 ClinicID)
        {
          string msg="";
            
            DBParameter oParamater = default(DBParameter);
            DataBaseLayer oDB = new DataBaseLayer();
            try
            {
                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.VarChar ;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@CategoryDescription";
                oParamater.Value =CategoryDescription;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.VarChar;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@CategoryType";
                oParamater.Value =CategoryType;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.BigInt;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@ClinicID";
                oParamater.Value = ClinicID;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                string strQuery = "BL_INUP_gloComm_CategoryMST";
                msg = oDB.GetDataValue(strQuery).ToString();
                return msg;
            }
            catch (Exception ex)
            {
                //clsGeneral.UpdateLog("glocommError  while Inserting Category in class BillingConf : " + ex.Message.ToString());  


                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
            return msg;
            }
      
        
        }

        public string InsertSpeciality(string sTaxonomyClassification, string sTaxonomyCode,string sTaxonomyDesc,string sCode,string sDescription, Int64 ClinicID, bool bIsBlocked)
        {
            string msg = "";

            DBParameter oParamater = default(DBParameter);
            DataBaseLayer oDB = new DataBaseLayer();
            try
            {

                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.VarChar;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@sTaxonomyClassification";
                oParamater.Value = sTaxonomyClassification;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.VarChar;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@sTaxonomyCode";
                oParamater.Value = sTaxonomyCode;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.VarChar;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@sTaxonomyDesc";
                oParamater.Value = sTaxonomyDesc;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;


                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.VarChar;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@sCode";
                oParamater.Value = sCode;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.VarChar;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@sDescription";
                oParamater.Value = sDescription;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.BigInt;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@nClinicID";
                oParamater.Value = ClinicID;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;
               
                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.Bit;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@bIsBlocked";
                oParamater.Value = bIsBlocked;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

               

                string strQuery = "BL_INUP_gloComm_SpecialityMST";
                msg = oDB.GetDataValue(strQuery).ToString();
                return msg;
            }
            catch (Exception ex)
            {
                //clsGeneral.UpdateLog("glocommError  while Inserting Speciality in class BillingConf : " + ex.Message.ToString());  


                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                return msg;
            }


        }

        public string InsertPatientRelationShip(string RelationshipDesc,string RelationshipCode,bool IsBlock,Int64 ClinicID )
        {
            string msg = "";
            DBParameter oParamater = default(DBParameter);
            DataBaseLayer oDB = new DataBaseLayer();
            try
            {

                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.VarChar;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@RelationshipDesc";
                oParamater.Value = RelationshipDesc;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.VarChar;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@RelationshipCode";
                oParamater.Value =RelationshipCode;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.VarChar;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@IsBlock";
                oParamater.Value =IsBlock;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.BigInt;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@ClinicID";
                oParamater.Value =ClinicID;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                string strQuery = "AB_INUP_gloComm_PatientRelationtship";
                msg = oDB.GetDataValue(strQuery).ToString();
                return msg;
           
            }
            catch (Exception ex)
            {
                //clsGeneral.UpdateLog("glocommError  while Inserting PatientRelationship in class BillingConf : " + ex.Message.ToString());  


                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                return msg;
            }
  

        }




        public bool CompareXMlData(DataTable local, DataTable server, string FilePath)
        {
            bool changedata = false;
          //  string billingcategory = "";
            DialogResult Result ;
          //  bool dataDownloaded = false; 
            try
            {
                foreach (DataRow dr in local.Rows)
                {

                    try
                    {
                        DataRow[] drr = null;
                        switch (dr["Billing Category"].ToString())
                        {
                            case "ICD9":
                                drr = server.Select("Code='" + dr["Code"].ToString().Replace("'", "''") + "' and Description= '" + dr["Description"].ToString().Replace("'", "''") + "'");
                                if (drr.Length > 0)
                                {
                                   Result = MessageBox.Show("For ICD9 '" + dr["Code"].ToString() + "' '" + dr["Description"].ToString() + "' Already Exist Do you want to Overwrite ?", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                                   // Result = DialogResult.Yes;
                                    if (Result == DialogResult.Yes)
                                    {
                                        server.Rows.Remove(drr[0]);
                                        server.ImportRow(dr);
                                        changedata = true;
                                    }

                                }
                                else
                                {
                                    server.ImportRow(dr);
                                    changedata = true;
                                }
                                break;

                            case "CPT":

                                drr = server.Select("CPTCode='" + dr["CPTCode"].ToString().Replace("'", "''") + "' and Description= '" + dr["Description"].ToString().Replace("'", "''") + "'");
                                if (drr.Length > 0)
                                {
                                  Result = MessageBox.Show("For CPT '" + dr["CPTCode"].ToString() + "' '" + dr["Description"].ToString() + "' Already Exist Do you want to Overwrite ?", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                                  //  Result = DialogResult.Yes;
                                    if (Result == DialogResult.Yes)
                                    {
                                        server.Rows.Remove(drr[0]);
                                        server.ImportRow(dr);
                                        changedata = true;
                                    }

                                }
                                else
                                {
                                    server.ImportRow(dr);
                                    changedata = true;
                                }


                                break;

                            case "Modifier":
                                drr = server.Select("Code='" + dr["Code"].ToString().Replace("'", "''") + "' and Description= '" + dr["Description"].ToString().Replace("'", "''") + "'");
                                if (drr.Length > 0)
                                {
                                  Result = MessageBox.Show("For Modifier '" + dr["Code"].ToString() + "' '" + dr["Description"].ToString() + "' Already Exist Do you want to Overwrite ?", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                                   // Result = DialogResult.Yes;
                                    if (Result == DialogResult.Yes)
                                    {
                                        server.Rows.Remove(drr[0]);
                                        server.ImportRow(dr);
                                        changedata = true;
                                    }

                                }
                                else
                                {
                                    server.ImportRow(dr);
                                    changedata = true;
                                }

                                break;
                            case "Specialty":

                                drr = server.Select("Code='" + dr["Code"].ToString().Replace("'", "''") + "'");
                                if (drr.Length > 0)
                                {
                                    // Result = MessageBox.Show("For specialty '" + dr["Code"].ToString() + "'Already Exist Do you want to Overwrite ?", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                                    // if (Result == DialogResult.Yes)
                                    //{
                                    server.Rows.Remove(drr[0]);
                                    server.ImportRow(dr);
                                    //changedata = true;
                                    // }

                                }
                                else
                                {
                                    server.ImportRow(dr);
                                    changedata = true;
                                }
                                break;


                            case "PlanType":
                                drr = server.Select("[Type Code]='" + dr["Type Code"].ToString().Replace("'", "''") + "'");
                                if (drr.Length > 0)
                                {
                                 Result = MessageBox.Show("For PlanType '" + dr["Type Code"].ToString() + "' Already Exist Do you want to Overwrite ?", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                                   // Result = DialogResult.Yes;
                                    if (Result == DialogResult.Yes)
                                    {
                                        server.Rows.Remove(drr[0]);
                                        server.ImportRow(dr);
                                        changedata = true;
                                    }

                                }
                                else
                                {
                                    server.ImportRow(dr);
                                    changedata = true;
                                }
                                break;
                            case "PatientRelation":
                                drr = server.Select("Code='" + dr["Code"].ToString().Replace("'", "''") + "' and Description= '" + dr["Description"].ToString().Replace("'", "''") + "'");
                                if (drr.Length > 0)
                                {
                                 Result = MessageBox.Show("For Patient Relationship '" + dr["Code"].ToString() + "' '" + dr["Description"].ToString() + "' Already Exist Do you want to Overwrite ?", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                                  //  Result = DialogResult.Yes;
                                    if (Result == DialogResult.Yes)
                                    {
                                        server.Rows.Remove(drr[0]);
                                        server.ImportRow(dr);
                                        changedata = true;
                                    }

                                }
                                else
                                {
                                    server.ImportRow(dr);
                                    changedata = true;
                                }

                                break;
                            case "CategoryType":
                                drr = server.Select("Description= '" + dr["Description"].ToString().Replace("'", "''") + "'");
                                if (drr.Length > 0)
                                {
                                   Result = MessageBox.Show("For Category Type '" + dr["Description"].ToString() + "' Already Exist Do you want to Overwrite ?", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                                    //Result = DialogResult.Yes;
                                    if (Result == DialogResult.Yes)
                                    {
                                        server.Rows.Remove(drr[0]);
                                        server.ImportRow(dr);
                                        changedata = true;
                                    }

                                }
                                else
                                {
                                    server.ImportRow(dr);
                                    changedata = true;
                                }
                                break;

                        }



                    }
                    catch (Exception ex)
                    {
                        //clsGeneral.UpdateLog("glocommError  while Comparing XML Data  in class BillingConf : " + ex.Message.ToString());  


                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                       // MessageBox.Show(dr["Billing Category"].ToString()+" " +ex.ToString(), clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);      
                    }
                 }



                server.WriteXml(FilePath);
            }
            catch(Exception ex)
            {
               // clsGeneral.UpdateLog("glocommError  while Comparing XML Data  in class BillingConf : " + ex.Message.ToString());  


                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
            //MessageBox.Show(exp.ToString() , clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK , MessageBoxIcon.Error );  
            }
                return changedata; 
        }



        public string InsertPlan(string InsuranceTypeDesc, string InsuranceTypeCode, Int64 ClinicID, bool bIsSystem)
        {
            string msg = "";

            DBParameter oParamater = default(DBParameter);
            DataBaseLayer oDB = new DataBaseLayer();
            try
            {
                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.VarChar;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@sInsuranceTypeDesc";
                oParamater.Value = InsuranceTypeDesc;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.VarChar;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@sInsuranceTypeCode";
                oParamater.Value = InsuranceTypeCode;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.BigInt;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@nClinicID";
                oParamater.Value = ClinicID;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;


                 oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.Bit;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@bIsSystem";
                oParamater.Value = bIsSystem;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;


                string strQuery = "gsp_gloComm_InsuranceType_MST";
                msg = oDB.GetDataValue(strQuery).ToString();
                return msg;
            }
            catch(Exception ex)
            {
                //clsGeneral.UpdateLog("glocommError  while Inserting PlanData  in class BillingConf : " + ex.Message.ToString());  
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                return msg;
            }


        }

     
    }
}
