using System;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
using System.ComponentModel;

namespace gloPatientPortal
{
    public class clsHealthForm
    {
        SqlConnection Conn = null;
        SqlCommand Cmd;

        //Gender type enum for the question
        public enum GenderType
        {
            All = 0,
            Male = 1,
            Female = 2,
            Others = 3
        }

        //Question type enum for the answer
        public enum QuestionType
        {   
            [Description("Free text")]
            Textbox = 0,
            [Description("Free text (Long)")]
            LongTextbox = 1,
            [Description("Multiple options (Checkbox)")]
            Checkbox = 2,
            [Description("Multiple options (Radio button)")]
            Radio = 3,
            [Description("Multiple options (Dropdown)")]
            Dropdown = 4
        }

        public DataTable FillControls(string sqlconn,string sCategoryType)
        {
            DataSet ds = null;
            SqlDataAdapter adpt = null;
            try
            {
                adpt = new SqlDataAdapter();
                ds = new DataSet();
                Conn = new System.Data.SqlClient.SqlConnection(sqlconn);

                Cmd = new SqlCommand("gsp_FillCategory_Mst", Conn);
                SqlParameter objParam = default(SqlParameter);

                Cmd.CommandType = CommandType.StoredProcedure;
                adpt.SelectCommand = Cmd;

                objParam = Cmd.Parameters.Add("@CategoryType", SqlDbType.VarChar);
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = sCategoryType;

                objParam = Cmd.Parameters.Add("@flag", SqlDbType.Int);
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = 1;

                adpt.Fill(ds);
                Conn.Close();
                return ds.Tables[0];
            }
            catch (SqlException ex)
            {
                Conn.Close();
                return null;
            }
            catch (Exception ex)
            {
                Conn.Close();
                return null;
            }
            finally
            {
                if (Cmd != null)
                {
                    Cmd.Dispose();
                    Cmd = null;
                }
                if (adpt != null)
                {
                    adpt.Dispose();
                    adpt = null;
                }
                if (Conn != null)
                {
                    Conn.Dispose();
                    Conn = null;
                }
            }
        }


        public long AddPFLibrary(long nCategoryID, string sPublishName, string sCategoryType, string sPreText, string sPostText, long nAnswerType, long nUserId, string sqlconn, long LibId, bool IsModify, bool IsMandatory, int nGenderType, int nQuestionType, long nHistoryCategoryID, bool IsDataTable=false, DataTable _dt = null,bool bIsPatientHistoryrelated = false,int rbtType=0)
        {
            long Id = 0;
            try
            {
                Conn = new System.Data.SqlClient.SqlConnection(sqlconn);
                Cmd = new System.Data.SqlClient.SqlCommand("PF_PFLibraryInsertUpdate", Conn);
                Cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter objParam = default(SqlParameter);

                objParam = Cmd.Parameters.Add("@nCategoryID", SqlDbType.BigInt);
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = nCategoryID;

                objParam = Cmd.Parameters.Add("@sPublishName", SqlDbType.VarChar, 500);
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = sPublishName;

                objParam = Cmd.Parameters.Add("@sCategoryType", SqlDbType.VarChar, 100);
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = sCategoryType;

                objParam = Cmd.Parameters.Add("@sPreText", SqlDbType.VarChar);
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = sPreText;

                objParam = Cmd.Parameters.Add("@sPostText", SqlDbType.VarChar);
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = sPostText;

                objParam = Cmd.Parameters.Add("@IsMandatory", SqlDbType.Bit);
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = IsMandatory;

                objParam = Cmd.Parameters.Add("@nAnswerType", SqlDbType.BigInt);
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = nAnswerType;

                objParam = Cmd.Parameters.Add("@nUserId", SqlDbType.BigInt);
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = nUserId;

                objParam = Cmd.Parameters.Add("@nPFLibId", SqlDbType.BigInt);
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = LibId;

                objParam = Cmd.Parameters.Add("@IsModify", SqlDbType.Bit);
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = IsModify;

                objParam = Cmd.Parameters.Add("@nGenderType", SqlDbType.Int);
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = nGenderType;

                objParam = Cmd.Parameters.Add("@nQuestionType", SqlDbType.Int);
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = nQuestionType;

                objParam = Cmd.Parameters.Add("@nHistoryCategoryID", SqlDbType.BigInt);
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = nHistoryCategoryID;

                objParam = Cmd.Parameters.Add("@bIsDataTable", SqlDbType.Bit);
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = IsDataTable;

                objParam = Cmd.Parameters.Add("@TVPAnswer", SqlDbType.Structured);
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = _dt;

                objParam = Cmd.Parameters.Add("@bIsPatientHistoryrelated", SqlDbType.Bit);
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = bIsPatientHistoryrelated;

                objParam = Cmd.Parameters.Add("@nGroupType", SqlDbType.Int);
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = rbtType;

                Conn.Open();


                Id = Convert.ToInt64(Cmd.ExecuteScalar());

                Conn.Close();
            }
            catch (SqlException ex)
            {
                Conn.Close();

            }
            catch (Exception ex)
            {
                Conn.Close();
            }
            finally
            {
                if (Cmd != null)
                {
                    Cmd.Dispose();
                    Cmd = null;
                }
                if (Conn != null)
                {
                    Conn.Dispose();
                    Conn = null;
                }
            }
            return Id;
        }

