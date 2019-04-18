using System;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;


namespace QSurvey.Serialization
{


    public class Serializer<T>
    {
        public string connectionString { get; set; }        

        public Serializer()
        {                    
            if (System.Configuration.ConfigurationManager.AppSettings["DBConnection"] != null)
            {
                connectionString = System.Configuration.ConfigurationManager.AppSettings["DBConnection"].ToString();
            }
        }

        public void Save(Int64 RequestID, object obj)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand comm = new SqlCommand("SurveyHoosHipInsert", conn))
                    {
                        comm.CommandType = CommandType.StoredProcedure;
                        comm.Parameters.Add(new SqlParameter("@nModelID", RequestID));
                        comm.Parameters.Add(new SqlParameter("@bModel", JsonConvert.SerializeObject(obj)));

                        comm.Connection.Open();
                        comm.ExecuteNonQuery();
                        comm.Connection.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex.ToString());
            }
        }

        private string GetModel(Int64 RequestID)
        {
            string sReturned = null;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand comm = new SqlCommand("SurveyHoosHipGet", conn))
                    {
                        comm.CommandType = CommandType.StoredProcedure;
                        comm.Parameters.AddWithValue("nModelID", RequestID);

                        comm.Connection.Open();
                        sReturned = Convert.ToString(comm.ExecuteScalar());
                        comm.Connection.Close();
                    }
                }
            }
            catch (Exception ex)
            {                
                Logger.Log(ex.ToString());
            }

            return sReturned;
        }

        public T GetModelReference(Int64 RequestID)
        {
            T returned = default(T);
            string sModel = null;

            try
            {
                sModel = this.GetModel(RequestID);

                if (sModel != null)
                {
                    returned = (T)JsonConvert.DeserializeObject<T>(sModel);
                    sModel = null;
                }                
            }
            catch (Exception ex)
            {
                Logger.Log(ex.ToString());
            }

            return returned;
        }
    }

    public class Logger
    {
        public static void Log(string content)
        {            
            if (System.Configuration.ConfigurationManager.AppSettings["LogFilePath"] != null)            
            {
                string sFilePath = "";
                sFilePath = System.Configuration.ConfigurationManager.AppSettings["DBConnection"].ToString();
                File.AppendAllText(sFilePath, Environment.NewLine + content);
            }            
        }
    }

}
