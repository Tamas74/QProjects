using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using gloEMRGeneralLibrary.gloEMRDatabase;
using System.Data;
using System.Windows.Forms;

namespace gloCommunity.Classes
{
    class clsIMSetup
    {
        //public DataTable GetImmunisations(String sFilter)
        //{
        //    DataBaseLayer oDB = new DataBaseLayer();
        //    DataTable oResultTable = new DataTable();
        //    string strQuery = "";
        //    try
        //    {
        //        strQuery = "getCommunity_share_Upload_IMSetup";
        //      oResultTable = oDB.GetDataTable (strQuery,true);
        //        return oResultTable;
        //    }
        //    catch
        //    {
        //        return null;
        //    }

        //    finally
        //    {

        //        oDB.Dispose();
        //        oResultTable.Dispose();
        //        oDB = null;
        //    }
        //}
        //public DataTable GetCodewiseDescription()
        //{
        //    DataBaseLayer oDB = new DataBaseLayer();
        //    DataTable oResultTable = new DataTable();
        //    string strQuery = "";
        //    try
        //    {
        //        strQuery = "getIMCodeDescrip";
        //        oResultTable = oDB.GetDataTable(strQuery, true);
        //        return oResultTable;
        //    }
        //    catch
        //    {
        //        return null;
        //    }

        //    finally
        //    {

        //        oDB.Dispose();
        //        oResultTable.Dispose();
        //        oDB = null;
        //    }
        //}
        //public bool CompareXMlData(DataTable local, DataTable server, string FilePath)
        //{
        //    int cnt = 0;
        //    try
        //    {
        //        foreach (DataRow dr in local.Rows)
        //        {
        //            DataRow[] drr = server.Select("Name='" + dr["Name"].ToString() + "' ");
        //            if (drr.Length == 1)
        //            {
        //                DialogResult result = MessageBox.Show("The Entry for \" " + dr["Name"].ToString() + " \" already Exists ,Do you want to Overwrite ", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        //                if (result == DialogResult.Yes)
        //                {
        //                    server.Rows.Remove(drr[0]);
        //                    server.ImportRow(dr);
        //                    cnt = cnt + 1;
        //                }


        //            }
        //            if (drr.Length == 0)
        //            {
        //                server.ImportRow(dr);
        //                cnt = cnt + 1;
        //            }
        //        }

        //        server.WriteXml(FilePath);
        //        if ( cnt > 0)
        //        {
        //            return true;
        //        }




        //    }
        //    catch (Exception ex)
        //    {
        //       // MessageBox.Show(ex.Message, clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    }
        //    return false;
        //}

        //public bool InsertIMSetUp_CptCodes(string sCPTCode, string sDescription)
        //{
        //    DataBaseLayer oDB = new DataBaseLayer();
        //    DataTable oResultTable = new DataTable();
        //    DBParameter oParamater = default(DBParameter);
        //    string strQuery = "";
        //    try
        //    {
        //        strQuery = "glocommunity_share_cptInsert";
        //        oParamater = new DBParameter();
        //        oParamater.DataType = SqlDbType.VarChar;
        //        oParamater.Direction = ParameterDirection.Input;
        //        oParamater.Name = "@sCPTCode";
        //        oParamater.Value = sCPTCode;
        //        oDB.DBParametersCol.Add(oParamater);
        //        oParamater = null;




        //        oParamater = new DBParameter();
        //        oParamater.DataType = SqlDbType.VarChar;
        //        oParamater.Direction = ParameterDirection.Input;
        //        oParamater.Name = "@sDescription";
        //        oParamater.Value = sDescription;
        //        oDB.DBParametersCol.Add(oParamater);
        //        oParamater = null;

        //        oParamater = new DBParameter();
        //        oParamater.DataType = SqlDbType.Int;
        //        oParamater.Direction = ParameterDirection.Input;
        //        oParamater.Name = "@clinicID";
        //        oParamater.Value = clsGeneral.gClinicID ;
        //        oDB.DBParametersCol.Add(oParamater);
        //        oParamater = null;


