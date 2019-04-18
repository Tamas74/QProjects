'start of code commented by manoj jadhav on 20111003
'Imports System.Data.SqlClient
'Public Class FrmDeviceActivation
'Dim sStoredKey As String = String.Empty

'Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
'    Dim objSettings As New clsSettings
'    Dim objEncrypt As New clsEncryption
'    Dim strActivationKey As String = String.Empty
'    If lblDeviceNm.Text.Trim = "VITALDEVICE" Then
'        If txtVitalDeviceKey.Text.Trim.Length = 0 Then
'            MessageBox.Show("Please enter vital device interface activation key", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
'            txtVitalDeviceKey.Focus()
'            Exit Sub
'        Else
'            Dim objEncr As New clsEncryption()
'            If Not (objEncr.EncryptToBase64String(String.Concat(lblAUSUserName.Text.Trim.ToLower, "gL0@PPs2k9!"), "87654321") = txtVitalDeviceKey.Text.Trim) Then
'                MessageBox.Show("Please enter valid vital device interface activation key", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
'                txtVitalDeviceKey.Focus()
'                Exit Sub
'            Else
'                strActivationKey = objEncr.EncryptToBase64String(txtVitalDeviceKey.Text.Trim, mdlGeneral.constEncryptDecryptKey)
'            End If
'        End If
'    ElseIf lblDeviceNm.Text = "SPIROMETRYDEVICE" Then
'        If txtVitalDeviceKey.Text.Trim.Length = 0 Then
'            MessageBox.Show("Please enter spirometry device interface activation key", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
'            txtVitalDeviceKey.Focus()
'            Exit Sub
'        Else
'            Dim objEncr As New clsEncryption()
'            If Not (objEncr.EncryptToBase64String(String.Concat(lblAUSUserName.Text.Trim.ToLower, "gL0@PPs2k9!8228"), "87654321") = txtVitalDeviceKey.Text.Trim) Then
'                MessageBox.Show("Please enter valid spirometry device interface activation key", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
'                txtVitalDeviceKey.Focus()
'                Exit Sub
'            Else
'                strActivationKey = objEncr.EncryptToBase64String(txtVitalDeviceKey.Text.Trim, mdlGeneral.constEncryptDecryptKey)
'                'strActivationKey = txtVitalDeviceKey.Text.Trim
'            End If
'        End If
'    ElseIf lblDeviceNm.Text = "INTUITINTERFACE" Then
'        If txtVitalDeviceKey.Text.Trim.Length = 0 Then
'            MessageBox.Show("Please enter intuit interface activation key", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
'            txtVitalDeviceKey.Focus()
'            Exit Sub
'        Else
'            Dim objEncr As New clsEncryption()
'            If Not (objEncr.EncryptToBase64String(String.Concat(lblAUSUserName.Text.Trim.ToLower, "gL0@PPs2k9!8709"), "87654321") = txtVitalDeviceKey.Text.Trim) Then
'                MessageBox.Show("Please enter valid intuit interface activation key", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
'                txtVitalDeviceKey.Focus()
'                Exit Sub
'            Else
'                strActivationKey = objEncr.EncryptToBase64String(txtVitalDeviceKey.Text.Trim, mdlGeneral.constEncryptDecryptKey)
'                'strActivationKey = txtVitalDeviceKey.Text.Trim
'            End If
'        End If
'    End If
'    UpdateSettings("USE" & lblDeviceNm.Text, lblDeviceNm.Text & "KEY", strActivationKey, "True")
'    If Not IsNothing(objEncrypt) Then
'        objEncrypt = Nothing
'    End If
'    If Not IsNothing(objSettings) Then
'        objEncrypt = Nothing
'    End If
'    Me.Close()
'    blnActivation = True
'End Sub

'Public Function UpdateSettings(ByVal strDeviceName As String, ByVal strDeviceKye As String, ByVal txtKey As String, ByVal strStatus As String) As Boolean
'    Dim objCon As New SqlConnection
'    objCon.ConnectionString = gloEMRAdmin.mdlGeneral.GetConnectionString()
'    Dim objCmd As New SqlCommand
'    objCmd.CommandType = CommandType.StoredProcedure
'    objCmd.CommandText = "gsp_UpdateSettings"
'    Dim objParaSettingsName As New SqlParameter
'    Dim objParaSettingsValue As New SqlParameter

'    Dim objParaSettingsClinicID As New SqlParameter
'    Dim objParaSettingsUserID As New SqlParameter
'    Dim objParaSettingsUserClinicFlag As New SqlParameter

'    objCmd.Connection = objCon

'    objCon.Open()
'    objCmd.Parameters.Clear()
'    With objParaSettingsName
'        .ParameterName = "@SettingsName"
'        .Value = strDeviceName
'        .Direction = ParameterDirection.Input
'        .SqlDbType = SqlDbType.VarChar
'    End With
'    objCmd.Parameters.Add(objParaSettingsName)

'    With objParaSettingsValue
'        .ParameterName = "@SettingsValue"
'        .Value = strStatus
'        .Direction = ParameterDirection.Input
'        .SqlDbType = SqlDbType.VarChar
'    End With
'    objCmd.Parameters.Add(objParaSettingsValue)
'    With objParaSettingsClinicID
'        .ParameterName = "@nClinicID"
'        .Value = 1
'        .Direction = ParameterDirection.Input
'        .SqlDbType = SqlDbType.BigInt
'    End With
'    objCmd.Parameters.Add(objParaSettingsClinicID)

'    With objParaSettingsUserID
'        .ParameterName = "@nUserID"
'        .Value = 0
'        .Direction = ParameterDirection.Input
'        .SqlDbType = SqlDbType.BigInt
'    End With
'    objCmd.Parameters.Add(objParaSettingsUserID)

'    With objParaSettingsUserClinicFlag
'        .ParameterName = "@nUserClinicFlag"
'        .Value = 2
'        .Direction = ParameterDirection.Input
'        .SqlDbType = SqlDbType.Int
'    End With
'    objCmd.Parameters.Add(objParaSettingsUserClinicFlag)
'    objCmd.ExecuteNonQuery()

'    objCmd.Parameters.Clear()
'    With objParaSettingsName
'        .ParameterName = "@SettingsName"
'        .Value = strDeviceKye
'        .Direction = ParameterDirection.Input
'        .SqlDbType = SqlDbType.VarChar
'    End With
'    objCmd.Parameters.Add(objParaSettingsName)
'    With objParaSettingsValue
'        .ParameterName = "@SettingsValue"
'        .Value = txtKey
'        .Direction = ParameterDirection.Input
'        .SqlDbType = SqlDbType.VarChar
'    End With
'    objCmd.Parameters.Add(objParaSettingsValue)

'    With objParaSettingsClinicID
'        .ParameterName = "@nClinicID"
'        .Value = 1
'        .Direction = ParameterDirection.Input
'        .SqlDbType = SqlDbType.BigInt
'    End With
'    objCmd.Parameters.Add(objParaSettingsClinicID)

'    With objParaSettingsUserID
'        .ParameterName = "@nUserID"
'        .Value = 0
'        .Direction = ParameterDirection.Input
'        .SqlDbType = SqlDbType.BigInt
'    End With
'    objCmd.Parameters.Add(objParaSettingsUserID)

'    With objParaSettingsUserClinicFlag
'        .ParameterName = "@nUserClinicFlag"
'        .Value = 2
'        .Direction = ParameterDirection.Input
'        .SqlDbType = SqlDbType.Int
'    End With
'    objCmd.Parameters.Add(objParaSettingsUserClinicFlag)
'    '' End Add ClinicID, UserID,UserClinicFlag
'    objCmd.ExecuteNonQuery()
'    Return True
'End Function

'Public Function GetSettings() As Boolean
'    Dim objCon As New SqlConnection
'    Dim objEncrypt As New clsEncryption
'    objCon.ConnectionString = gloEMRAdmin.mdlGeneral.GetConnectionString()
'    Dim objCmd As New SqlCommand
'    objCmd.CommandType = CommandType.StoredProcedure
'    objCmd.CommandText = "gsp_FillSettings"
'    objCmd.Connection = objCon
'    objCmd.Connection = objCon
'    objCon.Open()
'    Dim objDA As New SqlDataAdapter(objCmd)
'    Dim dsData As New DataSet
'    objDA.Fill(dsData)
'    objCon.Close()
'    objCon = Nothing
'    Dim nCount As Integer
'    If dsData.Tables(0).Rows.Count = 0 Then
'        Return False
'    End If
'    sStoredKey = String.Empty
'    If lblDeviceNm.Text = "VITALDEVICE" Then
'        For nCount = 0 To dsData.Tables(0).Rows.Count - 1
'            Select Case dsData.Tables(0).Rows(nCount).Item(1).ToString.ToUpper
'                Case "VitalDeviceKey".ToUpper()
'                    If IsDBNull(dsData.Tables(0).Rows(nCount).Item(2)) = False Then
'                        'txtVitalDeviceKey.Text = CType(dsData.Tables(0).Rows(nCount).Item(2), String)
'                        txtVitalDeviceKey.Text = objEncrypt.DecryptFromBase64String(CType(dsData.Tables(0).Rows(nCount).Item(2), String), mdlGeneral.constEncryptDecryptKey)
'                        sStoredKey = txtVitalDeviceKey.Text.Trim
'                    Else
'                        txtVitalDeviceKey.Text = String.Empty
'                    End If
'            End Select
'        Next
'    ElseIf lblDeviceNm.Text = "SPIROMETRYDEVICE" Then
'        For nCount = 0 To dsData.Tables(0).Rows.Count - 1
'            Select Case dsData.Tables(0).Rows(nCount).Item(1).ToString.ToUpper
'                Case "SpirometryDeviceKey".ToUpper()
'                    If IsDBNull(dsData.Tables(0).Rows(nCount).Item(2)) = False Then
'                        'txtVitalDeviceKey.Text = CType(dsData.Tables(0).Rows(nCount).Item(2), String)
'                        txtVitalDeviceKey.Text = objEncrypt.DecryptFromBase64String(CType(dsData.Tables(0).Rows(nCount).Item(2), String), mdlGeneral.constEncryptDecryptKey)
'                        sStoredKey = txtVitalDeviceKey.Text.Trim
'                    Else
'                        txtVitalDeviceKey.Text = ""
'                    End If
'            End Select
'        Next
'    ElseIf lblDeviceNm.Text = "INTUITINTERFACE" Then
'        For nCount = 0 To dsData.Tables(0).Rows.Count - 1
'            Select Case dsData.Tables(0).Rows(nCount).Item(1).ToString.ToUpper
'                Case "INTUITINTERFACEKEY"
'                    If IsDBNull(dsData.Tables(0).Rows(nCount).Item(2)) = False Then
'                        'txtVitalDeviceKey.Text = CType(dsData.Tables(0).Rows(nCount).Item(2), String)
'                        txtVitalDeviceKey.Text = objEncrypt.DecryptFromBase64String(CType(dsData.Tables(0).Rows(nCount).Item(2), String), mdlGeneral.constEncryptDecryptKey)
'                        sStoredKey = txtVitalDeviceKey.Text.Trim
'                    Else
'                        txtVitalDeviceKey.Text = ""
'                    End If
'            End Select
'        Next
'    End If
'    Return True
'End Function

