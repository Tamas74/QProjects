Option Compare Text
Imports System.IO
Imports oOffice = Microsoft.Office.Core
Imports Wd = Microsoft.Office.Interop.Word
'Imports gloEMR.gloAuditTrail.gloAuditTrail
Imports gloEMR.gloEMRWord
Imports gloUserControlLibrary
Imports System.Runtime.InteropServices

Public Class frmCPTTemplate
    Inherits System.Windows.Forms.Form
    Implements ISignature
    Implements IHotKey
    Implements gloVoice
    Implements IPatientContext
    'Instantiate voice class
    Private ogloVoice As ClsVoice
    'implement interface for Voice --supriya 03/06/2009
    'Dim _PatientID As Long
    Private m_VisitID As Long
    Private m_CPTID As Long
    Private m_TemplateID As Long
    Private m_VisitDate As Date
    Private ImagePath As String
    Private m_PatientID As Long
    Private m_FormID As Long
    Dim objclsCPTAssociation As clsCPTAssociation
    Private WithEvents oCurDoc As Wd.Document
    'Private WithEvents oTempDoc As Wd.Document
    Private WithEvents oWordApp As Wd.Application
    Dim tblTemplate As New DataTable
    ''''r("CPTID") = dt.Rows(i)(0)     '--0
    ''''r("CPT") = dt.Rows(i)(1)   '--1
    ''''r("TemplateID") = dt.Rows(i)(2)    '--2
    ''''r("Template") = dt.Rows(i)(3)  '--3
    Dim objCriteria As DocCriteria
    Dim ObjWord As clsWordDocument

    Dim hashExistingTemplates As HashSet(Of Long)
    Dim hashNewTemplates As HashSet(Of Long)
    Dim bnlIsFaxOpened As Boolean
    Dim ArrLst As New ArrayList
    ''ID = CPTID
    ''INDEX = TEMPLATEID
    ''DESCRIPTION = TEMPLATE NAME
    ''TEMPLATERESULT = TEMPLATE IMAGE
    Dim blnSignClick As Boolean = False

    Dim CPTID As Long
    Dim TemplateID As Long
    Dim blnFormExist As Boolean
    Dim dtCPTCode As New DataTable
    Dim dtCPTdesc As New DataTable
    Private WithEvents _PatientStrip As gloUC_PatientStrip
    Friend WithEvents grdTemplateGallery As gloEMR.clsDataGrid
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Public Shared CancelClick As Boolean
    Friend WithEvents pnl_wdFormGallery As System.Windows.Forms.Panel
    Friend WithEvents wdFormGallery As AxDSOFramer.AxFramerControl
    Friend WithEvents pnlPatientHeader As System.Windows.Forms.Panel
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtTemplateName As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents lblVisitDate As System.Windows.Forms.Label
    'Dim wdCtrlTemp As WordCtrl.WordControl
    ' Private WithEvents wdTemp As AxDSOFramer.AxFramerControl
    Private WithEvents tlsFormGallery As WordToolStrip.gloWordToolStrip
    Friend WithEvents Panel8 As System.Windows.Forms.Panel
    Friend WithEvents rbSearch2 As System.Windows.Forms.RadioButton
    Friend WithEvents rbSearch1 As System.Windows.Forms.RadioButton
    Friend WithEvents pnlSearch As System.Windows.Forms.Panel
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents pnl_PatientHeader As System.Windows.Forms.Panel
    Private WithEvents Label5 As System.Windows.Forms.Label
    Private WithEvents Label6 As System.Windows.Forms.Label
    Private WithEvents Label7 As System.Windows.Forms.Label
    Private WithEvents Label8 As System.Windows.Forms.Label
    Private WithEvents Label9 As System.Windows.Forms.Label
    Private WithEvents Label17 As System.Windows.Forms.Label
    Private WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Private WithEvents Label10 As System.Windows.Forms.Label
    Private WithEvents Label11 As System.Windows.Forms.Label
    Private WithEvents Label12 As System.Windows.Forms.Label
    Private WithEvents Label13 As System.Windows.Forms.Label
    Private WithEvents Label40 As System.Windows.Forms.Label
    Private WithEvents Label41 As System.Windows.Forms.Label
    Private WithEvents Label42 As System.Windows.Forms.Label
    Private WithEvents Label43 As System.Windows.Forms.Label
    Friend WithEvents pnl_grdTemplateGallery As System.Windows.Forms.Panel
    Private WithEvents Label27 As System.Windows.Forms.Label
    Private WithEvents Label28 As System.Windows.Forms.Label
    Private WithEvents Label29 As System.Windows.Forms.Label
    Private WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents pnl_Grid As System.Windows.Forms.Panel
    Friend WithEvents pnl_trvCptAssocation As System.Windows.Forms.Panel
    Private WithEvents Label23 As System.Windows.Forms.Label
    Private WithEvents Label24 As System.Windows.Forms.Label
    Private WithEvents Label25 As System.Windows.Forms.Label
    Private WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents pnl_RadioBtn As System.Windows.Forms.Panel
    Private WithEvents Label14 As System.Windows.Forms.Label
    Private WithEvents Label15 As System.Windows.Forms.Label
    Private WithEvents Label16 As System.Windows.Forms.Label
    Private WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents Label44 As System.Windows.Forms.Label
    Private WithEvents Label45 As System.Windows.Forms.Label
    Private WithEvents Label46 As System.Windows.Forms.Label
    Private WithEvents Label47 As System.Windows.Forms.Label
    Private WithEvents Label48 As System.Windows.Forms.Label
    Friend WithEvents pnlToolstripContainer As System.Windows.Forms.Panel
    Friend WithEvents GloUC_trvCPT As gloUserControlLibrary.gloUC_TreeView
    Friend WithEvents Splitter2 As System.Windows.Forms.Splitter
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Private WithEvents Label36 As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents GloUC_AddRefreshDic1 As gloUserControlLibrary.gloUC_AddRefreshDic
    Private WithEvents Label31 As System.Windows.Forms.Label
    Private WithEvents Label32 As System.Windows.Forms.Label
    Private WithEvents Label33 As System.Windows.Forms.Label
    Private WithEvents Label34 As System.Windows.Forms.Label
    Friend WithEvents Label35 As System.Windows.Forms.Label
    Friend WithEvents pnlMain As System.Windows.Forms.Panel

    Dim myidx As Int32



