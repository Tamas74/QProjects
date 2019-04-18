using System.Diagnostics;
using System;
using System.Windows.Forms;
using System.Collections;
using System.Drawing;
using Microsoft.VisualBasic;
using System.Data;
using System.Collections.Generic;

namespace gloEmdeonInterface.Forms
{
	//[global::Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class frmLab_Graphs : System.Windows.Forms.Form
		{
		
		//Form overrides dispose to clean up the component list.
		[System.Diagnostics.DebuggerNonUserCode()]protected override void Dispose(bool disposing)
			{
                System.Windows.Forms.DateTimePicker[] cntdtControls = { dtTo, dtFrom };
                System.Windows.Forms.Control[] cntControls = { dtTo, dtFrom };

            if (disposing && (components != null))
			{
                try
                {

                    if (cntdtControls != null)
                    {
                        if (cntdtControls.Length > 0)
                        {
                            gloGlobal.cEventHelper.RemoveAllEventHandlers(ref cntdtControls);

                        }
                    }

                }
                catch
                {
                }

                components.Dispose();
                base.Dispose(disposing);
                try
                {

                    if (cntControls != null)
                    {
                        if (cntControls.Length > 0)
                        {
                            gloGlobal.cEventHelper.DisposeAllControls(ref cntControls);

                        }
                    }
                  
                }
                catch
                {
                }


                //try
                //{
                //    if (dtFrom != null)
                //    {
                //        try
                //        {
                //            gloGlobal.cEventHelper.RemoveAllEventHandlers(dtFrom);
                //        }
                //        catch
                //        {
                //        }
                //        dtFrom.Dispose();
                //        dtFrom = null;
                //    }
                //}
                //catch
                //{
                //}

                try
                {
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(this);
                }
                catch
                {
                }
				
			}
			
		}
		
		//Required by the Windows Form Designer
		private System.ComponentModel.Container components = null;
		
		//NOTE: The following procedure is required by the Windows Form Designer
		//It can be modified using the Windows Form Designer.
		//Do not modify it using the code editor.
		[System.Diagnostics.DebuggerStepThrough()]private void InitializeComponent()
			{
                System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLab_Graphs));
                this.cmbResults = new System.Windows.Forms.ComboBox();
                this.cmbTests = new System.Windows.Forms.ComboBox();
                this.dtTo = new System.Windows.Forms.DateTimePicker();
                this.dtFrom = new System.Windows.Forms.DateTimePicker();
                this.lblResults = new System.Windows.Forms.Label();
                this.lblTests = new System.Windows.Forms.Label();
                this.lblTodate = new System.Windows.Forms.Label();
                this.lblFrmDate = new System.Windows.Forms.Label();
                this.Panel1 = new System.Windows.Forms.Panel();
                this.tlsp_LabResultGraph = new gloGlobal.gloToolStripIgnoreFocus();
                this.ts_btnSave = new System.Windows.Forms.ToolStripButton();
                this.ts_btnClose = new System.Windows.Forms.ToolStripButton();
                this.pnl_Base = new System.Windows.Forms.Panel();
                this.lbl_pnlBottom = new System.Windows.Forms.Label();
                this.lbl_pnlLeft = new System.Windows.Forms.Label();
                this.lbl_pnlRight = new System.Windows.Forms.Label();
                this.lbl_pnlTop = new System.Windows.Forms.Label();
                this.Panel1.SuspendLayout();
                this.tlsp_LabResultGraph.SuspendLayout();
                this.pnl_Base.SuspendLayout();
                this.SuspendLayout();
                // 
                // cmbResults
                // 
                this.cmbResults.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
                this.cmbResults.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.cmbResults.FormattingEnabled = true;
                this.cmbResults.Location = new System.Drawing.Point(261, 59);
                this.cmbResults.Name = "cmbResults";
                this.cmbResults.Size = new System.Drawing.Size(98, 22);
                this.cmbResults.TabIndex = 8;
                this.cmbResults.SelectionChangeCommitted += new System.EventHandler(this.cmbResults_SelectionChangeCommitted);
                this.cmbResults.SelectedIndexChanged += new System.EventHandler(this.cmbResults_SelectedIndexChanged);
                // 
                // cmbTests
                // 
                this.cmbTests.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
                this.cmbTests.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.cmbTests.FormattingEnabled = true;
                this.cmbTests.Location = new System.Drawing.Point(88, 59);
                this.cmbTests.Name = "cmbTests";
                this.cmbTests.Size = new System.Drawing.Size(97, 22);
                this.cmbTests.TabIndex = 7;
                this.cmbTests.SelectedIndexChanged += new System.EventHandler(this.cmbTests_SelectedIndexChanged);
                // 
                // dtTo
                // 
                this.dtTo.CalendarForeColor = System.Drawing.Color.Maroon;
                this.dtTo.CalendarMonthBackground = System.Drawing.Color.White;
                this.dtTo.CalendarTitleBackColor = System.Drawing.Color.Orange;
                this.dtTo.CalendarTitleForeColor = System.Drawing.Color.Brown;
                this.dtTo.CalendarTrailingForeColor = System.Drawing.Color.Tomato;
                this.dtTo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.dtTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
                this.dtTo.Location = new System.Drawing.Point(261, 25);
                this.dtTo.Name = "dtTo";
                this.dtTo.Size = new System.Drawing.Size(98, 22);
                this.dtTo.TabIndex = 5;
                // 
                // dtFrom
                // 
                this.dtFrom.CalendarForeColor = System.Drawing.Color.Maroon;
                this.dtFrom.CalendarMonthBackground = System.Drawing.Color.White;
                this.dtFrom.CalendarTitleBackColor = System.Drawing.Color.Orange;
                this.dtFrom.CalendarTitleForeColor = System.Drawing.Color.Brown;
                this.dtFrom.CalendarTrailingForeColor = System.Drawing.Color.Tomato;
                this.dtFrom.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.dtFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
                this.dtFrom.Location = new System.Drawing.Point(88, 25);
                this.dtFrom.Name = "dtFrom";
                this.dtFrom.Size = new System.Drawing.Size(97, 22);
                this.dtFrom.TabIndex = 4;
                this.dtFrom.Value = new System.DateTime(2007, 7, 4, 0, 0, 0, 0);
                // 
                // lblResults
                // 
                this.lblResults.AutoSize = true;
                this.lblResults.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.lblResults.Location = new System.Drawing.Point(204, 63);
                this.lblResults.Name = "lblResults";
                this.lblResults.Size = new System.Drawing.Size(53, 14);
                this.lblResults.TabIndex = 3;
                this.lblResults.Text = "Results :";
                // 
                // lblTests
                // 
                this.lblTests.AutoSize = true;
                this.lblTests.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.lblTests.Location = new System.Drawing.Point(39, 63);
                this.lblTests.Name = "lblTests";
                this.lblTests.Size = new System.Drawing.Size(45, 14);
                this.lblTests.TabIndex = 2;
                this.lblTests.Text = "Tests :";
                // 
                // lblTodate
                // 
                this.lblTodate.AutoSize = true;
                this.lblTodate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.lblTodate.Location = new System.Drawing.Point(197, 29);
                this.lblTodate.Name = "lblTodate";
                this.lblTodate.Size = new System.Drawing.Size(60, 14);
                this.lblTodate.TabIndex = 1;
                this.lblTodate.Text = "To Date :";
                // 
                // lblFrmDate
                // 
                this.lblFrmDate.AutoSize = true;
                this.lblFrmDate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.lblFrmDate.Location = new System.Drawing.Point(12, 29);
                this.lblFrmDate.Name = "lblFrmDate";
                this.lblFrmDate.Size = new System.Drawing.Size(72, 14);
                this.lblFrmDate.TabIndex = 0;
                this.lblFrmDate.Text = "From Date :";
                // 
                // Panel1
                // 
                this.Panel1.Controls.Add(this.tlsp_LabResultGraph);
                this.Panel1.Dock = System.Windows.Forms.DockStyle.Top;
                this.Panel1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
                this.Panel1.Location = new System.Drawing.Point(0, 0);
                this.Panel1.Name = "Panel1";
                this.Panel1.Size = new System.Drawing.Size(396, 53);
                this.Panel1.TabIndex = 1;
                // 
                // tlsp_LabResultGraph
                // 
                this.tlsp_LabResultGraph.BackColor = System.Drawing.Color.Transparent;
                this.tlsp_LabResultGraph.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tlsp_LabResultGraph.BackgroundImage")));
                this.tlsp_LabResultGraph.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
                this.tlsp_LabResultGraph.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.tlsp_LabResultGraph.ImageScalingSize = new System.Drawing.Size(32, 32);
                this.tlsp_LabResultGraph.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ts_btnSave,
            this.ts_btnClose});
                this.tlsp_LabResultGraph.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
                this.tlsp_LabResultGraph.Location = new System.Drawing.Point(0, 0);
                this.tlsp_LabResultGraph.Name = "tlsp_LabResultGraph";
                this.tlsp_LabResultGraph.Size = new System.Drawing.Size(396, 53);
                this.tlsp_LabResultGraph.TabIndex = 7;
                this.tlsp_LabResultGraph.Text = "toolStrip1";
                // 
                // ts_btnSave
                // 
                this.ts_btnSave.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.ts_btnSave.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
                this.ts_btnSave.Image = ((System.Drawing.Image)(resources.GetObject("ts_btnSave.Image")));
                this.ts_btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
                this.ts_btnSave.Name = "ts_btnSave";
                this.ts_btnSave.Size = new System.Drawing.Size(86, 50);
                this.ts_btnSave.Tag = "ShowResultGraph";
                this.ts_btnSave.Text = "Show &Graph";
                this.ts_btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
                this.ts_btnSave.Click += new System.EventHandler(this.btnShowGraphs_Click);
                // 
                // ts_btnClose
                // 
                this.ts_btnClose.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.ts_btnClose.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
                this.ts_btnClose.Image = ((System.Drawing.Image)(resources.GetObject("ts_btnClose.Image")));
                this.ts_btnClose.ImageTransparentColor = System.Drawing.Color.Magenta;
                this.ts_btnClose.Name = "ts_btnClose";
                this.ts_btnClose.Size = new System.Drawing.Size(43, 50);
                this.ts_btnClose.Tag = "Close";
                this.ts_btnClose.Text = "&Close";
                this.ts_btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
                // 
                // pnl_Base
                // 
                this.pnl_Base.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
                this.pnl_Base.Controls.Add(this.cmbResults);
                this.pnl_Base.Controls.Add(this.cmbTests);
                this.pnl_Base.Controls.Add(this.lbl_pnlBottom);
                this.pnl_Base.Controls.Add(this.dtTo);
                this.pnl_Base.Controls.Add(this.lbl_pnlLeft);
                this.pnl_Base.Controls.Add(this.dtFrom);
                this.pnl_Base.Controls.Add(this.lblResults);
                this.pnl_Base.Controls.Add(this.lbl_pnlRight);
                this.pnl_Base.Controls.Add(this.lblTests);
                this.pnl_Base.Controls.Add(this.lbl_pnlTop);
                this.pnl_Base.Controls.Add(this.lblTodate);
                this.pnl_Base.Controls.Add(this.lblFrmDate);
                this.pnl_Base.Dock = System.Windows.Forms.DockStyle.Fill;
                this.pnl_Base.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.pnl_Base.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
                this.pnl_Base.Location = new System.Drawing.Point(0, 53);
                this.pnl_Base.Name = "pnl_Base";
                this.pnl_Base.Padding = new System.Windows.Forms.Padding(3);
                this.pnl_Base.Size = new System.Drawing.Size(396, 105);
                this.pnl_Base.TabIndex = 2;
                // 
                // lbl_pnlBottom
                // 
                this.lbl_pnlBottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
                this.lbl_pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
                this.lbl_pnlBottom.Location = new System.Drawing.Point(4, 101);
                this.lbl_pnlBottom.Name = "lbl_pnlBottom";
                this.lbl_pnlBottom.Size = new System.Drawing.Size(388, 1);
                this.lbl_pnlBottom.TabIndex = 4;
                this.lbl_pnlBottom.Text = "label2";
                // 
                // lbl_pnlLeft
                // 
                this.lbl_pnlLeft.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
                this.lbl_pnlLeft.Dock = System.Windows.Forms.DockStyle.Left;
                this.lbl_pnlLeft.Location = new System.Drawing.Point(3, 4);
                this.lbl_pnlLeft.Name = "lbl_pnlLeft";
                this.lbl_pnlLeft.Size = new System.Drawing.Size(1, 98);
                this.lbl_pnlLeft.TabIndex = 3;
                this.lbl_pnlLeft.Text = "label4";
                // 
                // lbl_pnlRight
                // 
                this.lbl_pnlRight.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
                this.lbl_pnlRight.Dock = System.Windows.Forms.DockStyle.Right;
                this.lbl_pnlRight.Location = new System.Drawing.Point(392, 4);
                this.lbl_pnlRight.Name = "lbl_pnlRight";
                this.lbl_pnlRight.Size = new System.Drawing.Size(1, 98);
                this.lbl_pnlRight.TabIndex = 2;
                this.lbl_pnlRight.Text = "label3";
                // 
                // lbl_pnlTop
                // 
                this.lbl_pnlTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
                this.lbl_pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
                this.lbl_pnlTop.Location = new System.Drawing.Point(3, 3);
                this.lbl_pnlTop.Name = "lbl_pnlTop";
                this.lbl_pnlTop.Size = new System.Drawing.Size(390, 1);
                this.lbl_pnlTop.TabIndex = 0;
                this.lbl_pnlTop.Text = "label1";
                // 
                // frmLab_Graphs
                // 
                this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
                this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
                this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
                this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
                this.ClientSize = new System.Drawing.Size(396, 158);
                this.Controls.Add(this.pnl_Base);
                this.Controls.Add(this.Panel1);
                this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
                this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
                this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
                this.MaximizeBox = false;
                this.MinimizeBox = false;
                this.Name = "frmLab_Graphs";
                this.ShowInTaskbar = false;
                this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                this.Text = "Lab Result Graph";
                this.Load += new System.EventHandler(this.frmLab_Graphs_Load_1);
                this.Panel1.ResumeLayout(false);
                this.Panel1.PerformLayout();
                this.tlsp_LabResultGraph.ResumeLayout(false);
                this.tlsp_LabResultGraph.PerformLayout();
                this.pnl_Base.ResumeLayout(false);
                this.pnl_Base.PerformLayout();
                this.ResumeLayout(false);

		}
		internal System.Windows.Forms.Label lblResults;
		internal System.Windows.Forms.Label lblTests;
		internal System.Windows.Forms.Label lblTodate;
		internal System.Windows.Forms.Label lblFrmDate;
		internal System.Windows.Forms.DateTimePicker dtTo;
		internal System.Windows.Forms.DateTimePicker dtFrom;
		internal System.Windows.Forms.ComboBox cmbResults;
		internal System.Windows.Forms.ComboBox cmbTests;
		internal System.Windows.Forms.Panel Panel1;
		private gloGlobal.gloToolStripIgnoreFocus tlsp_LabResultGraph;
		private System.Windows.Forms.ToolStripButton ts_btnSave;
		internal System.Windows.Forms.ToolStripButton ts_btnClose;
		private System.Windows.Forms.Panel pnl_Base;
		private System.Windows.Forms.Label lbl_pnlBottom;
		private System.Windows.Forms.Label lbl_pnlLeft;
		private System.Windows.Forms.Label lbl_pnlRight;
		private System.Windows.Forms.Label lbl_pnlTop;
	}
	
}