'Private Sub FrmDeviceActivation_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
'    Try
'        txtVitalDeviceKey.Text = String.Empty
'        lblDeviceNm.Text = strDiveceName
'        lblAUSUserName.Text = frmSettings_New.GetClinicInformation("sExternalcode")
'        GetSettings()
'    Catch ex As Exception

'    End Try
'End Sub

'Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
'    If sStoredKey = String.Empty Then
'        blnActivation = False
'    Else
'        blnActivation = True
'    End If
'    Me.Close()
'End Sub
'end of code commented by manoj jadhav on 20111003

'Added New of class by manoj jadhav on 20111003

Imports System.Linq


Public Class FrmDeviceActivation

#Region "Form Variables"

    Private nClinicID As Long = 1
    Private nUSerId As Long = 0
    Private _IsSettingChanged As Boolean = False
    Private _PatientPortal_DefaultUserID As Int64 = 0
    Private _PatientPortal_defaultUserName As String = String.Empty
    Private oListControl_PatientPortalTask As gloListControl.gloListControl
    Dim _blnIntuitHealth As Boolean = False
    ''Added for MU2 Patient Portal implementation on 20130620
    Dim _blnPatientPortal As Boolean = False
    Private _IsPatientPortalActivated As Boolean
    ''End

    Public Property IsSettingChanged As Boolean
        Get
            Return _IsSettingChanged
        End Get
        Set(ByVal value As Boolean)
            _IsSettingChanged = value
        End Set
    End Property

    Public Property IsPatientPortalActivated() As Boolean
        Get
            Return _IsPatientPortalActivated
        End Get
        Set(ByVal value As Boolean)
            _IsPatientPortalActivated = value
        End Set
    End Property




    Dim sPatPortalMerchantId As String = String.Empty
    Dim sPatPortalRegistrationKey As String = String.Empty
    Dim sPatientPortalOnlinePaymentEnabled As String = "False"


    Public sCommonPatPortalEmailService As String = String.Empty
    Public sCommonPatientPortalgloCoreServicesIntallationPath As String = String.Empty
#End Region

#Region "Enum Variables"
    Public Enum DeviceSettings
        WelchAllynECGDevice
        WelChAllynVitalDevice
        MidmarkSpirometryDevice
        CardiacScienceECGDevice
        IntuitHealthInterface
        MidmarkECGDevice
        CCDAService
    End Enum
    Private _DeviceType As DeviceSettings = Nothing
#End Region

