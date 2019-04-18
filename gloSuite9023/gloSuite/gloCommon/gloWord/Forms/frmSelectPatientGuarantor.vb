''' <summary>
''' Form is used to select patient guarantor for pull data in data dictionary fields.
''' </summary>
''' <remarks></remarks>
Public Class frmSelectPatientGuarantor
    Public SelectedGuarantor As String
    Public SelectedAccount As Int64
    Dim isFormLoaded As Boolean = False
    Dim dtGuarantorAccount As DataTable = Nothing
    Dim _PatientID As Int64
    Dim _nClientID As Int64

    Private Sub frmSelectPatientGuarantor_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        For Each myForm As System.Windows.Forms.Form In System.Windows.Forms.Application.OpenForms
            If (myForm.TopMost) Then
                myForm.TopMost = False
            End If
        Next
        Me.TopMost = True
    End Sub

    Private Sub frmSelectPatientGuarantor_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        SelectedGuarantor = ""
        If IsNothing(dtGuarantorAccount) = False Then
            dtGuarantorAccount.Dispose()
            dtGuarantorAccount = Nothing
        End If
    End Sub

    Private Sub frmSelectPatientGuarantor_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        If (DialogResult <> Windows.Forms.DialogResult.OK) Then
            SelectedGuarantor = ""
            SelectedAccount = 0
        End If
        
        'DialogResult = Windows.Forms.DialogResult.OK
    End Sub

    Private Sub frmSelectPatientGuarantor_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            FillGuarantor()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub btnOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOk.Click
        Try
            SelectedGuarantor = cmbGuarantor.Text
            SelectedAccount = cmbGuarantor.SelectedValue
            DialogResult = Windows.Forms.DialogResult.OK
            Me.Close()
        Catch ex As Exception

        End Try
        
    End Sub
    Public Sub New(ByVal nPAtientID As Int64, ByVal nClinicID As Int64)
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        _PatientID = nPAtientID
        _nClientID = nClinicID

    End Sub

    Private Sub cmbGuarantor_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbGuarantor.SelectedIndexChanged
        Try
            If (isFormLoaded) Then
                SelectedGuarantor = cmbGuarantor.SelectedText
                SelectedAccount = cmbGuarantor.SelectedValue
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub FillGuarantor()
        Dim oClsSelectPatientGuarantor = New clsSelectPatientGuarantor(_PatientID, _nClientID)
        Try
            dtGuarantorAccount = oClsSelectPatientGuarantor.GetPatientAccounts(_PatientID, _nClientID)
            cmbGuarantor.DataSource = dtGuarantorAccount
            If (IsNothing(dtGuarantorAccount) = False) Then
                cmbGuarantor.DisplayMember = dtGuarantorAccount.Columns("sAccountNo").ColumnName
                cmbGuarantor.ValueMember = dtGuarantorAccount.Columns("nPAccountID").ColumnName
            End If
            isFormLoaded = True
            cmbGuarantor.SelectedIndex = 0
        Catch ex As Exception

        Finally
            If IsNothing(oClsSelectPatientGuarantor) = False Then
                oClsSelectPatientGuarantor.Dispose()
                oClsSelectPatientGuarantor = Nothing
            End If
        End Try
    End Sub
End Class