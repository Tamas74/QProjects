namespace gloPM.Forms
{
    partial class Form1
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
            this.schedule1 = new Janus.Windows.Schedule.Schedule();
            this.uiPanelManager1 = new Janus.Windows.UI.Dock.UIPanelManager(this.components);
            this.c1FlexGrid1 = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.c1SuperTooltip1 = new C1.Win.C1SuperTooltip.C1SuperTooltip(this.components);
            this.uiGroupBox1 = new Janus.Windows.EditControls.UIGroupBox();
            this.calendar1 = new Janus.Windows.Schedule.Calendar();
            ((System.ComponentModel.ISupportInitialize)(this.schedule1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.uiPanelManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1FlexGrid1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.calendar1)).BeginInit();
            this.SuspendLayout();
            // 
            // schedule1
            // 
            //this.schedule1.HorizontalScrollPosition = 0;
            this.schedule1.Location = new System.Drawing.Point(56, 100);
            this.schedule1.Name = "schedule1";
            this.schedule1.Size = new System.Drawing.Size(200, 200);
            this.schedule1.TabIndex = 0;
            //this.schedule1.VerticalScrollPosition = 16;
            // 
            // uiPanelManager1
            // 
            this.uiPanelManager1.ContainerControl = this;
            // 
            // c1FlexGrid1
            // 
            this.c1FlexGrid1.ColumnInfo = "10,1,0,0,0,85,Columns:";
            this.c1FlexGrid1.Location = new System.Drawing.Point(126, 46);
            this.c1FlexGrid1.Name = "c1FlexGrid1";
            this.c1FlexGrid1.Rows.DefaultSize = 17;
            this.c1FlexGrid1.Size = new System.Drawing.Size(240, 150);
            this.c1FlexGrid1.TabIndex = 5;
            // 
            // c1SuperTooltip1
            // 
            this.c1SuperTooltip1.Font = new System.Drawing.Font("Tahoma", 8F);
            // 
            // uiGroupBox1
            // 
            this.uiGroupBox1.Location = new System.Drawing.Point(366, 309);
            this.uiGroupBox1.Name = "uiGroupBox1";
            this.uiGroupBox1.Size = new System.Drawing.Size(200, 100);
            this.uiGroupBox1.TabIndex = 6;
            this.uiGroupBox1.Text = "uiGroupBox1";
            // 
            // calendar1
            // 
            this.calendar1.Location = new System.Drawing.Point(390, 231);
            this.calendar1.Name = "calendar1";
            this.calendar1.Size = new System.Drawing.Size(148, 136);
            this.calendar1.TabIndex = 7;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(579, 417);
            this.Controls.Add(this.calendar1);
            this.Controls.Add(this.uiGroupBox1);
            this.Controls.Add(this.c1FlexGrid1);
            this.Controls.Add(this.schedule1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.schedule1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.uiPanelManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1FlexGrid1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.calendar1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Janus.Windows.Schedule.Schedule schedule1;
        private Janus.Windows.UI.Dock.UIPanelManager uiPanelManager1;
        private C1.Win.C1FlexGrid.C1FlexGrid c1FlexGrid1;
        private C1.Win.C1SuperTooltip.C1SuperTooltip c1SuperTooltip1;
        private Janus.Windows.EditControls.UIGroupBox uiGroupBox1;
        private Janus.Windows.Schedule.Calendar calendar1;
    }
}