        public long AddPFHealthForm(string HealthFormName, long nUserId, string sqlconn, bool IsModify, long nPFListId, bool IsActive, string sDownloadFormat, int DMScategoryID, bool IsEnableTaskNotification)
        {
            long Id = 0;
            try
            {
                Conn = new System.Data.SqlClient.SqlConnection(sqlconn);
                Cmd = new System.Data.SqlClient.SqlCommand("PF_HealthFormInsertUpdate", Conn);
                Cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter objParam = default(SqlParameter);

                objParam = Cmd.Parameters.Add("@sHealthFormName", SqlDbType.VarChar);
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = HealthFormName;

                objParam = Cmd.Parameters.Add("@nUserId", SqlDbType.BigInt);
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = nUserId;

                objParam = Cmd.Parameters.Add("@nPFListId", SqlDbType.BigInt);
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = nPFListId;

                objParam = Cmd.Parameters.Add("@IsModify", SqlDbType.Bit);
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = IsModify;

                objParam = Cmd.Parameters.Add("@IsActive", SqlDbType.Bit);
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = IsActive;

                objParam = Cmd.Parameters.Add("@sDownloadFormat", SqlDbType.VarChar);
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = sDownloadFormat;

                objParam = Cmd.Parameters.Add("@nDMSCategory", SqlDbType.Int);
                objParam.Direction = ParameterDirection.Input;
                if (DMScategoryID == 0)
                {
                    objParam.Value = null;
                }
                else
                {
                    objParam.Value = DMScategoryID;
                }
                //User Story #89427: In gloEMR Admin, a new tab in task mapping screen: Online Patient forms should be created same as “Review Portal Users”.
                //Added new parameter to sp 
                objParam = Cmd.Parameters.Add("@bIsEnableTaskNotification", SqlDbType.Bit);
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = IsEnableTaskNotification;

                Conn.Open();
                Id = Convert.ToInt64(Cmd.ExecuteScalar());
                Conn.Close();
            }
            catch (SqlException ex)
            {
                Conn.Close();
            }
            catch (Exception ex)
            {
                Conn.Close();
            }
            finally
            {
                if (Cmd != null)
                {
                    Cmd.Dispose();
                    Cmd = null;
                }
                if (Conn != null)
                {
                    Conn.Dispose();
                    Conn = null;
                }
            }
            return Id;
        }

        public DataTable GetGroups(string sqlconn)
        {
            DataSet ds = null;
            SqlDataAdapter adpt = null;
            try
            {
                adpt = new SqlDataAdapter();
                ds = new DataSet();
                Conn = new System.Data.SqlClient.SqlConnection(sqlconn);

                Cmd = new SqlCommand("PF_SelectGroups", Conn);

                Cmd.CommandType = CommandType.StoredProcedure;
                adpt.SelectCommand = Cmd;

                adpt.Fill(ds);
                Conn.Close();
                return ds.Tables[0];
            }
            catch (SqlException ex)
            {
                Conn.Close();
                return null;
            }
            catch (Exception ex)
            {
                Conn.Close();
                return null;
            }
            finally
            {
                if (Cmd != null)
                {
                    Cmd.Dispose();
                    Cmd = null;
                }
                if (adpt != null)
                {
                    adpt.Dispose();
                    adpt = null;
                }
                if (Conn != null)
                {
                    Conn.Dispose();
                    Conn = null;
                }
                //if (ds != null)
                //{
                //    ds.Dispose();
                //    ds = null;
                //}
            }
        }

