using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data; 
using gloEMRGeneralLibrary.gloEMRDatabase;
using System.Windows.Forms;
using System.Collections; 
namespace gloCommunity.Classes
{
    //  ≈ 
    class clsformgallery
    {
        public DataTable getCPT()
        
        {

         //   string msg = "";
         //   DBParameter oParamater = default(DBParameter);
            DataBaseLayer oDB = new DataBaseLayer();
            try
            {
                //oParamater = new DBParameter();
                //oParamater.DataType = SqlDbType.Int;
                //oParamater.Direction = ParameterDirection.Input;
                //oParamater.Name = "@Flag";
                //oParamater.Value = 1;
                //oDB.DBParametersCol.Add(oParamater);
                //oParamater = null;
                string strQuery = "glocomm_getAssociated_CPTTemplate";
                DataTable dtcpt = oDB.GetDataTable(strQuery);

                return dtcpt;
            }

            catch
            {
                return null;
            }
        }


        public DataTable getAssociatedCPTCode()
        {
     
         //   DBParameter oParamater = default(DBParameter);
            DataBaseLayer oDB = new DataBaseLayer();
            try
            {
                //oParamater = new DBParameter();
                //oParamater.DataType = SqlDbType.Int;
                //oParamater.Direction = ParameterDirection.Input;
                //oParamater.Name = "@Flag";
                //oParamater.Value = 1;
                //oDB.DBParametersCol.Add(oParamater);
                //oParamater = null;
                string strQuery = "glocomm_getAssociated_CPTTemplateCode";
                DataTable dtcpt = oDB.GetDataTable(strQuery);

                return dtcpt;
            }

            catch
            {
                return null;
            }




        }

        public DataTable getCorresTemplate(Int64 CPTID)
        {
           //   string msg = "";
            DBParameter oParamater = default(DBParameter);
            DataBaseLayer oDB = new DataBaseLayer();
            try
            {

                  oParamater = new DBParameter();
                oParamater.DataType= SqlDbType.BigInt;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@nCPTID";
                oParamater.Value = CPTID;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;



                DataTable oResultTable = oDB.GetDataTable("glocomm_gsp_scanCPTAssociation");
             return oResultTable;
            }

            catch
            {
                return null;
            }
           
        //   Dim dt As DataTable
        //'''' To Get Already Associated Template with Selected CPT
        //dt = objclsCPTAssociation.FetchCPTforUpdate(associatenode.Key)
        //Dim i As Integer
        //For i = 0 To dt.Rows.Count - 1
        //    ''''Add Templaets to cpt node 
        //    associatenode.Nodes.Add(New myTreeNode(dt.Rows(i).Item(1), dt.Rows(i).Item(0)))
        //Next
        //trvCPTAssociation.ExpandAll()
        //trvCPTAssociation.Select()
        }

