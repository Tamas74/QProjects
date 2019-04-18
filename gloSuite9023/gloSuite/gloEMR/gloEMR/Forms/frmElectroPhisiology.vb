Imports System.IO
Imports Microsoft.Win32
Imports C1.Win.C1FlexGrid
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.Data.SqlClient
Imports System.Data.Common

Public Class frmElectroPhsiology
    Inherits System.Windows.Forms.Form

    Dim _PatientId As Long
    Dim _ExamId As Long
    Dim _VisitID As Int64

    Private WithEvents pnlInternalControl As Panel
    Dim ogloGridListControl As New gloBilling.gloGridListControl

    Private COL_ELECPHYSIOLOGYID As Integer = 0
    Private COL_PATIENTID As Integer = 1
    Private COL_EXAMID As Integer = 2
    Private COL_VISITID As Integer = 3
    Private COL_CLINICID As Integer = 4
    Private COL_PROCEDUREDATE As Integer = 5
    Private COL_CPTCODETESTTYPE As Integer = 6
    Private COL_PROCEDUREPERFORMED As Integer = 7
    Private COL_USERPROVIDERID As Integer = 8
    Private COL_BtnUSERPROVIDERID As Integer = 9


    Private COL_COUNT As Integer = 10

    '---
    Friend WithEvents tblStrip_32 As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents tblbtn_Save_32 As System.Windows.Forms.ToolStripButton
    Friend WithEvents tblbtn_Close_32 As System.Windows.Forms.ToolStripButton
    Friend WithEvents Label1 As System.Windows.Forms.Label

    Friend WithEvents pnlRtxtBox As System.Windows.Forms.Panel
    Friend WithEvents rchtxtbox As System.Windows.Forms.RichTextBox
    Private WithEvents lbl_BottomBrd As System.Windows.Forms.Label
    Private WithEvents lbl_LeftBrd As System.Windows.Forms.Label
    Private WithEvents lbl_RightBrd As System.Windows.Forms.Label
    Private WithEvents lbl_TopBrd As System.Windows.Forms.Label
    Private WithEvents Label8 As System.Windows.Forms.Label
    Private WithEvents Label11 As System.Windows.Forms.Label
    Private WithEvents Label12 As System.Windows.Forms.Label
    Private WithEvents Label13 As System.Windows.Forms.Label
    Private WithEvents Label14 As System.Windows.Forms.Label
    Private WithEvents Label15 As System.Windows.Forms.Label
    Private WithEvents Label16 As System.Windows.Forms.Label
    Private WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents C1SuperTooltip1 As C1.Win.C1SuperTooltip.C1SuperTooltip
    ''  Record Level Locking
    Private _blnRecordLock As Boolean = False

