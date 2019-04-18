Public Class frmMSTSIG

    Inherits System.Windows.Forms.Form
    '''' While Add, return this Paramerters to View-From so the Newly Added Records get Highlighted 
    Public _Dosage As String
    ''''

    Private m_ID As Long
    Private m_Caption As String
    Dim objclsSIG As New clsSIG
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Public CancelClick As Boolean
    Private WithEvents pnl_tlsp As System.Windows.Forms.Panel
    Private WithEvents tlsp_MSTSIG As gloGlobal.gloToolStripIgnoreFocus
    Private WithEvents ts_btnSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnClose As System.Windows.Forms.ToolStripButton
    Private WithEvents lbl_pnlBottom As System.Windows.Forms.Label
    Private WithEvents lbl_pnlLeft As System.Windows.Forms.Label
    Private WithEvents lbl_pnlRight As System.Windows.Forms.Label
    Private WithEvents lbl_pnlTop As System.Windows.Forms.Label
    Friend WithEvents cmbDuration As System.Windows.Forms.ComboBox
    Friend WithEvents txtRefills As System.Windows.Forms.TextBox
    Friend WithEvents lblRefills As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label

    'sarika 15th oct 07
    Private m_sigid As Long
    Friend WithEvents txtDoseUnit As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtAmount As System.Windows.Forms.TextBox
    Friend WithEvents cmbDoseUnit As System.Windows.Forms.ComboBox
    'variable added by dipak track form closing event occure due to save and close button or close button
    Private isSaveAndClose As Boolean = False
    Public Property SigId() As Long
        Get
            Return m_sigid
        End Get
        Set(ByVal value As Long)
            m_sigid = value
        End Set
    End Property
    '--------------------------------------------------------------------

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()
        'This call is required by the Windows Form Designer.
        InitializeComponent()
        'Add any initialization after the InitializeComponent() call
    End Sub

    Public Sub New(ByVal ID As Long)
        MyBase.New()
        m_ID = ID
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
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtDuration As System.Windows.Forms.TextBox
    Friend WithEvents txtDosage As System.Windows.Forms.TextBox
    Friend WithEvents chkAsNeeded As System.Windows.Forms.CheckBox
    Friend WithEvents txtRoute As System.Windows.Forms.TextBox
    Friend WithEvents txtFrequency As System.Windows.Forms.TextBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMSTSIG))
        Me.txtFrequency = New System.Windows.Forms.TextBox()
        Me.txtRoute = New System.Windows.Forms.TextBox()
        Me.chkAsNeeded = New System.Windows.Forms.CheckBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtDuration = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtDosage = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.cmbDoseUnit = New System.Windows.Forms.ComboBox()
        Me.txtDoseUnit = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtAmount = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtRefills = New System.Windows.Forms.TextBox()
        Me.lblRefills = New System.Windows.Forms.Label()
        Me.cmbDuration = New System.Windows.Forms.ComboBox()
        Me.lbl_pnlBottom = New System.Windows.Forms.Label()
        Me.lbl_pnlLeft = New System.Windows.Forms.Label()
        Me.lbl_pnlRight = New System.Windows.Forms.Label()
        Me.lbl_pnlTop = New System.Windows.Forms.Label()
        Me.pnl_tlsp = New System.Windows.Forms.Panel()
        Me.tlsp_MSTSIG = New gloGlobal.gloToolStripIgnoreFocus()
        Me.ts_btnSave = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnClose = New System.Windows.Forms.ToolStripButton()
        Me.Panel2.SuspendLayout()
        Me.pnl_tlsp.SuspendLayout()
        Me.tlsp_MSTSIG.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtFrequency
        '
        Me.txtFrequency.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFrequency.ForeColor = System.Drawing.Color.Black
        Me.txtFrequency.Location = New System.Drawing.Point(96, 50)
        Me.txtFrequency.MaxLength = 50
        Me.txtFrequency.Name = "txtFrequency"
        Me.txtFrequency.Size = New System.Drawing.Size(238, 22)
        Me.txtFrequency.TabIndex = 1
        '
        'txtRoute
        '
        Me.txtRoute.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRoute.ForeColor = System.Drawing.Color.Black
        Me.txtRoute.Location = New System.Drawing.Point(96, 15)
        Me.txtRoute.MaxLength = 50
        Me.txtRoute.Name = "txtRoute"
        Me.txtRoute.Size = New System.Drawing.Size(238, 22)
        Me.txtRoute.TabIndex = 0
        '
        'chkAsNeeded
        '
        Me.chkAsNeeded.AutoSize = True
        Me.chkAsNeeded.BackColor = System.Drawing.Color.Transparent
        Me.chkAsNeeded.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chkAsNeeded.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAsNeeded.Location = New System.Drawing.Point(21, 183)
        Me.chkAsNeeded.Name = "chkAsNeeded"
        Me.chkAsNeeded.Size = New System.Drawing.Size(90, 18)
        Me.chkAsNeeded.TabIndex = 5
        Me.chkAsNeeded.Text = "As Needed "
        Me.chkAsNeeded.UseVisualStyleBackColor = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(45, 19)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(48, 14)
        Me.Label4.TabIndex = 12
        Me.Label4.Text = "Route :"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(21, 54)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(72, 14)
        Me.Label3.TabIndex = 10
        Me.Label3.Text = "Frequency :"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtDuration
        '
        Me.txtDuration.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDuration.ForeColor = System.Drawing.Color.Black
        Me.txtDuration.Location = New System.Drawing.Point(96, 85)
        Me.txtDuration.MaxLength = 3
        Me.txtDuration.Name = "txtDuration"
        Me.txtDuration.ShortcutsEnabled = False
        Me.txtDuration.Size = New System.Drawing.Size(139, 22)
        Me.txtDuration.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(32, 89)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(61, 14)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "Duration :"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtDosage
        '
        Me.txtDosage.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDosage.ForeColor = System.Drawing.Color.Black
        Me.txtDosage.Location = New System.Drawing.Point(83, 227)
        Me.txtDosage.MaxLength = 50
        Me.txtDosage.Name = "txtDosage"
        Me.txtDosage.Size = New System.Drawing.Size(240, 22)
        Me.txtDosage.TabIndex = 0
        Me.txtDosage.Visible = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(25, 231)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(55, 14)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "Dosage :"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Label1.Visible = False
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Transparent
        Me.Panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel2.Controls.Add(Me.cmbDoseUnit)
        Me.Panel2.Controls.Add(Me.txtDoseUnit)
        Me.Panel2.Controls.Add(Me.Label6)
        Me.Panel2.Controls.Add(Me.txtAmount)
        Me.Panel2.Controls.Add(Me.Label5)
        Me.Panel2.Controls.Add(Me.txtRefills)
        Me.Panel2.Controls.Add(Me.lblRefills)
        Me.Panel2.Controls.Add(Me.cmbDuration)
        Me.Panel2.Controls.Add(Me.lbl_pnlBottom)
        Me.Panel2.Controls.Add(Me.lbl_pnlLeft)
        Me.Panel2.Controls.Add(Me.lbl_pnlRight)
        Me.Panel2.Controls.Add(Me.lbl_pnlTop)
        Me.Panel2.Controls.Add(Me.txtFrequency)
        Me.Panel2.Controls.Add(Me.txtRoute)
        Me.Panel2.Controls.Add(Me.Label2)
        Me.Panel2.Controls.Add(Me.txtDuration)
        Me.Panel2.Controls.Add(Me.chkAsNeeded)
        Me.Panel2.Controls.Add(Me.txtDosage)
        Me.Panel2.Controls.Add(Me.Label4)
        Me.Panel2.Controls.Add(Me.Label3)
        Me.Panel2.Controls.Add(Me.Label1)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(0, 53)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Padding = New System.Windows.Forms.Padding(3)
        Me.Panel2.Size = New System.Drawing.Size(356, 207)
        Me.Panel2.TabIndex = 0
        '
        'cmbDoseUnit
        '
        Me.cmbDoseUnit.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmbDoseUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDoseUnit.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.cmbDoseUnit.FormattingEnabled = True
        Me.cmbDoseUnit.Location = New System.Drawing.Point(173, 154)
        Me.cmbDoseUnit.Name = "cmbDoseUnit"
        Me.cmbDoseUnit.Size = New System.Drawing.Size(161, 22)
        Me.cmbDoseUnit.TabIndex = 48
        Me.cmbDoseUnit.Visible = False
        '
        'txtDoseUnit
        '
        Me.txtDoseUnit.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDoseUnit.ForeColor = System.Drawing.Color.Black
        Me.txtDoseUnit.Location = New System.Drawing.Point(173, 153)
        Me.txtDoseUnit.MaxLength = 30
        Me.txtDoseUnit.Name = "txtDoseUnit"
        Me.txtDoseUnit.ShortcutsEnabled = False
        Me.txtDoseUnit.Size = New System.Drawing.Size(58, 22)
        Me.txtDoseUnit.TabIndex = 47
        Me.txtDoseUnit.Visible = False
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(31, 157)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(62, 14)
        Me.Label6.TabIndex = 45
        Me.Label6.Text = "Quantity :"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtAmount
        '
        Me.txtAmount.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAmount.ForeColor = System.Drawing.Color.Black
        Me.txtAmount.Location = New System.Drawing.Point(97, 153)
        Me.txtAmount.MaxLength = 15
        Me.txtAmount.Name = "txtAmount"
        Me.txtAmount.Size = New System.Drawing.Size(70, 22)
        Me.txtAmount.TabIndex = 46
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.ForeColor = System.Drawing.Color.Red
        Me.Label5.Location = New System.Drawing.Point(34, 21)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(14, 14)
        Me.Label5.TabIndex = 19
        Me.Label5.Text = "*"
        '
        'txtRefills
        '
        Me.txtRefills.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRefills.ForeColor = System.Drawing.Color.Black
        Me.txtRefills.Location = New System.Drawing.Point(96, 120)
        Me.txtRefills.MaxLength = 50
        Me.txtRefills.Name = "txtRefills"
        Me.txtRefills.Size = New System.Drawing.Size(238, 22)
        Me.txtRefills.TabIndex = 4
        '
        'lblRefills
        '
        Me.lblRefills.AutoSize = True
        Me.lblRefills.BackColor = System.Drawing.Color.Transparent
        Me.lblRefills.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRefills.Location = New System.Drawing.Point(11, 124)
        Me.lblRefills.Name = "lblRefills"
        Me.lblRefills.Size = New System.Drawing.Size(82, 14)
        Me.lblRefills.TabIndex = 18
        Me.lblRefills.Text = "No. of Refills :"
        Me.lblRefills.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmbDuration
        '
        Me.cmbDuration.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDuration.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbDuration.ForeColor = System.Drawing.Color.Black
        Me.cmbDuration.Location = New System.Drawing.Point(239, 85)
        Me.cmbDuration.Name = "cmbDuration"
        Me.cmbDuration.Size = New System.Drawing.Size(95, 22)
        Me.cmbDuration.TabIndex = 3
        '
        'lbl_pnlBottom
        '
        Me.lbl_pnlBottom.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_pnlBottom.Location = New System.Drawing.Point(4, 203)
        Me.lbl_pnlBottom.Name = "lbl_pnlBottom"
        Me.lbl_pnlBottom.Size = New System.Drawing.Size(348, 1)
        Me.lbl_pnlBottom.TabIndex = 16
        Me.lbl_pnlBottom.Text = "label2"
        '
        'lbl_pnlLeft
        '
        Me.lbl_pnlLeft.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_pnlLeft.Location = New System.Drawing.Point(3, 4)
        Me.lbl_pnlLeft.Name = "lbl_pnlLeft"
        Me.lbl_pnlLeft.Size = New System.Drawing.Size(1, 200)
        Me.lbl_pnlLeft.TabIndex = 15
        Me.lbl_pnlLeft.Text = "label4"
        '
        'lbl_pnlRight
        '
        Me.lbl_pnlRight.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_pnlRight.Location = New System.Drawing.Point(352, 4)
        Me.lbl_pnlRight.Name = "lbl_pnlRight"
        Me.lbl_pnlRight.Size = New System.Drawing.Size(1, 200)
        Me.lbl_pnlRight.TabIndex = 14
        Me.lbl_pnlRight.Text = "label3"
        '
        'lbl_pnlTop
        '
        Me.lbl_pnlTop.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_pnlTop.Location = New System.Drawing.Point(3, 3)
        Me.lbl_pnlTop.Name = "lbl_pnlTop"
        Me.lbl_pnlTop.Size = New System.Drawing.Size(350, 1)
        Me.lbl_pnlTop.TabIndex = 13
        Me.lbl_pnlTop.Text = "label1"
        '
        'pnl_tlsp
        '
        Me.pnl_tlsp.BackColor = System.Drawing.Color.FromArgb(CType(CType(181, Byte), Integer), CType(CType(216, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.pnl_tlsp.Controls.Add(Me.tlsp_MSTSIG)
        Me.pnl_tlsp.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnl_tlsp.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnl_tlsp.Location = New System.Drawing.Point(0, 0)
        Me.pnl_tlsp.Name = "pnl_tlsp"
        Me.pnl_tlsp.Size = New System.Drawing.Size(356, 53)
        Me.pnl_tlsp.TabIndex = 1
        '
        'tlsp_MSTSIG
        '
        Me.tlsp_MSTSIG.BackColor = System.Drawing.Color.Transparent
        Me.tlsp_MSTSIG.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tlsp_MSTSIG.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlsp_MSTSIG.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlsp_MSTSIG.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tlsp_MSTSIG.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ts_btnSave, Me.ts_btnClose})
        Me.tlsp_MSTSIG.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.tlsp_MSTSIG.Location = New System.Drawing.Point(0, 0)
        Me.tlsp_MSTSIG.Name = "tlsp_MSTSIG"
        Me.tlsp_MSTSIG.Size = New System.Drawing.Size(356, 53)
        Me.tlsp_MSTSIG.TabIndex = 0
        Me.tlsp_MSTSIG.Text = "toolStrip1"
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
        'frmMSTSIG
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(356, 260)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.pnl_tlsp)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmMSTSIG"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "SIG"
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.pnl_tlsp.ResumeLayout(False)
        Me.pnl_tlsp.PerformLayout()
        Me.tlsp_MSTSIG.ResumeLayout(False)
        Me.tlsp_MSTSIG.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub frmSIG_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try

            cmbDuration.Items.Add("Days")
            cmbDuration.Items.Add("Weeks")
            cmbDuration.Items.Add("Months")
            cmbDuration.Text = cmbDuration.Items(0)

            If m_ID <> 0 Then
                Fill_SIG(m_ID)
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Drugs, gloAuditTrail.ActivityCategory.Drugs, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Function SplitDuration(ByVal _StringToSplit As String, ByVal Seperator As String) As Array
        Try
            Dim _result As String()
            _result = _StringToSplit.Split(Seperator)
            Return _result
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Drugs, gloAuditTrail.ActivityCategory.Drugs, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Return Nothing
        End Try

    End Function

    Private Sub Fill_SIG(ByVal ID As Long)
        Dim dv As New DataView
        objclsSIG.SelectSIG(ID)
        dv = objclsSIG.GetDataview
        Dim strDuration As String = ""
        Dim strCmbDuration As String = ""

        Dim strDispAmt As String = ""
        Dim strDoseUnit As String = ""

        txtDosage.Text = dv.Item(0)(0).ToString
        txtRoute.Text = dv.Item(0)(1).ToString
        txtFrequency.Text = dv.Item(0)(2).ToString

        'PER No: 2140 - 20 Jun 2009 - Saagar K 
        'For getting the Refills from the database
        txtRefills.Text = dv.Item(0)(5).ToString
        'PER No: 2140 - 20 Jun 2009 - Saagar K

        'CCHIT 08
        '\\ fetching Duration Value Days\weeks\months
        If Not IsNothing(dv.Item(0)("sDuration")) Then
            Dim retval As String() = SplitDuration(dv.Item(0)("sDuration").ToString, "|") '\\split value with " "(blank space)
            If Not IsNothing(retval) Then
                If retval.Length > 1 Then
                    strDuration = retval(0)
                    strCmbDuration = retval(retval.Length - 1)
                Else
                    strDuration = retval(0) 'dv.Item(0)("sDuration").ToString
                End If
            Else
                strDuration = retval(0) '"" 'dv.Item(0)("sDuration").ToString
            End If
        Else
            strDuration = ""
        End If

        If strCmbDuration <> "" Then
            If strCmbDuration.ToUpper = "DAYS" Then
                cmbDuration.Text = cmbDuration.Items(0) '0th item is Days
            ElseIf strCmbDuration.ToUpper = "WEEKS" Then
                cmbDuration.Text = cmbDuration.Items(1) '1st item is Weeks
            Else
                cmbDuration.Text = cmbDuration.Items(2) '2nd item is Months
            End If
        End If

        txtDuration.Text = strDuration 'dv.Item(0)("sDuration").ToString
        'CCHIT 08

        '\\ dispense amount and doseunit value
        If Not IsNothing(dv.Item(0)("sAmount")) Then
            Dim retval As String() = Nothing
            If dv.Item(0)("sAmount").ToString.Contains("|") Then
                retval = SplitDuration(dv.Item(0)("sAmount").ToString, "|") '\\split value with " "(blank space)
            Else
                retval = SplitDuration(dv.Item(0)("sAmount").ToString, " ") '\\split value with " "(blank space)
            End If

            If Not IsNothing(retval) Then
                If retval.Length > 1 Then
                    strDispAmt = retval(0)
                    strDoseUnit = retval(retval.Length - 1)
                Else
                    strDispAmt = retval(0) 'dv.Item(0)("sDuration").ToString
                End If
            Else
                strDispAmt = retval(0) '"" 'dv.Item(0)("sDuration").ToString
            End If
        Else
            strDispAmt = ""
            strDoseUnit = ""
        End If
        txtAmount.Text = strDispAmt

        If dv.Item(0)(4) = 0 Then
            chkAsNeeded.CheckState = CheckState.Unchecked
        Else
            chkAsNeeded.CheckState = CheckState.Checked
        End If
    End Sub
    'code commented and modify by dipak 20091107 to fix 4784 No validation On Click Of Close Button
    'Private Sub SaveMSTSIG()
    Private Function SaveMSTSIG(Optional ByVal isCallFromClosing As Boolean = False) As Boolean
        Try
            If Trim(txtRoute.Text) = "" Then
                MessageBox.Show("Route is mandatory", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtRoute.Focus()
                'added by dipak 20091107 isSaveAndClose set false to track save operation resume  return false help to cancle clossing event
                isSaveAndClose = False
                Return False
                Exit Function
            End If
            'commented Sandip Darade 20090717
            ''as  dosage in  current context 
            'If objclsSIG.CheckDuplicate(m_ID, Trim(txtDosage.Text)) = True Then
            ''Sandip Darade 20090806
            Dim sDuration As String = ""
            If txtDuration.Text.ToString.Trim.Length > 0 Then
                sDuration = txtDuration.Text & "|" & cmbDuration.Text.ToString()
            End If

            Dim sDispenseAmt As String = ""
            If txtAmount.Text.ToString.Trim.Length > 0 Then
                sDispenseAmt = txtAmount.Text
            End If
            If objclsSIG.CheckDuplicate(m_ID, Trim(txtDosage.Text), Trim(txtRoute.Text), Trim(txtFrequency.Text), sDuration, Trim(txtRefills.Text), sDispenseAmt, chkAsNeeded.CheckState) = True Then
                MessageBox.Show("SIG already exists", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtRoute.Focus()
                'added by dipak 20091107 isSaveAndClose set false to track save operation resume  return false help to cancle clossing event
                isSaveAndClose = False
                Return False
                Exit Function
            End If

            'If m_ID = 0 Then

            '\\Add validation -Discussion with pravin sir

            ' If txtDosage.Text.ToString.Trim.Length > 0 Or txtRoute.Text.ToString.Trim.Length > 0 Or txtFrequency.Text.ToString.Trim.Length > 0 Or txtDuration.Text.ToString.Trim.Length > 0 Or txtRefills.Text.ToString.Trim.Length > 0 Then
            If txtRoute.Text.ToString.Trim.Length > 0 Or txtFrequency.Text.ToString.Trim.Length > 0 Or txtDuration.Text.ToString.Trim.Length > 0 Or txtRefills.Text.ToString.Trim.Length > 0 Or txtAmount.Text.ToString.Trim.Length > 0 Then
                m_sigid = objclsSIG.AddNewSIG(m_ID, Trim(txtDosage.Text), txtRoute.Text, txtFrequency.Text, sDuration, chkAsNeeded.CheckState, txtRefills.Text.Trim, txtAmount.Text.Trim)
            Else
                MessageBox.Show("Enter at least one information", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtDosage.Focus()
                'added by dipak 20091107 isSaveAndClose set false to track save operation resume  return false help to cancle clossing event
                isSaveAndClose = False
                Return False
                Exit Function
            End If

            ''m_sigid = objclsSIG.AddNewSIG(m_ID, Trim(txtDosage.Text), txtRoute.Text, txtFrequency.Text, txtDuration.Text & "|" & cmbDuration.Text.ToString(), chkAsNeeded.CheckState, txtRefills.Text.Trim)
            _Dosage = Trim(txtDosage.Text)

            CancelClick = False
            'Else
            'objclsSIG.UpdateICD9(m_ID, txtICD9Code.Text, txtDescription.Text, cmbSpeciality.SelectedValue)
            'End If
            'code added by dipak 20091107 to avoid recursive call for closing event
            If (isCallFromClosing = False) Then
                isSaveAndClose = True
                Me.Close()
            End If
            Return True
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Drugs, gloAuditTrail.ActivityCategory.Drugs, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End Try
    End Function

    Private Sub CloseMSTSIG()
        CancelClick = True
        isSaveAndClose = False
        Me.Close()
    End Sub

    Private Sub tlsp_MSTSIG_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tlsp_MSTSIG.ItemClicked
        Try
            Select Case e.ClickedItem.Tag
                Case "Save"
                    isSaveAndClose = True
                    SaveMSTSIG()
                Case "Close"
                    CloseMSTSIG()

            End Select

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Drugs, gloAuditTrail.ActivityCategory.Drugs, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK)
        End Try
    End Sub

    Private Sub frmMSTSIG_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        'code added by dipak 20091107 to fix bug 4784 No validation On Click Of Close Button
        If (isSaveAndClose = False) Then
            Dim Result As Integer
            'Check if Yes no and cancel
            Result = MsgBox("Do you want to save the changes to SIG ?  ", MsgBoxStyle.Question + MsgBoxStyle.YesNoCancel)
            If Result = MsgBoxResult.Yes Then
                'optional patameter isCallFromClosing is pass true to track save is call by click close button
                If (SaveMSTSIG(True) = False) Then
                    e.Cancel = True
                End If
            ElseIf Result = MsgBoxResult.No Then

            ElseIf Result = MsgBoxResult.Cancel Then
                e.Cancel = True
            End If

        End If

    End Sub

    '' issue: 5493
    ''20091205 Added as per pravin sir discussion: Duration should be numeric value
    Private Sub txtDuration_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtDuration.KeyPress
        Try

            Dim chkNumeric As String = txtDuration.Text.Trim()
            If e.KeyChar.ToString() = " " Then
                e.Handled = True
                Exit Sub
            End If

            If e.KeyChar = vbBack Or e.KeyChar = vbCr Then
                e.Handled = False
            Else

                If Char.IsDigit(e.KeyChar) Then

                Else
                    If e.KeyChar = "." And chkNumeric.Contains(".") = False Then

                        MessageBox.Show("Enter valid Numeric value", "gloEMR", MessageBoxButtons.OK)
                        e.Handled = True
                        Exit Sub
                    Else
                        MessageBox.Show("Enter valid Numeric value", "gloEMR", MessageBoxButtons.OK)
                        e.Handled = True
                        Exit Sub
                    End If
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub txtRefills_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtRefills.KeyPress
        Try

            Dim chkNumeric As String = txtRefills.Text.Trim()
            If e.KeyChar = vbBack Or e.KeyChar = vbCr Then
                e.Handled = False
            Else

                If Char.IsDigit(e.KeyChar) Then

                Else
                    If e.KeyChar = "." And chkNumeric.Contains(".") = False Then

                    Else
                        MessageBox.Show("Enter valid numeric value", "gloEMR", MessageBoxButtons.OK)
                        e.Handled = True

                        Exit Sub
                    End If
                End If


            End If


        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Drugs, gloAuditTrail.ActivityCategory.Drugs, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK)
        End Try
    End Sub

    Private Sub txtAmount_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAmount.KeyPress
        Try

            Dim chkNumeric As String = txtAmount.Text.Trim()
            If e.KeyChar.ToString() = " " Then
                e.Handled = True
                Exit Sub

            End If



            'If chkNumeric = "" Then

            '    Exit Sub
            'End If
            If e.KeyChar = vbBack Or e.KeyChar = vbCr Then
                e.Handled = False
            Else

                If Char.IsDigit(e.KeyChar) Then

                Else
                    If e.KeyChar = "." And chkNumeric.Contains(".") = False Then

                    Else
                        MessageBox.Show("Enter valid numeric or decimal value", "gloEMR", MessageBoxButtons.OK)
                        e.Handled = True
                        Exit Sub
                    End If
                End If


            End If


        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Drugs, gloAuditTrail.ActivityCategory.Drugs, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK)
        End Try
    End Sub


    Private Sub FillPotencyCode()
        Dim dtPotencyCode As New DataTable

        'Dim oDblayer As New gloEMRGeneralLibrary.gloEMRPrescription.RxBusinesslayer(0)

        Try
            If cmbDoseUnit.Items.Count = 0 Then
                Using helper As New gloEMRGeneralLibrary.PrescriptionBusinessLayer()
                    dtPotencyCode = helper.GetPotencyCode()
                End Using
                If dtPotencyCode IsNot Nothing Then
                    If dtPotencyCode.Rows.Count > 0 Then

                        Dim dr As DataRow = dtPotencyCode.NewRow()
                        dr("sPotencycode") = "0"
                        dr("sDescription") = ""
                        dtPotencyCode.Rows.InsertAt(dr, 0)
                        dtPotencyCode.AcceptChanges()

                        cmbDoseUnit.DataSource = dtPotencyCode
                        cmbDoseUnit.ValueMember = dtPotencyCode.Columns("sPotencycode").ColumnName
                        cmbDoseUnit.DisplayMember = dtPotencyCode.Columns("sDescription").ColumnName

                    End If
                End If
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Drugs, gloAuditTrail.ActivityCategory.Drugs, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            If Not IsNothing(dtPotencyCode) Then
                dtPotencyCode.Dispose()
                dtPotencyCode = Nothing
            End If
            'If Not IsNothing(oDblayer) Then
            '    oDblayer.Dispose()
            '    oDblayer = Nothing
            'End If
        Finally
          
            'If Not IsNothing(oDblayer) Then
            '    oDblayer.Dispose()
            '    oDblayer = Nothing
            'End If
        End Try
    End Sub

    Private Sub cmbDoseUnit_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbDoseUnit.SelectedIndexChanged

    End Sub
End Class
