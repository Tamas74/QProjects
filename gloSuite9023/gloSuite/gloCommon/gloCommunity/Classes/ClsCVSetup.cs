using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using gloEMRGeneralLibrary.gloEMRDatabase;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using Microsoft.VisualBasic;
using System.Collections;
using System.Diagnostics;

namespace gloCommunity.Classes
{

    public enum enumDetailType
    {
        None,
        History,
        Medication,
        Lab,
        Order
    }

    class ClsCVSetup
    {

        public DataTable GetCV_Criteria()
        {
            DataBaseLayer oDB = new DataBaseLayer();
            DataTable oResultTable = new DataTable();
            string strQuery = "";
            try
            {
                strQuery = "getCVCriteria";
                oResultTable = oDB.GetDataTable(strQuery, true);
                return oResultTable;
            }
              catch (Exception ex)
            {
                //clsGeneral.UpdateLog("glocommError  while Getting CVCriteria    in class ClsCVSetup : " + ex.Message.ToString());  
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                return null;
            }

            finally
            {

                oDB.Dispose();
                oResultTable.Dispose();
                oDB = null;
            }
        }

        public DataTable getCvdata(Int64 criteriaid)
        {
            DataBaseLayer oDB = new DataBaseLayer();
            DataTable oResultTable = new DataTable();
            string strQuery = "";
            try
            {
                strQuery = "select cv_mst_CriteriaName, cv_mst_AgeMin, cv_mst_AgeMax, cv_mst_Gender," +
                                 " cv_mst_HeightMin, cv_mst_HeightMax, cv_mst_WeightMin, cv_mst_WeightMax, cv_mst_BMIMin, " +
                                  " cv_mst_BMIMax, cv_mst_TemperatureMin, cv_mst_TemperatureMax, cv_mst_PulseMin, cv_mst_PulseMax," +
                                  " cv_mst_PulseOxMin, cv_mst_PulseOxMax, cv_mst_BPSittingMin, cv_mst_BPSittingMax, cv_mst_BPStandingMin," +
                                  " cv_mst_BPStandingMax, cv_mst_DisplayMessage FROM CV_Criteria_MST WHERE (cv_mst_Id = " + criteriaid + ")";
                oResultTable = oDB.GetDataTable_Query(strQuery);
                return oResultTable;
            }
            catch (Exception ex)
            {
                //clsGeneral.UpdateLog("glocommError  while Getting CV Data   in class ClsCVSetup : " + ex.Message.ToString());  
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                return null;
            }

            finally
            {

                oDB.Dispose();
                oResultTable.Dispose();
                oDB = null;
            }
        }

        public DataTable getCvdet(Int64 criteriaid)
        {
            DataBaseLayer oDB = new DataBaseLayer();
            DataTable oResultTable = new DataTable();
            string strQuery = "";
            try
            {
                strQuery = "SELECT cv_dtl_CategoryName, cv_dtl_ItemName, cv_dtl_Operator, cv_dtl_ResultValue1, " +
                            "cv_dtl_ResultValue2, cv_dtl_Type FROM CV_Criteria_DTL  WHERE (cv_mst_Id = " + criteriaid + ")";
                oResultTable = oDB.GetDataTable_Query(strQuery);
                return oResultTable;
            }
            catch (Exception ex)
            {
                //clsGeneral.UpdateLog("glocommError  while Getting CVDet   in class ClsCVSetup : " + ex.Message.ToString());
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                return null;
            }

            finally
            {

                oDB.Dispose();
                oResultTable.Dispose();
                oDB = null;
            }
        }