        public DataTable FillHistoryItems(string sqlconn, long nCategoryId)
        {
            DataSet ds = null;
            SqlDataAdapter adpt = null;
            try
            {
                adpt = new SqlDataAdapter();
                ds = new DataSet();
                Conn = new System.Data.SqlClient.SqlConnection(sqlconn);

                Cmd = new SqlCommand("gsp_viewHistory_MST", Conn);
                SqlParameter objParam = default(SqlParameter);

                Cmd.CommandType = CommandType.StoredProcedure;
                adpt.SelectCommand = Cmd;

                objParam = Cmd.Parameters.Add("@nCategoryId", SqlDbType.BigInt);
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = nCategoryId;

                adpt.Fill(ds);
                Conn.Close();
                return ds.Tables[0];
            }
            catch (SqlException ex)
            {
                Conn.Close();
                return null;
            }
            catch (Exception ex)
            {
                Conn.Close();
                return null;
            }
            finally
            {
                if (Cmd != null)
                {
                    Cmd.Dispose();
                    Cmd = null;
                }
                if (adpt != null)
                {
                    adpt.Dispose();
                    adpt = null;
                }
                if (Conn != null)
                {
                    Conn.Dispose();
                    Conn = null;
                }
            }
        }

        public DataTable FillROSItems(string sqlconn, long nCategoryId)
        {
            DataSet ds = null;
            SqlDataAdapter adpt = null;
            try
            {
                adpt = new SqlDataAdapter();
                ds = new DataSet();
                Conn = new System.Data.SqlClient.SqlConnection(sqlconn);

                Cmd = new SqlCommand("gsp_ViewROS_Mst", Conn);
                SqlParameter objParam = default(SqlParameter);

                Cmd.CommandType = CommandType.StoredProcedure;
                adpt.SelectCommand = Cmd;

                objParam = Cmd.Parameters.Add("@nCategoryId", SqlDbType.BigInt);
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = nCategoryId;

                adpt.Fill(ds);
                Conn.Close();
                return ds.Tables[0];
            }
            catch (SqlException ex)
            {
                Conn.Close();
                return null;
            }
            catch (Exception ex)
            {
                Conn.Close();
                return null;
            }
            finally
            {
                if (Cmd != null)
                {
                    Cmd.Dispose();
                    Cmd = null;
                }
                if (adpt != null)
                {
                    adpt.Dispose();
                    adpt = null;
                }
                if (Conn != null)
                {
                    Conn.Dispose();
                    Conn = null;
                }
            }
        }

        public DataTable FillRelations(string sqlconn, long nMemberId)
        {
            DataSet ds = null;
            SqlDataAdapter adpt = null;
            try
            {
                adpt = new SqlDataAdapter();
                ds = new DataSet();
                Conn = new System.Data.SqlClient.SqlConnection(sqlconn);

                Cmd = new SqlCommand("gsp_viewFamilyMember_MST", Conn);
                SqlParameter objParam = default(SqlParameter);

                Cmd.CommandType = CommandType.StoredProcedure;
                adpt.SelectCommand = Cmd;

                objParam = Cmd.Parameters.Add("@nMemberID", SqlDbType.BigInt);
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = nMemberId;

                adpt.Fill(ds);
                Conn.Close();
                return ds.Tables[0];
            }
            catch (SqlException ex)
            {
                Conn.Close();
                return null;
            }
            catch (Exception ex)
            {
                Conn.Close();
                return null;
            }
            finally
            {
                if (Cmd != null)
                {
                    Cmd.Dispose();
                    Cmd = null;
                }
                if (adpt != null)
                {
                    adpt.Dispose();
                    adpt = null;
                }
                if (Conn != null)
                {
                    Conn.Dispose();
                    Conn = null;
                }
            }
        }

        public DataTable FillAllergies(string sqlconn)
        {
            DataSet ds = null;
            SqlDataAdapter adpt = null;
            try
            {
                adpt = new SqlDataAdapter();
                ds = new DataSet();
                Conn = new System.Data.SqlClient.SqlConnection(sqlconn);

                Cmd = new SqlCommand("gsp_FillCategory_Mst", Conn);
                SqlParameter objParam = default(SqlParameter);

                Cmd.CommandType = CommandType.StoredProcedure;
                adpt.SelectCommand = Cmd;

                objParam = Cmd.Parameters.Add("@CategoryType", SqlDbType.VarChar);
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = "Reaction";

                objParam = Cmd.Parameters.Add("@flag", SqlDbType.Int);
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = 1;

                adpt.Fill(ds);
                Conn.Close();
                return ds.Tables[0];
            }
            catch (SqlException ex)
            {
                Conn.Close();
                return null;
            }
            catch (Exception ex)
            {
                Conn.Close();
                return null;
            }
            finally
            {
                if (Cmd != null)
                {
                    Cmd.Dispose();
                    Cmd = null;
                }
                if (adpt != null)
                {
                    adpt.Dispose();
                    adpt = null;
                }
                if (Conn != null)
                {
                    Conn.Dispose();
                    Conn = null;
                }
            }
        }

