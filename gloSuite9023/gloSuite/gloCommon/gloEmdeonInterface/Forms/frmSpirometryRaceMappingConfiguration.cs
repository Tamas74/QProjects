using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;    

namespace gloEmdeonInterface.Forms
{
    public partial class frmSpirometryRaceMappingConfiguration : Form
    {
      #region " Declaration "

        private const int COLUM_EMRRACE = 0;
        private const int COLUM_SPIRORACE= 1;
        private const int COLUM_COUNT = 2;
        private string gloConnectionString = string.Empty;
        private string SpiroConnectionString = string.Empty;
       #endregion


      #region " Constructor and Destructor "

        public frmSpirometryRaceMappingConfiguration(string GloEmrConnectionString,string SpirometryConnectionstring)
        {
            InitializeComponent();
            // get connection string from app settings
            gloConnectionString = GloEmrConnectionString;
            SpiroConnectionString = SpirometryConnectionstring;
          }

      #endregion


        #region " Event "

        private void frmSpiroTest_RaceConfiguration_Load(object sender, EventArgs e)
        {
          LoadData();
        }

        private void ts_LabMain_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Tag.ToString())
            {

                case "AddNew":
                    {
                        frmViewSpirometryRace obj = new frmViewSpirometryRace(gloConnectionString, SpiroConnectionString);  
                        obj.ShowDialog();
                        BindSpiroRace();
                        break;
                    }
                case "Save&Close":
                    {
                      if (IsValidate())
                      {
                          AddUpdateMapingData();
                           this.Close();
                        }
                        break;
                    }
                case "Delete":
                    {
                        if (c1RaceConfiguration.RowSel > 0)
                        {
                            if (MessageBox.Show("Are you sure you want to delete the selected record?", "gloEMR", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {
                                string EMRRace = Convert.ToString(c1RaceConfiguration.GetData(c1RaceConfiguration.RowSel, COLUM_EMRRACE));
                                string SpiroRace = Convert.ToString(c1RaceConfiguration.GetData(c1RaceConfiguration.RowSel, COLUM_SPIRORACE));
                                if (EMRRace != "" && SpiroRace != "")
                                {
                                    long EMRRaceID = GetEMRRaceID(EMRRace); ;
                                    long SpiroRaceID = GetSPiroRaceID(SpiroRace);
                                    if (EMRRaceID != 0 && SpiroRaceID != 0)
                                    {
                                        DeleteMapping(EMRRaceID, SpiroRaceID);
                                        LoadData(); 
                                    }
                                    else
                                    {
                                        c1RaceConfiguration.SetData(c1RaceConfiguration.RowSel, COLUM_EMRRACE, string.Empty);
                                        c1RaceConfiguration.SetData(c1RaceConfiguration.RowSel, COLUM_SPIRORACE, string.Empty);
                                    }
                                }
                                else
                                {
                                    c1RaceConfiguration.SetData(c1RaceConfiguration.RowSel, COLUM_EMRRACE, string.Empty);
                                    c1RaceConfiguration.SetData(c1RaceConfiguration.RowSel, COLUM_SPIRORACE, string.Empty);
                                }
                            }
                        }
                    break; 
                    }
                case "Close":
                    {
                        this.Close();  
                        break;
                    }
            }
        }

        private void c1RaceConfiguration_EnterCell(object sender, EventArgs e)
        {

            BindEMRRace();
            BindSpiroRace(); 

        }

        private void c1RaceConfiguration_AfterEdit(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {
            // check coulum adited if it is EMR race colum then check for duplicate recored else if it is spiro colum check for EMR Race Exist or not
            if (e.Col == COLUM_EMRRACE)
            {
                string sCategoryName = string.Empty;
                sCategoryName = Convert.ToString(c1RaceConfiguration.GetData(e.Row, COLUM_EMRRACE)).Trim();
                if (sCategoryName != string.Empty && sCategoryName != null && sCategoryName != "")
                {
                    for (int rowcont = 1; rowcont < c1RaceConfiguration.Rows.Count; rowcont++)
                    {
                        // if duplicate recored found and coulum is not same then
                        if (sCategoryName == Convert.ToString(c1RaceConfiguration.GetData(rowcont, COLUM_EMRRACE)).Trim() && rowcont != e.Row)
                        {
                            MessageBox.Show("This Race Is ALredy Configured", "gloEMR", MessageBoxButtons.OK);
                            c1RaceConfiguration.SetData(e.Row, COLUM_EMRRACE, string.Empty);
                            c1RaceConfiguration.SetData(e.Row, COLUM_SPIRORACE, string.Empty);
                            break;
                        }

                    }//for
                } // if sCategoryName is empty

            }//  if e.Col
            else if (e.Col == COLUM_SPIRORACE)
            {
                // if spiro race is selected and EMR Race is not selected then
                if (Convert.ToString(c1RaceConfiguration.GetData(e.Row, COLUM_EMRRACE)).Trim() == String.Empty || Convert.ToString(c1RaceConfiguration.GetData(e.Row, COLUM_EMRRACE)).Trim() == null || Convert.ToString(c1RaceConfiguration.GetData(e.Row, COLUM_EMRRACE)).Trim() == "")
                {
                    MessageBox.Show("First Select EMR Race to configure", "gloEMR", MessageBoxButtons.OK);
                    c1RaceConfiguration.SetData(e.Row, COLUM_SPIRORACE, null);
                }
            }

        }

       #endregion

  #region " Function and Method "


        private void DesignGrid()
        {
            c1RaceConfiguration.DataSource = null;

            c1RaceConfiguration.Rows.Count = 1;
            c1RaceConfiguration.Rows.Fixed = 1;
            c1RaceConfiguration.Cols.Count = COLUM_COUNT;
            c1RaceConfiguration.Cols.Fixed = 0;

            c1RaceConfiguration.Cols[COLUM_EMRRACE].AllowEditing = true;
            c1RaceConfiguration.Cols[COLUM_SPIRORACE].AllowEditing = true;

            c1RaceConfiguration.Cols[COLUM_EMRRACE].Visible = true;
            c1RaceConfiguration.Cols[COLUM_SPIRORACE].Visible = true;

            c1RaceConfiguration.SetData(0, COLUM_EMRRACE, "EMR Race");
            c1RaceConfiguration.SetData(0, COLUM_SPIRORACE, "Spiro Race");

            c1RaceConfiguration.Rows[0].Height = 25;
            c1RaceConfiguration.Rows[0].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter;
            //c1RaceConfiguration.Cols[COLUM_SPIRORACE].ComboList = "Unspecified|Caucasian|Black|Asian|American Indian|Hispanic|African-American|African-Eruption";
            //c1RaceConfiguration.SetData(1, COLUM_EMRRACE, "");
            //c1RaceConfiguration.SetData(1, COLUM_SPIRORACE, "...");



            //C1.Win.C1FlexGrid.CellRange _range = c1RaceConfiguration.GetCellRange(1, COLUM_EMRRACE, 1, COLUM_EMRRACE);
            //_range.StyleNew.ComboList = "test1|test2";

            //C1.Win.C1FlexGrid.CellRange _range1 = c1RaceConfiguration.GetCellRange(1, COLUM_SPIRORACE, 1, COLUM_SPIRORACE);
            //_range1.StyleNew.ComboList = "test1|test2";


            c1RaceConfiguration.Cols[COLUM_EMRRACE].Width = 250;
            c1RaceConfiguration.Cols[COLUM_SPIRORACE].Width = 250;

            c1RaceConfiguration.ExtendLastCol = true;

            c1RaceConfiguration.Rows.Count = 1;

        }

        private void LoadData()
        {
            DesignGrid();
            DataTable _dtMappedRace = RetiriveMappedRace();
            for (int irow = 0; irow < _dtMappedRace.Rows.Count; irow++)
            {
                long EMRRaceID = 0;
                long SpiroRaceID = 0;
                long.TryParse(_dtMappedRace.Rows[irow][1].ToString(), out EMRRaceID);
                long.TryParse(_dtMappedRace.Rows[irow][0].ToString(), out SpiroRaceID);
                if (EMRRaceID != 0 && SpiroRaceID != 0)
                {
                  string EMRRaceName = GetEMRRace(EMRRaceID) ;
                  string SpiroraceName = GetSpiroRace(SpiroRaceID);
                  if (EMRRaceName != String.Empty && SpiroraceName != string.Empty)
                  {
                      c1RaceConfiguration.SetData(irow + 1, COLUM_EMRRACE, EMRRaceName);
                      c1RaceConfiguration.SetData(irow + 1, COLUM_SPIRORACE, SpiroraceName);
                  }
                 }
           }
        }


        private DataTable RetiriveMappedRace()
        {
            DataTable _RetiriveMappedRace = null;
            gloDatabaseLayer.DBLayer _oDbLayer = null;
            try
            {
                _oDbLayer = new gloDatabaseLayer.DBLayer(SpiroConnectionString);
                _oDbLayer.Connect(false);
                _oDbLayer.Retrive("Get_MappedRace", out _RetiriveMappedRace);
                _oDbLayer.Disconnect();

            }
            catch (Exception ex)
            { 
               MessageBox.Show(ex.ToString(), "gloEMR", MessageBoxButtons.OK);
               gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Spirometry, gloAuditTrail.ActivityType.Add, " Error in frmSpirometryRaceMappingConfiguration.RetiriveMappedRace() " + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);

            }
            finally
            {
                if (_oDbLayer != null)
                {
                    _oDbLayer.Dispose();
                    _oDbLayer = null;
                }

            }
            return _RetiriveMappedRace;
        }
        
        private DataTable RetrieveEMRRace()
        {
            DataTable _LoadEMRRace = null;
            string _SqlQry = string.Empty; 
            gloDatabaseLayer.DBLayer _oDbLayer = null;
            try
            {
                _SqlQry = "SELECT nCategoryID, sDescription FROM dbo.Category_MST WHERE sCategoryType = 'Race'";
                _oDbLayer = new gloDatabaseLayer.DBLayer(gloConnectionString);
                _oDbLayer.Connect(false);
                _oDbLayer.Retrive_Query(_SqlQry, out _LoadEMRRace);  
                _oDbLayer.Disconnect();

            }
            catch (Exception ex)
            { 
                MessageBox.Show(ex.ToString(), "gloEMR", MessageBoxButtons.OK);
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Spirometry, gloAuditTrail.ActivityType.Add, " Error in frmSpirometryRaceMappingConfiguration.RetrieveEMRRace() " + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);

            }
            finally
            {
                _SqlQry = string.Empty; 
                if (_oDbLayer != null)
                {
                    _oDbLayer.Dispose();
                    _oDbLayer = null;
                }

            }
            return _LoadEMRRace;
        }

