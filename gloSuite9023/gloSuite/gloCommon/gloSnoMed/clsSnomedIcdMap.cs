using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.SqlClient;
using System.Windows.Forms;
using System.IO;
using System.Data;
using System.Collections;


namespace gloSnoMed
{
    public class clsSnomedIcdMap
    {

        public DataTable Get_DefaultSnomedForICD(String ICD9Code, String ICD9Description,int nIcdRevision,string ConString)
        {
                      

            SqlConnection Conn = new SqlConnection();
            SqlDataAdapter da = new SqlDataAdapter();
            SqlCommand Cmd = null;
            DataTable dtSnomed=new DataTable ();
            SqlParameter objParam = default(SqlParameter);
            try
            {
                Conn.ConnectionString = ConString;
                Cmd = new System.Data.SqlClient.SqlCommand("gsp_GetDefaultSnomedForIcd", Conn);
                Cmd.CommandType = CommandType.StoredProcedure;

                //Changes done regarding Bug no 83499::gloEMR>NewExam>SmtDx> TimeOut Exception occuring.
                Cmd.CommandTimeout = 0;
                //
                objParam = Cmd.Parameters.Add("@Icd9Code", SqlDbType.NVarChar);
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = ICD9Code;

                objParam = Cmd.Parameters.Add("@IcdRevision", SqlDbType.SmallInt);
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = nIcdRevision;

                da = new SqlDataAdapter(Cmd);
                da.Fill(dtSnomed);
                Conn.Open();
                Conn.Close();
                return dtSnomed;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return dtSnomed;
            }
            finally
            {
                if (Conn.State != ConnectionState.Closed)
                {
                    Conn.Close();
                }
                if ((Cmd != null))
                {
                    Cmd.Parameters.Clear();  
                    Cmd.Dispose();
                    Cmd = null;
                }
                if ((Conn != null))
                {
                    Conn.Dispose();
                    Conn = null;
                }
                if ((objParam != null))
                { objParam = null; }
                
            }

        }


        public Boolean IsSnomedMandatory(string ConString)
        {
            SqlConnection Conn = new SqlConnection();
            SqlDataAdapter da = new SqlDataAdapter();
            SqlCommand Cmd = null;
            DataTable dtSnomed = new DataTable();

            bool IsSnomedMandatory = false;
            try
            {
                Conn.ConnectionString = ConString;
                Cmd = new SqlCommand("SELECT ISNULL(sSettingsValue,0) as IsSnomedCTMandatory FROM dbo.Settings WHERE sSettingsName ='REQUIRESNOMEDCT'", Conn);

                Conn.Open();
                da = new SqlDataAdapter(Cmd);
                da.Fill(dtSnomed);

                Conn.Close();
                if (dtSnomed != null)
                {
                    if (dtSnomed.Rows.Count > 0)
                    {
                        IsSnomedMandatory = Convert.ToBoolean(dtSnomed.Rows[0]["IsSnomedCTMandatory"]);
                    }
                }
                return IsSnomedMandatory;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return IsSnomedMandatory;
            }
            finally
            {
                if (Conn.State != ConnectionState.Closed)
                {
                    Conn.Close();
                }
                if ((Cmd != null))
                {
                    Cmd.Parameters.Clear();
                    Cmd.Dispose();
                    Cmd = null;
                }
                if ((Conn != null))
                {
                    Conn.Dispose();
                    Conn = null;
                }
            }
        }


        public String Get_SnomedDetailsForICD(string PatientId, string VisitId, string ICDId, string ConString)
        {
            SqlConnection Conn = new SqlConnection();
            SqlDataAdapter da = new SqlDataAdapter();
            SqlCommand Cmd = null;
            DataTable dtSnomed = new DataTable();
            SqlParameter objParam = default(SqlParameter);
            string SnomedDesc = "";
            try
            {
                Conn.ConnectionString = ConString;
                Cmd = new SqlCommand("Select top 1 sSnomedCode+'-'+sSnomedDesc as TermDescription from ExamICD9CPT where nPatientID=" + PatientId + " and sICD9Code='" + ICDId + "'", Conn);
                Conn.Open();
                da = new SqlDataAdapter(Cmd);
                da.Fill(dtSnomed);
                Conn.Close();
                if (dtSnomed != null)
                {
                    if (dtSnomed.Rows.Count > 0)
                    {
                        SnomedDesc = Convert.ToString(dtSnomed.Rows[0]["TermDescription"]);
                    }
                }
                return SnomedDesc;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return SnomedDesc;
            }
            finally
            {
                if (Conn.State != ConnectionState.Closed)
                {
                    Conn.Close();
                }
                if ((Cmd != null))
                {
                    Cmd.Parameters.Clear();
                    Cmd.Dispose();
                    Cmd = null;
                }
                if ((Conn != null))
                {
                    Conn.Dispose();
                    Conn = null;
                }
                if ((objParam != null))
                { objParam = null; }
                SnomedDesc = null;
            }
        }