#Region " Windows Form Designer generated code "
    'commented by dipak as constructor not in use.
    'Public Sub New()
    '    MyBase.New()

    '    'This call is required by the Windows Form Designer.
    '    InitializeComponent()

    '    'Add any initialization after the InitializeComponent() call

    'End Sub

    Public Sub New(ByVal _nPatientId As Long, ByVal _nExamId As Int64, ByVal _nVisitId As Long)
        MyBase.New()
        'This call is required by the Windows Form Designer.
        InitializeComponent()
        'Add any initialization after the InitializeComponent() call

        _PatientId = _nPatientId
        _ExamId = _nExamId
        _VisitID = _nVisitId


    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            Dim dtpControls() As System.Windows.Forms.DateTimePicker = {dtpTransaction}
            Dim cntControls() As System.Windows.Forms.Control = {dtpTransaction}
            components.Dispose()
            Try
                gloGlobal.cEventHelper.RemoveAllEventHandlers(Me)
            Catch
            End Try



            If (IsNothing(dtpControls) = False) Then
                If dtpControls.Length > 0 Then
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(dtpControls)
                End If
            End If


            If (IsNothing(cntControls) = False) Then
                If cntControls.Length > 0 Then
                    gloGlobal.cEventHelper.DisposeAllControls(cntControls)
                End If
            End If

            If Not (components Is Nothing) Then
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
    Friend WithEvents C1FlexTransaction As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents pnlTop As System.Windows.Forms.Panel
    Friend WithEvents pnlBottom As System.Windows.Forms.Panel
    Friend WithEvents pnlMain As System.Windows.Forms.Panel
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cntxNothing As System.Windows.Forms.ContextMenu
    Friend WithEvents cntxMenuNothing As System.Windows.Forms.MenuItem
    Friend WithEvents lblPatientAddress As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents lblName As System.Windows.Forms.Label
    Friend WithEvents lblCode As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents dtpTransaction As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents lblDOB As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents lblPatientPhone As System.Windows.Forms.Label
    Friend WithEvents lblAge As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label

    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmElectroPhsiology))
        Me.C1FlexTransaction = New C1.Win.C1FlexGrid.C1FlexGrid
        Me.pnlTop = New System.Windows.Forms.Panel
        Me.dtpTransaction = New System.Windows.Forms.DateTimePicker
        Me.Label10 = New System.Windows.Forms.Label
        Me.lblDOB = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.lblPatientPhone = New System.Windows.Forms.Label
        Me.lblAge = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.lblPatientAddress = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.lblName = New System.Windows.Forms.Label
        Me.lblCode = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.lbl_BottomBrd = New System.Windows.Forms.Label
        Me.lbl_LeftBrd = New System.Windows.Forms.Label
        Me.lbl_RightBrd = New System.Windows.Forms.Label
        Me.lbl_TopBrd = New System.Windows.Forms.Label
        Me.pnlBottom = New System.Windows.Forms.Panel
        Me.tblStrip_32 = New gloGlobal.gloToolStripIgnoreFocus
        Me.tblbtn_Save_32 = New System.Windows.Forms.ToolStripButton
        Me.tblbtn_Close_32 = New System.Windows.Forms.ToolStripButton
        Me.pnlMain = New System.Windows.Forms.Panel
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.Label13 = New System.Windows.Forms.Label
        Me.cntxNothing = New System.Windows.Forms.ContextMenu
        Me.cntxMenuNothing = New System.Windows.Forms.MenuItem
        Me.pnlRtxtBox = New System.Windows.Forms.Panel
        Me.Label14 = New System.Windows.Forms.Label
        Me.Label15 = New System.Windows.Forms.Label
        Me.Label16 = New System.Windows.Forms.Label
        Me.Label17 = New System.Windows.Forms.Label
        Me.rchtxtbox = New System.Windows.Forms.RichTextBox
        Me.C1SuperTooltip1 = New C1.Win.C1SuperTooltip.C1SuperTooltip(Me.components)
        CType(Me.C1FlexTransaction, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlTop.SuspendLayout()
        Me.pnlBottom.SuspendLayout()
        Me.tblStrip_32.SuspendLayout()
        Me.pnlMain.SuspendLayout()
        Me.pnlRtxtBox.SuspendLayout()
        Me.SuspendLayout()
        '
        'C1FlexTransaction
        '
        Me.C1FlexTransaction.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        Me.C1FlexTransaction.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.C1FlexTransaction.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.C1FlexTransaction.ColumnInfo = "10,0,0,0,0,95,Columns:"
        Me.C1FlexTransaction.Dock = System.Windows.Forms.DockStyle.Fill
        Me.C1FlexTransaction.FocusRect = C1.Win.C1FlexGrid.FocusRectEnum.None
        Me.C1FlexTransaction.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.C1FlexTransaction.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.C1FlexTransaction.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcrossOut
        Me.C1FlexTransaction.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross
        Me.C1FlexTransaction.Location = New System.Drawing.Point(3, 3)
        Me.C1FlexTransaction.Name = "C1FlexTransaction"
        Me.C1FlexTransaction.Rows.DefaultSize = 19
        Me.C1FlexTransaction.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.C1FlexTransaction.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always
        Me.C1FlexTransaction.ShowCellLabels = True
        Me.C1FlexTransaction.Size = New System.Drawing.Size(779, 408)
        Me.C1FlexTransaction.StyleInfo = resources.GetString("C1FlexTransaction.StyleInfo")
        Me.C1FlexTransaction.TabIndex = 0
        Me.C1FlexTransaction.Tree.NodeImageCollapsed = CType(resources.GetObject("C1FlexTransaction.Tree.NodeImageCollapsed"), System.Drawing.Image)
        Me.C1FlexTransaction.Tree.NodeImageExpanded = CType(resources.GetObject("C1FlexTransaction.Tree.NodeImageExpanded"), System.Drawing.Image)
        '
        'pnlTop
        '
        Me.pnlTop.BackColor = System.Drawing.Color.Transparent
        Me.pnlTop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlTop.Controls.Add(Me.dtpTransaction)
        Me.pnlTop.Controls.Add(Me.Label10)
        Me.pnlTop.Controls.Add(Me.lblDOB)
        Me.pnlTop.Controls.Add(Me.Label7)
        Me.pnlTop.Controls.Add(Me.lblPatientPhone)
        Me.pnlTop.Controls.Add(Me.lblAge)
        Me.pnlTop.Controls.Add(Me.Label5)
        Me.pnlTop.Controls.Add(Me.Label9)
        Me.pnlTop.Controls.Add(Me.lblPatientAddress)
        Me.pnlTop.Controls.Add(Me.Label6)
        Me.pnlTop.Controls.Add(Me.Label3)
        Me.pnlTop.Controls.Add(Me.lblName)
        Me.pnlTop.Controls.Add(Me.lblCode)
        Me.pnlTop.Controls.Add(Me.Label4)
        Me.pnlTop.Controls.Add(Me.Label2)
        Me.pnlTop.Controls.Add(Me.Label1)
        Me.pnlTop.Controls.Add(Me.lbl_BottomBrd)
        Me.pnlTop.Controls.Add(Me.lbl_LeftBrd)
        Me.pnlTop.Controls.Add(Me.lbl_RightBrd)
        Me.pnlTop.Controls.Add(Me.lbl_TopBrd)
        Me.pnlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlTop.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlTop.Location = New System.Drawing.Point(0, 56)
        Me.pnlTop.Name = "pnlTop"
        Me.pnlTop.Padding = New System.Windows.Forms.Padding(3)
        Me.pnlTop.Size = New System.Drawing.Size(785, 126)
        Me.pnlTop.TabIndex = 1
        '
        'dtpTransaction
        '
        Me.dtpTransaction.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dtpTransaction.CalendarFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpTransaction.CalendarForeColor = System.Drawing.Color.Maroon
        Me.dtpTransaction.CalendarMonthBackground = System.Drawing.Color.White
        Me.dtpTransaction.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.dtpTransaction.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.dtpTransaction.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.dtpTransaction.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpTransaction.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpTransaction.Location = New System.Drawing.Point(380, 91)
        Me.dtpTransaction.Name = "dtpTransaction"
        Me.dtpTransaction.Size = New System.Drawing.Size(120, 22)
        Me.dtpTransaction.TabIndex = 15
        '
        'Label10
        '
        Me.Label10.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(304, 68)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(69, 14)
        Me.Label10.TabIndex = 18
        Me.Label10.Text = "Phone No :"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblDOB
        '
        Me.lblDOB.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblDOB.AutoSize = True
        Me.lblDOB.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDOB.Location = New System.Drawing.Point(387, 40)
        Me.lblDOB.Name = "lblDOB"
        Me.lblDOB.Size = New System.Drawing.Size(0, 14)
        Me.lblDOB.TabIndex = 16
        Me.lblDOB.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label7
        '
        Me.Label7.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(334, 40)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(39, 14)
        Me.Label7.TabIndex = 14
        Me.Label7.Text = "DOB :"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblPatientPhone
        '
        Me.lblPatientPhone.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblPatientPhone.AutoSize = True
        Me.lblPatientPhone.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPatientPhone.Location = New System.Drawing.Point(387, 68)
        Me.lblPatientPhone.Name = "lblPatientPhone"
        Me.lblPatientPhone.Size = New System.Drawing.Size(0, 14)
        Me.lblPatientPhone.TabIndex = 17
        Me.lblPatientPhone.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblAge
        '
        Me.lblAge.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblAge.AutoSize = True
        Me.lblAge.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAge.Location = New System.Drawing.Point(387, 10)
        Me.lblAge.Name = "lblAge"
        Me.lblAge.Size = New System.Drawing.Size(0, 14)
        Me.lblAge.TabIndex = 12
        Me.lblAge.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label5
        '
        Me.Label5.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(266, 95)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(120, 14)
        Me.Label5.TabIndex = 13
        Me.Label5.Text = "Transaction Date :   "
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label9
        '
        Me.Label9.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(336, 10)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(37, 14)
        Me.Label9.TabIndex = 11
        Me.Label9.Text = "Age :"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblPatientAddress
        '
        Me.lblPatientAddress.AutoSize = True
        Me.lblPatientAddress.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPatientAddress.Location = New System.Drawing.Point(124, 74)
        Me.lblPatientAddress.Name = "lblPatientAddress"
        Me.lblPatientAddress.Size = New System.Drawing.Size(0, 14)
        Me.lblPatientAddress.TabIndex = 10
        Me.lblPatientAddress.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(11, 74)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(101, 14)
        Me.Label6.TabIndex = 9
        Me.Label6.Text = "Patient Address :"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(23, 16)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(89, 14)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "Patient Name :"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblName
        '
        Me.lblName.AutoSize = True
        Me.lblName.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblName.Location = New System.Drawing.Point(124, 16)
        Me.lblName.Name = "lblName"
        Me.lblName.Size = New System.Drawing.Size(0, 14)
        Me.lblName.TabIndex = 6
        Me.lblName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblCode
        '
        Me.lblCode.AutoSize = True
        Me.lblCode.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCode.Location = New System.Drawing.Point(124, 46)
        Me.lblCode.Name = "lblCode"
        Me.lblCode.Size = New System.Drawing.Size(0, 14)
        Me.lblCode.TabIndex = 8
        Me.lblCode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(26, 46)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(86, 14)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "Patient Code :"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label2
        '
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label2.Location = New System.Drawing.Point(4, 118)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(777, 4)
        Me.Label2.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(4, 4)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(777, 6)
        Me.Label1.TabIndex = 0
        Me.Label1.Visible = False
        '
        'lbl_BottomBrd
        '
        Me.lbl_BottomBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_BottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_BottomBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_BottomBrd.Location = New System.Drawing.Point(4, 122)
        Me.lbl_BottomBrd.Name = "lbl_BottomBrd"
        Me.lbl_BottomBrd.Size = New System.Drawing.Size(777, 1)
        Me.lbl_BottomBrd.TabIndex = 22
        Me.lbl_BottomBrd.Text = "label2"
        '
        'lbl_LeftBrd
        '
        Me.lbl_LeftBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_LeftBrd.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_LeftBrd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_LeftBrd.Location = New System.Drawing.Point(3, 4)
        Me.lbl_LeftBrd.Name = "lbl_LeftBrd"
        Me.lbl_LeftBrd.Size = New System.Drawing.Size(1, 119)
        Me.lbl_LeftBrd.TabIndex = 21
        Me.lbl_LeftBrd.Text = "label4"
        '
        'lbl_RightBrd
        '
        Me.lbl_RightBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_RightBrd.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_RightBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_RightBrd.Location = New System.Drawing.Point(781, 4)
        Me.lbl_RightBrd.Name = "lbl_RightBrd"
        Me.lbl_RightBrd.Size = New System.Drawing.Size(1, 119)
        Me.lbl_RightBrd.TabIndex = 20
        Me.lbl_RightBrd.Text = "label3"
        '
        'lbl_TopBrd
        '
        Me.lbl_TopBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_TopBrd.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_TopBrd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_TopBrd.Location = New System.Drawing.Point(3, 3)
        Me.lbl_TopBrd.Name = "lbl_TopBrd"
        Me.lbl_TopBrd.Size = New System.Drawing.Size(779, 1)
        Me.lbl_TopBrd.TabIndex = 19
        Me.lbl_TopBrd.Text = "label1"
        '
        'pnlBottom
        '
        Me.pnlBottom.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlBottom.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlBottom.Controls.Add(Me.tblStrip_32)
        Me.pnlBottom.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlBottom.Location = New System.Drawing.Point(0, 0)
        Me.pnlBottom.Name = "pnlBottom"
        Me.pnlBottom.Size = New System.Drawing.Size(785, 56)
        Me.pnlBottom.TabIndex = 2
        '
        'tblStrip_32
        '
        Me.tblStrip_32.BackColor = System.Drawing.Color.Transparent
        Me.tblStrip_32.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tblStrip_32.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tblStrip_32.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblStrip_32.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tblStrip_32.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tblbtn_Save_32, Me.tblbtn_Close_32})
        Me.tblStrip_32.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.tblStrip_32.Location = New System.Drawing.Point(0, 0)
        Me.tblStrip_32.Name = "tblStrip_32"
        Me.tblStrip_32.Size = New System.Drawing.Size(785, 53)
        Me.tblStrip_32.TabIndex = 0
        Me.tblStrip_32.Text = "ToolStrip1"
        '
        'tblbtn_Save_32
        '
        Me.tblbtn_Save_32.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblbtn_Save_32.Image = CType(resources.GetObject("tblbtn_Save_32.Image"), System.Drawing.Image)
        Me.tblbtn_Save_32.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblbtn_Save_32.Name = "tblbtn_Save_32"
        Me.tblbtn_Save_32.Size = New System.Drawing.Size(66, 50)
        Me.tblbtn_Save_32.Text = "&Save&&Cls"
        Me.tblbtn_Save_32.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tblbtn_Save_32.ToolTipText = "Save and Close"
        '
        'tblbtn_Close_32
        '
        Me.tblbtn_Close_32.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblbtn_Close_32.Image = CType(resources.GetObject("tblbtn_Close_32.Image"), System.Drawing.Image)
        Me.tblbtn_Close_32.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblbtn_Close_32.Name = "tblbtn_Close_32"
        Me.tblbtn_Close_32.Size = New System.Drawing.Size(43, 50)
        Me.tblbtn_Close_32.Text = "Close"
        Me.tblbtn_Close_32.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'pnlMain
        '
        Me.pnlMain.Controls.Add(Me.Label8)
        Me.pnlMain.Controls.Add(Me.Label11)
        Me.pnlMain.Controls.Add(Me.Label12)
        Me.pnlMain.Controls.Add(Me.Label13)
        Me.pnlMain.Controls.Add(Me.C1FlexTransaction)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Location = New System.Drawing.Point(0, 182)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Padding = New System.Windows.Forms.Padding(3)
        Me.pnlMain.Size = New System.Drawing.Size(785, 414)
        Me.pnlMain.TabIndex = 3
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label8.Location = New System.Drawing.Point(4, 410)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(777, 1)
        Me.Label8.TabIndex = 8
        Me.Label8.Text = "label2"
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label11.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label11.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(3, 4)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(1, 407)
        Me.Label11.TabIndex = 7
        Me.Label11.Text = "label4"
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label12.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label12.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label12.Location = New System.Drawing.Point(781, 4)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(1, 407)
        Me.Label12.TabIndex = 6
        Me.Label12.Text = "label3"
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label13.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label13.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(3, 3)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(779, 1)
        Me.Label13.TabIndex = 5
        Me.Label13.Text = "label1"
        '
        'cntxNothing
        '
        Me.cntxNothing.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.cntxMenuNothing})
        '
        'cntxMenuNothing
        '
        Me.cntxMenuNothing.Index = 0
        Me.cntxMenuNothing.Text = "Delete"
        '
        'pnlRtxtBox
        '
        Me.pnlRtxtBox.Controls.Add(Me.Label14)
        Me.pnlRtxtBox.Controls.Add(Me.Label15)
        Me.pnlRtxtBox.Controls.Add(Me.Label16)
        Me.pnlRtxtBox.Controls.Add(Me.Label17)
        Me.pnlRtxtBox.Controls.Add(Me.rchtxtbox)
        Me.pnlRtxtBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlRtxtBox.Location = New System.Drawing.Point(0, 182)
        Me.pnlRtxtBox.Name = "pnlRtxtBox"
        Me.pnlRtxtBox.Padding = New System.Windows.Forms.Padding(3)
        Me.pnlRtxtBox.Size = New System.Drawing.Size(785, 414)
        Me.pnlRtxtBox.TabIndex = 4
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label14.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label14.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label14.Location = New System.Drawing.Point(4, 410)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(777, 1)
        Me.Label14.TabIndex = 8
        Me.Label14.Text = "label2"
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label15.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label15.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(3, 4)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(1, 407)
        Me.Label15.TabIndex = 7
        Me.Label15.Text = "label4"
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label16.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label16.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label16.Location = New System.Drawing.Point(781, 4)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(1, 407)
        Me.Label16.TabIndex = 6
        Me.Label16.Text = "label3"
        '
        'Label17
        '
        Me.Label17.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label17.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label17.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.Location = New System.Drawing.Point(3, 3)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(779, 1)
        Me.Label17.TabIndex = 5
        Me.Label17.Text = "label1"
        '
        'rchtxtbox
        '
        Me.rchtxtbox.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.rchtxtbox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.rchtxtbox.ForeColor = System.Drawing.Color.Black
        Me.rchtxtbox.Location = New System.Drawing.Point(3, 3)
        Me.rchtxtbox.Name = "rchtxtbox"
        Me.rchtxtbox.Size = New System.Drawing.Size(779, 408)
        Me.rchtxtbox.TabIndex = 0
        Me.rchtxtbox.Text = ""
        '
        'C1SuperTooltip1
        '
        Me.C1SuperTooltip1.BackgroundGradient = C1.Win.C1SuperTooltip.BackgroundGradient.None
        Me.C1SuperTooltip1.Font = New System.Drawing.Font("Tahoma", 8.0!)
        '
        'frmElectroPhsiology
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(785, 596)
        Me.Controls.Add(Me.pnlMain)
        Me.Controls.Add(Me.pnlRtxtBox)
        Me.Controls.Add(Me.pnlTop)
        Me.Controls.Add(Me.pnlBottom)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmElectroPhsiology"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Electro Physiology"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.C1FlexTransaction, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlTop.ResumeLayout(False)
        Me.pnlTop.PerformLayout()
        Me.pnlBottom.ResumeLayout(False)
        Me.pnlBottom.PerformLayout()
        Me.tblStrip_32.ResumeLayout(False)
        Me.tblStrip_32.PerformLayout()
        Me.pnlMain.ResumeLayout(False)
        Me.pnlRtxtBox.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

