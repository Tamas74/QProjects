Imports AxMSChart20Lib.AxMSChart
Imports System.Drawing
Imports System.Diagnostics
Imports gloUserControlLibrary
Imports System.Text
Imports System.IO

Public Class frmShowGraphs
#Region " Windows Controls "
    Inherits System.Windows.Forms.Form
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents ToolTip2 As System.Windows.Forms.ToolTip
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents tblStrip_32 As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents tblbtn_AgeStature_32 As System.Windows.Forms.ToolStripButton
    Friend WithEvents tblbtn_AgeWt24_32 As System.Windows.Forms.ToolStripButton
    Friend WithEvents tblbtn_WtHt_32 As System.Windows.Forms.ToolStripButton
    Friend WithEvents tblbtn_AgeCircum_32 As System.Windows.Forms.ToolStripButton
    Friend WithEvents tblbtn_AgeHt_32 As System.Windows.Forms.ToolStripButton
    Friend WithEvents tblbtn_AgeWt_32 As System.Windows.Forms.ToolStripButton
    Friend WithEvents tblbtn_Close_32 As System.Windows.Forms.ToolStripButton
    Friend WithEvents tblbtn_Temperature_32 As System.Windows.Forms.ToolStripButton
    Friend WithEvents tblbtn_BPSitting_32 As System.Windows.Forms.ToolStripButton
    Friend WithEvents tblbtn_BPStanding_32 As System.Windows.Forms.ToolStripButton
    Friend WithEvents chtPatientGraphs As AxMSChart20Lib.AxMSChart

    'Removed PegausImageXpress7/twain4 -> Dhruv
    'Friend WithEvents PicImag_Graph As PegasusImaging.WinForms.ImagXpress7.PICImagXpress
    ''--
    Friend WithEvents tblbtn_Print_32 As System.Windows.Forms.ToolStripButton
    Private WithEvents lbl_BottomBrd As System.Windows.Forms.Label
    Private WithEvents lbl_LeftBrd As System.Windows.Forms.Label
    Private WithEvents lbl_RightBrd As System.Windows.Forms.Label
    Private WithEvents lbl_TopBrd As System.Windows.Forms.Label
    Friend WithEvents PrintPro1 As PegasusImaging.WinForms.PrintPro4.PrintPro
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Private WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents Label3 As System.Windows.Forms.Label
    Private WithEvents Label4 As System.Windows.Forms.Label
    Private WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents tblbtn_DtVsWeight_32 As System.Windows.Forms.ToolStripButton
#End Region

    Private _IsageGreater As Boolean = False
    Dim Pcte As System.Drawing.Image = Nothing
    Private _VisitID As Long
    Dim ds As New DataSet
    Dim dt As DataTable
    Private _VisitDate As Date
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Dim oclsViewGraphs As clsViewGraphs
    Dim _PatientID As Long
    Private strPatientCode As String
    Private strPatientFirstName As String
    Private strPatientMiddleName As String
    Private strPatientLastName As String
    Private strPatientDOB As String
    Private strPatientAge As String
    Private strPatientGender As String
    Private strPatientMaritalStatus As String
    Enum GraphType
        Height = 1
        Weight = 2
        HeadCircumfarance = 3
        Stature = 4
    End Enum

    Enum Gender
        Male = 1
        Female = 2
        Other = 3
    End Enum


