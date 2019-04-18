Imports AxMSChart20Lib.AxMSChart
Imports gloUserControlLibrary
Imports System.Data.SqlClient

Public Class frmVWPatientVitals


    Inherits System.Windows.Forms.Form
    Implements IPatientContext

    Public Shared blnModify As Boolean

    Dim _PatientID As Long
    Private _VisitID As Long
    Private _VisitDate As Date

    Dim dv As DataView
    Private objclsPatientVitals As New clsPatientVitals
    Public myCaller As frmPatientExam

    Public blnOpenFromExam As Boolean = False
    Public IsAgeGreater As Boolean = False
    Public Shared IsOpen As Boolean = False
    Private strPatientCode As String
    Private strPatientFirstName As String
    Private strPatientMiddleName As String
    Private strPatientLastName As String
    Private strPatientDOB As String
    Private strPatientAge As String
    Private strPatientGender As String
    Private strPatientMaritalStatus As String



#Region " Windows Controls "
    Friend WithEvents pnlTop As System.Windows.Forms.Panel
    Friend WithEvents tblStrip_32 As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents tblbtn_Print_32 As System.Windows.Forms.ToolStripButton
    Friend WithEvents tblbtn_Add_32 As System.Windows.Forms.ToolStripButton
    Friend WithEvents tblbtn_Modify_32 As System.Windows.Forms.ToolStripButton
    Friend WithEvents tblbtn_Delete_32 As System.Windows.Forms.ToolStripButton
    Friend WithEvents tblbtn_Refresh_32 As System.Windows.Forms.ToolStripButton
    Friend WithEvents tblbtn_Graphs_32 As System.Windows.Forms.ToolStripButton
    Friend WithEvents tblbtn_AdvGraphs_32 As System.Windows.Forms.ToolStripButton
    Private WithEvents Label5 As System.Windows.Forms.Label
    Private WithEvents Label6 As System.Windows.Forms.Label
    Private WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Private WithEvents C1SuperTooltip1 As C1.Win.C1SuperTooltip.C1SuperTooltip
    Friend WithEvents C1PatientVitals As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents tblbtn_Close_32 As System.Windows.Forms.ToolStripButton
    Private WithEvents Label8 As System.Windows.Forms.Label
