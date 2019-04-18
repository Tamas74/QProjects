Imports oOffice = Microsoft.Office.Core
Imports Wd = Microsoft.Office.Interop.Word
Imports System.IO
Imports gloEMR.gloEMRWord
Imports System.Runtime.InteropServices
Imports System.Text.RegularExpressions
Imports gloEMRGeneralLibrary.gloEMRDatabase
Imports System.Data.SqlClient


Public Class frmTemplateGallery
    Inherits System.Windows.Forms.Form
    Implements ISignature
    Implements IHotKey
    Implements gloVoice


    Private ogloVoice As ClsVoice

    Private TemplateGalleryVoiceCol As DNSTools.DgnStrings
    Public mycaller As frmVWTemplateGallary
    Private ImagePath As String
    '''' While Add, return this Paramerters to View-From so the Newly Added Records get Highlighted 
    Public Shared _TemplateName As String
    Public Shared _CategoryID As Long
    ''''
    Private voicecol As DNSTools.DgnStrings
    Private m_ID As Long
    Private m_CategoryType As String
    Private m_ProviderID As Long
    Dim objclsTemplateGallery As New clsTemplateGallery
    Private WithEvents oCurDoc As Wd.Document
    Private WithEvents oTempDoc As Wd.Document
    Dim oCol As New Collection
    Dim trvSearchNode As TreeNode
    Public Shared strItems As ArrayList 'Varibale to pick Items from anoter form
    Public Shared dDropDownWidth As Double
    Public CancelClick As Boolean
    Private WithEvents oWordApp As Wd.Application
    Private WithEvents myToolStrip As WordToolStrip.gloWordToolStrip
    Dim myidx As Int32
    Dim m_arrList As ArrayList
    Dim t_categoryName As String
    Dim dtdictionary As DataTable = Nothing  ''added for problem 00000187,v8020  
    Private objDataFillLiquidData As DataTable
    Private dtFill_Category As DataTable
    Private dtFill_Provider As DataTable
    Private dtFill_TemplateSpecility As DataTable
    Private oDatatrvDiscrete_NodeMouseDoubleClick As DataTable
    Private ofrmSnomedList As frmViewListControl
    Private oSnomedListControl As gloListControl.gloListControl
    Private strSnomedCode As String = ""
    Private strSnomedDescription As String = ""


#Region " Windows Controls "
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents tlsTemplateGallery As WordToolStrip.gloWordToolStrip
    Friend WithEvents wdTemplate As AxDSOFramer.AxFramerControl
    Friend WithEvents pnlTemplate As System.Windows.Forms.Panel
    Friend WithEvents AxDgnDictCustom1 As AxDNSTools.AxDgnDictCustom
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtDescription As System.Windows.Forms.TextBox
    Friend WithEvents cmbProvider As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cmbCategory As System.Windows.Forms.ComboBox
    Friend WithEvents txtTemplateName As System.Windows.Forms.TextBox
    Friend WithEvents imgTemplate As System.Windows.Forms.ImageList
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Private WithEvents lbl_pnlRight As System.Windows.Forms.Label
    Private WithEvents Label6 As System.Windows.Forms.Label
    Private WithEvents Label9 As System.Windows.Forms.Label
    Private WithEvents Label10 As System.Windows.Forms.Label
    Private WithEvents Label11 As System.Windows.Forms.Label
    Private WithEvents Label13 As System.Windows.Forms.Label
    Private WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Private WithEvents Label15 As System.Windows.Forms.Label
    Private WithEvents Label18 As System.Windows.Forms.Label
    Private WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents Panel7 As System.Windows.Forms.Panel
    Friend WithEvents trvDiscrete As System.Windows.Forms.TreeView
    Friend WithEvents Panel8 As System.Windows.Forms.Panel
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Private WithEvents _cntctrl As Wd.ContentControl
#End Region

    Dim EMField As String = ""
    Dim arrEMField() As String
    Dim _arrLabs As New ArrayList
    Dim _arrManagment As New ArrayList
    Dim _arrOrders As New ArrayList
    Dim _arrOtherDiag As New ArrayList
    Dim dtEMField As DataTable

    'sarika DM Denormalization
    Private _DMTemplateName As String = ""
    Private _DMCriteriaID As Int64 = 0
    Private _bISOpenedFromDM As Boolean = False
    Private _DMNode As myTreeNode = Nothing
    '---
    Private blnIsrightclik As Boolean = False

    Public sBibliography As String = ""
    Public sDeveloper As String = ""
#Region "Properties"
    'sarika DM Denormalization
    Public Property DMTemplateName() As String
        Get
            Return _DMTemplateName
        End Get
        Set(ByVal value As String)
            _DMTemplateName = value
        End Set
    End Property

    Public Property ISOpenedFromDM() As Boolean
        Get
            Return _bISOpenedFromDM
        End Get
        Set(ByVal value As Boolean)
            _bISOpenedFromDM = value
        End Set
    End Property

    Public Property DMSelectedNode() As myTreeNode
        Get
            Return _DMNode
        End Get
        Set(ByVal value As myTreeNode)
            _DMNode = value
        End Set
    End Property


    '--
#End Region

#Region "Liquid Data global Variable"
    Dim m_ElementId As Int64
    Dim m_FieldList As SortedList
    Dim m_Required As Boolean
    Dim m_Desc, m_TextValue, m_DataType, m_Caption, m_Fieldcategory, m_controlType As String
    Dim noCols As Int32
    Dim noRows As Int32
    Dim m_arraylist As New ArrayList
    Dim m_list As myList
    Dim oControl As Microsoft.Office.Interop.Word.ContentControl
#End Region

#Region "Liquid Data Table Design Variable"
    Dim objDefaultBehaviorWord8 As Object = Wd.WdDefaultTableBehavior.wdWord8TableBehavior
    Dim objAutoFitFixed As Object = Wd.WdAutoFitBehavior.wdAutoFitFixed
    Dim objMissing As Object = System.Reflection.Missing.Value
    Dim objCollapseEnd As Object = Wd.WdCollapseDirection.wdCollapseEnd
    Dim objHeadingStyle1 As Object = Wd.WdBuiltinStyle.wdStyleHeading1
    Dim objHeadingStyle2 As Object = Wd.WdBuiltinStyle.wdStyleHeading2
    Dim objNormalStyle As Object = Wd.WdBuiltinStyle.wdStyleNormal

    Dim BorderVertical As Wd.WdBorderType = Microsoft.Office.Interop.Word.WdBorderType.wdBorderVertical
    Dim BorderLeft As Wd.WdBorderType = Microsoft.Office.Interop.Word.WdBorderType.wdBorderLeft
    Dim BorderRight As Wd.WdBorderType = Microsoft.Office.Interop.Word.WdBorderType.wdBorderRight
    Dim BorderTop As Wd.WdBorderType = Microsoft.Office.Interop.Word.WdBorderType.wdBorderTop
    Dim BorderBottom As Wd.WdBorderType = Microsoft.Office.Interop.Word.WdBorderType.wdBorderBottom
    Dim LineStyleDouble As Wd.WdLineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleDouble
    Dim LineStyleNone As Wd.WdLineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleNone
    Dim LineStyleSingle As Wd.WdLineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle
    Dim TextureNone As Wd.WdTextureIndex = Microsoft.Office.Interop.Word.WdTextureIndex.wdTextureNone
    Dim ColorGray10 As Wd.WdColor = Microsoft.Office.Interop.Word.WdColor.wdColorGray10
    Dim ColorGray70 As Wd.WdColor = Microsoft.Office.Interop.Word.WdColor.wdColorGray70
    Private WithEvents Label26 As System.Windows.Forms.Label
    Private WithEvents Label24 As System.Windows.Forms.Label
    Private WithEvents Label23 As System.Windows.Forms.Label
    Private WithEvents Label25 As System.Windows.Forms.Label
    Private WithEvents Label27 As System.Windows.Forms.Label
    Private WithEvents Label28 As System.Windows.Forms.Label
    Private WithEvents Label29 As System.Windows.Forms.Label
    Private WithEvents Label17 As System.Windows.Forms.Label
    Private WithEvents Label30 As System.Windows.Forms.Label
    Private WithEvents Label31 As System.Windows.Forms.Label
    Private WithEvents Label32 As System.Windows.Forms.Label
    Private WithEvents Label34 As System.Windows.Forms.Label
    Private WithEvents Label33 As System.Windows.Forms.Label
    Private WithEvents Label7 As System.Windows.Forms.Label
    Private WithEvents Label8 As System.Windows.Forms.Label
    Private WithEvents Label12 As System.Windows.Forms.Label
    Private WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Panel9 As System.Windows.Forms.Panel
    Friend WithEvents Splitter1 As System.Windows.Forms.Splitter
    Friend WithEvents Splitter2 As System.Windows.Forms.Splitter
    Dim White As Wd.WdColorIndex = Microsoft.Office.Interop.Word.WdColorIndex.wdWhite
#End Region

