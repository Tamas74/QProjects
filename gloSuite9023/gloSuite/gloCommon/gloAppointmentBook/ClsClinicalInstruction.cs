using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace gloAppointmentBook
{
    public class ClsClinicalInstruction : IDisposable
    {
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {

                }
            }
            disposed = true;
        }
      
        
        public DataTable GetClinicalInstruction(Int64 nId)
        {
            DataTable _dtClicalInstractionMst = null;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            string _sqlQuery = string.Empty;
            try
            {
                oDB.Connect(false);
               
                if (nId==0)
                    _sqlQuery = " SELECT nID as Id,sClinicalInstruction as Instruction,sClinicalInstructionDtl as [Instruction Description]  FROM ClinicalInstruction_MST order by Instruction ";
                else
                    _sqlQuery = " SELECT nID as Id,sClinicalInstruction as Instruction,sClinicalInstructionDtl as [Instruction Description]  FROM ClinicalInstruction_MST where nID=" + nId + " order by Instruction ";

                oDB.Retrive_Query(_sqlQuery, out _dtClicalInstractionMst);
                _sqlQuery = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
            }
            return _dtClicalInstractionMst;
        }

        public void SaveClinicalInstruction(Int64 nId, string sInstruction, string sInstructionDesc)
        {
            
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            string _sqlQuery = string.Empty;
            try
            {
                oDB.Connect(false);
                oDBParameters.Add("@nID", nId, System.Data.ParameterDirection.InputOutput, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@sClinicalInstruction", sInstruction, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@sClinicalInstructionDtl", sInstructionDesc, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);            

                oDB.ExecuteScalar("INUP_ClinicalInstruction_Mst", oDBParameters);
                
             
            }
            catch (gloDatabaseLayer.DBException DBErr)
            {
                DBErr.ERROR_Log(DBErr.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                oDB.Disconnect();
                oDBParameters.Dispose();
                oDB.Dispose();
                _sqlQuery = null;
            }
            
           
        }



        public DataTable DeleteClinicalInstruction(Int64 nId)
        {
            DataTable _dtClicalInstractionMst = new DataTable();
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            string _sqlQuery = string.Empty;
            try
            {
                oDB.Connect(false);

                if (nId > 0)                   
                    _sqlQuery = " Delete from ClinicalInstruction_MST where nID=" + nId;

                oDB.Execute_Query(_sqlQuery);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
                _sqlQuery = null;
            }
            return _dtClicalInstractionMst;
        }


        
        
    }
}
