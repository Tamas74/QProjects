Imports System.Data.SqlClient

Public Class gloPatientDataGrid
    Dim dtPatient As DataTable
    Dim dvPatient As DataView

    Dim ProviderName As String = ""
    Dim ProviderLocation As String = ""

    Private _PatientID As Long
    Private _FirstName As String
    Private _MiddleName As String
    Private _LastName As String
    Private _PatientCode As String
    Dim Phone_AS As String
    Dim DOB_AS As Date
    Dim ISDOB_AS As Boolean

    Private _PatientSSN As String ''Used in mergePatient
    Private _PatientProvider As String ''Used in mergePatient
    Private _PaitentDOB As String ''Used in mergePatient

    Public Event OK_Click()

    Public Event Cancel_Click()

    Public Event PicAdv_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    Public Event Refresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    Public Event GetAllPatient_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    ''Start :: MergePatient
    Public Property PatientSSN() As String
        Get
            Return _PatientSSN
        End Get
        Set(ByVal value As String)
            _PatientSSN = value
        End Set
    End Property
    Public Property PatientProvider() As String
        Get
            Return _PatientProvider
        End Get
        Set(ByVal value As String)
            value = _PatientProvider
        End Set
    End Property
    Public Property PatientDOB() As String
        Get
            Return _PaitentDOB
        End Get
        Set(ByVal value As String)
            value = _PaitentDOB
        End Set
    End Property
    ''End :: MergePatient
    Public Property PatientID() As Long
        Get
            Return _PatientID
        End Get
        Set(ByVal Value As Long)
            _PatientID = Value
        End Set
    End Property

    Public Property FirstName() As String
        Get
            Return _FirstName
        End Get
        Set(ByVal Value As String)
            _FirstName = Value
        End Set
    End Property

    Public Property MiddleName() As String
        Get
            Return _MiddleName
        End Get
        Set(ByVal Value As String)
            _MiddleName = Value
        End Set
    End Property

    Public Property LastName() As String
        Get
            Return _LastName
        End Get
        Set(ByVal Value As String)
            _LastName = Value
        End Set
    End Property

    Public Property PatientCode() As String
        Get
            Return _PatientCode
        End Get
        Set(ByVal Value As String)
            _PatientCode = Value
        End Set
    End Property

    Public Sub New()
        InitializeComponent()


    End Sub

    Public Sub New(ByVal strProviderName As String, ByVal strLocation As String)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ProviderName = strProviderName
        ProviderLocation = strLocation
        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Public Sub New(ByVal _dtPatient As DataTable, Optional ByVal strProviderName As String = "", Optional ByVal strLocation As String = "")

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        dtPatient = _dtPatient
        ProviderName = strProviderName
        ProviderLocation = strLocation
        ' Add any initialization after the InitializeComponent() call.

    End Sub


    Private Sub gloPatientDataGrid_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            If IsNothing(dtPatient) = True Then
                dtPatient = Get_Patients()
            End If
            Call Fill_Patients()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.PatientDetail, gloAuditTrail.ActivityType.View, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Public Sub Fill_Patients()

        dgPatient.DataSource = dtPatient.DefaultView

        Dim grdTableStyle As New gloEMRGeneralLibrary.gloprintfax.clsDataGridTableStyle(dtPatient.TableName)

        Dim grdColStylePatientID As New DataGridTextBoxColumn
        With grdColStylePatientID
            .HeaderText = "Patient ID"
            .Alignment = HorizontalAlignment.Left
            .MappingName = dtPatient.Columns(0).ColumnName
            .NullText = ""
            .Width = 0
        End With


        Dim _SetWidth As Single = (dgPatient.Width - 15) / 8

        Dim grdColStylePatientCode As New DataGridTextBoxColumn
        With grdColStylePatientCode
            .HeaderText = "Patient ID"
            .Alignment = HorizontalAlignment.Left
            .MappingName = dtPatient.Columns(1).ColumnName
            .NullText = ""
            .Width = _SetWidth * 1

        End With


        Dim grdColStylePatientFirstName As New DataGridTextBoxColumn
        With grdColStylePatientFirstName
            .HeaderText = "First Name"
            .Alignment = HorizontalAlignment.Left
            .MappingName = dtPatient.Columns(2).ColumnName
            .NullText = ""
            .Width = _SetWidth * 1.5
        End With

        '' Middle name '' 20080526
        Dim grdColStylePatientMiddleName As New DataGridTextBoxColumn
        With grdColStylePatientMiddleName
            .HeaderText = "MI"
            .Alignment = HorizontalAlignment.Left
            .MappingName = dtPatient.Columns("PatientMiddleName").ColumnName
            .NullText = ""
            .Width = _SetWidth * 0.5
        End With

        Dim grdColStylePatientLastName As New DataGridTextBoxColumn
        With grdColStylePatientLastName
            .HeaderText = "Last Name"
            .Alignment = HorizontalAlignment.Left
            .MappingName = dtPatient.Columns(3).ColumnName
            .NullText = ""
            .Width = _SetWidth * 1.5
        End With


        Dim grdColStylePatientSSNNo As New DataGridTextBoxColumn
        With grdColStylePatientSSNNo
            .HeaderText = "SSN"
            .Alignment = HorizontalAlignment.Left
            .MappingName = dtPatient.Columns(4).ColumnName
            .NullText = ""
            .Width = _SetWidth * 1.4
        End With


        Dim grdColStylePatientProvider As New DataGridTextBoxColumn
        With grdColStylePatientProvider
            .HeaderText = "Provider"
            .Alignment = HorizontalAlignment.Left
            .MappingName = dtPatient.Columns(5).ColumnName
            .NullText = ""
            .Width = _SetWidth * 2
        End With

        '//

        Dim grdColStylePatientDOB As New DataGridTextBoxColumn
        With grdColStylePatientDOB
            .HeaderText = "DOB"
            .Alignment = HorizontalAlignment.Left
            .MappingName = dtPatient.Columns(6).ColumnName
            .NullText = ""
            .Width = 0
        End With

        '' 20061221 Advanced Serch CCHIT
        Dim grdColStylePatientPhone As New DataGridTextBoxColumn
        With grdColStylePatientPhone
            .HeaderText = "Phone"
            .Alignment = HorizontalAlignment.Left
            .MappingName = dtPatient.Columns("Phone").ColumnName
            .NullText = ""
            .Width = 0
        End With

        '' 20070128- Advanced Serch CCHIT
        ''sMother_fName,  sMother_lName, sMother_Phone, sMother_Mobile, sFather_fName, sFather_lName, sFather_Phone, sFather_Mobile
        Dim col_sMother_fName As New DataGridTextBoxColumn
        With col_sMother_fName
            .HeaderText = "Phone"
            .Alignment = HorizontalAlignment.Left
            .MappingName = dtPatient.Columns("sMother_fName").ColumnName
            .NullText = ""
            .Width = 0
        End With

        Dim col_sMother_lName As New DataGridTextBoxColumn
        With col_sMother_lName
            .HeaderText = "Phone"
            .Alignment = HorizontalAlignment.Left
            .MappingName = dtPatient.Columns("sMother_lName").ColumnName
            .NullText = ""
            .Width = 0
        End With

        Dim Col_sMother_Phone As New DataGridTextBoxColumn
        With Col_sMother_Phone
            .HeaderText = "Phone"
            .Alignment = HorizontalAlignment.Left
            .MappingName = dtPatient.Columns("sMother_Phone").ColumnName
            .NullText = ""
            .Width = 0
        End With

        Dim Col_sMother_Mobile As New DataGridTextBoxColumn
        With Col_sMother_Mobile
            .HeaderText = "Phone"
            .Alignment = HorizontalAlignment.Left
            .MappingName = dtPatient.Columns("sMother_Mobile").ColumnName
            .NullText = ""
            .Width = 0
        End With
        ''''sFather_fName, sFather_lName, sFather_Phone, sFather_Mobile
        Dim ColsFather_fName As New DataGridTextBoxColumn
        With ColsFather_fName
            .HeaderText = "Phone"
            .Alignment = HorizontalAlignment.Left
            .MappingName = dtPatient.Columns("Phone").ColumnName
            .NullText = ""
            .Width = 0
        End With

        Dim ColsFather_lName As New DataGridTextBoxColumn
        With ColsFather_lName
            .HeaderText = "Phone"
            .Alignment = HorizontalAlignment.Left
            .MappingName = dtPatient.Columns("sFather_lName").ColumnName
            .NullText = ""
            .Width = 0
        End With
        Dim ColsFather_Phone As New DataGridTextBoxColumn
        With ColsFather_Phone
            .HeaderText = "Phone"
            .Alignment = HorizontalAlignment.Left
            .MappingName = dtPatient.Columns("sFather_Phone").ColumnName
            .NullText = ""
            .Width = 0
        End With
        Dim ColsFather_Mobile As New DataGridTextBoxColumn
        With ColsFather_fName
            .HeaderText = "Phone"
            .Alignment = HorizontalAlignment.Left
            .MappingName = dtPatient.Columns("sFather_Mobile").ColumnName
            .NullText = ""
            .Width = 0
        End With

        grdTableStyle.GridColumnStyles.AddRange(New DataGridColumnStyle() {grdColStylePatientID, grdColStylePatientCode, grdColStylePatientFirstName, grdColStylePatientMiddleName, grdColStylePatientLastName, grdColStylePatientSSNNo, grdColStylePatientProvider, grdColStylePatientDOB, grdColStylePatientPhone, col_sMother_fName, col_sMother_lName, Col_sMother_Mobile, Col_sMother_Phone, ColsFather_fName, ColsFather_lName, ColsFather_Mobile, ColsFather_Phone})
        dgPatient.TableStyles.Clear()
        dgPatient.TableStyles.Add(grdTableStyle)
    End Sub

    Private Function Get_Patients(Optional ByVal strProvider As String = "All", Optional ByVal strLocation As String = "All") As DataTable
        Dim objCon As New SqlConnection
        objCon.ConnectionString = gloEMRGeneralLibrary.gloEMRDatabase.DataBaseLayer.ConnectionString
        Dim objCmd As New SqlCommand
        Dim dsData As New DataSet
        Try

            'Dim objSQLDataReader As SqlDataReader
            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "gsp_FillLastPatients"
            objCmd.Parameters.Clear()

            If strProvider <> "All" Then
                Dim objParaProvider As New SqlParameter
                With objParaProvider
                    .ParameterName = "@ProviderName"
                    .Value = strProvider
                    .Direction = ParameterDirection.Input
                    .SqlDbType = SqlDbType.VarChar
                End With
                objCmd.Parameters.Add(objParaProvider)
                objParaProvider = Nothing
            End If

            If strLocation <> "All" Then
                Dim objPara As New SqlParameter
                With objPara
                    .ParameterName = "@Location"
                    .Value = strLocation
                    .Direction = ParameterDirection.Input
                    .SqlDbType = SqlDbType.VarChar
                End With
                objCmd.Parameters.Add(objPara)
                objPara = Nothing
            End If

            objCmd.Connection = objCon
            objCon.Open()
            Dim objDA As New SqlDataAdapter(objCmd)

            objDA.Fill(dsData)
            objDA.Dispose()
            objDA = Nothing
            objCon.Close()
            objCon.Dispose()
            objCon = Nothing
            Dim myTable As DataTable = dsData.Tables(0).Copy()
            Return myTable
        Catch ex As Exception
            Return Nothing
        Finally
            If Not IsNothing(objCmd) Then
                objCmd.Parameters.Clear()
                objCmd.Dispose()
                objCmd = Nothing
            End If
            If (IsNothing(dsData) = False) Then
                dsData.Dispose()
                dsData = Nothing
            End If
        End Try

        '''' PatientID, PatientCode, PatientFirstName, PatientLastName, SSNNo, Provider, PatientDOB, Phone, 
        '''' sMother_fName, sMother_lName, sMother_Phone, sMother_Mobile, sFather_fName, sFather_lName, sFather_Phone, sFather_Mobile, PatientMiddleName  
    End Function

    Private Sub dgPatient_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgPatient.DoubleClick
        Try
            'Shubhangi 20090914
            btnOK_Click(sender, e)

            'AddHandler dgPatient_DoubleClick(), AddressOf btn_tls_OK.Click
            '  AddHandler oChildItem.Click, AddressOf SetMenus
            '    Select Case dgPatient.CurrentCell.ColumnNumber
            '        Case 1
            '            lblSearchCriteria.Text = "Patient ID"
            '        Case 2
            '            lblSearchCriteria.Text = "First Name"
            '        Case 3
            '            lblSearchCriteria.Text = "MI"
            '        Case 4
            '            lblSearchCriteria.Text = "Last Name"
            '        Case 5
            '            lblSearchCriteria.Text = "SSN No"
            '    End Select

        Catch objErr As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.PatientDetail, gloAuditTrail.ActivityType.General, objErr.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(objErr.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub dgPatient_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgPatient.MouseUp
        Dim point As New Point(e.X, e.Y)
        Dim hti As DataGrid.HitTestInfo = dgPatient.HitTest(point)
        If hti.Type = DataGrid.HitTestType.Cell Then
            ' ''
            If dgPatient.CurrentRowIndex >= 0 Then
                dgPatient.UnSelect(dgPatient.CurrentRowIndex)
            End If
            dgPatient.CurrentRowIndex = hti.Row
            dgPatient.Select(hti.Row)
        End If
    End Sub

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_tls_OK.Click
        '''''This IF statement/condition is added by Anil on 31/10/2007, Since it was giving error for Blank DB as there were no rows in the grid.
        If dgPatient.VisibleRowCount <> 0 Then
            _PatientID = dgPatient.Item(dgPatient.CurrentRowIndex, 0)
            _PatientCode = dgPatient.Item(dgPatient.CurrentRowIndex, 1)
            _FirstName = dgPatient.Item(dgPatient.CurrentRowIndex, 2)
            _MiddleName = dgPatient.Item(dgPatient.CurrentRowIndex, 3)
            _LastName = dgPatient.Item(dgPatient.CurrentRowIndex, 4)


            _PatientSSN = dgPatient.Item(dgPatient.CurrentRowIndex, 5)
            _PatientProvider = dgPatient.Item(dgPatient.CurrentRowIndex, 6)
            _PaitentDOB = dgPatient.Item(dgPatient.CurrentRowIndex, 7)

            RaiseEvent OK_Click()
        End If
    End Sub

    Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_tls_Cancel.Click
        RaiseEvent Cancel_Click()
    End Sub

