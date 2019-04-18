using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Xml;
using System.Xml.Linq;
using System.Windows.Data;
using System.Windows;

namespace gloUIControlLibrary.Classes.ICD10
{
    public class clsICD10DBLayer
    {
        public string ConnectionString { get; set; }

        public clsICD10DBLayer(string ConnectionString)
        { this.ConnectionString = ConnectionString; }

        public XmlDataProvider GetICD10Notes()
        {
            XmlDataProvider xmlDataProvider = new XmlDataProvider();
            XmlDocument xmlNotesDocument = new XmlDocument();

            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("ICD10_GetNotes", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            using (DataTable dt = new DataTable())
                            {
                                da.Fill(dt);

                                if (dt != null && dt.Rows.Count >0)
                                {
                                    string sXML = Convert.ToString(dt.Rows[0]["XMLDATA"]);
                                    xmlNotesDocument.LoadXml(sXML);
                                    xmlDataProvider.Document = xmlNotesDocument;

                                }
                            }
                        }
                    }
                }

                return xmlDataProvider;
            }
            catch (Exception Ex)
            {
                LogException.ExceptionLog(Ex.ToString(), true);
                return xmlDataProvider;
            }            

        }

        public DataTable LoadCategoryCodes(string codeStart, string codeEnd)
        {
            DataSet ds = new System.Data.DataSet();

            try
            {
                using (SqlConnection cn = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("ICD10_ICDListSearch_Gallery", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.CommandTimeout = 0;

                        cmd.Parameters.Add(new SqlParameter("@CodeStartKeyword", codeStart));
                        cmd.Parameters.Add(new SqlParameter("@CodeEndKeyword", codeEnd));

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        { da.Fill(ds); }
                    }
                }

                return ds.Tables[0].Copy();
            }
            catch (Exception Ex)
            {
                LogException.ExceptionLog(Ex.ToString(), true);
                return null;
            }
            finally
            {
                if (ds != null)
                {
                    ds.Dispose();
                    ds = null;
                }
            }
        }

        public DataTable GetParentCodes(string code)
        {
            DataSet ds = new System.Data.DataSet();

            try
            {
                using (SqlConnection cn = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("ICD10_GetICD10CodeParent", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.CommandTimeout = 0;

                        cmd.Parameters.Add(new SqlParameter("@Code", code));                        

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        { da.Fill(ds); }
                    }
                }

                return ds.Tables[0].Copy();
            }
            catch (Exception Ex) 
            { 
                LogException.ExceptionLog(Ex.ToString(), true);
                return null;
            }
            finally
            {
                if (ds != null)
                {
                    ds.Dispose();
                    ds = null;
                }
            }
        }

        public DataTable GetSpeciality(bool IncludeAll)
        {
            DataSet ds = new System.Data.DataSet();

            try
            {
                using (SqlConnection cn = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("gsp_FillSpecialty_MST", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(ds);

                            if (IncludeAll)
                            {
                                DataRow drAll = null;
                                drAll = ds.Tables[0].NewRow();
                                drAll["nSpecialtyId"] = 0;
                                drAll["sDescription"] = "All";
                                ds.Tables[0].Rows.Add(drAll);
                                drAll = null;
                            }
                        }
                    }
                }
                return ds.Tables[0].Copy();
            }
            catch (Exception Ex) 
            { 
                LogException.ExceptionLog(Ex.ToString(), true);
                return null;
            }
            finally
            {
                if (ds != null)
                {
                    ds.Dispose();
                    ds = null;
                }
            }
            
        }        

        public DataTable GetRemovedICD10Codes(string StartCode, string EndCode, string Code)
        {
            DataSet ds = new System.Data.DataSet();
            try
            {
                using (SqlConnection cn = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("ICD10_GetRemovedCode", cn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@CodeStart", StartCode ?? string.Empty));
                        cmd.Parameters.Add(new SqlParameter("@CodeEnd", EndCode ?? string.Empty));
                        cmd.Parameters.Add(new SqlParameter("@Code", Code ?? string.Empty));

                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(ds);
                        }
                    }
                }
                return ds.Tables[0].Copy();
            }
            catch (Exception Ex) 
            { 
                LogException.ExceptionLog(Ex.ToString(), true);
                return null;
            }
            finally
            {
                if (ds != null)
                {
                    ds.Dispose();
                    ds = null;
                }
            }
        }

        public DataView GetCategoryTreeSource(string p)
        {
            DataView dv = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = cn;
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.CommandText = "ICD10_GetCodeGuidesCode";
                        cmd.Parameters.Add(new SqlParameter("@SearchText", p));

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataSet ds = new System.Data.DataSet();
                            da.Fill(ds);

                            DataRelation dr = new DataRelation("rsParentChild", ds.Tables[0].Columns["sICD10Code"], ds.Tables[0].Columns["sICDParentCode"], false);
                            ds.Relations.Add(dr);
                            dv = ds.Tables[0].DefaultView;
                            dv.RowFilter = "sICDParentCode= ''";
                        }
                    }
                }
                return dv;
            }
            catch (Exception Ex)
            {
                LogException.ExceptionLog(Ex.ToString(), true);
                return null;
            }                       
        }

        public void DeleteICD10Code(string ICD10Code)
        {

            try
            {
                using (SqlConnection cn = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("DELETE FROM dbo.ICD9 WHERE nICDRevision = 10 AND sICD9Code = '" + ICD10Code + "'", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.Connection.Open();
                        cmd.ExecuteNonQuery();
                        cmd.Connection.Close();
                    }
                }
            }
            catch (Exception Ex) { LogException.ExceptionLog(Ex.ToString(), true); }
           
        }

        public bool IsInUseICD(string ICDCode)
        {
            bool result = true;

            try
            {
                using (SqlConnection cn = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("gsp_IsCodeUsed", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.Add(new SqlParameter("@sCode", ICDCode));
                        cmd.Parameters.Add(new SqlParameter("@nGalleryType", 10));

                        cmd.Connection.Open();
                        object cnt = Convert.ToBoolean(cmd.ExecuteScalar());
                        cmd.Connection.Close();

                        if (cnt != null && Convert.ToInt32(cnt) > 0)
                        { result = true; }
                        else
                        { result = false; }
                    }
                }
                return result;
            }
            catch (Exception Ex) 
            { 
                LogException.ExceptionLog(Ex.ToString(), true);
                return result;
            }
        }

        public void SaveICD10Codes(DataTable Codes)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("ICD10_SaveMasterCodes", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        SqlParameter sqlParameter = new SqlParameter();

                        sqlParameter.SqlDbType = SqlDbType.Structured;
                        sqlParameter.Direction = ParameterDirection.Input;
                        sqlParameter.ParameterName = "@TVP";
                        sqlParameter.Value = Codes;

                        cmd.Parameters.Add(sqlParameter);

                        cmd.Connection.Open();
                        cmd.ExecuteNonQuery();
                        cmd.Connection.Close();

                    }
                }
            }
            catch (Exception Ex) { LogException.ExceptionLog(Ex.ToString(), true); }
                                               
        }

        public DataTable GetMasterCD10Codes(Int64 SpecialityID, string SearchString)
        {
            DataSet ds = new System.Data.DataSet();

            try
            {
                using (SqlConnection cn = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("ICD10_GetMasterICD10Codes", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.Add(new SqlParameter("@nSpecialityID", SpecialityID));
                        cmd.Parameters.Add(new SqlParameter("@sSearchString", SearchString));

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd)) { da.Fill(ds); }
                    }
                }
                return ds.Tables[0].Copy();
            }
            catch (Exception Ex)
            {
                LogException.ExceptionLog(Ex.ToString(), true);
                return null;
            }
            finally
            {
                if (ds != null)
                {
                    ds.Dispose();
                    ds = null;
                }
            }

        }        
    }
}