#Region " Windows Form Designer generated code "

    Public Sub New(ByVal IsageGreate As Boolean, ByVal PatientID As Long)
        MyBase.New()
        _IsageGreater = IsageGreate
        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        PrintPro1.Licensing.UnlockRuntime(2052484428, 498072154, 1464682479, 5619)
        _PatientID = PatientID
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
    Friend WithEvents pnlMain As System.Windows.Forms.Panel
    Friend WithEvents DataGrid1 As System.Windows.Forms.DataGrid
    Friend WithEvents AxMSChart1 As AxMSChart20Lib.AxMSChart
    Friend WithEvents pnlTop As System.Windows.Forms.Panel
    Friend WithEvents lblPatientName As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblPatientCode As System.Windows.Forms.Label
    Friend WithEvents lblPatient As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmShowGraphs))
        Me.pnlMain = New System.Windows.Forms.Panel()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.chtPatientGraphs = New AxMSChart20Lib.AxMSChart()
        Me.lbl_RightBrd = New System.Windows.Forms.Label()
        Me.lbl_LeftBrd = New System.Windows.Forms.Label()
        Me.lbl_TopBrd = New System.Windows.Forms.Label()
        Me.lbl_BottomBrd = New System.Windows.Forms.Label()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.pnlTop = New System.Windows.Forms.Panel()
        Me.lblPatientName = New System.Windows.Forms.Label()
        Me.lblPatient = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.lblPatientCode = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.AxMSChart1 = New AxMSChart20Lib.AxMSChart()
        Me.DataGrid1 = New System.Windows.Forms.DataGrid()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.tblStrip_32 = New gloGlobal.gloToolStripIgnoreFocus()
        Me.tblbtn_AgeStature_32 = New System.Windows.Forms.ToolStripButton()
        Me.tblbtn_AgeWt24_32 = New System.Windows.Forms.ToolStripButton()
        Me.tblbtn_WtHt_32 = New System.Windows.Forms.ToolStripButton()
        Me.tblbtn_AgeCircum_32 = New System.Windows.Forms.ToolStripButton()
        Me.tblbtn_AgeHt_32 = New System.Windows.Forms.ToolStripButton()
        Me.tblbtn_AgeWt_32 = New System.Windows.Forms.ToolStripButton()
        Me.tblbtn_Temperature_32 = New System.Windows.Forms.ToolStripButton()
        Me.tblbtn_BPSitting_32 = New System.Windows.Forms.ToolStripButton()
        Me.tblbtn_BPStanding_32 = New System.Windows.Forms.ToolStripButton()
        Me.tblbtn_DtVsWeight_32 = New System.Windows.Forms.ToolStripButton()
        Me.tblbtn_Print_32 = New System.Windows.Forms.ToolStripButton()
        Me.tblbtn_Close_32 = New System.Windows.Forms.ToolStripButton()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.ToolTip2 = New System.Windows.Forms.ToolTip(Me.components)
        Me.PrintPro1 = New PegasusImaging.WinForms.PrintPro4.PrintPro(Me.components)
        Me.pnlMain.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.chtPatientGraphs, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel4.SuspendLayout()
        Me.pnlTop.SuspendLayout()
        CType(Me.AxMSChart1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGrid1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.tblStrip_32.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlMain
        '
        Me.pnlMain.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlMain.Controls.Add(Me.Panel5)
        Me.pnlMain.Controls.Add(Me.Panel4)
        Me.pnlMain.Controls.Add(Me.AxMSChart1)
        Me.pnlMain.Controls.Add(Me.DataGrid1)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlMain.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.pnlMain.Location = New System.Drawing.Point(0, 56)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(858, 507)
        Me.pnlMain.TabIndex = 2
        '
        'Panel5
        '
        Me.Panel5.Controls.Add(Me.Panel3)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel5.Location = New System.Drawing.Point(0, 30)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.Panel5.Size = New System.Drawing.Size(858, 477)
        Me.Panel5.TabIndex = 16
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.White
        Me.Panel3.Controls.Add(Me.chtPatientGraphs)
        Me.Panel3.Controls.Add(Me.lbl_RightBrd)
        Me.Panel3.Controls.Add(Me.lbl_LeftBrd)
        Me.Panel3.Controls.Add(Me.lbl_TopBrd)
        Me.Panel3.Controls.Add(Me.lbl_BottomBrd)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel3.Location = New System.Drawing.Point(3, 0)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(852, 474)
        Me.Panel3.TabIndex = 16
        '
        'chtPatientGraphs
        '
        Me.chtPatientGraphs.DataSource = Nothing
        Me.chtPatientGraphs.Dock = System.Windows.Forms.DockStyle.Fill
        Me.chtPatientGraphs.Location = New System.Drawing.Point(1, 1)
        Me.chtPatientGraphs.Name = "chtPatientGraphs"
        Me.chtPatientGraphs.OcxState = CType(resources.GetObject("chtPatientGraphs.OcxState"), System.Windows.Forms.AxHost.State)
        Me.chtPatientGraphs.Size = New System.Drawing.Size(850, 472)
        Me.chtPatientGraphs.TabIndex = 11
        '
        'lbl_RightBrd
        '
        Me.lbl_RightBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_RightBrd.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_RightBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_RightBrd.Location = New System.Drawing.Point(851, 1)
        Me.lbl_RightBrd.Name = "lbl_RightBrd"
        Me.lbl_RightBrd.Size = New System.Drawing.Size(1, 472)
        Me.lbl_RightBrd.TabIndex = 13
        Me.lbl_RightBrd.Text = "label3"
        '
        'lbl_LeftBrd
        '
        Me.lbl_LeftBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_LeftBrd.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_LeftBrd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_LeftBrd.Location = New System.Drawing.Point(0, 1)
        Me.lbl_LeftBrd.Name = "lbl_LeftBrd"
        Me.lbl_LeftBrd.Size = New System.Drawing.Size(1, 472)
        Me.lbl_LeftBrd.TabIndex = 14
        Me.lbl_LeftBrd.Text = "label4"
        '
        'lbl_TopBrd
        '
        Me.lbl_TopBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_TopBrd.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_TopBrd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_TopBrd.Location = New System.Drawing.Point(0, 0)
        Me.lbl_TopBrd.Name = "lbl_TopBrd"
        Me.lbl_TopBrd.Size = New System.Drawing.Size(852, 1)
        Me.lbl_TopBrd.TabIndex = 12
        Me.lbl_TopBrd.Text = "label1"
        '
        'lbl_BottomBrd
        '
        Me.lbl_BottomBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_BottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_BottomBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_BottomBrd.Location = New System.Drawing.Point(0, 473)
        Me.lbl_BottomBrd.Name = "lbl_BottomBrd"
        Me.lbl_BottomBrd.Size = New System.Drawing.Size(852, 1)
        Me.lbl_BottomBrd.TabIndex = 15
        Me.lbl_BottomBrd.Text = "label2"
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.pnlTop)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel4.Location = New System.Drawing.Point(0, 0)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Padding = New System.Windows.Forms.Padding(3)
        Me.Panel4.Size = New System.Drawing.Size(858, 30)
        Me.Panel4.TabIndex = 16
        '
        'pnlTop
        '
        Me.pnlTop.BackColor = System.Drawing.Color.Transparent
        Me.pnlTop.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.pnlTop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlTop.Controls.Add(Me.lblPatientName)
        Me.pnlTop.Controls.Add(Me.lblPatient)
        Me.pnlTop.Controls.Add(Me.Label6)
        Me.pnlTop.Controls.Add(Me.lblPatientCode)
        Me.pnlTop.Controls.Add(Me.Label1)
        Me.pnlTop.Controls.Add(Me.Label5)
        Me.pnlTop.Controls.Add(Me.Label2)
        Me.pnlTop.Controls.Add(Me.Label3)
        Me.pnlTop.Controls.Add(Me.Label4)
        Me.pnlTop.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlTop.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlTop.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.pnlTop.Location = New System.Drawing.Point(3, 3)
        Me.pnlTop.Name = "pnlTop"
        Me.pnlTop.Size = New System.Drawing.Size(852, 24)
        Me.pnlTop.TabIndex = 4
        '
        'lblPatientName
        '
        Me.lblPatientName.AutoSize = True
        Me.lblPatientName.BackColor = System.Drawing.Color.Transparent
        Me.lblPatientName.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblPatientName.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPatientName.Location = New System.Drawing.Point(279, 1)
        Me.lblPatientName.Name = "lblPatientName"
        Me.lblPatientName.Padding = New System.Windows.Forms.Padding(2, 4, 2, 2)
        Me.lblPatientName.Size = New System.Drawing.Size(75, 20)
        Me.lblPatientName.TabIndex = 30
        Me.lblPatientName.Text = "Mike Dodge"
        Me.lblPatientName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblPatientName.Visible = False
        '
        'lblPatient
        '
        Me.lblPatient.AutoSize = True
        Me.lblPatient.BackColor = System.Drawing.Color.Transparent
        Me.lblPatient.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblPatient.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPatient.Location = New System.Drawing.Point(178, 1)
        Me.lblPatient.Name = "lblPatient"
        Me.lblPatient.Padding = New System.Windows.Forms.Padding(2, 4, 2, 2)
        Me.lblPatient.Size = New System.Drawing.Size(101, 20)
        Me.lblPatient.TabIndex = 27
        Me.lblPatient.Text = "Patient Name :"
        Me.lblPatient.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblPatient.Visible = False
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label6.Location = New System.Drawing.Point(139, 1)
        Me.Label6.Name = "Label6"
        Me.Label6.Padding = New System.Windows.Forms.Padding(2, 4, 2, 2)
        Me.Label6.Size = New System.Drawing.Size(39, 22)
        Me.Label6.TabIndex = 35
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label6.Visible = False
        '
        'lblPatientCode
        '
        Me.lblPatientCode.AutoSize = True
        Me.lblPatientCode.BackColor = System.Drawing.Color.Transparent
        Me.lblPatientCode.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblPatientCode.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPatientCode.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblPatientCode.Location = New System.Drawing.Point(100, 1)
        Me.lblPatientCode.Name = "lblPatientCode"
        Me.lblPatientCode.Padding = New System.Windows.Forms.Padding(2, 4, 2, 2)
        Me.lblPatientCode.Size = New System.Drawing.Size(39, 20)
        Me.lblPatientCode.TabIndex = 28
        Me.lblPatientCode.Text = "1001"
        Me.lblPatientCode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblPatientCode.Visible = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(1, 1)
        Me.Label1.Name = "Label1"
        Me.Label1.Padding = New System.Windows.Forms.Padding(2, 4, 2, 2)
        Me.Label1.Size = New System.Drawing.Size(99, 20)
        Me.Label1.TabIndex = 29
        Me.Label1.Text = "Patient Code :"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Label1.Visible = False
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label5.Location = New System.Drawing.Point(1, 23)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(850, 1)
        Me.Label5.TabIndex = 34
        Me.Label5.Text = "label2"
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label2.Location = New System.Drawing.Point(851, 1)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(1, 23)
        Me.Label2.TabIndex = 32
        Me.Label2.Text = "label3"
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(0, 1)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(1, 23)
        Me.Label3.TabIndex = 33
        Me.Label3.Text = "label4"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(0, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(852, 1)
        Me.Label4.TabIndex = 31
        Me.Label4.Text = "label1"
        '
        'AxMSChart1
        '
        Me.AxMSChart1.DataSource = Nothing
        Me.AxMSChart1.Location = New System.Drawing.Point(235, 162)
        Me.AxMSChart1.Name = "AxMSChart1"
        Me.AxMSChart1.OcxState = CType(resources.GetObject("AxMSChart1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxMSChart1.Size = New System.Drawing.Size(448, 115)
        Me.AxMSChart1.TabIndex = 0
        '
        'DataGrid1
        '
        Me.DataGrid1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.DataGrid1.DataMember = ""
        Me.DataGrid1.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.DataGrid1.Location = New System.Drawing.Point(120, 502)
        Me.DataGrid1.Name = "DataGrid1"
        Me.DataGrid1.Size = New System.Drawing.Size(560, 62)
        Me.DataGrid1.TabIndex = 1
        Me.DataGrid1.Visible = False
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Panel1.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.Panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel1.Controls.Add(Me.tblStrip_32)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(858, 56)
        Me.Panel1.TabIndex = 9
        '
        'tblStrip_32
        '
        Me.tblStrip_32.BackColor = System.Drawing.Color.Transparent
        Me.tblStrip_32.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tblStrip_32.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblStrip_32.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tblStrip_32.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tblbtn_AgeStature_32, Me.tblbtn_AgeWt24_32, Me.tblbtn_WtHt_32, Me.tblbtn_AgeCircum_32, Me.tblbtn_AgeHt_32, Me.tblbtn_AgeWt_32, Me.tblbtn_Temperature_32, Me.tblbtn_BPSitting_32, Me.tblbtn_BPStanding_32, Me.tblbtn_DtVsWeight_32, Me.tblbtn_Print_32, Me.tblbtn_Close_32})
        Me.tblStrip_32.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.tblStrip_32.Location = New System.Drawing.Point(0, 0)
        Me.tblStrip_32.Name = "tblStrip_32"
        Me.tblStrip_32.Size = New System.Drawing.Size(858, 106)
        Me.tblStrip_32.TabIndex = 0
        Me.tblStrip_32.Text = "ToolStrip1"
        '
        'tblbtn_AgeStature_32
        '
        Me.tblbtn_AgeStature_32.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblbtn_AgeStature_32.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tblbtn_AgeStature_32.Image = CType(resources.GetObject("tblbtn_AgeStature_32.Image"), System.Drawing.Image)
        Me.tblbtn_AgeStature_32.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblbtn_AgeStature_32.Name = "tblbtn_AgeStature_32"
        Me.tblbtn_AgeStature_32.Size = New System.Drawing.Size(104, 50)
        Me.tblbtn_AgeStature_32.Tag = "AgeStature"
        Me.tblbtn_AgeStature_32.Text = "&Age Vs Stature"
        Me.tblbtn_AgeStature_32.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tblbtn_AgeWt24_32
        '
        Me.tblbtn_AgeWt24_32.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblbtn_AgeWt24_32.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tblbtn_AgeWt24_32.Image = CType(resources.GetObject("tblbtn_AgeWt24_32.Image"), System.Drawing.Image)
        Me.tblbtn_AgeWt24_32.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblbtn_AgeWt24_32.Name = "tblbtn_AgeWt24_32"
        Me.tblbtn_AgeWt24_32.Size = New System.Drawing.Size(119, 50)
        Me.tblbtn_AgeWt24_32.Tag = "AgeWeight24"
        Me.tblbtn_AgeWt24_32.Text = "Age Vs Wt &2+ yrs"
        Me.tblbtn_AgeWt24_32.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tblbtn_AgeWt24_32.ToolTipText = "Age Vs Weight above 2 years"
        '
        'tblbtn_WtHt_32
        '
        Me.tblbtn_WtHt_32.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblbtn_WtHt_32.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tblbtn_WtHt_32.Image = CType(resources.GetObject("tblbtn_WtHt_32.Image"), System.Drawing.Image)
        Me.tblbtn_WtHt_32.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblbtn_WtHt_32.Name = "tblbtn_WtHt_32"
        Me.tblbtn_WtHt_32.Size = New System.Drawing.Size(118, 50)
        Me.tblbtn_WtHt_32.Tag = "WeightHeight"
        Me.tblbtn_WtHt_32.Text = "Weight Vs &Height"
        Me.tblbtn_WtHt_32.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tblbtn_AgeCircum_32
        '
        Me.tblbtn_AgeCircum_32.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblbtn_AgeCircum_32.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tblbtn_AgeCircum_32.Image = CType(resources.GetObject("tblbtn_AgeCircum_32.Image"), System.Drawing.Image)
        Me.tblbtn_AgeCircum_32.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblbtn_AgeCircum_32.Name = "tblbtn_AgeCircum_32"
        Me.tblbtn_AgeCircum_32.Size = New System.Drawing.Size(103, 50)
        Me.tblbtn_AgeCircum_32.Tag = "AgeCircumfranse"
        Me.tblbtn_AgeCircum_32.Text = "Age Vs &Circumf"
        Me.tblbtn_AgeCircum_32.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tblbtn_AgeCircum_32.ToolTipText = "Age Vs Circumference"
        '
        'tblbtn_AgeHt_32
        '
        Me.tblbtn_AgeHt_32.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblbtn_AgeHt_32.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tblbtn_AgeHt_32.Image = CType(resources.GetObject("tblbtn_AgeHt_32.Image"), System.Drawing.Image)
        Me.tblbtn_AgeHt_32.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblbtn_AgeHt_32.Name = "tblbtn_AgeHt_32"
        Me.tblbtn_AgeHt_32.Size = New System.Drawing.Size(98, 50)
        Me.tblbtn_AgeHt_32.Tag = "AgeHeight"
        Me.tblbtn_AgeHt_32.Text = "Age &Vs Height"
        Me.tblbtn_AgeHt_32.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tblbtn_AgeWt_32
        '
        Me.tblbtn_AgeWt_32.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblbtn_AgeWt_32.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tblbtn_AgeWt_32.Image = CType(resources.GetObject("tblbtn_AgeWt_32.Image"), System.Drawing.Image)
        Me.tblbtn_AgeWt_32.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblbtn_AgeWt_32.Name = "tblbtn_AgeWt_32"
        Me.tblbtn_AgeWt_32.Size = New System.Drawing.Size(101, 50)
        Me.tblbtn_AgeWt_32.Tag = "AgeWeight"
        Me.tblbtn_AgeWt_32.Text = "Age Vs &Weight"
        Me.tblbtn_AgeWt_32.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tblbtn_Temperature_32
        '
        Me.tblbtn_Temperature_32.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblbtn_Temperature_32.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tblbtn_Temperature_32.Image = CType(resources.GetObject("tblbtn_Temperature_32.Image"), System.Drawing.Image)
        Me.tblbtn_Temperature_32.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblbtn_Temperature_32.Name = "tblbtn_Temperature_32"
        Me.tblbtn_Temperature_32.Size = New System.Drawing.Size(86, 50)
        Me.tblbtn_Temperature_32.Text = "Dt Vs &Tempr"
        Me.tblbtn_Temperature_32.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tblbtn_Temperature_32.ToolTipText = "Date Vs Temprature"
        '
        'tblbtn_BPSitting_32
        '
        Me.tblbtn_BPSitting_32.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblbtn_BPSitting_32.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tblbtn_BPSitting_32.Image = CType(resources.GetObject("tblbtn_BPSitting_32.Image"), System.Drawing.Image)
        Me.tblbtn_BPSitting_32.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblbtn_BPSitting_32.Name = "tblbtn_BPSitting_32"
        Me.tblbtn_BPSitting_32.Size = New System.Drawing.Size(106, 50)
        Me.tblbtn_BPSitting_32.Text = "Dt Vs &BPSitting"
        Me.tblbtn_BPSitting_32.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tblbtn_BPSitting_32.ToolTipText = "Date Vs Blood Pressure Sitting"
        '
        'tblbtn_BPStanding_32
        '
        Me.tblbtn_BPStanding_32.BackColor = System.Drawing.Color.Transparent
        Me.tblbtn_BPStanding_32.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tblbtn_BPStanding_32.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tblbtn_BPStanding_32.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblbtn_BPStanding_32.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tblbtn_BPStanding_32.Image = CType(resources.GetObject("tblbtn_BPStanding_32.Image"), System.Drawing.Image)
        Me.tblbtn_BPStanding_32.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblbtn_BPStanding_32.Name = "tblbtn_BPStanding_32"
        Me.tblbtn_BPStanding_32.Size = New System.Drawing.Size(120, 50)
        Me.tblbtn_BPStanding_32.Text = "Dt Vs BP&Standing"
        Me.tblbtn_BPStanding_32.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tblbtn_BPStanding_32.ToolTipText = "Date Vs Blood Pressure Standing"
        '
        'tblbtn_DtVsWeight_32
        '
        Me.tblbtn_DtVsWeight_32.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblbtn_DtVsWeight_32.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tblbtn_DtVsWeight_32.Image = CType(resources.GetObject("tblbtn_DtVsWeight_32.Image"), System.Drawing.Image)
        Me.tblbtn_DtVsWeight_32.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblbtn_DtVsWeight_32.Name = "tblbtn_DtVsWeight_32"
        Me.tblbtn_DtVsWeight_32.Size = New System.Drawing.Size(92, 50)
        Me.tblbtn_DtVsWeight_32.Text = "Dt Vs Wei&ght"
        Me.tblbtn_DtVsWeight_32.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tblbtn_DtVsWeight_32.ToolTipText = "Date Vs Weight"
        '
        'tblbtn_Print_32
        '
        Me.tblbtn_Print_32.BackColor = System.Drawing.Color.Transparent
        Me.tblbtn_Print_32.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tblbtn_Print_32.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblbtn_Print_32.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tblbtn_Print_32.Image = CType(resources.GetObject("tblbtn_Print_32.Image"), System.Drawing.Image)
        Me.tblbtn_Print_32.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblbtn_Print_32.Name = "tblbtn_Print_32"
        Me.tblbtn_Print_32.Size = New System.Drawing.Size(41, 50)
        Me.tblbtn_Print_32.Tag = "Print"
        Me.tblbtn_Print_32.Text = "&Print"
        Me.tblbtn_Print_32.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tblbtn_Print_32.ToolTipText = "Print  "
        '
        'tblbtn_Close_32
        '
        Me.tblbtn_Close_32.BackColor = System.Drawing.Color.Transparent
        Me.tblbtn_Close_32.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tblbtn_Close_32.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
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
        'Panel2
        '
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Panel2.Location = New System.Drawing.Point(0, 563)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(858, 1)
        Me.Panel2.TabIndex = 8
        '
        'frmShowGraphs
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(7, 15)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(858, 564)
        Me.Controls.Add(Me.pnlMain)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Panel2)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmShowGraphs"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Show Graph"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.pnlMain.ResumeLayout(False)
        Me.Panel5.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        CType(Me.chtPatientGraphs, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel4.ResumeLayout(False)
        Me.pnlTop.ResumeLayout(False)
        Me.pnlTop.PerformLayout()
        CType(Me.AxMSChart1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGrid1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.tblStrip_32.ResumeLayout(False)
        Me.tblStrip_32.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub btnGraphAgeHt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'lblMin.Text = "Minimum Height"
        'lblMax.Text = "Maximum Height"
        'lblPatients.Text = "Patient Height"

        oclsViewGraphs = New clsViewGraphs
        dt = New DataTable
        dt = oclsViewGraphs.ScanAgeHtWt(_PatientID)

        ' get patient Gender
        Dim nGender As New Integer

        nGender = GetPatientGender(strPatientGender)

        Dim dt_MINMAX As New DataTable

        'get STANDARD values for the minmax values from SP 'gsp_viewGraphMinMax' where standard data is stored.
        If nGender = 1 Then ' for male
            dt_MINMAX = oclsViewGraphs.GetMinMaxValues(Gender.Male, GraphType.Height)
        ElseIf nGender = 2 Then ' for female
            dt_MINMAX = oclsViewGraphs.GetMinMaxValues(Gender.Female, GraphType.Height)
        Else ' for other
            dt_MINMAX = oclsViewGraphs.GetMinMaxValues(Gender.Male, GraphType.Height)
        End If

        'view data in Datagrid
        DataGrid1.DataSource = dt_MINMAX

        Dim i As Integer '= 20
        ' string data where patient Age collection when vital record enterd.
        ''Dim AgeCollection(dt.Rows.Count - 1, 2) As String
        Dim AgeCollection(dt_MINMAX.Rows.Count - 1, 2) As String

        ' string for the data where hight of the patient is collected.
        Dim arrsales(dt_MINMAX.Rows.Count - 1, 12) As String

        Dim arrGraph(dt_MINMAX.Rows.Count - 1, 3) As String

        Dim strPatientInfo As String = Space(5) & " Patient Code:" & Space(1) & gloUC_PatientStrip1.PatientCode & Space(5) & "Patient Name:" & Space(1) & gloUC_PatientStrip1.PatientName & Space(5) & "DOB:" & Space(1) & gloUC_PatientStrip1.PatientDateOfBirth & Space(5) & "Phone No.:" & Space(1) & gloUC_PatientStrip1.PatientPhone & Space(5)
        chtPatientGraphs.Footnote.Location.LocationType = MSChart20Lib.VtChLocationType.VtChLocationTypeTop
        chtPatientGraphs.Footnote.Text = strPatientInfo

        chtPatientGraphs.TitleText = "Age Vs Height"

        'chtPatientGraphs.Backdrop.Fill.Brush.PatternColor.Automatic()

        Try
            With chtPatientGraphs
                .chartType = MSChart20Lib.VtChChartType.VtChChartType2dCombination
                '.chartType = MSChart20Lib.VtChChartType.VtChChartType2dLine
                If dt_MINMAX.Rows.Count >= 1 Then

                    DataGrid1.DataSource = Nothing
                    DataGrid1.DataSource = dt
                    'Axis Labels for the Graph
                    chtPatientGraphs.Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdX).AxisTitle.Text = "Age (Months)"
                    chtPatientGraphs.Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).AxisTitle.Text = "Height (Inch)"

                    ' get the total number of the vital entries for the patients
                    For i = 1 To dt_MINMAX.Rows.Count - 1  'dt.Rows.Count - 1
                        ' get gender and assign standard Height to the array string
                        arrsales(i, 1) = dt_MINMAX.Rows(i)("Agemos")
                        arrsales(i, 11) = dt_MINMAX.Rows(i)("P3") * 0.394
                        arrsales(i, 3) = dt_MINMAX.Rows(i)("P5") * 0.394
                        arrsales(i, 4) = dt_MINMAX.Rows(i)("P10") * 0.394
                        arrsales(i, 5) = dt_MINMAX.Rows(i)("P25") * 0.394
                        arrsales(i, 6) = dt_MINMAX.Rows(i)("P50") * 0.394
                        arrsales(i, 7) = dt_MINMAX.Rows(i)("P75") * 0.394
                        arrsales(i, 8) = dt_MINMAX.Rows(i)("P90") * 0.394
                        arrsales(i, 9) = dt_MINMAX.Rows(i)("P95") * 0.394
                        arrsales(i, 10) = dt_MINMAX.Rows(i)("P97") * 0.394

                        AgeCollection(i, 1) = dt_MINMAX.Rows(i)("Agemos")
                    Next
                Else
                    MessageBox.Show("No vital data available for this patient.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If

                'Fill For PAtient
                Dim Count As Integer
                Count = arrsales.Length / arrsales.GetLength(1)

                For i = 0 To Count - 1
                    For j As Integer = 0 To dt.Rows.Count - 1
                        If arrsales(i, 1) = dt.Rows(j)("AGE") Then
                            '' If Age Matches then Add Patients Ht in to The Array
                            Dim ft As Decimal
                            Dim Inch As Decimal
                            Dim temp() As String
                            ' data convert from ftInch to in points i.e. 1 ft 6 Inch is 1.5 Ft
                            If Not IsDBNull(dt.Rows(j)("sHeight")) Then
                                temp = GetFtInch(dt.Rows(j)("sHeight"))
                            Else
                                temp = GetFtInch(0)
                            End If

                            '''' Only Inch is available then
                            If temp(0).Trim <> "" Then
                                ft = Convert.ToDecimal(CType(temp(0), Object))
                            Else
                                ft = 0
                            End If
                            If temp.Length > 1 Then
                                If temp(1).Trim <> "" Then
                                    Inch = Convert.ToDecimal(CType(temp(1), Object))
                                Else
                                    Inch = 0
                                End If
                            End If
                            '''' If Age Matches then Add Patients Ht in to The Array 
                            arrsales(i, 2) = FtToMtr(ft, Inch) ' dt.Rows(j)("sHeight") '
                            Exit For
                        End If
                    Next
                    arrsales(i, 1) = Nothing
                Next

                'set data to draw chart
                .ChartData = CType(arrsales, Object)

                ' set graph styles for the patient vital entries.
                .Plot.SeriesCollection(3).SeriesType = MSChart20Lib.VtChSeriesType.VtChSeriesType2dLine
                .Plot.SeriesCollection(3).Select()
                '.Plot.SeriesCollection(3).SeriesMarker.Show = True
                .Plot.SeriesCollection(3).ShowLine = False
                .Plot.SeriesCollection(3).DataPoints(-1).Marker.Style = MSChart20Lib.VtMarkerStyle.VtMarkerStyleDiamond
                .Plot.SeriesCollection(3).DataPoints(-1).Marker.Visible = True
                .Plot.SeriesCollection(3).DataPoints(-1).Marker.FillColor.Set(255, 0, 0)
                '.Plot.SeriesCollection(3).DataPoints(-1).Marker.Pen.Style = MSChart20Lib.VtPenStyle.VtPenStyleDashDot
                .Plot.SeriesCollection(3).DataPoints(-1).Brush.Style = MSChart20Lib.VtBrushStyle.VtBrushStyleSolid
                .Plot.SeriesCollection(3).DataPoints(-1).Marker.Size = 20
                .Plot.SeriesCollection(3).Pen.Width = 20
                .Plot.SeriesCollection(3).DataPoints(-1).Brush.FillColor.Set(255, 0, 0)

                ' set the display properties of the graphs curve.
                Dim cnt As Integer
                For cnt = 1 To 12
                    If cnt <> 3 Then
                        .Plot.SeriesCollection(cnt).SeriesType = MSChart20Lib.VtChSeriesType.VtChSeriesType2dLine
                        .Plot.SeriesCollection(cnt).Pen.Width = 22
                        .Plot.SeriesCollection(cnt).Pen.VtColor.Set(0, 0, 0)
                    End If
                Next

                ' set the graph properties for the y axis.
                .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).ValueScale.Minimum = 18
                .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).ValueScale.Maximum = 42
                .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).ValueScale.MajorDivision = 10
                .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).ValueScale.MinorDivision = 1
                .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).CategoryScale.DivisionsPerLabel = 1
                .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).CategoryScale.DivisionsPerTick = 1

                ' set the properties for the x axis.
                .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdX).ValueScale.MajorDivision = 10
                .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdX).ValueScale.MinorDivision = 10

                'set the label of x axis.
                For i = 1 To .RowCount
                    .Row = i
                    .RowLabel = AgeCollection(i - 1, 1)
                Next

            End With
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub btnGraphAgeWt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'lblMin.Text = "Minimum Weight"
        'lblMax.Text = "Maximum Weight"
        'lblPatients.Text = "Patient Weight"

        Dim oclsViewGraphs As New clsViewGraphs
        dt = New DataTable
        dt = oclsViewGraphs.ScanAgeHtWt(_PatientID)

        ' get patient Gender
        Dim nGender As New Integer

        nGender = GetPatientGender(strPatientGender)
        Dim dt_MINMAX As New DataTable

        'get STANDARD values for the minmax values from SP 'gsp_viewGraphMinMax' where standard data is stored.
        If nGender = 1 Then ' for male
            dt_MINMAX = oclsViewGraphs.GetMinMaxValues(Gender.Male, GraphType.Weight)
        ElseIf nGender = 2 Then ' for female
            dt_MINMAX = oclsViewGraphs.GetMinMaxValues(Gender.Female, GraphType.Weight)
        Else ' for other
            dt_MINMAX = oclsViewGraphs.GetMinMaxValues(Gender.Male, GraphType.Weight)
        End If

        'view data in Datagrid
        DataGrid1.DataSource = dt_MINMAX

        Dim i As Integer '= 20
        ' string data where patient Age collection when vital record enterd.
        ''Dim AgeCollection(dt.Rows.Count - 1, 2) As String
        Dim AgeCollection(dt_MINMAX.Rows.Count - 1, 2) As String

        ' string for the data where hight of the patient is collected.
        Dim arrsales(dt_MINMAX.Rows.Count - 1, 12) As String

        Dim arrGraph(dt_MINMAX.Rows.Count - 1, 3) As String

        Dim strPatientInfo As String = Space(5) & " Patient Code:" & Space(1) & gloUC_PatientStrip1.PatientCode & Space(5) & "Patient Name:" & Space(1) & gloUC_PatientStrip1.PatientName & Space(5) & "DOB:" & Space(1) & gloUC_PatientStrip1.PatientDateOfBirth & Space(5) & "Phone No.:" & Space(1) & gloUC_PatientStrip1.PatientPhone & Space(5)
        chtPatientGraphs.Footnote.Location.LocationType = MSChart20Lib.VtChLocationType.VtChLocationTypeTop
        chtPatientGraphs.Footnote.Text = strPatientInfo

        chtPatientGraphs.TitleText = "Age Vs Weight"

        Try
            With chtPatientGraphs
                .chartType = MSChart20Lib.VtChChartType.VtChChartType2dCombination
                '.chartType = MSChart20Lib.VtChChartType.VtChChartType2dLine
                If dt_MINMAX.Rows.Count >= 1 Then
                    DataGrid1.DataSource = Nothing
                    DataGrid1.DataSource = dt
                    'Axis Labels for the Graph
                    chtPatientGraphs.Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdX).AxisTitle.Text = "Age (Months)"
                    chtPatientGraphs.Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).AxisTitle.Text = "Weight (lbs)"

                    ' get the total number of the vital entries for the patients
                    '''' Commented By Bipin ' 20070330
                    'For i = 1 To 36 ' dt_MINMAX.Rows.Count - 1  'dt.Rows.Count - 1
                    For i = 1 To 37 ' dt_MINMAX.Rows.Count - 1  'dt.Rows.Count - 1
                        ' get gender and assign standard Height to the array string
                        arrsales(i, 1) = dt_MINMAX.Rows(i)("Agemos")
                        arrsales(i, 11) = dt_MINMAX.Rows(i)("P3") / (0.45)
                        arrsales(i, 3) = dt_MINMAX.Rows(i)("P5") / (0.45)
                        arrsales(i, 4) = dt_MINMAX.Rows(i)("P10") / (0.45)
                        arrsales(i, 5) = dt_MINMAX.Rows(i)("P25") / (0.45)
                        arrsales(i, 6) = dt_MINMAX.Rows(i)("P50") / (0.45)
                        arrsales(i, 7) = dt_MINMAX.Rows(i)("P75") / (0.45)
                        arrsales(i, 8) = dt_MINMAX.Rows(i)("P90") / (0.45)
                        arrsales(i, 9) = dt_MINMAX.Rows(i)("P95") / (0.45)
                        arrsales(i, 10) = dt_MINMAX.Rows(i)("P97") / (0.45)

                        AgeCollection(i, 1) = dt_MINMAX.Rows(i)("Agemos")
                    Next
                Else
                    MessageBox.Show("No vital data available for this patient.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If

                'Fill For PAtient
                Dim Count As Integer
                Count = arrsales.Length / arrsales.GetLength(1)

                For i = 0 To Count - 1
                    For j As Integer = 0 To dt.Rows.Count - 1
                        If arrsales(i, 1) = dt.Rows(j)("AGE") Then
                            '''' If Age Matches then Add Patients Ht in to The Array 
                            If Not IsDBNull(dt.Rows(j)("dWeightinlbs")) Then
                                arrsales(i, 2) = dt.Rows(j)("dWeightinlbs") '/ (0.45) 'FtToMtr(ft, Inch)
                            Else
                                arrsales(i, 2) = 0 '/ (0.45) 'FtToMtr(ft, Inch)
                            End If

                            Exit For
                        End If
                    Next
                    arrsales(i, 1) = Nothing
                Next

                'set data to draw chart
                .ChartData = CType(arrsales, Object)
                ' set graph styles   
                '.Plot.Backdrop.Fill.Brush.PatternColor.Set(255, 255, 255)               

                Dim cnt As Integer
                For cnt = 1 To 12
                    If cnt <> 3 Then
                        .Plot.SeriesCollection(cnt).SeriesType = MSChart20Lib.VtChSeriesType.VtChSeriesType2dLine
                        .Plot.SeriesCollection(cnt).Pen.Width = 22
                        .Plot.SeriesCollection(cnt).Pen.VtColor.Set(0, 0, 0)
                    End If
                Next

                .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).ValueScale.Minimum = 1
                .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).ValueScale.Maximum = 40
                .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).ValueScale.MajorDivision = 10
                .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).ValueScale.MinorDivision = 1
                .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).CategoryScale.DivisionsPerLabel = 1
                .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).CategoryScale.DivisionsPerTick = 1

                .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdX).ValueScale.MajorDivision = 10
                .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdX).ValueScale.MinorDivision = 1
                '.Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdX).AxisTitle.VtFont.Size = 1


                .Plot.SeriesCollection(3).SeriesType = MSChart20Lib.VtChSeriesType.VtChSeriesType2dLine
                ' .Plot.SeriesCollection(3).SeriesMarker.Auto = True
                .Plot.SeriesCollection(3).Select()
                .Plot.SeriesCollection(3).SeriesMarker.Show = True
                .Plot.SeriesCollection(3).ShowLine = False
                .Plot.SeriesCollection(3).Pen.Style = MSChart20Lib.VtPenStyle.VtPenStyleDashDotDot
                .Plot.SeriesCollection(3).Pen.Width = 6

                For i = 1 To .RowCount
                    .Row = i
                    .RowLabel = AgeCollection(i - 1, 1)
                Next

            End With
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Function GetFtInch(ByVal strHeight As String) As Array
        'Dim arrHeight() As String
        strHeight = Mid(strHeight, 1, Len(strHeight) - 1)
        'arrHeight = 
        Return Split(strHeight, "'", , CompareMethod.Text)
        'Return arrHeight
    End Function

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Close()
    End Sub

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
            ''Added on 20100706 by sanjog to show patient control as vital graph
            .ShowDetail(_PatientID, gloUC_PatientStrip.enumFormName.VitalGraph)
            ''Added on 20100706 by sanjog to show patient control as vital graph
            '.pnlTranscationDate.Visible = False
            .SendToBack()
        End With
        Me.Controls.Add(gloUC_PatientStrip1)
        ''''
        chtPatientGraphs.BringToFront()
        '' Hide Previous Patient Details
        pnlMain.BringToFront()
        Panel1.SendToBack()
        Panel4.Visible = False
        ' ''
    End Sub

#End Region

    Private Function Get_PatientDetails()
        Dim dtPatient As DataTable = Nothing

        Try
            dtPatient = New DataTable
            dtPatient = GetPatientInfo(_PatientID)
            If IsNothing(dtPatient) = False Then
                If dtPatient.Rows.Count > 0 Then
                    strPatientCode = Convert.ToString(dtPatient.Rows(0)("sPatientCode"))
                    strPatientFirstName = Convert.ToString(dtPatient.Rows(0)("sFirstName"))
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

    Private Sub frmShowGraphs_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Call Get_PatientDetails()
            lblPatientCode.Text = strPatientCode
            lblPatientCode.Tag = _PatientID
            lblPatientName.Text = strPatientFirstName & " " & strPatientLastName
            '''' Set Patient Details
            Call Set_PatientDetailStrip()

            '''' Calculate Patient age in Months
            Dim nMonths As Integer
            nMonths = DateDiff(DateInterval.Month, CType(strPatientDOB, Date), Date.Now.Date)

            If nMonths <= 24 Then
                tblbtn_AgeStature_32.Visible = False
                tblbtn_AgeWt24_32.Visible = False
            End If

            'btnCircum_Click(btnCircum, e)
            'btnCircum.Select()
            If _IsageGreater Then
                '' 20090305 By Mahesh 
                '' If Patient's age is more than 20 years then show Date Vs Weight Graph as default 
                '' previosly it was Age Vs Temperature
                ' AgeVsTemprature() 

                DateVsWeight()
                ''
                tblbtn_AgeCircum_32.Visible = False
                tblbtn_AgeWt_32.Visible = False
                tblbtn_AgeHt_32.Visible = False
                tblbtn_WtHt_32.Visible = False
                tblbtn_AgeWt24_32.Visible = False
                tblbtn_AgeStature_32.Visible = False
            Else
                AgeHeight() 'AgeCircumfranse()
            End If
            Try
                gloPatient.gloPatient.GetWindowTitle(Me, _PatientID, GetConnectionString(), gstrMessageBoxCaption)
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            End Try
            Me.ShowIcon = True
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnCircum_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'lblMin.Text = "Minimum Circumfrance"
        'lblMax.Text = "Maximum Circumfrance"
        'lblPatients.Text = "Patient Circumference"

        Dim oclsViewGraphs As New clsViewGraphs
        dt = New DataTable
        dt = oclsViewGraphs.ScanAgeHtWt(_PatientID)

        ' get patient Gender
        Dim nGender As New Integer

        nGender = GetPatientGender(strPatientGender)

        Dim dt_MINMAX As New DataTable

        'get STANDARD values for the minmax values from SP 'gsp_viewGraphMinMax' where standard data is stored.
        If nGender = 1 Then ' for male
            dt_MINMAX = oclsViewGraphs.GetMinMaxValues(Gender.Male, GraphType.HeadCircumfarance)
        ElseIf nGender = 2 Then ' for female
            dt_MINMAX = oclsViewGraphs.GetMinMaxValues(Gender.Female, GraphType.HeadCircumfarance)
        Else ' for other
            dt_MINMAX = oclsViewGraphs.GetMinMaxValues(Gender.Male, GraphType.HeadCircumfarance)
        End If

        'view data in Datagrid
        DataGrid1.DataSource = dt_MINMAX

        Dim i As Integer '= 20
        ' string data where patient Age collection when vital record enterd.
        ''Dim AgeCollection(dt.Rows.Count - 1, 2) As String
        Dim AgeCollection(dt_MINMAX.Rows.Count - 1, 2) As String

        ' string for the data where hight of the patient is collected.
        Dim arrsales(dt_MINMAX.Rows.Count - 1, 12) As String

        Dim arrGraph(dt_MINMAX.Rows.Count - 1, 3) As String

        Dim strPatientInfo As String = Space(5) & " Patient Code:" & Space(1) & gloUC_PatientStrip1.PatientCode & Space(5) & "Patient Name:" & Space(1) & gloUC_PatientStrip1.PatientName & Space(5) & "DOB:" & Space(1) & gloUC_PatientStrip1.PatientDateOfBirth & Space(5) & "Phone No.:" & Space(1) & gloUC_PatientStrip1.PatientPhone & Space(5)
        chtPatientGraphs.Footnote.Location.LocationType = MSChart20Lib.VtChLocationType.VtChLocationTypeTop
        chtPatientGraphs.Footnote.Text = strPatientInfo

        chtPatientGraphs.TitleText = "Age Vs Circumfranse"

        Try
            With chtPatientGraphs
                .chartType = MSChart20Lib.VtChChartType.VtChChartType2dCombination
                '.chartType = MSChart20Lib.VtChChartType.VtChChartType2dLine
                If dt_MINMAX.Rows.Count >= 1 Then
                    DataGrid1.DataSource = Nothing
                    DataGrid1.DataSource = dt
                    'Axis Labels for the Graph
                    chtPatientGraphs.Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdX).AxisTitle.Text = "Age (Months)"
                    chtPatientGraphs.Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).AxisTitle.Text = "Head Circumference(cm)"

                    ' get the total number of the vital entries for the patients
                    For i = 1 To dt_MINMAX.Rows.Count - 1  'dt.Rows.Count - 1
                        ' get gender and assign standard Height to the array string
                        arrsales(i, 1) = dt_MINMAX.Rows(i)("Agemos")
                        arrsales(i, 11) = dt_MINMAX.Rows(i)("P3")
                        arrsales(i, 3) = dt_MINMAX.Rows(i)("P5")
                        arrsales(i, 4) = dt_MINMAX.Rows(i)("P10")
                        arrsales(i, 5) = dt_MINMAX.Rows(i)("P25")
                        arrsales(i, 6) = dt_MINMAX.Rows(i)("P50")
                        arrsales(i, 7) = dt_MINMAX.Rows(i)("P75")
                        arrsales(i, 8) = dt_MINMAX.Rows(i)("P90")
                        arrsales(i, 9) = dt_MINMAX.Rows(i)("P95")
                        arrsales(i, 10) = dt_MINMAX.Rows(i)("P97")

                        AgeCollection(i, 1) = dt_MINMAX.Rows(i)("Agemos")
                    Next
                Else
                    MessageBox.Show("No vital data available for this patient.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If

                'Fill For PAtient
                Dim Count As Integer
                Count = arrsales.Length / arrsales.GetLength(1)

                For i = 0 To Count - 1
                    For j As Integer = 0 To dt.Rows.Count - 1
                        If arrsales(i, 1) = dt.Rows(j)("AGE") Then
                            '''' If Age Matches then Add Patients Ht in to The Array 
                            If Not IsDBNull(dt.Rows(j)("dHeadCircumferance")) Then
                                arrsales(i, 2) = dt.Rows(j)("dHeadCircumferance") '/ (0.45) 'FtToMtr(ft, Inch)
                            Else
                                arrsales(i, 2) = 0
                            End If
                            Exit For
                        End If
                    Next
                    arrsales(i, 1) = Nothing
                Next

                'set data to draw chart
                .ChartData = CType(arrsales, Object)
                ' set graph styles   
                '.Plot.Backdrop.Fill.Brush.PatternColor.Set(255, 255, 255)

                Dim cnt As Integer
                For cnt = 1 To 12
                    If cnt <> 3 Then
                        .Plot.SeriesCollection(cnt).SeriesType = MSChart20Lib.VtChSeriesType.VtChSeriesType2dLine
                        .Plot.SeriesCollection(cnt).Pen.Width = 22
                        .Plot.SeriesCollection(cnt).Pen.VtColor.Set(0, 0, 0)
                    End If
                Next

                ' set the chart controls properties for the display.
                ' for y axis
                .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).ValueScale.Minimum = 32
                .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).ValueScale.Maximum = 53
                .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).ValueScale.MajorDivision = 10
                .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).ValueScale.MinorDivision = 1
                .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).CategoryScale.DivisionsPerLabel = 1
                .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).CategoryScale.DivisionsPerTick = 1

                ' for x axis
                .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdX).ValueScale.MajorDivision = 10
                .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdX).ValueScale.MinorDivision = 1

                ' set the properties for the column where data is stored of patients i.e. patient vital entries.
                .Plot.SeriesCollection(3).SeriesType = MSChart20Lib.VtChSeriesType.VtChSeriesType2dLine
                ' .Plot.SeriesCollection(3).SeriesMarker.Auto = True
                .Plot.SeriesCollection(3).Select()
                .Plot.SeriesCollection(3).SeriesMarker.Show = True
                .Plot.SeriesCollection(3).ShowLine = False
                .Plot.SeriesCollection(3).Pen.Style = MSChart20Lib.VtPenStyle.VtPenStyleSolid
                .Plot.SeriesCollection(3).Pen.Width = 6

                ' print labels on the x axis
                For i = 1 To .RowCount
                    .Row = i
                    .RowLabel = AgeCollection(i - 1, 1)
                Next

            End With
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnStature_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim nMonths As Int16
        nMonths = DateDiff(DateInterval.Month, CType(strPatientDOB, Date), Date.Now.Date)

        If nMonths <= 24 Then
            MessageBox.Show("Stature graph available only for more than 24 months patients.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        'lblMin.Text = "Minimum Stature"
        'lblMax.Text = "Maximum Stature"
        'lblPatients.Text = "Patient Stature"

        ' get patient Gender
        Dim nGender As New Integer

        nGender = GetPatientGender(strPatientGender)

        Dim oclsViewGraphs As New clsViewGraphs
        dt = New DataTable
        dt = oclsViewGraphs.ScanAgeHtWt(_PatientID)

        Dim dt_MINMAX As New DataTable

        'get STANDARD values for the minmax values from SP 'gsp_viewGraphMinMax' where standard data is stored.
        If nGender = 1 Then ' for male
            dt_MINMAX = oclsViewGraphs.getminmaxvalues20yrs(Gender.Male, GraphType.Stature, nMonths)
        ElseIf nGender = 2 Then ' for female
            dt_MINMAX = oclsViewGraphs.getminmaxvalues20yrs(Gender.Female, GraphType.Stature, nMonths)
        Else ' for other
            dt_MINMAX = oclsViewGraphs.getminmaxvalues20yrs(Gender.Male, GraphType.Stature, nMonths)
        End If

        'view data in Datagrid
        DataGrid1.DataSource = dt_MINMAX

        Dim i As Integer '= 20
        ' string data where patient Age collection when vital record enterd.
        ''Dim AgeCollection(dt.Rows.Count - 1, 2) As String
        Dim AgeCollection(dt_MINMAX.Rows.Count - 1, 2) As String

        ' string for the data where hight of the patient is collected.
        Dim arrsales(dt_MINMAX.Rows.Count - 1, 12) As String

        Dim arrGraph(dt_MINMAX.Rows.Count - 1, 3) As String

        Dim strPatientInfo As String = Space(5) & " Patient Code:" & Space(1) & gloUC_PatientStrip1.PatientCode & Space(5) & "Patient Name:" & Space(1) & gloUC_PatientStrip1.PatientName & Space(5) & "DOB:" & Space(1) & gloUC_PatientStrip1.PatientDateOfBirth & Space(5) & "Phone No.:" & Space(1) & gloUC_PatientStrip1.PatientPhone & Space(5)
        chtPatientGraphs.Footnote.Location.LocationType = MSChart20Lib.VtChLocationType.VtChLocationTypeTop
        chtPatientGraphs.Footnote.Text = strPatientInfo

        chtPatientGraphs.TitleText = "Age Vs Stature"

        Try
            With chtPatientGraphs
                .chartType = MSChart20Lib.VtChChartType.VtChChartType2dCombination
                '.chartType = MSChart20Lib.VtChChartType.VtChChartType2dLine
                If dt_MINMAX.Rows.Count >= 1 Then
                    DataGrid1.DataSource = Nothing
                    DataGrid1.DataSource = dt
                    'Axis Labels for the Graph
                    chtPatientGraphs.Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdX).AxisTitle.Text = "Age (Months)"
                    chtPatientGraphs.Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).AxisTitle.Text = "Stature (cm)"

                    ' get the total number of the vital entries for the patients
                    For i = 1 To dt_MINMAX.Rows.Count - 1  'dt.Rows.Count - 1
                        ' get gender and assign standard Height to the array string
                        arrsales(i, 1) = dt_MINMAX.Rows(i)("Agemos")
                        arrsales(i, 11) = dt_MINMAX.Rows(i)("P3")
                        arrsales(i, 3) = dt_MINMAX.Rows(i)("P5")
                        arrsales(i, 4) = dt_MINMAX.Rows(i)("P10")
                        arrsales(i, 5) = dt_MINMAX.Rows(i)("P25")
                        arrsales(i, 6) = dt_MINMAX.Rows(i)("P50")
                        arrsales(i, 7) = dt_MINMAX.Rows(i)("P75")
                        arrsales(i, 8) = dt_MINMAX.Rows(i)("P90")
                        arrsales(i, 9) = dt_MINMAX.Rows(i)("P95")
                        arrsales(i, 10) = dt_MINMAX.Rows(i)("P97")

                        AgeCollection(i, 1) = dt_MINMAX.Rows(i)("Agemos")
                    Next
                Else
                    MessageBox.Show("No vital data available for this patient.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If

                'Fill For PAtient
                Dim Count As Integer
                Count = arrsales.Length / arrsales.GetLength(1)

                For i = 0 To Count - 1
                    For j As Integer = 0 To dt.Rows.Count - 1
                        If arrsales(i, 1) = dt.Rows(j)("AGE") Then
                            '''' If Age Matches then Add Patients Ht in to The Array 
                            If IsDBNull(dt.Rows(j)("dStature")) = False Then
                                arrsales(i, 2) = dt.Rows(j)("dStature") '/ (0.45) 'FtToMtr(ft, Inch)
                            Else
                                arrsales(i, 2) = 0
                            End If
                            Exit For
                        End If
                    Next
                    arrsales(i, 1) = Nothing
                Next

                'set data to draw chart
                .ChartData = CType(arrsales, Object)
                ' set graph styles   
                '.Plot.Backdrop.Fill.Brush.PatternColor.Set(255, 255, 255)

                Dim cnt As Integer
                For cnt = 1 To 12
                    If cnt <> 3 Then
                        .Plot.SeriesCollection(cnt).SeriesType = MSChart20Lib.VtChSeriesType.VtChSeriesType2dLine
                        .Plot.SeriesCollection(cnt).Pen.Width = 22
                        .Plot.SeriesCollection(cnt).Pen.VtColor.Set(0, 0, 0)
                    End If
                Next

                ' set the chart controls properties for the display.
                ' for y axis
                .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).ValueScale.Minimum = 80
                .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).ValueScale.Maximum = 190
                .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).ValueScale.MajorDivision = 10
                .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).ValueScale.MinorDivision = 1
                .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).CategoryScale.DivisionsPerLabel = 1
                .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).CategoryScale.DivisionsPerTick = 1

                ' for x axis
                .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdX).ValueScale.MajorDivision = 10
                .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdX).ValueScale.MinorDivision = 1

                ' set the properties for the column where data is stored of patients i.e. patient vital entries.
                .Plot.SeriesCollection(3).SeriesType = MSChart20Lib.VtChSeriesType.VtChSeriesType2dLine
                ' .Plot.SeriesCollection(3).SeriesMarker.Auto = True
                .Plot.SeriesCollection(3).Select()
                .Plot.SeriesCollection(3).SeriesMarker.Show = True
                .Plot.SeriesCollection(3).ShowLine = False
                .Plot.SeriesCollection(3).Pen.Style = MSChart20Lib.VtPenStyle.VtPenStyleSolid
                .Plot.SeriesCollection(3).Pen.Width = 6

                ' print labels on the x axis
                For i = 1 To .RowCount
                    .Row = i
                    .RowLabel = AgeCollection(i - 1, 1)
                Next

            End With
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnAgeWeight20plus_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim nMonths As Int16
        nMonths = DateDiff(DateInterval.Month, CType(strPatientDOB, Date), Date.Now.Date)

        If nMonths < 24 Then
            MessageBox.Show("Graph available for more than 2 year old patients.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        'lblMin.Text = "Minimum Weight"
        'lblMax.Text = "Maximum Weight"
        'lblPatients.Text = "Patient Weight"

        Dim oclsViewGraphs As New clsViewGraphs
        dt = New DataTable
        dt = oclsViewGraphs.ScanAgeHtWt(_PatientID)

        ' get patient Gender
        Dim nGender As New Integer

        nGender = GetPatientGender(strPatientGender)

        Dim dt_MINMAX As New DataTable

        'get STANDARD values for the minmax values from SP 'gsp_viewGraphMinMax' where standard data is stored.
        If nGender = 1 Then ' for male
            dt_MINMAX = oclsViewGraphs.getminmaxvalues20yrs(Gender.Male, GraphType.Weight, nMonths)
        ElseIf nGender = 2 Then ' for female
            dt_MINMAX = oclsViewGraphs.getminmaxvalues20yrs(Gender.Female, GraphType.Weight, nMonths)
        Else ' for other
            dt_MINMAX = oclsViewGraphs.getminmaxvalues20yrs(Gender.Male, GraphType.Weight, nMonths)
        End If

        'view data in Datagrid
        DataGrid1.DataSource = dt_MINMAX

        Dim i As Integer '= 20
        ' string data where patient Age collection when vital record enterd.
        ''Dim AgeCollection(dt.Rows.Count - 1, 2) As String
        Dim AgeCollection(dt_MINMAX.Rows.Count - 1, 2) As String

        ' string for the data where hight of the patient is collected.
        Dim arrsales(dt_MINMAX.Rows.Count - 1, 12) As String

        Dim arrGraph(dt_MINMAX.Rows.Count - 1, 3) As String

        Dim strPatientInfo As String = Space(5) & " Patient Code:" & Space(1) & gloUC_PatientStrip1.PatientCode & Space(5) & "Patient Name:" & Space(1) & gloUC_PatientStrip1.PatientName & Space(5) & "DOB:" & Space(1) & gloUC_PatientStrip1.PatientDateOfBirth & Space(5) & "Phone No.:" & Space(1) & gloUC_PatientStrip1.PatientPhone & Space(5)
        chtPatientGraphs.Footnote.Location.LocationType = MSChart20Lib.VtChLocationType.VtChLocationTypeTop
        chtPatientGraphs.Footnote.Text = strPatientInfo

        chtPatientGraphs.TitleText = "Age Vs Weight"

        Try
            With chtPatientGraphs
                .chartType = MSChart20Lib.VtChChartType.VtChChartType2dCombination
                '.chartType = MSChart20Lib.VtChChartType.VtChChartType2dLine
                If dt_MINMAX.Rows.Count >= 1 Then
                    DataGrid1.DataSource = Nothing
                    DataGrid1.DataSource = dt
                    'Axis Labels for the Graph
                    chtPatientGraphs.Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdX).AxisTitle.Text = "Age (Months)"
                    chtPatientGraphs.Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).AxisTitle.Text = "Weight (lbs)"

                    ' get the total number of the vital entries for the patients
                    For i = 0 To dt_MINMAX.Rows.Count - 1   'dt.Rows.Count - 1
                        ' get gender and assign standard Height to the array string
                        arrsales(i, 1) = dt_MINMAX.Rows(i)("Agemos")
                        arrsales(i, 11) = dt_MINMAX.Rows(i)("P3") / (0.45)
                        arrsales(i, 3) = dt_MINMAX.Rows(i)("P5") / (0.45)
                        arrsales(i, 4) = dt_MINMAX.Rows(i)("P10") / (0.45)
                        arrsales(i, 5) = dt_MINMAX.Rows(i)("P25") / (0.45)
                        arrsales(i, 6) = dt_MINMAX.Rows(i)("P50") / (0.45)
                        arrsales(i, 7) = dt_MINMAX.Rows(i)("P75") / (0.45)
                        arrsales(i, 8) = dt_MINMAX.Rows(i)("P90") / (0.45)
                        arrsales(i, 9) = dt_MINMAX.Rows(i)("P95") / (0.45)
                        arrsales(i, 10) = dt_MINMAX.Rows(i)("P97") / (0.45)

                        AgeCollection(i, 1) = dt_MINMAX.Rows(i)("Agemos")
                    Next
                Else
                    MessageBox.Show("No vital data available for this patient.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If

                Dim graphMin As Integer
                graphMin = dt_MINMAX.Rows(1)("P3") / (0.45)

                Dim graphMax As Integer
                graphMax = dt_MINMAX.Rows(dt_MINMAX.Rows.Count - 1)("P97") / (0.45)


                'Fill For PAtient
                Dim Count As Integer
                Count = arrsales.Length / arrsales.GetLength(1)

                For i = 0 To Count - 1
                    For j As Integer = 0 To dt.Rows.Count - 1
                        If arrsales(i, 1) = dt.Rows(j)("AGE") Then
                            '''' If Age Matches then Add Patients Ht in to The Array 
                            If Not IsDBNull(dt.Rows(j)("dWeightinlbs")) Then
                                arrsales(i, 2) = dt.Rows(j)("dWeightinlbs") '/ (0.45) 'FtToMtr(ft, Inch)
                            Else
                                arrsales(i, 2) = 0
                            End If
                            Exit For
                        End If
                    Next
                    arrsales(i, 1) = Nothing
                Next

                'set data to draw chart
                .ChartData = CType(arrsales, Object)
                ' set graph styles   
                '.Plot.Backdrop.Fill.Brush.PatternColor.Set(255, 255, 255)               

                Dim cnt As Integer
                For cnt = 1 To 12
                    If cnt <> 3 Then
                        .Plot.SeriesCollection(cnt).SeriesType = MSChart20Lib.VtChSeriesType.VtChSeriesType2dLine
                        .Plot.SeriesCollection(cnt).Pen.Width = 22
                        .Plot.SeriesCollection(cnt).Pen.VtColor.Set(0, 0, 0)
                    End If
                Next

                .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).ValueScale.Minimum = graphMin - 10 '1
                .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).ValueScale.Maximum = graphMax + 10 '40
                .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).ValueScale.MajorDivision = 10
                .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).ValueScale.MinorDivision = 1
                .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).CategoryScale.DivisionsPerLabel = 1
                .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).CategoryScale.DivisionsPerTick = 1

                .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdX).ValueScale.MajorDivision = 10
                .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdX).ValueScale.MinorDivision = 1
                '.Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdX).AxisTitle.VtFont.Size = 1

                .Plot.SeriesCollection(3).SeriesType = MSChart20Lib.VtChSeriesType.VtChSeriesType2dLine
                ' .Plot.SeriesCollection(3).SeriesMarker.Auto = True
                .Plot.SeriesCollection(3).Select()
                .Plot.SeriesCollection(3).SeriesMarker.Show = True
                .Plot.SeriesCollection(3).ShowLine = False
                .Plot.SeriesCollection(3).Pen.Style = MSChart20Lib.VtPenStyle.VtPenStyleDashDotDot
                .Plot.SeriesCollection(3).Pen.Width = 6

                Dim yrs As Integer
                Dim reminder As Integer

                For i = 1 To .RowCount
                    .Row = i
                    yrs = AgeCollection(i - 1, 1) / 12
                    reminder = AgeCollection(i - 1, 1) Mod 12

                    If reminder = 0 Then
                        .RowLabel = yrs & "Yrs"
                    Else
                        .RowLabel = AgeCollection(i - 1, 1)
                    End If
                Next

            End With
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub btnWeightVsHeight_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Dim oclsViewGraphs As New clsViewGraphs
        dt = New DataTable
        dt = oclsViewGraphs.ScanAgeHtWt(_PatientID)

        ' get patient Gender
        Dim nGender As New Integer

        nGender = GetPatientGender(strPatientGender)

        Dim dt_MINMAX As New DataTable

        'get STANDARD values for the minmax values from SP 'gsp_viewGraphMinMax' where standard data is stored.
        If nGender = 1 Then ' for male
            dt_MINMAX = oclsViewGraphs.getminmaxvaluesWtHt(Gender.Male)
        ElseIf nGender = 2 Then ' for female
            dt_MINMAX = oclsViewGraphs.getminmaxvaluesWtHt(Gender.Female)
        Else ' for other
            dt_MINMAX = oclsViewGraphs.getminmaxvaluesWtHt(Gender.Male)
        End If

        'view data in Datagrid
        DataGrid1.DataSource = dt_MINMAX

        Dim i As Integer '= 20
        ' string data where patient Age collection when vital record enterd.
        ''Dim AgeCollection(dt.Rows.Count - 1, 2) As String
        Dim AgeCollection(dt_MINMAX.Rows.Count - 1, 2) As String

        ' string for the data where hight of the patient is collected.
        Dim arrsales(dt_MINMAX.Rows.Count - 1, 12) As String

        Dim arrGraph(dt_MINMAX.Rows.Count - 1, 3) As String

        Dim strPatientInfo As String = Space(5) & " Patient Code:" & Space(1) & gloUC_PatientStrip1.PatientCode & Space(5) & "Patient Name:" & Space(1) & gloUC_PatientStrip1.PatientName & Space(5) & "DOB:" & Space(1) & gloUC_PatientStrip1.PatientDateOfBirth & Space(5) & "Phone No.:" & Space(1) & gloUC_PatientStrip1.PatientPhone & Space(5)
        chtPatientGraphs.Footnote.Location.LocationType = MSChart20Lib.VtChLocationType.VtChLocationTypeTop
        chtPatientGraphs.Footnote.Text = strPatientInfo

        chtPatientGraphs.TitleText = "Weight Vs Height"

        Try
            With chtPatientGraphs
                .chartType = MSChart20Lib.VtChChartType.VtChChartType2dCombination
                '.chartType = MSChart20Lib.VtChChartType.VtChChartType2dLine
                If dt_MINMAX.Rows.Count >= 1 Then
                    DataGrid1.DataSource = Nothing
                    DataGrid1.DataSource = dt
                    'Axis Labels for the Graph
                    chtPatientGraphs.Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdX).AxisTitle.Text = "Height (Inch)"
                    chtPatientGraphs.Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).AxisTitle.Text = "Weight (Months)"

                    ' get the total number of the vital entries for the patients
                    For i = 1 To dt_MINMAX.Rows.Count - 1  'dt.Rows.Count - 1
                        ' get gender and assign standard Height to the array string
                        arrsales(i, 1) = dt_MINMAX.Rows(i)("Length") * 0.394
                        arrsales(i, 11) = dt_MINMAX.Rows(i)("P3") * 2.204
                        arrsales(i, 3) = dt_MINMAX.Rows(i)("P5") * 2.204
                        arrsales(i, 4) = dt_MINMAX.Rows(i)("P10") * 2.204
                        arrsales(i, 5) = dt_MINMAX.Rows(i)("P25") * 2.204
                        arrsales(i, 6) = dt_MINMAX.Rows(i)("P50") * 2.204
                        arrsales(i, 7) = dt_MINMAX.Rows(i)("P75") * 2.204
                        arrsales(i, 8) = dt_MINMAX.Rows(i)("P90") * 2.204
                        arrsales(i, 9) = dt_MINMAX.Rows(i)("P95") * 2.204
                        arrsales(i, 10) = dt_MINMAX.Rows(i)("P97") * 2.204

                        AgeCollection(i, 1) = dt_MINMAX.Rows(i)("Length") * 0.394
                    Next
                Else
                    MessageBox.Show("No vital data available for this patient.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If

                'Fill For PAtient
                Dim Count As Integer
                Count = arrsales.Length / arrsales.GetLength(1)
                For i = 0 To Count - 1
                    For j As Integer = 0 To dt.Rows.Count - 1
                        Dim ft As Decimal
                        Dim Inch As Decimal
                        Dim temp() As String
                        ' data convert from ftInch to in points i.e. 1 ft 6 Inch is 1.5 Ft
                        If Not IsDBNull(GetFtInch(dt.Rows(j)("sHeight"))) Then
                            temp = GetFtInch(dt.Rows(j)("sHeight"))
                        Else
                            temp = GetFtInch(0)
                        End If

                        '''' Only Inch is available then
                        If temp(0).Trim <> "" Then
                            ft = Convert.ToDecimal(CType(temp(0), Object))
                        Else
                            ft = 0
                        End If
                        If temp.Length > 1 Then
                            If temp(1).Trim <> "" Then
                                Inch = Convert.ToDecimal(CType(temp(1), Object))
                            Else
                                Inch = 0
                            End If
                        End If

                        If arrsales(i, 1) >= FtToMtr(ft, Inch) And arrsales(i, 1) <= (FtToMtr(ft, Inch) + 0.5) Then
                            '''' If Age Matches then Add Patients Ht in to The Array 
                            If Not IsDBNull(dt.Rows(j)("dweightinlbs")) Then
                                arrsales(i, 2) = dt.Rows(j)("dweightinlbs") ' dt.Rows(j)("sHeight") '
                            Else
                                arrsales(i, 2) = 0
                            End If

                            Exit For
                        End If
                    Next
                    arrsales(i, 1) = Nothing
                Next

                'set data to draw chart
                .ChartData = CType(arrsales, Object)

                ' set graph styles for the patient vital entries.
                .Plot.SeriesCollection(3).SeriesType = MSChart20Lib.VtChSeriesType.VtChSeriesType2dLine
                .Plot.SeriesCollection(3).Select()
                .Plot.SeriesCollection(3).ShowLine = False
                .Plot.SeriesCollection(3).DataPoints(-1).Marker.Style = MSChart20Lib.VtMarkerStyle.VtMarkerStyleDiamond
                .Plot.SeriesCollection(3).DataPoints(-1).Marker.Visible = True
                .Plot.SeriesCollection(3).DataPoints(-1).Marker.FillColor.Set(255, 0, 0)
                .Plot.SeriesCollection(3).DataPoints(-1).Brush.Style = MSChart20Lib.VtBrushStyle.VtBrushStyleSolid
                .Plot.SeriesCollection(3).DataPoints(-1).Marker.Size = 20
                .Plot.SeriesCollection(3).Pen.Width = 20
                .Plot.SeriesCollection(3).DataPoints(-1).Brush.FillColor.Set(255, 0, 0)

                ' set the display properties of the graphs curve.
                Dim cnt As Integer
                For cnt = 1 To 12
                    If cnt <> 3 Then
                        .Plot.SeriesCollection(cnt).SeriesType = MSChart20Lib.VtChSeriesType.VtChSeriesType2dLine
                        .Plot.SeriesCollection(cnt).Pen.Width = 22
                        .Plot.SeriesCollection(cnt).Pen.VtColor.Set(0, 0, 0)
                    End If
                Next

                ' set the graph properties for the y axis.
                .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).ValueScale.Minimum = 1
                .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).ValueScale.Maximum = 45
                .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).ValueScale.MajorDivision = 10
                .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).ValueScale.MinorDivision = 1
                .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).CategoryScale.DivisionsPerLabel = 1
                .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).CategoryScale.DivisionsPerTick = 1

                ' set the properties for the x axis.
                .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdX).ValueScale.MajorDivision = 10
                .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdX).ValueScale.MinorDivision = 10

                'set the label of x axis.
                ' Dim yrs, reminder

                For i = 1 To .RowCount
                    .Row = i
                    If AgeCollection(i - 1, 1) <> Nothing OrElse AgeCollection(i - 1, 1) = "" Then
                        .RowLabel = AgeCollection(i - 1, 1)
                    Else
                        .RowLabel = "" 'AgeCollection(i - 1, 1)
                    End If

                Next

            End With
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub tblStrip_32_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tblStrip_32.ItemClicked
        Select Case e.ClickedItem.Tag
            Case "AgeStature"
                AgeStature()
            Case "AgeWeight24" 'Save + Close
                AgeWeight24()
            Case "WeightHeight"
                WeightHeight()
            Case "AgeCircumfranse" 'Save + Close
                AgeCircumfranse()
            Case "AgeHeight"
                AgeHeight()
            Case "AgeWeight" 'Save + Close
                AgeWeight()
            Case "Print"
                PrintGraph()
            Case "Close"
                CloseVitals()
        End Select
    End Sub

    Public Sub PrintGraph()
        chtPatientGraphs.Backdrop.Fill.Brush.Style = MSChart20Lib.VtBrushStyle.VtBrushStyleSolid
        chtPatientGraphs.Backdrop.Fill.Style = MSChart20Lib.VtFillStyle.VtFillStyleBrush
        System.Windows.Forms.Application.DoEvents()
        If Not IsNothing(Pcte) Then
            Pcte.Dispose()
            Pcte = Nothing
        End If
        Pcte = Hardcopy.CreateBitmap(chtPatientGraphs)

        'PictureBox1.Image = Pcte
        'Removed PegausImageXpress7 -> Dhruv
        'With PicImag_Graph
        '    .Picture = Pcte
        '    .ZoomToFit(PegasusImaging.WinForms.ImagXpress7.enumZoomFit.ZOOMFIT_BEST)
        '    .BorderType = PegasusImaging.WinForms.ImagXpress7.enumBorderType.BORD_None
        'End With
        ''------
        'm_oGraphPic1.FormattedPicture = VB6.ImageToIPictureDisp(Pcte)
        chtPatientGraphs.Backdrop.Fill.Style = MSChart20Lib.VtFillStyle.VtFillStyleNull
        chtPatientGraphs.Backdrop.Fill.Brush.Style = MSChart20Lib.VtBrushStyle.VtBrushStyleSolid

        'PicPrintPro1.StartPrintDoc()
        'PicPrintPro1.ScaleMode = PegasusImaging.WinForms.PrintPro3.peScaleMode.SCALE_Pixel
        'Dim a As Integer
        'Dim b As Integer


        'PicImag_Graph.Picture = Pcte
        'PicPrintPro1.hDIB = PicImag_Graph.hDIB
        'PicPrintPro1.Alignment = PegasusImaging.WinForms.PrintPro3.peAlignment.ALIGN_CenterJustifyTop


        'a = PicPrintPro1.ScaleWidth - 1 - PicPrintPro1.Lmargin
        'b = PicPrintPro1.ScaleHeight - 1500 - PicPrintPro1.TMargin - PicPrintPro1.BMargin

        ''PicPrintPro1.PrintPicture(PicPrintPro1.Lmargin, PicPrintPro1.TMargin, a, b, 0, 0, 0, 0, True)
        'PicPrintPro1.PrintDIB(PicPrintPro1.Lmargin, PicPrintPro1.TMargin, a, b, 0, 0, 0, 0, True)

        'PicPrintPro1.EndPrintDoc()

        If gloGlobal.gloTSPrint.isCopyPrint Then
            Dim impPrint As gloGlobal.ImagePrint = New gloGlobal.ImagePrint()
            Dim dictImages As Dictionary(Of [String], [Byte]()) = impPrint.printdoc_Print_Conversion(8.5F, 11.0F, 600, 600, Pcte)
            Dim fileName As String = gloGlobal.clsFileExtensions.NewDocumentName(gloGlobal.gloTSPrint.TempPath, ".zip", "MMddyyyyHHmmssffff")
            Dim lstDocs As New List(Of gloPrintDialog.gloPrintProgressController.DocumentInfo)()
            Dim ZipedFiles As List(Of String) = gloGlobal.gloTSPrint.ZipAllBytes(dictImages, fileName, gloGlobal.gloTSPrint.NoOfPages)
            For i As Integer = 0 To ZipedFiles.Count - 1
                Dim DocInfo As New gloPrintDialog.gloPrintProgressController.DocumentInfo()
                DocInfo.PdfFileName = ZipedFiles(i)
                DocInfo.SrcFileName = ZipedFiles(i)
                DocInfo.footerInfo = Nothing
                lstDocs.Add(DocInfo)
            Next
            gloPrintDialog.gloPrintProgressController.SendForPrint(lstDocs)
        Else
            Dim printer As PegasusImaging.WinForms.PrintPro4.Printer
            Dim job As PegasusImaging.WinForms.PrintPro4.PrintJob
            printer = PegasusImaging.WinForms.PrintPro4.Printer.SelectPrinter(PrintPro1, False)

            If Not printer Is Nothing Then
                ' Create a PrintJob object for the above-selected Printer. If the printer is not specified, PrintPRO
                ' will just use Windows's default printer.
                job = New PegasusImaging.WinForms.PrintPro4.PrintJob(printer)
                job.Name = "Print Graph"

                ' Print all of the information on the page
                PrintPage(job)
                ' Finish the print job to end the current page and print the document.
                job.Finish()
                job.Dispose()
                printer.Dispose()
            End If
        End If


        If Not IsNothing(Pcte) Then
            Pcte.Dispose()
            Pcte = Nothing
        End If
    End Sub

    'Private Sub SendForPrint(lstDocs As List(Of gloPrintDialog.gloPrintProgressController.DocumentInfo))
    '    Dim ogloPrintProgressController As gloPrintDialog.gloPrintProgressController = Nothing

    '    Try
    '        Dim extendedPrinterSettings As New gloPrintDialog.gloExtendedPrinterSettings()
    '        extendedPrinterSettings.IsShowProgress = False
    '        extendedPrinterSettings.IsBackGroundPrint = True
    '        ogloPrintProgressController = New gloPrintDialog.gloPrintProgressController(lstDocs, Nothing, extendedPrinterSettings, blnUseEMFForSSRS:=True)
    '        ogloPrintProgressController.ShowProgress(Nothing)
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message, gloGlobal.gloTSPrint.getMessageCaption(), MessageBoxButtons.OK, MessageBoxIcon.[Error])

    '        ex = Nothing
    '    Finally
    '    End Try
    'End Sub

    Private Sub PrintPage(ByVal job As PegasusImaging.WinForms.PrintPro4.PrintJob)
        ' job.PrintImage(picture, new PointF(1640, 5700), new SizeF(5000, 4000), new PointF(0, 0), new SizeF(picture.Width, picture.Height), false)
        If Not IsNothing(Pcte) Then

            job.PrintImage(Pcte, New PointF(1000, 1000), New SizeF(10600, 9600), True)

        End If

    End Sub

    'code added by sarika vital Graphs 3rd june 08

    Private Function FtToMtr(ByVal Ft As Decimal, ByVal Inch As Decimal) As Decimal
        Return ((Ft * 30.48 + Inch * 2.54)) ' * 0.394) 'for centi meter
        ' Return (Ft + Inch * 2.54)
        'Return (Ft + Inch * 0.083)
        ''   1 ft = 30.48 cm
        ''   1 inch = 2.54 cm
    End Function

    Private Sub AgeStature()
        '''' Calculate Patient Age in Months
        Dim nMonths As Int16
        nMonths = DateDiff(DateInterval.Month, CType(strPatientDOB, Date), Date.Now.Date)

        If nMonths <= 24 Then
            MessageBox.Show("Stature graph available only for more than 24 months patients.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        'lblMin.Text = "Minimum Stature"
        'lblMax.Text = "Maximum Stature"
        'lblPatients.Text = "Patient Stature"

        '''' Get patient Gender
        ' Dim nGender As New Integer

        ' nGender = oclsHPITemplate.GetPatientGender(gnPatientID)

        ''''Get Patient Vitals Records
        Dim oclsViewGraphs As New clsViewGraphs
        dt = New DataTable
        dt = oclsViewGraphs.ScanAgeHtWt(_PatientID)

        'oclsHPITemplate = Nothing

        Dim dt_MINMAX As New DataTable


        '''''Get Standard MinMax values from gsp_ViewGraphMinMax SP
        'If nGender = 1 Then 'For male
        '    dt_MINMAX = oclsViewGraphs.getminmaxvalues20yrs(Gender.Male, GraphType.Stature, nMonths)
        'ElseIf nGender = 2 Then 'For female
        '    dt_MINMAX = oclsViewGraphs.getminmaxvalues20yrs(Gender.Female, GraphType.Stature, nMonths)
        'Else ' For other
        '    dt_MINMAX = oclsViewGraphs.getminmaxvalues20yrs(Gender.Male, GraphType.Stature, nMonths)
        'End If

        ''''Get Standard MinMax values from gsp_ViewGraphMinMax SP
        If strPatientGender.ToUpper = "Male".ToUpper Then 'For male
            dt_MINMAX = oclsViewGraphs.getminmaxvalues20yrs(Gender.Male, GraphType.Stature, nMonths)
        ElseIf strPatientGender.ToUpper = "Female".ToUpper Then 'For female
            dt_MINMAX = oclsViewGraphs.getminmaxvalues20yrs(Gender.Female, GraphType.Stature, nMonths)
        Else ' For other
            dt_MINMAX = oclsViewGraphs.getminmaxvalues20yrs(Gender.Male, GraphType.Stature, nMonths)
        End If

        ''''View data in Datagrid
        DataGrid1.DataSource = dt_MINMAX

        Dim i As Integer '= 20

        '''' String data where patient Age collection when vital record enterd.
        Dim AgeCollection(dt_MINMAX.Rows.Count - 1, 2) As String

        '''' String for the data where hight of the patient is collected.
        Dim arrStatureVsAge(dt_MINMAX.Rows.Count - 1, 12) As String

        ''''
        Dim arrGraph(dt_MINMAX.Rows.Count - 1, 3) As String

        Dim strPatientInfo As String


        Try
            With chtPatientGraphs
                '''' Set Graph Properties
                strPatientInfo = Space(5) & " Patient Code:" & Space(1) & gloUC_PatientStrip1.PatientCode & Space(5) & "Patient Name:" & Space(1) & gloUC_PatientStrip1.PatientName & Space(5) & "DOB:" & Space(1) & gloUC_PatientStrip1.PatientDateOfBirth & Space(5) & "Phone No.:" & Space(1) & gloUC_PatientStrip1.PatientPhone & Space(5)
                .Footnote.Location.LocationType = MSChart20Lib.VtChLocationType.VtChLocationTypeTop
                .Footnote.Text = strPatientInfo
                .TitleText = "Age Vs Stature"
                .chartType = MSChart20Lib.VtChChartType.VtChChartType2dCombination


                '''' Set MinMax data to arrStatureVsAge arraly
                If dt_MINMAX.Rows.Count >= 1 Then
                    DataGrid1.DataSource = Nothing
                    DataGrid1.DataSource = dt
                    'Axis Labels for the Graph
                    chtPatientGraphs.Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdX).AxisTitle.Text = "Age (Months)"
                    chtPatientGraphs.Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).AxisTitle.Text = "Stature (cm)"
                    ' get the total number of the vital entries for the patients
                    For i = 0 To dt_MINMAX.Rows.Count - 1  'dt.Rows.Count - 1
                        arrStatureVsAge(i, 1) = dt_MINMAX.Rows(i)("Agemos")
                        arrStatureVsAge(i, 11) = dt_MINMAX.Rows(i)("P3")
                        arrStatureVsAge(i, 3) = dt_MINMAX.Rows(i)("P5")
                        arrStatureVsAge(i, 4) = dt_MINMAX.Rows(i)("P10")
                        arrStatureVsAge(i, 5) = dt_MINMAX.Rows(i)("P25")
                        arrStatureVsAge(i, 6) = dt_MINMAX.Rows(i)("P50")
                        arrStatureVsAge(i, 7) = dt_MINMAX.Rows(i)("P75")
                        arrStatureVsAge(i, 8) = dt_MINMAX.Rows(i)("P90")
                        arrStatureVsAge(i, 9) = dt_MINMAX.Rows(i)("P95")
                        arrStatureVsAge(i, 10) = dt_MINMAX.Rows(i)("P97")
                        AgeCollection(i, 1) = dt_MINMAX.Rows(i)("Agemos")
                    Next
                Else
                    MessageBox.Show("No vital data available for this patient.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If

                'Fill For PAtient
                Dim Count As Integer
                'Count = arrStatureVsAge.Length / arrStatureVsAge.GetLength(1)
                Count = dt_MINMAX.Rows.Count

                '''' Ploat the Patient Record Point on the graph Using send that in array 
                For i = 0 To Count - 1
                    For j As Integer = 0 To dt.Rows.Count - 1
                        If arrStatureVsAge(i, 1) = dt.Rows(j)("AGE") Then '''' If Patient Age Match with standard Age Then Insert Data in array
                            If IsDBNull(dt.Rows(j)("dStature")) = False Then
                                arrStatureVsAge(i, 2) = dt.Rows(j)("dStature") '/ (0.45) 'FtToMtr(ft, Inch)
                            Else
                                arrStatureVsAge(i, 2) = 0
                            End If
                            Exit For
                        End If
                    Next
                    arrStatureVsAge(i, 1) = Nothing
                Next

                '''' Bind data to Chart
                .ChartData = CType(arrStatureVsAge, Object)


                '''' Set Graph Style


                Dim cnt As Integer
                For cnt = 1 To 12
                    If cnt <> 3 Then
                        .Plot.SeriesCollection(cnt).SeriesType = MSChart20Lib.VtChSeriesType.VtChSeriesType2dLine
                        .Plot.SeriesCollection(cnt).Pen.Width = 22
                        '.Plot.SeriesCollection(cnt).Pen.VtColor.Set(0, 0, 0)
                    End If
                Next

                ' set the chart controls properties for the display.
                ' for y axis
                .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).ValueScale.Minimum = 80
                .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).ValueScale.Maximum = 190
                .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).ValueScale.MajorDivision = 10
                .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).ValueScale.MinorDivision = 1
                .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).CategoryScale.DivisionsPerLabel = 1
                .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).CategoryScale.DivisionsPerTick = 1

                ' for x axis
                .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdX).ValueScale.MajorDivision = 10
                .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdX).ValueScale.MinorDivision = 1

                ' set the properties for the column where data is stored of patients i.e. patient vital entries.
                .Plot.SeriesCollection(3).SeriesType = MSChart20Lib.VtChSeriesType.VtChSeriesType2dLine
                ' .Plot.SeriesCollection(3).SeriesMarker.Auto = True
                .Plot.SeriesCollection(3).Select()
                .Plot.SeriesCollection(3).SeriesMarker.Show = True
                .Plot.SeriesCollection(3).ShowLine = False
                .Plot.SeriesCollection(3).Pen.Style = MSChart20Lib.VtPenStyle.VtPenStyleSolid
                .Plot.SeriesCollection(3).Pen.Width = 6

                ' print labels on the x axis
                For i = 1 To .RowCount
                    .Row = i
                    .RowLabel = AgeCollection(i - 1, 1)
                Next

            End With
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub AgeWeight24()

        '''' Calculate Patient Age in Months
        Dim nMonths As Int16
        nMonths = DateDiff(DateInterval.Month, CType(strPatientDOB, Date), Date.Now.Date)

        If nMonths < 24 Then
            MessageBox.Show("Graph available for more than 2 year old patients.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        'lblMin.Text = "Minimum Weight"
        'lblMax.Text = "Maximum Weight"
        'lblPatients.Text = "Patient Weight"

        ''''Get patient Vitals Records
        Dim oclsViewGraphs As New clsViewGraphs
        dt = New DataTable
        dt = oclsViewGraphs.ScanAgeHtWt(_PatientID)

        ''''Get Patient Gender
        Dim nGender As New Integer
        nGender = GetPatientGender(strPatientGender)

        Dim dt_MINMAX As New DataTable

        ''''Get Standard Values 
        If nGender = 1 Then ' For male
            dt_MINMAX = oclsViewGraphs.getminmaxvalues20yrs(Gender.Male, GraphType.Weight, nMonths)
        ElseIf nGender = 2 Then ' For female
            dt_MINMAX = oclsViewGraphs.getminmaxvalues20yrs(Gender.Female, GraphType.Weight, nMonths)
        Else ' For other
            dt_MINMAX = oclsViewGraphs.getminmaxvalues20yrs(Gender.Male, GraphType.Weight, nMonths)
        End If

        'View data in Datagrid
        DataGrid1.DataSource = dt_MINMAX

        Dim i As Integer '= 20

        ' string data where patient Age collection when vital record enterd.

        Dim AgeCollection(dt_MINMAX.Rows.Count - 1, 2) As String


        Dim arrAgeWeight24(dt_MINMAX.Rows.Count - 1, 12) As String

        Dim arrGraph(dt_MINMAX.Rows.Count - 1, 3) As String


        Dim strPatientInfo As String = ""


        Try
            With chtPatientGraphs
                ''''Set Graph Properties
                strPatientInfo = Space(5) & " Patient Code:" & Space(1) & gloUC_PatientStrip1.PatientCode & Space(5) & "Patient Name:" & Space(1) & gloUC_PatientStrip1.PatientName & Space(5) & "DOB:" & Space(1) & gloUC_PatientStrip1.PatientDateOfBirth & Space(5) & "Phone No.:" & Space(1) & gloUC_PatientStrip1.PatientPhone & Space(5)
                .Footnote.Location.LocationType = MSChart20Lib.VtChLocationType.VtChLocationTypeTop
                .Footnote.Text = strPatientInfo
                .TitleText = "Age Vs Weight"
                .chartType = MSChart20Lib.VtChChartType.VtChChartType2dCombination


                If dt_MINMAX.Rows.Count >= 1 Then
                    DataGrid1.DataSource = Nothing
                    DataGrid1.DataSource = dt

                    'Axis Labels for the Graph
                    chtPatientGraphs.Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdX).AxisTitle.Text = "Age (Months)"
                    chtPatientGraphs.Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).AxisTitle.Text = "Weight (lbs)"

                    '''' Get The Vitals Entry in Kg and convet it in lbs using (/0.45) and assign it to array
                    For i = 0 To dt_MINMAX.Rows.Count - 1
                        arrAgeWeight24(i, 1) = dt_MINMAX.Rows(i)("Agemos")
                        arrAgeWeight24(i, 11) = dt_MINMAX.Rows(i)("P3") / (0.45)
                        arrAgeWeight24(i, 3) = dt_MINMAX.Rows(i)("P5") / (0.45)
                        arrAgeWeight24(i, 4) = dt_MINMAX.Rows(i)("P10") / (0.45)
                        arrAgeWeight24(i, 5) = dt_MINMAX.Rows(i)("P25") / (0.45)
                        arrAgeWeight24(i, 6) = dt_MINMAX.Rows(i)("P50") / (0.45)
                        arrAgeWeight24(i, 7) = dt_MINMAX.Rows(i)("P75") / (0.45)
                        arrAgeWeight24(i, 8) = dt_MINMAX.Rows(i)("P90") / (0.45)
                        arrAgeWeight24(i, 9) = dt_MINMAX.Rows(i)("P95") / (0.45)
                        arrAgeWeight24(i, 10) = dt_MINMAX.Rows(i)("P97") / (0.45)

                        AgeCollection(i, 1) = dt_MINMAX.Rows(i)("Agemos")
                    Next
                Else
                    MessageBox.Show("No vital data available for this patient.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If

                Dim graphMin As Integer
                graphMin = dt_MINMAX.Rows(1)("P3") / (0.45)

                Dim graphMax As Integer
                graphMax = dt_MINMAX.Rows(dt_MINMAX.Rows.Count - 1)("P97") / (0.45)


                'Fill For PAtient
                Dim Count As Integer
                'Count = arrAgeWeight24.Length / arrAgeWeight24.GetLength(1)
                Count = dt_MINMAX.Rows.Count

                '''' Ploat the Patient Record Point on to Graph
                For i = 0 To Count - 1
                    For j As Integer = 0 To dt.Rows.Count - 1
                        If arrAgeWeight24(i, 1) = dt.Rows(j)("AGE") Then '''' If age match with standard vital data insert it in array
                            If Not IsDBNull(dt.Rows(j)("dWeightinlbs")) Then
                                arrAgeWeight24(i, 2) = dt.Rows(j)("dWeightinlbs") '/ (0.45) 'FtToMtr(ft, Inch)
                            Else
                                arrAgeWeight24(i, 2) = Nothing
                            End If
                            Exit For
                        End If
                    Next
                    arrAgeWeight24(i, 1) = Nothing
                Next

                '''' Set the Chart Data
                .ChartData = CType(arrAgeWeight24, Object)

                ''''Set Graph Style
                '.Plot.Backdrop.Fill.Brush.PatternColor.Set(255, 255, 255)               

                Dim cnt As Integer
                For cnt = 1 To 12
                    If cnt <> 3 Then
                        .Plot.SeriesCollection(cnt).SeriesType = MSChart20Lib.VtChSeriesType.VtChSeriesType2dLine
                        .Plot.SeriesCollection(cnt).Pen.Width = 22
                        '.Plot.SeriesCollection(cnt).Pen.VtColor.Set(0, 0, 0)
                    End If
                Next

                .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).ValueScale.Minimum = graphMin - 10 '1
                .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).ValueScale.Maximum = graphMax + 10 '40
                .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).ValueScale.MajorDivision = 10
                .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).ValueScale.MinorDivision = 1
                .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).CategoryScale.DivisionsPerLabel = 1
                .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).CategoryScale.DivisionsPerTick = 1

                .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdX).ValueScale.MajorDivision = 10
                .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdX).ValueScale.MinorDivision = 1
                '.Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdX).AxisTitle.VtFont.Size = 1

                .Plot.SeriesCollection(3).SeriesType = MSChart20Lib.VtChSeriesType.VtChSeriesType2dLine
                ' .Plot.SeriesCollection(3).SeriesMarker.Auto = True
                .Plot.SeriesCollection(3).Select()
                .Plot.SeriesCollection(3).SeriesMarker.Show = True
                .Plot.SeriesCollection(3).ShowLine = False
                .Plot.SeriesCollection(3).Pen.Style = MSChart20Lib.VtPenStyle.VtPenStyleDashDotDot
                .Plot.SeriesCollection(3).Pen.Width = 6

                Dim yrs As Integer
                Dim reminder As Integer

                For i = 1 To .RowCount
                    .Row = i
                    yrs = AgeCollection(i - 1, 1) / 12
                    reminder = AgeCollection(i - 1, 1) Mod 12

                    If reminder = 0 Then
                        .RowLabel = yrs & "Yrs"
                    Else
                        .RowLabel = AgeCollection(i - 1, 1)
                    End If
                Next

            End With
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub
    ''' <summary>
    ''' Weight Vs Height
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub WeightHeight()

        '''' Retrive Patient Vital records
        Dim oclsViewGraphs As New clsViewGraphs
        dt = New DataTable
        dt = oclsViewGraphs.ScanAgeHtWt(_PatientID)

        '''' Get patient Gender
        Dim nGender As New Integer
        ' Dim oclsHPITemplate As New clsHPITemplate
        nGender = GetPatientGender(strPatientGender)
        ' oclsHPITemplate = Nothing

        '''' Get Patient Standard data
        Dim dt_MINMAX As New DataTable
        If nGender = 1 Then ' For male
            dt_MINMAX = oclsViewGraphs.getminmaxvaluesWtHt(Gender.Male)
        ElseIf nGender = 2 Then ' For female
            dt_MINMAX = oclsViewGraphs.getminmaxvaluesWtHt(Gender.Female)
        Else ' For other
            dt_MINMAX = oclsViewGraphs.getminmaxvaluesWtHt(Gender.Male)
        End If

        'view data in Datagrid
        DataGrid1.DataSource = dt_MINMAX

        Dim i As Integer '= 20
        Dim AgeCollection(dt_MINMAX.Rows.Count - 1, 2) As String
        Dim arrWeightVsHeight(dt_MINMAX.Rows.Count - 1, 12) As String
        Dim arrGraph(dt_MINMAX.Rows.Count - 1, 3) As String


        Dim strPatientInfo As String = ""

        Try
            With chtPatientGraphs
                ''''Set Graph Properties
                strPatientInfo = Space(5) & " Patient Code:" & Space(1) & gloUC_PatientStrip1.PatientCode & Space(5) & "Patient Name:" & Space(1) & gloUC_PatientStrip1.PatientName & Space(5) & "DOB:" & Space(1) & gloUC_PatientStrip1.PatientDateOfBirth & Space(5) & "Phone No.:" & Space(1) & gloUC_PatientStrip1.PatientPhone & Space(5)
                .Footnote.Location.LocationType = MSChart20Lib.VtChLocationType.VtChLocationTypeTop
                .Footnote.Text = strPatientInfo
                .TitleText = "Weight Vs Height"
                .chartType = MSChart20Lib.VtChChartType.VtChChartType2dCombination


                If dt_MINMAX.Rows.Count >= 1 Then
                    DataGrid1.DataSource = Nothing
                    DataGrid1.DataSource = dt
                    'Axis Labels for the Graph
                    chtPatientGraphs.Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdX).AxisTitle.Text = "Height (cm)"
                    chtPatientGraphs.Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).AxisTitle.Text = "Weight (lbs)"

                    '''' Fill Array with Standard Vital Records 
                    For i = 0 To dt_MINMAX.Rows.Count - 1  'dt.Rows.Count - 1
                        arrWeightVsHeight(i, 1) = dt_MINMAX.Rows(i)("Length") '* 0.394
                        arrWeightVsHeight(i, 11) = dt_MINMAX.Rows(i)("P3") * 2.204
                        arrWeightVsHeight(i, 3) = dt_MINMAX.Rows(i)("P5") * 2.204
                        arrWeightVsHeight(i, 4) = dt_MINMAX.Rows(i)("P10") * 2.204
                        arrWeightVsHeight(i, 5) = dt_MINMAX.Rows(i)("P25") * 2.204
                        arrWeightVsHeight(i, 6) = dt_MINMAX.Rows(i)("P50") * 2.204
                        arrWeightVsHeight(i, 7) = dt_MINMAX.Rows(i)("P75") * 2.204
                        arrWeightVsHeight(i, 8) = dt_MINMAX.Rows(i)("P90") * 2.204
                        arrWeightVsHeight(i, 9) = dt_MINMAX.Rows(i)("P95") * 2.204
                        arrWeightVsHeight(i, 10) = dt_MINMAX.Rows(i)("P97") * 2.204

                        AgeCollection(i, 1) = dt_MINMAX.Rows(i)("Length") ' * 0.394
                    Next
                Else
                    MessageBox.Show("No vital data available for this patient.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If

                'Fill For PAtient
                Dim Count As Integer
                ' Count = arrsales.Length / arrsales.GetLength(1)
                Count = dt_MINMAX.Rows.Count
                For i = 0 To Count - 1
                    For j As Integer = 0 To dt.Rows.Count - 1
                        Dim ft As Decimal
                        Dim Inch As Decimal
                        Dim temp() As String
                        ' data convert from ftInch to in points i.e. 1 ft 6 Inch is 1.5 Ft
                        If Not IsDBNull(dt.Rows(j)("sHeight")) Then
                            temp = GetFtInch(dt.Rows(j)("sHeight"))
                        Else
                            temp = GetFtInch(0)
                        End If

                        '''' Only Inch is available then
                        If temp(0).Trim <> "" Then
                            ft = Convert.ToDecimal(CType(temp(0), Object))
                        Else
                            ft = 0
                        End If
                        If temp.Length > 1 Then
                            If temp(1).Trim <> "" Then
                                Inch = Convert.ToDecimal(CType(temp(1), Object))
                            Else
                                Inch = 0
                            End If
                        End If
                        'If arrsales(i, 1) <= FtToMtr(ft, Inch) And (FtToMtr(ft, Inch) + 1.0) >= arrsales(i, 1) Then
                        '    '''' If Age Matches then Add Patients Ht in to The Array 
                        '    If Not IsDBNull(dt.Rows(j)("dweightinlbs")) Then
                        '        arrsales(i, 2) = dt.Rows(j)("dweightinlbs") ' dt.Rows(j)("sHeight") '
                        '    Else
                        '        arrsales(i, 2) = 0
                        '    End If
                        '    Exit For
                        'End If
                        Dim ht1, ht2 As Double
                        ht1 = ht2 = 0.0

                        ht1 = arrWeightVsHeight(i, 1)
                        If i = 0 Or arrWeightVsHeight(i, 1) = 45 Then
                            ht2 = arrWeightVsHeight(i, 1) + 0.5
                        Else
                            ht2 = arrWeightVsHeight(i, 1) + 1.0
                        End If

                        Dim ht_incms As Double
                        ht_incms = FtToMtr(ft, Inch)
                        If ht_incms > ht1 And ht_incms <= ht2 Then
                            If Not IsDBNull(dt.Rows(j)("dweightinlbs")) Then
                                arrWeightVsHeight(i, 2) = dt.Rows(j)("dweightinlbs") ' dt.Rows(j)("sHeight") '
                            Else
                                arrWeightVsHeight(i, 2) = 0
                            End If
                            Exit For
                        End If


                    Next
                    arrWeightVsHeight(i, 1) = Nothing
                Next

                'set data to draw chart
                .ChartData = CType(arrWeightVsHeight, Object)

                ' set graph styles for the patient vital entries.
                .Plot.SeriesCollection(3).SeriesType = MSChart20Lib.VtChSeriesType.VtChSeriesType2dLine
                .Plot.SeriesCollection(3).Select()
                .Plot.SeriesCollection(3).ShowLine = False
                .Plot.SeriesCollection(3).DataPoints(-1).Marker.Style = MSChart20Lib.VtMarkerStyle.VtMarkerStyleDiamond
                .Plot.SeriesCollection(3).DataPoints(-1).Marker.Visible = True
                .Plot.SeriesCollection(3).DataPoints(-1).Marker.FillColor.Set(255, 0, 0)
                .Plot.SeriesCollection(3).DataPoints(-1).Brush.Style = MSChart20Lib.VtBrushStyle.VtBrushStyleSolid
                .Plot.SeriesCollection(3).DataPoints(-1).Marker.Size = 20
                .Plot.SeriesCollection(3).Pen.Width = 20
                .Plot.SeriesCollection(3).DataPoints(-1).Brush.FillColor.Set(255, 0, 0)

                ' set the display properties of the graphs curve.
                Dim cnt As Integer
                For cnt = 1 To 12
                    If cnt <> 3 Then
                        .Plot.SeriesCollection(cnt).SeriesType = MSChart20Lib.VtChSeriesType.VtChSeriesType2dLine
                        .Plot.SeriesCollection(cnt).Pen.Width = 22
                        '.Plot.SeriesCollection(cnt).Pen.VtColor.Set(0, 0, 0)
                    End If
                Next

                ' set the graph properties for the y axis.
                .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).ValueScale.Minimum = 1
                .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).ValueScale.Maximum = 45
                .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).ValueScale.MajorDivision = 10
                .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).ValueScale.MinorDivision = 1
                .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).CategoryScale.DivisionsPerLabel = 1
                .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).CategoryScale.DivisionsPerTick = 1

                ' set the properties for the x axis.
                .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdX).ValueScale.MajorDivision = 10
                .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdX).ValueScale.MinorDivision = 10

                'set the label of x axis.
                '  Dim yrs, reminder

                For i = 1 To .RowCount
                    .Row = i
                    If AgeCollection(i - 1, 1) <> Nothing OrElse AgeCollection(i - 1, 1) = "" Then
                        .RowLabel = AgeCollection(i - 1, 1)
                    Else
                        .RowLabel = "" 'AgeCollection(i - 1, 1)
                    End If

                Next

            End With
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub
    Private Sub AgeCircumfranse()
        'lblMin.Text = "Minimum Circumfrance"
        'lblMax.Text = "Maximum Circumfrance"
        'lblPatients.Text = "Patient Circumference"

        Dim oclsViewGraphs As New clsViewGraphs
        dt = New DataTable
        dt = oclsViewGraphs.ScanAgeHtWt(_PatientID)

        ' get patient Gender
        Dim nGender As New Integer

        nGender = GetPatientGender(strPatientGender)
        ' chtPatientGraphs.ToDefaults()
        Dim dt_MINMAX As New DataTable

        'get STANDARD values for the minmax values from SP 'gsp_viewGraphMinMax' where standard data is stored.
        If nGender = 1 Then ' for male
            dt_MINMAX = oclsViewGraphs.GetMinMaxValues(Gender.Male, GraphType.HeadCircumfarance)
        ElseIf nGender = 2 Then ' for female
            dt_MINMAX = oclsViewGraphs.GetMinMaxValues(Gender.Female, GraphType.HeadCircumfarance)
        Else ' for other
            dt_MINMAX = oclsViewGraphs.GetMinMaxValues(Gender.Male, GraphType.HeadCircumfarance)
        End If

        'view data in Datagrid
        DataGrid1.DataSource = dt_MINMAX

        Dim i As Integer '= 20
        ' string data where patient Age collection when vital record enterd.
        ''Dim AgeCollection(dt.Rows.Count - 1, 2) As String
        Dim AgeCollection(dt_MINMAX.Rows.Count - 1, 2) As String

        ' string for the data where hight of the patient is collected.
        Dim arrsales(dt_MINMAX.Rows.Count - 1, 12) As String

        Dim arrGraph(dt_MINMAX.Rows.Count - 1, 3) As String

        Dim strPatientInfo As String = Space(5) & " Patient Code:" & Space(1) & gloUC_PatientStrip1.PatientCode & Space(5) & "Patient Name:" & Space(1) & gloUC_PatientStrip1.PatientName & Space(5) & "DOB:" & Space(1) & gloUC_PatientStrip1.PatientDateOfBirth & Space(5) & "Phone No.:" & Space(1) & gloUC_PatientStrip1.PatientPhone & Space(5)
        chtPatientGraphs.Footnote.Location.LocationType = MSChart20Lib.VtChLocationType.VtChLocationTypeTop


        chtPatientGraphs.Footnote.Text = strPatientInfo

        chtPatientGraphs.TitleText = "Age Vs Circumference"

        Try
            With chtPatientGraphs
                .chartType = MSChart20Lib.VtChChartType.VtChChartType2dCombination
                '.chartType = MSChart20Lib.VtChChartType.VtChChartType2dLine
                If dt_MINMAX.Rows.Count >= 1 Then
                    DataGrid1.DataSource = Nothing
                    DataGrid1.DataSource = dt
                    'Axis Labels for the Graph
                    chtPatientGraphs.Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdX).AxisTitle.Text = "Age (Months)"
                    chtPatientGraphs.Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).AxisTitle.Text = "Head Circumference(cm)"

                    ' get the total number of the vital entries for the patients
                    For i = 0 To dt_MINMAX.Rows.Count - 1  'dt.Rows.Count - 1
                        ' get gender and assign standard Height to the array string
                        arrsales(i, 1) = dt_MINMAX.Rows(i)("Agemos")
                        arrsales(i, 11) = dt_MINMAX.Rows(i)("P3")
                        arrsales(i, 3) = dt_MINMAX.Rows(i)("P5")
                        arrsales(i, 4) = dt_MINMAX.Rows(i)("P10")
                        arrsales(i, 5) = dt_MINMAX.Rows(i)("P25")
                        arrsales(i, 6) = dt_MINMAX.Rows(i)("P50")
                        arrsales(i, 7) = dt_MINMAX.Rows(i)("P75")
                        arrsales(i, 8) = dt_MINMAX.Rows(i)("P90")
                        arrsales(i, 9) = dt_MINMAX.Rows(i)("P95")
                        arrsales(i, 10) = dt_MINMAX.Rows(i)("P97")

                        AgeCollection(i, 1) = dt_MINMAX.Rows(i)("Agemos")
                    Next
                Else
                    MessageBox.Show("No vital data available for this patient.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If

                'Fill For PAtient
                Dim Count As Integer
                'Count = arrsales.Length / arrsales.GetLength(1)
                Count = dt_MINMAX.Rows.Count
                For i = 0 To Count - 1
                    For j As Integer = 0 To dt.Rows.Count - 1
                        If arrsales(i, 1) = dt.Rows(j)("AGE") Then
                            '''' If Age Matches then Add Patients Ht in to The Array 
                            If Not IsDBNull(dt.Rows(j)("dHeadCircumferance")) Then
                                arrsales(i, 2) = dt.Rows(j)("dHeadCircumferance") '/ (0.45) 'FtToMtr(ft, Inch)
                            Else
                                arrsales(i, 2) = 0
                            End If

                            Exit For
                        End If
                    Next
                    arrsales(i, 1) = Nothing
                Next

                'set data to draw chart
                .ChartData = CType(arrsales, Object)
                ' set graph styles   
                '.Plot.Backdrop.Fill.Brush.PatternColor.Set(255, 255, 255)

                Dim cnt As Integer
                For cnt = 1 To 12
                    If cnt <> 3 Then
                        .Plot.SeriesCollection(cnt).SeriesType = MSChart20Lib.VtChSeriesType.VtChSeriesType2dLine
                        .Plot.SeriesCollection(cnt).Pen.Width = 22
                        '.Plot.SeriesCollection(cnt).Pen.VtColor.Set(0, 0, 0)
                    End If
                Next

                ' set the chart controls properties for the display.
                ' for y axis
                .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).ValueScale.Minimum = 32
                .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).ValueScale.Maximum = 53
                .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).ValueScale.MajorDivision = 10
                .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).ValueScale.MinorDivision = 1
                .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).CategoryScale.DivisionsPerLabel = 1
                .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).CategoryScale.DivisionsPerTick = 1

                ' for x axis
                .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdX).ValueScale.MajorDivision = 10
                .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdX).ValueScale.MinorDivision = 1

                ' set the properties for the column where data is stored of patients i.e. patient vital entries.
                .Plot.SeriesCollection(3).SeriesType = MSChart20Lib.VtChSeriesType.VtChSeriesType2dLine
                ' .Plot.SeriesCollection(3).SeriesMarker.Auto = True
                .Plot.SeriesCollection(3).Select()
                .Plot.SeriesCollection(3).SeriesMarker.Show = True
                .Plot.SeriesCollection(3).ShowLine = False
                .Plot.SeriesCollection(3).Pen.Style = MSChart20Lib.VtPenStyle.VtPenStyleSolid
                .Plot.SeriesCollection(3).Pen.Width = 6

                ' print labels on the x axis
                For i = 1 To .RowCount
                    .Row = i
                    .RowLabel = AgeCollection(i - 1, 1)
                Next

            End With
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub AgeHeight()
        'lblMin.Text = "Minimum Height"
        'lblMax.Text = "Maximum Height"
        'lblPatients.Text = "Patient Height"
        Dim oclsViewGraphs As New clsViewGraphs
        dt = New DataTable
        dt = oclsViewGraphs.ScanAgeHtWt(_PatientID)
        ' get patient Gender
        Dim nGender As New Integer

        nGender = GetPatientGender(strPatientGender)
        Dim dt_MINMAX As New DataTable

        'get STANDARD values for the minmax values from SP 'gsp_viewGraphMinMax' where standard data is stored.
        If nGender = 1 Then ' for male
            dt_MINMAX = oclsViewGraphs.GetMinMaxValues(Gender.Male, GraphType.Height)
        ElseIf nGender = 2 Then ' for female
            dt_MINMAX = oclsViewGraphs.GetMinMaxValues(Gender.Female, GraphType.Height)
        Else ' for other
            dt_MINMAX = oclsViewGraphs.GetMinMaxValues(Gender.Male, GraphType.Height)
        End If

        'view data in Datagrid
        DataGrid1.DataSource = dt_MINMAX

        Dim i As Integer '= 20
        ' string data where patient Age collection when vital record enterd.
        ''Dim AgeCollection(dt.Rows.Count - 1, 2) As String
        Dim AgeCollection(dt_MINMAX.Rows.Count - 1, 2) As String

        ' string for the data where hight of the patient is collected.
        Dim arrsales(dt_MINMAX.Rows.Count - 1, 12) As String

        Dim arrGraph(dt_MINMAX.Rows.Count - 1, 3) As String

        Dim strPatientInfo As String = Space(5) & " Patient Code:" & Space(1) & gloUC_PatientStrip1.PatientCode & Space(5) & "Patient Name:" & Space(1) & gloUC_PatientStrip1.PatientName & Space(5) & "DOB:" & Space(1) & gloUC_PatientStrip1.PatientDateOfBirth & Space(5) & "Phone No.:" & Space(1) & gloUC_PatientStrip1.PatientPhone & Space(5)
        chtPatientGraphs.Footnote.Location.LocationType = MSChart20Lib.VtChLocationType.VtChLocationTypeTop
        chtPatientGraphs.Footnote.Text = strPatientInfo

        chtPatientGraphs.TitleText = "Age Vs Height"

        Try
            With chtPatientGraphs
                .chartType = MSChart20Lib.VtChChartType.VtChChartType2dCombination
                '.chartType = MSChart20Lib.VtChChartType.VtChChartType2dLine
                If dt_MINMAX.Rows.Count >= 1 Then
                    DataGrid1.DataSource = Nothing
                    DataGrid1.DataSource = dt
                    'Axis Labels for the Graph
                    chtPatientGraphs.Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdX).AxisTitle.Text = "Age (Months)"
                    chtPatientGraphs.Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).AxisTitle.Text = "Height (inch)"

                    ' get the total number of the vital entries for the patients
                    ''''Pramod
                    'For i = 1 To dt_MINMAX.Rows.Count - 1  'dt.Rows.Count - 1
                    For i = 0 To dt_MINMAX.Rows.Count - 1  'dt.Rows.Count - 1
                        ' get gender and assign standard Height to the array string
                        arrsales(i, 1) = dt_MINMAX.Rows(i)("Agemos")
                        arrsales(i, 11) = dt_MINMAX.Rows(i)("P3") * 0.394
                        arrsales(i, 3) = dt_MINMAX.Rows(i)("P5") * 0.394
                        arrsales(i, 4) = dt_MINMAX.Rows(i)("P10") * 0.394
                        arrsales(i, 5) = dt_MINMAX.Rows(i)("P25") * 0.394
                        arrsales(i, 6) = dt_MINMAX.Rows(i)("P50") * 0.394
                        arrsales(i, 7) = dt_MINMAX.Rows(i)("P75") * 0.394
                        arrsales(i, 8) = dt_MINMAX.Rows(i)("P90") * 0.394
                        arrsales(i, 9) = dt_MINMAX.Rows(i)("P95") * 0.394
                        arrsales(i, 10) = dt_MINMAX.Rows(i)("P97") * 0.394

                        AgeCollection(i, 1) = dt_MINMAX.Rows(i)("Agemos")
                    Next
                Else
                    MessageBox.Show("No vital data available for this patient.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If

                'Fill For PAtient
                Dim Count As Integer
                '     Count = arrsales.Length / arrsales.GetLength(1)
                Count = dt_MINMAX.Rows.Count

                For i = 0 To Count - 1
                    For j As Integer = 0 To dt.Rows.Count - 1
                        If arrsales(i, 1) = dt.Rows(j)("AGE") Then
                            '' If Age Matches then Add Patients Ht in to The Array
                            Dim ft As Decimal
                            Dim Inch As Decimal
                            Dim temp() As String
                            ' data convert from ftInch to in points i.e. 1 ft 6 Inch is 1.5 Ft
                            If Not IsDBNull(dt.Rows(j)("sHeight")) Then
                                temp = GetFtInch(dt.Rows(j)("sHeight"))
                            Else
                                temp = GetFtInch(0)
                            End If

                            '''' Only Inch is available then
                            If temp(0).Trim <> "" Then
                                ft = Convert.ToDecimal(CType(temp(0), Object))
                            Else
                                ft = 0
                            End If
                            If temp.Length > 1 Then


                                If temp(1).Trim <> "" Then
                                    Inch = Convert.ToDecimal(CType(temp(1), Object))
                                Else
                                    Inch = 0
                                End If
                            End If
                            '''' If Age Matches then Add Patients Ht in to The Array 
                            arrsales(i, 2) = FtToMtr(ft, Inch) * 0.394 ' dt.Rows(j)("sHeight") '
                            Exit For
                        End If
                    Next
                    arrsales(i, 1) = Nothing
                Next

                'set data to draw chart
                .ChartData = CType(arrsales, Object)

                ' set graph styles for the patient vital entries.
                .Plot.SeriesCollection(3).SeriesType = MSChart20Lib.VtChSeriesType.VtChSeriesType2dLine
                .Plot.SeriesCollection(3).Select()
                '.Plot.SeriesCollection(3).SeriesMarker.Show = True
                .Plot.SeriesCollection(3).ShowLine = False
                .Plot.SeriesCollection(3).DataPoints(-1).Marker.Style = MSChart20Lib.VtMarkerStyle.VtMarkerStyleFilledCircle
                .Plot.SeriesCollection(3).DataPoints(-1).Marker.Visible = True
                .Plot.SeriesCollection(3).DataPoints(-1).Marker.FillColor.Set(255, 0, 0)
                '.Plot.SeriesCollection(3).DataPoints(-1).Marker.Pen.Style = MSChart20Lib.VtPenStyle.VtPenStyleDashDot
                .Plot.SeriesCollection(3).DataPoints(-1).Brush.Style = MSChart20Lib.VtBrushStyle.VtBrushStyleSolid
                .Plot.SeriesCollection(3).DataPoints(-1).Marker.Size = 20
                .Plot.SeriesCollection(3).Pen.Width = 20
                .Plot.SeriesCollection(3).DataPoints(-1).Brush.FillColor.Set(255, 0, 0)

                ' set the display properties of the graphs curve.
                Dim cnt As Integer
                For cnt = 1 To 12
                    If cnt <> 3 Then
                        .Plot.SeriesCollection(cnt).SeriesType = MSChart20Lib.VtChSeriesType.VtChSeriesType2dLine
                        .Plot.SeriesCollection(cnt).Pen.Width = 22
                        '.Plot.SeriesCollection(cnt).Pen.VtColor.Set(0, 0, 0)
                    End If
                Next
                'For cnt = 1 To 12
                '    If cnt = 1 Then
                '        .Plot.SeriesCollection(cnt).SeriesType = MSChart20Lib.VtChSeriesType.VtChSeriesType2dLine
                '        .Plot.SeriesCollection(cnt).Pen.Width = 22.49

                '        '.Plot.SeriesCollection(cnt).Pen.VtColor.Set(0, 0, 0)
                '    ElseIf cnt = 2 Then
                '        .Plot.SeriesCollection(cnt).SeriesType = MSChart20Lib.VtChSeriesType.VtChSeriesType2dLine
                '        .Plot.SeriesCollection(cnt).Pen.Width = 22.49

                '        '.Plot.SeriesCollection(cnt).Pen.VtColor.Set(0, 0, 0)
                '    ElseIf cnt = 4 Then
                '        .Plot.SeriesCollection(cnt).SeriesType = MSChart20Lib.VtChSeriesType.VtChSeriesType2dLine
                '        .Plot.SeriesCollection(cnt).Pen.Width = 22.49
                '        .Plot.SeriesCollection(cnt).Pen.VtColor.Set(255, 130, 130)

                '    ElseIf cnt = 5 Then
                '        .Plot.SeriesCollection(cnt).SeriesType = MSChart20Lib.VtChSeriesType.VtChSeriesType2dLine
                '        .Plot.SeriesCollection(cnt).Pen.Width = 22.49

                '        .Plot.SeriesCollection(cnt).Pen.VtColor.Set(255, 92, 92)
                '    ElseIf cnt = 6 Then
                '        .Plot.SeriesCollection(cnt).SeriesType = MSChart20Lib.VtChSeriesType.VtChSeriesType2dLine
                '        .Plot.SeriesCollection(cnt).Pen.Width = 22.49
                '        .Plot.SeriesCollection(cnt).Pen.VtColor.Set(255, 47, 47)

                '    ElseIf cnt = 7 Then
                '        .Plot.SeriesCollection(cnt).SeriesType = MSChart20Lib.VtChSeriesType.VtChSeriesType2dLine
                '        .Plot.SeriesCollection(cnt).Pen.Width = 22.49

                '        .Plot.SeriesCollection(cnt).Pen.VtColor.Set(255, 0, 0)
                '    ElseIf cnt = 8 Then
                '        .Plot.SeriesCollection(cnt).SeriesType = MSChart20Lib.VtChSeriesType.VtChSeriesType2dLine
                '        .Plot.SeriesCollection(cnt).Pen.Width = 22.49
                '        .Plot.SeriesCollection(cnt).Pen.VtColor.Set(215, 0, 0)
                '    ElseIf cnt = 9 Then
                '        .Plot.SeriesCollection(cnt).SeriesType = MSChart20Lib.VtChSeriesType.VtChSeriesType2dLine
                '        .Plot.SeriesCollection(cnt).Pen.Width = 22.49
                '        .Plot.SeriesCollection(cnt).Pen.VtColor.Set(170, 0, 0)
                '    ElseIf cnt = 10 Then
                '        .Plot.SeriesCollection(cnt).SeriesType = MSChart20Lib.VtChSeriesType.VtChSeriesType2dLine
                '        .Plot.SeriesCollection(cnt).Pen.Width = 22.49
                '        .Plot.SeriesCollection(cnt).Pen.VtColor.Set(120, 0, 0)
                '    ElseIf cnt = 11 Then
                '        .Plot.SeriesCollection(cnt).SeriesType = MSChart20Lib.VtChSeriesType.VtChSeriesType2dLine
                '        .Plot.SeriesCollection(cnt).Pen.Width = 22.49
                '        .Plot.SeriesCollection(cnt).Pen.VtColor.Set(92, 0, 0)
                '    ElseIf cnt = 12 Then
                '        .Plot.SeriesCollection(cnt).SeriesType = MSChart20Lib.VtChSeriesType.VtChSeriesType2dLine
                '        .Plot.SeriesCollection(cnt).Pen.Width = 22.49
                '        .Plot.SeriesCollection(cnt).Pen.VtColor.Set(255, 191, 191)
                '    End If
                'Next
                ' set the graph properties for the y axis.
                .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).ValueScale.Minimum = 18
                .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).ValueScale.Maximum = 42
                .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).ValueScale.MajorDivision = 10
                .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).ValueScale.MinorDivision = 1
                .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).CategoryScale.DivisionsPerLabel = 1
                .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).CategoryScale.DivisionsPerTick = 1

                ' set the properties for the x axis.
                .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdX).ValueScale.MajorDivision = 10
                .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdX).ValueScale.MinorDivision = 10

                'set the label of x axis.
                For i = 1 To .RowCount
                    .Row = i
                    .RowLabel = AgeCollection(i - 1, 1)
                Next

            End With
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub AgeWeight()
        'lblMin.Text = "Minimum Weight"
        'lblMax.Text = "Maximum Weight"
        'lblPatients.Text = "Patient Weight"
        Dim oclsViewGraphs As New clsViewGraphs
        dt = New DataTable
        dt = oclsViewGraphs.ScanAgeHtWt(_PatientID)

        ' get patient Gender
        Dim nGender As New Integer

        nGender = GetPatientGender(strPatientGender)

        Dim dt_MINMAX As New DataTable

        'get STANDARD values for the minmax values from SP 'gsp_viewGraphMinMax' where standard data is stored.
        If nGender = 1 Then ' for male
            dt_MINMAX = oclsViewGraphs.GetMinMaxValues(Gender.Male, GraphType.Weight)
        ElseIf nGender = 2 Then ' for female
            dt_MINMAX = oclsViewGraphs.GetMinMaxValues(Gender.Female, GraphType.Weight)
        Else ' for other
            dt_MINMAX = oclsViewGraphs.GetMinMaxValues(Gender.Male, GraphType.Weight)
        End If

        'view data in Datagrid
        DataGrid1.DataSource = dt_MINMAX

        Dim i As Integer '= 20
        ' string data where patient Age collection when vital record enterd.
        ''Dim AgeCollection(dt.Rows.Count - 1, 2) As String
        Dim AgeCollection(dt_MINMAX.Rows.Count - 1, 2) As String

        ' string for the data where hight of the patient is collected.
        Dim arrsales(dt_MINMAX.Rows.Count - 1, 12) As String

        Dim arrGraph(dt_MINMAX.Rows.Count - 1, 3) As String

        Dim strPatientInfo As String = Space(5) & " Patient Code:" & Space(1) & gloUC_PatientStrip1.PatientCode & Space(5) & "Patient Name:" & Space(1) & gloUC_PatientStrip1.PatientName & Space(5) & "DOB:" & Space(1) & gloUC_PatientStrip1.PatientDateOfBirth & Space(5) & "Phone No.:" & Space(1) & gloUC_PatientStrip1.PatientPhone & Space(5)
        chtPatientGraphs.Footnote.Location.LocationType = MSChart20Lib.VtChLocationType.VtChLocationTypeTop
        chtPatientGraphs.Footnote.Text = strPatientInfo

        chtPatientGraphs.TitleText = "Age Vs Weight"

        Try
            With chtPatientGraphs
                .chartType = MSChart20Lib.VtChChartType.VtChChartType2dCombination
                '.chartType = MSChart20Lib.VtChChartType.VtChChartType2dLine
                If dt_MINMAX.Rows.Count >= 1 Then
                    DataGrid1.DataSource = Nothing
                    DataGrid1.DataSource = dt
                    'Axis Labels for the Graph
                    chtPatientGraphs.Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdX).AxisTitle.Text = "Age (Months)"
                    chtPatientGraphs.Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).AxisTitle.Text = "Weight (lbs)"

                    ' get the total number of the vital entries for the patients
                    '''' Commented By Bipin ' 20070330
                    'For i = 1 To 36 ' dt_MINMAX.Rows.Count - 1  'dt.Rows.Count - 1
                    '   For i = 1 To 37 ' dt_MINMAX.Rows.Count - 1  'dt.Rows.Count - 1
                    For i = 0 To dt_MINMAX.Rows.Count - 1  'dt.Rows.Count - 1
                        ' get gender and assign standard Height to the array string
                        arrsales(i, 1) = dt_MINMAX.Rows(i)("Agemos")
                        arrsales(i, 11) = dt_MINMAX.Rows(i)("P3") / (0.45)
                        arrsales(i, 3) = dt_MINMAX.Rows(i)("P5") / (0.45)
                        arrsales(i, 4) = dt_MINMAX.Rows(i)("P10") / (0.45)
                        arrsales(i, 5) = dt_MINMAX.Rows(i)("P25") / (0.45)
                        arrsales(i, 6) = dt_MINMAX.Rows(i)("P50") / (0.45)
                        arrsales(i, 7) = dt_MINMAX.Rows(i)("P75") / (0.45)
                        arrsales(i, 8) = dt_MINMAX.Rows(i)("P90") / (0.45)
                        arrsales(i, 9) = dt_MINMAX.Rows(i)("P95") / (0.45)
                        arrsales(i, 10) = dt_MINMAX.Rows(i)("P97") / (0.45)

                        AgeCollection(i, 1) = dt_MINMAX.Rows(i)("Agemos")
                    Next
                Else
                    MessageBox.Show("No vital data available for this patient.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If

                'Fill For PAtient
                Dim Count As Integer
                '   Count = arrsales.Length / arrsales.GetLength(1)
                Count = dt_MINMAX.Rows.Count

                For i = 0 To Count - 1
                    For j As Integer = 0 To dt.Rows.Count - 1
                        If arrsales(i, 1) = dt.Rows(j)("AGE") Then
                            '''' If Age Matches then Add Patients Ht in to The Array 
                            If Not IsDBNull(dt.Rows(j)("dWeightinlbs")) Then
                                arrsales(i, 2) = dt.Rows(j)("dWeightinlbs") '/ (0.45) 'FtToMtr(ft, Inch)
                            Else
                                arrsales(i, 2) = 0
                            End If
                            Exit For
                        End If
                    Next
                    arrsales(i, 1) = Nothing
                Next

                'set data to draw chart
                .ChartData = CType(arrsales, Object)
                ' set graph styles   
                '.Plot.Backdrop.Fill.Brush.PatternColor.Set(255, 255, 255)               

                Dim cnt As Integer
                For cnt = 1 To 12
                    If cnt <> 3 Then
                        .Plot.SeriesCollection(cnt).SeriesType = MSChart20Lib.VtChSeriesType.VtChSeriesType2dLine
                        .Plot.SeriesCollection(cnt).Pen.Width = 22
                        '.Plot.SeriesCollection(cnt).Pen.VtColor.Set(0, 0, 0)
                    End If
                Next

                .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).ValueScale.Minimum = 1
                .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).ValueScale.Maximum = 40
                .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).ValueScale.MajorDivision = 10
                .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).ValueScale.MinorDivision = 1
                .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).CategoryScale.DivisionsPerLabel = 1
                .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).CategoryScale.DivisionsPerTick = 1

                .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdX).ValueScale.MajorDivision = 10
                .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdX).ValueScale.MinorDivision = 1
                '.Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdX).AxisTitle.VtFont.Size = 1


                .Plot.SeriesCollection(3).SeriesType = MSChart20Lib.VtChSeriesType.VtChSeriesType2dLine
                ' .Plot.SeriesCollection(3).SeriesMarker.Auto = True
                .Plot.SeriesCollection(3).Select()
                .Plot.SeriesCollection(3).SeriesMarker.Show = True
                .Plot.SeriesCollection(3).ShowLine = False
                .Plot.SeriesCollection(3).Pen.Style = MSChart20Lib.VtPenStyle.VtPenStyleDashDotDot
                .Plot.SeriesCollection(3).Pen.Width = 6

                For i = 1 To .RowCount
                    .Row = i
                    .RowLabel = AgeCollection(i - 1, 1)
                Next

            End With
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub
    Private Sub AgeVsTemprature()

        Dim oclsViewGraphs As New clsViewGraphs
        dt = New DataTable
        '''' Fill DataTable With Patient Vitals Records
        dt = oclsViewGraphs.ScanAgeHtWt(_PatientID)

        Dim i As Integer '= 20

        ' string data where patient Age collection when vital record enterd.
        Dim AgeCollection(dt.Rows.Count - 1, 2) As String

        ' string for the data where hight of the patient is collected.
        Dim arrsales(dt.Rows.Count - 1, 12) As String

        Dim strPatientInfo As String = Space(5) & " Patient Code:" & Space(1) & gloUC_PatientStrip1.PatientCode & Space(5) & "Patient Name:" & Space(1) & gloUC_PatientStrip1.PatientName & Space(5) & "DOB:" & Space(1) & gloUC_PatientStrip1.PatientDateOfBirth & Space(5) & "Phone No.:" & Space(1) & gloUC_PatientStrip1.PatientPhone & Space(5)
        chtPatientGraphs.Footnote.Location.LocationType = MSChart20Lib.VtChLocationType.VtChLocationTypeTop
        chtPatientGraphs.Footnote.Text = strPatientInfo

        chtPatientGraphs.TitleText = "Date-Time Vs Temperature"

        Try
            With chtPatientGraphs
                '.chartType = MSChart20Lib.VtChChartType.VtChChartType2dCombination
                .chartType = MSChart20Lib.VtChChartType.VtChChartType2dLine
                If dt.Rows.Count >= 1 Then
                    'Axis Labels for the Graph
                    chtPatientGraphs.Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdX).AxisTitle.Text = "Date-Time"
                    chtPatientGraphs.Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).AxisTitle.Text = "Temperature"

                    ' get the total number of the vital entries for the patients
                    For i = 0 To dt.Rows.Count - 1  ' dt_MINMAX.Rows.Count - 1  'dt.Rows.Count - 1
                        ' get gender and assign standard Height to the array string
                        If Not IsDBNull(dt.Rows(i)("dtvitaldate")) Then
                            arrsales(i, 1) = Convert.ToString(dt.Rows(i)("dtvitaldate"))
                        Else
                            arrsales(i, 1) = 0
                        End If

                        If Not IsDBNull(dt.Rows(i)("dTemperature")) Then
                            arrsales(i, 2) = Convert.ToString(dt.Rows(i)("dTemperature"))
                        Else
                            arrsales(i, 2) = 0
                        End If

                        arrsales(i, 3) = 90 ' dt.Rows(i)("dTemperature")

                        If Not IsDBNull(dt.Rows(i)("dTemperature")) Then
                            arrsales(i, 4) = Convert.ToString(dt.Rows(i)("dTemperature")) ' 90 'dt.Rows(i)("dTemperature")
                        Else
                            arrsales(i, 4) = 0
                        End If

                        arrsales(i, 5) = 110 'dt.Rows(i)("dTemperature")

                        If Not IsDBNull(dt.Rows(i)("dTemperature")) Then
                            arrsales(i, 6) = Convert.ToString(dt.Rows(i)("dTemperature"))
                        Else
                            arrsales(i, 6) = 0
                        End If


                        If Not IsDBNull(dt.Rows(i)("dtvitaldate")) Then
                            AgeCollection(i, 1) = Convert.ToString(dt.Rows(i)("dtvitaldate"))
                        Else
                            AgeCollection(i, 1) = 0
                        End If

                    Next
                Else
                    MessageBox.Show("No vital data available for this patient.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Me.Close()
                    Exit Sub
                End If

                'set data to draw chart
                .ChartData = CType(arrsales, Object)
                ' set graph styles
                '.Plot.Backdrop.Fill.Brush.PatternColor.Set(255, 255, 255)
                Dim count_series As Integer = .Plot.SeriesCollection.Count()
                Dim cnt As Integer
                For cnt = 1 To dt.Rows.Count - 1
                    If cnt <> 3 Then
                        If cnt <= count_series Then
                            .Plot.SeriesCollection(cnt).SeriesType = MSChart20Lib.VtChSeriesType.VtChSeriesType2dLine
                            .Plot.SeriesCollection(cnt).Pen.Width = 25
                            '.Plot.SeriesCollection(cnt).Pen.VtColor.Set(0, 0, 0)
                        End If
                    End If
                Next

                .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).ValueScale.Minimum = 80
                .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).ValueScale.Maximum = 120
                .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).ValueScale.MajorDivision = 10
                .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).ValueScale.MinorDivision = 1
                .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).CategoryScale.DivisionsPerLabel = 1
                .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).CategoryScale.DivisionsPerTick = 1

                .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdX).ValueScale.MajorDivision = 10
                .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdX).ValueScale.MinorDivision = 1
                ''.Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdX).AxisTitle.VtFont.Size = 1

                .Plot.SeriesCollection(3).SeriesType = MSChart20Lib.VtChSeriesType.VtChSeriesType2dLine
                ' .Plot.SeriesCollection(3).SeriesMarker.Auto = True
                .Plot.SeriesCollection(3).Select()
                .Plot.SeriesCollection(3).SeriesMarker.Show = True
                .Plot.SeriesCollection(3).ShowLine = False
                .Plot.SeriesCollection(3).Pen.Style = MSChart20Lib.VtPenStyle.VtPenStyleDashDotDot
                .Plot.SeriesCollection(3).Pen.Width = 6


                ' fill the labels of Y-axis
                For i = 1 To .RowCount
                    .Row = i
                    .RowLabel = AgeCollection(i - 1, 1)
                Next

            End With
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    ' function for the BP Sitting and standing for minimum as well as maximum values.
    Private Function GraphsForBP(ByVal nBPvalue As Integer)
        'get data from SP for the selectd patient
        Dim oclsViewGraphs As New clsViewGraphs
        dt = New DataTable
        dt = oclsViewGraphs.ScanAgeHtWt(_PatientID)
        Dim i As Integer '= 20
        ' string data where patient Age collection when vital record enterd.
        Dim AgeCollection(dt.Rows.Count - 1, 2) As String
        ' string for the data where hight of the patient is collected.
        Dim arrsales(dt.Rows.Count - 1, 12) As String

        If nBPvalue = 1 Then
            Dim strPatientInfo As String = Space(5) & " Patient Code:" & Space(1) & gloUC_PatientStrip1.PatientCode & Space(5) & "Patient Name:" & Space(1) & gloUC_PatientStrip1.PatientName & Space(5) & "DOB:" & Space(1) & gloUC_PatientStrip1.PatientDateOfBirth & Space(5) & "Phone No.:" & Space(1) & gloUC_PatientStrip1.PatientPhone & Space(5)
            chtPatientGraphs.Footnote.Location.LocationType = MSChart20Lib.VtChLocationType.VtChLocationTypeTop
            chtPatientGraphs.Footnote.Text = strPatientInfo
            chtPatientGraphs.TitleText = "Date-Time Vs BP Sitting"
        Else
            Dim strPatientInfo As String = Space(5) & " Patient Code:" & Space(1) & gloUC_PatientStrip1.PatientCode & Space(5) & "Patient Name:" & Space(1) & gloUC_PatientStrip1.PatientName & Space(5) & "DOB:" & Space(1) & gloUC_PatientStrip1.PatientDateOfBirth & Space(5) & "Phone No.:" & Space(1) & gloUC_PatientStrip1.PatientPhone & Space(5)
            chtPatientGraphs.Footnote.Location.LocationType = MSChart20Lib.VtChLocationType.VtChLocationTypeTop
            chtPatientGraphs.Footnote.Text = strPatientInfo
            chtPatientGraphs.TitleText = "Date-Time Vs BP Standing"
        End If
        Try
            With chtPatientGraphs
                '.chartType = MSChart20Lib.VtChChartType.VtChChartType2dCombination
                .chartType = MSChart20Lib.VtChChartType.VtChChartType2dLine
                '.chartType = MSChart20Lib.VtChChartType.VtChChartType2dLine
                If dt.Rows.Count >= 1 Then
                    'Axis Labels for the Graph
                    chtPatientGraphs.Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdX).AxisTitle.Text = "Date-Time"

                    If nBPvalue = 1 Then
                        chtPatientGraphs.Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).AxisTitle.Text = "BP Sitting"
                    Else
                        chtPatientGraphs.Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).AxisTitle.Text = "BP Standing"
                    End If

                    ' get the total number of the vital entries for the patients
                    For i = 0 To dt.Rows.Count - 1  ' dt_MINMAX.Rows.Count - 1  'dt.Rows.Count - 1
                        ' get gender and assign standard Height to the array string
                        arrsales(i, 1) = Convert.ToString(dt.Rows(i)("dtvitaldate"))
                        'arrsales(i, 2) = dt.Rows(i)("dBloodPressureSittingMin")
                        ' standard Minimum values
                        arrsales(i, 3) = 20
                        'arrsales(i, 4) = dt.Rows(i)("dBloodPressureSittingMin")
                        If nBPvalue = 1 Then
                            If Not IsDBNull(dt.Rows(i)("dBloodPressureSittingMin")) Then
                                arrsales(i, 5) = Convert.ToString(dt.Rows(i)("dBloodPressureSittingMin"))
                            Else
                                arrsales(i, 5) = 0
                            End If

                            If Not IsDBNull(dt.Rows(i)("dBloodPressureSittingMax")) Then
                                arrsales(i, 6) = Convert.ToString(dt.Rows(i)("dBloodPressureSittingMax"))
                            Else
                                arrsales(i, 6) = 0
                            End If

                        Else
                            If Not IsDBNull(dt.Rows(i)("dBloodPressureStandingMin")) Then
                                arrsales(i, 5) = Convert.ToString(dt.Rows(i)("dBloodPressureStandingMin"))
                            Else
                                arrsales(i, 5) = 0
                            End If

                            If Not IsDBNull(dt.Rows(i)("dBloodPressureStandingMax")) Then
                                arrsales(i, 6) = Convert.ToString(dt.Rows(i)("dBloodPressureStandingMax"))
                            Else
                                arrsales(i, 6) = 0
                            End If

                        End If
                        ' standard Maximum values
                        arrsales(i, 7) = 220
                        If Not IsDBNull(dt.Rows(i)("dtvitaldate")) Then
                            AgeCollection(i, 1) = Convert.ToString(dt.Rows(i)("dtvitaldate"))
                        Else
                            AgeCollection(i, 1) = 0
                        End If

                    Next
                Else
                    MessageBox.Show("No vital data available for this patient.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    GraphsForBP = ""
                    Exit Function
                End If

                'set data to draw chart
                .ChartData = CType(arrsales, Object)
                Dim Count_series As Integer = .Plot.SeriesCollection.Count()
                Dim cnt As Integer
                For cnt = 1 To dt.Rows.Count - 1
                    ' If cnt <> 3 Or cnt <> 4 Then
                    If cnt <= Count_series Then
                        .Plot.SeriesCollection(cnt).SeriesType = MSChart20Lib.VtChSeriesType.VtChSeriesType2dLine
                        .Plot.SeriesCollection(cnt).Pen.Width = 20
                        '.Plot.SeriesCollection(cnt).Pen.VtColor.Set(0, 0, 0)
                        '  End If
                    End If
                Next

                .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).ValueScale.Minimum = 15
                .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).ValueScale.Maximum = 225
                .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).ValueScale.MajorDivision = 10
                .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).ValueScale.MinorDivision = 1
                .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).CategoryScale.DivisionsPerLabel = 1
                .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).CategoryScale.DivisionsPerTick = 1

                .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdX).ValueScale.MajorDivision = 10
                .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdX).ValueScale.MinorDivision = 1

                ' fill the labels of Y-axis
                For i = 1 To .RowCount
                    .Row = i
                    .RowLabel = AgeCollection(i - 1, 1)
                Next

            End With
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        GraphsForBP = ""
    End Function

    '----------------code added by sarika vital Graphs 3rd june 08

    '' SUDHIR - 20090225 ''
    Private Sub DateVsWeight()
        'get data from SP for the selectd patient
        Dim oclsViewGraphs As New clsViewGraphs
        Dim nMaxVal As Int64 = 0
        Dim nMinVal As Int64 = 0
        'Dim dv As DataView
        'dt = New DataTable
        dt = oclsViewGraphs.ScanAgeHtWt(_PatientID)
        Dim i As Integer '= 20
        ' string data where patient Age collection when vital record enterd.
        Dim AgeCollection(dt.Rows.Count - 1, 2) As String
        ' string for the data where hight of the patient is collected.
        Dim arrsales(dt.Rows.Count - 1, 12) As String

        Dim strPatientInfo As String = Space(5) & " Patient Code:" & Space(1) & gloUC_PatientStrip1.PatientCode & Space(5) & "Patient Name:" _
                & Space(1) & gloUC_PatientStrip1.PatientName & Space(5) & "DOB:" & Space(1) & gloUC_PatientStrip1.PatientDateOfBirth & Space(5) & "Phone No.:" _
                & Space(1) & gloUC_PatientStrip1.PatientPhone & Space(5)

        chtPatientGraphs.Footnote.Location.LocationType = MSChart20Lib.VtChLocationType.VtChLocationTypeTop
        chtPatientGraphs.Footnote.Text = strPatientInfo
        chtPatientGraphs.TitleText = "Date-Time Vs Weight"

        Try
            With chtPatientGraphs
                .chartType = MSChart20Lib.VtChChartType.VtChChartType2dLine
                If dt.Rows.Count >= 1 Then
                    'Axis Labels for the Graph

                    'dv = New DataView
                    'dv = dt.DefaultView
                    'dv.Sort = "dWeightinlbs ASC"
                    'nMinVal = CType(dv.ToTable.Rows(0)("dWeightinlbs"), Int64)
                    'nMaxVal = CType(dv.ToTable.Rows(dt.Rows.Count - 1)("dWeightinlbs"), Int64)
                    'dv.Dispose()
                    'dv = Nothing
                    ''Sandip Darade 20090313
                    '' To Get Max/Min Values of Weight.
                    If Not IsDBNull(dt.Rows(i)("dWeightinlbs")) Then
                        nMaxVal = dt.Compute("Max(dWeightinlbs)", "dWeightinlbs >= 0")
                        nMinVal = dt.Compute("Min(dWeightinlbs)", "dWeightinlbs >= 0")
                    End If
                    '' To Get Max/Min Values of Weight.
                    ''nMaxVal = dt.Compute("Max(dWeightinlbs)", "dWeightinlbs >= 0")
                    ''nMinVal = dt.Compute("Min(dWeightinlbs)", "dWeightinlbs >= 0")
                    ''

                    chtPatientGraphs.Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdX).AxisTitle.Text = "Date-Time"

                    chtPatientGraphs.Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).AxisTitle.Text = "Weight (lbs)"

                    ' get the total number of the vital entries for the patients
                    For i = 0 To dt.Rows.Count - 1  ' dt_MINMAX.Rows.Count - 1  'dt.Rows.Count - 1
                        ' get gender and assign standard Height to the array string
                        If Not IsDBNull(dt.Rows(i)("dtvitaldate")) Then
                            arrsales(i, 1) = Convert.ToString(dt.Rows(i)("dtvitaldate"))
                        Else
                            arrsales(i, 1) = 0
                        End If

                        If Not IsDBNull(dt.Rows(i)("dWeightinlbs")) Then
                            arrsales(i, 2) = Convert.ToString(dt.Rows(i)("dWeightinlbs"))
                        Else
                            arrsales(i, 2) = 0
                            nMinVal = 0
                        End If

                        ' standard Minimum values for weight
                        arrsales(i, 3) = 0 'nMinVal - 5

                        If Not IsDBNull(dt.Rows(i)("dWeightinlbs")) Then
                            arrsales(i, 4) = Convert.ToString(dt.Rows(i)("dWeightinlbs"))
                        Else
                            arrsales(i, 4) = 0
                            nMinVal = 0
                        End If

                        ' standard Maximum values for weight
                        arrsales(i, 5) = nMaxVal + 5

                        If Not IsDBNull(dt.Rows(i)("dWeightinlbs")) Then
                            arrsales(i, 6) = Convert.ToString(dt.Rows(i)("dWeightinlbs"))
                        Else
                            arrsales(i, 6) = 0
                            nMinVal = 0
                        End If

                        If Not IsDBNull(dt.Rows(i)("dtvitaldate")) Then
                            AgeCollection(i, 1) = Convert.ToString(dt.Rows(i)("dtvitaldate"))
                        Else
                            AgeCollection(i, 1) = 0
                        End If

                    Next
                Else
                    MessageBox.Show("No vital data available for this patient.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If

                'set data to draw chart
                .ChartData = CType(arrsales, Object)
                Dim Count_series As Integer = .Plot.SeriesCollection.Count()
                Dim cnt As Integer
                For cnt = 1 To dt.Rows.Count - 1
                    ' If cnt <> 3 Or cnt <> 4 Then
                    If cnt <= Count_series Then
                        .Plot.SeriesCollection(cnt).SeriesType = MSChart20Lib.VtChSeriesType.VtChSeriesType2dLine
                        .Plot.SeriesCollection(cnt).Pen.Width = 20
                        '.Plot.SeriesCollection(cnt).Pen.VtColor.Set(0, 0, 0)
                        '  End If
                    End If
                Next

                .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).ValueScale.Minimum = nMinVal - 5
                .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).ValueScale.Maximum = nMaxVal + 5
                .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).ValueScale.MajorDivision = 10
                .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).ValueScale.MinorDivision = 1
                .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).CategoryScale.DivisionsPerLabel = 1
                .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).CategoryScale.DivisionsPerTick = 1

                .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdX).ValueScale.MajorDivision = 10
                .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdX).ValueScale.MinorDivision = 1

                .Plot.SeriesCollection(3).SeriesType = MSChart20Lib.VtChSeriesType.VtChSeriesType2dLine
                ' .Plot.SeriesCollection(3).SeriesMarker.Auto = True
                .Plot.SeriesCollection(3).Select()
                .Plot.SeriesCollection(3).SeriesMarker.Show = True
                .Plot.SeriesCollection(3).ShowLine = False
                .Plot.SeriesCollection(3).Pen.Style = MSChart20Lib.VtPenStyle.VtPenStyleDashDotDot
                .Plot.SeriesCollection(3).Pen.Width = 6

                ' fill the labels of Y-axis
                For i = 1 To .RowCount
                    .Row = i
                    .RowLabel = AgeCollection(i - 1, 1)
                Next

            End With
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    '' END SUDHIR 

    Private Sub CloseVitals()
        Me.Close()
    End Sub

    Private Sub tblbtn_Temperature_32_Click_old(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tblbtn_Temperature_32.Click
        AgeVsTemprature()
    End Sub

    Private Function tempGraph_old()
        Dim oclsViewGraphs As New clsViewGraphs
        dt = New DataTable
        dt = oclsViewGraphs.ScanAgeHtWt(_PatientID)

        'get STANDARD values for the minmax values from SP 'gsp_viewGraphMinMax' where standard data is stored.
        'If nGender = 1 Then ' for male
        '    dt_MINMAX = oclsViewGraphs.GetMinMaxValues(Gender.Male, GraphType.Weight)
        'ElseIf nGender = 2 Then ' for female
        '    dt_MINMAX = oclsViewGraphs.GetMinMaxValues(Gender.Female, GraphType.Weight)
        'Else ' for other
        '    dt_MINMAX = oclsViewGraphs.GetMinMaxValues(Gender.Male, GraphType.Weight)
        'End If

        'view data in Datagrid
        'DataGrid1.DataSource = dt_MINMAX

        Dim i As Integer '= 20
        ' string data where patient Visit date and time collection when vital record enterd.
        Dim AgeCollection(dt.Rows.Count - 1, 2) As String

        ' string for the data where hight of the patient is collected.
        Dim arrsales(dt.Rows.Count - 1, 12) As String

        Dim strPatientInfo As String = Space(5) & " Patient Code:" & Space(1) & gloUC_PatientStrip1.PatientCode & Space(5) & "Patient Name:" & Space(1) & gloUC_PatientStrip1.PatientName & Space(5) & "DOB:" & Space(1) & gloUC_PatientStrip1.PatientDateOfBirth & Space(5) & "Phone No.:" & Space(1) & gloUC_PatientStrip1.PatientPhone & Space(5)
        chtPatientGraphs.Footnote.Location.LocationType = MSChart20Lib.VtChLocationType.VtChLocationTypeTop
        chtPatientGraphs.Footnote.Text = strPatientInfo

        chtPatientGraphs.TitleText = "Date-Time Vs Temperature"

        Try
            With chtPatientGraphs
                .chartType = MSChart20Lib.VtChChartType.VtChChartType2dCombination
                '.chartType = MSChart20Lib.VtChChartType.VtChChartType2dLine
                If dt.Rows.Count >= 1 Then
                    DataGrid1.DataSource = Nothing
                    DataGrid1.DataSource = dt
                    'Axis Labels for the Graph
                    chtPatientGraphs.Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdX).AxisTitle.Text = "Date-Time"
                    chtPatientGraphs.Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).AxisTitle.Text = "Temperature"

                    ' get the total number of the vital entries for the patients                   
                    For i = 0 To dt.Rows.Count - 1  ' dt_MINMAX.Rows.Count - 1  'dt.Rows.Count - 1
                        ' get gender and assign standard Height to the array string
                        If Not IsDBNull(dt.Rows(i)("dtvitaldate")) Then
                            arrsales(i, 1) = Convert.ToString(dt.Rows(i)("dtvitaldate"))
                        Else
                            arrsales(i, 1) = 0
                        End If

                        arrsales(i, 2) = 100 ' dt.Rows(i)("dTemperature")
                        arrsales(i, 3) = 90 ' standard Minimum values for temperature
                        arrsales(1, 4) = 95 'dt.Rows(i)("dTemperature") '90
                        arrsales(i, 5) = 110 ' standard maximum values for temperature
                        'arrsales(1, 6) = 100 ' dt.Rows(i)("dTemperature") '90 
                        If Not IsDBNull(dt.Rows(i)("dtvitaldate")) Then
                            AgeCollection(i, 1) = Convert.ToString(dt.Rows(i)("dtvitaldate"))
                        Else
                            AgeCollection(i, 1) = 0
                        End If


                    Next
                Else
                    MessageBox.Show("No vital data available for this patient.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    tempGraph_old = ""
                    Exit Function
                End If

                'Fill For PAtient
                'Dim Count As Integer
                'Count = arrsales.Length / arrsales.GetLength(1)

                'For i = 0 To Count - 1
                '    For j As Integer = 0 To dt.Rows.Count - 1
                '        If arrsales(i, 1) = dt.Rows(j)("AGE") Then
                '            '''' If Age Matches then Add Patients Ht in to The Array 
                '            arrsales(i, 2) = dt.Rows(j)("dWeightinlbs") '/ (0.45) 'FtToMtr(ft, Inch)
                '            Exit For
                '        End If
                '    Next
                '    arrsales(i, 1) = Nothing
                'Next

                'set data to draw chart
                .ChartData = CType(arrsales, Object)
                ' set graph styles   
                '.Plot.Backdrop.Fill.Brush.PatternColor.Set(255, 255, 255)               

                Dim cnt As Integer
                For cnt = 1 To dt.Rows.Count - 1
                    If cnt <> 3 Then
                        .Plot.SeriesCollection(cnt).SeriesType = MSChart20Lib.VtChSeriesType.VtChSeriesType2dLine
                        .Plot.SeriesCollection(cnt).Pen.Width = 30
                        '.Plot.SeriesCollection(cnt).Pen.VtColor.Set(0, 0, 0)
                    End If
                Next

                ' values set to 80 - 120 while accepting Temperature data is only in between 90-110
                .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).ValueScale.Minimum = 85
                .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).ValueScale.Maximum = 115
                .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).ValueScale.MajorDivision = 10
                .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).ValueScale.MinorDivision = 1
                .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).CategoryScale.DivisionsPerLabel = 1
                .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).CategoryScale.DivisionsPerTick = 1

                .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdX).ValueScale.MajorDivision = 10
                .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdX).ValueScale.MinorDivision = 1
                ''.Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdX).AxisTitle.VtFont.Size = 1


                .Plot.SeriesCollection(3).SeriesType = MSChart20Lib.VtChSeriesType.VtChSeriesType2dLine
                ' .Plot.SeriesCollection(3).SeriesMarker.Auto = True
                .Plot.SeriesCollection(3).Select()
                .Plot.SeriesCollection(3).SeriesMarker.Show = True
                .Plot.SeriesCollection(3).ShowLine = False
                .Plot.SeriesCollection(3).Pen.Style = MSChart20Lib.VtPenStyle.VtPenStyleDashDotDot
                .Plot.SeriesCollection(3).Pen.Width = 30

                ' labels at the Y-axis
                For i = 1 To .RowCount
                    .Row = i
                    .RowLabel = AgeCollection(i - 1, 1)
                Next

            End With
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        tempGraph_old = ""
    End Function

    ' draw graph of patients BP standing (minimum and maximum) value
    Private Sub tblbtn_BPSitting_32_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tblbtn_BPSitting_32.Click
        Dim BPType As Integer = 0
        BPType = 1
        Call GraphsForBP(BPType) ' 1 digit for BP Sitting
    End Sub

    ' draw graph of patients BP standing (minimum and maximum) value
    Private Sub tblbtn_BPStanding_32_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tblbtn_BPStanding_32.Click
        Dim BPType As Integer = 0
        BPType = 2
        Call GraphsForBP(BPType) ' 2 digit for BP Standing
    End Sub

    Private Sub chtPatientGraphs_ChartSelected(ByVal sender As System.Object, ByVal e As AxMSChart20Lib._DMSChartEvents_ChartSelectedEvent)

    End Sub

    'code commented by sarika vital Graphs 3rd june 08
    'Private Function FtToMtr(ByVal Ft As Decimal, ByVal Inch As Decimal) As Decimal
    '    Return ((Ft * 30.48 + Inch * 2.54) * 0.394) 'for centi meter
    '    ' Return (Ft + Inch * 2.54)
    '    'Return (Ft + Inch * 0.083)
    '    ''   1 ft = 30.48 cm
    '    ''   1 inch = 2.54 cm
    'End Function
    'Private Sub AgeStature()
    '    Dim nMonths As Int16
    '    nMonths = DateDiff(DateInterval.Month, CType(gstrPatientDOB, Date), Date.Now.Date)

    '    If nMonths <= 24 Then
    '        MessageBox.Show("Stature graph available only for more than 24 months patients.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
    '        Exit Sub
    '    End If

    '    'lblMin.Text = "Minimum Stature"
    '    'lblMax.Text = "Maximum Stature"
    '    'lblPatients.Text = "Patient Stature"

    '    ' get patient Gender
    '    Dim nGender As New Integer
    '    Dim oclsHPITemplate As New clsHPITemplate
    '    nGender = oclsHPITemplate.GetPatientGender(gnPatientID)

    '    Dim oclsViewGraphs As New clsViewGraphs
    '    dt = New DataTable
    '    dt = oclsViewGraphs.ScanAgeHtWt()

    '    oclsHPITemplate = Nothing

    '    Dim dt_MINMAX As New DataTable

    '    'get STANDARD values for the minmax values from SP 'gsp_viewGraphMinMax' where standard data is stored.
    '    If nGender = 1 Then ' for male
    '        dt_MINMAX = oclsViewGraphs.getminmaxvalues20yrs(Gender.Male, GraphType.Stature, nMonths)
    '    ElseIf nGender = 2 Then ' for female
    '        dt_MINMAX = oclsViewGraphs.getminmaxvalues20yrs(Gender.Female, GraphType.Stature, nMonths)
    '    Else ' for other
    '        dt_MINMAX = oclsViewGraphs.getminmaxvalues20yrs(Gender.Male, GraphType.Stature, nMonths)
    '    End If

    '    'view data in Datagrid
    '    DataGrid1.DataSource = dt_MINMAX

    '    Dim i As Integer '= 20
    '    ' string data where patient Age collection when vital record enterd.
    '    ''Dim AgeCollection(dt.Rows.Count - 1, 2) As String
    '    Dim AgeCollection(dt_MINMAX.Rows.Count - 1, 2) As String

    '    ' string for the data where hight of the patient is collected.
    '    Dim arrsales(dt_MINMAX.Rows.Count - 1, 12) As String

    '    Dim arrGraph(dt_MINMAX.Rows.Count - 1, 3) As String

    '    Dim strPatientInfo As String = Space(5) & " Patient Code:" & Space(1) & gloUC_PatientStrip1.PatientCode & Space(5) & "Patient Name:" & Space(1) & gloUC_PatientStrip1.PatientName & Space(5) & "DOB:" & Space(1) & gloUC_PatientStrip1.PatientDateOfBirth & Space(5) & "Phone No.:" & Space(1) & gloUC_PatientStrip1.PatientPhone & Space(5)
    '    chtPatientGraphs.Footnote.Location.LocationType = MSChart20Lib.VtChLocationType.VtChLocationTypeTop
    '    chtPatientGraphs.Footnote.Text = strPatientInfo

    '    chtPatientGraphs.TitleText = "Age Vs Stature"

    '    Try
    '        With chtPatientGraphs
    '            .chartType = MSChart20Lib.VtChChartType.VtChChartType2dCombination
    '            '.chartType = MSChart20Lib.VtChChartType.VtChChartType2dLine
    '            If dt_MINMAX.Rows.Count >= 1 Then
    '                DataGrid1.DataSource = Nothing
    '                DataGrid1.DataSource = dt
    '                'Axis Labels for the Graph
    '                chtPatientGraphs.Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdX).AxisTitle.Text = "Age (Months)"
    '                chtPatientGraphs.Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).AxisTitle.Text = "Stature (cm)"

    '                ' get the total number of the vital entries for the patients
    '                For i = 1 To dt_MINMAX.Rows.Count - 1  'dt.Rows.Count - 1
    '                    ' get gender and assign standard Height to the array string
    '                    arrsales(i, 1) = dt_MINMAX.Rows(i)("Agemos")
    '                    arrsales(i, 11) = dt_MINMAX.Rows(i)("P3")
    '                    arrsales(i, 3) = dt_MINMAX.Rows(i)("P5")
    '                    arrsales(i, 4) = dt_MINMAX.Rows(i)("P10")
    '                    arrsales(i, 5) = dt_MINMAX.Rows(i)("P25")
    '                    arrsales(i, 6) = dt_MINMAX.Rows(i)("P50")
    '                    arrsales(i, 7) = dt_MINMAX.Rows(i)("P75")
    '                    arrsales(i, 8) = dt_MINMAX.Rows(i)("P90")
    '                    arrsales(i, 9) = dt_MINMAX.Rows(i)("P95")
    '                    arrsales(i, 10) = dt_MINMAX.Rows(i)("P97")

    '                    AgeCollection(i, 1) = dt_MINMAX.Rows(i)("Agemos")
    '                Next
    '            Else
    '                MessageBox.Show("No vital data available for this patient.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
    '                Exit Sub
    '            End If

    '            'Fill For PAtient
    '            Dim Count As Integer
    '            Count = arrsales.Length / arrsales.GetLength(1)

    '            For i = 0 To Count - 1
    '                For j As Integer = 0 To dt.Rows.Count - 1
    '                    If arrsales(i, 1) = dt.Rows(j)("AGE") Then
    '                        '''' If Age Matches then Add Patients Ht in to The Array 
    '                        If IsDBNull(dt.Rows(j)("dStature")) = False Then
    '                            arrsales(i, 2) = dt.Rows(j)("dStature") '/ (0.45) 'FtToMtr(ft, Inch)
    '                        Else
    '                            arrsales(i, 2) = 0
    '                        End If
    '                        Exit For

    '                    End If
    '                Next
    '                arrsales(i, 1) = Nothing
    '            Next

    '            'set data to draw chart
    '            .ChartData = CType(arrsales, Object)
    '            ' set graph styles   
    '            '.Plot.Backdrop.Fill.Brush.PatternColor.Set(255, 255, 255)

    '            Dim cnt As Integer
    '            For cnt = 1 To 12
    '                If cnt <> 3 Then
    '                    .Plot.SeriesCollection(cnt).SeriesType = MSChart20Lib.VtChSeriesType.VtChSeriesType2dLine
    '                    .Plot.SeriesCollection(cnt).Pen.Width = 22
    '                    '.Plot.SeriesCollection(cnt).Pen.VtColor.Set(0, 0, 0)
    '                End If
    '            Next

    '            ' set the chart controls properties for the display.
    '            ' for y axis
    '            .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).ValueScale.Minimum = 80
    '            .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).ValueScale.Maximum = 190
    '            .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).ValueScale.MajorDivision = 10
    '            .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).ValueScale.MinorDivision = 1
    '            .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).CategoryScale.DivisionsPerLabel = 1
    '            .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).CategoryScale.DivisionsPerTick = 1

    '            ' for x axis
    '            .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdX).ValueScale.MajorDivision = 10
    '            .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdX).ValueScale.MinorDivision = 1

    '            ' set the properties for the column where data is stored of patients i.e. patient vital entries.
    '            .Plot.SeriesCollection(3).SeriesType = MSChart20Lib.VtChSeriesType.VtChSeriesType2dLine
    '            ' .Plot.SeriesCollection(3).SeriesMarker.Auto = True
    '            .Plot.SeriesCollection(3).Select()
    '            .Plot.SeriesCollection(3).SeriesMarker.Show = True
    '            .Plot.SeriesCollection(3).ShowLine = False
    '            .Plot.SeriesCollection(3).Pen.Style = MSChart20Lib.VtPenStyle.VtPenStyleSolid
    '            .Plot.SeriesCollection(3).Pen.Width = 6

    '            ' print labels on the x axis
    '            For i = 1 To .RowCount
    '                .Row = i
    '                .RowLabel = AgeCollection(i - 1, 1)
    '            Next

    '        End With
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message)
    '    End Try
    'End Sub
    'Private Sub AgeWeight24()
    '    Dim nMonths As Int16
    '    nMonths = DateDiff(DateInterval.Month, CType(gstrPatientDOB, Date), Date.Now.Date)

    '    If nMonths < 24 Then
    '        MessageBox.Show("Graph available for more than 2 year old patients.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
    '        Exit Sub
    '    End If

    '    'lblMin.Text = "Minimum Weight"
    '    'lblMax.Text = "Maximum Weight"
    '    'lblPatients.Text = "Patient Weight"

    '    Dim oclsViewGraphs As New clsViewGraphs
    '    dt = New DataTable
    '    dt = oclsViewGraphs.ScanAgeHtWt()

    '    ' get patient Gender
    '    Dim nGender As New Integer
    '    Dim oclsHPITemplate As New clsHPITemplate
    '    nGender = oclsHPITemplate.GetPatientGender(gnPatientID)
    '    oclsHPITemplate = Nothing

    '    Dim dt_MINMAX As New DataTable

    '    'get STANDARD values for the minmax values from SP 'gsp_viewGraphMinMax' where standard data is stored.
    '    If nGender = 1 Then ' for male
    '        dt_MINMAX = oclsViewGraphs.getminmaxvalues20yrs(Gender.Male, GraphType.Weight, nMonths)
    '    ElseIf nGender = 2 Then ' for female
    '        dt_MINMAX = oclsViewGraphs.getminmaxvalues20yrs(Gender.Female, GraphType.Weight, nMonths)
    '    Else ' for other
    '        dt_MINMAX = oclsViewGraphs.getminmaxvalues20yrs(Gender.Male, GraphType.Weight, nMonths)
    '    End If

    '    'view data in Datagrid
    '    DataGrid1.DataSource = dt_MINMAX

    '    Dim i As Integer '= 20
    '    ' string data where patient Age collection when vital record enterd.
    '    ''Dim AgeCollection(dt.Rows.Count - 1, 2) As String
    '    Dim AgeCollection(dt_MINMAX.Rows.Count - 1, 2) As String

    '    ' string for the data where hight of the patient is collected.
    '    Dim arrsales(dt_MINMAX.Rows.Count - 1, 12) As String

    '    Dim arrGraph(dt_MINMAX.Rows.Count - 1, 3) As String


    '    Dim strPatientInfo As String = Space(5) & " Patient Code:" & Space(1) & gloUC_PatientStrip1.PatientCode & Space(5) & "Patient Name:" & Space(1) & gloUC_PatientStrip1.PatientName & Space(5) & "DOB:" & Space(1) & gloUC_PatientStrip1.PatientDateOfBirth & Space(5) & "Phone No.:" & Space(1) & gloUC_PatientStrip1.PatientPhone & Space(5)
    '    chtPatientGraphs.Footnote.Location.LocationType = MSChart20Lib.VtChLocationType.VtChLocationTypeTop
    '    chtPatientGraphs.Footnote.Text = strPatientInfo

    '    chtPatientGraphs.TitleText = "Age Vs Weight"

    '    Try
    '        With chtPatientGraphs
    '            .chartType = MSChart20Lib.VtChChartType.VtChChartType2dCombination
    '            '.chartType = MSChart20Lib.VtChChartType.VtChChartType2dLine
    '            If dt_MINMAX.Rows.Count >= 1 Then
    '                DataGrid1.DataSource = Nothing
    '                DataGrid1.DataSource = dt
    '                'Axis Labels for the Graph
    '                chtPatientGraphs.Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdX).AxisTitle.Text = "Age (Months)"
    '                chtPatientGraphs.Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).AxisTitle.Text = "Weight (lbs)"

    '                ' get the total number of the vital entries for the patients
    '                For i = 0 To dt_MINMAX.Rows.Count - 1   'dt.Rows.Count - 1
    '                    ' get gender and assign standard Height to the array string
    '                    arrsales(i, 1) = dt_MINMAX.Rows(i)("Agemos")
    '                    arrsales(i, 11) = dt_MINMAX.Rows(i)("P3") / (0.45)
    '                    arrsales(i, 3) = dt_MINMAX.Rows(i)("P5") / (0.45)
    '                    arrsales(i, 4) = dt_MINMAX.Rows(i)("P10") / (0.45)
    '                    arrsales(i, 5) = dt_MINMAX.Rows(i)("P25") / (0.45)
    '                    arrsales(i, 6) = dt_MINMAX.Rows(i)("P50") / (0.45)
    '                    arrsales(i, 7) = dt_MINMAX.Rows(i)("P75") / (0.45)
    '                    arrsales(i, 8) = dt_MINMAX.Rows(i)("P90") / (0.45)
    '                    arrsales(i, 9) = dt_MINMAX.Rows(i)("P95") / (0.45)
    '                    arrsales(i, 10) = dt_MINMAX.Rows(i)("P97") / (0.45)

    '                    AgeCollection(i, 1) = dt_MINMAX.Rows(i)("Agemos")
    '                Next
    '            Else
    '                MessageBox.Show("No vital data available for this patient.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
    '                Exit Sub
    '            End If

    '            Dim graphMin As Integer
    '            graphMin = dt_MINMAX.Rows(1)("P3") / (0.45)

    '            Dim graphMax As Integer
    '            graphMax = dt_MINMAX.Rows(dt_MINMAX.Rows.Count - 1)("P97") / (0.45)


    '            'Fill For PAtient
    '            Dim Count As Integer
    '            Count = arrsales.Length / arrsales.GetLength(1)

    '            For i = 0 To Count - 1
    '                For j As Integer = 0 To dt.Rows.Count - 1
    '                    If arrsales(i, 1) = dt.Rows(j)("AGE") Then
    '                        '''' If Age Matches then Add Patients Ht in to The Array 
    '                        If Not IsDBNull(dt.Rows(j)("dWeightinlbs")) Then
    '                            arrsales(i, 2) = dt.Rows(j)("dWeightinlbs") '/ (0.45) 'FtToMtr(ft, Inch)
    '                        Else
    '                            arrsales(i, 2) = 0
    '                        End If
    '                        Exit For
    '                    End If
    '                Next
    '                arrsales(i, 1) = Nothing
    '            Next

    '            'set data to draw chart
    '            .ChartData = CType(arrsales, Object)
    '            ' set graph styles   
    '            '.Plot.Backdrop.Fill.Brush.PatternColor.Set(255, 255, 255)               

    '            Dim cnt As Integer
    '            For cnt = 1 To 12
    '                If cnt <> 3 Then
    '                    .Plot.SeriesCollection(cnt).SeriesType = MSChart20Lib.VtChSeriesType.VtChSeriesType2dLine
    '                    .Plot.SeriesCollection(cnt).Pen.Width = 22
    '                    '.Plot.SeriesCollection(cnt).Pen.VtColor.Set(0, 0, 0)
    '                End If
    '            Next

    '            .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).ValueScale.Minimum = graphMin - 10 '1
    '            .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).ValueScale.Maximum = graphMax + 10 '40
    '            .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).ValueScale.MajorDivision = 10
    '            .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).ValueScale.MinorDivision = 1
    '            .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).CategoryScale.DivisionsPerLabel = 1
    '            .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).CategoryScale.DivisionsPerTick = 1

    '            .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdX).ValueScale.MajorDivision = 10
    '            .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdX).ValueScale.MinorDivision = 1
    '            '.Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdX).AxisTitle.VtFont.Size = 1

    '            .Plot.SeriesCollection(3).SeriesType = MSChart20Lib.VtChSeriesType.VtChSeriesType2dLine
    '            ' .Plot.SeriesCollection(3).SeriesMarker.Auto = True
    '            .Plot.SeriesCollection(3).Select()
    '            .Plot.SeriesCollection(3).SeriesMarker.Show = True
    '            .Plot.SeriesCollection(3).ShowLine = False
    '            .Plot.SeriesCollection(3).Pen.Style = MSChart20Lib.VtPenStyle.VtPenStyleDashDotDot
    '            .Plot.SeriesCollection(3).Pen.Width = 6

    '            Dim yrs As Integer
    '            Dim reminder As Integer

    '            For i = 1 To .RowCount
    '                .Row = i
    '                yrs = AgeCollection(i - 1, 1) / 12
    '                reminder = AgeCollection(i - 1, 1) Mod 12

    '                If reminder = 0 Then
    '                    .RowLabel = yrs & "Yrs"
    '                Else
    '                    .RowLabel = AgeCollection(i - 1, 1)
    '                End If
    '            Next

    '        End With
    '    Catch ex As Exception
    '        MsgBox(ex.ToString)
    '    End Try
    'End Sub
    'Private Sub WeightHeight()

    '    Dim oclsViewGraphs As New clsViewGraphs
    '    dt = New DataTable
    '    dt = oclsViewGraphs.ScanAgeHtWt()

    '    ' get patient Gender
    '    Dim nGender As New Integer
    '    Dim oclsHPITemplate As New clsHPITemplate
    '    nGender = oclsHPITemplate.GetPatientGender(gnPatientID)
    '    oclsHPITemplate = Nothing

    '    Dim dt_MINMAX As New DataTable

    '    'get STANDARD values for the minmax values from SP 'gsp_viewGraphMinMax' where standard data is stored.
    '    If nGender = 1 Then ' for male
    '        dt_MINMAX = oclsViewGraphs.getminmaxvaluesWtHt(Gender.Male)
    '    ElseIf nGender = 2 Then ' for female
    '        dt_MINMAX = oclsViewGraphs.getminmaxvaluesWtHt(Gender.Female)
    '    Else ' for other
    '        dt_MINMAX = oclsViewGraphs.getminmaxvaluesWtHt(Gender.Male)
    '    End If

    '    'view data in Datagrid
    '    DataGrid1.DataSource = dt_MINMAX

    '    Dim i As Integer '= 20
    '    ' string data where patient Age collection when vital record enterd.
    '    ''Dim AgeCollection(dt.Rows.Count - 1, 2) As String
    '    Dim AgeCollection(dt_MINMAX.Rows.Count - 1, 2) As String

    '    ' string for the data where hight of the patient is collected.
    '    Dim arrsales(dt_MINMAX.Rows.Count - 1, 12) As String

    '    Dim arrGraph(dt_MINMAX.Rows.Count - 1, 3) As String

    '    Dim strPatientInfo As String = Space(5) & " Patient Code:" & Space(1) & gloUC_PatientStrip1.PatientCode & Space(5) & "Patient Name:" & Space(1) & gloUC_PatientStrip1.PatientName & Space(5) & "DOB:" & Space(1) & gloUC_PatientStrip1.PatientDateOfBirth & Space(5) & "Phone No.:" & Space(1) & gloUC_PatientStrip1.PatientPhone & Space(5)
    '    chtPatientGraphs.Footnote.Location.LocationType = MSChart20Lib.VtChLocationType.VtChLocationTypeTop
    '    chtPatientGraphs.Footnote.Text = strPatientInfo

    '    chtPatientGraphs.TitleText = "Weight Vs Height"

    '    Try
    '        With chtPatientGraphs
    '            .chartType = MSChart20Lib.VtChChartType.VtChChartType2dCombination
    '            '.chartType = MSChart20Lib.VtChChartType.VtChChartType2dLine
    '            If dt_MINMAX.Rows.Count >= 1 Then
    '                DataGrid1.DataSource = Nothing
    '                DataGrid1.DataSource = dt
    '                'Axis Labels for the Graph
    '                chtPatientGraphs.Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdX).AxisTitle.Text = "Height (Inch)"
    '                chtPatientGraphs.Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).AxisTitle.Text = "Weight (lbs)"

    '                ' get the total number of the vital entries for the patients
    '                For i = 1 To dt_MINMAX.Rows.Count - 1  'dt.Rows.Count - 1
    '                    ' get gender and assign standard Height to the array string
    '                    arrsales(i, 1) = dt_MINMAX.Rows(i)("Length") * 0.394
    '                    arrsales(i, 11) = dt_MINMAX.Rows(i)("P3") * 2.204
    '                    arrsales(i, 3) = dt_MINMAX.Rows(i)("P5") * 2.204
    '                    arrsales(i, 4) = dt_MINMAX.Rows(i)("P10") * 2.204
    '                    arrsales(i, 5) = dt_MINMAX.Rows(i)("P25") * 2.204
    '                    arrsales(i, 6) = dt_MINMAX.Rows(i)("P50") * 2.204
    '                    arrsales(i, 7) = dt_MINMAX.Rows(i)("P75") * 2.204
    '                    arrsales(i, 8) = dt_MINMAX.Rows(i)("P90") * 2.204
    '                    arrsales(i, 9) = dt_MINMAX.Rows(i)("P95") * 2.204
    '                    arrsales(i, 10) = dt_MINMAX.Rows(i)("P97") * 2.204

    '                    AgeCollection(i, 1) = dt_MINMAX.Rows(i)("Length") * 0.394
    '                Next
    '            Else
    '                MessageBox.Show("No vital data available for this patient.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
    '                Exit Sub
    '            End If

    '            'Fill For PAtient
    '            Dim Count As Integer
    '            Count = arrsales.Length / arrsales.GetLength(1)
    '            For i = 0 To Count - 1
    '                For j As Integer = 0 To dt.Rows.Count - 1
    '                    Dim ft As Decimal
    '                    Dim Inch As Decimal
    '                    Dim temp() As String
    '                    ' data convert from ftInch to in points i.e. 1 ft 6 Inch is 1.5 Ft
    '                    If Not IsDBNull(dt.Rows(j)("sHeight")) Then
    '                        temp = GetFtInch(dt.Rows(j)("sHeight"))
    '                    Else
    '                        temp = GetFtInch(0)
    '                    End If

    '                    '''' Only Inch is available then
    '                    If temp(0).Trim <> "" Then
    '                        ft = Convert.ToDecimal(CType(temp(0), Object))
    '                    Else
    '                        ft = 0
    '                    End If

    '                    If temp(1).Trim <> "" Then
    '                        Inch = Convert.ToDecimal(CType(temp(1), Object))
    '                    Else
    '                        Inch = 0
    '                    End If

    '                    If arrsales(i, 1) >= FtToMtr(ft, Inch) And arrsales(i, 1) <= (FtToMtr(ft, Inch) + 0.5) Then
    '                        '''' If Age Matches then Add Patients Ht in to The Array 
    '                        If Not IsDBNull(dt.Rows(j)("dweightinlbs")) Then
    '                            arrsales(i, 2) = dt.Rows(j)("dweightinlbs") ' dt.Rows(j)("sHeight") '
    '                        Else
    '                            arrsales(i, 2) = 0
    '                        End If
    '                        Exit For
    '                    End If
    '                Next
    '                arrsales(i, 1) = Nothing
    '            Next

    '            'set data to draw chart
    '            .ChartData = CType(arrsales, Object)

    '            ' set graph styles for the patient vital entries.
    '            .Plot.SeriesCollection(3).SeriesType = MSChart20Lib.VtChSeriesType.VtChSeriesType2dLine
    '            .Plot.SeriesCollection(3).Select()
    '            .Plot.SeriesCollection(3).ShowLine = False
    '            .Plot.SeriesCollection(3).DataPoints(-1).Marker.Style = MSChart20Lib.VtMarkerStyle.VtMarkerStyleDiamond
    '            .Plot.SeriesCollection(3).DataPoints(-1).Marker.Visible = True
    '            .Plot.SeriesCollection(3).DataPoints(-1).Marker.FillColor.Set(255, 0, 0)
    '            .Plot.SeriesCollection(3).DataPoints(-1).Brush.Style = MSChart20Lib.VtBrushStyle.VtBrushStyleSolid
    '            .Plot.SeriesCollection(3).DataPoints(-1).Marker.Size = 20
    '            .Plot.SeriesCollection(3).Pen.Width = 20
    '            .Plot.SeriesCollection(3).DataPoints(-1).Brush.FillColor.Set(255, 0, 0)

    '            ' set the display properties of the graphs curve.
    '            Dim cnt As Integer
    '            For cnt = 1 To 12
    '                If cnt <> 3 Then
    '                    .Plot.SeriesCollection(cnt).SeriesType = MSChart20Lib.VtChSeriesType.VtChSeriesType2dLine
    '                    .Plot.SeriesCollection(cnt).Pen.Width = 22
    '                    '.Plot.SeriesCollection(cnt).Pen.VtColor.Set(0, 0, 0)
    '                End If
    '            Next

    '            ' set the graph properties for the y axis.
    '            .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).ValueScale.Minimum = 1
    '            .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).ValueScale.Maximum = 45
    '            .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).ValueScale.MajorDivision = 10
    '            .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).ValueScale.MinorDivision = 1
    '            .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).CategoryScale.DivisionsPerLabel = 1
    '            .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).CategoryScale.DivisionsPerTick = 1

    '            ' set the properties for the x axis.
    '            .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdX).ValueScale.MajorDivision = 10
    '            .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdX).ValueScale.MinorDivision = 10

    '            'set the label of x axis.
    '            Dim yrs, reminder

    '            For i = 1 To .RowCount
    '                .Row = i
    '                If AgeCollection(i - 1, 1) <> Nothing Or AgeCollection(i - 1, 1) = "" Then
    '                    .RowLabel = AgeCollection(i - 1, 1)
    '                Else
    '                    .RowLabel = "" 'AgeCollection(i - 1, 1)
    '                End If

    '            Next

    '        End With
    '    Catch ex As Exception
    '        MsgBox(ex.ToString)
    '    End Try
    'End Sub
    'Private Sub AgeCircumfranse()
    '    'lblMin.Text = "Minimum Circumfrance"
    '    'lblMax.Text = "Maximum Circumfrance"
    '    'lblPatients.Text = "Patient Circumference"

    '    Dim oclsViewGraphs As New clsViewGraphs
    '    dt = New DataTable
    '    dt = oclsViewGraphs.ScanAgeHtWt()

    '    ' get patient Gender
    '    Dim nGender As New Integer
    '    Dim oclsHPITemplate As New clsHPITemplate
    '    nGender = oclsHPITemplate.GetPatientGender(gnPatientID)
    '    oclsHPITemplate = Nothing

    '    Dim dt_MINMAX As New DataTable

    '    'get STANDARD values for the minmax values from SP 'gsp_viewGraphMinMax' where standard data is stored.
    '    If nGender = 1 Then ' for male
    '        dt_MINMAX = oclsViewGraphs.GetMinMaxValues(Gender.Male, GraphType.HeadCircumfarance)
    '    ElseIf nGender = 2 Then ' for female
    '        dt_MINMAX = oclsViewGraphs.GetMinMaxValues(Gender.Female, GraphType.HeadCircumfarance)
    '    Else ' for other
    '        dt_MINMAX = oclsViewGraphs.GetMinMaxValues(Gender.Male, GraphType.HeadCircumfarance)
    '    End If

    '    'view data in Datagrid
    '    DataGrid1.DataSource = dt_MINMAX

    '    Dim i As Integer '= 20
    '    ' string data where patient Age collection when vital record enterd.
    '    ''Dim AgeCollection(dt.Rows.Count - 1, 2) As String
    '    Dim AgeCollection(dt_MINMAX.Rows.Count - 1, 2) As String

    '    ' string for the data where hight of the patient is collected.
    '    Dim arrsales(dt_MINMAX.Rows.Count - 1, 12) As String

    '    Dim arrGraph(dt_MINMAX.Rows.Count - 1, 3) As String

    '    Dim strPatientInfo As String = Space(5) & " Patient Code:" & Space(1) & gloUC_PatientStrip1.PatientCode & Space(5) & "Patient Name:" & Space(1) & gloUC_PatientStrip1.PatientName & Space(5) & "DOB:" & Space(1) & gloUC_PatientStrip1.PatientDateOfBirth & Space(5) & "Phone No.:" & Space(1) & gloUC_PatientStrip1.PatientPhone & Space(5)
    '    chtPatientGraphs.Footnote.Location.LocationType = MSChart20Lib.VtChLocationType.VtChLocationTypeTop


    '    chtPatientGraphs.Footnote.Text = strPatientInfo

    '    chtPatientGraphs.TitleText = "Age Vs Circumference"

    '    Try
    '        With chtPatientGraphs
    '            .chartType = MSChart20Lib.VtChChartType.VtChChartType2dCombination
    '            '.chartType = MSChart20Lib.VtChChartType.VtChChartType2dLine
    '            If dt_MINMAX.Rows.Count >= 1 Then
    '                DataGrid1.DataSource = Nothing
    '                DataGrid1.DataSource = dt
    '                'Axis Labels for the Graph
    '                chtPatientGraphs.Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdX).AxisTitle.Text = "Age (Months)"
    '                chtPatientGraphs.Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).AxisTitle.Text = "Head Circumference(cm)"

    '                ' get the total number of the vital entries for the patients
    '                For i = 1 To dt_MINMAX.Rows.Count - 1  'dt.Rows.Count - 1
    '                    ' get gender and assign standard Height to the array string
    '                    arrsales(i, 1) = dt_MINMAX.Rows(i)("Agemos")
    '                    arrsales(i, 11) = dt_MINMAX.Rows(i)("P3")
    '                    arrsales(i, 3) = dt_MINMAX.Rows(i)("P5")
    '                    arrsales(i, 4) = dt_MINMAX.Rows(i)("P10")
    '                    arrsales(i, 5) = dt_MINMAX.Rows(i)("P25")
    '                    arrsales(i, 6) = dt_MINMAX.Rows(i)("P50")
    '                    arrsales(i, 7) = dt_MINMAX.Rows(i)("P75")
    '                    arrsales(i, 8) = dt_MINMAX.Rows(i)("P90")
    '                    arrsales(i, 9) = dt_MINMAX.Rows(i)("P95")
    '                    arrsales(i, 10) = dt_MINMAX.Rows(i)("P97")

    '                    AgeCollection(i, 1) = dt_MINMAX.Rows(i)("Agemos")
    '                Next
    '            Else
    '                MessageBox.Show("No vital data available for this patient.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
    '                Exit Sub
    '            End If

    '            'Fill For PAtient
    '            Dim Count As Integer
    '            Count = arrsales.Length / arrsales.GetLength(1)

    '            For i = 0 To Count - 1
    '                For j As Integer = 0 To dt.Rows.Count - 1
    '                    If arrsales(i, 1) = dt.Rows(j)("AGE") Then
    '                        '''' If Age Matches then Add Patients Ht in to The Array 
    '                        If Not IsDBNull(dt.Rows(j)("dHeadCircumferance")) Then
    '                            arrsales(i, 2) = dt.Rows(j)("dHeadCircumferance") '/ (0.45) 'FtToMtr(ft, Inch)
    '                        Else
    '                            arrsales(i, 2) = 0
    '                        End If

    '                        Exit For
    '                    End If
    '                Next
    '                arrsales(i, 1) = Nothing
    '            Next

    '            'set data to draw chart
    '            .ChartData = CType(arrsales, Object)
    '            ' set graph styles   
    '            '.Plot.Backdrop.Fill.Brush.PatternColor.Set(255, 255, 255)

    '            Dim cnt As Integer
    '            For cnt = 1 To 12
    '                If cnt <> 3 Then
    '                    .Plot.SeriesCollection(cnt).SeriesType = MSChart20Lib.VtChSeriesType.VtChSeriesType2dLine
    '                    .Plot.SeriesCollection(cnt).Pen.Width = 22
    '                    '.Plot.SeriesCollection(cnt).Pen.VtColor.Set(0, 0, 0)
    '                End If
    '            Next

    '            ' set the chart controls properties for the display.
    '            ' for y axis
    '            .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).ValueScale.Minimum = 32
    '            .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).ValueScale.Maximum = 53
    '            .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).ValueScale.MajorDivision = 10
    '            .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).ValueScale.MinorDivision = 1
    '            .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).CategoryScale.DivisionsPerLabel = 1
    '            .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).CategoryScale.DivisionsPerTick = 1

    '            ' for x axis
    '            .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdX).ValueScale.MajorDivision = 10
    '            .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdX).ValueScale.MinorDivision = 1

    '            ' set the properties for the column where data is stored of patients i.e. patient vital entries.
    '            .Plot.SeriesCollection(3).SeriesType = MSChart20Lib.VtChSeriesType.VtChSeriesType2dLine
    '            ' .Plot.SeriesCollection(3).SeriesMarker.Auto = True
    '            .Plot.SeriesCollection(3).Select()
    '            .Plot.SeriesCollection(3).SeriesMarker.Show = True
    '            .Plot.SeriesCollection(3).ShowLine = False
    '            .Plot.SeriesCollection(3).Pen.Style = MSChart20Lib.VtPenStyle.VtPenStyleSolid
    '            .Plot.SeriesCollection(3).Pen.Width = 6

    '            ' print labels on the x axis
    '            For i = 1 To .RowCount
    '                .Row = i
    '                .RowLabel = AgeCollection(i - 1, 1)
    '            Next

    '        End With
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message)
    '    End Try
    'End Sub
    'Private Sub AgeHeight()
    '    'lblMin.Text = "Minimum Height"
    '    'lblMax.Text = "Maximum Height"
    '    'lblPatients.Text = "Patient Height"

    '    Dim oclsViewGraphs As New clsViewGraphs
    '    dt = New DataTable
    '    dt = oclsViewGraphs.ScanAgeHtWt()

    '    ' get patient Gender
    '    Dim nGender As New Integer
    '    Dim oclsHPITemplate As New clsHPITemplate
    '    nGender = oclsHPITemplate.GetPatientGender(gnPatientID)
    '    oclsHPITemplate = Nothing

    '    Dim dt_MINMAX As New DataTable

    '    'get STANDARD values for the minmax values from SP 'gsp_viewGraphMinMax' where standard data is stored.
    '    If nGender = 1 Then ' for male
    '        dt_MINMAX = oclsViewGraphs.GetMinMaxValues(Gender.Male, GraphType.Height)
    '    ElseIf nGender = 2 Then ' for female
    '        dt_MINMAX = oclsViewGraphs.GetMinMaxValues(Gender.Female, GraphType.Height)
    '    Else ' for other
    '        dt_MINMAX = oclsViewGraphs.GetMinMaxValues(Gender.Male, GraphType.Height)
    '    End If

    '    'view data in Datagrid
    '    DataGrid1.DataSource = dt_MINMAX

    '    Dim i As Integer '= 20
    '    ' string data where patient Age collection when vital record enterd.
    '    ''Dim AgeCollection(dt.Rows.Count - 1, 2) As String
    '    Dim AgeCollection(dt_MINMAX.Rows.Count - 1, 2) As String

    '    ' string for the data where hight of the patient is collected.
    '    Dim arrsales(dt_MINMAX.Rows.Count - 1, 12) As String

    '    Dim arrGraph(dt_MINMAX.Rows.Count - 1, 3) As String

    '    Dim strPatientInfo As String = Space(5) & " Patient Code:" & Space(1) & gloUC_PatientStrip1.PatientCode & Space(5) & "Patient Name:" & Space(1) & gloUC_PatientStrip1.PatientName & Space(5) & "DOB:" & Space(1) & gloUC_PatientStrip1.PatientDateOfBirth & Space(5) & "Phone No.:" & Space(1) & gloUC_PatientStrip1.PatientPhone & Space(5)
    '    chtPatientGraphs.Footnote.Location.LocationType = MSChart20Lib.VtChLocationType.VtChLocationTypeTop
    '    chtPatientGraphs.Footnote.Text = strPatientInfo

    '    chtPatientGraphs.TitleText = "Age Vs Height"

    '    Try
    '        With chtPatientGraphs
    '            .chartType = MSChart20Lib.VtChChartType.VtChChartType2dCombination
    '            '.chartType = MSChart20Lib.VtChChartType.VtChChartType2dLine
    '            If dt_MINMAX.Rows.Count >= 1 Then
    '                DataGrid1.DataSource = Nothing
    '                DataGrid1.DataSource = dt
    '                'Axis Labels for the Graph
    '                chtPatientGraphs.Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdX).AxisTitle.Text = "Age (Months)"
    '                chtPatientGraphs.Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).AxisTitle.Text = "Height (Inch)"

    '                ' get the total number of the vital entries for the patients
    '                For i = 1 To dt_MINMAX.Rows.Count - 1  'dt.Rows.Count - 1
    '                    ' get gender and assign standard Height to the array string
    '                    arrsales(i, 1) = dt_MINMAX.Rows(i)("Agemos")
    '                    arrsales(i, 11) = dt_MINMAX.Rows(i)("P3") * 0.394
    '                    arrsales(i, 3) = dt_MINMAX.Rows(i)("P5") * 0.394
    '                    arrsales(i, 4) = dt_MINMAX.Rows(i)("P10") * 0.394
    '                    arrsales(i, 5) = dt_MINMAX.Rows(i)("P25") * 0.394
    '                    arrsales(i, 6) = dt_MINMAX.Rows(i)("P50") * 0.394
    '                    arrsales(i, 7) = dt_MINMAX.Rows(i)("P75") * 0.394
    '                    arrsales(i, 8) = dt_MINMAX.Rows(i)("P90") * 0.394
    '                    arrsales(i, 9) = dt_MINMAX.Rows(i)("P95") * 0.394
    '                    arrsales(i, 10) = dt_MINMAX.Rows(i)("P97") * 0.394

    '                    AgeCollection(i, 1) = dt_MINMAX.Rows(i)("Agemos")
    '                Next
    '            Else
    '                MessageBox.Show("No vital data available for this patient.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
    '                Exit Sub
    '            End If

    '            'Fill For PAtient
    '            Dim Count As Integer
    '            Count = arrsales.Length / arrsales.GetLength(1)

    '            For i = 0 To Count - 1
    '                For j As Integer = 0 To dt.Rows.Count - 1
    '                    If arrsales(i, 1) = dt.Rows(j)("AGE") Then
    '                        '' If Age Matches then Add Patients Ht in to The Array
    '                        Dim ft As Decimal
    '                        Dim Inch As Decimal
    '                        Dim temp() As String
    '                        ' data convert from ftInch to in points i.e. 1 ft 6 Inch is 1.5 Ft
    '                        If Not IsDBNull(dt.Rows(j)("sHeight")) Then
    '                            temp = GetFtInch(dt.Rows(j)("sHeight"))
    '                        Else
    '                            temp = GetFtInch(0)
    '                        End If

    '                        '''' Only Inch is available then
    '                        If temp(0).Trim <> "" Then
    '                            ft = Convert.ToDecimal(CType(temp(0), Object))
    '                        Else
    '                            ft = 0
    '                        End If

    '                        If temp(1).Trim <> "" Then
    '                            Inch = Convert.ToDecimal(CType(temp(1), Object))
    '                        Else
    '                            Inch = 0
    '                        End If
    '                        '''' If Age Matches then Add Patients Ht in to The Array 
    '                        arrsales(i, 2) = FtToMtr(ft, Inch) ' dt.Rows(j)("sHeight") '
    '                        Exit For
    '                    End If
    '                Next
    '                arrsales(i, 1) = Nothing
    '            Next

    '            'set data to draw chart
    '            .ChartData = CType(arrsales, Object)

    '            ' set graph styles for the patient vital entries.
    '            .Plot.SeriesCollection(3).SeriesType = MSChart20Lib.VtChSeriesType.VtChSeriesType2dLine
    '            .Plot.SeriesCollection(3).Select()
    '            '.Plot.SeriesCollection(3).SeriesMarker.Show = True
    '            .Plot.SeriesCollection(3).ShowLine = False
    '            .Plot.SeriesCollection(3).DataPoints(-1).Marker.Style = MSChart20Lib.VtMarkerStyle.VtMarkerStyleDiamond
    '            .Plot.SeriesCollection(3).DataPoints(-1).Marker.Visible = True
    '            .Plot.SeriesCollection(3).DataPoints(-1).Marker.FillColor.Set(255, 0, 0)
    '            '.Plot.SeriesCollection(3).DataPoints(-1).Marker.Pen.Style = MSChart20Lib.VtPenStyle.VtPenStyleDashDot
    '            .Plot.SeriesCollection(3).DataPoints(-1).Brush.Style = MSChart20Lib.VtBrushStyle.VtBrushStyleSolid
    '            .Plot.SeriesCollection(3).DataPoints(-1).Marker.Size = 20
    '            .Plot.SeriesCollection(3).Pen.Width = 20
    '            .Plot.SeriesCollection(3).DataPoints(-1).Brush.FillColor.Set(255, 0, 0)

    '            ' set the display properties of the graphs curve.
    '            Dim cnt As Integer
    '            For cnt = 1 To 12
    '                If cnt <> 3 Then
    '                    .Plot.SeriesCollection(cnt).SeriesType = MSChart20Lib.VtChSeriesType.VtChSeriesType2dLine
    '                    .Plot.SeriesCollection(cnt).Pen.Width = 22
    '                    '.Plot.SeriesCollection(cnt).Pen.VtColor.Set(0, 0, 0)
    '                End If
    '            Next

    '            ' set the graph properties for the y axis.
    '            .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).ValueScale.Minimum = 18
    '            .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).ValueScale.Maximum = 42
    '            .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).ValueScale.MajorDivision = 10
    '            .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).ValueScale.MinorDivision = 1
    '            .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).CategoryScale.DivisionsPerLabel = 1
    '            .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).CategoryScale.DivisionsPerTick = 1

    '            ' set the properties for the x axis.
    '            .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdX).ValueScale.MajorDivision = 10
    '            .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdX).ValueScale.MinorDivision = 10

    '            'set the label of x axis.
    '            For i = 1 To .RowCount
    '                .Row = i
    '                .RowLabel = AgeCollection(i - 1, 1)
    '            Next

    '        End With
    '    Catch ex As Exception
    '        MsgBox(ex.ToString)
    '    End Try
    'End Sub
    'Private Sub AgeWeight()
    '    'lblMin.Text = "Minimum Weight"
    '    'lblMax.Text = "Maximum Weight"
    '    'lblPatients.Text = "Patient Weight"
    '    Dim oclsViewGraphs As New clsViewGraphs
    '    dt = New DataTable
    '    dt = oclsViewGraphs.ScanAgeHtWt()

    '    ' get patient Gender
    '    Dim nGender As New Integer
    '    Dim oclsHPITemplate As New clsHPITemplate
    '    nGender = oclsHPITemplate.GetPatientGender(gnPatientID)
    '    oclsHPITemplate = Nothing

    '    Dim dt_MINMAX As New DataTable

    '    'get STANDARD values for the minmax values from SP 'gsp_viewGraphMinMax' where standard data is stored.
    '    If nGender = 1 Then ' for male
    '        dt_MINMAX = oclsViewGraphs.GetMinMaxValues(Gender.Male, GraphType.Weight)
    '    ElseIf nGender = 2 Then ' for female
    '        dt_MINMAX = oclsViewGraphs.GetMinMaxValues(Gender.Female, GraphType.Weight)
    '    Else ' for other
    '        dt_MINMAX = oclsViewGraphs.GetMinMaxValues(Gender.Male, GraphType.Weight)
    '    End If

    '    'view data in Datagrid
    '    DataGrid1.DataSource = dt_MINMAX

    '    Dim i As Integer '= 20
    '    ' string data where patient Age collection when vital record enterd.
    '    ''Dim AgeCollection(dt.Rows.Count - 1, 2) As String
    '    Dim AgeCollection(dt_MINMAX.Rows.Count - 1, 2) As String

    '    ' string for the data where hight of the patient is collected.
    '    Dim arrsales(dt_MINMAX.Rows.Count - 1, 12) As String

    '    Dim arrGraph(dt_MINMAX.Rows.Count - 1, 3) As String

    '    Dim strPatientInfo As String = Space(5) & " Patient Code:" & Space(1) & gloUC_PatientStrip1.PatientCode & Space(5) & "Patient Name:" & Space(1) & gloUC_PatientStrip1.PatientName & Space(5) & "DOB:" & Space(1) & gloUC_PatientStrip1.PatientDateOfBirth & Space(5) & "Phone No.:" & Space(1) & gloUC_PatientStrip1.PatientPhone & Space(5)
    '    chtPatientGraphs.Footnote.Location.LocationType = MSChart20Lib.VtChLocationType.VtChLocationTypeTop
    '    chtPatientGraphs.Footnote.Text = strPatientInfo

    '    chtPatientGraphs.TitleText = "Age Vs Weight"

    '    Try
    '        With chtPatientGraphs
    '            .chartType = MSChart20Lib.VtChChartType.VtChChartType2dCombination
    '            '.chartType = MSChart20Lib.VtChChartType.VtChChartType2dLine
    '            If dt_MINMAX.Rows.Count >= 1 Then
    '                DataGrid1.DataSource = Nothing
    '                DataGrid1.DataSource = dt
    '                'Axis Labels for the Graph
    '                chtPatientGraphs.Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdX).AxisTitle.Text = "Age (Months)"
    '                chtPatientGraphs.Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).AxisTitle.Text = "Weight (lbs)"

    '                ' get the total number of the vital entries for the patients
    '                '''' Commented By Bipin ' 20070330
    '                'For i = 1 To 36 ' dt_MINMAX.Rows.Count - 1  'dt.Rows.Count - 1
    '                For i = 1 To 37 ' dt_MINMAX.Rows.Count - 1  'dt.Rows.Count - 1
    '                    ' get gender and assign standard Height to the array string
    '                    arrsales(i, 1) = dt_MINMAX.Rows(i)("Agemos")
    '                    arrsales(i, 11) = dt_MINMAX.Rows(i)("P3") / (0.45)
    '                    arrsales(i, 3) = dt_MINMAX.Rows(i)("P5") / (0.45)
    '                    arrsales(i, 4) = dt_MINMAX.Rows(i)("P10") / (0.45)
    '                    arrsales(i, 5) = dt_MINMAX.Rows(i)("P25") / (0.45)
    '                    arrsales(i, 6) = dt_MINMAX.Rows(i)("P50") / (0.45)
    '                    arrsales(i, 7) = dt_MINMAX.Rows(i)("P75") / (0.45)
    '                    arrsales(i, 8) = dt_MINMAX.Rows(i)("P90") / (0.45)
    '                    arrsales(i, 9) = dt_MINMAX.Rows(i)("P95") / (0.45)
    '                    arrsales(i, 10) = dt_MINMAX.Rows(i)("P97") / (0.45)

    '                    AgeCollection(i, 1) = dt_MINMAX.Rows(i)("Agemos")
    '                Next
    '            Else
    '                MessageBox.Show("No vital data available for this patient.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
    '                Exit Sub
    '            End If

    '            'Fill For PAtient
    '            Dim Count As Integer
    '            Count = arrsales.Length / arrsales.GetLength(1)

    '            For i = 0 To Count - 1
    '                For j As Integer = 0 To dt.Rows.Count - 1
    '                    If arrsales(i, 1) = dt.Rows(j)("AGE") Then
    '                        '''' If Age Matches then Add Patients Ht in to The Array 
    '                        If Not IsDBNull(dt.Rows(j)("dWeightinlbs")) Then
    '                            arrsales(i, 2) = dt.Rows(j)("dWeightinlbs") '/ (0.45) 'FtToMtr(ft, Inch)
    '                        Else
    '                            arrsales(i, 2) = 0
    '                        End If
    '                        Exit For
    '                    End If
    '                Next
    '                arrsales(i, 1) = Nothing
    '            Next

    '            'set data to draw chart
    '            .ChartData = CType(arrsales, Object)
    '            ' set graph styles   
    '            '.Plot.Backdrop.Fill.Brush.PatternColor.Set(255, 255, 255)               

    '            Dim cnt As Integer
    '            For cnt = 1 To 12
    '                If cnt <> 3 Then
    '                    .Plot.SeriesCollection(cnt).SeriesType = MSChart20Lib.VtChSeriesType.VtChSeriesType2dLine
    '                    .Plot.SeriesCollection(cnt).Pen.Width = 22
    '                    '.Plot.SeriesCollection(cnt).Pen.VtColor.Set(0, 0, 0)
    '                End If
    '            Next

    '            .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).ValueScale.Minimum = 1
    '            .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).ValueScale.Maximum = 40
    '            .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).ValueScale.MajorDivision = 10
    '            .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).ValueScale.MinorDivision = 1
    '            .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).CategoryScale.DivisionsPerLabel = 1
    '            .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).CategoryScale.DivisionsPerTick = 1

    '            .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdX).ValueScale.MajorDivision = 10
    '            .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdX).ValueScale.MinorDivision = 1
    '            '.Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdX).AxisTitle.VtFont.Size = 1


    '            .Plot.SeriesCollection(3).SeriesType = MSChart20Lib.VtChSeriesType.VtChSeriesType2dLine
    '            ' .Plot.SeriesCollection(3).SeriesMarker.Auto = True
    '            .Plot.SeriesCollection(3).Select()
    '            .Plot.SeriesCollection(3).SeriesMarker.Show = True
    '            .Plot.SeriesCollection(3).ShowLine = False
    '            .Plot.SeriesCollection(3).Pen.Style = MSChart20Lib.VtPenStyle.VtPenStyleDashDotDot
    '            .Plot.SeriesCollection(3).Pen.Width = 6

    '            For i = 1 To .RowCount
    '                .Row = i
    '                .RowLabel = AgeCollection(i - 1, 1)
    '            Next

    '        End With
    '    Catch ex As Exception
    '        MsgBox(ex.ToString)
    '    End Try
    'End Sub
    'Private Sub AgeVsTemprature()
    '    Dim oclsViewGraphs As New clsViewGraphs
    '    dt = New DataTable
    '    dt = oclsViewGraphs.ScanAgeHtWt()
    '    Dim i As Integer '= 20
    '    ' string data where patient Age collection when vital record enterd.
    '    Dim AgeCollection(dt.Rows.Count - 1, 2) As String

    '    ' string for the data where hight of the patient is collected.
    '    Dim arrsales(dt.Rows.Count - 1, 12) As String

    '    Dim strPatientInfo As String = Space(5) & " Patient Code:" & Space(1) & gloUC_PatientStrip1.PatientCode & Space(5) & "Patient Name:" & Space(1) & gloUC_PatientStrip1.PatientName & Space(5) & "DOB:" & Space(1) & gloUC_PatientStrip1.PatientDateOfBirth & Space(5) & "Phone No.:" & Space(1) & gloUC_PatientStrip1.PatientPhone & Space(5)
    '    chtPatientGraphs.Footnote.Location.LocationType = MSChart20Lib.VtChLocationType.VtChLocationTypeTop
    '    chtPatientGraphs.Footnote.Text = strPatientInfo

    '    chtPatientGraphs.TitleText = "Date-Time Vs Temperature"

    '    Try
    '        With chtPatientGraphs
    '            '.chartType = MSChart20Lib.VtChChartType.VtChChartType2dCombination
    '            .chartType = MSChart20Lib.VtChChartType.VtChChartType2dLine
    '            If dt.Rows.Count >= 1 Then
    '                'Axis Labels for the Graph
    '                chtPatientGraphs.Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdX).AxisTitle.Text = "Date-Time"
    '                chtPatientGraphs.Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).AxisTitle.Text = "Temperature"

    '                ' get the total number of the vital entries for the patients
    '                For i = 0 To dt.Rows.Count - 1  ' dt_MINMAX.Rows.Count - 1  'dt.Rows.Count - 1
    '                    ' get gender and assign standard Height to the array string
    '                    If Not IsDBNull(dt.Rows(i)("dtvitaldate")) Then
    '                        arrsales(i, 1) = Convert.ToString(dt.Rows(i)("dtvitaldate"))
    '                    Else
    '                        arrsales(i, 1) = 0
    '                    End If

    '                    If Not IsDBNull(dt.Rows(i)("dTemperature")) Then
    '                        arrsales(i, 2) = Convert.ToString(dt.Rows(i)("dTemperature"))
    '                    Else
    '                        arrsales(i, 2) = 0
    '                    End If

    '                    arrsales(i, 3) = 90 ' dt.Rows(i)("dTemperature")

    '                    If Not IsDBNull(dt.Rows(i)("dTemperature")) Then
    '                        arrsales(i, 4) = Convert.ToString(dt.Rows(i)("dTemperature")) ' 90 'dt.Rows(i)("dTemperature")
    '                    Else
    '                        arrsales(i, 4) = 0
    '                    End If

    '                    arrsales(i, 5) = 110 'dt.Rows(i)("dTemperature")

    '                    If Not IsDBNull(dt.Rows(i)("dTemperature")) Then
    '                        arrsales(i, 6) = Convert.ToString(dt.Rows(i)("dTemperature"))
    '                    Else
    '                        arrsales(i, 6) = 0
    '                    End If


    '                    If Not IsDBNull(dt.Rows(i)("dtvitaldate")) Then
    '                        AgeCollection(i, 1) = Convert.ToString(dt.Rows(i)("dtvitaldate"))
    '                    Else
    '                        AgeCollection(i, 1) = 0
    '                    End If

    '                Next
    '            Else
    '                MessageBox.Show("No vital data available for this patient.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
    '                Me.Close()
    '                Exit Sub
    '            End If

    '            'set data to draw chart
    '            .ChartData = CType(arrsales, Object)
    '            ' set graph styles
    '            '.Plot.Backdrop.Fill.Brush.PatternColor.Set(255, 255, 255)
    '            Dim count_series As Integer = .Plot.SeriesCollection.Count()
    '            Dim cnt As Integer
    '            For cnt = 1 To dt.Rows.Count - 1
    '                If cnt <> 3 Then
    '                    If cnt <= count_series Then
    '                        .Plot.SeriesCollection(cnt).SeriesType = MSChart20Lib.VtChSeriesType.VtChSeriesType2dLine
    '                        .Plot.SeriesCollection(cnt).Pen.Width = 25
    '                        '.Plot.SeriesCollection(cnt).Pen.VtColor.Set(0, 0, 0)
    '                    End If
    '                End If
    '            Next

    '            .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).ValueScale.Minimum = 80
    '            .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).ValueScale.Maximum = 120
    '            .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).ValueScale.MajorDivision = 10
    '            .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).ValueScale.MinorDivision = 1
    '            .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).CategoryScale.DivisionsPerLabel = 1
    '            .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).CategoryScale.DivisionsPerTick = 1

    '            .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdX).ValueScale.MajorDivision = 10
    '            .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdX).ValueScale.MinorDivision = 1
    '            ''.Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdX).AxisTitle.VtFont.Size = 1

    '            .Plot.SeriesCollection(3).SeriesType = MSChart20Lib.VtChSeriesType.VtChSeriesType2dLine
    '            ' .Plot.SeriesCollection(3).SeriesMarker.Auto = True
    '            .Plot.SeriesCollection(3).Select()
    '            .Plot.SeriesCollection(3).SeriesMarker.Show = True
    '            .Plot.SeriesCollection(3).ShowLine = False
    '            .Plot.SeriesCollection(3).Pen.Style = MSChart20Lib.VtPenStyle.VtPenStyleDashDotDot
    '            .Plot.SeriesCollection(3).Pen.Width = 6


    '            ' fill the labels of Y-axis
    '            For i = 1 To .RowCount
    '                .Row = i
    '                .RowLabel = AgeCollection(i - 1, 1)
    '            Next

    '        End With
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Sub
    '' function for the BP Sitting and standing for minimum as well as maximum values.
    'Private Function GraphsForBP(ByVal nBPvalue As Integer)
    '    'get data from SP for the selectd patient
    '    Dim oclsViewGraphs As New clsViewGraphs
    '    dt = New DataTable
    '    dt = oclsViewGraphs.ScanAgeHtWt()
    '    Dim i As Integer '= 20
    '    ' string data where patient Age collection when vital record enterd.
    '    Dim AgeCollection(dt.Rows.Count - 1, 2) As String
    '    ' string for the data where hight of the patient is collected.
    '    Dim arrsales(dt.Rows.Count - 1, 12) As String

    '    If nBPvalue = 1 Then
    '        Dim strPatientInfo As String = Space(5) & " Patient Code:" & Space(1) & gloUC_PatientStrip1.PatientCode & Space(5) & "Patient Name:" & Space(1) & gloUC_PatientStrip1.PatientName & Space(5) & "DOB:" & Space(1) & gloUC_PatientStrip1.PatientDateOfBirth & Space(5) & "Phone No.:" & Space(1) & gloUC_PatientStrip1.PatientPhone & Space(5)
    '        chtPatientGraphs.Footnote.Location.LocationType = MSChart20Lib.VtChLocationType.VtChLocationTypeTop
    '        chtPatientGraphs.Footnote.Text = strPatientInfo
    '        chtPatientGraphs.TitleText = "Date-Time Vs BP Sitting"
    '    Else
    '        Dim strPatientInfo As String = Space(5) & " Patient Code:" & Space(1) & gloUC_PatientStrip1.PatientCode & Space(5) & "Patient Name:" & Space(1) & gloUC_PatientStrip1.PatientName & Space(5) & "DOB:" & Space(1) & gloUC_PatientStrip1.PatientDateOfBirth & Space(5) & "Phone No.:" & Space(1) & gloUC_PatientStrip1.PatientPhone & Space(5)
    '        chtPatientGraphs.Footnote.Location.LocationType = MSChart20Lib.VtChLocationType.VtChLocationTypeTop
    '        chtPatientGraphs.Footnote.Text = strPatientInfo
    '        chtPatientGraphs.TitleText = "Date-Time Vs BP Standing"
    '    End If
    '    Try
    '        With chtPatientGraphs
    '            '.chartType = MSChart20Lib.VtChChartType.VtChChartType2dCombination
    '            .chartType = MSChart20Lib.VtChChartType.VtChChartType2dLine
    '            '.chartType = MSChart20Lib.VtChChartType.VtChChartType2dLine
    '            If dt.Rows.Count >= 1 Then
    '                'Axis Labels for the Graph
    '                chtPatientGraphs.Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdX).AxisTitle.Text = "Date-Time"

    '                If nBPvalue = 1 Then
    '                    chtPatientGraphs.Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).AxisTitle.Text = "BP Sitting"
    '                Else
    '                    chtPatientGraphs.Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).AxisTitle.Text = "BP Standing"
    '                End If

    '                ' get the total number of the vital entries for the patients
    '                For i = 0 To dt.Rows.Count - 1  ' dt_MINMAX.Rows.Count - 1  'dt.Rows.Count - 1
    '                    ' get gender and assign standard Height to the array string
    '                    arrsales(i, 1) = Convert.ToString(dt.Rows(i)("dtvitaldate"))
    '                    'arrsales(i, 2) = dt.Rows(i)("dBloodPressureSittingMin")
    '                    ' standard Minimum values
    '                    arrsales(i, 3) = 20
    '                    'arrsales(i, 4) = dt.Rows(i)("dBloodPressureSittingMin")
    '                    If nBPvalue = 1 Then
    '                        If Not IsDBNull(dt.Rows(i)("dBloodPressureSittingMin")) Then
    '                            arrsales(i, 5) = Convert.ToString(dt.Rows(i)("dBloodPressureSittingMin"))
    '                        Else
    '                            arrsales(i, 5) = 0
    '                        End If

    '                        If Not IsDBNull(dt.Rows(i)("dBloodPressureSittingMax")) Then
    '                            arrsales(i, 6) = Convert.ToString(dt.Rows(i)("dBloodPressureSittingMax"))
    '                        Else
    '                            arrsales(i, 6) = 0
    '                        End If

    '                    Else
    '                        If Not IsDBNull(dt.Rows(i)("dBloodPressureStandingMin")) Then
    '                            arrsales(i, 5) = Convert.ToString(dt.Rows(i)("dBloodPressureStandingMin"))
    '                        Else
    '                            arrsales(i, 5) = 0
    '                        End If

    '                        If Not IsDBNull(dt.Rows(i)("dBloodPressureStandingMax")) Then
    '                            arrsales(i, 6) = Convert.ToString(dt.Rows(i)("dBloodPressureStandingMax"))
    '                        Else
    '                            arrsales(i, 6) = 0
    '                        End If

    '                    End If
    '                    ' standard Maximum values
    '                    arrsales(i, 7) = 220
    '                    If Not IsDBNull(dt.Rows(i)("dtvitaldate")) Then
    '                        AgeCollection(i, 1) = Convert.ToString(dt.Rows(i)("dtvitaldate"))
    '                    Else
    '                        AgeCollection(i, 1) = 0
    '                    End If

    '                Next
    '            Else
    '                MessageBox.Show("No vital data available for this patient.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
    '                Exit Function
    '            End If

    '            'set data to draw chart
    '            .ChartData = CType(arrsales, Object)
    '            Dim Count_series As Integer = .Plot.SeriesCollection.Count()
    '            Dim cnt As Integer
    '            For cnt = 1 To dt.Rows.Count - 1
    '                ' If cnt <> 3 Or cnt <> 4 Then
    '                If cnt <= Count_series Then
    '                    .Plot.SeriesCollection(cnt).SeriesType = MSChart20Lib.VtChSeriesType.VtChSeriesType2dLine
    '                    .Plot.SeriesCollection(cnt).Pen.Width = 20
    '                    '.Plot.SeriesCollection(cnt).Pen.VtColor.Set(0, 0, 0)
    '                    '  End If
    '                End If
    '            Next

    '            .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).ValueScale.Minimum = 15
    '            .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).ValueScale.Maximum = 225
    '            .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).ValueScale.MajorDivision = 10
    '            .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).ValueScale.MinorDivision = 1
    '            .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).CategoryScale.DivisionsPerLabel = 1
    '            .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdY).CategoryScale.DivisionsPerTick = 1

    '            .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdX).ValueScale.MajorDivision = 10
    '            .Plot.Axis(MSChart20Lib.VtChAxisId.VtChAxisIdX).ValueScale.MinorDivision = 1

    '            ' fill the labels of Y-axis
    '            For i = 1 To .RowCount
    '                .Row = i
    '                .RowLabel = AgeCollection(i - 1, 1)
    '            Next

    '        End With
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Function
    '--------------------------code commented by sarika vital Graphs 3rd june 08

    Private Sub tblbtn_DtVsWeight_32_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tblbtn_DtVsWeight_32.Click
        DateVsWeight()
    End Sub

    Private Function GetPatientGender(ByVal strGender As String) As Integer
        Try
            Select Case strGender
                Case "Male"
                    GetPatientGender = 1
                Case "Female"
                    GetPatientGender = 2
                Case Else
                    GetPatientGender = 3
            End Select


        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally

        End Try
    End Function
End Class

