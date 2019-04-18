<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPrintDialog
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
                Try
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(Me)
                Catch
                End Try

                Try
                    If (IsNothing(PrintDialog1) = False) Then
                        PrintDialog1.Dispose()
                        PrintDialog1 = Nothing
                    End If
                Catch ex As Exception

                End Try
                Try
                    If (IsNothing(printDocument1) = False) Then
                        gloGlobal.cEventHelper.RemoveAllEventHandlers(printDocument1)
                        printDocument1.Dispose()
                        printDocument1 = Nothing
                    End If
                Catch ex As Exception

                End Try
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPrintDialog))
        Me.tblbtn_Print_32 = New System.Windows.Forms.ToolStripButton()
        Me.tlsPrintDialog = New gloGlobal.gloToolStripIgnoreFocus()
        Me.tlbbtn_Close = New System.Windows.Forms.ToolStripButton()
        Me.lblPrint = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.prgGeneratefile = New System.Windows.Forms.ProgressBar()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.printDocument1 = New System.Drawing.Printing.PrintDocument()
        Me.PrintDialog1 = New System.Windows.Forms.PrintDialog()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.numCustomDPIValue = New System.Windows.Forms.NumericUpDown()
        Me.rbDefaultDPI = New System.Windows.Forms.RadioButton()
        Me.rbCustomDPI = New System.Windows.Forms.RadioButton()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.tlsPrintDialog.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.numCustomDPIValue, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'tblbtn_Print_32
        '
        Me.tblbtn_Print_32.BackColor = System.Drawing.Color.Transparent
        Me.tblbtn_Print_32.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tblbtn_Print_32.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblbtn_Print_32.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tblbtn_Print_32.Image = CType(resources.GetObject("tblbtn_Print_32.Image"), System.Drawing.Image)
        Me.tblbtn_Print_32.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblbtn_Print_32.Margin = New System.Windows.Forms.Padding(1)
        Me.tblbtn_Print_32.Name = "tblbtn_Print_32"
        Me.tblbtn_Print_32.Size = New System.Drawing.Size(49, 50)
        Me.tblbtn_Print_32.Tag = "Print"
        Me.tblbtn_Print_32.Text = " Print "
        Me.tblbtn_Print_32.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tblbtn_Print_32.ToolTipText = "Print"
        '
        'tlsPrintDialog
        '
        Me.tlsPrintDialog.BackColor = System.Drawing.Color.Transparent
        Me.tlsPrintDialog.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tlsPrintDialog.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlsPrintDialog.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlsPrintDialog.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tlsPrintDialog.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tblbtn_Print_32, Me.tlbbtn_Close})
        Me.tlsPrintDialog.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.tlsPrintDialog.Location = New System.Drawing.Point(0, 0)
        Me.tlsPrintDialog.Name = "tlsPrintDialog"
        Me.tlsPrintDialog.Size = New System.Drawing.Size(374, 53)
        Me.tlsPrintDialog.TabIndex = 1
        Me.tlsPrintDialog.Text = "toolStrip1"
        '
        'tlbbtn_Close
        '
        Me.tlbbtn_Close.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlbbtn_Close.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tlbbtn_Close.Image = CType(resources.GetObject("tlbbtn_Close.Image"), System.Drawing.Image)
        Me.tlbbtn_Close.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlbbtn_Close.Name = "tlbbtn_Close"
        Me.tlbbtn_Close.Size = New System.Drawing.Size(43, 50)
        Me.tlbbtn_Close.Tag = "Close"
        Me.tlbbtn_Close.Text = "&Close"
        Me.tlbbtn_Close.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'lblPrint
        '
        Me.lblPrint.BackColor = System.Drawing.Color.Transparent
        Me.lblPrint.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblPrint.Font = New System.Drawing.Font("Segoe UI", 14.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPrint.Location = New System.Drawing.Point(4, 4)
        Me.lblPrint.Name = "lblPrint"
        Me.lblPrint.Size = New System.Drawing.Size(366, 191)
        Me.lblPrint.TabIndex = 9
        Me.lblPrint.Text = "Ready for Printing..."
        Me.lblPrint.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.prgGeneratefile)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.lblPrint)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 106)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Padding = New System.Windows.Forms.Padding(3)
        Me.Panel1.Size = New System.Drawing.Size(374, 198)
        Me.Panel1.TabIndex = 10
        '
        'prgGeneratefile
        '
        Me.prgGeneratefile.BackColor = System.Drawing.Color.White
        Me.prgGeneratefile.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.prgGeneratefile.ForeColor = System.Drawing.Color.LimeGreen
        Me.prgGeneratefile.Location = New System.Drawing.Point(4, 180)
        Me.prgGeneratefile.Maximum = 250
        Me.prgGeneratefile.Name = "prgGeneratefile"
        Me.prgGeneratefile.Size = New System.Drawing.Size(366, 14)
        Me.prgGeneratefile.Step = 25
        Me.prgGeneratefile.TabIndex = 11
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label4.Location = New System.Drawing.Point(4, 194)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(366, 1)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "Label4"
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label3.Location = New System.Drawing.Point(4, 3)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(366, 1)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Label3"
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label2.Location = New System.Drawing.Point(370, 3)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(1, 192)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Label2"
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label1.Location = New System.Drawing.Point(3, 3)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(1, 192)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Label1"
        '
        'printDocument1
        '
        '
        'PrintDialog1
        '
        Me.PrintDialog1.UseEXDialog = True
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.Label6)
        Me.Panel2.Controls.Add(Me.numCustomDPIValue)
        Me.Panel2.Controls.Add(Me.ProgressBar1)
        Me.Panel2.Controls.Add(Me.rbDefaultDPI)
        Me.Panel2.Controls.Add(Me.Label5)
        Me.Panel2.Controls.Add(Me.rbCustomDPI)
        Me.Panel2.Controls.Add(Me.Label7)
        Me.Panel2.Controls.Add(Me.Label8)
        Me.Panel2.Controls.Add(Me.Label9)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 53)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Padding = New System.Windows.Forms.Padding(3, 3, 3, 0)
        Me.Panel2.Size = New System.Drawing.Size(374, 53)
        Me.Panel2.TabIndex = 11
        '
        'numCustomDPIValue
        '
        Me.numCustomDPIValue.Increment = New Decimal(New Integer() {50, 0, 0, 0})
        Me.numCustomDPIValue.Location = New System.Drawing.Point(266, 25)
        Me.numCustomDPIValue.Maximum = New Decimal(New Integer() {10000, 0, 0, 0})
        Me.numCustomDPIValue.Minimum = New Decimal(New Integer() {50, 0, 0, 0})
        Me.numCustomDPIValue.Name = "numCustomDPIValue"
        Me.numCustomDPIValue.Size = New System.Drawing.Size(78, 22)
        Me.numCustomDPIValue.TabIndex = 14
        Me.numCustomDPIValue.Value = New Decimal(New Integer() {150, 0, 0, 0})
        '
        'rbDefaultDPI
        '
        Me.rbDefaultDPI.AutoSize = True
        Me.rbDefaultDPI.Location = New System.Drawing.Point(27, 27)
        Me.rbDefaultDPI.Name = "rbDefaultDPI"
        Me.rbDefaultDPI.Size = New System.Drawing.Size(87, 18)
        Me.rbDefaultDPI.TabIndex = 13
        Me.rbDefaultDPI.TabStop = True
        Me.rbDefaultDPI.Text = "Default DPI"
        Me.rbDefaultDPI.UseVisualStyleBackColor = True
        '
        'rbCustomDPI
        '
        Me.rbCustomDPI.AutoSize = True
        Me.rbCustomDPI.Location = New System.Drawing.Point(174, 27)
        Me.rbCustomDPI.Name = "rbCustomDPI"
        Me.rbCustomDPI.Size = New System.Drawing.Size(89, 18)
        Me.rbCustomDPI.TabIndex = 12
        Me.rbCustomDPI.TabStop = True
        Me.rbCustomDPI.Text = "Custom DPI"
        Me.rbCustomDPI.UseVisualStyleBackColor = True
        '
        'ProgressBar1
        '
        Me.ProgressBar1.BackColor = System.Drawing.Color.White
        Me.ProgressBar1.ForeColor = System.Drawing.Color.LimeGreen
        Me.ProgressBar1.Location = New System.Drawing.Point(11, 129)
        Me.ProgressBar1.Maximum = 250
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(352, 14)
        Me.ProgressBar1.Step = 25
        Me.ProgressBar1.TabIndex = 11
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label5.Location = New System.Drawing.Point(4, 52)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(366, 1)
        Me.Label5.TabIndex = 3
        Me.Label5.Text = "Label5"
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label7.Location = New System.Drawing.Point(4, 3)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(366, 1)
        Me.Label7.TabIndex = 2
        Me.Label7.Text = "Label7"
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label8.Location = New System.Drawing.Point(370, 3)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(1, 50)
        Me.Label8.TabIndex = 1
        Me.Label8.Text = "Label8"
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label9.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label9.Location = New System.Drawing.Point(3, 3)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(1, 50)
        Me.Label9.TabIndex = 0
        Me.Label9.Text = "Label9"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(8, 8)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(75, 14)
        Me.Label6.TabIndex = 12
        Me.Label6.Text = "Printer DPI"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'frmPrintDialog
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(374, 304)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.tlsPrintDialog)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmPrintDialog"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Print"
        Me.tlsPrintDialog.ResumeLayout(False)
        Me.tlsPrintDialog.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.numCustomDPIValue, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents tblbtn_Print_32 As System.Windows.Forms.ToolStripButton
    Private WithEvents tlsPrintDialog As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents tlbbtn_Close As System.Windows.Forms.ToolStripButton
    Friend WithEvents lblPrint As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Private WithEvents printDocument1 As System.Drawing.Printing.PrintDocument
    Friend PrintDialog1 As System.Windows.Forms.PrintDialog
    Friend WithEvents prgGeneratefile As System.Windows.Forms.ProgressBar
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents numCustomDPIValue As System.Windows.Forms.NumericUpDown
    Friend WithEvents rbDefaultDPI As System.Windows.Forms.RadioButton
    Friend WithEvents rbCustomDPI As System.Windows.Forms.RadioButton
    Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
End Class
