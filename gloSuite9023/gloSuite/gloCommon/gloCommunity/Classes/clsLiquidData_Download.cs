using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using gloEMRGeneralLibrary.gloEMRDatabase;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace gloCommunity.Classes
{
    class clsLiquidData_Download
    {
        public void ImportFromXML(string ImportXmlPath)
        {
            ///to Import Liquid Data from XML 
            DataBaseLayer oDB = new DataBaseLayer();
            DataSet Ds = new DataSet();
            DataTable dt = new DataTable();
            DataTable dtChk = new DataTable();
            string sSQL = null;
            Int32 sfx = 1;
           // Int32 cnt = 0;
            long eID = 0;
            long subID = 0;
            //' Create a new Connection and SqlDataAdapter
            SqlConnection sqlCon = default(SqlConnection);
            SqlDataAdapter sqlDA = default(SqlDataAdapter);
            DataSet DsNew = new DataSet();
            DataRow DtRw = null;

            try
            {
                if (File.Exists(ImportXmlPath))
                {
                    Ds.ReadXml(ImportXmlPath);
                    dt = Ds.Tables[0];
                    //Delete Specialty column
                    //dt.Columns.Remove("ClinicName");
                    try
                    {
                        dt.Columns.Remove("Specialty");
                    }
                    catch
                    {
                    }
                    try
                    {
                        dt.Columns.Remove("ClinicName");
                    }
                    catch
                    {
                    }

                    //Problem : 00000163
                    //Issue : When creating liquid data fields, the sorting does not order itself in a logical way (it's completely random). 
                    //Change : Commented the below logic which sorts the datatable and new logic added which will create the new column nSequenceNo in datatable and changes did to maintain the sequence.
                    // To be verified by Pramod

                    //DataView dv = new DataView();
                    //dv.Table = dt;
                    //dv.Sort = "nGroupId";

                    //dt = dv.ToTable();
                    //dt.AcceptChanges();
                    //Ds.AcceptChanges();
                    //
                    
                    if (dt != null && dt.Rows.Count > 0 && !dt.Columns.Contains("nSequenceNo"))
                    {
                        dt.Columns.Add("nSequenceNo", typeof(System.Int32));
                        //dt.Columns.Add("nSequenceNo", System.Type.GetType("System.Int32"));
                    }

                    sqlCon = new SqlConnection(clsGeneral.EMRConnectionString);
                    sqlDA = new SqlDataAdapter("select nElementID, sElementName, sElementType, bIsMandatory, nGroupID, nColumnID, sCategoryName, sItemName, nControlType, sAssociatedCategory, sAssociateditem, sAssociatedProperty,nSequenceNo from LiquidData_MST", sqlCon);
                    sqlDA.Fill(DsNew, "LiquidData_MST");

                    ///''''''''''''''chk for duplicates
                    for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
                    {
                        string _strElementName = "";

                            _strElementName = dt.Rows[i]["selementname"].ToString().Trim().Replace('˜', '≈');
                  
                            sSQL = "select count(*) FROM LiquidData_MST where selementname = '" + _strElementName.ToString().Replace("'","''")   + "' and ngroupid=0  ";
                            dtChk = oDB.GetDataTable_Query(sSQL);
                            if (dtChk.Rows.Count > 0)
                            {
                                if (Convert.ToInt32(dtChk.Rows[0][0]) > 0)
                                {


                                    if (_strElementName.Contains("≈"))
                                    {
                                     
                                        string strelename = _strElementName + "≈";// _strElementName.Substring(0, _strElementName.Trim().LastIndexOf("≈") + 1);   
                                        sSQL = "select max(coalesce(cast(reverse(substring(reverse(selementname) ,0,charindex('≈',reverse(selementname) ))) AS int),null,0))  FROM LiquidData_MST  where selementname like  '"+strelename +"%' and ngroupid=0  ";
                                     }
                                    else
                                    {
                                    sSQL = "select max(coalesce(cast(reverse(substring(reverse(selementname) ,0,charindex('≈',reverse(selementname) ))) AS int),null,0))  FROM LiquidData_MST  where selementname like  '" + dt.Rows[i]["selementname"].ToString().Trim() + "%' and ngroupid=0  ";

                                    }
                                    dtChk = oDB.GetDataTable_Query(sSQL);
                                    if (dtChk.Rows.Count > 0)
                                    {
                                        if (dtChk.Rows[0][0].ToString().Trim().Length == 0)
                                        {
                                            dtChk.Rows[0][0] = 0;
                                        }
   
                                        if (Convert.ToInt32(dtChk.Rows[0][0]) > 0)
                                        {
                                            if (_strElementName.Contains("≈"))
                                            {
                                                string sval = "≈" + (Convert.ToInt32(dtChk.Rows[0][0]) + 1);
                                                dt.Rows[i]["selementname"] = _strElementName + sval;  
                                            }
                                            else
                                            {
                                                dt.Rows[i]["selementname"] = _strElementName + "≈" + (Convert.ToInt32(dtChk.Rows[0][0]) + 1);
                                            }
                                        }
                                        else
                                        {
                                            dt.Rows[i]["selementname"] = _strElementName + "≈" + sfx;
                                        }
                                        ///''''''''''''''
                                        if (Convert.ToInt64(dt.Rows[i]["ngroupid"]) == 0)
                                        {
                                            eID = Convert.ToInt64(oDB.GetDataValue("select dbo.GetPrefixTransactionID(0)", false));
                                            dt.Rows[i]["nelementid"] = eID;
                                        }
                                        ///''''''''''''''
                                    }
                                }

                               // else  //if specific element with selementname is not present inside DB then
                               // {
                                
                                




                               // }

                            }
                            ///''''''''''''''
                            if (eID != 0 & Convert.ToInt64(dt.Rows[i]["ngroupid"]) != 0)
                            {
                                if (subID == 0)
                                {
                                    subID = Convert.ToInt64(oDB.GetDataValue("select dbo.GetPrefixTransactionID(" + eID + ")", false));
                                }
                                else
                                {
                                    subID = Convert.ToInt64(oDB.GetDataValue("select dbo.GetPrefixTransactionID(" + subID + ")", false));
                                }
                                dt.Rows[i]["nelementid"] = subID;
                                dt.Rows[i]["ngroupid"] = eID;
                            }
                            ///''''''''''''''
                        //}

                                dt.Rows[i]["nSequenceNo"] = i;
                            
                    }
                    ///''''''''''''''

                    //for (Int32 i = 0; i <= Ds.Tables[0].Rows.Count - 1; i++)
                    //{
                    //    DtRw = DsNew.Tables[0].NewRow();
                    //    for (Int32 j = 0; j <= Ds.Tables[0].Columns.Count - 1; j++)
                    //    {
                    //        DtRw[j] = Ds.Tables[0].Rows[i][j].ToString();
                    //    }
                    //    DsNew.Tables[0].Rows.Add(DtRw);
                    //}

                    for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
                    {
                        DtRw = DsNew.Tables[0].NewRow();
                        for (Int32 j = 0; j <= dt.Columns.Count - 1; j++)
                        {
                            DtRw[j] = dt.Rows[i][j].ToString();
                        }
                        DsNew.Tables[0].Rows.Add(DtRw);
                    }

                    //DataView dv1 = new DataView();
                    //dv1 =  DsNew.;
                    //dv1.Sort = "sCategoryName";

                    //objdata = dv1.ToTable();
                    //objdata.AcceptChanges();

                    SqlCommandBuilder cmdBldr = new SqlCommandBuilder(sqlDA);
                    sqlDA.Update(DsNew, "LiquidData_MST");
                }
            }
            catch //(Exception ex)
            {
            }
            finally
            {
                oDB = null;
                sqlCon = null;
            }
            ///'''''Integrated by Mayuri as on 20100803 - to Import Liquid Data from XML 
        }

        public DataTable GetXmlData(string XmlPath, string TableName)
        {
            DataTable dt;
            try
            {
                if (File.Exists(XmlPath))
                {
                    DataSet newDataSet = new DataSet();
                    // Create a StreamReader to read the file.
                    StreamReader myStreamReader = new StreamReader(XmlPath);
                    // Read the XML document into the DataSet.
                    newDataSet.ReadXml(myStreamReader);
                    // Close the StreamReader.
                    myStreamReader.Close();
                    dt = newDataSet.Tables[TableName];
                    return dt;
                }
                else
                    return dt = null;
            }
            catch //(Exception ex)
            {
                //MessageBox.Show(ex.Message);
                return dt = null;
            }
        }
    }
}
