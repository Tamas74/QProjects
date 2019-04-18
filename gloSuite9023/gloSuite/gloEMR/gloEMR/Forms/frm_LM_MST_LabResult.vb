Public Class frm_LM_MST_LabResult
    Inherits System.Windows.Forms.Form

    '''' While Add, return this Paramerters to View-From so the Newly Added Records get Highlighted 
    Public _FlowSheetName As String
    ''''
    Public blnEdit As Boolean
    '' 
    Public _Cancel As Boolean = False

    Private m_ID As Long
    '''' m_IsInUse = True if then selected Flow sheet has Records stored in it
    Private m_IsInUse As Boolean

    Private ocls_LM_LabResult As New cls_LM_LabResult
    Friend WithEvents mskFormat As System.Windows.Forms.MaskedTextBox
    Friend WithEvents pnlTop As System.Windows.Forms.Panel
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Private WithEvents tlsLabResult As gloGlobal.gloToolStripIgnoreFocus
    Private WithEvents btn_tls_Save As System.Windows.Forms.ToolStripButton
    Private WithEvents btn_tls_Close As System.Windows.Forms.ToolStripButton
    Dim tblFlowSheet As New DataTable

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

            If Not (components Is Nothing) Then

                components.Dispose()
            End If
            If (IsNothing(tblFlowSheet) = False) Then
                tblFlowSheet.Dispose()
                tblFlowSheet = Nothing
            End If
            If (IsNothing(ocls_LM_LabResult) = False) Then
                ocls_LM_LabResult.Dispose()
                ocls_LM_LabResult = Nothing
            End If
            Try
                If (IsNothing(ColorDialog1) = False) Then
                    ColorDialog1.Dispose()
                    ColorDialog1 = Nothing
                End If
            Catch ex As Exception

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
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents pnlMask As System.Windows.Forms.Panel
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    'Friend WithEvents mskFormat As AxMSMask.AxMaskEdBox
    Friend WithEvents btnBackColor As System.Windows.Forms.Button
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents btnForeColor As System.Windows.Forms.Button
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents btnAdd As System.Windows.Forms.Button
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents chkUnderline As System.Windows.Forms.CheckBox
    Friend WithEvents chkItalic As System.Windows.Forms.CheckBox
    Friend WithEvents chkBold As System.Windows.Forms.CheckBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtColumnName As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtColumnWidth As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cmbAllinment As System.Windows.Forms.ComboBox
    Friend WithEvents cmbFontName As System.Windows.Forms.ComboBox
    Friend WithEvents cmbFormat As System.Windows.Forms.ComboBox
    Friend WithEvents ColorDialog1 As System.Windows.Forms.ColorDialog
    Friend WithEvents cmbFontSize As System.Windows.Forms.ComboBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents grdFlowSheetDetails As System.Windows.Forms.DataGrid
    ' Friend WithEvents FontDialog1 As System.Windows.Forms.FontDialog
    Friend WithEvents txtNoofColoumn As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtFlowSheetName As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmbColumnNo As System.Windows.Forms.ComboBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_LM_MST_LabResult))
        Me.pnlMask = New System.Windows.Forms.Panel
        Me.mskFormat = New System.Windows.Forms.MaskedTextBox
        Me.Label13 = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.btnBackColor = New System.Windows.Forms.Button
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.btnForeColor = New System.Windows.Forms.Button
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.btnAdd = New System.Windows.Forms.Button
        Me.btnDelete = New System.Windows.Forms.Button
        Me.chkUnderline = New System.Windows.Forms.CheckBox
        Me.chkItalic = New System.Windows.Forms.CheckBox
        Me.chkBold = New System.Windows.Forms.CheckBox
        Me.Label11 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.txtColumnName = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.txtColumnWidth = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.cmbAllinment = New System.Windows.Forms.ComboBox
        Me.cmbFontName = New System.Windows.Forms.ComboBox
        Me.cmbFormat = New System.Windows.Forms.ComboBox
        Me.ColorDialog1 = New System.Windows.Forms.ColorDialog
        Me.cmbFontSize = New System.Windows.Forms.ComboBox
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.grdFlowSheetDetails = New System.Windows.Forms.DataGrid
        '    Me.FontDialog1 = New System.Windows.Forms.FontDialog
        Me.txtNoofColoumn = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtFlowSheetName = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.cmbColumnNo = New System.Windows.Forms.ComboBox
        Me.pnlTop = New System.Windows.Forms.Panel
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.tlsLabResult = New gloGlobal.gloToolStripIgnoreFocus
        Me.btn_tls_Close = New System.Windows.Forms.ToolStripButton
        Me.btn_tls_Save = New System.Windows.Forms.ToolStripButton
        Me.pnlMask.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.grdFlowSheetDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlTop.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.tlsLabResult.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlMask
        '
        Me.pnlMask.BackColor = System.Drawing.Color.Transparent
        Me.pnlMask.Controls.Add(Me.mskFormat)
        Me.pnlMask.Controls.Add(Me.Label13)
        Me.pnlMask.Controls.Add(Me.Label12)
        Me.pnlMask.Location = New System.Drawing.Point(411, 75)
        Me.pnlMask.Name = "pnlMask"
        Me.pnlMask.Size = New System.Drawing.Size(184, 56)
        Me.pnlMask.TabIndex = 56
        '
        'mskFormat
        '
        Me.mskFormat.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.mskFormat.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.mskFormat.Location = New System.Drawing.Point(57, 10)
        Me.mskFormat.Name = "mskFormat"
        Me.mskFormat.Size = New System.Drawing.Size(100, 22)
        Me.mskFormat.TabIndex = 33
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(54, 34)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(93, 14)
        Me.Label13.TabIndex = 32
        Me.Label13.Text = "(eg. #'##'' ft)"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(12, 11)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(39, 14)
        Me.Label12.TabIndex = 31
        Me.Label12.Text = "Mask"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnBackColor
        '
        Me.btnBackColor.BackColor = System.Drawing.Color.White
        Me.btnBackColor.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnBackColor.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnBackColor.Location = New System.Drawing.Point(556, 51)
        Me.btnBackColor.Name = "btnBackColor"
        Me.btnBackColor.Size = New System.Drawing.Size(32, 22)
        Me.btnBackColor.TabIndex = 42
        Me.btnBackColor.UseVisualStyleBackColor = False
        '
        'ImageList1
        '
        Me.ImageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit
        Me.ImageList1.ImageSize = New System.Drawing.Size(16, 16)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        '
        'btnForeColor
        '
        Me.btnForeColor.BackColor = System.Drawing.Color.Black
        Me.btnForeColor.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnForeColor.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnForeColor.Location = New System.Drawing.Point(427, 51)
        Me.btnForeColor.Name = "btnForeColor"
        Me.btnForeColor.Size = New System.Drawing.Size(32, 22)
        Me.btnForeColor.TabIndex = 39
        Me.btnForeColor.UseVisualStyleBackColor = False
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Transparent
        Me.Panel2.Controls.Add(Me.btnAdd)
        Me.Panel2.Controls.Add(Me.btnDelete)
        Me.Panel2.Location = New System.Drawing.Point(435, 139)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(152, 24)
        Me.Panel2.TabIndex = 48
        '
        'btnAdd
        '
        Me.btnAdd.BackgroundImage = CType(resources.GetObject("btnAdd.BackgroundImage"), System.Drawing.Image)
        Me.btnAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnAdd.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAdd.Location = New System.Drawing.Point(8, 0)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(72, 24)
        Me.btnAdd.TabIndex = 0
        Me.btnAdd.Text = "&Add"
        '
        'btnDelete
        '
        Me.btnDelete.BackgroundImage = CType(resources.GetObject("btnDelete.BackgroundImage"), System.Drawing.Image)
        Me.btnDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnDelete.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDelete.Location = New System.Drawing.Point(80, 0)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(72, 24)
        Me.btnDelete.TabIndex = 2
        Me.btnDelete.Text = "&Delete"
        '
        'chkUnderline
        '
        Me.chkUnderline.BackColor = System.Drawing.Color.Transparent
        Me.chkUnderline.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chkUnderline.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.chkUnderline.Location = New System.Drawing.Point(199, 115)
        Me.chkUnderline.Name = "chkUnderline"
        Me.chkUnderline.Size = New System.Drawing.Size(88, 16)
        Me.chkUnderline.TabIndex = 46
        Me.chkUnderline.Text = "Underline"
        Me.chkUnderline.UseVisualStyleBackColor = False
        '
        'chkItalic
        '
        Me.chkItalic.BackColor = System.Drawing.Color.Transparent
        Me.chkItalic.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chkItalic.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.chkItalic.Location = New System.Drawing.Point(115, 115)
        Me.chkItalic.Name = "chkItalic"
        Me.chkItalic.Size = New System.Drawing.Size(64, 16)
        Me.chkItalic.TabIndex = 45
        Me.chkItalic.Text = "Italic"
        Me.chkItalic.UseVisualStyleBackColor = False
        '
        'chkBold
        '
        Me.chkBold.BackColor = System.Drawing.Color.Transparent
        Me.chkBold.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chkBold.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.chkBold.Location = New System.Drawing.Point(39, 115)
        Me.chkBold.Name = "chkBold"
        Me.chkBold.Size = New System.Drawing.Size(56, 16)
        Me.chkBold.TabIndex = 43
        Me.chkBold.Text = "Bold"
        Me.chkBold.UseVisualStyleBackColor = False
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.Color.Transparent
        Me.Label11.Location = New System.Drawing.Point(477, 55)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(73, 14)
        Me.Label11.TabIndex = 55
        Me.Label11.Text = "Back Color"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Location = New System.Drawing.Point(218, 87)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(51, 14)
        Me.Label7.TabIndex = 51
        Me.Label7.Text = "Format"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Location = New System.Drawing.Point(9, 87)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(69, 14)
        Me.Label10.TabIndex = 54
        Me.Label10.Text = "Alignment"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Location = New System.Drawing.Point(3, 55)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(75, 14)
        Me.Label8.TabIndex = 52
        Me.Label8.Text = "Font Name"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtColumnName
        '
        Me.txtColumnName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtColumnName.Location = New System.Drawing.Point(275, 11)
        Me.txtColumnName.Name = "txtColumnName"
        Me.txtColumnName.Size = New System.Drawing.Size(128, 22)
        Me.txtColumnName.TabIndex = 33
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Location = New System.Drawing.Point(219, 55)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(65, 14)
        Me.Label5.TabIndex = 44
        Me.Label5.Text = "Font Size"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtColumnWidth
        '
        Me.txtColumnWidth.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtColumnWidth.Location = New System.Drawing.Point(540, 11)
        Me.txtColumnWidth.Name = "txtColumnWidth"
        Me.txtColumnWidth.Size = New System.Drawing.Size(48, 22)
        Me.txtColumnWidth.TabIndex = 34
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Location = New System.Drawing.Point(3, 13)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(75, 14)
        Me.Label4.TabIndex = 35
        Me.Label4.Text = "Column No"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Location = New System.Drawing.Point(179, 11)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(94, 14)
        Me.Label6.TabIndex = 47
        Me.Label6.Text = "Column Name"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Location = New System.Drawing.Point(349, 55)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(72, 14)
        Me.Label9.TabIndex = 53
        Me.Label9.Text = "Fore Color"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Location = New System.Drawing.Point(423, 11)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(108, 32)
        Me.Label3.TabIndex = 37
        Me.Label3.Text = "Column Width (In Pixels)"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmbAllinment
        '
        Me.cmbAllinment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbAllinment.Location = New System.Drawing.Point(82, 83)
        Me.cmbAllinment.Name = "cmbAllinment"
        Me.cmbAllinment.Size = New System.Drawing.Size(128, 22)
        Me.cmbAllinment.TabIndex = 40
        '
        'cmbFontName
        '
        Me.cmbFontName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbFontName.Location = New System.Drawing.Point(83, 51)
        Me.cmbFontName.Name = "cmbFontName"
        Me.cmbFontName.Size = New System.Drawing.Size(128, 22)
        Me.cmbFontName.TabIndex = 36
        '
        'cmbFormat
        '
        Me.cmbFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbFormat.Location = New System.Drawing.Point(274, 83)
        Me.cmbFormat.Name = "cmbFormat"
        Me.cmbFormat.Size = New System.Drawing.Size(128, 22)
        Me.cmbFormat.TabIndex = 41
        '
        'cmbFontSize
        '
        Me.cmbFontSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbFontSize.Location = New System.Drawing.Point(291, 51)
        Me.cmbFontSize.Name = "cmbFontSize"
        Me.cmbFontSize.Size = New System.Drawing.Size(48, 22)
        Me.cmbFontSize.TabIndex = 38
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.grdFlowSheetDetails)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 92)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(616, 322)
        Me.Panel1.TabIndex = 50
        '
        'grdFlowSheetDetails
        '
        Me.grdFlowSheetDetails.AlternatingBackColor = System.Drawing.Color.FromArgb(CType(CType(214, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.grdFlowSheetDetails.BackColor = System.Drawing.Color.GhostWhite
        Me.grdFlowSheetDetails.BackgroundColor = System.Drawing.Color.GhostWhite
        Me.grdFlowSheetDetails.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.grdFlowSheetDetails.CaptionBackColor = System.Drawing.Color.FromArgb(CType(CType(4, Byte), Integer), CType(CType(96, Byte), Integer), CType(CType(162, Byte), Integer))
        Me.grdFlowSheetDetails.CaptionForeColor = System.Drawing.Color.White
        Me.grdFlowSheetDetails.CaptionVisible = False
        Me.grdFlowSheetDetails.DataMember = ""
        Me.grdFlowSheetDetails.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdFlowSheetDetails.ForeColor = System.Drawing.Color.Black
        Me.grdFlowSheetDetails.GridLineColor = System.Drawing.Color.Black
        Me.grdFlowSheetDetails.HeaderBackColor = System.Drawing.Color.FromArgb(CType(CType(4, Byte), Integer), CType(CType(96, Byte), Integer), CType(CType(162, Byte), Integer))
        Me.grdFlowSheetDetails.HeaderFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdFlowSheetDetails.HeaderForeColor = System.Drawing.Color.White
        Me.grdFlowSheetDetails.LinkColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.grdFlowSheetDetails.Location = New System.Drawing.Point(0, 0)
        Me.grdFlowSheetDetails.Name = "grdFlowSheetDetails"
        Me.grdFlowSheetDetails.ParentRowsBackColor = System.Drawing.Color.FromArgb(CType(CType(242, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.grdFlowSheetDetails.ParentRowsForeColor = System.Drawing.Color.Black
        Me.grdFlowSheetDetails.ReadOnly = True
        Me.grdFlowSheetDetails.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.grdFlowSheetDetails.SelectionForeColor = System.Drawing.Color.Black
        Me.grdFlowSheetDetails.Size = New System.Drawing.Size(616, 322)
        Me.grdFlowSheetDetails.TabIndex = 0
        '
        'txtNoofColoumn
        '
        Me.txtNoofColoumn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNoofColoumn.Location = New System.Drawing.Point(469, 9)
        Me.txtNoofColoumn.MaxLength = 2
        Me.txtNoofColoumn.Name = "txtNoofColoumn"
        Me.txtNoofColoumn.Size = New System.Drawing.Size(48, 22)
        Me.txtNoofColoumn.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Location = New System.Drawing.Point(365, 13)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(98, 14)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "No of Columns"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtFlowSheetName
        '
        Me.txtFlowSheetName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtFlowSheetName.Location = New System.Drawing.Point(146, 9)
        Me.txtFlowSheetName.Name = "txtFlowSheetName"
        Me.txtFlowSheetName.Size = New System.Drawing.Size(160, 22)
        Me.txtFlowSheetName.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Location = New System.Drawing.Point(23, 13)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(117, 14)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Flow Sheet Name"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmbColumnNo
        '
        Me.cmbColumnNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbColumnNo.Location = New System.Drawing.Point(83, 9)
        Me.cmbColumnNo.Name = "cmbColumnNo"
        Me.cmbColumnNo.Size = New System.Drawing.Size(56, 22)
        Me.cmbColumnNo.TabIndex = 32
        '
        'pnlTop
        '
        Me.pnlTop.BackgroundImage = CType(resources.GetObject("pnlTop.BackgroundImage"), System.Drawing.Image)
        Me.pnlTop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlTop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlTop.Controls.Add(Me.Label4)
        Me.pnlTop.Controls.Add(Me.Panel2)
        Me.pnlTop.Controls.Add(Me.btnForeColor)
        Me.pnlTop.Controls.Add(Me.chkUnderline)
        Me.pnlTop.Controls.Add(Me.btnBackColor)
        Me.pnlTop.Controls.Add(Me.chkItalic)
        Me.pnlTop.Controls.Add(Me.pnlMask)
        Me.pnlTop.Controls.Add(Me.chkBold)
        Me.pnlTop.Controls.Add(Me.cmbColumnNo)
        Me.pnlTop.Controls.Add(Me.Label11)
        Me.pnlTop.Controls.Add(Me.cmbFontSize)
        Me.pnlTop.Controls.Add(Me.Label7)
        Me.pnlTop.Controls.Add(Me.cmbFormat)
        Me.pnlTop.Controls.Add(Me.Label10)
        Me.pnlTop.Controls.Add(Me.cmbFontName)
        Me.pnlTop.Controls.Add(Me.Label8)
        Me.pnlTop.Controls.Add(Me.cmbAllinment)
        Me.pnlTop.Controls.Add(Me.txtColumnName)
        Me.pnlTop.Controls.Add(Me.Label3)
        Me.pnlTop.Controls.Add(Me.Label5)
        Me.pnlTop.Controls.Add(Me.Label9)
        Me.pnlTop.Controls.Add(Me.txtColumnWidth)
        Me.pnlTop.Controls.Add(Me.Label6)
        Me.pnlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlTop.Location = New System.Drawing.Point(0, 92)
        Me.pnlTop.Name = "pnlTop"
        Me.pnlTop.Size = New System.Drawing.Size(616, 168)
        Me.pnlTop.TabIndex = 57
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Transparent
        Me.Panel3.BackgroundImage = CType(resources.GetObject("Panel3.BackgroundImage"), System.Drawing.Image)
        Me.Panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel3.Controls.Add(Me.txtNoofColoumn)
        Me.Panel3.Controls.Add(Me.Label1)
        Me.Panel3.Controls.Add(Me.txtFlowSheetName)
        Me.Panel3.Controls.Add(Me.Label2)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(0, 52)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(616, 40)
        Me.Panel3.TabIndex = 58
        '
        'tlsLabResult
        '
        Me.tlsLabResult.BackColor = System.Drawing.Color.Transparent
        Me.tlsLabResult.BackgroundImage = CType(resources.GetObject("tlsLabResult.BackgroundImage"), System.Drawing.Image)
        Me.tlsLabResult.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlsLabResult.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tlsLabResult.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btn_tls_Save, Me.btn_tls_Close})
        Me.tlsLabResult.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.tlsLabResult.Location = New System.Drawing.Point(0, 0)
        Me.tlsLabResult.Name = "tlsLabResult"
        Me.tlsLabResult.Size = New System.Drawing.Size(616, 52)
        Me.tlsLabResult.TabIndex = 59
        Me.tlsLabResult.Text = "toolStrip1"
        '
        'btn_tls_Close
        '
        Me.btn_tls_Close.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_tls_Close.Image = CType(resources.GetObject("btn_tls_Close.Image"), System.Drawing.Image)
        Me.btn_tls_Close.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btn_tls_Close.Name = "btn_tls_Close"
        Me.btn_tls_Close.Size = New System.Drawing.Size(44, 49)
        Me.btn_tls_Close.Tag = "Close"
        Me.btn_tls_Close.Text = " &Close"
        Me.btn_tls_Close.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'btn_tls_Save
        '
        Me.btn_tls_Save.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_tls_Save.Image = CType(resources.GetObject("btn_tls_Save.Image"), System.Drawing.Image)
        Me.btn_tls_Save.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btn_tls_Save.Name = "btn_tls_Save"
        Me.btn_tls_Save.Size = New System.Drawing.Size(84, 49)
        Me.btn_tls_Save.Tag = "Save"
        Me.btn_tls_Save.Text = "&Save && Close"
        Me.btn_tls_Save.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'frm_LM_MST_LabResult
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(7, 15)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(244, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(253, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(616, 414)
        Me.Controls.Add(Me.pnlTop)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.tlsLabResult)
        Me.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frm_LM_MST_LabResult"
        Me.Text = "  Define Lab Result Flow Sheet"
        Me.pnlMask.ResumeLayout(False)
        Me.pnlMask.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        CType(Me.grdFlowSheetDetails, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlTop.ResumeLayout(False)
        Me.pnlTop.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.tlsLabResult.ResumeLayout(False)
        Me.tlsLabResult.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Private Sub frm_LM_MST_LabResult_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            ''to fill all fonts  Installed On this System
            Fill_Fontname()
            ''to fill fonts Size Installed On this System
            Fill_FontSize()
            ''to fill Allinments Installed On this System
            Fill_Allinment()
            Fill_Format()

            pnlMask.Visible = False

            GridFormat()
            grdFlowSheetDetails.DataSource = tblFlowSheet
            CustomGridStyle()

            txtColumnWidth.Text = 100

            If m_ID <> 0 Then
                grdFlowSheetDetails.ReadOnly = False
                ''''' m_IsInUse = True if then selected Flow sheet has Records stored in it
                If m_IsInUse = True Then
                    '' When FlowSheet is in Use
                    cmbFormat.Enabled = False
                    cmbFormat.BackColor = Color.White
                    cmbFormat.ForeColor = Color.Black

                    ''user should not Delete it 
                    btnDelete.Visible = False
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
                btnAdd.Visible = True
                btnAdd.Text = "Add"
                'btnUpdate.Visible = True
            Else

                cmbFormat.Enabled = True
                btnAdd.Visible = True
                btnAdd.Text = "Add"
                'btnUpdate.Visible = False
                txtNoofColoumn.Enabled = True
                'Else
                '    Dim objfrmFlowSheetView As New frmFlowSheetView
                '    cmbCategory.Text = m_CategoryType
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Fill_FlowSheet(ByVal ID As Long)
        'Dim ocls_LM_LabResult As New clsFlowSheet
        Dim dv As New DataView
        Dim i As Integer
        Dim r As DataRow
        ocls_LM_LabResult.SelectFlowSheet(ID)
        dv = ocls_LM_LabResult.GetDataview

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
            'Font Name
            r("Font Name") = dv.Item(i)(6).ToString
            'Font Size
            r("Font Size") = dv.Item(i)(7).ToString
            'IsBold
            If dv.Item(i)(9).ToString = True Then
                r("Is Bold") = "Bold"
            End If
            'IsItalic
            If dv.Item(i)(10).ToString = True Then
                r("Is Italic") = "Italic"
            End If
            'IsUnderline
            If dv.Item(i)(11).ToString = True Then
                r("Is Underline") = "Underline"
            End If
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
    End Sub

    Private Sub Fill_Fontname()
        Dim ff As FontFamily() = FontFamily.Families

        ' Loop and create a sample of each font.
        Dim i As Integer
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
    End Sub

    Private Sub Fill_FontSize()
        Dim i As Integer
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
    End Sub

    Public Sub Fill_Format()
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
    End Sub

    Public Sub Fill_Allinment()
        cmbAllinment.Items.Clear()
        cmbAllinment.Items.Add("Left")
        cmbAllinment.Items.Add("Center")
        cmbAllinment.Items.Add("Right")
        cmbAllinment.SelectedIndex = 0
    End Sub

    Private Sub txtNoofColoumn_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtNoofColoumn.TextChanged
        Try
            'If blnEdit = False Then
            Fill_ColNo()
            ' End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Fill_ColNo()

        On Error Resume Next

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
            MessageBox.Show("Number of columns must be greater than " & Max, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
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
            Dim ithObject As Object = tblFlowSheet.Rows(i)(0)
            For j = cmbColumnNo.Items.Count - 1 To 0 Step -1
                'SLR: Changed this logic on 4/2/2014
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

    End Sub

    '''''Private Sub btnColStyle_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnColStyle.Click
    '''''    FontDialog1.ShowDialog(Me)

    '''''    If FontDialog1.ShowDialog.OK = DialogResult.OK Then
    '''''        Dim r As DataRow

    '''''        'Add a new row
    '''''        r = tblFlowSheet.NewRow()
    '''''        'Specify the  col name to add value for the row
    '''''        r("No") = cmbColumnNo.Text
    '''''        r("ColumnName") = txtColumnName.Text
    '''''        r("Width") = txtColumnWidth.Text
    '''''        r("FontName") = FontDialog1.Font.Name.ToString
    '''''        r("FontSize") = FontDialog1.Font.Size.ToString

    '''''        '  r("FontColor") = FontDialog1.
    '''''        r("BackColor") = FontDialog1.Font.Size.ToString
    '''''        'Add the row to the tabledata
    '''''        tblFlowSheet.Rows.Add(r)

    '''''        'CustomGridStyle()
    '''''    End If
    '''''End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Try
            Dim r As DataRow
            If cmbColumnNo.Text = "" Then
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

            If grdFlowSheetDetails.CurrentRowIndex >= 0 AndAlso tblFlowSheet.Rows.Count >= 1 Then
                'Add a new row
                If blnEdit = True Then
                    tblFlowSheet.Rows.RemoveAt(grdFlowSheetDetails.CurrentRowIndex)
                End If

                r = tblFlowSheet.NewRow()
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
            r("Font Name") = cmbFontName.Text

            r("Font Size") = cmbFontSize.Text

            If chkBold.CheckState = CheckState.Checked Then
                r("Is Bold") = "Bold"
            Else
                r("Is Bold") = ""
            End If

            If chkItalic.CheckState = CheckState.Checked Then
                r("Is Italic") = "Italic"
            Else
                r("Is Italic") = ""
            End If

            If chkUnderline.CheckState = CheckState.Checked Then
                r("Is Underline") = "Underline"
            Else
                r("Is Underline") = ""
            End If
            r("Allinment") = cmbAllinment.Text
            r("Font Color") = CInt(btnForeColor.BackColor.ToArgb)     '10)

            'r("Font Color") = Color.FromName(picForeColor.BackColor.ToString)         '10
            r("Back Color") = CInt(btnBackColor.BackColor.ToArgb)   '11

            'Add the row to the tabledata
            tblFlowSheet.Rows.Add(r)

            grdFlowSheetDetails.DataSource = tblFlowSheet

            ''''''''''''''
            '' when column No gets added to grid it gets removed from combobox
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

            Reset()
            grdFlowSheetDetails.Enabled = True

            blnEdit = False
            btnAdd.Text = "Add"


        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Try
            If grdFlowSheetDetails.CurrentRowIndex >= 0 Then
                If MsgBox("Are you sure to Delete Details of Column " & grdFlowSheetDetails.Item(grdFlowSheetDetails.CurrentRowIndex, 1).ToString, MsgBoxStyle.YesNo) = Windows.Forms.DialogResult.Yes Then
                    tblFlowSheet.Rows.RemoveAt(grdFlowSheetDetails.CurrentRowIndex)
                    grdFlowSheetDetails.DataSource = tblFlowSheet
                    Call Reset()
                    cmbColumnNo.Enabled = True
                    blnEdit = False
                    btnAdd.Text = "Add"
                    grdFlowSheetDetails.Enabled = True
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Reset()
        txtColumnName.Text = ""
        cmbFormat.SelectedIndex = 0
        If (cmbFontName.Items.Count > 1) Then
            cmbFontName.SelectedIndex = 1
        Else
            If (cmbFontName.Items.Count > 0) Then
                cmbFontName.SelectedIndex = 0
            End If
        End If

        txtColumnWidth.Text = 100
        cmbFontSize.SelectedIndex = 0
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
        Dim colFontName As New DataColumn
        Dim colFontSize As New DataColumn
        Dim colFontColour As New DataColumn
        Dim colIsBold As New DataColumn
        Dim colIsItalic As New DataColumn
        Dim colIsUnderline As New DataColumn
        Dim colAllinment As New DataColumn
        Dim colBackColor As New DataColumn

        colNo.ColumnName = "No"  '0
        colName.ColumnName = "Column Name"  '1
        colFormat.ColumnName = "Format"  '2
        colWidth.ColumnName = "Width"  '3
        colFontName.ColumnName = "Font Name"  '4
        colFontSize.ColumnName = "Font Size"  '5
        colIsBold.ColumnName = "Is Bold"  '6
        ' colIsBold.DataType = GetType(Boolean)
        colIsItalic.ColumnName = "Is Italic"  '7
        ' colIsItalic.DataType = GetType(Boolean)
        colIsUnderline.ColumnName = "Is Underline"  '8
        ' colIsUnderline.DataType = GetType(Boolean)
        colAllinment.ColumnName = "Allinment"  '9
        colFontColour.ColumnName = "Font Color"  '10
        colBackColor.ColumnName = "Back Color"  '11

        tblFlowSheet.Columns.AddRange(New DataColumn() {colNo, colName, colFormat, colWidth, colFontName, colFontSize, colIsBold, colIsItalic, colIsUnderline, colAllinment, colFontColour, colBackColor})

    End Sub

    Private Sub grdFlowSheetDetails_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdFlowSheetDetails.DoubleClick
        Try
            'colNo, colName, colFormat, colWidth, colFontName, colFontSize,colIsBold(, colIsItalic, colIsUnderline, colAllinment, colFontColour, colBackColor)

            'If txtColumnName.Text <> "" Then
            '    btnAdd_Click(sender, e)
            'End If

            Dim i As Integer

            If grdFlowSheetDetails.CurrentRowIndex >= 0 Then

                blnEdit = True

                i = grdFlowSheetDetails.CurrentRowIndex

                'colNo
                cmbColumnNo.DropDownStyle = ComboBoxStyle.DropDown
                cmbColumnNo.Text = grdFlowSheetDetails.Item(i, 0).ToString
                cmbColumnNo.Enabled = False

                ' cmbColumnNo.DropDownStyle = ComboBoxStyle.DropDownList
                'colName
                txtColumnName.Text = grdFlowSheetDetails.Item(i, 1).ToString

                ''*********
                'colFormat
                Dim j As Integer
                Dim blnIsMasked As Boolean = True
                ''to Check that Format is Masked or Any Other 
                For j = 0 To cmbFormat.Items.Count - 1
                    If grdFlowSheetDetails.Item(i, 2).ToString = cmbFormat.Items(j).ToString Then
                        cmbFormat.Text = grdFlowSheetDetails.Item(i, 2).ToString
                        '''''if  Format is not "Masked" then
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
                'colFontName
                cmbFontName.Text = grdFlowSheetDetails.Item(i, 4).ToString
                'colFontSize
                cmbFontSize.Text = grdFlowSheetDetails.Item(i, 5).ToString
                If grdFlowSheetDetails.Item(i, 6).ToString = "" Then
                    chkBold.Checked = False
                Else
                    chkBold.Checked = True
                End If
                If grdFlowSheetDetails.Item(i, 7).ToString = "" Then
                    chkItalic.Checked = False
                Else
                    chkItalic.Checked = True
                End If
                If grdFlowSheetDetails.Item(i, 8).ToString = "" Then
                    chkUnderline.Checked = False
                Else
                    chkUnderline.Checked = True
                End If
                cmbAllinment.Text = grdFlowSheetDetails.Item(i, 9).ToString
                btnForeColor.BackColor = Color.FromArgb(grdFlowSheetDetails.Item(i, 10))
                btnBackColor.BackColor = Color.FromArgb(grdFlowSheetDetails.Item(i, 11))

                ''''' Add to new row
                'Specify the  col name to add value for the row

                'If i >= 0 Then
                '    tblFlowSheet.Rows.RemoveAt(i)
                '    'If tblFlowSheet.Rows.Count >= 1 Then
                '    grdFlowSheetDetails.DataSource = tblFlowSheet
                '    'End If
                '    'Add the row to the table
                '    ' tblFlowSheet.Rows.Add(r)
                'End If
                grdFlowSheetDetails.Enabled = False
                btnAdd.Visible = True
                btnAdd.Text = "Update"
                txtColumnName.Focus()

                ' btnUpdate.Visible = False
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub grdFlowSheetDetails_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grdFlowSheetDetails.MouseUp

        If (grdFlowSheetDetails.CurrentRowIndex) >= 0 Then
            grdFlowSheetDetails.Select(grdFlowSheetDetails.CurrentRowIndex)
        End If
    End Sub

    Public Sub CustomGridStyle()
        'Dim ts As New DataGridTableStyle

        'ts.ReadOnly = True
        'ts.AlternatingBackColor = System.Drawing.Color.Gainsboro
        'ts.BackColor = System.Drawing.Color.WhiteSmoke
        'ts.MappingName = tblFlowSheet.TableName.ToString
        'ts.HeaderBackColor = System.Drawing.Color.DimGray
        'ts.HeaderFont = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        'ts.ReadOnly = False
        'ts.RowHeadersVisible = False

        'Dim dv As DataView
        ' dv = ocls_LM_LabResult.GetDataview
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
            .Width = 150
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

        Dim FontName As New DataGridTextBoxColumn
        With FontName
            .Width = 100
            .MappingName = tblFlowSheet.Columns(4).ColumnName
            .HeaderText = "Font Name"
            .NullText = ""
        End With

        Dim FontSize As New DataGridTextBoxColumn
        With FontSize
            .Width = 75
            .MappingName = tblFlowSheet.Columns(5).ColumnName
            .HeaderText = "Font Size"
            .NullText = ""
        End With

        Dim IsBold As New DataGridTextBoxColumn
        With IsBold
            .Width = 75
            .MappingName = tblFlowSheet.Columns(6).ColumnName
            .HeaderText = "Is Bold"
            .NullText = ""
        End With

        Dim IsItalic As New DataGridTextBoxColumn
        With IsItalic
            .Width = 75
            .MappingName = tblFlowSheet.Columns(7).ColumnName
            .HeaderText = "Is Italic"
            .NullText = ""

        End With

        Dim IsUnderline As New DataGridTextBoxColumn
        With IsUnderline
            .Width = 85
            .MappingName = tblFlowSheet.Columns(8).ColumnName
            .HeaderText = "Is Underline"
            .NullText = ""
        End With

        Dim Allinment As New DataGridTextBoxColumn
        With Allinment
            .Width = 75
            .MappingName = tblFlowSheet.Columns(9).ColumnName
            .HeaderText = "Allinment"
            .NullText = ""
        End With

        Dim ForeColour As New DataGridTextBoxColumn
        With ForeColour
            .Width = 75
            .MappingName = tblFlowSheet.Columns(10).ColumnName
            .HeaderText = "Fore Color"
            .NullText = ""
        End With

        Dim BackColour As New DataGridTextBoxColumn
        With BackColour
            .Width = 75
            .MappingName = tblFlowSheet.Columns(11).ColumnName
            .HeaderText = "Back Color"
            .NullText = ""
        End With

        ts.GridColumnStyles.AddRange(New DataGridColumnStyle() {colNo, colName, colFormat, colWidth, FontName, FontSize, IsBold, IsItalic, IsUnderline, Allinment, ForeColour, BackColour})
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
            'Fill_ColNo()
            'End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnForeColor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnForeColor.Click
        Try
            'If (IsNothing(ColorDialog1) = False) Then
            '    ColorDialog1.Dispose()
            '    ColorDialog1 = Nothing
            'End If
            'ColorDialog1 = New ColorDialog()
            With ColorDialog1
                .AllowFullOpen = True
                .ShowHelp = False
                .Color = btnForeColor.BackColor
                Try
                    .CustomColors = gloGlobal.gloCustomColor.customColor
                Catch ex As Exception

                End Try
                If .ShowDialog(System.Windows.Forms.Form.ActiveForm) = Windows.Forms.DialogResult.OK Then
                    btnForeColor.BackColor = .Color
                    Try
                        gloGlobal.gloCustomColor.customColor = .CustomColors
                    Catch ex As Exception

                    End Try
                End If
            End With
            'ColorDialog1.Dispose()
            'ColorDialog1 = Nothing
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnBackColor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBackColor.Click
        Try
            'If (IsNothing(ColorDialog1) = False) Then
            '    ColorDialog1.Dispose()
            '    ColorDialog1 = Nothing
            'End If
            'ColorDialog1 = New ColorDialog()
            With ColorDialog1
                .AllowFullOpen = True
                .ShowHelp = False
                .Color = btnBackColor.BackColor
                Try
                    .CustomColors = gloGlobal.gloCustomColor.customColor
                Catch ex As Exception

                End Try
                If .ShowDialog(System.Windows.Forms.Form.ActiveForm) = Windows.Forms.DialogResult.OK Then
                    btnBackColor.BackColor = .Color
                    Try
                        gloGlobal.gloCustomColor.customColor = .CustomColors
                    Catch ex As Exception

                    End Try
                End If
            End With
            'ColorDialog1.Dispose()
            'ColorDialog1 = Nothing
        Catch ex As Exception
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

    Private Sub tlsLabResult_ItemClicked(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tlsLabResult.ItemClicked
        Select Case e.ClickedItem.Tag
            Case "Save"
                SaveResult()
            Case "Close"
                Try
                    _Cancel = True
                    gloWord.WordDialogBoxBackgroundCloser.Close(Me)
                    'Me.Close()
                Catch ex As Exception
                End Try

        End Select
    End Sub
    Private Sub SaveResult()
        Try
            If txtFlowSheetName.Text = "" Then
                MessageBox.Show("Flowsheet Name must be entered", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtFlowSheetName.Focus()
                Exit Sub
            End If

            If txtNoofColoumn.Text = "" Then
                MessageBox.Show("Number of columns must be entered", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtNoofColoumn.Focus()
                Exit Sub
            End If

            If ocls_LM_LabResult.CheckDuplicate(m_ID, Trim(txtFlowSheetName.Text)) = True Then
                MessageBox.Show("Flowsheet Name already exists", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtFlowSheetName.Focus()
                Exit Sub
            End If

            '''''''''''''''''
            If Trim(txtColumnName.Text) <> "" AndAlso Val(txtColumnWidth.Text) <> 0 AndAlso Val(cmbColumnNo.Text) <> 0 Then
                Dim r As DataRow

                If grdFlowSheetDetails.CurrentRowIndex >= 0 Then
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
                r("Font Name") = cmbFontName.Text  '4

                r("Font Size") = cmbFontSize.Text '5

                If chkBold.CheckState = CheckState.Checked Then
                    r("Is Bold") = "Bold"  ''6
                Else
                    r("Is Bold") = ""
                    'r("Is Bold") = CheckState.Checked
                End If

                If chkItalic.CheckState = CheckState.Checked Then
                    r("Is Italic") = "Italic"  '7
                Else
                    r("Is Italic") = ""
                End If

                If chkUnderline.CheckState = CheckState.Checked Then
                    r("Is Underline") = "Underline"  '8
                Else
                    r("Is Underline") = ""
                End If
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
            '    ocls_LM_LabResult.DeleteFlowSheet(m_ID, txtFlowSheetName.Text)
            'End If

            'Dim i As Integer
            'For i = 0 To tblFlowSheet.Rows.Count - 1
            'grdFlowSheetDetails.CurrentRowIndex = i
            '                                                                                       colNo,                                  colName,                                    colFormat,                             colWidth,                                   colFontName,                          colFontSize,                            colIsBold(,                             colIsItalic,                             colIsUnderline,                          colAllinment,                               colFontColour,                          colBackColor)
            'ocls_LM_LabResult.AddNewFlowSheet(i, m_ID, Trim(txtFlowSheetName.Text), Trim(txtNoofColoumn.Text), grdFlowSheetDetails.Item(i, 0).ToString, grdFlowSheetDetails.Item(i, 1).ToString, grdFlowSheetDetails.Item(i, 2).ToString, grdFlowSheetDetails.Item(i, 3).ToString, grdFlowSheetDetails.Item(i, 4).ToString, grdFlowSheetDetails.Item(i, 5).ToString, grdFlowSheetDetails.Item(i, 6).ToString, grdFlowSheetDetails.Item(i, 7).ToString, grdFlowSheetDetails.Item(i, 8).ToString, grdFlowSheetDetails.Item(i, 9).ToString, grdFlowSheetDetails.Item(i, 10).ToString, grdFlowSheetDetails.Item(i, 11).ToString)
            If tblFlowSheet.Rows.Count <= 0 Then
                MessageBox.Show("Flow Sheet does not contains proper information ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtColumnName.Focus()
                Exit Sub
            End If

            ocls_LM_LabResult.AddNewFlowSheet(m_ID, Trim(txtFlowSheetName.Text), Trim(txtNoofColoumn.Text), tblFlowSheet)
            'Next
            _FlowSheetName = Trim(txtFlowSheetName.Text)
            _Cancel = False
            ' Me.Close()
            gloWord.WordDialogBoxBackgroundCloser.Close(Me)

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

End Class
