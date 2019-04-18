Public Class HistoryMaster
    Inherits System.Windows.Forms.Form

    Public _CategoryID As Long
    Public _Description As String = ""
    'Sanjog
    Public _SelectedCategoty = ""
    Public strConceptID As String = ""
    Public strDescriptionID As String = ""
    Public strSnoMedID As String = ""
    Public strSnomedDescription As String = ""
    Public strSnomedDefination As String = ""
    '  Public strICD9 As String = ""
    Public strNDCCode As String = ""
    Public strRxNormCode As String = ""
    Public strLoincCode As String = ""
    Public strLoincDescr As String = ""
    Public strRefCode As String = ""
    Public strRefDescr As String = ""
    'Sanjog
    Private WithEvents dgCustomGrid As CustomTask
    Private Col_Check As Integer = 0
    Private Col_Name As Integer = 1
    Private Col_DGCustCnt As Integer = 2

    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Public _Comments As String = ""
    Private WithEvents pnl_tlsp As System.Windows.Forms.Panel
    Private WithEvents tlsp As gloGlobal.gloToolStripIgnoreFocus
    Private WithEvents ts_btnSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnClose As System.Windows.Forms.ToolStripButton
    Private WithEvents lbl_pnlBottom As System.Windows.Forms.Label
    Private WithEvents lbl_pnlLeft As System.Windows.Forms.Label
    Private WithEvents lbl_pnlRight As System.Windows.Forms.Label
    Private WithEvents lbl_pnlTop As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents ErrorProvider1 As System.Windows.Forms.ErrorProvider
    Friend WithEvents lbl_HistoryCaregory As System.Windows.Forms.Label
    Friend WithEvents lbl_ConceptID As System.Windows.Forms.Label
    Friend WithEvents btn_SnomedCode As System.Windows.Forms.Button
    Friend WithEvents cmb_HistoryCategory As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents imgTreeVIew As System.Windows.Forms.ImageList
    Friend WithEvents btn_Delete As System.Windows.Forms.Button
    Friend WithEvents txtSmokingStatus As System.Windows.Forms.TextBox
    Friend WithEvents lblSmokingStatus As System.Windows.Forms.Label
    Friend WithEvents btnSmokingStatus As System.Windows.Forms.Button
    Friend WithEvents PnlCustomTask As System.Windows.Forms.Panel
    Friend WithEvents pnlSmokingStatus As System.Windows.Forms.Panel
    Friend WithEvents pnlICD9Code As System.Windows.Forms.Panel
    Friend WithEvents txtICD9Code As System.Windows.Forms.TextBox
    Friend WithEvents btnbrowseICD9code As System.Windows.Forms.Button
    Friend WithEvents lblICD9Code As System.Windows.Forms.Label
    Public _DialogResult As Windows.Forms.DialogResult = Windows.Forms.DialogResult.Cancel
    Private dtHistoryType As DataTable
    Public nICDType As Integer = 9 ''parameter added for ICD10 implementation
    Private ToolTipbtnCloseICD9 As New System.Windows.Forms.ToolTip
    Private ToolTipbtnbrowseICD9code As New System.Windows.Forms.ToolTip
    Private ToolTipbtnCPTCode As New System.Windows.Forms.ToolTip
    Private ToolTipbtnCloseCPTCode As New System.Windows.Forms.ToolTip
    Private ToolTipbtn_SnomedCode As New System.Windows.Forms.ToolTip
    Private ToolTipbtn_Delete As New System.Windows.Forms.ToolTip
    Private ToolTipbtnClearSmokingStatus As New System.Windows.Forms.ToolTip

    Private ToolTipbtnSmokingStatus As New System.Windows.Forms.ToolTip
    Private ToolTiptxtICD9Code As System.Windows.Forms.ToolTip
    Private ToolTiptxtSmokingStatus As New System.Windows.Forms.ToolTip
    Private dtFillHistoryCategory As DataTable
    Private dtBindUserGridSmoking As DataTable
    Dim _dtHist As DataTable = Nothing
    Dim _dtHistCat As DataTable = Nothing
#Region "7010 Phase II Features"
    Dim ofrmDiagnosisList = New frmViewListControl
    Dim ofrmCPTList = New frmViewListControl
    Friend WithEvents btnCloseICD9 As System.Windows.Forms.Button
    Friend WithEvents FlpnlSmokingandICD9 As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents cmbHistoryType As System.Windows.Forms.ComboBox
    Friend WithEvents lblHistoryType As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents btnCloseCPTCode As System.Windows.Forms.Button
    Friend WithEvents txtCPTCode As System.Windows.Forms.TextBox
    Friend WithEvents btnCPTCode As System.Windows.Forms.Button
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Private oDiagnosisListControl As gloListControl.gloListControl
    Private WithEvents panel5 As System.Windows.Forms.Panel
    Private WithEvents label10 As System.Windows.Forms.Label
    Private WithEvents label9 As System.Windows.Forms.Label
    Private WithEvents label8 As System.Windows.Forms.Label
    Private WithEvents label7 As System.Windows.Forms.Label
    Private WithEvents txt_ConceptID As System.Windows.Forms.Label
    Friend WithEvents btnClearSmokingStatus As System.Windows.Forms.Button
    Friend WithEvents PnlHist As System.Windows.Forms.Panel
    Friend WithEvents cmbhist As System.Windows.Forms.ComboBox
    Friend WithEvents btnclosehist As System.Windows.Forms.Button
    Friend WithEvents btnhist As System.Windows.Forms.Button
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Private oCPTListControl As gloListControl.gloListControl
    Friend WithEvents pnlLoinc As System.Windows.Forms.Panel
    Friend WithEvents btncloseloinc As System.Windows.Forms.Button
    Friend WithEvents txtloinc As System.Windows.Forms.TextBox
    Friend WithEvents btnloinc As System.Windows.Forms.Button
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents pnlreason As System.Windows.Forms.Panel
    Friend WithEvents btnclrrefusal As System.Windows.Forms.Button
    Friend WithEvents txtSNOMEDRefusedCode As System.Windows.Forms.TextBox
    Friend WithEvents btnrefusal As System.Windows.Forms.Button
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Private oHistListControl As gloListControl.gloListControl
#End Region


