<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmReconsile_History
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmReconsile_History))
        Me.Label26 = New System.Windows.Forms.Label()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.pnl_btnmodifier = New System.Windows.Forms.Panel()
        Me.c1RecHistory = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.miniToolStrip = New gloGlobal.gloToolStripIgnoreFocus()
        Me.tlsbtnClose = New System.Windows.Forms.ToolStripButton()
        Me.pnl_tlsp_Top = New System.Windows.Forms.Panel()
        Me.tstripDiagnosis = New gloGlobal.gloToolStripIgnoreFocus()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.pnl_btnmodifier.SuspendLayout()
        CType(Me.c1RecHistory, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnl_tlsp_Top.SuspendLayout()
        Me.tstripDiagnosis.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label26
        '
        Me.Label26.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label26.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label26.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.Location = New System.Drawing.Point(3, 0)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(809, 1)
        Me.Label26.TabIndex = 10
        Me.Label26.Text = "label1"
        '
        'Label25
        '
        Me.Label25.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label25.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label25.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label25.Location = New System.Drawing.Point(811, 1)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(1, 422)
        Me.Label25.TabIndex = 11
        Me.Label25.Text = "label3"
        '
        'Label20
        '
        Me.Label20.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label20.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label20.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.Location = New System.Drawing.Point(3, 1)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(1, 422)
        Me.Label20.TabIndex = 12
        Me.Label20.Text = "label4"
        '
        'Label19
        '
        Me.Label19.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label19.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label19.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label19.Location = New System.Drawing.Point(4, 422)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(807, 1)
        Me.Label19.TabIndex = 13
        Me.Label19.Text = "label2"
        '
        'pnl_btnmodifier
        '
        Me.pnl_btnmodifier.BackColor = System.Drawing.Color.Transparent
        Me.pnl_btnmodifier.Controls.Add(Me.c1RecHistory)
        Me.pnl_btnmodifier.Controls.Add(Me.Label19)
        Me.pnl_btnmodifier.Controls.Add(Me.Label20)
        Me.pnl_btnmodifier.Controls.Add(Me.Label25)
        Me.pnl_btnmodifier.Controls.Add(Me.Label26)
        Me.pnl_btnmodifier.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnl_btnmodifier.Location = New System.Drawing.Point(0, 96)
        Me.pnl_btnmodifier.Name = "pnl_btnmodifier"
        Me.pnl_btnmodifier.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.pnl_btnmodifier.Size = New System.Drawing.Size(815, 426)
        Me.pnl_btnmodifier.TabIndex = 2
        '
        'c1RecHistory
        '
        Me.c1RecHistory.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        Me.c1RecHistory.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None
        Me.c1RecHistory.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.c1RecHistory.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.c1RecHistory.ColumnInfo = resources.GetString("c1RecHistory.ColumnInfo")
        Me.c1RecHistory.Dock = System.Windows.Forms.DockStyle.Fill
        Me.c1RecHistory.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.c1RecHistory.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.c1RecHistory.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross
        Me.c1RecHistory.Location = New System.Drawing.Point(4, 1)
        Me.c1RecHistory.Name = "c1RecHistory"
        Me.c1RecHistory.Rows.Count = 1
        Me.c1RecHistory.Rows.DefaultSize = 19
        Me.c1RecHistory.Rows.Fixed = 0
        Me.c1RecHistory.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.c1RecHistory.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always
        Me.c1RecHistory.ShowCellLabels = True
        Me.c1RecHistory.ShowSortPosition = C1.Win.C1FlexGrid.ShowSortPositionEnum.None
        Me.c1RecHistory.Size = New System.Drawing.Size(807, 421)
        Me.c1RecHistory.StyleInfo = resources.GetString("c1RecHistory.StyleInfo")
        Me.c1RecHistory.TabIndex = 21
        '
        'miniToolStrip
        '
        Me.miniToolStrip.AutoSize = False
        Me.miniToolStrip.BackColor = System.Drawing.Color.Transparent
        Me.miniToolStrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.miniToolStrip.CanOverflow = False
        Me.miniToolStrip.Dock = System.Windows.Forms.DockStyle.None
        Me.miniToolStrip.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.miniToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.miniToolStrip.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.miniToolStrip.Location = New System.Drawing.Point(109, 17)
        Me.miniToolStrip.Name = "miniToolStrip"
        Me.miniToolStrip.Size = New System.Drawing.Size(699, 53)
        Me.miniToolStrip.TabIndex = 0
        '
        'tlsbtnClose
        '
        Me.tlsbtnClose.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlsbtnClose.Image = CType(resources.GetObject("tlsbtnClose.Image"), System.Drawing.Image)
        Me.tlsbtnClose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlsbtnClose.Name = "tlsbtnClose"
        Me.tlsbtnClose.Size = New System.Drawing.Size(43, 50)
        Me.tlsbtnClose.Text = "&Close"
        Me.tlsbtnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlsbtnClose.ToolTipText = "Close"
        '
        'pnl_tlsp_Top
        '
        Me.pnl_tlsp_Top.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnl_tlsp_Top.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.pnl_tlsp_Top.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnl_tlsp_Top.Controls.Add(Me.tstripDiagnosis)
        Me.pnl_tlsp_Top.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnl_tlsp_Top.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnl_tlsp_Top.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.pnl_tlsp_Top.Location = New System.Drawing.Point(0, 0)
        Me.pnl_tlsp_Top.Name = "pnl_tlsp_Top"
        Me.pnl_tlsp_Top.Size = New System.Drawing.Size(815, 57)
        Me.pnl_tlsp_Top.TabIndex = 3
        '
        'tstripDiagnosis
        '
        Me.tstripDiagnosis.BackColor = System.Drawing.Color.Transparent
        Me.tstripDiagnosis.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tstripDiagnosis.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tstripDiagnosis.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.tstripDiagnosis.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tstripDiagnosis.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tlsbtnClose})
        Me.tstripDiagnosis.Location = New System.Drawing.Point(0, 0)
        Me.tstripDiagnosis.Name = "tstripDiagnosis"
        Me.tstripDiagnosis.Size = New System.Drawing.Size(815, 53)
        Me.tstripDiagnosis.TabIndex = 0
        Me.tstripDiagnosis.Text = "ToolStrip1"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Transparent
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.ComboBox1)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 57)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Padding = New System.Windows.Forms.Padding(3)
        Me.Panel1.Size = New System.Drawing.Size(815, 39)
        Me.Panel1.TabIndex = 4
        Me.Panel1.Visible = False
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(12, 12)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(126, 14)
        Me.Label5.TabIndex = 24
        Me.Label5.Text = "Select Reconcillation :"
        '
        'ComboBox1
        '
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Location = New System.Drawing.Point(144, 8)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(346, 22)
        Me.ComboBox1.TabIndex = 23
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label1.Location = New System.Drawing.Point(4, 35)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(807, 1)
        Me.Label1.TabIndex = 13
        Me.Label1.Text = "label2"
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(3, 4)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(1, 32)
        Me.Label2.TabIndex = 12
        Me.Label2.Text = "label4"
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label3.Location = New System.Drawing.Point(811, 4)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(1, 32)
        Me.Label3.TabIndex = 11
        Me.Label3.Text = "label3"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(3, 3)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(809, 1)
        Me.Label4.TabIndex = 10
        Me.Label4.Text = "label1"
        '
        'FrmReconsile_History
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(204, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(815, 522)
        Me.Controls.Add(Me.pnl_btnmodifier)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.pnl_tlsp_Top)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "FrmReconsile_History"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Reconcile History"
        Me.pnl_btnmodifier.ResumeLayout(False)
        CType(Me.c1RecHistory, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnl_tlsp_Top.ResumeLayout(False)
        Me.pnl_tlsp_Top.PerformLayout()
        Me.tstripDiagnosis.ResumeLayout(False)
        Me.tstripDiagnosis.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents Label26 As System.Windows.Forms.Label
    Private WithEvents Label25 As System.Windows.Forms.Label
    Private WithEvents Label20 As System.Windows.Forms.Label
    Private WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents pnl_btnmodifier As System.Windows.Forms.Panel
    Friend WithEvents miniToolStrip As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents tlsbtnClose As System.Windows.Forms.ToolStripButton
    Private WithEvents pnl_tlsp_Top As System.Windows.Forms.Panel
    Friend WithEvents tstripDiagnosis As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents c1RecHistory As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents ComboBox1 As System.Windows.Forms.ComboBox
    Private WithEvents Label1 As System.Windows.Forms.Label
    Private WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents Label3 As System.Windows.Forms.Label
    Private WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
End Class