        private DataTable RetriveSpiroRace()
        {
            DataTable _RetriveSpiroRace = null;
            gloDatabaseLayer.DBLayer oDBLayer = null;
            gloDatabaseLayer.DBParameters oDBParameters = null;
            try
            {
                oDBLayer = new gloDatabaseLayer.DBLayer(SpiroConnectionString);
                oDBParameters = new gloDatabaseLayer.DBParameters();
                oDBParameters.Add("@nCategoryId", 0, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@sCategoryType", "SPIRORACE", ParameterDirection.Input, SqlDbType.VarChar, 50);
                oDBLayer.Connect(false);
                oDBLayer.Retrive("Get_CategoryMST", oDBParameters, out _RetriveSpiroRace);
                oDBLayer.Disconnect();
            }
            catch (Exception ex)
            {
                //gloEmdeonInterface.Classes.clsGeneral obj = new gloEmdeonInterface.Classes.clsGeneral();
                //obj.UpdateLog("Error to retrive Recored  " + ex.ToString());
                //obj.Dispose();
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Spirometry, gloAuditTrail.ActivityType.Add, " Error in frmSpirometryRaceMappingConfiguration.RetriveSpiroRace() " + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }
            finally
            {
                if (oDBLayer != null)
                {
                    oDBLayer.Dispose();
                    oDBLayer = null;
                }

                if (oDBParameters != null)
                {
                    oDBParameters.Dispose();
                    oDBParameters = null;
                }
            }
            return _RetriveSpiroRace;
        }

        private String GetEMRRace(long EMRRaceID)
        {
            string _GetEMRRace = string.Empty;
            gloDatabaseLayer.DBLayer oDBLayer = null;
            string SqlQry = string.Empty;
            try
            {
                SqlQry = "SELECT Top 1 ISNULL(sDescription, 0) AS Expr1 FROM  dbo.Category_MST WHERE nCategoryID = " + EMRRaceID + "";
                oDBLayer = new gloDatabaseLayer.DBLayer(gloConnectionString);
                oDBLayer.Connect(false);
                _GetEMRRace = Convert.ToString(oDBLayer.ExecuteScalar_Query(SqlQry));
                oDBLayer.Disconnect();

            }
            catch (Exception ex)
            {
                _GetEMRRace = string.Empty;
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Spirometry, gloAuditTrail.ActivityType.Add, " Error in frmSpirometryRaceMappingConfiguration.GetEMRRace() " + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);

            }
            finally
            {
                if (oDBLayer != null)
                {
                    oDBLayer.Dispose();
                    oDBLayer = null;
                }
                SqlQry = string.Empty;
            }
            return _GetEMRRace;
        }