        public bool CompareXMlData(DataTable local, DataTable server, string FilePath)
        {
            int cnt = 0;
            try
            {
                foreach (DataRow dr in local.Rows)
                {
                    DataRow[] drr = server.Select("CriteriaName='" + dr["CriteriaName"].ToString() + "' ");
                    if (drr.Length == 1)
                    {
                        DialogResult result = MessageBox.Show("The Entry for " + dr["CriteriaName"].ToString() + " already Exists ,Do you want to Overwrite ", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (result == DialogResult.Yes)
                        {
                            server.Rows.Remove(drr[0]);
                            server.ImportRow(dr);
                            cnt = cnt + 1;
                        }


                    }
                    if (drr.Length == 0)
                    {
                        server.ImportRow(dr);
                        cnt = cnt + 1;
                    }
                }

                server.WriteXml(FilePath);
                if (cnt > 0)
                {
                    return true;
                }




            }
            catch (Exception ex)
            {
                //clsGeneral.UpdateLog("glocommError  while Comparing XMLData   in class ClsCVSetup : " + ex.Message.ToString());
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
              
                //MessageBox.Show(ex.Message, clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            return false;
        }

        //public bool InsertCVSetUp_Criteria(Int64 sCriteriaID, string CriteriaName, string Message)
        //{
        //    DataBaseLayer oDB = new DataBaseLayer();
        //    DataTable oResultTable = new DataTable();
        //    DBParameter oParamater = default(DBParameter);
        //    string strQuery = "";
        //    try
        //    {
        //        strQuery = "glocommunity_share_cptInsert";
        //        oParamater = new DBParameter();
        //        oParamater.DataType = SqlDbType.VarChar;
        //        oParamater.Direction = ParameterDirection.Input;
        //        oParamater.Name = "@sCPTCode";
        //        oParamater.Value = sCPTCode;
        //        oDB.DBParametersCol.Add(oParamater);
        //        oParamater = null;




        //        oParamater = new DBParameter();
        //        oParamater.DataType = SqlDbType.VarChar;
        //        oParamater.Direction = ParameterDirection.Input;
        //        oParamater.Name = "@sDescription";
        //        oParamater.Value = sDescription;
        //        oDB.DBParametersCol.Add(oParamater);
        //        oParamater = null;

        //        oParamater = new DBParameter();
        //        oParamater.DataType = SqlDbType.Int;
        //        oParamater.Direction = ParameterDirection.Input;
        //        oParamater.Name = "@clinicID";
        //        oParamater.Value = clsGeneral.gClinicID;
        //        oDB.DBParametersCol.Add(oParamater);
        //        oParamater = null;


        //        oDB.ExecuteNon_Query(strQuery);
        //        return true;
        //    }

        //    catch
        //    {
        //        return false;
        //    }

        //    finally
        //    {

        //        oDB.Dispose();
        //        oDB = null;
        //    }

        //}

        public bool InsertUPDCVSetup(string CriteriaName, decimal agemin, decimal agemax, string gender, decimal htmin, decimal htmax, double wgtmin, double wgtmax, double bmimin, double bmimax, double tempmin, double tempmax, double pulsemin, double pulsemax, double pulseoxmin, double pulseoxmax, double bpsitmin, double bpsitmax, double bpstandmin, double bpstandmax, string dispmsg, long criteriaid, DataTable dttempdet, bool isUpdate)
        {
            SqlConnection conn = new SqlConnection(clsGeneral.EMRConnectionString);

            //declare a transaction object
            SqlTransaction myTrans = default(SqlTransaction);
            SqlCommand cmdCriteria = default(SqlCommand);
            SqlParameter objparam = null;
            long Criteria_ID = 0;
            long MachineID = 0;


            try
            {
                conn.Open();
                myTrans = conn.BeginTransaction();
                cmdCriteria = conn.CreateCommand();
                cmdCriteria.Transaction = myTrans;


                cmdCriteria.Connection = conn;
                cmdCriteria.CommandType = CommandType.StoredProcedure;
                cmdCriteria.CommandText = "CV_InUpCriteriaMST";

                //'INSERT INTO CV_CRITARIA_MST
                objparam = new SqlParameter("@cv_mst_Id", SqlDbType.BigInt);
                var _with1 = cmdCriteria.Parameters;
                objparam.Direction = ParameterDirection.InputOutput;
                _with1.Add(objparam);
                _with1.Add("@MachineID", SqlDbType.BigInt);
                _with1.Add("@cv_mst_CriteriaName", SqlDbType.VarChar);
                _with1.Add("@cv_mst_AgeMin", SqlDbType.Decimal);
                _with1.Add("@cv_mst_AgeMax", SqlDbType.Decimal);
                _with1.Add("@cv_mst_Gender", SqlDbType.VarChar);
                _with1.Add("@cv_mst_HeightMin", SqlDbType.VarChar);
                _with1.Add("@cv_mst_HeightMax", SqlDbType.VarChar);
                _with1.Add("@cv_mst_WeightMin", SqlDbType.Decimal);
                _with1.Add("@cv_mst_WeightMax", SqlDbType.Decimal);
                _with1.Add("@cv_mst_BMIMin", SqlDbType.Decimal);
                _with1.Add("@cv_mst_BMIMax", SqlDbType.Decimal);
                _with1.Add("@cv_mst_TemperatureMin", SqlDbType.Decimal);
                _with1.Add("@cv_mst_TemperatureMax", SqlDbType.Decimal);
                _with1.Add("@cv_mst_PulseMin", SqlDbType.Decimal);
                _with1.Add("@cv_mst_PulseMax", SqlDbType.Decimal);
                _with1.Add("@cv_mst_PulseOxMin", SqlDbType.Decimal);
                _with1.Add("@cv_mst_PulseOxMax", SqlDbType.Decimal);
                _with1.Add("@cv_mst_BPSittingMin", SqlDbType.Decimal);
                _with1.Add("@cv_mst_BPSittingMax", SqlDbType.Decimal);
                _with1.Add("@cv_mst_BPStandingMin", SqlDbType.Decimal);
                _with1.Add("@cv_mst_BPStandingMax", SqlDbType.Decimal);
                _with1.Add("@cv_mst_DisplayMessage", SqlDbType.VarChar);

                var _with2 = cmdCriteria;
                //MachineID = GetPrefixTransactionID();
                MachineID = 0;

                objparam.Value = Criteria_ID;
                _with2.Parameters["@MachineID"].Value = MachineID;
                _with2.Parameters["@cv_mst_CriteriaName"].Value = CriteriaName;
                _with2.Parameters["@cv_mst_AgeMin"].Value = agemin;
                _with2.Parameters["@cv_mst_AgeMax"].Value = agemin;
                _with2.Parameters["@cv_mst_Gender"].Value = gender;
                _with2.Parameters["@cv_mst_HeightMin"].Value = htmin;
                _with2.Parameters["@cv_mst_HeightMax"].Value = htmax;

                if (wgtmin == 0.0)
                {
                    _with2.Parameters["@cv_mst_WeightMin"].Value = System.DBNull.Value;
                }
                else
                {
                    _with2.Parameters["@cv_mst_WeightMin"].Value = wgtmin;
                }

                if (wgtmax == 0.0)
                {
                    _with2.Parameters["@cv_mst_WeightMax"].Value = System.DBNull.Value;
                }
                else
                {
                    _with2.Parameters["@cv_mst_WeightMax"].Value = wgtmax;
                }

                if (bmimin == 0.0)
                {
                    _with2.Parameters["@cv_mst_BMIMin"].Value = System.DBNull.Value;
                }
                else
                {
                    _with2.Parameters["@cv_mst_BMIMin"].Value = bmimin;
                }

                if (bmimax == 0.0)
                {
                    _with2.Parameters["@cv_mst_BMIMax"].Value = System.DBNull.Value;
                }
                else
                {
                    _with2.Parameters["@cv_mst_BMIMax"].Value = bmimax;
                }

                if (pulsemin == 0.0)
                {
                    _with2.Parameters["@cv_mst_PulseMin"].Value = System.DBNull.Value;
                }
                else
                {
                    _with2.Parameters["@cv_mst_PulseMin"].Value = pulsemin;
                }

                if (pulsemax == 0.0)
                {
                    _with2.Parameters["@cv_mst_PulseMax"].Value = System.DBNull.Value;
                }
                else
                {
                    _with2.Parameters["@cv_mst_PulseMax"].Value = pulsemax;
                }

                if (bpsitmin == 0.0)
                {
                    _with2.Parameters["@cv_mst_BPSittingMin"].Value = System.DBNull.Value;
                }
                else
                {
                    _with2.Parameters["@cv_mst_BPSittingMin"].Value = bpsitmin;
                }

                if (bpsitmax == 0.0)
                {
                    _with2.Parameters["@cv_mst_BPSittingMax"].Value = System.DBNull.Value;
                }
                else
                {
                    _with2.Parameters["@cv_mst_BPSittingMax"].Value = bpsitmax;
                }

                if (bpstandmin == 0.0)
                {
                    _with2.Parameters["@cv_mst_BPStandingMin"].Value = System.DBNull.Value;
                }
                else
                {
                    _with2.Parameters["@cv_mst_BPStandingMin"].Value = bpstandmin;
                }

                if (bpstandmax == 0.0)
                {
                    _with2.Parameters["@cv_mst_BPStandingMax"].Value = System.DBNull.Value;
                }
                else
                {
                    _with2.Parameters["@cv_mst_BPStandingMax"].Value = bpstandmax;
                }

                if (pulseoxmin == 0.0)
                {
                    _with2.Parameters["@cv_mst_PulseOxMin"].Value = System.DBNull.Value;
                }
                else
                {
                    _with2.Parameters["@cv_mst_PulseOxMin"].Value = pulseoxmin;
                }

                if (pulseoxmax == 0.0)
                {
                    _with2.Parameters["@cv_mst_PulseOxMax"].Value = System.DBNull.Value;
                }
                else
                {
                    _with2.Parameters["@cv_mst_PulseOxMax"].Value = pulseoxmax;
                }

                if (tempmin == 0.0)
                {
                    _with2.Parameters["@cv_mst_TemperatureMin"].Value = System.DBNull.Value;
                }
                else
                {
                    _with2.Parameters["@cv_mst_TemperatureMin"].Value = tempmin;
                }

                if (tempmax == 0.0)
                {
                    _with2.Parameters["@cv_mst_TemperatureMax"].Value = System.DBNull.Value;
                }
                else
                {
                    _with2.Parameters["@cv_mst_TemperatureMax"].Value = tempmax;
                }

                _with2.Parameters["@cv_mst_DisplayMessage"].Value = dispmsg;


                cmdCriteria.ExecuteNonQuery();


                if ((objparam != null))
                {
                    // Criteria_ID = objparam.Value;
                    Criteria_ID = Convert.ToInt64(objparam.Value);
                }


                if (Criteria_ID > 0)
                {

                    for (int j = 0; j < dttempdet.Rows.Count; j++)
                    {

                        cmdCriteria.Parameters.Clear();
                        cmdCriteria.CommandText = "CV_InUpCriteriaDTL";

                        //'Insert All OtherDetails.

                        cmdCriteria.Parameters.Add("@cv_mst_Id", SqlDbType.BigInt);
                        cmdCriteria.Parameters.Add("@cv_dtl_Id", SqlDbType.BigInt);
                        cmdCriteria.Parameters.Add("@MachineID", SqlDbType.BigInt);
                        cmdCriteria.Parameters.Add("@cv_dtl_CategoryName", SqlDbType.VarChar);
                        cmdCriteria.Parameters.Add("@cv_dtl_ItemName", SqlDbType.VarChar);
                        cmdCriteria.Parameters.Add("@cv_dtl_Operator", SqlDbType.VarChar);
                        cmdCriteria.Parameters.Add("@cv_dtl_ResultValue1", SqlDbType.VarChar);
                        cmdCriteria.Parameters.Add("@cv_dtl_ResultValue2", SqlDbType.VarChar);
                        cmdCriteria.Parameters.Add("@cv_dtl_Type", SqlDbType.Int);

                        //   MachineID = GetPrefixTransactionID();
                        MachineID = 0;
                        cmdCriteria.Parameters["@cv_mst_Id"].Value = Criteria_ID;
                        cmdCriteria.Parameters["@cv_dtl_Id"].Value = 0;
                        cmdCriteria.Parameters["@MachineID"].Value = MachineID;
                        cmdCriteria.Parameters["@cv_dtl_CategoryName"].Value = dttempdet.Rows[j]["cv_dtl_CategoryName"].ToString();
                        cmdCriteria.Parameters["@cv_dtl_ItemName"].Value = dttempdet.Rows[j]["cv_dtl_ItemName"].ToString();
                        cmdCriteria.Parameters["@cv_dtl_Operator"].Value = 0;
                        cmdCriteria.Parameters["@cv_dtl_ResultValue1"].Value = "";
                        cmdCriteria.Parameters["@cv_dtl_ResultValue2"].Value = "";
                        cmdCriteria.Parameters["@cv_dtl_Type"].Value = dttempdet.Rows[j]["cv_dtl_Type"].ToString();

                        cmdCriteria.ExecuteNonQuery();
                        cmdCriteria.Parameters.Clear();

                    }
                    myTrans.Commit();



                    return true;
                }
                else
                {
                    myTrans.Rollback();
                    return false;
                }


            }
            catch //(Exception ex)
            {
                try
                {
                    myTrans.Rollback();
                    return false;
                }
                catch //(SqlException ex1)
                {
                    if ((myTrans.Connection != null))
                    {
                        return false;
                    }
                }

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
            return false;






        }

        public void DeleteCV_Criteria(string criterianm)
        {

            SqlConnection objCon = new SqlConnection(clsGeneral.EMRConnectionString);
            SqlCommand objCmd = new SqlCommand();
            try
            {
                // objCon.ConnectionString = clsGeneral.EMRConnectionString();
                objCon.Open();

                //  objCmd.CommandText = "select cv_mst_Id  FROM  CV_Criteria_MST WHERE  cv_mst_CriteriaName = '" + criterianm + "'";
                DataTable dt = new DataTable();
                SqlDataAdapter adp = new SqlDataAdapter("select cv_mst_Id  FROM  CV_Criteria_MST WHERE  cv_mst_CriteriaName = '" + criterianm + "'", objCon);
                adp.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    Int64 criteriaid = Convert.ToInt64(dt.Rows[0][0]);


                    objCmd.CommandType = CommandType.Text;
                    objCmd.CommandText = "DELETE  FROM  CV_Criteria_MST WHERE  cv_mst_CriteriaName = '" + criterianm + "'";
                    objCmd.Connection = objCon;
                    if ((objCmd.ExecuteNonQuery() > 0))
                    {
                        objCmd.CommandText = "DELETE  FROM  CV_Criteria_DTL WHERE  cv_mst_Id = " + criteriaid + "";
                        if ((objCmd.ExecuteNonQuery() > 0))
                        {

                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                //  gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CardioVascular, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Delete, "CardioVascular Criteria not deleted", gloAuditTrail.ActivityOutCome.Failure);
                //gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Add, "CV criteria not deleted.  ", gstrLoginName, gstrClientMachineName, gnPatientID, False, gloAuditTrail.enmOutCome.Failure, gstrMessageBoxCaption)
                //  MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);

            }
            catch (Exception ex)
            {
                //clsGeneral.UpdateLog("glocommError  while Deleting CV_Criteria in class ClsCVSetup : " + ex.Message.ToString());
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }
            finally
            {
                if ((objCon != null))
                {
                    if (objCon.State == ConnectionState.Open)
                    {
                        objCon.Close();
                    }
                    objCon.Dispose();
                    objCon = null;
                }
                if (objCmd != null)
                {
                    objCmd.Parameters.Clear();
                    objCmd.Dispose();
                    objCmd = null;
                }
            }
        }

        public gloCommunity.Classes.Criteria GetCriteria(long CriteriaID)
        {
            string _strSQL = "";
            Criteria oCriteria = new Criteria();

            OtherDetails oOtherDetails = null;
            OtherDetail oOtherDetail;
            SqlConnection conn = new SqlConnection(clsGeneral.EMRConnectionString);

            SqlDataReader oDataReader;
            SqlCommand ocmd=null;
            //bool _FillDetail = false;

            try
            {
                if (CriteriaID > 0)
                {
                    //            'Criteria Master Record
                    _strSQL = "SELECT cv_mst_CriteriaName, cv_mst_AgeMin, cv_mst_AgeMax, cv_mst_Gender, "
                             + " cv_mst_HeightMin, cv_mst_HeightMax, cv_mst_WeightMin, cv_mst_WeightMax, cv_mst_BMIMin, "
                             + " cv_mst_BMIMax, cv_mst_TemperatureMin, cv_mst_TemperatureMax, cv_mst_PulseMin, cv_mst_PulseMax,"
                             + " cv_mst_PulseOxMin, cv_mst_PulseOxMax, cv_mst_BPSittingMin, cv_mst_BPSittingMax, cv_mst_BPStandingMin,"
                             + " cv_mst_BPStandingMax, cv_mst_DisplayMessage FROM CV_Criteria_MST WHERE (cv_mst_Id = " + CriteriaID + ")";

                    conn.Open();
                    ocmd = new SqlCommand(_strSQL, conn);
                    oDataReader = ocmd.ExecuteReader();
                    if (oDataReader != null)
                    {
                        if (oDataReader.HasRows == true)
                        {
                          //  _FillDetail = true;
                            while (oDataReader.Read())
                            {
                                oCriteria.Name = oDataReader["cv_mst_CriteriaName"].ToString();

                                if (!Information.IsDBNull(oDataReader["cv_mst_AgeMin"]))
                                {
                                    oCriteria.AgeMinimum = Convert.ToDouble(oDataReader["cv_mst_AgeMin"]);
                                }

                                if (!Information.IsDBNull(oDataReader["cv_mst_AgeMax"]))
                                {
                                    oCriteria.AgeMaximum = Convert.ToDouble(oDataReader["cv_mst_AgeMax"]);
                                }


                                if (!Information.IsDBNull(oDataReader["cv_mst_Gender"]))
                                {
                                    oCriteria.Gender = oDataReader["cv_mst_Gender"].ToString();
                                }


                                if (!Information.IsDBNull(oDataReader["cv_mst_HeightMin"]))
                                {
                                    oCriteria.HeightMinimum = oDataReader["cv_mst_HeightMin"].ToString();
                                }

                                if (!Information.IsDBNull(oDataReader["cv_mst_HeightMax"]))
                                {
                                    oCriteria.HeightMaximum = oDataReader["cv_mst_HeightMax"].ToString();
                                }

                                if (!Information.IsDBNull(oDataReader["cv_mst_WeightMin"]))
                                {
                                    oCriteria.WeightMinimum = Convert.ToDouble(oDataReader["cv_mst_WeightMin"]);
                                }

                                if (!Information.IsDBNull(oDataReader["cv_mst_WeightMax"]))
                                {
                                    oCriteria.WeightMaximum = Convert.ToDouble(oDataReader["cv_mst_WeightMax"]);
                                }

                                if (!Information.IsDBNull(oDataReader["cv_mst_BMIMin"]))
                                {
                                    oCriteria.BMIMinimum = Convert.ToDouble(oDataReader["cv_mst_BMIMin"]);
                                }

                                if (!Information.IsDBNull(oDataReader["cv_mst_BMIMax"]))
                                {
                                    oCriteria.BMIMaximum = Convert.ToDouble(oDataReader["cv_mst_BMIMax"]);
                                }

                                if (!Information.IsDBNull(oDataReader["cv_mst_TemperatureMin"]))
                                {
                                    oCriteria.TempratureMinumum = Convert.ToDouble(oDataReader["cv_mst_TemperatureMin"]);
                                }

                                if (!Information.IsDBNull(oDataReader["cv_mst_TemperatureMax"]))
                                {
                                    oCriteria.TempratureMaximum = Convert.ToDouble(oDataReader["cv_mst_TemperatureMax"]);
                                }

                                if (!Information.IsDBNull(oDataReader["cv_mst_PulseMin"]))
                                {
                                    oCriteria.PulseMinimum = Convert.ToDouble(oDataReader["cv_mst_PulseMin"]);
                                }

                                if (!Information.IsDBNull(oDataReader["cv_mst_PulseMax"]))
                                {
                                    oCriteria.PulseMaximum = Convert.ToDouble(oDataReader["cv_mst_PulseMax"]);
                                }

                                if (!Information.IsDBNull(oDataReader["cv_mst_PulseOxMin"]))
                                {
                                    oCriteria.PulseOXMinimum = Convert.ToDouble(oDataReader["cv_mst_PulseOxMin"]);
                                }

                                if (!Information.IsDBNull(oDataReader["cv_mst_PulseOxMax"]))
                                {
                                    oCriteria.PulseOXMaximum = Convert.ToDouble(oDataReader["cv_mst_PulseOxMax"]);
                                }

                                if (!Information.IsDBNull(oDataReader["cv_mst_BPSittingMin"]))
                                {
                                    oCriteria.BPSittingMinimum = Convert.ToDouble(oDataReader["cv_mst_BPSittingMin"]);
                                }

                                if (!Information.IsDBNull(oDataReader["cv_mst_BPSittingMax"]))
                                {
                                    oCriteria.BPSittingMaximum = Convert.ToDouble(oDataReader["cv_mst_BPSittingMax"]);
                                }

                                if (!Information.IsDBNull(oDataReader["cv_mst_BPStandingMin"]))
                                {
                                    oCriteria.BPStandingMinimum = Convert.ToDouble(oDataReader["cv_mst_BPStandingMin"]);
                                }

                                if (!Information.IsDBNull(oDataReader["cv_mst_BPStandingMax"]))
                                {
                                    oCriteria.BPStandingMaximum = Convert.ToDouble(oDataReader["cv_mst_BPStandingMax"]);
                                }

                                if (!Information.IsDBNull(oDataReader["cv_mst_DisplayMessage"]))
                                {
                                    oCriteria.DisplayMessage = oDataReader["cv_mst_DisplayMessage"].ToString();
                                }
                            }
                            oDataReader.Close();
                        }
                    }

                    //            ''Other Details
                    _strSQL = "SELECT cv_dtl_CategoryName, cv_dtl_ItemName, cv_dtl_Operator, cv_dtl_ResultValue1, "
                        + "cv_dtl_ResultValue2, cv_dtl_Type FROM CV_Criteria_DTL WHERE (cv_mst_Id = " + CriteriaID + ")";
                    ocmd = new SqlCommand(_strSQL, conn);
                    oDataReader = ocmd.ExecuteReader();
                    oOtherDetails = new OtherDetails();
                    if (oDataReader != null)
                    {
                        if (oDataReader.HasRows == true)
                        {
                            while (oDataReader.Read())
                            {
                                oOtherDetail = new OtherDetail();

                                if (!Information.IsDBNull(oDataReader["cv_dtl_CategoryName"]))
                                {
                                    oOtherDetail.CategoryName = oDataReader["cv_dtl_CategoryName"].ToString();
                                }

                                if (!Information.IsDBNull(oDataReader["cv_dtl_ItemName"]))
                                {
                                    oOtherDetail.ItemName = oDataReader["cv_dtl_ItemName"].ToString();
                                }

                                if (!Information.IsDBNull(oDataReader["cv_dtl_Operator"]))
                                {
                                    oOtherDetail.OperatorName = oDataReader["cv_dtl_Operator"].ToString();
                                }

                                if (!Information.IsDBNull(oDataReader["cv_dtl_ResultValue1"]))
                                {
                                    oOtherDetail.Result1 = oDataReader["cv_dtl_ResultValue1"].ToString();
                                }

                                if (!Information.IsDBNull(oDataReader["cv_dtl_ResultValue2"]))
                                {
                                    oOtherDetail.Result2 = oDataReader["cv_dtl_ResultValue2"].ToString();
                                }

                                if (!Information.IsDBNull(oDataReader["cv_dtl_Type"]))
                                {
                                    oOtherDetail.DetailType = (enumDetailType)oDataReader["cv_dtl_Type"];
                                }


                                oOtherDetails.Add(oOtherDetail);
                                oOtherDetail = null;
                            }
                        }
                        oDataReader.Close();
                    }
                    oDataReader = null;

                    oCriteria.OtherDetails = oOtherDetails;


                }

            }
            catch (Exception ex)
            {
                //clsGeneral.UpdateLog("glocommError  while Getting Criteria   in class ClsCVSetup : " + ex.Message.ToString());
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                throw;
            }
            finally
            {
                if (ocmd != null)
                {
                    ocmd.Parameters.Clear();
                    ocmd.Dispose();
                    ocmd = null;
                }
                conn.Close();
                conn.Dispose();
            }
            return oCriteria;
        }

    }

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

        private OtherDetails mOtherDetails = new OtherDetails();
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

        public OtherDetails OtherDetails
        {
            get { return mOtherDetails; }
            set { mOtherDetails = value; }
        }

        public Criteria()
            : base()
        {
            mOtherDetails = new OtherDetails();

        }

        //protected override void Finalize()
        //{
        //    //_Histories = Nothing
        //    //_Drugs = Nothing
        //    //_ICD9s = Nothing
        //    //_CPTs = Nothing
        //    //_Labs = Nothing
        //    //_Guidelines = Nothing
        //    //_LabModuleTests = Nothing
        //    //_Guidelines = Nothing
        //    //_LabOrders = Nothing
        //    //_RadiologyOrders = Nothing
        //    //_RxDrugs = Nothing
        //    //_Referrals = Nothing
        //    base.Finalize();
        //}

        public long IsExistCriteria(string oCriteriaName)
        {
            string _strSQL = "";
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(clsGeneral.EMRConnectionString);
          //  System.Data.SqlClient.SqlDataReader oDataReader = null;
            long CriteriaID = 0;
           // bool _Result = true;
          //  long _Count = 0;

            SqlConnection Conn = default(SqlConnection);
            SqlCommand cmd = default(SqlCommand);
            try
            {
                //connect to the database
                oDB.Connect(false);
                Conn = new SqlConnection(clsGeneral.EMRConnectionString);
                Conn.Open();
                
                //extract the criteria id from the table for the given criteria name
                _strSQL = "SELECT cv_mst_Id FROM CV_Criteria_MST where cv_mst_CriteriaName = '" + oCriteriaName + "'";
                cmd = new SqlCommand(_strSQL, Conn);

                CriteriaID = Convert.ToInt64(cmd.ExecuteScalar());

                cmd.Dispose();
                cmd = null;
                Conn.Close();
                Conn.Dispose();
                Conn = null;

                ////set the query string
                //_strSQL = "SELECT COUNT(DM_TransId) FROM DM_Patient where DM_nCriteriaID =" + CriteriaID;
                ////execute the query and return a datareader
                //Conn = new SqlConnection(clsGeneral.EMRConnectionString);
                //Conn.Open();
                //_Count = Convert.ToInt64(oDB.ExecuteScalar_Query(_strSQL));

                ////check if there is any data in the datareader
                //if (_Count > 0)
                //{
                //    _Result = false;
                //}

                return CriteriaID;
            }
            catch (Exception ex)
            {
                //clsGeneral.UpdateLog("glocommError  while Checking isExist Criteria   in class ClsCVSetup : " + ex.Message.ToString());
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
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

        public long AddCVCriteria(Criteria oCriteria)
        {
            SqlConnection conn = new SqlConnection(clsGeneral.EMRConnectionString);

            //declare a transaction object
            SqlTransaction myTrans = default(SqlTransaction);
            SqlCommand cmdCriteria = default(SqlCommand);
            SqlParameter objparam = null;
            long Criteria_ID = 0;
            long MachineID = 0;

            try
            {
                conn.Open();
                myTrans = conn.BeginTransaction();
                cmdCriteria = conn.CreateCommand();
                cmdCriteria.Transaction = myTrans;


                cmdCriteria.Connection = conn;
                cmdCriteria.CommandType = CommandType.StoredProcedure;
                cmdCriteria.CommandText = "CV_InUpCriteriaMST";

                //'INSERT INTO CV_CRITARIA_MST
                objparam = new SqlParameter("@cv_mst_Id", SqlDbType.BigInt);
                var _with1 = cmdCriteria.Parameters;
                objparam.Direction = ParameterDirection.InputOutput;
                _with1.Add(objparam);
                _with1.Add("@MachineID", SqlDbType.BigInt);
                _with1.Add("@cv_mst_CriteriaName", SqlDbType.VarChar);
                _with1.Add("@cv_mst_AgeMin", SqlDbType.Decimal);
                _with1.Add("@cv_mst_AgeMax", SqlDbType.Decimal);
                _with1.Add("@cv_mst_Gender", SqlDbType.VarChar);
                _with1.Add("@cv_mst_HeightMin", SqlDbType.VarChar);
                _with1.Add("@cv_mst_HeightMax", SqlDbType.VarChar);
                _with1.Add("@cv_mst_WeightMin", SqlDbType.Decimal);
                _with1.Add("@cv_mst_WeightMax", SqlDbType.Decimal);
                _with1.Add("@cv_mst_BMIMin", SqlDbType.Decimal);
                _with1.Add("@cv_mst_BMIMax", SqlDbType.Decimal);
                _with1.Add("@cv_mst_TemperatureMin", SqlDbType.Decimal);
                _with1.Add("@cv_mst_TemperatureMax", SqlDbType.Decimal);
                _with1.Add("@cv_mst_PulseMin", SqlDbType.Decimal);
                _with1.Add("@cv_mst_PulseMax", SqlDbType.Decimal);
                _with1.Add("@cv_mst_PulseOxMin", SqlDbType.Decimal);
                _with1.Add("@cv_mst_PulseOxMax", SqlDbType.Decimal);
                _with1.Add("@cv_mst_BPSittingMin", SqlDbType.Decimal);
                _with1.Add("@cv_mst_BPSittingMax", SqlDbType.Decimal);
                _with1.Add("@cv_mst_BPStandingMin", SqlDbType.Decimal);
                _with1.Add("@cv_mst_BPStandingMax", SqlDbType.Decimal);
                _with1.Add("@cv_mst_DisplayMessage", SqlDbType.VarChar);

                var _with2 = cmdCriteria;
                //MachineID = GetPrefixTransactionID();

                objparam.Value = Criteria_ID;
                _with2.Parameters["@MachineID"].Value = MachineID;
                _with2.Parameters["@cv_mst_CriteriaName"].Value = oCriteria.Name;
                _with2.Parameters["@cv_mst_AgeMin"].Value = oCriteria.AgeMinimum;
                _with2.Parameters["@cv_mst_AgeMax"].Value = oCriteria.AgeMaximum;
                _with2.Parameters["@cv_mst_Gender"].Value = oCriteria.Gender;
                _with2.Parameters["@cv_mst_HeightMin"].Value = oCriteria.HeightMinimum;
                _with2.Parameters["@cv_mst_HeightMax"].Value = oCriteria.HeightMaximum;

                if (oCriteria.WeightMinimum == 0.0)
                {
                    _with2.Parameters["@cv_mst_WeightMin"].Value = System.DBNull.Value;
                }
                else
                {
                    _with2.Parameters["@cv_mst_WeightMin"].Value = oCriteria.WeightMinimum;
                }

                if (oCriteria.WeightMaximum == 0.0)
                {
                    _with2.Parameters["@cv_mst_WeightMax"].Value = System.DBNull.Value;
                }
                else
                {
                    _with2.Parameters["@cv_mst_WeightMax"].Value = oCriteria.WeightMaximum;
                }

                if (oCriteria.BMIMinimum == 0.0)
                {
                    _with2.Parameters["@cv_mst_BMIMin"].Value = System.DBNull.Value;
                }
                else
                {
                    _with2.Parameters["@cv_mst_BMIMin"].Value = oCriteria.BMIMinimum;
                }

                if (oCriteria.BMIMaximum == 0.0)
                {
                    _with2.Parameters["@cv_mst_BMIMax"].Value = System.DBNull.Value;
                }
                else
                {
                    _with2.Parameters["@cv_mst_BMIMax"].Value = oCriteria.BMIMaximum;
                }

                if (oCriteria.PulseMinimum == 0.0)
                {
                    _with2.Parameters["@cv_mst_PulseMin"].Value = System.DBNull.Value;
                }
                else
                {
                    _with2.Parameters["@cv_mst_PulseMin"].Value = oCriteria.PulseMinimum;
                }

                if (oCriteria.PulseMaximum == 0.0)
                {
                    _with2.Parameters["@cv_mst_PulseMax"].Value = System.DBNull.Value;
                }
                else
                {
                    _with2.Parameters["@cv_mst_PulseMax"].Value = oCriteria.PulseMaximum;
                }

                if (oCriteria.BPSittingMinimum == 0.0)
                {
                    _with2.Parameters["@cv_mst_BPSittingMin"].Value = System.DBNull.Value;
                }
                else
                {
                    _with2.Parameters["@cv_mst_BPSittingMin"].Value = oCriteria.BPSittingMinimum;
                }

                if (oCriteria.BPSittingMaximum == 0.0)
                {
                    _with2.Parameters["@cv_mst_BPSittingMax"].Value = System.DBNull.Value;
                }
                else
                {
                    _with2.Parameters["@cv_mst_BPSittingMax"].Value = oCriteria.BPSittingMaximum;
                }

                if (oCriteria.BPStandingMinimum == 0.0)
                {
                    _with2.Parameters["@cv_mst_BPStandingMin"].Value = System.DBNull.Value;
                }
                else
                {
                    _with2.Parameters["@cv_mst_BPStandingMin"].Value = oCriteria.BPStandingMinimum;
                }

                if (oCriteria.BPStandingMaximum == 0.0)
                {
                    _with2.Parameters["@cv_mst_BPStandingMax"].Value = System.DBNull.Value;
                }
                else
                {
                    _with2.Parameters["@cv_mst_BPStandingMax"].Value = oCriteria.BPStandingMaximum;
                }

                if (oCriteria.PulseOXMinimum == 0.0)
                {
                    _with2.Parameters["@cv_mst_PulseOxMin"].Value = System.DBNull.Value;
                }
                else
                {
                    _with2.Parameters["@cv_mst_PulseOxMin"].Value = oCriteria.PulseOXMinimum;
                }

                if (oCriteria.PulseOXMaximum == 0.0)
                {
                    _with2.Parameters["@cv_mst_PulseOxMax"].Value = System.DBNull.Value;
                }
                else
                {
                    _with2.Parameters["@cv_mst_PulseOxMax"].Value = oCriteria.PulseOXMaximum;
                }

                if (oCriteria.TempratureMinumum == 0.0)
                {
                    _with2.Parameters["@cv_mst_TemperatureMin"].Value = System.DBNull.Value;
                }
                else
                {
                    _with2.Parameters["@cv_mst_TemperatureMin"].Value = oCriteria.TempratureMinumum;
                }

                if (oCriteria.TempratureMaximum == 0.0)
                {
                    _with2.Parameters["@cv_mst_TemperatureMax"].Value = System.DBNull.Value;
                }
                else
                {
                    _with2.Parameters["@cv_mst_TemperatureMax"].Value = oCriteria.TempratureMaximum;
                }

                _with2.Parameters["@cv_mst_DisplayMessage"].Value = oCriteria.DisplayMessage;


                cmdCriteria.ExecuteNonQuery();


                if ((objparam != null))
                {
                    Criteria_ID = Convert.ToInt64(objparam.Value);
                }


                if (Criteria_ID > 0)
                {
                    Int64 i = 0;

                    cmdCriteria.Parameters.Clear();
                    cmdCriteria.CommandText = "CV_InUpCriteriaDTL";

                    //'Insert All OtherDetails.
                    for (i = 1; i <= oCriteria.OtherDetails.Count; i++)
                    {
                        cmdCriteria.Parameters.Add("@cv_mst_Id", SqlDbType.BigInt);
                        cmdCriteria.Parameters.Add("@cv_dtl_Id", SqlDbType.BigInt);
                        cmdCriteria.Parameters.Add("@MachineID", SqlDbType.BigInt);
                        cmdCriteria.Parameters.Add("@cv_dtl_CategoryName", SqlDbType.VarChar);
                        cmdCriteria.Parameters.Add("@cv_dtl_ItemName", SqlDbType.VarChar);
                        cmdCriteria.Parameters.Add("@cv_dtl_Operator", SqlDbType.VarChar);
                        cmdCriteria.Parameters.Add("@cv_dtl_ResultValue1", SqlDbType.VarChar);
                        cmdCriteria.Parameters.Add("@cv_dtl_ResultValue2", SqlDbType.VarChar);
                        cmdCriteria.Parameters.Add("@cv_dtl_Type", SqlDbType.Int);

                        //MachineID = GetPrefixTransactionID();
                        cmdCriteria.Parameters["@cv_mst_Id"].Value = Criteria_ID;
                        cmdCriteria.Parameters["@cv_dtl_Id"].Value = 0;
                        cmdCriteria.Parameters["@MachineID"].Value = MachineID;
                        cmdCriteria.Parameters["@cv_dtl_CategoryName"].Value = oCriteria.OtherDetails[i].CategoryName;
                        cmdCriteria.Parameters["@cv_dtl_ItemName"].Value = oCriteria.OtherDetails[i].ItemName;
                        cmdCriteria.Parameters["@cv_dtl_Operator"].Value = oCriteria.OtherDetails[i].OperatorName;
                        cmdCriteria.Parameters["@cv_dtl_ResultValue1"].Value = oCriteria.OtherDetails[i].Result1;
                        cmdCriteria.Parameters["@cv_dtl_ResultValue2"].Value = oCriteria.OtherDetails[i].Result2;
                        cmdCriteria.Parameters["@cv_dtl_Type"].Value = oCriteria.OtherDetails[i].DetailType.GetHashCode();

                        cmdCriteria.ExecuteNonQuery();
                        cmdCriteria.Parameters.Clear();
                    }

                    myTrans.Commit();

                    //'gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.CardioVascular, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Add, "'" & oCriteria.Name & "' Cardio Vascular Criteria Added", gloAuditTrail.ActivityOutCome.Success)
                    //'Added Rahul P on 20101008
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.CardioVascular, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Add, "'" + oCriteria.Name + "' Cardio Vascular Criteria Added", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);
                    //'
                    //Dim objAudit As New clsAudit
                    //objAudit.CreateLog(clsAudit.enmActivityType.Add, "'" & oCriteria.Name & "' Cardio Vascular Criteria Added", gstrLoginName, 0)
                    //objAudit = Nothing

                    return Criteria_ID;
                }
                else
                {
                    myTrans.Rollback();
                }

            }
            catch (Exception ex)
            {
                try
                {
                    myTrans.Rollback();
                    //MessageBox.Show(ex.ToString(),clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch //(SqlException ex1)
                {
                    if ((myTrans.Connection != null))
                    {
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CardioVascular, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Add, "clsCardioVascular -- Add -- " + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                        //UpdateLog("clsCardioVascular -- Add -- " & ex.ToString)
                    }
                }
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CardioVascular, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Add, "clsCardioVascular -- Add -- " + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                //UpdateLog("clsCardioVascular -- Add -- " & ex.ToString)
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

        public long ModifyCVCriteria(Int64 CriteriaID, Criteria oCriteria)
        {
            SqlConnection conn = new SqlConnection(clsGeneral.EMRConnectionString);

            //declare a transaction object
            SqlTransaction myTrans = default(SqlTransaction);
            SqlCommand cmdCriteria = default(SqlCommand);
            long MachineID = 0;
            SqlParameter objparam = null;

            try
            {
                conn.Open();
                myTrans = conn.BeginTransaction();
                cmdCriteria = conn.CreateCommand();
                cmdCriteria.Transaction = myTrans;


                cmdCriteria.Connection = conn;
                cmdCriteria.CommandType = CommandType.StoredProcedure;
                cmdCriteria.CommandText = "CV_InUpCriteriaMST";

                //'INSERT INTO CV_CRITARIA_MST
                objparam = new SqlParameter("@cv_mst_Id", SqlDbType.BigInt);
                var _with1 = cmdCriteria.Parameters;
                objparam.Direction = ParameterDirection.InputOutput;
                _with1.Add(objparam);
                _with1.Add("@MachineID", SqlDbType.BigInt);
                _with1.Add("@cv_mst_CriteriaName", SqlDbType.VarChar);
                _with1.Add("@cv_mst_AgeMin", SqlDbType.Decimal);
                _with1.Add("@cv_mst_AgeMax", SqlDbType.Decimal);
                _with1.Add("@cv_mst_Gender", SqlDbType.VarChar);
                _with1.Add("@cv_mst_HeightMin", SqlDbType.VarChar);
                _with1.Add("@cv_mst_HeightMax", SqlDbType.VarChar);
                _with1.Add("@cv_mst_WeightMin", SqlDbType.Decimal);
                _with1.Add("@cv_mst_WeightMax", SqlDbType.Decimal);
                _with1.Add("@cv_mst_BMIMin", SqlDbType.Decimal);
                _with1.Add("@cv_mst_BMIMax", SqlDbType.Decimal);
                _with1.Add("@cv_mst_TemperatureMin", SqlDbType.Decimal);
                _with1.Add("@cv_mst_TemperatureMax", SqlDbType.Decimal);
                _with1.Add("@cv_mst_PulseMin", SqlDbType.Decimal);
                _with1.Add("@cv_mst_PulseMax", SqlDbType.Decimal);
                _with1.Add("@cv_mst_PulseOxMin", SqlDbType.Decimal);
                _with1.Add("@cv_mst_PulseOxMax", SqlDbType.Decimal);
                _with1.Add("@cv_mst_BPSittingMin", SqlDbType.Decimal);
                _with1.Add("@cv_mst_BPSittingMax", SqlDbType.Decimal);
                _with1.Add("@cv_mst_BPStandingMin", SqlDbType.Decimal);
                _with1.Add("@cv_mst_BPStandingMax", SqlDbType.Decimal);
                _with1.Add("@cv_mst_DisplayMessage", SqlDbType.VarChar);

                var _with2 = cmdCriteria;
               //MachineID = GetPrefixTransactionID();

                objparam.Value = CriteriaID;
                _with2.Parameters["@MachineID"].Value = MachineID;
                _with2.Parameters["@cv_mst_CriteriaName"].Value = oCriteria.Name;
                _with2.Parameters["@cv_mst_AgeMin"].Value = oCriteria.AgeMinimum;
                _with2.Parameters["@cv_mst_AgeMax"].Value = oCriteria.AgeMaximum;
                _with2.Parameters["@cv_mst_Gender"].Value = oCriteria.Gender;
                _with2.Parameters["@cv_mst_HeightMin"].Value = oCriteria.HeightMinimum;
                _with2.Parameters["@cv_mst_HeightMax"].Value = oCriteria.HeightMaximum;

                if (oCriteria.WeightMinimum == 0.0)
                {
                    _with2.Parameters["@cv_mst_WeightMin"].Value = System.DBNull.Value;
                }
                else
                {
                    _with2.Parameters["@cv_mst_WeightMin"].Value = oCriteria.WeightMinimum;
                }

                if (oCriteria.WeightMaximum == 0.0)
                {
                    _with2.Parameters["@cv_mst_WeightMax"].Value = System.DBNull.Value;
                }
                else
                {
                    _with2.Parameters["@cv_mst_WeightMax"].Value = oCriteria.WeightMaximum;
                }

                if (oCriteria.BMIMinimum == 0.0)
                {
                    _with2.Parameters["@cv_mst_BMIMin"].Value = System.DBNull.Value;
                }
                else
                {
                    _with2.Parameters["@cv_mst_BMIMin"].Value = oCriteria.BMIMinimum;
                }

                if (oCriteria.BMIMaximum == 0.0)
                {
                    _with2.Parameters["@cv_mst_BMIMax"].Value = System.DBNull.Value;
                }
                else
                {
                    _with2.Parameters["@cv_mst_BMIMax"].Value = oCriteria.BMIMaximum;
                }

                if (oCriteria.PulseMinimum == 0.0)
                {
                    _with2.Parameters["@cv_mst_PulseMin"].Value = System.DBNull.Value;
                }
                else
                {
                    _with2.Parameters["@cv_mst_PulseMin"].Value = oCriteria.PulseMinimum;
                }

                if (oCriteria.PulseMaximum == 0.0)
                {
                    _with2.Parameters["@cv_mst_PulseMax"].Value = System.DBNull.Value;
                }
                else
                {
                    _with2.Parameters["@cv_mst_PulseMax"].Value = oCriteria.PulseMaximum;
                }

                if (oCriteria.BPSittingMinimum == 0.0)
                {
                    _with2.Parameters["@cv_mst_BPSittingMin"].Value = System.DBNull.Value;
                }
                else
                {
                    _with2.Parameters["@cv_mst_BPSittingMin"].Value = oCriteria.BPSittingMinimum;
                }

                if (oCriteria.BPSittingMaximum == 0.0)
                {
                    _with2.Parameters["@cv_mst_BPSittingMax"].Value = System.DBNull.Value;
                }
                else
                {
                    _with2.Parameters["@cv_mst_BPSittingMax"].Value = oCriteria.BPSittingMaximum;
                }

                if (oCriteria.BPStandingMinimum == 0.0)
                {
                    _with2.Parameters["@cv_mst_BPStandingMin"].Value = System.DBNull.Value;
                }
                else
                {
                    _with2.Parameters["@cv_mst_BPStandingMin"].Value = oCriteria.BPStandingMinimum;
                }

                if (oCriteria.BPStandingMaximum == 0.0)
                {
                    _with2.Parameters["@cv_mst_BPStandingMax"].Value = System.DBNull.Value;
                }
                else
                {
                    _with2.Parameters["@cv_mst_BPStandingMax"].Value = oCriteria.BPStandingMaximum;
                }

                if (oCriteria.PulseOXMinimum == 0.0)
                {
                    _with2.Parameters["@cv_mst_PulseOxMin"].Value = System.DBNull.Value;
                }
                else
                {
                    _with2.Parameters["@cv_mst_PulseOxMin"].Value = oCriteria.PulseOXMinimum;
                }

                if (oCriteria.PulseOXMaximum == 0.0)
                {
                    _with2.Parameters["@cv_mst_PulseOxMax"].Value = System.DBNull.Value;
                }
                else
                {
                    _with2.Parameters["@cv_mst_PulseOxMax"].Value = oCriteria.PulseOXMaximum;
                }

                if (oCriteria.TempratureMinumum == 0.0)
                {
                    _with2.Parameters["@cv_mst_TemperatureMin"].Value = System.DBNull.Value;
                }
                else
                {
                    _with2.Parameters["@cv_mst_TemperatureMin"].Value = oCriteria.TempratureMinumum;
                }

                if (oCriteria.TempratureMaximum == 0.0)
                {
                    _with2.Parameters["@cv_mst_TemperatureMax"].Value = System.DBNull.Value;
                }
                else
                {
                    _with2.Parameters["@cv_mst_TemperatureMax"].Value = oCriteria.TempratureMaximum;
                }

                _with2.Parameters["@cv_mst_DisplayMessage"].Value = oCriteria.DisplayMessage;


                cmdCriteria.ExecuteNonQuery();

                //'Delete All Records from OtherDetail Table & Insert Updated Data
                cmdCriteria.Connection = conn;
                cmdCriteria.CommandType = CommandType.Text;
                cmdCriteria.CommandText = "DELETE FROM CV_Criteria_DTL WHERE (cv_mst_Id = " + CriteriaID + ")";
                cmdCriteria.ExecuteNonQuery();

                Int64 i = 0;

                cmdCriteria.Parameters.Clear();
                cmdCriteria.CommandType = CommandType.StoredProcedure;
                cmdCriteria.CommandText = "CV_InUpCriteriaDTL";

                //'Insert All OtherDetails.
                for (i = 1; i <= oCriteria.OtherDetails.Count; i++)
                {
                    cmdCriteria.Parameters.Add("@cv_mst_Id", SqlDbType.BigInt);
                    cmdCriteria.Parameters.Add("@cv_dtl_Id", SqlDbType.BigInt);
                    cmdCriteria.Parameters.Add("@MachineID", SqlDbType.BigInt);
                    cmdCriteria.Parameters.Add("@cv_dtl_CategoryName", SqlDbType.VarChar);
                    cmdCriteria.Parameters.Add("@cv_dtl_ItemName", SqlDbType.VarChar);
                    cmdCriteria.Parameters.Add("@cv_dtl_Operator", SqlDbType.VarChar);
                    cmdCriteria.Parameters.Add("@cv_dtl_ResultValue1", SqlDbType.VarChar);
                    cmdCriteria.Parameters.Add("@cv_dtl_ResultValue2", SqlDbType.VarChar);
                    cmdCriteria.Parameters.Add("@cv_dtl_Type", SqlDbType.Int);

                    //MachineID = GetPrefixTransactionID();
                    cmdCriteria.Parameters["@cv_mst_Id"].Value = CriteriaID;
                    cmdCriteria.Parameters["@cv_dtl_Id"].Value = 0;
                    cmdCriteria.Parameters["@MachineID"].Value = MachineID;
                    cmdCriteria.Parameters["@cv_dtl_CategoryName"].Value = oCriteria.OtherDetails[i].CategoryName;
                    cmdCriteria.Parameters["@cv_dtl_ItemName"].Value = oCriteria.OtherDetails[i].ItemName;
                    cmdCriteria.Parameters["@cv_dtl_Operator"].Value = oCriteria.OtherDetails[i].OperatorName;
                    cmdCriteria.Parameters["@cv_dtl_ResultValue1"].Value = oCriteria.OtherDetails[i].Result1;
                    cmdCriteria.Parameters["@cv_dtl_ResultValue2"].Value = oCriteria.OtherDetails[i].Result2;
                    cmdCriteria.Parameters["@cv_dtl_Type"].Value = oCriteria.OtherDetails[i].DetailType.GetHashCode();

                    cmdCriteria.ExecuteNonQuery();
                    cmdCriteria.Parameters.Clear();
                }

                if ((objparam != null))
                {
                    CriteriaID = Convert.ToInt64(objparam.Value);
                    ////MsgBox(Criteria_ID)
                }

                myTrans.Commit();

                //'gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.CardioVascular, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Modify, "'" & oCriteria.Name & "' Cardio Vascular Criteria Modified", gloAuditTrail.ActivityOutCome.Success)
                //'Added Rahul P on 20101008
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.CardioVascular, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Modify, "'" + oCriteria.Name + "' Cardio Vascular Criteria Modified", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);
                //'
                //Dim objAudit As New clsAudit
                //objAudit.CreateLog(clsAudit.enmActivityType.Add, "'" & oCriteria.Name & "' Cardio Vascular Criteria Added", gstrLoginName, 0)
                //objAudit = Nothing

                return CriteriaID;
            }
            catch (Exception ex)
            {
                try
                {
                    myTrans.Rollback();
                    //MessageBox.Show(ex.ToString(),clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch //(SqlException ex1)
                {
                    if ((myTrans.Connection != null))
                    {
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CardioVascular, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Modify, "clsCardioVascular -- Modify -- " + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                        //UpdateLog("clsCardioVascular -- Add -- " & ex.ToString)
                    }
                }
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
            : base()
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

        public void Add(OtherDetail oOtherDetail)
        {
            mCol.Add(oOtherDetail);
        }

        public OtherDetail Add(Int64 ItemID, Int64 CategoryID, string CategoryName, string ItemName, string OperatorName, string Result1, string Result2)
        {
            OtherDetail functionReturnValue = null;
            //create a new object
            OtherDetail oOtherDetail;
            try
            {
                oOtherDetail = new OtherDetail();
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
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CardioVascular, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Add, "clsCardioVascular -- ItemDetails -- Add -- " + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                //UpdateLog("clsCardioVascular -- ItemDetails -- Add -- " & ex.ToString)
                //MessageBox.Show(ex.ToString(), "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return functionReturnValue;
        }

        public OtherDetail this[object vntIndexKey]
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

}
