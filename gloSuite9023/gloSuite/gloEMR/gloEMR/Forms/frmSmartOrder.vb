Option Compare Text
Imports gloEMRGeneralLibrary
Imports System.Text

Public Class frmSmartOrder


    Inherits System.Windows.Forms.Form
    Dim objclsSmartOrder As ClsSmartorderDBLayer
    Dim objclsSmartDiagnosis As clsSmartDiagnosis
    Dim dtOrderbyCode As DataTable
    Dim dtOrderbyDesc As DataTable

    Private m_VisitID As Long
    Private m_ExamID As Long
    Private m_ProviderID As Long
    Private m_PatientID As Long
    Dim m_ExamDate As DateTime
    Dim arrPE As New ArrayList
    Dim oclsDiagnosis As ClsDiagnosisDBLayer
    Public mycaller As frmPatientExam
    Dim dt_Orderset As DataTable
    Dim OrderNode As New myTreeNode
    Dim arrSelectedNode As New ArrayList
    Dim arrOrders As New ArrayList
    ''''''''''''''' Added by Ujwala - Smart Order Changes Integration - as on 20101016
    Dim objReferralsDBLayer As New ClsReferralsDBLayer
    Dim objclsSmartOrder1 As clsSmartOrder
    Dim _dt As DataTable

    Public Shared nRefTempID As Int64 = 0
    Private m_ExamFilePath As String
    Private ExamProviderId As Long
    Public blnExamFinished As Boolean
    Dim WithEvents frmExamChild As IExamChildEvents
    Public dtDos As DateTime
    Dim ReffCnt As Integer = 0

    ''''''''''''''' Added by Ujwala - Smart Order Changes Integration - as on 20101016
    '' SUDHIR 20090707 '' TO CHECK UNCHECK TREE ''
    Private bParentTrigger As Boolean = True
    Private bChildTrigger As Boolean = True
    '' END SUDHIR ''
    'Added by madan.
    Dim _ClinicID As Long = 0
    Dim _LoginUserID As Long = 0
    Dim appSettings As System.Collections.Specialized.NameValueCollection = System.Configuration.ConfigurationManager.AppSettings
#Region " Windows Controls "
    Friend WithEvents tblSmartOrders As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents tblSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents tblClose As System.Windows.Forms.ToolStripButton
    Friend WithEvents rbSearch2 As System.Windows.Forms.RadioButton
    Friend WithEvents rbSearch1 As System.Windows.Forms.RadioButton
    Friend WithEvents mnuOperOrderAss As System.Windows.Forms.MenuItem
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents pnlSearch As System.Windows.Forms.Panel
    Friend WithEvents lbl_WhiteSpaceTop As System.Windows.Forms.Label
    Friend WithEvents lbl_WhiteSpaceBottom As System.Windows.Forms.Label
    Friend WithEvents PicBx_Search As System.Windows.Forms.PictureBox
    Private WithEvents lbl_pnlSearchBottomBrd As System.Windows.Forms.Label
    Private WithEvents lbl_pnlSearchTopBrd As System.Windows.Forms.Label
    Private WithEvents lbl_pnlSearchLeftBrd As System.Windows.Forms.Label
    Private WithEvents lbl_pnlSearchRightBrd As System.Windows.Forms.Label
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Private WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents Label3 As System.Windows.Forms.Label
    Private WithEvents Label4 As System.Windows.Forms.Label
    Private WithEvents Label5 As System.Windows.Forms.Label
    Private WithEvents lbl_BottomBrd As System.Windows.Forms.Label
    Private WithEvents lbl_LeftBrd As System.Windows.Forms.Label
    Private WithEvents lbl_RightBrd As System.Windows.Forms.Label
    Private WithEvents lbl_TopBrd As System.Windows.Forms.Label
    Private WithEvents Label6 As System.Windows.Forms.Label
    Private WithEvents Label7 As System.Windows.Forms.Label
    Private WithEvents Label8 As System.Windows.Forms.Label
    Private WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents Splitter1 As System.Windows.Forms.Splitter
#End Region

