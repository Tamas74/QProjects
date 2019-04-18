Imports System.IO
Imports Microsoft.Win32
Imports C1.Win.C1FlexGrid
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.Data.SqlClient


Public Class frmPatient_RptViewer

    Inherits System.Windows.Forms.Form
    Dim appSettings As System.Collections.Specialized.NameValueCollection = System.Configuration.ConfigurationManager.AppSettings


#Region " Windows Form Designer generated code "

    Public Sub New(ByVal PatientID As Long)
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        _patientID = PatientID
    End Sub

    Public Sub New(ByVal LoadFromOtherForm As Boolean, ByVal PatientID As Long)
        InitializeComponent()
        mLoadFromOtherForm = LoadFromOtherForm
        _patientID = PatientID
    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then

            Try
                If (IsNothing(dtpToDate) = False) Then
                    Try
                        gloGlobal.cEventHelper.RemoveAllEventHandlers(dtpToDate)
                    Catch ex As Exception

                    End Try


                    dtpToDate.Dispose()
                    dtpToDate = Nothing
                End If
            Catch
            End Try

            Try
                If (IsNothing(dtpFromDate) = False) Then
                    Try
                        gloGlobal.cEventHelper.RemoveAllEventHandlers(dtpFromDate)
                    Catch ex As Exception

                    End Try


                    dtpFromDate.Dispose()
                    dtpFromDate = Nothing
                End If
            Catch
            End Try

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
    Dim _patientID As Long
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents pnlPatient As System.Windows.Forms.Panel
    Friend WithEvents pnlChkBox As System.Windows.Forms.Panel
    Friend WithEvents chkHistory As System.Windows.Forms.CheckBox
    Friend WithEvents chkInsurance As System.Windows.Forms.CheckBox
    Friend WithEvents chkPatientDemo As System.Windows.Forms.CheckBox
    Friend WithEvents chkPrescription As System.Windows.Forms.CheckBox
    Friend WithEvents chkDiagnosis As System.Windows.Forms.CheckBox
    Friend WithEvents chkTreatment As System.Windows.Forms.CheckBox
    Friend WithEvents chkEducation As System.Windows.Forms.CheckBox
    Friend WithEvents chkROS As System.Windows.Forms.CheckBox
    Friend WithEvents chkMessages As System.Windows.Forms.CheckBox
    Friend WithEvents chkExam As System.Windows.Forms.CheckBox
    Friend WithEvents chkVitals As System.Windows.Forms.CheckBox
    Friend WithEvents ChkSelectAll As System.Windows.Forms.CheckBox
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents lblMessage As System.Windows.Forms.Label
    Friend WithEvents dtpToDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents dtpFromDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Private WithEvents pnl_tlsp As System.Windows.Forms.Panel
    Private WithEvents tlsp_Patient_RptViewer As gloGlobal.gloToolStripIgnoreFocus
    Private WithEvents ts_btnSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton1 As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnClose As System.Windows.Forms.ToolStripButton
    Private WithEvents lbl_pnlBottom As System.Windows.Forms.Label
    Private WithEvents lbl_pnlLeft As System.Windows.Forms.Label
    Private WithEvents lbl_pnlRight As System.Windows.Forms.Label
    Private WithEvents lbl_pnlTop As System.Windows.Forms.Label
    Private WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents Label3 As System.Windows.Forms.Label
    Private WithEvents Label4 As System.Windows.Forms.Label
    Private WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents chkFlowsheet As System.Windows.Forms.CheckBox
    Friend WithEvents chkConsent As System.Windows.Forms.CheckBox
    Friend WithEvents chkDMS As System.Windows.Forms.CheckBox
    Friend WithEvents chkPatientExams As System.Windows.Forms.CheckBox
    Friend WithEvents chkLabs As System.Windows.Forms.CheckBox
    Friend WithEvents chkMedication As System.Windows.Forms.CheckBox
    Friend WithEvents chkOrders As System.Windows.Forms.CheckBox
    Friend WithEvents chkImmunization As System.Windows.Forms.CheckBox
    Friend WithEvents chkProblemList As System.Windows.Forms.CheckBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPatient_RptViewer))
        Me.pnlPatient = New System.Windows.Forms.Panel()
        Me.pnlChkBox = New System.Windows.Forms.Panel()
        Me.chkOrders = New System.Windows.Forms.CheckBox()
        Me.chkImmunization = New System.Windows.Forms.CheckBox()
        Me.chkFlowsheet = New System.Windows.Forms.CheckBox()
        Me.chkConsent = New System.Windows.Forms.CheckBox()
        Me.chkDMS = New System.Windows.Forms.CheckBox()
        Me.chkPatientExams = New System.Windows.Forms.CheckBox()
        Me.chkLabs = New System.Windows.Forms.CheckBox()
        Me.chkMedication = New System.Windows.Forms.CheckBox()
        Me.lbl_pnlBottom = New System.Windows.Forms.Label()
        Me.lbl_pnlLeft = New System.Windows.Forms.Label()
        Me.lbl_pnlRight = New System.Windows.Forms.Label()
        Me.lbl_pnlTop = New System.Windows.Forms.Label()
        Me.chkProblemList = New System.Windows.Forms.CheckBox()
        Me.ChkSelectAll = New System.Windows.Forms.CheckBox()
        Me.chkVitals = New System.Windows.Forms.CheckBox()
        Me.chkExam = New System.Windows.Forms.CheckBox()
        Me.chkROS = New System.Windows.Forms.CheckBox()
        Me.chkTreatment = New System.Windows.Forms.CheckBox()
        Me.chkEducation = New System.Windows.Forms.CheckBox()
        Me.chkMessages = New System.Windows.Forms.CheckBox()
        Me.chkDiagnosis = New System.Windows.Forms.CheckBox()
        Me.chkHistory = New System.Windows.Forms.CheckBox()
        Me.chkInsurance = New System.Windows.Forms.CheckBox()
        Me.chkPatientDemo = New System.Windows.Forms.CheckBox()
        Me.chkPrescription = New System.Windows.Forms.CheckBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.dtpToDate = New System.Windows.Forms.DateTimePicker()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.dtpFromDate = New System.Windows.Forms.DateTimePicker()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblMessage = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.pnl_tlsp = New System.Windows.Forms.Panel()
        Me.tlsp_Patient_RptViewer = New gloGlobal.gloToolStripIgnoreFocus()
        Me.ts_btnSave = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton1 = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnClose = New System.Windows.Forms.ToolStripButton()
        Me.pnlPatient.SuspendLayout()
        Me.pnlChkBox.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.pnl_tlsp.SuspendLayout()
        Me.tlsp_Patient_RptViewer.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlPatient
        '
        Me.pnlPatient.BackColor = System.Drawing.Color.Transparent
        Me.pnlPatient.Controls.Add(Me.pnlChkBox)
        Me.pnlPatient.Controls.Add(Me.Panel1)
        Me.pnlPatient.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlPatient.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlPatient.Location = New System.Drawing.Point(0, 54)
        Me.pnlPatient.Name = "pnlPatient"
        Me.pnlPatient.Size = New System.Drawing.Size(504, 386)
        Me.pnlPatient.TabIndex = 1
        '
        'pnlChkBox
        '
        Me.pnlChkBox.BackColor = System.Drawing.Color.Transparent
        Me.pnlChkBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlChkBox.Controls.Add(Me.chkOrders)
        Me.pnlChkBox.Controls.Add(Me.chkImmunization)
        Me.pnlChkBox.Controls.Add(Me.chkFlowsheet)
        Me.pnlChkBox.Controls.Add(Me.chkConsent)
        Me.pnlChkBox.Controls.Add(Me.chkDMS)
        Me.pnlChkBox.Controls.Add(Me.chkPatientExams)
        Me.pnlChkBox.Controls.Add(Me.chkLabs)
        Me.pnlChkBox.Controls.Add(Me.chkMedication)
        Me.pnlChkBox.Controls.Add(Me.lbl_pnlBottom)
        Me.pnlChkBox.Controls.Add(Me.lbl_pnlLeft)
        Me.pnlChkBox.Controls.Add(Me.lbl_pnlRight)
        Me.pnlChkBox.Controls.Add(Me.lbl_pnlTop)
        Me.pnlChkBox.Controls.Add(Me.chkProblemList)
        Me.pnlChkBox.Controls.Add(Me.ChkSelectAll)
        Me.pnlChkBox.Controls.Add(Me.chkVitals)
        Me.pnlChkBox.Controls.Add(Me.chkExam)
        Me.pnlChkBox.Controls.Add(Me.chkROS)
        Me.pnlChkBox.Controls.Add(Me.chkTreatment)
        Me.pnlChkBox.Controls.Add(Me.chkEducation)
        Me.pnlChkBox.Controls.Add(Me.chkMessages)
        Me.pnlChkBox.Controls.Add(Me.chkDiagnosis)
        Me.pnlChkBox.Controls.Add(Me.chkHistory)
        Me.pnlChkBox.Controls.Add(Me.chkInsurance)
        Me.pnlChkBox.Controls.Add(Me.chkPatientDemo)
        Me.pnlChkBox.Controls.Add(Me.chkPrescription)
        Me.pnlChkBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlChkBox.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlChkBox.Location = New System.Drawing.Point(0, 30)
        Me.pnlChkBox.Name = "pnlChkBox"
        Me.pnlChkBox.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.pnlChkBox.Size = New System.Drawing.Size(504, 356)
        Me.pnlChkBox.TabIndex = 1
        '
        'chkOrders
        '
        Me.chkOrders.AutoSize = True
        Me.chkOrders.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkOrders.Location = New System.Drawing.Point(262, 316)
        Me.chkOrders.Name = "chkOrders"
        Me.chkOrders.Size = New System.Drawing.Size(62, 18)
        Me.chkOrders.TabIndex = 19
        Me.chkOrders.Text = "Orders"
        '
        'chkImmunization
        '
        Me.chkImmunization.AutoSize = True
        Me.chkImmunization.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkImmunization.Location = New System.Drawing.Point(36, 316)
        Me.chkImmunization.Name = "chkImmunization"
        Me.chkImmunization.Size = New System.Drawing.Size(98, 18)
        Me.chkImmunization.TabIndex = 18
        Me.chkImmunization.Text = "Immunization"
        '
        'chkFlowsheet
        '
        Me.chkFlowsheet.AutoSize = True
        Me.chkFlowsheet.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkFlowsheet.Location = New System.Drawing.Point(262, 282)
        Me.chkFlowsheet.Name = "chkFlowsheet"
        Me.chkFlowsheet.Size = New System.Drawing.Size(82, 18)
        Me.chkFlowsheet.TabIndex = 17
        Me.chkFlowsheet.Text = "Flowsheet"
        '
        'chkConsent
        '
        Me.chkConsent.AutoSize = True
        Me.chkConsent.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkConsent.Location = New System.Drawing.Point(262, 248)
        Me.chkConsent.Name = "chkConsent"
        Me.chkConsent.Size = New System.Drawing.Size(114, 18)
        Me.chkConsent.TabIndex = 15
        Me.chkConsent.Text = "Patient Consent"
        '
        'chkDMS
        '
        Me.chkDMS.AutoSize = True
        Me.chkDMS.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkDMS.Location = New System.Drawing.Point(36, 282)
        Me.chkDMS.Name = "chkDMS"
        Me.chkDMS.Size = New System.Drawing.Size(50, 18)
        Me.chkDMS.TabIndex = 16
        Me.chkDMS.Text = "DMS"
        '
        'chkPatientExams
        '
        Me.chkPatientExams.AutoSize = True
        Me.chkPatientExams.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkPatientExams.Location = New System.Drawing.Point(262, 211)
        Me.chkPatientExams.Name = "chkPatientExams"
        Me.chkPatientExams.Size = New System.Drawing.Size(103, 18)
        Me.chkPatientExams.TabIndex = 13
        Me.chkPatientExams.Text = "Patient Exams"
        '
        'chkLabs
        '
        Me.chkLabs.AutoSize = True
        Me.chkLabs.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkLabs.Location = New System.Drawing.Point(262, 177)
        Me.chkLabs.Name = "chkLabs"
        Me.chkLabs.Size = New System.Drawing.Size(50, 18)
        Me.chkLabs.TabIndex = 11
        Me.chkLabs.Text = "Labs"
        '
        'chkMedication
        '
        Me.chkMedication.AutoSize = True
        Me.chkMedication.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkMedication.Location = New System.Drawing.Point(262, 145)
        Me.chkMedication.Name = "chkMedication"
        Me.chkMedication.Size = New System.Drawing.Size(84, 18)
        Me.chkMedication.TabIndex = 9
        Me.chkMedication.Text = "Medication"
        '
        'lbl_pnlBottom
        '
        Me.lbl_pnlBottom.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_pnlBottom.Location = New System.Drawing.Point(4, 352)
        Me.lbl_pnlBottom.Name = "lbl_pnlBottom"
        Me.lbl_pnlBottom.Size = New System.Drawing.Size(496, 1)
        Me.lbl_pnlBottom.TabIndex = 27
        Me.lbl_pnlBottom.Text = "label2"
        '
        'lbl_pnlLeft
        '
        Me.lbl_pnlLeft.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_pnlLeft.Location = New System.Drawing.Point(3, 1)
        Me.lbl_pnlLeft.Name = "lbl_pnlLeft"
        Me.lbl_pnlLeft.Size = New System.Drawing.Size(1, 352)
        Me.lbl_pnlLeft.TabIndex = 26
        Me.lbl_pnlLeft.Text = "label4"
        '
        'lbl_pnlRight
        '
        Me.lbl_pnlRight.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_pnlRight.Location = New System.Drawing.Point(500, 1)
        Me.lbl_pnlRight.Name = "lbl_pnlRight"
        Me.lbl_pnlRight.Size = New System.Drawing.Size(1, 352)
        Me.lbl_pnlRight.TabIndex = 25
        Me.lbl_pnlRight.Text = "label3"
        '
        'lbl_pnlTop
        '
        Me.lbl_pnlTop.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_pnlTop.Location = New System.Drawing.Point(3, 0)
        Me.lbl_pnlTop.Name = "lbl_pnlTop"
        Me.lbl_pnlTop.Size = New System.Drawing.Size(498, 1)
        Me.lbl_pnlTop.TabIndex = 24
        Me.lbl_pnlTop.Text = "label1"
        '
        'chkProblemList
        '
        Me.chkProblemList.AutoSize = True
        Me.chkProblemList.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkProblemList.Location = New System.Drawing.Point(262, 113)
        Me.chkProblemList.Name = "chkProblemList"
        Me.chkProblemList.Size = New System.Drawing.Size(92, 18)
        Me.chkProblemList.TabIndex = 7
        Me.chkProblemList.Text = "Problem List"
        '
        'ChkSelectAll
        '
        Me.ChkSelectAll.AutoSize = True
        Me.ChkSelectAll.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkSelectAll.Location = New System.Drawing.Point(37, 17)
        Me.ChkSelectAll.Name = "ChkSelectAll"
        Me.ChkSelectAll.Size = New System.Drawing.Size(82, 18)
        Me.ChkSelectAll.TabIndex = 0
        Me.ChkSelectAll.Text = "Select All"
        '
        'chkVitals
        '
        Me.chkVitals.AutoSize = True
        Me.chkVitals.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkVitals.Location = New System.Drawing.Point(262, 80)
        Me.chkVitals.Name = "chkVitals"
        Me.chkVitals.Size = New System.Drawing.Size(54, 18)
        Me.chkVitals.TabIndex = 5
        Me.chkVitals.Text = "Vitals"
        '
        'chkExam
        '
        Me.chkExam.AutoSize = True
        Me.chkExam.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkExam.Location = New System.Drawing.Point(36, 177)
        Me.chkExam.Name = "chkExam"
        Me.chkExam.Size = New System.Drawing.Size(55, 18)
        Me.chkExam.TabIndex = 10
        Me.chkExam.Text = "Exam"
        '
        'chkROS
        '
        Me.chkROS.AutoSize = True
        Me.chkROS.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkROS.Location = New System.Drawing.Point(36, 248)
        Me.chkROS.Name = "chkROS"
        Me.chkROS.Size = New System.Drawing.Size(49, 18)
        Me.chkROS.TabIndex = 14
        Me.chkROS.Text = "ROS"
        '
        'chkTreatment
        '
        Me.chkTreatment.AutoSize = True
        Me.chkTreatment.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkTreatment.Location = New System.Drawing.Point(262, 14)
        Me.chkTreatment.Name = "chkTreatment"
        Me.chkTreatment.Size = New System.Drawing.Size(85, 18)
        Me.chkTreatment.TabIndex = 1
        Me.chkTreatment.Text = "Treatment"
        Me.chkTreatment.Visible = False
        '
        'chkEducation
        '
        Me.chkEducation.AutoSize = True
        Me.chkEducation.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkEducation.Location = New System.Drawing.Point(36, 80)
        Me.chkEducation.Name = "chkEducation"
        Me.chkEducation.Size = New System.Drawing.Size(123, 18)
        Me.chkEducation.TabIndex = 4
        Me.chkEducation.Text = "Patient Education"
        '
        'chkMessages
        '
        Me.chkMessages.AutoSize = True
        Me.chkMessages.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkMessages.Location = New System.Drawing.Point(36, 211)
        Me.chkMessages.Name = "chkMessages"
        Me.chkMessages.Size = New System.Drawing.Size(77, 18)
        Me.chkMessages.TabIndex = 12
        Me.chkMessages.Text = "Messages"
        '
        'chkDiagnosis
        '
        Me.chkDiagnosis.AutoSize = True
        Me.chkDiagnosis.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkDiagnosis.Location = New System.Drawing.Point(353, 14)
        Me.chkDiagnosis.Name = "chkDiagnosis"
        Me.chkDiagnosis.Size = New System.Drawing.Size(75, 18)
        Me.chkDiagnosis.TabIndex = 14
        Me.chkDiagnosis.Text = "Diagnosis"
        Me.chkDiagnosis.Visible = False
        '
        'chkHistory
        '
        Me.chkHistory.AutoSize = True
        Me.chkHistory.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkHistory.Location = New System.Drawing.Point(36, 113)
        Me.chkHistory.Name = "chkHistory"
        Me.chkHistory.Size = New System.Drawing.Size(63, 18)
        Me.chkHistory.TabIndex = 6
        Me.chkHistory.Text = "History"
        '
        'chkInsurance
        '
        Me.chkInsurance.AutoSize = True
        Me.chkInsurance.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkInsurance.Location = New System.Drawing.Point(262, 49)
        Me.chkInsurance.Name = "chkInsurance"
        Me.chkInsurance.Size = New System.Drawing.Size(79, 18)
        Me.chkInsurance.TabIndex = 3
        Me.chkInsurance.Text = "Insurance"
        '
        'chkPatientDemo
        '
        Me.chkPatientDemo.AutoSize = True
        Me.chkPatientDemo.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkPatientDemo.Location = New System.Drawing.Point(36, 49)
        Me.chkPatientDemo.Name = "chkPatientDemo"
        Me.chkPatientDemo.Size = New System.Drawing.Size(145, 18)
        Me.chkPatientDemo.TabIndex = 2
        Me.chkPatientDemo.Text = "Patient Demographics"
        '
        'chkPrescription
        '
        Me.chkPrescription.AutoSize = True
        Me.chkPrescription.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkPrescription.Location = New System.Drawing.Point(36, 145)
        Me.chkPrescription.Name = "chkPrescription"
        Me.chkPrescription.Size = New System.Drawing.Size(89, 18)
        Me.chkPrescription.TabIndex = 8
        Me.chkPrescription.Text = "Prescription"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Panel4)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Padding = New System.Windows.Forms.Padding(3)
        Me.Panel1.Size = New System.Drawing.Size(504, 30)
        Me.Panel1.TabIndex = 0
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Transparent
        Me.Panel4.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.Panel4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel4.Controls.Add(Me.dtpToDate)
        Me.Panel4.Controls.Add(Me.Label5)
        Me.Panel4.Controls.Add(Me.dtpFromDate)
        Me.Panel4.Controls.Add(Me.Label1)
        Me.Panel4.Controls.Add(Me.lblMessage)
        Me.Panel4.Controls.Add(Me.Label2)
        Me.Panel4.Controls.Add(Me.Label3)
        Me.Panel4.Controls.Add(Me.Label4)
        Me.Panel4.Controls.Add(Me.Label6)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel4.Location = New System.Drawing.Point(3, 3)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(498, 24)
        Me.Panel4.TabIndex = 1
        '
        'dtpToDate
        '
        Me.dtpToDate.CalendarForeColor = System.Drawing.Color.Maroon
        Me.dtpToDate.CalendarMonthBackground = System.Drawing.Color.White
        Me.dtpToDate.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.dtpToDate.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.dtpToDate.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.dtpToDate.CustomFormat = "MM/dd/yyyy"
        Me.dtpToDate.Dock = System.Windows.Forms.DockStyle.Left
        Me.dtpToDate.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpToDate.Location = New System.Drawing.Point(404, 1)
        Me.dtpToDate.Name = "dtpToDate"
        Me.dtpToDate.Size = New System.Drawing.Size(93, 22)
        Me.dtpToDate.TabIndex = 1
        Me.dtpToDate.Value = New Date(2005, 8, 3, 16, 21, 41, 875)
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(340, 1)
        Me.Label5.Name = "Label5"
        Me.Label5.Padding = New System.Windows.Forms.Padding(2, 4, 2, 2)
        Me.Label5.Size = New System.Drawing.Size(64, 20)
        Me.Label5.TabIndex = 2
        Me.Label5.Text = "To Date :"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dtpFromDate
        '
        Me.dtpFromDate.CalendarForeColor = System.Drawing.Color.Maroon
        Me.dtpFromDate.CalendarMonthBackground = System.Drawing.Color.White
        Me.dtpFromDate.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.dtpFromDate.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.dtpFromDate.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.dtpFromDate.CustomFormat = "MM/dd/yyyy"
        Me.dtpFromDate.Dock = System.Windows.Forms.DockStyle.Left
        Me.dtpFromDate.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFromDate.Location = New System.Drawing.Point(245, 1)
        Me.dtpFromDate.Name = "dtpFromDate"
        Me.dtpFromDate.Size = New System.Drawing.Size(95, 22)
        Me.dtpFromDate.TabIndex = 0
        Me.dtpFromDate.Value = New Date(2005, 8, 3, 16, 21, 41, 890)
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(169, 1)
        Me.Label1.Name = "Label1"
        Me.Label1.Padding = New System.Windows.Forms.Padding(2, 4, 2, 2)
        Me.Label1.Size = New System.Drawing.Size(76, 20)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "From Date :"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblMessage
        '
        Me.lblMessage.AutoSize = True
        Me.lblMessage.BackColor = System.Drawing.Color.Transparent
        Me.lblMessage.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblMessage.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMessage.Location = New System.Drawing.Point(1, 1)
        Me.lblMessage.Name = "lblMessage"
        Me.lblMessage.Padding = New System.Windows.Forms.Padding(2, 4, 2, 2)
        Me.lblMessage.Size = New System.Drawing.Size(168, 20)
        Me.lblMessage.TabIndex = 0
        Me.lblMessage.Text = "Select Patient Visit Date :"
        Me.lblMessage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label2.Location = New System.Drawing.Point(1, 23)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(496, 1)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "label2"
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label3.Location = New System.Drawing.Point(0, 1)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(1, 23)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "label4"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label4.Location = New System.Drawing.Point(497, 1)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(1, 23)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "label3"
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label6.Location = New System.Drawing.Point(0, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(498, 1)
        Me.Label6.TabIndex = 5
        Me.Label6.Text = "label1"
        '
        'pnl_tlsp
        '
        Me.pnl_tlsp.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnl_tlsp.Controls.Add(Me.tlsp_Patient_RptViewer)
        Me.pnl_tlsp.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnl_tlsp.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnl_tlsp.Location = New System.Drawing.Point(0, 0)
        Me.pnl_tlsp.Name = "pnl_tlsp"
        Me.pnl_tlsp.Size = New System.Drawing.Size(504, 54)
        Me.pnl_tlsp.TabIndex = 0
        '
        'tlsp_Patient_RptViewer
        '
        Me.tlsp_Patient_RptViewer.AllowDrop = True
        Me.tlsp_Patient_RptViewer.BackColor = System.Drawing.Color.Transparent
        Me.tlsp_Patient_RptViewer.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tlsp_Patient_RptViewer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlsp_Patient_RptViewer.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlsp_Patient_RptViewer.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tlsp_Patient_RptViewer.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ts_btnSave, Me.ToolStripButton1, Me.ts_btnClose})
        Me.tlsp_Patient_RptViewer.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.tlsp_Patient_RptViewer.Location = New System.Drawing.Point(0, 0)
        Me.tlsp_Patient_RptViewer.Name = "tlsp_Patient_RptViewer"
        Me.tlsp_Patient_RptViewer.Size = New System.Drawing.Size(504, 53)
        Me.tlsp_Patient_RptViewer.TabIndex = 0
        Me.tlsp_Patient_RptViewer.Text = "toolStrip1"
        '
        'ts_btnSave
        '
        Me.ts_btnSave.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnSave.Image = CType(resources.GetObject("ts_btnSave.Image"), System.Drawing.Image)
        Me.ts_btnSave.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnSave.Name = "ts_btnSave"
        Me.ts_btnSave.Size = New System.Drawing.Size(41, 50)
        Me.ts_btnSave.Tag = "Print"
        Me.ts_btnSave.Text = "&Print"
        Me.ts_btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ToolStripButton1
        '
        Me.ToolStripButton1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStripButton1.Image = CType(resources.GetObject("ToolStripButton1.Image"), System.Drawing.Image)
        Me.ToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton1.Name = "ToolStripButton1"
        Me.ToolStripButton1.Size = New System.Drawing.Size(87, 50)
        Me.ToolStripButton1.Tag = "PrintPrView"
        Me.ToolStripButton1.Text = "&Print PrView"
        Me.ToolStripButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.ToolStripButton1.ToolTipText = "Print PreView"
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
        'frmPatient_RptViewer
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(504, 440)
        Me.Controls.Add(Me.pnlPatient)
        Me.Controls.Add(Me.pnl_tlsp)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmPatient_RptViewer"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Patient Demographic Criteria"
        Me.pnlPatient.ResumeLayout(False)
        Me.pnlChkBox.ResumeLayout(False)
        Me.pnlChkBox.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        Me.pnl_tlsp.ResumeLayout(False)
        Me.pnl_tlsp.PerformLayout()
        Me.tlsp_Patient_RptViewer.ResumeLayout(False)
        Me.tlsp_Patient_RptViewer.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

