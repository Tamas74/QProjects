Imports gloEMRGeneralLibrary.gloEMRActors
Imports gloEMRGeneralLibrary.gloEMRDatabase
Namespace gloGeneral
    Public Class VisitBusinessLayer
        Implements IDisposable
        Private disposedValue As Boolean = False        ' To detect redundant calls
        Private _Visit As Visit
        Private _VisitID As Int64
        Public Property VisitID() As Int64
            Get
                Return _VisitID
            End Get
            Set(ByVal value As Int64)
                _VisitID = value
            End Set
        End Property
        Public Property VisitObject() As Visit
            Get
                Return _Visit
            End Get
            Set(ByVal value As Visit)
                _Visit = value
            End Set
        End Property
        ' IDisposable
        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    ' TODO: free unmanaged resources when explicitly called
                End If

                ' TODO: free shared unmanaged resources
            End If
            Me.disposedValue = True
        End Sub

#Region " IDisposable Support "
        ' This code added by Visual Basic to correctly implement the disposable pattern.
        Public Sub Dispose() Implements IDisposable.Dispose
            ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub
#End Region
        Public Sub New()
            MyBase.New()
        End Sub
        Public Function InsertVisit() As Boolean
            Dim _VisitDBLayer As New VisitDatabaseLayer
            Try
                Return _VisitDBLayer.InsertVisit(_Visit)
            Catch ex As Exception
                Return Nothing
            Finally
                _VisitDBLayer = Nothing
            End Try
        End Function
    End Class
    Friend Class VisitDatabaseLayer
        Implements IDisposable
        Private disposedValue As Boolean = False        ' To detect redundant calls
        Public Sub New()
            MyBase.New()
        End Sub
        ' IDisposable
        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    ' TODO: free unmanaged resources when explicitly called
                End If

                ' TODO: free shared unmanaged resources
            End If
            Me.disposedValue = True
        End Sub
#Region " IDisposable Support "
        ' This code added by Visual Basic to correctly implement the disposable pattern.
        Public Sub Dispose() Implements IDisposable.Dispose
            ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub
#End Region
        Friend Function InsertVisit(ByRef _visit As Visit) As Boolean
            Dim _gloEMRDatabase As New gloEMRDatabase.DataBaseLayer
            Dim _DBParameter As DBParameter
            Try
                _DBParameter = New DBParameter
                _DBParameter.Value = _visit.PatientID
                _DBParameter.Direction = ParameterDirection.Input
                _DBParameter.DataType = SqlDbType.BigInt
                _DBParameter.Name = "@nPatientID"
                _gloEMRDatabase.DBParametersCol.Add(_DBParameter)

                _DBParameter = New DBParameter
                _DBParameter.Value = _visit.VisitDate
                _DBParameter.Direction = ParameterDirection.Input
                _DBParameter.DataType = SqlDbType.DateTime
                _DBParameter.Name = "@dtVisitdate"
                _gloEMRDatabase.DBParametersCol.Add(_DBParameter)

                'Retrieve Appointment ID
                'Dim nAppointmentID As Long
                'Dim objAppointmentID As New clsAppointments
                'nAppointmentID = objAppointmentID.GetPatientAppointment(System.DateTime.Now, gnPatientID)
                'objAppointmentID = Nothing

                _DBParameter = New DBParameter
                'Code to retrieve appointment Id has been commented
                'temporarily we pass 1
                _DBParameter.Value = 1
                _DBParameter.Direction = ParameterDirection.Input
                _DBParameter.DataType = SqlDbType.BigInt
                _DBParameter.Name = "@AppointmentID"
                _gloEMRDatabase.DBParametersCol.Add(_DBParameter)

                _DBParameter = New DBParameter
                _DBParameter.Value = clsgeneral.GetPrefixTransactionID(_visit.PatientID)
                _DBParameter.Direction = ParameterDirection.Input
                _DBParameter.DataType = SqlDbType.BigInt
                _DBParameter.Name = "@MachineID"
                _gloEMRDatabase.DBParametersCol.Add(_DBParameter)

                _DBParameter = New DBParameter
                _DBParameter.Direction = ParameterDirection.Output
                _DBParameter.DataType = SqlDbType.BigInt
                _DBParameter.Value = 0
                _DBParameter.Name = "@VisitID"
                _gloEMRDatabase.DBParametersCol.Add(_DBParameter)

                _DBParameter = New DBParameter
                _DBParameter.Direction = ParameterDirection.Output
                _DBParameter.DataType = SqlDbType.Int
                _DBParameter.Name = "@flag"
                _gloEMRDatabase.DBParametersCol.Add(_DBParameter)
                _visit.VisitID = _gloEMRDatabase.Add1("gsp_InsertVisits")
                'If Not IsDBNull(objParam.Value) Then
                '    visitid = objParam.Value
                'End If
                'If objflagParam.Value = 0 Then
                '    Dim objAudit As New clsAudit
                '    objAudit.CreateLog(clsAudit.enmActivityType.Add, "Visit Added on " & CType(Now, String), gstrLoginName, gstrClientMachineName, gnPatientID)
                '    objAudit = Nothing
                'End If
                Return True
            Catch ex As gloDBException
                Return Nothing
            Catch ex As Exception
                Return Nothing
            Finally
                _gloEMRDatabase.Dispose()
                _gloEMRDatabase = Nothing
            End Try
        End Function
    End Class
End Namespace