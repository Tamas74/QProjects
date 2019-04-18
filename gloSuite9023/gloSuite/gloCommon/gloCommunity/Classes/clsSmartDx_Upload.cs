using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using gloEMRGeneralLibrary.gloEMRDatabase;
using System.Windows.Forms;

namespace gloCommunity.Classes
{
    class clsSmartDx_Upload
    {
        
        public DataTable FetchAssociatedICD9()
        {
            DataTable oResultTable = null;
            try
            {
                DataBaseLayer oDB = new DataBaseLayer();
                //Dim oParamater As DBParameter


                oResultTable = oDB.GetDataTable("gsp_AssociatedICD9");

                if ((oResultTable != null))
                {
                    return oResultTable;
                }
                else
                {
                    return null;
                }
            }
            catch //(Exception ex)
            {
                //MessageBox.Show(ex.ToString());
                oResultTable = null;
            }
            finally
            {
                if (oResultTable != null)
                {
                    oResultTable.Dispose();
                    oResultTable = null;
                }
            }

            return oResultTable;
        }

        public DataTable FetchICD9forUpdate(long id)
        {
            DataTable dt = null;
            DataBaseLayer oDB = new DataBaseLayer();
            DBParameter oParamater = default(DBParameter);

            try
            {
                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.Decimal;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@nICD9ID";
                oParamater.Value = id;           
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                

                // object obj = da.SelectCommand.ExecuteScalar();
                dt = oDB.GetDataTable("gsp_scanICD9Association");
                return dt;
                //' da.Fill(dt)
                //return obj.ToString();

            }
            catch //(Exception ex)
            {
                //gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.Select, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                //MessageBox.Show(ex.ToString(), clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            finally
            {
                oDB = null;
            }
        }

        public DataTable FetchDrugByNDCCodepdate(string sNDCCode)
        {
            DataTable dt = null;
            DataBaseLayer oDB = new DataBaseLayer();
            DBParameter oParamater = default(DBParameter);

            try
            {
                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.VarChar;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@sNDCCode";
                oParamater.Value = sNDCCode;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;



                // object obj = da.SelectCommand.ExecuteScalar();
                dt = oDB.GetDataTable("GE_getDrug_ByNDCCode");
                return dt;
                //' da.Fill(dt)
                //return obj.ToString();

            }
            catch //(Exception ex)
            {
                //gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.Select, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                //MessageBox.Show(ex.ToString(), clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            finally
            {
                oDB = null;
            }
        }
    }
}
