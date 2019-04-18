using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using gloGlobal.FS3;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace gloGlobal.FS3
{
    public class gloSuiteDBHelper  : IDisposable
    {
        private string connectionString { get; set; }

        public gloSuiteDBHelper(string ConnectionString)
        {
            this.connectionString = ConnectionString;
        }

        public List<gloGlobal.DIB.AlternativeDrugDetails> GetAllStrengthAlternatives(List<AlternativeDrug> PayerAlternatives)
        {
            List<gloGlobal.DIB.AlternativeDrugDetails> data = new List<gloGlobal.DIB.AlternativeDrugDetails>();

            //DataTable dtParam = null;
            SqlParameter p = null;
            DataRow row = null;
            IEnumerable<gloGlobal.DIB.AlternativeDrugDetails> result = null;
            DataRow dRow = null;

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("FS3_GetAllStrengthAlternatives", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (DataTable dtTVP = new DataTable())
                        {
                            dtTVP.Columns.Add(new DataColumn("mpid", Type.GetType("System.Int32")));
                            dtTVP.Columns.Add(new DataColumn("PreferenceLevel", Type.GetType("System.Int32")));
                            dtTVP.Columns.Add(new DataColumn("FormularyStatus", Type.GetType("System.Int32")));

                            foreach (AlternativeDrug alternativeDrug in PayerAlternatives)
                            {
                                dRow = dtTVP.NewRow();

                                dRow["mpid"] = alternativeDrug.id;
                                dRow["PreferenceLevel"] = alternativeDrug.pl;
                                dRow["FormularyStatus"] = alternativeDrug.fs;

                                dtTVP.Rows.Add(dRow);
                                dRow = null;
                            }

                            p = new SqlParameter();
                            p.Direction = ParameterDirection.Input;
                            p.ParameterName = "@TVP";
                            p.SqlDbType = SqlDbType.Structured;
                            p.Value = dtTVP;
                            cmd.Parameters.Add(p);
                        }

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            using (DataTable dt = new DataTable())
                            {
                                da.Fill(dt);

                                if (dt != null && dt.Rows.Count >= 0)
                                {
                                        result = dt.AsEnumerable().Select
                                        (alt => new gloGlobal.DIB.AlternativeDrugDetails
                                        {
                                            id = Convert.ToInt64(alt["mpid"]),
                                            fs = alt["sStatus"].ToString(),
                                            pl = alt["PreferenceLevel"].ToString(),

                                            DrugName = alt["sDisplayDrugName"].ToString(),
                                            RxType = alt["RxType"].ToString(),
                                            NDC = alt["sNDCCode"].ToString(),
                                            IsPayerAlternative =Convert.ToBoolean(alt["bIsPayer"])
                                        });

                                        data = result.ToList<gloGlobal.DIB.AlternativeDrugDetails>();
                                }
                            }
                        }
                    }
                }
            }
            catch (SqlException)// ex)
            {
                //throw ex;
                return null;
            }
            finally
            {
                if (p != null) { p = null; }
                if (row != null) { row = null; }
                result = null;
            }

            return data;
        }

       
        
        public void Dispose()
        {
        }
    }  
}
