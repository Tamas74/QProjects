Imports System
'Imports System.Windows
Imports System.Windows.Forms
Imports System.Data.SqlClient


Public Class frmVisit
    Inherits System.Windows.Forms.Form
    'Create instance of class
    Dim objclsVisit As New clsVisit
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
            Try

                If (IsNothing(dgData) = False) Then
                    dgData.TableStyles.Clear()
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(dgData)
                    dgData.Dispose()
                    dgData = Nothing
                End If
            Catch ex As Exception

            End Try
            Try
                If (IsNothing(dtpDate) = False) Then
                    Try
                        gloGlobal.cEventHelper.RemoveAllEventHandlers(dtpDate)
                    Catch ex As Exception

                    End Try


                    dtpDate.Dispose()
                    dtpDate = Nothing
                End If
            Catch
            End Try

            Try
                If (IsNothing(dtpTime) = False) Then
                    Try
                        gloGlobal.cEventHelper.RemoveAllEventHandlers(dtpTime)
                    Catch ex As Exception

                    End Try


                    dtpTime.Dispose()
                    dtpTime = Nothing
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

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents dtpTime As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents txtLastName As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtFirstName As System.Windows.Forms.TextBox
    Friend WithEvents txtPatientCode As System.Windows.Forms.TextBox
    Friend WithEvents cmbProvider As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmdCancel As System.Windows.Forms.Button
    Friend WithEvents cmdOK As System.Windows.Forms.Button
    Friend WithEvents lblVisitID As System.Windows.Forms.Label
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents pnlSearchPatient As System.Windows.Forms.Panel
    Friend WithEvents pnlCommands As System.Windows.Forms.Panel
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents dgData As clsDataGrid '  System.Windows.Forms.DataGrid
    Friend WithEvents pnlSearchCriteria As System.Windows.Forms.Panel
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
    Friend WithEvents optLastName As System.Windows.Forms.RadioButton
    Friend WithEvents optFirstName As System.Windows.Forms.RadioButton
    Friend WithEvents optPatientCode As System.Windows.Forms.RadioButton
    Friend WithEvents pnlMain As System.Windows.Forms.Panel
    Friend WithEvents pnlMainCommands As System.Windows.Forms.Panel
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents btnSearchPatient As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmVisit))
Me.Panel2 = New System.Windows.Forms.Panel
Me.pnlMain = New System.Windows.Forms.Panel
Me.btnSearchPatient = New System.Windows.Forms.Button
Me.Label6 = New System.Windows.Forms.Label
Me.lblVisitID = New System.Windows.Forms.Label
Me.dtpTime = New System.Windows.Forms.DateTimePicker
Me.dtpDate = New System.Windows.Forms.DateTimePicker
Me.txtLastName = New System.Windows.Forms.TextBox
Me.Label8 = New System.Windows.Forms.Label
Me.txtFirstName = New System.Windows.Forms.TextBox
Me.txtPatientCode = New System.Windows.Forms.TextBox
Me.cmbProvider = New System.Windows.Forms.ComboBox
Me.Label5 = New System.Windows.Forms.Label
Me.Label4 = New System.Windows.Forms.Label
Me.Label3 = New System.Windows.Forms.Label
Me.Label2 = New System.Windows.Forms.Label
Me.Label1 = New System.Windows.Forms.Label
Me.pnlSearchPatient = New System.Windows.Forms.Panel
Me.pnlCommands = New System.Windows.Forms.Panel
Me.Button1 = New System.Windows.Forms.Button
Me.Button2 = New System.Windows.Forms.Button
Me.dgData = New gloEMR.clsDataGrid
Me.pnlSearchCriteria = New System.Windows.Forms.Panel
Me.txtSearch = New System.Windows.Forms.TextBox
Me.optLastName = New System.Windows.Forms.RadioButton
Me.optFirstName = New System.Windows.Forms.RadioButton
Me.optPatientCode = New System.Windows.Forms.RadioButton
Me.PictureBox1 = New System.Windows.Forms.PictureBox
Me.pnlMainCommands = New System.Windows.Forms.Panel
Me.cmdCancel = New System.Windows.Forms.Button
Me.cmdOK = New System.Windows.Forms.Button
Me.Panel2.SuspendLayout
Me.pnlMain.SuspendLayout
Me.pnlSearchPatient.SuspendLayout
Me.pnlCommands.SuspendLayout
CType(Me.dgData,System.ComponentModel.ISupportInitialize).BeginInit
Me.pnlSearchCriteria.SuspendLayout
CType(Me.PictureBox1,System.ComponentModel.ISupportInitialize).BeginInit
Me.pnlMainCommands.SuspendLayout
Me.SuspendLayout
'
'Panel2
'
Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
Me.Panel2.Controls.Add(Me.pnlMain)
Me.Panel2.Controls.Add(Me.pnlSearchPatient)
Me.Panel2.Controls.Add(Me.PictureBox1)
Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
Me.Panel2.Location = New System.Drawing.Point(0, 0)
Me.Panel2.Name = "Panel2"
Me.Panel2.Size = New System.Drawing.Size(464, 318)
Me.Panel2.TabIndex = 3
'
'pnlMain
'
Me.pnlMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
Me.pnlMain.Controls.Add(Me.btnSearchPatient)
Me.pnlMain.Controls.Add(Me.Label6)
Me.pnlMain.Controls.Add(Me.lblVisitID)
Me.pnlMain.Controls.Add(Me.dtpTime)
Me.pnlMain.Controls.Add(Me.dtpDate)
Me.pnlMain.Controls.Add(Me.txtLastName)
Me.pnlMain.Controls.Add(Me.Label8)
Me.pnlMain.Controls.Add(Me.txtFirstName)
Me.pnlMain.Controls.Add(Me.txtPatientCode)
Me.pnlMain.Controls.Add(Me.cmbProvider)
Me.pnlMain.Controls.Add(Me.Label5)
Me.pnlMain.Controls.Add(Me.Label4)
Me.pnlMain.Controls.Add(Me.Label3)
Me.pnlMain.Controls.Add(Me.Label2)
Me.pnlMain.Controls.Add(Me.Label1)
Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
Me.pnlMain.Font = New System.Drawing.Font("Arial", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
Me.pnlMain.Location = New System.Drawing.Point(0, 24)
Me.pnlMain.Name = "pnlMain"
Me.pnlMain.Size = New System.Drawing.Size(462, 292)
Me.pnlMain.TabIndex = 1
'
'btnSearchPatient
'
Me.btnSearchPatient.FlatStyle = System.Windows.Forms.FlatStyle.System
Me.btnSearchPatient.Location = New System.Drawing.Point(279, 24)
Me.btnSearchPatient.Name = "btnSearchPatient"
Me.btnSearchPatient.Size = New System.Drawing.Size(102, 20)
Me.btnSearchPatient.TabIndex = 23
Me.btnSearchPatient.Text = "Search Patient"
'
'Label6
'
Me.Label6.AutoSize = true
Me.Label6.Location = New System.Drawing.Point(5, 72)
Me.Label6.Name = "Label6"
Me.Label6.Size = New System.Drawing.Size(82, 15)
Me.Label6.TabIndex = 21
Me.Label6.Text = "Patient Name"
'
'lblVisitID
'
Me.lblVisitID.AutoSize = true
Me.lblVisitID.Location = New System.Drawing.Point(280, 24)
Me.lblVisitID.Name = "lblVisitID"
Me.lblVisitID.Size = New System.Drawing.Size(14, 15)
Me.lblVisitID.TabIndex = 20
Me.lblVisitID.Text = "0"
Me.lblVisitID.Visible = false
'
'dtpTime
'
Me.dtpTime.CustomFormat = "hh:mm tt"
Me.dtpTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom
Me.dtpTime.Location = New System.Drawing.Point(351, 161)
Me.dtpTime.Name = "dtpTime"
Me.dtpTime.ShowUpDown = true
Me.dtpTime.Size = New System.Drawing.Size(88, 21)
Me.dtpTime.TabIndex = 18
Me.dtpTime.Value = New Date(2005, 8, 30, 0, 0, 0, 0)
'
'dtpDate
'
Me.dtpDate.CustomFormat = "MM/dd/yyyy"
Me.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
Me.dtpDate.Location = New System.Drawing.Point(96, 161)
Me.dtpDate.Name = "dtpDate"
Me.dtpDate.Size = New System.Drawing.Size(112, 21)
Me.dtpDate.TabIndex = 17
Me.dtpDate.Value = New Date(2005, 8, 30, 0, 0, 0, 0)
'
'txtLastName
'
Me.txtLastName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
Me.txtLastName.Enabled = false
Me.txtLastName.Location = New System.Drawing.Point(279, 64)
Me.txtLastName.Name = "txtLastName"
Me.txtLastName.Size = New System.Drawing.Size(160, 21)
Me.txtLastName.TabIndex = 14
'
'Label8
'
Me.Label8.AutoSize = true
Me.Label8.Location = New System.Drawing.Point(319, 91)
Me.Label8.Name = "Label8"
Me.Label8.Size = New System.Drawing.Size(76, 15)
Me.Label8.TabIndex = 13
Me.Label8.Text = "(Last Name)"
'
'txtFirstName
'
Me.txtFirstName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
Me.txtFirstName.Enabled = false
Me.txtFirstName.Location = New System.Drawing.Point(96, 64)
Me.txtFirstName.Name = "txtFirstName"
Me.txtFirstName.Size = New System.Drawing.Size(160, 21)
Me.txtFirstName.TabIndex = 12
'
'txtPatientCode
'
Me.txtPatientCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
Me.txtPatientCode.Enabled = false
Me.txtPatientCode.Location = New System.Drawing.Point(96, 24)
Me.txtPatientCode.Name = "txtPatientCode"
Me.txtPatientCode.Size = New System.Drawing.Size(160, 21)
Me.txtPatientCode.TabIndex = 11
Me.txtPatientCode.Tag = "1"
'
'cmbProvider
'
Me.cmbProvider.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
Me.cmbProvider.Location = New System.Drawing.Point(96, 120)
Me.cmbProvider.Name = "cmbProvider"
Me.cmbProvider.Size = New System.Drawing.Size(344, 23)
Me.cmbProvider.TabIndex = 10
'
'Label5
'
Me.Label5.AutoSize = true
Me.Label5.Location = New System.Drawing.Point(315, 163)
Me.Label5.Name = "Label5"
Me.Label5.Size = New System.Drawing.Size(35, 15)
Me.Label5.TabIndex = 4
Me.Label5.Text = "Time"
'
'Label4
'
Me.Label4.AutoSize = true
Me.Label4.Location = New System.Drawing.Point(48, 163)
Me.Label4.Name = "Label4"
Me.Label4.Size = New System.Drawing.Size(33, 15)
Me.Label4.TabIndex = 3
Me.Label4.Text = "Date"
'
'Label3
'
Me.Label3.AutoSize = true
Me.Label3.Location = New System.Drawing.Point(32, 128)
Me.Label3.Name = "Label3"
Me.Label3.Size = New System.Drawing.Size(52, 15)
Me.Label3.TabIndex = 2
Me.Label3.Text = "Provider"
'
'Label2
'
Me.Label2.AutoSize = true
Me.Label2.Location = New System.Drawing.Point(136, 91)
Me.Label2.Name = "Label2"
Me.Label2.Size = New System.Drawing.Size(76, 15)
Me.Label2.TabIndex = 1
Me.Label2.Text = "(First Name)"
'
'Label1
'
Me.Label1.AutoSize = true
Me.Label1.Location = New System.Drawing.Point(8, 24)
Me.Label1.Name = "Label1"
Me.Label1.Size = New System.Drawing.Size(78, 15)
Me.Label1.TabIndex = 0
Me.Label1.Text = "Patient Code"
'
'pnlSearchPatient
'
Me.pnlSearchPatient.Controls.Add(Me.pnlCommands)
Me.pnlSearchPatient.Controls.Add(Me.dgData)
Me.pnlSearchPatient.Controls.Add(Me.pnlSearchCriteria)
Me.pnlSearchPatient.Dock = System.Windows.Forms.DockStyle.Fill
Me.pnlSearchPatient.Location = New System.Drawing.Point(0, 24)
Me.pnlSearchPatient.Name = "pnlSearchPatient"
Me.pnlSearchPatient.Size = New System.Drawing.Size(462, 292)
Me.pnlSearchPatient.TabIndex = 5
Me.pnlSearchPatient.Visible = false
'
'pnlCommands
'
Me.pnlCommands.Controls.Add(Me.Button1)
Me.pnlCommands.Controls.Add(Me.Button2)
Me.pnlCommands.Dock = System.Windows.Forms.DockStyle.Bottom
Me.pnlCommands.Location = New System.Drawing.Point(0, 260)
Me.pnlCommands.Name = "pnlCommands"
Me.pnlCommands.Size = New System.Drawing.Size(462, 32)
Me.pnlCommands.TabIndex = 2
'
'Button1
'
Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
Me.Button1.Location = New System.Drawing.Point(392, 8)
Me.Button1.Name = "Button1"
Me.Button1.Size = New System.Drawing.Size(64, 20)
Me.Button1.TabIndex = 3
Me.Button1.Text = "Cancel"
'
'Button2
'
Me.Button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
Me.Button2.Location = New System.Drawing.Point(320, 8)
Me.Button2.Name = "Button2"
Me.Button2.Size = New System.Drawing.Size(64, 20)
Me.Button2.TabIndex = 2
Me.Button2.Text = "OK"
'
'dgData
'
Me.dgData.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
Me.dgData.CaptionVisible = false
Me.dgData.DataMember = ""
Me.dgData.Dock = System.Windows.Forms.DockStyle.Fill
Me.dgData.FullRowSelect = false
Me.dgData.HeaderForeColor = System.Drawing.SystemColors.ControlText
Me.dgData.Location = New System.Drawing.Point(0, 32)
Me.dgData.Name = "dgData"
Me.dgData.ReadOnly = true
Me.dgData.Size = New System.Drawing.Size(462, 260)
Me.dgData.TabIndex = 1
'
'pnlSearchCriteria
'
Me.pnlSearchCriteria.Controls.Add(Me.txtSearch)
Me.pnlSearchCriteria.Controls.Add(Me.optLastName)
Me.pnlSearchCriteria.Controls.Add(Me.optFirstName)
Me.pnlSearchCriteria.Controls.Add(Me.optPatientCode)
Me.pnlSearchCriteria.Dock = System.Windows.Forms.DockStyle.Top
Me.pnlSearchCriteria.Location = New System.Drawing.Point(0, 0)
Me.pnlSearchCriteria.Name = "pnlSearchCriteria"
Me.pnlSearchCriteria.Size = New System.Drawing.Size(462, 32)
Me.pnlSearchCriteria.TabIndex = 0
'
'txtSearch
'
Me.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
Me.txtSearch.Location = New System.Drawing.Point(296, 5)
Me.txtSearch.Name = "txtSearch"
Me.txtSearch.Size = New System.Drawing.Size(160, 21)
Me.txtSearch.TabIndex = 3
'
'optLastName
'
Me.optLastName.Location = New System.Drawing.Point(207, 8)
Me.optLastName.Name = "optLastName"
Me.optLastName.Size = New System.Drawing.Size(88, 16)
Me.optLastName.TabIndex = 2
Me.optLastName.Text = "Last Name"
'
'optFirstName
'
Me.optFirstName.Location = New System.Drawing.Point(110, 8)
Me.optFirstName.Name = "optFirstName"
Me.optFirstName.Size = New System.Drawing.Size(88, 16)
Me.optFirstName.TabIndex = 1
Me.optFirstName.Text = "First Name"
'
'optPatientCode
'
Me.optPatientCode.Checked = true
Me.optPatientCode.Location = New System.Drawing.Point(8, 8)
Me.optPatientCode.Name = "optPatientCode"
Me.optPatientCode.Size = New System.Drawing.Size(96, 16)
Me.optPatientCode.TabIndex = 0
Me.optPatientCode.TabStop = true
Me.optPatientCode.Text = "Patient Code"
'
'PictureBox1
'
Me.PictureBox1.Dock = System.Windows.Forms.DockStyle.Top
Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"),System.Drawing.Image)
Me.PictureBox1.Location = New System.Drawing.Point(0, 0)
Me.PictureBox1.Name = "PictureBox1"
Me.PictureBox1.Size = New System.Drawing.Size(462, 24)
Me.PictureBox1.TabIndex = 2
Me.PictureBox1.TabStop = false
'
'pnlMainCommands
'
Me.pnlMainCommands.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
Me.pnlMainCommands.Controls.Add(Me.cmdCancel)
Me.pnlMainCommands.Controls.Add(Me.cmdOK)
Me.pnlMainCommands.Dock = System.Windows.Forms.DockStyle.Bottom
Me.pnlMainCommands.Location = New System.Drawing.Point(0, 318)
Me.pnlMainCommands.Name = "pnlMainCommands"
Me.pnlMainCommands.Size = New System.Drawing.Size(464, 40)
Me.pnlMainCommands.TabIndex = 2
'
'cmdCancel
'
Me.cmdCancel.Location = New System.Drawing.Point(392, 8)
Me.cmdCancel.Name = "cmdCancel"
Me.cmdCancel.Size = New System.Drawing.Size(64, 24)
Me.cmdCancel.TabIndex = 1
Me.cmdCancel.Text = "Cancel"
'
'cmdOK
'
Me.cmdOK.Location = New System.Drawing.Point(318, 8)
Me.cmdOK.Name = "cmdOK"
Me.cmdOK.Size = New System.Drawing.Size(64, 24)
Me.cmdOK.TabIndex = 0
Me.cmdOK.Text = "OK"
'
'frmVisit
'
Me.AutoScaleBaseSize = New System.Drawing.Size(6, 14)
Me.ClientSize = New System.Drawing.Size(464, 358)
Me.Controls.Add(Me.Panel2)
Me.Controls.Add(Me.pnlMainCommands)
Me.Font = New System.Drawing.Font("Arial", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
Me.MaximizeBox = false
Me.MinimizeBox = false
Me.Name = "frmVisit"
Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
Me.Text = "Visit"
Me.Panel2.ResumeLayout(false)
Me.pnlMain.ResumeLayout(false)
Me.pnlMain.PerformLayout
Me.pnlSearchPatient.ResumeLayout(false)
Me.pnlCommands.ResumeLayout(false)
CType(Me.dgData,System.ComponentModel.ISupportInitialize).EndInit
Me.pnlSearchCriteria.ResumeLayout(false)
Me.pnlSearchCriteria.PerformLayout
CType(Me.PictureBox1,System.ComponentModel.ISupportInitialize).EndInit
Me.pnlMainCommands.ResumeLayout(false)
Me.ResumeLayout(false)

End Sub

#End Region

    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        Me.Close()
    End Sub

    Private Sub cmdOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOK.Click
        'Save code goes here
        Dim Arrlist As New ArrayList
        Try
            'If Trim(txtPatientCode.Text) = "" Then
            'MsgBox("Patientcode Required")
            'txtPatientCode.Focus()
            'Exit Sub
            'End If
            SetData(Arrlist)

            objclsVisit.AddData(Arrlist)
            Me.Close()
        Catch ex As SqlClient.SqlException
            MessageBox.Show(ex.Message, "Visit Registration", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Visit Registration", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub frmVisit_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Open Connection
        'Fill_Provider()
        ''dtpDate.Value = frmViewVisits.dtVisitDate
        ''dtpTime.Value = frmViewVisits.dtVisitTime
        'dtpDate.Focus()
        'dtpTime.Value = Now

        'txtPatientCode.Tag = gnPatientID
        'txtPatientCode.Text = gstrPatientCode
        'txtFirstName.Text = gstrPatientFirstName
        'txtLastName.Text = gstrPatientLastName
        dgData.FullRowSelect = True
    End Sub


    Public Sub Fill_Provider()
        Dim dt As DataTable = objclsVisit.GetAllProvider()
        cmbProvider.DataSource = dt
        cmbProvider.ValueMember = dt.Columns(0).ColumnName
        cmbProvider.DisplayMember = dt.Columns(1).ColumnName
        '& " " & objclsTemplateGallery.GetAllProvider.Table.Columns(2).ColumnName & " " & objclsTemplateGallery.GetAllProvider.Table.Columns(3).ColumnName
        'cmbProvider.SelectedIndex = -1
    End Sub

    Private Sub SetData(ByRef Arrlist As ArrayList)

        Arrlist.Add(lblVisitID.Text) 'Visit ID
        Arrlist.Add(txtPatientCode.Tag)
        Arrlist.Add(cmbProvider.SelectedValue)
        Arrlist.Add(lblVisitID.Tag) 'Appoitment ID
        Arrlist.Add(dtpDate.Value.Date & " " & Format(dtpTime.Value, "Medium Time"))
    End Sub

    Private Sub btnSearchPatient_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchPatient.Click
        pnlMain.Visible = False
        pnlMainCommands.Visible = False
        pnlSearchPatient.Visible = True
    End Sub
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If dgData.CurrentRowIndex >= 0 Then
            pnlMain.Visible = True
            pnlMainCommands.Visible = True
            pnlSearchPatient.Visible = False
            txtPatientCode.Tag = dgData.Item(dgData.CurrentRowIndex, 0)
            txtPatientCode.Text = dgData.Item(dgData.CurrentRowIndex, 1)
            txtFirstName.Text = dgData.Item(dgData.CurrentRowIndex, 2)
            txtLastName.Text = dgData.Item(dgData.CurrentRowIndex, 3)
        Else
            MessageBox.Show("Please select Patient", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        pnlMain.Visible = True
        pnlMainCommands.Visible = True
        pnlSearchPatient.Visible = False
    End Sub

    Private Sub txtSearch_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSearch.KeyPress
        If e.KeyChar = ChrW(Keys.Enter) Then
            If Trim(txtSearch.Text) <> "" Then
                Dim dtPatient As DataTable
                Dim objPatient As New clsPatient
                If optPatientCode.Checked = True Then
                    dtPatient = objPatient.Fill_Patients(Trim(txtSearch.Text), clsPatient.enmPatientSearchCriteria.PatientCode)
                ElseIf optFirstName.Checked = True Then
                    dtPatient = objPatient.Fill_Patients(Trim(txtSearch.Text), clsPatient.enmPatientSearchCriteria.PatientFirstName)
                Else
                    dtPatient = objPatient.Fill_Patients(Trim(txtSearch.Text), clsPatient.enmPatientSearchCriteria.PatientLastName)
                End If
                objPatient = Nothing
                dgData.DataSource = dtPatient
                Dim grdTableStyle As New clsDataGridTableStyle(dtPatient.TableName)

                Dim grdColStylePatientID As New DataGridTextBoxColumn
                With grdColStylePatientID
                    .HeaderText = "Patient ID"
                    .Alignment = HorizontalAlignment.Left
                    .MappingName = dtPatient.Columns(0).ColumnName
                    .NullText = ""
                    .Width = 0
                End With

                Dim grdColStylePatientCode As New DataGridTextBoxColumn
                With grdColStylePatientCode
                    .HeaderText = "Patient ID"
                    .Alignment = HorizontalAlignment.Left
                    .MappingName = dtPatient.Columns(1).ColumnName
                    .NullText = ""
                    .Width = 0.33 * dgData.Width
                End With


                Dim grdColStylePatientFirstName As New DataGridTextBoxColumn
                With grdColStylePatientFirstName
                    .HeaderText = "First Name"
                    .Alignment = HorizontalAlignment.Left
                    .MappingName = dtPatient.Columns(2).ColumnName
                    .NullText = ""
                    .Width = 0.33 * dgData.Width
                End With

                Dim grdColStylePatientLastName As New DataGridTextBoxColumn
                With grdColStylePatientLastName
                    .HeaderText = "Last Name"
                    .Alignment = HorizontalAlignment.Left
                    .MappingName = dtPatient.Columns(4).ColumnName
                    .NullText = ""
                    .Width = 0.33 * dgData.Width - 5
                End With


                grdTableStyle.GridColumnStyles.AddRange(New DataGridColumnStyle() {grdColStylePatientID, grdColStylePatientCode, grdColStylePatientFirstName, grdColStylePatientLastName})
                dgData.TableStyles.Clear()
                dgData.TableStyles.Add(grdTableStyle)

            End If
        End If
    End Sub

    Private Sub dgData_CurrentCellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgData.CurrentCellChanged
        'dgData.Select(dgData.CurrentRowIndex)
    End Sub

    Private Sub dgData_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgData.DoubleClick
        If dgData.CurrentRowIndex >= 0 Then
            pnlMain.Visible = True
            pnlMainCommands.Visible = True
            pnlSearchPatient.Visible = False
            txtPatientCode.Tag = dgData.Item(dgData.CurrentRowIndex, 0)
            txtPatientCode.Text = dgData.Item(dgData.CurrentRowIndex, 1)
            txtFirstName.Text = dgData.Item(dgData.CurrentRowIndex, 2)
            txtLastName.Text = dgData.Item(dgData.CurrentRowIndex, 3)
        Else
            MessageBox.Show("Please select Patient", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If
    End Sub

    Private Sub pnlMain_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles pnlMain.Paint

    End Sub
End Class