        //        oDB.ExecuteNon_Query(strQuery);
        //        return true;
        //    }

        //    catch
        //    {
        //        return false;
        //    }

        //    finally
        //    {

        //        oDB.Dispose();
        //        oDB = null;
        //    }

        //}

        //public bool InsertUPDIMSetup(string im_item_Name, int im_item_Count, string im_cpt_code, string im_vaccine_code, decimal im_item_Id ,bool isUpdate)
        //{
        //    DataBaseLayer oDB = new DataBaseLayer();
        //    DataTable oResultTable = new DataTable();
        //    DBParameter oParamater = default(DBParameter);
        //    string strQuery = "";
        //    try
        //    {

        //        if (isUpdate)
        //        {

        //            delIMSetup(im_item_Name);
        //        }



        //        strQuery = "glocommunity_sharept_InsertIMt";

        //        oParamater = new DBParameter();
        //        oParamater.DataType = SqlDbType.VarChar;
        //        oParamater.Direction = ParameterDirection.Input;
        //        oParamater.Name = "@im_item_Name";
        //        oParamater.Value = im_item_Name;
        //        oDB.DBParametersCol.Add(oParamater);
        //        oParamater = null;

        //        oParamater = new DBParameter();
        //        oParamater.DataType = SqlDbType.Int;
        //        oParamater.Direction = ParameterDirection.Input;
        //        oParamater.Name = "@im_item_Count";
        //        oParamater.Value = im_item_Count;
        //        oDB.DBParametersCol.Add(oParamater);
        //        oParamater = null;


        //        oParamater = new DBParameter();
        //        oParamater.DataType = SqlDbType.VarChar;
        //        oParamater.Direction = ParameterDirection.Input;
        //        oParamater.Name = "@im_cpt_code";
        //        oParamater.Value = im_cpt_code;
        //        oDB.DBParametersCol.Add(oParamater);
        //        oParamater = null;


        //        oParamater = new DBParameter();
        //        oParamater.DataType = SqlDbType.VarChar;
        //        oParamater.Direction = ParameterDirection.Input;
        //        oParamater.Name = "@im_vaccine_code";
        //        oParamater.Value = im_vaccine_code;
        //        oDB.DBParametersCol.Add(oParamater);
        //        oParamater = null;



        //        oDB.ExecuteNon_Query(strQuery);
        //        return true;
        //    }

        //    catch
        //    {
        //        return false;
        //    }

        //    finally
        //    {

        //        oDB.Dispose();
        //        oDB = null;
        //    }

        //}

        //public bool delIMSetup(string im_item_Name)
        //{
        //    DataBaseLayer oDB = new DataBaseLayer();
        //    DataTable oResultTable = new DataTable();
        //    DBParameter oParamater = default(DBParameter);
        //    string strQuery = "";
        //    try
        //    {


        //            oParamater = new DBParameter();
        //            oParamater.DataType = SqlDbType.Text;
        //            oParamater.Direction = ParameterDirection.Input;
        //            oParamater.Name = "@im_item_Name";
        //            oParamater.Value = im_item_Name;
        //            oDB.DBParametersCol.Add(oParamater);
        //            strQuery = "glocommunity_sharept_UpdateIMt";

        //            oDB.ExecuteNon_Query(strQuery);
        //        return true;
        //    }

        //    catch
        //    {
        //        return false;
        //    }

        //    finally
        //    {

        //        oDB.Dispose();
        //        oDB = null;
        //    }

        //}

        public DataTable ImmunizationList(string Location, string Status)
        {
            DataBaseLayer oDB = new DataBaseLayer();
            DataTable oResultTable = new DataTable();
            DBParameter oParamater = default(DBParameter);
            string strQuery = "";
            try
            {

                strQuery = "Im_GetImmunizationDetails_New";

                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.VarChar;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@Status";
                oParamater.Value = Status;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.VarChar;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@Location";
                oParamater.Value = Location;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                oResultTable = oDB.GetDataTable(strQuery);
                return oResultTable;
            }

            catch
            {
                oResultTable = null;
                return oResultTable;
            }

            finally
            {

                oDB.Dispose();
                oDB = null;
            }

        }

