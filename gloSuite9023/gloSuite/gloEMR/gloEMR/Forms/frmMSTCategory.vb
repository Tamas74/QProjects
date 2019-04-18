Public Class CategoryMaster
    Inherits System.Windows.Forms.Form

    Public _CategoryName As String
    Public _OldCategoryName As String
    Public IsfromHistory As Boolean = False
    Public IsfromLocation As Boolean = False
    Public Isfromstatus As Boolean = False
    Public IsFromOrderSet As Boolean = False
    Public IsFromHistoryMaster As Boolean = False
    Public IsFromROS As Boolean = False

    Friend WithEvents pnlToolStrip As System.Windows.Forms.Panel
    Friend WithEvents ts_CategoryMaster As gloGlobal.gloToolStripIgnoreFocus
    Private WithEvents ts_btnSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnClose As System.Windows.Forms.ToolStripButton
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Private WithEvents lbl_pnlBottom As System.Windows.Forms.Label
    Private WithEvents lbl_pnlLeft As System.Windows.Forms.Label
    Private WithEvents lbl_pnlRight As System.Windows.Forms.Label
    Private WithEvents lbl_pnlTop As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtCode As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Private strCategoryType As String = ""


    Public _Code As String = ""
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents lblHistoryType As System.Windows.Forms.Label
    Friend WithEvents cmbHistoryType As System.Windows.Forms.ComboBox
    Friend WithEvents pnlHistoryType As System.Windows.Forms.Panel
    Public _CodeDescription = ""
    Dim dtHistoryType As New DataTable
    Friend WithEvents pnlFavourite As System.Windows.Forms.Panel
    Friend WithEvents chkFavourite As System.Windows.Forms.CheckBox
    Dim isformLoading As Boolean
    Dim EditFavoriteOnly As Boolean
    Friend WithEvents pnlParentRace As System.Windows.Forms.Panel
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents cmbParentRace As System.Windows.Forms.ComboBox
    Friend WithEvents pnlMain As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Dim _categoryFormDlgResult As DialogResult = Windows.Forms.DialogResult.None


    Public ReadOnly Property CategoryFromDialogResult() As DialogResult
        Get
            Return _categoryFormDlgResult
        End Get
    End Property

