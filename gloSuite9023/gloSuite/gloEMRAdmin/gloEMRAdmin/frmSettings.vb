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
        Me.SuspendLayout()
        '
        'frmSettings
        '
        Me.ClientSize = New System.Drawing.Size(292, 273)
        Me.Name = "frmSettings"
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

            ''sarika 11th aug 07
            'txtHL7FilePath.Text = objSettings.HL7SystemPath
            ''-------------------

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
            conn.ConnectionString = gloEMRAdmin.mdlGeneral.GetConnectionString()

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
            conn.ConnectionString = gloEMRAdmin.mdlGeneral.GetConnectionString()
            conn.Open()
            ''Comment By Dhruv 20091211 to retrive only the Active user
            '_strSQL = "select nUserID , sLoginName from User_MST"
            _strSQL = "select nUserID , sLoginName from User_MST where nBlockStatus = 0 "
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

            ''sarika 11th aug 07
            'objSettings.HL7SystemPath = txtHL7FilePath.Text
            ''-------------------------------------

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
        Dim conn As New SqlConnection(gloEMRAdmin.mdlGeneral.GetConnectionString)
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
            '' Bug 6482: Spelling of complexity changed
            MessageBox.Show("You have not set password complexity.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
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
        Dim conn As New SqlConnection(gloEMRAdmin.mdlGeneral.GetConnectionString)
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
