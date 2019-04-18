<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmVW_CCDDiagnosisOneFiles
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmVW_CCDDiagnosisOneFiles))
        Me.tsDiagnosisOne = New gloGlobal.gloToolStripIgnoreFocus()
        Me.tlbbtn_Refresh = New System.Windows.Forms.ToolStripButton()
        Me.tlbbtn_Close = New System.Windows.Forms.ToolStripButton()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.C1DiagnosisOneCCDResponse = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.C1SuperTooltip1 = New C1.Win.C1SuperTooltip.C1SuperTooltip(Me.components)
        Me.pnlMain = New System.Windows.Forms.Panel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.pnlPatientSearch = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.lbl_pnlSearchLeftBrd = New System.Windows.Forms.Label()
        Me.lbl_pnlSearchRightBrd = New System.Windows.Forms.Label()
        Me.lbl_pnlSearchTopBrd = New System.Windows.Forms.Label()
        Me.lbl_pnlSearchBottomBrd = New System.Windows.Forms.Label()
        Me.tsDiagnosisOne.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.C1DiagnosisOneCCDResponse, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlMain.SuspendLayout()
        Me.pnlPatientSearch.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.SuspendLayout()
        '
        'tsDiagnosisOne
        '
        Me.tsDiagnosisOne.BackgroundImage = CType(resources.GetObject("tsDiagnosisOne.BackgroundImage"), System.Drawing.Image)
        Me.tsDiagnosisOne.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tsDiagnosisOne.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.tsDiagnosisOne.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tsDiagnosisOne.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tlbbtn_Refresh, Me.tlbbtn_Close})
        Me.tsDiagnosisOne.Location = New System.Drawing.Point(0, 0)
        Me.tsDiagnosisOne.Name = "tsDiagnosisOne"
        Me.tsDiagnosisOne.Size = New System.Drawing.Size(943, 53)
        Me.tsDiagnosisOne.TabIndex = 0
        Me.tsDiagnosisOne.Text = "ToolStrip1"
        '
        'tlbbtn_Refresh
        '
        Me.tlbbtn_Refresh.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlbbtn_Refresh.Image = CType(resources.GetObject("tlbbtn_Refresh.Image"), System.Drawing.Image)
        Me.tlbbtn_Refresh.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlbbtn_Refresh.Name = "tlbbtn_Refresh"
        Me.tlbbtn_Refresh.Size = New System.Drawing.Size(58, 50)
        Me.tlbbtn_Refresh.Tag = "Refresh"
        Me.tlbbtn_Refresh.Text = "&Refresh"
        Me.tlbbtn_Refresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tlbbtn_Close
        '
        Me.tlbbtn_Close.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlbbtn_Close.Image = CType(resources.GetObject("tlbbtn_Close.Image"), System.Drawing.Image)
        Me.tlbbtn_Close.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlbbtn_Close.Name = "tlbbtn_Close"
        Me.tlbbtn_Close.Size = New System.Drawing.Size(43, 50)
        Me.tlbbtn_Close.Tag = "Close"
        Me.tlbbtn_Close.Text = "&Close"
        Me.tlbbtn_Close.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.tsDiagnosisOne)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(943, 56)
        Me.Panel1.TabIndex = 2
        '
        'C1DiagnosisOneCCDResponse
        '
        Me.C1DiagnosisOneCCDResponse.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.C1DiagnosisOneCCDResponse.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.C1DiagnosisOneCCDResponse.ColumnInfo = "0,0,0,0,0,105,Columns:"
        Me.C1DiagnosisOneCCDResponse.Dock = System.Windows.Forms.DockStyle.Fill
        Me.C1DiagnosisOneCCDResponse.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.C1DiagnosisOneCCDResponse.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcrossOut
        Me.C1DiagnosisOneCCDResponse.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcrossOut
        Me.C1DiagnosisOneCCDResponse.Location = New System.Drawing.Point(3, 0)
        Me.C1DiagnosisOneCCDResponse.Name = "C1DiagnosisOneCCDResponse"
        Me.C1DiagnosisOneCCDResponse.Rows.DefaultSize = 21
        Me.C1DiagnosisOneCCDResponse.ShowCellLabels = True
        Me.C1DiagnosisOneCCDResponse.Size = New System.Drawing.Size(937, 596)
        Me.C1DiagnosisOneCCDResponse.StyleInfo = resources.GetString("C1DiagnosisOneCCDResponse.StyleInfo")
        Me.C1DiagnosisOneCCDResponse.TabIndex = 23
        '
        'C1SuperTooltip1
        '
        Me.C1SuperTooltip1.BackgroundGradient = C1.Win.C1SuperTooltip.BackgroundGradient.None
        Me.C1SuperTooltip1.Font = New System.Drawing.Font("Tahoma", 8.0!)
        '
        'pnlMain
        '
        Me.pnlMain.Controls.Add(Me.Label2)
        Me.pnlMain.Controls.Add(Me.Label5)
        Me.pnlMain.Controls.Add(Me.Label4)
        Me.pnlMain.Controls.Add(Me.Label1)
        Me.pnlMain.Controls.Add(Me.C1DiagnosisOneCCDResponse)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Location = New System.Drawing.Point(0, 82)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.pnlMain.Size = New System.Drawing.Size(943, 599)
        Me.pnlMain.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label2.Location = New System.Drawing.Point(4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(935, 1)
        Me.Label2.TabIndex = 45
        Me.Label2.Text = "label1"
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label5.Location = New System.Drawing.Point(939, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(1, 595)
        Me.Label5.TabIndex = 44
        Me.Label5.Text = "label4"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label4.Location = New System.Drawing.Point(3, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(1, 595)
        Me.Label4.TabIndex = 43
        Me.Label4.Text = "label4"
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label1.Location = New System.Drawing.Point(3, 595)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(937, 1)
        Me.Label1.TabIndex = 42
        Me.Label1.Text = "label1"
        '
        'pnlPatientSearch
        '
        Me.pnlPatientSearch.Controls.Add(Me.Panel3)
        Me.pnlPatientSearch.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlPatientSearch.Location = New System.Drawing.Point(0, 56)
        Me.pnlPatientSearch.Name = "pnlPatientSearch"
        Me.pnlPatientSearch.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.pnlPatientSearch.Size = New System.Drawing.Size(943, 26)
        Me.pnlPatientSearch.TabIndex = 20
        '
        'Panel3
        '
        Me.Panel3.BackgroundImage = CType(resources.GetObject("Panel3.BackgroundImage"), System.Drawing.Image)
        Me.Panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel3.Controls.Add(Me.lbl_pnlSearchLeftBrd)
        Me.Panel3.Controls.Add(Me.lbl_pnlSearchRightBrd)
        Me.Panel3.Controls.Add(Me.lbl_pnlSearchTopBrd)
        Me.Panel3.Controls.Add(Me.lbl_pnlSearchBottomBrd)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel3.Location = New System.Drawing.Point(3, 0)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(937, 23)
        Me.Panel3.TabIndex = 0
        '
        'lbl_pnlSearchLeftBrd
        '
        Me.lbl_pnlSearchLeftBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlSearchLeftBrd.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_pnlSearchLeftBrd.Location = New System.Drawing.Point(0, 1)
        Me.lbl_pnlSearchLeftBrd.Name = "lbl_pnlSearchLeftBrd"
        Me.lbl_pnlSearchLeftBrd.Size = New System.Drawing.Size(1, 21)
        Me.lbl_pnlSearchLeftBrd.TabIndex = 39
        Me.lbl_pnlSearchLeftBrd.Text = "label4"
        '
        'lbl_pnlSearchRightBrd
        '
        Me.lbl_pnlSearchRightBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlSearchRightBrd.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_pnlSearchRightBrd.Location = New System.Drawing.Point(936, 1)
        Me.lbl_pnlSearchRightBrd.Name = "lbl_pnlSearchRightBrd"
        Me.lbl_pnlSearchRightBrd.Size = New System.Drawing.Size(1, 21)
        Me.lbl_pnlSearchRightBrd.TabIndex = 40
        Me.lbl_pnlSearchRightBrd.Text = "label4"
        '
        'lbl_pnlSearchTopBrd
        '
        Me.lbl_pnlSearchTopBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlSearchTopBrd.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_pnlSearchTopBrd.Location = New System.Drawing.Point(0, 0)
        Me.lbl_pnlSearchTopBrd.Name = "lbl_pnlSearchTopBrd"
        Me.lbl_pnlSearchTopBrd.Size = New System.Drawing.Size(937, 1)
        Me.lbl_pnlSearchTopBrd.TabIndex = 36
        Me.lbl_pnlSearchTopBrd.Text = "label1"
        '
        'lbl_pnlSearchBottomBrd
        '
        Me.lbl_pnlSearchBottomBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlSearchBottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_pnlSearchBottomBrd.Location = New System.Drawing.Point(0, 22)
        Me.lbl_pnlSearchBottomBrd.Name = "lbl_pnlSearchBottomBrd"
        Me.lbl_pnlSearchBottomBrd.Size = New System.Drawing.Size(937, 1)
        Me.lbl_pnlSearchBottomBrd.TabIndex = 35
        Me.lbl_pnlSearchBottomBrd.Text = "label1"
        '
        'frmVW_CCDDiagnosisOneFiles
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(943, 681)
        Me.Controls.Add(Me.pnlMain)
        Me.Controls.Add(Me.pnlPatientSearch)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmVW_CCDDiagnosisOneFiles"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "View CDS Files"
        Me.tsDiagnosisOne.ResumeLayout(False)
        Me.tsDiagnosisOne.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.C1DiagnosisOneCCDResponse, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlMain.ResumeLayout(False)
        Me.pnlPatientSearch.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents tsDiagnosisOne As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents tlbbtn_Refresh As System.Windows.Forms.ToolStripButton
    Friend WithEvents tlbbtn_Close As System.Windows.Forms.ToolStripButton
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents C1SuperTooltip1 As C1.Win.C1SuperTooltip.C1SuperTooltip
    Friend WithEvents pnlMain As System.Windows.Forms.Panel
    Friend WithEvents C1DiagnosisOneCCDResponse As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents pnlPatientSearch As System.Windows.Forms.Panel
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Private WithEvents lbl_pnlSearchLeftBrd As System.Windows.Forms.Label
    Private WithEvents lbl_pnlSearchRightBrd As System.Windows.Forms.Label
    Private WithEvents lbl_pnlSearchTopBrd As System.Windows.Forms.Label
    Private WithEvents lbl_pnlSearchBottomBrd As System.Windows.Forms.Label
    Private WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents Label5 As System.Windows.Forms.Label
    Private WithEvents Label4 As System.Windows.Forms.Label
    Private WithEvents Label1 As System.Windows.Forms.Label
End Class