#Region " Windows Form Designer generated code "

    'Public Sub New()
    '    MyBase.New()

    '    'This call is required by the Windows Form Designer.
    '    InitializeComponent()

    '    'Add any initialization after the InitializeComponent() call

    'End Sub

    Public Sub New(ByVal VisitID As Long, ByVal ExamID As Long, ByVal ExamDate As DateTime, ByVal ProviderID As Long, ByVal PatientID As Long)
        MyBase.New()
        m_VisitID = VisitID
        m_ExamID = ExamID
        m_ExamDate = ExamDate
        m_ProviderID = ProviderID
        m_PatientID = PatientID
        'Added by madan to retrieve the appsettings value

        If appSettings IsNot Nothing Then
            _LoginUserID = Convert.ToInt64(appSettings("UserID"))
            _ClinicID = Convert.ToInt64(appSettings("ClinicID"))
        End If


        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
            Try
                gloGlobal.cEventHelper.RemoveAllEventHandlers(Me)
            Catch
            End Try
            Dim dtpContextMenu As ContextMenu() = {cntTags, cntOrderAssociation}
            Try
                gloGlobal.cEventHelper.RemoveAllEventHandlers(dtpContextMenu)
                gloGlobal.cEventHelper.DisposeContextMenu(dtpContextMenu)
            Catch ex As Exception

            End Try


            If (IsNothing(objReferralsDBLayer) = False) Then
                objReferralsDBLayer.Dispose()
                objReferralsDBLayer = Nothing
            End If
            If (IsNothing(objclsSmartOrder) = False) Then
                objclsSmartOrder.Dispose()
                objclsSmartOrder = Nothing
            End If
            If (IsNothing(objclsSmartDiagnosis) = False) Then
                objclsSmartDiagnosis.Dispose()
                objclsSmartDiagnosis = Nothing
            End If
            If (IsNothing(oclsDiagnosis) = False) Then
                oclsDiagnosis.Dispose()
                oclsDiagnosis = Nothing
            End If
            If (IsNothing(objclsSmartOrder1) = False) Then
                objclsSmartOrder1.Dispose()
                objclsSmartOrder1 = Nothing
            End If


        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents txtsearchOrders As System.Windows.Forms.TextBox
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents trOrder As System.Windows.Forms.TreeView
    Friend WithEvents cntOrderAssociation As System.Windows.Forms.ContextMenu
    Friend WithEvents mnuDeleteICD9Item As System.Windows.Forms.MenuItem
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents ImgAssociation As System.Windows.Forms.ImageList
    Friend WithEvents trOrderAssociation As System.Windows.Forms.TreeView
    Friend WithEvents imgSmartOrder As System.Windows.Forms.ImageList
    Friend WithEvents cntTags As System.Windows.Forms.ContextMenu
    Friend WithEvents MenuItem1 As System.Windows.Forms.MenuItem
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSmartOrder))
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.trOrder = New System.Windows.Forms.TreeView()
        Me.imgSmartOrder = New System.Windows.Forms.ImageList(Me.components)
        Me.pnlSearch = New System.Windows.Forms.Panel()
        Me.txtsearchOrders = New System.Windows.Forms.TextBox()
        Me.lbl_WhiteSpaceTop = New System.Windows.Forms.Label()
        Me.lbl_WhiteSpaceBottom = New System.Windows.Forms.Label()
        Me.PicBx_Search = New System.Windows.Forms.PictureBox()
        Me.lbl_pnlSearchBottomBrd = New System.Windows.Forms.Label()
        Me.lbl_pnlSearchTopBrd = New System.Windows.Forms.Label()
        Me.lbl_pnlSearchLeftBrd = New System.Windows.Forms.Label()
        Me.lbl_pnlSearchRightBrd = New System.Windows.Forms.Label()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.rbSearch2 = New System.Windows.Forms.RadioButton()
        Me.rbSearch1 = New System.Windows.Forms.RadioButton()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.lbl_BottomBrd = New System.Windows.Forms.Label()
        Me.lbl_LeftBrd = New System.Windows.Forms.Label()
        Me.lbl_RightBrd = New System.Windows.Forms.Label()
        Me.lbl_TopBrd = New System.Windows.Forms.Label()
        Me.trOrderAssociation = New System.Windows.Forms.TreeView()
        Me.cntTags = New System.Windows.Forms.ContextMenu()
        Me.MenuItem1 = New System.Windows.Forms.MenuItem()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.tblSmartOrders = New gloGlobal.gloToolStripIgnoreFocus()
        Me.tblSave = New System.Windows.Forms.ToolStripButton()
        Me.tblClose = New System.Windows.Forms.ToolStripButton()
        Me.ImgAssociation = New System.Windows.Forms.ImageList(Me.components)
        Me.cntOrderAssociation = New System.Windows.Forms.ContextMenu()
        Me.mnuDeleteICD9Item = New System.Windows.Forms.MenuItem()
        Me.mnuOperOrderAss = New System.Windows.Forms.MenuItem()
        Me.Splitter1 = New System.Windows.Forms.Splitter()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.Panel1.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.pnlSearch.SuspendLayout()
        CType(Me.PicBx_Search, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel6.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.tblSmartOrders.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Panel3)
        Me.Panel1.Controls.Add(Me.pnlSearch)
        Me.Panel1.Controls.Add(Me.Panel6)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel1.Location = New System.Drawing.Point(0, 53)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(240, 563)
        Me.Panel1.TabIndex = 0
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.Label6)
        Me.Panel3.Controls.Add(Me.Label7)
        Me.Panel3.Controls.Add(Me.Label8)
        Me.Panel3.Controls.Add(Me.Label9)
        Me.Panel3.Controls.Add(Me.trOrder)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel3.Location = New System.Drawing.Point(0, 56)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Padding = New System.Windows.Forms.Padding(3, 0, 0, 3)
        Me.Panel3.Size = New System.Drawing.Size(240, 507)
        Me.Panel3.TabIndex = 2
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label6.Location = New System.Drawing.Point(4, 503)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(235, 1)
        Me.Label6.TabIndex = 8
        Me.Label6.Text = "label2"
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(3, 1)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(1, 503)
        Me.Label7.TabIndex = 7
        Me.Label7.Text = "label4"
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label8.Location = New System.Drawing.Point(239, 1)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(1, 503)
        Me.Label8.TabIndex = 6
        Me.Label8.Text = "label3"
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label9.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(3, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(237, 1)
        Me.Label9.TabIndex = 5
        Me.Label9.Text = "label1"
        '
        'trOrder
        '
        Me.trOrder.BackColor = System.Drawing.Color.White
        Me.trOrder.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trOrder.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trOrder.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.trOrder.ForeColor = System.Drawing.Color.Black
        Me.trOrder.HideSelection = False
        Me.trOrder.ImageIndex = 6
        Me.trOrder.ImageList = Me.imgSmartOrder
        Me.trOrder.ItemHeight = 20
        Me.trOrder.Location = New System.Drawing.Point(3, 0)
        Me.trOrder.Name = "trOrder"
        Me.trOrder.SelectedImageIndex = 6
        Me.trOrder.ShowLines = False
        Me.trOrder.Size = New System.Drawing.Size(237, 504)
        Me.trOrder.TabIndex = 1
        '
        'imgSmartOrder
        '
        Me.imgSmartOrder.ImageStream = CType(resources.GetObject("imgSmartOrder.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imgSmartOrder.TransparentColor = System.Drawing.Color.Transparent
        Me.imgSmartOrder.Images.SetKeyName(0, "")
        Me.imgSmartOrder.Images.SetKeyName(1, "")
        Me.imgSmartOrder.Images.SetKeyName(2, "")
        Me.imgSmartOrder.Images.SetKeyName(3, "Drugs.ico")
        Me.imgSmartOrder.Images.SetKeyName(4, "")
        Me.imgSmartOrder.Images.SetKeyName(5, "")
        Me.imgSmartOrder.Images.SetKeyName(6, "")
        Me.imgSmartOrder.Images.SetKeyName(7, "")
        Me.imgSmartOrder.Images.SetKeyName(8, "FLowsheet.ico")
        '
        'pnlSearch
        '
        Me.pnlSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlSearch.Controls.Add(Me.txtsearchOrders)
        Me.pnlSearch.Controls.Add(Me.lbl_WhiteSpaceBottom)
        Me.pnlSearch.Controls.Add(Me.lbl_WhiteSpaceTop)
        Me.pnlSearch.Controls.Add(Me.btnClear)
        Me.pnlSearch.Controls.Add(Me.PicBx_Search)
        Me.pnlSearch.Controls.Add(Me.lbl_pnlSearchBottomBrd)
        Me.pnlSearch.Controls.Add(Me.lbl_pnlSearchTopBrd)
        Me.pnlSearch.Controls.Add(Me.lbl_pnlSearchLeftBrd)
        Me.pnlSearch.Controls.Add(Me.lbl_pnlSearchRightBrd)
        Me.pnlSearch.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlSearch.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlSearch.ForeColor = System.Drawing.Color.Black
        Me.pnlSearch.Location = New System.Drawing.Point(0, 30)
        Me.pnlSearch.Name = "pnlSearch"
        Me.pnlSearch.Padding = New System.Windows.Forms.Padding(3, 0, 0, 3)
        Me.pnlSearch.Size = New System.Drawing.Size(240, 26)
        Me.pnlSearch.TabIndex = 0
        '
        'txtsearchOrders
        '
        Me.txtsearchOrders.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtsearchOrders.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtsearchOrders.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtsearchOrders.ForeColor = System.Drawing.Color.Black
        Me.txtsearchOrders.Location = New System.Drawing.Point(32, 5)
        Me.txtsearchOrders.Name = "txtsearchOrders"
        Me.txtsearchOrders.Size = New System.Drawing.Size(186, 15)
        Me.txtsearchOrders.TabIndex = 0
        '
        'lbl_WhiteSpaceTop
        '
        Me.lbl_WhiteSpaceTop.BackColor = System.Drawing.Color.White
        Me.lbl_WhiteSpaceTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_WhiteSpaceTop.Location = New System.Drawing.Point(32, 1)
        Me.lbl_WhiteSpaceTop.Name = "lbl_WhiteSpaceTop"
        Me.lbl_WhiteSpaceTop.Size = New System.Drawing.Size(186, 4)
        Me.lbl_WhiteSpaceTop.TabIndex = 37
        '
        'lbl_WhiteSpaceBottom
        '
        Me.lbl_WhiteSpaceBottom.BackColor = System.Drawing.Color.White
        Me.lbl_WhiteSpaceBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_WhiteSpaceBottom.Location = New System.Drawing.Point(32, 20)
        Me.lbl_WhiteSpaceBottom.Name = "lbl_WhiteSpaceBottom"
        Me.lbl_WhiteSpaceBottom.Size = New System.Drawing.Size(186, 2)
        Me.lbl_WhiteSpaceBottom.TabIndex = 38
        '
        'PicBx_Search
        '
        Me.PicBx_Search.BackColor = System.Drawing.Color.White
        Me.PicBx_Search.Dock = System.Windows.Forms.DockStyle.Left
        Me.PicBx_Search.Image = CType(resources.GetObject("PicBx_Search.Image"), System.Drawing.Image)
        Me.PicBx_Search.Location = New System.Drawing.Point(4, 1)
        Me.PicBx_Search.Name = "PicBx_Search"
        Me.PicBx_Search.Size = New System.Drawing.Size(28, 21)
        Me.PicBx_Search.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PicBx_Search.TabIndex = 9
        Me.PicBx_Search.TabStop = False
        '
        'lbl_pnlSearchBottomBrd
        '
        Me.lbl_pnlSearchBottomBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlSearchBottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_pnlSearchBottomBrd.Location = New System.Drawing.Point(4, 22)
        Me.lbl_pnlSearchBottomBrd.Name = "lbl_pnlSearchBottomBrd"
        Me.lbl_pnlSearchBottomBrd.Size = New System.Drawing.Size(235, 1)
        Me.lbl_pnlSearchBottomBrd.TabIndex = 35
        Me.lbl_pnlSearchBottomBrd.Text = "label1"
        '
        'lbl_pnlSearchTopBrd
        '
        Me.lbl_pnlSearchTopBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlSearchTopBrd.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_pnlSearchTopBrd.Location = New System.Drawing.Point(4, 0)
        Me.lbl_pnlSearchTopBrd.Name = "lbl_pnlSearchTopBrd"
        Me.lbl_pnlSearchTopBrd.Size = New System.Drawing.Size(235, 1)
        Me.lbl_pnlSearchTopBrd.TabIndex = 36
        Me.lbl_pnlSearchTopBrd.Text = "label1"
        '
        'lbl_pnlSearchLeftBrd
        '
        Me.lbl_pnlSearchLeftBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlSearchLeftBrd.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_pnlSearchLeftBrd.Location = New System.Drawing.Point(3, 0)
        Me.lbl_pnlSearchLeftBrd.Name = "lbl_pnlSearchLeftBrd"
        Me.lbl_pnlSearchLeftBrd.Size = New System.Drawing.Size(1, 23)
        Me.lbl_pnlSearchLeftBrd.TabIndex = 39
        Me.lbl_pnlSearchLeftBrd.Text = "label4"
        '
        'lbl_pnlSearchRightBrd
        '
        Me.lbl_pnlSearchRightBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlSearchRightBrd.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_pnlSearchRightBrd.Location = New System.Drawing.Point(239, 0)
        Me.lbl_pnlSearchRightBrd.Name = "lbl_pnlSearchRightBrd"
        Me.lbl_pnlSearchRightBrd.Size = New System.Drawing.Size(1, 23)
        Me.lbl_pnlSearchRightBrd.TabIndex = 40
        Me.lbl_pnlSearchRightBrd.Text = "label4"
        '
        'Panel6
        '
        Me.Panel6.Controls.Add(Me.Panel2)
        Me.Panel6.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel6.Location = New System.Drawing.Point(0, 0)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Padding = New System.Windows.Forms.Padding(3, 3, 0, 3)
        Me.Panel6.Size = New System.Drawing.Size(240, 30)
        Me.Panel6.TabIndex = 17
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(227, Byte), Integer), CType(CType(254, Byte), Integer))
        Me.Panel2.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.Panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel2.Controls.Add(Me.Label2)
        Me.Panel2.Controls.Add(Me.Label3)
        Me.Panel2.Controls.Add(Me.Label4)
        Me.Panel2.Controls.Add(Me.Label5)
        Me.Panel2.Controls.Add(Me.Label1)
        Me.Panel2.Controls.Add(Me.rbSearch2)
        Me.Panel2.Controls.Add(Me.rbSearch1)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(3, 3)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(237, 24)
        Me.Panel2.TabIndex = 0
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label2.Location = New System.Drawing.Point(1, 23)
        Me.Label2.Name = "Label2"
        Me.Label2.Padding = New System.Windows.Forms.Padding(3)
        Me.Label2.Size = New System.Drawing.Size(235, 1)
        Me.Label2.TabIndex = 10
        Me.Label2.Text = "label2"
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(0, 1)
        Me.Label3.Name = "Label3"
        Me.Label3.Padding = New System.Windows.Forms.Padding(3)
        Me.Label3.Size = New System.Drawing.Size(1, 23)
        Me.Label3.TabIndex = 9
        Me.Label3.Text = "label4"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label4.Location = New System.Drawing.Point(236, 1)
        Me.Label4.Name = "Label4"
        Me.Label4.Padding = New System.Windows.Forms.Padding(3)
        Me.Label4.Size = New System.Drawing.Size(1, 23)
        Me.Label4.TabIndex = 8
        Me.Label4.Text = "label3"
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(0, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Padding = New System.Windows.Forms.Padding(3)
        Me.Label5.Size = New System.Drawing.Size(237, 1)
        Me.Label5.TabIndex = 7
        Me.Label5.Text = "label1"
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(0, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(237, 24)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "  Orders"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'rbSearch2
        '
        Me.rbSearch2.BackColor = System.Drawing.Color.Transparent
        Me.rbSearch2.Checked = True
        Me.rbSearch2.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbSearch2.Location = New System.Drawing.Point(189, 4)
        Me.rbSearch2.Name = "rbSearch2"
        Me.rbSearch2.Size = New System.Drawing.Size(44, 18)
        Me.rbSearch2.TabIndex = 5
        Me.rbSearch2.TabStop = True
        Me.rbSearch2.Text = "Description"
        Me.rbSearch2.UseVisualStyleBackColor = False
        Me.rbSearch2.Visible = False
        '
        'rbSearch1
        '
        Me.rbSearch1.BackColor = System.Drawing.Color.Transparent
        Me.rbSearch1.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbSearch1.Location = New System.Drawing.Point(128, 3)
        Me.rbSearch1.Name = "rbSearch1"
        Me.rbSearch1.Size = New System.Drawing.Size(55, 18)
        Me.rbSearch1.TabIndex = 4
        Me.rbSearch1.Text = "ICD9 Code"
        Me.rbSearch1.UseVisualStyleBackColor = False
        Me.rbSearch1.Visible = False
        '
        'Panel5
        '
        Me.Panel5.Controls.Add(Me.lbl_BottomBrd)
        Me.Panel5.Controls.Add(Me.lbl_LeftBrd)
        Me.Panel5.Controls.Add(Me.lbl_RightBrd)
        Me.Panel5.Controls.Add(Me.lbl_TopBrd)
        Me.Panel5.Controls.Add(Me.trOrderAssociation)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel5.Location = New System.Drawing.Point(244, 53)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Padding = New System.Windows.Forms.Padding(0, 3, 3, 3)
        Me.Panel5.Size = New System.Drawing.Size(590, 563)
        Me.Panel5.TabIndex = 1
        '
        'lbl_BottomBrd
        '
        Me.lbl_BottomBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_BottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_BottomBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_BottomBrd.Location = New System.Drawing.Point(1, 559)
        Me.lbl_BottomBrd.Name = "lbl_BottomBrd"
        Me.lbl_BottomBrd.Size = New System.Drawing.Size(585, 1)
        Me.lbl_BottomBrd.TabIndex = 8
        Me.lbl_BottomBrd.Text = "label2"
        '
        'lbl_LeftBrd
        '
        Me.lbl_LeftBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_LeftBrd.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_LeftBrd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_LeftBrd.Location = New System.Drawing.Point(0, 4)
        Me.lbl_LeftBrd.Name = "lbl_LeftBrd"
        Me.lbl_LeftBrd.Size = New System.Drawing.Size(1, 556)
        Me.lbl_LeftBrd.TabIndex = 7
        Me.lbl_LeftBrd.Text = "label4"
        '
        'lbl_RightBrd
        '
        Me.lbl_RightBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_RightBrd.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_RightBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_RightBrd.Location = New System.Drawing.Point(586, 4)
        Me.lbl_RightBrd.Name = "lbl_RightBrd"
        Me.lbl_RightBrd.Size = New System.Drawing.Size(1, 556)
        Me.lbl_RightBrd.TabIndex = 6
        Me.lbl_RightBrd.Text = "label3"
        '
        'lbl_TopBrd
        '
        Me.lbl_TopBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_TopBrd.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_TopBrd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_TopBrd.Location = New System.Drawing.Point(0, 3)
        Me.lbl_TopBrd.Name = "lbl_TopBrd"
        Me.lbl_TopBrd.Size = New System.Drawing.Size(587, 1)
        Me.lbl_TopBrd.TabIndex = 5
        Me.lbl_TopBrd.Text = "label1"
        '
        'trOrderAssociation
        '
        Me.trOrderAssociation.BackColor = System.Drawing.Color.White
        Me.trOrderAssociation.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trOrderAssociation.CheckBoxes = True
        Me.trOrderAssociation.ContextMenu = Me.cntTags
        Me.trOrderAssociation.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trOrderAssociation.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.trOrderAssociation.ForeColor = System.Drawing.Color.Black
        Me.trOrderAssociation.HideSelection = False
        Me.trOrderAssociation.ImageIndex = 6
        Me.trOrderAssociation.ImageList = Me.imgSmartOrder
        Me.trOrderAssociation.Indent = 21
        Me.trOrderAssociation.ItemHeight = 20
        Me.trOrderAssociation.Location = New System.Drawing.Point(0, 3)
        Me.trOrderAssociation.Name = "trOrderAssociation"
        Me.trOrderAssociation.SelectedImageIndex = 6
        Me.trOrderAssociation.ShowLines = False
        Me.trOrderAssociation.Size = New System.Drawing.Size(587, 557)
        Me.trOrderAssociation.TabIndex = 4
        '
        'cntTags
        '
        Me.cntTags.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem1})
        '
        'MenuItem1
        '
        Me.MenuItem1.Index = 0
        Me.MenuItem1.Text = "Remove Item"
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(227, Byte), Integer), CType(CType(254, Byte), Integer))
        Me.Panel4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel4.Controls.Add(Me.tblSmartOrders)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel4.Location = New System.Drawing.Point(0, 0)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(834, 53)
        Me.Panel4.TabIndex = 3
        '
        'tblSmartOrders
        '
        Me.tblSmartOrders.BackColor = System.Drawing.Color.Transparent
        Me.tblSmartOrders.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tblSmartOrders.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tblSmartOrders.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblSmartOrders.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.tblSmartOrders.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tblSmartOrders.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tblSave, Me.tblClose})
        Me.tblSmartOrders.Location = New System.Drawing.Point(0, 0)
        Me.tblSmartOrders.Name = "tblSmartOrders"
        Me.tblSmartOrders.Size = New System.Drawing.Size(834, 53)
        Me.tblSmartOrders.TabIndex = 0
        Me.tblSmartOrders.Text = "ToolStrip1"
        '
        'tblSave
        '
        Me.tblSave.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblSave.Image = CType(resources.GetObject("tblSave.Image"), System.Drawing.Image)
        Me.tblSave.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblSave.Name = "tblSave"
        Me.tblSave.Size = New System.Drawing.Size(66, 50)
        Me.tblSave.Tag = "Save"
        Me.tblSave.Text = "&Save&&Cls"
        Me.tblSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tblSave.ToolTipText = "Save and Close"
        '
        'tblClose
        '
        Me.tblClose.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblClose.Image = CType(resources.GetObject("tblClose.Image"), System.Drawing.Image)
        Me.tblClose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblClose.Name = "tblClose"
        Me.tblClose.Size = New System.Drawing.Size(43, 50)
        Me.tblClose.Tag = "Close"
        Me.tblClose.Text = "&Close"
        Me.tblClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tblClose.ToolTipText = "Close"
        '
        'ImgAssociation
        '
        Me.ImgAssociation.ImageStream = CType(resources.GetObject("ImgAssociation.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImgAssociation.TransparentColor = System.Drawing.Color.Transparent
        Me.ImgAssociation.Images.SetKeyName(0, "")
        Me.ImgAssociation.Images.SetKeyName(1, "")
        '
        'cntOrderAssociation
        '
        Me.cntOrderAssociation.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuDeleteICD9Item, Me.mnuOperOrderAss})
        '
        'mnuDeleteICD9Item
        '
        Me.mnuDeleteICD9Item.Index = 0
        Me.mnuDeleteICD9Item.Text = "Remove Item"
        '
        'mnuOperOrderAss
        '
        Me.mnuOperOrderAss.Index = 1
        Me.mnuOperOrderAss.Text = "Edit Item"
        '
        'Splitter1
        '
        Me.Splitter1.Location = New System.Drawing.Point(240, 53)
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(4, 563)
        Me.Splitter1.TabIndex = 4
        Me.Splitter1.TabStop = False
        '
        'btnClear
        '
        Me.btnClear.BackColor = System.Drawing.Color.White
        Me.btnClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnClear.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnClear.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnClear.FlatAppearance.BorderColor = System.Drawing.Color.White
        Me.btnClear.FlatAppearance.BorderSize = 0
        Me.btnClear.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnClear.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClear.Image = CType(resources.GetObject("btnClear.Image"), System.Drawing.Image)
        Me.btnClear.Location = New System.Drawing.Point(218, 1)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(21, 21)
        Me.btnClear.TabIndex = 41
        Me.btnClear.UseVisualStyleBackColor = False
        '
        'frmSmartOrder
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(834, 616)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.Splitter1)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Panel4)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmSmartOrder"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Smart Order"
        Me.Panel1.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.pnlSearch.ResumeLayout(False)
        Me.pnlSearch.PerformLayout()
        CType(Me.PicBx_Search, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel6.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel5.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        Me.tblSmartOrders.ResumeLayout(False)
        Me.tblSmartOrders.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub frmSmartOrder_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.DoubleBuffered = True
            FillOrders()

            ''''''''''''''' Added by Ujwala - Smart Order Changes Integration - as on 20101016
            objclsSmartOrder1 = New clsSmartOrder
            ' '' ''_dt = New DataTable
            _dt = Nothing
            _dt = objclsSmartOrder1.FetchSmartOrder()
            ''''''''''''''' Added by Ujwala - Smart Order Changes Integration - as on 20101016

            '' To refresh the txtDrugs 
            Call RefreshSearch()
            txtsearchOrders.Focus()
            ''Added Rahul P on 20101011
            Try
                gloPatient.gloPatient.GetWindowTitle(Me, m_PatientID, GetConnectionString(), gstrMessageBoxCaption)
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            End Try
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartOrders, gloAuditTrail.ActivityType.View, "Smart Orders Opened", m_PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            ''
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartOrders, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, "Smart Orders", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub FillOrders()
        trOrderAssociation.Nodes.Clear()
        objclsSmartOrder = New ClsSmartorderDBLayer
        trOrderAssociation.AllowDrop = True

        Dim rootnode As myTreeNode
        Dim i As Integer
        '  Dim dt As DataTable
        rootnode = New myTreeNode("Order Templates", -1)

        rootnode.ImageIndex = 5
        rootnode.SelectedImageIndex = 5

        trOrder.Nodes.Add(rootnode)
        dt_Orderset = objclsSmartOrder.FillControl(0) ' .FillOrder(0)

        'Populate Order Data
        For i = 0 To dt_Orderset.Rows.Count - 1
            Dim mychildnode As myTreeNode
            ''mychildnode = New myTreeNode(dt.Rows(i)(1), dt.Rows(i)(0) , CType(dt.Rows(i)(2), String))
            mychildnode = New myTreeNode(dt_Orderset.Rows(i)(1), dt_Orderset.Rows(i)(0))
            mychildnode.ImageIndex = 4
            mychildnode.SelectedImageIndex = 4
            mychildnode.Tag = dt_Orderset.Rows(i)(0)
            trOrder.Nodes.Item(0).Nodes.Add(mychildnode)
        Next
        trOrder.ExpandAll()
        '  trOrder.Select()

        rootnode = New myTreeNode("Order Set", -1)
        rootnode.ImageIndex = 6
        rootnode.SelectedImageIndex = 6
        trOrderAssociation.Nodes.Add(rootnode)

        trOrderAssociation.SelectedNode = trOrderAssociation.Nodes(0)
    End Sub

    '''' If Smart_Diagnosis already Exists
    Private Sub Load_Diagnosis()
        Dim dt As DataTable
        '''' >>>>>>>>> To Insert Order <<<<<<<<<  '''''

        '''' get Dignosised ICD9(s) for selected Exam
        dt = objclsSmartDiagnosis.ScanDiagnosis(m_ExamID, m_VisitID)

        If IsNothing(dt) = False Then
            If dt.Rows.Count > 0 Then
                '' if there Exists Diagnosis
                Dim arrICD9 As New ArrayList
                Dim r As DataRow
                For Each r In dt.Rows
                    arrICD9.Add(CType(r(0), String) & "-" & CType(r(1), String))
                Next

                Dim ICD9Node As TreeNode
                For Each ICD9Node In trOrder.Nodes(0).Nodes
                    Dim i As Int16
                    For i = 0 To arrICD9.Count - 1
                        If ICD9Node.Text = CType(arrICD9(i), String) Then
                            '' If ICD9COde-Discription Mathches with TreeNode then 
                            '' then add that ICD9 to associated treeview
                            Dim sender As Object = Nothing
                            Dim earg As System.EventArgs = Nothing

                            trOrder.SelectedNode = ICD9Node

                            trOrder_DoubleClick(sender, earg)

                            trOrderAssociation.SelectedNode.Checked = True

                            Call Load_Treatment(trOrderAssociation.SelectedNode)
                            Call Load_Drugs(trOrderAssociation.SelectedNode)

                        End If
                    Next
                Next
                ''''
            End If
        End If

    End Sub

    Private Sub Load_Treatment(ByVal ICD9Node As myTreeNode)
        Dim obj As New ClsTreatmentDBLayer
        Dim dtCPT As DataTable
        Dim strICD9 As String()
        Dim strICD9code As String = ""
        Dim strICD9Desc As String = ""

        strICD9 = Split(ICD9Node.Name, "-", 2)

        strICD9code = strICD9.GetValue(0)
        strICD9Desc = strICD9.GetValue(1)
        '' To Get CPTs of the Selected ICD9
        dtCPT = obj.FetchCPTforUpdate(m_ExamID, strICD9code, strICD9Desc)

        'objclsSmartDiagnosis.dtTreatment = dtCPT

        obj = Nothing

        If IsNothing(dtCPT) = False Then
            If dtCPT.Rows.Count > 0 Then
                '' if there Exists Treatment (CPT)
                Dim arrCPT As New ArrayList
                Dim r As DataRow
                For Each r In dtCPT.Rows
                    arrCPT.Add(CType(r(0), String) & "-" & CType(r(1), String))
                Next

                ' Dim ICD9Node As TreeNode
                '    Dim Count As Int16
                Dim Count1 As Int16
                For Count1 = 0 To ICD9Node.GetNodeCount(False) - 1
                    If ICD9Node.Nodes(Count1).Text = "CPT" And CType(ICD9Node.Nodes(Count1), myTreeNode).Key = -1 Then
                        Dim CPTNode As TreeNode
                        ' CPTNode = trICD9Association.Nodes(0).Nodes(Count).Nodes(Count1)
                        For Each CPTNode In ICD9Node.Nodes(Count1).Nodes
                            Dim i As Int16
                            For i = 0 To arrCPT.Count - 1
                                If CPTNode.Text = CType(arrCPT(i), String) Then
                                    '' If ICD9COde-Discription Mathches with TreeNode then 
                                    '' then add that ICD9 to associated treeview
                                    CPTNode.Checked = True
                                    Exit For
                                End If
                            Next
                        Next
                    End If
                Next
                '     End If
                '  Next
                ''''
            End If
        End If

    End Sub

    Private Sub Load_Drugs(ByVal ICD9Node As myTreeNode)
        'Dim obj As New ClsTreatmentDBLayer
        Dim dtDrugs As DataTable
        dtDrugs = objclsSmartDiagnosis.FetchDrugsFromPrescription(m_VisitID, m_ExamDate)

        '' 20070630
        'objclsSmartDiagnosis.dtDrugs = dtDrugs

        If IsNothing(dtDrugs) = False Then
            If dtDrugs.Rows.Count > 0 Then
                '' if there Exists Treatment (CPT)
                Dim arrDrugs As New ArrayList
                Dim r As DataRow
                For Each r In dtDrugs.Rows
                    arrDrugs.Add(CType(r(0), String)) ''& "-" & CType(r(1), String))
                Next

                ' Dim ICD9Node As TreeNode
                ' Dim Count As Int16
                'For Count = 0 To trICD9Association.Nodes(0).GetNodeCount(False) - 1
                ' If ICD9Node.Text = trICD9Association.Nodes(0).Nodes(Count).Text Then
                Dim Count1 As Int16
                For Count1 = 0 To ICD9Node.GetNodeCount(False) - 1
                    If ICD9Node.Nodes(Count1).Text = "Drugs" And CType(ICD9Node.Nodes(Count1), myTreeNode).Key = -1 Then
                        Dim DrugsNode As TreeNode
                        ' CPTNode = trICD9Association.Nodes(0).Nodes(Count).Nodes(Count1)
                        For Each DrugsNode In ICD9Node.Nodes(Count1).Nodes
                            Dim i As Int16
                            For i = 0 To arrDrugs.Count - 1
                                If DrugsNode.Text = CType(arrDrugs(i), String) Then
                                    '' If ICD9COde-Discription Mathches with TreeNode then 
                                    '' then add that ICD9 to associated treeview
                                    DrugsNode.Checked = True
                                    Exit For
                                End If
                            Next
                        Next
                    End If
                Next
                '     End If
                '  Next
                ''''
            End If
        End If

    End Sub


    Private Function EscapeLikeValue(ByVal valueWithoutWildcards As String) As String
        Dim sb As New StringBuilder()
        For i As Integer = 0 To valueWithoutWildcards.Length - 1
            Dim c As Char = valueWithoutWildcards(i)
            If c = "*"c OrElse c = "%"c OrElse c = "["c OrElse c = "]"c Then
                sb.Append("[").Append(c).Append("]")
            ElseIf c = "'"c Then
                sb.Append("''")
            Else
                sb.Append(c)
            End If
        Next
        Return sb.ToString()
    End Function




    Private Sub txtsearchOrders_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtsearchOrders.TextChanged
        Try
            'sarika 26th sept 07
            'implement the instring search
            Dim dt As New DataTable
            'If txtsearchDrugs.Tag <> Trim(txtsearchDrugs.Text) Then
            ' If btnAllDrugs.Dock = DockStyle.Top Then

            'If rbSearch2.Checked = False Then
            '    AddSearchICD9(Trim(txtsearchDrugs.Text), dtOrderbyCode)
            'Else
            '    AddSearchICD9(Trim(txtsearchDrugs.Text), dtOrderbyDesc)
            'End If

            ''ElseIf btnClinicalDrugs.Dock = DockStyle.Top Then
            ''    AddDrugs(Trim(txtsearchCPT.Text))
            ''ElseIf btnFreqDrugs.Dock = DockStyle.Top Then
            ''    AddDrugs(Trim(txtsearchCPT.Text))
            ''End If
            ''If Len(Trim(txtsearchDrug.Text)) = 1 Then
            'txtsearchDrugs.Tag = Trim(txtsearchDrugs.Text)
            'End If
            FillOrderTreeView(dt_Orderset, Trim(txtsearchOrders.Text))
            ' End If

            Exit Sub
            '------------------------------------------------------
            'SLR : 8/5/2014: Code review: What is the purpose of following code : i commented ?

            'If Trim(txtsearchOrders.Text) <> "" Then
            '    If trOrder.Nodes.Item(0).GetNodeCount(False) > 0 Then
            '        Dim mychildnode As myTreeNode
            '        'child node collection
            '        For Each mychildnode In trOrder.Nodes.Item(0).Nodes
            '            'search against Description
            '            Dim strcode As String

            '            strcode = Splittext(mychildnode.Text)
            '            If rbSearch2.Checked = True Then
            '                Dim strICD9 As String
            '                strICD9 = Mid(mychildnode.Text, Len(strcode) + 2, Len(Trim(mychildnode.Text)))
            '                If UCase(Mid(strICD9, 1, Len(Trim(txtsearchOrders.Text)))) = UCase(Trim(txtsearchOrders.Text)) Then
            '                    '*************code added by sagar for showing the selected drug at the top on 4 july 2007
            '                    trOrder.SelectedNode = trOrder.SelectedNode.LastNode
            '                    '*************
            '                    'select matching node
            '                    trOrder.SelectedNode = mychildnode
            '                    txtsearchOrders.Focus()
            '                    Exit Sub
            '                End If
            '            Else
            '                'search against ICD9 Code
            '                strcode = Mid(strcode, 1, Len(Trim(txtsearchOrders.Text)))
            '                If UCase(strcode) = UCase(Trim(txtsearchOrders.Text)) Then
            '                    '*************code added by sagar for showing the selected drug at the top on 4 july 2007
            '                    trOrder.SelectedNode = trOrder.SelectedNode.LastNode

            '                    '*************
            '                    'select matching node
            '                    trOrder.SelectedNode = mychildnode
            '                    txtsearchOrders.Focus()
            '                    Exit Sub
            '                End If
            '            End If
            '        Next
            '    End If
            'End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartOrders, gloAuditTrail.ActivityType.Query, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, "Smart Orders", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub FillOrderTreeView(ByVal dt As DataTable, ByVal strSearchDetails As String)

        If IsNothing(dt) = False Then
            trOrder.Hide()
            trOrder.Nodes.Clear()
            Dim rootnode As TreeNode
            rootnode = New myTreeNode("Order Templates", -1)
            rootnode.ImageIndex = 5
            rootnode.SelectedImageIndex = 5
            trOrder.Nodes.Add(rootnode)

            Dim dv As DataView
            dv = dt.DefaultView

            '06-Mar-15 Aniket: Resolving Bug #80194 ( Modified): gloEMR: Smt-Orders- Application gives exception on search
            dv.RowFilter = dv.Table.Columns("sDescription").ColumnName & " Like '%" & EscapeLikeValue(Trim(strSearchDetails)) & "%'"
            Dim dt1 As DataTable
            dt1 = dv.ToTable

            Dim i As Integer

            For i = 0 To dt1.Rows.Count - 1
                Dim mychildnode As myTreeNode
                mychildnode = New myTreeNode(dt1.Rows(i)(1), dt1.Rows(i)(0), CType(dt1.Rows(i)(2), String))
                trOrder.Nodes.Item(0).Nodes.Add(mychildnode)
                mychildnode.ImageIndex = 4
                mychildnode.SelectedImageIndex = 4
            Next

            If trOrder.Nodes(0).GetNodeCount(False) > 0 Then
                trOrder.SelectedNode = trOrder.Nodes(0).Nodes(0)
                trOrder.SelectedNode = trOrder.SelectedNode.LastNode
                trOrder.SelectedNode = trOrder.Nodes(0).Nodes(0)
                txtsearchOrders.Focus()
            End If

        End If
        trOrder.ExpandAll()
        trOrder.Show()
        'trOrder.Select()


    End Sub

    Private Function Splittext(ByVal strsplittext As String) As String
        If Trim(strsplittext) <> "" Then
            Dim arrstring() As String
            arrstring = Split(strsplittext, "-")
            Return arrstring(0)
        Else
            Return ""
        End If
    End Function
    Dim _parentchecked As Boolean = False

    Dim _NodeChecking As Boolean = False

    'Private Sub trOrderAssociation_AfterCheck(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles trOrderAssociation.AfterCheck
    '    If bChildTrigger Then
    '        CheckAllChildren(e.Node, e.Node.Checked)
    '    End If
    '    If bParentTrigger Then
    '        CheckMyParent(e.Node, e.Node.Checked)
    '    End If
    'End Sub

    ''not in use 


    ''not in use 


    Private Sub treeViewCheck(ByVal e_Node As TreeNode, ByVal SelectedNode As TreeNode)
        If IsNothing(e_Node) = False Then

            If (e_Node.GetNodeCount(True) > 0) Then
                ''If parent node checked
                If (e_Node.Checked = True) Then
                    Dim n As TreeNode
                    _NodeChecking = True
                    trOrderAssociation.SelectedNode = e_Node
                    For Each n In e_Node.Nodes
                        If (n.Checked = False) Then
                            n.Checked = True
                        End If
                    Next
                    _NodeChecking = False
                Else ''If parent node checked
                    Dim n As TreeNode
                    'If (_parentchecked = False) Then
                    '  trOrderAssociation.SelectedNode = e_Node
                    If (trOrderAssociation.SelectedNode.Text = e_Node.Text) Then
                        For Each n In e_Node.Nodes
                            If (n.Checked = True) Then
                                n.Checked = False
                            End If
                        Next
                    End If
                End If
            Else ''If childnode checked or unchecked
                '  Dim n As TreeNode

                If (e_Node.Checked = False) Then
                    Dim _Dselectall As Boolean = True
                    Dim node As TreeNode
                    _NodeChecking = True
                    For Each node In e_Node.Parent.Nodes
                        If (node.Checked = False) Then
                            If (e_Node.Parent.Checked = True) Then
                                trOrderAssociation.SelectedNode = e_Node
                                e_Node.Parent.Checked = False
                                Exit For
                            End If
                            _Dselectall = False
                        End If
                    Next
                    _NodeChecking = False
                    If (_Dselectall = True) Then
                        e_Node.Parent.Checked = False
                        _parentchecked = True
                    End If
                Else
                    Dim _selectall As Boolean = True
                    Dim node As TreeNode
                    _NodeChecking = True
                    For Each node In e_Node.Parent.Nodes
                        If (node.Checked = False) Then
                            _selectall = False
                        End If
                    Next
                    _NodeChecking = False
                    If (_selectall = True) Then
                        e_Node.Parent.Checked = True
                        _parentchecked = True
                    End If
                End If
            End If
        End If
    End Sub


    ''Private Function IsAllChecked(ByVal oTree As TreeNode) As Boolean
    ''    For Each oNode As TreeNode In oTree.Nodes
    ''        If IsAllChecked(oNode) = True Then
    ''            oNode.Checked = True
    ''        Else
    ''            oNode.Checked = False
    ''        End If
    ''        If oNode.Checked = False Then
    ''            Return False
    ''        End If
    ''    Next
    ''    Return True
    ''End Function

    ''Dim IsChecked As Boolean = True
    ''Private Sub trOrderAssociation_AfterCheck(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles trOrderAssociation.AfterCheck
    ''    'If IsChecked Then
    ''    '    IsChecked = False
    ''    '    For Each oNode As TreeNode In e.Node.Nodes
    ''    '        oNode.Checked = e.Node.Checked
    ''    '    Next
    ''    '    IsAllChecked(trOrderAssociation.Nodes(0))
    ''    '    IsChecked = True
    ''    'End If

    ''    For Each oNode As TreeNode In e.Node.Nodes
    ''        oNode.Checked = e.Node.Checked
    ''    Next

    ''    If e.Node.Checked = False Then
    ''        e.Node.Parent.Checked = False
    ''    End If
    ''End Sub


    Private Sub trOrderAssociation_DragOver(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles trOrderAssociation.DragOver
        Try
            If IsNothing(trOrderAssociation.SelectedNode) = True Then
                Exit Sub
            End If

            'Check that there is a TreeNode being dragged
            'commented on 30/8/2005 If e.Data.GetDataPresent("System.Windows.Forms.TreeNode", True) = False Then Exit Sub
            If e.Data.GetDataPresent("gloEMR.myTreeNode", True) = False Then Exit Sub

            'Get the TreeView raising the event (incase multiple on form)
            Dim selectedTreeview As TreeView = CType(sender, TreeView)

            'As the mouse moves over nodes, provide feedback to the user
            'by highlighting the node that is the current drop target
            Dim pt As Point = CType(sender, TreeView).PointToClient(New Point(e.X, e.Y))

            'commented on 30/8/2005 Dim targetNode As TreeNode = selectedTreeview.GetNodeAt(pt)
            Dim targetNode As myTreeNode = selectedTreeview.GetNodeAt(pt)

            'See if the targetNode is currently selected, if so no need to validate again
            'If Not (selectedTreeview Is targetNode) Then
            '    'Select the node currently under the cursor
            '    selectedTreeview.SelectedNode = targetNode


            '    'Check that the selected node is not the dropNode and also that it
            '    'is not a child of the dropNode and therefore an invalid target
            '    Dim dropNode As TreeNode = CType(e.Data.GetData("gloEMR.myTreeNode"), myTreeNode)
            '    Do Until targetNode Is Nothing
            '        If targetNode Is dropNode Then
            '            e.Effect = DragDropEffects.None
            '            Exit Sub
            '        End If
            '        targetNode = targetNode.Parent
            '    Loop
            'End If

            'Currently selected node is a suitable target, allow the move
            e.Effect = DragDropEffects.Move
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartOrders, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, "Smart Orders", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub trOrder_ItemDrag(ByVal sender As Object, ByVal e As System.Windows.Forms.ItemDragEventArgs) Handles trOrder.ItemDrag
        ' DoDragDrop(e.Item, DragDropEffects.Move)
    End Sub

    Private Sub trOrder_DragDrop(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles trOrder.DragDrop, trOrderAssociation.DragDrop
        'Try
        '    If IsNothing(trOrderAssociation.SelectedNode) = True Then
        '        Exit Sub
        '    End If

        '    'Check that there is a TreeNode being dragged

        '    'commented on 30/08/2005 If e.Data.GetDataPresent("System.Windows.Forms.TreeNode", True) = False Then Exit Sub
        '    If e.Data.GetDataPresent("gloEMR.myTreeNode", True) = False Then Exit Sub

        '    'Get the TreeView raising the event (incase multiple on form)
        '    Dim selectedTreeview As TreeView = CType(sender, TreeView)

        '    'Get the TreeNode being dragged
        '    'commented on 30/08/2005 Dim dropNode As TreeNode = CType(e.Data.GetData("System.Windows.Forms.TreeNode"), TreeNode)

        '    Dim dropNode As myTreeNode = CType(e.Data.GetData("gloEMR.myTreeNode"), myTreeNode)

        '    'The target node should be selected from the DragOver event

        '    'commented on 30/08/2005 Dim targetNode As TreeNode = selectedTreeview.SelectedNode

        '    Dim targetNode As myTreeNode = CType(selectedTreeview.SelectedNode, myTreeNode)

        '    'Remove the drop node from its current location

        '    'If there is no targetNode add dropNode to the bottom of the TreeView root
        '    'nodes, otherwise add it to the end of the dropNode child nodes
        '    'If targetNode Is Nothing Then
        '    '    dropNode.Remove()
        '    '    selectedTreeview.Nodes.Add(dropNode)
        '    '    AddControl()
        '    '    PopulateMedication(dropNode.Key)

        '    'targetnode is the node selected on which the dropnode is to be dropped.
        '    'If targetNode Is selectedTreeview.Nodes.Item(0) Then
        '    'If Not IsNothing(dropNode) Then
        '    '    AddNode(dropNode)
        '    'End If
        '    'commented from 14/09/2005
        '    If dropNode.Parent Is trOrder.Nodes.Item(0) Then
        '        Dim str As String
        '        str = dropNode.Key
        '        Dim mytragetnode As myTreeNode
        '        For Each mytragetnode In trOrderAssociation.Nodes.Item(0).Nodes
        '            If mytragetnode.Key = str Then
        '                Exit Sub
        '            End If
        '        Next
        '        'dropNode.Remove()
        '        'If PopulateMedication(dropNode.Key) Then
        '        Dim associatenode As myTreeNode

        '        associatenode = dropNode.Clone
        '        associatenode.Key = dropNode.Key
        '        associatenode.Text = dropNode.Text
        '        associatenode.NodeName = dropNode.NodeName
        '        associatenode.ImageIndex = 5
        '        associatenode.SelectedImageIndex = 5

        '        associatenode.Checked = True

        '        trOrderAssociation.Nodes.Item(0).Nodes.Add(associatenode)

        '        Dim mychild As myTreeNode

        '        mychild = New myTreeNode("CPT", -1)
        '        mychild.ImageIndex = 0
        '        mychild.SelectedImageIndex = 0
        '        associatenode.Nodes.Add(mychild)

        '        mychild = New myTreeNode("Drugs", -1)
        '        mychild.ImageIndex = 1
        '        mychild.SelectedImageIndex = 1
        '        associatenode.Nodes.Add(mychild)

        '        mychild = New myTreeNode("Patient Education", -1)
        '        mychild.ImageIndex = 2
        '        mychild.SelectedImageIndex = 2
        '        associatenode.Nodes.Add(mychild)

        '        mychild = New myTreeNode("Tags", -1)
        '        mychild.ImageIndex = 3
        '        mychild.SelectedImageIndex = 3
        '        associatenode.Nodes.Add(mychild)

        '        'associatenode.Nodes.Add(New myTreeNode("CPT", -1))
        '        'associatenode.Nodes.Add(New myTreeNode("Drugs", -1))
        '        'associatenode.Nodes.Add(New myTreeNode("Patient Education", -1))
        '        'associatenode.Nodes.Add(New myTreeNode("Tags", -1))


        '        Dim dt As DataTable
        '        dt = objclsSmartDiagnosis.FetchICD9forUpdate(dropNode.Key)
        '        Dim i As Integer
        '        For i = 0 To dt.Rows.Count - 1
        '            If dt.Rows(i).Item(2) = "c" Then
        '                ''                                   myTreeNode(    StrName             Key
        '                associatenode.Nodes.Item(0).Nodes.Add(New myTreeNode(dt.Rows(i).Item(1), dt.Rows(i).Item(0)))
        '                Call Load_Treatment(associatenode)
        '            ElseIf dt.Rows(i).Item(2) = "d" Then
        '                associatenode.Nodes.Item(1).Nodes.Add(New myTreeNode(dt.Rows(i).Item(1), dt.Rows(i).Item(0)))
        '            ElseIf dt.Rows(i).Item(2) = "t" Then
        '                associatenode.Nodes.Item(2).Nodes.Add(New myTreeNode(dt.Rows(i).Item(1), dt.Rows(i).Item(0)))
        '            End If
        '        Next


        '        trOrderAssociation.ExpandAll()
        '        trOrderAssociation.Select()



        '        'Ensure the newly created node is visible to the user and select it
        '        associatenode.EnsureVisible()
        '        trOrderAssociation.SelectedNode = associatenode

        '        '' To refresh the txtDrugs 
        '        Call RefreshSearch()

        '    End If
        '    'commented from 14/09/2005
        'Catch ex As Exception
        '    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartOrders, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        '    MessageBox.Show(ex.Message, "Smart Diagnosis", MessageBoxButtons.OK, MessageBoxIcon.Error)
        'End Try
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'Me.Close()
        gloWord.WordDialogBoxBackgroundCloser.Close(Me)
    End Sub

    'Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Try
    '        'RefreshICD9()
    '    Catch ex As Exception
    '        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartOrders, gloAuditTrail.ActivityType.Refresh, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
    '        MessageBox.Show(ex.Message, "Smart Diagnosis", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Sub

    Private Sub CheckAllParentNodes()
        ''''''''''''''' Added by Ujwala - Smart Order Changes Integration - as on 20101016
        Dim innerchildflag As Boolean = False
        Dim outerchildflag As Boolean = False
        Dim parentflag As Boolean = False

        For Each ptn As TreeNode In trOrderAssociation.Nodes.Item(0).Nodes
            For Each otherptn As TreeNode In ptn.Nodes
                For Each ootherptn As TreeNode In otherptn.Nodes
                    If ootherptn.Checked = False Then
                        innerchildflag = True
                        Exit For
                    End If
                Next
                If innerchildflag = False And otherptn.Nodes.Count > 0 Then
                    otherptn.Checked = True

                Else

                    outerchildflag = True
                End If
                innerchildflag = False
            Next

            If outerchildflag = False And ptn.Nodes.Count > 0 Then
                ptn.Checked = True
            Else

                parentflag = True
            End If
            outerchildflag = False
        Next
        ''''''''''''''' Added by Ujwala - Smart Order Changes Integration - as on 20101016
    End Sub

    'Private Sub RefreshICD9()
    '    txtsearchOrders.Text = ""
    '    trOrderAssociation.Nodes.Item(0).Nodes.Clear()
    '    trOrder.Nodes.Item(0).Nodes.Clear()
    '    Dim dt As DataTable
    '    dt = objclsSmartDiagnosis.FillICD9()
    '    Dim i As Integer
    '    For i = 0 To dt.Rows.Count - 1
    '        Dim mychildnode As myTreeNode
    '        mychildnode = New myTreeNode(dt.Rows(i)(1), dt.Rows(i)(0))
    '        mychildnode.ImageIndex = 8
    '        mychildnode.SelectedImageIndex = 8
    '        trOrder.Nodes.Item(0).Nodes.Add(mychildnode)
    '    Next
    '    trOrder.ExpandAll()
    '    trOrder.Select()
    '    trOrderAssociation.Nodes(0).Nodes.Clear()
    '    '' To refresh the txtDrugs 
    '    Call RefreshSearch()

    'End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            SaveAssociation()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartOrders, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, "Smart Diagnosis", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    '''' Save Association Remove code Temp Start 
    'Dim arrICD9Only As New ArrayList 'if Only ICD9 is checked
    'Dim arrDruglist As New ArrayList 'for durglist
    'Dim arrExamICD9CPT As New ArrayList 'For cpt with Associatied ICD9
    'Dim arrlist As New ArrayList
    'Dim arrexam As New ArrayList 'arraylist which has ICD9send to exam

    'Dim ICD9Node As New myTreeNode
    'Dim arrICD9CPT As New ArrayList
    'Dim bIsICD9Checked As Boolean = False
    'Dim _ISICD9CPT As Boolean = False
    'Dim lstonlyICD9 As New myList
    'arrexam.Clear()
    '    arrICD9Only.Clear()
    '    arrPE.Clear()
    '''' Save Association Remove code Temp END

    Private Sub SaveAssociation()
        Dim ReffCnt As Integer = 0
        Dim arrLabs As New ArrayList
        Dim arrRadiology As New ArrayList
        Dim arrTemplates As New ArrayList
        Dim arrDrugs As New ArrayList
        Dim OrderNode As myTreeNode

        Dim arrFlow As New ArrayList
        Dim arrexam As New ArrayList
        Dim oclsSmartDiagnosis As New clsSmartDiagnosis


        Dim i As Integer
        Dim _IsRootNode As Boolean = False

        arrLabs.Clear()
        arrRadiology.Clear()
        arrTemplates.Clear()
        For i = 0 To trOrderAssociation.Nodes(0).GetNodeCount(False) - 1 ' Loop for treeview
            If (trOrderAssociation.Nodes(0).GetNodeCount(True) > 0) Then
                OrderNode = trOrderAssociation.Nodes(0).Nodes(i)
                If OrderNode.GetNodeCount(True) > 0 Then
                    For k As Integer = 0 To 4
                        Dim AssociateNode As myTreeNode
                        AssociateNode = OrderNode.Nodes(k)
                        For j As Integer = 0 To AssociateNode.GetNodeCount(False) - 1
                            If AssociateNode.Nodes(j).Checked = True Then
                                Dim lstExam As New myList
                                Dim lst As New myList
                                Dim Emdeonlst As New gloEmdeonCommon.myList '' Added by Abhijeet on 20100625


                                If AssociateNode.Text = "Labs" Or AssociateNode.Text = "Orders and Results" Then '''''''' Or condition by Ujwala as on 20101016 for Smart Order Changes
                                    '' by Abhijeet on 20100625
                                    ''changes : use gloEmdeoncommon.mylist object instead of gloEMR.mylist 
                                    ''lst.Value = AssociateNode.Nodes(j).Text
                                    ''lst.ID = AssociateNode.Nodes(j).Tag
                                    ''arrLabs.Add(lst) 'AssociateNode.Nodes(j).Text)
                                    Emdeonlst.Value = AssociateNode.Nodes(j).Text
                                    Emdeonlst.ID = AssociateNode.Nodes(j).Tag
                                    arrLabs.Add(Emdeonlst)
                                    '' End of changes by Abhijeet on 20100625

                                ElseIf AssociateNode.Text = "Order Templates" Then
                                    lst.Value = AssociateNode.Nodes(j).Text
                                    lst.Index = AssociateNode.Nodes(j).Tag
                                    arrRadiology.Add(lst) 'AssociateNode.Nodes(j).Text)
                                ElseIf AssociateNode.Text = "Referral Letter" Then
                                    lst.Value = AssociateNode.Nodes(j).Text
                                    lst.ID = AssociateNode.Nodes(j).Tag
                                    arrTemplates.Add(lst) 'AssociateNode.Nodes(j).Text)
                                    ReffCnt = ReffCnt + 1
                                    If ReffCnt >= 2 Then

                                        MessageBox.Show("Please select only one Referral ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                        arrTemplates.Clear()
                                        arrFlow.Clear()
                                        arrPE.Clear()
                                        arrLabs.Clear()
                                        arrRadiology.Clear()
                                        arrDrugs.Clear()

                                        arrexam.Clear()

                                        frmPatientExam.nRefTempID = 0
                                        Exit Sub
                                    End If
                                ElseIf AssociateNode.Text = "Drugs" Then
                                    'lst.Value = AssociateNode.Nodes(j).Text
                                    'lst.ID = AssociateNode.Nodes(j).Tag
                                    'arrDrugs.Add(AssociateNode.Nodes(j).Tag) 'AssociateNode.Nodes(j).Text)
                                    Dim oDrug As New gloEMRActors.Drug
                                    Dim oNode As myTreeNode = AssociateNode.Nodes(j)
                                    If IsNothing(oNode) = False Then
                                        oDrug.DrugsID = oNode.Key
                                        oDrug.DrugsName = oNode.DrugName
                                        oDrug.Dosage = oNode.Dosage
                                        oDrug.DrugForm = oNode.DrugForm
                                        oDrug.Route = oNode.Route
                                        oDrug.Frequency = oNode.Frequency
                                        oDrug.NDCCode = oNode.NDCCode
                                        oDrug.IsNarcotics = oNode.IsNarcotics
                                        oDrug.Duration = oNode.Duration
                                        oDrug.nddid = oNode.DDID
                                        oDrug.DrugQtyQualifier = oNode.DrugQtyQualifier

                                        ' '' ''Commented for Fixed BugId 7928 on 20110125 as per new Requirement.
                                        ' '' ''Added for Avoid duplication of Durgs (BugID 6798) on 20101130
                                        ' ''Dim _blnIsDrugExist As Boolean = False

                                        ' ''For DrugCnt As Integer = 0 To arrDrugs.Count - 1
                                        ' ''    If oDrug.NDCCode = DirectCast(DirectCast(arrDrugs.Item(DrugCnt), System.Object), gloEMRGeneralLibrary.gloEMRActors.Drug).NDCCode Then
                                        ' ''        _blnIsDrugExist = True
                                        ' ''        Exit For
                                        ' ''    End If
                                        ' ''Next

                                        ' ''If _blnIsDrugExist = False Then
                                        ' ''    arrDrugs.Add(oDrug)
                                        ' ''End If
                                        ' '' ''End

                                        ''Fixed BugId 7928 on 20110125
                                        arrDrugs.Add(oDrug)
                                        ''End

                                    End If
                                ElseIf AssociateNode.Text = "Flowsheet" Then
                                    lst.Value = AssociateNode.Nodes(j).Text
                                    lst.Index = AssociateNode.Nodes(j).Tag
                                    arrFlow.Add(lst) 'AssociateNode.Nodes(j).Text)

                                    If arrFlow.Count = 1 Then
                                        '' gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.SmartOrders, gloAuditTrail.ActivityType.Add, "Flowsheet Added", gloAuditTrail.ActivityOutCome.Success)
                                        ''Added vijay P on 20100916
                                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.SmartOrders, gloAuditTrail.ActivityType.Add, "Flowsheet Added", 0, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success)
                                        ''
                                    End If
                                End If
                            End If
                            'If AssociateNode.Text = "CPT" Then
                            'arrICD9Only.Add(lstonlyICD9)
                            _IsRootNode = False

                            'End If
                        Next 'For j As Integer = 0 To AssociateNode.GetNodeCount(False) - 1
                    Next  'For k As Integer = 0 To 3
                End If

            End If ''trOrderAssociation.Nodes(0).GetNodeCount(True) > 0
        Next ''For i = 0 To trOrderAssociation.Nodes(0).GetNodeCount(False) - 1


        ''Use datarow for performance
        If Not IsNothing(_dt) Then
            For Each drTask As DataRow In _dt.Rows
                If drTask(1) = "Orders and Results" Then ''drTask(1) for sFieldName
                    If arrLabs.Count > 0 Then
                        If drTask(5) = False Then ''drTask(5) for bSendTask
                            If drTask(2) = True Then ''drTask(2) for bFieldStatus

                                Dim _TestList As String = String.Empty

                                Dim _MyTestList As gloEmdeonCommon.myList = Nothing

                                _TestList = "Lab Tests:" & vbNewLine
                                For index As Integer = 0 To arrLabs.Count - 1
                                    _MyTestList = arrLabs(index)

                                    If index = 0 Then
                                        _TestList += _MyTestList.Value
                                    Else
                                        _TestList += ", " + _MyTestList.Value
                                    End If
                                Next

                                If gloEmdeonInterface.Classes.clsEmdeonGeneral.gloLab_Provider_Usage <> "" Then

                                    Select Case gloEmdeonInterface.Classes.clsEmdeonGeneral.gloLab_Provider_Usage

                                        Case "TASK"
                                            gloLabSettings("TASK", _TestList, arrLabs)
                                        Case "LABORDER"
                                            gloLabSettings("LABORDER", "", arrLabs) ''added to show testnames on Emdeonform ,v8022
                                        Case "RECORDRESULTS"
                                            gloLabSettings("RECORDRESULTS", "", arrLabs)
                                        Case "ASK"
                                            ' new modal dialog for instant choice for next action to be performed.
                                            Dim frmAskform As New gloEmdeonInterface.Forms.frmCnfrmLabFlow()
                                            frmAskform.ShowInTaskbar = False
                                            frmAskform.BringToFront()
                                            frmAskform.ShowDialog(IIf(IsNothing(frmAskform.Parent), Me, frmAskform.Parent))
                                            gloLabSettings(frmAskform.LabFlowConfirm, _TestList, arrLabs)
                                            frmAskform.Dispose()
                                            frmAskform = Nothing
                                        Case Else
                                            MessageBox.Show("Please configure Default user for Task in EMR Admin - Lab Settings", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                            Exit Select
                                    End Select
                                    _TestList = String.Empty
                                Else
                                    Dim frmAskform As New gloEmdeonInterface.Forms.frmCnfrmLabFlow()
                                    frmAskform.ShowInTaskbar = False
                                    frmAskform.BringToFront()
                                    frmAskform.ShowDialog(IIf(IsNothing(frmAskform.Parent), Me, frmAskform.Parent))
                                    gloLabSettings(frmAskform.LabFlowConfirm, _TestList)
                                    frmAskform.Dispose()
                                    frmAskform = Nothing
                                End If

                            Else
                                'Code Start-Added by kanchan on 20100823 for changes in logic
                                Dim frmNormalLab As New gloEmdeonInterface.Forms.frmViewNormalLab(m_PatientID)
                                frmNormalLab.ArrLabs = arrLabs '' Added by Abhijeet on 20100624
                                frmNormalLab.WindowState = FormWindowState.Minimized    '''''' added by Ujwala as on 11252010
                                frmNormalLab.ShowInTaskbar = False
                                frmNormalLab.BringToFront()
                                frmNormalLab.Show()
                                frmNormalLab.Hide()
                                ' RemoveHandler frmNormalLab.Event_CallCDA, AddressOf mdlGeneral.OpenCDA
                                frmNormalLab.Close()
                                frmNormalLab.Dispose()
                                'Code End-Added by kanchan on 20100823 for changes in logic

                            End If  ''bFieldStatus
                        Else
                            '' for assigning task ''
                            If drTask(7) = True Then '' If True Then show the Task form. drTask(7) for bAllowviewtsk
                                If Not IsNothing(arrLabs) Then
                                    '        If arrLabs.Count > 0 Then
                                    Dim dt As DataTable
                                    dt = Nothing
                                    Dim nLabProviderID As Int64
                                    Dim sLabProviderName As String
                                    Dim oPatientExam As New clsPatientExams
                                    Dim nProviderId As Long = oPatientExam.GetProviderIdforPatient(m_PatientID)
                                    oPatientExam.Dispose()
                                    oPatientExam = Nothing
                                    dt = GetLabTaskProvider(nProviderId)

                                    'Code changes by kanchan on 20100618 for Smart Order
                                    If Not IsNothing(dt) Then
                                        If dt.Rows.Count > 0 Then
                                            nLabProviderID = dt.Rows(0)("nProviderID")
                                            sLabProviderName = dt.Rows(0)("ProviderName")
                                        End If
                                        dt.Dispose()
                                        dt = Nothing
                                    End If

                                    Dim strlabs As String = ""
                                    Dim strlab As String = ""
                                    strlab = ""
                                    Dim sDescription As String = "Lab Tests:" & vbNewLine

                                    For olab As Integer = 0 To arrLabs.Count - 1
                                        strlab = CType(arrLabs.Item(olab), gloEmdeonCommon.myList).ID & "~" & CType(arrLabs.Item(olab), gloEmdeonCommon.myList).Value
                                        If olab = 0 Then
                                            strlabs = strlab
                                            sDescription += CType(arrLabs.Item(olab), gloEmdeonCommon.myList).Value
                                        Else
                                            strlabs = strlabs & "|" & strlab
                                            sDescription += ", " & CType(arrLabs.Item(olab), gloEmdeonCommon.myList).Value
                                        End If
                                    Next
                                    ''Added by kanchan on 20100618 for Smart Order
                                    ''Dim ofrm As New gloTaskMail.frmTask(GetConnectionString, 0, m_PatientID, nLabProviderID, sDescription, "Labs available", TaskType.LabOrder, gstrLoginName)
                                    ''Added Rahul on 20101025
                                    Dim ofrm As New gloTaskMail.frmTask
                                    ofrm.DataBaseConnectionString = GetConnectionString()
                                    ofrm.TaskID = 0
                                    ofrm.PatientID = m_PatientID
                                    ofrm.ProviderID = nLabProviderID
                                    ofrm.rtxtDescription.Text = sDescription
                                    ofrm.txtSubject.Text = "Place Lab Order"
                                    ofrm._TaskType = gloTaskMail.TaskType.PlaceLabOrder
                                    ofrm._UserName = gstrLoginName
                                    ofrm.UserID = gnLoginID
                                    ofrm._sNotesExt = strlabs

                                    If drTask(8) <> "" Then ''drTask(8) for sUserID
                                        Dim sUserID As String = drTask(8).ToString.Replace("|", ",")
                                        ofrm._taskuser_id = sUserID
                                        ofrm._SmartTask = True
                                    End If

                                    ofrm.ShowDialog(IIf(IsNothing(ofrm.Parent), Me, ofrm.Parent))
                                    ofrm.Dispose()
                                    ofrm = Nothing

                                    ''End

                                End If
                            Else '' If False Then Save the Task.

                                Dim strlabs As String = ""
                                Dim strlab As String = ""
                                strlab = ""
                                'Added by kanchan on 20100624 for Append selected Lab Test name in Notes
                                Dim sDescription As String = " For Lab Test:" & vbCrLf
                                Dim ncnt As Integer = 1

                                For olab As Integer = 0 To arrLabs.Count - 1
                                    strlab = CType(arrLabs.Item(olab), gloEmdeonCommon.myList).ID & "~" & CType(arrLabs.Item(olab), gloEmdeonCommon.myList).Value
                                    If olab = 0 Then
                                        strlabs = strlab
                                        'Added by kanchan on 20100624 for Append selected Lab Test name in Notes
                                        sDescription = sDescription & " " & ncnt & ". " & CType(arrLabs.Item(olab), gloEmdeonCommon.myList).Value & vbCrLf
                                        ncnt = ncnt + 1
                                    Else
                                        strlabs = strlabs & "|" & strlab
                                        'Added by kanchan on 20100624 for Append selected Lab Test name in Notes
                                        sDescription = sDescription & " " & ncnt & ". " & CType(arrLabs.Item(olab), gloEmdeonCommon.myList).Value & vbCrLf
                                        ncnt = ncnt + 1
                                    End If
                                Next

                                Dim _sUserID As String = ""
                                Dim _sTaskusers As String = ""
                                If drTask(8) <> "" And drTask(6) <> "" Then ''drTask(8) for sUserID & drTask(6) for sTaskusers.
                                    _sUserID = drTask(8)
                                    _sTaskusers = drTask(6)
                                End If

                                oclsSmartDiagnosis.AddTasks("Place Lab Order", sDescription, Now.ToString(), Now.ToString(), gloTaskMail.TaskType.PlaceLabOrder, strlabs, _sUserID, _sTaskusers, m_PatientID)
                            End If
                        End If
                    End If ''_dt.Rows(x)("sFieldName").ToString = "Labs"


                ElseIf drTask(1) = "Order Templates" Then ''drTask(1) for sFieldName.
                    'Dim frm As New frm_LM_Orders(m_VisitID, Now, m_PatientID)
                    Dim frm As frm_LM_Orders
                    frm = frm_LM_Orders.GetInstance(m_VisitID, Now, m_PatientID)
                    If IsNothing(frm) = True Then
                        Exit Sub
                    End If
                    If arrRadiology.Count > 0 Then
                        'Dim frm As New frm_LM_Orders
                        If drTask(5) = False Then ''drTask(5) for bSendTask
                            If drTask(2) = True Then ''drTask(2) for bFieldStatus
                                'Dim frm As New frm_LM_Orders
                                With frm
                                    ._ExamID = m_ExamID
                                    ._ArrRadi = arrRadiology
                                    ''._patientID = m_PatientID
                                    ''._VisitID = m_VisitID
                                    ''._VisitDate = Now
                                    .WindowState = FormWindowState.Maximized
                                    .BringToFront()
                                    .ShowDialog(IIf(IsNothing(frm.Parent), Me, frm.Parent))
                                    .Dispose()
                                End With
                                frm = Nothing
                            Else
                                With frm
                                    ._ExamID = m_ExamID
                                    ._ArrRadi = arrRadiology
                                    ''._patientID = m_PatientID
                                    ''._VisitID = m_VisitID
                                    ''._VisitDate = Now
                                    .WindowState = FormWindowState.Minimized
                                    '.BringToFront()
                                    .Opacity = 0
                                    .Show()
                                    .Hide()
                                    .SaveOrders()
                                    .Close()
                                End With
                            End If
                        Else
                            '' for assigning task ''
                            If drTask(7) = True Then '' If True Then show the Task form. drTask(7) for bAllowviewtsk
                                If Not IsNothing(arrRadiology) Then
                                    Dim dt As DataTable
                                    dt = Nothing
                                    Dim nOrderProviderID As Int64
                                    Dim sOrderProviderName As String
                                    Dim oPatientExam As New clsPatientExams
                                    Dim nProviderId As Long = oPatientExam.GetProviderIdforPatient(m_PatientID)
                                    oPatientExam.Dispose()
                                    oPatientExam = Nothing
                                    dt = GetLabTaskProvider(nProviderId)

                                    If Not IsNothing(dt) Then
                                        If dt.Rows.Count > 0 Then
                                            nOrderProviderID = dt.Rows(0)("nProviderID")
                                            sOrderProviderName = dt.Rows(0)("ProviderName")
                                        End If
                                        dt.Dispose()
                                        dt = Nothing
                                    End If

                                    ''If arrRadiology.Count > 0 Then
                                    Dim strOrders As String = ""
                                    ''= SerializeArrayList(arrLabs)
                                    Dim strOrder As String = ""
                                    strOrder = ""
                                    'Added by kanchan on 20100624 for Append selected Order in Notes
                                    Dim sDescription As String = " For Order:" & vbCrLf
                                    Dim ncnt As Integer = 1

                                    For oOrder As Integer = 0 To arrRadiology.Count - 1
                                        strOrder = CType(arrRadiology.Item(oOrder), myList).Index & "~" & CType(arrRadiology.Item(oOrder), myList).Value
                                        If oOrder = 0 Then
                                            strOrders = strOrder
                                            'Added by kanchan on 20100624 for Append selected Order in Notes
                                            sDescription = sDescription & " " & ncnt & ". " & CType(arrRadiology.Item(oOrder), myList).Value & vbCrLf
                                            ncnt = ncnt + 1
                                        Else
                                            strOrders = strOrders & "|" & strOrder
                                            'Added by kanchan on 20100624 for Append selected Order in Notes
                                            sDescription = sDescription & " " & ncnt & ". " & CType(arrRadiology.Item(oOrder), myList).Value & vbCrLf
                                            ncnt = ncnt + 1
                                        End If

                                    Next

                                    'Added by kanchan on 20100618 for Smart Order
                                    ''Dim ofrm As New gloTaskMail.frmTask(GetConnectionString, 0, m_PatientID, nOrderProviderID, sDescription, "Order available", TaskType.OrderRadiology, gstrLoginName)
                                    ''Added Rahul on 20101025
                                    Dim ofrm As New gloTaskMail.frmTask
                                    ofrm.DataBaseConnectionString = GetConnectionString()
                                    ofrm.TaskID = 0
                                    ofrm.PatientID = m_PatientID
                                    ofrm.ProviderID = nOrderProviderID
                                    ofrm.rtxtDescription.Text = sDescription
                                    ofrm.txtSubject.Text = "Order available"
                                    ofrm._TaskType = gloTaskMail.TaskType.OrderRadiology
                                    ofrm._UserName = gstrLoginName
                                    ofrm.UserID = gnLoginID
                                    ofrm._sNotesExt = strOrders

                                    If drTask(8) <> "" Then ''drTask(8) for sUserID
                                        Dim sUserID As String = drTask(8).ToString.Replace("|", ",")
                                        ofrm._taskuser_id = sUserID
                                        ofrm._SmartTask = True
                                    End If

                                    ofrm.ShowDialog(IIf(IsNothing(ofrm.Parent), Me, ofrm.Parent))
                                    ofrm.Dispose()
                                    ofrm = Nothing

                                    ''End
                                    ' '' ''Dim _TaskId As Int64 = ofrm.TaskID
                                    ' '' ''Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString)
                                    ' '' ''Dim strQry As String = "UPDATE tm_taskmst SET sNoteEXT='" & strOrders & "' where ntaskid=" & _TaskId
                                    ' '' ''oDB.Connect(False)
                                    ' '' ''oDB.Execute_Query(strQry)
                                End If
                            Else '' If False Then Save the Task.
                                Dim strOrders As String = ""
                                Dim strOrder As String = ""
                                strOrder = ""
                                'Added by kanchan on 20100624 for Append selected Order in Notes
                                Dim sDescription As String = " For Order:" & vbCrLf
                                Dim ncnt As Integer = 1

                                For oOrder As Integer = 0 To arrRadiology.Count - 1
                                    strOrder = CType(arrRadiology.Item(oOrder), myList).Index & "~" & CType(arrRadiology.Item(oOrder), myList).Value
                                    If oOrder = 0 Then
                                        strOrders = strOrder
                                        'Added by kanchan on 20100624 for Append selected Order in Notes
                                        sDescription = sDescription & " " & ncnt & ". " & CType(arrRadiology.Item(oOrder), myList).Value & vbCrLf
                                        ncnt = ncnt + 1

                                    Else
                                        strOrders = strOrders & "|" & strOrder
                                        'Added by kanchan on 20100624 for Append selected Order in Notes
                                        sDescription = sDescription & " " & ncnt & ". " & CType(arrRadiology.Item(oOrder), myList).Value & vbCrLf
                                        ncnt = ncnt + 1

                                    End If

                                Next

                                Dim _sUserID As String = ""
                                Dim _sTaskusers As String = ""
                                If drTask(8) <> "" And drTask(6) <> "" Then ''drTask(8) for sUserID & drTask(6) for sTaskusers
                                    _sUserID = drTask(8)
                                    _sTaskusers = drTask(6)
                                End If

                                oclsSmartDiagnosis.AddTasks("Order available", sDescription, Now.ToString(), Now.ToString(), gloTaskMail.TaskType.OrderRadiology, strOrders, _sUserID, _sTaskusers, m_PatientID)
                            End If
                        End If
                    End If

                ElseIf drTask(1) = "Referral Letter" Then ''drTask(1) for sFieldName
                    If arrTemplates.Count > 0 Then
                        If Not mycaller Is Nothing Then
                            'mycaller.nRefTempID = Convert.ToInt64(arrTemplates.Item(0))
                            Dim _TemplateName As String = ""
                            frmPatientExam.nRefTempID = Convert.ToInt64(Convert.ToInt64(CType(arrTemplates.Item(0), myList).ID))
                            _TemplateName = CType(arrTemplates.Item(0), myList).Value
                            '''''''' Convert.ToInt64(CType(arrTemplates.Item(0), myList).ID)

                            ' swaraj 03-04-2010 -- To Load Referral Templates Data'
                            Dim dtVisitRef As New DataTable
                            Dim dtPatRef As New DataTable

                            ''check if Referrals exists against given m_VisitId
                            If Not objReferralsDBLayer.CheckReferral(m_VisitID, m_ExamID, m_PatientID) Then
                                dtVisitRef = objReferralsDBLayer.FetchReferralsForUpdate(m_VisitID, m_PatientID, m_ExamID)
                                SaveReferrals(dtVisitRef, True, _TemplateName)
                            Else
                                'if Referral Details do not exist for that m_VisitId,
                                'Populate Referrals Details from Patient_Dtl Table
                                dtPatRef = objReferralsDBLayer.FillControls("R", m_PatientID)
                                SaveReferrals(dtPatRef, False, _TemplateName)
                            End If

                            'Dim clsExam As New clsPatientExams
                            'Dim chkPatRefPricnt As Integer = 0
                            'chkPatRefPricnt = clsExam.Chk_Reff_PriCarePhycnt(m_PatientID)
                            'If (chkPatRefPricnt <= 0) Then
                            '    If MessageBox.Show("Referral is  not Associated  for Patient. Do You Want to Associate It? ", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
                            '        Exit Sub
                            '    End If
                            'End If

                            frmSummaryofVisit.PatientTemplateID = Convert.ToInt64(Convert.ToInt64(CType(arrTemplates.Item(0), myList).ID))
                            '''''Convert.ToInt64(CType(arrTemplates.Item(0), myList).ID)

                            Dim frm As New frmSummaryofVisit(m_PatientID, m_VisitID, True, m_ExamID, m_ExamFilePath, ExamProviderId, blnExamFinished, True)
                            Try
                                ' Dim frm As New frmSummaryofVisit(PatientID, mgnVisitID, examid, sFileName, ExamProviderId, blnExamFinished, True, "", nRefTempID)
                                ''Dim frm As New frmSummaryofVisit(m_PatientID, m_VisitID, m_ExamID, m_ExamFilePath, ExamProviderId, blnExamFinished, True)
                                ' nRefTempID = 0
                                'frm.myCaller = 
                                If drTask(2) = True Then ''drTask(2) for bFieldStatus
                                    frm.dtDos = m_ExamDate
                                    frmExamChild = frm
                                    frm.Text = "Patient Referrals"
                                    frm.ShowDialog(IIf(IsNothing(frm.Parent), Me, frm.Parent))
                                    If Not IsNothing(frm) Then   'Obj Disposed by Mitesh
                                        frm.Close()
                                    End If
                                    If Not IsNothing(frm) Then
                                        frm.Dispose()
                                        frm = Nothing
                                    End If
                                Else
                                    frm.dtDos = m_ExamDate
                                    frmExamChild = frm
                                    frm.Text = "Patient Referrals"

                                    frm.Opacity = 0
                                    frm.Show()
                                    frm.Hide()
                                    frm.SaveReferrals()
                                    'frm.Close()
                                End If
                            Catch ex As Exception
                                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Referrals, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                                MessageBox.Show(ex.ToString, "Patient Exam", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Finally
                                If Not IsNothing(frm) Then  'Obj disposed by Mitesh 
                                    frm.Close()
                                End If

                                If Not IsNothing(frm) Then
                                    frm.Dispose()
                                    frm = Nothing
                                End If
                            End Try
                            'end swaraj'

                        Else
                            frmPatientExam.nRefTempID = 0
                        End If
                    End If


                ElseIf drTask(1) = "Drugs" Then ''drTask(1) for sFieldName
                    If arrDrugs.Count > 0 Then
                        'if Condition Added by kanchan on 20100619 for Task generation for Flowsheet/Drug
                        If drTask(5) = False Then ''drTask(5) for bSendTask
                            If drTask(2) = True Then ''drTask(2) for bFieldStatus

                                Dim ofrmPrescription As frmPrescription
                                ofrmPrescription = frmPrescription.GetInstance(arrDrugs, m_ProviderID, m_VisitID, m_PatientID)
                                If IsNothing(ofrmPrescription) = True Then
                                    Exit Sub
                                End If
                                If frmPrescription.IsOpen = False Then
                                    'Incident #00013567 : Medication carry forward case
                                    'following changes done to resolve incident
                                    'If ofrmPrescription.LockForm(m_PatientID) = False Then
                                    '    ofrmPrescription.Dispose()
                                    '    ofrmPrescription = Nothing
                                    'Else                                    
                                    With ofrmPrescription
                                        .WindowState = FormWindowState.Maximized
                                        .BringToFront()
                                        '.ShowReconcileMessage()
                                        .ShowDialog(IIf(IsNothing(ofrmPrescription.Parent), Me, ofrmPrescription.Parent))
                                        ofrmPrescription.Dispose()
                                        ofrmPrescription = Nothing
                                    End With
                                    'End If
                                    ''Dim ofrmPrescription As New frmPrescription(arrDrugs, m_ProviderID, m_VisitID, m_PatientID)
                                Else
                                    MessageBox.Show("Rx/Meds screen cannot be opened as it is already open in the background.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                End If


                            Else
                                Dim ofrmPrescription As frmPrescription
                                ofrmPrescription = frmPrescription.GetInstance(arrDrugs, m_ProviderID, m_VisitID, m_PatientID, False)
                                If IsNothing(ofrmPrescription) = True Then
                                    Exit Sub
                                End If
                                ''Dim ofrmPrescription As New frmPrescription(arrDrugs, m_ProviderID, m_VisitID, m_PatientID)

                                With ofrmPrescription
                                    .WindowState = FormWindowState.Minimized
                                    .Opacity = 0
                                    '.ShowReconcileMessage()
                                    .Show()
                                    .Hide()
                                    .DeleteLockRecord()
                                    .Close()
                                End With
                            End If
                        Else
                            'Code Added by kanchan on 20100621 for Task generation for Drug
                            'Generate Task for Drug
                            If drTask(7) = True Then '' If True Then show the Task form. drTask(7) for bAllowviewtsk
                                If Not IsNothing(arrDrugs) Then
                                    Dim dt As DataTable
                                    dt = Nothing
                                    Dim nDrugProviderID As Int64
                                    Dim sDrugProviderName As String
                                    Dim oPatientExam As New clsPatientExams
                                    Dim nProviderId As Long = oPatientExam.GetProviderIdforPatient(m_PatientID)
                                    oPatientExam.Dispose()
                                    oPatientExam = Nothing
                                    dt = GetLabTaskProvider(nProviderId)

                                    'Code changes by kanchan on 20100618 for Smart Order
                                    If Not IsNothing(dt) Then
                                        If dt.Rows.Count > 0 Then
                                            nDrugProviderID = dt.Rows(0)("nProviderID")
                                            sDrugProviderName = dt.Rows(0)("ProviderName")
                                        End If
                                        dt.Dispose()
                                        dt = Nothing
                                    End If

                                    Dim strDrug As String = ""
                                    Dim strDrugs As String = ""
                                    strDrug = ""

                                    'Added by kanchan on 20100624 for Append selected Drug in Notes
                                    Dim sDescription As String = " For Drug :" & vbCrLf
                                    Dim ncnt As Integer = 1

                                    For oDrug As Integer = 0 To arrDrugs.Count - 1
                                        strDrug = DirectCast(DirectCast(arrDrugs.Item(oDrug), System.Object), gloEMRGeneralLibrary.gloEMRActors.Drug).NDCCode
                                        If oDrug = 0 Then
                                            strDrugs = strDrug
                                            'Added by kanchan on 20100624 for Append selected Drug in Notes
                                            sDescription = sDescription & " " & ncnt & ". " & DirectCast(DirectCast(arrDrugs.Item(oDrug), System.Object), gloEMRGeneralLibrary.gloEMRActors.Drug).DrugsName & vbCrLf
                                            ncnt = ncnt + 1

                                        Else
                                            strDrugs = strDrugs & "|" & strDrug
                                            'Added by kanchan on 20100624 for Append selected Drug in Notes
                                            sDescription = sDescription & " " & ncnt & ". " & DirectCast(DirectCast(arrDrugs.Item(oDrug), System.Object), gloEMRGeneralLibrary.gloEMRActors.Drug).DrugsName & vbCrLf
                                            ncnt = ncnt + 1

                                        End If
                                    Next
                                    ''Dim ofrm As New gloTaskMail.frmTask(GetConnectionString, 0, m_PatientID, nDrugProviderID, sDescription, "Drugs available", TaskType.Drug, gstrLoginName)
                                    ''Added Rahul on 20101025
                                    Dim ofrm As New gloTaskMail.frmTask
                                    ofrm.DataBaseConnectionString = GetConnectionString()
                                    ofrm.TaskID = 0
                                    ofrm.PatientID = m_PatientID
                                    ofrm.ProviderID = nDrugProviderID
                                    ofrm.rtxtDescription.Text = sDescription
                                    ofrm.txtSubject.Text = "Drugs available"
                                    ofrm._TaskType = gloTaskMail.TaskType.Drug
                                    ofrm._UserName = gstrLoginName
                                    ofrm.UserID = gnLoginID
                                    ofrm._sNotesExt = strDrugs

                                    If drTask(8) <> "" Then ''drTask(8) for sUserID
                                        Dim sUserID As String = drTask(8).ToString.Replace("|", ",")
                                        ofrm._taskuser_id = sUserID
                                        ofrm._SmartTask = True
                                    End If

                                    ofrm.ShowDialog(IIf(IsNothing(ofrm.Parent), Me, ofrm.Parent))
                                    ofrm.Dispose()
                                    ofrm = Nothing


                                    ''End
                                    ' '' ''Dim _TaskId As Int64 = ofrm.TaskID
                                    ' '' ''Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString)
                                    ' '' ''Dim strQry As String = "UPDATE tm_taskmst SET sNoteEXT='" & strDrugs & "' where ntaskid=" & _TaskId
                                    ' '' ''oDB.Connect(False)
                                    ' '' ''oDB.Execute_Query(strQry)
                                End If
                            Else
                                Dim strDrug As String
                                Dim strDrugs As String = ""
                                strDrug = ""
                                Dim sDescription As String = " For Drug :" & vbCrLf
                                Dim ncnt As Integer = 1

                                For oDrug As Integer = 0 To arrDrugs.Count - 1
                                    strDrug = DirectCast(DirectCast(arrDrugs.Item(oDrug), System.Object), gloEMRGeneralLibrary.gloEMRActors.Drug).NDCCode
                                    If oDrug = 0 Then
                                        strDrugs = strDrug
                                        'Added by kanchan on 20100624 for Append selected Drug in Notes
                                        sDescription = sDescription & " " & ncnt & ". " & DirectCast(DirectCast(arrDrugs.Item(oDrug), System.Object), gloEMRGeneralLibrary.gloEMRActors.Drug).DrugsName & vbCrLf
                                        ncnt = ncnt + 1

                                    Else
                                        strDrugs = strDrugs & "|" & strDrug
                                        'Added by kanchan on 20100624 for Append selected Drug in Notes
                                        sDescription = sDescription & " " & ncnt & ". " & DirectCast(DirectCast(arrDrugs.Item(oDrug), System.Object), gloEMRGeneralLibrary.gloEMRActors.Drug).DrugsName & vbCrLf
                                        ncnt = ncnt + 1

                                    End If
                                Next

                                Dim _sUserID As String = ""
                                Dim _sTaskusers As String = ""
                                If drTask(8) <> "" And drTask(6) <> "" Then
                                    _sUserID = drTask(8)
                                    _sTaskusers = drTask(6)
                                End If
                                oclsSmartDiagnosis.AddTasks("Drugs available", sDescription, Now.ToString(), Now.ToString(), gloTaskMail.TaskType.Drug, strDrugs, _sUserID, _sTaskusers, m_PatientID)

                            End If
                        End If
                    End If
                ElseIf drTask(1) = "Flowsheet" Then
                    If arrFlow.Count > 0 Then
                        'if Condition Added by kanchan on 20100619 for Task generation for Flowsheet/Drug
                        If drTask(5) = False Then
                            If drTask(2) = True Then
                                Dim objfrmpatientflowsheet As New frmPatientFlowSheet(m_PatientID)
                                ''Bug : 00000828: Record locking
                                If objfrmpatientflowsheet.FormLevelLock() Then
                                    objfrmpatientflowsheet.WindowState = FormWindowState.Maximized
                                    frmPatientFlowSheet.Array_Flow_List = arrFlow

                                    objfrmpatientflowsheet.ShowDialog(IIf(IsNothing(objfrmpatientflowsheet.Parent), Me, objfrmpatientflowsheet.Parent))
                                End If
                                objfrmpatientflowsheet.Dispose()
                                objfrmpatientflowsheet = Nothing

                            Else
                                Dim objfrmpatientflowsheet As New frmPatientFlowSheet(m_PatientID)
                                ''Bug : 00000828: Record locking
                                If objfrmpatientflowsheet.FormLevelLock() Then
                                    objfrmpatientflowsheet.WindowState = FormWindowState.Maximized
                                    frmPatientFlowSheet.Array_Flow_List = arrFlow

                                    objfrmpatientflowsheet.Opacity = 0
                                    objfrmpatientflowsheet.Show()
                                    objfrmpatientflowsheet.Hide()
                                    objfrmpatientflowsheet.SavePatientFlowSheet()
                                    objfrmpatientflowsheet.Close()
                                Else
                                    objfrmpatientflowsheet.Dispose()
                                    objfrmpatientflowsheet = Nothing
                                End If

                            End If
                        Else
                            'Code Added by kanchan on 20100619 for Task generation for Flowsheet/Drug
                            'Generate Task for Flowsheet
                            '' for assigning task ''
                            If drTask(7) = True Then '' If True Then show the Task form.
                                If Not IsNothing(arrFlow) Then
                                    '        If arrLabs.Count > 0 Then

                                    Dim dt As DataTable
                                    dt = Nothing
                                    Dim nFlowsheetProviderID As Int64
                                    Dim sFlowsheetProviderName As String
                                    Dim oPatientExam As New clsPatientExams
                                    Dim nProviderId As Long = oPatientExam.GetProviderIdforPatient(m_PatientID)
                                    oPatientExam.Dispose()
                                    oPatientExam = Nothing

                                    dt = GetLabTaskProvider(nProviderId)
                                    'Code changes by kanchan on 20100618 for Smart Order

                                    If Not IsNothing(dt) Then
                                        If dt.Rows.Count > 0 Then
                                            nFlowsheetProviderID = dt.Rows(0)("nProviderID")
                                            sFlowsheetProviderName = dt.Rows(0)("ProviderName")
                                        End If
                                        dt.Dispose()
                                        dt = Nothing
                                    End If

                                    Dim strFlow As String = ""
                                    Dim strFlows As String = ""
                                    strFlow = ""
                                    'Added by kanchan on 20100624 for Append selected Flowsheet in Notes
                                    Dim sDescription As String = " For Flowsheet :" & vbCrLf
                                    Dim ncnt As Integer = 1

                                    For oFlowsheet As Integer = 0 To arrFlow.Count - 1
                                        strFlow = CType(arrFlow.Item(oFlowsheet), myList).ID & "~" & CType(arrFlow.Item(oFlowsheet), myList).Value
                                        If oFlowsheet = 0 Then
                                            strFlows = strFlow
                                            'Added by kanchan on 20100624 for Append selected Flowsheet in Notes
                                            sDescription = sDescription & " " & ncnt & ". " & CType(arrFlow.Item(oFlowsheet), myList).Value & vbCrLf
                                            ncnt = ncnt + 1

                                        Else
                                            strFlows = strFlows & "|" & strFlow
                                            'Added by kanchan on 20100624 for Append selected Flowsheet in Notes
                                            sDescription = sDescription & " " & ncnt & ". " & CType(arrFlow.Item(oFlowsheet), myList).Value & vbCrLf
                                            ncnt = ncnt + 1

                                        End If
                                    Next
                                    ''Dim ofrm As New gloTaskMail.frmTask(GetConnectionString, 0, m_PatientID, nFlowsheetProviderID, sDescription, "Flowsheet available", TaskType.Flowsheet, gstrLoginName)
                                    ''Added Rahul on 20101025
                                    Dim ofrm As New gloTaskMail.frmTask
                                    ofrm.DataBaseConnectionString = GetConnectionString()
                                    ofrm.TaskID = 0
                                    ofrm.PatientID = m_PatientID
                                    ofrm.ProviderID = nFlowsheetProviderID
                                    ofrm.rtxtDescription.Text = sDescription
                                    ofrm.txtSubject.Text = "Flowsheet available"
                                    ofrm._TaskType = gloTaskMail.TaskType.Flowsheet
                                    ofrm._UserName = gstrLoginName
                                    ofrm.UserID = gnLoginID
                                    ofrm._sNotesExt = strFlows

                                    If drTask(8) <> "" Then
                                        Dim sUserID As String = drTask(8).ToString.Replace("|", ",")
                                        ofrm._taskuser_id = sUserID
                                        ofrm._SmartTask = True
                                    End If

                                    ofrm.ShowDialog(IIf(IsNothing(ofrm.Parent), Me, ofrm.Parent))
                                    ofrm.Dispose()
                                    ofrm = Nothing
                                    ''End
                                    ' '' ''Dim _TaskId As Int64 = ofrm.TaskID
                                    ' '' ''Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString)
                                    ' '' ''Dim strQry As String = "UPDATE tm_taskmst SET sNoteEXT='" & strFlows & "' where ntaskid=" & _TaskId
                                    ' '' ''oDB.Connect(False)
                                    ' '' ''oDB.Execute_Query(strQry)
                                End If
                            Else '' If False Then Save the Task.
                                Dim strFlow As String = ""
                                Dim strFlows As String = ""
                                strFlow = ""
                                'Added by kanchan on 20100624 for Append selected Flowsheet in Notes
                                Dim sDescription As String = " For Flowsheet :" & vbCrLf
                                Dim ncnt As Integer = 1

                                For oFlowsheet As Integer = 0 To arrFlow.Count - 1
                                    strFlow = CType(arrFlow.Item(oFlowsheet), myList).ID & "~" & CType(arrFlow.Item(oFlowsheet), myList).Value
                                    If oFlowsheet = 0 Then
                                        strFlows = strFlow
                                        'Added by kanchan on 20100624 for Append selected Flowsheet in Notes
                                        sDescription = sDescription & " " & ncnt & ". " & CType(arrFlow.Item(oFlowsheet), myList).Value & vbCrLf
                                        ncnt = ncnt + 1

                                    Else
                                        strFlows = strFlows & "|" & strFlow
                                        'Added by kanchan on 20100624 for Append selected Flowsheet in Notes
                                        sDescription = sDescription & " " & ncnt & ". " & CType(arrFlow.Item(oFlowsheet), myList).Value & vbCrLf
                                        ncnt = ncnt + 1

                                    End If
                                Next

                                Dim _sUserID As String = ""
                                Dim _sTaskusers As String = ""
                                If drTask(8) <> "" And drTask(6) <> "" Then
                                    _sUserID = drTask(8)
                                    _sTaskusers = drTask(6)
                                End If

                                oclsSmartDiagnosis.AddTasks("Flowsheet available", sDescription, Now.ToString(), Now.ToString(), gloTaskMail.TaskType.Flowsheet, strFlows, _sUserID, _sTaskusers, m_PatientID)
                            End If
                        End If
                    End If
                End If

            Next
        End If
        ''End

        frmPatientExam.Arrlist = arrexam
        frmPatientExam.blnChangesMade = True
        frmPatientExam.nRefTempID = 0

        ' Me.Close()
        gloWord.WordDialogBoxBackgroundCloser.Close(Me)
        '''''''''''''''''''''''''''
    End Sub

    Private Sub SaveReferrals(ByVal objTable As DataTable, ByVal blnRef As Boolean, ByVal TemplateName As String)
        ''''''''''''''' Added by Ujwala - Smart Order Changes Integration - as on 20101016
        If Not objTable Is Nothing Then
            If objTable.Rows.Count > 0 Then
                Dim Arrlist As New ArrayList
                For j As Int32 = 0 To objTable.Rows.Count - 1
                    Dim strRefName As String = ""
                    Dim strFirstName As String = ""
                    Dim strMiddleName As String = ""
                    Dim strLastName As String = ""
                    Dim m_ContactId As Int64
                    Dim bIsPCP As Boolean
                    Dim ContactFlag As Int16
                    If blnRef Then
                        strRefName = objTable.Rows(j)(2).ToString

                        If Not IsDBNull(objTable.Rows(j)(2)) Then
                            m_ContactId = CType(objTable.Rows(j)(2), Int64)
                        End If
                        bIsPCP = CType(objTable.Rows(j)("bIsPCP"), Boolean)
                        ContactFlag = CType(objTable.Rows(j)("nContactFlag"), Int16)
                    Else
                        strRefName = objTable.Rows(j)(1).ToString
                        strFirstName = objTable.Rows(j)("sFirstName").ToString()
                        strMiddleName = objTable.Rows(j)("sMiddleName").ToString()
                        strLastName = objTable.Rows(j)("sLastName").ToString()
                        If Not IsDBNull(objTable.Rows(j)(0)) Then
                            m_ContactId = CType(objTable.Rows(j)(0), Int64)
                        End If
                        If CType(objTable.Rows(j)(2), String) = "P" Then
                            bIsPCP = True
                        Else
                            bIsPCP = False
                        End If
                        ContactFlag = CType(objTable.Rows(j)("nContactFlag"), Int16)
                    End If
                    Dim lst As New myList
                    'need to save templateid stored against every referral entry
                    lst.ID = frmPatientExam.nRefTempID 'TemplateID
                    lst.Index = m_ContactId 'ReferralID
                    lst.Description = strRefName '' ReferralName
                    lst.Type = bIsPCP
                    lst.ContactFlag = ContactFlag
                    lst.ContactTemplateName = TemplateName
                    lst.ContactFirstName = strFirstName
                    lst.ContactMiddleName = strMiddleName
                    lst.ContactLastName = strLastName
                    lst.TemplateResult = Nothing '' Template(Object)
                    Arrlist.Add(lst)
                Next
                If blnRef Then
                    objReferralsDBLayer.AddData(Arrlist, m_VisitID, DateTime.Now, m_PatientID, 2)
                Else
                    objReferralsDBLayer.AddData(Arrlist, m_VisitID, DateTime.Now, m_PatientID, m_ExamID)

                End If

            End If
        End If
        ''''''''''''''' Added by Ujwala - Smart Order Changes Integration - as on 20101016
    End Sub


#Region "Original Save Smart Diagnosis Procedure"
    '' By Mahesh on 20060316
    '''' WHY? : Overwrites Diagnosis & other data
    '' Commented On 20070630
    'Private Sub SaveAssociation_Original()

    '    'Get node count of child nodes in trICD9Associates
    '    'If trICD9Association.Nodes.Item(0).GetNodeCount(False) > 0 Then
    '    Dim arrDruglist As New ArrayList
    '    Dim i As Integer
    '    For i = 0 To trICD9Association.Nodes.Item(0).GetNodeCount(False) - 1
    '        Dim ICD9Node As myTreeNode
    '        'get the ICD9Node associated sequentially
    '        ICD9Node = trICD9Association.Nodes.Item(0).Nodes.Item(i)
    '        If ICD9Node.GetNodeCount(True) > 0 Then
    '            Dim k As Integer
    '            Dim arrlist As New ArrayList
    '            For k = 0 To 3
    '                Dim AssociateNode As myTreeNode
    '                AssociateNode = ICD9Node.Nodes.Item(k)
    '                Dim j As Integer
    '                For j = 0 To AssociateNode.GetNodeCount(False) - 1
    '                    If AssociateNode.Nodes.Item(j).Checked = True Then
    '                        If AssociateNode.Text = "CPT" Then
    '                            arrlist.Add(New myList(CType(AssociateNode.Nodes.Item(j), myTreeNode).Key, CType(AssociateNode.Nodes.Item(j), myTreeNode).Name, 1)) '' For CPT

    '                        ElseIf AssociateNode.Text = "Drugs" Then
    '                            Dim DrudID As Long = CType(AssociateNode.Nodes.Item(j), myTreeNode).Key
    '                            arrlist.Add(New myList(CType(AssociateNode.Nodes.Item(j), myTreeNode).Key, CType(AssociateNode.Nodes.Item(j), myTreeNode).Name, 2))  '' For Drugs
    '                            arrDruglist.Add(DrudID)
    '                            'arrDruglist.Add(New myList(CType(AssociateNode.Nodes.Item(j), myTreeNode).Key, CType(AssociateNode.Nodes.Item(j), myTreeNode).Name, 2))

    '                        ElseIf AssociateNode.Text = "Patient Education" Then
    '                            arrlist.Add(New myList(CType(AssociateNode.Nodes.Item(j), myTreeNode).Key, CType(AssociateNode.Nodes.Item(j), myTreeNode).Name, 3))  '' Patient Education

    '                        ElseIf AssociateNode.Text = "Tags" Then
    '                            arrlist.Add(New myList(CType(AssociateNode.Nodes.Item(j), myTreeNode).Key, CType(AssociateNode.Nodes.Item(j), myTreeNode).Name, 4))  '' For Tags
    '                            frmPatientExam.arrTagID.Add(CType(AssociateNode.Nodes.Item(j), myTreeNode).Key)
    '                        End If
    '                    End If
    '                Next

    '            Next

    '            objclsSmartDiagnosis.AddData(m_ExamID, m_VisitID, arrlist, ICD9Node.NodeName, arrDruglist)
    '        Else
    '            '''''if  for any ICD9 There is no Items Associated with it Then only That ICD9 is Saved as diagnosis
    '            ' objclsSmartDiagnosis.AddDiagnosis(m_ExamID, m_VisitID, ICD9Node.NodeName)
    '        End If
    '    Next
    '    RefreshICD9()
    '    frmPatientExam.blnChangesMade = True
    '    'End If
    'End Sub
#End Region

    'Private Sub trOrderAssociation_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles trOrderAssociation.MouseDown
    '    Try
    '        If e.Button = MouseButtons.Right Then

    '            Dim trvNode As TreeNode
    '            trvNode = trOrderAssociation.GetNodeAt(e.X, e.Y)
    '            If IsNothing(trvNode) = False Then
    '                trOrderAssociation.SelectedNode = trvNode
    '            End If

    '            If IsNothing(trOrderAssociation.SelectedNode.Parent) = True Then
    '                trOrderAssociation.ContextMenu = Nothing
    '                Exit Sub
    '            End If

    '            If IsNothing(trOrderAssociation.SelectedNode) = False Then

    '                'If trvNode.Text = "Order Templates" Then
    '                '    trOrder.ContextMenu = Nothing
    '                '    Exit Sub
    '                'End If
    '                If IsNothing(trvNode) = False Then
    '                    trOrder.SelectedNode = trvNode
    '                    trOrder.ContextMenu = cntOrderAssociation
    '                    OrderNode = trvNode
    '                End If
    '                ''If trICD9Association.Nodes.Item(0).Text = trICD9Association.SelectedNode.Text Or trICD9Association.SelectedNode.Parent Is trICD9Association.Nodes.Item(0) Or (CType(trICD9Association.SelectedNode, myTreeNode).Key = -1) Then
    '                If trOrderAssociation.Nodes(0).Text = trOrderAssociation.SelectedNode.Parent.Text Then
    '                    trOrderAssociation.ContextMenu = cntOrderAssociation
    '                Else
    '                    trOrderAssociation.ContextMenu = Nothing
    '                    'treeindex = trPrescriptionDetails.SelectedNode.Index
    '                End If
    '            End If

    '        End If
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message, "Smart Orders", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Sub

    Private Sub mnuDeleteICD9Item_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuDeleteICD9Item.Click
        Try
            If IsNothing(trOrderAssociation.SelectedNode.Parent) = False Then
                '   Dim i As Integer
                '  Dim j As Integer

                'If trICD9Association.SelectedNode Is trICD9Association.Nodes.Item(0) Then
                '    '''' ICD9Node 
                '    For i = trICD9Association.SelectedNode.GetNodeCount(True) - 1 To 0 Step -1
                '        trICD9Association.SelectedNode.Nodes(i).Remove()
                '    Next
                '    Exit Sub
                'End If

                'If CType(trICD9Association.SelectedNode, myTreeNode).Key = -1 Then
                '    '''' "CPT","Drugs","PE","Tags"
                '    For i = trICD9Association.SelectedNode.GetNodeCount(True) - 1 To 0 Step -1
                '        trICD9Association.SelectedNode.Nodes(i).Remove()
                '    Next
                '    Exit Sub
                'End If

                '''''''''<<<<<<<>>>>>>>>
                'Dim strICD9 As String()
                'strICD9 = Split(trICD9Association.SelectedNode.Text, "-", 2)

                'Dim RowIndex As Integer = -1
                'RowIndex = objclsSmartDiagnosis.Check_Existence(objclsSmartDiagnosis.dtDiagnosis, strICD9(0), strICD9(1))
                'If RowIndex <> -1 Then
                '    objclsSmartDiagnosis.dtDiagnosis.Rows.RemoveAt(RowIndex)
                'End If

                'For i = 0 To trICD9Association.SelectedNode.GetNodeCount(False) - 1
                '    Dim myNode As New myTreeNode
                '    If trICD9Association.SelectedNode.Nodes(i).Text = "CPT" Then
                '        For j = 0 To trICD9Association.SelectedNode.Nodes(i).GetNodeCount(False) - 1
                '            myNode = CType(trICD9Association.SelectedNode.Nodes(i).Nodes(j), myTreeNode)
                '            If myNode.Checked = True Then
                '                Dim strCPT As String()
                '                strCPT = Split(myNode.Text, "-", 2)
                '                RowIndex = -1
                '                RowIndex = objclsSmartDiagnosis.Check_Existence(objclsSmartDiagnosis.dtTreatment, strCPT(0), strCPT(1))
                '                If RowIndex <> -1 Then
                '                    objclsSmartDiagnosis.dtTreatment.Rows.RemoveAt(RowIndex)
                '                End If
                '            End If
                '        Next

                '    ElseIf trICD9Association.SelectedNode.Nodes(i).Text = "Drugs" Then

                '    ElseIf trICD9Association.SelectedNode.Nodes(i).Text = "Patient Education" Then
                '        For j = 0 To trICD9Association.SelectedNode.Nodes(i).GetNodeCount(False) - 1
                '            myNode = CType(trICD9Association.SelectedNode.Nodes(i).Nodes(j), myTreeNode)
                '            If myNode.Checked = True Then
                '                Dim l As Integer
                '                For l = arrPE.Count - 1 To 0 Step -1
                '                    If myNode.Name = CType(arrPE(l), myList).Description Then
                '                        arrPE.RemoveAt(l)
                '                    End If
                '                Next
                '            End If
                '        Next
                '    ElseIf trICD9Association.SelectedNode.Nodes(i).Text = "Tags" Then

                '    End If
                'Next
                '''''''''''<<<<<<<>>>>>>>>

                Dim mychildnode As myTreeNode
                ' Dim key As Long
                mychildnode = CType(trOrderAssociation.SelectedNode, myTreeNode)
                mychildnode.Remove() 'delete from treeview

            End If
        Catch ex As SqlClient.SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartOrders, gloAuditTrail.ActivityType.Delete, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, "Smart Order", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartOrders, gloAuditTrail.ActivityType.Delete, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, "Smart Order", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub trOrder_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles trOrder.DoubleClick
        Try
            If IsNothing(trOrder.SelectedNode) = False Then


                Dim mynode As myTreeNode
                mynode = CType(trOrder.SelectedNode, myTreeNode)

                If Not IsNothing(mynode) Then
                    'if condition added by dipak 20091110 to fix bug 5088 :Exam ->Smart order-Parent Node get added on doubleclick
                    If (mynode.Level <> 0) Then
                        AddNode(mynode)
                    End If
                    'end code added by dipak 20091110
                End If
            End If
            'selectedTreeview.ExpandAll()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartOrders, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, "Smart Orders", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub AddNode(ByVal mynode As myTreeNode)

        If Not mynode Is Nothing Then
            Dim str As String
            str = mynode.Text
            Dim mytragetnode As myTreeNode
            For Each mytragetnode In trOrderAssociation.Nodes.Item(0).Nodes
                If mytragetnode.Text = str Then
                    Exit Sub
                End If
            Next
            ' arrSelectedNode.Add(mynode)
            ''''Add CPT/Drugs/PE to icd9 node

            'trICD9.SelectedNode.Remove()
            Dim associatenode As myTreeNode

            associatenode = mynode.Clone
            associatenode.Key = mynode.Key
            associatenode.Text = mynode.Text
            associatenode.NodeName = mynode.Text

            associatenode.ImageIndex = 5
            associatenode.SelectedImageIndex = 5

            ' associatenode.Checked = True

            trOrderAssociation.Nodes.Item(0).Nodes.Add(associatenode)

            Dim mychild As myTreeNode

            ''''''''''''''' Added by Ujwala - Smart Order Changes Integration - as on 20101016
            For x As Integer = 0 To _dt.Rows.Count - 1
                mychild = New myTreeNode
                'mychild = New myTreeNode(_dt.Rows(x)("sFieldName").ToString(), -1)
                If _dt.Rows(x)("sFieldName").ToString = "Orders and Results" Then
                    mychild.Text = "Orders and Results"
                    mychild.ImageIndex = 0
                    mychild.SelectedImageIndex = 0
                ElseIf _dt.Rows(x)("sFieldName").ToString = "Order Templates" Then
                    mychild.Text = "Order Templates"
                    mychild.ImageIndex = 1
                    mychild.SelectedImageIndex = 1
                ElseIf _dt.Rows(x)("sFieldName").ToString = "Referral Letter" Then
                    mychild.Text = "Referral Letter"
                    mychild.ImageIndex = 2
                    mychild.SelectedImageIndex = 2
                ElseIf _dt.Rows(x)("sFieldName").ToString = "Drugs" Then
                    mychild.Text = "Drugs"
                    mychild.ImageIndex = 3
                    mychild.SelectedImageIndex = 3
                ElseIf _dt.Rows(x)("sFieldName").ToString = "Flowsheet" Then
                    mychild.Text = "Flowsheet"
                    mychild.ImageIndex = 8
                    mychild.SelectedImageIndex = 8
                End If
                'mychild.ImageIndex = x
                'mychild.SelectedImageIndex = x
                associatenode.Nodes.Add(mychild)
            Next
            ''''''''''''''' Added by Ujwala - Smart Order Changes Integration - as on 20101016

            ' '' ''mychild = New myTreeNode("Labs", -1)
            ' '' ''mychild.ImageIndex = 0
            ' '' ''mychild.SelectedImageIndex = 0
            ' '' ''associatenode.Nodes.Add(mychild)

            ' '' ''mychild = New myTreeNode("Order Templates", -1)
            ' '' ''mychild.ImageIndex = 1
            ' '' ''mychild.SelectedImageIndex = 1
            ' '' ''associatenode.Nodes.Add(mychild)

            ' '' ''mychild = New myTreeNode("Referral Letter", -1)
            ' '' ''mychild.ImageIndex = 2
            ' '' ''mychild.SelectedImageIndex = 2
            ' '' ''associatenode.Nodes.Add(mychild)


            ' '' ''mychild = New myTreeNode("Drugs", -1)
            ' '' ''mychild.ImageIndex = 3
            ' '' ''mychild.SelectedImageIndex = 3
            ' '' ''associatenode.Nodes.Add(mychild)



            'associatenode.Nodes.Add(New myTreeNode("CPT", -1))
            'associatenode.Nodes.Add(New myTreeNode("Drugs", -1))
            'associatenode.Nodes.Add(New myTreeNode("Patient Education", -1))
            'associatenode.Nodes.Add(New myTreeNode("Tags", -1))

            Dim dt As DataTable
            dt = objclsSmartOrder.FetchOrderforUpdate(associatenode.Key)
            Dim i As Integer
            Dim fl As Boolean = False
            If IsNothing(dt) = False Then
                For i = 0 To dt.Rows.Count - 1

                    If dt.Rows(i).Item("Status") = "True" Then
                        fl = True
                    Else
                        fl = False
                    End If

                    'add cpt items to cpt node in icd9
                    If dt.Rows(i).Item("AssociateType") = "L" Then
                        Dim strLabName As String
                        'strLabName = objclsSmartOrder.FetchLabName(dt.Rows(i).Item(1))
                        ''Sandip Darade 20090401 
                        ''Instead of using ID use name of the LAB 
                        strLabName = Convert.ToString(dt.Rows(i)("sAssociateName"))
                        Dim mytreenode As myTreeNode
                        mytreenode = New myTreeNode(strLabName, dt.Rows(i).Item("nAssociateID"))
                        mytreenode.ImageIndex = 4
                        mytreenode.SelectedImageIndex = 4
                        mytreenode.Tag = dt.Rows(i).Item("nAssociateID")

                        mytreenode.Checked = fl

                        For Each oNode As myTreeNode In associatenode.Nodes
                            If oNode.Text = "Orders and Results" Then
                                oNode.Nodes.Add(mytreenode)
                                Exit For
                            End If
                        Next

                        ''associatenode.Nodes.Item(0).Nodes.Add(mytreenode)

                        'associatenode.Nodes(0).Nodes.Add(New myTreeNode(dt.Rows(i).Item(1), dt.Rows(i).Item(0)))
                        'Load_Treatment(associatenode)

                        'add drug items to drug node in icd9
                    ElseIf dt.Rows(i).Item("AssociateType") = "R" Then

                        Dim strRadiology As String
                        'strRadiology = objclsSmartOrder.FetchRadiologyName(dt.Rows(i).Item(1))
                        ''Sandip Darade 20090401 
                        ''Instead of using ID use name of the Radiology 
                        strRadiology = Convert.ToString(dt.Rows(i)("sAssociateName"))
                        Dim mytreenode As myTreeNode
                        mytreenode = New myTreeNode(strRadiology, dt.Rows(i).Item("nAssociateID"))
                        mytreenode.Tag = dt.Rows(i).Item("nAssociateID")
                        mytreenode.ImageIndex = 4
                        mytreenode.SelectedImageIndex = 4

                        mytreenode.Checked = fl

                        For Each oNode As myTreeNode In associatenode.Nodes
                            If oNode.Text = "Order Templates" Then
                                oNode.Nodes.Add(mytreenode)
                                Exit For
                            End If
                        Next

                        ''associatenode.Nodes.Item(1).Nodes.Add(mytreenode)

                        'associatenode.Nodes.Item(1).Nodes.Add(New myTreeNode(dt.Rows(i).Item(1), dt.Rows(i).Item(0)))


                        'add PE items to PE node in icd9
                    ElseIf dt.Rows(i).Item("AssociateType") = "T" Then
                        Dim strTemplate As String
                        ' strTemplate = objclsSmartOrder.FetchTemplateName(dt.Rows(i).Item(1))
                        ''Sandip Darade 20090401 
                        ''Instead of using ID use name of the Template 
                        strTemplate = Convert.ToString(dt.Rows(i)("sAssociateName"))
                        Dim mytreenode As myTreeNode
                        mytreenode = New myTreeNode(strTemplate, dt.Rows(i).Item("nAssociateID"))
                        mytreenode.Tag = dt.Rows(i).Item("nAssociateID")
                        mytreenode.ImageIndex = 4
                        mytreenode.SelectedImageIndex = 4

                        mytreenode.Checked = fl

                        For Each oNode As myTreeNode In associatenode.Nodes
                            If oNode.Text = "Referral Letter" Then
                                oNode.Nodes.Add(mytreenode)
                                Exit For
                            End If
                        Next

                        ''associatenode.Nodes.Item(2).Nodes.Add(mytreenode)

                        'associatenode.Nodes.Item(2).Nodes.Add(New myTreeNode(dt.Rows(i).Item(1), dt.Rows(i).Item(0)))
                        'Call Load_PatientEducation(associatenode)


                    ElseIf dt.Rows(i).Item("AssociateType") = "D" Then
                        'Dim strDrugName As String
                        ''strDrugName = objclsSmartOrder.FetchDrugName(dt.Rows(i).Item(1))
                        ' ''Sandip Darade 20090401 
                        ' ''Instead of using ID use name of the Drug 
                        'strDrugName = Convert.ToString(dt.Rows(i)("sAssociateName"))
                        'Dim mytreenode As New myTreeNode
                        'mytreenode = New myTreeNode(strDrugName, dt.Rows(i).Item("nAssociateID"))
                        Dim oNode As New myTreeNode
                        oNode.Text = dt.Rows(i)("sAssociateName")
                       
                        oNode.DrugName = dt.Rows(i)("sAssociateName")
                        oNode.Key = Convert.ToInt64(dt.Rows(i)("nAssociateID"))
                        oNode.Dosage = dt.Rows(i)("sDosage")
                        oNode.DrugForm = dt.Rows(i)("sDrugForm")
                        oNode.Route = dt.Rows(i)("sRoute")
                        oNode.Frequency = dt.Rows(i)("sFrequency")
                        oNode.NDCCode = dt.Rows(i)("sNDCCode")
                        oNode.IsNarcotics = Convert.ToInt16(dt.Rows(i)("nIsNarcotics"))
                        oNode.Duration = dt.Rows(i)("sDuration")
                        oNode.mpid = Convert.ToInt32(dt.Rows(i)("mpid"))
                        oNode.DrugQtyQualifier = dt.Rows(i)("sDrugQtyQualifier")
                        oNode.Tag = Convert.ToInt64(dt.Rows(i)("nAssociateID"))
                        oNode.ImageIndex = 4
                        oNode.SelectedImageIndex = 4

                        oNode.Checked = fl

                        For Each _oNode As myTreeNode In associatenode.Nodes
                            If _oNode.Text = "Drugs" Then
                                _oNode.Nodes.Add(oNode)
                                Exit For
                            End If
                        Next

                        ''associatenode.Nodes.Item(3).Nodes.Add(oNode)

                        'associatenode.Nodes.Item(2).Nodes.Add(New myTreeNode(dt.Rows(i).Item(1), dt.Rows(i).Item(0)))
                        'Call Load_PatientEducation(associatenode)
                    ElseIf dt.Rows(i).Item("AssociateType") = "F" Then 'For Flowsheet

                        Dim strFlowsh As String

                        strFlowsh = Convert.ToString(dt.Rows(i)("sAssociateName"))
                        Dim mytreenode As myTreeNode
                        mytreenode = New myTreeNode(strFlowsh, dt.Rows(i).Item("nAssociateID"))
                        mytreenode.Tag = dt.Rows(i).Item("nAssociateID")
                        mytreenode.ImageIndex = 4
                        mytreenode.SelectedImageIndex = 4

                        mytreenode.Checked = fl


                        For Each oNode As myTreeNode In associatenode.Nodes
                            If oNode.Text = "Flowsheet" Then
                                oNode.Nodes.Add(mytreenode)
                                Exit For
                            End If
                        Next


                        'associatenode.Nodes.Item(4).Nodes.Add(mytreenode)



                    End If

                Next
            End If
            trOrderAssociation.ExpandAll()
            trOrderAssociation.Select()

            'treeindex = -1
            'End If

            'Ensure the newly created node is visible to the user and select it
            associatenode.EnsureVisible()
            trOrderAssociation.SelectedNode = associatenode

            '' To refresh the txtDrugs 
            Call RefreshSearch()
            '''''''''''''''''''
            CheckAllParentNodes()
            '''''''''''''''''''
            'treeindex = mynode.Index
        End If
    End Sub

    Private Sub txtsearchOrders_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtsearchOrders.KeyPress
        If (e.KeyChar = ChrW(13)) Then
            trOrder.Select()
        Else
            trOrder.SelectedNode = trOrder.Nodes.Item(0)
        End If
        ''--Added by Anil on 20071213
        mdlGeneral.ValidateText(txtsearchOrders.Text, e)
        ''----
    End Sub

    Private Sub txtsearchOrders_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtsearchOrders.Validating
        'Try
        '    Dim mychildnode As myTreeNode
        '    'child node collection
        '    For Each mychildnode In trICD9.Nodes.Item(0).Nodes
        '        'compare selected node text and entered text
        '        Dim str As String
        '        str = Mid(UCase(Trim(mychildnode.Tag)), 1, Len(UCase(Trim(txtsearchDrugs.Text))))
        '        If str = UCase(Trim(txtsearchDrugs.Text)) Then
        '            trICD9.SelectedNode = mychildnode
        '            Exit Sub
        '        End If
        '    Next
        'Catch ex As Exception
        '    MessageBox.Show(ex.Message, "Smart Diagnosis", MessageBoxButtons.OK, MessageBoxIcon.Error)
        'End Try
    End Sub

    Private Sub trOrder_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles trOrder.KeyPress
        If (e.KeyChar = ChrW(13)) Then
            Try
                Dim mynode As myTreeNode
                mynode = CType(trOrder.SelectedNode, myTreeNode)
                If Not IsNothing(mynode) Then
                    AddNode(mynode)
                End If
                'selectedTreeview.ExpandAll()
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartOrders, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                MessageBox.Show(ex.Message, "Smart Orders", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    Private Sub RefreshSearch()
        txtsearchOrders.Text = ""
        txtsearchOrders.Focus()
    End Sub

    'Private Sub trOrder_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles trOrder.MouseDown
    '    Try
    '        If e.Button = MouseButtons.Right Then

    '            Dim trvNode As TreeNode
    '            trvNode = trOrder.GetNodeAt(e.X, e.Y)
    '            Dim lst As New myList
    '            lst.Index = trvNode.Tag
    '            lst.Value = trvNode.Text

    '            arrSelectedNode.Add(lst)
    '        End If
    '    Catch ex As Exception

    '    End Try
    'End Sub

    Private Sub frmSmartOrder_Closed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Closed
        Try
            ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartOrders, gloAuditTrail.ActivityType.Close, "Smart Orders Closed", gloAuditTrail.ActivityOutCome.Success)
            ''Added Rahul P on 20101011
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartOrders, gloAuditTrail.ActivityType.Close, "Smart Orders Closed", m_PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            ''
            'gloAuditTrail.gloAuditTrail.CreateLog(clsAudit.enmActivityType.Other, "Smart Orders Closed", gstrLoginName, gstrClientMachineName, m_PatientID)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartOrders, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub tblSmartDiagnosis_ItemClicked(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tblSmartOrders.ItemClicked
        Try
            Select Case e.ClickedItem.Tag
                Case "Save"
                    Call SaveAssociation()
                    ' Me.Close()
                Case "Close"
                    frmPatientExam.nRefTempID = 0
                    'Me.Close()
                    gloWord.WordDialogBoxBackgroundCloser.Close(Me)
            End Select
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartOrders, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "Smart Orders", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub mnuOperOrderAss_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuOperOrderAss.Click
        Try
            Dim frm As New frmOrderAssociation
            With frm
                .IsOpenfrmSmartOrder = True
                .OrderNode = OrderNode
                arrOrders = New ArrayList
                ' Dim lst As New myList
                'lst.Index = OrderNode.Key
                'lst.Value = OrderNode.Name
                Dim lst As New myTreeNode
                lst.Key = OrderNode.Key
                lst.Text = OrderNode.Name
                arrOrders.Add(lst)
                .arrSelectedNodes = Nothing
                .arrSelectedNodes = arrOrders
                '.MdiParent = Me
                .WindowState = FormWindowState.Normal
                .Owner = Me
                '.ShowDialog(IIf(IsNothing(frm.Parent), Me, frm.Parent))
                .ShowDialog(frm.Parent)
                'If arrSelectedNode.Count > 0 Then
                '    trOrderAssociation.Nodes.Item(0).Nodes.Clear()
                '    For i As Integer = 0 To arrSelectedNode.Count - 1
                '        AddNode(CType(arrSelectedNode.Item(i), myTreeNode))
                '    Next
                'End If
                arrSelectedNode = New ArrayList
                For Each myReqNode As TreeNode In trOrderAssociation.Nodes.Item(0).Nodes
                    If myReqNode.Nodes.Count > 0 Then
                        myReqNode.Nodes.Clear()
                    End If
                    arrSelectedNode.Add(CType(myReqNode, myTreeNode))
                Next

                If arrSelectedNode.Count > 0 Then
                    trOrderAssociation.Nodes.Item(0).Nodes.Clear()
                    For i As Integer = 0 To arrSelectedNode.Count - 1
                        AddNode(CType(arrSelectedNode.Item(i), myTreeNode))
                    Next
                End If

            End With
            frm.Dispose()
            frm = Nothing
        Catch objErr As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartOrders, gloAuditTrail.ActivityType.General, objErr.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    'Private Sub trOrder_NodeMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles trOrder.NodeMouseClick
    '    Try
    '        trOrder.SelectedNode = e.Node
    '        If e.Button = MouseButtons.Right Then
    '            If Not IsNothing(trOrder.SelectedNode) Then

    '                'Dim trvNode As TreeNode
    '                'trvNode = trOrder.GetNodeAt(e.X, e.Y)
    '                'Dim lst As New myList
    '                'lst.Index = trvNode.Tag
    '                'lst.Value = trvNode.Text
    '                arrSelectedNode.Clear()
    '                arrSelectedNode.Add(CType(trOrder.SelectedNode, myTreeNode))
    '            End If
    '        End If
    '    Catch ex As Exception

    '    End Try
    'End Sub

    Private Sub trOrderAssociation_NodeMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles trOrderAssociation.NodeMouseClick
        Try
            If e.Button = MouseButtons.Right Then

                trOrderAssociation.SelectedNode = e.Node
                'Dim trvNode As TreeNode
                'trvNode = trOrderAssociation.GetNodeAt(e.X, e.Y)
                'If IsNothing(trvNode) = False Then
                '    trOrderAssociation.SelectedNode = trvNode
                'End If

                If IsNothing(trOrderAssociation.SelectedNode.Parent) = True Then
                    'Try
                    '    If (IsNothing(trOrderAssociation.ContextMenu) = False) Then
                    '        trOrderAssociation.ContextMenu.Dispose()
                    '        trOrderAssociation.ContextMenu = Nothing
                    '    End If
                    'Catch ex As Exception

                    'End Try
                    trOrderAssociation.ContextMenu = Nothing
                    Exit Sub
                End If

                If IsNothing(trOrderAssociation.SelectedNode) = False Then

                    'If trvNode.Text = "Order Templates" Then
                    '    trOrder.ContextMenu = Nothing 
                    '    Exit Sub
                    'End If
                    If IsNothing(trOrderAssociation.SelectedNode) = False Then
                        trOrderAssociation.SelectedNode = trOrderAssociation.SelectedNode
                        'Try
                        '    If (IsNothing(trOrderAssociation.ContextMenu) = False) Then
                        '        trOrderAssociation.ContextMenu.Dispose()
                        '        trOrderAssociation.ContextMenu = Nothing
                        '    End If
                        'Catch ex As Exception

                        'End Try
                        trOrderAssociation.ContextMenu = cntOrderAssociation
                        OrderNode = trOrderAssociation.SelectedNode
                    End If
                    ''If trICD9Association.Nodes.Item(0).Text = trICD9Association.SelectedNode.Text Or trICD9Association.SelectedNode.Parent Is trICD9Association.Nodes.Item(0) Or (CType(trICD9Association.SelectedNode, myTreeNode).Key = -1) Then
                    If trOrderAssociation.Nodes(0).Text = trOrderAssociation.SelectedNode.Parent.Text Then
                        'Try
                        '    If (IsNothing(trOrderAssociation.ContextMenu) = False) Then
                        '        trOrderAssociation.ContextMenu.Dispose()
                        '        trOrderAssociation.ContextMenu = Nothing
                        '    End If
                        'Catch ex As Exception

                        'End Try
                        trOrderAssociation.ContextMenu = cntOrderAssociation
                    Else
                        'Try
                        '    If (IsNothing(trOrderAssociation.ContextMenu) = False) Then
                        '        trOrderAssociation.ContextMenu.Dispose()
                        '        trOrderAssociation.ContextMenu = Nothing
                        '    End If
                        'Catch ex As Exception

                        'End Try
                        trOrderAssociation.ContextMenu = Nothing
                        'treeindex = trPrescriptionDetails.SelectedNode.Index
                    End If
                End If

            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartOrders, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, "Smart Orders", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    '' SUDHIR 20090707 ''
#Region " Tree Check Uncheck "
    Private Sub CheckAllChildren(ByVal tn As TreeNode, ByVal bCheck As [Boolean])
        bParentTrigger = False
        For Each ctn As TreeNode In tn.Nodes
            bChildTrigger = False
            ctn.Checked = bCheck
            bChildTrigger = True

            CheckAllChildren(ctn, bCheck)
        Next
        bParentTrigger = True
    End Sub

    Private Sub CheckMyParent(ByVal tn As TreeNode, ByVal bCheck As [Boolean])
        If tn Is Nothing Then
            Exit Sub
        End If
        If tn.Parent Is Nothing Then
            Exit Sub
        End If

        bChildTrigger = False
        bParentTrigger = False

        If bCheck Then
            Dim bNodeFound As Boolean = False
            For Each _Node As TreeNode In tn.Parent.Nodes
                If _Node.Checked = False Then
                    tn.Parent.Checked = False
                    bNodeFound = True
                    Exit For
                End If
            Next
            If bNodeFound = False Then
                tn.Parent.Checked = True
            End If
        Else
            tn.Parent.Checked = bCheck
        End If

        CheckMyParent(tn.Parent, bCheck)
        bParentTrigger = True
        bChildTrigger = True
    End Sub

    Private Sub trOrderAssociation_AfterCheck(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles trOrderAssociation.AfterCheck
        If bChildTrigger Then
            CheckAllChildren(e.Node, e.Node.Checked)
        End If
        If bParentTrigger Then
            CheckMyParent(e.Node, e.Node.Checked)
        End If
    End Sub

    Private Sub trOrderAssociation_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles trOrderAssociation.MouseDown
        Dim oNode As TreeNode = trOrderAssociation.GetNodeAt(e.X, e.Y)
        If oNode IsNot Nothing Then
            trOrderAssociation.SelectedNode = oNode
        End If
    End Sub
#End Region
    '' END SUDHIR ''
    Private Sub gloLabOrderScreen(Optional ByVal _arrLabs As ArrayList = Nothing)   ''_arrLabs added to show testnames on Emdeonform ,v8022

        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString)
        Dim objclsgeneral As New gloEmdeonInterface.Classes.clsGeneral()
        Dim _LoginUserProviderID As Long = 0
        Dim _PatientProviderID As Long = 0


        Dim loopcnt As Int16 = 0
        Dim objClsgloLabPatientLayer As gloEmdeonInterface.Classes.clsgloLabPatientLayer = New gloEmdeonInterface.Classes.clsgloLabPatientLayer()
        Dim objpatient As New gloPatient.Patient()
        Dim objgloPatient As New gloPatient.gloPatient(GetConnectionString)
        'Line commented by dipak 20100907 for case UC5070.003: replace m_PatientID by local variable .
        'objpatient = objgloPatient.GetPatient(m_PatientID)
        objpatient = objgloPatient.GetPatient(m_PatientID)
        'end modification by dipak

        _LoginUserProviderID = GetProviderIDForUser(_LoginUserID)
        _PatientProviderID = objpatient.DemographicsDetail.PatientProviderID

        'Commented  by madan on 20100601
        'If Not compareProvider(_PatientProviderID, _LoginUserProviderID) Then
        '    Return
        'End If

        If Not gloEmdeonInterface.Classes.clsEmdeonGeneral.CheckConnectionParameters(GetConnectionString) Then
            MessageBox.Show("Lab Settings have not been configured in gloEMR Admin." + vbCrLf + "Please complete Lab Settings before ordering.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        ' Checking Internet Connection
        Dim LabConnectionAvailable As gloEmdeonInterface.Classes.clsGeneral.InternetConnectivity
        LabConnectionAvailable = objclsgeneral.IsInternetConnectionAvailable()
        If LabConnectionAvailable = gloEmdeonInterface.Classes.clsGeneral.InternetConnectivity.Success Then
            ' Checking gloLab Admin Setting 
            'if(clsEmdeonGeneral.gloLab_boolValidateURL==false)
            '{ 
            ' for (int loopCnt = 0; loopCnt < 1; loopCnt++)
            ' {
            ' clsEmdeonGeneral.ValidateGloLabUrl(_dataBaseConnectionString);
            ' }
            ' if (clsEmdeonGeneral.gloLab_boolValidateURL == false)
            ' {
            ' MessageBox.Show("Please check admin settings.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ' return;
            ' }

            '}


            'Modified by madan on 20100601
            If Not compareProvider(_PatientProviderID, _LoginUserProviderID) Then
                Return
            End If


            ''if (gloEmdeonInterface.Classes.clsEmdeonGeneral.emdeonUserName.ToString().Trim() != "" && gloEmdeonInterface.Classes.clsEmdeonGeneral.emdeonUserPassword.ToString().Trim() != "" && gloEmdeonInterface.Classes.clsEmdeonGeneral.emdeonURL.ToString().Trim() != "" && gloEmdeonInterface.Classes.clsEmdeonGeneral.emdeonFacilityCode.ToString().Trim() != "" && gloEmdeonInterface.Classes.clsEmdeonGeneral.gloLab_hsilabel.ToString().Trim() != "")
            ''{
            ''Dim loopcnt As Int16 = 0
            ''Dim objClsgloLabPatientLayer As gloEmdeonInterface.Classes.clsgloLabPatientLayer = New gloEmdeonInterface.Classes.clsgloLabPatientLayer()
            ''Dim objpatient As New gloPatient.Patient()
            ''Dim objgloPatient As New gloPatient.gloPatient(GetConnectionString)
            ''objpatient = objgloPatient.GetPatient(m_PatientID)


            ''Isurance,Patient address verification for billing 
            'Dim _billingtype As String = GetPatientBillingType(objpatient)
            'gloEmdeonInterface.Classes.clsEmdeonGeneral.gloLab_BillingType = _billingtype
            'Dim _billingStatus As String = String.Empty
            ''******************************************************************//
            ''Check's the EMRAdmin Settings for billingstatus.... "ASK","YES","NO"
            ''If billing status is "YES" then patinet is directly registerd to emdeon.
            ''else billing status is "NO" then patinet is not registerd to emdeon.
            ''if the billing status is "ASK" then insurance validation,patient address validation,gurantor address validation is done,according to that billing type will be selected.
            ''******************************************************************//
            ''message box code added by madan- according to the desgin given by prasad sir..
            ''*********
            'Dim Encoder As System.Text.Encoding = System.Text.ASCIIEncoding.[Default]
            'Dim buffer As [Byte]() = New Byte() {CByte(149)}
            'Dim bullet As String = Encoding.GetEncoding(1252).GetString(buffer)
            ''''//********
            'If ConfirmNull(gloEmdeonInterface.Classes.clsEmdeonGeneral.gloLab_BillingStatus.ToString()) Then
            '    Select Case gloEmdeonInterface.Classes.clsEmdeonGeneral.gloLab_BillingStatus
            '        Case "Yes"
            '            _billingStatus = "confirm"
            '            Exit Select
            '        Case "No"
            '            If _billingtype <> "" Then
            '                If (_billingtype <> "T") AndAlso (_billingtype <> "P") AndAlso (_billingtype = "C") Then
            '                    Dim dlgResult As DialogResult = MessageBox.Show((("This patient does not have an address or insurance policy set up. This " & vbCr & vbLf & "practice requires a patient or an insurance policy to be billed for all labs. " & vbCr & vbLf & vbCr & vbLf & "In order to use gloLabs for this patient, you must either: " & vbCr & vbLf & vbCr & vbLf & " ") + bullet & " Set up billing information for the patient, or" & vbCr & vbLf & " ") + bullet & " Change the practice setting in the Admin program.", "gloLab Billing Status", MessageBoxButtons.OK)
            '                    If dlgResult = DialogResult.Yes Then
            '                        _billingStatus = "confirm"
            '                    ElseIf dlgResult = DialogResult.No Then
            '                        _billingStatus = "notconfirm"
            '                    End If
            '                Else
            '                    _billingStatus = "confirm"
            '                End If
            '            Else
            '                _billingStatus = "notconfirm"
            '            End If
            '            Exit Select
            '        Case "Ask"
            '            If _billingtype <> "" Then
            '                If (_billingtype <> "T") AndAlso (_billingtype <> "P") AndAlso (_billingtype = "C") Then
            '                    Dim dlgResult As DialogResult = MessageBox.Show("This patient does not have an address or insurance policy set up." & vbCr & vbLf & "Would you like to proceed with lab ordering?" & vbCr & vbLf & vbCr & vbLf & "Note: The practice will be billed for any tests ordered.", "gloLab Billing Status", MessageBoxButtons.YesNo)
            '                    If dlgResult = DialogResult.Yes Then
            '                        _billingStatus = "confirm"
            '                    ElseIf dlgResult = DialogResult.No Then
            '                        MessageBox.Show((("In order to use gloLabs for this patient, you must either: " & vbCr & vbLf & vbCr & vbLf & " ") + bullet & " Set up billing information for the patient, or" & vbCr & vbLf & " ") + bullet & " Change the practice setting in the Admin program.", "gloLab Billing Status", MessageBoxButtons.OK)
            '                        _billingStatus = "notconfirm"
            '                    End If
            '                Else
            '                    _billingStatus = "confirm"
            '                End If
            '            Else
            '                _billingStatus = "notconfirm"
            '            End If
            '            Exit Select
            '        Case Else
            '            _billingStatus = "notconfirm"
            '            Exit Select
            '    End Select
            'Else
            '    _billingStatus = "notconfirm"
            'End If




            ' If patinet does not insurance or guarnator inforamtion then it is considerd as client billing...

            'Added by madan on 20100601
            Dim _billingStatus As Boolean = False

            Dim objGloPatientLayer = New gloEmdeonInterface.Classes.clsgloLabPatientLayer()
            _billingStatus = objClsgloLabPatientLayer.CheckBillingType(objpatient)


            If _billingStatus = True Then
                If gloEmdeonInterface.Classes.clsEmdeonGeneral.IsDemoLab = True Then
                    Dim frmLabDemo As New gloEmdeonInterface.Forms.frmLabDemonstration(m_PatientID)
                    frmLabDemo.WindowState = FormWindowState.Maximized
                    frmLabDemo.BringToFront()
                    frmLabDemo.ShowDialog(IIf(IsNothing(frmLabDemo.Parent), Me, frmLabDemo.Parent))
                    frmLabDemo.Dispose()
                Else
                    Dim strQry As String = String.Empty
                    Dim boolPatientReg As [Boolean] = False
                    ' flag for patient registration 
                    'string strQry="select count(*) from patient_gloLab where nPatientId="_patientID;
                    If ConfirmNull(objpatient.DemographicsDetail.PatientCode.ToString()) Then
                        'strQry = "select count(*) from patient_gloLab where sPatientCode='" & objpatient.DemographicsDetail.PatientCode.ToString().Trim() & "'"
                        strQry = "SELECT COUNT(*) FROM PatientExternalCodes INNER JOIN Patient ON PatientExternalCodes.nPatientId = Patient.nPatientID  where PatientExternalCodes.sExternalType = 'EMDEON' AND  Patient.sPatientCode='" & objpatient.DemographicsDetail.PatientCode.ToString().Trim() & "'"
                    End If
                    oDB.Connect(False)

                    ' loop for checking patient registration
                    'Try 3 times for patient registration if fails to register.
                    For loopcnt = 1 To 3
                        ' checking patient is registered or not
                        'Int32 cnt = Convert.ToInt32(objSqlCmd.ExecuteScalar());
                        Dim cnt As Int32 = 0
                        cnt = Convert.ToInt32(oDB.ExecuteScalar_Query(strQry))
                        If cnt < 1 Then
                            ' if cnt is gretaer than zero means patient registered
                            'lblProcessInformation.Text = "Launching gloLab....";
                            'pnlregistration.Visible = True
                            'pnlregistration.BringToFront()
                            Application.DoEvents()

                            ' code for patient registration 
                            'gloEmdeonInterface.Classes.clsEmdeonGeneral.sConnectionString = _dataBaseConnectionString;

                            'Line commented by dipak 20100907 for case UC5070.003: replace m_PatientID by local variable .
                            'gloEmdeonInterface.Classes.clsEmdeonGeneral.gloLab_patID = m_PatientID
                            gloEmdeonInterface.Classes.clsEmdeonGeneral.gloLab_patID = m_PatientID
                            'end modification 

                            'boolPatientReg = objClsgloLabPatientLayer.RegisterPatienttoEmdeon(objpatient);
                            boolPatientReg = objClsgloLabPatientLayer.RegisterGloPatient(objpatient, GetConnectionString)

                            'lblProcessInformation.Text = "Process Information";
                            'gloEmdeonInterface.Forms.frmViewgloLab.pnlregistration.Visible = False
                            If boolPatientReg Then
                                Exit For
                            End If
                        Else
                            boolPatientReg = True
                            Exit For
                        End If

                    Next

                    If boolPatientReg = True Then
                        ' if patient is registered
                        'Line commented by dipak 20100907 for case UC5070.003: replace m_PatientID by local variable .
                        'Dim objfrmEmdonInterface As New gloEmdeonInterface.Forms.frmEmdeonInterface(m_PatientID)
                        Dim objfrmEmdonInterface As New gloEmdeonInterface.Forms.frmEmdeonInterface(m_PatientID)
                        objfrmEmdonInterface.LoginProviderID = gnLoginProviderID
                        'end modification 
                        Dim strLabTestName As String = ""  ''added to show testnames on Emdeonform ,v8022
                        If Not IsNothing(_arrLabs) Then
                            For i As Int32 = 0 To _arrLabs.Count - 1
                                If i = 0 Then
                                    strLabTestName = CType(_arrLabs.Item(i), gloEmdeonCommon.myList).Value
                                Else
                                    strLabTestName = strLabTestName & "~" & CType(_arrLabs.Item(i), gloEmdeonCommon.myList).Value
                                End If
                            Next
                        End If
                        objfrmEmdonInterface.TestsName = strLabTestName
                        objfrmEmdonInterface.WindowState = FormWindowState.Maximized
                        objfrmEmdonInterface.ShowDialog(IIf(IsNothing(objfrmEmdonInterface.Parent), Me, objfrmEmdonInterface.Parent))
                        objfrmEmdonInterface.Dispose()
                        objfrmEmdonInterface = Nothing
                        '' By Abhijeet Farkande on date 20100223
                        '' changes : refreshing the Order information for patient
                        'fillOpenOrdsGrid()
                        'gloUCLab_History_gUC_FillOrder(2)
                        ''gloUCLab_Transaction.ClearTest();
                        '' end of changes by Abhijeet for refreshing the order details.
                        'gloEmdeonInterface.Forms.frmViewgloLab.gloLabUC_Transaction1.ClearTest()
                    Else

                        If ConfirmNull(gloEmdeonInterface.Classes.clsEmdeonGeneral.gloLab_Identifier.ToString()) Then
                            MessageBox.Show(gloEmdeonInterface.Classes.clsEmdeonGeneral.gloLab_Identifier.ToString().Trim(), "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Else
                            MessageBox.Show("Patient is not registered With Emdeon,please try again.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End If
                    End If
                    ' }
                    'else
                    '{
                    ' MessageBox.Show("Please check admin settings.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    '}
                End If
            End If
        Else

            If LabConnectionAvailable = gloEmdeonInterface.Classes.clsGeneral.InternetConnectivity.NoInternet Then

                ' MessageBox.Show("Connection error, internet connection not available." & vbCr & vbLf & vbCr & vbLf & "You must be connected to the internet to access gloLab orders.", "gloEMR", MessageBoxButtons.OK)
                Dim objFrmConnectionConfirm As New gloEmdeonInterface.Forms.frmConfirmInternetConnection(True)
                objFrmConnectionConfirm.ShowInTaskbar = False
                objFrmConnectionConfirm.ShowDialog(IIf(IsNothing(objFrmConnectionConfirm.Parent), Me, objFrmConnectionConfirm.Parent))
                objFrmConnectionConfirm.Dispose()

            ElseIf LabConnectionAvailable = gloEmdeonInterface.Classes.clsGeneral.InternetConnectivity.ServerNotresponding Then
                Dim objFrmConnectionConfirm As New gloEmdeonInterface.Forms.frmConfirmInternetConnection(False)
                objFrmConnectionConfirm.ShowInTaskbar = False
                objFrmConnectionConfirm.ShowDialog(IIf(IsNothing(objFrmConnectionConfirm.Parent), Me, objFrmConnectionConfirm.Parent))
                objFrmConnectionConfirm.Dispose()

            End If
        End If

    End Sub

    'Madan--Added for billing type:20100302
    'Protected Function GetPatientBillingType(ByVal opatient As gloPatient.Patient) As String
    '    Dim _billingtype As String = String.Empty
    '    Dim objClsgloLabPatient As New gloEmdeonInterface.Classes.clsgloLabPatientLayer()
    '    Try
    '        _billingtype = objClsgloLabPatient.GetPatientBillingType(opatient)
    '        If _billingtype = "T" Then
    '            'clsEmdeonGeneral.gloLab_insuranceIndex for this variable value is assigned in clsgloPatientLayer--validateInsurance() method 

    '            If opatient.InsuranceDetails.InsurancesDetails.Count > 0 Then
    '                If opatient.InsuranceDetails.InsurancesDetails(gloEmdeonInterface.Classes.clsEmdeonGeneral.gloLab_insuranceIndex).InsuranceFlag = 2 Then
    '                    'DialogResult drIns = MessageBox.Show("Primary insurance information is not available for the patient,So Would you like to continue with secondary insurance Or territory insurance.", "Labs", MessageBoxButtons.YesNo, MessageBoxIcon.Question); 
    '                    Dim drIns As DialogResult = MessageBox.Show("This patient does not have primary insurance policy set up. This " & vbCr & vbLf & "practice requires insurance policy to be billed for all labs " & vbCr & vbLf & vbCr & vbLf & "Would you like to continue with secondary or tertiary insurance ? " & vbCr & vbLf & vbCr & vbLf, gstrMessageBoxCaption, MessageBoxButtons.YesNo)
    '                    If drIns = DialogResult.Yes Then
    '                        _billingtype = "T"
    '                    Else
    '                        _billingtype = "c"

    '                    End If
    '                ElseIf opatient.InsuranceDetails.InsurancesDetails(gloEmdeonInterface.Classes.clsEmdeonGeneral.gloLab_insuranceIndex).InsuranceFlag = 3 Then
    '                    'DialogResult drIns = MessageBox.Show("Primary & secondary insurance information is not available for the patient,So Would you like to continue with secondary insurance Or territory insurance.", "Labs", MessageBoxButtons.YesNo, MessageBoxIcon.Question); 
    '                    Dim drIns As DialogResult = MessageBox.Show("This patient does not have primary or secondary insurance policy set up. This " & vbCr & vbLf & "practice requires insurance policy to be billed for all labs " & vbCr & vbLf & vbCr & vbLf & "Would you like to continue with tertiary insurance ? " & vbCr & vbLf & vbCr & vbLf, gstrMessageBoxCaption, MessageBoxButtons.YesNo)
    '                    If drIns = DialogResult.Yes Then

    '                        _billingtype = "T"
    '                    Else
    '                        _billingtype = "c"

    '                    End If
    '                End If

    '            End If
    '        End If
    '    Catch ex As Exception

    '        _billingtype = String.Empty
    '    End Try

    '    Return _billingtype
    'End Function
    Protected Function ConfirmNull(ByVal strValue As String) As Boolean
        Dim blnCheck As Boolean = False
        Try
            If strValue IsNot Nothing AndAlso strValue.ToString().Trim().Length <> 0 AndAlso strValue.ToString() <> "" Then

                blnCheck = True
            End If
        Catch ex As Exception
            'objclsGeneral.UpdateLog("Null reference value!" + ex.ToString());
        End Try
        Return blnCheck
    End Function
    'Added by madan-- on 20100419 
    Public Function GetProviderIDForUser(ByVal UserID As Int64) As Int64
        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString)
        Dim ProID As Int64 = 0
        Try
            oDB.Connect(False)
            'ProID = Trim(oDB.ExecuteScaler) 
            ProID = Convert.ToInt64(oDB.ExecuteScalar_Query("SELECT nProviderID from user_mst where nUserID = " & UserID & ""))
            oDB.Disconnect()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)

            ProID = 0
        Finally
            oDB.Dispose()
        End Try
        Return ProID
    End Function
    ' added by madan on 20100419-- for provider comparison. 
    Private Function compareProvider(ByVal _PatientProviderID As Int64, ByVal _LoginUserProviderID As Int64) As Boolean

        'Dim objClsGeneral As New gloEmdeonInterface.Classes.clsGeneral()
        'Dim strProviderName As String = String.Empty
        'Dim strLoginUserName As String = String.Empty
        'Dim strLabID As String = String.Empty

        Try

            '12-May-14 Aniket: Remove the validations as some are not needed and some are moved to the Emdeon Screen.
            Return True

            'If _PatientProviderID <> 0 Then
            '    strProviderName = objClsGeneral.GetProviderName(_PatientProviderID, _ClinicID)
            'End If
            'If _LoginUserProviderID <> 0 Then
            '    strLoginUserName = objClsGeneral.GetProviderName(_LoginUserProviderID, _ClinicID)
            'End If
            'If _LoginUserProviderID = 0 Then
            '    'Modified as per 'DREW NALON' Commnets by madan on 20100515 

            '    Dim drMesgResult As DialogResult = MessageBox.Show(("The user you are using is not set up as a provider. If you proceed, the lab order " & vbCr & vbLf & "provider will be defaulted to the current patient’s provider '") + strProviderName & "'." & vbCr & vbLf & vbCr & vbLf & "Would you like to proceed with creating a new order?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question)


            '    'if (MessageBox.Show("Login user is not a provider, Do you like to proceed placing orders with labs as patient provider", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) 
            '    If drMesgResult = DialogResult.Yes Then
            '        strLabID = objClsGeneral.GetProvidergloLabId(_PatientProviderID)
            '        If ConfirmNull(strLabID.ToString()) Then
            '            Return True
            '        Else
            '            If MessageBox.Show("The current provider '" & strProviderName & " ' does not have a lab ID set up." & vbCr & vbLf & "If you place a lab order, you will have to select a provider in the labs interface." & vbCr & vbLf & "Would you like to proceed with the lab order? " & vbCr & vbLf & vbCr & vbLf & "To set up a lab ID, go to provider settings.", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            '                Return True
            '            Else
            '                Return False
            '            End If
            '        End If
            '    Else
            '        Return False
            '    End If
            'End If

            'If _LoginUserProviderID <> _PatientProviderID Then
            '    'Modified as per "DREW NALON" Comments by madan on 20100515. 
            '    Dim dgResult As DialogResult = MessageBox.Show((("This patient is currently assigned to the provider '" & strProviderName & "'.Would " & vbCr & vbLf & "you like to change the patient provider to '") + strLoginUserName & "' ? " & vbCr & vbLf & vbCr & vbLf & "If you select 'No', the lab order will be created for '") + strProviderName & "'.", gstrMessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)

            '    If dgResult = DialogResult.Yes Then
            '        'Line commented by dipak 20100907 for case UC5070.003: replace m_PatientID by local variable .
            '        'If objClsGeneral.changePatientProvider(_LoginUserProviderID, m_PatientID) Then
            '        If objClsGeneral.changePatientProvider(_LoginUserProviderID, m_PatientID) Then
            '            'end modification 
            '            strLabID = objClsGeneral.GetProvidergloLabId(_LoginUserProviderID)
            '            If ConfirmNull(strLabID.ToString()) Then
            '                Return True
            '            Else
            '                If MessageBox.Show("The current provider '" & strLoginUserName & " ' does not have a lab ID set up." & vbCr & vbLf & "If you place a lab order, you will have to select a provider in the labs interface." & vbCr & vbLf & "Would you like to proceed with the lab order? " & vbCr & vbLf & vbCr & vbLf & "To set up a lab ID, go to provider settings.", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            '                    Return True
            '                Else
            '                    Return False
            '                End If
            '            End If
            '        Else
            '            Return False
            '        End If
            '    ElseIf dgResult = DialogResult.No Then
            '        strLabID = objClsGeneral.GetProvidergloLabId(_PatientProviderID)
            '        If ConfirmNull(strLabID.ToString()) Then
            '            Return True
            '        Else
            '            'Modified as per "DREW NALON" Comments by madan on 20100515. 
            '            If MessageBox.Show("The current provider '" & strProviderName & " ' does not have a lab ID set up." & vbCr & vbLf & "If you place a lab order, you will have to select a provider in the labs interface." & vbCr & vbLf & "Would you like to proceed with the lab order? " & vbCr & vbLf & vbCr & vbLf & "To set up a lab ID, go to provider settings.", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            '                Return True
            '            Else
            '                Return False
            '            End If
            '        End If
            '    ElseIf dgResult = DialogResult.Cancel Then
            '        Return False

            '    End If
            'End If

            'If _LoginUserProviderID = _PatientProviderID Then
            '    strLabID = objClsGeneral.GetProvidergloLabId(_LoginUserProviderID)
            '    If ConfirmNull(strLabID.ToString()) Then
            '        Return True
            '    Else
            '        If MessageBox.Show("The current provider '" & strLoginUserName & " ' does not have a lab ID set up." & vbCr & vbLf & "If you place a lab order, you will have to select a provider in the labs interface." & vbCr & vbLf & "Would you like to proceed with the lab order? " & vbCr & vbLf & vbCr & vbLf & "To set up a lab ID, go to provider settings.", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            '            Return True
            '        Else
            '            Return False
            '        End If
            '    End If
            'Else
            '    Return False
            'End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
            Return False
        End Try

    End Function

    'Added by madan on 20100616
    '' By Abhijeet on 20100625,Added optional parameter 'ArrList' in function defination
    Private Sub gloLabSettings(ByVal _TaskType As String, Optional ByVal _TestNames As String = "", Optional ByVal _arrLabs As ArrayList = Nothing)

        Select Case _TaskType.ToString().ToUpper()
            Case "TASK"
                Dim objClsGeneral As New gloEmdeonInterface.Classes.clsGeneral()
                Dim objClsgloLabPatientLayer As gloEmdeonInterface.Classes.clsgloLabPatientLayer = New gloEmdeonInterface.Classes.clsgloLabPatientLayer()
                Dim objpatient As New gloPatient.Patient()
                Dim objgloPatient As New gloPatient.gloPatient(GetConnectionString)
                'Line commented by dipak 20100907 for case UC5070.003: replace m_PatientID by local variable .
                'objpatient = objgloPatient.GetPatient(m_PatientID)
                objpatient = objgloPatient.GetPatient(m_PatientID)
                'end modification

                'Developer: Sanjog(Dhamke)
                'Date:14 Dec 2011
                'Bug ID/PRD Name/Salesforce Case: Lab Usability PRD (6060) Show Task Information on Emdeon Lab 
                'Reason: To show task info
                Dim strLabTest As String = ""
                Dim strLabTests As String = ""
                If Not IsNothing(_arrLabs) Then
                    For i As Integer = 0 To _arrLabs.Count - 1
                        strLabTest = CType(_arrLabs.Item(i), gloEmdeonCommon.myList).ID & "~" & CType(_arrLabs.Item(i), gloEmdeonCommon.myList).Value
                        If i = 0 Then
                            strLabTests = strLabTest
                        Else
                            strLabTests = strLabTests & "|" & strLabTest
                        End If
                    Next
                End If
                'END - Sanjog
                Dim _LoginProviderId As Int64 = 0

                _LoginProviderId = GetProviderIDForUser(_LoginUserID)
                objClsGeneral.TestList = _TestNames
                objClsGeneral.TestlistOnly = strLabTests
                ''Added by Abhijeet on 20100625 , for audit trial implementation
                ''Modified by madan-- on 20100726-- for exam id.
                'Line commented by dipak 20100907 for case UC5070.003: replace m_PatientID by local variable .
                'Dim nTaskID As Long = objClsGeneral.AssignTaskToUser(m_PatientID, objpatient.DemographicsDetail.PatientProviderID, _LoginProviderId, m_ExamID, gloTaskMail.TaskType.Task)
                Dim nTaskID As Long = objClsGeneral.AssignTaskToUser(m_PatientID, objpatient.DemographicsDetail.PatientProviderID, _LoginProviderId, m_ExamID, gloTaskMail.TaskType.PlaceLabOrder)
                'end modification 
                If nTaskID > 0 Then
                    'Parameter m_PatientID changed to m_PatientID in audit trial
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Task, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.LabOrderRequest, "Task assigned for placing lab order", m_PatientID, 0, _LoginProviderId, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                End If
                '' End of changes by Abhijeet on 20100625

                _LoginProviderId = 0
            Case "LABORDER"

                'If gloEmdeonInterface.Classes.clsEmdeonGeneral.IsDemoLab = True Then
                '    Return
                'End If

                gloLabOrderScreen(_arrLabs)  ''added to show testnames on Emdeonform ,v8022 

            Case "RECORDRESULTS"
                'Line commented by dipak 20100907 for case UC5070.003: replace m_PatientID by local variable .
                'Dim frmNormalLab As New gloEmdeonInterface.Forms.frmViewNormalLab(m_PatientID)
                Dim frmNormalLab As New gloEmdeonInterface.Forms.frmViewNormalLab(m_PatientID)
                AddHandler frmNormalLab.Event_CallCDA, AddressOf mdlGeneral.OpenCDA
                'end modification
                frmNormalLab.ArrLabs = _arrLabs '' Added by Abhijeet on 20100624
                frmNormalLab.WindowState = FormWindowState.Maximized
                frmNormalLab.ShowInTaskbar = False
                frmNormalLab.BringToFront()
                frmNormalLab.ShowDialog(IIf(IsNothing(frmNormalLab.Parent), Me, frmNormalLab.Parent))
                RemoveHandler frmNormalLab.Event_CallCDA, AddressOf mdlGeneral.OpenCDA
                frmNormalLab.Dispose()
            Case Else
                MessageBox.Show("Please configure Default user for Task in EMR Admin - Lab Settings", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Select
        End Select
    End Sub

    Private Sub btnClear_Click(sender As System.Object, e As System.EventArgs) Handles btnClear.Click
        'Use to clear search text
        txtsearchOrders.ResetText()
        txtsearchOrders.Focus()
    End Sub
End Class