#Region " Patient Search "

    Private Sub txtSearchPatient_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearchPatient.TextChanged
        Try
            Me.Cursor = Cursors.WaitCursor
            'Dim dvPatient As DataView
            dvPatient = CType(dgPatient.DataSource(), DataView)
            If (IsNothing(dvPatient) = False) Then


                dgPatient.DataSource = dvPatient

                'Dim strPatientSearchDetails As String
                'If Trim(txtSearchPatient.Text) <> "" Then
                '    strPatientSearchDetails = Replace(txtSearchPatient.Text, "'", "''")
                'Else
                '    strPatientSearchDetails = ""
                'End If

                Dim strPatientSearchDetails As String
                If Trim(txtSearchPatient.Text) <> "" Then
                    strPatientSearchDetails = Replace(txtSearchPatient.Text, "'", "''")
                    ''''Code line below is added by Anil on 31/10/2007. Since it was giving error for special character " [  ".
                    strPatientSearchDetails = Replace(strPatientSearchDetails, "[", "") & ""
                    strPatientSearchDetails = ReplaceSpecialCharacters(strPatientSearchDetails)
                Else
                    strPatientSearchDetails = ""
                End If

                Dim nPatientCodeColumnNo As Byte = 1
                Dim nPatientFirstNameColumnNo As Byte = 2

                Dim nPatientLastNameColumnNo As Byte = 3
                Dim nPatientSSNColumnNo As Byte = 4
                Dim nProviderColumnNo As Byte = 5
                'If chkPatientsOn.Checked = True Then
                '    nPatientLastNameColumnNo = 4
                '    nPatientSSNColumnNo = 5
                'Else
                nPatientCodeColumnNo = dvPatient.Table.Columns.IndexOf("PatientCode")
                nPatientFirstNameColumnNo = dvPatient.Table.Columns.IndexOf("PatientFirstName")
                'nPatientMiddleNameColumnNo = dvPatient.Table.Columns.IndexOf("PatientMiddleName") '' 3
                nPatientLastNameColumnNo = dvPatient.Table.Columns.IndexOf("PatientLastName") ''4
                nPatientSSNColumnNo = dvPatient.Table.Columns.IndexOf("SSNNo")  ''5
                nProviderColumnNo = dvPatient.Table.Columns.IndexOf("Provider")
                'End If

                'SHUBHANGI 20090914
                'For Generalised Search 
                dvPatient.RowFilter = dvPatient.Table.Columns(nPatientCodeColumnNo).ColumnName & " Like '%" & strPatientSearchDetails & "%' OR " _
                    & dvPatient.Table.Columns(nPatientFirstNameColumnNo).ColumnName & " Like '%" & strPatientSearchDetails & "%' OR " _
                 & dvPatient.Table.Columns(nPatientLastNameColumnNo).ColumnName & " Like '%" & strPatientSearchDetails & "%' OR " _
                 & dvPatient.Table.Columns(nPatientSSNColumnNo).ColumnName & " Like '%" & strPatientSearchDetails & "%' OR " _
                 & dvPatient.Table.Columns(nProviderColumnNo).ColumnName & " Like '%" & strPatientSearchDetails & "%' "

                'Commented By SHUBHANGI
                'Select Case Trim(lblSearchCriteria.Text)
                '    Case "Patient ID"
                '        If strPatientSearchDetails.StartsWith("%") = True Or strPatientSearchDetails.StartsWith("*") = True Then
                '            dvPatient.RowFilter = dvPatient.Table.Columns(nPatientCodeColumnNo).ColumnName & " Like '%" & strPatientSearchDetails & "%'"
                '        Else
                '            dvPatient.RowFilter = dvPatient.Table.Columns(nPatientCodeColumnNo).ColumnName & " Like '" & strPatientSearchDetails & "%'"
                '        End If
                '    Case "First Name"
                '        If strPatientSearchDetails.IndexOf(",") >= 1 Then
                '            Dim strFirstName As String
                '            Dim strLastName As String
                '            strFirstName = Mid(strPatientSearchDetails, 1, strPatientSearchDetails.IndexOf(","))
                '            strLastName = Mid(strPatientSearchDetails, strPatientSearchDetails.IndexOf(",") + 2)
                '            dvPatient.RowFilter = dvPatient.Table.Columns(nPatientFirstNameColumnNo).ColumnName & " Like '" & strFirstName & "%' and " & dvPatient.Table.Columns(nPatientLastNameColumnNo).ColumnName & " Like '" & strLastName & "%'"
                '        Else
                '            If strPatientSearchDetails.StartsWith("%") = True Or strPatientSearchDetails.StartsWith("*") = True Then
                '                dvPatient.RowFilter = dvPatient.Table.Columns(nPatientFirstNameColumnNo).ColumnName & " Like '%" & strPatientSearchDetails & "%'"
                '            Else
                '                dvPatient.RowFilter = dvPatient.Table.Columns(nPatientFirstNameColumnNo).ColumnName & " Like '" & strPatientSearchDetails & "%'"
                '            End If
                '        End If

                '    Case "MI"
                '        If strPatientSearchDetails <> "" Then
                '            dvPatient.RowFilter = dvPatient.Table.Columns(nPatientMiddleNameColumnNo).ColumnName & " Like '%" & strPatientSearchDetails & "%'"
                '        Else
                '            dvPatient.RowFilter = dvPatient.Table.Columns(nPatientMiddleNameColumnNo).ColumnName & " Like '%'"
                '        End If

                '    Case "Last Name"
                '        If strPatientSearchDetails.IndexOf(",") >= 1 Then
                '            Dim strFirstName As String
                '            Dim strLastName As String
                '            strLastName = Mid(strPatientSearchDetails, 1, strPatientSearchDetails.IndexOf(","))
                '            strFirstName = Mid(strPatientSearchDetails, strPatientSearchDetails.IndexOf(",") + 2)
                '            dvPatient.RowFilter = dvPatient.Table.Columns(nPatientFirstNameColumnNo).ColumnName & " Like '" & strFirstName & "%' and " & dvPatient.Table.Columns(nPatientLastNameColumnNo).ColumnName & " Like '" & strLastName & "%'"
                '        Else
                '            If strPatientSearchDetails.StartsWith("%") = True Or strPatientSearchDetails.StartsWith("*") = True Then
                '                dvPatient.RowFilter = dvPatient.Table.Columns(nPatientLastNameColumnNo).ColumnName & " Like '%" & strPatientSearchDetails & "%'"
                '            Else
                '                dvPatient.RowFilter = dvPatient.Table.Columns(nPatientLastNameColumnNo).ColumnName & " Like '" & strPatientSearchDetails & "%'"
                '            End If
                '        End If
                '    Case "SSN No"
                '        If strPatientSearchDetails <> "" And IsNumeric(strPatientSearchDetails) = True Then
                '            dvPatient.RowFilter = dvPatient.Table.Columns(nPatientSSNColumnNo).ColumnName & " Like '%" & strPatientSearchDetails & "%'"
                '        Else
                '            dvPatient.RowFilter = dvPatient.Table.Columns(nPatientSSNColumnNo).ColumnName & " Like '%'"
                '        End If
                'End Select
            End If
            Me.Cursor = Cursors.Default
        Catch objErr As Exception
            Me.Cursor = Cursors.Default
            MessageBox.Show(objErr.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.PatientDetail, gloAuditTrail.ActivityType.General, objErr.ToString, gloAuditTrail.ActivityOutCome.Failure)
        End Try

    End Sub

    Private Sub txtSearchPatient_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSearchPatient.KeyPress
        If e.KeyChar = Chr(Keys.Enter) Then
            If dgPatient.VisibleRowCount >= 1 Then
                dgPatient.CurrentRowIndex = 0
                dgPatient.Select(0)
                'Call Fill_PatientDetails()
            End If
        End If
    End Sub
    Public Function ReplaceSpecialCharacters(ByVal strSpecialChar As String) As String
        Try

            strSpecialChar = Replace(strSpecialChar, "#", "") & ""
            strSpecialChar = Replace(strSpecialChar, "$", "") & ""
            strSpecialChar = Replace(strSpecialChar, "%", "") & ""
            strSpecialChar = Replace(strSpecialChar, "^", "") & ""
            strSpecialChar = Replace(strSpecialChar, "&", "") & ""
            'strSpecialChar = Replace(strSpecialChar, "~", "") & ""
            'strSpecialChar = Replace(strSpecialChar, "!", "") & ""
            'strSpecialChar = Replace(strSpecialChar, "*", "") & ""
            'strSpecialChar = Replace(strSpecialChar, ";", "") & ""
            'strSpecialChar = Replace(strSpecialChar, "/", "") & ""
            'strSpecialChar = Replace(strSpecialChar, "?", "") & ""
            'strSpecialChar = Replace(strSpecialChar, ">", "") & ""
            'strSpecialChar = Replace(strSpecialChar, "<", "") & ""
            'strSpecialChar = Replace(strSpecialChar, "\", "") & ""
            'strSpecialChar = Replace(strSpecialChar, "|", "") & ""
            'strSpecialChar = Replace(strSpecialChar, "{", "") & ""
            'strSpecialChar = Replace(strSpecialChar, "}", "") & ""
            'strSpecialChar = Replace(strSpecialChar, "-", "") & ""
            'strSpecialChar = Replace(strSpecialChar, "_", "") & ""
            'strSpecialChar = Replace(strSpecialChar, "'", "") & ""
            Return strSpecialChar
        Catch ex As Exception

            MessageBox.Show(ex.Message, "Patient", MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.PatientDetail, gloAuditTrail.ActivityType.General, ex.Message, gloAuditTrail.ActivityOutCome.Failure)
            Return Nothing
        End Try
    End Function

    Private Sub btnGetAllPatients_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetAllPatients.Click
        Try
            txtSearchPatient.Text = ""

            If IsNothing(dvPatient) = False Then
                dvPatient.RowFilter = ""
            End If

            Dim nCount As Integer
            Try
                Dim myDataview As DataView = CType(dgPatient.DataSource, DataView)
                If (IsNothing(myDataview) = False) Then


                    For nCount = 0 To myDataview.Table.Rows.Count - 1
                        If dgPatient.Item(nCount, 1) = _PatientCode Then
                            'If nCount + 10 <= CType(dgPatient.DataSource, DataView).Table.Rows.Count Then
                            '    dgPatient.CurrentRowIndex = nCount + 10
                            'ElseIf nCount + 5 <= CType(dgPatient.DataSource, DataView).Table.Rows.Count Then
                            '    dgPatient.CurrentRowIndex = nCount + 5
                            'End If

                            dgPatient.CurrentRowIndex = nCount
                            dgPatient.Select(nCount)
                            'Call Fill_PatientDetails()
                            RaiseEvent GetAllPatient_Click(sender, e)
                            Exit Sub
                        End If
                    Next
                End If
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.PatientDetail, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)

            End Try
        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.PatientDetail, gloAuditTrail.ActivityType.General, objErr.ToString, gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Try
            'RaiseEvent Refresh_Click(sender, e)
            dtPatient = Get_Patients()
            Call Fill_Patients()

        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.PatientDetail, gloAuditTrail.ActivityType.Refresh, objErr.ToString, gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub
#End Region


    Private Sub picAdvSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles picAdvSearch.Click
        RaiseEvent PicAdv_Click(sender, e)
    End Sub

    Private Sub btn_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs)
        CType(sender, Button).BackgroundImage = Global.gloUserControlLibrary.My.Resources.Resources.Img_LongYellow
        CType(sender, Button).BackgroundImageLayout = ImageLayout.Stretch
    End Sub

    Private Sub btn_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs)
        CType(sender, Button).BackgroundImage = Global.gloUserControlLibrary.My.Resources.Resources.Img_LongButton
        CType(sender, Button).BackgroundImageLayout = ImageLayout.Stretch
    End Sub


End Class