#Region "Control Event"


    Public Sub New()
        InitializeComponent()
    End Sub

    Public Sub New(ByVal DeviceType As DeviceSettings)
        InitializeComponent()
        _DeviceType = DeviceType
    End Sub

    Private Sub FrmDeviceActivation_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'get AUS name
        lblAUSUserName.Text = GetAUSName()
        'set control on form as per selected device type
        If SetControlProerties() Then
            ' read settings for device selected
            txtPatientPortalEmailService.Text = sCommonPatPortalEmailService
            txtPatientPortalgloCoreServicesInstallationPath.Text = sCommonPatientPortalgloCoreServicesIntallationPath
            ReadSettings()

        End If
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        'insert/update settings for selected device
        IsPatientPortalActivated = False
        If InUPDeviceSettings() Then
            ''''Added for Fixed Bug id 55276 on 20130806
            Select Case _DeviceType
                Case DeviceSettings.IntuitHealthInterface
                    If rbIntuitPortal.Checked Or rbPatientPortal.Checked Then
                        IsSettingChanged = True
                        IsPatientPortalActivated = rbPatientPortal.Checked
                        Me.Close()
                    Else
                        IsSettingChanged = False
                    End If
                Case Else
                    IsSettingChanged = True
                    Me.Close()
            End Select
        Else
            ''If Device Activation textbox blank then do not close the form on 20130911
            If txtDeviceActivationKey.Text.Trim.Length <> 0 And txtPortalSiteNm.Text.Trim.Length <> 0 And txtPatientPortalEmailService.Text.Trim.Length <> 0 And txtPatientPortalgloCoreServicesInstallationPath.Text.Trim.Length <> 0 And txtMerchantId.Text.Trim.Length <> 0 And txtRegistrationKey.Text.Trim.Length <> 0 And System.IO.Directory.Exists(txtPatientPortalgloCoreServicesInstallationPath.Text.Trim) Then
                Me.Close()
            End If
            ''End
        End If
        ''End
    End Sub

    Private Sub txtPrefixForSpirometryDevice_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If (Not (Char.IsLetter(e.KeyChar))) And (Not (e.KeyChar = Convert.ToChar(8))) Then
            e.Handled = True
        End If
    End Sub

    Private Sub btnPatientPortalTaskUserSearch_Click(sender As System.Object, e As System.EventArgs) Handles btnPatientPortalTaskUserSearch.Click
        Try
            If oListControl_PatientPortalTask IsNot Nothing Then
                For i As Integer = Me.Controls.Count - 1 To 0 Step -1
                    If Me.Controls(i).Name = oListControl_PatientPortalTask.Name Then
                        Me.Controls.Remove(Me.Controls(i))
                        Exit For
                    End If
                Next
            End If

            oListControl_PatientPortalTask = New gloListControl.gloListControl(mdlGeneral.GetConnectionString, gloListControl.gloListControlType.Users, False, Me.Width)
            oListControl_PatientPortalTask.ClinicID = 1
            oListControl_PatientPortalTask.ControlHeader = "Users"

            AddHandler oListControl_PatientPortalTask.ItemSelectedClick, AddressOf oListControl_PatientPortalTask_itemSelectedClick
            AddHandler oListControl_PatientPortalTask.ItemClosedClick, AddressOf oListControl_PatientPortalTask_ItemClosedClick

            Me.Controls.Add(oListControl_PatientPortalTask)
            oListControl_PatientPortalTask.OpenControl()

            If oListControl_PatientPortalTask.IsDisposed = False Then
                oListControl_PatientPortalTask.Dock = DockStyle.Fill
                oListControl_PatientPortalTask.BringToFront()
                PnlDeviceName.Visible = False
                Me.Height = Me.Height + 200
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        End Try
    End Sub

    Private Sub oListControl_PatientPortalTask_itemSelectedClick(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            txtPatientPortalTask_DefaultUser.Clear()

            If oListControl_PatientPortalTask.SelectedItems.Count > 0 Then
                _PatientPortal_DefaultUserID = oListControl_PatientPortalTask.SelectedItems(0).ID
                _PatientPortal_defaultUserName = oListControl_PatientPortalTask.SelectedItems(0).Description.ToString()

                txtPatientPortalTask_DefaultUser.Text = oListControl_PatientPortalTask.SelectedItems(0).Description.ToString()
            End If
            If Not IsNothing(oListControl_PatientPortalTask) Then
                oListControl_PatientPortalTask.Dispose()
                oListControl_PatientPortalTask = Nothing
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        Finally
            PnlDeviceName.Visible = True
            If ((Me.Height - 200) >= (tstrip.Height + PnlDeviceName.Height)) Then
                Me.Height = Me.Height - 200
            End If
        End Try
    End Sub
    Private Sub oListControl_PatientPortalTask_ItemClosedClick(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            If oListControl_PatientPortalTask IsNot Nothing Then
                For i As Integer = Me.Controls.Count - 1 To 0 Step -1
                    If Me.Controls(i).Name = oListControl_PatientPortalTask.Name Then
                        Me.Controls.Remove(Me.Controls(i))
                        Exit For
                    End If
                Next
                oListControl_PatientPortalTask.Dispose()
                oListControl_PatientPortalTask = Nothing
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        Finally
            PnlDeviceName.Visible = True
            If ((Me.Height - 200) >= (tstrip.Height + PnlDeviceName.Height)) Then
                Me.Height = Me.Height - 200
            End If
        End Try
    End Sub

    Private Sub btnPatientPortalTaskUserDelete_Click(sender As System.Object, e As System.EventArgs) Handles btnPatientPortalTaskUserDelete.Click
        Try
            txtPatientPortalTask_DefaultUser.Text = String.Empty
            _PatientPortal_DefaultUserID = 0
            _PatientPortal_defaultUserName = String.Empty
        Catch ex As Exception
            ex = Nothing
        End Try
    End Sub

#End Region

#Region "Functions And methods"

    ''' <summary>Function to set control properties on for as per device selected </summary>
    ''' <returns>return "True" If successfully Sets controls properties else "False"  </returns>
    ''' <remarks> Added By Manoj Jadhav on 20111003 </remarks>
    Private Function SetControlProerties() As Boolean
        Select Case _DeviceType
            Case DeviceSettings.WelchAllynECGDevice
                PnlDeviceName.Visible = True
                PnlVitalDeviceInterface.Visible = False
                PnlSpirometryInterface.Visible = False
                pnlECGInterface.Visible = False
                pnlPatientPortal.Visible = False
                lblDeviceName.Text = "WelchAllyn ECG Device"
                Me.Text = "Activation - " & lblDeviceName.Text
                Me.Size = New System.Drawing.Size(614, 181)
                SetControlProerties = True
                ''Added for MU2 Patient Portal implementation on 20130620
                rbIntuitPortal.Visible = False
                rbPatientPortal.Visible = False
                ''End
            Case DeviceSettings.CardiacScienceECGDevice
                PnlDeviceName.Visible = True
                PnlVitalDeviceInterface.Visible = False
                PnlSpirometryInterface.Visible = False
                pnlECGInterface.Visible = True
                pnlPatientPortal.Visible = False
                lblDeviceName.Text = "HeartCentrix ECG Device"
                Me.Text = "Activation - " & lblDeviceName.Text
                Me.Size = New System.Drawing.Size(613, 278)
                SetControlProerties = True
                ''Added for MU2 Patient Portal implementation on 20130620
                rbIntuitPortal.Visible = False
                rbPatientPortal.Visible = False
                ''End
            Case DeviceSettings.IntuitHealthInterface
                PnlDeviceName.Visible = True
                pnlPatientPortal.Visible = False
                PnlVitalDeviceInterface.Visible = False
                PnlSpirometryInterface.Visible = False
                pnlECGInterface.Visible = False
                ''Added for MU2 Patient Portal implimentation on 20130621
                'Me.Text = "Activation - Intuit Health"
                Me.Text = "Activation - Portal"
                ''End
                Me.Size = New System.Drawing.Size(614, 181)
                SetControlProerties = True
                ''Added for MU2 Patient Portal implementation on 20130620
                lblDeviceName.Visible = False
                ''lblDeviceName.Text = "Intuit Health"
                rbIntuitPortal.Visible = True
                rbPatientPortal.Visible = True
                ''End
            Case DeviceSettings.MidmarkSpirometryDevice
                PnlDeviceName.Visible = True
                PnlVitalDeviceInterface.Visible = False
                PnlSpirometryInterface.Visible = True
                pnlECGInterface.Visible = False
                pnlPatientPortal.Visible = False
                lblDeviceName.Text = "Midmark Spirometry Device"
                Me.Text = "Activation - " & lblDeviceName.Text
                Me.Size = New System.Drawing.Size(613, 211)
                SetControlProerties = True
                ''Added for MU2 Patient Portal implementation on 20130620
                rbIntuitPortal.Visible = False
                rbPatientPortal.Visible = False
                ''End
            Case DeviceSettings.WelChAllynVitalDevice
                PnlDeviceName.Visible = True
                PnlVitalDeviceInterface.Visible = True
                PnlSpirometryInterface.Visible = False
                pnlECGInterface.Visible = False
                pnlPatientPortal.Visible = False
                lblDeviceName.Text = "WelchAllyn Vital Device"
                Me.Text = "Activation - " & lblDeviceName.Text
                Me.Size = New System.Drawing.Size(613, 211)
                SetControlProerties = True
                ''Added for MU2 Patient Portal implementation on 20130620
                rbIntuitPortal.Visible = False
                rbPatientPortal.Visible = False
                ''End
            Case DeviceSettings.MidmarkECGDevice
                PnlDeviceName.Visible = True
                PnlVitalDeviceInterface.Visible = False
                PnlSpirometryInterface.Visible = False
                pnlECGInterface.Visible = False
                pnlPatientPortal.Visible = False
                lblDeviceName.Text = "Midmark IQ ECG Device"
                Me.Text = "Activation - " & lblDeviceName.Text
                Me.Size = New System.Drawing.Size(614, 181)
                SetControlProerties = True
                ''Added for MU2 Patient Portal implementation on 20130620
                rbIntuitPortal.Visible = False
                rbPatientPortal.Visible = False
                ''End
            Case DeviceSettings.CCDAService
                PnlDeviceName.Visible = True
                pnlPatientPortal.Visible = False
                PnlVitalDeviceInterface.Visible = False
                PnlSpirometryInterface.Visible = False
                pnlECGInterface.Visible = False
                Me.Text = "Activation - CCDA Service"
                Me.Size = New System.Drawing.Size(614, 181)
                SetControlProerties = True
                lblDeviceName.Visible = False
                ''lblDeviceName.Text = "Intuit Health"
                rbIntuitPortal.Visible = False
                rbPatientPortal.Visible = False
                Label5.Visible = False
                ''End
            Case Else
                PnlDeviceName.Visible = False
                PnlVitalDeviceInterface.Visible = False
                PnlSpirometryInterface.Visible = False
                pnlECGInterface.Visible = False
                pnlPatientPortal.Visible = False
                lblDeviceName.Text = ""
                Me.Text = "Activation"
                SetControlProerties = False
                ''Added for MU2 Patient Portal implementation on 20130620
                rbIntuitPortal.Visible = False
                rbPatientPortal.Visible = False
                ''End
        End Select
    End Function


    ''' <summary>Method to read settings value of selected device</summary>
    ''' <remarks> Added By Manoj Jadhav on 20111003 </remarks>
    ''' 
    Private Sub ReadSettings()
        Dim dtSettingValue As DataTable = Nothing
        Dim sqlqry As String = String.Empty
        Dim objEncrypt As clsEncryption = Nothing
        Try
            objEncrypt = New clsEncryption()
            Select Case _DeviceType

                Case DeviceSettings.WelchAllynECGDevice

                    Dim UseWelchAllynECGDevice As Boolean = False
                    Dim WelchAllynECGDeviceKey As String = String.Empty
                    Try
                        sqlqry = "SELECT [nSettingsID],[sSettingsName],[sSettingsValue] FROM [dbo].[Settings]  WHERE [sSettingsName] IN ('USEWELCHALLYNECGDEVICE','WELCHALLYNECGDEVICEKEY')"
                        dtSettingValue = RetriveSettings(sqlqry)
                        If Not dtSettingValue Is Nothing And dtSettingValue.Rows.Count > 0 Then
                            WelchAllynECGDeviceKey = objEncrypt.DecryptFromBase64String(Convert.ToString((From s In dtSettingValue.AsEnumerable Where s("sSettingsName") = "WELCHALLYNECGDEVICEKEY" Select s("sSettingsValue")).FirstOrDefault()), mdlGeneral.constEncryptDecryptKey)
                            Boolean.TryParse(Convert.ToString((From s In dtSettingValue.AsEnumerable Where s("sSettingsName") = "USEWELCHALLYNECGDEVICE" Select s("sSettingsValue")).FirstOrDefault()), UseWelchAllynECGDevice)
                        End If
                    Catch ex As Exception
                        WelchAllynECGDeviceKey = String.Empty
                        UseWelchAllynECGDevice = False
                        ex = Nothing
                    Finally
                        txtDeviceActivationKey.Text = WelchAllynECGDeviceKey
                        IsSettingChanged = UseWelchAllynECGDevice
                        WelchAllynECGDeviceKey = String.Empty
                        UseWelchAllynECGDevice = False
                    End Try

                Case DeviceSettings.CardiacScienceECGDevice

                    Dim ECGInstutionid As String = String.Empty
                    Dim ECGInterfaceurl As String = String.Empty
                    Dim ECGEnabled As Boolean = False
                    Dim ECGUserproviderid As String = String.Empty
                    Dim ECGDeviceKey As String = String.Empty
                    Try
                        sqlqry = "SELECT [nSettingsID],[sSettingsName],[sSettingsValue] FROM [dbo].[Settings]  WHERE [sSettingsName] IN ('ECGINSTUTIONID','ECGINTERFACEURL','ECGENABLED','ECGUSERPROVIDERID','ECGDEVICEKEY')"
                        dtSettingValue = RetriveSettings(sqlqry)
                        If Not dtSettingValue Is Nothing And dtSettingValue.Rows.Count > 0 Then
                            ECGInstutionid = Convert.ToString((From s In dtSettingValue.AsEnumerable Where s("sSettingsName") = "ECGINSTUTIONID" Select s("sSettingsValue")).FirstOrDefault())
                            ECGInterfaceurl = Convert.ToString((From s In dtSettingValue.AsEnumerable Where s("sSettingsName") = "ECGINTERFACEURL" Select s("sSettingsValue")).FirstOrDefault())
                            Boolean.TryParse(Convert.ToString((From s In dtSettingValue.AsEnumerable Where s("sSettingsName") = "ECGENABLED" Select s("sSettingsValue")).FirstOrDefault()), ECGEnabled)
                            ECGUserproviderid = Convert.ToString((From s In dtSettingValue.AsEnumerable Where s("sSettingsName") = "ECGUSERPROVIDERID" Select s("sSettingsValue")).FirstOrDefault())
                            ECGDeviceKey = objEncrypt.DecryptFromBase64String(Convert.ToString((From s In dtSettingValue.AsEnumerable Where s("sSettingsName") = "ECGDEVICEKEY" Select s("sSettingsValue")).FirstOrDefault()), mdlGeneral.constEncryptDecryptKey)
                        End If
                    Catch ex As Exception
                        ECGInstutionid = String.Empty
                        ECGInterfaceurl = String.Empty
                        ECGEnabled = String.Empty
                        ECGUserproviderid = String.Empty
                        ECGDeviceKey = False
                        ex = Nothing
                    Finally
                        txtECGInterfaceId.Text = ECGInstutionid
                        txtECGInterfaceUrl.Text = ECGInterfaceurl
                        txtECGUserProviderId.Text = ECGUserproviderid
                        txtDeviceActivationKey.Text = ECGDeviceKey
                        IsSettingChanged = ECGEnabled
                        ECGInstutionid = String.Empty
                        ECGInterfaceurl = String.Empty
                        ECGUserproviderid = String.Empty
                        ECGDeviceKey = False
                    End Try


                Case DeviceSettings.IntuitHealthInterface
                    Dim UseIntutinterface As Boolean = False
                    ''Added for MU2 Patient Portal implementation on 20130620
                    Dim UsePatientPortal As Boolean = False
                    ''End
                    Dim IntuitHealthDeviceKey As String = String.Empty
                    Dim sPatPortalSiteNm As String = String.Empty
                    Dim sPatPortalEmailService As String = String.Empty
                    Dim sPatientPortalgloCoreServicesIntallationPath As String = String.Empty



                    Dim _nUserID As Long = 0
                    Try
                        sqlqry = "SELECT [nSettingsID],[sSettingsName],[sSettingsValue] FROM [dbo].[Settings]  WHERE [sSettingsName] IN ('USEINTUITINTERFACE','INTUITINTERFACEKEY','PATIENT PORTAL DEFAULT USER','PatientPortalEnabled','PatientPortalSiteName','PatientPortalEmailService','PatientPortalCoreServicePath','PatientPortalOnlinePaymentMerchantId','PatientPortalOnlinePaymentRegistrationKey','PatientPortalOnlinePaymentEnabled')"
                        dtSettingValue = RetriveSettings(sqlqry)
                        If Not dtSettingValue Is Nothing And dtSettingValue.Rows.Count > 0 Then
                            IntuitHealthDeviceKey = objEncrypt.DecryptFromBase64String(Convert.ToString((From s In dtSettingValue.Rows Where s("sSettingsName") = "INTUITINTERFACEKEY" Select s("sSettingsValue")).FirstOrDefault()), mdlGeneral.constEncryptDecryptKey)
                            Boolean.TryParse(Convert.ToString((From s In dtSettingValue.Rows Where s("sSettingsName") = "USEINTUITINTERFACE" Select s("sSettingsValue")).FirstOrDefault()), UseIntutinterface)
                            Long.TryParse(Convert.ToString((From s In dtSettingValue.Rows Where s("sSettingsName") = "PATIENT PORTAL DEFAULT USER" Select s("sSettingsValue")).FirstOrDefault()), _nUserID)
                            ''Added for MU2 Patient Portal implementation on 20130620
                            Boolean.TryParse(Convert.ToString((From s In dtSettingValue.Rows Where s("sSettingsName") = "PatientPortalEnabled" Select s("sSettingsValue")).FirstOrDefault()), UsePatientPortal)
                            _blnIntuitHealth = UseIntutinterface
                            _blnPatientPortal = UsePatientPortal
                            pnlMu2Portal.Visible = UsePatientPortal
                            If UsePatientPortal = True Then
                                sPatPortalSiteNm = Convert.ToString((From s In dtSettingValue.Rows Where s("sSettingsName") = "PatientPortalSiteName" Select s("sSettingsValue")).FirstOrDefault())
                                sPatPortalEmailService = Convert.ToString((From s In dtSettingValue.Rows Where s("sSettingsName") = "PatientPortalEmailService" Select s("sSettingsValue")).FirstOrDefault())
                                sPatientPortalgloCoreServicesIntallationPath = Convert.ToString((From s In dtSettingValue.Rows Where s("sSettingsName") = "PatientPortalCoreServicePath" Select s("sSettingsValue")).FirstOrDefault())
                                sPatPortalMerchantId = Convert.ToString((From s In dtSettingValue.Rows Where s("sSettingsName") = "PatientPortalOnlinePaymentMerchantId" Select s("sSettingsValue")).FirstOrDefault())
                                sPatPortalRegistrationKey = Convert.ToString((From s In dtSettingValue.Rows Where s("sSettingsName") = "PatientPortalOnlinePaymentRegistrationKey" Select s("sSettingsValue")).FirstOrDefault())
                                sPatientPortalOnlinePaymentEnabled = Convert.ToString((From s In dtSettingValue.Rows Where s("sSettingsName") = "PatientPortalOnlinePaymentEnabled" Select s("sSettingsValue")).FirstOrDefault())

                                If (sPatientPortalOnlinePaymentEnabled = "" Or sPatientPortalOnlinePaymentEnabled.ToLower() = "false") Then
                                    chkOnlinePayment.Checked = False
                                    sPatientPortalOnlinePaymentEnabled = "False"
                                    pnlOnlinePayment.Visible = False
                                    txtMerchantId.Text = ""
                                    txtRegistrationKey.Text = ""
                                    Me.Size = New System.Drawing.Size(640, 282)
                                Else
                                    chkOnlinePayment.Checked = True
                                    sPatientPortalOnlinePaymentEnabled = "True"
                                    pnlOnlinePayment.BringToFront()
                                    pnlOnlinePayment.Visible = True
                                    txtMerchantId.Text = sPatPortalMerchantId
                                    txtRegistrationKey.Text = sPatPortalRegistrationKey
                                    Me.Size = New System.Drawing.Size(640, 344)
                                End If

                                'Me.Size = New System.Drawing.Size(640, 314)
                            End If
                            ''End
                        End If
                    Catch ex As Exception
                        ex = Nothing
                        IntuitHealthDeviceKey = String.Empty
                        UseIntutinterface = False
                    Finally
                        txtDeviceActivationKey.Text = IntuitHealthDeviceKey
                        ''Added for MU2 Patient Portal implementation on 20130620
                        ''IsSettingChanged = UseIntutinterface
                        txtPortalSiteNm.Text = sPatPortalSiteNm
                        '   txtPatientPortalEmailService.Text = sPatPortalEmailService
                       ' txtPatientPortalgloCoreServicesInstallationPath.Text = sPatientPortalgloCoreServicesIntallationPath
                        txtMerchantId.Text = sPatPortalMerchantId
                        txtRegistrationKey.Text = sPatPortalRegistrationKey

                        RemoveHandler rbIntuitPortal.CheckedChanged, AddressOf rbIntuitPortal_CheckedChanged
                        RemoveHandler rbPatientPortal.CheckedChanged, AddressOf rbPatientPortal_CheckedChanged
                        rbIntuitPortal.Checked = UseIntutinterface
                        rbPatientPortal.Checked = UsePatientPortal
                        AddHandler rbIntuitPortal.CheckedChanged, AddressOf rbIntuitPortal_CheckedChanged
                        AddHandler rbPatientPortal.CheckedChanged, AddressOf rbPatientPortal_CheckedChanged
                        If rbIntuitPortal.Checked Then
                            IsSettingChanged = UseIntutinterface
                        ElseIf rbPatientPortal.Checked Then
                            IsSettingChanged = UsePatientPortal
                            IsPatientPortalActivated = UsePatientPortal
                        End If
                        ''End

                        _PatientPortal_DefaultUserID = _nUserID
                        Dim sUserName As String = String.Empty
                        If _PatientPortal_DefaultUserID > 0 Then
                            sUserName = GetDefaultUserName(_PatientPortal_DefaultUserID)
                        End If

                        If Not IsNothing(sUserName) Then
                            _PatientPortal_defaultUserName = sUserName
                            txtPatientPortalTask_DefaultUser.Text = sUserName
                        Else
                            _PatientPortal_defaultUserName = String.Empty
                            txtPatientPortalTask_DefaultUser.Text = String.Empty
                        End If
                        IntuitHealthDeviceKey = String.Empty
                        UseIntutinterface = False

                        ChangeRadioButtonStyle()


                    End Try


                Case DeviceSettings.MidmarkSpirometryDevice

                    Dim UseMidmarkSpirometryDevice As Boolean = False
                    Dim MidmarkSpirometryDeviceKey As String = String.Empty
                    Dim MidmarkSpirometryDeviceOrderPrefix As String = String.Empty
                    Try
                        sqlqry = "SELECT [nSettingsID],[sSettingsName],[sSettingsValue] FROM [dbo].[Settings]  WHERE [sSettingsName] IN ('USESPIROMETRYDEVICE','SPIROMETRYDEVICEKEY','SPIROMETRYDEVICEORDERPREFIX')"
                        dtSettingValue = RetriveSettings(sqlqry)
                        If Not dtSettingValue Is Nothing And dtSettingValue.Rows.Count > 0 Then
                            MidmarkSpirometryDeviceKey = objEncrypt.DecryptFromBase64String(Convert.ToString((From s In dtSettingValue.Rows Where s("sSettingsName") = "SPIROMETRYDEVICEKEY" Select s("sSettingsValue")).FirstOrDefault()), mdlGeneral.constEncryptDecryptKey)
                            MidmarkSpirometryDeviceOrderPrefix = Convert.ToString((From s In dtSettingValue.Rows Where s("sSettingsName") = "SPIROMETRYDEVICEORDERPREFIX" Select s("sSettingsValue")).FirstOrDefault())
                            Boolean.TryParse(Convert.ToString((From s In dtSettingValue.Rows Where s("sSettingsName") = "USESPIROMETRYDEVICE" Select s("sSettingsValue")).FirstOrDefault()), UseMidmarkSpirometryDevice)
                        End If
                    Catch ex As Exception
                        MidmarkSpirometryDeviceKey = String.Empty
                        MidmarkSpirometryDeviceOrderPrefix = "SPI"
                        UseMidmarkSpirometryDevice = False
                    Finally
                        If MidmarkSpirometryDeviceOrderPrefix.Trim().Length <= 0 Then
                            MidmarkSpirometryDeviceOrderPrefix = "SPI"
                        End If
                        txtDeviceActivationKey.Text = MidmarkSpirometryDeviceKey
                        txtPrefixForSpirometryDevice.Text = MidmarkSpirometryDeviceOrderPrefix
                        IsSettingChanged = UseMidmarkSpirometryDevice
                        MidmarkSpirometryDeviceKey = String.Empty
                        MidmarkSpirometryDeviceOrderPrefix = String.Empty
                        UseMidmarkSpirometryDevice = False
                    End Try


                Case DeviceSettings.WelChAllynVitalDevice

                    Dim UseVitaldevice As Boolean = False
                    Dim VitalDeviceKey As String = String.Empty
                    Dim NoOfAttemptToConnectVitalDevice As String = String.Empty
                    Try
                        sqlqry = "SELECT [nSettingsID],[sSettingsName],[sSettingsValue] FROM [dbo].[Settings]  WHERE [sSettingsName] IN ('USEVITALDEVICE','VITALDEVICEKEY','NOOFATTEMPTTOCONNECTVITALDEVICE')"
                        dtSettingValue = RetriveSettings(sqlqry)
                        If Not dtSettingValue Is Nothing And dtSettingValue.Rows.Count > 0 Then
                            VitalDeviceKey = objEncrypt.DecryptFromBase64String(Convert.ToString((From s In dtSettingValue.Rows Where s("sSettingsName") = "VITALDEVICEKEY" Select s("sSettingsValue")).FirstOrDefault()), mdlGeneral.constEncryptDecryptKey)
                            NoOfAttemptToConnectVitalDevice = Convert.ToString((From s In dtSettingValue.Rows Where s("sSettingsName") = "NOOFATTEMPTTOCONNECTVITALDEVICE" Select s("sSettingsValue")).FirstOrDefault())
                            Boolean.TryParse(Convert.ToString((From s In dtSettingValue.Rows Where s("sSettingsName") = "USEVITALDEVICE" Select s("sSettingsValue")).FirstOrDefault()), UseVitaldevice)
                        End If
                    Catch ex As Exception
                        VitalDeviceKey = String.Empty
                        NoOfAttemptToConnectVitalDevice = 5
                        UseVitaldevice = False
                        ex = Nothing
                    Finally
                        If Not IsNumeric(NoOfAttemptToConnectVitalDevice) Then
                            NoOfAttemptToConnectVitalDevice = "5"
                        End If
                        txtDeviceActivationKey.Text = VitalDeviceKey
                        nup_NoofAttemptstoConnectVitalDevice.Value = NoOfAttemptToConnectVitalDevice
                        IsSettingChanged = UseVitaldevice
                        VitalDeviceKey = String.Empty
                        NoOfAttemptToConnectVitalDevice = String.Empty
                    End Try

                Case DeviceSettings.MidmarkECGDevice
                    Dim UseMidmarkECGDevice As Boolean = False
                    Dim MidmrakECGDeviceKey As String = String.Empty
                    Try
                        sqlqry = "SELECT [nSettingsID],[sSettingsName],[sSettingsValue] FROM [dbo].[Settings]  WHERE [sSettingsName] IN ('USEMIDMARKECGDEVICE','MIDMARKECGDEVICEKEY')"
                        dtSettingValue = RetriveSettings(sqlqry)
                        If Not dtSettingValue Is Nothing And dtSettingValue.Rows.Count > 0 Then
                            MidmrakECGDeviceKey = objEncrypt.DecryptFromBase64String(Convert.ToString((From s In dtSettingValue.Rows Where s("sSettingsName") = "MIDMARKECGDEVICEKEY" Select s("sSettingsValue")).FirstOrDefault()), mdlGeneral.constEncryptDecryptKey)
                            Boolean.TryParse(Convert.ToString((From s In dtSettingValue.Rows Where s("sSettingsName") = "USEMIDMARKECGDEVICE" Select s("sSettingsValue")).FirstOrDefault()), UseMidmarkECGDevice)
                        End If
                    Catch ex As Exception
                        MidmrakECGDeviceKey = String.Empty
                        UseMidmarkECGDevice = False
                        ex = Nothing
                    Finally
                        txtDeviceActivationKey.Text = MidmrakECGDeviceKey
                        IsSettingChanged = UseMidmarkECGDevice
                        MidmrakECGDeviceKey = String.Empty
                    End Try
                Case DeviceSettings.CCDAService
                    Dim UseExportCCDAService As Boolean = False
                    Dim ExportCCDAServiceKey As String = String.Empty
                    Try
                        sqlqry = "SELECT [nSettingsID],[sSettingsName],[sSettingsValue] FROM [dbo].[Settings]  WHERE [sSettingsName] IN ('USECCDADATAEXPORTSERVICE','CCDADATAEXPORTSERVICEKEY')"
                        dtSettingValue = RetriveSettings(sqlqry)
                        If Not dtSettingValue Is Nothing And dtSettingValue.Rows.Count > 0 Then
                            ExportCCDAServiceKey = objEncrypt.DecryptFromBase64String(Convert.ToString((From s In dtSettingValue.Rows Where s("sSettingsName") = "CCDADATAEXPORTSERVICEKEY" Select s("sSettingsValue")).FirstOrDefault()), mdlGeneral.constEncryptDecryptKey)
                            Boolean.TryParse(Convert.ToString((From s In dtSettingValue.Rows Where s("sSettingsName") = "USECCDADATAEXPORTSERVICE" Select s("sSettingsValue")).FirstOrDefault()), UseExportCCDAService)
                        End If
                    Catch ex As Exception
                        ExportCCDAServiceKey = String.Empty
                        UseExportCCDAService = False
                        ex = Nothing
                    Finally
                        txtDeviceActivationKey.Text = ExportCCDAServiceKey
                        IsSettingChanged = UseExportCCDAService
                        ExportCCDAServiceKey = String.Empty
                    End Try
            End Select
        Catch ex As Exception
            ex = Nothing
        Finally
            sqlqry = String.Empty
            objEncrypt = Nothing
            If Not dtSettingValue Is Nothing Then
                dtSettingValue.Dispose()
                dtSettingValue = Nothing
            End If
        End Try




    End Sub


    ''' <summary>Function to Retrieve Result from database</summary>
    ''' <param name="sqlqry" >take query as parameter</param>
    ''' <returns>return DataTable as out put of query</returns> 
    ''' <remarks> Added By Manoj Jadhav on 20111003 </remarks>
    Private Function RetriveSettings(ByVal sqlqry As String) As DataTable
        Dim OdbLayer As gloDatabaseLayer.DBLayer = Nothing
        Dim dtSettings As DataTable = Nothing
        Try
            OdbLayer = New gloDatabaseLayer.DBLayer(gloEMRAdmin.mdlGeneral.GetConnectionString())
            OdbLayer.Connect(False)
            OdbLayer.Retrive_Query(sqlqry, dtSettings)
            OdbLayer.Disconnect()
        Catch ex As Exception
            ex = Nothing
        Finally
            If Not OdbLayer Is Nothing Then
                OdbLayer.Dispose()
                OdbLayer = Nothing
            End If
            RetriveSettings = dtSettings
        End Try
    End Function


    ''' <summary>Function to give call to save settings for selected device</summary>
    ''' <returns>return "True" Or "False"</returns> 
    ''' <remarks> Added By Manoj Jadhav on 20111003 </remarks>
    Private Function InUPDeviceSettings() As Boolean

        Select Case _DeviceType

            Case DeviceSettings.WelchAllynECGDevice

                InUPDeviceSettings = In_UPWelchAllynECGDeviceSettings()

            Case DeviceSettings.CardiacScienceECGDevice

                InUPDeviceSettings = InUp_CardioScienceECGDeviceSetting()

            Case DeviceSettings.IntuitHealthInterface
                IsPatientPortalActivated = False
                Dim strMsg As String = String.Empty
                If rbIntuitPortal.Checked Then
                    strMsg = "Do you want to activate INTUIT Patient Portal Health Interface?" + Environment.NewLine + "Note: Please work with gloStream Implementation/Support team to configure the interface."
                ElseIf rbPatientPortal.Checked Then
                    strMsg = "Do you want to activate gloStream Patient Portal Health Interface?" + Environment.NewLine + "Note: Please work with gloStream Implementation/Support team to configure the interface."
                    IsPatientPortalActivated = True
                End If
                If strMsg <> String.Empty Then
                    Dim _Result As Integer = Convert.ToInt32(MessageBox.Show(strMsg, gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3))
                    If _Result = Convert.ToInt32(DialogResult.Yes) Then
                        InUPDeviceSettings = InUP_IntuitHealthInterface()
                    Else
                        Exit Function
                    End If
                End If

            Case DeviceSettings.CCDAService

                Dim strMsg As String

                strMsg = "Do you want to activate gloCCDA data export service?" + Environment.NewLine + Environment.NewLine + "Note: Please work with gloStream Implementation/Support team to configure the service."

                InUPDeviceSettings = InUP_CCDAExportService()

                If InUPDeviceSettings = True Then

                    Dim _Result As Integer = Convert.ToInt32(MessageBox.Show(strMsg, gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3))

                    If _Result <> Convert.ToInt32(DialogResult.Yes) Then
                        InUPDeviceSettings = False
                    End If

                End If


            Case DeviceSettings.MidmarkSpirometryDevice

                InUPDeviceSettings = InUp_MidmarkSpirometryDeviceSetting()

            Case DeviceSettings.WelChAllynVitalDevice

                InUPDeviceSettings = In_UPVitalDeviceSettings()
            Case DeviceSettings.MidmarkECGDevice

                InUPDeviceSettings = InUP_MidmarkECGDevice()

            Case Else

                InUPDeviceSettings = False

        End Select
    End Function


    ''' <summary>Function to store WelchAllyn ECG Device Settings into Database</summary>
    ''' <returns>return "True" Or "False"</returns> 
    ''' <remarks> Added By Manoj Jadhav on 20111003 </remarks>
    Private Function In_UPWelchAllynECGDeviceSettings() As Boolean

        If txtDeviceActivationKey.Text.Trim.Length <= 0 Then
            MessageBox.Show("Enter WelchAllyn ECG device interface activation key", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtDeviceActivationKey.Focus()
            In_UPWelchAllynECGDeviceSettings = False
            Exit Function
        End If

        Dim objEncrypt As clsEncryption = Nothing
        Try
            objEncrypt = New clsEncryption()
            If Not (objEncrypt.EncryptToBase64String(String.Concat(lblAUSUserName.Text.Trim().ToLower(), "gL0@PPs2k9!8610"), "87654321") = txtDeviceActivationKey.Text.Trim) Then
                MessageBox.Show("Enter valid WelchAllyn ECG device interface activation key", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtDeviceActivationKey.Focus()
                In_UPWelchAllynECGDeviceSettings = False
                Exit Function
            End If

            If UpdateSettings("WELCHALLYNECGDEVICEKEY", objEncrypt.EncryptToBase64String(txtDeviceActivationKey.Text.Trim, mdlGeneral.constEncryptDecryptKey)) And UpdateSettings("USEWELCHALLYNECGDEVICE", "True") Then
                In_UPWelchAllynECGDeviceSettings = True
            End If

        Catch ex As Exception
            In_UPWelchAllynECGDeviceSettings = False
            ex = Nothing
        Finally
            objEncrypt = Nothing
        End Try
    End Function

    ''' <summary>Function to store WelchAllyn Vital Device Settings into Database</summary>
    ''' <returns>return "True" Or "False"</returns> 
    ''' <remarks> Added By Manoj Jadhav on 20111003 </remarks>
    Private Function In_UPVitalDeviceSettings() As Boolean

        If txtDeviceActivationKey.Text.Trim.Length <= 0 Then
            MessageBox.Show("Enter WelchAllyn vital device interface activation key", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtDeviceActivationKey.Focus()
            In_UPVitalDeviceSettings = False
            Exit Function
        End If

        Dim objEncrypt As clsEncryption = Nothing
        Try
            objEncrypt = New clsEncryption()
            If Not (objEncrypt.EncryptToBase64String(String.Concat(lblAUSUserName.Text.Trim().ToLower(), "gL0@PPs2k9!"), "87654321") = txtDeviceActivationKey.Text.Trim) Then
                MessageBox.Show("Enter valid WelchAllyn vital device interface activation key", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtDeviceActivationKey.Focus()
                In_UPVitalDeviceSettings = False
                Exit Function
            End If
            If Not IsNumeric(nup_NoofAttemptstoConnectVitalDevice.Value) Or nup_NoofAttemptstoConnectVitalDevice.Value = 0 Then
                nup_NoofAttemptstoConnectVitalDevice.Value = 5
            End If
            If UpdateSettings("VITALDEVICEKEY", objEncrypt.EncryptToBase64String(txtDeviceActivationKey.Text.Trim, mdlGeneral.constEncryptDecryptKey)) And UpdateSettings("NOOFATTEMPTTOCONNECTVITALDEVICE", nup_NoofAttemptstoConnectVitalDevice.Value) And UpdateSettings("USEVITALDEVICE", "True") Then
                In_UPVitalDeviceSettings = True
            End If

        Catch ex As Exception
            In_UPVitalDeviceSettings = False
            ex = Nothing
        Finally
            objEncrypt = Nothing
        End Try
    End Function

    ''' <summary>Function to store Midmark Spirometry Device Settings into Database</summary>
    ''' <returns>return "True" Or "False"</returns> 
    ''' <remarks> Added By Manoj Jadhav on 20111003 </remarks>
    Private Function InUp_MidmarkSpirometryDeviceSetting() As Boolean
        If txtDeviceActivationKey.Text.Trim.Length <= 0 Then
            MessageBox.Show("Enter Midmark spirometer device interface activation key", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtDeviceActivationKey.Focus()
            InUp_MidmarkSpirometryDeviceSetting = False
            Exit Function
        End If

        Dim objEncrypt As clsEncryption = Nothing
        Try
            objEncrypt = New clsEncryption()

            If Not (objEncrypt.EncryptToBase64String(String.Concat(lblAUSUserName.Text.Trim().ToLower(), "gL0@PPs2k9!8228"), "87654321") = txtDeviceActivationKey.Text.Trim) Then
                MessageBox.Show("Enter valid Midmark spirometer device interface activation key", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtDeviceActivationKey.Focus()
                Exit Function
            End If

            If txtPrefixForSpirometryDevice.Text.Trim().Length <= 0 Then
                MessageBox.Show("Enter Prefix for Spirometry Device Order", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtPrefixForSpirometryDevice.Focus()
                Exit Function
            End If

            If UpdateSettings("SPIROMETRYDEVICEKEY", objEncrypt.EncryptToBase64String(txtDeviceActivationKey.Text.Trim, mdlGeneral.constEncryptDecryptKey)) And UpdateSettings("SPIROMETRYDEVICEORDERPREFIX", txtPrefixForSpirometryDevice.Text.Trim()) And UpdateSettings("USESPIROMETRYDEVICE", "True") Then
                InUp_MidmarkSpirometryDeviceSetting = True
            End If

        Catch ex As Exception
            InUp_MidmarkSpirometryDeviceSetting = False
            ex = Nothing
        Finally
            objEncrypt = Nothing
        End Try

    End Function

    ''' <summary>Function to store Cardio Science ECG Device Settings into Database</summary>
    ''' <returns>return "True" Or "False"</returns> 
    ''' <remarks> Added By Manoj Jadhav on 20111003 </remarks>
    Private Function InUp_CardioScienceECGDeviceSetting() As Boolean

        If txtDeviceActivationKey.Text.Trim.Length <= 0 Then
            MessageBox.Show("Enter HeartCentrix ECG device interface activation key", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtDeviceActivationKey.Focus()
            InUp_CardioScienceECGDeviceSetting = False
            Exit Function
        End If

        If txtECGInterfaceId.Text.Trim().Length <= 0 Then
            MessageBox.Show("Enter HeartCentrix ECG device interface Institution id", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtECGInterfaceId.Focus()
            InUp_CardioScienceECGDeviceSetting = False
            Exit Function
        End If

        If txtECGInterfaceUrl.Text.Trim().Length <= 0 Then
            MessageBox.Show("Enter HeartCentrix ECG device interface URL", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtECGInterfaceUrl.Focus()
            InUp_CardioScienceECGDeviceSetting = False
            Exit Function
        End If
        If txtECGUserProviderId.Text.Trim().Length <= 0 Then
            MessageBox.Show("Enter HeartCentrix ECG device interface provider id", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtECGUserProviderId.Focus()
            InUp_CardioScienceECGDeviceSetting = False
            Exit Function
        End If

        Dim objEncrypt As clsEncryption = Nothing
        Try

            objEncrypt = New clsEncryption()

            If Not (objEncrypt.EncryptToBase64String(String.Concat(lblAUSUserName.Text.Trim().ToLower(), "gL0@PPs2k9!8605"), "87654321") = txtDeviceActivationKey.Text.Trim) Then
                MessageBox.Show("Enter valid HeartCentrix ECG device interface activation key", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtDeviceActivationKey.Focus()
                Exit Function
            End If

            If UpdateSettings("ECGDEVICEKEY", objEncrypt.EncryptToBase64String(txtDeviceActivationKey.Text.Trim, mdlGeneral.constEncryptDecryptKey)) And UpdateSettings("ECGINSTUTIONID", txtECGInterfaceId.Text.Trim()) And UpdateSettings("ECGINTERFACEURL", txtECGInterfaceUrl.Text.Trim()) And UpdateSettings("ECGUSERPROVIDERID", txtECGUserProviderId.Text.Trim()) And UpdateSettings("ECGENABLED", "True") Then
                InUp_CardioScienceECGDeviceSetting = True
            End If

        Catch ex As Exception
            InUp_CardioScienceECGDeviceSetting = False
            ex = Nothing
        Finally
            objEncrypt = Nothing
        End Try

    End Function

    ''' <summary>Function to store IntuitHealth Device Settings into Database</summary>
    ''' <returns>return "True" Or "False"</returns> 
    ''' <remarks> Added By Manoj Jadhav on 20111003 </remarks>
    Private Function InUP_IntuitHealthInterface() As Boolean
        ''Added for Fixed Bug id 55275 on 20130806
        Dim strMessage As String = "Enter valid intuit health interface activation key"
        If rbPatientPortal.Checked Then
            strMessage = "Enter valid patient portal interface activation key"
        End If
        ''End
        If txtDeviceActivationKey.Text.Trim.Length <= 0 Then
            MessageBox.Show(strMessage, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtDeviceActivationKey.Focus()
            InUP_IntuitHealthInterface = False
            Exit Function
            'ElseIf txtPatientPortalTask_DefaultUser.Text.Trim.Length = 0 Then
            '    MessageBox.Show("Select Default User to Review Patient Portal tasks", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            '    btnPatientPortalTaskUserSearch.Focus()
            '    InUP_IntuitHealthInterface = False
            '    Exit Function
        End If

        ''Added for MU2 Patient portal implementation on 20130627
        If rbPatientPortal.Checked Then
            If txtPortalSiteNm.Text.Trim = String.Empty Then
                MessageBox.Show("Enter patient portal site name", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtPortalSiteNm.Focus()
                InUP_IntuitHealthInterface = False
                Exit Function
            End If

            'If txtPatientPortalEmailService.Text.Trim = String.Empty Then
            '    MessageBox.Show("Enter patient portal Email service address", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            '    txtPatientPortalEmailService.Focus()
            '    InUP_IntuitHealthInterface = False
            '    Exit Function
            'End If

            'If txtPatientPortalgloCoreServicesInstallationPath.Text.Trim = String.Empty Then
            '    MessageBox.Show("Enter gloCore service installation path", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            '    txtPatientPortalgloCoreServicesInstallationPath.Focus()
            '    InUP_IntuitHealthInterface = False
            '    Exit Function
            'End If
            'If Not System.IO.Directory.Exists(txtPatientPortalgloCoreServicesInstallationPath.Text.Trim) Then
            '    MessageBox.Show("Enter valid gloCore service installation path", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            '    txtPatientPortalgloCoreServicesInstallationPath.Focus()
            '    InUP_IntuitHealthInterface = False
            '    Exit Function
            'End If

            'If (Not (txtPatientPortalgloCoreServicesInstallationPath.Text.Trim = String.Empty)) Then
            '    If Not System.IO.Directory.Exists(txtPatientPortalgloCoreServicesInstallationPath.Text.Trim) Then
            '        MessageBox.Show("Enter valid gloCore service installation path", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            '        txtPatientPortalgloCoreServicesInstallationPath.Focus()
            '        InUP_IntuitHealthInterface = False
            '        Exit Function
            '    End If
            'End If

            If chkOnlinePayment.Checked Then
                If txtMerchantId.Text.Trim = String.Empty Then
                    MessageBox.Show("Enter Merchant Id", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    txtMerchantId.Focus()
                    InUP_IntuitHealthInterface = False
                    Exit Function
                End If

                If txtRegistrationKey.Text.Trim = String.Empty Then
                    MessageBox.Show("Enter Registration Key", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    txtRegistrationKey.Focus()
                    InUP_IntuitHealthInterface = False
                    Exit Function
                End If
            End If

        End If
        ''End
        Dim objEncrypt As clsEncryption = Nothing
        Try

            objEncrypt = New clsEncryption()

            If Not (objEncrypt.EncryptToBase64String(String.Concat(lblAUSUserName.Text.Trim.ToLower, "gL0@PPs2k9!8709"), "87654321") = txtDeviceActivationKey.Text.Trim) Then
                MessageBox.Show(strMessage, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtDeviceActivationKey.Focus()
                InUP_IntuitHealthInterface = False
                Exit Function
            End If
            ''Added 'PatientPortalEnabled' setting for MU2 Patient Portal implementation on 20130620
            ''Added Trim for txtPortalSiteNm.Text and txtPatientPortalEmailService.Text as it saves space at the end.

            If chkOnlinePayment.Checked = False Then
                txtMerchantId.Text = ""
                txtRegistrationKey.Text = ""
                sPatientPortalOnlinePaymentEnabled = "False"
            Else
                sPatientPortalOnlinePaymentEnabled = "True"
            End If

            If UpdateSettings("INTUITINTERFACEKEY", objEncrypt.EncryptToBase64String(txtDeviceActivationKey.Text.Trim, mdlGeneral.constEncryptDecryptKey)) And UpdateSettings("USEINTUITINTERFACE", rbIntuitPortal.Checked) And UpdateSettings("PATIENT PORTAL DEFAULT USER", _PatientPortal_DefaultUserID.ToString()) And UpdateSettings("PatientPortalEnabled", rbPatientPortal.Checked) And UpdateSettings("PatientPortalSiteName", txtPortalSiteNm.Text.Trim) And UpdateSettings("PatientPortalEmailService", txtPatientPortalEmailService.Text.Trim) And UpdateSettings("PatientPortalCoreServicePath", txtPatientPortalgloCoreServicesInstallationPath.Text.Trim) And UpdateSettings("PatientPortalOnlinePaymentMerchantId", txtMerchantId.Text.Trim()) And UpdateSettings("PatientPortalOnlinePaymentRegistrationKey", txtRegistrationKey.Text.Trim()) And UpdateSettings("PatientPortalOnlinePaymentEnabled", sPatientPortalOnlinePaymentEnabled) Then
                InUP_IntuitHealthInterface = True
            End If

        Catch ex As Exception
            InUP_IntuitHealthInterface = False
            ex = Nothing
        Finally
            objEncrypt = Nothing
        End Try

    End Function


    ''' <summary>Function to store CCDA Data Export Service Authentication Settings into Database</summary>
    ''' <returns>return "True" Or "False"</returns> 

    Private Function InUP_CCDAExportService() As Boolean
        Dim strMessage As String = "Enter valid gloCCDA data export service activation key."
        If txtDeviceActivationKey.Text.Trim.Length <= 0 Then
            MessageBox.Show(strMessage, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtDeviceActivationKey.Focus()
            InUP_CCDAExportService = False
            Exit Function
        End If
        Dim objEncrypt As clsEncryption = Nothing
        Try
            objEncrypt = New clsEncryption()
            If Not (objEncrypt.EncryptToBase64String(String.Concat(lblAUSUserName.Text.Trim.ToLower, "gL0@PPs2k9!2576"), "87654321") = txtDeviceActivationKey.Text.Trim) Then
                MessageBox.Show(strMessage, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtDeviceActivationKey.Focus()
                InUP_CCDAExportService = False
                Exit Function
            End If

            If UpdateSettings("CCDADATAEXPORTSERVICEKEY", objEncrypt.EncryptToBase64String(txtDeviceActivationKey.Text.Trim, mdlGeneral.constEncryptDecryptKey)) And UpdateSettings("USECCDADATAEXPORTSERVICE", True) Then
                InUP_CCDAExportService = True
            End If

        Catch ex As Exception
            InUP_CCDAExportService = False
            ex = Nothing
        Finally
            objEncrypt = Nothing
        End Try

    End Function




    ''' <summary>Function to store Midmark ECG Device Settings into Database</summary>
    ''' <param name="sSettingName" ></param>
    ''' <param name="sSettingValue"></param> 
    ''' <returns>return "True" Or "False"</returns> 
    ''' <remarks> Added By Manoj Jadhav on 20120326 </remarks>
    Private Function InUP_MidmarkECGDevice() As Boolean

        If txtDeviceActivationKey.Text.Trim.Length <= 0 Then
            MessageBox.Show("Enter Midmark IQ ECG device activation key", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtDeviceActivationKey.Focus()
            InUP_MidmarkECGDevice = False
            Exit Function
        End If
        Dim objEncrypt As clsEncryption = Nothing
        Try

            objEncrypt = New clsEncryption()

            If Not (objEncrypt.EncryptToBase64String(String.Concat(lblAUSUserName.Text.Trim.ToLower, "gL0@PPs2k9!7482"), "87654321") = txtDeviceActivationKey.Text.Trim) Then
                MessageBox.Show("Enter valid Midmark IQ ECG device activation key", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtDeviceActivationKey.Focus()
                InUP_MidmarkECGDevice = False
                Exit Function
            End If

            If UpdateSettings("MIDMARKECGDEVICEKEY", objEncrypt.EncryptToBase64String(txtDeviceActivationKey.Text.Trim, mdlGeneral.constEncryptDecryptKey)) And UpdateSettings("USEMIDMARKECGDEVICE", "True") Then
                InUP_MidmarkECGDevice = True
            End If

        Catch ex As Exception
            InUP_MidmarkECGDevice = False
            ex = Nothing
        Finally
            objEncrypt = Nothing
        End Try

    End Function


    ''' <summary>Function to store IntuitHealth Device Settings into Database</summary>
    ''' <param name="sSettingName" ></param>
    ''' <param name="sSettingValue"></param> 
    ''' <returns>return "True" Or "False"</returns> 
    ''' <remarks> Added By Manoj Jadhav on 20111003 </remarks>
    Public Function UpdateSettings(ByVal sSettingName As String, ByVal sSettingValue As String) As Boolean
        Dim objSettings As clsSettings = Nothing
        Try
            objSettings = New clsSettings()
            UpdateSettings = objSettings.Add(sSettingName, sSettingValue, nClinicID, nUSerId, gloEMRAdmin.SettingFlag.None)
        Catch ex As Exception
            ex = Nothing
            UpdateSettings = False
        Finally
            objSettings = Nothing
        End Try
    End Function

    Private Function GetAUSName() As String
        Try
            GetAUSName = frmSettings_New.GetClinicInformation("sExternalcode")
        Catch ex As Exception
            ex = Nothing
            GetAUSName = String.Empty
        End Try
    End Function

    Public Function DiasblDeviceSettings(ByVal DeviceType As DeviceSettings) As Boolean
        Try
            DiasblDeviceSettings = False
            Select Case DeviceType

                Case DeviceSettings.WelchAllynECGDevice

                    If UpdateSettings("WELCHALLYNECGDEVICEKEY", String.Empty) And UpdateSettings("USEWELCHALLYNECGDEVICE", "False") Then
                        DiasblDeviceSettings = True
                    End If

                Case DeviceSettings.CardiacScienceECGDevice

                    If UpdateSettings("ECGINSTUTIONID", String.Empty) And UpdateSettings("ECGINTERFACEURL", String.Empty) And UpdateSettings("ECGUSERPROVIDERID", String.Empty) And UpdateSettings("ECGDEVICEKEY", String.Empty) And UpdateSettings("ECGENABLED", "False") Then
                        DiasblDeviceSettings = True
                    End If

                Case DeviceSettings.IntuitHealthInterface
                    ''Added 'PatientPortalEnabled' for MU2 Patient Portal implementation on 20130620
                    If UpdateSettings("INTUITINTERFACEKEY", String.Empty) And UpdateSettings("USEINTUITINTERFACE", "False") And UpdateSettings("PATIENT PORTAL DEFAULT USER", "0") And UpdateSettings("PatientPortalEnabled", "False") And UpdateSettings("PatientPortalSiteName", String.Empty) Then
                        DiasblDeviceSettings = True
                    End If

                Case DeviceSettings.CCDAService
                    If UpdateSettings("CCDADATAEXPORTSERVICEKEY", String.Empty) And UpdateSettings("USECCDADATAEXPORTSERVICE", "False") Then
                        DiasblDeviceSettings = True
                    End If
                Case DeviceSettings.MidmarkSpirometryDevice

                    If UpdateSettings("SPIROMETRYDEVICEORDERPREFIX", String.Empty) And UpdateSettings("SPIROMETRYDEVICEKEY", String.Empty) And UpdateSettings("USESPIROMETRYDEVICE", "False") Then
                        DiasblDeviceSettings = True
                    End If


                Case DeviceSettings.WelChAllynVitalDevice


                    If UpdateSettings("NOOFATTEMPTTOCONNECTVITALDEVICE", String.Empty) And UpdateSettings("VITALDEVICEKEY", String.Empty) And UpdateSettings("USEVITALDEVICE", "False") Then
                        DiasblDeviceSettings = True
                    End If

                Case DeviceSettings.MidmarkECGDevice

                    If UpdateSettings("MIDMARKECGDEVICEKEY", String.Empty) And UpdateSettings("USEMIDMARKECGDEVICE", "False") Then
                        DiasblDeviceSettings = True
                    End If

                Case Else



            End Select
        Catch ex As Exception

        End Try

    End Function

    Private Function GetDefaultUserName(ByVal defaultUserid As Int64) As String

        Dim oDB As gloDatabaseLayer.DBLayer
        Dim obj As Object
        Dim sReturnValue As String = String.Empty
        Try
            oDB = New gloDatabaseLayer.DBLayer(gloEMRAdmin.mdlGeneral.GetConnectionString())
            oDB.Connect(False)
            Dim _strSQL As String = "select sLoginName from User_MST where nUserID = " & defaultUserid

            obj = oDB.ExecuteScalar_Query(_strSQL)
            If Not IsNothing(obj) Then
                sReturnValue = Convert.ToString(obj)
            Else
                sReturnValue = String.Empty
            End If

            Return sReturnValue
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
            Return sReturnValue
        Finally
            If Not IsNothing(oDB) Then
                oDB.Dispose()
                oDB = Nothing
            End If
            obj = Nothing
            sReturnValue = Nothing
        End Try
    End Function

#End Region

    Private Sub rbIntuitPortal_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbIntuitPortal.CheckedChanged
        If _blnIntuitHealth <> False Or _blnPatientPortal <> False Then
            If rbIntuitPortal.Checked Then
                MessageBox.Show("The requested operation requires data validation and interface configuration changes." + Environment.NewLine + " Please contact gloStream Support at " + """support@glostream.com""" + "", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                RemoveHandler rbIntuitPortal.CheckedChanged, AddressOf rbIntuitPortal_CheckedChanged
                RemoveHandler rbPatientPortal.CheckedChanged, AddressOf rbPatientPortal_CheckedChanged
                rbIntuitPortal.Checked = _blnIntuitHealth
                rbPatientPortal.Checked = _blnPatientPortal
                AddHandler rbIntuitPortal.CheckedChanged, AddressOf rbIntuitPortal_CheckedChanged
                AddHandler rbPatientPortal.CheckedChanged, AddressOf rbPatientPortal_CheckedChanged

                If sPatientPortalOnlinePaymentEnabled.ToLower = "true" Then
                    pnlOnlinePayment.BringToFront()
                    pnlOnlinePayment.Visible = True
                    Me.Size = New System.Drawing.Size(640, 344)
                Else
                    pnlOnlinePayment.Visible = False
                    Me.Size = New System.Drawing.Size(640, 282)
                End If

                ' Me.Size = New System.Drawing.Size(640, 260)
            End If
        Else
            pnlMu2Portal.Visible = False
            pnlOnlinePayment.Visible = False
            If rbIntuitPortal.Checked Then
                Me.Size = New System.Drawing.Size(614, 181)
            ElseIf rbPatientPortal.Checked Then
                Me.Size = New System.Drawing.Size(614, 260)

            End If
        End If
        ChangeRadioButtonStyle()
    End Sub

    Private Sub rbPatientPortal_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbPatientPortal.CheckedChanged
        If _blnIntuitHealth <> False Or _blnPatientPortal <> False Then
            If rbPatientPortal.Checked Then
                MessageBox.Show("The requested operation requires data validation and interface configuration changes." + Environment.NewLine + " Please contact gloStream Support at " + """support@glostream.com""" + "", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                RemoveHandler rbIntuitPortal.CheckedChanged, AddressOf rbIntuitPortal_CheckedChanged
                RemoveHandler rbPatientPortal.CheckedChanged, AddressOf rbPatientPortal_CheckedChanged
                rbIntuitPortal.Checked = _blnIntuitHealth
                rbPatientPortal.Checked = _blnPatientPortal
                AddHandler rbIntuitPortal.CheckedChanged, AddressOf rbIntuitPortal_CheckedChanged
                AddHandler rbPatientPortal.CheckedChanged, AddressOf rbPatientPortal_CheckedChanged
                Me.Size = New System.Drawing.Size(614, 181)
                pnlOnlinePayment.Visible = False
            End If
        Else
            pnlMu2Portal.Visible = True
            If sPatientPortalOnlinePaymentEnabled.ToLower = "true" Then
                pnlOnlinePayment.BringToFront()
                pnlOnlinePayment.Visible = True
                Me.Size = New System.Drawing.Size(640, 344)
            Else
                pnlOnlinePayment.Visible = False
                Me.Size = New System.Drawing.Size(640, 282)
            End If
            'Me.Size = New System.Drawing.Size(640, 314)
        End If
        ChangeRadioButtonStyle()
    End Sub

    Private Sub ChangeRadioButtonStyle()
        If rbIntuitPortal.Checked = True Then
            rbIntuitPortal.Font = New Font("Tahoma", 9, FontStyle.Bold)
        Else
            rbIntuitPortal.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If
        If rbPatientPortal.Checked = True Then
            rbPatientPortal.Font = New Font("Tahoma", 9, FontStyle.Bold)
        Else
            rbPatientPortal.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub btnBrowsePatientPortalgloCoreServicesInstallationPath_Click(sender As System.Object, e As System.EventArgs) Handles btnBrowsePatientPortalgloCoreServicesInstallationPath.Click
        fbdPatientPortalgloCoreServicesInstallationPath.ShowDialog()
        If Not String.IsNullOrEmpty(fbdPatientPortalgloCoreServicesInstallationPath.SelectedPath) Then
            If (Not System.IO.Directory.Exists(fbdPatientPortalgloCoreServicesInstallationPath.SelectedPath)) Then
                MessageBox.Show("Enter valid gloCore service installation path", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtPatientPortalgloCoreServicesInstallationPath.Text = ""
            Else
                txtPatientPortalgloCoreServicesInstallationPath.Text = fbdPatientPortalgloCoreServicesInstallationPath.SelectedPath

            End If

        End If

    End Sub

    Private Sub chkOnlinePayment_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkOnlinePayment.CheckedChanged
        If chkOnlinePayment.Checked = False Then
            sPatientPortalOnlinePaymentEnabled = "False"
            pnlOnlinePayment.Visible = False
            txtMerchantId.Text = ""
            txtRegistrationKey.Text = ""
            Me.Size = New System.Drawing.Size(640, 282)
        Else
            sPatientPortalOnlinePaymentEnabled = "True"
            pnlOnlinePayment.BringToFront()
            pnlOnlinePayment.Visible = True
            txtMerchantId.Text = sPatPortalMerchantId
            txtRegistrationKey.Text = sPatPortalRegistrationKey
            Me.Size = New System.Drawing.Size(640, 344)
        End If
    End Sub
End Class