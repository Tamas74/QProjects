//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Data;
//using System.Data.SqlClient;
//using gloEMRGeneralLibrary.gloEMRDatabase;
//using System.Windows.Forms;

//namespace gloCommunity.Classes
//{
//    class clsDmSetup
//    {
//        public DataTable GetCritera()
//        {
//            DataBaseLayer oDB = new DataBaseLayer();
//            DataTable oResultTable = new DataTable();
//            string strQuery = "";
//            try
//            {
//                strQuery = "SELECT dm_mst_Id, dm_mst_CriteriaName FROM DM_Criteria_MST WHERE dm_mst_PatientID = 0 or dm_mst_PatientID IS NULL";
//                oResultTable = oDB.GetDataTable_Query(strQuery);
//            }
//            catch (Exception ex)
//            {
//                oResultTable = null;
//                MessageBox.Show(ex.Message);
//            }
//            finally
//            {
//                oDB.Dispose();
//                oResultTable.Dispose();
//                oDB = null;
//            }
//            return oResultTable;
//        }
//    }
//}

using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Data.SqlClient;
//using gloEMRGeneralLibrary.gloEMRDatabase;
using gloCommunity.Classes;
using System.Windows.Forms;
using gloDatabaseLayer;

namespace gloStream
{
    namespace DiseaseManagement
    {

        public class DiseaseManagement
        {

            
            private string _ErrorMessage;
            Int16 nCPTTypeID = 1;
            Int16 nICDTypeID = 2;
            //private string strPatientCode = "";
            //private string strPatientFirstName = "";
            //private string strPatientMiddleName = "";
            //private string strPatientLastName = "";
            //private string strPatientDOB = "";
            //private string strPatientAge = "";
            //private string strPatientGender = "";
            //private string strPatientMaritalStatus = "";
            private bool _CriteriaInProcess;
      //      public event StartCriteriaEventHandler StartCriteria;
            public delegate void StartCriteriaEventHandler(bool status);
        //    public event FinishCriteriaEventHandler FinishCriteria;
            public delegate void FinishCriteriaEventHandler(bool status, Collection oPatients);
       //     public event ProcessCriteriaEventHandler ProcessCriteria;
            public delegate void ProcessCriteriaEventHandler(string inProcess);
            public enum TemplateCategoryID
            {
                Guidelines = 0,
                Labs = 1,
                Radiology = 2,
                Referrals = 3,
                Rx = 4,
                IM = 5

            }
            public enum TriggerActivity
            {
                Given = 1,
                OverrideNow = 2,
                OverrideLater = 3,
                OverrideForever = 4,
                OverridewithRecur = 5
            }

            public bool CriteriaInProcess
            {
                get { return _CriteriaInProcess; }
                set { _CriteriaInProcess = value; }
            }

            public string ErrorMessage
            {
                get { return _ErrorMessage; }
                set { _ErrorMessage = value; }
            }

            public string[] GetFtInch(string strHeight)
			{
				//Dim arrHeight() As String
				strHeight = Strings.Mid(strHeight, 1, Strings.Len(strHeight) - 1);
				//arrHeight = 
				return Strings.Split(strHeight, "'");

				//Return arrHeight
			}

            private decimal FtToMtr(decimal Ft, decimal Inch)
            {
                return (Ft * Convert.ToDecimal(30.48) + Inch * Convert.ToDecimal(2.54)) / 100;
                //'   1 ft = 30.48 cm
                //'   1 inch = 2.54 cm
            }

            private decimal FtToMtr(string FtInchString)
            {
                Array str = null;
                decimal Ft = default(decimal);
                decimal Inch = default(decimal);

                str = GetFtInch(FtInchString);
                if ((str != null))
                {
                    if ((Conversion.Str(0) != null) & !string.IsNullOrEmpty(Conversion.Str(0)))
                    {
                        Ft = Convert.ToDecimal(Conversion.Str(0));
                    }
                    if ((Conversion.Str(1) != null) & !string.IsNullOrEmpty(Conversion.Str(1)))
                    {
                        Inch = Convert.ToDecimal(Conversion.Str(1));
                    }
                    
                }
                return FtToMtr(Ft, Inch);
            }

            public Int64 SaveCriteria(Int64 criteriaID, gloStream.DiseaseManagement.Supporting.Criteria oCriteria)
            {
                //Dim ODB As New gloStream.gloDataBase.gloDataBase
                SqlConnection conn = new SqlConnection(clsGeneral.EMRConnectionString);

                //declare a transaction object
                SqlTransaction myTrans = null;
                SqlCommand cmdCriteria = null;
                SqlParameter objparam = null;
                long MachineID = 0;

                try
                {
                    //do the validations
                    if (string.IsNullOrEmpty(oCriteria.Name))
                    {
                        _ErrorMessage = "Please enter the Criteria name. ";
                    }

                    conn.Open();

                    myTrans = conn.BeginTransaction();

                    cmdCriteria = conn.CreateCommand();
                    cmdCriteria.Transaction = myTrans;


                    var _with1 = cmdCriteria;
                    _with1.Connection = conn;
                    _with1.CommandType = CommandType.StoredProcedure;
                    _with1.CommandText = "DM_InsUpdCriteria";

                    objparam = new SqlParameter("@dm_mst_Id", SqlDbType.BigInt);
                    var _with2 = cmdCriteria.Parameters;
                    objparam.Direction = ParameterDirection.InputOutput;
                    _with2.Add(objparam);
                    _with2.Add("@MachineID", SqlDbType.BigInt);
                    _with2.Add("@dm_mst_CriteriaName", SqlDbType.VarChar);
                    _with2.Add("@dm_mst_AgeMin", SqlDbType.Decimal);
                    _with2.Add("@dm_mst_AgeMax", SqlDbType.Decimal);
                    _with2.Add("@dm_mst_Gender", SqlDbType.VarChar);
                    _with2.Add("@dm_mst_Race", SqlDbType.VarChar);
                    _with2.Add("@dm_mst_MaritalStatus", SqlDbType.VarChar);
                    _with2.Add("@dm_mst_City", SqlDbType.VarChar);
                    _with2.Add("@dm_mst_Status", SqlDbType.VarChar);
                    _with2.Add("@dm_mst_Zip", SqlDbType.VarChar);
                    _with2.Add("@dm_mst_EmplyementStatus", SqlDbType.VarChar);
                    _with2.Add("@dm_mst_HeightMin", SqlDbType.VarChar);
                    _with2.Add("@dm_mst_HeightMax", SqlDbType.VarChar);
                    _with2.Add("@dm_mst_WeightMin", SqlDbType.Decimal);
                    _with2.Add("@dm_mst_WeightMax", SqlDbType.Decimal);
                    _with2.Add("@dm_mst_BMIMin", SqlDbType.Decimal);
                    _with2.Add("@dm_mst_BMIMax", SqlDbType.Decimal);
                    _with2.Add("@dm_mst_TemperatureMin", SqlDbType.Decimal);
                    _with2.Add("@dm_mst_TemperatureMax", SqlDbType.Decimal);
                    _with2.Add("@dm_mst_PulseMin", SqlDbType.Decimal);
                    _with2.Add("@dm_mst_PulseMax", SqlDbType.Decimal);
                    _with2.Add("@dm_mst_PulseOxMin", SqlDbType.Decimal);
                    _with2.Add("@dm_mst_PulseOxMax", SqlDbType.Decimal);
                    _with2.Add("@dm_mst_BPSittingMin", SqlDbType.Decimal);
                    _with2.Add("@dm_mst_BPSittingMax", SqlDbType.Decimal);
                    _with2.Add("@dm_mst_BPStandingMin", SqlDbType.Decimal);
                    _with2.Add("@dm_mst_BPStandingMax", SqlDbType.Decimal);
                    _with2.Add("@dm_mst_DisplayMessage", SqlDbType.VarChar);
                    _with2.Add("@dm_mst_PatientID", SqlDbType.BigInt);
                    _with2.Add("@dm_mst_OriginalID", SqlDbType.BigInt);

                    var _with3 = cmdCriteria;
                    //MachineID = GetPrefixTransactionID();

                    objparam.Value = criteriaID;
                    //.Parameters("@dm_mst_Id").Value = Criteria_ID
                    _with3.Parameters["@MachineID"].Value = MachineID;
                    _with3.Parameters["@dm_mst_CriteriaName"].Value = oCriteria.Name;
                    //' SUDHIR 20090309 - NEW FIELDS
                    _with3.Parameters["@dm_mst_PatientID"].Value = 0;
                    _with3.Parameters["@dm_mst_OriginalID"].Value = 0;
                    //' ''
                    _with3.Parameters["@dm_mst_AgeMin"].Value = oCriteria.AgeMinimum;
                    _with3.Parameters["@dm_mst_AgeMax"].Value = oCriteria.AgeMaximum;
                    _with3.Parameters["@dm_mst_Gender"].Value = oCriteria.Gender;
                    _with3.Parameters["@dm_mst_Race"].Value = oCriteria.Race;
                    _with3.Parameters["@dm_mst_MaritalStatus"].Value = oCriteria.MaritalStatus;
                    _with3.Parameters["@dm_mst_City"].Value = oCriteria.City;
                    _with3.Parameters["@dm_mst_Status"].Value = oCriteria.State;
                    _with3.Parameters["@dm_mst_Zip"].Value = oCriteria.Zip;
                    _with3.Parameters["@dm_mst_EmplyementStatus"].Value = oCriteria.EmployeeStatus;
                    _with3.Parameters["@dm_mst_HeightMin"].Value = oCriteria.HeightMinimum;
                    _with3.Parameters["@dm_mst_HeightMax"].Value = oCriteria.HeightMaximum;
                    if (oCriteria.WeightMinimum == 0.0)
                    {
                        _with3.Parameters["@dm_mst_WeightMin"].Value = System.DBNull.Value;
                    }
                    else
                    {
                        _with3.Parameters["@dm_mst_WeightMin"].Value = oCriteria.WeightMinimum;
                    }

                    if (oCriteria.WeightMaximum == 0.0)
                    {
                        _with3.Parameters["@dm_mst_WeightMax"].Value = System.DBNull.Value;
                    }
                    else
                    {
                        _with3.Parameters["@dm_mst_WeightMax"].Value = oCriteria.WeightMaximum;
                    }

                    if (oCriteria.BMIMinimum == 0.0)
                    {
                        _with3.Parameters["@dm_mst_BMIMin"].Value = System.DBNull.Value;
                    }
                    else
                    {
                        _with3.Parameters["@dm_mst_BMIMin"].Value = oCriteria.BMIMinimum;
                    }

                    if (oCriteria.BMIMaximum == 0.0)
                    {
                        _with3.Parameters["@dm_mst_BMIMax"].Value = System.DBNull.Value;
                    }
                    else
                    {
                        _with3.Parameters["@dm_mst_BMIMax"].Value = oCriteria.BMIMaximum;
                    }

                    if (oCriteria.PulseMinimum == 0.0)
                    {
                        _with3.Parameters["@dm_mst_PulseMin"].Value = System.DBNull.Value;
                    }
                    else
                    {
                        _with3.Parameters["@dm_mst_PulseMin"].Value = oCriteria.PulseMinimum;
                    }

                    if (oCriteria.PulseMaximum == 0.0)
                    {
                        _with3.Parameters["@dm_mst_PulseMax"].Value = System.DBNull.Value;
                    }
                    else
                    {
                        _with3.Parameters["@dm_mst_PulseMax"].Value = oCriteria.PulseMaximum;
                    }

                    if (oCriteria.BPSittingMinimum == 0.0)
                    {
                        _with3.Parameters["@dm_mst_BPSittingMin"].Value = System.DBNull.Value;
                    }
                    else
                    {
                        _with3.Parameters["@dm_mst_BPSittingMin"].Value = oCriteria.BPSittingMinimum;
                    }

                    if (oCriteria.BPSittingMaximum == 0.0)
                    {
                        _with3.Parameters["@dm_mst_BPSittingMax"].Value = System.DBNull.Value;
                    }
                    else
                    {
                        _with3.Parameters["@dm_mst_BPSittingMax"].Value = oCriteria.BPSittingMaximum;
                    }

                    if (oCriteria.BPStandingMinimum == 0.0)
                    {
                        _with3.Parameters["@dm_mst_BPStandingMin"].Value = System.DBNull.Value;
                    }
                    else
                    {
                        _with3.Parameters["@dm_mst_BPStandingMin"].Value = oCriteria.BPStandingMinimum;
                    }

                    if (oCriteria.BPStandingMaximum == 0.0)
                    {
                        _with3.Parameters["@dm_mst_BPStandingMax"].Value = System.DBNull.Value;
                    }
                    else
                    {
                        _with3.Parameters["@dm_mst_BPStandingMax"].Value = oCriteria.BPStandingMaximum;
                    }

                    _with3.Parameters["@dm_mst_DisplayMessage"].Value = oCriteria.DisplayMessage;
                    if (oCriteria.PulseOXMinimum == 0.0)
                    {
                        _with3.Parameters["@dm_mst_PulseOxMin"].Value = System.DBNull.Value;
                    }
                    else
                    {
                        _with3.Parameters["@dm_mst_PulseOxMin"].Value = oCriteria.PulseOXMinimum;
                    }

                    if (oCriteria.PulseOXMaximum == 0.0)
                    {
                        _with3.Parameters["@dm_mst_PulseOxMax"].Value = System.DBNull.Value;
                    }
                    else
                    {
                        _with3.Parameters["@dm_mst_PulseOxMax"].Value = oCriteria.PulseOXMaximum;
                    }

                    if (oCriteria.TempratureMinumum == 0.0)
                    {
                        _with3.Parameters["@dm_mst_TemperatureMin"].Value = System.DBNull.Value;
                    }
                    else
                    {
                        _with3.Parameters["@dm_mst_TemperatureMin"].Value = oCriteria.TempratureMinumum;
                    }

                    if (oCriteria.TempratureMaximum == 0.0)
                    {
                        _with3.Parameters["@dm_mst_TemperatureMax"].Value = System.DBNull.Value;
                    }
                    else
                    {
                        _with3.Parameters["@dm_mst_TemperatureMax"].Value = oCriteria.TempratureMaximum;
                    }



                    cmdCriteria.ExecuteNonQuery();


                    if ((objparam != null))
                    {
                        criteriaID = Convert.ToInt64(objparam.Value);
                        ////MsgBox(Criteria_ID)
                    }


                    if (criteriaID > 0)
                    {

                        //'Delete All Records from OtherDetail Table & Insert Updated Data
                        cmdCriteria.Connection = conn;
                        cmdCriteria.CommandType = CommandType.Text;
                        cmdCriteria.CommandText = "DELETE FROM DM_Criteria_DTL WHERE (dm_mst_Id = " + criteriaID + ")";
                        cmdCriteria.ExecuteNonQuery();

                        cmdCriteria.CommandText = "DELETE FROM DM_Templates_DTL WHERE (dm_Templatedtl_Id = " + criteriaID + ")";
                        cmdCriteria.ExecuteNonQuery();

                        //'END OF DELETING RECORDS ''

                        cmdCriteria.Parameters.Clear();
                        cmdCriteria.CommandType = CommandType.StoredProcedure;
                        cmdCriteria.CommandText = "DM_InCriteriaDTL";

                        //'Insert All OtherDetails.
                        if (oCriteria.OtherDetails != null)
                        {
                            for (int i = 1; i <= oCriteria.OtherDetails.Count; i++)
                            {
                                cmdCriteria.Parameters.Add("@dm_mst_Id", SqlDbType.BigInt);
                                cmdCriteria.Parameters.Add("@dm_dtl_Id", SqlDbType.BigInt);
                                cmdCriteria.Parameters.Add("@MachineID", SqlDbType.BigInt);
                                cmdCriteria.Parameters.Add("@dm_dtl_CategoryName", SqlDbType.VarChar);
                                cmdCriteria.Parameters.Add("@dm_dtl_ItemName", SqlDbType.VarChar);
                                cmdCriteria.Parameters.Add("@dm_dtl_Operator", SqlDbType.VarChar);
                                cmdCriteria.Parameters.Add("@dm_dtl_ResultValue1", SqlDbType.VarChar);
                                cmdCriteria.Parameters.Add("@dm_dtl_ResultValue2", SqlDbType.VarChar);
                                cmdCriteria.Parameters.Add("@dm_dtl_Type", SqlDbType.Int);

                                //MachineID = GetPrefixTransactionID();
                                cmdCriteria.Parameters["@dm_mst_Id"].Value = criteriaID;
                                cmdCriteria.Parameters["@dm_dtl_Id"].Value = 0;
                                cmdCriteria.Parameters["@MachineID"].Value = MachineID;
                                cmdCriteria.Parameters["@dm_dtl_CategoryName"].Value = oCriteria.OtherDetails[i].CategoryName;
                                cmdCriteria.Parameters["@dm_dtl_ItemName"].Value = oCriteria.OtherDetails[i].ItemName;
                                if ((oCriteria.OtherDetails[i].OperatorName != null))
                                {
                                    cmdCriteria.Parameters["@dm_dtl_Operator"].Value = oCriteria.OtherDetails[i].OperatorName;
                                }
                                else
                                {
                                    cmdCriteria.Parameters["@dm_dtl_Operator"].Value = "";
                                }
                                //cmdCriteria.Parameters["@dm_dtl_Operator"].Value = oCriteria.OtherDetails(i).OperatorName
                                cmdCriteria.Parameters["@dm_dtl_ResultValue1"].Value = oCriteria.OtherDetails[i].Result1;
                                cmdCriteria.Parameters["@dm_dtl_ResultValue2"].Value = oCriteria.OtherDetails[i].Result2;
                                cmdCriteria.Parameters["@dm_dtl_Type"].Value = oCriteria.OtherDetails[i].DetailType.GetHashCode();

                                cmdCriteria.ExecuteNonQuery();
                                cmdCriteria.Parameters.Clear();
                            }
                        }
                        //'END OF OTHER DETAILS


                        //'insert Orders into Template_MST table
                        cmdCriteria.Parameters.Clear();
                        cmdCriteria.CommandText = "DM_InsTemplates_DTL";

                        if (oCriteria.LabOrders != null)
                        {
                            for (int i = 1; i <= oCriteria.LabOrders.Count; i++)
                            {
                                cmdCriteria.Parameters.Add("@Criteria_ID", SqlDbType.BigInt);
                                cmdCriteria.Parameters["@Criteria_ID"].Value = criteriaID;

                                cmdCriteria.Parameters.Add("@OrderID", SqlDbType.BigInt);
                                cmdCriteria.Parameters["@OrderID"].Value = ((myList)oCriteria.LabOrders[i]).ID;
                                //Item(i).TestID

                                cmdCriteria.Parameters.Add("@OrderType", SqlDbType.BigInt);
                                cmdCriteria.Parameters["@OrderType"].Value = TemplateCategoryID.Labs;

                                //sarika DM Denormalization 20090331
                                cmdCriteria.Parameters.Add("@TemplateName", SqlDbType.VarChar);
                                cmdCriteria.Parameters["@TemplateName"].Value = ((myList)oCriteria.LabOrders[i]).Value;

                                cmdCriteria.Parameters.Add("@Template", SqlDbType.Image);
                                cmdCriteria.Parameters["@Template"].Value = null;

                                cmdCriteria.Parameters.Add("@dm_Templatedtl_TemplateDtlInfo", SqlDbType.VarChar);
                                cmdCriteria.Parameters["@dm_Templatedtl_TemplateDtlInfo"].Value = "";

                                //--


                                //sarika DM Denormalization for Rx on 20090410

                                cmdCriteria.Parameters.Add("@sDrugForm", SqlDbType.VarChar);
                                cmdCriteria.Parameters["@sDrugForm"].Value = "";
                                //CType(oCriteria.LabOrders.Item(i), myList).DrugForm

                                cmdCriteria.Parameters.Add("@sRoute", SqlDbType.VarChar);
                                cmdCriteria.Parameters["@sRoute"].Value = "";

                                cmdCriteria.Parameters.Add("@sFrequency", SqlDbType.VarChar);
                                cmdCriteria.Parameters["@sFrequency"].Value = "";

                                cmdCriteria.Parameters.Add("@sNDCCode", SqlDbType.VarChar);
                                cmdCriteria.Parameters["@sNDCCode"].Value = "";

                                cmdCriteria.Parameters.Add("@nIsNarcotics", SqlDbType.Int);
                                cmdCriteria.Parameters["@nIsNarcotics"].Value = 0;

                                cmdCriteria.Parameters.Add("@MPID", SqlDbType.Int);
                                cmdCriteria.Parameters["@MPID"].Value = 0;

                                cmdCriteria.Parameters.Add("@sDuration", SqlDbType.VarChar);
                                cmdCriteria.Parameters["@sDuration"].Value = "";


                                cmdCriteria.Parameters.Add("@sDrugQtyQualifier", SqlDbType.VarChar);
                                cmdCriteria.Parameters["@sDrugQtyQualifier"].Value = "";
                                //----

                                cmdCriteria.ExecuteNonQuery();
                                cmdCriteria.Parameters.Clear();
                            }
                        }//end oCriteria.LabOrders

                        cmdCriteria.Parameters.Clear();
                        cmdCriteria.CommandText = "DM_InsTemplates_DTL";

                        if (oCriteria.RadiologyOrders != null)
                        {
                            for (int i = 1; i <= oCriteria.RadiologyOrders.Count; i++)
                            {
                                cmdCriteria.Parameters.Add("@Criteria_ID", SqlDbType.BigInt);
                                cmdCriteria.Parameters["@Criteria_ID"].Value = criteriaID;

                                cmdCriteria.Parameters.Add("@OrderID", SqlDbType.BigInt);
                                cmdCriteria.Parameters["@OrderID"].Value = ((myList)oCriteria.RadiologyOrders[i]).ID;
                                //Item(i).TestID

                                cmdCriteria.Parameters.Add("@OrderType", SqlDbType.BigInt);
                                cmdCriteria.Parameters["@OrderType"].Value = TemplateCategoryID.Radiology;

                                //sarika DM Denormalization 20090331
                                cmdCriteria.Parameters.Add("@TemplateName", SqlDbType.VarChar);
                                cmdCriteria.Parameters["@TemplateName"].Value = ((myList)oCriteria.RadiologyOrders[i]).Value;

                                cmdCriteria.Parameters.Add("@Template", SqlDbType.Image);
                                cmdCriteria.Parameters["@Template"].Value = null;

                                cmdCriteria.Parameters.Add("@dm_Templatedtl_TemplateDtlInfo", SqlDbType.VarChar);
                                cmdCriteria.Parameters["@dm_Templatedtl_TemplateDtlInfo"].Value = "";
                                //--


                                //sarika DM Denormalization for Rx on 20090410

                                cmdCriteria.Parameters.Add("@sDrugForm", SqlDbType.VarChar);
                                cmdCriteria.Parameters["@sDrugForm"].Value = "";

                                cmdCriteria.Parameters.Add("@sRoute", SqlDbType.VarChar);
                                cmdCriteria.Parameters["@sRoute"].Value = "";

                                cmdCriteria.Parameters.Add("@sFrequency", SqlDbType.VarChar);
                                cmdCriteria.Parameters["@sFrequency"].Value = "";

                                cmdCriteria.Parameters.Add("@sNDCCode", SqlDbType.VarChar);
                                cmdCriteria.Parameters["@sNDCCode"].Value = "";

                                cmdCriteria.Parameters.Add("@nIsNarcotics", SqlDbType.Int);
                                cmdCriteria.Parameters["@nIsNarcotics"].Value = 0;

                                cmdCriteria.Parameters.Add("@mpid", SqlDbType.Int);
                                cmdCriteria.Parameters["@mpid"].Value = 0;

                                cmdCriteria.Parameters.Add("@sDuration", SqlDbType.VarChar);
                                cmdCriteria.Parameters["@sDuration"].Value = "";



                                cmdCriteria.Parameters.Add("@sDrugQtyQualifier", SqlDbType.VarChar);
                                cmdCriteria.Parameters["@sDrugQtyQualifier"].Value = "";
                                //----



                                cmdCriteria.ExecuteNonQuery();
                                cmdCriteria.Parameters.Clear();
                            }
                        }//end oCriteria.RadiologyOrders

                        cmdCriteria.Parameters.Clear();
                        cmdCriteria.CommandText = "DM_InsTemplates_DTL";

                        if (oCriteria.Referrals != null)
                        {
                            for (int i = 1; i <= oCriteria.Referrals.Count; i++)
                            {
                                cmdCriteria.Parameters.Add("@Criteria_ID", SqlDbType.BigInt);
                                cmdCriteria.Parameters["@Criteria_ID"].Value = criteriaID;

                                cmdCriteria.Parameters.Add("@OrderID", SqlDbType.BigInt);
                                cmdCriteria.Parameters["@OrderID"].Value = ((myList)oCriteria.Referrals[i]).ID;
                                //Item(i).TestID

                                cmdCriteria.Parameters.Add("@OrderType", SqlDbType.BigInt);
                                cmdCriteria.Parameters["@OrderType"].Value = TemplateCategoryID.Referrals;

                                //sarika DM Denormalization 20090331
                                cmdCriteria.Parameters.Add("@TemplateName", SqlDbType.VarChar);
                                cmdCriteria.Parameters["@TemplateName"].Value = ((myList)oCriteria.Referrals[i]).Value;

                                cmdCriteria.Parameters.Add("@Template", SqlDbType.Image);
                                cmdCriteria.Parameters["@Template"].Value = null;

                                cmdCriteria.Parameters.Add("@dm_Templatedtl_TemplateDtlInfo", SqlDbType.VarChar);
                                cmdCriteria.Parameters["@dm_Templatedtl_TemplateDtlInfo"].Value = "";
                                //--


                                //sarika DM Denormalization for Rx on 20090410

                                cmdCriteria.Parameters.Add("@sDrugForm", SqlDbType.VarChar);
                                cmdCriteria.Parameters["@sDrugForm"].Value = "";

                                cmdCriteria.Parameters.Add("@sRoute", SqlDbType.VarChar);
                                cmdCriteria.Parameters["@sRoute"].Value = "";

                                cmdCriteria.Parameters.Add("@sFrequency", SqlDbType.VarChar);
                                cmdCriteria.Parameters["@sFrequency"].Value = "";

                                cmdCriteria.Parameters.Add("@sNDCCode", SqlDbType.VarChar);
                                cmdCriteria.Parameters["@sNDCCode"].Value = "";

                                cmdCriteria.Parameters.Add("@nIsNarcotics", SqlDbType.Int);
                                cmdCriteria.Parameters["@nIsNarcotics"].Value = 0;

                                cmdCriteria.Parameters.Add("@mpid", SqlDbType.Int);
                                cmdCriteria.Parameters["@mpid"].Value = 0;

                                cmdCriteria.Parameters.Add("@sDuration", SqlDbType.VarChar);
                                cmdCriteria.Parameters["@sDuration"].Value = "";


                                cmdCriteria.Parameters.Add("@sDrugQtyQualifier", SqlDbType.VarChar);
                                cmdCriteria.Parameters["@sDrugQtyQualifier"].Value = "";
                                //----


                                cmdCriteria.ExecuteNonQuery();
                                cmdCriteria.Parameters.Clear();
                            }
                        }//end oCriteria.Referrals

                        cmdCriteria.Parameters.Clear();
                        cmdCriteria.CommandText = "DM_InsTemplates_DTL";

                        if (oCriteria.Guidelines != null)
                        {
                            for (int i = 1; i <= oCriteria.Guidelines.Count; i++)
                            {
                                cmdCriteria.Parameters.Add("@Criteria_ID", SqlDbType.BigInt);
                                cmdCriteria.Parameters["@Criteria_ID"].Value = criteriaID;

                                cmdCriteria.Parameters.Add("@OrderID", SqlDbType.BigInt);
                                cmdCriteria.Parameters["@OrderID"].Value = ((myList)oCriteria.Guidelines[i]).Index;
                                //Item(i).TestID

                                cmdCriteria.Parameters.Add("@OrderType", SqlDbType.BigInt);
                                cmdCriteria.Parameters["@OrderType"].Value = TemplateCategoryID.Guidelines;

                                //sarika DM Denormalization 20090331
                                cmdCriteria.Parameters.Add("@TemplateName", SqlDbType.VarChar);
                                cmdCriteria.Parameters["@TemplateName"].Value = ((myList)oCriteria.Guidelines[i]).DMTemplateName;

                                cmdCriteria.Parameters.Add("@Template", SqlDbType.Image);
                                if (!((((myList)oCriteria.Guidelines[i]).DMTemplate == null) == true))
                                {
                                    byte[] img = null;
                                    img = (byte[])((myList)oCriteria.Guidelines[i]).DMTemplate;
                                    cmdCriteria.Parameters["@Template"].Value = img;
                                    img = null;
                                }
                                else
                                {
                                    cmdCriteria.Parameters["@Template"].Value = null;

                                }

                                cmdCriteria.Parameters.Add("@dm_Templatedtl_TemplateDtlInfo", SqlDbType.VarChar);
                                cmdCriteria.Parameters["@dm_Templatedtl_TemplateDtlInfo"].Value = "";
                                //'guideline category
                                //--



                                //sarika DM Denormalization for Rx on 20090410

                                cmdCriteria.Parameters.Add("@sDrugForm", SqlDbType.VarChar);
                                cmdCriteria.Parameters["@sDrugForm"].Value = "";

                                cmdCriteria.Parameters.Add("@sRoute", SqlDbType.VarChar);
                                cmdCriteria.Parameters["@sRoute"].Value = "";

                                cmdCriteria.Parameters.Add("@sFrequency", SqlDbType.VarChar);
                                cmdCriteria.Parameters["@sFrequency"].Value = "";

                                cmdCriteria.Parameters.Add("@sNDCCode", SqlDbType.VarChar);
                                cmdCriteria.Parameters["@sNDCCode"].Value = "";

                                cmdCriteria.Parameters.Add("@nIsNarcotics", SqlDbType.Int);
                                cmdCriteria.Parameters["@nIsNarcotics"].Value = 0;

                                cmdCriteria.Parameters.Add("@mpid", SqlDbType.Int);
                                cmdCriteria.Parameters["@mpid"].Value = 0;

                                cmdCriteria.Parameters.Add("@sDuration", SqlDbType.VarChar);
                                cmdCriteria.Parameters["@sDuration"].Value = "";

                                                              

                                cmdCriteria.Parameters.Add("@sDrugQtyQualifier", SqlDbType.VarChar);
                                cmdCriteria.Parameters["@sDrugQtyQualifier"].Value = "";
                                //----


                                cmdCriteria.ExecuteNonQuery();
                                cmdCriteria.Parameters.Clear();
                            }
                        }//end oCriteria.Guidelines

                        cmdCriteria.Parameters.Clear();
                        cmdCriteria.CommandText = "DM_InsTemplates_DTL";

                        if (oCriteria.RxDrugs != null)
                        {
                            for (int i = 1; i <= oCriteria.RxDrugs.Count; i++)
                            {
                                cmdCriteria.Parameters.Add("@Criteria_ID", SqlDbType.BigInt);
                                cmdCriteria.Parameters["@Criteria_ID"].Value = criteriaID;

                                cmdCriteria.Parameters.Add("@OrderID", SqlDbType.BigInt);
                                cmdCriteria.Parameters["@OrderID"].Value = ((myList)oCriteria.RxDrugs[i]).ID;
                                //Item(i).TestID

                                cmdCriteria.Parameters.Add("@OrderType", SqlDbType.BigInt);
                                cmdCriteria.Parameters["@OrderType"].Value = TemplateCategoryID.Rx;

                                //sarika DM Denormalization 20090331
                                cmdCriteria.Parameters.Add("@TemplateName", SqlDbType.VarChar);
                                cmdCriteria.Parameters["@TemplateName"].Value = ((myList)oCriteria.RxDrugs[i]).DrugName;

                                cmdCriteria.Parameters.Add("@Template", SqlDbType.Image);
                                cmdCriteria.Parameters["@Template"].Value = null;

                                cmdCriteria.Parameters.Add("@dm_Templatedtl_TemplateDtlInfo", SqlDbType.VarChar);
                                cmdCriteria.Parameters["@dm_Templatedtl_TemplateDtlInfo"].Value = ((myList)oCriteria.RxDrugs[i]).Dosage;


                                //--


                                //sarika DM Denormalization for Rx on 20090410

                                cmdCriteria.Parameters.Add("@sDrugForm", SqlDbType.VarChar);
                                if ((((myList)oCriteria.RxDrugs[i]).DrugForm != null))
                                {
                                    cmdCriteria.Parameters["@sDrugForm"].Value = ((myList)oCriteria.RxDrugs[i]).DrugForm;
                                }
                                else
                                {
                                    cmdCriteria.Parameters["@sDrugForm"].Value = "";
                                }


                                cmdCriteria.Parameters.Add("@sRoute", SqlDbType.VarChar);
                                if ((((myList)oCriteria.RxDrugs[i]).Route != null))
                                {
                                    cmdCriteria.Parameters["@sRoute"].Value = ((myList)oCriteria.RxDrugs[i]).Route;
                                }
                                else
                                {
                                    cmdCriteria.Parameters["@sRoute"].Value = "";
                                }


                                cmdCriteria.Parameters.Add("@sFrequency", SqlDbType.VarChar);
                                if ((((myList)oCriteria.RxDrugs[i]).Frequency != null))
                                {
                                    cmdCriteria.Parameters["@sFrequency"].Value = ((myList)oCriteria.RxDrugs[i]).Frequency;
                                }
                                else
                                {
                                    cmdCriteria.Parameters["@sFrequency"].Value = "";
                                }


                                cmdCriteria.Parameters.Add("@sNDCCode", SqlDbType.VarChar);
                                if ((((myList)oCriteria.RxDrugs[i]).NDCCode != null))
                                {
                                    cmdCriteria.Parameters["@sNDCCode"].Value = ((myList)oCriteria.RxDrugs[i]).NDCCode;
                                }
                                else
                                {
                                    cmdCriteria.Parameters["@sNDCCode"].Value = "";
                                }

                                cmdCriteria.Parameters.Add("@nIsNarcotics", SqlDbType.Int);
                               // if ((((myList)oCriteria.RxDrugs[i]).IsNarcotic != null))
                                {
                                    cmdCriteria.Parameters["@nIsNarcotics"].Value = ((myList)oCriteria.RxDrugs[i]).IsNarcotic;
                                }
                                //else
                                //{
                                //    cmdCriteria.Parameters["@nIsNarcotics"].Value = 0;
                                //}


                                cmdCriteria.Parameters.Add("@mpid", SqlDbType.Int);
                                if ((((myList)oCriteria.RxDrugs[i]).NDCCode != null))
                                {
                                    cmdCriteria.Parameters["@mpid"].Value = ((myList)oCriteria.RxDrugs[i]).mpid;
                                }
                                else
                                {
                                    cmdCriteria.Parameters["@mpid"].Value = "";
                                }

                                cmdCriteria.Parameters.Add("@sDuration", SqlDbType.VarChar);
                                if ((((myList)oCriteria.RxDrugs[i]).Duration != null))
                                {
                                    cmdCriteria.Parameters["@sDuration"].Value = ((myList)oCriteria.RxDrugs[i]).Duration;
                                }
                                else
                                {
                                    cmdCriteria.Parameters["@sDuration"].Value = "";
                                }


                                cmdCriteria.Parameters.Add("@sDrugQtyQualifier", SqlDbType.VarChar);
                                if ((((myList)oCriteria.RxDrugs[i]).DrugQtyQualifier != null))
                                {
                                    cmdCriteria.Parameters["@sDrugQtyQualifier"].Value = ((myList)oCriteria.RxDrugs[i]).DrugQtyQualifier;
                                }
                                else
                                {
                                    cmdCriteria.Parameters["@sDrugQtyQualifier"].Value = "";
                                }


                                //----


                                cmdCriteria.ExecuteNonQuery();
                                cmdCriteria.Parameters.Clear();
                            }
                        }//end oCriteria.RxDrugs

                        //' Chetan integrated on 09 Oct 2010 for IM in DM Setup

                        cmdCriteria.Parameters.Clear();
                        cmdCriteria.CommandText = "DM_InsTemplates_DTL";

                        if (oCriteria.IMlst != null)
                        {
                            for (int i = 1; i <= oCriteria.IMlst.Count; i++)
                            {
                                cmdCriteria.Parameters.Add("@Criteria_ID", SqlDbType.BigInt);
                                cmdCriteria.Parameters["@Criteria_ID"].Value = criteriaID;


                                cmdCriteria.Parameters.Add("@OrderID", SqlDbType.BigInt);
                                cmdCriteria.Parameters["@OrderID"].Value = ((myList)oCriteria.IMlst[i]).ID;
                                // IM ID

                                cmdCriteria.Parameters.Add("@OrderType", SqlDbType.BigInt);
                                cmdCriteria.Parameters["@OrderType"].Value = TemplateCategoryID.IM;
                                //Enum - IM


                                cmdCriteria.Parameters.Add("@TemplateName", SqlDbType.VarChar);
                                cmdCriteria.Parameters["@TemplateName"].Value = ((myList)oCriteria.IMlst[i]).Value;

                                cmdCriteria.Parameters.Add("@Template", SqlDbType.Image);
                                cmdCriteria.Parameters["@Template"].Value = null;
                                //'If Not IsNothing(CType(oCriteria.Guidelines.Item(i), myList).DMTemplate) = True Then
                                //'    Dim img As Byte()
                                //'    img = CType(oCriteria.Guidelines.Item(i), myList).DMTemplate
                                //'    cmdCriteria.Parameters["@Template"].Value = img
                                //'    img = Nothing
                                //'Else
                                //'    cmdCriteria.Parameters["@Template"].Value = Nothing
                                //'End If

                                //--
                                cmdCriteria.Parameters.Add("@sDrugForm", SqlDbType.VarChar);
                                cmdCriteria.Parameters["@sDrugForm"].Value = ((myList)oCriteria.IMlst[i]).DrugForm;
                                //ConceptID

                                cmdCriteria.Parameters.Add("@sDuration", SqlDbType.VarChar);
                                cmdCriteria.Parameters["@sDuration"].Value = ((myList)oCriteria.IMlst[i]).Duration;
                                //DescriptionID

                                cmdCriteria.Parameters.Add("@sFrequency", SqlDbType.VarChar);
                                cmdCriteria.Parameters["@sFrequency"].Value = ((myList)oCriteria.IMlst[i]).Frequency;
                                //SnoMedID


                                cmdCriteria.Parameters.Add("@sDrugQtyQualifier", SqlDbType.VarChar);
                                cmdCriteria.Parameters["@sDrugQtyQualifier"].Value = ((myList)oCriteria.IMlst[i]).DrugQtyQualifier;
                                //Item Count
                                ///''''''''''''''''''''''''''''''''


                                cmdCriteria.Parameters.Add("@sRoute", SqlDbType.VarChar);
                                cmdCriteria.Parameters["@sRoute"].Value = ((myList)oCriteria.IMlst[i]).Route;
                                //Orignal Vaccine name


                                cmdCriteria.Parameters.Add("@sNDCCode", SqlDbType.VarChar);
                                cmdCriteria.Parameters["@sNDCCode"].Value = "";

                                cmdCriteria.Parameters.Add("@nIsNarcotics", SqlDbType.Int);
                                cmdCriteria.Parameters["@nIsNarcotics"].Value = 0;

                                cmdCriteria.Parameters.Add("@mpid", SqlDbType.Int);
                                cmdCriteria.Parameters["@mpid"].Value = 0;


                                cmdCriteria.Parameters.Add("@dm_Templatedtl_TemplateDtlInfo", SqlDbType.VarChar);
                                cmdCriteria.Parameters["@dm_Templatedtl_TemplateDtlInfo"].Value = "";
                                //----


                                cmdCriteria.ExecuteNonQuery();
                                cmdCriteria.Parameters.Clear();
                            }
                        }//end oCriteria.IMlst

                        myTrans.Commit();
                        //'Added Rahul P on 20101009
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DisclosureManagement, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Add, "'" + oCriteria.Name + "' Disease Management Criteria Added", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);
                        
                    }
                    else
                    {
                        myTrans.Rollback();
                    }

                }
                catch (Exception ex)
                {
                    //if some error occur when inserting in any of the tables then all the transactions are rollbacked
                    try
                    {
                        myTrans.Rollback();
                    }
                    catch //(SqlException ex1)
                    {
                        if ((myTrans.Connection != null))
                        {
                            //Console.WriteLine("An exception of type " & ex1.GetType().ToString() & _
                            //" was encountered while attempting to roll back the transaction.")
                            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.DisclosureManagement, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Add, "clsDiseaseManagement -- Add -- " + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                            //UpdateLog("clsDiseaseManagement -- Add -- " & ex.ToString)
                            _ErrorMessage = ex.Message;
                        }
                    }
                    //UpdateLog("clsDiseaseManagement -- Add -- " & ex.ToString)
                    _ErrorMessage = ex.Message;
                    _ErrorMessage = "Neither record was written to database.";
                }
                finally
                {
                    if (cmdCriteria != null)
                    {
                        cmdCriteria.Parameters.Clear();
                        cmdCriteria.Dispose();
                        cmdCriteria = null;
                    }
                    if (objparam != null)
                    {
                        objparam = null;
                    }
                    conn.Close();
                    conn.Dispose();
                    conn = null;
                }
                return criteriaID;
            }

            public Int64 Add(gloStream.DiseaseManagement.Supporting.Criteria oCriteria)
            {
                //Dim ODB As New gloStream.gloDataBase.gloDataBase
                SqlConnection conn = new SqlConnection(clsGeneral.EMRConnectionString);

                //declare a transaction object
                SqlTransaction myTrans = null;
                SqlCommand cmdCriteria = null;
                SqlParameter objparam = null;
                long Criteria_ID = 0;
                long MachineID = 0;


                try
                {
                    //do the validations
                    if (string.IsNullOrEmpty(oCriteria.Name))
                    {
                        _ErrorMessage = "Please enter the Criteria name. ";
                    }

                    conn.Open();

                    myTrans = conn.BeginTransaction();

                    cmdCriteria = conn.CreateCommand();
                    cmdCriteria.Transaction = myTrans;

                    //With ODB.DBParameters
                    //    .Add("@dm_mst_Id", oCriteria.ID, ParameterDirection.Input, SqlDbType.BigInt)
                    //End With

                    //ODB.ExecuteNonQuery("DM_InsUpdCriteria")
                    //Insert into the DM_InsUpdCriteria table.

                    var _with4 = cmdCriteria;
                    _with4.Connection = conn;
                    _with4.CommandType = CommandType.StoredProcedure;
                    _with4.CommandText = "DM_InsUpdCriteria";

                    objparam = new SqlParameter("@dm_mst_Id", SqlDbType.BigInt);
                    var _with5 = cmdCriteria.Parameters;
                    //.Add("@dm_mst_Id", SqlDbType.BigInt)
                    objparam.Direction = ParameterDirection.InputOutput;
                    _with5.Add(objparam);
                    _with5.Add("@MachineID", SqlDbType.BigInt);
                    _with5.Add("@dm_mst_CriteriaName", SqlDbType.VarChar);
                    _with5.Add("@dm_mst_AgeMin", SqlDbType.Decimal);
                    _with5.Add("@dm_mst_AgeMax", SqlDbType.Decimal);
                    _with5.Add("@dm_mst_Gender", SqlDbType.VarChar);
                    _with5.Add("@dm_mst_Race", SqlDbType.VarChar);
                    _with5.Add("@dm_mst_MaritalStatus", SqlDbType.VarChar);
                    _with5.Add("@dm_mst_City", SqlDbType.VarChar);
                    _with5.Add("@dm_mst_Status", SqlDbType.VarChar);
                    _with5.Add("@dm_mst_Zip", SqlDbType.VarChar);
                    _with5.Add("@dm_mst_EmplyementStatus", SqlDbType.VarChar);
                    _with5.Add("@dm_mst_HeightMin", SqlDbType.VarChar);
                    _with5.Add("@dm_mst_HeightMax", SqlDbType.VarChar);
                    _with5.Add("@dm_mst_WeightMin", SqlDbType.Decimal);
                    _with5.Add("@dm_mst_WeightMax", SqlDbType.Decimal);
                    _with5.Add("@dm_mst_BMIMin", SqlDbType.Decimal);
                    _with5.Add("@dm_mst_BMIMax", SqlDbType.Decimal);
                    _with5.Add("@dm_mst_TemperatureMin", SqlDbType.Decimal);
                    _with5.Add("@dm_mst_TemperatureMax", SqlDbType.Decimal);
                    _with5.Add("@dm_mst_PulseMin", SqlDbType.Decimal);
                    _with5.Add("@dm_mst_PulseMax", SqlDbType.Decimal);
                    _with5.Add("@dm_mst_PulseOxMin", SqlDbType.Decimal);
                    _with5.Add("@dm_mst_PulseOxMax", SqlDbType.Decimal);
                    _with5.Add("@dm_mst_BPSittingMin", SqlDbType.Decimal);
                    _with5.Add("@dm_mst_BPSittingMax", SqlDbType.Decimal);
                    _with5.Add("@dm_mst_BPStandingMin", SqlDbType.Decimal);
                    _with5.Add("@dm_mst_BPStandingMax", SqlDbType.Decimal);
                    _with5.Add("@dm_mst_DisplayMessage", SqlDbType.VarChar);
                    _with5.Add("@dm_mst_PatientID", SqlDbType.BigInt);
                    _with5.Add("@dm_mst_OriginalID", SqlDbType.BigInt);

                    var _with6 = cmdCriteria;
                    //MachineID = GetPrefixTransactionID();

                    objparam.Value = Criteria_ID;
                    //.Parameters["@dm_mst_Id"].Value = Criteria_ID
                    _with6.Parameters["@MachineID"].Value = MachineID;
                    _with6.Parameters["@dm_mst_CriteriaName"].Value = oCriteria.Name;
                    //' SUDHIR 20090309 - NEW FIELDS
                    _with6.Parameters["@dm_mst_PatientID"].Value = 0;
                    _with6.Parameters["@dm_mst_OriginalID"].Value = 0;
                    //' ''
                    _with6.Parameters["@dm_mst_AgeMin"].Value = oCriteria.AgeMinimum;
                    _with6.Parameters["@dm_mst_AgeMax"].Value = oCriteria.AgeMaximum;
                    _with6.Parameters["@dm_mst_Gender"].Value = oCriteria.Gender;
                    _with6.Parameters["@dm_mst_Race"].Value = oCriteria.Race;
                    _with6.Parameters["@dm_mst_MaritalStatus"].Value = oCriteria.MaritalStatus;
                    _with6.Parameters["@dm_mst_City"].Value = oCriteria.City;
                    _with6.Parameters["@dm_mst_Status"].Value = oCriteria.State;
                    _with6.Parameters["@dm_mst_Zip"].Value = oCriteria.Zip;
                    _with6.Parameters["@dm_mst_EmplyementStatus"].Value = oCriteria.EmployeeStatus;
                    _with6.Parameters["@dm_mst_HeightMin"].Value = oCriteria.HeightMinimum;
                    _with6.Parameters["@dm_mst_HeightMax"].Value = oCriteria.HeightMaximum;
                    if (oCriteria.WeightMinimum == 0.0)
                    {
                        _with6.Parameters["@dm_mst_WeightMin"].Value = System.DBNull.Value;
                    }
                    else
                    {
                        _with6.Parameters["@dm_mst_WeightMin"].Value = oCriteria.WeightMinimum;
                    }

                    if (oCriteria.WeightMaximum == 0.0)
                    {
                        _with6.Parameters["@dm_mst_WeightMax"].Value = System.DBNull.Value;
                    }
                    else
                    {
                        _with6.Parameters["@dm_mst_WeightMax"].Value = oCriteria.WeightMaximum;
                    }

                    if (oCriteria.BMIMinimum == 0.0)
                    {
                        _with6.Parameters["@dm_mst_BMIMin"].Value = System.DBNull.Value;
                    }
                    else
                    {
                        _with6.Parameters["@dm_mst_BMIMin"].Value = oCriteria.BMIMinimum;
                    }

                    if (oCriteria.BMIMaximum == 0.0)
                    {
                        _with6.Parameters["@dm_mst_BMIMax"].Value = System.DBNull.Value;
                    }
                    else
                    {
                        _with6.Parameters["@dm_mst_BMIMax"].Value = oCriteria.BMIMaximum;
                    }

                    if (oCriteria.PulseMinimum == 0.0)
                    {
                        _with6.Parameters["@dm_mst_PulseMin"].Value = System.DBNull.Value;
                    }
                    else
                    {
                        _with6.Parameters["@dm_mst_PulseMin"].Value = oCriteria.PulseMinimum;
                    }

                    if (oCriteria.PulseMaximum == 0.0)
                    {
                        _with6.Parameters["@dm_mst_PulseMax"].Value = System.DBNull.Value;
                    }
                    else
                    {
                        _with6.Parameters["@dm_mst_PulseMax"].Value = oCriteria.PulseMaximum;
                    }

                    if (oCriteria.BPSittingMinimum == 0.0)
                    {
                        _with6.Parameters["@dm_mst_BPSittingMin"].Value = System.DBNull.Value;
                    }
                    else
                    {
                        _with6.Parameters["@dm_mst_BPSittingMin"].Value = oCriteria.BPSittingMinimum;
                    }

                    if (oCriteria.BPSittingMaximum == 0.0)
                    {
                        _with6.Parameters["@dm_mst_BPSittingMax"].Value = System.DBNull.Value;
                    }
                    else
                    {
                        _with6.Parameters["@dm_mst_BPSittingMax"].Value = oCriteria.BPSittingMaximum;
                    }

                    if (oCriteria.BPStandingMinimum == 0.0)
                    {
                        _with6.Parameters["@dm_mst_BPStandingMin"].Value = System.DBNull.Value;
                    }
                    else
                    {
                        _with6.Parameters["@dm_mst_BPStandingMin"].Value = oCriteria.BPStandingMinimum;
                    }

                    if (oCriteria.BPStandingMaximum == 0.0)
                    {
                        _with6.Parameters["@dm_mst_BPStandingMax"].Value = System.DBNull.Value;
                    }
                    else
                    {
                        _with6.Parameters["@dm_mst_BPStandingMax"].Value = oCriteria.BPStandingMaximum;
                    }

                    _with6.Parameters["@dm_mst_DisplayMessage"].Value = oCriteria.DisplayMessage;
                    if (oCriteria.PulseOXMinimum == 0.0)
                    {
                        _with6.Parameters["@dm_mst_PulseOxMin"].Value = System.DBNull.Value;
                    }
                    else
                    {
                        _with6.Parameters["@dm_mst_PulseOxMin"].Value = oCriteria.PulseOXMinimum;
                    }

                    if (oCriteria.PulseOXMaximum == 0.0)
                    {
                        _with6.Parameters["@dm_mst_PulseOxMax"].Value = System.DBNull.Value;
                    }
                    else
                    {
                        _with6.Parameters["@dm_mst_PulseOxMax"].Value = oCriteria.PulseOXMaximum;
                    }

                    if (oCriteria.TempratureMinumum == 0.0)
                    {
                        _with6.Parameters["@dm_mst_TemperatureMin"].Value = System.DBNull.Value;
                    }
                    else
                    {
                        _with6.Parameters["@dm_mst_TemperatureMin"].Value = oCriteria.TempratureMinumum;
                    }

                    if (oCriteria.TempratureMaximum == 0.0)
                    {
                        _with6.Parameters["@dm_mst_TemperatureMax"].Value = System.DBNull.Value;
                    }
                    else
                    {
                        _with6.Parameters["@dm_mst_TemperatureMax"].Value = oCriteria.TempratureMaximum;
                    }



                    cmdCriteria.ExecuteNonQuery();


                    if ((objparam != null))
                    {
                        Criteria_ID = Convert.ToInt64(objparam.Value);
                        ////MsgBox(Criteria_ID)
                    }


                    if (Criteria_ID > 0)
                    {
                        cmdCriteria.Parameters.Clear();
                        cmdCriteria.CommandText = "DM_InCriteriaDTL";

                        //'Insert All OtherDetails.
                        for (int i = 1; i <= oCriteria.OtherDetails.Count; i++)
                        {
                            cmdCriteria.Parameters.Add("@dm_mst_Id", SqlDbType.BigInt);
                            cmdCriteria.Parameters.Add("@dm_dtl_Id", SqlDbType.BigInt);
                            cmdCriteria.Parameters.Add("@MachineID", SqlDbType.BigInt);
                            cmdCriteria.Parameters.Add("@dm_dtl_CategoryName", SqlDbType.VarChar);
                            cmdCriteria.Parameters.Add("@dm_dtl_ItemName", SqlDbType.VarChar);
                            cmdCriteria.Parameters.Add("@dm_dtl_Operator", SqlDbType.VarChar);
                            cmdCriteria.Parameters.Add("@dm_dtl_ResultValue1", SqlDbType.VarChar);
                            cmdCriteria.Parameters.Add("@dm_dtl_ResultValue2", SqlDbType.VarChar);
                            cmdCriteria.Parameters.Add("@dm_dtl_Type", SqlDbType.Int);

                            //MachineID = GetPrefixTransactionID();
                            cmdCriteria.Parameters["@dm_mst_Id"].Value = Criteria_ID;
                            cmdCriteria.Parameters["@dm_dtl_Id"].Value = 0;
                            cmdCriteria.Parameters["@MachineID"].Value = MachineID;
                            cmdCriteria.Parameters["@dm_dtl_CategoryName"].Value = oCriteria.OtherDetails[i].CategoryName;
                            cmdCriteria.Parameters["@dm_dtl_ItemName"].Value = oCriteria.OtherDetails[i].ItemName;
                            cmdCriteria.Parameters["@dm_dtl_Operator"].Value = oCriteria.OtherDetails[i].OperatorName;
                            cmdCriteria.Parameters["@dm_dtl_ResultValue1"].Value = oCriteria.OtherDetails[i].Result1;
                            cmdCriteria.Parameters["@dm_dtl_ResultValue2"].Value = oCriteria.OtherDetails[i].Result2;
                            cmdCriteria.Parameters["@dm_dtl_Type"].Value = oCriteria.OtherDetails[i].DetailType.GetHashCode();

                            cmdCriteria.ExecuteNonQuery();
                            cmdCriteria.Parameters.Clear();
                        }

                        //'END OF OTHER DETAILS


                        //'insert Orders into Template_MST table
                        cmdCriteria.Parameters.Clear();
                        cmdCriteria.CommandText = "DM_InsTemplates_DTL";

                        for (int i = 1; i <= oCriteria.LabOrders.Count; i++)
                        {
                            cmdCriteria.Parameters.Add("@Criteria_ID", SqlDbType.BigInt);
                            cmdCriteria.Parameters["@Criteria_ID"].Value = Criteria_ID;

                            cmdCriteria.Parameters.Add("@OrderID", SqlDbType.BigInt);
                            cmdCriteria.Parameters["@OrderID"].Value = ((myList)oCriteria.LabOrders[i]).Index;
                            //Item(i).TestID

                            cmdCriteria.Parameters.Add("@OrderType", SqlDbType.BigInt);
                            cmdCriteria.Parameters["@OrderType"].Value = TemplateCategoryID.Labs;


                            //sarika DM Denormalization 20090331
                            cmdCriteria.Parameters.Add("@TemplateName", SqlDbType.VarChar);
                            cmdCriteria.Parameters["@TemplateName"].Value = "";

                            cmdCriteria.Parameters.Add("@Template", SqlDbType.Image);
                            cmdCriteria.Parameters["@Template"].Value = null;


                            cmdCriteria.Parameters.Add("@dm_Templatedtl_TemplateDtlInfo", SqlDbType.VarChar);
                            cmdCriteria.Parameters["@dm_Templatedtl_TemplateDtlInfo"].Value = "";



                            //--


                            cmdCriteria.ExecuteNonQuery();
                            cmdCriteria.Parameters.Clear();
                        }
                        cmdCriteria.Parameters.Clear();
                        cmdCriteria.CommandText = "DM_InsTemplates_DTL";

                        for (int i = 1; i <= oCriteria.RadiologyOrders.Count; i++)
                        {
                            cmdCriteria.Parameters.Add("@Criteria_ID", SqlDbType.BigInt);
                            cmdCriteria.Parameters["@Criteria_ID"].Value = Criteria_ID;

                            cmdCriteria.Parameters.Add("@OrderID", SqlDbType.BigInt);
                            cmdCriteria.Parameters["@OrderID"].Value = ((myList)oCriteria.RadiologyOrders[i]).Index;
                            //Item(i).TestID

                            cmdCriteria.Parameters.Add("@OrderType", SqlDbType.BigInt);
                            cmdCriteria.Parameters["@OrderType"].Value = TemplateCategoryID.Radiology;


                            //sarika DM Denormalization 20090331
                            cmdCriteria.Parameters.Add("@TemplateName", SqlDbType.VarChar);
                            cmdCriteria.Parameters["@TemplateName"].Value = "";

                            cmdCriteria.Parameters.Add("@Template", SqlDbType.Image);
                            cmdCriteria.Parameters["@Template"].Value = null;

                            cmdCriteria.Parameters.Add("@dm_Templatedtl_TemplateDtlInfo", SqlDbType.VarChar);
                            cmdCriteria.Parameters["@dm_Templatedtl_TemplateDtlInfo"].Value = "";



                            //--

                            cmdCriteria.ExecuteNonQuery();
                            cmdCriteria.Parameters.Clear();
                        }
                        cmdCriteria.Parameters.Clear();
                        cmdCriteria.CommandText = "DM_InsTemplates_DTL";


                        for (int i = 1; i <= oCriteria.Referrals.Count; i++)
                        {
                            cmdCriteria.Parameters.Add("@Criteria_ID", SqlDbType.BigInt);
                            cmdCriteria.Parameters["@Criteria_ID"].Value = Criteria_ID;

                            cmdCriteria.Parameters.Add("@OrderID", SqlDbType.BigInt);
                            cmdCriteria.Parameters["@OrderID"].Value = ((myList)oCriteria.Referrals[i]).Index;
                            //Item(i).TestID

                            cmdCriteria.Parameters.Add("@OrderType", SqlDbType.BigInt);
                            cmdCriteria.Parameters["@OrderType"].Value = TemplateCategoryID.Referrals;


                            //sarika DM Denormalization 20090331
                            cmdCriteria.Parameters.Add("@TemplateName", SqlDbType.VarChar);
                            cmdCriteria.Parameters["@TemplateName"].Value = "";

                            cmdCriteria.Parameters.Add("@Template", SqlDbType.Image);
                            cmdCriteria.Parameters["@Template"].Value = null;

                            cmdCriteria.Parameters.Add("@dm_Templatedtl_TemplateDtlInfo", SqlDbType.VarChar);
                            cmdCriteria.Parameters["@dm_Templatedtl_TemplateDtlInfo"].Value = "";


                            //--


                            cmdCriteria.ExecuteNonQuery();
                            cmdCriteria.Parameters.Clear();
                        }
                        cmdCriteria.Parameters.Clear();
                        cmdCriteria.CommandText = "DM_InsTemplates_DTL";

                        for (int i = 1; i <= oCriteria.Guidelines.Count; i++)
                        {
                            cmdCriteria.Parameters.Add("@Criteria_ID", SqlDbType.BigInt);
                            cmdCriteria.Parameters["@Criteria_ID"].Value = Criteria_ID;

                            cmdCriteria.Parameters.Add("@OrderID", SqlDbType.BigInt);
                            cmdCriteria.Parameters["@OrderID"].Value = ((myList)oCriteria.Guidelines[i]).ID;
                            //Item(i).TestID

                            cmdCriteria.Parameters.Add("@OrderType", SqlDbType.BigInt);
                            cmdCriteria.Parameters["@OrderType"].Value = TemplateCategoryID.Guidelines;


                            //sarika DM Denormalization 20090331
                            cmdCriteria.Parameters.Add("@TemplateName", SqlDbType.VarChar);
                            cmdCriteria.Parameters["@TemplateName"].Value = ((myList)oCriteria.Guidelines[i]).DMTemplateName;

                            cmdCriteria.Parameters.Add("@Template", SqlDbType.Image);
                            cmdCriteria.Parameters["@Template"].Value = ((myList)oCriteria.Guidelines[i]).DMTemplate;

                            cmdCriteria.Parameters.Add("@dm_Templatedtl_TemplateDtlInfo", SqlDbType.VarChar);
                            cmdCriteria.Parameters["@dm_Templatedtl_TemplateDtlInfo"].Value = "";



                            //--


                            cmdCriteria.ExecuteNonQuery();
                            cmdCriteria.Parameters.Clear();
                        }
                        cmdCriteria.Parameters.Clear();
                        cmdCriteria.CommandText = "DM_InsTemplates_DTL";

                        for (int i = 1; i <= oCriteria.RxDrugs.Count; i++)
                        {
                            cmdCriteria.Parameters.Add("@Criteria_ID", SqlDbType.BigInt);
                            cmdCriteria.Parameters["@Criteria_ID"].Value = Criteria_ID;

                            cmdCriteria.Parameters.Add("@OrderID", SqlDbType.BigInt);
                            cmdCriteria.Parameters["@OrderID"].Value = ((myList)oCriteria.RxDrugs[i]).Index;
                            //Item(i).TestID

                            cmdCriteria.Parameters.Add("@OrderType", SqlDbType.BigInt);
                            cmdCriteria.Parameters["@OrderType"].Value = TemplateCategoryID.Rx;


                            //sarika DM Denormalization 20090331
                            cmdCriteria.Parameters.Add("@TemplateName", SqlDbType.VarChar);
                            cmdCriteria.Parameters["@TemplateName"].Value = ((myList)oCriteria.RxDrugs[i]).Dosage;

                            cmdCriteria.Parameters.Add("@Template", SqlDbType.Image);
                            cmdCriteria.Parameters["@Template"].Value = null;

                            cmdCriteria.Parameters.Add("@dm_Templatedtl_TemplateDtlInfo", SqlDbType.VarChar);
                            cmdCriteria.Parameters["@dm_Templatedtl_TemplateDtlInfo"].Value = ((myList)oCriteria.RxDrugs[i]).Dosage;


                            //--


                            cmdCriteria.ExecuteNonQuery();
                            cmdCriteria.Parameters.Clear();
                        }

                        myTrans.Commit();

                        //'gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DisclosureManagement, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Add, "'" & oCriteria.Name & "' Disease Management Criteria Added", gloAuditTrail.ActivityOutCome.Success)
                        //'Added Rahul P on 20101009
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DisclosureManagement, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Add, "'" + oCriteria.Name + "' Disease Management Criteria Added", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);
                        //'
                        //Dim objAudit As New clsAudit
                        //objAudit.CreateLog(clsAudit.enmActivityType.Add, "'" & oCriteria.Name & "' Disease Management Criteria Added", gstrLoginName, 0)
                        //objAudit = Nothing

                        
                    }
                    else
                    {
                        myTrans.Rollback();
                    }

                }
                catch (Exception ex)
                {
                    //if some error occur when inserting in any of the tables then all the transactions are rollbacked
                    try
                    {
                        myTrans.Rollback();
                    }
                    catch //(SqlException ex1)
                    {
                        if ((myTrans.Connection != null))
                        {
                            //Console.WriteLine("An exception of type " & ex1.GetType().ToString() & _
                            //" was encountered while attempting to roll back the transaction.")
                            //UpdateLog("clsDiseaseManagement -- Add -- " + ex.ToString());
                            _ErrorMessage = ex.Message;
                        }
                        //clsGeneral.UpdateLog("glocommerror while adding DiseaseManagement in Class DMSetup"+ex.ToString());  
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                    }
                    //UpdateLog("clsDiseaseManagement -- Add -- " + ex.ToString());
                    _ErrorMessage = ex.Message;
                    _ErrorMessage = "Neither record was written to database.";
                }
                finally
                {
                    if (cmdCriteria != null)
                    {
                        cmdCriteria.Parameters.Clear();
                        cmdCriteria.Dispose();
                        cmdCriteria = null;
                    }
                    if (objparam  != null)
                    {
                        objparam  = null;
                    }
                    conn.Close();
                    conn.Dispose();
                    conn = null;
                }
                return Criteria_ID;
            }

            public Int64 AddPatientCriteria(ArrayList ItemList, Int64 CriteriaID, Int64 PatientID, string CriteriaName, string Message, bool IsPatientSpecific)
            {
                //Dim ODB As New gloStream.gloDataBase.gloDataBase
                SqlConnection conn = new SqlConnection(clsGeneral.EMRConnectionString);

                //declare a transaction object
                SqlTransaction myTrans = null;
                SqlCommand cmdCriteria = null;
                SqlParameter objparam = null;
                long MachineID = 0;


                try
                {
                    conn.Open();

                    myTrans = conn.BeginTransaction();

                    cmdCriteria = conn.CreateCommand();
                    cmdCriteria.Transaction = myTrans;

                    var _with7 = cmdCriteria;
                    _with7.Connection = conn;
                    _with7.CommandType = CommandType.StoredProcedure;
                    _with7.CommandText = "DM_InsUpdCriteria";

                    objparam = new SqlParameter("@dm_mst_Id", SqlDbType.BigInt);
                    var _with8 = cmdCriteria.Parameters;
                    objparam.Direction = ParameterDirection.InputOutput;
                    _with8.Add(objparam);
                    _with8.Add("@MachineID", SqlDbType.BigInt);
                    _with8.Add("@dm_mst_CriteriaName", SqlDbType.VarChar);
                    _with8.Add("@dm_mst_AgeMin", SqlDbType.Decimal);
                    _with8.Add("@dm_mst_AgeMax", SqlDbType.Decimal);
                    _with8.Add("@dm_mst_Gender", SqlDbType.VarChar);
                    _with8.Add("@dm_mst_Race", SqlDbType.VarChar);
                    _with8.Add("@dm_mst_MaritalStatus", SqlDbType.VarChar);
                    _with8.Add("@dm_mst_City", SqlDbType.VarChar);
                    _with8.Add("@dm_mst_Status", SqlDbType.VarChar);
                    _with8.Add("@dm_mst_Zip", SqlDbType.VarChar);
                    _with8.Add("@dm_mst_EmplyementStatus", SqlDbType.VarChar);
                    _with8.Add("@dm_mst_HeightMin", SqlDbType.VarChar);
                    _with8.Add("@dm_mst_HeightMax", SqlDbType.VarChar);
                    _with8.Add("@dm_mst_WeightMin", SqlDbType.Decimal);
                    _with8.Add("@dm_mst_WeightMax", SqlDbType.Decimal);
                    _with8.Add("@dm_mst_BMIMin", SqlDbType.Decimal);
                    _with8.Add("@dm_mst_BMIMax", SqlDbType.Decimal);
                    _with8.Add("@dm_mst_TemperatureMin", SqlDbType.Decimal);
                    _with8.Add("@dm_mst_TemperatureMax", SqlDbType.Decimal);
                    _with8.Add("@dm_mst_PulseMin", SqlDbType.Decimal);
                    _with8.Add("@dm_mst_PulseMax", SqlDbType.Decimal);
                    _with8.Add("@dm_mst_PulseOxMin", SqlDbType.Decimal);
                    _with8.Add("@dm_mst_PulseOxMax", SqlDbType.Decimal);
                    _with8.Add("@dm_mst_BPSittingMin", SqlDbType.Decimal);
                    _with8.Add("@dm_mst_BPSittingMax", SqlDbType.Decimal);
                    _with8.Add("@dm_mst_BPStandingMin", SqlDbType.Decimal);
                    _with8.Add("@dm_mst_BPStandingMax", SqlDbType.Decimal);
                    _with8.Add("@dm_mst_DisplayMessage", SqlDbType.VarChar);
                    _with8.Add("@dm_mst_PatientID", SqlDbType.BigInt);
                    _with8.Add("@dm_mst_OriginalID", SqlDbType.BigInt);

                    var _with9 = cmdCriteria;
                    //MachineID = GetPrefixTransactionID(PatientID);

                    //' IF PATIENT SPECIFIC THEN UPDATE CRITERIA
                    if (IsPatientSpecific == true)
                    {
                        objparam.Value = CriteriaID;
                        //'ELSE CREATE COPY OF CRITERIA AND SAVE AGAINST PATIENT
                    }
                    else
                    {
                        objparam.Value = 0;
                    }

                    _with9.Parameters["@MachineID"].Value = MachineID;
                    _with9.Parameters["@dm_mst_CriteriaName"].Value = CriteriaName;
                    _with9.Parameters["@dm_mst_PatientID"].Value = PatientID;
                    if (IsPatientSpecific == true)
                    {
                        _with9.Parameters["@dm_mst_OriginalID"].Value = 0;
                        //'IF PATIENT SPECIFIC, THEN BY DEFAULT ZERO.
                    }
                    else
                    {
                        _with9.Parameters["@dm_mst_OriginalID"].Value = CriteriaID;
                        //'SAVE CRITERIA ID IN THIS FIELD TO KEEP REFERENCE OF COPIED CRITERIA.
                    }

                    _with9.Parameters["@dm_mst_DisplayMessage"].Value = Message;

                    //' BLANK VALUES ''
                    _with9.Parameters["@dm_mst_AgeMin"].Value = 0;
                    _with9.Parameters["@dm_mst_AgeMax"].Value = 0;
                    _with9.Parameters["@dm_mst_Gender"].Value = "";
                    _with9.Parameters["@dm_mst_Race"].Value = "";
                    _with9.Parameters["@dm_mst_MaritalStatus"].Value = "";
                    _with9.Parameters["@dm_mst_City"].Value = "";
                    _with9.Parameters["@dm_mst_Status"].Value = "";
                    _with9.Parameters["@dm_mst_Zip"].Value = "";
                    _with9.Parameters["@dm_mst_EmplyementStatus"].Value = "";
                    _with9.Parameters["@dm_mst_HeightMin"].Value = "" + "'" + "" + "''";
                    _with9.Parameters["@dm_mst_HeightMax"].Value = "" + "'" + "" + "''";
                    _with9.Parameters["@dm_mst_WeightMin"].Value = 0;
                    _with9.Parameters["@dm_mst_WeightMax"].Value = 0;
                    _with9.Parameters["@dm_mst_BMIMin"].Value = 0;
                    _with9.Parameters["@dm_mst_BMIMax"].Value = 0;
                    _with9.Parameters["@dm_mst_PulseMin"].Value = 0;
                    _with9.Parameters["@dm_mst_PulseMax"].Value = 0;
                    _with9.Parameters["@dm_mst_BPSittingMin"].Value = 0;
                    _with9.Parameters["@dm_mst_BPSittingMax"].Value = 0;
                    _with9.Parameters["@dm_mst_BPStandingMin"].Value = 0;
                    _with9.Parameters["@dm_mst_BPStandingMax"].Value = 0;
                    _with9.Parameters["@dm_mst_PulseOxMin"].Value = 0;
                    _with9.Parameters["@dm_mst_PulseOxMax"].Value = 0;
                    _with9.Parameters["@dm_mst_TemperatureMin"].Value = 0;
                    _with9.Parameters["@dm_mst_TemperatureMax"].Value = 0;
                    //' ''

                    cmdCriteria.ExecuteNonQuery();


                    if ((objparam != null))
                    {
                        CriteriaID =Convert.ToInt64( objparam.Value);
                        ////MsgBox(Criteria_ID)
                    }

                    if (CriteriaID > 0)
                    {
                        //'EMPTY TEMPLATE DETAILS IF UPDATING CRITERIA
                        cmdCriteria.CommandType = CommandType.Text;
                        cmdCriteria.CommandText = "DELETE FROM DM_Templates_DTL WHERE dm_Templatedtl_Id = " + CriteriaID + "";
                        cmdCriteria.ExecuteNonQuery();

                        //'insert Orders into Template_MST table
                        cmdCriteria.CommandType = CommandType.StoredProcedure;
                        cmdCriteria.Parameters.Clear();
                        cmdCriteria.CommandText = "DM_InsTemplates_DTL";
                        for (int i = 0; i <= ItemList.Count - 1; i++)
                        {
                            cmdCriteria.Parameters.Add("@Criteria_ID", SqlDbType.BigInt);
                            cmdCriteria.Parameters["@Criteria_ID"].Value = CriteriaID;

                            cmdCriteria.Parameters.Add("@OrderID", SqlDbType.BigInt);
                            cmdCriteria.Parameters["@OrderID"].Value = ((myList)ItemList[i]).ID;

                            cmdCriteria.Parameters.Add("@OrderType", SqlDbType.BigInt);
                            cmdCriteria.Parameters["@OrderType"].Value = ((myList)ItemList[i]).Index;

                            //sarika DM Denormalization 20090331
                            cmdCriteria.Parameters.Add("@TemplateName", SqlDbType.VarChar);
                            //If CType(ItemList(i), myList).Index = 0 Then
                            cmdCriteria.Parameters["@TemplateName"].Value = ((myList)ItemList[i]).DMTemplateName;

                            //Else
                            //cmdCriteria.Parameters["@TemplateName"].Value = ""
                            //End If


                            cmdCriteria.Parameters.Add("@Template", SqlDbType.Image);
                            //if guideline
                            //If CType(ItemList(i), myList).Index = 0 Then
                            cmdCriteria.Parameters["@Template"].Value = ((myList)ItemList[i]).DMTemplate;
                            //Else
                            //cmdCriteria.Parameters["@Template"].Value = Nothing
                            //End If

                            cmdCriteria.Parameters.Add("@dm_Templatedtl_TemplateDtlInfo", SqlDbType.VarChar);
                            cmdCriteria.Parameters.Add("@sDrugForm", SqlDbType.VarChar);
                            cmdCriteria.Parameters.Add("@sRoute", SqlDbType.VarChar);
                            cmdCriteria.Parameters.Add("@sFrequency", SqlDbType.VarChar);
                            cmdCriteria.Parameters.Add("@sNDCCode", SqlDbType.VarChar);
                            cmdCriteria.Parameters.Add("@nIsNarcotics", SqlDbType.Int);
                            cmdCriteria.Parameters.Add("@sDuration", SqlDbType.VarChar);
                            cmdCriteria.Parameters.Add("@mpid", SqlDbType.Int);

                            cmdCriteria.Parameters.Add("@sDrugQtyQualifier", SqlDbType.VarChar);

                            if ((TemplateCategoryID)cmdCriteria.Parameters["@OrderType"].Value == TemplateCategoryID.Rx)
                            {
                                cmdCriteria.Parameters["@dm_Templatedtl_TemplateDtlInfo"].Value = ((myList)ItemList[i]).Dosage;

                                //sarika DM Denormalization for Rx on 20090410
                                myList MyItemList = ((myList)ItemList[i]);
                                if ((MyItemList.DrugForm != null))
                                {
                                    cmdCriteria.Parameters["@sDrugForm"].Value = MyItemList.DrugForm;
                                }
                                else
                                {
                                    cmdCriteria.Parameters["@sDrugForm"].Value = "";
                                }


                                if ((MyItemList.Route != null))
                                {
                                    cmdCriteria.Parameters["@sRoute"].Value = MyItemList.Route;
                                }
                                else
                                {
                                    cmdCriteria.Parameters["@sRoute"].Value = "";
                                }


                                if ((MyItemList.Frequency != null))
                                {
                                    cmdCriteria.Parameters["@sFrequency"].Value = MyItemList.Frequency;
                                }
                                else
                                {
                                    cmdCriteria.Parameters["@sFrequency"].Value = "";
                                }


                                if ((MyItemList.NDCCode != null))
                                {
                                    cmdCriteria.Parameters["@sNDCCode"].Value = MyItemList.NDCCode;
                                }
                                else
                                {
                                    cmdCriteria.Parameters["@sNDCCode"].Value = "";
                                }

                          //      if (((myList)ItemList[i]).IsNarcotic != null)
                                {
                                    cmdCriteria.Parameters["@nIsNarcotics"].Value = MyItemList.IsNarcotic;
                                }
                                //else
                                //{
                                //    cmdCriteria.Parameters["@nIsNarcotics"].Value = 0;
                                //}

                              //  if ((((myList)ItemList[i]).mpid != null))
                                {
                                    cmdCriteria.Parameters["@mpid"].Value = MyItemList.mpid;
                                }
                                //else
                                //{
                                //    cmdCriteria.Parameters["@mpid"].Value = "";
                                //}

                                if ((MyItemList.Duration != null))
                                {
                                    cmdCriteria.Parameters["@sDuration"].Value = MyItemList.Duration;
                                }
                                else
                                {
                                    cmdCriteria.Parameters["@sDuration"].Value = "";
                                }


                                if ((MyItemList.DrugQtyQualifier != null))
                                {
                                    cmdCriteria.Parameters["@sDrugQtyQualifier"].Value = MyItemList.DrugQtyQualifier;
                                }
                                else
                                {
                                    cmdCriteria.Parameters["@sDrugQtyQualifier"].Value = "";
                                }


                                //----

                            }
                            else
                            {
                                cmdCriteria.Parameters["@dm_Templatedtl_TemplateDtlInfo"].Value = "";
                                cmdCriteria.Parameters["@sDrugForm"].Value = "";
                                cmdCriteria.Parameters["@sRoute"].Value = "";
                                cmdCriteria.Parameters["@sFrequency"].Value = "";

                                cmdCriteria.Parameters["@sNDCCode"].Value = "";

                                cmdCriteria.Parameters["@nIsNarcotics"].Value = 0;

                                cmdCriteria.Parameters["@sDuration"].Value = "";
                                
                                cmdCriteria.Parameters["@sDrugQtyQualifier"].Value = "";
                            }

                            //--

                            cmdCriteria.ExecuteNonQuery();
                            cmdCriteria.Parameters.Clear();
                        }

                        myTrans.Commit();
                        //'gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DisclosureManagement, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Add, "'" & CriteriaName & "' Disease Management Patient Criteria Added", gloAuditTrail.ActivityOutCome.Success)
                        //'Added Rahul P on 20101009
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DisclosureManagement, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Add, "'" + CriteriaName + "' Disease Management Patient Criteria Added", PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);
                        //'

                        //Dim objAudit As New clsAudit
                        //objAudit.CreateLog(clsAudit.enmActivityType.Add, "'" & CriteriaName & "' Disease Management Patient Criteria Added", gstrLoginName, 0)
                        //objAudit = Nothing

                        return CriteriaID;
                    }
                    else
                    {
                        myTrans.Rollback();
                    }

                }
                catch (Exception ex)
                {
                    //if some error occur when inserting in any of the tables then all the transactions are rollbacked
                    try
                    {
                        myTrans.Rollback();
                    }
                    catch //(SqlException ex1)
                    {
                        if ((myTrans.Connection != null))
                        {
                            //Console.WriteLine("An exception of type " & ex1.GetType().ToString() & _
                            //" was encountered while attempting to roll back the transaction.")
                            //UpdateLog("clsDiseaseManagement -- Add -- " + ex.ToString());
                            _ErrorMessage = ex.Message;
                        }
                    }
                    //UpdateLog("clsDiseaseManagement -- Add -- " + ex.ToString());
                    _ErrorMessage = ex.Message;
                    _ErrorMessage = "Neither record was written to database.";
                }
                finally
                {
                    if (cmdCriteria != null)
                    {
                        cmdCriteria.Parameters.Clear();
                        cmdCriteria.Dispose();
                        cmdCriteria = null;
                    }
                    if (objparam  != null)
                    {
                        objparam  = null;
                    }
                    conn.Close();
                    conn.Dispose();
                    conn = null;
                }
                return CriteriaID;
            }

            public bool Modify(string oModifyCriteriaName, gloStream.DiseaseManagement.Supporting.Criteria oCriteria)
            {
                SqlConnection conn = new SqlConnection(clsGeneral.EMRConnectionString);
                long ID = 0;
                SqlCommand cmd = null;
                bool IsModify = false;
                try
                {
                    //do the validations
                    if (string.IsNullOrEmpty(oCriteria.Name))
                    {
                        _ErrorMessage = "Please enter the Criteria name. ";
                    }

                    cmd = new SqlCommand();
                    conn.Open();
                    var _with10 = cmd;
                    _with10.Connection = conn;
                    _with10.CommandType = CommandType.Text;
                    _with10.CommandText = "SELECT dm_mst_Id FROM DM_Criteria_MST where dm_mst_CriteriaName = '" + oModifyCriteriaName + "'";

                    ID = Convert.ToInt64(cmd.ExecuteScalar());
                    Modify(ID, oCriteria);

                    conn.Close();
                    IsModify = true;
                }
                catch //(Exception ex)
                {
                    //UpdateLog("clsDiseaseManagement -- Modify(1) -- " + ex.ToString());
                    //MessageBox.Show(ex.ToString(),clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (cmd != null)
                    {
                        cmd.Parameters.Clear();
                        cmd.Dispose();
                        cmd = null;
                    }
                }
                return IsModify;
            }

            public Int64 Modify(long oModifyCriteriaID, gloStream.DiseaseManagement.Supporting.Criteria oCriteria)
            {
                //declare a connection object and pass it the connection string
                SqlConnection conn = new SqlConnection(clsGeneral.EMRConnectionString);
                //declare a command object
                //declare a transaction object
                SqlTransaction myTrans = null;
                SqlCommand cmdCriteria = null;
                SqlParameter objparam = null;
                try
                {
                    //do the validations 
                    if (oModifyCriteriaID == 0)
                    {
                        _ErrorMessage = "Please enter the CriteriaID . ";
                    }

                    conn.Open();

                    myTrans = conn.BeginTransaction();

                    cmdCriteria = conn.CreateCommand();
                    cmdCriteria.Transaction = myTrans;

                    var _with11 = cmdCriteria;
                    _with11.Connection = conn;
                    _with11.CommandType = CommandType.StoredProcedure;
                    _with11.CommandText = "DM_InsUpdCriteria";

                    objparam = new SqlParameter("@dm_mst_Id", SqlDbType.BigInt);
                    var _with12 = cmdCriteria.Parameters;
                    //.Add("@dm_mst_Id", SqlDbType.BigInt)
                    //objparam.Direction = ParameterDirection.InputOutput
                    _with12.Add(objparam);
                    _with12.Add("@MachineID", SqlDbType.BigInt);
                    _with12.Add("@dm_mst_CriteriaName", SqlDbType.VarChar);
                    _with12.Add("@dm_mst_AgeMin", SqlDbType.Decimal);
                    _with12.Add("@dm_mst_AgeMax", SqlDbType.Decimal);
                    _with12.Add("@dm_mst_Gender", SqlDbType.VarChar);
                    _with12.Add("@dm_mst_Race", SqlDbType.VarChar);
                    _with12.Add("@dm_mst_MaritalStatus", SqlDbType.VarChar);
                    _with12.Add("@dm_mst_City", SqlDbType.VarChar);
                    _with12.Add("@dm_mst_Status", SqlDbType.VarChar);
                    _with12.Add("@dm_mst_Zip", SqlDbType.VarChar);
                    _with12.Add("@dm_mst_EmplyementStatus", SqlDbType.VarChar);
                    _with12.Add("@dm_mst_HeightMin", SqlDbType.VarChar);
                    _with12.Add("@dm_mst_HeightMax", SqlDbType.VarChar);
                    _with12.Add("@dm_mst_WeightMin", SqlDbType.Decimal);
                    _with12.Add("@dm_mst_WeightMax", SqlDbType.Decimal);
                    _with12.Add("@dm_mst_BMIMin", SqlDbType.Decimal);
                    _with12.Add("@dm_mst_BMIMax", SqlDbType.Decimal);
                    _with12.Add("@dm_mst_TemperatureMin", SqlDbType.Decimal);
                    _with12.Add("@dm_mst_TemperatureMax", SqlDbType.Decimal);
                    _with12.Add("@dm_mst_PulseMin", SqlDbType.Decimal);
                    _with12.Add("@dm_mst_PulseMax", SqlDbType.Decimal);
                    _with12.Add("@dm_mst_PulseOxMin", SqlDbType.Decimal);
                    _with12.Add("@dm_mst_PulseOxMax", SqlDbType.Decimal);
                    _with12.Add("@dm_mst_BPSittingMin", SqlDbType.Decimal);
                    _with12.Add("@dm_mst_BPSittingMax", SqlDbType.Decimal);
                    _with12.Add("@dm_mst_BPStandingMin", SqlDbType.Decimal);
                    _with12.Add("@dm_mst_BPStandingMax", SqlDbType.Decimal);
                    _with12.Add("@dm_mst_DisplayMessage", SqlDbType.VarChar);
                    _with12.Add("@dm_mst_PatientID", SqlDbType.BigInt);
                    _with12.Add("@dm_mst_OriginalID", SqlDbType.BigInt);

                    var _with13 = cmdCriteria;
                    //MachineID = GetPrefixTransactionID()

                    objparam.Value = oModifyCriteriaID;
                    //.Parameters["@dm_mst_Id"].Value = Criteria_ID
                    _with13.Parameters["@MachineID"].Value = 0;
                    //MachineID
                    //' SUDHIR 20090309 - NEW FIELDS
                    _with13.Parameters["@dm_mst_PatientID"].Value = 0;
                    _with13.Parameters["@dm_mst_OriginalID"].Value = 0;
                    //' ''
                    _with13.Parameters["@dm_mst_CriteriaName"].Value = oCriteria.Name;
                    _with13.Parameters["@dm_mst_AgeMin"].Value = oCriteria.AgeMinimum;
                    _with13.Parameters["@dm_mst_AgeMax"].Value = oCriteria.AgeMaximum;
                    _with13.Parameters["@dm_mst_Gender"].Value = oCriteria.Gender;
                    _with13.Parameters["@dm_mst_Race"].Value = oCriteria.Race;
                    _with13.Parameters["@dm_mst_MaritalStatus"].Value = oCriteria.MaritalStatus;
                    _with13.Parameters["@dm_mst_City"].Value = oCriteria.City;
                    _with13.Parameters["@dm_mst_Status"].Value = oCriteria.State;
                    _with13.Parameters["@dm_mst_Zip"].Value = oCriteria.Zip;
                    _with13.Parameters["@dm_mst_EmplyementStatus"].Value = oCriteria.EmployeeStatus;
                    _with13.Parameters["@dm_mst_HeightMin"].Value = oCriteria.HeightMinimum;
                    _with13.Parameters["@dm_mst_HeightMax"].Value = oCriteria.HeightMaximum;
                    _with13.Parameters["@dm_mst_WeightMin"].Value = oCriteria.WeightMinimum;
                    _with13.Parameters["@dm_mst_WeightMax"].Value = oCriteria.WeightMaximum;
                    _with13.Parameters["@dm_mst_BMIMin"].Value = oCriteria.BMIMinimum;
                    _with13.Parameters["@dm_mst_BMIMax"].Value = oCriteria.BMIMaximum;
                    _with13.Parameters["@dm_mst_PulseMin"].Value = oCriteria.PulseMinimum;
                    _with13.Parameters["@dm_mst_PulseMax"].Value = oCriteria.PulseMaximum;
                    _with13.Parameters["@dm_mst_BPSittingMin"].Value = oCriteria.BPSittingMinimum;
                    _with13.Parameters["@dm_mst_BPSittingMax"].Value = oCriteria.BPSittingMaximum;
                    _with13.Parameters["@dm_mst_BPStandingMin"].Value = oCriteria.BPStandingMinimum;
                    _with13.Parameters["@dm_mst_BPStandingMax"].Value = oCriteria.BPStandingMaximum;
                    _with13.Parameters["@dm_mst_DisplayMessage"].Value = oCriteria.DisplayMessage;
                    _with13.Parameters["@dm_mst_PulseOxMin"].Value = oCriteria.PulseOXMinimum;
                    _with13.Parameters["@dm_mst_PulseOxMax"].Value = oCriteria.PulseOXMaximum;
                    _with13.Parameters["@dm_mst_TemperatureMin"].Value = oCriteria.TempratureMinumum;
                    _with13.Parameters["@dm_mst_TemperatureMax"].Value = oCriteria.TempratureMaximum;

                    cmdCriteria.ExecuteNonQuery();

                    //Delete all detail table - extra as compare to add

                    cmdCriteria.Parameters.Clear();

                    var _with14 = cmdCriteria;
                    _with14.Connection = conn;
                    _with14.CommandType = CommandType.Text;
                    _with14.CommandText = "delete from DM_CriteriaDrug_DTL where dm_Drugdtl_Id =" + oModifyCriteriaID;
                    cmdCriteria.ExecuteNonQuery();

                    //deleting from DM_CriteriaHistory_DTL table
                    cmdCriteria.CommandText = "delete from DM_CriteriaHistory_DTL where  dm_Chdtl_Id =" + oModifyCriteriaID;
                    cmdCriteria.ExecuteNonQuery();

                    //deleting from DM_ICD9CPT_DTL table
                    cmdCriteria.CommandText = "delete from DM_ICD9CPT_DTL where  dm_ICD9CPTdtl_Id =" + oModifyCriteriaID;
                    cmdCriteria.ExecuteNonQuery();

                    //deleting from DM_ICD9CPT_DTL table
                    cmdCriteria.CommandText = "delete from DM_Templates_DTL where  dm_Templatedtl_Id =" + oModifyCriteriaID;
                    cmdCriteria.ExecuteNonQuery();

                    //deleting from DM_LabModule_DTL table
                    cmdCriteria.CommandText = "delete from DM_LabModule_DTL where  dm_labdtl_ID =" + oModifyCriteriaID;
                    cmdCriteria.ExecuteNonQuery();

                    //deleting from DM_Labs_DTL table 
                    cmdCriteria.CommandText = "delete from DM_Labs_DTL where  dm_Labsdtl_Id =" + oModifyCriteriaID;
                    cmdCriteria.ExecuteNonQuery();

                    //all details table as it is Add
                    //Insert into the detail table DM_CriteriaDrug_DTL
                    // cmdCriteria.Parameters.Clear()
                    var _with15 = cmdCriteria;
                    _with15.Connection = conn;
                    _with15.CommandType = CommandType.StoredProcedure;
                    _with15.CommandText = "DM_InsCriteriaDrugDTL";

                    for (int i = 1; i <= oCriteria.Drugs.Count; i++)
                    {
                        cmdCriteria.Parameters.Add("@dm_Drugdtl_Id", SqlDbType.BigInt);
                        cmdCriteria.Parameters["@dm_Drugdtl_Id"].Value = oModifyCriteriaID;
                        cmdCriteria.Parameters.Add("@dm_Drugdtl_DrugID", SqlDbType.BigInt);
                        cmdCriteria.Parameters["@dm_Drugdtl_DrugID"].Value = oCriteria.Drugs[i];

                        cmdCriteria.ExecuteNonQuery();
                        cmdCriteria.Parameters.Clear();
                    }

                    //Insert into the second detail  table DM_CriteriaHistory_DTL 
                    cmdCriteria.Parameters.Clear();
                    cmdCriteria.CommandText = "DM_CriteriaHistoryDTL";

                    for (int i = 1; i <= oCriteria.Histories.Count; i++)
                    {
                        cmdCriteria.Parameters.Add("@dm_Chdtl_Id", SqlDbType.BigInt);
                        cmdCriteria.Parameters["@dm_Chdtl_Id"].Value = oModifyCriteriaID;

                        gloStream.DiseaseManagement.Supporting.HistoryItem objNewMember = new gloStream.DiseaseManagement.Supporting.HistoryItem();
                        objNewMember = (gloStream.DiseaseManagement.Supporting.HistoryItem)oCriteria.Histories[i];

                        cmdCriteria.Parameters.Add("@dm_Chdtl_HistoryTypeId", SqlDbType.BigInt);
                        cmdCriteria.Parameters["@dm_Chdtl_HistoryTypeId"].Value = objNewMember.CategoryID;

                        cmdCriteria.Parameters.Add("@dm_Chdtl_HistoryItemId", SqlDbType.BigInt);
                        cmdCriteria.Parameters["@dm_Chdtl_HistoryItemId"].Value = objNewMember.ID;

                        //' Sudhir 20090302 ''
                        cmdCriteria.Parameters.Add("@dm_Chdtl_HistoryItem", SqlDbType.VarChar);
                        cmdCriteria.Parameters["@dm_Chdtl_HistoryItem"].Value = objNewMember.Name;

                        cmdCriteria.Parameters.Add("@dm_Chdtl_HistoryCategory", SqlDbType.VarChar);
                        cmdCriteria.Parameters["@dm_Chdtl_HistoryCategory"].Value = objNewMember.CategoryName;
                        //' End Sudhir ''
                        cmdCriteria.ExecuteNonQuery();
                        cmdCriteria.Parameters.Clear();
                    }

                    //Insert into the third detail  table 
                    //insert ICD records

                    cmdCriteria.Parameters.Clear();
                    cmdCriteria.CommandText = "DM_InsICD9CPTDTL";

                    for (int i = 1; i <= oCriteria.ICD9s.Count; i++)
                    {
                        cmdCriteria.Parameters.Add("@dm_ICD9CPTdtl_Id", SqlDbType.BigInt);
                        cmdCriteria.Parameters["@dm_ICD9CPTdtl_Id"].Value = oModifyCriteriaID;
                        cmdCriteria.Parameters.Add("@dm_ICD9CPTdtl_ICID", SqlDbType.BigInt);
                        cmdCriteria.Parameters["@dm_ICD9CPTdtl_ICID"].Value = oCriteria.ICD9s[i];
                        cmdCriteria.Parameters.Add("@dm_ICD9CPTdtl_Type", SqlDbType.BigInt);
                        cmdCriteria.Parameters["@dm_ICD9CPTdtl_Type"].Value = nICDTypeID;

                        cmdCriteria.ExecuteNonQuery();
                        cmdCriteria.Parameters.Clear();
                    }


                    //insert CPT records
                    cmdCriteria.Parameters.Clear();
                    cmdCriteria.CommandText = "DM_InsICD9CPTDTL";

                    for (int i = 1; i <= oCriteria.CPTs.Count; i++)
                    {
                        cmdCriteria.Parameters.Add("@dm_ICD9CPTdtl_Id", SqlDbType.BigInt);
                        cmdCriteria.Parameters["@dm_ICD9CPTdtl_Id"].Value = oModifyCriteriaID;
                        cmdCriteria.Parameters.Add("@dm_ICD9CPTdtl_ICID", SqlDbType.BigInt);
                        cmdCriteria.Parameters["@dm_ICD9CPTdtl_ICID"].Value = oCriteria.CPTs[i];
                        cmdCriteria.Parameters.Add("@dm_ICD9CPTdtl_Type", SqlDbType.BigInt);
                        cmdCriteria.Parameters["@dm_ICD9CPTdtl_Type"].Value = nCPTTypeID;

                        cmdCriteria.ExecuteNonQuery();
                        cmdCriteria.Parameters.Clear();
                    }

                    //'insert Guidlines records
                    //cmdCriteria.Parameters.Clear()
                    //cmdCriteria.CommandText = "DM_InsTemplates_DTL"

                    //For i As Integer = 1 To oCriteria.Guidelines.Count
                    //    cmdCriteria.Parameters.Add("@dm_Templatedtl_Id", SqlDbType.BigInt)
                    //    cmdCriteria.Parameters["@dm_Templatedtl_Id"].Value = oModifyCriteriaID
                    //    cmdCriteria.Parameters.Add("@dm_Templatedtl_TemplateID", SqlDbType.BigInt)
                    //    cmdCriteria.Parameters["@dm_Templatedtl_TemplateID"].Value = CType(oCriteria.Guidelines.Item(i), myList).Index
                    //    cmdCriteria.Parameters.Add("@dm_Templatedtl_CategoryID", SqlDbType.BigInt)
                    //    cmdCriteria.Parameters["@dm_Templatedtl_CategoryID"].Value = 0

                    //    cmdCriteria.ExecuteNonQuery()
                    //    cmdCriteria.Parameters.Clear()
                    //Next

                    //'insert Orders into Template_MST table
                    cmdCriteria.Parameters.Clear();
                    cmdCriteria.CommandText = "DM_InsTemplates_DTL";

                    for (int i = 1; i <= oCriteria.LabOrders.Count; i++)
                    {
                        cmdCriteria.Parameters.Add("@Criteria_ID", SqlDbType.BigInt);
                        cmdCriteria.Parameters["@Criteria_ID"].Value = oModifyCriteriaID;

                        cmdCriteria.Parameters.Add("@OrderID", SqlDbType.BigInt);
                        cmdCriteria.Parameters["@OrderID"].Value = ((myList)oCriteria.LabOrders[i]).Index;
                        //Item(i).TestID

                        cmdCriteria.Parameters.Add("@OrderType", SqlDbType.BigInt);
                        cmdCriteria.Parameters["@OrderType"].Value = TemplateCategoryID.Labs;

                        cmdCriteria.ExecuteNonQuery();
                        cmdCriteria.Parameters.Clear();
                    }

                    cmdCriteria.Parameters.Clear();
                    cmdCriteria.CommandText = "DM_InsTemplates_DTL";

                    for (int i = 1; i <= oCriteria.RadiologyOrders.Count; i++)
                    {
                        cmdCriteria.Parameters.Add("@Criteria_ID", SqlDbType.BigInt);
                        cmdCriteria.Parameters["@Criteria_ID"].Value = oModifyCriteriaID;

                        cmdCriteria.Parameters.Add("@OrderID", SqlDbType.BigInt);
                        cmdCriteria.Parameters["@OrderID"].Value = ((myList)oCriteria.RadiologyOrders[i]).Index;
                        //Item(i).TestID

                        cmdCriteria.Parameters.Add("@OrderType", SqlDbType.BigInt);
                        cmdCriteria.Parameters["@OrderType"].Value = TemplateCategoryID.Radiology;

                        cmdCriteria.ExecuteNonQuery();
                        cmdCriteria.Parameters.Clear();
                    }
                    cmdCriteria.Parameters.Clear();
                    cmdCriteria.CommandText = "DM_InsTemplates_DTL";



                    for (int i = 1; i <= oCriteria.Referrals.Count; i++)
                    {
                        cmdCriteria.Parameters.Add("@Criteria_ID", SqlDbType.BigInt);
                        cmdCriteria.Parameters["@Criteria_ID"].Value = oModifyCriteriaID;

                        cmdCriteria.Parameters.Add("@OrderID", SqlDbType.BigInt);
                        cmdCriteria.Parameters["@OrderID"].Value = ((myList)oCriteria.Referrals[i]).Index;
                        //Item(i).TestID

                        cmdCriteria.Parameters.Add("@OrderType", SqlDbType.BigInt);
                        cmdCriteria.Parameters["@OrderType"].Value = TemplateCategoryID.Referrals;

                        cmdCriteria.ExecuteNonQuery();
                        cmdCriteria.Parameters.Clear();
                    }

                    cmdCriteria.Parameters.Clear();
                    cmdCriteria.CommandText = "DM_InsTemplates_DTL";

                    for (int i = 1; i <= oCriteria.Guidelines.Count; i++)
                    {
                        cmdCriteria.Parameters.Add("@Criteria_ID", SqlDbType.BigInt);
                        cmdCriteria.Parameters["@Criteria_ID"].Value = oModifyCriteriaID;

                        cmdCriteria.Parameters.Add("@OrderID", SqlDbType.BigInt);
                        cmdCriteria.Parameters["@OrderID"].Value = ((myList)oCriteria.Guidelines[i]).Index;
                        //Item(i).TestID

                        cmdCriteria.Parameters.Add("@OrderType", SqlDbType.BigInt);
                        cmdCriteria.Parameters["@OrderType"].Value = TemplateCategoryID.Guidelines;

                        cmdCriteria.ExecuteNonQuery();
                        cmdCriteria.Parameters.Clear();
                    }

                    cmdCriteria.Parameters.Clear();
                    cmdCriteria.CommandText = "DM_InsTemplates_DTL";

                    for (int i = 1; i <= oCriteria.RxDrugs.Count; i++)
                    {
                        cmdCriteria.Parameters.Add("@Criteria_ID", SqlDbType.BigInt);
                        cmdCriteria.Parameters["@Criteria_ID"].Value = oModifyCriteriaID;

                        cmdCriteria.Parameters.Add("@OrderID", SqlDbType.BigInt);
                        cmdCriteria.Parameters["@OrderID"].Value = ((myList)oCriteria.RxDrugs[i]).Index;
                        //Item(i).TestID

                        cmdCriteria.Parameters.Add("@OrderType", SqlDbType.BigInt);
                        cmdCriteria.Parameters["@OrderType"].Value = TemplateCategoryID.Rx;

                        cmdCriteria.ExecuteNonQuery();
                        cmdCriteria.Parameters.Clear();
                    }

                    ///'insert LabModule 
                    cmdCriteria.Parameters.Clear();
                    cmdCriteria.CommandText = "DM_InsLabTestDTL";

                    for (int i = 1; i <= oCriteria.LabModuleTests.Count; i++)
                    {

                        cmdCriteria.Parameters.Add("@dm_labdtl_ID", SqlDbType.BigInt);
                        cmdCriteria.Parameters["@dm_labdtl_ID"].Value = oModifyCriteriaID;

                        cmdCriteria.Parameters.Add("@dm_labdtl_TestID", SqlDbType.BigInt);
                        cmdCriteria.Parameters["@dm_labdtl_TestID"].Value = oCriteria.LabModuleTests[i].TestID;

                        cmdCriteria.Parameters.Add("@dm_labdtl_ResultID", SqlDbType.BigInt);
                        cmdCriteria.Parameters["@dm_labdtl_ResultID"].Value = oCriteria.LabModuleTests[i].ResultID;

                        cmdCriteria.Parameters.Add("@dm_labdtl_Operator", SqlDbType.VarChar);
                        cmdCriteria.Parameters["@dm_labdtl_Operator"].Value = oCriteria.LabModuleTests[i].Operators + "";

                        cmdCriteria.Parameters.Add("@dm_labdtl_ResultValue1", SqlDbType.VarChar);
                        cmdCriteria.Parameters["@dm_labdtl_ResultValue1"].Value = oCriteria.LabModuleTests[i].ResultValue1 + "";

                        cmdCriteria.Parameters.Add("@dm_labdtl_ResultValue2", SqlDbType.VarChar);
                        cmdCriteria.Parameters["@dm_labdtl_ResultValue2"].Value = oCriteria.LabModuleTests[i].ResultValue2 + "";

                        cmdCriteria.ExecuteNonQuery();
                        cmdCriteria.Parameters.Clear();
                    }


                    //insert into the fourth detail table DM_Labs_DTL
                    cmdCriteria.Parameters.Clear();
                    cmdCriteria.CommandText = "DM_InsLabsDTL";

                    for (int i = 1; i <= oCriteria.Labs.Count; i++)
                    {
                        cmdCriteria.Parameters.Add("@dm_Labsdtl_Id", SqlDbType.BigInt);
                        cmdCriteria.Parameters["@dm_Labsdtl_Id"].Value = oModifyCriteriaID;
                        cmdCriteria.Parameters.Add("@dm_Labsdtl_GroupID", SqlDbType.BigInt);
                        cmdCriteria.Parameters["@dm_Labsdtl_GroupID"].Value = oCriteria.Labs[i].GroupID;
                        cmdCriteria.Parameters.Add("@dm_Labsdtl_TestID", SqlDbType.BigInt);
                        cmdCriteria.Parameters["@dm_Labsdtl_TestID"].Value = oCriteria.Labs[i].TestID;
                        cmdCriteria.Parameters.Add("@dm_Labsdtl_NumericResultMin", SqlDbType.Decimal);
                        cmdCriteria.Parameters["@dm_Labsdtl_NumericResultMin"].Value = oCriteria.Labs[i].NumericMinimumResult;
                        cmdCriteria.Parameters.Add("@dm_Labsdtl_NumericResultMax", SqlDbType.Decimal);
                        cmdCriteria.Parameters["@dm_Labsdtl_NumericResultMax"].Value = oCriteria.Labs[i].NumericMaximumResult;

                        cmdCriteria.ExecuteNonQuery();
                        cmdCriteria.Parameters.Clear();
                    }

                    myTrans.Commit();

                    gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.DiseaseManagement, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Add, "Criteria  modified. ", gloAuditTrail.ActivityOutCome.Success);

                    //'gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DiseaseManagement, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Add, "Criteria modified.", gloAuditTrail.ActivityOutCome.Success)
                    //'Added Rahul P on 20101009
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DisclosureManagement, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Add, "Criteria modified.", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);
                    //'
                    return oModifyCriteriaID;

                }
                catch (Exception ex)
                {
                    //if some error occur when deleting from any of the table then all the transactions are rollbacked
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.DiseaseManagement, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                    //if some error occur when inserting in any of the tables then all the transactions are rollbacked
                    try
                    {
                        myTrans.Rollback();
                    }
                    catch //(SqlException ex1)
                    {
                        if ((myTrans.Connection != null))
                        {
                            //Console.WriteLine("An exception of type " & ex1.GetType().ToString() & _
                            //" was encountered while attempting to roll back the transaction.")
                            //UpdateLog("clsDiseaseManagement -- Modify(2) -- " + ex.ToString());
                            _ErrorMessage = ex.Message;
                        }
                    }
                    _ErrorMessage = ex.Message;
                    _ErrorMessage = "Neither record was modified.";
                    //UpdateLog("clsDiseaseManagement -- Modify(2) -- " + ex.ToString());
                }
                finally
                {
                    conn.Close();
                    conn.Dispose();
                    conn = null;
                    if (cmdCriteria != null)
                    {
                        cmdCriteria.Parameters.Clear();
                        cmdCriteria.Dispose();
                        cmdCriteria = null;
                    }
                    if (objparam != null)
                    {
                        objparam = null;
                    }
                }
                return oModifyCriteriaID;
            }

            public bool Delete(string oCriteriaName)
            {
                //get the CriteriaID for the Criteria name passed
                SqlConnection conn = new SqlConnection(clsGeneral.EMRConnectionString);
                long ID = 0;
                SqlCommand cmd = new SqlCommand();
                bool IsDelete = false;
                try
                {
                    conn.Open();

                    var _with16 = cmd;
                    _with16.Connection = conn;
                    _with16.CommandType = CommandType.Text;
                    _with16.CommandText = "SELECT dm_mst_Id FROM DM_Criteria_MST where dm_mst_CriteriaName = '" + oCriteriaName + "'";

                    ID = Convert.ToInt64(cmd.ExecuteScalar());
                    Delete(ID, oCriteriaName);

                    conn.Close();
                    conn.Dispose();
                    conn = null;
                    IsDelete = true;
                }
                catch //(Exception ex)
                {
                    //UpdateLog("clsDiseaseManagement -- Delete(1) -- " + ex.ToString());
                    //MessageBox.Show(ex.ToString(),clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (cmd != null)
                    {
                        cmd.Parameters.Clear();
                        cmd.Dispose();
                        cmd = null;
                    }
                }
                return IsDelete;
            }

            public bool Delete(long oCriteriaID, string oCriteriaName)
            {
                SqlConnection conn = new SqlConnection(clsGeneral.EMRConnectionString);
                SqlTransaction myTrans = null;
                SqlCommand cmdCriteria = null;

                try
                {
                    conn.Open();

                    myTrans = conn.BeginTransaction();
                    cmdCriteria = conn.CreateCommand();
                    cmdCriteria.Transaction = myTrans;

                    //first delete from all the detail tables 
                    //detail tables are : DM_CriteriaDrug_DTL ,DM_CriteriaHistory_DTL,DM_ICD9CPT_DTL and DM_Labs_DTL

                    //deleting first from DM_CriteriaDrug_DTL table
                    var _with17 = cmdCriteria;
                    _with17.Connection = conn;
                    _with17.CommandType = CommandType.Text;
                    _with17.CommandText = "delete from DM_CriteriaDrug_DTL where dm_Drugdtl_Id =" + oCriteriaID;
                    cmdCriteria.ExecuteNonQuery();

                    //deleting from DM_CriteriaHistory_DTL table
                    cmdCriteria.CommandText = "delete from DM_CriteriaHistory_DTL where  dm_Chdtl_Id =" + oCriteriaID;
                    cmdCriteria.ExecuteNonQuery();

                    //deleting from DM_ICD9CPT_DTL table
                    cmdCriteria.CommandText = "delete from DM_ICD9CPT_DTL where  dm_ICD9CPTdtl_Id =" + oCriteriaID;
                    cmdCriteria.ExecuteNonQuery();

                    //deleting from DM_Labs_DTL table 

                    cmdCriteria.CommandText = "delete from DM_Labs_DTL where  dm_Labsdtl_Id =" + oCriteriaID;
                    cmdCriteria.ExecuteNonQuery();

                    //deleting from the Master table DM_Criteria_MST

                    cmdCriteria.CommandText = "delete from DM_Criteria_MST where  dm_mst_Id =" + oCriteriaID;
                    cmdCriteria.ExecuteNonQuery();

                    myTrans.Commit();
                    gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.DiseaseManagement, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Delete, "Criteria  deleted. ", gloAuditTrail.ActivityOutCome.Success);

                    //'gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DiseaseManagement, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Delete, "Criteria deleted.", gloAuditTrail.ActivityOutCome.Success)
                    //'Added Rahul P on 20101009
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DisclosureManagement, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Delete, "Criteria deleted.", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);
                    //'

                    return true;

                }
                catch (Exception ex)
                {
                    //if some error occur when deleting from any of the table then all the transactions are rollbacked
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.DiseaseManagement, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Delete, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                    try
                    {
                        myTrans.Rollback();
                    }
                    catch //(SqlException ex1)
                    {
                        if ((myTrans.Connection != null))
                        {
                            //Console.WriteLine("An exception of type " & ex1.GetType().ToString() & _
                            //" was encountered while attempting to roll back the transaction.")
                            //UpdateLog("clsDiseaseManagement -- Delete(2) -- " + ex.ToString());
                            _ErrorMessage = ex.Message;
                        }
                    }
                    //UpdateLog("clsDiseaseManagement -- Delete(2) -- " + ex.ToString());
                    _ErrorMessage = "Neither record was deleted from the database.";
                    _ErrorMessage = ex.Message;
                }
                finally
                {
                    conn.Close();
                    conn.Dispose();
                    conn = null;
                    if (cmdCriteria != null)
                    {
                        cmdCriteria.Parameters.Clear();
                        cmdCriteria.Dispose();
                        cmdCriteria = null;
                    }
                }
                return true;
            }

            //Public Function GetCriteria(ByVal oCriteriaName As String) As gloStream.DiseaseManagement.Supporting.Criteria
            //    Dim conn As New SqlConnection(GetConnectionString)
            //    Dim ID As Long
            //    Dim cmd As New SqlCommand

            //    Try
            //        conn.Open()

            //        With cmd
            //            .Connection = conn
            //            .CommandType = CommandType.Text
            //            .CommandText = "SELECT dm_mst_Id FROM DM_Criteria_MST where dm_mst_CriteriaName = '" & oCriteriaName & "'"
            //        End With

            //        ID = cmd.ExecuteScalar
            //        GetCriteria(ID)

            //        conn.Close()
            //    Catch ex As SqlException
            //        MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            //        UpdateLog("clsDiseaseManagement -- GetCriteria(1) -- " & ex.ToString)
            //    Catch ex As Exception
            //        UpdateLog("clsDiseaseManagement -- GetCriteria(1) -- " & ex.ToString)
            //        MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            //    End Try
            //End Function

            //Public Function GetCriteria(ByVal oCriteriaID As Long, ByVal PatientID As Int64) As gloStream.DiseaseManagement.Supporting.Criteria
            //    Dim _strSQL As String = ""
            //    Dim oCriteria As New gloStream.DiseaseManagement.Supporting.Criteria
            //    Dim oDB As New gloStream.gloDataBase.gloDataBase
            //    Dim oDataReader As SqlClient.SqlDataReader
            //    Dim _FillDetail As Boolean = False
            //    Try
            //        If Not oCriteriaID = 0 Then
            //            'Criteria Master Record
            //            _strSQL = "SELECT * FROM DM_Criteria_MST WHERE dm_mst_Id = " & oCriteriaID & " AND dm_mst_CriteriaName IS NOT NULL"
            //            oDB.Connect(GetConnectionString)
            //            oDataReader = oDB.ReadQueryRecords(_strSQL)
            //            If Not oDataReader Is Nothing Then
            //                If oDataReader.HasRows = True Then
            //                    _FillDetail = True
            //                    While oDataReader.Read
            //                        With oCriteria
            //                            .Name = oDataReader.Item("dm_mst_CriteriaName") & ""
            //                            If Not IsDBNull(oDataReader.Item("dm_mst_AgeMin")) Then
            //                                .AgeMinimum = oDataReader.Item("dm_mst_AgeMin") & ""
            //                            End If
            //                            If Not IsDBNull(oDataReader.Item("dm_mst_AgeMax")) Then
            //                                .AgeMaximum = oDataReader.Item("dm_mst_AgeMax") & ""
            //                            End If
            //                            If Not IsDBNull(oDataReader.Item("dm_mst_Gender")) Then
            //                                .Gender = oDataReader.Item("dm_mst_Gender") & ""
            //                            End If
            //                            If Not IsDBNull(oDataReader.Item("dm_mst_Race")) Then
            //                                .Race = oDataReader.Item("dm_mst_Race") & ""
            //                            End If
            //                            If Not IsDBNull(oDataReader.Item("dm_mst_MaritalStatus")) Then
            //                                .MaritalStatus = oDataReader.Item("dm_mst_MaritalStatus") & ""
            //                            End If
            //                            If Not IsDBNull(oDataReader.Item("dm_mst_City")) Then
            //                                .City = oDataReader.Item("dm_mst_City") & ""
            //                            End If
            //                            If Not IsDBNull(oDataReader.Item("dm_mst_Status")) Then
            //                                .State = oDataReader.Item("dm_mst_Status") & ""
            //                            End If
            //                            If Not IsDBNull(oDataReader.Item("dm_mst_Zip")) Then
            //                                .Zip = oDataReader.Item("dm_mst_Zip") & ""
            //                            End If
            //                            If Not IsDBNull(oDataReader.Item("dm_mst_EmplyementStatus")) Then
            //                                .EmployeeStatus = oDataReader.Item("dm_mst_EmplyementStatus") & ""
            //                            End If
            //                            If Not IsDBNull(oDataReader.Item("dm_mst_HeightMin")) Then
            //                                .HeightMinimum = oDataReader.Item("dm_mst_HeightMin") & ""
            //                            End If
            //                            If Not IsDBNull(oDataReader.Item("dm_mst_HeightMax")) Then
            //                                .HeightMaximum = oDataReader.Item("dm_mst_HeightMax") & ""
            //                            End If
            //                            If Not IsDBNull(oDataReader.Item("dm_mst_WeightMin")) Then
            //                                .WeightMinimum = oDataReader.Item("dm_mst_WeightMin") & ""
            //                            End If
            //                            If Not IsDBNull(oDataReader.Item("dm_mst_WeightMax")) Then
            //                                .WeightMaximum = oDataReader.Item("dm_mst_WeightMax") & ""
            //                            End If
            //                            If Not IsDBNull(oDataReader.Item("dm_mst_BMIMin")) Then
            //                                .BMIMinimum = oDataReader.Item("dm_mst_BMIMin") & ""
            //                            End If
            //                            If Not IsDBNull(oDataReader.Item("dm_mst_BMIMax")) Then
            //                                .BMIMaximum = oDataReader.Item("dm_mst_BMIMax") & ""
            //                            End If
            //                            If Not IsDBNull(oDataReader.Item("dm_mst_TemperatureMin")) Then
            //                                .TempratureMinumum = oDataReader.Item("dm_mst_TemperatureMin") & ""
            //                            End If
            //                            If Not IsDBNull(oDataReader.Item("dm_mst_TemperatureMax")) Then
            //                                .TempratureMaximum = oDataReader.Item("dm_mst_TemperatureMax") & ""
            //                            End If
            //                            If Not IsDBNull(oDataReader.Item("dm_mst_PulseMin")) Then
            //                                .PulseMinimum = oDataReader.Item("dm_mst_PulseMin") & ""
            //                            End If
            //                            If Not IsDBNull(oDataReader.Item("dm_mst_PulseMax")) Then
            //                                .PulseMaximum = oDataReader.Item("dm_mst_PulseMax") & ""
            //                            End If
            //                            If Not IsDBNull(oDataReader.Item("dm_mst_PulseOxMin")) Then
            //                                .PulseOXMinimum = oDataReader.Item("dm_mst_PulseOxMin") & ""
            //                            End If
            //                            If Not IsDBNull(oDataReader.Item("dm_mst_PulseOxMax")) Then
            //                                .PulseOXMaximum = oDataReader.Item("dm_mst_PulseOxMax") & ""
            //                            End If
            //                            If Not IsDBNull(oDataReader.Item("dm_mst_BPSittingMin")) Then
            //                                .BPSittingMinimum = oDataReader.Item("dm_mst_BPSittingMin") & ""
            //                            End If
            //                            If Not IsDBNull(oDataReader.Item("dm_mst_BPSittingMax")) Then
            //                                .BPSittingMaximum = oDataReader.Item("dm_mst_BPSittingMax") & ""
            //                            End If
            //                            If Not IsDBNull(oDataReader.Item("dm_mst_BPStandingMin")) Then
            //                                .BPStandingMinimum = oDataReader.Item("dm_mst_BPStandingMin") & ""
            //                            End If
            //                            If Not IsDBNull(oDataReader.Item("dm_mst_BPStandingMax")) Then
            //                                .BPStandingMaximum = oDataReader.Item("dm_mst_BPStandingMax") & ""
            //                            End If
            //                            If Not IsDBNull(oDataReader.Item("dm_mst_DisplayMessage")) Then
            //                                .DisplayMessage = oDataReader.Item("dm_mst_DisplayMessage") & ""
            //                            End If

            //                        End With
            //                    End While
            //                End If
            //                oDataReader.Close()
            //            End If

            //            'Drug(Details)
            //            _strSQL = "SELECT * FROM  DM_CriteriaDrug_DTL where dm_Drugdtl_Id =" & oCriteriaID

            //            'oCriteriaID()
            //            oDataReader = oDB.ReadQueryRecords(_strSQL)
            //            If Not oDataReader Is Nothing Then
            //                If oDataReader.HasRows = True Then
            //                    While oDataReader.Read
            //                        With oCriteria
            //                            If Not IsDBNull(oDataReader.Item("dm_Drugdtl_DrugID")) Then
            //                                .Drugs.Add(oDataReader.Item("dm_Drugdtl_DrugID"))
            //                            End If
            //                        End With
            //                    End While
            //                End If
            //                oDataReader.Close()
            //            End If



            //            'History Details
            //            _strSQL = "SELECT  ISNULL(dm_Chdtl_HistoryTypeId,0) AS dm_Chdtl_HistoryTypeId , ISNULL(dm_Chdtl_HistoryItemId,0) AS dm_Chdtl_HistoryItemId ,ISNULL(dm_Chdtl_HistoryItem,'') AS dm_Chdtl_HistoryItem, ISNULL(dm_Chdtl_HistoryCategory,'') AS dm_Chdtl_HistoryCategory FROM DM_CriteriaHistory_DTL  where dm_Chdtl_Id =" & oCriteriaID
            //            oDataReader = oDB.ReadQueryRecords(_strSQL)
            //            If Not oDataReader Is Nothing Then
            //                If oDataReader.HasRows = True Then
            //                    While oDataReader.Read
            //                        With oCriteria
            //                            If Not IsDBNull(oDataReader.Item("dm_Chdtl_HistoryItemId")) Then
            //                                Dim objNewMember As New gloStream.DiseaseManagement.Supporting.HistoryItem
            //                                objNewMember.ID = oDataReader.Item("dm_Chdtl_HistoryItemId")
            //                                objNewMember.CategoryID = oDataReader.Item("dm_Chdtl_HistoryTypeId")
            //                                '' Sudhir 20090302 ''
            //                                objNewMember.Name = oDataReader.Item("dm_Chdtl_HistoryItem")
            //                                objNewMember.CategoryName = oDataReader.Item("dm_Chdtl_HistoryCategory")
            //                                ''
            //                                .Histories.Add(objNewMember)
            //                            End If
            //                        End With
            //                    End While
            //                End If
            //                oDataReader.Close()
            //            End If

            //            'ICD9 Details
            //            _strSQL = "SELECT *  FROM DM_ICD9CPT_DTL where dm_ICD9CPTdtl_Id =" & oCriteriaID & " and dm_ICD9CPTdtl_Type =" & nICDTypeID

            //            oDataReader = oDB.ReadQueryRecords(_strSQL)

            //            If Not oDataReader Is Nothing Then
            //                If oDataReader.HasRows = True Then
            //                    While oDataReader.Read
            //                        With oCriteria
            //                            If Not IsDBNull(oDataReader.Item("dm_ICD9CPTdtl_ICID")) Then
            //                                .ICD9s.Add(oDataReader.Item("dm_ICD9CPTdtl_ICID"))
            //                            End If
            //                        End With
            //                    End While
            //                End If
            //                oDataReader.Close()
            //            End If

            //            'CPT Details
            //            _strSQL = "SELECT *  FROM DM_ICD9CPT_DTL  where dm_ICD9CPTdtl_Id =" & oCriteriaID & " and dm_ICD9CPTdtl_Type =" & nCPTTypeID

            //            oDataReader = oDB.ReadQueryRecords(_strSQL)
            //            If Not oDataReader Is Nothing Then
            //                If oDataReader.HasRows = True Then
            //                    While oDataReader.Read
            //                        With oCriteria
            //                            If Not IsDBNull(oDataReader.Item("dm_ICD9CPTdtl_ICID")) Then
            //                                .CPTs.Add(oDataReader.Item("dm_ICD9CPTdtl_ICID"))
            //                            End If
            //                        End With
            //                    End While
            //                End If
            //                oDataReader.Close()
            //            End If


            //            '_strSQL = "SELECT *  FROM DM_Templates_DTL  where dm_Templatedtl_Id =" & oCriteriaID & " "

            //            'oDataReader = oDB.ReadQueryRecords(_strSQL)
            //            'If Not oDataReader Is Nothing Then
            //            '    If oDataReader.HasRows = True Then
            //            '        While oDataReader.Read
            //            '            With oCriteria
            //            '                If Not IsDBNull(oDataReader.Item("dm_Templatedtl_TemplateID")) Then
            //            '                    .Guidelines.Add(oDataReader.Item("dm_Templatedtl_TemplateID"))
            //            '                End If
            //            '            End With
            //            '        End While
            //            '    End If
            //            '    oDataReader.Close()
            //            'End If

            //            'Lab Test Result Details
            //            _strSQL = "SELECT *  FROM DM_LabModule_DTL  where dm_labdtl_ID =" & oCriteriaID & " "


            //            oDataReader = oDB.ReadQueryRecords(_strSQL)

            //            If Not oDataReader Is Nothing Then
            //                If oDataReader.HasRows = True Then
            //                    While oDataReader.Read
            //                        With oCriteria
            //                            If Not (IsDBNull(oDataReader.Item("dm_labdtl_TestID")) And IsDBNull(oDataReader.Item("dm_labdtl_ResultID")) And IsDBNull(oDataReader.Item("dm_labdtl_Operator")) And IsDBNull(oDataReader.Item("dm_labdtl_ResultValue1")) And IsDBNull(oDataReader.Item("dm_labdtl_ResultValue2"))) Then
            //                                .LabModuleTests.Add(oDataReader.Item("dm_labdtl_TestID"), oDataReader.Item("dm_labdtl_ResultID"), "", Supporting.enumTestModuleResultValueType.None, 0, oDataReader.Item("dm_labdtl_Operator"), oDataReader.Item("dm_labdtl_ResultValue1"), oDataReader.Item("dm_labdtl_ResultValue2"))
            //                            End If
            //                        End With
            //                    End While
            //                End If
            //                oDataReader.Close()
            //            End If

            //            'RadiologyLab Details
            //            _strSQL = "SELECT *  FROM DM_Labs_DTL  where dm_Labsdtl_Id =" & oCriteriaID


            //            oDataReader = oDB.ReadQueryRecords(_strSQL)

            //            If Not oDataReader Is Nothing Then
            //                If oDataReader.HasRows = True Then
            //                    While oDataReader.Read
            //                        With oCriteria
            //                            If Not (IsDBNull(oDataReader.Item("dm_Labsdtl_GroupID")) And IsDBNull(oDataReader.Item("dm_Labsdtl_TestID")) And IsDBNull(oDataReader.Item("dm_Labsdtl_NumericResultMin")) And IsDBNull(oDataReader.Item("dm_Labsdtl_NumericResultMax"))) Then
            //                                .Labs.Add(oDataReader.Item("dm_Labsdtl_GroupID"), oDataReader.Item("dm_Labsdtl_TestID"), oDataReader.Item("dm_Labsdtl_NumericResultMin"), oDataReader.Item("dm_Labsdtl_NumericResultMax"))

            //                            End If
            //                        End With
            //                    End While
            //                End If
            //                oDataReader.Close()
            //            End If

            //            'RadiologyLab Details
            //            '_strSQL = "SELECT Lab_Order_Test_ResultDtl.labotrd_ResultName, LM_Test.lm_test_Name, Drugs_MST.sDrugName, " _
            //            '          & " TemplateGallery_MST.sTemplateName, DM_Templates_DTL.dm_Templatedtl_Id ,  " _
            //            '          & " DM_Templates_DTL.dm_Templatedtl_TemplateID, DM_Templates_DTL.dm_Templatedtl_CategoryID " _
            //            '          & " FROM DM_Templates_DTL INNER JOIN Lab_Order_Test_ResultDtl ON DM_Templates_DTL.dm_Templatedtl_TemplateID = Lab_Order_Test_ResultDtl.labotrd_ResultName INNER JOIN " _
            //            '          & " TemplateGallery_MST ON DM_Templates_DTL.dm_Templatedtl_TemplateID = TemplateGallery_MST.sTemplateName INNER JOIN " _
            //            '          & " LM_Test ON DM_Templates_DTL.dm_Templatedtl_TemplateID = LM_Test.lm_test_Name INNER JOIN " _
            //            '          & " Drugs_MST ON DM_Templates_DTL.dm_Templatedtl_TemplateID = Drugs_MST.sDrugName where dm_Templatedtl_Id = '" & oCriteriaID & "'" ' " & oCriteriaID

            //            '' Commected on 20090307 - 
            //            '' To get criteria from DM_templaet_DTL
            //            '_strSQL = "SELECT dm_Templatedtl_Id, dm_Templatedtl_TemplateID, dm_Templatedtl_CategoryID  FROM DM_Templates_DTL  where dm_Templatedtl_Id =" & oCriteriaID
            //            'oDataReader = oDB.ReadQueryRecords(_strSQL)

            //            '' Added on 20090307
            //            ''To Get The Criteria for Patient (if exisits) or from the DM General Criteri
            //            oDB.DBParameters.Clear()
            //            oDB.DBParameters.Add("@Criteria_ID", oCriteriaID, ParameterDirection.Input, SqlDbType.BigInt)
            //            oDB.DBParameters.Add("@PatientID", PatientID, ParameterDirection.Input, SqlDbType.BigInt)
            //            oDataReader = oDB.ReadRecords("DM_SELECT_Patient_Criteria_MST")

            //            If Not oDataReader Is Nothing Then
            //                If oDataReader.HasRows = True Then
            //                    While oDataReader.Read
            //                        With oCriteria
            //                            If Not (IsDBNull(oDataReader.Item("CriteriaID")) And IsDBNull(oDataReader.Item("TemplateID")) And IsDBNull(oDataReader.Item("CategoryID"))) Then
            //                                If oDataReader.Item("CategoryID") = TemplateCategoryID.Labs Then
            //                                    .LabOrders.Add(oDataReader.Item("TemplateID"))
            //                                ElseIf oDataReader.Item("CategoryID") = TemplateCategoryID.Radiology Then
            //                                    .RadiologyOrders.Add(oDataReader.Item("TemplateID"))
            //                                ElseIf oDataReader.Item("CategoryID") = TemplateCategoryID.Referrals Then
            //                                    .Referrals.Add(oDataReader.Item("TemplateID"))
            //                                ElseIf oDataReader.Item("CategoryID") = TemplateCategoryID.Guidelines Then
            //                                    .Guidelines.Add(oDataReader.Item("TemplateID"))
            //                                ElseIf oDataReader.Item("CategoryID") = TemplateCategoryID.Rx Then
            //                                    .RxDrugs.Add(oDataReader.Item("TemplateID"))
            //                                End If
            //                            End If
            //                        End With
            //                    End While
            //                End If
            //                oDataReader.Close()
            //            End If
            //            oDB.Disconnect()

            //            'Return Object
            //            oCriteria.ID = oCriteriaID
            //            Return oCriteria

            //        Else
            //            _ErrorMessage = "Please select Criteria"
            //        End If
            //    Catch ex As SqlException
            //        MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            //        UpdateLog("clsDiseaseManagement -- GetCriteria(2) -- " & ex.ToString)
            //    Catch ex As Exception
            //        UpdateLog("clsDiseaseManagement -- GetCriteria(2) -- " & ex.ToString)
            //        MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            //    End Try
            //End Function

            public gloStream.DiseaseManagement.Supporting.Criteria GetCriteria(long oCriteriaID, Int64 PatientID)
            {
                string _strSQL = "";
                gloStream.DiseaseManagement.Supporting.Criteria oCriteria = new gloStream.DiseaseManagement.Supporting.Criteria();
                gloStream.DiseaseManagement.Supporting.OtherDetails oOtherDetails = new gloStream.DiseaseManagement.Supporting.OtherDetails();
                gloStream.DiseaseManagement.Supporting.OtherDetail oOtherDetail = null;
                //gloStream.gloDataBase.gloDataBase oDB = new gloStream.gloDataBase.gloDataBase();
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(clsGeneral.EMRConnectionString);
                System.Data.SqlClient.SqlDataReader oDataReader = null;
             //   bool _FillDetail = false;
                try
                {
                    if (!(oCriteriaID == 0))
                    {
                        //Criteria Master Record
                        _strSQL = "SELECT  dm_mst_CriteriaName, dm_mst_AgeMin, dm_mst_AgeMax, dm_mst_Gender, " + " dm_mst_Race, dm_mst_MaritalStatus, dm_mst_City, dm_mst_Status, dm_mst_Zip, dm_mst_EmplyementStatus," + " dm_mst_HeightMin, dm_mst_HeightMax, dm_mst_WeightMin, dm_mst_WeightMax, dm_mst_BMIMin, dm_mst_BMIMax," + " dm_mst_TemperatureMin, dm_mst_TemperatureMax, dm_mst_PulseMin, dm_mst_PulseMax, dm_mst_PulseOxMin, " + "dm_mst_PulseOxMax, dm_mst_BPSittingMin, dm_mst_BPSittingMax, dm_mst_BPStandingMin, dm_mst_BPStandingMax," + " dm_mst_DisplayMessage " + " FROM DM_Criteria_MST WHERE dm_mst_Id = " + oCriteriaID + " AND dm_mst_CriteriaName IS NOT NULL";
                        oDB.Connect(false);
                        oDB.Retrive_Query(_strSQL,out oDataReader);
                        if ((oDataReader != null))
                        {
                            if (oDataReader.HasRows == true)
                            {
                                //_FillDetail = true;
                                while (oDataReader.Read())
                                {
                                    var _with18 = oCriteria;
                                    double _tempAge = 0;
                                    double _tempAgeYr = 0;
                                    double _tempAgeMnt = 0;

                                    _with18.Name = oDataReader["dm_mst_CriteriaName"] + "";
                                    if (!Information.IsDBNull(oDataReader["dm_mst_AgeMin"]))
                                    {
                                        _tempAge = 0;
                                        _tempAgeYr = 0;
                                        _tempAgeMnt = 0;
                                        _tempAge = Convert.ToDouble(oDataReader["dm_mst_AgeMin"] + "");

                                        string[] _strage = _tempAge.ToString().Split('.');
                                        //   =tempage .Substring(0,_tempAge.ToString()     

                                        if (_strage.Length > 1)
                                        {
                                            _tempAgeYr = Convert.ToDouble(_strage[0].ToString());
                                            _tempAgeMnt = Convert.ToDouble(_strage[1].ToString());
                                        }
                                        else
                                        {
                                            _tempAgeYr = Convert.ToDouble(_strage[0].ToString());
                                            _tempAgeMnt = 0;
                                        }

                                        _with18.AgeMinimum = Convert.ToDouble(Strings.Format(_tempAgeYr + (_tempAgeMnt / 12), "#0.0000"));

                                        //.AgeMinimum = oDataReader.Item("dm_mst_AgeMin") & ""
                                    }

                                    if (!Information.IsDBNull(oDataReader["dm_mst_AgeMax"]))
                                    {
                                        _tempAge = 0;
                                        _tempAgeYr = 0;
                                        _tempAgeMnt = 0;
                                        _tempAge = Convert.ToDouble(oDataReader["dm_mst_AgeMax"] + "");

                                        string[] _strage = _tempAge.ToString().Split('.');
                                        //   =tempage .Substring(0,_tempAge.ToString()     

                                        if (_strage.Length > 1)
                                        {
                                            _tempAgeYr = Convert.ToDouble(_strage[0].ToString());
                                            _tempAgeMnt = Convert.ToDouble(_strage[1].ToString());
                                        }
                                        else
                                        {
                                            _tempAgeYr = Convert.ToDouble(_strage[0].ToString());
                                            _tempAgeMnt = 0;
                                        }

                                        _with18.AgeMaximum = Convert.ToDouble(Strings.Format(_tempAgeYr + (_tempAgeMnt / 12), "#0.0000"));

                                        //.AgeMaximum = oDataReader.Item("dm_mst_AgeMax") & ""
                                    }
                                    if (!Information.IsDBNull(oDataReader["dm_mst_Gender"]))
                                    {
                                        _with18.Gender = oDataReader["dm_mst_Gender"] + "";
                                    }
                                    if (!Information.IsDBNull(oDataReader["dm_mst_Race"]))
                                    {
                                        _with18.Race = oDataReader["dm_mst_Race"] + "";
                                    }
                                    if (!Information.IsDBNull(oDataReader["dm_mst_MaritalStatus"]))
                                    {
                                        _with18.MaritalStatus = oDataReader["dm_mst_MaritalStatus"] + "";
                                    }
                                    if (!Information.IsDBNull(oDataReader["dm_mst_City"]))
                                    {
                                        _with18.City = oDataReader["dm_mst_City"] + "";
                                    }
                                    if (!Information.IsDBNull(oDataReader["dm_mst_Status"]))
                                    {
                                        _with18.State = oDataReader["dm_mst_Status"] + "";
                                    }
                                    if (!Information.IsDBNull(oDataReader["dm_mst_Zip"]))
                                    {
                                        _with18.Zip = oDataReader["dm_mst_Zip"] + "";
                                    }
                                    if (!Information.IsDBNull(oDataReader["dm_mst_EmplyementStatus"]))
                                    {
                                        _with18.EmployeeStatus = oDataReader["dm_mst_EmplyementStatus"] + "";
                                    }
                                    if (!Information.IsDBNull(oDataReader["dm_mst_HeightMin"]))
                                    {
                                        _with18.HeightMinimum = oDataReader["dm_mst_HeightMin"] + "";
                                    }
                                    if (!Information.IsDBNull(oDataReader["dm_mst_HeightMax"]))
                                    {
                                        _with18.HeightMaximum = oDataReader["dm_mst_HeightMax"] + "";
                                    }
                                    if (!Information.IsDBNull(oDataReader["dm_mst_WeightMin"]))
                                    {
                                        _with18.WeightMinimum = Convert.ToDouble(oDataReader["dm_mst_WeightMin"]);// + "";
                                    }
                                    if (!Information.IsDBNull(oDataReader["dm_mst_WeightMax"]))
                                    {
                                        _with18.WeightMaximum = Convert.ToDouble(oDataReader["dm_mst_WeightMax"]);// +"";
                                    }
                                    if (!Information.IsDBNull(oDataReader["dm_mst_BMIMin"]))
                                    {
                                        _with18.BMIMinimum = Convert.ToDouble(oDataReader["dm_mst_BMIMin"]);// +"";
                                    }
                                    if (!Information.IsDBNull(oDataReader["dm_mst_BMIMax"]))
                                    {
                                        _with18.BMIMaximum = Convert.ToDouble(oDataReader["dm_mst_BMIMax"]);// +"";
                                    }
                                    if (!Information.IsDBNull(oDataReader["dm_mst_TemperatureMin"]))
                                    {
                                        _with18.TempratureMinumum = Convert.ToDouble(oDataReader["dm_mst_TemperatureMin"]);// +"";
                                    }
                                    if (!Information.IsDBNull(oDataReader["dm_mst_TemperatureMax"]))
                                    {
                                        _with18.TempratureMaximum = Convert.ToDouble(oDataReader["dm_mst_TemperatureMax"]);// +"";
                                    }
                                    if (!Information.IsDBNull(oDataReader["dm_mst_PulseMin"]))
                                    {
                                        _with18.PulseMinimum = Convert.ToDouble(oDataReader["dm_mst_PulseMin"]);// +"";
                                    }
                                    if (!Information.IsDBNull(oDataReader["dm_mst_PulseMax"]))
                                    {
                                        _with18.PulseMaximum = Convert.ToDouble(oDataReader["dm_mst_PulseMax"]);// +"";
                                    }
                                    if (!Information.IsDBNull(oDataReader["dm_mst_PulseOxMin"]))
                                    {
                                        _with18.PulseOXMinimum = Convert.ToDouble(oDataReader["dm_mst_PulseOxMin"]);// +"";
                                    }
                                    if (!Information.IsDBNull(oDataReader["dm_mst_PulseOxMax"]))
                                    {
                                        _with18.PulseOXMaximum = Convert.ToDouble(oDataReader["dm_mst_PulseOxMax"]);// +"";
                                    }
                                    if (!Information.IsDBNull(oDataReader["dm_mst_BPSittingMin"]))
                                    {
                                        _with18.BPSittingMinimum = Convert.ToDouble(oDataReader["dm_mst_BPSittingMin"]);// +"";
                                    }
                                    if (!Information.IsDBNull(oDataReader["dm_mst_BPSittingMax"]))
                                    {
                                        _with18.BPSittingMaximum = Convert.ToDouble(oDataReader["dm_mst_BPSittingMax"]);// +"";
                                    }
                                    if (!Information.IsDBNull(oDataReader["dm_mst_BPStandingMin"]))
                                    {
                                        _with18.BPStandingMinimum = Convert.ToDouble(oDataReader["dm_mst_BPStandingMin"]);// +"";
                                    }
                                    if (!Information.IsDBNull(oDataReader["dm_mst_BPStandingMax"]))
                                    {
                                        _with18.BPStandingMaximum = Convert.ToDouble(oDataReader["dm_mst_BPStandingMax"]);// +"";
                                    }
                                    if (!Information.IsDBNull(oDataReader["dm_mst_DisplayMessage"]))
                                    {
                                        _with18.DisplayMessage = oDataReader["dm_mst_DisplayMessage"] + "";
                                    }

                                }
                            }
                            oDataReader.Close();
                        }

                        //' FETCH OTHER DETAILS

                        _strSQL = " SELECT ISNULL(dm_mst_Id,0) AS dm_mst_Id, ISNULL(dm_dtl_Id,0) AS dm_dtl_Id, " + " ISNULL(dm_dtl_CategoryName,'') AS dm_dtl_CategoryName, ISNULL(dm_dtl_ItemName,'') AS dm_dtl_ItemName, " + " ISNULL(dm_dtl_Operator,'') AS dm_dtl_Operator, ISNULL(dm_dtl_ResultValue1,'') AS dm_dtl_ResultValue1, " + " ISNULL(dm_dtl_ResultValue2,'') AS dm_dtl_ResultValue2, ISNULL(dm_dtl_Type,0) AS dm_dtl_Type " + " FROM DM_Criteria_DTL WHERE dm_mst_Id = " + oCriteriaID + "";
                        oDB.Retrive_Query(_strSQL,out oDataReader);
                        if ((oDataReader != null))
                        {
                            if (oDataReader.HasRows == true)
                            {
                                while (oDataReader.Read())
                                {
                                    oOtherDetail = new Supporting.OtherDetail();
                                    oOtherDetail.ItemName = oDataReader["dm_dtl_ItemName"].ToString();
                                    oOtherDetail.CategoryName = oDataReader["dm_dtl_CategoryName"].ToString();
                                    oOtherDetail.OperatorName = oDataReader["dm_dtl_Operator"].ToString();
                                    oOtherDetail.Result1 = oDataReader["dm_dtl_ResultValue1"].ToString();
                                    oOtherDetail.Result2 = oDataReader["dm_dtl_ResultValue2"].ToString();
                                    oOtherDetail.DetailType = (gloStream.DiseaseManagement.Supporting.enumDetailType)oDataReader["dm_dtl_Type"];
                                    oOtherDetails.Add(oOtherDetail);
                                    oOtherDetail = null;
                                }
                            }
                            oDataReader.Close();
                        }

                        //' BIND OTHER DETAILS TO CRITERIA
                        oCriteria.OtherDetails = oOtherDetails;

                        //' END OTHER DETAILS

                        //' Added on 20090307
                        //'To Get The Criteria for Patient (if exisits) or from the DM General Criteri
                        gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
                        oParameters.Clear();
                        oParameters.Add("@Criteria_ID", oCriteriaID, ParameterDirection.Input, SqlDbType.BigInt);
                        oParameters.Add("@PatientID", PatientID, ParameterDirection.Input, SqlDbType.BigInt);

                        oDB.Connect(false);
                        oDB.Retrive("DM_SELECT_Patient_Criteria_MST",oParameters,out oDataReader);
                        myList objList = default(myList);

                        if ((oDataReader != null))
                        {
                            if (oDataReader.HasRows == true)
                            {
                                while (oDataReader.Read())
                                {
                                    var _with19 = oCriteria;
                                    if (!(Information.IsDBNull(oDataReader["CriteriaID"]) & Information.IsDBNull(oDataReader["TemplateID"]) & Information.IsDBNull(oDataReader["CategoryID"])))
                                    {
                                        if (Convert.ToInt32(oDataReader["CategoryID"]) == Convert.ToInt32(TemplateCategoryID.Labs))
                                        {
                                            //sarika DM Denormalization 20090404
                                            //.LabOrders.Add(oDataReader.Item("TemplateID"))

                                            objList = new myList();
                                            objList.ID = Convert.ToInt64(oDataReader["TemplateID"]);
                                            objList.Index = Convert.ToInt64(oDataReader["TemplateID"]);
                                            objList.Value = oDataReader["TemplateName"].ToString();
                                            objList.DMTemplateName = oDataReader["TemplateName"].ToString();
                                            _with19.LabOrders.Add(objList);

                                            objList = null;
                                            //----

                                        }
                                        else if (Convert.ToInt32(oDataReader["CategoryID"]) == Convert.ToInt32(TemplateCategoryID.Radiology))
                                        {
                                            //sarika DM Denormalization 
                                            //.RadiologyOrders.Add(oDataReader.Item("TemplateID"))

                                            objList = new myList();
                                            objList.ID = Convert.ToInt64(oDataReader["TemplateID"]);
                                            objList.Index = Convert.ToInt64(oDataReader["TemplateID"]);
                                            objList.Value = oDataReader["TemplateName"].ToString();
                                            objList.DMTemplateName = oDataReader["TemplateName"].ToString();
                                            _with19.RadiologyOrders.Add(objList);

                                            objList = null;
                                            //----


                                        }
                                        else if (Convert.ToInt32(oDataReader["CategoryID"]) == Convert.ToInt32(TemplateCategoryID.Referrals))
                                        {

                                            //sarika DM Denormalization 
                                            // .Referrals.Add(oDataReader.Item("TemplateID"))

                                            objList = new myList();
                                            objList.ID = Convert.ToInt64(oDataReader["TemplateID"]);
                                            objList.Index = Convert.ToInt64(oDataReader["TemplateID"]);
                                            objList.Value = oDataReader["TemplateName"].ToString();
                                            objList.DMTemplateName = oDataReader["TemplateName"].ToString();
                                            _with19.Referrals.Add(objList);

                                            objList = null;
                                            //----


                                        }
                                        else if (Convert.ToInt32(oDataReader["CategoryID"]) == Convert.ToInt32(TemplateCategoryID.Guidelines))
                                        {
                                            //sarika DM Denormalization 20090331
                                            //.Guidelines.Add(oDataReader.Item("TemplateID"))
                                            objList = new myList();
                                            objList.ID = Convert.ToInt64(oDataReader["TemplateID"]);
                                            objList.Index = Convert.ToInt64(oDataReader["TemplateID"]);
                                            objList.DMTemplateName = oDataReader["TemplateName"].ToString();
                                            objList.Value = oDataReader["TemplateName"].ToString();

                                            if (!Information.IsDBNull(oDataReader["Template"]))
                                            {
                                                objList.DMTemplate = oDataReader["Template"];
                                            }
                                            else
                                            {
                                                objList.DMTemplate = null;
                                            }


                                            _with19.Guidelines.Add(objList);

                                            objList = null;
                                            //---
                                        }
                                        else if (Convert.ToInt32(oDataReader["CategoryID"]) == Convert.ToInt32(TemplateCategoryID.Rx))
                                        {
                                            //sarika DM Denormalization 20090404
                                            //.RxDrugs.Add(oDataReader.Item("TemplateID"))

                                            objList = new myList();
                                            objList.ID = Convert.ToInt64(oDataReader["TemplateID"]);
                                            objList.Index = Convert.ToInt64(oDataReader["TemplateID"]);
                                            objList.Value = oDataReader["TemplateName"].ToString() + " " + oDataReader["TemplateDtlInfo"].ToString();
                                            objList.DMTemplateName = oDataReader["TemplateName"].ToString();
                                            objList.DrugName = oDataReader["TemplateName"].ToString();
                                            objList.Dosage = oDataReader["TemplateDtlInfo"].ToString();

                                            //sarika DM Denormalization for Rx 20090410
                                            objList.DrugForm = oDataReader["sDrugForm"].ToString();
                                            objList.Route = oDataReader["sRoute"].ToString();
                                            objList.Duration = oDataReader["sDuration"].ToString();
                                            objList.Frequency = oDataReader["sFrequency"].ToString();
                                            objList.IsNarcotic = Convert.ToInt16(oDataReader["nIsNarcotics"]);

                                            objList.NDCCode = oDataReader["sNDCCode"].ToString();
                                           
                                            objList.DrugQtyQualifier = oDataReader["sDrugQtyQualifier"].ToString();
                                            //---

                                            _with19.RxDrugs.Add(objList);

                                            objList = null;
                                            //----
                                        }
                                        else if (Convert.ToInt32(oDataReader["CategoryID"]) == Convert.ToInt32(TemplateCategoryID.IM))
                                        {
                                            ///''''''Added by Chetan as on 09 Oct 2010 - for IM in DM Setup

                                            objList = new myList();
                                            objList.ID = Convert.ToInt64(oDataReader["TemplateID"]);
                                            //IM ID                                               
                                            objList.Index = Convert.ToInt64(oDataReader["TemplateID"]);
                                            objList.Value = oDataReader["TemplateName"].ToString();
                                            objList.DMTemplateName = oDataReader["TemplateName"].ToString();
                                            objList.DrugForm = oDataReader["sDrugForm"].ToString();
                                            //ConceptID
                                            objList.Duration = oDataReader["sDuration"].ToString();
                                            //DescriptionID
                                            objList.Frequency = oDataReader["sFrequency"].ToString();
                                            //SnoMedID   
                                            objList.DrugQtyQualifier = oDataReader["sDrugQtyQualifier"].ToString();
                                            objList.Route = oDataReader["sRoute"].ToString();
                                            ///' Vaccine orignal name

                                            _with19.IMlst.Add(objList);

                                            objList = null;
                                            //----
                                            ///''''''Added by Chetan as on  09 Oct 2010 - for IM in DM Setup



                                        }
                                    }
                                }
                            }
                            oDataReader.Close();
                        }
                        oDB.Disconnect();
                        oDB.Dispose();
                        //Return Object
                        oCriteria.ID = oCriteriaID;
                        return oCriteria;

                    }
                    else
                    {
                        _ErrorMessage = "Please select Criteria";
                    }
                }
                catch //(SqlException ex)
                {
                    //MessageBox.Show(ex.ToString(), clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //UpdateLog("clsDiseaseManagement -- GetCriteria(2) -- " + ex.ToString());
                }
                //catch (Exception ex)
                //{
                //    //UpdateLog("clsDiseaseManagement -- GetCriteria(2) -- " + ex.ToString());
                //    //MessageBox.Show(ex.ToString(), clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                //}
                return oCriteria;
            }

            public string GetLabResultName(long ResultID)
            {
                //string _strSQL = "";
                //gloStream.DiseaseManagement.Supporting.Criteria oCriteria = new gloStream.DiseaseManagement.Supporting.Criteria();
                //gloStream.gloDataBase.gloDataBase oDB = new gloStream.gloDataBase.gloDataBase();
                //System.Data.SqlClient.SqlDataReader oDataReader = null;
                //oDB.Connect(GetConnectionString());
                //string LabResultName = oDB.ExecuteQueryScaler("select labtrd_ResultName from Lab_Test_ResultDtl where labtrd_TestID = " + ResultID + " ");
                //oDB.Disconnect();
                //oDB.Dispose();
                //oDB = null;
                //return LabResultName;
                return "";
            }
            public string GetLabTestName(long TesttID)
            {
                //string _strSQL = "";
                //gloStream.DiseaseManagement.Supporting.Criteria oCriteria = new gloStream.DiseaseManagement.Supporting.Criteria();
                //gloStream.gloDataBase.gloDataBase oDB = new gloStream.gloDataBase.gloDataBase();
                //oDB.Connect(GetConnectionString());
                ////Dim LabResultName As String = oDB.ExecuteQueryScaler("select labtrd_ResultName from Lab_Test_ResultDtl where labtrd_TestID = " & ResultID & " ")
                //string LabTestName = oDB.ExecuteQueryScaler("select labtm_Name from Lab_Test_Mst where labtm_ID= " + TesttID + " ");
                //oDB.Disconnect();
                //oDB.Dispose();
                //oDB = null;
                //return LabTestName;
                return "";
            }

            public string GetRadiologyOrder(long OrderID)
            {
                //string _strSQL = "";
                //gloStream.DiseaseManagement.Supporting.Criteria oCriteria = new gloStream.DiseaseManagement.Supporting.Criteria();
                //gloStream.gloDataBase.gloDataBase oDB = new gloStream.gloDataBase.gloDataBase();
                //System.Data.SqlClient.SqlDataReader oDataReader = null;
                //oDB.Connect(GetConnectionString());
                //string RadiologyOrderName = oDB.ExecuteQueryScaler("select lm_test_Name from LM_Test where lm_test_ID = " + OrderID + " ");
                //oDB.Disconnect();
                //oDB.Dispose();
                //oDB = null;
                //return RadiologyOrderName;
                return "";
            }
            public string GetRefferalName(long ReferralID)
            {
                //string _strSQL = "";
                //gloStream.DiseaseManagement.Supporting.Criteria oCriteria = new gloStream.DiseaseManagement.Supporting.Criteria();
                //gloStream.gloDataBase.gloDataBase oDB = new gloStream.gloDataBase.gloDataBase();
                //System.Data.SqlClient.SqlDataReader oDataReader = null;
                //oDB.Connect(GetConnectionString());
                //string ReferralName = oDB.ExecuteQueryScaler("select sTemplateName from TemplateGallery_MST where nTemplateID = " + ReferralID + " ");
                //oDB.Disconnect();
                //oDB.Dispose();
                //oDB = null;
                //return ReferralName;
                return "";
            }

            public string GetDrugName(long DrugID)
            {
                //string _strSQL = "";
                //gloStream.DiseaseManagement.Supporting.Criteria oCriteria = new gloStream.DiseaseManagement.Supporting.Criteria();
                //gloStream.gloDataBase.gloDataBase oDB = new gloStream.gloDataBase.gloDataBase();
                //System.Data.SqlClient.SqlDataReader oDataReader = null;
                //oDB.Connect(GetConnectionString());
                //string ReferralName = oDB.ExecuteQueryScaler("select  sDrugName from Drugs_MST where nDrugsID  = " + DrugID + " ");
                //oDB.Disconnect();
                //oDB.Dispose();
                //oDB = null;
                //return ReferralName;
                return "";
            }
            public string GetDrugDosageName(long DrugID)
            {
                //string _strSQL = "";
                //gloStream.DiseaseManagement.Supporting.Criteria oCriteria = new gloStream.DiseaseManagement.Supporting.Criteria();
                //gloStream.gloDataBase.gloDataBase oDB = new gloStream.gloDataBase.gloDataBase();
                //System.Data.SqlClient.SqlDataReader oDataReader = null;
                //oDB.Connect(GetConnectionString());
                //string strSelectQRY = " SELECT sDrugName +space(1) +isnull(sDosage,' ') FROM Drugs_MST where nDrugsID = " + DrugID + "";
                //string ReferralName = oDB.ExecuteQueryScaler(strSelectQRY);
                //oDB.Disconnect();
                //oDB.Dispose();
                //oDB = null;
                //return ReferralName;
                return "";
            }
            public string GetGuidLine(long GuidLineID)
            {
                //string _strSQL = "";
                //gloStream.DiseaseManagement.Supporting.Criteria oCriteria = new gloStream.DiseaseManagement.Supporting.Criteria();
                //gloStream.gloDataBase.gloDataBase oDB = new gloStream.gloDataBase.gloDataBase();
                //System.Data.SqlClient.SqlDataReader oDataReader = null;
                //oDB.Connect(GetConnectionString());
                ////sarika DM Denormalization 20090331

                //string ReferralName = oDB.ExecuteQueryScaler("select sTemplateName from TemplateGallery_MST where nTemplateID = " + GuidLineID + " ");
                ////  Dim ReferralName As String = oDB.ExecuteQueryScaler("select sTemplateName from DM_Templates_DTL where dm_Templatedtl_TemplateID = " & GuidLineID & " ")

                ////---
                //oDB.Disconnect();
                //oDB.Dispose();
                //oDB = null;
                //return ReferralName;
                return "";
            }
            public string GetCriteriaName(long oCriteriaID)
            {
                //string _strSQL = "";
                //string _Result = "";
                //gloStream.gloDataBase.gloDataBase oDB = new gloStream.gloDataBase.gloDataBase();
                //try
                //{
                //    if (!(oCriteriaID == 0))
                //    {
                //        //Criteria Master Record
                //        _strSQL = "SELECT dm_mst_CriteriaName FROM DM_Criteria_MST WHERE dm_mst_Id = " + oCriteriaID + " AND dm_mst_CriteriaName IS NOT NULL";
                //        oDB.Connect(GetConnectionString);
                //        _Result = oDB.ExecuteQueryScaler(_strSQL);
                //        oDB.Disconnect();
                //        oDB.Dispose();
                //        oDB = null;
                //        //Return Object
                //        return _Result;
                //    }
                //    else
                //    {
                //        _ErrorMessage = "Please select Criteria";
                //    }
                //}
                //catch (SqlException ex)
                //{
                //    MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    UpdateLog("clsDiseaseManagement -- GetCriteriaName -- " + ex.ToString());
                //}
                //catch (Exception ex)
                //{
                //    UpdateLog("clsDiseaseManagement -- GetCriteriaName -- " + ex.ToString());
                //    MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                //}
                return "";
            }

            public string GetCriteriaMessage(long oCriteriaID)
            {
                //string _strSQL = "";
                //string _Result = "";
                //gloStream.gloDataBase.gloDataBase oDB = new gloStream.gloDataBase.gloDataBase();
                //try
                //{
                //    if (!(oCriteriaID == 0))
                //    {
                //        //Criteria Master Record
                //        _strSQL = "SELECT dm_mst_DisplayMessage FROM DM_Criteria_MST WHERE dm_mst_Id = " + oCriteriaID + " AND dm_mst_DisplayMessage IS NOT NULL";
                //        oDB.Connect(GetConnectionString);
                //        _Result = oDB.ExecuteQueryScaler(_strSQL);
                //        oDB.Disconnect();
                //        oDB.Dispose();
                //        oDB = null;
                //        //Return Object
                //        return _Result;
                //    }
                //    else
                //    {
                //        _ErrorMessage = "Please select Criteria";
                //    }
                //}
                //catch (SqlException ex)
                //{
                //    MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    UpdateLog("clsDiseaseManagement -- GetCriteriaName -- " + ex.ToString());
                //}
                //catch (Exception ex)
                //{
                //    UpdateLog("clsDiseaseManagement -- GetCriteriaName -- " + ex.ToString());
                //    MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                //}
                return "";
            }


            public gloStream.DiseaseManagement.Supporting.ItemDetails GetCriteraList()
            {
                ////criteria master 
                ////id, name
                //string _strSQL = "";
                //gloStream.gloDataBase.gloDataBase oDB = new gloStream.gloDataBase.gloDataBase();
                //System.Data.SqlClient.SqlDataReader oDataReader = null;
                gloStream.DiseaseManagement.Supporting.ItemDetails oItemDetails = new gloStream.DiseaseManagement.Supporting.ItemDetails();

                //try
                //{
                //    oDB.Connect(GetConnectionString);

                //    //_strSQL = "SELECT dm_mst_Id, dm_mst_CriteriaName FROM DM_Criteria_MST"
                //    _strSQL = "SELECT dm_mst_Id, dm_mst_CriteriaName FROM DM_Criteria_MST WHERE dm_mst_PatientID = 0 or dm_mst_PatientID IS NULL";

                //    oDataReader = oDB.ReadQueryRecords(_strSQL);

                //    if ((oDataReader != null))
                //    {
                //        if (oDataReader.HasRows == true)
                //        {
                //            while (oDataReader.Read())
                //            {
                //                var _with20 = oItemDetails;
                //                if (!(Information.IsDBNull(oDataReader["dm_mst_Id"]) & Information.IsDBNull(oDataReader["dm_mst_CriteriaName"])))
                //                {
                //                    _with20.Add(Convert.ToInt64( oDataReader["dm_mst_Id"]), oDataReader["dm_mst_CriteriaName"].ToString());
                //                }
                //            }
                //        }
                //        oDataReader.Close();
                //    }

                //    return oItemDetails;

                //}
                //catch (Exception ex)
                //{
                //    //UpdateLog("clsDiseaseManagement -- GetCriteraList -- " + ex.ToString());
                //    MessageBox.Show(ex.ToString(),clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                //}
                //finally
                //{
                //    oDB.Disconnect();
                //    oDB.Dispose();
                //    oDB = null;
                //    oDataReader.Close();
                //}
                return oItemDetails;
            }
            //SHUBHANGI 20091005
            //To Apply In string search c1DiseaseList 
            public DataTable GetCriteraNames()
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(clsGeneral.EMRConnectionString);
                DataTable oResultTable = new DataTable();
                string strQuery = "";
                try
                {
                    oDB.Connect(false);
                    strQuery = "SELECT dm_mst_Id, dm_mst_CriteriaName FROM DM_Criteria_MST WHERE dm_mst_PatientID = 0 or dm_mst_PatientID IS NULL";
                    oDB.Retrive_Query(strQuery, out oResultTable);
                }
                catch //(Exception ex)
                {
                    oResultTable = null;
                    //commented by kanchan on 20120104
                    //MessageBox.Show(ex.Message);
                }
                finally
                {
                    oDB.Dispose();
                    oResultTable.Dispose();
                    oDB = null;
                }
                return oResultTable;
            }

            public Collection FindGuidelinesForMultiplePatient(long CriteriaID)
            {
                Collection functionReturnValue = null;
                ////A & D
                //string _strSQL = "";
                //gloStream.gloDataBase.gloDataBase oDB = new gloStream.gloDataBase.gloDataBase();
                //System.Data.SqlClient.SqlDataReader oDataReader = null;

                //Int16 _AgeMin = 0;
                //Int16 _AgeMax = 0;
                //string _Geneder = "";
                //string _Race = "";
                //string _MaritalStatus = "";
                //string _City = "";
                //string _State = "";
                //string _ZipCode = "";
                //string _EmpStatus = "";
                //double _HeightFtMin = 0;
                //double _HeightFtMax = 0;
                //double _HeightInchMin = 0;
                //double _HeightInchMax = 0;
                //double _WeightMin = 0;
                //double _WeightMax = 0;
                //double _PulseMin = 0;
                //double _PulseMax = 0;
                //double _PulseOXMin = 0;
                //double _PulseOXMax = 0;
                //double _BPSittingMin = 0;
                //double _BPSittingMax = 0;
                //double _BPStandingMin = 0;
                //double _BPStandingMax = 0;
                //double _BMIMin = 0;
                //double _BMIMax = 0;
                //double _TempMin = 0;
                //double _TempMax = 0;
                //bool _HaveVital = false;
                //bool _HaveVitalHeight = false;

                //Collection _Histories = new Collection();
                //Collection _Drugs = new Collection();
                //Collection _ICD9s = new Collection();
                //Collection _CPTs = new Collection();
                //Collection _Labs = new Collection();
                //Collection _LabModule = new Collection();

                //Collection _FinalPatientID = new Collection();
                //Collection _PrimaryPatientID = new Collection();
                //Collection _TempPatientID = new Collection();
                //string _PrimaryINPatientID = "";

                //try
                //{
                //    Application.DoEvents();
                //    if (StartCriteria != null)
                //    {
                //        StartCriteria(true);
                //    }
                //    //*********************>>>--- READ CRITERIA CONDITION ---<<<*******************************
                //    //connect to the database    
                //    oDB.Connect(GetConnectionString);

                //    //set the query string to retrieve the Patient record from the Patient table from the PatientID passed
                //    _strSQL = "SELECT dm_mst_Id,dm_mst_AgeMin,dm_mst_AgeMax, " + " dm_mst_Gender,dm_mst_Race,dm_mst_MaritalStatus, " + " dm_mst_City,dm_mst_Status,dm_mst_Zip, " + " dm_mst_EmplyementStatus,dm_mst_HeightMin,dm_mst_HeightMax, " + " dm_mst_WeightMin,dm_mst_WeightMax, " + " dm_mst_BMIMin,dm_mst_BMIMax,dm_mst_TemperatureMin,dm_mst_TemperatureMax, " + " dm_mst_PulseMin,dm_mst_PulseMax,dm_mst_PulseOxMin,dm_mst_PulseOxMax, " + " dm_mst_BPSittingMin,dm_mst_BPSittingMax,dm_mst_BPStandingMin,dm_mst_BPStandingMax, " + " dm_mst_DisplayMessage from dm_criteria_mst WHERE dm_mst_Id = " + CriteriaID + " ";

                //    if (ProcessCriteria != null)
                //    {
                //        ProcessCriteria("Start Findings");
                //    }

                //    //execute the query and get the results in a datareader
                //    oDataReader = oDB.ReadQueryRecords(_strSQL);
                //    if ((oDataReader != null))
                //    {
                //        if (oDataReader.HasRows == true)
                //        {
                //            while (oDataReader.Read())
                //            {
                //                if (!Information.IsDBNull(oDataReader["dm_mst_AgeMin"]))
                //                {
                //                    _AgeMin =Convert.ToInt16(oDataReader["dm_mst_AgeMin"]);//+ "";
                //                }
                //                if (!Information.IsDBNull(oDataReader["dm_mst_AgeMax"]))
                //                {
                //                    _AgeMax = Convert.ToInt16(oDataReader["dm_mst_AgeMax"]);//+ "";
                //                }
                //                if (!Information.IsDBNull(oDataReader["dm_mst_Gender"]))
                //                {
                //                    _Geneder = oDataReader["dm_mst_Gender"] + "";
                //                }
                //                if (!Information.IsDBNull(oDataReader["dm_mst_Race"]))
                //                {
                //                    _Race = oDataReader["dm_mst_Race"] + "";
                //                }
                //                if (!Information.IsDBNull(oDataReader["dm_mst_MaritalStatus"]))
                //                {
                //                    _MaritalStatus = oDataReader["dm_mst_MaritalStatus"] + "";
                //                }
                //                if (!Information.IsDBNull(oDataReader["dm_mst_City"]))
                //                {
                //                    _City = oDataReader["dm_mst_City"] + "";
                //                }
                //                if (!Information.IsDBNull(oDataReader["dm_mst_Status"]))
                //                {
                //                    _State = oDataReader["dm_mst_Status"] + "";
                //                }
                //                if (!Information.IsDBNull(oDataReader["dm_mst_Zip"]))
                //                {
                //                    _ZipCode = oDataReader["dm_mst_Zip"] + "";
                //                }
                //                if (!Information.IsDBNull(oDataReader["dm_mst_EmplyementStatus"]))
                //                {
                //                    _EmpStatus = oDataReader["dm_mst_EmplyementStatus"] + "";
                //                }

                //                string[] arrPatHeightMin = null;
                //                if (!Information.IsDBNull(oDataReader["dm_mst_HeightMin"]))
                //                {
                //                    arrPatHeightMin = (string[])GetFtInch(oDataReader["dm_mst_HeightMin"] + "");
                //                    if ((arrPatHeightMin != null))
                //                    {
                //                        if ((arrPatHeightMin[0] != null))
                //                        {
                //                            _HeightFtMin = Conversion.Val(arrPatHeightMin[0]);
                //                            if ((arrPatHeightMin[1] != null))
                //                                _HeightInchMin = Conversion.Val(arrPatHeightMin[1]);
                //                        }
                //                    }
                //                }

                //                string[] arrPatHeightMax = null;
                //                if (!Information.IsDBNull(oDataReader["dm_mst_HeightMax"]))
                //                {
                //                    arrPatHeightMax = (string[])GetFtInch(oDataReader["dm_mst_HeightMax"] + "");
                //                    if ((arrPatHeightMax != null))
                //                    {
                //                        if ((arrPatHeightMax[0] != null))
                //                        {
                //                            _HeightFtMax = Conversion.Val(arrPatHeightMax[0]);
                //                            if ((arrPatHeightMax[1] != null))
                //                                _HeightInchMax = Conversion.Val(arrPatHeightMax[1]);
                //                        }
                //                    }
                //                }

                //                //// now here we directlly convert height into meter, so we use only ft varibale against this
                //                _HeightFtMin = FtToMtr(Convert.ToDecimal(_HeightFtMin), Convert.ToDecimal(_HeightInchMin));
                //                _HeightFtMax = FtToMtr(Convert.ToDecimal(_HeightFtMax), Convert.ToDecimal(_HeightInchMax));
                //                if (_HeightFtMin + _HeightFtMax > 0)
                //                {
                //                    _HaveVitalHeight = true;
                //                }

                //                if (!Information.IsDBNull(oDataReader["dm_mst_WeightMin"]))
                //                {
                //                    _WeightMin = Convert.ToDouble(oDataReader["dm_mst_WeightMin"]);// +"";
                //                    if (_WeightMin > 0)
                //                        _HaveVital = true;
                //                }
                //                if (!Information.IsDBNull(oDataReader["dm_mst_WeightMax"]))
                //                {
                //                    _WeightMax = Convert.ToDouble(oDataReader["dm_mst_WeightMax"]);// +"";
                //                    if (_WeightMax > 0)
                //                        _HaveVital = true;
                //                }
                //                if (!Information.IsDBNull(oDataReader["dm_mst_PulseMin"]))
                //                {
                //                    _PulseMin = Convert.ToDouble(oDataReader["dm_mst_PulseMin"]);// +"";
                //                    if (_PulseMin > 0)
                //                        _HaveVital = true;
                //                }
                //                if (!Information.IsDBNull(oDataReader["dm_mst_PulseMax"]))
                //                {
                //                    _PulseMax = Convert.ToDouble(oDataReader["dm_mst_PulseMax"]);// +"";
                //                    if (_PulseMax > 0)
                //                        _HaveVital = true;
                //                }
                //                if (!Information.IsDBNull(oDataReader["dm_mst_PulseOxMin"]))
                //                {
                //                    _PulseOXMin = Convert.ToDouble(oDataReader["dm_mst_PulseOxMin"]);// +"";
                //                    if (_PulseOXMin > 0)
                //                        _HaveVital = true;
                //                }
                //                if (!Information.IsDBNull(oDataReader["dm_mst_PulseOxMax"]))
                //                {
                //                    _PulseOXMax = Convert.ToDouble(oDataReader["dm_mst_PulseOxMax"]);// +"";
                //                    if (_PulseOXMax > 0)
                //                        _HaveVital = true;
                //                }
                //                if (!Information.IsDBNull(oDataReader["dm_mst_BPSittingMin"]))
                //                {
                //                    _BPSittingMin = Convert.ToDouble(oDataReader["dm_mst_BPSittingMin"]);// +"";
                //                    if (_BPSittingMin > 0)
                //                        _HaveVital = true;
                //                }
                //                if (!Information.IsDBNull(oDataReader["dm_mst_BPSittingMax"]))
                //                {
                //                    _BPSittingMax = Convert.ToDouble(oDataReader["dm_mst_BPSittingMax"]);// +"";
                //                    if (_BPSittingMax > 0)
                //                        _HaveVital = true;
                //                }
                //                if (!Information.IsDBNull(oDataReader["dm_mst_BPStandingMin"]))
                //                {
                //                    _BPStandingMin = Convert.ToDouble(oDataReader["dm_mst_BPStandingMin"]);// +"";
                //                    if (_BPStandingMin > 0)
                //                        _HaveVital = true;
                //                }
                //                if (!Information.IsDBNull(oDataReader["dm_mst_BPStandingMax"]))
                //                {
                //                    _BPStandingMax = Convert.ToDouble(oDataReader["dm_mst_BPStandingMax"]);// +"";
                //                    if (_BPStandingMax > 0)
                //                        _HaveVital = true;
                //                }
                //                if (!Information.IsDBNull(oDataReader["dm_mst_BMIMin"]))
                //                {
                //                    _BMIMin = Convert.ToDouble(oDataReader["dm_mst_BMIMin"]);// +"";
                //                    if (_BMIMin > 0)
                //                        _HaveVital = true;
                //                }
                //                if (!Information.IsDBNull(oDataReader["dm_mst_BMIMax"]))
                //                {
                //                    _BMIMax = Convert.ToDouble(oDataReader["dm_mst_BMIMax"]);// +"";
                //                    if (_BMIMax > 0)
                //                        _HaveVital = true;
                //                }
                //                if (!Information.IsDBNull(oDataReader["dm_mst_TemperatureMin"]))
                //                {
                //                    _TempMin = Convert.ToDouble(oDataReader["dm_mst_TemperatureMin"]);// +"";
                //                    if (_TempMin > 0)
                //                        _HaveVital = true;
                //                }
                //                if (!Information.IsDBNull(oDataReader["dm_mst_TemperatureMax"]))
                //                {
                //                    _TempMax = Convert.ToDouble(oDataReader["dm_mst_TemperatureMax"]);// +"";
                //                    if (_TempMax > 0)
                //                        _HaveVital = true;
                //                }
                //            }
                //        }
                //        oDataReader.Close();
                //    }

                //    //// History
                //    //_strSQL = "SELECT dm_Chdtl_HistoryItemId FROM DM_CriteriaHistory_DTL WHERE dm_Chdtl_Id = " & CriteriaID & ""
                //    _strSQL = "SELECT dm_Chdtl_HistoryItem, dm_Chdtl_HistoryCategory FROM DM_CriteriaHistory_DTL WHERE dm_Chdtl_Id = " + CriteriaID + "";
                //    oDataReader = oDB.ReadQueryRecords(_strSQL);
                //    if ((oDataReader != null))
                //    {
                //        if (oDataReader.HasRows == true)
                //        {
                //            myList oList = default(myList);
                //            while (oDataReader.Read())
                //            {
                //                //_Histories.Add(oDataReader.Item("dm_Chdtl_HistoryItemId"))
                //                oList = new myList();
                //                oList.HistoryItem = oDataReader["dm_Chdtl_HistoryItem"].ToString();
                //                oList.HistoryCategory = oDataReader["dm_Chdtl_HistoryCategory"].ToString();
                //                _Histories.Add(oList);
                //                oList = null;
                //            }
                //        }
                //        oDataReader.Close();
                //    }

                //    //// Drugs
                //    _strSQL = "SELECT dm_Drugdtl_DrugID FROM DM_CriteriaDrug_DTL WHERE dm_Drugdtl_Id = " + CriteriaID + "";
                //    oDataReader = oDB.ReadQueryRecords(_strSQL);
                //    if ((oDataReader != null))
                //    {
                //        if (oDataReader.HasRows == true)
                //        {
                //            while (oDataReader.Read())
                //            {
                //                _Drugs.Add(oDataReader["dm_Drugdtl_DrugID"]);
                //            }
                //        }
                //        oDataReader.Close();
                //    }

                //    //// ICD9
                //    _strSQL = "SELECT dm_ICD9CPTdtl_ICID FROM DM_ICD9CPT_DTL WHERE dm_ICD9CPTdtl_Id = " + CriteriaID + " AND dm_ICD9CPTdtl_Type = 2";
                //    oDataReader = oDB.ReadQueryRecords(_strSQL);
                //    if ((oDataReader != null))
                //    {
                //        if (oDataReader.HasRows == true)
                //        {
                //            while (oDataReader.Read())
                //            {
                //                _ICD9s.Add(oDataReader["dm_ICD9CPTdtl_ICID"]);
                //            }
                //        }
                //    }
                //    oDataReader.Close();

                //    //// CPT
                //    _strSQL = "SELECT dm_ICD9CPTdtl_ICID FROM DM_ICD9CPT_DTL WHERE dm_ICD9CPTdtl_Id = " + CriteriaID + " AND dm_ICD9CPTdtl_Type = 1";
                //    oDataReader = oDB.ReadQueryRecords(_strSQL);
                //    if ((oDataReader != null))
                //    {
                //        if (oDataReader.HasRows == true)
                //        {
                //            while (oDataReader.Read())
                //            {
                //                _CPTs.Add(oDataReader["dm_ICD9CPTdtl_ICID"]);
                //            }
                //        }
                //        oDataReader.Close();
                //    }

                //    //// Radiology
                //    _strSQL = "SELECT dm_Labsdtl_TestID FROM DM_Labs_DTL WHERE dm_Labsdtl_Id = " + CriteriaID + " ";
                //    oDataReader = oDB.ReadQueryRecords(_strSQL);
                //    if ((oDataReader != null))
                //    {
                //        if (oDataReader.HasRows == true)
                //        {
                //            while (oDataReader.Read())
                //            {
                //                _Labs.Add(oDataReader["dm_Labsdtl_TestID"]);
                //            }
                //        }
                //        oDataReader.Close();
                //    }


                //    //// Mahesh 20070804
                //    //// Labs
                //    _strSQL = "SELECT dm_labdtl_testId,dm_labdtl_resultid FROM DM_LabModule_DTL WHERE dm_labdtl_ID = " + CriteriaID + " ";
                //    oDataReader = oDB.ReadQueryRecords(_strSQL);
                //    if ((oDataReader != null))
                //    {
                //        if (oDataReader.HasRows == true)
                //        {
                //            while (oDataReader.Read())
                //            {
                //                _LabModule.Add(oDataReader["dm_labdtl_testId"] + "|" + oDataReader["dm_labdtl_resultid"]);
                //            }
                //        }
                //        oDataReader.Close();
                //    }

                //    ////

                //    //*********************>>>--- READ CRITERIA CONDITION ---<<<*******************************

                //    //*********************>>>--- QUERY BUILDER AS PER CONDTION ---<<<*************************
                //    _strSQL = "SELECT nPatientID FROM Patient WHERE ";
                //    bool _CreateSQL = false;
                //    System.DateTime _AgeMinDate = default(System.DateTime);
                //    System.DateTime _AgeMaxDate = default(System.DateTime);
                //    //>>>AGE<<<
                //    if (_AgeMin > 0 && _AgeMax > 0)
                //    {
                //        _AgeMinDate = DateAndTime.DateAdd(DateInterval.Year, -_AgeMin, System.DateTime.Now);
                //        // eg. 2012
                //        _AgeMaxDate = DateAndTime.DateAdd(DateInterval.Year, -_AgeMax, System.DateTime.Now);
                //        // eg. 1930
                //        _strSQL = _strSQL + "dtDOB BETWEEN '" + _AgeMaxDate + "' AND '" + _AgeMinDate + "' ";
                //        _CreateSQL = true;
                //    }
                //    else
                //    {
                //        if (_AgeMin > 0 && _AgeMax == 0)
                //        {
                //            _AgeMinDate = DateAndTime.DateAdd(DateInterval.Year, -_AgeMin, System.DateTime.Now);
                //            _strSQL = _strSQL + "dtDOB >= '" + _AgeMinDate + "' ";
                //            _CreateSQL = true;
                //        }
                //        else if (_AgeMax > 0 && _AgeMin == 0)
                //        {
                //            _AgeMaxDate = DateAndTime.DateAdd(DateInterval.Year, -_AgeMax, System.DateTime.Now);
                //            _strSQL = _strSQL + "dtDOB <= '" + _AgeMaxDate + "' ";
                //            _CreateSQL = true;
                //        }
                //    }
                //    //----------------------------------------
                //    if (_CreateSQL == false)
                //        return functionReturnValue;
                //    //----------------------------------------
                //    //>>>GENEDER<<<
                //    if (!string.IsNullOrEmpty(_Geneder.Trim()))
                //    {
                //        if (!(_Geneder.Trim() == "All"))
                //        {
                //            _strSQL = _strSQL + " AND sGender = '" + _Geneder.Trim().Replace("'", "''") + "'";
                //        }
                //    }
                //    //>>>RACE<<<
                //    if (!string.IsNullOrEmpty(_Race.Trim()))
                //    {
                //        _strSQL = _strSQL + " AND sRace = '" + _Race.Trim().Replace("'", "''") + "'";
                //    }
                //    //>>>MARITAL STATUS<<<
                //    if (!string.IsNullOrEmpty(_MaritalStatus.Trim()))
                //    {
                //        _strSQL = _strSQL + " AND sMaritalStatus = '" + _MaritalStatus.Trim().Replace("'", "''") + "'";
                //    }
                //    //>>>CITY<<<
                //    if (!string.IsNullOrEmpty(_City.Trim()))
                //    {
                //        _strSQL = _strSQL + " AND sCity = '" + _City.Trim().Replace("'", "''") + "'";
                //    }
                //    //>>>STATE<<<
                //    if (!string.IsNullOrEmpty(_State.Trim()))
                //    {
                //        _strSQL = _strSQL + " AND sState = '" + _State.Trim().Replace("'", "''") + "'";
                //    }
                //    //>>>ZIPCODE<<<
                //    if (!string.IsNullOrEmpty(_ZipCode.Trim()))
                //    {
                //        _strSQL = _strSQL + " AND sZIP = '" + _ZipCode.Trim().Replace("'", "''") + "'";
                //    }
                //    //>>>ZIPCODE<<<
                //    if (!string.IsNullOrEmpty(_ZipCode.Trim()))
                //    {
                //        _strSQL = _strSQL + " AND sZIP = '" + _ZipCode.Trim().Replace("'", "''") + "'";
                //    }
                //    //>>>EMPSTATUS<<<
                //    if (!string.IsNullOrEmpty(_EmpStatus.Trim()))
                //    {
                //        _strSQL = _strSQL + " AND sEmploymentStatus = '" + _EmpStatus.Trim().Replace("'", "''") + "'";
                //    }

                //    if (!string.IsNullOrEmpty(_strSQL.Trim()))
                //    {
                //        oDataReader = oDB.ReadQueryRecords(_strSQL);
                //        if ((oDataReader != null))
                //        {
                //            if (oDataReader.HasRows == true)
                //            {
                //                while (oDataReader.Read())
                //                {
                //                    if (!Information.IsDBNull(oDataReader["nPatientID"]))
                //                    {
                //                        _PrimaryPatientID.Add(oDataReader["nPatientID"]);
                //                    }
                //                }
                //            }
                //            oDataReader.Close();
                //        }
                //    }
                //    for (Int16 i = 1; i <= _PrimaryPatientID.Count; i++)
                //    {
                //        if (i > 1)
                //        {
                //            _PrimaryINPatientID = _PrimaryINPatientID + "," + _PrimaryPatientID[i];
                //        }
                //        else
                //        {
                //            _PrimaryINPatientID = _PrimaryPatientID[i].ToString();
                //        }
                //    }

                //    //:::VITALS:::

                //    //Dim _PrimaryPatientID As New Collection
                //    //Dim _TempPatientID As New Collection
                //    //Dim _PrimaryINPatientID As String = ""

                //    bool _RunVitals = false;
                //    if (_PrimaryPatientID.Count > 0)
                //    {
                //        _strSQL = "";
                //        _strSQL = "Select distinct nPatientID from vitals v where dtvitaldate in " + " (select max(dtvitaldate) from vitals v1 where npatientid=v.npatientid group by nPatientID) " + " AND nPatientid in (" + _PrimaryINPatientID + ")";


                //        //>>>Weight Min & Max <<<
                //        if (_WeightMin > 0 && _WeightMax > 0)
                //        {
                //            _strSQL = _strSQL + " AND dWeightinlbs BETWEEN " + _WeightMin + " AND " + _WeightMax + "";
                //            _RunVitals = true;
                //        }
                //        else
                //        {
                //            if (_WeightMin > 0 && _WeightMax == 0)
                //            {
                //                _strSQL = _strSQL + " AND dWeightinlbs >= " + _WeightMin + "";
                //                _RunVitals = true;
                //            }
                //            else if (_WeightMax > 0 && _WeightMin == 0)
                //            {
                //                _strSQL = _strSQL + " AND dWeightinlbs <= " + _WeightMax + "";
                //                _RunVitals = true;
                //            }
                //        }
                //        //>>>Pulse Min & Max <<<
                //        if (_PulseMin > 0 && _PulseMax > 0)
                //        {
                //            _strSQL = _strSQL + " AND dPulsePerMinute BETWEEN " + _PulseMin + " AND " + _PulseMax + "";
                //            _RunVitals = true;
                //        }
                //        else
                //        {
                //            if (_PulseMin > 0 && _PulseMax == 0)
                //            {
                //                _strSQL = _strSQL + " AND dPulsePerMinute >= " + _PulseMin + "";
                //                _RunVitals = true;
                //            }
                //            else if (_PulseMax > 0 && _PulseMin == 0)
                //            {
                //                _strSQL = _strSQL + " AND dPulsePerMinute <= " + _PulseMax + "";
                //                _RunVitals = true;
                //            }
                //        }
                //        //>>>Pulse OX Min & Max <<<
                //        if (_PulseOXMin > 0 && _PulseOXMax > 0)
                //        {
                //            _strSQL = _strSQL + " AND dPulseOx BETWEEN " + _PulseOXMin + " AND " + _PulseOXMax + "";
                //            _RunVitals = true;
                //        }
                //        else
                //        {
                //            if (_PulseOXMin > 0 && _PulseOXMax == 0)
                //            {
                //                _strSQL = _strSQL + " AND dPulseOx >= " + _PulseOXMin + "";
                //                _RunVitals = true;
                //            }
                //            else if (_PulseMax > 0 && _PulseMin == 0)
                //            {
                //                _strSQL = _strSQL + " AND dPulseOx <= " + _PulseOXMax + "";
                //                _RunVitals = true;
                //            }
                //        }
                //        //>>>BP Sitting Min & Max <<<
                //        if (_BPSittingMin > 0 && _BPSittingMax > 0)
                //        {
                //            _strSQL = _strSQL + " AND dBloodPressureSittingMin >= " + _BPSittingMin + " AND dBloodPressureSittingMax <= " + _BPSittingMax + "";
                //            _RunVitals = true;
                //        }
                //        else
                //        {
                //            if (_BPSittingMin > 0 && _BPSittingMax == 0)
                //            {
                //                _strSQL = _strSQL + " AND dBloodPressureSittingMin >= " + _BPSittingMin + "";
                //                _RunVitals = true;
                //            }
                //            else if (_BPSittingMax > 0 && _BPSittingMin == 0)
                //            {
                //                _strSQL = _strSQL + " AND dBloodPressureSittingMax <= " + _BPSittingMax + "";
                //                _RunVitals = true;
                //            }
                //        }
                //        //>>>BP Sitting Min & Max <<<
                //        if (_BPStandingMin > 0 && _BPStandingMax > 0)
                //        {
                //            _strSQL = _strSQL + " AND dBloodPressureStandingMin >= " + _BPStandingMin + " AND dBloodPressureStandingMax <= " + _BPStandingMax + "";
                //            _RunVitals = true;
                //        }
                //        else
                //        {
                //            if (_BPStandingMin > 0 && _BPStandingMax == 0)
                //            {
                //                _strSQL = _strSQL + " AND dBloodPressureStandingMin >= " + _BPStandingMin + "";
                //                _RunVitals = true;
                //            }
                //            else if (_BPStandingMax > 0 && _BPStandingMin == 0)
                //            {
                //                _strSQL = _strSQL + " AND dBloodPressureStandingMax <= " + _BPStandingMax + "";
                //                _RunVitals = true;
                //            }
                //        }
                //        //>>>BP Sitting Min & Max <<<
                //        if (_TempMin > 0 && _TempMax > 0)
                //        {
                //            _strSQL = _strSQL + " AND dTemperature >= " + _TempMin + " AND dTemperature <= " + _TempMax + "";
                //            _RunVitals = true;
                //        }
                //        else
                //        {
                //            if (_TempMin > 0 && _TempMax == 0)
                //            {
                //                _strSQL = _strSQL + " AND dTemperature >= " + _TempMin + "";
                //                _RunVitals = true;
                //            }
                //            else if (_TempMax > 0 && _TempMin == 0)
                //            {
                //                _strSQL = _strSQL + " AND dTemperature <= " + _TempMax + "";
                //                _RunVitals = true;
                //            }
                //        }

                //        //_HeightFtMin = 0 - Vital
                //        //_HeightFtMax = 0 - Vital
                //        //_HeightInchMin = 0 - Vital
                //        //_HeightInchMax = 0 - Vital

                //        if (_RunVitals == true)
                //        {
                //            _TempPatientID = new Collection();
                //            if (!string.IsNullOrEmpty(_strSQL.Trim()))
                //            {
                //                oDataReader = oDB.ReadQueryRecords(_strSQL);
                //                if ((oDataReader != null))
                //                {
                //                    if (oDataReader.HasRows == true)
                //                    {
                //                        while (oDataReader.Read())
                //                        {
                //                            if (!Information.IsDBNull(oDataReader["nPatientID"]))
                //                            {
                //                                _TempPatientID.Add(oDataReader["nPatientID"]);
                //                            }
                //                        }
                //                    }
                //                    oDataReader.Close();
                //                }
                //            }
                //        }

                //        if (_HaveVital == true)
                //        {
                //            if (_TempPatientID.Count > 1)
                //            {
                //                _PrimaryPatientID = new Collection();
                //                for (Int16 i = 1; i <= _TempPatientID.Count; i++)
                //                {
                //                    _PrimaryPatientID.Add(_TempPatientID[i]);
                //                }
                //                _PrimaryINPatientID = "";
                //                for (Int16 i = 1; i <= _PrimaryPatientID.Count; i++)
                //                {
                //                    if (i > 1)
                //                    {
                //                        _PrimaryINPatientID = _PrimaryINPatientID + "," + _PrimaryPatientID[i];
                //                    }
                //                    else
                //                    {
                //                        _PrimaryINPatientID = _PrimaryPatientID[i].ToString();
                //                    }
                //                }
                //                _TempPatientID = null;

                //            }
                //            else
                //            {
                //                for (int i = _PrimaryPatientID.Count; i >= 1; i += -1)
                //                {
                //                    _PrimaryPatientID.Remove(i);
                //                }
                //                goto FinishFindingProcess;
                //            }
                //        }

                //        ////<<<<<<< History >>>>>>>
                //        if (_HaveVitalHeight == true)
                //        {
                //            _TempPatientID = new Collection();

                //            _strSQL = "Select distinct nPatientID,sHeight from vitals v where dtvitaldate in " + " (select max(dtvitaldate) from vitals v1 where npatientid=v.npatientid group by nPatientID) " + " AND nPatientid in (" + _PrimaryINPatientID + ")";
                //            oDataReader = oDB.ReadQueryRecords(_strSQL);
                //            if ((oDataReader != null))
                //            {
                //                if (oDataReader.HasRows == true)
                //                {
                //                    while (oDataReader.Read())
                //                    {
                //                        if (!Information.IsDBNull(oDataReader["nPatientID"]))
                //                        {
                //                            string[] _strPatHt = null;
                //                            double _PatHtFt = 0;
                //                            double _PatHtInch = 0;

                //                            if (!Information.IsDBNull(oDataReader["sHeight"]))
                //                            {
                //                                _strPatHt = (string[])GetFtInch(oDataReader["sHeight"] + "");
                //                                if ((_strPatHt != null))
                //                                {
                //                                    if ((_strPatHt[0] != null))
                //                                        _PatHtFt = Conversion.Val(_strPatHt[0]);
                //                                    if ((_strPatHt[1] != null))
                //                                        _PatHtInch = Conversion.Val(_strPatHt[1]);
                //                                }
                //                            }

                //                            _PatHtFt = FtToMtr(Convert.ToDecimal(_PatHtFt),Convert.ToDecimal( _PatHtInch));

                //                            if (_HeightFtMin > 0 | _HeightFtMax > 0)
                //                            {
                //                                if (_HeightFtMin > 0 & _HeightFtMax > 0)
                //                                {
                //                                    if (_PatHtFt >= _HeightFtMin & _PatHtFt <= _HeightFtMax)
                //                                    {
                //                                        _TempPatientID.Add(oDataReader["nPatientID"]);
                //                                    }
                //                                }
                //                                else if (_HeightFtMin > 0 & _HeightFtMax == 0)
                //                                {
                //                                    if (_PatHtFt >= _HeightFtMin)
                //                                    {
                //                                        _TempPatientID.Add(oDataReader["nPatientID"]);
                //                                    }
                //                                }
                //                                else if (_HeightFtMax > 0 & _HeightFtMin == 0)
                //                                {
                //                                    if (_PatHtFt <= _HeightFtMax)
                //                                    {
                //                                        _TempPatientID.Add(oDataReader["nPatientID"]);
                //                                    }
                //                                }
                //                            }


                //                        }
                //                    }
                //                }
                //                oDataReader.Close();
                //            }

                //            //// Work with Height match record
                //            if (_TempPatientID.Count > 0)
                //            {
                //                _PrimaryPatientID = new Collection();
                //                for (Int16 i = 1; i <= _TempPatientID.Count; i++)
                //                {
                //                    _PrimaryPatientID.Add(_TempPatientID[i]);
                //                }
                //                _PrimaryINPatientID = "";
                //                for (Int16 i = 1; i <= _PrimaryPatientID.Count; i++)
                //                {
                //                    if (i > 1)
                //                    {
                //                        _PrimaryINPatientID = _PrimaryINPatientID + "," + _PrimaryPatientID[i];
                //                    }
                //                    else
                //                    {
                //                        _PrimaryINPatientID = _PrimaryPatientID[i].ToString();
                //                    }
                //                }

                //            }
                //            else
                //            {
                //                for (int i = _PrimaryPatientID.Count; i >= 1; i += -1)
                //                {
                //                    _PrimaryPatientID.Remove(i);
                //                }
                //                goto FinishFindingProcess;
                //            }

                //            _TempPatientID = null;
                //        }
                //        ////<<<<<<< History >>>>>>>

                //    }
                //    //:::VITALS:::

                //    //*********************>>>--- QUERY BUILDER AS PER CONDTION ---<<<*************************

                //    //*********************>>>--- HISTORY, DRUGS, CPT, ICD9 Start ---<<<*****************************
                //    if (!string.IsNullOrEmpty(_PrimaryINPatientID.Trim()))
                //    {
                //        //::: HISTORY :::
                //        _strSQL = "";
                //        if (_Histories.Count > 0)
                //        {
                //            //'Commented by sudhir 20090302 ''
                //            //_strSQL = "SELECT DISTINCT History.nPatientID " _
                //            //& " FROM DM_CriteriaHistory_DTL INNER JOIN History_MST ON DM_CriteriaHistory_DTL.dm_Chdtl_HistoryItemId = History_MST.nHistoryID INNER JOIN " _
                //            //& " History ON History_MST.sDescription = History.sHistoryItem " _
                //            //& " WHERE (DM_CriteriaHistory_DTL.dm_Chdtl_Id = " & CriteriaID & ") AND (History.nPatientID IN (" & _PrimaryINPatientID & "))"

                //            _strSQL = "SELECT DISTINCT History.nPatientID FROM History INNER JOIN DM_CriteriaHistory_DTL ON History.sHistoryItem " + " = DM_CriteriaHistory_DTL.dm_Chdtl_HistoryItem " + " WHERE (DM_CriteriaHistory_DTL.dm_Chdtl_Id = " + CriteriaID + ") AND (History.nPatientID IN " + _PrimaryINPatientID + ")";
                //            _TempPatientID = new Collection();
                //            if (!string.IsNullOrEmpty(_strSQL.Trim()))
                //            {
                //                oDataReader = oDB.ReadQueryRecords(_strSQL);
                //                if ((oDataReader != null))
                //                {
                //                    if (oDataReader.HasRows == true)
                //                    {
                //                        while (oDataReader.Read())
                //                        {
                //                            if (!Information.IsDBNull(oDataReader["nPatientID"]))
                //                            {
                //                                _TempPatientID.Add(oDataReader["nPatientID"]);
                //                            }
                //                        }
                //                    }
                //                    oDataReader.Close();
                //                }
                //            }
                //            if (_TempPatientID.Count > 0)
                //            {
                //                _PrimaryPatientID = new Collection();
                //                for (Int16 i = 1; i <= _TempPatientID.Count; i++)
                //                {
                //                    _PrimaryPatientID.Add(_TempPatientID[i]);
                //                }

                //                _PrimaryINPatientID = "";
                //                for (Int16 i = 1; i <= _PrimaryPatientID.Count; i++)
                //                {
                //                    if (i > 1)
                //                    {
                //                        _PrimaryINPatientID = _PrimaryINPatientID + "," + _PrimaryPatientID[i];
                //                    }
                //                    else
                //                    {
                //                        _PrimaryINPatientID = _PrimaryPatientID[i].ToString();
                //                    }
                //                }
                //                _TempPatientID = null;
                //            }
                //            else
                //            {
                //                for (int i = _PrimaryPatientID.Count; i >= 1; i += -1)
                //                {
                //                    _PrimaryPatientID.Remove(i);
                //                }
                //                goto FinishFindingProcess;
                //            }
                //        }

                //        //::: DRUGS :::
                //        _strSQL = "";
                //        if (_Drugs.Count > 0)
                //        {
                //            _strSQL = "SELECT DISTINCT Medication.nPatientID " + " FROM DM_CriteriaDrug_DTL INNER JOIN Drugs_MST ON DM_CriteriaDrug_DTL.dm_Drugdtl_DrugID = Drugs_MST.nDrugsID INNER JOIN " + " Medication ON Drugs_MST.sDrugName = Medication.sMedication AND Drugs_MST.sDosage = Medication.sDosage " + " WHERE (DM_CriteriaDrug_DTL.dm_Drugdtl_Id = " + CriteriaID + ") AND (Medication.nPatientID IN (" + _PrimaryINPatientID + "))";

                //            _TempPatientID = new Collection();
                //            if (!string.IsNullOrEmpty(_strSQL.Trim()))
                //            {
                //                oDataReader = oDB.ReadQueryRecords(_strSQL);
                //                if ((oDataReader != null))
                //                {
                //                    if (oDataReader.HasRows == true)
                //                    {
                //                        while (oDataReader.Read())
                //                        {
                //                            if (!Information.IsDBNull(oDataReader["nPatientID"]))
                //                            {
                //                                _TempPatientID.Add(oDataReader["nPatientID"]);
                //                            }
                //                        }
                //                    }
                //                    oDataReader.Close();
                //                }
                //            }
                //            if (_TempPatientID.Count > 0)
                //            {
                //                _PrimaryPatientID = new Collection();
                //                for (Int16 i = 1; i <= _TempPatientID.Count; i++)
                //                {
                //                    _PrimaryPatientID.Add(_TempPatientID[i]);
                //                }

                //                _PrimaryINPatientID = "";
                //                for (Int16 i = 1; i <= _PrimaryPatientID.Count; i++)
                //                {
                //                    if (i > 1)
                //                    {
                //                        _PrimaryINPatientID = _PrimaryINPatientID + "," + _PrimaryPatientID[i];
                //                    }
                //                    else
                //                    {
                //                        _PrimaryINPatientID = _PrimaryPatientID[i].ToString();
                //                    }
                //                }
                //                _TempPatientID = null;
                //            }
                //            else
                //            {
                //                for (int i = _PrimaryPatientID.Count; i >= 1; i += -1)
                //                {
                //                    _PrimaryPatientID.Remove(i);
                //                }
                //                goto FinishFindingProcess;
                //            }
                //        }
                //        //::: ICD9 :::
                //        _strSQL = "";
                //        if (_ICD9s.Count > 0)
                //        {
                //            _strSQL = "SELECT DISTINCT ExamICD9CPT.nPatientID " + " FROM DM_ICD9CPT_DTL INNER JOIN ICD9 ON DM_ICD9CPT_DTL.dm_ICD9CPTdtl_ICID = ICD9.nICD9ID INNER JOIN " + " ExamICD9CPT ON ICD9.sICD9Code = ExamICD9CPT.sICD9Code AND ICD9.sDescription = ExamICD9CPT.sICD9Description " + " WHERE (DM_ICD9CPT_DTL.dm_ICD9CPTdtl_Type = 2) AND (DM_ICD9CPT_DTL.dm_ICD9CPTdtl_Id = " + CriteriaID + ") " + " AND (ExamICD9CPT.nPatientID IN (" + _PrimaryINPatientID + "))";

                //            _TempPatientID = new Collection();
                //            if (!string.IsNullOrEmpty(_strSQL.Trim()))
                //            {
                //                oDataReader = oDB.ReadQueryRecords(_strSQL);
                //                if ((oDataReader != null))
                //                {
                //                    if (oDataReader.HasRows == true)
                //                    {
                //                        while (oDataReader.Read())
                //                        {
                //                            if (!Information.IsDBNull(oDataReader["nPatientID"]))
                //                            {
                //                                _TempPatientID.Add(oDataReader["nPatientID"]);
                //                            }
                //                        }
                //                    }
                //                    oDataReader.Close();
                //                }
                //            }
                //            if (_TempPatientID.Count > 0)
                //            {
                //                _PrimaryPatientID = new Collection();
                //                for (Int16 i = 1; i <= _TempPatientID.Count; i++)
                //                {
                //                    _PrimaryPatientID.Add(_TempPatientID[i]);
                //                }

                //                _PrimaryINPatientID = "";
                //                for (Int16 i = 1; i <= _PrimaryPatientID.Count; i++)
                //                {
                //                    if (i > 1)
                //                    {
                //                        _PrimaryINPatientID = _PrimaryINPatientID + "," + _PrimaryPatientID[i];
                //                    }
                //                    else
                //                    {
                //                        _PrimaryINPatientID = _PrimaryPatientID[i].ToString();
                //                    }
                //                }
                //                _TempPatientID = null;
                //            }
                //            else
                //            {
                //                for (int i = _PrimaryPatientID.Count; i >= 1; i += -1)
                //                {
                //                    _PrimaryPatientID.Remove(i);
                //                }
                //                goto FinishFindingProcess;
                //            }
                //        }
                //        //::: CPT :::
                //        _strSQL = "";
                //        if (_CPTs.Count > 0)
                //        {
                //            _strSQL = "SELECT DISTINCT ExamICD9CPT.nPatientID " + " FROM DM_ICD9CPT_DTL INNER JOIN CPT_MST ON DM_ICD9CPT_DTL.dm_ICD9CPTdtl_ICID = CPT_MST.nCPTID INNER JOIN " + " ExamICD9CPT ON CPT_MST.sCPTCode = ExamICD9CPT.sCPTCode AND CPT_MST.sDescription = ExamICD9CPT.sCPTDescription " + " WHERE (DM_ICD9CPT_DTL.dm_ICD9CPTdtl_Type = 1) AND (DM_ICD9CPT_DTL.dm_ICD9CPTdtl_Id = " + CriteriaID + ") AND (ExamICD9CPT.nPatientID IN " + " (" + _PrimaryINPatientID + "))";

                //            _TempPatientID = new Collection();
                //            if (!string.IsNullOrEmpty(_strSQL.Trim()))
                //            {
                //                oDataReader = oDB.ReadQueryRecords(_strSQL);
                //                if ((oDataReader != null))
                //                {
                //                    if (oDataReader.HasRows == true)
                //                    {
                //                        while (oDataReader.Read())
                //                        {
                //                            if (!Information.IsDBNull(oDataReader["nPatientID"]))
                //                            {
                //                                _TempPatientID.Add(oDataReader["nPatientID"]);
                //                            }
                //                        }
                //                    }
                //                    oDataReader.Close();
                //                }
                //            }
                //            if (_TempPatientID.Count > 0)
                //            {
                //                _PrimaryPatientID = new Collection();
                //                for (Int16 i = 1; i <= _TempPatientID.Count; i++)
                //                {
                //                    _PrimaryPatientID.Add(_TempPatientID[i]);
                //                }

                //                _PrimaryINPatientID = "";
                //                for (Int16 i = 1; i <= _PrimaryPatientID.Count; i++)
                //                {
                //                    if (i > 1)
                //                    {
                //                        _PrimaryINPatientID = _PrimaryINPatientID + "," + _PrimaryPatientID[i];
                //                    }
                //                    else
                //                    {
                //                        _PrimaryINPatientID = _PrimaryPatientID[i].ToString();
                //                    }
                //                }
                //                _TempPatientID = null;
                //            }
                //            else
                //            {
                //                for (int i = _PrimaryPatientID.Count; i >= 1; i += -1)
                //                {
                //                    _PrimaryPatientID.Remove(i);
                //                }
                //                goto FinishFindingProcess;
                //            }
                //        }

                //        //::: RADIOLOGY :::
                //        _strSQL = "";
                //        if (_Labs.Count > 0)
                //        {
                //            _strSQL = "SELECT DISTINCT LM_Orders.lm_Patient_ID " + " FROM DM_Labs_DTL INNER JOIN LM_Orders ON DM_Labs_DTL.dm_Labsdtl_TestID = LM_Orders.lm_test_ID " + " WHERE (DM_Labs_DTL.dm_Labsdtl_Id = " + CriteriaID + ") AND (LM_Orders.lm_NumericResult BETWEEN " + " DM_Labs_DTL.dm_Labsdtl_NumericResultMin AND DM_Labs_DTL.dm_Labsdtl_NumericResultMax) AND (LM_Orders.lm_Patient_ID IN " + " (" + _PrimaryINPatientID + "))";

                //            _TempPatientID = new Collection();
                //            if (!string.IsNullOrEmpty(_strSQL.Trim()))
                //            {
                //                oDataReader = oDB.ReadQueryRecords(_strSQL);
                //                if ((oDataReader != null))
                //                {
                //                    if (oDataReader.HasRows == true)
                //                    {
                //                        while (oDataReader.Read())
                //                        {
                //                            if (!Information.IsDBNull(oDataReader["lm_Patient_ID"]))
                //                            {
                //                                _TempPatientID.Add(oDataReader["lm_Patient_ID"]);
                //                            }
                //                        }
                //                    }
                //                    oDataReader.Close();
                //                }
                //            }
                //            if (_TempPatientID.Count > 0)
                //            {
                //                _PrimaryPatientID = new Collection();
                //                for (Int16 i = 1; i <= _TempPatientID.Count; i++)
                //                {
                //                    _PrimaryPatientID.Add(_TempPatientID[i]);
                //                }

                //                _PrimaryINPatientID = "";
                //                for (Int16 i = 1; i <= _PrimaryPatientID.Count; i++)
                //                {
                //                    if (i > 1)
                //                    {
                //                        _PrimaryINPatientID = _PrimaryINPatientID + "," + _PrimaryPatientID[i];
                //                    }
                //                    else
                //                    {
                //                        _PrimaryINPatientID = _PrimaryPatientID[i].ToString();
                //                    }
                //                }
                //                _TempPatientID = null;
                //            }
                //            else
                //            {
                //                for (int i = _PrimaryPatientID.Count; i >= 1; i += -1)
                //                {
                //                    _PrimaryPatientID.Remove(i);
                //                }
                //                goto FinishFindingProcess;
                //            }
                //        }

                //        // Mahesh 20070804
                //        //::: LABS :::
                //        _strSQL = "";
                //        if (_LabModule.Count > 0)
                //        {
                //            _strSQL = "SELECT Lab_Order_MST.labom_PatientID, DM_LabModule_DTL.dm_labdtl_TestID AS TestID, DM_LabModule_DTL.dm_labdtl_ResultID AS ResultID, " + " DM_LabModule_DTL.dm_labdtl_Operator AS CondOperator, DM_LabModule_DTL.dm_labdtl_ResultValue1 AS CondValue1, " + " DM_LabModule_DTL.dm_labdtl_ResultValue2 AS CondValue2, Lab_Order_Test_ResultDtl.labotrd_ResultValue AS PatResult " + " FROM Lab_Order_Test_ResultDtl INNER JOIN Lab_Order_MST ON Lab_Order_Test_ResultDtl.labotrd_OrderID = Lab_Order_MST.labom_OrderID INNER JOIN DM_LabModule_DTL ON Lab_Order_Test_ResultDtl.labotrd_TestID = DM_LabModule_DTL.dm_labdtl_TestID AND Lab_Order_Test_ResultDtl.labotrd_ResultNameID = DM_LabModule_DTL.dm_labdtl_ResultID " + " WHERE (DM_LabModule_DTL.dm_labdtl_ID = " + CriteriaID + ") AND (Lab_Order_MST.labom_PatientID IN (" + _PrimaryINPatientID + ")) " + " AND DM_LabModule_DTL.dm_labdtl_Operator IS NOT NULL AND Lab_Order_Test_ResultDtl.labotrd_ResultValue IS NOT NULL AND Lab_Order_MST.labom_PatientID IS NOT NULL";

                //            //Check Value is greater than or less than or equal to
                //            if (!string.IsNullOrEmpty(_strSQL.Trim()))
                //            {
                //                Collection _SortPatients = new Collection();

                //                oDataReader = oDB.ReadQueryRecords(_strSQL);
                //                if ((oDataReader != null))
                //                {
                //                    if (oDataReader.HasRows == true)
                //                    {
                //                        while (oDataReader.Read())
                //                        {
                //                            if (!Information.IsDBNull(oDataReader["CondOperator"]))
                //                            {
                //                                switch (oDataReader["CondOperator"].ToString())
                //                                {
                //                                    case "Greater Than":
                //                                        if (!Information.IsDBNull(oDataReader["CondValue1"]))
                //                                        {
                //                                            if (Convert.ToDouble(oDataReader["PatResult"] + "") > Convert.ToDouble(oDataReader["CondValue1"] + ""))
                //                                            {
                //                                                _SortPatients.Add(oDataReader["labom_PatientID"]);
                //                                            }
                //                                        }
                //                                        break;
                //                                    case "Less Than":
                //                                        if (!Information.IsDBNull(oDataReader["CondValue1"]))
                //                                        {
                //                                            if (Convert.ToDouble(oDataReader["PatResult"] + "") < Convert.ToDouble(oDataReader["CondValue1"] + ""))
                //                                            {
                //                                                _SortPatients.Add(oDataReader["labom_PatientID"]);
                //                                            }
                //                                        }
                //                                        break;
                //                                    case "Between":
                //                                        if (!Information.IsDBNull(oDataReader["CondValue1"]))
                //                                        {
                //                                            if (!Information.IsDBNull(oDataReader["CondValue2"]))
                //                                            {
                //                                                if (Convert.ToDouble(oDataReader["PatResult"] + "") >= Convert.ToDouble(oDataReader["CondValue1"] + "") & Convert.ToDouble(oDataReader["PatResult"] + "") <= Convert.ToDouble(oDataReader["CondValue2"] + ""))
                //                                                {
                //                                                    _SortPatients.Add(oDataReader["labom_PatientID"]);
                //                                                }
                //                                            }
                //                                        }
                //                                        break;
                //                                }
                //                            }
                //                        }
                //                    }
                //                    oDataReader.Close();
                //                }

                //                _TempPatientID = new Collection();

                //                if (_SortPatients.Count > 0)
                //                {
                //                    Collection _SortedPatients = new Collection();
                //                    bool _AddPatient = false;

                //                    for (Int16 i = 1; i <= _SortPatients.Count; i++)
                //                    {
                //                        _AddPatient = false;

                //                        if (_SortedPatients.Count > 0)
                //                        {
                //                            for (Int16 j = 1; j <= _SortedPatients.Count; j++)
                //                            {
                //                                if (_SortedPatients[j] == _SortPatients[i])
                //                                {
                //                                    _AddPatient = false;
                //                                    break; // TODO: might not be correct. Was : Exit For
                //                                }
                //                            }
                //                        }
                //                        else
                //                        {
                //                            _AddPatient = true;
                //                        }

                //                        if (_AddPatient == true)
                //                        {
                //                            _SortedPatients.Add(_SortPatients[i]);
                //                        }
                //                    }

                //                    _TempPatientID = new Collection();
                //                    if (_SortedPatients.Count > 0)
                //                    {
                //                        for (Int16 i = 1; i <= _SortedPatients.Count; i++)
                //                        {
                //                            _TempPatientID.Add(_SortedPatients[i]);
                //                        }
                //                    }
                //                }


                //                if (_TempPatientID.Count > 0)
                //                {
                //                    _PrimaryPatientID = new Collection();
                //                    for (Int16 i = 1; i <= _TempPatientID.Count; i++)
                //                    {
                //                        _PrimaryPatientID.Add(_TempPatientID[i]);
                //                    }

                //                    _PrimaryINPatientID = "";
                //                    for (Int16 i = 1; i <= _PrimaryPatientID.Count; i++)
                //                    {
                //                        if (i > 1)
                //                        {
                //                            _PrimaryINPatientID = _PrimaryINPatientID + "," + _PrimaryPatientID[i];
                //                        }
                //                        else
                //                        {
                //                            _PrimaryINPatientID = _PrimaryPatientID[i].ToString();
                //                        }
                //                    }
                //                    _TempPatientID = null;
                //                }
                //                else
                //                {
                //                    for (int i = _PrimaryPatientID.Count; i >= 1; i += -1)
                //                    {
                //                        _PrimaryPatientID.Remove(i);
                //                    }
                //                    goto FinishFindingProcess;
                //                }


                //            }


                //        }

                //    }
                //FinishFindingProcess:


                //    //*********************>>>--- HISTORY, DRUGS, CPT, ICD9 Finish ---<<<*****************************
                //    if (_PrimaryPatientID.Count > 0)
                //    {
                //        if (FinishCriteria != null)
                //        {
                //            FinishCriteria(true, _PrimaryPatientID);
                //        }
                //    }
                //    else
                //    {
                //        if (FinishCriteria != null)
                //        {
                //            FinishCriteria(false, _PrimaryPatientID);
                //        }
                //    }

                //}
                //catch (Exception ex)
                //{
                //    //UpdateLog("clsDiseaseManagement -- FindGuidelinesForMultiplePatient -- " + ex.ToString());
                //    MessageBox.Show(ex.ToString(),clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                //}
                //finally
                //{
                //    //oDataReader.Close()
                //    oDB.Disconnect();
                //    oDB.Dispose();
                //    oDB = null;
                //}
                return functionReturnValue;
            }

            //'Added by Amit on 21-07-2011

            public Collection FindDMCriteriaOFPatient(long PatientID, long gnClinicID)
            {
                Collection DMPatientID = new Collection();

                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(clsGeneral.EMRConnectionString);
                gloDatabaseLayer.DBParameters oDBps = new gloDatabaseLayer.DBParameters();
                DataTable dtTask = new DataTable();
                try
                {
                    oDB.Connect(false);

                    oDBps.Add("@nPatientID", PatientID, ParameterDirection.Input, SqlDbType.BigInt);
                    oDBps.Add("@nClinicID ", gnClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                    oDB.Retrive("GetDMCriteriaOFPatient", oDBps,out  dtTask);

                    oDB.Disconnect();



                    for (Int32 _row = 0; _row <= dtTask.Rows.Count - 1; _row++)
                    {
                       // bool bIsRecurring = false;
                        DMPatientID.Add(dtTask.Rows[_row]["dm_mst_Id"]);
                    }



                    return DMPatientID;

                }
                catch (gloDatabaseLayer.DBException dbErr)
                {
                    dbErr.ERROR_Log(dbErr.ToString());
                    return null;

                }
                catch (Exception ex)
                {
                    MessageBox.Show("ERROR : " + ex.Message,clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;

                }
                finally
                {
                    //Obj Disposed by mitesh
                    if ((dtTask != null))
                    {
                        dtTask.Dispose();
                        dtTask = null;
                    }
                    if ((oDB != null))
                    {
                        oDB.Dispose();
                        oDB = null;
                    }
                    DMPatientID = null;
                }

            }

            ///'' Added on 20070208
            public Collection FindGuidelinesForSinglePatient(long PatientID)
            {
                ////A & D
                //string _strSQL = "";
                //gloStream.gloDataBase.gloDataBase oDB = new gloStream.gloDataBase.gloDataBase();
                //System.Data.SqlClient.SqlDataReader oDataReader = null;

                //decimal _Age = 0;
                //string _Geneder = "";
                //string _Race = "";
                //string _MaritalStatus = "";
                //string _City = "";
                //string _State = "";
                //string _ZipCode = "";
                //string _EmpStatus = "";
                //double _Height = 0;
                //double _Weight = 0;
                //double _Pulse = 0;
                //double _PulseOX = 0;
                //double _BPSittingMin = 0;
                //double _BPSittingMax = 0;
                //double _BPStandingMin = 0;
                //double _BPStandingMax = 0;
                //double _BMI = 0;
                //double _Temperature = 0;

                ////Dim _Histories As New Collection
                ////Dim _Drugs As New Collection
                ////Dim _ICD9s As New Collection
                ////Dim _CPTs As New Collection
                ////Dim _Labs As New Collection
                ////Dim _LabModule As New gloStream.DiseaseManagement.Supporting.LabModulePatientDetails


                //Collection _FinalPatientID = new Collection();
                //Collection _PrimaryPatientID = new Collection();
                //Collection _TempPatientID = new Collection();
                //string _PrimaryINPatientID = "";

                //try
                //{
                //    //UpdateLog(" START DM - FindGuidelinesForSinglePatient");
                //    //'Application.DoEvents()
                //    if (StartCriteria != null)
                //    {
                //        StartCriteria(true);
                //    }

                //    //*********************>>>--- READ PATIENT CRITERIA CONDITION ---<<<***********************
                //    oDB.Connect(GetConnectionString);
                //    //::: GENERAL INFORMATION :::
                //    _strSQL = "select dtDOB,sGender,sRace,sMaritalStatus,sCity,sState,sZip,sEmploymentStatus from patient where npatientid = " + PatientID + " and dtDOB is not null ";
                //    oDataReader = oDB.ReadQueryRecords(_strSQL);
                //    if ((oDataReader != null))
                //    {
                //        if (oDataReader.HasRows == true)
                //        {
                //            while (oDataReader.Read())
                //            {
                //                //gstrPatientDOB = Format(dgPatient.Item(dgPatient.CurrentRowIndex, 6), "MM/dd/yyyy")
                //                //Dim nMonths As Int16
                //                //nMonths = DateDiff(DateInterval.Month, CType(gstrPatientDOB, Date), Date.Now.Date)
                //                //gstrPatientAge = nMonths \ 12 & " Yrs " & nMonths Mod 12 & " Months" ' DateDiff(DateInterval.Month, CType(gstrPatientDOB, Date), Date.Now.Date) & " Yrs"

                //                Int16 nMonths = default(Int16);
                //                nMonths = DateAndTime.DateDiff(DateInterval.Month, Convert.ToDateTime(oDataReader["dtDOB"] + ""), System.DateTime.Now.Date);
                //                _Age = Conversion.Val(nMonths / 12) + "." + System.Math.Abs(Conversion.Val(nMonths % 12));

                //                if (!Information.IsDBNull(oDataReader["sGender"]))
                //                {
                //                    _Geneder = oDataReader["sGender"] + "";
                //                }
                //                if (!Information.IsDBNull(oDataReader["sRace"]))
                //                {
                //                    _Race = oDataReader["sRace"] + "";
                //                }
                //                if (!Information.IsDBNull(oDataReader["sMaritalStatus"]))
                //                {
                //                    _MaritalStatus = oDataReader["sMaritalStatus"] + "";
                //                }
                //                if (!Information.IsDBNull(oDataReader["sCity"]))
                //                {
                //                    _City = oDataReader["sCity"] + "";
                //                }
                //                if (!Information.IsDBNull(oDataReader["sState"]))
                //                {
                //                    _State = oDataReader["sState"] + "";
                //                }
                //                if (!Information.IsDBNull(oDataReader["sZip"]))
                //                {
                //                    _ZipCode = oDataReader["sZip"] + "";
                //                }
                //                if (!Information.IsDBNull(oDataReader["sEmploymentStatus"]))
                //                {
                //                    _EmpStatus = oDataReader["sEmploymentStatus"] + "";
                //                }
                //            }
                //        }
                //    }
                //    oDataReader.Close();

                //    //::: VITAL INFORMATION :::
                //    _strSQL = "";
                //    _strSQL = "Select sHeight, dWeightinlbs, dWeightinKg, dPulsePerMinute, dPulseOx, " + " dBloodPressureSittingMin, dBloodPressureSittingMax, dBloodPressureStandingMin, " + " dBloodPressureStandingMax, dBMI, dTemperature from vitals v where dtvitaldate in (select max(dtvitaldate) from vitals v1 where npatientid=v.npatientid group by nPatientID) AND nPatientid = " + PatientID + "";

                //    oDataReader = oDB.ReadQueryRecords(_strSQL);
                //    if ((oDataReader != null))
                //    {
                //        if (oDataReader.HasRows == true)
                //        {
                //            while (oDataReader.Read())
                //            {
                //                if (!Information.IsDBNull(oDataReader["sHeight"]))
                //                {
                //                    string[] _arrPHeight = null;
                //                    double _PHtFt = 0;
                //                    double _PHtInch = 0;

                //                    _arrPHeight = GetFtInch(oDataReader["sHeight"] + "");
                //                    if ((_arrPHeight != null))
                //                    {
                //                        if ((_arrPHeight[0] != null))
                //                            _PHtFt = Conversion.Val(_arrPHeight[0]);
                //                        if ((_arrPHeight[1] != null))
                //                            _PHtInch = Conversion.Val(_arrPHeight[1]);
                //                    }

                //                    _Height = FtToMtr(_PHtFt, _PHtInch);
                //                }
                //                if (!Information.IsDBNull(oDataReader["dWeightinlbs"]))
                //                {
                //                    _Weight = Conversion.Val(oDataReader["dWeightinlbs"]);
                //                }
                //                if (!Information.IsDBNull(oDataReader["dPulsePerMinute"]))
                //                {
                //                    _Pulse = Conversion.Val(oDataReader["dPulsePerMinute"]);
                //                }
                //                if (!Information.IsDBNull(oDataReader["dPulseOx"]))
                //                {
                //                    _PulseOX = Conversion.Val(oDataReader["dPulseOx"]);
                //                }
                //                if (!Information.IsDBNull(oDataReader["dBloodPressureSittingMin"]))
                //                {
                //                    _BPSittingMin = Conversion.Val(oDataReader["dBloodPressureSittingMin"]);
                //                }
                //                if (!Information.IsDBNull(oDataReader["dBloodPressureSittingMax"]))
                //                {
                //                    _BPSittingMax = Conversion.Val(oDataReader["dBloodPressureSittingMax"]);
                //                }
                //                if (!Information.IsDBNull(oDataReader["dBloodPressureStandingMin"]))
                //                {
                //                    _BPStandingMin = Conversion.Val(oDataReader["dBloodPressureStandingMin"]);
                //                }
                //                if (!Information.IsDBNull(oDataReader["dBloodPressureStandingMax"]))
                //                {
                //                    _BPStandingMax = Conversion.Val(oDataReader["dBloodPressureStandingMax"]);
                //                }
                //                if (!Information.IsDBNull(oDataReader["dBMI"]))
                //                {
                //                    _BMI = Conversion.Val(oDataReader["dBMI"]);
                //                }
                //                if (!Information.IsDBNull(oDataReader["dTemperature"]))
                //                {
                //                    _Temperature = Conversion.Val(oDataReader["dTemperature"]);
                //                }
                //            }
                //        }
                //    }
                //    oDataReader.Close();

                //    //::: HISTORIES :::
                //    //' COMMENTED BY SUDHIR 20090302
                //    //_strSQL = " SELECT DISTINCT History_MST.nHistoryID AS nHistoryID" _
                //    //        & " FROM History INNER JOIN " _
                //    //        & " History_MST ON History.sHistoryItem = History_MST.sDescription " _
                //    //        & " WHERE History.nPatientID = " & PatientID & ""
                //    //oDataReader = oDB.ReadQueryRecords(_strSQL)
                //    //If Not oDataReader Is Nothing Then
                //    //    If oDataReader.HasRows = True Then
                //    //        While oDataReader.Read
                //    //            If Not IsDBNull(oDataReader.Item("nHistoryID")) Then
                //    //                _Histories.Add(oDataReader.Item("nHistoryID"))
                //    //            End If
                //    //        End While
                //    //    End If
                //    //End If
                //    //oDataReader.Close()

                //    //_strSQL = "SELECT sHistoryItem, sHistoryCategory FROM History WHERE nPatientID = " & PatientID & " "
                //    //oDataReader = oDB.ReadQueryRecords(_strSQL)
                //    //If Not oDataReader Is Nothing Then
                //    //    If oDataReader.HasRows = True Then
                //    //        Dim oList As myList
                //    //        While oDataReader.Read
                //    //            oList = New myList
                //    //            oList.HistoryItem = oDataReader.Item("sHistoryItem")
                //    //            oList.HistoryCategory = oDataReader.Item("sHistoryCategory")
                //    //            _Histories.Add(oList)
                //    //            oList = Nothing
                //    //        End While
                //    //    End If
                //    //    oDataReader.Close()
                //    //End If
                //    //'::: DRUGS :::
                //    //_strSQL = ""
                //    //_strSQL = " SELECT DISTINCT Drugs_MST.nDrugsID AS nDrugsID" _
                //    //        & " FROM Medication INNER JOIN Drugs_MST ON Medication.sMedication = Drugs_MST.sDrugName AND Medication.sDosage = Drugs_MST.sDosage " _
                //    //        & " WHERE Medication.nPatientID = " & PatientID & ""
                //    //oDataReader = oDB.ReadQueryRecords(_strSQL)
                //    //If Not oDataReader Is Nothing Then
                //    //    If oDataReader.HasRows = True Then
                //    //        While oDataReader.Read
                //    //            If Not IsDBNull(oDataReader.Item("nDrugsID")) Then
                //    //                _Drugs.Add(oDataReader.Item("nDrugsID"))
                //    //            End If
                //    //        End While
                //    //    End If
                //    //End If
                //    //oDataReader.Close()

                //    //'::: ICD9S :::
                //    //_strSQL = ""
                //    //_strSQL = " SELECT DISTINCT ICD9.nICD9ID AS nICD9ID " _
                //    //        & " FROM ICD9 INNER JOIN ExamICD9CPT ON ICD9.sICD9Code = ExamICD9CPT.sICD9Code AND ICD9.sDescription = ExamICD9CPT.sICD9Description " _
                //    //        & " WHERE ExamICD9CPT.nPatientID = " & PatientID & ""
                //    //oDataReader = oDB.ReadQueryRecords(_strSQL)
                //    //If Not oDataReader Is Nothing Then
                //    //    If oDataReader.HasRows = True Then
                //    //        While oDataReader.Read
                //    //            If Not IsDBNull(oDataReader.Item("nICD9ID")) Then
                //    //                _ICD9s.Add(oDataReader.Item("nICD9ID"))
                //    //            End If
                //    //        End While
                //    //    End If
                //    //End If
                //    //oDataReader.Close()

                //    //'::: CPTS :::
                //    //_strSQL = ""
                //    //_strSQL = " SELECT DISTINCT CPT_MST.nCPTID AS nCPTID " _
                //    //        & " FROM ExamICD9CPT INNER JOIN CPT_MST ON ExamICD9CPT.sCPTCode = CPT_MST.sCPTCode AND ExamICD9CPT.sCPTDescription = CPT_MST.sDescription " _
                //    //        & " WHERE ExamICD9CPT.nPatientID = " & PatientID & ""
                //    //oDataReader = oDB.ReadQueryRecords(_strSQL)
                //    //If Not oDataReader Is Nothing Then
                //    //    If oDataReader.HasRows = True Then
                //    //        While oDataReader.Read
                //    //            If Not IsDBNull(oDataReader.Item("nCPTID")) Then
                //    //                _CPTs.Add(oDataReader.Item("nCPTID"))
                //    //            End If
                //    //        End While
                //    //    End If
                //    //End If
                //    //oDataReader.Close()

                //    //'::: Radiologies :::
                //    //_strSQL = ""
                //    //_strSQL = " SELECT DISTINCT LM_Test.lm_test_ID AS lm_test_ID " _
                //    //        & " FROM LM_Orders INNER JOIN LM_Test ON LM_Orders.lm_test_ID = LM_Test.lm_test_ID " _
                //    //        & " WHERE LM_Orders.lm_Patient_ID = " & PatientID & ""
                //    //oDataReader = oDB.ReadQueryRecords(_strSQL)
                //    //If Not oDataReader Is Nothing Then
                //    //    If oDataReader.HasRows = True Then
                //    //        While oDataReader.Read
                //    //            If Not IsDBNull(oDataReader.Item("lm_test_ID")) Then
                //    //                _Labs.Add(oDataReader.Item("lm_test_ID"))
                //    //            End If
                //    //        End While
                //    //    End If
                //    //End If
                //    //oDataReader.Close()

                //    //'::: Labs :::
                //    //_strSQL = "SELECT Lab_Order_Test_ResultDtl.labotrd_OrderID, Lab_Order_Test_ResultDtl.labotrd_TestID, " _
                //    //& " Lab_Order_Test_ResultDtl.labotrd_ResultNameID, Lab_Order_Test_ResultDtl.labotrd_ResultValue " _
                //    //& " FROM Lab_Order_MST INNER JOIN Lab_Order_Test_Result ON Lab_Order_MST.labom_OrderID = Lab_Order_Test_Result.labotr_OrderID INNER JOIN Lab_Order_Test_ResultDtl ON Lab_Order_Test_Result.labotr_OrderID = Lab_Order_Test_ResultDtl.labotrd_OrderID AND " _
                //    //& " Lab_Order_Test_Result.labotr_TestID = Lab_Order_Test_ResultDtl.labotrd_TestID WHERE(Lab_Order_MST.labom_PatientID = " & PatientID & ")"

                //    //oDataReader = oDB.ReadQueryRecords(_strSQL)
                //    //If Not oDataReader Is Nothing Then
                //    //    If oDataReader.HasRows = True Then
                //    //        While oDataReader.Read
                //    //            If IsDBNull(oDataReader.Item("labotrd_TestID")) = False And IsDBNull(oDataReader.Item("labotrd_ResultNameID")) = False And IsDBNull(oDataReader.Item("labotrd_ResultValue")) = False Then
                //    //                Dim _LabModDtl As New gloStream.DiseaseManagement.Supporting.LabModulePatientDetail
                //    //                With _LabModDtl
                //    //                    If Not IsDBNull(oDataReader.Item("labotrd_OrderID")) Then
                //    //                        .OrderID = oDataReader.Item("labotrd_OrderID")
                //    //                    End If
                //    //                    .TestID = oDataReader.Item("labotrd_TestID")
                //    //                    .ResultNameID = oDataReader.Item("labotrd_ResultNameID")
                //    //                    .ResultValue = oDataReader.Item("labotrd_ResultValue")
                //    //                End With
                //    //                _LabModule.Add(_LabModDtl)
                //    //                _LabModDtl = Nothing
                //    //            End If
                //    //        End While
                //    //    End If
                //    //    oDataReader.Close()
                //    //End If


                //    //*********************>>>--- READ CRITERIA CONDITION ---<<<*******************************
                //    //connect to the database    

                //    //' ::: CHECK CRITERIAS FOR GENDER & AGE ::: 
                    Collection COL_CariteriaforSinglePatient = new Collection();
                //    string strCriteriaID = "";

                //    _strSQL = "";
                //    _strSQL = " SELECT dm_mst_Id, dm_mst_AgeMin, dm_mst_AgeMax " + " FROM dm_criteria_mst WHERE dm_mst_Gender = '" + _Geneder + "' OR  dm_mst_Gender = 'ALL'";

                //    oDataReader = oDB.ReadQueryRecords(_strSQL);
                //    if ((oDataReader != null))
                //    {
                //        if (oDataReader.HasRows == true)
                //        {
                //            while (oDataReader.Read())
                //            {
                //                if (!Information.IsDBNull(oDataReader["dm_mst_Id"]))
                //                {
                //                    if (!Information.IsDBNull(oDataReader["dm_mst_AgeMin"]) & !Information.IsDBNull(oDataReader["dm_mst_AgeMax"]))
                //                    {
                //                        if (Conversion.Val(oDataReader["dm_mst_AgeMin"]) <= _Age & Conversion.Val(oDataReader["dm_mst_AgeMax"]) >= _Age)
                //                        {
                //                            //' Check For MIN MAX Age Crteria
                //                            COL_CariteriaforSinglePatient.Add(oDataReader["dm_mst_Id"]);
                //                        }
                //                        else if (Conversion.Val(oDataReader["dm_mst_AgeMin"]) <= _Age & Conversion.Val(oDataReader["dm_mst_AgeMax"]) == 0)
                //                        {
                //                            //' Check For Only MIN Age Crteria , MAX Age = 0
                //                            COL_CariteriaforSinglePatient.Add(oDataReader["dm_mst_Id"]);
                //                        }
                //                        else if (Conversion.Val(oDataReader["dm_mst_AgeMin"]) == 0 & Conversion.Val(oDataReader["dm_mst_AgeMax"]) >= _Age)
                //                        {
                //                            //' Check For Only MAX Age Crteria , MIN Age = 0
                //                            COL_CariteriaforSinglePatient.Add(oDataReader["dm_mst_Id"]);
                //                        }
                //                        else if (Conversion.Val(oDataReader["dm_mst_AgeMin"]) <= _Age & Conversion.Val(oDataReader["dm_mst_AgeMax"]) >= _Age)
                //                        {
                //                            //' Check For No Age Crteria  MIN Age = 0 , MAX Age = 0
                //                            COL_CariteriaforSinglePatient.Add(oDataReader["dm_mst_Id"]);
                //                        }
                //                    }
                //                }
                //            }
                //        }
                //    }
                //    oDataReader.Close();

                //    if (COL_CariteriaforSinglePatient.Count > 0)
                //    {
                //        ///' :::  FOR RACE  ::: 
                //        strCriteriaID = GetIDsAsString(COL_CariteriaforSinglePatient);
                //        COL_CariteriaforSinglePatient = new Collection();

                //        _strSQL = "";
                //        _strSQL = " SELECT dm_mst_Id " + " FROM dm_criteria_mst WHERE (dm_mst_Race = '" + _Race.Trim().Replace("'", "''") + "' Or ISNULL(dm_mst_Race,'') = '') AND (dm_criteria_mst.dm_mst_Id IN (" + strCriteriaID + "))";

                //        COL_CariteriaforSinglePatient = getCriteriaIDColection(_strSQL);

                //        //oDataReader = oDB.ReadQueryRecords(_strSQL)
                //        //If Not oDataReader Is Nothing Then
                //        //    If oDataReader.HasRows = True Then
                //        //        While oDataReader.Read
                //        //            If Not IsDBNull(oDataReader.Item("dm_mst_Id")) Then
                //        //                COL_CariteriaforSinglePatient.Add(oDataReader.Item("dm_mst_Id"))
                //        //            End If
                //        //        End While
                //        //    End If
                //        //End If
                //        //oDataReader.Close()
                //    }

                //    if (COL_CariteriaforSinglePatient.Count > 0)
                //    {
                //        ///' :::  FOR Marital Status  ::: 
                //        strCriteriaID = GetIDsAsString(COL_CariteriaforSinglePatient);
                //        COL_CariteriaforSinglePatient = new Collection();

                //        _strSQL = "";
                //        _strSQL = " SELECT dm_mst_Id " + " FROM dm_criteria_mst WHERE (dm_mst_MaritalStatus = '" + _MaritalStatus.Trim().Replace("'", "''") + "' OR ISNULL(dm_mst_MaritalStatus,'') = '' )AND (dm_criteria_mst.dm_mst_Id IN (" + strCriteriaID + "))";

                //        COL_CariteriaforSinglePatient = getCriteriaIDColection(_strSQL);

                //    }

                //    if (COL_CariteriaforSinglePatient.Count > 0)
                //    {
                //        ///' :::  FOR City  ::: 
                //        strCriteriaID = GetIDsAsString(COL_CariteriaforSinglePatient);
                //        COL_CariteriaforSinglePatient = new Collection();

                //        _strSQL = "";
                //        _strSQL = " SELECT dm_mst_Id " + " FROM dm_criteria_mst WHERE (dm_mst_City = '" + _City.Trim().Replace("'", "''") + "' OR ISNULL(dm_mst_City,'') = '')  AND (dm_criteria_mst.dm_mst_Id IN (" + strCriteriaID + "))";

                //        COL_CariteriaforSinglePatient = getCriteriaIDColection(_strSQL);

                //    }

                //    if (COL_CariteriaforSinglePatient.Count > 0)
                //    {
                //        ///' :::  FOR State  ::: 
                //        strCriteriaID = GetIDsAsString(COL_CariteriaforSinglePatient);
                //        COL_CariteriaforSinglePatient = new Collection();

                //        _strSQL = "";
                //        _strSQL = " SELECT dm_mst_Id " + " FROM dm_criteria_mst WHERE (dm_mst_Status = '" + _State.Trim().Replace("'", "''") + "' OR ISNULL(dm_mst_Status,'') = '') AND (dm_criteria_mst.dm_mst_Id IN (" + strCriteriaID + "))";

                //        COL_CariteriaforSinglePatient = getCriteriaIDColection(_strSQL);

                //    }

                //    if (COL_CariteriaforSinglePatient.Count > 0)
                //    {
                //        ///' :::  FOR _ZipCode  ::: 
                //        strCriteriaID = GetIDsAsString(COL_CariteriaforSinglePatient);
                //        COL_CariteriaforSinglePatient = new Collection();

                //        _strSQL = "";
                //        _strSQL = " SELECT dm_mst_Id " + " FROM dm_criteria_mst WHERE ( dm_mst_Zip = '" + _ZipCode.Trim().Replace("'", "''") + "' OR ISNULL(dm_mst_Zip,'') = '')  AND (dm_criteria_mst.dm_mst_Id IN (" + strCriteriaID + "))";

                //        COL_CariteriaforSinglePatient = getCriteriaIDColection(_strSQL);
                //    }

                //    if (COL_CariteriaforSinglePatient.Count > 0)
                //    {
                //        ///' :::  FOR EmpStatus  ::: 
                //        strCriteriaID = GetIDsAsString(COL_CariteriaforSinglePatient);
                //        COL_CariteriaforSinglePatient = new Collection();

                //        _strSQL = "";
                //        _strSQL = " SELECT dm_mst_Id " + " FROM dm_criteria_mst WHERE (dm_mst_EmplyementStatus = '" + _EmpStatus.Trim().Replace("'", "''") + "' OR ISNULL(dm_mst_EmplyementStatus,'') = '')  AND (dm_criteria_mst.dm_mst_Id IN (" + strCriteriaID + "))";

                //        COL_CariteriaforSinglePatient = getCriteriaIDColection(_strSQL);
                //    }


                //    if (COL_CariteriaforSinglePatient.Count > 0)
                //    {
                //        ///' :::  FOR EmpStatus  ::: 
                //        strCriteriaID = GetIDsAsString(COL_CariteriaforSinglePatient);
                //        COL_CariteriaforSinglePatient = new Collection();

                //        _strSQL = "";
                //        _strSQL = " SELECT dm_mst_Id " + " FROM dm_criteria_mst WHERE (dm_mst_EmplyementStatus = '" + _EmpStatus.Trim().Replace("'", "''") + "' OR ISNULL(dm_mst_EmplyementStatus,'') = '')  AND (dm_criteria_mst.dm_mst_Id IN (" + strCriteriaID + "))";

                //        COL_CariteriaforSinglePatient = getCriteriaIDColection(_strSQL);

                //        //oDataReader = oDB.ReadQueryRecords(_strSQL)
                //        //If Not oDataReader Is Nothing Then
                //        //    If oDataReader.HasRows = True Then
                //        //        While oDataReader.Read
                //        //            If Not IsDBNull(oDataReader.Item("dm_mst_Id")) Then
                //        //                COL_CariteriaforSinglePatient.Add(oDataReader.Item("dm_mst_Id"))
                //        //            End If
                //        //        End While
                //        //    End If
                //        //End If
                //        //oDataReader.Close()
                //    }

                //    ///'Dim _Height As Double = 0
                //    ///'Dim _Weight As Double = 0
                //    ///'Dim _Pulse As Double = 0
                //    ///'Dim _PulseOX As Double = 0
                //    ///'Dim _BPSittingMin As Double = 0
                //    ///'Dim _BPSittingMax As Double = 0
                //    ///'Dim _BPStandingMin As Double = 0
                //    ///'Dim _BPStandingMax As Double = 0
                //    ///'Dim _BMI As Double = 0
                //    ///'Dim _Temperature As Double = 0
                //    if (COL_CariteriaforSinglePatient.Count > 0)
                //    {
                //        ///' :::  FOR Height  ::: 
                //        strCriteriaID = GetIDsAsString(COL_CariteriaforSinglePatient);
                //        COL_CariteriaforSinglePatient = new Collection();

                //        _strSQL = "";
                //        _strSQL = " SELECT dm_mst_Id " + " FROM dm_criteria_mst WHERE (dm_mst_EmplyementStatus = '" + _EmpStatus.Trim().Replace("'", "''") + "' OR ISNULL(dm_mst_EmplyementStatus,'') = '')  AND (dm_criteria_mst.dm_mst_Id IN (" + strCriteriaID + "))";

                //        COL_CariteriaforSinglePatient = getCriteriaIDColection(_strSQL);

                //    }
                //    // RaiseEvent ProcessCriteria("Start Findings")

                //    if (COL_CariteriaforSinglePatient.Count > 0)
                //    {
                //        for (int i = COL_CariteriaforSinglePatient.Count; i >= 1; i += -1)
                //        {
                //            if (FindGuidelinesForSinglePatientCriteria(COL_CariteriaforSinglePatient[i], PatientID) == false)
                //            {
                //                COL_CariteriaforSinglePatient.Remove(i);
                //            }
                //        }
                //    }

                //    //*********************>>>--- END READ CRITERIA CONDITION ---<<<*******************************

                //    //*********************>>>--- READ PATIENT SPECIFIC CRITERIA CONDITION ---<<<*******************************
                //    //' To get the Patient Specific criteria conditions
                //    Collection Col_PatientSpecific = new Collection();

                //    _strSQL = "";
                //    if (PatientID > 0)
                //    {
                //        _strSQL = "SELECT dm_mst_Id FROM DM_Criteria_MST WHERE dm_mst_PatientID = " + PatientID + "";
                //        Col_PatientSpecific = getCriteriaIDColection(_strSQL);
                //    }


                //    if ((Col_PatientSpecific == null) == false)
                //    {
                //        if (Col_PatientSpecific.Count > 0)
                //        {
                //            for (int i = 1; i <= Col_PatientSpecific.Count; i++)
                //            {
                //                COL_CariteriaforSinglePatient.Add(Col_PatientSpecific[i]);
                //            }
                //        }
                //    }

                //    //*********************>>>--- END READ PATIENT SPECIFIC CRITERIA CONDITION ---<<<*******************************

                //    UpdateLog(" END DM - FindGuidelinesForSinglePatient");

                //    return COL_CariteriaforSinglePatient;
                //}
                //catch (SqlException ex)
                //{
                //    MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    UpdateLog("clsDiseaseManagement -- FindGuidelinesForSinglePatient -- " + ex.ToString());
                //}
                //catch (Exception ex)
                //{
                //    UpdateLog("clsDiseaseManagement -- FindGuidelinesForSinglePatient -- " + ex.ToString());
                //    MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                //}
                //finally
                //{
                //    //oDataReader.Close()
                //    oDB.Disconnect();
                //    oDB.Dispose();
                //    oDB = null;
                //}
                return COL_CariteriaforSinglePatient;
            }


            public Supporting.PatientDetail GetPatientDetails(Int64 PatientID)
			{

                //gloStream.gloDataBase.gloDataBase oDB = new gloStream.gloDataBase.gloDataBase();
                //DataTable dt = new DataTable();
                Supporting.PatientDetail oPatientDetails = new Supporting.PatientDetail();
                //Supporting.OtherDetail oOtherDetail = new Supporting.OtherDetail();
                //string strQuery = "";
                //Int16 i = default(Int16);
                //oDB.Connect(GetConnectionString);

                //try {
                //    //' Get Patient's Demographic Details
                //    //line commented and modified by  dipak20091216 to handle exception
                //    //strQuery = "Select  sPatientCode, ISNULL(sFirstName,'') + SPACE(1) + ISNULL(sMiddleName,'') + SPACE(1) + ISNULL(sLastName,'') AS PatientName ,dtDOB, sGender FROM Patient WHERE nPatientID = " & PatientID & ""
                //    strQuery = "Select  sPatientCode, ISNULL(sFirstName,'') + SPACE(1) + ISNULL(sMiddleName,'') + SPACE(1) + ISNULL(sLastName,'') AS PatientName ,dtDOB, ISNULL(sGender,0) as sGender FROM Patient WHERE nPatientID = " + PatientID + "";
                //    dt = oDB.ReadQueryDataTable(strQuery);
                //    if ((dt == null) == false) {
                //        if (dt.Rows.Count > 0) {
                //            var _with21 = dt.Rows[0];
                //            oPatientDetails.PatientCode = _with21.Item("sPatientCode");
                //            oPatientDetails.PatientName = _with21.Item("PatientName");
                //            oPatientDetails.Age = GetPatientAgeinYrs(_with21.Item("dtDOB"));
                //            oPatientDetails.Gender = _with21.Item("sGender");
                //        }
                //    }

                //    //' Get Patient's Latest Vital Details
                //    strQuery = "Select TOP 1 nVitalID, nVisitID, dtVitalDate, ISNULL(sHeight,'') AS sHeight, ISNULL(dWeightinlbs,0) AS dWeightinlbs, ISNULL(dBMI,0) AS dBMI, ISNULL(dWeightinKg,0) AS dWeightinKg, ISNULL(dTemperature,0) AS dTemperature, ISNULL(dRespiratoryRate,0) AS dRespiratoryRate, ISNULL(dPulsePerMinute,0) AS dPulsePerMinute, ISNULL(dPulseOx,0) AS dPulseOx, ISNULL(dBloodPressureSittingMin,0) AS dBloodPressureSittingMin, ISNULL(dBloodPressureSittingMax,0) AS dBloodPressureSittingMax, ISNULL(dBloodPressureStandingMin,0) AS dBloodPressureStandingMin, ISNULL(dBloodPressureStandingMax,0) AS dBloodPressureStandingMax  FROM Vitals WHERE nPatientID = " + PatientID + " Order by  dtVitalDate DESC ";
                //    dt = oDB.ReadQueryDataTable(strQuery);

                //    if ((dt == null) == false) {

                //        if (dt.Rows.Count > 0) {
                //            var _with22 = dt.Rows[0];
                //            oPatientDetails.VitalDate = _with22.Item("dtVitalDate");
                //            oPatientDetails.Height = _with22.Item("sHeight");
                //            oPatientDetails.WeightInlbs = _with22.Item("dWeightinlbs");
                //            oPatientDetails.BMI = _with22.Item("dBMI");
                //            oPatientDetails.TempratureInF = _with22.Item("dTemperature");
                //            oPatientDetails.Pulse = _with22.Item("dPulsePerMinute");
                //            oPatientDetails.PulseOX = _with22.Item("dPulseOx");
                //            oPatientDetails.BPSittingMinimum = _with22.Item("dBloodPressureSittingMin");
                //            oPatientDetails.BPSittingMaximum = _with22.Item("dBloodPressureSittingMax");
                //            oPatientDetails.BPStandingMinimum = _with22.Item("dBloodPressureStandingMin");
                //            oPatientDetails.BPStandingMaximum = _with22.Item("dBloodPressureStandingMax");
                //        }
                //    }

                //    //' Get Patient's Latest History Details 
                //    string strVisitID = "";
                //    Int64 nVisitID = 0;
                //    //' Get Latest VisitID against which the History Is Entered
                //    strQuery = "SELECT History.nVisitID AS nVisitID  " + " FROM\tHistory INNER JOIN\tVisits ON History.nVisitID = Visits.nVisitID WHERE History.nPatientID= " + PatientID + " ORDER BY dtVisitDate DESC ";
                //    strVisitID = oDB.ExecuteQueryScaler(strQuery);
                //    if ((!string.IsNullOrEmpty(strVisitID))) {
                //        nVisitID = Convert.ToInt64(strVisitID);
                //    } else {
                //        nVisitID = 0;
                //    }

                //    if (nVisitID != 0) {
                //        //' Get History For the latest Visit
                //        strQuery = "SELECT  ISNULL(sHistoryCategory,'') AS sHistoryCategory,ISNULL(sHistoryItem,'') AS sHistoryItem, ISNULL(sComments,'') AS sComments, dtVisitDate, History.nVisitID AS nVisitID  " + " FROM\tHistory INNER JOIN\tVisits ON History.nVisitID = Visits.nVisitID WHERE History.nVisitID= " + nVisitID + " ORDER BY dtVisitDate DESC ";
                //        dt = oDB.ReadQueryDataTable(strQuery);

                //        if ((dt == null) == false) {
                //            for (i = 0; i <= dt.Rows.Count - 1; i++) {
                //                oOtherDetail = new Supporting.OtherDetail();
                //                var _with23 = dt.Rows[i];
                //                oOtherDetail.CategoryName = _with23.Item("sHistoryCategory");
                //                oOtherDetail.ItemName = _with23.Item("sHistoryItem");
                //                oOtherDetail.ItemDate = _with23.Item("dtVisitDate");
                //                oOtherDetail.ItemID = _with23.Item("nVisitID");
                //                oOtherDetail.DetailType = gloStream.DiseaseManagement.Supporting.enumDetailType.History;

                //                oPatientDetails.OtherDetails.Add(ref oOtherDetail);
                //            }
                //        }
                //    }


                //    //'Medication
                //    //' Get Patient's Latest Medication Details 
                //    strVisitID = "";
                //    nVisitID = 0;
                //    //' Get Latest VisitID against which the Medication Is Entered
                //    strQuery = "SELECT ISNULL(Medication.nVisitID,0) AS nVisitID  " + " FROM\tMedication INNER JOIN\tVisits ON Medication.nVisitID = Visits.nVisitID WHERE Medication.nPatientID= " + PatientID + " ORDER BY dtVisitDate DESC ";
                //    strVisitID = oDB.ExecuteQueryScaler(strQuery);
                //    if ((!string.IsNullOrEmpty(strVisitID))) {
                //        nVisitID = Convert.ToInt64(strVisitID);
                //    } else {
                //        nVisitID = 0;
                //    }

                //    if (nVisitID != 0) {
                //        //fill the Patient's Drug information using the Medication table
                //        strQuery = "SELECT distinct LTRIM(RTRIM(ISNULL(Medication.sMedication,''))) AS sMedication ,LTRIM(RTRIM(ISNULL(Medication.sDosage,''))) AS sDosage, LTRIM(RTRIM(ISNULL(Medication.sRoute,''))) AS sRoute , dtMedicationDate,ISNULL(sDrugForm,'') as sDrugForm FROM Medication WHERE Medication.nVisitID = " + nVisitID + " AND Medication.nPatientID = " + PatientID + "";

                //        dt = oDB.ReadQueryDataTable(strQuery);

                //        if ((dt == null) == false) {
                //            for (i = 0; i <= dt.Rows.Count - 1; i++) {
                //                oOtherDetail = new Supporting.OtherDetail();
                //                var _with24 = dt.Rows[i];
                //                oOtherDetail.ItemID = 0;
                //                oOtherDetail.ItemDate = _with24.Item("dtMedicationDate");
                //                oOtherDetail.CategoryName = _with24.Item("sMedication");
                //                //oOtherDetail.ItemName = .Item("sDosage")
                //                oOtherDetail.ItemName = _with24.Item("sDrugForm");
                //                oOtherDetail.DetailType = gloStream.DiseaseManagement.Supporting.enumDetailType.Medication;
                //                oPatientDetails.OtherDetails.Add( ref oOtherDetail);
                //            }
                //        }
                //    }
                //    //'

                //    //' Get Patient's Latest Labs Details 
                //    string strLabID = "";
                //    Int64 nLabID = 0;
                //    //' Get Latest  Labs Date against which the Labs Is Entered
                //    strQuery = "SELECT ISNULL(labom_OrderID,0) AS labom_OrderID FROM Lab_Order_MST WHERE Lab_Order_MST.labom_PatientID= " + PatientID + " ORDER BY labom_TransactionDate DESC ";
                //    strLabID = oDB.ExecuteQueryScaler(strQuery);
                //    if ((!string.IsNullOrEmpty(strLabID))) {
                //        nLabID = Convert.ToInt64(strLabID);
                //    } else {
                //        nLabID = 0;
                //    }

                //    if (nLabID != 0) {
                //        //' Get Labs For the latest Labs Date
                //        strQuery = "SELECT DISTINCT  Lab_Order_MST.labom_OrderNoPrefix, Lab_Order_MST.labom_OrderNoID, ISNULL(Lab_Test_Mst.labtm_Name,'') AS labtm_Name, Lab_Order_TestDtl.labotd_TestID, " + " Lab_Test_Mst.labtm_Code, Lab_Order_MST.labom_TransactionDate, Lab_Order_MST.labom_VisitID, Lab_Order_MST.labom_ProviderID, " + " ISNULL(Lab_Order_Test_ResultDtl.labotrd_ResultName,'') AS labotrd_ResultName, ISNULL(Lab_Order_Test_ResultDtl.labotrd_ResultValue,'') AS labotrd_ResultValue, Lab_Order_Test_ResultDtl.labotrd_ResultUnit " + " FROM         Lab_Order_TestDtl INNER JOIN " + " Lab_Order_MST ON Lab_Order_TestDtl.labotd_OrderID = Lab_Order_MST.labom_OrderID INNER JOIN " + " Lab_Test_Mst ON Lab_Order_TestDtl.labotd_TestID = Lab_Test_Mst.labtm_ID LEFT OUTER JOIN " + " Lab_Order_Test_ResultDtl ON Lab_Order_TestDtl.labotd_OrderID = Lab_Order_Test_ResultDtl.labotrd_OrderID AND  " + " Lab_Order_TestDtl.labotd_TestID = Lab_Order_Test_ResultDtl.labotrd_TestID " + " WHERE Lab_Order_MST.labom_OrderID = " + nLabID + " ";
                //        dt = oDB.ReadQueryDataTable(strQuery);

                //        if ((dt == null) == false) {
                //            for (i = 0; i <= dt.Rows.Count - 1; i++) {
                //                oOtherDetail = new Supporting.OtherDetail();
                //                var _with25 = dt.Rows[i];
                //                oOtherDetail.ItemID = nLabID;
                //                oOtherDetail.ItemDate = _with25.Item("labom_TransactionDate");
                //                oOtherDetail.CategoryName = _with25.Item("labtm_Name");
                //                oOtherDetail.ItemName = _with25.Item("labotrd_ResultName");
                //                oOtherDetail.Result1 = _with25.Item("labotrd_ResultValue");
                //                oOtherDetail.DetailType = gloStream.DiseaseManagement.Supporting.enumDetailType.Lab;
                //                oPatientDetails.OtherDetails.Add( ref oOtherDetail);
                //            }
                //        }
                //    }


                //    //' Get Patient's Latest Order Details 

                //    //' Get Latest Orders Date against which the Labs Is Entered

                //    strQuery = "SELECT TOP 1 LM_Orders.lm_Visit_ID, LM_Orders.lm_OrderDate FROM   LM_Orders WHERE lm_Patient_ID = " + PatientID + " ORDER BY lm_OrderDate DESC ";
                //    DataTable dtOrders = new DataTable();
                //    dtOrders = oDB.ReadQueryDataTable(strQuery);
                //    Int64 VisitID = default(Int64);
                //    System.DateTime OrderDate = default(System.DateTime);
                //    if ((dtOrders == null) == false) {
                //        if (dtOrders.Rows.Count > 0) {
                //            VisitID = Convert.ToInt64(dtOrders.Rows[0]["lm_Visit_ID"]);
                //            OrderDate = Convert.ToDateTime(dtOrders.Rows[0]["lm_OrderDate"]);

                //            //' Get Orders For the latest Order DateTime
                //            //strQuery = "SELECT     ISNULL(LM_Category.lm_category_Description,'') AS lm_category_Description, ISNULL(LM_Test.lm_test_Name,lm_test_Name ) AS lm_test_Name, LM_Orders.lm_Visit_ID, LM_Orders.lm_Patient_ID, LM_Orders.lm_OrderDate " _
                //            //        & " FROM         LM_Orders INNER JOIN " _
                //            //        & " LM_Test ON LM_Orders.lm_test_ID = LM_Test.lm_test_ID INNER JOIN " _
                //            //        & " LM_Category ON LM_Test.lm_test_CategoryID = LM_Category.lm_category_ID " _
                //            //        & " WHERE lm_Visit_ID = " & VisitID & " AND lm_OrderDate = '" & OrderDate & "'"

                //            strQuery = " SELECT ISNULL(lm_sCategoryName,'') AS lm_sCategoryName, ISNULL(lm_sTestName,'') AS lm_sTestName, lm_OrderDate, " + " ISNULL(lm_sGroupName,'') AS lm_sGroupName FROM LM_Orders WHERE lm_Visit_ID = " + VisitID + " AND lm_OrderDate = '" + OrderDate + "'";

                //            dt = oDB.ReadQueryDataTable(strQuery);

                //            if ((dt == null) == false) {
                //                for (i = 0; i <= dt.Rows.Count - 1; i++) {
                //                    oOtherDetail = new Supporting.OtherDetail();
                //                    var _with26 = dt.Rows[i];
                //                    oOtherDetail.ItemID = VisitID;
                //                    oOtherDetail.ItemDate = _with26.Item("lm_OrderDate");
                //                    oOtherDetail.CategoryName = _with26.Item("lm_sCategoryName");
                //                    oOtherDetail.OperatorName = _with26.Item("lm_sGroupName");
                //                    oOtherDetail.ItemName = _with26.Item("lm_sTestName");
                //                    oOtherDetail.DetailType = gloStream.DiseaseManagement.Supporting.enumDetailType.Order;
                //                    oPatientDetails.OtherDetails.Add( ref oOtherDetail);
                //                }
                //            }
                //        }
                //    }
                //    dtOrders.Dispose();
                //    dtOrders = null;

                //    //' ICD9 ''
                //    strQuery = "SELECT DISTINCT sICD9Code, LTRIM(RTRIM(sICD9Description)) AS sICD9Description FROM ExamICD9CPT WHERE nPatientID = " + PatientID + "";
                //    DataTable dtICD9 = new DataTable();
                //    dtICD9 = oDB.ReadQueryDataTable(strQuery);
                //    for (int iICD9 = 0; iICD9 <= dtICD9.Rows.Count - 1; iICD9++) {
                //        oOtherDetail = new Supporting.OtherDetail();
                //        oOtherDetail.CategoryName = dtICD9.Rows[iICD9]["sICD9Code"];
                //        oOtherDetail.ItemName = dtICD9.Rows[iICD9]["sICD9Description"];
                //        oOtherDetail.DetailType = gloStream.DiseaseManagement.Supporting.enumDetailType.ICD9;
                //        oPatientDetails.OtherDetails.Add(ref oOtherDetail);
                //    }
                //    dtICD9.Dispose();
                //    dtICD9 = null;

                //    //' CPT ''
                //    strQuery = "SELECT DISTINCT sCPTcode, LTRIM(RTRIM(sCPTDescription)) AS sCPTDescription FROM ExamICD9CPT WHERE nPatientID = " + PatientID + "";
                //    DataTable dtCPT = new DataTable();
                //    dtCPT = oDB.ReadQueryDataTable(strQuery);
                //    for (int iCPT = 0; iCPT <= dtCPT.Rows.Count - 1; iCPT++) {
                //        oOtherDetail = new Supporting.OtherDetail();
                //        oOtherDetail.CategoryName = dtCPT.Rows[iCPT]["sCPTcode"];
                //        oOtherDetail.ItemName = dtCPT.Rows[iCPT]["sCPTDescription"];
                //        oOtherDetail.DetailType = gloStream.DiseaseManagement.Supporting.enumDetailType.CPT;
                //        oPatientDetails.OtherDetails.Add(ref oOtherDetail);
                //    }
                //    dtCPT.Dispose();
                //    dtCPT = null;
                //    //'Problemlist chetan added 
                //    strQuery = "SELECT ISNULL(sCheifComplaint,'') AS sCheifComplaint FROM PROBLEMLIST WHERE nPatientID = " + PatientID + "";
                //    DataTable dtProbList = null;
                //    dtProbList = oDB.ReadQueryDataTable(strQuery);
                //    if ((dtProbList != null)) {
                //        for (int iProbList = 0; iProbList <= dtProbList.Rows.Count - 1; iProbList++) {
                //            oOtherDetail = new Supporting.OtherDetail();
                //            oOtherDetail.ItemName = dtProbList.Rows[iProbList]["sCheifComplaint"];
                //            oOtherDetail.CategoryName = "ProblemList";
                //            oOtherDetail.DetailType = gloStream.DiseaseManagement.Supporting.enumDetailType.Problemlist;
                //            oPatientDetails.OtherDetails.Add(ref oOtherDetail);
                //        }
                //    }
                //    dtProbList.Dispose();
                //    dtProbList = null;
                //    dt.Dispose();
                //    dt = null;


                //} catch (Exception ex) {
                //    MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                //} finally {
                //    oDB.Disconnect();
                //    oDB.Dispose();
                //    oDB = null;
                //}

				return oPatientDetails;

			}

            public bool FindGuidelinesForSinglePatientCriteria(long CriteriaID, long PatientID)
            {
                bool functionReturnValue = false;
                Supporting.Criteria oCriteria = null;
                Supporting.PatientDetail oPatientDetail = null;

                try
                {
                    //Dim _CriteriaName As String = GetCriteriaName(CriteriaID)
                    oCriteria = GetCriteria(CriteriaID, 0);

                    if ((oCriteria == null) == true)
                    {
                        return false;
                        //return functionReturnValue;
                    }

                    oPatientDetail = GetPatientDetails(PatientID);

                    //'AGE
                    if (!(oPatientDetail.Age > oCriteria.AgeMinimum & oPatientDetail.Age < oCriteria.AgeMaximum))
                    {
                        return false;
                    }

                    //'GENDER
                    if (!(oPatientDetail.Gender == oCriteria.Gender | oCriteria.Gender == "All"))
                    {
                        return false;
                    }


                    //'HEIGHT
                    decimal CriteriaHeightMax = FtToMtr(oCriteria.HeightMaximum);
                    decimal CriteriaHeightMin = FtToMtr(oCriteria.HeightMinimum);

                    if (CriteriaHeightMax > 0 & CriteriaHeightMin > 0 & !string.IsNullOrEmpty(oPatientDetail.Height))
                    {
                        decimal PatientHeight = FtToMtr(oPatientDetail.Height);
                        if (!(PatientHeight > CriteriaHeightMin & PatientHeight < CriteriaHeightMax))
                        {
                            return false;
                        }
                    }

                    //'WEIGHT
                    if (oCriteria.WeightMinimum > 0 & oCriteria.WeightMaximum > 0)
                    {
                        if (!(oPatientDetail.WeightInlbs > oCriteria.WeightMinimum & oPatientDetail.WeightInlbs < oCriteria.WeightMaximum))
                        {
                            return false;
                        }
                    }

                    //'PULSE
                    if (oCriteria.PulseMinimum > 0 & oCriteria.PulseMaximum > 0)
                    {
                        if (!(oPatientDetail.Pulse > oCriteria.PulseMinimum & oPatientDetail.Pulse < oCriteria.PulseMaximum))
                        {
                            return false;
                        }
                    }

                    //'PULSE_OX
                    if (oCriteria.PulseOXMinimum > 0 & oCriteria.PulseOXMaximum > 0)
                    {
                        if (!(oPatientDetail.PulseOX > oCriteria.PulseOXMinimum & oPatientDetail.PulseOX < oCriteria.PulseOXMaximum))
                        {
                            return false;
                        }
                    }

                    //'BP SITTING MAX
                    if (oCriteria.BPSittingMaximum > 0)
                    {
                        if (!(oPatientDetail.BPSittingMaximum == oCriteria.BPSittingMaximum))
                        {
                            return false;
                        }
                    }

                    //'BP SITTING MIN
                    if (oCriteria.BPSittingMinimum > 0)
                    {
                        if (!(oPatientDetail.BPSittingMinimum == oCriteria.BPSittingMinimum))
                        {
                            return false;
                        }
                    }

                    //'BP STANDIN MAX
                    if (oCriteria.BPStandingMaximum > 0)
                    {
                        if (!(oPatientDetail.BPStandingMaximum == oCriteria.BPStandingMaximum))
                        {
                            return false;
                        }
                    }

                    //'BP STANDIN MIN
                    if (oCriteria.BPStandingMinimum > 0)
                    {
                        if (!(oPatientDetail.BPStandingMinimum == oCriteria.BPStandingMinimum))
                        {
                            return false;
                        }
                    }

                    //'BMI
                    if (oCriteria.BMIMinimum > 0 & oCriteria.BMIMaximum > 0)
                    {
                        if (!(oPatientDetail.BMI > oCriteria.BMIMinimum & oPatientDetail.BMI < oCriteria.BMIMaximum))
                        {
                            return false;
                        }
                    }

                    //' TEMPERATURE ''
                    if (oCriteria.TempratureMinumum > 0 & oCriteria.TempratureMaximum > 0)
                    {
                        if (!(oPatientDetail.TempratureInF > oCriteria.TempratureMinumum & oPatientDetail.TempratureInF < oCriteria.TempratureMaximum))
                        {
                            return false;
                        }
                    }


                    //'OTHER DETAILS
                    int _MatchCounter = 0;
                    for (int iPatDetail = 1; iPatDetail <= oPatientDetail.OtherDetails.Count; iPatDetail++)
                    {
                        for (int iCriteria = 1; iCriteria <= oCriteria.OtherDetails.Count; iCriteria++)
                        {
                            if (IsOtherDetailSame(oPatientDetail.OtherDetails[iPatDetail], oCriteria.OtherDetails[iCriteria]))
                            {
                                _MatchCounter = _MatchCounter + 1;
                                break; // TODO: might not be correct. Was : Exit For
                                //' SUDHIR 20091223 '' LOGIC AS PER 2.7.3 '' ALL CRITERIA SHOULD MATCH FOR ALERT ''

                                //' Any of the Criteria Matches then Return TRUE
                                //'Return True '' 20090812 -- Logic Changed -
                            }
                        }
                    }

                    //' SUDHIR 20091223 '' LOGIC AS PER 2.7.3 '' ALL CRITERIA SHOULD MATCH FOR ALERT ''
                    //' If All Criterias of Patient & DM are Matching then Return TRUE
                    if (!(_MatchCounter == oCriteria.OtherDetails.Count))
                    {
                        return false;
                    }

                    //' ALL CRITERIA SATISFIED ''
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (oCriteria != null)
                    {
                        
                        oCriteria = null;
                    }
                    if (oPatientDetail != null)
                    {
                        
                        oPatientDetail = null;
                    }
                }
                return functionReturnValue;
            }

            private bool IsOtherDetailSame(Supporting.OtherDetail PatientDetail, Supporting.OtherDetail CriteriaDetail)
            {
                if (PatientDetail.DetailType != CriteriaDetail.DetailType)
                {
                    return false;
                }
                if (PatientDetail.ItemName.ToLower().Trim() != CriteriaDetail.ItemName.ToLower().Trim())
                {
                    return false;
                }
                if (PatientDetail.CategoryName.ToLower().Trim() != CriteriaDetail.CategoryName.ToLower().Trim())
                {
                    return false;
                }
                //' ONLY FOR ORDER ''
                if (CriteriaDetail.OperatorName == PatientDetail.OperatorName & CriteriaDetail.DetailType == gloStream.DiseaseManagement.Supporting.enumDetailType.Order)
                {
                    return true;
                }
                if (!string.IsNullOrEmpty(CriteriaDetail.OperatorName))
                {
                    if (CriteriaDetail.OperatorName == "Greater Than")
                    {
                        //if (PatientDetail.Result1 > CriteriaDetail.Result1)
                        //{
                        //    return true;
                        //}
                    }
                    else if (CriteriaDetail.OperatorName == "Less Than")
                    {
                        //if (PatientDetail.Result1 < CriteriaDetail.Result1)
                        //{
                        //    return true;
                        //}
                    }
                    else if (CriteriaDetail.OperatorName == "Between")
                    {
                        //if (PatientDetail.Result1 > CriteriaDetail.Result1 & PatientDetail.Result2 < CriteriaDetail.Result2)
                        //{
                        //    return true;
                        //}
                    }
                }
                return true;
            }

            private string GetIDsAsString(Collection COL)
            {
                string strCol = "";
                for (int i = 1; i <= COL.Count; i++)
                {
                    if (string.IsNullOrEmpty(strCol))
                    {
                        strCol = COL[i].ToString();
                    }
                    else
                    {
                        strCol = strCol + "," + COL[i];
                    }
                }
                return strCol;
            }

            private Collection getCriteriaIDColection(string strSQL)
            {
                //gloDataBase.gloDataBase oDB = new gloDataBase.gloDataBase();
                //SqlDataReader oDataReader = null;
                Collection COLECTION = new Collection();
                //try
                //{
                //    oDB.Connect(GetConnectionString);
                //    oDataReader = oDB.ReadQueryRecords(strSQL);
                //    if ((oDataReader != null))
                //    {
                //        if (oDataReader.HasRows == true)
                //        {
                //            while (oDataReader.Read())
                //            {
                //                if (!Information.IsDBNull(oDataReader["dm_mst_Id"]))
                //                {
                //                    COLECTION.Add(oDataReader["dm_mst_Id"]);
                //                }
                //            }
                //        }
                //    }
                //    oDataReader.Close();
                //    oDB.Disconnect();
                //    oDB.Dispose();
                //    oDB = null;

                //    return COLECTION;

                //}
                //catch (SqlException ex)
                //{
                //    MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    UpdateLog("clsDiseaseManagement -- getCriteriaIDColection -- " + ex.ToString());
                //}
                //catch (Exception ex)
                //{
                //    UpdateLog("clsDiseaseManagement -- getCriteriaIDColection -- " + ex.ToString());
                //    MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                //}
                //finally
                //{
                //}
                return COLECTION;
            }


            public bool IsExists(string oCriteriaName, bool blnModify, Int64 m_CriteriaId)
            {
                //criteria master name exists
                string _strSQL = "";
                //gloStream.gloDataBase.gloDataBase oDB = new gloStream.gloDataBase.gloDataBase();
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(clsGeneral.EMRConnectionString);
                System.Data.SqlClient.SqlDataReader oDataReader = null;
                bool _Result = false;


                try
                {
                    //connect to the database
                    oDB.Connect(false);
                    //set the query string

                    //Added below condition to check duplicate record when modify the DM-Criteria
                    if (blnModify == false)
                    {
                        _strSQL = "SELECT dm_mst_Id,dm_mst_CriteriaName FROM DM_Criteria_MST where dm_mst_patientID = 0 and dm_mst_CriteriaName = '" + oCriteriaName + "'";
                    }
                    else
                    {
                        //_strSQL = "SELECT dm_mst_Id,dm_mst_CriteriaName FROM DM_Criteria_MST where dm_mst_CriteriaName = (select case when lower ('" & oCriteriaName & "') = (SELECT lower (dm_mst_CriteriaName) FROM DM_Criteria_MST where dm_mst_Id=" & m_CriteriaId & ") then '' else  '" & oCriteriaName & "' end )"
                        _strSQL = "select dm_mst_Id,dm_mst_CriteriaName from DM_Criteria_MST where dm_mst_patientID = 0 and lower(dm_mst_CriteriaName)= lower ('" + oCriteriaName + "') and  dm_mst_Id <> " + m_CriteriaId;
                    }
                    //execute the query and return a datareader
                    oDB.Retrive_Query(_strSQL,out oDataReader);

                    //check if there is any data in the datareader
                    if ((oDataReader != null))
                    {
                        if (oDataReader.HasRows == true)
                        {
                            while (oDataReader.Read())
                            {
                                if (!Information.IsDBNull(oDataReader["dm_mst_CriteriaName"]))
                                {
                                    //if the criteria name matches a name in the table then return true
                                    if (Strings.LCase(oDataReader["dm_mst_CriteriaName"].ToString()) == Strings.LCase(oCriteriaName))
                                    {
                                        _Result = true;
                                        break; // TODO: might not be correct. Was : Exit While
                                    }
                                }
                            }
                        }
                        oDataReader.Close();
                    }

                    return _Result;
                }
                catch //(Exception ex)
                {
                    //UpdateLog("clsDiseaseManagement -- IsExists -- " + ex.ToString());
                    //MessageBox.Show(ex.ToString(), clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                    oDB = null;
                }
                return _Result;
            }

            public bool IsDelete(string oCriteriaName)
            {
                //// REMARK

                string _strSQL = "";
                //gloStream.gloDataBase.gloDataBase oDB = new gloStream.gloDataBase.gloDataBase();
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(clsGeneral.EMRConnectionString);
                //System.Data.SqlClient.SqlDataReader oDataReader = null;
                long CriteriaID = 0;
                bool _Result = true;
                long _Count = 0;

                SqlConnection Conn = null;
                SqlCommand cmd = null;
                try
                {
                    //connect to the database
                    oDB.Connect(false);
                    //extract the criteria id from the table for the given criteria name
                    _strSQL = "SELECT dm_mst_Id FROM DM_Criteria_MST where dm_mst_CriteriaName = '" + oCriteriaName + "'";
                    cmd = new SqlCommand(_strSQL, Conn);

                    CriteriaID = Convert.ToInt64(cmd.ExecuteScalar());
                    //Val(oDB.ExecuteQueryScaler(_strSQL))
                    Conn.Close();
                    Conn.Dispose();
                    Conn = null;
                    //set the query string
                    _strSQL = "SELECT COUNT(DM_TransId) FROM DM_Patient where DM_nCriteriaID =" + CriteriaID;
                    //execute the query and return a datareader
                    _Count = Convert.ToInt64(oDB.ExecuteScalar_Query(_strSQL));

                    //check if there is any data in the datareader
                    if (_Count > 0)
                    {
                        _Result = false;
                    }

                    return _Result;
                }
                catch //(Exception ex)
                {
                    //UpdateLog("clsDiseaseManagement -- IsDelete -- " + ex.ToString());
                    //MessageBox.Show(ex.ToString(),clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    // Return _Result
                }
                finally
                {
                    if (cmd != null)
                    {
                        cmd.Parameters.Clear();
                        cmd.Dispose();
                        cmd = null;
                    }
                    oDB.Disconnect();
                    oDB.Dispose();
                    oDB = null;
                }
                return _Result;
            }

            public long IsExistCriteria(string oCriteriaName)
            {
                //// REMARK

                string _strSQL = "";
                //gloStream.gloDataBase.gloDataBase oDB = new gloStream.gloDataBase.gloDataBase();
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(clsGeneral.EMRConnectionString);
                //System.Data.SqlClient.SqlDataReader oDataReader = null;
                long CriteriaID = 0;
             //   bool _Result = true;
                long _Count = 0;

                SqlConnection Conn = null;
                SqlCommand cmd = null;
                try
                {
                    //connect to the database
                    oDB.Connect(false);
                    //extract the criteria id from the table for the given criteria name
                    _strSQL = "SELECT dm_mst_Id FROM DM_Criteria_MST where dm_mst_CriteriaName = '" + oCriteriaName + "'";
                    Conn = new SqlConnection(clsGeneral.EMRConnectionString);
                    Conn.Open();
                    cmd = new SqlCommand(_strSQL, Conn);

                    CriteriaID = Convert.ToInt64(cmd.ExecuteScalar());
                    //Val(oDB.ExecuteQueryScaler(_strSQL))
                    Conn.Close();
                    Conn.Dispose();
                    Conn = null;
                    //set the query string
                    _strSQL = "SELECT COUNT(DM_TransId) FROM DM_Patient where DM_nCriteriaID =" + CriteriaID;
                    //execute the query and return a datareader
                    Conn = new SqlConnection(clsGeneral.EMRConnectionString);
                    Conn.Open();
                    _Count = Convert.ToInt64(oDB.ExecuteScalar_Query(_strSQL));

                    //check if there is any data in the datareader
                    if (_Count > 0)
                    {
                      //  _Result = false;
                    }

                    return CriteriaID;
                }
                catch //(Exception ex)
                {
                    //UpdateLog("clsDiseaseManagement -- IsDelete -- " + ex.ToString());
                    //MessageBox.Show(ex.ToString(), clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    // Return _Result
                }
                finally
                {
                    if (cmd != null)
                    {
                        cmd.Parameters.Clear();
                        cmd.Dispose();
                        cmd = null;
                    }
                    oDB.Disconnect();
                    oDB.Dispose();
                    oDB = null;
                }
                return CriteriaID;
            }

            public bool IsPatientSpecificCriteria(Int64 CriteriaID)
            {
                //gloDataBase.gloDataBase oDB = new gloDataBase.gloDataBase();
                //string Query = "SELECT COUNT(*) FROM DM_Criteria_MST WHERE dm_mst_Id = " + CriteriaID + " AND ISNULL(dm_mst_PatientID,0) <> 0";
                //object oResult = 0;
                //try
                //{
                //    oDB.Connect(GetConnectionString);
                //    oResult = oDB.ExecuteQueryScaler(Query);

                //    if (Convert.ToInt32(oResult) > 0)
                //    {
                //        return true;
                //    }
                //    else
                //    {
                //        return false;
                //    }
                //}
                //catch (Exception ex)
                //{
                //    MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    return false;
                //}
                //finally
                //{
                //    oDB.Disconnect();
                //    oDB.Dispose();
                //    oDB = null;
                //}
                return false;

            }

            public bool IsInPatientHealthPlan(Int64 CriteriaID, Int64 PatientID)
            {
                //gloDataBase.gloDataBase oDB = new gloDataBase.gloDataBase();
                //string Query = "SELECT COUNT(*) FROM DM_Patient WHERE DM_nCriteriaID = " + CriteriaID + " AND DM_nPatientID = " + PatientID + "";
                //object oResult = 0;
                //try
                //{
                //    oDB.Connect(GetConnectionString);
                //    oResult = oDB.ExecuteQueryScaler(Query);

                //    if (Convert.ToInt32(oResult) > 0)
                //    {
                //        return true;

                //    }
                //    else
                //    {
                //        return false;

                //    }
                //}
                //catch (Exception ex)
                //{
                //    MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    return false;
                //}
                //finally
                //{
                //    oDB.Disconnect();
                //    oDB.Dispose();
                //    oDB = null;
                //}
                return false;
            }

            public DiseaseManagement()
                : base()
            {
            }

            //protected override void Finalize()
            //{
            //    base.Finalize();
            //}

            public DataTable FindPatientSpecificTriggers(Int64 PatientId)
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(clsGeneral.EMRConnectionString);
                //DBParameter oParamater = default(DBParameter);

                DataTable oResultTable = new DataTable();
                try
                {
                    string _strSQL = null;

                    _strSQL = "SELECT distinct DM_nCriteriaID from DM_Patient where DM_nPatientId = " + PatientId;

                    //' _strSQL = "SELECT  * from DM_Patient where DM_nPatientId = " & PatientId
                    oDB.Retrive_Query(_strSQL, out oResultTable);
                    if ((oResultTable != null))
                    {
                        return oResultTable;
                    }
                }
                catch //(Exception ex)
                {
                    //MessageBox.Show(ex.Message,clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if ((oDB != null))
                    {
                        oDB.Dispose();
                        oDB = null;
                    }
                }
                return oResultTable;
            }

            public DataTable FindPatientSpecificDueTriggers(Int64 PatientId, Int64 CriteriaId)
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(clsGeneral.EMRConnectionString);
                //DBParameter oParamater = default(DBParameter);

                DataTable oResultTable = new DataTable();
                try
                {
                    string _strSQL = null;

                    _strSQL = "SELECT DM_TransId, DM_dtTransDate, DM_nPatientID, DM_nCriteriaID, DM_nTriggerID, DM_sResult, DM_nPrint, DM_nFax, DM_nType, DM_DueType, DM_DueValue, " + " DM_bIsOverride, DM_sReason, DM_sNotes, DM_bIsGiven, DM_bIsRecurring, DM_TriggerName, DM_CriteriaName, DM_TriggerDtlInfo, sDrugForm, sRoute, " + "sFrequency, sNDCCode, nIsNarcotics, sDuration, mpid, sDrugQtyQualifier from DM_Patient where DM_bIsGiven = 'False' and DM_nPatientId = " + PatientId + " and  DM_nCriteriaID = " + CriteriaId + "  ";

                    oDB.Retrive_Query(_strSQL, out oResultTable);
                    if ((oResultTable != null))
                    {
                        return oResultTable;
                    }
                }
                catch //(Exception ex)
                {
                    //MessageBox.Show(ex.Message,clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if ((oDB != null))
                    {
                        oDB.Dispose();
                    }
                }
                return oResultTable;
            }

            public DataTable FindDueTriggerDetails(Int64 TransId, bool bIsRecurring)
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(clsGeneral.EMRConnectionString);
                //DBParameter oParamater = default(DBParameter);

                DataTable oResultTable = new DataTable();
                try
                {
                    string _strSQL = null;
                    if (bIsRecurring)
                    {
                        //_strSQL = "SELECT * from DM_Patient, DM_Patient_DTL where DM_bIsGiven = 'False' and DM_Patient.DM_TransId = " & TransId
                        _strSQL = "SELECT DM_dtTransDate,DM_DueType, DM_DueValue,DM_sReason,DM_sNotes,DM_bIsRecurring, DM_dtStartDate, DM_dtEndDate, DM_nDurationType, DM_nDurationPeriod  FROM DM_Patient INNER JOIN DM_Patient_DTL ON DM_Patient.DM_TransId = DM_Patient_DTL.DM_TransId where DM_bIsGiven = 'False' and DM_Patient.DM_TransId = " + TransId;
                    }
                    else
                    {
                        _strSQL = "SELECT DM_dtTransDate, DM_DueType, DM_DueValue, DM_sReason, DM_sNotes,DM_bIsRecurring from DM_Patient where DM_bIsGiven = 'False' and DM_TransId = " + TransId;
                    }


                    oDB.Retrive_Query(_strSQL, out oResultTable);
                    if ((oResultTable != null))
                    {
                        return oResultTable;
                    }
                }
                catch //(Exception ex)
                {
                    //MessageBox.Show(ex.Message,clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if ((oDB != null))
                    {
                        oDB.Dispose();
                    }
                }
                return oResultTable;
            }
            //private object Get_PatientDetails(long _PatientID)
            //{
            //    DataTable dtPatient = null;

            //    try
            //    {
            //        dtPatient = new DataTable();
            //        dtPatient = GetPatientInfo(_PatientID);
            //        if ((dtPatient == null) == false)
            //        {
            //            if (dtPatient.Rows.Count > 0)
            //            {
            //                strPatientCode = Convert.ToString(dtPatient.Rows[0]["sPatientCode"]);
            //                strPatientFirstName = Convert.ToString(dtPatient.Rows[0]["sFirstName"]);
            //                strPatientLastName = Convert.ToString(dtPatient.Rows[0]["sLastName"]);
            //                strPatientDOB = Convert.ToString(dtPatient.Rows[0]["dtDOB"]);
            //                strPatientAge = GetAge(Convert.ToDateTime(dtPatient.Rows[0]["dtDOB"]));
            //                strPatientGender = Convert.ToString(dtPatient.Rows[0]["sGender"]);
            //                strPatientMaritalStatus = Convert.ToString(dtPatient.Rows[0]["sMaritalStatus"]);

            //            }
            //        }

            //    }
            //    catch (Exception ex)
            //    {
            //    }
            //    finally
            //    {
            //        if ((dtPatient == null) == false)
            //        {
            //            dtPatient.Dispose();
            //            dtPatient = null;
            //        }


            //    }
            //}

            //public bool CheckDueGuidelines(Int64 _TransId, bool IsRecurring, long PatientID)
            //{
            //    //'Get the Trigger Details based on the Transaction id
            //    DataTable dtTriggerDetails = FindDueTriggerDetails(_TransId, IsRecurring);
            //    string _DueType = null;
            //    string _DueValue = null;
            //    string _DurationType = null;
            //    int _DurationPeriod = 0;
            //    System.DateTime _StartDate = default(System.DateTime);
            //    System.DateTime _EndDate = default(System.DateTime);
            //    Get_PatientDetails(PatientID);
            //    Int32 nPatAge = GetPatientAgeinYrs(Convert.ToDateTime( strPatientDOB));
            //    if ((dtTriggerDetails != null))
            //    {
            //        //'Loop though all the records ideally it should contain only one record

            //        for (Int32 _cnt = 0; _cnt <= dtTriggerDetails.Rows.Count - 1; _cnt++)
            //        {
            //            if (!Information.IsDBNull(dtTriggerDetails.Rows[_cnt]["DM_DueType"]))
            //            {
            //                _DueType = Convert.ToString(dtTriggerDetails.Rows[_cnt]["DM_DueType"]);
            //                if (_DueType == "Age")
            //                {
            //                    if (!Information.IsDBNull(dtTriggerDetails.Rows[_cnt]["DM_DueValue"]))
            //                    {
            //                        _DueValue = Convert.ToString(dtTriggerDetails.Rows[_cnt]["DM_DueValue"]);
            //                        if (_DueValue.Contains(">="))
            //                        {
            //                            _DueValue = _DueValue.Remove(0, 2);
            //                            if ((nPatAge >=Convert.ToInt32(_DueValue.Trim())))
            //                            {
            //                                return true;
            //                            }
            //                        }
            //                        else if (_DueValue.Contains(">"))
            //                        {
            //                            _DueValue = _DueValue.Remove(0, 1);
            //                            if ((nPatAge > Convert.ToInt32(_DueValue.Trim())))
            //                            {
            //                                return true;
            //                            }
            //                        }
            //                        else if (_DueValue.Contains("="))
            //                        {
            //                            _DueValue = _DueValue.Remove(0, 1);
            //                            if ((nPatAge == Convert.ToInt32(_DueValue.Trim())))
            //                            {
            //                                return true;
            //                            }

            //                        }
            //                    }
            //                }
            //                else
            //                {
            //                    //' SUDHIR - 20090311 - FOR EVERY CHECK IN 
            //                    if (IsRecurring)
            //                    {
            //                        if (!Information.IsDBNull(dtTriggerDetails.Rows[_cnt]["DM_nDurationType"]))
            //                        {
            //                            _DurationType = Convert.ToString(dtTriggerDetails.Rows[_cnt]["DM_nDurationType"]);
            //                            _StartDate = Convert.ToDateTime(dtTriggerDetails.Rows[_cnt]["DM_dtStartDate"]);
            //                            _EndDate = Convert.ToDateTime(dtTriggerDetails.Rows[_cnt]["DM_dtEndDate"]);
            //                            if (_DurationType == "On Every Check In")
            //                            {
            //                                if (IsPatientCheckIn(PatientID) == true)
            //                                {
            //                                    if (DateAndTime.Now.Date >= _StartDate.Date & DateAndTime.Now.Date <= _EndDate.Date)
            //                                    {
            //                                        return true;
            //                                    }
            //                                }
            //                            }
            //                            else
            //                            {
            //                                //' SUDHIR - 20090313 - FOR RECURRENCE
            //                                ArrayList arrTriggerDates = new ArrayList();
            //                                System.DateTime _tmpDate = default(System.DateTime);
            //                                _DurationPeriod = Convert.ToInt32(dtTriggerDetails.Rows[_cnt]["DM_nDurationPeriod"]);
            //                                switch (Strings.UCase(_DurationType))
            //                                {
            //                                    case Strings.UCase("Days"):
            //                                        _tmpDate = _StartDate;
            //                                        //'RECURRECE WILL START FROM STARTING DATE '' STARTING DATE WILL NOT CONSIDER AS TRIGGERING DATE
            //                                        //' FROM DATE - TO DATE VALIDATION
            //                                        while ((_tmpDate >= _StartDate & _tmpDate <= _EndDate & _DurationPeriod != 0))
            //                                        {
            //                                            _tmpDate = _tmpDate.AddDays(_DurationPeriod);
            //                                            //' ADDING INTERVALS
            //                                            arrTriggerDates.Add(_tmpDate.ToShortDateString());
            //                                            //' CREATE LIST OF POSSIBLE TRIGGER DATES
            //                                        }
            //                                        //'SEARCH TODAYS DATE FOR TRIGGER DATE.. 
            //                                        if (arrTriggerDates.Contains(DateAndTime.Now.Date.ToShortDateString()))
            //                                        {
            //                                            return true;
            //                                            //' RETURN RESULT , SAME LOGIC FOR BELOW CASES
            //                                        }
            //                                        break;
            //                                    case Strings.UCase("Weeks"):
            //                                        _tmpDate = _StartDate;
            //                                        while ((_tmpDate >= _StartDate & _tmpDate <= _EndDate & _DurationPeriod != 0))
            //                                        {
            //                                            _tmpDate = _tmpDate.AddDays(_DurationPeriod * 7);
            //                                            //' 7 MULTIPLY FOR FOR WEEKS
            //                                            arrTriggerDates.Add(_tmpDate.ToShortDateString());
            //                                        }
            //                                        if (arrTriggerDates.Contains(DateAndTime.Now.Date.ToShortDateString()))
            //                                        {
            //                                            return true;
            //                                        }
            //                                        break;
            //                                    case Strings.UCase("Months"):
            //                                        _tmpDate = _StartDate;
            //                                        while ((_tmpDate >= _StartDate & _tmpDate <= _EndDate & _DurationPeriod != 0))
            //                                        {
            //                                            _tmpDate = _tmpDate.AddMonths(_DurationPeriod);
            //                                            arrTriggerDates.Add(_tmpDate.ToShortDateString());
            //                                        }
            //                                        if (arrTriggerDates.Contains(DateAndTime.Now.Date.ToShortDateString()))
            //                                        {
            //                                            return true;
            //                                        }
            //                                        break;
            //                                    case Strings.UCase("Years"):
            //                                        _tmpDate = _StartDate;
            //                                        while ((_tmpDate >= _StartDate & _tmpDate <= _EndDate & _DurationPeriod != 0))
            //                                        {
            //                                            _tmpDate = _tmpDate.AddYears(_DurationPeriod);
            //                                            arrTriggerDates.Add(_tmpDate.ToShortDateString());
            //                                        }
            //                                        if (arrTriggerDates.Contains(DateAndTime.Now.Date.ToShortDateString()))
            //                                        {
            //                                            return true;
            //                                        }
            //                                        break;
            //                                }
            //                                arrTriggerDates = null;
            //                                //' RECURRENCE LOGIC END
            //                            }
            //                        }
            //                    }
            //                    else
            //                    {
            //                        if (!Information.IsDBNull(dtTriggerDetails.Rows[_cnt]["DM_DueValue"]))
            //                        {
            //                            DateTime _DueDate = default(DateTime);
            //                            DateTime _TransDate = default(DateTime);
            //                            if (!string.IsNullOrEmpty(dtTriggerDetails.Rows[_cnt]["DM_DueValue"]))
            //                            {
            //                                _DueDate = (DateTime)dtTriggerDetails.Rows[_cnt]["DM_DueValue"];
            //                                _TransDate = (DateTime)dtTriggerDetails.Rows[_cnt]["DM_dtTransDate"];
            //                                if (_DueDate.Date == _TransDate.Date & _DueDate.Date == DateAndTime.Now.Date)
            //                                {
            //                                    return false;
            //                                }
            //                                switch (DateTime.Compare(DateTime.Now.Date, _DueDate.Date))
            //                                {
            //                                    case -1:
            //                                        break;
            //                                    //'Smaller

            //                                    case 1:
            //                                        //'Bigger

            //                                        return true;
            //                                    case 0:
            //                                        //'equal
            //                                        return true;
            //                                }
            //                            }
            //                            else
            //                            {
            //                                return true;
            //                            }
            //                        }
            //                    }

            //                    //'END SUDHIR - ON EVERY CHECK IN

            //                }
            //            }
            //        }
            //    }

            //}
            public bool CheckDMReason(Int64 _TransId, bool IsRecurring)
            {
                DataTable dtTriggerDetails = FindDueTriggerDetails(_TransId, IsRecurring);
                if ((dtTriggerDetails != null))
                {
                    //'Loop though all the records ideally it should contain only one record
                    for (Int32 _cnt = 0; _cnt <= dtTriggerDetails.Rows.Count - 1; _cnt++)
                    {
                        bool blnPastFlag = false;
                        if (!Information.IsDBNull(dtTriggerDetails.Rows[_cnt][1]))
                        {
                            //Dim _TransDate As DateTime = CType(dtTriggerDetails.Rows(_cnt)(1), DateTime)
                            DateTime _TransDate = (DateTime)dtTriggerDetails.Rows[_cnt]["DM_dtTransDate"];
                            switch (DateTime.Compare(_TransDate.Date, DateTime.Now.Date))
                            {
                                case -1:
                                    //'Smaller
                                    return true;
                                case 1:
                                    //'Bigger
                                    blnPastFlag = true;

                                    break;
                                case 0:
                                    //'equal
                                    blnPastFlag = true;
                                    break;
                            }
                        }
                        else
                        {
                            blnPastFlag = true;
                        }
                        if (blnPastFlag)
                        {
                            //If Not IsDBNull(dtTriggerDetails.Rows(_cnt)(12)) Then
                            //    If dtTriggerDetails.Rows(_cnt)(12).ToString <> "" Then
                            if (!Information.IsDBNull(dtTriggerDetails.Rows[_cnt]["DM_sReason"]))
                            {
                                if (!string.IsNullOrEmpty(dtTriggerDetails.Rows[_cnt]["DM_sReason"].ToString()))
                                {
                                    return false;
                                }
                                else
                                {
                                    return true;
                                }
                            }
                            else
                            {
                                return true;
                            }
                        }

                    }
                }
                return false;
            }


            public double GetPatientAgeinYrs(System.DateTime PatientDOB)
            {
                double nMonths = 0;
                double nPatAge = 0;
                nMonths = DateAndTime.DateDiff(DateInterval.Month, Convert.ToDateTime(PatientDOB), System.DateTime.Now.Date);
                nPatAge = Convert.ToDouble(nMonths) / 12.0;
                return Convert.ToDouble(Strings.Format(nPatAge, "#0.00"));
            }

            //Public Function GetPatientAgeinYrs(ByVal PatientDOB As Date) As Int32
            //    Dim nMonths, nPatAge As Int32
            //    nMonths = DateDiff(DateInterval.Month, CType(PatientDOB, Date), Date.Now.Date)
            //    nPatAge = nMonths \ 12
            //    Return nPatAge
            //End Function
            public DataTable GetSpecifiHealthPlan(Int64 _Criteria)
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(clsGeneral.EMRConnectionString);
                //DBParameter oParamater = default(DBParameter);

                DataTable oResultTable = new DataTable();
                try
                {
                    string _strSQL = null;

                    _strSQL = "SELECT DISTINCT dm_mst_Id AS CriteriaID, dm_mst_Gender AS Gender, dm_mst_CriteriaName as Name, dm_mst_DisplayMessage as Message FROM DM_Criteria_MST where dm_mst_Id=" + _Criteria;

                    oDB.Retrive_Query(_strSQL, out oResultTable);
                    if ((oResultTable != null))
                    {
                        if (oResultTable.Rows.Count > 0)
                        {
                            return oResultTable;
                        }

                    }
                }
                catch //(Exception ex)
                {
                    //MessageBox.Show(ex.Message,clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }
                finally
                {
                    if ((oDB != null))
                    {
                        oDB.Dispose();
                    }
                }
                return oResultTable;
            }
            public DataTable GetAllHealthPlans()
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(clsGeneral.EMRConnectionString);
             //   DBParameter oParamater = default(DBParameter);

                DataTable oResultTable = new DataTable();
                try
                {
                    string _strSQL = null;

                    _strSQL = "SELECT DISTINCT dm_mst_Id AS CriteriaID, dm_mst_Gender AS Gender, dm_mst_CriteriaName as Name, dm_mst_DisplayMessage as Message FROM DM_Criteria_MST";

                    oDB.Retrive_Query(_strSQL, out oResultTable);
                    if ((oResultTable != null))
                    {
                        return oResultTable;
                    }
                }
                catch //(Exception ex)
                {
                    //MessageBox.Show(ex.Message,clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }
                finally
                {
                    if ((oDB != null))
                    {
                        oDB.Dispose();
                    }
                }
                return oResultTable;
            }
            public string GetDMAlerts(Int64 _PatientId)
            {
                Collection _oCriterias = new Collection();
                string strDMAlert = "";
                gloStream.DiseaseManagement.DiseaseManagement oDMSingleProcess = new gloStream.DiseaseManagement.DiseaseManagement();

                try
                {
                    //  Application.DoEvents()
                    _oCriterias = oDMSingleProcess.FindGuidelinesForSinglePatient(_PatientId);
                    DataTable oPatTriggers = null;
                    DataTable oPatientCriterias = null;
                    oPatTriggers = oDMSingleProcess.FindPatientSpecificTriggers(_PatientId);
                    if ((oPatTriggers != null))
                    {

                        if (_oCriterias.Count > 0)
                        {
                            for (Int32 _cnt = 0; _cnt <= oPatTriggers.Rows.Count - 1; _cnt++)
                            {
                                oPatientCriterias = oDMSingleProcess.FindPatientSpecificDueTriggers(_PatientId,Convert.ToInt64( oPatTriggers.Rows[_cnt]["DM_nCriteriaID"]));
                                if ((oPatientCriterias != null))
                                {
                                    //'to remove the guidelines from the Collection that are not due based on the due values
                                    Int32 nDueValue = 0;
                                    for (Int32 _row = 0; _row <= oPatientCriterias.Rows.Count - 1; _row++)
                                    {
                                        Int64 TransId = default(Int64);
                                        bool bIsRecurring = false;
                                        TransId = Convert.ToInt64(oPatientCriterias.Rows[_row]["DM_TransId"]);
                                        bIsRecurring =Convert.ToBoolean( oPatientCriterias.Rows[_row]["DM_bIsRecurring"]);

                                        
                                        //if (oDMSingleProcess.CheckDueGuidelines(TransId, bIsRecurring, _PatientId) == false)
                                        //{
                                        //    nDueValue += 1;
                                        //}
                                        


                                    }

                                    if (nDueValue == oPatientCriterias.Rows.Count)
                                    {
                                        for (Int32 _myindex = _oCriterias.Count; _myindex >= 1; _myindex += -1)
                                        {
                                            if (_oCriterias[_myindex] == oPatTriggers.Rows[_cnt]["DM_nCriteriaID"])
                                            {
                                                _oCriterias.Remove(_myindex);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        bool blnExists = false;
                                        for (Int32 _myindex = _oCriterias.Count; _myindex >= 1; _myindex += -1)
                                        {
                                            if (_oCriterias[_myindex] == oPatTriggers.Rows[_cnt]["DM_nCriteriaID"])
                                            {
                                                blnExists = true;
                                            }
                                        }
                                        if (blnExists == false)
                                        {
                                            _oCriterias.Add((Int64)oPatTriggers.Rows[_cnt]["DM_nCriteriaID"]);
                                        }

                                    }
                                }

                            }

                        }
                        else
                        {
                            for (Int32 _cnt = 0; _cnt <= oPatTriggers.Rows.Count - 1; _cnt++)
                            {
                                oPatientCriterias = oDMSingleProcess.FindPatientSpecificDueTriggers(_PatientId, Convert.ToInt64(oPatTriggers.Rows[_cnt]["DM_nCriteriaID"]));
                                if ((oPatientCriterias != null))
                                {
                                    //'to remove the guidelines from the Collectiont that are not due based on the due values
                                    Int32 nDueValue = 0;
                                    for (Int32 _row = 0; _row <= oPatientCriterias.Rows.Count - 1; _row++)
                                    {
                                        Int64 TransId = default(Int64);
                                        bool bIsRecurring = false;
                                        TransId =Convert.ToInt64( oPatientCriterias.Rows[_cnt]["DM_TransId"]);
                                        bIsRecurring = Convert.ToBoolean(oPatientCriterias.Rows[_cnt]["DM_bIsRecurring"]);

                                        //if (oDMSingleProcess.CheckDueGuidelines(TransId, bIsRecurring, _PatientId))
                                        //{
                                        //    nDueValue += 1;
                                        //}

                                    }
                                    if (nDueValue == oPatientCriterias.Rows.Count & nDueValue != 0)
                                    {
                                        _oCriterias.Add((Int64)oPatTriggers.Rows[_cnt]["DM_nCriteriaID"]);
                                    }

                                }

                            }
                        }
                    }
                    //UpdateLog(" END - FindGuidelinesForSinglePatient");
                    if ((_oCriterias == null) == false)
                    {
                        for (int i = 1; i <= _oCriterias.Count; i++)
                        {
                            if (i == 1)
                            {
                                strDMAlert = oDMSingleProcess.GetCriteriaMessage(Convert.ToInt64(_oCriterias[i]));
                            }
                            else
                            {
                                strDMAlert = strDMAlert + ", " + oDMSingleProcess.GetCriteriaMessage(Convert.ToInt64(_oCriterias[i]));
                            }

                        }
                    }

                    return strDMAlert;
                }
                catch //(Exception ex)
                {
                    return "";
                }
                finally
                {
                    _oCriterias = null;
                }
            }



            //sarika DM Denormalization 20090331
            public object GetTemplate(Int64 TemplateID)
            {
                object img = null;
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(clsGeneral.EMRConnectionString);
                string _strSQL = "";

                try
                {
                    _strSQL = "select sDescription from TemplateGallery_MST where  nTemplateID = " + TemplateID + "";

                    img = oDB.ExecuteScalar_Query(_strSQL);

                    return img;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if ((oDB != null))
                    {
                        oDB.Dispose();
                        oDB = null;
                    }
                }
            }
            //----

            public object GetTemplateByName(string Templatename)
            {
                object img = null;
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(clsGeneral.EMRConnectionString);
                string _strSQL = "";

                try
                {
                    _strSQL = "select sDescription from TemplateGallery_MST where  sTemplateName = '" + Templatename + "'";

                    img = oDB.ExecuteScalar_Query(_strSQL);

                    return img;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if ((oDB != null))
                    {
                        oDB.Dispose();
                        oDB = null;
                    }
                }
            }

        }


        namespace Common
        {

            public class Criteria
            {

                private string _ErrorMessage;
                public string ErrorMessage
                {
                    get { return _ErrorMessage; }
                    set { _ErrorMessage = value; }
                }

                public gloStream.DiseaseManagement.Supporting.Categories Categories()
                {
                    //gloStream.gloDataBase.gloDataBase ODB = new gloStream.gloDataBase.gloDataBase();
                    //System.Data.SqlClient.SqlDataReader oDataReader = null;
                    gloStream.DiseaseManagement.Supporting.Categories oCategories = new gloStream.DiseaseManagement.Supporting.Categories();

                    //try
                    //{
                    //    ODB.Connect(GetConnectionString);
                    //    oDataReader = ODB.ReadQueryRecords("SELECT nCategoryID,sDescription FROM Category_MST where sCategoryType = 'History'");
                    //    if (oDataReader.HasRows == true)
                    //    {
                    //        while (oDataReader.Read())
                    //        {
                    //            oCategories.Add(oDataReader["nCategoryID"], oDataReader["sDescription"]);
                    //        }
                    //    }

                    //    ODB.Disconnect();

                    //    return oCategories;
                    //}
                    //catch (Exception ex)
                    //{
                    //    //UpdateLog("clsDiseaseManagement -- Categories -- " + ex.ToString());
                    //    MessageBox.Show(ex.ToString(),clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //}
                    //finally
                    //{
                    //    ODB = null;
                    //    oDataReader = null;
                    //    oCategories = null;
                    //}
                    return oCategories;
                }

                public gloStream.DiseaseManagement.Supporting.History Histories(string oCategory)
                {
                    //gloStream.gloDataBase.gloDataBase ODB = new gloStream.gloDataBase.gloDataBase();
                    //System.Data.SqlClient.SqlDataReader oDataReader = null;
                    //DataTable dtHistory = null;
                    gloStream.DiseaseManagement.Supporting.History oHistory = new gloStream.DiseaseManagement.Supporting.History();

                    return oHistory;
                }
                //'Sandip Darade 20090305
                //'for the function to return datatable for coded history
                public DataTable GetHistoriesDataTable(string oCategory)
                {
                    //gloStream.gloDataBase.gloDataBase ODB = new gloStream.gloDataBase.gloDataBase();
                    //System.Data.SqlClient.SqlDataReader oDataReader = null;
                    DataTable dtHistory = null;
                    //gloStream.DiseaseManagement.Supporting.History oHistory = new gloStream.DiseaseManagement.Supporting.History();

                    ////' SUDHIR 20090622 ''
                    //oCategory = oCategory.Replace("'", "''");
                    ////' END SUDHIR ''

                    //string sqlstr = null;

                    //try
                    //{
                    //    //'IF CODED HISTORY ENABLED
                    //    if (gblnCodedHistory)
                    //    {
                    //        clsPatientHistory objclsPatientHistory = new clsPatientHistory();
                    //        if (oCategory.StartsWith("Aller"))
                    //        {
                    //            ODB.Connect(GetConnectionString);

                    //            sqlstr = "SELECT History_MST.nHistoryID, History_MST.sDescription FROM  Category_MST INNER JOIN ";
                    //            sqlstr += " History_MST ON Category_MST.nCategoryID = History_MST.nCategoryID ";
                    //            sqlstr += "where Category_MST.sDescription  = '" + oCategory + "' ORDER BY History_MST.sDescription ";

                    //            dtHistory = ODB.ReadQueryDataTable(sqlstr);
                    //            //If Not IsNothing(dtHistory) Then
                    //            //    For i As Integer = 0 To dtHistory.Rows.Count - 1
                    //            //        oHistory.Items.Add(dtHistory.Rows(i)("nHistoryID"), dtHistory.Rows(i)("sDescription"))
                    //            //    Next
                    //            //End If
                    //            return dtHistory;
                    //        }
                    //        else
                    //        {
                    //            //MEDICAL CONDITION 

                    //            if (oCategory == "Medical Condition" & gblnClinicDIAlert == true)
                    //            {
                    //                gloDIControl.ClsHistoryMedicalCondition objDrugInteract = new gloDIControl.ClsHistoryMedicalCondition();
                    //                gloDIControl.DrugInteractionCollection.gloInteractionCollection objCollection = new gloDIControl.DrugInteractionCollection.gloInteractionCollection();
                    //                objCollection = objDrugInteract.FillMedicationForScreening();
                    //                dtHistory = new DataTable();
                    //                dtHistory.Columns.Add(new DataColumn("ICD9ID", typeof(Int64)));
                    //                dtHistory.Columns.Add(new DataColumn("Column1", typeof(string)));

                    //                for (int i = 0; i <= objCollection.Count - 1; i++)
                    //                {
                    //                    //add the Medical conditions to the history object.
                    //                    int rowindex = dtHistory.Rows.Count;
                    //                    dtHistory.Rows.Add();
                    //                    dtHistory.Rows[rowindex]["ICD9ID"] = objCollection.Item(i).ID;
                    //                    dtHistory.Rows[rowindex]["Column1"] = objCollection.Item(i).Name;
                    //                }
                    //                return dtHistory;
                    //            }
                    //            else
                    //            {
                    //                dtHistory = objclsPatientHistory.GetAllICD9Gallery;
                    //                //If Not IsNothing(dtHistory) Then
                    //                //    For i As Integer = 0 To dtHistory.Rows.Count - 1
                    //                //        oHistory.Items.Add(dtHistory.Rows(i)("ICD9ID"), dtHistory.Rows(i)("Column1"))
                    //                //    Next
                    //                //End If
                    //                return dtHistory;
                    //            }
                    //        }

                    //    }
                    //    else if (oCategory == "Medical Condition" & gblnClinicDIAlert == true)
                    //    {
                    //        gloDIControl.ClsHistoryMedicalCondition objDrugInteract = new gloDIControl.ClsHistoryMedicalCondition();
                    //        gloDIControl.DrugInteractionCollection.gloInteractionCollection objCollection = new gloDIControl.DrugInteractionCollection.gloInteractionCollection();
                    //        objCollection = objDrugInteract.FillMedicationForScreening();
                    //        dtHistory = new DataTable();
                    //        dtHistory.Columns.Add(new DataColumn("ICD9ID", typeof(Int64)));
                    //        dtHistory.Columns.Add(new DataColumn("Column1", typeof(string)));

                    //        for (int i = 0; i <= objCollection.Count - 1; i++)
                    //        {
                    //            //add the Medical conditions to the history object.
                    //            int rowindex = dtHistory.Rows.Count;
                    //            dtHistory.Rows.Add();
                    //            dtHistory.Rows[rowindex]["ICD9ID"] = objCollection.Item(i).ID;
                    //            dtHistory.Rows[rowindex]["Column1"] = objCollection.Item(i).Name;
                    //        }
                    //        return dtHistory;
                    //    }
                    //    else
                    //    {
                    //        ODB.Connect(GetConnectionString);
                    //        sqlstr = "SELECT History_MST.nHistoryID, History_MST.sDescription FROM  Category_MST INNER JOIN ";
                    //        sqlstr += " History_MST ON Category_MST.nCategoryID = History_MST.nCategoryID ";
                    //        sqlstr += "where Category_MST.sDescription  = '" + oCategory + "' ORDER BY History_MST.sDescription ";

                    //        dtHistory = ODB.ReadQueryDataTable(sqlstr);
                    //        //If Not IsNothing(dtHistory) Then
                    //        //    For i As Integer = 0 To dtHistory.Rows.Count - 1
                    //        //        oHistory.Items.Add(dtHistory.Rows(i)("nHistoryID"), dtHistory.Rows(i)("sDescription"))
                    //        //    Next
                    //        //End If
                    //        return dtHistory;
                    //    }


                    //}
                    //catch (SqlException ex)
                    //{
                    //    MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //    UpdateLog("clsDiseaseManagement -- Histories -- " + ex.ToString());
                    //}
                    //catch (Exception ex)
                    //{
                    //    UpdateLog("clsDiseaseManagement -- Histories -- " + ex.ToString());
                    //    MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //}
                    //finally
                    //{
                    //    ODB = null;
                    //    oDataReader = null;
                    //    oHistory = null;
                    //}
                    return dtHistory;
                }
                

                public gloStream.DiseaseManagement.Supporting.Drugs Drugs(string SearchDrug)
                {

                    //gloStream.gloDataBase.gloDataBase ODB = new gloStream.gloDataBase.gloDataBase();
                    //System.Data.SqlClient.SqlDataReader oDataReader = null;
                    gloStream.DiseaseManagement.Supporting.Drugs oDrugs = new gloStream.DiseaseManagement.Supporting.Drugs();
                    
                    return oDrugs;
                }

                public gloStream.DiseaseManagement.Supporting.ICD9s ICD9s()
                {
                    //gloStream.gloDataBase.gloDataBase ODB = new gloStream.gloDataBase.gloDataBase();
                    //System.Data.SqlClient.SqlDataReader oDataReader = null;
                    gloStream.DiseaseManagement.Supporting.ICD9s oICD9s = new gloStream.DiseaseManagement.Supporting.ICD9s();
                    //string sqlstr = null;


                    //try
                    //{
                    //    ODB.Connect(GetConnectionString);
                    //    sqlstr = "SELECT nICD9ID, sICD9Code, sDescription FROM ICD9";

                    //    oDataReader = ODB.ReadQueryRecords(sqlstr);

                    //    if ((oDataReader != null))
                    //    {
                    //        if (oDataReader.HasRows == true)
                    //        {
                    //            while (oDataReader.Read())
                    //            {
                    //                gloStream.DiseaseManagement.Supporting.ICD9 oICD9 = new gloStream.DiseaseManagement.Supporting.ICD9();
                    //                var _with28 = oICD9;
                    //                if (!Information.IsDBNull(oDataReader["nICD9ID"]))
                    //                {
                    //                    _with28.ID = oDataReader["nICD9ID"];
                    //                }
                    //                if (!Information.IsDBNull(oDataReader["sICD9Code"]))
                    //                {
                    //                    _with28.Code = oDataReader["sICD9Code"];
                    //                }
                    //                if (!Information.IsDBNull(oDataReader["sDescription"]))
                    //                {
                    //                    _with28.Name = oDataReader["sDescription"];
                    //                }
                    //                oICD9s.Add(ref oICD9);
                    //            }
                    //        }
                    //        oDataReader.Close();
                    //    }
                    //    ODB.Disconnect();

                    //    return oICD9s;
                    //}
                    //catch (SqlException ex)
                    //{
                    //    MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //    UpdateLog("clsDiseaseManagement -- ICD9s -- " + ex.ToString());
                    //}
                    //catch (Exception ex)
                    //{
                    //    UpdateLog("clsDiseaseManagement -- ICD9s -- " + ex.ToString());
                    //    MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //}
                    //finally
                    //{
                    //    ODB.Dispose();
                    //    ODB = null;
                    //    oDataReader = null;
                    //    oICD9s = null;
                    //}
                    return oICD9s;
                }

                public gloStream.DiseaseManagement.Supporting.CPTs CPTs()
                {
                    //gloStream.gloDataBase.gloDataBase ODB = new gloStream.gloDataBase.gloDataBase();
                    //System.Data.SqlClient.SqlDataReader oDataReader = null;
                    gloStream.DiseaseManagement.Supporting.CPTs oCPTs = new gloStream.DiseaseManagement.Supporting.CPTs();
                    //string sqlstr = null;


                    //try
                    //{
                    //    ODB.Connect(GetConnectionString);

                    //    sqlstr = " SELECT nCPTID, sCPTCode, sDescription FROM  CPT_MST ";

                    //    oDataReader = ODB.ReadQueryRecords(sqlstr);
                    //    if ((oDataReader != null))
                    //    {
                    //        if (oDataReader.HasRows == true)
                    //        {
                    //            while (oDataReader.Read())
                    //            {
                    //                gloStream.DiseaseManagement.Supporting.CPT oCPT = new gloStream.DiseaseManagement.Supporting.CPT();
                    //                var _with29 = oCPT;
                    //                if (!Information.IsDBNull(oDataReader["nCPTID"]))
                    //                {
                    //                    _with29.ID = oDataReader["nCPTID"];
                    //                }
                    //                if (!Information.IsDBNull(oDataReader["sCPTCode"]))
                    //                {
                    //                    _with29.Code = oDataReader["sCPTCode"];
                    //                }
                    //                if (!Information.IsDBNull(oDataReader["sDescription"]))
                    //                {
                    //                    _with29.Name = oDataReader["sDescription"];
                    //                }
                    //                oCPTs.Add(ref oCPT);
                    //            }
                    //        }
                    //        oDataReader.Close();
                    //    }

                    //    ODB.Disconnect();

                    //    return oCPTs;

                    //}
                    //catch (SqlException ex)
                    //{
                    //    MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //    UpdateLog("clsDiseaseManagement -- CPTs -- " + ex.ToString());
                    //}
                    //catch (Exception ex)
                    //{
                    //    UpdateLog("clsDiseaseManagement -- CPTs -- " + ex.ToString());
                    //    MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //}
                    //finally
                    //{
                    //    ODB = null;
                    //    oDataReader = null;
                    //    oCPTs = null;
                    //}
                    return oCPTs;
                }

                public gloStream.DiseaseManagement.Supporting.Orders Orders()
				{
					//gloStream.gloDataBase.gloDataBase ODB = new gloStream.gloDataBase.gloDataBase();
					gloStream.DiseaseManagement.Supporting.Orders oLabs = new gloStream.DiseaseManagement.Supporting.Orders();
                    //gloStream.DiseaseManagement.Supporting.Order oLab = null;
                    //gloStream.DiseaseManagement.Supporting.OrderGroup oGroup = null;

                    //string sqlstr = null;

                    //DataTable oDTLab_Category = null;
                    //DataTable oDTLab_Groups = null;
                    //DataTable oDTLab_Tests = null;

                    //try {
                    //    ODB.Connect(GetConnectionString);
                    //    //fill the oDTLab_Category table by the data(id,description) from the LM_Category table  
                    //    oDTLab_Category = ODB.ReadData("DM_SelectLMCategory");

                    //    var _with30 = oLabs;
                    //    //for each category in the oDTLab_Category(LM_Category) table
                    //    for (int i = 0; i <= oDTLab_Category.Rows.Count - 1; i++) {
                    //        oLab = new gloStream.DiseaseManagement.Supporting.Order();
                    //        var _with31 = oLab;

                    //        _with31.ID = oDTLab_Category.Rows[i]["lm_category_ID"];
                    //        Int64 id = _with31.ID;
                    //        _with31.Category = oDTLab_Category.Rows[i]["lm_category_Description"];

                    //        //fill the oDTLab_Groups table by the data(id,name) from the LM_Test table  
                    //        //where  'LM_Test(TestGroupFlag = 'G' and CateogryID = LM_Category(i))
                    //        ODB.DBParameters.Clear();
                    //        ODB.DBParameters.Add("@id", id, ParameterDirection.Input, SqlDbType.BigInt);
                    //        oDTLab_Groups = ODB.ReadData("DM_SelectCategoryWiseLabGroup");
                    //        for (int j = 0; j <= oDTLab_Groups.Rows.Count - 1; j++) {
                    //            oGroup = new gloStream.DiseaseManagement.Supporting.OrderGroup();
                    //            var _with32 = oGroup;
                    //            _with32.ID = oDTLab_Groups.Rows[j]["lm_test_ID"];
                    //            Int64 groupid = _with32.ID;
                    //            _with32.Name = oDTLab_Groups.Rows[j]["lm_test_Name"];
                    //            //fill the oDTLab_Tests table by data (id,description) from the LM_test table 
                    //            //where LM_Test(TestGroupFlag = 'T' and CategoryID = LM_Category(i) and groupNo = LM_Test(j).id)
                    //            ODB.DBParameters.Clear();
                    //            ODB.DBParameters.Add("@id", id, ParameterDirection.Input, SqlDbType.BigInt);
                    //            ODB.DBParameters.Add("@Groupid", groupid, ParameterDirection.Input, SqlDbType.BigInt);
                    //            oDTLab_Tests = ODB.ReadData("DM_SelectGroupWiseLabTests");
                    //            for (int k = 0; k <= oDTLab_Tests.Rows.Count - 1; k++) {
                    //                _with32.Tests.Add(oDTLab_Tests.Rows[k]["lm_test_ID"], oDTLab_Tests.Rows[k]["lm_test_Name"]);
                    //            }
                    //            oLab.OrderGroups.Add(ref oGroup);
                    //            oGroup = null;
                    //        }
                    //        _with30.Add(oLab);
                    //        oLab = null;
                    //    }

                    //    return oLabs;

                    //} catch (SqlException ex) {
                    //    MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //    UpdateLog("clsDiseaseManagement -- Labs -- " + ex.ToString());
                    //} catch (Exception ex) {
                    //    UpdateLog("clsDiseaseManagement -- Labs -- " + ex.ToString());
                    //    MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //} finally {
                    //    ODB.Disconnect();
                    //}
                    return oLabs;
				}
                
                public DataTable OrdersTable()
                {
                    gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(clsGeneral.EMRConnectionString);
                    //DBParameter oParamater = default(DBParameter);

                    DataTable oResultTable = new DataTable();
                    try
                    {
                        string _strSQL = null;

                        // _strSQL = "SELECT    lm_test_ID, lm_test_Name   FROM   LM_Test  WHERE  (lm_test_TestGroupFlag = 'T')"
                        //'Sandip Darade 20090820
                        _strSQL = " SELECT   LM_Test.lm_test_ID as lm_test_ID, LM_Test.lm_test_Name as lm_test_Name FROM   LM_Test INNER JOIN " + "LM_Test AS LM_Test_1 ON LM_Test.lm_test_GroupNo = LM_Test_1.lm_test_ID INNER JOIN " + " LM_Category ON LM_Test_1.lm_test_CategoryID = LM_Category.lm_category_ID ";

                        oDB.Retrive_Query(_strSQL, out oResultTable);
                        
                    }
                    catch //(Exception ex)
                    {
                        //MessageBox.Show(ex.Message,clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        if ((oDB != null))
                        {
                            oDB.Dispose();
                            oDB = null;
                        }
                    }

                   
                        return oResultTable;
                    
                }


                public gloStream.DiseaseManagement.Supporting.LabModuleTests LabModuleTests()
                {
                    //gloStream.gloDataBase.gloDataBase ODB = new gloStream.gloDataBase.gloDataBase();
                    gloStream.DiseaseManagement.Supporting.LabModuleTests _LabModuleTests = new gloStream.DiseaseManagement.Supporting.LabModuleTests();
                    //gloStream.DiseaseManagement.Supporting.LabModuleTest _LabModuleTest = null;
                    //gloStream.DiseaseManagement.Supporting.LabModuleTestResult _LabModuleResult = null;

                    //DataTable odtl_Test = new DataTable();
                    //DataTable odtl_Result = new DataTable();
                    //ODB.Connect(GetConnectionString);
                    //try
                    //{
                    //    string strSelectQryTest = "SELECT DISTINCT labtm_Name,labtm_id FROM Lab_Test_Mst";
                    //    odtl_Test = ODB.ReadQueryDataTable(strSelectQryTest);

                    //    for (int i = 0; i <= odtl_Test.Rows.Count - 1; i++)
                    //    {
                    //        _LabModuleTest = new gloStream.DiseaseManagement.Supporting.LabModuleTest();
                    //        var _with33 = _LabModuleTest;
                    //        _with33.TestID = odtl_Test.Rows[i]["labtm_id"];
                    //        _with33.Name = odtl_Test.Rows[i]["labtm_Name"];

                    //        string strSelectQryResult = "Select DISTINCT ISNULL(Lab_Order_Test_ResultDtl.labotrd_ResultName,'') as 'labotrd_ResultName',ISNULL(Lab_Order_Test_ResultDtl.labotrd_ResultNameID,0) as 'labotrd_ResultNameID' FROM Lab_Order_Test_ResultDtl INNER JOIN Lab_Order_Test_Result ON Lab_Order_Test_ResultDtl.labotrd_TestID = " + odtl_Test.Rows[i]["labtm_id"] + " ";
                    //        odtl_Result = ODB.ReadQueryDataTable(strSelectQryResult);
                    //        for (int j = 0; j <= odtl_Result.Rows.Count - 1; j++)
                    //        {
                    //            _LabModuleResult = new gloStream.DiseaseManagement.Supporting.LabModuleTestResult();
                    //            var _with34 = _LabModuleResult;
                    //            _with34.ResultID = odtl_Result.Rows[j]["labotrd_ResultNameID"];
                    //            _with34.ResultName = odtl_Result.Rows[j]["labotrd_ResultName"];

                    //            _with33.LabModuleTestResults.Add(_LabModuleResult);
                    //            _LabModuleResult = null;
                    //        }
                    //        //j

                    //        _LabModuleTests.Add(ref _LabModuleTest);
                    //        _LabModuleTest = null;
                    //    }
                    //    //i
                    //    return _LabModuleTests;

                    //}
                    //catch (SqlException ex)
                    //{
                    //    MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //    UpdateLog("clsDiseaseManagement -- LabModuleTests -- " + ex.ToString());
                    //}
                    //catch (Exception ex)
                    //{
                    //    UpdateLog("clsDiseaseManagement -- LabModuleTests -- " + ex.ToString());
                    //    MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //}
                    //finally
                    //{
                    //    ODB.Disconnect();
                    //    ODB.Dispose();
                    //    ODB = null;
                    //}
                    return _LabModuleTests;
                }
                
                public DataTable LabModuleTest()
                {
                    //gloStream.gloDataBase.gloDataBase ODB = new gloStream.gloDataBase.gloDataBase();
                    DataTable dtTest = null;
                    //try
                    //{
                    //    ODB.Connect(GetConnectionString);
                    //    string strSelectQry = "SELECT DISTINCT labtm_Name,labtm_id FROM Lab_Test_Mst";
                    //    dtTest = ODB.ReadQueryDataTable(strSelectQry);
                    //    if (!(Information.IsDBNull(dtTest) == true))
                    //    {
                    //        return dtTest;
                    //    }

                    //}
                    //catch (SqlException ex)
                    //{
                    //    MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //    UpdateLog("clsDiseaseManagement -- LabModuleTest -- " + ex.ToString());
                    //}
                    //catch (Exception ex)
                    //{
                    //    UpdateLog("clsDiseaseManagement -- LabModuleTest -- " + ex.ToString());
                    //    MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //}
                    //finally
                    //{
                    //    ODB.Disconnect();
                    //    ODB.Dispose();
                    //    ODB = null;
                    //}
                    return dtTest;
                }
                //' chetan added for gettting chiefcomplaint from problemlist
                public DataTable GetProblemList(string strsearch = "")
                {
                    //gloStream.gloDataBase.gloDataBase ODB = new gloStream.gloDataBase.gloDataBase();
                    DataTable dtResult = null;
                    //ODB.Connect(GetConnectionString);
                    //try
                    //{
                    //    //Dim strSelectQry As String = "SELECT DISTINCT labotrd_ResultName FROM Lab_Order_Test_ResultDtl Where labotd_testid = " & TestID & ""
                    //    string strSelectQry = "";
                    //    if (!string.IsNullOrEmpty(strsearch.Trim()))
                    //    {
                    //        strSelectQry = "Select DISTINCT ISNULL(sCheifComplaint,'') as sChiefComplaint from ProblemList where sCheifComplaint like '%" + strsearch + "%' ";


                    //    }
                    //    else
                    //    {
                    //        strSelectQry = "Select DISTINCT ISNULL(sCheifComplaint,'') as sChiefComplaint from ProblemList ";

                    //    }
                    //    dtResult = ODB.ReadQueryDataTable(strSelectQry);
                    //    if (!(Information.IsDBNull(dtResult) == true))
                    //    {
                    //        return dtResult;
                    //    }
                    //}
                    //catch (SqlException ex)
                    //{
                    //    MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //    UpdateLog("clsDiseaseManagement -- GetProblemList -- " + ex.ToString());
                    //}
                    //catch (Exception ex)
                    //{
                    //    UpdateLog("clsDiseaseManagement -- GetProblemList -- " + ex.ToString());
                    //    MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //}
                    return dtResult;
                }

                public DataTable LabModuleResult(long TestID)
                {
                    //gloStream.gloDataBase.gloDataBase ODB = new gloStream.gloDataBase.gloDataBase();
                    DataTable dtResult = null;
                    //ODB.Connect(GetConnectionString);
                    //try
                    //{
                    //    //Dim strSelectQry As String = "SELECT DISTINCT labotrd_ResultName FROM Lab_Order_Test_ResultDtl Where labotd_testid = " & TestID & ""
                    //    string strSelectQry = "Select DISTINCT Lab_Order_Test_ResultDtl.labotrd_ResultName,Lab_Order_Test_ResultDtl.labotrd_ResultNameID FROM Lab_Order_Test_ResultDtl INNER JOIN Lab_Order_Test_Result ON Lab_Order_Test_ResultDtl.labotrd_TestID = " + TestID + " ";
                    //    dtResult = ODB.ReadQueryDataTable(strSelectQry);
                    //    if (!(Information.IsDBNull(dtResult) == true))
                    //    {
                    //        return dtResult;
                    //    }
                    //}
                    //catch (SqlException ex)
                    //{
                    //    MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //    UpdateLog("clsDiseaseManagement -- LabModuleResult -- " + ex.ToString());
                    //}
                    //catch (Exception ex)
                    //{
                    //    UpdateLog("clsDiseaseManagement -- LabModuleResult -- " + ex.ToString());
                    //    MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //}
                    //finally
                    //{
                    //    ODB.Disconnect();
                    //    ODB.Dispose();
                    //    ODB = null;
                    //}
                    return dtResult;
                }

                //-----------------------------------------------------------------------
                //
                //-----------------------------------------------------------------------
                public Collection Age()
                {
                    //Declare a collection object for age values
                    Collection _Age = new Collection();
                    try
                    {
                        //fill the collection object
                        var _with35 = _Age;
                        for (Int16 i = 0; i <= 125; i++)
                        {
                            _with35.Add(i);
                        }

                        //return the age collection
                        
                    }
                    catch (Exception ex)
                    {
                        _ErrorMessage = ex.Message;
                    }
                    finally
                    {
                        _Age = null;
                    }
                    return _Age;
                }

                public Collection Gender()
                {
                    //Male,Female,Other,All ' ref: gloEMR - patient registration form code file

                    //declare the collection object
                    Collection _Gender = new Collection();

                    try
                    {
                        //fill the collection object
                        var _with36 = _Gender;
                        _with36.Add("Male");
                        _with36.Add("Female");
                        _with36.Add("Other");
                        _with36.Add("All");

                        

                    }
                    catch //(Exception ex)
                    {
                        //UpdateLog("clsDiseaseManagement -- Gender -- " + ex.ToString());
                        //MessageBox.Show(ex.ToString(),clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        _Gender = null;
                    }
                    return _Gender;
                }

                public Collection Race()
                {
                    ////Category_Mst where type = race

                    ////declare the datareader and connection object
                    //gloStream.gloDataBase.gloDataBase ODB = new gloStream.gloDataBase.gloDataBase();
                    //System.Data.SqlClient.SqlDataReader oDataReader = null;
                    //string sqlstr = null;

                    ////declare the collection object
                    Collection _Race = new Collection();

                    //sqlstr = "SELECT sDescription FROM Category_MST WHERE sCategoryType = 'Race'";

                    ////Connect to the database
                    //ODB.Connect(GetConnectionString);

                    ////Get records and return a datareader
                    //oDataReader = ODB.ReadQueryRecords(sqlstr);

                    //try
                    //{
                    //    if ((oDataReader != null))
                    //    {
                    //        if (oDataReader.HasRows == true)
                    //        {
                    //            while (oDataReader.Read())
                    //            {
                    //                //Fill the collection object
                    //                var _with37 = _Race;
                    //                if (!Information.IsDBNull(oDataReader["sDescription"]))
                    //                {
                    //                    _with37.Add(oDataReader["sDescription"]);
                    //                }
                    //            }
                    //        }
                    //        oDataReader.Close();
                    //    }

                    //    return _Race;
                    //}
                    //catch (SqlException ex)
                    //{
                    //    MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //    UpdateLog("clsDiseaseManagement -- Race -- " + ex.ToString());
                    //}
                    //catch (Exception ex)
                    //{
                    //    UpdateLog("clsDiseaseManagement -- Race -- " + ex.ToString());
                    //    MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //}
                    //finally
                    //{
                    //    _Race = null;
                    //    ODB.Disconnect();
                    //    ODB.Dispose();
                    //    ODB = null;
                    //}
                    return _Race;
                }

                public Collection MaritalStatus()
                {
                    //Unmarried, Married, Single, Widowed, Divorced ' ' ref: gloEMR - patient registration form code file

                    //declare a collection object
                    Collection _MaritalStatus = new Collection();

                    try
                    {
                        //Fill the collection object
                        var _with38 = _MaritalStatus;
                        _with38.Add("Unmarried");
                        _with38.Add("Married");
                        _with38.Add("Single");
                        _with38.Add("Widowed");
                        _with38.Add("Divorced");

                        
                    }
                    catch //(Exception ex)
                    {
                        //UpdateLog("clsDiseaseManagement -- MaritalStatus -- " + ex.ToString());
                        //MessageBox.Show(ex.ToString(),clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        _MaritalStatus = null;
                    }

                    return _MaritalStatus;
                }

                public Collection State()
                {
                    ////CSZ_MST - Field Name = ST

                    ////declare the datareader and the connection object
                    //gloStream.gloDataBase.gloDataBase ODB = new gloStream.gloDataBase.gloDataBase();
                    //System.Data.SqlClient.SqlDataReader oDataReader = null;
                    //string sqlstr = null;

                    ////declare the collection object
                    Collection _State = new Collection();

                    //sqlstr = "SELECT DISTINCT ST FROM CSZ_MST";

                    ////Connect to the database
                    //ODB.Connect(GetConnectionString);

                    ////Execute the query ang return a datareader
                    //oDataReader = ODB.ReadQueryRecords(sqlstr);

                    //try
                    //{
                    //    //read from the datareader
                    //    if ((oDataReader != null))
                    //    {
                    //        if (oDataReader.HasRows == true)
                    //        {
                    //            while (oDataReader.Read())
                    //            {
                    //                //Fill the collection object
                    //                var _with39 = _State;
                    //                if (!Information.IsDBNull(oDataReader["ST"]))
                    //                {
                    //                    _with39.Add(oDataReader["ST"]);
                    //                }
                    //            }
                    //        }
                    //        oDataReader.Close();
                    //    }


                    //    return _State;
                    //}
                    //catch (SqlException ex)
                    //{
                    //    MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //    UpdateLog("clsDiseaseManagement -- State -- " + ex.ToString());
                    //}
                    //catch (Exception ex)
                    //{
                    //    UpdateLog("clsDiseaseManagement -- State -- " + ex.ToString());
                    //    MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //}
                    //finally
                    //{
                    //    ODB.Disconnect();
                    //    ODB.Dispose();
                    //    ODB = null;
                    //    _State = null;
                    //}
                    return _State;
                }

                public Collection EmploymentStatus()
                {
                    //Retired, Employed, Unemployed, Self-Employed, Student

                    //declare the collection object
                    Collection _EmploymentStatus = new Collection();

                    try
                    {
                        //fill the collection object
                        var _with40 = _EmploymentStatus;
                        _with40.Add("Retired");
                        _with40.Add("Employed");
                        _with40.Add("Unemployed");
                        _with40.Add("Self-Employed");
                        _with40.Add("Student");

                       
                    }
                    catch //(Exception ex)
                    {
                        //UpdateLog("clsDiseaseManagement -- EmploymentStatus -- " + ex.ToString());
                        //MessageBox.Show(ex.ToString(),clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        _EmploymentStatus = null;
                    }
                    return _EmploymentStatus;
                }

                public Criteria()
                    : base()
                {
                }

                //protected override void Finalize()
                //{
                //    base.Finalize();
                //}
            }

            public class Guidelines
            {

                private string _ErrorMessage;
                public string ErrorMessage
                {
                    get { return _ErrorMessage; }
                    set { _ErrorMessage = value; }
                }

                public gloStream.DiseaseManagement.Supporting.ItemDetails Guidelines1(string oType)
                {
                    //gloStream.gloDataBase.gloDataBase ODB = new gloStream.gloDataBase.gloDataBase();
                    //System.Data.SqlClient.SqlDataReader oDataReader = null;
                    gloStream.DiseaseManagement.Supporting.ItemDetails oGuidelines = new gloStream.DiseaseManagement.Supporting.ItemDetails();

                    //string sqlstr = null;

                    //try
                    //{
                    //    //      oGuidelines.Add()
                    //    ODB.Connect(GetConnectionString);

                    //    //sarika DM Denormalization 20090401

                    //    //sqlstr = "SELECT TemplateGallery_MST.nTemplateID, TemplateGallery_MST.sTemplateName"
                    //    //sqlstr &= " FROM TemplateGallery_MST INNER JOIN Category_MST ON TemplateGallery_MST.nCategoryID = Category_MST.nCategoryID"
                    //    //sqlstr &= " WHERE Category_MST.sCategoryType = 'Template' AND Category_MST.sDescription = '" & oType & "'"

                    //    sqlstr = "SELECT TemplateGallery_MST.nTemplateID, TemplateGallery_MST.sTemplateName, TemplateGallery_MST.sDescription";
                    //    sqlstr += " FROM TemplateGallery_MST INNER JOIN Category_MST ON TemplateGallery_MST.nCategoryID = Category_MST.nCategoryID";
                    //    sqlstr += " WHERE Category_MST.sCategoryType = 'Template' AND Category_MST.sDescription = '" + oType + "'";


                    //    //--------


                    //    oDataReader = ODB.ReadQueryRecords(sqlstr);

                    //    if ((oDataReader != null))
                    //    {
                    //        if (oDataReader.HasRows == true)
                    //        {
                    //            while (oDataReader.Read())
                    //            {
                    //                if (!(Information.IsDBNull(oDataReader["nTemplateID"]) & Information.IsDBNull(oDataReader["sTemplateName"])))
                    //                {
                    //                    oGuidelines.Add(oDataReader["nTemplateID"], oDataReader["sTemplateName"], oDataReader["sDescription"]);
                    //                }
                    //            }
                    //        }
                    //        oDataReader.Close();
                    //    }

                    //    ODB.Disconnect();

                    //    return oGuidelines;
                    //}
                    //catch (SqlException ex)
                    //{
                    //    MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //    UpdateLog("clsDiseaseManagement -- Guidelines -- " + ex.ToString());
                    //}
                    //catch (Exception ex)
                    //{
                    //    UpdateLog("clsDiseaseManagement -- Guidelines -- " + ex.ToString());
                    //    MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //}
                    //finally
                    //{
                    //    ODB = null;
                    //    oDataReader = null;
                    //    oGuidelines = null;
                    //}
                    return oGuidelines;
                }

                public DataTable GuidelinesTables(string oType)
                {
                    gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(clsGeneral.EMRConnectionString);
                    DataTable dtresult = new DataTable();
                    string sqlstr = null;


                    try
                    {
                        sqlstr = "SELECT TemplateGallery_MST.nTemplateID, TemplateGallery_MST.sTemplateName, TemplateGallery_MST.sDescription";
                        sqlstr += " FROM TemplateGallery_MST INNER JOIN Category_MST ON TemplateGallery_MST.nCategoryID = Category_MST.nCategoryID";
                        sqlstr += " WHERE Category_MST.sCategoryType = 'Template' AND Category_MST.sDescription  in ('Patient Education','Preventive Services','Wellness Guidelines')";

                        oDB.Retrive_Query(sqlstr, out dtresult);
                        if ((dtresult != null))
                        {
                            return dtresult;
                        }
                        //--------

                    }
                    catch //(Exception ex)
                    {
                        //UpdateLog("clsDiseaseManagement -- Guidelines -- " + ex.ToString());
                        //MessageBox.Show(ex.ToString(),clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {

                    }
                    return dtresult;
                }

                public Guidelines()
                    : base()
                {
                }

                //protected override void Finalize()
                //{
                //    base.Finalize();
                //}
            }

            public class GuidelinesType
            {

                public static string PreventiveServices
                {
                    get { return "Preventive Services"; }
                }

                public static string WellnessGuidelines
                {
                    get { return "Wellness Guidelines"; }
                }

                public static string PatientEducation
                {
                    get { return "Patient Education"; }
                }

                public GuidelinesType()
                    : base()
                {
                }

                //protected override void Finalize()
                //{
                //    base.Finalize();
                //}
            }

        }

        namespace Supporting
        {

            ////<<<<<<<<<<<<<< CATEGORY >>>>>>>>>>>>>>>>>>//

            public class Category
            {
                private long _ID;
                private string _Name;

                private string _Type = "History";
                public long ID
                {
                    get { return _ID; }
                    set { _ID = value; }
                }

                public string Name
                {
                    get { return _Name; }
                    set { _Name = value; }
                }

                private string Type
                {
                    get { return _Type; }
                }

                public Category()
                    : base()
                {
                }

                //protected override void Finalize()
                //{
                //    base.Finalize();
                //}
            }

            public class Categories : System.Collections.IEnumerable
            {

                private Collection mCol;
                //public gloStream.DiseaseManagement.Supporting.Category Add(ref gloStream.DiseaseManagement.Supporting.Category oCategory)
                //{
                //    mCol.Add(oCategory);
                //}

                public gloStream.DiseaseManagement.Supporting.Category Add(long ID, string Name)
                {
                    gloStream.DiseaseManagement.Supporting.Category functionReturnValue = null;
                    gloStream.DiseaseManagement.Supporting.Category objNewMember = null;
                    try
                    {
                        objNewMember = new gloStream.DiseaseManagement.Supporting.Category();
                        objNewMember.ID = ID;
                        objNewMember.Name = Name;
                        mCol.Add(objNewMember);
                        functionReturnValue = objNewMember;
                        objNewMember = null;
                    }
                    catch //(Exception ex)
                    {
                        //UpdateLog("clsDiseaseManagement -- Supporting -- Add -- " + ex.ToString());
                        //MessageBox.Show(ex.ToString(),clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    return functionReturnValue;
                }

                public gloStream.DiseaseManagement.Supporting.Category this[object vntIndexKey]
                {
                    get { return (Category)mCol[vntIndexKey]; }
                }

                public int Count
                {
                    get { return mCol.Count; }
                }

                //public System.Collections.IEnumerator GetEnumerator()
                //{
                //    //UPGRADE_TODO: Uncomment and change the following line to return the collection enumerator. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1055"'
                //    //GetEnumerator = mCol.GetEnumerator;
                //    //return mCol.GetEnumerator;
                //    //return (System.Collections.IEnumerator)mCol;
                //}

                public void Remove(ref object vntIndexKey)
                {
                    mCol.Remove(vntIndexKey.ToString());
                }

                public Categories()
                    : base()
                {
                    mCol = new Collection();
                }

                //protected override void Finalize()
                //{
                //    Clear();
                //    mCol = null;
                //    base.Finalize();
                //}

                public void Clear()
                {
                    if (mCol == null)
                        return;
                    // Shouldn't happen, but just in case.

                    int i = 0;
                    for (i = mCol.Count; i >= 1; i += -1)
                    {
                        mCol.Remove(i);
                    }
                }

                public IEnumerator GetEnumerator()
                {
                    throw new NotImplementedException();
                }
            }

            ////<<<<<<<<<<<<<< HISTORY >>>>>>>>>>>>>>>>>>//

            public class History
            {
                private string _Category;
                private long _CategoryID;

                private gloStream.DiseaseManagement.Supporting.HistoryItems _HistoryItems;
                public long CategoryID
                {
                    get { return _CategoryID; }
                    set { _CategoryID = value; }
                }

                public string Category
                {
                    get { return _Category; }
                    set { _Category = value; }
                }

                public gloStream.DiseaseManagement.Supporting.HistoryItems Items
                {
                    get { return _HistoryItems; }
                    set { _HistoryItems = value; }
                }

                public History()
                    : base()
                {
                    _HistoryItems = new gloStream.DiseaseManagement.Supporting.HistoryItems();
                }

                //protected override void Finalize()
                //{
                //    _HistoryItems = null;
                //    base.Finalize();
                //}

            }

            public class Histories : System.Collections.IEnumerable
            {

              
                private Collection mCol;
                //public gloStream.DiseaseManagement.Supporting.History Add(ref gloStream.DiseaseManagement.Supporting.History oHistory)
                //{
                //    mCol.Add(oHistory);
                //}

                public gloStream.DiseaseManagement.Supporting.History this[object vntIndexKey]
                {
                    get { return (History)mCol[vntIndexKey]; }
                }

                public int Count
                {
                    get { return mCol.Count; }
                }

                //public System.Collections.IEnumerator GetEnumerator()
                //{
                //    //UPGRADE_TODO: Uncomment and change the following line to return the collection enumerator. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1055"'
                //    //GetEnumerator = mCol.GetEnumerator
                //}

                public void Remove(ref object vntIndexKey)
                {
                    mCol.Remove(vntIndexKey.ToString());
                }

                public Histories()
                    : base()
                {
                    mCol = new Collection();
                }

                //protected override void Finalize()
                //{
                //    Clear();
                //    mCol = null;
                //    base.Finalize();
                //}

                public void Clear()
                {
                    if (mCol == null)
                        return;
                    // Shouldn't happen, but just in case.

                    int i = 0;
                    for (i = mCol.Count; i >= 1; i += -1)
                    {
                        mCol.Remove(i);
                    }
                }

                public IEnumerator GetEnumerator()
                {
                    throw new NotImplementedException();
                }
            }

            public class HistoryItem
            {
                private long _ID;
                private string _Name = "";
                private long _CategoryID;

                private string _CategoryName = "";
                public long ID
                {
                    get { return _ID; }
                    set { _ID = value; }
                }

                public string Name
                {
                    get { return _Name; }
                    set { _Name = value; }
                }

                public long CategoryID
                {
                    get { return _CategoryID; }
                    set { _CategoryID = value; }
                }

                public string CategoryName
                {
                    get { return _CategoryName; }
                    set { _CategoryName = value; }
                }

                public HistoryItem()
                    : base()
                {
                }

                //protected override void Finalize()
                //{
                //    base.Finalize();
                //}
            }

            public class HistoryItems : System.Collections.IEnumerable
            {

                private Collection mCol;
                //public gloStream.DiseaseManagement.Supporting.HistoryItem Add(ref gloStream.DiseaseManagement.Supporting.HistoryItem oHistory)
                //{
                //    return mCol.Add(oHistory);
                //}

                public gloStream.DiseaseManagement.Supporting.HistoryItem Add(long ID, string Name, Int64 CategoryID = 0, string CategoryName = "")
                {
                    gloStream.DiseaseManagement.Supporting.HistoryItem functionReturnValue = null;
                    gloStream.DiseaseManagement.Supporting.HistoryItem objNewMember = null;
                    try
                    {
                        objNewMember = new gloStream.DiseaseManagement.Supporting.HistoryItem();
                        objNewMember.ID = ID;
                        objNewMember.Name = Name;
                        objNewMember.CategoryID = CategoryID;
                        objNewMember.CategoryName = CategoryName;
                        mCol.Add(objNewMember);
                        functionReturnValue = objNewMember;
                        objNewMember = null;

                    }
                    catch //(Exception ex)
                    {
                        //UpdateLog("clsDiseaseManagement -- History -- Add -- " + ex.ToString());
                        //MessageBox.Show(ex.ToString(), clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    return functionReturnValue;

                }

                public gloStream.DiseaseManagement.Supporting.HistoryItem this[object vntIndexKey]
                {
                    get { return (HistoryItem)mCol[vntIndexKey]; }
                }

                public int Count
                {
                    get { return mCol.Count; }
                }

                //public System.Collections.IEnumerator GetEnumerator()
                //{
                //    //UPGRADE_TODO: Uncomment and change the following line to return the collection enumerator. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1055"'
                //    //GetEnumerator = mCol.GetEnumerator
                //}

                public void Remove(ref object vntIndexKey)
                {
                    mCol.Remove(vntIndexKey.ToString());
                }

                public HistoryItems()
                    : base()
                {
                    mCol = new Collection();
                }

                //protected override void Finalize()
                //{
                //    Clear();
                //    mCol = null;
                //    base.Finalize();
                //}

                public void Clear()
                {
                    if (mCol == null)
                        return;
                    // Shouldn't happen, but just in case.

                    int i = 0;
                    for (i = mCol.Count; i >= 1; i += -1)
                    {
                        mCol.Remove(i);
                    }
                }

                public IEnumerator GetEnumerator()
                {
                    throw new NotImplementedException();
                }
            }


            ////<<<<<<<<<<<<<< DRUGS >>>>>>>>>>>>>>>>>>//

            public class Drug
            {
                private long _ID = 0;
                private string _Name = "";
                private string _Dosage = "";
                private string _Route = "";
                private string _Frequency = "";

                private string _Duration = "";

                //sarika DM Denormalization
                private string _DrugName = "";
                //--

                //sarika DM Denormalization for Rx 20090410
                private string _DrugForm = "";
                private string _NDCCode = "";
                private int _IsNarcotics = 0;
                private long _mpid = 0;
                private string _DrugQtyQualifier = "";
                //---


                public long ID
                {
                    get { return _ID; }
                    set { _ID = value; }
                }

                public string Name
                {
                    get { return _Name; }
                    set { _Name = value; }
                }

                public string Dosage
                {
                    get { return _Dosage; }
                    set { _Dosage = value; }
                }

                public string Route
                {
                    get { return _Route; }
                    set { _Route = value; }
                }

                public string Frequency
                {
                    get { return _Frequency; }
                    set { _Frequency = value; }
                }

                public string Duration
                {
                    get { return _Duration; }
                    set { _Duration = value; }
                }


                //sarika DM Denormalization
                public string DrugName
                {
                    get { return _DrugName; }
                    set { _DrugName = value; }
                }

                //sarika DM Denormalization for Rx 20090410
                public string DrugForm
                {
                    get { return _DrugForm; }
                    set { _DrugForm = value; }
                }


                public string NDCCode
                {
                    get { return _NDCCode; }
                    set { _NDCCode = value; }
                }

                public int IsNarcotics
                {
                    get { return _IsNarcotics; }
                    set { _IsNarcotics = value; }
                }

                public long mpid
                {
                    get { return _mpid; }
                    set { _mpid = value; }
                }

                public string DrugQtyQualifier
                {
                    get { return _DrugQtyQualifier; }
                    set { _DrugQtyQualifier = value; }
                }
                //--


                public Drug()
                    : base()
                {
                }

                //protected override void Finalize()
                //{
                //    base.Finalize();
                //}
            }

            public class Drugs : System.Collections.IEnumerable
            {

                private Collection mCol;
                //public gloStream.DiseaseManagement.Supporting.Drug Add(ref gloStream.DiseaseManagement.Supporting.Drug oDrug)
                //{
                //    mCol.Add(oDrug);
                //}

                public gloStream.DiseaseManagement.Supporting.Drug Add(long ID, string Name, string Dosage, string Route, string Frequency, string Duration)
                {
                    gloStream.DiseaseManagement.Supporting.Drug functionReturnValue = null;
                    gloStream.DiseaseManagement.Supporting.Drug objNewMember = null;
                    try
                    {
                        objNewMember = new gloStream.DiseaseManagement.Supporting.Drug();
                        objNewMember.ID = ID;
                        objNewMember.Name = Name;
                        objNewMember.Dosage = Dosage;
                        objNewMember.Route = Route;
                        objNewMember.Frequency = Frequency;
                        objNewMember.Duration = Duration;

                        mCol.Add(objNewMember);
                        functionReturnValue = objNewMember;
                        objNewMember = null;
                    }
                    catch //(Exception ex)
                    {
                        //UpdateLog("clsDiseaseManagement -- Drugs -- Add -- " + ex.ToString());
                        //MessageBox.Show(ex.ToString(),clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    return functionReturnValue;

                }

                //sarika DM Denormalization for Rx 20090410
                public gloStream.DiseaseManagement.Supporting.Drug Add(long ID, string Name, string Dosage, string Route, string Frequency, string Duration, string DrugForm, string NDCCode, int IsNarcotics, long mpid,
                string DrugQtyQualifier)
                {
                    gloStream.DiseaseManagement.Supporting.Drug functionReturnValue = null;
                    gloStream.DiseaseManagement.Supporting.Drug objNewMember = null;
                    try
                    {
                        objNewMember = new gloStream.DiseaseManagement.Supporting.Drug();
                        objNewMember.ID = ID;
                        objNewMember.Name = Name;
                        objNewMember.Dosage = Dosage;
                        objNewMember.Route = Route;
                        objNewMember.Frequency = Frequency;
                        objNewMember.Duration = Duration;
                        objNewMember.NDCCode = NDCCode;
                        objNewMember.IsNarcotics = IsNarcotics;
                        objNewMember.mpid = mpid;
                        objNewMember.DrugQtyQualifier = DrugQtyQualifier;
                        mCol.Add(objNewMember);
                        functionReturnValue = objNewMember;
                        objNewMember = null;
                    }
                    catch //(Exception ex)
                    {
                        //UpdateLog("clsDiseaseManagement -- Drugs -- Add -- " + ex.ToString());
                        //MessageBox.Show(ex.ToString(),clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    return functionReturnValue;

                }
                //---

                public gloStream.DiseaseManagement.Supporting.Drug this[object vntIndexKey]
                {
                    get { return (Drug)mCol[vntIndexKey]; }
                }

                public int Count
                {
                    get { return mCol.Count; }
                }

                //public System.Collections.IEnumerator GetEnumerator()
                //{
                //    //UPGRADE_TODO: Uncomment and change the following line to return the collection enumerator. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1055"'
                //    //GetEnumerator = mCol.GetEnumerator
                //}

                public void Remove(ref object vntIndexKey)
                {
                    mCol.Remove(vntIndexKey.ToString());
                }

                public Drugs()
                    : base()
                {
                    mCol = new Collection();
                }

                //protected override void Finalize()
                //{
                //    Clear();
                //    mCol = null;
                //    base.Finalize();
                //}

                public void Clear()
                {
                    if (mCol == null)
                        return;
                    // Shouldn't happen, but just in case.

                    int i = 0;
                    for (i = mCol.Count; i >= 1; i += -1)
                    {
                        mCol.Remove(i);
                    }
                }

                public IEnumerator GetEnumerator()
                {
                    throw new NotImplementedException();
                }
            }


            ////<<<<<<<<<<<<<< ICD9 >>>>>>>>>>>>>>>>>>//

            public class ICD9
            {
                private long _ID;
                private string _Code;

                private string _Name;
                public long ID
                {
                    get { return _ID; }
                    set { _ID = value; }
                }

                public string Code
                {
                    get { return _Code; }
                    set { _Code = value; }
                }

                public string Name
                {
                    get { return _Name; }
                    set { _Name = value; }
                }

                public ICD9()
                    : base()
                {
                }

                //protected override void Finalize()
                //{
                //    base.Finalize();
                //}
            }

            public class ICD9s : System.Collections.IEnumerable
            {

                private Collection mCol;
                //public gloStream.DiseaseManagement.Supporting.ICD9 Add(ref gloStream.DiseaseManagement.Supporting.ICD9 oICD9)
                //{
                //    mCol.Add(oICD9);
                //}

                public gloStream.DiseaseManagement.Supporting.ICD9 Add(long ID, string Code, string Name)
                {
                    gloStream.DiseaseManagement.Supporting.ICD9 functionReturnValue = null;
                    gloStream.DiseaseManagement.Supporting.ICD9 objNewMember = null;
                    try
                    {
                        objNewMember = new gloStream.DiseaseManagement.Supporting.ICD9();
                        objNewMember.ID = ID;
                        objNewMember.Code = Code;
                        objNewMember.Name = Name;
                        mCol.Add(objNewMember);
                        functionReturnValue = objNewMember;
                        objNewMember = null;
                    }
                    catch //(Exception ex)
                    {
                        //UpdateLog("clsDiseaseManagement -- ICD9 -- Add -- " + ex.ToString());
                        //MessageBox.Show(ex.ToString(),clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    return functionReturnValue;
                }

                public gloStream.DiseaseManagement.Supporting.ICD9 this[object vntIndexKey]
                {
                    get { return (ICD9)mCol[vntIndexKey]; }
                }

                public int Count
                {
                    get { return mCol.Count; }
                }

                //public System.Collections.IEnumerator GetEnumerator()
                //{
                //    //UPGRADE_TODO: Uncomment and change the following line to return the collection enumerator. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1055"'
                //    //GetEnumerator = mCol.GetEnumerator
                //}

                public void Remove(ref object vntIndexKey)
                {
                    mCol.Remove(vntIndexKey.ToString());
                }

                public ICD9s()
                    : base()
                {
                    mCol = new Collection();
                }

                //protected override void Finalize()
                //{
                //    Clear();
                //    mCol = null;
                //    base.Finalize();
                //}

                public void Clear()
                {
                    if (mCol == null)
                        return;
                    // Shouldn't happen, but just in case.

                    int i = 0;
                    for (i = mCol.Count; i >= 1; i += -1)
                    {
                        mCol.Remove(i);
                    }
                }

                public IEnumerator GetEnumerator()
                {
                    throw new NotImplementedException();
                }
            }


            ////<<<<<<<<<<<<<< CPT >>>>>>>>>>>>>>>>>>//

            public class CPT
            {
                private long _ID;
                private string _Code;

                private string _Name;
                public long ID
                {
                    get { return _ID; }
                    set { _ID = value; }
                }

                public string Code
                {
                    get { return _Code; }
                    set { _Code = value; }
                }

                public string Name
                {
                    get { return _Name; }
                    set { _Name = value; }
                }

                public CPT()
                    : base()
                {
                }

                //protected override void Finalize()
                //{
                //    base.Finalize();
                //}
            }

            //---CPT Collection---
            public class CPTs : System.Collections.IEnumerable
            {

                private Collection mCol;
                //public gloStream.DiseaseManagement.Supporting.CPT Add(ref gloStream.DiseaseManagement.Supporting.CPT oCPT)
                //{
                //    mCol.Add(oCPT);
                //}

                public gloStream.DiseaseManagement.Supporting.CPT Add(long ID, string Code, string Name)
                {
                    gloStream.DiseaseManagement.Supporting.CPT functionReturnValue = null;
                    gloStream.DiseaseManagement.Supporting.CPT objNewMember = null;
                    try
                    {
                        objNewMember = new gloStream.DiseaseManagement.Supporting.CPT();
                        objNewMember.ID = ID;
                        objNewMember.Code = Code;
                        objNewMember.Name = Name;
                        mCol.Add(objNewMember);
                        functionReturnValue = objNewMember;
                        objNewMember = null;
                    }
                    catch //(Exception ex)
                    {
                        //UpdateLog("clsDiseaseManagement -- CPTs -- Add -- " + ex.ToString());
                        //MessageBox.Show(ex.ToString(),clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    return functionReturnValue;

                }

                public gloStream.DiseaseManagement.Supporting.CPT this[object vntIndexKey]
                {
                    get { return (CPT)mCol[vntIndexKey]; }
                }

                public int Count
                {
                    get { return mCol.Count; }
                }

                //public System.Collections.IEnumerator GetEnumerator()
                //{
                //    //UPGRADE_TODO: Uncomment and change the following line to return the collection enumerator. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1055"'
                //    //GetEnumerator = mCol.GetEnumerator
                //}

                public void Remove(ref object vntIndexKey)
                {
                    mCol.Remove(vntIndexKey.ToString());
                }

                public CPTs()
                    : base()
                {
                    mCol = new Collection();
                }

                //protected override void Finalize()
                //{
                //    Clear();
                //    mCol = null;
                //    base.Finalize();
                //}

                public void Clear()
                {
                    if (mCol == null)
                        return;
                    // Shouldn't happen, but just in case.

                    int i = 0;
                    for (i = mCol.Count; i >= 1; i += -1)
                    {
                        mCol.Remove(i);
                    }
                }

                public IEnumerator GetEnumerator()
                {
                    throw new NotImplementedException();
                }
            }

            ////<<<<<<<<<<<<<< ORDER >>>>>>>>>>>>>>>>>>//

            public class Order
            {
                private long _ID;
                private string _Category;

                private gloStream.DiseaseManagement.Supporting.OrderGroups _LabGroups;
                public long ID
                {
                    get { return _ID; }
                    set { _ID = value; }
                }

                public string Category
                {
                    get { return _Category; }
                    set { _Category = value; }
                }

                public gloStream.DiseaseManagement.Supporting.OrderGroups OrderGroups
                {
                    get { return _LabGroups; }
                    set { _LabGroups = value; }
                }

                public Order()
                    : base()
                {
                    _LabGroups = new gloStream.DiseaseManagement.Supporting.OrderGroups();
                }

                //protected override void Finalize()
                //{
                //    _LabGroups = null;
                //    base.Finalize();
                //}

            }

            //--- ORDER Collection---
            public class Orders : System.Collections.IEnumerable
            {

                private Collection mCol;
                //public gloStream.DiseaseManagement.Supporting.Order Add(ref gloStream.DiseaseManagement.Supporting.Order oLab)
                //{
                //    mCol.Add(oLab);
                //}

                public gloStream.DiseaseManagement.Supporting.Order this[object vntIndexKey]
                {
                    get { return (Order) mCol[vntIndexKey]; }
                }

                public int Count
                {
                    get { return mCol.Count; }
                }

                //public System.Collections.IEnumerator GetEnumerator()
                //{
                //    //UPGRADE_TODO: Uncomment and change the following line to return the collection enumerator. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1055"'
                //    //GetEnumerator = mCol.GetEnumerator
                //}

                public void Remove(ref object vntIndexKey)
                {
                    mCol.Remove(vntIndexKey.ToString());
                }

                public Orders()
                    : base()
                {
                    mCol = new Collection();
                }

                //protected override void Finalize()
                //{
                //    Clear();
                //    mCol = null;
                //    base.Finalize();
                //}

                public void Clear()
                {
                    if (mCol == null)
                        return;
                    // Shouldn't happen, but just in case.

                    int i = 0;
                    for (i = mCol.Count; i >= 1; i += -1)
                    {
                        mCol.Remove(i);
                    }
                }

                public IEnumerator GetEnumerator()
                {
                    throw new NotImplementedException();
                }

            }

            //---ORDER Group Class with Tests Collection---
            public class OrderGroup
            {
                private long _ID;
                private string _Name;

                private gloStream.DiseaseManagement.Supporting.ItemDetails _Tests;
                public long ID
                {
                    get { return _ID; }
                    set { _ID = value; }
                }

                public string Name
                {
                    get { return _Name; }
                    set { _Name = value; }
                }

                public gloStream.DiseaseManagement.Supporting.ItemDetails Tests
                {
                    get { return _Tests; }
                    set { _Tests = value; }
                }

                public OrderGroup()
                    : base()
                {
                    _Tests = new gloStream.DiseaseManagement.Supporting.ItemDetails();
                }

                //protected override void Finalize()
                //{
                //    _Tests = null;
                //    base.Finalize();
                //}

            }

            //---ORDER Group collection Class with Tests Collection---
            public class OrderGroups : System.Collections.IEnumerable
            {

                private Collection mCol;
                //public gloStream.DiseaseManagement.Supporting.OrderGroup Add(ref gloStream.DiseaseManagement.Supporting.OrderGroup oLabGroup)
                //{
                //    mCol.Add(oLabGroup);
                //}

                //'Public Function Add(ByVal oID As Long, ByVal oDescription As String) As gloStream.DiseaseManagement.Supporting.ItemDetail
                //'    'create a new object
                //'    Dim objNewMember As gloStream.DiseaseManagement.Supporting.ItemDetail
                //'    objNewMember = New gloStream.DiseaseManagement.Supporting.ItemDetail
                //'    objNewMember.ID = oID
                //'    objNewMember.Description = oDescription
                //'    mCol.Add(objNewMember)
                //'    Add = objNewMember
                //'    objNewMember = Nothing
                //'End Function

                public gloStream.DiseaseManagement.Supporting.OrderGroup this[object vntIndexKey]
                {
                    get { return (OrderGroup)mCol[vntIndexKey]; }
                }

                public int Count
                {
                    get { return mCol.Count; }
                }

                //public System.Collections.IEnumerator GetEnumerator()
                //{
                //    //UPGRADE_TODO: Uncomment and change the following line to return the collection enumerator. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1055"'
                //    //GetEnumerator = mCol.GetEnumerator
                //}

                public void Remove(ref object vntIndexKey)
                {
                    mCol.Remove(vntIndexKey.ToString());
                }

                public OrderGroups()
                    : base()
                {
                    mCol = new Collection();
                }

                //protected override void Finalize()
                //{
                //    Clear();
                //    mCol = null;
                //    base.Finalize();
                //}

                public void Clear()
                {
                    if (mCol == null)
                        return;
                    // Shouldn't happen, but just in case.

                    int i = 0;
                    for (i = mCol.Count; i >= 1; i += -1)
                    {
                        mCol.Remove(i);
                    }
                }

                public IEnumerator GetEnumerator()
                {
                    throw new NotImplementedException();
                }
            }


            ////<<<<<<<<<<<<<< NEW LAB MODULE TEST RESULT >>>>>>>>>>>>>>>>>>//

            public enum enumTestModuleResultType
            {
                None = 0,
                SingleResult = 1,
                ProfileResult = 2
            }

            public enum enumTestModuleResultValueType
            {
                None = 0,
                Text = 1,
                Numeric = 2
            }

            public enum enumTestModuleResultReadType
            {
                None = 0,
                Prilimnary = 1,
                Final = 2,
                Ammend = 3
            }

            public class LabModuleTest
            {
                private Int64 _TestID = 0;
                private string _Code = "";
                private string _Name = "";
                private enumTestModuleResultType _ResultType = enumTestModuleResultType.None;

                private LabModuleTestResults _LabModuleTestResults;
                public Int64 TestID
                {
                    get { return _TestID; }
                    set { _TestID = value; }
                }

                public string Code
                {
                    get { return _Code; }
                    set { _Code = value; }
                }

                public string Name
                {
                    get { return _Name; }
                    set { _Name = value; }
                }

                public enumTestModuleResultType ResultType
                {
                    get { return _ResultType; }
                    set { _ResultType = value; }
                }

                public LabModuleTestResults LabModuleTestResults
                {
                    get { return _LabModuleTestResults; }
                    set { _LabModuleTestResults = value; }
                }


                public LabModuleTest()
                    : base()
                {
                    _LabModuleTestResults = new LabModuleTestResults();
                }

                //protected override void Finalize()
                //{
                //    _LabModuleTestResults = null;
                //    base.Finalize();
                //}
            }

            public class LabModuleTests : System.Collections.IEnumerable
            {

                private Collection mCol;
                //public gloStream.DiseaseManagement.Supporting.LabModuleTest Add(ref gloStream.DiseaseManagement.Supporting.LabModuleTest oItemDetail)
                //{
                //    mCol.Add(oItemDetail);
                //}

                public gloStream.DiseaseManagement.Supporting.LabModuleTest this[object vntIndexKey]
                {
                    get { return (LabModuleTest)mCol[vntIndexKey]; }
                }

                public int Count
                {
                    get { return mCol.Count; }
                }

                //public System.Collections.IEnumerator GetEnumerator()
                //{
                //    //UPGRADE_TODO: Uncomment and change the following line to return the collection enumerator. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1055"'
                //    //GetEnumerator = mCol.GetEnumerator
                //}

                public void Remove(ref object vntIndexKey)
                {
                    mCol.Remove(vntIndexKey.ToString());
                }

                public LabModuleTests()
                    : base()
                {
                    mCol = new Collection();
                }

                //protected override void Finalize()
                //{
                //    Clear();
                //    mCol = null;
                //    base.Finalize();
                //}

                public void Clear()
                {
                    if (mCol == null)
                        return;
                    // Shouldn't happen, but just in case.

                    int i = 0;
                    for (i = mCol.Count; i >= 1; i += -1)
                    {
                        mCol.Remove(i);
                    }
                }


                public IEnumerator GetEnumerator()
                {
                    throw new NotImplementedException();
                }
            }

            public class LabModuleTestResult
            {
                private Int64 _TestID = 0;
                private Int64 _ResultID = 0;
                private string _ResultName = "";
                private enumTestModuleResultValueType _ValueType = enumTestModuleResultValueType.None;
                private string _Unit = "";
                private string _Operators;
                private string _ResultValue1;

                private string _ResultValue2;
                public Int64 TestID
                {
                    get { return _TestID; }
                    set { _TestID = value; }
                }

                public Int64 ResultID
                {
                    get { return _ResultID; }
                    set { _ResultID = value; }
                }

                public string ResultName
                {
                    get { return _ResultName; }
                    set { _ResultName = value; }
                }

                public enumTestModuleResultValueType ValueType
                {
                    get { return _ValueType; }
                    set { _ValueType = value; }
                }

                public string Unit
                {
                    get { return _Unit; }
                    set { _Unit = value; }
                }

                public string Operators
                {
                    get { return _Operators; }
                    set { _Operators = value; }
                }

                public string ResultValue1
                {
                    get { return _ResultValue1; }
                    set { _ResultValue1 = value; }
                }

                public string ResultValue2
                {
                    get { return _ResultValue2; }
                    set { _ResultValue2 = value; }
                }

                public LabModuleTestResult()
                    : base()
                {
                }

                //protected override void Finalize()
                //{
                //    base.Finalize();
                //}
            }

            public class LabModuleTestResults : System.Collections.IEnumerable
            {

                private Collection mCol;
                //public gloStream.DiseaseManagement.Supporting.LabModuleTestResult Add(ref gloStream.DiseaseManagement.Supporting.LabModuleTestResult oItemDetail)
                //{
                //    mCol.Add(oItemDetail);
                //}

                public gloStream.DiseaseManagement.Supporting.LabModuleTestResult Add(Int64 oTestID, Int64 oResultID, string oResultName, enumTestModuleResultValueType oValueType, string oUnit, string oOperators, string oResultValue1, string oResultValue2)
                {
                    gloStream.DiseaseManagement.Supporting.LabModuleTestResult functionReturnValue = null;
                    //create a new object
                    gloStream.DiseaseManagement.Supporting.LabModuleTestResult objNewMember = null;
                    try
                    {
                        objNewMember = new gloStream.DiseaseManagement.Supporting.LabModuleTestResult();
                        var _with41 = objNewMember;
                        _with41.TestID = oTestID;
                        _with41.ResultID = oResultID;
                        _with41.ResultName = oResultName;
                        _with41.ValueType = oValueType;
                        _with41.Unit = oUnit;
                        _with41.Operators = oOperators;
                        _with41.ResultValue1 = oResultValue1;
                        _with41.ResultValue2 = oResultValue2;
                        mCol.Add(objNewMember);
                        functionReturnValue = objNewMember;
                        objNewMember = null;
                    }
                    catch //(Exception ex)
                    {
                        //UpdateLog("clsDiseaseManagement -- LabModuleTestResults -- Add -- " + ex.ToString());
                        //MessageBox.Show(ex.ToString(),clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    return functionReturnValue;

                }

                public gloStream.DiseaseManagement.Supporting.LabModuleTestResult this[object vntIndexKey]
                {
                    get { return (LabModuleTestResult)mCol[vntIndexKey]; }
                }

                public int Count
                {
                    get { return mCol.Count; }
                }

                //public System.Collections.IEnumerator GetEnumerator()
                //{
                //    //UPGRADE_TODO: Uncomment and change the following line to return the collection enumerator. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1055"'
                //    //GetEnumerator = mCol.GetEnumerator
                //}

                public void Remove(ref object vntIndexKey)
                {
                    mCol.Remove(vntIndexKey.ToString());
                }

                public LabModuleTestResults()
                    : base()
                {
                    mCol = new Collection();
                }

                //protected override void Finalize()
                //{
                //    Clear();
                //    mCol = null;
                //    base.Finalize();
                //}

                public void Clear()
                {
                    if (mCol == null)
                        return;
                    // Shouldn't happen, but just in case.

                    int i = 0;
                    for (i = mCol.Count; i >= 1; i += -1)
                    {
                        mCol.Remove(i);
                    }
                }


                public IEnumerator GetEnumerator()
                {
                    throw new NotImplementedException();
                }
            }

            ////<<<<<<<<<<<<<< COMMON ITEM ID AND NAME >>>>>>>>>>>>>>>>>>//

            public class ItemDetail
            {
                private long _ItemID;

                private string _ItemDescription;
                //sarika DM Denormalization
                private byte[] _Template = null;
                //----

                public long ID
                {
                    get { return _ItemID; }
                    set { _ItemID = value; }
                }

                public string Description
                {
                    get { return _ItemDescription; }
                    set { _ItemDescription = value; }
                }

                //sarika DM Denormalization
                public byte[] Template
                {
                    get { return _Template; }
                    set { _Template = value; }
                }
                //----

                public ItemDetail()
                    : base()
                {
                }

                //protected override void Finalize()
                //{
                //    base.Finalize();
                //}
            }

            public class ItemDetails : System.Collections.IEnumerable
            {

                private Collection mCol;
                //public gloStream.DiseaseManagement.Supporting.ItemDetail Add(ref gloStream.DiseaseManagement.Supporting.ItemDetail oItemDetail)
                //{
                //    mCol.Add(oItemDetail);
                //}

                public gloStream.DiseaseManagement.Supporting.ItemDetail Add(long oID, string oDescription)
                {
                    gloStream.DiseaseManagement.Supporting.ItemDetail functionReturnValue = null;
                    //create a new object
                    gloStream.DiseaseManagement.Supporting.ItemDetail objNewMember = null;
                    try
                    {
                        objNewMember = new gloStream.DiseaseManagement.Supporting.ItemDetail();
                        objNewMember.ID = oID;
                        objNewMember.Description = oDescription;
                        mCol.Add(objNewMember);
                        functionReturnValue = objNewMember;
                        objNewMember = null;
                    }
                    catch //(Exception ex)
                    {
                        //UpdateLog("clsDiseaseManagement -- ItemDetails -- Add -- " + ex.ToString());
                        //MessageBox.Show(ex.ToString(),clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    return functionReturnValue;
                }

                //sarika DM Denormalization

                public gloStream.DiseaseManagement.Supporting.ItemDetail Add(long oID, string oDescription, byte[] oTemplate)
                {
                    gloStream.DiseaseManagement.Supporting.ItemDetail functionReturnValue = null;
                    //create a new object
                    gloStream.DiseaseManagement.Supporting.ItemDetail objNewMember = null;
                    try
                    {
                        objNewMember = new gloStream.DiseaseManagement.Supporting.ItemDetail();
                        objNewMember.ID = oID;
                        objNewMember.Description = oDescription;
                        objNewMember.Template = oTemplate;
                        mCol.Add(objNewMember);
                        functionReturnValue = objNewMember;
                        objNewMember = null;
                    }
                    catch //(Exception ex)
                    {
                        //UpdateLog("clsDiseaseManagement -- ItemDetails -- Add -- " + ex.ToString());
                        //MessageBox.Show(ex.ToString(),clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    return functionReturnValue;
                }
                //-----

                public gloStream.DiseaseManagement.Supporting.ItemDetail this[object vntIndexKey]
                {
                    get { return (ItemDetail)mCol[vntIndexKey]; }
                }

                public int Count
                {
                    get { return mCol.Count; }
                }

                //public System.Collections.IEnumerator GetEnumerator()
                //{
                //    //UPGRADE_TODO: Uncomment and change the following line to return the collection enumerator. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1055"'
                //    //GetEnumerator = mCol.GetEnumerator
                //}

                public void Remove(ref object vntIndexKey)
                {
                    mCol.Remove(vntIndexKey.ToString());
                }

                public ItemDetails()
                    : base()
                {
                    mCol = new Collection();
                }

                //protected override void Finalize()
                //{
                //    Clear();
                //    mCol = null;
                //    base.Finalize();
                //}

                public void Clear()
                {
                    if (mCol == null)
                        return;
                    // Shouldn't happen, but just in case.

                    int i = 0;
                    for (i = mCol.Count; i >= 1; i += -1)
                    {
                        mCol.Remove(i);
                    }
                }

                public IEnumerator GetEnumerator()
                {
                    throw new NotImplementedException();
                }
            }

            ////<<<<<<<<<<<<<< CRITERIA PARAMETERS >>>>>>>>>>>>>>>>>>//

            public class Criteria
            {
                private long _ID = 0;
                private string _Name = "";
                private double _Age_Minimum = 0;
                private double _Age_Maximum = 0;
                private string _Gender = "";
                private string _Race = "";
                private string _MaritalStatus = "";
                private string _City = "";
                private string _State = "";
                private string _Zip = "";
                private string _EmployementStatus = "";
                private string _Height_Minimum = "";
                private string _Height_Maximum = "";
                private double _Weight_Minimum = 0;
                private double _Weight_Maximum = 0;
                private double _BMI_Minimum = 0;
                private double _BMI_Maximum = 0;
                private double _Temprature_Minimum = 0;
                private double _Temprature_Maximum = 0;
                private double _Pulse_Minimum = 0;
                private double _Pulse_Maximum = 0;
                private double _PulseOX_Minimum = 0;
                private double _PulseOX_Maximum = 0;
                private double _BPSitting_Minimum = 0;
                private double _BPSitting_Maximum = 0;
                private double _BPStanding_Minimum = 0;
                private double _BPStanding_Maximum = 0;
                private string _DisplayMessage = "";

                private Supporting.OtherDetails _OtherDetails;
                private Collection _Histories;
                private Collection _Drugs;
                private Collection _ICD9s;
                private Collection _CPTs;
                private gloStream.DiseaseManagement.Supporting.Criteria_Labs _Labs;
                //Here we are not creating seprate class for criteria of Lab Module, bcz its already cover in selection class with name LabModuleTestResults

                private gloStream.DiseaseManagement.Supporting.LabModuleTestResults _LabModuleTests;
                private Collection _Guidelines;
                private Collection _LabOrders;
                private Collection _RadiologyOrders;
                private Collection _RxDrugs;

                private Collection _Referrals;
                ///''''''Added by Chetan on 09 Oct 2010 - for IM in DM Setup
                private Collection _IMlst;
                ///''''''Added by Chetan on 09 Oct 2010 - for IM in DM Setup

                public long ID
                {
                    get { return _ID; }
                    set { _ID = value; }
                }

                public string Name
                {
                    get { return _Name; }
                    set { _Name = value; }
                }

                public double AgeMinimum
                {
                    get { return _Age_Minimum; }
                    set { _Age_Minimum = value; }
                }

                public double AgeMaximum
                {
                    get { return _Age_Maximum; }
                    set { _Age_Maximum = value; }
                }

                public string Gender
                {
                    get { return _Gender; }
                    set { _Gender = value; }
                }

                public string Race
                {
                    get { return _Race; }
                    set { _Race = value; }
                }

                public string MaritalStatus
                {
                    get { return _MaritalStatus; }
                    set { _MaritalStatus = value; }
                }

                public string City
                {
                    get { return _City; }
                    set { _City = value; }
                }

                public string State
                {
                    get { return _State; }
                    set { _State = value; }
                }

                public string Zip
                {
                    get { return _Zip; }
                    set { _Zip = value; }
                }

                public string EmployeeStatus
                {
                    get { return _EmployementStatus; }
                    set { _EmployementStatus = value; }
                }

                public string HeightMinimum
                {
                    get { return _Height_Minimum; }
                    set { _Height_Minimum = value; }
                }

                public string HeightMaximum
                {
                    get { return _Height_Maximum; }
                    set { _Height_Maximum = value; }
                }

                public double WeightMinimum
                {
                    get { return _Weight_Minimum; }
                    set { _Weight_Minimum = value; }
                }

                public double WeightMaximum
                {
                    get { return _Weight_Maximum; }
                    set { _Weight_Maximum = value; }
                }

                public double BMIMinimum
                {
                    get { return _BMI_Minimum; }
                    set { _BMI_Minimum = value; }
                }

                public double BMIMaximum
                {
                    get { return _BMI_Maximum; }
                    set { _BMI_Maximum = value; }
                }

                public double TempratureMinumum
                {
                    get { return _Temprature_Minimum; }
                    set { _Temprature_Minimum = value; }
                }

                public double TempratureMaximum
                {
                    get { return _Temprature_Maximum; }
                    set { _Temprature_Maximum = value; }
                }

                public double PulseMinimum
                {
                    get { return _Pulse_Minimum; }
                    set { _Pulse_Minimum = value; }
                }

                public double PulseMaximum
                {
                    get { return _Pulse_Maximum; }
                    set { _Pulse_Maximum = value; }
                }

                public double PulseOXMinimum
                {
                    get { return _PulseOX_Minimum; }
                    set { _PulseOX_Minimum = value; }
                }

                public double PulseOXMaximum
                {
                    get { return _PulseOX_Maximum; }
                    set { _PulseOX_Maximum = value; }
                }

                public double BPSittingMinimum
                {
                    get { return _BPSitting_Minimum; }
                    set { _BPSitting_Minimum = value; }
                }

                public double BPSittingMaximum
                {
                    get { return _BPSitting_Maximum; }
                    set { _BPSitting_Maximum = value; }
                }

                public double BPStandingMinimum
                {
                    get { return _BPStanding_Minimum; }
                    set { _BPStanding_Minimum = value; }
                }

                public double BPStandingMaximum
                {
                    get { return _BPStanding_Maximum; }
                    set { _BPStanding_Maximum = value; }
                }

                public string DisplayMessage
                {
                    get { return _DisplayMessage; }
                    set { _DisplayMessage = value; }
                }

                public Supporting.OtherDetails OtherDetails
                {
                    get { return _OtherDetails; }
                    set { _OtherDetails = value; }
                }

                public Collection Histories
                {
                    get { return _Histories; }
                    set { _Histories = value; }
                }

                public Collection Drugs
                {
                    get { return _Drugs; }
                    set { _Drugs = value; }
                }

                public Collection ICD9s
                {
                    get { return _ICD9s; }
                    set { _ICD9s = value; }
                }

                public Collection CPTs
                {
                    get { return _CPTs; }
                    set { _CPTs = value; }
                }

                public gloStream.DiseaseManagement.Supporting.Criteria_Labs Labs
                {
                    get { return _Labs; }
                    set { _Labs = value; }
                }

                public gloStream.DiseaseManagement.Supporting.LabModuleTestResults LabModuleTests
                {
                    get { return _LabModuleTests; }
                    set { _LabModuleTests = value; }
                }

                public Collection Guidelines
                {
                    get { return _Guidelines; }
                    set { _Guidelines = value; }
                }

                public Collection LabOrders
                {
                    get { return _LabOrders; }
                    set { _LabOrders = value; }
                }
                public Collection RadiologyOrders
                {
                    get { return _RadiologyOrders; }
                    set { _RadiologyOrders = value; }
                }
                public Collection RxDrugs
                {
                    get { return _RxDrugs; }
                    set { _RxDrugs = value; }
                }
                public Collection Referrals
                {
                    get { return _Referrals; }
                    set { _Referrals = value; }
                }
                public Collection IMlst
                {
                    ///''''''Added by Ujwala Atre as on 20100907 - for IM in DM Setup
                    get { return _IMlst; }
                    set { _IMlst = value; }
                }
                ///''''''Added by Ujwala Atre as on 20100907 - for IM in DM Setup



                public Criteria()
                    : base()
                {
                    _Histories = new Collection();
                    _Drugs = new Collection();
                    _ICD9s = new Collection();
                    _CPTs = new Collection();
                    _Guidelines = new Collection();
                    _LabOrders = new Collection();
                    _RadiologyOrders = new Collection();
                    _RxDrugs = new Collection();
                    _Referrals = new Collection();

                    _Labs = new gloStream.DiseaseManagement.Supporting.Criteria_Labs();
                    _Guidelines = new Collection();
                    _LabModuleTests = new gloStream.DiseaseManagement.Supporting.LabModuleTestResults();
                    _IMlst = new Collection();

                }

                //protected override void Finalize()
                //{
                //    _Histories = null;
                //    _Drugs = null;
                //    _ICD9s = null;
                //    _CPTs = null;
                //    _Labs = null;
                //    _Guidelines = null;
                //    _LabModuleTests = null;
                //    _Guidelines = null;
                //    _LabOrders = null;
                //    _RadiologyOrders = null;
                //    _RxDrugs = null;
                //    _Referrals = null;
                //    _IMlst = null;
                //    base.Finalize();
                //}
            }

            public class PatientDetail
            {

                private Int64 _CriteriaID = 0;
                private string _CriteriaName = "";
                private long _PatientID = 0;
                private string _PatientCode = "";
                private string _PatientName = "";
                private double _Age = 0;
                private string _Gender = "";
                private string _Race = "";
                private string _MaritalStatus = "";
                private string _City = "";
                private string _State = "";
                private string _Zip = "";
                private string _EmployementStatus = "";
                private string _Height = "";
                private double _Weight = 0;
                private double _BMI = 0;
                private double _Temprature = 0;
                private double _Pulse = 0;
                private double _PulseOX = 0;
                private double _BPSitting_Minimum = 0;
                private double _BPSitting_Maximum = 0;
                private double _BPStanding_Minimum = 0;
                private double _BPStanding_Maximum = 0;

                private System.DateTime _VitalDate;

                private OtherDetails mOtherDetails = new OtherDetails();
                public PatientDetail()
                    : base()
                {
                }

                //protected override void Finalize()
                //{
                //    base.Finalize();
                //}

                public long CriteriaID
                {
                    get { return _CriteriaID; }
                    set { _CriteriaID = value; }
                }

                public string CriteriaName
                {
                    get { return _CriteriaName; }
                    set { _CriteriaName = value; }
                }

                public long PatientID
                {
                    get { return _PatientID; }
                    set { _PatientID = value; }
                }

                public string PatientCode
                {
                    get { return _PatientCode; }
                    set { _PatientCode = value; }
                }

                public string PatientName
                {
                    get { return _PatientName; }
                    set { _PatientName = value; }
                }

                public double Age
                {
                    get { return _Age; }
                    set { _Age = value; }
                }

                public string Gender
                {
                    get { return _Gender; }
                    set { _Gender = value; }
                }

                public string Race
                {
                    get { return _Race; }
                    set { _Race = value; }
                }

                public string MaritalStatus
                {
                    get { return _MaritalStatus; }
                    set { _MaritalStatus = value; }
                }

                public string City
                {
                    get { return _City; }
                    set { _City = value; }
                }

                public string State
                {
                    get { return _State; }
                    set { _State = value; }
                }

                public string Zip
                {
                    get { return _Zip; }
                    set { _Zip = value; }
                }

                public string EmployeeStatus
                {
                    get { return _EmployementStatus; }
                    set { _EmployementStatus = value; }
                }

                public string Height
                {
                    get { return _Height; }
                    set { _Height = value; }
                }

                public double WeightInlbs
                {
                    get { return _Weight; }
                    set { _Weight = value; }
                }

                public double BMI
                {
                    get { return _BMI; }
                    set { _BMI = value; }
                }

                public double TempratureInF
                {
                    get { return _Temprature; }
                    set { _Temprature = value; }
                }


                public double Pulse
                {
                    get { return _Pulse; }
                    set { _Pulse = value; }
                }

                public double PulseOX
                {
                    get { return _PulseOX; }
                    set { _PulseOX = value; }
                }

                public double BPSittingMinimum
                {
                    get { return _BPSitting_Minimum; }
                    set { _BPSitting_Minimum = value; }
                }

                public double BPSittingMaximum
                {
                    get { return _BPSitting_Maximum; }
                    set { _BPSitting_Maximum = value; }
                }

                public double BPStandingMinimum
                {
                    get { return _BPStanding_Minimum; }
                    set { _BPStanding_Minimum = value; }
                }

                public double BPStandingMaximum
                {
                    get { return _BPStanding_Maximum; }
                    set { _BPStanding_Maximum = value; }
                }

                public System.DateTime VitalDate
                {
                    get { return _VitalDate; }
                    set { _VitalDate = value; }
                }

                public OtherDetails OtherDetails
                {
                    get { return mOtherDetails; }
                    set { mOtherDetails = value; }
                }

            }

            public enum enumDetailType
            {
                None = 0,
                History = 1,
                Medication = 2,
                ICD9 = 3,
                CPT = 4,
                Lab = 5,
                Order = 6,
                Problemlist = 7
            }

            public class OtherDetail
            {
                private long mItemID = 0;
                private long mCategoryID = 0;
                private string mCategoryName = "";
                private string mItemName = "";
                private string mOperator = "";
                private string mResult1 = "";
                private string mResult2 = "";
                private System.DateTime mItemDate;

                private enumDetailType mDetailType;
                public long ItemID
                {
                    get { return mItemID; }
                    set { mItemID = value; }
                }

                public long CategoryID
                {
                    get { return mCategoryID; }
                    set { mCategoryID = value; }
                }

                public string CategoryName
                {
                    get { return mCategoryName; }
                    set { mCategoryName = value; }
                }

                public string ItemName
                {
                    get { return mItemName; }
                    set { mItemName = value; }
                }

                public string OperatorName
                {
                    get { return mOperator; }
                    set { mOperator = value; }
                }

                public string Result1
                {
                    get { return mResult1; }
                    set { mResult1 = value; }
                }

                public string Result2
                {
                    get { return mResult2; }
                    set { mResult2 = value; }
                }

                public System.DateTime ItemDate
                {
                    get { return mItemDate; }
                    set { mItemDate = value; }
                }

                public enumDetailType DetailType
                {
                    get { return mDetailType; }
                    set { mDetailType = value; }
                }

                public OtherDetail()
                {
                }

                //protected override void Finalize()
                //{
                //    base.Finalize();
                //}

            }

            public class OtherDetails : System.Collections.IEnumerable
            {

                private Collection mCol;
                public void Add(gloStream.DiseaseManagement.Supporting.OtherDetail oOtherDetail)
                {
                    mCol.Add(oOtherDetail);
                  
                }

                public gloStream.DiseaseManagement.Supporting.OtherDetail Add(Int64 ItemID, Int64 CategoryID, string CategoryName, string ItemName, string OperatorName, string Result1, string Result2)
                {
                    gloStream.DiseaseManagement.Supporting.OtherDetail functionReturnValue = null;
                    //create a new object
                    gloStream.DiseaseManagement.Supporting.OtherDetail oOtherDetail = null;
                    try
                    {
                        oOtherDetail = new gloStream.DiseaseManagement.Supporting.OtherDetail();
                        oOtherDetail.ItemID = ItemID;
                        oOtherDetail.CategoryID = CategoryID;
                        oOtherDetail.CategoryName = CategoryName;
                        oOtherDetail.ItemName = ItemName;
                        oOtherDetail.OperatorName = OperatorName;
                        oOtherDetail.Result1 = Result1;
                        oOtherDetail.Result2 = Result2;
                        mCol.Add(oOtherDetail);
                        functionReturnValue = oOtherDetail;
                        oOtherDetail = null;
                    }
                    catch //(Exception ex)
                    {
                        //UpdateLog("clsCardioVascular -- ItemDetails -- Add -- " + ex.ToString());
                        //MessageBox.Show(ex.ToString(),clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    return functionReturnValue;
                }

                public gloStream.DiseaseManagement.Supporting.OtherDetail this[object vntIndexKey]
                {
                    get { return (OtherDetail)mCol[vntIndexKey]; }
                }

                public int Count
                {
                    get { return mCol.Count; }
                }

                //public System.Collections.IEnumerator GetEnumerator()
                //{
                //    //UPGRADE_TODO: Uncomment and change the following line to return the collection enumerator. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1055"'
                //    //GetEnumerator = mCol.GetEnumerator
                //}

                public void Remove(ref object vntIndexKey)
                {
                    mCol.Remove(vntIndexKey.ToString());
                }

                public OtherDetails()
                    : base()
                {
                    mCol = new Collection();
                }

                //protected override void Finalize()
                //{
                //    Clear();
                //    mCol = null;
                //    base.Finalize();
                //}

                public void Clear()
                {
                    if (mCol == null)
                        return;
                    // Shouldn't happen, but just in case.

                    int i = 0;
                    for (i = mCol.Count; i >= 1; i += -1)
                    {
                        mCol.Remove(i);
                    }
                }


                public IEnumerator GetEnumerator()
                {
                    throw new NotImplementedException();
                }
            }

            public class Criteria_Lab
            {
                private long _GroupID;
                private long _TestID;
                private double _NumericMinimumResult;

                private double _NumericMaximumResult;
                public long GroupID
                {
                    get { return _GroupID; }
                    set { _GroupID = value; }
                }

                public long TestID
                {
                    get { return _TestID; }
                    set { _TestID = value; }
                }

                public double NumericMinimumResult
                {
                    get { return _NumericMinimumResult; }
                    set { _NumericMinimumResult = value; }
                }

                public double NumericMaximumResult
                {
                    get { return _NumericMaximumResult; }
                    set { _NumericMaximumResult = value; }
                }

                public Criteria_Lab()
                    : base()
                {
                }

                //protected override void Finalize()
                //{
                //    base.Finalize();
                //}
            }

            public class Criteria_Labs : System.Collections.IEnumerable
            {

                private Collection mCol;
                public gloStream.DiseaseManagement.Supporting.Criteria_Lab Add(long oGroupID, long oTestID, double oNumericMinResult, double oNumericMaxResult)
                {
                    gloStream.DiseaseManagement.Supporting.Criteria_Lab functionReturnValue = null;
                    //create a new object
                    try
                    {
                        gloStream.DiseaseManagement.Supporting.Criteria_Lab objNewMember = null;
                        objNewMember = new gloStream.DiseaseManagement.Supporting.Criteria_Lab();
                        objNewMember.GroupID = oGroupID;
                        objNewMember.TestID = oTestID;
                        objNewMember.NumericMinimumResult = oNumericMinResult;
                        objNewMember.NumericMaximumResult = oNumericMaxResult;
                        mCol.Add(objNewMember);
                        functionReturnValue = objNewMember;
                        objNewMember = null;
                    }
                    catch //(Exception ex)
                    {
                       // UpdateLog("clsDiseaseManagement -- Criteria_Labs -- Add -- " + ex.ToString());
                        //MessageBox.Show(ex.ToString(),clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    return functionReturnValue;
                }

                public gloStream.DiseaseManagement.Supporting.Criteria_Lab this[object vntIndexKey]
                {
                    get { return (Criteria_Lab)mCol[vntIndexKey]; }
                }

                public int Count
                {
                    get { return mCol.Count; }
                }

                //public System.Collections.IEnumerator GetEnumerator()
                //{
                //    //UPGRADE_TODO: Uncomment and change the following line to return the collection enumerator. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1055"'
                //    //GetEnumerator = mCol.GetEnumerator
                //}

                public void Remove(ref object vntIndexKey)
                {
                    mCol.Remove(vntIndexKey.ToString());
                }

                public Criteria_Labs()
                    : base()
                {
                    mCol = new Collection();
                }

                //protected override void Finalize()
                //{
                //    Clear();
                //    mCol = null;
                //    base.Finalize();
                //}

                public void Clear()
                {
                    if (mCol == null)
                        return;
                    // Shouldn't happen, but just in case.

                    int i = 0;
                    for (i = mCol.Count; i >= 1; i += -1)
                    {
                        mCol.Remove(i);
                    }
                }

                public IEnumerator GetEnumerator()
                {
                    throw new NotImplementedException();
                }
            }


            ////<<<<<<<<<<<<<<<< FIND LAB MODULE ITEMS FOR PATIENTS >>>>>>>>>>>>>>//
            public class LabModulePatientDetail
            {
                private long _OrderID;
                private long _TestID;
                private long _ResultNameID;

                private string _ResultValue;
                public long OrderID
                {
                    get { return _OrderID; }
                    set { _OrderID = value; }
                }

                public long TestID
                {
                    get { return _TestID; }
                    set { _TestID = value; }
                }

                public long ResultNameID
                {
                    get { return _ResultNameID; }
                    set { _ResultNameID = value; }
                }

                public string ResultValue
                {
                    get { return _ResultValue; }
                    set { _ResultValue = value; }
                }

                public LabModulePatientDetail()
                    : base()
                {
                }

                //protected override void Finalize()
                //{
                //    base.Finalize();
                //}
            }

            public class LabModulePatientDetails : System.Collections.IEnumerable
            {

                private Collection mCol;
                //public gloStream.DiseaseManagement.Supporting.LabModulePatientDetail Add(ref gloStream.DiseaseManagement.Supporting.LabModulePatientDetail oItemDetail)
                //{
                //    mCol.Add(oItemDetail);
                //}

                public gloStream.DiseaseManagement.Supporting.LabModulePatientDetail Add(long oOrderID, long oTestID, long oResultNameID, string oResultValue)
                {
                    gloStream.DiseaseManagement.Supporting.LabModulePatientDetail functionReturnValue = null;
                    //create a new object
                    try
                    {
                        gloStream.DiseaseManagement.Supporting.LabModulePatientDetail objNewMember = null;
                        objNewMember = new gloStream.DiseaseManagement.Supporting.LabModulePatientDetail();
                        objNewMember.OrderID = oOrderID;
                        objNewMember.TestID = oTestID;
                        objNewMember.ResultNameID = oResultNameID;
                        objNewMember.ResultValue = oResultValue;
                        mCol.Add(objNewMember);
                        functionReturnValue = objNewMember;
                        objNewMember = null;
                    }
                    catch //(Exception ex)
                    {
                        //UpdateLog("clsDiseaseManagement -- LabModulePatientDetails -- Add -- " + ex.ToString());
                        //MessageBox.Show(ex.ToString(),clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    return functionReturnValue;
                }

                public gloStream.DiseaseManagement.Supporting.LabModulePatientDetail this[object vntIndexKey]
                {
                    get { return (LabModulePatientDetail)mCol[vntIndexKey]; }
                }

                public int Count
                {
                    get { return mCol.Count; }
                }

                //public System.Collections.IEnumerator GetEnumerator()
                //{
                //    //UPGRADE_TODO: Uncomment and change the following line to return the collection enumerator. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1055"'
                //    //GetEnumerator = mCol.GetEnumerator
                //}

                public void Remove(ref object vntIndexKey)
                {
                    mCol.Remove(vntIndexKey.ToString());
                }

                public LabModulePatientDetails()
                    : base()
                {
                    mCol = new Collection();
                }

                //protected override void Finalize()
                //{
                //    Clear();
                //    mCol = null;
                //    base.Finalize();
                //}

                public void Clear()
                {
                    if (mCol == null)
                        return;
                    // Shouldn't happen, but just in case.

                    int i = 0;
                    for (i = mCol.Count; i >= 1; i += -1)
                    {
                        mCol.Remove(i);
                    }
                }

                public IEnumerator GetEnumerator()
                {
                    throw new NotImplementedException();
                }
            }

            public class TriggerDetails
            {
                // Private _CriteriaId As Int64
                private Int64 _TransId;
                private Int64 _TriggerId;
                private bool _Recurring;
                private string _Reason;
                private string _Notes;
                private DateTime _StartDate;
                private DateTime _EndDate;
                private string _DurationType;
                private Int32 _DurationPeriod;
                private string _DueType;
                private string _DueValue;

                private bool _OnEveryCheckIn;
                public TriggerDetails()
                    : base()
                {
                }
                //Public Property CriteriaId() As Int64
                //    Get
                //        Return _CriteriaId
                //    End Get
                //    Set(ByVal Value As Int64)
                //        _CriteriaId = Value
                //    End Set
                //End Property

                public Int64 TransId
                {
                    get { return _TransId; }
                    set { _TransId = value; }
                }

                public Int64 TriggerId
                {
                    get { return _TriggerId; }
                    set { _TriggerId = value; }
                }

                public string DurationType
                {
                    get { return _DurationType; }
                    set { _DurationType = value; }
                }

                public Int32 DurationPeriod
                {
                    get { return _DurationPeriod; }
                    set { _DurationPeriod = value; }
                }

                public bool Recurring
                {
                    get { return _Recurring; }
                    set { _Recurring = value; }
                }

                public string Reason
                {
                    get { return _Reason; }
                    set { _Reason = value; }
                }

                public string Notes
                {
                    get { return _Notes; }
                    set { _Notes = value; }
                }

                public string DueType
                {
                    get { return _DueType; }
                    set { _DueType = value; }
                }

                public string DueValue
                {
                    get { return _DueValue; }
                    set { _DueValue = value; }
                }
                public DateTime StartDate
                {
                    get { return _StartDate; }
                    set { _StartDate = value; }
                }
                public DateTime EndDate
                {
                    get { return _EndDate; }
                    set { _EndDate = value; }
                }
                public bool OnEveryCheckIn
                {
                    get { return _OnEveryCheckIn; }
                    set { _OnEveryCheckIn = value; }
                }

                //protected override void Finalize()
                //{
                //    base.Finalize();
                //}
            }
        }

    }
}