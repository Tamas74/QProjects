Imports gloEMRGeneralLibrary.gloEMRDatabase
Imports System.Data.SqlClient
Imports gloEMRGeneralLibrary.gloEMRActors

Namespace gloGeneral

    Public Class EPABusinesslayer
        Implements IDisposable



        Public Sub New()
            MyBase.New()
        End Sub


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

        Public Function GetDiagnosis(ByVal ProblemIDs As String) As DataRow

            Dim _gloEMRDatabase As New DataBaseLayer()
            Dim objParameter As DBParameter
            Dim dt As DataTable = Nothing

            Try
                objParameter = New DBParameter
                objParameter.Value = ProblemIDs
                objParameter.Direction = ParameterDirection.Input
                objParameter.DataType = SqlDbType.VarChar
                objParameter.Name = "@sProblemIDs"
                _gloEMRDatabase.DBParametersCol.Add(objParameter)
                objParameter = Nothing

                dt = _gloEMRDatabase.GetDataTable("gsp_getDiagnosisCodes")
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    Return dt.Rows(0)
                Else
                    Return Nothing
                End If
            Catch ex As Exception
                Throw ex
            Finally
                If Not IsNothing(dt) Then
                    dt.Dispose()
                    dt = Nothing
                End If

                If Not IsNothing(_gloEMRDatabase) Then
                    _gloEMRDatabase.Dispose()
                    _gloEMRDatabase = Nothing
                End If

            End Try
            Return Nothing
        End Function

        Public Function ePA_GetPatientProviderDetails(ByVal nProviderID As Int64) As DataTable
            Dim oDB As DataBaseLayer = Nothing
            Dim oParameter As DBParameter = Nothing
            Dim dtprovider As DataTable = Nothing
            Try
                oDB = New DataBaseLayer
                oParameter = New DBParameter

                oParameter = New DBParameter
                oParameter.DataType = SqlDbType.BigInt
                oParameter.Direction = ParameterDirection.Input
                oParameter.Name = "@nProviderID"
                oParameter.Value = nProviderID
                oDB.DBParametersCol.Add(oParameter)
                oParameter = Nothing

                dtprovider = oDB.GetDataTable("ePA_GetPatientProviderDetails")
                Return dtprovider
            Catch ex As Exception
                Throw ex
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

        Public Function GetUserRole(ByVal UserID As Int64, ByVal ProviderID As Int64) As DataRow

            Dim _gloEMRDatabase As New DataBaseLayer()
            Dim objParameter As DBParameter
            Dim dt As DataTable = Nothing

            Try
                objParameter = New DBParameter
                objParameter.Value = UserID
                objParameter.Direction = ParameterDirection.Input
                objParameter.DataType = SqlDbType.BigInt
                objParameter.Name = "@nUserID"
                _gloEMRDatabase.DBParametersCol.Add(objParameter)
                objParameter = Nothing

                objParameter = New DBParameter
                objParameter.Value = ProviderID
                objParameter.Direction = ParameterDirection.Input
                objParameter.DataType = SqlDbType.BigInt
                objParameter.Name = "@nProviderID"
                _gloEMRDatabase.DBParametersCol.Add(objParameter)
                objParameter = Nothing

                dt = _gloEMRDatabase.GetDataTable("epa_getUserRole")
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    Return dt.Rows(0)
                Else
                    Return Nothing
                End If
            Catch ex As Exception
                Throw ex
            Finally
                If Not IsNothing(dt) Then
                    dt.Dispose()
                    dt = Nothing
                End If

                If Not IsNothing(_gloEMRDatabase) Then
                    _gloEMRDatabase.Dispose()
                    _gloEMRDatabase = Nothing
                End If

            End Try
            Return Nothing
        End Function

        Public Function GetProviderInRoles(ByVal UserID As Int64) As DataTable

            Dim _gloEMRDatabase As New DataBaseLayer()
            Dim objParameter As DBParameter
            Dim dt As DataTable = Nothing

            Try
                objParameter = New DBParameter
                objParameter.Value = UserID
                objParameter.Direction = ParameterDirection.Input
                objParameter.DataType = SqlDbType.BigInt
                objParameter.Name = "@nUserID"
                _gloEMRDatabase.DBParametersCol.Add(objParameter)
                objParameter = Nothing

                dt = _gloEMRDatabase.GetDataTable("epa_getProviderInRoles")
                Return dt
            Catch ex As Exception
                Throw ex
            Finally
                If Not IsNothing(dt) Then
                    dt.Dispose()
                    dt = Nothing
                End If

                If Not IsNothing(_gloEMRDatabase) Then
                    _gloEMRDatabase.Dispose()
                    _gloEMRDatabase = Nothing
                End If

            End Try
            Return Nothing
        End Function

        Public Function CheckEPAUserIsInRole(ByVal UserID As Int64) As DataTable

            Dim _gloEMRDatabase As New DataBaseLayer()
            Dim objParameter As DBParameter
            Dim dt As DataTable = Nothing

            Try
                objParameter = New DBParameter
                objParameter.Value = UserID
                objParameter.Direction = ParameterDirection.Input
                objParameter.DataType = SqlDbType.BigInt
                objParameter.Name = "@nUserID"
                _gloEMRDatabase.DBParametersCol.Add(objParameter)
                objParameter = Nothing

                dt = _gloEMRDatabase.GetDataTable("epa_checkUserRoles")
                Return dt
            Catch ex As Exception
                Throw ex
            Finally
                If Not IsNothing(dt) Then
                    dt.Dispose()
                    dt = Nothing
                End If

                If Not IsNothing(_gloEMRDatabase) Then
                    _gloEMRDatabase.Dispose()
                    _gloEMRDatabase = Nothing
                End If
            End Try
        End Function

        Public Function GetActiveEPAList(ByVal PatientID As Int64, ByVal ProviderID As Int64) As List(Of ActiveEPA)

            Dim _activeEPAList As New List(Of ActiveEPA)

            Dim _Epacombination As ActiveEPA
            Dim oDB As DataBaseLayer = Nothing
            Dim oParameter As DBParameter = Nothing
            Dim dtWorkflowProcesses As DataTable = Nothing
            Try
                oDB = New DataBaseLayer
                oParameter = New DBParameter
                oParameter = New DBParameter
                oParameter.DataType = SqlDbType.BigInt
                oParameter.Direction = ParameterDirection.Input
                oParameter.Name = "@nPatientID"
                oParameter.Value = PatientID
                oDB.DBParametersCol.Add(oParameter)
                oParameter = Nothing

                oParameter = New DBParameter
                oParameter.DataType = SqlDbType.BigInt
                oParameter.Direction = ParameterDirection.Input
                oParameter.Name = "@nProviderID"
                oParameter.Value = ProviderID
                oDB.DBParametersCol.Add(oParameter)
                oParameter = Nothing

                dtWorkflowProcesses = oDB.GetDataTable("ePA_GetActiveList")
                If Not IsNothing(dtWorkflowProcesses) AndAlso dtWorkflowProcesses.Rows.Count > 0 Then
                    For Each row As DataRow In dtWorkflowProcesses.Rows
                        _Epacombination = New ActiveEPA
                        _Epacombination.PatientID = Convert.ToInt64(row("nPatientID"))
                        _Epacombination.ProviderID = Convert.ToInt64(row("nProviderID"))
                        _Epacombination.PAReferenceID = Convert.ToString(row("PAReferenceID"))
                        _Epacombination.mpid = Convert.ToInt32(row("mpid"))
                        _Epacombination.ndc = Convert.ToString(row("ndc"))
                        _Epacombination.PBMMemberId = Convert.ToString(row("sPBMMemberId"))
                        _Epacombination.DaySupply = Convert.ToString(row("DaySupply"))
                        _Epacombination.Qty = Convert.ToString(row("qty"))
                        If Not String.IsNullOrWhiteSpace(Convert.ToString(row("dtExpirationDate"))) Then
                            _Epacombination.ExpirationDate = Convert.ToString(row("dtExpirationDate"))
                        End If
                        If Not String.IsNullOrWhiteSpace(Convert.ToString(row("Status"))) Then
                            _Epacombination.Status = Convert.ToString(row("Status"))
                        End If
                        If Not String.IsNullOrWhiteSpace(Convert.ToString(row("EPANumber"))) Then
                            _Epacombination.PANumber = Convert.ToString(row("EPANumber"))
                        End If

                        _activeEPAList.Add(_Epacombination)
                    Next
                Else
                    Return Nothing
                End If
                Return _activeEPAList
            Catch ex As SqlException
                Return Nothing
            Catch ex As Exception
                Return Nothing
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

        Public Function epa_IsManualPriorAuthorization(ByVal nPatientID As Int64, PAReferenceID As String) As Boolean
            Dim _gloEMRDatabase As New DataBaseLayer()
            Dim objParameter As DBParameter
            Dim IsManualPA As Boolean = Nothing

            Try
                objParameter = New DBParameter
                objParameter.Value = nPatientID
                objParameter.Direction = ParameterDirection.Input
                objParameter.DataType = SqlDbType.BigInt
                objParameter.Name = "@nPatientID"
                _gloEMRDatabase.DBParametersCol.Add(objParameter)
                objParameter = Nothing

                objParameter = New DBParameter
                objParameter.Value = PAReferenceID
                objParameter.Direction = ParameterDirection.Input
                objParameter.DataType = SqlDbType.BigInt
                objParameter.Name = "@PAReferenceID"
                _gloEMRDatabase.DBParametersCol.Add(objParameter)
                objParameter = Nothing

                IsManualPA = _gloEMRDatabase.GetDataValue("epa_IsManualPriorAuthorization")
                Return IsManualPA
            Catch ex As Exception
                Throw ex
            Finally
                If Not IsNothing(_gloEMRDatabase) Then
                    _gloEMRDatabase.Dispose()
                    _gloEMRDatabase = Nothing
                End If
            End Try
        End Function

        Public Sub ePA_INUPMaster(ByVal nPatientID As Int64, nProviderID As Int64, sPBMMemberID As String, nMPID As Int32, sNDCCode As String, PAReferenceID As Int64, sMessageID As String, sPAStatus As String, sDaysSupply As String, sQuantity As String, Optional ByVal epaNumber As String = "")
            Dim oDB As DataBaseLayer = Nothing
            Dim oParameter As DBParameter = Nothing

            Try
                oDB = New DataBaseLayer

                oParameter = New DBParameter
                oParameter.DataType = SqlDbType.BigInt
                oParameter.Direction = ParameterDirection.Input
                oParameter.Name = "@nPatientID"
                oParameter.Value = nPatientID
                oDB.DBParametersCol.Add(oParameter)
                oParameter = Nothing

                oParameter = New DBParameter
                oParameter.DataType = SqlDbType.BigInt
                oParameter.Direction = ParameterDirection.Input
                oParameter.Name = "@nProviderID"
                oParameter.Value = nProviderID
                oDB.DBParametersCol.Add(oParameter)
                oParameter = Nothing

                oParameter = New DBParameter
                oParameter.DataType = SqlDbType.BigInt
                oParameter.Direction = ParameterDirection.Input
                oParameter.Name = "@nPrescriberID"
                oParameter.Value = nProviderID
                oDB.DBParametersCol.Add(oParameter)
                oParameter = Nothing

                oParameter = New DBParameter
                oParameter.DataType = SqlDbType.VarChar
                oParameter.Direction = ParameterDirection.Input
                oParameter.Name = "@sPBMMemberID"
                oParameter.Value = sPBMMemberID
                oDB.DBParametersCol.Add(oParameter)
                oParameter = Nothing

                oParameter = New DBParameter
                oParameter.DataType = SqlDbType.Int
                oParameter.Direction = ParameterDirection.Input
                oParameter.Name = "@nMPID"
                oParameter.Value = nMPID
                oDB.DBParametersCol.Add(oParameter)
                oParameter = Nothing

                oParameter = New DBParameter
                oParameter.DataType = SqlDbType.VarChar
                oParameter.Direction = ParameterDirection.Input
                oParameter.Name = "@sNDCCode"
                oParameter.Value = sNDCCode
                oDB.DBParametersCol.Add(oParameter)
                oParameter = Nothing

                oParameter = New DBParameter
                oParameter.DataType = SqlDbType.VarChar
                oParameter.Direction = ParameterDirection.Input
                oParameter.Name = "@PAReferenceID"
                oParameter.Value = PAReferenceID
                oDB.DBParametersCol.Add(oParameter)
                oParameter = Nothing

                oParameter = New DBParameter
                oParameter.DataType = SqlDbType.VarChar
                oParameter.Direction = ParameterDirection.Input
                oParameter.Name = "@sMessageID"
                oParameter.Value = sMessageID
                oDB.DBParametersCol.Add(oParameter)
                oParameter = Nothing

                oParameter = New DBParameter
                oParameter.DataType = SqlDbType.VarChar
                oParameter.Direction = ParameterDirection.Input
                oParameter.Name = "@sDaysSupply"
                oParameter.Value = sDaysSupply
                oDB.DBParametersCol.Add(oParameter)
                oParameter = Nothing

                oParameter = New DBParameter
                oParameter.DataType = SqlDbType.VarChar
                oParameter.Direction = ParameterDirection.Input
                oParameter.Name = "@sQuantity"
                oParameter.Value = sQuantity
                oDB.DBParametersCol.Add(oParameter)
                oParameter = Nothing

                oParameter = New DBParameter
                oParameter.DataType = SqlDbType.VarChar
                oParameter.Direction = ParameterDirection.Input
                oParameter.Name = "@sPAStatus"
                oParameter.Value = sPAStatus
                oDB.DBParametersCol.Add(oParameter)
                oParameter = Nothing

                If Not String.IsNullOrWhiteSpace(epaNumber) Then
                    oParameter = New DBParameter
                    oParameter.Value = epaNumber
                    oParameter.Direction = ParameterDirection.Input
                    oParameter.DataType = SqlDbType.VarChar
                    oParameter.Name = "@EPANumber"
                    oDB.DBParametersCol.Add(oParameter)
                    oParameter = Nothing
                End If

                oDB.ExecuteNon_Query("epa_INUPMaster")
            Catch ex As Exception
                Throw ex
            Finally
                If Not IsNothing(oParameter) Then  'Obj Disposed by Mitesh
                    oParameter = Nothing
                End If
                If Not IsNothing(oDB) Then
                    oDB.Dispose()
                    oDB = Nothing
                End If
            End Try
        End Sub

        Public Function UpdateEPAStatusINDB(ByVal nproviderID As Int64, ByVal patientID As Int64, ByVal RefId As String, ByVal _dtexpirationDate As DateTime?, ByVal epaNumber As String, ByVal Status As String) As Boolean
            Dim _gloEMRDatabase As DataBaseLayer = Nothing
            _gloEMRDatabase = New DataBaseLayer
            Dim objParameter As DBParameter
            Try

                objParameter = New DBParameter
                objParameter.Value = patientID
                objParameter.Direction = ParameterDirection.Input
                objParameter.DataType = SqlDbType.BigInt
                objParameter.Name = "@nPatientID"
                _gloEMRDatabase.DBParametersCol.Add(objParameter)
                objParameter = Nothing

                objParameter = New DBParameter
                objParameter.Value = nproviderID
                objParameter.Direction = ParameterDirection.Input
                objParameter.DataType = SqlDbType.BigInt
                objParameter.Name = "@nProviderID"
                _gloEMRDatabase.DBParametersCol.Add(objParameter)
                objParameter = Nothing

                objParameter = New DBParameter
                objParameter.Value = RefId
                objParameter.Direction = ParameterDirection.Input
                objParameter.DataType = SqlDbType.VarChar
                objParameter.Name = "@nPAReferenceID"
                _gloEMRDatabase.DBParametersCol.Add(objParameter)
                objParameter = Nothing

                objParameter = New DBParameter
                objParameter.Value = Status
                objParameter.Direction = ParameterDirection.Input
                objParameter.DataType = SqlDbType.VarChar
                objParameter.Name = "@Status"
                _gloEMRDatabase.DBParametersCol.Add(objParameter)
                objParameter = Nothing

                If _dtexpirationDate.HasValue Then
                    objParameter = New DBParameter
                    objParameter.Value = _dtexpirationDate.Value
                    objParameter.Direction = ParameterDirection.Input
                    objParameter.DataType = SqlDbType.DateTime
                    objParameter.Name = "@dtexpirationDate"
                    _gloEMRDatabase.DBParametersCol.Add(objParameter)
                    objParameter = Nothing
                End If
                If Not String.IsNullOrWhiteSpace(epaNumber) Then
                    objParameter = New DBParameter
                    objParameter.Value = epaNumber
                    objParameter.Direction = ParameterDirection.Input
                    objParameter.DataType = SqlDbType.VarChar
                    objParameter.Name = "@EPANumber"
                    _gloEMRDatabase.DBParametersCol.Add(objParameter)
                    objParameter = Nothing
                End If

                _gloEMRDatabase.ExecuteNon_Query("epa_UpdateEPAStatus")
                Return True
            Catch ex As Exception
                Return False
            Finally
                If Not IsNothing(_gloEMRDatabase) Then
                    _gloEMRDatabase.Dispose()
                    _gloEMRDatabase = Nothing
                End If
            End Try
        End Function
    End Class

End Namespace
