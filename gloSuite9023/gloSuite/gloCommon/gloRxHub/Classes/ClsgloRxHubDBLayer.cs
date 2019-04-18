using System;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace gloRxHub
{
    public class clsgloRxHubDBLayer : IDisposable
    {
        private string _ErrorMessage;
        private System.Data.SqlClient.SqlConnection gloDBConnection = new System.Data.SqlClient.SqlConnection();
        public ClsgloRxHubGeneral ogloRxHubGenral;
        public DateTime dtLastfillDate;

        public bool InsertEDIResponse271_Details(string storedProcedureName, ClsRxH_271Master objClsRxH_271Master)
        {
            SqlParameter oSqlParameter;

            SqlCommand oSQLCommand = new SqlCommand();
            try
            {
                bool _blnResult = false;
                int _RecordAffected = 0;

                //Check Connection 
                if (gloDBConnection.State == ConnectionState.Closed)
                {
                    _ErrorMessage = "Please check database connection";
                    return false;
                }

                //save 270 ISA_ControlID i.e obj.messageid
                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@nPatientID";
                oSqlParameter.SqlDbType = SqlDbType.BigInt;
                oSqlParameter.Direction = ParameterDirection.Input;
                oSqlParameter.Value = objClsRxH_271Master.PatientId;
                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;

                //save 270 ISA_ControlID i.e obj.messageid
                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sMessageID";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                oSqlParameter.Value = objClsRxH_271Master.ISA_ControlNumber;
                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;

                //save 270 BHT_TransactionID i.e obj.Tr
                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sReference270MessageID";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                oSqlParameter.Value = objClsRxH_271Master.MessageID;
                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;

                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sMessageType";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                oSqlParameter.Value = objClsRxH_271Master.MessageType;
                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;


                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@dt270ResponseDateTimeStamp";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                oSqlParameter.Value = DateTime.Now;

                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;


                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sPBM_PayerParticipantID";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                oSqlParameter.Value = objClsRxH_271Master.PayerParticipantId;

                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;


                //Work with database 
                oSQLCommand.CommandType = CommandType.StoredProcedure;
                // SQL Command Type , is Store Procedure 
                oSQLCommand.CommandText = storedProcedureName;
                // Set Store Procedure Name 
                oSQLCommand.Connection = gloDBConnection;

                _RecordAffected = oSQLCommand.ExecuteNonQuery();

                if (_RecordAffected > 0)
                {
                    _blnResult = true;
                }

                return _blnResult;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                return false;
            }
            finally
            {
                //SLR: Finaly oSQLCommand.Parameters.clear, free oSqlCommand 
                if (oSQLCommand != null)
                {
                    oSQLCommand.Parameters.Clear();
                    oSQLCommand.Dispose();
                    oSQLCommand = null;
                }                                
            }
        }

        public DataTable GetFormularlyCheckInfo(string sqlQuery)
        {
            // Data Reader 
            DataSet oDS = new DataSet();
            //SqlParameter OsqlParmeter = default(SqlParameter);
            //int i = 0;

            try
            {
                //Check Connection 
                if (gloDBConnection.State == ConnectionState.Closed)
                {
                    _ErrorMessage = "Please check database connecion";
                    return null;
                }

                //Work with database 
                SqlDataAdapter oDataAdapter = new SqlDataAdapter(sqlQuery, gloDBConnection);
                oDataAdapter.Fill(oDS);
                oDataAdapter.Dispose();
                oDataAdapter = null;
                DataTable dt = null;
                if (oDS.Tables.Count > 0)
                {
                    dt = oDS.Tables[0].Copy();
                }
                return dt;
            }
            catch (Exception objError)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, objError.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                _ErrorMessage = objError.Message;
                return null;
            }
            finally
            {
                oDS.Dispose();
                oDS = null;
            }

        }

        public bool InsertEDIResponse271_SubscriberDetails(string storedProcedureName, ClsRxH_271Master objClsRxH_271Master)
        {
            SqlParameter oSqlParameter;
            SqlCommand oSQLCommand = null;
            try
            {

                oSQLCommand = new SqlCommand();
                bool _blnResult = false;
                int _RecordAffected = 0;
                //int i = 0;



                //Check Connection 
                if (gloDBConnection.State == ConnectionState.Closed)
                {
                    _ErrorMessage = "Please check database connection";
                    return false;
                }

                //save 270 ISA_ControlID i.e obj.messageid
                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sMessageID";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                oSqlParameter.Value = objClsRxH_271Master.MessageID;
                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;

                //save 270 BHT_TransactionID i.e obj.Tr
                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sSubscriberFirstName";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                oSqlParameter.Value = objClsRxH_271Master.RxH_271Details[0].SubscriberFirstName;
                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;

                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sSubscriberMiddleName";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                oSqlParameter.Value = objClsRxH_271Master.RxH_271Details[0].SubscriberMiddleName;
                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;


                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sSubscriberLastName";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                oSqlParameter.Value = objClsRxH_271Master.RxH_271Details[0].SubscriberLastName;
                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;

                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sSubscriberSuffix";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                oSqlParameter.Value = objClsRxH_271Master.RxH_271Details[0].SubscriberSuffix;
                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;


                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sSubscriberGender";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                oSqlParameter.Value = objClsRxH_271Master.RxH_271Details[0].SubscriberGender;
                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;


                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sSubscriberDOB";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                oSqlParameter.Value = objClsRxH_271Master.RxH_271Details[0].SubscriberDOB;
                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;


                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sSubscriberSSN";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                oSqlParameter.Value = objClsRxH_271Master.RxH_271Details[0].SubscriberSSN;
                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;

                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sSubscriberAddress1";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                oSqlParameter.Value = objClsRxH_271Master.RxH_271Details[0].SubscriberAddress1;
                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;

                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sSubscriberAddress2";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                oSqlParameter.Value = objClsRxH_271Master.RxH_271Details[0].SubscriberAddress2;
                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;


                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sSubscriberCity";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                oSqlParameter.Value = objClsRxH_271Master.RxH_271Details[0].SubscriberCity;
                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;

                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sSubscriberState";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                oSqlParameter.Value = objClsRxH_271Master.RxH_271Details[0].SubscriberState;
                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;


                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sSubscriberZip";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                oSqlParameter.Value = objClsRxH_271Master.RxH_271Details[0].SubscriberZip;

                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;

                //Subscriber Changed Information 



                //Subscriber Changed Flag
                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@IsSubscriberdemoChange";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                oSqlParameter.Value = objClsRxH_271Master.RxH_271Details[0].IsSubscriberdemoChange;

                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;

                //Subscriber Changed Last Name
                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sSubscriberDemoChgLastName";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                oSqlParameter.Value = objClsRxH_271Master.RxH_271Details[0].SubscriberDemoChgLastName;

                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;

                //Subscriber Changed Last Name
                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sSubscriberDemochgFirstName";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                oSqlParameter.Value = objClsRxH_271Master.RxH_271Details[0].SubscriberDemochgFirstName;

                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;

                //Subscriber Changed middle Name
                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sSubscriberDemoChgMiddleName";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                oSqlParameter.Value = objClsRxH_271Master.RxH_271Details[0].SubscriberDemoChgMiddleName;

                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;

                //Subscriber Changed Zip
                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sSubscriberDemoChgZip";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                oSqlParameter.Value = objClsRxH_271Master.RxH_271Details[0].SubscriberDemoChgZip;

                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;



                //Subscriber Changed DOB
                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sSubscriberDemoChgDOB";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                oSqlParameter.Value = objClsRxH_271Master.RxH_271Details[0].SubscriberDemoChgDOB;

                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;

                //Subscriber Changed Gender
                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sSubscriberDemoChgGender";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                oSqlParameter.Value = objClsRxH_271Master.RxH_271Details[0].SubscriberDemoChgGender;

                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;

                //Subscriber Changed SSN
                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sSubscriberDemoChgSSN";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                oSqlParameter.Value = objClsRxH_271Master.RxH_271Details[0].SubscriberDemoChgSSN;

                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;

                //Subscriber Changed Address1
                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sSubscriberDemoChgAddress1";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                oSqlParameter.Value = objClsRxH_271Master.RxH_271Details[0].SubscriberDemoChgAddress1;

                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;

                //Subscriber Changed Address2
                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sSubscriberDemoChgAddress2";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                oSqlParameter.Value = objClsRxH_271Master.RxH_271Details[0].SubscriberDemoChgAddress2;

                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;

                //Subscriber Changed City
                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sSubscriberDemoChgCity";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                oSqlParameter.Value = objClsRxH_271Master.RxH_271Details[0].SubscriberDemoChgCity;

                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;

                //Subscriber Changed State
                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sSubscriberDemoChgState";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                oSqlParameter.Value = objClsRxH_271Master.RxH_271Details[0].SubscriberDemoChgState;

                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;

                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sSTLoopControlID";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                oSqlParameter.Value = objClsRxH_271Master.STLoopCount;

                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;

                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@nPatientID";
                oSqlParameter.SqlDbType = SqlDbType.BigInt;
                oSqlParameter.Direction = ParameterDirection.Input;
                oSqlParameter.Value = objClsRxH_271Master.PatientId;

                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;

                //Subscriber Changed Information 

                //Work with database 
                oSQLCommand.CommandType = CommandType.StoredProcedure;
                // SQL Command Type , is Store Procedure 
                oSQLCommand.CommandText = storedProcedureName;
                // Set Store Procedure Name 
                oSQLCommand.Connection = gloDBConnection;

                _RecordAffected = oSQLCommand.ExecuteNonQuery();

                if (_RecordAffected > 0)
                {
                    _blnResult = true;
                }


                return _blnResult;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                return false;
            }
            finally
            {

                //SLR: Finaly oSQLCommand.Parameters.clear, free oSqlCommand
                if (oSQLCommand != null)
                {
                    oSQLCommand.Parameters.Clear();
                    oSQLCommand.Dispose();
                    oSQLCommand = null;
                }
            }
        }

        public bool InsertEDIResponse271_Master(string storedProcedureName, ClsRxH_271Master objClsRxH_271Master)
        {
            SqlParameter oSqlParameter;

            SqlCommand oSQLCommand = new SqlCommand();
            bool _blnResult = false;
            int _RecordAffected = 0;
            //int i = 0;
            try
            {

                //Check Connection 
                if (gloDBConnection.State == ConnectionState.Closed)
                {
                    _ErrorMessage = "Please check database connecion";
                    return false;
                }

                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sMessageID";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                oSqlParameter.Value = objClsRxH_271Master.MessageID;
                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;


                //insert the multiple ST loop id 
                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sSTLoopControlID";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                oSqlParameter.Value = objClsRxH_271Master.STLoopCount;
                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;

                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sPBM_PayerName";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                oSqlParameter.Value = objClsRxH_271Master.PayerName;
                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;


                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sPBM_PayerParticipantID";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                oSqlParameter.Value = objClsRxH_271Master.PayerParticipantId;
                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;

                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sPBM_PayerMemberID";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                oSqlParameter.Value = objClsRxH_271Master.MemberID;

                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;


                //Physician Name
                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sPhysicianName";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                oSqlParameter.Value = objClsRxH_271Master.InformationRecieverName;
                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;

                //Physician Suffix - MD
                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sPhysicianSuffix";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                oSqlParameter.Value = objClsRxH_271Master.InformationRecieverSuffix;
                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;

                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sNPINumber";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                oSqlParameter.Value = objClsRxH_271Master.NPINumber;
                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;

                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sHealthPlanNumber";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                oSqlParameter.Value = objClsRxH_271Master.HealthPlanNumber;

                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;

                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sHealthPlanName";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                oSqlParameter.Value = objClsRxH_271Master.HealthPlanName;

                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;

                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sRelationshipCode";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                oSqlParameter.Value = objClsRxH_271Master.RelationshipCode;

                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;

                //if Relationshipcode = 18 then Relationshipdescription will be SELF else it will be DEPENDANT
                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sRelationshipDescription";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                oSqlParameter.Value = objClsRxH_271Master.RelationshipDescription;

                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;

                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sPersonCode";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                oSqlParameter.Value = objClsRxH_271Master.PersonCode;

                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;

                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sCardHolderID";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                oSqlParameter.Value = objClsRxH_271Master.CardHolderId;

                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;

                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sCardHolderName";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                oSqlParameter.Value = objClsRxH_271Master.CardHolderName;

                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;

                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sGroupID";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                oSqlParameter.Value = objClsRxH_271Master.GroupId;

                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;

                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sGroupName";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                oSqlParameter.Value = objClsRxH_271Master.GroupName;

                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;

                //ADDED for ANSI 5010
                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sSocialSecurityNumber";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                oSqlParameter.Value = objClsRxH_271Master.SocialSecurityNumber;

                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;

                //ADDED for ANSI 5010
                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sPatientAccountNumber";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                oSqlParameter.Value = objClsRxH_271Master.PatientAccountNumber;

                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;

                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sFormularyListID";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                oSqlParameter.Value = objClsRxH_271Master.FormularyListId;

                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;

                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sAlternativeListID";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                oSqlParameter.Value = objClsRxH_271Master.AlternativeListId;

                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;

                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sCoverageID";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                oSqlParameter.Value = objClsRxH_271Master.CoverageId;

                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;

                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sEmployeeID";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                oSqlParameter.Value = objClsRxH_271Master.EmployeeId;

                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;

                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sBINNumber";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                oSqlParameter.Value = objClsRxH_271Master.BINNumber;

                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;

                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sCoPayID";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                oSqlParameter.Value = objClsRxH_271Master.CopayId;

                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;

                //if pharmacy eligible then YES else NO
                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sIsRetailPharmacyEligible";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                oSqlParameter.Value = objClsRxH_271Master.IsRetailPharmacyEligible;

                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;

                //If pharmacy eligible then enter pharmacy coverage name else ""
                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sRetailPharmacyCoverageName";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                oSqlParameter.Value = objClsRxH_271Master.RetailPharmacyCoveragePlanName;

                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;

                //If pharmacy eligible then enter pharmacy coverage name else ""
                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sRetailPhEligiblityorBenefitInfo";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                oSqlParameter.Value = objClsRxH_271Master.RetailPharmacyEligiblityorBenefitInfo;

                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;


                //if Mail Order Rx Drug eligible then YES else NO
                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sMailOrderRxDrugEligible";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                oSqlParameter.Value = objClsRxH_271Master.IsMailOrdRxDrugEligible;

                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;

                //If Mail Order Rx Drug eligible eligible then enter pharmacy coverage name else ""
                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sMailOrderRxDrugCoverageName";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                oSqlParameter.Value = objClsRxH_271Master.MailOrdRxDrugCoveragePlanName;

                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;


                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sMailOrdEligiblityorBenefitInfo";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                oSqlParameter.Value = objClsRxH_271Master.MailOrdRxDrugEligiblityorBenefitInfo;

                //Eligiblity date returned in 271 response file in DTP section
                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;


                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sMailOrderInsTypeCode";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                oSqlParameter.Value = objClsRxH_271Master.MailOrderInsTypeCode;

                //Eligiblity date returned in 271 response file in DTP section
                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;


                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sRetailInsTypeCode";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                oSqlParameter.Value = objClsRxH_271Master.RetailInsTypeCode;

                //Eligiblity date returned in 271 response file in DTP section
                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;


                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sMailOrderMonetaryAmount";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                oSqlParameter.Value = objClsRxH_271Master.MailOrderMonetaryAmount;

                //Eligiblity date returned in 271 response file in DTP section
                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;


                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sRetailMonetaryAmount";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                oSqlParameter.Value = objClsRxH_271Master.RetailMonetaryAmount;

                //Eligiblity date returned in 271 response file in DTP section
                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;

                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sDFirstName";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                oSqlParameter.Value = objClsRxH_271Master.DFirstName;

                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;

                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sDMiddleName";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                oSqlParameter.Value = objClsRxH_271Master.DMiddleName;

                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;

                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sDLastName";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                oSqlParameter.Value = objClsRxH_271Master.DLastName;

                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;

                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sDGender";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                oSqlParameter.Value = objClsRxH_271Master.DGender;

                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;

                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sDDOB";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                oSqlParameter.Value = objClsRxH_271Master.DDOB;

                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;

                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sDSSN";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                oSqlParameter.Value = objClsRxH_271Master.DSSN;

                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;

                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sDAddress1";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                oSqlParameter.Value = objClsRxH_271Master.DAddress1;

                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;

                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sDAddress2";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                oSqlParameter.Value = objClsRxH_271Master.DAddress2;

                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;

                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sDCity";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                oSqlParameter.Value = objClsRxH_271Master.DCity;

                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;

                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sDState";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                oSqlParameter.Value = objClsRxH_271Master.DState;

                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;

                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sDZip";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                oSqlParameter.Value = objClsRxH_271Master.DZip;

                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;

                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@nPatientID";
                oSqlParameter.SqlDbType = SqlDbType.BigInt;
                oSqlParameter.Direction = ParameterDirection.Input;
                oSqlParameter.Value = objClsRxH_271Master.PatientId;

                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;


                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@dtResquestDateTimeStamp";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                oSqlParameter.Value = DateTime.Now;

                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;


                //adding the Is Demographic flag to database - for Subscriber

                //Dependant changed flag
                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@IsDependentdemoChange";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                oSqlParameter.Value = objClsRxH_271Master.IsDependentdemoChange;

                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;

                //Dependant changed  First Name
                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sDependentdemochgFirstName";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                oSqlParameter.Value = objClsRxH_271Master.DependentdemochgFirstName;

                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;

                //Dependant changed First Name
                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sDependentdemoChgMiddleName";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                oSqlParameter.Value = objClsRxH_271Master.DependentdemoChgMiddleName;

                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;

                //Dependant changed LastName
                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sDependentdemoChgLastName";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                oSqlParameter.Value = objClsRxH_271Master.DependentdemoChgLastName;

                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;

                //Dependant changed Gender
                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sDependentdemoChgGender";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                oSqlParameter.Value = objClsRxH_271Master.DependentdemoChgGender;

                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;
                //Dependant changed DOB
                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sDependentdemoChgDOB";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                oSqlParameter.Value = objClsRxH_271Master.DependentdemoChgDOB;

                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;

                //Dependant changed SSN
                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sDependentdemoChgSSN";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                oSqlParameter.Value = objClsRxH_271Master.DependentdemoChgSSN;

                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;

                //Dependant changed Address1
                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sDependentdemoChgAddress1";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                oSqlParameter.Value = objClsRxH_271Master.DependentdemoChgAddress1;

                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;

                //Dependant changed Address2
                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sDependentdemoChgAddress2";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                oSqlParameter.Value = objClsRxH_271Master.DependentdemoChgAddress2;

                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;

                //Dependant changed City
                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sDependentdemoChgCity";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                oSqlParameter.Value = objClsRxH_271Master.DependentdemoChgCity;

                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;

                //Dependant changed State
                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sDependentdemoChgState";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                oSqlParameter.Value = objClsRxH_271Master.DependentdemoChgState;


                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;

                //Dependant changed Zip
                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sDependentdemoChgZip";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                oSqlParameter.Value = objClsRxH_271Master.DependentdemoChgZip;

                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;

                //sIsContractedProvider
                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sIsContractedProvider";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                oSqlParameter.Value = objClsRxH_271Master.IsContractedProvider;

                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;

                //sContractedProviderName
                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sContractedProviderName";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                oSqlParameter.Value = objClsRxH_271Master.ContractedProviderName;

                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;

                //sContractedProviderNumber
                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sContractedProviderNumber";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                oSqlParameter.Value = objClsRxH_271Master.ContractedProviderNumber;

                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;

                //sContProvMailOrderEligible
                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sContProvMailOrderEligible";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                oSqlParameter.Value = objClsRxH_271Master.ContProvMailOrderEligible;

                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;

                //@sContProvMailOrderCoverageInfo
                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sContProvMailOrderCoverageInfo";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                oSqlParameter.Value = objClsRxH_271Master.ContProvMailOrderCoverageInfo;

                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;

                //@sContProvMailOrderInsTypeCode
                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sContProvMailOrderInsTypeCode";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                oSqlParameter.Value = objClsRxH_271Master.ContProvMailOrderInsTypeCode;

                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;

                //@sContProvMailOrderMonetaryAmt
                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sContProvMailOrderMonetaryAmt";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                oSqlParameter.Value = objClsRxH_271Master.ContProvMailOrderMonetaryAmt;


                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;

                //@sContProvRetailsEligible
                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sContProvRetailsEligible";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                oSqlParameter.Value = objClsRxH_271Master.ContProvRetailsEligible;

                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;

                //@sContProvRetailCoverageInfo
                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sContProvRetailCoverageInfo";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                oSqlParameter.Value = objClsRxH_271Master.ContProvRetailCoverageInfo;

                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;

                //@sContProvRetailInsTypeCode
                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sContProvRetailInsTypeCode";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                oSqlParameter.Value = objClsRxH_271Master.ContProvRetailInsTypeCode;

                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;

                //@sContProvRetailMonetaryAmt
                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sContProvRetailMonetaryAmt";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                oSqlParameter.Value = objClsRxH_271Master.ContProvRetailMonetaryAmt;

                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;

                //@sIsPrimaryPayer
                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sIsPrimaryPayer";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                oSqlParameter.Value = objClsRxH_271Master.IsPrimaryPayer;

                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;

                //@sPrimaryPayerName
                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sPrimaryPayerName";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                oSqlParameter.Value = objClsRxH_271Master.PrimaryPayerName;

                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;

                //@sPrimaryPayerNumber
                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sPrimaryPayerNumber";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                oSqlParameter.Value = objClsRxH_271Master.PrimaryPayerNumber;

                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;

                //@sPrimaryPayerMailOrderEligible
                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sPrimaryPayerMailOrderEligible";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                oSqlParameter.Value = objClsRxH_271Master.PrimaryPayerMailOrderEligible;

                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;

                //@sPrimaryPayerMailOrderCoverageInfo
                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sPrimaryPayerMailOrderCoverageInfo";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                oSqlParameter.Value = objClsRxH_271Master.PrimaryPayerMailOrderCoverageInfo;

                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;

                //@sPrimaryPayerMailOrderInsTypeCode
                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sPrimaryPayerMailOrderInsTypeCode";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                oSqlParameter.Value = objClsRxH_271Master.PrimaryPayerMailOrderInsTypeCode;

                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;

                //@sPrimaryPayerMailOrderMonetaryAmt
                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sPrimaryPayerMailOrderMonetaryAmt";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                oSqlParameter.Value = objClsRxH_271Master.PrimaryPayerMailOrderMonetaryAmt;


                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;

                //@sPrimaryPayerRetailsEligible
                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sPrimaryPayerRetailsEligible";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                oSqlParameter.Value = objClsRxH_271Master.PrimaryPayerRetailsEligible;


                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;

                //@sPrimaryPayerRetailCoverageInfo
                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sPrimaryPayerRetailCoverageInfo";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                oSqlParameter.Value = objClsRxH_271Master.PrimaryPayerRetailCoverageInfo;

                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;

                //@sPrimaryPayerRetailInsTypeCode
                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sPrimaryPayerRetailInsTypeCode";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                oSqlParameter.Value = objClsRxH_271Master.PrimaryPayerRetailInsTypeCode;


                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;

                //@sPrimaryPayerRetailMonetaryAmt
                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sPrimaryPayerRetailMonetaryAmt";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                oSqlParameter.Value = objClsRxH_271Master.PrimaryPayerRetailMonetaryAmt;


                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;

                //@sHealthPlanBenefitCoverageName
                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sHealthPlanBenefitCoverageName";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                oSqlParameter.Value = objClsRxH_271Master.HealthPlanBenefitCoverageName;


                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;


                //@LTCPharmCovName
                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@LTCPharmCovName";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                oSqlParameter.Value = objClsRxH_271Master.LTCPharmCovName;

                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;

                //@SpecialtyPharmCovName
                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@SpecialtyPharmCovName";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                oSqlParameter.Value = objClsRxH_271Master.SpecialtyPharmCovName;

                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;

                //Service date returned in 271 response file in DTP section
                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@HtlthPlnCovBenftEligDate";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                oSqlParameter.Value = objClsRxH_271Master.HlthPlnBenftCovEligibilityDate;

                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;

                //Service date returned in 271 response file in DTP section
                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@HtlthPlnCovBenftServiceDate";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                oSqlParameter.Value = objClsRxH_271Master.HlthPlnBenftCovServiceDate;

                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;

                //Service date returned in 271 response file in DTP section
                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@RetailPharmEligDate";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                oSqlParameter.Value = objClsRxH_271Master.RetailOrdPhrmEligibilityDate;

                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;

                //Service date returned in 271 response file in DTP section
                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@RetailPharmServiceDate";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                oSqlParameter.Value = objClsRxH_271Master.RetailPhrmServiceDate;

                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;

                //Service date returned in 271 response file in DTP section
                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@MailOrdPharmEligDate";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                oSqlParameter.Value = objClsRxH_271Master.MailOrdPhrmEligibilityDate;

                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;

                //Service date returned in 271 response file in DTP section
                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@MailOrdPharmServiceDate";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                oSqlParameter.Value = objClsRxH_271Master.MailOrdPhrmServiceDate;

                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;

                //Service date returned in 271 response file in DTP section
                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@LTCPharmEligDate";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                oSqlParameter.Value = objClsRxH_271Master.LTCPhrmEligDate;

                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;

                //Service date returned in 271 response file in DTP section
                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@LTCPharmServiceDate";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                oSqlParameter.Value = objClsRxH_271Master.LTCPhrmServiceDate;

                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;

                //Service date returned in 271 response file in DTP section
                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@SpecialtyPharmEligDate";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                oSqlParameter.Value = objClsRxH_271Master.SpecialtyPhrmEligDate;

                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;

                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@SpecialtyPharmServiceDate";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                oSqlParameter.Value = objClsRxH_271Master.SpecialtyPhrmServiceDate;

                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;

                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sEligiblityDate";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                oSqlParameter.Value = objClsRxH_271Master.EligiblityDate;

                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;

                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sServiceDate";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                oSqlParameter.Value = objClsRxH_271Master.ServiceDate;

                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;

                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sHlthPlnCovInsTypeCode";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                oSqlParameter.Value = objClsRxH_271Master.HlthPlnCovInsTypeCode;

                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;

                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sLTCPharmacyInsTypeCode";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                oSqlParameter.Value = objClsRxH_271Master.LTCPharmacyInsTypeCode;

                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;

                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sSpecialtyPharmacyInsTypeCode";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                oSqlParameter.Value = objClsRxH_271Master.SpecialtyPharmacyInsTypeCode;

                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;

                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sHealthPlanBenefitEligibilityInfo";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                oSqlParameter.Value = objClsRxH_271Master.HealthPlanBenefitEligibilityInfo;

                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;

                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sTRNReferenceIdentification";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                oSqlParameter.Value = objClsRxH_271Master.TRNReferenceIdentification;

                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;

                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sTRNOrignationCompanyIdentifier";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                oSqlParameter.Value = objClsRxH_271Master.TRNOrignationCompanyIdentifier;

                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;

                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sTRNDivisionorGroup";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                oSqlParameter.Value = objClsRxH_271Master.TRNDivisionorGroup;

                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;

                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sLTCPhEligiblityorBenefitInfo";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                oSqlParameter.Value = objClsRxH_271Master.LTCPhEligiblityorBenefitInfo;

                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;

                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sSpecialityPhEligiblityorBenefitInfo";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                oSqlParameter.Value = objClsRxH_271Master.SpecialityPhEligiblityorBenefitInfo;

                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;
                //#endregion "Parameters assignment"
                //adding the Is Demographic flag to database - for Subscriber

                //Work with database 
                oSQLCommand.CommandType = CommandType.StoredProcedure;
                // SQL Command Type , is Store Procedure 
                oSQLCommand.CommandText = storedProcedureName;
                // Set Store Procedure Name 
                oSQLCommand.Connection = gloDBConnection;

                _RecordAffected = oSQLCommand.ExecuteNonQuery();

                if (_RecordAffected > 0)
                {
                    _blnResult = true;
                }

                return _blnResult;

            }
            catch (Exception ex)
            {
              
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);

                if (oSQLCommand != null)
                {                    
                    oSQLCommand.Dispose();
                    oSQLCommand = null;
                }
                return false;

            }
            finally
            {
                
                if (oSQLCommand != null)
                {
                    //SLR: Finaly oSQLCommand.Parameters.clear and then
                    oSQLCommand.Parameters.Clear();
                    oSQLCommand.Dispose();
                    oSQLCommand = null;
                }


            }
        }
        //ISA_ControlNumber    //BHT_number
        public bool InsertEDIResquest270(string storedProcedureName, string messageId, string TransactionID, ClsPatient opatient)
        {
            SqlParameter oSqlParameter;

            SqlCommand oSQLCommand = new SqlCommand();
            bool _blnResult = false;
            int _RecordAffected = 0;
            //int i = 0;
            try
            {

                //Check Connection 
                if (gloDBConnection.State == ConnectionState.Closed)
                {
                    _ErrorMessage = "Please check database connecion";
                    return false;
                }

                oSqlParameter = new SqlParameter();
                //this will save the unique control number from 270 file
                oSqlParameter.ParameterName = "@sMessageID";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                oSqlParameter.Value = messageId;
                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;

                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sPatientCode";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                oSqlParameter.Value = opatient.PatientID;//opatient.SSN
                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;

                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@dt270RequestDateTimeStamp";
                oSqlParameter.SqlDbType = SqlDbType.DateTime;
                oSqlParameter.Direction = ParameterDirection.Input;
                oSqlParameter.Value = DateTime.Now;
                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;

                oSqlParameter = new SqlParameter();
                //this will save the unique BHT number from 270 file to identify the 271 transaction
                oSqlParameter.ParameterName = "@sTransactionID";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                oSqlParameter.Value = TransactionID;


                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;


                //VisitID
                oSqlParameter = new SqlParameter();
                //this will save the unique BHT number from 270 file to identify the 271 transaction
                long nMachineID;
                //gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(ClsgloRxHubGeneral.ConnectionString);
                nMachineID = ClsgloRxHubGeneral.GetPrefixTransactionID(opatient.PatientID);
                oSqlParameter.ParameterName = "@sVisitID";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                oSqlParameter.Value = TransactionID;


                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;

                //Work with database 
                oSQLCommand.CommandType = CommandType.StoredProcedure;
                // SQL Command Type , is Store Procedure 
                oSQLCommand.CommandText = storedProcedureName;
                // Set Store Procedure Name 
                oSQLCommand.Connection = gloDBConnection;

                _RecordAffected = oSQLCommand.ExecuteNonQuery();

                if (_RecordAffected > 0)
                {
                    _blnResult = true;
                }

                return _blnResult;

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                //_ErrorMessage = objError.Message;
                return false;

            }
            finally
            {
                if (oSQLCommand != null)
                {
                    //SLR: Finaly oSQLCommand.Parameters.clear, free oSQLCommand and then
                    oSQLCommand.Parameters.Clear();
                    oSQLCommand.Dispose();
                    oSQLCommand = null;
                }

            }
        }

        //ISA_ControlNumber    //BHT_number
        public bool InsertEDIResquest270_5010(string storedProcedureName, string messageId, string TransactionID, long PatientID)
        {
            SqlParameter oSqlParameter;

            SqlCommand oSQLCommand = new SqlCommand();

            SqlConnection oDBConnection = new SqlConnection(gloRxHub.ClsgloRxHubGeneral.ConnectionString);

            bool _blnResult = false;
            int _RecordAffected = 0;

            try
            {

                oSqlParameter = new SqlParameter();
                //this will save the unique control number from 270 file
                oSqlParameter.ParameterName = "@sMessageID";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                oSqlParameter.Value = messageId;
                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;

                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sPatientCode";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                oSqlParameter.Value = PatientID.ToString();
                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;
                
                // Take Server Date Handled in SP
                //oSqlParameter = new SqlParameter();
                //oSqlParameter.ParameterName = "@dt270RequestDateTimeStamp";
                //oSqlParameter.SqlDbType = SqlDbType.DateTime;
                //oSqlParameter.Direction = ParameterDirection.Input;
                //oSqlParameter.Value = DateTime.Now;
                //oSQLCommand.Parameters.Add(oSqlParameter);
                //oSqlParameter = null;

                oSqlParameter = new SqlParameter();
                //this will save the unique BHT number from 270 file to identify the 271 transaction
                oSqlParameter.ParameterName = "@sTransactionID";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                oSqlParameter.Value = TransactionID;


                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;

                //Work with database 
                oSQLCommand.CommandType = CommandType.StoredProcedure;
                // SQL Command Type , is Store Procedure 
                oSQLCommand.CommandText = storedProcedureName;
                // Set Store Procedure Name 
                oSQLCommand.Connection = oDBConnection;

                //Check Connection 

                if (oDBConnection.State == ConnectionState.Closed)
                {
                    oDBConnection.Open();
                }

                _RecordAffected = oSQLCommand.ExecuteNonQuery();

                oDBConnection.Close();
                oDBConnection.Dispose();
                oDBConnection = null;

                if (_RecordAffected > 0)
                {
                    _blnResult = true;
                }

                return _blnResult;

            }
            catch (Exception ex)
            {
                if (oDBConnection != null)
                {
                    oDBConnection.Close();
                    oDBConnection.Dispose();
                    oDBConnection = null;
                }
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);

                if (oSQLCommand != null)
                {
                    oSQLCommand.Dispose();
                    oSQLCommand = null;
                }
                return false;

            }
            finally
            {
                if (oDBConnection != null)
                {
                    oDBConnection.Close();
                    oDBConnection.Dispose();
                    oDBConnection = null;
                }
                if (oSQLCommand != null)
                {
                    oSQLCommand.Parameters.Clear();
                    oSQLCommand.Dispose();
                    oSQLCommand = null;
                }


            }
        }

        public bool InsertRxH_HistoryRespDetails(string storedProcedureName, ClsPatient objPatient, Int64 nPatientID)
        {
            SqlParameter oSqlParameter;

            SqlCommand oSQLCommand = new SqlCommand();
            bool _blnResult = false;
            int _RecordAffected = 0;
            int i = 0;
            try
            {

                //Check Connection 
                if (gloDBConnection.State == ConnectionState.Closed)
                {
                    _ErrorMessage = "Please check database connection";
                    return false;
                }

                for (i = 0; i <= objPatient.Medication.Count - 1; i++)
                {
                    oSqlParameter = new SqlParameter();
                    oSqlParameter.ParameterName = "@sRxReferenceNumber";
                    oSqlParameter.SqlDbType = SqlDbType.VarChar;
                    oSqlParameter.Direction = ParameterDirection.Input;
                    oSqlParameter.Value = "";
                    oSQLCommand.Parameters.Add(oSqlParameter);
                    oSqlParameter = null;

                    oSqlParameter = new SqlParameter();
                    oSqlParameter.ParameterName = "@sPrescriberOrderNumber";
                    oSqlParameter.SqlDbType = SqlDbType.VarChar;
                    oSqlParameter.Direction = ParameterDirection.Input;
                    oSqlParameter.Value = "";
                    oSQLCommand.Parameters.Add(oSqlParameter);
                    oSqlParameter = null;

                    oSqlParameter = new SqlParameter();
                    oSqlParameter.ParameterName = "@sPharmacyIdentification";
                    oSqlParameter.SqlDbType = SqlDbType.BigInt;
                    oSqlParameter.Direction = ParameterDirection.Input;
                    
                    oSqlParameter.Value = objPatient.Pharmacy.NCPDPID;
                    oSQLCommand.Parameters.Add(oSqlParameter);
                    oSqlParameter = null;

                    oSqlParameter = new SqlParameter();
                    oSqlParameter.ParameterName = "@sStoreName";
                    oSqlParameter.SqlDbType = SqlDbType.VarChar;
                    oSqlParameter.Direction = ParameterDirection.Input;
                    if (objPatient.Pharmacy.StoreName != "")
                    {
                        oSqlParameter.Value = objPatient.Pharmacy.StoreName;
                    }
                    else
                    {
                        oSqlParameter.Value = "";
                    }
                    //   oSqlParameter.Value = objPatient.Pharmacy.StoreName;
                    oSQLCommand.Parameters.Add(oSqlParameter);
                    oSqlParameter = null;

                    oSqlParameter = new SqlParameter();
                    oSqlParameter.ParameterName = "@sPharmacistLastName";
                    oSqlParameter.SqlDbType = SqlDbType.VarChar;
                    oSqlParameter.Direction = ParameterDirection.Input;
                    if (objPatient.Pharmacy.PharmacistLastName != "")//null)
                    {
                        oSqlParameter.Value = objPatient.Pharmacy.PharmacistLastName;
                    }
                    else
                    {
                        oSqlParameter.Value = "";
                    }

                    oSQLCommand.Parameters.Add(oSqlParameter);
                    oSqlParameter = null;


                    oSqlParameter = new SqlParameter();
                    oSqlParameter.ParameterName = "@sPharmacistFirstName";
                    oSqlParameter.SqlDbType = SqlDbType.VarChar;
                    oSqlParameter.Direction = ParameterDirection.Input;

                    if (objPatient.Pharmacy.PharmacistFirstName != "")//null)
                    {
                        oSqlParameter.Value = objPatient.Pharmacy.PharmacistFirstName;
                    }
                    else
                    {
                        oSqlParameter.Value = "";
                    }
                    oSQLCommand.Parameters.Add(oSqlParameter);
                    oSqlParameter = null;


                    oSqlParameter = new SqlParameter();
                    oSqlParameter.ParameterName = "@sPharmacistMiddleName";
                    oSqlParameter.SqlDbType = SqlDbType.VarChar;
                    oSqlParameter.Direction = ParameterDirection.Input;
                    if (objPatient.Pharmacy.PharmacistMiddleName != "")//null)
                    {
                        oSqlParameter.Value = objPatient.Pharmacy.PharmacistMiddleName;
                    }
                    else
                    {
                        oSqlParameter.Value = "";
                    }
                    //oSqlParameter.Value = objPatient.Pharmacy.PharmacistMiddleName;
                    oSQLCommand.Parameters.Add(oSqlParameter);
                    oSqlParameter = null;

                    oSqlParameter = new SqlParameter();
                    oSqlParameter.ParameterName = "@sPharmacistSuffix";
                    oSqlParameter.SqlDbType = SqlDbType.VarChar;
                    oSqlParameter.Direction = ParameterDirection.Input;
                    if (objPatient.Pharmacy.PharmacistSuffix != "")//null)
                    {
                        oSqlParameter.Value = objPatient.Pharmacy.PharmacistSuffix;
                    }
                    else
                    {
                        oSqlParameter.Value = "";
                    }
                    //oSqlParameter.Value = objPatient.Pharmacy.PharmacistSuffix;
                    oSQLCommand.Parameters.Add(oSqlParameter);
                    oSqlParameter = null;

                    oSqlParameter = new SqlParameter();
                    oSqlParameter.ParameterName = "@sPharmacistPrefix";
                    oSqlParameter.SqlDbType = SqlDbType.VarChar;
                    oSqlParameter.Direction = ParameterDirection.Input;
                    if (objPatient.Pharmacy.PharmacistPrefix != "")//null)
                    {
                        oSqlParameter.Value = objPatient.Pharmacy.PharmacistPrefix;
                    }
                    else
                    {
                        oSqlParameter.Value = "";
                    }
                    // oSqlParameter.Value = objPatient.Pharmacy.PharmacistPrefix;
                    oSQLCommand.Parameters.Add(oSqlParameter);
                    oSqlParameter = null;

                    oSqlParameter = new SqlParameter();
                    oSqlParameter.ParameterName = "@sPharmacyAddressLine1";
                    oSqlParameter.SqlDbType = SqlDbType.VarChar;
                    oSqlParameter.Direction = ParameterDirection.Input;
                    if (objPatient.Pharmacy.PhramacyAddress.AddressLine1 != "")// null)
                    {
                        oSqlParameter.Value = objPatient.Pharmacy.PhramacyAddress.AddressLine1;
                    }
                    else
                    {
                        oSqlParameter.Value = "";
                    }
                    //oSqlParameter.Value = objPatient.Pharmacy.PhramacyAddress.AddressLine1;
                    oSQLCommand.Parameters.Add(oSqlParameter);
                    oSqlParameter = null;

                    oSqlParameter = new SqlParameter();
                    oSqlParameter.ParameterName = "@sPharmacyAddressLine2";
                    oSqlParameter.SqlDbType = SqlDbType.VarChar;
                    oSqlParameter.Direction = ParameterDirection.Input;
                    if (objPatient.Pharmacy.PhramacyAddress.AddressLine2 != "")//null
                    {
                        oSqlParameter.Value = objPatient.Pharmacy.PhramacyAddress.AddressLine2;
                    }
                    else
                    {
                        oSqlParameter.Value = "";
                    }
                    // oSqlParameter.Value = objPatient.Pharmacy.PhramacyAddress.AddressLine2;
                    oSQLCommand.Parameters.Add(oSqlParameter);
                    oSqlParameter = null;

                    oSqlParameter = new SqlParameter();
                    oSqlParameter.ParameterName = "@sPharmacyState";
                    oSqlParameter.SqlDbType = SqlDbType.VarChar;
                    oSqlParameter.Direction = ParameterDirection.Input;
                    if (objPatient.Pharmacy.PhramacyAddress.State != "")//null
                    {
                        oSqlParameter.Value = objPatient.Pharmacy.PhramacyAddress.State;
                    }
                    else
                    {
                        oSqlParameter.Value = "";
                    }
                    oSQLCommand.Parameters.Add(oSqlParameter);
                    oSqlParameter = null;

                    oSqlParameter = new SqlParameter();
                    oSqlParameter.ParameterName = "@sPharmacyZip";
                    oSqlParameter.SqlDbType = SqlDbType.VarChar;
                    oSqlParameter.Direction = ParameterDirection.Input;
                    if (objPatient.Pharmacy.PhramacyAddress.Zip != "")//null
                    {
                        oSqlParameter.Value = objPatient.Pharmacy.PhramacyAddress.Zip;
                    }
                    else
                    {
                        oSqlParameter.Value = "";
                    }
                    //   oSqlParameter.Value = objPatient.Pharmacy.PhramacyAddress.Zip;
                    oSQLCommand.Parameters.Add(oSqlParameter);
                    oSqlParameter = null;


                    oSqlParameter = new SqlParameter();
                    oSqlParameter.ParameterName = "@sPharmacyPlaceLocQualifier";
                    oSqlParameter.SqlDbType = SqlDbType.VarChar;
                    oSqlParameter.Direction = ParameterDirection.Input;
                    if (objPatient.Pharmacy.PhramacyAddress.PlaceLocationQualifier != "")//null
                    {
                        oSqlParameter.Value = objPatient.Pharmacy.PhramacyAddress.PlaceLocationQualifier;
                    }
                    else
                    {
                        oSqlParameter.Value = "";
                    }

                    //oSqlParameter.Value = objPatient.Pharmacy.PhramacyAddress.PlaceLocationQualifier;
                    oSQLCommand.Parameters.Add(oSqlParameter);
                    oSqlParameter = null;


                    oSqlParameter = new SqlParameter();
                    oSqlParameter.ParameterName = "@sPharmacyEmail";
                    oSqlParameter.SqlDbType = SqlDbType.VarChar;
                    oSqlParameter.Direction = ParameterDirection.Input;
                    if (objPatient.Pharmacy.PharmacyContactDetails.Email != "")//null
                    {
                        oSqlParameter.Value = objPatient.Pharmacy.PharmacyContactDetails.Email;
                    }
                    else
                    {
                        oSqlParameter.Value = "";
                    }
                    //  oSqlParameter.Value = objPatient.Pharmacy.PharmacyContactDetails.Email;
                    oSQLCommand.Parameters.Add(oSqlParameter);
                    oSqlParameter = null;

                    oSqlParameter = new SqlParameter();
                    oSqlParameter.ParameterName = "@sPharmacyPhNo";
                    oSqlParameter.SqlDbType = SqlDbType.VarChar;
                    oSqlParameter.Direction = ParameterDirection.Input;
                    if (objPatient.Pharmacy.PharmacyContactDetails.Phone != "")//null
                    {
                        oSqlParameter.Value = objPatient.Pharmacy.PharmacyContactDetails.Phone;
                    }
                    else
                    {
                        oSqlParameter.Value = "";
                    }
                    //oSqlParameter.Value = objPatient.Pharmacy.PharmacyContactDetails.Phone;
                    oSQLCommand.Parameters.Add(oSqlParameter);
                    oSqlParameter = null;

                    oSqlParameter = new SqlParameter();
                    oSqlParameter.ParameterName = "@sPharmacyPhQualifier";
                    oSqlParameter.SqlDbType = SqlDbType.VarChar;
                    oSqlParameter.Direction = ParameterDirection.Input;
                    if (objPatient.Pharmacy.PharmacyContactDetails.PhoneQualifier != "")//null
                    {
                        oSqlParameter.Value = objPatient.Pharmacy.PharmacyContactDetails.PhoneQualifier;
                    }
                    else
                    {
                        oSqlParameter.Value = "";
                    }

                    // oSqlParameter.Value = objPatient.Pharmacy.PharmacyContactDetails.PhoneQualifier;
                    oSQLCommand.Parameters.Add(oSqlParameter);
                    oSqlParameter = null;

                    oSqlParameter = new SqlParameter();
                    oSqlParameter.ParameterName = "@sPresciberID";
                    oSqlParameter.SqlDbType = SqlDbType.BigInt;
                    oSqlParameter.Direction = ParameterDirection.Input;
                    
                    oSqlParameter.Value = objPatient.Provider.ProviderID;
                    oSQLCommand.Parameters.Add(oSqlParameter);
                    oSqlParameter = null;


                    oSqlParameter = new SqlParameter();
                    oSqlParameter.ParameterName = "@sClinicName";
                    oSqlParameter.SqlDbType = SqlDbType.VarChar;
                    oSqlParameter.Direction = ParameterDirection.Input;
                    if (objPatient.Provider.ClinicName != "")//null
                    {
                        oSqlParameter.Value = objPatient.Provider.ClinicName;
                    }
                    else
                    {
                        oSqlParameter.Value = "";
                    }
                    //oSqlParameter.Value = objPatient.Provider.ClinicName;
                    oSQLCommand.Parameters.Add(oSqlParameter);
                    oSqlParameter = null;

                    oSqlParameter = new SqlParameter();
                    oSqlParameter.ParameterName = "@sPrescriberFirstName";
                    oSqlParameter.SqlDbType = SqlDbType.VarChar;
                    oSqlParameter.Direction = ParameterDirection.Input;
                    if (objPatient.Provider.ProviderFirstName != "")//null
                    {
                        oSqlParameter.Value = objPatient.Provider.ProviderFirstName;
                    }
                    else
                    {
                        oSqlParameter.Value = "";
                    }
                    // oSqlParameter.Value = objPatient.Provider.ProviderFirstName;
                    oSQLCommand.Parameters.Add(oSqlParameter);
                    oSqlParameter = null;


                    oSqlParameter = new SqlParameter();
                    oSqlParameter.ParameterName = "@sPrescriberMiddleName";
                    oSqlParameter.SqlDbType = SqlDbType.VarChar;
                    oSqlParameter.Direction = ParameterDirection.Input;
                    if (objPatient.Provider.ProviderMiddleName != "")//null
                    {
                        oSqlParameter.Value = objPatient.Provider.ProviderMiddleName;
                    }
                    else
                    {
                        oSqlParameter.Value = "";
                    }

                    //oSqlParameter.Value = objPatient.Provider.ProviderMiddleName;
                    oSQLCommand.Parameters.Add(oSqlParameter);
                    oSqlParameter = null;

                    oSqlParameter = new SqlParameter();
                    oSqlParameter.ParameterName = "@sPrescriberLastName";
                    oSqlParameter.SqlDbType = SqlDbType.VarChar;
                    oSqlParameter.Direction = ParameterDirection.Input;
                    if (objPatient.Provider.ProviderLastName != "")//null
                    {
                        oSqlParameter.Value = objPatient.Provider.ProviderLastName;
                    }
                    else
                    {
                        oSqlParameter.Value = "";
                    }
                    // oSqlParameter.Value = objPatient.Provider.ProviderLastName;
                    oSQLCommand.Parameters.Add(oSqlParameter);
                    oSqlParameter = null;

                    oSqlParameter = new SqlParameter();
                    oSqlParameter.ParameterName = "@sPrescriberAMASpecialty";
                    oSqlParameter.SqlDbType = SqlDbType.VarChar;
                    oSqlParameter.Direction = ParameterDirection.Input;
                    if (objPatient.Provider.AMASpecialtyCode != "")//null
                    {
                        oSqlParameter.Value = objPatient.Provider.AMASpecialtyCode;
                    }
                    else
                    {
                        oSqlParameter.Value = "";
                    }
                    // oSqlParameter.Value = objPatient.Provider.AMASpecialtyCode;
                    oSQLCommand.Parameters.Add(oSqlParameter);
                    oSqlParameter = null;

                    oSqlParameter = new SqlParameter();
                    oSqlParameter.ParameterName = "@sPrescriberOtherSpeacialty";
                    oSqlParameter.SqlDbType = SqlDbType.VarChar;
                    oSqlParameter.Direction = ParameterDirection.Input;
                    if (objPatient.Provider.OtherSpecialtyCode != "")//null
                    {
                        oSqlParameter.Value = objPatient.Provider.OtherSpecialtyCode;
                    }
                    else
                    {
                        oSqlParameter.Value = "";
                    }
                    //oSqlParameter.Value = objPatient.Provider.OtherSpecialtyCode;
                    oSQLCommand.Parameters.Add(oSqlParameter);
                    oSqlParameter = null;

                    oSqlParameter = new SqlParameter();
                    oSqlParameter.ParameterName = "@sPrescriberAddressLine1";
                    oSqlParameter.SqlDbType = SqlDbType.VarChar;
                    oSqlParameter.Direction = ParameterDirection.Input;
                    if (objPatient.Provider.ProviderAddress.AddressLine1 != "")//null
                    {
                        oSqlParameter.Value = objPatient.Provider.ProviderAddress.AddressLine1;
                    }
                    else
                    {
                        oSqlParameter.Value = "";
                    }
                    // oSqlParameter.Value = objPatient.Provider.ProviderAddress.AddressLine1;
                    oSQLCommand.Parameters.Add(oSqlParameter);
                    oSqlParameter = null;

                    oSqlParameter = new SqlParameter();
                    oSqlParameter.ParameterName = "@sPrescriberAddressLine2";
                    oSqlParameter.SqlDbType = SqlDbType.VarChar;
                    oSqlParameter.Direction = ParameterDirection.Input;
                    if (objPatient.Provider.ProviderAddress.AddressLine2 != "")//null
                    {
                        oSqlParameter.Value = objPatient.Provider.ProviderAddress.AddressLine2;
                    }
                    else
                    {
                        oSqlParameter.Value = "";
                    }

                    //oSqlParameter.Value = objPatient.Provider.ProviderAddress.AddressLine2;
                    oSQLCommand.Parameters.Add(oSqlParameter);
                    oSqlParameter = null;

                    oSqlParameter = new SqlParameter();
                    oSqlParameter.ParameterName = "@sPrescriberState";
                    oSqlParameter.SqlDbType = SqlDbType.VarChar;
                    oSqlParameter.Direction = ParameterDirection.Input;

                    if (objPatient.Provider.ProviderAddress.State != "")//null
                    {
                        oSqlParameter.Value = objPatient.Provider.ProviderAddress.State;
                    }
                    else
                    {
                        oSqlParameter.Value = "";
                    }
                    //oSqlParameter.Value = objPatient.Provider.ProviderAddress.State;
                    oSQLCommand.Parameters.Add(oSqlParameter);
                    oSqlParameter = null;


                    oSqlParameter = new SqlParameter();
                    oSqlParameter.ParameterName = "@sPrescriberZip";
                    oSqlParameter.SqlDbType = SqlDbType.VarChar;
                    oSqlParameter.Direction = ParameterDirection.Input;
                    if (objPatient.Provider.ProviderAddress.Zip != "")//null
                    {
                        oSqlParameter.Value = objPatient.Provider.ProviderAddress.Zip;
                    }
                    else
                    {
                        oSqlParameter.Value = "";
                    }

                    //oSqlParameter.Value = objPatient.Provider.ProviderAddress.Zip;
                    oSQLCommand.Parameters.Add(oSqlParameter);
                    oSqlParameter = null;

                    oSqlParameter = new SqlParameter();
                    oSqlParameter.ParameterName = "@sPrescriberPlaceLocQualifier";
                    oSqlParameter.SqlDbType = SqlDbType.VarChar;
                    oSqlParameter.Direction = ParameterDirection.Input;
                    if (objPatient.Provider.ProviderAddress.PlaceLocationQualifier != "")//null
                    {
                        oSqlParameter.Value = objPatient.Provider.ProviderAddress.PlaceLocationQualifier;
                    }
                    else
                    {
                        oSqlParameter.Value = "";
                    }
                    //oSqlParameter.Value = objPatient.Provider.ProviderAddress.PlaceLocationQualifier;
                    oSQLCommand.Parameters.Add(oSqlParameter);
                    oSqlParameter = null;

                    oSqlParameter = new SqlParameter();
                    oSqlParameter.ParameterName = "@sPrescriberEmail";
                    oSqlParameter.SqlDbType = SqlDbType.VarChar;
                    oSqlParameter.Direction = ParameterDirection.Input;
                    if (objPatient.Provider.ProviderContactDtl.Email != "")//null
                    {
                        oSqlParameter.Value = objPatient.Provider.ProviderContactDtl.Email;
                    }
                    else
                    {
                        oSqlParameter.Value = "";
                    }
                    //oSqlParameter.Value = objPatient.Provider.ProviderContactDtl.Email;
                    oSQLCommand.Parameters.Add(oSqlParameter);
                    oSqlParameter = null;


                    oSqlParameter = new SqlParameter();
                    oSqlParameter.ParameterName = "@sPrescriberPhNo";
                    oSqlParameter.SqlDbType = SqlDbType.VarChar;
                    oSqlParameter.Direction = ParameterDirection.Input;
                    if (objPatient.Provider.ProviderContactDtl.Phone != "")//null
                    {
                        oSqlParameter.Value = objPatient.Provider.ProviderContactDtl.Phone;
                    }
                    else
                    {
                        oSqlParameter.Value = "";
                    }
                    //oSqlParameter.Value = objPatient.Provider.ProviderContactDtl.Phone;
                    oSQLCommand.Parameters.Add(oSqlParameter);
                    oSqlParameter = null;

                    oSqlParameter = new SqlParameter();
                    oSqlParameter.ParameterName = "@sPrescriberPhQualifier";
                    oSqlParameter.SqlDbType = SqlDbType.VarChar;
                    oSqlParameter.Direction = ParameterDirection.Input;
                    if (objPatient.Provider.ProviderContactDtl.PhoneQualifier != "")//null
                    {
                        oSqlParameter.Value = objPatient.Provider.ProviderContactDtl.PhoneQualifier;
                    }
                    else
                    {
                        oSqlParameter.Value = "";
                    }
                    // oSqlParameter.Value = objPatient.Provider.ProviderContactDtl.PhoneQualifier;
                    oSQLCommand.Parameters.Add(oSqlParameter);
                    oSqlParameter = null;

                    oSqlParameter = new SqlParameter();
                    oSqlParameter.ParameterName = "@sPatientRelationship";
                    oSqlParameter.SqlDbType = SqlDbType.VarChar;
                    oSqlParameter.Direction = ParameterDirection.Input;
                    if (objPatient.RxH271Master.RelationshipCode != "")//null
                    {
                        oSqlParameter.Value = objPatient.RxH271Master.RelationshipCode;
                    }
                    else
                    {
                        oSqlParameter.Value = "";
                    }
                    // oSqlParameter.Value = objPatient.RxH271Master.RelationshipCode;
                    oSQLCommand.Parameters.Add(oSqlParameter);
                    oSqlParameter = null;

                    oSqlParameter = new SqlParameter();
                    oSqlParameter.ParameterName = "@sPatientFirstName";
                    oSqlParameter.SqlDbType = SqlDbType.VarChar;
                    oSqlParameter.Direction = ParameterDirection.Input;
                    if (objPatient.FirstName != "")//null
                    {
                        oSqlParameter.Value = objPatient.FirstName;
                    }
                    else
                    {
                        oSqlParameter.Value = "";
                    }
                    // oSqlParameter.Value = objPatient.FirstName;
                    oSQLCommand.Parameters.Add(oSqlParameter);
                    oSqlParameter = null;

                    oSqlParameter = new SqlParameter();
                    oSqlParameter.ParameterName = "@sPatientMiddleName";
                    oSqlParameter.SqlDbType = SqlDbType.VarChar;
                    oSqlParameter.Direction = ParameterDirection.Input;

                    if (objPatient.MiddleName != "")//null
                    {
                        oSqlParameter.Value = objPatient.MiddleName;
                    }
                    else
                    {
                        oSqlParameter.Value = "";
                    }
                    //oSqlParameter.Value = objPatient.MiddleName;
                    oSQLCommand.Parameters.Add(oSqlParameter);
                    oSqlParameter = null;

                    oSqlParameter = new SqlParameter();
                    oSqlParameter.ParameterName = "@sPatientLastName";
                    oSqlParameter.SqlDbType = SqlDbType.VarChar;
                    oSqlParameter.Direction = ParameterDirection.Input;

                    if (objPatient.LastName != "")//null
                    {
                        oSqlParameter.Value = objPatient.LastName;
                    }
                    else
                    {
                        oSqlParameter.Value = "";
                    }
                    //oSqlParameter.Value = objPatient.LastName;
                    oSQLCommand.Parameters.Add(oSqlParameter);
                    oSqlParameter = null;

                    oSqlParameter = new SqlParameter();
                    oSqlParameter.ParameterName = "@sPatientGender";
                    oSqlParameter.SqlDbType = SqlDbType.VarChar;
                    oSqlParameter.Direction = ParameterDirection.Input;
                    if (objPatient.Gender != "")//null
                    {
                        oSqlParameter.Value = objPatient.Gender;
                    }
                    else
                    {
                        oSqlParameter.Value = "";
                    }
                    //oSqlParameter.Value = objPatient.Gender;
                    oSQLCommand.Parameters.Add(oSqlParameter);
                    oSqlParameter = null;

                    oSqlParameter = new SqlParameter();
                    oSqlParameter.ParameterName = "@sPatientDOB";
                    oSqlParameter.SqlDbType = SqlDbType.DateTime;
                    oSqlParameter.Direction = ParameterDirection.Input;
                    
                    oSqlParameter.Value = objPatient.DOB;
                    oSQLCommand.Parameters.Add(oSqlParameter);
                    oSqlParameter = null;


                    oSqlParameter = new SqlParameter();
                    oSqlParameter.ParameterName = "@sPatientAddressLine1";
                    oSqlParameter.SqlDbType = SqlDbType.VarChar;
                    oSqlParameter.Direction = ParameterDirection.Input;
                    if (objPatient.PatientAddress.AddressLine1 != "")//null
                    {
                        oSqlParameter.Value = objPatient.PatientAddress.AddressLine1;
                    }
                    else
                    {
                        oSqlParameter.Value = "";
                    }
                    // oSqlParameter.Value = objPatient.PatientAddress.AddressLine1;
                    oSQLCommand.Parameters.Add(oSqlParameter);
                    oSqlParameter = null;

                    oSqlParameter = new SqlParameter();
                    oSqlParameter.ParameterName = "@sPatientAddressLine2";
                    oSqlParameter.SqlDbType = SqlDbType.VarChar;
                    oSqlParameter.Direction = ParameterDirection.Input;
                    if (objPatient.PatientAddress.AddressLine2 != "")//null
                    {
                        oSqlParameter.Value = objPatient.PatientAddress.AddressLine2;
                    }
                    else
                    {
                        oSqlParameter.Value = "";
                    }
                    //oSqlParameter.Value = objPatient.PatientAddress.AddressLine2;
                    oSQLCommand.Parameters.Add(oSqlParameter);
                    oSqlParameter = null;

                    oSqlParameter = new SqlParameter();
                    oSqlParameter.ParameterName = "@sPatientCity";
                    oSqlParameter.SqlDbType = SqlDbType.VarChar;
                    oSqlParameter.Direction = ParameterDirection.Input;
                    if (objPatient.PatientAddress.City != "")//null)
                    {
                        oSqlParameter.Value = objPatient.PatientAddress.City;
                    }
                    else
                    {
                        oSqlParameter.Value = "";
                    }
                    //oSqlParameter.Value = objPatient.PatientAddress.City;
                    oSQLCommand.Parameters.Add(oSqlParameter);
                    oSqlParameter = null;

                    oSqlParameter = new SqlParameter();
                    oSqlParameter.ParameterName = "@sPatientState";
                    oSqlParameter.SqlDbType = SqlDbType.VarChar;
                    oSqlParameter.Direction = ParameterDirection.Input;

                    if (objPatient.PatientAddress.State != "")//null)
                    {
                        oSqlParameter.Value = objPatient.PatientAddress.State;
                    }
                    else
                    {
                        oSqlParameter.Value = "";
                    }
                    //oSqlParameter.Value = objPatient.PatientAddress.State;
                    oSQLCommand.Parameters.Add(oSqlParameter);
                    oSqlParameter = null;

                    oSqlParameter = new SqlParameter();
                    oSqlParameter.ParameterName = "@sPatientZip";
                    oSqlParameter.SqlDbType = SqlDbType.VarChar;
                    oSqlParameter.Direction = ParameterDirection.Input;
                    if (objPatient.PatientAddress.Zip != "")//null)
                    {
                        oSqlParameter.Value = objPatient.PatientAddress.Zip;
                    }
                    else
                    {
                        oSqlParameter.Value = "";
                    }
                    //oSqlParameter.Value = objPatient.PatientAddress.Zip;
                    oSQLCommand.Parameters.Add(oSqlParameter);
                    oSqlParameter = null;


                    oSqlParameter = new SqlParameter();
                    oSqlParameter.ParameterName = "@sPatientPlaceLocQualifier";
                    oSqlParameter.SqlDbType = SqlDbType.VarChar;
                    oSqlParameter.Direction = ParameterDirection.Input;
                    if (objPatient.PatientAddress.PlaceLocationQualifier != "")//null)
                    {
                        oSqlParameter.Value = objPatient.PatientAddress.PlaceLocationQualifier;
                    }
                    else
                    {
                        oSqlParameter.Value = "";
                    }
                    //oSqlParameter.Value = objPatient.PatientAddress.PlaceLocationQualifier;
                    oSQLCommand.Parameters.Add(oSqlParameter);
                    oSqlParameter = null;

                    oSqlParameter = new SqlParameter();
                    oSqlParameter.ParameterName = "@sPatientEmail";
                    oSqlParameter.SqlDbType = SqlDbType.VarChar;
                    oSqlParameter.Direction = ParameterDirection.Input;
                    //oSqlParameter.Value = objPatient.PatientContact.Email;
                    if (objPatient.PatientContact.Email != "")//null)
                    {
                        oSqlParameter.Value = objPatient.PatientContact.Email;
                    }
                    else
                    {
                        oSqlParameter.Value = "";
                    }
                    oSQLCommand.Parameters.Add(oSqlParameter);
                    oSqlParameter = null;

                    oSqlParameter = new SqlParameter();
                    oSqlParameter.ParameterName = "@sPatientPhNo";
                    oSqlParameter.SqlDbType = SqlDbType.VarChar;
                    oSqlParameter.Direction = ParameterDirection.Input;
                    if (objPatient.PatientContact.Phone != "")//null)
                    {
                        oSqlParameter.Value = objPatient.PatientContact.Phone;
                    }
                    else
                    {
                        oSqlParameter.Value = "";
                    }
                    //oSqlParameter.Value = objPatient.PatientContact.Phone;
                    oSQLCommand.Parameters.Add(oSqlParameter);
                    oSqlParameter = null;

                    oSqlParameter = new SqlParameter();
                    oSqlParameter.ParameterName = "@sPatientPhQualifier";
                    oSqlParameter.SqlDbType = SqlDbType.VarChar;
                    oSqlParameter.Direction = ParameterDirection.Input;
                    if (objPatient.PatientContact.PhoneQualifier != "")//null)
                    {
                        oSqlParameter.Value = objPatient.PatientContact.PhoneQualifier;
                    }
                    else
                    {
                        oSqlParameter.Value = "";
                    }
                    // oSqlParameter.Value = objPatient.PatientContact.PhoneQualifier;
                    oSQLCommand.Parameters.Add(oSqlParameter);
                    oSqlParameter = null;


                    oSqlParameter = new SqlParameter();
                    oSqlParameter.ParameterName = "@sDrugDesciption";
                    oSqlParameter.SqlDbType = SqlDbType.VarChar;
                    oSqlParameter.Direction = ParameterDirection.Input;
                    if (objPatient.Medication[i].MedicationDrug.DrugName != "")//null)
                    {
                        oSqlParameter.Value = objPatient.Medication[i].MedicationDrug.DrugName;
                    }
                    else
                    {
                        oSqlParameter.Value = "";
                    }
                    // oSqlParameter.Value = objPatient.Medication[i].MedicationDrug.DrugName;
                    oSQLCommand.Parameters.Add(oSqlParameter);
                    oSqlParameter = null;

                    oSqlParameter = new SqlParameter();
                    oSqlParameter.ParameterName = "@sDrugCode";
                    oSqlParameter.SqlDbType = SqlDbType.VarChar;
                    oSqlParameter.Direction = ParameterDirection.Input;
                    if (objPatient.Medication[i].MedicationDrug.NDCCode != "")//null)
                    {
                        oSqlParameter.Value = objPatient.Medication[i].MedicationDrug.NDCCode;
                    }
                    else
                    {
                        oSqlParameter.Value = "";
                    }
                    //oSqlParameter.Value =  objPatient.Medication[i].MedicationDrug.NDCCode;
                    oSQLCommand.Parameters.Add(oSqlParameter);
                    oSqlParameter = null;


                    oSqlParameter = new SqlParameter();
                    oSqlParameter.ParameterName = "@sDrugCodeQualifier";
                    oSqlParameter.SqlDbType = SqlDbType.VarChar;
                    oSqlParameter.Direction = ParameterDirection.Input;
                    if (objPatient.Medication[i].MedicationDrug.ProductCodeQualifier != "")//null)
                    {
                        oSqlParameter.Value = objPatient.Medication[i].MedicationDrug.ProductCodeQualifier;
                    }
                    else
                    {
                        oSqlParameter.Value = "";
                    }
                    // oSqlParameter.Value = objPatient.Medication[i].MedicationDrug.ProductCodeQualifier;
                    oSQLCommand.Parameters.Add(oSqlParameter);
                    oSqlParameter = null;


                    oSqlParameter = new SqlParameter();
                    oSqlParameter.ParameterName = "@sDrugQuantityQualifier";
                    oSqlParameter.SqlDbType = SqlDbType.VarChar;
                    oSqlParameter.Direction = ParameterDirection.Input;
                    if (objPatient.Medication[i].MedicationDrug.QuantityQualifier != "")//null)
                    {
                        oSqlParameter.Value = objPatient.Medication[i].MedicationDrug.QuantityQualifier;
                    }
                    else
                    {
                        oSqlParameter.Value = "";
                    }
                    //oSqlParameter.Value = objPatient.Medication[i].MedicationDrug.QuantityQualifier;
                    oSQLCommand.Parameters.Add(oSqlParameter);
                    oSqlParameter = null;

                    oSqlParameter = new SqlParameter();
                    oSqlParameter.ParameterName = "@sDrugQuantityValue";
                    oSqlParameter.SqlDbType = SqlDbType.VarChar;
                    oSqlParameter.Direction = ParameterDirection.Input;
                    if (objPatient.Medication[i].MedicationDrug.QuantityValue != "")//null)
                    {
                        oSqlParameter.Value = objPatient.Medication[i].MedicationDrug.QuantityValue;
                    }
                    else
                    {
                        oSqlParameter.Value = "";
                    }
                    //oSqlParameter.Value = objPatient.Medication[i].MedicationDrug.QuantityValue;
                    oSQLCommand.Parameters.Add(oSqlParameter);
                    oSqlParameter = null;

                    oSqlParameter = new SqlParameter();
                    oSqlParameter.ParameterName = "@sDrugCodeListQualifier";
                    oSqlParameter.SqlDbType = SqlDbType.VarChar;
                    oSqlParameter.Direction = ParameterDirection.Input;

                    if (objPatient.Medication[i].MedicationDrug.CodeListQualifier != "")//null)
                    {
                        oSqlParameter.Value = objPatient.Medication[i].MedicationDrug.CodeListQualifier;
                    }
                    else
                    {
                        oSqlParameter.Value = "";
                    }
                    //oSqlParameter.Value = objPatient.Medication[i].MedicationDrug.CodeListQualifier;
                    oSQLCommand.Parameters.Add(oSqlParameter);
                    oSqlParameter = null;

                    oSqlParameter = new SqlParameter();
                    oSqlParameter.ParameterName = "@sDrugDaysSupply";
                    oSqlParameter.SqlDbType = SqlDbType.VarChar;
                    oSqlParameter.Direction = ParameterDirection.Input;

                    if (objPatient.Medication[i].MedicationDrug.DaysSupply != "")//null)
                    {
                        oSqlParameter.Value = objPatient.Medication[i].MedicationDrug.DaysSupply;
                    }
                    else
                    {
                        oSqlParameter.Value = "";
                    }
                    //oSqlParameter.Value = objPatient.Medication[i].MedicationDrug.DaysSupply;
                    oSQLCommand.Parameters.Add(oSqlParameter);
                    oSqlParameter = null;

                    oSqlParameter = new SqlParameter();
                    oSqlParameter.ParameterName = "@sDrugLastFillDate";
                    oSqlParameter.SqlDbType = SqlDbType.DateTime;
                    oSqlParameter.Direction = ParameterDirection.Input;
                    
                    oSqlParameter.Value = objPatient.Medication[i].MedicationDrug.LastFillDate;
                    oSQLCommand.Parameters.Add(oSqlParameter);
                    oSqlParameter = null;

                    oSqlParameter = new SqlParameter();
                    oSqlParameter.ParameterName = "@sDrugStrength";
                    oSqlParameter.SqlDbType = SqlDbType.VarChar;
                    oSqlParameter.Direction = ParameterDirection.Input;
                    if (objPatient.Medication[i].MedicationDrug.Strength != "")//null)
                    {
                        oSqlParameter.Value = objPatient.Medication[i].MedicationDrug.Strength;
                    }
                    else
                    {
                        oSqlParameter.Value = "";
                    }
                    //oSqlParameter.Value = objPatient.Medication[i].MedicationDrug.Strength;
                    oSQLCommand.Parameters.Add(oSqlParameter);
                    oSqlParameter = null;


                    oSqlParameter = new SqlParameter();
                    oSqlParameter.ParameterName = "@sDrugDuration";
                    oSqlParameter.SqlDbType = SqlDbType.VarChar;
                    oSqlParameter.Direction = ParameterDirection.Input;
                    oSqlParameter.Value = "";//'objPatient.Medication[i].MedicationDrug.;
                    oSQLCommand.Parameters.Add(oSqlParameter);
                    oSqlParameter = null;

                    oSqlParameter = new SqlParameter();
                    oSqlParameter.ParameterName = "@sDrugPriorAuthorizationQualifier";
                    oSqlParameter.SqlDbType = SqlDbType.VarChar;
                    oSqlParameter.Direction = ParameterDirection.Input;
                    if (objPatient.Medication[i].MedicationDrug.PriorAuthorizationQualifier != "")//null)
                    {
                        oSqlParameter.Value = objPatient.Medication[i].MedicationDrug.PriorAuthorizationQualifier;
                    }
                    else
                    {
                        oSqlParameter.Value = "";
                    }
                    // oSqlParameter.Value = objPatient.Medication[i].MedicationDrug.PriorAuthorizationQualifier;
                    oSQLCommand.Parameters.Add(oSqlParameter);
                    oSqlParameter = null;

                    oSqlParameter = new SqlParameter();
                    oSqlParameter.ParameterName = "@sDrugPriorAuthorizationValue";
                    oSqlParameter.SqlDbType = SqlDbType.VarChar;
                    oSqlParameter.Direction = ParameterDirection.Input;
                    if (objPatient.Medication[i].MedicationDrug.PriorAuthorizationValue != "")//null)
                    {
                        oSqlParameter.Value = objPatient.Medication[i].MedicationDrug.PriorAuthorizationValue;
                    }
                    else
                    {
                        oSqlParameter.Value = "";
                    }
                    //oSqlParameter.Value = objPatient.Medication[i].MedicationDrug.PriorAuthorizationValue;
                    oSQLCommand.Parameters.Add(oSqlParameter);
                    oSqlParameter = null;

                    oSqlParameter = new SqlParameter();
                    oSqlParameter.ParameterName = "@sPayerID";
                    oSqlParameter.SqlDbType = SqlDbType.BigInt;
                    oSqlParameter.Direction = ParameterDirection.Input;
                    
                    oSqlParameter.Value = objPatient.RxH271Master.PatientId;
                    oSQLCommand.Parameters.Add(oSqlParameter);
                    oSqlParameter = null;

                    oSqlParameter = new SqlParameter();
                    oSqlParameter.ParameterName = "@sPayerName";
                    oSqlParameter.SqlDbType = SqlDbType.VarChar;
                    oSqlParameter.Direction = ParameterDirection.Input;
                    if (objPatient.RxH271Master.PayerName != "")//null)
                    {
                        oSqlParameter.Value = objPatient.RxH271Master.PayerName;
                    }
                    else
                    {
                        oSqlParameter.Value = "";
                    }
                    // oSqlParameter.Value = objPatient.RxH271Master.PayerName;
                    oSQLCommand.Parameters.Add(oSqlParameter);
                    oSqlParameter = null;

                    oSqlParameter = new SqlParameter();
                    oSqlParameter.ParameterName = "@sCardholderID";
                    oSqlParameter.SqlDbType = SqlDbType.VarChar;
                    oSqlParameter.Direction = ParameterDirection.Input;
                    if (objPatient.RxH271Master.CardHolderId != "")//null)
                    {
                        oSqlParameter.Value = objPatient.RxH271Master.CardHolderId;
                    }
                    else
                    {
                        oSqlParameter.Value = "";
                    }
                    //oSqlParameter.Value = objPatient.RxH271Master.CardHolderId;
                    oSQLCommand.Parameters.Add(oSqlParameter);
                    oSqlParameter = null;

                    oSqlParameter = new SqlParameter();
                    oSqlParameter.ParameterName = "@sCardholderName";
                    oSqlParameter.SqlDbType = SqlDbType.VarChar;
                    oSqlParameter.Direction = ParameterDirection.Input;
                    if (objPatient.RxH271Master.CardHolderName != "")//null)
                    {
                        oSqlParameter.Value = objPatient.RxH271Master.CardHolderName;
                    }
                    else
                    {
                        oSqlParameter.Value = "";
                    }
                    //oSqlParameter.Value = objPatient.RxH271Master.CardHolderName;
                    oSQLCommand.Parameters.Add(oSqlParameter);
                    oSqlParameter = null;


                    oSqlParameter = new SqlParameter();
                    oSqlParameter.ParameterName = "@sCardConsent";
                    oSqlParameter.SqlDbType = SqlDbType.VarChar;
                    oSqlParameter.Direction = ParameterDirection.Input;
                    if (objPatient.RxH271Master.Consent != "")//null)
                    {
                        oSqlParameter.Value = objPatient.RxH271Master.Consent;
                    }
                    else
                    {
                        oSqlParameter.Value = "";
                    }
                    //oSqlParameter.Value = objPatient.RxH271Master.Consent;
                    oSQLCommand.Parameters.Add(oSqlParameter);
                    oSqlParameter = null;


                    oSqlParameter = new SqlParameter();
                    oSqlParameter.ParameterName = "@sEffectiveDate";
                    oSqlParameter.SqlDbType = SqlDbType.VarChar;
                    oSqlParameter.Direction = ParameterDirection.Input;
                    if (objPatient.Medication[i].MedicationDrug.EffectiveDate != "")//null)
                    {
                        oSqlParameter.Value = objPatient.Medication[i].MedicationDrug.EffectiveDate;
                    }
                    else
                    {
                        oSqlParameter.Value = "";
                    }
                    //oSqlParameter.Value = objPatient.Medication[i].MedicationDrug.EffectiveDate;
                    oSQLCommand.Parameters.Add(oSqlParameter);
                    oSqlParameter = null;


                    oSqlParameter = new SqlParameter();
                    oSqlParameter.ParameterName = "@sPBMMember";
                    oSqlParameter.SqlDbType = SqlDbType.VarChar;
                    oSqlParameter.Direction = ParameterDirection.Input;
                    if (objPatient.RxH271Master.MemberID != "")//null)
                    {
                        oSqlParameter.Value = objPatient.RxH271Master.MemberID;
                    }
                    else
                    {
                        oSqlParameter.Value = "";
                    }
                    //oSqlParameter.Value = objPatient.RxH271Master.MemberID;
                    oSQLCommand.Parameters.Add(oSqlParameter);
                    oSqlParameter = null;

                    oSqlParameter = new SqlParameter();
                    oSqlParameter.ParameterName = "@nPatientID";
                    oSqlParameter.SqlDbType = SqlDbType.BigInt;
                    oSqlParameter.Direction = ParameterDirection.Input;
                  //  if (nPatientID != null)
                    {
                        oSqlParameter.Value = nPatientID;
                    }
                    //else
                    //{
                    //    oSqlParameter.Value = "";
                    //}
                    //oSqlParameter.Value = objPatient.RxH271Master.MemberID;
                    oSQLCommand.Parameters.Add(oSqlParameter);
                    oSqlParameter = null;

                    oSqlParameter = new SqlParameter();
                    oSqlParameter.ParameterName = "@sMessageID";
                    oSqlParameter.SqlDbType = SqlDbType.VarChar;
                    oSqlParameter.Direction = ParameterDirection.Input;
                    if (objPatient.MessageHeader.MessageID != "")//null)
                    {
                        oSqlParameter.Value = objPatient.MessageHeader.MessageID;
                    }
                    else
                    {
                        oSqlParameter.Value = "";
                    }
                    oSQLCommand.Parameters.Add(oSqlParameter);
                    oSqlParameter = null;


                    //Work with database 
                    oSQLCommand.CommandType = CommandType.StoredProcedure;
                    // SQL Command Type , is Store Procedure 
                    oSQLCommand.CommandText = storedProcedureName;
                    // Set Store Procedure Name 
                    oSQLCommand.Connection = gloDBConnection;


                    _RecordAffected = oSQLCommand.ExecuteNonQuery();
                    oSQLCommand.Parameters.Clear();

                }
                if (_RecordAffected > 0)
                {
                    _blnResult = true;
                }
                return _blnResult;

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                //_ErrorMessage = objError.Message;
                return false;

            }
            finally
            {
                //SLR: Finaly oSQLCommand.Parameters.clear, free oSQLCommand and then
                if (oSQLCommand != null)
                {
                    oSQLCommand.Parameters.Clear();
                    oSQLCommand.Dispose();
                    oSQLCommand = null;
                }

            }
        }

        public bool Delete_RxH_HistoryRespDetails(Int64 nPatientID)
        {
            SqlParameter oSqlParameter;
            SqlCommand oSQLCommand = new SqlCommand();
            bool _blnResult = false;
            int _RecordAffected = 0;
            try
            {

                if (gloDBConnection.State == ConnectionState.Closed)
                {
                    _ErrorMessage = "Please check database connection";
                    return false;
                }

                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@nPatientID";
                oSqlParameter.SqlDbType = SqlDbType.BigInt;
                oSqlParameter.Direction = ParameterDirection.Input;
                oSqlParameter.Value = nPatientID;
               
                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;

                oSQLCommand.CommandType = CommandType.StoredProcedure;

                oSQLCommand.CommandText = "gsp_RxH_DeleteHistoryRespDetails";               
                oSQLCommand.Connection = gloDBConnection;
                _RecordAffected = oSQLCommand.ExecuteNonQuery();
                if (_RecordAffected > 0)
                {
                    _blnResult = true;
                }
                return _blnResult;
            }
            catch (Exception ex)
            {
               gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);               
               return false;
            }
            finally
            {
                //SLR: Finaly oSQLCommand.Parameters.clear, free oSQLCommand and then
                if (oSQLCommand != null)
                {
                    oSQLCommand.Parameters.Clear();
                    oSQLCommand.Dispose();
                    oSQLCommand = null;
                }
            }
        }

        public bool InsertRxH_HistoryHeader(string storedProcedureName, ClsPatient objPatient, Int64 nPatientID)
        {
            SqlParameter oSqlParameter;

            SqlCommand oSQLCommand = new SqlCommand();
            bool _blnResult = false;
            int _RecordAffected = 0;
            //int i = 0;
            try
            {

                //Check Connection 
                if (gloDBConnection.State == ConnectionState.Closed)
                {
                    _ErrorMessage = "Please check database connection";
                    return false;
                }

                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sTo";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                if (objPatient.MessageHeader.To != "")//null)
                {
                    oSqlParameter.Value = objPatient.MessageHeader.To;
                }
                else
                {
                    oSqlParameter.Value = "";
                }
                //oSqlParameter.Value = objPatient.MessageHeader.To;
                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;

                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sFrom";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                if (objPatient.MessageHeader.From != "")//null)
                {
                    oSqlParameter.Value = objPatient.MessageHeader.From;
                }
                else
                {
                    oSqlParameter.Value = "";
                }
                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;

                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sSentTime";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                if (objPatient.MessageHeader.SentTime != "")//null)
                {
                    oSqlParameter.Value = objPatient.MessageHeader.SentTime;
                }
                else
                {
                    oSqlParameter.Value = "";
                }
                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;

                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sMailbox";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                if (objPatient.MessageHeader.MailBox != "")//null)
                {
                    oSqlParameter.Value = objPatient.MessageHeader.MailBox;
                }
                else
                {
                    oSqlParameter.Value = "";
                }
                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;

                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sSecurityUserName";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                if (objPatient.MessageHeader.SecurityUserName != "")//null)
                {
                    oSqlParameter.Value = objPatient.MessageHeader.SecurityUserName;
                }
                else
                {
                    oSqlParameter.Value = "";
                }
                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;

                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sSecuritySenderTertiaryID";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                if (objPatient.MessageHeader.SecurityTertiaryID != "")//null)
                {
                    oSqlParameter.Value = objPatient.MessageHeader.SecurityTertiaryID;
                }
                else
                {
                    oSqlParameter.Value = "";
                }
                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;

                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sSecuritySenderSecondaryID";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                if (objPatient.MessageHeader.SecuritySecondaryID != "")//null)
                {
                    oSqlParameter.Value = objPatient.MessageHeader.SecuritySecondaryID;
                }
                else
                {
                    oSqlParameter.Value = "";
                }
                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;

                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sTestMessage";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                if (objPatient.MessageHeader.TestMessage != "")//null)
                {
                    oSqlParameter.Value = objPatient.MessageHeader.TestMessage;
                }
                else
                {
                    oSqlParameter.Value = "";
                }
                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;


                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sSender";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                if (objPatient.MessageHeader.TestMessage != "")//null)
                {
                    oSqlParameter.Value = objPatient.MessageHeader.Sender;
                }
                else
                {
                    oSqlParameter.Value = "";
                }
                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;

                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sReceiver";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                if (objPatient.MessageHeader.TestMessage != "")//null)
                {
                    oSqlParameter.Value = objPatient.MessageHeader.SecurityReceiverSecID;
                }
                else
                {
                    oSqlParameter.Value = "";
                }
                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;

                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sMessageID";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                if (objPatient.MessageHeader.MessageID != "")//null)
                {
                    oSqlParameter.Value = objPatient.MessageHeader.MessageID;
                }
                else
                {
                    oSqlParameter.Value = "";
                }
                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;


                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@nPatientID";
                oSqlParameter.SqlDbType = SqlDbType.BigInt;
                oSqlParameter.Direction = ParameterDirection.Input;
            //    if (objPatient.PatientID != null)
                {
                    oSqlParameter.Value = nPatientID;
                }
                //else
                //{
                //    oSqlParameter.Value = "";
                //}
                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;

                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@dtdateTimeStamp";
                oSqlParameter.SqlDbType = SqlDbType.DateTime;
                oSqlParameter.Direction = ParameterDirection.Input;
                
                oSqlParameter.Value = DateTime.Now;
                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;

                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sMessageDescription";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                if (objPatient.MessageHeader.MessageDescription != "")//null)
                {
                    oSqlParameter.Value = objPatient.MessageHeader.MessageDescription;
                }
                else
                {
                    oSqlParameter.Value = "";
                }
                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;

                //Work with database 
                oSQLCommand.CommandType = CommandType.StoredProcedure;
                // SQL Command Type , is Store Procedure 
                oSQLCommand.CommandText = storedProcedureName;
                // Set Store Procedure Name 
                oSQLCommand.Connection = gloDBConnection;


                _RecordAffected = oSQLCommand.ExecuteNonQuery();
                if (_RecordAffected > 0)
                {
                    _blnResult = true;
                }
                return _blnResult;
            }

            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                //_ErrorMessage = objError.Message;
                return false;

            }
            finally
            {
                //SLR: Finaly oSQLCommand.Parameters.clear, free oSQLCommand and then
                if (oSQLCommand != null)
                {
                    oSQLCommand.Parameters.Clear();
                    oSQLCommand.Dispose();
                    oSQLCommand = null;
                }

            }


        }

        public bool InsertRxH_HistoryRequestDetails(string storedProcedureName, ClsPatient objPatient)
        {
            SqlParameter oSqlParameter;

            SqlCommand oSQLCommand = new SqlCommand();
            
            bool _blnResult = false;
            int _RecordAffected = 0;
            //int i = 0;
            try
            {

                //Check Connection 
                if (gloDBConnection.State == ConnectionState.Closed)
                {
                    _ErrorMessage = "Please check database connecion";
                    return false;
                }

                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sPrescriberDEANo";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                if (objPatient.Provider.ProviderDEA != "")//null
                {
                    oSqlParameter.Value = objPatient.Provider.ProviderDEA;
                }
                else
                {
                    oSqlParameter.Value = "";
                }
                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;

                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sPrescriberFirstName";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                if (objPatient.Provider.ProviderFirstName != "")//null
                {
                    oSqlParameter.Value = objPatient.Provider.ProviderFirstName;
                }
                else
                {
                    oSqlParameter.Value = "";
                }
                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;


                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sPrescriberMiddleName";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                if (objPatient.Provider.ProviderMiddleName != "")//null
                {
                    oSqlParameter.Value = objPatient.Provider.ProviderMiddleName;
                }
                else
                {
                    oSqlParameter.Value = "";
                }

                //oSqlParameter.Value = objPatient.Provider.ProviderMiddleName;
                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;

                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sPrescriberLastName";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                if (objPatient.Provider.ProviderLastName != "")//null
                {
                    oSqlParameter.Value = objPatient.Provider.ProviderLastName;
                }
                else
                {
                    oSqlParameter.Value = "";
                }
                // oSqlParameter.Value = objPatient.Provider.ProviderLastName;
                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;

                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sPrescriberAddressLine1";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                if (objPatient.Provider.ProviderAddress.AddressLine1 != "")//null
                {
                    oSqlParameter.Value = objPatient.Provider.ProviderAddress.AddressLine1;
                }
                else
                {
                    oSqlParameter.Value = "";
                }
                // oSqlParameter.Value = objPatient.Provider.ProviderAddress.AddressLine1;
                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;

                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sPrescriberAddressLine2";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                if (objPatient.Provider.ProviderAddress.AddressLine2 != "")//null
                {
                    oSqlParameter.Value = objPatient.Provider.ProviderAddress.AddressLine2;
                }
                else
                {
                    oSqlParameter.Value = "";
                }

                //oSqlParameter.Value = objPatient.Provider.ProviderAddress.AddressLine2;
                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;

                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sPrescriberState";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;

                if (objPatient.Provider.ProviderAddress.State != "")//null
                {
                    oSqlParameter.Value = objPatient.Provider.ProviderAddress.State;
                }
                else
                {
                    oSqlParameter.Value = "";
                }
                //oSqlParameter.Value = objPatient.Provider.ProviderAddress.State;
                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;


                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sPrescriberZip";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                if (objPatient.Provider.ProviderAddress.Zip != "")//null
                {
                    oSqlParameter.Value = objPatient.Provider.ProviderAddress.Zip;
                }
                else
                {
                    oSqlParameter.Value = "";
                }

                //oSqlParameter.Value = objPatient.Provider.ProviderAddress.Zip;
                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;


                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sPrescriberEmail";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                if (objPatient.Provider.ProviderContactDtl.Email != "")//null
                {
                    oSqlParameter.Value = objPatient.Provider.ProviderContactDtl.Email;
                }
                else
                {
                    oSqlParameter.Value = "";
                }
                //oSqlParameter.Value = objPatient.Provider.ProviderContactDtl.Email;
                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;


                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sPrescriberPhNo";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                if (objPatient.Provider.ProviderContactDtl.Phone != "")//null
                {
                    oSqlParameter.Value = objPatient.Provider.ProviderContactDtl.Phone;
                }
                else
                {
                    oSqlParameter.Value = "";
                }
                //oSqlParameter.Value = objPatient.Provider.ProviderContactDtl.Phone;
                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;

                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sPrescriberPhQualifier";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                if (objPatient.Provider.ProviderContactDtl.PhoneQualifier != "")//null
                {
                    oSqlParameter.Value = objPatient.Provider.ProviderContactDtl.PhoneQualifier;
                }
                else
                {
                    oSqlParameter.Value = "";
                }
                // oSqlParameter.Value = objPatient.Provider.ProviderContactDtl.PhoneQualifier;
                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;

                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sPatientRelationship";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                if (objPatient.RxH271Master.RelationshipCode != "")//null
                {
                    oSqlParameter.Value = objPatient.RxH271Master.RelationshipCode;
                }
                else
                {
                    oSqlParameter.Value = "";
                }
                // oSqlParameter.Value = objPatient.RxH271Master.RelationshipCode;
                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;

                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sPatientFirstName";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                if (objPatient.FirstName != "")//null
                {
                    oSqlParameter.Value = objPatient.FirstName;
                }
                else
                {
                    oSqlParameter.Value = "";
                }
                // oSqlParameter.Value = objPatient.FirstName;
                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;

                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sPatientMiddleName";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;

                if (objPatient.MiddleName != "")//null
                {
                    oSqlParameter.Value = objPatient.MiddleName;
                }
                else
                {
                    oSqlParameter.Value = "";
                }
                //oSqlParameter.Value = objPatient.MiddleName;
                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;

                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sPatientLastName";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;

                if (objPatient.LastName != "")//null
                {
                    oSqlParameter.Value = objPatient.LastName;
                }
                else
                {
                    oSqlParameter.Value = "";
                }
                //oSqlParameter.Value = objPatient.LastName;
                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;

                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sPatientGender";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                if (objPatient.Gender != "")//null
                {
                    oSqlParameter.Value = objPatient.Gender;
                }
                else
                {
                    oSqlParameter.Value = "";
                }
                //oSqlParameter.Value = objPatient.Gender;
                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;

                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sPatientDOB";
                oSqlParameter.SqlDbType = SqlDbType.DateTime;
                oSqlParameter.Direction = ParameterDirection.Input;
                
                oSqlParameter.Value = objPatient.DOB;
                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;


                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sPatientAddressLine1";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                if (objPatient.PatientAddress.AddressLine1 != "")//null
                {
                    oSqlParameter.Value = objPatient.PatientAddress.AddressLine1;
                }
                else
                {
                    oSqlParameter.Value = "";
                }
                // oSqlParameter.Value = objPatient.PatientAddress.AddressLine1;
                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;

                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sPatientAddressLine2";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                if (objPatient.PatientAddress.AddressLine2 != "")//null
                {
                    oSqlParameter.Value = objPatient.PatientAddress.AddressLine2;
                }
                else
                {
                    oSqlParameter.Value = "";
                }
                //oSqlParameter.Value = objPatient.PatientAddress.AddressLine2;
                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;

                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sPatientCity";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                if (objPatient.PatientAddress.City != "")//null)
                {
                    oSqlParameter.Value = objPatient.PatientAddress.City;
                }
                else
                {
                    oSqlParameter.Value = "";
                }
                //oSqlParameter.Value = objPatient.PatientAddress.City;
                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;

                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sPatientState";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;

                if (objPatient.PatientAddress.State != "")//null)
                {
                    oSqlParameter.Value = objPatient.PatientAddress.State;
                }
                else
                {
                    oSqlParameter.Value = "";
                }
                //oSqlParameter.Value = objPatient.PatientAddress.State;
                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;

                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sPatientZip";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                if (objPatient.PatientAddress.Zip != "")//null)
                {
                    oSqlParameter.Value = objPatient.PatientAddress.Zip;
                }
                else
                {
                    oSqlParameter.Value = "";
                }
                //oSqlParameter.Value = objPatient.PatientAddress.Zip;
                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;



                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sPatientEmail";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                //oSqlParameter.Value = objPatient.PatientContact.Email;
                if (objPatient.PatientContact.Email != "")//null)
                {
                    oSqlParameter.Value = objPatient.PatientContact.Email;
                }
                else
                {
                    oSqlParameter.Value = "";
                }
                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;

                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sPatientPhNo";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                if (objPatient.PatientContact.Phone != "")//null)
                {
                    oSqlParameter.Value = objPatient.PatientContact.Phone;
                }
                else
                {
                    oSqlParameter.Value = "";
                }
                //oSqlParameter.Value = objPatient.PatientContact.Phone;
                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;


                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sPayerID";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                
                oSqlParameter.Value = objPatient.RxH271Master.PayerParticipantId;
                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;

                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sPayerName";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                if (objPatient.RxH271Master.PayerName != "")//null)
                {
                    oSqlParameter.Value = objPatient.RxH271Master.PayerName;
                }
                else
                {
                    oSqlParameter.Value = "";
                }
                // oSqlParameter.Value = objPatient.RxH271Master.PayerName;
                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;

                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sCardholderID";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                if (objPatient.RxH271Master.CardHolderId != "")//null)
                {
                    oSqlParameter.Value = objPatient.RxH271Master.CardHolderId;
                }
                else
                {
                    oSqlParameter.Value = "";
                }
                //oSqlParameter.Value = objPatient.RxH271Master.CardHolderId;
                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;

                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sCardholderName";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                if (objPatient.RxH271Master.CardHolderName != "")//null)
                {
                    oSqlParameter.Value = objPatient.RxH271Master.CardHolderName;
                }
                else
                {
                    oSqlParameter.Value = "";
                }
                //oSqlParameter.Value = objPatient.RxH271Master.CardHolderName;
                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;


                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sCardConsent";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                if (objPatient.RxH271Master.Consent != "")//null)
                {
                    oSqlParameter.Value = objPatient.RxH271Master.Consent;
                }
                else
                {
                    oSqlParameter.Value = "";
                }
                //oSqlParameter.Value = objPatient.RxH271Master.Consent;
                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;


                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sGroupID";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                if (objPatient.RxH271Master.GroupId != "")//null)
                {
                    oSqlParameter.Value = objPatient.RxH271Master.GroupId;
                }
                else
                {
                    oSqlParameter.Value = "";
                }

                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;


                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sPBMMember";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;
                if (objPatient.RxH271Master.MemberID != "")//null)
                {
                    oSqlParameter.Value = objPatient.RxH271Master.MemberID;
                }
                else
                {
                    oSqlParameter.Value = "";
                }
                //oSqlParameter.Value = objPatient.RxH271Master.MemberID;
                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;


                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@nPatientID";
                oSqlParameter.SqlDbType = SqlDbType.BigInt;
                oSqlParameter.Direction = ParameterDirection.Input;
        //        if (objPatient.PatientID != null)
                {
                    oSqlParameter.Value = objPatient.PatientID;
                }
                //else
                //{
                //    oSqlParameter.Value = "";
                //}
                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;

                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sMessageID";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;

                oSqlParameter.Value = DateTime.Now.ToString("yyyyMMddhhmmss");

                oSQLCommand.Parameters.Add(oSqlParameter);

                oSqlParameter = null;
                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sPrescriberNPI";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;

                oSqlParameter.Value = objPatient.Provider.ProviderNPI;
                oSQLCommand.Parameters.Add(oSqlParameter);

                oSqlParameter = null;

                oSqlParameter = new SqlParameter();
                oSqlParameter.ParameterName = "@sPrescriberCity";
                oSqlParameter.SqlDbType = SqlDbType.VarChar;
                oSqlParameter.Direction = ParameterDirection.Input;

                oSqlParameter.Value = objPatient.Provider.ProviderAddress.City;
                oSQLCommand.Parameters.Add(oSqlParameter);
                oSqlParameter = null;

                //Work with database 
                oSQLCommand.CommandType = CommandType.StoredProcedure;
                // SQL Command Type , is Store Procedure 
                oSQLCommand.CommandText = storedProcedureName;
                // Set Store Procedure Name 
                oSQLCommand.Connection = gloDBConnection;


                _RecordAffected = oSQLCommand.ExecuteNonQuery();
                if (_RecordAffected > 0)
                {
                    _blnResult = true;
                }
                return _blnResult;
            }

            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                //_ErrorMessage = objError.Message;
                return false;

            }
            finally
            {
                //SLR: Finaly oSQLCommand.Parameters.clear, free oSQLCommand and then
                if (oSQLCommand != null)
                {
                    oSQLCommand.Parameters.Clear();
                    oSQLCommand.Dispose();
                    oSQLCommand = null;
                }

            }

        }

        internal long GetVisitID(string stroredProcedureName, long _PatientId, DateTime VisitDate)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(ClsgloRxHubGeneral.ConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            long _VisitId = 0;
        
           
            try
            {

                if (_PatientId > 0)
                {
                    oDB.Connect(false);

                    oParameters.Add("@PatientID", _PatientId, ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@VisitDate", VisitDate, ParameterDirection.Input, SqlDbType.DateTime);//////pass visit date as medication date
                   
                   string strVisitId = oDB.ExecuteScalar(stroredProcedureName, oParameters).ToString();
                   oDB.Disconnect();

                   if (strVisitId == "")
                   {
                       return 0;
                   }
                   else
                   {
                       _VisitId = Convert.ToInt64(strVisitId);

                   }

                                    
                   
                  
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                return 0;
            }
            finally
            {
                //SLR: Finaly free oParameters, odb
                if (oParameters != null)
                {                    
                    oParameters.Dispose();
                    oParameters = null;
                }
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                    oDB = null;
                }

            }
            return _VisitId;

        }

        internal long InsertVisit(string stroredProcedureName, long _PatientId,DateTime VisitDate)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(ClsgloRxHubGeneral.ConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            long _VisitId = 0;
            object _internalresult = null;
            int _RetVal = 0;
            try
            {

                if (_PatientId > 0)
                {
                    oDB.Connect(false);

                    oParameters.Add("@nPatientID", _PatientId, ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@dtVisitdate", VisitDate, ParameterDirection.Input, SqlDbType.DateTime);//////pass visit date as medication date
                    oParameters.Add("@AppointmentID", 1, ParameterDirection.Input, SqlDbType.BigInt);

                    long _MachineId = ClsgloRxHubGeneral.GetPrefixTransactionID(_PatientId);
                    oParameters.Add("@MachineID", _MachineId, ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@VisitID", 0, ParameterDirection.Output, SqlDbType.BigInt);
                    oParameters.Add("@flag", 0, ParameterDirection.Input, SqlDbType.Int);

                    _RetVal = oDB.Execute(stroredProcedureName, oParameters, out _internalresult);

                    //_internalresult = oDB.ExecuteScalar(stroredProcedureName, oParameters,);
                    if (_internalresult != null)
                    {
                        if (_internalresult.ToString() != null)
                        {
                            if (_internalresult.GetType() != typeof(System.DBNull))
                            {
                                if (_internalresult.ToString() != "")
                                {
                                    _VisitId = Convert.ToInt64(_internalresult);
                                }
                            }
                        }
                    }
                    oDB.Disconnect();
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                return 0;
            }
            finally
            {
                //SLR: Finaly free oParameters, odb, internalresult and then
                if (oParameters != null)
                {
                    oParameters.Dispose();
                    oParameters = null;
                }
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                    oDB = null;
                }
                _internalresult = null;
            }
            return _VisitId;

        }

        public bool DeleteRxH_Table(string stroredProcedureName, long _PatientId)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(ClsgloRxHubGeneral.ConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            int _RetVal = 0;
            try
            {

                if (_PatientId > 0)
                {
                    oDB.Connect(false);

                    oParameters.Add("@nPatientID", _PatientId, ParameterDirection.Input, SqlDbType.BigInt);

                    _RetVal = oDB.Execute(stroredProcedureName, oParameters);

                    oDB.Disconnect();
                    return true;

                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                oDB.Disconnect();
                return false;
            }
            finally
            {
                //SLR: Finaly free oParameters, odb
                if (oParameters != null)
                {
                    oParameters.Dispose();
                    oParameters = null;
                }
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                    oDB = null;
                }
            }
            return true;

        }

        public DataTable GetMedicationForDate(Int64 nPatientID, DateTime MedicationDate)
        {
            try
            {
                Connect(ClsgloRxHubGeneral.ConnectionString);

                //Check Connection 
                if (gloDBConnection.State == ConnectionState.Closed)
                {
                    _ErrorMessage = "Please check database connecion";
                    //return false;
                }


                string strQuery = "   SELECT  distinct    h.nMedicationID, h.nVisitID, h.nPatientID, h.sMedication, ISNULL(h.sDosage, '') AS sDosage, ISNULL(h.sRoute, '') AS sRoute, ISNULL(h.sFrequency, '') "
                      + " AS sFrequency, ISNULL(h.sDuration, '') AS sDuration, h.dtStartDate, h.dtEndDate, h.dtMedicationDate, ISNULL(h.sAmount, '') AS sAmount, ISNULL(h.sStatus, '') "
                      + " AS sStatus, ISNULL(h.sReason, '') AS sReason, ISNULL(h.nddid, 0) AS nddid, ISNULL(h.sUserName, '') AS sLoginName, ISNULL(h.nPrescriptionID, 0) "
                      + " AS nPrescriptionID, ISNULL(h.sRenewed, '') AS sRenewed, isnull(h.Rx_sRefills,'') as sRefills, isnull(h.Rx_sNotes,'') as sNotes,isnull(h.Rx_sMethod,'') as sMethod, "      
                      + " isnull(h.Rx_nProviderId,0) as nProviderId,isnull(h.Rx_nPharmacyId,0) as nPharmacyId,isnull(h.Rx_sNCPDPID,'') as sNCPDPID,isnull(h.Rx_nContactID,0) as nContactID,isnull(h.Rx_sName,'') as sName , "      
                      + " isnull(h.Rx_sPrescriberNotes,'') as sPrescriberNotes,isnull(h.Rx_eRxStatus,'') as eRxStatus,isnull(h.sNDCCode,'') as NDCCode,  "     
                      + " isnull(h.nIsNarcotic,0) as IsNarcotic,isnull(h.sDrugForm,0) as DrugForm,isnull(h.sStrengthUnit,0) as StrengthUnit,isnull(h.sPBMSourceName,'') as PBMName, isnull(h.Rx_bMaySubstitute,1) as Rx_bMaySubstitute "          
                      + " FROM         Medication AS h INNER JOIN "            
                      + " Visits AS v ON h.nVisitID = v.nVisitID LEFT OUTER JOIN  Prescription ON h.nPrescriptionID = Prescription.nPrescriptionID "
                      + " WHERE     (h.nPatientID = " + nPatientID + ") AND (CONVERT(datetime, CONVERT(varchar(50), DATEPART(mm, v.dtVisitDate)) + '/' + CONVERT(varchar(50), DATEPART(dd,v.dtVisitDate)) + '/' + CONVERT(varchar(50), DATEPART(yy, v.dtVisitDate))) = '" + MedicationDate + "')  order by h.dtmedicationdate ";
                
                SqlCommand cmd = new SqlCommand(strQuery, gloDBConnection);
                DataTable dt = new DataTable();
                SqlDataAdapter adpt = new SqlDataAdapter(cmd);
                adpt.Fill(dt);
                //SLR: Finaly free adpt, cmd, 
                adpt.Dispose();
                adpt = null;
                cmd.Parameters.Clear();
                cmd.Dispose();
                cmd = null;

                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        return dt;
                    }
                    else
                    {
                        return null;
                    }

                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                return null;

            }
            //return collection

            
        }

        public bool GetReuestDetails(DateTime StartDate, DateTime EndDate)
        {

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(ClsgloRxHubGeneral.ConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            int Result = 0;
            object _internalresult = null;
            int _RetVal = 0;
            try
            {


                oDB.Connect(false);
                oParameters.Add("@StartDate", StartDate, ParameterDirection.Input, SqlDbType.DateTime);
                oParameters.Add("@ENDate", EndDate, ParameterDirection.Input, SqlDbType.DateTime);
                oParameters.Add("@Result", 0, ParameterDirection.InputOutput, SqlDbType.Bit);

                _RetVal = oDB.Execute("gsp_RxHValidateRequest", oParameters, out _internalresult);

                //_internalresult = oDB.ExecuteScalar(stroredProcedureName, oParameters,);
               
                if (_internalresult != null )
                {
                    if (_internalresult.ToString() != null)
                    {
                        if (_internalresult.GetType() != typeof(System.DBNull))
                        {
                            if (_internalresult.ToString() != "")
                            {
                                Result = Convert.ToInt16(_internalresult);
                            }
                        }
                    }
                }
                oDB.Disconnect();

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                return false;
            }
            finally
            {
                //SLR: Finaly free oParameters, odb, internalresult and then
                if (oParameters != null)
                {
                    oParameters.Dispose();
                    oParameters = null;
                }
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                    oDB = null;
                }
                _internalresult = null;
            }
            if (Result == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool ISPBMExist(Int64 PatientID)
        {

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(ClsgloRxHubGeneral.ConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            int Result = 0;
            object _internalresult = null;
            int _RetVal = 0;
            try
            {


                oDB.Connect(false);
                oParameters.Add("@nPatientID", PatientID, ParameterDirection.Input, SqlDbType.BigInt);               
                oParameters.Add("@Result", 0, ParameterDirection.InputOutput, SqlDbType.Bit);

                _RetVal = oDB.Execute("gsp_ISValidPBMExist", oParameters, out _internalresult);

                //_internalresult = oDB.ExecuteScalar(stroredProcedureName, oParameters,);

                if (_internalresult != null)
                {
                    if (_internalresult.ToString() != null)
                    {
                        if (_internalresult.GetType() != typeof(System.DBNull))
                        {
                            if (_internalresult.ToString() != "")
                            {
                                Result = Convert.ToInt16(_internalresult);
                            }
                        }
                    }
                }
                oDB.Disconnect();

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                return false;
            }
            finally
            {
                //SLR: Finaly free oParameters, odb, internalresult and then
                if (oParameters != null)
                {
                    oParameters.Dispose();
                    oParameters = null;
                }
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                    oDB = null;
                }
                _internalresult = null;
            }
            if (Result == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public bool InsertRxH_Medication(string storedProcedureName, ClsPatient objPatient, Int64 nPatientID)
        {
            SqlParameter oSqlParameter;
            SqlCommand oSQLCommand = new SqlCommand();
            
            bool _blnResult = false;
            int _RecordAffected = 0;
            int i = 0;
            long _VisitId = 0;

            StringBuilder _strnoNDC = new StringBuilder();
            Boolean _blnNdcNotFound = false;
            try
            {


                //Check Connection 
                if (gloDBConnection.State == ConnectionState.Closed)
                {
                    _ErrorMessage = "Please check database connecion";
                    return false;
                }

                _strnoNDC.Append("Following NDC were not found against the database and so will not be saved");
                _strnoNDC.Append(Environment.NewLine);

                ////assign visit date as the Last fill date
                _VisitId = GetVisitID("gsp_GetVisitID", nPatientID, DateTime.Now);
                if (_VisitId == 0)
                {
                    _VisitId = InsertVisit("gsp_InsertVisits", nPatientID, objPatient.Medication[i].MedicationDrug.LastFillDate);
                }

                //_VisitId = InsertVisit("gsp_InsertVisits", nPatientID);

                for (i = 0; i <= objPatient.Medication.Count - 1; i++)
                {

                    //bool retval = CheckForUniqueMedication(objPatient.Medication[i].MedicationDrug.MedVisitId, objPatient,"","Active");

                    string sppid = "";
                    sppid = objPatient.Medication[i].MedicationDrug.NDCCode;
                    Connect(ClsgloRxHubGeneral.ConnectionString);
                     //Change by Rahul in query for Hybrid database change on 21-10-2010
                    string strQuery = "Select isnull(strength,'') as Strength,isnull(strengthunit,'') as Strengthunit,isnull(productname,'') as Productname,isnull(dosechekunit,'') as Dosechekunit From [" + gloEMRGeneralLibrary.gloGeneral.clsgeneral.sMmwServerName + "].[" + gloEMRGeneralLibrary.gloGeneral.clsgeneral.sMmwDatabaseName + "]." + "[dbo].[mmw_drug_pack] where ppid= '" + objPatient.Medication[i].MedicationDrug.NDCCode + "'";
                    System.Data.DataTable dt = ReadPatRecord(strQuery);

                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {

                            #region "In Data present in Datatable"
                            //oPatient.PatientID = Convert.ToInt64(dt.Rows[0]["PatientID"]);

                            oSqlParameter = new SqlParameter();
                            oSqlParameter.ParameterName = "@nMedicationId";
                            oSqlParameter.SqlDbType = SqlDbType.BigInt;
                            oSqlParameter.Direction = ParameterDirection.InputOutput;
                            oSqlParameter.Value = 0;
                            oSQLCommand.Parameters.Add(oSqlParameter);
                            oSqlParameter = null;


                            //if (objPatient.Medication[i].MedicationDrug.MedVisitId != 0)
                            //{
                            //    _VisitId = objPatient.Medication[i].MedicationDrug.MedVisitId;
                            //}
                            //else
                            //{

                            //    ////assign visit date as the Last fill date
                            //    _VisitId = GetVisitID("gsp_GetVisitID", nPatientID, objPatient.Medication[i].MedicationDrug.LastFillDate);
                            //    if (_VisitId == 0)
                            //    {
                            //        _VisitId = InsertVisit("gsp_InsertVisits", nPatientID, objPatient.Medication[i].MedicationDrug.LastFillDate);
                            //    }

                            //}


                            oSqlParameter = new SqlParameter();
                            oSqlParameter.ParameterName = "@nVisitID";
                            oSqlParameter.SqlDbType = SqlDbType.BigInt;
                            oSqlParameter.Direction = ParameterDirection.Input;
                            oSqlParameter.Value = _VisitId;
                            oSQLCommand.Parameters.Add(oSqlParameter);
                            oSqlParameter = null;



                            oSqlParameter = new SqlParameter();
                            oSqlParameter.ParameterName = "@nPatientID";
                            oSqlParameter.SqlDbType = SqlDbType.BigInt;
                            oSqlParameter.Direction = ParameterDirection.Input;
                            //if (nPatientID != null)
                            {
                                oSqlParameter.Value = nPatientID;
                            }
                            //else
                            //{
                            //    oSqlParameter.Value = 0;
                            //}
                            oSQLCommand.Parameters.Add(oSqlParameter);
                            oSqlParameter = null;


                            oSqlParameter = new SqlParameter();
                            oSqlParameter.ParameterName = "@sMedication";
                            oSqlParameter.SqlDbType = SqlDbType.VarChar;
                            oSqlParameter.Direction = ParameterDirection.Input;
                            if (objPatient.Medication[i].MedicationDrug.DrugName != null)
                            {
                                oSqlParameter.Value = objPatient.Medication[i].MedicationDrug.DrugName;
                            }
                            else
                            {
                                oSqlParameter.Value = "";
                            }
                            oSQLCommand.Parameters.Add(oSqlParameter);
                            oSqlParameter = null;


                            //string sDosage1 = objPatient.Medication[i].MedicationDrug.Dosage;
                            string sDosage = dt.Rows[0]["Strength"] + " " + dt.Rows[0]["Strengthunit"];

                            oSqlParameter = new SqlParameter();
                            oSqlParameter.ParameterName = "@sDosage";
                            oSqlParameter.SqlDbType = SqlDbType.VarChar;
                            oSqlParameter.Direction = ParameterDirection.Input;
                            if (sDosage != null)
                            {
                                oSqlParameter.Value = sDosage;
                            }
                            else
                            {
                                oSqlParameter.Value = "";
                            }
                            oSQLCommand.Parameters.Add(oSqlParameter);
                            oSqlParameter = null;


                            oSqlParameter = new SqlParameter();
                            oSqlParameter.ParameterName = "@sRoute";
                            oSqlParameter.SqlDbType = SqlDbType.VarChar;
                            oSqlParameter.Direction = ParameterDirection.Input;
                            //if (objPatient.Medication[i].MedicationDrug != null)
                            //{
                            //   // oSqlParameter.Value = objPatient.Medication[i].MedicationDrug.;
                            //}
                            //else
                            //{
                            //    oSqlParameter.Value = "";
                            //}

                            oSqlParameter.Value = "";
                            oSQLCommand.Parameters.Add(oSqlParameter);
                            oSqlParameter = null;


                            oSqlParameter = new SqlParameter();
                            oSqlParameter.ParameterName = "@sFrequency";
                            oSqlParameter.SqlDbType = SqlDbType.VarChar;
                            oSqlParameter.Direction = ParameterDirection.Input;
                            //if (objPatient.Medication[i].MedicationDrug.frequency != null)
                            //{
                            //     oSqlParameter.Value = objPatient.Medication[i].MedicationDrug.frequency;
                            //}
                            //else
                            //{
                            //    oSqlParameter.Value = 0;
                            //}

                            oSqlParameter.Value = "";
                            oSQLCommand.Parameters.Add(oSqlParameter);
                            oSqlParameter = null;

                            oSqlParameter = new SqlParameter();
                            oSqlParameter.ParameterName = "@sDuration";
                            oSqlParameter.SqlDbType = SqlDbType.VarChar;
                            oSqlParameter.Direction = ParameterDirection.Input;
                            if (objPatient.Medication[i].MedicationDrug.DaysSupply != null)
                            {
                                oSqlParameter.Value = objPatient.Medication[i].MedicationDrug.DaysSupply;
                            }
                            else
                            {
                                oSqlParameter.Value = 0;
                            }

                            oSQLCommand.Parameters.Add(oSqlParameter);
                            oSqlParameter = null;

                            oSqlParameter = new SqlParameter();
                            oSqlParameter.ParameterName = "@dtStartdate";
                            oSqlParameter.SqlDbType = SqlDbType.DateTime;
                            oSqlParameter.Direction = ParameterDirection.Input;
                            oSqlParameter.Value = objPatient.Medication[i].MedicationDrug.LastFillDate;
                            oSQLCommand.Parameters.Add(oSqlParameter);
                            oSqlParameter = null;


                            DateTime dtLastFillDate = objPatient.Medication[i].MedicationDrug.LastFillDate;
                            double sDaysupply = Convert.ToDouble(objPatient.Medication[i].MedicationDrug.DaysSupply);

                            // DateTime dtEndDate =  dtLastFillDate.AddDays(sDaysupply);


                            oSqlParameter = new SqlParameter();
                            oSqlParameter.ParameterName = "@dtMedicationDate";
                            oSqlParameter.SqlDbType = SqlDbType.DateTime;
                            oSqlParameter.Direction = ParameterDirection.Input;
                            oSqlParameter.Value = DateTime.Now;//objPatient.Medication[i].MedicationDrug.LastFillDate;
                            oSQLCommand.Parameters.Add(oSqlParameter);
                            oSqlParameter = null;



                            oSqlParameter = new SqlParameter();
                            oSqlParameter.ParameterName = "@sAmount";
                            oSqlParameter.SqlDbType = SqlDbType.VarChar;
                            oSqlParameter.Direction = ParameterDirection.Input;
                            if (objPatient.Medication[i].MedicationDrug.DrugQuantityValue != null)
                            {
                                oSqlParameter.Value = objPatient.Medication[i].MedicationDrug.DrugQuantityValue;
                            }
                            else
                            {
                                oSqlParameter.Value = "";
                            }
                            oSQLCommand.Parameters.Add(oSqlParameter);
                            oSqlParameter = null;


                            oSqlParameter = new SqlParameter();
                            oSqlParameter.ParameterName = "@dtEndDate";
                            oSqlParameter.SqlDbType = SqlDbType.DateTime;
                            oSqlParameter.Direction = ParameterDirection.Input;

                            //if (objPatient.PatientID != null)
                            //{
                            //    // oSqlParameter.Value = objPatient.Medication[i].MedicationDrug.DrugName;
                            //}
                            //else
                            //{

                            int sDaysSupply = Convert.ToInt16(objPatient.Medication[i].MedicationDrug.DaysSupply);
                            if (sDaysSupply <= 31)
                            {
                                oSqlParameter.Value = dtLastFillDate.AddDays(sDaysSupply);
                            }

                            else
                            {
                                sDaysupply = sDaysupply - 31;
                                dtLastFillDate = dtLastFillDate.AddMonths(1);

                                oSqlParameter.Value = dtLastFillDate.AddDays(sDaysSupply);
                            }

                            // }
                            oSQLCommand.Parameters.Add(oSqlParameter);
                            oSqlParameter = null;

                            // oClsgloRxHubGenral=new ClsgloRxHubGeneral();

                            long nMachinID;
                            //gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(ClsgloRxHubGeneral.ConnectionString);
                            nMachinID = ClsgloRxHubGeneral.GetPrefixTransactionID(nPatientID);
                            //nMachinID = ogloRxHubGenral.GetPrefixTransactionID();

                            oSqlParameter = new SqlParameter();
                            oSqlParameter.ParameterName = "@MachineID";
                            oSqlParameter.SqlDbType = SqlDbType.BigInt;
                            oSqlParameter.Direction = ParameterDirection.Input;
                            oSqlParameter.Value = nMachinID;
                            oSQLCommand.Parameters.Add(oSqlParameter);
                            oSqlParameter = null;

                            oSqlParameter = new SqlParameter();
                            oSqlParameter.ParameterName = "@sStatus";
                            oSqlParameter.SqlDbType = SqlDbType.VarChar;
                            oSqlParameter.Direction = ParameterDirection.Input;
                            //if (objPatient.PatientID != null)
                            //{
                            //    // oSqlParameter.Value = objPatient.Medication[i].MedicationDrug.DrugName;
                            //}
                            //else
                            //{
                            oSqlParameter.Value = "";
                            //}
                            oSQLCommand.Parameters.Add(oSqlParameter);
                            oSqlParameter = null;

                            oSqlParameter = new SqlParameter();
                            oSqlParameter.ParameterName = "@sReason";
                            oSqlParameter.SqlDbType = SqlDbType.VarChar;
                            oSqlParameter.Direction = ParameterDirection.Input;
                            //if (objPatient.PatientID != null)
                            //{
                            //    // oSqlParameter.Value = objPatient.Medication[i].MedicationDrug.DrugName;
                            //}
                            //else
                            //{
                            oSqlParameter.Value = "";
                            ////}
                            oSQLCommand.Parameters.Add(oSqlParameter);
                            oSqlParameter = null;


                            oSqlParameter = new SqlParameter();
                            oSqlParameter.ParameterName = "@nDDID";
                            oSqlParameter.SqlDbType = SqlDbType.BigInt;
                            oSqlParameter.Direction = ParameterDirection.Input;
                            //if (objPatient.PatientID != null)
                            //{
                            //    // oSqlParameter.Value = objPatient.Medication[i].MedicationDrug.DrugName;
                            //}
                            //else
                            //{
                            oSqlParameter.Value = 0;
                            //}
                            oSQLCommand.Parameters.Add(oSqlParameter);
                            oSqlParameter = null;


                            oSqlParameter = new SqlParameter();
                            oSqlParameter.ParameterName = "@sUserName";
                            oSqlParameter.SqlDbType = SqlDbType.VarChar;
                            oSqlParameter.Direction = ParameterDirection.Input;
                            //if (objPatient.PatientID != null)
                            //{
                            //    // oSqlParameter.Value = objPatient.Medication[i].MedicationDrug.DrugName;
                            //}
                            //else
                            //{
                            oSqlParameter.Value = gloRxHub.ClsgloRxHubGeneral.gstrRxHubLoginUser;
                            //}
                            oSQLCommand.Parameters.Add(oSqlParameter);
                            oSqlParameter = null;


                            //oSqlParameter = new SqlParameter();
                            //oSqlParameter.ParameterName = "@sUserName1";
                            //oSqlParameter.SqlDbType = SqlDbType.VarChar;
                            //oSqlParameter.Direction = ParameterDirection.Input;
                            ////if (objPatient.PatientID != null)
                            ////{
                            ////    // oSqlParameter.Value = objPatient.Medication[i].MedicationDrug.DrugName;
                            ////}
                            ////else
                            ////{
                            //oSqlParameter.Value = "";
                            ////}
                            //oSQLCommand.Parameters.Add(oSqlParameter);
                            //oSqlParameter = null;


                            //oSqlParameter = new SqlParameter();
                            //oSqlParameter.ParameterName = "@sMachineName";
                            //oSqlParameter.SqlDbType = SqlDbType.VarChar;
                            //oSqlParameter.Direction = ParameterDirection.Input;
                            ////if (objPatient.PatientID != null)
                            ////{
                            ////    // oSqlParameter.Value = objPatient.Medication[i].MedicationDrug.DrugName;
                            ////}
                            ////else
                            ////{
                            //oSqlParameter.Value = "";
                            ////}
                            //oSQLCommand.Parameters.Add(oSqlParameter);
                            //oSqlParameter = null;


                            oSqlParameter = new SqlParameter();
                            oSqlParameter.ParameterName = "@nPrescriptionID";
                            oSqlParameter.SqlDbType = SqlDbType.BigInt;
                            oSqlParameter.Direction = ParameterDirection.Input;
                            //if (objPatient.PatientID != null)
                            //{
                            //    // oSqlParameter.Value = objPatient.Medication[i].MedicationDrug.DrugName;
                            //}
                            //else
                            //{
                            oSqlParameter.Value = 0;
                            //}
                            oSQLCommand.Parameters.Add(oSqlParameter);
                            oSqlParameter = null;



                            oSqlParameter = new SqlParameter();
                            oSqlParameter.ParameterName = "@sRenewed";
                            oSqlParameter.SqlDbType = SqlDbType.VarChar;
                            oSqlParameter.Direction = ParameterDirection.Input;
                            //if (objPatient.PatientID != null)
                            //{
                            //    // oSqlParameter.Value = objPatient.Medication[i].MedicationDrug.DrugName;
                            //}
                            //else
                            //{
                            oSqlParameter.Value = "";
                            //}
                            oSQLCommand.Parameters.Add(oSqlParameter);
                            oSqlParameter = null;


                            oSqlParameter = new SqlParameter();
                            oSqlParameter.ParameterName = "@sNDCCode";
                            oSqlParameter.SqlDbType = SqlDbType.VarChar;
                            oSqlParameter.Direction = ParameterDirection.Input;
                            if (objPatient.Medication[i].MedicationDrug.NDCCode != null)
                            {
                                oSqlParameter.Value = objPatient.Medication[i].MedicationDrug.NDCCode;
                            }
                            else
                            {
                                oSqlParameter.Value = "";
                            }
                            oSQLCommand.Parameters.Add(oSqlParameter);
                            oSqlParameter = null;



                            oSqlParameter = new SqlParameter();
                            oSqlParameter.ParameterName = "@nIsNarcotic";
                            oSqlParameter.SqlDbType = SqlDbType.Int;
                            oSqlParameter.Direction = ParameterDirection.Input;
                            //if (objPatient.PatientID != null)
                            //{
                            //    // oSqlParameter.Value = objPatient.Medication[i].MedicationDrug.DrugName;
                            //}
                            //else
                            //{
                            oSqlParameter.Value = 0;
                            //}
                            oSQLCommand.Parameters.Add(oSqlParameter);
                            oSqlParameter = null;


                            string sDrugForm = Convert.ToString(dt.Rows[0]["Dosechekunit"]);

                            oSqlParameter = new SqlParameter();
                            oSqlParameter.ParameterName = "@sDrugForm";
                            oSqlParameter.SqlDbType = SqlDbType.VarChar;
                            oSqlParameter.Direction = ParameterDirection.Input;
                            if (sDrugForm != null)
                            {
                                oSqlParameter.Value = sDrugForm;
                            }
                            else
                            {
                                oSqlParameter.Value = "";
                            }
                            oSQLCommand.Parameters.Add(oSqlParameter);
                            oSqlParameter = null;

                            string sStrengthUnit = Convert.ToString(dt.Rows[0]["Strengthunit"]);
                            oSqlParameter = new SqlParameter();
                            oSqlParameter.ParameterName = "@sStrengthUnit";
                            oSqlParameter.SqlDbType = SqlDbType.VarChar;
                            oSqlParameter.Direction = ParameterDirection.Input;
                            if (sStrengthUnit != null)
                            {
                                oSqlParameter.Value = sStrengthUnit;
                            }
                            else
                            {
                                oSqlParameter.Value = "";
                            }
                            oSQLCommand.Parameters.Add(oSqlParameter);
                            oSqlParameter = null;

                            ////////////
                            ////@Rx_sRefills varchar(255), 
                            oSqlParameter = new SqlParameter();
                            oSqlParameter.ParameterName = "@Rx_sRefills";
                            oSqlParameter.SqlDbType = SqlDbType.VarChar;
                            oSqlParameter.Direction = ParameterDirection.Input;
                            if (objPatient.Medication[i].MedicationDrug.Rx_sRefills != null)
                            {
                                oSqlParameter.Value = objPatient.Medication[i].MedicationDrug.Rx_sRefills;
                            }
                            else
                            {
                                oSqlParameter.Value = "";
                            }
                            oSQLCommand.Parameters.Add(oSqlParameter);
                            oSqlParameter = null;

                            //// @Rx_sNotes varchar(1500),
                            oSqlParameter = new SqlParameter();
                            oSqlParameter.ParameterName = "@Rx_sNotes";
                            oSqlParameter.SqlDbType = SqlDbType.VarChar;
                            oSqlParameter.Direction = ParameterDirection.Input;
                            if (objPatient.Medication[i].MedicationDrug.Rx_sNotes != null)
                            {
                                oSqlParameter.Value = objPatient.Medication[i].MedicationDrug.Rx_sNotes;
                            }
                            else
                            {
                                oSqlParameter.Value = "";
                            }
                            oSQLCommand.Parameters.Add(oSqlParameter);
                            oSqlParameter = null;

                            ///@Rx_sMethod varchar(50),
                            oSqlParameter = new SqlParameter();
                            oSqlParameter.ParameterName = "@Rx_sMethod";
                            oSqlParameter.SqlDbType = SqlDbType.VarChar;
                            oSqlParameter.Direction = ParameterDirection.Input;
                            if (objPatient.Medication[i].MedicationDrug.Rx_sMethod != null)
                            {
                                oSqlParameter.Value = objPatient.Medication[i].MedicationDrug.Rx_sMethod;
                            }
                            else
                            {
                                oSqlParameter.Value = "";
                            }
                            oSQLCommand.Parameters.Add(oSqlParameter);
                            oSqlParameter = null;

                            //@Rx_bMaySubstitute bit,  
                            oSqlParameter = new SqlParameter();
                            oSqlParameter.ParameterName = "@Rx_bMaySubstitute";
                            oSqlParameter.SqlDbType = SqlDbType.Bit;
                            oSqlParameter.Direction = ParameterDirection.Input;
                            //if (objPatient.Medication[i].MedicationDrug.Rx_bMaySubstitute != null)
                            {
                                oSqlParameter.Value = objPatient.Medication[i].MedicationDrug.Rx_bMaySubstitute;
                            }
                            //else
                            //{
                            //    oSqlParameter.Value = false;
                            //}
                            oSQLCommand.Parameters.Add(oSqlParameter);
                            oSqlParameter = null;

                            ////@Rx_nDrugID numeric(18, 0), 
                            oSqlParameter = new SqlParameter();
                            oSqlParameter.ParameterName = "@Rx_nDrugID";
                            oSqlParameter.SqlDbType = SqlDbType.BigInt;
                            oSqlParameter.Direction = ParameterDirection.Input;
                       //     if (objPatient.Medication[i].MedicationDrug.Rx_nDrugID != null)
                            {
                                oSqlParameter.Value = objPatient.Medication[i].MedicationDrug.Rx_nDrugID;
                            }
                            //else
                            //{
                            //    oSqlParameter.Value = 0;
                            //}
                            oSQLCommand.Parameters.Add(oSqlParameter);
                            oSqlParameter = null;

                            //////@Rx_blnflag bit,  
                            oSqlParameter = new SqlParameter();
                            oSqlParameter.ParameterName = "@Rx_blnflag";
                            oSqlParameter.SqlDbType = SqlDbType.Bit;
                            oSqlParameter.Direction = ParameterDirection.Input;
                        //    if (objPatient.Medication[i].MedicationDrug.Rx_blnflag != null)
                            {
                                oSqlParameter.Value = objPatient.Medication[i].MedicationDrug.Rx_blnflag;
                            }
                            //else
                            //{
                            //    oSqlParameter.Value = false;
                            //}
                            oSQLCommand.Parameters.Add(oSqlParameter);
                            oSqlParameter = null;

                            ///////@Rx_sLotNo varchar(50),  
                            oSqlParameter = new SqlParameter();
                            oSqlParameter.ParameterName = "@Rx_sLotNo";
                            oSqlParameter.SqlDbType = SqlDbType.VarChar;
                            oSqlParameter.Direction = ParameterDirection.Input;
                            if (objPatient.Medication[i].MedicationDrug.Rx_sLotNo != null)
                            {
                                oSqlParameter.Value = objPatient.Medication[i].MedicationDrug.Rx_sLotNo;
                            }
                            else
                            {
                                oSqlParameter.Value = "";
                            }
                            oSQLCommand.Parameters.Add(oSqlParameter);
                            oSqlParameter = null;

                            /////// @Rx_dtExpirationdate datetime, 
                            oSqlParameter = new SqlParameter();
                            oSqlParameter.ParameterName = "@Rx_dtExpirationdate";
                            oSqlParameter.SqlDbType = SqlDbType.DateTime;
                            oSqlParameter.Direction = ParameterDirection.Input;
                            if (objPatient.Medication[i].MedicationDrug.Rx_dtExpirationdate != null)
                            {
                                oSqlParameter.Value = DBNull.Value; //DateTime.Now;///// objPatient.Medication[i].MedicationDrug.Rx_dtExpirationdate;
                            }
                            else
                            {
                                oSqlParameter.Value = DBNull.Value; //"12:00:00 AM";
                            }
                            oSQLCommand.Parameters.Add(oSqlParameter);
                            oSqlParameter = null;

                            //////@Rx_nProviderId numeric(18, 0),  
                            oSqlParameter = new SqlParameter();
                            oSqlParameter.ParameterName = "@Rx_nProviderId";
                            oSqlParameter.SqlDbType = SqlDbType.BigInt;
                            oSqlParameter.Direction = ParameterDirection.Input;
                    //        if (objPatient.Medication[i].MedicationDrug.Rx_nProviderId != null)
                            {
                                oSqlParameter.Value = objPatient.Medication[i].MedicationDrug.Rx_nProviderId;
                            }
                            //else
                            //{
                            //    oSqlParameter.Value = 0;
                            //}
                            oSQLCommand.Parameters.Add(oSqlParameter);
                            oSqlParameter = null;

                            // @Rx_sChiefComplaints varchar(255), 
                            oSqlParameter = new SqlParameter();
                            oSqlParameter.ParameterName = "@Rx_sChiefComplaints";
                            oSqlParameter.SqlDbType = SqlDbType.VarChar;
                            oSqlParameter.Direction = ParameterDirection.Input;
                            if (objPatient.Medication[i].MedicationDrug.Rx_sChiefComplaints != null)
                            {
                                oSqlParameter.Value = objPatient.Medication[i].MedicationDrug.Rx_sChiefComplaints;
                            }
                            else
                            {
                                oSqlParameter.Value = "";
                            }
                            oSQLCommand.Parameters.Add(oSqlParameter);
                            oSqlParameter = null;

                            // @Rx_sStatus varchar(50),
                            oSqlParameter = new SqlParameter();
                            oSqlParameter.ParameterName = "@Rx_sStatus";
                            oSqlParameter.SqlDbType = SqlDbType.VarChar;
                            oSqlParameter.Direction = ParameterDirection.Input;
                            if (objPatient.Medication[i].MedicationDrug.Rx_sStatus != null)
                            {
                                oSqlParameter.Value = objPatient.Medication[i].MedicationDrug.Rx_sStatus;
                            }
                            else
                            {
                                oSqlParameter.Value = "";
                            }
                            oSQLCommand.Parameters.Add(oSqlParameter);
                            oSqlParameter = null;

                            // @Rx_sRxReferenceNumber varchar(50),  
                            oSqlParameter = new SqlParameter();
                            oSqlParameter.ParameterName = "@Rx_sRxReferenceNumber";
                            oSqlParameter.SqlDbType = SqlDbType.VarChar;
                            oSqlParameter.Direction = ParameterDirection.Input;
                            if (objPatient.Medication[i].MedicationDrug.Rx_sRxReferenceNumber != null)
                            {
                                oSqlParameter.Value = objPatient.Medication[i].MedicationDrug.Rx_sRxReferenceNumber;
                            }
                            else
                            {
                                oSqlParameter.Value = "";
                            }
                            oSQLCommand.Parameters.Add(oSqlParameter);
                            oSqlParameter = null;

                            // @Rx_sRefillQualifier varchar(3), 
                            oSqlParameter = new SqlParameter();
                            oSqlParameter.ParameterName = "@Rx_sRefillQualifier";
                            oSqlParameter.SqlDbType = SqlDbType.VarChar;
                            oSqlParameter.Direction = ParameterDirection.Input;
                            if (objPatient.Medication[i].MedicationDrug.Rx_sRefillQualifier != null)
                            {
                                oSqlParameter.Value = objPatient.Medication[i].MedicationDrug.Rx_sRefillQualifier;
                            }
                            else
                            {
                                oSqlParameter.Value = "";
                            }
                            oSQLCommand.Parameters.Add(oSqlParameter);
                            oSqlParameter = null;

                            // @Rx_nPharmacyId numeric(18, 0),  
                            oSqlParameter = new SqlParameter();
                            oSqlParameter.ParameterName = "@Rx_nPharmacyId";
                            oSqlParameter.SqlDbType = SqlDbType.BigInt;
                            oSqlParameter.Direction = ParameterDirection.Input;
                     //       if (objPatient.Medication[i].MedicationDrug.Rx_nPharmacyId != null)
                            {
                                oSqlParameter.Value = objPatient.Medication[i].MedicationDrug.Rx_nPharmacyId;
                            }
                            //else
                            //{
                            //    oSqlParameter.Value = 0;
                            //}
                            oSQLCommand.Parameters.Add(oSqlParameter);
                            oSqlParameter = null;

                            // @Rx_sNCPDPID varchar(50),  
                            oSqlParameter = new SqlParameter();
                            oSqlParameter.ParameterName = "@Rx_sNCPDPID";
                            oSqlParameter.SqlDbType = SqlDbType.VarChar;
                            oSqlParameter.Direction = ParameterDirection.Input;
                            if (objPatient.Medication[i].MedicationDrug.Rx_sNCPDPID != null)
                            {
                                oSqlParameter.Value = objPatient.Medication[i].MedicationDrug.Rx_sNCPDPID;
                            }
                            else
                            {
                                oSqlParameter.Value = "";
                            }
                            oSQLCommand.Parameters.Add(oSqlParameter);
                            oSqlParameter = null;


                            // @Rx_nContactID numeric(18, 0),  
                            oSqlParameter = new SqlParameter();
                            oSqlParameter.ParameterName = "@Rx_nContactID";
                            oSqlParameter.SqlDbType = SqlDbType.BigInt;
                            oSqlParameter.Direction = ParameterDirection.Input;
                      //      if (objPatient.Medication[i].MedicationDrug.Rx_nContactID != null)
                            {
                                oSqlParameter.Value = objPatient.Medication[i].MedicationDrug.Rx_nContactID;
                            }
                            //else
                            //{
                            //    oSqlParameter.Value = 0;
                            //}
                            oSQLCommand.Parameters.Add(oSqlParameter);
                            oSqlParameter = null;

                            // @Rx_sName varchar(255),  
                            oSqlParameter = new SqlParameter();
                            oSqlParameter.ParameterName = "@Rx_sName";
                            oSqlParameter.SqlDbType = SqlDbType.VarChar;
                            oSqlParameter.Direction = ParameterDirection.Input;
                            if (objPatient.Medication[i].MedicationDrug.Rx_sName != null)
                            {
                                oSqlParameter.Value = objPatient.Medication[i].MedicationDrug.Rx_sName;
                            }
                            else
                            {
                                oSqlParameter.Value = "";
                            }
                            oSQLCommand.Parameters.Add(oSqlParameter);
                            oSqlParameter = null;

                            // @Rx_sAddressline1 varchar(255),  
                            oSqlParameter = new SqlParameter();
                            oSqlParameter.ParameterName = "@Rx_sAddressline1";
                            oSqlParameter.SqlDbType = SqlDbType.VarChar;
                            oSqlParameter.Direction = ParameterDirection.Input;
                            if (objPatient.Medication[i].MedicationDrug.Rx_sAddressline1 != null)
                            {
                                oSqlParameter.Value = objPatient.Medication[i].MedicationDrug.Rx_sAddressline1;
                            }
                            else
                            {
                                oSqlParameter.Value = "";
                            }
                            oSQLCommand.Parameters.Add(oSqlParameter);
                            oSqlParameter = null;

                            // @Rx_sAddressline2 nchar(10),
                            oSqlParameter = new SqlParameter();
                            oSqlParameter.ParameterName = "@Rx_sAddressline2";
                            oSqlParameter.SqlDbType = SqlDbType.VarChar;
                            oSqlParameter.Direction = ParameterDirection.Input;
                            if (objPatient.Medication[i].MedicationDrug.Rx_sAddressline2 != null)
                            {
                                oSqlParameter.Value = objPatient.Medication[i].MedicationDrug.Rx_sAddressline2;
                            }
                            else
                            {
                                oSqlParameter.Value = "";
                            }
                            oSQLCommand.Parameters.Add(oSqlParameter);
                            oSqlParameter = null;

                            // @Rx_sCity varchar(50),  
                            oSqlParameter = new SqlParameter();
                            oSqlParameter.ParameterName = "@Rx_sCity";
                            oSqlParameter.SqlDbType = SqlDbType.VarChar;
                            oSqlParameter.Direction = ParameterDirection.Input;
                            if (objPatient.Medication[i].MedicationDrug.Rx_sCity != null)
                            {
                                oSqlParameter.Value = objPatient.Medication[i].MedicationDrug.Rx_sCity;
                            }
                            else
                            {
                                oSqlParameter.Value = "";
                            }
                            oSQLCommand.Parameters.Add(oSqlParameter);
                            oSqlParameter = null;

                            // @Rx_sState varchar(50), 
                            oSqlParameter = new SqlParameter();
                            oSqlParameter.ParameterName = "@Rx_sState";
                            oSqlParameter.SqlDbType = SqlDbType.VarChar;
                            oSqlParameter.Direction = ParameterDirection.Input;
                            if (objPatient.Medication[i].MedicationDrug.Rx_sState != null)
                            {
                                oSqlParameter.Value = objPatient.Medication[i].MedicationDrug.Rx_sState;
                            }
                            else
                            {
                                oSqlParameter.Value = "";
                            }
                            oSQLCommand.Parameters.Add(oSqlParameter);
                            oSqlParameter = null;

                            // @Rx_sZip varchar(50),  
                            oSqlParameter = new SqlParameter();
                            oSqlParameter.ParameterName = "@Rx_sZip";
                            oSqlParameter.SqlDbType = SqlDbType.VarChar;
                            oSqlParameter.Direction = ParameterDirection.Input;
                            if (objPatient.Medication[i].MedicationDrug.Rx_sZip != null)
                            {
                                oSqlParameter.Value = objPatient.Medication[i].MedicationDrug.Rx_sZip;
                            }
                            else
                            {
                                oSqlParameter.Value = "";
                            }
                            oSQLCommand.Parameters.Add(oSqlParameter);
                            oSqlParameter = null;

                            // @Rx_sEmail varchar(50),  
                            oSqlParameter = new SqlParameter();
                            oSqlParameter.ParameterName = "@Rx_sEmail";
                            oSqlParameter.SqlDbType = SqlDbType.VarChar;
                            oSqlParameter.Direction = ParameterDirection.Input;
                            if (objPatient.Medication[i].MedicationDrug.Rx_sEmail != null)
                            {
                                oSqlParameter.Value = objPatient.Medication[i].MedicationDrug.Rx_sEmail;
                            }
                            else
                            {
                                oSqlParameter.Value = "";
                            }
                            oSQLCommand.Parameters.Add(oSqlParameter);
                            oSqlParameter = null;

                            // @Rx_sFax varchar(50),  
                            oSqlParameter = new SqlParameter();
                            oSqlParameter.ParameterName = "@Rx_sFax";
                            oSqlParameter.SqlDbType = SqlDbType.VarChar;
                            oSqlParameter.Direction = ParameterDirection.Input;
                            if (objPatient.Medication[i].MedicationDrug.Rx_sFax != null)
                            {
                                oSqlParameter.Value = objPatient.Medication[i].MedicationDrug.Rx_sFax;
                            }
                            else
                            {
                                oSqlParameter.Value = "";
                            }
                            oSQLCommand.Parameters.Add(oSqlParameter);
                            oSqlParameter = null;

                            // @Rx_sPhone varchar(50),  
                            oSqlParameter = new SqlParameter();
                            oSqlParameter.ParameterName = "@Rx_sPhone";
                            oSqlParameter.SqlDbType = SqlDbType.VarChar;
                            oSqlParameter.Direction = ParameterDirection.Input;
                            if (objPatient.Medication[i].MedicationDrug.Rx_sPhone != null)
                            {
                                oSqlParameter.Value = objPatient.Medication[i].MedicationDrug.Rx_sPhone;
                            }
                            else
                            {
                                oSqlParameter.Value = "";
                            }
                            oSQLCommand.Parameters.Add(oSqlParameter);
                            oSqlParameter = null;

                            // @Rx_sServiceLevel varchar(16),  
                            oSqlParameter = new SqlParameter();
                            oSqlParameter.ParameterName = "@Rx_sServiceLevel";
                            oSqlParameter.SqlDbType = SqlDbType.VarChar;
                            oSqlParameter.Direction = ParameterDirection.Input;
                            if (objPatient.Medication[i].MedicationDrug.Rx_sServiceLevel != null)
                            {
                                oSqlParameter.Value = objPatient.Medication[i].MedicationDrug.Rx_sServiceLevel;
                            }
                            else
                            {
                                oSqlParameter.Value = "";
                            }
                            oSQLCommand.Parameters.Add(oSqlParameter);
                            oSqlParameter = null;

                            // @Rx_sPrescriberNotes varchar(1500),  
                            oSqlParameter = new SqlParameter();
                            oSqlParameter.ParameterName = "@Rx_sPrescriberNotes";
                            oSqlParameter.SqlDbType = SqlDbType.VarChar;
                            oSqlParameter.Direction = ParameterDirection.Input;
                            if (objPatient.Medication[i].MedicationDrug.Rx_sPrescriberNotes != null)
                            {
                                oSqlParameter.Value = objPatient.Medication[i].MedicationDrug.Rx_sPrescriberNotes;
                            }
                            else
                            {
                                oSqlParameter.Value = "";
                            }
                            oSQLCommand.Parameters.Add(oSqlParameter);
                            oSqlParameter = null;

                            // @Rx_eRxStatus varchar(50),   
                            oSqlParameter = new SqlParameter();
                            oSqlParameter.ParameterName = "@Rx_eRxStatus";
                            oSqlParameter.SqlDbType = SqlDbType.VarChar;
                            oSqlParameter.Direction = ParameterDirection.Input;
                            if (objPatient.Medication[i].MedicationDrug.Rx_eRxStatus != null)
                            {
                                oSqlParameter.Value = objPatient.Medication[i].MedicationDrug.Rx_eRxStatus;
                            }
                            else
                            {
                                oSqlParameter.Value = "";
                            }
                            oSQLCommand.Parameters.Add(oSqlParameter);
                            oSqlParameter = null;

                            // @Rx_eRxStatusMessage varchar(500),
                            oSqlParameter = new SqlParameter();
                            oSqlParameter.ParameterName = "@Rx_eRxStatusMessage";
                            oSqlParameter.SqlDbType = SqlDbType.VarChar;
                            oSqlParameter.Direction = ParameterDirection.Input;
                            if (objPatient.Medication[i].MedicationDrug.Rx_eRxStatusMessage != null)
                            {
                                oSqlParameter.Value = objPatient.Medication[i].MedicationDrug.Rx_eRxStatusMessage;
                            }
                            else
                            {
                                oSqlParameter.Value = "";
                            }
                            oSQLCommand.Parameters.Add(oSqlParameter);
                            oSqlParameter = null;

                            ////////////

                            string sPBMSourceName = objPatient.Medication[i].MedicationDrug.PBMSourceName;
                            oSqlParameter = new SqlParameter();
                            oSqlParameter.ParameterName = "@sPBMSourceName";
                            oSqlParameter.SqlDbType = SqlDbType.VarChar;
                            oSqlParameter.Direction = ParameterDirection.Input;
                            if (sPBMSourceName != null)
                            {
                                oSqlParameter.Value = sPBMSourceName;
                            }
                            else
                            {
                                oSqlParameter.Value = "";
                            }
                            oSQLCommand.Parameters.Add(oSqlParameter);
                            oSqlParameter = null;


                            // @RxMed_DMSID numeric(18, 0),  
                            oSqlParameter = new SqlParameter();
                            oSqlParameter.ParameterName = "@RxMed_DMSID";
                            oSqlParameter.SqlDbType = SqlDbType.BigInt;
                            oSqlParameter.Direction = ParameterDirection.Input;
                            oSqlParameter.Value = 0;

                            oSQLCommand.Parameters.Add(oSqlParameter);
                            oSqlParameter = null;


                            //Work with database 
                            oSQLCommand.CommandType = CommandType.StoredProcedure;
                            // SQL Command Type , is Store Procedure 
                            oSQLCommand.CommandText = storedProcedureName;
                            // Set Store Procedure Name 
                            oSQLCommand.Connection = gloDBConnection;


                            /////get the medications against for that date and check if the drug is present with current drug, if found don't insert the value to database but will be binded to the medication grid, the on medication save&cls the validations is handled for duplicate medication
                            //DataTable dtMedications = new DataTable();
                            //bool DrugExist = false;
                            //dtMedications  = GetMedicationForDate(nPatientID, objPatient.Medication[i].MedicationDrug.LastFillDate);
                            //if (dtMedications != null)
                            //{
                            //    if (dtMedications.Rows.Count >0)
                            //    {
                            //        for (Int16 cnt = 0; cnt <= dtMedications.Rows.Count - 1; cnt++)
                            //        {
                            //            if (dtMedications.Rows[cnt]["sMedication"].ToString() == objPatient.Medication[i].MedicationDrug.DrugName.ToString() && dtMedications.Rows[cnt]["sDosage"].ToString() == sDosage.ToString() && dtMedications.Rows[cnt]["sRoute"].ToString() == "" && dtMedications.Rows[cnt]["sFrequency"].ToString() == "" && dtMedications.Rows[cnt]["sDuration"].ToString() == objPatient.Medication[i].MedicationDrug.DaysSupply)
                            //            {
                            //                DrugExist = true;
                            //                break;
                            //                //MessageBox.Show("The Medication " + dtMedications.Rows[cnt]["sMedication"].ToString() + " has already been entered against this visit ", "gloEMR", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                            //            }
                            //            else
                            //            {
                            //                DrugExist = false;
                            //            }
                            //        }

                            //    }
                            //}

                            //if (dtMedications != null)
                            //{
                            //    dtMedications.Dispose();
                            //} 

                            //if (DrugExist == false)
                            //{
                            //    _RecordAffected = oSQLCommand.ExecuteNonQuery();
                            //}
                            //else
                            //{
                            //   ///Do nothing
                            //}

                            _RecordAffected = oSQLCommand.ExecuteNonQuery();

                            oSQLCommand.Parameters.Clear();

                            if (_RecordAffected > 0)
                            {
                                _blnResult = true;
                            }

                            #endregion "In Data present in Datatable"

                        }
                        else
                        {
                            //new logic 
                            #region "In Data present in Datatable"
                            //oPatient.PatientID = Convert.ToInt64(dt.Rows[0]["PatientID"]);

                            oSqlParameter = new SqlParameter();
                            oSqlParameter.ParameterName = "@nMedicationId";
                            oSqlParameter.SqlDbType = SqlDbType.BigInt;
                            oSqlParameter.Direction = ParameterDirection.InputOutput;
                            oSqlParameter.Value = 0;
                            oSQLCommand.Parameters.Add(oSqlParameter);
                            oSqlParameter = null;


                            //if (objPatient.Medication[i].MedicationDrug.MedVisitId != 0)
                            //{
                            //    _VisitId = objPatient.Medication[i].MedicationDrug.MedVisitId;
                            //}
                            //else
                            //{

                            //    ////assign visit date as the Last fill date
                            //    _VisitId = GetVisitID("gsp_GetVisitID", nPatientID, objPatient.Medication[i].MedicationDrug.LastFillDate);
                            //    if (_VisitId == 0)
                            //    {
                            //        _VisitId = InsertVisit("gsp_InsertVisits", nPatientID, objPatient.Medication[i].MedicationDrug.LastFillDate);
                            //    }

                            //}




                            oSqlParameter = new SqlParameter();
                            oSqlParameter.ParameterName = "@nVisitID";
                            oSqlParameter.SqlDbType = SqlDbType.BigInt;
                            oSqlParameter.Direction = ParameterDirection.Input;
                            oSqlParameter.Value = _VisitId;
                            oSQLCommand.Parameters.Add(oSqlParameter);
                            oSqlParameter = null;



                            oSqlParameter = new SqlParameter();
                            oSqlParameter.ParameterName = "@nPatientID";
                            oSqlParameter.SqlDbType = SqlDbType.BigInt;
                            oSqlParameter.Direction = ParameterDirection.Input;
                       //     if (nPatientID != null)
                            {
                                oSqlParameter.Value = nPatientID;
                            }
                            //else
                            //{
                            //    oSqlParameter.Value = 0;
                            //}
                            oSQLCommand.Parameters.Add(oSqlParameter);
                            oSqlParameter = null;


                            oSqlParameter = new SqlParameter();
                            oSqlParameter.ParameterName = "@sMedication";
                            oSqlParameter.SqlDbType = SqlDbType.VarChar;
                            oSqlParameter.Direction = ParameterDirection.Input;
                            if (objPatient.Medication[i].MedicationDrug.DrugName != null)
                            {
                                oSqlParameter.Value = objPatient.Medication[i].MedicationDrug.DrugName;
                            }
                            else
                            {
                                oSqlParameter.Value = "";
                            }
                            oSQLCommand.Parameters.Add(oSqlParameter);
                            oSqlParameter = null;


                            //string sDosage1 = objPatient.Medication[i].MedicationDrug.Dosage;
                            string sDosage = ""; // dt.Rows[0]["Strength"] + " " + dt.Rows[0]["Strengthunit"];

                            oSqlParameter = new SqlParameter();
                            oSqlParameter.ParameterName = "@sDosage";
                            oSqlParameter.SqlDbType = SqlDbType.VarChar;
                            oSqlParameter.Direction = ParameterDirection.Input;
                            if (sDosage != null)
                            {
                                oSqlParameter.Value = sDosage;
                            }
                            else
                            {
                                oSqlParameter.Value = "";
                            }
                            oSQLCommand.Parameters.Add(oSqlParameter);
                            oSqlParameter = null;


                            oSqlParameter = new SqlParameter();
                            oSqlParameter.ParameterName = "@sRoute";
                            oSqlParameter.SqlDbType = SqlDbType.VarChar;
                            oSqlParameter.Direction = ParameterDirection.Input;
                            //if (objPatient.Medication[i].MedicationDrug != null)
                            //{
                            //   // oSqlParameter.Value = objPatient.Medication[i].MedicationDrug.;
                            //}
                            //else
                            //{
                            //    oSqlParameter.Value = "";
                            //}

                            oSqlParameter.Value = "";
                            oSQLCommand.Parameters.Add(oSqlParameter);
                            oSqlParameter = null;


                            oSqlParameter = new SqlParameter();
                            oSqlParameter.ParameterName = "@sFrequency";
                            oSqlParameter.SqlDbType = SqlDbType.VarChar;
                            oSqlParameter.Direction = ParameterDirection.Input;
                            //if (objPatient.Medication[i].MedicationDrug.frequency != null)
                            //{
                            //     oSqlParameter.Value = objPatient.Medication[i].MedicationDrug.frequency;
                            //}
                            //else
                            //{
                            //    oSqlParameter.Value = 0;
                            //}

                            oSqlParameter.Value = "";
                            oSQLCommand.Parameters.Add(oSqlParameter);
                            oSqlParameter = null;

                            oSqlParameter = new SqlParameter();
                            oSqlParameter.ParameterName = "@sDuration";
                            oSqlParameter.SqlDbType = SqlDbType.VarChar;
                            oSqlParameter.Direction = ParameterDirection.Input;
                            if (objPatient.Medication[i].MedicationDrug.DaysSupply != null)
                            {
                                oSqlParameter.Value = objPatient.Medication[i].MedicationDrug.DaysSupply;
                            }
                            else
                            {
                                oSqlParameter.Value = 0;
                            }

                            oSQLCommand.Parameters.Add(oSqlParameter);
                            oSqlParameter = null;

                            oSqlParameter = new SqlParameter();
                            oSqlParameter.ParameterName = "@dtStartdate";
                            oSqlParameter.SqlDbType = SqlDbType.DateTime;
                            oSqlParameter.Direction = ParameterDirection.Input;
                            oSqlParameter.Value = objPatient.Medication[i].MedicationDrug.LastFillDate;
                            oSQLCommand.Parameters.Add(oSqlParameter);
                            oSqlParameter = null;


                            DateTime dtLastFillDate = objPatient.Medication[i].MedicationDrug.LastFillDate;
                            double sDaysupply = Convert.ToDouble(objPatient.Medication[i].MedicationDrug.DaysSupply);

                            // DateTime dtEndDate =  dtLastFillDate.AddDays(sDaysupply);


                            oSqlParameter = new SqlParameter();
                            oSqlParameter.ParameterName = "@dtMedicationDate";
                            oSqlParameter.SqlDbType = SqlDbType.DateTime;
                            oSqlParameter.Direction = ParameterDirection.Input;
                            //if (objPatient.Medication[i].MedicationDrug.DrugName != null)
                            //{
                            //   oSqlParameter.Value = objPatient.Medication[i].MedicationDrug.DrugName;
                            //}
                            //else
                            //{
                            //    oSqlParameter.Value = 0;
                            //}
                            oSqlParameter.Value = DateTime.Now;//  objPatient.Medication[i].MedicationDrug.LastFillDate;
                            oSQLCommand.Parameters.Add(oSqlParameter);
                            oSqlParameter = null;



                            oSqlParameter = new SqlParameter();
                            oSqlParameter.ParameterName = "@sAmount";
                            oSqlParameter.SqlDbType = SqlDbType.VarChar;
                            oSqlParameter.Direction = ParameterDirection.Input;
                            if (objPatient.Medication[i].MedicationDrug.DrugQuantityValue != null)
                            {
                                oSqlParameter.Value = objPatient.Medication[i].MedicationDrug.DrugQuantityValue;
                            }
                            else
                            {
                                oSqlParameter.Value = "";
                            }
                            oSQLCommand.Parameters.Add(oSqlParameter);
                            oSqlParameter = null;


                            oSqlParameter = new SqlParameter();
                            oSqlParameter.ParameterName = "@dtEndDate";
                            oSqlParameter.SqlDbType = SqlDbType.DateTime;
                            oSqlParameter.Direction = ParameterDirection.Input;

                            //if (objPatient.PatientID != null)
                            //{
                            //    // oSqlParameter.Value = objPatient.Medication[i].MedicationDrug.DrugName;
                            //}
                            //else
                            //{

                            int sDaysSupply = Convert.ToInt16(objPatient.Medication[i].MedicationDrug.DaysSupply);
                            if (sDaysSupply <= 31)
                            {
                                oSqlParameter.Value = dtLastFillDate.AddDays(sDaysSupply);
                            }

                            else
                            {
                                sDaysupply = sDaysupply - 31;
                                dtLastFillDate = dtLastFillDate.AddMonths(1);

                                oSqlParameter.Value = dtLastFillDate.AddDays(sDaysSupply);
                            }

                            // }
                            oSQLCommand.Parameters.Add(oSqlParameter);
                            oSqlParameter = null;

                            // oClsgloRxHubGenral=new ClsgloRxHubGeneral();

                            long nMachinID;
                            //gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(ClsgloRxHubGeneral.ConnectionString);
                            nMachinID = ClsgloRxHubGeneral.GetPrefixTransactionID(nPatientID);
                            //nMachinID = ogloRxHubGenral.GetPrefixTransactionID();

                            oSqlParameter = new SqlParameter();
                            oSqlParameter.ParameterName = "@MachineID";
                            oSqlParameter.SqlDbType = SqlDbType.BigInt;
                            oSqlParameter.Direction = ParameterDirection.Input;
                            oSqlParameter.Value = nMachinID;
                            oSQLCommand.Parameters.Add(oSqlParameter);
                            oSqlParameter = null;

                            oSqlParameter = new SqlParameter();
                            oSqlParameter.ParameterName = "@sStatus";
                            oSqlParameter.SqlDbType = SqlDbType.VarChar;
                            oSqlParameter.Direction = ParameterDirection.Input;
                            //if (objPatient.PatientID != null)
                            //{
                            //    // oSqlParameter.Value = objPatient.Medication[i].MedicationDrug.DrugName;
                            //}
                            //else
                            //{
                            oSqlParameter.Value = "";
                            //}
                            oSQLCommand.Parameters.Add(oSqlParameter);
                            oSqlParameter = null;

                            oSqlParameter = new SqlParameter();
                            oSqlParameter.ParameterName = "@sReason";
                            oSqlParameter.SqlDbType = SqlDbType.VarChar;
                            oSqlParameter.Direction = ParameterDirection.Input;
                            //if (objPatient.PatientID != null)
                            //{
                            //    // oSqlParameter.Value = objPatient.Medication[i].MedicationDrug.DrugName;
                            //}
                            //else
                            //{
                            oSqlParameter.Value = "";
                            ////}
                            oSQLCommand.Parameters.Add(oSqlParameter);
                            oSqlParameter = null;


                            oSqlParameter = new SqlParameter();
                            oSqlParameter.ParameterName = "@nDDID";
                            oSqlParameter.SqlDbType = SqlDbType.BigInt;
                            oSqlParameter.Direction = ParameterDirection.Input;
                            //if (objPatient.PatientID != null)
                            //{
                            //    // oSqlParameter.Value = objPatient.Medication[i].MedicationDrug.DrugName;
                            //}
                            //else
                            //{
                            oSqlParameter.Value = 0;
                            //}
                            oSQLCommand.Parameters.Add(oSqlParameter);
                            oSqlParameter = null;


                            oSqlParameter = new SqlParameter();
                            oSqlParameter.ParameterName = "@sUserName";
                            oSqlParameter.SqlDbType = SqlDbType.VarChar;
                            oSqlParameter.Direction = ParameterDirection.Input;
                            //if (objPatient.PatientID != null)
                            //{
                            //    // oSqlParameter.Value = objPatient.Medication[i].MedicationDrug.DrugName;
                            //}
                            //else
                            //{
                            oSqlParameter.Value = gloRxHub.ClsgloRxHubGeneral.gstrRxHubLoginUser;
                            //}
                            oSQLCommand.Parameters.Add(oSqlParameter);
                            oSqlParameter = null;


                            //oSqlParameter = new SqlParameter();
                            //oSqlParameter.ParameterName = "@sUserName1";
                            //oSqlParameter.SqlDbType = SqlDbType.VarChar;
                            //oSqlParameter.Direction = ParameterDirection.Input;
                            ////if (objPatient.PatientID != null)
                            ////{
                            ////    // oSqlParameter.Value = objPatient.Medication[i].MedicationDrug.DrugName;
                            ////}
                            ////else
                            ////{
                            //oSqlParameter.Value = "";
                            ////}
                            //oSQLCommand.Parameters.Add(oSqlParameter);
                            //oSqlParameter = null;


                            //oSqlParameter = new SqlParameter();
                            //oSqlParameter.ParameterName = "@sMachineName";
                            //oSqlParameter.SqlDbType = SqlDbType.VarChar;
                            //oSqlParameter.Direction = ParameterDirection.Input;
                            ////if (objPatient.PatientID != null)
                            ////{
                            ////    // oSqlParameter.Value = objPatient.Medication[i].MedicationDrug.DrugName;
                            ////}
                            ////else
                            ////{
                            //oSqlParameter.Value = "";
                            ////}
                            //oSQLCommand.Parameters.Add(oSqlParameter);
                            //oSqlParameter = null;


                            oSqlParameter = new SqlParameter();
                            oSqlParameter.ParameterName = "@nPrescriptionID";
                            oSqlParameter.SqlDbType = SqlDbType.BigInt;
                            oSqlParameter.Direction = ParameterDirection.Input;
                            //if (objPatient.PatientID != null)
                            //{
                            //    // oSqlParameter.Value = objPatient.Medication[i].MedicationDrug.DrugName;
                            //}
                            //else
                            //{
                            oSqlParameter.Value = 0;
                            //}
                            oSQLCommand.Parameters.Add(oSqlParameter);
                            oSqlParameter = null;



                            oSqlParameter = new SqlParameter();
                            oSqlParameter.ParameterName = "@sRenewed";
                            oSqlParameter.SqlDbType = SqlDbType.VarChar;
                            oSqlParameter.Direction = ParameterDirection.Input;
                            //if (objPatient.PatientID != null)
                            //{
                            //    // oSqlParameter.Value = objPatient.Medication[i].MedicationDrug.DrugName;
                            //}
                            //else
                            //{
                            oSqlParameter.Value = "";
                            //}
                            oSQLCommand.Parameters.Add(oSqlParameter);
                            oSqlParameter = null;


                            oSqlParameter = new SqlParameter();
                            oSqlParameter.ParameterName = "@sNDCCode";
                            oSqlParameter.SqlDbType = SqlDbType.VarChar;
                            oSqlParameter.Direction = ParameterDirection.Input;
                            if (objPatient.Medication[i].MedicationDrug.NDCCode != null)
                            {
                                oSqlParameter.Value = objPatient.Medication[i].MedicationDrug.NDCCode;
                            }
                            else
                            {
                                oSqlParameter.Value = "";
                            }
                            oSQLCommand.Parameters.Add(oSqlParameter);
                            oSqlParameter = null;



                            oSqlParameter = new SqlParameter();
                            oSqlParameter.ParameterName = "@nIsNarcotic";
                            oSqlParameter.SqlDbType = SqlDbType.Int;
                            oSqlParameter.Direction = ParameterDirection.Input;
                            //if (objPatient.PatientID != null)
                            //{
                            //    // oSqlParameter.Value = objPatient.Medication[i].MedicationDrug.DrugName;
                            //}
                            //else
                            //{
                            oSqlParameter.Value = 0;
                            //}
                            oSQLCommand.Parameters.Add(oSqlParameter);
                            oSqlParameter = null;


                            string sDrugForm = ""; // Convert.ToString(dt.Rows[0]["Dosechekunit"]);

                            oSqlParameter = new SqlParameter();
                            oSqlParameter.ParameterName = "@sDrugForm";
                            oSqlParameter.SqlDbType = SqlDbType.VarChar;
                            oSqlParameter.Direction = ParameterDirection.Input;
                            if (sDrugForm != null)
                            {
                                oSqlParameter.Value = sDrugForm;
                            }
                            else
                            {
                                oSqlParameter.Value = "";
                            }
                            oSQLCommand.Parameters.Add(oSqlParameter);
                            oSqlParameter = null;

                            string sStrengthUnit = ""; // Convert.ToString(dt.Rows[0]["Strengthunit"]);
                            oSqlParameter = new SqlParameter();
                            oSqlParameter.ParameterName = "@sStrengthUnit";
                            oSqlParameter.SqlDbType = SqlDbType.VarChar;
                            oSqlParameter.Direction = ParameterDirection.Input;
                            if (sStrengthUnit != null)
                            {
                                oSqlParameter.Value = sStrengthUnit;
                            }
                            else
                            {
                                oSqlParameter.Value = "";
                            }
                            oSQLCommand.Parameters.Add(oSqlParameter);
                            oSqlParameter = null;

                            ////////////
                            ////@Rx_sRefills varchar(255), 
                            oSqlParameter = new SqlParameter();
                            oSqlParameter.ParameterName = "@Rx_sRefills";
                            oSqlParameter.SqlDbType = SqlDbType.VarChar;
                            oSqlParameter.Direction = ParameterDirection.Input;
                            if (objPatient.Medication[i].MedicationDrug.Rx_sRefills != null)
                            {
                                oSqlParameter.Value = objPatient.Medication[i].MedicationDrug.Rx_sRefills;
                            }
                            else
                            {
                                oSqlParameter.Value = "";
                            }
                            oSQLCommand.Parameters.Add(oSqlParameter);
                            oSqlParameter = null;

                            //// @Rx_sNotes varchar(1500),
                            oSqlParameter = new SqlParameter();
                            oSqlParameter.ParameterName = "@Rx_sNotes";
                            oSqlParameter.SqlDbType = SqlDbType.VarChar;
                            oSqlParameter.Direction = ParameterDirection.Input;
                            if (objPatient.Medication[i].MedicationDrug.Rx_sNotes != null)
                            {
                                oSqlParameter.Value = objPatient.Medication[i].MedicationDrug.Rx_sNotes;
                            }
                            else
                            {
                                oSqlParameter.Value = "";
                            }
                            oSQLCommand.Parameters.Add(oSqlParameter);
                            oSqlParameter = null;

                            ///@Rx_sMethod varchar(50),
                            oSqlParameter = new SqlParameter();
                            oSqlParameter.ParameterName = "@Rx_sMethod";
                            oSqlParameter.SqlDbType = SqlDbType.VarChar;
                            oSqlParameter.Direction = ParameterDirection.Input;
                            if (objPatient.Medication[i].MedicationDrug.Rx_sMethod != null)
                            {
                                oSqlParameter.Value = objPatient.Medication[i].MedicationDrug.Rx_sMethod;
                            }
                            else
                            {
                                oSqlParameter.Value = "";
                            }
                            oSQLCommand.Parameters.Add(oSqlParameter);
                            oSqlParameter = null;

                            //@Rx_bMaySubstitute bit,  
                            oSqlParameter = new SqlParameter();
                            oSqlParameter.ParameterName = "@Rx_bMaySubstitute";
                            oSqlParameter.SqlDbType = SqlDbType.Bit;
                            oSqlParameter.Direction = ParameterDirection.Input;
                      //      if (objPatient.Medication[i].MedicationDrug.Rx_bMaySubstitute != null)
                            {
                                oSqlParameter.Value = objPatient.Medication[i].MedicationDrug.Rx_bMaySubstitute;
                            }
                            //else
                            //{
                            //    oSqlParameter.Value = false;
                            //}
                            oSQLCommand.Parameters.Add(oSqlParameter);
                            oSqlParameter = null;

                            ////@Rx_nDrugID numeric(18, 0), 
                            oSqlParameter = new SqlParameter();
                            oSqlParameter.ParameterName = "@Rx_nDrugID";
                            oSqlParameter.SqlDbType = SqlDbType.BigInt;
                            oSqlParameter.Direction = ParameterDirection.Input;
                          //  if (objPatient.Medication[i].MedicationDrug.Rx_nDrugID != null)
                            {
                                oSqlParameter.Value = objPatient.Medication[i].MedicationDrug.Rx_nDrugID;
                            }
                            //else
                            //{
                            //    oSqlParameter.Value = 0;
                            //}
                            oSQLCommand.Parameters.Add(oSqlParameter);
                            oSqlParameter = null;

                            //////@Rx_blnflag bit,  
                            oSqlParameter = new SqlParameter();
                            oSqlParameter.ParameterName = "@Rx_blnflag";
                            oSqlParameter.SqlDbType = SqlDbType.Bit;
                            oSqlParameter.Direction = ParameterDirection.Input;
                           // if (objPatient.Medication[i].MedicationDrug.Rx_blnflag != null)
                            {
                                oSqlParameter.Value = objPatient.Medication[i].MedicationDrug.Rx_blnflag;
                            }
                            //else
                            //{
                            //    oSqlParameter.Value = false;
                            //}
                            oSQLCommand.Parameters.Add(oSqlParameter);
                            oSqlParameter = null;

                            ///////@Rx_sLotNo varchar(50),  
                            oSqlParameter = new SqlParameter();
                            oSqlParameter.ParameterName = "@Rx_sLotNo";
                            oSqlParameter.SqlDbType = SqlDbType.VarChar;
                            oSqlParameter.Direction = ParameterDirection.Input;
                            if (objPatient.Medication[i].MedicationDrug.Rx_sLotNo != null)
                            {
                                oSqlParameter.Value = objPatient.Medication[i].MedicationDrug.Rx_sLotNo;
                            }
                            else
                            {
                                oSqlParameter.Value = "";
                            }
                            oSQLCommand.Parameters.Add(oSqlParameter);
                            oSqlParameter = null;

                            /////// @Rx_dtExpirationdate datetime, 
                            oSqlParameter = new SqlParameter();
                            oSqlParameter.ParameterName = "@Rx_dtExpirationdate";
                            oSqlParameter.SqlDbType = SqlDbType.DateTime;
                            oSqlParameter.Direction = ParameterDirection.Input;
                            if (objPatient.Medication[i].MedicationDrug.Rx_dtExpirationdate != null)
                            {
                                oSqlParameter.Value = DBNull.Value;// DateTime.Now;///// objPatient.Medication[i].MedicationDrug.Rx_dtExpirationdate;
                            }
                            else
                            {
                                oSqlParameter.Value = DBNull.Value;//"12:00:00 AM";
                            }
                            oSQLCommand.Parameters.Add(oSqlParameter);
                            oSqlParameter = null;

                            //////@Rx_nProviderId numeric(18, 0),  
                            oSqlParameter = new SqlParameter();
                            oSqlParameter.ParameterName = "@Rx_nProviderId";
                            oSqlParameter.SqlDbType = SqlDbType.BigInt;
                            oSqlParameter.Direction = ParameterDirection.Input;
                    //        if (objPatient.Medication[i].MedicationDrug.Rx_nProviderId != null)
                            {
                                oSqlParameter.Value = objPatient.Medication[i].MedicationDrug.Rx_nProviderId;
                            }
                            //else
                            //{
                            //    oSqlParameter.Value = 0;
                            //}
                            oSQLCommand.Parameters.Add(oSqlParameter);
                            oSqlParameter = null;

                            // @Rx_sChiefComplaints varchar(255), 
                            oSqlParameter = new SqlParameter();
                            oSqlParameter.ParameterName = "@Rx_sChiefComplaints";
                            oSqlParameter.SqlDbType = SqlDbType.VarChar;
                            oSqlParameter.Direction = ParameterDirection.Input;
                            if (objPatient.Medication[i].MedicationDrug.Rx_sChiefComplaints != null)
                            {
                                oSqlParameter.Value = objPatient.Medication[i].MedicationDrug.Rx_sChiefComplaints;
                            }
                            else
                            {
                                oSqlParameter.Value = "";
                            }
                            oSQLCommand.Parameters.Add(oSqlParameter);
                            oSqlParameter = null;

                            // @Rx_sStatus varchar(50),
                            oSqlParameter = new SqlParameter();
                            oSqlParameter.ParameterName = "@Rx_sStatus";
                            oSqlParameter.SqlDbType = SqlDbType.VarChar;
                            oSqlParameter.Direction = ParameterDirection.Input;
                            if (objPatient.Medication[i].MedicationDrug.Rx_sStatus != null)
                            {
                                oSqlParameter.Value = objPatient.Medication[i].MedicationDrug.Rx_sStatus;
                            }
                            else
                            {
                                oSqlParameter.Value = "";
                            }
                            oSQLCommand.Parameters.Add(oSqlParameter);
                            oSqlParameter = null;

                            // @Rx_sRxReferenceNumber varchar(50),  
                            oSqlParameter = new SqlParameter();
                            oSqlParameter.ParameterName = "@Rx_sRxReferenceNumber";
                            oSqlParameter.SqlDbType = SqlDbType.VarChar;
                            oSqlParameter.Direction = ParameterDirection.Input;
                            if (objPatient.Medication[i].MedicationDrug.Rx_sRxReferenceNumber != null)
                            {
                                oSqlParameter.Value = objPatient.Medication[i].MedicationDrug.Rx_sRxReferenceNumber;
                            }
                            else
                            {
                                oSqlParameter.Value = "";
                            }
                            oSQLCommand.Parameters.Add(oSqlParameter);
                            oSqlParameter = null;

                            // @Rx_sRefillQualifier varchar(3), 
                            oSqlParameter = new SqlParameter();
                            oSqlParameter.ParameterName = "@Rx_sRefillQualifier";
                            oSqlParameter.SqlDbType = SqlDbType.VarChar;
                            oSqlParameter.Direction = ParameterDirection.Input;
                            if (objPatient.Medication[i].MedicationDrug.Rx_sRefillQualifier != null)
                            {
                                oSqlParameter.Value = objPatient.Medication[i].MedicationDrug.Rx_sRefillQualifier;
                            }
                            else
                            {
                                oSqlParameter.Value = "";
                            }
                            oSQLCommand.Parameters.Add(oSqlParameter);
                            oSqlParameter = null;

                            // @Rx_nPharmacyId numeric(18, 0),  
                            oSqlParameter = new SqlParameter();
                            oSqlParameter.ParameterName = "@Rx_nPharmacyId";
                            oSqlParameter.SqlDbType = SqlDbType.BigInt;
                            oSqlParameter.Direction = ParameterDirection.Input;
                     //       if (objPatient.Medication[i].MedicationDrug.Rx_nPharmacyId != null)
                            {
                                oSqlParameter.Value = objPatient.Medication[i].MedicationDrug.Rx_nPharmacyId;
                            }
                            //else
                            //{
                            //    oSqlParameter.Value = 0;
                            //}
                            oSQLCommand.Parameters.Add(oSqlParameter);
                            oSqlParameter = null;

                            // @Rx_sNCPDPID varchar(50),  
                            oSqlParameter = new SqlParameter();
                            oSqlParameter.ParameterName = "@Rx_sNCPDPID";
                            oSqlParameter.SqlDbType = SqlDbType.VarChar;
                            oSqlParameter.Direction = ParameterDirection.Input;
                            if (objPatient.Medication[i].MedicationDrug.Rx_sNCPDPID != null)
                            {
                                oSqlParameter.Value = objPatient.Medication[i].MedicationDrug.Rx_sNCPDPID;
                            }
                            else
                            {
                                oSqlParameter.Value = "";
                            }
                            oSQLCommand.Parameters.Add(oSqlParameter);
                            oSqlParameter = null;


                            // @Rx_nContactID numeric(18, 0),  
                            oSqlParameter = new SqlParameter();
                            oSqlParameter.ParameterName = "@Rx_nContactID";
                            oSqlParameter.SqlDbType = SqlDbType.BigInt;
                            oSqlParameter.Direction = ParameterDirection.Input;
                   //         if (objPatient.Medication[i].MedicationDrug.Rx_nContactID != null)
                            {
                                oSqlParameter.Value = objPatient.Medication[i].MedicationDrug.Rx_nContactID;
                            }
                            //else
                            //{
                            //    oSqlParameter.Value = 0;
                            //}
                            oSQLCommand.Parameters.Add(oSqlParameter);
                            oSqlParameter = null;

                            // @Rx_sName varchar(255),  
                            oSqlParameter = new SqlParameter();
                            oSqlParameter.ParameterName = "@Rx_sName";
                            oSqlParameter.SqlDbType = SqlDbType.VarChar;
                            oSqlParameter.Direction = ParameterDirection.Input;
                            if (objPatient.Medication[i].MedicationDrug.Rx_sName != null)
                            {
                                oSqlParameter.Value = objPatient.Medication[i].MedicationDrug.Rx_sName;
                            }
                            else
                            {
                                oSqlParameter.Value = "";
                            }
                            oSQLCommand.Parameters.Add(oSqlParameter);
                            oSqlParameter = null;

                            // @Rx_sAddressline1 varchar(255),  
                            oSqlParameter = new SqlParameter();
                            oSqlParameter.ParameterName = "@Rx_sAddressline1";
                            oSqlParameter.SqlDbType = SqlDbType.VarChar;
                            oSqlParameter.Direction = ParameterDirection.Input;
                            if (objPatient.Medication[i].MedicationDrug.Rx_sAddressline1 != null)
                            {
                                oSqlParameter.Value = objPatient.Medication[i].MedicationDrug.Rx_sAddressline1;
                            }
                            else
                            {
                                oSqlParameter.Value = "";
                            }
                            oSQLCommand.Parameters.Add(oSqlParameter);
                            oSqlParameter = null;

                            // @Rx_sAddressline2 nchar(10),
                            oSqlParameter = new SqlParameter();
                            oSqlParameter.ParameterName = "@Rx_sAddressline2";
                            oSqlParameter.SqlDbType = SqlDbType.VarChar;
                            oSqlParameter.Direction = ParameterDirection.Input;
                            if (objPatient.Medication[i].MedicationDrug.Rx_sAddressline2 != null)
                            {
                                oSqlParameter.Value = objPatient.Medication[i].MedicationDrug.Rx_sAddressline2;
                            }
                            else
                            {
                                oSqlParameter.Value = "";
                            }
                            oSQLCommand.Parameters.Add(oSqlParameter);
                            oSqlParameter = null;

                            // @Rx_sCity varchar(50),  
                            oSqlParameter = new SqlParameter();
                            oSqlParameter.ParameterName = "@Rx_sCity";
                            oSqlParameter.SqlDbType = SqlDbType.VarChar;
                            oSqlParameter.Direction = ParameterDirection.Input;
                            if (objPatient.Medication[i].MedicationDrug.Rx_sCity != null)
                            {
                                oSqlParameter.Value = objPatient.Medication[i].MedicationDrug.Rx_sCity;
                            }
                            else
                            {
                                oSqlParameter.Value = "";
                            }
                            oSQLCommand.Parameters.Add(oSqlParameter);
                            oSqlParameter = null;

                            // @Rx_sState varchar(50), 
                            oSqlParameter = new SqlParameter();
                            oSqlParameter.ParameterName = "@Rx_sState";
                            oSqlParameter.SqlDbType = SqlDbType.VarChar;
                            oSqlParameter.Direction = ParameterDirection.Input;
                            if (objPatient.Medication[i].MedicationDrug.Rx_sState != null)
                            {
                                oSqlParameter.Value = objPatient.Medication[i].MedicationDrug.Rx_sState;
                            }
                            else
                            {
                                oSqlParameter.Value = "";
                            }
                            oSQLCommand.Parameters.Add(oSqlParameter);
                            oSqlParameter = null;

                            // @Rx_sZip varchar(50),  
                            oSqlParameter = new SqlParameter();
                            oSqlParameter.ParameterName = "@Rx_sZip";
                            oSqlParameter.SqlDbType = SqlDbType.VarChar;
                            oSqlParameter.Direction = ParameterDirection.Input;
                            if (objPatient.Medication[i].MedicationDrug.Rx_sZip != null)
                            {
                                oSqlParameter.Value = objPatient.Medication[i].MedicationDrug.Rx_sZip;
                            }
                            else
                            {
                                oSqlParameter.Value = "";
                            }
                            oSQLCommand.Parameters.Add(oSqlParameter);
                            oSqlParameter = null;

                            // @Rx_sEmail varchar(50),  
                            oSqlParameter = new SqlParameter();
                            oSqlParameter.ParameterName = "@Rx_sEmail";
                            oSqlParameter.SqlDbType = SqlDbType.VarChar;
                            oSqlParameter.Direction = ParameterDirection.Input;
                            if (objPatient.Medication[i].MedicationDrug.Rx_sEmail != null)
                            {
                                oSqlParameter.Value = objPatient.Medication[i].MedicationDrug.Rx_sEmail;
                            }
                            else
                            {
                                oSqlParameter.Value = "";
                            }
                            oSQLCommand.Parameters.Add(oSqlParameter);
                            oSqlParameter = null;

                            // @Rx_sFax varchar(50),  
                            oSqlParameter = new SqlParameter();
                            oSqlParameter.ParameterName = "@Rx_sFax";
                            oSqlParameter.SqlDbType = SqlDbType.VarChar;
                            oSqlParameter.Direction = ParameterDirection.Input;
                            if (objPatient.Medication[i].MedicationDrug.Rx_sFax != null)
                            {
                                oSqlParameter.Value = objPatient.Medication[i].MedicationDrug.Rx_sFax;
                            }
                            else
                            {
                                oSqlParameter.Value = "";
                            }
                            oSQLCommand.Parameters.Add(oSqlParameter);
                            oSqlParameter = null;

                            // @Rx_sPhone varchar(50),  
                            oSqlParameter = new SqlParameter();
                            oSqlParameter.ParameterName = "@Rx_sPhone";
                            oSqlParameter.SqlDbType = SqlDbType.VarChar;
                            oSqlParameter.Direction = ParameterDirection.Input;
                            if (objPatient.Medication[i].MedicationDrug.Rx_sPhone != null)
                            {
                                oSqlParameter.Value = objPatient.Medication[i].MedicationDrug.Rx_sPhone;
                            }
                            else
                            {
                                oSqlParameter.Value = "";
                            }
                            oSQLCommand.Parameters.Add(oSqlParameter);
                            oSqlParameter = null;

                            // @Rx_sServiceLevel varchar(16),  
                            oSqlParameter = new SqlParameter();
                            oSqlParameter.ParameterName = "@Rx_sServiceLevel";
                            oSqlParameter.SqlDbType = SqlDbType.VarChar;
                            oSqlParameter.Direction = ParameterDirection.Input;
                            if (objPatient.Medication[i].MedicationDrug.Rx_sServiceLevel != null)
                            {
                                oSqlParameter.Value = objPatient.Medication[i].MedicationDrug.Rx_sServiceLevel;
                            }
                            else
                            {
                                oSqlParameter.Value = "";
                            }
                            oSQLCommand.Parameters.Add(oSqlParameter);
                            oSqlParameter = null;

                            // @Rx_sPrescriberNotes varchar(1500),  
                            oSqlParameter = new SqlParameter();
                            oSqlParameter.ParameterName = "@Rx_sPrescriberNotes";
                            oSqlParameter.SqlDbType = SqlDbType.VarChar;
                            oSqlParameter.Direction = ParameterDirection.Input;
                            if (objPatient.Medication[i].MedicationDrug.Rx_sPrescriberNotes != null)
                            {
                                oSqlParameter.Value = objPatient.Medication[i].MedicationDrug.Rx_sPrescriberNotes;
                            }
                            else
                            {
                                oSqlParameter.Value = "";
                            }
                            oSQLCommand.Parameters.Add(oSqlParameter);
                            oSqlParameter = null;

                            // @Rx_eRxStatus varchar(50),   
                            oSqlParameter = new SqlParameter();
                            oSqlParameter.ParameterName = "@Rx_eRxStatus";
                            oSqlParameter.SqlDbType = SqlDbType.VarChar;
                            oSqlParameter.Direction = ParameterDirection.Input;
                            if (objPatient.Medication[i].MedicationDrug.Rx_eRxStatus != null)
                            {
                                oSqlParameter.Value = objPatient.Medication[i].MedicationDrug.Rx_eRxStatus;
                            }
                            else
                            {
                                oSqlParameter.Value = "";
                            }
                            oSQLCommand.Parameters.Add(oSqlParameter);
                            oSqlParameter = null;

                            // @Rx_eRxStatusMessage varchar(500),
                            oSqlParameter = new SqlParameter();
                            oSqlParameter.ParameterName = "@Rx_eRxStatusMessage";
                            oSqlParameter.SqlDbType = SqlDbType.VarChar;
                            oSqlParameter.Direction = ParameterDirection.Input;
                            if (objPatient.Medication[i].MedicationDrug.Rx_eRxStatusMessage != null)
                            {
                                oSqlParameter.Value = objPatient.Medication[i].MedicationDrug.Rx_eRxStatusMessage;
                            }
                            else
                            {
                                oSqlParameter.Value = "";
                            }
                            oSQLCommand.Parameters.Add(oSqlParameter);
                            oSqlParameter = null;

                            ////////////

                            oSqlParameter = new SqlParameter();
                            oSqlParameter.ParameterName = "@sPBMSourceName";
                            oSqlParameter.SqlDbType = SqlDbType.VarChar;
                            oSqlParameter.Direction = ParameterDirection.Input;
                            if (objPatient.Medication[i].MedicationDrug.PBMSourceName != null)
                            {
                                oSqlParameter.Value = objPatient.Medication[i].MedicationDrug.PBMSourceName;
                            }
                            else
                            {
                                oSqlParameter.Value = "";
                            }
                            oSQLCommand.Parameters.Add(oSqlParameter);
                            oSqlParameter = null;


                            // @RxMed_DMSID numeric(18, 0),  
                            oSqlParameter = new SqlParameter();
                            oSqlParameter.ParameterName = "@RxMed_DMSID";
                            oSqlParameter.SqlDbType = SqlDbType.BigInt;
                            oSqlParameter.Direction = ParameterDirection.Input;
                            oSqlParameter.Value = 0;

                            oSQLCommand.Parameters.Add(oSqlParameter);
                            oSqlParameter = null;

                            //Work with database 
                            oSQLCommand.CommandType = CommandType.StoredProcedure;
                            // SQL Command Type , is Store Procedure 
                            oSQLCommand.CommandText = storedProcedureName;
                            // Set Store Procedure Name 
                            oSQLCommand.Connection = gloDBConnection;


                            /////get the medications against for that date and check if the drug is present with current drug, if found don't insert the value to database but will be binded to the medication grid, the on medication save&cls the validations is handled for duplicate medication
                            //DataTable dtMedications = new DataTable();
                            //bool DrugExist = false;
                            //dtMedications = GetMedicationForDate(nPatientID, objPatient.Medication[i].MedicationDrug.LastFillDate);
                            //if (dtMedications != null)
                            //{
                            //    if (dtMedications.Rows.Count > 0)
                            //    {
                            //        for (Int16 cnt = 0; cnt <= dtMedications.Rows.Count - 1; cnt++)
                            //        {
                            //            if (dtMedications.Rows[cnt]["sMedication"].ToString() == objPatient.Medication[i].MedicationDrug.DrugName.ToString() && dtMedications.Rows[cnt]["sDosage"].ToString() == sDosage.ToString() && dtMedications.Rows[cnt]["sRoute"].ToString() == "" && dtMedications.Rows[cnt]["sFrequency"].ToString() == "" && dtMedications.Rows[cnt]["sDuration"].ToString() == objPatient.Medication[i].MedicationDrug.DaysSupply)
                            //            {
                            //                DrugExist = true;
                            //                break;
                            //                //MessageBox.Show("The Medication " + dtMedications.Rows[cnt]["sMedication"].ToString() + " has already been entered against this visit ", "gloEMR", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                            //            }
                            //            else
                            //            {
                            //                DrugExist = false;
                            //            }
                            //        }

                            //    }
                            //}
                            //if (dtMedications != null)
                            //{
                            //    dtMedications.Dispose();
                            //}

                            //if (DrugExist == false)
                            //{
                            //    _RecordAffected = oSQLCommand.ExecuteNonQuery();
                            //}
                            //else
                            //{
                            //    ///Do nothing
                            //}

                            _RecordAffected = oSQLCommand.ExecuteNonQuery();

                            oSQLCommand.Parameters.Clear();

                            if (_RecordAffected > 0)
                            {
                                _blnResult = true;
                            }

                            #endregion "In Data present in Datatable"
                        }
                        dt.Dispose();
                        dt = null;
                    }



                }

                if (_blnNdcNotFound == true)
                {
                    if (_strnoNDC.Length > 0)
                    {
                        // Commented on 20090615
                        // MessageBox.Show(_strnoNDC.ToString(), "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                return _blnResult;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                //_ErrorMessage = objError.Message;
                return false;

            }
            finally
            {
                //SLR: Finaly clear oSQLCommand.Parameters, free oSQLCommand and then
                if (oSQLCommand != null)
                {
                    oSQLCommand.Parameters.Clear();
                    oSQLCommand.Dispose();
                    oSQLCommand = null;
                }                
            }
        }

        public bool CheckDuplicateDrug(DataTable dtMedications)
        {
            DataTable odt1=null;
            DataTable odt2=null;
            try
            {

                //DataTable odt2;
                if (dtMedications != null)
                {
                    if (dtMedications.Rows.Count > 0)
                    {
                        for (Int16 i = 0; i <= dtMedications.Rows.Count - 1; i++)
                        {
                            odt1 = dtMedications.Copy();
                            odt1.Rows.Clear();
                            odt1.ImportRow((DataRow)dtMedications.Rows[i]);
                            for (Int16 j = 0; j <= dtMedications.Rows.Count - 1; j++)
                            {
                                if (i != j)
                                {
                                    odt2 = dtMedications.Copy();
                                    odt2.Rows.Clear();
                                    odt2.ImportRow((DataRow)dtMedications.Rows[j]);
                                    if (odt1.Rows[0]["sMedication"] == odt2.Rows[0]["sMedication"] && odt1.Rows[0]["sDosage"] == odt2.Rows[0]["sDosage"])/////& medication1.Route == medication2.Route & medication1.Frequency == medication2.Frequency & medication1.Duration == medication2.Duration & medication1.Status == medication2.Status & medication1.Reason == medication2.Reason
                                    {
                                        System.Windows.Forms.MessageBox.Show("The Medication " + odt1.Rows[0]["Medication"] + " has already been entered against this visit ", "gloEMR", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                                        odt1.Dispose();
                                        odt1 = null;
                                        odt2.Dispose();
                                        odt2 = null;
                                        return false;

                                    }
                                    else
                                    {
                                        odt2.Dispose();
                                        odt2 = null;
                                    }
                                }
                                //odt2 = null;
                            }
                            odt1.Dispose();
                            odt1 = null;
                        }
                    }
                }

                return true;/////return true when duplicate drug is not found
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                return false;
            }
        }

        //Methods 
        public DataTable ReadPatRecord(string SQLquery)
        {
            // Data Reader 
            DataSet oDS = new DataSet();
            //SqlParameter OsqlParmeter = default(SqlParameter);
            //int i = 0;

            try
            {
                //Check Connection 
                if (gloDBConnection.State == ConnectionState.Closed)
                {
                    _ErrorMessage = "Please check database connecion";
                    return null;
                }

                //Work with database 
                SqlDataAdapter oDataAdapter = new SqlDataAdapter(SQLquery, gloDBConnection);
                oDataAdapter.Fill(oDS);
                DataTable dt = null; //SLR: new is not needeed
                if (oDS.Tables.Count > 0)
                {
                    dt = oDS.Tables[0].Copy();
                }
                //SLR: Finally free odataapater
                oDataAdapter.Dispose();
                oDataAdapter = null;
                return dt;
            }
            catch (Exception objError)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, objError.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                _ErrorMessage = objError.Message;
                return null;
            }
            finally
            {
                oDS.Dispose();
                oDS = null;

            }

        }

        public DataSet GetPatientInformation_270(long _PatientId, ref long nLoginProviderid)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(ClsgloRxHubGeneral.ConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            DataSet oDS = null;
            
            //int _RetVal = 0;
            try
            {

                if (_PatientId > 0)
                {
                    oDB.Connect(false);

                    oParameters.Add("@nPatientID", _PatientId, ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@nLoginProviderid", nLoginProviderid, ParameterDirection.Input, SqlDbType.BigInt);

                    //SqlDataAdapter oDataAdapter = new SqlDataAdapter(oCmd);
                    //oDataAdapter.Fill(oDS);

                    oDB.Retrive("gsp_GetPatientInfo_270", oParameters, out oDS);

                    oDB.Disconnect();
                    return oDS;

                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                oDB.Disconnect();
                return oDS;
            }
            finally
            {
                //SLR: free oParameters, ODB
                if (oParameters != null)
                {
                    oParameters.Dispose();
                    oParameters = null;
                }
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                    oDB = null;
                }
            }
            return oDS;

        }

        public DataTable Get271ResponseDetails(long _PatientId, DateTime CurrentDateTime, DateTime SubstractedDateTime)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(ClsgloRxHubGeneral.ConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            DataTable odt271Info = null;
            
            //int _RetVal = 0;
            try
            {

                if (_PatientId > 0)
                {


                    oParameters.Add("@nPatientID", _PatientId, ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@dtCurrentDate", CurrentDateTime, ParameterDirection.Input, SqlDbType.DateTime);
                    oParameters.Add("@dtSubstracted", SubstractedDateTime, ParameterDirection.Input, SqlDbType.DateTime);
                    //SqlDataAdapter oDataAdapter = new SqlDataAdapter(oCmd);
                    //oDataAdapter.Fill(oDS);

                    oDB.Connect(false);
                    oDB.Retrive("gsp_Get271ResponseInformation", oParameters, out odt271Info);
                    oDB.Disconnect();

                }
                return odt271Info;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                oDB.Disconnect();
                if (odt271Info != null)
                {
                    odt271Info.Dispose();
                    odt271Info = null;
                }

                return odt271Info;
            }
            finally
            {
                //SLR: Finally free oParameters, oDB 
                if (oParameters != null)
                {
                    oParameters.Dispose();
                    oParameters = null;
                }
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                    oDB = null;
                }
                
                //SLR: Following should not be disposed since it is a return
                //if (odt271Info != null)
                //{
                //    odt271Info.Dispose();
                //    odt271Info = null;
                //}
            }

        }

        public string GetSingleRecord(string SQLquery)
        {
            // Data Reader 
            string Result = "";

            SqlCommand ocmd = new SqlCommand(SQLquery, gloDBConnection);
            try
            {
                //Check Connection 
                if (gloDBConnection.State == ConnectionState.Closed)
                {
                    _ErrorMessage = "Please check database connection";
                    return _ErrorMessage;
                }

                Result = ocmd.ExecuteScalar().ToString();

                if (Result != "")
                {
                    return Result;
                }
                else
                {
                    return Result;
                }
            }
            catch (Exception objError)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, objError.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                _ErrorMessage = objError.Message;
                return _ErrorMessage;
            }
            //SLR: Finally free ocmd
            finally
            {
                if (ocmd != null)
                {
                    ocmd.Parameters.Clear();
                    ocmd.Dispose();
                    ocmd = null;
                }
            }
        }

        //Connections 
        public bool Connect(string ConnectionStrings)
        {
            try
            {
                if (!string.IsNullOrEmpty((ConnectionStrings)))
                {
                    {
                        if (gloDBConnection.State != ConnectionState.Closed) gloDBConnection.Close();
                        gloDBConnection.ConnectionString = ConnectionStrings;
                        if (gloDBConnection.State == ConnectionState.Closed)
                        {
                            gloDBConnection.Open();
                        }
                    }
                    return true;
                }
                else
                {
                    _ErrorMessage = "Connection information is not valid";
                    return false;

                }
            }
            catch (Exception objEror)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, objEror.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                _ErrorMessage = objEror.Message;
                return false;
            }
        }

        public bool Disconnect()
        {
            try
            {
                if (gloDBConnection.State != ConnectionState.Closed) gloDBConnection.Close();
                return true;
            }
            catch (Exception objError)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, objError.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                _ErrorMessage = objError.Message;
                return false;
            }
        }
        //------------------------ 


        private static string GetConnectionString(string strSQLServerName, string strDatabase, bool WindowsAuthentication) // ERROR: Optional parameters aren't supported in C# string strUserName, [System.Runtime.InteropServices.OptionalAttribute, System.Runtime.InteropServices.DefaultParameterValueAttribute("")] // ERROR: Optional parameters aren't supported in C# string strPassword) 
        {
            // Variable to store SQL Connection String 
            string strConnectionString = null;

            //Check the SQL Server Authentication 
            if (WindowsAuthentication == true)
            {
                //Build Connection String by Windows Authentication 
                strConnectionString = "SERVER=" + strSQLServerName + ";DATABASE=" + strDatabase + ";Integrated Security=SSPI";
            }
            else
            {
                //Build Connection String by SQL Server Authentication 
                // strConnectionString = "SERVER=" + strSQLServerName + ";UID=" + strUserName + ";PWD=" + strPassword + ";DATABASE=" + strDatabase; 
            }

            //Return Builded connection string 
            return strConnectionString;
        }

        public static bool IsConnect(string strSQLServerName, string strDatabase) // ERROR: Optional parameters aren't supported in C# bool WindowsAuthentication, [System.Runtime.InteropServices.OptionalAttribute, System.Runtime.InteropServices.DefaultParameterValueAttribute("")] // ERROR: Optional parameters aren't supported in C# string strUserName, [System.Runtime.InteropServices.OptionalAttribute, System.Runtime.InteropServices.DefaultParameterValueAttribute("")] // ERROR: Optional parameters aren't supported in C# string strPassword) 
        {
            //Create the object of SQL Connection class 
            SqlConnection objCon = new SqlConnection();
            try
            {
                //Assign the connection string 
                objCon.ConnectionString = GetConnectionString(strSQLServerName, strDatabase, true);
                //Open the connection 
                objCon.Open();

                //Connection to SQL Server database successfully established 
                return true;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                return false;
            }
            finally
            {
                //Close the connection 
                objCon.Close();
                //Connection to SQL Server database is not established 
                //SLR: Free objCOn and then
                objCon.Dispose();
                objCon = null;
            }

        }

        public string ErrorMessage
        {
            get { return _ErrorMessage; }
            set { _ErrorMessage = value; }
        }

        public clsgloRxHubDBLayer()
            : base()
        {
            //_DBParameters = new gloDBSupport.DBParameters(); 
        }

        //protected override void Finalize() 
        //{ 
        //    _DBParameters = null; 
        //    base.Finalize(); 
        //}
        private bool disposedValue = false;
        // To detect redundant calls 

        // IDisposable 
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {
                    if (gloDBConnection != null)
                    {
                        if (gloDBConnection.State != ConnectionState.Closed) { gloDBConnection.Close(); }
                        gloDBConnection.Dispose();
                        gloDBConnection = null;

                    }
                }
                // TODO: free managed resources when explicitly called 
                //SLR: Free gloDBConnection
              
            }

            // TODO: free shared unmanaged resources 
            this.disposedValue = true;
        }

        #region " IDisposable Support "
        // This code added by Visual Basic to correctly implement the disposable pattern. 
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(ByVal disposing As Boolean) above. 
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

        public DateTime RetrieveLastFillDate(Int64 nPatientID)
        {
            try
            {
                Connect(ClsgloRxHubGeneral.ConnectionString);

                //Check Connection 
                if (gloDBConnection.State == ConnectionState.Closed)
                {
                    _ErrorMessage = "Please check database connecion";
                    //return false;
                }


                string strQuery = "select convert(datetime,sDrugLastFillDate,101) as sDrugLastFillDate from  RxH_HistoryResponse_DTL where nPatientID=" + nPatientID + " order by convert(datetime,sDrugLastFillDate,101) asc";
                // string strQuery = "select top 1 sDrugLastFillDate from RxH_HistoryResponse_DTL where nPatientID=2 order by convert(datetime ,sDrugLastFillDate) desc";

                // string strQuery = "select top 1 sDrugLastFillDate from  RxH_HistoryResponse_DTL where nPatientID=" + nPatientID + " order by convert(datetime ,sDrugLastFillDate) desc ";
                SqlCommand cmd = new SqlCommand(strQuery, gloDBConnection);
                DataTable dt = new DataTable();
                SqlDataAdapter adpt = new SqlDataAdapter(cmd);
                adpt.Fill(dt);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        dtLastfillDate = Convert.ToDateTime(dt.Rows[0]["sDrugLastFillDate"]);
                    }


                }
                //SLR: Finaly free dt, adpt, cmd
                if (dt != null)
                {
                    dt.Dispose();
                    dt = null;
                }
                if (adpt != null)
                {
                    adpt.Dispose();
                    adpt = null;
                }
                if (cmd != null)
                {
                    cmd.Parameters.Clear();
                    cmd.Dispose();
                    cmd = null;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                throw ex;
            }
            //return collection
            
            
            return dtLastfillDate;

        }
        
    }
}
