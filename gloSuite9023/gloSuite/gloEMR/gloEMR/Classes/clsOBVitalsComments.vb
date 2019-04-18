Imports gloEMRGeneralLibrary.gloEMRDatabase
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO

Namespace gloOBVitals

    Public Class gloOBVitalsExceptions
        Inherits ApplicationException
        Implements IDisposable

        Private _ErrMessage As String

        Public Property ErrorMessage() As String
            Get
                Return _ErrMessage
            End Get
            Set(ByVal value As String)
                _ErrMessage = value
            End Set
        End Property

        Public Sub New()
            MyBase.New()
        End Sub

        Private disposedValue As Boolean = False        
        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                End If
            End If
            Me.disposedValue = True
        End Sub

#Region " IDisposable Support "

        Public Sub Dispose() Implements IDisposable.Dispose
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub
#End Region

    End Class
    Public Class ClsOBVitalsComment
        Implements IDisposable

        Dim _gloEMRDatabase As DataBaseLayer
        Private _Exception As gloOBVitalsExceptions
        Public Function AddModify(ByVal OBVitalComments_ID As Long, ByVal OBVitalComments As String)
            _gloEMRDatabase = New DataBaseLayer
            Dim objDBParameter As DBParameter
            Dim _OBVitalNotesID As Int64 = 0

            Try
                _gloEMRDatabase.DBParametersCol.Clear()
                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.BigInt
                objDBParameter.Value = OBVitalComments_ID
                objDBParameter.Name = "@OBVitalsComments_ID"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.VarChar
                objDBParameter.Value = OBVitalComments
                objDBParameter.Name = "@OBVitalsComments"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)


                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.InputOutput
                objDBParameter.DataType = SqlDbType.BigInt
                objDBParameter.Value = 0
                objDBParameter.Name = "@NewID"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                '-------------

                _OBVitalNotesID = _gloEMRDatabase.Add("OBVitals_InUpComments")

                Return _OBVitalNotesID
            Catch ex As Exception
                _Exception = New gloOBVitalsExceptions
                _Exception.ErrorMessage = ex.Message
                Throw _Exception
                _Exception.Dispose()
            Finally
                If Not IsNothing(_gloEMRDatabase) Then
                    _gloEMRDatabase.Dispose()
                    _gloEMRDatabase = Nothing
                End If
            End Try
        End Function

        Public Function Get_OBVitalComments(ByVal OBVitalsComments_ID As Long) As DataTable
            Dim dt As DataTable = Nothing
            Dim objDBParameter As DBParameter = Nothing

            Try
                _gloEMRDatabase = New DataBaseLayer
                _gloEMRDatabase.DBParametersCol.Clear()

                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.BigInt
                objDBParameter.Value = OBVitalsComments_ID
                objDBParameter.Name = "@OBVitalsComments_ID"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                objDBParameter = Nothing

                dt = _gloEMRDatabase.GetDataTable("OBVitals_SelectCommentsBy_ID")

                Return dt

            Catch ex As Exception
                Throw ex
                Return Nothing
            Finally
                If Not IsNothing(objDBParameter) Then
                    objDBParameter = Nothing
                End If

                If Not IsNothing(_gloEMRDatabase) Then
                    _gloEMRDatabase.Dispose()
                    _gloEMRDatabase = Nothing
                End If

            End Try
        End Function
        Public Function CheckDuplicate(ByVal ID As Long, ByVal OBComments As String) As Boolean

            Dim oDB As New DataBaseLayer
            Dim oParamater As DBParameter

            Try

                oParamater = New DBParameter
                oParamater.DataType = SqlDbType.BigInt
                oParamater.Direction = ParameterDirection.Input
                oParamater.Name = "@ID"
                oParamater.Value = ID
                oDB.DBParametersCol.Add(oParamater)
                oParamater = Nothing

                oParamater = New DBParameter
                oParamater.DataType = SqlDbType.VarChar
                oParamater.Direction = ParameterDirection.Input
                oParamater.Name = "@Comment"
                oParamater.Value = OBComments
                oDB.DBParametersCol.Add(oParamater)
                oParamater = Nothing

                Dim rowAffected As Int64
                rowAffected = CType(oDB.GetDataValue("gsp_CheckOBComments_MST"), Int64)
                If rowAffected > 0 Then
                    Return True
                Else
                    Return False
                End If
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                MessageBox.Show(ex.ToString, "OB Comments", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return Nothing
            Finally
                If Not IsNothing(oDB) Then
                    oDB.Dispose()
                    oDB = Nothing
                End If

            End Try
        End Function

        Public Function Delete(ByVal OBVitalsComments_ID As Long)
            _gloEMRDatabase = New DataBaseLayer
            Dim objDBParameter As DBParameter
            Dim _OBVitalsNotesID As Int64 = 0

            Try
                _gloEMRDatabase.DBParametersCol.Clear()
                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.BigInt
                objDBParameter.Value = OBVitalsComments_ID
                objDBParameter.Name = "@OBVitalsComments_ID"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)

                _OBVitalsNotesID = _gloEMRDatabase.Add("OBVitals_DeleteComment")

                Return _OBVitalsNotesID
            Catch ex As Exception
                _Exception = New gloOBVitalsExceptions
                _Exception.ErrorMessage = ex.Message
                Throw _Exception
                _Exception.Dispose()
            Finally
                If Not IsNothing(_gloEMRDatabase) Then
                    _gloEMRDatabase.Dispose()
                    _gloEMRDatabase = Nothing
                End If
            End Try
        End Function

#Region "IDisposable Support"
        Private disposedValue As Boolean ' To detect redundant calls
        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then

                End If

            End If
            Me.disposedValue = True
        End Sub


        Public Sub Dispose() Implements IDisposable.Dispose
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub
#End Region
    End Class

End Namespace
