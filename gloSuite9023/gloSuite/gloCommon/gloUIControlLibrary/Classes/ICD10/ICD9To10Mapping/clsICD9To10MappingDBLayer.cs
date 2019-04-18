using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace gloUIControlLibrary.Classes.ICD10.ICD9To10Mapping
{
    public class clsICD9To10MappingDBLayer
    {
        public string ConnectionString { get; set; }

        public clsICD9To10MappingDBLayer(string ConnectionString)
        { this.ConnectionString = ConnectionString; }

        public List<ICD9To10Mapping> Get9To10Mapping(string ICD9Code)
        {
            List<ICD9To10Mapping> codeList = new List<ICD9To10Mapping>();
            
            try
            {
                using (SqlConnection cn = new SqlConnection(this.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("ICD10_ICD9To10Mapping", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.CommandTimeout = 0;

                        cmd.Parameters.Add(new SqlParameter("@sICD9DecimalCode", ICD9Code));

                        using (DataSet ds = new DataSet())
                        {
                            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                            { da.Fill(ds); }

                            foreach (DataRow dataRow in ds.Tables[0].Rows)
                            {
                                ICD9To10Mapping obj = new ICD9To10Mapping();

                                obj.ICD9code = dataRow["sICD9Code"].ToString();
                                obj.ICD10code = dataRow["sICD10Code"].ToString();
                                obj.Description = dataRow["sDescriptionLong"].ToString();
                                obj.Flag = dataRow["sFlag"].ToString();
                                obj.ICD9Decimalcode = dataRow["sICD9DecimalCode"].ToString();

                                obj.Approximate = dataRow["nApproximateFlag"].ToString();
                                obj.NoMap = Convert.ToInt32(dataRow["nNoMapFLAG"]);
                                obj.Combination = Convert.ToInt32(dataRow["nCombinationFLAG"]);
                                obj.Scenario = Convert.ToInt32(dataRow["nScenario"]);
                                obj.choiceList = Convert.ToInt32(dataRow["nChoiceList"]);
                                codeList.Add(obj);

                                obj = null;
                            }   
                        }                        
                    }
                }

                return codeList;
            }
            catch (Exception Ex)
            {
                LogException.ExceptionLog(Ex.ToString(), true);
                return null;
            }          
        }

        public DataView GetCategoryTreeSource(string ICD10Code)
        {
            DataView dv = null;

            using (SqlConnection cn = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = cn;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.CommandText = "ICD10_GetAllCodesUnderThisCodeParent";
                    cmd.Parameters.Add(new SqlParameter("@ICD10Code", ICD10Code));


                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataSet ds = new System.Data.DataSet();
                        da.Fill(ds);

                        string sParentCode = ds.Tables[1].Rows[0]["sICDCode"].ToString();

                        if (ds.Tables[0].AsEnumerable().FirstOrDefault(a => a["sICDCode"].ToString() == sParentCode) != null)
                        { ds.Tables[0].AsEnumerable().FirstOrDefault(a => a["sICDCode"].ToString() == sParentCode)["sICDParentCode"] = DBNull.Value; }

                        ds.Relations.Add("rsParentChild", ds.Tables[0].Columns["sICDCode"], ds.Tables[0].Columns["sICDParentCode"]);

                        dv = ds.Tables[0].DefaultView;
                        dv.RowFilter = "[sICDParentCode] is null";
                    }
                }
            }
            return dv;
        }

        public DataTable Get9Codes(string SearchText)
        {

            DataTable dtReturned = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(this.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("ICD10_Search9Codes", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.CommandTimeout = 0;

                        cmd.Parameters.Add(new SqlParameter("@SearchText", SearchText));

                        using (DataSet ds = new DataSet())
                        {
                            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                            { da.Fill(ds); }

                            dtReturned = ds.Tables[0].Copy();
                        }
                    }
                }

                return dtReturned;
            }
            catch (Exception Ex)
            {
                LogException.ExceptionLog(Ex.ToString(), true);
                return null;
            }          
        }

        public ICD9To10MappingContainer LoadMappingCodes(string ICD9Code)
        {
            List<ICD9To10Mapping> ListOfMappingCodes = new List<ICD9To10Mapping>();
            ICD9To10MappingContainer ICD9To10MappingContainer = new ICD9To10MappingContainer();
            try
            {
                ListOfMappingCodes = this.Get9To10Mapping(ICD9Code);                 
                ICD9To10MappingContainer.GenerateStructure(ListOfMappingCodes);

                return ICD9To10MappingContainer;
            }
            catch (Exception Ex)
            { 
                LogException.ExceptionLog(Ex.ToString(), true);
                return null;
            }

            finally
            {
                if (ListOfMappingCodes != null)
                {
                    ListOfMappingCodes.Clear();
                    ListOfMappingCodes = null;
                }
            }
        }

        
    }
}