#End Region

#Region " Private Variables "
    Private mPatientDemographic As Boolean = False
    Private mInsurance As Boolean = False
    Private mHistory As Boolean = False
    Private mPrescription As Boolean = False
    Private mExam As Boolean = False
    Private mPatientEducation As Boolean = False
    Private mROS As Boolean = False
    Private mMessage As Boolean = False
    Private mVitals As Boolean = False
    Private mProblemList As Boolean = False
    Private mTreamtment As Boolean = False
    Private mDiagnosis As Boolean = False
    Private mAllCriteria As Boolean = False
    Private mFromDate As DateTime = Now
    Private mToDate As DateTime = Now
    Private mLoadFromOtherForm As Boolean = False
    ''Sandip Darade 200903010
    Private mPatientExam As Boolean = False
    Private mMedication As Boolean = False
    Private mLabs As Boolean = False
    Private mOrders As Boolean = False
    Private mDMS As Boolean = False
    Private mPatientConsent As Boolean = False
    Private mFlowsheet As Boolean = False
    Private mImmunization As Boolean = False
#End Region

#Region " Public Properties "
    Public Property PatientDemographic() As Boolean
        Get
            Return mPatientDemographic
        End Get
        Set(ByVal value As Boolean)
            mPatientDemographic = value
        End Set
    End Property
    Public Property Insurance() As Boolean
        Get
            Return mInsurance
        End Get
        Set(ByVal value As Boolean)
            mInsurance = value
        End Set
    End Property
    Public Property History() As Boolean
        Get
            Return mHistory
        End Get
        Set(ByVal value As Boolean)
            mHistory = value
        End Set
    End Property
    Public Property Prescription() As Boolean
        Get
            Return mPrescription
        End Get
        Set(ByVal value As Boolean)
            mPrescription = value
        End Set
    End Property
    Public Property Exam() As Boolean
        Get
            Return mExam
        End Get
        Set(ByVal value As Boolean)
            mExam = value
        End Set
    End Property
    Public Property PatientEducation() As Boolean
        Get
            Return mPatientEducation
        End Get
        Set(ByVal value As Boolean)
            mPatientEducation = value
        End Set
    End Property
    Public Property ROS() As Boolean
        Get
            Return mROS
        End Get
        Set(ByVal value As Boolean)
            mROS = value
        End Set
    End Property
    Public Property Message() As Boolean
        Get
            Return mMessage
        End Get
        Set(ByVal value As Boolean)
            mMessage = value
        End Set
    End Property
    Public Property Vitals() As Boolean
        Get
            Return mVitals
        End Get
        Set(ByVal value As Boolean)
            mVitals = value
        End Set
    End Property
    Public Property ProblemList() As Boolean
        Get
            Return mProblemList
        End Get
        Set(ByVal value As Boolean)
            mProblemList = value
        End Set
    End Property
    Public Property Treamtment() As Boolean
        Get
            Return mTreamtment
        End Get
        Set(ByVal value As Boolean)
            mTreamtment = value
        End Set
    End Property
    Public Property Diagnosis() As Boolean
        Get
            Return mDiagnosis
        End Get
        Set(ByVal value As Boolean)
            mDiagnosis = value
        End Set
    End Property
    Public Property AllCriteria() As Boolean
        Get
            Return mAllCriteria
        End Get
        Set(ByVal value As Boolean)
            mAllCriteria = value
        End Set
    End Property
    Public Property FromDate() As DateTime
        Get
            Return mFromDate
        End Get
        Set(ByVal value As DateTime)
            mFromDate = value
        End Set
    End Property
    Public Property ToDate() As DateTime
        Get
            Return mToDate
        End Get
        Set(ByVal value As DateTime)
            mToDate = value
        End Set
    End Property
    ''Sandip Darade 20090310
    Public Property PatientExam() As Boolean
        Get
            Return mPatientExam
        End Get
        Set(ByVal value As Boolean)
            mPatientExam = value
        End Set
    End Property
    Public Property Medication() As Boolean
        Get
            Return mMedication
        End Get
        Set(ByVal value As Boolean)
            mMedication = value
        End Set
    End Property
    Public Property Labs() As Boolean
        Get
            Return mLabs
        End Get
        Set(ByVal value As Boolean)
            mLabs = value
        End Set
    End Property
    Public Property Orders() As Boolean
        Get
            Return mOrders
        End Get
        Set(ByVal value As Boolean)
            mOrders = value
        End Set
    End Property
    Public Property DMS() As Boolean
        Get
            Return mDMS
        End Get
        Set(ByVal value As Boolean)
            mDMS = value
        End Set
    End Property
    Public Property PatientConsent() As Boolean
        Get
            Return mPatientConsent
        End Get
        Set(ByVal value As Boolean)
            mPatientConsent = value
        End Set
    End Property
    Public Property Flowsheet() As Boolean
        Get
            Return mFlowsheet
        End Get
        Set(ByVal value As Boolean)
            mFlowsheet = value
        End Set
    End Property
    Public Property Immunization() As Boolean
        Get
            Return mImmunization
        End Get
        Set(ByVal value As Boolean)
            mImmunization = value
        End Set
    End Property
   
   
