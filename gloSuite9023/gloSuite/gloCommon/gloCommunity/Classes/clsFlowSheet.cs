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
    class clsFlowSheet
    {

        public DataTable getFlowsheetName()
        {

            string strQuery = "glocomm_gsp_ViewFlowSheet_MST";
            DataBaseLayer oDB = new DataBaseLayer();
            try
            {
                DataTable getdata = null;

                getdata = oDB.GetDataTable(strQuery);
                return getdata;

            }

            catch
            {
                return null;
            }
            finally
            {
                oDB.Dispose();
                oDB = null;

            }

        }


        public DataTable getallFlowSheetbyParticularName(string FlowSheetName)
        {

            string strQuery = "glocomm_gsp_FlowSheetbyName_MST";
            DataBaseLayer oDB = new DataBaseLayer();
                DBParameter oParamater = default(DBParameter);

                try
                {
                    oParamater = new DBParameter();
                    oParamater.DataType = SqlDbType.VarChar;
                    oParamater.Direction = ParameterDirection.Input;
                    oParamater.Name = "@sFlowSheetName";
                    oParamater.Value = FlowSheetName;
                    oDB.DBParametersCol.Add(oParamater);
                    oParamater = null;
                    DataTable getdata = null;

                    getdata = oDB.GetDataTable(strQuery);
                    return getdata;

                }

                catch
                {
                    return null;
                }
                finally
                {
                    oDB.Dispose();
                    oDB = null;

                }


        }



        public void  InsertFlowSheetData(List<FlowSheetFields> objcls)//(string FlowsheetName,int Cols, int Colnum,string ColumnName,string Format,string FontName,int Width,int  FontSize,double ForeColor,bool bIsBold, bool   bIsItalic, bool bIsUnderline,string Alignment,double BackColor,bool status)
        {
            ArrayList arrflownew = new ArrayList();
            DialogResult Result;
            bool datainserted = false;
            string noflowsheet = "";
            Int64 floID = -1;
            for (int len = 0; len < objcls.Count; len++)
            {
               
               if(noflowsheet!=objcls[len].FlowsheetName)
               {
                   if (arrflownew.Contains(objcls[len].FlowsheetName.Trim()) == false)
                   {
                       arrflownew.Add(objcls[len].FlowsheetName.Trim());
                       floID = -1;


                       // @FlowsheetName
                       string _strQuery = "glocomm_chkflowsheetname";
                       DataBaseLayer objDB = new DataBaseLayer();
                       DBParameter objParamater = default(DBParameter);
                       string strobj = "";

                       try
                       {
                           objParamater = new DBParameter();
                           objParamater.DataType = SqlDbType.VarChar;
                           objParamater.Direction = ParameterDirection.Input;
                           objParamater.Name = "@FlowsheetName";
                           objParamater.Value = objcls[len].FlowsheetName;
                           objDB.DBParametersCol.Add(objParamater);
                           objParamater = null;
                           strobj = objDB.GetDataValue(_strQuery).ToString();
                       }
                       catch
                       {
                       }
                       finally
                       {
                           objDB.Dispose();
                           objDB = null;

                       }
                       if (strobj.Trim().Length > 1)
                       {
                           Result = MessageBox.Show(" Flowsheet '" + objcls[len].FlowsheetName + "'    Already Exist Do you want to Overwrite ?", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                           if (Result == DialogResult.Yes)
                           {

                               string _strQuery1 = "glocomm_delflowsheetbyname";
                               DataBaseLayer objDBlayer = new DataBaseLayer();
                               DBParameter objParm = default(DBParameter);
                               objDBlayer.DBParametersCol.Clear();

                               try
                               {
                                   objParm = new DBParameter();
                                   objParm.DataType = SqlDbType.VarChar;
                                   objParm.Direction = ParameterDirection.Input;
                                   objParm.Name = "@sFlowSheetName";
                                   objParm.Value = objcls[len].FlowsheetName;//.Replace("'", "''");
                                   objDBlayer.DBParametersCol.Add(objParm);
                                   objParm = null;
                                   bool bl = objDBlayer.Delete(_strQuery1);
                               }
                               catch
                               {
                               }
                               finally
                               {
                                   objDBlayer.Dispose();
                                   objDBlayer = null;

                               }


                              string strQuery = "glocomm_gsp_Insert_FlowSheet_MST";
                               DataBaseLayer oDB = new DataBaseLayer();
                               DBParameter oParamater = default(DBParameter);
                            //   DataTable getdata = null;
                               try
                               {

                                   oParamater = new DBParameter();
                                   oParamater.DataType = SqlDbType.Decimal;
                                   oParamater.Direction = ParameterDirection.Input;
                                   oParamater.Name = "@nFlowSheetID";
                                   oParamater.Value = floID;
                                   oDB.DBParametersCol.Add(oParamater);
                                   oParamater = null;


                                   oParamater = new DBParameter();
                                   oParamater.DataType = SqlDbType.VarChar;
                                   oParamater.Direction = ParameterDirection.Input;
                                   oParamater.Name = "@sFlowSheetName";
                                   oParamater.Value = objcls[len].FlowsheetName;
                                   oDB.DBParametersCol.Add(oParamater);
                                   oParamater = null;

                                   oParamater = new DBParameter();
                                   oParamater.DataType = SqlDbType.SmallInt;
                                   oParamater.Direction = ParameterDirection.Input;
                                   oParamater.Name = "@nCols";
                                   oParamater.Value = objcls[len].Cols;
                                   oDB.DBParametersCol.Add(oParamater);
                                   oParamater = null;

                                   oParamater = new DBParameter();
                                   oParamater.DataType = SqlDbType.SmallInt;
                                   oParamater.Direction = ParameterDirection.Input;
                                   oParamater.Name = "@nColNumber";
                                   oParamater.Value = objcls[len].ColNumber;
                                   oDB.DBParametersCol.Add(oParamater);
                                   oParamater = null;

                                   oParamater = new DBParameter();
                                   oParamater.DataType = SqlDbType.VarChar;
                                   oParamater.Direction = ParameterDirection.Input;
                                   oParamater.Name = "@sColumnName";
                                   oParamater.Value = objcls[len].ColumnName;
                                   oDB.DBParametersCol.Add(oParamater);
                                   oParamater = null;

                                   oParamater = new DBParameter();
                                   oParamater.DataType = SqlDbType.VarChar;
                                   oParamater.Direction = ParameterDirection.Input;
                                   oParamater.Name = "@sFormat";
                                   oParamater.Value = objcls[len].Format;
                                   oDB.DBParametersCol.Add(oParamater);
                                   oParamater = null;

                                   oParamater = new DBParameter();
                                   oParamater.DataType = SqlDbType.Decimal;
                                   oParamater.Direction = ParameterDirection.Input;
                                   oParamater.Name = "@dWidth";
                                   oParamater.Value = objcls[len].Width;
                                   oDB.DBParametersCol.Add(oParamater);
                                   oParamater = null;

                                   oParamater = new DBParameter();
                                   oParamater.DataType = SqlDbType.VarChar;
                                   oParamater.Direction = ParameterDirection.Input;
                                   oParamater.Name = "@sFontName";
                                   oParamater.Value = objcls[len].FontName;
                                   oDB.DBParametersCol.Add(oParamater);
                                   oParamater = null;

                                   oParamater = new DBParameter();
                                   oParamater.DataType = SqlDbType.Decimal;
                                   oParamater.Direction = ParameterDirection.Input;
                                   oParamater.Name = "@nFontSize";
                                   oParamater.Value = objcls[len].FontSize;
                                   oDB.DBParametersCol.Add(oParamater);
                                   oParamater = null;

                                   oParamater = new DBParameter();
                                   oParamater.DataType = SqlDbType.Decimal;
                                   oParamater.Direction = ParameterDirection.Input;
                                   oParamater.Name = "@nForeColor";
                                   oParamater.Value = objcls[len].ForeColor;
                                   oDB.DBParametersCol.Add(oParamater);
                                   oParamater = null;

                                   oParamater = new DBParameter();
                                   oParamater.DataType = SqlDbType.Bit;
                                   oParamater.Direction = ParameterDirection.Input;
                                   oParamater.Name = "@bIsBold";
                                   oParamater.Value = objcls[len].bIsBold;
                                   oDB.DBParametersCol.Add(oParamater);
                                   oParamater = null;


                                   oParamater = new DBParameter();
                                   oParamater.DataType = SqlDbType.Bit;
                                   oParamater.Direction = ParameterDirection.Input;
                                   oParamater.Name = "@bIsItalic";
                                   oParamater.Value = objcls[len].bIsItalic;
                                   oDB.DBParametersCol.Add(oParamater);
                                   oParamater = null;

                                   oParamater = new DBParameter();
                                   oParamater.DataType = SqlDbType.Bit;
                                   oParamater.Direction = ParameterDirection.Input;
                                   oParamater.Name = "@bIsUnderline";
                                   oParamater.Value = objcls[len].bIsUnderline;
                                   oDB.DBParametersCol.Add(oParamater);
                                   oParamater = null;

                                   oParamater = new DBParameter();
                                   oParamater.DataType = SqlDbType.VarChar;
                                   oParamater.Direction = ParameterDirection.Input;
                                   oParamater.Name = "@sAlignment";
                                   oParamater.Value = objcls[len].Alignment;
                                   oDB.DBParametersCol.Add(oParamater);
                                   oParamater = null;

                                   oParamater = new DBParameter();
                                   oParamater.DataType = SqlDbType.Decimal;
                                   oParamater.Direction = ParameterDirection.Input;
                                   oParamater.Name = "@nBackColor";
                                   oParamater.Value = objcls[len].BackColor;
                                   oDB.DBParametersCol.Add(oParamater);
                                   oParamater = null;

                                   oParamater = new DBParameter();
                                   oParamater.DataType = SqlDbType.Bit;
                                   oParamater.Direction = ParameterDirection.Input;
                                   oParamater.Name = "@Status";
                                   oParamater.Value = objcls[len].status;
                                   oDB.DBParametersCol.Add(oParamater);
                                   oParamater = null;

                                   floID = Convert.ToInt64(oDB.GetDataValue(strQuery));

                                   datainserted = true;

                               }


                               catch
                               {
                               }
                               finally
                               {
                                   oDB.Dispose();
                                   oDB = null;
                               }

                           } //IF

                           else
                           {
                               if (Result == DialogResult.No)
                               {
                                   noflowsheet = objcls[len].FlowsheetName;
                               }


                               else
                               {
                                   string strQuery = "glocomm_gsp_Insert_FlowSheet_MST";
                                   DataBaseLayer oDB = new DataBaseLayer();
                                   DBParameter oParamater = default(DBParameter);
                                 //  DataTable getdata = null;
                                   try
                                   {

                                       oParamater = new DBParameter();
                                       oParamater.DataType = SqlDbType.Decimal;
                                       oParamater.Direction = ParameterDirection.Input;
                                       oParamater.Name = "@nFlowSheetID";
                                       oParamater.Value = floID;
                                       oDB.DBParametersCol.Add(oParamater);
                                       oParamater = null;


                                       oParamater = new DBParameter();
                                       oParamater.DataType = SqlDbType.VarChar;
                                       oParamater.Direction = ParameterDirection.Input;
                                       oParamater.Name = "@sFlowSheetName";
                                       oParamater.Value = objcls[len].FlowsheetName;
                                       oDB.DBParametersCol.Add(oParamater);
                                       oParamater = null;

                                       oParamater = new DBParameter();
                                       oParamater.DataType = SqlDbType.SmallInt;
                                       oParamater.Direction = ParameterDirection.Input;
                                       oParamater.Name = "@nCols";
                                       oParamater.Value = objcls[len].Cols;
                                       oDB.DBParametersCol.Add(oParamater);
                                       oParamater = null;

                                       oParamater = new DBParameter();
                                       oParamater.DataType = SqlDbType.SmallInt;
                                       oParamater.Direction = ParameterDirection.Input;
                                       oParamater.Name = "@nColNumber";
                                       oParamater.Value = objcls[len].ColNumber;
                                       oDB.DBParametersCol.Add(oParamater);
                                       oParamater = null;

                                       oParamater = new DBParameter();
                                       oParamater.DataType = SqlDbType.VarChar;
                                       oParamater.Direction = ParameterDirection.Input;
                                       oParamater.Name = "@sColumnName";
                                       oParamater.Value = objcls[len].ColumnName;
                                       oDB.DBParametersCol.Add(oParamater);
                                       oParamater = null;

                                       oParamater = new DBParameter();
                                       oParamater.DataType = SqlDbType.VarChar;
                                       oParamater.Direction = ParameterDirection.Input;
                                       oParamater.Name = "@sFormat";
                                       oParamater.Value = objcls[len].Format;
                                       oDB.DBParametersCol.Add(oParamater);
                                       oParamater = null;

                                       oParamater = new DBParameter();
                                       oParamater.DataType = SqlDbType.Decimal;
                                       oParamater.Direction = ParameterDirection.Input;
                                       oParamater.Name = "@dWidth";
                                       oParamater.Value = objcls[len].Width;
                                       oDB.DBParametersCol.Add(oParamater);
                                       oParamater = null;

                                       oParamater = new DBParameter();
                                       oParamater.DataType = SqlDbType.VarChar;
                                       oParamater.Direction = ParameterDirection.Input;
                                       oParamater.Name = "@sFontName";
                                       oParamater.Value = objcls[len].FontName;
                                       oDB.DBParametersCol.Add(oParamater);
                                       oParamater = null;

                                       oParamater = new DBParameter();
                                       oParamater.DataType = SqlDbType.Decimal;
                                       oParamater.Direction = ParameterDirection.Input;
                                       oParamater.Name = "@nFontSize";
                                       oParamater.Value = objcls[len].FontSize;
                                       oDB.DBParametersCol.Add(oParamater);
                                       oParamater = null;

                                       oParamater = new DBParameter();
                                       oParamater.DataType = SqlDbType.Decimal;
                                       oParamater.Direction = ParameterDirection.Input;
                                       oParamater.Name = "@nForeColor";
                                       oParamater.Value = objcls[len].ForeColor;
                                       oDB.DBParametersCol.Add(oParamater);
                                       oParamater = null;

                                       oParamater = new DBParameter();
                                       oParamater.DataType = SqlDbType.Bit;
                                       oParamater.Direction = ParameterDirection.Input;
                                       oParamater.Name = "@bIsBold";
                                       oParamater.Value = objcls[len].bIsBold;
                                       oDB.DBParametersCol.Add(oParamater);
                                       oParamater = null;


                                       oParamater = new DBParameter();
                                       oParamater.DataType = SqlDbType.Bit;
                                       oParamater.Direction = ParameterDirection.Input;
                                       oParamater.Name = "@bIsItalic";
                                       oParamater.Value = objcls[len].bIsItalic;
                                       oDB.DBParametersCol.Add(oParamater);
                                       oParamater = null;

                                       oParamater = new DBParameter();
                                       oParamater.DataType = SqlDbType.Bit;
                                       oParamater.Direction = ParameterDirection.Input;
                                       oParamater.Name = "@bIsUnderline";
                                       oParamater.Value = objcls[len].bIsUnderline;
                                       oDB.DBParametersCol.Add(oParamater);
                                       oParamater = null;

                                       oParamater = new DBParameter();
                                       oParamater.DataType = SqlDbType.VarChar;
                                       oParamater.Direction = ParameterDirection.Input;
                                       oParamater.Name = "@sAlignment";
                                       oParamater.Value = objcls[len].Alignment;
                                       oDB.DBParametersCol.Add(oParamater);
                                       oParamater = null;

                                       oParamater = new DBParameter();
                                       oParamater.DataType = SqlDbType.Decimal;
                                       oParamater.Direction = ParameterDirection.Input;
                                       oParamater.Name = "@nBackColor";
                                       oParamater.Value = objcls[len].BackColor;
                                       oDB.DBParametersCol.Add(oParamater);
                                       oParamater = null;

                                       oParamater = new DBParameter();
                                       oParamater.DataType = SqlDbType.Bit;
                                       oParamater.Direction = ParameterDirection.Input;
                                       oParamater.Name = "@Status";
                                       oParamater.Value = objcls[len].status;
                                       oDB.DBParametersCol.Add(oParamater);
                                       oParamater = null;

                                       floID = Convert.ToInt64(oDB.GetDataValue(strQuery));

                                       datainserted = true;

                                       //return "";

                                   }

                                   catch
                                   {
                                       //   return "";
                                   }
                                   finally
                                   {
                                       oDB.Dispose();
                                       oDB = null;

                                   }
                               }
                           }  //Else
                       }// if no record found 

                       else
                       {


                           string strQuery = "glocomm_gsp_Insert_FlowSheet_MST";
                           DataBaseLayer oDB = new DataBaseLayer();
                           DBParameter oParamater = default(DBParameter);
                         //  DataTable getdata = null;
                           try
                           {

                               oParamater = new DBParameter();
                               oParamater.DataType = SqlDbType.Decimal;
                               oParamater.Direction = ParameterDirection.Input;
                               oParamater.Name = "@nFlowSheetID";
                               oParamater.Value = floID;
                               oDB.DBParametersCol.Add(oParamater);
                               oParamater = null;


                               oParamater = new DBParameter();
                               oParamater.DataType = SqlDbType.VarChar;
                               oParamater.Direction = ParameterDirection.Input;
                               oParamater.Name = "@sFlowSheetName";
                               oParamater.Value = objcls[len].FlowsheetName;
                               oDB.DBParametersCol.Add(oParamater);
                               oParamater = null;

                               oParamater = new DBParameter();
                               oParamater.DataType = SqlDbType.SmallInt;
                               oParamater.Direction = ParameterDirection.Input;
                               oParamater.Name = "@nCols";
                               oParamater.Value = objcls[len].Cols;
                               oDB.DBParametersCol.Add(oParamater);
                               oParamater = null;

                               oParamater = new DBParameter();
                               oParamater.DataType = SqlDbType.SmallInt;
                               oParamater.Direction = ParameterDirection.Input;
                               oParamater.Name = "@nColNumber";
                               oParamater.Value = objcls[len].ColNumber;
                               oDB.DBParametersCol.Add(oParamater);
                               oParamater = null;

                               oParamater = new DBParameter();
                               oParamater.DataType = SqlDbType.VarChar;
                               oParamater.Direction = ParameterDirection.Input;
                               oParamater.Name = "@sColumnName";
                               oParamater.Value = objcls[len].ColumnName;
                               oDB.DBParametersCol.Add(oParamater);
                               oParamater = null;

                               oParamater = new DBParameter();
                               oParamater.DataType = SqlDbType.VarChar;
                               oParamater.Direction = ParameterDirection.Input;
                               oParamater.Name = "@sFormat";
                               oParamater.Value = objcls[len].Format;
                               oDB.DBParametersCol.Add(oParamater);
                               oParamater = null;

                               oParamater = new DBParameter();
                               oParamater.DataType = SqlDbType.Decimal;
                               oParamater.Direction = ParameterDirection.Input;
                               oParamater.Name = "@dWidth";
                               oParamater.Value = objcls[len].Width;
                               oDB.DBParametersCol.Add(oParamater);
                               oParamater = null;

                               oParamater = new DBParameter();
                               oParamater.DataType = SqlDbType.VarChar;
                               oParamater.Direction = ParameterDirection.Input;
                               oParamater.Name = "@sFontName";
                               oParamater.Value = objcls[len].FontName;
                               oDB.DBParametersCol.Add(oParamater);
                               oParamater = null;

                               oParamater = new DBParameter();
                               oParamater.DataType = SqlDbType.Decimal;
                               oParamater.Direction = ParameterDirection.Input;
                               oParamater.Name = "@nFontSize";
                               oParamater.Value = objcls[len].FontSize;
                               oDB.DBParametersCol.Add(oParamater);
                               oParamater = null;

                               oParamater = new DBParameter();
                               oParamater.DataType = SqlDbType.Decimal;
                               oParamater.Direction = ParameterDirection.Input;
                               oParamater.Name = "@nForeColor";
                               oParamater.Value = objcls[len].ForeColor;
                               oDB.DBParametersCol.Add(oParamater);
                               oParamater = null;

                               oParamater = new DBParameter();
                               oParamater.DataType = SqlDbType.Bit;
                               oParamater.Direction = ParameterDirection.Input;
                               oParamater.Name = "@bIsBold";
                               oParamater.Value = objcls[len].bIsBold;
                               oDB.DBParametersCol.Add(oParamater);
                               oParamater = null;


                               oParamater = new DBParameter();
                               oParamater.DataType = SqlDbType.Bit;
                               oParamater.Direction = ParameterDirection.Input;
                               oParamater.Name = "@bIsItalic";
                               oParamater.Value = objcls[len].bIsItalic;
                               oDB.DBParametersCol.Add(oParamater);
                               oParamater = null;

                               oParamater = new DBParameter();
                               oParamater.DataType = SqlDbType.Bit;
                               oParamater.Direction = ParameterDirection.Input;
                               oParamater.Name = "@bIsUnderline";
                               oParamater.Value = objcls[len].bIsUnderline;
                               oDB.DBParametersCol.Add(oParamater);
                               oParamater = null;

                               oParamater = new DBParameter();
                               oParamater.DataType = SqlDbType.VarChar;
                               oParamater.Direction = ParameterDirection.Input;
                               oParamater.Name = "@sAlignment";
                               oParamater.Value = objcls[len].Alignment;
                               oDB.DBParametersCol.Add(oParamater);
                               oParamater = null;

                               oParamater = new DBParameter();
                               oParamater.DataType = SqlDbType.Decimal;
                               oParamater.Direction = ParameterDirection.Input;
                               oParamater.Name = "@nBackColor";
                               oParamater.Value = objcls[len].BackColor;
                               oDB.DBParametersCol.Add(oParamater);
                               oParamater = null;

                               oParamater = new DBParameter();
                               oParamater.DataType = SqlDbType.Bit;
                               oParamater.Direction = ParameterDirection.Input;
                               oParamater.Name = "@Status";
                               oParamater.Value = objcls[len].status;
                               oDB.DBParametersCol.Add(oParamater);
                               oParamater = null;

                               floID = Convert.ToInt64(oDB.GetDataValue(strQuery));

                               datainserted = true;

                               //return "";

                           }

                           catch
                           {
                               //   return "";
                           }
                           finally
                           {
                               oDB.Dispose();
                               oDB = null;

                           }
                   
                       
                       }


                   }//IF arr does not contain
                   else
                   {


                       string strQuery = "glocomm_gsp_Insert_FlowSheet_MST";
                       DataBaseLayer oDB = new DataBaseLayer();
                       DBParameter oParamater = default(DBParameter);
                    //   DataTable getdata = null;
                       try
                       {

                           oParamater = new DBParameter();
                           oParamater.DataType = SqlDbType.Decimal;
                           oParamater.Direction = ParameterDirection.Input;
                           oParamater.Name = "@nFlowSheetID";
                           oParamater.Value = floID;
                           oDB.DBParametersCol.Add(oParamater);
                           oParamater = null;


                           oParamater = new DBParameter();
                           oParamater.DataType = SqlDbType.VarChar;
                           oParamater.Direction = ParameterDirection.Input;
                           oParamater.Name = "@sFlowSheetName";
                           oParamater.Value = objcls[len].FlowsheetName;
                           oDB.DBParametersCol.Add(oParamater);
                           oParamater = null;

                           oParamater = new DBParameter();
                           oParamater.DataType = SqlDbType.SmallInt;
                           oParamater.Direction = ParameterDirection.Input;
                           oParamater.Name = "@nCols";
                           oParamater.Value = objcls[len].Cols;
                           oDB.DBParametersCol.Add(oParamater);
                           oParamater = null;

                           oParamater = new DBParameter();
                           oParamater.DataType = SqlDbType.SmallInt;
                           oParamater.Direction = ParameterDirection.Input;
                           oParamater.Name = "@nColNumber";
                           oParamater.Value = objcls[len].ColNumber;
                           oDB.DBParametersCol.Add(oParamater);
                           oParamater = null;

                           oParamater = new DBParameter();
                           oParamater.DataType = SqlDbType.VarChar;
                           oParamater.Direction = ParameterDirection.Input;
                           oParamater.Name = "@sColumnName";
                           oParamater.Value = objcls[len].ColumnName;
                           oDB.DBParametersCol.Add(oParamater);
                           oParamater = null;

                           oParamater = new DBParameter();
                           oParamater.DataType = SqlDbType.VarChar;
                           oParamater.Direction = ParameterDirection.Input;
                           oParamater.Name = "@sFormat";
                           oParamater.Value = objcls[len].Format;
                           oDB.DBParametersCol.Add(oParamater);
                           oParamater = null;

                           oParamater = new DBParameter();
                           oParamater.DataType = SqlDbType.Decimal;
                           oParamater.Direction = ParameterDirection.Input;
                           oParamater.Name = "@dWidth";
                           oParamater.Value = objcls[len].Width;
                           oDB.DBParametersCol.Add(oParamater);
                           oParamater = null;

                           oParamater = new DBParameter();
                           oParamater.DataType = SqlDbType.VarChar;
                           oParamater.Direction = ParameterDirection.Input;
                           oParamater.Name = "@sFontName";
                           oParamater.Value = objcls[len].FontName;
                           oDB.DBParametersCol.Add(oParamater);
                           oParamater = null;

                           oParamater = new DBParameter();
                           oParamater.DataType = SqlDbType.Decimal;
                           oParamater.Direction = ParameterDirection.Input;
                           oParamater.Name = "@nFontSize";
                           oParamater.Value = objcls[len].FontSize;
                           oDB.DBParametersCol.Add(oParamater);
                           oParamater = null;

                           oParamater = new DBParameter();
                           oParamater.DataType = SqlDbType.Decimal;
                           oParamater.Direction = ParameterDirection.Input;
                           oParamater.Name = "@nForeColor";
                           oParamater.Value = objcls[len].ForeColor;
                           oDB.DBParametersCol.Add(oParamater);
                           oParamater = null;

                           oParamater = new DBParameter();
                           oParamater.DataType = SqlDbType.Bit;
                           oParamater.Direction = ParameterDirection.Input;
                           oParamater.Name = "@bIsBold";
                           oParamater.Value = objcls[len].bIsBold;
                           oDB.DBParametersCol.Add(oParamater);
                           oParamater = null;


                           oParamater = new DBParameter();
                           oParamater.DataType = SqlDbType.Bit;
                           oParamater.Direction = ParameterDirection.Input;
                           oParamater.Name = "@bIsItalic";
                           oParamater.Value = objcls[len].bIsItalic;
                           oDB.DBParametersCol.Add(oParamater);
                           oParamater = null;

                           oParamater = new DBParameter();
                           oParamater.DataType = SqlDbType.Bit;
                           oParamater.Direction = ParameterDirection.Input;
                           oParamater.Name = "@bIsUnderline";
                           oParamater.Value = objcls[len].bIsUnderline;
                           oDB.DBParametersCol.Add(oParamater);
                           oParamater = null;

                           oParamater = new DBParameter();
                           oParamater.DataType = SqlDbType.VarChar;
                           oParamater.Direction = ParameterDirection.Input;
                           oParamater.Name = "@sAlignment";
                           oParamater.Value = objcls[len].Alignment;
                           oDB.DBParametersCol.Add(oParamater);
                           oParamater = null;

                           oParamater = new DBParameter();
                           oParamater.DataType = SqlDbType.Decimal;
                           oParamater.Direction = ParameterDirection.Input;
                           oParamater.Name = "@nBackColor";
                           oParamater.Value = objcls[len].BackColor;
                           oDB.DBParametersCol.Add(oParamater);
                           oParamater = null;

                           oParamater = new DBParameter();
                           oParamater.DataType = SqlDbType.Bit;
                           oParamater.Direction = ParameterDirection.Input;
                           oParamater.Name = "@Status";
                           oParamater.Value = objcls[len].status;
                           oDB.DBParametersCol.Add(oParamater);
                           oParamater = null;

                           floID = Convert.ToInt64(oDB.GetDataValue(strQuery));

                           datainserted = true;

                           //return "";

                       }

                       catch
                       {
                           //   return "";
                       }
                       finally
                       {
                           oDB.Dispose();
                           oDB = null;

                       }
                   
                   }
                  //  return "";
                
            }
              
            }
            if (datainserted == true)
            {
                MessageBox.Show("Data For Flowsheet Downloaded Successfully.", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        public bool CompareXMlData(DataTable local, DataTable server, string FilePath)
        {
            bool changedata = false;
          
            DialogResult Result;
            ArrayList remflow = new ArrayList(); 
            foreach (DataRow dr in local.Rows)
            {

              DataRow[] drr = null;
              drr = server.Select("FlowsheetName = '" + dr["FlowsheetName"].ToString().Replace("'", "''") + "'");
              if (drr.Length > 0)
              {

                  if (remflow.Contains(dr["FlowsheetName"].ToString().Trim()) == false)
                  {
                      Result = MessageBox.Show("For Flowsheet '" + dr["FlowsheetName"].ToString().Replace("'", "''") + "'    Already Exist Do you want to Overwrite ?", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                      if (Result == DialogResult.Yes)
                      {
                          remflow.Add(dr["FlowsheetName"].ToString().Trim());
                          for (int lendr = 0; lendr < drr.Length; lendr++)
                          {
                              server.Rows.Remove(drr[lendr]);
                          }

                          DataRow[] drrlocal = local.Select("FlowsheetName = '" + dr["FlowsheetName"].ToString().Replace("'", "''") + "'");
                          for (int lenlocal = 0; lenlocal < drrlocal.Length; lenlocal++)
                          {

                              server.ImportRow(drrlocal[lenlocal]);
                              changedata = true;
                          }



                      }
                      else
                      {
                          remflow.Add(dr["FlowsheetName"].ToString().Trim());
                      }

                  }
              }

              else
              {
                  DataRow[] drrlocal = local.Select("FlowsheetName = '" + dr["FlowsheetName"].ToString().Replace("'", "''") + "'");
                  if (drrlocal.Length > 0)
                  {
                      if (remflow.Contains(drrlocal[0]["FlowsheetName"].ToString().Trim().Replace("'", "''")) == false)
                      {

                          remflow.Add(drrlocal[0]["FlowsheetName"].ToString().Trim().Replace("'", "''"));
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









    }


    //  string FlowsheetName,int Cols, int Colnum,string ColumnName,string Format,string FontName,int Width,int  FontSize,double ForeColor,
    //bool bIsBold, bool   bIsItalic, bool bIsUnderline,string Alignment,double BackColor,bool status)
    public class FlowSheetFields
    {

        public int ColNumber
        {
            get;
            set;
        }

        public bool status
        {
            get;
            set;
        }

        public bool bIsUnderline
        {
            get;
            set;
        }
        public bool bIsItalic
        {
            get;
            set;
        }
        public bool bIsBold
        {
            get;
            set;
        }
        public double BackColor
        {
            get;
            set;
        }
        public double ForeColor
        {
            get;
            set;
        }
        public string Alignment
        {
            get;
            set;
        }

        public string FlowsheetName
        {
            get;
            set;
        }

        public string Format
        {
            get;
            set;
        }

        public string FontName
        {
            get;
            set;
        }
        public string ColumnName
        {
            get;
            set;
        }
        public int FontSize
        {
            get;
            set;
        }
        public Single Width
        {
            get;
            set;
        }
        public int Cols
        {
            get;
            set;
        }

    }

}
