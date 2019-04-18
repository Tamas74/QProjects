'***************************************************************************
' Module Name :- gloEMR Admin Login
' Company Name :- gloStream Inc.
' Written By :- Pankaj Naval
' Description :-
'This form is to validate the User Name and Password
'Processes
'   1) 
'***************************************************************************
Imports System.Data.SqlClient
Imports System.io

Public Class frmSettings
    Inherits System.Windows.Forms.Form

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
    Friend WithEvents grpClinic As System.Windows.Forms.GroupBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents tmStartTime As System.Windows.Forms.DateTimePicker
    Friend WithEvents tmEndTime As System.Windows.Forms.DateTimePicker
    Friend WithEvents AppointmentInterval As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents PullChartsInterval As System.Windows.Forms.NumericUpDown
    Friend WithEvents numMaxNoOfRetries As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents numFAXRetryInterval As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents pnlHPI As System.Windows.Forms.Panel
    Friend WithEvents optHPIYes As System.Windows.Forms.RadioButton
    Friend WithEvents optHPINo As System.Windows.Forms.RadioButton
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents optLocationAddressedNo As System.Windows.Forms.RadioButton
    Friend WithEvents optLocationAddressedYes As System.Windows.Forms.RadioButton
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents cmbFAXCompression As System.Windows.Forms.ComboBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents cmbSpeakerVolume As System.Windows.Forms.ComboBox
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents optFAXreceiveNo As System.Windows.Forms.RadioButton
    Friend WithEvents optFAXreceiveYes As System.Windows.Forms.RadioButton
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents pnlPwd As System.Windows.Forms.Panel
    Friend WithEvents optPwdComplexNo As System.Windows.Forms.RadioButton
    Friend WithEvents optPwdComplexYes As System.Windows.Forms.RadioButton
    Friend WithEvents btnSetPwdComplexity As System.Windows.Forms.Button
    Friend WithEvents lblPwdComplexity As System.Windows.Forms.Label
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents cmbCategoryDirective As System.Windows.Forms.ComboBox
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents cmbOMRCategoryPatientRegistration As System.Windows.Forms.ComboBox
    Friend WithEvents cmbOMRCategoryROS As System.Windows.Forms.ComboBox
    Friend WithEvents cmbOMRCategoryHistory As System.Windows.Forms.ComboBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents btnBrowseRxReportPath As System.Windows.Forms.Button
    Friend WithEvents FolderBrowserDialog1 As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents txtRxReportPath As System.Windows.Forms.TextBox
    Friend WithEvents txtNoOfAttempts As System.Windows.Forms.TextBox
    Friend WithEvents pnlDI As System.Windows.Forms.Panel
    Friend WithEvents optClinicDINo As System.Windows.Forms.RadioButton
    Friend WithEvents optClinicDIYes As System.Windows.Forms.RadioButton
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents cmbLabCategory As System.Windows.Forms.ComboBox
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents chkRecordLocking As System.Windows.Forms.CheckBox
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents txtThresholdValue As System.Windows.Forms.TextBox
    Friend WithEvents btnHL7FilePath As System.Windows.Forms.Button
    Friend WithEvents txtHL7FilePath As System.Windows.Forms.TextBox
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents cmbFaxCategory As System.Windows.Forms.ComboBox
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents grpVersionInfo As System.Windows.Forms.GroupBox
    Friend WithEvents txtAppVersion As System.Windows.Forms.TextBox
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents txtDBVersion As System.Windows.Forms.TextBox
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents grpFaxTaskSettings As System.Windows.Forms.GroupBox
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents cmbRecieveFaxUser As System.Windows.Forms.ComboBox
    Friend WithEvents cmbPendingFaxUser As System.Windows.Forms.ComboBox
    Friend WithEvents AxSigPlus1 As AxSIGPLUSLib.AxSigPlus
    Friend WithEvents lblLockOutAttempts As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSettings))
        Me.grpClinic = New System.Windows.Forms.GroupBox
        Me.txtThresholdValue = New System.Windows.Forms.TextBox
        Me.Label23 = New System.Windows.Forms.Label
        Me.Label22 = New System.Windows.Forms.Label
        Me.chkRecordLocking = New System.Windows.Forms.CheckBox
        Me.pnlDI = New System.Windows.Forms.Panel
        Me.optClinicDINo = New System.Windows.Forms.RadioButton
        Me.optClinicDIYes = New System.Windows.Forms.RadioButton
        Me.Label20 = New System.Windows.Forms.Label
        Me.lblLockOutAttempts = New System.Windows.Forms.Label
        Me.txtNoOfAttempts = New System.Windows.Forms.TextBox
        Me.btnSetPwdComplexity = New System.Windows.Forms.Button
        Me.pnlPwd = New System.Windows.Forms.Panel
        Me.optPwdComplexNo = New System.Windows.Forms.RadioButton
        Me.optPwdComplexYes = New System.Windows.Forms.RadioButton
        Me.lblPwdComplexity = New System.Windows.Forms.Label
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.optFAXreceiveNo = New System.Windows.Forms.RadioButton
        Me.optFAXreceiveYes = New System.Windows.Forms.RadioButton
        Me.Label14 = New System.Windows.Forms.Label
        Me.cmbSpeakerVolume = New System.Windows.Forms.ComboBox
        Me.Label13 = New System.Windows.Forms.Label
        Me.cmbFAXCompression = New System.Windows.Forms.ComboBox
        Me.Label12 = New System.Windows.Forms.Label
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.optLocationAddressedNo = New System.Windows.Forms.RadioButton
        Me.optLocationAddressedYes = New System.Windows.Forms.RadioButton
        Me.pnlHPI = New System.Windows.Forms.Panel
        Me.optHPINo = New System.Windows.Forms.RadioButton
        Me.optHPIYes = New System.Windows.Forms.RadioButton
        Me.Label11 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.numFAXRetryInterval = New System.Windows.Forms.NumericUpDown
        Me.Label8 = New System.Windows.Forms.Label
        Me.numMaxNoOfRetries = New System.Windows.Forms.NumericUpDown
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.PullChartsInterval = New System.Windows.Forms.NumericUpDown
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.AppointmentInterval = New System.Windows.Forms.NumericUpDown
        Me.tmEndTime = New System.Windows.Forms.DateTimePicker
        Me.tmStartTime = New System.Windows.Forms.DateTimePicker
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.cmbFaxCategory = New System.Windows.Forms.ComboBox
        Me.Label25 = New System.Windows.Forms.Label
        Me.cmbLabCategory = New System.Windows.Forms.ComboBox
        Me.Label21 = New System.Windows.Forms.Label
        Me.cmbCategoryDirective = New System.Windows.Forms.ComboBox
        Me.Label19 = New System.Windows.Forms.Label
        Me.cmbOMRCategoryPatientRegistration = New System.Windows.Forms.ComboBox
        Me.cmbOMRCategoryROS = New System.Windows.Forms.ComboBox
        Me.cmbOMRCategoryHistory = New System.Windows.Forms.ComboBox
        Me.Label15 = New System.Windows.Forms.Label
        Me.Label16 = New System.Windows.Forms.Label
        Me.Label17 = New System.Windows.Forms.Label
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.btnHL7FilePath = New System.Windows.Forms.Button
        Me.txtHL7FilePath = New System.Windows.Forms.TextBox
        Me.Label24 = New System.Windows.Forms.Label
        Me.btnBrowseRxReportPath = New System.Windows.Forms.Button
        Me.txtRxReportPath = New System.Windows.Forms.TextBox
        Me.Label18 = New System.Windows.Forms.Label
        Me.btnCancel = New System.Windows.Forms.Button
        Me.btnOK = New System.Windows.Forms.Button
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.grpVersionInfo = New System.Windows.Forms.GroupBox
        Me.txtAppVersion = New System.Windows.Forms.TextBox
        Me.Label27 = New System.Windows.Forms.Label
        Me.txtDBVersion = New System.Windows.Forms.TextBox
        Me.Label26 = New System.Windows.Forms.Label
        Me.grpFaxTaskSettings = New System.Windows.Forms.GroupBox
        Me.cmbRecieveFaxUser = New System.Windows.Forms.ComboBox
        Me.cmbPendingFaxUser = New System.Windows.Forms.ComboBox
        Me.Label28 = New System.Windows.Forms.Label
        Me.Label29 = New System.Windows.Forms.Label
        Me.AxSigPlus1 = New AxSIGPLUSLib.AxSigPlus
        Me.grpClinic.SuspendLayout()
        Me.pnlDI.SuspendLayout()
        Me.pnlPwd.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.pnlHPI.SuspendLayout()
        CType(Me.numFAXRetryInterval, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.numMaxNoOfRetries, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PullChartsInterval, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.AppointmentInterval, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.grpVersionInfo.SuspendLayout()
        Me.grpFaxTaskSettings.SuspendLayout()
        CType(Me.AxSigPlus1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'grpClinic
        '
        Me.grpClinic.BackColor = System.Drawing.Color.Transparent
        Me.grpClinic.Controls.Add(Me.AxSigPlus1)
        Me.grpClinic.Controls.Add(Me.txtThresholdValue)
        Me.grpClinic.Controls.Add(Me.Label23)
        Me.grpClinic.Controls.Add(Me.Label22)
        Me.grpClinic.Controls.Add(Me.chkRecordLocking)
        Me.grpClinic.Controls.Add(Me.pnlDI)
        Me.grpClinic.Controls.Add(Me.Label20)
        Me.grpClinic.Controls.Add(Me.lblLockOutAttempts)
        Me.grpClinic.Controls.Add(Me.txtNoOfAttempts)
        Me.grpClinic.Controls.Add(Me.btnSetPwdComplexity)
        Me.grpClinic.Controls.Add(Me.pnlPwd)
        Me.grpClinic.Controls.Add(Me.lblPwdComplexity)
        Me.grpClinic.Controls.Add(Me.Panel2)
        Me.grpClinic.Controls.Add(Me.Label14)
        Me.grpClinic.Controls.Add(Me.cmbSpeakerVolume)
        Me.grpClinic.Controls.Add(Me.Label13)
        Me.grpClinic.Controls.Add(Me.cmbFAXCompression)
        Me.grpClinic.Controls.Add(Me.Label12)
        Me.grpClinic.Controls.Add(Me.Panel1)
        Me.grpClinic.Controls.Add(Me.pnlHPI)
        Me.grpClinic.Controls.Add(Me.Label11)
        Me.grpClinic.Controls.Add(Me.Label10)
        Me.grpClinic.Controls.Add(Me.Label9)
        Me.grpClinic.Controls.Add(Me.numFAXRetryInterval)
        Me.grpClinic.Controls.Add(Me.Label8)
        Me.grpClinic.Controls.Add(Me.numMaxNoOfRetries)
        Me.grpClinic.Controls.Add(Me.Label7)
        Me.grpClinic.Controls.Add(Me.Label5)
        Me.grpClinic.Controls.Add(Me.PullChartsInterval)
        Me.grpClinic.Controls.Add(Me.Label6)
        Me.grpClinic.Controls.Add(Me.Label3)
        Me.grpClinic.Controls.Add(Me.AppointmentInterval)
        Me.grpClinic.Controls.Add(Me.tmEndTime)
        Me.grpClinic.Controls.Add(Me.tmStartTime)
        Me.grpClinic.Controls.Add(Me.Label4)
        Me.grpClinic.Controls.Add(Me.Label2)
        Me.grpClinic.Controls.Add(Me.Label1)
        Me.grpClinic.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.grpClinic.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpClinic.ForeColor = System.Drawing.Color.Black
        Me.grpClinic.Location = New System.Drawing.Point(0, 3)
        Me.grpClinic.Name = "grpClinic"
        Me.grpClinic.Size = New System.Drawing.Size(689, 298)
        Me.grpClinic.TabIndex = 0
        Me.grpClinic.TabStop = False
        Me.grpClinic.Text = "Clinic Settings"
        '
        'txtThresholdValue
        '
        Me.txtThresholdValue.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtThresholdValue.Location = New System.Drawing.Point(531, 202)
        Me.txtThresholdValue.MaxLength = 15
        Me.txtThresholdValue.Name = "txtThresholdValue"
        Me.txtThresholdValue.Size = New System.Drawing.Size(87, 22)
        Me.txtThresholdValue.TabIndex = 39
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.Location = New System.Drawing.Point(619, 204)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(70, 14)
        Me.Label23.TabIndex = 38
        Me.Label23.Text = "(in mints.)"
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.Location = New System.Drawing.Point(398, 209)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(129, 14)
        Me.Label22.TabIndex = 36
        Me.Label22.Text = "   Threshold Value :"
        '
        'chkRecordLocking
        '
        Me.chkRecordLocking.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkRecordLocking.Location = New System.Drawing.Point(28, 268)
        Me.chkRecordLocking.Name = "chkRecordLocking"
        Me.chkRecordLocking.Size = New System.Drawing.Size(161, 24)
        Me.chkRecordLocking.TabIndex = 35
        Me.chkRecordLocking.Text = "Record Level Locking"
        Me.chkRecordLocking.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chkRecordLocking.UseVisualStyleBackColor = True
        '
        'pnlDI
        '
        Me.pnlDI.Controls.Add(Me.optClinicDINo)
        Me.pnlDI.Controls.Add(Me.optClinicDIYes)
        Me.pnlDI.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlDI.Location = New System.Drawing.Point(531, 170)
        Me.pnlDI.Name = "pnlDI"
        Me.pnlDI.Size = New System.Drawing.Size(108, 28)
        Me.pnlDI.TabIndex = 34
        '
        'optClinicDINo
        '
        Me.optClinicDINo.Checked = True
        Me.optClinicDINo.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optClinicDINo.Location = New System.Drawing.Point(62, 5)
        Me.optClinicDINo.Name = "optClinicDINo"
        Me.optClinicDINo.Size = New System.Drawing.Size(42, 19)
        Me.optClinicDINo.TabIndex = 1
        Me.optClinicDINo.TabStop = True
        Me.optClinicDINo.Text = "No"
        '
        'optClinicDIYes
        '
        Me.optClinicDIYes.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optClinicDIYes.Location = New System.Drawing.Point(4, 5)
        Me.optClinicDIYes.Name = "optClinicDIYes"
        Me.optClinicDIYes.Size = New System.Drawing.Size(52, 17)
        Me.optClinicDIYes.TabIndex = 0
        Me.optClinicDIYes.Text = "Yes"
        '
        'Label20
        '
        Me.Label20.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.Location = New System.Drawing.Point(371, 172)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(154, 22)
        Me.Label20.TabIndex = 33
        Me.Label20.Text = "Clinic DI Settings :"
        Me.Label20.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblLockOutAttempts
        '
        Me.lblLockOutAttempts.AutoSize = True
        Me.lblLockOutAttempts.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLockOutAttempts.Location = New System.Drawing.Point(25, 205)
        Me.lblLockOutAttempts.Name = "lblLockOutAttempts"
        Me.lblLockOutAttempts.Size = New System.Drawing.Size(132, 14)
        Me.lblLockOutAttempts.TabIndex = 32
        Me.lblLockOutAttempts.Text = "LockOut  Attempts :"
        '
        'txtNoOfAttempts
        '
        Me.txtNoOfAttempts.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNoOfAttempts.Location = New System.Drawing.Point(158, 201)
        Me.txtNoOfAttempts.Name = "txtNoOfAttempts"
        Me.txtNoOfAttempts.Size = New System.Drawing.Size(122, 22)
        Me.txtNoOfAttempts.TabIndex = 31
        '
        'btnSetPwdComplexity
        '
        Me.btnSetPwdComplexity.BackgroundImage = CType(resources.GetObject("btnSetPwdComplexity.BackgroundImage"), System.Drawing.Image)
        Me.btnSetPwdComplexity.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnSetPwdComplexity.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.btnSetPwdComplexity.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.btnSetPwdComplexity.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSetPwdComplexity.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSetPwdComplexity.Location = New System.Drawing.Point(269, 230)
        Me.btnSetPwdComplexity.Name = "btnSetPwdComplexity"
        Me.btnSetPwdComplexity.Size = New System.Drawing.Size(178, 24)
        Me.btnSetPwdComplexity.TabIndex = 27
        Me.btnSetPwdComplexity.Text = "Set Password Complexity"
        '
        'pnlPwd
        '
        Me.pnlPwd.Controls.Add(Me.optPwdComplexNo)
        Me.pnlPwd.Controls.Add(Me.optPwdComplexYes)
        Me.pnlPwd.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlPwd.Location = New System.Drawing.Point(159, 230)
        Me.pnlPwd.Name = "pnlPwd"
        Me.pnlPwd.Size = New System.Drawing.Size(108, 25)
        Me.pnlPwd.TabIndex = 26
        '
        'optPwdComplexNo
        '
        Me.optPwdComplexNo.Checked = True
        Me.optPwdComplexNo.Location = New System.Drawing.Point(62, 2)
        Me.optPwdComplexNo.Name = "optPwdComplexNo"
        Me.optPwdComplexNo.Size = New System.Drawing.Size(42, 20)
        Me.optPwdComplexNo.TabIndex = 2
        Me.optPwdComplexNo.TabStop = True
        Me.optPwdComplexNo.Text = "No"
        '
        'optPwdComplexYes
        '
        Me.optPwdComplexYes.Location = New System.Drawing.Point(4, 2)
        Me.optPwdComplexYes.Name = "optPwdComplexYes"
        Me.optPwdComplexYes.Size = New System.Drawing.Size(52, 20)
        Me.optPwdComplexYes.TabIndex = 1
        Me.optPwdComplexYes.Text = "Yes"
        '
        'lblPwdComplexity
        '
        Me.lblPwdComplexity.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPwdComplexity.Location = New System.Drawing.Point(6, 235)
        Me.lblPwdComplexity.Name = "lblPwdComplexity"
        Me.lblPwdComplexity.Size = New System.Drawing.Size(154, 20)
        Me.lblPwdComplexity.TabIndex = 25
        Me.lblPwdComplexity.Text = "Password Complexity :"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.optFAXreceiveNo)
        Me.Panel2.Controls.Add(Me.optFAXreceiveYes)
        Me.Panel2.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel2.Location = New System.Drawing.Point(159, 133)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(120, 28)
        Me.Panel2.TabIndex = 23
        '
        'optFAXreceiveNo
        '
        Me.optFAXreceiveNo.Checked = True
        Me.optFAXreceiveNo.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optFAXreceiveNo.Location = New System.Drawing.Point(62, 8)
        Me.optFAXreceiveNo.Name = "optFAXreceiveNo"
        Me.optFAXreceiveNo.Size = New System.Drawing.Size(52, 17)
        Me.optFAXreceiveNo.TabIndex = 1
        Me.optFAXreceiveNo.TabStop = True
        Me.optFAXreceiveNo.Text = "No"
        '
        'optFAXreceiveYes
        '
        Me.optFAXreceiveYes.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optFAXreceiveYes.Location = New System.Drawing.Point(4, 8)
        Me.optFAXreceiveYes.Name = "optFAXreceiveYes"
        Me.optFAXreceiveYes.Size = New System.Drawing.Size(50, 17)
        Me.optFAXreceiveYes.TabIndex = 0
        Me.optFAXreceiveYes.Text = "Yes"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(65, 140)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(91, 14)
        Me.Label14.TabIndex = 22
        Me.Label14.Text = "FAX Receive :"
        '
        'cmbSpeakerVolume
        '
        Me.cmbSpeakerVolume.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSpeakerVolume.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbSpeakerVolume.Location = New System.Drawing.Point(531, 105)
        Me.cmbSpeakerVolume.Name = "cmbSpeakerVolume"
        Me.cmbSpeakerVolume.Size = New System.Drawing.Size(144, 22)
        Me.cmbSpeakerVolume.TabIndex = 21
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(375, 109)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(150, 14)
        Me.Label13.TabIndex = 20
        Me.Label13.Text = "FAX- Speaker Volume :"
        '
        'cmbFAXCompression
        '
        Me.cmbFAXCompression.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbFAXCompression.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbFAXCompression.Location = New System.Drawing.Point(159, 105)
        Me.cmbFAXCompression.Name = "cmbFAXCompression"
        Me.cmbFAXCompression.Size = New System.Drawing.Size(144, 22)
        Me.cmbFAXCompression.TabIndex = 19
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(30, 109)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(125, 14)
        Me.Label12.TabIndex = 18
        Me.Label12.Text = "FAX Compression :"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.optLocationAddressedNo)
        Me.Panel1.Controls.Add(Me.optLocationAddressedYes)
        Me.Panel1.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel1.Location = New System.Drawing.Point(532, 136)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(107, 28)
        Me.Panel1.TabIndex = 17
        '
        'optLocationAddressedNo
        '
        Me.optLocationAddressedNo.Checked = True
        Me.optLocationAddressedNo.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optLocationAddressedNo.Location = New System.Drawing.Point(61, 6)
        Me.optLocationAddressedNo.Name = "optLocationAddressedNo"
        Me.optLocationAddressedNo.Size = New System.Drawing.Size(42, 16)
        Me.optLocationAddressedNo.TabIndex = 1
        Me.optLocationAddressedNo.TabStop = True
        Me.optLocationAddressedNo.Text = "No"
        '
        'optLocationAddressedYes
        '
        Me.optLocationAddressedYes.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optLocationAddressedYes.Location = New System.Drawing.Point(4, 6)
        Me.optLocationAddressedYes.Name = "optLocationAddressedYes"
        Me.optLocationAddressedYes.Size = New System.Drawing.Size(52, 17)
        Me.optLocationAddressedYes.TabIndex = 0
        Me.optLocationAddressedYes.Text = "Yes"
        '
        'pnlHPI
        '
        Me.pnlHPI.Controls.Add(Me.optHPINo)
        Me.pnlHPI.Controls.Add(Me.optHPIYes)
        Me.pnlHPI.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlHPI.Location = New System.Drawing.Point(158, 167)
        Me.pnlHPI.Name = "pnlHPI"
        Me.pnlHPI.Size = New System.Drawing.Size(122, 28)
        Me.pnlHPI.TabIndex = 16
        '
        'optHPINo
        '
        Me.optHPINo.Checked = True
        Me.optHPINo.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optHPINo.Location = New System.Drawing.Point(62, 8)
        Me.optHPINo.Name = "optHPINo"
        Me.optHPINo.Size = New System.Drawing.Size(54, 18)
        Me.optHPINo.TabIndex = 1
        Me.optHPINo.TabStop = True
        Me.optHPINo.Text = "No"
        '
        'optHPIYes
        '
        Me.optHPIYes.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optHPIYes.Location = New System.Drawing.Point(4, 8)
        Me.optHPIYes.Name = "optHPIYes"
        Me.optHPIYes.Size = New System.Drawing.Size(48, 17)
        Me.optHPIYes.TabIndex = 0
        Me.optHPIYes.Text = "Yes"
        '
        'Label11
        '
        Me.Label11.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(301, 139)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(224, 22)
        Me.Label11.TabIndex = 15
        Me.Label11.Text = "Pull Location Address in Patient :"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(118, 174)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(38, 14)
        Me.Label10.TabIndex = 14
        Me.Label10.Text = "HPI :"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(588, 81)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(70, 14)
        Me.Label9.TabIndex = 13
        Me.Label9.Text = "(in mints.)"
        '
        'numFAXRetryInterval
        '
        Me.numFAXRetryInterval.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.numFAXRetryInterval.Location = New System.Drawing.Point(531, 77)
        Me.numFAXRetryInterval.Maximum = New Decimal(New Integer() {500, 0, 0, 0})
        Me.numFAXRetryInterval.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.numFAXRetryInterval.Name = "numFAXRetryInterval"
        Me.numFAXRetryInterval.Size = New System.Drawing.Size(52, 22)
        Me.numFAXRetryInterval.TabIndex = 12
        Me.numFAXRetryInterval.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(396, 81)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(129, 14)
        Me.Label8.TabIndex = 11
        Me.Label8.Text = "FAX Retry Interval :"
        '
        'numMaxNoOfRetries
        '
        Me.numMaxNoOfRetries.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.numMaxNoOfRetries.Location = New System.Drawing.Point(159, 77)
        Me.numMaxNoOfRetries.Maximum = New Decimal(New Integer() {500, 0, 0, 0})
        Me.numMaxNoOfRetries.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.numMaxNoOfRetries.Name = "numMaxNoOfRetries"
        Me.numMaxNoOfRetries.Size = New System.Drawing.Size(52, 22)
        Me.numMaxNoOfRetries.TabIndex = 10
        Me.numMaxNoOfRetries.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(26, 77)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(129, 14)
        Me.Label7.TabIndex = 9
        Me.Label7.Text = "FAX - Max. Retries :"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(214, 47)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(70, 14)
        Me.Label5.TabIndex = 8
        Me.Label5.Text = "(in mints.)"
        '
        'PullChartsInterval
        '
        Me.PullChartsInterval.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PullChartsInterval.Increment = New Decimal(New Integer() {5, 0, 0, 0})
        Me.PullChartsInterval.Location = New System.Drawing.Point(159, 45)
        Me.PullChartsInterval.Maximum = New Decimal(New Integer() {1440, 0, 0, 0})
        Me.PullChartsInterval.Minimum = New Decimal(New Integer() {5, 0, 0, 0})
        Me.PullChartsInterval.Name = "PullChartsInterval"
        Me.PullChartsInterval.Size = New System.Drawing.Size(52, 22)
        Me.PullChartsInterval.TabIndex = 7
        Me.PullChartsInterval.Value = New Decimal(New Integer() {5, 0, 0, 0})
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(2, 47)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(153, 14)
        Me.Label6.TabIndex = 6
        Me.Label6.Text = "PULL CHARTS Interval :"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(589, 50)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(70, 14)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "(in mints.)"
        '
        'AppointmentInterval
        '
        Me.AppointmentInterval.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AppointmentInterval.Increment = New Decimal(New Integer() {5, 0, 0, 0})
        Me.AppointmentInterval.Location = New System.Drawing.Point(531, 47)
        Me.AppointmentInterval.Maximum = New Decimal(New Integer() {1440, 0, 0, 0})
        Me.AppointmentInterval.Minimum = New Decimal(New Integer() {5, 0, 0, 0})
        Me.AppointmentInterval.Name = "AppointmentInterval"
        Me.AppointmentInterval.Size = New System.Drawing.Size(52, 22)
        Me.AppointmentInterval.TabIndex = 4
        Me.AppointmentInterval.Value = New Decimal(New Integer() {5, 0, 0, 0})
        '
        'tmEndTime
        '
        Me.tmEndTime.CalendarForeColor = System.Drawing.Color.Maroon
        Me.tmEndTime.CalendarMonthBackground = System.Drawing.Color.White
        Me.tmEndTime.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.tmEndTime.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.tmEndTime.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.tmEndTime.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tmEndTime.Format = System.Windows.Forms.DateTimePickerFormat.Time
        Me.tmEndTime.Location = New System.Drawing.Point(531, 22)
        Me.tmEndTime.Name = "tmEndTime"
        Me.tmEndTime.ShowUpDown = True
        Me.tmEndTime.Size = New System.Drawing.Size(108, 22)
        Me.tmEndTime.TabIndex = 2
        Me.tmEndTime.Value = New Date(2007, 9, 17, 17, 0, 0, 0)
        '
        'tmStartTime
        '
        Me.tmStartTime.CalendarForeColor = System.Drawing.Color.Maroon
        Me.tmStartTime.CalendarMonthBackground = System.Drawing.Color.White
        Me.tmStartTime.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.tmStartTime.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.tmStartTime.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.tmStartTime.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tmStartTime.Format = System.Windows.Forms.DateTimePickerFormat.Time
        Me.tmStartTime.Location = New System.Drawing.Point(158, 19)
        Me.tmStartTime.Name = "tmStartTime"
        Me.tmStartTime.ShowUpDown = True
        Me.tmStartTime.Size = New System.Drawing.Size(118, 22)
        Me.tmStartTime.TabIndex = 0
        Me.tmStartTime.Value = New Date(2007, 9, 17, 8, 0, 0, 0)
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(376, 51)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(149, 14)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "Appointment Interval :"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(430, 26)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(95, 14)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Closing Time :"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(74, 22)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(80, 14)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Start Time :"
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox1.Controls.Add(Me.cmbFaxCategory)
        Me.GroupBox1.Controls.Add(Me.Label25)
        Me.GroupBox1.Controls.Add(Me.cmbLabCategory)
        Me.GroupBox1.Controls.Add(Me.Label21)
        Me.GroupBox1.Controls.Add(Me.cmbCategoryDirective)
        Me.GroupBox1.Controls.Add(Me.Label19)
        Me.GroupBox1.Controls.Add(Me.cmbOMRCategoryPatientRegistration)
        Me.GroupBox1.Controls.Add(Me.cmbOMRCategoryROS)
        Me.GroupBox1.Controls.Add(Me.cmbOMRCategoryHistory)
        Me.GroupBox1.Controls.Add(Me.Label15)
        Me.GroupBox1.Controls.Add(Me.Label16)
        Me.GroupBox1.Controls.Add(Me.Label17)
        Me.GroupBox1.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.ForeColor = System.Drawing.Color.Black
        Me.GroupBox1.Location = New System.Drawing.Point(0, 307)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(689, 118)
        Me.GroupBox1.TabIndex = 30
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "OMR Category (Sent to DMS)"
        '
        'cmbFaxCategory
        '
        Me.cmbFaxCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbFaxCategory.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbFaxCategory.Location = New System.Drawing.Point(500, 83)
        Me.cmbFaxCategory.Name = "cmbFaxCategory"
        Me.cmbFaxCategory.Size = New System.Drawing.Size(177, 22)
        Me.cmbFaxCategory.TabIndex = 31
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.Location = New System.Drawing.Point(401, 86)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(96, 14)
        Me.Label25.TabIndex = 30
        Me.Label25.Text = "Fax Category:"
        Me.Label25.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmbLabCategory
        '
        Me.cmbLabCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbLabCategory.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbLabCategory.Location = New System.Drawing.Point(499, 55)
        Me.cmbLabCategory.Name = "cmbLabCategory"
        Me.cmbLabCategory.Size = New System.Drawing.Size(178, 22)
        Me.cmbLabCategory.TabIndex = 29
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.Location = New System.Drawing.Point(402, 62)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(97, 14)
        Me.Label21.TabIndex = 28
        Me.Label21.Text = "Lab Category:"
        Me.Label21.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmbCategoryDirective
        '
        Me.cmbCategoryDirective.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCategoryDirective.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbCategoryDirective.Location = New System.Drawing.Point(201, 80)
        Me.cmbCategoryDirective.Name = "cmbCategoryDirective"
        Me.cmbCategoryDirective.Size = New System.Drawing.Size(188, 22)
        Me.cmbCategoryDirective.TabIndex = 27
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.Location = New System.Drawing.Point(9, 83)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(189, 14)
        Me.Label19.TabIndex = 26
        Me.Label19.Text = "Advance Directive Category :"
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmbOMRCategoryPatientRegistration
        '
        Me.cmbOMRCategoryPatientRegistration.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbOMRCategoryPatientRegistration.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbOMRCategoryPatientRegistration.Location = New System.Drawing.Point(201, 54)
        Me.cmbOMRCategoryPatientRegistration.Name = "cmbOMRCategoryPatientRegistration"
        Me.cmbOMRCategoryPatientRegistration.Size = New System.Drawing.Size(188, 22)
        Me.cmbOMRCategoryPatientRegistration.TabIndex = 25
        '
        'cmbOMRCategoryROS
        '
        Me.cmbOMRCategoryROS.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbOMRCategoryROS.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbOMRCategoryROS.Location = New System.Drawing.Point(499, 27)
        Me.cmbOMRCategoryROS.Name = "cmbOMRCategoryROS"
        Me.cmbOMRCategoryROS.Size = New System.Drawing.Size(178, 22)
        Me.cmbOMRCategoryROS.TabIndex = 24
        '
        'cmbOMRCategoryHistory
        '
        Me.cmbOMRCategoryHistory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbOMRCategoryHistory.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbOMRCategoryHistory.Location = New System.Drawing.Point(199, 27)
        Me.cmbOMRCategoryHistory.Name = "cmbOMRCategoryHistory"
        Me.cmbOMRCategoryHistory.Size = New System.Drawing.Size(190, 22)
        Me.cmbOMRCategoryHistory.TabIndex = 23
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(6, 59)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(141, 14)
        Me.Label15.TabIndex = 22
        Me.Label15.Text = "Patient Registration :"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(402, 31)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(42, 14)
        Me.Label16.TabIndex = 21
        Me.Label16.Text = "ROS :"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.Location = New System.Drawing.Point(10, 30)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(60, 14)
        Me.Label17.TabIndex = 20
        Me.Label17.Text = "History :"
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'GroupBox2
        '
        Me.GroupBox2.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox2.Controls.Add(Me.btnHL7FilePath)
        Me.GroupBox2.Controls.Add(Me.txtHL7FilePath)
        Me.GroupBox2.Controls.Add(Me.Label24)
        Me.GroupBox2.Controls.Add(Me.btnBrowseRxReportPath)
        Me.GroupBox2.Controls.Add(Me.txtRxReportPath)
        Me.GroupBox2.Controls.Add(Me.Label18)
        Me.GroupBox2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GroupBox2.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.ForeColor = System.Drawing.Color.Black
        Me.GroupBox2.Location = New System.Drawing.Point(0, 493)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(688, 90)
        Me.GroupBox2.TabIndex = 29
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Server Path Settings"
        '
        'btnHL7FilePath
        '
        Me.btnHL7FilePath.BackgroundImage = CType(resources.GetObject("btnHL7FilePath.BackgroundImage"), System.Drawing.Image)
        Me.btnHL7FilePath.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnHL7FilePath.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnHL7FilePath.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnHL7FilePath.Location = New System.Drawing.Point(562, 56)
        Me.btnHL7FilePath.Name = "btnHL7FilePath"
        Me.btnHL7FilePath.Size = New System.Drawing.Size(29, 25)
        Me.btnHL7FilePath.TabIndex = 32
        Me.btnHL7FilePath.Text = "..."
        '
        'txtHL7FilePath
        '
        Me.txtHL7FilePath.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtHL7FilePath.Location = New System.Drawing.Point(201, 57)
        Me.txtHL7FilePath.Name = "txtHL7FilePath"
        Me.txtHL7FilePath.Size = New System.Drawing.Size(355, 22)
        Me.txtHL7FilePath.TabIndex = 31
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.Location = New System.Drawing.Point(71, 61)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(123, 14)
        Me.Label24.TabIndex = 30
        Me.Label24.Text = "HL7 System Path :"
        '
        'btnBrowseRxReportPath
        '
        Me.btnBrowseRxReportPath.BackgroundImage = CType(resources.GetObject("btnBrowseRxReportPath.BackgroundImage"), System.Drawing.Image)
        Me.btnBrowseRxReportPath.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnBrowseRxReportPath.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBrowseRxReportPath.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBrowseRxReportPath.Location = New System.Drawing.Point(562, 23)
        Me.btnBrowseRxReportPath.Name = "btnBrowseRxReportPath"
        Me.btnBrowseRxReportPath.Size = New System.Drawing.Size(29, 25)
        Me.btnBrowseRxReportPath.TabIndex = 29
        Me.btnBrowseRxReportPath.Text = "..."
        '
        'txtRxReportPath
        '
        Me.txtRxReportPath.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRxReportPath.Location = New System.Drawing.Point(201, 24)
        Me.txtRxReportPath.Name = "txtRxReportPath"
        Me.txtRxReportPath.Size = New System.Drawing.Size(355, 22)
        Me.txtRxReportPath.TabIndex = 28
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(85, 28)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(110, 14)
        Me.Label18.TabIndex = 0
        Me.Label18.Text = "Rx Report Path :"
        '
        'btnCancel
        '
        Me.btnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnCancel.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCancel.Location = New System.Drawing.Point(623, 0)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(64, 23)
        Me.btnCancel.TabIndex = 1
        Me.btnCancel.Text = "&Cancel"
        '
        'btnOK
        '
        Me.btnOK.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnOK.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnOK.Location = New System.Drawing.Point(559, 0)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(64, 23)
        Me.btnOK.TabIndex = 0
        Me.btnOK.Text = "&OK"
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Transparent
        Me.Panel3.BackgroundImage = CType(resources.GetObject("Panel3.BackgroundImage"), System.Drawing.Image)
        Me.Panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel3.Controls.Add(Me.btnOK)
        Me.Panel3.Controls.Add(Me.btnCancel)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel3.Location = New System.Drawing.Point(0, 679)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(689, 25)
        Me.Panel3.TabIndex = 31
        '
        'grpVersionInfo
        '
        Me.grpVersionInfo.BackColor = System.Drawing.Color.Transparent
        Me.grpVersionInfo.Controls.Add(Me.txtAppVersion)
        Me.grpVersionInfo.Controls.Add(Me.Label27)
        Me.grpVersionInfo.Controls.Add(Me.txtDBVersion)
        Me.grpVersionInfo.Controls.Add(Me.Label26)
        Me.grpVersionInfo.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.grpVersionInfo.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpVersionInfo.ForeColor = System.Drawing.Color.Black
        Me.grpVersionInfo.Location = New System.Drawing.Point(0, 432)
        Me.grpVersionInfo.Name = "grpVersionInfo"
        Me.grpVersionInfo.Size = New System.Drawing.Size(689, 56)
        Me.grpVersionInfo.TabIndex = 32
        Me.grpVersionInfo.TabStop = False
        Me.grpVersionInfo.Text = "Version Information"
        '
        'txtAppVersion
        '
        Me.txtAppVersion.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAppVersion.Location = New System.Drawing.Point(203, 21)
        Me.txtAppVersion.Name = "txtAppVersion"
        Me.txtAppVersion.ReadOnly = True
        Me.txtAppVersion.Size = New System.Drawing.Size(190, 22)
        Me.txtAppVersion.TabIndex = 32
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label27.Location = New System.Drawing.Point(58, 29)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(135, 14)
        Me.Label27.TabIndex = 31
        Me.Label27.Text = "Application Version :"
        '
        'txtDBVersion
        '
        Me.txtDBVersion.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDBVersion.Location = New System.Drawing.Point(500, 21)
        Me.txtDBVersion.Name = "txtDBVersion"
        Me.txtDBVersion.ReadOnly = True
        Me.txtDBVersion.Size = New System.Drawing.Size(179, 22)
        Me.txtDBVersion.TabIndex = 30
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.Location = New System.Drawing.Point(410, 24)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(84, 14)
        Me.Label26.TabIndex = 29
        Me.Label26.Text = "DB Version :"
        '
        'grpFaxTaskSettings
        '
        Me.grpFaxTaskSettings.BackColor = System.Drawing.Color.Transparent
        Me.grpFaxTaskSettings.Controls.Add(Me.cmbRecieveFaxUser)
        Me.grpFaxTaskSettings.Controls.Add(Me.cmbPendingFaxUser)
        Me.grpFaxTaskSettings.Controls.Add(Me.Label28)
        Me.grpFaxTaskSettings.Controls.Add(Me.Label29)
        Me.grpFaxTaskSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.grpFaxTaskSettings.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpFaxTaskSettings.ForeColor = System.Drawing.Color.Black
        Me.grpFaxTaskSettings.Location = New System.Drawing.Point(0, 589)
        Me.grpFaxTaskSettings.Name = "grpFaxTaskSettings"
        Me.grpFaxTaskSettings.Size = New System.Drawing.Size(688, 90)
        Me.grpFaxTaskSettings.TabIndex = 33
        Me.grpFaxTaskSettings.TabStop = False
        Me.grpFaxTaskSettings.Text = "Fax Tasks User Settings"
        '
        'cmbRecieveFaxUser
        '
        Me.cmbRecieveFaxUser.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbRecieveFaxUser.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbRecieveFaxUser.Location = New System.Drawing.Point(199, 53)
        Me.cmbRecieveFaxUser.Name = "cmbRecieveFaxUser"
        Me.cmbRecieveFaxUser.Size = New System.Drawing.Size(392, 22)
        Me.cmbRecieveFaxUser.TabIndex = 34
        '
        'cmbPendingFaxUser
        '
        Me.cmbPendingFaxUser.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbPendingFaxUser.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbPendingFaxUser.Location = New System.Drawing.Point(199, 20)
        Me.cmbPendingFaxUser.Name = "cmbPendingFaxUser"
        Me.cmbPendingFaxUser.Size = New System.Drawing.Size(392, 22)
        Me.cmbPendingFaxUser.TabIndex = 33
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label28.Location = New System.Drawing.Point(71, 59)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(123, 14)
        Me.Label28.TabIndex = 30
        Me.Label28.Text = "Recieve Fax User :"
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label29.Location = New System.Drawing.Point(68, 27)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(126, 14)
        Me.Label29.TabIndex = 0
        Me.Label29.Text = "Pending Fax User :"
        '
        'AxSigPlus1
        '
        Me.AxSigPlus1.Enabled = True
        Me.AxSigPlus1.Location = New System.Drawing.Point(485, 245)
        Me.AxSigPlus1.Name = "AxSigPlus1"
        Me.AxSigPlus1.OcxState = CType(resources.GetObject("AxSigPlus1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxSigPlus1.Size = New System.Drawing.Size(137, 23)
        Me.AxSigPlus1.TabIndex = 40
        '
        'frmSettings
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(7, 15)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(227, Byte), Integer), CType(CType(254, Byte), Integer))
        Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(689, 704)
        Me.Controls.Add(Me.grpFaxTaskSettings)
        Me.Controls.Add(Me.grpVersionInfo)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.grpClinic)
        Me.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmSettings"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Settings"
        Me.grpClinic.ResumeLayout(False)
        Me.grpClinic.PerformLayout()
        Me.pnlDI.ResumeLayout(False)
        Me.pnlPwd.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.pnlHPI.ResumeLayout(False)
        CType(Me.numFAXRetryInterval, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.numMaxNoOfRetries, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PullChartsInterval, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.AppointmentInterval, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.grpVersionInfo.ResumeLayout(False)
        Me.grpVersionInfo.PerformLayout()
        Me.grpFaxTaskSettings.ResumeLayout(False)
        Me.grpFaxTaskSettings.PerformLayout()
        CType(Me.AxSigPlus1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region


#Region "variables"


    Dim optcnt As Integer = 0
    Dim blnadminflag As Boolean

    'local variables

    Private m_numofCapletters As Integer = 0
    Private m_numofLetters As Integer = 0
    Private m_numofspecialchars As Integer = 0
    Private m_numminlength As Integer = 0
    Private m_numofdays As Integer = 0
    Private m_numofdigits As Integer = 0

    'sarika 31st aug 07
    ''''' next 2 arrays are used for any modification on form and Close button click 
    Dim _arrayGetData As ArrayList
    Dim _arraySetData As ArrayList
    Public bModifyData As Boolean = False

    '----------------

    'sarika 26th june 07
    '    Public _sqlstrsettings As String = ""
    Public sqlstrsettings As String = ""
    '---
    Dim m_strSQL As String = ""
    Dim m_blnSetComplexisityOnSetting As Boolean = False
#End Region


#Region "properties"


    'properties
    Public Property NoofDigits() As Integer
        Get
            Return m_numofdigits
        End Get
        Set(ByVal Value As Integer)
            m_numofdigits = Value
        End Set
    End Property

    Public Property NoofCapitalLetters() As Integer
        Get
            Return m_numofCapletters
        End Get
        Set(ByVal Value As Integer)
            m_numofCapletters = Value
        End Set
    End Property

    Public Property NoofLetters() As Integer
        Get
            Return m_numofLetters
        End Get
        Set(ByVal Value As Integer)
            m_numofLetters = Value
        End Set
    End Property

    Public Property NumMinimumLength() As Integer
        Get
            Return m_numminlength
        End Get
        Set(ByVal Value As Integer)
            m_numminlength = Value
        End Set
    End Property

    Public Property NoofDays() As Integer
        Get
            Return m_numofdays
        End Get
        Set(ByVal Value As Integer)
            m_numofdays = Value
        End Set
    End Property

    Public Property NoOfSpecialChars() As Integer
        Get
            Return m_numofspecialchars
        End Get
        Set(ByVal Value As Integer)
            m_numofspecialchars = Value
        End Set
    End Property

    Public Property strSQL() As String
        Get
            Return m_strSQL
        End Get
        Set(ByVal Value As String)
            m_strSQL = Value
        End Set
    End Property
#End Region

    Private Sub frmSettings_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try

            optcnt = 0

            blnadminflag = GetAdminFlag()

            If blnadminflag = True Then
                pnlPwd.Visible = True
                lblPwdComplexity.Visible = True
                btnSetPwdComplexity.Visible = True
                txtNoOfAttempts.Visible = True
                lblLockOutAttempts.Visible = True
            Else
                pnlPwd.Visible = False
                lblPwdComplexity.Visible = False
                btnSetPwdComplexity.Visible = False
                txtNoOfAttempts.Visible = False
                lblLockOutAttempts.Visible = False
            End If

            Me.Cursor = Cursors.WaitCursor
            Call Fill_FAXCompressions()
            Call Fill_FAXSpeakerVolume()
            Call Fill_DMSCategories()

            'sarika 5th sept 07
            Call Fill_FaxUsers()
            '----------------------

            Dim objSettings As New clsSettings
            If objSettings.GetSettings() = True Then
                If IsNothing(objSettings.AppointmentStartTime) = False Then
                    tmStartTime.Value = objSettings.AppointmentStartTime
                End If
                If IsNothing(objSettings.AppointmentEndTime) = False Then
                    tmEndTime.Value = objSettings.AppointmentEndTime
                End If
                If IsNothing(objSettings.AppointmentInterval) = False Then
                    AppointmentInterval.Value = objSettings.AppointmentInterval
                End If
                If IsNothing(objSettings.PULLCHARTSInterval) = False Then
                    PullChartsInterval.Value = objSettings.PULLCHARTSInterval
                End If
                If IsNothing(objSettings.MaxNoOfFAXRetries) = False Then
                    numMaxNoOfRetries.Value = objSettings.MaxNoOfFAXRetries
                End If
                If IsNothing(objSettings.FAXCompression) = False Then
                    cmbFAXCompression.Text = objSettings.FAXCompression
                Else
                    cmbFAXCompression.Text = "CCITT G3"
                End If
                If IsNothing(objSettings.FAXSpeakerVoulme) = False Then
                    cmbSpeakerVolume.Text = objSettings.FAXSpeakerVoulme
                Else
                    cmbSpeakerVolume.Text = "No Volume"
                End If
                If IsNothing(objSettings.FAXRetryInterval) = False Then
                    numFAXRetryInterval.Value = objSettings.FAXRetryInterval
                End If
                optFAXreceiveYes.Checked = objSettings.FAXReceiveEnabled
                optHPIYes.Checked = objSettings.HPIEnabled
                optLocationAddressedYes.Checked = objSettings.LocationAddressed
                '*************
                optPwdComplexYes.Checked = objSettings.blnPwdComplexity

                'sarika 14th june 07
                optClinicDIYes.Checked = objSettings.ClinicDISettings
                '---------------

                If Trim(objSettings.OMRCategoryHistory) <> "" Then
                    cmbOMRCategoryHistory.Text = objSettings.OMRCategoryHistory
                End If
                If Trim(objSettings.OMRCategoryROS) <> "" Then
                    cmbOMRCategoryROS.Text = objSettings.OMRCategoryROS
                End If
                If Trim(objSettings.OMRCategoryPatientRegistration) <> "" Then
                    cmbOMRCategoryPatientRegistration.Text = objSettings.OMRCategoryPatientRegistration
                End If
                If Trim(objSettings.OMRCategoryDirective) <> "" Then
                    cmbCategoryDirective.Text = objSettings.OMRCategoryDirective
                End If
                If Trim(objSettings.Labs) <> "" Then
                    cmbLabCategory.Text = objSettings.Labs
                End If
                'sarika 31st aug 07
                If Trim(objSettings.OMRCategoryFax) <> "" Then
                    cmbFaxCategory.Text = objSettings.OMRCategoryFax
                End If
                '-------------------

                ''//Code commented by Ravikiran on 14/02/2007
                '''' //Code added by Ravikiran on 13/02/2007 for RxReportPath settings
                'If gstrRxReportpath Is Nothing Then
                '    txtRxReportPath.Text = ""
                'Else
                '    txtRxReportPath.Text = gstrRxReportpath
                'End If

                txtNoOfAttempts.Text = objSettings.NoOfAttempts

                If IsNothing(objSettings.RecordLevelLocking) = False Then
                    chkRecordLocking.Checked = objSettings.RecordLevelLocking
                End If
                'code added by sagar to access the Threshold value on 31 july 2007
                If IsNothing(objSettings.ThresholdValue) = False Then
                    txtThresholdValue.Text = objSettings.ThresholdValue
                End If


                'sarika 5th sept 07
                If IsNothing(objSettings.PendingFaxUserID) = False Then
                    cmbPendingFaxUser.Text = GetLoginName(objSettings.PendingFaxUserID)
                End If

                If IsNothing(objSettings.RecieveFaxUserID) = False Then
                    cmbRecieveFaxUser.Text = GetLoginName(objSettings.RecieveFaxUserID)
                End If
                '-------------------------
            End If

            If optPwdComplexNo.Checked = True Then
                btnSetPwdComplexity.Visible = False
            End If

            'sarika 11th aug 07
            txtHL7FilePath.Text = objSettings.HL7SystemPath
            '-------------------

            'sarika 31st aug 07
            If Trim(objSettings.DBVersion) <> "" Then
                txtDBVersion.Text = objSettings.DBVersion
            End If
            If Trim(objSettings.AppVersion) <> "" Then
                txtAppVersion.Text = objSettings.AppVersion
            End If
            '-------------


            'sarika 31st aug 07
            'fill the arraylist with fax values at form load
            Dim arrList As ArrayList
            arrList = New ArrayList
            SetData(arrList)

            _arrayGetData = arrList
            '---------

            objSettings = Nothing
            Me.Cursor = Cursors.Default
        Catch objErr As Exception
            Me.Cursor = Cursors.Default
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Function GetLoginName(ByVal UserID As Int64) As String
        Dim conn As New SqlConnection()
        Dim objCmd As SqlCommand
        Dim LoginName As String = ""
        Dim _strSQL As String = ""

        Try
            conn.ConnectionString = gloPMAdmin.mdlGeneral.GetConnectionString()

            conn.Open()
            _strSQL = "select isnull(sLoginName,'') as sLoginName from User_MST where nUserID = " & UserID
            objCmd = New SqlCommand(_strSQL, conn)
            LoginName = objCmd.ExecuteScalar()

            Return LoginName

        Catch ex As Exception

            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return ""
        Finally
            conn.Close()
        End Try
    End Function

    Private Sub Fill_FaxUsers()
        Dim conn As New SqlConnection()
        Dim objdaUsers As SqlDataAdapter
        Dim dtUsers As DataTable
        Dim dtUsers1 As DataTable
        Dim _strSQL As String = ""

        Try
            conn.ConnectionString = gloPMAdmin.mdlGeneral.GetConnectionString()
            conn.Open()

            _strSQL = "select nUserID , sLoginName from User_MST"
            objdaUsers = New SqlDataAdapter(_strSQL, conn)

            dtUsers = New DataTable
            dtUsers1 = New DataTable

            objdaUsers.Fill(dtUsers)
            objdaUsers.Fill(dtUsers1)

            cmbPendingFaxUser.DataSource = dtUsers
            cmbPendingFaxUser.DisplayMember = "sLoginName"
            cmbPendingFaxUser.ValueMember = "nUserID"

            cmbRecieveFaxUser.DataSource = dtUsers1
            cmbRecieveFaxUser.DisplayMember = "sLoginName"
            cmbRecieveFaxUser.ValueMember = "nUserID"

        Catch sqlex As SqlException
            MessageBox.Show(sqlex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
            conn = Nothing
            objdaUsers = Nothing
            dtUsers = Nothing
        End Try
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Try
            Me.Close()
        Catch objErr As Exception
            Me.Cursor = Cursors.Default
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        Try
            Dim dtStart As Long
            Dim dtEnd As Long
            dtStart = New TimeSpan(tmStartTime.Value.Hour, tmStartTime.Value.Minute, tmStartTime.Value.Second).Ticks
            dtEnd = New TimeSpan(tmEndTime.Value.Hour, tmEndTime.Value.Minute, tmEndTime.Value.Second).Ticks
            If dtStart >= dtEnd Then
                MessageBox.Show("Clinic Start Time must be less than Clinic Closing Time", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                tmStartTime.Focus()
                Exit Sub
            End If
            If AppointmentInterval.Value <= 0 Then
                MessageBox.Show("Appointment Interval must be greater than 0 minutes.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                AppointmentInterval.Focus()
                Exit Sub
            End If
            If AppointmentInterval.Value Mod 5 <> 0 Then
                MessageBox.Show("Appointment Interval must be in multiple of 5 minutes.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                AppointmentInterval.Focus()
                Exit Sub
            End If
            If PullChartsInterval.Value Mod 5 <> 0 Then
                MessageBox.Show("Pull Charts Interval must be in multiple of 5 minutes.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                PullChartsInterval.Focus()
                Exit Sub
            End If
            If numMaxNoOfRetries.Value <= 0 Or numMaxNoOfRetries.Value >= 500 Then
                MessageBox.Show("Maximum No of Retries for FAX must be between 1 to 500.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                numMaxNoOfRetries.Focus()
                Exit Sub
            End If
            If numFAXRetryInterval.Value <= 0 Or numFAXRetryInterval.Value >= 500 Then
                MessageBox.Show("FAX Retry Interval must be between 1 to 500 minutes.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                numFAXRetryInterval.Focus()
                Exit Sub
            End If
            'code added by sagar on 31 july 2007 for threshold value
            If Trim(txtThresholdValue.Text).Length <> 0 Then
                If Val(Trim(txtThresholdValue.Text)) = 0 Then
                    MessageBox.Show("Threshold value should be minimum 1 minute", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    txtThresholdValue.Focus()
                    Exit Sub
                End If
            Else
                MessageBox.Show("Default Threshold value 420 minute will be saved as no value has been entered", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If

            Me.Cursor = Cursors.WaitCursor
            Dim objSettings As New clsSettings
            objSettings.AppointmentStartTime = tmStartTime.Value
            objSettings.AppointmentEndTime = tmEndTime.Value
            objSettings.AppointmentInterval = AppointmentInterval.Value
            objSettings.PULLCHARTSInterval = PullChartsInterval.Value
            objSettings.MaxNoOfFAXRetries = numMaxNoOfRetries.Value
            objSettings.FAXRetryInterval = numFAXRetryInterval.Value
            objSettings.HPIEnabled = optHPIYes.Checked
            objSettings.LocationAddressed = optLocationAddressedYes.Checked
            objSettings.FAXCompression = cmbFAXCompression.Text
            objSettings.FAXSpeakerVoulme = cmbSpeakerVolume.Text
            objSettings.FAXReceiveEnabled = optFAXreceiveYes.Checked

            objSettings.OMRCategoryHistory = cmbOMRCategoryHistory.Text
            objSettings.OMRCategoryROS = cmbOMRCategoryROS.Text
            objSettings.OMRCategoryPatientRegistration = cmbOMRCategoryPatientRegistration.Text
            objSettings.OMRCategoryDirective = cmbCategoryDirective.Text
            objSettings.Labs = cmbLabCategory.Text
            'sarika 31st aug 07
            objSettings.OMRCategoryFax = cmbFaxCategory.Text
            '----------------------------
            'objSettings.blnPwdComplexity = optPwdComplexYes.Checked

            objSettings.NoOfAttempts = txtNoOfAttempts.Text.Trim

            'save the password complexity settings 
            'm_strSQL()
            'm_blnSetComplexisityOnSetting()
            'If m_blnSetComplexisityOnSetting = False And optPwdComplexYes.Checked = True Then
            '    MessageBox.Show("You have not set password complexicity.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            '    frm.ShowDialog(Me)
            '    'optPwdComplexNo.Checked = True
            '    If frm._blnSetComplexisityOnSetting = False Then
            '        optPwdComplexNo.Checked = True
            '    End If
            'End If

            objSettings.blnPwdComplexity = optPwdComplexYes.Checked

            'sarika 14th june 07
            objSettings.ClinicDISettings = optClinicDIYes.Checked

            '' Mahesh 20070723 -- Record Level Locking 
            objSettings.RecordLevelLocking = chkRecordLocking.Checked

            'code added by sagar on 31 july 2007 for threshold value
            If Trim(txtThresholdValue.Text) <> "" Then
                objSettings.ThresholdValue = Trim(txtThresholdValue.Text)
            Else
                objSettings.ThresholdValue = 420
            End If

            'sarika 11th aug 07
            objSettings.HL7SystemPath = txtHL7FilePath.Text
            '-------------------------------------

            'sarika 31st aug 07
            objSettings.DBVersion = txtDBVersion.Text
            objSettings.AppVersion = txtAppVersion.Text
            '-------------------------------------

            'sarika 5th sept 07
            objSettings.PendingFaxUserID = cmbPendingFaxUser.SelectedValue
            objSettings.RecieveFaxUserID = cmbRecieveFaxUser.SelectedValue
            '-----------------


            If objSettings.UpdateSettings() = False Then
                Me.Cursor = Cursors.Default
                MessageBox.Show("Unable to update settings", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                tmStartTime.Focus()
                objSettings = Nothing
                Exit Sub
            End If

            Dim objAudit As New clsAudit

            If objSettings.blnPwdComplexity = True Then
                If m_strSQL <> "" Then
                    If objSettings.SetPwdComplexitySettings(m_strSQL) = False Then
                        Me.Cursor = Cursors.Default
                        MessageBox.Show("Unable to update the password complexity settings", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        tmStartTime.Focus()
                        objSettings = Nothing
                        Exit Sub
                    Else
                        'sarika  21 feb

                        objAudit.CreateLog(clsAudit.enmActivityType.Other, gstrLoginName & " has reset the Password Settings.", gstrLoginName, gstrClientMachineName)
                    End If
                End If

            Else
                If sqlstrsettings <> "" Then
                    If objSettings.SetPwdComplexitySettings(sqlstrsettings) = False Then
                        Me.Cursor = Cursors.Default
                        MessageBox.Show("Unable to update the password complexity settings", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        tmStartTime.Focus()
                        objSettings = Nothing
                        Exit Sub
                    Else
                        'sarika  21 feb
                        objAudit.CreateLog(clsAudit.enmActivityType.Other, gstrLoginName & " has reset the Password Settings.", gstrLoginName, gstrClientMachineName)
                    End If
                    'Else
                    'optPwdComplexNo.Checked = True
                    'objSettings.blnPwdComplexity = False
                End If

                'objAudit = Nothing
                '-------------

            End If



            'sarika  21 feb
            '  Dim objAudit As New clsAudit
            objAudit.CreateLog(clsAudit.enmActivityType.Other, gstrLoginName & " has reset the Settings.", gstrLoginName, gstrClientMachineName)
            objAudit = Nothing
            '-------------

            'If objSettings.blnPwdComplexity = True Then
            '    If m_strSQL <> "" Then
            '        If objSettings.SetPwdComplexitySettings(m_strSQL) = False Then
            '            Me.Cursor = Cursors.Default
            '            MessageBox.Show("Unable to update the password complexity settings", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '            tmStartTime.Focus()
            '            objSettings = Nothing
            '            Exit Sub
            '        End If
            '    End If

            'Else
            '    If _sqlstrsettings <> "" Then
            '        If objSettings.SetPwdComplexitySettings(_sqlstrsettings) = False Then
            '            Me.Cursor = Cursors.Default
            '            MessageBox.Show("Unable to update the password complexity settings", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '            tmStartTime.Focus()
            '            objSettings = Nothing
            '            Exit Sub
            '        End If
            '        'Else
            '        'optPwdComplexNo.Checked = True
            '        'objSettings.blnPwdComplexity = False
            '    End If

            'End If


            'If _sqlstrsettings <> "" Then
            '    If objSettings.SetPwdComplexitySettings(_sqlstrsettings) = False Then
            '        Me.Cursor = Cursors.Default
            '        MessageBox.Show("Unable to update the password complexity settings", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '        tmStartTime.Focus()
            '        objSettings = Nothing
            '        Exit Sub
            '    End If

            'End If

            objSettings = Nothing
            If CheckFaxModifications() = True Then
                MessageBox.Show("To apply the new FAX Settings to the FAX Application, please re-run the FAX Application", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

            Me.Close()
            Me.Cursor = Cursors.Default

            ''//code commented by Ravikiran on 14/02/2007 
            ''''' Code added for customr Report path settings by Ravikiran on 10/02/2007
            ''If Trim(txtRxReportPath.Text) <> "" Then
            ''    If Directory.Exists(txtRxReportPath.Text) = False Then
            ''        MessageBox.Show(txtRxReportPath.Text & " is not valid path." & vbCrLf & "Please browse for valid RxReport Directory", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ''        txtRxReportPath.Focus()
            ''        Exit Sub
            ''    End If
            ''End If
            ''If Trim(txtRxReportPath.Text) <> "" Then
            ''    gstrRxReportpath = txtRxReportPath.Text
            ''    If Not checkRxReportPath(gstrRxReportpath) Then
            ''        InsertRxReportPath(gstrRxReportpath)
            ''    End If

            ''End If

            ''''Code updation ends







        Catch objErr As Exception
            Me.Cursor = Cursors.Default
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            'sarika  21 feb
            Dim objAudit As New clsAudit
            objAudit.CreateLog(clsAudit.enmActivityType.Modify, " Error occured while modifying the settings.", gstrLoginName, gstrClientMachineName, 0, , clsAudit.enmOutcome.Failure)
            objAudit = Nothing
            '-------------
        End Try
    End Sub

    Private Function CheckFaxModifications() As Boolean
        Try
            'chk the current values with the stored vals
            Dim Faxreceive As Integer

            If optFAXreceiveYes.Checked = True Then
                Faxreceive = 1
            Else
                Faxreceive = 0
            End If

            If _arrayGetData(0) <> numMaxNoOfRetries.Value Then
                bModifyData = True
                Return bModifyData
            End If
            If _arrayGetData(1) <> numFAXRetryInterval.Value Then
                bModifyData = True
                Return bModifyData
            End If
            If _arrayGetData(2) <> cmbFAXCompression.Text Then
                bModifyData = True
                Return bModifyData
            End If
            If _arrayGetData(3) <> cmbSpeakerVolume.Text Then
                bModifyData = True
                Return bModifyData
            End If
            If _arrayGetData(4) <> Faxreceive Then
                bModifyData = True
                Return bModifyData
            End If


        Catch ex As Exception
            MessageBox.Show("Error in Setdata : " & ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function

    Private Sub SetData(ByVal arrList As ArrayList)
        Try
            arrList.Add(numMaxNoOfRetries.Value)
            arrList.Add(numFAXRetryInterval.Value)
            arrList.Add(cmbFAXCompression.Text)
            arrList.Add(cmbSpeakerVolume.Text)
            If optFAXreceiveYes.Checked = True Then
                arrList.Add(1)
            Else
                arrList.Add(0)
            End If
        Catch ex As Exception
            MessageBox.Show("Error in Setdata : " & ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

#Region "   Private Methods"
    Private Sub Fill_FAXCompressions()
        With cmbFAXCompression
            .BeginUpdate()
            .Items.Clear()
            .Items.Add("CCITT G3")
            .Items.Add("CCITT G4")
            .Items.Add("Packbits")
            .EndUpdate()
        End With
    End Sub
    Private Sub Fill_FAXSpeakerVolume()
        With cmbSpeakerVolume
            .BeginUpdate()
            .Items.Clear()
            .Items.Add("No Volume")
            .Items.Add("Low Volume")
            .Items.Add("Medium Volume")
            .Items.Add("High Volume")
            .EndUpdate()
        End With
    End Sub
    Private Sub Fill_DMSCategories()
        Dim clCategories As New Collection
        clCategories = DMSCategories()

        cmbOMRCategoryHistory.BeginUpdate()
        cmbOMRCategoryHistory.Items.Clear()
        cmbOMRCategoryROS.Items.Clear()
        cmbOMRCategoryPatientRegistration.Items.Clear()
        cmbCategoryDirective.Items.Clear()
        cmbLabCategory.Items.Clear()

        'sarika 31st aug 07
        cmbFaxCategory.Items.Clear()
        '--------------
        cmbOMRCategoryHistory.Items.Add("")
        cmbOMRCategoryROS.Items.Add("")
        cmbOMRCategoryPatientRegistration.Items.Add("")
        cmbCategoryDirective.Items.Add("")
        cmbLabCategory.Items.Add("")

        'sarika 31st aug 07
        cmbFaxCategory.Items.Add("")
        '---------

        Dim nCount As Int16
        For nCount = 1 To clCategories.Count
            cmbOMRCategoryHistory.Items.Add(clCategories(nCount))
            cmbOMRCategoryROS.Items.Add(clCategories(nCount))
            cmbOMRCategoryPatientRegistration.Items.Add(clCategories(nCount))
            cmbCategoryDirective.Items.Add(clCategories(nCount))
            cmbLabCategory.Items.Add(clCategories(nCount))

            'sarika 31st aug 07
            cmbFaxCategory.Items.Add(clCategories(nCount))
            '---------
        Next
        cmbOMRCategoryHistory.EndUpdate()

    End Sub

#End Region

    Private Sub optPwdComplexYes_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optPwdComplexYes.CheckedChanged
        'Try
        '    If optcnt <> 0 Then
        '        If optPwdComplexYes.Checked = True Then
        '            Dim frm As New frmPwdSettings
        '            frm.ShowDialog()
        '        End If
        '    End If
        '    'cmbOMRCategoryHistory.Focus()
        'Catch ex As Exception
        '    MsgBox(ex.Message, MsgBoxStyle.OKOnly, "Password Complexity")
        'Finally
        'End Try
        'optcnt = 1
        Dim conn As New SqlConnection(gloPMAdmin.mdlGeneral.GetConnectionString)
        Dim cmd As SqlCommand
        Dim cnt As Integer = 0
        Dim _strSQL As String = ""
        'Dim oDataReader As SqlDataReader
        Dim blnadmin As Boolean = False
        Dim str As String = ""

        If optPwdComplexYes.Checked = True Then
            '   _strSQL = ""
            btnSetPwdComplexity.Visible = True
            '_strSQL = ""
        Else
            btnSetPwdComplexity.Visible = False
            ' btnSetPwdComplexity.Visible = False
            conn.Open()

            'str = "Password Complexity"
            '_strSQL = "update Settings set sSettingsValue = " & 0 & " where sSettingsName ='" & str & "'"
            'cmd = New SqlCommand(_strSQL, conn)
            'cmd.ExecuteNonQuery()

            _strSQL = "select count(*) from PwdSettings"
            cmd = New SqlCommand(_strSQL, conn)
            cnt = cmd.ExecuteScalar

            If cnt = 0 Then
                'insert  row
                _strSQL = "insert into PwdSettings(ExpCapitalLetters,ExpNoOfLetters,ExpNoOfDigits,ExpNoOfSpecChars,ExpPwdLength,ExpTimeFrameinDays) " & _
                        " values(" & 0 & "," & 0 & "," & 0 & "," & 0 & "," & 1 & "," & 0 & ")"

            Else
                'update row
                _strSQL = "Update PwdSettings set ExpCapitalLetters = " & 0 & " ,ExpNoOfLetters = " & 0 & " ,ExpNoOfDigits = " & 0 & _
                        ",ExpNoOfSpecChars = " & 0 & ",ExpPwdLength = " & 1 & ",ExpTimeFrameinDays = " & 0
            End If
            'cmd = New SqlCommand(_strSQL, conn)
            'cmd.ExecuteNonQuery()

            'Dim frmSettings As New frmSettings
            'frmSettings.strSQL = _strSQL
            sqlstrsettings = _strSQL
            'm_strSQL = _strSQL

        End If

    End Sub

    Private Sub btnSetPwdComplexity_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetPwdComplexity.Click
        'Dim conn As New SqlConnection(gloEMRAdmin.mdlGeneral.GetConnectionString)
        'Dim cmd As SqlCommand
        'Dim cnt As Integer = 0
        'Dim _strSQL As String = ""
        'Dim oDataReader As SqlDataReader

        'Try
        '    If optPwdComplexYes.Checked = True Then

        Dim frm As New frmPwdSettings
        frm.ShowDialog(Me)
        m_strSQL = frm.strSQL
        m_blnSetComplexisityOnSetting = frm.blnSetComplexisityOnSetting
        If m_blnSetComplexisityOnSetting = False Then
            MessageBox.Show("You have not set password complexicity.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            optPwdComplexNo.Checked = True
        End If
        'Else
        'conn.Open()
        '_strSQL = "select count(*) from PwdSettings"
        'cmd = New SqlCommand(_strSQL, conn)
        'cnt = cmd.ExecuteScalar

        'If cnt = 0 Then
        '    'insert  row
        '    _strSQL = "insert into PwdSettings(ExpCapitalLetters,ExpNoOfLetters,ExpNoOfDigits,ExpNoOfSpecChars,ExpPwdLength,ExpTimeFrameinDays) " & _
        '              " values(" & 0 & "," & 0 & "," & 0 & "," & 0 & "," & 0 & "," & 0 & ")"

        'Else
        '    'update row
        '    _strSQL = "Update PwdSettings set ExpCapitalLetters = " & 0 & " ,ExpNoOfLetters = " & 0 & " ,ExpNoOfDigits = " & 0 & _
        '              ",ExpNoOfSpecChars = " & 0 & ",ExpPwdLength = " & 0 & ",ExpTimeFrameinDays = " & 0
        'End If
        'cmd = New SqlCommand(_strSQL, conn)
        'cmd.ExecuteNonQuery()
        '    End If
        'Catch ex As Exception
        '    MsgBox(ex.Message, MsgBoxStyle.OKOnly, "Password Complexity")
        'Finally

        'End Try
    End Sub

    Public Function GetAdminFlag() As Boolean
        Dim conn As New SqlConnection(gloPMAdmin.mdlGeneral.GetConnectionString)
        Dim cmd As SqlCommand
        Dim cnt As Integer = 0
        Dim _strSQL As String = ""
        'Dim oDataReader As SqlDataReader
        Dim blnadmin As Boolean = False

        Try
            conn.Open()
            _strSQL = "select nAdministrator from User_MST where sLoginName ='" & gstrLoginName & "'"
            cmd = New SqlCommand(_strSQL, conn)
            blnadmin = cmd.ExecuteScalar

            Return blnadmin

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            conn.Close()
        End Try
    End Function

    Private Sub btnBrowseRxReportPath_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowseRxReportPath.Click
        Try
            With FolderBrowserDialog1()
                .ShowNewFolderButton = True
                .Description = "Select Rx Report Directory"
                If .ShowDialog() = DialogResult.OK Then
                    txtRxReportPath.Text = .SelectedPath
                End If
            End With
        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub
    '//code commented by Ravikiran on 14/02/2007
    ' // Setting Report path Settings in Database
    'Private Function InsertRxReportPath(ByVal strPath As String)
    '    Dim objConn As New SqlConnection
    '    Dim objcmd As New SqlCommand
    '    Try


    '        Dim _strSQL As String = ""
    '        objConn.ConnectionString = gloEMRAdmin.mdlGeneral.GetConnectionString()
    '        objcmd.Connection = objConn
    '        If objConn.State = ConnectionState.Open Then
    '            objConn.Close()
    '        Else
    '            objConn.Open()
    '            _strSQL = "Select max(nSettingsID) from Settings"
    '            objcmd.CommandText = _strSQL
    '            Dim RxID = objcmd.ExecuteScalar
    '            objcmd.Cancel()
    '            _strSQL = Nothing
    '            If Not IsDBNull(RxID) Then
    '                RxID += 1
    '                _strSQL = "Insert into Settings(nSettingsID,sSettingsName,sSettingsValue) values(" & RxID & ",'RxReportPath','" & strPath & "')"
    '                objcmd.CommandText = _strSQL
    '                ' objcmd.Connection = objConn
    '                objcmd.ExecuteNonQuery()
    '                objcmd.Cancel()
    '            End If


    '        End If
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message)
    '    Finally
    '        objConn.Close()
    '    End Try
    'End Function

    'Private Function checkRxReportPath(ByVal gstrRxReportpath As String) As Boolean
    '    Dim objConn As New SqlConnection
    '    Dim objcmd As New SqlCommand
    '    Dim objReader As SqlDataReader
    '    Try


    '        Dim _strSQL As String = ""
    '        objConn.ConnectionString = gloEMRAdmin.mdlGeneral.GetConnectionString()
    '        objcmd.Connection = objConn
    '        If objConn.State = ConnectionState.Open Then
    '            objConn.Close()
    '        Else
    '            objConn.Open()
    '            _strSQL = "Select nSettingsID from Settings where sSettingsName='RxReportPath'"
    '            objcmd.CommandText = _strSQL
    '            Dim RxPath As Long
    '            objReader = objcmd.ExecuteReader
    '            If Not IsDBNull(objReader) Then
    '                If objReader.HasRows Then
    '                    objReader.Read()
    '                    RxPath = objReader(0)
    '                    objReader.Close()
    '                    objcmd.Cancel()
    '                Else
    '                    Return False
    '                End If

    '            Else
    '                Return False

    '            End If

    '            If RxPath <> 0 Then
    '                objcmd.CommandText = "Update Settings set sSettingsValue='" & gstrRxReportpath & "' where nSettingsID=" & RxPath

    '                objcmd.ExecuteNonQuery()
    '                objcmd.Cancel()
    '                Return True
    '            Else
    '                Return False
    '            End If
    '        End If


    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message)
    '        Return False
    '    Finally
    '        objConn.Close()
    '    End Try
    'End Function




    Private Sub txtNoOfAttempts_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtNoOfAttempts.KeyPress
        Try
            If Not ((e.KeyChar >= ChrW(48) And e.KeyChar <= ChrW(57)) Or (e.KeyChar = ChrW(8))) Then
                e.Handled = True
            Else
                If (e.KeyChar = ChrW(8)) Then
                    Exit Sub
                Else
                    txtNoOfAttempts.Focus()
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Patient Vitals", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub txtNoOfAttempts_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtNoOfAttempts.Validating
        Try
            If CInt(Val(txtNoOfAttempts.Text.Trim) < 1) Then
                MessageBox.Show("LockOut Attempts cannot be 0. It must be atleast 1.", "gloEMR Admin", MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtNoOfAttempts.Text = ""
                txtNoOfAttempts.Focus()
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub txtThresholdValue_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtThresholdValue.KeyPress
        'Allow only numeric and decimal point keys
        If InStr(Trim(Text), ".") <> 0 And (e.KeyChar = ChrW(46)) Then
            e.Handled = True
        Else
            If Not ((e.KeyChar >= ChrW(48) And e.KeyChar <= ChrW(57)) Or (e.KeyChar = ChrW(46)) Or (e.KeyChar = ChrW(8))) Then
                e.Handled = True
            End If
        End If
    End Sub
    'code added by sarika on 11th aug 07
    '-------------
    Private Sub btnHL7FilePath_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHL7FilePath.Click

        Try
            With FolderBrowserDialog1()
                .ShowNewFolderButton = True
                .Description = "Select HL7 System Path"
                If .ShowDialog() = DialogResult.OK Then
                    txtHL7FilePath.Text = .SelectedPath
                End If
            End With
        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    '-------------
End Class
