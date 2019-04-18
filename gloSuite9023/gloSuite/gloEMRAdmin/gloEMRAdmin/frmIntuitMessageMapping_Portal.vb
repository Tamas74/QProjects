Imports System.Linq
Public Class frmIntuitMessageMapping_Portal


    Dim COL_Provider As Integer = 0
    Dim COL_Location As Integer = 1
    Dim COL_User As Integer = 2

    Dim _messageBoxCaption As String = gloGlobal.gloPMGlobal.MessageBoxCaption


    Dim dtProviders As DataTable
    Dim dtLocation As DataTable
    Dim dtUser As DataTable
    Dim dtDefaultUser As DataTable
    Dim dtAppointmentUser As DataTable
    Dim dtRxRenewalUser As DataTable
    Dim dtBillPayUser As DataTable
    ''Added for MU2 Patient portal implementation on 20130625
    Dim dtPortalUser As DataTable
    ''End
    Dim dtPatientPortalUser As DataTable

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub LoadData(ByVal sMessageType As String)
        Dim dt As DataTable
        Try
            dtUser = FillUser()
            'Select Case Tb_Messages.SelectedTab.Tag

            'Case "Appointment Request"
            dtAppointmentUser = dtUser.Copy
            cmbApptUser.DataSource = dtAppointmentUser
            cmbApptUser.DisplayMember = "sLoginName"
            cmbApptUser.ValueMember = "nUserID"

            'Case "Rx Renewal"
            dtRxRenewalUser = dtUser.Copy
            cmbRxUser.DataSource = dtRxRenewalUser
            cmbRxUser.DisplayMember = "sLoginName"
            cmbRxUser.ValueMember = "nUserID"

            'Case "Online Bill Pay"
            dtBillPayUser = dtUser.Copy
            cmbBillPayUser.DataSource = dtBillPayUser
            cmbBillPayUser.DisplayMember = "sLoginName"
            cmbBillPayUser.ValueMember = "nUserID"
            'End Select

            ''Added for MU2 Patient portal implementation on 20130625
            dtPortalUser = dtUser.Copy
            cmbPortalUsers.DataSource = dtPortalUser
            cmbPortalUsers.DisplayMember = "sLoginName"
            cmbPortalUsers.ValueMember = "nUserID"
            ''End

            dtPatientPortalUser = dtUser.Copy
            cmbPatientFormUsers.DataSource = dtPatientPortalUser
            cmbPatientFormUsers.DisplayMember = "sLoginName"
            cmbPatientFormUsers.ValueMember = "nUserID"

            DesignGrid(sMessageType)
            dt = GetMessageMapping(sMessageType)
            SetData(dt)
        Catch ex As Exception
            MessageBox.Show(ex.ToString(), gstrMessageBoxCaption)
        Finally
            If Not IsNothing(dt) Then
                dt.Dispose()
                dt = Nothing
            End If
        End Try
    End Sub

    Private Sub LoadDefaultUserData(ByVal sMessageType As String)
        Dim dt As DataTable
        Dim dtDetaultAppointmentUser As DataTable
        Dim dtDetaultRxRenewalUser As DataTable
        Dim dtDetaultBillPayUser As DataTable
        Dim dtDetaultPortalUser As DataTable
        Dim dtDetaultPatientFormUser As DataTable
        Try
            dtDefaultUser = FillUser()
            'Case "Appointment Request"
            'dtAppointmentUser = dtUser.Copy
            'cmbApptUser.DataSource = dtAppointmentUser
            'cmbApptUser.DisplayMember = "sLoginName"
            'cmbApptUser.ValueMember = "nUserID"

            dtDetaultAppointmentUser = FillMappedUsers("Appointment Request", "Default")
            cmbApptDefaultUser.DataSource = dtDetaultAppointmentUser
            cmbApptDefaultUser.ValueMember = dtDetaultAppointmentUser.Columns("nUserID").ColumnName
            cmbApptDefaultUser.DisplayMember = dtDetaultAppointmentUser.Columns("Description").ColumnName

            'Case "Rx Renewal"
            'dtRxRenewalUser = dtUser.Copy
            'cmbRxUser.DataSource = dtRxRenewalUser
            'cmbRxUser.DisplayMember = "sLoginName"
            'cmbRxUser.ValueMember = "nUserID"

            dtDetaultRxRenewalUser = FillMappedUsers("Rx Renewal", "Default")
            cmbRxDefaultUser.DataSource = dtDetaultRxRenewalUser
            cmbRxDefaultUser.ValueMember = dtDetaultRxRenewalUser.Columns("nUserID").ColumnName
            cmbRxDefaultUser.DisplayMember = dtDetaultRxRenewalUser.Columns("Description").ColumnName

            'Case "Online Bill Pay"
            'dtBillPayUser = dtUser.Copy
            'cmbBillPayUser.DataSource = dtBillPayUser
            'cmbBillPayUser.DisplayMember = "sLoginName"
            'cmbBillPayUser.ValueMember = "nUserID"
            'End Select

            dtDetaultBillPayUser = FillMappedUsers("Online Bill Pay", "Default")
            cmbBillPayDefaultUser.DataSource = dtDetaultBillPayUser
            cmbBillPayDefaultUser.ValueMember = dtDetaultBillPayUser.Columns("nUserID").ColumnName
            cmbBillPayDefaultUser.DisplayMember = dtDetaultBillPayUser.Columns("Description").ColumnName

            ''Added for MU2 Patient portal implementation on 20130625
            'dtPortalUser = dtUser.Copy
            'cmbPortalUsers.DataSource = dtPortalUser
            'cmbPortalUsers.DisplayMember = "sLoginName"
            'cmbPortalUsers.ValueMember = "nUserID"
            ''End

            dtDetaultPortalUser = FillMappedUsers("Review Portal Users", "Default")
            cmbPortalDefaultUser.DataSource = dtDetaultPortalUser
            cmbPortalDefaultUser.ValueMember = dtDetaultPortalUser.Columns("nUserID").ColumnName
            cmbPortalDefaultUser.DisplayMember = dtDetaultPortalUser.Columns("Description").ColumnName

            dtDetaultPatientFormUser = FillMappedUsers("Online Patient Form", "Default")
            cmbPatientFormDefaultUser.DataSource = dtDetaultPatientFormUser
            cmbPatientFormDefaultUser.ValueMember = dtDetaultPatientFormUser.Columns("nUserID").ColumnName
            cmbPatientFormDefaultUser.DisplayMember = dtDetaultPatientFormUser.Columns("Description").ColumnName

            DesignGrid(sMessageType)
            dt = GetMessageMapping(sMessageType)
            SetData(dt)
        Catch ex As Exception
            MessageBox.Show(ex.ToString(), gstrMessageBoxCaption)
        Finally
            If Not IsNothing(dt) Then
                dt.Dispose()
                dt = Nothing
            End If
        End Try
    End Sub

    Public Function FillMappedUsers(ByVal sMessageType As String, ByVal sSettingName As String) As DataTable
        Dim oclsgloIntuit As clsgloIntuit
        Dim dt As DataTable
        Try
            Select Case sMessageType
                Case "Appointment Request"
                    oclsgloIntuit = New clsgloIntuit
                    dt = oclsgloIntuit.GetDetaultMappedUsers(sMessageType, sSettingName)
                    If Not IsNothing(dt) Then
                        Dim ToItem As gloGeneralItem.gloItem
                        _ToApptDefaultUserList = New gloGeneralItem.gloItems
                        For i As Int16 = 0 To dt.Rows.Count - 1
                            ToItem = New gloGeneralItem.gloItem()
                            ToItem.ID = dt.Rows(i)("nUserID")
                            ToItem.Description = dt.Rows(i)("Description")
                            _ToApptDefaultUserList.Add(ToItem)
                            ToItem = Nothing
                        Next
                    End If
                    Return dt

                Case "Rx Renewal"

                    oclsgloIntuit = New clsgloIntuit
                    dt = oclsgloIntuit.GetDetaultMappedUsers(sMessageType, sSettingName)
                    If Not IsNothing(dt) Then
                        Dim ToItem As gloGeneralItem.gloItem
                        _ToRxDefaultUserList = New gloGeneralItem.gloItems
                        For i As Int16 = 0 To dt.Rows.Count - 1
                            ToItem = New gloGeneralItem.gloItem()
                            ToItem.ID = dt.Rows(i)("nUserID")
                            ToItem.Description = dt.Rows(i)("Description")
                            _ToRxDefaultUserList.Add(ToItem)
                            ToItem = Nothing
                        Next
                    End If
                    Return dt

                Case "Online Bill Pay"

                    oclsgloIntuit = New clsgloIntuit
                    dt = oclsgloIntuit.GetDetaultMappedUsers(sMessageType, sSettingName)
                    If Not IsNothing(dt) Then
                        Dim ToItem As gloGeneralItem.gloItem
                        _ToBillDefaultUserList = New gloGeneralItem.gloItems
                        For i As Int16 = 0 To dt.Rows.Count - 1
                            ToItem = New gloGeneralItem.gloItem()
                            ToItem.ID = dt.Rows(i)("nUserID")
                            ToItem.Description = dt.Rows(i)("Description")
                            _ToBillDefaultUserList.Add(ToItem)
                            ToItem = Nothing
                        Next
                    End If
                    Return dt

                    ''Added for MU2 Patient portal implementaiton on 20130626
                Case "Review Portal Users"
                    oclsgloIntuit = New clsgloIntuit
                    dt = oclsgloIntuit.GetDetaultMappedUsers(sMessageType, sSettingName)
                    If Not IsNothing(dt) Then
                        Dim ToItem As gloGeneralItem.gloItem
                        _ToPortalDefaultUserList = New gloGeneralItem.gloItems
                        For i As Int16 = 0 To dt.Rows.Count - 1
                            ToItem = New gloGeneralItem.gloItem()
                            ToItem.ID = dt.Rows(i)("nUserID")
                            ToItem.Description = dt.Rows(i)("Description")
                            _ToPortalDefaultUserList.Add(ToItem)
                            ToItem = Nothing
                        Next
                    End If
                    Return dt
                Case "Online Patient Form"
                    oclsgloIntuit = New clsgloIntuit
                    dt = oclsgloIntuit.GetDetaultMappedUsers(sMessageType, sSettingName)
                    If Not IsNothing(dt) Then
                        Dim ToItem As gloGeneralItem.gloItem
                        _ToPatientFormDefaultUserList = New gloGeneralItem.gloItems
                        For i As Int16 = 0 To dt.Rows.Count - 1
                            ToItem = New gloGeneralItem.gloItem()
                            ToItem.ID = dt.Rows(i)("nUserID")
                            ToItem.Description = dt.Rows(i)("Description")
                            _ToPatientFormDefaultUserList.Add(ToItem)
                            ToItem = Nothing
                        Next
                    End If
                    Return dt
            End Select




        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not IsNothing(oclsgloIntuit) Then
                oclsgloIntuit.Dispose()
                oclsgloIntuit = Nothing
            End If
        End Try
        Return dt
    End Function

    Private Sub frmMessageMapping_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            If (IspatientPortalEnabled()) Then
                pnlAutoCompleteTasK.Visible = True
                chkAutoCompleteTask.Checked = IsAutoCompleteTaskEnabled()
                LoadDefaultUserData("Appointment Request")
                pnlApptDefaultUser.Visible = True
                pnlRxDefaultUser.Visible = True
                pnlBillPayDefaultUser.Visible = True
                pnlPortalDefaultUser.Visible = True
                pnlPatientFormDefaultUser.Visible = True
                pnlPFEnableTaskNotification.Visible = True
                chkPFEnableTaskNotification.Checked = IsPatientFormTaskNotificationEnabled()
                pnlPatientFormAutoCompleteTasK.Visible = True
                If chkPFEnableTaskNotification.Checked Then
                    chkPatientFormAutoCompleteTask.Checked = IsPFAutoCompleteTaskEnabled()
                Else
                    chkPatientFormAutoCompleteTask.Checked = False
                    chkPatientFormAutoCompleteTask.Enabled = False
                End If


                ''Bug #90097: Patient Portal task mapping :Application throwing exception on clicking search for advanced settings
                dtUser = dtDefaultUser
            Else
                LoadData("Appointment Request")
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString(), gstrMessageBoxCaption)
        End Try
    End Sub

    Private Sub DesignGrid(ByVal sMessageType As String)
        Try
            dtLocation = FillProviderandLocation("Location")

            Dim _strLocation As String = ""

            If Not dtLocation Is Nothing Then
                If dtLocation.Rows.Count > 0 Then
                    For i As Integer = 0 To dtLocation.Rows.Count - 1

                        _strLocation = _strLocation & "|" & dtLocation.Rows(i)("Location").ToString()

                    Next
                End If
            End If


            dtProviders = FillProviderandLocation("Provider")

            Dim _strProviders As String = ""
            If Not dtProviders Is Nothing Then
                If dtProviders.Rows.Count > 0 Then
                    For i As Integer = 0 To dtProviders.Rows.Count - 1

                        _strProviders = _strProviders & "|" & dtProviders.Rows(i)("Provider").ToString()

                    Next
                End If
            End If



            'dtUser = FillUser()

            Dim _strUser As String = ""
            If Not dtUser Is Nothing Then
                If dtUser.Rows.Count > 0 Then
                    For i As Integer = 0 To dtUser.Rows.Count - 1

                        _strUser = _strUser & "|" & dtUser.Rows(i)("sLoginName").ToString()

                    Next
                End If
            End If
            'Select Case Tb_Messages.SelectedTab.Tag
            ' Case "Appointment Request"
            c1AppointmentRequest.Cols("Provider").ComboList = " |" & _strProviders
            'c1AppointmentRequest.Cols("User").ComboList = " |" & _strUser
            c1AppointmentRequest.Cols("User").ComboList = " |"
            c1AppointmentRequest.Cols("Location").ComboList = " |" & _strLocation
            ' Case "Rx Renewal"
            C1RxRenewal.Cols("Provider").ComboList = " |" & _strProviders
            'C1RxRenewal.Cols("User").ComboList = " |" & _strUser
            C1RxRenewal.Cols("User").ComboList = " |"
            C1RxRenewal.Cols("Location").ComboList = " |" & _strLocation
            'Case "Online Bill Pay"
            'C1BillPay.Cols("Provider").ComboList = " |" & _strProviders
            C1BillPay.Cols("Provider").Visible = False
            'C1BillPay.Cols("User").ComboList = " |" & _strUser
            C1BillPay.Cols("User").ComboList = " |"
            C1BillPay.Cols("Location").ComboList = " |" & _strLocation
            'End Select

            ''Added for MU2 Patient portal implementation on 20130625
            C1PortalUsers.Cols("Provider").ComboList = " |" & _strProviders
            C1PortalUsers.Cols("User").ComboList = " |"
            C1PortalUsers.Cols("Location").ComboList = " |" & _strLocation
            ''End

            C1PatientFormUsers.Cols("Provider").ComboList = " |" & _strProviders
            C1PatientFormUsers.Cols("User").ComboList = " |"
            C1PatientFormUsers.Cols("Location").ComboList = " |" & _strLocation

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        End Try

    End Sub

    Private Sub ts_btnAddLine_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ts_btnAddLine.Click
        Try
            Select Case Tb_Messages.SelectedTab.Tag
                Case "Appointment Request"
                    c1AppointmentRequest.Rows.Add()
                    c1AppointmentRequest.SetData(c1AppointmentRequest.Rows.Count - 1, 0, 999)
                    c1AppointmentRequest.SetCellImage(c1AppointmentRequest.Rows.Count - 1, 5, imgTreeVIew.Images(0))
                Case "Rx Renewal"
                    C1RxRenewal.Rows.Add()
                    C1RxRenewal.SetData(C1RxRenewal.Rows.Count - 1, 0, 999)
                    C1RxRenewal.SetCellImage(C1RxRenewal.Rows.Count - 1, 5, imgTreeVIew.Images(0))
                Case "Online Bill Pay"
                    C1BillPay.Rows.Add()
                    C1BillPay.SetData(C1BillPay.Rows.Count - 1, 0, 999)
                    C1BillPay.SetCellImage(C1BillPay.Rows.Count - 1, 5, imgTreeVIew.Images(0))
                    ''Added for MU2 Patient portal implementation on 20130625
                Case "Review Portal Users"
                    C1PortalUsers.Rows.Add()
                    C1PortalUsers.SetData(C1PortalUsers.Rows.Count - 1, 0, 999)
                    C1PortalUsers.SetCellImage(C1PortalUsers.Rows.Count - 1, 5, imgTreeVIew.Images(0))
                    ''End
                Case "Online Patient Form"
                    C1PatientFormUsers.Rows.Add()
                    C1PatientFormUsers.SetData(C1PatientFormUsers.Rows.Count - 1, 0, 999)
                    C1PatientFormUsers.SetCellImage(C1PatientFormUsers.Rows.Count - 1, 5, imgTreeVIew.Images(0))
            End Select

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        End Try
    End Sub

    Private Sub tsb_btnRemoveLine_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsb_btnRemoveLine.Click
        Try

            Select Case Tb_Messages.SelectedTab.Tag
                Case "Appointment Request"
                    If c1AppointmentRequest.RowSel > 0 Then
                        If (c1AppointmentRequest.GetData(c1AppointmentRequest.RowSel, "Provider") Is Nothing) And (c1AppointmentRequest.GetData(c1AppointmentRequest.RowSel, "Location") Is Nothing) And (c1AppointmentRequest.GetData(c1AppointmentRequest.RowSel, "User") Is Nothing) Then
                            If c1AppointmentRequest IsNot Nothing AndAlso c1AppointmentRequest.Rows.Count > 1 Then
                                Dim rowIndex As Integer = c1AppointmentRequest.RowSel
                                c1AppointmentRequest.Rows.Remove(rowIndex)
                            End If
                        Else
                            Dim res As DialogResult = MessageBox.Show("Are you sure you want to delete this rule ? ", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information)
                            If res = DialogResult.Yes Then
                                If c1AppointmentRequest IsNot Nothing AndAlso c1AppointmentRequest.Rows.Count > 1 Then
                                    Dim rowIndex As Integer = c1AppointmentRequest.RowSel
                                    c1AppointmentRequest.Rows.Remove(rowIndex)
                                End If
                            End If
                        End If

                    End If
                Case "Rx Renewal"
                    If C1RxRenewal.RowSel > 0 Then
                        If (C1RxRenewal.GetData(C1RxRenewal.RowSel, "Provider") Is Nothing) And (C1RxRenewal.GetData(C1RxRenewal.RowSel, "Location") Is Nothing) And (C1RxRenewal.GetData(C1RxRenewal.RowSel, "User") Is Nothing) Then
                            If C1RxRenewal IsNot Nothing AndAlso C1RxRenewal.Rows.Count > 1 Then
                                Dim rowIndex As Integer = C1RxRenewal.RowSel
                                C1RxRenewal.Rows.Remove(rowIndex)
                            End If
                        Else
                            Dim res As DialogResult = MessageBox.Show("Are you sure you want to delete this rule ? ", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information)
                            If res = DialogResult.Yes Then
                                If C1RxRenewal IsNot Nothing AndAlso C1RxRenewal.Rows.Count > 1 Then
                                    Dim rowIndex As Integer = C1RxRenewal.RowSel
                                    C1RxRenewal.Rows.Remove(rowIndex)
                                End If
                            End If
                        End If
                    End If
                Case "Online Bill Pay"
                    If C1BillPay.RowSel > 0 Then
                        If (C1BillPay.GetData(C1BillPay.RowSel, "Provider") Is Nothing) And (C1BillPay.GetData(C1BillPay.RowSel, "User") Is Nothing) Then
                            If C1BillPay IsNot Nothing AndAlso C1BillPay.Rows.Count > 1 Then
                                Dim rowIndex As Integer = C1BillPay.RowSel
                                C1BillPay.Rows.Remove(rowIndex)
                            End If
                        Else
                            Dim res As DialogResult = MessageBox.Show("Are you sure you want to delete this rule ? ", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information)
                            If res = DialogResult.Yes Then
                                If C1BillPay IsNot Nothing AndAlso C1BillPay.Rows.Count > 1 Then
                                    Dim rowIndex As Integer = C1BillPay.RowSel
                                    C1BillPay.Rows.Remove(rowIndex)
                                End If
                            End If
                        End If
                    End If

                    ''Added for Mu2 Patient portal implementation on 20130626
                Case "Review Portal Users"
                    If C1PortalUsers.RowSel > 0 Then
                        If (C1PortalUsers.GetData(C1PortalUsers.RowSel, "Provider") Is Nothing) And (C1PortalUsers.GetData(C1PortalUsers.RowSel, "Location") Is Nothing) And (C1PortalUsers.GetData(C1PortalUsers.RowSel, "User") Is Nothing) Then
                            If C1PortalUsers IsNot Nothing AndAlso C1PortalUsers.Rows.Count > 1 Then
                                Dim rowIndex As Integer = C1PortalUsers.RowSel
                                C1PortalUsers.Rows.Remove(rowIndex)
                            End If
                        Else
                            Dim res As DialogResult = MessageBox.Show("Are you sure you want to delete this rule ? ", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information)
                            If res = DialogResult.Yes Then
                                If C1PortalUsers IsNot Nothing AndAlso C1PortalUsers.Rows.Count > 1 Then
                                    Dim rowIndex As Integer = C1PortalUsers.RowSel
                                    C1PortalUsers.Rows.Remove(rowIndex)
                                End If
                            End If
                        End If
                    End If
                    ''End
                Case "Online Patient Form"
                    If C1PatientFormUsers.RowSel > 0 Then
                        If (C1PatientFormUsers.GetData(C1PatientFormUsers.RowSel, "Provider") Is Nothing) And (C1PatientFormUsers.GetData(C1PatientFormUsers.RowSel, "Location") Is Nothing) And (C1PatientFormUsers.GetData(C1PatientFormUsers.RowSel, "User") Is Nothing) Then
                            If C1PatientFormUsers IsNot Nothing AndAlso C1PatientFormUsers.Rows.Count > 1 Then
                                Dim rowIndex As Integer = C1PatientFormUsers.RowSel
                                C1PatientFormUsers.Rows.Remove(rowIndex)
                            End If
                        Else
                            Dim res As DialogResult = MessageBox.Show("Are you sure you want to delete this rule ? ", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information)
                            If res = DialogResult.Yes Then
                                If C1PatientFormUsers IsNot Nothing AndAlso C1PatientFormUsers.Rows.Count > 1 Then
                                    Dim rowIndex As Integer = C1PatientFormUsers.RowSel
                                    C1PatientFormUsers.Rows.Remove(rowIndex)
                                End If
                            End If
                        End If
                    End If
            End Select



        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        End Try
    End Sub

    Private Function FillProviderandLocation(ByVal sCategory As String) As DataTable
        Dim dt As DataTable = Nothing
        Dim oclsgloIntuit As clsgloIntuit = Nothing
        Try
            oclsgloIntuit = New clsgloIntuit()
            dt = oclsgloIntuit.GetIntuitMappedProviderandLocation(sCategory)
            Return dt
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
            Return dt
        Finally
            If Not IsNothing(oclsgloIntuit) Then
                oclsgloIntuit.Dispose()
                oclsgloIntuit = Nothing
            End If
            If Not IsNothing(dt) Then
                dt.Dispose()
                dt = Nothing
            End If
        End Try
    End Function

    Private Function FillUser() As DataTable
        Dim dt As DataTable = Nothing
        Dim oclsgloIntuit As clsgloIntuit = Nothing
        Try
            oclsgloIntuit = New clsgloIntuit()
            dt = oclsgloIntuit.GetUsers()
            Return dt
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
            Return dt
        Finally
            If Not IsNothing(oclsgloIntuit) Then
                oclsgloIntuit.Dispose()
                oclsgloIntuit = Nothing
            End If
            If Not IsNothing(dt) Then
                dt.Dispose()
                dt = Nothing
            End If
        End Try
    End Function

    Private Sub tsb_Saveclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsb_Saveclose.Click

        Dim dt As New DataTable()
        Try
            Select Case Tb_Messages.SelectedTab.Tag.ToString()
                Case "Appointment Request"
                    If c1AppointmentRequest.Rows.Count > 0 Then
                        c1AppointmentRequest.[Select](c1AppointmentRequest.RowSel, c1AppointmentRequest.ColSel)
                        c1AppointmentRequest.Focus()
                    End If

                Case "Rx Renewal"

                    If C1RxRenewal.Rows.Count > 0 Then
                        C1RxRenewal.[Select](C1RxRenewal.RowSel, C1RxRenewal.ColSel)
                        C1RxRenewal.Focus()
                    End If

                Case "Online Bill Pay"

                    If C1BillPay.Rows.Count > 0 Then
                        C1BillPay.[Select](C1BillPay.RowSel, C1BillPay.ColSel)
                        C1BillPay.Focus()
                    End If

                    ''Added for MU2 Patient portal implementaiton on 20130626
                Case "Review Portal Users"
                    If C1PortalUsers.Rows.Count > 0 Then
                        C1PortalUsers.[Select](C1PortalUsers.RowSel, C1PortalUsers.ColSel)
                        C1PortalUsers.Focus()
                    End If
                    ''End

            End Select
            UpdateSettings("PatientPortalAutoCompleteTask", IIf(chkAutoCompleteTask.Checked = True, "True", "False"))
            UpdateSettings("PatientPortalPFTaskNotification", IIf(chkPFEnableTaskNotification.Checked = True, "True", "False"))
            UpdateSettings("PatientPortalPFAutoCompleteTask", IIf(chkPatientFormAutoCompleteTask.Checked = True, "True", "False"))

            If (ValidateRule()) Then
                If (IspatientPortalEnabled()) Then
                    If (ValidateUser()) Then
                        dt = GetdataTOsaveDefaultUser()
                        dt = AddBasicDefaultUserSetting(dt)
                    Else
                        Return
                    End If
                Else
                    dt = GetdataTOsave()

                    'Aniket 08-Mar-13: Remove validation for duplicate task user
                    'If (ValidateData(dt)) Then
                    dt = AddBasicSetting(dt)
                End If

                If (SaveMapping(dt, Tb_Messages.SelectedTab.Tag)) Then
                    Me.Close()
                End If
                'End If

            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        Finally
            If Not IsNothing(dt) Then
                dt.Dispose()
                dt = Nothing
            End If
        End Try

    End Sub

    Private Function ValidateUser() As Boolean
        Dim _blnResult As Boolean = True
        Try
            If cmbApptDefaultUser.Items.Count() = 0 Then
                MessageBox.Show("Please select default user on Appointment tab.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                _blnResult = False
                Tb_Messages.SelectedTab = TbPg_AppointmentRequest
            ElseIf cmbRxDefaultUser.Items.Count = 0 Then
                MessageBox.Show("Please select default user on Rx renewal tab.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                _blnResult = False
                Tb_Messages.SelectedTab = TbPg_RxRenewal
            ElseIf cmbBillPayDefaultUser.Items.Count() = 0 Then
                MessageBox.Show("Please select default user on Online bill pay tab.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                _blnResult = False
                Tb_Messages.SelectedTab = TbPg_BillPay
            ElseIf cmbPortalDefaultUser.Items.Count() = 0 Then
                MessageBox.Show("Please select default user on Review portal user tab.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                _blnResult = False
                Tb_Messages.SelectedTab = TbPg_PortalUsers
            ElseIf cmbPatientFormDefaultUser.Items.Count = 0 Then
                If chkPFEnableTaskNotification.Checked = True Then
                    MessageBox.Show("Please select default user on Online patient form tab.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    _blnResult = False
                    Tb_Messages.SelectedTab = TbPg_OnlinePatientForm
                End If
            End If

            Return _blnResult
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
            Return _blnResult
        End Try
    End Function

    Private Function ValidateRule() As Boolean
        Dim _blnResult As Boolean = True
        Try
            'Select Case Tb_Messages.SelectedTab.Tag.ToString()
            'Case "Appointment Request"
            For i As Integer = 1 To c1AppointmentRequest.Rows.Count - 1
                If (
                    ((c1AppointmentRequest.GetData(i, "Provider") Is Nothing) And (c1AppointmentRequest.GetData(i, "Location") Is Nothing)) Or
                    ((Convert.ToString(c1AppointmentRequest.GetData(i, "Provider")).Trim() = "") And (Convert.ToString(c1AppointmentRequest.GetData(i, "Location")).Trim() = ""))) Then

                    Tb_Messages.SelectTab(TbPg_AppointmentRequest)
                    MessageBox.Show("Provider or Location not found. Please assign Provider or Location.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    c1AppointmentRequest.[Select](i, c1AppointmentRequest.ColSel)
                    c1AppointmentRequest.Focus()
                    _blnResult = False
                    Exit Function
                End If
            Next
            For i As Integer = 1 To c1AppointmentRequest.Rows.Count - 1
                If ((c1AppointmentRequest.GetData(i, "User") Is Nothing) Or (Convert.ToString(c1AppointmentRequest.GetData(i, "User")).Trim() = "")) Then

                    Tb_Messages.SelectTab(TbPg_AppointmentRequest)
                    MessageBox.Show("User is not found. Please assign User.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    c1AppointmentRequest.[Select](i, c1AppointmentRequest.ColSel)
                    c1AppointmentRequest.Focus()
                    _blnResult = False
                    Exit Function
                End If
            Next
            'Case "Rx Renewal"
            For i As Integer = 1 To C1RxRenewal.Rows.Count - 1
                If (
                    ((C1RxRenewal.GetData(i, "Provider") Is Nothing) And (C1RxRenewal.GetData(i, "Location") Is Nothing)) Or
                    ((Convert.ToString(C1RxRenewal.GetData(i, "Provider")).Trim() = "") And (Convert.ToString(C1RxRenewal.GetData(i, "Location")).Trim() = ""))) Then

                    Tb_Messages.SelectTab(TbPg_RxRenewal)
                    MessageBox.Show("Provider or Location not found. Please assign Provider or Location.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    C1RxRenewal.[Select](i, C1RxRenewal.ColSel)
                    C1RxRenewal.Focus()
                    _blnResult = False
                    Exit Function
                End If
            Next
            For i As Integer = 1 To C1RxRenewal.Rows.Count - 1
                If ((C1RxRenewal.GetData(i, "User") Is Nothing) Or (Convert.ToString(C1RxRenewal.GetData(i, "User")).Trim() = "")) Then

                    Tb_Messages.SelectTab(TbPg_RxRenewal)
                    MessageBox.Show("User is not found. Please assign User.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    C1RxRenewal.[Select](i, C1RxRenewal.ColSel)
                    C1RxRenewal.Focus()
                    _blnResult = False
                    Exit Function
                End If
            Next
            'Case "Online Bill Pay"
            For i As Integer = 1 To C1BillPay.Rows.Count - 1
                If ((C1BillPay.GetData(i, "Location") Is Nothing) Or (Convert.ToString(C1BillPay.GetData(i, "Location")).Trim() = "")) Then

                    Tb_Messages.SelectTab(TbPg_BillPay)
                    MessageBox.Show("Location not found. Please assign Location.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    C1BillPay.[Select](i, C1BillPay.ColSel)
                    C1BillPay.Focus()
                    _blnResult = False
                    Exit Function
                End If
            Next
            For i As Integer = 1 To C1BillPay.Rows.Count - 1
                If ((C1BillPay.GetData(i, "User") Is Nothing) Or (Convert.ToString(C1BillPay.GetData(i, "User")).Trim() = "")) Then

                    Tb_Messages.SelectTab(TbPg_BillPay)
                    MessageBox.Show("User is not found. Please assign User.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    C1BillPay.[Select](i, C1BillPay.ColSel)
                    C1BillPay.Focus()
                    _blnResult = False
                    Exit Function
                End If
            Next

            ''Added for MU2 Patient portal implementation on 20130626
            For i As Integer = 1 To C1PortalUsers.Rows.Count - 1
                If (
                    ((C1PortalUsers.GetData(i, "Provider") Is Nothing) And (C1PortalUsers.GetData(i, "Location") Is Nothing)) Or
                    ((Convert.ToString(C1PortalUsers.GetData(i, "Provider")).Trim() = "") And (Convert.ToString(C1PortalUsers.GetData(i, "Location")).Trim() = ""))) Then

                    Tb_Messages.SelectTab(TbPg_PortalUsers)
                    MessageBox.Show("Provider or Location not found. Please assign Provider or Location.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    C1PortalUsers.[Select](i, C1PortalUsers.ColSel)
                    C1PortalUsers.Focus()
                    _blnResult = False
                    Exit Function
                End If
            Next
            For i As Integer = 1 To C1PortalUsers.Rows.Count - 1
                If ((C1PortalUsers.GetData(i, "User") Is Nothing) Or (Convert.ToString(C1PortalUsers.GetData(i, "User")).Trim() = "")) Then

                    Tb_Messages.SelectTab(TbPg_PortalUsers)
                    MessageBox.Show("User is not found. Please assign User.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    C1PortalUsers.[Select](i, C1PortalUsers.ColSel)
                    C1PortalUsers.Focus()
                    _blnResult = False
                    Exit Function
                End If
            Next
            ''End
            For i As Integer = 1 To C1PatientFormUsers.Rows.Count - 1
                If (
                    ((C1PatientFormUsers.GetData(i, "Provider") Is Nothing) And (C1PatientFormUsers.GetData(i, "Location") Is Nothing)) Or
                    ((Convert.ToString(C1PatientFormUsers.GetData(i, "Provider")).Trim() = "") And (Convert.ToString(C1PatientFormUsers.GetData(i, "Location")).Trim() = ""))) Then

                    Tb_Messages.SelectTab(TbPg_OnlinePatientForm)
                    MessageBox.Show("Provider or Location not found. Please assign Provider or Location.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    C1PatientFormUsers.[Select](i, C1PatientFormUsers.ColSel)
                    C1PatientFormUsers.Focus()
                    _blnResult = False
                    Exit Function
                End If
            Next
            For i As Integer = 1 To C1PatientFormUsers.Rows.Count - 1
                If ((C1PatientFormUsers.GetData(i, "User") Is Nothing) Or (Convert.ToString(C1PatientFormUsers.GetData(i, "User")).Trim() = "")) Then

                    Tb_Messages.SelectTab(TbPg_OnlinePatientForm)
                    MessageBox.Show("User is not found. Please assign User.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    C1PatientFormUsers.[Select](i, C1PatientFormUsers.ColSel)
                    C1PatientFormUsers.Focus()
                    _blnResult = False
                    Exit Function
                End If
            Next
            'End Select

            Return _blnResult
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
            Return _blnResult
        End Try
    End Function

    Private Function ValidateData(ByVal Dt As DataTable) As Boolean

        Dim Result As Boolean = True
        Dim DtData As New DataTable()

        Try
            DtData = Dt.Copy
            DtData.Columns.Remove("nPriorityID")
            DtData.Columns.Remove("nProviderID")
            DtData.Columns.Remove("nLocationID")
            'DtData.Columns.Remove("sMessageType")
            DtData.Columns.Remove("sSettingType")

            Dim distinctData As DataTable = DtData.DefaultView.ToTable(True)
            If (distinctData.Rows.Count <> DtData.Rows.Count) Then
                MessageBox.Show("Duplicate records. ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Result = False
            End If
            Return Result
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
            Return Result
        Finally
            If Not IsNothing(DtData) Then
                DtData.Dispose()
                DtData = Nothing
            End If
        End Try
    End Function

    Private Sub btnApptUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnApptUp.Click
        Try
            If c1AppointmentRequest.Rows.Count > 1 Then
                If c1AppointmentRequest.RowSel = 1 Then
                    c1AppointmentRequest.RowSel = 1
                Else
                    c1AppointmentRequest.Rows(c1AppointmentRequest.RowSel).Move(c1AppointmentRequest.RowSel - 1)
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        End Try
    End Sub

    Private Sub btnApptDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnApptDown.Click
        Try
            If c1AppointmentRequest.Rows.Count > 1 Then
                If c1AppointmentRequest.RowSel = c1AppointmentRequest.Rows.Count - 1 Then
                    c1AppointmentRequest.RowSel = c1AppointmentRequest.Rows.Count - 1
                Else
                    c1AppointmentRequest.Rows(c1AppointmentRequest.RowSel).Move(c1AppointmentRequest.RowSel + 1)
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        End Try
    End Sub

    Private Function GetdataTOsave() As DataTable

        Dim dtgrid As DataTable = New DataTable()

        Try
            dtgrid.Columns.Add("nID")
            dtgrid.Columns.Add("nPriorityID")
            dtgrid.Columns.Add("nProviderID")
            dtgrid.Columns.Add("nLocationID")
            dtgrid.Columns.Add("nUserID")
            dtgrid.Columns.Add("sMessageType")
            dtgrid.Columns.Add("sSettingType")


            Dim _nProviderID As Int64
            Dim _nLocationID As Int64
            Dim _nUserID As String

            'Select Case Tb_Messages.SelectedTab.Tag
            '    Case "Appointment Request"
            For i As Integer = 1 To c1AppointmentRequest.Rows.Count - 1

                _nProviderID = 0
                _nLocationID = 0
                _nUserID = ""

                If Not IsNothing(c1AppointmentRequest.GetData(i, "Provider")) Then
                    If (c1AppointmentRequest.GetData(i, "Provider").ToString().Trim <> "") Then

                        Dim _dr() As DataRow
                        _dr = dtProviders.[Select]("Provider = '" & Convert.ToString(c1AppointmentRequest.GetData(i, "Provider")).Replace("'", "''") & "'")
                        If Not IsNothing(_dr) And _dr.Length > 0 Then
                            _nProviderID = Convert.ToInt64(_dr(0).Item("nProviderID"))
                        Else
                            _nProviderID = Nothing
                        End If

                    End If
                End If

                If Not IsNothing(c1AppointmentRequest.GetData(i, "Location")) Then
                    If (c1AppointmentRequest.GetData(i, "Location").ToString().Trim <> "") Then

                        Dim _dr() As DataRow
                        _dr = dtLocation.[Select]("Location = '" & Convert.ToString(c1AppointmentRequest.GetData(i, "Location")).Replace("'", "''") & "'")
                        If Not IsNothing(_dr) And _dr.Length > 0 Then
                            _nLocationID = Convert.ToInt64(_dr(0).Item("nLocationID"))
                        Else
                            _nLocationID = Nothing
                        End If

                    End If
                End If


                If Not IsNothing(c1AppointmentRequest.GetData(i, "User")) Then
                    If (c1AppointmentRequest.GetData(i, "User").ToString().Trim <> "") Then
                        Dim ostyle As C1.Win.C1FlexGrid.CellStyle
                        Dim strUserList As String
                        If Not IsNothing(c1AppointmentRequest.GetCellStyle(i, 4)) Then
                            ostyle = c1AppointmentRequest.GetCellStyle(i, 4)
                            strUserList = ostyle.ComboList
                            Dim splstruser As String() = strUserList.Split("|")
                            For j As Int16 = 0 To splstruser.Length - 1
                                Dim _dr() As DataRow
                                _dr = dtUser.[Select]("sLoginName = '" & Convert.ToString(splstruser(j).ToString()).Replace("'", "''") & "'")
                                If Not IsNothing(_dr) And _dr.Length > 0 Then
                                    If _nUserID = "" Then
                                        _nUserID = _dr(0).Item("nUserID")
                                    Else
                                        _nUserID = _nUserID + "|" + _dr(0).Item("nUserID").ToString()
                                    End If
                                End If
                                _dr = Nothing
                            Next
                        End If
                        strUserList = Nothing
                        ostyle = Nothing
                    End If
                End If

                Dim _drNew As DataRow = dtgrid.NewRow()
                _drNew("nID") = 0
                _drNew("nPriorityID") = i
                _drNew("nProviderID") = _nProviderID
                _drNew("nLocationId") = _nLocationID
                _drNew("nUserID") = _nUserID
                _drNew("sMessageType") = "Appointment Request"
                _drNew("sSettingType") = "Advance"

                dtgrid.Rows.Add(_drNew)

            Next
            ' Case "Rx Renewal"
            For i As Integer = 1 To C1RxRenewal.Rows.Count - 1

                _nProviderID = 0
                _nLocationID = 0
                _nUserID = ""

                If Not IsNothing(C1RxRenewal.GetData(i, "Provider")) Then
                    If (C1RxRenewal.GetData(i, "Provider").ToString().Trim <> "") Then

                        Dim _dr() As DataRow
                        _dr = dtProviders.[Select]("Provider = '" & Convert.ToString(C1RxRenewal.GetData(i, "Provider")).Replace("'", "''") & "'")
                        If Not IsNothing(_dr) And _dr.Length > 0 Then
                            _nProviderID = Convert.ToInt64(_dr(0).Item("nProviderID"))
                        Else
                            _nProviderID = Nothing
                        End If

                    End If
                End If

                If Not IsNothing(C1RxRenewal.GetData(i, "Location")) Then
                    If (C1RxRenewal.GetData(i, "Location").ToString().Trim <> "") Then

                        Dim _dr() As DataRow
                        _dr = dtLocation.[Select]("Location = '" & Convert.ToString(C1RxRenewal.GetData(i, "Location")).Replace("'", "''") & "'")
                        If Not IsNothing(_dr) And _dr.Length > 0 Then
                            _nLocationID = Convert.ToInt64(_dr(0).Item("nLocationID"))
                        Else
                            _nLocationID = Nothing
                        End If

                    End If
                End If

                If Not IsNothing(C1RxRenewal.GetData(i, "User")) Then
                    If (C1RxRenewal.GetData(i, "User").ToString().Trim <> "") Then
                        Dim ostyle As C1.Win.C1FlexGrid.CellStyle
                        Dim strUserList As String
                        If Not IsNothing(C1RxRenewal.GetCellStyle(i, 4)) Then
                            ostyle = C1RxRenewal.GetCellStyle(i, 4)
                            strUserList = ostyle.ComboList
                            Dim splstruser As String() = strUserList.Split("|")
                            For j As Int16 = 0 To splstruser.Length - 1
                                Dim _dr() As DataRow
                                _dr = dtUser.[Select]("sLoginName = '" & Convert.ToString(splstruser(j).ToString()).Replace("'", "''") & "'")
                                If Not IsNothing(_dr) And _dr.Length > 0 Then
                                    If _nUserID = "" Then
                                        _nUserID = _dr(0).Item("nUserID")
                                    Else
                                        _nUserID = _nUserID + "|" + _dr(0).Item("nUserID").ToString()
                                    End If
                                End If
                                _dr = Nothing
                            Next
                        End If
                        strUserList = Nothing
                        ostyle = Nothing
                    End If
                End If

                Dim _drNew As DataRow = dtgrid.NewRow()
                _drNew("nID") = 0
                _drNew("nPriorityID") = i
                _drNew("nProviderID") = _nProviderID
                _drNew("nLocationId") = _nLocationID
                _drNew("nUserID") = _nUserID
                _drNew("sMessageType") = "Rx Renewal"
                _drNew("sSettingType") = "Advance"

                dtgrid.Rows.Add(_drNew)

            Next
            'Case "Online Bill Pay"
            For i As Integer = 1 To C1BillPay.Rows.Count - 1

                _nProviderID = 0
                _nLocationID = 0
                _nUserID = ""

                If Not IsNothing(C1BillPay.GetData(i, "Provider")) Then
                    If (C1BillPay.GetData(i, "Provider").ToString().Trim <> "") Then

                        Dim _dr() As DataRow
                        _dr = dtProviders.[Select]("Provider = '" & Convert.ToString(C1BillPay.GetData(i, "Provider")).Replace("'", "''") & "'")
                        If Not IsNothing(_dr) And _dr.Length > 0 Then
                            _nProviderID = Convert.ToInt64(_dr(0).Item("nProviderID"))
                        Else
                            _nProviderID = Nothing
                        End If

                    End If
                End If

                If Not IsNothing(C1BillPay.GetData(i, "Location")) Then
                    If (C1BillPay.GetData(i, "Location").ToString().Trim <> "") Then

                        Dim _dr() As DataRow
                        _dr = dtLocation.[Select]("Location = '" & Convert.ToString(C1BillPay.GetData(i, "Location")).Replace("'", "''") & "'")
                        If Not IsNothing(_dr) And _dr.Length > 0 Then
                            _nLocationID = Convert.ToInt64(_dr(0).Item("nLocationID"))
                        Else
                            _nLocationID = Nothing
                        End If

                    End If
                End If


                'If Not IsNothing(C1BillPay.GetData(i, "User")) Then
                '    If (C1BillPay.GetData(i, "User").ToString().Trim <> "") Then

                '        Dim _dr() As DataRow
                '        _dr = dtUser.[Select]("sLoginName = '" & Convert.ToString(C1BillPay.GetData(i, "User")).Replace("'", "''") & "'")
                '        If Not IsNothing(_dr) And _dr.Length > 0 Then
                '            _nUserID = Convert.ToInt64(_dr(0).Item("nUserID"))
                '        Else
                '            _nUserID = Nothing

                '        End If

                '    End If
                'End If

                If Not IsNothing(C1BillPay.GetData(i, "User")) Then
                    If (C1BillPay.GetData(i, "User").ToString().Trim <> "") Then
                        Dim ostyle As C1.Win.C1FlexGrid.CellStyle
                        Dim strUserList As String
                        If Not IsNothing(C1BillPay.GetCellStyle(i, 4)) Then
                            ostyle = C1BillPay.GetCellStyle(i, 4)
                            strUserList = ostyle.ComboList
                            Dim splstruser As String() = strUserList.Split("|")
                            For j As Int16 = 0 To splstruser.Length - 1
                                Dim _dr() As DataRow
                                _dr = dtUser.[Select]("sLoginName = '" & Convert.ToString(splstruser(j).ToString()).Replace("'", "''") & "'")
                                If Not IsNothing(_dr) And _dr.Length > 0 Then
                                    If _nUserID = "" Then
                                        _nUserID = _dr(0).Item("nUserID")
                                    Else
                                        _nUserID = _nUserID + "|" + _dr(0).Item("nUserID").ToString()
                                    End If
                                End If
                                _dr = Nothing
                            Next
                        End If
                        strUserList = Nothing
                        ostyle = Nothing
                    End If
                End If

                Dim _drNew As DataRow = dtgrid.NewRow()
                _drNew("nID") = 0
                _drNew("nPriorityID") = i
                _drNew("nProviderID") = _nProviderID
                _drNew("nLocationId") = _nLocationID
                _drNew("nUserID") = _nUserID
                _drNew("sMessageType") = "Online Bill Pay"
                _drNew("sSettingType") = "Advance"

                dtgrid.Rows.Add(_drNew)

            Next

            ''Added for MU2 Patient portal implementation on 20130626
            For i As Integer = 1 To C1PortalUsers.Rows.Count - 1

                _nProviderID = 0
                _nLocationID = 0
                _nUserID = ""

                If Not IsNothing(C1PortalUsers.GetData(i, "Provider")) Then
                    If (C1PortalUsers.GetData(i, "Provider").ToString().Trim <> "") Then

                        Dim _dr() As DataRow
                        _dr = dtProviders.[Select]("Provider = '" & Convert.ToString(C1PortalUsers.GetData(i, "Provider")).Replace("'", "''") & "'")
                        If Not IsNothing(_dr) And _dr.Length > 0 Then
                            _nProviderID = Convert.ToInt64(_dr(0).Item("nProviderID"))
                        Else
                            _nProviderID = Nothing
                        End If

                    End If
                End If

                If Not IsNothing(C1PortalUsers.GetData(i, "Location")) Then
                    If (C1PortalUsers.GetData(i, "Location").ToString().Trim <> "") Then

                        Dim _dr() As DataRow
                        _dr = dtLocation.[Select]("Location = '" & Convert.ToString(C1PortalUsers.GetData(i, "Location")).Replace("'", "''") & "'")
                        If Not IsNothing(_dr) And _dr.Length > 0 Then
                            _nLocationID = Convert.ToInt64(_dr(0).Item("nLocationID"))
                        Else
                            _nLocationID = Nothing
                        End If

                    End If
                End If


                If Not IsNothing(C1PortalUsers.GetData(i, "User")) Then
                    If (C1PortalUsers.GetData(i, "User").ToString().Trim <> "") Then
                        Dim ostyle As C1.Win.C1FlexGrid.CellStyle
                        Dim strUserList As String
                        If Not IsNothing(C1PortalUsers.GetCellStyle(i, 4)) Then
                            ostyle = C1PortalUsers.GetCellStyle(i, 4)
                            strUserList = ostyle.ComboList
                            Dim splstruser As String() = strUserList.Split("|")
                            For j As Int16 = 0 To splstruser.Length - 1
                                Dim _dr() As DataRow
                                _dr = dtUser.[Select]("sLoginName = '" & Convert.ToString(splstruser(j).ToString()).Replace("'", "''") & "'")
                                If Not IsNothing(_dr) And _dr.Length > 0 Then
                                    If _nUserID = "" Then
                                        _nUserID = _dr(0).Item("nUserID")
                                    Else
                                        _nUserID = _nUserID + "|" + _dr(0).Item("nUserID").ToString()
                                    End If
                                End If
                                _dr = Nothing
                            Next
                        End If
                        strUserList = Nothing
                        ostyle = Nothing
                    End If
                End If

                Dim _drNew As DataRow = dtgrid.NewRow()
                _drNew("nID") = 0
                _drNew("nPriorityID") = i
                _drNew("nProviderID") = _nProviderID
                _drNew("nLocationId") = _nLocationID
                _drNew("nUserID") = _nUserID
                _drNew("sMessageType") = "Review Portal Users"
                _drNew("sSettingType") = "Advance"

                dtgrid.Rows.Add(_drNew)

            Next
            ''End
            For i As Integer = 1 To C1PatientFormUsers.Rows.Count - 1

                _nProviderID = 0
                _nLocationID = 0
                _nUserID = ""

                If Not IsNothing(C1PatientFormUsers.GetData(i, "Provider")) Then
                    If (C1PatientFormUsers.GetData(i, "Provider").ToString().Trim <> "") Then

                        Dim _dr() As DataRow
                        _dr = dtProviders.[Select]("Provider = '" & Convert.ToString(C1PatientFormUsers.GetData(i, "Provider")).Replace("'", "''") & "'")
                        If Not IsNothing(_dr) And _dr.Length > 0 Then
                            _nProviderID = Convert.ToInt64(_dr(0).Item("nProviderID"))
                        Else
                            _nProviderID = Nothing
                        End If

                    End If
                End If

                If Not IsNothing(C1PatientFormUsers.GetData(i, "Location")) Then
                    If (C1PatientFormUsers.GetData(i, "Location").ToString().Trim <> "") Then

                        Dim _dr() As DataRow
                        _dr = dtLocation.[Select]("Location = '" & Convert.ToString(C1PatientFormUsers.GetData(i, "Location")).Replace("'", "''") & "'")
                        If Not IsNothing(_dr) And _dr.Length > 0 Then
                            _nLocationID = Convert.ToInt64(_dr(0).Item("nLocationID"))
                        Else
                            _nLocationID = Nothing
                        End If

                    End If
                End If


                If Not IsNothing(C1PatientFormUsers.GetData(i, "User")) Then
                    If (C1PatientFormUsers.GetData(i, "User").ToString().Trim <> "") Then
                        Dim ostyle As C1.Win.C1FlexGrid.CellStyle
                        Dim strUserList As String
                        If Not IsNothing(C1PatientFormUsers.GetCellStyle(i, 4)) Then
                            ostyle = C1PatientFormUsers.GetCellStyle(i, 4)
                            strUserList = ostyle.ComboList
                            Dim splstruser As String() = strUserList.Split("|")
                            For j As Int16 = 0 To splstruser.Length - 1
                                Dim _dr() As DataRow
                                _dr = dtUser.[Select]("sLoginName = '" & Convert.ToString(splstruser(j).ToString()).Replace("'", "''") & "'")
                                If Not IsNothing(_dr) And _dr.Length > 0 Then
                                    If _nUserID = "" Then
                                        _nUserID = _dr(0).Item("nUserID")
                                    Else
                                        _nUserID = _nUserID + "|" + _dr(0).Item("nUserID").ToString()
                                    End If
                                End If
                                _dr = Nothing
                            Next
                        End If
                        strUserList = Nothing
                        ostyle = Nothing
                    End If
                End If

                Dim _drNew As DataRow = dtgrid.NewRow()
                _drNew("nID") = 0
                _drNew("nPriorityID") = i
                _drNew("nProviderID") = _nProviderID
                _drNew("nLocationId") = _nLocationID
                _drNew("nUserID") = _nUserID
                _drNew("sMessageType") = "Review Portal Users"
                _drNew("sSettingType") = "Advance"

                dtgrid.Rows.Add(_drNew)

            Next
            ''End Select

            Return dtgrid
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
            Return dtgrid
        Finally
            If Not IsNothing(dtgrid) Then
                dtgrid.Dispose()
                dtgrid = Nothing
            End If
        End Try
    End Function

    Private Function GetdataTOsaveDefaultUser() As DataTable

        Dim dtgrid As DataTable = New DataTable()

        Try
            dtgrid.Columns.Add("nID")
            dtgrid.Columns.Add("nPriorityID")
            dtgrid.Columns.Add("nProviderID")
            dtgrid.Columns.Add("nLocationID")
            dtgrid.Columns.Add("nUserID")
            dtgrid.Columns.Add("sMessageType")
            dtgrid.Columns.Add("sSettingType")


            Dim _nProviderID As Int64
            Dim _nLocationID As Int64
            Dim _nUserID As String

            'Select Case Tb_Messages.SelectedTab.Tag
            '    Case "Appointment Request"
            For i As Integer = 1 To c1AppointmentRequest.Rows.Count - 1

                _nProviderID = 0
                _nLocationID = 0
                _nUserID = ""

                If Not IsNothing(c1AppointmentRequest.GetData(i, "Provider")) Then
                    If (c1AppointmentRequest.GetData(i, "Provider").ToString().Trim <> "") Then

                        Dim _dr() As DataRow
                        _dr = dtProviders.[Select]("Provider = '" & Convert.ToString(c1AppointmentRequest.GetData(i, "Provider")).Replace("'", "''") & "'")
                        If Not IsNothing(_dr) And _dr.Length > 0 Then
                            _nProviderID = Convert.ToInt64(_dr(0).Item("nProviderID"))
                        Else
                            _nProviderID = Nothing
                        End If

                    End If
                End If

                If Not IsNothing(c1AppointmentRequest.GetData(i, "Location")) Then
                    If (c1AppointmentRequest.GetData(i, "Location").ToString().Trim <> "") Then

                        Dim _dr() As DataRow
                        _dr = dtLocation.[Select]("Location = '" & Convert.ToString(c1AppointmentRequest.GetData(i, "Location")).Replace("'", "''") & "'")
                        If Not IsNothing(_dr) And _dr.Length > 0 Then
                            _nLocationID = Convert.ToInt64(_dr(0).Item("nLocationID"))
                        Else
                            _nLocationID = Nothing
                        End If

                    End If
                End If


                If Not IsNothing(c1AppointmentRequest.GetData(i, "User")) Then
                    If (c1AppointmentRequest.GetData(i, "User").ToString().Trim <> "") Then
                        Dim ostyle As C1.Win.C1FlexGrid.CellStyle
                        Dim strUserList As String
                        If Not IsNothing(c1AppointmentRequest.GetCellStyle(i, 4)) Then
                            ostyle = c1AppointmentRequest.GetCellStyle(i, 4)
                            strUserList = ostyle.ComboList
                            Dim splstruser As String() = strUserList.Split("|")
                            For j As Int16 = 0 To splstruser.Length - 1
                                Dim _dr() As DataRow
                                _dr = dtDefaultUser.[Select]("sLoginName = '" & Convert.ToString(splstruser(j).ToString()).Replace("'", "''") & "'")
                                If Not IsNothing(_dr) And _dr.Length > 0 Then
                                    If _nUserID = "" Then
                                        _nUserID = _dr(0).Item("nUserID")
                                    Else
                                        _nUserID = _nUserID + "|" + _dr(0).Item("nUserID").ToString()
                                    End If
                                End If
                                _dr = Nothing
                            Next
                        End If
                        strUserList = Nothing
                        ostyle = Nothing
                    End If
                End If

                Dim _drNew As DataRow = dtgrid.NewRow()
                _drNew("nID") = 0
                _drNew("nPriorityID") = i
                _drNew("nProviderID") = _nProviderID
                _drNew("nLocationId") = _nLocationID
                _drNew("nUserID") = _nUserID
                _drNew("sMessageType") = "Appointment Request"
                _drNew("sSettingType") = "Advance"

                dtgrid.Rows.Add(_drNew)

            Next
            ' Case "Rx Renewal"
            For i As Integer = 1 To C1RxRenewal.Rows.Count - 1

                _nProviderID = 0
                _nLocationID = 0
                _nUserID = ""

                If Not IsNothing(C1RxRenewal.GetData(i, "Provider")) Then
                    If (C1RxRenewal.GetData(i, "Provider").ToString().Trim <> "") Then

                        Dim _dr() As DataRow
                        _dr = dtProviders.[Select]("Provider = '" & Convert.ToString(C1RxRenewal.GetData(i, "Provider")).Replace("'", "''") & "'")
                        If Not IsNothing(_dr) And _dr.Length > 0 Then
                            _nProviderID = Convert.ToInt64(_dr(0).Item("nProviderID"))
                        Else
                            _nProviderID = Nothing
                        End If

                    End If
                End If

                If Not IsNothing(C1RxRenewal.GetData(i, "Location")) Then
                    If (C1RxRenewal.GetData(i, "Location").ToString().Trim <> "") Then

                        Dim _dr() As DataRow
                        _dr = dtLocation.[Select]("Location = '" & Convert.ToString(C1RxRenewal.GetData(i, "Location")).Replace("'", "''") & "'")
                        If Not IsNothing(_dr) And _dr.Length > 0 Then
                            _nLocationID = Convert.ToInt64(_dr(0).Item("nLocationID"))
                        Else
                            _nLocationID = Nothing
                        End If

                    End If
                End If

                If Not IsNothing(C1RxRenewal.GetData(i, "User")) Then
                    If (C1RxRenewal.GetData(i, "User").ToString().Trim <> "") Then
                        Dim ostyle As C1.Win.C1FlexGrid.CellStyle
                        Dim strUserList As String
                        If Not IsNothing(C1RxRenewal.GetCellStyle(i, 4)) Then
                            ostyle = C1RxRenewal.GetCellStyle(i, 4)
                            strUserList = ostyle.ComboList
                            Dim splstruser As String() = strUserList.Split("|")
                            For j As Int16 = 0 To splstruser.Length - 1
                                Dim _dr() As DataRow
                                _dr = dtDefaultUser.[Select]("sLoginName = '" & Convert.ToString(splstruser(j).ToString()).Replace("'", "''") & "'")
                                If Not IsNothing(_dr) And _dr.Length > 0 Then
                                    If _nUserID = "" Then
                                        _nUserID = _dr(0).Item("nUserID")
                                    Else
                                        _nUserID = _nUserID + "|" + _dr(0).Item("nUserID").ToString()
                                    End If
                                End If
                                _dr = Nothing
                            Next
                        End If
                        strUserList = Nothing
                        ostyle = Nothing
                    End If
                End If

                Dim _drNew As DataRow = dtgrid.NewRow()
                _drNew("nID") = 0
                _drNew("nPriorityID") = i
                _drNew("nProviderID") = _nProviderID
                _drNew("nLocationId") = _nLocationID
                _drNew("nUserID") = _nUserID
                _drNew("sMessageType") = "Rx Renewal"
                _drNew("sSettingType") = "Advance"

                dtgrid.Rows.Add(_drNew)

            Next
            'Case "Online Bill Pay"
            For i As Integer = 1 To C1BillPay.Rows.Count - 1

                _nProviderID = 0
                _nLocationID = 0
                _nUserID = ""

                If Not IsNothing(C1BillPay.GetData(i, "Provider")) Then
                    If (C1BillPay.GetData(i, "Provider").ToString().Trim <> "") Then

                        Dim _dr() As DataRow
                        _dr = dtProviders.[Select]("Provider = '" & Convert.ToString(C1BillPay.GetData(i, "Provider")).Replace("'", "''") & "'")
                        If Not IsNothing(_dr) And _dr.Length > 0 Then
                            _nProviderID = Convert.ToInt64(_dr(0).Item("nProviderID"))
                        Else
                            _nProviderID = Nothing
                        End If

                    End If
                End If

                If Not IsNothing(C1BillPay.GetData(i, "Location")) Then
                    If (C1BillPay.GetData(i, "Location").ToString().Trim <> "") Then

                        Dim _dr() As DataRow
                        _dr = dtLocation.[Select]("Location = '" & Convert.ToString(C1BillPay.GetData(i, "Location")).Replace("'", "''") & "'")
                        If Not IsNothing(_dr) And _dr.Length > 0 Then
                            _nLocationID = Convert.ToInt64(_dr(0).Item("nLocationID"))
                        Else
                            _nLocationID = Nothing
                        End If

                    End If
                End If


                'If Not IsNothing(C1BillPay.GetData(i, "User")) Then
                '    If (C1BillPay.GetData(i, "User").ToString().Trim <> "") Then

                '        Dim _dr() As DataRow
                '        _dr = dtUser.[Select]("sLoginName = '" & Convert.ToString(C1BillPay.GetData(i, "User")).Replace("'", "''") & "'")
                '        If Not IsNothing(_dr) And _dr.Length > 0 Then
                '            _nUserID = Convert.ToInt64(_dr(0).Item("nUserID"))
                '        Else
                '            _nUserID = Nothing

                '        End If

                '    End If
                'End If

                If Not IsNothing(C1BillPay.GetData(i, "User")) Then
                    If (C1BillPay.GetData(i, "User").ToString().Trim <> "") Then
                        Dim ostyle As C1.Win.C1FlexGrid.CellStyle
                        Dim strUserList As String
                        If Not IsNothing(C1BillPay.GetCellStyle(i, 4)) Then
                            ostyle = C1BillPay.GetCellStyle(i, 4)
                            strUserList = ostyle.ComboList
                            Dim splstruser As String() = strUserList.Split("|")
                            For j As Int16 = 0 To splstruser.Length - 1
                                Dim _dr() As DataRow
                                _dr = dtDefaultUser.[Select]("sLoginName = '" & Convert.ToString(splstruser(j).ToString()).Replace("'", "''") & "'")
                                If Not IsNothing(_dr) And _dr.Length > 0 Then
                                    If _nUserID = "" Then
                                        _nUserID = _dr(0).Item("nUserID")
                                    Else
                                        _nUserID = _nUserID + "|" + _dr(0).Item("nUserID").ToString()
                                    End If
                                End If
                                _dr = Nothing
                            Next
                        End If
                        strUserList = Nothing
                        ostyle = Nothing
                    End If
                End If

                Dim _drNew As DataRow = dtgrid.NewRow()
                _drNew("nID") = 0
                _drNew("nPriorityID") = i
                _drNew("nProviderID") = _nProviderID
                _drNew("nLocationId") = _nLocationID
                _drNew("nUserID") = _nUserID
                _drNew("sMessageType") = "Online Bill Pay"
                _drNew("sSettingType") = "Advance"

                dtgrid.Rows.Add(_drNew)

            Next

            ''Added for MU2 Patient portal implementation on 20130626
            For i As Integer = 1 To C1PortalUsers.Rows.Count - 1

                _nProviderID = 0
                _nLocationID = 0
                _nUserID = ""

                If Not IsNothing(C1PortalUsers.GetData(i, "Provider")) Then
                    If (C1PortalUsers.GetData(i, "Provider").ToString().Trim <> "") Then

                        Dim _dr() As DataRow
                        _dr = dtProviders.[Select]("Provider = '" & Convert.ToString(C1PortalUsers.GetData(i, "Provider")).Replace("'", "''") & "'")
                        If Not IsNothing(_dr) And _dr.Length > 0 Then
                            _nProviderID = Convert.ToInt64(_dr(0).Item("nProviderID"))
                        Else
                            _nProviderID = Nothing
                        End If

                    End If
                End If

                If Not IsNothing(C1PortalUsers.GetData(i, "Location")) Then
                    If (C1PortalUsers.GetData(i, "Location").ToString().Trim <> "") Then

                        Dim _dr() As DataRow
                        _dr = dtLocation.[Select]("Location = '" & Convert.ToString(C1PortalUsers.GetData(i, "Location")).Replace("'", "''") & "'")
                        If Not IsNothing(_dr) And _dr.Length > 0 Then
                            _nLocationID = Convert.ToInt64(_dr(0).Item("nLocationID"))
                        Else
                            _nLocationID = Nothing
                        End If

                    End If
                End If


                If Not IsNothing(C1PortalUsers.GetData(i, "User")) Then
                    If (C1PortalUsers.GetData(i, "User").ToString().Trim <> "") Then
                        Dim ostyle As C1.Win.C1FlexGrid.CellStyle
                        Dim strUserList As String
                        If Not IsNothing(C1PortalUsers.GetCellStyle(i, 4)) Then
                            ostyle = C1PortalUsers.GetCellStyle(i, 4)
                            strUserList = ostyle.ComboList
                            Dim splstruser As String() = strUserList.Split("|")
                            For j As Int16 = 0 To splstruser.Length - 1
                                Dim _dr() As DataRow
                                _dr = dtDefaultUser.[Select]("sLoginName = '" & Convert.ToString(splstruser(j).ToString()).Replace("'", "''") & "'")
                                If Not IsNothing(_dr) And _dr.Length > 0 Then
                                    If _nUserID = "" Then
                                        _nUserID = _dr(0).Item("nUserID")
                                    Else
                                        _nUserID = _nUserID + "|" + _dr(0).Item("nUserID").ToString()
                                    End If
                                End If
                                _dr = Nothing
                            Next
                        End If
                        strUserList = Nothing
                        ostyle = Nothing
                    End If
                End If

                Dim _drNew As DataRow = dtgrid.NewRow()
                _drNew("nID") = 0
                _drNew("nPriorityID") = i
                _drNew("nProviderID") = _nProviderID
                _drNew("nLocationId") = _nLocationID
                _drNew("nUserID") = _nUserID
                _drNew("sMessageType") = "Review Portal Users"
                _drNew("sSettingType") = "Advance"

                dtgrid.Rows.Add(_drNew)

            Next
            ''End
            For i As Integer = 1 To C1PatientFormUsers.Rows.Count - 1

                _nProviderID = 0
                _nLocationID = 0
                _nUserID = ""

                If Not IsNothing(C1PatientFormUsers.GetData(i, "Provider")) Then
                    If (C1PatientFormUsers.GetData(i, "Provider").ToString().Trim <> "") Then

                        Dim _dr() As DataRow
                        _dr = dtProviders.[Select]("Provider = '" & Convert.ToString(C1PatientFormUsers.GetData(i, "Provider")).Replace("'", "''") & "'")
                        If Not IsNothing(_dr) And _dr.Length > 0 Then
                            _nProviderID = Convert.ToInt64(_dr(0).Item("nProviderID"))
                        Else
                            _nProviderID = Nothing
                        End If

                    End If
                End If

                If Not IsNothing(C1PatientFormUsers.GetData(i, "Location")) Then
                    If (C1PatientFormUsers.GetData(i, "Location").ToString().Trim <> "") Then

                        Dim _dr() As DataRow
                        _dr = dtLocation.[Select]("Location = '" & Convert.ToString(C1PatientFormUsers.GetData(i, "Location")).Replace("'", "''") & "'")
                        If Not IsNothing(_dr) And _dr.Length > 0 Then
                            _nLocationID = Convert.ToInt64(_dr(0).Item("nLocationID"))
                        Else
                            _nLocationID = Nothing
                        End If

                    End If
                End If


                If Not IsNothing(C1PatientFormUsers.GetData(i, "User")) Then
                    If (C1PatientFormUsers.GetData(i, "User").ToString().Trim <> "") Then
                        Dim ostyle As C1.Win.C1FlexGrid.CellStyle
                        Dim strUserList As String
                        If Not IsNothing(C1PatientFormUsers.GetCellStyle(i, 4)) Then
                            ostyle = C1PatientFormUsers.GetCellStyle(i, 4)
                            strUserList = ostyle.ComboList
                            Dim splstruser As String() = strUserList.Split("|")
                            For j As Int16 = 0 To splstruser.Length - 1
                                Dim _dr() As DataRow
                                _dr = dtUser.[Select]("sLoginName = '" & Convert.ToString(splstruser(j).ToString()).Replace("'", "''") & "'")
                                If Not IsNothing(_dr) And _dr.Length > 0 Then
                                    If _nUserID = "" Then
                                        _nUserID = _dr(0).Item("nUserID")
                                    Else
                                        _nUserID = _nUserID + "|" + _dr(0).Item("nUserID").ToString()
                                    End If
                                End If
                                _dr = Nothing
                            Next
                        End If
                        strUserList = Nothing
                        ostyle = Nothing
                    End If
                End If

                Dim _drNew As DataRow = dtgrid.NewRow()
                _drNew("nID") = 0
                _drNew("nPriorityID") = i
                _drNew("nProviderID") = _nProviderID
                _drNew("nLocationId") = _nLocationID
                _drNew("nUserID") = _nUserID
                _drNew("sMessageType") = "Online Patient Form"
                _drNew("sSettingType") = "Advance"

                dtgrid.Rows.Add(_drNew)

            Next
            ''End Select

            Return dtgrid
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
            Return dtgrid
        Finally
            If Not IsNothing(dtgrid) Then
                dtgrid.Dispose()
                dtgrid = Nothing
            End If
        End Try
    End Function
    Private Function GetMessageMapping(ByVal sMessageType As String) As DataTable
        Dim dt As DataTable = Nothing
        Dim oclsgloIntuit As clsgloIntuit = Nothing
        Try
            oclsgloIntuit = New clsgloIntuit()
            dt = oclsgloIntuit.GetMapping(sMessageType)
            Return dt
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
            Return dt
        Finally
            If Not IsNothing(oclsgloIntuit) Then
                oclsgloIntuit.Dispose()
                oclsgloIntuit = Nothing
            End If
            If Not IsNothing(dt) Then
                dt.Dispose()
                dt = Nothing
            End If
        End Try
    End Function

    Private Sub SetData(ByVal dt As DataTable)
        Dim drDefault() As DataRow
        Dim drAdvance() As DataRow
        Try
            drDefault = dt.Select("sSettingType='Default' and sMessageType='Appointment Request'")
            If drDefault.Length > 0 Then
                cmbApptUser.Text = drDefault(0).Item(5)
            End If
            drDefault = dt.Select("sSettingType='Default' and sMessageType='Rx Renewal'")
            If drDefault.Length > 0 Then
                cmbRxUser.Text = drDefault(0).Item(5)
            End If
            drDefault = dt.Select("sSettingType='Default' and sMessageType='Online Bill Pay'")
            If drDefault.Length > 0 Then
                cmbBillPayUser.Text = drDefault(0).Item(5)
            End If
            ''Added for MU2 Patient portal implementation on 20130626
            drDefault = dt.Select("sSettingType='Default' and sMessageType='Review Portal Users'")
            If drDefault.Length > 0 Then
                cmbPortalUsers.Text = drDefault(0).Item(5)
            End If
            ''End
            drDefault = dt.Select("sSettingType='Default' and sMessageType='Online Patient Form'")
            If drDefault.Length > 0 Then
                cmbPatientFormUsers.Text = drDefault(0).Item(5)
            End If

            c1AppointmentRequest.Rows.RemoveRange(1, c1AppointmentRequest.Rows.Count - 1)
            C1RxRenewal.Rows.RemoveRange(1, C1RxRenewal.Rows.Count - 1)
            C1BillPay.Rows.RemoveRange(1, C1BillPay.Rows.Count - 1)
            ''Added for MU2 Patient portal implementation on 20130626
            C1PortalUsers.Rows.RemoveRange(1, C1PortalUsers.Rows.Count - 1)
            ''End
            C1PatientFormUsers.Rows.RemoveRange(1, C1PatientFormUsers.Rows.Count - 1)

            drAdvance = dt.Select("sSettingType='Advance'  and sMessageType='Appointment Request'")

            For i As Integer = 0 To drAdvance.Length - 1
                c1AppointmentRequest.Rows.Add()
                c1AppointmentRequest.SetData(i + 1, "Provider", drAdvance(i).Item(1))
                c1AppointmentRequest.SetData(i + 1, "Location", drAdvance(i).Item(3))
                'c1AppointmentRequest.SetData(i + 1, "User", drAdvance(i).Item(5))

                Dim strUserList As String
                strUserList = drAdvance(i).Item(5)
                Dim cstyle As C1.Win.C1FlexGrid.CellStyle = Nothing
                cstyle = c1AppointmentRequest.Styles.Add("BubbleValues" & i + 1)
                Dim ocell As C1.Win.C1FlexGrid.CellRange = Nothing
                cstyle.ComboList = strUserList
                c1AppointmentRequest.SetData(i + 1, 4, "")
                ocell = c1AppointmentRequest.GetCellRange(i + 1, 4, i + 1, 4)
                ocell.Style = cstyle
                Dim splstruser As String() = strUserList.Split("|")
                If splstruser.Length > 0 Then
                    ocell.Data = splstruser(0)
                End If
                c1AppointmentRequest.SetCellImage(i + 1, 5, imgTreeVIew.Images(0))
                cstyle = Nothing
                strUserList = Nothing
            Next

            drAdvance = dt.Select("sSettingType='Advance'  and sMessageType='Rx Renewal'")
            For i As Integer = 0 To drAdvance.Length - 1
                C1RxRenewal.Rows.Add()
                C1RxRenewal.SetData(i + 1, "Provider", drAdvance(i).Item(1))
                C1RxRenewal.SetData(i + 1, "Location", drAdvance(i).Item(3))
                'C1RxRenewal.SetData(i + 1, "User", drAdvance(i).Item(5))

                Dim strUserList As String
                strUserList = drAdvance(i).Item(5)
                Dim cstyle As C1.Win.C1FlexGrid.CellStyle = Nothing
                cstyle = C1RxRenewal.Styles.Add("BubbleValues" & i + 1)
                Dim ocell As C1.Win.C1FlexGrid.CellRange = Nothing
                cstyle.ComboList = strUserList
                C1RxRenewal.SetData(i + 1, 4, "")
                ocell = C1RxRenewal.GetCellRange(i + 1, 4, i + 1, 4)
                ocell.Style = cstyle
                Dim splstruser As String() = strUserList.Split("|")
                If splstruser.Length > 0 Then
                    ocell.Data = splstruser(0)
                End If
                C1RxRenewal.SetCellImage(i + 1, 5, imgTreeVIew.Images(0))
                cstyle = Nothing
                strUserList = Nothing
            Next

            drAdvance = dt.Select("sSettingType='Advance'  and sMessageType='Online Bill Pay'")
            For i As Integer = 0 To drAdvance.Length - 1
                C1BillPay.Rows.Add()
                C1BillPay.SetData(i + 1, "Provider", drAdvance(i).Item(1))
                C1BillPay.SetData(i + 1, "Location", drAdvance(i).Item(3))
                'C1BillPay.SetData(i + 1, "User", drAdvance(i).Item(5))

                Dim strUserList As String
                strUserList = drAdvance(i).Item(5)
                Dim cstyle As C1.Win.C1FlexGrid.CellStyle = Nothing
                cstyle = C1BillPay.Styles.Add("BubbleValues" & i + 1)
                Dim ocell As C1.Win.C1FlexGrid.CellRange = Nothing
                cstyle.ComboList = strUserList
                C1BillPay.SetData(i + 1, 4, "")
                ocell = C1BillPay.GetCellRange(i + 1, 4, i + 1, 4)
                ocell.Style = cstyle
                Dim splstruser As String() = strUserList.Split("|")
                If splstruser.Length > 0 Then
                    ocell.Data = splstruser(0)
                End If
                C1BillPay.SetCellImage(i + 1, 5, imgTreeVIew.Images(0))
                cstyle = Nothing
                strUserList = Nothing
            Next

            ''Added for MU2 Patient portal implementation on 20130626
            drAdvance = dt.Select("sSettingType='Advance'  and sMessageType='Review Portal Users'")

            For i As Integer = 0 To drAdvance.Length - 1
                C1PortalUsers.Rows.Add()
                C1PortalUsers.SetData(i + 1, "Provider", drAdvance(i).Item(1))
                C1PortalUsers.SetData(i + 1, "Location", drAdvance(i).Item(3))
                'C1PortalUsers.SetData(i + 1, "User", drAdvance(i).Item(5))

                Dim strUserList As String
                strUserList = drAdvance(i).Item(5)
                Dim cstyle As C1.Win.C1FlexGrid.CellStyle = Nothing
                cstyle = C1PortalUsers.Styles.Add("BubbleValues" & i + 1)
                Dim ocell As C1.Win.C1FlexGrid.CellRange = Nothing
                cstyle.ComboList = strUserList
                C1PortalUsers.SetData(i + 1, 4, "")
                ocell = C1PortalUsers.GetCellRange(i + 1, 4, i + 1, 4)
                ocell.Style = cstyle
                Dim splstruser As String() = strUserList.Split("|")
                If splstruser.Length > 0 Then
                    ocell.Data = splstruser(0)
                End If
                C1PortalUsers.SetCellImage(i + 1, 5, imgTreeVIew.Images(0))
                cstyle = Nothing
                strUserList = Nothing
            Next
            ''End

            drAdvance = dt.Select("sSettingType='Advance'  and sMessageType='Online Patient Form'")

            For i As Integer = 0 To drAdvance.Length - 1
                C1PatientFormUsers.Rows.Add()
                C1PatientFormUsers.SetData(i + 1, "Provider", drAdvance(i).Item(1))
                C1PatientFormUsers.SetData(i + 1, "Location", drAdvance(i).Item(3))
                'C1PatientFormUsers.SetData(i + 1, "User", drAdvance(i).Item(5))

                Dim strUserList As String
                strUserList = drAdvance(i).Item(5)
                Dim cstyle As C1.Win.C1FlexGrid.CellStyle = Nothing
                cstyle = C1PatientFormUsers.Styles.Add("BubbleValues" & i + 1)
                Dim ocell As C1.Win.C1FlexGrid.CellRange = Nothing
                cstyle.ComboList = strUserList
                C1PatientFormUsers.SetData(i + 1, 4, "")
                ocell = C1PatientFormUsers.GetCellRange(i + 1, 4, i + 1, 4)
                ocell.Style = cstyle
                Dim splstruser As String() = strUserList.Split("|")
                If splstruser.Length > 0 Then
                    ocell.Data = splstruser(0)
                End If
                C1PatientFormUsers.SetCellImage(i + 1, 5, imgTreeVIew.Images(0))
                cstyle = Nothing
                strUserList = Nothing
            Next

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        End Try

    End Sub

    Private Sub ts_btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ts_btnClose.Click
        Me.Close()
    End Sub

    Private Function SaveMapping(ByVal dtMapping As DataTable, ByVal sMessageType As String) As Boolean
        Dim _IsDataSaved As Boolean = False
        Dim oclsgloIntuit As clsgloIntuit = Nothing
        Try
            oclsgloIntuit = New clsgloIntuit()
            _IsDataSaved = oclsgloIntuit.SaveMapping(dtMapping, sMessageType)
            Return _IsDataSaved
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
            Return _IsDataSaved
        Finally
            If Not IsNothing(oclsgloIntuit) Then
                oclsgloIntuit.Dispose()
                oclsgloIntuit = Nothing
            End If

        End Try
    End Function

    Private Function AddBasicSetting(ByVal dt As DataTable) As DataTable
        Dim dtBasicSetting As DataTable
        Dim _drNew As DataRow
        Try
            dtBasicSetting = dt
            _drNew = dtBasicSetting.NewRow()
            _drNew("nID") = 0
            _drNew("nPriorityID") = 0
            _drNew("nProviderID") = 0
            _drNew("nLocationId") = 0
            _drNew("nUserID") = cmbApptUser.SelectedValue
            _drNew("sMessageType") = "Appointment Request"
            _drNew("sSettingType") = "Default"

            dtBasicSetting.Rows.Add(_drNew)
            _drNew = Nothing

            _drNew = dtBasicSetting.NewRow()
            _drNew("nID") = 0
            _drNew("nPriorityID") = 0
            _drNew("nProviderID") = 0
            _drNew("nLocationId") = 0
            _drNew("nUserID") = cmbRxUser.SelectedValue
            _drNew("sMessageType") = "Rx Renewal"
            _drNew("sSettingType") = "Default"

            dtBasicSetting.Rows.Add(_drNew)
            _drNew = Nothing

            _drNew = dtBasicSetting.NewRow()
            _drNew("nID") = 0
            _drNew("nPriorityID") = 0
            _drNew("nProviderID") = 0
            _drNew("nLocationId") = 0
            _drNew("nUserID") = cmbBillPayUser.SelectedValue
            _drNew("sMessageType") = "Online Bill Pay"
            _drNew("sSettingType") = "Default"

            dtBasicSetting.Rows.Add(_drNew)
            _drNew = Nothing

            ''Added for MU2 Patient portal implementation on 20130626
            _drNew = dtBasicSetting.NewRow()
            _drNew("nID") = 0
            _drNew("nPriorityID") = 0
            _drNew("nProviderID") = 0
            _drNew("nLocationId") = 0
            _drNew("nUserID") = cmbPortalUsers.SelectedValue
            _drNew("sMessageType") = "Review Portal Users"
            _drNew("sSettingType") = "Default"
            ''End

            dtBasicSetting.Rows.Add(_drNew)
            _drNew = Nothing

            _drNew = dtBasicSetting.NewRow()
            _drNew("nID") = 0
            _drNew("nPriorityID") = 0
            _drNew("nProviderID") = 0
            _drNew("nLocationId") = 0
            _drNew("nUserID") = cmbPatientFormUsers.SelectedValue
            _drNew("sMessageType") = "Online Patient Form"
            _drNew("sSettingType") = "Default"

            dtBasicSetting.Rows.Add(_drNew)
            _drNew = Nothing

            Return dtBasicSetting
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
            Return dtBasicSetting
        End Try
    End Function

    Private Function AddBasicDefaultUserSetting(ByVal dt As DataTable) As DataTable
        Dim dtBasicSetting As DataTable
        Dim _drNew As DataRow
        Try
            dtBasicSetting = dt

            'For i As Integer = 0 To cmbApptDefaultUser.Items.Count
            '    _drNew = dtBasicSetting.NewRow()
            '    _drNew("nID") = 0
            '    _drNew("nPriorityID") = 0
            '    _drNew("nProviderID") = 0
            '    _drNew("nLocationId") = 0
            '    _drNew("nUserID") = cmbApptDefaultUser.Items(i).value
            '    _drNew("sMessageType") = "Appointment Request"
            '    _drNew("sSettingType") = "Default"

            '    dtBasicSetting.Rows.Add(_drNew)
            '    _drNew = Nothing
            'Next

            For Each dr As DataRowView In cmbApptDefaultUser.Items
                _drNew = dtBasicSetting.NewRow()
                _drNew("nID") = 0
                _drNew("nPriorityID") = 0
                _drNew("nProviderID") = 0
                _drNew("nLocationId") = 0
                _drNew("nUserID") = dr.Item(2)
                _drNew("sMessageType") = "Appointment Request"
                _drNew("sSettingType") = "Default"

                dtBasicSetting.Rows.Add(_drNew)
                _drNew = Nothing
            Next
            '_drNew = dtBasicSetting.NewRow()
            '_drNew("nID") = 0
            '_drNew("nPriorityID") = 0
            '_drNew("nProviderID") = 0
            '_drNew("nLocationId") = 0
            '_drNew("nUserID") = cmbApptUser.SelectedValue
            '_drNew("sMessageType") = "Appointment Request"
            '_drNew("sSettingType") = "Default"

            'dtBasicSetting.Rows.Add(_drNew)
            '_drNew = Nothing

            For Each dr As DataRowView In cmbRxDefaultUser.Items
                _drNew = dtBasicSetting.NewRow()
                _drNew("nID") = 0
                _drNew("nPriorityID") = 0
                _drNew("nProviderID") = 0
                _drNew("nLocationId") = 0
                _drNew("nUserID") = dr.Item(2)
                _drNew("sMessageType") = "Rx Renewal"
                _drNew("sSettingType") = "Default"

                dtBasicSetting.Rows.Add(_drNew)
                _drNew = Nothing
            Next

            For Each dr As DataRowView In cmbBillPayDefaultUser.Items
                _drNew = dtBasicSetting.NewRow()
                _drNew("nID") = 0
                _drNew("nPriorityID") = 0
                _drNew("nProviderID") = 0
                _drNew("nLocationId") = 0
                _drNew("nUserID") = dr.Item(2)
                _drNew("sMessageType") = "Online Bill Pay"
                _drNew("sSettingType") = "Default"

                dtBasicSetting.Rows.Add(_drNew)
                _drNew = Nothing
            Next

            For Each dr As DataRowView In cmbPortalDefaultUser.Items
                _drNew = dtBasicSetting.NewRow()
                _drNew("nID") = 0
                _drNew("nPriorityID") = 0
                _drNew("nProviderID") = 0
                _drNew("nLocationId") = 0
                _drNew("nUserID") = dr.Item(2)
                _drNew("sMessageType") = "Review Portal Users"
                _drNew("sSettingType") = "Default"

                dtBasicSetting.Rows.Add(_drNew)
                _drNew = Nothing
            Next

            For Each dr As DataRowView In cmbPatientFormDefaultUser.Items
                _drNew = dtBasicSetting.NewRow()
                _drNew("nID") = 0
                _drNew("nPriorityID") = 0
                _drNew("nProviderID") = 0
                _drNew("nLocationId") = 0
                _drNew("nUserID") = dr.Item(2)
                _drNew("sMessageType") = "Online Patient Form"
                _drNew("sSettingType") = "Default"

                dtBasicSetting.Rows.Add(_drNew)
                _drNew = Nothing
            Next


            Return dtBasicSetting
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
            Return dtBasicSetting
        End Try
    End Function

    Private Sub Tb_Messages_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Tb_Messages.Click
        ' LoadData(Tb_Messages.SelectedTab.Tag)
    End Sub

    Private Sub btnRxUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRxUp.Click
        Try
            If C1RxRenewal.Rows.Count > 1 Then
                If C1RxRenewal.RowSel = 1 Then
                    C1RxRenewal.RowSel = 1
                Else
                    C1RxRenewal.Rows(C1RxRenewal.RowSel).Move(C1RxRenewal.RowSel - 1)
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        End Try
    End Sub

    Private Sub btnRxDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRxDown.Click
        Try
            If C1RxRenewal.Rows.Count > 1 Then
                If C1RxRenewal.RowSel = C1RxRenewal.Rows.Count - 1 Then
                    C1RxRenewal.RowSel = C1RxRenewal.Rows.Count - 1
                Else
                    C1RxRenewal.Rows(C1RxRenewal.RowSel).Move(C1RxRenewal.RowSel + 1)
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        End Try
    End Sub

    Private Sub btnBillUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBillUp.Click
        Try
            If C1BillPay.Rows.Count > 1 Then
                If C1BillPay.RowSel = 1 Then
                    C1BillPay.RowSel = 1
                Else
                    C1BillPay.Rows(C1BillPay.RowSel).Move(C1BillPay.RowSel - 1)
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        End Try
    End Sub

    Private Sub btnBillDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBillDown.Click
        Try
            If C1BillPay.Rows.Count > 1 Then
                If C1BillPay.RowSel = C1BillPay.Rows.Count - 1 Then
                    C1BillPay.RowSel = C1BillPay.Rows.Count - 1
                Else
                    C1BillPay.Rows(C1BillPay.RowSel).Move(C1BillPay.RowSel + 1)
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        End Try
    End Sub

    Dim _ToList As gloGeneralItem.gloItems
    Dim _ToDefaultUserList As gloGeneralItem.gloItems
    Dim _ToApptDefaultUserList As gloGeneralItem.gloItems
    Dim _ToRxDefaultUserList As gloGeneralItem.gloItems
    Dim _ToBillDefaultUserList As gloGeneralItem.gloItems
    Dim _ToPortalDefaultUserList As gloGeneralItem.gloItems
    Dim _ToPatientFormDefaultUserList As gloGeneralItem.gloItems
    Dim ofrmList As New frmUserList
    Dim ofrmDefaultList As New frmUserList
    Dim oListUsers As gloListControl.gloListControl

    Private Sub c1AppointmentRequest_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles c1AppointmentRequest.Click

        Dim r As Integer = c1AppointmentRequest.RowSel
        Dim ostyle As C1.Win.C1FlexGrid.CellStyle
        nSelectedRow = r
        Dim strUserList As String = ""

        If r >= 0 Then
            If c1AppointmentRequest.ColSel = 5 Then

                If Not IsNothing(c1AppointmentRequest.GetCellStyle(r, 4)) Then
                    ostyle = c1AppointmentRequest.GetCellStyle(r, 4)
                    strUserList = ostyle.ComboList
                Else
                    If Not IsNothing(c1AppointmentRequest.GetData(r, 4)) Then
                        If c1AppointmentRequest.GetData(r, 4) <> "" Then
                            strUserList = c1AppointmentRequest.GetData(r, 4)
                        End If
                    End If
                End If

                If Trim(strUserList) <> "" Then
                    Dim dtprv As New DataTable
                    Dim nUserID As New DataColumn("nUserID")
                    Dim Description As New DataColumn("Description")
                    dtprv.Columns.Add(nUserID)
                    dtprv.Columns.Add(Description)

                    Dim splstruser As String() = strUserList.Split("|")

                    For i As Int16 = 0 To splstruser.Length - 1
                        Dim drTemp As DataRow = dtprv.NewRow()
                        'drTemp("nUserID") = 216512308151873336

                        Dim _dr() As DataRow
                        _dr = dtUser.[Select]("sLoginName = '" & Convert.ToString(splstruser(i).ToString()).Replace("'", "''") & "'")
                        If Not IsNothing(_dr) And _dr.Length > 0 Then
                            drTemp("nUserID") = Convert.ToInt64(_dr(0).Item("nUserID"))
                        End If
                        _dr = Nothing
                        drTemp("Description") = splstruser(i).ToString()
                        dtprv.Rows.Add(drTemp)
                    Next

                    If dtprv.Rows.Count > 0 Then
                        Dim ToItemp As gloGeneralItem.gloItem
                        _ToList = New gloGeneralItem.gloItems
                        For i As Int16 = 0 To dtprv.Rows.Count - 1
                            ToItemp = New gloGeneralItem.gloItem()
                            ToItemp.ID = dtprv.Rows(i)("nUserID")
                            ToItemp.Description = dtprv.Rows(i)("Description")
                            _ToList.Add(ToItemp)
                            ToItemp = Nothing
                        Next
                    End If
                Else
                    If IsNothing(_ToList) = False Then
                        _ToList.Clear()
                    End If
                End If
            End If
        End If

        Dim strUsers As String = ""

        With c1AppointmentRequest
            If r >= 0 Then
                If .ColSel = 5 Then
                    oListUsers = New gloListControl.gloListControl(gloEMRAdmin.mdlGeneral.GetConnectionString(), gloListControl.gloListControlType.Users, True, Me.Width)
                    oListUsers.ControlHeader = "Users"
                    AddHandler oListUsers.ItemSelectedClick, AddressOf olistUsers_ItemSelectedClick
                    AddHandler oListUsers.ItemClosedClick, AddressOf olistUsers_ItemClosedClick
                    'To Select already Added Users.
                    If IsNothing(_ToList) = False Then
                        For i As Integer = 0 To _ToList.Count - 1
                            oListUsers.SelectedItems.Add(_ToList(i))
                        Next
                    End If
                    ofrmList.Controls.Add(oListUsers)
                    oListUsers.Dock = DockStyle.Fill
                    oListUsers.BringToFront()
                    oListUsers.ShowHeaderPanel(False)
                    oListUsers.OpenControl()
                    ofrmList.Text = "Users"
                    ofrmList.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedSingle
                    ofrmList.ShowDialog()
                End If
            End If
        End With
    End Sub

    Private Sub olistUsers_ItemSelectedClick(ByVal sender As Object, ByVal e As EventArgs)
        Dim dtUsers As New DataTable()
        Dim dcnIntuitStaffMappingDetailID As New DataColumn("nIntuitStaffMappingDetailID")
        Dim dcnIntuitStaffMappingID As New DataColumn("nIntuitStaffMappingID")
        Dim dcId As New DataColumn("nUserID")
        Dim dcDescription As New DataColumn("Description")
        dtUsers.Columns.Add(dcId)
        dtUsers.Columns.Add(dcDescription)
        _ToList = New gloGeneralItem.gloItems
        Dim ToItem As gloGeneralItem.gloItem

        If oListUsers.SelectedItems.Count > 0 Then
            For i As Int16 = 0 To oListUsers.SelectedItems.Count - 1
                Dim drTemp As DataRow = dtUsers.NewRow()
                drTemp("nUserID") = oListUsers.SelectedItems(i).ID
                drTemp("Description") = oListUsers.SelectedItems(i).Description
                dtUsers.Rows.Add(drTemp)

                ToItem = New gloGeneralItem.gloItem()

                ToItem.ID = oListUsers.SelectedItems(i).ID
                ToItem.Description = oListUsers.SelectedItems(i).Description

                _ToList.Add(ToItem)
                '
                ToItem = Nothing
            Next
        End If
        RefreshUsers(dtUsers)
        ofrmList.Close()
    End Sub

    Dim nSelectedRow As Integer = 0

    Public Sub RefreshUsers(ByVal dt As DataTable)
        Try
            Dim strUserList As String = ""
            Dim strUserID As String = ""

            For i As Int16 = 0 To dt.Rows.Count - 1
                If strUserList = "" Then
                    strUserList = dt.Rows(i)("Description")
                    strUserID = dt.Rows(i)("nUserID")
                Else
                    strUserList = strUserList + "|" + dt.Rows(i)("Description")
                    strUserID = strUserID + "|" + dt.Rows(i)("nUserID")
                End If
            Next

            Select Case Tb_Messages.SelectedTab.Tag.ToString()
                Case "Appointment Request"
                    If dt.Rows.Count > 0 Then
                        Dim cstyle As C1.Win.C1FlexGrid.CellStyle = Nothing
                        Dim ocell As C1.Win.C1FlexGrid.CellRange = Nothing
                        cstyle = c1AppointmentRequest.Styles.Add("BubbleValues" & nSelectedRow)
                        cstyle.ComboList = strUserList
                        c1AppointmentRequest.SetData(nSelectedRow, 4, "")
                        ocell = c1AppointmentRequest.GetCellRange(nSelectedRow, 4, nSelectedRow, 4)
                        ocell.Style = cstyle
                        Dim splstruser As String() = strUserList.Split("|")
                        If splstruser.Length > 0 Then
                            ocell.Data = splstruser(0)
                        End If
                        splstruser = Nothing
                        c1AppointmentRequest.[Select](nSelectedRow, 4)
                    Else
                        Dim cstyle As C1.Win.C1FlexGrid.CellStyle = Nothing
                        Dim ocell As C1.Win.C1FlexGrid.CellRange = Nothing
                        cstyle = c1AppointmentRequest.Styles.Add("BubbleValues" & nSelectedRow)
                        cstyle.ComboList = " "
                        c1AppointmentRequest.SetData(nSelectedRow, 4, "")
                        c1AppointmentRequest.[Select](nSelectedRow, 4)
                        ocell = c1AppointmentRequest.GetCellRange(nSelectedRow, 4, nSelectedRow, 4)
                        ocell.Style = cstyle
                    End If
                Case "Rx Renewal"
                    If dt.Rows.Count > 0 Then
                        Dim cstyle As C1.Win.C1FlexGrid.CellStyle = Nothing
                        Dim ocell As C1.Win.C1FlexGrid.CellRange = Nothing
                        cstyle = C1RxRenewal.Styles.Add("BubbleValues" & nSelectedRow)
                        cstyle.ComboList = strUserList
                        C1RxRenewal.SetData(nSelectedRow, 4, "")
                        ocell = C1RxRenewal.GetCellRange(nSelectedRow, 4, nSelectedRow, 4)
                        ocell.Style = cstyle
                        Dim splstruser As String() = strUserList.Split("|")
                        If splstruser.Length > 0 Then
                            ocell.Data = splstruser(0)
                        End If
                        splstruser = Nothing
                        C1RxRenewal.[Select](nSelectedRow, 4)
                    Else
                        Dim cstyle As C1.Win.C1FlexGrid.CellStyle = Nothing
                        Dim ocell As C1.Win.C1FlexGrid.CellRange = Nothing
                        cstyle = C1RxRenewal.Styles.Add("BubbleValues" & nSelectedRow)
                        cstyle.ComboList = " "
                        C1RxRenewal.SetData(nSelectedRow, 4, "")
                        C1RxRenewal.[Select](nSelectedRow, 4)
                        ocell = C1RxRenewal.GetCellRange(nSelectedRow, 4, nSelectedRow, 4)
                        ocell.Style = cstyle
                    End If
                Case "Online Bill Pay"
                    If dt.Rows.Count > 0 Then
                        Dim cstyle As C1.Win.C1FlexGrid.CellStyle = Nothing
                        Dim ocell As C1.Win.C1FlexGrid.CellRange = Nothing
                        cstyle = C1BillPay.Styles.Add("BubbleValues" & nSelectedRow)
                        cstyle.ComboList = strUserList
                        C1BillPay.SetData(nSelectedRow, 4, "")
                        ocell = C1BillPay.GetCellRange(nSelectedRow, 4, nSelectedRow, 4)
                        ocell.Style = cstyle
                        Dim splstruser As String() = strUserList.Split("|")
                        If splstruser.Length > 0 Then
                            ocell.Data = splstruser(0)
                        End If
                        splstruser = Nothing
                        C1BillPay.[Select](nSelectedRow, 4)
                    Else
                        Dim cstyle As C1.Win.C1FlexGrid.CellStyle = Nothing
                        Dim ocell As C1.Win.C1FlexGrid.CellRange = Nothing
                        cstyle = C1BillPay.Styles.Add("BubbleValues" & nSelectedRow)
                        cstyle.ComboList = " "
                        C1BillPay.SetData(nSelectedRow, 4, "")
                        C1BillPay.[Select](nSelectedRow, 4)
                        ocell = C1BillPay.GetCellRange(nSelectedRow, 4, nSelectedRow, 4)
                        ocell.Style = cstyle
                    End If
                    ''Added For MU2 Patient portal implementation on 20130625
                Case "Review Portal Users"
                    If dt.Rows.Count > 0 Then
                        Dim cstyle As C1.Win.C1FlexGrid.CellStyle = Nothing
                        Dim ocell As C1.Win.C1FlexGrid.CellRange = Nothing
                        cstyle = C1PortalUsers.Styles.Add("BubbleValues" & nSelectedRow)
                        cstyle.ComboList = strUserList
                        C1PortalUsers.SetData(nSelectedRow, 4, "")
                        ocell = C1PortalUsers.GetCellRange(nSelectedRow, 4, nSelectedRow, 4)
                        ocell.Style = cstyle
                        Dim splstruser As String() = strUserList.Split("|")
                        If splstruser.Length > 0 Then
                            ocell.Data = splstruser(0)
                        End If
                        splstruser = Nothing
                        C1PortalUsers.[Select](nSelectedRow, 4)
                    Else
                        Dim cstyle As C1.Win.C1FlexGrid.CellStyle = Nothing
                        Dim ocell As C1.Win.C1FlexGrid.CellRange = Nothing
                        cstyle = C1PortalUsers.Styles.Add("BubbleValues" & nSelectedRow)
                        cstyle.ComboList = " "
                        C1PortalUsers.SetData(nSelectedRow, 4, "")
                        C1PortalUsers.[Select](nSelectedRow, 4)
                        ocell = C1PortalUsers.GetCellRange(nSelectedRow, 4, nSelectedRow, 4)
                        ocell.Style = cstyle
                    End If
                Case "Online Patient Form"
                    If dt.Rows.Count > 0 Then
                        Dim cstyle As C1.Win.C1FlexGrid.CellStyle = Nothing
                        Dim ocell As C1.Win.C1FlexGrid.CellRange = Nothing
                        cstyle = C1PatientFormUsers.Styles.Add("BubbleValues" & nSelectedRow)
                        cstyle.ComboList = strUserList
                        C1PatientFormUsers.SetData(nSelectedRow, 4, "")
                        ocell = C1PatientFormUsers.GetCellRange(nSelectedRow, 4, nSelectedRow, 4)
                        ocell.Style = cstyle
                        Dim splstruser As String() = strUserList.Split("|")
                        If splstruser.Length > 0 Then
                            ocell.Data = splstruser(0)
                        End If
                        splstruser = Nothing
                        C1PatientFormUsers.[Select](nSelectedRow, 4)
                    Else
                        Dim cstyle As C1.Win.C1FlexGrid.CellStyle = Nothing
                        Dim ocell As C1.Win.C1FlexGrid.CellRange = Nothing
                        cstyle = C1PatientFormUsers.Styles.Add("BubbleValues" & nSelectedRow)
                        cstyle.ComboList = " "
                        C1PatientFormUsers.SetData(nSelectedRow, 4, "")
                        C1PatientFormUsers.[Select](nSelectedRow, 4)
                        ocell = C1PatientFormUsers.GetCellRange(nSelectedRow, 4, nSelectedRow, 4)
                        ocell.Style = cstyle
                    End If
                    ''End
            End Select

            Dim ToItem As gloGeneralItem.gloItem
            _ToList = New gloGeneralItem.gloItems
            For i As Int16 = 0 To dt.Rows.Count - 1
                ToItem = New gloGeneralItem.gloItem()
                ToItem.ID = dt.Rows(i)("nUserID")
                ToItem.Description = dt.Rows(i)("Description")
                _ToList.Add(ToItem)
                ToItem = Nothing
            Next
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub olistUsers_ItemClosedClick(ByVal sender As Object, ByVal e As EventArgs)
        If Not IsNothing(ofrmList) Then
            ofrmList.Close()
        Else
            ofrmList = New frmUserList
            ofrmList.Close()
            ofrmList.Dispose()
            ofrmList = Nothing
        End If
    End Sub

    Private Sub C1RxRenewal_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles C1RxRenewal.Click
        Dim r As Integer = C1RxRenewal.RowSel
        Dim ostyle As C1.Win.C1FlexGrid.CellStyle
        nSelectedRow = r
        Dim strUserList As String = ""

        If r >= 0 Then
            If C1RxRenewal.ColSel = 5 Then

                If Not IsNothing(C1RxRenewal.GetCellStyle(r, 4)) Then
                    ostyle = C1RxRenewal.GetCellStyle(r, 4)
                    strUserList = ostyle.ComboList
                Else
                    If Not IsNothing(C1RxRenewal.GetData(r, 4)) Then
                        If C1RxRenewal.GetData(r, 4) <> "" Then
                            strUserList = C1RxRenewal.GetData(r, 4)
                        End If
                    End If
                End If

                If Trim(strUserList) <> "" Then
                    Dim dtprv As New DataTable
                    Dim nUserID As New DataColumn("nUserID")
                    Dim Description As New DataColumn("Description")
                    dtprv.Columns.Add(nUserID)
                    dtprv.Columns.Add(Description)

                    Dim splstruser As String() = strUserList.Split("|")

                    For i As Int16 = 0 To splstruser.Length - 1
                        Dim drTemp As DataRow = dtprv.NewRow()
                        Dim _dr() As DataRow
                        _dr = dtUser.[Select]("sLoginName = '" & Convert.ToString(splstruser(i).ToString()).Replace("'", "''") & "'")
                        If Not IsNothing(_dr) And _dr.Length > 0 Then
                            drTemp("nUserID") = Convert.ToInt64(_dr(0).Item("nUserID"))
                        End If
                        _dr = Nothing
                        drTemp("Description") = splstruser(i).ToString()
                        dtprv.Rows.Add(drTemp)
                    Next

                    If dtprv.Rows.Count > 0 Then
                        Dim ToItemp As gloGeneralItem.gloItem
                        _ToList = New gloGeneralItem.gloItems
                        For i As Int16 = 0 To dtprv.Rows.Count - 1
                            ToItemp = New gloGeneralItem.gloItem()
                            ToItemp.ID = dtprv.Rows(i)("nUserID")
                            ToItemp.Description = dtprv.Rows(i)("Description")
                            _ToList.Add(ToItemp)
                            ToItemp = Nothing
                        Next
                    End If
                Else
                    If IsNothing(_ToList) = False Then
                        _ToList.Clear()
                    End If
                End If
            End If
        End If

        Dim strUsers As String = ""

        With C1RxRenewal
            If r >= 0 Then
                If .ColSel = 5 Then
                    oListUsers = New gloListControl.gloListControl(gloEMRAdmin.mdlGeneral.GetConnectionString(), gloListControl.gloListControlType.Users, True, Me.Width)
                    oListUsers.ControlHeader = "Users"
                    AddHandler oListUsers.ItemSelectedClick, AddressOf olistUsers_ItemSelectedClick
                    AddHandler oListUsers.ItemClosedClick, AddressOf olistUsers_ItemClosedClick
                    'To Select already Added Users.
                    If IsNothing(_ToList) = False Then
                        For i As Integer = 0 To _ToList.Count - 1
                            oListUsers.SelectedItems.Add(_ToList(i))
                        Next
                    End If
                    ofrmList.Controls.Add(oListUsers)
                    oListUsers.Dock = DockStyle.Fill
                    oListUsers.BringToFront()
                    oListUsers.ShowHeaderPanel(False)
                    oListUsers.OpenControl()
                    ofrmList.Text = "Users"
                    ofrmList.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedSingle
                    ofrmList.ShowDialog()
                End If
            End If

        End With
    End Sub

    Private Sub C1BillPay_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles C1BillPay.Click
        Dim r As Integer = C1BillPay.RowSel
        Dim ostyle As C1.Win.C1FlexGrid.CellStyle
        nSelectedRow = r
        Dim strUserList As String = ""

        If r >= 0 Then
            If C1BillPay.ColSel = 5 Then

                If Not IsNothing(C1BillPay.GetCellStyle(r, 4)) Then
                    ostyle = C1BillPay.GetCellStyle(r, 4)
                    strUserList = ostyle.ComboList
                Else
                    If Not IsNothing(C1BillPay.GetData(r, 4)) Then
                        If C1BillPay.GetData(r, 4) <> "" Then
                            strUserList = C1BillPay.GetData(r, 4)
                        End If
                    End If
                End If

                If Trim(strUserList) <> "" Then
                    Dim dtprv As New DataTable
                    Dim nUserID As New DataColumn("nUserID")
                    Dim Description As New DataColumn("Description")
                    dtprv.Columns.Add(nUserID)
                    dtprv.Columns.Add(Description)
                    Dim splstruser As String() = strUserList.Split("|")
                    For i As Int16 = 0 To splstruser.Length - 1
                        Dim drTemp As DataRow = dtprv.NewRow()
                        Dim _dr() As DataRow
                        _dr = dtUser.[Select]("sLoginName = '" & Convert.ToString(splstruser(i).ToString()).Replace("'", "''") & "'")
                        If Not IsNothing(_dr) And _dr.Length > 0 Then
                            drTemp("nUserID") = Convert.ToInt64(_dr(0).Item("nUserID"))
                        End If
                        _dr = Nothing
                        drTemp("Description") = splstruser(i).ToString()
                        dtprv.Rows.Add(drTemp)
                    Next
                    If dtprv.Rows.Count > 0 Then
                        Dim ToItemp As gloGeneralItem.gloItem
                        _ToList = New gloGeneralItem.gloItems
                        For i As Int16 = 0 To dtprv.Rows.Count - 1
                            ToItemp = New gloGeneralItem.gloItem()
                            ToItemp.ID = dtprv.Rows(i)("nUserID")
                            ToItemp.Description = dtprv.Rows(i)("Description")
                            _ToList.Add(ToItemp)
                            ToItemp = Nothing
                        Next
                    End If
                Else
                    If IsNothing(_ToList) = False Then
                        _ToList.Clear()
                    End If
                End If
            End If
        End If

        Dim strUsers As String = ""

        With C1BillPay
            If r >= 0 Then
                If .ColSel = 5 Then
                    oListUsers = New gloListControl.gloListControl(gloEMRAdmin.mdlGeneral.GetConnectionString(), gloListControl.gloListControlType.Users, True, Me.Width)
                    oListUsers.ControlHeader = "Users"
                    AddHandler oListUsers.ItemSelectedClick, AddressOf olistUsers_ItemSelectedClick
                    AddHandler oListUsers.ItemClosedClick, AddressOf olistUsers_ItemClosedClick
                    'To Select already Added Users.
                    If IsNothing(_ToList) = False Then
                        For i As Integer = 0 To _ToList.Count - 1
                            oListUsers.SelectedItems.Add(_ToList(i))
                        Next
                    End If
                    ofrmList.Controls.Add(oListUsers)
                    oListUsers.Dock = DockStyle.Fill
                    oListUsers.BringToFront()
                    oListUsers.ShowHeaderPanel(False)
                    oListUsers.OpenControl()
                    ofrmList.Text = "Users"
                    ofrmList.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedSingle
                    ofrmList.ShowDialog()
                End If
            End If

        End With
    End Sub
    ''Added for MU2 Patient portal implementation on 20130626
    Private Sub C1PortalUsers_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles C1PortalUsers.Click

        Dim r As Integer = C1PortalUsers.RowSel
        Dim ostyle As C1.Win.C1FlexGrid.CellStyle
        nSelectedRow = r
        Dim strUserList As String = ""

        If r >= 0 Then
            If C1PortalUsers.ColSel = 5 Then

                If Not IsNothing(C1PortalUsers.GetCellStyle(r, 4)) Then
                    ostyle = C1PortalUsers.GetCellStyle(r, 4)
                    strUserList = ostyle.ComboList
                Else
                    If Not IsNothing(C1PortalUsers.GetData(r, 4)) Then
                        If C1PortalUsers.GetData(r, 4) <> "" Then
                            strUserList = C1PortalUsers.GetData(r, 4)
                        End If
                    End If
                End If

                If Trim(strUserList) <> "" Then
                    Dim dtprv As New DataTable
                    Dim nUserID As New DataColumn("nUserID")
                    Dim Description As New DataColumn("Description")
                    dtprv.Columns.Add(nUserID)
                    dtprv.Columns.Add(Description)

                    Dim splstruser As String() = strUserList.Split("|")

                    For i As Int16 = 0 To splstruser.Length - 1
                        Dim drTemp As DataRow = dtprv.NewRow()
                        'drTemp("nUserID") = 216512308151873336

                        Dim _dr() As DataRow
                        _dr = dtUser.[Select]("sLoginName = '" & Convert.ToString(splstruser(i).ToString()).Replace("'", "''") & "'")
                        If Not IsNothing(_dr) And _dr.Length > 0 Then
                            drTemp("nUserID") = Convert.ToInt64(_dr(0).Item("nUserID"))
                        End If
                        _dr = Nothing
                        drTemp("Description") = splstruser(i).ToString()
                        dtprv.Rows.Add(drTemp)
                    Next

                    If dtprv.Rows.Count > 0 Then
                        Dim ToItemp As gloGeneralItem.gloItem
                        _ToList = New gloGeneralItem.gloItems
                        For i As Int16 = 0 To dtprv.Rows.Count - 1
                            ToItemp = New gloGeneralItem.gloItem()
                            ToItemp.ID = dtprv.Rows(i)("nUserID")
                            ToItemp.Description = dtprv.Rows(i)("Description")
                            _ToList.Add(ToItemp)
                            ToItemp = Nothing
                        Next
                    End If
                Else
                    If IsNothing(_ToList) = False Then
                        _ToList.Clear()
                    End If
                End If
            End If
        End If

        Dim strUsers As String = ""

        With C1PortalUsers
            If r >= 0 Then
                If .ColSel = 5 Then
                    oListUsers = New gloListControl.gloListControl(gloEMRAdmin.mdlGeneral.GetConnectionString(), gloListControl.gloListControlType.Users, True, Me.Width)
                    oListUsers.ControlHeader = "Users"
                    AddHandler oListUsers.ItemSelectedClick, AddressOf olistUsers_ItemSelectedClick
                    AddHandler oListUsers.ItemClosedClick, AddressOf olistUsers_ItemClosedClick
                    'To Select already Added Users.
                    If IsNothing(_ToList) = False Then
                        For i As Integer = 0 To _ToList.Count - 1
                            oListUsers.SelectedItems.Add(_ToList(i))
                        Next
                    End If
                    ofrmList.Controls.Add(oListUsers)
                    oListUsers.Dock = DockStyle.Fill
                    oListUsers.BringToFront()
                    oListUsers.ShowHeaderPanel(False)
                    oListUsers.OpenControl()
                    ofrmList.Text = "Users"
                    ofrmList.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedSingle
                    ofrmList.ShowDialog()
                End If
            End If
        End With
    End Sub

    Private Sub btnPortalDown_Click(sender As System.Object, e As System.EventArgs) Handles btnPortalDown.Click
        Try
            If C1PortalUsers.Rows.Count > 1 Then
                If C1PortalUsers.RowSel = C1PortalUsers.Rows.Count - 1 Then
                    C1PortalUsers.RowSel = C1PortalUsers.Rows.Count - 1
                Else
                    C1PortalUsers.Rows(C1PortalUsers.RowSel).Move(C1PortalUsers.RowSel + 1)
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        End Try
    End Sub

    Private Sub btnPortalUp_Click(sender As System.Object, e As System.EventArgs) Handles btnPortalUp.Click
        Try
            If C1PortalUsers.Rows.Count > 1 Then
                If C1PortalUsers.RowSel = 1 Then
                    C1PortalUsers.RowSel = 1
                Else
                    C1PortalUsers.Rows(C1PortalUsers.RowSel).Move(C1PortalUsers.RowSel - 1)
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        End Try
    End Sub

    Private Function IspatientPortalEnabled() As Boolean
        Dim IsPortalEnabled As Boolean = False
        Try
            Dim objSettings As New clsSettings
            Dim isPortalEnable As Object = Nothing
            objSettings.GetSetting("PatientPortalEnabled", gnLoginID, gnClinicID, isPortalEnable)
            If isPortalEnable IsNot Nothing Then
                If Convert.ToString(isPortalEnable).ToLower() = "true" Then
                    IsPortalEnabled = True
                End If
            End If
            objSettings = Nothing
        Catch ex As Exception
            MessageBox.Show(ex.ToString(), gstrMessageBoxCaption)
            Return IsPortalEnabled
        End Try
        Return IsPortalEnabled
    End Function

    Private Function IsAutoCompleteTaskEnabled() As Boolean
        Dim IsAutoCompleteEnabled As Boolean = False
        Try
            Dim objSettings As New clsSettings
            Dim isEnable As Object = Nothing
            objSettings.GetSetting("PatientPortalAutoCompleteTask", gnLoginID, gnClinicID, isEnable)
            If isEnable IsNot Nothing Then
                If Convert.ToString(isEnable).ToLower() = "true" Then
                    IsAutoCompleteEnabled = True
                End If
            End If
            objSettings = Nothing
        Catch ex As Exception
            MessageBox.Show(ex.ToString(), gstrMessageBoxCaption)
            Return IsAutoCompleteEnabled
        End Try
        Return IsAutoCompleteEnabled
    End Function

    Private Function IsPFAutoCompleteTaskEnabled() As Boolean
        Dim IsPFAutoCompleteEnabled As Boolean = False
        Try
            Dim objSettings As New clsSettings
            Dim isEnable As Object = Nothing
            objSettings.GetSetting("PatientPortalPFAutoCompleteTask", gnLoginID, gnClinicID, isEnable)
            If isEnable IsNot Nothing Then
                If Convert.ToString(isEnable).ToLower() = "true" Then
                    IsPFAutoCompleteEnabled = True
                End If
            End If
            objSettings = Nothing
        Catch ex As Exception
            MessageBox.Show(ex.ToString(), gstrMessageBoxCaption)
            Return IsPFAutoCompleteEnabled
        End Try
        Return IsPFAutoCompleteEnabled
    End Function

    Private Function IsPatientFormTaskNotificationEnabled() As Boolean
        Dim IsPFTaskNotificationEnabled As Boolean = False
        Try
            Dim objSettings As New clsSettings
            Dim isEnable As Object = Nothing
            objSettings.GetSetting("PatientPortalPFTaskNotification", gnLoginID, gnClinicID, isEnable)
            If isEnable IsNot Nothing Then
                If Convert.ToString(isEnable).ToLower() = "true" Then
                    IsPFTaskNotificationEnabled = True
                End If
            End If
            objSettings = Nothing
        Catch ex As Exception
            MessageBox.Show(ex.ToString(), gstrMessageBoxCaption)
            Return IsPFTaskNotificationEnabled
        End Try
        Return IsPFTaskNotificationEnabled
    End Function
    Public Function UpdateSettings(ByVal sSettingName As String, ByVal sSettingValue As String) As Boolean
        Dim obj As New clsSettings
        Try
            obj.AddEMSettings(sSettingName, sSettingValue)
        Catch ex As Exception
            MessageBox.Show(ex.ToString(), gstrMessageBoxCaption)
        Finally
            obj = Nothing
        End Try
    End Function

    Private Sub btnSearchApptUser_Click(sender As System.Object, e As System.EventArgs) Handles btnSearchApptUser.Click
        Try
            ofrmDefaultList = New frmUserList
            oListUsers = New gloListControl.gloListControl(gloEMRAdmin.mdlGeneral.GetConnectionString(), gloListControl.gloListControlType.Users, True, Me.Width)
            oListUsers.ControlHeader = "Users"
            AddHandler oListUsers.ItemSelectedClick, AddressOf olistDefaultUsers_ItemSelectedClick
            AddHandler oListUsers.ItemClosedClick, AddressOf olistDefaultUsers_ItemClosedClick

            ' ''To Select already Added Users.
            If IsNothing(_ToApptDefaultUserList) = False Then
                For i As Integer = 0 To _ToApptDefaultUserList.Count - 1
                    oListUsers.SelectedItems.Add(_ToApptDefaultUserList(i))
                Next
            End If
            '

            ofrmDefaultList.Controls.Add(oListUsers)
            oListUsers.Dock = DockStyle.Fill
            oListUsers.BringToFront()
            oListUsers.ShowHeaderPanel(False)
            oListUsers.OpenControl()
            ofrmDefaultList.Text = "Users"
            ofrmDefaultList.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedSingle
            ofrmDefaultList.ShowDialog()
            If IsNothing(ofrmDefaultList) = False Then
                ofrmDefaultList.Dispose()
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnClearApptUsers_Click(sender As System.Object, e As System.EventArgs) Handles btnClearApptUsers.Click
        Try

            Dim dtusers As DataTable
            dtusers = CType(cmbApptDefaultUser.DataSource, DataTable)
            If Not IsNothing(dtusers) Then
                If dtusers.Rows.Count > 0 Then
                    Dim i As Integer
                    i = cmbApptDefaultUser.SelectedIndex
                    dtusers.Rows.RemoveAt(cmbApptDefaultUser.SelectedIndex)
                    dtusers.AcceptChanges()
                    cmbApptDefaultUser.DataSource = dtusers
                    _ToApptDefaultUserList = New gloGeneralItem.gloItems
                    Dim ToItem As gloGeneralItem.gloItem
                    For j As Int16 = 0 To dtusers.Rows.Count - 1
                        ToItem = New gloGeneralItem.gloItem()
                        ToItem.ID = dtusers.Rows(j)("nUserID")
                        ToItem.Description = dtusers.Rows(j)("Description")
                        _ToApptDefaultUserList.Add(ToItem)
                        ToItem = Nothing
                    Next
                    cmbApptDefaultUser.Refresh()
                    If i > 0 Then
                        cmbApptDefaultUser.SelectedIndex = 0
                    End If
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
    End Sub

    Private Sub olistDefaultUsers_ItemSelectedClick(ByVal sender As Object, ByVal e As EventArgs)
        'cmb_To.Items.Clear(); 
        Dim dtUsers As New DataTable()
        Dim dcnIntuitStaffMappingDetailID As New DataColumn("IntuitMessageMappingDetailID")
        Dim dcnIntuitStaffMappingID As New DataColumn("IntuitMessageMappingID")
        Dim dcId As New DataColumn("nUserID")
        Dim dcDescription As New DataColumn("Description")
        dtUsers.Columns.Add(dcnIntuitStaffMappingDetailID)
        dtUsers.Columns.Add(dcnIntuitStaffMappingID)
        dtUsers.Columns.Add(dcId)
        dtUsers.Columns.Add(dcDescription)

        Select Case Tb_Messages.SelectedTab.Tag.ToString()
            Case "Appointment Request"
                _ToApptDefaultUserList = New gloGeneralItem.gloItems
                Dim ToItem As gloGeneralItem.gloItem

                If oListUsers.SelectedItems.Count > 0 Then
                    For i As Int16 = 0 To oListUsers.SelectedItems.Count - 1
                        Dim drTemp As DataRow = dtUsers.NewRow()
                        drTemp("IntuitMessageMappingDetailID") = 0
                        drTemp("IntuitMessageMappingID") = 0
                        drTemp("nUserID") = oListUsers.SelectedItems(i).ID
                        drTemp("Description") = oListUsers.SelectedItems(i).Description
                        dtUsers.Rows.Add(drTemp)

                        '
                        ToItem = New gloGeneralItem.gloItem()

                        ToItem.ID = oListUsers.SelectedItems(i).ID
                        ToItem.Description = oListUsers.SelectedItems(i).Description

                        _ToApptDefaultUserList.Add(ToItem)
                        '
                        ToItem = Nothing
                    Next
                End If
            Case "Rx Renewal"
                _ToRxDefaultUserList = New gloGeneralItem.gloItems
                Dim ToItem As gloGeneralItem.gloItem

                If oListUsers.SelectedItems.Count > 0 Then
                    For i As Int16 = 0 To oListUsers.SelectedItems.Count - 1
                        Dim drTemp As DataRow = dtUsers.NewRow()
                        drTemp("IntuitMessageMappingDetailID") = 0
                        drTemp("IntuitMessageMappingID") = 0
                        drTemp("nUserID") = oListUsers.SelectedItems(i).ID
                        drTemp("Description") = oListUsers.SelectedItems(i).Description
                        dtUsers.Rows.Add(drTemp)

                        '
                        ToItem = New gloGeneralItem.gloItem()

                        ToItem.ID = oListUsers.SelectedItems(i).ID
                        ToItem.Description = oListUsers.SelectedItems(i).Description

                        _ToRxDefaultUserList.Add(ToItem)
                        '
                        ToItem = Nothing
                    Next
                End If
            Case "Online Bill Pay"
                _ToBillDefaultUserList = New gloGeneralItem.gloItems
                Dim ToItem As gloGeneralItem.gloItem

                If oListUsers.SelectedItems.Count > 0 Then
                    For i As Int16 = 0 To oListUsers.SelectedItems.Count - 1
                        Dim drTemp As DataRow = dtUsers.NewRow()
                        drTemp("IntuitMessageMappingDetailID") = 0
                        drTemp("IntuitMessageMappingID") = 0
                        drTemp("nUserID") = oListUsers.SelectedItems(i).ID
                        drTemp("Description") = oListUsers.SelectedItems(i).Description
                        dtUsers.Rows.Add(drTemp)

                        '
                        ToItem = New gloGeneralItem.gloItem()

                        ToItem.ID = oListUsers.SelectedItems(i).ID
                        ToItem.Description = oListUsers.SelectedItems(i).Description

                        _ToBillDefaultUserList.Add(ToItem)
                        '
                        ToItem = Nothing
                    Next
                End If
            Case "Review Portal Users"
                _ToPortalDefaultUserList = New gloGeneralItem.gloItems
                Dim ToItem As gloGeneralItem.gloItem

                If oListUsers.SelectedItems.Count > 0 Then
                    For i As Int16 = 0 To oListUsers.SelectedItems.Count - 1
                        Dim drTemp As DataRow = dtUsers.NewRow()
                        drTemp("IntuitMessageMappingDetailID") = 0
                        drTemp("IntuitMessageMappingID") = 0
                        drTemp("nUserID") = oListUsers.SelectedItems(i).ID
                        drTemp("Description") = oListUsers.SelectedItems(i).Description
                        dtUsers.Rows.Add(drTemp)

                        '
                        ToItem = New gloGeneralItem.gloItem()

                        ToItem.ID = oListUsers.SelectedItems(i).ID
                        ToItem.Description = oListUsers.SelectedItems(i).Description

                        _ToPortalDefaultUserList.Add(ToItem)
                        '
                        ToItem = Nothing
                    Next
                End If
            Case "Online Patient Form"
                _ToPatientFormDefaultUserList = New gloGeneralItem.gloItems
                Dim ToItem As gloGeneralItem.gloItem

                If oListUsers.SelectedItems.Count > 0 Then
                    For i As Int16 = 0 To oListUsers.SelectedItems.Count - 1
                        Dim drTemp As DataRow = dtUsers.NewRow()
                        drTemp("IntuitMessageMappingDetailID") = 0
                        drTemp("IntuitMessageMappingID") = 0
                        drTemp("nUserID") = oListUsers.SelectedItems(i).ID
                        drTemp("Description") = oListUsers.SelectedItems(i).Description
                        dtUsers.Rows.Add(drTemp)

                        '
                        ToItem = New gloGeneralItem.gloItem()

                        ToItem.ID = oListUsers.SelectedItems(i).ID
                        ToItem.Description = oListUsers.SelectedItems(i).Description

                        _ToPatientFormDefaultUserList.Add(ToItem)
                        '
                        ToItem = Nothing
                    Next
                End If
        End Select



        '_ToDefaultUserList = New gloGeneralItem.gloItems
        'Dim ToItem As gloGeneralItem.gloItem

        'If oListUsers.SelectedItems.Count > 0 Then
        '    For i As Int16 = 0 To oListUsers.SelectedItems.Count - 1
        '        Dim drTemp As DataRow = dtUsers.NewRow()
        '        drTemp("IntuitMessageMappingDetailID") = 0
        '        drTemp("IntuitMessageMappingID") = 0
        '        drTemp("nUserID") = oListUsers.SelectedItems(i).ID
        '        drTemp("Description") = oListUsers.SelectedItems(i).Description
        '        dtUsers.Rows.Add(drTemp)

        '        '
        '        ToItem = New gloGeneralItem.gloItem()

        '        ToItem.ID = oListUsers.SelectedItems(i).ID
        '        ToItem.Description = oListUsers.SelectedItems(i).Description

        '        _ToDefaultUserList.Add(ToItem)
        '        '
        '        ToItem = Nothing
        '    Next
        'End If
        RefreshDefaultUsers(dtUsers)
        ofrmDefaultList.Close()
    End Sub

    Private Sub olistDefaultUsers_ItemClosedClick(ByVal sender As Object, ByVal e As EventArgs)
        If Not IsNothing(ofrmDefaultList) Then
            ofrmDefaultList.Close()
        Else
            ofrmDefaultList = New frmUserList
            ofrmDefaultList.Close()
            ofrmDefaultList.Dispose()
            ofrmDefaultList = Nothing
        End If
    End Sub
    Public Sub RefreshDefaultUsers(ByVal dt As DataTable)
        Try
            Select Case Tb_Messages.SelectedTab.Tag.ToString()
                Case "Appointment Request"
                    cmbApptDefaultUser.DataSource = dt
                    cmbApptDefaultUser.ValueMember = dt.Columns("nUserID").ColumnName
                    cmbApptDefaultUser.DisplayMember = dt.Columns("Description").ColumnName

                    Dim ToItem As gloGeneralItem.gloItem
                    _ToApptDefaultUserList = New gloGeneralItem.gloItems
                    For i As Int16 = 0 To dt.Rows.Count - 1
                        ToItem = New gloGeneralItem.gloItem()
                        ToItem.ID = dt.Rows(i)("nUserID")
                        ToItem.Description = dt.Rows(i)("Description")
                        _ToApptDefaultUserList.Add(ToItem)
                        ToItem = Nothing
                    Next
                Case "Rx Renewal"
                    cmbRxDefaultUser.DataSource = dt
                    cmbRxDefaultUser.ValueMember = dt.Columns("nUserID").ColumnName
                    cmbRxDefaultUser.DisplayMember = dt.Columns("Description").ColumnName

                    Dim ToItem As gloGeneralItem.gloItem
                    _ToRxDefaultUserList = New gloGeneralItem.gloItems
                    For i As Int16 = 0 To dt.Rows.Count - 1
                        ToItem = New gloGeneralItem.gloItem()
                        ToItem.ID = dt.Rows(i)("nUserID")
                        ToItem.Description = dt.Rows(i)("Description")
                        _ToRxDefaultUserList.Add(ToItem)
                        ToItem = Nothing
                    Next
                Case "Online Bill Pay"
                    cmbBillPayDefaultUser.DataSource = dt
                    cmbBillPayDefaultUser.ValueMember = dt.Columns("nUserID").ColumnName
                    cmbBillPayDefaultUser.DisplayMember = dt.Columns("Description").ColumnName

                    Dim ToItem As gloGeneralItem.gloItem
                    _ToBillDefaultUserList = New gloGeneralItem.gloItems
                    For i As Int16 = 0 To dt.Rows.Count - 1
                        ToItem = New gloGeneralItem.gloItem()
                        ToItem.ID = dt.Rows(i)("nUserID")
                        ToItem.Description = dt.Rows(i)("Description")
                        _ToBillDefaultUserList.Add(ToItem)
                        ToItem = Nothing
                    Next
                Case "Review Portal Users"
                    cmbPortalDefaultUser.DataSource = dt
                    cmbPortalDefaultUser.ValueMember = dt.Columns("nUserID").ColumnName
                    cmbPortalDefaultUser.DisplayMember = dt.Columns("Description").ColumnName

                    Dim ToItem As gloGeneralItem.gloItem
                    _ToPortalDefaultUserList = New gloGeneralItem.gloItems
                    For i As Int16 = 0 To dt.Rows.Count - 1
                        ToItem = New gloGeneralItem.gloItem()
                        ToItem.ID = dt.Rows(i)("nUserID")
                        ToItem.Description = dt.Rows(i)("Description")
                        _ToPortalDefaultUserList.Add(ToItem)
                        ToItem = Nothing
                    Next
                Case "Online Patient Form"
                    cmbPatientFormDefaultUser.DataSource = dt
                    cmbPatientFormDefaultUser.ValueMember = dt.Columns("nUserID").ColumnName
                    cmbPatientFormDefaultUser.DisplayMember = dt.Columns("Description").ColumnName

                    Dim ToItem As gloGeneralItem.gloItem
                    _ToPatientFormDefaultUserList = New gloGeneralItem.gloItems
                    For i As Int16 = 0 To dt.Rows.Count - 1
                        ToItem = New gloGeneralItem.gloItem()
                        ToItem.ID = dt.Rows(i)("nUserID")
                        ToItem.Description = dt.Rows(i)("Description")
                        _ToPatientFormDefaultUserList.Add(ToItem)
                        ToItem = Nothing
                    Next
            End Select


            'Dim ToItem As gloGeneralItem.gloItem
            '_ToDefaultUserList = New gloGeneralItem.gloItems
            'For i As Int16 = 0 To dt.Rows.Count - 1
            '    ToItem = New gloGeneralItem.gloItem()
            '    ToItem.ID = dt.Rows(i)("nUserID")
            '    ToItem.Description = dt.Rows(i)("Description")
            '    _ToDefaultUserList.Add(ToItem)
            '    ToItem = Nothing
            'Next
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnSearchRxUser_Click(sender As System.Object, e As System.EventArgs) Handles btnSearchRxUser.Click
        Try
            ofrmDefaultList = New frmUserList
            oListUsers = New gloListControl.gloListControl(gloEMRAdmin.mdlGeneral.GetConnectionString(), gloListControl.gloListControlType.Users, True, Me.Width)
            oListUsers.ControlHeader = "Users"
            AddHandler oListUsers.ItemSelectedClick, AddressOf olistDefaultUsers_ItemSelectedClick
            AddHandler oListUsers.ItemClosedClick, AddressOf olistDefaultUsers_ItemClosedClick

            ' ''To Select already Added Users.
            If IsNothing(_ToRxDefaultUserList) = False Then
                For i As Integer = 0 To _ToRxDefaultUserList.Count - 1
                    oListUsers.SelectedItems.Add(_ToRxDefaultUserList(i))
                Next
            End If
            '

            ofrmDefaultList.Controls.Add(oListUsers)
            oListUsers.Dock = DockStyle.Fill
            oListUsers.BringToFront()
            oListUsers.ShowHeaderPanel(False)
            oListUsers.OpenControl()
            ofrmDefaultList.Text = "Users"
            ofrmDefaultList.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedSingle
            ofrmDefaultList.ShowDialog()
            If IsNothing(ofrmDefaultList) = False Then
                ofrmDefaultList.Dispose()
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnClearRxUsers_Click(sender As System.Object, e As System.EventArgs) Handles btnClearRxUsers.Click
        Try

            Dim dtusers As DataTable
            dtusers = CType(cmbRxDefaultUser.DataSource, DataTable)
            If Not IsNothing(dtusers) Then
                If dtusers.Rows.Count > 0 Then
                    Dim i As Integer
                    i = cmbRxDefaultUser.SelectedIndex
                    dtusers.Rows.RemoveAt(cmbRxDefaultUser.SelectedIndex)
                    dtusers.AcceptChanges()
                    cmbRxDefaultUser.DataSource = dtusers
                    _ToRxDefaultUserList = New gloGeneralItem.gloItems
                    Dim ToItem As gloGeneralItem.gloItem
                    For j As Int16 = 0 To dtusers.Rows.Count - 1
                        ToItem = New gloGeneralItem.gloItem()
                        ToItem.ID = dtusers.Rows(j)("nUserID")
                        ToItem.Description = dtusers.Rows(j)("Description")
                        _ToRxDefaultUserList.Add(ToItem)
                        ToItem = Nothing
                    Next
                    cmbRxDefaultUser.Refresh()
                    If i > 0 Then
                        cmbRxDefaultUser.SelectedIndex = 0
                    End If
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
    End Sub

    Private Sub btnSearchBillPayUser_Click(sender As System.Object, e As System.EventArgs) Handles btnSearchBillPayUser.Click
        Try
            ofrmDefaultList = New frmUserList
            oListUsers = New gloListControl.gloListControl(gloEMRAdmin.mdlGeneral.GetConnectionString(), gloListControl.gloListControlType.Users, True, Me.Width)
            oListUsers.ControlHeader = "Users"
            AddHandler oListUsers.ItemSelectedClick, AddressOf olistDefaultUsers_ItemSelectedClick
            AddHandler oListUsers.ItemClosedClick, AddressOf olistDefaultUsers_ItemClosedClick

            ' ''To Select already Added Users.
            If IsNothing(_ToBillDefaultUserList) = False Then
                For i As Integer = 0 To _ToBillDefaultUserList.Count - 1
                    oListUsers.SelectedItems.Add(_ToBillDefaultUserList(i))
                Next
            End If
            '

            ofrmDefaultList.Controls.Add(oListUsers)
            oListUsers.Dock = DockStyle.Fill
            oListUsers.BringToFront()
            oListUsers.ShowHeaderPanel(False)
            oListUsers.OpenControl()
            ofrmDefaultList.Text = "Users"
            ofrmDefaultList.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedSingle
            ofrmDefaultList.ShowDialog()
            If IsNothing(ofrmDefaultList) = False Then
                ofrmDefaultList.Dispose()
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnClearBillPayUsers_Click(sender As System.Object, e As System.EventArgs) Handles btnClearBillPayUsers.Click
        Try

            Dim dtusers As DataTable
            dtusers = CType(cmbBillPayDefaultUser.DataSource, DataTable)
            If Not IsNothing(dtusers) Then
                If dtusers.Rows.Count > 0 Then
                    Dim i As Integer
                    i = cmbBillPayDefaultUser.SelectedIndex
                    dtusers.Rows.RemoveAt(cmbBillPayDefaultUser.SelectedIndex)
                    dtusers.AcceptChanges()
                    cmbBillPayDefaultUser.DataSource = dtusers
                    _ToBillDefaultUserList = New gloGeneralItem.gloItems
                    Dim ToItem As gloGeneralItem.gloItem
                    For j As Int16 = 0 To dtusers.Rows.Count - 1
                        ToItem = New gloGeneralItem.gloItem()
                        ToItem.ID = dtusers.Rows(j)("nUserID")
                        ToItem.Description = dtusers.Rows(j)("Description")
                        _ToBillDefaultUserList.Add(ToItem)
                        ToItem = Nothing
                    Next
                    cmbBillPayDefaultUser.Refresh()
                    If i > 0 Then
                        cmbBillPayDefaultUser.SelectedIndex = 0
                    End If
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
    End Sub

    Private Sub btnSearchPortalUser_Click(sender As System.Object, e As System.EventArgs) Handles btnSearchPortalUser.Click
        Try
            ofrmDefaultList = New frmUserList
            oListUsers = New gloListControl.gloListControl(gloEMRAdmin.mdlGeneral.GetConnectionString(), gloListControl.gloListControlType.Users, True, Me.Width)
            oListUsers.ControlHeader = "Users"
            AddHandler oListUsers.ItemSelectedClick, AddressOf olistDefaultUsers_ItemSelectedClick
            AddHandler oListUsers.ItemClosedClick, AddressOf olistDefaultUsers_ItemClosedClick

            ' ''To Select already Added Users.
            If IsNothing(_ToPortalDefaultUserList) = False Then
                For i As Integer = 0 To _ToPortalDefaultUserList.Count - 1
                    oListUsers.SelectedItems.Add(_ToPortalDefaultUserList(i))
                Next
            End If
            '

            ofrmDefaultList.Controls.Add(oListUsers)
            oListUsers.Dock = DockStyle.Fill
            oListUsers.BringToFront()
            oListUsers.ShowHeaderPanel(False)
            oListUsers.OpenControl()
            ofrmDefaultList.Text = "Users"
            ofrmDefaultList.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedSingle
            ofrmDefaultList.ShowDialog()
            If IsNothing(ofrmDefaultList) = False Then
                ofrmDefaultList.Dispose()
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnClearPortalUsers_Click(sender As System.Object, e As System.EventArgs) Handles btnClearPortalUsers.Click
        Try

            Dim dtusers As DataTable
            dtusers = CType(cmbPortalDefaultUser.DataSource, DataTable)
            If Not IsNothing(dtusers) Then
                If dtusers.Rows.Count > 0 Then
                    Dim i As Integer
                    i = cmbPortalDefaultUser.SelectedIndex
                    dtusers.Rows.RemoveAt(cmbPortalDefaultUser.SelectedIndex)
                    dtusers.AcceptChanges()
                    cmbPortalDefaultUser.DataSource = dtusers
                    _ToPortalDefaultUserList = New gloGeneralItem.gloItems
                    Dim ToItem As gloGeneralItem.gloItem
                    For j As Int16 = 0 To dtusers.Rows.Count - 1
                        ToItem = New gloGeneralItem.gloItem()
                        ToItem.ID = dtusers.Rows(j)("nUserID")
                        ToItem.Description = dtusers.Rows(j)("Description")
                        _ToPortalDefaultUserList.Add(ToItem)
                        ToItem = Nothing
                    Next
                    cmbPortalDefaultUser.Refresh()
                    If i > 0 Then
                        cmbPortalDefaultUser.SelectedIndex = 0
                    End If
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
    End Sub

    Private Sub btnPatientFormDown_Click(sender As System.Object, e As System.EventArgs) Handles btnPatientFormDown.Click
        Try
            If C1PatientFormUsers.Rows.Count > 1 Then
                If C1PatientFormUsers.RowSel = C1PatientFormUsers.Rows.Count - 1 Then
                    C1PatientFormUsers.RowSel = C1PatientFormUsers.Rows.Count - 1
                Else
                    C1PatientFormUsers.Rows(C1PatientFormUsers.RowSel).Move(C1PatientFormUsers.RowSel + 1)
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        End Try
    End Sub

    Private Sub btnPatientFormUp_Click(sender As System.Object, e As System.EventArgs) Handles btnPatientFormUp.Click
        Try
            If C1PatientFormUsers.Rows.Count > 1 Then
                If C1PatientFormUsers.RowSel = 1 Then
                    C1PatientFormUsers.RowSel = 1
                Else
                    C1PatientFormUsers.Rows(C1PatientFormUsers.RowSel).Move(C1PatientFormUsers.RowSel - 1)
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        End Try
    End Sub

    Private Sub btnSearchPatientFormUser_Click(sender As System.Object, e As System.EventArgs) Handles btnSearchPatientFormUser.Click
        Try
            ofrmDefaultList = New frmUserList
            oListUsers = New gloListControl.gloListControl(gloEMRAdmin.mdlGeneral.GetConnectionString(), gloListControl.gloListControlType.Users, True, Me.Width)
            oListUsers.ControlHeader = "Users"
            AddHandler oListUsers.ItemSelectedClick, AddressOf olistDefaultUsers_ItemSelectedClick
            AddHandler oListUsers.ItemClosedClick, AddressOf olistDefaultUsers_ItemClosedClick

            ' ''To Select already Added Users.
            If IsNothing(_ToPatientFormDefaultUserList) = False Then
                For i As Integer = 0 To _ToPatientFormDefaultUserList.Count - 1
                    oListUsers.SelectedItems.Add(_ToPatientFormDefaultUserList(i))
                Next
            End If
            '

            ofrmDefaultList.Controls.Add(oListUsers)
            oListUsers.Dock = DockStyle.Fill
            oListUsers.BringToFront()
            oListUsers.ShowHeaderPanel(False)
            oListUsers.OpenControl()
            ofrmDefaultList.Text = "Users"
            ofrmDefaultList.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedSingle
            ofrmDefaultList.ShowDialog()
            If IsNothing(ofrmDefaultList) = False Then
                ofrmDefaultList.Dispose()
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnClearPatientFormUsers_Click(sender As System.Object, e As System.EventArgs) Handles btnClearPatientFormUsers.Click
        Try

            Dim dtusers As DataTable
            dtusers = CType(cmbPatientFormDefaultUser.DataSource, DataTable)
            If Not IsNothing(dtusers) Then
                If dtusers.Rows.Count > 0 Then
                    Dim i As Integer
                    i = cmbPatientFormDefaultUser.SelectedIndex
                    dtusers.Rows.RemoveAt(cmbPatientFormDefaultUser.SelectedIndex)
                    dtusers.AcceptChanges()
                    cmbPatientFormDefaultUser.DataSource = dtusers
                    _ToPatientFormDefaultUserList = New gloGeneralItem.gloItems
                    Dim ToItem As gloGeneralItem.gloItem
                    For j As Int16 = 0 To dtusers.Rows.Count - 1
                        ToItem = New gloGeneralItem.gloItem()
                        ToItem.ID = dtusers.Rows(j)("nUserID")
                        ToItem.Description = dtusers.Rows(j)("Description")
                        _ToPatientFormDefaultUserList.Add(ToItem)
                        ToItem = Nothing
                    Next
                    cmbPatientFormDefaultUser.Refresh()
                    If i > 0 Then
                        cmbPatientFormDefaultUser.SelectedIndex = 0
                    End If
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
    End Sub

    Private Sub C1PatientFormUsers_Click(sender As System.Object, e As System.EventArgs) Handles C1PatientFormUsers.Click
        Dim r As Integer = C1PatientFormUsers.RowSel
        Dim ostyle As C1.Win.C1FlexGrid.CellStyle
        nSelectedRow = r
        Dim strUserList As String = ""

        If r >= 0 Then
            If C1PatientFormUsers.ColSel = 5 Then

                If Not IsNothing(C1PatientFormUsers.GetCellStyle(r, 4)) Then
                    ostyle = C1PatientFormUsers.GetCellStyle(r, 4)
                    strUserList = ostyle.ComboList
                Else
                    If Not IsNothing(C1PatientFormUsers.GetData(r, 4)) Then
                        If C1PatientFormUsers.GetData(r, 4) <> "" Then
                            strUserList = C1PatientFormUsers.GetData(r, 4)
                        End If
                    End If
                End If

                If Trim(strUserList) <> "" Then
                    Dim dtprv As New DataTable
                    Dim nUserID As New DataColumn("nUserID")
                    Dim Description As New DataColumn("Description")
                    dtprv.Columns.Add(nUserID)
                    dtprv.Columns.Add(Description)

                    Dim splstruser As String() = strUserList.Split("|")

                    For i As Int16 = 0 To splstruser.Length - 1
                        Dim drTemp As DataRow = dtprv.NewRow()
                        'drTemp("nUserID") = 216512308151873336

                        Dim _dr() As DataRow
                        _dr = dtUser.[Select]("sLoginName = '" & Convert.ToString(splstruser(i).ToString()).Replace("'", "''") & "'")
                        If Not IsNothing(_dr) And _dr.Length > 0 Then
                            drTemp("nUserID") = Convert.ToInt64(_dr(0).Item("nUserID"))
                        End If
                        _dr = Nothing
                        drTemp("Description") = splstruser(i).ToString()
                        dtprv.Rows.Add(drTemp)
                    Next

                    If dtprv.Rows.Count > 0 Then
                        Dim ToItemp As gloGeneralItem.gloItem
                        _ToList = New gloGeneralItem.gloItems
                        For i As Int16 = 0 To dtprv.Rows.Count - 1
                            ToItemp = New gloGeneralItem.gloItem()
                            ToItemp.ID = dtprv.Rows(i)("nUserID")
                            ToItemp.Description = dtprv.Rows(i)("Description")
                            _ToList.Add(ToItemp)
                            ToItemp = Nothing
                        Next
                    End If
                Else
                    If IsNothing(_ToList) = False Then
                        _ToList.Clear()
                    End If
                End If
            End If
        End If

        Dim strUsers As String = ""

        With C1PatientFormUsers
            If r >= 0 Then
                If .ColSel = 5 Then
                    oListUsers = New gloListControl.gloListControl(gloEMRAdmin.mdlGeneral.GetConnectionString(), gloListControl.gloListControlType.Users, True, Me.Width)
                    oListUsers.ControlHeader = "Users"
                    AddHandler oListUsers.ItemSelectedClick, AddressOf olistUsers_ItemSelectedClick
                    AddHandler oListUsers.ItemClosedClick, AddressOf olistUsers_ItemClosedClick
                    'To Select already Added Users.
                    If IsNothing(_ToList) = False Then
                        For i As Integer = 0 To _ToList.Count - 1
                            oListUsers.SelectedItems.Add(_ToList(i))
                        Next
                    End If
                    ofrmList.Controls.Add(oListUsers)
                    oListUsers.Dock = DockStyle.Fill
                    oListUsers.BringToFront()
                    oListUsers.ShowHeaderPanel(False)
                    oListUsers.OpenControl()
                    ofrmList.Text = "Users"
                    ofrmList.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedSingle
                    ofrmList.ShowDialog()
                End If
            End If
        End With
    End Sub

    Private Sub chkPFEnableTaskNotification_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkPFEnableTaskNotification.CheckedChanged
        If chkPFEnableTaskNotification.Checked Then
            chkPatientFormAutoCompleteTask.Checked = IsPFAutoCompleteTaskEnabled()
            chkPatientFormAutoCompleteTask.Enabled = True
        Else
            chkPatientFormAutoCompleteTask.Checked = False
            chkPatientFormAutoCompleteTask.Enabled = False
        End If
    End Sub
End Class
''End