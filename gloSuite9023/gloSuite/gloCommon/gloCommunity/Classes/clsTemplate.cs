using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using gloEMRGeneralLibrary.gloEMRDatabase;
using System.Data;
using System.Windows.Forms;
namespace gloCommunity.Classes
{
    class clsTemplate
    {
        DataSet ds = new DataSet();
        public DataTable GetAllProvider()
        {
            DataBaseLayer oDB = new DataBaseLayer();
            try
            {
               
                //Dim oParamater As DBParameter

                DataTable oResultTable = new DataTable();

                oResultTable = oDB.GetDataTable("gsp_FillProvider_MST");

                if ((oResultTable != null))
                {
                    return oResultTable;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.Select, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                //MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;

            }
            finally
            {
                oDB = null;
            }
        }

        public DataTable GetAllCategory(string CategoryType)
        {

            DataBaseLayer oDB = new DataBaseLayer();
            DBParameter oParamater = default(DBParameter);
            DataTable oResultTable = new DataTable();
            try
            {
                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.VarChar;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@CategoryType";
                oParamater.Value = CategoryType;// "Template";
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                oResultTable = oDB.GetDataTable("gsp_FillCategory_MST");

                if ((oResultTable != null))
                {
                    return oResultTable;
                }
                return oResultTable;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.Select, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                //MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return oResultTable;
            }
            finally
            {
                oDB.Dispose(); 
                oDB = null;
            
            }
        }

        public DataSet GetAllTemplates(long ID, long ProviderID = 0)
        {

            DataBaseLayer oDB = new DataBaseLayer();
            DBParameter oParamater = default(DBParameter);
            DataTable oResultTable = new DataTable();
            DataSet dsTemplates = new DataSet();
            try
            {
                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.BigInt;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@ID";
                oParamater.Value = ID;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.BigInt;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@ProviderID";
                if (ProviderID != 0)
                {
                    oParamater.Value = ProviderID;
                }
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                oResultTable = oDB.GetDataTable("gsp_ViewTemplateGallery_MST");
                ds.Clear();
                if ((oResultTable != null))
                {
                    dsTemplates.Tables.Add(oResultTable.Copy());
                }
                return dsTemplates;

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                //MessageBox.Show(ex.ToString(), "Template Gallery", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return dsTemplates;
            }
            finally
            {
                oDB.Dispose();
                oDB = null;
            
            }

        }

        private  DataTable SelectTemplateGallery(Int64 ID)
        {
            DataBaseLayer oDB = new DataBaseLayer();
            DBParameter oParamater = default(DBParameter);
            DataTable oResultTable = new DataTable();
            DataView dv = new DataView();
            try
            {
                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.BigInt;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@TemplateID";
                oParamater.Value = ID;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                oResultTable = oDB.GetDataTable("gsp_ScanTemplateGallery_MST");
                if ((oResultTable != null))
                {
                    dv = oResultTable.DefaultView;
                }
                return oResultTable;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.Select, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                //MessageBox.Show(ex.ToString(),clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return oResultTable;
            }
            finally
            {
                oDB.Dispose();
                oDB = null;
            
            }
        }

        public string Fill_TemplateGallery(long ID)
        {
            string strFileName = "";
            try
            {
                DataTable dt = SelectTemplateGallery(ID);
                //DateTime _dtCurrentDateTime = DateTime.Now;
                string _TemplateName = gloGlobal.clsFileExtensions.GetUniqueDateString("MMddyyyyHHmmssffff") + ".docx";//" " + _dtCurrentDateTime.Millisecond.ToString() + ".docx";
                //string _TemplateName = dt.Rows[0][0].ToString();
                strFileName = gloSettings.FolderSettings.AppTempFolderPath + _TemplateName;
                strFileName = clsGeneral.GenerateFile(dt.Rows[0][3], strFileName);
            }
            catch //(Exception ex)
            {
                //MessageBox.Show(ex.Message);
                strFileName = "";
            }
            return strFileName;
        }





        public DataTable GetContentTitle()
        {
            DataBaseLayer oDB = new DataBaseLayer();
     
            DataTable oResultTable = new DataTable();
          

            try
            {
                oResultTable = oDB.GetDataTable("GetContentTitle");


                return oResultTable;

            }
            catch //(Exception ex)
            {
                  return null;

            }
            finally
            {
                oDB.Dispose();
                oDB = null;
            
            }
        }







        public void AddNewTemplateGallery(long ID, string TemplateName, long CategoryID, string categoryName, long ProviderID, byte[] Description)
        {
            DataBaseLayer oDB = new DataBaseLayer();
            DBParameter oParamater = default(DBParameter);
         try
            {
                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.BigInt;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@TemplateID";
                oParamater.Value = ID;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.VarChar;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@TemplateName";
                oParamater.Value = TemplateName;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.BigInt;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@CategoryID";
                if (CategoryID > 0)
                {
                    oParamater.Value = CategoryID;
                }
                else
                {
                    oParamater.Value = -1;
                }
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.VarChar;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@CategoryName";
                oParamater.Value = categoryName;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.BigInt;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@ProviderID";
                if (ProviderID > 0)
                {
                    oParamater.Value = ProviderID;
                }
                else
                {
                    oParamater.Value = 0;
                }

                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.Image;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@Description";
                //  Dim objword As New clsWordDocument
                //' To convert from Object to Binary Format
                oParamater.Value = Description;
                //objword.ConvertFiletoBinary(Description)

                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;


                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.BigInt;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@MachineID";
                
                oParamater.Value =clsGeneral.GetPrefixTransactionID(); 
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                // oDB.ExecuteNon_Query("gsp_InUpExternalTemplateGallery_MST");
                oDB.ExecuteNon_Query("gsp_glocomm_InUpExternalTemplateGallery_MST");    
            // []
             }
            catch //(Exception ex)
            {
            }
           finally
             {
             oDB.Dispose();
             oDB = null;

            }

        }




        //Problem : 00000163
        //Issue : When creating liquid data fields, the sorting does not order itself in a logical way (it's completely random). 
        //Change : New parameter nSequenceNo added to below function AddDataFieldValue() and value passed to SP Insert_DataFields.

        public Int64 AddDataFieldValue(Int64 nElementId, Int64 nGroupID, string strFieldName, string strDataType, bool bIsRequired, myList ItemList = null, string strFieldCategory = "", Int32 nSequenceNo = 0)
        {
            DataBaseLayer oDB = default(DataBaseLayer);
            DBParameter oParamater = default(DBParameter);
            Int64 ElementID = 0;
            try
            {
                oDB = new DataBaseLayer();


                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.VarChar;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@ElementName";
                if (nGroupID != 0)
                {
                    if (strDataType == "Multiple Selection" | (strDataType == "Boolean" & (ItemList == null) == false))
                    {
                        oParamater.Value = ItemList.Value.ToString();
                    }
                    else
                    {
                        oParamater.Value = strFieldName;
                    }
                }
                else
                {
                    oParamater.Value = strFieldName;
                }

                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.VarChar;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@ElementType";
                oParamater.Value = strDataType;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.Bit;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@bIsMandatory";

                if (bIsRequired)
                {
                    oParamater.Value = 1;
                }
                else
                {
                    oParamater.Value = 0;
                }

                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;


                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.BigInt;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@GroupID";
                oParamater.Value = nGroupID;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.BigInt;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@ColumnID";
                oParamater.Value = 0;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.VarChar;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@sCategoryName";
                if (nGroupID != 0)
                {
                    if (strDataType == "Table" | strDataType == "Group")
                    {
                        if ((ItemList == null))
                        {
                            oParamater.Value = "";
                        }
                        else
                        {
                            oParamater.Value = ItemList.HistoryCategory.ToString();
                        }
                    }
                    else
                    {
                        oParamater.Value = "";
                    }
                }
                else
                {
                    oParamater.Value = strFieldCategory;
                }
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;


                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.VarChar;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@sItemName";
                if (strDataType == "Table" | strDataType == "Group")
                {
                    if ((ItemList == null))
                    {
                        oParamater.Value = "";
                    }
                    else
                    {
                        oParamater.Value = ItemList.HistoryItem.ToString();
                    }
                }
                else
                {
                    oParamater.Value = "";
                }
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;


                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.BigInt;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@MachineID";
              //  oParamater.Value = GetPrefixTransactionID();
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.BigInt;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@nControlType";
                if (strDataType == "Multiple Selection" | strDataType == "Table" | strDataType == "Group")
                {
                    if ((ItemList == null))
                    {
                        oParamater.Value = 0;
                    }
                    else
                    {
                        oParamater.Value = Convert.ToInt32(ItemList.ControlType);
                    }
                }
                else
                {
                    oParamater.Value = 0;
                }
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.VarChar;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@sAssociatedCategory";
                if (nGroupID != 0)
                {
                    if (strDataType == "Table" | strDataType == "Group")
                    {
                        if ((ItemList == null))
                        {
                            oParamater.Value = "";
                        }
                        else
                        {
                            oParamater.Value = ItemList.AssociatedCategory.ToString();
                        }
                    }
                    else
                    {
                        oParamater.Value = "";
                    }
                }
                else
                {
                    oParamater.Value = strFieldCategory;
                }
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;


                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.VarChar;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@sAssociateditem";
                if (strDataType == "Table" | strDataType == "Group")
                {
                    if ((ItemList == null))
                    {
                        oParamater.Value = "";
                    }
                    else
                    {
                        oParamater.Value = ItemList.AssociatedItem.ToString();
                    }
                }
                else
                {
                    oParamater.Value = "";
                }
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;


                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.VarChar;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@sAssociatedProperty";
                if (strDataType == "Multiple Selection" | strDataType == "Table" | strDataType == "Group" | strDataType == "Boolean")
                {
                    if ((ItemList == null))
                    {
                        oParamater.Value = "";
                    }
                    else
                    {
                        oParamater.Value = ItemList.AssociatedProperty.ToString();
                    }
                }
                else
                {
                    oParamater.Value = "";
                }
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.Int;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@nSequenceNo";
                oParamater.Value = nSequenceNo;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.BigInt;
                oParamater.Direction = ParameterDirection.InputOutput;
                oParamater.Name = "@ElementID";
                oParamater.Value = nElementId;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;



                ElementID = oDB.Add("Insert_DataFields");
                return ElementID;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                return 0;
            }
            finally
            {
                oDB.Dispose();
                oDB = null;

            }

        }




        
    }
}
