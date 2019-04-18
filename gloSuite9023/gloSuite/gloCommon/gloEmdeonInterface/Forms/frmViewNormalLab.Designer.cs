namespace gloEmdeonInterface.Forms
{
    partial class frmViewNormalLab
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                try
                {
                    components.Dispose();
                    if (GloUC_AddRefreshDic1.DTLETTERDATEs != null)
                    {
                        try
                        {
                            gloGlobal.cEventHelper.RemoveAllEventHandlers(GloUC_AddRefreshDic1.DTLETTERDATEs);
                        }
                        catch
                        {
                        }
                        GloUC_AddRefreshDic1.DTLETTERDATEs.Dispose();
                        GloUC_AddRefreshDic1.DTLETTERDATEs = null;
                    }
                }
                catch
                {
                }

                try
                {
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(this);
                }
                catch
                {
                }
                //try
                //{
                //    if (PrintDialog1 != null)
                //    {
                //        PrintDialog1.Dispose();
                //        PrintDialog1 = null;
                //    }
                //}
                //catch
                //{
                //}
                try
                {
                    if (gloLabUC_Transaction1 != null)
                    {
                        gloGlobal.cEventHelper.RemoveAllEventHandlers(gloLabUC_Transaction1);
                        gloLabUC_Transaction1.Dispose();
                        gloLabUC_Transaction1 = null;
                    }
                }
                catch
                {
                }
                try
                {
                    if (gloUCLab_Transaction != null)
                    {
                        gloGlobal.cEventHelper.RemoveAllEventHandlers(gloUCLab_Transaction);
                        gloUCLab_Transaction.Dispose();
                        gloUCLab_Transaction = null;
                    }
                }
                catch
                {
                }
                try
                {
                    if (gloUCLab_TestDetail != null)
                    {
                        gloGlobal.cEventHelper.RemoveAllEventHandlers(gloUCLab_TestDetail);
                        gloUCLab_TestDetail.Dispose();
                        gloUCLab_TestDetail = null;
                    }
                }
                catch
                {
                }

                try
                {
                    if (GloUC_TemplateTreeControl_Orders != null)
                    {
                        gloGlobal.cEventHelper.RemoveAllEventHandlers(GloUC_TemplateTreeControl_Orders);
                        GloUC_TemplateTreeControl_Orders.Dispose();
                        GloUC_TemplateTreeControl_Orders = null;
                    }
                }
                catch
                {
                }
                try
                {
                    if (gloUCLab_TestDetail != null)
                    {
                        gloGlobal.cEventHelper.RemoveAllEventHandlers(gloUCLab_TestDetail);
                        gloUCLab_TestDetail.Dispose();
                        gloUCLab_TestDetail = null;
                    }
                }
                catch
                {
                }
                try
                {
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(this);
                }
                catch
                {
                }
                try
                {
                    if (gloUC_PatientStrip1 != null)
                    {
                        gloUC_PatientStrip1.Dispose();
                        gloUC_PatientStrip1 = null;
                    }
                }
                catch
                {
                }
                
            }
            dMdi = null;
            _MdiParent = null;
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmViewNormalLab));
            this.pnlToolStrip = new System.Windows.Forms.Panel();
            this.ts_LabMain = new gloGlobal.gloToolStripIgnoreFocus();
            this.tlbbtnNew = new System.Windows.Forms.ToolStripButton();
            this.tlbbtnSave = new System.Windows.Forms.ToolStripButton();
            this.tlbbtnClose = new System.Windows.Forms.ToolStripButton();
            this.tlbbtn_New = new System.Windows.Forms.ToolStripButton();
            this.tlbbtn_Save = new System.Windows.Forms.ToolStripButton();
            this.tlbbtn_Finish = new System.Windows.Forms.ToolStripButton();
            this.tlbbtn_Print = new System.Windows.Forms.ToolStripButton();
            this.tlbbtn_Fax = new System.Windows.Forms.ToolStripButton();
            this.tlbbtn_Previous = new System.Windows.Forms.ToolStripButton();
            this.tlbbtn_HL7 = new System.Windows.Forms.ToolStripButton();
            this.tlbbtn_Acknowledgment = new System.Windows.Forms.ToolStripButton();
            this.tlbbtn_VWAcknowledgment = new System.Windows.Forms.ToolStripButton();
            this.tlbbtn_PrvLabs = new System.Windows.Forms.ToolStripButton();
            this.tlbbtn_Close = new System.Windows.Forms.ToolStripButton();
            this.pnlLeft = new System.Windows.Forms.Panel();
            this.pnlList_Detail = new System.Windows.Forms.Panel();
            this.pnltrvList = new System.Windows.Forms.Panel();
            this.GloUC_trvTest = new gloUserControlLibrary.gloUC_TreeView();
            this.panel8 = new System.Windows.Forms.Panel();
            this.chkIncludeTestCode = new System.Windows.Forms.CheckBox();
            this.label39 = new System.Windows.Forms.Label();
            this.label40 = new System.Windows.Forms.Label();
            this.label41 = new System.Windows.Forms.Label();
            this.label42 = new System.Windows.Forms.Label();
            this.trvList = new System.Windows.Forms.TreeView();
            this.pnl_btnTests = new System.Windows.Forms.Panel();
            this.btnTests = new System.Windows.Forms.Button();
            this.Label15 = new System.Windows.Forms.Label();
            this.Label16 = new System.Windows.Forms.Label();
            this.Label17 = new System.Windows.Forms.Label();
            this.Label18 = new System.Windows.Forms.Label();
            this.pnl_btnRadiologyImaging = new System.Windows.Forms.Panel();
            this.label36 = new System.Windows.Forms.Label();
            this.btnRadiologyImaging = new System.Windows.Forms.Button();
            this.lblRadiologyImaging1 = new System.Windows.Forms.Label();
            this.lblRadiologyImaging2 = new System.Windows.Forms.Label();
            this.lblRadiologyImaging4 = new System.Windows.Forms.Label();
            this.lblRadiologyImaging3 = new System.Windows.Forms.Label();
            this.pnl_btnRefTest = new System.Windows.Forms.Panel();
            this.btnRefTest = new System.Windows.Forms.Button();
            this.Label1 = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label3 = new System.Windows.Forms.Label();
            this.Label4 = new System.Windows.Forms.Label();
            this.pnl_btnOthers = new System.Windows.Forms.Panel();
            this.label37 = new System.Windows.Forms.Label();
            this.btnOthers = new System.Windows.Forms.Button();
            this.lblOthers1 = new System.Windows.Forms.Label();
            this.lblOthers2 = new System.Windows.Forms.Label();
            this.lblOthers3 = new System.Windows.Forms.Label();
            this.lblOthers4 = new System.Windows.Forms.Label();
            this.pnl_btnGroups = new System.Windows.Forms.Panel();
            this.label35 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.btnGroups = new System.Windows.Forms.Button();
            this.pnlPlannedOrder = new System.Windows.Forms.Panel();
            this.label47 = new System.Windows.Forms.Label();
            this.label48 = new System.Windows.Forms.Label();
            this.label49 = new System.Windows.Forms.Label();
            this.label50 = new System.Windows.Forms.Label();
            this.btnPlannedOrder = new System.Windows.Forms.Button();
            this.pnlSearch = new System.Windows.Forms.Panel();
            this.txtListSearch = new System.Windows.Forms.TextBox();
            this.Label20 = new System.Windows.Forms.Label();
            this.Label21 = new System.Windows.Forms.Label();
            this.PictureBox1 = new System.Windows.Forms.PictureBox();
            this.label9 = new System.Windows.Forms.Label();
            this.Label12 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton11 = new System.Windows.Forms.ToolStripButton();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label27 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.gloLabUC_Transaction1 = new gloUserControlLibrary.gloLabUC_Transaction();
            this.label29 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.chkOutboundTransition = new System.Windows.Forms.CheckBox();
            this.chkFasting = new System.Windows.Forms.CheckBox();
            this.label31 = new System.Windows.Forms.Label();
            this.cmbOrderStatus = new System.Windows.Forms.ComboBox();
            this.chkOrderNotCPOE = new System.Windows.Forms.CheckBox();
            this.label23 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.gloUCLab_Transaction = new gloUserControlLibrary.gloUC_Transaction();
            this.gloUCLab_TestDetail = new gloUserControlLibrary.gloUC_LabTest();
            this.gloUCLab_OrderDetail = new gloUserControlLibrary.gloUC_LabOrderDetail();
            this.panel6 = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.label46 = new System.Windows.Forms.Label();
            this.label45 = new System.Windows.Forms.Label();
            this.label44 = new System.Windows.Forms.Label();
            this.label38 = new System.Windows.Forms.Label();
            this.label43 = new System.Windows.Forms.Label();
            this.cmbProvider = new System.Windows.Forms.ComboBox();
            this.ImageList1 = new System.Windows.Forms.ImageList(this.components);
            this.pnlOrder = new System.Windows.Forms.Panel();
            this.pnlWordTemplate = new System.Windows.Forms.Panel();
            this.Panel4 = new System.Windows.Forms.Panel();
            this.pnl_wdOrders = new System.Windows.Forms.Panel();
            this.wdOrders = new AxDSOFramer.AxFramerControl();
            this.label5 = new System.Windows.Forms.Label();
            this.Label24 = new System.Windows.Forms.Label();
            this.Label25 = new System.Windows.Forms.Label();
            this.Label26 = new System.Windows.Forms.Label();
            this.pnl_lblHeading = new System.Windows.Forms.Panel();
            this.Panel2 = new System.Windows.Forms.Panel();
            this.GloUC_AddRefreshDic1 = new gloUserControlLibrary.gloUC_AddRefreshDic();
            this.lblHeading = new System.Windows.Forms.Label();
            this.Label13 = new System.Windows.Forms.Label();
            this.Label22 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.Splitter4 = new System.Windows.Forms.Splitter();
            this.pnlGloUC_TemplateTreeControl = new System.Windows.Forms.Panel();
            this.GloUC_TemplateTreeControl_Orders = new gloUserControlLibrary.gloUC_TemplateTreeControl();
            this.pnl_cmdPastExam = new System.Windows.Forms.Panel();
            this.cmdPastExam = new System.Windows.Forms.Button();
            this.Label57 = new System.Windows.Forms.Label();
            this.Label58 = new System.Windows.Forms.Label();
            this.Label59 = new System.Windows.Forms.Label();
            this.Label60 = new System.Windows.Forms.Label();
            this.tmrDocProtect = new System.Windows.Forms.Timer(this.components);
            this.pnlToolStrip.SuspendLayout();
            this.ts_LabMain.SuspendLayout();
            this.pnlLeft.SuspendLayout();
            this.pnlList_Detail.SuspendLayout();
            this.pnltrvList.SuspendLayout();
            this.panel8.SuspendLayout();
            this.pnl_btnTests.SuspendLayout();
            this.pnl_btnRadiologyImaging.SuspendLayout();
            this.pnl_btnRefTest.SuspendLayout();
            this.pnl_btnOthers.SuspendLayout();
            this.pnl_btnGroups.SuspendLayout();
            this.pnlPlannedOrder.SuspendLayout();
            this.pnlSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel7.SuspendLayout();
            this.pnlOrder.SuspendLayout();
            this.pnlWordTemplate.SuspendLayout();
            this.Panel4.SuspendLayout();
            this.pnl_wdOrders.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.wdOrders)).BeginInit();
            this.pnl_lblHeading.SuspendLayout();
            this.Panel2.SuspendLayout();
            this.pnlGloUC_TemplateTreeControl.SuspendLayout();
            this.pnl_cmdPastExam.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlToolStrip
            // 
            this.pnlToolStrip.Controls.Add(this.ts_LabMain);
            this.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolStrip.Location = new System.Drawing.Point(0, 0);
            this.pnlToolStrip.Name = "pnlToolStrip";
            this.pnlToolStrip.Size = new System.Drawing.Size(1038, 54);
            this.pnlToolStrip.TabIndex = 1;
            // 
            // ts_LabMain
            // 
            this.ts_LabMain.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ts_LabMain.BackgroundImage")));
            this.ts_LabMain.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ts_LabMain.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.ts_LabMain.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.ts_LabMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tlbbtnNew,
            this.tlbbtnSave,
            this.tlbbtnClose});
            this.ts_LabMain.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.ts_LabMain.Location = new System.Drawing.Point(0, 0);
            this.ts_LabMain.Name = "ts_LabMain";
            this.ts_LabMain.Size = new System.Drawing.Size(1038, 53);
            this.ts_LabMain.TabIndex = 0;
            this.ts_LabMain.Text = "toolStrip1";
            this.ts_LabMain.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.ts_LabMain_ItemClicked);
            // 
            // tlbbtnNew
            // 
            this.tlbbtnNew.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlbbtnNew.Image = ((System.Drawing.Image)(resources.GetObject("tlbbtnNew.Image")));
            this.tlbbtnNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlbbtnNew.Name = "tlbbtnNew";
            this.tlbbtnNew.Size = new System.Drawing.Size(37, 50);
            this.tlbbtnNew.Text = "&New";
            this.tlbbtnNew.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlbbtnNew.Visible = false;
            // 
            // tlbbtnSave
            // 
            this.tlbbtnSave.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlbbtnSave.Image = ((System.Drawing.Image)(resources.GetObject("tlbbtnSave.Image")));
            this.tlbbtnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlbbtnSave.Name = "tlbbtnSave";
            this.tlbbtnSave.Size = new System.Drawing.Size(66, 50);
            this.tlbbtnSave.Text = "&Save&&Cls";
            this.tlbbtnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlbbtnSave.ToolTipText = "Save and Close";
            // 
            // tlbbtnClose
            // 
            this.tlbbtnClose.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlbbtnClose.Image = ((System.Drawing.Image)(resources.GetObject("tlbbtnClose.Image")));
            this.tlbbtnClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlbbtnClose.Name = "tlbbtnClose";
            this.tlbbtnClose.Size = new System.Drawing.Size(43, 50);
            this.tlbbtnClose.Text = "&Close";
            this.tlbbtnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlbbtnClose.ToolTipText = "Close";
            // 
            // tlbbtn_New
            // 
            this.tlbbtn_New.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlbbtn_New.Image = ((System.Drawing.Image)(resources.GetObject("tlbbtn_New.Image")));
            this.tlbbtn_New.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlbbtn_New.Name = "tlbbtn_New";
            this.tlbbtn_New.Size = new System.Drawing.Size(37, 50);
            this.tlbbtn_New.Tag = "New";
            this.tlbbtn_New.Text = "&New";
            this.tlbbtn_New.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlbbtn_New.ToolTipText = "New";
            // 
            // tlbbtn_Save
            // 
            this.tlbbtn_Save.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlbbtn_Save.Image = ((System.Drawing.Image)(resources.GetObject("tlbbtn_Save.Image")));
            this.tlbbtn_Save.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlbbtn_Save.Name = "tlbbtn_Save";
            this.tlbbtn_Save.Size = new System.Drawing.Size(66, 50);
            this.tlbbtn_Save.Tag = "Save and Close";
            this.tlbbtn_Save.Text = "&Save&&Cls";
            this.tlbbtn_Save.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlbbtn_Save.ToolTipText = "Save and Close";
            // 
            // tlbbtn_Finish
            // 
            this.tlbbtn_Finish.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlbbtn_Finish.Image = ((System.Drawing.Image)(resources.GetObject("tlbbtn_Finish.Image")));
            this.tlbbtn_Finish.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlbbtn_Finish.Name = "tlbbtn_Finish";
            this.tlbbtn_Finish.Size = new System.Drawing.Size(45, 50);
            this.tlbbtn_Finish.Tag = "Finish";
            this.tlbbtn_Finish.Text = "&Finish";
            this.tlbbtn_Finish.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlbbtn_Finish.ToolTipText = "Finish";
            // 
            // tlbbtn_Print
            // 
            this.tlbbtn_Print.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlbbtn_Print.Image = ((System.Drawing.Image)(resources.GetObject("tlbbtn_Print.Image")));
            this.tlbbtn_Print.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlbbtn_Print.Name = "tlbbtn_Print";
            this.tlbbtn_Print.Size = new System.Drawing.Size(41, 50);
            this.tlbbtn_Print.Tag = "&Print";
            this.tlbbtn_Print.Text = "&Print";
            this.tlbbtn_Print.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlbbtn_Print.ToolTipText = "Print";
            // 
            // tlbbtn_Fax
            // 
            this.tlbbtn_Fax.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlbbtn_Fax.Image = ((System.Drawing.Image)(resources.GetObject("tlbbtn_Fax.Image")));
            this.tlbbtn_Fax.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlbbtn_Fax.Name = "tlbbtn_Fax";
            this.tlbbtn_Fax.Size = new System.Drawing.Size(36, 50);
            this.tlbbtn_Fax.Tag = "Fax  ";
            this.tlbbtn_Fax.Text = "F&ax";
            this.tlbbtn_Fax.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlbbtn_Fax.ToolTipText = "Fax  ";
            // 
            // tlbbtn_Previous
            // 
            this.tlbbtn_Previous.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlbbtn_Previous.Image = ((System.Drawing.Image)(resources.GetObject("tlbbtn_Previous.Image")));
            this.tlbbtn_Previous.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlbbtn_Previous.Name = "tlbbtn_Previous";
            this.tlbbtn_Previous.Size = new System.Drawing.Size(50, 50);
            this.tlbbtn_Previous.Tag = "Show ";
            this.tlbbtn_Previous.Text = "&Show ";
            this.tlbbtn_Previous.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlbbtn_Previous.ToolTipText = "Show ";
            // 
            // tlbbtn_HL7
            // 
            this.tlbbtn_HL7.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlbbtn_HL7.Image = ((System.Drawing.Image)(resources.GetObject("tlbbtn_HL7.Image")));
            this.tlbbtn_HL7.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlbbtn_HL7.Name = "tlbbtn_HL7";
            this.tlbbtn_HL7.Size = new System.Drawing.Size(36, 50);
            this.tlbbtn_HL7.Tag = "HL7";
            this.tlbbtn_HL7.Text = "&HL7";
            this.tlbbtn_HL7.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlbbtn_HL7.ToolTipText = "HL7";
            // 
            // tlbbtn_Acknowledgment
            // 
            this.tlbbtn_Acknowledgment.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlbbtn_Acknowledgment.Image = ((System.Drawing.Image)(resources.GetObject("tlbbtn_Acknowledgment.Image")));
            this.tlbbtn_Acknowledgment.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlbbtn_Acknowledgment.Name = "tlbbtn_Acknowledgment";
            this.tlbbtn_Acknowledgment.Size = new System.Drawing.Size(44, 50);
            this.tlbbtn_Acknowledgment.Tag = "Acknowledgment";
            this.tlbbtn_Acknowledgment.Text = "&Ackw";
            this.tlbbtn_Acknowledgment.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlbbtn_Acknowledgment.ToolTipText = "Acknowledgment";
            // 
            // tlbbtn_VWAcknowledgment
            // 
            this.tlbbtn_VWAcknowledgment.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlbbtn_VWAcknowledgment.Image = ((System.Drawing.Image)(resources.GetObject("tlbbtn_VWAcknowledgment.Image")));
            this.tlbbtn_VWAcknowledgment.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlbbtn_VWAcknowledgment.Name = "tlbbtn_VWAcknowledgment";
            this.tlbbtn_VWAcknowledgment.Size = new System.Drawing.Size(77, 50);
            this.tlbbtn_VWAcknowledgment.Tag = "View Acknowledgment";
            this.tlbbtn_VWAcknowledgment.Text = "&View Ackw";
            this.tlbbtn_VWAcknowledgment.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlbbtn_VWAcknowledgment.ToolTipText = "View Acknowledgment";
            // 
            // tlbbtn_PrvLabs
            // 
            this.tlbbtn_PrvLabs.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlbbtn_PrvLabs.Image = ((System.Drawing.Image)(resources.GetObject("tlbbtn_PrvLabs.Image")));
            this.tlbbtn_PrvLabs.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlbbtn_PrvLabs.Name = "tlbbtn_PrvLabs";
            this.tlbbtn_PrvLabs.Size = new System.Drawing.Size(59, 50);
            this.tlbbtn_PrvLabs.Tag = "Previous Labs";
            this.tlbbtn_PrvLabs.Text = "&PrvLabs";
            this.tlbbtn_PrvLabs.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlbbtn_PrvLabs.ToolTipText = "Previous Labs";
            // 
            // tlbbtn_Close
            // 
            this.tlbbtn_Close.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlbbtn_Close.Image = ((System.Drawing.Image)(resources.GetObject("tlbbtn_Close.Image")));
            this.tlbbtn_Close.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlbbtn_Close.Name = "tlbbtn_Close";
            this.tlbbtn_Close.Size = new System.Drawing.Size(43, 50);
            this.tlbbtn_Close.Tag = "Close";
            this.tlbbtn_Close.Text = "&Close";
            this.tlbbtn_Close.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlbbtn_Close.ToolTipText = "Close";
            // 
            // pnlLeft
            // 
            this.pnlLeft.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnlLeft.Controls.Add(this.pnlList_Detail);
            this.pnlLeft.Controls.Add(this.pnlSearch);
            this.pnlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlLeft.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.pnlLeft.Location = new System.Drawing.Point(0, 0);
            this.pnlLeft.Name = "pnlLeft";
            this.pnlLeft.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.pnlLeft.Size = new System.Drawing.Size(235, 625);
            this.pnlLeft.TabIndex = 5;
            // 
            // pnlList_Detail
            // 
            this.pnlList_Detail.Controls.Add(this.pnltrvList);
            this.pnlList_Detail.Controls.Add(this.pnl_btnTests);
            this.pnlList_Detail.Controls.Add(this.pnl_btnRadiologyImaging);
            this.pnlList_Detail.Controls.Add(this.pnl_btnRefTest);
            this.pnlList_Detail.Controls.Add(this.pnl_btnOthers);
            this.pnlList_Detail.Controls.Add(this.pnl_btnGroups);
            this.pnlList_Detail.Controls.Add(this.pnlPlannedOrder);
            this.pnlList_Detail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlList_Detail.Location = new System.Drawing.Point(0, 3);
            this.pnlList_Detail.Name = "pnlList_Detail";
            this.pnlList_Detail.Size = new System.Drawing.Size(235, 622);
            this.pnlList_Detail.TabIndex = 8;
            // 
            // pnltrvList
            // 
            this.pnltrvList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnltrvList.Controls.Add(this.GloUC_trvTest);
            this.pnltrvList.Controls.Add(this.panel8);
            this.pnltrvList.Controls.Add(this.trvList);
            this.pnltrvList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnltrvList.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnltrvList.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.pnltrvList.Location = new System.Drawing.Point(0, 30);
            this.pnltrvList.Name = "pnltrvList";
            this.pnltrvList.Padding = new System.Windows.Forms.Padding(3, 0, 0, 3);
            this.pnltrvList.Size = new System.Drawing.Size(235, 442);
            this.pnltrvList.TabIndex = 5;
            // 
            // GloUC_trvTest
            // 
            this.GloUC_trvTest.AllergyClassID = null;
            this.GloUC_trvTest.BackColor = System.Drawing.Color.Transparent;
            this.GloUC_trvTest.CheckBoxes = false;
            this.GloUC_trvTest.CodeMember = null;
            this.GloUC_trvTest.ColonAsSeparator = false;
            this.GloUC_trvTest.Comment = null;
            this.GloUC_trvTest.ConceptID = null;
            this.GloUC_trvTest.CPT = null;
            this.GloUC_trvTest.CQMDESC = null;
            this.GloUC_trvTest.CQMID = null;
            this.GloUC_trvTest.DDIDMember = null;
            this.GloUC_trvTest.DescriptionMember = null;
            this.GloUC_trvTest.DisplayContextMenuStrip = null;
            this.GloUC_trvTest.DisplayType = gloUserControlLibrary.gloUC_TreeView.enumDisplayType.Descripation;
            this.GloUC_trvTest.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GloUC_trvTest.DrugFlag = ((short)(16));
            this.GloUC_trvTest.DrugFormMember = null;
            this.GloUC_trvTest.DrugQtyQualifierMember = null;
            this.GloUC_trvTest.DurationMember = null;
            this.GloUC_trvTest.EducationMappingSearchType = 1;
            this.GloUC_trvTest.FrequencyMember = null;
            this.GloUC_trvTest.HistoryType = null;
            this.GloUC_trvTest.ICD9 = null;
            this.GloUC_trvTest.ICDRevision = null;
            this.GloUC_trvTest.ImageIndex = -1;
            this.GloUC_trvTest.ImageList = null;
            this.GloUC_trvTest.ImageObject = null;
            this.GloUC_trvTest.Indicator = null;
            this.GloUC_trvTest.IsCPTSearch = false;
            this.GloUC_trvTest.IsDiagnosisSearch = false;
            this.GloUC_trvTest.IsDrug = false;
            this.GloUC_trvTest.IsNarcoticsMember = null;
            this.GloUC_trvTest.IsSearchForEducationMapping = false;
            this.GloUC_trvTest.IsSystemCategory = null;
            this.GloUC_trvTest.Location = new System.Drawing.Point(3, 30);
            this.GloUC_trvTest.MaximumNodes = 1100;
            this.GloUC_trvTest.mpidmember = null;
            this.GloUC_trvTest.Name = "GloUC_trvTest";
            this.GloUC_trvTest.NDCCodeMember = null;
            this.GloUC_trvTest.ParentImageIndex = 0;
            this.GloUC_trvTest.ParentMember = null;
            this.GloUC_trvTest.RouteMember = null;
            this.GloUC_trvTest.RowOrderMember = null;
            this.GloUC_trvTest.RxNormCode = null;
            this.GloUC_trvTest.Search = gloUserControlLibrary.gloUC_TreeView.enumSearchType.Instring;
            this.GloUC_trvTest.SearchBox = true;
            this.GloUC_trvTest.SearchText = null;
            this.GloUC_trvTest.SelectedImageIndex = -1;
            this.GloUC_trvTest.SelectedNode = null;
            this.GloUC_trvTest.SelectedNodeIDs = ((System.Collections.ArrayList)(resources.GetObject("GloUC_trvTest.SelectedNodeIDs")));
            this.GloUC_trvTest.SelectedParentImageIndex = 0;
            this.GloUC_trvTest.Size = new System.Drawing.Size(232, 409);
            this.GloUC_trvTest.SmartTreatmentId = null;
            this.GloUC_trvTest.Sort = gloUserControlLibrary.gloUC_TreeView.enumSortType.ByDescription;
            this.GloUC_trvTest.TabIndex = 42;
            this.GloUC_trvTest.Tag = null;
            this.GloUC_trvTest.UnitMember = null;
            this.GloUC_trvTest.ValueMember = null;
            this.GloUC_trvTest.NodeMouseDoubleClick += new gloUserControlLibrary.gloUC_TreeView.NodeMouseDoubleClickEventHandler(this.GloUC_trvTest_NodeMouseDoubleClick);
            this.GloUC_trvTest.KeyPress += new System.EventHandler<System.Windows.Forms.KeyPressEventArgs>(this.GloUC_trvTest_KeyPress);
            this.GloUC_trvTest.MouseDown += new gloUserControlLibrary.gloUC_TreeView.MouseDownEventHandler(this.GloUC_trvTest_MouseDown);
            this.GloUC_trvTest.MouseUp += new gloUserControlLibrary.gloUC_TreeView.MouseUpEventHandler(this.GloUC_trvTest_MouseUp);
            // 
            // panel8
            // 
            this.panel8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.panel8.Controls.Add(this.chkIncludeTestCode);
            this.panel8.Controls.Add(this.label39);
            this.panel8.Controls.Add(this.label40);
            this.panel8.Controls.Add(this.label41);
            this.panel8.Controls.Add(this.label42);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel8.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.panel8.Location = new System.Drawing.Point(3, 0);
            this.panel8.Name = "panel8";
            this.panel8.Padding = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.panel8.Size = new System.Drawing.Size(232, 30);
            this.panel8.TabIndex = 43;
            // 
            // chkIncludeTestCode
            // 
            this.chkIncludeTestCode.AutoSize = true;
            this.chkIncludeTestCode.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkIncludeTestCode.Location = new System.Drawing.Point(8, 6);
            this.chkIncludeTestCode.Name = "chkIncludeTestCode";
            this.chkIncludeTestCode.Size = new System.Drawing.Size(127, 17);
            this.chkIncludeTestCode.TabIndex = 19;
            this.chkIncludeTestCode.Text = "Include Test Code";
            this.chkIncludeTestCode.UseVisualStyleBackColor = true;
            this.chkIncludeTestCode.CheckedChanged += new System.EventHandler(this.chkIncludeTestCode_CheckedChanged);
            // 
            // label39
            // 
            this.label39.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label39.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label39.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label39.Location = new System.Drawing.Point(1, 26);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(230, 1);
            this.label39.TabIndex = 18;
            this.label39.Text = "label2";
            // 
            // label40
            // 
            this.label40.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label40.Dock = System.Windows.Forms.DockStyle.Left;
            this.label40.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label40.Location = new System.Drawing.Point(0, 1);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(1, 26);
            this.label40.TabIndex = 17;
            this.label40.Text = "label4";
            // 
            // label41
            // 
            this.label41.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label41.Dock = System.Windows.Forms.DockStyle.Right;
            this.label41.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label41.Location = new System.Drawing.Point(231, 1);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(1, 26);
            this.label41.TabIndex = 16;
            this.label41.Text = "label3";
            // 
            // label42
            // 
            this.label42.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label42.Dock = System.Windows.Forms.DockStyle.Top;
            this.label42.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label42.Location = new System.Drawing.Point(0, 0);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(232, 1);
            this.label42.TabIndex = 15;
            this.label42.Text = "label1";
            // 
            // trvList
            // 
            this.trvList.BackColor = System.Drawing.Color.White;
            this.trvList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.trvList.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.trvList.ForeColor = System.Drawing.Color.Black;
            this.trvList.HideSelection = false;
            this.trvList.Indent = 20;
            this.trvList.ItemHeight = 20;
            this.trvList.Location = new System.Drawing.Point(12, 205);
            this.trvList.Name = "trvList";
            this.trvList.ShowNodeToolTips = true;
            this.trvList.ShowPlusMinus = false;
            this.trvList.ShowRootLines = false;
            this.trvList.Size = new System.Drawing.Size(17, 300);
            this.trvList.TabIndex = 3;
            this.trvList.Visible = false;
            // 
            // pnl_btnTests
            // 
            this.pnl_btnTests.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnl_btnTests.Controls.Add(this.btnTests);
            this.pnl_btnTests.Controls.Add(this.Label15);
            this.pnl_btnTests.Controls.Add(this.Label16);
            this.pnl_btnTests.Controls.Add(this.Label17);
            this.pnl_btnTests.Controls.Add(this.Label18);
            this.pnl_btnTests.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_btnTests.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnl_btnTests.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.pnl_btnTests.Location = new System.Drawing.Point(0, 0);
            this.pnl_btnTests.Name = "pnl_btnTests";
            this.pnl_btnTests.Padding = new System.Windows.Forms.Padding(3, 0, 0, 3);
            this.pnl_btnTests.Size = new System.Drawing.Size(235, 30);
            this.pnl_btnTests.TabIndex = 4;
            // 
            // btnTests
            // 
            this.btnTests.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(207)))), ((int)(((byte)(85)))));
            this.btnTests.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnTests.BackgroundImage")));
            this.btnTests.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnTests.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnTests.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnTests.FlatAppearance.BorderSize = 0;
            this.btnTests.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTests.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTests.Location = new System.Drawing.Point(4, 1);
            this.btnTests.Name = "btnTests";
            this.btnTests.Size = new System.Drawing.Size(230, 25);
            this.btnTests.TabIndex = 0;
            this.btnTests.Text = "Lab Tests";
            this.btnTests.UseVisualStyleBackColor = false;
            this.btnTests.Click += new System.EventHandler(this.btnTests_Click);
            this.btnTests.MouseEnter += new System.EventHandler(this.btnTests_MouseEnter);
            this.btnTests.MouseLeave += new System.EventHandler(this.btnTests_MouseLeave);
            // 
            // Label15
            // 
            this.Label15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label15.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Label15.Font = new System.Drawing.Font("Tahoma", 9F);
            this.Label15.Location = new System.Drawing.Point(4, 26);
            this.Label15.Name = "Label15";
            this.Label15.Size = new System.Drawing.Size(230, 1);
            this.Label15.TabIndex = 18;
            this.Label15.Text = "label2";
            // 
            // Label16
            // 
            this.Label16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label16.Dock = System.Windows.Forms.DockStyle.Left;
            this.Label16.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label16.Location = new System.Drawing.Point(3, 1);
            this.Label16.Name = "Label16";
            this.Label16.Size = new System.Drawing.Size(1, 26);
            this.Label16.TabIndex = 17;
            this.Label16.Text = "label4";
            // 
            // Label17
            // 
            this.Label17.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label17.Dock = System.Windows.Forms.DockStyle.Right;
            this.Label17.Font = new System.Drawing.Font("Tahoma", 9F);
            this.Label17.Location = new System.Drawing.Point(234, 1);
            this.Label17.Name = "Label17";
            this.Label17.Size = new System.Drawing.Size(1, 26);
            this.Label17.TabIndex = 16;
            this.Label17.Text = "label3";
            // 
            // Label18
            // 
            this.Label18.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label18.Dock = System.Windows.Forms.DockStyle.Top;
            this.Label18.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label18.Location = new System.Drawing.Point(3, 0);
            this.Label18.Name = "Label18";
            this.Label18.Size = new System.Drawing.Size(232, 1);
            this.Label18.TabIndex = 15;
            this.Label18.Text = "label1";
            // 
            // pnl_btnRadiologyImaging
            // 
            this.pnl_btnRadiologyImaging.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnl_btnRadiologyImaging.Controls.Add(this.label36);
            this.pnl_btnRadiologyImaging.Controls.Add(this.btnRadiologyImaging);
            this.pnl_btnRadiologyImaging.Controls.Add(this.lblRadiologyImaging1);
            this.pnl_btnRadiologyImaging.Controls.Add(this.lblRadiologyImaging2);
            this.pnl_btnRadiologyImaging.Controls.Add(this.lblRadiologyImaging4);
            this.pnl_btnRadiologyImaging.Controls.Add(this.lblRadiologyImaging3);
            this.pnl_btnRadiologyImaging.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnl_btnRadiologyImaging.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnl_btnRadiologyImaging.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.pnl_btnRadiologyImaging.Location = new System.Drawing.Point(0, 472);
            this.pnl_btnRadiologyImaging.Name = "pnl_btnRadiologyImaging";
            this.pnl_btnRadiologyImaging.Padding = new System.Windows.Forms.Padding(3, 0, 0, 3);
            this.pnl_btnRadiologyImaging.Size = new System.Drawing.Size(235, 30);
            this.pnl_btnRadiologyImaging.TabIndex = 4;
            // 
            // label36
            // 
            this.label36.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label36.Dock = System.Windows.Forms.DockStyle.Left;
            this.label36.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label36.Location = new System.Drawing.Point(3, 1);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(1, 25);
            this.label36.TabIndex = 19;
            this.label36.Text = "label4";
            // 
            // btnRadiologyImaging
            // 
            this.btnRadiologyImaging.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.btnRadiologyImaging.BackgroundImage = global::gloEmdeonInterface.Properties.Resources.Img_LongButton;
            this.btnRadiologyImaging.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnRadiologyImaging.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnRadiologyImaging.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnRadiologyImaging.FlatAppearance.BorderSize = 0;
            this.btnRadiologyImaging.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRadiologyImaging.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRadiologyImaging.Location = new System.Drawing.Point(3, 1);
            this.btnRadiologyImaging.Name = "btnRadiologyImaging";
            this.btnRadiologyImaging.Size = new System.Drawing.Size(231, 25);
            this.btnRadiologyImaging.TabIndex = 0;
            this.btnRadiologyImaging.Text = "Radiology/Imaging";
            this.btnRadiologyImaging.UseVisualStyleBackColor = false;
            this.btnRadiologyImaging.Click += new System.EventHandler(this.btnRadiologyImaging_Click);
            this.btnRadiologyImaging.MouseEnter += new System.EventHandler(this.btnRadiologyImaging_MouseEnter);
            this.btnRadiologyImaging.MouseLeave += new System.EventHandler(this.btnRadiologyImaging_MouseLeave);
            // 
            // lblRadiologyImaging1
            // 
            this.lblRadiologyImaging1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblRadiologyImaging1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblRadiologyImaging1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRadiologyImaging1.Location = new System.Drawing.Point(3, 26);
            this.lblRadiologyImaging1.Name = "lblRadiologyImaging1";
            this.lblRadiologyImaging1.Size = new System.Drawing.Size(231, 1);
            this.lblRadiologyImaging1.TabIndex = 15;
            this.lblRadiologyImaging1.Text = "lblRadiologyImaging1";
            // 
            // lblRadiologyImaging2
            // 
            this.lblRadiologyImaging2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblRadiologyImaging2.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblRadiologyImaging2.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lblRadiologyImaging2.Location = new System.Drawing.Point(3, 1);
            this.lblRadiologyImaging2.Name = "lblRadiologyImaging2";
            this.lblRadiologyImaging2.Size = new System.Drawing.Size(0, 26);
            this.lblRadiologyImaging2.TabIndex = 18;
            this.lblRadiologyImaging2.Text = "lblRadiologyImaging2";
            // 
            // lblRadiologyImaging4
            // 
            this.lblRadiologyImaging4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblRadiologyImaging4.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblRadiologyImaging4.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lblRadiologyImaging4.Location = new System.Drawing.Point(234, 1);
            this.lblRadiologyImaging4.Name = "lblRadiologyImaging4";
            this.lblRadiologyImaging4.Size = new System.Drawing.Size(1, 26);
            this.lblRadiologyImaging4.TabIndex = 16;
            this.lblRadiologyImaging4.Text = "lblRadiologyImaging4";
            // 
            // lblRadiologyImaging3
            // 
            this.lblRadiologyImaging3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblRadiologyImaging3.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblRadiologyImaging3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRadiologyImaging3.Location = new System.Drawing.Point(3, 0);
            this.lblRadiologyImaging3.Name = "lblRadiologyImaging3";
            this.lblRadiologyImaging3.Size = new System.Drawing.Size(232, 1);
            this.lblRadiologyImaging3.TabIndex = 17;
            this.lblRadiologyImaging3.Text = "lblRadiologyImaging3";
            // 
            // pnl_btnRefTest
            // 
            this.pnl_btnRefTest.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnl_btnRefTest.Controls.Add(this.btnRefTest);
            this.pnl_btnRefTest.Controls.Add(this.Label1);
            this.pnl_btnRefTest.Controls.Add(this.Label2);
            this.pnl_btnRefTest.Controls.Add(this.Label3);
            this.pnl_btnRefTest.Controls.Add(this.Label4);
            this.pnl_btnRefTest.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnl_btnRefTest.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnl_btnRefTest.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.pnl_btnRefTest.Location = new System.Drawing.Point(0, 502);
            this.pnl_btnRefTest.Name = "pnl_btnRefTest";
            this.pnl_btnRefTest.Padding = new System.Windows.Forms.Padding(3, 0, 0, 3);
            this.pnl_btnRefTest.Size = new System.Drawing.Size(235, 30);
            this.pnl_btnRefTest.TabIndex = 4;
            // 
            // btnRefTest
            // 
            this.btnRefTest.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.btnRefTest.BackgroundImage = global::gloEmdeonInterface.Properties.Resources.Img_LongButton;
            this.btnRefTest.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnRefTest.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnRefTest.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnRefTest.FlatAppearance.BorderSize = 0;
            this.btnRefTest.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefTest.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefTest.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnRefTest.Location = new System.Drawing.Point(4, 1);
            this.btnRefTest.Name = "btnRefTest";
            this.btnRefTest.Size = new System.Drawing.Size(230, 25);
            this.btnRefTest.TabIndex = 2;
            this.btnRefTest.Text = "Referrals";
            this.btnRefTest.UseVisualStyleBackColor = false;
            this.btnRefTest.Click += new System.EventHandler(this.btnRefTest_Click);
            this.btnRefTest.MouseEnter += new System.EventHandler(this.btnRefTest_MouseEnter);
            this.btnRefTest.MouseLeave += new System.EventHandler(this.btnRefTest_MouseLeave);
            // 
            // Label1
            // 
            this.Label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Label1.Font = new System.Drawing.Font("Tahoma", 9F);
            this.Label1.Location = new System.Drawing.Point(4, 26);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(230, 1);
            this.Label1.TabIndex = 18;
            this.Label1.Text = "label2";
            // 
            // Label2
            // 
            this.Label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label2.Dock = System.Windows.Forms.DockStyle.Left;
            this.Label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label2.Location = new System.Drawing.Point(3, 1);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(1, 26);
            this.Label2.TabIndex = 17;
            this.Label2.Text = "label4";
            // 
            // Label3
            // 
            this.Label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label3.Dock = System.Windows.Forms.DockStyle.Right;
            this.Label3.Font = new System.Drawing.Font("Tahoma", 9F);
            this.Label3.Location = new System.Drawing.Point(234, 1);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(1, 26);
            this.Label3.TabIndex = 16;
            this.Label3.Text = "label3";
            // 
            // Label4
            // 
            this.Label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label4.Dock = System.Windows.Forms.DockStyle.Top;
            this.Label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label4.Location = new System.Drawing.Point(3, 0);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(232, 1);
            this.Label4.TabIndex = 15;
            this.Label4.Text = "label1";
            // 
            // pnl_btnOthers
            // 
            this.pnl_btnOthers.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnl_btnOthers.Controls.Add(this.label37);
            this.pnl_btnOthers.Controls.Add(this.btnOthers);
            this.pnl_btnOthers.Controls.Add(this.lblOthers1);
            this.pnl_btnOthers.Controls.Add(this.lblOthers2);
            this.pnl_btnOthers.Controls.Add(this.lblOthers3);
            this.pnl_btnOthers.Controls.Add(this.lblOthers4);
            this.pnl_btnOthers.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnl_btnOthers.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnl_btnOthers.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.pnl_btnOthers.Location = new System.Drawing.Point(0, 532);
            this.pnl_btnOthers.Name = "pnl_btnOthers";
            this.pnl_btnOthers.Padding = new System.Windows.Forms.Padding(3, 0, 0, 3);
            this.pnl_btnOthers.Size = new System.Drawing.Size(235, 30);
            this.pnl_btnOthers.TabIndex = 6;
            // 
            // label37
            // 
            this.label37.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label37.Dock = System.Windows.Forms.DockStyle.Left;
            this.label37.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label37.Location = new System.Drawing.Point(3, 1);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(1, 25);
            this.label37.TabIndex = 19;
            this.label37.Text = "label4";
            // 
            // btnOthers
            // 
            this.btnOthers.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.btnOthers.BackgroundImage = global::gloEmdeonInterface.Properties.Resources.Img_LongButton;
            this.btnOthers.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnOthers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnOthers.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnOthers.FlatAppearance.BorderSize = 0;
            this.btnOthers.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOthers.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOthers.Location = new System.Drawing.Point(3, 1);
            this.btnOthers.Name = "btnOthers";
            this.btnOthers.Size = new System.Drawing.Size(231, 25);
            this.btnOthers.TabIndex = 0;
            this.btnOthers.Text = "Other";
            this.btnOthers.UseVisualStyleBackColor = false;
            this.btnOthers.Click += new System.EventHandler(this.btnOthers_Click);
            this.btnOthers.MouseEnter += new System.EventHandler(this.btnOthers_MouseEnter);
            this.btnOthers.MouseLeave += new System.EventHandler(this.btnOthers_MouseLeave);
            // 
            // lblOthers1
            // 
            this.lblOthers1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblOthers1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblOthers1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOthers1.Location = new System.Drawing.Point(3, 26);
            this.lblOthers1.Name = "lblOthers1";
            this.lblOthers1.Size = new System.Drawing.Size(231, 1);
            this.lblOthers1.TabIndex = 15;
            this.lblOthers1.Text = "label36";
            // 
            // lblOthers2
            // 
            this.lblOthers2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblOthers2.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblOthers2.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lblOthers2.Location = new System.Drawing.Point(3, 1);
            this.lblOthers2.Name = "lblOthers2";
            this.lblOthers2.Size = new System.Drawing.Size(0, 26);
            this.lblOthers2.TabIndex = 18;
            this.lblOthers2.Text = "label37";
            // 
            // lblOthers3
            // 
            this.lblOthers3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblOthers3.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblOthers3.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lblOthers3.Location = new System.Drawing.Point(234, 1);
            this.lblOthers3.Name = "lblOthers3";
            this.lblOthers3.Size = new System.Drawing.Size(1, 26);
            this.lblOthers3.TabIndex = 16;
            this.lblOthers3.Text = "label38";
            // 
            // lblOthers4
            // 
            this.lblOthers4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblOthers4.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblOthers4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOthers4.Location = new System.Drawing.Point(3, 0);
            this.lblOthers4.Name = "lblOthers4";
            this.lblOthers4.Size = new System.Drawing.Size(232, 1);
            this.lblOthers4.TabIndex = 17;
            this.lblOthers4.Text = "label39";
            // 
            // pnl_btnGroups
            // 
            this.pnl_btnGroups.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnl_btnGroups.Controls.Add(this.label35);
            this.pnl_btnGroups.Controls.Add(this.label34);
            this.pnl_btnGroups.Controls.Add(this.label33);
            this.pnl_btnGroups.Controls.Add(this.label32);
            this.pnl_btnGroups.Controls.Add(this.btnGroups);
            this.pnl_btnGroups.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnl_btnGroups.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnl_btnGroups.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.pnl_btnGroups.Location = new System.Drawing.Point(0, 562);
            this.pnl_btnGroups.Name = "pnl_btnGroups";
            this.pnl_btnGroups.Padding = new System.Windows.Forms.Padding(3, 0, 0, 3);
            this.pnl_btnGroups.Size = new System.Drawing.Size(235, 30);
            this.pnl_btnGroups.TabIndex = 4;
            // 
            // label35
            // 
            this.label35.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label35.Dock = System.Windows.Forms.DockStyle.Top;
            this.label35.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label35.Location = new System.Drawing.Point(4, 0);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(230, 1);
            this.label35.TabIndex = 21;
            this.label35.Text = "label2";
            // 
            // label34
            // 
            this.label34.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label34.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label34.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label34.Location = new System.Drawing.Point(4, 26);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(230, 1);
            this.label34.TabIndex = 20;
            this.label34.Text = "label2";
            // 
            // label33
            // 
            this.label33.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label33.Dock = System.Windows.Forms.DockStyle.Right;
            this.label33.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label33.Location = new System.Drawing.Point(234, 0);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(1, 27);
            this.label33.TabIndex = 19;
            this.label33.Text = "label4";
            // 
            // label32
            // 
            this.label32.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label32.Dock = System.Windows.Forms.DockStyle.Left;
            this.label32.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label32.Location = new System.Drawing.Point(3, 0);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(1, 27);
            this.label32.TabIndex = 18;
            this.label32.Text = "label4";
            // 
            // btnGroups
            // 
            this.btnGroups.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.btnGroups.BackgroundImage = global::gloEmdeonInterface.Properties.Resources.Img_LongButton;
            this.btnGroups.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnGroups.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnGroups.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnGroups.FlatAppearance.BorderSize = 0;
            this.btnGroups.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGroups.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGroups.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnGroups.Location = new System.Drawing.Point(3, 0);
            this.btnGroups.Name = "btnGroups";
            this.btnGroups.Size = new System.Drawing.Size(232, 27);
            this.btnGroups.TabIndex = 2;
            this.btnGroups.Text = "Groups";
            this.btnGroups.UseVisualStyleBackColor = false;
            this.btnGroups.Click += new System.EventHandler(this.btnGroups_Click);
            this.btnGroups.MouseEnter += new System.EventHandler(this.btnGroups_MouseEnter);
            this.btnGroups.MouseLeave += new System.EventHandler(this.btnGroups_MouseLeave);
            // 
            // pnlPlannedOrder
            // 
            this.pnlPlannedOrder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnlPlannedOrder.Controls.Add(this.label47);
            this.pnlPlannedOrder.Controls.Add(this.label48);
            this.pnlPlannedOrder.Controls.Add(this.label49);
            this.pnlPlannedOrder.Controls.Add(this.label50);
            this.pnlPlannedOrder.Controls.Add(this.btnPlannedOrder);
            this.pnlPlannedOrder.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlPlannedOrder.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlPlannedOrder.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.pnlPlannedOrder.Location = new System.Drawing.Point(0, 592);
            this.pnlPlannedOrder.Name = "pnlPlannedOrder";
            this.pnlPlannedOrder.Padding = new System.Windows.Forms.Padding(3, 0, 0, 3);
            this.pnlPlannedOrder.Size = new System.Drawing.Size(235, 30);
            this.pnlPlannedOrder.TabIndex = 7;
            this.pnlPlannedOrder.Visible = false;
            // 
            // label47
            // 
            this.label47.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label47.Dock = System.Windows.Forms.DockStyle.Top;
            this.label47.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label47.Location = new System.Drawing.Point(4, 0);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(230, 1);
            this.label47.TabIndex = 21;
            this.label47.Text = "label2";
            // 
            // label48
            // 
            this.label48.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label48.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label48.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label48.Location = new System.Drawing.Point(4, 26);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(230, 1);
            this.label48.TabIndex = 20;
            this.label48.Text = "label2";
            // 
            // label49
            // 
            this.label49.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label49.Dock = System.Windows.Forms.DockStyle.Right;
            this.label49.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label49.Location = new System.Drawing.Point(234, 0);
            this.label49.Name = "label49";
            this.label49.Size = new System.Drawing.Size(1, 27);
            this.label49.TabIndex = 19;
            this.label49.Text = "label4";
            // 
            // label50
            // 
            this.label50.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label50.Dock = System.Windows.Forms.DockStyle.Left;
            this.label50.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label50.Location = new System.Drawing.Point(3, 0);
            this.label50.Name = "label50";
            this.label50.Size = new System.Drawing.Size(1, 27);
            this.label50.TabIndex = 18;
            this.label50.Text = "label4";
            // 
            // btnPlannedOrder
            // 
            this.btnPlannedOrder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.btnPlannedOrder.BackgroundImage = global::gloEmdeonInterface.Properties.Resources.Img_LongButton;
            this.btnPlannedOrder.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPlannedOrder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnPlannedOrder.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnPlannedOrder.FlatAppearance.BorderSize = 0;
            this.btnPlannedOrder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPlannedOrder.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPlannedOrder.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnPlannedOrder.Location = new System.Drawing.Point(3, 0);
            this.btnPlannedOrder.Name = "btnPlannedOrder";
            this.btnPlannedOrder.Size = new System.Drawing.Size(232, 27);
            this.btnPlannedOrder.TabIndex = 2;
            this.btnPlannedOrder.Text = "Planned Order";
            this.btnPlannedOrder.UseVisualStyleBackColor = false;
            this.btnPlannedOrder.Click += new System.EventHandler(this.btnPlannedOrder_Click);
            // 
            // pnlSearch
            // 
            this.pnlSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnlSearch.Controls.Add(this.txtListSearch);
            this.pnlSearch.Controls.Add(this.Label20);
            this.pnlSearch.Controls.Add(this.Label21);
            this.pnlSearch.Controls.Add(this.PictureBox1);
            this.pnlSearch.Controls.Add(this.label9);
            this.pnlSearch.Controls.Add(this.Label12);
            this.pnlSearch.Controls.Add(this.label10);
            this.pnlSearch.Controls.Add(this.label11);
            this.pnlSearch.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlSearch.ForeColor = System.Drawing.Color.Black;
            this.pnlSearch.Location = new System.Drawing.Point(0, 0);
            this.pnlSearch.Name = "pnlSearch";
            this.pnlSearch.Padding = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.pnlSearch.Size = new System.Drawing.Size(235, 29);
            this.pnlSearch.TabIndex = 16;
            this.pnlSearch.Visible = false;
            // 
            // txtListSearch
            // 
            this.txtListSearch.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtListSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtListSearch.ForeColor = System.Drawing.Color.Black;
            this.txtListSearch.Location = new System.Drawing.Point(32, 8);
            this.txtListSearch.Name = "txtListSearch";
            this.txtListSearch.Size = new System.Drawing.Size(202, 15);
            this.txtListSearch.TabIndex = 0;
            // 
            // Label20
            // 
            this.Label20.BackColor = System.Drawing.Color.White;
            this.Label20.Dock = System.Windows.Forms.DockStyle.Top;
            this.Label20.Location = new System.Drawing.Point(32, 4);
            this.Label20.Name = "Label20";
            this.Label20.Size = new System.Drawing.Size(202, 4);
            this.Label20.TabIndex = 37;
            // 
            // Label21
            // 
            this.Label21.BackColor = System.Drawing.Color.White;
            this.Label21.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Label21.Location = new System.Drawing.Point(32, 23);
            this.Label21.Name = "Label21";
            this.Label21.Size = new System.Drawing.Size(202, 2);
            this.Label21.TabIndex = 38;
            // 
            // PictureBox1
            // 
            this.PictureBox1.BackColor = System.Drawing.Color.White;
            this.PictureBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.PictureBox1.Location = new System.Drawing.Point(4, 4);
            this.PictureBox1.Name = "PictureBox1";
            this.PictureBox1.Size = new System.Drawing.Size(28, 21);
            this.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox1.TabIndex = 9;
            this.PictureBox1.TabStop = false;
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label9.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label9.Location = new System.Drawing.Point(4, 25);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(230, 1);
            this.label9.TabIndex = 35;
            this.label9.Text = "label1";
            // 
            // Label12
            // 
            this.Label12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label12.Dock = System.Windows.Forms.DockStyle.Top;
            this.Label12.Location = new System.Drawing.Point(4, 3);
            this.Label12.Name = "Label12";
            this.Label12.Size = new System.Drawing.Size(230, 1);
            this.Label12.TabIndex = 36;
            this.Label12.Text = "label1";
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label10.Dock = System.Windows.Forms.DockStyle.Left;
            this.label10.Location = new System.Drawing.Point(3, 3);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(1, 23);
            this.label10.TabIndex = 39;
            this.label10.Text = "label4";
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label11.Dock = System.Windows.Forms.DockStyle.Right;
            this.label11.Location = new System.Drawing.Point(234, 3);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(1, 23);
            this.label11.TabIndex = 40;
            this.label11.Text = "label4";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(37, 50);
            this.toolStripButton1.Tag = "New";
            this.toolStripButton1.Text = "&New";
            this.toolStripButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButton1.ToolTipText = "New";
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(66, 50);
            this.toolStripButton2.Tag = "Save and Close";
            this.toolStripButton2.Text = "&Save&&Cls";
            this.toolStripButton2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButton2.ToolTipText = "Save and Close";
            // 
            // toolStripButton11
            // 
            this.toolStripButton11.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripButton11.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton11.Image")));
            this.toolStripButton11.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton11.Name = "toolStripButton11";
            this.toolStripButton11.Size = new System.Drawing.Size(43, 50);
            this.toolStripButton11.Tag = "Close";
            this.toolStripButton11.Text = "&Close";
            this.toolStripButton11.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButton11.ToolTipText = "Close";
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(235, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(4, 625);
            this.splitter1.TabIndex = 6;
            this.splitter1.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel5);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.gloUCLab_Transaction);
            this.panel1.Controls.Add(this.gloUCLab_TestDetail);
            this.panel1.Controls.Add(this.gloUCLab_OrderDetail);
            this.panel1.Controls.Add(this.panel6);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(239, 0);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.panel1.Size = new System.Drawing.Size(799, 625);
            this.panel1.TabIndex = 7;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.label27);
            this.panel5.Controls.Add(this.label28);
            this.panel5.Controls.Add(this.gloLabUC_Transaction1);
            this.panel5.Controls.Add(this.label29);
            this.panel5.Controls.Add(this.label30);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(0, 210);
            this.panel5.Name = "panel5";
            this.panel5.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.panel5.Size = new System.Drawing.Size(796, 331);
            this.panel5.TabIndex = 35;
            // 
            // label27
            // 
            this.label27.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label27.Dock = System.Windows.Forms.DockStyle.Right;
            this.label27.Location = new System.Drawing.Point(795, 4);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(1, 326);
            this.label27.TabIndex = 3;
            this.label27.Text = "label27";
            // 
            // label28
            // 
            this.label28.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label28.Dock = System.Windows.Forms.DockStyle.Left;
            this.label28.Location = new System.Drawing.Point(0, 4);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(1, 326);
            this.label28.TabIndex = 2;
            this.label28.Text = "label28";
            // 
            // gloLabUC_Transaction1
            // 
            this.gloLabUC_Transaction1.AutoSize = true;
            this.gloLabUC_Transaction1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.gloLabUC_Transaction1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gloLabUC_Transaction1.dtSelectedFromDt = new System.DateTime(((long)(0)));
            this.gloLabUC_Transaction1.dtSelectedToDt = new System.DateTime(((long)(0)));
            this.gloLabUC_Transaction1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gloLabUC_Transaction1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.gloLabUC_Transaction1.IsCQMConceptDisplay = false;
            this.gloLabUC_Transaction1.IsLoadLastTransaction = false;
            this.gloLabUC_Transaction1.IsOrderLocked = false;
            this.gloLabUC_Transaction1.LabModified = false;
            this.gloLabUC_Transaction1.LabResultId = ((long)(0));
            this.gloLabUC_Transaction1.LabResultName = null;
            this.gloLabUC_Transaction1.LabTestId = ((long)(0));
            this.gloLabUC_Transaction1.LabTestName = null;
            this.gloLabUC_Transaction1.Location = new System.Drawing.Point(0, 4);
            this.gloLabUC_Transaction1.Name = "gloLabUC_Transaction1";
            this.gloLabUC_Transaction1.ParentControl = "";
            this.gloLabUC_Transaction1.PatientID = ((long)(0));
            this.gloLabUC_Transaction1.PreferredLab = null;
            this.gloLabUC_Transaction1.PreferredLabID = ((long)(0));
            this.gloLabUC_Transaction1.ProviderID = ((long)(0));
            this.gloLabUC_Transaction1.Size = new System.Drawing.Size(796, 326);
            this.gloLabUC_Transaction1.TabIndex = 33;
            this.gloLabUC_Transaction1.TransactionType = gloUserControlLibrary.gloLabUC_Transaction.enumTransactionType.None;
            this.gloLabUC_Transaction1.gUC_TestSelected += new gloUserControlLibrary.gloLabUC_Transaction.gUC_TestSelectedEventHandler(this.gloLabUC_Transaction1_gUC_TestSelected);
            this.gloLabUC_Transaction1.gUC_ScanDocument += new gloUserControlLibrary.gloLabUC_Transaction.gUC_ScanDocumentEventHandler(this.gloLabUC_Transaction1_gUC_ScanDocument);
            this.gloLabUC_Transaction1.gUC_ViewDocument += new gloUserControlLibrary.gloLabUC_Transaction.gUC_ViewDocumentEventHandler(this.gloLabUC_Transaction1_gUC_ViewDocument);
            this.gloLabUC_Transaction1.gUC_AddFormHandlerClick += new gloUserControlLibrary.gloLabUC_Transaction.gUC_AddFormHandlerClickEventHandler(this.gloLabUC_Transaction1_gUC_AddFormHandlerClick);
            this.gloLabUC_Transaction1.gUC_ViewDicomDocument += new gloUserControlLibrary.gloLabUC_Transaction.gUC_ViewDicomDocumentEventHandler(this.gloLabUC_Transaction1_gUC_ViewDicomDocument);
            this.gloLabUC_Transaction1.gUC_ButtonDiagnCPTClicked += new gloUserControlLibrary.gloLabUC_Transaction.gUC_ButtonDiagnCPTClickedEventHandler(this.gloLabUC_Transaction1_gUC_ButtonDiagnCPTClicked);
            this.gloLabUC_Transaction1.gUC_ButtonTemplatesClicked += new gloUserControlLibrary.gloLabUC_Transaction.gUC_ButtonTemplatesClickedEventHandler(this.gloLabUC_Transaction1_gUC_ButtonTemplatesClicked);
            this.gloLabUC_Transaction1.gUC_OkButtonClicked += new gloUserControlLibrary.gloLabUC_Transaction.gUC_OkButtonClickedEventHandler(this.gloLabUC_Transaction1_gUC_OkButtonClicked);
            this.gloLabUC_Transaction1.gUC_InfoButtonClicked += new gloUserControlLibrary.gloLabUC_Transaction.gUC_InfoButtonClickedEventHandler(this.gloLabUC_transaction1_gUC_InfobuttonClicked);
            this.gloLabUC_Transaction1.gUC_InfoButtonDocumentClicked += new gloUserControlLibrary.gloLabUC_Transaction.gUC_InfoButtonDocumentClickedEventHandler(this.gloLabUC_transaction1_gUC_InfoButtonDocumentClicked);
            this.gloLabUC_Transaction1.Load += new System.EventHandler(this.gloLabUC_Transaction1_Load);
            // 
            // label29
            // 
            this.label29.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label29.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label29.Location = new System.Drawing.Point(0, 330);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(796, 1);
            this.label29.TabIndex = 1;
            this.label29.Text = "label29";
            // 
            // label30
            // 
            this.label30.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label30.Dock = System.Windows.Forms.DockStyle.Top;
            this.label30.Location = new System.Drawing.Point(0, 3);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(796, 1);
            this.label30.TabIndex = 0;
            this.label30.Text = "label30";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.chkOutboundTransition);
            this.panel3.Controls.Add(this.chkFasting);
            this.panel3.Controls.Add(this.label31);
            this.panel3.Controls.Add(this.cmbOrderStatus);
            this.panel3.Controls.Add(this.chkOrderNotCPOE);
            this.panel3.Controls.Add(this.label23);
            this.panel3.Controls.Add(this.label19);
            this.panel3.Controls.Add(this.label14);
            this.panel3.Controls.Add(this.label8);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 178);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(796, 32);
            this.panel3.TabIndex = 34;
            // 
            // chkOutboundTransition
            // 
            this.chkOutboundTransition.AutoSize = true;
            this.chkOutboundTransition.Dock = System.Windows.Forms.DockStyle.Right;
            this.chkOutboundTransition.Location = new System.Drawing.Point(426, 1);
            this.chkOutboundTransition.Name = "chkOutboundTransition";
            this.chkOutboundTransition.Size = new System.Drawing.Size(190, 30);
            this.chkOutboundTransition.TabIndex = 7;
            this.chkOutboundTransition.Text = "Outbound Transition of Care  ";
            this.chkOutboundTransition.UseVisualStyleBackColor = true;
            // 
            // chkFasting
            // 
            this.chkFasting.AutoSize = true;
            this.chkFasting.Dock = System.Windows.Forms.DockStyle.Right;
            this.chkFasting.Location = new System.Drawing.Point(616, 1);
            this.chkFasting.Name = "chkFasting";
            this.chkFasting.Size = new System.Drawing.Size(64, 30);
            this.chkFasting.TabIndex = 8;
            this.chkFasting.Text = "Fasting";
            this.chkFasting.UseVisualStyleBackColor = true;
            this.chkFasting.Visible = false;
            // 
            // label31
            // 
            this.label31.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(20, 9);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(85, 14);
            this.label31.TabIndex = 6;
            this.label31.Text = "Order Status :";
            // 
            // cmbOrderStatus
            // 
            this.cmbOrderStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.cmbOrderStatus.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbOrderStatus.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbOrderStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOrderStatus.FormattingEnabled = true;
            this.cmbOrderStatus.Location = new System.Drawing.Point(109, 5);
            this.cmbOrderStatus.Name = "cmbOrderStatus";
            this.cmbOrderStatus.Size = new System.Drawing.Size(272, 22);
            this.cmbOrderStatus.TabIndex = 5;
            // 
            // chkOrderNotCPOE
            // 
            this.chkOrderNotCPOE.AutoSize = true;
            this.chkOrderNotCPOE.Dock = System.Windows.Forms.DockStyle.Right;
            this.chkOrderNotCPOE.Location = new System.Drawing.Point(680, 1);
            this.chkOrderNotCPOE.Name = "chkOrderNotCPOE";
            this.chkOrderNotCPOE.Size = new System.Drawing.Size(115, 30);
            this.chkOrderNotCPOE.TabIndex = 4;
            this.chkOrderNotCPOE.Text = "Order Not CPOE";
            this.chkOrderNotCPOE.UseVisualStyleBackColor = true;
            // 
            // label23
            // 
            this.label23.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label23.Dock = System.Windows.Forms.DockStyle.Right;
            this.label23.Location = new System.Drawing.Point(795, 1);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(1, 30);
            this.label23.TabIndex = 3;
            this.label23.Text = "label23";
            // 
            // label19
            // 
            this.label19.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label19.Dock = System.Windows.Forms.DockStyle.Left;
            this.label19.Location = new System.Drawing.Point(0, 1);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(1, 30);
            this.label19.TabIndex = 2;
            this.label19.Text = "label19";
            // 
            // label14
            // 
            this.label14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label14.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label14.Location = new System.Drawing.Point(0, 31);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(796, 1);
            this.label14.TabIndex = 1;
            this.label14.Text = "label14";
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label8.Dock = System.Windows.Forms.DockStyle.Top;
            this.label8.Location = new System.Drawing.Point(0, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(796, 1);
            this.label8.TabIndex = 0;
            this.label8.Text = "label8";
            // 
            // gloUCLab_Transaction
            // 
            this.gloUCLab_Transaction.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.gloUCLab_Transaction.dtSelectedFromDt = new System.DateTime(((long)(0)));
            this.gloUCLab_Transaction.dtSelectedToDt = new System.DateTime(((long)(0)));
            this.gloUCLab_Transaction.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gloUCLab_Transaction.ForeColor = System.Drawing.Color.Black;
            this.gloUCLab_Transaction.LabResultId = ((long)(0));
            this.gloUCLab_Transaction.LabResultName = null;
            this.gloUCLab_Transaction.LabTestId = ((long)(0));
            this.gloUCLab_Transaction.LabTestName = null;
            this.gloUCLab_Transaction.Location = new System.Drawing.Point(449, 225);
            this.gloUCLab_Transaction.Name = "gloUCLab_Transaction";
            this.gloUCLab_Transaction.Padding = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.gloUCLab_Transaction.PatientID = ((long)(0));
            this.gloUCLab_Transaction.Size = new System.Drawing.Size(245, 239);
            this.gloUCLab_Transaction.TabIndex = 31;
            this.gloUCLab_Transaction.TransactionType = gloUserControlLibrary.gloUC_Transaction.enumTransactionType.None;
            // 
            // gloUCLab_TestDetail
            // 
            this.gloUCLab_TestDetail.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.gloUCLab_TestDetail.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.gloUCLab_TestDetail.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.gloUCLab_TestDetail.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gloUCLab_TestDetail.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.gloUCLab_TestDetail.Location = new System.Drawing.Point(0, 541);
            this.gloUCLab_TestDetail.Name = "gloUCLab_TestDetail";
            this.gloUCLab_TestDetail.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.gloUCLab_TestDetail.Size = new System.Drawing.Size(796, 84);
            this.gloUCLab_TestDetail.TabIndex = 32;
            // 
            // gloUCLab_OrderDetail
            // 
            this.gloUCLab_OrderDetail.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.gloUCLab_OrderDetail.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.gloUCLab_OrderDetail.ClinicID = ((long)(0));
            this.gloUCLab_OrderDetail.Dock = System.Windows.Forms.DockStyle.Top;
            this.gloUCLab_OrderDetail.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gloUCLab_OrderDetail.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.gloUCLab_OrderDetail.IsOpenedFromViewLab = false;
            this.gloUCLab_OrderDetail.IsPreferredIDCleared = "False";
            this.gloUCLab_OrderDetail.Location = new System.Drawing.Point(0, 33);
            this.gloUCLab_OrderDetail.Name = "gloUCLab_OrderDetail";
            this.gloUCLab_OrderDetail.OrderLabType = "";
            this.gloUCLab_OrderDetail.OrderModified = false;
            this.gloUCLab_OrderDetail.OrderNumberID = ((long)(0));
            this.gloUCLab_OrderDetail.OrderNumberPrefix = null;
            this.gloUCLab_OrderDetail.OrderSelected = false;
            this.gloUCLab_OrderDetail.Padding = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.gloUCLab_OrderDetail.PreferredLab = null;
            this.gloUCLab_OrderDetail.PreferredLabID = ((long)(0));
            this.gloUCLab_OrderDetail.ReferredBy = null;
            this.gloUCLab_OrderDetail.ReferredByFName = "";
            this.gloUCLab_OrderDetail.ReferredByID = ((long)(0));
            this.gloUCLab_OrderDetail.ReferredByLName = "";
            this.gloUCLab_OrderDetail.ReferredByMName = "";
            this.gloUCLab_OrderDetail.ReferredTo = null;
            this.gloUCLab_OrderDetail.ReferredToID = ((long)(0));
            this.gloUCLab_OrderDetail.SampledBy = null;
            this.gloUCLab_OrderDetail.SampledByID = ((long)(0));
            this.gloUCLab_OrderDetail.SendTo = 1;
            this.gloUCLab_OrderDetail.Size = new System.Drawing.Size(796, 145);
            this.gloUCLab_OrderDetail.TabIndex = 18;
            this.gloUCLab_OrderDetail.TaskDescription = null;
            this.gloUCLab_OrderDetail.TaskDueDate = new System.DateTime(((long)(0)));
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.Transparent;
            this.panel6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel6.Controls.Add(this.panel7);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel6.Location = new System.Drawing.Point(0, 0);
            this.panel6.Name = "panel6";
            this.panel6.Padding = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.panel6.Size = new System.Drawing.Size(796, 33);
            this.panel6.TabIndex = 48;
            // 
            // panel7
            // 
            this.panel7.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel7.BackgroundImage")));
            this.panel7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel7.Controls.Add(this.label46);
            this.panel7.Controls.Add(this.label45);
            this.panel7.Controls.Add(this.label44);
            this.panel7.Controls.Add(this.label38);
            this.panel7.Controls.Add(this.label43);
            this.panel7.Controls.Add(this.cmbProvider);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel7.Location = new System.Drawing.Point(0, 3);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(796, 27);
            this.panel7.TabIndex = 37;
            // 
            // label46
            // 
            this.label46.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label46.Dock = System.Windows.Forms.DockStyle.Top;
            this.label46.Location = new System.Drawing.Point(1, 0);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(794, 1);
            this.label46.TabIndex = 47;
            this.label46.Text = "label46";
            // 
            // label45
            // 
            this.label45.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label45.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label45.Location = new System.Drawing.Point(1, 26);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(794, 1);
            this.label45.TabIndex = 46;
            this.label45.Text = "label45";
            // 
            // label44
            // 
            this.label44.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label44.Dock = System.Windows.Forms.DockStyle.Left;
            this.label44.Location = new System.Drawing.Point(0, 0);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(1, 27);
            this.label44.TabIndex = 45;
            this.label44.Text = "label44";
            // 
            // label38
            // 
            this.label38.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label38.Dock = System.Windows.Forms.DockStyle.Right;
            this.label38.Location = new System.Drawing.Point(795, 0);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(1, 27);
            this.label38.TabIndex = 44;
            this.label38.Text = "label38";
            // 
            // label43
            // 
            this.label43.AutoSize = true;
            this.label43.BackColor = System.Drawing.Color.Transparent;
            this.label43.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label43.ForeColor = System.Drawing.Color.White;
            this.label43.Location = new System.Drawing.Point(9, 7);
            this.label43.Name = "label43";
            this.label43.Padding = new System.Windows.Forms.Padding(4, 0, 0, 0);
            this.label43.Size = new System.Drawing.Size(127, 14);
            this.label43.TabIndex = 43;
            this.label43.Text = "Ordering Provider :";
            this.label43.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmbProvider
            // 
            this.cmbProvider.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.cmbProvider.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbProvider.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbProvider.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProvider.FormattingEnabled = true;
            this.cmbProvider.Location = new System.Drawing.Point(139, 3);
            this.cmbProvider.Name = "cmbProvider";
            this.cmbProvider.Size = new System.Drawing.Size(242, 22);
            this.cmbProvider.TabIndex = 5;
            this.cmbProvider.SelectedIndexChanged += new System.EventHandler(this.cmbProvider_SelectedIndexChanged);
            this.cmbProvider.SelectionChangeCommitted += new System.EventHandler(this.cmbProvider_SelectionChangeCommitted);
            // 
            // ImageList1
            // 
            this.ImageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ImageList1.ImageStream")));
            this.ImageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.ImageList1.Images.SetKeyName(0, "");
            this.ImageList1.Images.SetKeyName(1, "");
            // 
            // pnlOrder
            // 
            this.pnlOrder.Controls.Add(this.panel1);
            this.pnlOrder.Controls.Add(this.splitter1);
            this.pnlOrder.Controls.Add(this.pnlLeft);
            this.pnlOrder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlOrder.Location = new System.Drawing.Point(0, 54);
            this.pnlOrder.Name = "pnlOrder";
            this.pnlOrder.Size = new System.Drawing.Size(1038, 625);
            this.pnlOrder.TabIndex = 43;
            // 
            // pnlWordTemplate
            // 
            this.pnlWordTemplate.Controls.Add(this.Panel4);
            this.pnlWordTemplate.Controls.Add(this.Splitter4);
            this.pnlWordTemplate.Controls.Add(this.pnlGloUC_TemplateTreeControl);
            this.pnlWordTemplate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlWordTemplate.Location = new System.Drawing.Point(0, 54);
            this.pnlWordTemplate.Name = "pnlWordTemplate";
            this.pnlWordTemplate.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.pnlWordTemplate.Size = new System.Drawing.Size(1038, 625);
            this.pnlWordTemplate.TabIndex = 44;
            this.pnlWordTemplate.Visible = false;
            // 
            // Panel4
            // 
            this.Panel4.Controls.Add(this.pnl_wdOrders);
            this.Panel4.Controls.Add(this.pnl_lblHeading);
            this.Panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Panel4.Location = new System.Drawing.Point(220, 3);
            this.Panel4.Name = "Panel4";
            this.Panel4.Size = new System.Drawing.Size(818, 622);
            this.Panel4.TabIndex = 47;
            // 
            // pnl_wdOrders
            // 
            this.pnl_wdOrders.BackColor = System.Drawing.Color.Transparent;
            this.pnl_wdOrders.Controls.Add(this.wdOrders);
            this.pnl_wdOrders.Controls.Add(this.label5);
            this.pnl_wdOrders.Controls.Add(this.Label24);
            this.pnl_wdOrders.Controls.Add(this.Label25);
            this.pnl_wdOrders.Controls.Add(this.Label26);
            this.pnl_wdOrders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_wdOrders.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnl_wdOrders.Location = new System.Drawing.Point(0, 26);
            this.pnl_wdOrders.Name = "pnl_wdOrders";
            this.pnl_wdOrders.Padding = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.pnl_wdOrders.Size = new System.Drawing.Size(818, 596);
            this.pnl_wdOrders.TabIndex = 0;
            // 
            // wdOrders
            // 
            this.wdOrders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wdOrders.Enabled = true;
            this.wdOrders.Location = new System.Drawing.Point(1, 1);
            this.wdOrders.Name = "wdOrders";
            this.wdOrders.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("wdOrders.OcxState")));
            this.wdOrders.Size = new System.Drawing.Size(816, 591);
            this.wdOrders.TabIndex = 0;
            this.wdOrders.OnDocumentOpened += new AxDSOFramer._DFramerCtlEvents_OnDocumentOpenedEventHandler(this.wdOrders_OnDocumentOpened);
            this.wdOrders.OnDocumentClosed += new System.EventHandler(this.wdOrders_OnDocumentClosed);
            this.wdOrders.BeforeDocumentClosed += new AxDSOFramer._DFramerCtlEvents_BeforeDocumentClosedEventHandler(this.wdOrders_BeforeDocumentClosed);
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label5.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label5.Location = new System.Drawing.Point(1, 592);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(816, 1);
            this.label5.TabIndex = 12;
            this.label5.Text = "label2";
            // 
            // Label24
            // 
            this.Label24.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label24.Dock = System.Windows.Forms.DockStyle.Left;
            this.Label24.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label24.Location = new System.Drawing.Point(0, 1);
            this.Label24.Name = "Label24";
            this.Label24.Size = new System.Drawing.Size(1, 592);
            this.Label24.TabIndex = 11;
            this.Label24.Text = "label4";
            // 
            // Label25
            // 
            this.Label25.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label25.Dock = System.Windows.Forms.DockStyle.Right;
            this.Label25.Font = new System.Drawing.Font("Tahoma", 9F);
            this.Label25.Location = new System.Drawing.Point(817, 1);
            this.Label25.Name = "Label25";
            this.Label25.Size = new System.Drawing.Size(1, 592);
            this.Label25.TabIndex = 10;
            this.Label25.Text = "label3";
            // 
            // Label26
            // 
            this.Label26.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label26.Dock = System.Windows.Forms.DockStyle.Top;
            this.Label26.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label26.Location = new System.Drawing.Point(0, 0);
            this.Label26.Name = "Label26";
            this.Label26.Size = new System.Drawing.Size(818, 1);
            this.Label26.TabIndex = 9;
            this.Label26.Text = "label1";
            // 
            // pnl_lblHeading
            // 
            this.pnl_lblHeading.Controls.Add(this.Panel2);
            this.pnl_lblHeading.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_lblHeading.Location = new System.Drawing.Point(0, 0);
            this.pnl_lblHeading.Name = "pnl_lblHeading";
            this.pnl_lblHeading.Padding = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.pnl_lblHeading.Size = new System.Drawing.Size(818, 26);
            this.pnl_lblHeading.TabIndex = 48;
            // 
            // Panel2
            // 
            this.Panel2.BackColor = System.Drawing.Color.Transparent;
            this.Panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Panel2.Controls.Add(this.GloUC_AddRefreshDic1);
            this.Panel2.Controls.Add(this.lblHeading);
            this.Panel2.Controls.Add(this.Label13);
            this.Panel2.Controls.Add(this.Label22);
            this.Panel2.Controls.Add(this.label6);
            this.Panel2.Controls.Add(this.label7);
            this.Panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Panel2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Panel2.Location = new System.Drawing.Point(0, 0);
            this.Panel2.Name = "Panel2";
            this.Panel2.Size = new System.Drawing.Size(818, 23);
            this.Panel2.TabIndex = 37;
            // 
            // GloUC_AddRefreshDic1
            // 
            this.GloUC_AddRefreshDic1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GloUC_AddRefreshDic1.BackColor = System.Drawing.Color.Transparent;
            this.GloUC_AddRefreshDic1.CONNECTIONSTRINGs = null;
            this.GloUC_AddRefreshDic1.dtLetterAllocated = false;
            this.GloUC_AddRefreshDic1.DTLETTERDATEs = null;
            this.GloUC_AddRefreshDic1.Location = new System.Drawing.Point(763, 2);
            this.GloUC_AddRefreshDic1.M_PATIENTIDs = ((long)(0));
            this.GloUC_AddRefreshDic1.M_ProviderIDs = ((long)(0));
            this.GloUC_AddRefreshDic1.Name = "GloUC_AddRefreshDic1";
            this.GloUC_AddRefreshDic1.OBJCRITERIAs = null;
            this.GloUC_AddRefreshDic1.ObjFrom = null;
            this.GloUC_AddRefreshDic1.OBJWORDs = null;
            this.GloUC_AddRefreshDic1.OCURDOCs = null;
            this.GloUC_AddRefreshDic1.OWORDAPPs = null;
            this.GloUC_AddRefreshDic1.Size = new System.Drawing.Size(48, 19);
            this.GloUC_AddRefreshDic1.TabIndex = 47;
            this.GloUC_AddRefreshDic1.wdPatientWordDocs = null;
            // 
            // lblHeading
            // 
            this.lblHeading.BackColor = System.Drawing.Color.Transparent;
            this.lblHeading.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblHeading.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblHeading.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeading.Location = new System.Drawing.Point(1, 1);
            this.lblHeading.Name = "lblHeading";
            this.lblHeading.Size = new System.Drawing.Size(816, 21);
            this.lblHeading.TabIndex = 0;
            this.lblHeading.Tag = "  ";
            this.lblHeading.Text = "  Order Comments :";
            this.lblHeading.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label13
            // 
            this.Label13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label13.Dock = System.Windows.Forms.DockStyle.Top;
            this.Label13.Location = new System.Drawing.Point(1, 0);
            this.Label13.Name = "Label13";
            this.Label13.Size = new System.Drawing.Size(816, 1);
            this.Label13.TabIndex = 41;
            this.Label13.Text = "label1";
            // 
            // Label22
            // 
            this.Label22.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label22.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Label22.Location = new System.Drawing.Point(1, 22);
            this.Label22.Name = "Label22";
            this.Label22.Size = new System.Drawing.Size(816, 1);
            this.Label22.TabIndex = 46;
            this.Label22.Text = "label2";
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label6.Dock = System.Windows.Forms.DockStyle.Left;
            this.label6.Location = new System.Drawing.Point(0, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(1, 23);
            this.label6.TabIndex = 43;
            this.label6.Text = "label4";
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label7.Dock = System.Windows.Forms.DockStyle.Right;
            this.label7.Location = new System.Drawing.Point(817, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(1, 23);
            this.label7.TabIndex = 42;
            this.label7.Text = "label3";
            // 
            // Splitter4
            // 
            this.Splitter4.Location = new System.Drawing.Point(217, 3);
            this.Splitter4.Name = "Splitter4";
            this.Splitter4.Size = new System.Drawing.Size(3, 622);
            this.Splitter4.TabIndex = 49;
            this.Splitter4.TabStop = false;
            // 
            // pnlGloUC_TemplateTreeControl
            // 
            this.pnlGloUC_TemplateTreeControl.Controls.Add(this.GloUC_TemplateTreeControl_Orders);
            this.pnlGloUC_TemplateTreeControl.Controls.Add(this.pnl_cmdPastExam);
            this.pnlGloUC_TemplateTreeControl.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlGloUC_TemplateTreeControl.Location = new System.Drawing.Point(0, 3);
            this.pnlGloUC_TemplateTreeControl.Name = "pnlGloUC_TemplateTreeControl";
            this.pnlGloUC_TemplateTreeControl.Padding = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.pnlGloUC_TemplateTreeControl.Size = new System.Drawing.Size(217, 622);
            this.pnlGloUC_TemplateTreeControl.TabIndex = 48;
            // 
            // GloUC_TemplateTreeControl_Orders
            // 
            this.GloUC_TemplateTreeControl_Orders.DocCriteria = null;
            this.GloUC_TemplateTreeControl_Orders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GloUC_TemplateTreeControl_Orders.ExpandConsent = false;
            this.GloUC_TemplateTreeControl_Orders.Location = new System.Drawing.Point(0, 27);
            this.GloUC_TemplateTreeControl_Orders.Name = "GloUC_TemplateTreeControl_Orders";
            this.GloUC_TemplateTreeControl_Orders.ObjClsWord = null;
            this.GloUC_TemplateTreeControl_Orders.ProviderId = ((long)(0));
            this.GloUC_TemplateTreeControl_Orders.Size = new System.Drawing.Size(217, 592);
            this.GloUC_TemplateTreeControl_Orders.TabIndex = 0;
            this.GloUC_TemplateTreeControl_Orders.Treeview_NodeMouseDoubleClick += new gloUserControlLibrary.gloUC_TemplateTreeControl.Treeview_NodeMouseDoubleClickEventHandler(this.GloUC_TemplateTreeControl_Orders_Treeview_NodeMouseDoubleClick);
            // 
            // pnl_cmdPastExam
            // 
            this.pnl_cmdPastExam.BackColor = System.Drawing.Color.Transparent;
            this.pnl_cmdPastExam.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnl_cmdPastExam.Controls.Add(this.cmdPastExam);
            this.pnl_cmdPastExam.Controls.Add(this.Label57);
            this.pnl_cmdPastExam.Controls.Add(this.Label58);
            this.pnl_cmdPastExam.Controls.Add(this.Label59);
            this.pnl_cmdPastExam.Controls.Add(this.Label60);
            this.pnl_cmdPastExam.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_cmdPastExam.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnl_cmdPastExam.Location = new System.Drawing.Point(0, 0);
            this.pnl_cmdPastExam.Name = "pnl_cmdPastExam";
            this.pnl_cmdPastExam.Padding = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.pnl_cmdPastExam.Size = new System.Drawing.Size(217, 27);
            this.pnl_cmdPastExam.TabIndex = 1;
            // 
            // cmdPastExam
            // 
            this.cmdPastExam.BackColor = System.Drawing.Color.Transparent;
            this.cmdPastExam.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cmdPastExam.BackgroundImage")));
            this.cmdPastExam.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.cmdPastExam.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmdPastExam.FlatAppearance.BorderSize = 0;
            this.cmdPastExam.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(197)))), ((int)(((byte)(108)))));
            this.cmdPastExam.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(160)))));
            this.cmdPastExam.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdPastExam.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdPastExam.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdPastExam.Location = new System.Drawing.Point(1, 1);
            this.cmdPastExam.Name = "cmdPastExam";
            this.cmdPastExam.Size = new System.Drawing.Size(215, 22);
            this.cmdPastExam.TabIndex = 3;
            this.cmdPastExam.Text = "Orders";
            this.cmdPastExam.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdPastExam.UseVisualStyleBackColor = false;
            // 
            // Label57
            // 
            this.Label57.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label57.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Label57.Font = new System.Drawing.Font("Tahoma", 9F);
            this.Label57.Location = new System.Drawing.Point(1, 23);
            this.Label57.Name = "Label57";
            this.Label57.Size = new System.Drawing.Size(215, 1);
            this.Label57.TabIndex = 12;
            this.Label57.Text = "label2";
            // 
            // Label58
            // 
            this.Label58.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label58.Dock = System.Windows.Forms.DockStyle.Left;
            this.Label58.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label58.Location = new System.Drawing.Point(0, 1);
            this.Label58.Name = "Label58";
            this.Label58.Size = new System.Drawing.Size(1, 23);
            this.Label58.TabIndex = 11;
            this.Label58.Text = "label4";
            // 
            // Label59
            // 
            this.Label59.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label59.Dock = System.Windows.Forms.DockStyle.Right;
            this.Label59.Font = new System.Drawing.Font("Tahoma", 9F);
            this.Label59.Location = new System.Drawing.Point(216, 1);
            this.Label59.Name = "Label59";
            this.Label59.Size = new System.Drawing.Size(1, 23);
            this.Label59.TabIndex = 10;
            this.Label59.Text = "label3";
            // 
            // Label60
            // 
            this.Label60.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label60.Dock = System.Windows.Forms.DockStyle.Top;
            this.Label60.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label60.Location = new System.Drawing.Point(0, 0);
            this.Label60.Name = "Label60";
            this.Label60.Size = new System.Drawing.Size(217, 1);
            this.Label60.TabIndex = 9;
            this.Label60.Text = "label1";
            // 
            // tmrDocProtect
            // 
            this.tmrDocProtect.Interval = 500;
            this.tmrDocProtect.Tick += new System.EventHandler(this.tmrDocProtect_Tick);
            // 
            // frmViewNormalLab
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(1038, 679);
            this.Controls.Add(this.pnlOrder);
            this.Controls.Add(this.pnlWordTemplate);
            this.Controls.Add(this.pnlToolStrip);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmViewNormalLab";
            this.Text = "Order Entry";
            this.Activated += new System.EventHandler(this.frmViewNormalLab_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmViewNormalLab_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmViewNormalLab_FormClosed);
            this.Load += new System.EventHandler(this.frmViewNormalLab_Load);
            this.pnlToolStrip.ResumeLayout(false);
            this.pnlToolStrip.PerformLayout();
            this.ts_LabMain.ResumeLayout(false);
            this.ts_LabMain.PerformLayout();
            this.pnlLeft.ResumeLayout(false);
            this.pnlList_Detail.ResumeLayout(false);
            this.pnltrvList.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            this.pnl_btnTests.ResumeLayout(false);
            this.pnl_btnRadiologyImaging.ResumeLayout(false);
            this.pnl_btnRefTest.ResumeLayout(false);
            this.pnl_btnOthers.ResumeLayout(false);
            this.pnl_btnGroups.ResumeLayout(false);
            this.pnlPlannedOrder.ResumeLayout(false);
            this.pnlSearch.ResumeLayout(false);
            this.pnlSearch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.pnlOrder.ResumeLayout(false);
            this.pnlWordTemplate.ResumeLayout(false);
            this.Panel4.ResumeLayout(false);
            this.pnl_wdOrders.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.wdOrders)).EndInit();
            this.pnl_lblHeading.ResumeLayout(false);
            this.Panel2.ResumeLayout(false);
            this.pnlGloUC_TemplateTreeControl.ResumeLayout(false);
            this.pnl_cmdPastExam.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Panel pnlToolStrip;
        internal System.Windows.Forms.ToolStripButton tlbbtn_New;
        internal System.Windows.Forms.ToolStripButton tlbbtn_Save;
        internal System.Windows.Forms.ToolStripButton tlbbtn_Finish;
        internal System.Windows.Forms.ToolStripButton tlbbtn_Print;
        internal System.Windows.Forms.ToolStripButton tlbbtn_Fax;
        internal System.Windows.Forms.ToolStripButton tlbbtn_Previous;
        internal System.Windows.Forms.ToolStripButton tlbbtn_HL7;
        public System.Windows.Forms.ToolStripButton tlbbtn_Acknowledgment;
        public System.Windows.Forms.ToolStripButton tlbbtn_VWAcknowledgment;
        internal System.Windows.Forms.ToolStripButton tlbbtn_PrvLabs;
        internal System.Windows.Forms.ToolStripButton tlbbtn_Close;
        internal System.Windows.Forms.Panel pnlLeft;
        internal System.Windows.Forms.Panel pnlList_Detail;
        private System.Windows.Forms.Panel pnltrvList;
        internal gloUserControlLibrary.gloUC_TreeView GloUC_trvTest;
        internal System.Windows.Forms.TreeView trvList;
        private System.Windows.Forms.Panel pnl_btnTests;
        private System.Windows.Forms.Panel pnl_btnRadiologyImaging;
        internal System.Windows.Forms.Button btnTests;
        internal System.Windows.Forms.Button btnRadiologyImaging;
        private System.Windows.Forms.Label Label15;
        private System.Windows.Forms.Label Label16;
        private System.Windows.Forms.Label Label17;
        private System.Windows.Forms.Label Label18;
        private System.Windows.Forms.Label lblRadiologyImaging1;
        private System.Windows.Forms.Label lblRadiologyImaging2;
        private System.Windows.Forms.Label lblRadiologyImaging3;
        private System.Windows.Forms.Label lblRadiologyImaging4;
        private System.Windows.Forms.Panel pnl_btnGroups;
        private System.Windows.Forms.Panel pnl_btnRefTest;
        internal System.Windows.Forms.Button btnGroups;
        internal System.Windows.Forms.Button btnRefTest;
        private System.Windows.Forms.Label Label1;
        private System.Windows.Forms.Label Label2;
        private System.Windows.Forms.Label Label3;
        private System.Windows.Forms.Label Label4;
        internal System.Windows.Forms.Panel pnlSearch;
        internal System.Windows.Forms.TextBox txtListSearch;
        internal System.Windows.Forms.Label Label20;
        internal System.Windows.Forms.Label Label21;
        internal System.Windows.Forms.PictureBox PictureBox1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label Label12;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        internal System.Windows.Forms.ToolStripButton toolStripButton1;
        internal System.Windows.Forms.ToolStripButton toolStripButton2;
        internal System.Windows.Forms.ToolStripButton toolStripButton11;
        private System.Windows.Forms.Splitter splitter1;
        public System.Windows.Forms.Panel panel1;
        public gloUserControlLibrary.gloUC_LabOrderDetail gloUCLab_OrderDetail;
        internal gloUserControlLibrary.gloUC_Transaction gloUCLab_Transaction;
        internal gloUserControlLibrary.gloUC_LabTest gloUCLab_TestDetail;
        internal System.Windows.Forms.ImageList ImageList1;
        //internal System.Windows.Forms.PrintDialog PrintDialog1;
        private gloGlobal.gloToolStripIgnoreFocus ts_LabMain;
        private System.Windows.Forms.ToolStripButton tlbbtnNew;
        private System.Windows.Forms.ToolStripButton tlbbtnSave;
        private System.Windows.Forms.ToolStripButton tlbbtnClose;
        private gloUserControlLibrary.gloLabUC_Transaction gloLabUC_Transaction1;
        public System.Windows.Forms.Panel pnlOrder;
        internal System.Windows.Forms.Panel pnlWordTemplate;
        internal System.Windows.Forms.Panel Panel4;
        internal System.Windows.Forms.Panel pnl_wdOrders;
        internal AxDSOFramer.AxFramerControl wdOrders;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label Label24;
        private System.Windows.Forms.Label Label25;
        private System.Windows.Forms.Label Label26;
        internal System.Windows.Forms.Panel pnl_lblHeading;
        internal System.Windows.Forms.Panel Panel2;
        internal gloUserControlLibrary.gloUC_AddRefreshDic GloUC_AddRefreshDic1;
        internal System.Windows.Forms.Label lblHeading;
        private System.Windows.Forms.Label Label13;
        private System.Windows.Forms.Label Label22;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        internal System.Windows.Forms.Splitter Splitter4;
        internal System.Windows.Forms.Panel pnlGloUC_TemplateTreeControl;
        internal gloUserControlLibrary.gloUC_TemplateTreeControl GloUC_TemplateTreeControl_Orders;
        internal System.Windows.Forms.Panel pnl_cmdPastExam;
        internal System.Windows.Forms.Button cmdPastExam;
        private System.Windows.Forms.Label Label57;
        private System.Windows.Forms.Label Label58;
        private System.Windows.Forms.Label Label59;
        private System.Windows.Forms.Label Label60;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.ComboBox cmbOrderStatus;
        private System.Windows.Forms.CheckBox chkOrderNotCPOE;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox chkOutboundTransition;
        private System.Windows.Forms.CheckBox chkFasting;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Panel pnl_btnOthers;
        internal System.Windows.Forms.Button btnOthers;
        private System.Windows.Forms.Label lblOthers1;
        private System.Windows.Forms.Label lblOthers2;
        private System.Windows.Forms.Label lblOthers3;
        private System.Windows.Forms.Label lblOthers4;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.ComboBox cmbProvider;
        internal System.Windows.Forms.Panel panel7;
        internal System.Windows.Forms.Label label43;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label label46;
        private System.Windows.Forms.Label label45;
        private System.Windows.Forms.Label label44;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.CheckBox chkIncludeTestCode;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.Label label42;
        private System.Windows.Forms.Panel pnlPlannedOrder;
        private System.Windows.Forms.Label label47;
        private System.Windows.Forms.Label label48;
        private System.Windows.Forms.Label label49;
        private System.Windows.Forms.Label label50;
        internal System.Windows.Forms.Button btnPlannedOrder;
        private System.Windows.Forms.Timer tmrDocProtect;
        
    }
}
