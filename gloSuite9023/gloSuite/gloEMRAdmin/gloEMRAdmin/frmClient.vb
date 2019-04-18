'***************************************************************************
' Module Name :- gloEMR Admin Login
' Company Name :- gloStream Inc.
' Written By :- Pankaj Naval
' Description :-
'This form is to validate the User Name and Password
'Processes
'   1) 
'***************************************************************************
Public Class frmClient
    Inherits System.Windows.Forms.Form
    Public blnModify As Boolean
    Friend WithEvents chk_allGen As System.Windows.Forms.CheckBox
    Friend WithEvents chk_allhl7 As System.Windows.Forms.CheckBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Private WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents Label22 As System.Windows.Forms.Label
    Private WithEvents Label3 As System.Windows.Forms.Label
    Private WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Private WithEvents Label11 As System.Windows.Forms.Label
    Private WithEvents Label12 As System.Windows.Forms.Label
    Private WithEvents Label13 As System.Windows.Forms.Label
    Private WithEvents Label14 As System.Windows.Forms.Label
    Private WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents chkgenius_default As System.Windows.Forms.CheckBox
    Friend WithEvents chkhl7_default As System.Windows.Forms.CheckBox
    Friend WithEvents chk_HL7_SendVisitSum_SaveClose As System.Windows.Forms.CheckBox
    Friend WithEvents Chk_HL7_SendVisitSum_SaveFinish As System.Windows.Forms.CheckBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmbMachineName As System.Windows.Forms.ComboBox
    Friend WithEvents txtMachineName As System.Windows.Forms.TextBox
    Private WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents btnBrowse As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents optVoiceYes As System.Windows.Forms.RadioButton
    Friend WithEvents optVoiceNo As System.Windows.Forms.RadioButton
    Private WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents optScanYes As System.Windows.Forms.RadioButton
    Friend WithEvents optScanNo As System.Windows.Forms.RadioButton
    Private WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Private WithEvents Label5 As System.Windows.Forms.Label
    Dim obj As New clsClientInterface
