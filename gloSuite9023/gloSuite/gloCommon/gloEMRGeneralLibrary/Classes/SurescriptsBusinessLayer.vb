Imports gloEMRGeneralLibrary.gloEMRDatabase
Imports System.Windows.Forms
Imports Schema = gloGlobal.Schemas.Surescript
Imports System.Globalization
Imports gloSureScript
Imports gloEMRGeneralLibrary.gloEMRActors
Imports gloEMRGeneralLibrary.gloGeneral

Public Class SurescriptsBusinessLayer
    Implements IDisposable

    Public Sub New()

    End Sub

    Public Function GetDenialReasonCodes() As DataTable

        Dim result As New DataTable()
        With result.Columns
            .Add("key")
            .Add("value")
        End With

        result.Rows.Add({"AA", "Patient Unknown to the Prescriber"})
        result.Rows.Add({"AB", "Patient never under Prescriber care"})
        result.Rows.Add({"AC", "Patient no longer under Prescriber care"})
        'result.Rows.Add({"AD", "Patient has requested refill too soon"})
        result.Rows.Add({"AE", "Medication never prescribed for the patient"})
        result.Rows.Add({"AF", "Patient should contact Prescriber first"})
        'result.Rows.Add({"AG", "Refill not appropriate"})
        result.Rows.Add({"AH", "Patient has picked up prescription"})
        result.Rows.Add({"AJ", "Patient has picked up partial fill of prescription"})
        result.Rows.Add({"AK", "Patient has not picked up prescription, drug returned to stock"})
        result.Rows.Add({"AL", "Change not appropriate"})
        result.Rows.Add({"AM", "Patient needs appointment"})
        result.Rows.Add({"AN", "Prescriber not associated with this practice or location"})
        result.Rows.Add({"AO", "No attempt will be made to obtain Prior Authorization"})
        result.Rows.Add({"AP", "Request already responded to by other means (e.g. phone or fax})"})
        result.Rows.Add({"AQ", "More medication history available"})
        Return result

    End Function

    Public Sub UpdateRxMessageStatus(ByVal MessageID As String, ByVal Status As String, ByVal MessageType As String)
        Dim dbHelper As DataBaseLayer = Nothing
        Dim param As DBParameter = Nothing

        Try

            Dim sStatus As String = String.Empty

            If MessageType = "RxFill" Then
                sStatus = Status
            ElseIf MessageType = "CancelRxResponse" Then
                sStatus = Status
            ElseIf MessageType = "RxChange" Then
                Select Case Status
                    Case "0"
                        sStatus = "Approved"
                    Case "1"
                        sStatus = "ApprovedWithChanges"
                    Case "2"
                        sStatus = "Denied"
                    Case "3"
                        sStatus = "DeniedWithNewRxToFollow"
                End Select
            End If

            dbHelper = New DataBaseLayer()

            param = New DBParameter
            param.Value = MessageID
            param.Direction = ParameterDirection.Input
            param.DataType = SqlDbType.VarChar
            param.Name = "@nMessageID"
            dbHelper.DBParametersCol.Add(param)
            param = Nothing

            param = New DBParameter
            param.Value = sStatus
            param.Direction = ParameterDirection.Input
            param.DataType = SqlDbType.VarChar
            param.Name = "@sStatus"
            dbHelper.DBParametersCol.Add(param)
            param = Nothing

            dbHelper.ExecuteNon_Query("gsp_UpdateRxMessageStatus")

        Catch ex As Exception
            Throw ex
        Finally
            If dbHelper IsNot Nothing Then
                dbHelper.Dispose()
                dbHelper = Nothing
            End If

            param = Nothing
        End Try
    End Sub

    Public Function GetPrescriptionByTransactionID(ByVal TransactionID As String) As Prescription
        Dim _gloEMRDatabase As DataBaseLayer = Nothing
        _gloEMRDatabase = New DataBaseLayer
        Dim param As DBParameter = Nothing
        Dim nTransactionId As Int64 = 0
        Dim objPrescription As Prescription = Nothing

        Try
            Dim dt As DataTable 
            Int64.TryParse(TransactionID, nTransactionId)

            param = New DBParameter
            param.Value = nTransactionId
            param.Direction = ParameterDirection.Input
            param.DataType = SqlDbType.BigInt
            param.Name = "@PrescriptionID"
            _gloEMRDatabase.DBParametersCol.Add(param)
            param = Nothing

            dt = _gloEMRDatabase.GetDataTable("gsp_GetPrescriptionByID", True)

            If (IsNothing(dt) = False) Then


                If dt.Rows.Count > 0 Then
                    objPrescription = New Prescription()
                    objPrescription.PatientID = dt.Rows(0)("PatientID")
                    objPrescription.Medication = dt.Rows(0)("Medication")
                    objPrescription.Dosage = dt.Rows(0)("Dosage")
                    objPrescription.Route = dt.Rows(0)("Route")
                    objPrescription.DosageForm = dt.Rows(0)("DrugForm")

                    ''For Resolving case no GLO2011-0013746  i.e eRx Refill Request Issues
                    objPrescription.NDCCode = dt.Rows(0)("NDCCode")
                    objPrescription.mpid = dt.Rows(0)("mpid")
                    objPrescription.DrugID = dt.Rows(0)("DrugID")
                    ''For Resolving Problem 00000208
                    ''Start
                    'objPrescription.UserName = dt.Rows(0)("UserName")
                    objPrescription.UserName = globalSecurity.gstrLoginName
                    'End
                    objPrescription.ChiefComplaint = dt.Rows(0)("ChiefComplaints")
                    objPrescription.Problems = dt.Rows(0)("ProblemID")
                    objPrescription.IsNarcotics = dt.Rows(0)("IsNarcotics")
                    objPrescription.Status = "Approved"
                    objPrescription.Renewed = "Renewed" & " " & Now & " " & globalSecurity.gstrLoginName
                    'objPrescription.RefillQuantity = RxRefillRequestQty
                    objPrescription.State = "A"
                    'objPrescriptions.Add(objPrescription) 'SLR: not used
                    objPrescription.PotencyCode = dt.Rows(0)("sPotencyCode")
                    objPrescription.PotencyUnit = dt.Rows(0)("sPotencyUnit")

                End If
                dt.Dispose()
                dt = Nothing
            End If
            Return objPrescription

        Catch ex As gloDBException
            Return Nothing
        Catch ex As gloEMRActorsException
            Return Nothing

        Finally
            _gloEMRDatabase.Dispose()
            _gloEMRDatabase = Nothing
        End Try
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        Me.disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
