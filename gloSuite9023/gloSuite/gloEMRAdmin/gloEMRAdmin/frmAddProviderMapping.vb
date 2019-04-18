Imports System.Data.SqlClient

Public Class frmAddProviderMapping
    Dim _MappingID As Long
    Dim _ProviderID As Long
    Dim _IntuitProviderID As String
    Dim _bModifyMode As Boolean
    

    Public Property MappingID() As Long
        Get
            Return _MappingID
        End Get
        Set(ByVal value As Long)
            _MappingID = value
        End Set
    End Property

    Public Property ProviderID() As Long
        Get
            Return _ProviderID
        End Get
        Set(ByVal value As Long)
            _ProviderID = value
        End Set
    End Property

    Public Property IntuitProviderID() As Long
        Get
            Return _IntuitProviderID
        End Get
        Set(ByVal value As Long)
            _IntuitProviderID = value
        End Set
    End Property


    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub frmAddProviderMapping_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        FillProviders()

        ''added code to remove "Intuit" from Label header in grid if Patient Portal is enable.
        Dim objSettings As New clsSettings
        Dim isPortalEnable As String = String.Empty
        objSettings.GetSetting("PatientPortalEnabled", gnLoginID, gnClinicID, isPortalEnable)

        If isPortalEnable.ToLower() = "true" Then
            lblProviderID.AutoSize = False
            lblProviderID.Text = "Provider ID :"
            lblProviderID.TextAlign = ContentAlignment.TopRight
        End If
        objSettings = Nothing

        If _MappingID > 0 Then
            _bModifyMode = True
            loadControls()
        Else
            _bModifyMode = False
        End If
    End Sub

    
    Private Sub loadControls()
        Try
            cmbProvider.SelectedValue = _ProviderID
            cmbProvider.Enabled = False
            txtIntuitProviderID.Text = _IntuitProviderID
            ' FillMappedUsers()
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        Dim oclsgloIntuit As New clsgloIntuit
        Try
            If Validate_data(_bModifyMode) = True Then
                Exit Sub
            End If
            _ProviderID = Convert.ToInt64(cmbProvider.SelectedValue)
            _IntuitProviderID = txtIntuitProviderID.Text
           

            oclsgloIntuit = New clsgloIntuit
            oclsgloIntuit.SaveProviderMapping(_MappingID, _ProviderID, _IntuitProviderID)
            Me.Close()

        Catch ex As Exception
            'If (ex.Message.Contains("IX_nStaffID")) Then
            '    MessageBox.Show("Staff ID already present. Enter a different StaffID.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            '    Exit Sub
            'ElseIf (ex.Message.Contains("IX_sDescription")) Then
            '    MessageBox.Show("Staff Description already present. Enter a different Staff Description", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            '    Exit Sub
            'Else
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            'End If
        Finally
            If Not IsNothing(oclsgloIntuit) Then
                oclsgloIntuit.Dispose()
                oclsgloIntuit = Nothing
            End If

        End Try
    End Sub


    Private Function Validate_data(ByVal bModifyMode As Boolean) As Boolean
        Dim oclsgloIntuit As New clsgloIntuit
        Try
            If cmbProvider.Text = "" Then
                MessageBox.Show("Please select provider name. ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                cmbProvider.Focus()
                Return True
            End If

            If txtIntuitProviderID.Text = "" Then
                MessageBox.Show("Please enter intuit provider id. ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtIntuitProviderID.Focus()
                Return True
            End If
            Dim result As String = oclsgloIntuit.checkProviderMappingExists(cmbProvider.SelectedValue, txtIntuitProviderID.Text, bModifyMode)

            If result <> "" Then
                MessageBox.Show(result, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtIntuitProviderID.Focus()
                result = Nothing
                Return True
            End If


            'If _MappingID > 0 Then
            '    Dim _checkmessage As Integer
            '    If txtstaffID.Text <> _ProviderID Then
            '        oclsgloIntuit = New clsgloIntuit
            '        _checkmessage = oclsgloIntuit.checkStaffIDAssociation(Convert.ToInt64(txtstaffID.Text))
            '        If _checkmessage > 0 Then
            '            If txtstaffID.Text <> _ProviderID Then
            '                MessageBox.Show("Staff ID cannot be modified as it is used further.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            '                Return True
            '            End If
            '        End If
            '    End If
            'End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function

    'Private Sub txtstaffID_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
    '    If (Microsoft.VisualBasic.Asc(e.KeyChar) < 48) _
    '          Or (Microsoft.VisualBasic.Asc(e.KeyChar) > 57) Then
    '        e.Handled = True
    '    End If
    '    If (Microsoft.VisualBasic.Asc(e.KeyChar) = 8) Then
    '        e.Handled = False
    '    End If
    'End Sub

    Private Sub FillProviders()
        Dim dt As New DataTable
        Try
            dt = GetProvider()
            If dt IsNot Nothing Then
                cmbProvider.DataSource = dt
                cmbProvider.ValueMember = dt.Columns("nProviderID").ColumnName
                cmbProvider.DisplayMember = dt.Columns("ProviderName").ColumnName
                cmbProvider.Refresh()
                cmbProvider.SelectedIndex = -1

            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
       
        End Try
    End Sub

    Private Function GetProvider() As DataTable
        Try
            Dim odb As New gloStream.gloDataBase.gloDataBase
            Dim _sqlQuery As String = "SELECT ISNULL(nProviderID,0) AS  nProviderID , (ISNULL(sFirstName,'')+ SPACE(1) + ISNULL(sMiddleName,'') + SPACE(1) +ISNULL(sLastName,'')) AS ProviderName FROM  Provider_MST WHERE nClinicID = 1 And bIsblocked<>1  ORDER BY ProviderName"
            Dim dt As New DataTable
            odb.Connect(gstrConnectionString)
            dt = odb.ReadQueryData(_sqlQuery)
            Return dt

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End Try
    End Function

    Private Sub txtIntuitProviderID_KeyPress(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtIntuitProviderID.KeyPress
        If (Microsoft.VisualBasic.Asc(e.KeyChar) < 48) _
              Or (Microsoft.VisualBasic.Asc(e.KeyChar) > 57) Then
            e.Handled = True
        End If
        If (Microsoft.VisualBasic.Asc(e.KeyChar) = 8) Then
            e.Handled = False
        End If
    End Sub
End Class