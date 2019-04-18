<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDxSnomed
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmDxSnomed))
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.tsTop = New gloGlobal.gloToolStripIgnoreFocus()
        Me.btnBrowseSnomed = New System.Windows.Forms.ToolStripButton()
        Me.tlbbtn_Save = New System.Windows.Forms.ToolStripButton()
        Me.tlbbtn_Close = New System.Windows.Forms.ToolStripButton()
        Me.pnlC1DxSnomed = New System.Windows.Forms.Panel()
        Me.c1DxSnomedList = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel1.SuspendLayout()
        Me.tsTop.SuspendLayout()
        Me.pnlC1DxSnomed.SuspendLayout()
        CType(Me.c1DxSnomedList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.tsTop)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(908, 57)
        Me.Panel1.TabIndex = 25
        '
        'tsTop
        '
        Me.tsTop.BackgroundImage = CType(resources.GetObject("tsTop.BackgroundImage"), System.Drawing.Image)
        Me.tsTop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tsTop.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.tsTop.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tsTop.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnBrowseSnomed, Me.tlbbtn_Save, Me.tlbbtn_Close})
        Me.tsTop.Location = New System.Drawing.Point(0, 0)
        Me.tsTop.Name = "tsTop"
        Me.tsTop.Size = New System.Drawing.Size(908, 53)
        Me.tsTop.TabIndex = 0
        Me.tsTop.Text = "ToolStrip1"
        '
        'btnBrowseSnomed
        '
        Me.btnBrowseSnomed.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBrowseSnomed.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnBrowseSnomed.Image = CType(resources.GetObject("btnBrowseSnomed.Image"), System.Drawing.Image)
        Me.btnBrowseSnomed.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnBrowseSnomed.Name = "btnBrowseSnomed"
        Me.btnBrowseSnomed.Size = New System.Drawing.Size(102, 50)
        Me.btnBrowseSnomed.Tag = "Close"
        Me.btnBrowseSnomed.Text = "Select Snomed"
        Me.btnBrowseSnomed.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tlbbtn_Save
        '
        Me.tlbbtn_Save.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlbbtn_Save.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tlbbtn_Save.Image = CType(resources.GetObject("tlbbtn_Save.Image"), System.Drawing.Image)
        Me.tlbbtn_Save.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlbbtn_Save.Name = "tlbbtn_Save"
        Me.tlbbtn_Save.Size = New System.Drawing.Size(66, 50)
        Me.tlbbtn_Save.Tag = "Save"
        Me.tlbbtn_Save.Text = "&Save&&Cls"
        Me.tlbbtn_Save.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlbbtn_Save.ToolTipText = "Save and close"
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
        'pnlC1DxSnomed
        '
        Me.pnlC1DxSnomed.Controls.Add(Me.c1DxSnomedList)
        Me.pnlC1DxSnomed.Controls.Add(Me.Label5)
        Me.pnlC1DxSnomed.Controls.Add(Me.Label4)
        Me.pnlC1DxSnomed.Controls.Add(Me.Label3)
        Me.pnlC1DxSnomed.Controls.Add(Me.Label1)
        Me.pnlC1DxSnomed.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlC1DxSnomed.Location = New System.Drawing.Point(0, 57)
        Me.pnlC1DxSnomed.Name = "pnlC1DxSnomed"
        Me.pnlC1DxSnomed.Padding = New System.Windows.Forms.Padding(3)
        Me.pnlC1DxSnomed.Size = New System.Drawing.Size(908, 560)
        Me.pnlC1DxSnomed.TabIndex = 2
        '
        'c1DxSnomedList
        '
        Me.c1DxSnomedList.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.c1DxSnomedList.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.c1DxSnomedList.ColumnInfo = resources.GetString("c1DxSnomedList.ColumnInfo")
        Me.c1DxSnomedList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.c1DxSnomedList.ExtendLastCol = True
        Me.c1DxSnomedList.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.c1DxSnomedList.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcrossOut
        Me.c1DxSnomedList.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcrossOut
        Me.c1DxSnomedList.Location = New System.Drawing.Point(4, 4)
        Me.c1DxSnomedList.Name = "c1DxSnomedList"
        Me.c1DxSnomedList.Rows.Count = 1
        Me.c1DxSnomedList.Rows.DefaultSize = 21
        Me.c1DxSnomedList.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.c1DxSnomedList.ShowCellLabels = True
        Me.c1DxSnomedList.Size = New System.Drawing.Size(900, 552)
        Me.c1DxSnomedList.StyleInfo = resources.GetString("c1DxSnomedList.StyleInfo")
        Me.c1DxSnomedList.TabIndex = 23
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label5.Location = New System.Drawing.Point(904, 4)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(1, 552)
        Me.Label5.TabIndex = 41
        Me.Label5.Text = "label4"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label4.Location = New System.Drawing.Point(3, 4)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(1, 552)
        Me.Label4.TabIndex = 40
        Me.Label4.Text = "label4"
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label3.Location = New System.Drawing.Point(3, 3)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(902, 1)
        Me.Label3.TabIndex = 37
        Me.Label3.Text = "label1"
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label1.Location = New System.Drawing.Point(3, 556)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(902, 1)
        Me.Label1.TabIndex = 36
        Me.Label1.Text = "label1"
        '
        'frmDxSnomed
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(908, 617)
        Me.Controls.Add(Me.pnlC1DxSnomed)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmDxSnomed"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Dx Snomed"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.tsTop.ResumeLayout(False)
        Me.tsTop.PerformLayout()
        Me.pnlC1DxSnomed.ResumeLayout(False)
        CType(Me.c1DxSnomedList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents tsTop As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents tlbbtn_Save As System.Windows.Forms.ToolStripButton
    Friend WithEvents tlbbtn_Close As System.Windows.Forms.ToolStripButton
    Friend WithEvents pnlC1DxSnomed As System.Windows.Forms.Panel
    Friend WithEvents c1DxSnomedList As C1.Win.C1FlexGrid.C1FlexGrid
    Private WithEvents Label5 As System.Windows.Forms.Label
    Private WithEvents Label4 As System.Windows.Forms.Label
    Private WithEvents Label3 As System.Windows.Forms.Label
    Private WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnBrowseSnomed As System.Windows.Forms.ToolStripButton
End Class