#End Region
#Region " Windows Form Designer generated code "

    Public Sub New(ByVal PatientID As Long)
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        _PatientID = PatientID
    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            Try
                gloGlobal.cEventHelper.RemoveAllEventHandlers(Me)
            Catch
            End Try
            Try
                If IsNothing(ContextMenu1) = False Then
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(ContextMenu1)
                    If (IsNothing(ContextMenu1.MenuItems) = False) Then
                        ContextMenu1.MenuItems.Clear()
                    End If
                    ContextMenu1.Dispose()
                    ContextMenu1 = Nothing
                End If
            Catch ex As Exception

            End Try
            If Not (components Is Nothing) Then
                components.Dispose()
                Try
                    If (IsNothing(gloUC_PatientStrip1) = False) Then
                        gloUC_PatientStrip1.Dispose()
                        gloUC_PatientStrip1 = Nothing
                    End If
                Catch ex As Exception

                End Try
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    '  Friend WithEvents grdVital As System.Windows.Forms.DataGrid
    Friend WithEvents pnlMain As System.Windows.Forms.Panel
    Friend WithEvents ContextMenu1 As System.Windows.Forms.ContextMenu
    Friend WithEvents cmnuMakeAsCurrent As System.Windows.Forms.MenuItem
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmVWPatientVitals))
        Me.pnlMain = New System.Windows.Forms.Panel()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.C1PatientVitals = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.pnlTop = New System.Windows.Forms.Panel()
        Me.tblStrip_32 = New gloGlobal.gloToolStripIgnoreFocus()
        Me.tblbtn_Add_32 = New System.Windows.Forms.ToolStripButton()
        Me.tblbtn_Modify_32 = New System.Windows.Forms.ToolStripButton()
        Me.tblbtn_Delete_32 = New System.Windows.Forms.ToolStripButton()
        Me.tblbtn_Refresh_32 = New System.Windows.Forms.ToolStripButton()
        Me.tblbtn_Graphs_32 = New System.Windows.Forms.ToolStripButton()
        Me.tblbtn_AdvGraphs_32 = New System.Windows.Forms.ToolStripButton()
        Me.tblbtn_Print_32 = New System.Windows.Forms.ToolStripButton()
        Me.tblbtn_Close_32 = New System.Windows.Forms.ToolStripButton()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.ContextMenu1 = New System.Windows.Forms.ContextMenu()
        Me.cmnuMakeAsCurrent = New System.Windows.Forms.MenuItem()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.C1SuperTooltip1 = New C1.Win.C1SuperTooltip.C1SuperTooltip(Me.components)
        Me.pnlMain.SuspendLayout()
        CType(Me.C1PatientVitals, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlTop.SuspendLayout()
        Me.tblStrip_32.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlMain
        '
        Me.pnlMain.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlMain.Controls.Add(Me.Label5)
        Me.pnlMain.Controls.Add(Me.Label6)
        Me.pnlMain.Controls.Add(Me.Label7)
        Me.pnlMain.Controls.Add(Me.Label8)
        Me.pnlMain.Controls.Add(Me.C1PatientVitals)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlMain.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.pnlMain.Location = New System.Drawing.Point(0, 57)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Padding = New System.Windows.Forms.Padding(3)
        Me.pnlMain.Size = New System.Drawing.Size(1028, 545)
        Me.pnlMain.TabIndex = 0
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label5.Location = New System.Drawing.Point(4, 541)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(1020, 1)
        Me.Label5.TabIndex = 12
        Me.Label5.Text = "label2"
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(3, 4)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(1, 538)
        Me.Label6.TabIndex = 11
        Me.Label6.Text = "label4"
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label7.Location = New System.Drawing.Point(1024, 4)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(1, 538)
        Me.Label7.TabIndex = 10
        Me.Label7.Text = "label3"
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(3, 3)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(1022, 1)
        Me.Label8.TabIndex = 9
        Me.Label8.Text = "label1"
        '
        'C1PatientVitals
        '
        Me.C1PatientVitals.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        Me.C1PatientVitals.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None
        Me.C1PatientVitals.AutoGenerateColumns = False
        Me.C1PatientVitals.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.C1PatientVitals.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.C1PatientVitals.ColumnInfo = resources.GetString("C1PatientVitals.ColumnInfo")
        Me.C1PatientVitals.Dock = System.Windows.Forms.DockStyle.Fill
        Me.C1PatientVitals.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.C1PatientVitals.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.C1PatientVitals.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross
        Me.C1PatientVitals.Location = New System.Drawing.Point(3, 3)
        Me.C1PatientVitals.Name = "C1PatientVitals"
        Me.C1PatientVitals.Rows.Count = 13
        Me.C1PatientVitals.Rows.DefaultSize = 19
        Me.C1PatientVitals.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.C1PatientVitals.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always
        Me.C1PatientVitals.ShowCellLabels = True
        Me.C1PatientVitals.ShowSortPosition = C1.Win.C1FlexGrid.ShowSortPositionEnum.None
        Me.C1PatientVitals.Size = New System.Drawing.Size(1022, 539)
        Me.C1PatientVitals.StyleInfo = resources.GetString("C1PatientVitals.StyleInfo")
        Me.C1PatientVitals.TabIndex = 14
        Me.C1PatientVitals.Tree.NodeImageCollapsed = CType(resources.GetObject("C1PatientVitals.Tree.NodeImageCollapsed"), System.Drawing.Image)
        Me.C1PatientVitals.Tree.NodeImageExpanded = CType(resources.GetObject("C1PatientVitals.Tree.NodeImageExpanded"), System.Drawing.Image)
        '
        'pnlTop
        '
        Me.pnlTop.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlTop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlTop.Controls.Add(Me.tblStrip_32)
        Me.pnlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlTop.Location = New System.Drawing.Point(0, 0)
        Me.pnlTop.Name = "pnlTop"
        Me.pnlTop.Size = New System.Drawing.Size(1028, 57)
        Me.pnlTop.TabIndex = 27
        '
        'tblStrip_32
        '
        Me.tblStrip_32.BackColor = System.Drawing.Color.Transparent
        Me.tblStrip_32.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tblStrip_32.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tblStrip_32.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblStrip_32.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tblStrip_32.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tblbtn_Add_32, Me.tblbtn_Modify_32, Me.tblbtn_Delete_32, Me.tblbtn_Refresh_32, Me.tblbtn_Graphs_32, Me.tblbtn_AdvGraphs_32, Me.tblbtn_Print_32, Me.tblbtn_Close_32})
        Me.tblStrip_32.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.tblStrip_32.Location = New System.Drawing.Point(0, 0)
        Me.tblStrip_32.Name = "tblStrip_32"
        Me.tblStrip_32.Size = New System.Drawing.Size(1028, 53)
        Me.tblStrip_32.TabIndex = 0
        Me.tblStrip_32.Text = "ToolStrip1"
        '
        'tblbtn_Add_32
        '
        Me.tblbtn_Add_32.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblbtn_Add_32.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tblbtn_Add_32.Image = CType(resources.GetObject("tblbtn_Add_32.Image"), System.Drawing.Image)
        Me.tblbtn_Add_32.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblbtn_Add_32.Name = "tblbtn_Add_32"
        Me.tblbtn_Add_32.Size = New System.Drawing.Size(37, 50)
        Me.tblbtn_Add_32.Tag = "Add"
        Me.tblbtn_Add_32.Text = "&New"
        Me.tblbtn_Add_32.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tblbtn_Modify_32
        '
        Me.tblbtn_Modify_32.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblbtn_Modify_32.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tblbtn_Modify_32.Image = CType(resources.GetObject("tblbtn_Modify_32.Image"), System.Drawing.Image)
        Me.tblbtn_Modify_32.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblbtn_Modify_32.Name = "tblbtn_Modify_32"
        Me.tblbtn_Modify_32.Size = New System.Drawing.Size(53, 50)
        Me.tblbtn_Modify_32.Tag = "Modify"
        Me.tblbtn_Modify_32.Text = "&Modify"
        Me.tblbtn_Modify_32.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tblbtn_Delete_32
        '
        Me.tblbtn_Delete_32.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblbtn_Delete_32.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tblbtn_Delete_32.Image = CType(resources.GetObject("tblbtn_Delete_32.Image"), System.Drawing.Image)
        Me.tblbtn_Delete_32.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblbtn_Delete_32.Name = "tblbtn_Delete_32"
        Me.tblbtn_Delete_32.Size = New System.Drawing.Size(50, 50)
        Me.tblbtn_Delete_32.Tag = "Delete"
        Me.tblbtn_Delete_32.Text = "&Delete"
        Me.tblbtn_Delete_32.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tblbtn_Refresh_32
        '
        Me.tblbtn_Refresh_32.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblbtn_Refresh_32.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tblbtn_Refresh_32.Image = CType(resources.GetObject("tblbtn_Refresh_32.Image"), System.Drawing.Image)
        Me.tblbtn_Refresh_32.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblbtn_Refresh_32.Name = "tblbtn_Refresh_32"
        Me.tblbtn_Refresh_32.Size = New System.Drawing.Size(58, 50)
        Me.tblbtn_Refresh_32.Tag = "Refresh"
        Me.tblbtn_Refresh_32.Text = "&Refresh"
        Me.tblbtn_Refresh_32.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tblbtn_Graphs_32
        '
        Me.tblbtn_Graphs_32.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblbtn_Graphs_32.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tblbtn_Graphs_32.Image = CType(resources.GetObject("tblbtn_Graphs_32.Image"), System.Drawing.Image)
        Me.tblbtn_Graphs_32.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblbtn_Graphs_32.Name = "tblbtn_Graphs_32"
        Me.tblbtn_Graphs_32.Size = New System.Drawing.Size(53, 50)
        Me.tblbtn_Graphs_32.Tag = "Graphs"
        Me.tblbtn_Graphs_32.Text = "&Graphs"
        Me.tblbtn_Graphs_32.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tblbtn_AdvGraphs_32
        '
        Me.tblbtn_AdvGraphs_32.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblbtn_AdvGraphs_32.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tblbtn_AdvGraphs_32.Image = CType(resources.GetObject("tblbtn_AdvGraphs_32.Image"), System.Drawing.Image)
        Me.tblbtn_AdvGraphs_32.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblbtn_AdvGraphs_32.Name = "tblbtn_AdvGraphs_32"
        Me.tblbtn_AdvGraphs_32.Size = New System.Drawing.Size(83, 50)
        Me.tblbtn_AdvGraphs_32.Tag = "Advanced Chart"
        Me.tblbtn_AdvGraphs_32.Text = "&Adv. Charts"
        Me.tblbtn_AdvGraphs_32.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tblbtn_AdvGraphs_32.ToolTipText = "Advance Charts"
        Me.tblbtn_AdvGraphs_32.Visible = False
        '
        'tblbtn_Print_32
        '
        Me.tblbtn_Print_32.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblbtn_Print_32.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tblbtn_Print_32.Image = CType(resources.GetObject("tblbtn_Print_32.Image"), System.Drawing.Image)
        Me.tblbtn_Print_32.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblbtn_Print_32.Name = "tblbtn_Print_32"
        Me.tblbtn_Print_32.Size = New System.Drawing.Size(41, 50)
        Me.tblbtn_Print_32.Tag = "Print"
        Me.tblbtn_Print_32.Text = "&Print"
        Me.tblbtn_Print_32.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tblbtn_Close_32
        '
        Me.tblbtn_Close_32.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblbtn_Close_32.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tblbtn_Close_32.Image = CType(resources.GetObject("tblbtn_Close_32.Image"), System.Drawing.Image)
        Me.tblbtn_Close_32.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblbtn_Close_32.Name = "tblbtn_Close_32"
        Me.tblbtn_Close_32.Size = New System.Drawing.Size(43, 50)
        Me.tblbtn_Close_32.Tag = "Close"
        Me.tblbtn_Close_32.Text = "&Close"
        Me.tblbtn_Close_32.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "")
        '
        'ContextMenu1
        '
        Me.ContextMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.cmnuMakeAsCurrent})
        '
        'cmnuMakeAsCurrent
        '
        Me.cmnuMakeAsCurrent.Index = 0
        Me.cmnuMakeAsCurrent.Text = "Make As Current"
        '
        'C1SuperTooltip1
        '
        Me.C1SuperTooltip1.BackgroundGradient = C1.Win.C1SuperTooltip.BackgroundGradient.None
        Me.C1SuperTooltip1.Font = New System.Drawing.Font("Tahoma", 8.0!)
        '
        'frmVWPatientVitals
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1028, 602)
        Me.Controls.Add(Me.pnlMain)
        Me.Controls.Add(Me.pnlTop)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmVWPatientVitals"
        Me.Text = "View Patient Vitals"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.pnlMain.ResumeLayout(False)
        CType(Me.C1PatientVitals, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlTop.ResumeLayout(False)
        Me.pnlTop.PerformLayout()
        Me.tblStrip_32.ResumeLayout(False)
        Me.tblStrip_32.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

#End Region

#Region " TO Check the Multiple instances Of Form "

    '' TO Keep track that the Form's Instance is Disposed or not
    Private blnDisposed As Boolean

    Private Shared frm As frmVWPatientVitals

    Public Shared Function GetInstance(ByVal Patientid As Long) As frmVWPatientVitals


        Try


            IsOpen = False
            ''If frm Is Nothing Then

            For Each f As Form In Application.OpenForms
                If f.Name = "frmVWPatientVitals" Then
                    'If CType(f, frmRpt_PatientICD9CPT) = PatientID Then
                    If CType(f, frmVWPatientVitals)._PatientID = Patientid Then
                        IsOpen = True
                        frm = f
                    End If
                    'End If

                End If
            Next
            If (IsOpen = False) Then
                frm = New frmVWPatientVitals(Patientid)
            End If

        Finally

        End Try
        Return frm
    End Function

#End Region
#Region " Patient Details Strip "
    Private WithEvents gloUC_PatientStrip1 As gloUC_PatientStrip

    Private Sub GloUC_PatientStrip1_ControlSizeChanged() Handles gloUC_PatientStrip1.ControlSizeChanged
        Try
            '' pnlPatientHeader.Height = gloUC_PatientStrip1.Height
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Set_PatientDetailStrip()
        ' '' Add Patient Details Control
        gloUC_PatientStrip1 = New gloUC_PatientStrip

        With gloUC_PatientStrip1
            .Dock = DockStyle.Top
            .Padding = New Padding(3, 0, 3, 0)
            '' Pass Paarameters Type of Form
            .ShowDetail(_PatientID, gloUC_PatientStrip.enumFormName.PatientVitals)
            '.pnlTranscationDate.Visible = False
            .BringToFront()
        End With
        Me.Controls.Add(gloUC_PatientStrip1)
        ''''
        'grdVital.BringToFront()
        pnlMain.BringToFront()
        pnlTop.SendToBack()
        '' Hide Previous Patient Details
        'pnlPatientHeader.Visible = False
        ' ''
    End Sub

#End Region

    Private Sub frmVWPatientVitals_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Try
            CType(Me.ParentForm, MainMenu).SetGnPatientID = GetCurrentPatientID
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Vitals, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try

    End Sub

    Private Sub frmVWPatientVitals_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing

    End Sub


    Private Sub frmVWPatientVitals_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load


        Try
            C1PatientVitals.ShowCellLabels = False
            C1PatientVitals.AllowSorting = True
            Call Set_PatientDetailStrip()
            Call Get_PatientDetails()


            dv = objclsPatientVitals.GetAllVitals(_PatientID)

            C1PatientVitals.Enabled = False
            C1PatientVitals.DataSource = dv
            C1PatientVitals.Enabled = True

            CustomGridStyle_New()
            If gblnAdvancedGrowthChart = True Then
                tblbtn_AdvGraphs_32.Visible = True
            Else
                tblbtn_AdvGraphs_32.Visible = False
            End If

            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Vitals, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Open, "View Patient Vitals Opened", _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)

            Try
                gloPatient.gloPatient.GetWindowTitle(Me, _PatientID, GetConnectionString(), gstrMessageBoxCaption)
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            End Try

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally

        End Try
    End Sub


    'Public Sub CustomGridStyle_old()
    '    'Dim ts As New DataGridTableStyle
    '    Dim dt As DataTable
    '    'Dim objclsCPT As New clsCPT
    '    ''''ts.ReadOnly = True
    '    ''''ts.AlternatingBackColor = System.Drawing.Color.Gainsboro
    '    ''''ts.BackColor = System.Drawing.Color.WhiteSmoke
    '    dt = objclsPatientVitals.GetDataview.Table
    '    ''''ts.MappingName = dt.TableName
    '    ''''ts.HeaderBackColor = System.Drawing.Color.DimGray
    '    ''''ts.HeaderFont = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    '    ''''ts.RowHeadersVisible = False

    '    Dim ts As New clsDataGridTableStyle(dt.TableName)

    '    Dim VitalIDCol As New DataGridTextBoxColumn
    '    With VitalIDCol
    '        .Width = 0
    '        .MappingName = dt.Columns("nVitalID").ColumnName
    '        .ReadOnly = True
    '        .HeaderText = "VitalID"
    '    End With

    '    Dim VisitIDCol As New DataGridTextBoxColumn
    '    With VisitIDCol
    '        .Width = 0
    '        .MappingName = dt.Columns("nVisitID").ColumnName
    '        .ReadOnly = True
    '        .HeaderText = "VisitID"
    '    End With

    '    Dim PatientIDCol As New DataGridTextBoxColumn
    '    With PatientIDCol
    '        .Width = 0
    '        .MappingName = dt.Columns("nPatientID").ColumnName
    '        .ReadOnly = True
    '        .HeaderText = "PatientID"
    '    End With

    '    Dim DateCol As New DataGridTextBoxColumn
    '    With DateCol
    '        .Width = 140
    '        .MappingName = dt.Columns("dtVitalDate").ColumnName
    '        .ReadOnly = True
    '        .HeaderText = "Vital Date"
    '        .NullText = ""
    '    End With

    '    Dim HeightCol As New DataGridTextBoxColumn
    '    With HeightCol
    '        .Width = 100
    '        .MappingName = dt.Columns("sHeight").ColumnName
    '        .ReadOnly = True
    '        '.HeaderText = "Height"
    '        '.HeaderText = "Height (ft)"
    '        .HeaderText = "Height (ft' in'')"
    '        .NullText = ""
    '    End With

    '    Dim WtlbsCol As New DataGridTextBoxColumn
    '    With WtlbsCol
    '        .Width = 100
    '        .MappingName = dt.Columns("dWeightinlbs").ColumnName
    '        .ReadOnly = True
    '        .HeaderText = "Weight (lbs)"
    '        .NullText = ""
    '    End With

    '    Dim WtChangeCol As New DataGridTextBoxColumn
    '    With WtChangeCol
    '        .Width = 120
    '        .MappingName = dt.Columns("dWeightChange").ColumnName
    '        .ReadOnly = True
    '        .HeaderText = "Weight Change"
    '        .NullText = ""
    '    End With

    '    Dim BMICol As New DataGridTextBoxColumn
    '    With BMICol
    '        .Width = 60
    '        .MappingName = dt.Columns("dBMI").ColumnName
    '        .ReadOnly = True
    '        .HeaderText = "BMI"
    '        .NullText = ""
    '    End With

    '    Dim WtKgCol As New DataGridTextBoxColumn
    '    With WtKgCol
    '        .Width = 100
    '        .MappingName = dt.Columns("dWeightinKg").ColumnName
    '        .ReadOnly = True
    '        .HeaderText = "Weight (kg)"
    '        .NullText = ""
    '    End With

    '    Dim TempCol As New DataGridTextBoxColumn
    '    With TempCol
    '        .Width = 120
    '        .MappingName = dt.Columns("dTemperature").ColumnName
    '        .ReadOnly = True
    '        .HeaderText = "Temperature (F)"
    '        .NullText = ""
    '    End With

    '    Dim CommentCol As New DataGridTextBoxColumn
    '    With CommentCol
    '        .Width = 120
    '        .MappingName = dt.Columns("sComments").ColumnName
    '        .ReadOnly = True
    '        .HeaderText = "Comments"
    '        .NullText = ""
    '    End With

    '    Dim RespiratoryCol As New DataGridTextBoxColumn
    '    With RespiratoryCol
    '        .Width = 130
    '        .MappingName = dt.Columns("dRespiratoryRate").ColumnName
    '        .ReadOnly = True
    '        .HeaderText = "Respiratory Rate"
    '        .NullText = ""
    '    End With

    '    Dim PulsePerMinCol As New DataGridTextBoxColumn
    '    With PulsePerMinCol
    '        .Width = 125
    '        .MappingName = dt.Columns("dPulsePerMinute").ColumnName
    '        .ReadOnly = True
    '        .HeaderText = "Pulse Per Minute"
    '        .NullText = ""
    '    End With

    '    Dim PulseOXCol As New DataGridTextBoxColumn
    '    With PulseOXCol
    '        .Width = 90
    '        .MappingName = dt.Columns("dPulseOx").ColumnName
    '        .ReadOnly = True
    '        .HeaderText = "Pulse OX"
    '        .NullText = ""
    '    End With

    '    '' SUDHIR 20090703 ''
    '    'Dim BPSittingMinCol As New DataGridTextBoxColumn
    '    'With BPSittingMinCol
    '    '    .Width = 140
    '    '    .MappingName = dt.Columns(13).ColumnName
    '    '    .ReadOnly = True
    '    '    '.HeaderText = "BP Sitting Min"
    '    '    .HeaderText = "Diastolic BP Sitting"
    '    '    .NullText = ""
    '    'End With

    '    'Dim BPSittingMaxCol As New DataGridTextBoxColumn
    '    'With BPSittingMaxCol
    '    '    .Width = 140
    '    '    .MappingName = dt.Columns(14).ColumnName
    '    '    .ReadOnly = True
    '    '    '.HeaderText = "BP Sitting Max"
    '    '    .HeaderText = "Systolic BP Sitting"

    '    '    .NullText = ""
    '    'End With

    '    Dim BPSittingCol As New DataGridTextBoxColumn
    '    With BPSittingCol
    '        .Width = 120
    '        .MappingName = dt.Columns("BPSitting").ColumnName
    '        .ReadOnly = True
    '        .HeaderText = "BP Sitting"

    '        .NullText = ""
    '    End With

    '    'Dim BPStandingMinCol As New DataGridTextBoxColumn
    '    'With BPStandingMinCol
    '    '    .Width = 155
    '    '    .MappingName = dt.Columns(15).ColumnName
    '    '    .ReadOnly = True
    '    '    '.HeaderText = "BP Standing Min"
    '    '    .HeaderText = "Diastolic BP Standing"
    '    '    .NullText = ""
    '    'End With

    '    'Dim BPStandingMaxCol As New DataGridTextBoxColumn
    '    'With BPStandingMaxCol
    '    '    .Width = 150
    '    '    .MappingName = dt.Columns(16).ColumnName
    '    '    .ReadOnly = True
    '    '    '.HeaderText = "BP Standing Max"
    '    '    .HeaderText = "Systolic BP Standing"
    '    '    .NullText = ""
    '    'End With


    '    Dim BPStandingCol As New DataGridTextBoxColumn
    '    With BPStandingCol
    '        .Width = 120
    '        .MappingName = dt.Columns("BPStanding").ColumnName
    '        .ReadOnly = True
    '        .HeaderText = "BP Standing"

    '        .NullText = ""
    '    End With


    '    Dim HCircumferenceCol As New DataGridTextBoxColumn
    '    With HCircumferenceCol
    '        .Width = 140
    '        .MappingName = dt.Columns("dHeadCircumferance").ColumnName
    '        .ReadOnly = True
    '        .HeaderText = "Head Circum (cm)"
    '        .NullText = ""
    '    End With

    '    Dim statureCol As New DataGridTextBoxColumn
    '    With statureCol
    '        .Width = 110
    '        .MappingName = dt.Columns("dStature").ColumnName
    '        .ReadOnly = True
    '        .HeaderText = "Stature (cm)"
    '        .NullText = ""
    '    End With
    '    Dim THRMinCol As New DataGridTextBoxColumn
    '    With THRMinCol
    '        .Width = 110
    '        .MappingName = dt.Columns("dTHRMin").ColumnName
    '        .ReadOnly = True
    '        .HeaderText = "THR Minimum"
    '        .NullText = ""
    '    End With

    '    Dim THRMaxCol As New DataGridTextBoxColumn
    '    With THRMaxCol
    '        .Width = 110
    '        .MappingName = dt.Columns("dTHRMax").ColumnName
    '        .ReadOnly = True
    '        .HeaderText = "THR Maximum"
    '        .NullText = ""
    '    End With

    '    Dim THRCol As New DataGridTextBoxColumn
    '    With THRCol
    '        .Width = 70
    '        .MappingName = dt.Columns("dTHR").ColumnName
    '        .ReadOnly = True
    '        .HeaderText = "THR"
    '        .NullText = ""
    '    End With

    '    ''Sudhir 20081204 To Insert New Vitals in Grid
    '    'dHeightinInch,dHeightinCm,sWeightinLbsOz,dTemperatureinCelcius,nPainLevel,dPEFR1,dPEFR2,dPEFR3,dHeadCircuminInch,dStatureinInch

    '    Dim HeightinInchCol As New DataGridTextBoxColumn
    '    With HeightinInchCol
    '        .Width = 100
    '        .MappingName = dt.Columns("dHeightinInch").ColumnName
    '        .ReadOnly = True
    '        .HeaderText = "Height (in)"
    '        .NullText = ""
    '    End With

    '    Dim HeightinCmCol As New DataGridTextBoxColumn
    '    With HeightinCmCol
    '        .Width = 90
    '        .MappingName = dt.Columns("dHeightinCm").ColumnName
    '        .ReadOnly = True
    '        .HeaderText = "Height (cm)"
    '        .NullText = ""
    '    End With

    '    Dim WeightinLbsOzCol As New DataGridTextBoxColumn
    '    With WeightinLbsOzCol
    '        .Width = 120
    '        .MappingName = dt.Columns("sWeightinLbsOz").ColumnName
    '        .ReadOnly = True
    '        .HeaderText = "Weight (lbs oz)"
    '        .NullText = ""
    '    End With

    '    Dim TemperatureinCelciusCol As New DataGridTextBoxColumn
    '    With TemperatureinCelciusCol
    '        .Width = 125
    '        .MappingName = dt.Columns("dTemperatureinCelcius").ColumnName
    '        .ReadOnly = True
    '        .HeaderText = "Temperature (C)"
    '        .NullText = ""
    '    End With

    '    Dim PainLevelCol As New DataGridTextBoxColumn
    '    With PainLevelCol
    '        .Width = 128
    '        .MappingName = dt.Columns("nPainLevel").ColumnName
    '        .ReadOnly = True
    '        .HeaderText = "Pain Level Current"
    '        .NullText = ""
    '    End With

    '    Dim PainLevelWithMedicationCol As New DataGridTextBoxColumn
    '    With PainLevelWithMedicationCol
    '        .Width = 195
    '        .MappingName = dt.Columns("nPainLevelWithMedication").ColumnName
    '        .ReadOnly = True
    '        .HeaderText = "Pain Level With Medication"
    '        .NullText = ""
    '    End With

    '    Dim PainLevelWithOutMedicationCol As New DataGridTextBoxColumn
    '    With PainLevelWithOutMedicationCol
    '        .Width = 205
    '        .MappingName = dt.Columns("nPainLevelWithoutMedication").ColumnName
    '        .ReadOnly = True
    '        .HeaderText = "Pain Level Without Medication"
    '        .NullText = ""
    '    End With

    '    Dim PainLevelWorstCol As New DataGridTextBoxColumn
    '    With PainLevelWorstCol
    '        .Width = 125
    '        .MappingName = dt.Columns("nPainLevelWorst").ColumnName
    '        .ReadOnly = True
    '        .HeaderText = "Pain Level Worst"
    '        .NullText = ""
    '    End With

    '    Dim PEFR1Col As New DataGridTextBoxColumn
    '    With PEFR1Col
    '        .Width = 70
    '        .MappingName = dt.Columns("dPEFR1").ColumnName
    '        .ReadOnly = True
    '        .HeaderText = "PEFR 1"
    '        .NullText = ""
    '    End With

    '    Dim PEFR2Col As New DataGridTextBoxColumn
    '    With PEFR2Col
    '        .Width = 70
    '        .MappingName = dt.Columns("dPEFR2").ColumnName
    '        .ReadOnly = True
    '        .HeaderText = "PEFR 2"
    '        .NullText = ""
    '    End With

    '    Dim PEFR3Col As New DataGridTextBoxColumn
    '    With PEFR3Col
    '        .Width = 70
    '        .MappingName = dt.Columns("dPEFR3").ColumnName
    '        .ReadOnly = True
    '        .HeaderText = "PEFR 3"
    '        .NullText = ""
    '    End With

    '    Dim HeadCircuminInchCol As New DataGridTextBoxColumn
    '    With HeadCircuminInchCol
    '        .Width = 150
    '        .MappingName = dt.Columns("dHeadCircuminInch").ColumnName
    '        .ReadOnly = True
    '        .HeaderText = "Head Circum (in)"
    '        .NullText = ""
    '    End With

    '    Dim StatureinInchCol As New DataGridTextBoxColumn
    '    With StatureinInchCol
    '        .Width = 120
    '        .MappingName = dt.Columns("dStatureinInch").ColumnName
    '        .ReadOnly = True
    '        .HeaderText = "Stature (in)"
    '        .NullText = ""
    '    End With

    '    '''''Added On 20100702 by sanjog
    '    Dim SiteForBloodPressureCol As New DataGridTextBoxColumn
    '    With SiteForBloodPressureCol
    '        .Width = 110
    '        .MappingName = dt.Columns("Site For BP").ColumnName
    '        .ReadOnly = True
    '        .HeaderText = "Site For BP"
    '        .NullText = ""
    '    End With

    '    Dim lastMenstrualPeriodCol As New DataGridTextBoxColumn
    '    With lastMenstrualPeriodCol
    '        .Width = 150
    '        .MappingName = dt.Columns("Last Menstrual Period").ColumnName
    '        .ReadOnly = True
    '        .HeaderText = "Last Menstrual Period"
    '        .NullText = ""
    '    End With

    '    Dim NeckCircumCMCol As New DataGridTextBoxColumn
    '    With NeckCircumCMCol
    '        .Width = 170
    '        .MappingName = dt.Columns("Neck Circumference (cm)").ColumnName
    '        .ReadOnly = True
    '        .HeaderText = "Neck Circumference (cm)"
    '        .NullText = ""
    '    End With

    '    Dim NeckCircumInCol As New DataGridTextBoxColumn
    '    With NeckCircumInCol
    '        .Width = 160
    '        .MappingName = dt.Columns("Neck Circumference (in)").ColumnName
    '        .ReadOnly = True
    '        .HeaderText = "Neck Circumference (in)"
    '        .NullText = ""
    '    End With

    '    Dim LeftEyePresCol As New DataGridTextBoxColumn
    '    With LeftEyePresCol
    '        .Width = 130
    '        .MappingName = dt.Columns("Left Eye Pressure").ColumnName
    '        .ReadOnly = True
    '        .HeaderText = "Left Eye Pressure"
    '        .NullText = ""
    '    End With

    '    Dim RightEyePresCol As New DataGridTextBoxColumn
    '    With RightEyePresCol
    '        .Width = 140
    '        .MappingName = dt.Columns("Right Eye Pressure").ColumnName
    '        .ReadOnly = True
    '        .HeaderText = "Right Eye Pressure"
    '        .NullText = ""
    '    End With

    '    Dim ODICol As New DataGridTextBoxColumn
    '    With ODICol
    '        .Width = 80
    '        .MappingName = dt.Columns("nODIPercent").ColumnName
    '        .ReadOnly = True
    '        .HeaderText = "ODI (%)"
    '        .NullText = ""
    '    End With
    '    '''''Added On 20100702 by sanjog

    '    ''Original gridStyle Comment by sudhir for new 
    '    'ts.GridColumnStyles.AddRange(New DataGridColumnStyle() {VitalIDCol, VisitIDCol, PatientIDCol, DateCol, HeightCol, WtlbsCol, WtChangeCol, BMICol, WtKgCol, TempCol, RespiratoryCol, PulsePerMinCol, PulseOXCol, BPSittingMinCol, BPSittingMaxCol, BPStandingMinCol, BPStandingMaxCol, CommentCol, HCircumferenceCol, statureCol, THRCol, THRMinCol, THRMaxCol})
    '    'ts.GridColumnStyles.AddRange(New DataGridColumnStyle() {VitalIDCol, VisitIDCol, PatientIDCol, DateCol, HeightCol, HeightinInchCol, HeightinCmCol, WeightinLbsOzCol, WtlbsCol, WtKgCol, WtChangeCol, BMICol, TempCol, TemperatureinCelciusCol, CommentCol, RespiratoryCol, PulsePerMinCol, PulseOXCol, BPSittingMinCol, BPSittingMaxCol, BPStandingMinCol, BPStandingMaxCol, HCircumferenceCol, HeadCircuminInchCol, statureCol, StatureinInchCol, THRCol, THRMinCol, THRMaxCol, PEFR1Col, PEFR2Col, PEFR3Col, PainLevelCol})
    '    ''Sandip Darade 20090320
    '    ''Removed header PEFR2Col, PEFR3Col
    '    '        ts.GridColumnStyles.AddRange(New DataGridColumnStyle() {VitalIDCol, VisitIDCol, PatientIDCol, DateCol, HeightCol, HeightinInchCol, HeightinCmCol, WeightinLbsOzCol, WtlbsCol, WtKgCol, WtChangeCol, BMICol, TempCol, TemperatureinCelciusCol, CommentCol, RespiratoryCol, PulsePerMinCol, PulseOXCol, BPSittingMinCol, BPSittingMaxCol, BPStandingMinCol, BPStandingMaxCol, HCircumferenceCol, HeadCircuminInchCol, statureCol, StatureinInchCol, THRCol, THRMinCol, THRMaxCol, PEFR1Col, PainLevelCol})
    '    '''''Added On 20100702 by sanjog
    '    ts.GridColumnStyles.AddRange(New DataGridColumnStyle() {VitalIDCol, VisitIDCol, PatientIDCol, DateCol, HeightCol, HeightinInchCol, HeightinCmCol, WeightinLbsOzCol, WtlbsCol, WtKgCol, WtChangeCol, BMICol, TempCol, TemperatureinCelciusCol, CommentCol, RespiratoryCol, PulsePerMinCol, PulseOXCol, BPSittingCol, BPStandingCol, HCircumferenceCol, HeadCircuminInchCol, statureCol, StatureinInchCol, THRCol, THRMinCol, THRMaxCol, PEFR1Col, PainLevelCol, PainLevelWithMedicationCol, PainLevelWithOutMedicationCol, PainLevelWorstCol, SiteForBloodPressureCol, lastMenstrualPeriodCol, NeckCircumCMCol, NeckCircumInCol, LeftEyePresCol, RightEyePresCol, ODICol})
    '    '''''Added On 20100702 by sanjog
    '    'grdVital.TableStyles.Clear()
    '    'grdVital.TableStyles.Add(ts)

    'End Sub
    Public Sub CustomGridStyle_New()



        Dim Vitals() As String
        Dim bpcnt As Integer = 0
        Dim str1 As String = ""
        Dim k As Int16
        Dim l As Int16 = 4
        Dim SelectedVitals() As String
        Dim objSettings As New clsSettings
        objSettings.GetSettings()

        If IsNothing(objSettings.VitalSettingsValue) = False Then
           

            If objSettings.VitalSettingsValue = "" Then
                objSettings.VitalSettingsValue = "0.1-Height (ft & in),0.2-Height (in),0.3-Height (cm),1.1-Weight (lbsoz),1.2-Weight (lbs),1.3-Weight (kg),2.0-Weight Change,3.0-BMI,4.0-Respiratory Rate,5.0-Pulse Per Min,6.0-Pulse OX,6.1-Pulse Ox w/Supplemental Oxygen,7.1-BP Sitting,7.2-BP Standing,8.1-Temperature (F),8.2-Temperature (C),9.0-PEFR,10.0-Last Menstrual Period,11.1-Head Circumference (in),11.2-Head Circumference (cm),12.1-Neck Circumference (in),12.2-Neck Circumference (cm),13.1-Stature (in),13.2-Stature (cm),14.0-Left Eye Pressure Over Time,15.0-Right Eye Pressure Over Time,16.0-Heart Rate,17.1-Pain Level : Current,17.2-Pain Level : With Medication,17.3-Pain Level : Without Medication,17.4-Pain Level : Worst,18.0-OB Vitals,19.0-ODI,20.0-Comments,21.0-DAS 28,22.1-Total Pregnancies,22.2-Full Term,22.3-Living,22.4-Multiple Births,22.5-Premature,22.6-Aborted (Spontaneous),22.7-Aborted (Induced),22.8-Ectopic"
            End If
            SelectedVitals = objSettings.VitalSettingsValue.Trim.Split(",")

            For k = 0 To SelectedVitals.Length - 1
                Vitals = SelectedVitals.GetValue(k).ToString().Split("-")
                If str1 = "" Then
                    str1 = Vitals.GetValue(1)
                Else
                    str1 = str1 & "," & Vitals.GetValue(1)
                End If
            Next
            Dim j As Int64
            SelectedVitals = str1.Split(",")
            Try
                Dim _col As Int16
                For _col = 0 To C1PatientVitals.Cols.Count - 1

                    C1PatientVitals.Cols(_col).Visible = False
                Next
                C1PatientVitals.Cols("dtVitalDate").Visible = True
            Catch ex As Exception
                MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try

            For j = 0 To SelectedVitals.Length - 1
                Select Case SelectedVitals(j)
                    Case "Height (ft & in)"
                        C1PatientVitals.Cols("sHeight").Visible = True
                        C1PatientVitals.Cols("sHeight").Move(l)
                        l = l + 1
                    Case "Weight (lbs)"
                        C1PatientVitals.Cols("dWeightinlbs").Visible = True
                        C1PatientVitals.Cols("dWeightinlbs").Move(l)
                        l = l + 1
                    Case "Weight Change"
                        C1PatientVitals.Cols("dWeightChange").Visible = True
                        C1PatientVitals.Cols("dWeightChange").Move(l)
                        l = l + 1
                    Case "BMI"
                        C1PatientVitals.Cols("dBMI").Visible = True
                        C1PatientVitals.Cols("dBMI").Move(l)
                        l = l + 1
                        C1PatientVitals.Cols("nBMIPercentile").Visible = True
                        C1PatientVitals.Cols("nBMIPercentile").Move(l)
                        l = l + 1
                    Case "Weight (kg)"
                        C1PatientVitals.Cols("dWeightinKg").Visible = True
                        C1PatientVitals.Cols("dWeightinKg").Move(l)
                        l = l + 1
                    Case "Temperature (F)"
                        C1PatientVitals.Cols("dTemperature").Visible = True
                        C1PatientVitals.Cols("dTemperature").Move(l)
                        l = l + 1
                    Case "Comments"
                        C1PatientVitals.Cols("sComments").Visible = True
                        C1PatientVitals.Cols("sComments").Move(l)
                        l = l + 1
                    Case "Respiratory Rate"
                        C1PatientVitals.Cols("dRespiratoryRate").Visible = True
                        C1PatientVitals.Cols("dRespiratoryRate").Move(l)
                        l = l + 1
                    Case "Pulse Per Min"
                        C1PatientVitals.Cols("dPulsePerMinute").Visible = True
                        C1PatientVitals.Cols("dPulsePerMinute").Move(l)
                        l = l + 1
                    Case "Pulse OX"
                        C1PatientVitals.Cols("dPulseOx").Visible = True
                        C1PatientVitals.Cols("dPulseOx").Move(l)
                        l = l + 1
                    Case "Pulse Ox w/Supplemental Oxygen"
                        C1PatientVitals.Cols("dPulseOxSupplement").Visible = True
                        C1PatientVitals.Cols("dPulseOxSupplement").Move(l)
                        l = l + 1
                        C1PatientVitals.Cols("dPulseRate").Visible = True
                        C1PatientVitals.Cols("dPulseRate").Move(l)
                        l = l + 1
                    Case "BP Sitting"
                        C1PatientVitals.Cols("BPSitting").Visible = True
                        C1PatientVitals.Cols("BPSitting").Move(l)
                        l = l + 1
                        C1PatientVitals.Cols("Site For BP").Visible = True
                        C1PatientVitals.Cols("Site For BP").Move(l)
                        l = l + 1
                    Case "BP Standing"
                        C1PatientVitals.Cols("BPStanding").Visible = True
                        C1PatientVitals.Cols("BPStanding").Move(l)
                        l = l + 1
                        C1PatientVitals.Cols("Site For BP").Visible = True
                        C1PatientVitals.Cols("Site For BP").Move(l)
                        l = l + 1
                    Case "Head Circumference (cm)"
                        C1PatientVitals.Cols("dHeadCircumferance").Visible = True
                        C1PatientVitals.Cols("dHeadCircumferance").Move(l)
                        l = l + 1
                    Case "Stature (cm)"
                        C1PatientVitals.Cols("dStature").Visible = True
                        C1PatientVitals.Cols("dStature").Move(l)
                        l = l + 1
                    Case "Heart Rate"
                        C1PatientVitals.Cols("dTHR").Visible = True
                        C1PatientVitals.Cols("dTHR").Move(l)
                        l = l + 1
                        C1PatientVitals.Cols("dTHRMin").Visible = True
                        C1PatientVitals.Cols("dTHRMin").Move(l)
                        l = l + 1
                        C1PatientVitals.Cols("dTHRMax").Visible = True
                        C1PatientVitals.Cols("dTHRMax").Move(l)
                        l = l + 1
                    Case "Height (in)"
                        C1PatientVitals.Cols("dHeightinInch").Visible = True
                        C1PatientVitals.Cols("dHeightinInch").Move(l)
                        l = l + 1
                    Case "Height (cm)"
                        C1PatientVitals.Cols("dHeightinCm").Visible = True
                        C1PatientVitals.Cols("dHeightinCm").Move(l)
                        l = l + 1
                    Case "Weight (lbsoz)"
                        C1PatientVitals.Cols("sWeightinLbsOz").Visible = True
                        C1PatientVitals.Cols("sWeightinLbsOz").Move(l)
                        l = l + 1
                    Case "Temperature (C)"
                        C1PatientVitals.Cols("dTemperatureinCelcius").Visible = True
                        C1PatientVitals.Cols("dTemperatureinCelcius").Move(l)
                        l = l + 1
                    Case "Pain Level : Current", "Pain Level"
                        C1PatientVitals.Cols("nPainLevel").Visible = True
                        C1PatientVitals.Cols("nPainLevel").Move(l)
                        l = l + 1
                    Case "Pain Level : With Medication"
                        C1PatientVitals.Cols("nPainLevelWithMedication").Visible = True
                        C1PatientVitals.Cols("nPainLevelWithMedication").Move(l)
                        l = l + 1
                    Case "Pain Level : Without Medication"
                        C1PatientVitals.Cols("nPainLevelWithoutMedication").Visible = True
                        C1PatientVitals.Cols("nPainLevelWithoutMedication").Move(l)
                        l = l + 1
                    Case "Pain Level : Worst"
                        C1PatientVitals.Cols("nPainLevelWorst").Visible = True
                        C1PatientVitals.Cols("nPainLevelWorst").Move(l)
                        l = l + 1
                    Case "PEFR"
                        C1PatientVitals.Cols("dPEFR1").Visible = True
                        C1PatientVitals.Cols("dPEFR1").Move(l)
                        l = l + 1
                    Case "Head Circumference (in)"
                        C1PatientVitals.Cols("dHeadCircuminInch").Visible = True
                        C1PatientVitals.Cols("dHeadCircuminInch").Move(l)
                        l = l + 1
                    Case "Stature (in)"
                        C1PatientVitals.Cols("dStatureinInch").Visible = True
                        C1PatientVitals.Cols("dStatureinInch").Move(l)
                        l = l + 1

                    Case "Last Menstrual Period"
                        C1PatientVitals.Cols("Last Menstrual Period").Visible = True
                        C1PatientVitals.Cols("Last Menstrual Period").Move(l)
                        l = l + 1
                    Case "Neck Circumference (in)"
                        C1PatientVitals.Cols("Neck Circumference (in)").Visible = True
                        C1PatientVitals.Cols("Neck Circumference (in)").Move(l)
                        l = l + 1
                    Case "Neck Circumference (cm)"
                        C1PatientVitals.Cols("Neck Circumference (cm)").Visible = True
                        C1PatientVitals.Cols("Neck Circumference (cm)").Move(l)
                        l = l + 1
                    Case "Left Eye Pressure Over Time"
                        C1PatientVitals.Cols("Left Eye Pressure").Visible = True
                        C1PatientVitals.Cols("Left Eye Pressure").Move(l)
                        l = l + 1
                    Case "Right Eye Pressure Over Time"
                        C1PatientVitals.Cols("Right Eye Pressure").Visible = True
                        C1PatientVitals.Cols("Right Eye Pressure").Move(l)
                        l = l + 1
                    Case "ODI"
                        C1PatientVitals.Cols("nODIPercent").Visible = True
                        C1PatientVitals.Cols("nODIPercent").Move(l)
                        l = l + 1
                    Case "DAS 28"
                        C1PatientVitals.Cols("nDAS28").Visible = True
                        C1PatientVitals.Cols("nDAS28").Move(l)
                        l = l + 1
                    Case "Total Pregnancies"
                        C1PatientVitals.Cols("TotalPregnancies").Visible = True
                        C1PatientVitals.Cols("TotalPregnancies").Move(l)
                        l = l + 1
                    Case "Full Term"
                        C1PatientVitals.Cols("FullTerm").Visible = True
                        C1PatientVitals.Cols("FullTerm").Move(l)
                        l = l + 1
                    Case "Living"
                        C1PatientVitals.Cols("Living").Visible = True
                        C1PatientVitals.Cols("Living").Move(l)
                        l = l + 1
                    Case "Multiple Births"
                        C1PatientVitals.Cols("MultipleBirth").Visible = True
                        C1PatientVitals.Cols("MultipleBirth").Move(l)
                        l = l + 1
                    Case "Premature"
                        C1PatientVitals.Cols("Premature").Visible = True
                        C1PatientVitals.Cols("Premature").Move(l)
                        l = l + 1
                    Case "Aborted (Induced)"
                        C1PatientVitals.Cols("Aborted Induced").Visible = True
                        C1PatientVitals.Cols("Aborted Induced").Move(l)
                        l = l + 1
                    Case "Aborted (Spontaneous)"
                        C1PatientVitals.Cols("AbortedSpontaneous").Visible = True
                        C1PatientVitals.Cols("AbortedSpontaneous").Move(l)
                        l = l + 1
                    Case "Ectopic"
                        C1PatientVitals.Cols("Ectopics").Visible = True
                        C1PatientVitals.Cols("Ectopics").Move(l)
                        l = l + 1
                End Select

            Next
        End If



    End Sub
    'Private Sub grdVital_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs)
    '    Try
    '        If grdVital.CurrentRowIndex >= 0 Then
    '            grdVital.Select(grdVital.CurrentRowIndex)
    '        End If
    '    Catch ex As Exception
    '        MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Me.Close()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            AddVital()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub AddVital()

        If MainMenu.IsAccess(False, _PatientID) = False Then
            Exit Sub
        End If

        If gblnProviderDisable = True Then
            If ShowAssociateProvider(_PatientID, Me) = True Then
                CType(Me.ParentForm, MainMenu).oPatientListControl.FillPatients()
            End If
        End If


        Dim frm1 As New frmPatientVitals(0, _PatientID)
        With frm1
            .ShowInTaskbar = False
            .StartPosition = FormStartPosition.CenterParent
            .ShowDialog(IIf(IsNothing(frm1.Parent), Me, frm1.Parent))
            .Dispose()
        End With
        C1PatientVitals.DataSource = objclsPatientVitals.GetAllVitals(_PatientID)

    End Sub

    Private Sub btnUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try

            If IsNothing(C1PatientVitals.Row) = False Then
                UpdateVital()
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub UpdateVital(Optional ByVal IsMakeAsCurrent As Boolean = False)
        If C1PatientVitals.Rows.Count > 1 Then

            If MainMenu.IsAccess(False, _PatientID) = False Then
                Exit Sub
            End If

            Dim VitalID As Long
            ''   VitalID = Convert.ToInt64(grdVital.Item(grdVital.CurrentRowIndex, 0))
            VitalID = Convert.ToInt64(C1PatientVitals.GetData(C1PatientVitals.Row, 0))

            Dim blnRecordLock As Boolean = False
            If gblnRecordLocking = True Then
                Dim mydt As mytable
                If IsMakeAsCurrent = False Then
                    mydt = Scan_n_Lock_Transaction(TrnType.PatientVitals, VitalID, 0, Now)
                    If (IsNothing(mydt) = False) Then
                
                        If mydt.Code <> gstrLoginName OrElse mydt.Description <> gstrClientMachineName Then
                            If MessageBox.Show("This Patient Vital is being modified by " & mydt.Code & " on " & mydt.Description & ". You cannot modify it now. Do you want to open it?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                                blnRecordLock = True
                            Else
                                If (IsNothing(mydt) = False) Then
                                    mydt.Dispose()
                                    mydt = Nothing
                                End If
                                Exit Sub
                            End If
                        End If
                        If (IsNothing(mydt) = False) Then
                            mydt.Dispose()
                            mydt = Nothing
                        End If
                End If

            End If
        End If

        Dim frm As New frmPatientVitals(VitalID, _PatientID, IsMakeAsCurrent, blnRecordLock)
        With frm
            .Text = "Modify Vital"
            .ShowInTaskbar = False
            .StartPosition = FormStartPosition.CenterParent
            .ShowDialog(IIf(IsNothing(frm.Parent), Me, frm.Parent))
            .Dispose()
            ''  grdVital.DataSource = objclsPatientVitals.GetAllVitals(_PatientID)
            C1PatientVitals.DataSource = objclsPatientVitals.GetAllVitals(_PatientID)
            Dim rowIndex As Int64
            rowIndex = C1PatientVitals.FindRow(VitalID, 1, 0, False, True, False)
            C1PatientVitals.Select(rowIndex, 0, True)
        End With
        End If

    End Sub


    Private Sub btnRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try

            Dim oclsPatReg As New ClsPatientRegistrationDBLayer
            With oclsPatReg
                Dim PatientStatus As String = ""
                PatientStatus = .PatientStatus(_PatientID)

                If PatientStatus = gtsrPatientStatus_Deceased Or PatientStatus = gtsrPatientStatus_Pending Then
                    MessageBox.Show("The status of the patient is '" & PatientStatus & "'." & vbCrLf & "You can not perform any activity on this Patient. ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Exit Sub
                End If
            End With
            oclsPatReg.Dispose()
            oclsPatReg = Nothing
            ''''<><><><><><><><><><><>''''




            If C1PatientVitals.Rows.Count > 1 Then
                Dim VitalID As Long = 0
                VitalID = Convert.ToString(C1PatientVitals.GetData(C1PatientVitals.Row, 0))
                Dim rowIndex As Int64
                rowIndex = C1PatientVitals.FindRow(VitalID, 1, 0, False, True, False)
                C1PatientVitals.Select(rowIndex, 0, True)

                If gblnRecordLocking = True Then
                    Dim mydt As mytable
                    mydt = Scan_n_Lock_Transaction(TrnType.PatientVitals, VitalID, 0, Now)
                    If (IsNothing(mydt) = False) Then
                        If mydt.Code <> gstrLoginName OrElse mydt.Description <> gstrClientMachineName Then
                            MessageBox.Show("This Patient Vital is being modified by " & mydt.Code & " on " & mydt.Description & ". You cannot delete it now.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            If (IsNothing(mydt) = False) Then
                                mydt.Dispose()
                                mydt = Nothing
                            End If
                            Exit Sub
                        End If
                        If (IsNothing(mydt) = False) Then
                            mydt.Dispose()
                            mydt = Nothing
                        End If
                    End If

                End If
                '''' <><><> Record Level Locking <><><><> 

                If MessageBox.Show("Are you sure to Delete Vital selected from Table ?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = DialogResult.Yes Then
                    objclsPatientVitals.DeleteVitals(Convert.ToString(C1PatientVitals.GetData(C1PatientVitals.Row, 0)), _PatientID)
                    C1PatientVitals.Enabled = False
                    C1PatientVitals.DataSource = objclsPatientVitals.GetAllVitals(_PatientID)
                    C1PatientVitals.Enabled = True
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            C1PatientVitals.Enabled = False
            C1PatientVitals.DataSource = objclsPatientVitals.GetAllVitals(_PatientID)
            C1PatientVitals.Enabled = True
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    'Private Sub grdVital_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdVital.DoubleClick
    '    Try

    '        Call UpdateVital()

    '    Catch ex As Exception
    '        MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Sub

    'Private Sub grdVital_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grdVital.MouseUp
    '    Try
    '        If grdVital.CurrentRowIndex >= 0 Then
    '            grdVital.Select(grdVital.CurrentRowIndex)
    '        End If
    '    Catch ex As Exception
    '        MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Sub

    'Private Sub grdVital_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grdVital.MouseDown
    '    Try
    '        Dim point As New Point(e.X, e.Y)

    '        Dim hti As DataGrid.HitTestInfo = grdVital.HitTest(point)

    '        For i As Int16 = 0 To grdVital.VisibleRowCount - 1
    '            grdVital.UnSelect(i)
    '        Next

    '        If e.Button = MouseButtons.Right Then
    '            If hti.Row >= 0 Then
    '                grdVital.ContextMenu = ContextMenu1
    '                ''sudhir 20090124
    '                grdVital.Select(hti.Row)
    '                grdVital.CurrentRowIndex = hti.Row
    '                ''
    '            Else
    '                grdVital.ContextMenu = Nothing
    '            End If
    '        End If


    '    Catch ex As Exception

    '    End Try

    'End Sub

    Private Sub cmnuMakeAsCurrent_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuMakeAsCurrent.Click
        Try
            Call UpdateVital(True)
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnGraphAgeHt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim ageCal = DateDiff(DateInterval.Month, CType(strPatientDOB, Date), Date.Now.Date)

            ' check for the patient age is more than 3 years
            If ageCal > 240 Then
                MessageBox.Show("No graphs available for this patient..", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If


        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub tblStrip_32_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tblStrip_32.ItemClicked
        Select Case e.ClickedItem.Tag
            Case "Close"
                CloseVitals()
            Case "Add" 'Save + Close
                AddVitals()
            Case "Modify" 'Save + finish
                ModifyVitals()
            Case "Delete"
                DeleteVitals()
            Case "Refresh"
                Try
                    Dim _col As Int16
                    For _col = 0 To C1PatientVitals.Cols.Count - 1

                        C1PatientVitals.Cols(_col).Visible = False
                    Next
                    C1PatientVitals.Cols("dtVitalDate").Visible = True
                Catch ex As Exception
                    MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
                RefreshVitals()
            Case "Graphs"
                GraphsVitals()
            Case "Advanced Chart"
                AdvChart()
            Case "Print"
                Print()

        End Select
    End Sub
    Private Sub Print()
        Try
            Dim frm As frmRptViewVitalCustomization
            Try
                frm = frmRptViewVitalCustomization.GetInstance(_PatientID, "RptViewVitalsCustomization")
                frm.MdiParent = Me.ParentForm
                frm.WindowState = FormWindowState.Maximized
                frm.Show()

            Catch ex As Exception
                MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Finally

            End Try
        Catch ex As Exception

        End Try
    End Sub
    Private Sub CloseVitals()
        Try
            Me.Close()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub AddVitals()
        Try
            AddVital()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub ModifyVitals()
        Try

            If IsNothing(C1PatientVitals.Row) = False Then
                UpdateVital()
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub DeleteVitals()
        Try

            If MainMenu.IsAccess(False, _PatientID) = False Then
                Exit Sub
            End If
            '''''<><><><><> Check Patient Status <><><><><><>''''

            If C1PatientVitals.Rows.Count > 1 Then
                Dim VitalID As Long = 0
                VitalID = Convert.ToString(C1PatientVitals.GetData(C1PatientVitals.Row, 0))
                Dim rowIndex As Int64
                rowIndex = C1PatientVitals.FindRow(VitalID, 1, 0, False, True, False)
                C1PatientVitals.Select(rowIndex, 0, True)
                If MessageBox.Show("Are you sure to Delete Vital selected from Table ?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) = DialogResult.Yes Then

                    Dim ocls As New clsDAS
                    ocls.Delete_DAS(Convert.ToString(C1PatientVitals.GetData(C1PatientVitals.Row, 0)))
                    If (IsNothing(ocls.DASImage) = False) Then
                        ocls.DASImage.Dispose()
                        ocls.DASImage = Nothing
                    End If
                    ocls = Nothing

                    objclsPatientVitals.DeleteVitals(Convert.ToString(C1PatientVitals.GetData(C1PatientVitals.Row, 0)), _PatientID)
                    C1PatientVitals.Enabled = False
                    C1PatientVitals.DataSource = objclsPatientVitals.GetAllVitals(_PatientID)
                    C1PatientVitals.Enabled = True
                    CustomGridStyle_New()
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub RefreshVitals()
        Try
            C1PatientVitals.Enabled = False
            C1PatientVitals.DataSource = objclsPatientVitals.GetAllVitals(_PatientID)
            C1PatientVitals.Enabled = True
            CustomGridStyle_New()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub GraphsVitals()
        '''''''The below IF Statement is added by Anil 0n 06/10/2007 
        '''''''The code is added because the application was giving error for empty "gstrPatientDOB".

        If C1PatientVitals.Row = -1 Then
            MessageBox.Show("No vital data available for this patient.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If
        If IsNothing(strPatientDOB) = True OrElse strPatientDOB = "" Then
            Exit Sub
        Else
            Try
                Dim ageCal = DateDiff(DateInterval.Month, CType(strPatientDOB, Date), Date.Now.Date)
                IsAgeGreater = False
                ' check for the patient age is more than 3 years
                If ageCal > 240 Then

                    IsAgeGreater = True
                End If



                Dim Frm As New frmShowGraphs(IsAgeGreater, _PatientID)
                With Frm
                    .Owner = Me
                    .ShowInTaskbar = False
                    .WindowState = FormWindowState.Maximized

                    CType(Me.MdiParent, MainMenu).ShowHideMainMenu(False, False)

                    '.ShowDialog(IIf(IsNothing(Frm.Parent), Me, Frm.Parent))
                    .ShowDialog(Frm.Parent)
                    .Dispose()
                End With

            Catch ex As IO.FileNotFoundException
                If (ex.Message.Contains("AxInterop.MSChart20Lib")) Then
                    MessageBox.Show("The required components for Grapg chart are missing. Please install.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            Catch ex As Runtime.InteropServices.COMException
                If (ex.ToString().Contains("REGDB_E_CLASSNOTREG")) Then
                    MessageBox.Show("The required components for Graph chart are missing. Please install.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If


            Catch ex As Exception
                MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                CType(Me.MdiParent, MainMenu).ShowHideMainMenu(False, False)

            End Try
        End If
    End Sub
    Private Sub frmVWPatientVitals_Closed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Closed
        Try

            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Vitals, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Close, "View Patient Vitals Closed", _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)

        Catch ex As Exception

        End Try


    End Sub
    Private Sub AdvChart()

        Dim isMessageShow As Boolean = False
        If C1PatientVitals.Row = -1 Then
            MessageBox.Show("No vital data available for this patient.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If


        Try
            Dim con As New SqlConnection(GetConnectionString)
            Dim adp As New SqlDataAdapter("Select sGender from Patient where nPatientID=" & _PatientID & "", con)
            Dim dt As New DataTable
            adp.Fill(dt)
            Dim _Gender As String = ""
            If Not IsNothing(dt) Then
                If dt.Rows.Count > 0 Then
                    _Gender = dt.Rows(0)("sGender")
                End If
                If _Gender = "Other" Then
                    MessageBox.Show("No Growth Charts available for specified gender.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If
            End If
        Catch ex As Exception

        End Try



        If IsNothing(strPatientDOB) = True OrElse strPatientDOB = "" Then
            Exit Sub
        Else
            Try
                Dim Frm As New frmAdvanceGraph(_PatientID)
                With Frm
                    .Owner = Me
                    .ShowInTaskbar = False
                    .WindowState = FormWindowState.Maximized
                    .MdiParent = CType(Me.MdiParent, MainMenu)
                    CType(Me.MdiParent, MainMenu).ShowHideMainMenu(False, False)

                    CType(Me.MdiParent, MainMenu).pnlMainToolBar.Visible = False
                    .Show()
                End With
            Catch ex As IO.FileNotFoundException
                If (ex.Message.Contains("GROWTHCHARTLib")) Then
                    MessageBox.Show("The required components for Advance chart are missing. Please install.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    isMessageShow = True
                End If
            Catch ex As Runtime.InteropServices.COMException
                If (ex.ToString().Contains("REGDB_E_CLASSNOTREG") And isMessageShow) Then
                    MessageBox.Show("The required components for Advance chart are missing. Please install.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If

            Catch ex As Exception
                MessageBox.Show("The required components for Advance chart are missing. Please install.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                CType(Me.MdiParent, MainMenu).ShowHideMainMenu(False, False)

                CType(Me.MdiParent, MainMenu).pnlMainToolBar.Visible = True
            Finally

                isMessageShow = False
            End Try
        End If
    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub


    'Private Sub grdVital_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grdVital.MouseMove
    '    Try
    '        Dim Tp As String
    '        Dim htInfo As DataGrid.HitTestInfo = grdVital.HitTest(e.X, e.Y)
    '        If htInfo.Type = Windows.Forms.DataGrid.HitTestType.Cell Then
    '            Tp = grdVital.Item(htInfo.Row, htInfo.Column).ToString()
    '            If String.Compare(ToolTip1.GetToolTip(grdVital), Tp) <> 0 Then
    '                If htInfo.Column = 14 Then
    '                    ToolTip1.SetToolTip(grdVital, grdVital.Item(htInfo.Row, htInfo.Column).ToString())
    '                Else
    '                    ToolTip1.SetToolTip(grdVital, "")
    '                End If
    '            End If


    '        End If
    '    Catch ex As Exception

    '    End Try
    'End Sub

    Private Function Get_PatientDetails()
        Dim dtPatient As DataTable = Nothing
        Try
            'dtPatient = New DataTable
            dtPatient = GetPatientInfo(_PatientID)
            If IsNothing(dtPatient) = False Then
                If dtPatient.Rows.Count > 0 Then
                    strPatientCode = Convert.ToString(dtPatient.Rows(0)("sPatientCode"))
                    strPatientFirstName = Convert.ToString(dtPatient.Rows(0)("sFirstName"))
                    strPatientMiddleName = Convert.ToString(dtPatient.Rows(0)("sMiddleName"))
                    strPatientLastName = Convert.ToString(dtPatient.Rows(0)("sLastName"))
                    strPatientDOB = Convert.ToString(dtPatient.Rows(0)("dtDOB"))
                    strPatientAge = GetAge(Convert.ToDateTime(dtPatient.Rows(0)("dtDOB")))
                    strPatientGender = Convert.ToString(dtPatient.Rows(0)("sGender"))
                    strPatientMaritalStatus = Convert.ToString(dtPatient.Rows(0)("sMaritalStatus"))

                End If
            End If
        Catch ex As Exception

        Finally
            If IsNothing(dtPatient) = False Then
                dtPatient.Dispose()
                dtPatient = Nothing
            End If

        End Try
        Return Nothing
    End Function

    ''' <summary>
    ''' Property writen for Patient Context
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property GetCurrentPatientID() As Int64 Implements mdlGeneral.IPatientContext.GetCurrentPatientID
        Get
            Return _PatientID  'Curent patient variable(Local variable) for this module 
        End Get
    End Property

    Private Sub C1PatientVitals_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        Try
            'If C1PatientVitals.Row >= 0 Then
            ' C1PatientVitals.Select(C1PatientVitals.Row)
            'End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    

    Private Sub C1PatientVitals_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles C1PatientVitals.MouseDoubleClick
        Try
            Dim ptPoint As Point = New Point(e.X, e.Y)
            Dim htInfo As C1.Win.C1FlexGrid.HitTestInfo = C1PatientVitals.HitTest(ptPoint)

            If htInfo.Type = C1.Win.C1FlexGrid.HitTestTypeEnum.Cell Then

                Call UpdateVital()

            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    'Private Sub C1PatientVitals_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
    '    Try
    '        Dim point As New Point(e.X, e.Y)

    '        Dim r As Integer = C1PatientVitals.HitTest(e.X, e.Y).Row

    '        'For i As Int16 = 0 To C1PatientVitals.Rows.Count - 1
    '        '    C1PatientVitals.Select (
    '        'Next

    '        If e.Button = MouseButtons.Right Then
    '            If r >= 0 Then
    '                C1PatientVitals.ContextMenu = ContextMenu1

    '                C1PatientVitals.Select(r, True)
    '                C1PatientVitals.Row = r
    '                ''
    '            Else
    '                C1PatientVitals.ContextMenu = Nothing
    '            End If
    '        End If


    '    Catch ex As Exception

    '    End Try
    'End Sub

   


    Private Sub C1PatientVitals_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles C1PatientVitals.MouseDown
        Try
            Dim point As New Point(e.X, e.Y)

            Dim r As Integer = C1PatientVitals.HitTest(e.X, e.Y).Row

            If e.Button = MouseButtons.Right Then
                If r > 0 Then
                    'Try
                    '    If (IsNothing(C1PatientVitals.ContextMenu) = False) Then
                    '        C1PatientVitals.ContextMenu.Dispose()
                    '        C1PatientVitals.ContextMenu = Nothing
                    '    End If
                    'Catch ex As Exception

                    'End Try
                    C1PatientVitals.ContextMenu = ContextMenu1

                    C1PatientVitals.Select(r, True)
                    C1PatientVitals.Row = r
                    ''
                Else
                    'Try
                    '    If (IsNothing(C1PatientVitals.ContextMenu) = False) Then
                    '        C1PatientVitals.ContextMenu.Dispose()
                    '        C1PatientVitals.ContextMenu = Nothing
                    '    End If
                    'Catch ex As Exception

                    'End Try
                    C1PatientVitals.ContextMenu = Nothing
                End If
            End If


        Catch ex As Exception

        End Try
    End Sub

    Private Sub C1PatientVitals_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles C1PatientVitals.MouseMove
        gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, sender, e.Location)
    End Sub
End Class
