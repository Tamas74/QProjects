Imports gloUserControlLibrary
Imports gloEMR.gloStream.DiseaseManagement.Supporting
Imports gloemr.gloStream.DiseaseManagement
Public Class frmConsentTracking_History

    Inherits System.Windows.Forms.Form
    Friend WithEvents pnlToolStrip As System.Windows.Forms.Panel
    Friend WithEvents ts_ViewButtons As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents ts_btnClose As System.Windows.Forms.ToolStripButton
    Private WithEvents pnlMain As System.Windows.Forms.Panel
    Private WithEvents C1SuperTooltip1 As C1.Win.C1SuperTooltip.C1SuperTooltip
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents pnlImHistory As System.Windows.Forms.Panel
    Friend WithEvents pnlImmunization As System.Windows.Forms.Panel
    Private WithEvents Panel2 As System.Windows.Forms.Panel
    Private WithEvents Label1 As System.Windows.Forms.Label
    Private WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents gvData As C1.Win.C1FlexGrid.C1FlexGrid
    Private WithEvents panel1 As System.Windows.Forms.Panel
    Private WithEvents label49 As System.Windows.Forms.Label
    Private WithEvents Label5 As System.Windows.Forms.Label
    Private WithEvents Label3 As System.Windows.Forms.Label
    Private WithEvents Label4 As System.Windows.Forms.Label
    Private WithEvents Label6 As System.Windows.Forms.Label
    Private WithEvents Label7 As System.Windows.Forms.Label
    Private WithEvents Label10 As System.Windows.Forms.Label
    Private WithEvents Label8 As System.Windows.Forms.Label
    Private WithEvents Label9 As System.Windows.Forms.Label
    Private WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Splitter1 As System.Windows.Forms.Splitter
    Dim _nPatientId As Int64

