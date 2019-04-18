<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmViewMedHxDupInfo
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                Try
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(Me)
                Catch
                End Try
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmViewMedHxDupInfo))
        Me.pnlToostrip = New System.Windows.Forms.Panel()
        Me.ts_ViewButtons = New gloGlobal.gloToolStripIgnoreFocus()
        Me.tlbbtnClose = New System.Windows.Forms.ToolStripButton()
        Me.pnlMedHxDupDrgsFlexGrid = New System.Windows.Forms.Panel()
        Me.MedHxDupDrugFlexgrid = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.pnlInformationMessage = New System.Windows.Forms.Panel()
        Me.lblInformationMessage = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.pnlToostrip.SuspendLayout()
        Me.ts_ViewButtons.SuspendLayout()
        Me.pnlMedHxDupDrgsFlexGrid.SuspendLayout()
        CType(Me.MedHxDupDrugFlexgrid, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlInformationMessage.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlToostrip
        '
        Me.pnlToostrip.Controls.Add(Me.ts_ViewButtons)
        Me.pnlToostrip.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlToostrip.Location = New System.Drawing.Point(0, 0)
        Me.pnlToostrip.Name = "pnlToostrip"
        Me.pnlToostrip.Size = New System.Drawing.Size(399, 55)
        Me.pnlToostrip.TabIndex = 21
        '
        'ts_ViewButtons
        '
        Me.ts_ViewButtons.BackColor = System.Drawing.Color.Transparent
        Me.ts_ViewButtons.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.ts_ViewButtons.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ts_ViewButtons.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ts_ViewButtons.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_ViewButtons.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ts_ViewButtons.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.ts_ViewButtons.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tlbbtnClose})
        Me.ts_ViewButtons.Location = New System.Drawing.Point(0, 0)
        Me.ts_ViewButtons.Name = "ts_ViewButtons"
        Me.ts_ViewButtons.Size = New System.Drawing.Size(399, 55)
        Me.ts_ViewButtons.TabIndex = 1
        Me.ts_ViewButtons.Text = "ToolStrip1"
        '
        'tlbbtnClose
        '
        Me.tlbbtnClose.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlbbtnClose.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tlbbtnClose.Image = CType(resources.GetObject("tlbbtnClose.Image"), System.Drawing.Image)
        Me.tlbbtnClose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlbbtnClose.Name = "tlbbtnClose"
        Me.tlbbtnClose.Size = New System.Drawing.Size(43, 52)
        Me.tlbbtnClose.Text = "&Close"
        Me.tlbbtnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'pnlMedHxDupDrgsFlexGrid
        '
        Me.pnlMedHxDupDrgsFlexGrid.Controls.Add(Me.MedHxDupDrugFlexgrid)
        Me.pnlMedHxDupDrgsFlexGrid.Controls.Add(Me.Label4)
        Me.pnlMedHxDupDrgsFlexGrid.Controls.Add(Me.Label3)
        Me.pnlMedHxDupDrgsFlexGrid.Controls.Add(Me.Label2)
        Me.pnlMedHxDupDrgsFlexGrid.Controls.Add(Me.Label1)
        Me.pnlMedHxDupDrgsFlexGrid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMedHxDupDrgsFlexGrid.Location = New System.Drawing.Point(0, 116)
        Me.pnlMedHxDupDrgsFlexGrid.Name = "pnlMedHxDupDrgsFlexGrid"
        Me.pnlMedHxDupDrgsFlexGrid.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.pnlMedHxDupDrgsFlexGrid.Size = New System.Drawing.Size(399, 256)
        Me.pnlMedHxDupDrgsFlexGrid.TabIndex = 22
        '
        'MedHxDupDrugFlexgrid
        '
        Me.MedHxDupDrugFlexgrid.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        Me.MedHxDupDrugFlexgrid.AllowEditing = False
        Me.MedHxDupDrugFlexgrid.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.MultiColumn
        Me.MedHxDupDrugFlexgrid.AutoResize = True
        Me.MedHxDupDrugFlexgrid.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.MedHxDupDrugFlexgrid.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.MedHxDupDrugFlexgrid.ColumnInfo = "2,0,0,0,0,95,Columns:0{Width:250;Name:""ColDrugName"";Caption:""Drug"";}" & Global.Microsoft.VisualBasic.ChrW(9) & "1{Name:""ColS" & _
            "tartDate"";Caption:""Start Date"";}" & Global.Microsoft.VisualBasic.ChrW(9)
        Me.MedHxDupDrugFlexgrid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MedHxDupDrugFlexgrid.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MedHxDupDrugFlexgrid.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.MedHxDupDrugFlexgrid.Location = New System.Drawing.Point(4, 1)
        Me.MedHxDupDrugFlexgrid.Name = "MedHxDupDrugFlexgrid"
        Me.MedHxDupDrugFlexgrid.Rows.Count = 1
        Me.MedHxDupDrugFlexgrid.Rows.DefaultSize = 19
        Me.MedHxDupDrugFlexgrid.Size = New System.Drawing.Size(391, 251)
        Me.MedHxDupDrugFlexgrid.StyleInfo = resources.GetString("MedHxDupDrugFlexgrid.StyleInfo")
        Me.MedHxDupDrugFlexgrid.TabIndex = 2
        '
        'pnlInformationMessage
        '
        Me.pnlInformationMessage.Controls.Add(Me.Label6)
        Me.pnlInformationMessage.Controls.Add(Me.Label5)
        Me.pnlInformationMessage.Controls.Add(Me.Label7)
        Me.pnlInformationMessage.Controls.Add(Me.Label8)
        Me.pnlInformationMessage.Controls.Add(Me.lblInformationMessage)
        Me.pnlInformationMessage.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlInformationMessage.Location = New System.Drawing.Point(0, 55)
        Me.pnlInformationMessage.Name = "pnlInformationMessage"
        Me.pnlInformationMessage.Padding = New System.Windows.Forms.Padding(3)
        Me.pnlInformationMessage.Size = New System.Drawing.Size(399, 61)
        Me.pnlInformationMessage.TabIndex = 18
        '
        'lblInformationMessage
        '
        Me.lblInformationMessage.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblInformationMessage.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblInformationMessage.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblInformationMessage.Location = New System.Drawing.Point(3, 3)
        Me.lblInformationMessage.Name = "lblInformationMessage"
        Me.lblInformationMessage.Padding = New System.Windows.Forms.Padding(5)
        Me.lblInformationMessage.Size = New System.Drawing.Size(393, 55)
        Me.lblInformationMessage.TabIndex = 17
        Me.lblInformationMessage.Text = "Information Message"
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label8.Location = New System.Drawing.Point(395, 3)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(1, 55)
        Me.Label8.TabIndex = 16
        Me.Label8.Text = "label3"
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label2.Location = New System.Drawing.Point(3, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(393, 1)
        Me.Label2.TabIndex = 10
        Me.Label2.Text = "label2"
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label1.Location = New System.Drawing.Point(3, 252)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(393, 1)
        Me.Label1.TabIndex = 9
        Me.Label1.Text = "label2"
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(3, 3)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(1, 55)
        Me.Label7.TabIndex = 18
        Me.Label7.Text = "label4"
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(3, 1)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(1, 251)
        Me.Label3.TabIndex = 16
        Me.Label3.Text = "label4"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(395, 1)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(1, 251)
        Me.Label4.TabIndex = 17
        Me.Label4.Text = "label4"
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label5.Location = New System.Drawing.Point(4, 3)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(391, 1)
        Me.Label5.TabIndex = 19
        Me.Label5.Text = "label2"
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label6.Location = New System.Drawing.Point(4, 57)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(391, 1)
        Me.Label6.TabIndex = 20
        Me.Label6.Text = "label2"
        '
        'frmViewMedHxDupInfo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(399, 372)
        Me.Controls.Add(Me.pnlMedHxDupDrgsFlexGrid)
        Me.Controls.Add(Me.pnlInformationMessage)
        Me.Controls.Add(Me.pnlToostrip)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmViewMedHxDupInfo"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Duplicate Medication List"
        Me.pnlToostrip.ResumeLayout(False)
        Me.pnlToostrip.PerformLayout()
        Me.ts_ViewButtons.ResumeLayout(False)
        Me.ts_ViewButtons.PerformLayout()
        Me.pnlMedHxDupDrgsFlexGrid.ResumeLayout(False)
        CType(Me.MedHxDupDrugFlexgrid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlInformationMessage.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlToostrip As System.Windows.Forms.Panel
    Friend WithEvents ts_ViewButtons As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents tlbbtnClose As System.Windows.Forms.ToolStripButton
    Friend WithEvents pnlMedHxDupDrgsFlexGrid As System.Windows.Forms.Panel
    Protected WithEvents MedHxDupDrugFlexgrid As C1.Win.C1FlexGrid.C1FlexGrid
    Private WithEvents Label8 As System.Windows.Forms.Label
    Private WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents pnlInformationMessage As System.Windows.Forms.Panel
    Friend WithEvents lblInformationMessage As System.Windows.Forms.Label
    Private WithEvents Label4 As System.Windows.Forms.Label
    Private WithEvents Label3 As System.Windows.Forms.Label
    Private WithEvents Label6 As System.Windows.Forms.Label
    Private WithEvents Label5 As System.Windows.Forms.Label
    Private WithEvents Label7 As System.Windows.Forms.Label
End Class
