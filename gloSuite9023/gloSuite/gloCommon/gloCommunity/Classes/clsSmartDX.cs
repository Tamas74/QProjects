
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using gloEMRGeneralLibrary.gloEMRDatabase;
using System.Net; 
using System.IO;

   
namespace gloCommunity.Classes
{
    class clsSmartDX
    {
  

        public DataTable GetCPTIDFromCodes(string StrCptCode)
        {
          
            DataBaseLayer oDB = new DataBaseLayer();
            try
            {
              

                string _strSQL = "SELECT nCPTID as ID, sCPTcode as Code FROM  CPT_Mst  WHERE sCPTcode in (" + StrCptCode + ")";
                DataTable dt = oDB.GetDataTable_Query(_strSQL);
                return dt;

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.Select, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                return null;

            }
            finally
            {
                oDB.Dispose(); 
                oDB = null;
            }


        }

        public DataTable GetICD9IDFromCodes(string StrICD9Code)
        {
            DataBaseLayer oDB = new DataBaseLayer();
            try
            {
                string _strSQL = "SELECT nICD9ID as ID, sICD9Code as Code FROM  ICD9  WHERE sICD9Code in (" + StrICD9Code + ")";
                DataTable dt = oDB.GetDataTable_Query(_strSQL);
                return dt;

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.Select, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                return null;

            }
            finally
            {
                oDB.Dispose();
                oDB = null;
            }

        }


        public DataTable GetFloIDFromCodes(string StrFloCode)
        {
              DataTable dt = null;

            DataBaseLayer oDB = new DataBaseLayer();
            try
            {
                string _strSQL = "SELECT nFlowSheetID as ID,sFlowSheetName as Code FROM FlowSheet_MST where sFlowSheetName in (" + StrFloCode + ")";
                dt = oDB.GetDataTable_Query(_strSQL);
                return dt;

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.Select, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                return null;

            }
            finally
            {
                oDB.Dispose();
                oDB = null;
            }


        }


        public DataTable GetLabIDFromCodes(string StrLabCode)
        {
            DataTable dt = null;
            DataBaseLayer oDB = new DataBaseLayer();
            try
            {
             
                string _strSQL = "Select labtm_id as ID,labtm_name as Code FROM  Lab_Test_Mst   Where labtm_name in (" + StrLabCode + ")";
                dt = oDB.GetDataTable_Query(_strSQL);
                return dt;


            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.Select, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                 return null;

            }
            finally
            {
                oDB.Dispose();
                oDB = null;
            }
        }

        public DataTable GetOrdIDFromCodes(string StrOrdCode)
        {
          DataTable dt = null;
           
            DataBaseLayer oDB = new DataBaseLayer();
            try
            {
               


                string _strSQL = "SELECT lm_test_id as ID,lm_test_name as Code FROM Lm_test i where lm_test_name in (" + StrOrdCode + ")";
                dt = oDB.GetDataTable_Query(_strSQL);
                return dt;


            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.Select, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                return null;

            }
            finally
            {
                oDB.Dispose();
                oDB = null;
            }



        }


        public DataTable GetRefTgsPEIDFromCodes(string StrRefCode)
        {
            
            DataTable dt = null;
            DataBaseLayer oDB = new DataBaseLayer();
          
            try
            {
               string _strSQL = "SELECT nTemplateID as ID,sTemplateName as Code,sCategoryName as Category FROM  TemplateGallery_Mst  Where sTemplateName in (" + StrRefCode + ")";
                dt = oDB.GetDataTable_Query(_strSQL);

                return dt;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.Select, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                return null;
            }
            finally
            {
                oDB.Dispose();
                oDB = null;
            }
        }