#Region " Windows Form Designer generated code "

    Public Sub New(ByVal PatientID As Long)
        MyBase.New()
        'm_hotKeys = New HotKeyCollection(Me)
        'This call is required by the Windows Form Designer.
        InitializeComponent()
        'Add any initialization after the InitializeComponent() call
        'Line commented and modified by dipak 20100907 for case UC5070.003: replace gnPatientID by local variable .
        'm_PatientID = gnPatientID
        m_PatientID = PatientID
        'end modification


    End Sub

    'Public Sub New(ByVal nVisitID As Long, ByVal nCPTID As Long, ByVal nTemplateID As Long, ByVal VisitDate As Date, ByVal PatientID As Long)
    '    MyBase.New()

    '    'm_hotKeys = New HotKeyCollection(Me)

    '    m_VisitID = nVisitID
    '    m_CPTID = nCPTID
    '    m_TemplateID = nTemplateID
    '    m_VisitDate = VisitDate
    '    'Line commented and modified by dipak 20100907 for case UC5070.003: replace gnPatientID by local variable .
    '    'm_PatientID = gnPatientID
    '    m_PatientID = m_PatientID
    '    'end modification

    '    'This call is required by the Windows Form Designer.
    '    InitializeComponent()

    '    'Add any initialization after the InitializeComponent() call

    'End Sub
    Public Sub New(ByVal nVisitID As Long, ByVal nCPTID As Long, ByVal nTemplateID As Long, ByVal VisitDate As Date, ByVal nPatientID As Long)
        MyBase.New()

        'm_hotKeys = New HotKeyCollection(Me)

        m_VisitID = nVisitID
        m_CPTID = nCPTID
        m_TemplateID = nTemplateID
        m_VisitDate = VisitDate
        m_PatientID = nPatientID
        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    Public Sub New(ByVal nVisitID As Long, ByVal nCPTID As Long, ByVal nTemplateID As Long, ByVal VisitDate As Date, ByVal nPatientID As Long, ByVal nFormID As Long)
        MyBase.New()

        'm_hotKeys = New HotKeyCollection(Me)

        m_VisitID = nVisitID
        m_CPTID = nCPTID
        m_TemplateID = nTemplateID
        m_VisitDate = VisitDate
        m_PatientID = nPatientID
        m_FormID = nFormID

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            Dim cntControl() As System.Windows.Forms.ContextMenu = {cntFormGallery, grdTemplateGallery.ContextMenu}
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
            Try
                gloGlobal.cEventHelper.RemoveAllEventHandlers(Me)
            Catch
            End Try

            Try

                If (IsNothing(grdTemplateGallery) = False) Then
                    grdTemplateGallery.TableStyles.Clear()
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(grdTemplateGallery)
                    grdTemplateGallery.Dispose()
                    grdTemplateGallery = Nothing
                End If
            Catch ex As Exception

            End Try


            If (IsNothing(cntControl) = False) Then
                If cntControl.Length > 0 Then
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(cntControl)
                End If
            End If

            'If (IsNothing(cntFormGallery.MenuItems) = False) Then
            '    cntFormGallery.MenuItems.Clear()
            'End If
            'If (IsNothing(grdTemplateGallery.ContextMenu.MenuItems) = False) Then
            '    grdTemplateGallery.ContextMenu.MenuItems.Clear()
            'End If

            If (IsNothing(cntControl) = False) Then
                If cntControl.Length > 0 Then
                    gloGlobal.cEventHelper.DisposeContextMenu(cntControl)
                End If
            End If

            If (GloUC_AddRefreshDic1.dtLetterAllocated) Then

                Try
                    If (IsNothing(GloUC_AddRefreshDic1.DTLETTERDATEs) = False) Then
                        Try
                            gloGlobal.cEventHelper.RemoveAllEventHandlers(GloUC_AddRefreshDic1.DTLETTERDATEs)
                        Catch ex As Exception

                        End Try


                        GloUC_AddRefreshDic1.DTLETTERDATEs.Dispose()
                        GloUC_AddRefreshDic1.DTLETTERDATEs = Nothing

                    End If
                Catch
                End Try
                GloUC_AddRefreshDic1.dtLetterAllocated = False
            End If



            If Not IsNothing(ogloVoice) Then
                ogloVoice.Dispose()
                ogloVoice = Nothing
            End If
            If (IsNothing(objclsCPTAssociation) = False) Then
                objclsCPTAssociation.Dispose()
                objclsCPTAssociation = Nothing
            End If
            If (IsNothing(tblTemplate) = False) Then
                tblTemplate.Dispose()
                tblTemplate = Nothing
            End If
            If (IsNothing(dtCPTCode) = False) Then
                dtCPTCode.Dispose()
                dtCPTCode = Nothing
            End If
            If (IsNothing(dtCPTdesc) = False) Then
                dtCPTdesc.Dispose()
                dtCPTdesc = Nothing
            End If
            If (IsNothing(ArrLst) = False) Then
                ArrLst.Clear()
                ArrLst = Nothing
            End If
            Try
                If IsNothing(_PatientStrip) = False Then

                    _PatientStrip.Dispose()
                    _PatientStrip = Nothing
                End If
            Catch

            End Try
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents pnl_Left As System.Windows.Forms.Panel
    Friend WithEvents Splitter1 As System.Windows.Forms.Splitter
    Friend WithEvents pnlCPT As System.Windows.Forms.Panel
    Friend WithEvents pnlTemplate As System.Windows.Forms.Panel
    Friend WithEvents lstCPT As System.Windows.Forms.ListBox
    Friend WithEvents lstTemplates As System.Windows.Forms.ListBox
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents trvCptAssocation As System.Windows.Forms.TreeView
    Friend WithEvents pnlGrid As System.Windows.Forms.Panel
    Friend WithEvents Label3 As System.Windows.Forms.Label
    ' System.Windows.Forms.DataGrid
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
    Friend WithEvents ImgTreeView As System.Windows.Forms.ImageList
    Friend WithEvents cntFormGallery As System.Windows.Forms.ContextMenu
    Friend WithEvents mnuRemoveForm As System.Windows.Forms.MenuItem
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCPTTemplate))
        Me.pnl_Left = New System.Windows.Forms.Panel()
        Me.pnlCPT = New System.Windows.Forms.Panel()
        Me.pnl_grdTemplateGallery = New System.Windows.Forms.Panel()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.grdTemplateGallery = New gloEMR.clsDataGrid()
        Me.cntFormGallery = New System.Windows.Forms.ContextMenu()
        Me.mnuRemoveForm = New System.Windows.Forms.MenuItem()
        Me.pnl_Grid = New System.Windows.Forms.Panel()
        Me.pnlGrid = New System.Windows.Forms.Panel()
        Me.Label45 = New System.Windows.Forms.Label()
        Me.Label46 = New System.Windows.Forms.Label()
        Me.Label47 = New System.Windows.Forms.Label()
        Me.Label48 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Splitter2 = New System.Windows.Forms.Splitter()
        Me.GloUC_trvCPT = New gloUserControlLibrary.gloUC_TreeView()
        Me.ImgTreeView = New System.Windows.Forms.ImageList(Me.components)
        Me.pnl_trvCptAssocation = New System.Windows.Forms.Panel()
        Me.trvCptAssocation = New System.Windows.Forms.TreeView()
        Me.lstCPT = New System.Windows.Forms.ListBox()
        Me.Label44 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.pnlSearch = New System.Windows.Forms.Panel()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Label40 = New System.Windows.Forms.Label()
        Me.Label41 = New System.Windows.Forms.Label()
        Me.Label42 = New System.Windows.Forms.Label()
        Me.Label43 = New System.Windows.Forms.Label()
        Me.pnl_RadioBtn = New System.Windows.Forms.Panel()
        Me.Panel8 = New System.Windows.Forms.Panel()
        Me.rbSearch2 = New System.Windows.Forms.RadioButton()
        Me.rbSearch1 = New System.Windows.Forms.RadioButton()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.pnlTemplate = New System.Windows.Forms.Panel()
        Me.lstTemplates = New System.Windows.Forms.ListBox()
        Me.Splitter1 = New System.Windows.Forms.Splitter()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.pnl_wdFormGallery = New System.Windows.Forms.Panel()
        Me.Label36 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.wdFormGallery = New AxDSOFramer.AxFramerControl()
        Me.pnl_PatientHeader = New System.Windows.Forms.Panel()
        Me.pnlPatientHeader = New System.Windows.Forms.Panel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtTemplateName = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.lblVisitDate = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.pnlToolstripContainer = New System.Windows.Forms.Panel()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.GloUC_AddRefreshDic1 = New gloUserControlLibrary.gloUC_AddRefreshDic()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.Label34 = New System.Windows.Forms.Label()
        Me.Label35 = New System.Windows.Forms.Label()
        Me.pnlMain = New System.Windows.Forms.Panel()
        Me.pnl_Left.SuspendLayout()
        Me.pnlCPT.SuspendLayout()
        Me.pnl_grdTemplateGallery.SuspendLayout()
        CType(Me.grdTemplateGallery, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnl_Grid.SuspendLayout()
        Me.pnlGrid.SuspendLayout()
        Me.pnl_trvCptAssocation.SuspendLayout()
        Me.pnlSearch.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnl_RadioBtn.SuspendLayout()
        Me.Panel8.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.pnlTemplate.SuspendLayout()
        Me.pnl_wdFormGallery.SuspendLayout()
        CType(Me.wdFormGallery, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnl_PatientHeader.SuspendLayout()
        Me.pnlPatientHeader.SuspendLayout()
        Me.pnlToolstripContainer.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.pnlMain.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnl_Left
        '
        Me.pnl_Left.Controls.Add(Me.pnlCPT)
        Me.pnl_Left.Controls.Add(Me.pnlTemplate)
        Me.pnl_Left.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnl_Left.Location = New System.Drawing.Point(0, 54)
        Me.pnl_Left.Name = "pnl_Left"
        Me.pnl_Left.Size = New System.Drawing.Size(240, 708)
        Me.pnl_Left.TabIndex = 0
        '
        'pnlCPT
        '
        Me.pnlCPT.Controls.Add(Me.pnl_grdTemplateGallery)
        Me.pnlCPT.Controls.Add(Me.pnl_Grid)
        Me.pnlCPT.Controls.Add(Me.Splitter2)
        Me.pnlCPT.Controls.Add(Me.GloUC_trvCPT)
        Me.pnlCPT.Controls.Add(Me.pnl_trvCptAssocation)
        Me.pnlCPT.Controls.Add(Me.pnlSearch)
        Me.pnlCPT.Controls.Add(Me.pnl_RadioBtn)
        Me.pnlCPT.Controls.Add(Me.Panel1)
        Me.pnlCPT.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlCPT.Location = New System.Drawing.Point(0, 0)
        Me.pnlCPT.Name = "pnlCPT"
        Me.pnlCPT.Size = New System.Drawing.Size(240, 708)
        Me.pnlCPT.TabIndex = 1
        '
        'pnl_grdTemplateGallery
        '
        Me.pnl_grdTemplateGallery.Controls.Add(Me.Label27)
        Me.pnl_grdTemplateGallery.Controls.Add(Me.Label28)
        Me.pnl_grdTemplateGallery.Controls.Add(Me.Label29)
        Me.pnl_grdTemplateGallery.Controls.Add(Me.Label30)
        Me.pnl_grdTemplateGallery.Controls.Add(Me.grdTemplateGallery)
        Me.pnl_grdTemplateGallery.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnl_grdTemplateGallery.Location = New System.Drawing.Point(0, 423)
        Me.pnl_grdTemplateGallery.Name = "pnl_grdTemplateGallery"
        Me.pnl_grdTemplateGallery.Padding = New System.Windows.Forms.Padding(3, 0, 0, 3)
        Me.pnl_grdTemplateGallery.Size = New System.Drawing.Size(240, 285)
        Me.pnl_grdTemplateGallery.TabIndex = 19
        '
        'Label27
        '
        Me.Label27.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label27.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label27.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label27.Location = New System.Drawing.Point(4, 281)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(235, 1)
        Me.Label27.TabIndex = 12
        Me.Label27.Text = "label2"
        '
        'Label28
        '
        Me.Label28.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label28.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label28.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label28.Location = New System.Drawing.Point(3, 1)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(1, 281)
        Me.Label28.TabIndex = 11
        Me.Label28.Text = "label4"
        '
        'Label29
        '
        Me.Label29.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label29.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label29.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label29.Location = New System.Drawing.Point(239, 1)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(1, 281)
        Me.Label29.TabIndex = 10
        Me.Label29.Text = "label3"
        '
        'Label30
        '
        Me.Label30.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label30.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label30.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label30.Location = New System.Drawing.Point(3, 0)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(237, 1)
        Me.Label30.TabIndex = 9
        Me.Label30.Text = "label1"
        '
        'grdTemplateGallery
        '
        Me.grdTemplateGallery.AlternatingBackColor = System.Drawing.Color.FromArgb(CType(CType(214, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.grdTemplateGallery.BackColor = System.Drawing.Color.White
        Me.grdTemplateGallery.BackgroundColor = System.Drawing.Color.White
        Me.grdTemplateGallery.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.grdTemplateGallery.CaptionBackColor = System.Drawing.Color.FromArgb(CType(CType(4, Byte), Integer), CType(CType(96, Byte), Integer), CType(CType(162, Byte), Integer))
        Me.grdTemplateGallery.CaptionFont = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdTemplateGallery.CaptionForeColor = System.Drawing.Color.White
        Me.grdTemplateGallery.CaptionVisible = False
        Me.grdTemplateGallery.ContextMenu = Me.cntFormGallery
        Me.grdTemplateGallery.DataMember = ""
        Me.grdTemplateGallery.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdTemplateGallery.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdTemplateGallery.ForeColor = System.Drawing.Color.Black
        Me.grdTemplateGallery.FullRowSelect = True
        Me.grdTemplateGallery.GridLineColor = System.Drawing.Color.Black
        Me.grdTemplateGallery.HeaderBackColor = System.Drawing.Color.FromArgb(CType(CType(4, Byte), Integer), CType(CType(96, Byte), Integer), CType(CType(162, Byte), Integer))
        Me.grdTemplateGallery.HeaderForeColor = System.Drawing.Color.White
        Me.grdTemplateGallery.Location = New System.Drawing.Point(3, 0)
        Me.grdTemplateGallery.Name = "grdTemplateGallery"
        Me.grdTemplateGallery.ParentRowsBackColor = System.Drawing.Color.GhostWhite
        Me.grdTemplateGallery.ReadOnly = True
        Me.grdTemplateGallery.RowHeadersVisible = False
        Me.grdTemplateGallery.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.grdTemplateGallery.Size = New System.Drawing.Size(237, 282)
        Me.grdTemplateGallery.TabIndex = 5
        '
        'cntFormGallery
        '
        Me.cntFormGallery.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuRemoveForm})
        '
        'mnuRemoveForm
        '
        Me.mnuRemoveForm.Index = 0
        Me.mnuRemoveForm.Text = "Remove Template"
        '
        'pnl_Grid
        '
        Me.pnl_Grid.Controls.Add(Me.pnlGrid)
        Me.pnl_Grid.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnl_Grid.Location = New System.Drawing.Point(0, 395)
        Me.pnl_Grid.Name = "pnl_Grid"
        Me.pnl_Grid.Padding = New System.Windows.Forms.Padding(3, 1, 1, 3)
        Me.pnl_Grid.Size = New System.Drawing.Size(240, 28)
        Me.pnl_Grid.TabIndex = 19
        '
        'pnlGrid
        '
        Me.pnlGrid.BackColor = System.Drawing.Color.Transparent
        Me.pnlGrid.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.pnlGrid.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlGrid.Controls.Add(Me.Label45)
        Me.pnlGrid.Controls.Add(Me.Label46)
        Me.pnlGrid.Controls.Add(Me.Label47)
        Me.pnlGrid.Controls.Add(Me.Label48)
        Me.pnlGrid.Controls.Add(Me.Label3)
        Me.pnlGrid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlGrid.Location = New System.Drawing.Point(3, 1)
        Me.pnlGrid.Name = "pnlGrid"
        Me.pnlGrid.Size = New System.Drawing.Size(236, 24)
        Me.pnlGrid.TabIndex = 4
        '
        'Label45
        '
        Me.Label45.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label45.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label45.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label45.Location = New System.Drawing.Point(1, 23)
        Me.Label45.Name = "Label45"
        Me.Label45.Size = New System.Drawing.Size(234, 1)
        Me.Label45.TabIndex = 12
        Me.Label45.Text = "label2"
        '
        'Label46
        '
        Me.Label46.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label46.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label46.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label46.Location = New System.Drawing.Point(0, 1)
        Me.Label46.Name = "Label46"
        Me.Label46.Size = New System.Drawing.Size(1, 23)
        Me.Label46.TabIndex = 11
        Me.Label46.Text = "label4"
        '
        'Label47
        '
        Me.Label47.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label47.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label47.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label47.Location = New System.Drawing.Point(235, 1)
        Me.Label47.Name = "Label47"
        Me.Label47.Size = New System.Drawing.Size(1, 23)
        Me.Label47.TabIndex = 10
        Me.Label47.Text = "label3"
        '
        'Label48
        '
        Me.Label48.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label48.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label48.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label48.Location = New System.Drawing.Point(0, 0)
        Me.Label48.Name = "Label48"
        Me.Label48.Size = New System.Drawing.Size(236, 1)
        Me.Label48.TabIndex = 9
        Me.Label48.Text = "label1"
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Location = New System.Drawing.Point(0, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(236, 24)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "Templates"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Splitter2
        '
        Me.Splitter2.Cursor = System.Windows.Forms.Cursors.HSplit
        Me.Splitter2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Splitter2.Location = New System.Drawing.Point(0, 392)
        Me.Splitter2.Name = "Splitter2"
        Me.Splitter2.Size = New System.Drawing.Size(240, 3)
        Me.Splitter2.TabIndex = 21
        Me.Splitter2.TabStop = False
        '
        'GloUC_trvCPT
        '
        Me.GloUC_trvCPT.BackColor = System.Drawing.Color.Transparent
        Me.GloUC_trvCPT.CheckBoxes = False
        Me.GloUC_trvCPT.CodeMember = Nothing
        Me.GloUC_trvCPT.Comment = Nothing
        Me.GloUC_trvCPT.ConceptID = Nothing
        Me.GloUC_trvCPT.mpidmember = Nothing
        Me.GloUC_trvCPT.DescriptionMember = Nothing
        Me.GloUC_trvCPT.DisplayType = gloUserControlLibrary.gloUC_TreeView.enumDisplayType.Code_Description
        Me.GloUC_trvCPT.Dock = System.Windows.Forms.DockStyle.Top
        Me.GloUC_trvCPT.DrugFlag = CType(16, Short)
        Me.GloUC_trvCPT.DrugFormMember = Nothing
        Me.GloUC_trvCPT.DrugQtyQualifierMember = Nothing
        Me.GloUC_trvCPT.DurationMember = Nothing
        Me.GloUC_trvCPT.FrequencyMember = Nothing
        Me.GloUC_trvCPT.ImageIndex = 0
        Me.GloUC_trvCPT.ImageList = Me.ImgTreeView
        Me.GloUC_trvCPT.ImageObject = Nothing
        Me.GloUC_trvCPT.Indicator = Nothing
        Me.GloUC_trvCPT.IsDrug = False
        Me.GloUC_trvCPT.IsNarcoticsMember = Nothing
        Me.GloUC_trvCPT.IsSystemCategory = Nothing
        Me.GloUC_trvCPT.Location = New System.Drawing.Point(0, 60)
        Me.GloUC_trvCPT.MaximumNodes = 1000
        Me.GloUC_trvCPT.Name = "GloUC_trvCPT"
        Me.GloUC_trvCPT.NDCCodeMember = Nothing
        Me.GloUC_trvCPT.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.GloUC_trvCPT.ParentImageIndex = 0
        Me.GloUC_trvCPT.ParentMember = Nothing
        Me.GloUC_trvCPT.RouteMember = Nothing
        Me.GloUC_trvCPT.Search = gloUserControlLibrary.gloUC_TreeView.enumSearchType.Instring
        Me.GloUC_trvCPT.SearchBox = True
        Me.GloUC_trvCPT.SearchText = Nothing
        Me.GloUC_trvCPT.SelectedImageIndex = 0
        Me.GloUC_trvCPT.SelectedNode = Nothing
        Me.GloUC_trvCPT.SelectedNodeIDs = CType(resources.GetObject("GloUC_trvCPT.SelectedNodeIDs"), System.Collections.ArrayList)
        Me.GloUC_trvCPT.SelectedParentImageIndex = 0
        Me.GloUC_trvCPT.Size = New System.Drawing.Size(240, 332)
        Me.GloUC_trvCPT.Sort = gloUserControlLibrary.gloUC_TreeView.enumSortType.ByDescription
        Me.GloUC_trvCPT.TabIndex = 20
        Me.GloUC_trvCPT.Tag = Nothing
        Me.GloUC_trvCPT.UnitMember = Nothing
        Me.GloUC_trvCPT.ValueMember = Nothing
        '
        'ImgTreeView
        '
        Me.ImgTreeView.ImageStream = CType(resources.GetObject("ImgTreeView.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImgTreeView.TransparentColor = System.Drawing.Color.Transparent
        Me.ImgTreeView.Images.SetKeyName(0, "Bullet06.ico")
        Me.ImgTreeView.Images.SetKeyName(1, "CPT.ico")
        '
        'pnl_trvCptAssocation
        '
        Me.pnl_trvCptAssocation.Controls.Add(Me.trvCptAssocation)
        Me.pnl_trvCptAssocation.Controls.Add(Me.lstCPT)
        Me.pnl_trvCptAssocation.Controls.Add(Me.Label44)
        Me.pnl_trvCptAssocation.Controls.Add(Me.Label23)
        Me.pnl_trvCptAssocation.Controls.Add(Me.Label24)
        Me.pnl_trvCptAssocation.Controls.Add(Me.Label25)
        Me.pnl_trvCptAssocation.Controls.Add(Me.Label26)
        Me.pnl_trvCptAssocation.Location = New System.Drawing.Point(79, 162)
        Me.pnl_trvCptAssocation.Name = "pnl_trvCptAssocation"
        Me.pnl_trvCptAssocation.Padding = New System.Windows.Forms.Padding(3, 1, 1, 1)
        Me.pnl_trvCptAssocation.Size = New System.Drawing.Size(142, 136)
        Me.pnl_trvCptAssocation.TabIndex = 19
        Me.pnl_trvCptAssocation.Visible = False
        '
        'trvCptAssocation
        '
        Me.trvCptAssocation.BackColor = System.Drawing.Color.White
        Me.trvCptAssocation.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trvCptAssocation.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trvCptAssocation.ForeColor = System.Drawing.Color.Black
        Me.trvCptAssocation.HideSelection = False
        Me.trvCptAssocation.ImageIndex = 0
        Me.trvCptAssocation.ImageList = Me.ImgTreeView
        Me.trvCptAssocation.Indent = 21
        Me.trvCptAssocation.ItemHeight = 20
        Me.trvCptAssocation.Location = New System.Drawing.Point(4, 6)
        Me.trvCptAssocation.Name = "trvCptAssocation"
        Me.trvCptAssocation.SelectedImageIndex = 0
        Me.trvCptAssocation.ShowLines = False
        Me.trvCptAssocation.Size = New System.Drawing.Size(136, 114)
        Me.trvCptAssocation.TabIndex = 2
        '
        'lstCPT
        '
        Me.lstCPT.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lstCPT.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lstCPT.ForeColor = System.Drawing.Color.Black
        Me.lstCPT.ItemHeight = 14
        Me.lstCPT.Location = New System.Drawing.Point(4, 120)
        Me.lstCPT.Name = "lstCPT"
        Me.lstCPT.Size = New System.Drawing.Size(136, 14)
        Me.lstCPT.TabIndex = 1
        '
        'Label44
        '
        Me.Label44.BackColor = System.Drawing.Color.White
        Me.Label44.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label44.Location = New System.Drawing.Point(4, 2)
        Me.Label44.Name = "Label44"
        Me.Label44.Size = New System.Drawing.Size(136, 4)
        Me.Label44.TabIndex = 38
        '
        'Label23
        '
        Me.Label23.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label23.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label23.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label23.Location = New System.Drawing.Point(4, 134)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(136, 1)
        Me.Label23.TabIndex = 12
        Me.Label23.Text = "label2"
        '
        'Label24
        '
        Me.Label24.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label24.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label24.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.Location = New System.Drawing.Point(3, 2)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(1, 133)
        Me.Label24.TabIndex = 11
        Me.Label24.Text = "label4"
        '
        'Label25
        '
        Me.Label25.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label25.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label25.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label25.Location = New System.Drawing.Point(140, 2)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(1, 133)
        Me.Label25.TabIndex = 10
        Me.Label25.Text = "label3"
        '
        'Label26
        '
        Me.Label26.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label26.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label26.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.Location = New System.Drawing.Point(3, 1)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(138, 1)
        Me.Label26.TabIndex = 9
        Me.Label26.Text = "label1"
        '
        'pnlSearch
        '
        Me.pnlSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlSearch.Controls.Add(Me.txtSearch)
        Me.pnlSearch.Controls.Add(Me.Label20)
        Me.pnlSearch.Controls.Add(Me.Label21)
        Me.pnlSearch.Controls.Add(Me.PictureBox1)
        Me.pnlSearch.Controls.Add(Me.Label40)
        Me.pnlSearch.Controls.Add(Me.Label41)
        Me.pnlSearch.Controls.Add(Me.Label42)
        Me.pnlSearch.Controls.Add(Me.Label43)
        Me.pnlSearch.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlSearch.ForeColor = System.Drawing.Color.Black
        Me.pnlSearch.Location = New System.Drawing.Point(79, 128)
        Me.pnlSearch.Name = "pnlSearch"
        Me.pnlSearch.Padding = New System.Windows.Forms.Padding(3, 1, 1, 3)
        Me.pnlSearch.Size = New System.Drawing.Size(135, 28)
        Me.pnlSearch.TabIndex = 16
        Me.pnlSearch.Visible = False
        '
        'txtSearch
        '
        Me.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtSearch.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtSearch.ForeColor = System.Drawing.Color.Black
        Me.txtSearch.Location = New System.Drawing.Point(32, 6)
        Me.txtSearch.Multiline = True
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(101, 16)
        Me.txtSearch.TabIndex = 0
        '
        'Label20
        '
        Me.Label20.BackColor = System.Drawing.Color.White
        Me.Label20.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label20.Location = New System.Drawing.Point(32, 2)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(101, 4)
        Me.Label20.TabIndex = 37
        '
        'Label21
        '
        Me.Label21.BackColor = System.Drawing.Color.White
        Me.Label21.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label21.Location = New System.Drawing.Point(32, 22)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(101, 2)
        Me.Label21.TabIndex = 38
        '
        'PictureBox1
        '
        Me.PictureBox1.BackColor = System.Drawing.Color.White
        Me.PictureBox1.Dock = System.Windows.Forms.DockStyle.Left
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(4, 2)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(28, 22)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox1.TabIndex = 9
        Me.PictureBox1.TabStop = False
        '
        'Label40
        '
        Me.Label40.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label40.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label40.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label40.Location = New System.Drawing.Point(4, 24)
        Me.Label40.Name = "Label40"
        Me.Label40.Size = New System.Drawing.Size(129, 1)
        Me.Label40.TabIndex = 42
        Me.Label40.Text = "label2"
        '
        'Label41
        '
        Me.Label41.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label41.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label41.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label41.Location = New System.Drawing.Point(3, 2)
        Me.Label41.Name = "Label41"
        Me.Label41.Size = New System.Drawing.Size(1, 23)
        Me.Label41.TabIndex = 41
        Me.Label41.Text = "label4"
        '
        'Label42
        '
        Me.Label42.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label42.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label42.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label42.Location = New System.Drawing.Point(133, 2)
        Me.Label42.Name = "Label42"
        Me.Label42.Size = New System.Drawing.Size(1, 23)
        Me.Label42.TabIndex = 40
        Me.Label42.Text = "label3"
        '
        'Label43
        '
        Me.Label43.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label43.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label43.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label43.Location = New System.Drawing.Point(3, 1)
        Me.Label43.Name = "Label43"
        Me.Label43.Size = New System.Drawing.Size(131, 1)
        Me.Label43.TabIndex = 39
        Me.Label43.Text = "label1"
        '
        'pnl_RadioBtn
        '
        Me.pnl_RadioBtn.Controls.Add(Me.Panel8)
        Me.pnl_RadioBtn.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnl_RadioBtn.Location = New System.Drawing.Point(0, 30)
        Me.pnl_RadioBtn.Name = "pnl_RadioBtn"
        Me.pnl_RadioBtn.Padding = New System.Windows.Forms.Padding(3, 0, 0, 3)
        Me.pnl_RadioBtn.Size = New System.Drawing.Size(240, 30)
        Me.pnl_RadioBtn.TabIndex = 18
        Me.pnl_RadioBtn.Visible = False
        '
        'Panel8
        '
        Me.Panel8.BackColor = System.Drawing.Color.Transparent
        Me.Panel8.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.Panel8.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel8.Controls.Add(Me.rbSearch2)
        Me.Panel8.Controls.Add(Me.rbSearch1)
        Me.Panel8.Controls.Add(Me.Label14)
        Me.Panel8.Controls.Add(Me.Label15)
        Me.Panel8.Controls.Add(Me.Label16)
        Me.Panel8.Controls.Add(Me.Label22)
        Me.Panel8.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel8.Location = New System.Drawing.Point(3, 0)
        Me.Panel8.Name = "Panel8"
        Me.Panel8.Size = New System.Drawing.Size(237, 27)
        Me.Panel8.TabIndex = 7
        '
        'rbSearch2
        '
        Me.rbSearch2.BackColor = System.Drawing.Color.Transparent
        Me.rbSearch2.Checked = True
        Me.rbSearch2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbSearch2.Location = New System.Drawing.Point(139, 1)
        Me.rbSearch2.Name = "rbSearch2"
        Me.rbSearch2.Size = New System.Drawing.Size(96, 24)
        Me.rbSearch2.TabIndex = 3
        Me.rbSearch2.TabStop = True
        Me.rbSearch2.Text = "Description"
        Me.rbSearch2.UseVisualStyleBackColor = False
        Me.rbSearch2.Visible = False
        '
        'rbSearch1
        '
        Me.rbSearch1.BackColor = System.Drawing.Color.Transparent
        Me.rbSearch1.Location = New System.Drawing.Point(11, 1)
        Me.rbSearch1.Name = "rbSearch1"
        Me.rbSearch1.Size = New System.Drawing.Size(88, 24)
        Me.rbSearch1.TabIndex = 2
        Me.rbSearch1.Text = "CPT Code"
        Me.rbSearch1.UseVisualStyleBackColor = False
        Me.rbSearch1.Visible = False
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label14.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label14.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label14.Location = New System.Drawing.Point(1, 26)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(235, 1)
        Me.Label14.TabIndex = 12
        Me.Label14.Text = "label2"
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label15.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label15.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(0, 1)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(1, 26)
        Me.Label15.TabIndex = 11
        Me.Label15.Text = "label4"
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label16.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label16.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label16.Location = New System.Drawing.Point(236, 1)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(1, 26)
        Me.Label16.TabIndex = 10
        Me.Label16.Text = "label3"
        '
        'Label22
        '
        Me.Label22.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label22.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label22.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.Location = New System.Drawing.Point(0, 0)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(237, 1)
        Me.Label22.TabIndex = 9
        Me.Label22.Text = "label1"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Panel4)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Padding = New System.Windows.Forms.Padding(3, 3, 0, 3)
        Me.Panel1.Size = New System.Drawing.Size(240, 30)
        Me.Panel1.TabIndex = 17
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Transparent
        Me.Panel4.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.Panel4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel4.Controls.Add(Me.Label10)
        Me.Panel4.Controls.Add(Me.Label11)
        Me.Panel4.Controls.Add(Me.Label12)
        Me.Panel4.Controls.Add(Me.Label13)
        Me.Panel4.Controls.Add(Me.Label1)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Panel4.Location = New System.Drawing.Point(3, 3)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(237, 24)
        Me.Panel4.TabIndex = 6
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label10.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label10.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label10.Location = New System.Drawing.Point(1, 23)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(235, 1)
        Me.Label10.TabIndex = 12
        Me.Label10.Text = "label2"
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label11.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label11.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(0, 1)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(1, 23)
        Me.Label11.TabIndex = 11
        Me.Label11.Text = "label4"
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label12.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label12.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label12.Location = New System.Drawing.Point(236, 1)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(1, 23)
        Me.Label12.TabIndex = 10
        Me.Label12.Text = "label3"
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label13.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label13.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(0, 0)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(237, 1)
        Me.Label13.TabIndex = 9
        Me.Label13.Text = "label1"
        '
        'Label1
        '
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(0, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(237, 24)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "   CPT"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pnlTemplate
        '
        Me.pnlTemplate.Controls.Add(Me.lstTemplates)
        Me.pnlTemplate.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlTemplate.Location = New System.Drawing.Point(0, 0)
        Me.pnlTemplate.Name = "pnlTemplate"
        Me.pnlTemplate.Size = New System.Drawing.Size(240, 708)
        Me.pnlTemplate.TabIndex = 3
        '
        'lstTemplates
        '
        Me.lstTemplates.ItemHeight = 14
        Me.lstTemplates.Location = New System.Drawing.Point(0, 304)
        Me.lstTemplates.Name = "lstTemplates"
        Me.lstTemplates.Size = New System.Drawing.Size(238, 60)
        Me.lstTemplates.TabIndex = 2
        '
        'Splitter1
        '
        Me.Splitter1.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Splitter1.Location = New System.Drawing.Point(240, 54)
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(3, 708)
        Me.Splitter1.TabIndex = 3
        Me.Splitter1.TabStop = False
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "")
        Me.ImageList1.Images.SetKeyName(1, "")
        Me.ImageList1.Images.SetKeyName(2, "")
        Me.ImageList1.Images.SetKeyName(3, "")
        Me.ImageList1.Images.SetKeyName(4, "")
        Me.ImageList1.Images.SetKeyName(5, "")
        Me.ImageList1.Images.SetKeyName(6, "")
        Me.ImageList1.Images.SetKeyName(7, "")
        Me.ImageList1.Images.SetKeyName(8, "")
        Me.ImageList1.Images.SetKeyName(9, "")
        Me.ImageList1.Images.SetKeyName(10, "")
        Me.ImageList1.Images.SetKeyName(11, "")
        Me.ImageList1.Images.SetKeyName(12, "")
        Me.ImageList1.Images.SetKeyName(13, "")
        '
        'pnl_wdFormGallery
        '
        Me.pnl_wdFormGallery.BackColor = System.Drawing.Color.Transparent
        Me.pnl_wdFormGallery.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnl_wdFormGallery.Controls.Add(Me.Label36)
        Me.pnl_wdFormGallery.Controls.Add(Me.Label9)
        Me.pnl_wdFormGallery.Controls.Add(Me.Label17)
        Me.pnl_wdFormGallery.Controls.Add(Me.Label18)
        Me.pnl_wdFormGallery.Controls.Add(Me.wdFormGallery)
        Me.pnl_wdFormGallery.Controls.Add(Me.pnl_PatientHeader)
        Me.pnl_wdFormGallery.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnl_wdFormGallery.Location = New System.Drawing.Point(0, 30)
        Me.pnl_wdFormGallery.Name = "pnl_wdFormGallery"
        Me.pnl_wdFormGallery.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.pnl_wdFormGallery.Size = New System.Drawing.Size(941, 678)
        Me.pnl_wdFormGallery.TabIndex = 16
        '
        'Label36
        '
        Me.Label36.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label36.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label36.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label36.Location = New System.Drawing.Point(1, 0)
        Me.Label36.Name = "Label36"
        Me.Label36.Size = New System.Drawing.Size(936, 1)
        Me.Label36.TabIndex = 47
        Me.Label36.Text = "label2"
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label9.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label9.Location = New System.Drawing.Point(1, 674)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(936, 1)
        Me.Label9.TabIndex = 46
        Me.Label9.Text = "label2"
        '
        'Label17
        '
        Me.Label17.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label17.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label17.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.Location = New System.Drawing.Point(0, 0)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(1, 675)
        Me.Label17.TabIndex = 45
        Me.Label17.Text = "label4"
        '
        'Label18
        '
        Me.Label18.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label18.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label18.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label18.Location = New System.Drawing.Point(937, 0)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(1, 675)
        Me.Label18.TabIndex = 44
        Me.Label18.Text = "label3"
        '
        'wdFormGallery
        '
        Me.wdFormGallery.Dock = System.Windows.Forms.DockStyle.Fill
        Me.wdFormGallery.Enabled = True
        Me.wdFormGallery.Location = New System.Drawing.Point(0, 0)
        Me.wdFormGallery.Name = "wdFormGallery"
        Me.wdFormGallery.OcxState = CType(resources.GetObject("wdFormGallery.OcxState"), System.Windows.Forms.AxHost.State)
        Me.wdFormGallery.Size = New System.Drawing.Size(938, 675)
        Me.wdFormGallery.TabIndex = 42
        '
        'pnl_PatientHeader
        '
        Me.pnl_PatientHeader.Controls.Add(Me.pnlPatientHeader)
        Me.pnl_PatientHeader.Location = New System.Drawing.Point(53, 12)
        Me.pnl_PatientHeader.Name = "pnl_PatientHeader"
        Me.pnl_PatientHeader.Padding = New System.Windows.Forms.Padding(1, 3, 3, 3)
        Me.pnl_PatientHeader.Size = New System.Drawing.Size(434, 30)
        Me.pnl_PatientHeader.TabIndex = 44
        Me.pnl_PatientHeader.Visible = False
        '
        'pnlPatientHeader
        '
        Me.pnlPatientHeader.BackColor = System.Drawing.Color.Transparent
        Me.pnlPatientHeader.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.pnlPatientHeader.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlPatientHeader.Controls.Add(Me.Label2)
        Me.pnlPatientHeader.Controls.Add(Me.txtTemplateName)
        Me.pnlPatientHeader.Controls.Add(Me.Label4)
        Me.pnlPatientHeader.Controls.Add(Me.lblVisitDate)
        Me.pnlPatientHeader.Controls.Add(Me.Label5)
        Me.pnlPatientHeader.Controls.Add(Me.Label6)
        Me.pnlPatientHeader.Controls.Add(Me.Label7)
        Me.pnlPatientHeader.Controls.Add(Me.Label8)
        Me.pnlPatientHeader.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlPatientHeader.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlPatientHeader.Location = New System.Drawing.Point(1, 3)
        Me.pnlPatientHeader.Name = "pnlPatientHeader"
        Me.pnlPatientHeader.Size = New System.Drawing.Size(430, 24)
        Me.pnlPatientHeader.TabIndex = 43
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(1, 1)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(780, 22)
        Me.Label2.TabIndex = 30
        Me.Label2.Text = "  Template Name "
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtTemplateName
        '
        Me.txtTemplateName.AutoSize = True
        Me.txtTemplateName.BackColor = System.Drawing.Color.Transparent
        Me.txtTemplateName.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTemplateName.Location = New System.Drawing.Point(127, 7)
        Me.txtTemplateName.Name = "txtTemplateName"
        Me.txtTemplateName.Size = New System.Drawing.Size(0, 15)
        Me.txtTemplateName.TabIndex = 29
        Me.txtTemplateName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(488, 7)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(76, 14)
        Me.Label4.TabIndex = 28
        Me.Label4.Text = "Visit Date :"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.Label4.Visible = False
        '
        'lblVisitDate
        '
        Me.lblVisitDate.AutoSize = True
        Me.lblVisitDate.BackColor = System.Drawing.Color.Transparent
        Me.lblVisitDate.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVisitDate.Location = New System.Drawing.Point(568, 7)
        Me.lblVisitDate.Name = "lblVisitDate"
        Me.lblVisitDate.Size = New System.Drawing.Size(0, 14)
        Me.lblVisitDate.TabIndex = 19
        Me.lblVisitDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblVisitDate.Visible = False
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label5.Location = New System.Drawing.Point(1, 23)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(428, 1)
        Me.Label5.TabIndex = 34
        Me.Label5.Text = "label2"
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(0, 1)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(1, 23)
        Me.Label6.TabIndex = 33
        Me.Label6.Text = "label4"
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label7.Location = New System.Drawing.Point(429, 1)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(1, 23)
        Me.Label7.TabIndex = 32
        Me.Label7.Text = "label3"
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(0, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(430, 1)
        Me.Label8.TabIndex = 31
        Me.Label8.Text = "label1"
        '
        'pnlToolstripContainer
        '
        Me.pnlToolstripContainer.Controls.Add(Me.Label19)
        Me.pnlToolstripContainer.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlToolstripContainer.Location = New System.Drawing.Point(0, 0)
        Me.pnlToolstripContainer.Name = "pnlToolstripContainer"
        Me.pnlToolstripContainer.Padding = New System.Windows.Forms.Padding(1, 3, 3, 3)
        Me.pnlToolstripContainer.Size = New System.Drawing.Size(1184, 54)
        Me.pnlToolstripContainer.TabIndex = 47
        '
        'Label19
        '
        Me.Label19.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label19.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label19.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.Location = New System.Drawing.Point(1, 3)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(1180, 48)
        Me.Label19.TabIndex = 1
        Me.Label19.Text = "WordToolStrip Panel"
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.Label19.Visible = False
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.Panel3)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Padding = New System.Windows.Forms.Padding(0, 3, 3, 3)
        Me.Panel2.Size = New System.Drawing.Size(941, 30)
        Me.Panel2.TabIndex = 48
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Transparent
        Me.Panel3.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.Panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel3.Controls.Add(Me.GloUC_AddRefreshDic1)
        Me.Panel3.Controls.Add(Me.Label31)
        Me.Panel3.Controls.Add(Me.Label32)
        Me.Panel3.Controls.Add(Me.Label33)
        Me.Panel3.Controls.Add(Me.Label34)
        Me.Panel3.Controls.Add(Me.Label35)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Panel3.Location = New System.Drawing.Point(0, 3)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(938, 24)
        Me.Panel3.TabIndex = 6
        '
        'GloUC_AddRefreshDic1
        '
        Me.GloUC_AddRefreshDic1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GloUC_AddRefreshDic1.BackColor = System.Drawing.Color.Transparent
        Me.GloUC_AddRefreshDic1.CONNECTIONSTRINGs = Nothing
        Me.GloUC_AddRefreshDic1.DTLETTERDATEs = Nothing
        Me.GloUC_AddRefreshDic1.Location = New System.Drawing.Point(873, 3)
        Me.GloUC_AddRefreshDic1.M_PATIENTIDs = CType(0, Long)
        Me.GloUC_AddRefreshDic1.Name = "GloUC_AddRefreshDic1"
        Me.GloUC_AddRefreshDic1.OBJCRITERIAs = Nothing
        Me.GloUC_AddRefreshDic1.ObjFrom = Nothing
        Me.GloUC_AddRefreshDic1.OBJWORDs = Nothing
        Me.GloUC_AddRefreshDic1.OCURDOCs = Nothing
        Me.GloUC_AddRefreshDic1.OWORDAPPs = Nothing
        Me.GloUC_AddRefreshDic1.Size = New System.Drawing.Size(48, 19)
        Me.GloUC_AddRefreshDic1.TabIndex = 13
        Me.GloUC_AddRefreshDic1.Visible = False
        Me.GloUC_AddRefreshDic1.wdPatientWordDocs = Nothing
        '
        'Label31
        '
        Me.Label31.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label31.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label31.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label31.Location = New System.Drawing.Point(1, 23)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(936, 1)
        Me.Label31.TabIndex = 12
        Me.Label31.Text = "label2"
        '
        'Label32
        '
        Me.Label32.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label32.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label32.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label32.Location = New System.Drawing.Point(0, 1)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(1, 23)
        Me.Label32.TabIndex = 11
        Me.Label32.Text = "label4"
        '
        'Label33
        '
        Me.Label33.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label33.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label33.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label33.Location = New System.Drawing.Point(937, 1)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(1, 23)
        Me.Label33.TabIndex = 10
        Me.Label33.Text = "label3"
        '
        'Label34
        '
        Me.Label34.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label34.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label34.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label34.Location = New System.Drawing.Point(0, 0)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(938, 1)
        Me.Label34.TabIndex = 9
        Me.Label34.Text = "label1"
        '
        'Label35
        '
        Me.Label35.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label35.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label35.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label35.Location = New System.Drawing.Point(0, 0)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(938, 24)
        Me.Label35.TabIndex = 0
        Me.Label35.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pnlMain
        '
        Me.pnlMain.Controls.Add(Me.pnl_wdFormGallery)
        Me.pnlMain.Controls.Add(Me.Panel2)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Location = New System.Drawing.Point(243, 54)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(941, 708)
        Me.pnlMain.TabIndex = 49
        '
        'frmCPTTemplate
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1184, 762)
        Me.Controls.Add(Me.pnlMain)
        Me.Controls.Add(Me.Splitter1)
        Me.Controls.Add(Me.pnl_Left)
        Me.Controls.Add(Me.pnlToolstripContainer)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmCPTTemplate"
        Me.ShowInTaskbar = False
        Me.Text = "Form Gallery"
        Me.pnl_Left.ResumeLayout(False)
        Me.pnlCPT.ResumeLayout(False)
        Me.pnl_grdTemplateGallery.ResumeLayout(False)
        CType(Me.grdTemplateGallery, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnl_Grid.ResumeLayout(False)
        Me.pnlGrid.ResumeLayout(False)
        Me.pnl_trvCptAssocation.ResumeLayout(False)
        Me.pnlSearch.ResumeLayout(False)
        Me.pnlSearch.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnl_RadioBtn.ResumeLayout(False)
        Me.Panel8.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.pnlTemplate.ResumeLayout(False)
        Me.pnl_wdFormGallery.ResumeLayout(False)
        CType(Me.wdFormGallery, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnl_PatientHeader.ResumeLayout(False)
        Me.pnlPatientHeader.ResumeLayout(False)
        Me.pnlPatientHeader.PerformLayout()
        Me.pnlToolstripContainer.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.pnlMain.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    'code commented by supriya on 13/11/2006
    'Private m_hotKeys As HotKeyCollection

    'Public Event HotKeyPressed As HotKeyPressedEventHandler
    'Public Event PrintWindowPressed As PrintWindowPressedEventHandler
    'Public Event PrintDesktopPressed As PrintDesktopPressedEventHandler

    'Public ReadOnly Property HotKeys() As HotKeyCollection
    '    Get
    '        HotKeys = m_hotKeys
    '    End Get
    'End Property

    'Public Sub RestoreAndActivate()
    '    If Not (UnmanagedMethods.IsWindowVisible(Me.Handle)) Then
    '        UnmanagedMethods.ShowWindow(Me.Handle, UnmanagedMethods.SW_SHOW)
    '    End If
    '    If (UnmanagedMethods.IsIconic(Me.Handle)) Then
    '        UnmanagedMethods.SendMessage(Me.Handle, UnmanagedMethods.WM_SYSCOMMAND, _
    '            UnmanagedMethods.SC_RESTORE, IntPtr.Zero)
    '    End If
    '    UnmanagedMethods.SetForegroundWindow(Me.Handle)
    'End Sub
    'code commented by supriya on 13/11/2006
    Protected Overrides Sub OnClosed(ByVal e As System.EventArgs)
        'HotKeys.Clear()
        MyBase.OnClosed(e)
    End Sub

    Private Sub frmCPTTemplate_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Try
            CType(Me.ParentForm, MainMenu).SetGnPatientID = GetCurrentPatientID
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Formgallery, gloAuditTrail.ActivityCategory.TemplateAllocation, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        Finally
            If Me.ParentForm IsNot Nothing Then
                CType(Me.ParentForm, MainMenu).RegisterMyHotKey()
                CType(Me.ParentForm, MainMenu).ActiveDSO = wdFormGallery
            End If
        End Try
        If gblnVoiceEnabled = True AndAlso gblnSpeakerExists = True Then
            If Not IsNothing(ogloVoice) Then
                ogloVoice.ShowMicroPhone()
            End If
        End If


        'Developer: Yatin N.Bhagat
        'Date:12/26/2011
        'Bug ID/PRD Name/Salesforce Case:Bug No. 17246:Patient Consent >> Checkbox in the template are not working once you finish exam
        'Reason: Handler For DDLCBEvent is Not Added while activating the form
        If Not (IsNothing(wdFormGallery)) Then
            If Not (IsNothing(wdFormGallery.DocumentName)) Then
                If Not (IsNothing(wdFormGallery.ActiveDocument)) Then
                    oCurDoc = wdFormGallery.ActiveDocument
                    oWordApp = oCurDoc.Application
                    Try
                        RemoveHandler oWordApp.WindowSelectionChange, AddressOf DDLCBEvent
                    Catch ex As Exception
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                        ex = Nothing
                    End Try

                    Try
                        AddHandler oWordApp.Application.WindowSelectionChange, AddressOf DDLCBEvent
                    Catch ex As Exception
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                        ex = Nothing
                    End Try

                    isHandlerRemoved = False
                    oCurDoc.FormFields.Shaded = False
                    oCurDoc.ActiveWindow.SetFocus()
                    wdFormGallery.Focus()
                End If
            End If
        End If

        Me.WindowState = FormWindowState.Maximized
    End Sub

    Private Sub frmCPTTemplate_Deactivate(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Deactivate
        If gblnVoiceEnabled = True AndAlso gblnSpeakerExists = True Then
            If Not IsNothing(ogloVoice) Then
                ogloVoice.TurnoffMicrophone()
            End If
        End If


        'Developer: Yatin N.Bhagat
        'Date:12/26/2011
        'Bug ID/PRD Name/Salesforce Case:Bug No. 17246:Patient Consent >> Checkbox in the template are not working once you finish exam
        'Reason: Handler For DDLCBEvent is Not Added while activating the form
        Try
            If Not oWordApp Is Nothing Then
                RemoveHandler oWordApp.WindowSelectionChange, AddressOf DDLCBEvent
                isHandlerRemoved = True
                gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Close, "RemoveHandler for WindowSelectionChange,WindowBeforeDoubleClick  for oWordApp", gloAuditTrail.ActivityOutCome.Success)
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try


        'Developer: Yatin N.Bhagat
        'Date:12/26/2011
        'Bug ID/PRD Name/Salesforce Case:Bug No. 18131:View >> Form Gallery >> Application is showing an exception when we click on Save&cls
        'Reason: 

        'Me.WindowState = FormWindowState.Minimized
    End Sub

    Private Sub frmCPTTemplate_FormClosed(sender As Object, e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        If (IsNothing(Me.ParentForm) = False) Then
            CType(Me.ParentForm, MainMenu).ActiveDSO = Nothing
        End If
        Try
            If (IsNothing(_PatientStrip) = False) Then
                _PatientStrip.Dispose()
                _PatientStrip = Nothing
            End If
        Catch ex As Exception

        End Try

        If (IsNothing(mdlFAX.Owner) = False) Then
            mdlFAX.Owner = Nothing
        End If

    End Sub

    'Private Sub frmCPTTemplate_FormClosed(sender As Object, e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
    '    ArrLst.Clear()

    'End Sub

    Private Sub frmCPTTemplate_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If IsNothing(oCurDoc) = False Then
            If oCurDoc.Saved = False Then
                Dim Result As DialogResult
                Result = MessageBox.Show("Do you want to save the changes to Form Gallery?", gstrMessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                If Result = Windows.Forms.DialogResult.Yes Then
                    Call SaveFormGallery(True)
                    e.Cancel = False
                ElseIf Result = Windows.Forms.DialogResult.Cancel Then
                    'line added by dipak 20091102 to fix 4828:form get closed when we click on cancel button
                    e.Cancel = True
                ElseIf Result = Windows.Forms.DialogResult.No Then
                    ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Formgallery, gloAuditTrail.ActivityCategory.TemplateAllocation, gloAuditTrail.ActivityType.View, "Form Gallery viewed", m_PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success)
                    ''Added Rahul P on 20101011
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Formgallery, gloAuditTrail.ActivityCategory.TemplateAllocation, gloAuditTrail.ActivityType.View, "Form Gallery viewed", m_PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                    ''
                    e.Cancel = False
                End If
            Else
                e.Cancel = False
            End If
        Else
            e.Cancel = False
        End If
        If gblnVoiceEnabled = True AndAlso gblnSpeakerExists = True Then
            If Not IsNothing(ogloVoice) Then
                TurnoffMicrophone()
                ogloVoice.UnInitializeVoiceComponents()
            End If
        End If

        If hashNewTemplates IsNot Nothing Then
            hashNewTemplates.Clear()
            hashNewTemplates = Nothing
        End If

        If hashExistingTemplates IsNot Nothing Then
            hashExistingTemplates.Clear()
            hashExistingTemplates = Nothing
        End If
    End Sub


    Private Sub frmCPTTemplate_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try

            lblVisitDate.Text = Format(Now, "MM/dd/yyyy") & Space(1) & Format(Now, "Short Time")
            loadPatientStrip()


            If m_VisitID = 0 Then
                m_VisitID = GenerateVisitID(m_PatientID)
            End If

            Call EditForm()

            If gblnVoiceEnabled = True AndAlso gblnSpeakerExists = True Then
                InitializeVoiceObject()
                ShowMicroPhone()
            End If

            Try
                gloPatient.gloPatient.GetWindowTitle(Me, m_PatientID, GetConnectionString(), gstrMessageBoxCaption)
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Formgallery, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                ex = Nothing
            End Try

            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Formgallery, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Open, "Form Gallery Opened", m_PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)


        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.TemplateAllocation, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            ex = Nothing
        End Try

        calltoAddRefreshButtonControl()

    End Sub
    ''Integrated ON 20101020 BY MAYURI FOR SIGNATURE
    Private Function AddChildMenu() As DataTable
        Try
            Dim oProvider As New clsProvider
            Dim rslt As Boolean
            rslt = oProvider.CheckSignDelegateStatus()
            If rslt Then
                Dim dt As New DataTable
                dt = oProvider.GetAllAssignProviders(gnLoginID)
                oProvider.Dispose()
                oProvider = Nothing
                If dt.Rows.Count > 0 Then
                    Return dt
                Else
                    Return Nothing
                End If
            Else
                oProvider.Dispose()
                oProvider = Nothing
                Return Nothing
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            ex = Nothing
            Return Nothing
        End Try
    End Function


    Private Sub EditForm()

        Dim dt As DataTable

        FillCPT()
        GridFormat()

        lblVisitDate.Text = Format(m_VisitDate, "MM/dd/yyyy") & Space(1) & Format(m_VisitDate, "Short Time")


        If (IsNothing(objclsCPTAssociation)) Then
            objclsCPTAssociation = New clsCPTAssociation
        End If
        dt = objclsCPTAssociation.SelectFormGallery(m_PatientID, m_VisitID)

        Dim r As DataRow

        Dim i As Integer
        Dim selIndex As Integer = -1

        If hashExistingTemplates Is Nothing Then
            hashExistingTemplates = New HashSet(Of Long)
        End If

        If IsNothing(dt) = False Then
            For i = 0 To dt.Rows.Count - 1
                r = tblTemplate.NewRow()

                'Specify the  col name to add value for the row
                ''''' Refer Sub GridFormat()
                ' GridFormat()
                r("CPTID") = dt.Rows(i)(0)   '--0 CPT ID
                r("CPT") = dt.Rows(i)(1)   '--1 CPT Name
                r("TemplateID") = dt.Rows(i)(2)  '--2 Template ID
                r("Template") = dt.Rows(i)(3)  '--3 Template Name
                r("FormID") = dt.Rows(i)(5)  '--3 FormID
                ''''' dt.Rows(0)(4) '' Template Image

                tblTemplate.Rows.Add(r)

                If Not hashExistingTemplates.Contains(Convert.ToInt64(dt.Rows(i)(5))) Then
                    hashExistingTemplates.Add(Convert.ToInt64(dt.Rows(i)(5)))
                End If

                Dim lst As New myList
                lst.ID = dt.Rows(i)(0) '' CPTID
                lst.Index = dt.Rows(i)(2) '' TemplateID
                lst.Description = dt.Rows(i)(3) ''''' TEmplate Name
                lst.TemplateResult = dt.Rows(i)(4) '' Template Image
                lst.FormID = dt.Rows(i)(5) '' FormID

                ArrLst.Add(lst)

                If dt.Rows(i)(0) = m_CPTID AndAlso dt.Rows(i)(2) = m_TemplateID Then
                    txtTemplateName.Text = dt.Rows(i)(3)
                End If

                If (lst.FormID = m_FormID) Then
                    selIndex = i
                End If
            Next
            dt.Dispose()
            dt = Nothing
        End If

        grdTemplateGallery.DataSource = tblTemplate
        CustomGridStyle()

        If (selIndex >= 0) Then
            grdTemplateGallery.CurrentRowIndex = selIndex
            grdTemplateGallery.Select(selIndex)
        End If

        ''''''''' +++++++++++++++++++++++

        'dt = New DataTable
        If (IsNothing(objclsCPTAssociation) = False) Then
            objclsCPTAssociation.Dispose()
            objclsCPTAssociation = Nothing
        End If
        objclsCPTAssociation = New clsCPTAssociation
        dt = objclsCPTAssociation.SelectForm(m_PatientID, m_VisitID, m_CPTID, m_TemplateID, m_FormID)

        '''''''' Code for Display Priview Template in Wd Object

        If Not IsNothing(dt) Then
            'Check if there are records for selected Node
            If dt.Rows.Count > 0 Then
                If IsDBNull(dt.Rows(0)(0)) = False Then
                    CPTID = m_CPTID
                    TemplateID = m_TemplateID
                    ObjWord = New clsWordDocument
                    Dim strFileName As String
                    strFileName = ExamNewDocumentName
                    strFileName = ObjWord.GenerateFile(dt.Rows(0)(0), strFileName)
                    ObjWord = Nothing
                    LoadWordUserControl(strFileName, False)
                    'Set the Start postion of the cursor in documents
                    oCurDoc.Application.Selection.HomeKey(Wd.WdUnits.wdStory)

                End If
            End If
        End If
        ''''''''''
    End Sub

    Private Sub FillCPT()


        If (IsNothing(objclsCPTAssociation) = False) Then
            objclsCPTAssociation.Dispose()
            objclsCPTAssociation = Nothing
        End If
        objclsCPTAssociation = New clsCPTAssociation

        If (IsNothing(dtCPTdesc) = False) Then
            dtCPTdesc.Dispose()
            dtCPTdesc = Nothing
        End If
        dtCPTdesc = objclsCPTAssociation.GetCPTTemplatesAssociation(1)


        If Not IsNothing(dtCPTdesc) Then
            GloUC_trvCPT.Clear()
            GloUC_trvCPT.DataSource = dtCPTdesc
            GloUC_trvCPT.ParentMember = "CodenDesc"
            GloUC_trvCPT.ValueMember = Convert.ToString(dtCPTdesc.Columns("TemplateID").ColumnName)  ''TO STORE TEMPLATE ID

            GloUC_trvCPT.DescriptionMember = Convert.ToString(dtCPTdesc.Columns("TemplateName").ColumnName)
            GloUC_trvCPT.CodeMember = Convert.ToString(dtCPTdesc.Columns("CPTCode").ColumnName)
            GloUC_trvCPT.DisplayType = gloUC_TreeView.enumDisplayType.Descripation

            GloUC_trvCPT.ParentImageIndex = 1
            GloUC_trvCPT.SelectedParentImageIndex = 1
            GloUC_trvCPT.Tag = Convert.ToString(dtCPTdesc.Columns("CPTID").ColumnName)  ''TO STORE CPTID
            GloUC_trvCPT.FillTreeView()
        End If

    End Sub

    Private Sub GridFormat()

        Dim colCPTID As New DataColumn
        Dim colCPTName As New DataColumn

        Dim colTemplateID As New DataColumn
        Dim colTemplateName As New DataColumn
        Dim colFormID As New DataColumn
        '' Dim colTemplate As New DataColumn

        colCPTID.ColumnName = "CPTID"
        colCPTName.ColumnName = "CPT"
        colTemplateID.ColumnName = "TemplateID"
        colTemplateName.ColumnName = "Template"
        colFormID.ColumnName = "FormID"
        '' colTemplate.ColumnName = "Template"

        tblTemplate.Columns.AddRange(New DataColumn() {colCPTID, colCPTName, colTemplateID, colTemplateName, colFormID})

    End Sub

    Public Sub CustomGridStyle()
        'Dim ts As New DataGridTableStyle

        'ts.ReadOnly = True
        'ts.AlternatingBackColor = System.Drawing.Color.Gainsboro
        'ts.BackColor = System.Drawing.Color.WhiteSmoke
        'ts.MappingName = tblTemplate.TableName.ToString
        'ts.HeaderBackColor = System.Drawing.Color.DimGray
        'ts.HeaderFont = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        'ts.ReadOnly = False
        'ts.RowHeadersVisible = False

        Dim ts As New clsDataGridTableStyle(tblTemplate.TableName)


        Dim colCPTID As New DataGridTextBoxColumn
        With colCPTID
            .Width = 0
            .MappingName = tblTemplate.Columns(0).ColumnName
            .HeaderText = "CPTID"
            .NullText = ""
        End With

        Dim colCPTName As New DataGridTextBoxColumn
        With colCPTName
            .Width = pnlGrid.Width / 2
            .MappingName = tblTemplate.Columns(1).ColumnName
            .HeaderText = "CPT"
            .NullText = ""
        End With


        Dim colTempalteID As New DataGridTextBoxColumn
        With colTempalteID
            .Width = 0
            .MappingName = tblTemplate.Columns(2).ColumnName
            .HeaderText = "TempalteID"
            .NullText = ""
        End With

        Dim colTemplateName As New DataGridTextBoxColumn
        With colTemplateName
            .Width = pnlGrid.Width / 2
            .MappingName = tblTemplate.Columns(3).ColumnName
            .HeaderText = "Template"
            .NullText = ""
        End With

        Dim colFormID As New DataGridTextBoxColumn
        With colFormID
            .Width = 0
            .MappingName = tblTemplate.Columns(4).ColumnName
            .HeaderText = "FormID"
            .NullText = ""
        End With

        ''Dim colFormat As New DataGridTextBoxColumn
        ''With colFormat
        ''    .Width = 125
        ''    .MappingName = tblTemplate.Columns(2).ColumnName
        ''    .HeaderText = "Template"
        ''    .NullText = ""
        ''End With


        ts.GridColumnStyles.AddRange(New DataGridColumnStyle() {colCPTID, colCPTName, colTempalteID, colTemplateName, colFormID})
        grdTemplateGallery.TableStyles.Clear()
        grdTemplateGallery.TableStyles.Add(ts)

    End Sub

    Private Sub trvCptAssocation_AfterSelect(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles trvCptAssocation.AfterSelect
        ''*********************************
        'trvCPT()
        ''*********************************

        Try
            If IsNothing(trvCptAssocation.SelectedNode) Then
                Exit Sub
            End If
            If IsNothing(trvCptAssocation.SelectedNode.Parent) Then
                Dim dtTemplate As DataTable
                Dim j As Integer
                If (IsNothing(objclsCPTAssociation)) Then
                    objclsCPTAssociation = New clsCPTAssociation
                End If
                dtTemplate = objclsCPTAssociation.GetAccociatedTemplates(trvCptAssocation.SelectedNode.Tag)
                For j = 0 To trvCptAssocation.GetNodeCount(False) - 1
                    trvCptAssocation.Nodes(j).Nodes.Clear()
                Next
                If (IsNothing(dtTemplate) = False) Then


                    'trvCptAssocation.SelectedNode.Nodes.Clear()
                    For j = 0 To dtTemplate.Rows.Count - 1
                        Dim TemplateNode As New TreeNode
                        TemplateNode.Tag = CType(dtTemplate.Rows(j)(0).ToString, Long)
                        TemplateNode.Text = CType(dtTemplate.Rows(j)(1).ToString, String)
                        trvCptAssocation.SelectedNode.Nodes.Add(TemplateNode)
                    Next
                    dtTemplate.Dispose()
                    dtTemplate = Nothing
                End If

                trvCptAssocation.SelectedNode.Expand()
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.TemplateAllocation, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            ex = Nothing
        End Try
    End Sub

    Private Sub trvCptAssocation_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles trvCptAssocation.DoubleClick
        Try
            If IsNothing(trvCptAssocation.SelectedNode) Then
                Exit Sub
            End If

            If IsNothing(trvCptAssocation.SelectedNode.Parent) Then
                Exit Sub
            End If

            Dim r As DataRow
            'Dim c As DataColumn

            r = tblTemplate.NewRow()

            ''''' Specify the  col name to add value for the row
            ''''' Refer Sub GridFormat()
            r("CPTID") = trvCptAssocation.SelectedNode.Parent.Tag   '--0
            r("CPT") = trvCptAssocation.SelectedNode.Parent.Text    '--1
            r("TemplateID") = trvCptAssocation.SelectedNode.Tag '--2
            r("Template") = trvCptAssocation.SelectedNode.Text  '--3

            Dim i As Integer
            ' Dim lst As New myList

            '''''  if there no rows in tblTemplate table then add Template to tblTemplate 
            If tblTemplate.Rows.Count = 0 Then
                tblTemplate.Rows.Add(r)
                grdTemplateGallery.DataSource = tblTemplate
                Exit Sub
            End If

            For i = 0 To tblTemplate.Rows.Count - 1
                ''''' check if Template For that CPT is already Exists 
                If tblTemplate.Rows(i)(0) = trvCptAssocation.SelectedNode.Parent.Tag AndAlso tblTemplate.Rows(i)(2) = trvCptAssocation.SelectedNode.Tag Then
                    ''''' if Template exists then exit
                    Exit Sub
                End If
            Next

            tblTemplate.Rows.Add(r)
            grdTemplateGallery.DataSource = tblTemplate

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.TemplateAllocation, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            ex = Nothing
        End Try
    End Sub

    Private Sub grdTemplateGallery_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grdTemplateGallery.MouseDown
        Try
            If grdTemplateGallery.CurrentRowIndex >= 0 Then
                If e.Button = Windows.Forms.MouseButtons.Right Then
                    'Try
                    '    If (IsNothing(grdTemplateGallery.ContextMenu) = False) Then
                    '        grdTemplateGallery.ContextMenu.Dispose()
                    '        grdTemplateGallery.ContextMenu = Nothing
                    '    End If
                    'Catch ex As Exception

                    'End Try
                    grdTemplateGallery.ContextMenu = cntFormGallery
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.TemplateAllocation, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Private Sub mnuRemoveForm_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuRemoveForm.Click
        Try
            If tblTemplate.Rows.Count > 0 Then
                Dim nCPTID As Long
                Dim nTemplateID As Long
                Dim i As Integer

                nCPTID = grdTemplateGallery.Item(grdTemplateGallery.CurrentRowIndex, 0)
                nTemplateID = grdTemplateGallery.Item(grdTemplateGallery.CurrentRowIndex, 2)

                For i = 0 To tblTemplate.Rows.Count - 1
                    ''''' check if Template For that CPT is already Exists 
                    If tblTemplate.Rows(i)(0) = nCPTID AndAlso tblTemplate.Rows(i)(2) = nTemplateID Then
                        ''''' if Template exists then exit
                        tblTemplate.Rows.RemoveAt(i)
                        Exit For
                    End If
                Next
                grdTemplateGallery.DataSource = tblTemplate


                If ArrLst.Count > 0 Then
                    For i = 0 To ArrLst.Count - 1
                        Dim lst As myList
                        lst = CType(ArrLst(i), myList)
                        If lst.ID = nCPTID AndAlso lst.Index = nTemplateID Then
                            ArrLst.RemoveAt(i)
                            Exit For
                        End If
                    Next
                End If

                If nCPTID = CPTID AndAlso nTemplateID = TemplateID Then
                    oCurDoc = Nothing
                    wdFormGallery.Close()
                    CPTID = 0
                    TemplateID = 0
                End If
            End If


        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.TemplateAllocation, gloAuditTrail.ActivityType.Remove, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            ex = Nothing
        End Try

    End Sub

    Private Sub grdTemplateGallery_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grdTemplateGallery.MouseUp
        If grdTemplateGallery.CurrentRowIndex >= 0 Then
            grdTemplateGallery.Select(grdTemplateGallery.CurrentRowIndex)
        End If
    End Sub

    Private Sub grdTemplateGallery_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdTemplateGallery.DoubleClick
        Try



            '******Shweta 20090828 *********'
            'To check exeception related to word
            If CheckWordForException() = False Then
                Exit Sub
            End If
            'End Shweta
            If (IsNothing(objclsCPTAssociation) = False) Then
                objclsCPTAssociation.Dispose()
                objclsCPTAssociation = Nothing
            End If
            objclsCPTAssociation = New clsCPTAssociation
            Dim strFileName As String
            Dim objWord As clsWordDocument
            'Dim objCriteria As DocCriteria
            Dim i As Integer

            wdFormGallery.Focus()

            '' Confirmation message while switching between forms 
            If Not IsNothing(oCurDoc) Then
                Dim newSeletedFormID As Long = grdTemplateGallery.Item(grdTemplateGallery.CurrentRowIndex, 4)
                If m_FormID <> newSeletedFormID Then

                    Dim Result As DialogResult
                    Result = MessageBox.Show("Do you want to save the changes to Form Gallery?", gstrMessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                    If Result = Windows.Forms.DialogResult.Yes Then
                        oCurDoc.Save()
                        Call SaveFormGallery()
                    ElseIf Result = Windows.Forms.DialogResult.No Then
                    ElseIf Result = Windows.Forms.DialogResult.Cancel Then
                        wdFormGallery.Focus()
                        oCurDoc = wdFormGallery.ActiveDocument
                        Exit Sub
                    End If
                Else
                    Exit Sub
                End If
            End If

            'Commented as not in used for Printing 
            ' ''''' 
            'If IsNothing(oCurDoc) = False Then
            '    strFileName = ExamNewDocumentName '' Application.StartupPath & "\Temp\Temp9.doc"

            '    wdFormGallery.Save(strFileName, True, "", "")
            '    '''''wdNewExam.Close()
            '    '''''Check if Visit Id is selected or not
            '    oCurDoc = Nothing
            '    wdFormGallery.Close()

            '    Dim lst As New myList
            '    lst.ID = CPTID
            '    lst.Index = TemplateID
            '    objWord = New clsWordDocument

            '    lst.TemplateResult = CType(objWord.ConvertFiletoBinary(strFileName), Object)
            '    objWord = Nothing
            '    '' Template Name
            '    lst.Description = grdTemplateGallery.Item(grdTemplateGallery.CurrentRowIndex, 3)

            '    If ArrLst.Count > 0 Then
            '        For i = 0 To ArrLst.Count - 1
            '            If CType(ArrLst(i), myList).ID = CPTID And CType(ArrLst(i), myList).Index = TemplateID Then
            '                CType(ArrLst(i), myList).TemplateResult = lst.TemplateResult
            '                blnFormExist = True
            '                Exit For
            '            Else
            '                blnFormExist = False
            '            End If
            '        Next
            '        If blnFormExist = False Then
            '            ArrLst.Add(lst)
            '            'blnFormExist = True
            '        End If
            '    Else
            '        ArrLst.Add(lst)
            '        'blnFormExist = True
            '    End If
            '    ''objclsCPTAssociation.SaveTempFormGallery(lblPatientCode.Tag , grdTemplateGallery.Item(grdTemplateGallery.CurrentRowIndex, 0), grdTemplateGallery.Item(grdTemplateGallery.CurrentRowIndex, 2), strFileName)
            'End If

            ' ''''' Code for Priview Template in Wd Object
            'Commented as not in used

            If grdTemplateGallery.VisibleRowCount >= 1 Then
                If (IsNothing(objclsCPTAssociation) = False) Then
                    objclsCPTAssociation.Dispose()
                    objclsCPTAssociation = Nothing
                End If
                objclsCPTAssociation = New clsCPTAssociation
                Dim dt As DataTable = Nothing

                ' ''===================
                'If ArrLst.Count > 0 Then
                '    For i = 0 To ArrLst.Count - 1
                '        If CType(ArrLst(i), myList).ID = grdTemplateGallery.Item(grdTemplateGallery.CurrentRowIndex, 0) And CType(ArrLst(i), myList).Index = grdTemplateGallery.Item(grdTemplateGallery.CurrentRowIndex, 2) Then
                '            ' CType(ArrLst(i), myList).TemplateResult

                '            strFileName = ExamNewDocumentName ''Application.StartupPath & "\Temp\Temp9.doc"
                '            objWord = New clsWordDocument
                '            strFileName = ExamNewDocumentName
                '            strFileName = objWord.GenerateFile(CType(ArrLst(i), myList).TemplateResult, strFileName)
                '            objWord = Nothing
                '            '''''Pramod20070606

                '            LoadWordUserControl(strFileName, True)
                '            'Set the Start postion of the cursor in documents
                '            oCurDoc.Application.Selection.HomeKey(Wd.WdUnits.wdStory)

                '            CPTID = grdTemplateGallery.Item(grdTemplateGallery.CurrentRowIndex, 0)
                '            TemplateID = grdTemplateGallery.Item(grdTemplateGallery.CurrentRowIndex, 2)

                '            txtTemplateName.Text = grdTemplateGallery.Item(grdTemplateGallery.CurrentRowIndex, 3)
                '            Exit Sub
                '        End If
                '    Next
                'End If

                ' '''''=================== 
                ' '' to check that Template exists in FormGallery 
                Dim blnFromFormGallery As Boolean

                blnFromFormGallery = objclsCPTAssociation.CheckInFormGallery(m_PatientID, m_VisitID, grdTemplateGallery.Item(grdTemplateGallery.CurrentRowIndex, 0), grdTemplateGallery.Item(grdTemplateGallery.CurrentRowIndex, 2), grdTemplateGallery.Item(grdTemplateGallery.CurrentRowIndex, 4))
                'If blnFormExist = True Then
                If blnFromFormGallery = True Then
                    ''''' Template Exists in Template Gallery open to Edit
                    dt = objclsCPTAssociation.SelectForm(m_PatientID, m_VisitID, grdTemplateGallery.Item(grdTemplateGallery.CurrentRowIndex, 0), grdTemplateGallery.Item(grdTemplateGallery.CurrentRowIndex, 2), grdTemplateGallery.Item(grdTemplateGallery.CurrentRowIndex, 4))
                Else
                    ''''' Template Open from Template Gallry 
                    dt = objclsCPTAssociation.SelectTemplateGallery(grdTemplateGallery.Item(grdTemplateGallery.CurrentRowIndex, 2))
                End If

                '' column(3)= Template Image.
                If Not IsNothing(dt) Then
                    'Check if there are records for selected Node
                    If dt.Rows.Count > 0 Then
                        If IsDBNull(dt.Rows(0)(0)) = False Then
                            ''''' write Template in Stream & open it Temp9.Doc  
                            objWord = New clsWordDocument
                            strFileName = ExamNewDocumentName '' Application.StartupPath & "\Temp\Temp9.doc"
                            If blnFromFormGallery = True Then
                                strFileName = objWord.GenerateFile(dt.Rows(0)(0), strFileName)
                            Else
                                strFileName = objWord.GenerateFile(dt.Rows(0)(3), strFileName)
                            End If

                            Dim lst As New myList
                            ''CPTID
                            CPTID = grdTemplateGallery.Item(grdTemplateGallery.CurrentRowIndex, 0)
                            lst.ID = CPTID
                            ''TemplateID
                            TemplateID = grdTemplateGallery.Item(grdTemplateGallery.CurrentRowIndex, 2)
                            lst.Index = TemplateID
                            ''Template Image
                            lst.TemplateResult = CType(objWord.ConvertFiletoBinary(strFileName), Object)
                            '' Template Name
                            lst.Description = grdTemplateGallery.Item(grdTemplateGallery.CurrentRowIndex, 3)
                            '' FormID
                            lst.FormID = grdTemplateGallery.Item(grdTemplateGallery.CurrentRowIndex, 4)
                            m_FormID = lst.FormID

                            txtTemplateName.Text = grdTemplateGallery.Item(grdTemplateGallery.CurrentRowIndex, 3)

                            '''''Pramod                           
                            LoadWordUserControl(strFileName, True)
                            'Set the Start postion of the cursor in documents

                            oCurDoc.Application.Selection.HomeKey(Wd.WdUnits.wdStory)

                            ''While Inserting new CPT form the list if is not in the list then only insert the value in the arraylist

                            If blnFromFormGallery = False Then
                                If ArrLst.Count = 0 Then
                                    ArrLst.Add(lst)
                                Else
                                    Dim cnt As Integer = 0
                                    If ArrLst.Count > 0 Then
                                        For i = 0 To ArrLst.Count - 1
                                            If CType(ArrLst(i), myList).FormID <> m_FormID Then
                                                cnt = cnt + 1
                                            End If
                                        Next
                                        If cnt = ArrLst.Count Then
                                            ArrLst.Add(lst)
                                        Else
                                            lst.Dispose()
                                            lst = Nothing
                                        End If
                                    End If
                                End If
                            Else
                                lst.Dispose()
                                lst = Nothing
                            End If
                        End If


                    End If

                    oCurDoc.Saved = False
                    dt.Dispose()
                    dt = Nothing
                End If


            End If



            ''''''''''
        Catch ex As Exception
            ''MessageBox.Show("Problem found in Template. Please check the template.", "Form Gallery", MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.TemplateAllocation, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            ex = Nothing
        End Try
    End Sub


    Private Sub SaveFormGallery(Optional ByVal IsClose As Boolean = False)
        ''When we click on save and close it will go in IF Condition
        ''When we click on save it go to else condition

        If IsClose = True Then
            Try
                Dim i As Integer
                Dim ProviderID As Long
                ProviderID = 1
                ' Dim strFileName As String
                If IsNothing(oCurDoc) = False Then
                    'strFileName = ExamNewDocumentName
                    'oCurDoc.SaveAs(strFileName, Wd.WdSaveFormat.wdFormatXMLDocument, False, "", False)
                    'wdFormGallery.Close()
                    ' Dim lst As New myList
                    Dim myByte As Byte() = gloWord.LoadAndCloseWord.SaveWordFiletoBinary(wdFormGallery, oCurDoc, oWordApp, gloSettings.FolderSettings.AppTempFolderPath, False, True)

                    Dim myBinaray As Object = Nothing
                    If (IsNothing(myByte) = False) Then
                        myBinaray = CType(myByte, Object)
                    End If

                    'If strFileName <> "" Then
                    '    ObjWord = New clsWordDocument
                    '    Dim myBinaray As Object = CType(ObjWord.ConvertFiletoBinary(strFileName), Object)
                    '    ObjWord = Nothing
                    If ArrLst.Count > 0 Then
                        For i = 0 To ArrLst.Count - 1
                            If CType(ArrLst(i), myList).ID = CPTID AndAlso CType(ArrLst(i), myList).Index = TemplateID AndAlso CType(ArrLst(i), myList).FormID = m_FormID Then
                                CType(ArrLst(i), myList).TemplateResult = myBinaray
                                Exit For
                            End If
                        Next
                    End If
                    'End If

                    If IsNothing(ArrLst) = False Then
                        If ArrLst.Count > 0 Then
                            If (IsNothing(objclsCPTAssociation)) Then
                                objclsCPTAssociation = New clsCPTAssociation

                            End If
                            objclsCPTAssociation.SaveFormGallery(m_PatientID, m_VisitID, ArrLst)
                        End If
                    End If
                End If

            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.TemplateAllocation, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                ex = Nothing
            Finally
                If (IsNothing(oCurDoc) = False) Then
                    Try
                        Marshal.ReleaseComObject(oCurDoc)
                    Catch ex As Exception


                    End Try
                    oCurDoc = Nothing

                End If
            End Try

        Else


            If IsNothing(oCurDoc) = False Then
                If m_FormID <> 0 Then

                    Dim myByte As Byte() = gloWord.LoadAndCloseWord.SaveWordFiletoBinary(wdFormGallery, oCurDoc, oWordApp, gloSettings.FolderSettings.AppTempFolderPath, False, False)

                    Dim fileiInBinary As Object = Nothing
                    If (IsNothing(myByte) = False) Then
                        fileiInBinary = CType(myByte, Object)
                    End If

                    'Dim strFileName As String = ExamNewDocumentName ''Application.StartupPath & "\Temp\Temp9.doc"
                    'oCurDoc.SaveAs(strFileName, Wd.WdSaveFormat.wdFormatXMLDocument, False, "", False)
                    'wdFormGallery.Close()
                    'ObjWord = New clsWordDocument
                    'Dim fileiInBinary = CType(ObjWord.ConvertFiletoBinary(strFileName), Object)
                    'Dim lst As New myList
                    If ArrLst.Count > 0 Then
                        For i As Integer = 0 To ArrLst.Count - 1
                            If CType(ArrLst(i), myList).ID = CPTID AndAlso CType(ArrLst(i), myList).Index = TemplateID AndAlso CType(ArrLst(i), myList).FormID = m_FormID Then
                                CType(ArrLst(i), myList).TemplateResult = fileiInBinary
                                Exit For
                            End If
                        Next
                    End If
                    If (IsNothing(objclsCPTAssociation)) Then
                        objclsCPTAssociation = New clsCPTAssociation

                    End If
                    objclsCPTAssociation.InsertUpdateFormGallery(m_PatientID, m_FormID, CPTID, m_VisitID, TemplateID, fileiInBinary)
                    'LoadWordUserControl(strFileName, False)
                    'oCurDoc.Application.Selection.HomeKey(Wd.WdUnits.wdStory)
                    'oCurDoc.Saved = True
                    'ObjWord = Nothing

                    If hashNewTemplates IsNot Nothing Then
                        If hashNewTemplates.Contains(m_FormID) Then
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.TemplateAllocation, gloAuditTrail.ActivityType.Add, "Form Added", m_PatientID, m_FormID, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                            hashNewTemplates.Remove(m_FormID)

                            If hashExistingTemplates IsNot Nothing Then
                                If Not hashExistingTemplates.Contains(m_FormID) Then
                                    hashExistingTemplates.Add(m_FormID)
                                End If
                            End If
                        Else
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.TemplateAllocation, gloAuditTrail.ActivityType.Modify, "Form Modified", m_PatientID, m_FormID, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                        End If
                    Else
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.TemplateAllocation, gloAuditTrail.ActivityType.Modify, "Form Modified", m_PatientID, m_FormID, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                    End If


                End If
            End If
        End If


    End Sub

    'Private Sub SaveFormGallery(Optional ByVal IsClose As Boolean = False)
    '    Try
    '        Dim i As Integer
    '        'Dim j As Integer
    '        'Dim strNode As String
    '        'Dim Node As TreeNode
    '        'Dim ParentNode As TreeNode
    '        Dim ProviderID As Long
    '        ProviderID = 1
    '        Dim strFileName As String
    '        'Insert Order template in a temporary table
    '        If IsNothing(oCurDoc) = False Then
    '            'If oCurDoc.Saved = False Then

    '            strFileName = ExamNewDocumentName ''Application.StartupPath & "\Temp\Temp9.doc"

    '            '  wdFormGallery.Save(strFileName, True, "", "")
    '            oCurDoc.SaveAs(strFileName, Wd.WdSaveFormat.wdFormatXMLDocument, False, "", False)
    '            '''''Check if Visit Id is selected or not

    '            wdFormGallery.Close()

    '            Dim lst As New myList
    '            lst.ID = CPTID
    '            lst.Index = TemplateID
    '            If strFileName <> "" Then
    '                ObjWord = New clsWordDocument
    '                lst.TemplateResult = CType(ObjWord.ConvertFiletoBinary(strFileName), Object)
    '                ObjWord = Nothing
    '                '' Template Name
    '                lst.Description = txtTemplateName.Text

    '                If ArrLst.Count > 0 Then
    '                    For i = 0 To ArrLst.Count - 1
    '                        If CType(ArrLst(i), myList).ID = CPTID And CType(ArrLst(i), myList).Index = TemplateID Then
    '                            CType(ArrLst(i), myList).TemplateResult = lst.TemplateResult
    '                            blnFormExist = True
    '                            Exit For
    '                        Else
    '                            blnFormExist = False
    '                        End If
    '                    Next

    '                    If blnFormExist = False Then
    '                        ArrLst.Add(lst)
    '                    End If

    '                Else
    '                    ArrLst.Add(lst)
    '                End If
    '            End If
    '            If IsNothing(ArrLst) = False Then
    '                If ArrLst.Count > 0 Then
    '                    objclsCPTAssociation.SaveFormGallery(m_PatientID, m_VisitID, ArrLst)
    '                    'Next
    '                End If
    '                If Not IsClose Then
    '                    LoadWordUserControl(strFileName, False)
    '                    'Set the Start postion of the cursor in documents
    '                    oCurDoc.Application.Selection.HomeKey(Wd.WdUnits.wdStory)
    '                    oCurDoc.Saved = True
    '                End If

    '            End If
    '        End If

    '    Catch ex As Exception
    '        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.TemplateAllocation, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
    '        MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try

    'End Sub



    Private Sub txtsearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearch.TextChanged
        Try
            If txtSearch.Tag <> Trim(txtSearch.Text) Then
                ' If btnAllDrugs.Dock = DockStyle.Top Then
                If rbSearch2.Checked = False Then
                    AddCPT(Trim(txtSearch.Text), dtCPTCode)
                Else
                    AddCPT(Trim(txtSearch.Text), dtCPTdesc)
                End If

                'ElseIf btnClinicalDrugs.Dock = DockStyle.Top Then
                '    AddDrugs(Trim(txtsearchCPT.Text))
                'ElseIf btnFreqDrugs.Dock = DockStyle.Top Then
                '    AddDrugs(Trim(txtsearchCPT.Text))
                'End If
                'If Len(Trim(txtsearchDrug.Text)) = 1 Then
                txtSearch.Tag = Trim(txtSearch.Text)
                'End If
            End If
            Exit Sub

            Dim mychildnode As TreeNode
            'child node collection
            For Each mychildnode In trvCptAssocation.Nodes
                'compare selected node text and entered text
                'Dim str As String
                ' str = Mid(UCase(Trim(mychildnode.Tag)), 1, Len(UCase(Trim(txtSearchDrugs.Text))))
                If mychildnode.Text.StartsWith(UCase(Trim(txtSearch.Text))) Then
                    trvCptAssocation.SelectedNode = mychildnode
                    txtSearch.Focus()
                    Exit Sub
                End If
            Next
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.TemplateAllocation, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            ex = Nothing
        End Try

    End Sub

    Private Sub txtSearch_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSearch.KeyPress
        Try
            If (e.KeyChar = ChrW(13)) Then
                trvCptAssocation.Select()
            Else
                trvCptAssocation.SelectedNode = trvCptAssocation.Nodes.Item(0)
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.TemplateAllocation, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Private Sub txtSearch_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtSearch.Validating
        'Try
        '    Dim mychildnode As TreeNode
        '    'child node collection
        '    If trvCptAssocation.GetNodeCount(True) > 1 Then
        '        For Each mychildnode In trvCptAssocation.Nodes.Item(0).Nodes
        '            'compare selected node text and entered text
        '            'Dim str As String
        '            'str = Mid(UCase(Trim(mychildnode.Tag)), 1, Len(UCase(Trim(txtSearchDrugs.Text))))
        '            If mychildnode.Text.StartsWith(UCase(Trim(txtSearch.Text))) Then
        '                trvCptAssocation.SelectedNode = mychildnode
        '                trvCptAssocation.Focus()
        '                Exit Sub
        '            End If
        '        Next
        '    End If
        'Catch ex As Exception
        '    'MessageBox.Show(ex.Message, "CPT Association", MessageBoxButtons.OK, MessageBoxIcon.Error)
        'End Try
    End Sub

    'Private Sub tblMedication_ButtonClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolBarButtonClickEventArgs)
    '    Try
    '        Select Case e.Button.Text
    '            Case "&Print"
    '                'oCurDoc.ActiveWindow.PrintOut()
    '                Print()
    '            Case "&Fax"
    '                Try
    '                    Me.Cursor = Cursors.WaitCursor
    '                    Call GeneratePrintFaxDocument(False)
    '                    Me.Cursor = Cursors.Default
    '                Catch ex As Exception
    '                    Me.Cursor = Cursors.Default
    '                End Try
    '            Case "&Save"
    '                Call SaveFormGallery()
    '                CancelClick = False
    '                Me.Close()
    '            Case "&Close"
    '                CancelClick = True
    '                Me.Close()
    '            Case "Print All"
    '                Call SaveFormGallery()
    '                Call PrintAll()
    '            Case "Fax All"
    '                Call SaveFormGallery()
    '                Call FAXAll()
    '            Case "Capture"
    '                Call InsertSignature()
    '            Case "Sign"
    '                Call InsertProviderSignature()
    '        End Select
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message, "CPT Assocation", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Sub

    Private Sub Print()
        If Not oCurDoc Is Nothing Then
            GeneratePrintFaxDocument()
            ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.TemplateAllocation, gloAuditTrail.ActivityType.Print, "Patient Form Gallery Document Printed.", gloAuditTrail.ActivityOutCome.Success)
            ''Added Rahul P on 20101011
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.TemplateAllocation, gloAuditTrail.ActivityType.Print, "Patient Form Gallery Document Printed.", m_PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            ''
            'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.PHIExport, "Patient Form Gallery Document Printed.", gstrLoginName, gstrClientMachineName, gnPatientID)
        End If


    End Sub
    Private Sub GeneratePrintFaxDocument(Optional ByVal IsPrintFlag As Boolean = True)
        If Not oCurDoc Is Nothing Then
            Dim _SaveFlag As Boolean = False
            Dim PageNo As Integer = 0
            Dim totalPages As Integer = 0
            Dim PageCountStat As Microsoft.Office.Interop.Word.WdStatistic = Microsoft.Office.Interop.Word.WdStatistic.wdStatisticPages
            Dim Missing As Object = System.Reflection.Missing.Value
            If oCurDoc.Saved Then
                _SaveFlag = True
            End If
            'Dim sFileName As String = ExamNewDocumentName
            'Dim wordRefresh As New WordRefresh()
            'Dim WDocViewType As Wd.WdViewType
            'If IsPrintFlag Then
            '    'Ashish added on 1st November
            '    'to prevent screen from refreshing            
            '    'WDocViewType = oCurDoc.ActiveWindow.View.Type
            '    'wordRefresh.OptimizePerformance(False, oCurDoc, 0)
            'End If
            If IsNothing(wdFormGallery) = False AndAlso IsNothing(oWordApp) = False Then

                Try
                    gloWord.LoadAndCloseWord.SaveDSO(wdFormGallery, oCurDoc, oWordApp)
                Catch ex As Exception

                End Try
                If (IsPrintFlag) Then
                    Try
                        PageNo = oCurDoc.Application.Selection.Information(Microsoft.Office.Interop.Word.WdInformation.wdActiveEndPageNumber)
                    Catch ex As Exception

                    End Try
                    Try
                        totalPages = oCurDoc.ComputeStatistics(PageCountStat, Missing)
                    Catch ex As Exception

                    End Try
                End If
                ' wdFormGallery.Save(sFileName, True, "", "")
                'Try

                '    oCurDoc.SaveAs(oCurDoc.FullName)
                'Catch ex As Exception
                '    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.TemplateAllocation, gloAuditTrail.ActivityType.Print, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)

                '    Try
                '        oCurDoc.Save()
                '    Catch ex1 As Exception

                '        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.TemplateAllocation, gloAuditTrail.ActivityType.Print, ex1.ToString(), gloAuditTrail.ActivityOutCome.Failure)

                '    End Try
                'End Try
                'oCurDoc.SaveAs(sFileName, Wd.WdSaveFormat.wdFormatXMLDocument, False, "", False)
                'oCurDoc.Saved = _SaveFlag


                'If Not File.Exists(sFileName) Then
                '    Try
                '        File.Copy(oCurDoc.FullName, sFileName)
                '    Catch ex As Exception
                '        MessageBox.Show("Error while printing or faxing. Please try again.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                '        ex = Nothing
                '    End Try
                'End If

                'If Not File.Exists(sFileName) Then
                '    Exit Sub
                'End If

                '  wdFormGallery.Close()

                'wdTemp = New AxDSOFramer.AxFramerControl

                'Me.Controls.Add(wdTemp)

                ' ''Open Template for processing in Temp user Ctrl
                'wdTemp.Open(sFileName)
                'oTempDoc = wdTemp.ActiveDocument
                'oTempDoc.ActiveWindow.SetFocus()
                Dim myLoadWord As gloWord.LoadAndCloseWord = New gloWord.LoadAndCloseWord()
                Try
                    PrintAndFaxWord.ClsPrintOrFax.PrintOrFaxWordDocument(myLoadWord, oCurDoc.FullName, IsPrintFlag, m_PatientID, AddressOf FaxCPTTemplate, totalPages, PageNo:=PageNo, iOwner:=Me)
                Catch ex As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.TemplateAllocation, gloAuditTrail.ActivityType.Print, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                    ex = Nothing
                End Try


                'Dim oTempDoc As Wd.Document = myLoadWord.LoadWordApplication(sFileName)

                'If IsPrintFlag Then
                '    'If oTempDoc.ProtectionType = Wd.WdProtectionType.wdAllowOnlyComments Then
                '    '    oTempDoc.Unprotect()
                '    'End If
                '    'Dim oPrint As New clsPrintFAX
                '    'oPrint.PrintDoc(oTempDoc, m_PatientID)
                '    'oPrint.Dispose()
                '    'oPrint = Nothing


                'Else
                '    Call FaxCPTTemplate(myLoadWord, oTempDoc)
                '    ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.TemplateAllocation, gloAuditTrail.ActivityType.Fax, "Form Gallery Fax", gloAuditTrail.ActivityOutCome.Success)
                '    ''Added Rahul P on 20101011
                '    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.TemplateAllocation, gloAuditTrail.ActivityType.Fax, "Form Gallery Fax", m_PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                '    ''
                '    'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.PHIExport, "Form Gallery Fax", gstrLoginName, gstrClientMachineName, gnPatientID)
                'End If
                'wdTemp.Close()
                'Me.Controls.Remove(wdTemp)
                'wdTemp.Dispose()
                myLoadWord.CloseApplicationOnly()
                myLoadWord = Nothing

                'LoadWordUserControl(sFileName, False)
                'oCurDoc.ActiveWindow.View.ShowFieldCodes = False
                ''Set the Start postion of the cursor in documents
                'oCurDoc.Application.Selection.HomeKey(Wd.WdUnits.wdStory)
                If Not IsNothing(oCurDoc) Then
                    oCurDoc.Saved = _SaveFlag
                End If
                'If IsPrintFlag Then
                '    'Ashish added on 1st November
                '    'to prevent screen from refreshing
                '    'wordRefresh.OptimizePerformance(True, oCurDoc, WDocViewType)
                '    'WDocViewType = Nothing
                'End If

                'wordRefresh.Dispose()
                'wordRefresh = Nothing
            End If

        End If

    End Sub

    Private Sub PrintAll()

        'Dim strFileName As String
        'Dim i As Integer
        'variable oPrint moved from loop to out of loop for preserving variable _sPrivioususedPrinter by Dipak 20090825

        Dim blnPrintCancel As Boolean = False
        Dim _PreviousUsedPrinter As String = ""
        'Dim myTempDoc As String = Nothing

        If IsNothing(oCurDoc) = False Then
            If m_FormID <> 0 Then
                'strFileName = gloWord.LoadAndCloseWord.NewDocumentName(gloSettings.FolderSettings.AppTempFolderPath) 'ExamNewDocumentName ''Application.StartupPath & "\Temp\Temp9.doc"
                'oCurDoc.SaveAs(strFileName, Wd.WdSaveFormat.wdFormatXMLDocument, False, "", False)
                'wdFormGallery.Close()
                'ObjWord = New clsWordDocument
                Dim myByte As Byte() = gloWord.LoadAndCloseWord.SaveWordFiletoBinary(oCurDoc, gloSettings.FolderSettings.AppTempFolderPath)
                Dim fileiInBinary As Object = Nothing
                If (IsNothing(myByte) = False) Then
                    fileiInBinary = CType(myByte, Object)
                End If

                '   Dim lst As New myList
                If ArrLst.Count > 0 Then
                    For iCnt As Integer = 0 To ArrLst.Count - 1
                        If CType(ArrLst(iCnt), myList).ID = CPTID AndAlso CType(ArrLst(iCnt), myList).Index = TemplateID AndAlso CType(ArrLst(iCnt), myList).FormID = m_FormID Then
                            CType(ArrLst(iCnt), myList).TemplateResult = fileiInBinary
                            Exit For
                        End If
                    Next
                End If
                If (IsNothing(objclsCPTAssociation)) Then
                    objclsCPTAssociation = New clsCPTAssociation

                End If
                objclsCPTAssociation.InsertUpdateFormGallery(m_PatientID, m_FormID, CPTID, m_VisitID, TemplateID, fileiInBinary)
                'LoadWordUserControl(strFileName, False)
                'oCurDoc.Application.Selection.HomeKey(Wd.WdUnits.wdStory)
                'oCurDoc.Saved = True
                'ObjWord = Nothing

                If hashNewTemplates IsNot Nothing Then
                    If hashNewTemplates.Contains(m_FormID) Then
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.TemplateAllocation, gloAuditTrail.ActivityType.Add, "Form Added", m_PatientID, m_FormID, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                        hashNewTemplates.Remove(m_FormID)

                        If hashExistingTemplates IsNot Nothing Then
                            If Not hashExistingTemplates.Contains(m_FormID) Then
                                hashExistingTemplates.Add(m_FormID)
                            End If
                        End If
                    Else
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.TemplateAllocation, gloAuditTrail.ActivityType.Modify, "Form Modified", m_PatientID, m_FormID, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                    End If
                Else
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.TemplateAllocation, gloAuditTrail.ActivityType.Modify, "Form Modified", m_PatientID, m_FormID, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                End If
            End If
        End If


      

        blnPrintCancel = False
        If ArrLst.Count > 0 Then
            ''added for bugid 104407
            PrintAndFaxWord.ClsPrintOrFax.PrintAllorFaxGalleryDocument(ArrLst, m_PatientID, m_VisitID, _PreviousUsedPrinter)
            'Dim myLoadWord As gloWord.LoadAndCloseWord = New gloWord.LoadAndCloseWord()
            'Dim oTempDoc As Wd.Document = Nothing
            'Dim PageCountStat As Microsoft.Office.Interop.Word.WdStatistic = Microsoft.Office.Interop.Word.WdStatistic.wdStatisticPages
            'Dim Missing As Object = System.Reflection.Missing.Value

            'Try
            '    tlsFormGallery.MyToolStrip.Items("PrintAll").Enabled = False
            '    For i = 0 To ArrLst.Count - 1
            '        ' ObjWord = New clsWordDocument
            '        Dim lst As myList
            '        lst = CType(ArrLst.Item(i), myList)

            '        txtTemplateName.Text = lst.Description
            '        '  strFileName = ExamNewDocumentName '' Application.StartupPath & "\Temp\Temp9.doc"
            '        strFileName = gloWord.LoadAndCloseWord.ConvertFileFromBinary(lst.TemplateResult, gloSettings.FolderSettings.AppTempFolderPath) 'ObjWord.GenerateFile(lst.TemplateResult, strFileName)
            '        'ObjWord = Nothing
            '        If strFileName <> "" Then
            '            ObjWord = New clsWordDocument
            '            objCriteria = New DocCriteria
            '            objCriteria.DocCategory = enumDocCategory.Others  ''added for bugid 87030
            '            objCriteria.PatientID = m_PatientID
            '            objCriteria.VisitID = m_VisitID
            '            objCriteria.PrimaryID = 0
            '            ObjWord.DocumentCriteria = objCriteria

            '            oTempDoc = myLoadWord.LoadWordApplication(strFileName)

            '            ObjWord.CurDocument = oTempDoc
            '            ObjWord.GetFormFieldData(enumDocType.None)
            '            oTempDoc = ObjWord.CurDocument
            '            objCriteria.Dispose()
            '            objCriteria = Nothing

            '            ObjWord = Nothing
            '            Dim myWordFileName As String = myLoadWord.SaveCurrentWord(oTempDoc, gloSettings.FolderSettings.AppTempFolderPath)


            '            PrintAndFaxWord.ClsPrintOrFax.PrintAllOrFaxWordDocument(myLoadWord, myWordFileName, True, m_PatientID, Nothing, 0, Not CType(i, Boolean), oTempDoc, blnPrintCancel:=blnPrintCancel, _PreviousUsedPrinter:=_PreviousUsedPrinter, PrintDocno:=i)



            '            myLoadWord.CloseWordOnly(oTempDoc)


            '        End If

            '        If (blnPrintCancel = True) Then
            '            Exit For
            '        End If
            '    Next
            'Catch ex As Exception
            '    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.TemplateAllocation, gloAuditTrail.ActivityType.PrintAll, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            '    ex = Nothing
            'Finally
            '    ''added property for bugid 96982,96984
            '    PrintAndFaxWord.ClsPrintOrFax.SetAllFileDialog = Nothing
            '    tlsFormGallery.MyToolStrip.Items("PrintAll").Enabled = True
            'End Try


            'myLoadWord.CloseApplicationOnly()
            ' myLoadWord = Nothing

            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.TemplateAllocation, gloAuditTrail.ActivityType.Print, "Patient Form Gallery Document Printed", m_PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
        End If

    End Sub

    Private Sub lstTemplates_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstTemplates.SelectedValueChanged
        Dim objclsTemplateGalllry As New clsTemplateGallery
        Dim dt As DataTable

        dt = objclsTemplateGalllry.SelectTemplateGallery(lstTemplates.SelectedValue)
        ' 3 Template dEsc.

        Dim strFileName As String
        Dim objWord As clsWordDocument

        If Not IsNothing(dt) Then
            'Check if there are records for selected Node
            If dt.Rows.Count > 0 Then
                If IsDBNull(dt.Rows(0)(0)) = False Then
                    objWord = New clsWordDocument
                    strFileName = ExamNewDocumentName '' Application.StartupPath & "\Temp\Temp9.doc"

                    strFileName = objWord.GenerateFile(dt.Rows(0)(3), strFileName)
                    objWord = Nothing
                    LoadWordUserControl(strFileName, False)
                    'Set the Start postion of the cursor in documents
                    oCurDoc.Application.Selection.HomeKey(Wd.WdUnits.wdStory)

                End If
            End If
            dt.Dispose()
            dt = Nothing
        End If
        objclsTemplateGalllry.Dispose()
        objclsTemplateGalllry = Nothing
    End Sub

    Private Sub lstCPT_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstCPT.SelectedValueChanged
        'Dim dt As New DataTable
        'objclsCPTAssociation = New clsCPTAssociation
        'dt = objclsCPTAssociation.GetAccociatedTemplates(lstCPT.SelectedValue)
        'lstTemplates.DataSource = dt
        'lstCPT.DisplayMember = dt.Columns(1).ColumnName
        'lstCPT.ValueMember = dt.Columns(0).ColumnName
    End Sub

    Private Sub grdTemplateGallery_Navigate(ByVal sender As System.Object, ByVal ne As System.Windows.Forms.NavigateEventArgs) Handles grdTemplateGallery.Navigate

    End Sub

    Private Sub pnlGrid_SizeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles pnlGrid.SizeChanged
        Try
            'grdTemplateGallery.DataSource = tblTemplate
            'CustomGridStyle()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.TemplateAllocation, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            ex = Nothing
        End Try
    End Sub

    Private Sub FaxCPTTemplate(ByRef myLoadWord As gloWord.LoadAndCloseWord, ByVal oTempDoc As String)
        mdlFAX.Owner = Me
        If RetrieveFAXDetails(mdlFAX.enmFAXType.FormGallery, m_PatientID, "", "", txtTemplateName.Text, 0, 0, 0, True, Me) = False Then
            Exit Sub
        End If
        CurrentSendingFAXPriority = mdlFAX.enmFAXPriority.NormalPriority

        ''Unprotect the document
        'If oTempDoc.ProtectionType = Wd.WdProtectionType.wdAllowOnlyComments Then
        '    oTempDoc.Unprotect()
        'End If

        'Commented by Shweta 20100201
        '''''''Against the bug id:5260 '''''''
        'Check the FAX Cover Page is enabled or not.
        'If the FAX Cover Page is enabled then Delete the Page Header from Exam
        'If gblnFAXCoverPage Then
        '    'To Delete Header
        '    'UpdateLog("Deleting Patient Form Gallery Page Header")
        '    gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.TemplateAllocation, gloAuditTrail.ActivityType.Fax, "Deleting Patient Form Gallery Page Header", gloAuditTrail.ActivityOutCome.Success)
        '    Try

        '        If oTempDoc.ActiveWindow.ActivePane.View.Type = Wd.WdViewType.wdNormalView Or oTempDoc.ActiveWindow.ActivePane.View.Type = Wd.WdViewType.wdOutlineView Then
        '            oTempDoc.ActiveWindow.ActivePane.View.Type = Wd.WdViewType.wdPrintView
        '        End If
        '        oTempDoc.Activate()
        '        oTempDoc.ActiveWindow.ActivePane.View.SeekView = Wd.WdSeekView.wdSeekPrimaryHeader
        '        If oTempDoc.Application.Selection.HeaderFooter.IsHeader Then
        '            oTempDoc.Application.Selection.HeaderFooter.Range.Select()
        '            oTempDoc.Application.Selection.HeaderFooter.Range.Delete()
        '            'UpdateLog("Form Gallery Page Header deleted")
        '            gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.TemplateAllocation, gloAuditTrail.ActivityType.Fax, "Form Gallery Page Header deleted", gloAuditTrail.ActivityOutCome.Success)
        '        End If

        '    Catch ex As Exception
        '        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.TemplateAllocation, gloAuditTrail.ActivityType.Fax, "Error Deleting Form Gallery Page Header - " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
        '        'UpdateVoiceLog("Error Deleting Form Gallery Page Header - " & ex.ToString)
        '    Finally
        '        oTempDoc.ActiveWindow.ActivePane.View.SeekView = Wd.WdSeekView.wdSeekMainDocument
        '    End Try

        'End If
        'End Commenting

        'Send the document for Printing i.e. to generate the TIFF File
        Dim objPrintFAX As New clsPrintFAX(gstrFAXPrinterName)
        If objPrintFAX.FAXDocument(myLoadWord, oTempDoc, m_PatientID, gstrFAXContactPerson, gstrFAXContactPersonFAXNo, gstrLoginName, System.DateTime.Now, txtTemplateName.Text, clsPrintFAX.enmFAXType.FormGallery) = False Then
            If Trim(objPrintFAX.ErrorMessage) <> "" Then
                MessageBox.Show("Unable to send the Fax due to " & objPrintFAX.ErrorMessage, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If

        End If
        objPrintFAX.Dispose()
        objPrintFAX = Nothing
    End Sub

    Private Sub FAXAll()
        CurrentSendingFAXPriority = mdlFAX.enmFAXPriority.NormalPriority

        Dim strFileName As String
        Dim i As Integer
        'Dim myTempDoc As String = Nothing

        If IsNothing(oCurDoc) = False Then
            If m_FormID <> 0 Then
                'strFileName = ExamNewDocumentName ''Application.StartupPath & "\Temp\Temp9.doc"
                'oCurDoc.SaveAs(strFileName, Wd.WdSaveFormat.wdFormatXMLDocument, False, "", False)
                ''wdFormGallery.Close()
                'ObjWord = New clsWordDocument
                'Dim fileiInBinary = CType(ObjWord.ConvertFiletoBinary(strFileName), Object)
                ' strFileName = gloWord.LoadAndCloseWord.NewDocumentName(gloSettings.FolderSettings.AppTempFolderPath) 'ExamNewDocumentName ''Application.StartupPath & "\Temp\Temp9.doc"
                'oCurDoc.SaveAs(strFileName, Wd.WdSaveFormat.wdFormatXMLDocument, False, "", False)

                Dim myByte As Byte() = gloWord.LoadAndCloseWord.SaveWordFiletoBinary(oCurDoc, gloSettings.FolderSettings.AppTempFolderPath)
                Dim fileiInBinary As Object = Nothing
                If (IsNothing(myByte) = False) Then
                    fileiInBinary = CType(myByte, Object)
                End If

                ' Dim lst As New myList

                If ArrLst.Count > 0 Then
                    For iCnt As Integer = 0 To ArrLst.Count - 1
                        If CType(ArrLst(iCnt), myList).ID = CPTID AndAlso CType(ArrLst(iCnt), myList).Index = TemplateID AndAlso CType(ArrLst(iCnt), myList).FormID = m_FormID Then
                            CType(ArrLst(iCnt), myList).TemplateResult = fileiInBinary
                            Exit For
                        End If
                    Next
                End If
                If (IsNothing(objclsCPTAssociation)) Then
                    objclsCPTAssociation = New clsCPTAssociation

                End If
                objclsCPTAssociation.InsertUpdateFormGallery(m_PatientID, m_FormID, CPTID, m_VisitID, TemplateID, fileiInBinary)
                'LoadWordUserControl(strFileName, False)
                'oCurDoc.Application.Selection.HomeKey(Wd.WdUnits.wdStory)
                'oCurDoc.Saved = True
                'ObjWord = Nothing

                If hashNewTemplates IsNot Nothing Then
                    If hashNewTemplates.Contains(m_FormID) Then
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.TemplateAllocation, gloAuditTrail.ActivityType.Add, "Form Added", m_PatientID, m_FormID, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                        hashNewTemplates.Remove(m_FormID)

                        If hashExistingTemplates IsNot Nothing Then
                            If Not hashExistingTemplates.Contains(m_FormID) Then
                                hashExistingTemplates.Add(m_FormID)
                            End If
                        End If
                    Else
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.TemplateAllocation, gloAuditTrail.ActivityType.Modify, "Form Modified", m_PatientID, m_FormID, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                    End If
                Else
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.TemplateAllocation, gloAuditTrail.ActivityType.Modify, "Form Modified", m_PatientID, m_FormID, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                End If
            End If
        End If

        ''Close the actual opened document 
        'If Not IsNothing(oCurDoc) Then
        '    myTempDoc = oCurDoc.FullName.ToString()
        '    wdFormGallery.Close()
        'End If
        If ArrLst.Count > 0 Then
            'wdOrderComment.Hide()
            Dim myLoadWord As gloWord.LoadAndCloseWord = New gloWord.LoadAndCloseWord()
            Dim oTempDoc As Wd.Document = Nothing
            Try
                For i = 0 To ArrLst.Count - 1
                    '   ObjWord = New clsWordDocument
                    Dim lst As myList
                    lst = CType(ArrLst(i), myList)

                    txtTemplateName.Text = lst.Description

                    '  strFileName = ExamNewDocumentName '' Application.StartupPath & "\Temp\Temp9.doc"
                    strFileName = gloWord.LoadAndCloseWord.ConvertFileFromBinary(lst.TemplateResult, gloSettings.FolderSettings.AppTempFolderPath) 'ObjWord.GenerateFile(lst.TemplateResult, strFileName)
                    'ObjWord = Nothing
                    If strFileName <> "" Then
                        ObjWord = New clsWordDocument
                        objCriteria = New DocCriteria
                        objCriteria.DocCategory = enumDocCategory.Others ''change to get the only patient related data 
                        objCriteria.PatientID = m_PatientID
                        objCriteria.VisitID = m_VisitID
                        objCriteria.PrimaryID = 0
                        ObjWord.DocumentCriteria = objCriteria

                        'wdTemp = New AxDSOFramer.AxFramerControl

                        'Me.Controls.Add(wdTemp)
                        ' ''Open Template for processing in Temp user Ctrl
                        'wdTemp.Open(strFileName)
                        'oTempDoc = wdTemp.ActiveDocument
                        oTempDoc = myLoadWord.LoadWordApplication(strFileName)

                        ObjWord.CurDocument = oTempDoc
                        ObjWord.GetFormFieldData(enumDocType.None)
                        oTempDoc = ObjWord.CurDocument
                        objCriteria.Dispose()
                        objCriteria = Nothing
                        ObjWord = Nothing
                        Dim myWordFile As String = myLoadWord.SaveCurrentWord(oTempDoc, gloSettings.FolderSettings.AppTempFolderPath)
                        myLoadWord.CloseWordOnly(oTempDoc)
                        'Try
                        '    oTempDoc.SaveAs(oTempDoc.FullName)
                        'Catch ex As Exception
                        '    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.TemplateAllocation, gloAuditTrail.ActivityType.PrintAll, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                        '    Try
                        '        oTempDoc.Save()
                        '    Catch ex1 As Exception

                        '        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.TemplateAllocation, gloAuditTrail.ActivityType.Print, ex1.ToString(), gloAuditTrail.ActivityOutCome.Failure)

                        '    End Try
                        'End Try
                        '    oTempDoc.SaveAs(strFileName, Wd.WdSaveFormat.wdFormatXMLDocument, False, "", False)


                        'If Not File.Exists(strFileName) Then
                        '    Try
                        '        File.Copy(oTempDoc.FullName, strFileName)
                        '    Catch ex As Exception
                        '        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.TemplateAllocation, gloAuditTrail.ActivityType.FaxAll, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                        '        ex = Nothing
                        '    End Try
                        'End If

                        'If Not File.Exists(strFileName) Then
                        '    Exit Sub
                        'End If

                        'Call FaxCPTTemplate(myLoadWord, oTempDoc)
                        PrintAndFaxWord.ClsPrintOrFax.PrintOrFaxWordDocument(myLoadWord, myWordFile, False, m_PatientID, AddressOf FaxCPTTemplate, 0, Not CType(i, Boolean), UseDirectFaxName:=True, iOwner:=Me)
                        'wdTemp.Close()
                        'Me.Controls.Remove(wdTemp)
                        'wdTemp.Dispose()
                        'oTempDoc = Nothing


                    End If

                Next
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.TemplateAllocation, gloAuditTrail.ActivityType.FaxAll, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                ex = Nothing
            End Try

            myLoadWord.CloseApplicationOnly()
            myLoadWord = Nothing
            'If Not IsNothing(myTempDoc) Then
            '    If File.Exists(myTempDoc) Then
            '        If IsNothing(oCurDoc) Then
            '            LoadWordUserControl(myTempDoc, False)
            '        End If
            '    End If
            'End If
        End If

    End Sub

    Public Sub InsertSignature()
        If oCurDoc Is Nothing Then
            Exit Sub
        End If
        Try
            ImagePath = ""
            Dim frm As New FrmSignature
            frm.Owner = Me
            'frm.ShowDialog(IIf(IsNothing(frm.Parent), Me, frm.Parent))
            frm.ShowDialog(frm.Parent)
            frm.Dispose()
            ''commented by Dhruv 20091214 
            ''To not to save on form closing
            'If File.Exists(ImagePath) Then
            '    oCurDoc.ActiveWindow.SetFocus()

            '    '' SUDHIR 20090619 '' 
            '    Dim oWord As New clsWordDocument
            '    oWord.CurDocument = oCurDoc
            '    oWord.InsertImage(ImagePath)
            '    oWord = Nothing
            '    'oCurDoc.Application.Selection.InlineShapes.AddPicture(FileName:=ImagePath, LinkToFile:=False, SaveWithDocument:=True)
            '    '' END SUDHIR ''

            '    oCurDoc.Application.Selection.TypeParagraph()
            '    '' By Mahesh Signature With Date - 20070113
            '    ''''' Add Date Time When Signature is Inserted
            '    oCurDoc.Application.Selection.TypeText(Text:=Format(Now, "MM/dd/yyyy") & " " & Format(Now, "Medium Time"))
            'End If
        Catch objErr As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.TemplateAllocation, gloAuditTrail.ActivityType.Add, objErr.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            objErr = Nothing
        End Try
    End Sub
    ''Dhruv 20091214 To add the signature into the Word document
    Public Sub AddSignature(ByVal sImagePath As String) Implements ISignature.AddSignature

        If Not IsNothing(oCurDoc) Then
            If File.Exists(sImagePath) Then

                oCurDoc.ActiveWindow.SetFocus()
                Dim oWord As New clsWordDocument
                oWord.CurDocument = oCurDoc
                Dim myType As Wd.WdViewType = Nothing
                Dim myLayout As Boolean = gloWord.LoadAndCloseWord.ChangeToEditView(oCurDoc, myType)
                oWord.InsertImage(sImagePath)
                oWord = Nothing
                oCurDoc.Application.Selection.TypeParagraph()
                oCurDoc.Application.Selection.TypeText(Text:=Format(Now, "MM/dd/yyyy") & " " & Format(Now, "Medium Time"))
                gloWord.LoadAndCloseWord.RestoreFromEditView(oCurDoc, myType, myLayout)
            End If
        End If
    End Sub

    Public WriteOnly Property ImageFilePath() As String Implements mdlGeneral.ISignature.ImageFilePath
        Set(ByVal Value As String)
            ImagePath = Value
        End Set
    End Property
    Public Sub InsertCoSignature()
        Try
            ObjWord = New clsWordDocument
            objCriteria = New DocCriteria
            objCriteria.DocCategory = enumDocCategory.Others
            objCriteria.PatientID = m_PatientID
            objCriteria.VisitID = 0
            objCriteria.PrimaryID = gnLoginID '' For inserting coSignature
            ObjWord.DocumentCriteria = objCriteria

            ImagePath = ObjWord.getData_FromDB("User_MST.imgSignature", "Co-Signature")
            objCriteria.Dispose()
            objCriteria = Nothing
            ObjWord = Nothing
            ImagePath = Mid(ImagePath, 1, Len(ImagePath) - 2)
            If oCurDoc Is Nothing Then
                Exit Sub
            End If
            If System.IO.File.Exists(ImagePath) Then
                oCurDoc.ActiveWindow.SetFocus()

                '' SUDHIR 20090619 '' 
                Dim oWord As New clsWordDocument
                oWord.CurDocument = oCurDoc
                Dim myType As Wd.WdViewType = Nothing
                Dim myLayout As Boolean = gloWord.LoadAndCloseWord.ChangeToEditView(oCurDoc, myType)
                oWord.InsertImage(ImagePath)
                oWord = Nothing
                'oCurDoc.Application.Selection.InlineShapes.AddPicture(FileName:=ImagePath, LinkToFile:=False, SaveWithDocument:=True)
                '' END SUDHIR ''

                oCurDoc.Application.Selection.TypeParagraph()
                '' By Mahesh Signature With Date - 20070113
                '''' Add Date Time When Signature is Inserted
                oCurDoc.Application.Selection.TypeText(Text:=Format(Now, "MM/dd/yyyy") & " " & Format(Now, "Medium Time"))
                gloWord.LoadAndCloseWord.RestoreFromEditView(oCurDoc, myType, myLayout)
                ''''
            End If
        Catch objErr As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.TemplateAllocation, gloAuditTrail.ActivityType.Modify, objErr.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            objErr = Nothing

        End Try
    End Sub

    Public Sub InsertUserSignature()
        Try
            If oCurDoc Is Nothing Then
                Exit Sub
            End If

            Dim objWord As New clsWordDocument
            Dim objCriteria As DocCriteria
            objCriteria = New DocCriteria
            objCriteria.DocCategory = enumDocCategory.Exam
            'Line commented and modified by dipak 20100907 for case UC5070.003: replace gnPatientID by local variable .
            'objCriteria.PatientID = gnPatientID
            objCriteria.PatientID = m_PatientID
            'end modification
            objCriteria.VisitID = 0
            objCriteria.PrimaryID = gnLoginID
            objWord.DocumentCriteria = objCriteria

            ImagePath = objWord.getData_FromDB("User_MST.imgSignature", "Provider Signature")
            objCriteria.Dispose()
            objCriteria = Nothing
            objWord = Nothing
            ImagePath = Mid(ImagePath, 1, Len(ImagePath) - 2)
            If ImagePath = "" Then
                MessageBox.Show("Current user has no signature on file.  Electronic signature cannot be added.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If


            If File.Exists(ImagePath) Then
                oCurDoc.ActiveWindow.SetFocus()
                Dim oWord As New clsWordDocument
                oWord.CurDocument = oCurDoc
                Dim myType As Wd.WdViewType = Nothing
                Dim myLayout As Boolean = gloWord.LoadAndCloseWord.ChangeToEditView(oCurDoc, myType)
                oWord.InsertImage(ImagePath)
                oWord = Nothing

                'code added by dipak 20100118 to fixe bug no 5741 :Patient EXAM Sigin
                Dim wdRng As Wd.Range = oCurDoc.Application.Selection.Range
                If wdRng.Tables.Count > 0 Then
                    'oCurDoc.Application.Selection.Move(1)
                    oCurDoc.Application.Selection.EndKey()
                End If
                'end code added by dipak 

                oCurDoc.Application.Selection.TypeParagraph()
                'Dim clsExam As New clsPatientExams
                'clsExam.Dispose()
                'clsExam = Nothing
                oCurDoc.Application.Selection.TypeText(Text:=gstrSignatureText & " '" & gstrLoginName & "'. " & Format(Now, "MM/dd/yyyy") & " " & Format(Now, "Medium Time") & " (" & gstrLoginName & ")")
                gloWord.LoadAndCloseWord.RestoreFromEditView(oCurDoc, myType, myLayout)
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.InsertSignature, gloAuditTrail.ActivityType.General, "User Signature Inserted from CPTTemplate", m_PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            End If
        Catch objErr As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.InsertSignature, gloAuditTrail.ActivityType.View, objErr.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            objErr = Nothing
        End Try
    End Sub

    Public Sub InsertProviderSignature(Optional ByVal ProviderID As Int64 = 0)
        'Try
        '    Dim rslt As Boolean
        '    Dim oclsProvider As New clsProvider
        '    rslt = oclsProvider.CheckSignDelegateStatus()
        '    Dim _ProviderID As Int64
        '    If ProviderID <> 0 Then

        '        Dim blnResult As Boolean
        '        Dim Pat_Provider As String
        '        Dim SelectedProvider As String
        '        Dim dResult As DialogResult
        '        blnResult = oclsProvider.CheckpatientProviderStatus(m_PatientID, ProviderID)
        '        If blnSignClick = False Then
        '            If blnResult Then
        '                ''Selected Provider Is Exam Provider
        '            Else
        '                Pat_Provider = oclsProvider.GetPatientProviderName(m_PatientID)
        '                SelectedProvider = oclsProvider.GetProviderName(ProviderID)
        '                dResult = MessageBox.Show("Patient provider '" & Pat_Provider & "' does not match provider selected for signature '" & SelectedProvider & "'.  Would you like to continue?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        '                If dResult = Windows.Forms.DialogResult.Yes Then
        '                    ''Insert The Selectedd Provider Sign
        '                Else
        '                    Return
        '                End If
        '            End If
        '        End If
        '    Else
        '        If rslt Then

        '            _ProviderID = oclsProvider.GetPatientProvider(m_PatientID)
        '            Dim dt As DataTable
        '            Dim _IsSignRight As Boolean = False
        '            Dim i As Int16
        '            dt = New DataTable
        '            dt = oclsProvider.GetAllAssignProviders(gnLoginID)
        '            If dt.Rows.Count = 0 Then
        '                If _ProviderID <> gnLoginProviderID Then
        '                    MessageBox.Show("You are not designated as a Signature Delegate for any providers.  No provider signatures are available for you to use. Signature delegates may be added in Provider setup in gloEMR Admin.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        '                    Return
        '                End If
        '            Else
        '                If dt.Rows.Count > 0 Then
        '                    For i = 0 To dt.Rows.Count - 1
        '                        If _ProviderID = dt.Rows(i)("nProviderId").ToString().Trim() Or _ProviderID = gnLoginProviderID Then
        '                            _IsSignRight = True
        '                        End If
        '                    Next
        '                    If _IsSignRight = False Then
        '                        'MessageBox.Show("Current user is not designated as a Signature Delegate for selected patient's provider. Signature delegates may be added in Provider setup in gloEMR Admin.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        '                        Dim strName As String = oclsProvider.GetPatientProviderNameWithPrefix(m_PatientID)
        '                        MessageBox.Show("User '" & gstrLoginName & "' is not designated as a Signature Delegate for '" & strName & "'. Signature Delegates may be assigned via the ‘Provider’ option in gloEMR Admin.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        '                        oclsProvider = Nothing
        '                        Return
        '                    End If
        '                End If
        '            End If
        '        End If
        '    End If
        '    Dim objWord As New clsWordDocument
        '    Dim objCriteria As DocCriteria
        '    objCriteria = New DocCriteria
        '    objCriteria.DocCategory = enumDocCategory.Others
        '    objCriteria.PatientID = m_PatientID
        '    objCriteria.VisitID = m_VisitID
        '    objCriteria.ProviderID = ProviderID
        '    objCriteria.PrimaryID = 0
        '    objWord.DocumentCriteria = objCriteria
        '    ''// Get the path of the image of Provider Signature
        '    ImagePath = objWord.getData_FromDB("Provider_MST.imgSignature", "Provider Signature")
        '    objCriteria = Nothing
        '    objWord = Nothing
        '    ''Integrated by Mayuri:20101021
        '    ImagePath = Mid(ImagePath, 1, Len(ImagePath) - 2)
        '    If ImagePath = "" Then
        '        If blnSignClick = True Then
        '            MessageBox.Show("Current user has no signature on file.  Electronic signature cannot be added.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        '        Else
        '            MessageBox.Show("Selected Provider has no signature on file.  Electronic signature cannot be added.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        '        End If
        '        Return
        '    End If
        '    If oCurDoc Is Nothing Then
        '        Exit Sub
        '    End If
        '    If File.Exists(ImagePath) Then
        '        oCurDoc.ActiveWindow.SetFocus()

        '        '' SUDHIR 20090619 '' 
        '        Dim oWord As New clsWordDocument
        '        oWord.CurDocument = oCurDoc
        '        oWord.InsertImage(ImagePath)
        '        oWord = Nothing
        '        'oCurDoc.Application.Selection.InlineShapes.AddPicture(FileName:=ImagePath, LinkToFile:=False, SaveWithDocument:=True)
        '        '' END SUDHIR ''
        '        'code added by dipak 20100118 to fixe bug no 5741 :Patient EXAM Sigin
        '        Dim wdRng As Wd.Range = oCurDoc.Application.Selection.Range
        '        If wdRng.Tables.Count > 0 Then
        '            'oCurDoc.Application.Selection.Move(1)
        '            oCurDoc.Application.Selection.EndKey()
        '        End If
        '        'end code added by dipak 
        '        ''Added on 20101008 by sanjog
        '        Dim clsExam As New clsPatientExams
        '        Dim strProviderName As String
        '        ''Added on 20101008 by snajog for signature
        '        If ProviderID <> 0 Then
        '            strProviderName = clsExam.GetProvidernameforExam(ProviderID)
        '        Else
        '            strProviderName = clsExam.GetProvidernameforExam(_ProviderID)
        '        End If
        '        ''Added on 20101008 by sanjog

        '        oCurDoc.Application.Selection.TypeParagraph()
        '        '' By Mahesh Signature With Date - 20070113
        '        ''''' Add Date Time When Signature is Inserted

        '        'Developer: Yatin N.Bhagat
        '        'Date:01/20/2012
        '        'Bug ID/PRD Name/Salesforce Case:Salesforce Case No.GLO2010-0009688 
        '        'Reason: If Condition is added to check the Setting
        '        If oclsProvider.AddUserNameInProviderSignature() Then
        '            oCurDoc.Application.Selection.TypeText(Text:=gstrSignatureText & " '" & strProviderName & "'. " & Format(Now, "MM/dd/yyyy") & " " & Format(Now, "Medium Time") & " (" & gstrLoginName & ")")
        '        Else
        '            oCurDoc.Application.Selection.TypeText(Text:=gstrSignatureText & " '" & strProviderName & "'. " & Format(Now, "MM/dd/yyyy") & " " & Format(Now, "Medium Time")) '& " (" & gstrLoginName & ")"
        '        End If
        '        '''''
        '    End If

        'Catch objErr As Exception
        '    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.TemplateAllocation, gloAuditTrail.ActivityType.General, objErr.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        '    MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        'End Try


        'Developer:Yatin N. Bhagat
        'Date:01/31/2012
        'Bug ID/PRD Name/Salesforce Case:Provider Signature Format Case
        'Reason: Comman Fucntionality is added 
        Try

            If oCurDoc Is Nothing Then
                Exit Sub
            End If
            'dtMessage.Tag = GenerateVisitID(dtMessage.Value, m_PatientID)
            Dim objWord As New clsWordDocument
            Dim oclsProvider As New clsProvider
            Dim clsExam As New clsPatientExams
            Dim pSign() As String = objWord.GetProviderSignature(ProviderID, m_PatientID, m_VisitID, blnSignClick)
            objCriteria = Nothing
            objWord = Nothing
            If pSign(2) = "1" Then
                If File.Exists(pSign(0)) Then
                    oCurDoc.ActiveWindow.SetFocus()

                    '' SUDHIR 20090619 '' 
                    Dim oWord As New clsWordDocument
                    oWord.CurDocument = oCurDoc
                    Dim myType As Wd.WdViewType = Nothing
                    Dim myLayout As Boolean = gloWord.LoadAndCloseWord.ChangeToEditView(oCurDoc, myType)
                    oWord.InsertImage(pSign(0))
                    oWord = Nothing
                    'oCurDoc.Application.Selection.InlineShapes.AddPicture(FileName:=ImagePath, LinkToFile:=False, SaveWithDocument:=True)
                    '' END SUDHIR ''
                    'code added by dipak 20100118 to fixe bug no 5741 :Patient EXAM Sigin
                    Dim wdRng As Wd.Range = oCurDoc.Application.Selection.Range
                    If wdRng.Tables.Count > 0 Then
                        'oCurDoc.Application.Selection.Move(1)
                        oCurDoc.Application.Selection.EndKey()
                    End If
                    'end code added by dipak 
                    oCurDoc.Application.Selection.TypeParagraph()
                    '' By Mahesh Signature With Date - 20070113
                    '' Add Date Time When Signature is Inserted
                    ''oCurDoc.Application.Selection.TypeText(Text:=Format(Now, "MM/dd/yyyy") & " " & Format(Now, "Medium Time"))
                    oCurDoc.Application.Selection.TypeText(Text:=pSign(1))
                    gloWord.LoadAndCloseWord.RestoreFromEditView(oCurDoc, myType, myLayout)
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.InsertSignature, gloAuditTrail.ActivityType.General, "Provider Signature Inserted", m_PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                End If
            End If
            'Dispose object by mitesh
            If Not IsNothing(clsExam) Then
                clsExam.Dispose()
                clsExam = Nothing
            End If
            If Not IsNothing(oclsProvider) Then
                oclsProvider.Dispose()
                oclsProvider = Nothing
            End If

        Catch objErr As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.InsertSignature, gloAuditTrail.ActivityType.View, objErr.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            'MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            objErr = Nothing
        End Try


    End Sub

    Public Sub Navigate(ByVal strstring As String) Implements mdlHotkey.IHotKey.Navigate
        If strstring = "ON" Then
            If gblnVoiceEnabled = True AndAlso gblnSpeakerExists = True Then
                tlsFormGallery.MyToolStrip.Items("Mic").Visible = True

                tlsFormGallery.MyToolStrip.ButtonsToHide.Remove(tlsFormGallery.MyToolStrip.Items("Mic").Name)

                tlsFormGallery.MyToolStrip.Items("Mic").Image = Global.gloEMR.My.Resources.Mic_ON
            End If
        ElseIf strstring = "OFF" Then
            If gblnVoiceEnabled = True AndAlso gblnSpeakerExists = True Then
                tlsFormGallery.MyToolStrip.Items("Mic").Visible = True
                tlsFormGallery.MyToolStrip.ButtonsToHide.Remove(tlsFormGallery.MyToolStrip.Items("Mic").Name)

            Else
                tlsFormGallery.MyToolStrip.Items("Mic").Visible = False
                If tlsFormGallery.MyToolStrip.ButtonsToHide.Contains(tlsFormGallery.MyToolStrip.Items("Mic").Name) = False Then
                    tlsFormGallery.MyToolStrip.ButtonsToHide.Add(tlsFormGallery.MyToolStrip.Items("Mic").Name)
                End If
            End If
            tlsFormGallery.MyToolStrip.Items("Mic").Image = Global.gloEMR.My.Resources.Mic_OFF
        Else
            If bnlIsFaxOpened = False Then

                Try
                    If Not oCurDoc Is Nothing Then
                        oCurDoc.ActiveWindow.SetFocus()
                        gloEMRWord.clsWordDocument.FindAndReplace(MyApp:=oCurDoc.Application, FindText:=strstring, ReplaceWith:=" ", Forward:=True, Wrap:=Wd.WdFindWrap.wdFindContinue, Replace:=Wd.WdReplace.wdReplaceNone, MatchWildCards:=False, MatchWholeWord:=False)
                    End If
                Catch ex2 As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.NurseNotes, gloAuditTrail.ActivityCategory.Fax, gloAuditTrail.ActivityType.Send, ex2.ToString, gloAuditTrail.ActivityOutCome.Failure)
                    ex2 = Nothing
                End Try
                'oCurDoc.ActiveWindow.SetFocus()

            Else
                For Each frm As Form In Application.OpenForms
                    If frm.Name = "frmSelectContactFAXWithFAXCoverPage" Then
                        If Not IsNothing(DirectCast(frm, gloEMR.frmSelectContactFAXWithFAXCoverPage).oCurDoc) Then
                            Try
                                DirectCast(frm, gloEMR.frmSelectContactFAXWithFAXCoverPage).oCurDoc.ActiveWindow.SetFocus()
                                gloEMRWord.clsWordDocument.FindAndReplace(MyApp:=DirectCast(frm, gloEMR.frmSelectContactFAXWithFAXCoverPage).oCurDoc.Application, FindText:=strstring, ReplaceWith:=" ", Forward:=True, Wrap:=Wd.WdFindWrap.wdFindContinue, Replace:=Wd.WdReplace.wdReplaceNone, MatchWildCards:=False, MatchWholeWord:=False)
                                Exit For
                            Catch ex As Exception
                                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Fax, gloAuditTrail.ActivityType.Send, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                                ex = Nothing
                            End Try
                        End If
                    End If
                Next
            End If



        End If
    End Sub

    Private Sub loadPatientStrip()
        _PatientStrip = New gloUC_PatientStrip
        _PatientStrip.ShowDetail(m_PatientID, gloUC_PatientStrip.enumFormName.FormGallery)
        _PatientStrip.Dock = DockStyle.Top
        pnlMain.Controls.Add(_PatientStrip)
        loadToolStrip()
    End Sub
    Private Sub UnDoChanges()
        Try
            If Not IsNothing(oCurDoc) Then
                oCurDoc.Undo()
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.TemplateAllocation, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub
    Private Sub ReDoChanges()
        Try
            If Not IsNothing(oCurDoc) Then
                oCurDoc.Redo()
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.TemplateAllocation, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub
    Private Sub ImportDocument(ByVal nInsertScan As Int16)
        'Insert File - 1
        'Scan Images - 2
        'Set focus to Wd object

        If Not IsNothing(oCurDoc) Then    ''''''This line is added by Anil on 04/10/2007, it is added because the application was giving error "Object Reference not set".

            oCurDoc.ActiveWindow.SetFocus()
            Try
                If nInsertScan = 1 Then
                    Dim oFileDialogWindow As New System.Windows.Forms.OpenFileDialog
                    oFileDialogWindow.Filter = "Text Files (*.txt)|*.txt|Word 97-2003 Documents (*.doc)|*.doc|Word Documents (*.docx)|*.docx|Rich Text Format (*.rtf)|*.rtf"
                    ' oFileDialogWindow.Filter = "Text Files|*.txt|Wd Documents|*.doc|Rich Text Format|*.rtf"
                    '//oFileDialogWindow.Filter = "Text Files (*.txt)|*.txt|Wd Documents (*.doc)|*.doc|Rich Text Format (*.rtf)|*.rtf"
                    oFileDialogWindow.FilterIndex = 3
                    oFileDialogWindow.Title = "Insert External Documents"
                    oFileDialogWindow.Multiselect = False
                    If oFileDialogWindow.ShowDialog(System.Windows.Forms.Form.ActiveForm) = Windows.Forms.DialogResult.OK Then
                        Dim oFile As FileInfo = New FileInfo(oFileDialogWindow.FileName)
                        If oFile.Extension.ToUpper = UCase(".Doc") Or oFile.Extension.ToUpper = UCase(".Docx") Or oFile.Extension.ToUpper = UCase(".txt") Or oFile.Extension.ToUpper = UCase(".rtf") Then

                            oCurDoc.Application.Selection.InsertFile(oFile.FullName)
                        End If
                    End If
                    oFileDialogWindow.Dispose()
                ElseIf nInsertScan = 2 Then
                    'Dim frmObj As New frmDMS_ScannedDocumentEvent_TwainPro
                    'Dim _Files As New Collection
                    'frmObj.blnDMSScan = False
                    'frmObj.ShowDialog(Me)
                    '_Files = frmObj._NonDMSFileCollection


                    Dim _Files As New Collection
                    Dim strFilePathe As String = ""

                    Dim oFiles As New ArrayList()
                    Dim oEDocument As New gloEDocumentV3.gloEDocV3Management()

                    'Commented BY Rahul Patel on 26-10-2010
                    'gloEDocumentV3.gloEDocV3Admin.Connect(GetConnectionString(), gDMSV2TempPath, Convert.ToInt64(gnLoginID), gClinicID, Application.StartupPath)
                    'Added by Rahul Patel on 26-10-2010
                    'For changing the DMS Hybrid database change.
                    gloEDocumentV3.gloEDocV3Admin.Connect(GetConnectionString(), GetDMSConnectionString(), gDMSV2TempPath, Convert.ToInt64(gnLoginID), gClinicID, Application.StartupPath)
                    'End of code added by Rahul Patel on 26-10-2010

                    oEDocument.ShowEScannerForImages(m_PatientID, oFiles)
                    oEDocument.Dispose()


                    Dim firstFlag As Boolean = True
                    Dim i As Integer
                    Dim myType As Wd.WdViewType = Nothing
                    Dim myLayout As Boolean = gloWord.LoadAndCloseWord.ChangeToEditView(oCurDoc, myType)
                    For i = 0 To oFiles.Count - 1
                        If File.Exists(oFiles.Item(i)) Then

                            '' SUDHIR 20090619 '' 
                            Dim oWord As New clsWordDocument
                            oWord.GetandSetMyFirstFlag(True, firstFlag)
                            oWord.CurDocument = oCurDoc
                            oWord.InsertImage(oFiles.Item(i))
                            firstFlag = oWord.GetandSetMyFirstFlag(False, False)
                            oWord = Nothing
                            'oCurDoc.Application.Selection.InlineShapes.AddPicture(FileName:=oFiles.Item(i), LinkToFile:=False, SaveWithDocument:=True)
                            '' END SUDHIR ''

                            oCurDoc.Application.Selection.EndKey()
                            oCurDoc.Application.Selection.InsertBreak()

                        End If
                    Next
                    gloWord.LoadAndCloseWord.RestoreFromEditView(oCurDoc, myType, myLayout)
                    oCurDoc.ActiveWindow.SetFocus()

                    For i = oFiles.Count - 1 To 0 Step -1
                        If File.Exists(oFiles.Item(i)) Then
                            Try
                                Kill(oFiles.Item(i))
                            Catch
                            End Try

                        End If
                    Next

                    ''frmObj = Nothing
                    i = Nothing
                    _Files = Nothing
                End If
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.TemplateAllocation, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                ex = Nothing
            Finally

            End Try
        End If
    End Sub

    Private Sub LoadWordUserControl(ByVal strFileName As String, Optional ByVal blnGetData As Boolean = False)
        'Dim wordOptimizer As New WordRefresh()
        Dim WDocViewType As Wd.WdViewType

        Try
            'wordOptimizer.ShowPanel(Me.pnl_wdFormGallery)
            gloWord.LoadAndCloseWord.OpenDSO(wdFormGallery, strFileName, oCurDoc, oWordApp)
            WDocViewType = oCurDoc.ActiveWindow.View.Type

            If blnGetData Then
                ''//To retrieve the Form fields for the Word document
                ObjWord = New clsWordDocument
                objCriteria = New DocCriteria
                objCriteria.DocCategory = enumDocCategory.Others
                objCriteria.PatientID = m_PatientID
                objCriteria.VisitID = m_VisitID
                objCriteria.PrimaryID = 0
                ObjWord.DocumentCriteria = objCriteria
                ObjWord.CurDocument = oCurDoc

                ''Replace Form fields with Concerned data

                'wordOptimizer.OptimizePerformance(False, oCurDoc, 0)
                ObjWord.GetFormFieldData(enumDocType.None)

                oCurDoc = ObjWord.CurDocument
                oCurDoc.ActiveWindow.View.ShowFieldCodes = False
                objCriteria.Dispose()
                objCriteria = Nothing
                ObjWord = Nothing
            Else
                ObjWord = New clsWordDocument
                ObjWord.CurDocument = oCurDoc
                ObjWord.HighlightColor()
                oCurDoc = ObjWord.CurDocument
                oCurDoc.ActiveWindow.View.ShowFieldCodes = False
                ObjWord = Nothing
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.TemplateAllocation, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing

        Finally
            'wordOptimizer.HidePanel(Me.pnl_wdFormGallery)
            If blnGetData Then
                '    wordOptimizer.OptimizePerformance(True, oCurDoc, WDocViewType)
            End If

            'wordOptimizer.Dispose()
            'wordOptimizer = Nothing
        End Try
    End Sub
    Private Sub loadToolStrip()
        If Not tlsFormGallery Is Nothing Then
            tlsFormGallery.Dispose()
        End If

        tlsFormGallery = New WordToolStrip.gloWordToolStrip
        tlsFormGallery.Dock = DockStyle.Top
        'code added by dipak 20091219 to set ConnectionString for customise
        tlsFormGallery.ConnectionString = GetConnectionString()
        tlsFormGallery.UserID = gnLoginID
        ''ADDED ON 20101007 BY SANJOG FOR SIGNATURE
        tlsFormGallery.dtInput = AddChildMenu()
        Dim oclsProvider As New clsProvider
        tlsFormGallery.ptProvider = oclsProvider.GetPatientProviderName(m_PatientID)
        tlsFormGallery.ptProviderId = oclsProvider.GetPatientProvider(m_PatientID)
        oclsProvider.Dispose()
        oclsProvider = Nothing
        ''ADDED ON 20101007 BY SANJOG FOR SIGNATURE

        tlsFormGallery.BringToFront()
        tlsFormGallery.IsCoSignEnabled = gblnCoSignFlag
        tlsFormGallery.FormType = WordToolStrip.enumControlType.FormGallery

        Me.pnlToolstripContainer.Controls.Add(tlsFormGallery)
        'Dim dt As DataTable
        'dt = oclsProvider.GetAllAssignProviders(gnLoginID)

        If gblnAssociatedProviderSignature Then
            tlsFormGallery.MyToolStrip.Items("Insert Associated Provider Signature").Visible = True
            'If dt.Rows.Count > 0 Then
            '    tlsFormGallery.MyToolStrip.Items("Insert Associated Provider Signature").Enabled = True
            'Else
            '    tlsFormGallery.MyToolStrip.Items("Insert Associated Provider Signature").Enabled = False
            'End If
            tlsFormGallery.MyToolStrip.ButtonsToHide.Remove(tlsFormGallery.MyToolStrip.Items("Insert Associated Provider Signature").Name)
        Else
            tlsFormGallery.MyToolStrip.Items("Insert Associated Provider Signature").Visible = False
            If (tlsFormGallery.MyToolStrip.ButtonsToHide.Contains(tlsFormGallery.MyToolStrip.Items("Insert Associated Provider Signature").Name) = False) Then
                tlsFormGallery.MyToolStrip.ButtonsToHide.Add(tlsFormGallery.MyToolStrip.Items("Insert Associated Provider Signature").Name)
            End If


        End If
        If gblnVoiceEnabled = True AndAlso gblnSpeakerExists = True Then
            If Not IsNothing(ogloVoice) Then
                ogloVoice.MyWordToolStrip = tlsFormGallery
                ShowMicroPhone()
            End If
        End If
        '''' Check Secure Messaging is enable and User has rights to access it
        If gblnIsSecureMsgEnable = False Or gblnSecureUserrights = False Then
            If tlsFormGallery.MyToolStrip.Items.ContainsKey("SecureMsg") = True Then
                If (tlsFormGallery.MyToolStrip.ButtonsToHide.Contains(tlsFormGallery.MyToolStrip.Items("SecureMsg").Name) = False) Then
                    tlsFormGallery.MyToolStrip.ButtonsToHide.Add(tlsFormGallery.MyToolStrip.Items("SecureMsg").Name)
                End If
            End If
            If tlsFormGallery.MyToolStrip.Items.ContainsKey("SecureMsg") = True Then
                tlsFormGallery.MyToolStrip.Items("SecureMsg").Visible = False
            End If
        End If
    End Sub
    ''ADDED ON 20101007 BY SANJOG FOR SIGNATURE
    Private Sub tlsFormGallery_ToolStripButtonClick(ByVal sender As Object, ByVal e As System.EventArgs, ByVal _Tag As String) Handles tlsFormGallery.ToolStripButtonClick
        If IsNothing(oCurDoc) = False Then
            InsertProviderSignature(gloGlobal.clsMISC.ConvertToLong(_Tag)) 'IIf(IsNumeric(_Tag), _Tag, 0))
        End If
    End Sub
    ''ADDED ON 20101007 BY SANJOG FOR SIGNATURE


    '''' <summary>
    '''' To implemt the Dropdown and check Box selection change event
    '''' </summary>
    '''' <param name="Sel"></param>
    '''' <remarks></remarks>
    Private Sub DDLCBEvent(ByVal Sel As Wd.Selection)

        Try
            If IsNothing(Sel) Then
                Return
            End If
            If (Sel.Type <> Microsoft.Office.Interop.Word.WdSelectionType.wdNoSelection) Then

                If Sel.Start = Sel.End Then

                    Dim r As Wd.Range = Nothing
                    Try
                        r = Sel.Range
                    Catch ex As Exception

                    End Try
                    If (IsNothing(r)) Then
                        Exit Sub
                    End If
                    Try
                        r.SetRange(Sel.Start, Sel.End + 1)
                    Catch ex As Exception

                    End Try
                    If (IsNothing(r)) Then
                        Exit Sub
                    End If
                    ' r.SetRange(Sel.Start, Sel.End + 1)

                    If r.FormFields IsNot Nothing AndAlso r.FormFields.Count >= 1 Then

                        ' Dim om As Object = System.Reflection.Missing.Value

                        Dim f As Wd.FormField = Nothing



                        Try
                            Dim o As Object = 1
                            f = r.FormFields.Item(o)
                            o = Nothing
                        Catch

                        End Try


                        'If f.Type = Wd.WdFieldType.wdFieldFormDropDown Then

                        '    Dim dd As Wd.DropDown = f.DropDown

                        '    Dim iCurSel As Integer = dd.Value

                        '    Dim oPU As oOffice.CommandBar = oWordApp.CommandBars.Add("CustomFormFieldPopup", oOffice.MsoBarPosition.msoBarPopup, om, True)

                        '    If False Then

                        '        Dim oDD As oOffice.CommandBarComboBox = CType(oPU.Controls.Add(oOffice.MsoControlType.msoControlDropdown, om, om, om, True), oOffice.CommandBarComboBox)

                        '        oDD.Style = oOffice.MsoComboStyle.msoComboLabel

                        '        oDD.DropDownLines = dd.ListEntries.Count

                        '        Dim le As Wd.ListEntry
                        '        For Each le In dd.ListEntries

                        '            oDD.AddItem(le.Name, om)

                        '        Next

                        '        oDD.ListIndex = iCurSel

                        '        CType(oPU, oOffice.CommandBar).ShowPopup(om, om)

                        '        dd.Value = oDD.ListIndex

                        '    Else

                        '        myidx = dd.Value

                        '        Dim iter As Integer = 1

                        '        Dim le As Wd.ListEntry
                        '        For Each le In dd.ListEntries

                        '            Dim btn As oOffice.CommandBarButton
                        '            '     Dim oDD As oOffice.CommandBarComboBox = CType(oPU.Controls.Add(oOffice.MsoControlType.msoControlButton, om, om, om, True), oOffice.CommandBarButton)

                        '            btn = CType(oPU.Controls.Add(oOffice.MsoControlType.msoControlButton, om, om, om, True), oOffice.CommandBarButton)

                        '            btn.Style = oOffice.MsoButtonStyle.msoButtonAutomatic

                        '            btn.Caption = le.Name

                        '            btn.Enabled = True

                        '            If iter = myidx Then

                        '                btn.State = oOffice.MsoButtonState.msoButtonDown
                        '            End If

                        '            iter = iter + 1

                        '            ' btn.Click += New Microsoft.Office.Core._CommandBarButtonEvents_ClickEventHandler(btn_Click)
                        '            AddHandler btn.Click, AddressOf btn_Click
                        '        Next

                        '        CType(oPU, oOffice.CommandBar).ShowPopup(om, om)

                        '        dd.Value = myidx

                        '    End If

                        'End If

                        If (IsNothing(f) = False) Then


                            If f.Type = Wd.WdFieldType.wdFieldFormCheckBox Then

                                f.CheckBox.Value = Not f.CheckBox.Value

                                Dim oUnit As Object = Wd.WdUnits.wdCharacter

                                Dim oCnt As Object = 1

                                Dim oMove As Object = Wd.WdMovementType.wdMove

                                Sel.MoveRight(oUnit, oCnt, oMove)

                            End If

                        End If
                    End If
                End If
            End If

        Catch excp As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.TemplateAllocation, gloAuditTrail.ActivityType.General, excp.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            excp = Nothing
        End Try

    End Sub

    ''' <summary>
    ''' To raise the click event for drop down list
    ''' </summary>
    ''' <param name="btn"></param>
    ''' <param name="Cancel"></param>
    ''' <remarks></remarks>
    Private Sub btn_Click(ByVal btn As oOffice.CommandBarButton, ByRef Cancel As Boolean)
        myidx = btn.Index
    End Sub

    Private Sub RemoveWordHandler()

        Try
            If (IsNothing(oWordApp) = False) Then
                RemoveHandler oWordApp.WindowSelectionChange, AddressOf DDLCBEvent
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.TemplateAllocation, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Private Sub wdFormGallery_BeforeDocumentClosed(ByVal sender As Object, ByVal e As AxDSOFramer._DFramerCtlEvents_BeforeDocumentClosedEvent) Handles wdFormGallery.BeforeDocumentClosed
        Try
            If Not oWordApp Is Nothing Then
                'RemoveHandler oWordApp.WindowSelectionChange, AddressOf DDLCBEvent
                RemoveWordHandler()
                frmPatientExam.blnIsHandlers = True
                isHandlerRemoved = True

                For Each oFile As Wd.RecentFile In oWordApp.RecentFiles
                    If (IsNothing(oFile) = False) Then
                        Try
                            If oFile.Path = gloSettings.FolderSettings.AppTempFolderPath Then
                                Try
                                    oFile.Delete()
                                Catch ex As Exception
                                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.TemplateAllocation, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                                    'UpdateVoiceLog(ex.ToString)
                                    ex = Nothing
                                End Try
                            End If
                        Catch ex As Exception

                        End Try
                    End If
                Next
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.TemplateAllocation, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            'UpdateVoiceLog(ex.ToString)
            ex = Nothing
        End Try
    End Sub

    Private Sub wdFormGallery_OnDocumentClosed(ByVal sender As Object, ByVal e As System.EventArgs) Handles wdFormGallery.OnDocumentClosed
        Try

            If Not oCurDoc Is Nothing Then
                Marshal.ReleaseComObject(oCurDoc)
                oCurDoc = Nothing
            End If
            'If Not oWordApp Is Nothing Then
            '    ' Marshal.FinalReleaseComObject(oWordApp)
            '    oWordApp = Nothing
            'End If
            'Clean obj of AddRefresh Control
            Clear_AddRefreshButtonControl()

            'GC.Collect()
            'GC.WaitForPendingFinalizers()
            'GC.Collect()
            'GC.WaitForPendingFinalizers()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.TemplateAllocation, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            'UpdateVoiceLog(ex.ToString)
            ex = Nothing
        End Try
    End Sub


    Private Sub wdFormGallery_OnDocumentOpened(ByVal sender As Object, ByVal e As AxDSOFramer._DFramerCtlEvents_OnDocumentOpenedEvent) Handles wdFormGallery.OnDocumentOpened
        oCurDoc = wdFormGallery.ActiveDocument
        oWordApp = oCurDoc.Application

        Try

            'RemoveHandler oWordApp.WindowSelectionChange, AddressOf DDLCBEvent
            RemoveWordHandler()
            AddHandler oWordApp.WindowSelectionChange, AddressOf DDLCBEvent
            isHandlerRemoved = False
            ' Set obj AddRefreshButtonControl
            GloUC_AddRefreshDic1.Visible = True
            calltoAddRefreshButtonControl()

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.TemplateAllocation, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
        oCurDoc.ActiveWindow.SetFocus()
        oCurDoc.FormFields.Shaded = False
    End Sub

    Private Sub tlsFormGallery_ToolStripClick(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tlsFormGallery.ToolStripClick
        Try


            ''******Shweta 20090828 *********'
            ''To check exeception related to word
            'If CheckWordForException() = False Then
            '    Exit Sub
            'End If
            ''End Shweta


            Select Case e.ClickedItem.Name
                Case "Mic"
                    gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Formgallery, gloAuditTrail.ActivityCategory.VoiceCommands, gloAuditTrail.ActivityType.Load, "SwitchOff Mic started from tblButtons_ButtonClick in PatientConsent when " & e.ClickedItem.Name & " is invoked", gloAuditTrail.ActivityOutCome.Success)
                    'UpdateVoiceLog("--------------SwitchOff Mic started from tblButtons_ButtonClick in Patient Messages when " & e.ClickedItem.Name & " is invoked------------")
                    If MyMDIParent.DgnMicBtn1.MicState = DNSTools.DgnMicStateConstants.dgnmicOn Then
                        MyMDIParent.DgnMicBtn1.MicState = DNSTools.DgnMicStateConstants.dgnmicOff
                        e.ClickedItem.Image = Global.gloEMR.My.Resources.Mic_OFF
                        e.ClickedItem.ToolTipText = "Microphone Off"
                    ElseIf MyMDIParent.DgnMicBtn1.MicState = DNSTools.DgnMicStateConstants.dgnmicOff Then
                        MyMDIParent.DgnMicBtn1.MicState = DNSTools.DgnMicStateConstants.dgnmicOn
                        e.ClickedItem.Image = Global.gloEMR.My.Resources.Mic_ON
                        e.ClickedItem.ToolTipText = "Microphone On"
                    End If
                    gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Formgallery, gloAuditTrail.ActivityCategory.VoiceCommands, gloAuditTrail.ActivityType.Load, "SwitchOff Mic Completed from tblButtons_ButtonClick in PatientConsent when " & e.ClickedItem.Name & " is invoked", gloAuditTrail.ActivityOutCome.Success)
                Case "Save"
                    TurnoffMicrophone()

                    Call SaveFormGallery()

                    CancelClick = False
                Case "Save & Close"
                    TurnoffMicrophone()
                    Call SaveFormGallery()
                    CancelClick = False
                    Me.Close()
                Case "Print"
                    TurnoffMicrophone()
                    Call Print()
                Case "FAX"
                    bnlIsFaxOpened = True
                    Try
                        Me.Cursor = Cursors.WaitCursor
                        TurnoffMicrophone()
                        Call GeneratePrintFaxDocument(False)
                        Me.Cursor = Cursors.Default
                    Catch ex As Exception
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.TemplateAllocation, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                        ex = Nothing
                        Me.Cursor = Cursors.Default
                    End Try
                    bnlIsFaxOpened = False
                Case "Insert Sign"
                    'Call InsertProviderSignature()
                    If IsNothing(oCurDoc) = False Then
                        blnSignClick = True
                        If gnLoginProviderID > 0 Then
                            InsertProviderSignature(gnLoginProviderID)
                        Else
                            InsertUserSignature()
                        End If
                        blnSignClick = False
                        'end code added by dipak 20100105
                    End If
                    'case added by dipak 20100105 for ProviderSign 
                Case "Insert Associated Provider Signature"
                    If IsNothing(oCurDoc) = False Then
                        InsertProviderSignature()
                    End If
                Case "Insert CoSign"
                    Call InsertCoSignature()
                Case "Capture Sign"
                    Call InsertSignature()
                Case "Undo"
                    Call UnDoChanges()
                Case "Redo"
                    Call ReDoChanges()
                Case "Insert File"
                    TurnoffMicrophone()
                    ImportDocument(1)
                Case "Scan Documents"
                    TurnoffMicrophone()
                    ImportDocument(2)
                Case "Close"
                    Me.Close()
                Case "PrintAll"
                    TurnoffMicrophone()
                    'SaveFormGallery()
                    Call PrintAll()
                Case "FaxAll"
                    TurnoffMicrophone()
                    'SaveFormGallery()
                    Call FAXAll()
                Case "Export"
                    ' Export Function for Word Docs Integrated by Dipak  as on 26 oct 2010
                    Dim objword1 As clsWordDocument
                    objword1 = New clsWordDocument
                    Dim Result As Boolean = objword1.ExportData(oCurDoc, "", True, "Form Gallery", Me)
                    If Result = True Then
                        MessageBox.Show("Document Exported Successfully.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                    objword1 = Nothing
                    '' Export Function for Word Docs Integrated by dipak  as on 26 oct 2010


                Case "tblbtn_StrikeThrough"
                    '' chetan added on 25-oct-2010 for Strike Through
                    InsertStrike()
                Case "SecureMsg"
                    If strProviderDirectAddress <> "" OrElse gloSurescriptSecureMessage.SecureMessageProperties.ListUserProviderAssociation IsNot Nothing Then
                        Dim sError As String = gloSurescriptSecureMessage.SecureMessage.ValidateZipCode(m_PatientID)
                        If sError <> "" Then
                            MessageBox.Show(sError, gloSurescriptSecureMessage.SecureMessageProperties.MessageBoxCaption, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information)
                            Return
                        Else
                            TurnoffMicrophone()
                            Call SendSecureMsg()
                        End If

                    Else
                        MessageBox.Show(gstrDirectWarningMessage, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
            End Select
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.TemplateAllocation, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            ex = Nothing
        End Try
    End Sub

    Private Sub SendSecureMsg()

        If Not oCurDoc Is Nothing Then
            GenerateSecureMsgDocument()

            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.TemplateAllocation, gloAuditTrail.ActivityType.ClinicalExchange, "Send Form Gallery using Secure Message", m_PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            ''

        End If

    End Sub
    Private Sub GenerateSecureMsgDocument()
        Dim _SaveFlag As Boolean = False
        If oCurDoc.Saved Then
            _SaveFlag = True
        End If
        Try
            gloWord.LoadAndCloseWord.SaveDSO(wdFormGallery, oCurDoc, oWordApp)
        Catch ex As Exception

        End Try

        'Try
        '    oCurDoc.SaveAs(oCurDoc.FullName)
        'Catch ex As Exception
        '    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.TemplateAllocation, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        '    Try
        '        oCurDoc.Save()
        '    Catch ex1 As Exception

        '        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.TemplateAllocation, gloAuditTrail.ActivityType.Print, ex1.ToString(), gloAuditTrail.ActivityOutCome.Failure)

        '    End Try
        'End Try
        '   Dim sFileName As String = ExamNewDocumentName

        '  wdPTProtocols.Save(sFileName, True, "", "")
        'oCurDoc.SaveAs(sFileName, Wd.WdSaveFormat.wdFormatXMLDocument, False, "", False)

        'wdFormGallery.Close()

        'wdTemp = New AxDSOFramer.AxFramerControl

        'Me.Controls.Add(wdTemp)


        'wdTemp.Open(sFileName)
        'oTempDoc = wdTemp.ActiveDocument
        'oTempDoc.ActiveWindow.SetFocus()
        Dim myLoadWord As gloWord.LoadAndCloseWord = New gloWord.LoadAndCloseWord()
        Dim osenddox As String = String.Empty
        Try
            osenddox = SendWord.MdlSendWord.SendWordDocument(myLoadWord, oCurDoc.FullName, m_PatientID)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.TemplateAllocation, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try

        'Dim oTempDoc As Wd.Document = myLoadWord.LoadWordApplication(sFileName)

        'If oTempDoc.ProtectionType = Wd.WdProtectionType.wdAllowOnlyComments Then
        '    oTempDoc.Unprotect()
        'End If

        'Dim oSendDoc As New clsPrintFAX

        'osenddox = oSendDoc.SendDoc(oTempDoc, m_PatientID)
        'oSendDoc.Dispose()
        'oSendDoc = Nothing
        'wdTemp.Close()
        'Me.Controls.Remove(wdTemp)
        'wdTemp.Dispose()
        'wdTemp = Nothing
        myLoadWord.CloseApplicationOnly()
        myLoadWord = Nothing
        oCurDoc.Saved = _SaveFlag

        ''Read Secure Messages settings and call Inbox form
        If (osenddox.Length > 0) Then
            If File.Exists(osenddox) Then
                Dim ofrmSendNewMail As New InBox.NewMail(m_PatientID, osenddox)
                AddHandler ofrmSendNewMail.EvntGenerateCDA, AddressOf Raise_EvntGenerateCDAFromCPTTemplate
                If gloSurescriptSecureMessage.SecureMessageProperties.ListUserProviderAssociation IsNot Nothing Then
                    gloSurescriptSecureMessage.SecureMessage.SetPreferredProvider(gloEMR.gnPatientProviderID)
                    ofrmSendNewMail.ListOfProviders = gloSurescriptSecureMessage.SecureMessageProperties.ListUserProviderAssociation
                End If
                ofrmSendNewMail.ShowInTaskbar = True
                ofrmSendNewMail.ShowDialog()
                'ofrmInbox.Dispose()
                RemoveHandler ofrmSendNewMail.EvntGenerateCDA, AddressOf Raise_EvntGenerateCDAFromCPTTemplate
                ofrmSendNewMail.Close()
                ofrmSendNewMail = Nothing
            Else
                MessageBox.Show("Error While generating attachment. Please try again", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Else
            MessageBox.Show("Error While generating attachment. Please try again", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
        'LoadWordUserControl(sFileName, False)
        'oCurDoc.ActiveWindow.View.ShowFieldCodes = False
        'Set the Start postion of the cursor in documents
        'oCurDoc.Application.Selection.HomeKey(Wd.WdUnits.wdStory)
        'oCurDoc.Saved = _SaveFlag
    End Sub
    '' chetan added on 25-oct-2010 for Strike Through
    Private Sub InsertStrike()
        Try
            Dim strThrough As String
            If Not IsNothing(oCurDoc) Then
                If Not IsNothing(oCurDoc.Application.Selection) Then
                    If oCurDoc.Application.Selection.Characters.Count - 1 > 0 Then
                        strThrough = "Strikethrough by " & gstrLoginName & " on " & Format(Now, "MM/dd/yyyy") & " " & Format(Now, "Medium Time")

                        If oCurDoc.Application.ActiveDocument.ProtectionType = Microsoft.Office.Interop.Word.WdProtectionType.wdAllowOnlyComments Then
                            oCurDoc.Application.ActiveDocument.Unprotect()
                        End If
                        oCurDoc.Application.Selection.Range.Font.DoubleStrikeThrough = True
                        oCurDoc.Application.Selection.Move(1)
                        oCurDoc.Application.Selection.TypeParagraph()
                        oCurDoc.Application.Selection.Font.DoubleStrikeThrough = False
                        oCurDoc.Application.Selection.TypeText(Text:=strThrough)
                        oCurDoc.Application.Selection.Move(1)
                        oCurDoc.Application.Selection.TypeParagraph()

                        ''SANJOG-ADDED ON 20101123 TO MAKE UNDO CHANGES IN DOCUMENT AFTER STRIKE THROUGH
                        oCurDoc.Application.ActiveDocument.Unprotect(Wd.WdProtectionType.wdAllowOnlyComments)
                        ''SANJOG-ADDED ON 20101123 TO MAKE UNDO CHANGES IN DOCUMENT AFTER STRIKE THROUGH

                    End If
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.TemplateAllocation, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing

        End Try
    End Sub
    Private Sub rbSearch1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbSearch1.Click

        Dim i As Integer
        'Dim j As Integer
        If (IsNothing(objclsCPTAssociation) = False) Then
            objclsCPTAssociation.Dispose()
            objclsCPTAssociation = Nothing
        End If
        objclsCPTAssociation = New clsCPTAssociation
        If (IsNothing(dtCPTCode) = False) Then
            dtCPTCode.Dispose()
            dtCPTCode = Nothing
        End If
        dtCPTCode = objclsCPTAssociation.GetAssociatedCPT(0)
        trvCptAssocation.Nodes.Clear()
        For i = 0 To dtCPTCode.Rows.Count - 1
            Dim CPTNode As New TreeNode
            CPTNode.Tag = CType(dtCPTCode.Rows(i)(0).ToString, Long)
            CPTNode.Text = CType(dtCPTCode.Rows(i)(2).ToString, String)
            CPTNode.ImageIndex = 1
            CPTNode.SelectedImageIndex = 1
            trvCptAssocation.Nodes.Add(CPTNode)
        Next
        txtSearch.Text = ""
        txtSearch.Focus()
    End Sub

    Private Sub rbSearch2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbSearch2.Click

        Dim i As Integer
        'Dim j As Integer
        If (IsNothing(objclsCPTAssociation) = False) Then
            objclsCPTAssociation.Dispose()
            objclsCPTAssociation = Nothing
        End If
        objclsCPTAssociation = New clsCPTAssociation
        If (IsNothing(dtCPTdesc) = False) Then
            dtCPTdesc.Dispose()
            dtCPTdesc = Nothing
        End If
        dtCPTdesc = objclsCPTAssociation.GetAssociatedCPT(1)
        trvCptAssocation.Nodes.Clear()
        For i = 0 To dtCPTdesc.Rows.Count - 1
            Dim CPTNode As New TreeNode
            CPTNode.Tag = CType(dtCPTdesc.Rows(i)(0).ToString, Long)
            CPTNode.Text = CType(dtCPTdesc.Rows(i)(2).ToString, String)
            CPTNode.ImageIndex = 1
            CPTNode.SelectedImageIndex = 1
            trvCptAssocation.Nodes.Add(CPTNode)
        Next
        txtSearch.Text = ""
        txtSearch.Focus()
    End Sub
    Private Sub AddCPT(ByVal strsearch As String, ByVal dt As DataTable)
        Try
            Dim i As Integer
            Dim tdt As DataTable
            'For i = 0 To dt.Rows.Count - 1
            Dim dv As New DataView(dt)
            'CPT_MST.nCPTID as CPTID,CPT_MST.sDescription AS sDescription, CPT_MST.sCPTCode + '-' + CPT_MST.sDescription, sCPTCode AS CPTCode     
            If rbSearch2.Checked = True Then
                ''description 
                dv.RowFilter = "sDescription Like '%" & strsearch & "%'"
            Else
                ''code
                dv.RowFilter = "CPTCode Like '%" & strsearch & "%'"
            End If
            tdt = New DataTable
            tdt = dv.ToTable

            'add the nodes to treenode
            trvCptAssocation.BeginUpdate()
            trvCptAssocation.Visible = False
            'If trvCptAssocation.GetNodeCount(False) > 0 Then
            '    trvCptAssocation.Nodes.Item(0).Remove()
            '    Dim rootnode As TreeNode
            '    rootnode = New myTreeNode("CPT", -1)
            '    rootnode.ImageIndex = 1
            '    rootnode.SelectedImageIndex = 1
            '    trvCptAssocation.Nodes.Add(rootnode)
            'End If

            'fill the treeview with the dv
            trvCptAssocation.Nodes.Clear()
            If Not tdt Is Nothing Then

                For i = 0 To tdt.Rows.Count - 1
                    Dim CPTNode As New TreeNode
                    CPTNode.Tag = CType(tdt.Rows(i)(0).ToString, Long)
                    CPTNode.Text = CType(tdt.Rows(i)(2).ToString, String)
                    CPTNode.ImageIndex = 1
                    CPTNode.SelectedImageIndex = 1
                    trvCptAssocation.Nodes.Add(CPTNode)
                Next
                ' If tdt.Rows.Count > 0 Then
                trvCptAssocation.Visible = True
                'trvCptAssocation.SelectedNode = trvCptAssocation.Nodes.Item(0)
                '  End If
            Else
            End If
            trvCptAssocation.ExpandAll()
        Catch ex As Exception
            'Dim objex As New gloUserControlLibrary.gloUserControlExceptions
            'objex.ErrorMessage = ""
            'Throw objex
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.TemplateAllocation, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            ex = Nothing
        Finally
            txtSearch.Focus()
            trvCptAssocation.EndUpdate()
        End Try

    End Sub
    '''''''**** This code is added by Anil on 04/10/2007 at 04:10 p.m.
    '''''''**** This code is for context menu "Remove Template" in CPTTemplate Grid Should not pop up at blank spaces and also if the row is not selected.
    '''''''**** This the solution for Bug ID - 180
    Private Sub cntFormGallery_Popup(ByVal sender As Object, ByVal e As System.EventArgs) Handles cntFormGallery.Popup
        Try
            If tblTemplate.Rows.Count > 0 Then
                If grdTemplateGallery.IsSelected(grdTemplateGallery.CurrentRowIndex) = False Then
                    mnuRemoveForm.Visible = False
                    Exit Sub
                Else
                    mnuRemoveForm.Visible = True
                End If
            Else
                mnuRemoveForm.Visible = False
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.TemplateAllocation, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub
    '********************Ojeswini_18Dec2008*******************************************
    Private Sub rbSearch1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbSearch1.CheckedChanged
        If rbSearch1.Checked = True Then
            rbSearch1.Font = gloGlobal.clsgloFont.gFont_BOLD 'New Font("Tahoma", 9, FontStyle.Bold)

        Else
            rbSearch1.Font = gloGlobal.clsgloFont.gFont 'New Font("Tahoma", 9, FontStyle.Regular)

        End If
    End Sub

    Private Sub rbSearch2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbSearch2.CheckedChanged
        If rbSearch2.Checked = True Then
            rbSearch2.Font = gloGlobal.clsgloFont.gFont_BOLD 'New Font("Tahoma", 9, FontStyle.Bold)

        Else
            rbSearch2.Font = gloGlobal.clsgloFont.gFont ' New Font("Tahoma", 9, FontStyle.Regular)

        End If
    End Sub
    '********************Ojeswini_18Dec2008*******************************************

    Private Sub GloUC_trvCPT_NodeMouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles GloUC_trvCPT.NodeMouseDoubleClick
        Try

            If IsNothing(GloUC_trvCPT.SelectedNode.Parent) Then
                Exit Sub
            End If

            Dim oNode As gloUserControlLibrary.myTreeNode = CType(GloUC_trvCPT.SelectedNode, gloUserControlLibrary.myTreeNode)
            Dim r As DataRow

            r = tblTemplate.NewRow()

            Dim uniqueFormID As Long = Convert.ToInt64(getUniqueID())

            If hashNewTemplates Is Nothing Then
                hashNewTemplates = New HashSet(Of Long)
            End If

            If Not hashNewTemplates.Contains(uniqueFormID) Then
                hashNewTemplates.Add(uniqueFormID)
            End If


            r("CPTID") = Convert.ToInt64(oNode.Tag) '--0
            r("CPT") = oNode.Parent.Text    '--1
            'r("TemplateID") = Convert.ToInt64(oNode.Name)
            r("TemplateID") = Convert.ToInt64(oNode.ID)
            r("Template") = oNode.Text
            r("FormID") = uniqueFormID

            Dim i As Integer
            'Dim lst As New myList

            '''''  if there no rows in tblTemplate table then add Template to tblTemplate 
            If tblTemplate.Rows.Count = 0 Then
                tblTemplate.Rows.Add(r)
                grdTemplateGallery.DataSource = tblTemplate
                Exit Sub
            End If

            'For i = 0 To tblTemplate.Rows.Count - 1
            '    ''''' check if Template For that CPT is already Exists 
            '    If tblTemplate.Rows(i)(0) = Convert.ToString(oNode.Parent.Tag) And tblTemplate.Rows(i)(2) = oNode.Name Then
            '        ''''' if Template exists then exit
            '        Exit Sub
            '    End If
            'Next

            tblTemplate.Rows.Add(r)
            grdTemplateGallery.DataSource = tblTemplate
            CustomGridStyle()
            If ArrLst.Count > 0 Then
                For i = 0 To ArrLst.Count - 1
                    If grdTemplateGallery.Item(i, 4) = m_FormID Then
                        grdTemplateGallery.Select(i)
                    End If
                Next
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.TemplateAllocation, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            ex = Nothing
        End Try
    End Sub

    Private Sub GloUC_trvCPT_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles GloUC_trvCPT.KeyPress

        Try
            If Not IsNothing(GloUC_trvCPT.SelectedNode) Then ''added for bugid 92594
                If IsNothing(GloUC_trvCPT.SelectedNode.Parent) Then
                    Exit Sub
                End If

                ''    Dim oNodeParent As gloUserControlLibrary.myTreeNode = CType(GloUC_trvCPT.SelectedNode.Parent, gloUserControlLibrary.myTreeNode)
                Dim oNode As gloUserControlLibrary.myTreeNode = CType(GloUC_trvCPT.SelectedNode, gloUserControlLibrary.myTreeNode)
                Dim r As DataRow

                r = tblTemplate.NewRow()

                ''changes made for  Bug #81416
                Dim uniqueFormID As Long = Convert.ToInt64(getUniqueID())
                If hashNewTemplates Is Nothing Then
                    hashNewTemplates = New HashSet(Of Long)
                End If

                If Not hashNewTemplates.Contains(uniqueFormID) Then
                    hashNewTemplates.Add(uniqueFormID)
                End If
                r("CPTID") = oNode.Tag   '--0  
                r("CPT") = oNode.Parent.Text      '--1
                r("TemplateID") = Convert.ToInt64(oNode.ID)
                r("Template") = oNode.Text
                r("FormID") = uniqueFormID
                Dim i As Integer
                'Dim lst As New myList

                '''''  if there no rows in tblTemplate table then add Template to tblTemplate 
                If tblTemplate.Rows.Count = 0 Then
                    tblTemplate.Rows.Add(r)
                    grdTemplateGallery.DataSource = tblTemplate
                    Exit Sub
                End If

                For i = 0 To tblTemplate.Rows.Count - 1
                    ''''' check if Template For that CPT is already Exists 
                    If tblTemplate.Rows(i)(0) = Convert.ToString(oNode.ID) AndAlso tblTemplate.Rows(i)(2) = oNode.Name Then
                        ''''' if Template exists then exit
                        Exit Sub
                    End If
                Next

                tblTemplate.Rows.Add(r)
                grdTemplateGallery.DataSource = tblTemplate
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.TemplateAllocation, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            ex = Nothing
        End Try
    End Sub

    Private Sub GloUC_trvCPT_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles GloUC_trvCPT.AfterSelect
        Try
            If IsNothing(GloUC_trvCPT.SelectedNode) Then
                Exit Sub
            End If
            If IsNothing(GloUC_trvCPT.SelectedNode.Parent) Then
                'Dim oNode As gloUserControlLibrary.myTreeNode = CType(GloUC_trvCPT.SelectedNode, gloUserControlLibrary.myTreeNode)
                'Dim dtTemplate As New DataTable
                'Dim j As Integer
                'dtTemplate = objclsCPTAssociation.GetAccociatedTemplates(oNode.ID)
                'For j = 0 To GloUC_trvCPT.SelectedNode.Nodes.Count - 1
                '    GloUC_trvCPT.SelectedNode.Nodes.Clear()
                'Next
                'Dim ParentNode As gloUserControlLibrary.myTreeNode

                'For j = 0 To dtTemplate.Rows.Count - 1

                '    Dim TemplateNode As New gloUserControlLibrary.myTreeNode
                '    TemplateNode.Tag = CType(dtTemplate.Rows(j)(0).ToString, Long)
                '    TemplateNode.Text = CType(dtTemplate.Rows(j)(1).ToString, String)
                '    GloUC_trvCPT.SelectedNode.Nodes.Add(Convert.ToString(TemplateNode.Tag), TemplateNode.Text, 0, 0)

                'Next
                Dim ParentNode As gloUserControlLibrary.myTreeNode
                GloUC_trvCPT.SelectedNode.ExpandAll()
                For Each ParentNode In GloUC_trvCPT.Nodes
                    If (ParentNode.Text <> GloUC_trvCPT.SelectedNode.Text) Then
                        ParentNode.Collapse()
                    End If
                Next
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.TemplateAllocation, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            ex = Nothing
        End Try
    End Sub

    Public Sub ActivateBasicVoiceCmds(ByVal VoiceCol As DNSTools.DgnStrings) Implements mdlgloVoice.gloVoice.ActivateBasicVoiceCmds

    End Sub
    ''' <summary>
    ''' Trigger Voice commands
    ''' </summary>
    ''' <param name="VoiceCol"></param>
    ''' <remarks></remarks>
    Public Sub ActivateVoiceCmds(ByVal VoiceCol As DNSTools.DgnStrings) Implements mdlgloVoice.gloVoice.ActivateVoiceCmds
        If gblnVoiceEnabled = True AndAlso gblnSpeakerExists = True Then
            If Not IsNothing(ogloVoice) Then
                ogloVoice.ActivateVoiceCmds(VoiceCol)
            End If
        End If
    End Sub
    ''' <summary>
    ''' Add voice commands from custom collection to DgnStrings
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub AddVoiceCommands() Implements mdlgloVoice.gloVoice.AddVoiceCommands
        If gblnVoiceEnabled = True AndAlso gblnSpeakerExists = True Then
            If Not IsNothing(ogloVoice) Then
                ogloVoice.AddVoiceCommands()
            End If
        End If
    End Sub

    Public Sub CustomGetchanges(ByVal e As AxDNSTools._DDgnDictCustomEvents_GetChangesEvent) Implements mdlgloVoice.gloVoice.CustomGetchanges

    End Sub

    Public Sub CustomMakechanges(ByVal e As AxDNSTools._DDgnDictCustomEvents_MakeChangesEvent) Implements mdlgloVoice.gloVoice.CustomMakechanges

    End Sub
    ''' <summary>
    ''' Show microphone
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ShowMicroPhone() Implements mdlgloVoice.gloVoice.ShowMicroPhone
        If gblnVoiceEnabled = True AndAlso gblnSpeakerExists = True Then
            If Not IsNothing(ogloVoice) Then
                ogloVoice.ShowMicroPhone()
            End If
        End If
    End Sub
    ''' <summary>
    ''' Turnoff microphone
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub TurnoffMicrophone() Implements mdlgloVoice.gloVoice.TurnoffMicrophone
        If gblnVoiceEnabled = True AndAlso gblnSpeakerExists = True Then
            If Not IsNothing(ogloVoice) Then
                ogloVoice.TurnoffMicrophone()
            End If

        End If
    End Sub
    ''' <summary>
    ''' Initialise glovoice class
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InitializeVoiceObject()
        If Not IsNothing(ogloVoice) Then
            ogloVoice.Dispose()
            ogloVoice = Nothing
        End If
        Dim oHashtable As Hashtable = AddBasicVoiceCommands()
        ogloVoice = New ClsVoice(oHashtable)
        ogloVoice.eActivityModule = gloAuditTrail.ActivityModule.Formgallery
        ogloVoice.MyWordToolStrip = Me.tlsFormGallery
        ogloVoice.MDIParentVoice = MyMDIParent
        ogloVoice.MessageName = "Form Gallery"

        ogloVoice.InitializeVoiceComponents()
        ogloVoice.DelWordToolStripClick = New HandleWordToolStripClick(AddressOf tlsFormGallery_ToolStripClick)
    End Sub
    ''' <summary>
    ''' Add Basic Voice commands to hashtable
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function AddBasicVoiceCommands() As Hashtable

        Dim oHashtable As New Hashtable
        oHashtable.Clear()
        oHashtable.Add("Save Form Gallery", "Save")
        oHashtable.Add("Print Form Gallery", "Print")
        oHashtable.Add("Fax Form Gallery", "FAX")
        oHashtable.Add("Save and Close", "Save & Close")
        oHashtable.Add("Save and Close Form Gallery", "Save & Close")
        oHashtable.Add("Insert Signature", "Insert Sign")
        oHashtable.Add("Close Form Gallery", "Close")
        oHashtable.Add("Finish Form Gallery", "Save & Finish")
        Return oHashtable
    End Function
    Public ReadOnly Property MyParent() As MainMenu Implements mdlgloVoice.gloVoice.MyParent
        Get
            Return MyMDIParent
        End Get
    End Property
    ''' <summary>
    ''' Property writen for Patient Context
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property GetCurrentPatientID() As Int64 Implements mdlGeneral.IPatientContext.GetCurrentPatientID
        Get
            Return m_PatientID   'Curent patient variable(Local variable) for this module 
        End Get
    End Property

    Public Sub calltoAddRefreshButtonControl()
        Try
            ObjWord = New clsWordDocument
            ObjWord.WaitControlPanel = Me.pnl_wdFormGallery
            objCriteria = New DocCriteria
            Dim dtDate As New DateTimePicker()
            dtDate.Format = DateTimePickerFormat.Custom
            dtDate.CustomFormat = Format("MM/dd/yyyy hh:mm tt")
            dtDate.Value = Now
            objCriteria.PatientID = m_PatientID
            objCriteria.VisitID = GenerateVisitID(dtDate.Value, m_PatientID)

            GloUC_AddRefreshDic1.CONNECTIONSTRINGs = GetConnectionString()
            GloUC_AddRefreshDic1.OBJWORDs = ObjWord
            If (IsNothing(GloUC_AddRefreshDic1.OBJCRITERIAs) = False) Then
                Try
                    DirectCast(GloUC_AddRefreshDic1.OBJCRITERIAs, DocCriteria).Dispose()

                Catch

                End Try
                GloUC_AddRefreshDic1.OBJCRITERIAs = Nothing
            End If
            GloUC_AddRefreshDic1.OBJCRITERIAs = objCriteria
            GloUC_AddRefreshDic1.M_PATIENTIDs = m_PatientID
            GloUC_AddRefreshDic1.ObjFrom = Me
            GloUC_AddRefreshDic1.OWORDAPPs = oWordApp
            GloUC_AddRefreshDic1.wdPatientWordDocs = wdFormGallery
            If (GloUC_AddRefreshDic1.dtLetterAllocated) Then
                Try
                    If (IsNothing(GloUC_AddRefreshDic1.DTLETTERDATEs) = False) Then
                        Try
                            gloGlobal.cEventHelper.RemoveAllEventHandlers(GloUC_AddRefreshDic1.DTLETTERDATEs)
                        Catch ex As Exception

                        End Try


                        GloUC_AddRefreshDic1.DTLETTERDATEs.Dispose()
                        GloUC_AddRefreshDic1.DTLETTERDATEs = Nothing

                    End If
                Catch
                End Try
                GloUC_AddRefreshDic1.dtLetterAllocated = False
            End If

            GloUC_AddRefreshDic1.DTLETTERDATEs = dtDate
            GloUC_AddRefreshDic1.dtLetterAllocated = True
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Letters, gloAuditTrail.ActivityCategory.TemplateAllocation, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub


    Public Sub Clear_AddRefreshButtonControl()
        Try

            GloUC_AddRefreshDic1.OCURDOCs = Nothing
            GloUC_AddRefreshDic1.CONNECTIONSTRINGs = Nothing
            GloUC_AddRefreshDic1.OBJWORDs = Nothing
            If (IsNothing(GloUC_AddRefreshDic1.OBJCRITERIAs) = False) Then
                Try
                    DirectCast(GloUC_AddRefreshDic1.OBJCRITERIAs, DocCriteria).Dispose()

                Catch

                End Try
                GloUC_AddRefreshDic1.OBJCRITERIAs = Nothing
            End If
            If (GloUC_AddRefreshDic1.dtLetterAllocated) Then
                Try
                    If (IsNothing(GloUC_AddRefreshDic1.DTLETTERDATEs) = False) Then
                        Try
                            gloGlobal.cEventHelper.RemoveAllEventHandlers(GloUC_AddRefreshDic1.DTLETTERDATEs)
                        Catch ex As Exception

                        End Try


                        GloUC_AddRefreshDic1.DTLETTERDATEs.Dispose()
                        GloUC_AddRefreshDic1.DTLETTERDATEs = Nothing

                    End If
                Catch
                End Try
                GloUC_AddRefreshDic1.dtLetterAllocated = False
            End If

            GloUC_AddRefreshDic1.M_PATIENTIDs = Nothing
            GloUC_AddRefreshDic1.ObjFrom = Me
            GloUC_AddRefreshDic1.OWORDAPPs = Nothing
            GloUC_AddRefreshDic1.wdPatientWordDocs = Nothing
            GloUC_AddRefreshDic1.DTLETTERDATEs = Nothing
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Letters, gloAuditTrail.ActivityCategory.TemplateAllocation, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

#Region "Call Generate CCDA from Dashboard"
    'Public Delegate Sub GenerateCDAFromCPTTemplate(ByVal PatientID As Int64)
    'Public Event EvntGenerateCDAFromCPTTemplate(ByVal PatientID As Int64)

    Protected Overridable Sub Raise_EvntGenerateCDAFromCPTTemplate(ByVal PatientID As Int64)
        'RaiseEvent EvntGenerateCDAFromCPTTemplate(PatientID)

        Try
            mdlGeneral.OpenCDA(PatientID)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString, True)
            ex = Nothing
        End Try
    End Sub
#End Region


End Class