#Region " Windows Form Designer generated code "
    ''Sandip Darade 24 feb 09
    Dim _nLoginProviderId = 0
    Friend WithEvents lblAssociateEM As System.Windows.Forms.Label
    Friend WithEvents chkAssociateEMCategory As System.Windows.Forms.CheckedListBox
    Friend WithEvents Panel10 As System.Windows.Forms.Panel
    Friend WithEvents txtSearchLiquidData As System.Windows.Forms.TextBox
    Friend WithEvents Label35 As System.Windows.Forms.Label
    Friend WithEvents Label36 As System.Windows.Forms.Label
    Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
    Private WithEvents Label37 As System.Windows.Forms.Label
    Private WithEvents Label38 As System.Windows.Forms.Label
    Private WithEvents Label39 As System.Windows.Forms.Label
    Private WithEvents Label40 As System.Windows.Forms.Label
    Friend WithEvents btnClearLiquiddata As System.Windows.Forms.Button
    Friend WithEvents btnClearTemplate As System.Windows.Forms.Button
    Friend WithEvents Label41 As System.Windows.Forms.Label
    Friend WithEvents Label42 As System.Windows.Forms.Label
    Friend WithEvents cmbTemplateSpecility As System.Windows.Forms.ComboBox
    Friend WithEvents btnBibliography As System.Windows.Forms.Button
    Friend WithEvents btnClearSNOMEDCode As System.Windows.Forms.Button
    Friend WithEvents btnBrowseSNOMEDCode As System.Windows.Forms.Button
    Friend WithEvents txtSNOMEDCode As System.Windows.Forms.TextBox
    Friend WithEvents Label54 As System.Windows.Forms.Label
    Friend WithEvents PnlSnomed As System.Windows.Forms.Panel
    Dim appSettings As System.Collections.Specialized.NameValueCollection = System.Configuration.ConfigurationManager.AppSettings
    'constructor commented as not in use
    'Public Sub New()
    '    MyBase.New()
    '    'This call is required by the Windows Form Designer.
    '    InitializeComponent()
    '    'Add any initialization after the InitializeComponent() call

    '    ''Sandip Darade 24 feb 09
    '    ''Providerid retrievedro  set the login provider
    '    If appSettings("ProviderID") IsNot Nothing Then
    '        If appSettings("ProviderID") <> "" Then
    '            _nLoginProviderId = Convert.ToInt64(appSettings("ProviderID"))
    '        Else
    '            _nLoginProviderId = 0
    '        End If
    '    Else
    '        _nLoginProviderId = 0
    '    End If

    'End Sub

    Public Sub New(ByVal ID As Long)
        MyBase.New()
        m_ID = ID
        'This call is required by the Windows Form Designer.
        InitializeComponent()
        'Add any initialization after the InitializeComponent() call

        ''Sandip Darade 24 feb 09
        ''Providerid retrievedro  set the login provider
        If appSettings("ProviderID") IsNot Nothing Then
            If appSettings("ProviderID") <> "" Then
                _nLoginProviderId = Convert.ToInt64(appSettings("ProviderID"))
            Else
                _nLoginProviderId = 0
            End If
        Else
            _nLoginProviderId = 0
        End If
    End Sub
    Public Sub New(ByVal CategoryType As String, ByVal ProviderID As Long)
        MyBase.New()
        m_CategoryType = CategoryType
        m_ProviderID = ProviderID
        'This call is required by the Windows Form Designer.
        InitializeComponent()
        'Add any initialization after the InitializeComponent() call

        ''Sandip Darade 24 feb 09
        ''Providerid retrievedro  set the login provider
        If appSettings("ProviderID") IsNot Nothing Then
            If appSettings("ProviderID") <> "" Then
                _nLoginProviderId = Convert.ToInt64(appSettings("ProviderID"))
            Else
                _nLoginProviderId = 0
            End If
        Else
            _nLoginProviderId = 0
        End If
    End Sub


    'sarika DM Denormalization
    Public Sub New(ByVal DMCriteriaID As Int64, ByVal DMTemplateName As String)
        MyBase.New()
        _DMCriteriaID = DMCriteriaID
        _DMTemplateName = DMTemplateName
        'This call is required by the Windows Form Designer.
        InitializeComponent()
        'Add any initialization after the InitializeComponent() call

        ''Sandip Darade 24 feb 09
        ''Providerid retrievedro  set the login provider
        If appSettings("ProviderID") IsNot Nothing Then
            If appSettings("ProviderID") <> "" Then
                _nLoginProviderId = Convert.ToInt64(appSettings("ProviderID"))
            Else
                _nLoginProviderId = 0
            End If
        Else
            _nLoginProviderId = 0
        End If
    End Sub

    Public Sub New(ByVal bIsOpenedFromDM As Boolean)
        MyBase.New()
        '_DMCriteriaID = DMNode.
        '_DMTemplateName = DMNode.Text
        '_TemplateName = DMNode.Text
        _bISOpenedFromDM = bIsOpenedFromDM
        '_DMNode = DMNode
        'This call is required by the Windows Form Designer.
        InitializeComponent()
        'Add any initialization after the InitializeComponent() call

        ''Sandip Darade 24 feb 09
        ''Providerid retrievedro  set the login provider
        If appSettings("ProviderID") IsNot Nothing Then
            If appSettings("ProviderID") <> "" Then
                _nLoginProviderId = Convert.ToInt64(appSettings("ProviderID"))
            Else
                _nLoginProviderId = 0
            End If
        Else
            _nLoginProviderId = 0
        End If
    End Sub

    '---

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            Try
                gloGlobal.cEventHelper.RemoveAllEventHandlers(Me)
            Catch
            End Try
            Try
                If (IsNothing(trvDataDictionary.ContextMenu) = False) Then
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(trvDataDictionary.ContextMenu)
                    If (IsNothing(trvDataDictionary.ContextMenu.MenuItems) = False) Then
                        trvDataDictionary.ContextMenu.MenuItems.Clear()
                    End If

                    trvDataDictionary.ContextMenu.Dispose()
                    trvDataDictionary.ContextMenu = Nothing
                End If
            Catch ex As Exception

            End Try
            If Not (components Is Nothing) Then
                components.Dispose()

            End If
            Try
                If (IsNothing(OpenFileDialog1) = False) Then
                    OpenFileDialog1.Dispose()
                    OpenFileDialog1 = Nothing
                End If
            Catch ex As Exception

            End Try
            If IsNothing(ogloVoice) = False Then
                ogloVoice.Dispose() : ogloVoice = Nothing
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
    Friend WithEvents chkCaption As System.Windows.Forms.CheckBox
    '  Friend WithEvents wdDescription As AxDSOFramer.AxFramerControl
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents txtsearchtemplate As System.Windows.Forms.TextBox
    Friend WithEvents trvDataDictionary As System.Windows.Forms.TreeView
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmTemplateGallery))
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.lbl_pnlRight = New System.Windows.Forms.Label()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.wdTemplate = New AxDSOFramer.AxFramerControl()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.lblAssociateEM = New System.Windows.Forms.Label()
        Me.chkAssociateEMCategory = New System.Windows.Forms.CheckedListBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.pnlTemplate = New System.Windows.Forms.Panel()
        Me.PnlSnomed = New System.Windows.Forms.Panel()
        Me.btnClearSNOMEDCode = New System.Windows.Forms.Button()
        Me.Label54 = New System.Windows.Forms.Label()
        Me.btnBrowseSNOMEDCode = New System.Windows.Forms.Button()
        Me.txtSNOMEDCode = New System.Windows.Forms.TextBox()
        Me.btnBibliography = New System.Windows.Forms.Button()
        Me.Label42 = New System.Windows.Forms.Label()
        Me.cmbTemplateSpecility = New System.Windows.Forms.ComboBox()
        Me.Label41 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.AxDgnDictCustom1 = New AxDNSTools.AxDgnDictCustom()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtDescription = New System.Windows.Forms.TextBox()
        Me.cmbProvider = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cmbCategory = New System.Windows.Forms.ComboBox()
        Me.txtTemplateName = New System.Windows.Forms.TextBox()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.trvDiscrete = New System.Windows.Forms.TreeView()
        Me.imgTemplate = New System.Windows.Forms.ImageList(Me.components)
        Me.Panel10 = New System.Windows.Forms.Panel()
        Me.txtSearchLiquidData = New System.Windows.Forms.TextBox()
        Me.btnClearLiquiddata = New System.Windows.Forms.Button()
        Me.Label35 = New System.Windows.Forms.Label()
        Me.Label36 = New System.Windows.Forms.Label()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.Label37 = New System.Windows.Forms.Label()
        Me.Label38 = New System.Windows.Forms.Label()
        Me.Label39 = New System.Windows.Forms.Label()
        Me.Label40 = New System.Windows.Forms.Label()
        Me.Panel9 = New System.Windows.Forms.Panel()
        Me.Panel8 = New System.Windows.Forms.Panel()
        Me.Label34 = New System.Windows.Forms.Label()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Splitter1 = New System.Windows.Forms.Splitter()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.trvDataDictionary = New System.Windows.Forms.TreeView()
        Me.chkCaption = New System.Windows.Forms.CheckBox()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.txtsearchtemplate = New System.Windows.Forms.TextBox()
        Me.btnClearTemplate = New System.Windows.Forms.Button()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.tlsTemplateGallery = New WordToolStrip.gloWordToolStrip()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.Splitter2 = New System.Windows.Forms.Splitter()
        Me.Panel1.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.wdTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel4.SuspendLayout()
        Me.pnlTemplate.SuspendLayout()
        Me.PnlSnomed.SuspendLayout()
        CType(Me.AxDgnDictCustom1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        Me.Panel7.SuspendLayout()
        Me.Panel10.SuspendLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel9.SuspendLayout()
        Me.Panel8.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.Panel6.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Transparent
        Me.Panel1.Controls.Add(Me.lbl_pnlRight)
        Me.Panel1.Controls.Add(Me.Panel3)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(229, 158)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.Panel1.Size = New System.Drawing.Size(1034, 558)
        Me.Panel1.TabIndex = 8
        '
        'lbl_pnlRight
        '
        Me.lbl_pnlRight.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_pnlRight.Location = New System.Drawing.Point(1030, 0)
        Me.lbl_pnlRight.Name = "lbl_pnlRight"
        Me.lbl_pnlRight.Size = New System.Drawing.Size(1, 555)
        Me.lbl_pnlRight.TabIndex = 6
        Me.lbl_pnlRight.Text = "label3"
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Panel3.Controls.Add(Me.wdTemplate)
        Me.Panel3.Controls.Add(Me.Label18)
        Me.Panel3.Controls.Add(Me.Panel4)
        Me.Panel3.Controls.Add(Me.Label9)
        Me.Panel3.Controls.Add(Me.Label10)
        Me.Panel3.Controls.Add(Me.lblAssociateEM)
        Me.Panel3.Controls.Add(Me.chkAssociateEMCategory)
        Me.Panel3.Controls.Add(Me.Label11)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel3.Location = New System.Drawing.Point(0, 0)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(1031, 555)
        Me.Panel3.TabIndex = 3
        '
        'wdTemplate
        '
        Me.wdTemplate.Dock = System.Windows.Forms.DockStyle.Fill
        Me.wdTemplate.Enabled = True
        Me.wdTemplate.Location = New System.Drawing.Point(1, 1)
        Me.wdTemplate.Name = "wdTemplate"
        Me.wdTemplate.OcxState = CType(resources.GetObject("wdTemplate.OcxState"), System.Windows.Forms.AxHost.State)
        Me.wdTemplate.Size = New System.Drawing.Size(1029, 553)
        Me.wdTemplate.TabIndex = 17
        '
        'Label18
        '
        Me.Label18.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label18.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label18.Location = New System.Drawing.Point(1, 554)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(1029, 1)
        Me.Label18.TabIndex = 37
        Me.Label18.Text = "label1"
        '
        'Panel4
        '
        Me.Panel4.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.Panel4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel4.Controls.Add(Me.Label6)
        Me.Panel4.Controls.Add(Me.Label5)
        Me.Panel4.Location = New System.Drawing.Point(24, 156)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.Panel4.Size = New System.Drawing.Size(212, 26)
        Me.Panel4.TabIndex = 33
        Me.Panel4.Visible = False
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label6.Location = New System.Drawing.Point(0, 22)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(212, 1)
        Me.Label6.TabIndex = 34
        Me.Label6.Text = "label1"
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Location = New System.Drawing.Point(0, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(212, 23)
        Me.Label5.TabIndex = 31
        Me.Label5.Text = "Search"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label9.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label9.Location = New System.Drawing.Point(0, 1)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(1, 554)
        Me.Label9.TabIndex = 11
        Me.Label9.Text = "label4"
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label10.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label10.Location = New System.Drawing.Point(1030, 1)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(1, 554)
        Me.Label10.TabIndex = 10
        Me.Label10.Text = "label3"
        '
        'lblAssociateEM
        '
        Me.lblAssociateEM.AutoSize = True
        Me.lblAssociateEM.BackColor = System.Drawing.Color.Transparent
        Me.lblAssociateEM.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblAssociateEM.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAssociateEM.Location = New System.Drawing.Point(497, 203)
        Me.lblAssociateEM.Name = "lblAssociateEM"
        Me.lblAssociateEM.Size = New System.Drawing.Size(155, 14)
        Me.lblAssociateEM.TabIndex = 38
        Me.lblAssociateEM.Text = "Associate EM Category :"
        Me.lblAssociateEM.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblAssociateEM.Visible = False
        '
        'chkAssociateEMCategory
        '
        Me.chkAssociateEMCategory.FormattingEnabled = True
        Me.chkAssociateEMCategory.Items.AddRange(New Object() {"Management Option", "Labs", "X-Ray/Radiology", "Other Diagnostic Tests"})
        Me.chkAssociateEMCategory.Location = New System.Drawing.Point(654, 203)
        Me.chkAssociateEMCategory.Name = "chkAssociateEMCategory"
        Me.chkAssociateEMCategory.Size = New System.Drawing.Size(184, 55)
        Me.chkAssociateEMCategory.TabIndex = 10
        Me.chkAssociateEMCategory.Visible = False
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label11.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label11.Location = New System.Drawing.Point(0, 0)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(1031, 1)
        Me.Label11.TabIndex = 9
        Me.Label11.Text = "label1"
        '
        'pnlTemplate
        '
        Me.pnlTemplate.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlTemplate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlTemplate.Controls.Add(Me.PnlSnomed)
        Me.pnlTemplate.Controls.Add(Me.btnBibliography)
        Me.pnlTemplate.Controls.Add(Me.Label42)
        Me.pnlTemplate.Controls.Add(Me.cmbTemplateSpecility)
        Me.pnlTemplate.Controls.Add(Me.Label41)
        Me.pnlTemplate.Controls.Add(Me.Label19)
        Me.pnlTemplate.Controls.Add(Me.Label15)
        Me.pnlTemplate.Controls.Add(Me.Label13)
        Me.pnlTemplate.Controls.Add(Me.Label14)
        Me.pnlTemplate.Controls.Add(Me.AxDgnDictCustom1)
        Me.pnlTemplate.Controls.Add(Me.Label1)
        Me.pnlTemplate.Controls.Add(Me.Label2)
        Me.pnlTemplate.Controls.Add(Me.txtDescription)
        Me.pnlTemplate.Controls.Add(Me.cmbProvider)
        Me.pnlTemplate.Controls.Add(Me.Label4)
        Me.pnlTemplate.Controls.Add(Me.Label3)
        Me.pnlTemplate.Controls.Add(Me.cmbCategory)
        Me.pnlTemplate.Controls.Add(Me.txtTemplateName)
        Me.pnlTemplate.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlTemplate.Location = New System.Drawing.Point(229, 53)
        Me.pnlTemplate.Name = "pnlTemplate"
        Me.pnlTemplate.Padding = New System.Windows.Forms.Padding(0, 3, 3, 3)
        Me.pnlTemplate.Size = New System.Drawing.Size(1034, 105)
        Me.pnlTemplate.TabIndex = 7
        '
        'PnlSnomed
        '
        Me.PnlSnomed.Controls.Add(Me.btnClearSNOMEDCode)
        Me.PnlSnomed.Controls.Add(Me.Label54)
        Me.PnlSnomed.Controls.Add(Me.btnBrowseSNOMEDCode)
        Me.PnlSnomed.Controls.Add(Me.txtSNOMEDCode)
        Me.PnlSnomed.Location = New System.Drawing.Point(19, 66)
        Me.PnlSnomed.Name = "PnlSnomed"
        Me.PnlSnomed.Size = New System.Drawing.Size(743, 29)
        Me.PnlSnomed.TabIndex = 17
        Me.PnlSnomed.Visible = False
        '
        'btnClearSNOMEDCode
        '
        Me.btnClearSNOMEDCode.BackColor = System.Drawing.Color.Transparent
        Me.btnClearSNOMEDCode.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.btnClearSNOMEDCode.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnClearSNOMEDCode.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnClearSNOMEDCode.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnClearSNOMEDCode.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnClearSNOMEDCode.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClearSNOMEDCode.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClearSNOMEDCode.Image = CType(resources.GetObject("btnClearSNOMEDCode.Image"), System.Drawing.Image)
        Me.btnClearSNOMEDCode.Location = New System.Drawing.Point(713, 2)
        Me.btnClearSNOMEDCode.Name = "btnClearSNOMEDCode"
        Me.btnClearSNOMEDCode.Size = New System.Drawing.Size(22, 23)
        Me.btnClearSNOMEDCode.TabIndex = 7
        Me.btnClearSNOMEDCode.UseVisualStyleBackColor = False
        '
        'Label54
        '
        Me.Label54.AutoSize = True
        Me.Label54.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label54.Location = New System.Drawing.Point(2, 6)
        Me.Label54.Name = "Label54"
        Me.Label54.Size = New System.Drawing.Size(102, 14)
        Me.Label54.TabIndex = 222
        Me.Label54.Text = "SNOMED Code :"
        '
        'btnBrowseSNOMEDCode
        '
        Me.btnBrowseSNOMEDCode.BackColor = System.Drawing.Color.Transparent
        Me.btnBrowseSNOMEDCode.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.btnBrowseSNOMEDCode.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnBrowseSNOMEDCode.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnBrowseSNOMEDCode.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnBrowseSNOMEDCode.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnBrowseSNOMEDCode.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBrowseSNOMEDCode.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBrowseSNOMEDCode.Image = CType(resources.GetObject("btnBrowseSNOMEDCode.Image"), System.Drawing.Image)
        Me.btnBrowseSNOMEDCode.Location = New System.Drawing.Point(687, 2)
        Me.btnBrowseSNOMEDCode.Name = "btnBrowseSNOMEDCode"
        Me.btnBrowseSNOMEDCode.Size = New System.Drawing.Size(22, 23)
        Me.btnBrowseSNOMEDCode.TabIndex = 6
        Me.btnBrowseSNOMEDCode.UseVisualStyleBackColor = False
        '
        'txtSNOMEDCode
        '
        Me.txtSNOMEDCode.Enabled = False
        Me.txtSNOMEDCode.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSNOMEDCode.Location = New System.Drawing.Point(108, 2)
        Me.txtSNOMEDCode.Name = "txtSNOMEDCode"
        Me.txtSNOMEDCode.Size = New System.Drawing.Size(573, 22)
        Me.txtSNOMEDCode.TabIndex = 8
        '
        'btnBibliography
        '
        Me.btnBibliography.BackgroundImage = Global.gloEMR.My.Resources.Resources.About_us1
        Me.btnBibliography.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnBibliography.Location = New System.Drawing.Point(704, 9)
        Me.btnBibliography.Name = "btnBibliography"
        Me.btnBibliography.Size = New System.Drawing.Size(24, 24)
        Me.btnBibliography.BackgroundImageLayout = ImageLayout.Stretch
        Me.btnBibliography.TabIndex = 2
        Me.btnBibliography.UseVisualStyleBackColor = True
        Me.btnBibliography.Visible = False
        '
        'Label42
        '
        Me.Label42.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label42.AutoSize = True
        Me.Label42.BackColor = System.Drawing.Color.Transparent
        Me.Label42.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label42.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label42.Location = New System.Drawing.Point(358, 42)
        Me.Label42.Name = "Label42"
        Me.Label42.Size = New System.Drawing.Size(130, 14)
        Me.Label42.TabIndex = 42
        Me.Label42.Text = "Template Specialty :"
        Me.Label42.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmbTemplateSpecility
        '
        Me.cmbTemplateSpecility.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cmbTemplateSpecility.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbTemplateSpecility.ForeColor = System.Drawing.Color.Black
        Me.cmbTemplateSpecility.Location = New System.Drawing.Point(491, 38)
        Me.cmbTemplateSpecility.Name = "cmbTemplateSpecility"
        Me.cmbTemplateSpecility.Size = New System.Drawing.Size(210, 22)
        Me.cmbTemplateSpecility.Sorted = True
        Me.cmbTemplateSpecility.TabIndex = 5
        '
        'Label41
        '
        Me.Label41.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label41.AutoSize = True
        Me.Label41.ForeColor = System.Drawing.Color.Red
        Me.Label41.Location = New System.Drawing.Point(5, 14)
        Me.Label41.Name = "Label41"
        Me.Label41.Size = New System.Drawing.Size(14, 14)
        Me.Label41.TabIndex = 39
        Me.Label41.Text = "*"
        '
        'Label19
        '
        Me.Label19.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label19.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label19.Location = New System.Drawing.Point(1, 101)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(1029, 1)
        Me.Label19.TabIndex = 37
        Me.Label19.Text = "label1"
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label15.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label15.Location = New System.Drawing.Point(1, 3)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(1029, 1)
        Me.Label15.TabIndex = 36
        Me.Label15.Text = "label1"
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label13.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label13.Location = New System.Drawing.Point(0, 3)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(1, 99)
        Me.Label13.TabIndex = 18
        Me.Label13.Text = "label4"
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label14.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label14.Location = New System.Drawing.Point(1030, 3)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(1, 99)
        Me.Label14.TabIndex = 17
        Me.Label14.Text = "label3"
        '
        'AxDgnDictCustom1
        '
        Me.AxDgnDictCustom1.Enabled = True
        Me.AxDgnDictCustom1.Location = New System.Drawing.Point(730, 130)
        Me.AxDgnDictCustom1.Name = "AxDgnDictCustom1"
        Me.AxDgnDictCustom1.OcxState = CType(resources.GetObject("AxDgnDictCustom1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxDgnDictCustom1.Size = New System.Drawing.Size(16, 15)
        Me.AxDgnDictCustom1.TabIndex = 15
        '
        'Label1
        '
        Me.Label1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(16, 14)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(108, 14)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "Template Name :"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(357, 138)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(96, 22)
        Me.Label2.TabIndex = 14
        Me.Label2.Text = "Description :"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Label2.Visible = False
        '
        'txtDescription
        '
        Me.txtDescription.ForeColor = System.Drawing.Color.Black
        Me.txtDescription.Location = New System.Drawing.Point(456, 138)
        Me.txtDescription.Name = "txtDescription"
        Me.txtDescription.Size = New System.Drawing.Size(210, 22)
        Me.txtDescription.TabIndex = 15
        Me.txtDescription.Visible = False
        '
        'cmbProvider
        '
        Me.cmbProvider.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cmbProvider.BackColor = System.Drawing.SystemColors.Window
        Me.cmbProvider.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbProvider.ForeColor = System.Drawing.Color.Black
        Me.cmbProvider.Location = New System.Drawing.Point(126, 38)
        Me.cmbProvider.Name = "cmbProvider"
        Me.cmbProvider.Size = New System.Drawing.Size(210, 22)
        Me.cmbProvider.TabIndex = 4
        '
        'Label4
        '
        Me.Label4.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(417, 14)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(71, 14)
        Me.Label4.TabIndex = 12
        Me.Label4.Text = "Category :"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label3
        '
        Me.Label3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(57, 42)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(66, 14)
        Me.Label3.TabIndex = 10
        Me.Label3.Text = "Provider :"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmbCategory
        '
        Me.cmbCategory.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cmbCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCategory.ForeColor = System.Drawing.Color.Black
        Me.cmbCategory.Location = New System.Drawing.Point(491, 10)
        Me.cmbCategory.Name = "cmbCategory"
        Me.cmbCategory.Size = New System.Drawing.Size(210, 22)
        Me.cmbCategory.TabIndex = 3
        '
        'txtTemplateName
        '
        Me.txtTemplateName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtTemplateName.ForeColor = System.Drawing.Color.Black
        Me.txtTemplateName.Location = New System.Drawing.Point(126, 10)
        Me.txtTemplateName.MaxLength = 50
        Me.txtTemplateName.Name = "txtTemplateName"
        Me.txtTemplateName.Size = New System.Drawing.Size(210, 22)
        Me.txtTemplateName.TabIndex = 1
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Panel2.Controls.Add(Me.Panel7)
        Me.Panel2.Controls.Add(Me.Panel10)
        Me.Panel2.Controls.Add(Me.Panel9)
        Me.Panel2.Controls.Add(Me.Splitter1)
        Me.Panel2.Controls.Add(Me.Panel5)
        Me.Panel2.Controls.Add(Me.Panel6)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel2.Location = New System.Drawing.Point(0, 53)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(226, 663)
        Me.Panel2.TabIndex = 1
        '
        'Panel7
        '
        Me.Panel7.Controls.Add(Me.Label17)
        Me.Panel7.Controls.Add(Me.Label30)
        Me.Panel7.Controls.Add(Me.Label31)
        Me.Panel7.Controls.Add(Me.Label32)
        Me.Panel7.Controls.Add(Me.trvDiscrete)
        Me.Panel7.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel7.Location = New System.Drawing.Point(0, 412)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Padding = New System.Windows.Forms.Padding(3, 0, 0, 3)
        Me.Panel7.Size = New System.Drawing.Size(226, 251)
        Me.Panel7.TabIndex = 40
        '
        'Label17
        '
        Me.Label17.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label17.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label17.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label17.Location = New System.Drawing.Point(4, 247)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(221, 1)
        Me.Label17.TabIndex = 20
        Me.Label17.Text = "label2"
        '
        'Label30
        '
        Me.Label30.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label30.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label30.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label30.Location = New System.Drawing.Point(3, 1)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(1, 247)
        Me.Label30.TabIndex = 19
        Me.Label30.Text = "label4"
        '
        'Label31
        '
        Me.Label31.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label31.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label31.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label31.Location = New System.Drawing.Point(225, 1)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(1, 247)
        Me.Label31.TabIndex = 18
        Me.Label31.Text = "label3"
        '
        'Label32
        '
        Me.Label32.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label32.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label32.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label32.Location = New System.Drawing.Point(3, 0)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(223, 1)
        Me.Label32.TabIndex = 17
        Me.Label32.Text = "label1"
        '
        'trvDiscrete
        '
        Me.trvDiscrete.BackColor = System.Drawing.Color.GhostWhite
        Me.trvDiscrete.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trvDiscrete.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trvDiscrete.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.trvDiscrete.ForeColor = System.Drawing.Color.Black
        Me.trvDiscrete.HideSelection = False
        Me.trvDiscrete.ImageIndex = 0
        Me.trvDiscrete.ImageList = Me.imgTemplate
        Me.trvDiscrete.ItemHeight = 20
        Me.trvDiscrete.Location = New System.Drawing.Point(3, 0)
        Me.trvDiscrete.Name = "trvDiscrete"
        Me.trvDiscrete.SelectedImageIndex = 0
        Me.trvDiscrete.Size = New System.Drawing.Size(223, 248)
        Me.trvDiscrete.TabIndex = 11
        '
        'imgTemplate
        '
        Me.imgTemplate.ImageStream = CType(resources.GetObject("imgTemplate.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imgTemplate.TransparentColor = System.Drawing.Color.Transparent
        Me.imgTemplate.Images.SetKeyName(0, "Liduid Data.ico")
        Me.imgTemplate.Images.SetKeyName(1, "SubTemplate.ico")
        Me.imgTemplate.Images.SetKeyName(2, "Bullet06.ico")
        Me.imgTemplate.Images.SetKeyName(3, "datadictionary.ico")
        Me.imgTemplate.Images.SetKeyName(4, "Table_04.ico")
        Me.imgTemplate.Images.SetKeyName(5, "Arrow_02.ico")
        Me.imgTemplate.Images.SetKeyName(6, "Small Arrow.ico")
        '
        'Panel10
        '
        Me.Panel10.Controls.Add(Me.txtSearchLiquidData)
        Me.Panel10.Controls.Add(Me.btnClearLiquiddata)
        Me.Panel10.Controls.Add(Me.Label35)
        Me.Panel10.Controls.Add(Me.Label36)
        Me.Panel10.Controls.Add(Me.PictureBox2)
        Me.Panel10.Controls.Add(Me.Label37)
        Me.Panel10.Controls.Add(Me.Label38)
        Me.Panel10.Controls.Add(Me.Label39)
        Me.Panel10.Controls.Add(Me.Label40)
        Me.Panel10.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel10.Location = New System.Drawing.Point(0, 386)
        Me.Panel10.Name = "Panel10"
        Me.Panel10.Padding = New System.Windows.Forms.Padding(3, 0, 0, 3)
        Me.Panel10.Size = New System.Drawing.Size(226, 26)
        Me.Panel10.TabIndex = 43
        '
        'txtSearchLiquidData
        '
        Me.txtSearchLiquidData.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtSearchLiquidData.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtSearchLiquidData.Location = New System.Drawing.Point(32, 5)
        Me.txtSearchLiquidData.Name = "txtSearchLiquidData"
        Me.txtSearchLiquidData.Size = New System.Drawing.Size(172, 15)
        Me.txtSearchLiquidData.TabIndex = 8
        '
        'btnClearLiquiddata
        '
        Me.btnClearLiquiddata.BackColor = System.Drawing.Color.Transparent
        Me.btnClearLiquiddata.BackgroundImage = CType(resources.GetObject("btnClearLiquiddata.BackgroundImage"), System.Drawing.Image)
        Me.btnClearLiquiddata.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnClearLiquiddata.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnClearLiquiddata.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnClearLiquiddata.FlatAppearance.BorderSize = 0
        Me.btnClearLiquiddata.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnClearLiquiddata.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnClearLiquiddata.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClearLiquiddata.Image = CType(resources.GetObject("btnClearLiquiddata.Image"), System.Drawing.Image)
        Me.btnClearLiquiddata.Location = New System.Drawing.Point(204, 5)
        Me.btnClearLiquiddata.Name = "btnClearLiquiddata"
        Me.btnClearLiquiddata.Size = New System.Drawing.Size(21, 15)
        Me.btnClearLiquiddata.TabIndex = 45
        Me.btnClearLiquiddata.UseVisualStyleBackColor = False
        '
        'Label35
        '
        Me.Label35.BackColor = System.Drawing.Color.White
        Me.Label35.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label35.Location = New System.Drawing.Point(32, 1)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(193, 4)
        Me.Label35.TabIndex = 37
        '
        'Label36
        '
        Me.Label36.BackColor = System.Drawing.Color.White
        Me.Label36.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label36.Location = New System.Drawing.Point(32, 20)
        Me.Label36.Name = "Label36"
        Me.Label36.Size = New System.Drawing.Size(193, 2)
        Me.Label36.TabIndex = 38
        '
        'PictureBox2
        '
        Me.PictureBox2.BackColor = System.Drawing.Color.White
        Me.PictureBox2.Dock = System.Windows.Forms.DockStyle.Left
        Me.PictureBox2.Image = CType(resources.GetObject("PictureBox2.Image"), System.Drawing.Image)
        Me.PictureBox2.Location = New System.Drawing.Point(4, 1)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(28, 21)
        Me.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox2.TabIndex = 9
        Me.PictureBox2.TabStop = False
        '
        'Label37
        '
        Me.Label37.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label37.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label37.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label37.Location = New System.Drawing.Point(4, 22)
        Me.Label37.Name = "Label37"
        Me.Label37.Size = New System.Drawing.Size(221, 1)
        Me.Label37.TabIndex = 42
        Me.Label37.Text = "label2"
        '
        'Label38
        '
        Me.Label38.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label38.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label38.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label38.Location = New System.Drawing.Point(3, 1)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(1, 22)
        Me.Label38.TabIndex = 41
        Me.Label38.Text = "label4"
        '
        'Label39
        '
        Me.Label39.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label39.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label39.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label39.Location = New System.Drawing.Point(225, 1)
        Me.Label39.Name = "Label39"
        Me.Label39.Size = New System.Drawing.Size(1, 22)
        Me.Label39.TabIndex = 40
        Me.Label39.Text = "label3"
        '
        'Label40
        '
        Me.Label40.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label40.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label40.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label40.Location = New System.Drawing.Point(3, 0)
        Me.Label40.Name = "Label40"
        Me.Label40.Size = New System.Drawing.Size(223, 1)
        Me.Label40.TabIndex = 39
        Me.Label40.Text = "label1"
        '
        'Panel9
        '
        Me.Panel9.Controls.Add(Me.Panel8)
        Me.Panel9.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel9.Location = New System.Drawing.Point(0, 358)
        Me.Panel9.Name = "Panel9"
        Me.Panel9.Padding = New System.Windows.Forms.Padding(3, 0, 0, 3)
        Me.Panel9.Size = New System.Drawing.Size(226, 28)
        Me.Panel9.TabIndex = 42
        '
        'Panel8
        '
        Me.Panel8.BackColor = System.Drawing.Color.Transparent
        Me.Panel8.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.Panel8.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel8.Controls.Add(Me.Label34)
        Me.Panel8.Controls.Add(Me.Label33)
        Me.Panel8.Controls.Add(Me.Label26)
        Me.Panel8.Controls.Add(Me.Label24)
        Me.Panel8.Controls.Add(Me.Label22)
        Me.Panel8.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel8.ForeColor = System.Drawing.Color.Black
        Me.Panel8.Location = New System.Drawing.Point(3, 0)
        Me.Panel8.Name = "Panel8"
        Me.Panel8.Size = New System.Drawing.Size(223, 25)
        Me.Panel8.TabIndex = 14
        '
        'Label34
        '
        Me.Label34.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label34.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label34.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label34.Location = New System.Drawing.Point(222, 1)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(1, 23)
        Me.Label34.TabIndex = 43
        Me.Label34.Text = "label4"
        '
        'Label33
        '
        Me.Label33.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label33.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label33.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label33.Location = New System.Drawing.Point(0, 1)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(1, 23)
        Me.Label33.TabIndex = 42
        Me.Label33.Text = "label4"
        '
        'Label26
        '
        Me.Label26.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label26.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label26.Location = New System.Drawing.Point(0, 0)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(223, 1)
        Me.Label26.TabIndex = 38
        Me.Label26.Text = "label1"
        '
        'Label24
        '
        Me.Label24.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label24.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label24.Location = New System.Drawing.Point(0, 24)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(223, 1)
        Me.Label24.TabIndex = 37
        Me.Label24.Text = "label1"
        '
        'Label22
        '
        Me.Label22.BackColor = System.Drawing.Color.Transparent
        Me.Label22.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label22.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label22.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label22.Location = New System.Drawing.Point(0, 0)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(223, 25)
        Me.Label22.TabIndex = 2
        Me.Label22.Text = "  Liquid Data Dictionary"
        Me.Label22.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Splitter1
        '
        Me.Splitter1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Splitter1.Location = New System.Drawing.Point(0, 355)
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(226, 3)
        Me.Splitter1.TabIndex = 41
        Me.Splitter1.TabStop = False
        '
        'Panel5
        '
        Me.Panel5.Controls.Add(Me.Label23)
        Me.Panel5.Controls.Add(Me.trvDataDictionary)
        Me.Panel5.Controls.Add(Me.chkCaption)
        Me.Panel5.Controls.Add(Me.Label25)
        Me.Panel5.Controls.Add(Me.Label27)
        Me.Panel5.Controls.Add(Me.Label28)
        Me.Panel5.Controls.Add(Me.Label29)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel5.Location = New System.Drawing.Point(0, 29)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.Panel5.Size = New System.Drawing.Size(226, 326)
        Me.Panel5.TabIndex = 39
        '
        'Label23
        '
        Me.Label23.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label23.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label23.Location = New System.Drawing.Point(4, 302)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(221, 1)
        Me.Label23.TabIndex = 37
        Me.Label23.Text = "label1"
        '
        'trvDataDictionary
        '
        Me.trvDataDictionary.BackColor = System.Drawing.Color.White
        Me.trvDataDictionary.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trvDataDictionary.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trvDataDictionary.ForeColor = System.Drawing.Color.Black
        Me.trvDataDictionary.HideSelection = False
        Me.trvDataDictionary.ImageIndex = 0
        Me.trvDataDictionary.ImageList = Me.imgTemplate
        Me.trvDataDictionary.ItemHeight = 21
        Me.trvDataDictionary.Location = New System.Drawing.Point(4, 1)
        Me.trvDataDictionary.Name = "trvDataDictionary"
        Me.trvDataDictionary.SelectedImageIndex = 0
        Me.trvDataDictionary.Size = New System.Drawing.Size(221, 302)
        Me.trvDataDictionary.TabIndex = 9
        '
        'chkCaption
        '
        Me.chkCaption.BackColor = System.Drawing.Color.Transparent
        Me.chkCaption.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.chkCaption.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.chkCaption.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.chkCaption.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.chkCaption.Location = New System.Drawing.Point(4, 303)
        Me.chkCaption.Name = "chkCaption"
        Me.chkCaption.Padding = New System.Windows.Forms.Padding(40, 0, 0, 0)
        Me.chkCaption.Size = New System.Drawing.Size(221, 22)
        Me.chkCaption.TabIndex = 10
        Me.chkCaption.Text = " Include Caption"
        Me.chkCaption.UseVisualStyleBackColor = False
        '
        'Label25
        '
        Me.Label25.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label25.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label25.Location = New System.Drawing.Point(4, 325)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(221, 1)
        Me.Label25.TabIndex = 38
        Me.Label25.Text = "label1"
        '
        'Label27
        '
        Me.Label27.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label27.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label27.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label27.Location = New System.Drawing.Point(3, 1)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(1, 325)
        Me.Label27.TabIndex = 41
        Me.Label27.Text = "label4"
        '
        'Label28
        '
        Me.Label28.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label28.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label28.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label28.Location = New System.Drawing.Point(225, 1)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(1, 325)
        Me.Label28.TabIndex = 40
        Me.Label28.Text = "label3"
        '
        'Label29
        '
        Me.Label29.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label29.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label29.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label29.Location = New System.Drawing.Point(3, 0)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(223, 1)
        Me.Label29.TabIndex = 39
        Me.Label29.Text = "label1"
        '
        'Panel6
        '
        Me.Panel6.Controls.Add(Me.txtsearchtemplate)
        Me.Panel6.Controls.Add(Me.btnClearTemplate)
        Me.Panel6.Controls.Add(Me.Label20)
        Me.Panel6.Controls.Add(Me.Label21)
        Me.Panel6.Controls.Add(Me.PictureBox1)
        Me.Panel6.Controls.Add(Me.Label7)
        Me.Panel6.Controls.Add(Me.Label8)
        Me.Panel6.Controls.Add(Me.Label12)
        Me.Panel6.Controls.Add(Me.Label16)
        Me.Panel6.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel6.Location = New System.Drawing.Point(0, 0)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Padding = New System.Windows.Forms.Padding(3, 3, 0, 3)
        Me.Panel6.Size = New System.Drawing.Size(226, 29)
        Me.Panel6.TabIndex = 7
        '
        'txtsearchtemplate
        '
        Me.txtsearchtemplate.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtsearchtemplate.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtsearchtemplate.Location = New System.Drawing.Point(32, 8)
        Me.txtsearchtemplate.Name = "txtsearchtemplate"
        Me.txtsearchtemplate.Size = New System.Drawing.Size(172, 15)
        Me.txtsearchtemplate.TabIndex = 8
        '
        'btnClearTemplate
        '
        Me.btnClearTemplate.BackColor = System.Drawing.Color.Transparent
        Me.btnClearTemplate.BackgroundImage = CType(resources.GetObject("btnClearTemplate.BackgroundImage"), System.Drawing.Image)
        Me.btnClearTemplate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnClearTemplate.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnClearTemplate.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnClearTemplate.FlatAppearance.BorderSize = 0
        Me.btnClearTemplate.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnClearTemplate.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnClearTemplate.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClearTemplate.Image = CType(resources.GetObject("btnClearTemplate.Image"), System.Drawing.Image)
        Me.btnClearTemplate.Location = New System.Drawing.Point(204, 8)
        Me.btnClearTemplate.Name = "btnClearTemplate"
        Me.btnClearTemplate.Size = New System.Drawing.Size(21, 15)
        Me.btnClearTemplate.TabIndex = 44
        Me.btnClearTemplate.UseVisualStyleBackColor = False
        '
        'Label20
        '
        Me.Label20.BackColor = System.Drawing.Color.White
        Me.Label20.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label20.Location = New System.Drawing.Point(32, 4)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(193, 4)
        Me.Label20.TabIndex = 37
        '
        'Label21
        '
        Me.Label21.BackColor = System.Drawing.Color.White
        Me.Label21.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label21.Location = New System.Drawing.Point(32, 23)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(193, 2)
        Me.Label21.TabIndex = 38
        '
        'PictureBox1
        '
        Me.PictureBox1.BackColor = System.Drawing.Color.White
        Me.PictureBox1.Dock = System.Windows.Forms.DockStyle.Left
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(4, 4)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(28, 21)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox1.TabIndex = 9
        Me.PictureBox1.TabStop = False
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label7.Location = New System.Drawing.Point(4, 25)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(221, 1)
        Me.Label7.TabIndex = 42
        Me.Label7.Text = "label2"
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(3, 4)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(1, 22)
        Me.Label8.TabIndex = 41
        Me.Label8.Text = "label4"
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label12.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label12.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label12.Location = New System.Drawing.Point(225, 4)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(1, 22)
        Me.Label12.TabIndex = 40
        Me.Label12.Text = "label3"
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label16.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label16.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(3, 3)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(223, 1)
        Me.Label16.TabIndex = 39
        Me.Label16.Text = "label1"
        '
        'tlsTemplateGallery
        '
        Me.tlsTemplateGallery._IsExamAddedndum = False
        Me.tlsTemplateGallery.AddedndumExamProviderId = CType(0, Long)
        Me.tlsTemplateGallery.AutoSize = True
        Me.tlsTemplateGallery.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.tlsTemplateGallery.ButtonsToHide = CType(resources.GetObject("tlsTemplateGallery.ButtonsToHide"), System.Collections.ArrayList)
        Me.tlsTemplateGallery.ConnectionString = Nothing
        Me.tlsTemplateGallery.Dock = System.Windows.Forms.DockStyle.Top
        Me.tlsTemplateGallery.dtAssignUsers = Nothing
        Me.tlsTemplateGallery.dtInput = Nothing
        Me.tlsTemplateGallery.ExamProvider = Nothing
        Me.tlsTemplateGallery.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlsTemplateGallery.FormType = WordToolStrip.enumControlType.TemplateGallery
        Me.tlsTemplateGallery.IsCoSignEnabled = False
        Me.tlsTemplateGallery.IsEMAssociated = False
        Me.tlsTemplateGallery.Location = New System.Drawing.Point(0, 0)
        Me.tlsTemplateGallery.Name = "tlsTemplateGallery"
        Me.tlsTemplateGallery.ptProvider = Nothing
        Me.tlsTemplateGallery.ptProviderId = CType(0, Long)
        Me.tlsTemplateGallery.Size = New System.Drawing.Size(1263, 53)
        Me.tlsTemplateGallery.TabIndex = 16
        Me.tlsTemplateGallery.UserID = CType(0, Long)
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'Splitter2
        '
        Me.Splitter2.Location = New System.Drawing.Point(226, 53)
        Me.Splitter2.Name = "Splitter2"
        Me.Splitter2.Size = New System.Drawing.Size(3, 663)
        Me.Splitter2.TabIndex = 9
        Me.Splitter2.TabStop = False
        '
        'frmTemplateGallery
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1263, 716)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.pnlTemplate)
        Me.Controls.Add(Me.Splitter2)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.tlsTemplateGallery)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "frmTemplateGallery"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Template Gallery"
        Me.Panel1.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        CType(Me.wdTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel4.ResumeLayout(False)
        Me.pnlTemplate.ResumeLayout(False)
        Me.pnlTemplate.PerformLayout()
        Me.PnlSnomed.ResumeLayout(False)
        Me.PnlSnomed.PerformLayout()
        CType(Me.AxDgnDictCustom1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.Panel7.ResumeLayout(False)
        Me.Panel10.ResumeLayout(False)
        Me.Panel10.PerformLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel9.ResumeLayout(False)
        Me.Panel8.ResumeLayout(False)
        Me.Panel5.ResumeLayout(False)
        Me.Panel6.ResumeLayout(False)
        Me.Panel6.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region
    Private Sub frmTemplateGallery_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        If gblnVoiceEnabled = True And gblnSpeakerExists = True Then
            If Not IsNothing(ogloVoice) Then
                ogloVoice.ShowMicroPhone()
            End If
        End If
        Me.WindowState = FormWindowState.Maximized
    End Sub

    Private Sub frmTemplateGallery_Deactivate(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Deactivate
        If gblnVoiceEnabled = True And gblnSpeakerExists = True Then
            If Not IsNothing(ogloVoice) Then
                ogloVoice.TurnoffMicrophone()
            End If
        End If
        If blnIsrightclik = False Then
            Me.WindowState = FormWindowState.Minimized
        Else
            Me.WindowState = FormWindowState.Maximized
            blnIsrightclik = False
        End If
    End Sub

    Private Sub frmTemplateGallery_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed


        Try

            If (IsNothing(ttBibliography) = False) Then
                ttBibliography.Dispose()
                ttBibliography = Nothing
            End If

            If Not dtEMField Is Nothing Then
                dtEMField.Dispose()
                dtEMField = Nothing
            End If


            If Not oDatatrvDiscrete_NodeMouseDoubleClick Is Nothing Then
                oDatatrvDiscrete_NodeMouseDoubleClick.Dispose()
                oDatatrvDiscrete_NodeMouseDoubleClick = Nothing
            End If


            If Not dtFill_TemplateSpecility Is Nothing Then
                dtFill_TemplateSpecility.Dispose()
                dtFill_TemplateSpecility = Nothing
            End If

            If Not dtFill_Provider Is Nothing Then
                dtFill_Provider.Dispose()
                dtFill_Provider = Nothing
            End If

            If Not dtFill_Category Is Nothing Then
                dtFill_Category.Dispose()
                dtFill_Category = Nothing
            End If

            If Not objDataFillLiquidData Is Nothing Then
                objDataFillLiquidData.Dispose()
                objDataFillLiquidData = Nothing
            End If

            If Not IsNothing(dtdictionary) Then
                dtdictionary.Dispose()
                dtdictionary = Nothing
            End If
            If Not IsNothing(objclsTemplateGallery) Then
                objclsTemplateGallery.Dispose()
                objclsTemplateGallery = Nothing
            End If
            Me.Hide()
            If IsNothing(mycaller) = False Then
                If mycaller.CanSelect = True Then
                    mycaller.RefreshGrid()
                End If
            End If

            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Close, "Template Gallery Closed", gloAuditTrail.ActivityOutCome.Success)
            
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally

        End Try
    End Sub

    Private Sub frmTemplateGallery_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing

        gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.Close, "--------------Template Gallery Close Started------------", gloAuditTrail.ActivityOutCome.Success)


        If gblnVoiceEnabled = True And gblnSpeakerExists = True Then
            If Not IsNothing(ogloVoice) Then
                TurnoffMicrophone()
                ogloVoice.UnInitializeVoiceComponents()
            End If
        End If
        Try
            If Not oCurDoc Is Nothing Then
                If oCurDoc.Saved = False Then
                    Dim Result As Integer
                    Result = MessageBox.Show("Do you want to save the changes to template?", gstrMessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)

                    If Result = DialogResult.Yes Then

                        If SaveTemplate(True) = False Then

                            e.Cancel = True
                        End If
                    ElseIf Result = DialogResult.Cancel Then
                        '' Dont Close the Form
                        e.Cancel = True
                    ElseIf Result = DialogResult.No Then
                        e.Cancel = False
                        ' CancelClick = True
                    End If
                Else
                    oCurDoc.ActiveWindow.View.ShowBookmarks = False
                    e.Cancel = False
                    ' CancelClick = True
                End If
            Else
                e.Cancel = False
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub frmTemplateGallery_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Leave

    End Sub

    Dim ttBibliography As New ToolTip()
    Private Sub frmTemplateGallery_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Dim trvRootNode As TreeNode
        'Dim objTemplateGalleryView As frmTemplateGalleryView

        Try
            If (IsNothing(dtdictionary) = False) Then
                dtdictionary.Dispose()
                dtdictionary = Nothing
            End If
            dtdictionary = New DataTable() '' added for problem 00000187 to filter using like for Dictionary 
            dtdictionary.Columns.Add("ID", GetType(Long))
            dtdictionary.Columns.Add("Description", GetType(String))
            dtdictionary.Columns.Add("ParentNode", GetType(String))
            'code added by dipak 20091228 to set ConnectionString for customise
            tlsTemplateGallery.Dock = DockStyle.Top
            tlsTemplateGallery.FormType = WordToolStrip.enumControlType.TemplateGallery
            tlsTemplateGallery.ConnectionString = GetConnectionString()
            tlsTemplateGallery.UserID = gnLoginID
            'end code added by dipak 20091228
            Fill_Provider()
            Fill_Category()
            Fill_TemplateSpecility()
            Fill_DataDictionary()
            FillLiquidData()

            If m_ID <> 0 Then
                'If _bISOpenedFromDM = True Then
                '    If Not IsNothing(_DMNode) = True Then

                '        ''opened from dm
                '    Else
                '        Fill_TemplateGallery(m_ID)
                '    End If
                'Else
                '    Fill_TemplateGallery(m_ID)
                'End If

                Fill_TemplateGallery(m_ID)
                Dim strTemplateSpecility As String = GetTemplateSpecility(m_ID)
                cmbTemplateSpecility.Text = strTemplateSpecility
            Else
                If _bISOpenedFromDM = True Then
                    'dm
                    ' _DMNode = CType(Me.Parent, frmDM_Setup).DMSelectedNode
                    If Not IsNothing(_DMNode) = True Then

                        _DMTemplateName = _DMNode.Text
                        _TemplateName = _DMNode.Text
                        cmbCategory.Text = "Wellness Guidelines"
                        Fill_TemplateGallery()
                    End If
                Else
                    cmbCategory.Text = m_CategoryType
                    If m_ProviderID <> 0 Then
                        cmbProvider.SelectedValue = m_ProviderID
                    End If
                    LoadNewDocument()
                End If
            End If
            ' Dim nde As TreeNode
            ''Randomize()
            If cmbCategory.Text = "Tags" Then
                'chkAssociateEMCategory.Visible = True
                'lblAssociateEM.Visible = True

                PnlSnomed.Visible = True
                tlsTemplateGallery.IsEMAssociated = True
            Else
                pnlTemplate.Height = pnlTemplate.Height - PnlSnomed.Height
                PnlSnomed.Visible = False
                tlsTemplateGallery.IsEMAssociated = False
            End If

            If cmbCategory.Text = "MU Provider Reference" Then
                btnBibliography.Visible = True
            Else
                btnBibliography.Visible = False
            End If

            If gblnVoiceEnabled = True And gblnSpeakerExists = True Then
                InitializeVoiceObject()
                ShowMicroPhone()
            End If
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Open, "Template Gallery Opened", gloAuditTrail.ActivityOutCome.Success)
            'Dim objAudit As New clsAudit
            'objAudit.CreateLog(clsAudit.enmActivityType.Other, "Template Gallery Opened", gstrLoginName, gstrClientMachineName)
            'objAudit = Nothing

            'Tooltip added for bibliography button

            ttBibliography.SetToolTip(Me.btnBibliography, "Bibliographic and Developer Citation")
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
        txtsearchtemplate.Select()

    End Sub

    Public Sub FillLiquidData()

        Dim trvRootNode As TreeNode
        Dim trvChildNode As myTreeNode ''User defined tree node to hold custom values

        Try

            If Not objDataFillLiquidData Is Nothing Then
                objDataFillLiquidData.Dispose()
                objDataFillLiquidData = Nothing
            End If

            ''Get the Liquid data  for binding it to Treeview
            objDataFillLiquidData = objclsTemplateGallery.GetLiquidData
            If Not objDataFillLiquidData Is Nothing Then
                With trvDiscrete
                    .Nodes.Clear()
                    ''Add the default tree node
                    trvRootNode = New TreeNode
                    trvRootNode.Text = "Liquid Data"
                    trvRootNode.ImageIndex = 0
                    trvRootNode.SelectedImageIndex = 0
                    .Nodes.Add(trvRootNode)
                    ''Add the Data fileds under the parent node
                    For _inc As Int32 = 0 To objDataFillLiquidData.Rows.Count - 1
                        ' trvChildNode = New myTreeNode
                        Dim _Flag As Int64 = 0
                        'check for Mandatory field and set the flag
                        If objDataFillLiquidData.Rows(_inc)("bIsMandatory") = True Then
                            _Flag = 1
                        End If
                        ''Key Refers to Element Id, Strname refers to Element name, _Flag refers to IsMandatory 
                        trvChildNode = New myTreeNode(objDataFillLiquidData.Rows(_inc)("sElementName"), objDataFillLiquidData.Rows(_inc)("nElementID"))
                        trvChildNode.ImageIndex = 2
                        trvChildNode.SelectedImageIndex = 2
                        trvChildNode.NodeName = objDataFillLiquidData.Rows(_inc)("sElementType")
                        trvRootNode.Nodes.Add(trvChildNode)
                        trvChildNode = Nothing
                    Next
                    .SelectedNode = trvRootNode
                    .ExpandAll()
                End With

            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "Liquid Data", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally
            trvRootNode = Nothing
        End Try

    End Sub
    Private Sub Fill_TemplateGallery(ByVal ID As Long)

        Dim dv As DataView

        Dim dt As DataTable = objclsTemplateGallery.SelectTemplateGallery(ID)
        dv = objclsTemplateGallery.GetDataview
        If (IsNothing(dt) = False) Then
            dt.Dispose()
            dt = Nothing
        End If
        txtTemplateName.Text = dv.Item(0)(0).ToString
        _TemplateName = dv.Item(0)(0).ToString

        cmbCategory.SelectedValue = dv.Item(0)(1)
        _CategoryID = dv.Item(0)(1)


        sBibliography = dv.Item(0)("sBibliographicinfo").ToString()
        sDeveloper = dv.Item(0)("sBibliographicDeveloper").ToString()
        cmbProvider.SelectedValue = dv.Item(0)(2)
        strSnomedCode = Convert.ToString(dv.Item(0)(6))
        strSnomedDescription = Convert.ToString(dv.Item(0)(7))
        If Convert.ToString(dv.Item(0)(6)) <> "" And Convert.ToString(dv.Item(0)(7)) <> "" Then
            txtSNOMEDCode.Text = Convert.ToString(dv.Item(0)(6)) + " - " + Convert.ToString(dv.Item(0)(7))
        End If
        Dim strFileName As String = ExamNewDocumentName
        Dim ObjWord As New clsWordDocument
        strFileName = ObjWord.GenerateFile(dv.Item(0)(3), strFileName)
        ObjWord = Nothing
        If strFileName <> "" Then
            LoadWordControl(strFileName)
        End If
        If (IsNothing(dtEMField) = False) Then
            dtEMField.Dispose()
            dtEMField = Nothing
        End If


        dtEMField = objclsTemplateGallery.GetEMAssociatedField(ID)
        FillEMArraylist(dtEMField)



    End Sub

    Public Function GetTemplateSpecility(ByVal ID As Long) As String

        Dim Con As SqlConnection = Nothing

        Try

            Dim TemplateSpecility As String

            Con = New System.Data.SqlClient.SqlConnection(GetConnectionString)

            Dim cmd As New SqlCommand("gsp_GetTemplateSpecility", Con)
            cmd.CommandType = CommandType.StoredProcedure
            Dim sqlParam As SqlParameter

            sqlParam = cmd.Parameters.Add("@TemplateID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = ID

            sqlParam = cmd.Parameters.Add("@TemplateSpecility", SqlDbType.VarChar, 50)
            sqlParam.Direction = ParameterDirection.Output

            Con.Open()
            cmd.ExecuteNonQuery()
            TemplateSpecility = sqlParam.Value()
            cmd.Parameters.Clear()
            cmd.Dispose()
            cmd = Nothing
            Con.Close()
            sqlParam = Nothing
            Return TemplateSpecility

        Catch ex As SqlException
            Return ""

        Catch ex As Exception
            Return ""

        Finally
            If (IsNothing(Con) = False) Then
                Con.Dispose()
                Con = Nothing
            End If



        End Try
    End Function


    Private Sub Fill_TemplateGallery()

        txtTemplateName.Text = _TemplateName ' dv.Item(0)(0).ToString

        Dim strFileName As String = ExamNewDocumentName
        Dim ObjWord As New clsWordDocument
        strFileName = ObjWord.GenerateFile(_DMNode.DMTemplate, strFileName)
        ObjWord = Nothing
        If strFileName <> "" Then
            LoadWordControl(strFileName)
        End If


    End Sub


    Public Sub FillEMArraylist(ByVal _dt As DataTable)
        ' Dim Emylist As myList
        Dim oListItem As gloGeneralItem.gloItem
        If Not IsNothing(_dt) Then
            For i As Integer = 0 To _dt.Rows.Count - 1
                ''Labs
                If _dt.Rows(i)("sAssociatedEMName") = "IncisionalBiopsyRoutine" And _dt.Rows(i)("sAssociatedEMCategory") = strLabs.ToString() Then

                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "IncisionalBiopsyRoutine"
                    'Emylist.AssociatedCategory = strLabs.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrLabs.Add(Emylist)
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "IncisionalBiopsyRoutine"
                    oListItem.Code = strLabs.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrLabs.Add(oListItem)
                    oListItem.Dispose()
                End If
                If _dt.Rows(i)("sAssociatedEMName") = "SuperficialBiopsyRoutine" And _dt.Rows(i)("sAssociatedEMCategory") = strLabs.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "SuperficialBiopsyRoutine"
                    oListItem.Code = strLabs.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrLabs.Add(oListItem)
                    oListItem.Dispose()

                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "SuperficialBiopsyRoutine"
                    'Emylist.AssociatedCategory = strLabs.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrLabs.Add(Emylist)
                End If
                If _dt.Rows(i)("sAssociatedEMName") = "TypeCrossmatchRoutine" And _dt.Rows(i)("sAssociatedEMCategory") = strLabs.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "TypeCrossmatchRoutine"
                    oListItem.Code = strLabs.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrLabs.Add(oListItem)
                    oListItem.Dispose()

                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "TypeCrossmatchRoutine"
                    'Emylist.AssociatedCategory = strLabs.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrLabs.Add(Emylist)
                End If

                If _dt.Rows(i)("sAssociatedEMName") = "PTRoutine" And _dt.Rows(i)("sAssociatedEMCategory") = strLabs.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "PTRoutine"
                    oListItem.Code = strLabs.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrLabs.Add(oListItem)
                    oListItem.Dispose()

                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "PTRoutine"
                    'Emylist.AssociatedCategory = strLabs.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrLabs.Add(Emylist)
                End If

                If _dt.Rows(i)("sAssociatedEMName") = "ABGsRoutine" And _dt.Rows(i)("sAssociatedEMCategory") = strLabs.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "ABGsRoutine"
                    oListItem.Code = strLabs.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrLabs.Add(oListItem)
                    oListItem.Dispose()

                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "ABGsRoutine"
                    'Emylist.AssociatedCategory = strLabs.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrLabs.Add(Emylist)
                End If
                If _dt.Rows(i)("sAssociatedEMName") = "CardiacEnzymesRoutine" And _dt.Rows(i)("sAssociatedEMCategory") = strLabs.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "CardiacEnzymesRoutine"
                    oListItem.Code = strLabs.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrLabs.Add(oListItem)
                    oListItem.Dispose()

                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "CardiacEnzymesRoutine"
                    'Emylist.AssociatedCategory = strLabs.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrLabs.Add(Emylist)
                End If
                If _dt.Rows(i)("sAssociatedEMName") = "ChemicalProfileRoutine" And _dt.Rows(i)("sAssociatedEMCategory") = strLabs.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "ChemicalProfileRoutine"
                    oListItem.Code = strLabs.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrLabs.Add(oListItem)
                    oListItem.Dispose()

                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "ChemicalProfileRoutine"
                    'Emylist.AssociatedCategory = strLabs.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrLabs.Add(Emylist)
                End If
                If _dt.Rows(i)("sAssociatedEMName") = "DrugScreenRoutine" And _dt.Rows(i)("sAssociatedEMCategory") = strLabs.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "DrugScreenRoutine"
                    oListItem.Code = strLabs.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrLabs.Add(oListItem)
                    oListItem.Dispose()

                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "DrugScreenRoutine"
                    'Emylist.AssociatedCategory = strLabs.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrLabs.Add(Emylist)
                End If
                If _dt.Rows(i)("sAssociatedEMName") = "ElectrolytesRoutine" And _dt.Rows(i)("sAssociatedEMCategory") = strLabs.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "ElectrolytesRoutine"
                    oListItem.Code = strLabs.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrLabs.Add(oListItem)
                    oListItem.Dispose()

                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "ElectrolytesRoutine"
                    'Emylist.AssociatedCategory = strLabs.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrLabs.Add(Emylist)
                End If

                If _dt.Rows(i)("sAssociatedEMName") = "BunCreatinineRoutine" And _dt.Rows(i)("sAssociatedEMCategory") = strLabs.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "BunCreatinineRoutine"
                    oListItem.Code = strLabs.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrLabs.Add(oListItem)
                    oListItem.Dispose()

                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "BunCreatinineRoutine"
                    'Emylist.AssociatedCategory = strLabs.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrLabs.Add(Emylist)
                End If
                If _dt.Rows(i)("sAssociatedEMName") = "AmylaseRoutine" And _dt.Rows(i)("sAssociatedEMCategory") = strLabs.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "AmylaseRoutine"
                    oListItem.Code = strLabs.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrLabs.Add(oListItem)
                    oListItem.Dispose()

                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "AmylaseRoutine"
                    'Emylist.AssociatedCategory = strLabs.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrLabs.Add(Emylist)
                End If

                If _dt.Rows(i)("sAssociatedEMName") = "PregnancyTestRoutine" And _dt.Rows(i)("sAssociatedEMCategory") = strLabs.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "PregnancyTestRoutine"
                    oListItem.Code = strLabs.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrLabs.Add(oListItem)
                    oListItem.Dispose()

                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "PregnancyTestRoutine"
                    'Emylist.AssociatedCategory = strLabs.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrLabs.Add(Emylist)
                End If
                If _dt.Rows(i)("sAssociatedEMName") = "FluStrepMonoRoutine" And _dt.Rows(i)("sAssociatedEMCategory") = strLabs.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "FluStrepMonoRoutine"
                    oListItem.Code = strLabs.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrLabs.Add(oListItem)
                    oListItem.Dispose()

                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "FluStrepMonoRoutine"
                    'Emylist.AssociatedCategory = strLabs.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrLabs.Add(Emylist)
                End If

                If _dt.Rows(i)("sAssociatedEMName") = "CbcUaRoutine" And _dt.Rows(i)("sAssociatedEMCategory") = strLabs.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "CbcUaRoutine"
                    oListItem.Code = strLabs.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrLabs.Add(oListItem)
                    oListItem.Dispose()

                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "CbcUaRoutine"
                    'Emylist.AssociatedCategory = strLabs.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrLabs.Add(Emylist)
                End If
                If _dt.Rows(i)("sAssociatedEMName") = "IndependentVisualTest" And _dt.Rows(i)("sAssociatedEMCategory") = strLabs.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "IndependentVisualTest"
                    oListItem.Code = strLabs.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrLabs.Add(oListItem)
                    oListItem.Dispose()

                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "IndependentVisualTest"
                    'Emylist.AssociatedCategory = strLabs.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrLabs.Add(Emylist)
                End If
                If _dt.Rows(i)("sAssociatedEMName") = "DiscussionWPerformingPhys" And _dt.Rows(i)("sAssociatedEMCategory") = strLabs.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "DiscussionWPerformingPhys"
                    oListItem.Code = strLabs.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrLabs.Add(oListItem)
                    oListItem.Dispose()

                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "DiscussionWPerformingPhys"
                    'Emylist.AssociatedCategory = strLabs.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrLabs.Add(Emylist)
                End If

                If _dt.Rows(i)("sAssociatedEMName") = "OtherLabsCount" And _dt.Rows(i)("sAssociatedEMCategory") = strLabs.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "OtherLabsCount"
                    oListItem.Code = strLabs.ToString()
                    oListItem.Status = _dt.Rows(i)("sStatus").ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrLabs.Add(oListItem)
                    oListItem.Dispose()

                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "OtherLabsCount"
                    'Emylist.AssociatedCategory = strLabs.ToString()
                    'Emylist.AssociatedItem = _dt.Rows(i)("sStatus").ToString()
                    '_arrLabs.Add(Emylist)
                End If


                ''orders

                If _dt.Rows(i)("sAssociatedEMName") = "VascularStudiesWRiskRoutine" And _dt.Rows(i)("sAssociatedEMCategory") = strOrders.ToString() Then


                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "VascularStudiesWRiskRoutine"
                    'Emylist.AssociatedCategory = strOrders.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrOrders.Add(Emylist)
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "VascularStudiesWRiskRoutine"
                    oListItem.Code = strOrders.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrOrders.Add(oListItem)
                    oListItem.Dispose()
                End If
                If _dt.Rows(i)("sAssociatedEMName") = "VascularStudiesRoutine" And _dt.Rows(i)("sAssociatedEMCategory") = strOrders.ToString() Then


                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "VascularStudiesRoutine"
                    'Emylist.AssociatedCategory = strOrders.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrOrders.Add(Emylist)

                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "VascularStudiesRoutine"
                    oListItem.Code = strOrders.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrOrders.Add(oListItem)
                    oListItem.Dispose()
                End If

                If _dt.Rows(i)("sAssociatedEMName") = "MRIRoutine" And _dt.Rows(i)("sAssociatedEMCategory") = strOrders.ToString() Then


                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "MRIRoutine"
                    'Emylist.AssociatedCategory = strOrders.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrOrders.Add(Emylist)

                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "MRIRoutine"
                    oListItem.Code = strOrders.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrOrders.Add(oListItem)
                    oListItem.Dispose()
                End If
                If _dt.Rows(i)("sAssociatedEMName") = "CATScanRoutine" And _dt.Rows(i)("sAssociatedEMCategory") = strOrders.ToString() Then


                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "CATScanRoutine"
                    'Emylist.AssociatedCategory = strOrders.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrOrders.Add(Emylist)

                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "CATScanRoutine"
                    oListItem.Code = strOrders.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrOrders.Add(oListItem)
                    oListItem.Dispose()
                End If
                If _dt.Rows(i)("sAssociatedEMName") = "IVPRoutine" And _dt.Rows(i)("sAssociatedEMCategory") = strOrders.ToString() Then


                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "IVPRoutine"
                    'Emylist.AssociatedCategory = strOrders.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrOrders.Add(Emylist)

                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "IVPRoutine"
                    oListItem.Code = strOrders.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrOrders.Add(oListItem)
                    oListItem.Dispose()
                End If

                If _dt.Rows(i)("sAssociatedEMName") = "GIGallbladderRoutine" And _dt.Rows(i)("sAssociatedEMCategory") = strOrders.ToString() Then


                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "GIGallbladderRoutine"
                    'Emylist.AssociatedCategory = strOrders.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrOrders.Add(Emylist)

                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "GIGallbladderRoutine"
                    oListItem.Code = strOrders.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrOrders.Add(oListItem)
                    oListItem.Dispose()
                End If
                If _dt.Rows(i)("sAssociatedEMName") = "TLSpineRoutine" And _dt.Rows(i)("sAssociatedEMCategory") = strOrders.ToString() Then


                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "TLSpineRoutine"
                    'Emylist.AssociatedCategory = strOrders.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrOrders.Add(Emylist)
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "TLSpineRoutine"
                    oListItem.Code = strOrders.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrOrders.Add(oListItem)
                    oListItem.Dispose()
                End If
                If _dt.Rows(i)("sAssociatedEMName") = "DiscographyRoutine" And _dt.Rows(i)("sAssociatedEMCategory") = strOrders.ToString() Then


                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "DiscographyRoutine"
                    'Emylist.AssociatedCategory = strOrders.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrOrders.Add(Emylist)

                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "DiscographyRoutine"
                    oListItem.Code = strOrders.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrOrders.Add(oListItem)
                    oListItem.Dispose()
                End If
                If _dt.Rows(i)("sAssociatedEMName") = "DiagUltrasoundRoutine" And _dt.Rows(i)("sAssociatedEMCategory") = strOrders.ToString() Then


                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "DiagUltrasoundRoutine"
                    'Emylist.AssociatedCategory = strOrders.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrOrders.Add(Emylist)

                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "DiagUltrasoundRoutine"
                    oListItem.Code = strOrders.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrOrders.Add(oListItem)
                    oListItem.Dispose()
                End If
                If _dt.Rows(i)("sAssociatedEMName") = "CSpineRoutine" And _dt.Rows(i)("sAssociatedEMCategory") = strOrders.ToString() Then


                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "CSpineRoutine"
                    'Emylist.AssociatedCategory = strOrders.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrOrders.Add(Emylist)

                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "CSpineRoutine"
                    oListItem.Code = strOrders.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrOrders.Add(oListItem)
                    oListItem.Dispose()
                End If
                If _dt.Rows(i)("sAssociatedEMName") = "HipPelvisRoutine" And _dt.Rows(i)("sAssociatedEMCategory") = strOrders.ToString() Then

                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "HipPelvisRoutine"
                    'Emylist.AssociatedCategory = strOrders.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrOrders.Add(Emylist)

                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "HipPelvisRoutine"
                    oListItem.Code = strOrders.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrOrders.Add(oListItem)
                    oListItem.Dispose()
                End If
                If _dt.Rows(i)("sAssociatedEMName") = "AbdomenRoutine" And _dt.Rows(i)("sAssociatedEMCategory") = strOrders.ToString() Then


                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "AbdomenRoutine"
                    'Emylist.AssociatedCategory = strOrders.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrOrders.Add(Emylist)

                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "AbdomenRoutine"
                    oListItem.Code = strOrders.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrOrders.Add(oListItem)
                    oListItem.Dispose()
                End If
                If _dt.Rows(i)("sAssociatedEMName") = "ExtremitiesRoutine" And _dt.Rows(i)("sAssociatedEMCategory") = strOrders.ToString() Then


                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "ExtremitiesRoutine"
                    'Emylist.AssociatedCategory = strOrders.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrOrders.Add(Emylist)

                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "ExtremitiesRoutine"
                    oListItem.Code = strOrders.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrOrders.Add(oListItem)
                    oListItem.Dispose()
                End If
                If _dt.Rows(i)("sAssociatedEMName") = "ChestRoutine" And _dt.Rows(i)("sAssociatedEMCategory") = strOrders.ToString() Then

                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "ChestRoutine"
                    'Emylist.AssociatedCategory = strOrders.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrOrders.Add(Emylist)

                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "ChestRoutine"
                    oListItem.Code = strOrders.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrOrders.Add(oListItem)
                    oListItem.Dispose()
                End If
                If _dt.Rows(i)("sAssociatedEMName") = "IndependentVisualTest" And _dt.Rows(i)("sAssociatedEMCategory") = strOrders.ToString() Then


                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "IndependentVisualTest"
                    'Emylist.AssociatedCategory = strOrders.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrOrders.Add(Emylist)

                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "IndependentVisualTest"
                    oListItem.Code = strOrders.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrOrders.Add(oListItem)
                    oListItem.Dispose()
                End If
                If _dt.Rows(i)("sAssociatedEMName") = "DiscussWPerformingPhys" And _dt.Rows(i)("sAssociatedEMCategory") = strOrders.ToString() Then


                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "DiscussWPerformingPhys"
                    'Emylist.AssociatedCategory = strOrders.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrOrders.Add(Emylist)

                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "DiscussWPerformingPhys"
                    oListItem.Code = strOrders.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrOrders.Add(oListItem)
                    oListItem.Dispose()
                End If
                If _dt.Rows(i)("sAssociatedEMName") = "OtherXRaysCount" And _dt.Rows(i)("sAssociatedEMCategory") = strOrders.ToString() Then

                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "OtherXRaysCount"
                    'Emylist.AssociatedCategory = strOrders.ToString()
                    'Emylist.AssociatedItem = _dt.Rows(i)("sStatus").ToString
                    '_arrOrders.Add(Emylist)

                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "OtherXRaysCount"
                    oListItem.Code = strOrders.ToString()
                    oListItem.Status = _dt.Rows(i)("sStatus").ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrOrders.Add(oListItem)
                    oListItem.Dispose()
                End If

                ''''Other Diagnosis

                If _dt.Rows(i)("sAssociatedEMName") = "EndoscopeWRiskRoutine" And _dt.Rows(i)("sAssociatedEMCategory") = strOtherDiagnosis.ToString() Then
                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "EndoscopeWRiskRoutine"
                    'Emylist.AssociatedCategory = strOtherDiagnosis.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrOtherDiag.Add(Emylist)

                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "EndoscopeWRiskRoutine"
                    oListItem.Code = strOtherDiagnosis.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrOtherDiag.Add(oListItem)
                    oListItem.Dispose()

                End If
                If _dt.Rows(i)("sAssociatedEMName") = "EndoscopeRoutine" And _dt.Rows(i)("sAssociatedEMCategory") = strOtherDiagnosis.ToString() Then
                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "EndoscopeRoutine"
                    'Emylist.AssociatedCategory = strOtherDiagnosis.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrOtherDiag.Add(Emylist)

                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "EndoscopeRoutine"
                    oListItem.Code = strOtherDiagnosis.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrOtherDiag.Add(oListItem)
                    oListItem.Dispose()

                End If
                If _dt.Rows(i)("sAssociatedEMName") = "CuldocentesesRoutine" And _dt.Rows(i)("sAssociatedEMCategory") = strOtherDiagnosis.ToString() Then
                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "CuldocentesesRoutine"
                    'Emylist.AssociatedCategory = strOtherDiagnosis.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrOtherDiag.Add(Emylist)
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "CuldocentesesRoutine"
                    oListItem.Code = strOtherDiagnosis.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrOtherDiag.Add(oListItem)
                    oListItem.Dispose()
                End If
                If _dt.Rows(i)("sAssociatedEMName") = "ThoracentesisRoutine" And _dt.Rows(i)("sAssociatedEMCategory") = strOtherDiagnosis.ToString() Then
                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "ThoracentesisRoutine"
                    'Emylist.AssociatedCategory = strOtherDiagnosis.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrOtherDiag.Add(Emylist)
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "ThoracentesisRoutine"
                    oListItem.Code = strOtherDiagnosis.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrOtherDiag.Add(oListItem)
                    oListItem.Dispose()
                End If
                If _dt.Rows(i)("sAssociatedEMName") = "LumbarPunctureRoutine" And _dt.Rows(i)("sAssociatedEMCategory") = strOtherDiagnosis.ToString() Then
                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "LumbarPunctureRoutine"
                    'Emylist.AssociatedCategory = strOtherDiagnosis.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrOtherDiag.Add(Emylist)
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "LumbarPunctureRoutine"
                    oListItem.Code = strOtherDiagnosis.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrOtherDiag.Add(oListItem)
                    oListItem.Dispose()
                End If
                If _dt.Rows(i)("sAssociatedEMName") = "NuclearScanRoutine" And _dt.Rows(i)("sAssociatedEMCategory") = strOtherDiagnosis.ToString() Then
                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "NuclearScanRoutine"
                    'Emylist.AssociatedCategory = strOtherDiagnosis.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrOtherDiag.Add(Emylist)

                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "NuclearScanRoutine"
                    oListItem.Code = strOtherDiagnosis.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrOtherDiag.Add(oListItem)
                    oListItem.Dispose()
                End If
                If _dt.Rows(i)("sAssociatedEMName") = "PulmonaryStudiesRoutine" And _dt.Rows(i)("sAssociatedEMCategory") = strOtherDiagnosis.ToString() Then
                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "PulmonaryStudiesRoutine"
                    'Emylist.AssociatedCategory = strOtherDiagnosis.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrOtherDiag.Add(Emylist)

                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "PulmonaryStudiesRoutine"
                    oListItem.Code = strOtherDiagnosis.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrOtherDiag.Add(oListItem)
                    oListItem.Dispose()
                End If
                If _dt.Rows(i)("sAssociatedEMName") = "DopplerFlowStudiesRoutine" And _dt.Rows(i)("sAssociatedEMCategory") = strOtherDiagnosis.ToString() Then
                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "DopplerFlowStudiesRoutine"
                    'Emylist.AssociatedCategory = strOtherDiagnosis.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrOtherDiag.Add(Emylist)
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "DopplerFlowStudiesRoutine"
                    oListItem.Code = strOtherDiagnosis.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrOtherDiag.Add(oListItem)
                    oListItem.Dispose()
                End If
                If _dt.Rows(i)("sAssociatedEMName") = "VectorcardiogramRoutine" And _dt.Rows(i)("sAssociatedEMCategory") = strOtherDiagnosis.ToString() Then
                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "VectorcardiogramRoutine"
                    'Emylist.AssociatedCategory = strOtherDiagnosis.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrOtherDiag.Add(Emylist)

                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "VectorcardiogramRoutine"
                    oListItem.Code = strOtherDiagnosis.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrOtherDiag.Add(oListItem)
                    oListItem.Dispose()
                End If
                If _dt.Rows(i)("sAssociatedEMName") = "EegEmgRoutine" And _dt.Rows(i)("sAssociatedEMCategory") = strOtherDiagnosis.ToString() Then
                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "EegEmgRoutine"
                    'Emylist.AssociatedCategory = strOtherDiagnosis.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrOtherDiag.Add(Emylist)

                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "EegEmgRoutine"
                    oListItem.Code = strOtherDiagnosis.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrOtherDiag.Add(oListItem)
                    oListItem.Dispose()
                End If
                If _dt.Rows(i)("sAssociatedEMName") = "TreadmillStressTestRoutine" And _dt.Rows(i)("sAssociatedEMCategory") = strOtherDiagnosis.ToString() Then
                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "TreadmillStressTestRoutine"
                    'Emylist.AssociatedCategory = strOtherDiagnosis.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrOtherDiag.Add(Emylist)

                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "TreadmillStressTestRoutine"
                    oListItem.Code = strOtherDiagnosis.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrOtherDiag.Add(oListItem)
                    oListItem.Dispose()
                End If
                If _dt.Rows(i)("sAssociatedEMName") = "HolterMonitorRoutine" And _dt.Rows(i)("sAssociatedEMCategory") = strOtherDiagnosis.ToString() Then
                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "HolterMonitorRoutine"
                    'Emylist.AssociatedCategory = strOtherDiagnosis.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrOtherDiag.Add(Emylist)

                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "HolterMonitorRoutine"
                    oListItem.Code = strOtherDiagnosis.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrOtherDiag.Add(oListItem)
                    oListItem.Dispose()
                End If
                If _dt.Rows(i)("sAssociatedEMName") = "EkgEcgRoutine" And _dt.Rows(i)("sAssociatedEMCategory") = strOtherDiagnosis.ToString() Then
                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "EkgEcgRoutine"
                    'Emylist.AssociatedCategory = strOtherDiagnosis.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrOtherDiag.Add(Emylist)

                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "EkgEcgRoutine"
                    oListItem.Code = strOtherDiagnosis.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrOtherDiag.Add(oListItem)
                    oListItem.Dispose()
                End If
                If _dt.Rows(i)("sAssociatedEMName") = "IndependentVisualTest" And _dt.Rows(i)("sAssociatedEMCategory") = strOtherDiagnosis.ToString() Then
                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "IndependentVisualTest"
                    'Emylist.AssociatedCategory = strOtherDiagnosis.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrOtherDiag.Add(Emylist)

                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "IndependentVisualTest"
                    oListItem.Code = strOtherDiagnosis.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrOtherDiag.Add(oListItem)
                    oListItem.Dispose()
                End If
                If _dt.Rows(i)("sAssociatedEMName") = "DiscussWPerformingPhys" And _dt.Rows(i)("sAssociatedEMCategory") = strOtherDiagnosis.ToString() Then
                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "IDiscussWPerformingPhys"
                    'Emylist.AssociatedCategory = strOtherDiagnosis.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrOtherDiag.Add(Emylist)

                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "DiscussWPerformingPhys"
                    oListItem.Code = strOtherDiagnosis.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrOtherDiag.Add(oListItem)
                    oListItem.Dispose()
                End If

                If _dt.Rows(i)("sAssociatedEMName") = "OtherDiagnosticStudiesCount" And _dt.Rows(i)("sAssociatedEMCategory") = strOtherDiagnosis.ToString() Then
                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "OtherDiagnosticStudiesCount"
                    'Emylist.AssociatedCategory = strOtherDiagnosis.ToString()
                    'Emylist.AssociatedItem = _dt.Rows(i)("sStatus").ToString()
                    '_arrOtherDiag.Add(Emylist)

                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "OtherDiagnosticStudiesCount"
                    oListItem.Code = strOtherDiagnosis.ToString()
                    oListItem.Status = _dt.Rows(i)("sStatus").ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrOtherDiag.Add(oListItem)
                    oListItem.Dispose()
                End If




                '''' Managment Option
                If _dt.Rows(i)("sAssociatedEMName") = "DiscussCaseWHealthProvider" And _dt.Rows(i)("sAssociatedEMCategory") = strMangementOption.ToString() Then
                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "DiscussCaseWHealthProvider"
                    'Emylist.AssociatedCategory = strMangementOption.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrManagment.Add(Emylist)
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "DiscussCaseWHealthProvider"
                    oListItem.Code = strMangementOption.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrManagment.Add(oListItem)
                    oListItem.Dispose()
                End If
                If _dt.Rows(i)("sAssociatedEMName") = "ReviewMedicalRecsOther" And _dt.Rows(i)("sAssociatedEMCategory") = strMangementOption.ToString() Then
                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "ReviewMedicalRecsOther"
                    'Emylist.AssociatedCategory = strMangementOption.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrManagment.Add(Emylist)
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "ReviewMedicalRecsOther"
                    oListItem.Code = strMangementOption.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrManagment.Add(oListItem)
                    oListItem.Dispose()
                End If
                If _dt.Rows(i)("sAssociatedEMName") = "DecisionObtainMedicalRecsOther" And _dt.Rows(i)("sAssociatedEMCategory") = strMangementOption.ToString() Then
                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "DecisionObtainMedicalRecsOther"
                    'Emylist.AssociatedCategory = strMangementOption.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrManagment.Add(Emylist)
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "DecisionObtainMedicalRecsOther"
                    oListItem.Code = strMangementOption.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrManagment.Add(oListItem)
                    oListItem.Dispose()
                End If
                If _dt.Rows(i)("sAssociatedEMName") = "DecisionNotResuscitate" And _dt.Rows(i)("sAssociatedEMCategory") = strMangementOption.ToString() Then
                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "DecisionNotResuscitate"
                    'Emylist.AssociatedCategory = strMangementOption.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrManagment.Add(Emylist)
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "DecisionNotResuscitate"
                    oListItem.Code = strMangementOption.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrManagment.Add(oListItem)
                    oListItem.Dispose()
                End If
                If _dt.Rows(i)("sAssociatedEMName") = "MajorEmergencySurgery" And _dt.Rows(i)("sAssociatedEMCategory") = strMangementOption.ToString() Then
                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "MajorEmergencySurgery"
                    'Emylist.AssociatedCategory = strMangementOption.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrManagment.Add(Emylist)
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "MajorEmergencySurgery"
                    oListItem.Code = strMangementOption.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrManagment.Add(oListItem)
                    oListItem.Dispose()
                End If
                If _dt.Rows(i)("sAssociatedEMName") = "MajorSurgeryWRiskFactors" And _dt.Rows(i)("sAssociatedEMCategory") = strMangementOption.ToString() Then
                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "MajorSurgeryWRiskFactors"
                    'Emylist.AssociatedCategory = strMangementOption.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrManagment.Add(Emylist)

                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "MajorSurgeryWRiskFactors"
                    oListItem.Code = strMangementOption.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrManagment.Add(oListItem)
                    oListItem.Dispose()
                End If
                If _dt.Rows(i)("sAssociatedEMName") = "MajorSurgery" And _dt.Rows(i)("sAssociatedEMCategory") = strMangementOption.ToString() Then
                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "MajorSurgery"
                    'Emylist.AssociatedCategory = strMangementOption.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrManagment.Add(Emylist)

                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "MajorSurgery"
                    oListItem.Code = strMangementOption.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrManagment.Add(oListItem)
                    oListItem.Dispose()
                End If
                If _dt.Rows(i)("sAssociatedEMName") = "MinorSurgeryWRiskFactors" And _dt.Rows(i)("sAssociatedEMCategory") = strMangementOption.ToString() Then
                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "MinorSurgeryWRiskFactors"
                    'Emylist.AssociatedCategory = strMangementOption.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrManagment.Add(Emylist)
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "MinorSurgeryWRiskFactors"
                    oListItem.Code = strMangementOption.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrManagment.Add(oListItem)
                    oListItem.Dispose()
                End If
                If _dt.Rows(i)("sAssociatedEMName") = "MinorSurgery" And _dt.Rows(i)("sAssociatedEMCategory") = strMangementOption.ToString() Then
                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "MinorSurgery"
                    'Emylist.AssociatedCategory = strMangementOption.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrManagment.Add(Emylist)
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "MinorSurgery"
                    oListItem.Code = strMangementOption.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrManagment.Add(oListItem)
                    oListItem.Dispose()
                End If
                If _dt.Rows(i)("sAssociatedEMName") = "ClosedFx" And _dt.Rows(i)("sAssociatedEMCategory") = strMangementOption.ToString() Then
                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "ClosedFx"
                    'Emylist.AssociatedCategory = strMangementOption.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrManagment.Add(Emylist)
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "ClosedFx"
                    oListItem.Code = strMangementOption.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrManagment.Add(oListItem)
                    oListItem.Dispose()
                End If
                If _dt.Rows(i)("sAssociatedEMName") = "PhysicalTherapy" And _dt.Rows(i)("sAssociatedEMCategory") = strMangementOption.ToString() Then
                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "PhysicalTherapy"
                    'Emylist.AssociatedCategory = strMangementOption.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrManagment.Add(Emylist)
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "PhysicalTherapy"
                    oListItem.Code = strMangementOption.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrManagment.Add(oListItem)
                    'oListItem.Dispose()
                End If
                If _dt.Rows(i)("sAssociatedEMName") = "NuclearMedicine" And _dt.Rows(i)("sAssociatedEMCategory") = strMangementOption.ToString() Then
                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "NuclearMedicine"
                    'Emylist.AssociatedCategory = strMangementOption.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrManagment.Add(Emylist)
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "NuclearMedicine"
                    oListItem.Code = strMangementOption.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrManagment.Add(oListItem)
                    oListItem.Dispose()
                End If
                If _dt.Rows(i)("sAssociatedEMName") = "RespiratoryTreatments" And _dt.Rows(i)("sAssociatedEMCategory") = strMangementOption.ToString() Then
                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "RespiratoryTreatments"
                    'Emylist.AssociatedCategory = strMangementOption.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrManagment.Add(Emylist)
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "RespiratoryTreatments"
                    oListItem.Code = strMangementOption.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrManagment.Add(oListItem)
                    oListItem.Dispose()
                End If
                If _dt.Rows(i)("sAssociatedEMName") = "Telemetry" And _dt.Rows(i)("sAssociatedEMCategory") = strMangementOption.ToString() Then
                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "Telemetry"
                    'Emylist.AssociatedCategory = strMangementOption.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrManagment.Add(Emylist)
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "Telemetry"
                    oListItem.Code = strMangementOption.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrManagment.Add(oListItem)
                    oListItem.Dispose()
                End If
                If _dt.Rows(i)("sAssociatedEMName") = "HighRiskMeds" And _dt.Rows(i)("sAssociatedEMCategory") = strMangementOption.ToString() Then
                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "HighRiskMeds"
                    'Emylist.AssociatedCategory = strMangementOption.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrManagment.Add(Emylist)
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "HighRiskMeds"
                    oListItem.Code = strMangementOption.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrManagment.Add(oListItem)
                    oListItem.Dispose()
                End If
                If _dt.Rows(i)("sAssociatedEMName") = "IVMedsWAdditives" And _dt.Rows(i)("sAssociatedEMCategory") = strMangementOption.ToString() Then
                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "IVMedsWAdditives"
                    'Emylist.AssociatedCategory = strMangementOption.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrManagment.Add(Emylist)
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "IVMedsWAdditives"
                    oListItem.Code = strMangementOption.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrManagment.Add(oListItem)
                    oListItem.Dispose()
                End If
                If _dt.Rows(i)("sAssociatedEMName") = "IVMeds" And _dt.Rows(i)("sAssociatedEMCategory") = strMangementOption.ToString() Then
                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "IVMeds"
                    'Emylist.AssociatedCategory = strMangementOption.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrManagment.Add(Emylist)
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "IVMeds"
                    oListItem.Code = strMangementOption.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrManagment.Add(oListItem)
                    oListItem.Dispose()
                End If
                If _dt.Rows(i)("sAssociatedEMName") = "PrescripIMMeds" And _dt.Rows(i)("sAssociatedEMCategory") = strMangementOption.ToString() Then
                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "PrescripIMMeds"
                    'Emylist.AssociatedCategory = strMangementOption.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrManagment.Add(Emylist)
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "PrescripIMMeds"
                    oListItem.Code = strMangementOption.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrManagment.Add(oListItem)
                    oListItem.Dispose()
                End If
                If _dt.Rows(i)("sAssociatedEMName") = "OverCounterMeds" And _dt.Rows(i)("sAssociatedEMCategory") = strMangementOption.ToString() Then
                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "OverCounterMeds"
                    'Emylist.AssociatedCategory = strMangementOption.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrManagment.Add(Emylist)
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "OverCounterMeds"
                    oListItem.Code = strMangementOption.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrManagment.Add(oListItem)
                    oListItem.Dispose()
                End If

                If _dt.Rows(i)("sAssociatedEMName") = "ConfWPatientFamilyMinutes" And _dt.Rows(i)("sAssociatedEMCategory") = strMangementOption.ToString() Then
                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "ConfWPatientFamilyMinutes"
                    'Emylist.AssociatedCategory = strMangementOption.ToString()
                    'Emylist.AssociatedItem = _dt.Rows(i)("sStatus").ToString()
                    '_arrManagment.Add(Emylist)
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "ConfWPatientFamilyMinutes"
                    oListItem.Code = strMangementOption.ToString()
                    oListItem.Status = _dt.Rows(i)("sStatus").ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrManagment.Add(oListItem)
                    oListItem.Dispose()
                End If
            Next
        End If
    End Sub
    Public Sub SetFlags(ByVal cnt As Integer)
        Dim i As Integer = 0
        If Convert.ToString(arrEMField.GetValue(cnt)).Contains("X-Ray/Radiology") Then
            i = 0
            For Each oItem As Object In chkAssociateEMCategory.Items
                If oItem.ToString().Contains("X-Ray/Radiology") Then
                    chkAssociateEMCategory.SetItemChecked(i, True)
                    Exit For
                End If
                i = i + 1
            Next
            'chkAssociateEMCategory.SetItemChecked(cnt, True)
        ElseIf Convert.ToString(arrEMField.GetValue(cnt)).Contains("Labs") Then
            i = 0
            For Each oItem As Object In chkAssociateEMCategory.Items
                If oItem.ToString().Contains("Labs") Then
                    chkAssociateEMCategory.SetItemChecked(i, True)
                    Exit For
                End If
                i = i + 1
            Next
            'chkAssociateEMCategory.SetItemChecked(cnt, True)
        ElseIf Convert.ToString(arrEMField.GetValue(cnt)).Contains("Management Option") Then
            i = 0
            For Each oItem As Object In chkAssociateEMCategory.Items
                If oItem.ToString().Contains("Management Option") Then
                    chkAssociateEMCategory.SetItemChecked(i, True)
                    Exit For
                End If
                i = i + 1
            Next
            'chkAssociateEMCategory.SetItemChecked(cnt, True)
        ElseIf Convert.ToString(arrEMField.GetValue(cnt)).Contains("Other Diagnostic Tests") Then
            i = 0
            For Each oItem As Object In chkAssociateEMCategory.Items
                If oItem.ToString().Contains("Other Diagnostic Tests") Then
                    chkAssociateEMCategory.SetItemChecked(i, True)
                    Exit For
                End If
                i = i + 1
            Next
            'chkAssociateEMCategory.SetItemChecked(cnt, True)
        End If
    End Sub

    Private Sub Fill_Provider()

        If Not dtFill_Provider Is Nothing Then
            dtFill_Provider.Dispose()
            dtFill_Provider = Nothing
        End If

        dtFill_Provider = objclsTemplateGallery.GetAllProvider()

        If Not IsNothing(dtFill_Provider) Then

            Dim objrow As DataRow
            objrow = dtFill_Provider.NewRow
            objrow.Item(0) = 0
            objrow.Item(1) = "All"
            dtFill_Provider.Rows.Add(objrow)
            cmbProvider.DataSource = dtFill_Provider
            cmbProvider.ValueMember = dtFill_Provider.Columns(0).ColumnName
            cmbProvider.DisplayMember = dtFill_Provider.Columns(1).ColumnName

            cmbProvider.SelectedIndex = -1

            If (_nLoginProviderId > 0) Then

                cmbProvider.SelectedValue = _nLoginProviderId
            Else
                cmbProvider.Text = "All"
            End If
        End If

    End Sub

    Private Sub Fill_Category()

        If Not dtFill_Category Is Nothing Then
            dtFill_Category.Dispose()
            dtFill_Category = Nothing
        End If

        dtFill_Category = objclsTemplateGallery.GetAllCategory()
        cmbCategory.DataSource = dtFill_Category
        cmbCategory.ValueMember = dtFill_Category.Columns(0).ColumnName
        cmbCategory.DisplayMember = dtFill_Category.Columns(1).ColumnName
        cmbCategory.SelectedIndex = -1

    End Sub

    Private Sub Fill_TemplateSpecility()

        If Not dtFill_TemplateSpecility Is Nothing Then
            dtFill_TemplateSpecility.Dispose()
            dtFill_TemplateSpecility = Nothing
        End If

        dtFill_TemplateSpecility = GetAllTemplateSpecility()
        Dim str As String = dtFill_TemplateSpecility.DefaultView.Sort()

        cmbTemplateSpecility.DataSource = dtFill_TemplateSpecility.DefaultView
        cmbTemplateSpecility.ValueMember = dtFill_TemplateSpecility.Columns(0).ColumnName
        cmbTemplateSpecility.DisplayMember = dtFill_TemplateSpecility.Columns(1).ColumnName
        cmbTemplateSpecility.SelectedIndex = -1

    End Sub

    Private Sub Fill_DataDictionary()
        Dim nTableCount As Integer
        Dim nFieldCount As Integer
        Dim mylist As myList
        Dim trvRootNode As TreeNode
        trvRootNode = New TreeNode
        Dim trvTableNode As TreeNode
        Dim trvFieldNode As TreeNode

        Dim clDataDictionaryTables As Collection
        Dim clDataDictionaryFields As Collection
        Dim objDataDictionaryTables As New clsDataDictionary
        '  Dim objDataDictionaryFields As New clsDataDictionary
        clDataDictionaryTables = objDataDictionaryTables.Fill_DataDictionaryTables()
        If Not IsNothing(dtdictionary) Then '' added for problem 00000187 to filter using like condition for Dictionary treeview
            dtdictionary.Rows.Clear()
        End If
        With trvDataDictionary
            .Nodes.Clear()
            trvRootNode.Text = "Data Dictionary"
            trvRootNode.ImageIndex = 0
            trvRootNode.SelectedImageIndex = 0
            .Nodes.Add(trvRootNode)

            For nTableCount = 1 To clDataDictionaryTables.Count
                trvTableNode = New TreeNode
                trvTableNode.Text = clDataDictionaryTables.Item(nTableCount)
                trvTableNode.ImageIndex = 1
                trvTableNode.SelectedImageIndex = 1
                trvRootNode.Nodes.Add(trvTableNode)
                clDataDictionaryFields = objDataDictionaryTables.Fill_DataDictionaryFields(clDataDictionaryTables.Item(nTableCount))
                For nFieldCount = 1 To clDataDictionaryFields.Count
                    trvFieldNode = New TreeNode
                    mylist = clDataDictionaryFields.Item(nFieldCount)
                    trvFieldNode.Tag = mylist.ID
                    trvFieldNode.Text = mylist.Description
                    trvFieldNode.ImageIndex = 2
                    trvFieldNode.SelectedImageIndex = 2
                    trvTableNode.Nodes.Add(trvFieldNode)
                    ''commented by Sandip Darade 20090406
                    'trvTableNode.EnsureVisible()
                    'trvTableNode.ExpandAll()
                    Dim drc As DataRow = dtdictionary.NewRow() '' added for problem 00000187 to filter using like condition for Dictionary treeview
                    drc("ID") = mylist.ID
                    drc("Description") = mylist.Description
                    drc("ParentNode") = trvTableNode.Text
                    dtdictionary.Rows.Add(drc)
                Next
                clDataDictionaryFields.Clear()
                'trvTableNode.ExpandAll()
            Next
            clDataDictionaryTables.Clear()

            ''Sandip Darade 20090406
            For Each n As TreeNode In trvDataDictionary.Nodes
                If (n.Level = 0) Then
                    n.Expand()
                    Exit For
                End If
            Next
        End With

        ' objDataDictionaryFields = Nothing

        objDataDictionaryTables = Nothing



    End Sub



    Private Sub Fill_FilterDataDictionary(ByVal searchstr As String) '' added function for problem 00000187 to filter using like condition for Dictionary treeview

        Try


            Dim trvRootNode As TreeNode
            trvRootNode = New TreeNode
            Dim trvTableNode As TreeNode = Nothing
            Dim trvFieldNode As TreeNode
            Dim nTableCount As Integer = 0

            Dim drr As DataRow() = dtdictionary.Select("Description  like '%" + searchstr + "%' ", "ParentNode")
            '%" & Word & "%' 
            If drr.Length > 0 Then
                Dim parentnodestr As String = ""
                With trvDataDictionary
                    .Nodes.Clear()
                    trvRootNode.Text = "Data Dictionary"
                    trvRootNode.ImageIndex = 0
                    trvRootNode.SelectedImageIndex = 0
                    .Nodes.Add(trvRootNode)

                    For nTableCount = 0 To drr.Length - 1
                        If parentnodestr.Trim() <> drr(nTableCount)("ParentNode").ToString().Trim() Then
                            trvTableNode = New TreeNode
                            trvTableNode.Text = drr(nTableCount)("ParentNode")
                            trvTableNode.ImageIndex = 1
                            trvTableNode.SelectedImageIndex = 1
                            trvRootNode.Nodes.Add(trvTableNode)
                            parentnodestr = drr(nTableCount)("ParentNode").ToString()
                        End If

                        If (IsNothing(trvTableNode)) Then
                            trvTableNode = New TreeNode
                            trvRootNode.Nodes.Add(trvTableNode)
                        End If
                        trvFieldNode = New TreeNode

                        trvFieldNode.Tag = drr(nTableCount)("ID")
                        trvFieldNode.Text = drr(nTableCount)("Description")
                        trvFieldNode.ImageIndex = 2
                        trvFieldNode.SelectedImageIndex = 2
                        trvTableNode.Nodes.Add(trvFieldNode)


                    Next
                End With

            End If
            trvRootNode.ExpandAll()
        Catch ex As Exception

        End Try



    End Sub



    Public Sub NewTemplate()

        Try
            m_ID = 0
            txtTemplateName.Text = ""
            txtTemplateName.Focus()
            txtSNOMEDCode.Text = String.Empty
            LoadNewDocument()

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        m_ID = 0
    End Sub

    Public Sub SaveAsTemplate()
        'wdDescription.ShowDialog(DSOFramer.dsoShowDialogType.dsoDialogOpen)
        m_ID = 0
        txtTemplateName.Focus()
    End Sub

    Public Sub OpenTemplate()
        Dim filNm As String
        OpenFileDialog1.Multiselect = False
        OpenFileDialog1.Filter = "MS-Word Files (*.doc,*.dot,*.docx,*.dotx) | *.doc;*.dot;*.docx;*.dotx"
        ''Code Added by Mayuri:2009112
        ''To fix Bug ID:#4484-When Clicking on the Open Button Under the FileName It should be Empty.It shows the "OpenFileDialogBox1"
        OpenFileDialog1.FileName = ""
        ''End Code Added by Mayuri:20091112
        If OpenFileDialog1.ShowDialog(System.Windows.Forms.Form.ActiveForm) = Windows.Forms.DialogResult.OK Then
            filNm = OpenFileDialog1.FileName

        Else
            Return
        End If
        Try
            LoadWordControl(filNm)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub



    Public Function SaveTemplate(Optional ByVal IsClose As Boolean = False) As Boolean

        If _bISOpenedFromDM = True Then
            If Not IsNothing(_DMNode) = True Then
                SaveDMTemplate()
            End If
        Else


            Dim blnValid As Boolean
            Dim objclsTemplateGallery As New clsTemplateGallery
            'Dim objfrmTemplateGalleryView As New frmTemplateGalleryView
            Try
                If Not oCurDoc Is Nothing Then

                    If txtTemplateName.Text.Trim = "" Then
                        MessageBox.Show("Please enter Template Name", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        txtTemplateName.Focus()
                        objclsTemplateGallery.Dispose()
                        objclsTemplateGallery = Nothing
                        SaveTemplate = Nothing

                        Exit Function
                    End If

                    If objclsTemplateGallery.CheckDuplicate(m_ID, Trim(txtTemplateName.Text), cmbCategory.SelectedValue, cmbProvider.SelectedValue) = True Then
                        MessageBox.Show("Template Name for this Category and Doctor already Exists", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        txtTemplateName.Focus()
                        objclsTemplateGallery.Dispose()
                        objclsTemplateGallery = Nothing
                        SaveTemplate = Nothing
                        Exit Function
                    End If

                    '''''''''Next two if statements are added by Anil on 05/10/2007 at 12:50 p.m.
                    '''''''''These if statements are added because previously the new template were get added even if the Category and Provider is not given.
                    If cmbCategory.Text = "" Then
                        MessageBox.Show("Please select the Category", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        objclsTemplateGallery.Dispose()
                        objclsTemplateGallery = Nothing
                        SaveTemplate = Nothing
                        Exit Function
                    End If
                    If cmbProvider.Text = "" Then
                        MessageBox.Show("Please select the Provider", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        objclsTemplateGallery.Dispose()
                        objclsTemplateGallery = Nothing
                        SaveTemplate = Nothing
                        Exit Function
                    End If

                    ''For SaleForce case  no :GLO2007-0001359
                    Dim strBookmark As String
                    Dim intBookmarkCount As Integer

                    'Dim bmCnt As Int32 = 1 '0 SLR: initialized to 1 : optimization
                    'oCurDoc.Bookmarks.DefaultSorting = Wd.WdBookmarkSortBy.wdSortByLocation
                    ''SLR: optimized to check only for min times
                    'For i As Int32 = 1 To oCurDoc.Bookmarks.Count + 1
                    '    If oCurDoc.Bookmarks.Exists("BM" & i.ToString) = False Then
                    '        bmCnt = i
                    '        Exit For
                    '    End If
                    'Next
                    'Dim bmRLCnt As Int32 = 1
                    'For i As Int32 = 1 To oCurDoc.Bookmarks.Count + 1
                    '    If oCurDoc.Bookmarks.Exists("BMRL" & i.ToString) = False Then
                    '        bmRLCnt = i
                    '        Exit For
                    '    End If
                    'Next
                    For i As Int32 = 1 To oCurDoc.Bookmarks.Count
                        strBookmark = oCurDoc.Bookmarks.Item(i).Name

                        If strBookmark.StartsWith("BM") = True Then
                            intBookmarkCount += 1
                        End If

                    Next

                    'If (bmCnt Mod 2 = 0) Or (bmRLCnt Mod 2 = 0) Then
                    If (intBookmarkCount Mod 2 <> 0) Then
                        If (MessageBox.Show("There are unpaired bookmarks in this template. Do you still want to save the template?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1)) = Windows.Forms.DialogResult.No Then
                            objclsTemplateGallery.Dispose()
                            objclsTemplateGallery = Nothing
                            SaveTemplate = Nothing
                            Exit Function
                        End If
                    End If

                    ''End  

                    oCurDoc.ActiveWindow.View.ShowBookmarks = False

                    '  Dim sFileName As String = ExamNewDocumentName

                    ' wdTemplate.Save(sFileName, True, "", "")


                    Try
                        If (oCurDoc.CompatibilityMode <> 65535) Then 'wdcurrent: but unable to enumerate:
                            If (oCurDoc.CompatibilityMode <> oCurDoc.Application.Version) Then
                                oCurDoc.Convert()
                            End If
                        End If
                    Catch ex As Exception
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                    End Try
                    'oCurDoc.SaveAs(sFileName, Wd.WdSaveFormat.wdFormatXMLDocument, False, "", False)

                    'If IsClose = False Then
                    '    oCurDoc.Saved = True
                    'End If
                    'wdTemplate.Close()
                    'txtDescription.Text = sFileName
                    Dim myByte As Byte() = gloWord.LoadAndCloseWord.SaveWordFiletoBinary(wdTemplate, oCurDoc, oWordApp, gloSettings.FolderSettings.AppTempFolderPath, False, IsClose)

                    Dim myBinaray As Object = Nothing
                    If (IsNothing(myByte) = False) Then
                        myBinaray = CType(myByte, Object)
                    End If
                    'Dim strAssociatedItem As String = GetAssociateEMField()

                    'If strAssociatedItem <> "" Then
                    '    objclsTemplateGallery.AddNewTemplateGallery(m_ID, Trim(txtTemplateName.Text), cmbCategory.SelectedValue, cmbProvider.SelectedValue, txtDescription.Text, strAssociatedItem)
                    'Else
                    '    objclsTemplateGallery.AddNewTemplateGallery(m_ID, Trim(txtTemplateName.Text), cmbCategory.SelectedValue, cmbProvider.SelectedValue, txtDescription.Text)
                    'End If

                    Dim sUserActivity As String
                    Dim sUserActivityType As Integer
                    If m_ID = 0 Then
                        sUserActivity = "New Template (" + Trim(txtTemplateName.Text) + ") Created"
                        sUserActivityType = 1
                    Else
                        sUserActivity = "Template (" + Trim(txtTemplateName.Text) + ") Modified"
                        sUserActivityType = 2
                    End If

                    objclsTemplateGallery.AddNewTemplateGalleryBytes(m_ID, Trim(txtTemplateName.Text), cmbCategory.SelectedValue, cmbCategory.Text, cmbProvider.SelectedValue, myBinaray, _arrLabs, _arrOrders, _arrManagment, _arrOtherDiag, cmbTemplateSpecility.Text, sBibliography, sDeveloper, strSnomedCode, strSnomedDescription)
                    '' Flag blnValid added to Check Template is Saved or not 
                    blnValid = True

                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, sUserActivityType, sUserActivity, 0, m_ID, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)

                    '' 
                    _TemplateName = Trim(txtTemplateName.Text)
                    _CategoryID = cmbCategory.SelectedValue
                    '  CancelClick = False
                    If IsClose Then

                        If (IsNothing(oCurDoc) = False) Then
                            Try
                                Marshal.ReleaseComObject(oCurDoc)
                            Catch ex As Exception


                            End Try
                            oCurDoc = Nothing

                        End If
                        Me.Close()
                    Else
                        '   LoadWordControl(sFileName)
                        If (IsNothing(oCurDoc) = False) Then
                            oCurDoc.Saved = True
                        End If


                    End If

                    Return blnValid
                End If

            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return Nothing
            Finally

                If (IsNothing(objclsTemplateGallery) = False) Then
                    objclsTemplateGallery.Dispose()
                    objclsTemplateGallery = Nothing
                End If

                'objfrmTemplateGalleryView = Nothing
            End Try

        End If
        Return Nothing
    End Function

    Private Sub SaveDMTemplate()

        Try
            If Not oCurDoc Is Nothing Then

                If txtTemplateName.Text = "" Then
                    MessageBox.Show("Please enter Template Name", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    txtTemplateName.Focus()
                    Exit Sub
                End If


                'check this in DM_templates_dtl
                'If objclsTemplateGallery.CheckDuplicate(m_ID, Trim(txtTemplateName.Text), cmbCategory.SelectedValue, cmbProvider.SelectedValue) = True Then
                '    MessageBox.Show("Template Name for this Category and Doctor already Exists", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    txtTemplateName.Focus()
                '    Exit Sub
                'End If

                '''''''''Next two if statements are added by Anil on 05/10/2007 at 12:50 p.m.
                '''''''''These if statements are added because previously the new template were get added even if the Category and Provider is not given.
                If cmbCategory.Text = "" Then
                    MessageBox.Show("Please select the Category", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If

                'needs to be confirmed
                ' ''If cmbProvider.Text = "" Then
                ' ''    MessageBox.Show("Please select the Provider", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                ' ''    Exit Sub
                ' ''End If

                oCurDoc.ActiveWindow.View.ShowBookmarks = False

                'Dim sFileName As String = ExamNewDocumentName

                'If (IsNothing(oCurDoc) = False) Then
                '    oCurDoc.SaveAs(sFileName, Wd.WdSaveFormat.wdFormatXMLDocument, False, "", False)
                'Else
                '    wdTemplate.Save(sFileName, True, "", "")
                'End If


                Try
                    If (oCurDoc.CompatibilityMode <> 65535) Then 'wdcurrent: but unable to enumerate:
                        If (oCurDoc.CompatibilityMode <> oCurDoc.Application.Version) Then
                            oCurDoc.Convert()
                        End If
                    End If
                Catch ex As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                End Try
                'oCurDoc.SaveAs(sFileName, Wd.WdSaveFormat.wdFormatXMLDocument, False, "", False)

                'wdTemplate.Close()
                'txtDescription.Text = sFileName
                Dim myByte As Byte() = gloWord.LoadAndCloseWord.SaveWordFiletoBinary(wdTemplate, oCurDoc, oWordApp, gloSettings.FolderSettings.AppTempFolderPath, False, True)

                Dim myBinaray As Object = Nothing
                If (IsNothing(myByte) = False) Then
                    myBinaray = CType(myByte, Object)
                End If
                'Dim strAssociatedItem As String = GetAssociateEMField()

                'If strAssociatedItem <> "" Then
                '    objclsTemplateGallery.AddNewTemplateGallery(m_ID, Trim(txtTemplateName.Text), cmbCategory.SelectedValue, cmbProvider.SelectedValue, txtDescription.Text, strAssociatedItem)
                'Else
                '    objclsTemplateGallery.AddNewTemplateGallery(m_ID, Trim(txtTemplateName.Text), cmbCategory.SelectedValue, cmbProvider.SelectedValue, txtDescription.Text)
                'End If
                '   objclsTemplateGallery.AddNewTemplateGallery(m_ID, Trim(txtTemplateName.Text), cmbCategory.SelectedValue, cmbCategory.Text, cmbProvider.SelectedValue, txtDescription.Text, _arrLabs, _arrOrders, _arrManagment, _arrOtherDiag)
                '' Flag blnValid added to Check Template is Saved or not 

                _TemplateName = Trim(txtTemplateName.Text)


                ' Dim objword As New clsWordDocument
                '' To convert from Object to Binary Format
                _DMNode.DMTemplate = myBinaray ' objword.ConvertFiletoBinary(sFileName)

                '  objword = Nothing

                'needs to be confirmed
                '   _CategoryID = cmbCategory.SelectedValue
                '---





                '  CancelClick = False
                'If Not IsClose Then
                '    LoadWordControl(sFileName)
                '    oCurDoc.Saved = True
                '  CancelClick = True
                'End If


                If (IsNothing(oCurDoc) = False) Then
                    Try
                        Marshal.ReleaseComObject(oCurDoc)
                    Catch ex As Exception


                    End Try
                    oCurDoc = Nothing

                End If

                Me.Close()

            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Throw ex
        End Try
    End Sub

    Private Function GetAssociateEMField() As String
        Dim strAssociatedItem As String = ""
        Dim i As Integer = 0
        Try
            For Each oItem As Object In chkAssociateEMCategory.CheckedItems
                If i = 0 Then
                    strAssociatedItem = oItem.ToString()
                Else
                    strAssociatedItem = strAssociatedItem & "|" & oItem.ToString()
                End If
                i = i + 1
            Next
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Assciation, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
        Return strAssociatedItem
    End Function
    Private Sub SearchNode(ByVal Trv As TreeView, ByVal strText As String)
        Dim trvNde As TreeNode
        For Each trvNde In Trv.Nodes
            SearchNode(trvNde, strText)
        Next
    End Sub

    Private Sub SearchNode(ByVal rootNode As TreeNode, ByVal strText As String)
        For Each childNode As TreeNode In rootNode.Nodes
            If LCase(Trim(childNode.Text)) = LCase(Trim(strText)) Then
                trvSearchNode = childNode
                Exit Sub
            End If
            SearchNode(childNode, strText)
        Next
    End Sub



    Public Sub NavigateTemplate(ByVal blnKeyValue As Boolean)
        If Not IsNothing(trvDataDictionary.SelectedNode) Then
            'Navigate forward
            If blnKeyValue Then

                'if selectednode is rootnode
                If trvDataDictionary.SelectedNode Is trvDataDictionary.Nodes.Item(0) Then
                    If trvDataDictionary.Nodes.Item(0).GetNodeCount(False) > 0 Then
                        If trvDataDictionary.Nodes.Item(0).Nodes.Item(0).GetNodeCount(False) > 0 Then
                            trvDataDictionary.SelectedNode = trvDataDictionary.Nodes.Item(0).Nodes.Item(0).FirstNode
                            trvDataDictionary.Select()
                        End If
                    End If

                    'if selected node is My template /ALL Template
                ElseIf trvDataDictionary.SelectedNode.Parent Is trvDataDictionary.Nodes.Item(0) Then

                    Dim i As Integer
                    For i = trvDataDictionary.SelectedNode.Index To trvDataDictionary.Nodes.Item(0).GetNodeCount(False) - 1
                        If trvDataDictionary.Nodes.Item(0).Nodes.Item(i).GetNodeCount(False) > 0 Then
                            trvDataDictionary.SelectedNode = trvDataDictionary.Nodes.Item(0).Nodes.Item(i).FirstNode
                            trvDataDictionary.Select()
                            Exit Sub
                        End If
                    Next


                    'if selected node is prescription history items node 
                    'ElseIf Not CType(trvDataDictionary.SelectedNode, myTreeNode).Tag Is Nothing Then
                ElseIf trvDataDictionary.SelectedNode.Parent.Parent Is trvDataDictionary.Nodes(0) Then

                    SelectnextNode(trvDataDictionary.SelectedNode.Parent.Index)
                    trvDataDictionary.Select()

                End If
                'Navigate backward
            Else
                'if selectednode is not rootnode
                If Not trvDataDictionary.SelectedNode Is trvDataDictionary.Nodes.Item(0) Then

                    'if selected node is current/yesterday/last week/last month/older
                    If trvDataDictionary.SelectedNode.Parent Is trvDataDictionary.Nodes.Item(0) Then

                        Dim i As Integer
                        For i = trvDataDictionary.SelectedNode.Index - 1 To 0 Step -1
                            If trvDataDictionary.Nodes.Item(0).Nodes.Item(i).GetNodeCount(False) > 0 Then
                                trvDataDictionary.SelectedNode = trvDataDictionary.Nodes.Item(0).Nodes.Item(i).LastNode
                                trvDataDictionary.Select()
                                Exit Sub
                            End If
                        Next

                    ElseIf trvDataDictionary.SelectedNode.Parent.Parent Is trvDataDictionary.Nodes(0) Then
                        SelectPreviousNode(trvDataDictionary.SelectedNode.Parent.Index)
                        trvDataDictionary.Select()
                    End If
                End If
            End If
        Else
            If trvDataDictionary.GetNodeCount(False) > 0 Then
                trvDataDictionary.SelectedNode = trvDataDictionary.Nodes.Item(0)
                trvDataDictionary.Select()
            End If
        End If
    End Sub

    Private Sub SelectPreviousNode(ByVal intnode As Integer)
        Dim i As Integer
        If trvDataDictionary.SelectedNode.Parent.Parent Is trvDataDictionary.Nodes.Item(0) Then
            For i = intnode To 0 Step -1
                If i = intnode Then
                    Dim j As Integer
                    For j = trvDataDictionary.SelectedNode.Index - 1 To 0 Step -1
                        trvDataDictionary.SelectedNode = trvDataDictionary.Nodes.Item(0).Nodes.Item(intnode).Nodes.Item(j)
                        Exit Sub
                    Next
                Else
                    Dim j As Integer
                    For j = trvDataDictionary.Nodes.Item(0).Nodes.Item(i).GetNodeCount(False) - 1 To 0 Step -1
                        trvDataDictionary.SelectedNode = trvDataDictionary.Nodes.Item(0).Nodes.Item(i).Nodes.Item(j)
                        Exit Sub
                    Next


                End If
            Next
        End If

    End Sub
    Private Sub SelectnextNode(ByVal intnode As Integer)
        Try
            Dim i As Integer
            If trvDataDictionary.SelectedNode.Parent.Parent Is trvDataDictionary.Nodes.Item(0) Then
                'Get the number of nodes in first template node
                For i = intnode To trvDataDictionary.Nodes.Item(0).GetNodeCount(False) - 1
                    If i = intnode Then
                        Dim j As Integer
                        For j = trvDataDictionary.SelectedNode.Index + 1 To trvDataDictionary.Nodes.Item(0).Nodes.Item(intnode).GetNodeCount(False) - 1
                            trvDataDictionary.SelectedNode = trvDataDictionary.Nodes.Item(0).Nodes.Item(intnode).Nodes.Item(j)
                            Exit Sub
                        Next
                    Else
                        Dim myHistoryItemNode As TreeNode
                        For Each myHistoryItemNode In trvDataDictionary.Nodes.Item(0).Nodes.Item(i).Nodes
                            trvDataDictionary.SelectedNode = myHistoryItemNode
                            Exit Sub
                        Next
                    End If
                Next
                'Get the number of nodes in second template node
                For i = 0 To trvDataDictionary.Nodes.Item(1).GetNodeCount(False) - 1
                    Dim myHistoryItemNode As TreeNode
                    For Each myHistoryItemNode In trvDataDictionary.Nodes.Item(1).Nodes.Item(i).Nodes
                        trvDataDictionary.SelectedNode = myHistoryItemNode
                        Exit Sub
                    Next
                Next
            ElseIf trvDataDictionary.SelectedNode.Parent.Parent Is trvDataDictionary.Nodes.Item(1) Then
                'Get the number of nodes in second template node
                For i = intnode To trvDataDictionary.Nodes.Item(1).GetNodeCount(False) - 1
                    'check if the index of the child template is the one that has been selected
                    If i = intnode Then
                        Dim j As Integer
                        For j = trvDataDictionary.SelectedNode.Index + 1 To trvDataDictionary.Nodes.Item(1).Nodes.Item(intnode).GetNodeCount(False) - 1
                            trvDataDictionary.SelectedNode = trvDataDictionary.Nodes.Item(1).Nodes.Item(intnode).Nodes.Item(j)
                            Exit Sub
                        Next

                    Else
                        Dim myHistoryItemNode As TreeNode
                        For Each myHistoryItemNode In trvDataDictionary.Nodes.Item(1).Nodes.Item(i).Nodes
                            trvDataDictionary.SelectedNode = myHistoryItemNode
                            Exit Sub
                        Next
                    End If

                Next

            End If

            'trvTarget.SelectedNode = trvTarget.Nodes(i - 1)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            'MsgBox(ex.Message)
        End Try
    End Sub

    Public Sub UnProtectForm()
        Dim oSect As Wd.Section
        ' Dim oSect1 As Wd.Section
        If oCurDoc.ProtectionType <> Wd.WdProtectionType.wdNoProtection Then oCurDoc.Unprotect()
        'For Each oSect In oCurDoc.Sections
        'oSect.ProtectedForForms = True
        For Each oSect In oCurDoc.Sections
            oSect.ProtectedForForms = False
        Next
        'Next
        oCurDoc.Protect(Type:=Wd.WdProtectionType.wdAllowOnlyFormFields, NoReset:=False, Password:="")

    End Sub

    Public Sub ProtectForm()
        Dim oSect As Wd.Section
        ' Dim oSect1 As Wd.Section
        If oCurDoc.ProtectionType <> Wd.WdProtectionType.wdNoProtection Then oCurDoc.Unprotect()
        'For Each oSect In oCurDoc.Sections
        'oSect.ProtectedForForms = True
        For Each oSect In oCurDoc.Sections
            oSect.ProtectedForForms = True
        Next

        oCurDoc.Protect(Type:=Wd.WdProtectionType.wdAllowOnlyFormFields, NoReset:=True, Password:="")

    End Sub

    Private Sub InsertLogo()
        Try

            Dim objWord As New clsWordDocument
            Dim objCriteria As DocCriteria
            objCriteria = New DocCriteria
            objCriteria.DocCategory = enumDocCategory.Exam
            objCriteria.PatientID = 0
            objCriteria.VisitID = 0
            objCriteria.PrimaryID = 0
            objWord.DocumentCriteria = objCriteria

            ImagePath = objWord.getData_FromDB("Clinic_MST.imgClinicLogo")
            objCriteria.Dispose()
            objCriteria = Nothing
            objWord = Nothing
            ImagePath = Mid(ImagePath, 1, Len(ImagePath) - 2)
            If oCurDoc Is Nothing Then
                Exit Sub
            End If

            If oCurDoc.Application.ActiveWindow.ActivePane.View.Type = Wd.WdViewType.wdNormalView Or oCurDoc.Application.ActiveWindow.ActivePane.View.Type = Wd.WdViewType.wdOutlineView Then
                oCurDoc.Application.ActiveWindow.ActivePane.View.Type = Wd.WdViewType.wdPrintView
            End If
            oCurDoc.Activate()
            oCurDoc.Application.ActiveWindow.ActivePane.View.SeekView = Wd.WdSeekView.wdSeekCurrentPageHeader

            'Changed By Shweta 20100106
            'against the bug id:5260
            'This property used to insert header only on first page  
            'With oCurDoc.Application.ActiveDocument.PageSetup
            '    .DifferentFirstPageHeaderFooter = True
            'End With
            With oCurDoc.Application.ActiveDocument.PageSetup
                .DifferentFirstPageHeaderFooter = False         'insert header on all pages
            End With
            'End 20100106

            If File.Exists(ImagePath) Then

                '' SUDHIR 20090619 '' 
                Dim oWord As New clsWordDocument
                oWord.CurDocument = oCurDoc
                ' Dim myType As Wd.WdViewType = gloWord.LoadAndCloseWord.ChangeToEditView(oCurDoc)
                oWord.InsertImage(ImagePath)
                oWord = Nothing
                'gloWord.LoadAndCloseWord.RestoreFromEditView(oCurDoc, myType)
                'oCurDoc.Application.Selection.InlineShapes.AddPicture(FileName:=ImagePath, LinkToFile:=False, SaveWithDocument:=True)
                '' END SUDHIR ''

            End If

        Catch objErr As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.Add, objErr.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            oCurDoc.Application.ActiveWindow.ActivePane.View.SeekView = Wd.WdSeekView.wdSeekMainDocument

        End Try

    End Sub

    Private Sub InsertSignature()

        Try
            ImagePath = ""
            Dim frm As New FrmSignature
            frm.Owner = Me
            ' frm.ShowDialog(IIf(IsNothing(frm.Parent), Me, frm.Parent))
            frm.ShowDialog(frm.Parent)
            frm.Dispose()
            ''commented by Dhruv 20091214 
            ''To not to save on form closing
            'If File.Exists(ImagePath) Then

            '    '' SUDHIR 20090619 '' 
            '    Dim oWord As New clsWordDocument
            '    oWord.CurDocument = oCurDoc
            '    oWord.InsertImage(ImagePath)
            '    oWord = Nothing
            '    'oCurDoc.Application.Selection.InlineShapes.AddPicture(FileName:=ImagePath, LinkToFile:=False, SaveWithDocument:=True)
            '    '' END SUDHIR ''

            '    oCurDoc.Application.Selection.TypeParagraph()
            'End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
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
    Private Sub UnDoChanges()
        Try
            If Not IsNothing(oCurDoc) Then
                oCurDoc.Undo()
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Undo, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub ReDoChanges()
        Try
            If Not IsNothing(oCurDoc) Then
                oCurDoc.Redo()
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Redo, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Public Sub trvDataDictionary_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles trvDataDictionary.KeyPress
        'if enter key is pressed then
        If (e.KeyChar = ChrW(13)) Then
            Dim objSender As Object = Nothing
            '  Dim obje As EventArgs = Nothing

            Dim objTrvArgs As New TreeNodeMouseClickEventArgs(trvDataDictionary.SelectedNode, Windows.Forms.MouseButtons.Left, 2, trvDataDictionary.SelectedNode.Bounds.X, trvDataDictionary.SelectedNode.Bounds.Y)
            Call trvDataDictionary_NodeMouseDoubleClick(objSender, objTrvArgs)
            objTrvArgs = Nothing

            ' Call trvDataDictionary_DoubleClick(objSender, obje)
        End If
        'End If
    End Sub

    Private Sub txtsearchtemplate_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtsearchtemplate.TextChanged
        Try

            Fill_FilterDataDictionary(txtsearchtemplate.Text.Replace("'", "''")) ''added for problem 00000187, v8020
            '  End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtsearchtemplate_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtsearchtemplate.KeyPress
        If (e.KeyChar = ChrW(13)) Then
            trvDataDictionary.Select()
        Else
            trvDataDictionary.SelectedNode = trvDataDictionary.Nodes.Item(0)
        End If

    End Sub


    Public WriteOnly Property ImageFilePath() As String Implements mdlGeneral.ISignature.ImageFilePath
        Set(ByVal Value As String)
            ImagePath = Value
        End Set
    End Property

#Region "Code added by Ravikiran for Word 2007 UserControl Implementation on 05/05/2007"

    Public Sub AddCheckbox()
        If Not oCurDoc Is Nothing Then
            If IsFeasible(enumControls.FormFieldControl) Then
                Dim oNameField As Wd.FormField
                oNameField = oCurDoc.FormFields.Add(oCurDoc.Application.Selection.Range, Wd.WdFieldType.wdFieldFormCheckBox)
                oCurDoc.ActiveWindow.SetFocus()
            End If
        End If
    End Sub

    Public Sub AddDropDown()
        If Not oCurDoc Is Nothing Then
            'Dim oNameField As Wd.FormField
            'oNameField = oCurDoc.FormFields.Add(oCurDoc.Application.Selection.Range, Wd.WdFieldType.wdFieldFormDropDown)
            If IsFeasible(enumControls.FormFieldControl) Then
                Dim objDD As New frmAddDropDown
                objDD.GetDropdownItems = Nothing
                objDD.GetDropdownTitle = ""
                objDD.ShowDialog(IIf(IsNothing(objDD.Parent), Me, objDD.Parent))
                Dim m_arrlist As ArrayList = objDD.GetDropdownItems
                If Not IsNothing(m_arrlist) Then

                    With oCurDoc.Application.Selection
                        .Range.ContentControls.Add(Wd.WdContentControlType.wdContentControlDropdownList)
                        .ParentContentControl.Title = objDD.GetDropdownTitle
                        .ParentContentControl.Tag = GetUniqueKey()
                        '.ParentContentControl.LockContentControl = True
                        .ParentContentControl.DropdownListEntries.Clear()
                        For _cnt As Int32 = 0 To m_arrlist.Count - 1
                            .ParentContentControl.DropdownListEntries.Add(Text:=m_arrlist(_cnt).ToString, Value:=m_arrlist(_cnt).ToString)
                        Next
                    End With

                    oCurDoc.ActiveWindow.SetFocus()
                End If
                objDD.Dispose()
                objDD = Nothing
            End If
        End If
    End Sub

    Public Sub OnFormClicked(ByVal Sel As Wd.Selection, ByRef Cancel As Boolean)
        Try
            '' SUDHIR 20091202 '' TO HANDLE EMR-PM WORD INSTANCE ''            
            'If gstrOpenDocument <> GetActiveDocumentName() Then
            '    Exit Sub
            'End If
            '' END SUDHIR '' 
            'CODE ADDED BY DIPAK 20091224 ''' TO HANDLE EMR-PM WORD INSTANCE ''
            Dim activeDocumentName As String = GetActiveDocumentName()
            If (String.IsNullOrEmpty(activeDocumentName)) Then
                Exit Sub
            End If
            If Not garrOpenDocument.Contains(activeDocumentName) Then
                Exit Sub
            End If
            'END CODE ADDED BY DIPAK 

            Cancel = False
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
            If (r.ContentControls.Count = 1) Then

                _cntctrl = r.ContentControls(1)
                If _cntctrl.Type = Wd.WdContentControlType.wdContentControlDropdownList And r.Application.Selection.ParentContentControl.Temporary = False Then
                    AccessControl()
                    Cancel = True
                End If

            ElseIf Not r.ParentContentControl Is Nothing Then
                _cntctrl = r.ParentContentControl
                If _cntctrl.Type = Wd.WdContentControlType.wdContentControlDropdownList Then
                    AccessControl()
                    Cancel = True
                End If
            End If



        Catch ex As Exception
        End Try
    End Sub

    ''' <summary>
    ''' To open the form under same thread as we are opening the form using com object click events
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AccessControl()
        If Me.InvokeRequired Then
            Me.Invoke(New MethodInvoker(AddressOf AccessControl))

        Else
            OpenDropDown()
        End If
    End Sub

    Private Sub OpenDropDown()
        If Not oCurDoc Is Nothing Then
            m_arrList = New ArrayList
            For Each ddlEntry As Wd.ContentControlListEntry In _cntctrl.DropdownListEntries
                m_arrList.Add(ddlEntry.Text)
            Next

            Dim objDD As New frmAddDropDown
            objDD.GetDropdownItems = m_arrList
            objDD.GetDropdownTitle = _cntctrl.Title
            objDD.ShowDialog(IIf(IsNothing(objDD.Parent), Me, objDD.Parent))

            Dim m_TempList As ArrayList = objDD.GetDropdownItems
            If Not IsNothing(m_TempList) Then

                With _cntctrl
                    .Title = objDD.GetDropdownTitle
                    '.LockContentControl = True
                    .DropdownListEntries.Clear()
                    For _cnt As Int32 = 0 To m_TempList.Count - 1
                        .DropdownListEntries.Add(Text:=m_TempList(_cnt).ToString, Value:=m_TempList(_cnt).ToString)
                    Next

                End With
                m_TempList.Clear()
                m_TempList = Nothing
                oCurDoc.ActiveWindow.SetFocus()
            End If
            objDD.Dispose()
            objDD = Nothing
        End If

    End Sub

#End Region
    Private Sub ImportDocument(ByVal nInsertScan As Int16)
        ''Insert File - 1
        ''Scan Images - 2
        ''Set focus to Wd object
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
                If oFileDialogWindow.ShowDialog(System.Windows.Forms.Form.ActiveForm) = DialogResult.OK Then
                    Dim oFile As FileInfo = New FileInfo(oFileDialogWindow.FileName)
                    If oFile.Extension.ToUpper = UCase(".Doc") Or oFile.Extension.ToUpper = UCase(".Docx") Or oFile.Extension.ToUpper = UCase(".txt") Or oFile.Extension.ToUpper = UCase(".rtf") Then
                        ''''' Statement to Go end of Selelcted Wd Document
                        '//oCurDoc1.ActiveWindow.Selection.EndKey(Wd.WdUnits.wdStory)
                        'Insert file in Wd dobject
                        oCurDoc.Application.Selection.InsertFile(oFile.FullName)
                    End If
                End If
                oFileDialogWindow.Dispose()
                oFileDialogWindow = Nothing
            ElseIf nInsertScan = 2 Then
                'Dim frmObj As New frmDMS_ScannedDocumentEvent_TwainPro
                'Dim _Files As New Collection
                'frmObj.blnDMSScan = False
                'frmObj.ShowDialog(Me)
                '_Files = frmObj._NonDMSFileCollection
                'Dim i As Integer
                'For i = 1 To _Files.Count
                '    If File.Exists(_Files.Item(i)) Then
                '        oCurDoc.Application.Selection.InlineShapes.AddPicture(FileName:=_Files.Item(i), LinkToFile:=False, SaveWithDocument:=True)
                '        oCurDoc.Application.Selection.EndKey()
                '        oCurDoc.Application.Selection.InsertBreak()

                '    End If
                'Next

                'For i = 1 To _Files.Count
                '    If File.Exists(_Files.Item(i)) Then
                '        Kill(_Files.Item(i))
                '    End If
                'Next
                'oCurDoc.ActiveWindow.SetFocus()
                'frmObj = Nothing
                'i = Nothing
                '_Files = Nothing

                Dim oFiles As New ArrayList()
                Dim oEDocument As New gloEDocumentV3.gloEDocV3Management()
                'Commented BY Rahul Patel on 26-10-2010
                'gloEDocumentV3.gloEDocV3Admin.Connect(GetConnectionString(), gDMSV2TempPath, gnLoginID, gClinicID, Application.StartupPath)
                'Added by Rahul Patel on 26-10-2010
                'For changing the DMS Hybrid database change.
                gloEDocumentV3.gloEDocV3Admin.Connect(GetConnectionString(), GetDMSConnectionString(), gDMSV2TempPath, gnLoginID, gClinicID, Application.StartupPath)
                'End of code added by Rahul Patel on 26-10-2010
                oEDocument.ShowEScannerForImages(0, oFiles) ''pass patient id 0 to replace gnpatientid as this form is master not bounded with patient.
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
                ''_Files = Nothing
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally

        End Try
    End Sub

    Private Sub LoadWordControl(ByVal strFileName As String)
        '' TRY BY SUDHIR 20090708 ''
        Try
            '   wdTemplate.Open(strFileName)
            '   Dim oWordApp As Wd.Application = Nothing
            Dim strError As String = gloWord.LoadAndCloseWord.OpenDSO(wdTemplate, strFileName, oCurDoc, oWordApp)
            If (strError <> String.Empty) Then
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.WINWORD, gloAuditTrail.ActivityType.Modify, strError, gloAuditTrail.ActivityOutCome.Failure)
                MessageBox.Show("Template cannot be open because there are problems with the contents.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                wdTemplate.CreateNew("Word.Document")
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.WINWORD, gloAuditTrail.ActivityType.Modify, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show("Template cannot be open because there are problems with the contents.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            wdTemplate.CreateNew("Word.Document")
        End Try
    End Sub

    Private Sub LoadNewDocument()

        '' wdTemplate.CreateNew("Word.Document")
        ''added for incident CAS-00640-Q3T3V0 while clicking on save showing word dialogbox 
        ''Bug #101169 
        gloWord.LoadAndCloseWord.CreateAndLoadNewDocument(wdTemplate, oCurDoc, oWordApp, False)


    End Sub


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
                        '  Dim om As Object = System.Reflection.Missing.Value
                        Dim f As Wd.FormField = Nothing



                        Try
                            Dim o As Object = 1
                            f = r.FormFields.Item(o)
                            o = Nothing
                        Catch

                        End Try

                        If (IsNothing(f) = False) Then


                            If f.Type = Wd.WdFieldType.wdFieldFormCheckBox Then
                                f.CheckBox.Value = Not f.CheckBox.Value
                                Dim oUnit As Object = Wd.WdUnits.wdCharacter
                                Dim oCnt As Object = 1
                                Dim oMove As Object = Wd.WdMovementType.wdMove
                                Sel.MoveRight(oUnit, oCnt, oMove)

                                'SLR: 6/27/2014 : I could not understand, after moving right do we have checkbox again or do we need to again check for the type and then check for the value?
                                '16-Jul-14 Aniket: Moving the following If Block as CheckBox type was not checked
                                If f.CheckBox.Value = False Then
                                    If f.HelpText = "Group" Then  ''AllowEditing = True And
                                        Dim style As Wd.Style = CreateTableStyleFalse()
                                        For Each T1 As Wd.Table In r.Tables
                                            FormatTables(style, T1)
                                        Next
                                        style = Nothing
                                    End If

                                Else
                                    If f.HelpText = "Group" Then  ''AllowEditing = True And
                                        Dim style As Wd.Style = CreateTableStyleTrue()
                                        For Each T1 As Wd.Table In r.Tables
                                            FormatTables(style, T1)
                                        Next
                                        style = Nothing
                                    End If
                                End If

                            End If

                        End If
                    End If

                End If
            End If
        Catch excp As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.General, excp.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Public Function CreateTableStyleFalse() As Wd.Style
        Dim styleTypeTable As Object = Wd.WdStyleType.wdStyleTypeTable
        Dim StyleName As String = "New Table Style" & Convert.ToString(DateTime.Now)
        Dim styl As Wd.Style = oCurDoc.Styles.Add(StyleName, styleTypeTable)
        styl.Font.Name = "Arial"
        styl.Font.Size = 10
        Dim stylTbl As Wd.TableStyle = styl.Table
        stylTbl.Borders.Enable = 0 '1

        Dim evenrowbinding As Wd.ConditionalStyle = stylTbl.Condition(Microsoft.Office.Interop.Word.WdConditionCode.wdEvenRowBanding)
        evenrowbinding.Shading.Texture = Microsoft.Office.Interop.Word.WdTextureIndex.wdTextureNone
        'evenrowbinding.Shading.BackgroundPatternColor = Microsoft.Office.Interop.Word.WdColor.wdColorWhite
        evenrowbinding.Font.Color = -603923969 'Microsoft.Office.Interop.Word.WdColor.wdColorWhite
        evenrowbinding.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderLeft).LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleDouble
        evenrowbinding.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderRight).LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleDouble
        evenrowbinding.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderVertical).LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle
        evenrowbinding.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderBottom).LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle

        'evenrowbinding.Shading.Texture = Microsoft.Office.Interop.Word.WdTextureIndex.wdTextureNone
        'evenrowbinding.Shading.BackgroundPatternColor = Microsoft.Office.Interop.Word.WdColor.wdColorWhite
        'evenrowbinding.Font.Color = Microsoft.Office.Interop.Word.WdColor.wdColorWhite
        'evenrowbinding.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderLeft).LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleNone
        'evenrowbinding.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderRight).LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleNone
        'evenrowbinding.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderVertical).LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleNone
        'evenrowbinding.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderBottom).LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleNone

        'evenrowbinding.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderLeft).Color = Microsoft.Office.Interop.Word.WdColor.wdColorWhite
        'evenrowbinding.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderRight).Color = Microsoft.Office.Interop.Word.WdColor.wdColorWhite
        'evenrowbinding.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderVertical).Color = Microsoft.Office.Interop.Word.WdColor.wdColorWhite
        'evenrowbinding.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderBottom).Color = Microsoft.Office.Interop.Word.WdColor.wdColorWhite


        Dim oddrowbinding As Wd.ConditionalStyle = stylTbl.Condition(Microsoft.Office.Interop.Word.WdConditionCode.wdOddRowBanding)
        oddrowbinding.Shading.Texture = Microsoft.Office.Interop.Word.WdTextureIndex.wdTextureNone
        'oddrowbinding.Shading.BackgroundPatternColor = Microsoft.Office.Interop.Word.WdColor.wdColorWhite
        oddrowbinding.Font.Color = -603923969 ' Microsoft.Office.Interop.Word.WdColor.wdColorWhite
        oddrowbinding.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderLeft).LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleDouble
        oddrowbinding.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderRight).LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleDouble
        oddrowbinding.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderVertical).LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle
        oddrowbinding.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderBottom).LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle

        'oddrowbinding.Shading.Texture = Microsoft.Office.Interop.Word.WdTextureIndex.wdTextureNone
        'oddrowbinding.Shading.BackgroundPatternColor = Microsoft.Office.Interop.Word.WdColor.wdColorWhite
        'oddrowbinding.Font.Color = Microsoft.Office.Interop.Word.WdColor.wdColorWhite
        'oddrowbinding.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderLeft).LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleNone
        'oddrowbinding.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderRight).LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleNone
        'oddrowbinding.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderVertical).LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleNone
        'oddrowbinding.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderBottom).LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleNone

        'oddrowbinding.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderLeft).Color = Microsoft.Office.Interop.Word.WdColor.wdColorWhite
        'oddrowbinding.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderRight).Color = Microsoft.Office.Interop.Word.WdColor.wdColorWhite
        'oddrowbinding.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderVertical).Color = Microsoft.Office.Interop.Word.WdColor.wdColorWhite
        'oddrowbinding.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderBottom).Color = Microsoft.Office.Interop.Word.WdColor.wdColorWhite

        Dim FirstRow As Wd.ConditionalStyle = stylTbl.Condition(Microsoft.Office.Interop.Word.WdConditionCode.wdFirstRow)
        'FirstRow.Shading.BackgroundPatternColor = Microsoft.Office.Interop.Word.WdColor.wdColorGray70
        FirstRow.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderLeft).LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleDouble
        FirstRow.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderTop).LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleDouble
        FirstRow.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderRight).LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleDouble
        FirstRow.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderVertical).LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle
        FirstRow.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderBottom).LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle
        FirstRow.Font.Size = 12
        FirstRow.Font.ColorIndex = Microsoft.Office.Interop.Word.WdColorIndex.wdAuto
        FirstRow.Font.Bold = 1

        stylTbl.RowStripe = 1
        Return styl
    End Function
    Public Function CreateTableStyleTrue() As Wd.Style
        Dim styleTypeTable As Object = Wd.WdStyleType.wdStyleTypeTable
        Dim StyleName As String = "New Table Style" & Convert.ToString(DateTime.Now)
        Dim styl As Wd.Style = oCurDoc.Styles.Add(StyleName, styleTypeTable)
        styl.Font.Name = "Arial"
        styl.Font.Size = 10
        Dim stylTbl As Wd.TableStyle = styl.Table
        stylTbl.Borders.Enable = 1

        Dim evenrowbinding As Wd.ConditionalStyle = stylTbl.Condition(Microsoft.Office.Interop.Word.WdConditionCode.wdEvenRowBanding)
        evenrowbinding.Shading.Texture = Microsoft.Office.Interop.Word.WdTextureIndex.wdTextureNone
        evenrowbinding.Shading.BackgroundPatternColor = Microsoft.Office.Interop.Word.WdColor.wdColorGray10
        evenrowbinding.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderLeft).LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleDouble
        evenrowbinding.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderRight).LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleDouble
        evenrowbinding.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderVertical).LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle
        evenrowbinding.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderBottom).LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle

        Dim FirstRow As Wd.ConditionalStyle = stylTbl.Condition(Microsoft.Office.Interop.Word.WdConditionCode.wdFirstRow)
        'FirstRow.Shading.BackgroundPatternColor = Microsoft.Office.Interop.Word.WdColor.wdColorGray70
        FirstRow.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderLeft).LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleDouble
        FirstRow.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderTop).LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleDouble
        FirstRow.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderRight).LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleDouble
        FirstRow.Font.Size = 14
        FirstRow.Font.ColorIndex = Microsoft.Office.Interop.Word.WdColorIndex.wdAuto
        FirstRow.Font.Bold = 1

        stylTbl.RowStripe = 1
        Return styl
    End Function

    Private Sub btn_Click(ByVal btn As oOffice.CommandBarButton, ByRef Cancel As Boolean)
        myidx = btn.Index
    End Sub

    Private Sub tlsTemplateGallery_ToolStripClick(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tlsTemplateGallery.ToolStripClick
        Try
            Select Case e.ClickedItem.Name
                Case "Mic"
                    'UpdateVoiceLog("--------------SwitchOff Mic started from tblButtons_ButtonClick in Template Gallery when " & e.ClickedItem.Name & " is invoked------------")
                    gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.General, "SwitchOff Mic started from tblButtons_ButtonClick in Template Gallery when " & e.ClickedItem.Name & " is invoked", gloAuditTrail.ActivityOutCome.Success)
                    If MyMDIParent.DgnMicBtn1.MicState = DNSTools.DgnMicStateConstants.dgnmicOn Then
                        MyMDIParent.DgnMicBtn1.MicState = DNSTools.DgnMicStateConstants.dgnmicOff
                        e.ClickedItem.Image = Global.gloEMR.My.Resources.Mic_OFF
                        e.ClickedItem.ToolTipText = "Microphone Off"
                    ElseIf MyMDIParent.DgnMicBtn1.MicState = DNSTools.DgnMicStateConstants.dgnmicOff Then
                        MyMDIParent.DgnMicBtn1.MicState = DNSTools.DgnMicStateConstants.dgnmicOn
                        e.ClickedItem.Image = Global.gloEMR.My.Resources.Mic_ON
                        e.ClickedItem.ToolTipText = "Microphone On"
                    End If
                    'UpdateVoiceLog("--------------SwitchOff Mic Completed from tblButtons_ButtonClick in Template Gallery when " & e.ClickedItem.Name & " is invoked------------")
                    gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.General, "SwitchOff Mic Completed from tblButtons_ButtonClick in Template Gallery when " & e.ClickedItem.Name & " is invoked", gloAuditTrail.ActivityOutCome.Success)
                Case "New" '0 'new
                    If oCurDoc.Saved Then
                        NewTemplate()
                    Else
                        Dim oResult As DialogResult
                        oResult = MessageBox.Show("Do you want to save the changes to template?", gstrMessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)
                        Select Case oResult
                            Case Windows.Forms.DialogResult.Yes
                                If SaveTemplate(False) Then
                                    NewTemplate()
                                End If
                            Case Windows.Forms.DialogResult.No
                                NewTemplate()

                            Case Windows.Forms.DialogResult.Cancel
                                Exit Sub
                        End Select
                    End If
                Case "Open" '1 'open
                    OpenTemplate()
                Case "SaveClose" '2 'SaveClose
                    SaveTemplate(True)
                Case "Save"
                    SaveTemplate(False)
                Case "SaveAs" '2 'Save As
                    SaveAsTemplate()
                Case "Insert CheckBox" '3
                    AddCheckbox()
                Case "Insert DropDownlist"
                    AddDropDown()
                Case "Capture Sign"
                    If IsFeasible(enumControls.None) Then
                        InsertSignature()
                    End If
                Case "Undo"
                    UnDoChanges()
                Case "Redo"
                    ReDoChanges()
                Case "Insert File"
                    If IsFeasible(enumControls.None) Then
                        ImportDocument(1)
                    End If
                Case "Scan Documents"
                    If IsFeasible(enumControls.None) Then
                        ImportDocument(2)
                    End If
                Case "Insert Header"
                    If IsFeasible(enumControls.None) Then
                        InsertLogo()
                    End If
                Case "Close"
                    Me.Close()
                Case "AddBM"
                    AddBookMark()
                Case "RemoveBM"
                    RemoveAllBookMarks()
                Case "ShowHideBM"
                    ShowHideBookMarks()
                Case "AssociateEMField"
                    GetEMAssociatedField()
            End Select

        Catch ex As Exception
            If ex.Message = "This command is not available." Then
                MessageBox.Show("DataFields, Checkboxes and drop down lists cannot be used inside text boxes or other shapes.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Else
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If

        End Try
    End Sub

    Private Sub GetEMAssociatedField()
        Dim ofrm As New frmEMTagAssociation(_arrLabs, _arrOrders, _arrOtherDiag, _arrManagment)
        With ofrm
            .StartPosition = FormStartPosition.CenterParent
            .ShowDialog(IIf(IsNothing(ofrm.Parent), Me, ofrm.Parent))
            _arrLabs = .arrLabs
            _arrManagment = .arrManagment
            _arrOrders = .arrOrders
            _arrOtherDiag = .arrOtherDiag
            .Dispose()
        End With
    End Sub
    Private Sub wdTemplate_BeforeDocumentClosed(ByVal sender As Object, ByVal e As AxDSOFramer._DFramerCtlEvents_BeforeDocumentClosedEvent) Handles wdTemplate.BeforeDocumentClosed
        Try

            If Not oCurDoc Is Nothing Then
                'CODE ADDED BY DIPAK 20091224 ''' TO HANDLE EMR-PM WORD INSTANCE ''
                garrOpenDocument.Remove(oCurDoc.FullName)
                'END CODE ADDED BY DIPAK
                If (IsNothing(oWordApp) = False) Then
                    Try

                        RemoveHandler oWordApp.WindowSelectionChange, AddressOf DDLCBEvent
                        RemoveHandler oWordApp.WindowBeforeDoubleClick, AddressOf OnFormClicked
                        RemoveHandler oWordApp.WindowBeforeRightClick, AddressOf BeforeRightClick

                    Catch ex As Exception

                    End Try


                    gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.Close, "RemoveHandler for WindowSelectionChange,WindowBeforeDoubleClick  for oWordApp", gloAuditTrail.ActivityOutCome.Success)
                    For Each oFile As Wd.RecentFile In oWordApp.RecentFiles
                        If (IsNothing(oFile) = False) Then
                            Try
                                If oFile.Path = gloSettings.FolderSettings.AppTempFolderPath Then
                                    Try
                                        oFile.Delete()
                                    Catch ex As Exception
                                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)

                                        ex = Nothing
                                    End Try
                                End If
                            Catch ex As Exception
            
                            End Try
                        End If
                    Next

                End If
                'UpdateVoiceLog("Remove from word recent File list")
                gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.Close, "Remove from word recent File list", gloAuditTrail.ActivityOutCome.Success)
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            'UpdateVoiceLog(ex.ToString)
        End Try
    End Sub

    Private Sub wdTemplate_OnDocumentClosed(ByVal sender As Object, ByVal e As System.EventArgs) Handles wdTemplate.OnDocumentClosed
        Try
            'UpdateVoiceLog(" wdTemplate_OnDocumentClosed - Start")
            gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.Close, "wdTemplate_OnDocumentClosed - Start", gloAuditTrail.ActivityOutCome.Success)
            If Not oCurDoc Is Nothing Then

                Marshal.ReleaseComObject(oCurDoc)
                oCurDoc = Nothing
            End If
            'If Not oWordApp Is Nothing Then
            '    ' Marshal.FinalReleaseComObject(oWordApp)
            '    oWordApp = Nothing
            'End If
            'GC.Collect()
            'GC.WaitForPendingFinalizers()
            'GC.Collect()
            'GC.WaitForPendingFinalizers()
            'UpdateVoiceLog(" wdTemplate_OnDocumentClosed - End")
            gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.Close, "wdTemplate_OnDocumentClosed - End", gloAuditTrail.ActivityOutCome.Success)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            'UpdateVoiceLog(ex.ToString)
        End Try
    End Sub

    Private Sub wdTemplate_OnDocumentOpened(ByVal sender As Object, ByVal e As AxDSOFramer._DFramerCtlEvents_OnDocumentOpenedEvent) Handles wdTemplate.OnDocumentOpened
        Try
            'UpdateVoiceLog(" wdTemplate_OnDocumentOpened Started ")
            gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.Open, "wdTemplate_OnDocumentOpened Started", gloAuditTrail.ActivityOutCome.Success)
            oCurDoc = wdTemplate.ActiveDocument
            oWordApp = oCurDoc.Application
            If Not (garrOpenDocument.Contains(oCurDoc.FullName)) Then
                garrOpenDocument.Add(oCurDoc.FullName)
            End If

            AddHandler oWordApp.WindowSelectionChange, AddressOf DDLCBEvent
            AddHandler oWordApp.WindowBeforeDoubleClick, AddressOf OnFormClicked
            AddHandler oWordApp.WindowBeforeRightClick, AddressOf BeforeRightClick
            oCurDoc.ActiveWindow.SetFocus()
            oCurDoc.Application.Options.ShowDevTools = False
            'UpdateVoiceLog(" wdTemplate_OnDocumentOpened END ")
            gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.Open, "wdTemplate_OnDocumentOpened END", gloAuditTrail.ActivityOutCome.Success)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.Open, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            'UpdateVoiceLog(ex.ToString)
        End Try

    End Sub
    Public Sub BeforeRightClick(ByVal Sel As Wd.Selection, ByRef Cancel As Boolean)
        Try
            'Me.WindowState = FormWindowState.Maximized
            'Me.Invoke(New MethodInvoker(AddressOf MaximizeForm))
            'oCurDoc.ActiveWindow.SetFocus()
            blnIsrightclik = True
        Catch ex As Exception

        End Try
    End Sub


    Private Sub AddBookMark()
        Dim cnt As Int32 = 1 '0 SLR: initialized to 1 : optimization
        oCurDoc.Bookmarks.DefaultSorting = Wd.WdBookmarkSortBy.wdSortByLocation
        ''Bug #86640: 00000951 : Unable to add multiple bookmarks on new document
        For i As Int32 = 1 To oCurDoc.Bookmarks.Count + 1
            If oCurDoc.Bookmarks.Exists("BM" & i.ToString) = False Then
                cnt = i
                Exit For
            End If
        Next
        'If cnt = 0 Then
        '    cnt = 1
        'End If
        With oCurDoc.Bookmarks
            .Add(Range:=oCurDoc.Application.Selection.Range, Name:="BM" & cnt.ToString)
            .DefaultSorting = Wd.WdBookmarkSortBy.wdSortByLocation
            .ShowHidden = False
        End With
    End Sub

    Private Sub RemoveAllBookMarks()
        'SLR: Changed to avoid deleting from beginning..
        oCurDoc.Bookmarks.DefaultSorting = Wd.WdBookmarkSortBy.wdSortByLocation 'Wd.WdBookmarkSortBy.wdSortByName
        ''Bug #86640: 00000951 : Unable to add multiple bookmarks on new document
        For i As Int32 = oCurDoc.Bookmarks.Count To 1 Step -1
            Dim bmBookMark As Wd.Bookmark = oCurDoc.Bookmarks.Item(i)
            'If InStr(bmBookMark.Name, "BM") Then
            If bmBookMark.Name.StartsWith("BM") Then
                bmBookMark.Delete()
            End If
        Next

        'For Each bmBookMark As Wd.Bookmark In oCurDoc.Range.Bookmarks
        '    If InStr(bmBookMark.Name, "BM") Then
        '        bmBookMark.Delete()
        '    End If
        'Next

        'Dim cnt As Int32 = oCurDoc.Bookmarks.Count
        'Dim arr As New ArrayList

        'For i As Int32 = 1 To cnt
        '    If InStr(oCurDoc.Bookmarks.Item(i).Name, "BM") Then
        '        arr.Add(oCurDoc.Bookmarks.Item(i).Name)

        '    End If
        'Next
        'For j As Int32 = 0 To arr.Count - 1
        '    If oCurDoc.Bookmarks.Exists(arr(j).ToString) Then
        '        oCurDoc.Bookmarks.Item(arr(j).ToString).Delete()
        '    End If
        'Next
        'arr.Clear()
        'arr = Nothing

    End Sub

    Private Sub ShowHideBookMarks()

        If oCurDoc.ActiveWindow.View.ShowBookmarks Then
            oCurDoc.ActiveWindow.View.ShowBookmarks = False
        Else
            oCurDoc.ActiveWindow.View.ShowBookmarks = True
        End If

    End Sub

    '' SUDHIR 20090629 ''
    Private Sub trvDataDictionary_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles trvDataDictionary.MouseDown
        Try
            If e.Button = Windows.Forms.MouseButtons.Right Then
                Try
                    If (IsNothing(trvDataDictionary.ContextMenu) = False) Then
                        gloGlobal.cEventHelper.RemoveAllEventHandlers(trvDataDictionary.ContextMenu)
                        If (IsNothing(trvDataDictionary.ContextMenu.MenuItems)) Then
                            trvDataDictionary.ContextMenu.MenuItems.Clear()
                        End If

                        trvDataDictionary.ContextMenu.Dispose()
                        trvDataDictionary.ContextMenu = Nothing
                    End If
                Catch ex As Exception

                End Try

                Dim oContext As New ContextMenu
                Dim oContextItem As New MenuItem
                oContextItem.Text = "Add Table Template"
                oContext.MenuItems.Add(oContextItem)
                trvDataDictionary.ContextMenu = oContext
                AddHandler oContextItem.Click, AddressOf OnTableTemplate_Click
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub OnTableTemplate_Click(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim ofrm As New frmMSTTableTemplate
            ofrm.ShowDialog(IIf(IsNothing(ofrm.Parent), Me, ofrm.Parent))
            Fill_DataDictionary()
            ofrm.Dispose()
            ofrm = Nothing
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    '' END SUDHIR ''

    Private Sub trvDataDictionary_NodeMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles trvDataDictionary.NodeMouseDoubleClick
        Try
            If IsNothing(e.Node) Then
                Exit Sub
            End If
            trvDataDictionary.SelectedNode = e.Node

            If IsNothing(trvDataDictionary.SelectedNode) = True Then
                Exit Sub
            End If

            If trvDataDictionary.SelectedNode Is trvDataDictionary.Nodes(0) Then
                Exit Sub
            End If

            If trvDataDictionary.SelectedNode.Parent Is trvDataDictionary.Nodes(0) Then
                Exit Sub
            End If
            If IsFeasible(enumControls.FormFieldControl) = False Then
                Exit Sub
            End If
            Dim objDataDictionary As New clsDataDictionary
            oCurDoc.ActiveWindow.SetFocus()
            objDataDictionary.ViewDataDictionary(Trim(trvDataDictionary.SelectedNode.Text), trvDataDictionary.SelectedNode.Tag)
            Dim oNameField As Wd.FormField
            If chkCaption.Checked = True Then
                oCurDoc.Application.Selection.TypeText(trvDataDictionary.SelectedNode.Text & ": ")
            End If
            oNameField = oCurDoc.FormFields.Add(oCurDoc.Application.Selection.Range, Wd.WdFieldType.wdFieldFormTextInput)
            oNameField.Result = trvDataDictionary.SelectedNode.Text 'Result To show caption
            oNameField.StatusText = objDataDictionary.FieldNames 'Status text to hold Table & field names 
            oNameField.HelpText = trvDataDictionary.SelectedNode.Text 'Help text to hold group
            'oNameField.Name = trvDataDictionary.SelectedNode.Parent.Text

            objDataDictionary = Nothing
            'End If
            oCurDoc.ActiveWindow.SetFocus()

        Catch ex As Exception
            If ex.Message = "This command is not available." Then
                MessageBox.Show("Current operation is invalid for adding the required data fields", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Else
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End Try

    End Sub
    Private Function IsFeasible(ByVal eCtrlType As enumControls) As Boolean
        Try
            If oCurDoc.Application.ActiveDocument.ProtectionType = Wd.WdProtectionType.wdAllowOnlyComments Or oCurDoc.Application.ActiveDocument.ProtectionType = Wd.WdProtectionType.wdAllowOnlyFormFields Or oCurDoc.Application.ActiveDocument.ProtectionType = Wd.WdProtectionType.wdAllowOnlyReading Then
                MessageBox.Show("Current operation is invalid as document is under protection mode.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return False
            End If

            If oCurDoc.Application.Selection.ShapeRange.Count > 0 Then
                If eCtrlType = enumControls.FormFieldControl Or eCtrlType = enumControls.ContentControl Then
                    MessageBox.Show("Checkboxes and drop down lists cannot be used inside text boxes or other shapes.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Return False
                End If
            End If
            If Not oCurDoc.Application.Selection.HeaderFooter Is Nothing Then
                If oCurDoc.Application.Selection.HeaderFooter.IsHeader Then
                    If eCtrlType = enumControls.FormFieldControl Or eCtrlType = enumControls.ContentControl Then
                        MessageBox.Show("Checkboxes and drop down lists cannot be used inside text boxes or other shapes.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        Return False
                    End If
                End If
            End If
            Dim r As Wd.Range = oCurDoc.Application.Selection.Range
            r.SetRange(oCurDoc.Application.Selection.Start, oCurDoc.Application.Selection.End + 1)
            If (r.ContentControls.Count = 1) Then
                'If oCurDoc.Application.Selection.Range.ContentControls.Count > 0 Then
                If eCtrlType = enumControls.FormFieldControl Or eCtrlType = enumControls.ContentControl Then
                    MessageBox.Show("Checkboxes and drop down lists cannot be used inside text boxes or other shapes.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Return False
                End If

            ElseIf Not r.ParentContentControl Is Nothing Then
                If eCtrlType = enumControls.FormFieldControl Or eCtrlType = enumControls.ContentControl Then
                    MessageBox.Show("Checkboxes and drop down lists cannot be used inside text boxes or other shapes.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Return False
                End If
            End If

            Return True
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End Try


    End Function
    'ADDED BY SHUBHANGI 20100625 TO CHECK FOR LIQUID DATA
    Private Function IsFeasibleforLiquiddata(ByVal eCtrlType As enumControls) As Boolean
        Try
            If Not oCurDoc.Application.Selection.HeaderFooter Is Nothing Then
                If oCurDoc.Application.Selection.HeaderFooter.IsHeader Then
                    If eCtrlType = enumControls.FormFieldControl Or eCtrlType = enumControls.ContentControl Then
                        MessageBox.Show("Current operation is invalid as document is under Header", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        Return False
                    End If
                End If
            End If

            Return True
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End Try

    End Function
#Region "Voice implementation"

    Public Sub ActivateBasicVoiceCmds(ByVal myVoiceCol As DNSTools.DgnStrings) Implements mdlgloVoice.gloVoice.ActivateBasicVoiceCmds

        'If myVoiceCol.Count > 0 Then
        '    Dim objSender As Object = Nothing
        '    Dim obje As EventArgs = Nothing
        '    'Dim objKeye As KeyEventArgs
        '    Dim objtblbtn As New ToolStripButton

        '    Select Case myVoiceCol.Item(1)


        '        Case "Save Template"
        '            objtblbtn.Name = "Save"
        '            Dim objtbl As New System.Windows.Forms.ToolStripItemClickedEventArgs(objtblbtn)
        '            tlsTemplateGallery_ToolStripClick(objSender, objtbl)

        '        Case "Close Template"
        '            objtblbtn.Name = "Close"
        '            Dim objtbl As New System.Windows.Forms.ToolStripItemClickedEventArgs(objtblbtn)
        '            tlsTemplateGallery_ToolStripClick(objSender, objtbl)

        '    End Select
        'End If

    End Sub

    'Private Sub AddBasicVoiceCommands()
    '    TemplateGalleryVoiceCol.Clear()
    '    TemplateGalleryVoiceCol.Add("Save Template")
    '    TemplateGalleryVoiceCol.Add("Close Template")
    'End Sub
    Private Function AddBasicVoiceCommands() As Hashtable

        Dim oHashtable As New Hashtable
        oHashtable.Clear()
        oHashtable.Add("Save Template Gallery", "Save")
        oHashtable.Add("Close Template Gallery", "Print")
        oHashtable.Add("Save & Close Template Gallery", "SaveClose")
        Return oHashtable

    End Function
    ''' <summary>
    ''' Trigger Voice commands
    ''' </summary>
    ''' <param name="VoiceCol"></param>
    ''' <remarks></remarks>
    Public Sub ActivateVoiceCmds(ByVal VoiceCol As DNSTools.DgnStrings) Implements mdlgloVoice.gloVoice.ActivateVoiceCmds
        If gblnVoiceEnabled = True And gblnSpeakerExists = True Then
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
        'vVoiceMenu.Remove(1)
        'If IsNothing(TemplateGalleryVoiceCol) Then
        '    TemplateGalleryVoiceCol = New DNSTools.DgnStrings
        '    Call AddBasicVoiceCommands()
        'End If
        'vVoiceMenu.ListSetStrings(gstrMessageBoxCaption, TemplateGalleryVoiceCol)
        'vVoiceMenu.Add(1, "<Template Gallery>", "", "")
        If gblnVoiceEnabled = True And gblnSpeakerExists = True Then
            If Not IsNothing(ogloVoice) Then
                ogloVoice.AddVoiceCommands()
            End If
        End If
    End Sub

    Public Sub CustomGetchanges(ByVal e As AxDNSTools._DDgnDictCustomEvents_GetChangesEvent) Implements mdlgloVoice.gloVoice.CustomGetchanges

    End Sub

    Public Sub CustomMakechanges(ByVal e As AxDNSTools._DDgnDictCustomEvents_MakeChangesEvent) Implements mdlgloVoice.gloVoice.CustomMakechanges

    End Sub

    'Private Sub ShowMicrophone()
    '    Try
    '        If gblnVoiceEnabled = True And gblnSpeakerExists = True Then
    '            tlsTemplateGallery.MyToolStrip.Items("Mic").Visible = True
    '            tlsTemplateGallery.MyToolStrip.Items("Mic").Image = Global.gloEMR.My.Resources.Mic_OFF
    '        Else
    '            tlsTemplateGallery.MyToolStrip.Items("Mic").Visible = False
    '        End If
    '    Catch ex As Exception
    '        MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Sub

    'Private Sub TurnOffMicrophone()
    '    Try
    '        'code commented as logic for mic on/off has changed
    '        If gblnVoiceEnabled = True And gblnSpeakerExists = True Then

    '            If CType(Me.MdiParent, MainMenu).DgnMicBtn1.MicState = DNSTools.DgnMicStateConstants.dgnmicOn Then
    '                CType(Me.MdiParent, MainMenu).DgnMicBtn1.MicState = DNSTools.DgnMicStateConstants.dgnmicOff
    '                tlsTemplateGallery.MyToolStrip.Items("Mic").Image = Global.gloEMR.My.Resources.Mic_OFF
    '            End If

    '        Else
    '            tlsTemplateGallery.MyToolStrip.Items("Mic").Visible = False
    '        End If
    '    Catch ex As Exception
    '        MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Sub

    Public Sub Navigate(ByVal strstring As String) Implements mdlHotkey.IHotKey.Navigate
        If strstring = "ON" Then
            If gblnVoiceEnabled = True And gblnSpeakerExists = True Then
                tlsTemplateGallery.MyToolStrip.Items("Mic").Visible = True
                tlsTemplateGallery.MyToolStrip.Items("Mic").Image = Global.gloEMR.My.Resources.Mic_ON
            End If
        ElseIf strstring = "OFF" Then
            If gblnVoiceEnabled = True And gblnSpeakerExists = True Then
                tlsTemplateGallery.MyToolStrip.Items("Mic").Visible = True
                tlsTemplateGallery.MyToolStrip.ButtonsToHide.Remove(tlsTemplateGallery.MyToolStrip.Items("Mic").Name)
            Else
                tlsTemplateGallery.MyToolStrip.Items("Mic").Visible = False
                If tlsTemplateGallery.MyToolStrip.ButtonsToHide.Contains(tlsTemplateGallery.MyToolStrip.Items("Mic").Name) = False Then
                    tlsTemplateGallery.MyToolStrip.ButtonsToHide.Add(tlsTemplateGallery.MyToolStrip.Items("Mic").Name)
                End If

            End If
            tlsTemplateGallery.MyToolStrip.Items("Mic").Image = Global.gloEMR.My.Resources.Mic_OFF
        Else
            'oCurDoc.Application.Selection.Find.ClearFormatting()
            'Try
            '    With oCurDoc.Application.Selection.Find
            '        .Text = strstring
            '        .Replacement.Text = " "
            '        .Forward = True
            '        .Wrap = Wd.WdFindWrap.wdFindContinue
            '        .Format = False
            '        .MatchCase = False
            '        .MatchWholeWord = False
            '        .MatchWildcards = False
            '        .MatchSoundsLike = False
            '        .MatchAllWordForms = False
            '    End With
            '    oCurDoc.Application.Selection.Find.Execute()
            'Catch ex As Exception
            If Not oCurDoc Is Nothing Then
                oCurDoc.ActiveWindow.SetFocus()
                Try
                    ''Bug #75280 : gloEMR- Exam - F5 functional key is not working
                    gloEMRWord.clsWordDocument.FindAndReplace(MyApp:=oCurDoc.Application, FindText:=strstring, ReplaceWith:=" ", Forward:=True, Wrap:=Wd.WdFindWrap.wdFindContinue, Replace:=Wd.WdReplace.wdReplaceNone, MatchWildCards:=False, MatchWholeWord:=False)
                Catch ex2 As Exception

                End Try
                'End Try
            End If



            End If
    End Sub
#End Region

#Region "Liquid Data Implementation"
    Private Sub trvDiscrete_NodeMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles trvDiscrete.NodeMouseDoubleClick


        Try
            If IsNothing(e.Node.Parent) Then
                Exit Sub
            End If

            If IsFeasibleforLiquiddata(enumControls.ContentControl) = False Then
                Exit Sub
            End If
            trvDiscrete.SelectedNode = e.Node
            Dim myNode As myTreeNode
            Dim strElementName As String = ""
            myNode = CType(trvDiscrete.SelectedNode, myTreeNode)
            m_ElementId = myNode.Key

            If Not oDatatrvDiscrete_NodeMouseDoubleClick Is Nothing Then
                oDatatrvDiscrete_NodeMouseDoubleClick.Dispose()
                oDatatrvDiscrete_NodeMouseDoubleClick = Nothing
            End If

            oDatatrvDiscrete_NodeMouseDoubleClick = objclsTemplateGallery.GetDataField(m_ElementId)
            If (IsNothing(m_arraylist) = False) Then
                m_arraylist.Clear()
                m_arraylist = Nothing
            End If

            If Not oDatatrvDiscrete_NodeMouseDoubleClick Is Nothing Then
                m_FieldList = New SortedList
                m_arraylist = New ArrayList
                For _inc As Int32 = 0 To oDatatrvDiscrete_NodeMouseDoubleClick.Rows.Count - 1
                    ''set the Values to gobal variables for further processing
                    m_Required = oDatatrvDiscrete_NodeMouseDoubleClick.Rows(0)("bIsMandatory")
                    m_DataType = oDatatrvDiscrete_NodeMouseDoubleClick.Rows(_inc)("sElementType")
                    If oDatatrvDiscrete_NodeMouseDoubleClick.Rows(_inc)("nElementID") = m_ElementId Then
                        m_Desc = oDatatrvDiscrete_NodeMouseDoubleClick.Rows(_inc)("sElementName").ToString
                        m_Fieldcategory = oDatatrvDiscrete_NodeMouseDoubleClick.Rows(_inc)("sCategoryName").ToString
                    Else
                        If m_DataType = "Text" Then
                            m_TextValue = oDatatrvDiscrete_NodeMouseDoubleClick.Rows(_inc)("sElementName").ToString
                        ElseIf m_DataType = "Table" Or m_DataType = "Group" Then
                            m_list = New myList
                            m_list.ID = oDatatrvDiscrete_NodeMouseDoubleClick.Rows(_inc)("nElementID")
                            m_list.HistoryCategory = oDatatrvDiscrete_NodeMouseDoubleClick.Rows(_inc)("sCategoryName").ToString()
                            m_list.HistoryItem = oDatatrvDiscrete_NodeMouseDoubleClick.Rows(_inc)("sItemName").ToString()
                            m_list.ControlType = CType(oDatatrvDiscrete_NodeMouseDoubleClick.Rows(_inc)("nControlType"), ControlType)
                            m_list.AssociatedCategory = CType(oDatatrvDiscrete_NodeMouseDoubleClick.Rows(_inc)("sAssociatedCategory"), String)
                            m_list.AssociatedItem = CType(oDatatrvDiscrete_NodeMouseDoubleClick.Rows(_inc)("sAssociateditem"), String)
                            m_list.AssociatedProperty = CType(oDatatrvDiscrete_NodeMouseDoubleClick.Rows(_inc)("sAssociatedProperty"), String)
                            m_Caption = oDatatrvDiscrete_NodeMouseDoubleClick.Rows(_inc)("sElementName")
                            m_arraylist.Add(m_list)
                        Else
                            m_list = New myList
                            m_list.ID = oDatatrvDiscrete_NodeMouseDoubleClick.Rows(_inc)("nElementID")
                            m_list.HistoryItem = oDatatrvDiscrete_NodeMouseDoubleClick.Rows(_inc)("sElementName")
                            m_list.ControlType = CType(oDatatrvDiscrete_NodeMouseDoubleClick.Rows(_inc)("nControlType"), ControlType)
                            m_list.AssociatedProperty = CType(oDatatrvDiscrete_NodeMouseDoubleClick.Rows(_inc)("sAssociatedProperty"), String)
                            m_arraylist.Add(m_list)

                        End If
                    End If
                Next
                ''Only add the controls in the document if the count greate than one
                If oDatatrvDiscrete_NodeMouseDoubleClick.Rows.Count > 0 Then
                    AddLiquidControl()
                End If

            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MsgBox(ex.ToString)

        Finally
            ''Reset all the variables for further use
            m_TextValue = String.Empty
            If Not IsNothing(m_FieldList) Then
                m_FieldList.Clear()
                m_FieldList = Nothing
            End If

            m_DataType = String.Empty
            m_Desc = String.Empty
            m_ElementId = 0
            noCols = 0
            noRows = 0
        End Try

    End Sub

    Private Sub AddLiquidControl()
        Try


            Select Case m_DataType
                Case "Single Selection"
                    InsertDropDown()
                Case "Boolean"
                    InsertCheckbox()
                Case "Multiple Selection"
                    InsertCheckboxInBooleanFormat()
                Case "Text"

                    If m_Fieldcategory = "5" Then
                        InsertTextControl_HPI()
                    Else
                        InsertTextControl()
                    End If
                Case "Table"
                    InsertTable()
                Case "Group"
                    InsertGroup() 'In Liquid data project it name is InsertGroupNew()
                Case Else

            End Select
        Catch ex As Exception
            'If ex.Message = "This command is not available." Then
            '    MessageBox.Show("Current operation is invalid for adding the required data fields", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            'Else
            '    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            '    MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            'End If
        End Try
    End Sub
    ''' <summary>
    ''' Insert Text Content control as liquid datafield control
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InsertTextControl_HPI()
        Try
            With oCurDoc.Application.Selection
                Dim cntcontrol As Wd.ContentControl = oCurDoc.Application.Selection.Range.ParentContentControl
                If Not IsNothing(cntcontrol) Then
                    oCurDoc.Application.Selection.Move(Unit:=Wd.WdUnits.wdSection, Count:=1)
                End If
                'Dim oPareneContentControl As Wd.ContentControl
                .Range.ContentControls.Add(Microsoft.Office.Interop.Word.WdContentControlType.wdContentControlRichText)
                .ParentContentControl.Type = Microsoft.Office.Interop.Word.WdContentControlType.wdContentControlRichText

                '.Range.ContentControls(1).DefaultTextStyle = oCurDoc.Application.Selection.Style
                ''Title will be Field description

                ''Elementid, Required flag  for reference stored in tag and temporary variables
                .ParentContentControl.Tag = m_ElementId & "|" & m_DataType & "|" & m_Required & "|" & m_Fieldcategory

                ' .ParentContentControl.Temporary = m_Require
                Dim _strCaption As String = ""
                Dim selectedIndex As Integer
                If m_Desc.Contains("Brief") Then
                    Dim retval As String() = Split(m_Desc, "(")
                    If Not IsNothing(retval) Then
                        If retval.Length > 1 Then
                            _strCaption = retval(0)
                            selectedIndex = 1
                        End If
                    End If
                ElseIf m_Desc.Contains("Extended") Then
                    Dim retval As String() = Split(m_Desc, "(")
                    If Not IsNothing(retval) Then
                        If retval.Length > 1 Then
                            _strCaption = retval(0)
                            selectedIndex = 2
                        End If
                    End If
                Else
                    _strCaption = m_Desc
                End If
                .ParentContentControl.Title = _strCaption
                ''Type the caption for the Text Datafield
                .TypeText(Text:=_strCaption & "  ")
                Dim oContainer As Wd.ContentControl
                oContainer = .Range.ContentControls.Add(Wd.WdContentControlType.wdContentControlRichText)
                oContainer.Tag = "DropDown"
                Dim odrop As Wd.ContentControl
                odrop = oContainer.Range.ContentControls.Add(Microsoft.Office.Interop.Word.WdContentControlType.wdContentControlComboBox)
                odrop.DropdownListEntries.Clear()


                odrop.DropdownListEntries.Add(Text:="Brief", Value:="1")
                odrop.DropdownListEntries.Add(Text:="Extended", Value:="2")

                .MoveRight(Unit:=Wd.WdUnits.wdCharacter, Count:=1)
                .TypeText(Text:="  : ")
                ''oPareneContentControl.Range.Application.Selection.MoveRight(Unit:=Wd.WdUnits.wdCharacter, Count:=m_Desc.Length + 7)
                .Range.ContentControls.Add(Wd.WdContentControlType.wdContentControlRichText)




                If m_TextValue <> "" Then
                    ''Type the text int he Richtext control
                    .TypeText(Text:=m_TextValue)
                End If
                '' .MoveRight(Unit:=Wd.WdUnits.wdCharacter, Count:=1)


                If selectedIndex = 1 Then
                    odrop.DropdownListEntries.Item(1).Select()
                ElseIf selectedIndex = 2 Then
                    odrop.DropdownListEntries.Item(2).Select()
                    '.ParentContentControl.DropdownListEntries.Item(2).Select()
                End If

                ''move cursor out of the rich text content control
                .MoveDown(Unit:=Wd.WdUnits.wdLine, Count:=1)
                .Select()
                ''.MoveRight(Unit:=Wd.WdUnits.wdLine, Count:=1)

                .MoveRight(Unit:=Wd.WdUnits.wdCharacter, Count:=3)
                .InsertParagraph()
                .MoveDown(Unit:=Wd.WdUnits.wdLine, Count:=1)
                .Select()
            End With
            oCurDoc.ActiveWindow.SetFocus()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            If oCurDoc.Application.Selection.Range.ParentContentControl.Type = Microsoft.Office.Interop.Word.WdContentControlType.wdContentControlRichText Then
                oCurDoc.Application.Selection.Range.ParentContentControl.Delete(True)
            End If
        End Try
    End Sub

    ''' <summary>
    ''' Insert Drop down Content control as Liquid datafield
    ''' </summary>
    ''' <remarks></remarks>
    ''' 
    Private Sub InsertDropDown()
        Try
            If Not IsNothing(m_FieldList) Then
                With oCurDoc.Application.Selection
                    Dim cntcontrol As Wd.ContentControl = oCurDoc.Application.Selection.Range.ParentContentControl
                    If Not IsNothing(cntcontrol) Then
                        oCurDoc.Application.Selection.Move(Unit:=Wd.WdUnits.wdSection, Count:=1)
                    End If

                    .TypeText(Text:=m_Desc & ": ")
                    .Range.ContentControls.Add(Wd.WdContentControlType.wdContentControlDropdownList)

                    ''Title with be Field description
                    .ParentContentControl.Title = m_Desc
                    .ParentContentControl.Temporary = True '' THIS PROPERTY IS SET TO TRUE THEN INDICATE LIQUID DATA DROP DOWN LIST '' FALSE INDICATES DROP DOWN LIST OTHER THAN LIQUID DATA '' 
                    ''Elementid, Required flag  for reference stored in tag and temporary variables
                    .ParentContentControl.Tag = m_ElementId & "|" & m_DataType & "|" & m_Required & "|" & m_Fieldcategory
                    '.ParentContentControl.Temporary = m_Required
                    .ParentContentControl.DropdownListEntries.Clear()
                    ''Loop thourgh each entry in hash table and add as drop down items 
                    'For Each objEntry As DictionaryEntry In m_FieldList
                    '    .ParentContentControl.DropdownListEntries.Add(Text:=objEntry.Value.ToString, Value:=objEntry.Key.ToString)
                    'Next
                    For Each objEntry As myList In m_arraylist
                        .ParentContentControl.DropdownListEntries.Add(Text:=objEntry.HistoryItem.ToString, Value:=objEntry.ID.ToString)
                    Next
                    .MoveRight(Unit:=Wd.WdUnits.wdCharacter, Count:=1)
                    .InsertParagraph()
                    .MoveRight(Unit:=Wd.WdUnits.wdCharacter, Count:=1)
                End With
                oCurDoc.ActiveWindow.SetFocus()
            End If

            'ADDED BY SHUBHANGI 20100625 TO HANDLE COM EXCEPTION 
            ''DONT CHANGE THE ex.Message  TO ex.ToString FOR THIS CASE ONLY
        Catch ex As COMException
            If ex.Message = "This command is not available." Then
                MessageBox.Show("Current operation is invalid for adding the required data fields", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Else
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.InsertDropDown, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            If oCurDoc.Application.Selection.Range.ParentContentControl.Type = Microsoft.Office.Interop.Word.WdContentControlType.wdContentControlRichText Then
                oCurDoc.Application.Selection.Range.ParentContentControl.Delete(True)
            End If
        End Try

    End Sub
    'Private Sub InsertCheckbox()
    '    If Not IsNothing(m_FieldList) Then

    '        With oCurDoc.Application.Selection
    '            Dim cntcontrol As Wd.ContentControl = oCurDoc.Application.Selection.Range.ParentContentControl
    '            If Not IsNothing(cntcontrol) Then
    '                oCurDoc.Application.Selection.Move(Unit:=Wd.WdUnits.wdSection, Count:=1)
    '            End If
    '            ''Add the rich text Content control
    '            .Range.ContentControls.Add(Wd.WdContentControlType.wdContentControlRichText)
    '            ' .ParentContentControl.DefaultTextStyle = oCurDoc.Application.Selection.Style
    '            ''Title will be Field description
    '            .ParentContentControl.Title = m_Desc
    '            ''Elementid, Required flag  for reference stored in tag and temporary variables
    '            .ParentContentControl.Tag = m_ElementId & "|" & m_DataType & "|" & m_Required & "|" & m_Fieldcategory
    '            '  .ParentContentControl.Temporary = m_Required
    '            ''Get Table rows and column count based on the available items
    '            GetRCTable(m_FieldList.Count)
    '            ''for proper alignment and formatiing,  insert the items in a table
    '            'Dim t1Check As Wd.Table = .Tables.Add(Range:=oCurDoc.Application.Selection.Range, NumRows:=noRows, NumColumns:=noCols, DefaultTableBehavior:=objDefaultBehaviorWord8, AutoFitBehavior:=objAutoFitFixed)
    '            Dim t1Check As Wd.Table = .Tables.Add(oCurDoc.Application.Selection.Range, 1, noCols, objDefaultBehaviorWord8, objAutoFitFixed)

    '            If PopulateAndExtendTableCheckbox(t1Check) Then
    '                With t1Check
    '                    'If .Style <> "Table Grid" Then
    '                    .Style = "Table Grid"
    '                    'End If
    '                    .ApplyStyleHeadingRows = True
    '                    .ApplyStyleLastRow = False
    '                    .ApplyStyleFirstColumn = True
    '                    .ApplyStyleLastColumn = False
    '                    .ApplyStyleRowBands = True
    '                    .ApplyStyleColumnBands = False
    '                    .Borders(Wd.WdBorderType.wdBorderLeft).LineStyle = LineStyleSingle 'Wd.WdLineStyle.wdLineStyleNone
    '                    .Borders(Wd.WdBorderType.wdBorderRight).LineStyle = LineStyleSingle 'Wd.WdLineStyle.wdLineStyleNone
    '                    .Borders(Wd.WdBorderType.wdBorderTop).LineStyle = LineStyleSingle 'Wd.WdLineStyle.wdLineStyleNone
    '                    .Borders(Wd.WdBorderType.wdBorderBottom).LineStyle = LineStyleSingle 'Wd.WdLineStyle.wdLineStyleNone
    '                    .Borders(Wd.WdBorderType.wdBorderHorizontal).LineStyle = LineStyleSingle 'Wd.WdLineStyle.wdLineStyleNone
    '                    .Borders(Wd.WdBorderType.wdBorderVertical).LineStyle = LineStyleSingle 'Wd.WdLineStyle.wdLineStyleNone
    '                    .Borders(Wd.WdBorderType.wdBorderDiagonalDown).LineStyle = Wd.WdLineStyle.wdLineStyleNone
    '                    .Borders(Wd.WdBorderType.wdBorderDiagonalUp).LineStyle = Wd.WdLineStyle.wdLineStyleNone
    '                    .Borders.Shadow = False
    '                End With
    '            End If
    '            '    ''move cursor up and delete the space 
    '            '    .MoveUp(Unit:=Wd.WdUnits.wdLine, Count:=1)
    '            '    .Delete(Unit:=Wd.WdUnits.wdCharacter, Count:=1)
    '            '    ''Type the caption/Description of the Datafield
    '            '    .TypeText(Text:=m_Desc & " ")
    '            '    ''Place the cursor to other cell in the table
    '            '    .MoveRight(Wd.WdUnits.wdCell)
    '            '    Dim _Tabcnt As Int32 = 1

    '            '    ''Loop thourgh each entry in hash table and add as drop down items
    '            '    Dim i As Integer = 0
    '            '    For Each objEntry As DictionaryEntry In m_FieldList
    '            '        i = i + 1
    '            '        .TypeText(Text:=" ")
    '            '        Dim oNameField As Wd.FormField
    '            '        oNameField = .FormFields.Add(oCurDoc.Application.Selection.Range, Wd.WdFieldType.wdFieldFormCheckBox)
    '            '        oNameField.HelpText = objEntry.Key.ToString
    '            '        oNameField = Nothing
    '            '        '.Tables(i).Application.Selection.Text = objEntry.Value.ToString
    '            '        .TypeText(Text:=" " & objEntry.Value.ToString & " ")
    '            '        If i <> m_FieldList.Count Then
    '            '            .MoveRight(Wd.WdUnits.wdCell)
    '            '        End If
    '            '        _Tabcnt += 1
    '            '        Dim _rem As Int32 = 1
    '            '        Math.DivRem(_Tabcnt, 4, _rem)
    '            '        If _rem = 0 Then
    '            '            .MoveRight(Wd.WdUnits.wdCell)
    '            '        End If
    '            '        ' .TypeText(Text:=objEntry.Value.ToString)

    '            '    Next
    '            '    '.MoveDown(Unit:=Wd.WdUnits.wdLine, Count:=1)
    '            '    '.Delete(Unit:=Wd.WdUnits.wdCharacter, Count:=1)
    '            '    .MoveDown(Unit:=Wd.WdUnits.wdLine, Count:=1)
    '            '    .MoveRight(Unit:=Wd.WdUnits.wdCharacter, Count:=1)
    '        End With
    '        oCurDoc.ActiveWindow.SetFocus()
    '        '  oCurDoc.Application.Selection.Next()

    '    End If

    'End Sub

    Private Sub InsertCheckbox()
        If Not IsNothing(m_arraylist) Then
            '
            ''ADDED BY SHUBHANGI 200100625 TO RESOLVE 4485
            If oCurDoc.ContentControls.Count > 0 And oCurDoc.FormFields.Count > 0 Then

                oCurDoc.Application.Selection.Move(Unit:=Wd.WdUnits.wdSection, Count:=1)
                oCurDoc.Application.Selection.TypeParagraph()

            ElseIf ((oCurDoc.ContentControls.Count > 0) Or (oCurDoc.Tables.Count > 0)) Then
                oCurDoc.Application.Selection.Move(Unit:=Wd.WdUnits.wdSection, Count:=1)
            ElseIf (oCurDoc.FormFields.Count > 0) Then
                oCurDoc.Application.Selection.TypeParagraph()
            End If
            'END

            With oCurDoc.Application.Selection
                ''Add the rich text Content control
                .Range.ContentControls.Add(Wd.WdContentControlType.wdContentControlRichText)
                ' .ParentContentControl.DefaultTextStyle = oCurDoc.Application.Selection.Style
                ''Title will be Field description
                .ParentContentControl.Title = m_Desc
                ''Elementid, Required flag  for reference stored in tag and temporary variables
                .ParentContentControl.Tag = m_ElementId & "|" & m_DataType & "|" & m_Required & "|" & m_Fieldcategory

                If m_arraylist.Count > 1 Then
                    '  .ParentContentControl.Temporary = m_Required
                    ''Get Table rows and column count based on the available items
                    GetRCTable(m_arraylist.Count)
                    ''for proper alignment and formatiing,  insert the items in a table
                    'Dim t1Check As Wd.Table = .Tables.Add(Range:=oCurDoc.Application.Selection.Range, NumRows:=noRows, NumColumns:=noCols, DefaultTableBehavior:=objDefaultBehaviorWord8, AutoFitBehavior:=objAutoFitFixed)
                    Dim t1Check As Wd.Table = .Tables.Add(oCurDoc.Application.Selection.Range, 1, noCols, objDefaultBehaviorWord8, objAutoFitFixed)

                    PopulateAndExtendTableCheckbox(t1Check)
                    With t1Check
                        'If .Style <> "Table Grid" Then
                        .Style = "Table Grid"
                        'End If
                        .ApplyStyleHeadingRows = True
                        .ApplyStyleLastRow = False
                        .ApplyStyleFirstColumn = True
                        .ApplyStyleLastColumn = False
                        .ApplyStyleRowBands = True
                        .ApplyStyleColumnBands = False
                        .Borders(Wd.WdBorderType.wdBorderLeft).LineStyle = LineStyleSingle 'Wd.WdLineStyle.wdLineStyleNone
                        .Borders(Wd.WdBorderType.wdBorderRight).LineStyle = LineStyleSingle 'Wd.WdLineStyle.wdLineStyleNone
                        .Borders(Wd.WdBorderType.wdBorderTop).LineStyle = LineStyleSingle 'Wd.WdLineStyle.wdLineStyleNone
                        .Borders(Wd.WdBorderType.wdBorderBottom).LineStyle = LineStyleSingle 'Wd.WdLineStyle.wdLineStyleNone
                        .Borders(Wd.WdBorderType.wdBorderHorizontal).LineStyle = LineStyleSingle 'Wd.WdLineStyle.wdLineStyleNone
                        .Borders(Wd.WdBorderType.wdBorderVertical).LineStyle = LineStyleSingle 'Wd.WdLineStyle.wdLineStyleNone
                        .Borders(Wd.WdBorderType.wdBorderDiagonalDown).LineStyle = Wd.WdLineStyle.wdLineStyleNone
                        .Borders(Wd.WdBorderType.wdBorderDiagonalUp).LineStyle = Wd.WdLineStyle.wdLineStyleNone
                        .Borders.Shadow = False
                    End With
                Else
                    For Each objEntry As myList In m_arraylist
                        Dim oNameField As Wd.FormField
                        oNameField = .FormFields.Add(.Range, Wd.WdFieldType.wdFieldFormCheckBox)
                        oNameField.StatusText = objEntry.HistoryItem.ToString
                        oNameField.HelpText = objEntry.ID.ToString & "|" & objEntry.AssociatedProperty.ToString
                        .Text = objEntry.HistoryItem.ToString
                        oNameField.CheckBox.Value = False
                        oNameField = Nothing
                    Next
                    oCurDoc.Application.Selection.MoveDown(Unit:=Wd.WdUnits.wdLine, Count:=1)
                    oCurDoc.Application.Selection.MoveRight(Unit:=Wd.WdUnits.wdCharacter, Count:=1)
                    oCurDoc.Application.Selection.InsertParagraph()
                    oCurDoc.Application.Selection.MoveRight(Unit:=Wd.WdUnits.wdCharacter, Count:=1)
                    '.Move(Wd.WdUnits.wdLine, Count:=1)
                End If

            End With
            oCurDoc.ActiveWindow.SetFocus()
            '  oCurDoc.Application.Selection.Next()

        End If

    End Sub

    Private Sub InsertCheckboxInBooleanFormat()
        If Not IsNothing(m_arraylist) Then

            If Not IsNothing(oCurDoc) Then

                ''ADDED BY SHUBHANGI 200100625 TO RESOLVE 4485
                If oCurDoc.ContentControls.Count > 0 And oCurDoc.FormFields.Count > 0 Then

                    oCurDoc.Application.Selection.Move(Unit:=Wd.WdUnits.wdSection, Count:=1)
                    oCurDoc.Application.Selection.TypeParagraph()

                ElseIf (oCurDoc.ContentControls.Count > 0) Then
                    oCurDoc.Application.Selection.Move(Unit:=Wd.WdUnits.wdSection, Count:=1)
                    oCurDoc.Application.Selection.TypeParagraph()
                Else 'If (oCurDoc.FormFields.Count > 0) Then
                    oCurDoc.Application.Selection.Move(Unit:=Wd.WdUnits.wdSection, Count:=1)
                    oCurDoc.Application.Selection.TypeParagraph()

                End If
                ''
            End If

            With oCurDoc.Application.Selection
                Dim cntcontrol As Wd.ContentControl = oCurDoc.Application.Selection.Range.ParentContentControl
                If Not IsNothing(cntcontrol) Then
                    oCurDoc.Application.Selection.Move(Unit:=Wd.WdUnits.wdSection, Count:=1)
                End If

                .Range.ContentControls.Add(Wd.WdContentControlType.wdContentControlRichText)

                ''Title will be Field description
                .ParentContentControl.Title = m_Desc

                ''Elementid, Required flag  for reference stored in tag and temporary variables
                .ParentContentControl.Tag = m_ElementId & "|" & m_DataType & "|" & m_Required & "|" & m_Fieldcategory

                If m_arraylist.Count > 1 Or m_DataType = "Multiple Selection" Then
                    '  .ParentContentControl.Temporary = m_Required
                    ''Get Table rows and column count based on the available items
                    'GetRCTable(m_arraylist.Count)
                    noCols = 3
                    ''for proper alignment and formatiing,  insert the items in a table
                    'Dim t1Check As Wd.Table = .Tables.Add(Range:=oCurDoc.Application.Selection.Range, NumRows:=noRows, NumColumns:=noCols, DefaultTableBehavior:=objDefaultBehaviorWord8, AutoFitBehavior:=objAutoFitFixed)
                    Dim t1Check As Wd.Table = .Tables.Add(oCurDoc.Application.Selection.Range, 1, noCols, objDefaultBehaviorWord8, objAutoFitFixed)

                    PopulateAndExtendTableInBooleanFormat(t1Check)
                    With t1Check
                        'If .Style <> "Table Grid" Then
                        .Style = "Table Grid"
                        'End If
                        .ApplyStyleHeadingRows = True
                        .ApplyStyleLastRow = False
                        .ApplyStyleFirstColumn = True
                        .ApplyStyleLastColumn = False
                        .ApplyStyleRowBands = True
                        .ApplyStyleColumnBands = False
                        .Borders(Wd.WdBorderType.wdBorderLeft).LineStyle = LineStyleSingle 'Wd.WdLineStyle.wdLineStyleNone
                        .Borders(Wd.WdBorderType.wdBorderRight).LineStyle = LineStyleSingle 'Wd.WdLineStyle.wdLineStyleNone
                        .Borders(Wd.WdBorderType.wdBorderTop).LineStyle = LineStyleSingle 'Wd.WdLineStyle.wdLineStyleNone
                        .Borders(Wd.WdBorderType.wdBorderBottom).LineStyle = LineStyleSingle 'Wd.WdLineStyle.wdLineStyleNone
                        .Borders(Wd.WdBorderType.wdBorderHorizontal).LineStyle = LineStyleSingle 'Wd.WdLineStyle.wdLineStyleNone
                        .Borders(Wd.WdBorderType.wdBorderVertical).LineStyle = LineStyleSingle 'Wd.WdLineStyle.wdLineStyleNone
                        .Borders(Wd.WdBorderType.wdBorderDiagonalDown).LineStyle = Wd.WdLineStyle.wdLineStyleNone
                        .Borders(Wd.WdBorderType.wdBorderDiagonalUp).LineStyle = Wd.WdLineStyle.wdLineStyleNone
                        .Borders.Shadow = False
                    End With
                Else
                    For Each objEntry As myList In m_arraylist
                        Dim oNameField As Wd.FormField
                        oNameField = .FormFields.Add(.Range, Wd.WdFieldType.wdFieldFormCheckBox)
                        oNameField.StatusText = objEntry.HistoryItem.ToString
                        oNameField.HelpText = objEntry.ID.ToString & "|" & objEntry.AssociatedProperty.ToString
                        .Text = objEntry.HistoryItem.ToString
                        oNameField.CheckBox.Value = False
                        oNameField = Nothing
                    Next
                    .Move(Wd.WdUnits.wdLine, Count:=1)
                End If

            End With
            oCurDoc.ActiveWindow.SetFocus()
            '  oCurDoc.Application.Selection.Next()
            'End If
        End If

    End Sub

    ''' <summary>
    ''' Insert Text Content control as liquid datafield control
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InsertTextControl()
        Try
            With oCurDoc.Application.Selection
                Dim cntcontrol As Wd.ContentControl = oCurDoc.Application.Selection.Range.ParentContentControl
                If Not IsNothing(cntcontrol) Then
                    oCurDoc.Application.Selection.Move(Unit:=Wd.WdUnits.wdSection, Count:=1)
                End If
                ''Type the caption for the Text Datafield
                .TypeText(Text:=m_Desc & ": ")
                .Range.ContentControls.Add(Wd.WdContentControlType.wdContentControlRichText)


                .ParentContentControl.Type = Microsoft.Office.Interop.Word.WdContentControlType.wdContentControlRichText

                '.Range.ContentControls(1).DefaultTextStyle = oCurDoc.Application.Selection.Style
                ''Title will be Field description
                .ParentContentControl.Title = m_Desc
                ''Elementid, Required flag  for reference stored in tag and temporary variables
                .ParentContentControl.Tag = m_ElementId & "|" & m_DataType & "|" & m_Required & "|" & m_Fieldcategory
                ' .ParentContentControl.Temporary = m_Required

                If m_TextValue <> "" Then
                    ''Type the text int he Richtext control
                    .TypeText(Text:=m_TextValue)
                End If

                ''move cursor out of the rich text content control
                .MoveRight(Unit:=Wd.WdUnits.wdCharacter, Count:=1)
                .InsertParagraph()
                .MoveRight(Unit:=Wd.WdUnits.wdCharacter, Count:=1)
            End With
            oCurDoc.ActiveWindow.SetFocus()

            'ADDED BY SHUBHANGI 20100625 TO HANDLE COM EXCEPTION 
            ''DONT CHANGE THE ex.Message  TO ex.ToString FOR THIS CASE ONLY
        Catch ex As COMException
            If ex.Message = "This command is not available." Then
                MessageBox.Show("Current operation is invalid for adding the required data fields", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Else
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            If oCurDoc.Application.Selection.Range.ParentContentControl.Type = Microsoft.Office.Interop.Word.WdContentControlType.wdContentControlRichText Then
                oCurDoc.Application.Selection.Range.ParentContentControl.Delete(True)
            End If
        End Try
    End Sub

    Private Sub InsertTable()
        'Dim WDocViewType As Wd.WdViewType
        'Dim wordRefresh As New WordRefresh()
        Try
            If Not IsNothing(m_arraylist) Then
                'ADDED BY SHUBHANGI 20100625

                'Ashish added on 1st October 
                'to prevent screen from refreshing               
                'WDocViewType = oCurDoc.ActiveWindow.View.Type
                'wordRefresh.OptimizePerformance(False, oCurDoc, 0)

                If oCurDoc.ContentControls.Count > 0 And oCurDoc.FormFields.Count > 0 Then

                    oCurDoc.Application.Selection.Move(Unit:=Wd.WdUnits.wdSection, Count:=1)
                    oCurDoc.Application.Selection.TypeParagraph()

                ElseIf (oCurDoc.ContentControls.Count > 0) Then
                    oCurDoc.Application.Selection.Move(Unit:=Wd.WdUnits.wdSection, Count:=1)
                ElseIf (oCurDoc.FormFields.Count > 0) Then
                    oCurDoc.Application.Selection.TypeParagraph()
                End If
                'END
                If m_arraylist.Count > 0 Then
                    With oCurDoc.Application.Selection

                        Dim cntcontrol As Wd.ContentControl = oCurDoc.Application.Selection.Range.ParentContentControl
                        If Not IsNothing(cntcontrol) Then
                            oCurDoc.Application.Selection.Move(Unit:=Wd.WdUnits.wdSection, Count:=1)
                        End If


                        'oCurDoc.ActiveWindow.View.Type = Microsoft.Office.Interop.Word.WdViewType.wdNormalView

                        ''''Create Basic Table
                        Dim nrRows As Integer = 1
                        Dim nrCols As Integer = 2
                        ''Add the rich text Content control
                        oCurDoc.Application.Selection.Range.ContentControls.Add(Wd.WdContentControlType.wdContentControlRichText)
                        ' .ParentContentControl.DefaultTextStyle = oCurDoc.Application.Selection.Style
                        ''Title will be Field description
                        oCurDoc.Application.Selection.ParentContentControl.Title = m_Desc

                        ''oCurDoc.Application.Selection.ParentContentControl.Temporary

                        ''Elementid, Required flag  for reference stored in tag and temporary variables
                        oCurDoc.Application.Selection.ParentContentControl.Tag = m_ElementId & "|" & m_DataType & "|" & m_Required & "|" & m_Fieldcategory
                        Dim wdRng As Wd.Range = oCurDoc.Application.Selection.Range
                        CreateIntro(wdRng, m_Desc)
                        '.Tables.Add(Range:=oCurDoc.Application.Selection.Range, NumRows:=noRows, NumColumns:=noCols, DefaultTableBehavior:=Wd.WdDefaultTableBehavior.wdWord9TableBehavior, AutoFitBehavior:=Wd.WdAutoFitBehavior.wdAutoFitFixed)
                        Dim tb1 As Wd.Table = wdRng.Tables.Add(wdRng, nrRows, nrCols, objDefaultBehaviorWord8, objAutoFitFixed)
                        If PopulateAndExtendTable(tb1) Then
                            'wdRng = CreateSpaceAfterTable(tb1)
                            Dim style As Wd.Style = CreateTableStyle()
                            FormatTables(style, tb1)
                        End If
                    End With
                    oCurDoc.ActiveWindow.SetFocus()
                End If
            End If
            'ADDED BY SHUBHANGI 20100625 TO HANDLE COM EXCEPTION 
            ''DONT CHANGE THE ex.Message  TO ex.ToString FOR THIS CASE ONLY
        Catch ex As COMException
            If ex.Message = "This command is not available." Then
                MessageBox.Show("Current operation is invalid for adding the required data fields", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Else
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            oCurDoc.Application.Selection.InsertParagraph()
            oCurDoc.Application.Selection.MoveRight(Unit:=Wd.WdUnits.wdCharacter, Count:=1)
        Finally
            'Ashish added on 31st October 
            'to prevent screen from refreshing
            'wordRefresh.OptimizePerformance(True, oCurDoc, WDocViewType)
            'wordRefresh.Dispose()
            'wordRefresh = Nothing

            'WDocViewType = Nothing
        End Try
    End Sub
    Private Sub InsertGroup()
        Try
            If Not IsNothing(m_arraylist) Then
                'ADDED BY SHUBHANGI 20100625
                If oCurDoc.ContentControls.Count > 0 And oCurDoc.FormFields.Count > 0 Then

                    oCurDoc.Application.Selection.Move(Unit:=Wd.WdUnits.wdSection, Count:=1)
                    oCurDoc.Application.Selection.TypeParagraph()

                ElseIf (oCurDoc.ContentControls.Count > 0) Then
                    oCurDoc.Application.Selection.Move(Unit:=Wd.WdUnits.wdSection, Count:=1)
                ElseIf (oCurDoc.FormFields.Count > 0) Then
                    oCurDoc.Application.Selection.TypeParagraph()
                End If
                'END
                If m_arraylist.Count > 0 Then
                    With oCurDoc.Application.Selection

                        Dim cntcontrol As Wd.ContentControl = oCurDoc.Application.Selection.Range.ParentContentControl
                        If Not IsNothing(cntcontrol) Then
                            oCurDoc.Application.Selection.Move(Unit:=Wd.WdUnits.wdSection, Count:=1)
                        Else
                            oCurDoc.Application.Selection.Move(Unit:=Wd.WdUnits.wdCharacter, Count:=1)
                        End If
                        'oCurDoc.ActiveWindow.View.Type = Microsoft.Office.Interop.Word.WdViewType.wdNormalView

                        ''''Create Basic Table
                        Dim nrRows As Integer = 1
                        Dim nrCols As Integer = 2
                        ''Add the rich text Content control
                        oCurDoc.Application.Selection.Range.ContentControls.Add(Wd.WdContentControlType.wdContentControlRichText)
                        ' .ParentContentControl.DefaultTextStyle = oCurDoc.Application.Selection.Style
                        ''Title will be Field description
                        oCurDoc.Application.Selection.ParentContentControl.Title = m_Desc

                        ''oCurDoc.Application.Selection.ParentContentControl.Temporary

                        ''Elementid, Required flag  for reference stored in tag and temporary variables
                        oCurDoc.Application.Selection.ParentContentControl.Tag = m_ElementId & "|" & m_DataType & "|" & m_Required & "|" & m_Fieldcategory
                        Dim wdRng As Wd.Range = oCurDoc.Application.Selection.Range
                        CreateIntro(wdRng, m_Desc)
                        '.Tables.Add(Range:=oCurDoc.Application.Selection.Range, NumRows:=noRows, NumColumns:=noCols, DefaultTableBehavior:=Wd.WdDefaultTableBehavior.wdWord9TableBehavior, AutoFitBehavior:=Wd.WdAutoFitBehavior.wdAutoFitFixed)
                        Dim tb1 As Wd.Table = wdRng.Tables.Add(wdRng, nrRows, nrCols, objDefaultBehaviorWord8, objAutoFitFixed)
                        If PopulateAndExtendTableNew(tb1) Then
                            'wdRng = CreateSpaceAfterTable(tb1)
                            Dim style As Wd.Style = CreateTableStyle()
                            FormatTables(style, tb1)
                        End If
                    End With
                    oCurDoc.ActiveWindow.SetFocus()
                End If
            End If
            'ADDED BY SHUBHANGI 20100625 TO HANDLE COM EXCEPTION 
            ''DONT CHANGE THE ex.Message  TO ex.ToString FOR THIS CASE ONLY
        Catch ex As COMException
            If ex.Message = "This command is not available." Then
                MessageBox.Show("Current operation is invalid for adding the required data fields", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Else
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            oCurDoc.Application.Selection.InsertParagraph()
            oCurDoc.Application.Selection.MoveRight(Unit:=Wd.WdUnits.wdCharacter, Count:=1)
        End Try
    End Sub
    Public Sub CreateIntro(ByVal wdRange As Wd.Range, ByVal HeaderText As String)
        Try
            wdRange.Text = HeaderText & vbNewLine

            wdRange.Style = objHeadingStyle1
            wdRange.Collapse(objCollapseEnd)
            wdRange.Style = objNormalStyle
            'wdRange.InsertParagraph()
            wdRange.Collapse(objCollapseEnd)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Public Function PopulateAndExtendTableCheckbox(ByVal T1ChkBox As Wd.Table) As Boolean
        Try

            '''''Move Cursor to the Table 
            oCurDoc.Application.Selection.GoToNext(Microsoft.Office.Interop.Word.WdGoToItem.wdGoToTable)

            Dim i As Integer = 1
            'SLR: is '0' ok?
            T1ChkBox.Cell(0, 0).Application.Selection.Text = m_Desc
            oCurDoc.Application.Selection.MoveRight()
            Dim Reminder As Integer = 0
            Dim Result As Integer = 0
            Dim CurrnetRow As Integer = 0
            For Each objEntry As myList In m_arraylist
                i = i + 1
                Reminder = 0
                System.Math.DivRem(i, 5, Reminder)  ''Math Rem Rem(i, 4, Result)
                If Reminder <> 0 Then

                    If CurrnetRow <> 0 Then
                        oCurDoc.Application.Selection.Move(Wd.WdUnits.wdRow)
                        oCurDoc.Application.Selection.MoveRight()
                    Else
                        oCurDoc.Application.Selection.Move(Wd.WdUnits.wdColumn)
                    End If
                    Dim oNameField As Wd.FormField
                    If (CurrnetRow <= T1ChkBox.Rows.Count) And (i <= T1ChkBox.Columns.Count) Then


                        T1ChkBox.Cell(CurrnetRow, i).Application.Selection.Select()

                        If CType(objEntry.ControlType, ControlType) = ControlType.CheckBox Or CType(objEntry.ControlType, ControlType) = ControlType.None Then
                            oNameField = T1ChkBox.Cell(CurrnetRow, i).Application.Selection.FormFields.Add(T1ChkBox.Cell(CurrnetRow, i).Application.Selection.Range, Wd.WdFieldType.wdFieldFormCheckBox)
                            oNameField.StatusText = objEntry.HistoryItem.ToString
                            oNameField.HelpText = objEntry.ID.ToString & "|" & objEntry.AssociatedProperty.ToString
                            T1ChkBox.Cell(CurrnetRow, i).Application.Selection.Text = objEntry.HistoryItem.ToString
                            oNameField.CheckBox.Value = False
                            oNameField = Nothing
                        ElseIf CType(objEntry.ControlType, ControlType) = ControlType.Text Then
                            oControl = T1ChkBox.Cell(CurrnetRow, i).Application.Selection.Range.ContentControls.Add(Wd.WdContentControlType.wdContentControlRichText)
                            T1ChkBox.Cell(CurrnetRow, i).Application.Selection.Range.ParentContentControl.Type = Microsoft.Office.Interop.Word.WdContentControlType.wdContentControlRichText
                            ''Title will be Field description
                            'tb1.Cell(nrRows, 2).Application.Selection.Range.ParentContentControl.Title = m_Desc
                            ''Elementid, Required flag  for reference stored in tag and temporary variables
                            T1ChkBox.Cell(CurrnetRow, i).Application.Selection.Range.ParentContentControl.Tag = objEntry.ID.ToString() & "|" & objEntry.HistoryItem.ToString()
                            T1ChkBox.Cell(CurrnetRow, i).Application.Selection.TypeText(Text:=objEntry.HistoryItem.ToString)

                        End If
                    End If
                Else
                    CurrnetRow = CurrnetRow + 1
                    T1ChkBox.Rows.Add(objMissing)

                    oCurDoc.Application.Selection.Move(Wd.WdUnits.wdRow)
                    oCurDoc.Application.Selection.MoveRight()
                    Dim oNameField As Wd.FormField
                    i = 2
                    If (CurrnetRow <= T1ChkBox.Rows.Count) And (i <= T1ChkBox.Columns.Count) Then
                        T1ChkBox.Cell(CurrnetRow, i).Application.Selection.Select()

                        If CType(objEntry.ControlType, ControlType) = ControlType.CheckBox Or CType(objEntry.ControlType, ControlType) = ControlType.None Then
                            oNameField = T1ChkBox.Cell(CurrnetRow, i).Application.Selection.FormFields.Add(T1ChkBox.Cell(CurrnetRow, i).Application.Selection.Range, Wd.WdFieldType.wdFieldFormCheckBox)

                            oNameField.StatusText = objEntry.HistoryItem.ToString
                            oNameField.HelpText = objEntry.ID.ToString & "|" & objEntry.AssociatedProperty.ToString
                            T1ChkBox.Cell(CurrnetRow, i).Application.Selection.Text = objEntry.HistoryItem.ToString
                            'oCurDoc.Application.Selection.MoveDown(Wd.WdUnits.wdLine)
                            oNameField.CheckBox.Value = False
                            oNameField = Nothing
                        ElseIf CType(objEntry.ControlType, ControlType) = ControlType.Text Then
                            oControl = T1ChkBox.Cell(CurrnetRow, i).Application.Selection.Range.ContentControls.Add(Wd.WdContentControlType.wdContentControlRichText)
                            T1ChkBox.Cell(CurrnetRow, i).Application.Selection.Range.ParentContentControl.Type = Microsoft.Office.Interop.Word.WdContentControlType.wdContentControlRichText
                            ''Title will be Field description
                            'tb1.Cell(nrRows, 2).Application.Selection.Range.ParentContentControl.Title = m_Desc
                            ''Elementid, Required flag  for reference stored in tag and temporary variables
                            'tb1.Cell(nrRows, 2).Application.Selection.Range.ParentContentControl.Tag = m_ElementId & "|" & m_DataType & "|" & m_Required & "|" & m_Fieldcategory
                            T1ChkBox.Cell(CurrnetRow, i).Application.Selection.Range.ParentContentControl.Tag = objEntry.ID.ToString() & "|" & objEntry.HistoryItem.ToString()

                            T1ChkBox.Cell(CurrnetRow, i).Application.Selection.TypeText(Text:=objEntry.HistoryItem.ToString)

                        End If


                    End If


                End If
            Next

            oCurDoc.Application.Selection.MoveDown(Unit:=Wd.WdUnits.wdLine, Count:=1)
            oCurDoc.Application.Selection.MoveRight(Unit:=Wd.WdUnits.wdCharacter, Count:=1)
            oCurDoc.Application.Selection.InsertParagraph()
            oCurDoc.Application.Selection.MoveRight(Unit:=Wd.WdUnits.wdCharacter, Count:=1)
            Return True
            'oCurDoc.Application.Selection.MoveRight()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            If oCurDoc.Application.Selection.Range.ParentContentControl.Type = Microsoft.Office.Interop.Word.WdContentControlType.wdContentControlRichText Then
                oCurDoc.Application.Selection.Range.ParentContentControl.Delete(True)
            End If
            Return False
        End Try
    End Function
    Dim t_AssoicaitedProperty As String
    Public Function PopulateAndExtendTableInBooleanFormat(ByVal tb1 As Wd.Table) As Boolean
        Try

            Dim nrCols As Integer = 3
            Dim nrRows As Integer = 1
            If (tb1.Rows.Count >= 1) And (tb1.Columns.Count) Then
                tb1.Cell(1, 1).Range.Text = m_Desc
            End If

            If oCurDoc.ContentControls.Count <> 0 Then

                '''''Move Cursor to the Table 
                oCurDoc.Application.Selection.GoToNext(Microsoft.Office.Interop.Word.WdGoToItem.wdGoToTable)
            End If
            ''''''Move Cursor down in the Table
            '   Dim t_list As myList

            Dim t_itemName As String
            Dim t_ElementID As String

            ' Dim ocontrolLength As Integer
            Dim t_Control_Type As ControlType



            'Dim t_AssoicaitedProperty As String
            oCurDoc.Application.Selection.Move(Wd.WdUnits.wdColumn, Count:=2)
            For i As Integer = 0 To m_arraylist.Count - 1


                't_categoryName = CType(m_arraylist.Item(i), myList).HistoryCategory.ToString

                t_itemName = CType(m_arraylist.Item(i), myList).HistoryItem.ToString
                t_ElementID = CType(m_arraylist.Item(i), myList).ID.ToString
                t_Control_Type = CType(m_arraylist.Item(i), myList).ControlType
                t_AssoicaitedProperty = CType(m_arraylist.Item(i), myList).AssociatedProperty

                tb1.Rows.Add(objMissing)  '''' new Row

                oCurDoc.Application.Selection.Move(Wd.WdUnits.wdRow) '''' Move Cursor to Newly Added Row in table

                'oCurDoc.Application.Selection.MoveRight()
                nrRows = nrRows + 1
                '''' Add Catergory in New Row and category Column

                '''' Add Item for Selected category 


                Dim oNameField As Wd.FormField
                If (nrRows <= tb1.Rows.Count) And (tb1.Columns.Count >= 1) Then
                    tb1.Cell(nrRows, 1).Application.Selection.Select()
                End If

                oCurDoc.Application.Selection.SelectCell()
                If t_Control_Type = ControlType.CheckBox Then
                    If (nrRows <= tb1.Rows.Count) And (tb1.Columns.Count >= 1) Then
                        tb1.Cell(nrRows, 1).Application.Selection.Text = t_itemName
                    End If

                    oCurDoc.Application.Selection.Move(Wd.WdUnits.wdCell, Count:=1)

                    oCurDoc.Application.Selection.Select()
                    If (nrRows <= tb1.Rows.Count) And (tb1.Columns.Count >= 2) Then


                        oNameField = tb1.Cell(nrRows, 2).Application.Selection.FormFields.Add(tb1.Cell(nrRows, 2).Application.Selection.Range, Wd.WdFieldType.wdFieldFormCheckBox)
                        oNameField.HelpText = t_ElementID & "|" & t_AssoicaitedProperty  ''t_ElementID 'Convert.ToString(t_ElementID) 
                        oNameField.StatusText = t_itemName
                        tb1.Cell(nrRows, 2).Application.Selection.Text = " Yes"
                    End If
                    oCurDoc.Application.Selection.Move(Wd.WdUnits.wdCell, Count:=1)
                    oCurDoc.Application.Selection.Select()
                    If (nrRows <= tb1.Rows.Count) And (tb1.Columns.Count >= 2) Then


                        oNameField = tb1.Cell(nrRows, 3).Application.Selection.FormFields.Add(tb1.Cell(nrRows, 3).Application.Selection.Range, Wd.WdFieldType.wdFieldFormCheckBox)
                        oNameField.StatusText = t_itemName
                        oNameField.HelpText = t_ElementID & "|" & t_AssoicaitedProperty  ''t_ElementID 'Convert.ToString(t_ElementID) 
                        tb1.Cell(nrRows, 3).Application.Selection.Text = " No"
                    End If

                ElseIf t_Control_Type = ControlType.Text Then
                    If (nrRows <= tb1.Rows.Count) And (tb1.Columns.Count >= 1) Then
                        tb1.Cell(nrRows, 1).Application.Selection.Text = t_itemName
                    End If

                    oCurDoc.Application.Selection.Move(Wd.WdUnits.wdCell, Count:=1)

                    oCurDoc.Application.Selection.Select()
                    If (nrRows <= tb1.Rows.Count) And (tb1.Columns.Count >= 2) Then


                        oControl = tb1.Cell(nrRows, 2).Application.Selection.Range.ContentControls.Add(Wd.WdContentControlType.wdContentControlRichText)
                        oControl.Range.Application.Selection.TypeText(Text:="[]")
                        tb1.Cell(nrRows, 2).Application.Selection.Range.ParentContentControl.Type = Microsoft.Office.Interop.Word.WdContentControlType.wdContentControlRichText
                        ''Title will be Field description
                        'tb1.Cell(nrRows, 2).Application.Selection.Range.ParentContentControl.Title = m_Desc
                        ''Elementid, Required flag  for reference stored in tag and temporary variables
                        'tb1.Cell(nrRows, 2).Application.Selection.Range.ParentContentControl.Tag = m_ElementId & "|" & m_DataType & "|" & m_Required & "|" & m_Fieldcategory
                        GetShortNameMulti()
                        tb1.Cell(nrRows, 2).Application.Selection.Range.ParentContentControl.Tag = m_ElementId & "|" & m_DataType & "|" & m_Required & "|" & m_Fieldcategory & "|" & t_AssoicaitedProperty
                        'tb1.Cell(nrRows, 2).Application.Selection.TypeText(Text:=t_itemName)
                    End If

                Else
                    If (nrRows <= tb1.Rows.Count) And (tb1.Columns.Count >= 1) Then
                        tb1.Cell(nrRows, 1).Application.Selection.Text = t_itemName
                    End If

                    oCurDoc.Application.Selection.Move(Wd.WdUnits.wdCell, Count:=1)

                    oCurDoc.Application.Selection.Select()
                    If (nrRows <= tb1.Rows.Count) And (tb1.Columns.Count >= 2) Then
                        oNameField = tb1.Cell(nrRows, 2).Application.Selection.FormFields.Add(tb1.Cell(nrRows, 2).Application.Selection.Range, Wd.WdFieldType.wdFieldFormCheckBox)
                        oNameField.HelpText = t_ElementID & "|" & t_AssoicaitedProperty  ''t_ElementID 'Convert.ToString(t_ElementID) 
                        oNameField.StatusText = t_itemName
                        tb1.Cell(nrRows, 2).Application.Selection.Text = " Yes"
                    End If

                    oCurDoc.Application.Selection.Move(Wd.WdUnits.wdCell, Count:=1)
                    oCurDoc.Application.Selection.Select()
                    If (nrRows <= tb1.Rows.Count) And (tb1.Columns.Count >= 3) Then
                        oNameField = tb1.Cell(nrRows, 3).Application.Selection.FormFields.Add(tb1.Cell(nrRows, 3).Application.Selection.Range, Wd.WdFieldType.wdFieldFormCheckBox)
                        oNameField.HelpText = t_ElementID & "|" & t_AssoicaitedProperty  ''t_ElementID 'Convert.ToString(t_ElementID) 
                        oNameField.StatusText = t_itemName
                        tb1.Cell(nrRows, 3).Application.Selection.Text = " No"
                    End If


                End If

            Next

            '''''Move Cursor down in the Table
            oCurDoc.Application.Selection.MoveDown(Unit:=Wd.WdUnits.wdLine, Count:=1)
            oCurDoc.Application.Selection.MoveRight(Unit:=Wd.WdUnits.wdCharacter, Count:=1)
            oCurDoc.Application.Selection.InsertParagraph()
            oCurDoc.Application.Selection.MoveRight(Unit:=Wd.WdUnits.wdCharacter, Count:=1)

            'For i As Integer = 0 To tb1.Columns.Count - 1
            '    tb1.Columns(i + 1).AutoFit()
            'Next

            Return True
            'oCurDoc.Application.Selection.MoveRight()

            'ADDED BY SHUBHANGI 20100625 TO HANDLE COM EXCEPTION 
            ''DONT CHANGE THE ex.Message  TO ex.ToString FOR THIS CASE ONLY
        Catch ex As COMException

            If ex.Message = "This command is not available." Then
                MessageBox.Show("Current operation is invalid for adding the required data fields", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                oCurDoc.Application.Selection.Range.ParentContentControl.Delete(True)
                Return False
            Else
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            If oCurDoc.Application.Selection.Range.ParentContentControl.Type = Microsoft.Office.Interop.Word.WdContentControlType.wdContentControlRichText Then
                oCurDoc.Application.Selection.Range.ParentContentControl.Delete(True)
            End If
            Return False
        End Try
    End Function
    Private Sub GetShortNameMulti()
        Select Case t_AssoicaitedProperty

            Case "RespiratoryTreatments"
                t_AssoicaitedProperty = MagRespiratoryTreatments
            Case "MinorSurgeryWRiskFactors"
                t_AssoicaitedProperty = MagMinorSurgeryWRiskFactors
            Case "MajorSurgeryWRiskFactors"
                t_AssoicaitedProperty = MagMajorSurgeryWRiskFactors
            Case "MajorEmergencySurgery"
                t_AssoicaitedProperty = MagMajorEmergencySurgery
            Case "DecisionNotResuscitate"
                t_AssoicaitedProperty = MagDecisionNotResuscitate
            Case "DecisionObtainMedicalRecsOther"
                t_AssoicaitedProperty = MagDecisionObtainMedicalRecsOther
            Case "ReviewMedicalRecsOther"
                t_AssoicaitedProperty = MagReviewMedicalRecsOther
            Case "DiscussCaseWHealthProvider"
                t_AssoicaitedProperty = MagDiscussCaseWHealthProvider
            Case "FluStrepMonoRoutine"
                t_AssoicaitedProperty = LabFluStrepMonoRoutine
            Case "PregnancyTestRoutine"
                t_AssoicaitedProperty = LabPregnancyTestRoutine
            Case "BunCreatinineRoutine"
                t_AssoicaitedProperty = LabBunCreatinineRoutine
            Case "ElectrolytesRoutine"
                t_AssoicaitedProperty = LabElectrolytesRoutine
            Case "ChemicalProfileRoutine"
                t_AssoicaitedProperty = LabChemicalProfileRoutine
            Case "CardiacEnzymesRoutine"
                t_AssoicaitedProperty = LabCardiacEnzymesRoutine
            Case "TypeCrossmatchRoutine"
                t_AssoicaitedProperty = LabTypeCrossmatchRoutine
            Case "SuperficialBiopsyRoutine"
                t_AssoicaitedProperty = LabSuperficialBiopsyRoutine
            Case "IncisionalBiopsyRoutine"
                t_AssoicaitedProperty = LabIncisionalBiopsyRoutine
            Case "IndependentVisualTest"
                t_AssoicaitedProperty = LabIndependentVisualTest
            Case "DiscussionWPerformingPhys"
                t_AssoicaitedProperty = LabDiscussionWPerformingPhys
            Case "DiagUltrasoundRoutine"
                t_AssoicaitedProperty = XDiagUltrasoundRoutine
            Case "GIGallbladderRoutine"
                t_AssoicaitedProperty = XGIGallbladderRoutine
            Case "VascularStudiesRoutine"
                t_AssoicaitedProperty = XVascularStudiesRoutine
            Case "VascularStudiesWRiskRoutine"
                t_AssoicaitedProperty = XVascularStudiesWRiskRoutine
            Case "IndependentVisualTest"
                t_AssoicaitedProperty = XIndependentVisualTest
            Case "DiscussWPerformingPhys"
                t_AssoicaitedProperty = XDiscussWPerformingPhys
            Case "HolterMonitorRoutine"
                t_AssoicaitedProperty = OHolterMonitorRoutine
            Case "TreadmillStressTestRoutine"
                t_AssoicaitedProperty = OTreadmillStressTestRoutine
            Case "VectorcardiogramRoutine"
                t_AssoicaitedProperty = OVectorcardiogramRoutine
            Case "DopplerFlowStudiesRoutine"
                t_AssoicaitedProperty = ODopplerFlowStudiesRoutine
            Case "PulmonaryStudiesRoutine"
                t_AssoicaitedProperty = OPulmonaryStudiesRoutine
            Case "LumbarPunctureRoutine"
                t_AssoicaitedProperty = OLumbarPunctureRoutine
            Case "ThoracentesisRoutine"
                t_AssoicaitedProperty = OThoracentesisRoutine
            Case "CuldocentesesRoutine"
                t_AssoicaitedProperty = OCuldocentesesRoutine
            Case "EndoscopeWRiskRoutine"
                t_AssoicaitedProperty = OEndoscopeWRiskRoutine
            Case "IndependentVisualTest"
                t_AssoicaitedProperty = OIndependentVisualTest
            Case "DiscussWPerformingPhys"
                t_AssoicaitedProperty = ODiscussWPerformingPhys
        End Select
    End Sub
    Public Sub GetShortName(ByVal strCategoryName As String)

        Select Case strCategoryName
            Case "Lymphatic *requires palpation of two or more nodes per area"
                t_categoryName = EMLymphatic
            Case "Musculoskeletal-Spine, Ribs and Pelvis"
                t_categoryName = EMMusculoskeletalSpine
            Case "Musculoskeletal-Right upper extremity"
                t_categoryName = EMMusculoskeletalupperR
            Case "Musculoskeletal-Left upper extremity"
                t_categoryName = EMMusculoskeletalupperL
            Case "Musculoskeletal-Right lower extremity"
                t_categoryName = EMMusculoskeletallowerR
            Case "Musculoskeletal-Left lower extremity"
                t_categoryName = EMMusculoskeletallowerL
        End Select
    End Sub


    Public Function PopulateAndExtendTable(ByVal tb1 As Wd.Table) As Boolean
        Try

            Dim nrCols As Integer = 2
            Dim nrRows As Integer = 1
            If (nrRows <= tb1.Rows.Count) And (tb1.Columns.Count >= 1) Then
                tb1.Cell(1, 1).Range.Text = "Category"
            End If
            If (nrRows <= tb1.Rows.Count) And (tb1.Columns.Count >= 2) Then
                tb1.Cell(1, 2).Range.Text = "Items"
            End If


            '''''Move Cursor to the Table 
            oCurDoc.Application.Selection.GoToNext(Microsoft.Office.Interop.Word.WdGoToItem.wdGoToTable)

            ''''''Move Cursor down in the Table
            'Dim t_list As myList
            'Dim t_categoryName As String
            Dim t_itemName As String
            Dim t_ElementID As String

            Dim ocontrolLength As Integer
            Dim t_Control_Type As ControlType

            Dim t_AssoicatedCategory As String
            Dim t_AssoicaitedItem As String
            Dim t_AssoicaitedProperty As String
            For i As Integer = 0 To m_arraylist.Count - 1
                t_categoryName = CType(m_arraylist.Item(i), myList).HistoryCategory.ToString

                t_itemName = CType(m_arraylist.Item(i), myList).HistoryItem.ToString
                t_ElementID = CType(m_arraylist.Item(i), myList).ID.ToString
                t_Control_Type = CType(m_arraylist.Item(i), myList).ControlType
                t_AssoicatedCategory = CType(m_arraylist.Item(i), myList).AssociatedCategory
                t_AssoicaitedItem = CType(m_arraylist.Item(i), myList).AssociatedItem
                t_AssoicaitedProperty = CType(m_arraylist.Item(i), myList).AssociatedProperty
                If i = 0 Then
                    tb1.Rows.Add(objMissing)  '''' new Row
                    oCurDoc.Application.Selection.Move(Wd.WdUnits.wdRow) '''' Move Cursor to Newly Added Row in table
                    oCurDoc.Application.Selection.MoveRight()
                    nrRows = nrRows + 1
                    '''' Add Catergory in New Row and category Column
                    If (nrRows <= tb1.Rows.Count) And (tb1.Columns.Count >= 1) Then
                        tb1.Cell(nrRows, 1).Range.Text = t_categoryName
                    End If

                    '''' Add Item for Selected category 
                    Dim strCatItem As String = t_itemName & "-" & t_categoryName
                    Dim strAssCatItem As String = t_AssoicaitedItem & "-" & t_AssoicatedCategory
                    Dim oNameField As Wd.FormField = Nothing
                    If (nrRows <= tb1.Rows.Count) And (tb1.Columns.Count >= 2) Then
                        tb1.Cell(nrRows, 2).Application.Selection.Select()
                        If t_Control_Type = ControlType.CheckBox Then
                            oNameField = tb1.Cell(nrRows, 2).Application.Selection.FormFields.Add(tb1.Cell(nrRows, 2).Application.Selection.Range, Wd.WdFieldType.wdFieldFormCheckBox)

                            oNameField.StatusText = strCatItem

                            oNameField.HelpText = t_ElementID & "|" & t_AssoicaitedProperty  ''t_ElementID 'Convert.ToString(t_ElementID) 
                        ElseIf t_Control_Type = ControlType.Text Then
                            tb1.Cell(nrRows, 2).Application.Selection.TypeText(Text:=t_itemName & ": ")
                            oControl = tb1.Cell(nrRows, 2).Application.Selection.Range.ContentControls.Add(Wd.WdContentControlType.wdContentControlRichText)
                            oControl.Title = t_AssoicaitedProperty
                            oControl.Range.Application.Selection.TypeText(Text:="[]")

                            tb1.Cell(nrRows, 2).Application.Selection.Range.ParentContentControl.Type = Microsoft.Office.Interop.Word.WdContentControlType.wdContentControlRichText
                            ''Title will be Field description
                            'tb1.Cell(nrRows, 2).Application.Selection.Range.ParentContentControl.Title = m_Desc
                            ''Elementid, Required flag  for reference stored in tag and temporary variables
                            'tb1.Cell(nrRows, 2).Application.Selection.Range.ParentContentControl.Tag = m_ElementId & "|" & m_DataType & "|" & m_Required & "|" & m_Fieldcategory
                            GetShortName(t_categoryName)
                            tb1.Cell(nrRows, 2).Application.Selection.Range.ParentContentControl.Tag = m_ElementId & "|" & m_DataType & "|" & m_Required & "|" & m_Fieldcategory & "|" & t_categoryName

                            'tb1.Cell(nrRows, 2).Application.Selection.Range.ParentContentControl. .DateDisplayFormat = t_AssoicaitedProperty
                            'tb1.Cell(nrRows, 2).Application.Selection.TypeText(Text:=t_itemName)
                            'oNameField = tb1.Cell(nrRows, 2).Application.Selection.FormFields.Add(tb1.Cell(nrRows, 2).Application.Selection.Range, Wd.WdFieldType.wdFieldFormTextInput)
                        Else
                            oNameField = tb1.Cell(nrRows, 2).Application.Selection.FormFields.Add(tb1.Cell(nrRows, 2).Application.Selection.Range, Wd.WdFieldType.wdFieldFormCheckBox)

                            oNameField.StatusText = strCatItem

                            oNameField.HelpText = t_ElementID & "|" & t_AssoicaitedProperty  ''t_ElementID 'Convert.ToString(t_ElementID) 
                        End If


                        If t_Control_Type = ControlType.Text Then
                            If Not IsNothing(oNameField) Then
                                oNameField.Name = t_itemName
                            End If
                        End If

                        If i <> m_arraylist.Count - 1 Then
                            If CType(m_arraylist.Item(i), myList).HistoryCategory.ToString <> CType(m_arraylist.Item(i + 1), myList).HistoryCategory.ToString Then
                                If t_Control_Type <> ControlType.Text Then
                                    tb1.Cell(nrRows, 2).Application.Selection.Text = t_itemName
                                Else
                                    oCurDoc.Application.Selection.Move(Wd.WdUnits.wdCharacter)
                                    tb1.Cell(nrRows, 2).Application.Application.Selection.MoveRight(Wd.WdUnits.wdCharacter, 1)
                                End If
                            Else
                                If t_Control_Type <> ControlType.Text Then
                                    tb1.Cell(nrRows, 2).Application.Selection.Text = t_itemName & ", "
                                Else
                                    'tb1.Cell(nrRows, 2).Application.Application.Selection.MoveRight(Wd.WdUnits.wdCharacter, oControl.Range.Characters.Count)
                                    oCurDoc.Application.Selection.MoveRight(Wd.WdUnits.wdCharacter, Count:=1)
                                    oCurDoc.Application.Selection.Select()
                                    'tb1.Cell(nrRows, 2).Application.Selection.Select()
                                    tb1.Cell(nrRows, 2).Application.Selection.Text = " , "
                                End If
                                'tb1.Cell(nrRows, 2).Application.Selection.Text = t_itemName & ", "
                            End If
                        Else
                            tb1.Cell(nrRows, 2).Application.Selection.Text = t_itemName
                        End If
                        'oNameField.CheckBox.Value = False
                        tb1.Cell(nrRows, 2).Application.Application.Selection.MoveRight(Wd.WdUnits.wdCharacter, 1)
                        'oNameField = Nothing
                    End If
                ElseIf CType(m_arraylist.Item(i), myList).HistoryCategory.ToString <> CType(m_arraylist.Item(i - 1), myList).HistoryCategory.ToString Then ''''' If the New category The add it in new Row
                    tb1.Rows.Add(objMissing)  '''' new Row
                    oCurDoc.Application.Selection.Move(Wd.WdUnits.wdRow) '''' Move Cursor to Newly Added Row in table
                    oCurDoc.Application.Selection.MoveRight()
                    nrRows = nrRows + 1
                    '''' Add Catergory in New Row and category Column
                    If (nrRows <= tb1.Rows.Count) And (tb1.Columns.Count >= 1) Then
                        tb1.Cell(nrRows, 1).Range.Text = t_categoryName
                    End If

                    '''' Add Item for Selected category
                    Dim oNameField As Wd.FormField
                    Dim strCatItem As String = t_itemName & "-" & t_categoryName
                    If (nrRows <= tb1.Rows.Count) And (tb1.Columns.Count >= 2) Then
                        tb1.Cell(nrRows, 2).Application.Selection.Select()
                        'oNameField = tb1.Cell(nrRows, 2).Application.Selection.FormFields.Add(tb1.Cell(nrRows, 2).Application.Selection.Range, Wd.WdFieldType.wdFieldFormCheckBox)
                        If t_Control_Type = ControlType.CheckBox Then
                            oNameField = tb1.Cell(nrRows, 2).Application.Selection.FormFields.Add(tb1.Cell(nrRows, 2).Application.Selection.Range, Wd.WdFieldType.wdFieldFormCheckBox)
                            oNameField.StatusText = strCatItem
                            oNameField.HelpText = t_ElementID & "|" & t_AssoicaitedProperty
                        ElseIf t_Control_Type = ControlType.Text Then
                            tb1.Cell(nrRows, 2).Application.Selection.TypeText(Text:=t_itemName & ": ")
                            'oCurDoc.Application.Selection.MoveRight(Wd.WdUnits.wdCharacter, Count:=1)
                            oControl = tb1.Cell(nrRows, 2).Application.Selection.Range.ContentControls.Add(Wd.WdContentControlType.wdContentControlRichText)
                            oControl.Title = t_AssoicaitedProperty
                            oControl.Range.Application.Selection.TypeText(Text:="[]")
                            tb1.Cell(nrRows, 2).Application.Selection.Range.ParentContentControl.Type = Microsoft.Office.Interop.Word.WdContentControlType.wdContentControlRichText
                            ''Title will be Field description
                            'tb1.Cell(nrRows, 2).Application.Selection.Range.ParentContentControl.Title = m_Desc
                            ''Elementid, Required flag  for reference stored in tag and temporary variables
                            'tb1.Cell(nrRows, 2).Application.Selection.Range.ParentContentControl.Tag = m_ElementId & "|" & m_DataType & "|" & m_Required & "|" & m_Fieldcategory
                            'If t_categoryName = "Lymphatic *requires palpation of two or more nodes per area" Or t_categoryName = "Musculoskeletal-Spine, Ribs and Pelvis" Or t_categoryName = "Musculoskeletal-Right upper extremity" Or t_categoryName = "Musculoskeletal-Left upper extremity" Or t_categoryName = "Musculoskeletal-Right lower extremity" Or t_categoryName = "Musculoskeletal-Left lower extremity" Then
                            '    MessageBox.Show("Exceed")
                            'End If
                            GetShortName(t_categoryName)
                            tb1.Cell(nrRows, 2).Application.Selection.Range.ParentContentControl.Tag = m_ElementId & "|" & m_DataType & "|" & m_Required & "|" & m_Fieldcategory & "|" & t_categoryName
                            'oNameField = tb1.Cell(nrRows, 2).Application.Selection.FormFields.Add(tb1.Cell(nrRows, 2).Application.Selection.Range, Wd.WdFieldType.wdFieldFormTextInput)
                        Else
                            oNameField = tb1.Cell(nrRows, 2).Application.Selection.FormFields.Add(tb1.Cell(nrRows, 2).Application.Selection.Range, Wd.WdFieldType.wdFieldFormCheckBox)
                            oNameField.StatusText = strCatItem
                            oNameField.HelpText = t_ElementID & "|" & t_AssoicaitedProperty
                        End If


                        '' Convert.ToString(t_ElementID)
                        If i <> m_arraylist.Count - 1 Then
                            If CType(m_arraylist.Item(i), myList).HistoryCategory.ToString <> CType(m_arraylist.Item(i + 1), myList).HistoryCategory.ToString Then
                                'tb1.Cell(nrRows, 2).Application.Selection.Text = t_itemName
                                If t_Control_Type <> ControlType.Text Then
                                    tb1.Cell(nrRows, 2).Application.Selection.Text = t_itemName
                                Else
                                    oCurDoc.Application.Selection.Move(Wd.WdUnits.wdCharacter)
                                    tb1.Cell(nrRows, 2).Application.Application.Selection.MoveRight(Wd.WdUnits.wdCharacter, 1)
                                End If
                            Else
                                If t_Control_Type <> ControlType.Text Then
                                    tb1.Cell(nrRows, 2).Application.Selection.Text = t_itemName & ", "
                                Else
                                    ''tb1.Cell(nrRows, 2).Application.Application.Selection.MoveRight(Wd.WdUnits.wdCharacter, oControl.Range.Characters.Count)
                                    'oCurDoc.Application.Selection.Move(Wd.WdUnits.wdCharacter)
                                    oCurDoc.Application.Selection.MoveRight(Wd.WdUnits.wdCharacter, Count:=1)
                                    oCurDoc.Application.Selection.Select()
                                    tb1.Cell(nrRows, 2).Application.Selection.Text = " , "

                                End If
                                'tb1.Cell(nrRows, 2).Application.Selection.Text = t_itemName & ", "
                            End If
                        Else
                            tb1.Cell(nrRows, 2).Application.Selection.Text = t_itemName
                        End If
                        'oNameField.CheckBox.Value = False
                        tb1.Cell(nrRows, 2).Application.Application.Selection.MoveRight(Wd.WdUnits.wdCharacter, 1)
                        'oNameField = Nothing
                    End If

                Else '''' If the category is already add then add Item in the category
                    Dim oNameField As Wd.FormField
                    Dim strCatItem As String
                    strCatItem = t_itemName & "-" & t_categoryName
                    If (nrRows <= tb1.Rows.Count) And (tb1.Columns.Count >= 2) Then

                        tb1.Cell(nrRows, 2).Application.Selection.Select()
                        'oNameField = tb1.Cell(nrRows, 2).Application.Selection.FormFields.Add(tb1.Cell(nrRows, 2).Application.Selection.Range, Wd.WdFieldType.wdFieldFormCheckBox)
                        If t_Control_Type = ControlType.CheckBox Then
                            oNameField = tb1.Cell(nrRows, 2).Application.Selection.FormFields.Add(tb1.Cell(nrRows, 2).Application.Selection.Range, Wd.WdFieldType.wdFieldFormCheckBox)

                            oNameField.StatusText = strCatItem
                            oNameField.HelpText = t_ElementID & "|" & t_AssoicaitedProperty
                        ElseIf t_Control_Type = ControlType.Text Then

                            tb1.Cell(nrRows, 2).Application.Selection.TypeText(Text:=t_itemName & ": ")
                            'oCurDoc.Application.Selection.MoveRight(Wd.WdUnits.wdCharacter, Count:=1)
                            oControl = tb1.Cell(nrRows, 2).Application.Selection.Range.ContentControls.Add(Wd.WdContentControlType.wdContentControlRichText)
                            oControl.Title = t_AssoicaitedProperty
                            oControl.Range.Application.Selection.TypeText(Text:="[]")
                            ocontrolLength = oControl.Range.Characters.Count
                            tb1.Cell(nrRows, 2).Application.Selection.Range.ParentContentControl.Type = Microsoft.Office.Interop.Word.WdContentControlType.wdContentControlRichText
                            ''Title will be Field description
                            'tb1.Cell(nrRows, 2).Application.Selection.Range.ParentContentControl.Title = m_Desc
                            ''Elementid, Required flag  for reference stored in tag and temporary variables
                            'tb1.Cell(nrRows, 2).Application.Selection.Range.ParentContentControl.Tag = m_ElementId & "|" & m_DataType & "|" & m_Required & "|" & m_Fieldcategory
                            GetShortName(t_categoryName)
                            tb1.Cell(nrRows, 2).Application.Selection.Range.ParentContentControl.Tag = m_ElementId & "|" & m_DataType & "|" & m_Required & "|" & m_Fieldcategory & "|" & t_categoryName

                            'oNameField = tb1.Cell(nrRows, 2).Application.Selection.FormFields.Add(tb1.Cell(nrRows, 2).Application.Selection.Range, Wd.WdFieldType.wdFieldFormTextInput)
                        Else
                            oNameField = tb1.Cell(nrRows, 2).Application.Selection.FormFields.Add(tb1.Cell(nrRows, 2).Application.Selection.Range, Wd.WdFieldType.wdFieldFormCheckBox)
                            oNameField.StatusText = strCatItem
                            oNameField.HelpText = t_ElementID
                        End If



                        If i <> m_arraylist.Count - 1 Then
                            If CType(m_arraylist.Item(i), myList).HistoryCategory.ToString <> CType(m_arraylist.Item(i + 1), myList).HistoryCategory.ToString Then
                                If t_Control_Type <> ControlType.Text Then
                                    tb1.Cell(nrRows, 2).Application.Selection.Text = t_itemName
                                Else
                                    oCurDoc.Application.Selection.Move(Wd.WdUnits.wdCharacter)
                                    tb1.Cell(nrRows, 2).Application.Application.Selection.MoveRight(Wd.WdUnits.wdCharacter, 1)
                                End If
                            Else
                                If t_Control_Type <> ControlType.Text Then
                                    tb1.Cell(nrRows, 2).Application.Selection.Text = t_itemName & ", "
                                Else
                                    'tb1.Cell(nrRows, 2).Application.Application.Selection.GoToNext(Microsoft.Office.Interop.Word.WdGoToItem.wdGoToS) '' .MoveRight(Wd.WdUnits.wdCharacter, oControl.Range.Characters.Count)
                                    ''tb1.Cell(nrRows, 2).Application.Application.Selection.NextField() '' .MoveRight(Wd.WdUnits.wdWor, oControl.Range.Characters.Count)
                                    'oCurDoc.Application.Selection.Move(Wd.WdUnits.wdCharacter)
                                    oCurDoc.Application.Selection.MoveRight(Wd.WdUnits.wdCharacter, Count:=1)
                                    oCurDoc.Application.Selection.Select()
                                    tb1.Cell(nrRows, 2).Application.Selection.Text = " , "

                                End If
                            End If

                        Else
                            If t_Control_Type <> ControlType.Text Then
                                tb1.Cell(nrRows, 2).Application.Selection.Text = t_itemName
                            End If
                        End If
                        'oNameField.CheckBox.Value = False
                        'If t_Control_Type = ControlType.Text Then
                        '    tb1.Cell(nrRows, 2).Application.Application.Selection.MoveRight(Wd.WdUnits.wdCharacter, oControl.Range.Characters.Count) '' .MoveRight(Wd.WdUnits.wdCharacter, 1)
                        'Else

                        'End If
                        tb1.Cell(nrRows, 2).Application.Application.Selection.MoveRight(Wd.WdUnits.wdCharacter, 1)
                        'oNameField = Nothing
                    End If

                End If
            Next

            ''''Move Cursor down in the Table


            oCurDoc.Application.Selection.MoveDown(Unit:=Wd.WdUnits.wdLine, Count:=1)
            oCurDoc.Application.Selection.MoveRight(Unit:=Wd.WdUnits.wdCharacter, Count:=1)
            oCurDoc.Application.Selection.InsertParagraph()
            oCurDoc.Application.Selection.MoveRight(Unit:=Wd.WdUnits.wdCharacter, Count:=1)

            'For i As Integer = 0 To tb1.Columns.Count - 1
            '    tb1.Columns(i + 1).AutoFit()
            'Next
            Return True
            'oCurDoc.Application.Selection.MoveRight()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            If oCurDoc.Application.Selection.Range.ParentContentControl.Type = Microsoft.Office.Interop.Word.WdContentControlType.wdContentControlRichText Then
                oCurDoc.Application.Selection.Range.ParentContentControl.Delete(True)
            End If
            Return False
        End Try
    End Function
    Public Sub FormatTables(ByVal tstyle As Wd.Style, ByVal tb1 As Wd.Table)

        'For Each t1 As Wd.Table In oCurDoc.Tables
        Dim objtStyl As Object = CType(tstyle, Object)
        tb1.Range.Style = tstyle
        'Next
    End Sub
    Public Function CreateTableStyle() As Wd.Style
        Dim styleTypeTable As Object = Wd.WdStyleType.wdStyleTypeTable
        Dim StyleName As String = "New Table Style" & Convert.ToString(DateTime.Now)
        Dim styl As Wd.Style = oCurDoc.Styles.Add(StyleName, styleTypeTable)
        styl.Font.Name = "Arial"
        styl.Font.Size = 10
        Dim stylTbl As Wd.TableStyle = styl.Table
        stylTbl.Borders.Enable = 1

        Dim evenrowbinding As Wd.ConditionalStyle = stylTbl.Condition(Microsoft.Office.Interop.Word.WdConditionCode.wdEvenRowBanding)
        evenrowbinding.Shading.Texture = TextureNone
        evenrowbinding.Shading.BackgroundPatternColor = ColorGray10
        evenrowbinding.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderLeft).LineStyle = LineStyleDouble
        evenrowbinding.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderRight).LineStyle = LineStyleDouble
        evenrowbinding.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderVertical).LineStyle = LineStyleSingle
        evenrowbinding.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderBottom).LineStyle = LineStyleSingle

        Dim FirstRow As Wd.ConditionalStyle = stylTbl.Condition(Microsoft.Office.Interop.Word.WdConditionCode.wdFirstRow)
        'FirstRow.Shading.BackgroundPatternColor = Microsoft.Office.Interop.Word.WdColor.wdColorWhite
        FirstRow.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderLeft).LineStyle = LineStyleDouble
        FirstRow.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderTop).LineStyle = LineStyleDouble
        FirstRow.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderRight).LineStyle = LineStyleDouble
        FirstRow.Font.Size = 14
        FirstRow.Font.ColorIndex = Microsoft.Office.Interop.Word.WdColorIndex.wdAuto
        FirstRow.Font.Bold = 1

        stylTbl.RowStripe = 1
        Return styl
    End Function
    Public Function PopulateAndExtendTableNew(ByVal tb1 As Wd.Table) As Boolean
        Try

            Dim nrCols As Integer = 2
            Dim nrRows As Integer = 1
            'tb1.Cell(1, 1).Range.Text = "Category"
            'tb1.Cell(1, 2).Range.Text = "Items"
            ''''''Move Cursor to the Table 

            oCurDoc.Application.Selection.GoToNext(Microsoft.Office.Interop.Word.WdGoToItem.wdGoToTable)

            Dim _oNameField As Wd.FormField
            'SLR: is '0' ok?
            _oNameField = tb1.Cell(0, 0).Application.Selection.FormFields.Add(tb1.Cell(0, 0).Application.Selection.Range, Wd.WdFieldType.wdFieldFormCheckBox)
            'ADDED BY SHUBHANGI TO RE SOLVE OBJECT HAS BEEN DELETED EXCEPTION
            tb1.Cell(0, 0).Application.Selection.Text = m_Caption
            _oNameField.CheckBox.Value = True
            _oNameField.StatusText = m_Caption
            _oNameField.HelpText = "Group"
            _oNameField = Nothing

            'COMMENTED BY SHUBHANGI TO RE SOLVE OBJECT HAS BEEN DELETED EXCEPTION
            'tb1.Cell(0, 0).Application.Selection.Text = m_Caption
            oCurDoc.Application.Selection.MoveRight()

            ''''''Move Cursor down in the Table
            'oCurDoc.Application.Selection.MoveDown()
            'oCurDoc.Application.Selection.MoveRight()
            ' Dim t_list As myList
            'Dim t_categoryName As String
            Dim t_itemName As String
            Dim t_ElementID As String
            Dim t_Control_Type As ControlType
            Dim t_AssociatedCategory As String
            Dim t_AssociatedItem As String
            Dim t_AssoicaitedProperty As String
            For i As Integer = 0 To m_arraylist.Count - 1
                t_categoryName = CType(m_arraylist.Item(i), myList).HistoryCategory.ToString
                t_itemName = CType(m_arraylist.Item(i), myList).HistoryItem.ToString
                t_ElementID = CType(m_arraylist.Item(i), myList).ID.ToString
                t_Control_Type = CType(m_arraylist.Item(i), myList).ControlType
                t_AssociatedCategory = CType(m_arraylist.Item(i), myList).AssociatedCategory
                t_AssociatedItem = CType(m_arraylist.Item(i), myList).AssociatedItem
                t_AssoicaitedProperty = CType(m_arraylist.Item(i), myList).AssociatedProperty
                If i = 0 Then
                    tb1.Rows.Add(objMissing)  '''' new Row
                    oCurDoc.Application.Selection.Move(Wd.WdUnits.wdRow) '''' Move Cursor to Newly Added Row in table
                    oCurDoc.Application.Selection.MoveRight()
                    nrRows = nrRows + 1
                    '''' Add Catergory in New Row and category Column
                    If (nrRows <= tb1.Rows.Count) And (tb1.Columns.Count >= 1) Then
                        tb1.Cell(nrRows, 1).Range.Text = t_categoryName
                    End If

                    '''' Add Item for Selected category
                    Dim oNameField As Wd.FormField
                    Dim strCatItem As String = t_itemName & "-" & t_categoryName
                    If (nrRows <= tb1.Rows.Count) And (tb1.Columns.Count >= 2) Then
                        tb1.Cell(nrRows, 2).Application.Selection.Select()

                        If t_Control_Type = ControlType.CheckBox Then
                            oNameField = tb1.Cell(nrRows, 2).Application.Selection.FormFields.Add(tb1.Cell(nrRows, 2).Application.Selection.Range, Wd.WdFieldType.wdFieldFormCheckBox)
                            oNameField.StatusText = strCatItem
                            oNameField.HelpText = t_ElementID & "|" & t_AssoicaitedProperty   ''t_ElementID 'Convert.ToString(t_ElementID) 

                        ElseIf t_Control_Type = ControlType.Text Then
                            tb1.Cell(nrRows, 2).Application.Selection.TypeText(Text:=t_itemName & ": ")
                            oControl = tb1.Cell(nrRows, 2).Application.Selection.Range.ContentControls.Add(Wd.WdContentControlType.wdContentControlRichText)
                            oControl.Title = t_AssoicaitedProperty
                            oControl.Range.Application.Selection.TypeText(Text:="[]")
                            tb1.Cell(nrRows, 2).Application.Selection.Range.ParentContentControl.Type = Microsoft.Office.Interop.Word.WdContentControlType.wdContentControlRichText
                            ''Title will be Field description
                            'tb1.Cell(nrRows, 2).Application.Selection.Range.ParentContentControl.Title = m_Desc
                            ''Elementid, Required flag  for reference stored in tag and temporary variables
                            GetShortName(t_categoryName)
                            tb1.Cell(nrRows, 2).Application.Selection.Range.ParentContentControl.Tag = m_ElementId & "|" & m_DataType & "|" & m_Required & "|" & m_Fieldcategory & "|" & t_categoryName
                            tb1.Cell(nrRows, 2).Application.Selection.Range.ParentContentControl.BuildingBlockCategory = strCatItem
                            'tb1.Cell(nrRows, 2).Application.Selection.Range.ParentContentControl.Tag = m_DataType
                            'oNameField = tb1.Cell(nrRows, 2).Application.Selection.FormFields.Add(tb1.Cell(nrRows, 2).Application.Selection.Range, Wd.WdFieldType.wdFieldFormTextInput)
                        Else
                            oNameField = tb1.Cell(nrRows, 2).Application.Selection.FormFields.Add(tb1.Cell(nrRows, 2).Application.Selection.Range, Wd.WdFieldType.wdFieldFormCheckBox)
                            oNameField.StatusText = strCatItem
                            oNameField.HelpText = t_ElementID & "|" & t_AssoicaitedProperty  ''t_ElementID 'Convert.ToString(t_ElementID) 

                        End If


                        If i <> m_arraylist.Count - 1 Then
                            If CType(m_arraylist.Item(i), myList).HistoryCategory.ToString <> CType(m_arraylist.Item(i + 1), myList).HistoryCategory.ToString Then
                                If t_Control_Type <> ControlType.Text Then
                                    tb1.Cell(nrRows, 2).Application.Selection.Text = t_itemName
                                Else
                                    oCurDoc.Application.Selection.Move(Wd.WdUnits.wdCharacter)
                                    tb1.Cell(nrRows, 2).Application.Application.Selection.MoveRight(Wd.WdUnits.wdCharacter, 1)
                                End If
                            Else
                                If t_Control_Type <> ControlType.Text Then
                                    tb1.Cell(nrRows, 2).Application.Selection.Text = t_itemName & ", "
                                Else
                                    'tb1.Cell(nrRows, 2).Application.Application.Selection.MoveRight(Wd.WdUnits.wdCharacter, oControl.Range.Characters.Count)
                                    'oCurDoc.Application.Selection.Move(Wd.WdUnits.wdCharacter)
                                    oCurDoc.Application.Selection.MoveRight(Wd.WdUnits.wdCharacter, Count:=1)
                                    oCurDoc.Application.Selection.Select()
                                    tb1.Cell(nrRows, 2).Application.Selection.Text = " , "

                                End If
                            End If
                        Else
                            tb1.Cell(nrRows, 2).Application.Selection.Text = t_itemName
                        End If
                        'oNameField.CheckBox.Value = False
                        tb1.Cell(nrRows, 2).Application.Application.Selection.MoveRight(Wd.WdUnits.wdCharacter, 1)
                        'oNameField = Nothing
                    End If

                ElseIf CType(m_arraylist.Item(i), myList).HistoryCategory.ToString <> CType(m_arraylist.Item(i - 1), myList).HistoryCategory.ToString Then ''''' If the New category The add it in new Row
                    tb1.Rows.Add(objMissing)  '''' new Row
                    oCurDoc.Application.Selection.Move(Wd.WdUnits.wdRow) '''' Move Cursor to Newly Added Row in table
                    oCurDoc.Application.Selection.MoveRight()
                    nrRows = nrRows + 1
                    '''' Add Catergory in New Row and category Column
                    If (nrRows <= tb1.Rows.Count) And (tb1.Columns.Count >= 1) Then
                        tb1.Cell(nrRows, 1).Range.Text = t_categoryName
                    End If

                    '''' Add Item for Selected category
                    Dim oNameField As Wd.FormField
                    tb1.Cell(nrRows, 2).Application.Selection.Select()
                    If (nrRows <= tb1.Rows.Count) And (tb1.Columns.Count >= 2) Then
                        Dim strCatItem As String = t_itemName & "-" & t_categoryName
                        If t_Control_Type = ControlType.CheckBox Then
                            oNameField = tb1.Cell(nrRows, 2).Application.Selection.FormFields.Add(tb1.Cell(nrRows, 2).Application.Selection.Range, Wd.WdFieldType.wdFieldFormCheckBox)
                            oNameField.StatusText = strCatItem
                            oNameField.HelpText = t_ElementID & "|" & t_AssoicaitedProperty
                        ElseIf t_Control_Type = ControlType.Text Then
                            tb1.Cell(nrRows, 2).Application.Selection.TypeText(Text:=t_itemName & ": ")
                            oControl = tb1.Cell(nrRows, 2).Application.Selection.Range.ContentControls.Add(Wd.WdContentControlType.wdContentControlRichText)
                            oControl.Title = t_AssoicaitedProperty
                            oControl.Range.Application.Selection.TypeText(Text:="[]")
                            tb1.Cell(nrRows, 2).Application.Selection.Range.ParentContentControl.Type = Microsoft.Office.Interop.Word.WdContentControlType.wdContentControlRichText
                            ''Title will be Field description
                            'tb1.Cell(nrRows, 2).Application.Selection.Range.ParentContentControl.Title = m_Desc
                            ''Elementid, Required flag  for reference stored in tag and temporary variables
                            GetShortName(t_categoryName)
                            tb1.Cell(nrRows, 2).Application.Selection.Range.ParentContentControl.Tag = m_ElementId & "|" & m_DataType & "|" & m_Required & "|" & m_Fieldcategory & "|" & t_categoryName
                            'tb1.Cell(nrRows, 2).Application.Selection.Range.ParentContentControl.Tag = m_DataType

                            'oNameField = tb1.Cell(nrRows, 2).Application.Selection.FormFields.Add(tb1.Cell(nrRows, 2).Application.Selection.Range, Wd.WdFieldType.wdFieldFormTextInput)
                        Else
                            oNameField = tb1.Cell(nrRows, 2).Application.Selection.FormFields.Add(tb1.Cell(nrRows, 2).Application.Selection.Range, Wd.WdFieldType.wdFieldFormCheckBox)
                            oNameField.StatusText = strCatItem
                            oNameField.HelpText = t_ElementID & "|" & t_AssoicaitedProperty
                        End If




                        '' Convert.ToString(t_ElementID)
                        If i <> m_arraylist.Count - 1 Then
                            If CType(m_arraylist.Item(i), myList).HistoryCategory.ToString <> CType(m_arraylist.Item(i + 1), myList).HistoryCategory.ToString Then
                                If t_Control_Type <> ControlType.Text Then
                                    tb1.Cell(nrRows, 2).Application.Selection.Text = t_itemName
                                Else
                                    oCurDoc.Application.Selection.Move(Wd.WdUnits.wdCharacter)
                                    tb1.Cell(nrRows, 2).Application.Application.Selection.MoveRight(Wd.WdUnits.wdCharacter, 1)
                                End If
                            Else
                                If t_Control_Type <> ControlType.Text Then
                                    tb1.Cell(nrRows, 2).Application.Selection.Text = t_itemName & ", "
                                Else
                                    ''tb1.Cell(nrRows, 2).Application.Application.Selection.MoveRight(Wd.WdUnits.wdCharacter, oControl.Range.Characters.Count)
                                    'oCurDoc.Application.Selection.Move(Wd.WdUnits.wdCharacter)
                                    oCurDoc.Application.Selection.MoveRight(Wd.WdUnits.wdCharacter, Count:=1)
                                    oCurDoc.Application.Selection.Select()
                                    tb1.Cell(nrRows, 2).Application.Selection.Text = " , "

                                End If

                                'tb1.Cell(nrRows, 2).Application.Selection.Text = t_itemName & ", "
                            End If
                        Else
                            tb1.Cell(nrRows, 2).Application.Selection.Text = t_itemName
                        End If
                        'oNameField.CheckBox.Value = False
                        tb1.Cell(nrRows, 2).Application.Application.Selection.MoveRight(Wd.WdUnits.wdCharacter, 1)
                        'oNameField = Nothing
                    End If

                Else '''' If the category is already add then add Item in the category
                    Dim oNameField As Wd.FormField
                    If (nrRows <= tb1.Rows.Count) And (tb1.Columns.Count >= 2) Then
                        tb1.Cell(nrRows, 2).Application.Selection.Select()

                        Dim strCatItem As String = t_itemName & "-" & t_categoryName
                        If t_Control_Type = ControlType.CheckBox Then
                            oNameField = tb1.Cell(nrRows, 2).Application.Selection.FormFields.Add(tb1.Cell(nrRows, 2).Application.Selection.Range, Wd.WdFieldType.wdFieldFormCheckBox)

                            oNameField.StatusText = strCatItem
                            oNameField.HelpText = t_ElementID & "|" & t_AssoicaitedProperty
                        ElseIf t_Control_Type = ControlType.Text Then

                            tb1.Cell(nrRows, 2).Application.Selection.TypeText(Text:=t_itemName & ": ")

                            oControl = tb1.Cell(nrRows, 2).Application.Selection.Range.ContentControls.Add(Wd.WdContentControlType.wdContentControlRichText)
                            oControl.Title = t_AssoicaitedProperty
                            oControl.Range.Application.Selection.TypeText(Text:="[]")
                            tb1.Cell(nrRows, 2).Application.Selection.Range.ParentContentControl.Type = Microsoft.Office.Interop.Word.WdContentControlType.wdContentControlRichText
                            ''Title will be Field description
                            'tb1.Cell(nrRows, 2).Application.Selection.Range.ParentContentControl.Title = m_Desc
                            ''Elementid, Required flag  for reference stored in tag and temporary variables
                            GetShortName(t_categoryName)
                            tb1.Cell(nrRows, 2).Application.Selection.Range.ParentContentControl.Tag = m_ElementId & "|" & m_DataType & "|" & m_Required & "|" & m_Fieldcategory & "|" & t_categoryName
                            'tb1.Cell(nrRows, 2).Application.Selection.Range.ParentContentControl.Tag = m_DataType
                            tb1.Cell(nrRows, 2).Application.Selection.Range.ParentContentControl.BuildingBlockCategory = strCatItem

                            'oNameField = tb1.Cell(nrRows, 2).Application.Selection.FormFields.Add(tb1.Cell(nrRows, 2).Application.Selection.Range, Wd.WdFieldType.wdFieldFormTextInput)
                        Else
                            oNameField = tb1.Cell(nrRows, 2).Application.Selection.FormFields.Add(tb1.Cell(nrRows, 2).Application.Selection.Range, Wd.WdFieldType.wdFieldFormCheckBox)
                            oNameField.StatusText = strCatItem
                            oNameField.HelpText = t_ElementID & "|" & t_AssoicaitedProperty
                        End If








                        If i <> m_arraylist.Count - 1 Then
                            If CType(m_arraylist.Item(i), myList).HistoryCategory.ToString <> CType(m_arraylist.Item(i + 1), myList).HistoryCategory.ToString Then
                                If t_Control_Type <> ControlType.Text Then
                                    tb1.Cell(nrRows, 2).Application.Selection.Text = t_itemName
                                Else
                                    oCurDoc.Application.Selection.Move(Wd.WdUnits.wdCharacter)
                                    tb1.Cell(nrRows, 2).Application.Application.Selection.MoveRight(Wd.WdUnits.wdCharacter, 1)
                                End If
                            Else
                                If t_Control_Type <> ControlType.Text Then
                                    tb1.Cell(nrRows, 2).Application.Selection.Text = t_itemName & ", "
                                Else
                                    'tb1.Cell(nrRows, 2).Application.Application.Selection.GoToNext(Microsoft.Office.Interop.Word.WdGoToItem.wdGoToS) '' .MoveRight(Wd.WdUnits.wdCharacter, oControl.Range.Characters.Count)
                                    ''tb1.Cell(nrRows, 2).Application.Application.Selection.NextField() '' .MoveRight(Wd.WdUnits.wdWor, oControl.Range.Characters.Count)
                                    'oCurDoc.Application.Selection.Move(Wd.WdUnits.wdCharacter)
                                    oCurDoc.Application.Selection.MoveRight(Wd.WdUnits.wdCharacter, Count:=1)
                                    oCurDoc.Application.Selection.Select()
                                    tb1.Cell(nrRows, 2).Application.Selection.Text = " , "

                                End If

                            End If

                        Else
                            If t_Control_Type <> ControlType.Text Then
                                tb1.Cell(nrRows, 2).Application.Selection.Text = t_itemName
                            End If
                        End If
                        'oNameField.CheckBox.Value = False
                        tb1.Cell(nrRows, 2).Application.Application.Selection.MoveRight(Wd.WdUnits.wdCharacter, 1)
                        'oNameField = Nothing
                    End If

                End If
            Next
            If (tb1.Rows.Count >= 1) And (tb1.Columns.Count >= 2) Then
                tb1.Cell(1, 1).Merge(tb1.Cell(1, 2))
            End If


            ''''Move Cursor down in the Table


            oCurDoc.Application.Selection.MoveDown(Unit:=Wd.WdUnits.wdLine, Count:=1)
            oCurDoc.Application.Selection.MoveRight(Unit:=Wd.WdUnits.wdCharacter, Count:=1)
            oCurDoc.Application.Selection.InsertParagraph()
            oCurDoc.Application.Selection.MoveRight(Unit:=Wd.WdUnits.wdCharacter, Count:=1)
            'oCurDoc.Application.Selection.MoveRight()
            Return True
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            If oCurDoc.Application.Selection.Range.ParentContentControl.Type = Microsoft.Office.Interop.Word.WdContentControlType.wdContentControlRichText Then
                oCurDoc.Application.Selection.Range.ParentContentControl.Delete(True)
            End If
            Return False
        End Try
    End Function


    'Private Sub GetRCTable(ByVal _cnt As Int32)
    '    If _cnt > 0 Then
    '        ''By default assuming columns count as 4 for proper formatting if sub items count is greater than 2
    '        If _cnt >= 3 Then
    '            noCols = 4
    '            Dim _Rem As Int32
    '            ''set the Rows count based on the no of sub items
    '            noRows = Math.DivRem(_cnt, 3, _Rem)
    '            If _Rem = 0 Then
    '            Else
    '                noRows += 1
    '            End If
    '        Else
    '            ''set the columns count as 3 and rows count as 1, if the no of sub items is less than 3
    '            noCols = 3
    '            noRows = 1
    '        End If
    '    End If
    'End Sub

    ''' <summary>
    ''' For defining the Rows and columns in Table
    ''' </summary>
    ''' <param name="_cnt"></param>
    ''' <remarks></remarks>
    Private Sub GetRCTable(ByVal _cnt As Int32)
        If _cnt > 0 Then
            ''By default assuming columns count as 4 for proper formatting if sub items count is greater than 2
            If _cnt >= 3 Then
                noCols = 4
                Dim _Rem As Int32
                ''set the Rows count based on the no of sub items
                noRows = Math.DivRem(_cnt, 3, _Rem)
                If _Rem = 0 Then
                Else
                    noRows += 1
                End If
            Else
                ''set the columns count as 3 and rows count as 1, if the no of sub items is less than 3
                noCols = 3
                noRows = 1
            End If
        End If
    End Sub

#End Region





    Private Sub cmbCategory_SelectionChangeCommitted(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbCategory.SelectionChangeCommitted
        'If cmbCategory..se;le]\Text = Tag Then
        If cmbCategory.Text = "Tags" Then
            'chkAssociateEMCategory.Visible = True
            'lblAssociateEM.Visible = True
            If PnlSnomed.Visible = False Then
                pnlTemplate.Height = pnlTemplate.Height + PnlSnomed.Height
                PnlSnomed.Visible = True
            End If

            tlsTemplateGallery.IsEMAssociated = True
        Else
            'chkAssociateEMCategory.Visible = False
            'lblAssociateEM.Visible = False
            strSnomedCode = ""
            strSnomedDescription = ""
            txtSNOMEDCode.Text = ""
            If PnlSnomed.Visible = True Then
                pnlTemplate.Height = pnlTemplate.Height - PnlSnomed.Height
                PnlSnomed.Visible = False
            End If
            tlsTemplateGallery.IsEMAssociated = False
        End If

        If cmbCategory.Text = "MU Provider Reference" Then
            btnBibliography.Visible = True
        Else
            btnBibliography.Visible = False
        End If
    End Sub
    ''' <summary>
    ''' Show microphone
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ShowMicroPhone() Implements mdlgloVoice.gloVoice.ShowMicroPhone
        If gblnVoiceEnabled = True And gblnSpeakerExists = True Then
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
        If gblnVoiceEnabled = True And gblnSpeakerExists = True Then
            If Not IsNothing(ogloVoice) Then
                ogloVoice.TurnoffMicrophone()
            End If
        End If

    End Sub
    Private Sub InitializeVoiceObject()
        If Not IsNothing(ogloVoice) Then
            ogloVoice.Dispose()
            ogloVoice = Nothing
        End If
        Dim oHashtable As Hashtable = AddBasicVoiceCommands()
        ogloVoice = New ClsVoice(oHashtable)

        ogloVoice.MyWordToolStrip = Me.tlsTemplateGallery
        ogloVoice.MDIParentVoice = MyMDIParent
        ogloVoice.MessageName = "Template Gallery"
        ogloVoice.eActivityModule = gloAuditTrail.ActivityModule.TemplateGallery
        ogloVoice.InitializeVoiceComponents()
        ogloVoice.DelWordToolStripClick = New HandleWordToolStripClick(AddressOf tlsTemplateGallery_ToolStripClick)
        'ShowMicroPhone()
    End Sub

    Public ReadOnly Property MyParent() As MainMenu Implements mdlgloVoice.gloVoice.MyParent
        Get
            Return MyMDIParent
        End Get
    End Property
    'ADDED BY SHUBHANGI 20100625 FOR ENTER EVENT
    Private Sub txtSearchLiquidData_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSearchLiquidData.KeyPress
        If (e.KeyChar = ChrW(13)) Then
            trvDiscrete.Select()
        Else
            trvDiscrete.SelectedNode = trvDiscrete.Nodes.Item(0)
        End If
    End Sub

    Private Sub txtSearchLiquidData_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearchLiquidData.TextChanged
        'SHUBHANGI 20090919
        Try
            If Trim(txtSearchLiquidData.Text) <> "" Then
                Dim mynode As TreeNode
                'Root node collection
                'mynode is current/yesterday/last week/last month
                For Each mynode In trvDiscrete.Nodes.Item(0).Nodes

                    ' Dim mychildnode As TreeNode
                    'For Each mychildnode In mynode.Nodes.Item(0)


                    If Trim(mynode.Text) <> "" Then

                        Dim str As String
                        str = UCase(Trim(mynode.Text))
                        If str.Contains(UCase(txtSearchLiquidData.Text.Trim)) Then
                            'trvDiscrete.SelectedNode = trvDiscrete.SelectedNode.FirstNode
                            trvDiscrete.SelectedNode = mynode
                            ' txtSearchLiquidData.Focus()
                            Exit Sub
                        End If

                        'End If
                    End If
                    'Next
                    'Next
                Next

            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub btnClearLiquiddata_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearLiquiddata.Click
        'SHUBHANGI 20091001 
        'USE CLEAR BUTTON TO CLEAR SEARCH TEXT BOX IN TREE VIEW.
        txtSearchLiquidData.ResetText()
        txtSearchLiquidData.Focus()
        trvDiscrete.SelectedNode = trvDiscrete.Nodes(0)
        trvDiscrete.Focus()
    End Sub

    Private Sub btnClearTemplate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearTemplate.Click

        txtsearchtemplate.ResetText()
        txtsearchtemplate.Focus()
        trvDataDictionary.SelectedNode = trvDataDictionary.Nodes(0)

    End Sub



    Public Function GetAllTemplateSpecility() As DataTable

        Dim oDB As New DataBaseLayer
        Dim oParamater As DBParameter
        Dim oResultTable As DataTable

        Try
            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.VarChar
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@CategoryType"

            oParamater.Value = "Template Specialty"
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing
            oResultTable = oDB.GetDataTable("gsp_FillCategory_MST")

            If Not oResultTable Is Nothing Then
                oResultTable.Rows.Add(0, "")
                Return oResultTable
            Else
                Return Nothing
            End If

        Catch ex As Exception

            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing

        Finally

            oDB.Dispose()
            oDB = Nothing
        End Try

    End Function

    Private Sub btnBibliography_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBibliography.Click
        Dim oBiblio As New frmBibliography(1)
        'Showing Previous Bibliographic values
        oBiblio.sBibliography = sBibliography
        oBiblio.sDeveloper = sDeveloper
        oBiblio.ShowDialog(IIf(IsNothing(oBiblio.Parent), Me, oBiblio.Parent))

        'Getting New Bibliographic values
        sBibliography = oBiblio.sBibliography
        sDeveloper = oBiblio.sDeveloper

        oBiblio.Dispose()
        oBiblio = Nothing
    End Sub

    Private Sub btnBrowseSNOMEDCode_Click(sender As Object, e As System.EventArgs) Handles btnBrowseSNOMEDCode.Click
        Try
            ofrmSnomedList = New frmViewListControl
            oSnomedListControl = New gloListControl.gloListControl(GetConnectionString(), gloListControl.gloListControlType.TagSnomed, False, Me.Width)
            oSnomedListControl.ControlHeader = "Snomed Code"
            'set the property true for refused code you want 
            oSnomedListControl.bShowNotTakenCodes = True
            oSnomedListControl.bShowAttributeCodes = True
            AddHandler oSnomedListControl.ItemSelectedClick, AddressOf oSnomedListControl_ItemSelectedClick
            AddHandler oSnomedListControl.ItemClosedClick, AddressOf oSnomedListControl_ItemClosedClick
            ofrmSnomedList.Controls.Add(oSnomedListControl)
            oSnomedListControl.Dock = DockStyle.Fill
            oSnomedListControl.BringToFront()
            oSnomedListControl.ShowHeaderPanel(False)
            ofrmSnomedList.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedSingle
            ofrmSnomedList.Text = "Snomed Code"
            ofrmSnomedList.ShowDialog(IIf(IsNothing(CType(ofrmSnomedList, Control).Parent), Me, CType(ofrmSnomedList, Control).Parent))
            If (IsNothing(oSnomedListControl) = False) Then
                ofrmSnomedList.Controls.Remove(oSnomedListControl)
                RemoveHandler oSnomedListControl.ItemSelectedClick, AddressOf oSnomedListControl_ItemSelectedClick
                RemoveHandler oSnomedListControl.ItemClosedClick, AddressOf oSnomedListControl_ItemClosedClick
                oSnomedListControl.Dispose()
                oSnomedListControl = Nothing
            End If

            If IsNothing(ofrmSnomedList) = False Then
                ofrmSnomedList.Dispose()
                ofrmSnomedList = Nothing
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.ProblemList, gloAuditTrail.ActivityCategory.ProblemList, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub oSnomedListControl_ItemSelectedClick(sender As Object, e As EventArgs)
        Try
            If oSnomedListControl.SelectedItems.Count > 0 Then
                For i As Int16 = 0 To oSnomedListControl.SelectedItems.Count - 1
                    txtSNOMEDCode.Text = oSnomedListControl.SelectedItems(i).Code + " - " + oSnomedListControl.SelectedItems(i).Description
                    strSnomedCode = Convert.ToString(oSnomedListControl.SelectedItems(i).Code)
                    strSnomedDescription = Convert.ToString(oSnomedListControl.SelectedItems(i).Description)
                Next
                ofrmSnomedList.Close()
            Else
                txtSNOMEDCode.Clear()
                ofrmSnomedList.Close()
            End If

        Catch ex As Exception
            MessageBox.Show("Error on User List Control." & ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub oSnomedListControl_ItemClosedClick(sender As Object, e As EventArgs)
        ofrmSnomedList.Close()
    End Sub

    Private Sub btnClearSNOMEDCode_Click(sender As Object, e As System.EventArgs) Handles btnClearSNOMEDCode.Click
        txtSNOMEDCode.Clear()
        strSnomedCode = String.Empty
        strSnomedDescription = String.Empty
    End Sub
End Class