#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

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
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Private WithEvents pnl_tlsp_Top As System.Windows.Forms.Panel
    Friend WithEvents tstrip As System.Windows.Forms.ToolStrip
    Friend WithEvents btnOK As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnCancel As System.Windows.Forms.ToolStripButton
    Friend WithEvents chkHL7Appointment As System.Windows.Forms.CheckBox
    Friend WithEvents chkHL7Immunization As System.Windows.Forms.CheckBox
    Friend WithEvents chkPatientReg As System.Windows.Forms.CheckBox
    Friend WithEvents chk_HL7_SendCharges_SaveFinish As System.Windows.Forms.CheckBox
    Friend WithEvents chk_HL7_SendCharges_SaveClose As System.Windows.Forms.CheckBox
    Friend WithEvents pnlHl7 As System.Windows.Forms.Panel
    Friend WithEvents pnlGenius As System.Windows.Forms.Panel
    Friend WithEvents chk_Gen_SaveandFinish As System.Windows.Forms.CheckBox
    Friend WithEvents chk_Gen_SaveandClose As System.Windows.Forms.CheckBox
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Private WithEvents Label20 As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmClient))
        Me.pnl_tlsp_Top = New System.Windows.Forms.Panel()
        Me.tstrip = New System.Windows.Forms.ToolStrip()
        Me.btnOK = New System.Windows.Forms.ToolStripButton()
        Me.btnCancel = New System.Windows.Forms.ToolStripButton()
        Me.chkHL7Appointment = New System.Windows.Forms.CheckBox()
        Me.chkHL7Immunization = New System.Windows.Forms.CheckBox()
        Me.chkPatientReg = New System.Windows.Forms.CheckBox()
        Me.pnlGenius = New System.Windows.Forms.Panel()
        Me.chkgenius_default = New System.Windows.Forms.CheckBox()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.chk_allGen = New System.Windows.Forms.CheckBox()
        Me.chk_Gen_SaveandFinish = New System.Windows.Forms.CheckBox()
        Me.chk_Gen_SaveandClose = New System.Windows.Forms.CheckBox()
        Me.pnlHl7 = New System.Windows.Forms.Panel()
        Me.chk_HL7_SendVisitSum_SaveClose = New System.Windows.Forms.CheckBox()
        Me.Chk_HL7_SendVisitSum_SaveFinish = New System.Windows.Forms.CheckBox()
        Me.chkhl7_default = New System.Windows.Forms.CheckBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.chk_allhl7 = New System.Windows.Forms.CheckBox()
        Me.chk_HL7_SendCharges_SaveClose = New System.Windows.Forms.CheckBox()
        Me.chk_HL7_SendCharges_SaveFinish = New System.Windows.Forms.CheckBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmbMachineName = New System.Windows.Forms.ComboBox()
        Me.txtMachineName = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.btnBrowse = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.optVoiceYes = New System.Windows.Forms.RadioButton()
        Me.optVoiceNo = New System.Windows.Forms.RadioButton()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.optScanYes = New System.Windows.Forms.RadioButton()
        Me.optScanNo = New System.Windows.Forms.RadioButton()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.pnl_tlsp_Top.SuspendLayout()
        Me.tstrip.SuspendLayout()
        Me.pnlGenius.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.pnlHl7.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnl_tlsp_Top
        '
        Me.pnl_tlsp_Top.AutoSize = True
        Me.pnl_tlsp_Top.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnl_tlsp_Top.Controls.Add(Me.tstrip)
        Me.pnl_tlsp_Top.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnl_tlsp_Top.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnl_tlsp_Top.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.pnl_tlsp_Top.Location = New System.Drawing.Point(0, 0)
        Me.pnl_tlsp_Top.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.pnl_tlsp_Top.Name = "pnl_tlsp_Top"
        Me.pnl_tlsp_Top.Size = New System.Drawing.Size(507, 53)
        Me.pnl_tlsp_Top.TabIndex = 18
        '
        'tstrip
        '
        Me.tstrip.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.tstrip.BackgroundImage = CType(resources.GetObject("tstrip.BackgroundImage"), System.Drawing.Image)
        Me.tstrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tstrip.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tstrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.tstrip.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tstrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnOK, Me.btnCancel})
        Me.tstrip.Location = New System.Drawing.Point(0, 0)
        Me.tstrip.Name = "tstrip"
        Me.tstrip.Size = New System.Drawing.Size(507, 53)
        Me.tstrip.TabIndex = 0
        Me.tstrip.Text = "ToolStrip1"
        '
        'btnOK
        '
        Me.btnOK.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnOK.Image = CType(resources.GetObject("btnOK.Image"), System.Drawing.Image)
        Me.btnOK.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(66, 50)
        Me.btnOK.Text = "&Save&&Cls"
        Me.btnOK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnOK.ToolTipText = "Save and Close"
        '
        'btnCancel
        '
        Me.btnCancel.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancel.Image = CType(resources.GetObject("btnCancel.Image"), System.Drawing.Image)
        Me.btnCancel.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(43, 50)
        Me.btnCancel.Text = "&Close"
        Me.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnCancel.ToolTipText = "Close"
        '
        'chkHL7Appointment
        '
        Me.chkHL7Appointment.AutoSize = True
        Me.chkHL7Appointment.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkHL7Appointment.Location = New System.Drawing.Point(262, 54)
        Me.chkHL7Appointment.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.chkHL7Appointment.Name = "chkHL7Appointment"
        Me.chkHL7Appointment.Size = New System.Drawing.Size(169, 18)
        Me.chkHL7Appointment.TabIndex = 6
        Me.chkHL7Appointment.Text = "Send Appointment Details"
        Me.chkHL7Appointment.UseVisualStyleBackColor = True
        '
        'chkHL7Immunization
        '
        Me.chkHL7Immunization.AutoSize = True
        Me.chkHL7Immunization.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkHL7Immunization.Location = New System.Drawing.Point(16, 126)
        Me.chkHL7Immunization.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.chkHL7Immunization.Name = "chkHL7Immunization"
        Me.chkHL7Immunization.Size = New System.Drawing.Size(169, 18)
        Me.chkHL7Immunization.TabIndex = 11
        Me.chkHL7Immunization.Text = "Send Immunization Details"
        Me.chkHL7Immunization.UseVisualStyleBackColor = True
        '
        'chkPatientReg
        '
        Me.chkPatientReg.AutoSize = True
        Me.chkPatientReg.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkPatientReg.Location = New System.Drawing.Point(16, 54)
        Me.chkPatientReg.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.chkPatientReg.Name = "chkPatientReg"
        Me.chkPatientReg.Size = New System.Drawing.Size(136, 18)
        Me.chkPatientReg.TabIndex = 5
        Me.chkPatientReg.Text = "Send Patient Details"
        Me.chkPatientReg.UseVisualStyleBackColor = True
        '
        'pnlGenius
        '
        Me.pnlGenius.Controls.Add(Me.chkgenius_default)
        Me.pnlGenius.Controls.Add(Me.Panel3)
        Me.pnlGenius.Controls.Add(Me.Label2)
        Me.pnlGenius.Controls.Add(Me.Label22)
        Me.pnlGenius.Controls.Add(Me.Label3)
        Me.pnlGenius.Controls.Add(Me.Label4)
        Me.pnlGenius.Controls.Add(Me.chk_allGen)
        Me.pnlGenius.Controls.Add(Me.chk_Gen_SaveandFinish)
        Me.pnlGenius.Controls.Add(Me.chk_Gen_SaveandClose)
        Me.pnlGenius.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlGenius.Location = New System.Drawing.Point(0, 296)
        Me.pnlGenius.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.pnlGenius.Name = "pnlGenius"
        Me.pnlGenius.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.pnlGenius.Size = New System.Drawing.Size(507, 105)
        Me.pnlGenius.TabIndex = 15
        '
        'chkgenius_default
        '
        Me.chkgenius_default.AutoSize = True
        Me.chkgenius_default.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkgenius_default.Location = New System.Drawing.Point(262, 32)
        Me.chkgenius_default.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.chkgenius_default.Name = "chkgenius_default"
        Me.chkgenius_default.Size = New System.Drawing.Size(96, 18)
        Me.chkgenius_default.TabIndex = 30
        Me.chkgenius_default.Text = "Use Default"
        Me.chkgenius_default.UseVisualStyleBackColor = True
        '
        'Panel3
        '
        Me.Panel3.BackgroundImage = Global.gloEMRAdmin.My.Resources.Resources.Img_Button
        Me.Panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel3.Controls.Add(Me.Label9)
        Me.Panel3.Controls.Add(Me.Label20)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(4, 1)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(499, 22)
        Me.Panel3.TabIndex = 5
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(0, 0)
        Me.Label9.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(499, 21)
        Me.Label9.TabIndex = 12
        Me.Label9.Text = "   Genius Outbound Settings"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label20
        '
        Me.Label20.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label20.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label20.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.Location = New System.Drawing.Point(0, 21)
        Me.Label20.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(499, 1)
        Me.Label20.TabIndex = 11
        Me.Label20.Text = "label1"
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label2.Location = New System.Drawing.Point(4, 101)
        Me.Label2.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(499, 1)
        Me.Label2.TabIndex = 21
        Me.Label2.Text = "label2"
        '
        'Label22
        '
        Me.Label22.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label22.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label22.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.Location = New System.Drawing.Point(4, 0)
        Me.Label22.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(499, 1)
        Me.Label22.TabIndex = 20
        Me.Label22.Text = "label1"
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(3, 0)
        Me.Label3.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(1, 102)
        Me.Label3.TabIndex = 22
        Me.Label3.Text = "label4"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label4.Location = New System.Drawing.Point(503, 0)
        Me.Label4.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(1, 102)
        Me.Label4.TabIndex = 23
        Me.Label4.Text = "label3"
        '
        'chk_allGen
        '
        Me.chk_allGen.AutoSize = True
        Me.chk_allGen.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chk_allGen.Location = New System.Drawing.Point(16, 32)
        Me.chk_allGen.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.chk_allGen.Name = "chk_allGen"
        Me.chk_allGen.Size = New System.Drawing.Size(86, 18)
        Me.chk_allGen.TabIndex = 15
        Me.chk_allGen.Text = "All Events"
        Me.chk_allGen.UseVisualStyleBackColor = True
        '
        'chk_Gen_SaveandFinish
        '
        Me.chk_Gen_SaveandFinish.AutoSize = True
        Me.chk_Gen_SaveandFinish.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chk_Gen_SaveandFinish.Location = New System.Drawing.Point(16, 80)
        Me.chk_Gen_SaveandFinish.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.chk_Gen_SaveandFinish.Name = "chk_Gen_SaveandFinish"
        Me.chk_Gen_SaveandFinish.Size = New System.Drawing.Size(206, 18)
        Me.chk_Gen_SaveandFinish.TabIndex = 4
        Me.chk_Gen_SaveandFinish.Text = "Send Charges on Save and Finish"
        Me.chk_Gen_SaveandFinish.UseVisualStyleBackColor = True
        '
        'chk_Gen_SaveandClose
        '
        Me.chk_Gen_SaveandClose.AutoSize = True
        Me.chk_Gen_SaveandClose.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chk_Gen_SaveandClose.Location = New System.Drawing.Point(16, 56)
        Me.chk_Gen_SaveandClose.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.chk_Gen_SaveandClose.Name = "chk_Gen_SaveandClose"
        Me.chk_Gen_SaveandClose.Size = New System.Drawing.Size(205, 18)
        Me.chk_Gen_SaveandClose.TabIndex = 3
        Me.chk_Gen_SaveandClose.Text = "Send Charges on Save and Close"
        Me.chk_Gen_SaveandClose.UseVisualStyleBackColor = True
        '
        'pnlHl7
        '
        Me.pnlHl7.Controls.Add(Me.chk_HL7_SendVisitSum_SaveClose)
        Me.pnlHl7.Controls.Add(Me.Chk_HL7_SendVisitSum_SaveFinish)
        Me.pnlHl7.Controls.Add(Me.chkhl7_default)
        Me.pnlHl7.Controls.Add(Me.Panel1)
        Me.pnlHl7.Controls.Add(Me.Label12)
        Me.pnlHl7.Controls.Add(Me.Label13)
        Me.pnlHl7.Controls.Add(Me.Label14)
        Me.pnlHl7.Controls.Add(Me.Label15)
        Me.pnlHl7.Controls.Add(Me.chk_allhl7)
        Me.pnlHl7.Controls.Add(Me.chkPatientReg)
        Me.pnlHl7.Controls.Add(Me.chkHL7Appointment)
        Me.pnlHl7.Controls.Add(Me.chk_HL7_SendCharges_SaveClose)
        Me.pnlHl7.Controls.Add(Me.chk_HL7_SendCharges_SaveFinish)
        Me.pnlHl7.Controls.Add(Me.chkHL7Immunization)
        Me.pnlHl7.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHl7.Location = New System.Drawing.Point(0, 146)
        Me.pnlHl7.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.pnlHl7.Name = "pnlHl7"
        Me.pnlHl7.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.pnlHl7.Size = New System.Drawing.Size(507, 150)
        Me.pnlHl7.TabIndex = 16
        '
        'chk_HL7_SendVisitSum_SaveClose
        '
        Me.chk_HL7_SendVisitSum_SaveClose.AutoSize = True
        Me.chk_HL7_SendVisitSum_SaveClose.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chk_HL7_SendVisitSum_SaveClose.Location = New System.Drawing.Point(16, 102)
        Me.chk_HL7_SendVisitSum_SaveClose.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.chk_HL7_SendVisitSum_SaveClose.Name = "chk_HL7_SendVisitSum_SaveClose"
        Me.chk_HL7_SendVisitSum_SaveClose.Size = New System.Drawing.Size(238, 18)
        Me.chk_HL7_SendVisitSum_SaveClose.TabIndex = 9
        Me.chk_HL7_SendVisitSum_SaveClose.Text = "Send Visit Summary on Save and Close"
        Me.chk_HL7_SendVisitSum_SaveClose.UseVisualStyleBackColor = True
        '
        'Chk_HL7_SendVisitSum_SaveFinish
        '
        Me.Chk_HL7_SendVisitSum_SaveFinish.AutoSize = True
        Me.Chk_HL7_SendVisitSum_SaveFinish.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Chk_HL7_SendVisitSum_SaveFinish.Location = New System.Drawing.Point(262, 102)
        Me.Chk_HL7_SendVisitSum_SaveFinish.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.Chk_HL7_SendVisitSum_SaveFinish.Name = "Chk_HL7_SendVisitSum_SaveFinish"
        Me.Chk_HL7_SendVisitSum_SaveFinish.Size = New System.Drawing.Size(239, 18)
        Me.Chk_HL7_SendVisitSum_SaveFinish.TabIndex = 10
        Me.Chk_HL7_SendVisitSum_SaveFinish.Text = "Send Visit Summary on Save and Finish"
        Me.Chk_HL7_SendVisitSum_SaveFinish.UseVisualStyleBackColor = True
        '
        'chkhl7_default
        '
        Me.chkhl7_default.AutoSize = True
        Me.chkhl7_default.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkhl7_default.Location = New System.Drawing.Point(262, 30)
        Me.chkhl7_default.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.chkhl7_default.Name = "chkhl7_default"
        Me.chkhl7_default.Size = New System.Drawing.Size(96, 18)
        Me.chkhl7_default.TabIndex = 29
        Me.chkhl7_default.Text = "Use Default"
        Me.chkhl7_default.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.BackgroundImage = Global.gloEMRAdmin.My.Resources.Resources.Img_Button
        Me.Panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel1.Controls.Add(Me.Label10)
        Me.Panel1.Controls.Add(Me.Label11)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(4, 1)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(499, 22)
        Me.Panel1.TabIndex = 28
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label10.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(0, 0)
        Me.Label10.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(499, 21)
        Me.Label10.TabIndex = 12
        Me.Label10.Text = "   HL7 Outbound Settings"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label11.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label11.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(0, 21)
        Me.Label11.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(499, 1)
        Me.Label11.TabIndex = 11
        Me.Label11.Text = "label1"
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label12.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label12.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label12.Location = New System.Drawing.Point(4, 146)
        Me.Label12.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(499, 1)
        Me.Label12.TabIndex = 25
        Me.Label12.Text = "label2"
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label13.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label13.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(4, 0)
        Me.Label13.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(499, 1)
        Me.Label13.TabIndex = 24
        Me.Label13.Text = "label1"
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label14.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label14.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(3, 0)
        Me.Label14.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(1, 147)
        Me.Label14.TabIndex = 26
        Me.Label14.Text = "label4"
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label15.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label15.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label15.Location = New System.Drawing.Point(503, 0)
        Me.Label15.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(1, 147)
        Me.Label15.TabIndex = 27
        Me.Label15.Text = "label3"
        '
        'chk_allhl7
        '
        Me.chk_allhl7.AutoSize = True
        Me.chk_allhl7.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chk_allhl7.Location = New System.Drawing.Point(16, 30)
        Me.chk_allhl7.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.chk_allhl7.Name = "chk_allhl7"
        Me.chk_allhl7.Size = New System.Drawing.Size(86, 18)
        Me.chk_allhl7.TabIndex = 16
        Me.chk_allhl7.Text = "All Events"
        Me.chk_allhl7.UseVisualStyleBackColor = True
        '
        'chk_HL7_SendCharges_SaveClose
        '
        Me.chk_HL7_SendCharges_SaveClose.AutoSize = True
        Me.chk_HL7_SendCharges_SaveClose.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chk_HL7_SendCharges_SaveClose.Location = New System.Drawing.Point(16, 78)
        Me.chk_HL7_SendCharges_SaveClose.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.chk_HL7_SendCharges_SaveClose.Name = "chk_HL7_SendCharges_SaveClose"
        Me.chk_HL7_SendCharges_SaveClose.Size = New System.Drawing.Size(205, 18)
        Me.chk_HL7_SendCharges_SaveClose.TabIndex = 7
        Me.chk_HL7_SendCharges_SaveClose.Text = "Send Charges on Save and Close"
        Me.chk_HL7_SendCharges_SaveClose.UseVisualStyleBackColor = True
        '
        'chk_HL7_SendCharges_SaveFinish
        '
        Me.chk_HL7_SendCharges_SaveFinish.AutoSize = True
        Me.chk_HL7_SendCharges_SaveFinish.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chk_HL7_SendCharges_SaveFinish.Location = New System.Drawing.Point(262, 78)
        Me.chk_HL7_SendCharges_SaveFinish.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.chk_HL7_SendCharges_SaveFinish.Name = "chk_HL7_SendCharges_SaveFinish"
        Me.chk_HL7_SendCharges_SaveFinish.Size = New System.Drawing.Size(206, 18)
        Me.chk_HL7_SendCharges_SaveFinish.TabIndex = 8
        Me.chk_HL7_SendCharges_SaveFinish.Text = "Send Charges on Save and Finish"
        Me.chk_HL7_SendCharges_SaveFinish.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(4, 12)
        Me.Label1.Margin = New System.Windows.Forms.Padding(0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(97, 15)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "C&lient Name :"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmbMachineName
        '
        Me.cmbMachineName.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbMachineName.Location = New System.Drawing.Point(105, 8)
        Me.cmbMachineName.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.cmbMachineName.Name = "cmbMachineName"
        Me.cmbMachineName.Size = New System.Drawing.Size(248, 22)
        Me.cmbMachineName.TabIndex = 0
        Me.cmbMachineName.Visible = False
        '
        'txtMachineName
        '
        Me.txtMachineName.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMachineName.Location = New System.Drawing.Point(105, 8)
        Me.txtMachineName.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.txtMachineName.Name = "txtMachineName"
        Me.txtMachineName.Size = New System.Drawing.Size(248, 22)
        Me.txtMachineName.TabIndex = 1
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(3, 3)
        Me.Label8.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(501, 1)
        Me.Label8.TabIndex = 5
        Me.Label8.Text = "label1"
        '
        'btnBrowse
        '
        Me.btnBrowse.AutoSize = True
        Me.btnBrowse.BackColor = System.Drawing.Color.Transparent
        Me.btnBrowse.BackgroundImage = Global.gloEMRAdmin.My.Resources.Resources.Img_Button
        Me.btnBrowse.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnBrowse.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.btnBrowse.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.btnBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBrowse.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBrowse.Image = CType(resources.GetObject("btnBrowse.Image"), System.Drawing.Image)
        Me.btnBrowse.Location = New System.Drawing.Point(358, 7)
        Me.btnBrowse.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.btnBrowse.Name = "btnBrowse"
        Me.btnBrowse.Size = New System.Drawing.Size(24, 24)
        Me.btnBrowse.TabIndex = 1
        Me.btnBrowse.UseVisualStyleBackColor = False
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox1.Controls.Add(Me.optVoiceYes)
        Me.GroupBox1.Controls.Add(Me.optVoiceNo)
        Me.GroupBox1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.GroupBox1.Location = New System.Drawing.Point(15, 35)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.GroupBox1.Size = New System.Drawing.Size(122, 50)
        Me.GroupBox1.TabIndex = 6
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Voice Facility"
        '
        'optVoiceYes
        '
        Me.optVoiceYes.BackColor = System.Drawing.Color.Transparent
        Me.optVoiceYes.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optVoiceYes.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.optVoiceYes.Location = New System.Drawing.Point(12, 22)
        Me.optVoiceYes.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.optVoiceYes.Name = "optVoiceYes"
        Me.optVoiceYes.Size = New System.Drawing.Size(47, 18)
        Me.optVoiceYes.TabIndex = 0
        Me.optVoiceYes.Text = "Yes"
        Me.optVoiceYes.UseVisualStyleBackColor = False
        '
        'optVoiceNo
        '
        Me.optVoiceNo.BackColor = System.Drawing.Color.Transparent
        Me.optVoiceNo.Checked = True
        Me.optVoiceNo.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optVoiceNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.optVoiceNo.Location = New System.Drawing.Point(66, 22)
        Me.optVoiceNo.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.optVoiceNo.Name = "optVoiceNo"
        Me.optVoiceNo.Size = New System.Drawing.Size(44, 18)
        Me.optVoiceNo.TabIndex = 1
        Me.optVoiceNo.TabStop = True
        Me.optVoiceNo.Text = "No"
        Me.optVoiceNo.UseVisualStyleBackColor = False
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label7.Location = New System.Drawing.Point(503, 4)
        Me.Label7.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(1, 86)
        Me.Label7.TabIndex = 6
        Me.Label7.Text = "label3"
        '
        'GroupBox2
        '
        Me.GroupBox2.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox2.Controls.Add(Me.optScanYes)
        Me.GroupBox2.Controls.Add(Me.optScanNo)
        Me.GroupBox2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.GroupBox2.Location = New System.Drawing.Point(150, 35)
        Me.GroupBox2.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Padding = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.GroupBox2.Size = New System.Drawing.Size(122, 48)
        Me.GroupBox2.TabIndex = 7
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Scan Facility"
        '
        'optScanYes
        '
        Me.optScanYes.BackColor = System.Drawing.Color.Transparent
        Me.optScanYes.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optScanYes.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.optScanYes.Location = New System.Drawing.Point(11, 19)
        Me.optScanYes.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.optScanYes.Name = "optScanYes"
        Me.optScanYes.Size = New System.Drawing.Size(46, 18)
        Me.optScanYes.TabIndex = 0
        Me.optScanYes.Text = "Yes"
        Me.optScanYes.UseVisualStyleBackColor = False
        '
        'optScanNo
        '
        Me.optScanNo.BackColor = System.Drawing.Color.Transparent
        Me.optScanNo.Checked = True
        Me.optScanNo.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optScanNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.optScanNo.Location = New System.Drawing.Point(64, 19)
        Me.optScanNo.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.optScanNo.Name = "optScanNo"
        Me.optScanNo.Size = New System.Drawing.Size(43, 18)
        Me.optScanNo.TabIndex = 1
        Me.optScanNo.TabStop = True
        Me.optScanNo.Text = "No"
        Me.optScanNo.UseVisualStyleBackColor = False
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(3, 4)
        Me.Label6.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(1, 86)
        Me.Label6.TabIndex = 7
        Me.Label6.Text = "label4"
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel2.Controls.Add(Me.Label5)
        Me.Panel2.Controls.Add(Me.Label6)
        Me.Panel2.Controls.Add(Me.GroupBox2)
        Me.Panel2.Controls.Add(Me.Label7)
        Me.Panel2.Controls.Add(Me.GroupBox1)
        Me.Panel2.Controls.Add(Me.btnBrowse)
        Me.Panel2.Controls.Add(Me.Label8)
        Me.Panel2.Controls.Add(Me.txtMachineName)
        Me.Panel2.Controls.Add(Me.cmbMachineName)
        Me.Panel2.Controls.Add(Me.Label1)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel2.Location = New System.Drawing.Point(0, 53)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Padding = New System.Windows.Forms.Padding(3)
        Me.Panel2.Size = New System.Drawing.Size(507, 93)
        Me.Panel2.TabIndex = 20
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label5.Location = New System.Drawing.Point(4, 89)
        Me.Label5.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(499, 1)
        Me.Label5.TabIndex = 8
        Me.Label5.Text = "label2"
        '
        'frmClient
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoSize = True
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(507, 401)
        Me.Controls.Add(Me.pnlGenius)
        Me.Controls.Add(Me.pnlHl7)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.pnl_tlsp_Top)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmClient"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Client Settings"
        Me.pnl_tlsp_Top.ResumeLayout(False)
        Me.pnl_tlsp_Top.PerformLayout()
        Me.tstrip.ResumeLayout(False)
        Me.tstrip.PerformLayout()
        Me.pnlGenius.ResumeLayout(False)
        Me.pnlGenius.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.pnlHl7.ResumeLayout(False)
        Me.pnlHl7.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Try
            Me.DialogResult = DialogResult.Cancel
            Me.Close()
        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ClientInterfaceFill(ByVal Machinename As String)
        Try
            obj.HL7_SendAppointmentDetails = chkHL7Appointment.CheckState
            obj.Hl7_SendPatientDetails = chkPatientReg.CheckState
            obj.Hl7_SendImmunizationDetails = chkHL7Immunization.CheckState
            obj.HL7_SendChargesSaveClose = chk_HL7_SendCharges_SaveClose.CheckState
            obj.HL7_SendChargesSaveFinish = chk_HL7_SendCharges_SaveFinish.CheckState
            obj.Genius_SendChargesSaveClose = chk_Gen_SaveandClose.CheckState
            obj.Genius_SendChargesSaveFinish = chk_Gen_SaveandFinish.CheckState

            obj.HL7_SendVisitSumSaveClose = chk_HL7_SendVisitSum_SaveClose.CheckState
            obj.HL7_SendVisitSumSaveFinish = Chk_HL7_SendVisitSum_SaveFinish.CheckState

            obj.ProductName = "gloEMR"
            obj.MachineName = Machinename

            obj.InsertClientInterface(chkhl7_default.Checked, chkgenius_default.Checked)

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    'Private Sub AllClientInterfaceFill()
    '    Try
    '        obj.HL7_SendAppointmentDetails = chkHL7Appointment.CheckState
    '        obj.Hl7_SendPatientDetails = chkPatientReg.CheckState
    '        obj.Hl7_SendImmunizationDetails = chkHL7Immunization.CheckState
    '        obj.HL7_SendChargesSaveClose = chk_HL7_SendCharges_SaveClose.CheckState
    '        obj.HL7_SendChargesSaveFinish = chk_HL7_SendCharges_SaveFinish.CheckState
    '        obj.Genius_SendChargesSaveClose = chk_Gen_SaveandClose.CheckState
    '        obj.Genius_SendChargesSaveFinish = chk_Gen_SaveandFinish.CheckState
    '        obj.ProductName = "gloEMR"

    '        obj.InsertClientAllMachineInterface()

    '    Catch ex As Exception
    '        MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try

    'End Sub




    Private Sub BindClientInterface(ByVal ProductCode As String, ByVal MachineName As String)

        Try
            'Dim objHL7 As Object = Nothing
            'Dim objGen As Object = Nothing
            'clsSettingsGeneral.GetSetting("HL7SENDOUTBOUNDGLOEMR", objHL7)
            'clsSettingsGeneral.GetSetting("GENIUSSENDOUTBOUNDGLOEMR", objGen)
            'If IsNothing(objHL7) Then
            '    objHL7 = "0"
            'End If
            'If IsNothing(objGen) Then
            '    objGen = "0"
            'End If
            'If (objHL7.ToString() = "1") Then
            '    pnlHl7.Visible = True
            'Else
            '    pnlHl7.Visible = False
            'End If
            'If (objGen.ToString() = "1") Then
            '    pnlGenius.Visible = True
            'Else
            '    pnlGenius.Visible = False
            'End If

            If (obj.ScanClientInterface(ProductCode, MachineName)) Then

                If ((obj.HL7_SendAppointmentDetails = False) Or (obj.Hl7_SendPatientDetails = False) Or (obj.Hl7_SendImmunizationDetails = False) Or (obj.HL7_SendChargesSaveClose = False) Or (obj.HL7_SendChargesSaveFinish = False)) _
                    Or ((obj.HL7_SendAppointmentDetails = False) Or (obj.HL7_SendVisitSumSaveClose = False) Or (obj.HL7_SendVisitSumSaveFinish = False)) Then
                    chk_allhl7.Checked = False
                Else
                    chk_allhl7.Checked = True
                End If
                If ((obj.Genius_SendChargesSaveClose = False) Or (obj.Genius_SendChargesSaveFinish = False)) Then
                    chk_allGen.Checked = False
                Else
                    chk_allGen.Checked = True
                End If



                chkHL7Appointment.Checked = obj.HL7_SendAppointmentDetails
                chkPatientReg.Checked = obj.Hl7_SendPatientDetails
                chkHL7Immunization.Checked = obj.Hl7_SendImmunizationDetails
                chk_HL7_SendCharges_SaveClose.Checked = obj.HL7_SendChargesSaveClose
                chk_HL7_SendCharges_SaveFinish.Checked = obj.HL7_SendChargesSaveFinish
                chk_Gen_SaveandClose.Checked = obj.Genius_SendChargesSaveClose
                chk_Gen_SaveandFinish.Checked = obj.Genius_SendChargesSaveFinish
                chk_HL7_SendVisitSum_SaveClose.Checked = obj.HL7_SendVisitSumSaveClose
                Chk_HL7_SendVisitSum_SaveFinish.Checked = obj.HL7_SendVisitSumSaveFinish
                Try

                    RemoveHandler chkhl7_default.CheckedChanged, AddressOf chkhl7_default_CheckedChanged
                    RemoveHandler chkgenius_default.CheckedChanged, AddressOf chkgenius_default_CheckedChanged
                    chkhl7_default.Checked = obj.HL7_UseDefault
                    chkgenius_default.Checked = obj.Genius_UseDefault
                    If (chkhl7_default.Checked = True) Then
                        chk_allhl7.Enabled = False
                        chk_HL7_SendCharges_SaveClose.Enabled = False
                        chk_HL7_SendCharges_SaveFinish.Enabled = False
                        chkHL7Appointment.Enabled = False
                        chkHL7Immunization.Enabled = False
                        chkPatientReg.Enabled = False
                        chk_HL7_SendVisitSum_SaveClose.Enabled = False
                        Chk_HL7_SendVisitSum_SaveFinish.Enabled = False
                    End If
                    If (chkgenius_default.Checked = True) Then

                        chk_allGen.Enabled = False
                        chk_Gen_SaveandClose.Enabled = False
                        chk_Gen_SaveandFinish.Enabled = False
                    End If
                Catch ex As Exception
                Finally
                    AddHandler chkhl7_default.CheckedChanged, AddressOf chkhl7_default_CheckedChanged
                    AddHandler chkgenius_default.CheckedChanged, AddressOf chkgenius_default_CheckedChanged
                End Try


            End If


        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub



    Private Sub BindDefaulthl7orGeniusSettings(ByVal ProductCode As String, ByVal type As String)

        Try

            obj.ScanClientInterface(ProductCode, "")
            If type = "HL7Outbound" Then
                If ((obj.HL7_SendAppointmentDetails = False) Or (obj.Hl7_SendPatientDetails = False) Or (obj.Hl7_SendImmunizationDetails = False) Or (obj.HL7_SendChargesSaveClose = False) Or (obj.HL7_SendChargesSaveFinish = False) _
                    Or (obj.HL7_SendVisitSumSaveClose = False) Or (obj.HL7_SendVisitSumSaveFinish = False)) Then
                    chk_allhl7.Checked = False
                Else
                    chk_allhl7.Checked = True
                End If

                chkHL7Appointment.Checked = obj.HL7_SendAppointmentDetails
                chkPatientReg.Checked = obj.Hl7_SendPatientDetails
                chkHL7Immunization.Checked = obj.Hl7_SendImmunizationDetails
                chk_HL7_SendCharges_SaveClose.Checked = obj.HL7_SendChargesSaveClose
                chk_HL7_SendCharges_SaveFinish.Checked = obj.HL7_SendChargesSaveFinish
                chk_HL7_SendVisitSum_SaveClose.Checked = obj.HL7_SendVisitSumSaveClose
                Chk_HL7_SendVisitSum_SaveFinish.Checked = obj.HL7_SendVisitSumSaveFinish

            Else
                If ((obj.Genius_SendChargesSaveClose = False) Or (obj.Genius_SendChargesSaveFinish = False)) Then
                    chk_allGen.Checked = False
                Else
                    chk_allGen.Checked = True
                End If
                chk_Gen_SaveandClose.Checked = obj.Genius_SendChargesSaveClose
                chk_Gen_SaveandFinish.Checked = obj.Genius_SendChargesSaveFinish



            End If



        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub


    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        Try
            If txtMachineName.Visible = True Then
                If Trim(txtMachineName.Text) = "" Then
                    MessageBox.Show("Enter client machine name", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    cmbMachineName.Focus()
                    Exit Sub
                End If
            Else
                If Trim(cmbMachineName.Text) = "" Then
                    MessageBox.Show("Select client name", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    cmbMachineName.Focus()
                    Exit Sub
                End If
            End If
            Dim strClientMachineName As String



            If txtMachineName.Visible = True Then
                strClientMachineName = txtMachineName.Text
            Else
                strClientMachineName = cmbMachineName.Text
            End If
            Me.Cursor = Cursors.WaitCursor
            Dim objClientSettings As New clsClientMachines
            'Check User already exists or not
            If blnModify = True Then
                'If objClientSettings.CheckMachineExists(strClientMachineName, cmbMachineName.Tag) = True Then
                If objClientSettings.CheckMachineExists(strClientMachineName, cmbMachineName.Tag) = True Then
                    Me.Cursor = Cursors.Default
                    MessageBox.Show("Client machine name already exists.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    objClientSettings = Nothing
                    If txtMachineName.Visible = True Then
                        txtMachineName.Focus()
                    Else
                        cmbMachineName.Focus()
                    End If
                    Exit Sub
                End If
            Else
                'If objClientSettings.CheckMachineExists(strClientMachineName) = True Then
                If objClientSettings.CheckMachineExists(strClientMachineName) = True Then
                    Me.Cursor = Cursors.Default
                    MessageBox.Show("Client machine name already exists.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    objClientSettings = Nothing
                    If txtMachineName.Visible = True Then
                        txtMachineName.Focus()
                    Else
                        cmbMachineName.Focus()
                    End If
                    Exit Sub
                End If
            End If
            objClientSettings.ClientMachineName = strClientMachineName
            If optVoiceYes.Checked = True Then
                objClientSettings.VoiceEnabled = True
            Else
                objClientSettings.VoiceEnabled = False
            End If
            If optScanYes.Checked = True Then
                objClientSettings.ScanEnabled = True
            Else
                objClientSettings.ScanEnabled = False
            End If



            If blnModify = True Then
                If objClientSettings.UpdateClient(cmbMachineName.Tag) = True Then
                    objClientSettings = Nothing

                    'sarika  21 feb
                    Dim objAudit As New clsAudit
                    objAudit.CreateLog(clsAudit.enmActivityType.Modify, gstrLoginName & " Has modified the settings of client : " & txtMachineName.Text, gstrLoginName, gstrClientMachineName)
                    objAudit = Nothing
                    '-------------
                    Me.DialogResult = DialogResult.OK
                    Me.Close()
                Else
                    MessageBox.Show("Unable to update client settings", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            Else
                If objClientSettings.InsertClient() = True Then
                    objClientSettings = Nothing

                    'sarika  21 feb
                    Dim objAudit As New clsAudit
                    objAudit.CreateLog(clsAudit.enmActivityType.Add, gstrLoginName & " has added new Client Settings.", gstrLoginName, gstrClientMachineName)
                    objAudit = Nothing
                    '-------------
                    Me.DialogResult = DialogResult.OK
                    Me.Close()
                Else
                    Dim objAudit As New clsAudit
                    MessageBox.Show("Unable to add client settings", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    objAudit.CreateLog(clsAudit.enmActivityType.Add, "Error while adding new client settings.", gstrLoginName, gstrClientMachineName, 0, , clsAudit.enmOutcome.Failure)
                    objAudit = Nothing
                End If
            End If
            ClientInterfaceFill(strClientMachineName)
            Me.Cursor = Cursors.Default
        Catch objErr As Exception
            Me.Cursor = Cursors.Default
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Dim objAudit As New clsAudit
            objAudit.CreateLog(clsAudit.enmActivityType.Add, "Error while adding new client ettings.", gstrLoginName, gstrClientMachineName, 0, , clsAudit.enmOutcome.Failure)
            objAudit = Nothing
        End Try
    End Sub

    Private Sub Fill_Machines()
        cmbMachineName.Items.Clear()
        Dim clMachines As New Collection
        Dim objClients As New clsWindowsGroupsUsers
        clMachines = objClients.PopulateMachines
        objClients = Nothing
        Dim nCount As Int16

        For nCount = 1 To clMachines.Count
            cmbMachineName.Items.Add(clMachines.Item(nCount))
        Next

        Dim objClient As New clsClientMachines
        Dim clClients As New Collection
        clClients = objClient.Fill_ClientMachines()
        objClient = Nothing

        For nCount = 1 To clClients.Count
            cmbMachineName.Items.Remove(clClients.Item(nCount))
        Next
    End Sub

    Private Sub btnBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowse.Click
        Try
            txtMachineName.Visible = False
            cmbMachineName.Visible = True
            If cmbMachineName.Items.Count <= 0 Then
                Call Fill_Machines()
            End If
            cmbMachineName.Text = txtMachineName.Text
        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub optVoiceYes_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optVoiceYes.CheckedChanged
        If optVoiceYes.Checked = True Then
            optVoiceYes.Font = New Font("Tahoma", 9, FontStyle.Bold)
        Else
            optVoiceYes.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub optVoiceNo_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optVoiceNo.CheckedChanged
        If optVoiceNo.Checked = True Then
            optVoiceNo.Font = New Font("Tahoma", 9, FontStyle.Bold)
        Else
            optVoiceNo.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub optScanYes_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optScanYes.CheckedChanged
        If optScanYes.Checked = True Then
            optScanYes.Font = New Font("Tahoma", 9, FontStyle.Bold)
        Else
            optScanYes.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub optScanNo_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optScanNo.CheckedChanged
        If optScanNo.Checked = True Then ''Bug #48818 7030
            optScanNo.Font = New Font("Tahoma", 9, FontStyle.Bold)
        Else
            optScanNo.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub


    Private Sub ToolStrip_HL7_Click(sender As System.Object, e As System.EventArgs)
        '  ToolStrip_HL7.CheckState = CheckState.Checked
        '  ToolStrip_Gen.CheckState = CheckState.Unchecked
        pnlHl7.Show()
        'pnlGenius.Hide()
    End Sub

    Private Sub ToolStrip_Gen_Click(sender As System.Object, e As System.EventArgs)
        '  ToolStrip_HL7.CheckState = CheckState.Unchecked
        '  ToolStrip_Gen.CheckState = CheckState.Checked
        ' pnlHl7.Hide()
        pnlGenius.Show()
    End Sub

    Private Sub frmClient_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        '  ToolStrip_Gen.Select()
        ' ToolStrip_Gen.CheckState = CheckState.Checked
        ' ToolStrip_HL7.CheckState = CheckState.Unchecked
        ' pnlHl7.Hide()
        ' pnlGenius.Show()
        ShowHidePanels()
        txtMachineName.Select()
        changeHeightAsPerResolution()
        If blnModify = True Then

            BindClientInterface("gloEMR", txtMachineName.Text)
        Else
            BindClientInterface("gloEMR", "")
            Try
                ''while adding new machine making default to true 
                RemoveHandler chkhl7_default.CheckedChanged, AddressOf chkhl7_default_CheckedChanged
                RemoveHandler chkgenius_default.CheckedChanged, AddressOf chkgenius_default_CheckedChanged
                chkgenius_default.Checked = True
                chkhl7_default.Checked = True
                chk_allhl7.Enabled = False
                chk_HL7_SendCharges_SaveClose.Enabled = False
                chk_HL7_SendCharges_SaveFinish.Enabled = False
                chkHL7Appointment.Enabled = False
                chkHL7Immunization.Enabled = False
                chkPatientReg.Enabled = False
                chk_allGen.Enabled = False
                chk_Gen_SaveandClose.Enabled = False
                chk_Gen_SaveandFinish.Enabled = False
                chk_HL7_SendVisitSum_SaveClose.Enabled = False
                Chk_HL7_SendVisitSum_SaveFinish.Enabled = False

            Catch ex As Exception
            Finally
                AddHandler chkhl7_default.CheckedChanged, AddressOf chkhl7_default_CheckedChanged
                AddHandler chkgenius_default.CheckedChanged, AddressOf chkgenius_default_CheckedChanged


            End Try

        End If
    End Sub
    Private Sub ShowHidePanels()
        Dim objHL7 As Object = Nothing
        Dim objGen As Object = Nothing

        clsSettingsGeneral.GetSetting("HL7SENDOUTBOUNDGLOEMR", objHL7)
        clsSettingsGeneral.GetSetting("GENIUSSENDOUTBOUNDGLOEMR", objGen)
        If IsNothing(objHL7) Then
            objHL7 = "0"
        End If
        If IsNothing(objGen) Then
            objGen = "0"
        End If
        If (objHL7.ToString() = "1") Then
            pnlHl7.Visible = True
        Else
            pnlHl7.Visible = False
            Me.Height = Me.Height - pnlHl7.Height
        End If
        If (objGen.ToString() = "1") Then
            pnlGenius.Visible = True
        Else
            pnlGenius.Visible = False
            Me.Height = Me.Height - pnlGenius.Height
        End If
        'If ((pnlGenius.Visible = False) And (pnlHl7.Visible = False)) Then
        '    ' Me.Height = 178
        'End If
      End Sub

    Private Sub btnSelectAllHL7_Click(sender As System.Object, e As System.EventArgs)
        'If btnSelectAllHL7.Text = "Select All" Then
        '    chk_HL7_SendCharges_SaveClose.Checked = True
        '    chk_HL7_SendCharges_SaveFinish.Checked = True
        '    chkHL7Appointment.Checked = True
        '    chkHL7Immunization.Checked = True
        '    chkPatientReg.Checked = True
        '    btnSelectAllHL7.Text = "Clear All"
        'Else
        '    chk_HL7_SendCharges_SaveClose.Checked = False
        '    chk_HL7_SendCharges_SaveFinish.Checked = False
        '    chkHL7Appointment.Checked = False
        '    chkHL7Immunization.Checked = False
        '    chkPatientReg.Checked = False
        '    btnSelectAllHL7.Text = "Select All"
        'End If
    End Sub

    Private Sub btnSelectAllGen_Click(sender As System.Object, e As System.EventArgs)
        'If btnSelectAllGen.Text = "Select All" Then
        '    chk_Gen_SaveandClose.Checked = True
        '    chk_Gen_SaveandFinish.Checked = True
        '    btnSelectAllGen.Text = "Clear All"
        'Else
        '    chk_Gen_SaveandClose.Checked = False
        '    chk_Gen_SaveandFinish.Checked = False
        '    btnSelectAllGen.Text = "Select All"
        'End If
    End Sub

    Private Sub btnapplyallmc_Click(sender As System.Object, e As System.EventArgs)
        '  AllClientInterfaceFill()
    End Sub

    Private Sub chk_allhl7_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chk_allhl7.CheckedChanged

        chk_HL7_SendCharges_SaveClose.Checked = chk_allhl7.Checked
        chk_HL7_SendCharges_SaveFinish.Checked = chk_allhl7.Checked
        chkHL7Appointment.Checked = chk_allhl7.Checked
        chkHL7Immunization.Checked = chk_allhl7.Checked
        chkPatientReg.Checked = chk_allhl7.Checked

        chk_HL7_SendVisitSum_SaveClose.Checked = chk_allhl7.Checked
        Chk_HL7_SendVisitSum_SaveFinish.Checked = chk_allhl7.Checked

    End Sub

    Private Sub chk_allGen_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chk_allGen.CheckedChanged
        chk_Gen_SaveandClose.Checked = chk_allGen.Checked
        chk_Gen_SaveandFinish.Checked = chk_allGen.Checked

    End Sub

    Private Sub chkhl7_default_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkhl7_default.CheckedChanged
        Try
            RemoveHandler chkhl7_default.CheckedChanged, AddressOf chkhl7_default_CheckedChanged
            Dim txtvis As Boolean = txtMachineName.Visible
            Dim cmdvis As Boolean = cmbMachineName.Visible
            Dim strmacname As String = String.Empty

            If (txtvis = False) Then
                strmacname = cmbMachineName.Text.Trim()
                If (strmacname = "") Then
                    MessageBox.Show("Select client name", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    cmbMachineName.Focus()
                    chkhl7_default.Checked = Not (chkhl7_default.Checked)
                    Exit Sub
                End If
            Else
                strmacname = txtMachineName.Text.Trim()
                If (strmacname = "") Then
                    MessageBox.Show("Enter client machine name", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    txtMachineName.Focus()
                    chkhl7_default.Checked = Not (chkhl7_default.Checked)
                    Exit Sub
                End If

            End If
                If chkhl7_default.Checked = True Then
                    Dim dlg As DialogResult = MessageBox.Show("This will enable default HL7 outbound file generation on  '" & strmacname & "' machine?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information)
                    If (dlg = Windows.Forms.DialogResult.No) Then
                        chkhl7_default.Checked = False
                    Else
                        BindDefaulthl7orGeniusSettings("gloEMR", "HL7Outbound")
                        chk_allhl7.Enabled = False
                        chk_HL7_SendCharges_SaveClose.Enabled = False
                        chk_HL7_SendCharges_SaveFinish.Enabled = False
                        chkHL7Appointment.Enabled = False
                        chkHL7Immunization.Enabled = False
                    chkPatientReg.Enabled = False
                    chk_HL7_SendVisitSum_SaveClose.Enabled = False
                    Chk_HL7_SendVisitSum_SaveFinish.Enabled = False

                    End If

                Else
                    Dim dlg As DialogResult = MessageBox.Show("Are you sure you want to disable default HL7 outbound file generation on '" & strmacname & "' machine?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question)

                    If (dlg = Windows.Forms.DialogResult.No) Then
                        BindDefaulthl7orGeniusSettings("gloEMR", "HL7Outbound")
                        chk_allhl7.Enabled = False
                        chk_HL7_SendCharges_SaveClose.Enabled = False
                        chk_HL7_SendCharges_SaveFinish.Enabled = False
                        chkHL7Appointment.Enabled = False
                        chkHL7Immunization.Enabled = False
                        chkPatientReg.Enabled = False
                    chkhl7_default.Checked = True
                    chk_HL7_SendVisitSum_SaveClose.Enabled = False
                    Chk_HL7_SendVisitSum_SaveFinish.Enabled = False
                    Else

                        chk_allhl7.Enabled = True
                        chk_HL7_SendCharges_SaveClose.Enabled = True
                        chk_HL7_SendCharges_SaveFinish.Enabled = True
                        chkHL7Appointment.Enabled = True
                        chkHL7Immunization.Enabled = True
                    chkPatientReg.Enabled = True
                    chk_HL7_SendVisitSum_SaveClose.Enabled = True
                    Chk_HL7_SendVisitSum_SaveFinish.Enabled = True

                    End If


                End If

        Catch ex As Exception
        Finally
            AddHandler chkhl7_default.CheckedChanged, AddressOf chkhl7_default_CheckedChanged


        End Try
    End Sub

    Private Sub chkgenius_default_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkgenius_default.CheckedChanged
        Try
            RemoveHandler chkgenius_default.CheckedChanged, AddressOf chkgenius_default_CheckedChanged
            Dim txtvis As Boolean = txtMachineName.Visible
            Dim cmdvis As Boolean = cmbMachineName.Visible
            Dim strmacname As String = String.Empty
            'If (txtvis = False) Then
            '    strmacname = cmbMachineName.Text
            'Else
            '    strmacname = txtMachineName.Text
            'End If
            If (txtvis = False) Then
                strmacname = cmbMachineName.Text.Trim()
                If (strmacname = "") Then
                    MessageBox.Show("Select client name", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    cmbMachineName.Focus()
                    chkgenius_default.Checked = Not (chkgenius_default.Checked)
                    Exit Sub
                End If
            Else
                strmacname = txtMachineName.Text.Trim()
                If (strmacname = "") Then
                    MessageBox.Show("Enter client machine name", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    txtMachineName.Focus()
                    chkgenius_default.Checked = Not (chkgenius_default.Checked)
                    Exit Sub
                End If

            End If

   
        If chkgenius_default.Checked = True Then

                Dim dlg As DialogResult = MessageBox.Show("This will enable default Genius Outbound Charges generation on '" & strmacname & "' machine?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information)
                If (dlg = Windows.Forms.DialogResult.No) Then
                    chkgenius_default.Checked = False
                Else

                    BindDefaulthl7orGeniusSettings("gloEMR", "Genius")


                    chk_allGen.Enabled = False
                    chk_Gen_SaveandClose.Enabled = False
                    chk_Gen_SaveandFinish.Enabled = False
                End If
            Else

                Dim dlg As DialogResult = MessageBox.Show("Are you sure you want to disable default Genius Charges generation on '" & strmacname & "' machine?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question)

                If (dlg = Windows.Forms.DialogResult.No) Then
                    BindDefaulthl7orGeniusSettings("gloEMR", "Genius")


                    chk_allGen.Enabled = False
                    chk_Gen_SaveandClose.Enabled = False
                    chk_Gen_SaveandFinish.Enabled = False
                    chkgenius_default.Checked = True
                Else

                    chk_allGen.Enabled = True
                    chk_Gen_SaveandClose.Enabled = True
                    chk_Gen_SaveandFinish.Enabled = True
                End If
            End If
        Catch ex As Exception

        Finally
            AddHandler chkgenius_default.CheckedChanged, AddressOf chkgenius_default_CheckedChanged


        End Try

    End Sub

    Private Sub changeHeightAsPerResolution()

        Try

            Dim myScreenHeight As Int32 = CType(Screen.PrimaryScreen.WorkingArea.Height * 0.99, Int32)
            If myScreenHeight < Me.Height Then
                Me.Height = myScreenHeight
            End If

            'Dim myScreenWidth As Int32 = CType(Screen.PrimaryScreen.WorkingArea.Width * 0.99, Int32)
            'If myScreenHeight > Me.Width Then
            '    Me.Width = myScreenWidth
            'End If

        Catch
            'Blank catch
        End Try

    End Sub

    

   
End Class
