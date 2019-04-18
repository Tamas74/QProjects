namespace gloAddress
{
    partial class gloZipcontrol
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
                if ((ToolTip1 != null))
                {
                    ToolTip1.RemoveAll();
                    ToolTip1.Dispose();
                    ToolTip1 = null;
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(gloZipcontrol));
            this.pnlGridLIst = new System.Windows.Forms.Panel();
            this.btnModify = new System.Windows.Forms.Button();
            this.btnSelect = new System.Windows.Forms.Button();
            this.btnCloseRefill = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.C1GridList = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.Panel4 = new System.Windows.Forms.Panel();
            this.Panel1 = new System.Windows.Forms.Panel();
            this.Label3 = new System.Windows.Forms.Label();
            this.txtListSearch = new System.Windows.Forms.TextBox();
            this.lblHeader = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label4 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.label5 = new System.Windows.Forms.Label();
            this.C1SuperTooltip1 = new C1.Win.C1SuperTooltip.C1SuperTooltip(this.components);
            this.pnlGridLIst.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.C1GridList)).BeginInit();
            this.Panel4.SuspendLayout();
            this.Panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlGridLIst
            // 
            this.pnlGridLIst.Controls.Add(this.btnModify);
            this.pnlGridLIst.Controls.Add(this.btnSelect);
            this.pnlGridLIst.Controls.Add(this.btnCloseRefill);
            this.pnlGridLIst.Controls.Add(this.btnAdd);
            this.pnlGridLIst.Controls.Add(this.C1GridList);
            this.pnlGridLIst.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGridLIst.Location = new System.Drawing.Point(0, 27);
            this.pnlGridLIst.Name = "pnlGridLIst";
            this.pnlGridLIst.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.pnlGridLIst.Size = new System.Drawing.Size(542, 294);
            this.pnlGridLIst.TabIndex = 24;
            // 
            // btnModify
            // 
            this.btnModify.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnModify.FlatAppearance.BorderSize = 0;
            this.btnModify.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnModify.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnModify.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnModify.Image = ((System.Drawing.Image)(resources.GetObject("btnModify.Image")));
            this.btnModify.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnModify.Location = new System.Drawing.Point(156, 116);
            this.btnModify.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnModify.Name = "btnModify";
            this.btnModify.Size = new System.Drawing.Size(25, 22);
            this.btnModify.TabIndex = 10;
            this.btnModify.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnModify.Visible = false;
            // 
            // btnSelect
            // 
            this.btnSelect.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSelect.FlatAppearance.BorderSize = 0;
            this.btnSelect.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnSelect.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnSelect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelect.Image = ((System.Drawing.Image)(resources.GetObject("btnSelect.Image")));
            this.btnSelect.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnSelect.Location = new System.Drawing.Point(107, 116);
            this.btnSelect.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(24, 22);
            this.btnSelect.TabIndex = 12;
            this.btnSelect.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnSelect.Visible = false;
            // 
            // btnCloseRefill
            // 
            this.btnCloseRefill.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCloseRefill.FlatAppearance.BorderSize = 0;
            this.btnCloseRefill.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnCloseRefill.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnCloseRefill.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCloseRefill.Image = ((System.Drawing.Image)(resources.GetObject("btnCloseRefill.Image")));
            this.btnCloseRefill.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnCloseRefill.Location = new System.Drawing.Point(181, 116);
            this.btnCloseRefill.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnCloseRefill.Name = "btnCloseRefill";
            this.btnCloseRefill.Size = new System.Drawing.Size(25, 22);
            this.btnCloseRefill.TabIndex = 9;
            this.btnCloseRefill.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnCloseRefill.Visible = false;
            // 
            // btnAdd
            // 
            this.btnAdd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAdd.FlatAppearance.BorderSize = 0;
            this.btnAdd.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnAdd.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.Image = ((System.Drawing.Image)(resources.GetObject("btnAdd.Image")));
            this.btnAdd.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnAdd.Location = new System.Drawing.Point(131, 116);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(25, 22);
            this.btnAdd.TabIndex = 11;
            this.btnAdd.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnAdd.Visible = false;
            // 
            // C1GridList
            // 
            this.C1GridList.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.C1GridList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.C1GridList.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.C1GridList.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.FixedSingle;
            this.C1GridList.ColumnInfo = "0,0,0,0,0,95,Columns:";
            this.C1GridList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.C1GridList.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.C1GridList.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.C1GridList.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.None;
            this.C1GridList.Location = new System.Drawing.Point(0, 3);
            this.C1GridList.Name = "C1GridList";
            this.C1GridList.Rows.DefaultSize = 19;
            this.C1GridList.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.C1GridList.ShowCellLabels = true;
            this.C1GridList.Size = new System.Drawing.Size(542, 291);
            this.C1GridList.StyleInfo = resources.GetString("C1GridList.StyleInfo");
            this.C1GridList.TabIndex = 3;
            this.C1GridList.AfterSelChange += new C1.Win.C1FlexGrid.RangeEventHandler(this.C1GridList_AfterSelChange);
            this.C1GridList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.C1GridList_KeyDown);
            this.C1GridList.Leave += new System.EventHandler(this.C1GridList_Leave);
            this.C1GridList.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.C1GridList_MouseDoubleClick);
            this.C1GridList.MouseMove += new System.Windows.Forms.MouseEventHandler(this.C1GridList_MouseMove);
            // 
            // Panel4
            // 
            this.Panel4.Controls.Add(this.Panel1);
            this.Panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.Panel4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Panel4.ForeColor = System.Drawing.Color.DarkBlue;
            this.Panel4.Location = new System.Drawing.Point(0, 1);
            this.Panel4.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Panel4.Name = "Panel4";
            this.Panel4.Size = new System.Drawing.Size(542, 26);
            this.Panel4.TabIndex = 25;
            // 
            // Panel1
            // 
            this.Panel1.BackColor = System.Drawing.Color.Transparent;
            this.Panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Panel1.BackgroundImage")));
            this.Panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Panel1.Controls.Add(this.Label3);
            this.Panel1.Controls.Add(this.txtListSearch);
            this.Panel1.Controls.Add(this.lblHeader);
            this.Panel1.Controls.Add(this.Label2);
            this.Panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Panel1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Panel1.Location = new System.Drawing.Point(0, 0);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(542, 26);
            this.Panel1.TabIndex = 19;
            // 
            // Label3
            // 
            this.Label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label3.Dock = System.Windows.Forms.DockStyle.Right;
            this.Label3.Font = new System.Drawing.Font("Tahoma", 9F);
            this.Label3.Location = new System.Drawing.Point(541, 0);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(1, 26);
            this.Label3.TabIndex = 15;
            this.Label3.Text = "label3";
            // 
            // txtListSearch
            // 
            this.txtListSearch.ForeColor = System.Drawing.Color.Black;
            this.txtListSearch.Location = new System.Drawing.Point(291, 2);
            this.txtListSearch.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtListSearch.Name = "txtListSearch";
            this.txtListSearch.Size = new System.Drawing.Size(251, 22);
            this.txtListSearch.TabIndex = 1;
            this.txtListSearch.Visible = false;
            // 
            // lblHeader
            // 
            this.lblHeader.BackColor = System.Drawing.Color.Transparent;
            this.lblHeader.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblHeader.Location = new System.Drawing.Point(1, 0);
            this.lblHeader.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(104, 26);
            this.lblHeader.TabIndex = 0;
            this.lblHeader.Text = " Custom List :";
            this.lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblHeader.Visible = false;
            // 
            // Label2
            // 
            this.Label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label2.Dock = System.Windows.Forms.DockStyle.Left;
            this.Label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label2.Location = new System.Drawing.Point(0, 0);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(1, 26);
            this.Label2.TabIndex = 14;
            this.Label2.Text = "label4";
            // 
            // Label4
            // 
            this.Label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label4.Dock = System.Windows.Forms.DockStyle.Top;
            this.Label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label4.Location = new System.Drawing.Point(0, 27);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(542, 1);
            this.Label4.TabIndex = 15;
            this.Label4.Text = "label1";
            // 
            // Label1
            // 
            this.Label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Label1.Font = new System.Drawing.Font("Tahoma", 9F);
            this.Label1.Location = new System.Drawing.Point(0, 320);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(542, 1);
            this.Label1.TabIndex = 16;
            this.Label1.Text = "label2";
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label5.Dock = System.Windows.Forms.DockStyle.Top;
            this.label5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(0, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(542, 1);
            this.label5.TabIndex = 26;
            this.label5.Text = "label1";
            this.label5.Visible = false;
            // 
            // C1SuperTooltip1
            // 
            this.C1SuperTooltip1.BackgroundGradient = C1.Win.C1SuperTooltip.BackgroundGradient.None;
            this.C1SuperTooltip1.Font = new System.Drawing.Font("Tahoma", 8F);
            // 
            // gloZipcontrol
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.Label4);
            this.Controls.Add(this.pnlGridLIst);
            this.Controls.Add(this.Panel4);
            this.Controls.Add(this.label5);
            this.Name = "gloZipcontrol";
            this.Size = new System.Drawing.Size(542, 321);
            this.Load += new System.EventHandler(this.gloZipcontrol_Load);
            this.pnlGridLIst.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.C1GridList)).EndInit();
            this.Panel4.ResumeLayout(false);
            this.Panel1.ResumeLayout(false);
            this.Panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlGridLIst;
        internal System.Windows.Forms.Panel Panel4;
        internal System.Windows.Forms.Panel Panel1;
        protected System.Windows.Forms.Button btnCloseRefill;
        protected System.Windows.Forms.Button btnModify;
        protected System.Windows.Forms.Button btnAdd;
        protected System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.TextBox txtListSearch;
        private System.Windows.Forms.Label lblHeader;
        internal System.Windows.Forms.ToolTip ToolTip1;
        public C1.Win.C1FlexGrid.C1FlexGrid C1GridList;
        private System.Windows.Forms.Label Label4;
        private System.Windows.Forms.Label Label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label Label3;
        private System.Windows.Forms.Label Label2;
        internal C1.Win.C1SuperTooltip.C1SuperTooltip C1SuperTooltip1;
    }
}
