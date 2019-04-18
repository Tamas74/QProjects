Imports gloUserControlLibrary
Imports gloEMR.gloStream.DiseaseManagement.Supporting
Imports gloemr.gloStream.DiseaseManagement
Public Class frmIM_History
    Inherits System.Windows.Forms.Form
    Friend WithEvents pnlToolStrip As System.Windows.Forms.Panel
    Friend WithEvents ts_ViewButtons As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents ts_btnClose As System.Windows.Forms.ToolStripButton
    Private WithEvents pnlMain As System.Windows.Forms.Panel
    Private WithEvents C1SuperTooltip1 As C1.Win.C1SuperTooltip.C1SuperTooltip
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip

    Public oMainForm As MainMenu

    Dim _ntransaction_id As Int64
    Dim _CriteriaID As Int64
    Friend WithEvents pnlImHistory As System.Windows.Forms.Panel
    Friend WithEvents pnlImmunization As System.Windows.Forms.Panel
    Private WithEvents Panel2 As System.Windows.Forms.Panel
    Private WithEvents Label1 As System.Windows.Forms.Label
    Private WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents c1Immunization As C1.Win.C1FlexGrid.C1FlexGrid
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

    Public Sub New(ByVal ntransaction_id As Int64, ByVal _PatientId As Int64)
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        _ntransaction_id = ntransaction_id
        _nPatientId = _PatientId

    End Sub

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
    Friend WithEvents c1History As C1.Win.C1FlexGrid.C1FlexGrid
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmIM_History))
        Me.c1History = New C1.Win.C1FlexGrid.C1FlexGrid()
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
        Me.c1Immunization = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.panel1 = New System.Windows.Forms.Panel()
        Me.label49 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.C1SuperTooltip1 = New C1.Win.C1SuperTooltip.C1SuperTooltip(Me.components)
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        CType(Me.c1History, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlToolStrip.SuspendLayout()
        Me.ts_ViewButtons.SuspendLayout()
        Me.pnlMain.SuspendLayout()
        Me.pnlImHistory.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.pnlImmunization.SuspendLayout()
        CType(Me.c1Immunization, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'c1History
        '
        Me.c1History.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.c1History.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.c1History.ColumnInfo = "0,0,0,0,0,95,Columns:"
        Me.c1History.Dock = System.Windows.Forms.DockStyle.Fill
        Me.c1History.ExtendLastCol = True
        Me.c1History.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.c1History.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.c1History.Location = New System.Drawing.Point(4, 27)
        Me.c1History.Name = "c1History"
        Me.c1History.Rows.DefaultSize = 19
        Me.c1History.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.c1History.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always
        Me.c1History.ShowCellLabels = True
        Me.c1History.Size = New System.Drawing.Size(927, 317)
        Me.c1History.StyleInfo = resources.GetString("c1History.StyleInfo")
        Me.c1History.TabIndex = 0
        '
        'pnlToolStrip
        '
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
        Me.pnlMain.TabIndex = 2
        '
        'pnlImHistory
        '
        Me.pnlImHistory.Controls.Add(Me.Label3)
        Me.pnlImHistory.Controls.Add(Me.c1History)
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
        Me.Label2.Text = "   Immunization History"
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
        Me.pnlImmunization.Controls.Add(Me.c1Immunization)
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
        'c1Immunization
        '
        Me.c1Immunization.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.c1Immunization.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.c1Immunization.ColumnInfo = "0,0,0,0,0,95,Columns:"
        Me.c1Immunization.Dock = System.Windows.Forms.DockStyle.Fill
        Me.c1Immunization.ExtendLastCol = True
        Me.c1Immunization.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.c1Immunization.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.c1Immunization.Location = New System.Drawing.Point(4, 30)
        Me.c1Immunization.Name = "c1Immunization"
        Me.c1Immunization.Rows.DefaultSize = 19
        Me.c1Immunization.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.c1Immunization.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always
        Me.c1Immunization.ShowCellLabels = True
        Me.c1Immunization.Size = New System.Drawing.Size(927, 183)
        Me.c1Immunization.StyleInfo = resources.GetString("c1Immunization.StyleInfo")
        Me.c1Immunization.TabIndex = 144
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
        Me.Label5.Text = "   Immunization"
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
        'frmIM_History
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(935, 617)
        Me.Controls.Add(Me.pnlMain)
        Me.Controls.Add(Me.pnlToolStrip)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmIM_History"
        Me.Text = "Immunization History"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.c1History, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlToolStrip.ResumeLayout(False)
        Me.pnlToolStrip.PerformLayout()
        Me.ts_ViewButtons.ResumeLayout(False)
        Me.ts_ViewButtons.PerformLayout()
        Me.pnlMain.ResumeLayout(False)
        Me.pnlImHistory.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.pnlImmunization.ResumeLayout(False)
        CType(Me.c1Immunization, System.ComponentModel.ISupportInitialize).EndInit()
        Me.panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub frmDM_DiseaseView_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load

        RemoveHandler c1Immunization.AfterRowColChange, AddressOf c1Immunization_AfterRowColChange
        gloC1FlexStyle.Style(c1History)

        Try
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.ViewTransaction, gloAuditTrail.ActivityType.View, "Immunization History Opened", _nPatientId, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            Fill_ImmunizationHistory()
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        AddHandler c1Immunization.AfterRowColChange, AddressOf c1Immunization_AfterRowColChange
    End Sub

    Private Sub DesignGrid()

        c1History.Cols("Date").DataType = GetType(System.String)
        c1History.Cols("Date").Width = c1Immunization.Width * 10 / 100


        With c1History

            For i As Int16 = 0 To .Cols.Count - 1
                .Cols(i).AllowEditing = False
            Next

            .AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None

        End With

        c1History.Cols(0).Format = "MM/dd/yyyy hh:mm tt"
        c1History.Cols(0).Width = 148



        With c1History
            For _rowIndex As Int16 = 1 To c1History.Rows.Count - 1
                .Rows(_rowIndex).Height = 40
            Next

            If c1History.Rows.Count > 1 Then

                Dim cStyle As C1.Win.C1FlexGrid.CellStyle
                '  cStyle = .Styles.Add("WordWrap")
                Try
                    If (.Styles.Contains("WordWrap")) Then
                        cStyle = .Styles("WordWrap")
                    Else
                        cStyle = .Styles.Add("WordWrap")

                    End If
                Catch ex As Exception
                    cStyle = .Styles.Add("WordWrap")

                End Try
                cStyle.WordWrap = True
                cStyle.Trimming = StringTrimming.EllipsisCharacter



            End If

        End With

    End Sub
    Private Sub DesignImmunizationGrid()

        c1Immunization.Cols("Date").DataType = GetType(System.String)
        c1Immunization.Cols("Date").Width = c1Immunization.Width * 10 / 100

        c1Immunization.ShowCellLabels = False
        c1Immunization.AllowSorting = True
        c1Immunization.AllowEditing = False
        c1Immunization.Cols(0).Visible = False
        c1Immunization.Cols(1).Visible = False
        c1Immunization.Cols(2).Visible = False

        With c1Immunization

            For i As Int16 = 0 To .Cols.Count - 1
                .Cols(i).AllowEditing = False
            Next

            .AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None

        End With
        



        

    End Sub
    Private Sub Fill_ImmunizationHistory()
        Dim clsIMTran As New clsgloIMTransaction
        Try

            c1Immunization.BeginUpdate()
            Dim _dtImmunization As DataTable
            Dim _dtImmunizationHistory As DataTable
            clsIMTran.PatientID = _nPatientId
            _dtImmunization = clsIMTran.ShowImmunization()
            c1Immunization.DataSource = _dtImmunization
            c1Immunization.EndUpdate()



            Dim index As Integer = 0
            If _dtImmunization IsNot Nothing Then
                If _dtImmunization.Rows.Count > 0 Then
                    Dim foundRows As DataRow()
                    Dim filter As String = " TransactionID == " & _ntransaction_id
                    foundRows = _dtImmunization.[Select](String.Format("TransactionID='{0}'", _ntransaction_id))
                    If foundRows IsNot Nothing Then
                        If foundRows.Length > 0 Then
                            index = _dtImmunization.Rows.IndexOf(foundRows(0))
                            _ntransaction_id = _dtImmunization(index)(1)
                        End If
                    End If
                End If



            End If

            c1History.BeginUpdate()
            If _dtImmunization IsNot Nothing AndAlso _ntransaction_id = 0 Then
                If _dtImmunization.Rows.Count > 0 Then
                    _ntransaction_id = _dtImmunization(0)(1)
                End If

            End If
            clsIMTran.TranctionID = _ntransaction_id

            _dtImmunizationHistory = clsIMTran.ShowImmunizationHistory()
           




            c1History.DataSource = _dtImmunizationHistory

            c1History.EndUpdate()

            DesignGrid()
            DesignImmunizationGrid()
            If (c1Immunization.Rows.Count > 1) Then
                c1Immunization.RowSel = index + 1
                c1Immunization.Select(index + 1, COL_CAT_CATEGORY)
            End If
            


            ' gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.ViewRecommendationRule, gloAuditTrail.ActivityCategory.ViewRecommendationRule, gloAuditTrail.ActivityType.RecommendationViewHistory, "' Viewed Recommendation history", _nPatientId, _CriteriaID, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            'gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.ViewRecommendationRule, gloAuditTrail.ActivityCategory.ViewRecommendationRule, gloAuditTrail.ActivityType.RecommendationViewHistory, "' unsuccessfully Viewed Recommendation history", _nPatientId, _CriteriaID, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR)
        Finally
            If clsIMTran IsNot Nothing Then
                clsIMTran = Nothing
            End If
        End Try


    End Sub



    Private Sub RefreshGrid()
        Try


        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub FormClose()
        Me.Close()
    End Sub



    Private Sub ts_btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ts_btnClose.Click
        FormClose()
    End Sub

    Private Sub c1RecommendationHistory_MouseMove(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles c1History.MouseMove
        Try

            ' If c1History.HitTest(e.X, e.Y).Column = 6 Then
            gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, CType(sender, C1.Win.C1FlexGrid.C1FlexGrid), e.Location)
            '  End If

        Catch ex As Exception
            'Blank
        End Try
    End Sub

   

   

    Private Sub c1Immunization_AfterRowColChange(sender As System.Object, e As C1.Win.C1FlexGrid.RangeEventArgs) Handles c1Immunization.AfterRowColChange
        Dim clsIMTran As clsgloIMTransaction = Nothing
        Try
            If c1Immunization.RowSel > 0 Then
                If Convert.ToInt64(c1Immunization.GetData(c1Immunization.Row, 1)) > 0 Then
                    clsIMTran = New clsgloIMTransaction

                    clsIMTran.TranctionID = Convert.ToInt64(c1Immunization.GetData(c1Immunization.Row, 1))
                    clsIMTran.PatientID = _nPatientId
                    c1History.BeginUpdate()
                    c1History.DataSource = clsIMTran.ShowImmunizationHistory()
                    c1History.EndUpdate()
                    DesignGrid()
                End If
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            '' gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.ViewRecommendationRule, gloAuditTrail.ActivityCategory.ViewRecommendationRule, gloAuditTrail.ActivityType.RecommendationViewHistory, "' unsuccessfully Viewed Recommendation history", _nPatientId, _CriteriaID, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR)

        Finally
            If clsIMTran IsNot Nothing Then
                clsIMTran = Nothing
            End If
        End Try
    End Sub

    Private Sub c1Immunization_MouseMove(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles c1Immunization.MouseMove
        Try

            '  If c1Immunization.HitTest(e.X, e.Y).Column = 6 Then
            gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, CType(sender, C1.Win.C1FlexGrid.C1FlexGrid), e.Location)
            '  End If

        Catch ex As Exception
            'Blank
        End Try
    End Sub
End Class