        public DataTable GetQuestions(string sqlconn)
        {
            DataSet ds = null;
            SqlDataAdapter adpt = null;
            try
            {
                adpt = new SqlDataAdapter();
                ds = new DataSet();
                Conn = new System.Data.SqlClient.SqlConnection(sqlconn);

                Cmd = new SqlCommand("PF_SelectQuestion", Conn);

                Cmd.CommandType = CommandType.StoredProcedure;


                SqlParameter objParam = default(SqlParameter);

                objParam = Cmd.Parameters.Add("@sCategoryType", SqlDbType.VarChar);
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = "Q";

                adpt.SelectCommand = Cmd;

                adpt.Fill(ds);
                Conn.Close();
                return ds.Tables[0];
            }
            catch (SqlException ex)
            {
                Conn.Close();
                return null;
            }
            catch (Exception ex)
            {
                Conn.Close();
                return null;
            }
            finally
            {
                if (Cmd != null)
                {
                    Cmd.Dispose();
                    Cmd = null;
                }
                if (adpt != null)
                {
                    adpt.Dispose();
                    adpt = null;
                }
                if (Conn != null)
                {
                    Conn.Dispose();
                    Conn = null;
                }
            }
        }

        public long InsertAnswers(DataTable DtData, string sqlconn, string SPName)
        {
            long lngId = 0;
            //DataTable dt = new DataTable();
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(sqlconn);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();

            try
            {
                oDBParameters.Add("@TVP", DtData, ParameterDirection.Input, SqlDbType.Structured);
                oDB.Connect(false);
                Object obj = oDB.Execute(SPName, oDBParameters);
                oDB.Disconnect();

                if (Convert.ToInt64(obj) > 0)
                {
                    lngId = Convert.ToInt64(obj);
                }
                else if (Convert.ToInt64(obj) == -1)
                {
                    lngId = -1;
                }
                else
                {
                    lngId = 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Dispose();
                    oDB = null;
                }

                if (oDBParameters != null)
                {
                    oDBParameters.Dispose();
                    oDBParameters = null;
                }

            }
            return lngId;
        }

        public DataTable GetAnswers(string sqlconn,long LibId)
        {
            DataSet ds = null;
            SqlDataAdapter adpt = null;
            try
            {
                adpt = new SqlDataAdapter();
                ds = new DataSet();
                Conn = new System.Data.SqlClient.SqlConnection(sqlconn);

                Cmd = new SqlCommand("PF_SelectAnswers", Conn);
                SqlParameter objParam = default(SqlParameter);

                Cmd.CommandType = CommandType.StoredProcedure;
                adpt.SelectCommand = Cmd;

                objParam = Cmd.Parameters.Add("@nPFLibId", SqlDbType.BigInt);
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = LibId;

                adpt.Fill(ds);
                Conn.Close();
                return ds.Tables[0];
            }
            catch (SqlException ex)
            {
                Conn.Close();
                return null;
            }
            catch (Exception ex)
            {
                Conn.Close();
                return null;
            }
            finally
            {
                if (Cmd != null)
                {
                    Cmd.Dispose();
                    Cmd = null;
                }
                if (adpt != null)
                {
                    adpt.Dispose();
                    adpt = null;
                }
                if (Conn != null)
                {
                    Conn.Dispose();
                    Conn = null;
                }
            }
        }

        public DataTable GetHealthForms(string sqlconn, long nPFListId)
        {
            DataSet ds = null;
            SqlDataAdapter adpt = null;
            try
            {
                adpt = new SqlDataAdapter();
                ds = new DataSet();
                Conn = new System.Data.SqlClient.SqlConnection(sqlconn);
                Cmd = new SqlCommand("PF_SelectHealthForms", Conn);
                Cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter objParam = default(SqlParameter);
                objParam = Cmd.Parameters.Add("@nPFListId", SqlDbType.BigInt);
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = nPFListId;

                adpt.SelectCommand = Cmd;
                adpt.Fill(ds);

                Conn.Close();
            }
            catch (SqlException ex)
            {
                Conn.Close();
                return null;
            }
            catch (Exception ex)
            {
                Conn.Close();
                return null;
            }
            finally
            {
                if (Cmd != null)
                {
                    Cmd.Dispose();
                    Cmd = null;
                }
                if (adpt != null)
                {
                    adpt.Dispose();
                    adpt = null;
                }
                if (Conn != null)
                {
                    Conn.Dispose();
                    Conn = null;
                }
            }

            return ds.Tables[0];
        }