#End Region

    Public PatientID As Long
    Public strDMSScan_PatientCode As String = ""
    Public strDMSScan_PatientFirstName As String = ""
    Public strDMSScan_PatientLastName As String = ""
    Public blnFormLoad As Boolean = False
    Public PatFlag As Boolean = False

#Region " C1 Constants "
    Private Const COL_PATIENTSEL = 0
    Private Const COL_PATIENTID = 1
    Private Const COL_PATIENTCODE = 2
    Private Const COL_PATIENTFIRST = 3
    Private Const COL_PATIENTMIDDLE = 4
    Private Const COL_PATIENTLAST = 5
    Private Const COL_PATIENTSSN = 6
    Private Const COL_PATIENTDOB = 7
    Private Const COL_COUNT = 8
#End Region

    'Private orpt As ReportDocument


    ''Design the C1Flex grid
    'Private Sub DesignGrid()
    '    With c1PatientList
    '        .Clear(C1.Win.C1FlexGrid.ClearFlags.Content)
    '        .Rows.Count = 1
    '        .Rows.Fixed = 1
    '        .Cols.Count = COL_COUNT
    '        .AllowEditing = False
    '        .SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row

    '        .Cols(COL_PATIENTSEL).DataType = GetType(Boolean)
    '        .Cols(COL_PATIENTID).DataType = GetType(Integer)
    '        .Cols(COL_PATIENTID).TextAlign = TextAlignEnum.LeftCenter
    '        .SetData(0, COL_PATIENTID, "ID")
    '        .SetData(0, COL_PATIENTSEL, "Select")
    '        .SetData(0, COL_PATIENTCODE, "Code")
    '        .SetData(0, COL_PATIENTLAST, "Last Name")
    '        .SetData(0, COL_PATIENTMIDDLE, "Middle Name")
    '        .SetData(0, COL_PATIENTFIRST, "First Name")
    '        .SetData(0, COL_PATIENTSSN, "SSN")
    '        .SetData(0, COL_PATIENTDOB, "Date Of Birth")
    '        .Rows(0).Height = 22
    '    End With
    'End Sub

    'Resize grid Function
    'Private Sub ResizeGrid()
    '    With c1PatientList
    '        Dim nFactor As Single
    '        nFactor = .Width - 20
    '        .Cols(COL_PATIENTID).Width = 0
    '        .Cols(COL_PATIENTSEL).Width = 47
    '        .Cols(COL_PATIENTCODE).Width = (nFactor / 6)
    '        .Cols(COL_PATIENTLAST).Width = (nFactor / 6)
    '        .Cols(COL_PATIENTMIDDLE).Width = (nFactor / 6)
    '        .Cols(COL_PATIENTFIRST).Width = (nFactor / 6)
    '        .Cols(COL_PATIENTSSN).Width = (nFactor / 6)
    '        .Cols(COL_PATIENTDOB).Width = (nFactor / 6)
    '        .Rows(0).Height = 20
    '    End With
    'End Sub

    'Form Load event
    Private Sub frmHL7_Patient_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            ' DesignGrid()
            ' ResizeGrid()
            'Fill_PatientList()
            blnFormLoad = False
            'FetchMinDate()
            chkPatientDemo.Checked = True
            dtpFromDate.Value = System.DateTime.Now.Date.AddYears(-1)
            dtpToDate.Value = System.DateTime.Now.Date

            If mLoadFromOtherForm Then
                SelectLoadCriteria()
            End If
            Try
                gloPatient.gloPatient.GetWindowTitle(Me, _patientID, GetConnectionString(), gstrMessageBoxCaption)
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            End Try
        Catch oError As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.Reports, gloAuditTrail.ActivityType.Open, oError.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show("Unable to Load Criteria form", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

    End Sub


    'Close the Form
    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Close()
    End Sub

    'Fill PatientList in Grid 
    'Private Sub Fill_PatientList()
    '    With c1PatientList

    '        'Fill Data
    '        Dim oDB As New gloStream.gloDataBase.gloDataBase
    '        Dim sqladapter As New SqlClient.SqlDataAdapter
    '        Dim objDataSet As System.Data.DataSet

    '        oDB.Connect(GetConnectionString)
    '        'Fax List Type
    '        'oDB.DBParameters.Add("@Type", _Category, ParameterDirection.Input, SqlDbType.VarChar)

    '        objDataSet = oDB.ReadQueryRecordAsDataSet("sp_DMS_FillPatients")

    '        SetGridStyle(objDataSet.Tables(0))
    '        'oDataReader = oDB.ReadQueryRecordAsDataSet("sp_DMS_FillPatients")
    '        'If oDataReader.HasRows = True Then
    '        '    While oDataReader.Read
    '        '        .Rows.Add()
    '        '        .Rows(.Rows.Count - 1).Height = 22
    '        '        .SetData(.Rows.Count - 1, COL_PATIENTID, Trim(oDataReader.Item(0) & ""))
    '        '        .SetData(.Rows.Count - 1, COL_PATIENTCODE, Trim(oDataReader.Item(1) & ""))
    '        '        .SetData(.Rows.Count - 1, COL_PATIENTLAST, Trim(oDataReader.Item(2) & ""))
    '        '        .SetData(.Rows.Count - 1, COL_PATIENTMIDDLE, Trim(oDataReader.Item(3) & ""))
    '        '        .SetData(.Rows.Count - 1, COL_PATIENTFIRST, Trim(oDataReader.Item(4) & ""))
    '        '        .SetData(.Rows.Count - 1, COL_PATIENTSSN, Trim(oDataReader.Item(5) & ""))
    '        '        .SetData(.Rows.Count - 1, COL_PATIENTDOB, Trim(oDataReader.Item(6) & ""))
    '        '    End While
    '        'End If
    '        oDB.Disconnect()

    '        oDB = Nothing
    '        'oDataReader = Nothing
    '    End With
    'End Sub

    'Private Sub c1PatientList_EnterCell(ByVal sender As Object, ByVal e As System.EventArgs) Handles c1PatientList.EnterCell
    '    If blnFormLoad = False Then
    '        lblSearchOn.Text = ""
    '        lblPatientCode.Text = ""
    '        lblPatientDetails.Text = ""

    '        With c1PatientList
    '            'Set Label
    '            If .Rows.Count > 0 Then
    '                If .ColSel <> -1 Then
    '                    lblSearchOn.Text = Trim(.GetData(0, .ColSel))
    '                End If
    '            End If

    '            If .RowSel > 0 Then
    '                lblPatientCode.Text = Trim(.GetData(.RowSel, 1))
    '                lblPatientDetails.Text = Trim(.GetData(.RowSel, 2)) & " " & Trim(.GetData(.RowSel, 3)) & " " & Trim(.GetData(.RowSel, 4))

    '                ''PatientID = Trim(.GetData(.RowSel, COL_PATIENTID))
    '                'If .GetCellCheck(.RowSel, COL_PATIENTSEL) = CheckEnum.Unchecked Then
    '                '    .SetCellCheck(.RowSel, COL_PATIENTSEL, CheckEnum.Checked)
    '                'Else
    '                '    .SetCellCheck(.RowSel, COL_PATIENTSEL, CheckEnum.Unchecked)
    '                'End If

    '                strDMSScan_PatientCode = Trim(.GetData(.RowSel, 1))
    '                strDMSScan_PatientFirstName = Trim(.GetData(.RowSel, 2))
    '                strDMSScan_PatientLastName = Trim(.GetData(.RowSel, 4))
    '            End If
    '        End With
    '    End If
    'End Sub

    'Private Sub txtSearchDocument_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtSearchDocument.KeyUp
    '    If e.KeyCode = Keys.Down Or e.KeyCode = Keys.Enter Or e.KeyCode = Keys.Return Then
    '        btnSearch_Click(sender, e)
    '    End If
    'End Sub

    ''Search the selected text in grid
    'Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
    '    Dim sSearchDoc As String = UCase(Trim(txtSearchDocument.Text)) & "*"
    '    Dim i As Integer
    '    c1PatientList.Row = -1

    '    If sSearchDoc <> "" Then
    '        With c1PatientList
    '            For i = 0 To .Rows.Count - 1
    '                If UCase(Trim(.GetData(i, .ColSel))) Like sSearchDoc Then
    '                    .Row = i
    '                    .Focus()
    '                    Exit Sub
    '                End If
    '            Next
    '        End With
    '    End If
    '    sSearchDoc = Nothing
    '    i = Nothing
    'End Sub




    'Private Function IsSettings() As Boolean
    '    Dim regKey As RegistryKey
    '    Try

    '        If IsNothing(Registry.LocalMachine.OpenSubKey("Software\gloEMR")) = True Then
    '            Return False
    '        End If

    '        regKey = Registry.LocalMachine.OpenSubKey("Software\gloEMR", True)
    '        If IsNothing(regKey.GetValue("SQLServer")) = True Then
    '            regKey.Close()
    '            Return False
    '        End If
    '        If IsNothing(regKey.GetValue("Database")) = True Then
    '            regKey.Close()
    '            Return False
    '        End If

    '        gstrSQLServerName = regKey.GetValue("SQLServer")

    '        gstrDatabaseName = regKey.GetValue("Database")

    '        regKey.Close()

    '        If gstrSQLServerName = "" Or gstrDatabaseName = "" Then
    '            Return False
    '        Else
    '            Return True
    '        End If

    '    Catch ex As Exception
    '        MessageBox.Show("Error Message :" & ex.Message, "Hl7 Interfacce Applicaiton", MessageBoxButtons.OK)
    '    End Try
    'End Function

    'Private Sub frmPatient_RptViewer_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Resize
    '    'ResizeGrid()
    'End Sub

    Private Sub PrintPatient()
        'With c1PatientList
        '    For i As Int16 = 1 To .Rows.Count - 1
        '        If .GetCellCheck(i, COL_PATIENTSEL) = CheckEnum.Checked Then
        '            PatientID = PatientID & .GetData(i, COL_PATIENTID) & ","
        '        End If
        '    Next
        'End With
        Try
            'Dim objchkbox As CheckBox
            Dim objControl As Control
            Dim count As Int16
            For Each objControl In pnlChkBox.Controls
                If TypeOf objControl Is CheckBox Then
                    If CType(objControl, CheckBox).Checked = True Then
                        count = count + 1
                    End If
                End If
            Next
            If count = 0 Then
                MessageBox.Show("Please Select at least one item", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If
            PrintReport()
            ''Sandip Darade 
            ''To invoke frmPrintFAXExam  shoeing exams of same dates that of rport form
            If (chkPatientExams.Checked = True) Then
                Dim ofrm As New frmPrintFAXExam(PatientID)
                ofrm.FromDate = dtpFromDate.Value
                ofrm.ToDate = dtpToDate.Value
                ofrm.IsfromRptviewer = True
                ofrm.ShowDialog(IIf(IsNothing(ofrm.Parent), Me, ofrm.Parent))
                ofrm.Dispose()
                ofrm = Nothing
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.Reports, gloAuditTrail.ActivityType.Print, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)

            MessageBox.Show("Unable to Load Report", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub PrintReport()
        Try

            Dim _dmsServerName As String = gloEMRGeneralLibrary.gloGeneral.clsgeneral.sDmsServerName
            Dim _dmsDatabaseName As String = gloEMRGeneralLibrary.gloGeneral.clsgeneral.sDmsDatabaseName

            If (_dmsServerName = "" Or _dmsDatabaseName = "") Then
                MessageBox.Show("Please check DMS database settings.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            Dim orpt As ReportDocument = New ReportDocument
            orpt.Load(Application.StartupPath & "\Reports\rptDemographics.rpt")

            MapDatabaseInfo(orpt)

            Dim prm As ParameterValues
            Dim discreteval As ParameterDiscreteValue

            prm = orpt.DataDefinition.ParameterFields.Item(0).CurrentValues()
            prm.Clear()
            discreteval = New ParameterDiscreteValue
            discreteval.Value = chkHistory.Checked
            prm.Add(discreteval)
            orpt.DataDefinition.ParameterFields.Item(0).ApplyCurrentValues(prm)

            prm = orpt.DataDefinition.ParameterFields.Item(1).CurrentValues()
            prm.Clear()
            discreteval = New ParameterDiscreteValue
            discreteval.Value = chkHistory.Checked
            prm.Add(discreteval)
            orpt.DataDefinition.ParameterFields.Item(1).ApplyCurrentValues(prm)

            prm = orpt.DataDefinition.ParameterFields.Item(2).CurrentValues()
            prm.Clear()
            discreteval = New ParameterDiscreteValue
            discreteval.Value = chkPrescription.Checked
            prm.Add(discreteval)
            orpt.DataDefinition.ParameterFields.Item(2).ApplyCurrentValues(prm)

            prm = orpt.DataDefinition.ParameterFields.Item(3).CurrentValues()
            prm.Clear()
            discreteval = New ParameterDiscreteValue
            discreteval.Value = chkROS.Checked
            prm.Add(discreteval)
            orpt.DataDefinition.ParameterFields.Item(3).ApplyCurrentValues(prm)

            prm = orpt.DataDefinition.ParameterFields.Item(4).CurrentValues()
            prm.Clear()
            discreteval = New ParameterDiscreteValue
            discreteval.Value = chkEducation.Checked
            prm.Add(discreteval)
            orpt.DataDefinition.ParameterFields.Item(4).ApplyCurrentValues(prm)

            prm = orpt.DataDefinition.ParameterFields.Item(5).CurrentValues()
            prm.Clear()
            discreteval = New ParameterDiscreteValue
            discreteval.Value = chkMessages.Checked
            prm.Add(discreteval)
            orpt.DataDefinition.ParameterFields.Item(5).ApplyCurrentValues(prm)

            prm = orpt.DataDefinition.ParameterFields.Item(6).CurrentValues()
            prm.Clear()
            discreteval = New ParameterDiscreteValue
            discreteval.Value = chkExam.Checked
            prm.Add(discreteval)
            orpt.DataDefinition.ParameterFields.Item(6).ApplyCurrentValues(prm)

            prm = orpt.DataDefinition.ParameterFields.Item(7).CurrentValues()
            prm.Clear()
            discreteval = New ParameterDiscreteValue
            discreteval.Value = chkVitals.Checked
            prm.Add(discreteval)
            orpt.DataDefinition.ParameterFields.Item(7).ApplyCurrentValues(prm)

            prm = orpt.DataDefinition.ParameterFields.Item(8).CurrentValues()
            prm.Clear()
            discreteval = New ParameterDiscreteValue
            discreteval.Value = chkInsurance.Checked
            prm.Add(discreteval)
            orpt.DataDefinition.ParameterFields.Item(8).ApplyCurrentValues(prm)

            prm = orpt.DataDefinition.ParameterFields.Item(9).CurrentValues()
            prm.Clear()
            discreteval = New ParameterDiscreteValue
            discreteval.Value = chkPatientDemo.Checked
            prm.Add(discreteval)
            orpt.DataDefinition.ParameterFields.Item(9).ApplyCurrentValues(prm)

            prm = orpt.DataDefinition.ParameterFields.Item(10).CurrentValues()
            prm.Clear()
            discreteval = New ParameterDiscreteValue
            discreteval.Value = chkProblemList.Checked
            prm.Add(discreteval)
            orpt.DataDefinition.ParameterFields.Item(10).ApplyCurrentValues(prm)

            prm = orpt.DataDefinition.ParameterFields.Item(11).CurrentValues()
            prm.Clear()
            discreteval = New ParameterDiscreteValue
            discreteval.Value = dtpFromDate.Value.Date
            prm.Add(discreteval)
            orpt.DataDefinition.ParameterFields.Item(11).ApplyCurrentValues(prm)

            prm = orpt.DataDefinition.ParameterFields.Item(12).CurrentValues()
            prm.Clear()
            discreteval = New ParameterDiscreteValue
            discreteval.Value = dtpToDate.Value.Date
            prm.Add(discreteval)
            orpt.DataDefinition.ParameterFields.Item(12).ApplyCurrentValues(prm)

            prm = orpt.DataDefinition.ParameterFields.Item(13).CurrentValues()
            prm.Clear()
            discreteval = New ParameterDiscreteValue
            'Line commented and modified by dipak 20100907 for case UC5070.003: replace gnPatientID by local variable .
            'discreteval.Value = gnPatientID.ToString
            discreteval.Value = _patientID.ToString
            'end modification

            prm.Add(discreteval)
            orpt.DataDefinition.ParameterFields.Item(13).ApplyCurrentValues(prm)



            prm = orpt.DataDefinition.ParameterFields.Item(14).CurrentValues()
            prm.Clear()
            discreteval = New ParameterDiscreteValue
            discreteval.Value = chkConsent.Checked
            prm.Add(discreteval)
            orpt.DataDefinition.ParameterFields.Item(14).ApplyCurrentValues(prm)

            prm = orpt.DataDefinition.ParameterFields.Item(15).CurrentValues()
            prm.Clear()
            discreteval = New ParameterDiscreteValue
            discreteval.Value = chkFlowsheet.Checked
            prm.Add(discreteval)
            orpt.DataDefinition.ParameterFields.Item(15).ApplyCurrentValues(prm)

            prm = orpt.DataDefinition.ParameterFields.Item(16).CurrentValues()
            prm.Clear()
            discreteval = New ParameterDiscreteValue
            discreteval.Value = chkOrders.Checked
            prm.Add(discreteval)
            orpt.DataDefinition.ParameterFields.Item(16).ApplyCurrentValues(prm)


            orpt.PrintToPrinter(1, False, 0, 0)
            orpt.Close()
            orpt.Dispose()
            prm = Nothing
            discreteval = Nothing

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub MapDatabaseInfo(ByVal rpt As ReportDocument)

        Dim crConnectionInfo As New ConnectionInfo
        Try


            With crConnectionInfo
                .ServerName = gstrSQLServerName

                'If you are connecting to Oracle there is no 
                'DatabaseName. Use an empty string. 
                'For example, .DatabaseName = "" 

                .DatabaseName = gstrDatabaseName

                '.UserID = "Your User ID"
                '.Password = "Your Password"
                .IntegratedSecurity = True
            End With

            SetDBLogonForReport(crConnectionInfo, rpt)
            SetDBLogonForSubreports(crConnectionInfo, rpt)

            'MapTableInfo(crConnectionInfo, rpt)
            'Dim objsubrpt As SubreportObject
            'Dim objrpt As ReportDocument

            'objsubrpt = rpt.ReportDefinition.Sections.Item(2).ReportObjects(0)
            'objrpt = New ReportDocument
            'objrpt = rpt.OpenSubreport(objsubrpt.SubreportName)
            'MapTableInfo(crConnectionInfo, objrpt)

            'objsubrpt = rpt.ReportDefinition.Sections.Item(3).ReportObjects(0)
            'objrpt = New ReportDocument
            'objrpt = rpt.OpenSubreport(objsubrpt.SubreportName)
            'MapTableInfo(crConnectionInfo, objrpt)

            'Dim i As Integer
            'For i = 5 To 12
            '    objsubrpt = rpt.ReportDefinition.Sections.Item(i).ReportObjects(0)
            '    objrpt = New ReportDocument
            '    objrpt = rpt.OpenSubreport(objsubrpt.SubreportName)
            '    MapTableInfo(crConnectionInfo, objrpt)
            'Next

            'objsubrpt = rpt.ReportDefinition.Sections.Item(14).ReportObjects(0)
            'objrpt = New ReportDocument
            'objrpt = rpt.OpenSubreport(objsubrpt.SubreportName)
            'MapTableInfo(crConnectionInfo, objrpt)
            crConnectionInfo = Nothing

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SetDBLogonForReport(ByVal connectionInfo As ConnectionInfo, ByVal reportDocument As ReportDocument)

        Dim tables As Tables = reportDocument.Database.Tables

        Try


            For Each table As CrystalDecisions.CrystalReports.Engine.Table In tables
                Dim tableLogonInfo As TableLogOnInfo = table.LogOnInfo
                tableLogonInfo.ConnectionInfo = connectionInfo
                tableLogonInfo.TableName = table.Name
                ' Added to try and make other databases work. 
                table.ApplyLogOnInfo(tableLogonInfo)
                table.Location = table.Name
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SetDBLogonForSubreports(ByVal connectionInfo As ConnectionInfo, ByVal reportDocument As ReportDocument)


        Dim sections As Sections = reportDocument.ReportDefinition.Sections
        Try


            For Each section As Section In sections
                Dim reportObjects As ReportObjects = section.ReportObjects
                For Each reportObject As ReportObject In reportObjects
                    If reportObject.Kind = ReportObjectKind.SubreportObject Then
                        Dim subreportObject As SubreportObject = DirectCast(reportObject, SubreportObject)
                        Dim subreportDocument As ReportDocument = subreportObject.OpenSubreport(subreportObject.SubreportName)
                        SetDBLogonForReport(connectionInfo, subreportDocument)
                    End If
                Next
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub MapTableInfo(ByVal crConnectionInfo As ConnectionInfo, ByVal rpt As ReportDocument)
        '  Dim crtableLogoninfos As New TableLogOnInfos
        Dim crtableLogoninfo As TableLogOnInfo

        Dim CrTables As Tables
        Dim CrTable As Table
        'Dim TableCounter
        'This code works for both user tables and stored 
        'procedures. Set the CrTables to the Tables collection 
        'of the report 
        Try

            CrTables = rpt.Database.Tables

            'Loop through each table in the report and apply the 
            'LogonInfo information 

            For Each CrTable In CrTables
                crtableLogoninfo = CrTable.LogOnInfo
                crtableLogoninfo.ConnectionInfo = crConnectionInfo
                CrTable.ApplyLogOnInfo(crtableLogoninfo)

                'If your DatabaseName is changing at runtime, specify 
                'the table location. 
                'For example, when you are reporting off of a 
                'Northwind database on SQL server you 
                'should have the following line of code: 

                CrTable.Location = gstrDatabaseName & ".dbo." & CrTable.Name
            Next

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub Priview_Patient()
        'With c1PatientList
        '    PatientID = 0
        '    PatientID = CLng(c1PatientList.GetData(c1PatientList.RowSel, 0))
        'End With


        Try

            Dim _dmsServerName As String = gloEMRGeneralLibrary.gloGeneral.clsgeneral.sDmsServerName
            Dim _dmsDatabaseName As String = gloEMRGeneralLibrary.gloGeneral.clsgeneral.sDmsDatabaseName

            If (_dmsServerName = "" Or _dmsDatabaseName = "") Then
                MessageBox.Show("Please check DMS database settings.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If


            Me.Cursor = Cursors.WaitCursor
            Dim objchkbox As Control
            Dim count As Int16
            For Each objchkbox In pnlChkBox.Controls
                If TypeOf (objchkbox) Is CheckBox Then
                    If CType(objchkbox, CheckBox).Checked = True Then
                        count = count + 1
                    End If
                End If
            Next
            If count = 0 Then
                MessageBox.Show("Please Select at least one item", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If
            Dim frm As frmViewMainPatientDemographics
            'Line commented and modified by dipak 20100907 for case UC5070.003: replace gnPatientID by local variable .
            'frm = New frmViewMainPatientDemographics(gnPatientID, dtpFromDate.Value.Date, dtpToDate.Value.Date)
            frm = New frmViewMainPatientDemographics(_patientID, dtpFromDate.Value.Date, dtpToDate.Value.Date)
            'end modification 
            frm.HistoryFlag = chkHistory.Checked
            'frm.MedicationFlag = chkMedication.Checked
            frm.PrescriptionFlag = chkPrescription.Checked
            frm.DiagnosisFlag = chkDiagnosis.Checked
            frm.TreatmentFlag = chkTreatment.Checked
            frm.ROSFlag = chkROS.Checked
            frm.PatientEducationFlag = chkEducation.Checked
            frm.MessagesFlag = chkMessages.Checked
            frm.ExamFlag = chkExam.Checked
            frm.VitalFlag = chkVitals.Checked
            frm.InsuranceFlag = chkInsurance.Checked
            frm.DemographicsFlag = chkPatientDemo.Checked
            frm.ProblemListFlag = chkProblemList.Checked
            frm.ConsentFlag = chkConsent.Checked
            frm.FlowSheetFlag = chkFlowsheet.Checked
            frm.OrdersFlag = chkOrders.Checked


            frm.Show()
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.Reports, gloAuditTrail.ActivityType.Print, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)

            MessageBox.Show("Unable to load Report", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.Cursor = Cursors.Default
        End Try

    End Sub

    'Private Sub FetchMinDate()
    '    Dim Cmd As System.Data.SqlClient.SqlCommand
    '    'Dim dreader As System.Data.SqlClient.SqlDataReader
    '    Dim sqlconn As String
    '    Dim conn As System.Data.SqlClient.SqlConnection
    '    Try
    '        sqlconn = GetConnectionString()
    '        conn = New System.Data.SqlClient.SqlConnection(sqlconn)

    '        Cmd = New System.Data.SqlClient.SqlCommand("select min(dtvisitdate) from visits where npatientid=" & gnPatientID & "", conn)
    '        Cmd.CommandType = CommandType.Text

    '        If Conn.State = ConnectionState.Open Then
    '            Conn.Close()
    '        End If
    '        conn.Open()
    '        Dim mindate As DateTime
    '        mindate = Cmd.ExecuteScalar
    '        If Not IsNothing(mindate) Then
    '            dtpFromDate.Value = mindate.Date
    '        End If
    '        'If Not IsNothing(dreader) Then
    '        '    While dreader.Read
    '        '        dtpFromDate.Value = dreader.Item(0)
    '        '    End While
    '        'End If
    '        'dreader.Close()
    '        conn.Close()
    '    Catch ex As Exception
    '        If conn.State = ConnectionState.Open Then
    '            conn.Close()
    '        End If
    '    End Try
    'End Sub
    'Private Sub SetGridStyle(ByVal dt As DataTable)
    '    With c1PatientList
    '        .Clear(ClearFlags.All)
    '        .Visible = False
    '        '  If dt.Rows.Count > 0 Then
    '        .DataSource = dt.DefaultView
    '        '    End If

    '        .Cols.Count = 7

    '        .AllowEditing = False
    '        .Width = .Width - 20

    '        ''''' NEW '''' Select, ExamID,DOS, ExamName, VisitID, DOB, ProviderID, ProviderName , bIsFinished

    '        .Cols(0).Width = .Width * 0
    '        .SetData(0, 0, "PatientID")

    '        '.SetData(.Rows.Count - 1, COL_PATIENTID, Trim(oDataReader.Item(0) & ""))
    '        '        .SetData(.Rows.Count - 1, COL_PATIENTCODE, Trim(oDataReader.Item(1) & ""))
    '        '        .SetData(.Rows.Count - 1, COL_PATIENTLAST, Trim(oDataReader.Item(2) & ""))
    '        '        .SetData(.Rows.Count - 1, COL_PATIENTMIDDLE, Trim(oDataReader.Item(3) & ""))
    '        '        .SetData(.Rows.Count - 1, COL_PATIENTFIRST, Trim(oDataReader.Item(4) & ""))
    '        '        .SetData(.Rows.Count - 1, COL_PATIENTSSN, Trim(oDataReader.Item(5) & ""))
    '        '        .SetData(.Rows.Count - 1, COL_PATIENTDOB, Trim(oDataReader.Item(6) & ""))

    '        .Cols(1).Width = .Width * 0.14
    '        .SetData(0, 1, "Patient Code")
    '        .Cols(1).AllowEditing = False

    '        .Cols(2).Width = .Width * 0.2
    '        .SetData(0, 2, "First Name")
    '        .Cols(2).AllowEditing = False

    '        .Cols(3).Width = .Width * 0.05
    '        .SetData(0, 3, "Middle Name")
    '        .Cols(3).AllowEditing = False


    '        .Cols(4).Width = .Width * 0.2
    '        .SetData(0, 4, "Last Name")
    '        .Cols(4).AllowEditing = False

    '        .Cols(5).Width = .Width * 0.2
    '        .SetData(0, 5, "SSN")
    '        .Cols(5).AllowEditing = False

    '        .Cols(6).Width = .Width * 0.2
    '        .SetData(0, 6, "Date of Birth")
    '        .Cols(6).AllowEditing = False

    '        .Visible = True
    '        .Refresh()
    '    End With

    'End Sub

    'Private Sub c1PatientList_CellChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles c1PatientList.CellChanged

    'End Sub

    'Private Sub c1PatientList_EnterCell(ByVal sender As Object, ByVal e As System.EventArgs) Handles c1PatientList.EnterCell
    '    Try
    '        With c1PatientList
    '            Select Case c1PatientList.ColSel
    '                Case 1
    '                    lblSearchOn.Text = "Patient Code"
    '                Case 2
    '                    lblSearchOn.Text = "First Name"
    '                Case 4
    '                    lblSearchOn.Text = "Last Name"
    '            End Select
    '        End With

    '    Catch objErr As Exception
    '        MessageBox.Show(objErr.ToString, "ICD9", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Sub
    'Private Sub txtSearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearchDocument.TextChanged
    '    Try
    '        Dim dvPatient As DataView
    '        With c1PatientList
    '            Me.Cursor = Cursors.WaitCursor
    '            dvPatient = CType(.DataSource(), DataView)

    '            If IsNothing(dvPatient) Then
    '                Me.Cursor = Cursors.Default
    '                Exit Sub
    '            End If

    '            .DataSource = dvPatient
    '            Dim strPatientSearchDetails As String
    '            If Trim(txtSearchDocument.Text) <> "" Then
    '                strPatientSearchDetails = Replace(txtSearchDocument.Text, "'", "''")
    '            Else
    '                strPatientSearchDetails = ""
    '            End If

    '            Select Case Trim(lblSearchOn.Text)
    '                Case "Patient Code"
    '                    If strPatientSearchDetails.StartsWith("%") = True Or strPatientSearchDetails.StartsWith("*") = True Then
    '                        dvPatient.RowFilter = dvPatient.Table.Columns(1).ColumnName & " Like '%" & strPatientSearchDetails & "%'"
    '                    Else
    '                        dvPatient.RowFilter = dvPatient.Table.Columns(1).ColumnName & " Like '" & strPatientSearchDetails & "%'"
    '                    End If
    '                Case "First Name"
    '                    If strPatientSearchDetails.StartsWith("%") = True Or strPatientSearchDetails.StartsWith("*") = True Then
    '                        dvPatient.RowFilter = dvPatient.Table.Columns(2).ColumnName & " Like '%" & strPatientSearchDetails & "%'"
    '                    Else
    '                        dvPatient.RowFilter = dvPatient.Table.Columns(2).ColumnName & " Like '" & strPatientSearchDetails & "%'"
    '                    End If
    '                Case "Last Name"
    '                    If strPatientSearchDetails.StartsWith("%") = True Or strPatientSearchDetails.StartsWith("*") = True Then
    '                        dvPatient.RowFilter = dvPatient.Table.Columns(4).ColumnName & " Like '%" & strPatientSearchDetails & "%'"
    '                    Else
    '                        dvPatient.RowFilter = dvPatient.Table.Columns(4).ColumnName & " Like '" & strPatientSearchDetails & "%'"
    '                    End If
    '            End Select
    '        End With

    '        Me.Cursor = Cursors.Default
    '    Catch objErr As Exception
    '        Me.Cursor = Cursors.Default
    '        MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Sub


    Private Sub ChkSelectAll_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkSelectAll.CheckedChanged
        Dim objchkbox As Control

        If ChkSelectAll.Checked = True Then
            For Each objchkbox In pnlChkBox.Controls
                If TypeOf (objchkbox) Is CheckBox Then
                    CType(objchkbox, CheckBox).Checked = True
                End If
            Next
        ElseIf ChkSelectAll.Checked = False Then
            For Each objchkbox In pnlChkBox.Controls
                If TypeOf (objchkbox) Is CheckBox Then
                    CType(objchkbox, CheckBox).Checked = False
                End If
            Next
        End If
    End Sub

    Private Sub SelectLoadCriteria()
        Try

      
            '' SUDHIR 20090305 '' CRITERIA SELECTION ''
            If mAllCriteria = True Then
                ChkSelectAll.Checked = True
            Else
                If mPatientDemographic = True Then
                    chkPatientDemo.Checked = True
                End If
                If mInsurance = True Then
                    chkInsurance.Checked = True
                End If
                If mHistory = True Then
                    chkHistory.Checked = True
                End If
                If mPrescription = True Then
                    chkPrescription.Checked = True
                End If
                If mExam = True Then
                    chkExam.Checked = True
                End If
                If mPatientEducation = True Then
                    chkEducation.Checked = True
                End If
                If mROS = True Then
                    chkROS.Checked = True
                End If
                If mMessage = True Then
                    chkMessages.Checked = True
                End If
                If mVitals = True Then
                    chkVitals.Checked = True
                End If
                If mProblemList = True Then
                    chkProblemList.Checked = True
                End If
                If mTreamtment = True Then
                    chkTreatment.Checked = True
                End If
                If mDiagnosis = True Then
                    chkDiagnosis.Checked = True
                End If
                If Not IsNothing(mFromDate) Then
                    dtpFromDate.Value = mFromDate
                    dtpFromDate.Enabled = False
                End If
                If Not IsNothing(mToDate) Then
                    dtpToDate.Value = mToDate
                    dtpToDate.Enabled = False
                End If

                ''Sandip Darade 20090310
                ''''
                If mPatientExam = True Then
                    chkPatientExams.Checked = True
                End If
                If mMedication = True Then
                    chkMedication.Checked = True
                End If
                If mLabs = True Then
                    chkLabs.Checked = True
                End If
                If mOrders = True Then
                    chkOrders.Checked = True
                End If
                If mDMS = True Then
                    chkDMS.Checked = True
                End If
                If mPatientConsent = True Then
                    chkConsent.Checked = True
                End If
                If mFlowsheet = True Then
                    chkFlowsheet.Checked = True
                End If
                If mImmunization = True Then
                    chkImmunization.Checked = True
                End If
                ''''
            End If
            '' END SUDHIR ''
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub tlsp_Patient_RptViewer_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tlsp_Patient_RptViewer.ItemClicked
        Try
            Select Case e.ClickedItem.Tag
                Case "Print"
                    'PrintPatient()
                    PrintSSRSReport()
                Case "PrintPrView"
                    'Priview_Patient()
                    ShowSSRSReport()
                Case "Close"
                    Me.Close()

            End Select

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK)
        End Try
    End Sub


    Private Sub ShowSSRSReport()
        Cursor.Current = Cursors.WaitCursor

        Dim frmSSRS As New gloSSRSApplication.frmPatientDemographicsViewer


        'frmSSRS.Conn = gloPMGlobal.DatabaseConnectionString
        frmSSRS.reportName = "rptPatientDemographics"
        frmSSRS.reportTitle = Me.Text
        frmSSRS.Conn = GetConnectionString()
        frmSSRS.parameterName = "PatientID,Flag,FromDate,ToDate,Demog,Insurance,Education,Vitals,History,ProblemList,Prescription,Medication,Exam,Labs,Messages,PatientExams,ROS,Consent,DMS,Flowsheet,Immunization,Orders,User"
        frmSSRS.ParameterValue = _patientID.ToString() & "," & _
                                 "Main" & "," & _
                                 dtpFromDate.Value.ToString() & "," & _
                                 dtpToDate.Value.ToString() & "," & _
                                 chkPatientDemo.Checked.ToString() & "," & _
                                 chkInsurance.Checked.ToString() & "," & _
                                 chkEducation.Checked.ToString() & "," & _
                                 chkVitals.Checked.ToString() & "," & _
                                 chkHistory.Checked.ToString() & "," & _
                                 chkProblemList.Checked.ToString() & "," & _
                                 chkPrescription.Checked.ToString() & "," & _
                                 chkMedication.Checked.ToString() & "," & _
                                 chkExam.Checked.ToString() & "," & _
                                 chkLabs.Checked.ToString() & "," & _
                                 chkMessages.Checked.ToString() & "," & _
                                 chkPatientExams.Checked.ToString() & "," & _
                                 chkROS.Checked.ToString() & "," & _
                                 chkConsent.Checked.ToString() & "," & _
                                 chkDMS.Checked.ToString() & "," & _
                                 chkFlowsheet.Checked.ToString() & "," & _
                                 chkImmunization.Checked.ToString() & "," & _
                                 chkOrders.Checked.ToString() & "," & gstrLoginName


        frmSSRS.MdiParent = Me.ParentForm
        Cursor.Current = Cursors.Default
        frmSSRS.Show()
    End Sub

    Private Sub PrintSSRSReport()
        Dim clsPrntRpt As gloSSRSApplication.clsPrintReport
        Dim _MessageBoxCaption As String = String.Empty
        Dim _databaseConnectionString As String = String.Empty
        Dim _LoginName As String = String.Empty
        Dim gstrSQLServerName As String = String.Empty
        Dim gstrDatabaseName As String = String.Empty
        Dim gblnSQLAuthentication As String = String.Empty
        Dim gstrSQLUserEMR As String = String.Empty
        Dim gstrSQLPasswordEMR As String = String.Empty
        Dim gblnDefaultPrinter As Boolean = False
        Dim Con As SqlConnection = Nothing
        Try

            If appSettings("DataBaseConnectionString") IsNot Nothing Then
                If appSettings("DataBaseConnectionString") <> "" Then
                    _databaseConnectionString = Convert.ToString(appSettings("DataBaseConnectionString"))
                    Con = New SqlConnection(_databaseConnectionString)
                End If
            End If
            If (IsNothing(Con)) Then
                Con = New SqlConnection(_databaseConnectionString)
            End If
            If appSettings("UserName") IsNot Nothing Then
                If appSettings("UserName") <> "" Then
                    _LoginName = Convert.ToString(appSettings("UserName"))
                End If
            End If

            If appSettings("SQLServerName") IsNot Nothing Then
                If appSettings("SQLServerName") <> "" Then
                    gstrSQLServerName = Convert.ToString(appSettings("SQLServerName"))
                End If
            End If

            If appSettings("DatabaseName") IsNot Nothing Then
                If appSettings("DatabaseName") <> "" Then
                    gstrDatabaseName = Convert.ToString(appSettings("DatabaseName"))
                End If
            End If

            If appSettings("SQLLoginName") IsNot Nothing Then
                If appSettings("SQLLoginName") <> "" Then
                    gstrSQLUserEMR = Convert.ToString(appSettings("SQLLoginName"))
                End If
            End If

            If appSettings("SQLPassword") IsNot Nothing Then
                If appSettings("SQLPassword") <> "" Then
                    gstrSQLPasswordEMR = Convert.ToString(appSettings("SQLPassword"))
                End If
            End If

            If appSettings("DefaultPrinter") IsNot Nothing Then
                If appSettings("DefaultPrinter") <> "" Then
                    gblnDefaultPrinter = Not Convert.ToBoolean(appSettings("DefaultPrinter"))
                End If
            End If

            If appSettings("WindowAuthentication") IsNot Nothing Then
                If appSettings("WindowAuthentication") <> "" Then
                    gblnSQLAuthentication = Not Convert.ToBoolean(appSettings("WindowAuthentication"))
                End If
            End If

            Dim ParameterValue As String
            Dim ParameterName As String

            ParameterName = "PatientID,Flag,FromDate,ToDate,Demog,Insurance,Education,Vitals,History,ProblemList,Prescription,Medication,Exam,Labs,Messages,PatientExams,ROS,Consent,DMS,Flowsheet,Immunization,Orders,User"
            ParameterValue = _patientID.ToString() & "," & _
                                     "Main" & "," & _
                                     dtpFromDate.Value.ToString() & "," & _
                                     dtpToDate.Value.ToString() & "," & _
                                     chkPatientDemo.Checked.ToString() & "," & _
                                     chkInsurance.Checked.ToString() & "," & _
                                     chkEducation.Checked.ToString() & "," & _
                                     chkVitals.Checked.ToString() & "," & _
                                     chkHistory.Checked.ToString() & "," & _
                                     chkProblemList.Checked.ToString() & "," & _
                                     chkPrescription.Checked.ToString() & "," & _
                                     chkMedication.Checked.ToString() & "," & _
                                     chkExam.Checked.ToString() & "," & _
                                     chkLabs.Checked.ToString() & "," & _
                                     chkMessages.Checked.ToString() & "," & _
                                     chkPatientExams.Checked.ToString() & "," & _
                                     chkROS.Checked.ToString() & "," & _
                                     chkConsent.Checked.ToString() & "," & _
                                     chkDMS.Checked.ToString() & "," & _
                                     chkFlowsheet.Checked.ToString() & "," & _
                                     chkImmunization.Checked.ToString() & "," & _
                                     chkOrders.Checked.ToString() & "," & gstrLoginName


            clsPrntRpt = New gloSSRSApplication.clsPrintReport(gstrSQLServerName, gstrDatabaseName, gblnSQLAuthentication, gstrSQLUserEMR, gstrSQLPasswordEMR)
            clsPrntRpt.PrintReport("rptPatientDemographics", ParameterName, ParameterValue, gblnDefaultPrinter, "")
            clsPrntRpt = Nothing


        Catch ex As Exception
            MessageBox.Show(ex.ToString, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub





End Class
