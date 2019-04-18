namespace gloAppointmentBook
{
    partial class frmProvider_AppointmentType_Association
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmProvider_AppointmentType_Association));
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbl_BottomBrd = new System.Windows.Forms.Label();
            this.lbl_LeftBrd = new System.Windows.Forms.Label();
            this.lbl_RightBrd = new System.Windows.Forms.Label();
            this.lbl_TopBrd = new System.Windows.Forms.Label();
            this.c1AppointmentType = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.pnlToolStrip = new System.Windows.Forms.Panel();
            this.tlsp_ProApptypeAssociation = new gloGlobal.gloToolStripIgnoreFocus();
            this.tsb_Save = new System.Windows.Forms.ToolStripButton();
            this.tlsp_btnOK = new System.Windows.Forms.ToolStripButton();
            this.tlsp_btnCancel = new System.Windows.Forms.ToolStripButton();
            this.C1SuperTooltip1 = new C1.Win.C1SuperTooltip.C1SuperTooltip(this.components);
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1AppointmentType)).BeginInit();
            this.pnlToolStrip.SuspendLayout();
            this.tlsp_ProApptypeAssociation.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lbl_BottomBrd);
            this.panel1.Controls.Add(this.lbl_LeftBrd);
            this.panel1.Controls.Add(this.lbl_RightBrd);
            this.panel1.Controls.Add(this.lbl_TopBrd);
            this.panel1.Controls.Add(this.c1AppointmentType);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 53);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(3);
            this.panel1.Size = new System.Drawing.Size(472, 242);
            this.panel1.TabIndex = 0;
            // 
            // lbl_BottomBrd
            // 
            this.lbl_BottomBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_BottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_BottomBrd.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lbl_BottomBrd.Location = new System.Drawing.Point(4, 238);
            this.lbl_BottomBrd.Name = "lbl_BottomBrd";
            this.lbl_BottomBrd.Size = new System.Drawing.Size(464, 1);
            this.lbl_BottomBrd.TabIndex = 15;
            this.lbl_BottomBrd.Text = "label2";
            // 
            // lbl_LeftBrd
            // 
            this.lbl_LeftBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_LeftBrd.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_LeftBrd.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_LeftBrd.Location = new System.Drawing.Point(3, 4);
            this.lbl_LeftBrd.Name = "lbl_LeftBrd";
            this.lbl_LeftBrd.Size = new System.Drawing.Size(1, 235);
            this.lbl_LeftBrd.TabIndex = 14;
            this.lbl_LeftBrd.Text = "label4";
            // 
            // lbl_RightBrd
            // 
            this.lbl_RightBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_RightBrd.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_RightBrd.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lbl_RightBrd.Location = new System.Drawing.Point(468, 4);
            this.lbl_RightBrd.Name = "lbl_RightBrd";
            this.lbl_RightBrd.Size = new System.Drawing.Size(1, 235);
            this.lbl_RightBrd.TabIndex = 13;
            this.lbl_RightBrd.Text = "label3";
            // 
            // lbl_TopBrd
            // 
            this.lbl_TopBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_TopBrd.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_TopBrd.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_TopBrd.Location = new System.Drawing.Point(3, 3);
            this.lbl_TopBrd.Name = "lbl_TopBrd";
            this.lbl_TopBrd.Size = new System.Drawing.Size(466, 1);
            this.lbl_TopBrd.TabIndex = 12;
            this.lbl_TopBrd.Text = "label1";
            // 
            // c1AppointmentType
            // 
            this.c1AppointmentType.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.c1AppointmentType.AllowEditing = false;
            this.c1AppointmentType.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.ColumnsUniform;
            this.c1AppointmentType.AutoGenerateColumns = false;
            this.c1AppointmentType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.c1AppointmentType.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1AppointmentType.ColumnInfo = "1,1,0,0,0,95,Columns:";
            this.c1AppointmentType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1AppointmentType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1AppointmentType.Location = new System.Drawing.Point(3, 3);
            this.c1AppointmentType.Name = "c1AppointmentType";
            this.c1AppointmentType.Rows.Count = 1;
            this.c1AppointmentType.Rows.DefaultSize = 19;
            this.c1AppointmentType.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Cell;
            this.c1AppointmentType.Size = new System.Drawing.Size(466, 236);
            this.c1AppointmentType.StyleInfo = resources.GetString("c1AppointmentType.StyleInfo");
            this.c1AppointmentType.TabIndex = 11;
            this.c1AppointmentType.MouseMove += new System.Windows.Forms.MouseEventHandler(this.c1AppointmentType_MouseMove);
            // 
            // pnlToolStrip
            // 
            this.pnlToolStrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlToolStrip.Controls.Add(this.tlsp_ProApptypeAssociation);
            this.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolStrip.Location = new System.Drawing.Point(0, 0);
            this.pnlToolStrip.Name = "pnlToolStrip";
            this.pnlToolStrip.Size = new System.Drawing.Size(472, 53);
            this.pnlToolStrip.TabIndex = 12;
            // 
            // tlsp_ProApptypeAssociation
            // 
            this.tlsp_ProApptypeAssociation.BackColor = System.Drawing.Color.Transparent;
            this.tlsp_ProApptypeAssociation.BackgroundImage = global::gloAppointmentBook.Properties.Resources.Img_Toolstrip;
            this.tlsp_ProApptypeAssociation.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tlsp_ProApptypeAssociation.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tlsp_ProApptypeAssociation.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tlsp_ProApptypeAssociation.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsb_Save,
            this.tlsp_btnOK,
            this.tlsp_btnCancel});
            this.tlsp_ProApptypeAssociation.Location = new System.Drawing.Point(0, 0);
            this.tlsp_ProApptypeAssociation.Name = "tlsp_ProApptypeAssociation";
            this.tlsp_ProApptypeAssociation.Size = new System.Drawing.Size(472, 53);
            this.tlsp_ProApptypeAssociation.TabIndex = 0;
            this.tlsp_ProApptypeAssociation.Text = "toolStrip1";
            this.tlsp_ProApptypeAssociation.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.tlsp_ProApptypeAssociation_ItemClicked);
            // 
            // tsb_Save
            // 
            this.tsb_Save.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Save.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Save.Image")));
            this.tsb_Save.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Save.Name = "tsb_Save";
            this.tsb_Save.Size = new System.Drawing.Size(40, 50);
            this.tsb_Save.Tag = "Save";
            this.tsb_Save.Text = "&Save";
            this.tsb_Save.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Save.ToolTipText = "Save";
            // 
            // tlsp_btnOK
            // 
            this.tlsp_btnOK.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlsp_btnOK.Image = ((System.Drawing.Image)(resources.GetObject("tlsp_btnOK.Image")));
            this.tlsp_btnOK.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlsp_btnOK.Name = "tlsp_btnOK";
            this.tlsp_btnOK.Size = new System.Drawing.Size(66, 50);
            this.tlsp_btnOK.Tag = "OK";
            this.tlsp_btnOK.Text = "Sa&ve&&Cls";
            this.tlsp_btnOK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlsp_btnOK.ToolTipText = "Save and Close";
            // 
            // tlsp_btnCancel
            // 
            this.tlsp_btnCancel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlsp_btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("tlsp_btnCancel.Image")));
            this.tlsp_btnCancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlsp_btnCancel.Name = "tlsp_btnCancel";
            this.tlsp_btnCancel.Size = new System.Drawing.Size(43, 50);
            this.tlsp_btnCancel.Tag = "Cancel";
            this.tlsp_btnCancel.Text = "&Close";
            this.tlsp_btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // C1SuperTooltip1
            // 
            this.C1SuperTooltip1.BackgroundGradient = C1.Win.C1SuperTooltip.BackgroundGradient.None;
            this.C1SuperTooltip1.Font = new System.Drawing.Font("Tahoma", 8F);
            // 
            // frmProvider_AppointmentType_Association
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(472, 295);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pnlToolStrip);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmProvider_AppointmentType_Association";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Provider Appointment Type Association";
            this.Load += new System.EventHandler(this.frmProvider_AppointmentType_Association_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1AppointmentType)).EndInit();
            this.pnlToolStrip.ResumeLayout(false);
            this.pnlToolStrip.PerformLayout();
            this.tlsp_ProApptypeAssociation.ResumeLayout(false);
            this.tlsp_ProApptypeAssociation.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private C1.Win.C1FlexGrid.C1FlexGrid c1AppointmentType;
        private System.Windows.Forms.Panel pnlToolStrip;
        private gloGlobal.gloToolStripIgnoreFocus tlsp_ProApptypeAssociation;
        private System.Windows.Forms.ToolStripButton tlsp_btnOK;
        private System.Windows.Forms.ToolStripButton tlsp_btnCancel;
        private System.Windows.Forms.Label lbl_BottomBrd;
        private System.Windows.Forms.Label lbl_LeftBrd;
        private System.Windows.Forms.Label lbl_RightBrd;
        private System.Windows.Forms.Label lbl_TopBrd;
        internal C1.Win.C1SuperTooltip.C1SuperTooltip C1SuperTooltip1;
        //private System.Windows.Forms.ToolStripButton tsb_Save;
        internal  System.Windows.Forms.ToolStripButton tsb_Save;
    }
}