        private String GetSpiroRace(long SpiroRaceID)
        {
            string _GetSpiroRace = string.Empty;
            gloDatabaseLayer.DBLayer oDBLayer = null;
            string SqlQry = string.Empty;
            try
            {
                SqlQry = "SELECT top 1 sCategoryName FROM dbo.e_CategoryMST where nCategoryId= " + SpiroRaceID + "";
                oDBLayer = new gloDatabaseLayer.DBLayer(SpiroConnectionString);
                oDBLayer.Connect(false);
                _GetSpiroRace = Convert.ToString(oDBLayer.ExecuteScalar_Query(SqlQry));
                oDBLayer.Disconnect();

            }
            catch (Exception ex)
            {
                _GetSpiroRace = string.Empty;
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Spirometry, gloAuditTrail.ActivityType.Add, " Error in frmSpirometryRaceMappingConfiguration.GetSpiroRace() " + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);

            }
            finally
            {
                if (oDBLayer != null)
                {
                    oDBLayer.Dispose();
                    oDBLayer = null;
                }
                SqlQry = string.Empty;
            }
            return _GetSpiroRace;
        }

        private long GetEMRRaceID(string EMRRace)
        {
            long _GetEMRRaceID =0;
            gloDatabaseLayer.DBLayer oDBLayer = null;
            string SqlQry = string.Empty;
            try
            {
                SqlQry = "SELECT  Top 1 ISNULL(nCategoryID,0) FROM  dbo.Category_MST WHERE sCategoryType = 'Race' AND sDescription =  '" + EMRRace.Trim() + "'";
                oDBLayer = new gloDatabaseLayer.DBLayer(gloConnectionString);
                oDBLayer.Connect(false);
                _GetEMRRaceID = Convert.ToInt64(oDBLayer.ExecuteScalar_Query(SqlQry));
                oDBLayer.Disconnect();

            }
            catch (Exception ex)
            {
                _GetEMRRaceID =0;
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Spirometry, gloAuditTrail.ActivityType.Add, " Error in frmSpirometryRaceMappingConfiguration.GetEMRRaceID() " + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);

            }
            finally
            {
                if (oDBLayer != null)
                {
                    oDBLayer.Dispose();
                    oDBLayer = null;
                }
                SqlQry = string.Empty;
            }
            return _GetEMRRaceID;
        }

        private long GetSPiroRaceID(string SpiroRace)
        {
            long _GetspiroRaceID = 0;
            gloDatabaseLayer.DBLayer oDBLayer = null;
            string SqlQry = string.Empty;
            try
            {
                SqlQry = "SELECT top 1 ISNULL(nCategoryId,0)  FROM  e_CategoryMST WHERE  sCategoryName='" + SpiroRace.Trim() + "'";
                oDBLayer = new gloDatabaseLayer.DBLayer(SpiroConnectionString);
                oDBLayer.Connect(false);
                _GetspiroRaceID = Convert.ToInt64(oDBLayer.ExecuteScalar_Query(SqlQry));
                oDBLayer.Disconnect();

            }
            catch (Exception ex)
            {
                _GetspiroRaceID = 0;
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Spirometry, gloAuditTrail.ActivityType.Add, " Error in frmSpirometryRaceMappingConfiguration.GetSPiroRaceID() " + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);

            }
            finally
            {
                if (oDBLayer != null)
                {
                    oDBLayer.Dispose();
                    oDBLayer = null;
                }
                SqlQry = string.Empty;
            }
            return _GetspiroRaceID;
        }

        private bool IsValidate()
        {
            bool _IsValidate = true;
            string sCategoryName = string.Empty;
            string sCategoryDesc = string.Empty;
            for (int irow = 1; irow < c1RaceConfiguration.Rows.Count; irow++)
            {
                sCategoryName = Convert.ToString(c1RaceConfiguration.GetData(irow, COLUM_EMRRACE)).Trim();
                sCategoryDesc = Convert.ToString(c1RaceConfiguration.GetData(irow, COLUM_SPIRORACE)).Trim();
                if (sCategoryName != "" && sCategoryDesc == "")
                {
                    _IsValidate = false;
                    MessageBox.Show("Race is not configured", "gloEMR", MessageBoxButtons.OK);
                    break;
                }
            }
            return _IsValidate;
        }

        private void AddUpdateMapingData()
        {
          c1RaceConfiguration.FinishEditing();
           try
            {
                DeleteMapping(0,0);
                for (int rowcont = 1; rowcont < c1RaceConfiguration.Rows.Count; rowcont++)
                {
                  string EMRRace = Convert.ToString(c1RaceConfiguration.GetData(rowcont,COLUM_EMRRACE)).Trim() ;
                  string SpiroRace =Convert.ToString(c1RaceConfiguration.GetData(rowcont,COLUM_SPIRORACE)).Trim();
                  if (EMRRace != "" && SpiroRace != "" )
                   { 
                       long EMRRaceID = GetEMRRaceID(EMRRace);;
                       long SpiroRaceID =GetSPiroRaceID(SpiroRace) ;
                       if (EMRRaceID != 0 && SpiroRaceID != 0)
                       {
                           SaveUpdate(EMRRaceID, SpiroRaceID);
                       }
                   }
               } // for
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "gloEMR", MessageBoxButtons.OK,MessageBoxIcon.Error);
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Spirometry, gloAuditTrail.ActivityType.Add, " Error in frmSpirometryRaceMappingConfiguration.AddUpdateMapingData() " + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);

            }

        }
        
        private void SaveUpdate(long EMRRaceID, long SpiroRaceID)
        {
         gloDatabaseLayer.DBLayer oDBLayer = null;
         gloDatabaseLayer.DBParameters oDBParameters = null;
         try
         {
             oDBLayer = new gloDatabaseLayer.DBLayer(SpiroConnectionString);
             oDBParameters = new gloDatabaseLayer.DBParameters();
             oDBParameters.Add("@eCategoryId", SpiroRaceID, ParameterDirection.Input, SqlDbType.BigInt);
             oDBParameters.Add("@ngloCategoryId",EMRRaceID  , ParameterDirection.Input, SqlDbType.BigInt);
             oDBLayer.Connect(false);
             oDBLayer.Execute("INUP_CategoryMapping", oDBParameters);
             oDBLayer.Disconnect();


         }
         catch (Exception ex)
         {
             MessageBox.Show(ex.ToString(), "gloEMR", MessageBoxButtons.OK,MessageBoxIcon.Error);
             gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Spirometry, gloAuditTrail.ActivityType.Add, " Error in frmSpirometryRaceMappingConfiguration.SaveUpdate() " + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);

         }

         finally
         {
             if (oDBLayer != null)
             {
                 oDBLayer.Dispose();
                 oDBLayer = null;
             }
             oDBParameters = null;
         }
        }

        private void BindEMRRace()
        {
            string _EMRRace = string.Empty;
            DataTable _dtEMRRace = RetrieveEMRRace();
            for (int irow = 0; irow < _dtEMRRace.Rows.Count; irow++)
            {
                _EMRRace = _EMRRace + "|" + _dtEMRRace.Rows[irow][1].ToString();
                if (c1RaceConfiguration.Rows.Count < _dtEMRRace.Rows.Count + 1)
                {
                    c1RaceConfiguration.Rows.Add();
                }
            }
            c1RaceConfiguration.Cols[COLUM_EMRRACE].ComboList = _EMRRace;
            _dtEMRRace.Dispose();
            _dtEMRRace = null;
           
        }

        private void BindSpiroRace()
        {
            string _spiroRace = string.Empty;
            DataTable _dtspiroRace = RetriveSpiroRace();
            for (int irow = 0; irow < _dtspiroRace.Rows.Count; irow++)
            {
                _spiroRace = _spiroRace + "|" + _dtspiroRace.Rows[irow][2].ToString();
                if (c1RaceConfiguration.Rows.Count < _dtspiroRace.Rows.Count + 1)
                {
                    c1RaceConfiguration.Rows.Add();
                }
            }
            c1RaceConfiguration.Cols[COLUM_SPIRORACE].ComboList = _spiroRace;
            _dtspiroRace.Dispose();
            _dtspiroRace = null;
        }


        private void DeleteMapping(long EMRraceId, long SpiroRaceID)
        {

            gloDatabaseLayer.DBLayer oDBLayer = null;
            gloDatabaseLayer.DBParameters oDBParameters = null;
            try
            {
                oDBLayer = new gloDatabaseLayer.DBLayer(SpiroConnectionString);
                oDBParameters = new gloDatabaseLayer.DBParameters();
                oDBLayer.Connect(false);
                oDBParameters.Add("@eCategoryId", SpiroRaceID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@ngloCategoryId", EMRraceId , ParameterDirection.Input, SqlDbType.BigInt);
                oDBLayer.Execute("Remove_CategoryMapping", oDBParameters);
                oDBLayer.Disconnect();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "gloEMR", MessageBoxButtons.OK);
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Spirometry, gloAuditTrail.ActivityType.Add, " Error in frmSpirometryRaceMappingConfiguration.DeleteMapping() " + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);

            }
            finally
            {
                if (oDBLayer != null)
                {
                    oDBLayer.Dispose();
                    oDBLayer = null;
                }
                oDBParameters = null;
            }
         

        }

        //private void DeleteMapingData(long _scateoryID) 
        //{
        //    gloDatabaseLayer.DBLayer oDBLayer = null;
        //    gloDatabaseLayer.DBParameters oDBParameters = null;
        //    try
        //    {
        //        oDBLayer = new gloDatabaseLayer.DBLayer(SpiroConnectionString);
        //        oDBParameters = new gloDatabaseLayer.DBParameters();
        //        oDBLayer.Connect(false);
        //        oDBParameters.Add("@nCategoryId", _scateoryID, ParameterDirection.Input, SqlDbType.BigInt);
        //        oDBLayer.Execute("Del_e_CategoryMST", oDBParameters);
        //        oDBLayer.Disconnect(); 

        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString(), "gloEMR", MessageBoxButtons.OK); 
        //    }
        //    finally
        //    {
        //      if (oDBLayer != null)
        //        {
        //            oDBLayer.Dispose();
        //            oDBLayer = null;
        //        }
        //        oDBParameters = null;
        //    }
         


        //}

        //private long GetCategoryMasterID(string _sCategoryName, string _sCategoryDesc)
        //{
        //    object objResult = null;
        //    gloDatabaseLayer.DBLayer oDBLayer = null;
        //    gloDatabaseLayer.DBParameters oDBParameters = null;
        //    try
        //    {
        //        oDBLayer = new gloDatabaseLayer.DBLayer(SpiroConnectionString);
        //        oDBParameters = new gloDatabaseLayer.DBParameters();
        //        oDBLayer.Connect(false);
        //        oDBParameters.Add("@nCategoryId", "0", ParameterDirection.Output, SqlDbType.VarChar, 255);
        //        oDBParameters.Add("@sCategoryName", _sCategoryName, ParameterDirection.Input, SqlDbType.VarChar, 255);
        //        oDBParameters.Add("@sCategoryDesc", _sCategoryDesc, ParameterDirection.Input, SqlDbType.VarChar, 512);
        //        oDBLayer.Execute("GetID_e_CategoryMST", oDBParameters, out objResult);

        //    }
        //    catch (Exception ex)
        //    { 
        //       MessageBox.Show(ex.ToString(), "gloEMR", MessageBoxButtons.OK);
        //       objResult = 0;
        //    }
        //    finally
        //    {
        //    if (oDBLayer != null)
        //       {
        //        oDBLayer.Dispose();
        //        oDBLayer=null;
        //       }
        //     oDBParameters = null;
        //    }
        //    return Convert.ToInt64(objResult);
        //}

        //private void AddUpdateMapingData()
        //{
        //    c1RaceConfiguration.FinishEditing();
        //    long nCategoryId = 0;
        //    long ngloCategoryID = 0;
        //    string sCategoryCode = string.Empty;
        //    string sCategoryName = string.Empty;
        //    string sCategoryDesc = string.Empty;
        //    try
        //    {
        //            for (int rowcont = 1; rowcont < c1RaceConfiguration.Rows.Count ; rowcont++)
        //            {
        //                sCategoryCode = string.Empty;
        //                sCategoryName = Convert.ToString(c1RaceConfiguration.GetData(rowcont, COLUM_SPIRORACE)).Trim();
        //                sCategoryDesc = Convert.ToString(c1RaceConfiguration.GetData(rowcont, COLUM_EMRRACE)).Trim() ;
        //                if (sCategoryName != string.Empty && sCategoryName != null && sCategoryName != "" && sCategoryDesc != string.Empty && sCategoryDesc != null && sCategoryDesc!= "")
        //                    {
        //                        nCategoryId = GetCategoryMasterID(sCategoryName, sCategoryDesc);
        //                        if (nCategoryId == 0)
        //                        {
        //                            nCategoryId = GetUniqueueId();
        //                        }
        //                        sCategoryCode = RetriveCategoryCode(sCategoryName);
        //                        ngloCategoryID = RetirivegloCategoryId(sCategoryDesc);
        //                        if (nCategoryId != 0 && ngloCategoryID != 0 && sCategoryCode != string.Empty)
        //                        {
        //                            SaveUpdateMappedRace(nCategoryId, sCategoryCode, sCategoryName, sCategoryDesc, ClinicID, false, "Spiro", PatientID, " ", ngloCategoryID);
        //                        }

        //                    } // if empty or null
                           
        //            } // for
        //    }
        //    catch (Exception Ex)
        //    {
        //        MessageBox.Show(Ex.ToString(), "gloEMR", MessageBoxButtons.OK); 
        //    }

        //}

        //private void SaveUpdateMappedRace(long _nCategoryId, string _sCategoryCode, string _sCategoryName, string _sCategoryDesc, Int64 _ClinicID, bool _bIsBlocked, string _sCategoryType, Int64 _nParentID, string _sSystemCode, Int64 _gloCategoryID)
        //{
        //    gloDatabaseLayer.DBLayer oDBLayer = null;
        //    gloDatabaseLayer.DBParameters oDBParameters = null;
        //    try
        //    {
        //        oDBLayer = new gloDatabaseLayer.DBLayer(SpiroConnectionString);
        //        oDBParameters = new gloDatabaseLayer.DBParameters();
        //        oDBParameters.Add("@nCategoryId", _nCategoryId, ParameterDirection.Input, SqlDbType.BigInt);
        //        oDBParameters.Add("@sCategoryCode", _sCategoryCode, ParameterDirection.Input, SqlDbType.VarChar, 55);
        //        oDBParameters.Add("@sCategoryName", _sCategoryName, ParameterDirection.Input, SqlDbType.VarChar, 255);
        //        oDBParameters.Add("@sCategoryDesc", _sCategoryDesc, ParameterDirection.Input, SqlDbType.VarChar, 512);
        //        oDBParameters.Add("@nClinicID", _ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
        //        oDBParameters.Add("@bIsBlocked", _bIsBlocked, ParameterDirection.Input, SqlDbType.Bit);
        //        oDBParameters.Add("@sCategoryType", _sCategoryType, ParameterDirection.Input, SqlDbType.VarChar, 50);
        //        oDBParameters.Add("@nParentID", _nParentID, ParameterDirection.Input, SqlDbType.Int);
        //        oDBParameters.Add("@sSystemCode", _sSystemCode, ParameterDirection.Input, SqlDbType.VarChar, 255);
        //        oDBParameters.Add("@ngloCategoryId", _gloCategoryID, ParameterDirection.Input, SqlDbType.BigInt);
        //        oDBLayer.Connect(false);
        //        oDBLayer.Execute("INUP_e_CategoryMST", oDBParameters);
        //        oDBLayer.Disconnect();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString(), "gloEMR", MessageBoxButtons.OK);
        //    }
        //    finally
        //    {
        //        if (oDBLayer != null)
        //        {
        //            oDBLayer.Dispose();
        //            oDBLayer = null;
        //        }
        //        oDBParameters = null;
        //    }
        //}


       

        //private DataTable RetrieveEMRRace()
        //{
        //    DataTable _LoadEMRRace = null;
        //    gloDatabaseLayer.DBLayer _oDbLayer = null;
        //   try
        //    {
        //        _oDbLayer = new gloDatabaseLayer.DBLayer(ConnectionString);
        //        _oDbLayer.Connect(false);
        //        _oDbLayer.Retrive("SpiroGetEMRRace", out _LoadEMRRace);
        //        _oDbLayer.Disconnect();

        //    }
        //    catch (Exception ex)
        //    { MessageBox.Show(ex.ToString(), "gloEMR", MessageBoxButtons.OK); }
        //    finally
        //    {
        //        if (_oDbLayer != null)
        //        {
        //            _oDbLayer.Dispose();
        //            _oDbLayer = null;
        //        }

        //    }
        //    return _LoadEMRRace;
        //}

        //private long GetUniqueueId()
        //{
        //    gloDatabaseLayer.DBLayer oDBLayer = null;
        //    gloDatabaseLayer.DBParameters oDBParameters = null;
        //    object objResult = null;
        //    try
        //    {
        //        oDBLayer = new gloDatabaseLayer.DBLayer(ConnectionString.ToString());
        //        oDBParameters = new gloDatabaseLayer.DBParameters();
        //        oDBParameters.Clear();
        //        oDBParameters.Add("@ID", "0", ParameterDirection.Output, SqlDbType.BigInt);
        //        oDBLayer.Connect(false);
        //        oDBLayer.Execute("gsp_GetUniqueID", oDBParameters, out objResult);
        //        oDBLayer.Disconnect();
        //    }
        //    catch (Exception ex)
        //    { MessageBox.Show(ex.ToString(), "gloEMR", MessageBoxButtons.OK); }
        //    finally
        //    {
        //        if (oDBLayer != null)
        //        {
        //            oDBLayer.Dispose();
        //            oDBLayer = null;
        //        }
        //        if (oDBParameters != null)
        //        {
        //            oDBParameters.Dispose();
        //            oDBParameters = null;
        //        }
        //    }
        //    return Convert.ToInt64(objResult);
        //}

        //private string RetriveCategoryCode(string _sCategoryCode)
        //{
        //    string _RetriveCategoryCode = ""; 
        //    switch (_sCategoryCode)
        //        {
        //            case "Unspecified":
        //            {
        //                _RetriveCategoryCode = "0";
        //                break; 
        //            }
        //            case "Caucasian":
        //            {
        //                _RetriveCategoryCode = "1";
        //                break;
        //            }
        //            case "Black":
        //            {
        //                _RetriveCategoryCode = "2";
        //                break;
        //            }
        //            case "Asian":
        //            {
        //                _RetriveCategoryCode = "3";
        //                break;
        //            }
        //            case "American Indian":
        //            {
        //                _RetriveCategoryCode = "4";
        //                break;
        //            }
        //            case "Hispanic":
        //            {
        //                _RetriveCategoryCode = "5";
        //                break;
        //            }
        //            case "African-American":
        //            {
        //                _RetriveCategoryCode = "6";
        //                break;
        //            }
        //            case "African-Eruption":
        //            {
        //                _RetriveCategoryCode = "7";
        //                break;
        //            }
                

        //        }


        //    return _RetriveCategoryCode;
        //}

        //private long RetirivegloCategoryId(string _glocategoryRace)
        //{
        //    gloDatabaseLayer.DBLayer oDBLayer = null;
        //    gloDatabaseLayer.DBParameters oDBParameters = null;
        //    object objResult = null;
        //    try
        //    {
        //        oDBLayer = new gloDatabaseLayer.DBLayer(ConnectionString.ToString());
        //        oDBParameters = new gloDatabaseLayer.DBParameters();
        //        oDBParameters.Clear();
        //        oDBParameters.Add("@sDescription", _glocategoryRace, ParameterDirection.Input, SqlDbType.VarChar);
        //        oDBParameters.Add("@nCategoryID", "0", ParameterDirection.Output, SqlDbType.BigInt);
        //        oDBLayer.Connect(false);
        //        oDBLayer.Execute("RetriveGloRace", oDBParameters, out objResult);
        //        oDBLayer.Disconnect();
        //    }
        //    catch (Exception ex)
        //    { MessageBox.Show(ex.ToString(), "gloEMR", MessageBoxButtons.OK); }
        //    finally
        //    {
        //        if (oDBLayer != null)
        //        {
        //            oDBLayer.Dispose();
        //            oDBLayer = null;
        //        }
        //        if (oDBParameters != null)
        //        {
        //            oDBParameters.Dispose();
        //            oDBParameters = null;
        //        }
        //    }
        //    return Convert.ToInt64(objResult);
        //}

        //private bool IsValidate()
        //{
        //    bool _IsValidate = true;
        //    string sCategoryName = string.Empty;
        //    string sCategoryDesc = string.Empty;
        //    for (int irow = 1; irow < c1RaceConfiguration.Rows.Count   ; irow++)
        //    {
        //        sCategoryName = Convert.ToString(c1RaceConfiguration.GetData(irow, COLUM_EMRRACE)).Trim();
        //        sCategoryDesc = Convert.ToString(c1RaceConfiguration.GetData(irow, COLUM_SPIRORACE)).Trim();
        //        if (sCategoryName != "" && sCategoryDesc == "")
        //        {
        //            _IsValidate = false;
        //             MessageBox.Show("Race Is not Configured", "gloEMR", MessageBoxButtons.OK);
        //             break;
        //        }
        //    }
        //    return _IsValidate; 
        //}

#endregion

        
    }
}
