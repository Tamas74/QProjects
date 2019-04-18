Imports System.Data.SqlClient
Imports System.Data
Public Class frmMobileMgnt
    Dim dtUser As DataTable
    Dim StrUser As String
    Dim ChangeStrUser As String = ""
    Dim NewRowNo As Int16
    Dim Temp As String
    Dim applicationindex As Integer
    Dim dtApplication As DataTable
    Dim UserChange As Boolean = False
    Dim UserCount As Int16


    ''' <summary>
    ''' Call WebService Setting Form 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub TsSettingBtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TsSettingBtn.Click
        Try
            Dim frmWebServiceSetting As New frmWebServiceSetting
            frmWebServiceSetting.ShowDialog()
        Catch ex As Exception
            MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' Fill Application ComboBox.Design and fill application grid as per application comboBox selection
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmMobileMgnt_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ResetData()        
    End Sub


    Public Sub ResetData()
        Try
            RemoveHandler cmbApplication.SelectedValueChanged, AddressOf cmbApplication_SelectedValueChanged
            RemoveHandler c1ApplicationData.EnterCell, AddressOf c1ApplicationData_EnterCell
            cmbApplication.DropDownStyle = ComboBoxStyle.DropDownList
            cmbApplication.DataSource = FillApplication(Nothing)
            cmbApplication.DisplayMember = "ApplicationName"
            cmbApplication.ValueMember = "ApplicationID"
            cmbApplication.SelectedIndex = -1
            applicationindex = cmbApplication.SelectedIndex

            AddHandler cmbApplication.SelectedValueChanged, AddressOf cmbApplication_SelectedValueChanged

            If (cmbApplication.Text <> "") Then
                c1ApplicationData.DataSource = FillApplication(cmbApplication.SelectedValue)
            Else
                c1ApplicationData.DataSource = FillApplication(-2)
            End If

            NewRowNo = c1ApplicationData.Rows.Count
            DesignGrid()
            AddHandler c1ApplicationData.EnterCell, AddressOf c1ApplicationData_EnterCell

        Catch ex As Exception
            MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' Get Application Data
    ''' </summary>
    ''' <param name="ApplicationID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function FillApplication(ByVal ApplicationID As Int64) As DataTable
        Dim dt As DataTable
        Dim ds As DataSet
        Dim objCon As New SqlConnection
        Try

            objCon.ConnectionString = gloEMRAdmin.mdlGeneral.GetConnectionString()
            Dim objCmd As New SqlCommand

            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "WS_GetApplicationData"

            Dim oparam As SqlParameter
            oparam = New SqlParameter("@ApplicationID", SqlDbType.BigInt)
            oparam.Value = ApplicationID
            objCmd.Parameters.Add(oparam)
            oparam = Nothing

            objCmd.Connection = objCon
            objCon.Open()

            Dim objDA As New SqlDataAdapter(objCmd)
            ds = New DataSet
            objDA.Fill(ds, "ApplicationData")
            dt = ds.Tables("ApplicationData")
            StrUser = ds.Tables("ApplicationData1").Rows(0)(0).ToString()

            UserCount = ds.Tables("ApplicationData2").Rows(0)(0)

            'Dim userlist As String()

            'userlist = StrUser.Split("|")
            'UserCount = userlist.Length
            dtApplication = dt
            Return dt
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return Nothing
        Finally
            If Not IsNothing(objCon) Then
                objCon.Dispose()
                objCon = Nothing
            End If
            If Not IsNothing(ds) Then
                ds.Dispose()
                ds = Nothing
            End If           
        End Try
    End Function


    Public Function FillUser() As String
        Dim dt As DataTable
        Dim ds As DataSet
        Dim objCon As New SqlConnection
        Try

            objCon.ConnectionString = gloEMRAdmin.mdlGeneral.GetConnectionString()
            Dim objCmd As New SqlCommand

            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "WS_GetUserData"

            objCmd.Connection = objCon
            objCon.Open()

            Dim objDA As New SqlDataAdapter(objCmd)
            ds = New DataSet
            objDA.Fill(ds, "UserData")
            dt = ds.Tables("UserData")
            Dim str As String
            str = dt.Rows(0)(0).ToString()
            Return str
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return ""
        Finally
            If Not IsNothing(objCon) Then
                objCon.Dispose()
                objCon = Nothing
            End If
            If Not IsNothing(ds) Then
                ds.Dispose()
                ds = Nothing
            End If
        End Try
    End Function

    ''' <summary>
    ''' As per application selection fill User Application grid.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmbApplication_SelectedValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbApplication.SelectedValueChanged
        Dim result As DialogResult
        Dim dt As DataTable

        Dim StrUser As String = ""
        Try
            c1ApplicationData.Update()
            c1ApplicationData.FinishEditing()
            dt = c1ApplicationData.DataSource
            For i As Integer = NewRowNo To c1ApplicationData.Rows.Count - 1
                If (cmbApplication.Text = "") Then
                    MessageBox.Show("Select application to add new user ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    cmbApplication.Focus()
                    RemoveHandler cmbApplication.SelectedValueChanged, AddressOf cmbApplication_SelectedValueChanged
                    cmbApplication.SelectedIndex = applicationindex
                    AddHandler cmbApplication.SelectedValueChanged, AddressOf cmbApplication_SelectedValueChanged
                    Return
                End If

                If (c1ApplicationData.GetData(i, "gloEMRUser").ToString().Trim() = "") Then
                    MessageBox.Show("Select gloEMRUser to provide access for application '" + cmbApplication.Text.Trim() + "'", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    c1ApplicationData.Select(i, 0)
                    RemoveHandler cmbApplication.SelectedValueChanged, AddressOf cmbApplication_SelectedValueChanged
                    cmbApplication.SelectedIndex = applicationindex
                    AddHandler cmbApplication.SelectedValueChanged, AddressOf cmbApplication_SelectedValueChanged
                    Return
                Else
                    If (StrUser = "") Then
                        StrUser = c1ApplicationData.GetData(i, "gloEMRUser").ToString().Trim()
                    Else
                        StrUser = StrUser + "," + c1ApplicationData.GetData(i, "gloEMRUser").ToString().Trim()
                    End If
                End If
            Next
            If (StrUser <> "") Then '' module Save and close
                UserChange = False
                If MessageBox.Show("You are adding new users " + StrUser + " to selected application, Click 'Yes' to confirm", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information) = DialogResult.Yes Then
                    UpdateApplication(dt)
                    c1ApplicationData.DataSource = FillApplication(cmbApplication.SelectedValue)
                    DesignGrid()
                    NewRowNo = c1ApplicationData.Rows.Count
                Else
                    c1ApplicationData.DataSource = FillApplication(cmbApplication.SelectedValue)
                    DesignGrid()
                    NewRowNo = c1ApplicationData.Rows.Count
                End If
            Else
                If (UserChange = True) Then
                    UserChange = False
                    result = MessageBox.Show("Do you want save changes , Click 'Yes' to confirm", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information)
                    If (result = DialogResult.Yes) Then
                        c1ApplicationData.Update()
                        c1ApplicationData.FinishEditing()
                        UpdateApplication(c1ApplicationData.DataSource)
                    Else

                    End If

                End If
                c1ApplicationData.DataSource = FillApplication(cmbApplication.SelectedValue)
                DesignGrid()
                NewRowNo = c1ApplicationData.Rows.Count
            End If
           

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        'Try
        '    '   c1ApplicationData.DataSource = Nothing     
        '    c1ApplicationData.DataSource = FillApplication(cmbApplication.SelectedValue)
        '    DesignGrid()
        '    NewRowNo = c1ApplicationData.Rows.Count
        'Catch ex As Exception
        '    MessageBox.Show(ex.Message)
        'End Try

        applicationindex = cmbApplication.SelectedIndex

    End Sub

    ''' <summary>
    ''' Update and insert data to database
    ''' </summary>
    ''' <param name="ApplicationData"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function UpdateApplication(ByVal ApplicationData As DataTable) As Boolean

        Dim objCon As New SqlConnection
        Try

            objCon.ConnectionString = gloEMRAdmin.mdlGeneral.GetConnectionString()
            Dim objCmd As New SqlCommand

            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "WS_UpdateUserApplication"

            Dim oparam As SqlParameter
            oparam = New SqlParameter("@TVP_Application", SqlDbType.Structured)
            oparam.Value = ApplicationData
            objCmd.Parameters.Add(oparam)
            oparam = Nothing

            objCmd.Connection = objCon
            objCon.Open()
            objCmd.ExecuteNonQuery()

            Return True
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return False
        Finally
            If Not IsNothing(objCon) Then
                objCon.Dispose()
                objCon = Nothing
            End If
          
        End Try
    End Function


    ''' <summary>
    ''' Save grid data to Database
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub TsSaveBtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TsSaveBtn.Click
        Dim dt As DataTable

        Dim StrUser As String = ""
        Try
            c1ApplicationData.Update()
            c1ApplicationData.FinishEditing()
            dt = c1ApplicationData.DataSource
            For i As Integer = NewRowNo To c1ApplicationData.Rows.Count - 1
                If (cmbApplication.Text = "") Then
                    MessageBox.Show("Select application to add new user ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    cmbApplication.Focus()
                    Return
                End If

                If (c1ApplicationData.GetData(i, "gloEMRUser").ToString().Trim() = "") Then
                    MessageBox.Show("Select gloEMRUser to provide access for application" + cmbApplication.Text.Trim(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    c1ApplicationData.Select(i, 0)
                    Return
                Else
                    If (StrUser = "") Then
                        StrUser = "'" + c1ApplicationData.GetData(i, "gloEMRUser").ToString().Trim() + "'"
                    Else
                        StrUser = StrUser + ",'" + c1ApplicationData.GetData(i, "gloEMRUser").ToString().Trim() + "'"
                    End If
                End If
            Next
            If (StrUser <> "") Then '' module Save and close
                If MessageBox.Show("You are adding new users " + StrUser + " to selected application, Click 'Yes' to confirm", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information) = DialogResult.Yes Then
                    UpdateApplication(dt)
                    NewRowNo = c1ApplicationData.Rows.Count() + 1
                    UserChange = False
                    Me.Close()
                Else
                    Return
                End If
            Else
                UpdateApplication(dt)
                NewRowNo = c1ApplicationData.Rows.Count() + 1
                UserChange = False
                Me.Close()
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Sub TsCancelBtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TsCancelBtn.Click
        If (NewRowNo < c1ApplicationData.Rows.Count()) Then ''Found new user got added to grid but not saved
            StrUser = ""
            For i As Integer = NewRowNo To c1ApplicationData.Rows.Count - 1
                If (c1ApplicationData.GetData(i, "gloEMRUser").ToString().Trim() <> "") Then
                    If (StrUser = "") Then
                        StrUser = "'" + c1ApplicationData.GetData(i, "gloEMRUser").ToString().Trim() + "'"
                    Else
                        StrUser = StrUser + ",'" + c1ApplicationData.GetData(i, "gloEMRUser").ToString().Trim() + "'"
                    End If
                End If
            Next
            Dim result As DialogResult
            result = MessageBox.Show("Do you want save new users " + StrUser + " to selected application, Click 'Yes' to confirm", gstrMessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information)
            If (result = DialogResult.Yes) Then
                For i As Integer = NewRowNo To c1ApplicationData.Rows.Count - 1
                    If (cmbApplication.Text = "") Then
                        MessageBox.Show("Select application to add new user ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        cmbApplication.Focus()
                        Return
                    End If

                    If (c1ApplicationData.GetData(i, "gloEMRUser").ToString().Trim() = "") Then
                        MessageBox.Show("Select gloEMRUser to provide access for application" + cmbApplication.Text.Trim(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        c1ApplicationData.Select(i, 0)
                        Return
                    End If
                Next
                c1ApplicationData.Update()
                c1ApplicationData.FinishEditing()
                UpdateApplication(c1ApplicationData.DataSource)
                NewRowNo = c1ApplicationData.Rows.Count() + 1
                Me.Close()
            ElseIf (result = DialogResult.No) Then
                NewRowNo = c1ApplicationData.Rows.Count() + 1
                Me.Close()
            Else

            End If
        Else
            Me.Close()
        End If

    End Sub
    ''' <summary>
    ''' Add new user 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub TsNewBtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TsNewBtn.Click
        Try
            If (cmbApplication.Text = "") Then
                MessageBox.Show("Select application to add new user", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                cmbApplication.Focus()
                Return
            End If
            c1ApplicationData.Rows.Add()
            If (ChangeStrUser = "") Then
                If (c1ApplicationData.RowSel >= 0) Then
                    c1ApplicationData.Rows.Remove(c1ApplicationData.RowSel)
                    '' All users in gloSUITE  had given authorization to selected application - Display message says All users have associated to this application
                    MessageBox.Show("All gloEMRUsers has been assigned access for selected application", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    c1ApplicationData.Select(c1ApplicationData.RowSel, 0)
                    Return
                End If             
            End If
            If (UserCount < c1ApplicationData.Rows.Count() - 1) Then
                If (c1ApplicationData.RowSel >= 0) Then
                    c1ApplicationData.Rows.Remove(c1ApplicationData.RowSel)
                    '' All users in gloSUITE  had given authorization to selected application - Display message says All users have associated to this application
                    MessageBox.Show("Selected application user limit over ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    c1ApplicationData.Select(c1ApplicationData.RowSel, 0)
                    Return
                End If
            End If
            c1ApplicationData.Update()
            c1ApplicationData.FinishEditing()
            'NewRowNo = c1ApplicationData.Rows.Count - 1
            c1ApplicationData.Select(c1ApplicationData.Rows.Count - 2, 0)
            c1ApplicationData.SetData(c1ApplicationData.Rows.Count - 1, "nApplicationId", cmbApplication.SelectedValue)
            c1ApplicationData.SetData(c1ApplicationData.Rows.Count - 1, "ActivatedBy", gstrLoginName)
            c1ApplicationData.SetData(c1ApplicationData.Rows.Count - 1, "Activated", System.DateTime.Now)
            c1ApplicationData.SetData(c1ApplicationData.Rows.Count - 1, "Active", False)
            c1ApplicationData.SetData(c1ApplicationData.Rows.Count - 1, "Block", False)
            c1ApplicationData.SetData(c1ApplicationData.Rows.Count - 1, "nAuthenticationId", 0)
            c1ApplicationData.SetData(c1ApplicationData.Rows.Count - 1, "nUserID", 0)
            c1ApplicationData.Select(c1ApplicationData.Rows.Count - 1, 0)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' As per selection of gloUser column allow editing properties change and Fill user list
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub c1ApplicationData_EnterCell(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles c1ApplicationData.EnterCell
        Try
            'If c1ApplicationData.RowSel = c1ApplicationData.Rows.Count - 1 Then
            '    UserList()
            '    If ChangeStrUser = "" Then
            '        c1ApplicationData.Cols(0).ComboList = StrUser
            '    Else
            '        c1ApplicationData.Cols(0).ComboList = ChangeStrUser
            '    End If

            '    '    c1ApplicationData.Cols(0).ComboList = 
            'End If
            If c1ApplicationData.ColSel = 0 Then
                If c1ApplicationData.RowSel >= NewRowNo And NewRowNo <> -1 Then
                    c1ApplicationData.Cols(0).AllowEditing = True
                    UserList()
                    'If ChangeStrUser = "" Then
                    '    c1ApplicationData.Cols(0).ComboList = StrUser
                    'Else
                    If ChangeStrUser <> "" Then
                        c1ApplicationData.Cols(0).ComboList = ChangeStrUser
                    End If

                    'End If
                Else
                    c1ApplicationData.Cols(0).AllowEditing = False
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' User list updated
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub UserList()
        Try
        ChangeStrUser = StrUser
            For i As Integer = NewRowNo To c1ApplicationData.Rows.Count - 1
                If c1ApplicationData.Cols.Count > 3 Then
                    If (IsDBNull(c1ApplicationData.GetData(i, "gloEMRUser"))) Then
                    Else
                        If (i <> c1ApplicationData.RowSel) Then
                            Temp = c1ApplicationData.GetData(i, "gloEMRUser")
                            If Temp <> "" Then
                                If (StrUser.Contains(Temp + "|")) Then
                                    ChangeStrUser = ChangeStrUser.Replace(Temp + "|", "")
                                ElseIf (StrUser.Contains(Temp)) Then
                                    ChangeStrUser = ChangeStrUser.Replace(Temp, "")
                                End If
                            End If
                        End If
                    End If
                End If
            Next
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' Design Application grid
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub DesignGrid()
        Try
            c1ApplicationData.Cols("nApplicationId").Visible = False
            c1ApplicationData.Cols("nUserID").Visible = False
            c1ApplicationData.Cols("nAuthenticationId").Visible = False

            c1ApplicationData.Cols(0).AllowEditing = False
            c1ApplicationData.Cols(2).AllowEditing = False
            c1ApplicationData.Cols(3).AllowEditing = False

            c1ApplicationData.Cols("gloEMRUser").Caption = "gloEMR User"
            c1ApplicationData.Cols("Activated").Caption = "Activated Date"
            c1ApplicationData.Cols("ActivatedBy").Caption = "Activated By"

            c1ApplicationData.Cols("gloEMRUser").Width = 207
            c1ApplicationData.Cols("Active").Width = 81
            c1ApplicationData.Cols("Activated").Width = 156
            c1ApplicationData.Cols("ActivatedBy").Width = 203
            c1ApplicationData.Cols("Block").Width = 90


            c1ApplicationData.Cols("Activated").DataType = GetType(DateTime)
            c1ApplicationData.Cols("Activated").Format = "g"

            c1ApplicationData.AllowSorting = False

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub



    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Try
            Dim temp As Int64
            temp = cmbApplication.SelectedValue
            Dim frmapplication As New frmApplication
            frmapplication.ShowDialog()
            ResetData()
            If (temp <> 0) Then
                cmbApplication.SelectedValue = temp
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub c1ApplicationData_AfterEdit(ByVal sender As System.Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles c1ApplicationData.AfterEdit, c1ApplicationData.BeforeEdit

        'Try
        'If c1ApplicationData.ColSel = 0 Then
        '    If c1ApplicationData.RowSel >= NewRowNo Then
        '        If (StrUser.Contains(cmbApplication.SelectedText + "|")) Then
        '            ChangeStrUser = StrUser.Replace(cmbApplication.SelectedText + "|", "")
        '        End If
        '        If (StrUser.Contains(cmbApplication.SelectedText)) Then
        '            ChangeStrUser = StrUser.Replace(cmbApplication.SelectedText, "")
        '        End If
        '        'cmbApplication.SelectedText
        '    End If
        '    End If
        'Catch ex As Exception
        '    MessageBox.Show(ex.Message)
        'End Try
    End Sub

    Private Sub TsDeleteBtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TsDeleteBtn.Click
        Try
            If (c1ApplicationData.RowSel >= 0) Then
                Dim authid As Long = c1ApplicationData.GetData(c1ApplicationData.RowSel, "nAuthenticationId")
                If (authid <> 0) Then
                    If (MessageBox.Show("Do you want to remove " + cmbApplication.Text.Trim() + " authorization for gloEMRUser '" + c1ApplicationData.GetData(c1ApplicationData.RowSel, "gloEMRUser") + "'?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information) = DialogResult.Yes) Then
                        If (DeleteUser(authid)) Then
                            Dim temp As String = cmbApplication.SelectedValue.ToString()
                            c1ApplicationData.Rows.Remove(c1ApplicationData.RowSel)
                            ResetData()
                            cmbApplication.SelectedValue = temp
                        End If
                    Else
                        Return
                    End If
                Else
                    c1ApplicationData.Rows.Remove(c1ApplicationData.RowSel)
                    If (NewRowNo > 1) Then
                        NewRowNo = NewRowNo - 1
                    End If
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Function DeleteUser(ByVal AuthenticationId As Long) As Boolean
        
        Dim objCon As New SqlConnection
        Try
            objCon.ConnectionString = gloEMRAdmin.mdlGeneral.GetConnectionString()
            Dim objCmd As New SqlCommand

            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "WS_DeleteAuthenticationData"

            Dim oparam As SqlParameter
            oparam = New SqlParameter("@AuthenticationId", SqlDbType.BigInt)
            oparam.Value = AuthenticationId
            objCmd.Parameters.Add(oparam)
            oparam = Nothing

            objCmd.Connection = objCon
            objCon.Open()

            objCmd.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return False
        Finally
            If Not IsNothing(objCon) Then
                objCon.Dispose()
                objCon = Nothing
            End If          
        End Try
    End Function


    Private Sub frmMobileMgnt_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        Dim result As DialogResult
        If (NewRowNo < c1ApplicationData.Rows.Count()) Then
            StrUser = ""
            For i As Integer = NewRowNo To c1ApplicationData.Rows.Count - 1
                If (c1ApplicationData.GetData(i, "gloEMRUser").ToString().Trim() <> "") Then
                    If (StrUser = "") Then
                        StrUser = "'" + c1ApplicationData.GetData(i, "gloEMRUser").ToString().Trim() + "'"
                    Else
                        StrUser = StrUser + ",'" + c1ApplicationData.GetData(i, "gloEMRUser").ToString().Trim() + "'"
                    End If
                End If
            Next

            result = MessageBox.Show("Do you want save new users " + StrUser + " to selected application, Click 'Yes' to confirm", gstrMessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information)
            If (result = DialogResult.Yes) Then
                For i As Integer = NewRowNo To c1ApplicationData.Rows.Count - 1
                    If (cmbApplication.Text = "") Then
                        MessageBox.Show("Select application to add new user ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        cmbApplication.Focus()
                        e.Cancel = True
                        Return
                    End If

                    If (c1ApplicationData.GetData(i, "gloEMRUser").ToString().Trim() = "") Then
                        MessageBox.Show("Select gloEMRUser to provide access for application" + cmbApplication.Text.Trim(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        c1ApplicationData.Select(i, 0)
                        e.Cancel = True
                        Return
                    End If
                Next
                c1ApplicationData.Update()
                c1ApplicationData.FinishEditing()
                UpdateApplication(c1ApplicationData.DataSource)
            ElseIf (result = DialogResult.No) Then

            Else
                e.Cancel = True
            End If
        Else
            If (UserChange = True) Then
                result = MessageBox.Show("Do you want save changes , Click 'Yes' to confirm", gstrMessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information)
                If (result = DialogResult.Yes) Then
                    c1ApplicationData.Update()
                    c1ApplicationData.FinishEditing()
                    UpdateApplication(c1ApplicationData.DataSource)
                ElseIf (result = DialogResult.No) Then

                Else
                    e.Cancel = True
                End If

            End If
        End If

    End Sub


    
    Private Sub c1ApplicationData_CellChecked(ByVal sender As System.Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles c1ApplicationData.CellChecked
        UserChange = True
    End Sub
End Class