#Region " Windows Form Designer generated code "

    Public Sub New(Optional ByVal CategoryType As String = "")
        MyBase.New()
        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        strCategoryType = CategoryType
    End Sub
    'constructor to initialize the variable mode 
    'mode=1 opened in edit mode
    'mode=2 opened in update mode 
    Public Sub New(ByVal intId As Long)
        MyBase.New()
        intCatId = intId 'set the mode in which the form has been opened
        InitializeComponent()

    End Sub

    Public Sub New(ByVal intId As Long, ByVal EditFav As Boolean)
        MyBase.New()
        EditFavoriteOnly = EditFav
        intCatId = intId 'set the mode in which the form has been opened
        InitializeComponent()

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
    Private objDBLayer As New ClsDBLayer
    Private intCatId As Long
    Friend WithEvents cmbCategoryType As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtCategoryDesc As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Private errDescription As New ErrorProvider
    Private errtype As New ErrorProvider
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(CategoryMaster))
        Me.cmbCategoryType = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtCategoryDesc = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.pnlToolStrip = New System.Windows.Forms.Panel()
        Me.ts_CategoryMaster = New gloGlobal.gloToolStripIgnoreFocus()
        Me.ts_btnSave = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnClose = New System.Windows.Forms.ToolStripButton()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.pnlParentRace = New System.Windows.Forms.Panel()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.cmbParentRace = New System.Windows.Forms.ComboBox()
        Me.pnlFavourite = New System.Windows.Forms.Panel()
        Me.chkFavourite = New System.Windows.Forms.CheckBox()
        Me.pnlHistoryType = New System.Windows.Forms.Panel()
        Me.cmbHistoryType = New System.Windows.Forms.ComboBox()
        Me.lblHistoryType = New System.Windows.Forms.Label()
        Me.pnlMain = New System.Windows.Forms.Panel()
        Me.txtCode = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.lbl_pnlBottom = New System.Windows.Forms.Label()
        Me.lbl_pnlLeft = New System.Windows.Forms.Label()
        Me.lbl_pnlRight = New System.Windows.Forms.Label()
        Me.lbl_pnlTop = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.pnlToolStrip.SuspendLayout()
        Me.ts_CategoryMaster.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.pnlParentRace.SuspendLayout()
        Me.pnlFavourite.SuspendLayout()
        Me.pnlHistoryType.SuspendLayout()
        Me.pnlMain.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmbCategoryType
        '
        Me.cmbCategoryType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCategoryType.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbCategoryType.ForeColor = System.Drawing.Color.Black
        Me.cmbCategoryType.Location = New System.Drawing.Point(144, 67)
        Me.cmbCategoryType.Name = "cmbCategoryType"
        Me.cmbCategoryType.Size = New System.Drawing.Size(240, 22)
        Me.cmbCategoryType.TabIndex = 7
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Location = New System.Drawing.Point(45, 71)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(96, 14)
        Me.Label2.TabIndex = 9
        Me.Label2.Text = "Category Type :"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtCategoryDesc
        '
        Me.txtCategoryDesc.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCategoryDesc.ForeColor = System.Drawing.Color.Black
        Me.txtCategoryDesc.Location = New System.Drawing.Point(144, 37)
        Me.txtCategoryDesc.MaxLength = 254
        Me.txtCategoryDesc.Name = "txtCategoryDesc"
        Me.txtCategoryDesc.Size = New System.Drawing.Size(240, 22)
        Me.txtCategoryDesc.TabIndex = 6
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label1.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Label1.Location = New System.Drawing.Point(13, 42)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(128, 14)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "Category Description :"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'pnlToolStrip
        '
        Me.pnlToolStrip.BackColor = System.Drawing.Color.FromArgb(CType(CType(204, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlToolStrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlToolStrip.Controls.Add(Me.ts_CategoryMaster)
        Me.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlToolStrip.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlToolStrip.ForeColor = System.Drawing.Color.Black
        Me.pnlToolStrip.Location = New System.Drawing.Point(0, 0)
        Me.pnlToolStrip.Name = "pnlToolStrip"
        Me.pnlToolStrip.Size = New System.Drawing.Size(413, 53)
        Me.pnlToolStrip.TabIndex = 11
        '
        'ts_CategoryMaster
        '
        Me.ts_CategoryMaster.BackColor = System.Drawing.Color.FromArgb(CType(CType(204, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ts_CategoryMaster.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.ts_CategoryMaster.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ts_CategoryMaster.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_CategoryMaster.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ts_CategoryMaster.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.ts_CategoryMaster.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ts_btnSave, Me.ts_btnClose})
        Me.ts_CategoryMaster.Location = New System.Drawing.Point(0, 0)
        Me.ts_CategoryMaster.Name = "ts_CategoryMaster"
        Me.ts_CategoryMaster.Size = New System.Drawing.Size(413, 53)
        Me.ts_CategoryMaster.TabIndex = 1
        Me.ts_CategoryMaster.Text = "ToolStrip1"
        '
        'ts_btnSave
        '
        Me.ts_btnSave.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnSave.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.ts_btnSave.Image = CType(resources.GetObject("ts_btnSave.Image"), System.Drawing.Image)
        Me.ts_btnSave.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnSave.Name = "ts_btnSave"
        Me.ts_btnSave.Size = New System.Drawing.Size(66, 50)
        Me.ts_btnSave.Tag = "Save"
        Me.ts_btnSave.Text = "&Save&&Cls"
        Me.ts_btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.ts_btnSave.ToolTipText = "Save and Close"
        '
        'ts_btnClose
        '
        Me.ts_btnClose.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnClose.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.ts_btnClose.Image = CType(resources.GetObject("ts_btnClose.Image"), System.Drawing.Image)
        Me.ts_btnClose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnClose.Name = "ts_btnClose"
        Me.ts_btnClose.Size = New System.Drawing.Size(43, 50)
        Me.ts_btnClose.Tag = "Close"
        Me.ts_btnClose.Text = "&Close"
        Me.ts_btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.pnlParentRace)
        Me.Panel1.Controls.Add(Me.pnlFavourite)
        Me.Panel1.Controls.Add(Me.pnlHistoryType)
        Me.Panel1.Controls.Add(Me.pnlMain)
        Me.Panel1.Controls.Add(Me.lbl_pnlBottom)
        Me.Panel1.Controls.Add(Me.lbl_pnlLeft)
        Me.Panel1.Controls.Add(Me.lbl_pnlRight)
        Me.Panel1.Controls.Add(Me.lbl_pnlTop)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 53)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Padding = New System.Windows.Forms.Padding(3)
        Me.Panel1.Size = New System.Drawing.Size(413, 197)
        Me.Panel1.TabIndex = 12
        '
        'pnlParentRace
        '
        Me.pnlParentRace.Controls.Add(Me.Label7)
        Me.pnlParentRace.Controls.Add(Me.Label8)
        Me.pnlParentRace.Controls.Add(Me.cmbParentRace)
        Me.pnlParentRace.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlParentRace.Location = New System.Drawing.Point(4, 157)
        Me.pnlParentRace.Name = "pnlParentRace"
        Me.pnlParentRace.Size = New System.Drawing.Size(405, 34)
        Me.pnlParentRace.TabIndex = 21
        Me.pnlParentRace.Visible = False
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Location = New System.Drawing.Point(61, 9)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(81, 14)
        Me.Label7.TabIndex = 17
        Me.Label7.Text = "Parent Race :"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.Red
        Me.Label8.Location = New System.Drawing.Point(48, 9)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(14, 14)
        Me.Label8.TabIndex = 18
        Me.Label8.Text = "*"
        '
        'cmbParentRace
        '
        Me.cmbParentRace.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbParentRace.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbParentRace.ForeColor = System.Drawing.Color.Black
        Me.cmbParentRace.Location = New System.Drawing.Point(144, 4)
        Me.cmbParentRace.Name = "cmbParentRace"
        Me.cmbParentRace.Size = New System.Drawing.Size(240, 22)
        Me.cmbParentRace.TabIndex = 16
        '
        'pnlFavourite
        '
        Me.pnlFavourite.Controls.Add(Me.chkFavourite)
        Me.pnlFavourite.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlFavourite.Location = New System.Drawing.Point(4, 129)
        Me.pnlFavourite.Name = "pnlFavourite"
        Me.pnlFavourite.Size = New System.Drawing.Size(405, 28)
        Me.pnlFavourite.TabIndex = 20
        Me.pnlFavourite.Visible = False
        '
        'chkFavourite
        '
        Me.chkFavourite.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chkFavourite.Location = New System.Drawing.Point(58, 4)
        Me.chkFavourite.Name = "chkFavourite"
        Me.chkFavourite.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkFavourite.Size = New System.Drawing.Size(105, 22)
        Me.chkFavourite.TabIndex = 19
        Me.chkFavourite.Text = "      Favorite :"
        Me.chkFavourite.UseVisualStyleBackColor = True
        '
        'pnlHistoryType
        '
        Me.pnlHistoryType.Controls.Add(Me.cmbHistoryType)
        Me.pnlHistoryType.Controls.Add(Me.lblHistoryType)
        Me.pnlHistoryType.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHistoryType.Location = New System.Drawing.Point(4, 95)
        Me.pnlHistoryType.Name = "pnlHistoryType"
        Me.pnlHistoryType.Size = New System.Drawing.Size(405, 34)
        Me.pnlHistoryType.TabIndex = 19
        Me.pnlHistoryType.Visible = False
        '
        'cmbHistoryType
        '
        Me.cmbHistoryType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbHistoryType.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbHistoryType.ForeColor = System.Drawing.Color.Black
        Me.cmbHistoryType.Location = New System.Drawing.Point(143, 6)
        Me.cmbHistoryType.Name = "cmbHistoryType"
        Me.cmbHistoryType.Size = New System.Drawing.Size(240, 22)
        Me.cmbHistoryType.TabIndex = 17
        '
        'lblHistoryType
        '
        Me.lblHistoryType.AutoSize = True
        Me.lblHistoryType.BackColor = System.Drawing.Color.Transparent
        Me.lblHistoryType.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHistoryType.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblHistoryType.Location = New System.Drawing.Point(56, 10)
        Me.lblHistoryType.Name = "lblHistoryType"
        Me.lblHistoryType.Size = New System.Drawing.Size(84, 14)
        Me.lblHistoryType.TabIndex = 18
        Me.lblHistoryType.Text = "History Type :"
        '
        'pnlMain
        '
        Me.pnlMain.Controls.Add(Me.Panel2)
        Me.pnlMain.Controls.Add(Me.cmbCategoryType)
        Me.pnlMain.Controls.Add(Me.txtCategoryDesc)
        Me.pnlMain.Controls.Add(Me.txtCode)
        Me.pnlMain.Controls.Add(Me.Label2)
        Me.pnlMain.Controls.Add(Me.Label3)
        Me.pnlMain.Controls.Add(Me.Label1)
        Me.pnlMain.Controls.Add(Me.Label4)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlMain.Location = New System.Drawing.Point(4, 4)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(405, 91)
        Me.pnlMain.TabIndex = 21
        '
        'txtCode
        '
        Me.txtCode.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCode.ForeColor = System.Drawing.Color.Black
        Me.txtCode.Location = New System.Drawing.Point(144, 9)
        Me.txtCode.MaxLength = 50
        Me.txtCode.Name = "txtCode"
        Me.txtCode.Size = New System.Drawing.Size(240, 22)
        Me.txtCode.TabIndex = 6
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Red
        Me.Label6.Location = New System.Drawing.Point(75, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(14, 14)
        Me.Label6.TabIndex = 16
        Me.Label6.Text = "*"
        Me.Label6.Visible = False
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Label5.Location = New System.Drawing.Point(89, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(43, 14)
        Me.Label5.TabIndex = 8
        Me.Label5.Text = "Code :"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Red
        Me.Label3.Location = New System.Drawing.Point(1, 41)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(14, 14)
        Me.Label3.TabIndex = 14
        Me.Label3.Text = "*"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Red
        Me.Label4.Location = New System.Drawing.Point(32, 72)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(14, 14)
        Me.Label4.TabIndex = 15
        Me.Label4.Text = "*"
        '
        'lbl_pnlBottom
        '
        Me.lbl_pnlBottom.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_pnlBottom.Location = New System.Drawing.Point(4, 193)
        Me.lbl_pnlBottom.Name = "lbl_pnlBottom"
        Me.lbl_pnlBottom.Size = New System.Drawing.Size(405, 1)
        Me.lbl_pnlBottom.TabIndex = 13
        Me.lbl_pnlBottom.Text = "label2"
        '
        'lbl_pnlLeft
        '
        Me.lbl_pnlLeft.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_pnlLeft.Location = New System.Drawing.Point(3, 4)
        Me.lbl_pnlLeft.Name = "lbl_pnlLeft"
        Me.lbl_pnlLeft.Size = New System.Drawing.Size(1, 190)
        Me.lbl_pnlLeft.TabIndex = 12
        Me.lbl_pnlLeft.Text = "label4"
        '
        'lbl_pnlRight
        '
        Me.lbl_pnlRight.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_pnlRight.Location = New System.Drawing.Point(409, 4)
        Me.lbl_pnlRight.Name = "lbl_pnlRight"
        Me.lbl_pnlRight.Size = New System.Drawing.Size(1, 190)
        Me.lbl_pnlRight.TabIndex = 11
        Me.lbl_pnlRight.Text = "label3"
        '
        'lbl_pnlTop
        '
        Me.lbl_pnlTop.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_pnlTop.Location = New System.Drawing.Point(3, 3)
        Me.lbl_pnlTop.Name = "lbl_pnlTop"
        Me.lbl_pnlTop.Size = New System.Drawing.Size(407, 1)
        Me.lbl_pnlTop.TabIndex = 10
        Me.lbl_pnlTop.Text = "label1"
        '
        'Panel2
        '
        Me.Panel2.AutoSize = True
        Me.Panel2.Controls.Add(Me.Label6)
        Me.Panel2.Controls.Add(Me.Label5)
        Me.Panel2.Location = New System.Drawing.Point(9, 13)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(132, 15)
        Me.Panel2.TabIndex = 17
        '
        'CategoryMaster
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(204, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(413, 250)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.pnlToolStrip)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "CategoryMaster"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Category"
        Me.pnlToolStrip.ResumeLayout(False)
        Me.pnlToolStrip.PerformLayout()
        Me.ts_CategoryMaster.ResumeLayout(False)
        Me.ts_CategoryMaster.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.pnlParentRace.ResumeLayout(False)
        Me.pnlParentRace.PerformLayout()
        Me.pnlFavourite.ResumeLayout(False)
        Me.pnlHistoryType.ResumeLayout(False)
        Me.pnlHistoryType.PerformLayout()
        Me.pnlMain.ResumeLayout(False)
        Me.pnlMain.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub CategoryMaster_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim arrlist As ArrayList
        'Dim str1 As String
        isformLoading = True
        txtCategoryDesc.Focus()
        dtHistoryType = objDBLayer.GetStandardTypes()

        cmbHistoryType.DataSource = dtHistoryType
        cmbHistoryType.DataSource = dtHistoryType
        cmbHistoryType.DisplayMember = dtHistoryType.Columns(0).ColumnName
        cmbHistoryType.ValueMember = dtHistoryType.Columns(1).ColumnName
        If (dtHistoryType.Rows.Count > 0) Then
            cmbHistoryType.SelectedIndex = 0
        End If



        RemoveHandler cmbCategoryType.SelectedIndexChanged, AddressOf cmbCategoryType_SelectedIndexChanged
        Dim dtCategoryType As DataTable
        dtCategoryType = objDBLayer.GetCategoryTypes()
        cmbCategoryType.Text = ""
        cmbCategoryType.DataSource = dtCategoryType
        If (dtCategoryType.Rows.Count > 0) Then
            cmbCategoryType.SelectedIndex = 0
        End If

        cmbCategoryType.DisplayMember = dtCategoryType.Columns(1).ColumnName
        cmbCategoryType.ValueMember = dtCategoryType.Columns(0).ColumnName
        AddHandler cmbCategoryType.SelectedIndexChanged, AddressOf cmbCategoryType_SelectedIndexChanged

        If intCatId <> 0 Then
            Try
                If gloSettings.gloEMRAdminSettings.globlnEnableMultipleRaceFeatures Then
                    If EditFavoriteOnly Then
                        txtCode.Enabled = False
                        txtCategoryDesc.Enabled = False
                        cmbCategoryType.Enabled = False
                        pnlFavourite.Visible = True
                        pnlHistoryType.Visible = False
                    End If
                End If


                arrlist = objDBLayer.FetchDataForUpdate(intCatId)

                If arrlist.Count > 0 Then
                    txtCategoryDesc.Text = CType(arrlist.Item(1), System.String)
                    _OldCategoryName = txtCategoryDesc.Text.Trim                    ''Taking the previous catagory name
                    cmbCategoryType.Text = CType(arrlist.Item(2), System.String)
                    txtCode.Text = CType(arrlist.Item(3), System.String)

                    If cmbCategoryType.Text.ToString.Trim = "History" Then
                        cmbHistoryType.SelectedValue = CType(arrlist.Item(4), System.String)
                    ElseIf cmbCategoryType.Text.ToString.Trim = "Race" Or cmbCategoryType.Text.ToString.Trim = "Language" Or cmbCategoryType.Text.ToString.Trim = "Ethnicity" Then
                        chkFavourite.Checked = CType(arrlist.Item(5), System.Boolean)
                    ElseIf cmbCategoryType.Text.ToString.Trim = "Race Specification" Then
                        fillRaceEthnicityParentCoder(cmbCategoryType.Text)
                        cmbParentRace.SelectedValue = CType(arrlist.Item(7), System.Decimal)
                        chkFavourite.Checked = CType(arrlist.Item(5), System.Boolean)
                    ElseIf cmbCategoryType.Text.ToString.Trim = "Ethnicity Specification" Then
                        fillRaceEthnicityParentCoder(cmbCategoryType.Text)
                        cmbParentRace.SelectedValue = CType(arrlist.Item(7), System.Decimal)
                        chkFavourite.Checked = CType(arrlist.Item(5), System.Boolean)
                    End If

                    '21-May-15 Aniket: Resolving Bug #83495: GloEMR->Edit Category->Ob Plan->Modify OB Plan first Trimester->system should not allow item to modify/delete
                    If IsNothing(arrlist.Item(6)) = False Then

                        If IsDBNull(arrlist.Item(6)) = False Then
                            If arrlist.Item(6) = True Then
                                txtCategoryDesc.Enabled = False
                                cmbCategoryType.Enabled = False
                            End If
                        End If

                    End If

                End If
            Catch ex As SqlClient.SqlException
                MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
        If IsfromHistory = True Then

            '10-Apr-13 Aniket: Fixing Issue 49064
            cmbCategoryType.Text = "Reaction"
            If cmbCategoryType.Text.Trim() = "Reaction" Then
                If txtCode.Text.ToString.IndexOf("|") >= 0 Then
                    MessageBox.Show("Cannot save Category Type 'Reaction' with '|' character in it.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    txtCode.Focus()
                    Exit Sub
                End If
                If txtCategoryDesc.ToString.IndexOf("|") >= 0 Then
                    MessageBox.Show("Cannot save Category Type 'Reaction' with '|' character in it.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    txtCategoryDesc.Focus()
                    Exit Sub
                End If
            End If
            cmbCategoryType.Enabled = False
        End If
        If IsfromLocation = True Then
            cmbCategoryType.Text = "Location"
            cmbCategoryType.Enabled = False
        End If
        If Isfromstatus = True Then
            cmbCategoryType.Text = "Status"
            cmbCategoryType.Enabled = False
        End If

        '27-May-13 Aniket: Resolving Bug 51426
        If IsFromOrderSet = True Then
            cmbCategoryType.Text = "Orderset"
            cmbCategoryType.Enabled = False
        End If

        If Not strCategoryType = "" Then
            cmbCategoryType.Text = strCategoryType
            cmbCategoryType.Enabled = False
        End If

        '28-Jan-15 Aniket: Bug #79102: gloEMR - History Master - Application display wrong Category type when user tries to add new category in history.
        If IsFromHistoryMaster = True Then
            cmbCategoryType.Text = "History"
            cmbCategoryType.Enabled = False
        End If
        If IsFromROS = True Then
            cmbCategoryType.Text = "ROS"
            cmbCategoryType.Enabled = False
        End If
        _Code = txtCode.Text.Trim()
        _CodeDescription = txtCategoryDesc.Text.Trim()

        '11-Nov-14 Aniket: Bug #75683: gloEMR - History - Default History category is not getting loaded after opening history,exception is coming.
        If txtCategoryDesc.Text = "Allergies" Then
            txtCategoryDesc.Enabled = False
        End If

    End Sub
    Private Sub txtCode_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCode.KeyPress
        If cmbCategoryType.Text = "Reaction" Then
            If e.KeyChar = "|" Then
                e.Handled = True
            End If
        End If
    End Sub
    Private Sub txtCategoryDesc_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCategoryDesc.KeyPress
        If cmbCategoryType.Text = "Reaction" Then
            If e.KeyChar = "|" Then
                e.Handled = True
            End If
        End If
    End Sub
    Private Sub OKMSTCategory()
        'start'To check for the Manufacturar category

        Dim formClose As Boolean = True

        If cmbCategoryType.Text.Trim() = "Manufacturer" Then
            If txtCode.Text.Trim() = "" Then
                'errDescription.SetError(txtCode, "Code Required")
                MessageBox.Show("Code is required for Category Type 'Manufacturer'", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtCode.Focus()
                Exit Sub
            End If
            'end'To check for the Manufacturar category
        End If

        If cmbCategoryType.Text.Trim() = "Reaction" Then
            If txtCode.Text.ToString.IndexOf("|") >= 0 Then
                MessageBox.Show("Cannot save Category Type 'Reaction' with '|' character in it.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtCode.Focus()
                Exit Sub
            End If
            If txtCategoryDesc.ToString.IndexOf("|") >= 0 Then
                MessageBox.Show("Cannot save Category Type 'Reaction' with '|' character in it.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtCategoryDesc.Focus()
                Exit Sub
            End If
        End If

        If cmbCategoryType.Text.Trim() = "Vaccine" Then
            If txtCode.Text.Trim() = "" Then
                'errDescription.SetError(txtCode, "Code Required")
                MessageBox.Show("Code is required for Category Type 'Vaccine'", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtCode.Focus()
                Exit Sub
            End If
            'end'To check for the Manufacturar category
        End If
        If cmbCategoryType.Text.Trim() = "TradeName" Then
            If txtCode.Text.Trim() = "" Then
                'errDescription.SetError(txtCode, "Code Required")
                MessageBox.Show("Code is required for Category Type 'TradeName'", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtCode.Focus()
                Exit Sub
            End If

        End If
        If cmbCategoryType.Text.Trim() = "Unit Of Measure Codes" Then
            If txtCode.Text.Trim() = "" Then
                MessageBox.Show("Code is required for Category Type 'Unit Of Measure Codes'", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtCode.Focus()
                Exit Sub
            End If

        End If
        If cmbCategoryType.Text.Trim() = "Publicity Code" Then
            If txtCode.Text.Trim() = "" Then
                MessageBox.Show("Code is required for Category Type 'Publicity Code'", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtCode.Focus()
                Exit Sub
            End If

            If txtCode.Text.Trim().Length > 10 Then
                MessageBox.Show("Code cannot be greater then 10 character for type 'Publicity Code'.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtCode.Focus()
                Exit Sub
            End If

        End If

        If cmbCategoryType.Text.Trim() = "Gender" Then
            If txtCode.Text.Trim() = "" Then
                MessageBox.Show("Code is required for Category Type 'Gender'", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtCode.Focus()
                Exit Sub
            End If
            If txtCategoryDesc.Text.Trim().Length > 10 Then
                MessageBox.Show("Description cannot be greater then 10 character for type 'Gender'.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtCode.Focus()
                Exit Sub
            End If

        End If

        If Trim(txtCategoryDesc.Text) = "" Then
            'errDescription.SetError(txtCategoryDesc, "Description Required")
            MessageBox.Show("Description is Required", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            txtCategoryDesc.Focus()
            Exit Sub
        Else
            'errDescription.SetError(txtCategoryDesc, "")
            If cmbCategoryType.Text <> "" Then
                'errDescription.SetError(txtCategoryDesc, "")
                If cmbCategoryType.Text = "Vaccine" Or cmbCategoryType.Text = "Manufacturer" Or cmbCategoryType.Text = "TradeName" Then
                    If Not objDBLayer.ValidateCode(cmbCategoryType.Text, Trim(txtCategoryDesc.Text), intCatId, txtCode.Text.Trim()) Then
                        MessageBox.Show("Duplicate Code or Description. ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        '  txtCode.Focus()
                        Exit Sub

                    ElseIf Not objDBLayer.ValidateCode(cmbCategoryType.Text, Trim(txtCategoryDesc.Text), intCatId, txtCode.Text.Trim(), cmbCategoryType.Text.Trim) Then
                        MessageBox.Show("Duplicate Code or Description. ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        '  txtCode.Focus()
                        Exit Sub
                    End If
                ElseIf cmbCategoryType.Text = "Unit Of Measure Codes" Then

                    If Not objDBLayer.ValidateCode(cmbCategoryType.Text, Trim(txtCategoryDesc.Text), intCatId, txtCode.Text.Trim(), cmbCategoryType.Text.Trim) Then
                        MessageBox.Show("Unit Of Measure code already exists. ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        txtCode.Focus()
                        Exit Sub
                    End If
                ElseIf cmbCategoryType.Text = "Publicity Code" Then

                    If Not objDBLayer.ValidateCode(cmbCategoryType.Text, Trim(txtCategoryDesc.Text), intCatId, txtCode.Text.Trim(), cmbCategoryType.Text.Trim) Then
                        MessageBox.Show("Publicity code already exists. ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        txtCode.Focus()
                        Exit Sub
                    End If

                    If objDBLayer.IsPublicityCodeUsedonIM(_Code) = True And Trim(txtCode.Text).ToUpper() <> _Code.Trim().ToUpper() And intCatId <> 0 Then

                        Dim _dlgResult As DialogResult = DialogResult.None

                        _dlgResult = MessageBox.Show("Publicity code is associated with patient immunization(s); modifying code will lose publicity information on saved immunization(s). " + Environment.NewLine + " Continue save?", gstrMessageBoxCaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation)
                        If _dlgResult = Windows.Forms.DialogResult.Cancel Or _dlgResult = Windows.Forms.DialogResult.None Then
                            txtCode.Focus()
                            Exit Sub
                        End If

                    End If

                Else


                    If Not objDBLayer.ValidateDescription(cmbCategoryType.Text, Trim(txtCategoryDesc.Text), intCatId, txtCode.Text.Trim()) Then
                        'errDescription.SetError(txtCategoryDesc, "Duplicate Description")
                        If IsfromLocation = True Then
                            MessageBox.Show("Duplicate Location", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            txtCategoryDesc.Focus()
                            Exit Sub
                        ElseIf Isfromstatus = True Then
                            MessageBox.Show("Duplicate Status", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            txtCategoryDesc.Focus()
                            Exit Sub
                        Else
                            MessageBox.Show("Duplicate Description", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            txtCategoryDesc.Focus()
                            Exit Sub
                        End If

                    End If
                End If
            End If
        End If
        If cmbCategoryType.Text = "" Then
            'errtype.SetError(cmbCategoryType, "Description Required")
            MessageBox.Show("Category Type Required", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        Else
            'errtype.SetError(cmbCategoryType, "")
        End If
        If intCatId = 0 Then
            Try
                If cmbCategoryType.Text.Trim() = "Manufacturer" Then
                    If txtCode.Text.Length > 3 Then
                        MessageBox.Show("Category code cannot be greater then 3 character for type 'Manufacturer'", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        txtCode.Focus()
                        Return
                    End If
                    objDBLayer.AddData(Trim(txtCode.Text), Trim(txtCategoryDesc.Text))
                End If
                If cmbCategoryType.Text.Trim() = "History" Then
                    objDBLayer.AddData(Trim(txtCode.Text), Trim(txtCategoryDesc.Text), cmbCategoryType.Text, cmbHistoryType.SelectedValue.Trim())
                    objDBLayer = Nothing
                ElseIf cmbCategoryType.Text.Trim().ToUpper() = "RACE" Or cmbCategoryType.Text.Trim().ToUpper() = "LANGUAGE" Or cmbCategoryType.Text.Trim().ToUpper() = "ETHNICITY" Then
                    objDBLayer.AddData(Trim(txtCode.Text), Trim(txtCategoryDesc.Text), cmbCategoryType.Text, , chkFavourite.Checked)
                    objDBLayer = Nothing
                ElseIf cmbCategoryType.Text.Trim().ToUpper() = "RACE SPECIFICATION" Then
                    Dim sParentCode As String = String.Empty
                    Dim drv As DataRowView = cmbParentRace.SelectedItem
                    If IsNothing(drv) = False Then
                        sParentCode = drv.Row.Item(5).ToString()
                    End If


                    objDBLayer.AddData(Trim(txtCode.Text), Trim(txtCategoryDesc.Text), cmbCategoryType.Text, "", chkFavourite.Checked, cmbParentRace.SelectedValue, sParentCode)
                    objDBLayer = Nothing
                ElseIf cmbCategoryType.Text.Trim().ToUpper() = "ETHNICITY SPECIFICATION" Then
                    Dim sParentCode As String = String.Empty
                    Dim drv As DataRowView = cmbParentRace.SelectedItem
                    If IsNothing(drv) = False Then
                        sParentCode = drv.Row.Item(5).ToString()
                    End If


                    objDBLayer.AddData(Trim(txtCode.Text), Trim(txtCategoryDesc.Text), cmbCategoryType.Text, "", chkFavourite.Checked, cmbParentRace.SelectedValue, sParentCode)
                    objDBLayer = Nothing
                Else
                    objDBLayer.AddData(Trim(txtCode.Text), Trim(txtCategoryDesc.Text), cmbCategoryType.Text)
                    objDBLayer = Nothing
                End If

                _CategoryName = Trim(txtCategoryDesc.Text)          ''storing the changed catogery name
                _categoryFormDlgResult = Windows.Forms.DialogResult.OK
                Me.Close()
            Catch ex As SqlClient.SqlException
                MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        Else
            ''Dhruv 20100216 ''to update the category 
            Try

                If cmbCategoryType.Text.Trim.ToUpper() = "History".ToUpper() Or cmbCategoryType.Text.Trim.ToUpper() = "ROS".ToUpper() Then
                    Dim Result As Boolean = CheckIsCatagoryExistsinPortal(intCatId)
                    If Result Then
                        formClose = False
                        Exit Sub
                    End If
                End If
              

                '' Update category 
                If cmbCategoryType.Text.Trim() = "Manufacturer" Then

                    ''Checking the text length for the type manufacutrer
                    If txtCode.Text.Length > 3 Then
                        MessageBox.Show("Category code cannot be greater than 3 character for type 'Manufacturer'", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        txtCode.Focus()
                        Return
                    End If
                    objDBLayer.UpDateData(Trim(txtCode.Text), Trim(txtCategoryDesc.Text), _Code, _CodeDescription)
                Else
                    ''It is checked when the Cataegory type is changed from 'Manufacturer' to any other [Dhruv]
                    Dim _isCheckResult = objDBLayer.CheckCount(_Code, _CodeDescription)
                    If _isCheckResult = True Then ''If exists then delete from the table
                        objDBLayer.DeleteData(_Code, _CodeDescription)
                    End If
                End If
                If cmbCategoryType.Text.Trim = "History" Then
                    objDBLayer.UpDateData(Trim(txtCode.Text), Trim(txtCategoryDesc.Text), cmbCategoryType.Text, intCatId, cmbHistoryType.SelectedValue.Trim())
                    objDBLayer = Nothing
                ElseIf cmbCategoryType.Text.Trim().ToUpper() = "RACE" Or cmbCategoryType.Text.Trim().ToUpper() = "LANGUAGE" Or cmbCategoryType.Text.Trim().ToUpper() = "ETHNICITY" Then
                    objDBLayer.UpDateData(Trim(txtCode.Text), Trim(txtCategoryDesc.Text), cmbCategoryType.Text, intCatId, , chkFavourite.Checked)
                    objDBLayer = Nothing
                ElseIf cmbCategoryType.Text.Trim = "CPT" Then
                    If Trim(txtCategoryDesc.Text) <> "" And intCatId <> 0 Then

                        If objDBLayer.IsCPTCategoryInUse(intCatId, txtCategoryDesc.Text) Then

                            Dim oDlgResult As DialogResult = DialogResult.None
                            oDlgResult = MessageBox.Show("CPT Category is in use. Do you want to modify and update each CPT record?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                            If oDlgResult = System.Windows.Forms.DialogResult.Yes Then

                                '''''first update let the Category master table and then update the CPT_Mst Category description based on the Category ID
                                objDBLayer.UpDateData(Trim(txtCode.Text), Trim(txtCategoryDesc.Text), cmbCategoryType.Text, intCatId)

                                ''update the respective CPT's Category description in CPT_Mst with Category description of Category_Mst when Category type = 'CPT'
                                objDBLayer.UpdateCPTCategoryInUse(intCatId)
                                objDBLayer = Nothing

                            End If
                            txtCategoryDesc.Focus()
                        Else
                            ''only update the Category master table
                            objDBLayer.UpDateData(Trim(txtCode.Text), Trim(txtCategoryDesc.Text), cmbCategoryType.Text, intCatId)
                            objDBLayer = Nothing
                        End If
                    End If
                ElseIf cmbCategoryType.Text.Trim().ToUpper() = "RACE SPECIFICATION" Then
                    Dim sParentCode As String = String.Empty
                    Dim drv As DataRowView = cmbParentRace.SelectedItem
                    If IsNothing(drv) = False Then
                        sParentCode = drv.Row.Item(5).ToString()
                    End If


                    objDBLayer.UpDateData(Trim(txtCode.Text), Trim(txtCategoryDesc.Text), cmbCategoryType.Text, intCatId, "", chkFavourite.Checked, cmbParentRace.SelectedValue, sParentCode)
                    objDBLayer = Nothing
                ElseIf cmbCategoryType.Text.Trim().ToUpper() = "ETHNICITY SPECIFICATION" Then
                    Dim sParentCode As String = String.Empty
                    Dim drv As DataRowView = cmbParentRace.SelectedItem
                    If IsNothing(drv) = False Then
                        sParentCode = drv.Row.Item(5).ToString()
                    End If


                    objDBLayer.UpDateData(Trim(txtCode.Text), Trim(txtCategoryDesc.Text), cmbCategoryType.Text, intCatId, "", chkFavourite.Checked, cmbParentRace.SelectedValue, sParentCode)
                    objDBLayer = Nothing
                Else

                    objDBLayer.UpDateData(Trim(txtCode.Text), Trim(txtCategoryDesc.Text), cmbCategoryType.Text, intCatId)
                    objDBLayer = Nothing

                End If


                _CategoryName = Trim(txtCategoryDesc.Text)

                '' to update templates
                If _OldCategoryName <> _CategoryName Then               ''Checking the new and old category name if it is same come out
                    Dim isUpdateTemplate As Boolean = False
                    If cmbCategoryType.Text.ToUpper = "History".ToUpper Then        'if the categorytype is history 
                        _OldCategoryName = "History.sHistoryItem+History.sComments|" & _OldCategoryName ''set the catogery name "OLD"
                        _CategoryName = "History.sHistoryItem+History.sComments|" & _CategoryName       ''set the catogery name "New"    
                        isUpdateTemplate = True
                    ElseIf cmbCategoryType.Text.ToUpper = "ROS".ToUpper Then            ''checking for the category type "ROS"
                        _OldCategoryName = "ROS.sROSItem+ROS.sComments|" & _OldCategoryName  ''set the catogery name "OLD"
                        _CategoryName = "ROS.sROSItem+ROS.sComments|" & _CategoryName       ''set the catogery name "New"   
                        isUpdateTemplate = True
                    End If                                              '

                    If isUpdateTemplate = True Then         ''if updated then only update theh templates other wise no needed
                        If MessageBox.Show("Do you want to update existing templates?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                            Dim ofrm As New frmUpdateTemplates(_OldCategoryName, _CategoryName, False)
                            ofrm.ShowDialog(IIf(IsNothing(ofrm.Parent), Me, ofrm.Parent))
                            ofrm.Dispose()
                            ofrm = Nothing
                        End If
                    End If
                End If
                ''

                _CategoryName = Trim(txtCategoryDesc.Text)
                If formClose = True Then
                    Me.Close()
                End If
            Catch ex As SqlClient.SqlException
                MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub
    Private Sub CloseMSTCategory()

        objDBLayer = Nothing
        _categoryFormDlgResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub



    Private Sub cmbCategoryType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbCategoryType.SelectedIndexChanged
        Try


            cmbParentRace.DataSource = Nothing

            If cmbCategoryType.Text = "" Then

                MsgBox("Description Required")
                cmbCategoryType.Focus()
            Else

                If cmbCategoryType.Text = "Vaccine" Or cmbCategoryType.Text = "TradeName" Or cmbCategoryType.Text = "Manufacturer" Or cmbCategoryType.Text = "Unit Of Measure Codes" Or cmbCategoryType.Text = "Publicity Code" Then
                    'If Not objDBLayer.ValidateCode(cmbCategoryType.Text, txtCategoryDesc.Text, intCatId, txtCode.Text.Trim()) Then
                    '    'errtype.SetError(cmbCategoryType, "Duplicate Category")
                    '    MsgBox("Duplicate Category")
                    '    cmbCategoryType.Focus()
                    'ElseIf Not objDBLayer.ValidateCode(cmbCategoryType.Text, txtCategoryDesc.Text, intCatId, txtCode.Text.Trim(), cmbCategoryType.Text.Trim) Then
                    '    MsgBox("Duplicate Category")
                    '    cmbCategoryType.Focus()
                    'End If
                Else
                    ' _categoryType = cmbCategoryType.Text
                    If Not objDBLayer.ValidateDescription(cmbCategoryType.Text, txtCategoryDesc.Text, intCatId, txtCode.Text.Trim()) Then
                        'errtype.SetError(cmbCategoryType, "Duplicate Category")
                        MsgBox("Duplicate Category")
                        cmbCategoryType.Focus()
                    End If
                End If
            End If

            Label6.Location = New Point(73, 24)

            If cmbCategoryType.Text = "Vaccine" Then
                Label6.Visible = True
                Label5.Text = "CVX Code :"
                '   Me.Label5.Location = New System.Drawing.Point(77, 24)

            ElseIf cmbCategoryType.Text = "TradeName" Then
                Label6.Visible = True
                Label5.Text = "CVX Code :"
                ' Me.Label5.Location = New System.Drawing.Point(77, 24)
            ElseIf cmbCategoryType.Text = "Manufacturer" Then
                Label6.Visible = True
                Label5.Text = "MVX Code :"
                '   Me.Label5.Location = New System.Drawing.Point(77, 24)
            ElseIf cmbCategoryType.Text = "Unit Of Measure Codes" Then
                Label6.Visible = True
                Label5.Text = "Unit Code :"
            ElseIf cmbCategoryType.Text = "Publicity Code" Then
                Label6.Visible = True
                Label5.Text = "Publicity Code :"
                Label6.Location = New Point(49, 24)

            Else
                Label6.Visible = False
                Label5.Text = "Code :"
                '   Me.Label5.Location = New System.Drawing.Point(102, 24)
            End If
            If cmbCategoryType.Text = "History" Then
                'cmbHistoryType.SelectedIndex = 0
                pnlHistoryType.Visible = True
                pnlFavourite.Visible = False
                pnlHistoryType.BringToFront()
            ElseIf cmbCategoryType.Text = "Race" Or cmbCategoryType.Text = "Language" Or cmbCategoryType.Text = "Ethnicity" Then
                ''Favourite option will be visible irrespective of the settings 
                ''If gloSettings.gloEMRAdminSettings.globlnEnableMultipleRaceFeatures Then
                'cmbHistoryType.SelectedIndex = 0
                pnlHistoryType.Visible = False
                pnlFavourite.Visible = True
                pnlFavourite.BringToFront()
                pnlParentRace.Visible = False
                ''End If

            ElseIf cmbCategoryType.Text = "Race Specification" Then
                fillRaceEthnicityParentCoder(cmbCategoryType.Text)
                pnlHistoryType.Visible = False
                pnlFavourite.Visible = True
                pnlParentRace.Visible = True
                pnlParentRace.BringToFront()
                Label7.Text = "Parent Race :"
                Label7.Location = New System.Drawing.Point(61, 9)
            ElseIf cmbCategoryType.Text = "Ethnicity Specification" Then
                fillRaceEthnicityParentCoder(cmbCategoryType.Text)
                pnlHistoryType.Visible = False
                pnlFavourite.Visible = True
                pnlParentRace.Visible = True
                pnlParentRace.BringToFront()
                Label7.Text = "Parent Ethnicity :"
                Label7.Location = New System.Drawing.Point(42, 9)
            Else
                pnlHistoryType.Visible = False
                pnlFavourite.Visible = False
                pnlParentRace.Visible = False
            End If
            'End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub fillRaceEthnicityParentCoder(ByVal sCategoryType As String)
        Dim dtRaceEthnicitySpecification As DataTable
        Try
            dtRaceEthnicitySpecification = objDBLayer.GetRaceEthnicityParentList(sCategoryType)
            cmbParentRace.Text = ""
            cmbParentRace.DataSource = dtRaceEthnicitySpecification
            If (dtRaceEthnicitySpecification.Rows.Count > 0) Then
                cmbParentRace.SelectedIndex = 0
            End If
            cmbParentRace.DisplayMember = dtRaceEthnicitySpecification.Columns(1).ColumnName
            cmbParentRace.ValueMember = dtRaceEthnicitySpecification.Columns(0).ColumnName
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK)
        
        End Try
      
    End Sub


    Private Sub ts_CategoryMaster_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles ts_CategoryMaster.ItemClicked
        Try
            Select Case UCase(e.ClickedItem.Tag)
                Case UCase("Save")
                    OKMSTCategory()
                Case UCase("Close")
                    CloseMSTCategory()

            End Select
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK)


        End Try
    End Sub

    Private Function CheckIsCatagoryExistsinPortal(ByVal CategoryID As Long) As Boolean
        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString)
        Dim oParameters As gloDatabaseLayer.DBParameters = Nothing
        Dim _dt As DataTable = Nothing
        Dim _dt2 As DataTable = Nothing
        Try

            oParameters = New gloDatabaseLayer.DBParameters()
            oParameters.Add("@ncategoryid", CategoryID, ParameterDirection.Input, SqlDbType.BigInt)
            oParameters.Add("@IsDelete", False, ParameterDirection.Input, SqlDbType.Bit)
            oDB.Connect(False)
            oDB.Retrive("WS_IsHistorycategoryExistsinHealthform", oParameters, _dt)
            If _dt IsNot Nothing AndAlso _dt.Rows.Count > 0 Then

                If MessageBox.Show("Selected category is used in patient portal forms. After this modification all existing patient portal forms data and any new incoming data from portal will be associated to this modified category." + System.Environment.NewLine + System.Environment.NewLine + "Do you want to continue with the modification?", gstrMessageBoxCaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Information) = Windows.Forms.DialogResult.OK Then
                    Try
                        ' Set IsRepublish Required to 1 & Delete Entry 
                        oParameters = New gloDatabaseLayer.DBParameters()
                        oParameters.Add("@ncategoryid", CategoryID, ParameterDirection.Input, SqlDbType.BigInt)
                        oParameters.Add("@IsDelete", False, ParameterDirection.Input, SqlDbType.Bit)
                        oParameters.Add("@IsUpdatePatientForm", True, ParameterDirection.Input, SqlDbType.Bit)
                        oDB.Connect(False)
                        oDB.Retrive("WS_IsHistorycategoryExistsinHealthform", oParameters, _dt2)

                    Catch ex As Exception
                    End Try
                    Return False
                Else
                    Return True
                End If
            Else
                Return False
            End If

        Catch dbEx As gloDatabaseLayer.DBException
            dbEx.ERROR_Log(dbEx.ToString())
            Throw dbEx
        Finally
            If oParameters IsNot Nothing Then

                oParameters.Dispose()
                oParameters = Nothing
            End If
            If oDB IsNot Nothing Then
                oDB.Disconnect()
                oDB.Dispose()
                oDB = Nothing
            End If
            If Not IsNothing(_dt) Then
                _dt.Dispose()
                _dt = Nothing
            End If

            If Not IsNothing(_dt2) Then
                _dt2.Dispose()
                _dt2 = Nothing
            End If
        End Try

        Return False

    End Function

End Class
