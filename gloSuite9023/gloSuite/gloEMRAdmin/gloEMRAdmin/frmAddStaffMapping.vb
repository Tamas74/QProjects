Imports System.Data.SqlClient

Public Class frmAddStaffMapping
    Dim _MappingID As Long
    Dim _staffID As Long
    Dim _Description As String
    Dim dtUsers As DataTable
    Dim ofrmList As frmUserList
    Dim oListUsers As gloListControl.gloListControl
    Private _getMappedUsers As DataTable
    Dim _ToList As gloGeneralItem.gloItems

    Public Property MappingID() As Long
        Get
            Return _MappingID
        End Get
        Set(ByVal value As Long)
            _MappingID = value
        End Set
    End Property

    Public Property staffID() As Long
        Get
            Return _staffID
        End Get
        Set(ByVal value As Long)
            _staffID = value
        End Set
    End Property

    Public Property Description() As String
        Get
            Return _Description
        End Get
        Set(ByVal value As String)
            _Description = value
        End Set
    End Property

    Private Sub btnSearchUser_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchUser.Click
        Try
            ofrmList = New frmUserList
            oListUsers = New gloListControl.gloListControl(gloEMRAdmin.mdlGeneral.GetConnectionString(), gloListControl.gloListControlType.Users, True, Me.Width)
            oListUsers.ControlHeader = "Users"
            AddHandler oListUsers.ItemSelectedClick, AddressOf olistUsers_ItemSelectedClick
            AddHandler oListUsers.ItemClosedClick, AddressOf olistUsers_ItemClosedClick

            ' ''To Select already Added Users.
            If IsNothing(_ToList) = False Then
                For i As Integer = 0 To _ToList.Count - 1
                    oListUsers.SelectedItems.Add(_ToList(i))
                Next
            End If
            '

            ofrmList.Controls.Add(oListUsers)
            oListUsers.Dock = DockStyle.Fill
            oListUsers.BringToFront()
            oListUsers.ShowHeaderPanel(False)
            oListUsers.OpenControl()
            ofrmList.Text = "Users"
            ofrmList.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedSingle
            ofrmList.ShowDialog()
            If IsNothing(ofrmList) = False Then
                ofrmList.Dispose()
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub frmAddStaffMapping_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If _MappingID > 0 Then
            loadControls()
        End If
    End Sub

    Public Sub RefreshUsers(ByVal dt As DataTable)
        Try
            cmb_To.DataSource = dt
            cmb_To.ValueMember = dt.Columns("nUserID").ColumnName
            cmb_To.DisplayMember = dt.Columns("Description").ColumnName
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

    Public Sub FillMappedUsers()
        Dim oclsgloIntuit As clsgloIntuit
        Dim dt As DataTable
        Try

            oclsgloIntuit = New clsgloIntuit
            dt = oclsgloIntuit.GetMappedUsers(_MappingID)
            If Not IsNothing(dt) Then
                Dim ToItem As gloGeneralItem.gloItem
                _ToList = New gloGeneralItem.gloItems
                For i As Int16 = 0 To dt.Rows.Count - 1
                    ToItem = New gloGeneralItem.gloItem()
                    ToItem.ID = dt.Rows(i)("nUserID")
                    ToItem.Description = dt.Rows(i)("Description")
                    _ToList.Add(ToItem)
                    ToItem = Nothing
                Next
            End If

            cmb_To.DataSource = dt
            cmb_To.ValueMember = dt.Columns("nUserID").ColumnName
            cmb_To.DisplayMember = dt.Columns("Description").ColumnName


        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not IsNothing(oclsgloIntuit) Then
                oclsgloIntuit.Dispose()
                oclsgloIntuit = Nothing
            End If
        End Try
    End Sub

    Private Sub olistUsers_ItemSelectedClick(ByVal sender As Object, ByVal e As EventArgs)
        'cmb_To.Items.Clear(); 
        Dim dtUsers As New DataTable()
        Dim dcnIntuitStaffMappingDetailID As New DataColumn("nIntuitStaffMappingDetailID")
        Dim dcnIntuitStaffMappingID As New DataColumn("nIntuitStaffMappingID")
        Dim dcId As New DataColumn("nUserID")
        Dim dcDescription As New DataColumn("Description")
        dtUsers.Columns.Add(dcnIntuitStaffMappingDetailID)
        dtUsers.Columns.Add(dcnIntuitStaffMappingID)
        dtUsers.Columns.Add(dcId)
        dtUsers.Columns.Add(dcDescription)
        _ToList = New gloGeneralItem.gloItems
        Dim ToItem As gloGeneralItem.gloItem

        If oListUsers.SelectedItems.Count > 0 Then
            For i As Int16 = 0 To oListUsers.SelectedItems.Count - 1
                Dim drTemp As DataRow = dtUsers.NewRow()
                drTemp("nIntuitStaffMappingDetailID") = 0
                drTemp("nIntuitStaffMappingID") = 0
                drTemp("nUserID") = oListUsers.SelectedItems(i).ID
                drTemp("Description") = oListUsers.SelectedItems(i).Description
                dtUsers.Rows.Add(drTemp)

                '
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

    Private Sub loadControls()
        Try
            txtstaffID.Text = _staffID
            txtDescription.Text = _Description
            FillMappedUsers()
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        Dim oclsgloIntuit As clsgloIntuit
        Try
            If Validate_data() = True Then
                Exit Sub
            End If
            _staffID = Convert.ToInt64(txtstaffID.Text)
            _Description = txtDescription.Text
            Dim dtUsers As DataTable
            If Not IsNothing(CType(cmb_To.DataSource, DataTable)) Then
                dtUsers = CType(cmb_To.DataSource, DataTable).Copy
            End If
            If Not IsNothing(dtUsers) Then
                dtUsers.Columns.RemoveAt(3)
                dtUsers.AcceptChanges()
            Else
                dtUsers = New DataTable()
                Dim dcnIntuitStaffMappingDetailID As New DataColumn("nIntuitStaffMappingDetailID")
                Dim dcnIntuitStaffMappingID As New DataColumn("nIntuitStaffMappingID")
                Dim dcId As New DataColumn("nUserID")
                Dim dcDescription As New DataColumn("Description")
                dtUsers.Columns.Add(dcnIntuitStaffMappingDetailID)
                dtUsers.Columns.Add(dcnIntuitStaffMappingID)
                dtUsers.Columns.Add(dcId)
            End If

            oclsgloIntuit = New clsgloIntuit
            oclsgloIntuit.SaveStaffMapping(_MappingID, _staffID, _Description, dtUsers)
            Me.Close()

        Catch ex As Exception
            If (ex.Message.Contains("IX_nStaffID")) Then
                MessageBox.Show("Staff ID already present. Enter a different StaffID.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            ElseIf (ex.Message.Contains("IX_sDescription")) Then
                MessageBox.Show("Staff Description already present. Enter a different Staff Description", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            Else
                MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Finally
            If Not IsNothing(oclsgloIntuit) Then
                oclsgloIntuit.Dispose()
                oclsgloIntuit = Nothing
            End If

        End Try
    End Sub

    Private Sub btnClearTestName_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearTestName.Click
        Try
           
            Dim dtusers As DataTable
            dtusers = CType(cmb_To.DataSource, DataTable)
            If Not IsNothing(dtusers) Then
                If dtusers.Rows.Count > 0 Then
                    Dim i As Integer
                    i = cmb_To.SelectedIndex
                    dtusers.Rows.RemoveAt(cmb_To.SelectedIndex)
                    dtusers.AcceptChanges()
                    cmb_To.DataSource = dtusers
                    _ToList = New gloGeneralItem.gloItems
                    Dim ToItem As gloGeneralItem.gloItem
                    For j As Int16 = 0 To dtusers.Rows.Count - 1
                        ToItem = New gloGeneralItem.gloItem()
                        ToItem.ID = dtusers.Rows(j)("nUserID")
                        ToItem.Description = dtusers.Rows(j)("Description")
                        _ToList.Add(ToItem)
                        ToItem = Nothing
                    Next
                    cmb_To.Refresh()
                    If i > 0 Then
                        cmb_To.SelectedIndex = 0
                    End If
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
    End Sub

    Private Function Validate_data() As Boolean
        Dim oclsgloIntuit As clsgloIntuit = New clsgloIntuit
        Try
            If txtstaffID.Text = "" Then
                MessageBox.Show("Please Enter Staff ID.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return True
            End If
            If IsNumeric(txtstaffID.Text) = False Then
                MessageBox.Show("Staff ID should be numeric.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return True
            End If
            If txtDescription.Text = "" Then
                MessageBox.Show("Please Enter Description.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return True
            End If
            If cmb_To.Items.Count = 0 Then
                If oclsgloIntuit.IsPatientPortalEnabled() Then
                    If btnSearchUser.Enabled <> False And btnClearTestName.Enabled <> False And cmb_To.Enabled <> False Then
                        MessageBox.Show("Please select user.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Return True
                    Else
                        Me.Close()
                    End If
                Else
                    MessageBox.Show("Please select user.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Return True
                End If
            End If
            If _MappingID > 0 Then
                Dim _checkmessage As Integer
                If txtstaffID.Text <> _staffID Then
                    oclsgloIntuit = New clsgloIntuit
                    _checkmessage = oclsgloIntuit.checkStaffIDAssociation(Convert.ToInt64(txtstaffID.Text))
                    If _checkmessage > 0 Then
                        If txtstaffID.Text <> _staffID Then
                            MessageBox.Show("Staff ID cannot be modified as it is used further.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Return True
                        End If
                    End If
                End If
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        Finally
            If Not IsNothing(oclsgloIntuit) Then
                oclsgloIntuit.Dispose()
                oclsgloIntuit = Nothing
            End If
        End Try
    End Function

    Private Sub txtstaffID_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtstaffID.KeyPress
        If (Microsoft.VisualBasic.Asc(e.KeyChar) < 48) _
              Or (Microsoft.VisualBasic.Asc(e.KeyChar) > 57) Then
            e.Handled = True
        End If
        If (Microsoft.VisualBasic.Asc(e.KeyChar) = 8) Then
            e.Handled = False
        End If
    End Sub
End Class