#Region " Patient Details Strip "
    Private WithEvents gloUC_PatientStrip1 As gloUserControlLibrary.gloUC_PatientStrip

    Private Sub GloUC_PatientStrip1_ControlSizeChanged() Handles gloUC_PatientStrip1.ControlSizeChanged
        Try
            '' pnlPatientHeader.Height = gloUC_PatientStrip1.Height
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Set_PatientDetailStrip()
        ' '' Add Patient Details Control
        If IsNothing(gloUC_PatientStrip1) = False Then
            Me.Controls.Remove(_gloUC_PatientStrip1)
            gloUC_PatientStrip1.Dispose()
            gloUC_PatientStrip1 = Nothing
        End If
        gloUC_PatientStrip1 = New gloUserControlLibrary.gloUC_PatientStrip

        With gloUC_PatientStrip1
            .Dock = DockStyle.Top
            .Padding = New Padding(3, 0, 3, 0)
            '' Pass Paarameters Type of Form
            .ShowDetail(_PatientId, gloUserControlLibrary.gloUC_PatientStrip.enumFormName.Immunization)
            .BringToFront()
        End With
        Me.Controls.Add(gloUC_PatientStrip1)
        '''''
        pnlMain.BringToFront()
        C1FlexTransaction.BringToFront()
        '' Hide Previous Patient Details
        pnlTop.Visible = False
        ' ''
    End Sub

#End Region


    Public Function PopulateElectroPhysioTable(ByVal _PatientID As Long, ByVal _VisitID As Long) As DataTable

        'Declare a variable for connection string
        Dim dt As New DataTable
        Dim sqladpt As SqlDataAdapter
        Dim conn As SqlConnection = New SqlConnection(GetConnectionString) '"server=gloint;database=gloEMR50_CCHIT2008_1;Integrated security=True")

        'Declare a variable for command
        Dim strQuery As String = "select isnull(nElectroPhysiologyID,0) as ElectroPhysiologyID, isnull(nPatientID,0) as PatientID, isnull(nExamID,0) as ExamID, isnull(nVisitID,0) as VisitID, isnull(nClinicID,0) as ClinicID, isnull(dtProcedureDate,0) as ProcedureDate, isnull(sCPTCode,'') as CPTCode, isnull(sProcedures,'') as Procedures,isnull(sUserProvider,'') as UserProvider from CV_ElectroPhysiology where nPatientID =  " & _PatientID & " and nVisitID = " & _VisitID

        Dim cmd As SqlCommand = New SqlCommand(strQuery, conn)
        sqladpt = New SqlDataAdapter
        sqladpt.SelectCommand = cmd

        'Fill data table_
        sqladpt.Fill(dt)
        sqladpt.Dispose()
        sqladpt = Nothing
        conn.Close()
        conn.Dispose()
        conn = Nothing

        If cmd IsNot Nothing Then
            cmd.Parameters.Clear()
            cmd.Dispose()
            cmd = Nothing
        End If
        'Return data table
        Return dt

    End Function


    Private Sub FillItems()

        'Set_PatientDetailStrip()

        With C1FlexTransaction
            'set properties of c1 grid
            '.Rows.Count = 1
            .Rows.Fixed = 1
            .Cols.Count = COL_COUNT
            .Cols.Fixed = 0
            .Rows(.Rows.Count - 1).Height = 22

            Dim _Width As Single = (.Width - 20) / 10

            .Rows.Add()
            'set column value
            .Cols(COL_ELECPHYSIOLOGYID).Width = _Width * 1  '_Width * 2
            .Cols(COL_PATIENTID).Width = _Width * 1  '_Width * 2
            .Cols(COL_EXAMID).Width = _Width * 1  '_Width * 2
            .Cols(COL_VISITID).Width = _Width * 1 '_Width * 2
            .Cols(COL_CLINICID).Width = _Width * 1 '_Width * 2
            .Cols(COL_PROCEDUREDATE).Width = _Width * 1  '_Width * 2
            .Cols(COL_CPTCODETESTTYPE).Width = _Width * 1 '_Width * 1
            .Cols(COL_PROCEDUREPERFORMED).Width = _Width * 1 '_Width * 1
            .Cols(COL_USERPROVIDERID).Width = _Width * 1 '_Width * 1
            .Cols(COL_BtnUSERPROVIDERID).Width = _Width * 0.15 '_Width * 1


            'set column header
            .SetData(0, COL_ELECPHYSIOLOGYID, "ElecPhysioID")
            .SetData(0, COL_PATIENTID, "PatientID")
            .SetData(0, COL_EXAMID, "ExamID")
            .SetData(0, COL_VISITID, "VisitID")
            .SetData(0, COL_CLINICID, "ClinicID")
            .SetData(0, COL_PROCEDUREDATE, "Procedure Date")
            .SetData(0, COL_CPTCODETESTTYPE, "CPT Code")
            .SetData(0, COL_PROCEDUREPERFORMED, "Procedure Performed")
            .SetData(0, COL_USERPROVIDERID, "Provider ID")
            .SetData(0, COL_BtnUSERPROVIDERID, "")



            'set visibility of the column
            .Cols(COL_ELECPHYSIOLOGYID).Visible = False
            .Cols(COL_PATIENTID).Visible = False
            .Cols(COL_EXAMID).Visible = False
            .Cols(COL_VISITID).Visible = False
            .Cols(COL_CLINICID).Visible = False
            .Cols(COL_PROCEDUREDATE).Visible = True
            .Cols(COL_CPTCODETESTTYPE).Visible = True 'True
            .Cols(COL_PROCEDUREPERFORMED).Visible = True
            .Cols(COL_USERPROVIDERID).Visible = True
            .Cols(COL_BtnUSERPROVIDERID).Visible = True


            'set data type of the column 
            .Cols(COL_ELECPHYSIOLOGYID).DataType = GetType(Long)
            .Cols(COL_PATIENTID).DataType = GetType(Long)
            .Cols(COL_EXAMID).DataType = GetType(Long)
            .Cols(COL_VISITID).DataType = GetType(Long)
            .Cols(COL_CLINICID).DataType = GetType(Long)
            .Cols(COL_PROCEDUREDATE).DataType = GetType(Date)
            .Cols(COL_CPTCODETESTTYPE).DataType = GetType(String)
            .Cols(COL_PROCEDUREPERFORMED).DataType = GetType(String)
            .Cols(COL_USERPROVIDERID).DataType = GetType(String)
            .Cols(COL_BtnUSERPROVIDERID).DataType = GetType(String)

            .Cols(COL_BtnUSERPROVIDERID).ComboList = "..."
            Dim UserProvName As DataTable

            UserProvName = GetUserProvName()
            Dim strUserProv As String = ""
            If Not UserProvName Is Nothing Then
                For i As Integer = 0 To UserProvName.Rows.Count - 1

                    strUserProv = strUserProv & "|" & UserProvName.Rows(i)("UserName")

                Next
                UserProvName.Dispose()
                UserProvName = Nothing
            End If


            .Cols(COL_USERPROVIDERID).ComboList = strUserProv





            ' set column editing properties.
            .Cols(COL_PROCEDUREDATE).AllowEditing = True
            .Cols(COL_CPTCODETESTTYPE).AllowEditing = True
            .Cols(COL_PROCEDUREPERFORMED).AllowEditing = True
            .Cols(COL_USERPROVIDERID).AllowEditing = True
            .Cols(COL_BtnUSERPROVIDERID).AllowEditing = True


            .Cols(COL_PROCEDUREPERFORMED).TextAlign = TextAlignEnum.LeftCenter
            .Cols(COL_USERPROVIDERID).TextAlign = TextAlignEnum.LeftCenter



            ''load the grid with the given patient exam & visit id
            'Bindgrid()

            'Dim CptControl As New gloBilling.gloGridListControl
            'CptControl.ControlType = gloBilling.gloGridListControlType.CPT



        End With
    End Sub

    Private Sub Bindgrid()
        Try
            Dim myDT As DataTable = Nothing
            'Dim _strSQL As String = "select nElectroPhysiologyID as ElectroPhysiologyID,nPatientID as PatientID, nExamID as ExamID, nVisitID as VisitID, nClinicID as ClinicID, dtProcedureDate as ProcedureDate, sCPTCode as CPTCode, sProcedures as Procedures from CV_ElectroPhysiology where nPatientID = " & _PatientId & " and nExamID = " & _ExamId & " "

            Dim _strSQL As String = "select nElectroPhysiologyID as ElectroPhysiologyID,nPatientID as PatientID, nExamID as ExamID, nVisitID as VisitID, nClinicID as ClinicID, dtProcedureDate as ProcedureDate, sCPTCode as CPTCode, sProcedures as Procedures from CV_ElectroPhysiology where nPatientID = 397693646236950001 and nExamID = 0  "
            Dim oDB As New gloStream.gloDataBase.gloDataBase

            oDB.Connect(GetConnectionString)
            myDT = oDB.ReadQueryDataTable(_strSQL)
            oDB.Disconnect()

            If Not IsNothing(myDT) Then
                If myDT.Rows.Count > 0 Then
                    For i As Int16 = 0 To myDT.Rows.Count - 1

                        With C1FlexTransaction
                            C1FlexTransaction.Rows.Add()
                            .SetData(i + 1, COL_ELECPHYSIOLOGYID, myDT.Rows(i)("ElectroPhysiologyID"))
                            .SetData(i + 1, COL_PATIENTID, myDT.Rows(i)("PatientID"))
                            .SetData(i + 1, COL_EXAMID, myDT.Rows(i)("ExamID"))
                            .SetData(i + 1, COL_VISITID, myDT.Rows(i)("VisitID"))
                            .SetData(i + 1, COL_CLINICID, myDT.Rows(i)("ClinicID"))
                            .SetData(i + 1, COL_PROCEDUREDATE, myDT.Rows(i)("ProcedureDate"))
                            '.SetData(i+1,COL_ELECPHYSIOLOGYID,mydt.Rows(i)("ElectroPhysiologyID")
                            '.SetData(i+1,COL_ELECPHYSIOLOGYID,mydt.Rows(i)("ElectroPhysiologyID")
                            .SetData(i + 1, COL_PROCEDUREPERFORMED, myDT.Rows(i)("Procedures"))
                        End With
                    Next
                End If
                myDT.Dispose()
                myDT = Nothing
            End If
            oDB.Dispose()
            oDB = Nothing

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Electrophysiology, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Function GetUserProvName() As DataTable
        Try
            Dim myDT As DataTable = Nothing
            Dim _strSQL As String = "select isnull(sLoginName,'') as UserName from user_mst union select isnull(sfirstname,'') + ' ' + isnull(smiddlename,'') + ' ' + isnull(slastname,'') as UserName from provider_mst"

            Dim oDB As New gloStream.gloDataBase.gloDataBase

            oDB.Connect(GetConnectionString)
            myDT = oDB.ReadQueryDataTable(_strSQL)
            oDB.Disconnect()

            oDB.Dispose()
            oDB = Nothing
            If IsNothing(myDT) = False Then
                Return myDT
            Else
                Return Nothing
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Electrophysiology, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Return Nothing
        End Try

    End Function

    Private Function getUserId(ByVal LoginName As String) As Long
        'Dim conn As New SqlClient.SqlConnection(GetConnectionString)
        ''Dim oDataReader As SqlClient.SqlDataReader
        'Dim cmdItemTrans As New SqlClient.SqlCommand
        'Dim userid As Long = 0

        'Try
        '    conn.Open()

        '    With cmdItemTrans
        '        .CommandType = CommandType.Text
        '        .Connection = conn
        '        .CommandText = "Select nUserID from user_mst where sLoginName = '" & LoginName & "'"
        '    End With

        '    userid = cmdItemTrans.ExecuteScalar

        '    Return userid

        'Catch ex As Exception
        '    MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error)
        'Finally
        '    conn.Close()

        'End Try
        Return Nothing
    End Function

    Private Function OpenInternalControl(ByVal ControlType As gloBilling.gloGridListControlType, ByVal ControlHeader As String, ByVal IsMultiSelect As Boolean, ByVal RowIndex As Integer, ByVal ColIndex As Integer, ByVal SearchText As String) As Boolean
        Dim _result As Boolean = False
        Try

            If Not ogloGridListControl Is Nothing Then
                CloseInternalControl()
            End If
            'ogloGridListControl = New gloBilling.gloGridListControl

          

            ogloGridListControl = New gloBilling.gloGridListControl(ControlType, False, pnlInternalControl.Width, RowIndex, ColIndex)
            ogloGridListControl.DatabaseConnectionString = GetConnectionString()
            AddHandler ogloGridListControl.ItemSelected, AddressOf C1ControlItemSelected
            AddHandler ogloGridListControl.InternalGridKeyDown, AddressOf C1ControlInternalGridKeyDown
            'gloBilling.ogloGridListControl.ItemSelected += New gloBilling.gloGridListControl.Item_Selected(ogloGridListControl_ItemSelected)
            'gloBilling.ogloGridListControl.InternalGridKeyDown += New gloBilling.gloGridListControl.Key_Down(ogloGridListControl_InternalGridKeyDown)

            ogloGridListControl.ClinicID = 1
            ogloGridListControl.ControlHeader = ControlHeader
            pnlInternalControl.Controls.Add(ogloGridListControl)
            ogloGridListControl.Dock = DockStyle.Fill

            'If SearchText <> "" Then
            '    ogloGridListControl.Search(SearchText, SearchColumn.Code)
            'End If
            ogloGridListControl.Show()

            Dim _x As Integer = C1FlexTransaction.Cols(ColIndex).Left
            Dim _y As Integer = C1FlexTransaction.Rows(RowIndex).Bottom
            Dim _width As Integer = pnlInternalControl.Width
            Dim _height As Integer = pnlInternalControl.Height
            Dim _parentleft As Integer = C1FlexTransaction.Left 'Me.Parent.Bounds.Left
            Dim _parentwidth As Integer = C1FlexTransaction.Right 'Me.Parent.Bounds.Width
            Dim _diffFactor As Integer = _parentwidth - _x

            If _diffFactor < _width Then
                _x = Me.Parent.Bounds.Width + (_diffFactor)
                pnlInternalControl.SetBounds(_x, _y, 0, 0, BoundsSpecified.Location)
            Else
                pnlInternalControl.SetBounds(_x, _y, 0, 0, BoundsSpecified.Location)
            End If

            'pnlInternalControl.SetBounds(c1Transaction.Cols[ColIndex].Left, c1Transaction.Rows[RowIndex].Bottom, 0, 0, BoundsSpecified.Location);
            pnlInternalControl.Visible = True
            pnlInternalControl.BringToFront()
            ogloGridListControl.Focus()
            _result = True
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Electrophysiology, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            _result = False
        Finally
            ' RePositionInternalControl()
        End Try
        Return _result
    End Function

    Private Sub C1ControlItemSelected(ByVal sender As Object, ByVal e As EventArgs)
        Try

            If Not IsNothing(ogloGridListControl) Then
                Dim strvalue As String = CType(ogloGridListControl.SelectedItems(0), gloGeneralItem.gloItem).Code
                C1FlexTransaction.SetData(C1FlexTransaction.RowSel, C1FlexTransaction.ColSel, strvalue)
                CloseInternalControl()
            End If
            pnlInternalControl.Visible = False
            pnlInternalControl.SendToBack()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Electrophysiology, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub C1ControlInternalGridKeyDown(ByVal sender As Object, ByVal e As EventArgs)

    End Sub

    Private Function CloseInternalControl() As Boolean
        Dim _result As Boolean = False
        Try
            'SLR: Changed on 4/2/2014
            'For i As Integer = pnlInternalControl.Controls.Count - 1 To 0 Step -1
            '    pnlInternalControl.Controls.RemoveAt(i)
            'Next
            If Not IsNothing(ogloGridListControl) Then
                pnlInternalControl.Controls.Remove(ogloGridListControl)
                RemoveHandler ogloGridListControl.ItemSelected, AddressOf C1ControlItemSelected
                RemoveHandler ogloGridListControl.InternalGridKeyDown, AddressOf C1ControlInternalGridKeyDown
                ogloGridListControl.Dispose()
                ogloGridListControl = Nothing
            End If
            pnlInternalControl.Visible = False
            pnlInternalControl.SendToBack()
            _result = True
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Electrophysiology, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            'gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
            _result = False
        Finally
        End Try
        Return _result
    End Function

    Private Sub tblbtn_Save_32_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tblbtn_Save_32.Click


        SaveElectroPhysioTest()

        'Dim ogloDatabaseLayer As New gloDatabaseLayer.DBLayer(GetConnectionString)
        'Dim odbParameters As gloDatabaseLayer.DBParameters

        'Try
        '    Dim Con As New SqlConnection(GetConnectionString)
        '    Dim cmd As SqlCommand

        '    If Con.State = ConnectionState.Closed Then
        '        Con.Open()
        '    End If

        '    '@nElectroPhysiologyID 	numeric(18,0) OutPut,
        '    '@nPatientID 	numeric(18,0),
        '    '@nExamID 	numeric(18,0),
        '    '@nVisitID 	numeric(18,0),
        '    '@nClinicID 	numeric(18,0),
        '    '@dtProcedureDate 		datetime,
        '    '@sCPTCode	Varchar(50),	
        '    '@sProcedures	Varchar(100),	
        '    '@sUserProvider Varchar(50),
        '    '@MachineID	Numeric(18,0)=0

        '    ogloDatabaseLayer.Connect(False)



        '    With C1FlexTransaction

        '        '.GetData(i, COL_PATIENTID)
        '        '.GetData(i, COL_EXAMID)
        '        '.GetData(i, COL_VISITID)
        '        '.GetData(i, COL_CLINICID)
        '        '.GetData(i, COL_PROCEDUREDATE)
        '        '.GetData(i, COL_CPTCODETESTTYPE)
        '        '.GetData(i, COL_PROCEDUREPERFORMED)
        '        '.GetData(i, COL_USERPROVIDERID)
        '        '.GetData(i, COL_MachineID)

        '        .Select(0, 0, False)

        '        For i As Int16 = 1 To .Rows.Count - 1
        '            odbParameters = New gloDatabaseLayer.DBParameters()
        '            odbParameters.Clear()

        '            If Not IsNothing(.GetData(i, COL_ELECPHYSIOLOGYID)) Then
        '                odbParameters.Add("@nElectroPhysiologyID", CType(.GetData(i, COL_ELECPHYSIOLOGYID), Long), ParameterDirection.InputOutput, SqlDbType.BigInt)
        '            Else
        '                odbParameters.Add("@nElectroPhysiologyID", 0, ParameterDirection.InputOutput, SqlDbType.BigInt)
        '            End If

        '            If Not IsNothing(.GetData(i, COL_PATIENTID)) Then
        '                odbParameters.Add("@nPatientID", CType(.GetData(i, COL_PATIENTID), Long), ParameterDirection.Input, SqlDbType.BigInt)
        '            Else
        '                odbParameters.Add("@nPatientID", gnPatientID, ParameterDirection.Input, SqlDbType.BigInt)
        '            End If

        '            If Not IsNothing(.GetData(i, COL_EXAMID)) Then
        '                odbParameters.Add("@nExamID", CType(.GetData(i, COL_EXAMID), Long), ParameterDirection.Input, SqlDbType.BigInt)
        '            Else
        '                odbParameters.Add("@nExamID", 0, ParameterDirection.Input, SqlDbType.BigInt)
        '            End If

        '            If Not IsNothing(.GetData(i, COL_VISITID)) Then
        '                odbParameters.Add("@nVisitID", CType(.GetData(i, COL_VISITID), Long), ParameterDirection.Input, SqlDbType.BigInt)
        '            Else
        '                odbParameters.Add("@nVisitID", 0, ParameterDirection.Input, SqlDbType.BigInt)
        '            End If

        '            If Not IsNothing(.GetData(i, COL_CLINICID)) Then
        '                odbParameters.Add("@nClinicID", CType(.GetData(i, COL_CLINICID), Int64), ParameterDirection.Input, SqlDbType.BigInt)
        '            Else
        '                odbParameters.Add("@nClinicID", 0, ParameterDirection.Input, SqlDbType.BigInt)
        '            End If

        '            If Not IsNothing(.GetData(i, COL_PROCEDUREDATE)) Then
        '                odbParameters.Add("@dtProcedureDate", CType(.GetData(i, COL_PROCEDUREDATE), Date), ParameterDirection.Input, SqlDbType.DateTime)
        '            Else
        '                odbParameters.Add("@dtProcedureDate", Now.Date, ParameterDirection.Input, SqlDbType.DateTime)
        '            End If
        '            If Not IsNothing(CType(.GetData(i, COL_CPTCODETESTTYPE), String)) Then
        '                odbParameters.Add("@sCPTCode", .GetData(i, COL_CPTCODETESTTYPE).ToString(), ParameterDirection.Input, SqlDbType.VarChar)
        '            Else
        '                odbParameters.Add("@sCPTCode", "", ParameterDirection.Input, SqlDbType.VarChar)
        '            End If
        '            If Not IsNothing(CType(C1FlexTransaction.GetData(i, COL_PROCEDUREPERFORMED), String)) Then
        '                odbParameters.Add("@sProcedures", .GetData(i, COL_PROCEDUREPERFORMED).ToString(), ParameterDirection.Input, SqlDbType.VarChar)
        '            Else
        '                odbParameters.Add("@sProcedures", "", ParameterDirection.Input, SqlDbType.VarChar)
        '            End If
        '            If Not IsNothing(CType(.GetData(i, COL_USERPROVIDERID), String)) Then
        '                odbParameters.Add("@sUserProvider", C1FlexTransaction.GetData(i, COL_USERPROVIDERID).ToString(), ParameterDirection.Input, SqlDbType.VarChar)
        '            Else
        '                odbParameters.Add("@sUserProvider", "", ParameterDirection.Input, SqlDbType.VarChar)
        '            End If



        '            odbParameters.Add("@MachineID", CType(.GetData(i, COL_ELECPHYSIOLOGYID), Int64), ParameterDirection.Input, SqlDbType.BigInt)

        '            ogloDatabaseLayer.Execute("sp_InUpCVElectroPhysio", odbParameters)

        '            odbParameters.Dispose()



        '        Next

        '    End With

        '    '' Insert Or Update problem List
        '    ogloDatabaseLayer.Disconnect()
        '    ogloDatabaseLayer.Dispose()

        '    Me.Close()

        'Catch ex As Exception
        'Finally
        'End Try
    End Sub

    Private Sub tblbtn_Close_32_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tblbtn_Close_32.Click


        Me.Close()

    End Sub

    ''''''''******This function is added by Anil on 01/10/07 at 10:10 a.m.
    '''''''*******This is added to add new Route, Site and Manufacturer using ContextMenu in the application in immunization module.

    Public Sub AddContextMenu()
        'Try
        '    ''''''''This code gets the column which is clicked by the user 
        '    Dim _CategoryName As String
        '    _CategoryName = ""
        '    If C1FlexTransaction.Col = COL_ROUTE Then
        '        _CategoryName = "Route"
        '    ElseIf C1FlexTransaction.Col = COL_MANUFACT Then
        '        _CategoryName = "Manf."
        '    ElseIf C1FlexTransaction.Col = COL_SITE Then
        '        _CategoryName = "Site"
        '    End If
        '    ''''''''end

        '    Dim frm As New CategoryMaster(_CategoryName)        '''''''Object of Form CategoryMaster is Declared
        '    ''''''Category name is assigned and name is given to the form at runtime and CategoryMaster is opened.

        '    If _CategoryName = "Route" Then
        '        frm.Text = "Add Route"
        '        frm.ShowDialog(Me)
        '        ''''''''''''
        '        Dim _Routes As String = " "
        '        Dim oRoutes As New gloStream.Immunization.Supporting.ItemDetails
        '        Dim oIMRoutes As New gloStream.Immunization.Common

        '        oRoutes = oIMRoutes.Route

        '        ' get item details
        '        If Not oRoutes Is Nothing Then
        '            For i As Integer = 1 To oRoutes.Count
        '                'If i = 1 Then
        '                '    _Manufacturers = oMans(i).Description
        '                'Else
        '                _Routes = _Routes & "|" & oRoutes(i).Description
        '                'End If
        '            Next
        '        End If
        '        oRoutes = Nothing
        '        oIMRoutes = Nothing

        '        ''''''''''''
        '        C1FlexTransaction.Cols(COL_ROUTE).ComboList = _Routes
        '    End If
        '    If _CategoryName = "Site" Then
        '        frm.Text = "Add Site"
        '        frm.ShowDialog(Me)

        '        Dim _Sites As String = " "
        '        Dim oSites As New gloStream.Immunization.Supporting.ItemDetails
        '        Dim oIMSite As New gloStream.Immunization.Common

        '        oSites = oIMSite.Sites
        '        ' get item details
        '        If Not oSites Is Nothing Then
        '            For i As Integer = 1 To oSites.Count
        '                'If i = 1 Then
        '                '    _Manufacturers = oMans(i).Description
        '                'Else
        '                _Sites = _Sites & "|" & oSites(i).Description
        '                'End If
        '            Next
        '        End If
        '        oSites = Nothing
        '        oIMSite = Nothing

        '        C1FlexTransaction.Cols(COL_SITE).ComboList = _Sites
        '    End If
        '    If _CategoryName = "Manf." Then
        '        frm.Text = "Add Manufacturer"
        '        frm.ShowDialog(Me)

        '        Dim _Manufacturers As String = " "
        '        Dim oMans As New gloStream.Immunization.Supporting.ItemDetails
        '        Dim oIM As New gloStream.Immunization.Common

        '        oMans = oIM.Manufacturers
        '        ' get item details
        '        If Not oMans Is Nothing Then
        '            For i As Integer = 1 To oMans.Count
        '                'If i = 1 Then
        '                '    _Manufacturers = oMans(i).Description
        '                'Else
        '                _Manufacturers = _Manufacturers & "|" & oMans(i).Description
        '                'End If
        '            Next
        '        End If
        '        oMans = Nothing
        '        oIM = Nothing

        '        C1FlexTransaction.Cols(COL_MANUFACT).ComboList = _Manufacturers
        '    End If

        'Catch ex As Exception
        '    MessageBox.Show(ex.Message, "Add Category", MessageBoxButtons.OK, MessageBoxIcon.Error)
        'End Try
    End Sub

    Public Function SaveElectroPhysioTest()
        Try
            Dim objElectroPhysio As clsElectroPhysio
            Dim Arrlist As New ArrayList
            Dim objElectroPhysioDBLayer As New clsElectroPhysioDBLayer
            With C1FlexTransaction
                .Select(0, 0, False)
                For i As Int16 = 1 To .Rows.Count - 1
                    If .GetData(i, COL_ELECPHYSIOLOGYID) = 0 Then
                        If Not IsNothing(.GetData(i, COL_PROCEDUREDATE)) Then
                            objElectroPhysio = New clsElectroPhysio
                            objElectroPhysio.ElectroPhysiologyID = .GetData(i, COL_ELECPHYSIOLOGYID)
                            objElectroPhysio.PatientID = _PatientId

                            objElectroPhysio.ExamID = _ExamId
                            objElectroPhysio.VisitID = _VisitID
                            objElectroPhysio.ClinicID = 1

                            .SetData(i, COL_PATIENTID, _PatientId)
                            .SetData(i, COL_EXAMID, _ExamId)
                            .SetData(i, COL_VISITID, _VisitID)
                            .SetData(i, COL_CLINICID, 1)

                            objElectroPhysio.dtProcedureDate = .GetData(i, COL_PROCEDUREDATE)
                            objElectroPhysio.CPTCode = .GetData(i, COL_CPTCODETESTTYPE)
                            objElectroPhysio.Procedures = .GetData(i, COL_PROCEDUREPERFORMED)

                            'objElectroPhysio.UserProvider = .GetData(i, COL_USERPROVIDERID)
                            Dim rg As C1.Win.C1FlexGrid.CellRange = C1FlexTransaction.GetCellRange(i, COL_USERPROVIDERID, i, COL_USERPROVIDERID)
                            Dim UserStyle As CellStyle = rg.Style()
                            Dim struser As String = ""
                            If Not UserStyle Is Nothing Then
                                If UserStyle.ComboList <> "" Then
                                    struser = UserStyle.ComboList
                                    objElectroPhysio.UserProvider = struser
                                End If
                            End If
                            Arrlist.Add(objElectroPhysio)
                        End If
                    Else
                        objElectroPhysio = New clsElectroPhysio
                        objElectroPhysio.ElectroPhysiologyID = .GetData(i, COL_ELECPHYSIOLOGYID)


                        objElectroPhysio.PatientID = .GetData(i, COL_PATIENTID)
                        objElectroPhysio.ExamID = .GetData(i, COL_EXAMID)
                        objElectroPhysio.VisitID = .GetData(i, COL_VISITID)
                        objElectroPhysio.ClinicID = .GetData(i, COL_CLINICID)

                        objElectroPhysio.dtProcedureDate = .GetData(i, COL_PROCEDUREDATE)
                        objElectroPhysio.CPTCode = .GetData(i, COL_CPTCODETESTTYPE)
                        objElectroPhysio.Procedures = .GetData(i, COL_PROCEDUREPERFORMED)

                        'objElectroPhysio.UserProvider = .GetData(i, COL_USERPROVIDERID)
                        Dim rg As C1.Win.C1FlexGrid.CellRange = C1FlexTransaction.GetCellRange(i, COL_USERPROVIDERID, i, COL_USERPROVIDERID)
                        Dim UserStyle As CellStyle = rg.Style()
                        Dim struser As String = ""
                        If Not UserStyle Is Nothing Then
                            If UserStyle.ComboList <> "" Then
                                struser = UserStyle.ComboList
                                objElectroPhysio.UserProvider = struser
                            End If
                        End If
                        Arrlist.Add(objElectroPhysio)
                    End If
                Next
                objElectroPhysioDBLayer.SaveElectroPhysioTest(Arrlist)
                objElectroPhysioDBLayer = Nothing
                Arrlist.Clear()
                Arrlist = Nothing

            End With
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Electrophysiology, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try

        Return True

    End Function


    Private Sub SetGridStyle(ByVal dt As DataTable)

        'Declare a variable


        'Dim cStyle As C1.Win.C1FlexGrid.CellStyle


        'Dim struser As String

        With C1FlexTransaction



            Dim _Width As Single = (.Width - 20) / 10

            Dim i As Int16
            '.Dock = DockStyle.Fill
            'Dim _TotalWidth As Single = 0
            '_TotalWidth = (.Width - 20) / 10
            '.Cols.Count = COL_COUNT
            '.Rows.Count = 1
            .AllowEditing = True
            .AllowAddNew = True

            'set properties of c1 grid
            .Rows.Count = 1
            .Rows.Fixed = 1
            .Cols.Count = COL_COUNT
            .Cols.Fixed = 0
            .Rows(.Rows.Count - 1).Height = 22

            .Styles.ClearUnused()

            .Cols(COL_ELECPHYSIOLOGYID).Width = 0 '_Width * 1
            .Cols(COL_ELECPHYSIOLOGYID).AllowEditing = True
            .SetData(0, COL_ELECPHYSIOLOGYID, "ELECPHYSIOID")
            .Cols(COL_ELECPHYSIOLOGYID).TextAlignFixed = TextAlignEnum.CenterCenter

            .Cols(COL_PATIENTID).Width = 0 '_Width * 1.2
            .Cols(COL_PATIENTID).AllowEditing = True
            .SetData(0, COL_PATIENTID, "PatientID")
            .Cols(COL_PATIENTID).TextAlignFixed = TextAlignEnum.CenterCenter

            .Cols(COL_EXAMID).Width = 0 '_Width * 1.2
            .Cols(COL_EXAMID).AllowEditing = True
            .SetData(0, COL_EXAMID, "ExamID")
            .Cols(COL_EXAMID).TextAlignFixed = TextAlignEnum.CenterCenter

            .Cols(COL_VISITID).Width = 0 '_Width * 1.2
            .Cols(COL_VISITID).AllowEditing = True
            .SetData(0, COL_VISITID, "VisitID")
            .Cols(COL_VISITID).TextAlignFixed = TextAlignEnum.CenterCenter

            .Cols(COL_CLINICID).Width = 0 '_Width * 1.2
            .Cols(COL_CLINICID).AllowEditing = True
            .SetData(0, COL_CLINICID, "ClinicID")
            .Cols(COL_CLINICID).TextAlignFixed = TextAlignEnum.CenterCenter

            .Cols(COL_PROCEDUREDATE).Width = _Width * 1.2
            .SetData(0, COL_PROCEDUREDATE, "Procedure Date")
            .Cols(COL_PROCEDUREDATE).DataType = GetType(Date)
            .Cols(COL_PROCEDUREDATE).AllowEditing = True

            .Cols(COL_CPTCODETESTTYPE).Width = _Width * 1.7
            .SetData(0, COL_CPTCODETESTTYPE, "CPT Code Type")
            .Cols(COL_CPTCODETESTTYPE).AllowEditing = True

            .Cols(COL_PROCEDUREPERFORMED).Width = _Width * 1.7
            .SetData(0, COL_PROCEDUREPERFORMED, "Procedure Performed")
            .Cols(COL_PROCEDUREPERFORMED).AllowEditing = True

            .Cols(COL_USERPROVIDERID).Width = _Width * 1
            .SetData(0, COL_USERPROVIDERID, "User Provider Name")
            .Cols(COL_USERPROVIDERID).AllowEditing = True


            .Cols(COL_BtnUSERPROVIDERID).Width = _Width * 0.15
            .SetData(0, COL_BtnUSERPROVIDERID, "")
            .Cols(COL_BtnUSERPROVIDERID).AllowEditing = True


            'Dim dt1 As DataTable
            'dt1 = fillusercombo()
            'Dim strUserName As New System.Text.StringBuilder
            'For j As Int32 = 0 To dt1.Rows.Count - 1
            '    If j > 0 Then
            '        strUserName.Append("|")
            '    End If
            '    strUserName.Append(dt1.Rows(j)("sLoginName"))
            'Next




            Dim cstyle1 As CellStyle
            cstyle1 = .Styles.Add("BubbleValues")
            Try
                If (.Styles.Contains("BubbleValues")) Then
                    cstyle1 = .Styles("BubbleValues")
                Else
                    cstyle1 = .Styles.Add("BubbleValues")

                End If
            Catch ex As Exception
                cstyle1 = .Styles.Add("BubbleValues")

            End Try
            cstyle1.ComboList = "..."
            .Cols(COL_BtnUSERPROVIDERID).Style = cstyle1

            '' Fill Values In ComboBox
            'csuser.ComboList = username
            '.Cols(COL_UserName).Style = csuser


            ' Table dt Contains following Columns
            ' StressID,PatientID,ExamId, VisitID , ClinicID
            'Dim UserProvName As DataTable

            'UserProvName = GetUserProvName()
            'Dim strUserProv As String = ""
            'If Not UserProvName Is Nothing Then
            '    For j As Integer = 0 To UserProvName.Rows.Count - 1

            '        strUserProv = strUserProv & "|" & UserProvName.Rows(j)("UserName")

            '    Next
            'End If


            .Cols(COL_USERPROVIDERID).ComboList = "" 'strUserProv


            For i = 0 To dt.Rows.Count - 1
                .Rows.Add()

                ' Fill the Retrived information to relative controls
                .SetData(i + 1, COL_ELECPHYSIOLOGYID, dt.Rows(i)("ElectroPhysiologyID"))
                .SetData(i + 1, COL_PATIENTID, dt.Rows(i)("PatientID"))
                .SetData(i + 1, COL_EXAMID, dt.Rows(i)("ExamID"))
                .SetData(i + 1, COL_VISITID, dt.Rows(i)("VisitID"))
                .SetData(i + 1, COL_CLINICID, dt.Rows(i)("ClinicID"))
                .SetData(i + 1, COL_PROCEDUREDATE, Format(dt.Rows(i)("ProcedureDate"), "MM/dd/yyyy"))
                .SetData(i + 1, COL_CPTCODETESTTYPE, dt.Rows(i)("CPTCode"))
                .SetData(i + 1, COL_PROCEDUREPERFORMED, dt.Rows(i)("Procedures"))

                Dim arrUsers() As String = Split(dt.Rows(i)("UserProvider"), "|")
                Dim strUserName As System.Text.StringBuilder
                If arrUsers.Length > 0 Then
                    strUserName = New System.Text.StringBuilder
                    For icnt As Int32 = 0 To arrUsers.Length - 1
                        If icnt > 0 Then
                            strUserName.Append("|")
                        End If
                        strUserName.Append(arrUsers(icnt))
                    Next
                    Dim csuser As CellStyle '= .Styles.Add("UserProvider")
                    Try
                        If (.Styles.Contains("UserProvider")) Then
                            csuser = .Styles("UserProvider")
                        Else
                            csuser = .Styles.Add("UserProvider")

                        End If
                    Catch ex As Exception
                        csuser = .Styles.Add("UserProvider")

                    End Try
                    'Fill Value in combo box
                    csuser.ComboList = strUserName.ToString()
                    .SetCellStyle(i + 1, COL_USERPROVIDERID, csuser)
                    .SetData(i + 1, COL_USERPROVIDERID, arrUsers(0))
                End If
                .SetData(i + 1, COL_BtnUSERPROVIDERID, "")
                Dim rgDig As C1.Win.C1FlexGrid.CellRange = .GetCellRange(i + 1, COL_BtnUSERPROVIDERID, i + 1, COL_BtnUSERPROVIDERID)
                rgDig.Style = cstyle1


                '.SetData(i + 1, COL_USERPROVIDERID, dt.Rows(i)("UserProvider"))


            Next

        End With

    End Sub

    'Private Sub FillC1Grid()

    '    Try

    '        Dim r As C1.Win.C1FlexGrid.Row
    '        r = C1FlexTransaction.Rows.Add()

    '        C1FlexTransaction.SetData(r.Index, COL_PATIENTID, m_patientID)
    '        C1Cardiology.SetData(r.Index, COL_ExamID, m_ExamID)
    '        C1Cardiology.SetData(r.Index, COL_VisitID, m_VisitID)

    '        Dim dtDevicetype As DataTable
    '        dtDevicetype = New DataTable()
    '        dtDevicetype = GetDeviceTypeData()

    '        Dim strComboString As String = " "
    '        For i As Int64 = 0 To dtDevicetype.Rows.Count - 1
    '            strComboString = strComboString & "|" & dtDevicetype.Rows(i)(0).ToString
    '        Next
    '        '' add ctstyle for combo of c1
    '        Dim cStyle As C1.Win.C1FlexGrid.CellStyle
    '        Dim rgOperator As C1.Win.C1FlexGrid.CellRange = C1Cardiology.GetCellRange(r.Index, COL_TypeofDevice, r.Index, COL_TypeofDevice)
    '        cStyle = C1Cardiology.Styles.Add("Devicetype")
    '        cStyle.ComboList = strComboString
    '        strComboString = ""
    '        rgOperator.Style = cStyle

    '        Dim dtProductname As DataTable
    '        dtProductname = New DataTable()
    '        dtProductname = GetProductData()
    '        strComboString = ""
    '        For i As Int32 = 0 To dtProductname.Rows.Count - 1
    '            strComboString = strComboString & "|" & dtProductname.Rows(i)(0).ToString
    '        Next
    '        Dim cStyle1 As C1.Win.C1FlexGrid.CellStyle
    '        Dim rgOperator1 As C1.Win.C1FlexGrid.CellRange = C1Cardiology.GetCellRange(r.Index, COL_ProductName, r.Index, COL_ProductName)
    '        cStyle1 = C1Cardiology.Styles.Add("ProductionName")
    '        cStyle1.ComboList = strComboString
    '        strComboString = ""
    '        rgOperator1.Style = cStyle1

    '        Dim dtDeviceManf As DataTable
    '        dtDeviceManf = New DataTable()
    '        dtDeviceManf = GetDevicemanfData()
    '        strComboString = ""
    '        For i As Int32 = 0 To dtDeviceManf.Rows.Count - 1
    '            strComboString = strComboString & "|" & dtDeviceManf.Rows(i)(0).ToString
    '        Next
    '        Dim cStyle2 As C1.Win.C1FlexGrid.CellStyle
    '        Dim rgOperator2 As C1.Win.C1FlexGrid.CellRange = C1Cardiology.GetCellRange(r.Index, COL_DeviceManufacturer, r.Index, COL_DeviceManufacturer)
    '        cStyle2 = C1Cardiology.Styles.Add("DeviceManf")
    '        cStyle2.ComboList = strComboString
    '        strComboString = ""
    '        rgOperator2.Style = cStyle2

    '        Dim dtLeadtype As DataTable
    '        dtLeadtype = New DataTable()
    '        dtLeadtype = GetLeadData()
    '        strComboString = ""
    '        For i As Int32 = 0 To dtLeadtype.Rows.Count - 1
    '            strComboString = strComboString & "|" & dtLeadtype.Rows(i)(0).ToString
    '        Next
    '        Dim cStyle3 As C1.Win.C1FlexGrid.CellStyle
    '        Dim rgOperator3 As C1.Win.C1FlexGrid.CellRange = C1Cardiology.GetCellRange(r.Index, COL_LeadsType, r.Index, COL_LeadsType)
    '        cStyle3 = C1Cardiology.Styles.Add("Leadtype")
    '        cStyle3.ComboList = strComboString
    '        strComboString = ""
    '        rgOperator3.Style = cStyle3

    '        'C1Cardiology.Rows.Add()

    '    Catch ex As Exception
    '        MessageBox.Show(ex.ToString)
    '    End Try
    'End Sub

    Private Sub frmElectroPhsiology_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        gloC1FlexStyle.Style(C1FlexTransaction)
        Try
            Set_PatientDetailStrip()

            pnlInternalControl = New Panel
            pnlInternalControl.Width = 337
            pnlInternalControl.Height = 221
            pnlInternalControl.Visible = False
            pnlInternalControl.SendToBack()
            C1FlexTransaction.Controls.Add(pnlInternalControl)

            Dim dt As DataTable = PopulateElectroPhysioTable(_PatientId, _VisitID)
            SetGridStyle(dt)
            dt.Dispose()
            dt = Nothing
            'FillItems()

            'If C1FlexTransaction.Rows.Count <> 1 Then
            '    C1FlexTransaction.Rows.Add()
            'End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Electrophysiology, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If _blnRecordLock = True Then
                '' Record is Opened by other user 
                tblbtn_Save_32.Enabled = False
            Else
                tblbtn_Save_32.Enabled = True
            End If
        End Try
    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Private Sub C1FlexTransaction_CellButtonClick(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles C1FlexTransaction.CellButtonClick
        Try
            With C1FlexTransaction

                If (e.Col = COL_BtnUSERPROVIDERID) Then
                    'send true to populate only user and false to populate all users and providers
                    Dim ofrmUser As New frmUserList(False)
                    If Not IsNothing(.GetCellStyle(e.Row, COL_USERPROVIDERID)) Then
                        If Not IsNothing(.GetCellStyle(e.Row, COL_USERPROVIDERID).ComboList) Then
                            ofrmUser.Users = .GetCellStyle(e.Row, COL_USERPROVIDERID).ComboList
                        End If
                    End If

                    ofrmUser.WindowState = FormWindowState.Normal
                    ofrmUser.StartPosition = FormStartPosition.CenterParent
                    ofrmUser.ShowDialog(IIf(IsNothing(ofrmUser.Parent), Me, ofrmUser.Parent))

                    Dim strUserName As String = ofrmUser.Users
                    Dim csuser As CellStyle '= .Styles.Add("UserName")
                    Try
                        If (.Styles.Contains("UserName")) Then
                            csuser = .Styles("UserName")
                        Else
                            csuser = .Styles.Add("UserName")

                        End If
                    Catch ex As Exception
                        csuser = .Styles.Add("UserName")

                    End Try
                    'Fill Value in combo box
                    csuser.ComboList = strUserName.ToString()
                    .SetCellStyle(e.Row, COL_USERPROVIDERID, csuser)

                    If strUserName.Length > 0 Then
                        .SetData(e.Row, COL_USERPROVIDERID, strUserName.Split("|").GetValue(0))
                    End If
                    ofrmUser.Dispose()
                    ofrmUser = Nothing
                End If
            End With

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Electrophysiology, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub C1FlexTransaction_ChangeEdit(ByVal sender As Object, ByVal e As System.EventArgs) Handles C1FlexTransaction.ChangeEdit
        Dim _strSearchString As String = ""
        Try

            _strSearchString = C1FlexTransaction.Editor.Text

            If ogloGridListControl IsNot Nothing Then
                'ogloGridListControl.Search(_strSearchString, SearchColumn.Code); 
                'ogloGridListControl.InStringSearch(_strSearchString); 

                If C1FlexTransaction.Col = COL_PROCEDUREPERFORMED Then
                    'ogloGridListControl.FillControl(_strSearchString); 

                    'Dim _cptCode As String = ""
                    'Dim _facilityCode As String = ""

                    'If C1StressList IsNot Nothing AndAlso C1StressList.Rows.Count > 0 Then
                    '    _cptCode = Convert.ToString(C1StressList.GetData(C1StressList.Row, COL_Procedure))
                    '    _facilityCode = Convert.ToString(C1StressList.GetData(C1StressList.Row, COL_Procedure))
                    '    ogloGridListControl.SelectedCPTCode = _cptCode
                    '    ogloGridListControl.SelectedFacilityCode = _facilityCode
                    'End If

                    ogloGridListControl.FillControl(_strSearchString)
                Else
                    ogloGridListControl.AdvanceSearch(_strSearchString)
                End If

            End If




        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Electrophysiology, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show("ERROR : " & ex.ToString(), "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally

        End Try
    End Sub

    Private Sub C1FlexTransaction_RowColChange(ByVal sender As Object, ByVal e As System.EventArgs) Handles C1FlexTransaction.RowColChange
        Try
            If C1FlexTransaction.Col <> COL_CPTCODETESTTYPE Then
                CloseInternalControl()
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Electrophysiology, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub C1FlexTransaction_StartEdit(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles C1FlexTransaction.StartEdit
        Try
            If e.Col = COL_CPTCODETESTTYPE Then
                Dim str As String = C1FlexTransaction.Editor.Text
                OpenInternalControl(gloBilling.gloGridListControlType.CPT, "CPT", False, e.Row, e.Col, str)

            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Electrophysiology, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub C1FlexTransaction_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles C1FlexTransaction.MouseMove
        gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, sender, e.Location)
    End Sub
End Class
