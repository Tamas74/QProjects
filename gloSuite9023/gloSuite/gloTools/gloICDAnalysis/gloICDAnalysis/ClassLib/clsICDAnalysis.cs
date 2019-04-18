using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace gloICDAnalysis.ClassLib
{
    public class clsICDAnalysis
    {
        public enum ProviderType
        { 
            Exam,
            Claim
        }
               
        public clsICDAnalysis()
        { 

        }

        public static DataTable GetAllProvider(ProviderType type)
        { 
            // Add the "All" item in the list, which will be default
            // gsp_GetProviderByType
            SqlConnection con = null;
            SqlCommand cmd = null;
            SqlDataAdapter adp = null;
            DataTable dtProvider=null;
            try
            {
                con = new SqlConnection(clsDBSettings.CurrentDBInfo.ConnectionString); 
                cmd = new SqlCommand("gsp_GetProviderByType", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@ProviderType", SqlDbType.VarChar, 10).Value = type.ToString();
                    
                adp = new SqlDataAdapter(cmd);
                dtProvider = new DataTable();
                adp.Fill(dtProvider);
               
            }
            catch (Exception ex)
            {
                UpdateLog(ex.ToString(), true);
            }
            
            finally 
            {
               
                if (adp != null)
                {
                    adp.Dispose();
                    adp = null;
                }
                if (cmd != null)
                {
                    cmd.Parameters.Clear();
                    cmd.Dispose();
                    cmd = null;
                }
                if (con != null)
                {
                    con.Dispose();
                    con = null;
                }

               
            }
            return dtProvider;
            
        }

        public static DataTable GetICDsByFilter(clsICDAnalysis.ProviderType providerType, Int64? ProviderID, DateTime StartDate, DateTime EndDate,Int32 dbVersion, Int32 ICDType)
        {
            // Add the "All" item in the list, which will be default
            // gsp_GetICDUsageByFilters
            SqlConnection con = null;
            SqlCommand cmd = null;
            SqlDataAdapter adp = null;
             DataTable dtICDs=null;
            try 
	        {
                con = new SqlConnection(clsDBSettings.CurrentDBInfo.ConnectionString); 

                if (dbVersion < 8010)
                {
                    cmd = new SqlCommand("gsp_GetICDUsageByProviderRevised_7022", con);
                }
                else
                {
                    cmd = new SqlCommand("gsp_GetICDUsageByProviderRevised", con);
                }

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@ProviderType", SqlDbType.VarChar, 10).Value = providerType.ToString();
                cmd.Parameters.Add("@ProviderID", SqlDbType.BigInt, 15).Value = ProviderID;
                cmd.Parameters.Add("@dtStartDate", SqlDbType.Date, 10).Value = StartDate.ToShortDateString();
                cmd.Parameters.Add("@dtEndDate", SqlDbType.Date, 10).Value = EndDate.ToShortDateString();
                cmd.Parameters.Add("@ICDType", SqlDbType.BigInt, 10).Value = ICDType;
                adp = new SqlDataAdapter(cmd);
                dtICDs = new DataTable();
                adp.Fill(dtICDs);
	        }
	        catch (Exception ex)
	        {
                UpdateLog(ex.ToString(), true);
	        }
            finally
            {

                if (adp != null)
                {
                    adp.Dispose();
                    adp = null;
                }
                if (cmd != null)
                {
                    cmd.Parameters.Clear();
                    cmd.Dispose();
                    cmd = null;
                }
                if (con != null)
                {
                    con.Dispose();
                    con = null;
                }


            }
            return dtICDs;
        }

        #region ICD 10 Import Functions
        public static DataTable GetICD10ImportData(DataTable tvpSelectedICD10Codes)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(clsDBSettings.CurrentDBInfo.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("ICD10_GetICD10UnimportedCodes", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@TVPICD10Codes", tvpSelectedICD10Codes));
                        cmd.CommandTimeout = 0;

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            using (DataTable dt = new System.Data.DataTable())
                            {
                                da.Fill(dt);
                                return dt;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clsICDAnalysis.UpdateLog("Exception while Getting ICD10 Import data :" + ex.ToString(), false);
            }

            return null;
        }

        public static void SaveICD10Codes(DataTable Codes)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(clsDBSettings.CurrentDBInfo.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("ICD10_SaveMasterCodes", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.CommandTimeout = 0;

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
            catch (Exception Ex) { clsICDAnalysis.UpdateLog("Exception while saving ICD 10 data :" + Ex.ToString(), false); }

        } 
        #endregion

        public static void UpdateLog( string Description,bool _ShowMessage = false)
        {
            System.IO.StreamWriter objFile = null;
            System.Text.StringBuilder strMessage = new System.Text.StringBuilder();
            try
            {
                Boolean flgApplicationErr = true;

               

                if (flgApplicationErr == true)
                {

                    //string _fileName = "ApplicationLog" + DateTime.Now.Date.ToString("MM-dd-yyyy") + ".log";
                    string _fileName = Environment.MachineName.ToString() + "-" + DateTime.Now.Date.ToString("yyyyMMdd") + ".log";

                    string LogPath = System.Windows.Forms.Application.StartupPath + "\\Log\\ApplicationLog";
                    if (System.IO.Directory.Exists(LogPath) == false)
                    {
                        System.IO.Directory.CreateDirectory(LogPath);

                    }
                    // FileStream fs = new FileStream(" ", FileMode.Append, Fi

                    objFile = new System.IO.StreamWriter(LogPath + "\\" + _fileName, true);
                    try
                    {
                        strMessage.Append(Environment.NewLine);
                        //strMessage.Append("-------------------------------------------------------------------------------------------------");
                        //strMessage.Append(Environment.NewLine);
                        strMessage.Append(System.DateTime.Now + ":" + System.DateTime.Now.Millisecond + ", ");
                       // strMessage.Append("Module: " + oActivityModule.ToString() + ", ");
                        //strMessage.Append("Category: " + oActivityCategory.ToString() + ", ");
                        //strMessage.Append("Type: " + oActivityType.ToString() + ", ");
                        strMessage.Append("Description: " + Description + ", ");
                        //strMessage.Append("OutCome: " + oActivityOutCome.ToString());
                        //strMessage.Append("-------------------------------------------------------------------------------------------------");
                        objFile.WriteLine(strMessage.ToString());

                        if (_ShowMessage == true)
                        {
                            MessageBox.Show(strMessage.ToString(), "gloICD10 Utility", MessageBoxButtons.OK, MessageBoxIcon.Error); 
                        }
                    }
                    catch (Exception)
                    {

                        //throw;
                    }
                }
            }
            catch (Exception)
            {

                // throw;
                //ex.ToString();
                //ex = null;
            }
            finally
            {
                if (objFile != null)
                {
                    objFile.Close();
                    objFile.Dispose();
                    objFile = null;
                }
                if (strMessage != null)
                {
                    strMessage.Remove(0, strMessage.Length);
                    strMessage = null;
                }
            }


            //CreateAuditLog(oActivityModule, oActivityCategory, oActivityType, Description, 0, 0, 0, oActivityOutCome);
        }

       

        public static DataTable GetMappings(DataTable tvpSelectedCodes)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(clsDBSettings.CurrentDBInfo.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("gsp_GetICD10Mapping", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@TVP_ICDCodes", tvpSelectedCodes));

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            using (DataTable dt = new System.Data.DataTable())
                            {
                                da.Fill(dt);
                                return dt;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clsICDAnalysis.UpdateLog("Exception while Getting ICD10 Mapping data :" + ex.ToString(), false);
            }

            return null;
        }

        
    }

}
