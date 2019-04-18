Public Class frmAddPharmacyNotest

    Dim _nPatientId As Int64 = 0
    Dim _nPrescriptionId As Int64 = 0
    Dim _sPharmacyNotes As String = ""

    Public Sub New(ByVal PatientId As Int64, ByVal PrescriptionID As Int64, ByVal PharmacyNotes As String)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        _nPatientId = PatientId
        _nPrescriptionId = PrescriptionID
        _sPharmacyNotes = PharmacyNotes
        If _sPharmacyNotes <> "" Then
            txtPharmacyNotes.Text = _sPharmacyNotes
        End If
    End Sub

    'Private Sub frmAddPharmacyNotest_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    '    Try
    '        If _sPharmacyNotes <> "" Then
    '            txtPharmacyNotes.Text = _sPharmacyNotes
    '        End If
    '    Catch ex As Exception

    '    End Try
    'End Sub

    Private Sub tStrpSaveClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tStrpSaveClose.Click
        Dim oDB As New gloStream.gloDataBase.gloDataBase

        Dim strSQL As String
        Dim retval As Boolean
        Try

            _sPharmacyNotes = txtPharmacyNotes.Text
            If _sPharmacyNotes <> "" Then '''''validation
                txtPharmacyNotes.MaxLength = txtPharmacyNotes.MaxLength - _sPharmacyNotes.Length
                If txtPharmacyNotes.Text.Length > txtPharmacyNotes.MaxLength Then
                    MessageBox.Show("Maximum characters length has been exceeded.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    If (IsNothing(oDB) = False) Then
                        oDB.Disconnect()
                        oDB.Dispose()
                        oDB = Nothing
                    End If
                    Exit Sub
                End If
            Else

            End If

            'If txtPharmacyNotes.Text <> "" Then
            '    _sPharmacyNotes = _sPharmacyNotes & Environment.NewLine & txtPharmacyNotes.Text
            '    _sPharmacyNotes = ReplaceSpecialCharacters(_sPharmacyNotes)
            '    strSQL = "update prescription set sNotes = '" & _sPharmacyNotes & "' where nprescriptionid = " & _nPrescriptionId & " and npatientid = " & _nPatientId
            '    oDB.Connect(GetConnectionString)
            '    retval = oDB.ExecuteNonSQLQuery(strSQL)


            'Else

            'End If

            _sPharmacyNotes = txtPharmacyNotes.Text
            _sPharmacyNotes = ReplaceSpecialCharacters(_sPharmacyNotes)
            strSQL = "update prescription set sNotes = '" & _sPharmacyNotes & "' where nprescriptionid = " & _nPrescriptionId & " and npatientid = " & _nPatientId
            oDB.Connect(GetConnectionString)
            retval = oDB.ExecuteNonSQLQuery(strSQL)


            Me.Close()
        Catch ex As Exception
            If (IsNothing(oDB) = False) Then
                oDB.Disconnect()
                oDB.Dispose()
                oDB = Nothing
            End If
        Finally
            If (IsNothing(oDB) = False) Then
                oDB.Disconnect()
                oDB.Dispose()
                oDB = Nothing
            End If
           
        End Try
    End Sub

    
    Private Sub tlbbtnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tlbbtnClose.Click
        Me.Close()
    End Sub
End Class