        public DataTable GetHealthFormDetails(string sqlconn, long nPFListId)
        {
            DataSet ds = null;
            SqlDataAdapter adpt = null;
            try
            {
                adpt = new SqlDataAdapter();
                ds = new DataSet();
                Conn = new System.Data.SqlClient.SqlConnection(sqlconn);
                Cmd = new SqlCommand("PF_SelectHealthFormsDetail", Conn);
                Cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter objParam = default(SqlParameter);
                objParam = Cmd.Parameters.Add("@nPFListId", SqlDbType.BigInt);
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = nPFListId;

                adpt.SelectCommand = Cmd;
                adpt.Fill(ds);

                Conn.Close();
            }
            catch (SqlException ex)
            {
                Conn.Close();
                return null;
            }
            catch (Exception ex)
            {
                Conn.Close();
                return null;
            }
            finally
            {
                if (Cmd != null)
                {
                    Cmd.Dispose();
                    Cmd = null;
                }
                if (adpt != null)
                {
                    adpt.Dispose();
                    adpt = null;
                }
                if (Conn != null)
                {
                    Conn.Dispose();
                    Conn = null;
                }
            }

            return ds.Tables[0];
        }

        public long InsertAssociation(DataTable DtData, string sqlconn, string SPName, long nUserId, long nPFListId, Boolean IsModify)
        {
            long lngId = 0;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(sqlconn);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            try
            {
                oDBParameters.Add("@nUserId", nUserId, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@nPFListId", nPFListId, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@TVP", DtData, ParameterDirection.Input, SqlDbType.Structured);
                oDBParameters.Add("@IsModify", IsModify, ParameterDirection.Input, SqlDbType.Bit);
                oDB.Connect(false);
                Object obj = oDB.Execute(SPName, oDBParameters);
                oDB.Disconnect();

                if (Convert.ToInt64(obj) > 0)
                {
                    lngId = Convert.ToInt64(obj);
                }
                else if (Convert.ToInt64(obj) == -1)
                {
                    lngId = -1;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Dispose();
                    oDB = null;
                }
                if (oDBParameters != null)
                {
                    oDBParameters.Dispose();
                    oDBParameters = null;
                }

            }
            return lngId;
        }

