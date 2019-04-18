<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
 Partial Class frmViewPatientSummary
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
                Try
                    If (IsNothing(gloUC_PatientStrip1) = False) Then
                        gloUC_PatientStrip1.Dispose()
                        gloUC_PatientStrip1 = Nothing
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
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmViewPatientSummary))
        Me.PatientSummFlexGrid = New C1.Win.C1FlexGrid.C1FlexGrid
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.lbl_BottomBrd = New System.Windows.Forms.Label
        Me.lbl_LeftBrd = New System.Windows.Forms.Label
        Me.lbl_RightBrd = New System.Windows.Forms.Label
        Me.lbl_TopBrd = New System.Windows.Forms.Label
        Me.pnltls = New System.Windows.Forms.Panel
        Me.tlsSummary = New gloGlobal.gloToolStripIgnoreFocus
        Me.tlbClose = New System.Windows.Forms.ToolStripButton
        Me.C1SuperTooltip1 = New C1.Win.C1SuperTooltip.C1SuperTooltip(Me.components)
        CType(Me.PatientSummFlexGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.pnltls.SuspendLayout()
        Me.tlsSummary.SuspendLayout()
        Me.SuspendLayout()
        '
        'PatientSummFlexGrid
        '
        Me.PatientSummFlexGrid.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        Me.PatientSummFlexGrid.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None
        Me.PatientSummFlexGrid.BackColor = System.Drawing.Color.GhostWhite
        Me.PatientSummFlexGrid.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.PatientSummFlexGrid.ColumnInfo = "1,0,0,0,0,95,Columns:0{StyleFixed:""TextAlign:CenterCenter;ImageAlign:CenterCenter" & _
            ";"";}" & Global.Microsoft.VisualBasic.ChrW(9)
        Me.PatientSummFlexGrid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PatientSummFlexGrid.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PatientSummFlexGrid.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross
        Me.PatientSummFlexGrid.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross
        Me.PatientSummFlexGrid.Location = New System.Drawing.Point(3, 3)
        Me.PatientSummFlexGrid.Name = "PatientSummFlexGrid"
        Me.PatientSummFlexGrid.Rows.Count = 1
        Me.PatientSummFlexGrid.Rows.DefaultSize = 19
        Me.PatientSummFlexGrid.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.PatientSummFlexGrid.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always
        Me.PatientSummFlexGrid.ShowSort = False
        Me.PatientSummFlexGrid.Size = New System.Drawing.Size(625, 318)
        Me.PatientSummFlexGrid.StyleInfo = resources.GetString("PatientSummFlexGrid.StyleInfo")
        Me.PatientSummFlexGrid.TabIndex = 6
        Me.PatientSummFlexGrid.Tree.NodeImageCollapsed = CType(resources.GetObject("PatientSummFlexGrid.Tree.NodeImageCollapsed"), System.Drawing.Image)
        Me.PatientSummFlexGrid.Tree.NodeImageExpanded = CType(resources.GetObject("PatientSummFlexGrid.Tree.NodeImageExpanded"), System.Drawing.Image)
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.lbl_BottomBrd)
        Me.Panel1.Controls.Add(Me.lbl_LeftBrd)
        Me.Panel1.Controls.Add(Me.lbl_RightBrd)
        Me.Panel1.Controls.Add(Me.lbl_TopBrd)
        Me.Panel1.Controls.Add(Me.PatientSummFlexGrid)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 56)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Padding = New System.Windows.Forms.Padding(3)
        Me.Panel1.Size = New System.Drawing.Size(631, 324)
        Me.Panel1.TabIndex = 8
        '
        'lbl_BottomBrd
        '
        Me.lbl_BottomBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_BottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_BottomBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_BottomBrd.Location = New System.Drawing.Point(4, 320)
        Me.lbl_BottomBrd.Name = "lbl_BottomBrd"
        Me.lbl_BottomBrd.Size = New System.Drawing.Size(623, 1)
        Me.lbl_BottomBrd.TabIndex = 10
        Me.lbl_BottomBrd.Text = "label2"
        '
        'lbl_LeftBrd
        '
        Me.lbl_LeftBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_LeftBrd.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_LeftBrd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_LeftBrd.Location = New System.Drawing.Point(3, 4)
        Me.lbl_LeftBrd.Name = "lbl_LeftBrd"
        Me.lbl_LeftBrd.Size = New System.Drawing.Size(1, 317)
        Me.lbl_LeftBrd.TabIndex = 9
        Me.lbl_LeftBrd.Text = "label4"
        '
        'lbl_RightBrd
        '
        Me.lbl_RightBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_RightBrd.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_RightBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_RightBrd.Location = New System.Drawing.Point(627, 4)
        Me.lbl_RightBrd.Name = "lbl_RightBrd"
        Me.lbl_RightBrd.Size = New System.Drawing.Size(1, 317)
        Me.lbl_RightBrd.TabIndex = 8
        Me.lbl_RightBrd.Text = "label3"
        '
        'lbl_TopBrd
        '
        Me.lbl_TopBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_TopBrd.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_TopBrd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_TopBrd.Location = New System.Drawing.Point(3, 3)
        Me.lbl_TopBrd.Name = "lbl_TopBrd"
        Me.lbl_TopBrd.Size = New System.Drawing.Size(625, 1)
        Me.lbl_TopBrd.TabIndex = 7
        Me.lbl_TopBrd.Text = "label1"
        '
        'pnltls
        '
        Me.pnltls.Controls.Add(Me.tlsSummary)
        Me.pnltls.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnltls.Location = New System.Drawing.Point(0, 0)
        Me.pnltls.Name = "pnltls"
        Me.pnltls.Size = New System.Drawing.Size(631, 56)
        Me.pnltls.TabIndex = 9
        '
        'tlsSummary
        '
        Me.tlsSummary.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tlsSummary.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlsSummary.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tlsSummary.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tlbClose})
        Me.tlsSummary.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.tlsSummary.Location = New System.Drawing.Point(0, 0)
        Me.tlsSummary.Name = "tlsSummary"
        Me.tlsSummary.Size = New System.Drawing.Size(631, 53)
        Me.tlsSummary.TabIndex = 0
        Me.tlsSummary.Text = "ToolStrip1"
        '
        'tlbClose
        '
        Me.tlbClose.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlbClose.Image = CType(resources.GetObject("tlbClose.Image"), System.Drawing.Image)
        Me.tlbClose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlbClose.Name = "tlbClose"
        Me.tlbClose.Size = New System.Drawing.Size(43, 50)
        Me.tlbClose.Tag = "Close"
        Me.tlbClose.Text = "&Close"
        Me.tlbClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'C1SuperTooltip1
        '
        Me.C1SuperTooltip1.BackgroundGradient = C1.Win.C1SuperTooltip.BackgroundGradient.None
        Me.C1SuperTooltip1.Font = New System.Drawing.Font("Tahoma", 8.0!)
        '
        'frmViewPatientSummary
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(631, 380)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.pnltls)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmViewPatientSummary"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = " Patient Summary"
        CType(Me.PatientSummFlexGrid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.pnltls.ResumeLayout(False)
        Me.pnltls.PerformLayout()
        Me.tlsSummary.ResumeLayout(False)
        Me.tlsSummary.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents PatientSummFlexGrid As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents pnltls As System.Windows.Forms.Panel
    Friend WithEvents tlsSummary As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents tlbClose As System.Windows.Forms.ToolStripButton
    Private WithEvents lbl_BottomBrd As System.Windows.Forms.Label
    Private WithEvents lbl_LeftBrd As System.Windows.Forms.Label
    Private WithEvents lbl_RightBrd As System.Windows.Forms.Label
    Private WithEvents lbl_TopBrd As System.Windows.Forms.Label
    Friend WithEvents C1SuperTooltip1 As C1.Win.C1SuperTooltip.C1SuperTooltip
End Class
