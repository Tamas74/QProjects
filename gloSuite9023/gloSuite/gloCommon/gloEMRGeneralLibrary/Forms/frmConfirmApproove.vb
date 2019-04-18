Imports gloSureScript

Public Class frmConfirmApproove
    Private dtMedication As DataTable
    Public Property nResponce As Int16 = 0
    Dim _Status As gloSureScriptInterface.SentMessageType
    Dim _sRxRequestType As String = Nothing

    Public Sub New(ByVal _dtMedication As DataTable, ByVal Status As gloSureScriptInterface.SentMessageType, ByVal sRxRequestType As String)
        MyBase.New()
        InitializeComponent()
        dtMedication = _dtMedication
        _Status = Status
        _sRxRequestType = sRxRequestType
    End Sub

    Private Sub frmConfirmApproove_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            Me.Text = _sRxRequestType & " Request vs Response"
            FilleRxFields(dtMedication)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub tlbbtnApprovewithChanges_Click(sender As System.Object, e As System.EventArgs) Handles tlbbtnApprovewithChanges.Click
        nResponce = 3
        Me.Close()
    End Sub

    Private Sub tlbbtnDTNF_Click(sender As System.Object, e As System.EventArgs) Handles tlbbtnDTNF.Click
        nResponce = 1
        Me.Close()
    End Sub

    Private Sub ts_btnClose_Click(sender As System.Object, e As System.EventArgs) Handles ts_btnClose.Click
        'nResponce = 0
        Me.Close()
    End Sub
    Private Sub FilleRxFields(ByVal dtMed As DataTable)
        Dim sLabelName As String = ""
        Dim sPrescribed As String = ""
        Dim sDespenced As String = ""
        Try
            For i As Int16 = 0 To dtMed.Rows.Count - 1
                sLabelName = Convert.ToString(dtMed.Rows(i)("Description")).Trim
                sPrescribed = Convert.ToString(dtMed.Rows(i)("MedicationPrescribed")).Trim
                sDespenced = Convert.ToString(dtMed.Rows(i)("MedicationDispenced")).Trim
                Select Case sLabelName
                    Case "Substitution flag"
                        lblSubstitution.Text = sPrescribed
                        lblMDSubstitution.Text = sDespenced
                        If sPrescribed <> sDespenced Then
                            lblSubstitution.ForeColor = Drawing.Color.Red
                        End If
                    Case "Pharmacy Notes"
                        lblNotes.Text = sPrescribed
                        lblMDNotes.Text = sDespenced
                        If sPrescribed <> sDespenced Then
                            lblNotes.ForeColor = Drawing.Color.Red
                        End If
                    Case "Pharmacy NCPDPID"
                        lblNCPDPId.Text = sPrescribed
                        lblMDNCPDPId.Text = sDespenced
                        If sPrescribed <> sDespenced Then
                            lblNCPDPId.ForeColor = Drawing.Color.Red
                        End If
                    Case "Drug Directions"
                        lblDirection.Text = sPrescribed
                        lblMDDirection.Text = sDespenced
                        If sPrescribed <> sDespenced Then
                            lblDirection.ForeColor = Drawing.Color.Red
                        End If
                    Case "Drug Quantity"
                        lblQty.Text = sPrescribed
                        lblMDQty.Text = sDespenced
                        If sPrescribed <> sDespenced Then
                            lblQty.ForeColor = Drawing.Color.Red
                        End If
                    Case "Drug Duration"
                        lblDuration.Text = sPrescribed
                        lblMDDuration.Text = sDespenced
                        If sPrescribed <> sDespenced Then
                            lblDuration.ForeColor = Drawing.Color.Red
                        End If
                    Case "Drug Potency Code"
                        lblPotencyCode.Text = sPrescribed
                        lblMDPotencyCode.Text = sDespenced
                        If sPrescribed <> sDespenced Then
                            lblPotencyCode.ForeColor = Drawing.Color.Red
                        End If
                    Case "Drug NDCCode"
                        lblNDCCode.Text = sPrescribed
                        lblMDNDCCode.Text = sDespenced
                        If sPrescribed <> sDespenced Then
                            lblNDCCode.ForeColor = Drawing.Color.Red
                        End If
                    Case "Drug"
                        lblDrug.Text = sPrescribed
                        lblMDDrug.Text = sDespenced
                        If sPrescribed.Trim.Replace(" ", "").ToUpper <> sDespenced.Trim.Replace(" ", "").ToUpper Then
                            lblDrug.ForeColor = Drawing.Color.Red
                        End If
                    Case "Refills"
                        If _Status = gloSureScriptInterface.SentMessageType.eApprovedWithChanges Then
                            If sPrescribed <> "" Then
                                If sPrescribed <> "0" Then
                                    If _sRxRequestType <> "RxChange" Then
                                        sPrescribed = Val(sPrescribed) - 1
                                    Else
                                        sPrescribed = Val(sPrescribed)
                                    End If

                                End If
                            End If
                        End If
                        lblRefills.Text = sPrescribed
                        lblMDRefills.Text = sDespenced
                        If sPrescribed <> sDespenced Then
                            lblRefills.ForeColor = Drawing.Color.Red
                        End If
                    Case "RefillQualifier"

                        If _Status = gloSureScriptInterface.SentMessageType.eDeniedWithNewRxToFollow Then
                            sPrescribed = "R"
                        End If
                        lblRefQlf.Text = sPrescribed
                        lblMDRefQlf.Text = sDespenced
                        If sPrescribed <> sDespenced Then
                            lblRefQlf.ForeColor = Drawing.Color.Red
                        End If
                    Case "NPI"    ' ' ------------------  Approve With changes 
                        lblNPI.Text = sPrescribed
                        lblMDNPI.Text = sDespenced
                        If sPrescribed <> sDespenced Then
                            lblNPI.ForeColor = Drawing.Color.Red
                        End If
                    Case "DEA"
                        lblDEA.Text = sPrescribed
                        lblMDDEA.Text = sDespenced
                        If sPrescribed <> sDespenced Then
                            lblDEA.ForeColor = Drawing.Color.Red
                        End If
                    Case "SSN"
                        lblSSN.Text = sPrescribed
                        lblMDSSN.Text = sDespenced
                        If sPrescribed <> sDespenced Then
                            lblSSN.ForeColor = Drawing.Color.Red
                        End If
                    Case "PrescriberAdd1"
                        lblAddress1.Text = sPrescribed
                        lblMDAddress1.Text = sDespenced
                        If sPrescribed <> sDespenced Then
                            lblAddress1.ForeColor = Drawing.Color.Red
                        End If
                    Case "PrescriberAdd2"
                        lblAddress2.Text = sPrescribed
                        lblMDAddress2.Text = sDespenced
                        If sPrescribed <> sDespenced Then
                            lblAddress2.ForeColor = Drawing.Color.Red
                        End If
                    Case "PrescriberCity"
                        lblCity.Text = sPrescribed
                        lblMDCity.Text = sDespenced
                        If sPrescribed <> sDespenced Then
                            lblCity.ForeColor = Drawing.Color.Red
                        End If
                    Case "PrescriberState"
                        lblState.Text = sPrescribed
                        lblMDState.Text = sDespenced
                        If sPrescribed <> sDespenced Then
                            lblState.ForeColor = Drawing.Color.Red
                        End If
                    Case "PrescriberZip"
                        lblZip.Text = sPrescribed
                        lblMDZip.Text = sDespenced
                        If sPrescribed <> sDespenced Then
                            lblZip.ForeColor = Drawing.Color.Red
                        End If
                    Case "PrescriberPhone"
                        lblPhone.Text = sPrescribed
                        lblMDPhone.Text = sDespenced
                        If sPrescribed <> sDespenced Then
                            lblPhone.ForeColor = Drawing.Color.Red
                        End If
                    Case "PrescriberFax"
                        lblFax.Text = sPrescribed
                        lblMDFax.Text = sDespenced
                        If sPrescribed <> sDespenced Then
                            lblFax.ForeColor = Drawing.Color.Red
                        End If
                    Case "PharmacyName"
                        lblPharmacyName.Text = sPrescribed
                        lblMDPharmacyName.Text = sDespenced
                        If sPrescribed.Trim.Replace(" ", "").ToUpper <> sDespenced.Trim.Replace(" ", "").ToUpper Then
                            lblPharmacyName.ForeColor = Drawing.Color.Red
                        End If

                End Select
            Next

            lblRequest.Text = _sRxRequestType & " Request"
            lblResponse.Text = _sRxRequestType & " Response"

        Catch ex As Exception
            Throw ex
        End Try
    End Sub


End Class