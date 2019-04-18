
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Data.SqlClient;
using gloEMRGeneralLibrary.gloEMRDatabase;
namespace gloCommunity.Classes
{
    public class ClsICD9AssociationDBLayer
    {
       
       private SqlConnection Conn = new SqlConnection();
      //  private DataView Dv;
//        private System.Data.SqlClient.SqlCommand Cmd;
     //   private ArrayList ArrMedicationCol = new ArrayList();
        public bool AddData(Int64 ICD9code, string ICD9Name, ArrayList arrlist)  // check it for applying Transaction
        {
           
            try
            {
                int i = 0;
         
              //  DataTable dt = null;

                DataBaseLayer oDB = new DataBaseLayer();
                DBParameter oParamater = default(DBParameter);

                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.BigInt;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@nICD9ID";
                oParamater.Value = ICD9code;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;
                oDB.ExecuteNon_Query("gsp_DeleteFromAllICD9");
                oDB.Dispose();
                oDB = null;
                  for (i = 0; i <= arrlist.Count - 1; i++)
                  {
                    myList objmylist = default(myList);
                    objmylist = (myList)arrlist[i];


                    //Insert data in ICD9CPT

                    if (objmylist.Description == "c")
                    {
                    
                ///'''''''''''' Added by Ujwala - Smart Diagnosis Changes  - as on 20101011
                        //Insert data in ICD9Drugs

                        DataBaseLayer oDBc = new DataBaseLayer();
                        DBParameter oParamaterc = default(DBParameter);

                        oParamaterc = new DBParameter();
                        oParamaterc.DataType = SqlDbType.BigInt;
                        oParamaterc.Direction = ParameterDirection.Input;
                        oParamaterc.Name = "@nICD9ID";
                        oParamaterc.Value = ICD9code;
                        oDBc.DBParametersCol.Add(oParamaterc);
                        oParamaterc = null;


                        oParamaterc = new DBParameter();
                        oParamaterc.DataType = SqlDbType.BigInt;
                        oParamaterc.Direction = ParameterDirection.Input;
                        oParamaterc.Name = "@nCPTID";
                        oParamaterc.Value = objmylist.Index;
                        oDBc.DBParametersCol.Add(oParamaterc);
                        oParamaterc = null;


                        oParamaterc = new DBParameter();
                        oParamaterc.DataType = SqlDbType.Bit;
                        oParamaterc.Direction = ParameterDirection.Input;
                        oParamaterc.Name = "@bStatus";
                        oParamaterc.Value = objmylist.Type   ;
                        oDBc.DBParametersCol.Add(oParamaterc);
                        oParamaterc = null;


                       oDBc.ExecuteNon_Query("gsp_InsertICD9CPT");
                       oDBc.Dispose();
                       oDBc = null;
             
                     gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.ICD9, gloAuditTrail.ActivityType.Add, "ICD9 Association Added to CPT", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);
                      

                    }
                    else if (objmylist.Description == "d")
                    {
                    
                        DataBaseLayer oDBc = new DataBaseLayer();
                        DBParameter oParamaterc = default(DBParameter);

                        oParamaterc = new DBParameter();
                        oParamaterc.DataType = SqlDbType.BigInt;
                        oParamaterc.Direction = ParameterDirection.Input;
                        oParamaterc.Name = "@nICD9ID";
                        oParamaterc.Value = ICD9code;
                        oDBc.DBParametersCol.Add(oParamaterc);
                        oParamaterc = null;

                        oParamaterc = new DBParameter();
                        oParamaterc.DataType = SqlDbType.BigInt;
                        oParamaterc.Direction = ParameterDirection.Input;
                        oParamaterc.Name = "@nDrugsID";
                        oParamaterc.Value = objmylist.Index;
                        oDBc.DBParametersCol.Add(oParamaterc);
                        oParamaterc = null;

                        //For De-Normalization
                        oParamaterc = new DBParameter();
                        oParamaterc.DataType = SqlDbType.VarChar;
                        oParamaterc.Direction = ParameterDirection.Input;
                        oParamaterc.Name = "@sDrugName";
                        oParamaterc.Value = objmylist.DrugName;
                        oDBc.DBParametersCol.Add(oParamaterc);
                        oParamaterc = null;

                

                        oParamaterc = new DBParameter();
                        oParamaterc.DataType = SqlDbType.VarChar;
                        oParamaterc.Direction = ParameterDirection.Input;
                        oParamaterc.Name = "@sDosage";
                        oParamaterc.Value = objmylist.Dosage;
                        oDBc.DBParametersCol.Add(oParamaterc);
                        oParamaterc = null;
                        
                        
                   

                        oParamaterc = new DBParameter();
                        oParamaterc.DataType = SqlDbType.VarChar;
                        oParamaterc.Direction = ParameterDirection.Input;
                        oParamaterc.Name = "@sDrugForm";
                        oParamaterc.Value = objmylist.DrugForm;
                        oDBc.DBParametersCol.Add(oParamaterc);
                        oParamaterc = null;

                      


                        oParamaterc = new DBParameter();
                        oParamaterc.DataType = SqlDbType.VarChar;
                        oParamaterc.Direction = ParameterDirection.Input;
                        oParamaterc.Name = "@sRoute";
                        oParamaterc.Value = objmylist.Route;
                        oDBc.DBParametersCol.Add(oParamaterc);
                        oParamaterc = null;
                        
                        
                      
                      

                        oParamaterc = new DBParameter();
                        oParamaterc.DataType = SqlDbType.VarChar;
                        oParamaterc.Direction = ParameterDirection.Input;
                        oParamaterc.Name = "@sFrequency";
                        oParamaterc.Value = objmylist.Frequency;
                        oDBc.DBParametersCol.Add(oParamaterc);
                        oParamaterc = null;
                        
                        
                        oParamaterc = new DBParameter();
                        oParamaterc.DataType = SqlDbType.VarChar;
                        oParamaterc.Direction = ParameterDirection.Input;
                        oParamaterc.Name = "@sNDCCode";
                        oParamaterc.Value = objmylist.NDCCode;
                        oDBc.DBParametersCol.Add(oParamaterc);
                        oParamaterc = null;
                        

                        oParamaterc = new DBParameter();
                        oParamaterc.DataType = SqlDbType.Int;
                        oParamaterc.Direction = ParameterDirection.Input;
                        oParamaterc.Name = "@nIsNarcotics";
                        oParamaterc.Value = objmylist.IsNarcotic;
                        oDBc.DBParametersCol.Add(oParamaterc);
                        oParamaterc = null;



                        //Duration

                        oParamaterc = new DBParameter();
                        oParamaterc.DataType = SqlDbType.VarChar;
                        oParamaterc.Direction = ParameterDirection.Input;
                        oParamaterc.Name = "@sDuration";
                        oParamaterc.Value = objmylist.Duration;
                        oDBc.DBParametersCol.Add(oParamaterc);
                        oParamaterc = null;
                        
                       

                        //DDid

                        oParamaterc = new DBParameter();
                        oParamaterc.DataType = SqlDbType.Int;
                        oParamaterc.Direction = ParameterDirection.Input;
                        oParamaterc.Name = "@mpid";
                        oParamaterc.Value = objmylist.mpid;
                        oDBc.DBParametersCol.Add(oParamaterc);
                        oParamaterc = null;



                        //DrugQtyQualifier

                        oParamaterc = new DBParameter();
                        oParamaterc.DataType = SqlDbType.VarChar;
                        oParamaterc.Direction = ParameterDirection.Input;
                        oParamaterc.Name = "@sDrugQtyQualifier";
                        oParamaterc.Value = objmylist.DrugQtyQualifier;
                        oDBc.DBParametersCol.Add(oParamaterc);
                        oParamaterc = null;
                        
                    

                        oParamaterc = new DBParameter();
                        oParamaterc.DataType = SqlDbType.Bit;
                        oParamaterc.Direction = ParameterDirection.Input;
                        oParamaterc.Name = "@bStatus";
                        oParamaterc.Value = objmylist.ItemChecked  ;
                        oDBc.DBParametersCol.Add(oParamaterc);
                        oParamaterc = null;
                        
                       
               
                        //For De-Normalization
                        oDBc.ExecuteNon_Query("gsp_InsertICD9Drugs");

                        oDBc.Dispose();
                        oDBc = null;
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.ICD9, gloAuditTrail.ActivityType.Add, "ICD9 Association Added to Drugs", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);
                        
                      
                    }
                    else if (objmylist.Description == "p")    //Insert data in ICD9Patient Education
                    {
                        DataBaseLayer oDBc = new DataBaseLayer();
                        DBParameter oParamaterc = default(DBParameter);

                        oParamaterc = new DBParameter();
                        oParamaterc.DataType = SqlDbType.BigInt;
                        oParamaterc.Direction = ParameterDirection.Input;
                        oParamaterc.Name = "@nICD9ID";
                        oParamaterc.Value = ICD9code;
                        oDBc.DBParametersCol.Add(oParamaterc);
                        oParamaterc = null;

                        oParamaterc = new DBParameter();
                        oParamaterc.DataType = SqlDbType.BigInt;
                        oParamaterc.Direction = ParameterDirection.Input;
                        oParamaterc.Name = "@nPEID";
                        oParamaterc.Value = objmylist.Index;
                        oDBc.DBParametersCol.Add(oParamaterc);
                        oParamaterc = null;


                        oParamaterc = new DBParameter();
                        oParamaterc.DataType = SqlDbType.Bit ;
                        oParamaterc.Direction = ParameterDirection.Input;
                        oParamaterc.Name = "@bStatus";
                        oParamaterc.Value = objmylist.Type;
                        oDBc.DBParametersCol.Add(oParamaterc);
                        oParamaterc = null;
                        
                        oDBc.ExecuteNon_Query("gsp_InsertICD9PE");
                        oDBc.Dispose();
                        oDBc = null;
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.ICD9, gloAuditTrail.ActivityType.Add, "ICD9 Association Added to Patient Education", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);
                            //Insert data in ICD9 Short Notes (ICD9SN)
                    }
                    else if (objmylist.Description == "t")
                    {
                  
                    
                        DataBaseLayer oDBc = new DataBaseLayer();
                        DBParameter oParamaterc = default(DBParameter);

                        oParamaterc = new DBParameter();
                        oParamaterc.DataType = SqlDbType.BigInt;
                        oParamaterc.Direction = ParameterDirection.Input;
                        oParamaterc.Name = "@nICD9ID";
                        oParamaterc.Value = ICD9code;
                        oDBc.DBParametersCol.Add(oParamaterc);
                        oParamaterc = null;

                    
                        oParamaterc = new DBParameter();
                        oParamaterc.DataType = SqlDbType.BigInt;
                        oParamaterc.Direction = ParameterDirection.Input;
                        oParamaterc.Name = "@nSNID";
                        oParamaterc.Value = objmylist.Index;
                        oDBc.DBParametersCol.Add(oParamaterc);
                        oParamaterc = null;

                        

                        ///'''''''''''' Added by Ujwala - Smart Diagnosis Changes  - as on 20101011
                        oParamaterc = new DBParameter();
                        oParamaterc.DataType = SqlDbType.Bit ;
                        oParamaterc.Direction = ParameterDirection.Input;
                        oParamaterc.Name = "@flag";
                        oParamaterc.Value = objmylist.Type;
                        oDBc.DBParametersCol.Add(oParamaterc);
                        oParamaterc = null;
                        
                       
                        ///'''''''''''' Added by Ujwala - Smart Diagnosis Changes  - as on 20101011
                        //' Chetan Added on 13-Nov 2010
                        oParamaterc = new DBParameter();
                        oParamaterc.DataType = SqlDbType.VarChar;
                        oParamaterc.Direction = ParameterDirection.Input;
                        oParamaterc.Name = "@sTemplateName";
                        oParamaterc.Value = objmylist.Comments;
                        oDBc.DBParametersCol.Add(oParamaterc);
                        oParamaterc = null;

                        oDBc.ExecuteNon_Query("gsp_InsertICD9SN");
                        oDBc.Dispose();
                        oDBc = null;
                      gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.ICD9, gloAuditTrail.ActivityType.Add, "ICD9 Association Added to Short Note", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);
              
