Imports System.Linq
Public Class frmIntuitMessageMapping


    Dim COL_Provider As Integer = 0
    Dim COL_Location As Integer = 1
    Dim COL_User As Integer = 2

    Dim _messageBoxCaption As String = gloGlobal.gloPMGlobal.MessageBoxCaption


    Dim dtProviders As DataTable
    Dim dtLocation As DataTable
    Dim dtUser As DataTable
    Dim dtAppointmentUser As DataTable
    Dim dtRxRenewalUser As DataTable
    Dim dtBillPayUser As DataTable
    ''Added for MU2 Patient portal implementation on 20130625
    Dim dtPortalUser As DataTable
    ''End

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

    Private Sub frmMessageMapping_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            LoadData("Appointment Request")
            If (IspatientPortalEnabled()) Then
                pnlAutoCompleteTasK.Visible = True
                chkAutoCompleteTask.Checked = IsAutoCompleteTaskEnabled()
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
            If (ValidateRule()) Then
                dt = GetdataTOsave()

                'Aniket 08-Mar-13: Remove validation for duplicate task user
                'If (ValidateData(dt)) Then
                dt = AddBasicSetting(dt)
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

            c1AppointmentRequest.Rows.RemoveRange(1, c1AppointmentRequest.Rows.Count - 1)
            C1RxRenewal.Rows.RemoveRange(1, C1RxRenewal.Rows.Count - 1)
            C1BillPay.Rows.RemoveRange(1, C1BillPay.Rows.Count - 1)
            ''Added for MU2 Patient portal implementation on 20130626
            C1PortalUsers.Rows.RemoveRange(1, C1PortalUsers.Rows.Count - 1)
            ''End

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
    Dim ofrmList As New frmUserList
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

End Class
''End