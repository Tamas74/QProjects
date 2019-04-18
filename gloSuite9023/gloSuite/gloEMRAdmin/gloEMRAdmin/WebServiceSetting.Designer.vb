<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmWebServiceSetting
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmWebServiceSetting))
        Me.tstrip = New System.Windows.Forms.ToolStrip()
        Me.TsSaveBtn = New System.Windows.Forms.ToolStripButton()
        Me.TsCancelBtn = New System.Windows.Forms.ToolStripButton()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.c1Setting = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.tstrip.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.c1Setting, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'tstrip
        '
        Me.tstrip.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.tstrip.BackgroundImage = CType(resources.GetObject("tstrip.BackgroundImage"), System.Drawing.Image)
        Me.tstrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tstrip.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tstrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.tstrip.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tstrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.TsSaveBtn, Me.TsCancelBtn})
        Me.tstrip.Location = New System.Drawing.Point(0, 0)
        Me.tstrip.Name = "tstrip"
        Me.tstrip.Size = New System.Drawing.Size(464, 53)
        Me.tstrip.TabIndex = 2
        Me.tstrip.Text = "ToolStrip1"
        '
        'TsSaveBtn
        '
        Me.TsSaveBtn.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TsSaveBtn.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.TsSaveBtn.Image = CType(resources.GetObject("TsSaveBtn.Image"), System.Drawing.Image)
        Me.TsSaveBtn.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.TsSaveBtn.Name = "TsSaveBtn"
        Me.TsSaveBtn.Size = New System.Drawing.Size(66, 50)
        Me.TsSaveBtn.Text = "&Save&&Cls"
        Me.TsSaveBtn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.TsSaveBtn.ToolTipText = "Save and Close"
        '
        'TsCancelBtn
        '
        Me.TsCancelBtn.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TsCancelBtn.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.TsCancelBtn.Image = CType(resources.GetObject("TsCancelBtn.Image"), System.Drawing.Image)
        Me.TsCancelBtn.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.TsCancelBtn.Name = "TsCancelBtn"
        Me.TsCancelBtn.Size = New System.Drawing.Size(43, 50)
        Me.TsCancelBtn.Text = "&Close"
        Me.TsCancelBtn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.TsCancelBtn.ToolTipText = "Close"
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Transparent
        Me.Panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel3.Controls.Add(Me.c1Setting)
        Me.Panel3.Controls.Add(Me.Label2)
        Me.Panel3.Controls.Add(Me.Label3)
        Me.Panel3.Controls.Add(Me.Label4)
        Me.Panel3.Controls.Add(Me.Label9)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel3.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel3.Location = New System.Drawing.Point(0, 53)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(464, 88)
        Me.Panel3.TabIndex = 3
        '
        'c1Setting
        '
        Me.c1Setting.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.None
        Me.c1Setting.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None
        Me.c1Setting.AutoGenerateColumns = False
        Me.c1Setting.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.c1Setting.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.c1Setting.ColumnInfo = "2,0,0,0,0,105,Columns:0{Width:356;Name:""SettingName"";Caption:""Setting Name"";}" & Global.Microsoft.VisualBasic.ChrW(9) & "1{N" & _
            "ame:""SettingValue"";Caption:""Value"";Style:""Format:""""N0"""";DataType:System.UInt16;T" & _
            "extAlign:GeneralCenter;"";}" & Global.Microsoft.VisualBasic.ChrW(9)
        Me.c1Setting.Dock = System.Windows.Forms.DockStyle.Fill
        Me.c1Setting.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.c1Setting.Location = New System.Drawing.Point(1, 1)
        Me.c1Setting.Name = "c1Setting"
        Me.c1Setting.Rows.Count = 4
        Me.c1Setting.Rows.DefaultSize = 21
        Me.c1Setting.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.c1Setting.Size = New System.Drawing.Size(462, 86)
        Me.c1Setting.StyleInfo = resources.GetString("c1Setting.StyleInfo")
        Me.c1Setting.TabIndex = 25
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label2.Location = New System.Drawing.Point(463, 1)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(1, 86)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "label3"
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(1, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(463, 1)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "label1"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(0, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(1, 87)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "label4"
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label9.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label9.Location = New System.Drawing.Point(0, 87)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(464, 1)
        Me.Label9.TabIndex = 8
        Me.Label9.Text = "label2"
        '
        'frmWebServiceSetting
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(464, 141)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.tstrip)
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmWebServiceSetting"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Web Services Setting "
        Me.tstrip.ResumeLayout(False)
        Me.tstrip.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        CType(Me.c1Setting, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents tstrip As System.Windows.Forms.ToolStrip
    Friend WithEvents TsSaveBtn As System.Windows.Forms.ToolStripButton
    Friend WithEvents TsCancelBtn As System.Windows.Forms.ToolStripButton
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Private WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents Label3 As System.Windows.Forms.Label
    Private WithEvents Label4 As System.Windows.Forms.Label
    Private WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents c1Setting As C1.Win.C1FlexGrid.C1FlexGrid
End Class