                        //'Added Rahul on 20101013
                    }
                    else if (objmylist.Description == "f")
                    {


              

                        DataBaseLayer oDBc = new DataBaseLayer();
                        DBParameter oParamaterc = default(DBParameter);

                        oParamaterc = new DBParameter();
                        oParamaterc.DataType = SqlDbType.BigInt;
                        oParamaterc.Direction = ParameterDirection.Input;
                        oParamaterc.Name = "@nICD9ID";
                        oParamaterc.Value = ICD9code;
                        oDBc.DBParametersCol.Add(oParamaterc);
                        oParamaterc = null;
                        
                      
                   


                        oParamaterc = new DBParameter();
                        oParamaterc.DataType = SqlDbType.BigInt;
                        oParamaterc.Direction = ParameterDirection.Input;
                        oParamaterc.Name = "@nflowsheetid";
                        oParamaterc.Value = objmylist.Index;
                        oDBc.DBParametersCol.Add(oParamaterc);
                        oParamaterc = null;
                        
                      

                        oParamaterc = new DBParameter();
                        oParamaterc.DataType = SqlDbType.Bit; 
                        oParamaterc.Direction = ParameterDirection.Input;
                        oParamaterc.Name = "@bStatus";
                        oParamaterc.Value = objmylist.Type;
                        oDBc.DBParametersCol.Add(oParamaterc);
                        oParamaterc = null;
                        
                    
                        oDBc.ExecuteNon_Query("gsp_InsertICD9Flowsheet");
                        oDBc.Dispose();
                        oDBc = null;
                         //'Added Rahul P on 201000916
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.ICD9, gloAuditTrail.ActivityType.Add, "ICD9 Associatin Added to Flowsheet", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success);
                        //'

                    }
                    else if (objmylist.Description == "l")
                    {
                      

                        DataBaseLayer oDBc = new DataBaseLayer();
                        DBParameter oParamaterc = default(DBParameter);

                        oParamaterc = new DBParameter();
                        oParamaterc.DataType = SqlDbType.BigInt;
                        oParamaterc.Direction = ParameterDirection.Input;
                        oParamaterc.Name = "@nICD9ID";
                        oParamaterc.Value = ICD9code;
                        oDBc.DBParametersCol.Add(oParamaterc);
                        oParamaterc = null;
                        

                        oParamaterc = new DBParameter();
                        oParamaterc.DataType = SqlDbType.BigInt;
                        oParamaterc.Direction = ParameterDirection.Input;
                        oParamaterc.Name = "@labtm_ID";
                        oParamaterc.Value = objmylist.Index;
                        oDBc.DBParametersCol.Add(oParamaterc);
                        oParamaterc = null;



                        oParamaterc = new DBParameter();
                        oParamaterc.DataType = SqlDbType.Bit;
                        oParamaterc.Direction = ParameterDirection.Input;
                        oParamaterc.Name = "@bStatus";
                        oParamaterc.Value = objmylist.Type;
                        oDBc.DBParametersCol.Add(oParamaterc);
                        oParamaterc = null;

                        oDBc.ExecuteNon_Query("gsp_InsertICD9LabOrders");

                        oDBc.Dispose();
                        oDBc = null;
                      
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.ICD9, gloAuditTrail.ActivityType.Add, "ICD9 Associatin Added to Flowsheet", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success);
                     

                    }
                    else if (objmylist.Description == "r")
                    {
                     
                        DataBaseLayer oDBc = new DataBaseLayer();
                        DBParameter oParamaterc = default(DBParameter);

                        oParamaterc = new DBParameter();
                        oParamaterc.DataType = SqlDbType.BigInt;
                        oParamaterc.Direction = ParameterDirection.Input;
                        oParamaterc.Name = "@nICD9ID";
                        oParamaterc.Value = ICD9code;
                        oDBc.DBParametersCol.Add(oParamaterc);
                        oParamaterc = null;

                   

                        oParamaterc = new DBParameter();
                        oParamaterc.DataType = SqlDbType.BigInt;
                        oParamaterc.Direction = ParameterDirection.Input;
                        oParamaterc.Name = "@ntemplateID";
                        oParamaterc.Value  = objmylist.Index;
                        oDBc.DBParametersCol.Add(oParamaterc);
                        oParamaterc = null;

                      
                        oParamaterc = new DBParameter();
                        oParamaterc.DataType = SqlDbType.Bit ;
                        oParamaterc.Direction = ParameterDirection.Input;
                        oParamaterc.Name = "@bStatus";
                        oParamaterc.Value = objmylist.Type;
                        oDBc.DBParametersCol.Add(oParamaterc);
                        oParamaterc = null;
                       
                        oDBc.ExecuteNon_Query("gsp_InsertICD9Referral_Letter");

                        oDBc.Dispose();
                        oDBc = null;
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.ICD9, gloAuditTrail.ActivityType.Add, "ICD9 Associatin Added to Referral Letter", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success);
                        
                    }
                    else if (objmylist.Description == "o")
                    {
                   

                        DataBaseLayer oDBc = new DataBaseLayer();
                        DBParameter oParamaterc = default(DBParameter);

                        oParamaterc = new DBParameter();
                        oParamaterc.DataType = SqlDbType.BigInt;
                        oParamaterc.Direction = ParameterDirection.Input;
                        oParamaterc.Name = "@nICD9ID";
                        oParamaterc.Value = ICD9code;
                        oDBc.DBParametersCol.Add(oParamaterc);
                        oParamaterc = null;

                   
                        oParamaterc = new DBParameter();
                        oParamaterc.DataType = SqlDbType.BigInt;
                        oParamaterc.Direction = ParameterDirection.Input;
                        oParamaterc.Name = "@lm_test_id";
                        oParamaterc.Value = objmylist.Index;
                        oDBc.DBParametersCol.Add(oParamaterc);
                        oParamaterc = null;

                        oParamaterc = new DBParameter();
                        oParamaterc.DataType = SqlDbType.BigInt;
                        oParamaterc.Direction = ParameterDirection.Input;
                        oParamaterc.Name = "@bStatus";
                        oParamaterc.Value = objmylist.Type;
                        oDBc.DBParametersCol.Add(oParamaterc);
                        oParamaterc = null;

                     
                        oDBc.ExecuteNon_Query("gsp_InsertICD9_Lm_Test");
                        oDBc.Dispose();
                        oDBc = null;
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.ICD9, gloAuditTrail.ActivityType.Add, "ICD9 Associatin Added to Referral Letter", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success);
                      
                    }

                 

                }

             

             //   trICD9Association.Commit();
             //   Conn.Close();
                return true;

            }
           
            catch //(Exception ex)
            {
              
                return false;
            }
        }
        //public DataView DsDataview
        //{
          
        //    get { return Dv; }
        //}

        //public void SortDataview(string strsort)
        //{
        //    Dv.Sort = strsort;
        //}
        //Public Function ClearCol()
        //    ArrMedicationCol.Clear()
        //End Function
        //public void ClearCol()
        //{
        //    ArrMedicationCol.Clear();
        //}
        //public DataTable FillControls(long id, string strsearch)
        //{
        //    SqlDataAdapter adpt = new SqlDataAdapter();
        //    DataTable dt = new DataTable();

        //    if (id == 0)
        //    {
        //        //'Cmd = New SqlCommand("sp_FillDrugs_Mst", Conn) '' this  SP pulls top 40 records 
        //        Cmd = new SqlCommand("gsp_FillAllDrugs_Mst", Conn);
        //        //' Now Pull all records

        //        Cmd.CommandType = CommandType.StoredProcedure;
        //        adpt.SelectCommand = Cmd;

        //        SqlParameter objParam = null;

        //        objParam = Cmd.Parameters.Add("@drugletter", SqlDbType.Char);
        //        objParam.Direction = ParameterDirection.Input;
        //        objParam.Value = Strings.LCase(strsearch);

        //        objParam = Cmd.Parameters.Add("@flag", SqlDbType.Int);
        //        objParam.Direction = ParameterDirection.Input;
        //        //objParam.Value = 1 ''4

        //        //For De-Normalization
        //        objParam.Value = 16;
        //        //For De-Normalization
        //    }
        //    else if (id == 1)
        //    {
        //        Cmd = new SqlCommand("gsp_FillCPTCategory_MST", Conn);

        //        Cmd.CommandType = CommandType.StoredProcedure;
        //        adpt.SelectCommand = Cmd;

        //        SqlParameter objParam = null;
        //        objParam = Cmd.Parameters.Add("@Flag", SqlDbType.Int);
        //        objParam.Direction = ParameterDirection.Input;
        //        if (strsearch == "DESC")
        //        {
        //            objParam.Value = 1;
        //            //' Sort Dy Description
        //        }
        //        else
        //        {
        //            objParam.Value = 0;
        //            //' Sort Dy CODE (DEFAULT)
        //        }


        //    }
        //    else if (id == 2)
        //    {
        //        ///'' For Fill Templates of Patient Education
        //        Cmd = new SqlCommand("gsp_FillTemplateGallery_MST", Conn);
        //        Cmd.CommandType = CommandType.StoredProcedure;
        //        adpt.SelectCommand = Cmd;

        //        SqlParameter objParam = null;
        //        objParam = Cmd.Parameters.Add("@flag", SqlDbType.Int);
        //        objParam.Direction = ParameterDirection.Input;
        //        objParam.Value = 99;
        //        //' flag to fill Patient education

        //        //' For Tags
        //    }
        //    else if (id == 4)
        //    {
        //        Cmd = new SqlCommand("gsp_FillTemplateGallery_MST", Conn);
        //        Cmd.CommandType = CommandType.StoredProcedure;
        //        adpt.SelectCommand = Cmd;

        //        SqlParameter objParam = null;
        //        objParam = Cmd.Parameters.Add("@flag", SqlDbType.Int);
        //        objParam.Direction = ParameterDirection.Input;
        //        objParam.Value = 4;
        //        //' flag to fill Tags

        //        //'Added Rahul on 20101013
        //        //' For refferal letter 
        //    }
        //    else if (id == 10)
        //    {
        //        Cmd = new SqlCommand("gsp_FillTemplateGallery_MST", Conn);
        //        Cmd.CommandType = CommandType.StoredProcedure;
        //        adpt.SelectCommand = Cmd;

        //        SqlParameter objParam = null;
        //        objParam = Cmd.Parameters.Add("@flag", SqlDbType.Int);
        //        objParam.Direction = ParameterDirection.Input;
        //        objParam.Value = 10;
        //        //' flag to fill Tags
        //        //' For flowsheet
        //    }
        //    else if (id == 11)
        //    {

        //        string strquery = " SELECT DISTINCT nFlowSheetID AS nFlowSheetID, sFlowSheetName FROM FlowSheet_MST " + "  ORDER BY sFlowSheetName";
        //        // & " UNION SELECT DISTINCT 0 AS nFlowSheetID, sFlowSheetName FROM FlowSheet1 " _
        //        // & " WHERE sFlowSheetName not in (SELECT DISTINCT  sFlowSheetName FROM FlowSheet_MST) " _

        //        // Cmd = New SqlCommand("select nFlowSheetId,sFlowSheetName from FlowSheet_MST", Conn)

        //        Cmd = new SqlCommand(strquery, Conn);
        //        adpt.SelectCommand = Cmd;

        //        //' For Orders (Radiology Orders)
        //    }
        //    else if (id == 12)
        //    {

        //        string strSQL = null;
        //        strSQL = " SELECT   LM_Test.lm_test_ID as lm_test_ID, LM_Test.lm_test_Name as lm_test_Name FROM   LM_Test INNER JOIN " + "LM_Test AS LM_Test_1 ON LM_Test.lm_test_GroupNo = LM_Test_1.lm_test_ID INNER JOIN " + " LM_Category ON LM_Test_1.lm_test_CategoryID = LM_Category.lm_category_ID ";

        //        Cmd = new SqlCommand(strSQL, Conn);
        //        adpt.SelectCommand = Cmd;

        //        //' Template
        //    }
        //    else if (id == 13)
        //    {

        //        string strSQL = null;
        //        strSQL = "SELECT nTemplateID,sTemplateName FROM TemplateGallery_MST " + "where sCategoryName ='SOAP' ";
        //        //"select nCategoryID,sDescription from Category_Mst " _
        //        //             & " where sCategoryType='Template' and  nCategoryID<>-1 " _
        //        //             & " ORDER BY sDescription "

        //        Cmd = new SqlCommand(strSQL, Conn);
        //        adpt.SelectCommand = Cmd;

        //        //' Lab Orders 
        //    }
        //    else if (id == 14)
        //    {

        //        string strSQL = null;
        //        strSQL = "SELECT  labtm_id, labtm_Name FROM Lab_Test_Mst ";
        //        Cmd = new SqlCommand(strSQL, Conn);
        //        adpt.SelectCommand = Cmd;
        //        //'
        //    }
        //    else
        //    {
        //        Cmd = new SqlCommand("sp_FillICD9", Conn);

        //        SqlParameter objParam = null;
        //        objParam = Cmd.Parameters.Add("@flag1", SqlDbType.Int);
        //        objParam.Direction = ParameterDirection.Input;
        //        if (strsearch == "DESC")
        //        {
        //            objParam.Value = 1;
        //            //' Sort Dy Description
        //        }
        //        else
        //        {
        //            objParam.Value = 0;
        //            //' Sort Dy CODE (DEFAULT)
        //        }


        //        Cmd.CommandType = CommandType.StoredProcedure;
        //        adpt.SelectCommand = Cmd;
        //    }

        //    adpt.Fill(dt);
        //    Conn.Close();
        //    return dt;

        //    //Dim dreader As SqlDataReader
        //    //Conn.Open()
        //    //dreader = Cmd.ExecuteReader()

        //    //Do While dreader.Read
        //    //    Dim i As Integer
        //    //    i = dreader("nSpecialtyID")

        //    //Loop
        //}

        public DataTable FetchICD9forUpdate(long id)
        {
            SqlParameter objParam = null;
            try
            {
                DataTable dt = new DataTable();
                SqlDataAdapter sqladpt = new SqlDataAdapter();
                SqlCommand Cmd = new System.Data.SqlClient.SqlCommand("gsp_scanICD9Association", Conn);
                Cmd.CommandType = CommandType.StoredProcedure;
                sqladpt.SelectCommand = Cmd;

                objParam = Cmd.Parameters.Add("@nICD9ID", SqlDbType.BigInt);
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = id;

                sqladpt.Fill(dt);
                Conn.Close();
                Cmd.Parameters.Clear();
                Cmd.Dispose();
                Cmd = null;
                sqladpt.Dispose();
                sqladpt = null;
                return dt;
            }
            catch (SqlException ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.CPT, gloAuditTrail.ActivityType.General, "ClsICD9AssociationDBLayer - FetchICD9forUpdate : " + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                //UpdateLog("ClsICD9AssociationDBLayer - FetchICD9forUpdate : " & ex.ToString)
                return null;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.CPT, gloAuditTrail.ActivityType.General, "ClsICD9AssociationDBLayer - FetchICD9forUpdate : " + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                //MsgBox(ex.Message)
                //Conn.Close()
                return null;
            }
            finally
            {
                if (objParam != null)
                    objParam = null;
            }
        }
        //Shubhangi 20091211
        //Function to fetch Associated data.
        public DataTable FetchAssociatedICD9()
        {
            DataTable dt = new DataTable();
            try
            {
              
                SqlDataAdapter ad = null;
                //Check connection states
                if (Conn.State == ConnectionState.Closed)
                {
                    Conn.Open();
                }

                SqlCommand Cmd = new System.Data.SqlClient.SqlCommand("gsp_AssociatedICD9", Conn);
                Cmd.CommandType = CommandType.StoredProcedure;
                ad = new SqlDataAdapter(Cmd);
                ad.Fill(dt);
                ad.Dispose();
                ad = null;
                //'Sanjog 2011 jan 14 to handle con.open() error
                Conn.Close();
                //'Sanjog 2011 jan 14 to handle con.open() error
                Cmd.Parameters.Clear();
                Cmd.Dispose();
                Cmd = null;

            }
            catch //(Exception ex)
            {
                ////Interaction.MsgBox(ex.ToString());
            }
            return dt;
        }




        public string GetICD9IDFromCode(string ICD9Code, string ICD9Desc = "")
        {
            DataBaseLayer oDBc = new DataBaseLayer();

            try
            {

              
                DBParameter oParamaterc = default(DBParameter);

                oParamaterc = new DBParameter();
                oParamaterc.DataType = SqlDbType.VarChar;
                oParamaterc.Direction = ParameterDirection.Input;
                oParamaterc.Name = "@sICD9Code";
                oParamaterc.Value = ICD9Code;
                oDBc.DBParametersCol.Add(oParamaterc);
                oParamaterc = null;

                oParamaterc = new DBParameter();
                oParamaterc.DataType = SqlDbType.VarChar;
                oParamaterc.Direction = ParameterDirection.Input;
                oParamaterc.Name = "@sICD9Desc";
                oParamaterc.Value = ICD9Desc;
                oDBc.DBParametersCol.Add(oParamaterc);
                oParamaterc = null;

                object ICD9ID = null;
                // string strICD9ID = string.Empty;
                ICD9ID = oDBc.GetDataValue("getICD9IDformCode");
                if (ICD9ID != null)
                    return ICD9ID.ToString();
                else
                    return string.Empty;
            }
            catch
            {
                return string.Empty;
            }
            finally
            {
                oDBc.Dispose();
                oDBc = null;
            }
        }


    



        public int CheckICD9Associated(Int64  ICD9ID)
        {
            DataBaseLayer oDBc = new DataBaseLayer();

            try
            {


                DBParameter oParamaterc = default(DBParameter);

                oParamaterc = new DBParameter();
                oParamaterc.DataType = SqlDbType.BigInt;
                oParamaterc.Direction = ParameterDirection.Input;
                oParamaterc.Name = "@nICD9ID";
                oParamaterc.Value = ICD9ID;
                oDBc.DBParametersCol.Add(oParamaterc);
                oParamaterc = null;

               

                object objICD9ID = null;
                // string strICD9ID = string.Empty;
                objICD9ID = oDBc.GetDataValue("CHKSPICD9Associated");
                if (objICD9ID != null)
                    return Convert.ToInt32(objICD9ID);
                else
                    return 0;
            }
            catch
            {
                return 0;
            }
            finally
            {
                oDBc.Dispose();
                oDBc = null;
            }
        }
    
           
    
    }
}
