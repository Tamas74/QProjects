using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using gloGallery;
using gloGlobal;
using gloDateMaster;



namespace gloGallery
{
    public partial class frmICD9CPTGallery : Form
    {
        public clsGallery.GalleryType GalleryType { get; set; }
        int MaxICDorCPTCount=200;
        #region Variable Declaration

        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

        private string _messageBoxCaption = "";
        private string _databaseConnectionString = "";
        private Int64 _clinicID;

        #endregion Variable Declaration

        #region Constructor

        public frmICD9CPTGallery(string DatabaseConnectionString)
        {
            InitializeComponent();
            RetriveFormInfo();

            _databaseConnectionString = DatabaseConnectionString;
        }

        public frmICD9CPTGallery(string DatabaseConnectionString, clsGallery.GalleryType galleryType)
        {
            InitializeComponent();
            RetriveFormInfo();

            GalleryType = galleryType;
            _databaseConnectionString = DatabaseConnectionString;
        }

        private void RetriveFormInfo()
        {
            #region " Retrieve MessageBoxCaption from AppSettings "

            if (appSettings["MessageBOXCaption"] != null)
            {
                if (appSettings["MessageBOXCaption"] != "")
                {
                    _messageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    _messageBoxCaption = "";
                }
            }
            else
            { _messageBoxCaption = ""; }

            #endregion

            #region " Retrieve ClinicID from AppSettings "

            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _clinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _clinicID = 0; }
            }
            else
            { _clinicID = 0; }

