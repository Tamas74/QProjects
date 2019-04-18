using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace gloUIControlLibrary.Classes.ClinicalChartQueue
{
    public class clsClinicalChartDBLayer
    {
        public string ConnectionString { get; set; }

        public clsClinicalChartDBLayer(string ConnectionString)
        { this.ConnectionString = ConnectionString; }

        public DataSet LoadQueue(DateTime StartDate, DateTime EndDate)
        {
            DataSet ds = new System.Data.DataSet();

            try
            {
                using (SqlConnection cn = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("ClinicalChart_GetQueue", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.CommandTimeout = 0;

                        cmd.Parameters.Add(new SqlParameter("@dtFromDate", StartDate));
                        cmd.Parameters.Add(new SqlParameter("@dtToDate", EndDate));

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        { 
                            da.Fill(ds);
                            ds.Tables[0].TableName = "Master";
                           // ds.Tables[1].TableName = "Details";
                        }
                    }
                }

                return ds;
            }
            catch (Exception Ex)
            {
                gloUIControlLibrary.Classes.ICD10.LogException.ExceptionLog(Ex.ToString(), true);
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

        public DataTable GetPatientInsurancePlan(Int64 PatientID)
        {
            DataSet ds = new System.Data.DataSet();

            try
            {
                using (SqlConnection cn = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("ClinicalChartQueue_GetPatientInsurancePlan", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.CommandTimeout = 0;

                        cmd.Parameters.Add(new SqlParameter("@nPatientID", PatientID));
                        

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
                gloUIControlLibrary.Classes.ICD10.LogException.ExceptionLog(Ex.ToString(), true);
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
        public DataTable GetClaimPrinterList()
        {
            DataSet ds = new System.Data.DataSet();

            try
            {
                using (SqlConnection cn = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT bncs.sPrinterName AS [Printer Name] FROM dbo.BL_NewCMS1500PrintSettings bncs UNION SELECT bus.sPrinterName AS [Printer Name] FROM dbo.BL_UB04PrintSettings bus ORDER BY [Printer Name]", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.CommandTimeout = 0;

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(ds);
                        }
                    }
                }
                ds.Tables[0].Rows.InsertAt(ds.Tables[0].NewRow(), 0);
                return ds.Tables[0].Copy();
            }
            catch (Exception Ex)
            {
                gloUIControlLibrary.Classes.ICD10.LogException.ExceptionLog(Ex.ToString(), true);
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
        public DataTable GetQueueHistory(Int64 nQueueID)
        {
            DataSet ds = new System.Data.DataSet();

            try
            {
                using (SqlConnection cn = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("Clinicalchart_QueueHistory", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.CommandTimeout = 0;

                        cmd.Parameters.Add(new SqlParameter("@nQueueID", nQueueID));
                        if (cn != null)
                        {
                            if (cn.State != ConnectionState.Open)
                            {
                                cn.Open();
                            }
                        }

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
                gloUIControlLibrary.Classes.ICD10.LogException.ExceptionLog(Ex.ToString(), true);
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
        public void InsertNotes(Int64 QueueID, string Notes, Int64 AssociatedContactID, string UserName, Int64 UserID, enumNoteStatus NoteType, string ClaimNo = "")
        {            
            try
            {
                using (SqlConnection cn = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("ClinicalChartQueue_InsertNotes", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.CommandTimeout = 0;

                        cmd.Parameters.Add(new SqlParameter("@nQueueID", QueueID));
                        cmd.Parameters.Add(new SqlParameter("@sNote", Notes));
                        cmd.Parameters.Add(new SqlParameter("@nAssociatedContactID", AssociatedContactID));

                        cmd.Parameters.Add(new SqlParameter("@sClaimNo", ClaimNo));
                        cmd.Parameters.Add(new SqlParameter("@sUserName", UserName));
                        cmd.Parameters.Add(new SqlParameter("@nUserID", UserID));   
                     
                        cmd.Parameters.Add(new SqlParameter("@nNoteType", Convert.ToInt32(NoteType)));
                        
                        cmd.Connection.Open();
                        cmd.ExecuteNonQuery();
                        cmd.Connection.Close();
                    }
                }                
            }
            catch (Exception Ex)
            {
                gloUIControlLibrary.Classes.ICD10.LogException.ExceptionLog(Ex.ToString(), true);                
            }         
        }

        public bool UpdateQueueStatus(enumPrintStatus oStatus, long qId)
        {
            bool result = false;

            SqlCommand cmd = new SqlCommand();
            SqlConnection con;
            try
            {
                con = new SqlConnection(ConnectionString);
                cmd.CommandText = "Update ClinicalChartQueueMst set nQueueStatus=" + Convert.ToInt32(oStatus) + " where nQueueId=" + Convert.ToString(qId);
                cmd.Connection = con;
                con.Open();
                if (cmd.ExecuteNonQuery() > 0)
                {
                    result = true;
                }
                return result;
            }
            catch (Exception)
            {
            }

            return false;

        }

        public Boolean InsertQueueNotes(Int64 nQueueID, string sNotes, Int64 nAssociatedContactID, string sUserName, Int64 nUserID, int nNoteType, int nQueueStatus)
        {
            int result = 0;

            try
            {
                using (SqlConnection cn = new SqlConnection(ConnectionString))
                {
                    if (cn != null)
                    {
                        if (cn.State != ConnectionState.Open)
                        {
                            cn.Open();
                        }
                    }

                    using (SqlCommand cmd = new SqlCommand("ClinicalCharts_InsertQueueNotes", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.CommandTimeout = 0;

                        cmd.Parameters.Add(new SqlParameter("@nQueueID", nQueueID));
                        cmd.Parameters.Add(new SqlParameter("@sNotes", sNotes));
                        cmd.Parameters.Add(new SqlParameter("@nAssociatedContactID", nAssociatedContactID));
                        cmd.Parameters.Add(new SqlParameter("@sUserName", sUserName));
                        cmd.Parameters.Add(new SqlParameter("@sUserID", nUserID));
                        cmd.Parameters.Add(new SqlParameter("@nNoteType", nNoteType));
                        cmd.Parameters.Add(new SqlParameter("@nQueueStatus", nQueueStatus));
                        result = cmd.ExecuteNonQuery();
                        if (result > 0)
                        {
                            return true;
                        }

                    }
                }
                return false;

            }
            catch (Exception Ex)
            {
                gloUIControlLibrary.Classes.ICD10.LogException.ExceptionLog(Ex.ToString(), true);
                return false;
            }
            finally
            {

            }
        }
    }
}