        public string  InsertCPTMSTData(string strCPTCode, string strCPTDescription, decimal ClinicID, bool Status)
        {
           // DataTable dt = null;
            DataBaseLayer oDB = new DataBaseLayer();
            DBParameter oParamater = default(DBParameter);
          
          
            try
            {
               

                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.VarChar;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@sCPTCode";
                oParamater.Value = strCPTCode;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;


                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.VarChar;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@sDescription";
                oParamater.Value = strCPTDescription;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

              
                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.Bit;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@nClinicID";
                oParamater.Value = clsGeneral.gClinicID;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;


                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.VarChar;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@sSpecialityCode";
                oParamater.Value = "All";
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;


                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.VarChar;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@sCategoryDesc";
                oParamater.Value = "All";
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.Decimal;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@nUnits";
                oParamater.Value = 0;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.Bit;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@bIsCPTDrug";
                oParamater.Value = 0;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.Bit;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@bIsTaxable";
                oParamater.Value = 0;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.Bit;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@bInActive";
                oParamater.Value = 0;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.Bit;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@bIsUseFromFeeSchedule";
                oParamater.Value = 0;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                object obj= oDB.GetDataValue ("InsSPCPT_MST");
                if (obj == null)
                    return string.Empty;
                else 
                return obj.ToString() ;
            

              

                 

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.Select, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                //  MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            finally
            {
                oDB.Dispose();
                oDB = null;
            }

        }





        public DataTable  InsertOrderMSTData(string lm_test_Name, string lm_test_TestGroupFlag, decimal lm_test_CategoryID, decimal lm_test_Template_ID, Int32 lm_test_GroupNo, Int32 lm_test_LevelNo, string lm_test_Dimension, decimal lm_test_MaleLowerValue, decimal lm_test_MaleHigherValue, decimal lm_test_FemaleLowerValue,
        decimal lm_test_FemaleHigherValue, Int32 lm_test_IsSpecimenRequired, decimal lm_test_SpecimenID, decimal lm_test_LabResultID, string lm_test_sLonicID)
        {
            DataTable dt = null;
            DataBaseLayer oDB = new DataBaseLayer();
            DBParameter oParamater = default(DBParameter);

            try
            {
                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.VarChar;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@lm_test_Name";
                oParamater.Value = lm_test_Name;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.VarChar;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@lm_test_TestGroupFlag";
                oParamater.Value = lm_test_TestGroupFlag;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.Decimal;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@lm_test_CategoryID";
                oParamater.Value = lm_test_CategoryID;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.Decimal;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@lm_test_Template_ID";
                oParamater.Value = lm_test_Template_ID;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;
                
                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.Int;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@lm_test_GroupNo";
                oParamater.Value = lm_test_GroupNo;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;
                
                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.Int;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@lm_test_LevelNo";
                oParamater.Value = lm_test_LevelNo;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;


                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.VarChar;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@lm_test_Dimension";
                oParamater.Value = lm_test_Dimension;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.Decimal;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@lm_test_MaleLowerValue";
                oParamater.Value = lm_test_MaleLowerValue;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.Decimal;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@lm_test_MaleHigherValue";
                oParamater.Value = lm_test_MaleHigherValue;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

              
                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.Decimal;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@lm_test_FemaleLowerValue";
                oParamater.Value = lm_test_FemaleLowerValue;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.Decimal;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@lm_test_FemaleHigherValue";
                oParamater.Value = lm_test_FemaleHigherValue;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;


                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.Int;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@lm_test_IsSpecimenRequired";
                oParamater.Value = lm_test_IsSpecimenRequired;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.Int;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@lm_test_SpecimenID";
                oParamater.Value = lm_test_SpecimenID;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.Decimal;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@lm_test_LabResultID";
                oParamater.Value = lm_test_SpecimenID; //check it
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;


                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.VarChar;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@lm_test_sLonicID";
                oParamater.Value = lm_test_sLonicID;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                dt = oDB.GetDataTable("InsSPOrder");
                return dt;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.Select, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                //  MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            finally
            {
                oDB.Dispose();
                oDB = null;
            }

        }




        public string  InsertFlowsheetMSTData(decimal nFlowsheetid, string sFlowSheetName, Int64 nCols, Int16 nColNumber, string sColName, string sFormat, decimal Width, string sAlignment, decimal nForecolor, decimal nBackColor,
        bool Status, bool bisunderline=false )
        {

           // DataTable dt = null;
            DataBaseLayer oDB = new DataBaseLayer();
            DBParameter oParamater = default(DBParameter);
            object objflmst = null;

           try
            {
          
                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.VarChar;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@sFlowSheetName";
                oParamater.Value = sFlowSheetName;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;
               
               oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.Int;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@nCols";
                oParamater.Value = nCols;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;
               
               oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.SmallInt;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@nColNumber";
                oParamater.Value = nColNumber;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.VarChar;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@sColumnName";
                oParamater.Value = sColName;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.VarChar;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@sFormat";
                oParamater.Value = sFormat;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.Bit ;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@bIsUnderLine";
                oParamater.Value = bisunderline;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;


                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.VarChar;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@sAlignment";
                oParamater.Value = sAlignment;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

              
                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.Decimal;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@nForecolor";
                oParamater.Value = nForecolor;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.Decimal;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@nBackColor";
                oParamater.Value = nBackColor;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;


                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.Bit;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@Status";
                oParamater.Value = Status;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.Decimal;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@nFlowSheetID";
                oParamater.Value = nFlowsheetid;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

            
               // objflmst = oDB.GetDataValue("InsSPFlowsheet_MST");
                objflmst = oDB.GetDataValue("glocomm_InsSPFlowsheet_MST");  
           }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.Select, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                // MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            finally
            {
                //  conn.Close();
                oDB = null;
            }
            if (objflmst == null)
                return string.Empty;
            else
                return objflmst.ToString();  


        }

        public string InsertDrugsMSTData(string sDrugName, string sGenericName, string sDosage, string sRoute, string sFrequency, string sDuration, bool bIsClinicalDrug, Int64 nIsNarcotics, double nddid, bool bIsAllergicDrug,
        decimal nClinicID, bool bIsBlocked, string sNDCCode, string sDrugForm, string sDrugQtyQualifier, string RxType, decimal nDrugType, bool blsSystem,string Quantity)
        {
          
               // Generic Name - A
//Quantity - A (sAmount)
//Practice Favorites - (bIsClinicalDrug)
//Beers List - (nDrugType)
//Is Allergic Drug

            
            
          //  DataTable dt = null;
            DataBaseLayer oDB = new DataBaseLayer();
            DBParameter oParamater = default(DBParameter);
            object insdrg = null;


            try
            {
                //oParamater = new DBParameter();
                //oParamater.DataType = SqlDbType.Decimal;
                //oParamater.Direction = ParameterDirection.Input;
                //oParamater.Name = "@bIsAllergicDrug";
                //oParamater.Value = beerlist;
                //oDB.DBParametersCol.Add(oParamater);
                //oParamater = null;
                
                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.VarChar;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@sAmount";
                oParamater.Value = Quantity;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;
                
                
                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.VarChar;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@sDrugName";
                oParamater.Value = sDrugName;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.VarChar;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@sGenericName";
                oParamater.Value = sGenericName;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;


                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.VarChar;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@sDosage";
                oParamater.Value = sDosage;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;
               
                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.VarChar;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@sRoute";
                oParamater.Value = sRoute;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

         

                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.VarChar;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@sFrequency";
                oParamater.Value = sFrequency;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;


                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.VarChar;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@sDuration";
                oParamater.Value = sDuration;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.Bit;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@bIsClinicalDrug";
                oParamater.Value = bIsClinicalDrug;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.Int;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@nIsNarcotics";
                oParamater.Value = nIsNarcotics;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.Decimal;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@nddid";
                oParamater.Value = nddid;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

               
                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.Bit;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@bIsAllergicDrug";
                oParamater.Value = bIsAllergicDrug;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.Decimal;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@nClinicID";
                oParamater.Value = nClinicID;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.Bit;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@bIsBlocked";
                oParamater.Value = bIsBlocked;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;
               
                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.VarChar;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@sNDCCode";
                oParamater.Value = sNDCCode;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

              

                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.VarChar ;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@sDrugForm";
                oParamater.Value = sDrugForm;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.VarChar;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@sDrugQtyQualifier";
                oParamater.Value = sDrugQtyQualifier;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;


                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.VarChar;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@RxType";
                oParamater.Value = RxType;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.Decimal;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@nDrugType";
                oParamater.Value = nDrugType;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.Bit;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@blsSystem";
                oParamater.Value = blsSystem;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                //object obj = da.SelectCommand.ExecuteScalar();




                insdrg = oDB.GetDataValue("glocomm_InsSPDrug_MST");

            //    return dt;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.Select, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                return null;
            }
            finally
            {
                oDB.Dispose();
                oDB = null;



            }

            if ( insdrg== null)
                return string.Empty; 
            else
            return insdrg.ToString();

        }



        public string InsertLabTestMSTData(string strLabCode, string strLabName, bool Status)
        {
        
            //DataTable dt = null;
            DataBaseLayer oDB = new DataBaseLayer();
            DBParameter oParamater = default(DBParameter);

            object inslabtst=null;
           try
            {


                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.VarChar;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@labtm_Code";
                oParamater.Value = strLabCode;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;


                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.VarChar;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@labtm_Name";
                oParamater.Value = strLabName;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.Bit;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@labtm_Ordarable";
                oParamater.Value = 0;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;


                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.Decimal;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@labtm_DeprtCatID";
                oParamater.Value = 0;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.Decimal;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@labtm_TestHeadID";
                oParamater.Value = 0;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.Int;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@labtm_ResultType";
                oParamater.Value = 2;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.BigInt;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@nClinicID";
                oParamater.Value = clsGeneral.gClinicID ;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;
           

                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.Bit;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@Status";
                oParamater.Value = Status;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

               inslabtst = oDB.GetDataValue("InsSPLab_Test_MST");

                
               
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.Select, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                //    MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            finally
            {
                oDB.Dispose();
                oDB = null;
            }
            if (inslabtst == null)
                return string.Empty;
            else
               return  inslabtst.ToString();  

        }







        public string  GetIDFromData(string sTemplateName, decimal nProviderID, string sCategoryName, bool Status,bool localsite)
        {
            //DataTable dt = null;
            DataBaseLayer oDB = new DataBaseLayer();
            DBParameter oParamater = default(DBParameter);
            object getidfr = null;
           try
            {
              
                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.Decimal;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@nTemplateID";
                oParamater.Value = 1;                     //check it
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.VarChar;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@sTemplateName";
                oParamater.Value = sTemplateName;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.Decimal;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@nProviderID";
                oParamater.Value = 0;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.VarChar;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@sCategoryName";
                oParamater.Value = sCategoryName;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;


                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.Bit;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@Status";
                oParamater.Value = Status;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;


                if ((sCategoryName.ToLower() == "patient education") || (sCategoryName.ToLower() == "referral letter") || (sCategoryName.ToLower() == "tags") || (sCategoryName.ToLower() == "orders"))
                {
                    string FolderNm = "";
                    string Wpath = "";

                    if (localsite ==true )
                    {
                        FolderNm = clsGeneral.ClinicWebFolder + "/" + sCategoryName + "/" + sTemplateName + ".docx";
                        Wpath = clsGeneral.Webpath + "/" + clsGeneral.WebSite + "/" + clsGeneral.ClinicRepository + "/";
                    }

                    else
                    {

                        string gWebpath = "http://" + clsGeneral.gstrSharepointSrvNm + "/";
                        string gWebFolder = clsGeneral.GlobalRepository;
                        string gWebSite = clsGeneral.gstrSharepointSiteNm;

                        FolderNm = clsGeneral.WebSite + " " + clsGeneral.WebFolder + "/" + clsGeneral.ClinicWebFolder + "/" + sCategoryName + "/" + sTemplateName + ".docx";
                        Wpath = gWebpath + gWebSite + "/" + gWebFolder + "/";



                    }
                    string FileUrl = Wpath + FolderNm;

                    HttpWebRequest request = default(HttpWebRequest);
                    HttpWebResponse response = null;
                    try
                    {

                        request = (HttpWebRequest)WebRequest.Create(FileUrl);
                        request.Credentials = System.Net.CredentialCache.DefaultCredentials;
                        request.Timeout = 10000;
                        request.AllowWriteStreamBuffering = false;
                        response = (HttpWebResponse)request.GetResponse();

                        Stream s = response.GetResponseStream();
                        byte[] read = new byte[501];
                        int count = 1;
                        int len = 0;
                        while ((count != 0))
                        {
                            count = s.Read(read, len, 500);
                            len += count;
                            Array.Resize(ref read, len + 500);
                        }
                        Array.Resize(ref read, len);

                        s.Close();
                        response.Close();


                        oParamater = new DBParameter();
                        oParamater.DataType = SqlDbType.Image;
                        oParamater.Direction = ParameterDirection.Input;
                        oParamater.Name = "@sDescription";
                        oParamater.Value = read;
                        oDB.DBParametersCol.Add(oParamater);
                        oParamater = null;


                    }
                    catch
                    {

                    }

                }    


    getidfr = oDB.GetDataValue("glocomm_InsSPTemplateGallery_MST");
      }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.Select, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                //  MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
               // return null;
            }
            finally
            {
                oDB.Dispose();
                oDB = null;
            }
            if (getidfr == null)
                return string.Empty;
            else
                return getidfr.ToString();  
        }

        public DataTable GetPEFromCodes(string StrPECode)
        {
         

            DataTable dt = null;
            DataBaseLayer oDB = new DataBaseLayer();
            try
            {
              string _strSQL = "SELECT nTemplateID as ID,sTemplateName as Code FROM  TemplateGallery_Mst   where sTemplatename in (" + StrPECode + ")";
                dt = oDB.GetDataTable(_strSQL);

                return dt;

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.Select, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                return null;

            }
            finally
            {
                oDB.Dispose();
                oDB = null;
            }

        }

        public DataTable GetDrgFromCodes(string StrDrgCode)
        {
         DataTable dt = null;
            DataBaseLayer oDB = new DataBaseLayer();

            try
            {

                string _strSQL = "SELECT nDrugsID as ID,(isnull(sDrugname,'')+ ' - '+ISNULL(sDrugForm,'')) as Code FROM Drugs_MST WHERE sDrugname in (" + StrDrgCode + ")";
                dt = oDB.GetDataTable_Query(_strSQL);

                return dt;

               
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.Select, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                return null;

            }
            finally
            {
                oDB.Dispose();
                oDB = null;
            }

        }

        public DataTable GetTagsFromCodes(string StrTagsCode)
        {
      

            DataTable dt = null;
            DataBaseLayer oDB = new DataBaseLayer();

            try
            {
                string _strSQL = "SELECT i.nSNID as ID,i.sTemplatename as Code  FROM ICD9SN i   Where i.sTemplatename in (" + StrTagsCode + ")";
                dt = oDB.GetDataTable(_strSQL);

                return dt;
             }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.Select, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                return null;
            }
            finally
            {
                oDB = null;
            }
        }





        public  Int64  GetCategoryID(string oCategory)
        {
            long _ID = 0;
           DataBaseLayer oDB = new DataBaseLayer();
            try
            {
                _ID = Convert.ToInt64(oDB.GetRecord_Query("SELECT IsNull(lm_category_ID,0)as lm_category_ID FROM LM_Category WHERE lm_category_Description = '" + oCategory.Trim() + "'"));
                if (_ID == 0)
                {
                    _ID = Convert.ToInt64(oDB.GetRecord_Query("SELECT MAX(lm_category_ID) FROM LM_Category") + "") + 1;
                    string _strSQL = "";
                    _strSQL = "INSERT INTO LM_Category (lm_category_ID,lm_category_Description,lm_category_CategoryType) " + "VALUES (" + _ID + ",'" + oCategory + "'," + "1)";
                    oDB.Delete_Query (_strSQL);
                }
            }
            catch
            {

            }
            finally
            {
                // oDB.Disconnect();
                oDB = null;
            }
                return _ID;
        }

        public  Int64 GetGroupID(string GroupNm, long CategoryId)
        {
            long _ID = 0;
            DataBaseLayer oDB = new DataBaseLayer();
            try
            {

                _ID = Convert.ToInt64(oDB.GetRecord_Query("select IsNull(lm_test_ID,0)as lm_test_ID from LM_Test where lm_test_Name='" + GroupNm + "' and lm_test_CategoryID =" + CategoryId + " and lm_test_TestGroupFlag ='G'"));
                if (_ID == 0)
                {
                    _ID = Convert.ToInt64(oDB.GetRecord_Query("SELECT MAX(lm_test_ID) FROM LM_Test") + "")+ 1;
                   Int64 _levelNo = GetNewLevelNumber(_ID, CategoryId);
                    string _strSQL = "";
                    _strSQL = "INSERT INTO LM_Test (lm_test_ID,lm_test_Name,lm_test_TestGroupFlag,lm_test_CategoryID,lm_test_Template_ID,lm_test_GroupNo,lm_test_LevelNo,lm_test_Dimension,lm_test_MaleLowerValue,lm_test_MaleHigherValue,lm_test_FemaleLowerValue,lm_test_FemaleHigherValue,lm_test_IsSpecimenRequired,lm_test_SpecimenID,lm_test_LabResultID,lm_test_sLonicID)" + " VALUES (" + _ID + ",'" + GroupNm + "','G'," + CategoryId + ",0,0," + _levelNo + ",'',0,0,0,0,0,0,0,'')";
                    oDB.Delete_Query (_strSQL);
                }
            }
            catch
            {

            }
            finally
            {

                oDB.Dispose();
                oDB = null;
            }
                return _ID;
        }

        public  Int64  GetNewLevelNumber(long GroupNo, long oCategoryNo)
        {
         Int64  _ID = 0;
            DataBaseLayer oDB = new DataBaseLayer();
            try
            {
                if (GroupNo == 0)
                {
                    _ID = 1;
                }
                else
                {
                    _ID = Convert.ToInt64(oDB.GetRecord_Query("SELECT lm_test_LevelNo FROM LM_Test WHERE lm_test_GroupNo = " + GroupNo + " AND lm_test_CategoryID = " + oCategoryNo + " ") + "") + 1;
                   
                }
            }
            catch
            {
            }
            finally
            {
                oDB.Dispose();
                oDB = null;
            }
            return _ID;
        }

    public  Int64 InsertTest(long CategoryId, long GroupId, string TestNm)
        {
           Int64 _ID = 0;
            DataBaseLayer oDB = new DataBaseLayer();
            try
            {

                _ID = Convert.ToInt64(oDB.GetRecord_Query("SELECT MAX(lm_test_ID) FROM LM_Test") + "") + 1;
              Int64  _levelNo = GetNewLevelNumber(GroupId, CategoryId);
                string _strSQL = "";
                _strSQL = "INSERT INTO LM_Test (lm_test_ID,lm_test_Name,lm_test_TestGroupFlag,lm_test_CategoryID,lm_test_Template_ID,lm_test_GroupNo,lm_test_LevelNo,lm_test_Dimension,lm_test_MaleLowerValue,lm_test_MaleHigherValue,lm_test_FemaleLowerValue,lm_test_FemaleHigherValue,lm_test_IsSpecimenRequired,lm_test_SpecimenID,lm_test_LabResultID,lm_test_sLonicID)" + " VALUES (" + _ID + ",'" + TestNm + "','T'," + CategoryId + ",0," + GroupId + "," + _levelNo + ",'',0,0,0,0,0,0,0,'')";
                oDB.Delete_Query(_strSQL);
                return _ID;
            }
            catch
            {
                return 0;
            }
            finally
            {
                oDB.Dispose();
                oDB = null;
            }
          
        }



        
     
    }
}