        public DataTable GetImmunizationByID(long ImID)
        {
            DataBaseLayer oDB = new DataBaseLayer();
            DataTable oResultTable = new DataTable();
            DBParameter oParamater = default(DBParameter);
            string strQuery = "";
            try
            {
                strQuery = "GC_Im_GetImmunizationDetails_ById";
                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.BigInt;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@im_item_id";
                oParamater.Value = ImID;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;
                oResultTable = oDB.GetDataTable(strQuery);
                return oResultTable;
            }
            catch
            {
                oResultTable = null;
                return oResultTable;
            }
            finally
            {
                oDB.Dispose();
                oDB = null;
            }
        }

        public long SaveImmunization(int HowMany, string CPTCode, string VaccineCode, string _ConceptID, string _DescriptionID, string _SnoMedID, string _SnomedDescription, string _RxnormID, string _NDCCode, string _IM_strSnomedDescription,
        string _IM_SKU, string _ReceivedDate, string _IM_Active, string _IM_CVXDescription, string _MVXDescription, string _IM_TradeName, string _IM_LotNo, string _IM_ExpiryDate, decimal _Vialcount, decimal _IM_DosesPerVial,
        decimal _IM_AvailableDoses, string _IM_VIS, string _IM_publicationdate, string DiagnosisCode, string _IM_NDCCode, string _IM_FundingSource, string _IM_Commnets, long _EditID, long _nDocumentID, string _Location, long _CategoryId)
        {
            gloEMRGeneralLibrary.gloEMRDatabase.DataBaseLayer oDB = new gloEMRGeneralLibrary.gloEMRDatabase.DataBaseLayer();
            gloEMRGeneralLibrary.gloEMRDatabase.DBParameter oParamater = default(gloEMRGeneralLibrary.gloEMRDatabase.DBParameter);
            long MachineID = 0;
           // long IMTrn_ID = 0;
            long id = 0;
            try
            {
                oParamater = new gloEMRGeneralLibrary.gloEMRDatabase.DBParameter();
                oParamater.DataType = SqlDbType.BigInt;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@MachineID";
                oParamater.Value = MachineID;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                oParamater = new gloEMRGeneralLibrary.gloEMRDatabase.DBParameter();
                oParamater.DataType = SqlDbType.VarChar;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@im_item_Name";
                oParamater.Value = "";
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                oParamater = new gloEMRGeneralLibrary.gloEMRDatabase.DBParameter();
                oParamater.DataType = SqlDbType.Int;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@im_item_Count";
                oParamater.Value = HowMany;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                oParamater = new gloEMRGeneralLibrary.gloEMRDatabase.DBParameter();
                oParamater.DataType = SqlDbType.VarChar;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@im_cpt_code";
                oParamater.Value = CPTCode;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                oParamater = new gloEMRGeneralLibrary.gloEMRDatabase.DBParameter();
                oParamater.DataType = SqlDbType.VarChar;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@im_vaccine_code";
                oParamater.Value = VaccineCode;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                oParamater = new gloEMRGeneralLibrary.gloEMRDatabase.DBParameter();
                oParamater.DataType = SqlDbType.VarChar;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@im_sConceptID";
                oParamater.Value = _ConceptID;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                oParamater = new gloEMRGeneralLibrary.gloEMRDatabase.DBParameter();
                oParamater.DataType = SqlDbType.VarChar;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@im_sDescriptionID";
                oParamater.Value = _DescriptionID;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                oParamater = new gloEMRGeneralLibrary.gloEMRDatabase.DBParameter();
                oParamater.DataType = SqlDbType.VarChar;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@im_sSnoMedID";
                oParamater.Value = _SnoMedID;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                oParamater = new gloEMRGeneralLibrary.gloEMRDatabase.DBParameter();
                oParamater.DataType = SqlDbType.VarChar;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@im_sICD9";
                oParamater.Value = "";
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;


                oParamater = new gloEMRGeneralLibrary.gloEMRDatabase.DBParameter();
                oParamater.DataType = SqlDbType.VarChar;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@im_sSnomedDescription";
                oParamater.Value = _SnomedDescription;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                oParamater = new gloEMRGeneralLibrary.gloEMRDatabase.DBParameter();
                oParamater.DataType = SqlDbType.VarChar;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@im_sTranID1";
                oParamater.Value = _RxnormID;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                oParamater = new gloEMRGeneralLibrary.gloEMRDatabase.DBParameter();
                oParamater.DataType = SqlDbType.VarChar;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@im_sTranID2";
                oParamater.Value = _NDCCode;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                oParamater = new gloEMRGeneralLibrary.gloEMRDatabase.DBParameter();
                oParamater.DataType = SqlDbType.VarChar;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@im_sSnomedDefination";
                oParamater.Value = "";
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                oParamater = new gloEMRGeneralLibrary.gloEMRDatabase.DBParameter();
                oParamater.DataType = SqlDbType.NVarChar;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@im_sSKU";
                if (string.IsNullOrEmpty(_IM_SKU))
                {
                    oParamater.Value = System.DBNull.Value;
                }
                else
                {
                    oParamater.Value = _IM_SKU;
                }
                // oParamater.Value = _IM_SKU
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                oParamater = new gloEMRGeneralLibrary.gloEMRDatabase.DBParameter();
                oParamater.DataType = SqlDbType.DateTime;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@im_dtReceivedDate";
                if (string.IsNullOrEmpty(_ReceivedDate))
                {
                    oParamater.Value = System.DBNull.Value;
                }
                else
                {
                    oParamater.Value = _ReceivedDate;
                }
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                oParamater = new gloEMRGeneralLibrary.gloEMRDatabase.DBParameter();
                oParamater.DataType = SqlDbType.VarChar;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@im_sActive";
                oParamater.Value = _IM_Active;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                oParamater = new gloEMRGeneralLibrary.gloEMRDatabase.DBParameter();
                oParamater.DataType = SqlDbType.NVarChar;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@im_sVaccine";
                oParamater.Value = _IM_CVXDescription;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                oParamater = new gloEMRGeneralLibrary.gloEMRDatabase.DBParameter();
                oParamater.DataType = SqlDbType.NVarChar;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@im_sManufacturer";
                oParamater.Value = _MVXDescription;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                oParamater = new gloEMRGeneralLibrary.gloEMRDatabase.DBParameter();
                oParamater.DataType = SqlDbType.NVarChar;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@im_sTradeName";
                oParamater.Value = _IM_TradeName;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                oParamater = new gloEMRGeneralLibrary.gloEMRDatabase.DBParameter();
                oParamater.DataType = SqlDbType.VarChar;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@im_LotNumber";
                oParamater.Value = _IM_LotNo;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                oParamater = new gloEMRGeneralLibrary.gloEMRDatabase.DBParameter();
                oParamater.DataType = SqlDbType.DateTime;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@im_dtExpiration";
                if (string.IsNullOrEmpty(_IM_ExpiryDate))
                {
                    oParamater.Value = System.DBNull.Value;
                }
                else
                {
                    oParamater.Value = _IM_ExpiryDate;
                }

                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                oParamater = new gloEMRGeneralLibrary.gloEMRDatabase.DBParameter();
                oParamater.DataType = SqlDbType.Decimal;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@im_item_VialCount";
                oParamater.Value = _Vialcount;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;


                oParamater = new gloEMRGeneralLibrary.gloEMRDatabase.DBParameter();
                oParamater.DataType = SqlDbType.Decimal;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@im_DosesperVial";
                oParamater.Value = _IM_DosesPerVial;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                oParamater = new gloEMRGeneralLibrary.gloEMRDatabase.DBParameter();
                oParamater.DataType = SqlDbType.Decimal;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@im_AvailableDoses";
                oParamater.Value = _IM_AvailableDoses;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;


                oParamater = new gloEMRGeneralLibrary.gloEMRDatabase.DBParameter();
                oParamater.DataType = SqlDbType.VarChar;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@im_sVIS";
                oParamater.Value = _IM_VIS;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                oParamater = new gloEMRGeneralLibrary.gloEMRDatabase.DBParameter();
                oParamater.DataType = SqlDbType.DateTime;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@im_dtPublication";
                if (string.IsNullOrEmpty(_IM_publicationdate))
                {
                    oParamater.Value = System.DBNull.Value;
                }
                else
                {
                    oParamater.Value = _IM_publicationdate;
                }

                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                oParamater = new gloEMRGeneralLibrary.gloEMRDatabase.DBParameter();
                oParamater.DataType = SqlDbType.VarChar;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@im_Diagnosis_Code";
                oParamater.Value = DiagnosisCode;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                oParamater = new gloEMRGeneralLibrary.gloEMRDatabase.DBParameter();
                //Bug #80928: 00000521 : NDC CODES WITH LEADING ZEROS
                oParamater.DataType = SqlDbType.NVarChar;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@im_sNDCCode";
                if (string.IsNullOrEmpty(_IM_NDCCode))
                {
                    oParamater.Value = System.DBNull.Value;
                }
                else
                {
                    oParamater.Value = _IM_NDCCode;
                }

                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;


                oParamater = new gloEMRGeneralLibrary.gloEMRDatabase.DBParameter();
                oParamater.DataType = SqlDbType.VarChar;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@im_sFundingSource";
                oParamater.Value = _IM_FundingSource;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                oParamater = new gloEMRGeneralLibrary.gloEMRDatabase.DBParameter();
                oParamater.DataType = SqlDbType.VarChar;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@im_sComments";
                oParamater.Value = _IM_Commnets;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                oParamater = new gloEMRGeneralLibrary.gloEMRDatabase.DBParameter();
                oParamater.DataType = SqlDbType.VarChar;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@im_sEntryUser";
                oParamater.Value = ""; //gstrLoginName;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;


                oParamater = new gloEMRGeneralLibrary.gloEMRDatabase.DBParameter();
                oParamater.DataType = SqlDbType.BigInt;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@im_nDocumentID";
                oParamater.Value = _nDocumentID;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                oParamater = new gloEMRGeneralLibrary.gloEMRDatabase.DBParameter();
                oParamater.DataType = SqlDbType.VarChar;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@im_sLocation";
                oParamater.Value = _Location;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                oParamater = new gloEMRGeneralLibrary.gloEMRDatabase.DBParameter();
                oParamater.DataType = SqlDbType.VarChar;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@im_bTrackInventory";
                oParamater.Value = "0";
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                oParamater = new gloEMRGeneralLibrary.gloEMRDatabase.DBParameter();
                oParamater.DataType = SqlDbType.BigInt;
                oParamater.Direction = ParameterDirection.InputOutput;
                oParamater.Name = "@im_item_Id";
                oParamater.Value = _EditID;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                oParamater = new gloEMRGeneralLibrary.gloEMRDatabase.DBParameter();
                oParamater.DataType = SqlDbType.BigInt;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@im_nCategoryID";
                oParamater.Value = _CategoryId;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                id = Convert.ToInt64(oDB.GetDataValue("IM_InsUpdItemMast"));
            }
            catch (Exception ex)
            {
                id = -1;
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }
            finally
            {
                if ((oDB != null))
                {
                    oDB.Dispose();
                    oDB = null;
                }
                if ((oParamater != null))
                {
                    oParamater = null;
                }
            }
            return id;
        }

