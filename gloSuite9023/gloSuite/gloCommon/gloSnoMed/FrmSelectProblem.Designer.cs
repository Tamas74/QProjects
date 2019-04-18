namespace gloSnoMed
{
	partial class FrmSelectProblem
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
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(this);
                }
                catch
                {
                }
                try
                {

                    System.Windows.Forms.ContextMenuStrip[] cntmnuControls = { cntFindings };
                    System.Windows.Forms.Control[] cntControls = { cntFindings };
                    if (cntmnuControls != null)
                    {
                        if (cntmnuControls.Length > 0)
                        {
                            gloGlobal.cEventHelper.RemoveAllEventHandlers(ref cntmnuControls);

                        }
                    }
                    if (cntControls != null)
                    {
                        if (cntControls.Length > 0)
                        {
                            gloGlobal.cEventHelper.DisposeAllControls(ref cntControls);

                        }
                    }
                    
                    
                    //if (cntFindings != null)
                    //{
                    //    gloGlobal.cEventHelper.RemoveAllEventHandlers(cntFindings);
                    //    if (cntFindings.Items != null)
                    //    {
                    //        cntFindings.Items.Clear();

                    //    }
                    //    cntFindings.Dispose();
                    //    cntFindings = null;
                    //}
                }
                catch
                {
                }

				components.Dispose();
			}
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSelectProblem));
            this.trvSnoMed = new System.Windows.Forms.TreeView();
            this.lblSearchby = new System.Windows.Forms.Label();
            this.txtDescriptionID = new System.Windows.Forms.TextBox();
            this.tls_SM = new gloGlobal.gloToolStripIgnoreFocus();
            this.tlb_SMSave = new System.Windows.Forms.ToolStripButton();
            this.tlb_SMClose = new System.Windows.Forms.ToolStripButton();
            this.trvSubtype = new System.Windows.Forms.TreeView();
            this.pnlsubType = new System.Windows.Forms.Panel();
            this.Panel3 = new System.Windows.Forms.Panel();
            this.Label27 = new System.Windows.Forms.Label();
            this.Label28 = new System.Windows.Forms.Label();
            this.Label29 = new System.Windows.Forms.Label();
            this.Label30 = new System.Windows.Forms.Label();
            this.txtConceptID = new System.Windows.Forms.TextBox();
            this.lblConceptID = new System.Windows.Forms.Label();
            this.Label7 = new System.Windows.Forms.Label();
            this.pnltlstripSM = new System.Windows.Forms.Panel();
            this.Panel7 = new System.Windows.Forms.Panel();
            this.Panel8 = new System.Windows.Forms.Panel();
            this.lblDescriptionID = new System.Windows.Forms.Label();
            this.Label9 = new System.Windows.Forms.Label();
            this.lblSnoMedID = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.Label4 = new System.Windows.Forms.Label();
            this.Label5 = new System.Windows.Forms.Label();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.panel16 = new System.Windows.Forms.Panel();
            this.RbConceptID = new System.Windows.Forms.RadioButton();
            this.RbICD9 = new System.Windows.Forms.RadioButton();
            this.RbICD10 = new System.Windows.Forms.RadioButton();
            this.panel9 = new System.Windows.Forms.Panel();
            this.btnAdvanceDef = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.chkCOREProblem = new System.Windows.Forms.CheckBox();
            this.tlAddFields = new System.Windows.Forms.ToolTip(this.components);
            this.btnClear = new System.Windows.Forms.Button();
            this.mnuFindings = new System.Windows.Forms.ToolStripMenuItem();
            this.cntFindings = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.trICD9 = new System.Windows.Forms.TreeView();
            this.pnlSMSearch = new System.Windows.Forms.Panel();
            this.txtSMSearch = new gloSnoMed.gloSearchTextBox();
            this.lbl_WhiteSpaceTop = new System.Windows.Forms.Label();
            this.lbl_WhiteSpaceBottom = new System.Windows.Forms.Label();
            this.PicBx_Search = new System.Windows.Forms.PictureBox();
            this.lbl_pnlSearchBottomBrd = new System.Windows.Forms.Label();
            this.lbl_pnlSearchTopBrd = new System.Windows.Forms.Label();
            this.lbl_pnlSearchLeftBrd = new System.Windows.Forms.Label();
            this.lbl_pnlSearchRightBrd = new System.Windows.Forms.Label();
            this.pnlICD9 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label19 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label43 = new System.Windows.Forms.Label();
            this.label47 = new System.Windows.Forms.Label();
            this.label48 = new System.Windows.Forms.Label();
            this.panel10 = new System.Windows.Forms.Panel();
            this.panel11 = new System.Windows.Forms.Panel();
            this.Label23 = new System.Windows.Forms.Label();
            this.label42 = new System.Windows.Forms.Label();
            this.label44 = new System.Windows.Forms.Label();
            this.label45 = new System.Windows.Forms.Label();
            this.label46 = new System.Windows.Forms.Label();
            this.trvFindings = new System.Windows.Forms.TreeView();
            this.pnlmiddle = new System.Windows.Forms.Panel();
            this.panel12 = new System.Windows.Forms.Panel();
            this.label49 = new System.Windows.Forms.Label();
            this.label50 = new System.Windows.Forms.Label();
            this.label51 = new System.Windows.Forms.Label();
            this.label52 = new System.Windows.Forms.Label();
            this.label53 = new System.Windows.Forms.Label();
            this.label54 = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.panel14 = new System.Windows.Forms.Panel();
            this.Label36 = new System.Windows.Forms.Label();
            this.label38 = new System.Windows.Forms.Label();
            this.label39 = new System.Windows.Forms.Label();
            this.label61 = new System.Windows.Forms.Label();
            this.label62 = new System.Windows.Forms.Label();
            this.pnlfinding = new System.Windows.Forms.Panel();
            this.label41 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label40 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.pnlleft = new System.Windows.Forms.Panel();
            this.Panel4 = new System.Windows.Forms.Panel();
            this.Panel5 = new System.Windows.Forms.Panel();
            this.Label35 = new System.Windows.Forms.Label();
            this.Label31 = new System.Windows.Forms.Label();
            this.Label34 = new System.Windows.Forms.Label();
            this.Label33 = new System.Windows.Forms.Label();
            this.Label32 = new System.Windows.Forms.Label();
            this.pnlSMMain = new System.Windows.Forms.Panel();
            this.pnlright = new System.Windows.Forms.Panel();
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.panel13 = new System.Windows.Forms.Panel();
            this.trICD10 = new System.Windows.Forms.TreeView();
            this.label55 = new System.Windows.Forms.Label();
            this.label56 = new System.Windows.Forms.Label();
            this.label57 = new System.Windows.Forms.Label();
            this.label58 = new System.Windows.Forms.Label();
            this.label59 = new System.Windows.Forms.Label();
            this.label60 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel15 = new System.Windows.Forms.Panel();
            this.label20 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.label37 = new System.Windows.Forms.Label();
            this.Splitter2 = new System.Windows.Forms.Splitter();
            this.splitter4 = new System.Windows.Forms.Splitter();
            this.splitter3 = new System.Windows.Forms.Splitter();
            this.Timer1 = new System.Windows.Forms.Timer(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.tls_SM.SuspendLayout();
            this.pnlsubType.SuspendLayout();
            this.Panel3.SuspendLayout();
            this.pnltlstripSM.SuspendLayout();
            this.Panel7.SuspendLayout();
            this.Panel8.SuspendLayout();
            this.pnlHeader.SuspendLayout();
            this.panel16.SuspendLayout();
            this.panel9.SuspendLayout();
            this.cntFindings.SuspendLayout();
            this.pnlSMSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PicBx_Search)).BeginInit();
            this.pnlICD9.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel10.SuspendLayout();
            this.panel11.SuspendLayout();
            this.pnlmiddle.SuspendLayout();
            this.panel12.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel14.SuspendLayout();
            this.pnlfinding.SuspendLayout();
            this.pnlleft.SuspendLayout();
            this.Panel4.SuspendLayout();
            this.Panel5.SuspendLayout();
            this.pnlSMMain.SuspendLayout();
            this.pnlright.SuspendLayout();
            this.pnlBottom.SuspendLayout();
            this.panel13.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel15.SuspendLayout();
            this.SuspendLayout();
            // 
            // trvSnoMed
            // 
            this.trvSnoMed.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.trvSnoMed.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trvSnoMed.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.trvSnoMed.HideSelection = false;
            this.trvSnoMed.ItemHeight = 19;
            this.trvSnoMed.Location = new System.Drawing.Point(5, 9);
            this.trvSnoMed.Name = "trvSnoMed";
            this.trvSnoMed.Size = new System.Drawing.Size(319, 691);
            this.trvSnoMed.TabIndex = 3;
            // 
            // lblSearchby
            // 
            this.lblSearchby.AutoSize = true;
            this.lblSearchby.BackColor = System.Drawing.Color.Transparent;
            this.lblSearchby.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSearchby.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblSearchby.Location = new System.Drawing.Point(27, 41);
            this.lblSearchby.Name = "lblSearchby";
            this.lblSearchby.Size = new System.Drawing.Size(69, 14);
            this.lblSearchby.TabIndex = 37;
            this.lblSearchby.Text = "Search By :";
            this.lblSearchby.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtDescriptionID
            // 
            this.txtDescriptionID.Location = new System.Drawing.Point(144, 11);
            this.txtDescriptionID.Name = "txtDescriptionID";
            this.txtDescriptionID.Size = new System.Drawing.Size(181, 21);
            this.txtDescriptionID.TabIndex = 2;
            this.txtDescriptionID.Visible = false;
            // 
            // tls_SM
            // 
            this.tls_SM.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.tls_SM.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tls_SM.BackgroundImage")));
            this.tls_SM.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tls_SM.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tls_SM.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tls_SM.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tlb_SMSave,
            this.tlb_SMClose});
            this.tls_SM.Location = new System.Drawing.Point(0, 0);
            this.tls_SM.Name = "tls_SM";
            this.tls_SM.Size = new System.Drawing.Size(1272, 53);
            this.tls_SM.TabIndex = 1;
            this.tls_SM.Text = "ToolStrip1";
            this.tls_SM.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.tls_SM_ItemClicked);
            // 
            // tlb_SMSave
            // 
            this.tlb_SMSave.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlb_SMSave.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tlb_SMSave.Image = ((System.Drawing.Image)(resources.GetObject("tlb_SMSave.Image")));
            this.tlb_SMSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlb_SMSave.Name = "tlb_SMSave";
            this.tlb_SMSave.Size = new System.Drawing.Size(66, 50);
            this.tlb_SMSave.Tag = "Save";
            this.tlb_SMSave.Text = "&Save&&Cls";
            this.tlb_SMSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlb_SMSave.ToolTipText = "Save and Close";
            // 
            // tlb_SMClose
            // 
            this.tlb_SMClose.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlb_SMClose.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tlb_SMClose.Image = ((System.Drawing.Image)(resources.GetObject("tlb_SMClose.Image")));
            this.tlb_SMClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlb_SMClose.Name = "tlb_SMClose";
            this.tlb_SMClose.Size = new System.Drawing.Size(43, 50);
            this.tlb_SMClose.Tag = "Close";
            this.tlb_SMClose.Text = "&Close";
            this.tlb_SMClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlb_SMClose.ToolTipText = "Close";
            // 
            // trvSubtype
            // 
            this.trvSubtype.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trvSubtype.Location = new System.Drawing.Point(0, 24);
            this.trvSubtype.Name = "trvSubtype";
            this.trvSubtype.Size = new System.Drawing.Size(25, 21);
            this.trvSubtype.TabIndex = 2;
            // 
            // pnlsubType
            // 
            this.pnlsubType.Controls.Add(this.trvSubtype);
            this.pnlsubType.Controls.Add(this.Panel3);
            this.pnlsubType.Location = new System.Drawing.Point(576, 2);
            this.pnlsubType.Name = "pnlsubType";
            this.pnlsubType.Size = new System.Drawing.Size(25, 45);
            this.pnlsubType.TabIndex = 3;
            this.pnlsubType.Visible = false;
            // 
            // Panel3
            // 
            this.Panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Panel3.Controls.Add(this.Label27);
            this.Panel3.Controls.Add(this.Label28);
            this.Panel3.Controls.Add(this.Label29);
            this.Panel3.Controls.Add(this.Label30);
            this.Panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.Panel3.Location = new System.Drawing.Point(0, 0);
            this.Panel3.Name = "Panel3";
            this.Panel3.Size = new System.Drawing.Size(25, 24);
            this.Panel3.TabIndex = 6;
            // 
            // Label27
            // 
            this.Label27.BackColor = System.Drawing.Color.Transparent;
            this.Label27.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Label27.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label27.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label27.Location = new System.Drawing.Point(1, 1);
            this.Label27.Name = "Label27";
            this.Label27.Size = new System.Drawing.Size(23, 23);
            this.Label27.TabIndex = 31;
            this.Label27.Text = "  Sub Type";
            this.Label27.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label28
            // 
            this.Label28.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label28.Dock = System.Windows.Forms.DockStyle.Left;
            this.Label28.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label28.Location = new System.Drawing.Point(0, 1);
            this.Label28.Name = "Label28";
            this.Label28.Size = new System.Drawing.Size(1, 23);
            this.Label28.TabIndex = 9;
            this.Label28.Text = "label1";
            // 
            // Label29
            // 
            this.Label29.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label29.Dock = System.Windows.Forms.DockStyle.Right;
            this.Label29.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label29.Location = new System.Drawing.Point(24, 1);
            this.Label29.Name = "Label29";
            this.Label29.Size = new System.Drawing.Size(1, 23);
            this.Label29.TabIndex = 8;
            this.Label29.Text = "label1";
            // 
            // Label30
            // 
            this.Label30.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label30.Dock = System.Windows.Forms.DockStyle.Top;
            this.Label30.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label30.Location = new System.Drawing.Point(0, 0);
            this.Label30.Name = "Label30";
            this.Label30.Size = new System.Drawing.Size(25, 1);
            this.Label30.TabIndex = 6;
            this.Label30.Text = "label1";
            // 
            // txtConceptID
            // 
            this.txtConceptID.Location = new System.Drawing.Point(348, 11);
            this.txtConceptID.Name = "txtConceptID";
            this.txtConceptID.Size = new System.Drawing.Size(181, 21);
            this.txtConceptID.TabIndex = 2;
            this.txtConceptID.Visible = false;
            // 
            // lblConceptID
            // 
            this.lblConceptID.AutoEllipsis = true;
            this.lblConceptID.BackColor = System.Drawing.Color.Transparent;
            this.lblConceptID.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblConceptID.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblConceptID.Location = new System.Drawing.Point(97, 66);
            this.lblConceptID.Name = "lblConceptID";
            this.lblConceptID.Size = new System.Drawing.Size(1100, 14);
            this.lblConceptID.TabIndex = 36;
            this.lblConceptID.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label7
            // 
            this.Label7.AutoSize = true;
            this.Label7.BackColor = System.Drawing.Color.Transparent;
            this.Label7.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label7.Location = new System.Drawing.Point(14, 66);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(82, 14);
            this.Label7.TabIndex = 35;
            this.Label7.Text = "SNOMED-CT :";
            this.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnltlstripSM
            // 
            this.pnltlstripSM.Controls.Add(this.Panel7);
            this.pnltlstripSM.Controls.Add(this.txtConceptID);
            this.pnltlstripSM.Controls.Add(this.txtDescriptionID);
            this.pnltlstripSM.Controls.Add(this.tls_SM);
            this.pnltlstripSM.Controls.Add(this.pnlsubType);
            this.pnltlstripSM.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnltlstripSM.Location = new System.Drawing.Point(0, 0);
            this.pnltlstripSM.Name = "pnltlstripSM";
            this.pnltlstripSM.Size = new System.Drawing.Size(1272, 55);
            this.pnltlstripSM.TabIndex = 0;
            // 
            // Panel7
            // 
            this.Panel7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Panel7.Controls.Add(this.Panel8);
            this.Panel7.Location = new System.Drawing.Point(561, 7);
            this.Panel7.Name = "Panel7";
            this.Panel7.Size = new System.Drawing.Size(649, 24);
            this.Panel7.TabIndex = 34;
            this.Panel7.Visible = false;
            // 
            // Panel8
            // 
            this.Panel8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.Panel8.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Panel8.Controls.Add(this.lblDescriptionID);
            this.Panel8.Controls.Add(this.Label9);
            this.Panel8.Controls.Add(this.lblSnoMedID);
            this.Panel8.Controls.Add(this.Label2);
            this.Panel8.Controls.Add(this.Label1);
            this.Panel8.Controls.Add(this.Label4);
            this.Panel8.Controls.Add(this.Label5);
            this.Panel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Panel8.Location = new System.Drawing.Point(0, 0);
            this.Panel8.Name = "Panel8";
            this.Panel8.Size = new System.Drawing.Size(649, 24);
            this.Panel8.TabIndex = 32;
            // 
            // lblDescriptionID
            // 
            this.lblDescriptionID.AutoSize = true;
            this.lblDescriptionID.BackColor = System.Drawing.Color.Transparent;
            this.lblDescriptionID.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescriptionID.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblDescriptionID.Location = new System.Drawing.Point(94, 5);
            this.lblDescriptionID.Name = "lblDescriptionID";
            this.lblDescriptionID.Size = new System.Drawing.Size(0, 14);
            this.lblDescriptionID.TabIndex = 38;
            this.lblDescriptionID.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label9
            // 
            this.Label9.AutoSize = true;
            this.Label9.BackColor = System.Drawing.Color.Transparent;
            this.Label9.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label9.Location = new System.Drawing.Point(6, 5);
            this.Label9.Name = "Label9";
            this.Label9.Size = new System.Drawing.Size(91, 14);
            this.Label9.TabIndex = 37;
            this.Label9.Text = "Description ID :";
            this.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblSnoMedID
            // 
            this.lblSnoMedID.AutoSize = true;
            this.lblSnoMedID.BackColor = System.Drawing.Color.Transparent;
            this.lblSnoMedID.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSnoMedID.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblSnoMedID.Location = new System.Drawing.Point(284, 5);
            this.lblSnoMedID.Name = "lblSnoMedID";
            this.lblSnoMedID.Size = new System.Drawing.Size(0, 14);
            this.lblSnoMedID.TabIndex = 34;
            this.lblSnoMedID.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.BackColor = System.Drawing.Color.Transparent;
            this.Label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label2.Location = new System.Drawing.Point(205, 5);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(83, 14);
            this.Label2.TabIndex = 33;
            this.Label2.Text = "  SnoMed ID :";
            this.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label1
            // 
            this.Label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label1.Location = new System.Drawing.Point(1, 23);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(647, 1);
            this.Label1.TabIndex = 32;
            this.Label1.Text = "label1";
            // 
            // Label4
            // 
            this.Label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label4.Dock = System.Windows.Forms.DockStyle.Right;
            this.Label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label4.Location = new System.Drawing.Point(648, 0);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(1, 24);
            this.Label4.TabIndex = 8;
            this.Label4.Text = "label1";
            // 
            // Label5
            // 
            this.Label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label5.Dock = System.Windows.Forms.DockStyle.Left;
            this.Label5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label5.Location = new System.Drawing.Point(0, 0);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(1, 24);
            this.Label5.TabIndex = 9;
            this.Label5.Text = "label1";
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnlHeader.Controls.Add(this.panel16);
            this.pnlHeader.Controls.Add(this.panel9);
            this.pnlHeader.Controls.Add(this.label10);
            this.pnlHeader.Controls.Add(this.label8);
            this.pnlHeader.Controls.Add(this.label6);
            this.pnlHeader.Controls.Add(this.label3);
            this.pnlHeader.Controls.Add(this.lblConceptID);
            this.pnlHeader.Controls.Add(this.Label7);
            this.pnlHeader.Controls.Add(this.chkCOREProblem);
            this.pnlHeader.Controls.Add(this.lblSearchby);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 55);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Padding = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.pnlHeader.Size = new System.Drawing.Size(1272, 88);
            this.pnlHeader.TabIndex = 4;
            // 
            // panel16
            // 
            this.panel16.Controls.Add(this.RbConceptID);
            this.panel16.Controls.Add(this.RbICD9);
            this.panel16.Controls.Add(this.RbICD10);
            this.panel16.Location = new System.Drawing.Point(97, 37);
            this.panel16.Name = "panel16";
            this.panel16.Size = new System.Drawing.Size(352, 26);
            this.panel16.TabIndex = 49;
            // 
            // RbConceptID
            // 
            this.RbConceptID.AutoSize = true;
            this.RbConceptID.BackColor = System.Drawing.Color.Transparent;
            this.RbConceptID.Checked = true;
            this.RbConceptID.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RbConceptID.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.RbConceptID.Location = new System.Drawing.Point(3, 4);
            this.RbConceptID.Name = "RbConceptID";
            this.RbConceptID.Size = new System.Drawing.Size(92, 18);
            this.RbConceptID.TabIndex = 39;
            this.RbConceptID.TabStop = true;
            this.RbConceptID.Tag = "ConceptID";
            this.RbConceptID.Text = "SNOMED-CT";
            this.RbConceptID.UseVisualStyleBackColor = false;
            this.RbConceptID.CheckedChanged += new System.EventHandler(this.RbConceptID_CheckedChanged);
            this.RbConceptID.Click += new System.EventHandler(this.RbConceptID_Click);
            // 
            // RbICD9
            // 
            this.RbICD9.AutoSize = true;
            this.RbICD9.BackColor = System.Drawing.Color.Transparent;
            this.RbICD9.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RbICD9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.RbICD9.Location = new System.Drawing.Point(111, 4);
            this.RbICD9.Name = "RbICD9";
            this.RbICD9.Size = new System.Drawing.Size(51, 18);
            this.RbICD9.TabIndex = 40;
            this.RbICD9.Tag = "ICD9";
            this.RbICD9.Text = "ICD9";
            this.RbICD9.UseVisualStyleBackColor = false;
            this.RbICD9.CheckedChanged += new System.EventHandler(this.RbICD9_CheckedChanged);
            this.RbICD9.Click += new System.EventHandler(this.RbICD9_Click);
            // 
            // RbICD10
            // 
            this.RbICD10.AutoSize = true;
            this.RbICD10.BackColor = System.Drawing.Color.Transparent;
            this.RbICD10.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RbICD10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.RbICD10.Location = new System.Drawing.Point(176, 4);
            this.RbICD10.Name = "RbICD10";
            this.RbICD10.Size = new System.Drawing.Size(58, 18);
            this.RbICD10.TabIndex = 41;
            this.RbICD10.Tag = "ICD10";
            this.RbICD10.Text = "ICD10";
            this.RbICD10.UseVisualStyleBackColor = false;
            this.RbICD10.CheckedChanged += new System.EventHandler(this.RbICD10_CheckedChanged);
            this.RbICD10.Click += new System.EventHandler(this.RbICD10_Click);
            // 
            // panel9
            // 
            this.panel9.Controls.Add(this.btnAdvanceDef);
            this.panel9.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel9.Location = new System.Drawing.Point(1212, 4);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(56, 83);
            this.panel9.TabIndex = 44;
            // 
            // btnAdvanceDef
            // 
            this.btnAdvanceDef.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAdvanceDef.BackgroundImage")));
            this.btnAdvanceDef.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAdvanceDef.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdvanceDef.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdvanceDef.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnAdvanceDef.Image = ((System.Drawing.Image)(resources.GetObject("btnAdvanceDef.Image")));
            this.btnAdvanceDef.Location = new System.Drawing.Point(19, 24);
            this.btnAdvanceDef.Name = "btnAdvanceDef";
            this.btnAdvanceDef.Size = new System.Drawing.Size(29, 27);
            this.btnAdvanceDef.TabIndex = 43;
            this.btnAdvanceDef.UseVisualStyleBackColor = true;
            this.btnAdvanceDef.Click += new System.EventHandler(this.btnAdvanceDef_Click);
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label10.Dock = System.Windows.Forms.DockStyle.Right;
            this.label10.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(1268, 4);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(1, 83);
            this.label10.TabIndex = 48;
            this.label10.Text = "label1";
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label8.Dock = System.Windows.Forms.DockStyle.Left;
            this.label8.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(3, 4);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(1, 83);
            this.label8.TabIndex = 47;
            this.label8.Text = "label1";
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label6.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(3, 87);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(1266, 1);
            this.label6.TabIndex = 46;
            this.label6.Text = "label1";
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Top;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(3, 3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(1266, 1);
            this.label3.TabIndex = 45;
            this.label3.Text = "label1";
            // 
            // chkCOREProblem
            // 
            this.chkCOREProblem.AutoSize = true;
            this.chkCOREProblem.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkCOREProblem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.chkCOREProblem.Location = new System.Drawing.Point(98, 11);
            this.chkCOREProblem.Name = "chkCOREProblem";
            this.chkCOREProblem.Size = new System.Drawing.Size(98, 18);
            this.chkCOREProblem.TabIndex = 42;
            this.chkCOREProblem.Text = "CORE Subset";
            this.chkCOREProblem.UseVisualStyleBackColor = true;
            this.chkCOREProblem.CheckedChanged += new System.EventHandler(this.chkCOREProblem_CheckedChanged);
            // 
            // btnClear
            // 
            this.btnClear.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClear.BackgroundImage")));
            this.btnClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClear.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClear.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnClear.FlatAppearance.BorderSize = 0;
            this.btnClear.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnClear.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClear.Image = ((System.Drawing.Image)(resources.GetObject("btnClear.Image")));
            this.btnClear.Location = new System.Drawing.Point(630, 1);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(24, 26);
            this.btnClear.TabIndex = 41;
            this.tlAddFields.SetToolTip(this.btnClear, "Clear");
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // mnuFindings
            // 
            this.mnuFindings.Name = "mnuFindings";
            this.mnuFindings.Size = new System.Drawing.Size(144, 22);
            this.mnuFindings.Text = "Add Findings";
            this.mnuFindings.Click += new System.EventHandler(this.mnuFindings_Click);
            // 
            // cntFindings
            // 
            this.cntFindings.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFindings});
            this.cntFindings.Name = "cntFindings";
            this.cntFindings.Size = new System.Drawing.Size(145, 26);
            // 
            // trICD9
            // 
            this.trICD9.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.trICD9.CheckBoxes = true;
            this.trICD9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trICD9.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.trICD9.HideSelection = false;
            this.trICD9.ItemHeight = 19;
            this.trICD9.Location = new System.Drawing.Point(5, 9);
            this.trICD9.Name = "trICD9";
            this.trICD9.ShowNodeToolTips = true;
            this.trICD9.Size = new System.Drawing.Size(275, 285);
            this.trICD9.TabIndex = 4;
            this.trICD9.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.trICD9_AfterCheck);
            // 
            // pnlSMSearch
            // 
            this.pnlSMSearch.BackColor = System.Drawing.Color.White;
            this.pnlSMSearch.Controls.Add(this.txtSMSearch);
            this.pnlSMSearch.Controls.Add(this.lbl_WhiteSpaceTop);
            this.pnlSMSearch.Controls.Add(this.lbl_WhiteSpaceBottom);
            this.pnlSMSearch.Controls.Add(this.btnClear);
            this.pnlSMSearch.Controls.Add(this.PicBx_Search);
            this.pnlSMSearch.Controls.Add(this.lbl_pnlSearchBottomBrd);
            this.pnlSMSearch.Controls.Add(this.lbl_pnlSearchTopBrd);
            this.pnlSMSearch.Controls.Add(this.lbl_pnlSearchLeftBrd);
            this.pnlSMSearch.Controls.Add(this.lbl_pnlSearchRightBrd);
            this.pnlSMSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSMSearch.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlSMSearch.ForeColor = System.Drawing.Color.Black;
            this.pnlSMSearch.Location = new System.Drawing.Point(3, 30);
            this.pnlSMSearch.Name = "pnlSMSearch";
            this.pnlSMSearch.Size = new System.Drawing.Size(655, 28);
            this.pnlSMSearch.TabIndex = 28;
            // 
            // txtSMSearch
            // 
            this.txtSMSearch.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSMSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSMSearch.Location = new System.Drawing.Point(34, 4);
            this.txtSMSearch.Name = "txtSMSearch";
            this.txtSMSearch.Size = new System.Drawing.Size(596, 15);
            this.txtSMSearch.TabIndex = 42;
            this.txtSMSearch.SearchFired += new gloSnoMed.gloSearchTextBox.SearchFiredEventHandler(this.txtSMSearch_SearchFired);
            this.txtSMSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSMSearch_KeyPress);
            // 
            // lbl_WhiteSpaceTop
            // 
            this.lbl_WhiteSpaceTop.BackColor = System.Drawing.Color.White;
            this.lbl_WhiteSpaceTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_WhiteSpaceTop.Location = new System.Drawing.Point(34, 1);
            this.lbl_WhiteSpaceTop.Name = "lbl_WhiteSpaceTop";
            this.lbl_WhiteSpaceTop.Size = new System.Drawing.Size(596, 3);
            this.lbl_WhiteSpaceTop.TabIndex = 37;
            // 
            // lbl_WhiteSpaceBottom
            // 
            this.lbl_WhiteSpaceBottom.BackColor = System.Drawing.Color.White;
            this.lbl_WhiteSpaceBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_WhiteSpaceBottom.Location = new System.Drawing.Point(34, 22);
            this.lbl_WhiteSpaceBottom.Name = "lbl_WhiteSpaceBottom";
            this.lbl_WhiteSpaceBottom.Size = new System.Drawing.Size(596, 5);
            this.lbl_WhiteSpaceBottom.TabIndex = 38;
            // 
            // PicBx_Search
            // 
            this.PicBx_Search.BackColor = System.Drawing.Color.White;
            this.PicBx_Search.Dock = System.Windows.Forms.DockStyle.Left;
            this.PicBx_Search.Image = ((System.Drawing.Image)(resources.GetObject("PicBx_Search.Image")));
            this.PicBx_Search.Location = new System.Drawing.Point(1, 1);
            this.PicBx_Search.Name = "PicBx_Search";
            this.PicBx_Search.Size = new System.Drawing.Size(33, 26);
            this.PicBx_Search.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PicBx_Search.TabIndex = 9;
            this.PicBx_Search.TabStop = false;
            // 
            // lbl_pnlSearchBottomBrd
            // 
            this.lbl_pnlSearchBottomBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlSearchBottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_pnlSearchBottomBrd.Location = new System.Drawing.Point(1, 27);
            this.lbl_pnlSearchBottomBrd.Name = "lbl_pnlSearchBottomBrd";
            this.lbl_pnlSearchBottomBrd.Size = new System.Drawing.Size(653, 1);
            this.lbl_pnlSearchBottomBrd.TabIndex = 35;
            this.lbl_pnlSearchBottomBrd.Text = "label1";
            // 
            // lbl_pnlSearchTopBrd
            // 
            this.lbl_pnlSearchTopBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlSearchTopBrd.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_pnlSearchTopBrd.Location = new System.Drawing.Point(1, 0);
            this.lbl_pnlSearchTopBrd.Name = "lbl_pnlSearchTopBrd";
            this.lbl_pnlSearchTopBrd.Size = new System.Drawing.Size(653, 1);
            this.lbl_pnlSearchTopBrd.TabIndex = 36;
            this.lbl_pnlSearchTopBrd.Text = "label1";
            // 
            // lbl_pnlSearchLeftBrd
            // 
            this.lbl_pnlSearchLeftBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlSearchLeftBrd.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_pnlSearchLeftBrd.Location = new System.Drawing.Point(0, 0);
            this.lbl_pnlSearchLeftBrd.Name = "lbl_pnlSearchLeftBrd";
            this.lbl_pnlSearchLeftBrd.Size = new System.Drawing.Size(1, 28);
            this.lbl_pnlSearchLeftBrd.TabIndex = 39;
            this.lbl_pnlSearchLeftBrd.Text = "label4";
            // 
            // lbl_pnlSearchRightBrd
            // 
            this.lbl_pnlSearchRightBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlSearchRightBrd.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_pnlSearchRightBrd.Location = new System.Drawing.Point(654, 0);
            this.lbl_pnlSearchRightBrd.Name = "lbl_pnlSearchRightBrd";
            this.lbl_pnlSearchRightBrd.Size = new System.Drawing.Size(1, 28);
            this.lbl_pnlSearchRightBrd.TabIndex = 40;
            this.lbl_pnlSearchRightBrd.Text = "label4";
            // 
            // pnlICD9
            // 
            this.pnlICD9.Controls.Add(this.panel1);
            this.pnlICD9.Controls.Add(this.panel10);
            this.pnlICD9.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlICD9.Location = new System.Drawing.Point(0, 3);
            this.pnlICD9.Name = "pnlICD9";
            this.pnlICD9.Size = new System.Drawing.Size(281, 319);
            this.pnlICD9.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.trICD9);
            this.panel1.Controls.Add(this.label19);
            this.panel1.Controls.Add(this.label21);
            this.panel1.Controls.Add(this.label22);
            this.panel1.Controls.Add(this.label43);
            this.panel1.Controls.Add(this.label47);
            this.panel1.Controls.Add(this.label48);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 24);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.panel1.Size = new System.Drawing.Size(281, 295);
            this.panel1.TabIndex = 32;
            // 
            // label19
            // 
            this.label19.BackColor = System.Drawing.Color.White;
            this.label19.Dock = System.Windows.Forms.DockStyle.Left;
            this.label19.Location = new System.Drawing.Point(1, 9);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(4, 285);
            this.label19.TabIndex = 43;
            // 
            // label21
            // 
            this.label21.BackColor = System.Drawing.Color.White;
            this.label21.Dock = System.Windows.Forms.DockStyle.Top;
            this.label21.Location = new System.Drawing.Point(1, 4);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(279, 5);
            this.label21.TabIndex = 42;
            // 
            // label22
            // 
            this.label22.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label22.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label22.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.Location = new System.Drawing.Point(1, 294);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(279, 1);
            this.label22.TabIndex = 41;
            this.label22.Text = "label1";
            // 
            // label43
            // 
            this.label43.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label43.Dock = System.Windows.Forms.DockStyle.Top;
            this.label43.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label43.Location = new System.Drawing.Point(1, 3);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(279, 1);
            this.label43.TabIndex = 45;
            this.label43.Text = "label1";
            // 
            // label47
            // 
            this.label47.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label47.Dock = System.Windows.Forms.DockStyle.Left;
            this.label47.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label47.Location = new System.Drawing.Point(0, 3);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(1, 292);
            this.label47.TabIndex = 46;
            this.label47.Text = "label1";
            // 
            // label48
            // 
            this.label48.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label48.Dock = System.Windows.Forms.DockStyle.Right;
            this.label48.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label48.Location = new System.Drawing.Point(280, 3);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(1, 292);
            this.label48.TabIndex = 44;
            this.label48.Text = "label1";
            // 
            // panel10
            // 
            this.panel10.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel10.Controls.Add(this.panel11);
            this.panel10.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel10.Location = new System.Drawing.Point(0, 0);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(281, 24);
            this.panel10.TabIndex = 31;
            // 
            // panel11
            // 
            this.panel11.BackColor = System.Drawing.Color.Transparent;
            this.panel11.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel11.BackgroundImage")));
            this.panel11.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel11.Controls.Add(this.Label23);
            this.panel11.Controls.Add(this.label42);
            this.panel11.Controls.Add(this.label44);
            this.panel11.Controls.Add(this.label45);
            this.panel11.Controls.Add(this.label46);
            this.panel11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel11.Location = new System.Drawing.Point(0, 0);
            this.panel11.Name = "panel11";
            this.panel11.Size = new System.Drawing.Size(281, 24);
            this.panel11.TabIndex = 32;
            // 
            // Label23
            // 
            this.Label23.BackColor = System.Drawing.Color.Transparent;
            this.Label23.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Label23.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label23.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label23.Location = new System.Drawing.Point(1, 1);
            this.Label23.Name = "Label23";
            this.Label23.Size = new System.Drawing.Size(279, 22);
            this.Label23.TabIndex = 33;
            this.Label23.Text = "  ICD9";
            this.Label23.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label42
            // 
            this.label42.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label42.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label42.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label42.Location = new System.Drawing.Point(1, 23);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(279, 1);
            this.label42.TabIndex = 32;
            this.label42.Text = "label1";
            // 
            // label44
            // 
            this.label44.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label44.Dock = System.Windows.Forms.DockStyle.Top;
            this.label44.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label44.Location = new System.Drawing.Point(1, 0);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(279, 1);
            this.label44.TabIndex = 6;
            this.label44.Text = "label1";
            // 
            // label45
            // 
            this.label45.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label45.Dock = System.Windows.Forms.DockStyle.Right;
            this.label45.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label45.Location = new System.Drawing.Point(280, 0);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(1, 24);
            this.label45.TabIndex = 8;
            this.label45.Text = "label1";
            // 
            // label46
            // 
            this.label46.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label46.Dock = System.Windows.Forms.DockStyle.Left;
            this.label46.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label46.Location = new System.Drawing.Point(0, 0);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(1, 24);
            this.label46.TabIndex = 9;
            this.label46.Text = "label1";
            // 
            // trvFindings
            // 
            this.trvFindings.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.trvFindings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trvFindings.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.trvFindings.HideSelection = false;
            this.trvFindings.ItemHeight = 19;
            this.trvFindings.Location = new System.Drawing.Point(5, 9);
            this.trvFindings.Name = "trvFindings";
            this.trvFindings.Size = new System.Drawing.Size(649, 660);
            this.trvFindings.TabIndex = 2;
            this.trvFindings.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.trvFindings_BeforeExpand);
            this.trvFindings.BeforeSelect += new System.Windows.Forms.TreeViewCancelEventHandler(this.trvFindings_BeforeSelect);
            this.trvFindings.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.trvFindings_AfterSelect);
            // 
            // pnlmiddle
            // 
            this.pnlmiddle.Controls.Add(this.panel12);
            this.pnlmiddle.Controls.Add(this.panel6);
            this.pnlmiddle.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlmiddle.Location = new System.Drawing.Point(947, 143);
            this.pnlmiddle.Name = "pnlmiddle";
            this.pnlmiddle.Padding = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.pnlmiddle.Size = new System.Drawing.Size(325, 731);
            this.pnlmiddle.TabIndex = 3;
            this.pnlmiddle.Visible = false;
            // 
            // panel12
            // 
            this.panel12.Controls.Add(this.trvSnoMed);
            this.panel12.Controls.Add(this.label49);
            this.panel12.Controls.Add(this.label50);
            this.panel12.Controls.Add(this.label51);
            this.panel12.Controls.Add(this.label52);
            this.panel12.Controls.Add(this.label53);
            this.panel12.Controls.Add(this.label54);
            this.panel12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel12.Location = new System.Drawing.Point(0, 27);
            this.panel12.Name = "panel12";
            this.panel12.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.panel12.Size = new System.Drawing.Size(325, 701);
            this.panel12.TabIndex = 33;
            // 
            // label49
            // 
            this.label49.BackColor = System.Drawing.Color.White;
            this.label49.Dock = System.Windows.Forms.DockStyle.Left;
            this.label49.Location = new System.Drawing.Point(1, 9);
            this.label49.Name = "label49";
            this.label49.Size = new System.Drawing.Size(4, 691);
            this.label49.TabIndex = 43;
            // 
            // label50
            // 
            this.label50.BackColor = System.Drawing.Color.White;
            this.label50.Dock = System.Windows.Forms.DockStyle.Top;
            this.label50.Location = new System.Drawing.Point(1, 4);
            this.label50.Name = "label50";
            this.label50.Size = new System.Drawing.Size(323, 5);
            this.label50.TabIndex = 42;
            // 
            // label51
            // 
            this.label51.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label51.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label51.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label51.Location = new System.Drawing.Point(1, 700);
            this.label51.Name = "label51";
            this.label51.Size = new System.Drawing.Size(323, 1);
            this.label51.TabIndex = 41;
            this.label51.Text = "label1";
            // 
            // label52
            // 
            this.label52.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label52.Dock = System.Windows.Forms.DockStyle.Top;
            this.label52.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label52.Location = new System.Drawing.Point(1, 3);
            this.label52.Name = "label52";
            this.label52.Size = new System.Drawing.Size(323, 1);
            this.label52.TabIndex = 45;
            this.label52.Text = "label1";
            // 
            // label53
            // 
            this.label53.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label53.Dock = System.Windows.Forms.DockStyle.Left;
            this.label53.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label53.Location = new System.Drawing.Point(0, 3);
            this.label53.Name = "label53";
            this.label53.Size = new System.Drawing.Size(1, 698);
            this.label53.TabIndex = 46;
            this.label53.Text = "label1";
            // 
            // label54
            // 
            this.label54.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label54.Dock = System.Windows.Forms.DockStyle.Right;
            this.label54.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label54.Location = new System.Drawing.Point(324, 3);
            this.label54.Name = "label54";
            this.label54.Size = new System.Drawing.Size(1, 698);
            this.label54.TabIndex = 44;
            this.label54.Text = "label1";
            // 
            // panel6
            // 
            this.panel6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel6.Controls.Add(this.panel14);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel6.Location = new System.Drawing.Point(0, 3);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(325, 24);
            this.panel6.TabIndex = 35;
            // 
            // panel14
            // 
            this.panel14.BackColor = System.Drawing.Color.Transparent;
            this.panel14.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel14.BackgroundImage")));
            this.panel14.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel14.Controls.Add(this.Label36);
            this.panel14.Controls.Add(this.label38);
            this.panel14.Controls.Add(this.label39);
            this.panel14.Controls.Add(this.label61);
            this.panel14.Controls.Add(this.label62);
            this.panel14.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel14.Location = new System.Drawing.Point(0, 0);
            this.panel14.Name = "panel14";
            this.panel14.Size = new System.Drawing.Size(325, 24);
            this.panel14.TabIndex = 32;
            // 
            // Label36
            // 
            this.Label36.BackColor = System.Drawing.Color.Transparent;
            this.Label36.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Label36.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label36.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label36.Location = new System.Drawing.Point(1, 1);
            this.Label36.Name = "Label36";
            this.Label36.Size = new System.Drawing.Size(323, 22);
            this.Label36.TabIndex = 31;
            this.Label36.Text = " Definition ";
            this.Label36.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label38
            // 
            this.label38.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label38.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label38.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label38.Location = new System.Drawing.Point(1, 23);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(323, 1);
            this.label38.TabIndex = 32;
            this.label38.Text = "label1";
            // 
            // label39
            // 
            this.label39.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label39.Dock = System.Windows.Forms.DockStyle.Top;
            this.label39.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label39.Location = new System.Drawing.Point(1, 0);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(323, 1);
            this.label39.TabIndex = 6;
            this.label39.Text = "label1";
            // 
            // label61
            // 
            this.label61.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label61.Dock = System.Windows.Forms.DockStyle.Right;
            this.label61.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label61.Location = new System.Drawing.Point(324, 0);
            this.label61.Name = "label61";
            this.label61.Size = new System.Drawing.Size(1, 24);
            this.label61.TabIndex = 8;
            this.label61.Text = "label1";
            // 
            // label62
            // 
            this.label62.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label62.Dock = System.Windows.Forms.DockStyle.Left;
            this.label62.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label62.Location = new System.Drawing.Point(0, 0);
            this.label62.Name = "label62";
            this.label62.Size = new System.Drawing.Size(1, 24);
            this.label62.TabIndex = 9;
            this.label62.Text = "label1";
            // 
            // pnlfinding
            // 
            this.pnlfinding.Controls.Add(this.trvFindings);
            this.pnlfinding.Controls.Add(this.label41);
            this.pnlfinding.Controls.Add(this.label12);
            this.pnlfinding.Controls.Add(this.label11);
            this.pnlfinding.Controls.Add(this.label13);
            this.pnlfinding.Controls.Add(this.label40);
            this.pnlfinding.Controls.Add(this.label14);
            this.pnlfinding.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlfinding.Location = new System.Drawing.Point(3, 58);
            this.pnlfinding.Name = "pnlfinding";
            this.pnlfinding.Padding = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.pnlfinding.Size = new System.Drawing.Size(655, 673);
            this.pnlfinding.TabIndex = 0;
            // 
            // label41
            // 
            this.label41.BackColor = System.Drawing.Color.White;
            this.label41.Dock = System.Windows.Forms.DockStyle.Left;
            this.label41.Location = new System.Drawing.Point(1, 9);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(4, 660);
            this.label41.TabIndex = 43;
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.White;
            this.label12.Dock = System.Windows.Forms.DockStyle.Top;
            this.label12.Location = new System.Drawing.Point(1, 4);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(653, 5);
            this.label12.TabIndex = 42;
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label11.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label11.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(1, 669);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(653, 1);
            this.label11.TabIndex = 41;
            this.label11.Text = "label1";
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label13.Dock = System.Windows.Forms.DockStyle.Top;
            this.label13.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(1, 3);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(653, 1);
            this.label13.TabIndex = 45;
            this.label13.Text = "label1";
            // 
            // label40
            // 
            this.label40.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label40.Dock = System.Windows.Forms.DockStyle.Left;
            this.label40.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label40.Location = new System.Drawing.Point(0, 3);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(1, 667);
            this.label40.TabIndex = 46;
            this.label40.Text = "label1";
            // 
            // label14
            // 
            this.label14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label14.Dock = System.Windows.Forms.DockStyle.Right;
            this.label14.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(654, 3);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(1, 667);
            this.label14.TabIndex = 44;
            this.label14.Text = "label1";
            // 
            // pnlleft
            // 
            this.pnlleft.Controls.Add(this.pnlfinding);
            this.pnlleft.Controls.Add(this.pnlSMSearch);
            this.pnlleft.Controls.Add(this.Panel4);
            this.pnlleft.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlleft.Location = new System.Drawing.Point(0, 143);
            this.pnlleft.Name = "pnlleft";
            this.pnlleft.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.pnlleft.Size = new System.Drawing.Size(658, 731);
            this.pnlleft.TabIndex = 1;
            // 
            // Panel4
            // 
            this.Panel4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Panel4.Controls.Add(this.Panel5);
            this.Panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.Panel4.Location = new System.Drawing.Point(3, 0);
            this.Panel4.Name = "Panel4";
            this.Panel4.Padding = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.Panel4.Size = new System.Drawing.Size(655, 30);
            this.Panel4.TabIndex = 30;
            // 
            // Panel5
            // 
            this.Panel5.BackColor = System.Drawing.Color.Transparent;
            this.Panel5.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Panel5.BackgroundImage")));
            this.Panel5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Panel5.Controls.Add(this.Label35);
            this.Panel5.Controls.Add(this.Label31);
            this.Panel5.Controls.Add(this.Label34);
            this.Panel5.Controls.Add(this.Label33);
            this.Panel5.Controls.Add(this.Label32);
            this.Panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Panel5.Location = new System.Drawing.Point(0, 3);
            this.Panel5.Name = "Panel5";
            this.Panel5.Size = new System.Drawing.Size(655, 24);
            this.Panel5.TabIndex = 32;
            // 
            // Label35
            // 
            this.Label35.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label35.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Label35.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label35.Location = new System.Drawing.Point(1, 23);
            this.Label35.Name = "Label35";
            this.Label35.Size = new System.Drawing.Size(653, 1);
            this.Label35.TabIndex = 32;
            this.Label35.Text = "label1";
            // 
            // Label31
            // 
            this.Label31.BackColor = System.Drawing.Color.Transparent;
            this.Label31.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Label31.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label31.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label31.Location = new System.Drawing.Point(1, 1);
            this.Label31.Name = "Label31";
            this.Label31.Size = new System.Drawing.Size(653, 23);
            this.Label31.TabIndex = 31;
            this.Label31.Text = " Enter search text, then select SNOMED-CT";
            this.Label31.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label34
            // 
            this.Label34.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label34.Dock = System.Windows.Forms.DockStyle.Top;
            this.Label34.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label34.Location = new System.Drawing.Point(1, 0);
            this.Label34.Name = "Label34";
            this.Label34.Size = new System.Drawing.Size(653, 1);
            this.Label34.TabIndex = 6;
            this.Label34.Text = "label1";
            // 
            // Label33
            // 
            this.Label33.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label33.Dock = System.Windows.Forms.DockStyle.Right;
            this.Label33.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label33.Location = new System.Drawing.Point(654, 0);
            this.Label33.Name = "Label33";
            this.Label33.Size = new System.Drawing.Size(1, 24);
            this.Label33.TabIndex = 8;
            this.Label33.Text = "label1";
            // 
            // Label32
            // 
            this.Label32.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label32.Dock = System.Windows.Forms.DockStyle.Left;
            this.Label32.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label32.Location = new System.Drawing.Point(0, 0);
            this.Label32.Name = "Label32";
            this.Label32.Size = new System.Drawing.Size(1, 24);
            this.Label32.TabIndex = 9;
            this.Label32.Text = "label1";
            // 
            // pnlSMMain
            // 
            this.pnlSMMain.Controls.Add(this.pnlright);
            this.pnlSMMain.Controls.Add(this.splitter4);
            this.pnlSMMain.Controls.Add(this.splitter3);
            this.pnlSMMain.Controls.Add(this.pnlmiddle);
            this.pnlSMMain.Controls.Add(this.pnlleft);
            this.pnlSMMain.Controls.Add(this.pnlHeader);
            this.pnlSMMain.Controls.Add(this.pnltlstripSM);
            this.pnlSMMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSMMain.Location = new System.Drawing.Point(0, 0);
            this.pnlSMMain.Name = "pnlSMMain";
            this.pnlSMMain.Size = new System.Drawing.Size(1272, 874);
            this.pnlSMMain.TabIndex = 33;
            // 
            // pnlright
            // 
            this.pnlright.Controls.Add(this.pnlBottom);
            this.pnlright.Controls.Add(this.Splitter2);
            this.pnlright.Controls.Add(this.pnlICD9);
            this.pnlright.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlright.Location = new System.Drawing.Point(662, 143);
            this.pnlright.Name = "pnlright";
            this.pnlright.Padding = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.pnlright.Size = new System.Drawing.Size(281, 731);
            this.pnlright.TabIndex = 2;
            // 
            // pnlBottom
            // 
            this.pnlBottom.Controls.Add(this.panel13);
            this.pnlBottom.Controls.Add(this.panel2);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBottom.Location = new System.Drawing.Point(0, 325);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(281, 403);
            this.pnlBottom.TabIndex = 4;
            // 
            // panel13
            // 
            this.panel13.Controls.Add(this.trICD10);
            this.panel13.Controls.Add(this.label55);
            this.panel13.Controls.Add(this.label56);
            this.panel13.Controls.Add(this.label57);
            this.panel13.Controls.Add(this.label58);
            this.panel13.Controls.Add(this.label59);
            this.panel13.Controls.Add(this.label60);
            this.panel13.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel13.Location = new System.Drawing.Point(0, 24);
            this.panel13.Name = "panel13";
            this.panel13.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.panel13.Size = new System.Drawing.Size(281, 379);
            this.panel13.TabIndex = 34;
            // 
            // trICD10
            // 
            this.trICD10.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.trICD10.CheckBoxes = true;
            this.trICD10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trICD10.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.trICD10.HideSelection = false;
            this.trICD10.ItemHeight = 19;
            this.trICD10.Location = new System.Drawing.Point(5, 9);
            this.trICD10.Name = "trICD10";
            this.trICD10.Size = new System.Drawing.Size(275, 369);
            this.trICD10.TabIndex = 4;
            this.trICD10.BeforeCheck += new System.Windows.Forms.TreeViewCancelEventHandler(this.trICD10_BeforeCheck);
            // 
            // label55
            // 
            this.label55.BackColor = System.Drawing.Color.White;
            this.label55.Dock = System.Windows.Forms.DockStyle.Left;
            this.label55.Location = new System.Drawing.Point(1, 9);
            this.label55.Name = "label55";
            this.label55.Size = new System.Drawing.Size(4, 369);
            this.label55.TabIndex = 43;
            // 
            // label56
            // 
            this.label56.BackColor = System.Drawing.Color.White;
            this.label56.Dock = System.Windows.Forms.DockStyle.Top;
            this.label56.Location = new System.Drawing.Point(1, 4);
            this.label56.Name = "label56";
            this.label56.Size = new System.Drawing.Size(279, 5);
            this.label56.TabIndex = 42;
            // 
            // label57
            // 
            this.label57.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label57.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label57.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label57.Location = new System.Drawing.Point(1, 378);
            this.label57.Name = "label57";
            this.label57.Size = new System.Drawing.Size(279, 1);
            this.label57.TabIndex = 41;
            this.label57.Text = "label1";
            // 
            // label58
            // 
            this.label58.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label58.Dock = System.Windows.Forms.DockStyle.Top;
            this.label58.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label58.Location = new System.Drawing.Point(1, 3);
            this.label58.Name = "label58";
            this.label58.Size = new System.Drawing.Size(279, 1);
            this.label58.TabIndex = 45;
            this.label58.Text = "label1";
            // 
            // label59
            // 
            this.label59.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label59.Dock = System.Windows.Forms.DockStyle.Left;
            this.label59.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label59.Location = new System.Drawing.Point(0, 3);
            this.label59.Name = "label59";
            this.label59.Size = new System.Drawing.Size(1, 376);
            this.label59.TabIndex = 46;
            this.label59.Text = "label1";
            // 
            // label60
            // 
            this.label60.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label60.Dock = System.Windows.Forms.DockStyle.Right;
            this.label60.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label60.Location = new System.Drawing.Point(280, 3);
            this.label60.Name = "label60";
            this.label60.Size = new System.Drawing.Size(1, 376);
            this.label60.TabIndex = 44;
            this.label60.Text = "label1";
            // 
            // panel2
            // 
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel2.Controls.Add(this.panel15);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(281, 24);
            this.panel2.TabIndex = 36;
            // 
            // panel15
            // 
            this.panel15.BackColor = System.Drawing.Color.Transparent;
            this.panel15.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel15.BackgroundImage")));
            this.panel15.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel15.Controls.Add(this.label20);
            this.panel15.Controls.Add(this.label24);
            this.panel15.Controls.Add(this.label25);
            this.panel15.Controls.Add(this.label26);
            this.panel15.Controls.Add(this.label37);
            this.panel15.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel15.Location = new System.Drawing.Point(0, 0);
            this.panel15.Name = "panel15";
            this.panel15.Size = new System.Drawing.Size(281, 24);
            this.panel15.TabIndex = 32;
            // 
            // label20
            // 
            this.label20.BackColor = System.Drawing.Color.Transparent;
            this.label20.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label20.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label20.Location = new System.Drawing.Point(1, 1);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(279, 22);
            this.label20.TabIndex = 31;
            this.label20.Text = " ICD10";
            this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label24
            // 
            this.label24.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label24.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label24.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label24.Location = new System.Drawing.Point(1, 23);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(279, 1);
            this.label24.TabIndex = 32;
            this.label24.Text = "label1";
            // 
            // label25
            // 
            this.label25.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label25.Dock = System.Windows.Forms.DockStyle.Top;
            this.label25.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label25.Location = new System.Drawing.Point(1, 0);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(279, 1);
            this.label25.TabIndex = 6;
            this.label25.Text = "label1";
            // 
            // label26
            // 
            this.label26.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label26.Dock = System.Windows.Forms.DockStyle.Right;
            this.label26.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label26.Location = new System.Drawing.Point(280, 0);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(1, 24);
            this.label26.TabIndex = 8;
            this.label26.Text = "label1";
            // 
            // label37
            // 
            this.label37.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label37.Dock = System.Windows.Forms.DockStyle.Left;
            this.label37.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label37.Location = new System.Drawing.Point(0, 0);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(1, 24);
            this.label37.TabIndex = 9;
            this.label37.Text = "label1";
            // 
            // Splitter2
            // 
            this.Splitter2.Dock = System.Windows.Forms.DockStyle.Top;
            this.Splitter2.Location = new System.Drawing.Point(0, 322);
            this.Splitter2.Name = "Splitter2";
            this.Splitter2.Size = new System.Drawing.Size(281, 3);
            this.Splitter2.TabIndex = 3;
            this.Splitter2.TabStop = false;
            // 
            // splitter4
            // 
            this.splitter4.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitter4.Location = new System.Drawing.Point(943, 143);
            this.splitter4.Name = "splitter4";
            this.splitter4.Size = new System.Drawing.Size(4, 731);
            this.splitter4.TabIndex = 6;
            this.splitter4.TabStop = false;
            // 
            // splitter3
            // 
            this.splitter3.Location = new System.Drawing.Point(658, 143);
            this.splitter3.Name = "splitter3";
            this.splitter3.Size = new System.Drawing.Size(4, 731);
            this.splitter3.TabIndex = 5;
            this.splitter3.TabStop = false;
            // 
            // Timer1
            // 
            this.Timer1.Tick += new System.EventHandler(this.Timer1_Tick);
            // 
            // FrmSelectProblem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(1272, 874);
            this.Controls.Add(this.pnlSMMain);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmSelectProblem";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Select Problem";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Activated += new System.EventHandler(this.FrmSelectProblem_Activated);
            this.Deactivate += new System.EventHandler(this.FrmSelectProblem_Deactivate);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmSelectProblem_FormClosed);
            this.Load += new System.EventHandler(this.FrmSelectProblem_Load);
            this.tls_SM.ResumeLayout(false);
            this.tls_SM.PerformLayout();
            this.pnlsubType.ResumeLayout(false);
            this.Panel3.ResumeLayout(false);
            this.pnltlstripSM.ResumeLayout(false);
            this.pnltlstripSM.PerformLayout();
            this.Panel7.ResumeLayout(false);
            this.Panel8.ResumeLayout(false);
            this.Panel8.PerformLayout();
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.panel16.ResumeLayout(false);
            this.panel16.PerformLayout();
            this.panel9.ResumeLayout(false);
            this.cntFindings.ResumeLayout(false);
            this.pnlSMSearch.ResumeLayout(false);
            this.pnlSMSearch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PicBx_Search)).EndInit();
            this.pnlICD9.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel10.ResumeLayout(false);
            this.panel11.ResumeLayout(false);
            this.pnlmiddle.ResumeLayout(false);
            this.panel12.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel14.ResumeLayout(false);
            this.pnlfinding.ResumeLayout(false);
            this.pnlleft.ResumeLayout(false);
            this.Panel4.ResumeLayout(false);
            this.Panel5.ResumeLayout(false);
            this.pnlSMMain.ResumeLayout(false);
            this.pnlright.ResumeLayout(false);
            this.pnlBottom.ResumeLayout(false);
            this.panel13.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel15.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion

        internal System.Windows.Forms.TreeView trvSnoMed;
        internal System.Windows.Forms.Label lblSearchby;
        internal System.Windows.Forms.TextBox txtDescriptionID;
        internal gloGlobal.gloToolStripIgnoreFocus tls_SM;
        internal System.Windows.Forms.ToolStripButton tlb_SMSave;
        internal System.Windows.Forms.ToolStripButton tlb_SMClose;
        internal System.Windows.Forms.TreeView trvSubtype;
        internal System.Windows.Forms.Panel pnlsubType;
        internal System.Windows.Forms.Panel Panel3;
        internal System.Windows.Forms.Label Label27;
        private System.Windows.Forms.Label Label28;
        private System.Windows.Forms.Label Label29;
        private System.Windows.Forms.Label Label30;
        internal System.Windows.Forms.TextBox txtConceptID;
        internal System.Windows.Forms.Label lblConceptID;
        internal System.Windows.Forms.Label Label7;
        internal System.Windows.Forms.Panel pnltlstripSM;
        internal System.Windows.Forms.Panel pnlHeader;
        internal System.Windows.Forms.Label lblDescriptionID;
        internal System.Windows.Forms.Label Label9;
        internal System.Windows.Forms.Label lblSnoMedID;
        internal System.Windows.Forms.Label Label2;
        private System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.ToolTip tlAddFields;
        internal System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Label Label4;
        internal System.Windows.Forms.Panel Panel8;
        private System.Windows.Forms.Label Label5;
        internal System.Windows.Forms.Panel Panel7;
        internal System.Windows.Forms.ToolStripMenuItem mnuFindings;
        internal System.Windows.Forms.ContextMenuStrip cntFindings;
        internal System.Windows.Forms.TreeView trICD9;
        internal System.Windows.Forms.Panel pnlSMSearch;
        internal System.Windows.Forms.Label lbl_WhiteSpaceTop;
        internal System.Windows.Forms.Label lbl_WhiteSpaceBottom;
        internal System.Windows.Forms.PictureBox PicBx_Search;
        private System.Windows.Forms.Label lbl_pnlSearchBottomBrd;
        private System.Windows.Forms.Label lbl_pnlSearchTopBrd;
        private System.Windows.Forms.Label lbl_pnlSearchLeftBrd;
        private System.Windows.Forms.Label lbl_pnlSearchRightBrd;
        internal System.Windows.Forms.Panel pnlICD9;
        internal System.Windows.Forms.TreeView trvFindings;
        internal System.Windows.Forms.Panel pnlmiddle;
        internal System.Windows.Forms.Label Label36;
        internal System.Windows.Forms.Panel pnlfinding;
        internal System.Windows.Forms.Panel pnlleft;
        internal System.Windows.Forms.Panel Panel4;
        internal System.Windows.Forms.Panel Panel5;
        private System.Windows.Forms.Label Label35;
        internal System.Windows.Forms.Label Label31;
        private System.Windows.Forms.Label Label34;
        private System.Windows.Forms.Label Label33;
        private System.Windows.Forms.Label Label32;
        internal System.Windows.Forms.Panel pnlSMMain;
        internal System.Windows.Forms.Panel pnlright;
        internal System.Windows.Forms.Panel pnlBottom;
        internal System.Windows.Forms.TreeView trICD10;
        internal System.Windows.Forms.Splitter Splitter2;
        internal System.Windows.Forms.Timer Timer1;
       
       // public gloSearchTextBox txtSMSearch;
        public gloSearchTextBox txtSMSearch;
        private System.Windows.Forms.RadioButton RbConceptID;
        private System.Windows.Forms.RadioButton RbICD10;
        private System.Windows.Forms.RadioButton RbICD9;
        private System.Windows.Forms.CheckBox chkCOREProblem;
        internal System.Windows.Forms.Splitter splitter4;
        internal System.Windows.Forms.Splitter splitter3;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Button btnAdvanceDef;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        internal System.Windows.Forms.Panel panel1;
        internal System.Windows.Forms.Label label19;
        internal System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label43;
        private System.Windows.Forms.Label label47;
        private System.Windows.Forms.Label label48;
        internal System.Windows.Forms.Panel panel10;
        internal System.Windows.Forms.Panel panel11;
        internal System.Windows.Forms.Label Label23;
        private System.Windows.Forms.Label label42;
        private System.Windows.Forms.Label label44;
        private System.Windows.Forms.Label label45;
        private System.Windows.Forms.Label label46;
        internal System.Windows.Forms.Panel panel12;
        internal System.Windows.Forms.Label label49;
        internal System.Windows.Forms.Label label50;
        private System.Windows.Forms.Label label51;
        private System.Windows.Forms.Label label52;
        private System.Windows.Forms.Label label53;
        private System.Windows.Forms.Label label54;
        internal System.Windows.Forms.Panel panel6;
        internal System.Windows.Forms.Panel panel14;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.Label label61;
        private System.Windows.Forms.Label label62;
        internal System.Windows.Forms.Label label41;
        internal System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.Label label14;
        internal System.Windows.Forms.Panel panel13;
        internal System.Windows.Forms.Label label55;
        internal System.Windows.Forms.Label label56;
        private System.Windows.Forms.Label label57;
        private System.Windows.Forms.Label label58;
        private System.Windows.Forms.Label label59;
        private System.Windows.Forms.Label label60;
        internal System.Windows.Forms.Panel panel2;
        internal System.Windows.Forms.Panel panel15;
        internal System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.Panel panel16;
	}
}