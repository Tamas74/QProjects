Public Class frmPatientInjuryDate
    Inherits System.Windows.Forms.Form

#Region " Windows Form Designer generated code "

    Public Sub New(ByVal PatientID As Long)
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        _nPatientId = PatientID
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
    Friend WithEvents pnlMiddle As System.Windows.Forms.Panel
    Friend WithEvents grpComplaints As System.Windows.Forms.GroupBox
    Friend WithEvents txtChiefComplaints As System.Windows.Forms.TextBox
    Friend WithEvents grpDate As System.Windows.Forms.GroupBox
    'Friend WithEvents mskSurgeryDate As AxMSMask.AxMaskEdBox
    'Friend WithEvents mskInjurydate As AxMSMask.AxMaskEdBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents mskSurgeryDate As System.Windows.Forms.MaskedTextBox
    Friend WithEvents mskInjurydate As System.Windows.Forms.MaskedTextBox
    Private WithEvents pnl_tlspTOP As System.Windows.Forms.Panel
    Private WithEvents tlsp_PatientInjuryDate As gloGlobal.gloToolStripIgnoreFocus
    Private WithEvents ts_btnOk As System.Windows.Forms.ToolStripButton
    Private WithEvents ts_btnCancel As System.Windows.Forms.ToolStripButton
    Private WithEvents lbl_BottomBrd As System.Windows.Forms.Label
    Private WithEvents lbl_LeftBrd As System.Windows.Forms.Label
    Private WithEvents lbl_RightBrd As System.Windows.Forms.Label
    Private WithEvents lbl_TopBrd As System.Windows.Forms.Label
    Friend WithEvents grpEncounterReason As System.Windows.Forms.GroupBox
    Friend WithEvents txtEncounterReason As System.Windows.Forms.TextBox
    Friend WithEvents btnClrCode As System.Windows.Forms.Button
    Friend WithEvents btnConceptID As System.Windows.Forms.Button
    Friend WithEvents optReason As System.Windows.Forms.RadioButton
    Friend WithEvents optICD9_10 As System.Windows.Forms.RadioButton
    Friend WithEvents optSnomed As System.Windows.Forms.RadioButton
    Friend WithEvents Label1 As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPatientInjuryDate))
        Me.pnlMiddle = New System.Windows.Forms.Panel()
        Me.grpEncounterReason = New System.Windows.Forms.GroupBox()
        Me.optReason = New System.Windows.Forms.RadioButton()
        Me.optICD9_10 = New System.Windows.Forms.RadioButton()
        Me.optSnomed = New System.Windows.Forms.RadioButton()
        Me.txtEncounterReason = New System.Windows.Forms.TextBox()
        Me.btnClrCode = New System.Windows.Forms.Button()
        Me.btnConceptID = New System.Windows.Forms.Button()
        Me.grpDate = New System.Windows.Forms.GroupBox()
        Me.mskSurgeryDate = New System.Windows.Forms.MaskedTextBox()
        Me.mskInjurydate = New System.Windows.Forms.MaskedTextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.grpComplaints = New System.Windows.Forms.GroupBox()
        Me.txtChiefComplaints = New System.Windows.Forms.TextBox()
        Me.lbl_BottomBrd = New System.Windows.Forms.Label()
        Me.lbl_LeftBrd = New System.Windows.Forms.Label()
        Me.lbl_RightBrd = New System.Windows.Forms.Label()
        Me.lbl_TopBrd = New System.Windows.Forms.Label()
        Me.pnl_tlspTOP = New System.Windows.Forms.Panel()
        Me.tlsp_PatientInjuryDate = New gloGlobal.gloToolStripIgnoreFocus()
        Me.ts_btnOk = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnCancel = New System.Windows.Forms.ToolStripButton()
        Me.pnlMiddle.SuspendLayout()
        Me.grpEncounterReason.SuspendLayout()
        Me.grpDate.SuspendLayout()
        Me.grpComplaints.SuspendLayout()
        Me.pnl_tlspTOP.SuspendLayout()
        Me.tlsp_PatientInjuryDate.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlMiddle
        '
        Me.pnlMiddle.BackColor = System.Drawing.Color.Transparent
        Me.pnlMiddle.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlMiddle.Controls.Add(Me.grpEncounterReason)
        Me.pnlMiddle.Controls.Add(Me.grpDate)
        Me.pnlMiddle.Controls.Add(Me.grpComplaints)
        Me.pnlMiddle.Controls.Add(Me.lbl_BottomBrd)
        Me.pnlMiddle.Controls.Add(Me.lbl_LeftBrd)
        Me.pnlMiddle.Controls.Add(Me.lbl_RightBrd)
        Me.pnlMiddle.Controls.Add(Me.lbl_TopBrd)
        Me.pnlMiddle.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMiddle.Location = New System.Drawing.Point(0, 54)
        Me.pnlMiddle.Name = "pnlMiddle"
        Me.pnlMiddle.Padding = New System.Windows.Forms.Padding(3)
        Me.pnlMiddle.Size = New System.Drawing.Size(786, 425)
        Me.pnlMiddle.TabIndex = 0
        '
        'grpEncounterReason
        '
        Me.grpEncounterReason.Controls.Add(Me.optReason)
        Me.grpEncounterReason.Controls.Add(Me.optICD9_10)
        Me.grpEncounterReason.Controls.Add(Me.optSnomed)
        Me.grpEncounterReason.Controls.Add(Me.txtEncounterReason)
        Me.grpEncounterReason.Controls.Add(Me.btnClrCode)
        Me.grpEncounterReason.Controls.Add(Me.btnConceptID)
        Me.grpEncounterReason.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpEncounterReason.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.grpEncounterReason.Location = New System.Drawing.Point(11, 13)
        Me.grpEncounterReason.Name = "grpEncounterReason"
        Me.grpEncounterReason.Size = New System.Drawing.Size(760, 102)
        Me.grpEncounterReason.TabIndex = 0
        Me.grpEncounterReason.TabStop = False
        Me.grpEncounterReason.Text = "Reason for Encounter"
        '
        'optReason
        '
        Me.optReason.AutoSize = True
        Me.optReason.Checked = True
        Me.optReason.Location = New System.Drawing.Point(9, 21)
        Me.optReason.Name = "optReason"
        Me.optReason.Size = New System.Drawing.Size(70, 18)
        Me.optReason.TabIndex = 0
        Me.optReason.TabStop = True
        Me.optReason.Text = "Reason"
        Me.optReason.UseVisualStyleBackColor = True
        '
        'optICD9_10
        '
        Me.optICD9_10.AutoSize = True
        Me.optICD9_10.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optICD9_10.Location = New System.Drawing.Point(110, 21)
        Me.optICD9_10.Name = "optICD9_10"
        Me.optICD9_10.Size = New System.Drawing.Size(74, 18)
        Me.optICD9_10.TabIndex = 1
        Me.optICD9_10.Text = "ICD 9/10"
        Me.optICD9_10.UseVisualStyleBackColor = True
        '
        'optSnomed
        '
        Me.optSnomed.AutoSize = True
        Me.optSnomed.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optSnomed.Location = New System.Drawing.Point(215, 21)
        Me.optSnomed.Name = "optSnomed"
        Me.optSnomed.Size = New System.Drawing.Size(89, 18)
        Me.optSnomed.TabIndex = 2
        Me.optSnomed.Text = "Snomed CT"
        Me.optSnomed.UseVisualStyleBackColor = True
        '
        'txtEncounterReason
        '
        Me.txtEncounterReason.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtEncounterReason.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.txtEncounterReason.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEncounterReason.ForeColor = System.Drawing.Color.Black
        Me.txtEncounterReason.Location = New System.Drawing.Point(3, 47)
        Me.txtEncounterReason.MaxLength = 1500
        Me.txtEncounterReason.Multiline = True
        Me.txtEncounterReason.Name = "txtEncounterReason"
        Me.txtEncounterReason.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtEncounterReason.Size = New System.Drawing.Size(754, 52)
        Me.txtEncounterReason.TabIndex = 20
        '
        'btnClrCode
        '
        Me.btnClrCode.BackColor = System.Drawing.Color.Transparent
        Me.btnClrCode.BackgroundImage = CType(resources.GetObject("btnClrCode.BackgroundImage"), System.Drawing.Image)
        Me.btnClrCode.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnClrCode.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnClrCode.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnClrCode.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClrCode.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClrCode.Image = CType(resources.GetObject("btnClrCode.Image"), System.Drawing.Image)
        Me.btnClrCode.Location = New System.Drawing.Point(346, 19)
        Me.btnClrCode.Name = "btnClrCode"
        Me.btnClrCode.Size = New System.Drawing.Size(23, 23)
        Me.btnClrCode.TabIndex = 4
        Me.btnClrCode.UseVisualStyleBackColor = False
        '
        'btnConceptID
        '
        Me.btnConceptID.BackColor = System.Drawing.Color.Transparent
        Me.btnConceptID.BackgroundImage = CType(resources.GetObject("btnConceptID.BackgroundImage"), System.Drawing.Image)
        Me.btnConceptID.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnConceptID.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnConceptID.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnConceptID.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnConceptID.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnConceptID.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnConceptID.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnConceptID.Image = CType(resources.GetObject("btnConceptID.Image"), System.Drawing.Image)
        Me.btnConceptID.Location = New System.Drawing.Point(321, 19)
        Me.btnConceptID.Name = "btnConceptID"
        Me.btnConceptID.Size = New System.Drawing.Size(22, 23)
        Me.btnConceptID.TabIndex = 3
        Me.btnConceptID.UseVisualStyleBackColor = False
        '
        'grpDate
        '
        Me.grpDate.BackColor = System.Drawing.Color.Transparent
        Me.grpDate.Controls.Add(Me.mskSurgeryDate)
        Me.grpDate.Controls.Add(Me.mskInjurydate)
        Me.grpDate.Controls.Add(Me.Label2)
        Me.grpDate.Controls.Add(Me.Label1)
        Me.grpDate.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.grpDate.Location = New System.Drawing.Point(11, 332)
        Me.grpDate.Name = "grpDate"
        Me.grpDate.Size = New System.Drawing.Size(760, 76)
        Me.grpDate.TabIndex = 2
        Me.grpDate.TabStop = False
        Me.grpDate.Text = "Injury/Surgery Date"
        '
        'mskSurgeryDate
        '
        Me.mskSurgeryDate.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.mskSurgeryDate.ForeColor = System.Drawing.Color.Black
        Me.mskSurgeryDate.Location = New System.Drawing.Point(107, 45)
        Me.mskSurgeryDate.Mask = "00/00/0000"
        Me.mskSurgeryDate.Name = "mskSurgeryDate"
        Me.mskSurgeryDate.Size = New System.Drawing.Size(154, 22)
        Me.mskSurgeryDate.TabIndex = 1
        '
        'mskInjurydate
        '
        Me.mskInjurydate.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.mskInjurydate.ForeColor = System.Drawing.Color.Black
        Me.mskInjurydate.Location = New System.Drawing.Point(107, 19)
        Me.mskInjurydate.Mask = "00/00/0000"
        Me.mskInjurydate.Name = "mskInjurydate"
        Me.mskInjurydate.Size = New System.Drawing.Size(154, 22)
        Me.mskInjurydate.TabIndex = 0
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(17, 50)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(87, 14)
        Me.Label2.TabIndex = 31
        Me.Label2.Text = "Surgery Date :"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(28, 23)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(76, 14)
        Me.Label1.TabIndex = 30
        Me.Label1.Text = "Injury Date :"
        '
        'grpComplaints
        '
        Me.grpComplaints.Controls.Add(Me.txtChiefComplaints)
        Me.grpComplaints.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpComplaints.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.grpComplaints.Location = New System.Drawing.Point(11, 124)
        Me.grpComplaints.Name = "grpComplaints"
        Me.grpComplaints.Size = New System.Drawing.Size(760, 199)
        Me.grpComplaints.TabIndex = 1
        Me.grpComplaints.TabStop = False
        Me.grpComplaints.Text = "Chief Complaints"
        '
        'txtChiefComplaints
        '
        Me.txtChiefComplaints.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtChiefComplaints.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtChiefComplaints.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtChiefComplaints.ForeColor = System.Drawing.Color.Black
        Me.txtChiefComplaints.Location = New System.Drawing.Point(3, 18)
        Me.txtChiefComplaints.MaxLength = 1500
        Me.txtChiefComplaints.Multiline = True
        Me.txtChiefComplaints.Name = "txtChiefComplaints"
        Me.txtChiefComplaints.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtChiefComplaints.Size = New System.Drawing.Size(754, 178)
        Me.txtChiefComplaints.TabIndex = 0
        '
        'lbl_BottomBrd
        '
        Me.lbl_BottomBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_BottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_BottomBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_BottomBrd.Location = New System.Drawing.Point(4, 421)
        Me.lbl_BottomBrd.Name = "lbl_BottomBrd"
        Me.lbl_BottomBrd.Size = New System.Drawing.Size(778, 1)
        Me.lbl_BottomBrd.TabIndex = 8
        Me.lbl_BottomBrd.Text = "label2"
        '
        'lbl_LeftBrd
        '
        Me.lbl_LeftBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_LeftBrd.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_LeftBrd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_LeftBrd.Location = New System.Drawing.Point(3, 4)
        Me.lbl_LeftBrd.Name = "lbl_LeftBrd"
        Me.lbl_LeftBrd.Size = New System.Drawing.Size(1, 418)
        Me.lbl_LeftBrd.TabIndex = 7
        Me.lbl_LeftBrd.Text = "label4"
        '
        'lbl_RightBrd
        '
        Me.lbl_RightBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_RightBrd.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_RightBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_RightBrd.Location = New System.Drawing.Point(782, 4)
        Me.lbl_RightBrd.Name = "lbl_RightBrd"
        Me.lbl_RightBrd.Size = New System.Drawing.Size(1, 418)
        Me.lbl_RightBrd.TabIndex = 6
        Me.lbl_RightBrd.Text = "label3"
        '
        'lbl_TopBrd
        '
        Me.lbl_TopBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_TopBrd.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_TopBrd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_TopBrd.Location = New System.Drawing.Point(3, 3)
        Me.lbl_TopBrd.Name = "lbl_TopBrd"
        Me.lbl_TopBrd.Size = New System.Drawing.Size(780, 1)
        Me.lbl_TopBrd.TabIndex = 5
        Me.lbl_TopBrd.Text = "label1"
        '
        'pnl_tlspTOP
        '
        Me.pnl_tlspTOP.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnl_tlspTOP.Controls.Add(Me.tlsp_PatientInjuryDate)
        Me.pnl_tlspTOP.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnl_tlspTOP.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnl_tlspTOP.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.pnl_tlspTOP.Location = New System.Drawing.Point(0, 0)
        Me.pnl_tlspTOP.Name = "pnl_tlspTOP"
        Me.pnl_tlspTOP.Size = New System.Drawing.Size(786, 54)
        Me.pnl_tlspTOP.TabIndex = 1
        '
        'tlsp_PatientInjuryDate
        '
        Me.tlsp_PatientInjuryDate.BackColor = System.Drawing.Color.Transparent
        Me.tlsp_PatientInjuryDate.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tlsp_PatientInjuryDate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlsp_PatientInjuryDate.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlsp_PatientInjuryDate.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tlsp_PatientInjuryDate.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ts_btnOk, Me.ts_btnCancel})
        Me.tlsp_PatientInjuryDate.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.tlsp_PatientInjuryDate.Location = New System.Drawing.Point(0, 0)
        Me.tlsp_PatientInjuryDate.Name = "tlsp_PatientInjuryDate"
        Me.tlsp_PatientInjuryDate.Size = New System.Drawing.Size(786, 53)
        Me.tlsp_PatientInjuryDate.TabIndex = 0
        Me.tlsp_PatientInjuryDate.TabStop = True
        Me.tlsp_PatientInjuryDate.Text = "toolStrip1"
        '
        'ts_btnOk
        '
        Me.ts_btnOk.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnOk.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.ts_btnOk.Image = CType(resources.GetObject("ts_btnOk.Image"), System.Drawing.Image)
        Me.ts_btnOk.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnOk.Name = "ts_btnOk"
        Me.ts_btnOk.Size = New System.Drawing.Size(66, 50)
        Me.ts_btnOk.Tag = "OK"
        Me.ts_btnOk.Text = "&Save&&Cls"
        Me.ts_btnOk.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.ts_btnOk.ToolTipText = "Save and Close"
        '
        'ts_btnCancel
        '
        Me.ts_btnCancel.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnCancel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.ts_btnCancel.Image = CType(resources.GetObject("ts_btnCancel.Image"), System.Drawing.Image)
        Me.ts_btnCancel.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnCancel.Name = "ts_btnCancel"
        Me.ts_btnCancel.Size = New System.Drawing.Size(43, 50)
        Me.ts_btnCancel.Tag = "Cancel"
        Me.ts_btnCancel.Text = "&Close"
        Me.ts_btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'frmPatientInjuryDate
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(786, 479)
        Me.Controls.Add(Me.pnlMiddle)
        Me.Controls.Add(Me.pnl_tlspTOP)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmPatientInjuryDate"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Patient Complaints & Injury/Surgery  Date Details"
        Me.pnlMiddle.ResumeLayout(False)
        Me.grpEncounterReason.ResumeLayout(False)
        Me.grpEncounterReason.PerformLayout()
        Me.grpDate.ResumeLayout(False)
        Me.grpDate.PerformLayout()
        Me.grpComplaints.ResumeLayout(False)
        Me.grpComplaints.PerformLayout()
        Me.pnl_tlspTOP.ResumeLayout(False)
        Me.pnl_tlspTOP.PerformLayout()
        Me.tlsp_PatientInjuryDate.ResumeLayout(False)
        Me.tlsp_PatientInjuryDate.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Dim _nPatientId As Int64
    Dim objclsPatientInjuryDate As ClsPatientInjuryDate
    
    Dim _nExamId As Int64 = 0
    Dim _nVisitId As Int64 = 0
    Dim _nComplaintd As Int64 = 0
    Dim _dtvisitdate As DateTime = Now
    Dim _bIsNewComplaint As Boolean = False
    'variable used to store previous ChiefComplaint
    Dim _PreviousChiefComplaint As String
    Dim OptFontBold As Font = New Font("Tahoma", 9, FontStyle.Bold)
    Dim optFontRegular As Font = New Font("Tahoma", 9, FontStyle.Regular)

    Public Sub New(ByVal PatientId As Int64, ByVal complaintId As Int64, ByVal ExamId As Int64, ByVal dtvisitdate As DateTime, ByVal IsNew As Boolean)
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        _nPatientId = PatientId
        _nComplaintd = complaintId
        _nExamId = ExamId
        _dtvisitdate = dtvisitdate
        _bIsNewComplaint = IsNew
    End Sub
    Private Sub frmPatientInjuryDate_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            GetComplaint()
            'line added by dipak 20091005 to store previous chief complaint text which is used in SaveComplaint function
            _PreviousChiefComplaint = txtChiefComplaints.Text
            
            Try
                gloPatient.gloPatient.GetWindowTitle(Me, _nPatientId, GetConnectionString(), gstrMessageBoxCaption)
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            End Try
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Patient Injury Date Details", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    
    ''to add data to  PatientChiefComplaint table.
    Private Sub SaveComplaint()

        Try
            If txtChiefComplaints.Text.Trim = "" And txtEncounterReason.Text.Trim = "" Then
                MessageBox.Show("Please enter a chief complaint.  ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtChiefComplaints.Focus()
                Exit Sub
            Else
                If mskSurgeryDate.Text <> "" Then
                    If mskSurgeryDate.Text <> "  /  /" Then
                        If IsValidDate(mskSurgeryDate.Text) = True Then
                            If (Convert.ToDateTime(mskSurgeryDate.Text.Trim()) = DateTime.MinValue Or Convert.ToDateTime(mskSurgeryDate.Text.Trim()) < Convert.ToDateTime("01/01/1900")) Then ' Or Convert.ToDateTime(mskSurgeryDate.Text).Date > DateTime.Now.Date 
                                MessageBox.Show("Please enter a valid surgery date.  ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                mskSurgeryDate.Focus()
                                Exit Sub
                            End If
                        Else
                            MessageBox.Show("Please enter a valid surgery date.  ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            mskSurgeryDate.Focus()
                            Exit Sub
                        End If
                    End If
                End If

                If mskInjurydate.Text <> "" Then
                    If mskInjurydate.Text <> "  /  /" Then
                        If IsValidDate(mskInjurydate.Text) = True Then
                            If (Convert.ToDateTime(mskInjurydate.Text.Trim()) = DateTime.MinValue Or Convert.ToDateTime(mskInjurydate.Text.Trim()) < Convert.ToDateTime("01/01/1900")) Then ' Convert.ToDateTime(mskInjurydate.Text).Date > DateTime.Now.Date Or 
                                MessageBox.Show("Please enter a valid injury date.  ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                mskInjurydate.Focus()
                                Exit Sub
                            End If
                        Else
                            MessageBox.Show("Please enter a valid injury date.  ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            mskInjurydate.Focus()
                            Exit Sub
                        End If
                    End If
                End If


                If mskInjurydate.MaskCompleted = False And mskInjurydate.Text <> "  /  /" Then
                    MessageBox.Show("Please enter a valid injury date.  ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    mskInjurydate.Focus()
                    Exit Sub
                End If
                If mskSurgeryDate.MaskCompleted = False And mskSurgeryDate.Text <> "  /  /" Then
                    MessageBox.Show("Please enter a valid surgery date.  ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    mskSurgeryDate.Focus()
                    Exit Sub
                End If


                Dim Arrlist As New ArrayList

                '@nChiefComplaintID @nPatientID, @nVisitID, @nExamID, @dtVisitDate, @sChiefComplaint, @dtInjuryDate, @dtSurgeryDate, @nClinicID
                Arrlist.Add(CType(_nComplaintd, Int64))
                ''generate visit id 
                _nVisitId = GenerateVisitID(_dtvisitdate, _nPatientId)
                Arrlist.Add(CType(_nVisitId, Int64))
                Arrlist.Add(CType(_nExamId, Int64))
                Arrlist.Add(CType(_dtvisitdate, Date))
                Arrlist.Add(txtChiefComplaints.Text)
                If Len(mskInjurydate.Text.Trim) = 4 Then
                    ' If The Surgery Date is Not Entered then Send it Blank
                    Arrlist.Add(Now)
                    Arrlist.Add(True)
                Else
                    Arrlist.Add(CType(mskInjurydate.Text, Date))
                    Arrlist.Add(False)
                End If

                If Len(mskSurgeryDate.Text.Trim) = 4 Then
                    ' If The Surgery Date is Not Entered then Send it Blank
                    Arrlist.Add(Now)
                    Arrlist.Add(True)
                Else
                    Arrlist.Add(CType(mskSurgeryDate.Text, Date))
                    Arrlist.Add(False)
                End If
                If txtEncounterReason.Text.Trim <> "" Then
                    Arrlist.Add(txtEncounterReason.Text)
                    Arrlist.Add(ReasonCodeType)
                Else
                    Arrlist.Add(String.Empty)
                    Arrlist.Add(String.Empty)
                End If

                If Arrlist.Count > 0 Then
                    objclsPatientInjuryDate.TempChiefComplaint = Replace(_PreviousChiefComplaint, "'", "''")
                    objclsPatientInjuryDate.AddChiefComplaints(_nPatientId, Arrlist)

                End If
            End If
            Me.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''Sandip Darade 21 st Feb 09 
    ''to get  data from PatientChiefComplaint table.
    Private Sub GetComplaint()
        Try
            objclsPatientInjuryDate = New ClsPatientInjuryDate(_nPatientId)
            Dim Arrlist As New ArrayList
            ''get visitID
            If (_nComplaintd = 0) Then
                _nVisitId = GenerateVisitID(_dtvisitdate, _nPatientId)
                ''A patinet can have one chief complaint a day 
                Arrlist = objclsPatientInjuryDate.GetComplaints(_nPatientId, _nVisitId, _nExamId)
            Else
                Arrlist = objclsPatientInjuryDate.GetComplainttoEdit(_nComplaintd)
            End If

            If Arrlist.Count > 0 Then

                If Not IsDBNull(Arrlist.Item(0)) Then
                    _nComplaintd = CType(Arrlist.Item(0), Int64)
                End If
                If Not IsDBNull(Arrlist.Item(1)) Then
                    If IsDate(Arrlist.Item(1)) Then
                        _dtvisitdate = Format(CType(Arrlist.Item(1), System.DateTime).Date, "MM/dd/yyyy")
                    End If
                End If
                If Not IsDBNull(Arrlist.Item(2)) Then
                    txtChiefComplaints.Text = CType(Arrlist.Item(2), String)
                End If
                If Not IsDBNull(Arrlist.Item(3)) Then
                    If IsDate(Arrlist.Item(3)) Then
                        mskInjurydate.Text = Format(CType(Arrlist.Item(3), System.DateTime).Date, "MM/dd/yyyy")
                    End If
                End If
                If Not IsDBNull(Arrlist.Item(4)) Then
                    If IsDate(Arrlist.Item(4)) Then
                        mskSurgeryDate.Text = Format(CType(Arrlist.Item(4), System.DateTime).Date, "MM/dd/yyyy")
                    End If
                End If                
                If Not IsDBNull(Arrlist.Item(6)) Then
                    If CType(Arrlist.Item(6), String) <> "" Then
                        If CType(Arrlist.Item(6), String) = "ICD9" Then
                            optICD9_10.Checked = True
                            ReasonCodeType = "ICD9"
                        Else
                            If CType(Arrlist.Item(6), String) = "ICD10" Then
                                optICD9_10.Checked = True
                                ReasonCodeType = "ICD10"
                            Else
                                If CType(Arrlist.Item(6), String) = "Snomed" Then
                                    optSnomed.Checked = True
                                    ReasonCodeType = "Snomed"
                                End If
                            End If
                        End If
                    Else
                        optReason.Checked = True
                        ReasonCodeType = ""
                    End If
                Else
                    optReason.Checked = True
                    ReasonCodeType = ""
                End If

                If Not IsDBNull(Arrlist.Item(5)) Then
                    txtEncounterReason.Text = CType(Arrlist.Item(5), String)
                Else
                    txtEncounterReason.Text = String.Empty
                End If

            End If
            
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Patient Injury Date Details", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            
        End Try
    End Sub
    Private Function IsValidDate(ByVal DOB As String) As Boolean
        Dim year As Int32 = 0
        Dim month As Int32 = 0
        Dim day As Int32 = 0
        'vishal 
        If DOB.Trim().Length <= 4 Then
            ' for blank date,length=4 ,including '/' character...  
            Return True
        End If

        '*****
        Dim _Date As String() = DOB.Split("/"c)
        If _Date.Length = 3 Then
            For i As Integer = 0 To _Date.Length - 1
                If _Date(i).Trim() <> "" Then
                    If i = 0 Then
                        month = Convert.ToInt32(_Date(i))
                    End If
                    If i = 1 Then
                        day = Convert.ToInt32(_Date(i))
                    End If
                    If i = 2 Then

                        If _Date(i).Trim().Replace("_", "").Length = 4 Then
                            year = Convert.ToInt32(_Date(i))
                        Else
                            Return False
                        End If
                    End If
                Else
                    Return False

                End If
            Next

            If month > 12 Then
                Return False
            End If

            If day = 29 Then
                If month = 2 Then
                    If year Mod 4 = 0 Then
                        Return True
                    Else
                        Return False
                    End If
                Else

                    Return True
                End If
            ElseIf day > 31 Then
                Return False
            ElseIf day = 0 Then
                Return False
            ElseIf day = 31 Then
                If month = 2 OrElse month = 4 OrElse month = 6 OrElse month = 9 OrElse month = 11 Then
                    Return False
                Else
                    Return True
                End If
            Else
                Return True

            End If
        Else
            Return False
        End If
    End Function

    Private Sub frmPatientInjuryDate_Closed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Closed
        Try
            'Dim objAudit As New clsAudit
            'objAudit.CreateLog(clsAudit.enmActivityType.Other, "Patient Chief Complaints Closed", gstrLoginName, gstrClientMachineName, _nPatientId)
            'objAudit = Nothing
        Catch ex As Exception

        End Try
    End Sub

    Private Sub tlsp_PatientInjuryDate_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tlsp_PatientInjuryDate.ItemClicked
        Try
            Select Case e.ClickedItem.Tag
                Case "OK"
                    SaveComplaint()
                Case "Cancel"
                    Me.Close()

            End Select

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK)
        End Try
    End Sub

    Private Sub mskInjurydate_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles mskInjurydate.MouseClick
        If mskInjurydate.Text = "  /  /" Then
            mskInjurydate.SelectionStart = 0
            mskInjurydate.SelectionLength = 0
        End If
    End Sub

    Private Sub mskSurgeryDate_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles mskSurgeryDate.MouseClick
        If mskSurgeryDate.Text = "  /  /" Then
            mskSurgeryDate.SelectionStart = 0
            mskSurgeryDate.SelectionLength = 0
        End If
    End Sub

    Private Sub opt_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles optReason.CheckedChanged, optICD9_10.CheckedChanged, optSnomed.CheckedChanged

        ReasonCodeType = ""
        If optICD9_10.Checked Then
            txtEncounterReason.ReadOnly = True
            btnClrCode.Enabled = True
            btnConceptID.Enabled = True
            optICD9_10.Font = OptFontBold
            optSnomed.Font = optFontRegular
            optReason.Font = optFontRegular
        ElseIf optSnomed.Checked Then
            txtEncounterReason.ReadOnly = True
            btnClrCode.Enabled = True
            btnConceptID.Enabled = True
            optSnomed.Font = OptFontBold
            optICD9_10.Font = optFontRegular
            optReason.Font = optFontRegular
        Else
            txtEncounterReason.ReadOnly = False
            btnClrCode.Enabled = False
            btnConceptID.Enabled = False
            optReason.Font = OptFontBold
            optICD9_10.Font = optFontRegular
            optSnomed.Font = optFontRegular
        End If
        txtEncounterReason.Text = ""

    End Sub
    Dim ofrmDiagnosisList As frmViewListControl
    Private oDiagnosisListControl As gloListControl.gloListControl

    Private Sub ICDSelection()
        Try
            ofrmDiagnosisList = New frmViewListControl
            oDiagnosisListControl = New gloListControl.gloListControl(GetConnectionString(), gloListControl.gloListControlType.Diagnosis, False, Me.Width)
            oDiagnosisListControl.ControlHeader = "Diagnosis"
            oDiagnosisListControl.gblnIcd10Transition = gblnIcd10Transition ''If true then ICD10 gets selected 
            AddHandler oDiagnosisListControl.ItemSelectedClick, AddressOf oDiagnosisListControl_ItemSelectedClick
            AddHandler oDiagnosisListControl.ItemClosedClick, AddressOf oDiagnosisListControl_ItemClosedClick
            ofrmDiagnosisList.Controls.Add(oDiagnosisListControl)
            oDiagnosisListControl.Dock = DockStyle.Fill
            oDiagnosisListControl.BringToFront()
            
            oDiagnosisListControl.ShowHeaderPanel(False)
            oDiagnosisListControl.OpenControl()
            ofrmDiagnosisList.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedSingle
            ofrmDiagnosisList.Text = "Encounter Reason"
            ofrmDiagnosisList.ShowDialog(IIf(IsNothing(ofrmDiagnosisList.Parent), Me, ofrmDiagnosisList.Parent))

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
    Private ReasonCodeType As String = ""    
    Private Sub oDiagnosisListControl_ItemSelectedClick(ByVal sender As Object, ByVal e As EventArgs)
        Dim Strdata As String = ""
        Try
            If oDiagnosisListControl.SelectedItems.Count > 0 Then
                For i As Int16 = 0 To oDiagnosisListControl.SelectedItems.Count - 1
                    txtEncounterReason.Text = oDiagnosisListControl.SelectedItems(i).Code & " : " & oDiagnosisListControl.SelectedItems(i).Description
                    If txtChiefComplaints.Text.Trim = "" Then
                        txtChiefComplaints.Text = oDiagnosisListControl.SelectedItems(i).Description
                    End If
                Next
                
                ReasonCodeType = "ICD" + Convert.ToString(oDiagnosisListControl.IsICD9_10)
                ofrmDiagnosisList.Close()
            Else
                txtEncounterReason.Text = ""                
                ofrmDiagnosisList.Close()
            End If
            
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

    Private Sub btnConceptID_Click(sender As System.Object, e As System.EventArgs) Handles btnConceptID.Click
        If optICD9_10.Checked Then
            ICDSelection()
        Else
            If optSnomed.Checked Then
                SnomedSelection()
            End If
        End If

    End Sub
    Private Sub SnomedSelection()
        gstrSMDBConnstr = GetHybridConnectionString(gstrSMDBServerName, gstrSMDBDatabaseName, gblnSMDBAuthen, gstrSMDBUserID, gstrSMDBPassWord)
        Dim frm As New gloSnoMed.FrmSelectProblem("Chief Complaints", gstrSMDBConnstr, GetConnectionString())
        Dim str As String = ""
        Try
            frm.strConceptDesc = ""
            frm.strDescriptionID = ""
            Dim splconceptid As String() = txtEncounterReason.Text.Trim().Split(" : ")
            If (splconceptid.Length > 1) Then
                frm.strConceptID = splconceptid(0)
                frm.txtSMSearch.Text = splconceptid(0)
            Else
                frm.strConceptID = txtEncounterReason.Text.Trim
                frm.txtSMSearch.Text = txtEncounterReason.Text.Trim
            End If
            
            frm.ShowDialog(IIf(IsNothing(frm.Parent), Me, frm.Parent))


            If frm._DialogResult Then

                If IsNothing(frm.strConceptID) = False Then


                    If Convert.ToString(frm.strConceptID).Trim = "0" Then
                        txtEncounterReason.Text = ""
                    Else
                        txtEncounterReason.Text = Convert.ToString(frm.strConceptID) '.ToString()
                    End If
                Else
                    txtEncounterReason.Text = frm.strConceptID
                End If
                If txtEncounterReason.Text.Trim() <> "" Then    ''changes done for 8020 snomed prd
                    If frm.strSelectedDescription.Trim() <> "" Then
                        txtEncounterReason.Text = txtEncounterReason.Text + " : " + frm.strSelectedDescription
                        If txtChiefComplaints.Text.Trim = "" Then
                            txtChiefComplaints.Text = frm.strSelectedDescription
                        End If
                    End If
                End If
                ReasonCodeType = "Snomed"
            End If


        Catch ex As Exception

            frm.Dispose()
        End Try
    End Sub

    Private Sub btnClrCode_Click(sender As System.Object, e As System.EventArgs) Handles btnClrCode.Click
        If txtEncounterReason.Text.Trim <> "" AndAlso Not optReason.Checked Then
            txtEncounterReason.Text = ""
        End If
    End Sub
End Class

