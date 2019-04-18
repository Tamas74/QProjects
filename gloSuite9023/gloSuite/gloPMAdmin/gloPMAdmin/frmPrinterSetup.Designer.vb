<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPrinterSetup
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPrinterSetup))
        Me.pnltlsStrip = New System.Windows.Forms.Panel()
        Me.tls_SetupResource = New System.Windows.Forms.ToolStrip()
        Me.tsb_Select = New System.Windows.Forms.ToolStripButton()
        Me.tsb_Close = New System.Windows.Forms.ToolStripButton()
        Me.pnlImport = New System.Windows.Forms.Panel()
        Me.C1PrinterList = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.label3 = New System.Windows.Forms.Label()
        Me.label2 = New System.Windows.Forms.Label()
        Me.label1 = New System.Windows.Forms.Label()
        Me.label59 = New System.Windows.Forms.Label()
        Me.dlgBrowseFile = New System.Windows.Forms.OpenFileDialog()
        Me.pnltlsStrip.SuspendLayout()
        Me.tls_SetupResource.SuspendLayout()
        Me.pnlImport.SuspendLayout()
        CType(Me.C1PrinterList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnltlsStrip
        '
        Me.pnltlsStrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnltlsStrip.Controls.Add(Me.tls_SetupResource)
        Me.pnltlsStrip.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnltlsStrip.Location = New System.Drawing.Point(0, 0)
        Me.pnltlsStrip.Name = "pnltlsStrip"
        Me.pnltlsStrip.Size = New System.Drawing.Size(526, 54)
        Me.pnltlsStrip.TabIndex = 2
        '
        'tls_SetupResource
        '
        Me.tls_SetupResource.BackColor = System.Drawing.Color.Transparent
        Me.tls_SetupResource.BackgroundImage = Global.gloPMAdmin.My.Resources.Resources.Img_Toolstrip
        Me.tls_SetupResource.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tls_SetupResource.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tls_SetupResource.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.tls_SetupResource.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tls_SetupResource.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsb_Select, Me.tsb_Close})
        Me.tls_SetupResource.Location = New System.Drawing.Point(0, 0)
        Me.tls_SetupResource.Name = "tls_SetupResource"
        Me.tls_SetupResource.Size = New System.Drawing.Size(526, 53)
        Me.tls_SetupResource.TabIndex = 1
        Me.tls_SetupResource.TabStop = True
        Me.tls_SetupResource.Text = "toolStrip1"
        '
        'tsb_Select
        '
        Me.tsb_Select.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tsb_Select.Image = CType(resources.GetObject("tsb_Select.Image"), System.Drawing.Image)
        Me.tsb_Select.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsb_Select.Name = "tsb_Select"
        Me.tsb_Select.Size = New System.Drawing.Size(48, 50)
        Me.tsb_Select.Tag = "Select"
        Me.tsb_Select.Text = "&Select"
        Me.tsb_Select.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tsb_Select.ToolTipText = "Select New Printer to Add"
        '
        'tsb_Close
        '
        Me.tsb_Close.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tsb_Close.Image = CType(resources.GetObject("tsb_Close.Image"), System.Drawing.Image)
        Me.tsb_Close.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsb_Close.Name = "tsb_Close"
        Me.tsb_Close.Size = New System.Drawing.Size(43, 50)
        Me.tsb_Close.Tag = "Cancel"
        Me.tsb_Close.Text = "&Close"
        Me.tsb_Close.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'pnlImport
        '
        Me.pnlImport.Controls.Add(Me.C1PrinterList)
        Me.pnlImport.Controls.Add(Me.label3)
        Me.pnlImport.Controls.Add(Me.label2)
        Me.pnlImport.Controls.Add(Me.label1)
        Me.pnlImport.Controls.Add(Me.label59)
        Me.pnlImport.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlImport.Location = New System.Drawing.Point(0, 54)
        Me.pnlImport.Name = "pnlImport"
        Me.pnlImport.Padding = New System.Windows.Forms.Padding(3)
        Me.pnlImport.Size = New System.Drawing.Size(526, 228)
        Me.pnlImport.TabIndex = 3
        '
        'C1PrinterList
        '
        Me.C1PrinterList.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        Me.C1PrinterList.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.C1PrinterList.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.C1PrinterList.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.C1PrinterList.ColumnInfo = "0,0,0,0,0,95,Columns:"
        Me.C1PrinterList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.C1PrinterList.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.C1PrinterList.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.C1PrinterList.HighLight = C1.Win.C1FlexGrid.HighLightEnum.WithFocus
        Me.C1PrinterList.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.None
        Me.C1PrinterList.Location = New System.Drawing.Point(4, 4)
        Me.C1PrinterList.Name = "C1PrinterList"
        Me.C1PrinterList.Rows.Count = 1
        Me.C1PrinterList.Rows.DefaultSize = 19
        Me.C1PrinterList.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.C1PrinterList.ShowCellLabels = True
        Me.C1PrinterList.Size = New System.Drawing.Size(518, 220)
        Me.C1PrinterList.StyleInfo = resources.GetString("C1PrinterList.StyleInfo")
        Me.C1PrinterList.TabIndex = 30
        '
        'label3
        '
        Me.label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label3.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.label3.Location = New System.Drawing.Point(4, 224)
        Me.label3.Name = "label3"
        Me.label3.Size = New System.Drawing.Size(518, 1)
        Me.label3.TabIndex = 29
        Me.label3.Text = "label3"
        '
        'label2
        '
        Me.label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label2.Dock = System.Windows.Forms.DockStyle.Top
        Me.label2.Location = New System.Drawing.Point(4, 3)
        Me.label2.Name = "label2"
        Me.label2.Size = New System.Drawing.Size(518, 1)
        Me.label2.TabIndex = 28
        Me.label2.Text = "label2"
        '
        'label1
        '
        Me.label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label1.Dock = System.Windows.Forms.DockStyle.Right
        Me.label1.Location = New System.Drawing.Point(522, 3)
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(1, 222)
        Me.label1.TabIndex = 27
        Me.label1.Text = "label1"
        '
        'label59
        '
        Me.label59.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label59.Dock = System.Windows.Forms.DockStyle.Left
        Me.label59.Location = New System.Drawing.Point(3, 3)
        Me.label59.Name = "label59"
        Me.label59.Size = New System.Drawing.Size(1, 222)
        Me.label59.TabIndex = 26
        Me.label59.Text = "label59"
        '
        'frmPrinterSetup
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(526, 282)
        Me.Controls.Add(Me.pnlImport)
        Me.Controls.Add(Me.pnltlsStrip)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmPrinterSetup"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Printer List"
        Me.pnltlsStrip.ResumeLayout(False)
        Me.pnltlsStrip.PerformLayout()
        Me.tls_SetupResource.ResumeLayout(False)
        Me.tls_SetupResource.PerformLayout()
        Me.pnlImport.ResumeLayout(False)
        CType(Me.C1PrinterList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents pnltlsStrip As System.Windows.Forms.Panel
    Private WithEvents pnlImport As System.Windows.Forms.Panel
    Private WithEvents label3 As System.Windows.Forms.Label
    Private WithEvents label2 As System.Windows.Forms.Label
    Private WithEvents label1 As System.Windows.Forms.Label
    Private WithEvents label59 As System.Windows.Forms.Label
    Private WithEvents dlgBrowseFile As System.Windows.Forms.OpenFileDialog
    Private WithEvents C1PrinterList As C1.Win.C1FlexGrid.C1FlexGrid
    Private WithEvents tls_SetupResource As System.Windows.Forms.ToolStrip
    Private WithEvents tsb_Select As System.Windows.Forms.ToolStripButton
    Private WithEvents tsb_Close As System.Windows.Forms.ToolStripButton
End Class
