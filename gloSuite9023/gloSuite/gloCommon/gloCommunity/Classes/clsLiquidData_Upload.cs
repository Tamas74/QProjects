using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;
using gloEMRGeneralLibrary.gloEMRDatabase;

namespace gloCommunity.Classes
{
    class clsLiquidData_Upload
    {
        public DataTable GetLiquidData()
        {

            DataBaseLayer oDB = new DataBaseLayer();
            // Dim oParamater As DBParameter
            DataTable oResultTable = new DataTable();
            string strSQL = null;
            try
            {   
                //ADD ALL LIQUID DATA ASCENDINGLY IT RESOLVE THE PROBLEM THAT IT IS CHANING SEQUENCE OF RECORDS ON SAVE & CLOSE
                strSQL = "select nElementID,sElementName,sElementType,bIsMandatory FROM LiquidData_MST WHERE nGroupId = 0 order by sElementName ASC";
                oResultTable = oDB.GetDataTable_Query(strSQL);

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
                //gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.Select, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                //MessageBox.Show(ex.ToString(), "Liquid Data", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            finally
            {
                oDB = null;
            }

        }

        public DataTable GetDataField(Int64 nElementId)
        {
            DataBaseLayer oDB = new DataBaseLayer();
            DataTable oResult = new DataTable();
            string strSQL = null;
            try
            {
                //'Query to retrieve the complete
                //Problem : 00000163
                //Issue : When creating liquid data fields, the sorting does not order itself in a logical way (it's completely random). 
                //Change : Order By nSequenceNo clause added to retrieve data as per sequence maintained by user.

                strSQL = "Select nElementID, sElementName, sElementType, bIsMandatory, nGroupID, nColumnID, sCategoryName, sItemName, nControlType, sAssociatedCategory, sAssociateditem, sAssociatedProperty FROM LiquidData_MST WHERE nElementId = " + nElementId + " OR nGroupID= " + nElementId + " order by nSequenceNo";
                oResult = oDB.GetDataTable_Query(strSQL);
                if ((oResult != null))
                {
                    return oResult;
                }
                else
                {
                    return null;
                }
            }
            catch //(Exception ex)
            {
                //MessageBox.Show(ex.Message);
                //gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.Select, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                return null;
            }
            finally
            {
                oDB = null;
                oResult.Dispose();
                oResult = null;
            }
        }
    }
}
