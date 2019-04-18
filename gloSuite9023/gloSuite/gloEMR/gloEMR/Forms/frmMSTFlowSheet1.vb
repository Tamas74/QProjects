Imports System.Drawing.Text.InstalledFontCollection
Imports System.Reflection
Public Class frmMSTFlowSheet1
    Inherits System.Windows.Forms.Form

    '''' While Add, return this Paramerters to View-From so the Newly Added Records get Highlighted 
    Public _FlowSheetName As String
    Public _OldFlowSheetName As String
    ''''
    Public blnEdit As Boolean
    '' 
    Public _Cancel As Boolean = False

    Private m_ID As Long
    '''' m_IsInUse = True if then selected Flow sheet has Records stored in it
    Private m_IsInUse As Boolean

    Private objclsFlowSheet As New clsFlowSheet
    Dim tblFlowSheet As New DataTable
    Dim strAssociatedName As String = ""
    Dim objFlowsheet As New clsFlowSheet

    Dim _TempColumnName As String
    Dim _IsModified As Boolean = False
    Dim _IsSaved As Boolean = False
    Dim _arrLabs As New ArrayList
    Dim _arrManagment As New ArrayList
    Dim _arrOrders As New ArrayList
    Dim _arrOtherDiag As New ArrayList
    Dim dtEMField As DataTable
    Dim ToolTip1 As New System.Windows.Forms.ToolTip

#Region " Windows Controls "

    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents mskFormat As System.Windows.Forms.MaskedTextBox
    Friend WithEvents tlsp_MSTFlowSheet As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents tls_btnOK As System.Windows.Forms.ToolStripButton
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents tls_btnCancel As System.Windows.Forms.ToolStripButton
    Friend WithEvents tls_btnAdd As System.Windows.Forms.ToolStripButton
    Friend WithEvents tls_btnDelete As System.Windows.Forms.ToolStripButton
    Friend WithEvents pic_Update As System.Windows.Forms.PictureBox
    Friend WithEvents pic_Add As System.Windows.Forms.PictureBox
    Private WithEvents Label18 As System.Windows.Forms.Label
    Private WithEvents Label19 As System.Windows.Forms.Label
    Private WithEvents Label20 As System.Windows.Forms.Label
    Private WithEvents Label21 As System.Windows.Forms.Label
    Private WithEvents lbl_BottomBrd As System.Windows.Forms.Label
    Private WithEvents lbl_LeftBrd As System.Windows.Forms.Label
    Private WithEvents lbl_RightBrd As System.Windows.Forms.Label
    Private WithEvents lbl_TopBrd As System.Windows.Forms.Label
    Private WithEvents Label14 As System.Windows.Forms.Label
    Private WithEvents Label15 As System.Windows.Forms.Label
    Private WithEvents Label16 As System.Windows.Forms.Label
    Private WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents cmbAssociatedEMField As System.Windows.Forms.ComboBox
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents btnBrowse As System.Windows.Forms.Button
    Friend WithEvents Label27 As System.Windows.Forms.Label
#End Region

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
    End Sub

    Public Sub New(ByVal ID As Long, ByVal IsInUse As Boolean)
        MyBase.New()
        m_ID = ID
        m_IsInUse = IsInUse
        'This call is required by the Windows Form Designer.
        InitializeComponent()
        'Add any initialization after the InitializeComponent() call
    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            Try
                gloGlobal.cEventHelper.RemoveAllEventHandlers(Me)
            Catch
            End Try
            Try

                If (IsNothing(grdFlowSheetDetails) = False) Then
                    grdFlowSheetDetails.TableStyles.Clear()
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(grdFlowSheetDetails)
                    grdFlowSheetDetails.Dispose()
                    grdFlowSheetDetails = Nothing
                End If
            Catch ex As Exception

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
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtFlowSheetName As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cmbColumnNo As System.Windows.Forms.ComboBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents txtColumnWidth As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtNoofColoumn As System.Windows.Forms.TextBox
    '  Friend WithEvents FontDialog1 As System.Windows.Forms.FontDialog
    Friend WithEvents grdFlowSheetDetails As System.Windows.Forms.DataGrid
    Friend WithEvents txtColumnName As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents cmbFontSize As System.Windows.Forms.ComboBox
    Friend WithEvents cmbFormat As System.Windows.Forms.ComboBox
    Friend WithEvents cmbFontName As System.Windows.Forms.ComboBox
    Friend WithEvents cmbAllinment As System.Windows.Forms.ComboBox
    Friend WithEvents chkBold As System.Windows.Forms.CheckBox
    Friend WithEvents chkItalic As System.Windows.Forms.CheckBox
    Friend WithEvents chkUnderline As System.Windows.Forms.CheckBox
    Friend WithEvents ColorDialog1 As System.Windows.Forms.ColorDialog
    Friend WithEvents btnForeColor As System.Windows.Forms.Button
    Friend WithEvents btnBackColor As System.Windows.Forms.Button
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents pnlMask As System.Windows.Forms.Panel
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    'Friend WithEvents mskFormat As AxMSMask.AxMaskEdBox


    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMSTFlowSheet1))
        Me.txtNoofColoumn = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtFlowSheetName = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtColumnWidth = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cmbColumnNo = New System.Windows.Forms.ComboBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.grdFlowSheetDetails = New System.Windows.Forms.DataGrid()
        Me.cmbFontSize = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtColumnName = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.cmbFormat = New System.Windows.Forms.ComboBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.cmbFontName = New System.Windows.Forms.ComboBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.cmbAllinment = New System.Windows.Forms.ComboBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.chkBold = New System.Windows.Forms.CheckBox()
        Me.chkItalic = New System.Windows.Forms.CheckBox()
        Me.chkUnderline = New System.Windows.Forms.CheckBox()
        Me.ColorDialog1 = New System.Windows.Forms.ColorDialog()
        Me.btnForeColor = New System.Windows.Forms.Button()
        Me.btnBackColor = New System.Windows.Forms.Button()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.pnlMask = New System.Windows.Forms.Panel()
        Me.mskFormat = New System.Windows.Forms.MaskedTextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.tlsp_MSTFlowSheet = New gloGlobal.gloToolStripIgnoreFocus()
        Me.tls_btnAdd = New System.Windows.Forms.ToolStripButton()
        Me.tls_btnDelete = New System.Windows.Forms.ToolStripButton()
        Me.tls_btnOK = New System.Windows.Forms.ToolStripButton()
        Me.tls_btnCancel = New System.Windows.Forms.ToolStripButton()
        Me.btnBrowse = New System.Windows.Forms.Button()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.lbl_BottomBrd = New System.Windows.Forms.Label()
        Me.lbl_LeftBrd = New System.Windows.Forms.Label()
        Me.lbl_RightBrd = New System.Windows.Forms.Label()
        Me.lbl_TopBrd = New System.Windows.Forms.Label()
        Me.cmbAssociatedEMField = New System.Windows.Forms.ComboBox()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.pic_Update = New System.Windows.Forms.PictureBox()
        Me.pic_Add = New System.Windows.Forms.PictureBox()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.Panel1.SuspendLayout()
        CType(Me.grdFlowSheetDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlMask.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.tlsp_MSTFlowSheet.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.Panel5.SuspendLayout()
        CType(Me.pic_Update, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pic_Add, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel6.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtNoofColoumn
        '
        Me.txtNoofColoumn.Dock = System.Windows.Forms.DockStyle.Right
        Me.txtNoofColoumn.ForeColor = System.Drawing.Color.Black
        Me.txtNoofColoumn.Location = New System.Drawing.Point(536, 1)
        Me.txtNoofColoumn.MaxLength = 2
        Me.txtNoofColoumn.Name = "txtNoofColoumn"
        Me.txtNoofColoumn.Size = New System.Drawing.Size(30, 22)
        Me.txtNoofColoumn.TabIndex = 4
        '
        'Label2
        '
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(419, 1)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(117, 22)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "  No of Columns :"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtFlowSheetName
        '
        Me.txtFlowSheetName.Dock = System.Windows.Forms.DockStyle.Left
        Me.txtFlowSheetName.ForeColor = System.Drawing.Color.Black
        Me.txtFlowSheetName.Location = New System.Drawing.Point(128, 1)
        Me.txtFlowSheetName.MaxLength = 50
        Me.txtFlowSheetName.Name = "txtFlowSheetName"
        Me.txtFlowSheetName.Size = New System.Drawing.Size(283, 22)
        Me.txtFlowSheetName.TabIndex = 3
        '
        'Label1
        '
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(1, 1)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(127, 22)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "  Flowsheet Name :"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtColumnWidth
        '
        Me.txtColumnWidth.ForeColor = System.Drawing.Color.Black
        Me.txtColumnWidth.Location = New System.Drawing.Point(677, 12)
        Me.txtColumnWidth.MaxLength = 6
        Me.txtColumnWidth.Name = "txtColumnWidth"
        Me.txtColumnWidth.Size = New System.Drawing.Size(48, 22)
        Me.txtColumnWidth.TabIndex = 2
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Location = New System.Drawing.Point(525, 16)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(149, 14)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "Column Width (In Pixel) : "
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Location = New System.Drawing.Point(23, 16)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(74, 14)
        Me.Label4.TabIndex = 5
        Me.Label4.Text = "Column No :"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmbColumnNo
        '
        Me.cmbColumnNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbColumnNo.ForeColor = System.Drawing.Color.Black
        Me.cmbColumnNo.Location = New System.Drawing.Point(99, 12)
        Me.cmbColumnNo.Name = "cmbColumnNo"
        Me.cmbColumnNo.Size = New System.Drawing.Size(119, 22)
        Me.cmbColumnNo.TabIndex = 0
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Label18)
        Me.Panel1.Controls.Add(Me.Label19)
        Me.Panel1.Controls.Add(Me.Label20)
        Me.Panel1.Controls.Add(Me.Label21)
        Me.Panel1.Controls.Add(Me.grdFlowSheetDetails)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 181)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Padding = New System.Windows.Forms.Padding(3, 1, 3, 3)
        Me.Panel1.Size = New System.Drawing.Size(747, 356)
        Me.Panel1.TabIndex = 3
        '
        'Label18
        '
        Me.Label18.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label18.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label18.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label18.Location = New System.Drawing.Point(4, 352)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(739, 1)
        Me.Label18.TabIndex = 8
        Me.Label18.Text = "label2"
        '
        'Label19
        '
        Me.Label19.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label19.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label19.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.Location = New System.Drawing.Point(3, 2)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(1, 351)
        Me.Label19.TabIndex = 7
        Me.Label19.Text = "label4"
        '
        'Label20
        '
        Me.Label20.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label20.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label20.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label20.Location = New System.Drawing.Point(743, 2)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(1, 351)
        Me.Label20.TabIndex = 6
        Me.Label20.Text = "label3"
        '
        'Label21
        '
        Me.Label21.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label21.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label21.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.Location = New System.Drawing.Point(3, 1)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(741, 1)
        Me.Label21.TabIndex = 5
        Me.Label21.Text = "label1"
        '
        'grdFlowSheetDetails
        '
        Me.grdFlowSheetDetails.AllowSorting = False
        Me.grdFlowSheetDetails.AlternatingBackColor = System.Drawing.Color.FromArgb(CType(CType(222, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.grdFlowSheetDetails.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.grdFlowSheetDetails.BackgroundColor = System.Drawing.Color.White
        Me.grdFlowSheetDetails.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.grdFlowSheetDetails.CaptionBackColor = System.Drawing.Color.FromArgb(CType(CType(86, Byte), Integer), CType(CType(126, Byte), Integer), CType(CType(211, Byte), Integer))
        Me.grdFlowSheetDetails.CaptionForeColor = System.Drawing.Color.White
        Me.grdFlowSheetDetails.CaptionVisible = False
        Me.grdFlowSheetDetails.DataMember = ""
        Me.grdFlowSheetDetails.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdFlowSheetDetails.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.grdFlowSheetDetails.GridLineColor = System.Drawing.Color.FromArgb(CType(CType(159, Byte), Integer), CType(CType(181, Byte), Integer), CType(CType(221, Byte), Integer))
        Me.grdFlowSheetDetails.HeaderBackColor = System.Drawing.Color.FromArgb(CType(CType(86, Byte), Integer), CType(CType(126, Byte), Integer), CType(CType(211, Byte), Integer))
        Me.grdFlowSheetDetails.HeaderForeColor = System.Drawing.Color.White
        Me.grdFlowSheetDetails.LinkColor = System.Drawing.Color.FromArgb(CType(CType(254, Byte), Integer), CType(CType(207, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.grdFlowSheetDetails.Location = New System.Drawing.Point(3, 1)
        Me.grdFlowSheetDetails.Name = "grdFlowSheetDetails"
        Me.grdFlowSheetDetails.ParentRowsBackColor = System.Drawing.Color.FromArgb(CType(CType(242, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.grdFlowSheetDetails.ParentRowsForeColor = System.Drawing.Color.Black
        Me.grdFlowSheetDetails.ReadOnly = True
        Me.grdFlowSheetDetails.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(254, Byte), Integer), CType(CType(207, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.grdFlowSheetDetails.SelectionForeColor = System.Drawing.Color.Black
        Me.grdFlowSheetDetails.Size = New System.Drawing.Size(741, 352)
        Me.grdFlowSheetDetails.TabIndex = 0
        '
        'cmbFontSize
        '
        Me.cmbFontSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbFontSize.ForeColor = System.Drawing.Color.Black
        Me.cmbFontSize.Location = New System.Drawing.Point(680, 73)
        Me.cmbFontSize.Name = "cmbFontSize"
        Me.cmbFontSize.Size = New System.Drawing.Size(10, 22)
        Me.cmbFontSize.TabIndex = 7
        Me.cmbFontSize.Visible = False
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Location = New System.Drawing.Point(673, 77)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(10, 14)
        Me.Label5.TabIndex = 13
        Me.Label5.Text = "Font Size :"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Label5.Visible = False
        '
        'txtColumnName
        '
        Me.txtColumnName.ForeColor = System.Drawing.Color.Black
        Me.txtColumnName.Location = New System.Drawing.Point(326, 12)
        Me.txtColumnName.MaxLength = 50
        Me.txtColumnName.Name = "txtColumnName"
        Me.txtColumnName.Size = New System.Drawing.Size(184, 22)
        Me.txtColumnName.TabIndex = 1
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Location = New System.Drawing.Point(233, 16)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(90, 14)
        Me.Label6.TabIndex = 15
        Me.Label6.Text = "Column Name :"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmbFormat
        '
        Me.cmbFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbFormat.ForeColor = System.Drawing.Color.Black
        Me.cmbFormat.Location = New System.Drawing.Point(99, 38)
        Me.cmbFormat.Name = "cmbFormat"
        Me.cmbFormat.Size = New System.Drawing.Size(119, 22)
        Me.cmbFormat.TabIndex = 3
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Location = New System.Drawing.Point(44, 42)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(53, 14)
        Me.Label7.TabIndex = 17
        Me.Label7.Text = "Format :"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmbFontName
        '
        Me.cmbFontName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbFontName.ForeColor = System.Drawing.Color.Black
        Me.cmbFontName.Location = New System.Drawing.Point(653, 73)
        Me.cmbFontName.Name = "cmbFontName"
        Me.cmbFontName.Size = New System.Drawing.Size(14, 22)
        Me.cmbFontName.TabIndex = 6
        Me.cmbFontName.Visible = False
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Location = New System.Drawing.Point(636, 77)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(15, 14)
        Me.Label8.TabIndex = 19
        Me.Label8.Text = "Font Name :"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Label8.Visible = False
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Location = New System.Drawing.Point(253, 68)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(70, 14)
        Me.Label9.TabIndex = 21
        Me.Label9.Text = "Fore Color :"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmbAllinment
        '
        Me.cmbAllinment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbAllinment.ForeColor = System.Drawing.Color.Black
        Me.cmbAllinment.Location = New System.Drawing.Point(326, 38)
        Me.cmbAllinment.Name = "cmbAllinment"
        Me.cmbAllinment.Size = New System.Drawing.Size(88, 22)
        Me.cmbAllinment.TabIndex = 4
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Location = New System.Drawing.Point(253, 42)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(70, 14)
        Me.Label10.TabIndex = 25
        Me.Label10.Text = "Alignment :"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.Color.Transparent
        Me.Label11.Location = New System.Drawing.Point(25, 68)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(71, 14)
        Me.Label11.TabIndex = 26
        Me.Label11.Text = "Back Color :"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'chkBold
        '
        Me.chkBold.BackColor = System.Drawing.Color.Transparent
        Me.chkBold.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chkBold.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.chkBold.Location = New System.Drawing.Point(728, 75)
        Me.chkBold.Name = "chkBold"
        Me.chkBold.Size = New System.Drawing.Size(10, 18)
        Me.chkBold.TabIndex = 12
        Me.chkBold.Text = "Bold"
        Me.chkBold.UseVisualStyleBackColor = False
        Me.chkBold.Visible = False
        '
        'chkItalic
        '
        Me.chkItalic.BackColor = System.Drawing.Color.Transparent
        Me.chkItalic.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chkItalic.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.chkItalic.Location = New System.Drawing.Point(712, 75)
        Me.chkItalic.Name = "chkItalic"
        Me.chkItalic.Size = New System.Drawing.Size(10, 18)
        Me.chkItalic.TabIndex = 13
        Me.chkItalic.Text = "Italic"
        Me.chkItalic.UseVisualStyleBackColor = False
        Me.chkItalic.Visible = False
        '
        'chkUnderline
        '
        Me.chkUnderline.BackColor = System.Drawing.Color.Transparent
        Me.chkUnderline.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chkUnderline.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.chkUnderline.Location = New System.Drawing.Point(696, 75)
        Me.chkUnderline.Name = "chkUnderline"
        Me.chkUnderline.Size = New System.Drawing.Size(10, 18)
        Me.chkUnderline.TabIndex = 14
        Me.chkUnderline.Text = "Underline"
        Me.chkUnderline.UseVisualStyleBackColor = False
        Me.chkUnderline.Visible = False
        '
        'btnForeColor
        '
        Me.btnForeColor.BackColor = System.Drawing.Color.Black
        Me.btnForeColor.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnForeColor.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(127, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(158, Byte), Integer))
        Me.btnForeColor.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnForeColor.Location = New System.Drawing.Point(326, 64)
        Me.btnForeColor.Name = "btnForeColor"
        Me.btnForeColor.Size = New System.Drawing.Size(88, 22)
        Me.btnForeColor.TabIndex = 6
        Me.btnForeColor.UseVisualStyleBackColor = False
        '
        'btnBackColor
        '
        Me.btnBackColor.BackColor = System.Drawing.Color.White
        Me.btnBackColor.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnBackColor.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(127, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(158, Byte), Integer))
        Me.btnBackColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBackColor.ForeColor = System.Drawing.Color.Black
        Me.btnBackColor.Location = New System.Drawing.Point(99, 64)
        Me.btnBackColor.Name = "btnBackColor"
        Me.btnBackColor.Size = New System.Drawing.Size(127, 22)
        Me.btnBackColor.TabIndex = 5
        Me.btnBackColor.UseVisualStyleBackColor = False
        '
        'ImageList1
        '
        Me.ImageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit
        Me.ImageList1.ImageSize = New System.Drawing.Size(16, 16)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        '
        'pnlMask
        '
        Me.pnlMask.BackColor = System.Drawing.Color.Transparent
        Me.pnlMask.Controls.Add(Me.mskFormat)
        Me.pnlMask.Controls.Add(Me.Label13)
        Me.pnlMask.Controls.Add(Me.Label12)
        Me.pnlMask.Location = New System.Drawing.Point(424, 36)
        Me.pnlMask.Name = "pnlMask"
        Me.pnlMask.Size = New System.Drawing.Size(311, 26)
        Me.pnlMask.TabIndex = 30
        '
        'mskFormat
        '
        Me.mskFormat.Location = New System.Drawing.Point(57, 2)
        Me.mskFormat.Name = "mskFormat"
        Me.mskFormat.PromptChar = Global.Microsoft.VisualBasic.ChrW(35)
        Me.mskFormat.Size = New System.Drawing.Size(119, 22)
        Me.mskFormat.TabIndex = 33
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(179, 6)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(88, 14)
        Me.Label13.TabIndex = 32
        Me.Label13.Text = "(eg. #'##'' ft)"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(12, 6)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(41, 14)
        Me.Label12.TabIndex = 31
        Me.Label12.Text = "Mask :"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Panel3
        '
        Me.Panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel3.Controls.Add(Me.tlsp_MSTFlowSheet)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel3.Location = New System.Drawing.Point(0, 0)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(747, 54)
        Me.Panel3.TabIndex = 31
        '
        'tlsp_MSTFlowSheet
        '
        Me.tlsp_MSTFlowSheet.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tlsp_MSTFlowSheet.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlsp_MSTFlowSheet.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlsp_MSTFlowSheet.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tlsp_MSTFlowSheet.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tls_btnAdd, Me.tls_btnDelete, Me.tls_btnOK, Me.tls_btnCancel})
        Me.tlsp_MSTFlowSheet.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.tlsp_MSTFlowSheet.Location = New System.Drawing.Point(0, 0)
        Me.tlsp_MSTFlowSheet.Name = "tlsp_MSTFlowSheet"
        Me.tlsp_MSTFlowSheet.Size = New System.Drawing.Size(747, 53)
        Me.tlsp_MSTFlowSheet.TabIndex = 0
        Me.tlsp_MSTFlowSheet.Text = "ToolStrip1"
        '
        'tls_btnAdd
        '
        Me.tls_btnAdd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tls_btnAdd.Image = CType(resources.GetObject("tls_btnAdd.Image"), System.Drawing.Image)
        Me.tls_btnAdd.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tls_btnAdd.Name = "tls_btnAdd"
        Me.tls_btnAdd.Size = New System.Drawing.Size(37, 50)
        Me.tls_btnAdd.Tag = "Add"
        Me.tls_btnAdd.Text = "&New"
        Me.tls_btnAdd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tls_btnDelete
        '
        Me.tls_btnDelete.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tls_btnDelete.Image = CType(resources.GetObject("tls_btnDelete.Image"), System.Drawing.Image)
        Me.tls_btnDelete.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tls_btnDelete.Name = "tls_btnDelete"
        Me.tls_btnDelete.Size = New System.Drawing.Size(50, 50)
        Me.tls_btnDelete.Tag = "Delete"
        Me.tls_btnDelete.Text = "&Delete"
        Me.tls_btnDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tls_btnOK
        '
        Me.tls_btnOK.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tls_btnOK.Image = CType(resources.GetObject("tls_btnOK.Image"), System.Drawing.Image)
        Me.tls_btnOK.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tls_btnOK.Name = "tls_btnOK"
        Me.tls_btnOK.Size = New System.Drawing.Size(66, 50)
        Me.tls_btnOK.Tag = "OK"
        Me.tls_btnOK.Text = "&Save&&Cls"
        Me.tls_btnOK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tls_btnOK.ToolTipText = "Save and Close"
        '
        'tls_btnCancel
        '
        Me.tls_btnCancel.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tls_btnCancel.Image = CType(resources.GetObject("tls_btnCancel.Image"), System.Drawing.Image)
        Me.tls_btnCancel.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tls_btnCancel.Name = "tls_btnCancel"
        Me.tls_btnCancel.Size = New System.Drawing.Size(43, 50)
        Me.tls_btnCancel.Tag = "Cancel"
        Me.tls_btnCancel.Text = "&Close"
        Me.tls_btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'btnBrowse
        '
        Me.btnBrowse.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnBrowse.FlatAppearance.BorderSize = 0
        Me.btnBrowse.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnBrowse.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBrowse.Image = CType(resources.GetObject("btnBrowse.Image"), System.Drawing.Image)
        Me.btnBrowse.Location = New System.Drawing.Point(709, 1)
        Me.btnBrowse.Name = "btnBrowse"
        Me.btnBrowse.Size = New System.Drawing.Size(31, 22)
        Me.btnBrowse.TabIndex = 5
        Me.btnBrowse.UseVisualStyleBackColor = True
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Transparent
        Me.Panel4.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.Panel4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel4.Controls.Add(Me.Label2)
        Me.Panel4.Controls.Add(Me.txtNoofColoumn)
        Me.Panel4.Controls.Add(Me.Label27)
        Me.Panel4.Controls.Add(Me.btnBrowse)
        Me.Panel4.Controls.Add(Me.Label23)
        Me.Panel4.Controls.Add(Me.Label22)
        Me.Panel4.Controls.Add(Me.txtFlowSheetName)
        Me.Panel4.Controls.Add(Me.Label1)
        Me.Panel4.Controls.Add(Me.lbl_BottomBrd)
        Me.Panel4.Controls.Add(Me.lbl_LeftBrd)
        Me.Panel4.Controls.Add(Me.lbl_RightBrd)
        Me.Panel4.Controls.Add(Me.lbl_TopBrd)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel4.Location = New System.Drawing.Point(3, 3)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(741, 24)
        Me.Panel4.TabIndex = 32
        '
        'Label27
        '
        Me.Label27.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label27.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label27.Location = New System.Drawing.Point(566, 1)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(143, 22)
        Me.Label27.TabIndex = 6
        Me.Label27.Text = "Associate E&&M Fields :"
        Me.Label27.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.ForeColor = System.Drawing.Color.Red
        Me.Label23.Location = New System.Drawing.Point(424, 4)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(14, 14)
        Me.Label23.TabIndex = 2
        Me.Label23.Text = "*"
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.ForeColor = System.Drawing.Color.Red
        Me.Label22.Location = New System.Drawing.Point(3, 5)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(14, 14)
        Me.Label22.TabIndex = 1
        Me.Label22.Text = "*"
        '
        'lbl_BottomBrd
        '
        Me.lbl_BottomBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_BottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_BottomBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_BottomBrd.Location = New System.Drawing.Point(1, 23)
        Me.lbl_BottomBrd.Name = "lbl_BottomBrd"
        Me.lbl_BottomBrd.Size = New System.Drawing.Size(739, 1)
        Me.lbl_BottomBrd.TabIndex = 1
        Me.lbl_BottomBrd.Text = "label2"
        '
        'lbl_LeftBrd
        '
        Me.lbl_LeftBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_LeftBrd.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_LeftBrd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_LeftBrd.Location = New System.Drawing.Point(0, 1)
        Me.lbl_LeftBrd.Name = "lbl_LeftBrd"
        Me.lbl_LeftBrd.Size = New System.Drawing.Size(1, 23)
        Me.lbl_LeftBrd.TabIndex = 9
        Me.lbl_LeftBrd.Text = "label4"
        '
        'lbl_RightBrd
        '
        Me.lbl_RightBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_RightBrd.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_RightBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_RightBrd.Location = New System.Drawing.Point(740, 1)
        Me.lbl_RightBrd.Name = "lbl_RightBrd"
        Me.lbl_RightBrd.Size = New System.Drawing.Size(1, 23)
        Me.lbl_RightBrd.TabIndex = 8
        Me.lbl_RightBrd.Text = "label3"
        '
        'lbl_TopBrd
        '
        Me.lbl_TopBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_TopBrd.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_TopBrd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_TopBrd.Location = New System.Drawing.Point(0, 0)
        Me.lbl_TopBrd.Name = "lbl_TopBrd"
        Me.lbl_TopBrd.Size = New System.Drawing.Size(741, 1)
        Me.lbl_TopBrd.TabIndex = 7
        Me.lbl_TopBrd.Text = "label1"
        '
        'cmbAssociatedEMField
        '
        Me.cmbAssociatedEMField.FormattingEnabled = True
        Me.cmbAssociatedEMField.Location = New System.Drawing.Point(465, 69)
        Me.cmbAssociatedEMField.Name = "cmbAssociatedEMField"
        Me.cmbAssociatedEMField.Size = New System.Drawing.Size(113, 22)
        Me.cmbAssociatedEMField.TabIndex = 5
        Me.cmbAssociatedEMField.Visible = False
        '
        'Panel5
        '
        Me.Panel5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel5.Controls.Add(Me.cmbAssociatedEMField)
        Me.Panel5.Controls.Add(Me.Label25)
        Me.Panel5.Controls.Add(Me.Label24)
        Me.Panel5.Controls.Add(Me.btnBackColor)
        Me.Panel5.Controls.Add(Me.Label9)
        Me.Panel5.Controls.Add(Me.pnlMask)
        Me.Panel5.Controls.Add(Me.Label11)
        Me.Panel5.Controls.Add(Me.Label14)
        Me.Panel5.Controls.Add(Me.btnForeColor)
        Me.Panel5.Controls.Add(Me.Label15)
        Me.Panel5.Controls.Add(Me.Label16)
        Me.Panel5.Controls.Add(Me.Label17)
        Me.Panel5.Controls.Add(Me.chkUnderline)
        Me.Panel5.Controls.Add(Me.chkItalic)
        Me.Panel5.Controls.Add(Me.chkBold)
        Me.Panel5.Controls.Add(Me.Label10)
        Me.Panel5.Controls.Add(Me.Label8)
        Me.Panel5.Controls.Add(Me.Label7)
        Me.Panel5.Controls.Add(Me.txtColumnName)
        Me.Panel5.Controls.Add(Me.Label5)
        Me.Panel5.Controls.Add(Me.txtColumnWidth)
        Me.Panel5.Controls.Add(Me.Label4)
        Me.Panel5.Controls.Add(Me.Label6)
        Me.Panel5.Controls.Add(Me.Label3)
        Me.Panel5.Controls.Add(Me.cmbAllinment)
        Me.Panel5.Controls.Add(Me.cmbFontName)
        Me.Panel5.Controls.Add(Me.cmbFormat)
        Me.Panel5.Controls.Add(Me.cmbFontSize)
        Me.Panel5.Controls.Add(Me.cmbColumnNo)
        Me.Panel5.Controls.Add(Me.pic_Update)
        Me.Panel5.Controls.Add(Me.pic_Add)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel5.Location = New System.Drawing.Point(0, 84)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.Panel5.Size = New System.Drawing.Size(747, 97)
        Me.Panel5.TabIndex = 2
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.ForeColor = System.Drawing.Color.Red
        Me.Label25.Location = New System.Drawing.Point(222, 16)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(14, 14)
        Me.Label25.TabIndex = 34
        Me.Label25.Text = "*"
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.ForeColor = System.Drawing.Color.Red
        Me.Label24.Location = New System.Drawing.Point(12, 16)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(14, 14)
        Me.Label24.TabIndex = 33
        Me.Label24.Text = "*"
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label14.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label14.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label14.Location = New System.Drawing.Point(4, 93)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(739, 1)
        Me.Label14.TabIndex = 32
        Me.Label14.Text = "label2"
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label15.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label15.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(3, 1)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(1, 93)
        Me.Label15.TabIndex = 31
        Me.Label15.Text = "label4"
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label16.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label16.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label16.Location = New System.Drawing.Point(743, 1)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(1, 93)
        Me.Label16.TabIndex = 30
        Me.Label16.Text = "label3"
        '
        'Label17
        '
        Me.Label17.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label17.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label17.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.Location = New System.Drawing.Point(3, 0)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(741, 1)
        Me.Label17.TabIndex = 29
        Me.Label17.Text = "label1"
        '
        'pic_Update
        '
        Me.pic_Update.Image = CType(resources.GetObject("pic_Update.Image"), System.Drawing.Image)
        Me.pic_Update.Location = New System.Drawing.Point(10, 3)
        Me.pic_Update.Name = "pic_Update"
        Me.pic_Update.Size = New System.Drawing.Size(10, 21)
        Me.pic_Update.TabIndex = 28
        Me.pic_Update.TabStop = False
        Me.pic_Update.Visible = False
        '
        'pic_Add
        '
        Me.pic_Add.Image = CType(resources.GetObject("pic_Add.Image"), System.Drawing.Image)
        Me.pic_Add.Location = New System.Drawing.Point(9, 3)
        Me.pic_Add.Name = "pic_Add"
        Me.pic_Add.Size = New System.Drawing.Size(11, 14)
        Me.pic_Add.TabIndex = 27
        Me.pic_Add.TabStop = False
        Me.pic_Add.Visible = False
        '
        'Panel6
        '
        Me.Panel6.Controls.Add(Me.Panel4)
        Me.Panel6.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel6.Location = New System.Drawing.Point(0, 54)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Padding = New System.Windows.Forms.Padding(3)
        Me.Panel6.Size = New System.Drawing.Size(747, 30)
        Me.Panel6.TabIndex = 1
        '
        'frmMSTFlowSheet1
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(747, 537)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.Panel6)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Panel3)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmMSTFlowSheet1"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Flowsheet Details"
        Me.TopMost = True
        Me.Panel1.ResumeLayout(False)
        CType(Me.grdFlowSheetDetails, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlMask.ResumeLayout(False)
        Me.pnlMask.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.tlsp_MSTFlowSheet.ResumeLayout(False)
        Me.tlsp_MSTFlowSheet.PerformLayout()
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        Me.Panel5.ResumeLayout(False)
        Me.Panel5.PerformLayout()
        CType(Me.pic_Update, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pic_Add, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel6.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub frmMSTFlowSheet1_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        ToolTip1.Dispose()
        ToolTip1 = Nothing
    End Sub

    Private Sub frmMSTFlowSheet1_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Try
            If _IsModified And _IsSaved = False Then
                Dim _Result As DialogResult
                _Result = MessageBox.Show("Do you want to save records?", gstrMessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)
                If _Result = Windows.Forms.DialogResult.Cancel Then
                    e.Cancel = True
                ElseIf _Result = Windows.Forms.DialogResult.Yes Then
                    If OKFlowSheet() = False Then
                        e.Cancel = True
                    End If
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub frmMSTFlowSheet1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'Handle Exception Using Try-Catch Block
        Try

            'Call Function AssociatedEMField
            ''commented by Mayuri:20100616
            'AssociatedEMField()
            '' COMMENT BY SUDHIR 20090408
            ''to fill all fonts  Installed On this System
            'Fill_Fontname()
            ''to fill fonts Size Installed On this System
            'Fill_FontSize()
            ''to fill Alignments Installed On this System
            Fill_Allinment()
            Fill_Format()

            pnlMask.Visible = False

            GridFormat()
            grdFlowSheetDetails.DataSource = tblFlowSheet
            CustomGridStyle()
            grdFlowSheetDetails.AllowSorting = False

            txtColumnWidth.Text = 100

            'Check m_ID > 0
            If m_ID <> 0 Then
                grdFlowSheetDetails.ReadOnly = False

                '''' m_IsInUse = True if then selected Flow sheet has Records stored in it
                If m_IsInUse = True Then
                    '' When FlowSheet is in Use
                    cmbFormat.Enabled = False
                    cmbFormat.BackColor = Color.White
                    cmbFormat.ForeColor = Color.Black

                    ''user should not Delete it 
                    tls_btnDelete.Visible = False
                    ''user should not Change No Of Column
                    txtNoofColoumn.Enabled = False

                    txtNoofColoumn.BackColor = Color.White
                    txtNoofColoumn.ForeColor = Color.Black
                Else
                    cmbFormat.Enabled = True
                    txtNoofColoumn.Enabled = True

                    txtNoofColoumn.BackColor = Color.White
                End If

                Fill_FlowSheet(m_ID)
                grdFlowSheetDetails.ReadOnly = True
                tls_btnAdd.Visible = True
                tls_btnAdd.Text = "Add"
                tls_btnAdd.Tag = "Add"
                tls_btnAdd.Image = pic_Add.Image



                'Call retriveAssociatedName()

                'For Modify


                Dim arrstr() As String
                strAssociatedName = objFlowsheet.retriveAssociatedName(m_ID)
                arrstr = Split(strAssociatedName, "-")
                If arrstr.Length = 2 Then
                    cmbAssociatedEMField.Text = arrstr.GetValue(0)
                Else
                    If arrstr.GetValue(0) = "" Then
                        cmbAssociatedEMField.Text = "Select EM Field"
                    Else
                        cmbAssociatedEMField.Text = arrstr.GetValue(0)
                    End If

                End If

                'If strAssociatedName <> "" Then
                '    cmbAssociatedEMField.Text = strAssociatedName
                'Else
                '    cmbAssociatedEMField.Text = "Select EM Field"
                'End If
                'cmbAssociatedEMField.Text = strAssociatedName
                'btnUpdate.Visible = True
            Else
                cmbFormat.Enabled = True
                tls_btnAdd.Visible = True
                tls_btnAdd.Text = "Add"
                tls_btnAdd.Tag = "Add"
                tls_btnAdd.Image = pic_Add.Image
                'btnUpdate.Visible = False
                txtNoofColoumn.Enabled = True
                'Else
                '    Dim objfrmFlowSheetView As New frmFlowSheetView
                '    cmbCategory.Text = m_CategoryType
            End If

            _OldFlowSheetName = txtFlowSheetName.Text  ''To take for the update the old flowsheetname

            'Audit Trail
            'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.RecordViewed, "Patient Flowsheet data viewed.", gstrLoginName, gstrClientMachineName, 0, True, gloAuditTrail.enmOutCome.Success, "gloEMR")
            gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Flowsheet, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Open, "Patient Flowsheet Opened", gloAuditTrail.ActivityOutCome.Success)
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Flowsheet, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Open, "Patient Flowsheet  Opened", gloAuditTrail.ActivityOutCome.Success)

            ToolTip1.SetToolTip(Me.btnBrowse, " Associate E&M Fields ")

        Catch ex As Exception
            '   gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.RecordViewed, "Patient Flowsheet data could not be viewed.", gstrLoginName, gstrClientMachineName, 0, True, gloAuditTrail.enmOutCome.Failure, "gloEMR")
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Flowsheet, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Open, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub



    Private Sub Fill_FlowSheet(ByVal ID As Long)
        'Dim objclsFlowSheet As New clsFlowSheet
        Dim dv As New DataView
        Dim i As Integer
        Dim r As DataRow

        Try

            objclsFlowSheet.SelectFlowSheet(ID)
            dv = objclsFlowSheet.GetDataview

            'nColNumber sColumnName sFormat dWidth  sFontName  nFontSize sForeColor bIsBold bIsItalic 
            'bIsUnderline sAlignment sBackColor           
            For i = 0 To dv.Count - 1
                'Add a new row
                r = tblFlowSheet.NewRow()
                'Specify the  col name to add value for the row

                'colNo
                r("No") = dv.Item(i)(2).ToString
                'col Name
                r("Column Name") = dv.Item(i)(3).ToString
                'Format
                r("Format") = dv.Item(i)(4).ToString
                'ColWidth
                r("Width") = dv.Item(i)(5).ToString
                '' COMMENTED BY SUDHIR 20090408 ''
                'Font Name
                'r("Font Name") = dv.Item(i)(6).ToString
                ''Font Size
                'r("Font Size") = dv.Item(i)(7).ToString
                'IsBold
                'If dv.Item(i)(9).ToString = True Then
                '    r("Is Bold") = "Bold"
                'End If
                ''IsItalic
                'If dv.Item(i)(10).ToString = True Then
                '    r("Is Italic") = "Italic"
                'End If
                ''IsUnderline
                'If dv.Item(i)(11).ToString = True Then
                '    r("Is Underline") = "Underline"
                'End If
                'Allinment
                r("Allinment") = dv.Item(i)(12).ToString
                'Fore Color
                r("Font Color") = dv.Item(i)(8).ToString
                'Back Color
                r("Back Color") = dv.Item(i)(13).ToString

                tblFlowSheet.Rows.Add(r)
            Next

            grdFlowSheetDetails.DataSource = tblFlowSheet

            txtFlowSheetName.Text = dv.Item(0)(0).ToString
            txtNoofColoumn.Text = dv.Item(0)(1).ToString

            cmbColumnNo.SelectedValue = dv.Item(0)(2)
            'cmbCategory.SelectedValue = dv.Item(0)(3)
            ''Added by Mayuri:20100616-To associate multiple EM fields to flowsheet-Case No:0003710
            dtEMField = objFlowsheet.GetEMAssociatedField(ID)
            FillEMArraylist(dtEMField)
        Catch ex As Exception
            Throw ex
        Finally
            If Not IsNothing(dv) Then
                dv.Dispose()
                dv = Nothing
            End If
        End Try
    End Sub
    ''Added by Mayuri:20100616-To retrive associated fields
    Public Sub FillEMArraylist(ByVal _dt As DataTable)
        Dim oListItem As gloGeneralItem.gloItem
        'Dim Emylist As myList
        If Not IsNothing(_dt) Then
            For i As Integer = 0 To _dt.Rows.Count - 1
                ''Labs
                If _dt.Rows(i)("sAssociatedEMName") = "IncisionalBiopsyRoutine" And _dt.Rows(i)("sAssociatedEMCategory") = strLabs.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "IncisionalBiopsyRoutine"
                    oListItem.Code = strLabs.ToString()
                    _arrLabs.Add(oListItem)
                    If Not IsNothing(oListItem) Then   'obj Disposed by mitesh
                        oListItem.Dispose()
                        oListItem = Nothing
                    End If

                End If
                If _dt.Rows(i)("sAssociatedEMName") = "SuperficialBiopsyRoutine" And _dt.Rows(i)("sAssociatedEMCategory") = strLabs.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "SuperficialBiopsyRoutine"
                    oListItem.Code = strLabs.ToString()
                    _arrLabs.Add(oListItem)
                    If Not IsNothing(oListItem) Then   'obj Disposed by mitesh
                        oListItem.Dispose()
                        oListItem = Nothing
                    End If


                End If
                If _dt.Rows(i)("sAssociatedEMName") = "TypeCrossmatchRoutine" And _dt.Rows(i)("sAssociatedEMCategory") = strLabs.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "TypeCrossmatchRoutine"
                    oListItem.Code = strLabs.ToString()
                    _arrLabs.Add(oListItem)
                    If Not IsNothing(oListItem) Then   'obj Disposed by mitesh
                        oListItem.Dispose()
                        oListItem = Nothing
                    End If


                End If

                If _dt.Rows(i)("sAssociatedEMName") = "PTRoutine" And _dt.Rows(i)("sAssociatedEMCategory") = strLabs.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "PTRoutine"
                    oListItem.Code = strLabs.ToString()
                    _arrLabs.Add(oListItem)
                    If Not IsNothing(oListItem) Then   'obj Disposed by mitesh
                        oListItem.Dispose()
                        oListItem = Nothing
                    End If


                End If

                If _dt.Rows(i)("sAssociatedEMName") = "ABGsRoutine" And _dt.Rows(i)("sAssociatedEMCategory") = strLabs.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "ABGsRoutine"
                    oListItem.Code = strLabs.ToString()
                    _arrLabs.Add(oListItem)
                    If Not IsNothing(oListItem) Then   'obj Disposed by mitesh
                        oListItem.Dispose()
                        oListItem = Nothing
                    End If


                End If
                If _dt.Rows(i)("sAssociatedEMName") = "CardiacEnzymesRoutine" And _dt.Rows(i)("sAssociatedEMCategory") = strLabs.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "CardiacEnzymesRoutine"
                    oListItem.Code = strLabs.ToString()
                    _arrLabs.Add(oListItem)
                    If Not IsNothing(oListItem) Then   'obj Disposed by mitesh
                        oListItem.Dispose()
                        oListItem = Nothing
                    End If


                End If
                If _dt.Rows(i)("sAssociatedEMName") = "ChemicalProfileRoutine" And _dt.Rows(i)("sAssociatedEMCategory") = strLabs.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "ChemicalProfileRoutine"
                    oListItem.Code = strLabs.ToString()
                    _arrLabs.Add(oListItem)
                    oListItem.Dispose()


                End If
                If _dt.Rows(i)("sAssociatedEMName") = "DrugScreenRoutine" And _dt.Rows(i)("sAssociatedEMCategory") = strLabs.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "DrugScreenRoutine"
                    oListItem.Code = strLabs.ToString()
                    _arrLabs.Add(oListItem)
                    If Not IsNothing(oListItem) Then   'obj Disposed by mitesh
                        oListItem.Dispose()
                        oListItem = Nothing
                    End If


                End If
                If _dt.Rows(i)("sAssociatedEMName") = "ElectrolytesRoutine" And _dt.Rows(i)("sAssociatedEMCategory") = strLabs.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "ElectrolytesRoutine"
                    oListItem.Code = strLabs.ToString()
                    _arrLabs.Add(oListItem)
                    If Not IsNothing(oListItem) Then   'obj Disposed by mitesh
                        oListItem.Dispose()
                        oListItem = Nothing
                    End If


                End If

                If _dt.Rows(i)("sAssociatedEMName") = "BunCreatinineRoutine" And _dt.Rows(i)("sAssociatedEMCategory") = strLabs.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "BunCreatinineRoutine"
                    oListItem.Code = strLabs.ToString()
                    _arrLabs.Add(oListItem)
                    If Not IsNothing(oListItem) Then   'obj Disposed by mitesh
                        oListItem.Dispose()
                        oListItem = Nothing
                    End If


                End If
                If _dt.Rows(i)("sAssociatedEMName") = "AmylaseRoutine" And _dt.Rows(i)("sAssociatedEMCategory") = strLabs.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "AmylaseRoutine"
                    oListItem.Code = strLabs.ToString()
                    _arrLabs.Add(oListItem)
                    If Not IsNothing(oListItem) Then   'obj Disposed by mitesh
                        oListItem.Dispose()
                        oListItem = Nothing
                    End If


                End If

                If _dt.Rows(i)("sAssociatedEMName") = "PregnancyTestRoutine" And _dt.Rows(i)("sAssociatedEMCategory") = strLabs.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "PregnancyTestRoutine"
                    oListItem.Code = strLabs.ToString()
                    _arrLabs.Add(oListItem)
                    If Not IsNothing(oListItem) Then   'obj Disposed by mitesh
                        oListItem.Dispose()
                        oListItem = Nothing
                    End If


                End If
                If _dt.Rows(i)("sAssociatedEMName") = "FluStrepMonoRoutine" And _dt.Rows(i)("sAssociatedEMCategory") = strLabs.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "FluStrepMonoRoutine"
                    oListItem.Code = strLabs.ToString()
                    _arrLabs.Add(oListItem)
                    If Not IsNothing(oListItem) Then   'obj Disposed by mitesh
                        oListItem.Dispose()
                        oListItem = Nothing
                    End If


                End If

                If _dt.Rows(i)("sAssociatedEMName") = "CbcUaRoutine" And _dt.Rows(i)("sAssociatedEMCategory") = strLabs.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "CbcUaRoutine"
                    oListItem.Code = strLabs.ToString()
                    _arrLabs.Add(oListItem)
                    If Not IsNothing(oListItem) Then   'obj Disposed by mitesh
                        oListItem.Dispose()
                        oListItem = Nothing
                    End If


                End If
                If _dt.Rows(i)("sAssociatedEMName") = "IndependentVisualTest" And _dt.Rows(i)("sAssociatedEMCategory") = strLabs.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "IndependentVisualTest"
                    oListItem.Code = strLabs.ToString()
                    _arrLabs.Add(oListItem)
                    If Not IsNothing(oListItem) Then   'obj Disposed by mitesh
                        oListItem.Dispose()
                        oListItem = Nothing
                    End If


                End If
                If _dt.Rows(i)("sAssociatedEMName") = "DiscussionWPerformingPhys" And _dt.Rows(i)("sAssociatedEMCategory") = strLabs.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "DiscussionWPerformingPhys"
                    oListItem.Code = strLabs.ToString()
                    _arrLabs.Add(oListItem)
                    If Not IsNothing(oListItem) Then   'obj Disposed by mitesh
                        oListItem.Dispose()
                        oListItem = Nothing
                    End If


                End If

                If _dt.Rows(i)("sAssociatedEMName") = "OtherLabsCount" And _dt.Rows(i)("sAssociatedEMCategory") = strLabs.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "OtherLabsCount"
                    oListItem.Code = strLabs.ToString()
                    oListItem.Status = _dt.Rows(i)("sStatus").ToString()
                    _arrLabs.Add(oListItem)
                    If Not IsNothing(oListItem) Then   'obj Disposed by mitesh
                        oListItem.Dispose()
                        oListItem = Nothing
                    End If


                End If
                ''orders


                If _dt.Rows(i)("sAssociatedEMName") = "VascularStudiesWRiskRoutine" And _dt.Rows(i)("sAssociatedEMCategory") = strOrders.ToString() Then


                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "VascularStudiesWRiskRoutine"
                    oListItem.Code = strOrders.ToString()
                    _arrOrders.Add(oListItem)
                    If Not IsNothing(oListItem) Then   'obj Disposed by mitesh
                        oListItem.Dispose()
                        oListItem = Nothing
                    End If
                End If
                If _dt.Rows(i)("sAssociatedEMName") = "VascularStudiesRoutine" And _dt.Rows(i)("sAssociatedEMCategory") = strOrders.ToString() Then

                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "VascularStudiesRoutine"
                    oListItem.Code = strOrders.ToString()
                    _arrOrders.Add(oListItem)
                    If Not IsNothing(oListItem) Then   'obj Disposed by mitesh
                        oListItem.Dispose()
                        oListItem = Nothing
                    End If
                End If

                If _dt.Rows(i)("sAssociatedEMName") = "MRIRoutine" And _dt.Rows(i)("sAssociatedEMCategory") = strOrders.ToString() Then


                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "MRIRoutine"
                    oListItem.Code = strOrders.ToString()
                    _arrOrders.Add(oListItem)
                    If Not IsNothing(oListItem) Then   'obj Disposed by mitesh
                        oListItem.Dispose()
                        oListItem = Nothing
                    End If
                End If
                If _dt.Rows(i)("sAssociatedEMName") = "CATScanRoutine" And _dt.Rows(i)("sAssociatedEMCategory") = strOrders.ToString() Then


                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "CATScanRoutine"
                    oListItem.Code = strOrders.ToString()
                    _arrOrders.Add(oListItem)
                    If Not IsNothing(oListItem) Then   'obj Disposed by mitesh
                        oListItem.Dispose()
                        oListItem = Nothing
                    End If
                End If
                If _dt.Rows(i)("sAssociatedEMName") = "IVPRoutine" And _dt.Rows(i)("sAssociatedEMCategory") = strOrders.ToString() Then

                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "IVPRoutine"
                    oListItem.Code = strOrders.ToString()
                    _arrOrders.Add(oListItem)
                    If Not IsNothing(oListItem) Then   'obj Disposed by mitesh
                        oListItem.Dispose()
                        oListItem = Nothing
                    End If
                End If

                If _dt.Rows(i)("sAssociatedEMName") = "GIGallbladderRoutine" And _dt.Rows(i)("sAssociatedEMCategory") = strOrders.ToString() Then

                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "GIGallbladderRoutine"
                    oListItem.Code = strOrders.ToString()
                    _arrOrders.Add(oListItem)
                    If Not IsNothing(oListItem) Then   'obj Disposed by mitesh
                        oListItem.Dispose()
                        oListItem = Nothing
                    End If
                End If
                If _dt.Rows(i)("sAssociatedEMName") = "TLSpineRoutine" And _dt.Rows(i)("sAssociatedEMCategory") = strOrders.ToString() Then


                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "TLSpineRoutine"
                    oListItem.Code = strOrders.ToString()
                    _arrOrders.Add(oListItem)
                    If Not IsNothing(oListItem) Then   'obj Disposed by mitesh
                        oListItem.Dispose()
                        oListItem = Nothing
                    End If
                End If
                If _dt.Rows(i)("sAssociatedEMName") = "DiscographyRoutine" And _dt.Rows(i)("sAssociatedEMCategory") = strOrders.ToString() Then


                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "DiscographyRoutine"
                    oListItem.Code = strOrders.ToString()
                    _arrOrders.Add(oListItem)
                    If Not IsNothing(oListItem) Then   'obj Disposed by mitesh
                        oListItem.Dispose()
                        oListItem = Nothing
                    End If
                End If
                If _dt.Rows(i)("sAssociatedEMName") = "DiagUltrasoundRoutine" And _dt.Rows(i)("sAssociatedEMCategory") = strOrders.ToString() Then

                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "DiagUltrasoundRoutine"
                    oListItem.Code = strOrders.ToString()
                    _arrOrders.Add(oListItem)
                    If Not IsNothing(oListItem) Then   'obj Disposed by mitesh
                        oListItem.Dispose()
                        oListItem = Nothing
                    End If
                End If
                If _dt.Rows(i)("sAssociatedEMName") = "CSpineRoutine" And _dt.Rows(i)("sAssociatedEMCategory") = strOrders.ToString() Then

                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "CSpineRoutine"
                    oListItem.Code = strOrders.ToString()
                    _arrOrders.Add(oListItem)
                    If Not IsNothing(oListItem) Then   'obj Disposed by mitesh
                        oListItem.Dispose()
                        oListItem = Nothing
                    End If
                End If
                If _dt.Rows(i)("sAssociatedEMName") = "HipPelvisRoutine" And _dt.Rows(i)("sAssociatedEMCategory") = strOrders.ToString() Then


                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "HipPelvisRoutine"
                    oListItem.Code = strOrders.ToString()
                    _arrOrders.Add(oListItem)
                    If Not IsNothing(oListItem) Then   'obj Disposed by mitesh
                        oListItem.Dispose()
                        oListItem = Nothing
                    End If
                End If
                If _dt.Rows(i)("sAssociatedEMName") = "AbdomenRoutine" And _dt.Rows(i)("sAssociatedEMCategory") = strOrders.ToString() Then

                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "AbdomenRoutine"
                    oListItem.Code = strOrders.ToString()
                    _arrOrders.Add(oListItem)
                    If Not IsNothing(oListItem) Then   'obj Disposed by mitesh
                        oListItem.Dispose()
                        oListItem = Nothing
                    End If
                End If
                If _dt.Rows(i)("sAssociatedEMName") = "ExtremitiesRoutine" And _dt.Rows(i)("sAssociatedEMCategory") = strOrders.ToString() Then

                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "ExtremitiesRoutine"
                    oListItem.Code = strOrders.ToString()
                    _arrOrders.Add(oListItem)
                    If Not IsNothing(oListItem) Then   'obj Disposed by mitesh
                        oListItem.Dispose()
                        oListItem = Nothing
                    End If
                End If
                If _dt.Rows(i)("sAssociatedEMName") = "ChestRoutine" And _dt.Rows(i)("sAssociatedEMCategory") = strOrders.ToString() Then


                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "ChestRoutine"
                    oListItem.Code = strOrders.ToString()
                    _arrOrders.Add(oListItem)
                    If Not IsNothing(oListItem) Then   'obj Disposed by mitesh
                        oListItem.Dispose()
                        oListItem = Nothing
                    End If
                End If
                If _dt.Rows(i)("sAssociatedEMName") = "IndependentVisualTest" And _dt.Rows(i)("sAssociatedEMCategory") = strOrders.ToString() Then

                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "IndependentVisualTest"
                    oListItem.Code = strOrders.ToString()
                    _arrOrders.Add(oListItem)
                    If Not IsNothing(oListItem) Then   'obj Disposed by mitesh
                        oListItem.Dispose()
                        oListItem = Nothing
                    End If
                End If
                If _dt.Rows(i)("sAssociatedEMName") = "DiscussWPerformingPhys" And _dt.Rows(i)("sAssociatedEMCategory") = strOrders.ToString() Then

                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "DiscussWPerformingPhys"
                    oListItem.Code = strOrders.ToString()
                    _arrOrders.Add(oListItem)
                    If Not IsNothing(oListItem) Then   'obj Disposed by mitesh
                        oListItem.Dispose()
                        oListItem = Nothing
                    End If
                End If
                If _dt.Rows(i)("sAssociatedEMName") = "OtherXRaysCount" And _dt.Rows(i)("sAssociatedEMCategory") = strOrders.ToString() Then

                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "OtherXRaysCount"
                    oListItem.Code = strOrders.ToString()
                    oListItem.Status = _dt.Rows(i)("sStatus").ToString()
                    _arrOrders.Add(oListItem)
                    If Not IsNothing(oListItem) Then   'obj Disposed by mitesh
                        oListItem.Dispose()
                        oListItem = Nothing
                    End If


                End If



                ''''Other Diagnosis

                If _dt.Rows(i)("sAssociatedEMName") = "EndoscopeWRiskRoutine" And _dt.Rows(i)("sAssociatedEMCategory") = strOtherDiagnosis.ToString() Then


                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "EndoscopeWRiskRoutine"
                    oListItem.Code = strOtherDiagnosis.ToString()
                    _arrOtherDiag.Add(oListItem)
                    If Not IsNothing(oListItem) Then   'obj Disposed by mitesh
                        oListItem.Dispose()
                        oListItem = Nothing
                    End If
                End If
                If _dt.Rows(i)("sAssociatedEMName") = "EndoscopeRoutine" And _dt.Rows(i)("sAssociatedEMCategory") = strOtherDiagnosis.ToString() Then


                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "EndoscopeRoutine"
                    oListItem.Code = strOtherDiagnosis.ToString()
                    _arrOtherDiag.Add(oListItem)
                    If Not IsNothing(oListItem) Then   'obj Disposed by mitesh
                        oListItem.Dispose()
                        oListItem = Nothing
                    End If
                End If
                If _dt.Rows(i)("sAssociatedEMName") = "CuldocentesesRoutine" And _dt.Rows(i)("sAssociatedEMCategory") = strOtherDiagnosis.ToString() Then


                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "CuldocentesesRoutine"
                    oListItem.Code = strOtherDiagnosis.ToString()
                    _arrOtherDiag.Add(oListItem)
                    If Not IsNothing(oListItem) Then   'obj Disposed by mitesh
                        oListItem.Dispose()
                        oListItem = Nothing
                    End If
                End If
                If _dt.Rows(i)("sAssociatedEMName") = "ThoracentesisRoutine" And _dt.Rows(i)("sAssociatedEMCategory") = strOtherDiagnosis.ToString() Then


                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "ThoracentesisRoutine"
                    oListItem.Code = strOtherDiagnosis.ToString()
                    _arrOtherDiag.Add(oListItem)
                    If Not IsNothing(oListItem) Then   'obj Disposed by mitesh
                        oListItem.Dispose()
                        oListItem = Nothing
                    End If
                End If
                If _dt.Rows(i)("sAssociatedEMName") = "LumbarPunctureRoutine" And _dt.Rows(i)("sAssociatedEMCategory") = strOtherDiagnosis.ToString() Then


                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "LumbarPunctureRoutine"
                    oListItem.Code = strOtherDiagnosis.ToString()
                    _arrOtherDiag.Add(oListItem)
                    If Not IsNothing(oListItem) Then   'obj Disposed by mitesh
                        oListItem.Dispose()
                        oListItem = Nothing
                    End If
                End If
                If _dt.Rows(i)("sAssociatedEMName") = "NuclearScanRoutine" And _dt.Rows(i)("sAssociatedEMCategory") = strOtherDiagnosis.ToString() Then


                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "NuclearScanRoutine"
                    oListItem.Code = strOtherDiagnosis.ToString()
                    _arrOtherDiag.Add(oListItem)
                    If Not IsNothing(oListItem) Then   'obj Disposed by mitesh
                        oListItem.Dispose()
                        oListItem = Nothing
                    End If
                End If
                If _dt.Rows(i)("sAssociatedEMName") = "PulmonaryStudiesRoutine" And _dt.Rows(i)("sAssociatedEMCategory") = strOtherDiagnosis.ToString() Then


                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "PulmonaryStudiesRoutine"
                    oListItem.Code = strOtherDiagnosis.ToString()
                    _arrOtherDiag.Add(oListItem)
                    If Not IsNothing(oListItem) Then   'obj Disposed by mitesh
                        oListItem.Dispose()
                        oListItem = Nothing
                    End If
                End If
                If _dt.Rows(i)("sAssociatedEMName") = "DopplerFlowStudiesRoutine" And _dt.Rows(i)("sAssociatedEMCategory") = strOtherDiagnosis.ToString() Then


                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "DopplerFlowStudiesRoutine"
                    oListItem.Code = strOtherDiagnosis.ToString()
                    _arrOtherDiag.Add(oListItem)
                    If Not IsNothing(oListItem) Then   'obj Disposed by mitesh
                        oListItem.Dispose()
                        oListItem = Nothing
                    End If
                End If
                If _dt.Rows(i)("sAssociatedEMName") = "VectorcardiogramRoutine" And _dt.Rows(i)("sAssociatedEMCategory") = strOtherDiagnosis.ToString() Then


                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "VectorcardiogramRoutine"
                    oListItem.Code = strOtherDiagnosis.ToString()
                    _arrOtherDiag.Add(oListItem)
                    If Not IsNothing(oListItem) Then   'obj Disposed by mitesh
                        oListItem.Dispose()
                        oListItem = Nothing
                    End If
                End If
                If _dt.Rows(i)("sAssociatedEMName") = "EegEmgRoutine" And _dt.Rows(i)("sAssociatedEMCategory") = strOtherDiagnosis.ToString() Then


                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "EegEmgRoutine"
                    oListItem.Code = strOtherDiagnosis.ToString()
                    _arrOtherDiag.Add(oListItem)
                    If Not IsNothing(oListItem) Then   'obj Disposed by mitesh
                        oListItem.Dispose()
                        oListItem = Nothing
                    End If
                End If
                If _dt.Rows(i)("sAssociatedEMName") = "TreadmillStressTestRoutine" And _dt.Rows(i)("sAssociatedEMCategory") = strOtherDiagnosis.ToString() Then


                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "TreadmillStressTestRoutine"
                    oListItem.Code = strOtherDiagnosis.ToString()
                    _arrOtherDiag.Add(oListItem)
                    If Not IsNothing(oListItem) Then   'obj Disposed by mitesh
                        oListItem.Dispose()
                        oListItem = Nothing
                    End If
                End If
                If _dt.Rows(i)("sAssociatedEMName") = "HolterMonitorRoutine" And _dt.Rows(i)("sAssociatedEMCategory") = strOtherDiagnosis.ToString() Then


                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "HolterMonitorRoutine"
                    oListItem.Code = strOtherDiagnosis.ToString()
                    _arrOtherDiag.Add(oListItem)
                    If Not IsNothing(oListItem) Then   'obj Disposed by mitesh
                        oListItem.Dispose()
                        oListItem = Nothing
                    End If
                End If
                If _dt.Rows(i)("sAssociatedEMName") = "EkgEcgRoutine" And _dt.Rows(i)("sAssociatedEMCategory") = strOtherDiagnosis.ToString() Then


                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "EkgEcgRoutine"
                    oListItem.Code = strOtherDiagnosis.ToString()
                    _arrOtherDiag.Add(oListItem)
                    If Not IsNothing(oListItem) Then   'obj Disposed by mitesh
                        oListItem.Dispose()
                        oListItem = Nothing
                    End If
                End If
                If _dt.Rows(i)("sAssociatedEMName") = "IndependentVisualTest" And _dt.Rows(i)("sAssociatedEMCategory") = strOtherDiagnosis.ToString() Then

                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "IndependentVisualTest"
                    oListItem.Code = strOtherDiagnosis.ToString()
                    _arrOtherDiag.Add(oListItem)
                    If Not IsNothing(oListItem) Then   'obj Disposed by mitesh
                        oListItem.Dispose()
                        oListItem = Nothing
                    End If
                End If
                If _dt.Rows(i)("sAssociatedEMName") = "DiscussWPerformingPhys" And _dt.Rows(i)("sAssociatedEMCategory") = strOtherDiagnosis.ToString() Then

                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "DiscussWPerformingPhys"
                    oListItem.Code = strOtherDiagnosis.ToString()
                    _arrOtherDiag.Add(oListItem)
                    If Not IsNothing(oListItem) Then   'obj Disposed by mitesh
                        oListItem.Dispose()
                        oListItem = Nothing
                    End If
                End If

                If _dt.Rows(i)("sAssociatedEMName") = "OtherDiagnosticStudiesCount" And _dt.Rows(i)("sAssociatedEMCategory") = strOtherDiagnosis.ToString() Then

                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "OtherDiagnosticStudiesCount"
                    oListItem.Code = strOtherDiagnosis.ToString()
                    oListItem.Status = _dt.Rows(i)("sStatus").ToString()
                    _arrOtherDiag.Add(oListItem)
                    If Not IsNothing(oListItem) Then   'obj Disposed by mitesh
                        oListItem.Dispose()
                        oListItem = Nothing
                    End If
                End If


                ''management option


                If _dt.Rows(i)("sAssociatedEMName") = "DiscussCaseWHealthProvider" And _dt.Rows(i)("sAssociatedEMCategory") = strMangementOption.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "DiscussCaseWHealthProvider"
                    oListItem.Code = strMangementOption.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrManagment.Add(oListItem)
                    If Not IsNothing(oListItem) Then   'obj Disposed by mitesh
                        oListItem.Dispose()
                        oListItem = Nothing
                    End If


                End If
                If _dt.Rows(i)("sAssociatedEMName") = "ReviewMedicalRecsOther" And _dt.Rows(i)("sAssociatedEMCategory") = strMangementOption.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "ReviewMedicalRecsOther"
                    oListItem.Code = strMangementOption.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrManagment.Add(oListItem)
                    If Not IsNothing(oListItem) Then   'obj Disposed by mitesh
                        oListItem.Dispose()
                        oListItem = Nothing
                    End If


                End If

                If _dt.Rows(i)("sAssociatedEMName") = "DecisionObtainMedicalRecsOther" And _dt.Rows(i)("sAssociatedEMCategory") = strMangementOption.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "DecisionObtainMedicalRecsOther"
                    oListItem.Code = strMangementOption.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrManagment.Add(oListItem)
                    If Not IsNothing(oListItem) Then   'obj Disposed by mitesh
                        oListItem.Dispose()
                        oListItem = Nothing
                    End If


                End If

                If _dt.Rows(i)("sAssociatedEMName") = "DecisionNotResuscitate" And _dt.Rows(i)("sAssociatedEMCategory") = strMangementOption.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "DecisionNotResuscitate"
                    oListItem.Code = strMangementOption.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrManagment.Add(oListItem)
                    If Not IsNothing(oListItem) Then   'obj Disposed by mitesh
                        oListItem.Dispose()
                        oListItem = Nothing
                    End If


                End If

                If _dt.Rows(i)("sAssociatedEMName") = "MajorEmergencySurgery" And _dt.Rows(i)("sAssociatedEMCategory") = strMangementOption.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "MajorEmergencySurgery"
                    oListItem.Code = strMangementOption.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrManagment.Add(oListItem)
                    If Not IsNothing(oListItem) Then   'obj Disposed by mitesh
                        oListItem.Dispose()
                        oListItem = Nothing
                    End If

                End If
                If _dt.Rows(i)("sAssociatedEMName") = "MajorSurgeryWRiskFactors" And _dt.Rows(i)("sAssociatedEMCategory") = strMangementOption.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "MajorSurgeryWRiskFactors"
                    oListItem.Code = strMangementOption.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrManagment.Add(oListItem)
                    If Not IsNothing(oListItem) Then   'obj Disposed by mitesh
                        oListItem.Dispose()
                        oListItem = Nothing
                    End If


                End If
                If _dt.Rows(i)("sAssociatedEMName") = "MajorSurgery" And _dt.Rows(i)("sAssociatedEMCategory") = strMangementOption.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "MajorSurgery"
                    oListItem.Code = strMangementOption.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrManagment.Add(oListItem)
                    If Not IsNothing(oListItem) Then   'obj Disposed by mitesh
                        oListItem.Dispose()
                        oListItem = Nothing
                    End If


                End If
                If _dt.Rows(i)("sAssociatedEMName") = "MinorSurgeryWRiskFactors" And _dt.Rows(i)("sAssociatedEMCategory") = strMangementOption.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "MinorSurgeryWRiskFactors"
                    oListItem.Code = strMangementOption.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrManagment.Add(oListItem)
                    If Not IsNothing(oListItem) Then   'obj Disposed by mitesh
                        oListItem.Dispose()
                        oListItem = Nothing
                    End If


                End If
                If _dt.Rows(i)("sAssociatedEMName") = "MinorSurgery" And _dt.Rows(i)("sAssociatedEMCategory") = strMangementOption.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "MinorSurgery"
                    oListItem.Code = strMangementOption.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrManagment.Add(oListItem)
                    If Not IsNothing(oListItem) Then   'obj Disposed by mitesh
                        oListItem.Dispose()
                        oListItem = Nothing
                    End If


                End If

                If _dt.Rows(i)("sAssociatedEMName") = "ClosedFx" And _dt.Rows(i)("sAssociatedEMCategory") = strMangementOption.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "ClosedFx"
                    oListItem.Code = strMangementOption.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrManagment.Add(oListItem)
                    If Not IsNothing(oListItem) Then   'obj Disposed by mitesh
                        oListItem.Dispose()
                        oListItem = Nothing
                    End If


                End If
                If _dt.Rows(i)("sAssociatedEMName") = "PhysicalTherapy" And _dt.Rows(i)("sAssociatedEMCategory") = strMangementOption.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "PhysicalTherapy"
                    oListItem.Code = strMangementOption.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrManagment.Add(oListItem)
                    If Not IsNothing(oListItem) Then   'obj Disposed by mitesh
                        oListItem.Dispose()
                        oListItem = Nothing
                    End If


                End If

                If _dt.Rows(i)("sAssociatedEMName") = "NuclearMedicine" And _dt.Rows(i)("sAssociatedEMCategory") = strMangementOption.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "NuclearMedicine"
                    oListItem.Code = strMangementOption.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrManagment.Add(oListItem)
                    If Not IsNothing(oListItem) Then   'obj Disposed by mitesh
                        oListItem.Dispose()
                        oListItem = Nothing
                    End If


                End If
                If _dt.Rows(i)("sAssociatedEMName") = "RespiratoryTreatments" And _dt.Rows(i)("sAssociatedEMCategory") = strMangementOption.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "RespiratoryTreatments"
                    oListItem.Code = strMangementOption.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrManagment.Add(oListItem)
                    If Not IsNothing(oListItem) Then   'obj Disposed by mitesh
                        oListItem.Dispose()
                        oListItem = Nothing
                    End If


                End If


                If _dt.Rows(i)("sAssociatedEMName") = "Telemetry" And _dt.Rows(i)("sAssociatedEMCategory") = strMangementOption.ToString() Then

                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "Telemetry"
                    oListItem.Code = strMangementOption.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrManagment.Add(oListItem)
                    If Not IsNothing(oListItem) Then   'obj Disposed by mitesh
                        oListItem.Dispose()
                        oListItem = Nothing
                    End If
                End If

                'End If
                If _dt.Rows(i)("sAssociatedEMName") = "HighRiskMeds" And _dt.Rows(i)("sAssociatedEMCategory") = strMangementOption.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "HighRiskMeds"
                    oListItem.Code = strMangementOption.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrManagment.Add(oListItem)
                    If Not IsNothing(oListItem) Then   'obj Disposed by mitesh
                        oListItem.Dispose()
                        oListItem = Nothing
                    End If


                End If

                If _dt.Rows(i)("sAssociatedEMName") = "IVMedsWAdditives" And _dt.Rows(i)("sAssociatedEMCategory") = strMangementOption.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "IVMedsWAdditives"
                    oListItem.Code = strMangementOption.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrManagment.Add(oListItem)
                    If Not IsNothing(oListItem) Then   'obj Disposed by mitesh
                        oListItem.Dispose()
                        oListItem = Nothing
                    End If


                End If

                If _dt.Rows(i)("sAssociatedEMName") = "IVMeds" And _dt.Rows(i)("sAssociatedEMCategory") = strMangementOption.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "IVMeds"
                    oListItem.Code = strMangementOption.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrManagment.Add(oListItem)
                    If Not IsNothing(oListItem) Then   'obj Disposed by mitesh
                        oListItem.Dispose()
                        oListItem = Nothing
                    End If

                End If

                If _dt.Rows(i)("sAssociatedEMName") = "PrescripIMMeds" And _dt.Rows(i)("sAssociatedEMCategory") = strMangementOption.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "PrescripIMMeds"
                    oListItem.Code = strMangementOption.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrManagment.Add(oListItem)
                    If Not IsNothing(oListItem) Then   'obj Disposed by mitesh
                        oListItem.Dispose()
                        oListItem = Nothing
                    End If


                End If

                If _dt.Rows(i)("sAssociatedEMName") = "OverCounterMeds" And _dt.Rows(i)("sAssociatedEMCategory") = strMangementOption.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "OverCounterMeds"
                    oListItem.Code = strMangementOption.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrManagment.Add(oListItem)
                    If Not IsNothing(oListItem) Then   'obj Disposed by mitesh
                        oListItem.Dispose()
                        oListItem = Nothing
                    End If


                End If

                If _dt.Rows(i)("sAssociatedEMName") = "ConfWPatientFamilyMinutes" And _dt.Rows(i)("sAssociatedEMCategory") = strMangementOption.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "ConfWPatientFamilyMinutes"
                    oListItem.Code = strMangementOption.ToString()
                    oListItem.Status = _dt.Rows(i)("sStatus").ToString()
                    _arrManagment.Add(oListItem)
                    If Not IsNothing(oListItem) Then   'obj Disposed by mitesh
                        oListItem.Dispose()
                        oListItem = Nothing
                    End If


                End If

            Next
        End If
    End Sub


    Private Sub Fill_Fontname()
        Dim ff As FontFamily() = FontFamily.Families

        ' Loop and create a sample of each font.
        Dim i As Integer

        Try


            For i = 0 To ff.Length - 1
                Dim font As Font = Nothing
                Dim size As Size = Nothing
                ' Create the font - based on the styles available.

                If ff(i).IsStyleAvailable(FontStyle.Regular) Then
                    font = New System.Drawing.Font(ff(i).Name, cmbFontName.Font.Size)

                ElseIf ff(i).IsStyleAvailable(FontStyle.Bold) Then
                    font = New System.Drawing.Font(ff(i).Name, cmbFontName.Font.Size, FontStyle.Bold)
                ElseIf ff(i).IsStyleAvailable(FontStyle.Italic) Then
                    font = New System.Drawing.Font(ff(i).Name, cmbFontName.Font.Size, FontStyle.Italic)
                ElseIf ff(i).IsStyleAvailable(FontStyle.Strikeout) Then
                    font = New System.Drawing.Font(ff(i).Name, cmbFontName.Font.Size, FontStyle.Strikeout)
                ElseIf ff(i).IsStyleAvailable(FontStyle.Underline) Then
                    font = New System.Drawing.Font(ff(i).Name, cmbFontName.Font.Size, FontStyle.Underline)
                End If
                ' Should we add the item?

                If Not (font Is Nothing) Then
                    cmbFontName.Items.Add(font.Name)
                    font.Dispose()
                    font = Nothing
                End If
            Next  ' End for all the fonts.
            ff = Nothing
            cmbFontName.SelectedIndex = 1
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub Fill_FontSize()
        Dim i As Integer

        Try


            cmbFontSize.Items.Clear()
            For i = 8 To 12
                cmbFontSize.Items.Add(i)
            Next

            For i = 14 To 28 Step 2
                cmbFontSize.Items.Add(i)
            Next

            cmbFontSize.Items.Add("36")
            cmbFontSize.Items.Add("48")
            cmbFontSize.Items.Add("72")

            cmbFontSize.SelectedIndex = 0

            'Dim m_color As New Color

            ' cmbForeColor.Items.Add(m_color)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub Fill_Format()
        Try


            cmbFormat.Items.Clear()

            cmbFormat.Items.Add("General") '' String
            cmbFormat.Items.Add("-1,234.00") ''Double

            cmbFormat.Items.Add("1234") ''Int64
            cmbFormat.Items.Add("1234.00") ''Double()

            cmbFormat.Items.Add("(1,234.00)") ''Double
            cmbFormat.Items.Add("-1234.00") ''Double
            cmbFormat.Items.Add("(1234.00)") ''Double
            cmbFormat.Items.Add("Percentage") '' Masked
            cmbFormat.Items.Add("-$1234.00")
            cmbFormat.Items.Add("$1,234.00")
            cmbFormat.Items.Add("Percentage")

            cmbFormat.Items.Add("MM/DD/YYYY") '' DateTime
            cmbFormat.Items.Add("DD/MM/YYYY")
            cmbFormat.Items.Add("MMMM DD/YYYY")
            cmbFormat.Items.Add("DD/MMMM/YYYY")
            cmbFormat.Items.Add("HH:MM:ss")
            cmbFormat.Items.Add("HH:MM")
            cmbFormat.Items.Add("HH:MM:ss PM")
            cmbFormat.Items.Add("HH:MM PM")
            cmbFormat.Items.Add("Masked")

            cmbFormat.SelectedIndex = 0
        Catch ex As Exception
            Throw ex
        End Try

    End Sub
    Public Sub Fill_Allinment()
        Try


            cmbAllinment.Items.Clear()
            cmbAllinment.Items.Add("Left")
            cmbAllinment.Items.Add("Center")
            cmbAllinment.Items.Add("Right")
            cmbAllinment.SelectedIndex = 0
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub txtNoofColoumn_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtNoofColoumn.TextChanged
        Try
            'If blnEdit = False Then
            'Fill_ColNo()
            ' End If
            _IsModified = True
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Flowsheet, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.None, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)

            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Fill_ColNo()
        Try


            'On Error Resume Next

            Dim i As Integer
            Dim Max As Integer = 0

            cmbColumnNo.DropDownStyle = ComboBoxStyle.DropDownList

            For i = 0 To tblFlowSheet.Rows.Count - 1
                'Max = tblFlowSheet.Rows(i)(0)
                If Max < tblFlowSheet.Rows(i)(0) Then
                    Max = tblFlowSheet.Rows(i)(0)
                End If
            Next

            If Val(txtNoofColoumn.Text) <= 0 Then
                Exit Sub
            End If

            If Val(txtNoofColoumn.Text) < Max Then
                MessageBox.Show("Number of columns must not be less than  " & Max, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                cmbColumnNo.Items.Clear()
                txtNoofColoumn.Text = Max
                Exit Sub
            End If

            cmbColumnNo.Items.Clear()
            For i = 1 To Val(txtNoofColoumn.Text)
                cmbColumnNo.Items.Add(i)
            Next

            Dim j As Integer
            For i = 0 To tblFlowSheet.Rows.Count - 1
                'SLR: Changed on 4/2/2014
                Dim ithObject As Object = tblFlowSheet.Rows(i)(0)
                For j = cmbColumnNo.Items.Count - 1 To 0 Step -1
                    cmbColumnNo.SelectedIndex = j
                    If ithObject = cmbColumnNo.SelectedItem Then
                        cmbColumnNo.Items.RemoveAt(cmbColumnNo.SelectedIndex)
                        'Exit For
                    End If
                    'If cmbColumnNo.Items.Count <= j + 1 Then
                    '    Exit For
                    'End If
                Next
            Next

            If cmbColumnNo.Items.Count > 0 Then
                cmbColumnNo.SelectedIndex = 0
            Else
                cmbColumnNo.Text = ""
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub



    Private Sub AddFlowSheet()

        Dim _tempflag As Boolean = False
        Dim _currentIndex As Integer = -1
        Try
            _tempflag = blnEdit

            Dim r As DataRow
            If cmbColumnNo.Text = "" Then
                MessageBox.Show("Column No is Invalid", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If
            If Trim(txtColumnName.Text) = "" Then
                MessageBox.Show("Column Name must be Entered", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtColumnName.Focus()
                Exit Sub
            End If
            If Trim(txtColumnWidth.Text) = "" Then
                MessageBox.Show("Column width must be Entered", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtColumnWidth.Focus()
                Exit Sub
            End If

            '' SUDHIR 20090704 '' VALIDATION FOR REPEAT COLUMN ''
            For iRow As Integer = 0 To tblFlowSheet.Rows.Count - 1
                If tblFlowSheet.Rows(iRow)("Column Name") = txtColumnName.Text.Trim And txtColumnName.Text.Trim <> _TempColumnName Then
                    MessageBox.Show("Column Name already present in flowsheet", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            Next
            '' END SUDHIR ''

            If grdFlowSheetDetails.CurrentRowIndex > -1 And tblFlowSheet.Rows.Count >= 1 Then
                'Add a new row
                If blnEdit = True Then
                    r = tblFlowSheet.Rows(grdFlowSheetDetails.CurrentRowIndex)
                Else
                    r = tblFlowSheet.NewRow()
                End If

            Else
                tblFlowSheet.Clear()
                r = tblFlowSheet.NewRow()
            End If



            'Specify the  col name to add value for the row
            r("No") = cmbColumnNo.Text
            r("Column Name") = txtColumnName.Text

            If cmbFormat.Text = "Masked" Then
                r("Format") = Trim(mskFormat.Text)
                mskFormat.Mask = ""
                mskFormat.Text = ""
            Else
                r("Format") = cmbFormat.Text
            End If

            r("Width") = txtColumnWidth.Text
            'r("Font Name") = "" 'cmbFontName.Text

            'r("Font Size") = 0 'cmbFontSize.Text

            'If chkBold.CheckState = CheckState.Checked Then
            '    r("Is Bold") = "Bold"
            'Else
            '    r("Is Bold") = ""
            'End If

            'If chkItalic.CheckState = CheckState.Checked Then
            '    r("Is Italic") = "Italic"
            'Else
            '    r("Is Italic") = ""
            'End If

            'If chkUnderline.CheckState = CheckState.Checked Then
            '    r("Is Underline") = "Underline"
            'Else
            '    r("Is Underline") = ""
            'End If
            r("Allinment") = cmbAllinment.Text
            r("Font Color") = CInt(btnForeColor.BackColor.ToArgb)     '10)

            'r("Font Color") = Color.FromName(picForeColor.BackColor.ToString)         '10
            r("Back Color") = CInt(btnBackColor.BackColor.ToArgb)   '11

            'Add the row to the tabledata
            
            If blnEdit = False Then
                tblFlowSheet.Rows.Add(r)
            End If
            grdFlowSheetDetails.DataSource = tblFlowSheet

            ''''''''''''''
            '' when column No gets added to grid it gets removed from combo box
            cmbColumnNo.Items.Remove(cmbColumnNo.SelectedItem)
            '''''''''''''
            If cmbColumnNo.Items.Count > 0 Then
                cmbColumnNo.SelectedIndex = 0
                txtColumnName.Focus()
                ' btnAdd.Visible = True
            Else
                cmbColumnNo.Text = ""
                'btnAdd.Visible = False
            End If

            cmbColumnNo.Enabled = True


            grdFlowSheetDetails.Enabled = True
            If blnEdit = True Then
                Reset()
            End If
            blnEdit = False
            tls_btnAdd.Visible = True
            tls_btnAdd.Text = "Add"
            tls_btnAdd.Tag = "Add"
            tls_btnAdd.Image = pic_Add.Image

        Catch ex As Exception
            If _tempflag = False Then
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Flowsheet, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)

            Else
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Flowsheet, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Modify, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)

            End If
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
    End Sub


    Private Sub DeleteFlowSheet()
        Try
            If grdFlowSheetDetails.CurrentRowIndex > -1 Then
                If MessageBox.Show("Are you sure to Delete Details of Column " & grdFlowSheetDetails.Item(grdFlowSheetDetails.CurrentRowIndex, 1).ToString & "?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    tblFlowSheet.Rows.RemoveAt(grdFlowSheetDetails.CurrentRowIndex)
                    grdFlowSheetDetails.DataSource = tblFlowSheet
                    Call Reset()
                    cmbColumnNo.Enabled = True
                    blnEdit = False
                    tls_btnAdd.Visible = True
                    tls_btnAdd.Text = "Add"
                    tls_btnAdd.Tag = "Add"
                    tls_btnAdd.Image = pic_Add.Image
                    grdFlowSheetDetails.Enabled = True
                    'For Delete
                    objFlowsheet.deleteAssociatedName(m_ID)
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Flowsheet, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Delete, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Function OKFlowSheet() As Boolean
        Try
            If txtFlowSheetName.Text = "" Then
                MessageBox.Show("Flowsheet Name must be entered", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtFlowSheetName.Focus()
                OKFlowSheet = Nothing
                Exit Function
            End If

            If txtNoofColoumn.Text = "" Then
                MessageBox.Show("Number of columns must be entered", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtNoofColoumn.Focus()
                OKFlowSheet = Nothing
                Exit Function
            End If

            If cmbColumnNo.Items.Count > 0 Then
                MessageBox.Show("Please complete all columns", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                cmbColumnNo.Focus()
                OKFlowSheet = Nothing
                Exit Function
            End If

            If objclsFlowSheet.CheckDuplicate(m_ID, Trim(txtFlowSheetName.Text)) = True Then
                MessageBox.Show("Flowsheet Name already exists", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtFlowSheetName.Focus()
                OKFlowSheet = Nothing
                Exit Function
            End If


            '''' solving issue tfs id - 15523 for 6031
            Dim i As Integer
            Dim Max As Integer = 0
            For i = 0 To tblFlowSheet.Rows.Count - 1
                If Max < tblFlowSheet.Rows(i)(0) Then
                    Max = tblFlowSheet.Rows(i)(0)
                End If
            Next

            If Val(txtNoofColoumn.Text) < Max Then
                MessageBox.Show("Number of columns must not be less than " & Max, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                cmbColumnNo.Items.Clear()
                txtNoofColoumn.Text = Max
                OKFlowSheet = Nothing
                Exit Function
            End If

            ''''end code


            '''' solving issue tfs id - 15538 for 6031

            If Val(txtNoofColoumn.Text) > Max Then
                MessageBox.Show("Number of columns must not be greater than " & Max, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                cmbColumnNo.Items.Clear()
                txtNoofColoumn.Text = Max
                OKFlowSheet = Nothing
                Exit Function
            End If

            '''' end code 

            '' COMMENT BY SUDHIR 20090723 ''
            'If objclsFlowSheet.CheckIsUsed(Trim(txtFlowSheetName.Text)) = True Then
            '    MessageBox.Show("Flow Sheet Name already exists against patient.", "Flow Sheet", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    txtFlowSheetName.Focus()
            '    Exit Function
            'End If
            '' END SUDHIR ''
            '''''''''''''''''
            If Trim(txtColumnName.Text) <> "" And Val(txtColumnWidth.Text) <> 0 And Val(cmbColumnNo.Text) <> 0 Then
                Dim r As DataRow

                If grdFlowSheetDetails.CurrentRowIndex > -1 Then
                    tblFlowSheet.Rows.RemoveAt(grdFlowSheetDetails.CurrentRowIndex)
                End If

                r = tblFlowSheet.NewRow()

                r("No") = cmbColumnNo.Text '0
                r("Column Name") = txtColumnName.Text '1

                If cmbFormat.Text = "Masked" Then
                    r("Format") = Trim(mskFormat.Text)    '2
                    mskFormat.Mask = ""
                    mskFormat.Text = ""
                Else
                    r("Format") = cmbFormat.Text
                End If

                r("Width") = txtColumnWidth.Text '3
                'r("Font Name") = cmbFontName.Text  '4

                'r("Font Size") = cmbFontSize.Text '5

                'If chkBold.CheckState = CheckState.Checked Then
                '    r("Is Bold") = "Bold"  ''6
                'Else
                '    r("Is Bold") = ""
                '    'r("Is Bold") = CheckState.Checked
                'End If

                'If chkItalic.CheckState = CheckState.Checked Then
                '    r("Is Italic") = "Italic"  '7
                'Else
                '    r("Is Italic") = ""
                'End If

                'If chkUnderline.CheckState = CheckState.Checked Then
                '    r("Is Underline") = "Underline"  '8
                'Else
                '    r("Is Underline") = ""
                'End If
                r("Allinment") = cmbAllinment.Text  '9
                r("Font Color") = CInt(btnForeColor.BackColor.ToArgb)   '10
                r("Back Color") = CInt(btnBackColor.BackColor.ToArgb)  '11

                ''''''''''''''
                '' when column No gets added to grid it gets removed from combobox
                cmbColumnNo.Items.Remove(cmbColumnNo.SelectedItem)
                '''''''''''''
                If cmbColumnNo.Items.Count > 0 Then
                    cmbColumnNo.SelectedIndex = 0
                Else
                    cmbColumnNo.Text = ""
                End If

                tblFlowSheet.Rows.Add(r)
            End If
            ''''''''

            'If frmViewFlowSheet.blnModify = True Then
            '    objclsFlowSheet.DeleteFlowSheet(m_ID, txtFlowSheetName.Text)
            'End If

            'Dim i As Integer
            'For i = 0 To tblFlowSheet.Rows.Count - 1
            'grdFlowSheetDetails.CurrentRowIndex = i
            '                                                                                       colNo,                                  colName,                                    colFormat,                             colWidth,                                   colFontName,                          colFontSize,                            colIsBold(,                             colIsItalic,                             colIsUnderline,                          colAllinment,                               colFontColour,                          colBackColor)
            'objclsFlowSheet.AddNewFlowSheet(i, m_ID, Trim(txtFlowSheetName.Text), Trim(txtNoofColoumn.Text), grdFlowSheetDetails.Item(i, 0).ToString, grdFlowSheetDetails.Item(i, 1).ToString, grdFlowSheetDetails.Item(i, 2).ToString, grdFlowSheetDetails.Item(i, 3).ToString, grdFlowSheetDetails.Item(i, 4).ToString, grdFlowSheetDetails.Item(i, 5).ToString, grdFlowSheetDetails.Item(i, 6).ToString, grdFlowSheetDetails.Item(i, 7).ToString, grdFlowSheetDetails.Item(i, 8).ToString, grdFlowSheetDetails.Item(i, 9).ToString, grdFlowSheetDetails.Item(i, 10).ToString, grdFlowSheetDetails.Item(i, 11).ToString)
            If tblFlowSheet.Rows.Count <= 0 Then
                MessageBox.Show("Flowsheet does not contains proper information ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtColumnName.Focus()
                OKFlowSheet = Nothing
                Exit Function
            End If
            'Dim strAssociatedProperty As String
            'Dim strAssociatedCategory As String
            'If cmbAssociatedEMField.Text <> "Select EM Field" Then
            '    strAssociatedProperty = cmbAssociatedEMField.Text
            '    If cmbAssociatedEMField.SelectedValue = CategoryType.Labs Then
            '        strAssociatedCategory = strLabs.ToString()
            '    ElseIf cmbAssociatedEMField.SelectedValue = CategoryType.X_Ray_Radiology Then
            '        strAssociatedCategory = strOrders.ToString()
            '    ElseIf cmbAssociatedEMField.SelectedValue = CategoryType.Other_Diagonsis_Tests Then
            '        strAssociatedCategory = strOtherDiagnosis.ToString()
            '    End If
            'Else
            '    strAssociatedProperty = ""
            '    strAssociatedCategory = ""
            'End If
            'objclsFlowSheet.AddNewFlowSheet(m_ID, Trim(txtFlowSheetName.Text), Trim(txtNoofColoumn.Text), tblFlowSheet, strAssociatedProperty, strAssociatedCategory, _arrLabs, _arrOrders, _arrOtherDiag, _arrManagment)
            objclsFlowSheet.AddNewFlowSheet(m_ID, Trim(txtFlowSheetName.Text), Trim(txtNoofColoumn.Text), tblFlowSheet, _arrLabs, _arrOrders, _arrOtherDiag, _arrManagment)

            _FlowSheetName = Trim(txtFlowSheetName.Text)            ''dhruv 20100218 To take the change name 

            '' to update templates
            If _OldFlowSheetName <> "" And _OldFlowSheetName <> _FlowSheetName And m_IsInUse = False Then
                Dim isUpdateTemplate As Boolean = False

                _OldFlowSheetName = "FlowSheet1.sFlowSheetName|" & _OldFlowSheetName        ''formfield value for the flowsheet oldname
                _FlowSheetName = "FlowSheet1.sFlowSheetName|" & _FlowSheetName              ''formfield value for the flowsheet newname

                If MessageBox.Show("Do you want to update existing templates?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then ''When changing tthe the flowsheet field against the history+ROS+flowsheet
                    Dim ofrm As New frmUpdateTemplates(_OldFlowSheetName, _FlowSheetName, False)
                    ofrm.ShowDialog(IIf(IsNothing(ofrm.Parent), Me, ofrm.Parent))
                    If Not IsNothing(ofrm) Then   'obj Disposed by mitesh
                        ofrm.Dispose()
                        ofrm = Nothing
                    End If
                End If
            End If
            ''end----------------Dhruv

            'Next
            _FlowSheetName = Trim(txtFlowSheetName.Text)
            _Cancel = False
            _IsSaved = True
            Return True

        Catch ex As Exception
            If m_ID = 0 Then
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Flowsheet, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Else
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Flowsheet, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Modify, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            End If

            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End Try
    End Function
    Private Sub CancelFlowsheet()
        Try
            _Cancel = True
            Me.Close()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Flowsheet, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)

        End Try
    End Sub

    Private Sub Reset()
        txtColumnName.Text = ""
        cmbFormat.SelectedIndex = 0
        'cmbFontName.SelectedIndex = 1
        txtColumnWidth.Text = 100
        'cmbFontSize.SelectedIndex = 0
        chkBold.CheckState = CheckState.Unchecked
        chkItalic.CheckState = CheckState.Unchecked
        chkUnderline.CheckState = CheckState.Unchecked
        cmbAllinment.SelectedIndex = 0
        btnForeColor.BackColor = Color.Black
        btnBackColor.BackColor = Color.White
        Call Fill_ColNo()
    End Sub
    Private Sub GridFormat()
        ' Dim tblFlowSheet As New DataTable

        Dim colNo As New DataColumn
        Dim colName As New DataColumn
        Dim colFormat As New DataColumn
        Dim colWidth As New DataColumn
        'Dim colFontName As New DataColumn
        'Dim colFontSize As New DataColumn
        Dim colFontColour As New DataColumn
        'Dim colIsBold As New DataColumn
        'Dim colIsItalic As New DataColumn
        'Dim colIsUnderline As New DataColumn
        Dim colAllinment As New DataColumn
        Dim colBackColor As New DataColumn

        colNo.ColumnName = "No"  '0
        colName.ColumnName = "Column Name"  '1
        colFormat.ColumnName = "Format"  '2
        colWidth.ColumnName = "Width"  '3
        'colFontName.ColumnName = "Font Name"  '4
        'colFontSize.ColumnName = "Font Size"  '5
        'colIsBold.ColumnName = "Is Bold"  '6
        '' colIsBold.DataType = GetType(Boolean)
        'colIsItalic.ColumnName = "Is Italic"  '7
        '' colIsItalic.DataType = GetType(Boolean)
        'colIsUnderline.ColumnName = "Is Underline"  '8
        ' colIsUnderline.DataType = GetType(Boolean)
        colAllinment.ColumnName = "Allinment"  '9
        colFontColour.ColumnName = "Font Color"  '10
        colBackColor.ColumnName = "Back Color"  '11

        'tblFlowSheet.Columns.AddRange(New DataColumn() {colNo, colName, colFormat, colWidth, colFontName, colFontSize, colIsBold, colIsItalic, colIsUnderline, colAllinment, colFontColour, colBackColor})
        tblFlowSheet.Columns.AddRange(New DataColumn() {colNo, colName, colFormat, colWidth, colAllinment, colFontColour, colBackColor})

    End Sub

    Private Sub grdFlowSheetDetails_MouseDoubleClick(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles grdFlowSheetDetails.MouseDoubleClick
        Try


            Dim i As Integer

            If (grdFlowSheetDetails.HitTest(e.X, e.Y).Row >= 0) Then



                If grdFlowSheetDetails.CurrentRowIndex > -1 Then

                    blnEdit = True

                    i = grdFlowSheetDetails.CurrentRowIndex

                    'colNo
                    cmbColumnNo.DropDownStyle = ComboBoxStyle.DropDown
                    cmbColumnNo.Text = grdFlowSheetDetails.Item(i, 0).ToString
                    cmbColumnNo.Enabled = False

                    ' cmbColumnNo.DropDownStyle = ComboBoxStyle.DropDownList
                    'colName
                    txtColumnName.Text = grdFlowSheetDetails.Item(i, 1).ToString
                    _TempColumnName = txtColumnName.Text

                    ''*********
                    'colFormat
                    Dim j As Integer
                    Dim blnIsMasked As Boolean = True
                    ''to Check that Format is Masked or Any Other 
                    For j = 0 To cmbFormat.Items.Count - 1
                        If grdFlowSheetDetails.Item(i, 2).ToString = cmbFormat.Items(j).ToString Then
                            cmbFormat.Text = grdFlowSheetDetails.Item(i, 2).ToString
                            ''''if  Format is not "Masked" then
                            blnIsMasked = False
                            Exit For
                        End If
                    Next

                    If blnIsMasked = True Then
                        '''''IF Format is "Masked" then Select Masked in cmbFormat  
                        cmbFormat.SelectedItem = "Masked"
                        '' and Write MAsk Format in mskFormat
                        mskFormat.Text = grdFlowSheetDetails.Item(i, 2).ToString
                    End If
                    ''*********

                    'colWidth
                    txtColumnWidth.Text = grdFlowSheetDetails.Item(i, 3).ToString

                    cmbAllinment.Text = grdFlowSheetDetails.Item(i, 4).ToString
                    btnForeColor.BackColor = Color.FromArgb(grdFlowSheetDetails.Item(i, 5))
                    btnBackColor.BackColor = Color.FromArgb(grdFlowSheetDetails.Item(i, 6))


                    grdFlowSheetDetails.Enabled = False

                    tls_btnAdd.Visible = True
                    tls_btnAdd.Text = "Update"
                    tls_btnAdd.Tag = "Update"
                    tls_btnAdd.Image = pic_Update.Image
                    txtColumnName.Focus()

                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Flowsheet, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.None, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)

            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub grdFlowSheetDetails_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grdFlowSheetDetails.MouseUp

        If (grdFlowSheetDetails.CurrentRowIndex) > -1 Then
            grdFlowSheetDetails.Select(grdFlowSheetDetails.CurrentRowIndex)
        End If
    End Sub

    Public Sub CustomGridStyle()

        Dim ts As New clsDataGridTableStyle(tblFlowSheet.TableName.ToString)


        Dim colNo As New DataGridTextBoxColumn
        With colNo
            .Width = 85
            .MappingName = tblFlowSheet.Columns(0).ColumnName
            .HeaderText = "Column No"
            .NullText = ""
        End With

        Dim colName As New DataGridTextBoxColumn
        With colName
            .Width = 180
            .MappingName = tblFlowSheet.Columns(1).ColumnName
            .HeaderText = "Column Name"
            .NullText = ""
        End With

        Dim colFormat As New DataGridTextBoxColumn
        With colFormat
            .Width = 150
            .MappingName = tblFlowSheet.Columns(2).ColumnName
            .HeaderText = "Format"
            .NullText = ""
        End With

        Dim colWidth As New DataGridTextBoxColumn
        With colWidth
            .Width = 75
            .MappingName = tblFlowSheet.Columns(3).ColumnName
            .HeaderText = "Width"
            .NullText = ""
        End With


        Dim Allinment As New DataGridTextBoxColumn
        With Allinment
            .Width = 75
            .MappingName = tblFlowSheet.Columns(4).ColumnName
            .HeaderText = "Alignment "
            .NullText = ""
        End With

        Dim ForeColour As New DataGridTextBoxColumn
        With ForeColour
            .Width = 85
            .MappingName = tblFlowSheet.Columns(5).ColumnName
            .HeaderText = "Fore Color"
            .NullText = ""
        End With

        Dim BackColour As New DataGridTextBoxColumn
        With BackColour
            .Width = 85
            .MappingName = tblFlowSheet.Columns(6).ColumnName
            .HeaderText = "Back Color"
            .NullText = ""
        End With

        'ts.GridColumnStyles.AddRange(New DataGridColumnStyle() {colNo, colName, colFormat, colWidth, FontName, FontSize, IsBold, IsItalic, IsUnderline, Allinment, ForeColour, BackColour})
        ts.GridColumnStyles.AddRange(New DataGridColumnStyle() {colNo, colName, colFormat, colWidth, Allinment, ForeColour, BackColour})
        grdFlowSheetDetails.TableStyles.Clear()
        grdFlowSheetDetails.TableStyles.Add(ts)

    End Sub

    Private Sub txtNoofColoumn_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtNoofColoumn.KeyPress
        If Not ((e.KeyChar >= ChrW(48) AndAlso e.KeyChar <= ChrW(57)) OrElse (e.KeyChar = ChrW(8))) Then
            e.Handled = True
        End If
        Exit Sub
    End Sub

    Private Sub txtColumnWidth_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtColumnWidth.KeyPress
        If Not ((e.KeyChar >= ChrW(48) AndAlso e.KeyChar <= ChrW(57)) OrElse (e.KeyChar = ChrW(8))) Then
            e.Handled = True
        End If
        Exit Sub
    End Sub

    Private Sub txtNoofColoumn_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtNoofColoumn.Validating
        Try
            'If blnEdit = False Then
            Fill_ColNo()
            'End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnForeColor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnForeColor.Click
        Try
            With ColorDialog1
                .AllowFullOpen = True
                .ShowHelp = False
                .Color = btnForeColor.BackColor
                Try
                    .CustomColors = gloGlobal.gloCustomColor.customColor
                Catch ex As Exception

                End Try
                If .ShowDialog(System.Windows.Forms.Form.ActiveForm) = DialogResult.OK Then
                    btnForeColor.BackColor = .Color
                    Try
                        gloGlobal.gloCustomColor.customColor = .CustomColors
                    Catch ex As Exception

                    End Try
                End If
            End With
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Flowsheet, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Delete, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)

            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnBackColor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBackColor.Click
        Try
            With ColorDialog1
                .AllowFullOpen = True
                .ShowHelp = False
                .Color = btnBackColor.BackColor
                Try
                    .CustomColors = gloGlobal.gloCustomColor.customColor
                Catch ex As Exception

                End Try
                If .ShowDialog(System.Windows.Forms.Form.ActiveForm) = DialogResult.OK Then
                    btnBackColor.BackColor = .Color
                    Try
                        gloGlobal.gloCustomColor.customColor = .CustomColors
                    Catch ex As Exception

                    End Try
                End If
            End With
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Flowsheet, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Delete, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)

            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub cmbFormat_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbFormat.SelectedIndexChanged
        If cmbFormat.Text = "Masked" Then
            mskFormat.PromptChar = "#"
            pnlMask.Visible = True
            mskFormat.Focus()
        Else
            pnlMask.Visible = False
        End If
    End Sub

    Private Sub tlsp_MSTFlowSheet_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tlsp_MSTFlowSheet.ItemClicked
        Try
            Select Case e.ClickedItem.Tag
                Case "OK"
                    If OKFlowSheet() Then
                        Me.Close()
                    End If

                Case "Cancel"
                    CancelFlowsheet()

                Case "Add"
                    AddFlowSheet()

                Case "Update"
                    AddFlowSheet()
                Case "Delete"
                    DeleteFlowSheet()


            End Select
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Label2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label2.Click, Label27.Click

    End Sub

    Private Sub txtColumnWidth_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtColumnWidth.TextChanged

    End Sub
    Private Sub Label3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label3.Click

    End Sub

    Private Sub txtColumnName_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtColumnName.TextChanged

    End Sub

    Private Sub Label6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label6.Click

    End Sub


    Private Sub btnBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowse.Click
        Try
            Dim ofrm As New frmEMTagAssociation(_arrLabs, _arrOrders, _arrOtherDiag, _arrManagment, "Flowsheet")
            With ofrm
                .StartPosition = FormStartPosition.CenterParent
                .ShowDialog(IIf(IsNothing(ofrm.Parent), Me, ofrm.Parent))
                _arrLabs = .arrLabs
                _arrOrders = .arrOrders
                _arrOtherDiag = .arrOtherDiag
                _arrManagment = .arrManagment
            End With

            If Not IsNothing(ofrm) Then    'obj Disposed by mitesh
                ofrm.Dispose()
                ofrm = Nothing
            End If
        Catch ex As Exception

        End Try
    End Sub


End Class
