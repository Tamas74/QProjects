namespace ChargeRules
{
    partial class gloListControl
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
                if (_dtList != null)
                {
                    _dtList.Dispose();
                    _dtList = null;
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(gloListControl));
            this.Panel4 = new System.Windows.Forms.Panel();
            this.Panel1 = new System.Windows.Forms.Panel();
            this.txtListSearch = new System.Windows.Forms.TextBox();
            this.lblHeader = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label3 = new System.Windows.Forms.Label();
            this.Label4 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.c1GridList = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.Label8 = new System.Windows.Forms.Label();
            this.Label7 = new System.Windows.Forms.Label();
            this.Label6 = new System.Windows.Forms.Label();
            this.pnlGridList = new System.Windows.Forms.Panel();
            this.Label5 = new System.Windows.Forms.Label();
            this.C1SuperTooltip1 = new C1.Win.C1SuperTooltip.C1SuperTooltip(this.components);
            this.mypnl = new System.Windows.Forms.Panel();
            this.Panel4.SuspendLayout();
            this.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1GridList)).BeginInit();
            this.pnlGridList.SuspendLayout();
            this.mypnl.SuspendLayout();
            this.SuspendLayout();
            // 
            // Panel4
            // 
            this.Panel4.Controls.Add(this.Panel1);
            this.Panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.Panel4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Panel4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Panel4.Location = new System.Drawing.Point(0, 0);
            this.Panel4.Name = "Panel4";
            this.Panel4.Padding = new System.Windows.Forms.Padding(3);
            this.Panel4.Size = new System.Drawing.Size(533, 28);
            this.Panel4.TabIndex = 24;
            // 
            // Panel1
            // 
            this.Panel1.BackColor = System.Drawing.Color.Transparent;
            this.Panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Panel1.BackgroundImage")));
            this.Panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Panel1.Controls.Add(this.txtListSearch);
            this.Panel1.Controls.Add(this.lblHeader);
            this.Panel1.Controls.Add(this.Label2);
            this.Panel1.Controls.Add(this.Label3);
            this.Panel1.Controls.Add(this.Label4);
            this.Panel1.Controls.Add(this.Label1);
            this.Panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Panel1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Panel1.Location = new System.Drawing.Point(3, 3);
            this.Panel1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(527, 22);
            this.Panel1.TabIndex = 19;
            // 
            // txtListSearch
            // 
            this.txtListSearch.ForeColor = System.Drawing.Color.Black;
            this.txtListSearch.Location = new System.Drawing.Point(211, 0);
            this.txtListSearch.Name = "txtListSearch";
            this.txtListSearch.Size = new System.Drawing.Size(189, 22);
            this.txtListSearch.TabIndex = 1;
            this.txtListSearch.Visible = false;
            // 
            // lblHeader
            // 
            this.lblHeader.BackColor = System.Drawing.Color.Transparent;
            this.lblHeader.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.ForeColor = System.Drawing.Color.White;
            this.lblHeader.Location = new System.Drawing.Point(1, 1);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(201, 20);
            this.lblHeader.TabIndex = 0;
            this.lblHeader.Text = " Custom List :";
            this.lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label2
            // 
            this.Label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label2.Dock = System.Windows.Forms.DockStyle.Left;
            this.Label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label2.Location = new System.Drawing.Point(0, 1);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(1, 20);
            this.Label2.TabIndex = 7;
            this.Label2.Text = "label4";
            // 
            // Label3
            // 
            this.Label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label3.Dock = System.Windows.Forms.DockStyle.Right;
            this.Label3.Font = new System.Drawing.Font("Tahoma", 9F);
            this.Label3.Location = new System.Drawing.Point(526, 1);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(1, 20);
            this.Label3.TabIndex = 6;
            this.Label3.Text = "label3";
            // 
            // Label4
            // 
            this.Label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label4.Dock = System.Windows.Forms.DockStyle.Top;
            this.Label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label4.Location = new System.Drawing.Point(0, 0);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(527, 1);
            this.Label4.TabIndex = 5;
            this.Label4.Text = "label1";
            // 
            // Label1
            // 
            this.Label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Label1.Font = new System.Drawing.Font("Tahoma", 9F);
            this.Label1.Location = new System.Drawing.Point(0, 21);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(527, 1);
            this.Label1.TabIndex = 8;
            this.Label1.Text = "label2";
            // 
            // c1GridList
            // 
            this.c1GridList.AccessibleRole = System.Windows.Forms.AccessibleRole.Window;
            this.c1GridList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.c1GridList.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.c1GridList.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1GridList.ColumnInfo = "0,0,0,0,0,95,Columns:";
            this.c1GridList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1GridList.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.c1GridList.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1GridList.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.None;
            this.c1GridList.Location = new System.Drawing.Point(4, 1);
            this.c1GridList.Name = "c1GridList";
            this.c1GridList.Rows.Count = 0;
            this.c1GridList.Rows.DefaultSize = 19;
            this.c1GridList.Rows.Fixed = 0;
            this.c1GridList.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1GridList.ShowCellLabels = true;
            this.c1GridList.Size = new System.Drawing.Size(525, 319);
            this.c1GridList.StyleInfo = resources.GetString("c1GridList.StyleInfo");
            this.c1GridList.TabIndex = 2;
            this.c1GridList.AfterSelChange += new C1.Win.C1FlexGrid.RangeEventHandler(this.c1GridList_AfterSelChange);
            this.c1GridList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.c1GridList_KeyDown);
            this.c1GridList.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.c1GridList_MouseDoubleClick);
            // 
            // Label8
            // 
            this.Label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label8.Dock = System.Windows.Forms.DockStyle.Top;
            this.Label8.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label8.Location = new System.Drawing.Point(3, 0);
            this.Label8.Name = "Label8";
            this.Label8.Size = new System.Drawing.Size(527, 1);
            this.Label8.TabIndex = 9;
            this.Label8.Text = "label1";
            // 
            // Label7
            // 
            this.Label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label7.Dock = System.Windows.Forms.DockStyle.Right;
            this.Label7.Font = new System.Drawing.Font("Tahoma", 9F);
            this.Label7.Location = new System.Drawing.Point(529, 1);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(1, 320);
            this.Label7.TabIndex = 10;
            this.Label7.Text = "label3";
            // 
            // Label6
            // 
            this.Label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label6.Dock = System.Windows.Forms.DockStyle.Left;
            this.Label6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label6.Location = new System.Drawing.Point(3, 1);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(1, 320);
            this.Label6.TabIndex = 11;
            this.Label6.Text = "label4";
            // 
            // pnlGridList
            // 
            this.pnlGridList.Controls.Add(this.c1GridList);
            this.pnlGridList.Controls.Add(this.Label5);
            this.pnlGridList.Controls.Add(this.Label6);
            this.pnlGridList.Controls.Add(this.Label7);
            this.pnlGridList.Controls.Add(this.Label8);
            this.pnlGridList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGridList.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlGridList.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.pnlGridList.Location = new System.Drawing.Point(0, 0);
            this.pnlGridList.Name = "pnlGridList";
            this.pnlGridList.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.pnlGridList.Size = new System.Drawing.Size(533, 324);
            this.pnlGridList.TabIndex = 22;
            // 
            // Label5
            // 
            this.Label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Label5.Font = new System.Drawing.Font("Tahoma", 9F);
            this.Label5.Location = new System.Drawing.Point(4, 320);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(525, 1);
            this.Label5.TabIndex = 12;
            this.Label5.Text = "label2";
            // 
            // C1SuperTooltip1
            // 
            this.C1SuperTooltip1.BackgroundGradient = C1.Win.C1SuperTooltip.BackgroundGradient.None;
            this.C1SuperTooltip1.Font = new System.Drawing.Font("Tahoma", 8F);
            // 
            // mypnl
            // 
            this.mypnl.Controls.Add(this.pnlGridList);
            this.mypnl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mypnl.Location = new System.Drawing.Point(0, 28);
            this.mypnl.Name = "mypnl";
            this.mypnl.Size = new System.Drawing.Size(533, 324);
            this.mypnl.TabIndex = 24;
            // 
            // gloListControl
            // 
            this.AccessibleRole = System.Windows.Forms.AccessibleRole.Window;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.Controls.Add(this.mypnl);
            this.Controls.Add(this.Panel4);
            this.Name = "gloListControl";
            this.Size = new System.Drawing.Size(533, 352);
            this.Load += new System.EventHandler(this.gloListControl_Load);
            this.Panel4.ResumeLayout(false);
            this.Panel1.ResumeLayout(false);
            this.Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1GridList)).EndInit();
            this.pnlGridList.ResumeLayout(false);
            this.mypnl.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Panel Panel4;
        internal System.Windows.Forms.Panel Panel1;
        private System.Windows.Forms.TextBox txtListSearch;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Label Label2;
        private System.Windows.Forms.Label Label3;
        private System.Windows.Forms.Label Label4;
        private System.Windows.Forms.Label Label1;
        private C1.Win.C1FlexGrid.C1FlexGrid c1GridList;
        private System.Windows.Forms.Label Label8;
        private System.Windows.Forms.Label Label7;
        private System.Windows.Forms.Label Label6;
        private System.Windows.Forms.Panel pnlGridList;
        private System.Windows.Forms.Label Label5;
        private C1.Win.C1SuperTooltip.C1SuperTooltip C1SuperTooltip1;
        private System.Windows.Forms.Panel mypnl;



    }
}
