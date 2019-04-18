using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using gloEMRGeneralLibrary.gloEMRDatabase;
using System.Data.SqlClient;
using System.Collections;
using System.Windows.Forms;

namespace gloCommunity.Classes
{
    class clsSmartOrder
    {
        

        public DataTable FillControl(long ID)
        { SqlConnection Conn=null;
          System.Data.SqlClient.SqlCommand Cmd=null;
            try
            {
                DataTable dt = new DataTable();
                SqlDataAdapter sqladpt = new SqlDataAdapter();
                Conn = new SqlConnection(clsGeneral.EMRConnectionString);
                //Check connection states
                if (Conn.State == ConnectionState.Closed)
                {
                    Conn.Open();
                }
                Cmd = new System.Data.SqlClient.SqlCommand("gsp_FillOderset", Conn);
                Cmd.CommandType = CommandType.StoredProcedure;
                sqladpt.SelectCommand = Cmd;

                SqlParameter objParam = null;

                objParam = Cmd.Parameters.Add("@flag", SqlDbType.BigInt);
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = ID;

                sqladpt.Fill(dt);
                sqladpt.Dispose();
                sqladpt = null;
                return dt;
            }
            catch //(Exception ex)
            {
                return null;
            }
            finally
            {
                if (Conn != null)
                {
                    Conn.Close();
                    Conn.Dispose();
                    Conn = null;
                }


               
             
                if (Cmd != null)
                {
                    Cmd.Parameters.Clear();
                    Cmd.Dispose();
                    Cmd = null;
                }
             
            }
        }

        public DataTable FetchOrderforUpdate(long id)
        {
            SqlConnection Conn = null;
            System.Data.SqlClient.SqlCommand Cmd = null;
            try
            {
                Conn = new SqlConnection(clsGeneral.EMRConnectionString);

                if (Conn.State == ConnectionState.Closed)
                {
                    Conn.Open();
                }

                DataTable dt = new DataTable();
                SqlDataAdapter sqladpt = new SqlDataAdapter();

                Cmd = new System.Data.SqlClient.SqlCommand("gsp_scanOrderAssociation", Conn);
                Cmd.CommandType = CommandType.StoredProcedure;
                sqladpt.SelectCommand = Cmd;

                SqlParameter objParam = default(SqlParameter);

                objParam = Cmd.Parameters.Add("@nOrderID", SqlDbType.BigInt);
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = id;

                sqladpt.Fill(dt);
                sqladpt.Dispose();
                sqladpt = null;
                if (objParam != null)
                    objParam = null;
                return dt;
            }
            catch //(SqlException ex)
            {
                return null;
            }
            //catch (Exception ex)
            //{
            //    return null;
            //}
            finally
            {
                if (Conn != null)
                {
                    Conn.Close();
                    Conn.Dispose();
                    Conn = null;
                }




                if (Cmd != null)
                {
                    Cmd.Parameters.Clear();
                    Cmd.Dispose();
                    Cmd = null;
                }

            }
        }

