using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Data.SqlClient;
using gloEMRGeneralLibrary.gloEMRDatabase;
using System.Windows.Forms;

namespace gloCommunity.Classes
{
    class clsCPTAssociationDBLayer
    {
        public int CheckCPTAssociated(Int64 CPTID)
        {
            DataBaseLayer oDBc = new DataBaseLayer();
            try
            {
                DBParameter oParamaterc = default(DBParameter);

                oParamaterc = new DBParameter();
                oParamaterc.DataType = SqlDbType.BigInt;
                oParamaterc.Direction = ParameterDirection.Input;
                oParamaterc.Name = "@nCPTID";
                oParamaterc.Value = CPTID;
                oDBc.DBParametersCol.Add(oParamaterc);
                oParamaterc = null;

                object objICD9ID = null;
                objICD9ID = oDBc.GetDataValue("CHKSPCPTAssociated");

                if (objICD9ID != null)
                    return Convert.ToInt32(objICD9ID);
                else
                    return 0;
            }
            catch (Exception ex)
            {
                //clsGeneral.UpdateLog("glocommError  while Checking AssociatedCPT   in class CPTAssociationDBLayer : " + ex.Message.ToString());


                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.CPT, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                return 0;
            }
            finally
            {
                oDBc = null;
            }
        }

        public string GetCPTIDFromCode(string CPTCode, string CPTDesc = "")
        {
            DataBaseLayer oDBc = new DataBaseLayer();
            try
            {
                DBParameter oParamaterc = default(DBParameter);

                oParamaterc = new DBParameter();
                oParamaterc.DataType = SqlDbType.VarChar;
                oParamaterc.Direction = ParameterDirection.Input;
                oParamaterc.Name = "@sCPTCode";
                oParamaterc.Value = CPTCode;
                oDBc.DBParametersCol.Add(oParamaterc);
                oParamaterc = null;

                oParamaterc = new DBParameter();
                oParamaterc.DataType = SqlDbType.VarChar;
                oParamaterc.Direction = ParameterDirection.Input;
                oParamaterc.Name = "@sCPTDesc";
                oParamaterc.Value = CPTDesc;
                oDBc.DBParametersCol.Add(oParamaterc);
                oParamaterc = null;

                object ICD9ID = null;
                ICD9ID = oDBc.GetDataValue("getCPTIDformCode");

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
                oDBc = null;
            }
        }

        public bool AddData(long CPTID, string CPTName, ArrayList arrlist)
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
                oParamater.Name = "@nCPTID";
                oParamater.Value = CPTID;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;
                oDB.ExecuteNon_Query("gsp_DeleteFromAllCPT");

