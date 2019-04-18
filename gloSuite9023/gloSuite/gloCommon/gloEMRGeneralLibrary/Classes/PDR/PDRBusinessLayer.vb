Imports gloEMRGeneralLibrary.gloEMRDatabase
Imports System.Data.SqlClient
Imports gloEMRGeneralLibrary.gloEMRActors
Imports gloGlobal.Schemas.PDR

Namespace gloGeneral

    Public Class PatientCommunicationBusinessLayer
        Implements IDisposable

        Public Sub New()
            MyBase.New()
        End Sub

#Region "PDR"

        'Public Function GetPatientCommunications(ByVal PatientID As Int64) As List(Of ActiveProgram)
        '    Dim ActiveComms As New List(Of ActiveProgram)

        '    Dim oDB As DataBaseLayer = Nothing
        '    Dim oParameter As DBParameter = Nothing
        '    Dim dtPatientCommunication As DataTable = Nothing
        '    Dim e As ActiveProgram = Nothing
        '    Try
        '        oDB = New DataBaseLayer
        '        oParameter = New DBParameter
        '        oParameter = New DBParameter
        '        oParameter.DataType = SqlDbType.BigInt
        '        oParameter.Direction = ParameterDirection.Input
        '        oParameter.Name = "@nPatientID"
        '        oParameter.Value = PatientID
        '        oDB.DBParametersCol.Add(oParameter)
        '        oParameter = Nothing

        '        dtPatientCommunication = oDB.GetDataTable("PDR_GetCommunications")

        '        If Not IsNothing(dtPatientCommunication) AndAlso dtPatientCommunication.Rows.Count > 0 Then
        '            For Each row As DataRow In dtPatientCommunication.Rows
        '                e = New ActiveProgram
        '                e.PrescriptionID = Convert.ToInt64(row("nPrescriptionID"))
        '                e.PBMMemberId = Convert.ToString(row("sPBMMemberID"))
        '                e.ndc = Convert.ToString(row("sNDCCode"))
        '                e.mpid = Convert.ToInt32(row("nMPID"))
        '                e.Refills = Convert.ToString(row("sRefills"))
        '                e.Qty = Convert.ToString(row("sQuantity"))
        '                e.DaySupply = Convert.ToString(row("sDaysSupplied"))
        '                e.SIG = Convert.ToString(row("sSig"))
        '                e.Dose = Convert.ToString(row("sDose"))
        '                e.PrescriptionForm = Convert.ToString(row("sPrescriptionForm"))
        '                e.TransactionID = Convert.ToInt64(row("nTransactionID"))
        '                e.ProgramID = Convert.ToString(row("nProgramID"))
        '                e.Paid = Convert.ToInt16(row("nPaid"))
        '                e.IsPrinted = Convert.ToInt16(row("bIsPrinted"))
        '                e.IsAcknowledged = Convert.ToInt16(row("bIsAcknowledged"))
        '                e.dtCreatedDate = Convert.ToDateTime(row("dtCreatedDate"))
        '                ActiveComms.Add(e)
        '            Next
        '        Else
        '            Return Nothing
        '        End If

        '        Return ActiveComms
        '    Catch ex As SqlException
        '        Return Nothing
        '    Catch ex As Exception
        '        Return Nothing
        '    Finally
        '        If Not IsNothing(oParameter) Then
        '            oParameter = Nothing

        '        End If
        '        If Not IsNothing(oDB) Then
        '            oDB.Dispose()
        '            oDB = Nothing
        '        End If
        '    End Try

        'End Function

        Public Sub UpdateAcknowledgementStatus(ByVal Programs As DataTable)

            Dim oDB As DataBaseLayer = Nothing
            Dim oParameter As DBParameter = Nothing
            Try
                oDB = New DataBaseLayer
                oParameter = New DBParameter

                oParameter = New DBParameter
                oParameter.DataType = SqlDbType.Structured
                oParameter.Direction = ParameterDirection.Input
                oParameter.Name = "@TVP"
                oParameter.Value = Programs
                oDB.DBParametersCol.Add(oParameter)
                oParameter = Nothing

                oDB.ExecuteNon_Query("PDR_UpdateAcknowledgementStatus")
            Catch ex As SqlException
                Throw ex
            Catch ex As Exception
                Throw ex
            Finally
                If Not IsNothing(oParameter) Then
                    oParameter = Nothing
                End If
                If Not IsNothing(oDB) Then
                    oDB.Dispose()
                    oDB = Nothing
                End If
            End Try

        End Sub

        Public Function UpdatePrintedStatus(ByVal Programs As DataTable) As Boolean

            Dim oDB As DataBaseLayer = Nothing
            Dim oParameter As DBParameter = Nothing
            Try
                oDB = New DataBaseLayer
                oParameter = New DBParameter

                oParameter = New DBParameter
                oParameter.DataType = SqlDbType.Structured
                oParameter.Direction = ParameterDirection.Input
                oParameter.Name = "@TVP"
                oParameter.Value = Programs
                oDB.DBParametersCol.Add(oParameter)
                oParameter = Nothing

                oDB.ExecuteNon_Query("PDR_UpdatePrintedStatus")
                Return True
            Catch ex As SqlException
                Throw ex
            Catch ex As Exception
                Throw ex
            Finally
                If Not IsNothing(oParameter) Then
                    oParameter = Nothing
                End If
                If Not IsNothing(oDB) Then
                    oDB.Dispose()
                    oDB = Nothing
                End If

            End Try

        End Function

        Public Function GetPatientImmunizations(ByVal nPatientID As Int64) As DataSet
            Dim oDB As DataBaseLayer = Nothing
            Dim oParameter As DBParameter = Nothing
            Dim dsImmunizations As New DataSet()
            Try
                oDB = New DataBaseLayer
                oParameter = New DBParameter

                oParameter = New DBParameter
                oParameter.DataType = SqlDbType.BigInt
                oParameter.Direction = ParameterDirection.Input
                oParameter.Name = "@nPatientID"
                oParameter.Value = nPatientID
                oDB.DBParametersCol.Add(oParameter)
                oParameter = Nothing

                dsImmunizations = oDB.GetDataSet("PDR_PC_GetImmunizations")

                Return dsImmunizations
            Catch ex As SqlException
                Throw ex
                Return Nothing
            Catch ex As Exception
                Throw ex
                Return Nothing
            Finally
                If Not IsNothing(oParameter) Then  'Obj Disposed by Mitesh
                    oParameter = Nothing
                End If
                If Not IsNothing(oDB) Then
                    oDB.Dispose()
                    oDB = Nothing
                End If
            End Try
        End Function

        Public Function GetPatientLabs(ByVal nPatientID As Int64, ByVal nVisitID As Int64) As DataSet
            Dim oDB As DataBaseLayer = Nothing
            Dim oParameter As DBParameter = Nothing
            Dim dsAllergies As New DataSet()
            Try
                oDB = New DataBaseLayer
                oParameter = New DBParameter

                oParameter = New DBParameter
                oParameter.DataType = SqlDbType.BigInt
                oParameter.Direction = ParameterDirection.Input
                oParameter.Name = "@nPatientID"
                oParameter.Value = nPatientID
                oDB.DBParametersCol.Add(oParameter)
                oParameter = Nothing

                oParameter = New DBParameter

                oParameter = New DBParameter
                oParameter.DataType = SqlDbType.BigInt
                oParameter.Direction = ParameterDirection.Input
                oParameter.Name = "@nVisitID"
                oParameter.Value = nVisitID
                oDB.DBParametersCol.Add(oParameter)
                oParameter = Nothing

                dsAllergies = oDB.GetDataSet("PDR_PC_GetLabs")

                Return dsAllergies
            Catch ex As SqlException
                Throw ex
                Return Nothing
            Catch ex As Exception
                Throw ex
                Return Nothing
            Finally
                If Not IsNothing(oParameter) Then  'Obj Disposed by Mitesh
                    oParameter = Nothing
                End If
                If Not IsNothing(oDB) Then
                    oDB.Dispose()
                    oDB = Nothing
                End If
            End Try
        End Function

        Public Function GetPatientVitals(ByVal nPatientID As Int64) As DataSet
            Dim oDB As DataBaseLayer = Nothing
            Dim oParameter As DBParameter = Nothing
            Dim dsHeightWeight As New DataSet()
            Try
                oDB = New DataBaseLayer
                oParameter = New DBParameter

                oParameter = New DBParameter
                oParameter.DataType = SqlDbType.BigInt
                oParameter.Direction = ParameterDirection.Input
                oParameter.Name = "@nPatientID"
                oParameter.Value = nPatientID
                oDB.DBParametersCol.Add(oParameter)
                oParameter = Nothing

                dsHeightWeight = oDB.GetDataSet("PDR_PC_GetPatientVitals")

                Return dsHeightWeight
            Catch ex As SqlException
                Throw ex
                Return Nothing
            Catch ex As Exception
                Throw ex
                Return Nothing
            Finally
                If Not IsNothing(oParameter) Then  'Obj Disposed by Mitesh
                    oParameter = Nothing
                End If
                If Not IsNothing(oDB) Then
                    oDB.Dispose()
                    oDB = Nothing
                End If
            End Try
        End Function

        Public Function GetClaimType(ByVal nPatientID As Int64) As DataSet
            Dim oDB As DataBaseLayer = Nothing
            Dim oParameter As DBParameter = Nothing
            Dim dsClaimType As New DataSet()
            Try
                oDB = New DataBaseLayer
                oParameter = New DBParameter

                oParameter = New DBParameter
                oParameter.DataType = SqlDbType.BigInt
                oParameter.Direction = ParameterDirection.Input
                oParameter.Name = "@nPatientID"
                oParameter.Value = nPatientID
                oDB.DBParametersCol.Add(oParameter)
                oParameter = Nothing

                dsClaimType = oDB.GetDataSet("PDR_GetClaimTypeByPatientID")

                Return dsClaimType
            Catch ex As SqlException
                Throw ex
                Return Nothing
            Catch ex As Exception
                Throw ex
                Return Nothing
            Finally
                If Not IsNothing(oParameter) Then  'Obj Disposed by Mitesh
                    oParameter = Nothing
                End If
                If Not IsNothing(oDB) Then
                    oDB.Dispose()
                    oDB = Nothing
                End If
            End Try
        End Function

        Public Function GetPatientDiagnosis(ByVal nPatientID As Int64, ByVal nVisitID As Int64) As DataSet
            Dim oDB As DataBaseLayer = Nothing
            Dim oParameter As DBParameter = Nothing
            Dim dsHeightWeight As New DataSet()
            Try
                oDB = New DataBaseLayer
                oParameter = New DBParameter

                oParameter = New DBParameter
                oParameter.DataType = SqlDbType.BigInt
                oParameter.Direction = ParameterDirection.Input
                oParameter.Name = "@nPatientID"
                oParameter.Value = nPatientID
                oDB.DBParametersCol.Add(oParameter)
                oParameter = Nothing

                oParameter = New DBParameter

                oParameter = New DBParameter
                oParameter.DataType = SqlDbType.BigInt
                oParameter.Direction = ParameterDirection.Input
                oParameter.Name = "@nVisitID"
                oParameter.Value = nVisitID
                oDB.DBParametersCol.Add(oParameter)
                oParameter = Nothing

                dsHeightWeight = oDB.GetDataSet("PDR_PC_GetDiagnosis")

                Return dsHeightWeight
            Catch ex As SqlException
                Throw ex
                Return Nothing
            Catch ex As Exception
                Throw ex
                Return Nothing
            Finally
                If Not IsNothing(oParameter) Then  'Obj Disposed by Mitesh
                    oParameter = Nothing
                End If
                If Not IsNothing(oDB) Then
                    oDB.Dispose()
                    oDB = Nothing
                End If
            End Try
        End Function

        Public Function GetPDRPrograms(ByVal nPatientID As Int64, ByVal FromDate As Date, ByVal ToDate As Date) As DataSet
            Dim oDB As DataBaseLayer = Nothing
            Dim oParameter As DBParameter = Nothing
            Dim dsReturned As New DataSet()

            Try
                oDB = New DataBaseLayer
                oParameter = New DBParameter

                oParameter = New DBParameter
                oParameter.DataType = SqlDbType.BigInt
                oParameter.Direction = ParameterDirection.Input
                oParameter.Name = "@nPatientID"
                oParameter.Value = nPatientID
                oDB.DBParametersCol.Add(oParameter)
                oParameter = Nothing

                oParameter = New DBParameter

                oParameter = New DBParameter
                oParameter.DataType = SqlDbType.Date
                oParameter.Direction = ParameterDirection.Input
                oParameter.Name = "@dtFrom"
                oParameter.Value = FromDate
                oDB.DBParametersCol.Add(oParameter)
                oParameter = Nothing

                oParameter = New DBParameter

                oParameter = New DBParameter
                oParameter.DataType = SqlDbType.Date
                oParameter.Direction = ParameterDirection.Input
                oParameter.Name = "@dtTo"
                oParameter.Value = ToDate
                oDB.DBParametersCol.Add(oParameter)
                oParameter = Nothing

                dsReturned = oDB.GetDataSet("PDR_GetPrograms")

                Return dsReturned
            Catch ex As SqlException
                Throw ex
                Return Nothing
            Catch ex As Exception
                Throw ex
                Return Nothing
            Finally
                If Not IsNothing(oParameter) Then  'Obj Disposed by Mitesh
                    oParameter = Nothing
                End If
                If Not IsNothing(oDB) Then
                    oDB.Dispose()
                    oDB = Nothing
                End If
            End Try
        End Function

        Public Function GetPDRProgram(ByVal nID As Int64) As String
            Dim oDB As DataBaseLayer = Nothing
            Dim oParameter As DBParameter = Nothing
            Dim dsReturned As New DataSet()
            Dim sReturned As String = ""
            Try
                oDB = New DataBaseLayer
                oParameter = New DBParameter

                oParameter = New DBParameter
                oParameter.DataType = SqlDbType.BigInt
                oParameter.Direction = ParameterDirection.Input
                oParameter.Name = "@nID"
                oParameter.Value = nID
                oDB.DBParametersCol.Add(oParameter)
                oParameter = Nothing

                dsReturned = oDB.GetDataSet("PDR_GetProgram")

                If dsReturned.Tables.Count > 0 AndAlso dsReturned.Tables(0).Rows.Count > 0 Then
                    Dim dRow As DataRow = dsReturned.Tables(0).Rows(0)
                    sReturned = Convert.ToString(dRow("sContent"))
                    dRow = Nothing
                End If

                Return sReturned
            Catch ex As SqlException
                Throw ex
                Return Nothing
            Catch ex As Exception
                Throw ex
                Return Nothing
            Finally
                If Not IsNothing(oParameter) Then  'Obj Disposed by Mitesh
                    oParameter = Nothing
                End If
                If Not IsNothing(oDB) Then
                    oDB.Dispose()
                    oDB = Nothing
                End If
            End Try
        End Function

        Function GetAllPrograms(nTransactionID As Long, Optional ByVal Attribute As String = "") As ProgramResponse
            Dim Programs As New ProgramResponse
            Programs.programs = New List(Of Program)


            Dim oDB As DataBaseLayer = Nothing
            Dim oParameter As DBParameter = Nothing
            Dim dsReturned As New DataSet()
            Dim sReturned As String = ""
            Try
                oDB = New DataBaseLayer
                oParameter = New DBParameter

                oParameter = New DBParameter
                oParameter.DataType = SqlDbType.BigInt
                oParameter.Direction = ParameterDirection.Input
                oParameter.Name = "@nTransactionID"
                oParameter.Value = nTransactionID
                oDB.DBParametersCol.Add(oParameter)
                oParameter = Nothing

                If Not String.IsNullOrEmpty(Attribute) Then
                    oParameter = New DBParameter
                    oParameter.DataType = SqlDbType.VarChar
                    oParameter.Direction = ParameterDirection.Input
                    oParameter.Name = "@Atribute"
                    oParameter.Value = Attribute
                    oDB.DBParametersCol.Add(oParameter)
                    oParameter = Nothing
                End If

                dsReturned = oDB.GetDataSet("PDR_GetAllPrograms")

                If dsReturned.Tables.Count > 0 AndAlso dsReturned.Tables(0).Rows.Count > 0 Then
                    For Each row As DataRow In dsReturned.Tables(0).Rows
                        Dim _program As New Program()
                        _program.id = row("nProgramID")
                        _program.name = row("sProgramName")
                        _program.paymentNotes = row("sPaymentNotes")
                        _program.image = row("sContent")

                        If String.IsNullOrWhiteSpace(Programs.RxNumber) Then
                            Programs.RxNumber = row("nRxNumber")
                        End If

                        If String.IsNullOrWhiteSpace(Programs.TransactionID) Then
                            Programs.TransactionID = row("nTransactionID")
                        End If

                        Programs.Programs.Add(_program)
                    Next
                End If

                Return Programs
            Catch ex As SqlException
                Throw ex
                Return Nothing
            Catch ex As Exception
                Throw ex
                Return Nothing
            Finally
                If Not IsNothing(dsReturned) Then
                    dsReturned.Dispose()
                    dsReturned = Nothing
                End If
                If Not IsNothing(oParameter) Then
                    oParameter = Nothing
                End If
                If Not IsNothing(oDB) Then
                    oDB.Dispose()
                    oDB = Nothing
                End If
            End Try

            Return Programs
        End Function
#End Region


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

End Namespace