#Region " Windows Form Designer generated code "

    Public Sub New(ByVal lnCategoryID As Long, Optional ByVal strHistory As String = "", Optional ByVal strCommets As String = "")
        MyBase.New()
        CategoryId = lnCategoryID
        _Description = strHistory
        _Comments = strCommets
        'This call is required by the Windows Form Designer.
        InitializeComponent()
        'Add any initialization after the InitializeComponent() call

    End Sub
    Public Sub New(ByVal lnCategoryID As Long, ByVal lnHistoryId As Long)
        MyBase.New()
        CategoryId = lnCategoryID
        HistoryId = lnHistoryId
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
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtDescription As System.Windows.Forms.TextBox
    Friend WithEvents txtComments As System.Windows.Forms.TextBox
    Private HistoryDBLayer As New ClsHistoryDBLayer
    Private CategoryId As Long
    Private HistoryId As Long
    Private errprovider As New ErrorProvider
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(HistoryMaster))
        Me.txtComments = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtDescription = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.panel5 = New System.Windows.Forms.Panel()
        Me.label10 = New System.Windows.Forms.Label()
        Me.label9 = New System.Windows.Forms.Label()
        Me.label8 = New System.Windows.Forms.Label()
        Me.label7 = New System.Windows.Forms.Label()
        Me.txt_ConceptID = New System.Windows.Forms.Label()
        Me.cmbHistoryType = New System.Windows.Forms.ComboBox()
        Me.lblHistoryType = New System.Windows.Forms.Label()
        Me.FlpnlSmokingandICD9 = New System.Windows.Forms.FlowLayoutPanel()
        Me.pnlSmokingStatus = New System.Windows.Forms.Panel()
        Me.btnClearSmokingStatus = New System.Windows.Forms.Button()
        Me.txtSmokingStatus = New System.Windows.Forms.TextBox()
        Me.btnSmokingStatus = New System.Windows.Forms.Button()
        Me.lblSmokingStatus = New System.Windows.Forms.Label()
        Me.pnlICD9Code = New System.Windows.Forms.Panel()
        Me.btnCloseICD9 = New System.Windows.Forms.Button()
        Me.txtICD9Code = New System.Windows.Forms.TextBox()
        Me.btnbrowseICD9code = New System.Windows.Forms.Button()
        Me.lblICD9Code = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.btnCloseCPTCode = New System.Windows.Forms.Button()
        Me.txtCPTCode = New System.Windows.Forms.TextBox()
        Me.btnCPTCode = New System.Windows.Forms.Button()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.PnlHist = New System.Windows.Forms.Panel()
        Me.cmbhist = New System.Windows.Forms.ComboBox()
        Me.btnclosehist = New System.Windows.Forms.Button()
        Me.btnhist = New System.Windows.Forms.Button()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.pnlLoinc = New System.Windows.Forms.Panel()
        Me.btncloseloinc = New System.Windows.Forms.Button()
        Me.txtloinc = New System.Windows.Forms.TextBox()
        Me.btnloinc = New System.Windows.Forms.Button()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.pnlreason = New System.Windows.Forms.Panel()
        Me.btnclrrefusal = New System.Windows.Forms.Button()
        Me.txtSNOMEDRefusedCode = New System.Windows.Forms.TextBox()
        Me.btnrefusal = New System.Windows.Forms.Button()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.btn_Delete = New System.Windows.Forms.Button()
        Me.cmb_HistoryCategory = New System.Windows.Forms.ComboBox()
        Me.lbl_HistoryCaregory = New System.Windows.Forms.Label()
        Me.lbl_ConceptID = New System.Windows.Forms.Label()
        Me.btn_SnomedCode = New System.Windows.Forms.Button()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lbl_pnlBottom = New System.Windows.Forms.Label()
        Me.lbl_pnlLeft = New System.Windows.Forms.Label()
        Me.lbl_pnlRight = New System.Windows.Forms.Label()
        Me.lbl_pnlTop = New System.Windows.Forms.Label()
        Me.PnlCustomTask = New System.Windows.Forms.Panel()
        Me.pnl_tlsp = New System.Windows.Forms.Panel()
        Me.tlsp = New gloGlobal.gloToolStripIgnoreFocus()
        Me.ts_btnSave = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnClose = New System.Windows.Forms.ToolStripButton()
        Me.imgTreeVIew = New System.Windows.Forms.ImageList(Me.components)
        Me.Panel2.SuspendLayout()
        Me.panel5.SuspendLayout()
        Me.FlpnlSmokingandICD9.SuspendLayout()
        Me.pnlSmokingStatus.SuspendLayout()
        Me.pnlICD9Code.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.PnlHist.SuspendLayout()
        Me.pnlLoinc.SuspendLayout()
        Me.pnlreason.SuspendLayout()
        Me.pnl_tlsp.SuspendLayout()
        Me.tlsp.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtComments
        '
        Me.txtComments.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtComments.ForeColor = System.Drawing.Color.Black
        Me.txtComments.Location = New System.Drawing.Point(213, 74)
        Me.txtComments.MaxLength = 255
        Me.txtComments.Name = "txtComments"
        Me.txtComments.Size = New System.Drawing.Size(243, 22)
        Me.txtComments.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(134, 79)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(73, 14)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Comments :"
        '
        'txtDescription
        '
        Me.txtDescription.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDescription.ForeColor = System.Drawing.Color.Black
        Me.txtDescription.Location = New System.Drawing.Point(213, 44)
        Me.txtDescription.MaxLength = 255
        Me.txtDescription.Name = "txtDescription"
        Me.txtDescription.Size = New System.Drawing.Size(243, 22)
        Me.txtDescription.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(102, 48)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(105, 14)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Item Description :"
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Transparent
        Me.Panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel2.Controls.Add(Me.panel5)
        Me.Panel2.Controls.Add(Me.cmbHistoryType)
        Me.Panel2.Controls.Add(Me.lblHistoryType)
        Me.Panel2.Controls.Add(Me.FlpnlSmokingandICD9)
        Me.Panel2.Controls.Add(Me.btn_Delete)
        Me.Panel2.Controls.Add(Me.cmb_HistoryCategory)
        Me.Panel2.Controls.Add(Me.lbl_HistoryCaregory)
        Me.Panel2.Controls.Add(Me.lbl_ConceptID)
        Me.Panel2.Controls.Add(Me.btn_SnomedCode)
        Me.Panel2.Controls.Add(Me.Label4)
        Me.Panel2.Controls.Add(Me.Label3)
        Me.Panel2.Controls.Add(Me.lbl_pnlBottom)
        Me.Panel2.Controls.Add(Me.lbl_pnlLeft)
        Me.Panel2.Controls.Add(Me.lbl_pnlRight)
        Me.Panel2.Controls.Add(Me.lbl_pnlTop)
        Me.Panel2.Controls.Add(Me.txtComments)
        Me.Panel2.Controls.Add(Me.Label2)
        Me.Panel2.Controls.Add(Me.Label1)
        Me.Panel2.Controls.Add(Me.txtDescription)
        Me.Panel2.Controls.Add(Me.PnlCustomTask)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel2.Location = New System.Drawing.Point(0, 53)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Padding = New System.Windows.Forms.Padding(3)
        Me.Panel2.Size = New System.Drawing.Size(625, 420)
        Me.Panel2.TabIndex = 4
        '
        'panel5
        '
        Me.panel5.BackColor = System.Drawing.Color.White
        Me.panel5.Controls.Add(Me.label10)
        Me.panel5.Controls.Add(Me.label9)
        Me.panel5.Controls.Add(Me.label8)
        Me.panel5.Controls.Add(Me.label7)
        Me.panel5.Controls.Add(Me.txt_ConceptID)
        Me.panel5.Location = New System.Drawing.Point(213, 134)
        Me.panel5.Name = "panel5"
        Me.panel5.Size = New System.Drawing.Size(243, 23)
        Me.panel5.TabIndex = 227
        '
        'label10
        '
        Me.label10.BackColor = System.Drawing.Color.Silver
        Me.label10.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.label10.Location = New System.Drawing.Point(1, 22)
        Me.label10.Name = "label10"
        Me.label10.Size = New System.Drawing.Size(241, 1)
        Me.label10.TabIndex = 229
        '
        'label9
        '
        Me.label9.BackColor = System.Drawing.Color.Silver
        Me.label9.Dock = System.Windows.Forms.DockStyle.Top
        Me.label9.Location = New System.Drawing.Point(1, 0)
        Me.label9.Name = "label9"
        Me.label9.Size = New System.Drawing.Size(241, 1)
        Me.label9.TabIndex = 228
        '
        'label8
        '
        Me.label8.BackColor = System.Drawing.Color.Silver
        Me.label8.Dock = System.Windows.Forms.DockStyle.Right
        Me.label8.Location = New System.Drawing.Point(242, 0)
        Me.label8.Name = "label8"
        Me.label8.Size = New System.Drawing.Size(1, 23)
        Me.label8.TabIndex = 227
        '
        'label7
        '
        Me.label7.BackColor = System.Drawing.Color.Silver
        Me.label7.Dock = System.Windows.Forms.DockStyle.Left
        Me.label7.Location = New System.Drawing.Point(0, 0)
        Me.label7.Name = "label7"
        Me.label7.Size = New System.Drawing.Size(1, 23)
        Me.label7.TabIndex = 226
        '
        'txt_ConceptID
        '
        Me.txt_ConceptID.AutoEllipsis = True
        Me.txt_ConceptID.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txt_ConceptID.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_ConceptID.ForeColor = System.Drawing.Color.Black
        Me.txt_ConceptID.Location = New System.Drawing.Point(0, 0)
        Me.txt_ConceptID.Name = "txt_ConceptID"
        Me.txt_ConceptID.Padding = New System.Windows.Forms.Padding(2, 2, 0, 0)
        Me.txt_ConceptID.Size = New System.Drawing.Size(243, 23)
        Me.txt_ConceptID.TabIndex = 225
        Me.txt_ConceptID.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cmbHistoryType
        '
        Me.cmbHistoryType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbHistoryType.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.cmbHistoryType.ForeColor = System.Drawing.Color.Black
        Me.cmbHistoryType.FormattingEnabled = True
        Me.cmbHistoryType.Location = New System.Drawing.Point(213, 104)
        Me.cmbHistoryType.Name = "cmbHistoryType"
        Me.cmbHistoryType.Size = New System.Drawing.Size(243, 22)
        Me.cmbHistoryType.TabIndex = 226
        '
        'lblHistoryType
        '
        Me.lblHistoryType.AutoSize = True
        Me.lblHistoryType.BackColor = System.Drawing.Color.Transparent
        Me.lblHistoryType.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHistoryType.Location = New System.Drawing.Point(123, 110)
        Me.lblHistoryType.Name = "lblHistoryType"
        Me.lblHistoryType.Size = New System.Drawing.Size(84, 14)
        Me.lblHistoryType.TabIndex = 225
        Me.lblHistoryType.Text = "History Type :"
        '
        'FlpnlSmokingandICD9
        '
        Me.FlpnlSmokingandICD9.Controls.Add(Me.pnlSmokingStatus)
        Me.FlpnlSmokingandICD9.Controls.Add(Me.pnlICD9Code)
        Me.FlpnlSmokingandICD9.Controls.Add(Me.Panel1)
        Me.FlpnlSmokingandICD9.Controls.Add(Me.PnlHist)
        Me.FlpnlSmokingandICD9.Controls.Add(Me.pnlLoinc)
        Me.FlpnlSmokingandICD9.Controls.Add(Me.pnlreason)
        Me.FlpnlSmokingandICD9.FlowDirection = System.Windows.Forms.FlowDirection.TopDown
        Me.FlpnlSmokingandICD9.Location = New System.Drawing.Point(60, 164)
        Me.FlpnlSmokingandICD9.Name = "FlpnlSmokingandICD9"
        Me.FlpnlSmokingandICD9.Size = New System.Drawing.Size(525, 223)
        Me.FlpnlSmokingandICD9.TabIndex = 224
        '
        'pnlSmokingStatus
        '
        Me.pnlSmokingStatus.Controls.Add(Me.btnClearSmokingStatus)
        Me.pnlSmokingStatus.Controls.Add(Me.txtSmokingStatus)
        Me.pnlSmokingStatus.Controls.Add(Me.btnSmokingStatus)
        Me.pnlSmokingStatus.Controls.Add(Me.lblSmokingStatus)
        Me.pnlSmokingStatus.Location = New System.Drawing.Point(3, 3)
        Me.pnlSmokingStatus.Name = "pnlSmokingStatus"
        Me.pnlSmokingStatus.Size = New System.Drawing.Size(472, 28)
        Me.pnlSmokingStatus.TabIndex = 222
        Me.pnlSmokingStatus.Visible = False
        '
        'btnClearSmokingStatus
        '
        Me.btnClearSmokingStatus.BackColor = System.Drawing.Color.Transparent
        Me.btnClearSmokingStatus.BackgroundImage = CType(resources.GetObject("btnClearSmokingStatus.BackgroundImage"), System.Drawing.Image)
        Me.btnClearSmokingStatus.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnClearSmokingStatus.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnClearSmokingStatus.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnClearSmokingStatus.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClearSmokingStatus.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClearSmokingStatus.Image = CType(resources.GetObject("btnClearSmokingStatus.Image"), System.Drawing.Image)
        Me.btnClearSmokingStatus.Location = New System.Drawing.Point(423, 5)
        Me.btnClearSmokingStatus.Name = "btnClearSmokingStatus"
        Me.btnClearSmokingStatus.Size = New System.Drawing.Size(21, 21)
        Me.btnClearSmokingStatus.TabIndex = 221
        Me.btnClearSmokingStatus.UseVisualStyleBackColor = False
        '
        'txtSmokingStatus
        '
        Me.txtSmokingStatus.Enabled = False
        Me.txtSmokingStatus.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSmokingStatus.ForeColor = System.Drawing.Color.Black
        Me.txtSmokingStatus.Location = New System.Drawing.Point(150, 4)
        Me.txtSmokingStatus.MaxLength = 255
        Me.txtSmokingStatus.Name = "txtSmokingStatus"
        Me.txtSmokingStatus.Size = New System.Drawing.Size(243, 22)
        Me.txtSmokingStatus.TabIndex = 219
        '
        'btnSmokingStatus
        '
        Me.btnSmokingStatus.BackColor = System.Drawing.Color.Transparent
        Me.btnSmokingStatus.BackgroundImage = CType(resources.GetObject("btnSmokingStatus.BackgroundImage"), System.Drawing.Image)
        Me.btnSmokingStatus.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnSmokingStatus.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnSmokingStatus.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnSmokingStatus.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSmokingStatus.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSmokingStatus.Image = CType(resources.GetObject("btnSmokingStatus.Image"), System.Drawing.Image)
        Me.btnSmokingStatus.Location = New System.Drawing.Point(399, 5)
        Me.btnSmokingStatus.Name = "btnSmokingStatus"
        Me.btnSmokingStatus.Size = New System.Drawing.Size(21, 21)
        Me.btnSmokingStatus.TabIndex = 218
        Me.btnSmokingStatus.Text = "strSnomedDescription"
        Me.btnSmokingStatus.UseVisualStyleBackColor = False
        '
        'lblSmokingStatus
        '
        Me.lblSmokingStatus.AutoSize = True
        Me.lblSmokingStatus.BackColor = System.Drawing.Color.Transparent
        Me.lblSmokingStatus.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSmokingStatus.Location = New System.Drawing.Point(44, 8)
        Me.lblSmokingStatus.Name = "lblSmokingStatus"
        Me.lblSmokingStatus.Size = New System.Drawing.Size(100, 14)
        Me.lblSmokingStatus.TabIndex = 220
        Me.lblSmokingStatus.Text = "Smoking Status :"
        '
        'pnlICD9Code
        '
        Me.pnlICD9Code.Controls.Add(Me.btnCloseICD9)
        Me.pnlICD9Code.Controls.Add(Me.txtICD9Code)
        Me.pnlICD9Code.Controls.Add(Me.btnbrowseICD9code)
        Me.pnlICD9Code.Controls.Add(Me.lblICD9Code)
        Me.pnlICD9Code.Location = New System.Drawing.Point(3, 37)
        Me.pnlICD9Code.Name = "pnlICD9Code"
        Me.pnlICD9Code.Size = New System.Drawing.Size(472, 28)
        Me.pnlICD9Code.TabIndex = 223
        '
        'btnCloseICD9
        '
        Me.btnCloseICD9.BackColor = System.Drawing.Color.Transparent
        Me.btnCloseICD9.BackgroundImage = CType(resources.GetObject("btnCloseICD9.BackgroundImage"), System.Drawing.Image)
        Me.btnCloseICD9.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnCloseICD9.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnCloseICD9.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnCloseICD9.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCloseICD9.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCloseICD9.Image = CType(resources.GetObject("btnCloseICD9.Image"), System.Drawing.Image)
        Me.btnCloseICD9.Location = New System.Drawing.Point(423, 5)
        Me.btnCloseICD9.Name = "btnCloseICD9"
        Me.btnCloseICD9.Size = New System.Drawing.Size(21, 21)
        Me.btnCloseICD9.TabIndex = 221
        Me.btnCloseICD9.UseVisualStyleBackColor = False
        '
        'txtICD9Code
        '
        Me.txtICD9Code.Enabled = False
        Me.txtICD9Code.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtICD9Code.ForeColor = System.Drawing.Color.Black
        Me.txtICD9Code.Location = New System.Drawing.Point(150, 4)
        Me.txtICD9Code.MaxLength = 255
        Me.txtICD9Code.Name = "txtICD9Code"
        Me.txtICD9Code.Size = New System.Drawing.Size(243, 22)
        Me.txtICD9Code.TabIndex = 219
        '
        'btnbrowseICD9code
        '
        Me.btnbrowseICD9code.BackColor = System.Drawing.Color.Transparent
        Me.btnbrowseICD9code.BackgroundImage = CType(resources.GetObject("btnbrowseICD9code.BackgroundImage"), System.Drawing.Image)
        Me.btnbrowseICD9code.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnbrowseICD9code.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnbrowseICD9code.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnbrowseICD9code.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnbrowseICD9code.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnbrowseICD9code.Image = CType(resources.GetObject("btnbrowseICD9code.Image"), System.Drawing.Image)
        Me.btnbrowseICD9code.Location = New System.Drawing.Point(399, 5)
        Me.btnbrowseICD9code.Name = "btnbrowseICD9code"
        Me.btnbrowseICD9code.Size = New System.Drawing.Size(21, 21)
        Me.btnbrowseICD9code.TabIndex = 218
        Me.btnbrowseICD9code.Text = "strSnomedDescription"
        Me.btnbrowseICD9code.UseVisualStyleBackColor = False
        '
        'lblICD9Code
        '
        Me.lblICD9Code.AutoSize = True
        Me.lblICD9Code.BackColor = System.Drawing.Color.Transparent
        Me.lblICD9Code.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblICD9Code.Location = New System.Drawing.Point(84, 8)
        Me.lblICD9Code.Name = "lblICD9Code"
        Me.lblICD9Code.Size = New System.Drawing.Size(60, 14)
        Me.lblICD9Code.TabIndex = 220
        Me.lblICD9Code.Text = "ICD9/10 :"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.btnCloseCPTCode)
        Me.Panel1.Controls.Add(Me.txtCPTCode)
        Me.Panel1.Controls.Add(Me.btnCPTCode)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Location = New System.Drawing.Point(3, 71)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(472, 28)
        Me.Panel1.TabIndex = 224
        '
        'btnCloseCPTCode
        '
        Me.btnCloseCPTCode.BackColor = System.Drawing.Color.Transparent
        Me.btnCloseCPTCode.BackgroundImage = CType(resources.GetObject("btnCloseCPTCode.BackgroundImage"), System.Drawing.Image)
        Me.btnCloseCPTCode.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnCloseCPTCode.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnCloseCPTCode.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnCloseCPTCode.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCloseCPTCode.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCloseCPTCode.Image = CType(resources.GetObject("btnCloseCPTCode.Image"), System.Drawing.Image)
        Me.btnCloseCPTCode.Location = New System.Drawing.Point(423, 5)
        Me.btnCloseCPTCode.Name = "btnCloseCPTCode"
        Me.btnCloseCPTCode.Size = New System.Drawing.Size(21, 21)
        Me.btnCloseCPTCode.TabIndex = 221
        Me.btnCloseCPTCode.UseVisualStyleBackColor = False
        '
        'txtCPTCode
        '
        Me.txtCPTCode.Enabled = False
        Me.txtCPTCode.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCPTCode.ForeColor = System.Drawing.Color.Black
        Me.txtCPTCode.Location = New System.Drawing.Point(150, 4)
        Me.txtCPTCode.MaxLength = 255
        Me.txtCPTCode.Name = "txtCPTCode"
        Me.txtCPTCode.Size = New System.Drawing.Size(243, 22)
        Me.txtCPTCode.TabIndex = 219
        '
        'btnCPTCode
        '
        Me.btnCPTCode.BackColor = System.Drawing.Color.Transparent
        Me.btnCPTCode.BackgroundImage = CType(resources.GetObject("btnCPTCode.BackgroundImage"), System.Drawing.Image)
        Me.btnCPTCode.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnCPTCode.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnCPTCode.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnCPTCode.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCPTCode.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCPTCode.Image = CType(resources.GetObject("btnCPTCode.Image"), System.Drawing.Image)
        Me.btnCPTCode.Location = New System.Drawing.Point(399, 5)
        Me.btnCPTCode.Name = "btnCPTCode"
        Me.btnCPTCode.Size = New System.Drawing.Size(21, 21)
        Me.btnCPTCode.TabIndex = 218
        Me.btnCPTCode.Text = "strSnomedDescription"
        Me.btnCPTCode.UseVisualStyleBackColor = False
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(107, 8)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(37, 14)
        Me.Label5.TabIndex = 220
        Me.Label5.Text = "CPT :"
        '
        'PnlHist
        '
        Me.PnlHist.Controls.Add(Me.cmbhist)
        Me.PnlHist.Controls.Add(Me.btnclosehist)
        Me.PnlHist.Controls.Add(Me.btnhist)
        Me.PnlHist.Controls.Add(Me.Label6)
        Me.PnlHist.Location = New System.Drawing.Point(3, 105)
        Me.PnlHist.Name = "PnlHist"
        Me.PnlHist.Size = New System.Drawing.Size(472, 28)
        Me.PnlHist.TabIndex = 228
        '
        'cmbhist
        '
        Me.cmbhist.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbhist.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.cmbhist.ForeColor = System.Drawing.Color.Black
        Me.cmbhist.FormattingEnabled = True
        Me.cmbhist.Location = New System.Drawing.Point(150, 2)
        Me.cmbhist.Name = "cmbhist"
        Me.cmbhist.Size = New System.Drawing.Size(243, 22)
        Me.cmbhist.TabIndex = 218
        '
        'btnclosehist
        '
        Me.btnclosehist.BackColor = System.Drawing.Color.Transparent
        Me.btnclosehist.BackgroundImage = CType(resources.GetObject("btnclosehist.BackgroundImage"), System.Drawing.Image)
        Me.btnclosehist.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnclosehist.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnclosehist.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnclosehist.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnclosehist.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclosehist.Image = CType(resources.GetObject("btnclosehist.Image"), System.Drawing.Image)
        Me.btnclosehist.Location = New System.Drawing.Point(423, 3)
        Me.btnclosehist.Name = "btnclosehist"
        Me.btnclosehist.Size = New System.Drawing.Size(21, 21)
        Me.btnclosehist.TabIndex = 220
        Me.btnclosehist.UseVisualStyleBackColor = False
        '
        'btnhist
        '
        Me.btnhist.BackColor = System.Drawing.Color.Transparent
        Me.btnhist.BackgroundImage = CType(resources.GetObject("btnhist.BackgroundImage"), System.Drawing.Image)
        Me.btnhist.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnhist.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnhist.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnhist.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnhist.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnhist.Image = CType(resources.GetObject("btnhist.Image"), System.Drawing.Image)
        Me.btnhist.Location = New System.Drawing.Point(399, 3)
        Me.btnhist.Name = "btnhist"
        Me.btnhist.Size = New System.Drawing.Size(21, 21)
        Me.btnhist.TabIndex = 219
        Me.btnhist.Text = "strSnomedDescription"
        Me.btnhist.UseVisualStyleBackColor = False
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(42, 6)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(102, 14)
        Me.Label6.TabIndex = 220
        Me.Label6.Text = "History Answers :"
        '
        'pnlLoinc
        '
        Me.pnlLoinc.Controls.Add(Me.btncloseloinc)
        Me.pnlLoinc.Controls.Add(Me.txtloinc)
        Me.pnlLoinc.Controls.Add(Me.btnloinc)
        Me.pnlLoinc.Controls.Add(Me.Label11)
        Me.pnlLoinc.Location = New System.Drawing.Point(3, 139)
        Me.pnlLoinc.Name = "pnlLoinc"
        Me.pnlLoinc.Size = New System.Drawing.Size(472, 28)
        Me.pnlLoinc.TabIndex = 229
        '
        'btncloseloinc
        '
        Me.btncloseloinc.BackColor = System.Drawing.Color.Transparent
        Me.btncloseloinc.BackgroundImage = CType(resources.GetObject("btncloseloinc.BackgroundImage"), System.Drawing.Image)
        Me.btncloseloinc.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btncloseloinc.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btncloseloinc.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btncloseloinc.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btncloseloinc.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btncloseloinc.Image = CType(resources.GetObject("btncloseloinc.Image"), System.Drawing.Image)
        Me.btncloseloinc.Location = New System.Drawing.Point(423, 4)
        Me.btncloseloinc.Name = "btncloseloinc"
        Me.btncloseloinc.Size = New System.Drawing.Size(21, 21)
        Me.btncloseloinc.TabIndex = 221
        Me.btncloseloinc.UseVisualStyleBackColor = False
        '
        'txtloinc
        '
        Me.txtloinc.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtloinc.ForeColor = System.Drawing.Color.Black
        Me.txtloinc.Location = New System.Drawing.Point(150, 3)
        Me.txtloinc.MaxLength = 255
        Me.txtloinc.Name = "txtloinc"
        Me.txtloinc.ReadOnly = True
        Me.txtloinc.Size = New System.Drawing.Size(243, 22)
        Me.txtloinc.TabIndex = 219
        '
        'btnloinc
        '
        Me.btnloinc.BackColor = System.Drawing.Color.Transparent
        Me.btnloinc.BackgroundImage = CType(resources.GetObject("btnloinc.BackgroundImage"), System.Drawing.Image)
        Me.btnloinc.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnloinc.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnloinc.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnloinc.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnloinc.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnloinc.Image = CType(resources.GetObject("btnloinc.Image"), System.Drawing.Image)
        Me.btnloinc.Location = New System.Drawing.Point(399, 4)
        Me.btnloinc.Name = "btnloinc"
        Me.btnloinc.Size = New System.Drawing.Size(21, 21)
        Me.btnloinc.TabIndex = 218
        Me.btnloinc.Text = "strSnomedDescription"
        Me.btnloinc.UseVisualStyleBackColor = False
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.Color.Transparent
        Me.Label11.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(63, 7)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(81, 14)
        Me.Label11.TabIndex = 220
        Me.Label11.Text = "LOINC Code :"
        '
        'pnlreason
        '
        Me.pnlreason.Controls.Add(Me.btnclrrefusal)
        Me.pnlreason.Controls.Add(Me.txtSNOMEDRefusedCode)
        Me.pnlreason.Controls.Add(Me.btnrefusal)
        Me.pnlreason.Controls.Add(Me.Label12)
        Me.pnlreason.Location = New System.Drawing.Point(3, 173)
        Me.pnlreason.Name = "pnlreason"
        Me.pnlreason.Size = New System.Drawing.Size(472, 28)
        Me.pnlreason.TabIndex = 230
        '
        'btnclrrefusal
        '
        Me.btnclrrefusal.BackColor = System.Drawing.Color.Transparent
        Me.btnclrrefusal.BackgroundImage = CType(resources.GetObject("btnclrrefusal.BackgroundImage"), System.Drawing.Image)
        Me.btnclrrefusal.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnclrrefusal.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnclrrefusal.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnclrrefusal.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnclrrefusal.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclrrefusal.Image = CType(resources.GetObject("btnclrrefusal.Image"), System.Drawing.Image)
        Me.btnclrrefusal.Location = New System.Drawing.Point(423, 4)
        Me.btnclrrefusal.Name = "btnclrrefusal"
        Me.btnclrrefusal.Size = New System.Drawing.Size(21, 21)
        Me.btnclrrefusal.TabIndex = 221
        Me.btnclrrefusal.UseVisualStyleBackColor = False
        '
        'txtSNOMEDRefusedCode
        '
        Me.txtSNOMEDRefusedCode.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSNOMEDRefusedCode.ForeColor = System.Drawing.Color.Black
        Me.txtSNOMEDRefusedCode.Location = New System.Drawing.Point(150, 3)
        Me.txtSNOMEDRefusedCode.MaxLength = 255
        Me.txtSNOMEDRefusedCode.Name = "txtSNOMEDRefusedCode"
        Me.txtSNOMEDRefusedCode.ReadOnly = True
        Me.txtSNOMEDRefusedCode.Size = New System.Drawing.Size(243, 22)
        Me.txtSNOMEDRefusedCode.TabIndex = 219
        '
        'btnrefusal
        '
        Me.btnrefusal.BackColor = System.Drawing.Color.Transparent
        Me.btnrefusal.BackgroundImage = CType(resources.GetObject("btnrefusal.BackgroundImage"), System.Drawing.Image)
        Me.btnrefusal.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnrefusal.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnrefusal.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnrefusal.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnrefusal.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnrefusal.Image = CType(resources.GetObject("btnrefusal.Image"), System.Drawing.Image)
        Me.btnrefusal.Location = New System.Drawing.Point(399, 4)
        Me.btnrefusal.Name = "btnrefusal"
        Me.btnrefusal.Size = New System.Drawing.Size(21, 21)
        Me.btnrefusal.TabIndex = 218
        Me.btnrefusal.Text = "strSnomedDescription"
        Me.btnrefusal.UseVisualStyleBackColor = False
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.Color.Transparent
        Me.Label12.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(15, 7)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(129, 14)
        Me.Label12.TabIndex = 220
        Me.Label12.Text = "Refusal\Reason Code :"
        '
        'btn_Delete
        '
        Me.btn_Delete.BackColor = System.Drawing.Color.Transparent
        Me.btn_Delete.BackgroundImage = CType(resources.GetObject("btn_Delete.BackgroundImage"), System.Drawing.Image)
        Me.btn_Delete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btn_Delete.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btn_Delete.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btn_Delete.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_Delete.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_Delete.Image = CType(resources.GetObject("btn_Delete.Image"), System.Drawing.Image)
        Me.btn_Delete.Location = New System.Drawing.Point(486, 136)
        Me.btn_Delete.Name = "btn_Delete"
        Me.btn_Delete.Size = New System.Drawing.Size(21, 21)
        Me.btn_Delete.TabIndex = 217
        Me.btn_Delete.UseVisualStyleBackColor = False
        '
        'cmb_HistoryCategory
        '
        Me.cmb_HistoryCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb_HistoryCategory.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.cmb_HistoryCategory.ForeColor = System.Drawing.Color.Black
        Me.cmb_HistoryCategory.FormattingEnabled = True
        Me.cmb_HistoryCategory.Location = New System.Drawing.Point(213, 14)
        Me.cmb_HistoryCategory.Name = "cmb_HistoryCategory"
        Me.cmb_HistoryCategory.Size = New System.Drawing.Size(243, 22)
        Me.cmb_HistoryCategory.TabIndex = 209
        '
        'lbl_HistoryCaregory
        '
        Me.lbl_HistoryCaregory.AutoSize = True
        Me.lbl_HistoryCaregory.BackColor = System.Drawing.Color.Transparent
        Me.lbl_HistoryCaregory.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_HistoryCaregory.Location = New System.Drawing.Point(102, 17)
        Me.lbl_HistoryCaregory.Name = "lbl_HistoryCaregory"
        Me.lbl_HistoryCaregory.Size = New System.Drawing.Size(105, 14)
        Me.lbl_HistoryCaregory.TabIndex = 208
        Me.lbl_HistoryCaregory.Text = "History Category :"
        '
        'lbl_ConceptID
        '
        Me.lbl_ConceptID.AutoSize = True
        Me.lbl_ConceptID.BackColor = System.Drawing.Color.Transparent
        Me.lbl_ConceptID.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_ConceptID.Location = New System.Drawing.Point(144, 140)
        Me.lbl_ConceptID.Name = "lbl_ConceptID"
        Me.lbl_ConceptID.Size = New System.Drawing.Size(63, 14)
        Me.lbl_ConceptID.TabIndex = 207
        Me.lbl_ConceptID.Text = "SNOMED :"
        '
        'btn_SnomedCode
        '
        Me.btn_SnomedCode.BackColor = System.Drawing.Color.Transparent
        Me.btn_SnomedCode.BackgroundImage = CType(resources.GetObject("btn_SnomedCode.BackgroundImage"), System.Drawing.Image)
        Me.btn_SnomedCode.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btn_SnomedCode.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btn_SnomedCode.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btn_SnomedCode.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_SnomedCode.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_SnomedCode.Image = CType(resources.GetObject("btn_SnomedCode.Image"), System.Drawing.Image)
        Me.btn_SnomedCode.Location = New System.Drawing.Point(462, 136)
        Me.btn_SnomedCode.Name = "btn_SnomedCode"
        Me.btn_SnomedCode.Size = New System.Drawing.Size(21, 21)
        Me.btn_SnomedCode.TabIndex = 205
        Me.btn_SnomedCode.Text = "strSnomedDescription"
        Me.btn_SnomedCode.UseVisualStyleBackColor = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.ForeColor = System.Drawing.Color.Red
        Me.Label4.Location = New System.Drawing.Point(91, 17)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(13, 13)
        Me.Label4.TabIndex = 214
        Me.Label4.Text = "*"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.ForeColor = System.Drawing.Color.Red
        Me.Label3.Location = New System.Drawing.Point(92, 48)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(13, 13)
        Me.Label3.TabIndex = 9
        Me.Label3.Text = "*"
        '
        'lbl_pnlBottom
        '
        Me.lbl_pnlBottom.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_pnlBottom.Location = New System.Drawing.Point(4, 416)
        Me.lbl_pnlBottom.Name = "lbl_pnlBottom"
        Me.lbl_pnlBottom.Size = New System.Drawing.Size(617, 1)
        Me.lbl_pnlBottom.TabIndex = 8
        Me.lbl_pnlBottom.Text = "label2"
        '
        'lbl_pnlLeft
        '
        Me.lbl_pnlLeft.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_pnlLeft.Location = New System.Drawing.Point(3, 4)
        Me.lbl_pnlLeft.Name = "lbl_pnlLeft"
        Me.lbl_pnlLeft.Size = New System.Drawing.Size(1, 413)
        Me.lbl_pnlLeft.TabIndex = 7
        Me.lbl_pnlLeft.Text = "label4"
        '
        'lbl_pnlRight
        '
        Me.lbl_pnlRight.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_pnlRight.Location = New System.Drawing.Point(621, 4)
        Me.lbl_pnlRight.Name = "lbl_pnlRight"
        Me.lbl_pnlRight.Size = New System.Drawing.Size(1, 413)
        Me.lbl_pnlRight.TabIndex = 6
        Me.lbl_pnlRight.Text = "label3"
        '
        'lbl_pnlTop
        '
        Me.lbl_pnlTop.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_pnlTop.Location = New System.Drawing.Point(3, 3)
        Me.lbl_pnlTop.Name = "lbl_pnlTop"
        Me.lbl_pnlTop.Size = New System.Drawing.Size(619, 1)
        Me.lbl_pnlTop.TabIndex = 5
        Me.lbl_pnlTop.Text = "label1"
        '
        'PnlCustomTask
        '
        Me.PnlCustomTask.BackColor = System.Drawing.Color.Transparent
        Me.PnlCustomTask.Location = New System.Drawing.Point(179, 164)
        Me.PnlCustomTask.Name = "PnlCustomTask"
        Me.PnlCustomTask.Size = New System.Drawing.Size(430, 226)
        Me.PnlCustomTask.TabIndex = 221
        Me.PnlCustomTask.Visible = False
        '
        'pnl_tlsp
        '
        Me.pnl_tlsp.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnl_tlsp.Controls.Add(Me.tlsp)
        Me.pnl_tlsp.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnl_tlsp.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnl_tlsp.Location = New System.Drawing.Point(0, 0)
        Me.pnl_tlsp.Name = "pnl_tlsp"
        Me.pnl_tlsp.Size = New System.Drawing.Size(625, 53)
        Me.pnl_tlsp.TabIndex = 14
        '
        'tlsp
        '
        Me.tlsp.BackColor = System.Drawing.Color.Transparent
        Me.tlsp.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tlsp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlsp.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlsp.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tlsp.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ts_btnSave, Me.ts_btnClose})
        Me.tlsp.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.tlsp.Location = New System.Drawing.Point(0, 0)
        Me.tlsp.Name = "tlsp"
        Me.tlsp.Size = New System.Drawing.Size(625, 53)
        Me.tlsp.TabIndex = 0
        Me.tlsp.Text = "toolStrip1"
        '
        'ts_btnSave
        '
        Me.ts_btnSave.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
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
        Me.ts_btnClose.Image = CType(resources.GetObject("ts_btnClose.Image"), System.Drawing.Image)
        Me.ts_btnClose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnClose.Name = "ts_btnClose"
        Me.ts_btnClose.Size = New System.Drawing.Size(43, 50)
        Me.ts_btnClose.Tag = "Close"
        Me.ts_btnClose.Text = "&Close"
        Me.ts_btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'imgTreeVIew
        '
        Me.imgTreeVIew.ImageStream = CType(resources.GetObject("imgTreeVIew.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imgTreeVIew.TransparentColor = System.Drawing.Color.Transparent
        Me.imgTreeVIew.Images.SetKeyName(0, "Small Arrow.ico")
        Me.imgTreeVIew.Images.SetKeyName(1, "Bullet06.ico")
        Me.imgTreeVIew.Images.SetKeyName(2, "Defination.ico")
        '
        'HistoryMaster
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 14)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(625, 473)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.pnl_tlsp)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "HistoryMaster"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "History"
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.panel5.ResumeLayout(False)
        Me.FlpnlSmokingandICD9.ResumeLayout(False)
        Me.pnlSmokingStatus.ResumeLayout(False)
        Me.pnlSmokingStatus.PerformLayout()
        Me.pnlICD9Code.ResumeLayout(False)
        Me.pnlICD9Code.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.PnlHist.ResumeLayout(False)
        Me.PnlHist.PerformLayout()
        Me.pnlLoinc.ResumeLayout(False)
        Me.pnlLoinc.PerformLayout()
        Me.pnlreason.ResumeLayout(False)
        Me.pnlreason.PerformLayout()
        Me.pnl_tlsp.ResumeLayout(False)
        Me.pnl_tlsp.PerformLayout()
        Me.tlsp.ResumeLayout(False)
        Me.tlsp.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub HistoryMaster_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed

        ToolTipbtnCloseICD9.Dispose()
        ToolTipbtnCloseICD9 = Nothing

        ToolTipbtnbrowseICD9code.Dispose()
        ToolTipbtnbrowseICD9code = Nothing

        ToolTipbtnCPTCode.Dispose()
        ToolTipbtnCPTCode = Nothing

        ToolTipbtnCloseCPTCode.Dispose()
        ToolTipbtnCloseCPTCode = Nothing

        ToolTipbtn_SnomedCode.Dispose()
        ToolTipbtn_SnomedCode = Nothing

        ToolTipbtn_Delete.Dispose()
        ToolTipbtn_Delete = Nothing

        ToolTipbtnSmokingStatus.Dispose()
        ToolTipbtnSmokingStatus = Nothing

        ToolTipbtnClearSmokingStatus.Dispose()
        ToolTipbtnClearSmokingStatus = Nothing

        If Not ToolTiptxtICD9Code Is Nothing Then
            ToolTiptxtICD9Code.Dispose()
            ToolTiptxtICD9Code = Nothing
        End If

        If Not ToolTiptxtSmokingStatus Is Nothing Then
            ToolTiptxtSmokingStatus.Dispose()
            ToolTiptxtSmokingStatus = Nothing
        End If


        If Not dtFillHistoryCategory Is Nothing Then
            dtFillHistoryCategory.Dispose()
            dtFillHistoryCategory = Nothing
        End If

        If Not dtBindUserGridSmoking Is Nothing Then
            dtBindUserGridSmoking.Dispose()
            dtBindUserGridSmoking = Nothing
        End If

        If Not dtHistoryType Is Nothing Then
            dtHistoryType.Dispose()
            dtHistoryType = Nothing
        End If

        If Not _dtHist Is Nothing Then
            _dtHist.Dispose()
            _dtHist = Nothing
        End If

        If Not _dtHistCat Is Nothing Then
            _dtHistCat.Dispose()
            _dtHistCat = Nothing
        End If
    End Sub

    Private Sub HistoryMaster_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try

            dtHistoryType = HistoryDBLayer.GetStandardTypes()
            cmbHistoryType.DataSource = dtHistoryType
            cmbHistoryType.ValueMember = dtHistoryType.Columns(1).ColumnName
            cmbHistoryType.DisplayMember = dtHistoryType.Columns(0).ColumnName
            If (dtHistoryType.Rows.Count > 0) Then
                cmbHistoryType.SelectedIndex = 0
            End If


            txtSmokingStatus.Enabled = True
            txtSmokingStatus.ReadOnly = True
            txtICD9Code.Enabled = True
            txtICD9Code.ReadOnly = True
            txtCPTCode.Enabled = True
            txtCPTCode.ReadOnly = True
            pnlICD9Code.Visible = False
            FlpnlSmokingandICD9.Controls.Clear()
            FlpnlSmokingandICD9.Controls.Add(pnlSmokingStatus)
            FlpnlSmokingandICD9.Controls.Add(pnlICD9Code)
            FlpnlSmokingandICD9.Controls.Add(Panel1)
            FlpnlSmokingandICD9.Controls.Add(pnlLoinc)
            FlpnlSmokingandICD9.Controls.Add(pnlreason)
            ' pnlSmokingStatus.Visible = True
            pnlICD9Code.Visible = True
            Panel1.Visible = True
            FillHistoryGategory()
            If HistoryId <> 0 Then
                Try
                    Dim arrlist As ArrayList
                    arrlist = HistoryDBLayer.FetchDataForUpdate(HistoryId, cmb_HistoryCategory.SelectedValue)
                    If arrlist.Count > 0 Then
                        txtDescription.Text = CType(arrlist.Item(0), System.String)
                        txtComments.Text = CType(arrlist.Item(1), System.String)

                        '' txt_ConceptID.Text = CType(arrlist.Item(2), System.String)
                        strConceptID = CType(arrlist.Item(2), System.String)
                        strDescriptionID = CType(arrlist.Item(3), System.String)
                        strSnoMedID = CType(arrlist.Item(4), System.String)
                        strSnomedDescription = CType(arrlist.Item(5), System.String)
                        strSnomedDefination = CType(arrlist.Item(7), System.String)
                        txtSmokingStatus.Text = CType(arrlist.Item(8), System.String)
                        '  strICD9 = CType(arrlist.Item(6), System.String)
                        txtICD9Code.Text = CType(arrlist.Item(6), System.String)
                        txtCPTCode.Text = CType(arrlist.Item(9), System.String)
                        cmbHistoryType.SelectedValue = CType(arrlist.Item(10), System.String)
                        strRxNormCode = CType(arrlist.Item(11), System.String)
                        strNDCCode = CType(arrlist.Item(12), System.String)
                        strLoincCode = CType(arrlist.Item(13), System.String).Trim()
                        strLoincDescr = CType(arrlist.Item(14), System.String).Trim()

                        strRefCode = CType(arrlist.Item(15), System.String).Trim()
                        strRefDescr = CType(arrlist.Item(16), System.String).Trim()
                        'If Not strSnomedDescription = "" Then  ''8020 Prd snomed changes  tree removed 
                        '    txtsnodesc.Text = strSnomedDescription
                        'Else
                        '    txtsnodesc.Text = ""
                        'End If
                        ''chetan added for snomed changes 8020 
                        Dim strSnomed As String = strConceptID.Trim() + "-" + strSnomedDescription.Trim()
                        If strSnomed.Trim().Length > 1 Then
                            txt_ConceptID.Text = strSnomed
                        Else
                            txt_ConceptID.Text = ""
                        End If
                        If (strLoincCode.Trim().Length > 0) Then
                            txtloinc.Text = strLoincCode & " : " & strLoincDescr
                        Else
                            txtloinc.Text = ""
                        End If
                        If ((strRefCode.Trim().Length + strRefDescr.Trim().Length) > 2) Then
                            txtSNOMEDRefusedCode.Text = strRefCode + " ; " + strRefDescr
                        End If
                        'strSnoMedID = CType(arrlist.Item(2), System.String)
                        ' txtComments.Text = CType(arrlist.Item(5), System.String)

                    End If
                Catch ex As SqlClient.SqlException
                    MessageBox.Show(ex.Message, "History", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            Else
                txtDescription.Text = _Description
                txtComments.Text = _Comments
                txt_ConceptID.Text = ""
                strConceptID = ""
                strDescriptionID = ""
                strSnoMedID = ""
                strSnomedDescription = ""
                txtICD9Code.Text = ""
                txtCPTCode.Text = ""
                cmbHistoryType.SelectedValue = ""
                txtloinc.Text = ""
            End If

            If cmb_HistoryCategory.Text = "Smoking Status" Then
                cmbHistoryType.Enabled = False
                lblHistoryType.Enabled = False
                cmbHistoryType.Text = ""
            Else
                cmbHistoryType.Enabled = True
                lblHistoryType.Enabled = True
            End If

            If cmb_HistoryCategory.Text = "OB Initial Physical Examination" Then
                PnlHist.Visible = True
                FlpnlSmokingandICD9.Controls.Add(PnlHist)
            Else
                PnlHist.Visible = False
                FlpnlSmokingandICD9.Controls.Remove(PnlHist)
            End If
            ToolTipbtnCloseICD9.SetToolTip(Me.btnCloseICD9, "Clear ICD9/ICD10")
            ToolTipbtnbrowseICD9code.SetToolTip(Me.btnbrowseICD9code, "Browse ICD9/ICD10")
            ToolTipbtnCPTCode.SetToolTip(Me.btnCPTCode, "Browse CPT")

            ToolTipbtnCloseCPTCode.SetToolTip(Me.btnCloseCPTCode, "Clear CPT")
            ToolTipbtn_SnomedCode.SetToolTip(Me.btn_SnomedCode, "Browse Concept ID")
            ToolTipbtn_Delete.SetToolTip(Me.btn_Delete, "Clear Concept ID")
            ToolTipbtnSmokingStatus.SetToolTip(Me.btnSmokingStatus, "Browse Smoking Status")
            ToolTipbtnClearSmokingStatus.SetToolTip(Me.btnClearSmokingStatus, "Clear Smoking Status")
            BindDatatoCombo()
        Catch ex As Exception

        End Try

    End Sub

    Private Function GetHistoryDetails(ByVal HistoryId As Long) As DataTable
        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString)
        Dim oParameters As gloDatabaseLayer.DBParameters = Nothing
        Dim _dt As DataTable = Nothing


        Try

            oParameters = New gloDatabaseLayer.DBParameters()
            oParameters.Add("@nhistoryid", HistoryId, ParameterDirection.Input, SqlDbType.BigInt)

            oDB.Connect(False)
            oDB.Retrive("gsp_OBHistoryCategoryDetails", oParameters, _dt)
            Return _dt

        Catch dbEx As gloDatabaseLayer.DBException
            dbEx.ERROR_Log(dbEx.ToString())
            Return Nothing
        Finally
            If oParameters IsNot Nothing Then

                oParameters.Clear()
                oParameters.Dispose()
                oParameters = Nothing
            End If
            If oDB IsNot Nothing Then
                oDB.Disconnect()
                oDB.Dispose()
                oDB = Nothing
            End If

            

        End Try



    End Function


    Private Sub BindDatatoCombo()
        _dtHist = GetHistoryDetails(HistoryId)

        cmbhist.DataSource = Nothing
        cmbhist.Items.Clear()
        If _dtHist IsNot Nothing AndAlso _dtHist.Rows.Count > 0 Then
            cmbhist.DisplayMember = "sDescription"
            cmbhist.ValueMember = "nHistoryDetailsID"
            cmbhist.DataSource = _dtHist
        End If
    End Sub
    Private Sub FillHistoryGategory()

        Try


            dtFillHistoryCategory = HistoryDBLayer.FillControls()
            cmb_HistoryCategory.ValueMember = dtFillHistoryCategory.Columns(0).ToString
            cmb_HistoryCategory.DisplayMember = dtFillHistoryCategory.Columns(1).ToString()
            cmb_HistoryCategory.DataSource = dtFillHistoryCategory

            If Not _SelectedCategoty = "" Then
                cmb_HistoryCategory.Text = _SelectedCategoty
            End If

        Catch ex As Exception

        End Try

    End Sub

    Private Sub CloseHistory()
        'errprovider.SetError(txtDescription, "")
        _Description = ""
        HistoryDBLayer = Nothing
        Me.Close()
    End Sub

    Private Sub SaveHistory()

        If cmb_HistoryCategory.Text = "OB Initial Physical Examination" Then

            If Not IsNothing(_dtHistCat) Then
                _dtHistCat.Clear()
            Else
                _dtHistCat = New DataTable()
                _dtHistCat.Columns.Add("nHistoryID", GetType(Long))
                _dtHistCat.Columns.Add("nCategoryID", GetType(Long))
            End If



            If Not _dtHist Is Nothing Then

                For i As Int32 = 0 To _dtHist.Rows.Count - 1
                    Dim dr As DataRow = _dtHistCat.NewRow()
                    dr(0) = HistoryId
                    dr(1) = _dtHist(i)(0)

                    _dtHistCat.Rows.Add(dr)
                Next
            End If

        End If


        Dim formclose As Boolean = True
        If cmb_HistoryCategory.Text = "Medical Condition" Or cmb_HistoryCategory.Text = "Coded History" Then
            MessageBox.Show("You cannot Add Items to System defined categories", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else

            If Trim(txtDescription.Text) = "" Then
                'errprovider.SetError(txtDescription, "Description is Required")
                MessageBox.Show("Description is Required", "History", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtDescription.Focus()
                Exit Sub
            Else
                'errprovider.SetError(txtDescription, "")
                If Not HistoryDBLayer.ValidateDescription(cmb_HistoryCategory.SelectedValue, Trim(txtDescription.Text), HistoryId, strConceptID.Trim()) Then
                    'errprovider.SetError(txtDescription, "Duplicate Description")
                    MessageBox.Show("Duplicate Description", "History", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    txtDescription.Focus()
                    Exit Sub
                Else
                    'errprovider.SetError(txtDescription, "")
                End If
            End If
            If Not cmb_HistoryCategory.Text = "Smoking Status" Then
                txtSmokingStatus.Text = ""
            End If
            Dim loincCode As String() = txtloinc.Text.Trim().Split(":")
            If (loincCode.Length > 1) Then
                strLoincCode = loincCode(0).Trim()
                strLoincDescr = loincCode(1).Trim()
            Else
                strLoincCode = ""
                strLoincDescr = ""
            End If
            Dim refusalcode As String() = txtSNOMEDRefusedCode.Text.Trim().Split(";")
            If (refusalcode.Length > 1) Then
               
                strRefCode = refusalcode(0).Trim()
                strRefDescr = refusalcode(1).Trim()
            Else
                strRefCode = ""
                strRefDescr = ""
            End If
            If HistoryId = 0 Then
                Try
                    ''IcdType ''parameter added for ICD10 implementation
                    If HistoryDBLayer.AddData(Trim(txtDescription.Text), Trim(txtComments.Text), cmb_HistoryCategory.SelectedValue, strConceptID.Trim(), strDescriptionID, strSnoMedID, strSnomedDescription, txtICD9Code.Text, strNDCCode, strRxNormCode, strSnomedDefination, Convert.ToString(txtSmokingStatus.Text).Trim(), txtCPTCode.Text.Trim, cmbHistoryType.SelectedValue.Trim(), nICDType, _dtHistCat, LoincCode:=strLoincCode, LoincDescr:=strLoincDescr, refusalcode:=strRefCode, refusalcodeDescr:=strRefDescr) <> 0 Then
                        _Description = Trim(txtDescription.Text)
                        _Comments = Trim(txtComments.Text)
                        _CategoryID = cmb_HistoryCategory.SelectedValue
                    Else
                        _Description = ""
                    End If

                Catch ex As SqlClient.SqlException
                    MessageBox.Show(ex.Message, "History", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Finally
                    HistoryDBLayer = Nothing
                    _DialogResult = Windows.Forms.DialogResult.OK
                    Me.Close()
                End Try
            Else
                Try

                    Dim Result As Boolean = CheckIsHistoryItemExistsinPortal(HistoryId)
                    If Result Then
                        formclose = False
                        Exit Sub
                    End If



                    ''IcdType ''parameter added for ICD10 implementation
                    HistoryDBLayer.UpdateData(txtDescription.Text, txtComments.Text, HistoryId, strConceptID.Trim(), strDescriptionID, strSnoMedID, strSnomedDescription, cmb_HistoryCategory.SelectedValue, txtICD9Code.Text, strNDCCode, strRxNormCode, strSnomedDefination, Convert.ToString(txtSmokingStatus.Text).Trim(), txtCPTCode.Text.Trim, cmbHistoryType.SelectedValue.Trim, nICDType, _dtHistCat, LoincCode:=strLoincCode, LoincDescr:=strLoincDescr, refusalcode:=strRefCode, refusalcodeDescr:=strRefDescr)
                    _CategoryID = cmb_HistoryCategory.SelectedValue

                Catch ex As SqlClient.SqlException
                    MessageBox.Show(ex.Message, "History", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Finally
                    If formclose = True Then
                        HistoryDBLayer = Nothing
                        _DialogResult = Windows.Forms.DialogResult.OK
                        Me.Close()
                    End If

                End Try
            End If
        End If

    End Sub

    Private Function CheckIsHistoryItemExistsinPortal(ByVal HistoryID As Long) As Boolean
        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString)
        Dim oParameters As gloDatabaseLayer.DBParameters = Nothing
        Dim _dt As DataTable = Nothing
        Dim _dt2 As DataTable = Nothing

        Try

            oParameters = New gloDatabaseLayer.DBParameters()
            oParameters.Add("@nhistoryid", HistoryID, ParameterDirection.Input, SqlDbType.BigInt)
            oParameters.Add("@IsDelete", False, ParameterDirection.Input, SqlDbType.Bit)

            oDB.Connect(False)
            oDB.Retrive("WS_IsHistoryItemExistsinHealthform", oParameters, _dt)
            If _dt IsNot Nothing AndAlso _dt.Rows.Count > 0 Then
                If MessageBox.Show("Selected history category item is used in patient portal forms. After this modification all existing patient portal forms data and any new incoming data from portal will be associated to this modified history category item." + System.Environment.NewLine + System.Environment.NewLine + "Do you want to continue with the modification?", gstrMessageBoxCaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Information) = Windows.Forms.DialogResult.OK Then
                    Try

                        ' Set IsRepublish Required to 1 & Delete Entry 
                        oParameters = New gloDatabaseLayer.DBParameters()
                        oParameters.Add("@nhistoryid", HistoryID, ParameterDirection.Input, SqlDbType.BigInt)
                        oParameters.Add("@IsDelete", False, ParameterDirection.Input, SqlDbType.Bit)
                        oParameters.Add("@IsUpdatePatientForm", True, ParameterDirection.Input, SqlDbType.Bit)
                        oDB.Connect(False)
                        oDB.Retrive("WS_IsHistoryItemExistsinHealthform", oParameters, _dt2)

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

    Private Sub tlsp_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tlsp.ItemClicked
        Try
            Select Case e.ClickedItem.Tag.ToString().ToUpper()
                Case UCase("Save")
                    SaveHistory()

                Case UCase("Close")
                    CloseHistory()

            End Select

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK)
        End Try
    End Sub

    Private Sub btn_SnomedCode_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_SnomedCode.Click
        gstrSMDBConnstr = GetHybridConnectionString(gstrSMDBServerName, gstrSMDBDatabaseName, gblnSMDBAuthen, gstrSMDBUserID, gstrSMDBPassWord)
        Dim frm As New gloSnoMed.FrmSelectProblem("History", gstrSMDBConnstr, GetConnectionString())
        '  Dim Hs_StrSnoMedID As String
        Dim str As String = ""
        Try
            '  frm.StartPosition = FormStartPosition.CenterScreen
            '  frm.ShowInTaskbar = False

            ''Code changed by MAYURI:20130125 To show Conceptid in search window in modify mode else shoe conceptDescription
            If txt_ConceptID.Text.Trim <> "" Then ''8020 Prd snomed changes,  treeview removed 
                frm.txtSMSearch.Text = strConceptID ''txt_ConceptID.Text.Trim
            Else
                frm.txtSMSearch.Text = txtDescription.Text
            End If
            frm.strConceptDesc = txtDescription.Text
            frm.strDescriptionID = strDescriptionID
            frm.strConceptID = strConceptID  ''txt_ConceptID.Text.Trim
            frm.ShowDialog(IIf(IsNothing(frm.Parent), Me, frm.Parent))
            txtDescription.Focus()
            'txtName.Text = frm.strProblem
            'txtName.Tag = frm.strICD9
            'IM_strConceptID = frm.strConceptID
            'IM_strDescriptionID = frm.strDescriptionID
            If frm._DialogResult Then
                strConceptID = frm.strConceptID
                strDescriptionID = frm.strDescriptionID
                strSnoMedID = frm.StrSnoMedID
                strSnomedDescription = frm.strSelectedDescription
                strSnomedDefination = frm.strSelectedDefination
                ' txtICD9Code.Text = frm.strICD9
                strRxNormCode = frm.strRxNormCode
                strNDCCode = frm.strNDCCode
                If txtDescription.Text = "" Then
                    txtDescription.Text = frm.strProblem
                End If
                If frm.strICD10 <> "" Then 'gblnIcd10Transition
                    If txtICD9Code.Text = "" Or frm.strICD10 <> "" Then

                        txtICD9Code.Text = frm.strICD10
                        nICDType = gloGlobal.gloICD.CodeRevision.ICD10

                    End If
                Else
                    If txtICD9Code.Text = "" Or frm.strICD9 <> "" Then

                        txtICD9Code.Text = frm.strICD9
                        nICDType = gloGlobal.gloICD.CodeRevision.ICD9
                    End If
                End If


                'lblConceptID.Text = IM_strConceptID
                'lblDescriptionID.Text = IM_strDescriptionID
                ' txt_ConceptID.Text = strConceptID
                'str = frm.strProblem & "|" & strSnomedDescription
                'strSnomedDescription = frm.strDefination


                'If Not strSnomedDescription = "" Then  ''8020 Prd snomed changes,  treeview removed 
                '    txtsnodesc.Text = strSnomedDescription

                'Else
                '    txtsnodesc.Text = ""
                'End If
                ''chetan added for snomed changes 8020 
                Dim strSnomed As String = strConceptID.Trim() + "-" + strSnomedDescription.Trim()
                If strSnomed.Trim().Length > 1 Then
                    txt_ConceptID.Text = strSnomed
                Else
                    txt_ConceptID.Text = ""
                End If


            End If
            frm.Dispose()
            frm = Nothing
        Catch ex As Exception
            frm.Dispose()
        End Try
    End Sub


    Private Sub cmb_HistoryCategory_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmb_HistoryCategory.SelectedIndexChanged
        Try
            If cmb_HistoryCategory.Text = "Medical Condition" Or cmb_HistoryCategory.Text = "Coded History" Then
                MessageBox.Show("You cannot Add Items to System defined categories", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

            If cmb_HistoryCategory.Text = "Smoking Status" Then
                pnlSmokingStatus.Visible = True
                cmbHistoryType.Enabled = False
                lblHistoryType.Enabled = False
                cmbHistoryType.Text = ""
            Else
                cmbHistoryType.Enabled = True
                lblHistoryType.Enabled = True
                RemoveControl()
                pnlSmokingStatus.Visible = False
            End If
            If cmb_HistoryCategory.Text = "OB Initial Physical Examination" Then
                PnlHist.Visible = True
            Else
                PnlHist.Visible = False
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK)
        End Try
    End Sub

    Private Sub btn_Delete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Delete.Click
        txt_ConceptID.Text = ""
        strConceptID = ""  ''added for bugid 71131
        '' txtsnodesc.Text = ""
        strSnomedDefination = ""
        strDescriptionID = ""
        strSnoMedID = ""
        txtICD9Code.Text = ""
        strSnomedDescription = ""
        ' strICD9 = ""
        strNDCCode = ""
        strRxNormCode = ""
        strSnomedDefination = ""
    End Sub

    ''Sanjog Added for Smoking status

    Public Sub CustomDrugsGridStyle()

        dgCustomGrid.tsbtn_New.Visible = False

        Dim _TotalWidth As Single = dgCustomGrid.C1Task.Width - 5

        ' '' Show Drugs Info
        With dgCustomGrid.C1Task
            .Cols.Fixed = 0
            .Rows.Fixed = 1
            .Cols.Count = Col_DGCustCnt
            .AllowEditing = True
            .ExtendLastCol = True
            .SetData(0, Col_Check, "Select")
            '.Cols(Col_Check).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter
            .Cols(Col_Check).Width = _TotalWidth * 0.1
            .Cols(Col_Check).AllowEditing = True
            .Cols(Col_Check).DataType = System.Type.GetType("System.Boolean")

            .SetData(0, Col_Name, "sDescription")
            .Cols(Col_Name).Width = _TotalWidth * 0.85

        End With
    End Sub

    Private Sub BindUserGridSmoking()
        Try
            ' Dim dt As DataTable
            Dim objclsPatientHistory As New clsPatientHistory


            dtBindUserGridSmoking = objclsPatientHistory.GetAllCategory("Smoking Status Type")
            objclsPatientHistory.Dispose()
            objclsPatientHistory = Nothing
            CustomDrugsGridStyle()
            Dim col As New DataColumn
            col.ColumnName = "Select"
            col.DataType = System.Type.GetType("System.Boolean")

            col.DefaultValue = CBool("False")
            dtBindUserGridSmoking.Columns.Add(col)

            If Not IsNothing(dtBindUserGridSmoking) Then
                dtBindUserGridSmoking.Columns("sDescription").Caption = "Description"
                dgCustomGrid.datasource(dtBindUserGridSmoking.DefaultView)
            End If
            ''Reset the grid
            Dim _TotalWidth As Single = dgCustomGrid.C1Task.Width - 5
            dgCustomGrid.C1Task.Cols.Move(dgCustomGrid.C1Task.Cols.Count - 1, 0)
            dgCustomGrid.C1Task.AllowEditing = True
            dgCustomGrid.C1Task.Cols(0).AllowEditing = True
            dgCustomGrid.C1Task.Cols(0).Width = _TotalWidth * 0.1
            dgCustomGrid.C1Task.Cols(1).Visible = False
            dgCustomGrid.C1Task.Cols(2).AllowEditing = False
            dgCustomGrid.C1Task.Cols(2).Width = _TotalWidth * 0.85
            dgCustomGrid.Visible = True

            If txtSmokingStatus.Text.ToString().Trim() <> "" Then
                CheckDGCustomGridSmoking()
            End If
        Catch ex As SqlClient.SqlException
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub CheckDGCustomGridSmoking()

        Dim StrData As System.Array = txtSmokingStatus.Text.ToString().Split(vbNewLine)

        For Len As Integer = 0 To StrData.Length - 1
            StrData(Len) = StrData(Len).Trim()
        Next
        For i As Integer = 0 To dgCustomGrid.C1Task.Rows.Count - 1
            If Array.IndexOf(StrData, (dgCustomGrid.GetItem(i, 2).ToString.Trim())) >= 0 Then
                dgCustomGrid.C1Task.SetCellCheck(i, 0, C1.Win.C1FlexGrid.CheckEnum.Checked)
            End If
        Next
    End Sub

    Private Sub AddControlSmoking()

        If Not IsNothing(dgCustomGrid) Then
            RemoveControl()
        End If
        dgCustomGrid = New CustomTask
        dgCustomGrid.Dock = DockStyle.Fill
        PnlCustomTask.Controls.Add(dgCustomGrid)
        PnlCustomTask.BringToFront()

        Dim y As Int64
        Dim x As Int64
        x = 300
        y = 250
        ''''''''''''''''''''''
        'PnlCustomTask.Location = New Point(PnlCustomTask.Location.X, PnlCustomTask.Location.Y)
        PnlCustomTask.Visible = True
        dgCustomGrid.Visible = True
        PnlCustomTask.BringToFront()
        dgCustomGrid.BringToFront()

        Me.Size = New System.Drawing.Size(630, 492)

    End Sub
    Private Sub RemoveControl()
        If Not IsNothing(dgCustomGrid) Then
            PnlCustomTask.Controls.Remove(dgCustomGrid)
            dgCustomGrid.Dispose()
            dgCustomGrid = Nothing
            PnlCustomTask.Visible = False
            Me.Size = New System.Drawing.Size(550, 399)
        End If
    End Sub
    Private Sub btnSmokingStatus_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSmokingStatus.Click
        LoadUserGridSmoking()
    End Sub

    Private Sub LoadUserGridSmoking()
        Try
            AddControlSmoking()
            If Not IsNothing(dgCustomGrid) Then
                dgCustomGrid.Visible = True
                PnlCustomTask.Width = 400
                PnlCustomTask.Height = 220
                dgCustomGrid.txtsearch.Width = 120

                dgCustomGrid.BringToFront()
                BindUserGridSmoking()
                dgCustomGrid.Selectsearch(CustomDataGrid.enmcontrol.Search)
                PnlCustomTask.BringToFront()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub dgCustomGrid_AfterSelChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgCustomGrid.AfterSelChanged
        Dim ind As Integer = dgCustomGrid.GetCurrentrowIndex
        Try
            For i As Integer = 0 To dgCustomGrid.C1Task.Rows.Count - 1
                If dgCustomGrid.C1Task.GetCellCheck(i, 0) = C1.Win.C1FlexGrid.CheckEnum.Checked And i <> ind Then
                    dgCustomGrid.C1Task.SetCellCheck(i, 0, C1.Win.C1FlexGrid.CheckEnum.Unchecked)
                End If
            Next
        Catch ex As Exception

        End Try

    End Sub

    Private Sub dgCustomGrid_CloseClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgCustomGrid.CloseClick
        PnlCustomTask.Visible = False
        Me.Size = New System.Drawing.Size(550, 399)
    End Sub

    Private Sub dgCustomGrid_OKClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgCustomGrid.OKClick

        Dim Strdata As String = ""
        Dim cnt As Integer = 0
        For i As Integer = 0 To dgCustomGrid.C1Task.Rows.Count - 1
            If dgCustomGrid.C1Task.GetCellCheck(i, 0) = C1.Win.C1FlexGrid.CheckEnum.Checked Then
                If Strdata.Trim = "" Then
                    Strdata = dgCustomGrid.GetItem(i, 2).ToString
                Else
                    Strdata &= vbNewLine & dgCustomGrid.GetItem(i, 2).ToString
                End If
                cnt = cnt + 1
            End If
        Next

        txtSmokingStatus.Text = Strdata

        PnlCustomTask.Visible = False
        Me.Size = New System.Drawing.Size(550, 399)
    End Sub

    Private Sub dgCustomGrid_SearchChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgCustomGrid.SearchChanged
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim dvPatient As New DataView()
            dvPatient = CType(dgCustomGrid.C1Task.DataSource(), DataView) '' (CType(dt.DefaultView, DataView))

            If IsNothing(dvPatient) Then
                Me.Cursor = Cursors.Default
                Exit Sub
            End If

            Dim strPatientSearchDetails As String
            If Trim(dgCustomGrid.txtsearch.Text) <> "" Then
                strPatientSearchDetails = Replace(dgCustomGrid.txtsearch.Text, "'", "''")
                strPatientSearchDetails = Replace(strPatientSearchDetails, "[", "") & ""
                strPatientSearchDetails = mdlGeneral.ReplaceSpecialCharacters(strPatientSearchDetails)
            Else
                strPatientSearchDetails = ""
            End If

            dvPatient.RowFilter = "[" & dvPatient.Table.Columns(1).ColumnName & "] Like '%" & strPatientSearchDetails & "%' "

            dgCustomGrid.Enabled = False
            dgCustomGrid.datasource(dvPatient)
            dgCustomGrid.Enabled = True
            Me.Cursor = Cursors.Default
            Dim _TotalWidth As Single = dgCustomGrid.C1Task.Width - 5
            dgCustomGrid.C1Task.Cols(Col_Check).Width = _TotalWidth * 0.1
            dgCustomGrid.C1Task.Cols(Col_Name).Width = _TotalWidth * 0.85
            dgCustomGrid.txtsearch.Focus()
        Catch objErr As Exception
            Me.Cursor = Cursors.Default
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.Select, objErr.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub
#Region "7010 Phase II Features"


    Private Sub btnbrowseICD9code_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnbrowseICD9code.Click
        Try

            ofrmDiagnosisList = New frmViewListControl
            '   Dim arrCPTTextSplit As String()
            oDiagnosisListControl = New gloListControl.gloListControl(GetConnectionString(), gloListControl.gloListControlType.Diagnosis, False, Me.Width)
            oDiagnosisListControl.ControlHeader = "Diagnosis"
            oDiagnosisListControl.gblnIcd10Transition = gblnIcd10MasterTransition 'gblnIcd10Transition   ''If true then ICD10 gets selected 
            AddHandler oDiagnosisListControl.ItemSelectedClick, AddressOf oDiagnosisListControl_ItemSelectedClick
            AddHandler oDiagnosisListControl.ItemClosedClick, AddressOf oDiagnosisListControl_ItemClosedClick
            ofrmDiagnosisList.Controls.Add(oDiagnosisListControl)
            oDiagnosisListControl.Dock = DockStyle.Fill
            oDiagnosisListControl.BringToFront()
            'code make all CPT Code Selected in ListControl  Which are present in cmbCPT
            'For i As Integer = 0 To cmbDiagnosis.Items.Count - 1
            '    cmbDiagnosis.SelectedIndex = i
            '    oDiagnosisListControl.SelectedItems.Add(0, txtICD9Code.Text, "")
            'Next
            oDiagnosisListControl.ShowHeaderPanel(False)
            oDiagnosisListControl.OpenControl()
            ofrmDiagnosisList.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedSingle
            ofrmDiagnosisList.Text = "Diagnosis"
            ofrmDiagnosisList.ShowDialog(IIf(IsNothing(CType(ofrmDiagnosisList, Control).Parent), Me, CType(ofrmDiagnosisList, Control).Parent))

            If IsNothing(ofrmDiagnosisList) = False Then
                RemoveHandler oDiagnosisListControl.ItemSelectedClick, AddressOf oDiagnosisListControl_ItemSelectedClick
                RemoveHandler oDiagnosisListControl.ItemClosedClick, AddressOf oDiagnosisListControl_ItemClosedClick
                ofrmDiagnosisList.Controls.Remove(oDiagnosisListControl)
                oDiagnosisListControl.Dispose()
                oDiagnosisListControl = Nothing
                ofrmDiagnosisList.Dispose()
                ofrmDiagnosisList = Nothing
                'End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub oDiagnosisListControl_ItemSelectedClick(ByVal sender As Object, ByVal e As EventArgs)
        Try
            If oDiagnosisListControl.SelectedItems.Count > 0 Then
                For i As Int16 = 0 To oDiagnosisListControl.SelectedItems.Count - 1
                    txtICD9Code.Text = oDiagnosisListControl.SelectedItems(i).Code + " : " + oDiagnosisListControl.SelectedItems(i).Description
                    nICDType = oDiagnosisListControl.IsICD9_10 ''  nICDType added for ICD10 implementation
                    ' strICD9 = txtICD9Code.Text
                    '   txtICD9Code.Tag = oDiagnosisListControl.SelectedItems(i).ID
                Next

                ofrmDiagnosisList.Close()
            Else
                txtICD9Code.Text = ""
                ' txtICD9Code.Tag = ""
                ofrmDiagnosisList.Close()
            End If
            'Dim dtICD9Code As DataTable
            'Dim ToList As gloGeneralItem.gloItems
            'dtICD9Code = New DataTable
            'Dim dcID As New DataColumn("ID")
            'Dim dcDescription As New DataColumn("Code")
            'dtICD9Code.Columns.Add(dcID)
            'dtICD9Code.Columns.Add(dcDescription)
            'ToList = New gloGeneralItem.gloItems()
            'Dim ToItem As gloGeneralItem.gloItem
            'If oDiagnosisListControl.SelectedItems.Count > 0 Then
            '    If oDiagnosisListControl.SelectedItems.Count > 5 Then
            '        MessageBox.Show("Select only 5 Diagnosis", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '        oDiagnosisListControl.CloseOnDoubleClick = False
            '    Else
            '        For i As Int16 = 0 To oDiagnosisListControl.SelectedItems.Count - 1
            '            Dim drTemp As DataRow = dtICD9Code.NewRow()
            '            drTemp("ID") = oDiagnosisListControl.SelectedItems(i).ID
            '            drTemp("Code") = oDiagnosisListControl.SelectedItems(i).Code
            '            dtICD9Code.Rows.Add(drTemp)
            '            ToItem = New gloGeneralItem.gloItem()
            '            ToItem.ID = oDiagnosisListControl.SelectedItems(i).ID
            '            ToItem.Description = oDiagnosisListControl.SelectedItems(i).Code
            '            ToList.Add(ToItem)
            '            ToItem = Nothing
            '        Next
            '        txtICD9Code.Text = dtICD9Code.Columns("Code").ColumnName
            '        txtICD9Code.Tag = dtICD9Code.Columns("ID").ColumnName

            '        ofrmDiagnosisList.Close()
            '    End If
            'Else

            '    txtICD9Code.Text = ""
            '    ofrmDiagnosisList.Close()

            'End If

        Catch ex As Exception
            MessageBox.Show("Error on User List Control." & ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub
    Private Sub oDiagnosisListControl_ItemClosedClick(ByVal sender As Object, ByVal e As EventArgs)
        ofrmDiagnosisList.Close()
        If IsNothing(ofrmDiagnosisList) = False Then
            ofrmDiagnosisList = Nothing
        End If
    End Sub

    Private Sub oCPTListControl_ItemSelectedClick(ByVal sender As Object, ByVal e As EventArgs)
        Try
            If oCPTListControl.SelectedItems.Count > 0 Then
                For i As Int16 = 0 To oCPTListControl.SelectedItems.Count - 1
                    txtCPTCode.Text = oCPTListControl.SelectedItems(i).Code + " : " + oCPTListControl.SelectedItems(i).Description
                    ' strICD9 = txtICD9Code.Text
                    '   txtICD9Code.Tag = oDiagnosisListControl.SelectedItems(i).ID
                Next
                ofrmCPTList.Close()
            Else
                txtCPTCode.Text = ""
                ' txtICD9Code.Tag = ""
                ofrmCPTList.Close()
            End If


        Catch ex As Exception
            MessageBox.Show("Error on User List Control." & ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub
    Private Sub oCPTListControl_ItemClosedClick(ByVal sender As Object, ByVal e As EventArgs)
        ofrmCPTList.Close()
        If IsNothing(ofrmCPTList) = False Then
            ofrmCPTList = Nothing
        End If
    End Sub




    Private Sub oHistListControl_ItemSelectedClick(ByVal sender As Object, ByVal e As EventArgs)
        Try
            If Not IsNothing(_dtHist) Then
                _dtHist.Rows.Clear()
                If oHistListControl.SelectedItems.Count > 0 Then



                    For i As Int16 = 0 To oHistListControl.SelectedItems.Count - 1
                        Dim dr As DataRow = _dtHist.NewRow()
                        dr(0) = oHistListControl.SelectedItems(i).ID
                        dr(1) = oHistListControl.SelectedItems(i).Description
                        _dtHist.Rows.Add(dr)
                    Next

                End If
                cmbhist.DisplayMember = "sDescription"
                cmbhist.ValueMember = "nHistoryDetailsID"
                cmbhist.DataSource = _dtHist

            End If

        Catch ex As Exception
            MessageBox.Show("Error on User List Control." & ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            ofrmCPTList.Close()
        End Try

    End Sub
    Private Sub oHistListControl_ItemClosedClick(ByVal sender As Object, ByVal e As EventArgs)
        ofrmCPTList.Close()
        If IsNothing(ofrmCPTList) = False Then
            ofrmCPTList = Nothing
        End If
    End Sub

#End Region



    Private Sub txtICD9Code_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtICD9Code.MouseHover

        If Not ToolTiptxtICD9Code Is Nothing Then
            ToolTiptxtICD9Code.Dispose()
            ToolTiptxtICD9Code = Nothing
        End If

        ToolTiptxtICD9Code = New System.Windows.Forms.ToolTip

        ToolTiptxtICD9Code.SetToolTip(Me.txtICD9Code, txtICD9Code.Text)

    End Sub

    Private Sub btnCloseICD9_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCloseICD9.Click
        txtICD9Code.Text = ""

    End Sub



    Private Sub txtSmokingStatus_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSmokingStatus.MouseHover

        If Not ToolTiptxtSmokingStatus Is Nothing Then
            ToolTiptxtSmokingStatus.Dispose()
            ToolTiptxtSmokingStatus = Nothing
        End If

        ToolTiptxtSmokingStatus = New System.Windows.Forms.ToolTip

        ToolTiptxtSmokingStatus.SetToolTip(Me.txtSmokingStatus, txtSmokingStatus.Text)



    End Sub

    Private Sub btnCPTCode_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCPTCode.Click
        Try

            ofrmCPTList = New frmViewListControl
            ' Dim arrCPTTextSplit As String()
            oCPTListControl = New gloListControl.gloListControl(GetConnectionString(), gloListControl.gloListControlType.CPT, False, Me.Width)
            oCPTListControl.ControlHeader = "CPT"
            AddHandler oCPTListControl.ItemSelectedClick, AddressOf oCPTListControl_ItemSelectedClick
            AddHandler oCPTListControl.ItemClosedClick, AddressOf oCPTListControl_ItemClosedClick
            ofrmCPTList.Controls.Add(oCPTListControl)
            oCPTListControl.Dock = DockStyle.Fill
            oCPTListControl.BringToFront()

            oCPTListControl.ShowHeaderPanel(False)
            oCPTListControl.OpenControl()
            ofrmCPTList.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedSingle
            ofrmCPTList.Text = "CPT"
            ofrmCPTList.ShowDialog(IIf(IsNothing(CType(ofrmCPTList, Control).Parent), Me, CType(ofrmCPTList, Control).Parent))

            If IsNothing(ofrmCPTList) = False Then
                RemoveHandler oCPTListControl.ItemSelectedClick, AddressOf oCPTListControl_ItemSelectedClick
                RemoveHandler oCPTListControl.ItemClosedClick, AddressOf oCPTListControl_ItemClosedClick
                ofrmCPTList.Controls.Remove(oCPTListControl)
                oCPTListControl.Dispose()
                oCPTListControl = Nothing
                ofrmCPTList.Dispose()
                ofrmCPTList = Nothing
                'End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnCloseCPTCode_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCloseCPTCode.Click
        txtCPTCode.Text = ""
    End Sub


    Private Sub btnClearSmokingStatus_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearSmokingStatus.Click
        txtSmokingStatus.Text = ""
    End Sub

    Private Sub txtSmokingStatus_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSmokingStatus.TextChanged

    End Sub

    Private Sub btnhist_Click(sender As System.Object, e As System.EventArgs) Handles btnhist.Click
        Try

            ofrmCPTList = New frmViewListControl
            ' Dim arrCPTTextSplit As String()
            oHistListControl = New gloListControl.gloListControl(GetConnectionString(), gloListControl.gloListControlType.HistoryOBPhysicalExam, True, Me.Width)
            oHistListControl.ControlHeader = "History"
            AddHandler oHistListControl.ItemSelectedClick, AddressOf oHistListControl_ItemSelectedClick
            AddHandler oHistListControl.ItemClosedClick, AddressOf oHistListControl_ItemClosedClick
            ofrmCPTList.Controls.Add(oHistListControl)
            oHistListControl.Dock = DockStyle.Fill
            oHistListControl.BringToFront()

            If Not IsNothing(_dtHist) Then
                For Each dr As DataRow In _dtHist.Rows

                    oHistListControl.SelectedItems.Add(Convert.ToInt64(dr("nHistoryDetailsID")), Convert.ToString(dr("sDescription")))
                Next
            End If
            oHistListControl.ShowHeaderPanel(False)
            oHistListControl.OpenControl()
            ofrmCPTList.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedSingle
            ofrmCPTList.Text = "History Answers"
            ofrmCPTList.ShowDialog(IIf(IsNothing(CType(ofrmCPTList, Control).Parent), Me, CType(ofrmCPTList, Control).Parent))

            If IsNothing(ofrmCPTList) = False Then
                RemoveHandler oHistListControl.ItemSelectedClick, AddressOf oHistListControl_ItemSelectedClick
                RemoveHandler oHistListControl.ItemClosedClick, AddressOf oHistListControl_ItemClosedClick
                ofrmCPTList.Controls.Remove(oHistListControl)
                oHistListControl.Dispose()
                oHistListControl = Nothing
                ofrmCPTList.Dispose()
                ofrmCPTList = Nothing
                'End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnclosehist_Click(sender As System.Object, e As System.EventArgs) Handles btnclosehist.Click
        _dtHist = cmbhist.DataSource
        If (Not IsNothing(_dtHist)) Then
            Dim strhistcat As String = cmbhist.Text
            Dim dr As DataRow() = _dtHist.Select("sDescription='" + strhistcat + "'")
            If (dr.Length > 0) Then
                _dtHist.Rows.Remove(dr(0))
            End If
            dr = Nothing
        End If
    End Sub

    Private Sub btncloseloinc_Click(sender As System.Object, e As System.EventArgs) Handles btncloseloinc.Click
        txtloinc.Text = ""
    End Sub
    Dim oListControl As gloListControl.gloListControl
    Dim oRefusalListControl As gloListControl.gloListControl
    Private Sub btnloinc_Click(sender As System.Object, e As System.EventArgs) Handles btnloinc.Click


        Try

            ofrmCPTList = New frmViewListControl
            ' Dim arrCPTTextSplit As String()
            oListControl = New gloListControl.gloListControl(GetConnectionString(), gloListControl.gloListControlType.HistoryLOINC, False, Me.Width)
            oListControl.ControlHeader = "LOINC"
            AddHandler oListControl.ItemSelectedClick, AddressOf oListControl_ItemSelectedClick
            AddHandler oListControl.ItemClosedClick, AddressOf oListControl_ItemClosedClick
            ofrmCPTList.Controls.Add(oListControl)
            oListControl.Dock = DockStyle.Fill
            oListControl.BringToFront()

            oListControl.ShowHeaderPanel(False)
            oListControl.OpenControl()
            ofrmCPTList.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedSingle
            ofrmCPTList.Text = "LOINC"
            ofrmCPTList.ShowDialog(IIf(IsNothing(CType(ofrmCPTList, Control).Parent), Me, CType(ofrmCPTList, Control).Parent))

            If IsNothing(ofrmCPTList) = False Then
                RemoveHandler oListControl.ItemSelectedClick, AddressOf oListControl_ItemSelectedClick
                RemoveHandler oListControl.ItemClosedClick, AddressOf oListControl_ItemClosedClick
                ofrmCPTList.Controls.Remove(oListControl)
                oListControl.Dispose()
                oListControl = Nothing
                ofrmCPTList.Dispose()
                ofrmCPTList = Nothing
                'End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        'oListControl = New gloListControl.gloListControl(GetConnectionString, gloListControl.gloListControlType.CQMOrders, True, Me.Width)
        ''oListControl.CMSID = cmb_measure.SelectedValue.ToString()
        'oListControl.ClinicID = 1
        'oListControl.ControlHeader = "Orders"

        '' AddHandler oListControl.ItemSelectedClick, AddressOf oListControl_ItemSelectedClick
        '' AddHandler oListControl.ItemClosedClick, AddressOf oListControl_ItemClosedClick

        'Me.Controls.Add(oListControl)
        ''    pnlICD9.Visible = True
        ''    pnlICD9.BringToFront()
        ''if (cmbInsCompany.DataSource != null)
        ''{
        ''    for (int i = 0; i < cmbInsCompany.Items.Count; i++)
        ''    {
        ''        cmbInsCompany.SelectedIndex = i;
        ''        cmbInsCompany.Refresh();
        ''        oListControl.SelectedItems.Add(Convert.ToInt64(cmbInsCompany.SelectedValue), cmbInsCompany.Text);
        ''    }
        ''}
        ''   AddSelectedItemstoListControl(lstVw_Lab)
        'oListControl.OpenControl()
        'oListControl.Dock = DockStyle.Fill
        'oListControl.BringToFront()
    End Sub
    Private Sub oListControl_ItemSelectedClick(sender As System.Object, e As System.EventArgs)
        Dim strloinc As String = ""
        If oListControl.SelectedItems.Count > 0 Then
            For i As Int16 = 0 To oListControl.SelectedItems.Count - 1
                strloinc = oListControl.SelectedItems(i).Code + " : " + oListControl.SelectedItems(i).Description
                If (strloinc.Trim().Length > 3) Then
                    txtloinc.Text = strloinc
                Else
                    txtloinc.Text = ""
                End If
                ' strICD9 = txtICD9Code.Text
                '   txtICD9Code.Tag = oDiagnosisListControl.SelectedItems(i).ID
            Next
            ofrmCPTList.Close()
        Else
            txtloinc.Text = ""
            ' txtICD9Code.Tag = ""
            ofrmCPTList.Close()
        End If
            
      

    End Sub
    Private Sub oListControl_ItemClosedClick(sender As System.Object, e As System.EventArgs)

        '

        ' AddItems(lstVw_Lab)
        ofrmCPTList.Close()
        If IsNothing(ofrmCPTList) = False Then
            ofrmCPTList = Nothing
        End If


    End Sub

    Private Sub txtCPTCode_MouseHover(sender As System.Object, e As System.EventArgs) Handles txtCPTCode.MouseHover

        If ToolTiptxtSmokingStatus Is Nothing Then
            ToolTiptxtSmokingStatus = New System.Windows.Forms.ToolTip

        End If
        ToolTiptxtSmokingStatus.AutomaticDelay = 1000


        ToolTiptxtSmokingStatus.SetToolTip(Me.txtCPTCode, txtCPTCode.Text)


    End Sub

    Private Sub txtloinc_MouseHover(sender As System.Object, e As System.EventArgs) Handles txtloinc.MouseHover

        If ToolTiptxtSmokingStatus Is Nothing Then
            ToolTiptxtSmokingStatus = New System.Windows.Forms.ToolTip

        End If
        ToolTiptxtSmokingStatus.AutomaticDelay = 1000


        ToolTiptxtSmokingStatus.SetToolTip(Me.txtloinc, txtloinc.Text)

    End Sub

    Private Sub btnrefusal_Click(sender As System.Object, e As System.EventArgs) Handles btnrefusal.Click
        Try
            ofrmCPTList = New frmViewListControl
            oRefusalListControl = New gloListControl.gloListControl(GetConnectionString(), gloListControl.gloListControlType.MUCQMRefusedCode, False, Me.Width)
            oRefusalListControl.ControlHeader = "Refusal Reason Code"
            'set the property true for refused code you want 
            oRefusalListControl.bShowNotTakenCodes = True
            oRefusalListControl.bShowAttributeCodes = True
            '' oRefusalListControl.strSearchText = strRefusalCode
            AddHandler oRefusalListControl.ItemSelectedClick, AddressOf oRefusalListControl_ItemSelectedClick
            AddHandler oRefusalListControl.ItemClosedClick, AddressOf oRefusalListControl_ItemClosedClick
            ofrmCPTList.Controls.Add(oRefusalListControl)
            oRefusalListControl.Dock = DockStyle.Fill
            oRefusalListControl.BringToFront()

            oRefusalListControl.ShowHeaderPanel(False)
            'oDiagnosisListControl.OpenControl()
            ofrmCPTList.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedSingle
            ofrmCPTList.Text = "Refusal Reason Code"
            ofrmCPTList.ShowDialog(IIf(IsNothing(CType(ofrmCPTList, Control).Parent), Me, CType(ofrmCPTList, Control).Parent))
            If (IsNothing(oDiagnosisListControl) = False) Then
                ofrmCPTList.Controls.Remove(oDiagnosisListControl)
                RemoveHandler oRefusalListControl.ItemSelectedClick, AddressOf oRefusalListControl_ItemSelectedClick
                RemoveHandler oRefusalListControl.ItemClosedClick, AddressOf oRefusalListControl_ItemClosedClick
                oRefusalListControl.Dispose()
                oRefusalListControl = Nothing
            End If

            If IsNothing(ofrmCPTList) = False Then
                ofrmCPTList.Dispose()
                ofrmCPTList = Nothing
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.ProblemList, gloAuditTrail.ActivityCategory.ProblemList, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub oRefusalListControl_ItemClosedClick(sender As Object, e As EventArgs)
        ofrmCPTList.Close()
    End Sub

    Private Sub oRefusalListControl_ItemSelectedClick(sender As Object, e As EventArgs)
        Try
            If oRefusalListControl.SelectedItems.Count > 0 Then
                For i As Int16 = 0 To oRefusalListControl.SelectedItems.Count - 1
                    txtSNOMEDRefusedCode.Text = oRefusalListControl.SelectedItems(i).Code + " ; " + oRefusalListControl.SelectedItems(i).Description
                    '  strRefusalCode = Convert.ToString(oRefusalListControl.SelectedItems(i).Code)
                    '  strRefusalDescription = Convert.ToString(oRefusalListControl.SelectedItems(i).Description)
                    'C1HistoryDetails.SetData(C1HistoryDetails.Row, "sRefusalCode", strRefusalCode)
                    'C1HistoryDetails.SetData(C1HistoryDetails.Row, "sRefusalDesc", strRefusalDescription)
                Next
                ofrmCPTList.Close()
            Else
                txtSNOMEDRefusedCode.Text = ""
                'C1HistoryDetails.SetData(C1HistoryDetails.Row, "sRefusalCode", "")
                'C1HistoryDetails.SetData(C1HistoryDetails.Row, "sRefusalDesc", "")
                ofrmCPTList.Close()
            End If

        Catch ex As Exception
            MessageBox.Show("Error on User List Control." & ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnclrrefusal_Click(sender As System.Object, e As System.EventArgs) Handles btnclrrefusal.Click
        txtSNOMEDRefusedCode.Text = ""
    End Sub
End Class