                ///'' Insert Data from ArrayList 
                for (i = 0; i <= arrlist.Count - 1; i++)
                {
                    myList objmylist = default(myList);
                    objmylist = (myList)arrlist[i];

                    //Insert data in CPTICD9

                    if (objmylist.Description == "i")
                    {
                        DataBaseLayer oDBc = new DataBaseLayer();
                        DBParameter oParamaterc = default(DBParameter);

                        oParamaterc = new DBParameter();
                        oParamaterc.DataType = SqlDbType.BigInt;
                        oParamaterc.Direction = ParameterDirection.Input;
                        oParamaterc.Name = "@nCPTID";
                        oParamaterc.Value = CPTID;
                        oDBc.DBParametersCol.Add(oParamaterc);
                        oParamaterc = null;

                        oParamaterc = new DBParameter();
                        oParamaterc.DataType = SqlDbType.BigInt;
                        oParamaterc.Direction = ParameterDirection.Input;
                        oParamaterc.Name = "@nICD9ID";
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

                        oDBc.ExecuteNon_Query("gsp_InsertCPTICD9");

                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.CPT, gloAuditTrail.ActivityType.Add, "ICD9 Associated with CPT " + Convert.ToString(CPTName), 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);

                    }
                    else if (objmylist.Description == "d")
                    {
                        DataBaseLayer oDBc = new DataBaseLayer();
                        DBParameter oParamaterc = default(DBParameter);

                        oParamaterc = new DBParameter();
                        oParamaterc.DataType = SqlDbType.BigInt;
                        oParamaterc.Direction = ParameterDirection.Input;
                        oParamaterc.Name = "@nCPTID";
                        oParamaterc.Value = CPTID;
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
                        oParamaterc.Value = objmylist.ItemChecked;
                        oDBc.DBParametersCol.Add(oParamaterc);
                        oParamaterc = null;

                        oDBc.ExecuteNon_Query("gsp_InsertCPTDrugs");

                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.CPT, gloAuditTrail.ActivityType.Add, "Drugs Associated with CPT " + Convert.ToString(CPTName), 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);

                    }
                    else if (objmylist.Description == "p")
                    {
                        DataBaseLayer oDBc = new DataBaseLayer();
                        DBParameter oParamaterc = default(DBParameter);

                        oParamaterc = new DBParameter();
                        oParamaterc.DataType = SqlDbType.BigInt;
                        oParamaterc.Direction = ParameterDirection.Input;
                        oParamaterc.Name = "@nCPTID";
                        oParamaterc.Value = CPTID;
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
                        oParamaterc.DataType = SqlDbType.Bit;
                        oParamaterc.Direction = ParameterDirection.Input;
                        oParamaterc.Name = "@bStatus";
                        oParamaterc.Value = objmylist.Type;
                        oDBc.DBParametersCol.Add(oParamaterc);
                        oParamaterc = null;

                        oDBc.ExecuteNon_Query("gsp_InsertCPTPE");

                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.CPT, gloAuditTrail.ActivityType.Add, "Patient Education Associated with Patient Education " + Convert.ToString(CPTName), 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);
                       
                    }
                    else if (objmylist.Description == "t")
                    {
                        DataBaseLayer oDBc = new DataBaseLayer();
                        DBParameter oParamaterc = default(DBParameter);

                        oParamaterc = new DBParameter();
                        oParamaterc.DataType = SqlDbType.BigInt;
                        oParamaterc.Direction = ParameterDirection.Input;
                        oParamaterc.Name = "@nCPTID";
                        oParamaterc.Value = CPTID;
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
                        oParamaterc.DataType = SqlDbType.Bit;
                        oParamaterc.Direction = ParameterDirection.Input;
                        oParamaterc.Name = "@bStatus";
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

                        oDBc.ExecuteNon_Query("gsp_InsertCPTSN");

                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.CPT, gloAuditTrail.ActivityType.Add, "Short Notes Associated with CPT " + Convert.ToString(CPTName), 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);
                        
                    }
                    else if (objmylist.Description == "f")
                    {
                        DataBaseLayer oDBc = new DataBaseLayer();
                        DBParameter oParamaterc = default(DBParameter);

                        oParamaterc = new DBParameter();
                        oParamaterc.DataType = SqlDbType.BigInt;
                        oParamaterc.Direction = ParameterDirection.Input;
                        oParamaterc.Name = "@nCPTID";
                        oParamaterc.Value = CPTID;
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
                        oParamaterc.Name = "@Status";
                        oParamaterc.Value = objmylist.Type;
                        oDBc.DBParametersCol.Add(oParamaterc);
                        oParamaterc = null;
                        
                        oDBc.ExecuteNon_Query("gsp_InsertCPTFlowsheet");
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.CPT, gloAuditTrail.ActivityType.Add, "Short Notes Associated with CPT " + Convert.ToString(CPTName), 0, 0, 0, gloAuditTrail.ActivityOutCome.Success);

                    }
                    else if (objmylist.Description == "r")
                    {
                        DataBaseLayer oDBc = new DataBaseLayer();
                        DBParameter oParamaterc = default(DBParameter);

                        oParamaterc = new DBParameter();
                        oParamaterc.DataType = SqlDbType.BigInt;
                        oParamaterc.Direction = ParameterDirection.Input;
                        oParamaterc.Name = "@nCPTID";
                        oParamaterc.Value = CPTID;
                        oDBc.DBParametersCol.Add(oParamaterc);
                        oParamaterc = null;

                        oParamaterc = new DBParameter();
                        oParamaterc.DataType = SqlDbType.BigInt;
                        oParamaterc.Direction = ParameterDirection.Input;
                        oParamaterc.Name = "@ntemplateID";
                        oParamaterc.Value = objmylist.Index;
                        oDBc.DBParametersCol.Add(oParamaterc);
                        oParamaterc = null;


                        oParamaterc = new DBParameter();
                        oParamaterc.DataType = SqlDbType.Bit;
                        oParamaterc.Direction = ParameterDirection.Input;
                        oParamaterc.Name = "@Status";
                        oParamaterc.Value = objmylist.Type;
                        oDBc.DBParametersCol.Add(oParamaterc);
                        oParamaterc = null;

                        oDBc.ExecuteNon_Query("gsp_InsertCPTReff_Letter");


                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.CPT, gloAuditTrail.ActivityType.Add, "Short Notes Associated with CPT " + Convert.ToString(CPTName), 0, 0, 0, gloAuditTrail.ActivityOutCome.Success);

                    }
                    else if (objmylist.Description == "l")
                    {
                        DataBaseLayer oDBc = new DataBaseLayer();
                        DBParameter oParamaterc = default(DBParameter);

                        oParamaterc = new DBParameter();
                        oParamaterc.DataType = SqlDbType.BigInt;
                        oParamaterc.Direction = ParameterDirection.Input;
                        oParamaterc.Name = "@nCPTID";
                        oParamaterc.Value = CPTID;
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
                        oParamaterc.Name = "@Status";
                        oParamaterc.Value = objmylist.Type;
                        oDBc.DBParametersCol.Add(oParamaterc);
                        oParamaterc = null;

                        oDBc.ExecuteNon_Query("gsp_InsertCPTLab_Order");

                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.CPT, gloAuditTrail.ActivityType.Add, "Short Notes Associated with CPT " + Convert.ToString(CPTName), 0, 0, 0, gloAuditTrail.ActivityOutCome.Success);

                    }
                    else if (objmylist.Description == "o")
                    {
                        DataBaseLayer oDBc = new DataBaseLayer();
                        DBParameter oParamaterc = default(DBParameter);

                        oParamaterc = new DBParameter();
                        oParamaterc.DataType = SqlDbType.BigInt;
                        oParamaterc.Direction = ParameterDirection.Input;
                        oParamaterc.Name = "@nCPTID";
                        oParamaterc.Value = CPTID;
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
                        oParamaterc.Name = "@Status";
                        oParamaterc.Value = objmylist.Type;
                        oDBc.DBParametersCol.Add(oParamaterc);
                        oParamaterc = null;

                        oDBc.ExecuteNon_Query("gsp_InsertCPTLm_Test");

                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.CPT, gloAuditTrail.ActivityType.Add, "Short Notes Associated with CPT " + Convert.ToString(CPTName), 0, 0, 0, gloAuditTrail.ActivityOutCome.Success);
                    }
                }
                return true;
            }
            catch //(Exception ex)
            {
                //MessageBox.Show(ex.Message);
                return false;
            }
        }

        public string InsertICD9MSTData(string strICD9Code, string strICD9Description, decimal ClinicID, bool Status)
        {
            //DataTable dt = null;
            DataBaseLayer oDB = new DataBaseLayer();
            DBParameter oParamater = default(DBParameter);

            try
            {
                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.VarChar;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@ICD9ID";
                oParamater.Value = 0;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.VarChar;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@ICD9Code";
                oParamater.Value = strICD9Code;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.VarChar;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@sDescription";
                oParamater.Value = strICD9Description;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.VarChar;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@SpecialtyID";
                oParamater.Value = 0;
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
                oParamater.Name = "@MachineID";
                oParamater.Value = 0;//oDB.GetPrefixTransactionID(0);
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;


                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.Bit;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@Inactive";
                oParamater.Value = Status;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;


                object obj = oDB.GetDataValue("gsp_InUpICD9");
                if (obj == null)
                    return string.Empty;
                else
                    return obj.ToString();

            }
            catch (Exception ex)
            {
               // clsGeneral.UpdateLog("glocommError  while Inserting  ICD9MST Data   in class CPTAssociationDBLayer : " + ex.Message.ToString());  
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.CPT, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                //MessageBox.Show(ex.Message);
                //gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.Select, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                ////  MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            finally
            {
                oDB.Dispose();
                oDB = null;
            }

        }
    }
}