        public bool AddData(long OrderID, string OrderName, ArrayList arrlist)
        {
            SqlConnection Conn = new SqlConnection(clsGeneral.EMRConnectionString);
            if (Conn.State == ConnectionState.Closed)
            {
                Conn.Open();
            }

            SqlTransaction trOrderAssociation = default(SqlTransaction);
            trOrderAssociation = Conn.BeginTransaction();
            SqlCommand cmddelete = null;
            try
            {
                int i = 0;
                SqlParameter oParam = default(SqlParameter);

                cmddelete = new System.Data.SqlClient.SqlCommand("gsp_DeleteOrders", Conn);
                cmddelete.CommandType = CommandType.StoredProcedure;
                cmddelete.Transaction = trOrderAssociation;

                oParam = cmddelete.Parameters.Add("@nOrderID", SqlDbType.BigInt);
                oParam.Direction = ParameterDirection.Input;
                oParam.Value = OrderID;


                cmddelete.ExecuteNonQuery();
                cmddelete.Parameters.Clear();


                for (i = 0; i <= arrlist.Count - 1; i++)
                {
                    myList oMyList = default(myList);
                    oMyList = (myList)arrlist[i];


                    SqlCommand Cmd = new System.Data.SqlClient.SqlCommand("gsp_InsertOrders", Conn);

                    Cmd.CommandType = CommandType.StoredProcedure;
                    Cmd.Transaction = trOrderAssociation;

                    oParam = Cmd.Parameters.Add("@nOrderID", SqlDbType.BigInt);
                    oParam.Value = OrderID;

                    oParam = Cmd.Parameters.Add("@nAssociateID", SqlDbType.BigInt);
                    oParam.Value = oMyList.Index;

                    oParam = Cmd.Parameters.Add("@AssociateType", SqlDbType.VarChar);
                    oParam.Value = oMyList.Description;

                    //' Add name of the associate ..
                    oParam = Cmd.Parameters.Add("@sAssociateName", SqlDbType.VarChar);
                    ///'means Drug, then save the oMyList.Drugname val in sAssociateName field, else save the oMyList.ParameterName, becaz when the drug is shown in Rx-Meds for it shows with concated drugname and Drug Form
                    if (oMyList.Description == "D")
                    {
                        oParam.Value = oMyList.DrugName;
                    }
                    else
                    {
                        oParam.Value = oMyList.ParameterName;
                    }

                    oParam = Cmd.Parameters.Add("@sDosage", SqlDbType.VarChar);
                    oParam.Value = oMyList.Dosage;

                    oParam = Cmd.Parameters.Add("@sDrugForm", SqlDbType.VarChar);
                    oParam.Value = oMyList.DrugForm;

                    oParam = Cmd.Parameters.Add("@sRoute", SqlDbType.VarChar);
                    if ((oMyList.Route == null))
                    {
                        oParam.Value = "";
                    }
                    else
                    {
                        oParam.Value = oMyList.Route;
                    }

                    oParam = Cmd.Parameters.Add("@sFrequency", SqlDbType.VarChar);
                    if ((oMyList.Frequency == null))
                    {
                        oParam.Value = "";
                    }
                    else
                    {
                        oParam.Value = oMyList.Frequency;
                    }

                    oParam = Cmd.Parameters.Add("@sNDCCode", SqlDbType.VarChar);
                    if ((oMyList.NDCCode == null))
                    {
                        oParam.Value = "";
                    }
                    else
                    {
                        oParam.Value = oMyList.NDCCode;
                    }

                    oParam = Cmd.Parameters.Add("@nIsNarcotics", SqlDbType.Int);
                    //if ((oMyList.IsNarcotic == null))
                    //{
                    //    oParam.Value = "";
                    //}
                    //else
                    {
                        oParam.Value = oMyList.IsNarcotic;
                    }

                    oParam = Cmd.Parameters.Add("@sDuration", SqlDbType.VarChar);
                    if ((oMyList.Duration == null))
                    {
                        oParam.Value = "";
                    }
                    else
                    {
                        oParam.Value = oMyList.Duration;
                    }

                    oParam = Cmd.Parameters.Add("@mpid", SqlDbType.Int);
                    //if ((oMyList.DDid == null))
                    //{
                    //    oParam.Value = 0;
                    //}
                    //else
                    {
                        oParam.Value = oMyList.mpid;
                    }

                    oParam = Cmd.Parameters.Add("@sDrugQtyQualifier", SqlDbType.VarChar);
                    if ((oMyList.DrugQtyQualifier == null))
                    {
                        oParam.Value = "";
                    }
                    else
                    {
                        oParam.Value = oMyList.DrugQtyQualifier;
                    }
                    ///' Insert Order Lab 

                    oParam = Cmd.Parameters.Add("@bStatus", SqlDbType.Bit);
                    oParam.Direction = ParameterDirection.Input;
                    oParam.Value = oMyList.ItemChecked;

                    Cmd.ExecuteNonQuery();

                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartOrders, gloAuditTrail.ActivityType.Add, "Order Association Added to Lab for " + Convert.ToString(OrderName), 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);

                    Cmd.Parameters.Clear();
                    Cmd.Dispose();
                    Cmd = null;
                }

                trOrderAssociation.Commit();
                Conn.Close();
                if (oParam != null)
                    oParam = null;
                return true;

            }
            catch //(Exception ex)
            {
                trOrderAssociation.Rollback();
                trOrderAssociation = null;
                //Cmd = null;
                //cmddelete = null;
                Conn.Close();
                Conn = null;
                //MessageBox.Show(ex.ToString(),clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                if (cmddelete != null)
                {
                    cmddelete.Parameters.Clear();
                    cmddelete.Dispose();
                    cmddelete = null;
                }
                if (trOrderAssociation != null)
                {
                    trOrderAssociation.Dispose();
                    trOrderAssociation = null;
                }
            }
        }

        public string GetOrderIDFromCode(string sDescription, string sCategoryType, long nClinicID)
        {
            DataBaseLayer oDBc = new DataBaseLayer();
            try
            {
                DBParameter oParamaterc = default(DBParameter);
                
                oParamaterc = new DBParameter();
                oParamaterc.DataType = SqlDbType.VarChar;
                oParamaterc.Direction = ParameterDirection.Input;
                oParamaterc.Name = "@sDescription";
                oParamaterc.Value = sDescription;
                oDBc.DBParametersCol.Add(oParamaterc);
                oParamaterc = null;

                oParamaterc = new DBParameter();
                oParamaterc.DataType = SqlDbType.VarChar;
                oParamaterc.Direction = ParameterDirection.Input;
                oParamaterc.Name = "@sCategoryType";
                oParamaterc.Value = sCategoryType;
                oDBc.DBParametersCol.Add(oParamaterc);
                oParamaterc = null;

                oParamaterc = new DBParameter();
                oParamaterc.DataType = SqlDbType.BigInt;
                oParamaterc.Direction = ParameterDirection.Input;
                oParamaterc.Name = "@nClinicID";
                oParamaterc.Value = nClinicID;
                oDBc.DBParametersCol.Add(oParamaterc);
                oParamaterc = null;

                object OrderID = null;
                OrderID = oDBc.GetDataValue("getOrderIDformCode");

                if (OrderID != null)
                    return OrderID.ToString();
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

        public int CheckOrderAssociated(Int64 nOrderID)
        {
            DataBaseLayer oDBc = new DataBaseLayer();
            try
            {
                DBParameter oParamaterc = default(DBParameter);

                oParamaterc = new DBParameter();
                oParamaterc.DataType = SqlDbType.BigInt;
                oParamaterc.Direction = ParameterDirection.Input;
                oParamaterc.Name = "@nOrderID";
                oParamaterc.Value = nOrderID;
                oDBc.DBParametersCol.Add(oParamaterc);
                oParamaterc = null;

                object objICD9ID = null;
                objICD9ID = oDBc.GetDataValue("CHKSPOrderAssociated");

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
