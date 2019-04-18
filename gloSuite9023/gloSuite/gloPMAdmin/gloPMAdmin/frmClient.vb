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
    Friend WithEvents chk_allhl7 As System.Windows.Forms.CheckBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents chkhl7_default As System.Windows.Forms.CheckBox
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
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmbMachineName As System.Windows.Forms.ComboBox
    Friend WithEvents btnBrowse As System.Windows.Forms.Button
    Private WithEvents pnl_tlsp_Top As System.Windows.Forms.Panel
    Friend WithEvents tstrip As System.Windows.Forms.ToolStrip
    Friend WithEvents btnOK As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnCancel As System.Windows.Forms.ToolStripButton
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Private WithEvents Label5 As System.Windows.Forms.Label
    Private WithEvents Label6 As System.Windows.Forms.Label
    Private WithEvents Label7 As System.Windows.Forms.Label
    Private WithEvents Label8 As System.Windows.Forms.Label
    Private WithEvents chkPatientReg As System.Windows.Forms.CheckBox
    Private WithEvents chkHL7Appointment As System.Windows.Forms.CheckBox
    Friend WithEvents pnlTab As System.Windows.Forms.Panel
    Friend WithEvents pnlHL7 As System.Windows.Forms.Panel
    Private WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Private WithEvents Label20 As System.Windows.Forms.Label
    Private WithEvents Label22 As System.Windows.Forms.Label
    Private WithEvents Label3 As System.Windows.Forms.Label
    Private WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtMachineName As System.Windows.Forms.TextBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmClient))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtMachineName = New System.Windows.Forms.TextBox()
        Me.cmbMachineName = New System.Windows.Forms.ComboBox()
        Me.pnl_tlsp_Top = New System.Windows.Forms.Panel()
        Me.tstrip = New System.Windows.Forms.ToolStrip()
        Me.btnOK = New System.Windows.Forms.ToolStripButton()
        Me.btnCancel = New System.Windows.Forms.ToolStripButton()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.btnBrowse = New System.Windows.Forms.Button()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.chkPatientReg = New System.Windows.Forms.CheckBox()
        Me.chkHL7Appointment = New System.Windows.Forms.CheckBox()
        Me.pnlTab = New System.Windows.Forms.Panel()
        Me.pnlHL7 = New System.Windows.Forms.Panel()
        Me.chkhl7_default = New System.Windows.Forms.CheckBox()
        Me.chk_allhl7 = New System.Windows.Forms.CheckBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.pnl_tlsp_Top.SuspendLayout()
        Me.tstrip.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.pnlTab.SuspendLayout()
        Me.pnlHL7.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(13, 14)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(87, 14)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "C&lient Name :"
        '
        'txtMachineName
        '
        Me.txtMachineName.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMachineName.Location = New System.Drawing.Point(103, 10)
        Me.txtMachineName.Name = "txtMachineName"
        Me.txtMachineName.Size = New System.Drawing.Size(265, 22)
        Me.txtMachineName.TabIndex = 1
        '
        'cmbMachineName
        '
        Me.cmbMachineName.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbMachineName.Location = New System.Drawing.Point(109, 10)
        Me.cmbMachineName.Name = "cmbMachineName"
        Me.cmbMachineName.Size = New System.Drawing.Size(192, 22)
        Me.cmbMachineName.TabIndex = 0
        Me.cmbMachineName.Visible = False
        '
        'pnl_tlsp_Top
        '
        Me.pnl_tlsp_Top.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnl_tlsp_Top.Controls.Add(Me.tstrip)
        Me.pnl_tlsp_Top.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnl_tlsp_Top.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnl_tlsp_Top.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.pnl_tlsp_Top.Location = New System.Drawing.Point(0, 0)
        Me.pnl_tlsp_Top.Name = "pnl_tlsp_Top"
        Me.pnl_tlsp_Top.Size = New System.Drawing.Size(408, 56)
        Me.pnl_tlsp_Top.TabIndex = 5
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
        Me.tstrip.Size = New System.Drawing.Size(408, 53)
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
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel2.Controls.Add(Me.Label5)
        Me.Panel2.Controls.Add(Me.Label6)
        Me.Panel2.Controls.Add(Me.Label7)
        Me.Panel2.Controls.Add(Me.btnBrowse)
        Me.Panel2.Controls.Add(Me.Label8)
        Me.Panel2.Controls.Add(Me.txtMachineName)
        Me.Panel2.Controls.Add(Me.cmbMachineName)
        Me.Panel2.Controls.Add(Me.Label1)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel2.Location = New System.Drawing.Point(0, 56)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Padding = New System.Windows.Forms.Padding(3)
        Me.Panel2.Size = New System.Drawing.Size(408, 42)
        Me.Panel2.TabIndex = 1
        Me.Panel2.TabStop = True
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label5.Location = New System.Drawing.Point(4, 38)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(400, 1)
        Me.Label5.TabIndex = 8
        Me.Label5.Text = "label2"
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(3, 4)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(1, 35)
        Me.Label6.TabIndex = 7
        Me.Label6.Text = "label4"
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label7.Location = New System.Drawing.Point(404, 4)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(1, 35)
        Me.Label7.TabIndex = 6
        Me.Label7.Text = "label3"
        '
        'btnBrowse
        '
        Me.btnBrowse.BackColor = System.Drawing.Color.Transparent
        Me.btnBrowse.BackgroundImage = Global.gloPMAdmin.My.Resources.Resources.Img_Button
        Me.btnBrowse.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnBrowse.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.btnBrowse.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.btnBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBrowse.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBrowse.Image = CType(resources.GetObject("btnBrowse.Image"), System.Drawing.Image)
        Me.btnBrowse.Location = New System.Drawing.Point(373, 10)
        Me.btnBrowse.Name = "btnBrowse"
        Me.btnBrowse.Size = New System.Drawing.Size(22, 22)
        Me.btnBrowse.TabIndex = 2
        Me.btnBrowse.UseVisualStyleBackColor = False
        Me.btnBrowse.Visible = False
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(3, 3)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(402, 1)
        Me.Label8.TabIndex = 5
        Me.Label8.Text = "label1"
        '
        'chkPatientReg
        '
        Me.chkPatientReg.AutoSize = True
        Me.chkPatientReg.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkPatientReg.Location = New System.Drawing.Point(48, 37)
        Me.chkPatientReg.Name = "chkPatientReg"
        Me.chkPatientReg.Size = New System.Drawing.Size(136, 18)
        Me.chkPatientReg.TabIndex = 1
        Me.chkPatientReg.Text = "Send Patient Details"
        Me.chkPatientReg.UseVisualStyleBackColor = True
        '
        'chkHL7Appointment
        '
        Me.chkHL7Appointment.AutoSize = True
        Me.chkHL7Appointment.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkHL7Appointment.Location = New System.Drawing.Point(48, 63)
        Me.chkHL7Appointment.Name = "chkHL7Appointment"
        Me.chkHL7Appointment.Size = New System.Drawing.Size(169, 18)
        Me.chkHL7Appointment.TabIndex = 2
        Me.chkHL7Appointment.Text = "Send Appointment Details"
        Me.chkHL7Appointment.UseVisualStyleBackColor = True
        '
        'pnlTab
        '
        Me.pnlTab.Controls.Add(Me.pnlHL7)
        Me.pnlTab.Controls.Add(Me.Label2)
        Me.pnlTab.Controls.Add(Me.Panel3)
        Me.pnlTab.Controls.Add(Me.Label22)
        Me.pnlTab.Controls.Add(Me.Label3)
        Me.pnlTab.Controls.Add(Me.Label4)
        Me.pnlTab.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlTab.Location = New System.Drawing.Point(0, 98)
        Me.pnlTab.Name = "pnlTab"
        Me.pnlTab.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.pnlTab.Size = New System.Drawing.Size(408, 120)
        Me.pnlTab.TabIndex = 2
        Me.pnlTab.TabStop = True
        '
        'pnlHL7
        '
        Me.pnlHL7.Controls.Add(Me.chkhl7_default)
        Me.pnlHL7.Controls.Add(Me.chk_allhl7)
        Me.pnlHL7.Controls.Add(Me.chkHL7Appointment)
        Me.pnlHL7.Controls.Add(Me.chkPatientReg)
        Me.pnlHL7.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlHL7.Location = New System.Drawing.Point(4, 25)
        Me.pnlHL7.Name = "pnlHL7"
        Me.pnlHL7.Size = New System.Drawing.Size(400, 91)
        Me.pnlHL7.TabIndex = 1
        Me.pnlHL7.TabStop = True
        '
        'chkhl7_default
        '
        Me.chkhl7_default.AutoSize = True
        Me.chkhl7_default.Location = New System.Drawing.Point(254, 13)
        Me.chkhl7_default.Name = "chkhl7_default"
        Me.chkhl7_default.Size = New System.Drawing.Size(89, 18)
        Me.chkhl7_default.TabIndex = 4
        Me.chkhl7_default.Text = "Use Default"
        Me.chkhl7_default.UseVisualStyleBackColor = True
        '
        'chk_allhl7
        '
        Me.chk_allhl7.AutoSize = True
        Me.chk_allhl7.Location = New System.Drawing.Point(48, 13)
        Me.chk_allhl7.Name = "chk_allhl7"
        Me.chk_allhl7.Size = New System.Drawing.Size(74, 18)
        Me.chk_allhl7.TabIndex = 3
        Me.chk_allhl7.Text = "All Event"
        Me.chk_allhl7.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label2.Location = New System.Drawing.Point(4, 116)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(400, 1)
        Me.Label2.TabIndex = 17
        Me.Label2.Text = "label2"
        '
        'Panel3
        '
        Me.Panel3.BackgroundImage = Global.gloPMAdmin.My.Resources.Resources.Img_Button
        Me.Panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel3.Controls.Add(Me.Label9)
        Me.Panel3.Controls.Add(Me.Label20)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(4, 1)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(400, 24)
        Me.Panel3.TabIndex = 5
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(0, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(400, 23)
        Me.Label9.TabIndex = 12
        Me.Label9.Text = "  HL7 Outbound Settings :"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label20
        '
        Me.Label20.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label20.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label20.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.Location = New System.Drawing.Point(0, 23)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(400, 1)
        Me.Label20.TabIndex = 11
        Me.Label20.Text = "label1"
        '
        'Label22
        '
        Me.Label22.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label22.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label22.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.Location = New System.Drawing.Point(4, 0)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(400, 1)
        Me.Label22.TabIndex = 14
        Me.Label22.Text = "label1"
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(3, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(1, 117)
        Me.Label3.TabIndex = 18
        Me.Label3.Text = "label4"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label4.Location = New System.Drawing.Point(404, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(1, 117)
        Me.Label4.TabIndex = 19
        Me.Label4.Text = "label3"
        '
        'frmClient
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(408, 218)
        Me.Controls.Add(Me.pnlTab)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.pnl_tlsp_Top)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
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
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.pnlTab.ResumeLayout(False)
        Me.pnlHL7.ResumeLayout(False)
        Me.pnlHL7.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.ResumeLayout(False)

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

    Private Sub ClientInterfaceFill()
        Try
            obj.HL7_SendAppointmentDetails = chkHL7Appointment.CheckState
            obj.Hl7_SendPatientDetails = chkPatientReg.CheckState
            obj.ProductName = "gloPM"
            obj.MachineName = txtMachineName.Text
            obj.HL7_UseDefault = chkhl7_default.Checked
            obj.InsertClientInterface()

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub BindClientInterface(ByVal ProductCode As String, ByVal MachineName As String)
        Try
            Dim objglohl7 As Object = Nothing
            clsSettingsGeneral.GetSetting("HL7SENDOUTBOUNDGLOPM", objglohl7)

            If Not IsNothing(objglohl7) AndAlso
                    Convert.ToString(objglohl7) = "1" Then
                pnlTab.Visible = True
            Else
                pnlTab.Visible = False
            End If


            If (obj.ScanClientInterface(ProductCode, MachineName)) Then

                 If ((obj.HL7_SendAppointmentDetails = False) Or (obj.Hl7_SendPatientDetails = False)) Then
                    chk_allhl7.Checked = False
                Else
                    chk_allhl7.Checked = True
                End If
                chkHL7Appointment.Checked = obj.HL7_SendAppointmentDetails
                chkPatientReg.Checked = obj.Hl7_SendPatientDetails

                Try
                    RemoveHandler chkhl7_default.CheckedChanged, AddressOf chkhl7_default_CheckedChanged

                    chkhl7_default.Checked = obj.HL7_UseDefault
                    If (chkhl7_default.Checked = True) Then
                        chkHL7Appointment.Enabled = False
                        chkPatientReg.Enabled = False

                        chk_allhl7.Enabled = False
                    End If
                Catch ex As Exception
                Finally

                    AddHandler chkhl7_default.CheckedChanged, AddressOf chkhl7_default_CheckedChanged

                End Try

            End If
           

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub


    Private Sub BindClientInterfaceForHL7(ByVal ProductCode As String)
        Try
            Dim objglohl7 As Object = Nothing


            If (obj.ScanClientInterface(ProductCode, "")) Then
                If ((obj.HL7_SendAppointmentDetails = False) Or (obj.Hl7_SendPatientDetails = False)) Then
                    chk_allhl7.Checked = False
                Else
                    chk_allhl7.Checked = True
                End If
                chkHL7Appointment.Checked = obj.HL7_SendAppointmentDetails
                chkPatientReg.Checked = obj.Hl7_SendPatientDetails
                

            End If
          

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub


    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        Try
            If txtMachineName.Visible = True Then
                If Trim(txtMachineName.Text) = "" Then
                    MessageBox.Show("Enter client machine name", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    cmbMachineName.Focus()
                    Exit Sub
                End If
            Else
                If Trim(cmbMachineName.Text) = "" Then
                    MessageBox.Show("Select client name", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
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
                    MessageBox.Show("Client machine name already exists.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
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
                    MessageBox.Show("Client machine name already exists.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
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
            ClientInterfaceFill()
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



    Private Sub frmClient_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

        If blnModify = True Then
            BindClientInterface("gloPM", txtMachineName.Text)
            Dim bl As Boolean = cmbMachineName.Visible
        Else
            HidePanels()
            Try

                RemoveHandler chkhl7_default.CheckedChanged, AddressOf chkhl7_default_CheckedChanged
                BindClientInterfaceForHL7("gloPM")
                chkHL7Appointment.Enabled = False
                chkPatientReg.Enabled = False
                chk_allhl7.Enabled = False
                chkhl7_default.Checked = True

            Catch ex As Exception
            Finally
                AddHandler chkhl7_default.CheckedChanged, AddressOf chkhl7_default_CheckedChanged

            End Try
        
        End If
        If pnlTab.Visible = False Then
            Me.Height = 126
        End If
    End Sub

    Private Sub HidePanels()
        Try
            Dim objglohl7 As Object = Nothing
            clsSettingsGeneral.GetSetting("HL7SENDOUTBOUNDGLOPM", objglohl7)
            If Not IsNothing(objglohl7) AndAlso
                Convert.ToString(objglohl7) = "1" Then
            Else
                pnlTab.Visible = False
            End If
        Catch ex As Exception

        End Try
  

    End Sub
    'Private Sub btnSelectAllHL7_Click(sender As System.Object, e As System.EventArgs) Handles btnSelectAllHL7.Click
    '    'If btnSelectAllHL7.Text = "Select All" Then
    '    '    chkHL7Appointment.Checked = True
    '    '    chkPatientReg.Checked = True
    '    '    btnSelectAllHL7.Text = "Clear All"
    '    'Else
    '    '    chkHL7Appointment.Checked = False
    '    '    chkPatientReg.Checked = False
    '    '    btnSelectAllHL7.Text = "Select All"
    '    'End If
    'End Sub

    Private Sub chk_allhl7_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chk_allhl7.CheckedChanged
        chkHL7Appointment.Checked = chk_allhl7.Checked
        chkPatientReg.Checked = chk_allhl7.Checked
    End Sub

    Private Sub chkhl7_default_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkhl7_default.CheckedChanged

        Try
            RemoveHandler chkhl7_default.CheckedChanged, AddressOf chkhl7_default_CheckedChanged
            Dim strmacname As String = String.Empty
            If txtMachineName.Visible = True Then
                If Trim(txtMachineName.Text) = "" Then
                    MessageBox.Show("Enter client machine name", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    txtMachineName.Focus()
                    chkhl7_default.Checked = Not (chkhl7_default.Checked)
                    Exit Sub
                End If
                strmacname = txtMachineName.Text
            Else
                If Trim(cmbMachineName.Text) = "" Then
                    MessageBox.Show("Select client name", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    cmbMachineName.Focus()
                    chkhl7_default.Checked = Not (chkhl7_default.Checked)
                    Exit Sub
                End If
                strmacname = cmbMachineName.Text
            End If



                If chkhl7_default.Checked = True Then
                Dim dlg As DialogResult = MessageBox.Show("This will enable default HL7 outbound file generation on '" & strmacname & "' client machine?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information)
                If (dlg = Windows.Forms.DialogResult.No) Then
                    chkhl7_default.Checked = False
                    chkHL7Appointment.Enabled = True
                    chkPatientReg.Enabled = True

                    chk_allhl7.Enabled = True
                Else
                  
                    chkHL7Appointment.Enabled = False
                    chkPatientReg.Enabled = False
                    BindClientInterfaceForHL7("gloPM")
                    chkhl7_default.Checked = True

                    chk_allhl7.Enabled = False

                End If
            Else
                Dim dlg As DialogResult = MessageBox.Show("Are you sure you want to disable default HL7 outbound file generation on '" & strmacname & "' client machine?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question)

                If (dlg = Windows.Forms.DialogResult.No) Then
                    BindClientInterfaceForHL7("gloPM")
                    chkhl7_default.Checked = True
                    chkHL7Appointment.Enabled = False
                    chkPatientReg.Enabled = False

                    chk_allhl7.Enabled = False
                Else

                    chkHL7Appointment.Enabled = True
                    chkPatientReg.Enabled = True
                    chk_allhl7.Enabled = True
                End If
            End If

        Catch ex As Exception
        Finally
            AddHandler chkhl7_default.CheckedChanged, AddressOf chkhl7_default_CheckedChanged


        End Try

    End Sub
End Class
