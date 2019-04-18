using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;


namespace gloRxHub.FS3
{
    public class gloDBHelper : BaseDBHelper
    {
        public string GetFormularyStatus(string SenderID, string FormularyId, Int64 DDID, string RxType)
        {
            string status = string.Empty;
            SqlParameter p = new SqlParameter();

            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("FS3_GetFormularyStatusByDrug", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        p.Direction = ParameterDirection.Input;
                        p.ParameterName = "@SenderID";
                        p.DbType = DbType.String;
                        p.Value = SenderID;
                        cmd.Parameters.Add(p);

                        p = new SqlParameter();
                        p.Direction = ParameterDirection.Input;
                        p.ParameterName = "@FormularyID";
                        p.DbType = DbType.String;
                        p.Value = FormularyId;
                        cmd.Parameters.Add(p);

                        p = new SqlParameter();
                        p.Direction = ParameterDirection.Input;
                        p.ParameterName = "@DDID";
                        p.DbType = DbType.Int64;
                        p.Value = DDID;
                        cmd.Parameters.Add(p);

                        p = new SqlParameter();
                        p.Direction = ParameterDirection.Input;
                        p.ParameterName = "@RxType";
                        p.DbType = DbType.String;
                        p.Value = RxType;
                        cmd.Parameters.Add(p);

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            using (DataTable dt = new DataTable())
                            {
                                da.Fill(dt);
                                if (dt != null)
                                {
                                    IEnumerable<string> result = dt.AsEnumerable().Select(fs => Convert.ToString(fs["FormularyStatus"]));

                                    status = result.FirstOrDefault();
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (p != null) { p = null; }
            }
            return status;
        }

        public List<CopayFactor> GetCopayFactor(string SenderID, string CopayID, Int64 DDID, string FormularyStatusID)
        {
            List<CopayFactor> data = new List<CopayFactor>();

            DataTable dtParam = null;
            SqlParameter p = null;
            DataRow row = null;

            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("FS3_GetCopayFactors", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        p = new SqlParameter();
                        p.Direction = ParameterDirection.Input;
                        p.ParameterName = "@SenderID";
                        p.DbType = DbType.String;
                        p.Value = SenderID;
                        cmd.Parameters.Add(p);

                        p = new SqlParameter();
                        p.Direction = ParameterDirection.Input;
                        p.ParameterName = "@CopayID";
                        p.DbType = DbType.String;
                        p.Value = CopayID;
                        cmd.Parameters.Add(p);

                        p = new SqlParameter();
                        p.Direction = ParameterDirection.Input;
                        p.ParameterName = "@DDID";
                        p.DbType = DbType.Int64;
                        p.Value = DDID;
                        cmd.Parameters.Add(p);

                        p = new SqlParameter();
                        p.Direction = ParameterDirection.Input;
                        p.ParameterName = "@FormularyStatusID";
                        p.DbType = DbType.String;
                        p.Value = FormularyStatusID;
                        cmd.Parameters.Add(p);

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            using (DataTable dt = new DataTable())
                            {
                                da.Fill(dt);

                                if (dt != null && dt.Rows.Count >= 0)
                                {
                                    IEnumerable<CopayFactor> result = dt.AsEnumerable().Select
                                        (cop => new CopayFactor
                                        {
                                            ddid = cop["ProductID"].ToString(),
                                            type = cop["CopayListType"].ToString(),

                                            flat = cop["FlatCoPayAmount"].ToString(),
                                            rate = cop["PercentCoPayRate"].ToString(),
                                            ptype = cop["PharmacyType"].ToString(),

                                            firstterm = cop["FirstCoPayTerm"].ToString(),
                                            mincop = cop["MinimumCoPay"].ToString(),
                                            maxcop = cop["MaximumCoPay"].ToString(),

                                            days = cop["DaysSupplyPerCoPay"].ToString(),
                                            mintier = cop["CoPayTier"].ToString(),
                                            maxtier = cop["MaximumCoPayTier"].ToString(),

                                            spocket = cop["OutOfPocketRangeStart"].ToString(),
                                            epocket = cop["OutOfPocketRangeEnd"].ToString()

                                        });

                                    data = result.ToList<CopayFactor>();
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (p != null) { p = null; }
                if (row != null) { row = null; }
                if (dtParam != null) { dtParam.Dispose(); dtParam = null; }
            }

            return data;
        }

        public List<CoverageFactor> GetCoverageFactor(DrugFormularyParameter Params)
        {
            return null;
        }
    }

    public class BaseDBHelper
    {
        public string ConnectionString { get { return GetConnectionString(); } }

        private string GetConnectionString()
        {
            try
            {
                return ConfigurationManager.ConnectionStrings["fs30_staging"].ConnectionString;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }
    }
}