        public long DeleteQuestion(string sqlconn, string SPName, long nPFLibId,string sCategoryType)
        {
            long lngId = 0;
            DataTable dt = new DataTable();
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(sqlconn);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            Conn = new System.Data.SqlClient.SqlConnection(sqlconn);

            Cmd = new SqlCommand(SPName, Conn);

            Cmd.CommandType = CommandType.StoredProcedure;

            try
            {

                oDBParameters.Add("@nPFLibId", nPFLibId, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@sCategoryType", sCategoryType, ParameterDirection.Input, SqlDbType.VarChar);
                oDB.Connect(false);
                Object obj = oDB.Execute(SPName, oDBParameters);
                oDB.Disconnect();

                if (Convert.ToInt64(obj) > 0)
                {
                    lngId = Convert.ToInt64(obj);
                }
                else if (Convert.ToInt64(obj) == -1)
                {
                    lngId = -1;
                }
                else
                {
                    lngId = 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Dispose();
                    oDB = null;
                }

                if (oDBParameters != null)
                {
                    oDBParameters.Dispose();
                    oDBParameters = null;
                }

            }
            return lngId;
        }

        public Int32 CheckAssociatedQuesAns(string sqlconn, string SPName, long nPFLibId)
        {
            Int32 lngId = 0;
            DataTable dt = new DataTable();
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(sqlconn);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            Conn = new System.Data.SqlClient.SqlConnection(sqlconn);

            Cmd = new SqlCommand(SPName, Conn);

            Cmd.CommandType = CommandType.StoredProcedure;

            try
            {

                oDBParameters.Add("@nPFLibId", nPFLibId, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Connect(false);
                Object obj = oDB.ExecuteScalar(SPName, oDBParameters);
                oDB.Disconnect();

                if (Convert.ToInt32(obj) > 0)
                {
                    lngId = Convert.ToInt32(obj);
                }
               
                else
                {
                    lngId = 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Dispose();
                    oDB = null;
                }

                if (oDBParameters != null)
                {
                    oDBParameters.Dispose();
                    oDBParameters = null;
                }

            }
            return lngId;
        }

        public DataTable GetQuestionAssociatedForm(string sqlconn, string SPName, long nPFLibId, bool bIsGetPFList)
        {
            
            DataTable dt = new DataTable();
            SqlDataAdapter adpt = null;

            try
            {
                adpt = new SqlDataAdapter();
                Conn = new System.Data.SqlClient.SqlConnection(sqlconn);

                Cmd = new SqlCommand(SPName, Conn);
                SqlParameter objParam = default(SqlParameter);

                Cmd.CommandType = CommandType.StoredProcedure;
                adpt.SelectCommand = Cmd;

                objParam = Cmd.Parameters.Add("@nPFLibId", SqlDbType.BigInt);
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = nPFLibId;

                objParam = Cmd.Parameters.Add("@bIsGetPFList", SqlDbType.Bit);
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = bIsGetPFList;

                Conn.Open();

                adpt.Fill(dt);
                Conn.Close();
                //return dt;
            }
            catch (SqlException ex)
            {
                Conn.Close();
                return null;
            }
            catch (Exception ex)
            {
                Conn.Close();
                return null;
            }
            finally
            {
                if (Cmd != null)
                {
                    Cmd.Dispose();
                    Cmd = null;
                }
                if (adpt != null)
                {
                    adpt.Dispose();
                    adpt = null;
                }
                if (Conn != null)
                {
                    Conn.Dispose();
                    Conn = null;
                }
            }
            return dt;
        }
        public string IsValidPatientForm(string sqlconn, string SPName, long nMstId,string sType)
        {
            Int32 lngId = 0;
            DataTable dt = new DataTable();
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(sqlconn);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            Conn = new System.Data.SqlClient.SqlConnection(sqlconn);

            Cmd = new SqlCommand(SPName, Conn);

            Cmd.CommandType = CommandType.StoredProcedure;

            try
            {

                oDBParameters.Add("@nMstId", nMstId, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@sType", sType, ParameterDirection.Input, SqlDbType.VarChar);
                oDB.Connect(false);
                Object obj = oDB.ExecuteScalar(SPName, oDBParameters);
                oDB.Disconnect();

                if ( obj.ToString()!="")
                {
                    return obj.ToString();
                }

                else
                {
                    return "";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Dispose();
                    oDB = null;
                }

                if (oDBParameters != null)
                {
                    oDBParameters.Dispose();
                    oDBParameters = null;
                }

            }
            return "";
        }

        public long DeleteAnswer(string sqlconn, string SPName, long nPFAnswerId, long nPFLibId)
        {
            long lngId = 0;
            DataTable dt = new DataTable();
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(sqlconn);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            Conn = new System.Data.SqlClient.SqlConnection(sqlconn);

            Cmd = new SqlCommand(SPName, Conn);

            Cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                oDBParameters.Add("@nPFAnswerId", nPFAnswerId, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@nPFLibId", nPFLibId, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Connect(false);
                Object obj = oDB.Execute(SPName, oDBParameters);
                oDB.Disconnect();

                if (Convert.ToInt64(obj) > 0)
                {
                    lngId = Convert.ToInt64(obj);
                }
                else if (Convert.ToInt64(obj) == -1)
                {
                    lngId = -1;
                }
                else
                {
                    lngId = 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Dispose();
                    oDB = null;
                }

                if (oDBParameters != null)
                {
                    oDBParameters.Dispose();
                    oDBParameters = null;
                }

            }
            return lngId;
        }

        public DataSet getHFDetailsforDiv(string sqlconn, long nPFListId)
        {
            DataSet ds = null;
            SqlDataAdapter adpt = null;

            try
            {
                adpt = new SqlDataAdapter();
                ds = new DataSet();
                Conn = new System.Data.SqlClient.SqlConnection(sqlconn);

                Cmd = new SqlCommand("PF_getHFDetailsforDiv", Conn);
                SqlParameter objParam = default(SqlParameter);

                Cmd.CommandType = CommandType.StoredProcedure;
                adpt.SelectCommand = Cmd;

                objParam = Cmd.Parameters.Add("@nPFListId", SqlDbType.BigInt);
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = nPFListId;

                Conn.Open();

                adpt.Fill(ds);
                Conn.Close();
                return ds;
            }
            catch (SqlException ex)
            {
                Conn.Close();
                return null;
            }
            catch (Exception ex)
            {
                Conn.Close();
                return null;
            }
            finally
            {
                if (Cmd != null)
                {
                    Cmd.Dispose();
                    Cmd = null;
                }
                if (adpt != null)
                {
                    adpt.Dispose();
                    adpt = null;
                }
                if (Conn != null)
                {
                    Conn.Dispose();
                    Conn = null;
                }
            }
        }

        public string getName(string sName)
        {
            sName.Replace(' ', '_');
            return sName;
        }

        public long UpdateStatus(string sqlconn, string SPName, long nPFLibId, bool bIsActive)
        {
            long lngId = 0;
            DataTable dt = new DataTable();
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(sqlconn);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            Conn = new System.Data.SqlClient.SqlConnection(sqlconn);

            Cmd = new SqlCommand(SPName, Conn);

            Cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                oDBParameters.Add("@nPFLibId", nPFLibId, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@bIsActive", bIsActive, ParameterDirection.Input, SqlDbType.Bit);
                oDB.Connect(false);
                Object obj = oDB.ExecuteScalar(SPName, oDBParameters);
                oDB.Disconnect();

                if (Convert.ToInt64(obj) > 0)
                {
                    lngId = Convert.ToInt64(obj);
                }
                else if (Convert.ToInt64(obj) == -1)
                {
                    lngId = -1;
                }
                else
                {
                    lngId = 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Dispose();
                    oDB = null;
                }

                if (oDBParameters != null)
                {
                    oDBParameters.Dispose();
                    oDBParameters = null;
                }

            }
            return lngId;
        }

        public long PublishForm(long nPFListId, string sHTMLContent,string sqlconn, string SPName)
        {
            long lngId = 0;
            DataTable dt = new DataTable();
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(sqlconn);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            Conn = new System.Data.SqlClient.SqlConnection(sqlconn);

            Cmd = new SqlCommand(SPName, Conn);

            Cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                oDBParameters.Add("@nPFListId", nPFListId, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@sHTMLContent", sHTMLContent, ParameterDirection.Input, SqlDbType.NVarChar);
                oDB.Connect(false);
                Object obj = oDB.ExecuteScalar(SPName, oDBParameters);
                oDB.Disconnect();

                if (Convert.ToInt64(obj) > 0)
                {
                    lngId = Convert.ToInt64(obj);
                }
                else if (Convert.ToInt64(obj) == -1)
                {
                    lngId = -1;
                }
                else
                {
                    lngId = 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Dispose();
                    oDB = null;
                }

                if (oDBParameters != null)
                {
                    oDBParameters.Dispose();
                    oDBParameters = null;
                }

            }
            return lngId;
        }

        public DataTable GetHistoryType(string sqlconn, long nCategoryId)
        {
            DataTable dt = null;
            SqlDataAdapter adpt = null;
            try
            {
                adpt = new SqlDataAdapter();
                dt = new DataTable();
                Conn = new System.Data.SqlClient.SqlConnection(sqlconn);

                Cmd = new SqlCommand("WS_GetHistoryType", Conn);
                SqlParameter objParam = default(SqlParameter);

                Cmd.CommandType = CommandType.StoredProcedure;
                adpt.SelectCommand = Cmd;

                objParam = Cmd.Parameters.Add("@nCategoryId", SqlDbType.BigInt);
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = nCategoryId;

                
                adpt.Fill(dt);
                Conn.Close();
                return dt;
            }
            catch (SqlException ex)
            {
                Conn.Close();
                return null;
            }
            catch (Exception ex)
            {
                Conn.Close();
                return null;
            }
            finally
            {
                if (Cmd != null)
                {
                    Cmd.Dispose();
                    Cmd = null;
                }
                if (adpt != null)
                {
                    adpt.Dispose();
                    adpt = null;
                }
                if (Conn != null)
                {
                    Conn.Dispose();
                    Conn = null;
                }
            }
        }

        public DataTable GetDMScategorytoUpdate(string sqlconn, long nCategoryId)
        {
            DataTable dt = null;
            SqlDataAdapter adpt = null;
            try
            {
                adpt = new SqlDataAdapter();
                dt = new DataTable();
                Conn = new System.Data.SqlClient.SqlConnection(sqlconn);

                Cmd = new SqlCommand("GetDMSCategoryNameByID", Conn);
                SqlParameter objParam = default(SqlParameter);

                Cmd.CommandType = CommandType.StoredProcedure;
                adpt.SelectCommand = Cmd;

                objParam = Cmd.Parameters.Add("@nCategoryID", SqlDbType.Int);
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = nCategoryId;

                adpt.Fill(dt);
                Conn.Close();
                return dt;
            }
            catch (SqlException ex)
            {
                Conn.Close();
                return null;
            }
            catch (Exception ex)
            {
                Conn.Close();
                return null;
            }
            finally
            {
                if (Cmd != null)
                {
                    Cmd.Dispose();
                    Cmd = null;
                }
                if (adpt != null)
                {
                    adpt.Dispose();
                    adpt = null;
                }
                if (Conn != null)
                {
                    Conn.Dispose();
                    Conn = null;
                }
            }
        }

        public DataTable GetDMSCategory(string sqlconn,bool addSelectindata = false)
        {
            DataSet ds = null;
            SqlDataAdapter adpt = null;
            try
            {
                adpt = new SqlDataAdapter();
                ds = new DataSet();
                Conn = new System.Data.SqlClient.SqlConnection(sqlconn);
                string strqry = "";
                if (addSelectindata == true)
                { 
                strqry = "SELECT 0 AS CategoryId , 'Select DMS Category' AS CategoryName,0 AS ClinicID UNION ";
                }
                strqry += "select  CategoryId, CategoryName, isnull(ClinicID,0) as ClinicID from eDocument_Category_V3 where isnull(CategoryName,'') <> '' order by CategoryId";
                Cmd = new SqlCommand(strqry, Conn);
                Cmd.CommandType = CommandType.Text;

                adpt.SelectCommand = Cmd;

                adpt.Fill(ds);
                Conn.Close();
                return ds.Tables[0];
            }
            catch (SqlException ex)
            {
                Conn.Close();
                return null;
            }
            catch (Exception ex)
            {
                Conn.Close();
                return null;
            }
            finally
            {
                if (Cmd != null)
                {
                    Cmd.Dispose();
                    Cmd = null;
                }
                if (adpt != null)
                {
                    adpt.Dispose();
                    adpt = null;
                }
                if (Conn != null)
                {
                    Conn.Dispose();
                    Conn = null;
                }
            }
        }


        public int UpdateDMScategoryforPFList(string sqlconn, Int64 npfListID, Int16 nDMSCategoryID)
        {
            SqlConnection sqlConnection = null;
            SqlCommand sqlCommand = null;
            SqlParameter oParameter = null;

            try
            {
                sqlConnection = new SqlConnection(sqlconn);
                sqlConnection.Open();
                sqlCommand = new SqlCommand();
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.CommandText = "PF_HealthFormInsertUpdate";
                sqlCommand.Connection = sqlConnection;


                oParameter = new SqlParameter();
                oParameter.ParameterName = "@sHealthFormName";
                oParameter.Direction = ParameterDirection.Input;
                oParameter.SqlDbType = SqlDbType.VarChar;
                oParameter.Value = "";
                sqlCommand.Parameters.Add(oParameter);

                oParameter = new SqlParameter();
                oParameter.ParameterName = "@nUserId";
                oParameter.Direction = ParameterDirection.Input;
                oParameter.SqlDbType = SqlDbType.Decimal;
                oParameter.Value = 0;
                sqlCommand.Parameters.Add(oParameter);

                oParameter = new SqlParameter();
                oParameter.ParameterName = "@nPFListId";
                oParameter.Direction = ParameterDirection.Input;
                oParameter.SqlDbType = SqlDbType.Decimal;
                oParameter.Value = npfListID;
                sqlCommand.Parameters.Add(oParameter);

                oParameter = new SqlParameter();
                oParameter.ParameterName = "@IsModify";
                oParameter.Direction = ParameterDirection.Input;
                oParameter.SqlDbType = SqlDbType.Bit;
                oParameter.Value = 1;
                sqlCommand.Parameters.Add(oParameter);

                oParameter = new SqlParameter();
                oParameter.ParameterName = "@IsActive";
                oParameter.Direction = ParameterDirection.Input;
                oParameter.SqlDbType = SqlDbType.Bit;
                oParameter.Value = 0;
                sqlCommand.Parameters.Add(oParameter);

                oParameter = new SqlParameter();
                oParameter.ParameterName = "@sDownloadFormat";
                oParameter.Direction = ParameterDirection.Input;
                oParameter.SqlDbType = SqlDbType.VarChar;
                oParameter.Value = "";
                sqlCommand.Parameters.Add(oParameter);

                oParameter = new SqlParameter();
                oParameter.ParameterName = "@nDMSCategory";
                oParameter.Direction = ParameterDirection.Input;
                oParameter.SqlDbType = SqlDbType.Int;
                oParameter.Value = nDMSCategoryID;
                sqlCommand.Parameters.Add(oParameter);

                oParameter = new SqlParameter();
                oParameter.ParameterName = "@IsUpdateDMSCategory";
                oParameter.Direction = ParameterDirection.Input;
                oParameter.SqlDbType = SqlDbType.Decimal;
                oParameter.Value = 1;
                sqlCommand.Parameters.Add(oParameter);

                int result = sqlCommand.ExecuteNonQuery();

                return result;

            }
            catch (Exception ex)
            {
                return 0;
                throw ex;

            }
            finally
            {
                if (sqlCommand != null)
                { sqlCommand.Dispose(); }
                if (sqlConnection.State == ConnectionState.Open)
                { sqlConnection.Close(); }
                if (sqlConnection != null)
                { sqlConnection.Dispose(); }
                oParameter = null;

            }
        }
       
    }

    public static class clsGeneral
    {
         public static Boolean IsHtmlCharcter(char chr)
        {
            if (chr == '<' || chr == '>')
            {
                MessageBox.Show("\'<\' and \'>\' characters are not allowed.", "gloEMRAdmin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }
            return false;
        }

         public static Boolean ContainsHtml(string str, string strControl)
         {
             if (str.Contains("<") || str.Contains(">"))
             {
                 MessageBox.Show("\'<\' and \'>\' characters are not allowed in " + strControl + ".", "gloEMRAdmin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                 return false;
             }
             return true;
         }
    }
}
