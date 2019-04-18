using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace gloBilling
{
    class ClsCommonCPT
    {

        string _databaseConnectionString = string.Empty;
        Int16 _CommonType =1;
        public ClsCommonCPT(string databaseConnectionString, Int16 CommonType)
        {
            _databaseConnectionString = databaseConnectionString;
            _CommonType = CommonType; 
        }

        public Boolean InsertCommonType(DataTable dtCPTType,int CommonType)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            bool _returnvalue = false;
            try
            {
                oParameters.Add("@TvpCommonCPT", dtCPTType, ParameterDirection.Input, SqlDbType.Structured);
                oParameters.Add("@CommonType", CommonType, ParameterDirection.Input , SqlDbType.TinyInt);

                oDB.Connect(false);
                //oDB.Execute("SaveCharges_TVP", oParameters, out  _oTransactionMstID, out _oNewClaimNo);
                object  value ;
                int    oOUT = oDB.Execute("gsp_InsertCommonCPTType", oParameters, out value);
               
                oDB.Disconnect();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Dispose();
                    oDB = null;
                }
                if (oParameters != null)
                {
                    oParameters.Clear();  
                    oParameters.Dispose();
                    oParameters = null;
                }

            }
            return _returnvalue;
        }


        public DataTable getAllCommonCPT()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            DataTable dtresult = null;
          //  bool _returnvalue = false;
            try
            {

                oParameters.Add("@CommonType", _CommonType, ParameterDirection.Input, SqlDbType.TinyInt);

                oDB.Connect(false);

               // object value;

                oDB.Retrive("gsp_GetCommonCPTType", oParameters, out dtresult);

                oDB.Disconnect();
                return dtresult;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return null;
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Dispose();
                    oDB = null;
                }
                if (oParameters != null)
                {
                    oParameters.Clear();  
                    oParameters.Dispose();
                    oParameters = null;
                }

            }
        }


        public DataTable DeleteCommonTypeCPTId(Int64 CommonCPTID,Int16 CPTType)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            DataTable dtresult = null;
          //  bool _returnvalue = false;
            try
            {

                oParameters.Add("@nCPTCommonID", CommonCPTID, ParameterDirection.Input, SqlDbType.Decimal);
                oParameters.Add("@nCPTType", CPTType, ParameterDirection.Input, SqlDbType.SmallInt);
                oDB.Connect(false);

                

                oDB.Retrive("gsp_DeleteCommonCPTType", oParameters, out dtresult);

                oDB.Disconnect();
                return dtresult;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return null;
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Dispose();
                    oDB = null;
                }
                if (oParameters != null)
                {
                    oParameters.Clear();  
                    oParameters.Dispose();
                    oParameters = null;
                }

            }

        }


        public DataTable  SelectCommonTypeCPT()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            DataTable dtresult = null;
         //   bool _returnvalue = false;
            try
            {
               
                oParameters.Add("@CommonType", _CommonType, ParameterDirection.Input, SqlDbType.TinyInt);

                oDB.Connect(false);
                
               

               oDB.Retrive("gsp_SelectCommonCPTType", oParameters, out dtresult);

                oDB.Disconnect();
                return dtresult; 
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return null;
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Dispose();
                    oDB = null;
                }
                if (oParameters != null)
                {
                    oParameters.Clear();  
                    oParameters.Dispose();
                    oParameters = null;
                }

            }
          
        }
    
    }
}
