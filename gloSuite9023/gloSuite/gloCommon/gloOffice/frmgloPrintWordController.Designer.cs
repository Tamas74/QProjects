using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;
namespace gloOffice
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    partial class frmgloPrintWordProgressController : System.Windows.Forms.Form
    {

        //Form overrides dispose to clean up the component list.
        [System.Diagnostics.DebuggerNonUserCode()]
        protected override void Dispose(bool disposing)
        {
            try
            {
                try
                {
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(this);

                }
                catch //(Exception ex)
                {
                }

                if (disposing && components != null)
                {
                    components.Dispose();
                }
            }
            finally
            {
                base.Dispose(disposing);
            }
        }
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmgloPrintWordProgressController));
            this.lblPrinterNameValue = new System.Windows.Forms.Label();
            this.lblPrinterName = new System.Windows.Forms.Label();
            this.lblPages = new System.Windows.Forms.Label();
            this.lblPageNoOfDocument = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnRestart = new System.Windows.Forms.Button();
            this.btnPause = new System.Windows.Forms.Button();
            this.btnPlay = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.pbDocument = new System.Windows.Forms.ProgressBar();
            this.lblCopies = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.Label1 = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label3 = new System.Windows.Forms.Label();
            this.Label4 = new System.Windows.Forms.Label();
            this.Label5 = new System.Windows.Forms.Label();
            this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panel3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblPrinterNameValue
            // 
            this.lblPrinterNameValue.AutoEllipsis = true;
            this.lblPrinterNameValue.AutoSize = true;
            this.lblPrinterNameValue.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblPrinterNameValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPrinterNameValue.Location = new System.Drawing.Point(85, 10);
            this.lblPrinterNameValue.Name = "lblPrinterNameValue";
            this.lblPrinterNameValue.Size = new System.Drawing.Size(77, 13);
            this.lblPrinterNameValue.TabIndex = 8;
            this.lblPrinterNameValue.Text = "printer name";
            // 
            // lblPrinterName
            // 
            this.lblPrinterName.AutoSize = true;
            this.lblPrinterName.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblPrinterName.Location = new System.Drawing.Point(10, 10);
            this.lblPrinterName.Name = "lblPrinterName";
            this.lblPrinterName.Size = new System.Drawing.Size(75, 14);
            this.lblPrinterName.TabIndex = 7;
            this.lblPrinterName.Text = "Printing To :";
            // 
            // lblPages
            // 
            this.lblPages.AutoEllipsis = true;
            this.lblPages.AutoSize = true;
            this.lblPages.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblPages.Location = new System.Drawing.Point(10, 10);
            this.lblPages.Name = "lblPages";
            this.lblPages.Padding = new System.Windows.Forms.Padding(2, 0, 0, 0);
            this.lblPages.Size = new System.Drawing.Size(88, 14);
            this.lblPages.TabIndex = 2;
            this.lblPages.Text = "Please Wait... ";
            // 
            // lblPageNoOfDocument
            // 
            this.lblPageNoOfDocument.AutoSize = true;
            this.lblPageNoOfDocument.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblPageNoOfDocument.Location = new System.Drawing.Point(336, 10);
            this.lblPageNoOfDocument.Name = "lblPageNoOfDocument";
            this.lblPageNoOfDocument.Size = new System.Drawing.Size(0, 14);
            this.lblPageNoOfDocument.TabIndex = 4;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btnRestart);
            this.panel3.Controls.Add(this.btnPause);
            this.panel3.Controls.Add(this.btnPlay);
            this.panel3.Controls.Add(this.btnCancel);
            this.panel3.Controls.Add(this.lblPages);
            this.panel3.Controls.Add(this.pbDocument);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(1, 36);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(10);
            this.panel3.Size = new System.Drawing.Size(349, 91);
            this.panel3.TabIndex = 4;
            // 
            // btnRestart
            // 
            this.btnRestart.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnRestart.Enabled = false;
            this.btnRestart.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnRestart.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnRestart.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnRestart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRestart.Image = ((System.Drawing.Image)(resources.GetObject("btnRestart.Image")));
            this.btnRestart.Location = new System.Drawing.Point(215, 24);
            this.btnRestart.Name = "btnRestart";
            this.btnRestart.Size = new System.Drawing.Size(31, 39);
            this.btnRestart.TabIndex = 7;
            this.ToolTip1.SetToolTip(this.btnRestart, "Restart");
            this.btnRestart.UseVisualStyleBackColor = true;
            this.btnRestart.Click += new System.EventHandler(this.btnRestart_Click);
            // 
            // btnPause
            // 
            this.btnPause.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnPause.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnPause.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnPause.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnPause.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPause.Image = ((System.Drawing.Image)(resources.GetObject("btnPause.Image")));
            this.btnPause.Location = new System.Drawing.Point(246, 24);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(31, 39);
            this.btnPause.TabIndex = 8;
            this.ToolTip1.SetToolTip(this.btnPause, "Pause");
            this.btnPause.UseVisualStyleBackColor = true;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // btnPlay
            // 
            this.btnPlay.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnPlay.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnPlay.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnPlay.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnPlay.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPlay.Image = ((System.Drawing.Image)(resources.GetObject("btnPlay.Image")));
            this.btnPlay.Location = new System.Drawing.Point(277, 24);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(31, 39);
            this.btnPlay.TabIndex = 10;
            this.ToolTip1.SetToolTip(this.btnPlay, "Play");
            this.btnPlay.UseVisualStyleBackColor = true;
            this.btnPlay.Visible = false;
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
            this.btnCancel.Location = new System.Drawing.Point(308, 24);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(31, 39);
            this.btnCancel.TabIndex = 5;
            this.ToolTip1.SetToolTip(this.btnCancel, "Close");
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // pbDocument
            // 
            this.pbDocument.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pbDocument.Location = new System.Drawing.Point(10, 63);
            this.pbDocument.Name = "pbDocument";
            this.pbDocument.Size = new System.Drawing.Size(329, 18);
            this.pbDocument.TabIndex = 9;
            // 
            // lblCopies
            // 
            this.lblCopies.AutoEllipsis = true;
            this.lblCopies.AutoSize = true;
            this.lblCopies.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblCopies.Location = new System.Drawing.Point(336, 10);
            this.lblCopies.Name = "lblCopies";
            this.lblCopies.Padding = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.lblCopies.Size = new System.Drawing.Size(3, 14);
            this.lblCopies.TabIndex = 3;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblPageNoOfDocument);
            this.panel1.Controls.Add(this.lblCopies);
            this.panel1.Controls.Add(this.lblPrinterNameValue);
            this.panel1.Controls.Add(this.lblPrinterName);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(1, 1);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(10);
            this.panel1.Size = new System.Drawing.Size(349, 35);
            this.panel1.TabIndex = 4;
            // 
            // Label1
            // 
            this.Label1.BackColor = System.Drawing.Color.Silver;
            this.Label1.Dock = System.Windows.Forms.DockStyle.Right;
            this.Label1.Location = new System.Drawing.Point(350, 1);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(1, 126);
            this.Label1.TabIndex = 5;
            // 
            // Label2
            // 
            this.Label2.BackColor = System.Drawing.Color.Silver;
            this.Label2.Dock = System.Windows.Forms.DockStyle.Left;
            this.Label2.Location = new System.Drawing.Point(0, 1);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(1, 126);
            this.Label2.TabIndex = 6;
            // 
            // Label3
            // 
            this.Label3.BackColor = System.Drawing.Color.Silver;
            this.Label3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Label3.Location = new System.Drawing.Point(0, 127);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(351, 1);
            this.Label3.TabIndex = 7;
            // 
            // Label4
            // 
            this.Label4.BackColor = System.Drawing.Color.Silver;
            this.Label4.Dock = System.Windows.Forms.DockStyle.Top;
            this.Label4.Location = new System.Drawing.Point(0, 0);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(351, 1);
            this.Label4.TabIndex = 8;
            // 
            // Label5
            // 
            this.Label5.BackColor = System.Drawing.Color.Silver;
            this.Label5.Dock = System.Windows.Forms.DockStyle.Top;
            this.Label5.Location = new System.Drawing.Point(1, 36);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(349, 1);
            this.Label5.TabIndex = 10;
            // 
            // frmgloPrintWordProgressController
            // 
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(351, 128);
            this.Controls.Add(this.Label5);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.Label3);
            this.Controls.Add(this.Label4);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmgloPrintWordProgressController";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmgloPrintWordProgressController_FormClosed);
            this.Shown += new System.EventHandler(this.frmgloPrintWordProgressController_Shown);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }
        private System.Windows.Forms.Label lblPrinterNameValue;
        private System.Windows.Forms.Label lblPrinterName;
        private System.Windows.Forms.Label lblPages;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ProgressBar pbDocument;
      //  private System.Windows.Forms.Button withEventsField_btnCancel;
        private System.Windows.Forms.Button btnCancel;
        //{
        //    get { return withEventsField_btnCancel; }
        //    set
        //    {
        //        if (withEventsField_btnCancel != null)
        //        {
        //            withEventsField_btnCancel.Click -= btnCancel_Click;
        //        }
        //        withEventsField_btnCancel = value;
        //        if (withEventsField_btnCancel != null)
        //        {
        //            withEventsField_btnCancel.Click += btnCancel_Click;
        //        }
        //    }
        //}
        // private System.Windows.Forms.Button withEventsField_btnPause;
        private System.Windows.Forms.Button btnPause;
        //{
        //    get { return withEventsField_btnPause; }
        //    set
        //    {
        //        if (withEventsField_btnPause != null)
        //        {
        //            withEventsField_btnPause.Click -= btnPause_Click;
        //        }
        //        withEventsField_btnPause = value;
        //        if (withEventsField_btnPause != null)
        //        {
        //            withEventsField_btnPause.Click += btnPause_Click;
        //        }
        //    }
        //}
       // private System.Windows.Forms.Button withEventsField_btnRestart;
        private System.Windows.Forms.Button btnRestart;
        //{
        //    get { return withEventsField_btnRestart; }
        //    set
        //    {
        //        if (withEventsField_btnRestart != null)
        //        {
        //            withEventsField_btnRestart.Click -= btnRestart_Click;
        //        }
        //        withEventsField_btnRestart = value;
        //        if (withEventsField_btnRestart != null)
        //        {
        //            withEventsField_btnRestart.Click += btnRestart_Click;
        //        }
        //    }

        //}
        //Required by the Windows Form Designer
        private System.ComponentModel.IContainer components;
        internal System.Windows.Forms.Label lblPageNoOfDocument;
        private System.Windows.Forms.Button btnPlay;
        private System.Windows.Forms.Label lblCopies;
        private System.Windows.Forms.Panel panel1;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.Label Label4;
        internal System.Windows.Forms.Label Label5;

        internal System.Windows.Forms.ToolTip ToolTip1;
        //NOTE: The following procedure is required by the Windows Form Designer
        //It can be modified using the Windows Form Designer.  
        //Do not modify it using the code editor.
        //<System.Diagnostics.DebuggerStepThrough()> _
        //Private Sub InitializeComponent()
        //    components = New System.ComponentModel.Container
        //    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        //    Me.Text = "frmgloPrintWordController"
        //End Sub
    }

}