        public long InsertCategory(string Code, string categoryDescription, string categoryType)
        {
            DataBaseLayer oDB = new DataBaseLayer();
            DataTable oResultTable = new DataTable();
            DBParameter oParamater = default(DBParameter);
            string strQuery = "";
            long _Id = 0;
            try
            {

                strQuery = "GC_InsertCategory";

                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.VarChar;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@sCode";
                oParamater.Value = Code;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.VarChar;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@sDescription";
                oParamater.Value = categoryDescription;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.VarChar;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@sCategoryType";
                oParamater.Value = categoryType;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.BigInt;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@nClinicID";
                oParamater.Value = clsGeneral.clinicID;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.Bit;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@bIsBlocked";
                oParamater.Value = 0;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                //oDB.ExecuteNon_Query(strQuery);
                //return true;
                _Id = Convert.ToInt64(oDB.GetDataValue(strQuery));
            }

            catch
            {
                return -1;
            }

            finally
            {

                oDB.Dispose();
                oDB = null;
            }
            return _Id;
        }

        public long CheckVaccineExist(string _Vaccine, string _IM_LotNo)
        {
            gloEMRGeneralLibrary.gloEMRDatabase.DataBaseLayer oDB = new gloEMRGeneralLibrary.gloEMRDatabase.DataBaseLayer();
            gloEMRGeneralLibrary.gloEMRDatabase.DBParameter oParamater = default(gloEMRGeneralLibrary.gloEMRDatabase.DBParameter);

            long id = 0;
            try
            {
                oParamater = new gloEMRGeneralLibrary.gloEMRDatabase.DBParameter();
                oParamater.DataType = SqlDbType.VarChar;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@im_sVaccine";
                oParamater.Value = _Vaccine;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                oParamater = new gloEMRGeneralLibrary.gloEMRDatabase.DBParameter();
                oParamater.DataType = SqlDbType.VarChar;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@im_sLotNumber";
                oParamater.Value = _IM_LotNo;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                id = Convert.ToInt32(oDB.GetDataValue("GC_CheckVaccineExist"));
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }
            finally
            {
                if ((oDB != null))
                {
                    oDB.Dispose();
                    oDB = null;
                }
                if ((oParamater != null))
                {
                    oParamater = null;
                }
            }
            return id;
        }

