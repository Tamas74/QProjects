using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace gloEmdeonInterface.Forms
{
    public partial class frmViewSpirometryRace : Form
    {
        #region "Declaration"
        private const Int16 COL_RaceID = 0;
        private const Int16 COL_RaceCode = 1;
        private const Int16 COL_RaceName = 2;
        private const Int16 COL_RaceDesc = 3;
        private const Int16 COL_nClinicID = 4;
        private const Int16 COL_IsBloked = 5;
        private const Int16 COL_COUNT = 6;
        private string gloConnectionstring=string.Empty; 
        private string SpiroConnectionString = string.Empty;
        private TreeView _TrvMappedRace = null;


       
        private string _gstrMessageBoxCaption = string.Empty;
        private long _ClinicID = 0;


        #endregion
        
        #region "Constractor"

        public frmViewSpirometryRace(string GloEmrConnectionString, string SpirometryConnectionString, TreeView TrvMappedRace)
        {
            InitializeComponent();
            gloConnectionstring = GloEmrConnectionString;
            SpiroConnectionString = SpirometryConnectionString;
            _TrvMappedRace = TrvMappedRace;

            #region " Retrieve MessageBoxCaption from AppSettings "
            System.Collections.Specialized.NameValueCollection appSettings =null;
            try
            {
                appSettings = System.Configuration.ConfigurationManager.AppSettings;
                if (appSettings["MessageBOXCaption"] != null)
                {
                    if (appSettings["MessageBOXCaption"].Length > 0)
                    {
                        _gstrMessageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                    }
                    else
                    {
                        _gstrMessageBoxCaption = "gloEMR";
                    }
                }
                else
                { 
                    _gstrMessageBoxCaption = "gloEMR"; 
                }

                if (appSettings["ClinicID"] != null)
                {
                    if (appSettings["ClinicID"].Length > 0)
                    {
                        _ClinicID = Convert.ToInt64(appSettings["ClinicID"]);
                    }
                    else
                    {
                        _ClinicID = 1;
                    }
                }
                else
                {
                    _ClinicID = 1;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Spirometry, gloAuditTrail.ActivityType.Add, " Error while reading values from AppSetting" + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                ex = null;
            }
            finally
            {
                appSettings = null;
            }
            #endregion
        }

        #endregion



        #region "Events"


        private void frmVWSpiroRace_Load(object sender, EventArgs e)
        {
            gloEmdeonCommon.gloC1FlexStyle.Style(c1SpiroRace, false);
            LoadSpiroRace();

        }

        private void ts_LabMain_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

            switch (e.ClickedItem.Tag.ToString())
            {
                case "New":
                    {
                        frmAddSpirometryRace frmobj = new frmAddSpirometryRace(0,SpiroConnectionString,gloConnectionstring );
                        frmobj.ShowDialog(this);
                        if (frmobj.IsDataChanged)
                        {
                            LoadSpiroRace();
                        }
                        if (frmobj != null)
                        {
                            frmobj.Dispose();
                            frmobj = null;
                        }
                        break;
                    }
                case "Modify":
                    {
                        if (c1SpiroRace.RowSel > 0)
                        {
                            long CategoryID = GetSelectedCategoryID();
                            if (CategoryID != 0)
                            {
                                frmAddSpirometryRace frmobj = new frmAddSpirometryRace(CategoryID, SpiroConnectionString, gloConnectionstring);
                                frmobj.ShowDialog(this);
                                if (frmobj.IsDataChanged)
                                {
                                    LoadSpiroRace();
                                }
                                if (frmobj != null)
                                {
                                    frmobj.Dispose();
                                    frmobj = null;
                                }
                            }
                           
                       }
                      break;
                    }
                case "Delete":
                    {
                        if (c1SpiroRace.RowSel > 0)
                        {
                            gloEmdeonInterface.Classes.clsCategoryMST objcatMst = null;
                            try
                            {
                                long CategoryID = GetSelectedCategoryID();
                                objcatMst = new gloEmdeonInterface.Classes.clsCategoryMST(SpiroConnectionString);
                                if (CategoryID != 0)
                                {
                                    if (IsMapped(CategoryID) == false)
                                    {
                                        if (MessageBox.Show("Are you sure you want to delete?", _gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                        {
                                            objcatMst.DeleteSpiroRace(CategoryID);
                                            objcatMst.DeleteMappedRace(CategoryID);   
                                            LoadSpiroRace();
                                        }
                                    }
                                    else
                                    {

                                        MessageBox.Show("Race is already in use.", _gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                }//if
                            }
                            catch (Exception ex)
                            {
                                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Spirometry, gloAuditTrail.ActivityType.Add, " Error While Deleting Device Race" + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                                ex = null;
                            }
                            finally
                            {
                                if (objcatMst != null)
                                {
                                    objcatMst.Dispose();
                                    objcatMst = null;
                                }
                            }
                        }// if
                       break;
                    }
                case "Close":
                    {
                        this.Close();
                        break;
                    }
            }

        }

        #endregion

        #region "Method and function"

        private bool IsMapped(long RaceID)
        {
            bool _IsMappped = false;
            long _SpiroRaceID = 0;
            if (_TrvMappedRace != null)
            {
                foreach (TreeNode MappedNode in _TrvMappedRace.Nodes[0].Nodes)
                {
                    _SpiroRaceID = 0;
                    if (MappedNode.Nodes.Count == 1 )
                    {
                        long.TryParse(Convert.ToString(MappedNode.Nodes[0].Tag), out _SpiroRaceID);
                        if (_SpiroRaceID == RaceID)
                        {
                            _IsMappped = true;
                            break;
                        }
                    }
                }

            }
            return _IsMappped;
        }
       
        private long GetSelectedCategoryID()
        {
              long _GetSelectedCategoryID = 0;
              if (c1SpiroRace.RowSel > 0)
              {
                  Int64.TryParse(c1SpiroRace.GetData(c1SpiroRace.RowSel, COL_RaceID).ToString(), out _GetSelectedCategoryID);
              }
              return _GetSelectedCategoryID;
        }
        
        private void DesignGrid()
        {

            try
            {
               // c1SpiroRace.Clear();

                c1SpiroRace.DataSource = null;
                c1SpiroRace.Clear();
              
                //c1SpiroRace.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
                //// setfont
                //c1SpiroRace.Font = new System.Drawing.Font("Tahoma", 9, FontStyle.Regular);
                //c1SpiroRace.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
                //c1SpiroRace.BackColor = Color.White;
                //c1SpiroRace.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;

                c1SpiroRace.Cols.Count = COL_COUNT;
                //c1Spiro.Cols.Fixed = 1;
                c1SpiroRace.Rows.Count = 1;
                c1SpiroRace.Rows.Fixed = 1;

                // set visibility of column
                c1SpiroRace.Cols[COL_RaceID].Visible = false;
                c1SpiroRace.Cols[COL_RaceCode].Visible = true;
                c1SpiroRace.Cols[COL_RaceName].Visible = true;
                c1SpiroRace.Cols[COL_RaceDesc].Visible = false;
                c1SpiroRace.Cols[COL_nClinicID].Visible = false;
                c1SpiroRace.Cols[COL_IsBloked].Visible = false;
             

                // set column type
                c1SpiroRace.Cols[COL_IsBloked].DataType = typeof(bool);
                c1SpiroRace.AllowEditing = true;



                // set column editing

                c1SpiroRace.Cols[COL_RaceID].AllowEditing = false;
                c1SpiroRace.Cols[COL_RaceCode].AllowEditing = false;
                c1SpiroRace.Cols[COL_RaceName].AllowEditing = false;
                c1SpiroRace.Cols[COL_RaceDesc].AllowEditing = false;
                c1SpiroRace.Cols[COL_nClinicID].AllowEditing = false;
                c1SpiroRace.Cols[COL_IsBloked].AllowEditing = false;


                // set alignment

                //c1SpiroRace.Cols[COL_RaceID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                //c1SpiroRace.Cols[COL_RaceCode].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                //c1SpiroRace.Cols[COL_RaceName].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                //c1SpiroRace.Cols[COL_RaceDesc].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                //c1SpiroRace.Cols[COL_nClinicID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                //c1SpiroRace.Cols[COL_IsBloked].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;


                //set Heading
                c1SpiroRace.SetData(0, COL_RaceID, "Race ID");
                c1SpiroRace.SetData(0, COL_RaceCode, "Race Code");
                c1SpiroRace.SetData(0, COL_RaceName, "Race Name");
                c1SpiroRace.SetData(0, COL_RaceDesc, "Race Desc");
                c1SpiroRace.SetData(0, COL_nClinicID, "Clinic ID");
                c1SpiroRace.SetData(0, COL_IsBloked, "Blocked");
           
              


                // set width

                c1SpiroRace.Cols[COL_RaceID].Width = 0;
                c1SpiroRace.Cols[COL_RaceCode].Width =Convert.ToInt32(this.Width * 0.20);
                c1SpiroRace.Cols[COL_RaceName].Width = Convert.ToInt32(this.Width * 0.80);
                c1SpiroRace.Cols[COL_RaceDesc].Width = 0;
                c1SpiroRace.Cols[COL_nClinicID].Width = 0;
                c1SpiroRace.Cols[COL_IsBloked].Width = 0;

                c1SpiroRace.ExtendLastCol = true;

            }
            catch (Exception ex)
            {
                //gloEmdeonInterface.Classes.clsGeneral obj = new gloEmdeonInterface.Classes.clsGeneral();
                //obj.UpdateLog("Error In Designing Grid " + e.ToString());
                //obj.Dispose(); 
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Spirometry, gloAuditTrail.ActivityType.Add, " Error in frmViewSpirometryRace.DesignGrid() " + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                ex = null;

            }

        }

        /// <summary>
        /// Set data in C1 flex
        /// </summary>
        private void LoadSpiroRace()
        {
            DesignGrid();
            gloEmdeonInterface.Classes.clsCategoryMST objcatMst = null;
            DataTable dtSpirorace = null;
            try
            {
                objcatMst = new gloEmdeonInterface.Classes.clsCategoryMST(SpiroConnectionString);
                dtSpirorace = objcatMst.RetriveSpiroRace();           
                for (int irow = 0; irow <= dtSpirorace.Rows.Count -1 ; irow++)
                {
                    c1SpiroRace.Rows.Add();
                    Int32 _Row = c1SpiroRace.Rows.Count - 1;
                    c1SpiroRace.SetData(_Row, COL_RaceID, dtSpirorace.Rows[irow][0].ToString());
                    c1SpiroRace.SetData(_Row, COL_RaceCode, dtSpirorace.Rows[irow][1].ToString());
                    c1SpiroRace.SetData(_Row, COL_RaceName, dtSpirorace.Rows[irow][2].ToString());
                    c1SpiroRace.SetData(_Row, COL_RaceDesc, dtSpirorace.Rows[irow][3].ToString());
                    c1SpiroRace.SetData(_Row, COL_nClinicID, dtSpirorace.Rows[irow][4].ToString());
                    c1SpiroRace.SetData(_Row, COL_IsBloked, dtSpirorace.Rows[irow][5].ToString());

                }
            }
            catch (Exception ex)
            {
                //gloEmdeonInterface.Classes.clsGeneral obj = new gloEmdeonInterface.Classes.clsGeneral();
                //obj.UpdateLog("Error In Designing Grid " + e.ToString());
                //obj.Dispose();
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Spirometry, gloAuditTrail.ActivityType.Add, " Error in frmViewSpirometryRace.LoadSpiroRace() " + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                ex = null;

            }
            finally
            {
                if (objcatMst != null)
                {
                    objcatMst.Dispose();
                    objcatMst = null;
                }
                if (dtSpirorace != null)
                {
                    dtSpirorace.Dispose();
                    dtSpirorace = null;
                }

            }
        }





        #endregion

      
     
    }
}