        public DataTable getTemplateData(ArrayList Tempnames)
        {
          //  ≈ 
          //  string msg = "";
          //  DBParameter oParamater = default(DBParameter);
            DataBaseLayer oDB = new DataBaseLayer();
            string strTempname = "";
            for (int len = 0; len < Tempnames.Count; len++)
            {
                if (Tempnames[len].ToString().Trim() != "")
                    strTempname += "'" + Tempnames[len].ToString().Substring(0, Tempnames[len].ToString().IndexOf("≈")) + "',";  
            }
            if (strTempname.Trim().Length > 0)
                strTempname = strTempname.Substring(0, strTempname.Length - 1);
            if (strTempname.Trim().Length > 0)
            {
                try
                {
                    DataTable oResultTable = oDB.GetDataTable_Query("select  sTemplateName as TemplateName,sDescription as  Description , sCategoryName as 'CategoryName' from TemplateGallery_MST where sTemplateName in(" + strTempname + ")");
                    return oResultTable;
                }

                catch
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
            //   Dim dt As DataTable
            //'''' To Get Already Associated Template with Selected CPT
            //dt = objclsCPTAssociation.FetchCPTforUpdate(associatenode.Key)
            //Dim i As Integer
            //For i = 0 To dt.Rows.Count - 1
            //    ''''Add Templaets to cpt node 
            //    associatenode.Nodes.Add(New myTreeNode(dt.Rows(i).Item(1), dt.Rows(i).Item(0)))
            //Next
            //trvCPTAssociation.ExpandAll()
            //trvCPTAssociation.Select()
        }



        public DataTable getTemplateNames(string Templatename)
        {
          //  string msg = "";
          //  DBParameter oParamater = default(DBParameter);
            DataBaseLayer oDB = new DataBaseLayer();
           
            if (Templatename.Trim().Length > 0)
            {
                try
                {
                    DataTable oResultTable = oDB.GetDataTable_Query("select  nTemplateID, sTemplateName as TemplateName, sCategoryName as 'CategoryName' from TemplateGallery_MST where sTemplateName in(" + Templatename + ")");
                    return oResultTable;
                }

                catch
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
         
        }

        
        
        public bool CompareXMlData(DataTable local, ref DataTable server, string FilePath)
        {
            bool changedata = false;

            DialogResult Result;
            ArrayList remflow = new ArrayList();
            foreach (DataRow dr in local.Rows)
            {
                DataRow[] drr = null;
                drr = server.Select("CPTCode = '" + dr["CPTCode"].ToString().Replace("'", "''") + "'");
                if (drr.Length > 0)
                {

                    if (remflow.Contains(dr["CPTCode"].ToString().Trim()) == false)
                    {
                        Result = MessageBox.Show("For Form Gallery CPT Code  '" + dr["CPTCode"].ToString().Replace("'", "''") + "'    Already Exist Do you want to Overwrite ?", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                        if (Result == DialogResult.Yes)
                        {
                            remflow.Add(dr["CPTCode"].ToString().Trim());
                            for (int lendr = 0; lendr < drr.Length; lendr++)
                            {
                                server.Rows.Remove(drr[lendr]);
                            }

                               server.ImportRow(dr);
                                changedata = true;
                         }
                       

                    }
                }

                else
                {
                    DataRow[] drrlocal = local.Select("CPTCode = '" + dr["CPTCode"].ToString().Replace("'", "''") + "'");
                    if (drrlocal.Length > 0)
                    {
                        if (remflow.Contains(drrlocal[0]["CPTCode"].ToString().Trim().Replace("'", "''")) == false)
                        {

                            remflow.Add(drrlocal[0]["CPTCode"].ToString().Trim().Replace("'", "''"));
                            for (int lenlocal = 0; lenlocal < drrlocal.Length; lenlocal++)
                            {

                                server.ImportRow(drrlocal[lenlocal]);
                                changedata = true;
                            }
                        }
                    }

                }

            }

            if (changedata == true)
                server.WriteXml(FilePath);

            return changedata;
        }



        public bool AddNewTemplateGallery(long ID, string TemplateName,  string categoryName,Int64 ProviderID, byte[] Description)
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


                //oParamater = new DBParameter();
                //oParamater.DataType = SqlDbType.BigInt;
                //oParamater.Direction = ParameterDirection.Input;
                //oParamater.Name = "@MachineID";
                //oParamater.Value = 111111111; //GetPrefixTransactionID();
                //oDB.DBParametersCol.Add(oParamater);
                //oParamater = null;

                oDB.ExecuteNon_Query("gsp_glocomm_InUpFormTemplateGallery_MST");
                return true;
            }
            catch //(Exception ex)
            {
                return false;
            }
            finally
            {
                oDB.Dispose();
                oDB = null;

            }

        }


        public bool  InsertCptTemplate(string TemplateNames,string CptCode)
        {
            DataBaseLayer oDB = new DataBaseLayer();
            DBParameter oParamater = default(DBParameter);
            try
            {
               
                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.VarChar;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@TemplateNames";
                oParamater.Value = TemplateNames;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;


                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.VarChar;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@CptCode";
                oParamater.Value = CptCode;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                
           oDB.ExecuteNon_Query("gsp_glocomm_InUpCPTTemplatewithTemplateNames");
           return true;  
            }
            catch //(Exception ex)
            {
                return false;  
            }
            finally
            {
                oDB.Dispose();
                oDB = null;

            }

        }

    
    }
}