        public DataTable GetLocation()
        {
            DataBaseLayer oDB = new DataBaseLayer();
            DataTable oResultTable = new DataTable();

            string strQuery = "";
            try
            {
                strQuery = "SELECT nLocationID, sLocation,bIsDefault FROM AB_Location WHERE bIsBlocked = 0 ORDER By SLocation";
                oResultTable = oDB.GetDataTable_Query(strQuery);
                return oResultTable;
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

        public long IsExists(string sLocation, long nClinicId)
        {
            gloEMRGeneralLibrary.gloEMRDatabase.DataBaseLayer oDB = new gloEMRGeneralLibrary.gloEMRDatabase.DataBaseLayer();
            gloEMRGeneralLibrary.gloEMRDatabase.DBParameter oParamater = default(gloEMRGeneralLibrary.gloEMRDatabase.DBParameter);

            long id = 0;
            try
            {
                oParamater = new gloEMRGeneralLibrary.gloEMRDatabase.DBParameter();
                oParamater.DataType = SqlDbType.VarChar;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@sLocation";
                oParamater.Value = sLocation;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                oParamater = new gloEMRGeneralLibrary.gloEMRDatabase.DBParameter();
                oParamater.DataType = SqlDbType.BigInt;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@nClinicID";
                oParamater.Value = nClinicId;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                id = Convert.ToInt32(oDB.GetDataValue("GC_CheckAndInsertLocation"));
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }
            finally
            {
                if ((oDB != null))
                {
                    oDB.Dispose();
                    oDB = null;
                }
                if ((oParamater != null))
                {
                    oParamater = null;
                }
            }
            return id;
        }
    }
}