#Region " Windows Form Designer generated code "

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            Try
                gloGlobal.cEventHelper.RemoveAllEventHandlers(Me)
            Catch
            End Try

            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents gvHistory As C1.Win.C1FlexGrid.C1FlexGrid
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmConsentTracking_History))
        Me.gvHistory = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.pnlToolStrip = New System.Windows.Forms.Panel()
        Me.ts_ViewButtons = New gloGlobal.gloToolStripIgnoreFocus()
        Me.ts_btnClose = New System.Windows.Forms.ToolStripButton()
        Me.pnlMain = New System.Windows.Forms.Panel()
        Me.pnlImHistory = New System.Windows.Forms.Panel()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Splitter1 = New System.Windows.Forms.Splitter()
        Me.pnlImmunization = New System.Windows.Forms.Panel()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.gvData = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.panel1 = New System.Windows.Forms.Panel()
        Me.label49 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.C1SuperTooltip1 = New C1.Win.C1SuperTooltip.C1SuperTooltip(Me.components)
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        CType(Me.gvHistory, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlToolStrip.SuspendLayout()
        Me.ts_ViewButtons.SuspendLayout()
        Me.pnlMain.SuspendLayout()
        Me.pnlImHistory.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.pnlImmunization.SuspendLayout()
        CType(Me.gvData, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'gvHistory
        '
        Me.gvHistory.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.gvHistory.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.gvHistory.ColumnInfo = "0,0,0,0,0,95,Columns:"
        Me.gvHistory.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvHistory.ExtendLastCol = True
        Me.gvHistory.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gvHistory.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.gvHistory.Location = New System.Drawing.Point(4, 27)
        Me.gvHistory.Name = "gvHistory"
        Me.gvHistory.Rows.DefaultSize = 19
        Me.gvHistory.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.gvHistory.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always
        Me.gvHistory.ShowCellLabels = True
        Me.gvHistory.Size = New System.Drawing.Size(927, 317)
        Me.gvHistory.StyleInfo = resources.GetString("gvHistory.StyleInfo")
        Me.gvHistory.TabIndex = 0
        '
        'pnlToolStrip
        '
        Me.pnlToolStrip.AutoSize = True
        Me.pnlToolStrip.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlToolStrip.Controls.Add(Me.ts_ViewButtons)
        Me.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlToolStrip.Location = New System.Drawing.Point(0, 0)
        Me.pnlToolStrip.Name = "pnlToolStrip"
        Me.pnlToolStrip.Size = New System.Drawing.Size(935, 53)
        Me.pnlToolStrip.TabIndex = 0
        '
        'ts_ViewButtons
        '
        Me.ts_ViewButtons.BackColor = System.Drawing.Color.Transparent
        Me.ts_ViewButtons.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.ts_ViewButtons.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ts_ViewButtons.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_ViewButtons.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ts_ViewButtons.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.ts_ViewButtons.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ts_btnClose})
        Me.ts_ViewButtons.Location = New System.Drawing.Point(0, 0)
        Me.ts_ViewButtons.Name = "ts_ViewButtons"
        Me.ts_ViewButtons.Size = New System.Drawing.Size(935, 53)
        Me.ts_ViewButtons.TabIndex = 0
        Me.ts_ViewButtons.Text = "ToolStrip1"
        '
        'ts_btnClose
        '
        Me.ts_btnClose.Image = CType(resources.GetObject("ts_btnClose.Image"), System.Drawing.Image)
        Me.ts_btnClose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnClose.Name = "ts_btnClose"
        Me.ts_btnClose.Size = New System.Drawing.Size(43, 50)
        Me.ts_btnClose.Tag = "Close"
        Me.ts_btnClose.Text = "&Close"
        Me.ts_btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'pnlMain
        '
        Me.pnlMain.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlMain.Controls.Add(Me.pnlImHistory)
        Me.pnlMain.Controls.Add(Me.Splitter1)
        Me.pnlMain.Controls.Add(Me.pnlImmunization)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlMain.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.pnlMain.Location = New System.Drawing.Point(0, 53)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(935, 564)
        Me.pnlMain.TabIndex = 1
        '
        'pnlImHistory
        '
        Me.pnlImHistory.Controls.Add(Me.Label3)
        Me.pnlImHistory.Controls.Add(Me.gvHistory)
        Me.pnlImHistory.Controls.Add(Me.Panel2)
        Me.pnlImHistory.Controls.Add(Me.Label4)
        Me.pnlImHistory.Controls.Add(Me.Label6)
        Me.pnlImHistory.Controls.Add(Me.Label7)
        Me.pnlImHistory.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlImHistory.Location = New System.Drawing.Point(0, 217)
        Me.pnlImHistory.Name = "pnlImHistory"
        Me.pnlImHistory.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.pnlImHistory.Size = New System.Drawing.Size(935, 347)
        Me.pnlImHistory.TabIndex = 1
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Location = New System.Drawing.Point(4, 343)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(927, 1)
        Me.Label3.TabIndex = 144
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Transparent
        Me.Panel2.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Blue2007
        Me.Panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel2.Controls.Add(Me.Label1)
        Me.Panel2.Controls.Add(Me.Label2)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(4, 1)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(927, 26)
        Me.Panel2.TabIndex = 143
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(0, 25)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(927, 1)
        Me.Label1.TabIndex = 141
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.White
        Me.Label2.Location = New System.Drawing.Point(0, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(927, 26)
        Me.Label2.TabIndex = 142
        Me.Label2.Text = "  Consent Tracking History"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Location = New System.Drawing.Point(4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(927, 1)
        Me.Label4.TabIndex = 145
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label6.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label6.Location = New System.Drawing.Point(3, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(1, 344)
        Me.Label6.TabIndex = 146
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label7.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Location = New System.Drawing.Point(931, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(1, 344)
        Me.Label7.TabIndex = 147
        '
        'Splitter1
        '
        Me.Splitter1.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Splitter1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Splitter1.Location = New System.Drawing.Point(0, 213)
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(935, 4)
        Me.Splitter1.TabIndex = 3
        Me.Splitter1.TabStop = False
        '
        'pnlImmunization
        '
        Me.pnlImmunization.Controls.Add(Me.Label10)
        Me.pnlImmunization.Controls.Add(Me.gvData)
        Me.pnlImmunization.Controls.Add(Me.panel1)
        Me.pnlImmunization.Controls.Add(Me.Label8)
        Me.pnlImmunization.Controls.Add(Me.Label9)
        Me.pnlImmunization.Controls.Add(Me.Label11)
        Me.pnlImmunization.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlImmunization.Location = New System.Drawing.Point(0, 0)
        Me.pnlImmunization.Name = "pnlImmunization"
        Me.pnlImmunization.Padding = New System.Windows.Forms.Padding(3, 3, 3, 0)
        Me.pnlImmunization.Size = New System.Drawing.Size(935, 213)
        Me.pnlImmunization.TabIndex = 2
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label10.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label10.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label10.Location = New System.Drawing.Point(4, 212)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(927, 1)
        Me.Label10.TabIndex = 145
        '
        'gvData
        '
        Me.gvData.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.gvData.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.gvData.ColumnInfo = "0,0,0,0,0,95,Columns:"
        Me.gvData.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvData.ExtendLastCol = True
        Me.gvData.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gvData.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.gvData.Location = New System.Drawing.Point(4, 30)
        Me.gvData.Name = "gvData"
        Me.gvData.Rows.DefaultSize = 19
        Me.gvData.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.gvData.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always
        Me.gvData.ShowCellLabels = True
        Me.gvData.Size = New System.Drawing.Size(927, 183)
        Me.gvData.StyleInfo = resources.GetString("gvData.StyleInfo")
        Me.gvData.TabIndex = 144
        '
        'panel1
        '
        Me.panel1.BackColor = System.Drawing.Color.Transparent
        Me.panel1.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Blue2007
        Me.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.panel1.Controls.Add(Me.label49)
        Me.panel1.Controls.Add(Me.Label5)
        Me.panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.panel1.Location = New System.Drawing.Point(4, 4)
        Me.panel1.Name = "panel1"
        Me.panel1.Size = New System.Drawing.Size(927, 26)
        Me.panel1.TabIndex = 143
        '
        'label49
        '
        Me.label49.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label49.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.label49.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label49.Location = New System.Drawing.Point(0, 25)
        Me.label49.Name = "label49"
        Me.label49.Size = New System.Drawing.Size(927, 1)
        Me.label49.TabIndex = 141
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.White
        Me.Label5.Location = New System.Drawing.Point(0, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(927, 26)
        Me.Label5.TabIndex = 142
        Me.Label5.Text = "   Consent Tracking"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label8.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Location = New System.Drawing.Point(931, 4)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(1, 209)
        Me.Label8.TabIndex = 148
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label9.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label9.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label9.Location = New System.Drawing.Point(3, 4)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(1, 209)
        Me.Label9.TabIndex = 149
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label11.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label11.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label11.Location = New System.Drawing.Point(3, 3)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(929, 1)
        Me.Label11.TabIndex = 150
        '
        'C1SuperTooltip1
        '
        Me.C1SuperTooltip1.BackgroundGradient = C1.Win.C1SuperTooltip.BackgroundGradient.None
        Me.C1SuperTooltip1.Font = New System.Drawing.Font("Tahoma", 8.0!)
        '
        'frmConsentTracking_History
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(935, 617)
        Me.Controls.Add(Me.pnlMain)
        Me.Controls.Add(Me.pnlToolStrip)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Name = "frmConsentTracking_History"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Consent Tracking History"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.gvHistory, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlToolStrip.ResumeLayout(False)
        Me.pnlToolStrip.PerformLayout()
        Me.ts_ViewButtons.ResumeLayout(False)
        Me.ts_ViewButtons.PerformLayout()
        Me.pnlMain.ResumeLayout(False)
        Me.pnlImHistory.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.pnlImmunization.ResumeLayout(False)
        CType(Me.gvData, System.ComponentModel.ISupportInitialize).EndInit()
        Me.panel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

#Region "Constructor"

    Public Sub New(ByVal _PatientId As Int64)
        MyBase.New()
        'This call is required by the Windows Form Designer.
        InitializeComponent()
        _nPatientId = _PatientId
    End Sub

#End Region

#Region "Form Events"

    Private Sub frmConsentTracking_History_FormClosed(sender As Object, e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        RemoveHandler gvData.AfterRowColChange, AddressOf c1Immunization_AfterRowColChange
    End Sub

    Private Sub frmDM_DiseaseView_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load


        gloC1FlexStyle.Style(gvHistory)
        gloC1FlexStyle.Style(gvData)
        Try
            FillGridData()
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        AddHandler gvData.AfterRowColChange, AddressOf c1Immunization_AfterRowColChange
    End Sub

    Private Sub frmConsentTracking_History_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub

#End Region

#Region "Button Events"

    Private Sub ts_btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ts_btnClose.Click
        Me.Close()
    End Sub

#End Region

#Region "Grid Events"

    Private Sub c1RecommendationHistory_MouseMove(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles gvHistory.MouseMove
        Try
            gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, CType(sender, C1.Win.C1FlexGrid.C1FlexGrid), e.Location)
            '  End If
        Catch ex As Exception
            'Blank
        End Try
    End Sub

    Private Sub c1Immunization_AfterRowColChange(sender As System.Object, e As C1.Win.C1FlexGrid.RangeEventArgs) 'Handles gvData.AfterRowColChange
        Dim clsIMTran As clsgloIMTransaction = Nothing
        Try
            If gvData.RowSel > 0 Then
                If Convert.ToInt64(gvData.GetData(gvData.Row, "nPatientConsentTrackingID")) > 0 Then
                    clsIMTran = New clsgloIMTransaction
                    Dim transactionID As Int64 = Convert.ToInt64(gvData.GetData(gvData.Row, "nPatientConsentTrackingID"))
                    gvHistory.DataSource = GetConsentTrackingHistory(transactionID)
                    formatGrid()
                End If
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If clsIMTran IsNot Nothing Then
                clsIMTran = Nothing
            End If
        End Try
    End Sub

    Private Sub c1Immunization_MouseMove(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles gvData.MouseMove
        Try
            gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, CType(sender, C1.Win.C1FlexGrid.C1FlexGrid), e.Location)
        Catch ex As Exception
        End Try
    End Sub

#End Region

#Region "function & Methods"

    Public Sub formatGrid()
        Try
            If gvData.DataSource IsNot Nothing Then
                gvData.AllowEditing = False
                For i As Integer = 0 To gvData.Cols.Count - 1
                    If gvData.Cols(i).Caption = "nPatientConsentTrackingID" OrElse gvData.Cols(i).Caption = "nPatientID" OrElse gvData.Cols(i).Caption = "nConsentType" OrElse gvData.Cols(i).Caption = "nConsentStatus" OrElse gvData.Cols(i).Caption = "nObtainedBy" Then
                        gvData.Cols(i).Visible = False
                    End If
                    gvData.Cols(i).AllowSorting = True
                    gvData.Cols(i).Width = 130
                Next
            End If

            If gvHistory.DataSource IsNot Nothing Then
                gvHistory.AllowEditing = False
                For i As Integer = 0 To gvHistory.Cols.Count - 1
                    If gvHistory.Cols(i).Caption = "nPatientConsentTrackingID" OrElse gvHistory.Cols(i).Caption = "nPatientID" OrElse gvHistory.Cols(i).Caption = "nConsentType" OrElse gvHistory.Cols(i).Caption = "nConsentStatus" OrElse gvHistory.Cols(i).Caption = "nObtainedBy" Then
                        gvHistory.Cols(i).Visible = False
                    ElseIf gvHistory.Cols(i).Caption = "Activity Date" Then
                        gvHistory.Cols(i).Format = "MM/dd/yyyy hh:mm tt"
                    End If
                    gvHistory.Cols(i).AllowSorting = True
                    gvHistory.Cols(i).Width = 130
                Next
            End If

        Catch ex As Exception
            MessageBox.Show("Error on Consent Tracking." & ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub FillGridData()
        Try
            Dim _TransactionID As Int64 = 0
            Dim dtConsentdata As New DataTable
            dtConsentdata = GetConsentTrackingData()
            gvData.DataSource = dtConsentdata
            If dtConsentdata IsNot Nothing Then
                If dtConsentdata.Rows.Count > 0 Then
                    _TransactionID = dtConsentdata(0)("nPatientConsentTrackingID")
                End If
            End If

            If _TransactionID > 0 Then
                gvHistory.DataSource = GetConsentTrackingHistory(_TransactionID)
            End If
            formatGrid()
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Function GetConsentTrackingData() As DataTable
        Dim dtdata As DataTable = Nothing
        Try
            Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString())
            Dim oParam As gloDatabaseLayer.DBParameters

            oDB.Connect(False)
            oParam = New gloDatabaseLayer.DBParameters
            oParam.Add("@nPatientConsentTrackingID", 0, ParameterDirection.Input, SqlDbType.BigInt)
            oParam.Add("@PatientID", _nPatientId, ParameterDirection.Input, SqlDbType.BigInt)
            oDB.Retrive("GetConsentTrackingHistory", oParam, dtdata)
            oDB.Disconnect()
            oParam.Dispose()
            oParam = Nothing
            oDB.Disconnect()
            oDB.Dispose()
            oDB = Nothing

            If dtdata IsNot Nothing Then
                Return dtdata
            Else
                Return Nothing
            End If

        Catch ex As Exception
            MessageBox.Show("Error on Consent Tracking." & ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End Try
    End Function

    Public Function GetConsentTrackingHistory(ByVal _TransactionID As Int64) As DataTable
        Dim dtdata As DataTable = Nothing
        Try
            Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString())
            Dim oParam As gloDatabaseLayer.DBParameters

            oDB.Connect(False)
            oParam = New gloDatabaseLayer.DBParameters
            oParam.Add("@nPatientConsentTrackingID", _TransactionID, ParameterDirection.Input, SqlDbType.BigInt)
            oParam.Add("@PatientID", _nPatientId, ParameterDirection.Input, SqlDbType.BigInt)
            oDB.Retrive("GetConsentTrackingHistory", oParam, dtdata)
            oDB.Disconnect()
            oParam.Dispose()
            oParam = Nothing
            oDB.Disconnect()
            oDB.Dispose()
            oDB = Nothing

            If dtdata IsNot Nothing Then
                Return dtdata
            Else
                Return Nothing
            End If

        Catch ex As Exception
            MessageBox.Show("Error on Consent Tracking." & ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End Try
    End Function

#End Region

End Class
