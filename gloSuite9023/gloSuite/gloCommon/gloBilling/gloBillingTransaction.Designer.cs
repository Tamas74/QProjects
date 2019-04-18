namespace gloBilling
{
    partial class gloBillingTransaction
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
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(this);
                }
                catch
                {
                }
                try
                {
                    System.Windows.Forms.ContextMenuStrip[] cntMenuControls = { cmnu_Apply };
                    System.Windows.Forms.Control[] cntmnuControls = { cmnu_Apply };
                    if (cntMenuControls != null)
                    {
                        if (cntMenuControls.Length > 0)
                        {
                            gloGlobal.cEventHelper.RemoveAllEventHandlers(ref cntMenuControls);

                        }
                    }
                    if (cntmnuControls != null)
                    {
                        if (cntmnuControls.Length > 0)
                        {
                            gloGlobal.cEventHelper.DisposeAllControls(ref cntmnuControls);

                        }
                    }
                    //if (cmnu_Apply != null)
                    //{
                    //    gloGlobal.cEventHelper.RemoveAllEventHandlers(cmnu_Apply);
                    //    if (cmnu_Apply.Items != null)
                    //    {
                    //        cmnu_Apply.Items.Clear();

                    //    }
                    //    cmnu_Apply.Dispose();
                    //    cmnu_Apply = null;
                    //}
                }
                catch
                {
                }

               
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(gloBillingTransaction));
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.pnlFooter = new System.Windows.Forms.Panel();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.pnlInternalControl = new System.Windows.Forms.Panel();
            this.c1Transaction = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.label1 = new System.Windows.Forms.Label();
            this.c1Total = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.cmnu_Apply = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tls_cmnu_ApplyAll = new System.Windows.Forms.ToolStripMenuItem();
            this.tls_cmnu_OverwriteApplyAll = new System.Windows.Forms.ToolStripMenuItem();
            this.C1SuperTooltip1 = new C1.Win.C1SuperTooltip.C1SuperTooltip(this.components);
            this.tmrChangeEditSearch = new System.Windows.Forms.Timer(this.components);
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1Transaction)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1Total)).BeginInit();
            this.cmnu_Apply.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(725, 1);
            this.pnlHeader.TabIndex = 1;
            this.pnlHeader.Visible = false;
            // 
            // pnlFooter
            // 
            this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFooter.Location = new System.Drawing.Point(0, 296);
            this.pnlFooter.Name = "pnlFooter";
            this.pnlFooter.Size = new System.Drawing.Size(725, 1);
            this.pnlFooter.TabIndex = 2;
            this.pnlFooter.Visible = false;
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.pnlInternalControl);
            this.pnlMain.Controls.Add(this.c1Transaction);
            this.pnlMain.Controls.Add(this.label1);
            this.pnlMain.Controls.Add(this.c1Total);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 1);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(725, 295);
            this.pnlMain.TabIndex = 4;
            // 
            // pnlInternalControl
            // 
            this.pnlInternalControl.AutoScroll = true;
            this.pnlInternalControl.AutoSize = true;
            this.pnlInternalControl.Location = new System.Drawing.Point(105, 20);
            this.pnlInternalControl.Name = "pnlInternalControl";
            this.pnlInternalControl.Size = new System.Drawing.Size(337, 211);
            this.pnlInternalControl.TabIndex = 3;
            this.pnlInternalControl.Visible = false;
            // 
            // c1Transaction
            // 
            this.c1Transaction.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.Rows;
            this.c1Transaction.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.c1Transaction.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.c1Transaction.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1Transaction.ColumnInfo = "10,1,0,0,0,90,Columns:3{Style:\"Format:\"\"d\"\";\";}\t4{Style:\"Format:\"\"d\"\";\";}\t";
            this.c1Transaction.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1Transaction.DragMode = C1.Win.C1FlexGrid.DragModeEnum.AutomaticMove;
            this.c1Transaction.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.c1Transaction.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1Transaction.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.None;
            this.c1Transaction.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
            this.c1Transaction.Location = new System.Drawing.Point(0, 0);
            this.c1Transaction.Name = "c1Transaction";
            this.c1Transaction.Rows.Count = 2;
            this.c1Transaction.Rows.DefaultSize = 18;
            this.c1Transaction.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1Transaction.Size = new System.Drawing.Size(725, 268);
            this.c1Transaction.StyleInfo = resources.GetString("c1Transaction.StyleInfo");
            this.c1Transaction.TabIndex = 0;
            this.c1Transaction.AfterDragRow += new C1.Win.C1FlexGrid.DragRowColEventHandler(this.c1Transaction_AfterDragRow);
            this.c1Transaction.AfterScroll += new C1.Win.C1FlexGrid.RangeEventHandler(this.c1Transaction_AfterScroll);
            this.c1Transaction.AfterRowColChange += new C1.Win.C1FlexGrid.RangeEventHandler(this.c1Transaction_AfterRowColChange);
            this.c1Transaction.BeforeSelChange += new C1.Win.C1FlexGrid.RangeEventHandler(this.c1Transaction_BeforeSelChange);
            this.c1Transaction.AfterSelChange += new C1.Win.C1FlexGrid.RangeEventHandler(this.c1Transaction_AfterSelChange);
            this.c1Transaction.StartEdit += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1Transaction_StartEdit);
            this.c1Transaction.AfterEdit += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1Transaction_AfterEdit);
            this.c1Transaction.LeaveEdit += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1Transaction_LeaveEdit);
            this.c1Transaction.CellButtonClick += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1Transaction_CellButtonClick);
            this.c1Transaction.ChangeEdit += new System.EventHandler(this.c1Transaction_ChangeEdit);
            this.c1Transaction.KeyDownEdit += new C1.Win.C1FlexGrid.KeyEditEventHandler(this.c1Transaction_KeyDownEdit);
            this.c1Transaction.KeyUpEdit += new C1.Win.C1FlexGrid.KeyEditEventHandler(this.c1Transaction_KeyUpEdit);
            this.c1Transaction.KeyPressEdit += new C1.Win.C1FlexGrid.KeyPressEditEventHandler(this.c1Transaction_KeyPressEdit);
            this.c1Transaction.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1Transaction_CellChanged);            
            this.c1Transaction.Click += new System.EventHandler(this.c1Transaction_Click);
            this.c1Transaction.KeyDown += new System.Windows.Forms.KeyEventHandler(this.c1Transaction_KeyDown);
            this.c1Transaction.KeyUp += new System.Windows.Forms.KeyEventHandler(this.c1Transaction_KeyUp);
            this.c1Transaction.MouseClick += new System.Windows.Forms.MouseEventHandler(this.c1Transaction_MouseClick);
            this.c1Transaction.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.c1Transaction_MouseDoubleClick);
            this.c1Transaction.MouseDown += new System.Windows.Forms.MouseEventHandler(this.c1Transaction_MouseDown);
            this.c1Transaction.MouseMove += new System.Windows.Forms.MouseEventHandler(this.c1Transaction_MouseMove);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.SteelBlue;
            this.label1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label1.Location = new System.Drawing.Point(0, 268);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(725, 4);
            this.label1.TabIndex = 102;
            // 
            // c1Total
            // 
            this.c1Total.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.None;
            this.c1Total.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.c1Total.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.c1Total.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1Total.ColumnInfo = "1,1,0,0,0,95,Columns:";
            this.c1Total.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.c1Total.FocusRect = C1.Win.C1FlexGrid.FocusRectEnum.None;
            this.c1Total.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.c1Total.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1Total.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.None;
            this.c1Total.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcrossOut;
            this.c1Total.Location = new System.Drawing.Point(0, 272);
            this.c1Total.Name = "c1Total";
            this.c1Total.Rows.Count = 1;
            this.c1Total.Rows.DefaultSize = 19;
            this.c1Total.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.c1Total.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1Total.Size = new System.Drawing.Size(725, 23);
            this.c1Total.StyleInfo = resources.GetString("c1Total.StyleInfo");
            this.c1Total.TabIndex = 101;
            this.c1Total.TabStop = false;
            // 
            // cmnu_Apply
            // 
            this.cmnu_Apply.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tls_cmnu_ApplyAll,
            this.tls_cmnu_OverwriteApplyAll});
            this.cmnu_Apply.Name = "cmnu_Apply";
            this.cmnu_Apply.Size = new System.Drawing.Size(200, 48);
            this.cmnu_Apply.Closing += new System.Windows.Forms.ToolStripDropDownClosingEventHandler(this.cmnu_Apply_Closing);
            // 
            // tls_cmnu_ApplyAll
            // 
            this.tls_cmnu_ApplyAll.Image = ((System.Drawing.Image)(resources.GetObject("tls_cmnu_ApplyAll.Image")));
            this.tls_cmnu_ApplyAll.Name = "tls_cmnu_ApplyAll";
            this.tls_cmnu_ApplyAll.Size = new System.Drawing.Size(199, 22);
            this.tls_cmnu_ApplyAll.Tag = "ApplyAll";
            this.tls_cmnu_ApplyAll.Text = "Apply All";
            this.tls_cmnu_ApplyAll.ToolTipText = "Apply All";
            this.tls_cmnu_ApplyAll.Click += new System.EventHandler(this.tls_cmnu_ApplyAll_Click);
            // 
            // tls_cmnu_OverwriteApplyAll
            // 
            this.tls_cmnu_OverwriteApplyAll.Image = ((System.Drawing.Image)(resources.GetObject("tls_cmnu_OverwriteApplyAll.Image")));
            this.tls_cmnu_OverwriteApplyAll.Name = "tls_cmnu_OverwriteApplyAll";
            this.tls_cmnu_OverwriteApplyAll.Size = new System.Drawing.Size(199, 22);
            this.tls_cmnu_OverwriteApplyAll.Tag = "OverwriteApplyAll";
            this.tls_cmnu_OverwriteApplyAll.Text = "Overwrite and Apply All";
            this.tls_cmnu_OverwriteApplyAll.ToolTipText = "Overwrite and apply";
            this.tls_cmnu_OverwriteApplyAll.Click += new System.EventHandler(this.tls_cmnu_OverwriteApplyAll_Click);
            // 
            // C1SuperTooltip1
            // 
            this.C1SuperTooltip1.BackgroundGradient = C1.Win.C1SuperTooltip.BackgroundGradient.None;
            this.C1SuperTooltip1.Font = new System.Drawing.Font("Tahoma", 8F);
            // 
            // tmrChangeEditSearch
            // 
            this.tmrChangeEditSearch.Tick += new System.EventHandler(this.tmrChangeEditSearch_Tick);
            // 
            // gloBillingTransaction
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnlFooter);
            this.Controls.Add(this.pnlHeader);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.DarkBlue;
            this.Name = "gloBillingTransaction";
            this.Size = new System.Drawing.Size(725, 297);
            this.Load += new System.EventHandler(this.gloBillingTransaction_Load);
            this.Scroll += new System.Windows.Forms.ScrollEventHandler(this.gloBillingTransaction_Scroll);
            this.Leave += new System.EventHandler(this.gloBillingTransaction_Leave);
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1Transaction)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1Total)).EndInit();
            this.cmnu_Apply.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Panel pnlFooter;
        private System.Windows.Forms.Panel pnlMain;
        private C1.Win.C1FlexGrid.C1FlexGrid c1Transaction;
        private System.Windows.Forms.Panel pnlInternalControl;
        private C1.Win.C1FlexGrid.C1FlexGrid c1Total;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ContextMenuStrip cmnu_Apply;
        private System.Windows.Forms.ToolStripMenuItem tls_cmnu_ApplyAll;
        private System.Windows.Forms.ToolStripMenuItem tls_cmnu_OverwriteApplyAll;
        private System.Windows.Forms.Timer tmrChangeEditSearch;
        private C1.Win.C1SuperTooltip.C1SuperTooltip C1SuperTooltip1;

    }
}
