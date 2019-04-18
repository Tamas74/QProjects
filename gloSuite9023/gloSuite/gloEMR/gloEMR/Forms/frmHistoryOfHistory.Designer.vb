<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmHistoryOfHistory
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            Try
                gloGlobal.cEventHelper.RemoveAllEventHandlers(Me)
            Catch
            End Try
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmHistoryOfHistory))
        Me.C1HistoryOfHistory = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.tlsHistoryOFHistory = New gloGlobal.gloToolStripIgnoreFocus()
        Me.btn_tls_Close = New System.Windows.Forms.ToolStripButton()
        Me.lbl_pnlLeft = New System.Windows.Forms.Label()
        Me.pnl_Base = New System.Windows.Forms.Panel()
        Me.lbl_pnlBottom = New System.Windows.Forms.Label()
        Me.lbl_pnlRight = New System.Windows.Forms.Label()
        Me.lbl_pnlTop = New System.Windows.Forms.Label()
        Me.C1SuperTooltip1 = New C1.Win.C1SuperTooltip.C1SuperTooltip(Me.components)
        CType(Me.C1HistoryOfHistory, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        Me.tlsHistoryOFHistory.SuspendLayout()
        Me.pnl_Base.SuspendLayout()
        Me.SuspendLayout()
        '
        'C1HistoryOfHistory
        '
        Me.C1HistoryOfHistory.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        Me.C1HistoryOfHistory.AllowEditing = False
        Me.C1HistoryOfHistory.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.MultiColumn
        Me.C1HistoryOfHistory.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.C1HistoryOfHistory.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.C1HistoryOfHistory.ColumnInfo = resources.GetString("C1HistoryOfHistory.ColumnInfo")
        Me.C1HistoryOfHistory.Dock = System.Windows.Forms.DockStyle.Fill
        Me.C1HistoryOfHistory.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.C1HistoryOfHistory.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.C1HistoryOfHistory.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross
        Me.C1HistoryOfHistory.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross
        Me.C1HistoryOfHistory.Location = New System.Drawing.Point(4, 4)
        Me.C1HistoryOfHistory.Name = "C1HistoryOfHistory"
        Me.C1HistoryOfHistory.Rows.Count = 1
        Me.C1HistoryOfHistory.Rows.DefaultSize = 19
        Me.C1HistoryOfHistory.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.C1HistoryOfHistory.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always
        Me.C1HistoryOfHistory.ShowSortPosition = C1.Win.C1FlexGrid.ShowSortPositionEnum.None
        Me.C1HistoryOfHistory.Size = New System.Drawing.Size(957, 598)
        Me.C1HistoryOfHistory.StyleInfo = resources.GetString("C1HistoryOfHistory.StyleInfo")
        Me.C1HistoryOfHistory.TabIndex = 7
        Me.C1HistoryOfHistory.Tree.LineColor = System.Drawing.Color.LightGray
        Me.C1HistoryOfHistory.Tree.NodeImageCollapsed = CType(resources.GetObject("C1HistoryOfHistory.Tree.NodeImageCollapsed"), System.Drawing.Image)
        Me.C1HistoryOfHistory.Tree.NodeImageExpanded = CType(resources.GetObject("C1HistoryOfHistory.Tree.NodeImageExpanded"), System.Drawing.Image)
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel2.Controls.Add(Me.tlsHistoryOFHistory)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(965, 54)
        Me.Panel2.TabIndex = 8
        '
        'tlsHistoryOFHistory
        '
        Me.tlsHistoryOFHistory.BackColor = System.Drawing.Color.Transparent
        Me.tlsHistoryOFHistory.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tlsHistoryOFHistory.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlsHistoryOFHistory.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlsHistoryOFHistory.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tlsHistoryOFHistory.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btn_tls_Close})
        Me.tlsHistoryOFHistory.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.tlsHistoryOFHistory.Location = New System.Drawing.Point(0, 0)
        Me.tlsHistoryOFHistory.Name = "tlsHistoryOFHistory"
        Me.tlsHistoryOFHistory.Size = New System.Drawing.Size(965, 53)
        Me.tlsHistoryOFHistory.TabIndex = 1
        Me.tlsHistoryOFHistory.Text = "toolStrip1"
        '
        'btn_tls_Close
        '
        Me.btn_tls_Close.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_tls_Close.Image = CType(resources.GetObject("btn_tls_Close.Image"), System.Drawing.Image)
        Me.btn_tls_Close.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btn_tls_Close.Name = "btn_tls_Close"
        Me.btn_tls_Close.Size = New System.Drawing.Size(59, 50)
        Me.btn_tls_Close.Tag = "Close"
        Me.btn_tls_Close.Text = "  &Close  "
        Me.btn_tls_Close.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btn_tls_Close.ToolTipText = "Close"
        '
        'lbl_pnlLeft
        '
        Me.lbl_pnlLeft.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_pnlLeft.Location = New System.Drawing.Point(3, 4)
        Me.lbl_pnlLeft.Name = "lbl_pnlLeft"
        Me.lbl_pnlLeft.Size = New System.Drawing.Size(1, 599)
        Me.lbl_pnlLeft.TabIndex = 3
        Me.lbl_pnlLeft.Text = "label4"
        '
        'pnl_Base
        '
        Me.pnl_Base.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnl_Base.Controls.Add(Me.C1HistoryOfHistory)
        Me.pnl_Base.Controls.Add(Me.lbl_pnlBottom)
        Me.pnl_Base.Controls.Add(Me.lbl_pnlLeft)
        Me.pnl_Base.Controls.Add(Me.lbl_pnlRight)
        Me.pnl_Base.Controls.Add(Me.lbl_pnlTop)
        Me.pnl_Base.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnl_Base.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnl_Base.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.pnl_Base.Location = New System.Drawing.Point(0, 54)
        Me.pnl_Base.Name = "pnl_Base"
        Me.pnl_Base.Padding = New System.Windows.Forms.Padding(3)
        Me.pnl_Base.Size = New System.Drawing.Size(965, 606)
        Me.pnl_Base.TabIndex = 8
        '
        'lbl_pnlBottom
        '
        Me.lbl_pnlBottom.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_pnlBottom.Location = New System.Drawing.Point(4, 602)
        Me.lbl_pnlBottom.Name = "lbl_pnlBottom"
        Me.lbl_pnlBottom.Size = New System.Drawing.Size(957, 1)
        Me.lbl_pnlBottom.TabIndex = 4
        Me.lbl_pnlBottom.Text = "label2"
        '
        'lbl_pnlRight
        '
        Me.lbl_pnlRight.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_pnlRight.Location = New System.Drawing.Point(961, 4)
        Me.lbl_pnlRight.Name = "lbl_pnlRight"
        Me.lbl_pnlRight.Size = New System.Drawing.Size(1, 599)
        Me.lbl_pnlRight.TabIndex = 2
        Me.lbl_pnlRight.Text = "label3"
        '
        'lbl_pnlTop
        '
        Me.lbl_pnlTop.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_pnlTop.Location = New System.Drawing.Point(3, 3)
        Me.lbl_pnlTop.Name = "lbl_pnlTop"
        Me.lbl_pnlTop.Size = New System.Drawing.Size(959, 1)
        Me.lbl_pnlTop.TabIndex = 0
        Me.lbl_pnlTop.Text = "label1"
        '
        'C1SuperTooltip1
        '
        Me.C1SuperTooltip1.BackgroundGradient = C1.Win.C1SuperTooltip.BackgroundGradient.None
        Me.C1SuperTooltip1.Font = New System.Drawing.Font("Tahoma", 8.0!)
        '
        'frmHistoryOfHistory
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(965, 660)
        Me.Controls.Add(Me.pnl_Base)
        Me.Controls.Add(Me.Panel2)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmHistoryOfHistory"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "History Audit"
        CType(Me.C1HistoryOfHistory, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.tlsHistoryOFHistory.ResumeLayout(False)
        Me.tlsHistoryOFHistory.PerformLayout()
        Me.pnl_Base.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents C1HistoryOfHistory As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Private WithEvents tlsHistoryOFHistory As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents btn_tls_Close As System.Windows.Forms.ToolStripButton
    Private WithEvents pnl_Base As System.Windows.Forms.Panel
    Private WithEvents lbl_pnlBottom As System.Windows.Forms.Label
    Private WithEvents lbl_pnlLeft As System.Windows.Forms.Label
    Private WithEvents lbl_pnlRight As System.Windows.Forms.Label
    Private WithEvents lbl_pnlTop As System.Windows.Forms.Label
    Friend WithEvents C1SuperTooltip1 As C1.Win.C1SuperTooltip.C1SuperTooltip
End Class