            #endregion

        }

        #endregion Constructor

        #region Page Load

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle = cp.ExStyle | 0x2000000;
                return cp;
            }
        }

        private void frmICD9CPTGallery_Load(object sender, System.EventArgs e)
        {
            lblCopyRight.Text = gloTransparentScreen.clsgloCopyRightText.gloCopyRightMain;
            Label35.Text = gloTransparentScreen.clsgloCopyRightText.gloCopyRightMain;
            
            
            //change made for screen resolution  bugid 64951,64952,64953
            int   myScreenWidth =(int ) System.Windows.SystemParameters.PrimaryScreenWidth;
            int  myScreenHeight =(int) System.Windows.SystemParameters.PrimaryScreenHeight-50; //changed the location for bottom margin
            if ((this.Width > myScreenWidth) || (this.Height > myScreenHeight))
            {
                this.MaximumSize = new System.Drawing.Size(myScreenWidth, (myScreenHeight ));
                this.AutoScroll = true;

                //this.AutoSize = true;
                //this.AutoSizeMode = AutoSizeMode.GrowAndShrink;

            }



            LoadData();
        }

        private void LoadData()
        {
            this.Cursor = Cursors.WaitCursor;
            FillSpeciality();
            
            if (GalleryType == clsGallery.GalleryType.CPT)
            {
                pnlIndicator.Visible = false;
                pnlICD9bottom.Visible = false;
                FillCategory();
                FillActivationDates();
            }
            else
            {
                pnlIndicator.Visible = true;
                pnlICD9bottom.Visible = true;
                FillCodeIndicators();
            }
            DBICD9CPT oDBICD9CPT = null;
            try
            {
                oDBICD9CPT = new DBICD9CPT(_databaseConnectionString);
                MaxICDorCPTCount= oDBICD9CPT.GetMaxICDorCPTCount(GalleryType);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            LoadGallery();
            LoadMasterCodes();
            this.Cursor = Cursors.Default;
        }

        #endregion Page Load

        # region Form Control Events

        private void tlICD9CptGallery_ItemClicked(object sender, System.Windows.Forms.ToolStripItemClickedEventArgs e)
        {
            if (Convert.ToString(e.ClickedItem.Tag) == "Close")
            {
                this.Close();
            }
        }

        private void btnUnSelectGalleryCode_Click(object sender, EventArgs e)
        {
            gloUCGallery.UncheckAllNodes();
            btnSelectAllGalleryCode.Visible = true;
            btnUnSelectGalleryCode.Visible = false;
        }

        private void btnSelectAllGalleryCode_Click(object sender, EventArgs e)
        {
            gloUCGallery.CheckAllNodes();
            btnSelectAllGalleryCode.Visible = false;
            btnUnSelectGalleryCode.Visible = true;
        }

        private void btnUnSelectAllMasterCodes_Click(object sender, EventArgs e)
        {
            gloUCMaster.UncheckAllNodes();
            btnSelectAllMasterCodes.Visible = true;
            btnUnSelectAllMasterCodes.Visible = false;
        }

        private void btnSelectAllMasterCodes_Click(object sender, EventArgs e)
        {
            gloUCMaster.CheckAllNodes();
            btnSelectAllMasterCodes.Visible = false;
            btnUnSelectAllMasterCodes.Visible = true;
        }

        private void cmbSpeciality_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                if (cmbSpeciality.SelectedIndex != -1)
                {
                    LoadMasterCodes();
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

        private void cmbCategory_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                if (cmbCategory.SelectedIndex != -1)
                {
                    LoadMasterCodes();
                }
            }
            catch (Exception ex)
            {

                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

        private void btnAddCode_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (GalleryType == clsGallery.GalleryType.ICD10 || GalleryType == clsGallery.GalleryType.ICD9)
                {
                    AddToMasterICD();
                }
                else
                {
                    AddToMasterCPT();
                }

                RefreshControls();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog("Exception while adding a code to master" + ex.ToString(), true);
            }
            finally 
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void btnRemoveCode_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                RemoveFromMaster();
                RefreshControls();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }

        }

        private void glo_trvICD9Gallery_NodeAdded(myTreeNode ChildNode)
        {
            try
            {
                //if (cmbICD9Gallery != null)
                //{
                //    if (cmbICD9Gallery.Text == "All")
                //    {
                //        if (ChildNode != null)
                //        {
                //            //'NEW
                //            if ((ChildNode.Indicator == "N"))
                //            {
                //                ChildNode.ImageIndex = 8;
                //                ChildNode.SelectedImageIndex = 8;
                //                //REVISED'
                //            }
                //            else if ((ChildNode.Indicator == "R"))
                //            {
                //                ChildNode.ImageIndex = 9;
                //                ChildNode.SelectedImageIndex = 9;
                //                //'NOT [NEW and REVISED]
                //            }
                //            else if ((ChildNode.Indicator != "R" & ChildNode.Indicator != "N"))
                //            {
                //                ChildNode.ImageIndex = 4;
                //                ChildNode.SelectedImageIndex = 4;
                //            }
                //        }
                //        else
                //        {
                //            gloAuditTrail.gloAuditTrail.ExceptionLog("Child node is null", false);
                //        }
                //        //'NEW
                //    }
                //    else if (cmbICD9Gallery.Text == "New")
                //    {
                //        if (gloTrvICD9Gallery != null)
                //        {
                //            gloTrvICD9Gallery.ImageIndex = 8;
                //            gloTrvICD9Gallery.SelectedImageIndex = 8;
                //        }
                //        else
                //        {
                //            gloAuditTrail.gloAuditTrail.ExceptionLog("gloTrvICD9Gallery node is null", false);
                //        }
                //        //REVISED'
                //    }
                //    else if (cmbICD9Gallery.Text == "Revised")
                //    {
                //        if (gloTrvICD9Gallery != null)
                //        {
                //            gloTrvICD9Gallery.ImageIndex = 9;
                //            gloTrvICD9Gallery.SelectedImageIndex = 9;
                //        }
                //        else
                //        {
                //            gloAuditTrail.gloAuditTrail.ExceptionLog("gloTrvICD9Gallery node is null", false);

                //        }
                //    }
                //    else if (cmbICD9Gallery.Text == "No Change")
                //    {
                //        if (gloTrvICD9Gallery != null)
                //        {
                //            gloTrvICD9Gallery.ImageIndex = 4;
                //            gloTrvICD9Gallery.SelectedImageIndex = 4;
                //        }
                //        else
                //        {
                //            gloAuditTrail.gloAuditTrail.ExceptionLog("gloTrvICD9Gallery node is null", false);
                //        }
                //    }
                //}
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }

        }

        # endregion Form Control Events

        #region "Revised Codes"

        //public string SelectedMappingFilter
        //{
        //    get
        //    {
        //        if (rdb_AllCodes.Checked)
        //        {
        //            return "ALL";
        //        }
        //        else if (rdb_GemsMapping.Checked)
        //        {
        //            return "MAPPED";
        //        }

        //        return "ALL";
        //    }
        //}

        public bool SelectedMappingFilter
        {
            get
            {
                if (chkMapping.Checked)
                {
                    return true;
                }
                else if (chkMapping.Checked==false)
                {
                    return false;
                }

                return false;
            }
        }

        public bool SelectedUnUsedFilter
        {
            get
            {
                if (chkUnsusedOnly.Checked)
                {
                    return true;
                }
                else if (chkUnsusedOnly.Checked == false)
                {
                    return false;
                }

                return false;
            }
        }


        public string SelectedIndicatorFilter
        {
            get
            {
                if (cmbICD9Gallery.SelectedIndex>=0)
                {
                    return Convert.ToString(cmbICD9Gallery.SelectedValue);
                }
                return "ALL";
            }
        }

        private void LoadGallery()
        {
            DataTable dtGallery = null;

            this.Cursor = Cursors.WaitCursor;

            try
            {
                this.Text = GalleryType.ToString() + " Gallery";
                this.lblGalleryHeader.Text = this.Text;
                this.lblMaster.Text = "Current " + GalleryType.ToString();

                if (GalleryType == clsGallery.GalleryType.ICD10)
                {
                    pnlCategory.Visible = false;
                    SetDefaultMapping();
                    dtGallery = clsGallery.GetGallery(_databaseConnectionString, clsGallery.GalleryType.ICD10,SelectedUnUsedFilter, SelectedMappingFilter, SelectedIndicatorFilter);
                }
                else if (GalleryType == clsGallery.GalleryType.ICD9)
                {
                    pnlCategory.Visible = false;
                    pnlMapping.Visible = true;
                    chkMapping.Visible = false; 
                    //panel1.Visible = false;
                    dtGallery = clsGallery.GetGallery(_databaseConnectionString, clsGallery.GalleryType.ICD9, SelectedUnUsedFilter,false, SelectedIndicatorFilter);
                }
                else
                {                    
                    panel1.Visible = false;
                    pnlMapping.Visible = false;
                    pnlActivationDatesFilter.Visible = true;

                    string sValue = "ALL";

                    if (cmbActivationDate.SelectedValue is string)
                    { sValue = (string)(cmbActivationDate.SelectedValue); }

                    dtGallery = clsGallery.GetCPTGallery(_databaseConnectionString, sValue);
                }
                
                    FillGallery(dtGallery);                                
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.ICD, gloAuditTrail.ActivityCategory.ICD9, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }
            finally
            {
                if (dtGallery != null) { dtGallery.Dispose(); dtGallery = null; }
                this.Cursor = Cursors.Default;
            }
        }

        private void FillGallery(DataTable dtGallery)
        {
            gloUCGallery.ClearSearchText();            
            gloUCGallery.NodeAdded += new gloUC_TreeView.NodeAddedEventHandler(gloUCGallery_NodeAdded);
            gloUCGallery.DataSource = dtGallery;

            if (GalleryType == clsGallery.GalleryType.ICD10 || GalleryType == clsGallery.GalleryType.ICD9)
            {
                gloUCGallery.ValueMember = dtGallery.Columns["nICD9ID"].ColumnName;
                gloUCGallery.CodeMember = dtGallery.Columns["sICD9Code"].ColumnName;
                gloUCGallery.DescriptionMember = dtGallery.Columns["sDescriptionShort"].ColumnName;
                gloUCGallery.Indicator = dtGallery.Columns["sIndicator"].ColumnName;
            }
            else
            {
                gloUCGallery.ValueMember = dtGallery.Columns["nCPTID"].ColumnName;
                gloUCGallery.CodeMember = dtGallery.Columns["sCPTCode"].ColumnName;
                gloUCGallery.DescriptionMember = dtGallery.Columns["sDescription"].ColumnName;

                if (dtGallery.Columns.Contains("dtActivationDate"))
                { gloUCGallery.CPTActivationDate = dtGallery.Columns["dtActivationDate"].ColumnName; }

                if (dtGallery.Columns.Contains("dtDeactivationDate"))
                { gloUCGallery.CPTDeactivationDate = dtGallery.Columns["dtDeactivationDate"].ColumnName; }                
            }
          
            gloUCGallery.ImageIndex = 4;
            gloUCGallery.SelectedImageIndex = 4;
            gloUCGallery.MaximumNodes = MaxICDorCPTCount;
            gloUCGallery.FillTreeView();
        }

        private void SetDefaultMapping()
        {
           
            try
            {
                if (gloGlobal.clsICD.GetICDCodeType(0, gloDate.DateAsNumber(DateTime.Now.Date.ToString()),_clinicID) == gloGlobal.gloICD.CodeRevision.ICD10)
                {
                    chkMapping.CheckedChanged -= new EventHandler(chkMapping_CheckedChanged);
                    chkMapping.Checked = false;
                    chkMapping.CheckedChanged += new EventHandler(chkMapping_CheckedChanged);
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog("Exception while setting a default Mapping Checked " + ex.ToString(), true);
            }
            

        }

        private void gloUCGallery_NodeAdded(myTreeNode ChildNode)
        {
            try
            {
                if (cmbICD9Gallery != null)
                {
                    if (cmbICD9Gallery.Text == "All")
                    {
                        if (ChildNode != null)
                        {
                            //'NEW
                            if (GalleryType == clsGallery.GalleryType.ICD10 & ChildNode.Indicator == "N")
                            {
                                ChildNode.ImageIndex = 9;
                                ChildNode.SelectedImageIndex = 9;
                                //REVISED'
                            }
                            else if (GalleryType == clsGallery.GalleryType.ICD9 & ChildNode.Indicator == "N")
                            {
                                ChildNode.ImageIndex = 8;
                                ChildNode.SelectedImageIndex = 8;
                            }

                            if (GalleryType == clsGallery.GalleryType.ICD10 & ChildNode.Indicator == "A")
                            {
                                ChildNode.ImageIndex = 8;
                                ChildNode.SelectedImageIndex = 8;
                            }
                            else if (GalleryType == clsGallery.GalleryType.ICD9 & ChildNode.Indicator == "R")
                            {
                                ChildNode.ImageIndex = 9;
                                ChildNode.SelectedImageIndex = 9;
                            }

                            //if ((ChildNode.Indicator == "R"))
                            //{
                            //    ChildNode.ImageIndex = 9;
                            //    ChildNode.SelectedImageIndex = 9;
                            //    // No Change
                            //}
                            else if ((ChildNode.Indicator == "D"))
                            {
                                ChildNode.ImageIndex = 14;
                                ChildNode.SelectedImageIndex = 14;
                                //'Deleted
                            }
                            else if ((ChildNode.Indicator != "R" & ChildNode.Indicator != "A" & ChildNode.Indicator != "D" & ChildNode.Indicator != "O" & ChildNode.Indicator != "N"))
                            {
                                ChildNode.ImageIndex = 4;
                                ChildNode.SelectedImageIndex = 4;
                            }
                        }
                        else
                        {
                            gloAuditTrail.gloAuditTrail.ExceptionLog("Child node is null", false);
                        }

                       return;
                    }
                    if (gloUCGallery != null)
                    {

                        if (GalleryType == clsGallery.GalleryType.ICD10 && SelectedIndicatorFilter == "N")
                        {
                            gloUCGallery.ImageIndex = 9;
                            gloUCGallery.SelectedImageIndex = 9;
                            return;
                        }
                        else if (GalleryType == clsGallery.GalleryType.ICD9 && SelectedIndicatorFilter == "N")
                        {
                            gloUCGallery.ImageIndex = 8;
                            gloUCGallery.SelectedImageIndex = 8;
                            return;
                        }


                        if (GalleryType == clsGallery.GalleryType.ICD10 && SelectedIndicatorFilter == "A")
                        {
                            gloUCGallery.ImageIndex = 9;
                            gloUCGallery.SelectedImageIndex = 9;
                            return;
                        }
                        else if (GalleryType == clsGallery.GalleryType.ICD9 && SelectedIndicatorFilter == "R")
                        {
                            gloUCGallery.ImageIndex = 9;
                            gloUCGallery.SelectedImageIndex = 9;
                            return;
                        }


                        if (SelectedIndicatorFilter == "D")
                        {
                            gloUCGallery.ImageIndex = 14;
                            gloUCGallery.SelectedImageIndex = 14;
                            return; 
                        }

                        if (SelectedIndicatorFilter == "NC")
                        {

                            gloUCGallery.ImageIndex = 4;
                            gloUCGallery.SelectedImageIndex = 4;
                            return;

                        }
                    }
                    
                    else
                    {
                        gloAuditTrail.gloAuditTrail.ExceptionLog("gloTrvICD9Gallery node is null", false);
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }

        }

        private void LoadMasterCodes()
        {
            DataTable dtMaster = null;
            try
            {
                Int64 specialityID = 0;

                if (cmbSpeciality.Items.Count > 0)
                {
                    specialityID = Convert.ToInt64(cmbSpeciality.SelectedValue);
                } 

                if (GalleryType == clsGallery.GalleryType.ICD10)
                {
                    dtMaster = clsGallery.GetMaster(_databaseConnectionString, clsGallery.GalleryType.ICD10, specialityID);
                }
                else if (GalleryType == clsGallery.GalleryType.ICD9)
                {
                    dtMaster = clsGallery.GetMaster(_databaseConnectionString, clsGallery.GalleryType.ICD9, specialityID);
                }
                else if (GalleryType == clsGallery.GalleryType.CPT)
                {
                    Int64 categoryID  = 0;
                    if (cmbCategory.Items.Count > 0)
                    {
                        categoryID = Convert.ToInt64(cmbCategory.SelectedValue);
                    }
                    dtMaster = clsGallery.GetMaster(_databaseConnectionString, clsGallery.GalleryType.CPT, specialityID, categoryID);
                }

                FillMasterCodes(dtMaster);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.ICD, gloAuditTrail.ActivityCategory.ICD9, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }
            finally
            {
                if (dtMaster != null) { dtMaster.Dispose(); dtMaster = null; }
            }
        }

        private void FillMasterCodes(DataTable dtMaster)
        {
            try
            {
                gloUCMaster.DataSource = dtMaster;

                if (GalleryType == clsGallery.GalleryType.ICD10 || GalleryType == clsGallery.GalleryType.ICD9)
                {
                    gloUCMaster.ValueMember = dtMaster.Columns["nICD9ID"].ColumnName;
                    gloUCMaster.CodeMember = dtMaster.Columns["sICD9Code"].ColumnName;
                    gloUCMaster.DescriptionMember = dtMaster.Columns["sDescription"].ColumnName;
                    if (GalleryType == clsGallery.GalleryType.ICD10)
                    {
                        gloUCMaster.ImageIndex = 12;
                        gloUCMaster.SelectedImageIndex = 12;
                    }
                    else
                    {
                        gloUCMaster.ImageIndex = 0;
                        gloUCMaster.SelectedImageIndex = 0;
                    }
                }
                else
                {
                    gloUCMaster.ValueMember = dtMaster.Columns["nCPTID"].ColumnName;
                    gloUCMaster.CodeMember = dtMaster.Columns["sCPTCode"].ColumnName;
                    gloUCMaster.DescriptionMember = dtMaster.Columns["sDescription"].ColumnName;
                    gloUCMaster.ImageIndex = 1;
                    gloUCMaster.SelectedImageIndex = 1;
                }

                gloUCMaster.CheckBoxes = true;
                gloUCMaster.FillTreeView();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

        private void FillCategory()
        {
            DataTable dtCategory = null;
            try
            {
                dtCategory = clsGallery.GetAllCategory(_databaseConnectionString);

                if (dtCategory != null)
                {
                    cmbCategory.ValueMember = dtCategory.Columns["nCategoryID"].ColumnName;
                    cmbCategory.DisplayMember = dtCategory.Columns["sDescription"].ColumnName;
                    cmbCategory.DataSource = dtCategory;

                    cmbCategory.SelectedIndex = cmbCategory.FindStringExact("All");
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Category, gloAuditTrail.ActivityCategory.ICD9, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }
        }


        private void FillCodeIndicators()
        {
            DataTable dtIndicators = null;

            try
            {
                if (GalleryType == clsGallery.GalleryType.ICD10)
                { dtIndicators = gloGlobal.clsICD.GetAllIndicators(gloICD.CodeRevision.ICD10); }
                else
                { dtIndicators = gloGlobal.clsICD.GetAllIndicators(gloICD.CodeRevision.ICD9); }

                if (dtIndicators != null)
                {
                    cmbICD9Gallery.DataSource = dtIndicators;
                    cmbICD9Gallery.ValueMember = dtIndicators.Columns["sIndicator"].ColumnName;
                    cmbICD9Gallery.DisplayMember = dtIndicators.Columns["sDescription"].ColumnName;

                    cmbICD9Gallery.SelectedIndex = cmbICD9Gallery.FindStringExact("All");
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.ICD, gloAuditTrail.ActivityCategory.ICD9, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }
        }

        private void FillActivationDates()
        {
            DataTable dtActivationDates = null;

            try
            {
                dtActivationDates = gloGallery.clsGallery.GetDistinctGalleryDates(_databaseConnectionString);

                if (dtActivationDates != null)
                {
                    cmbActivationDate.DataSource = dtActivationDates;
                    cmbActivationDate.ValueMember = dtActivationDates.Columns["dtActivationDate"].ColumnName;
                    cmbActivationDate.DisplayMember = dtActivationDates.Columns["dtActivationDate"].ColumnName;
                    cmbActivationDate.SelectedIndex = cmbActivationDate.FindStringExact("All");
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.ICD, gloAuditTrail.ActivityCategory.ICD9, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }
        }

        private void FillSpeciality()
        {
            DataTable dtSpeciality = null;

            try
            {
                dtSpeciality = gloGlobal.clsICD.GetAllSpeciality(true, _databaseConnectionString);

                if (dtSpeciality != null)
                {
                    cmbSpeciality.DataSource = dtSpeciality;
                    cmbSpeciality.ValueMember = dtSpeciality.Columns["nSpecialtyId"].ColumnName;
                    cmbSpeciality.DisplayMember = dtSpeciality.Columns["sDescription"].ColumnName;

                    cmbSpeciality.SelectedIndex = cmbSpeciality.FindStringExact("All");
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.ICD, gloAuditTrail.ActivityCategory.ICD9, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }
        }

        private void AddToMasterICD()
        {
            try
            {
                int countIsExist = 0;
                int countIsAdded = 0;
                if (gloUCGallery.SelectedNodes.Count == 0)
                {
                    MessageBox.Show("Please select ICD's to Add/Modify", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                foreach (myTreeNode node in gloUCGallery.SelectedNodes)
                {
                    if (gloGlobal.clsICD.IsExistsICD(0,node.Code,GalleryType.GetHashCode(),_clinicID) == true)
                    {
                        countIsExist += 1;
                    }
                    else
                    {
                        using (clsICD objICD = new clsICD()) 
                        {
                            if (GalleryType == clsGallery.GalleryType.ICD9)
                            { objICD.ICDRevision = gloICD.CodeRevision.ICD9; }
                            else if (GalleryType == clsGallery.GalleryType.ICD10)
                            { objICD.ICDRevision = gloICD.CodeRevision.ICD10; }

                            objICD.ID = 0;
                            objICD.Code = node.Code;
                            objICD.Description = node.Description;
                            objICD.SpecialityID = Convert.ToInt64(cmbSpeciality.SelectedValue);
                            objICD.ClinicID = _clinicID;
                            objICD.IsActive = false;
                            objICD.ImmediacyID = 3;

                            gloGlobal.clsICD.SaveICD(objICD);
                        }

                        countIsAdded += 1;
                    }
                }

                string _msgStr = "";

                if (countIsAdded > 0)
                {
                    _msgStr = countIsAdded +" "+ GalleryType.ToString() + "(s) inserted" + Environment.NewLine;
                }

                if (countIsExist > 0)
                {
                    _msgStr = _msgStr + countIsExist + " " +GalleryType.ToString() + "(s) already exists.";
                }

                if (!string.IsNullOrEmpty(_msgStr))
                {
                    MessageBox.Show(_msgStr, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

        private void RemoveFromMaster()
        {
            clsCPT oclsCPT = null;
            try
            {
                int countIsDeleted = 0;
                int countIsUsed = 0;

                if (gloUCMaster.SelectedNodes.Count == 0)
                {
                    MessageBox.Show("Please select " + GalleryType.ToString() + "'s to remove", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                foreach (myTreeNode node in gloUCMaster.SelectedNodes)
                {
                    if (GalleryType == clsGallery.GalleryType.ICD10 || GalleryType == clsGallery.GalleryType.ICD9)
                    {
                        if (gloGlobal.clsICD.IsInUseICD(node.Code, GalleryType.GetHashCode()) == false)
                        {
                            gloGlobal.clsICD.Delete(node.ID);
                            countIsDeleted += 1;
                        }
                        else
                        {
                            countIsUsed += 1;
                        }
                    }
                    else
                    {
                        oclsCPT=new clsCPT(_databaseConnectionString);
                        if (oclsCPT.IsCPTCodeInUse(node.Code) == false)
                        {
                            oclsCPT.DeleteCPT(node.ID, node.Code);
                            countIsDeleted += 1;
                        }
                        else
                        {
                            countIsUsed += 1;
                        }
                    }
                  
                }

              

                string _msgStr = "";

                if (countIsDeleted > 0)
                {
                    _msgStr = countIsDeleted + " " + GalleryType.ToString()+"(s) deleted" + Environment.NewLine;
                }

                if (countIsUsed > 0)
                {
                    _msgStr = countIsUsed + " " + GalleryType.ToString() + "(s) are in use and cannot be deleted" + Environment.NewLine;
                }
                            
                if (!string.IsNullOrEmpty(_msgStr))
                {
                    MessageBox.Show(_msgStr, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

        private void AddToMasterCPT()
        {
            try
            {
                int countIsExist = 0;
                int countIsAdded = 0;
                clsCPT oclsCPT = new clsCPT(_databaseConnectionString);
                foreach (myTreeNode node in gloUCGallery.SelectedNodes)
                {
                    if (oclsCPT.IsExistCPT(node.Code) == true)
                    {
                        countIsExist += 1;
                    }
                    else
                    {
                        oclsCPT.CPTID = 0;
                        countIsAdded += 1;
                        oclsCPT.CPTCode = node.Code;
                        oclsCPT.Description = node.Description;
                        oclsCPT.Inactive = false;
                        oclsCPT.nSpecialtyID = Convert.ToInt64(cmbSpeciality.SelectedValue);
                        oclsCPT.SpecialityCode = cmbSpeciality.Text;

                        oclsCPT.ActivationDate = node.CPTActivationDate;
                        oclsCPT.DeactivationDate = node.CPTDeactivationDate;

                        oclsCPT.nCategoryID = Convert.ToInt64(cmbCategory.SelectedValue);
                        oclsCPT.Categorydesc = cmbCategory.Text;
                        
                        oclsCPT.Add();
                        
                    }
                }

                string _MsgString = "";
                if (countIsAdded > 0)
                {
                    _MsgString = countIsAdded + " CPT(s) inserted " + Environment.NewLine;
                }
                if (countIsExist > 0)
                {
                    _MsgString = _MsgString + countIsExist + " CPT(s) already exists.";
                }
                if (!string.IsNullOrEmpty(_MsgString))
                {
                    MessageBox.Show(_MsgString, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

        private void RefreshControls()
        {
            try
            {
                //LoadGallery();
                LoadMasterCodes();

                btnSelectAllGalleryCode.Visible = true;
                btnUnSelectGalleryCode.Visible = false;
                btnSelectAllMasterCodes.Visible = true;
                btnUnSelectAllMasterCodes.Visible = false;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

        #endregion

        private void btnAddCode_MouseHover(object sender, EventArgs e)
        {
            ToolTip1.SetToolTip(btnAddCode, "Add and Save " + GalleryType.ToString());
        }

        private void btnRemoveCode_MouseHover(object sender, EventArgs e)
        {
            ToolTip1.SetToolTip(btnRemoveCode, "Remove and Save " + GalleryType.ToString());
        }

        //private void rdb_AllCodes_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (rdb_AllCodes.Checked == true)
        //    {
        //        LoadGallery();
        //    }
        //}

        //private void rdb_GemsMapping_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (rdb_GemsMapping.Checked == true)
        //    {
        //        LoadGallery();
        //    }
        //}

        private void cmbICD9Gallery_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cmbICD9Gallery.SelectedIndex >= 0)
            {
                LoadGallery();
            }
        }

        private void chkMapping_CheckedChanged(object sender, EventArgs e)
        {
            LoadGallery();
        }

        private void chkUnsusedOnly_CheckedChanged(object sender, EventArgs e)
        {
            LoadGallery();
        }

        private void rbCode_CheckedChanged(object sender, EventArgs e)
        {
            if (rbCode.Checked == true)
            {
                rbCode.Font = gloGlobal.clsgloFont.gFont_BOLD; //new Font("Tahoma", 9, FontStyle.Bold);
                rbDescription.Font = gloGlobal.clsgloFont.gFont; //new Font("Tahoma", 9, FontStyle.Regular);
                this.gloUCGallery.Sort = gloGallery.gloUC_TreeView.enumSortType.ByCode;
            }
            else
            {
                rbCode.Font = gloGlobal.clsgloFont.gFont; //new Font("Tahoma", 9, FontStyle.Regular);
                rbDescription.Font = gloGlobal.clsgloFont.gFont_BOLD; //new Font("Tahoma", 9, FontStyle.Bold);
                this.gloUCGallery.Sort = gloGallery.gloUC_TreeView.enumSortType.ByDescription;
            }
            LoadGallery();
        }

        private void cmbActivationDate_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (GalleryType == clsGallery.GalleryType.CPT)
            { rbCode_CheckedChanged(this, new EventArgs()); }            
        }

        
    }
}