Imports System.Data.SqlClient

Public Class frmAddLocationMapping
    Dim _MappingID As Long
    Dim _LocationID As Long
    Dim _IntuitLocationID As String
    Dim _bModifyMode As Boolean


    Public Property MappingID() As Long
        Get
            Return _MappingID
        End Get
        Set(ByVal value As Long)
            _MappingID = value
        End Set
    End Property

    Public Property LocationID() As Long
        Get
            Return _LocationID
        End Get
        Set(ByVal value As Long)
            _LocationID = value
        End Set
    End Property

    Public Property IntuitLocationID() As Long
        Get
            Return _IntuitLocationID
        End Get
        Set(ByVal value As Long)
            _IntuitLocationID = value
        End Set
    End Property


    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub frmAddLocationMapping_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        FillLocations()

        ''added code to remove "Intuit" from Label header in grid if Patient Portal is enable.
        Dim objSettings As New clsSettings
        Dim isPortalEnable As String = String.Empty
        objSettings.GetSetting("PatientPortalEnabled", gnLoginID, gnClinicID, isPortalEnable)

        If isPortalEnable.ToLower() = "true" Then
            lblLocationID.AutoSize = False
            lblLocationID.Text = "Location ID : "
            lblLocationID.TextAlign = ContentAlignment.TopRight
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
            cmbLocation.SelectedValue = _LocationID
            cmbLocation.Enabled = False
            TxtIntuitLocationID.Text = _IntuitLocationID

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
            _LocationID = Convert.ToInt64(cmbLocation.SelectedValue)
            _IntuitLocationID = TxtIntuitLocationID.Text


            oclsgloIntuit = New clsgloIntuit
            oclsgloIntuit.SaveLocationMapping(_MappingID, _LocationID, _IntuitLocationID)
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
            If cmbLocation.Text = "" Then
                MessageBox.Show("Please select Location name. ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                cmbLocation.Focus()
                Return True
            End If

            If TxtIntuitLocationID.Text = "" Then
                MessageBox.Show("Please enter Location id. ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                TxtIntuitLocationID.Focus()
                Return True
            End If
            Dim result As String = oclsgloIntuit.checkLocationMappingExists(cmbLocation.SelectedValue, TxtIntuitLocationID.Text, bModifyMode)

            If result <> "" Then
                MessageBox.Show(result, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                TxtIntuitLocationID.Focus()
                result = Nothing
                Return True
            End If


        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function



    Private Sub FillLocations()
        Dim dt As New DataTable
        Try
            dt = GetLocation()
            If dt IsNot Nothing Then
                cmbLocation.DataSource = dt
                cmbLocation.ValueMember = dt.Columns("nLocationID").ColumnName
                cmbLocation.DisplayMember = dt.Columns("LocationName").ColumnName
                cmbLocation.Refresh()
                cmbLocation.SelectedIndex = -1

            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
    End Sub

    Private Function GetLocation() As DataTable
        Try
            Dim odb As New gloStream.gloDataBase.gloDataBase
            Dim _sqlQuery As String = "SELECT ISNULL(nLocationID,0) AS  nLocationID , sLocation AS LocationName FROM  AB_Location WHERE nClinicID = 1 AND  sLocation <>'' And bIsblocked<>1 ORDER BY sLocation"
            Dim dt As New DataTable
            odb.Connect(gstrConnectionString)
            dt = odb.ReadQueryData(_sqlQuery)
            Return dt

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End Try
    End Function

    Private Sub txtIntuitLocationID_KeyPress(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs) Handles TxtIntuitLocationID.KeyPress
        If (Microsoft.VisualBasic.Asc(e.KeyChar) < 48) _
              Or (Microsoft.VisualBasic.Asc(e.KeyChar) > 57) Then
            e.Handled = True
        End If
        If (Microsoft.VisualBasic.Asc(e.KeyChar) = 8) Then
            e.Handled = False
        End If
    End Sub
End Class