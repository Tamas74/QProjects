using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using gloEMRGeneralLibrary.gloEMRDatabase;
using System.Data;
using System.Windows.Forms;
namespace gloCommunity.Classes
{
    class clsTaskMail
    {

        public DataTable  GetPriorities()
        {
            DataBaseLayer oDB = new DataBaseLayer();
            DataTable oResultTable = new DataTable();

            string strQuery = "";
            try
            {
                strQuery = "select bIsSystem as 'Select',sDescription as 'Description',Case bIsSystem when 0 then 'User' else '' end 'Record Type' ,'Priorities' as Category,nLevel  from TM_Priority_MST where bIsBlocked='" + false + "'and   nClinicID =" + clsGeneral.gClinicID + " and bIsSystem=0";
                oResultTable = oDB.GetDataTable_Query(strQuery);
                return oResultTable;
            }
           catch
            {
                return null;
            }
           
            finally
            {
               oDB.Dispose();
               oDB = null;
            }
        }


        public bool CompareXMlData(DataTable local,DataTable server,string FilePath)
        {
            bool changedata = false; 
            DialogResult Result;
            foreach (DataRow dr in local.Rows)
            {
                DataRow[] drr = server.Select("Description='" + dr["Description"].ToString() + "' and Category='" + dr["Category"].ToString() + "'");
                if (drr.Length == 0)
                {
                    server.ImportRow(dr);
                    changedata = true; 
                }
                else
                {
                 Result = MessageBox.Show("For Category '" + dr["Category"].ToString() + "' '" + dr["Description"].ToString() + "' Already Exist Do you want to Overwrite ?", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                 if (Result == DialogResult.Yes)
                      {
                         
                         
                              server.Rows.Remove(drr[0]);
                              server.ImportRow(dr);
                              changedata = true;     
                 }
                }
               
            }

            server.WriteXml(FilePath);
            return changedata; 
        }


        public DataTable  GetFollowUps()
        {
            DataBaseLayer oDB = new DataBaseLayer();
            DataTable oResultTable = new DataTable();

            string strQuery = "";
            try
            {
                strQuery = "select bIsSystem as 'Select',sDescription as 'Description',Case bIsSystem when 0 then 'User' else '' end 'Record Type','FollowUp' as Category,-1 as 'nLevel'  from TM_FollowUp_MST where bIsBlocked='" + false + "' AND nClinicID = " + clsGeneral.gClinicID + " and bIsSystem=0";
                oResultTable = oDB.GetDataTable_Query(strQuery);
                return oResultTable;
            }
            catch
            {
                return null;
            }

            finally
            {

                oDB.Dispose();
                oDB = null;
            }
        }

      
        public DataTable GetStatuses()
        {
            DataBaseLayer oDB = new DataBaseLayer();
            DataTable oResultTable = new DataTable();

            string strQuery = "";
            try
            {
                strQuery = "select bIsSystem as 'Select',sDescription as 'Description', Case bIsSystem when 0 then 'User' else '' end 'Record Type','Status' as Category,-1 as 'nLevel'   from TM_Status_MST where bIsBlocked='" + false + "' AND nClinicID = " + clsGeneral.gClinicID + " and bIsSystem=0 ";
                oResultTable = oDB.GetDataTable_Query(strQuery);
                return oResultTable;
            }
       
            catch
            {
                return null;
            }

            finally
            {

                oDB.Dispose();
                oDB = null;
            }
        }

        public bool InsertStatuses(string Desc,bool bissystem,Int64 ClinicID,bool bisblocked)
        {
            DataBaseLayer oDB = new DataBaseLayer();
            DataTable oResultTable = new DataTable();
             DBParameter oParamater = default(DBParameter);
            string strQuery = "";
            try
            {
                strQuery = "gsp_insertglocomm_TM_Stat_Mst"; 
	            oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.VarChar;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@sDesc";
                oParamater.Value = Desc;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;


                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.Bit ;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@bissystem";
                oParamater.Value = bissystem;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

              
                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.Bit;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@nClinicID";
                oParamater.Value = ClinicID;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;


                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.Bit;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@bisBlocked";
                oParamater.Value = bisblocked;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

               oDB.ExecuteNon_Query(strQuery);
               return true;
            }

            catch
            {
                return false;
            }

            finally
            {

                oDB.Dispose();
                oDB = null;
            }
        }




        public bool InsertFollowUps(string Desc, bool bissystem, Int64 ClinicID, bool bisblocked)
        {
            DataBaseLayer oDB = new DataBaseLayer();
            DataTable oResultTable = new DataTable();
            DBParameter oParamater = default(DBParameter);
            string strQuery = "";
            try
            {
                strQuery = "gsp_insertglocomm_TM_FollowUp_MST"; 
	            oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.VarChar;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@sDesc";
                oParamater.Value = Desc;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;
               
                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.Bit ;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@bissystem";
                oParamater.Value = bissystem;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

              
                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.Bit;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@nClinicID";
                oParamater.Value = ClinicID;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;


                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.Bit;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@bisBlocked";
                oParamater.Value = bisblocked;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

             oDB.ExecuteNon_Query(strQuery);
             return true;
            }

            catch
            {
                return false;
            }

            finally
            {

                oDB.Dispose();
                oDB = null;
            }

       }




        public bool InsertPriorities(string Desc, bool bissystem, Int64 ClinicID, bool bisblocked,Int64 level)
        {
            DataBaseLayer oDB = new DataBaseLayer();
            DataTable oResultTable = new DataTable();
            DBParameter oParamater = default(DBParameter);

            string strQuery = "gsp_insertglocomm_TM_Priority_MST";
            try
            {
                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.VarChar;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@sDesc";
                oParamater.Value = Desc;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.Bit;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@bissystem";
                oParamater.Value = bissystem;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;


                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.Bit;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@nClinicID";
                oParamater.Value = ClinicID;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;


                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.Bit;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@bisBlocked";
                oParamater.Value = bisblocked;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.Decimal;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@nLevel";
                oParamater.Value = level;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;
                
                oDB.ExecuteNon_Query(strQuery);
                
                return true;
            }
            catch
            {
                return false;
            }

            finally
            {
                oDB.Dispose();
                oDB = null;
            }
        }

    }
}