        //public DataTable Get_PatientProblemList(long PatientID, string ConString)
        //{
        //    SqlConnection objCon = null;
        //    SqlCommand cmd = null;
        //    SqlDataAdapter da = null;
        //    DataTable dt = null;
        //    try
        //    {
        //        objCon = new SqlConnection();
        //        objCon.ConnectionString = ConString;
        //        cmd = new SqlCommand("gsp_GetPatientProblemList", objCon);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        SqlParameter ExamParam = default(SqlParameter);

        //        ExamParam = cmd.Parameters.Add("@PatientID", SqlDbType.BigInt);
        //        ExamParam.Direction = ParameterDirection.Input;
        //        ExamParam.Value = PatientID;


        //        objCon.Open();
        //        da = new SqlDataAdapter();
        //        da.SelectCommand = cmd;
        //        dt = new DataTable();
        //        da.Fill(dt);
        //        objCon.Close();
        //        return dt;

        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }
        //    finally
        //    {
        //        if ((dt != null))
        //        {
        //            dt.Dispose();
        //            dt = null;
        //        }
        //        if ((da != null))
        //        {
        //            da.Dispose();
        //            da = null;
        //        }
        //        if ((cmd != null))
        //        {
        //            cmd.Dispose();
        //            cmd = null;
        //        }
        //        if ((objCon != null))
        //        {
        //            objCon.Dispose();
        //            objCon = null;
        //        }
        //    }
        //}

        public String Get_SnomedDetails(string SnomedId, string ConString)
        {
            SqlConnection Conn = new SqlConnection();
            SqlDataAdapter da = new SqlDataAdapter();
            SqlCommand Cmd = null;
            DataTable dtSnomed = new DataTable();
            SqlParameter objParam = default(SqlParameter);
            string SnomedDesc = "";
            try
            {
                Conn.ConnectionString = ConString;
                Cmd = new SqlCommand("Select top 1 Term as Description from Term_Descriptions where CONCEPTID=" + SnomedId + "", Conn);
                Conn.Open();
                da = new SqlDataAdapter(Cmd);
                da.Fill(dtSnomed);
                Conn.Close();
                if (dtSnomed != null)
                {
                    if (dtSnomed.Rows.Count > 0)
                    {
                        SnomedDesc = Convert.ToString(dtSnomed.Rows[0]["Description"]);
                    }
                }
                return SnomedDesc;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return SnomedDesc;
            }
            finally
            {
                if (Conn.State != ConnectionState.Closed)
                {
                    Conn.Close();
                }
                if ((Cmd != null))
                {
                    Cmd.Parameters.Clear();
                    Cmd.Dispose();
                    Cmd = null;
                }
                if ((Conn != null))
                {
                    Conn.Dispose();
                    Conn = null;
                }
                if ((objParam != null))
                { objParam = null; }
                SnomedDesc = null;
            }
        }

        //public Hashtable FillIcdSnomedMapping(long nPatientId, string ConString)
        //{
        //    String[] arrIcd;
        //    DataTable dtProblemsIcd = null;
        //    dtProblemsIcd = Get_PatientProblemList(nPatientId, ConString);
        //    Hashtable htIcdSnomed = new Hashtable();
        //    try
        //    {
        //        for (int icd = 0; icd <= dtProblemsIcd.Rows.Count - 1; icd += 1)
        //        {
        //            arrIcd = Convert.ToString(dtProblemsIcd.Rows[icd]["Diagnosis"]).Split('-');
        //            if (arrIcd.Length > 1)
        //            {
        //                if (htIcdSnomed.Count > 0)
        //                {
        //                    if (htIcdSnomed.ContainsKey(arrIcd[0]))
        //                    {
        //                        if (Convert.ToString(htIcdSnomed[arrIcd[0]]) != Convert.ToString(dtProblemsIcd.Rows[icd]["sConceptId"]))
        //                            htIcdSnomed[arrIcd[0]] = Convert.ToString(dtProblemsIcd.Rows[icd]["sConceptId"]);
        //                    }
        //                    else
        //                        htIcdSnomed.Add(arrIcd[0], dtProblemsIcd.Rows[icd]["sConceptId"]);
        //                }
        //                else
        //                    htIcdSnomed.Add(arrIcd[0], dtProblemsIcd.Rows[icd]["sConceptId"]);
        //            }
        //        }
        //        return htIcdSnomed;
        //    }
        //    catch (Exception ex)
        //    {
        //        return htIcdSnomed;
        //        MessageBox.Show(ex.Message);
        //    }
        //    finally
        //    {
        //        if ((dtProblemsIcd != null))
        //        {
        //            dtProblemsIcd.Dispose();
        //            dtProblemsIcd = null;
        //        }
        //    }
        //}

    }
}
