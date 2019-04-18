Imports System.Data.SqlClient
Imports System.IO

Public Class clsIntuit

    Public Function AddParameters(ByVal PatientId As Long, ByVal CommDetailId As Long, ByVal sFlag As Integer, ByVal sValue As String, ByVal dFromDate As DateTime, ByVal dToDate As DateTime, ByVal sSearchValue As String) As SqlCommand

        Dim objCmd As New SqlCommand
        Try

      
        objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "IntuitCommunication_New"

        Dim objParasrProviderId As New SqlParameter
        With objParasrProviderId
            .ParameterName = "@PatientId"
            .Direction = ParameterDirection.Input
            .Value = PatientId
            .SqlDbType = SqlDbType.BigInt
        End With
        objCmd.Parameters.Add(objParasrProviderId)

        Dim objParajrProviderId As New SqlParameter
        With objParajrProviderId
            .ParameterName = "@CommDetailId"
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.BigInt
            .Value = CommDetailId
        End With
        objCmd.Parameters.Add(objParajrProviderId)

        Dim objParasFlag As New SqlParameter
        With objParasFlag
            .ParameterName = "@sFlag"
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.Int
            .Value = sFlag
        End With
        objCmd.Parameters.Add(objParasFlag)


        Dim objParasvalue As New SqlParameter
        With objParasvalue
            .ParameterName = "@sValue"
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
            .Value = sValue
        End With
        objCmd.Parameters.Add(objParasvalue)

        Dim objFromDate As New SqlParameter
        With objFromDate
            .ParameterName = "@dFromDate"
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.DateTime
            .Value = dFromDate
        End With
        objCmd.Parameters.Add(objFromDate)

        Dim objToDate As New SqlParameter
        With objToDate
            .ParameterName = "@dToDate"
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.DateTime
            .Value = dToDate
        End With
        objCmd.Parameters.Add(objToDate)

        Dim objSearch As New SqlParameter
        With objSearch
            .ParameterName = "@sSearchValue"
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
            .Value = sSearchValue
        End With
        objCmd.Parameters.Add(objSearch)

            objParasrProviderId = Nothing
            objParajrProviderId = Nothing
            objParasFlag = Nothing
            objParasvalue = Nothing
            objFromDate = Nothing
            objToDate = Nothing
            objSearch = Nothing

            Return objCmd
        Catch ex As Exception
            Return objCmd
        Finally
            'If objCmd IsNot Nothing Then
            '    'objCmd.Parameters.Clear()
            '    objCmd.Dispose()
            '    objCmd = Nothing
            'End If
        End Try


    End Function

    Public Function GetIntuitMessageForPatient(ByVal PatientId As Long) As DataTable

        Dim objCon As New SqlConnection
        Dim objCmd As SqlCommand = Nothing

        'sFlag= 1.....Get Messages By Patient Id
        Try
            objCon.ConnectionString = mdlGeneral.GetConnectionString()
            objCmd = AddParameters(PatientId, 0, 1, "", System.DateTime.Now, System.DateTime.Now, "")
            objCmd.Connection = objCon
            objCon.Open()
            Dim objDA As New SqlDataAdapter(objCmd)
            Dim dt As New DataTable
            objDA.Fill(dt)
            objCon.Close()
            objDA.Dispose()
            objDA = Nothing
            Return dt
        Catch ex As Exception
            Return Nothing
        Finally
            objCon.Dispose()
            If objCmd IsNot Nothing Then
                objCmd.Parameters.Clear()
                objCmd.Dispose()
                objCmd = Nothing
            End If

        End Try

    End Function

    Public Function DeleteMessage(ByVal PatientId As Long, ByVal CommDetailID As Long) As Boolean
        'sFlag= 2.....Delete Message By Message Id
        Dim objCon As New SqlConnection
        Dim objCmd As SqlCommand = Nothing
        Try
            objCon.ConnectionString = mdlGeneral.GetConnectionString()
            objCmd = AddParameters(PatientId, CommDetailID, 2, 1, System.DateTime.Now, System.DateTime.Now, "")
            objCmd.Connection = objCon
            objCon.Open()
            Dim a As Integer = objCmd.ExecuteNonQuery()
            objCon.Close()
            If a >= 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Return False
        Finally
            objCon.Dispose()
            If objCmd IsNot Nothing Then
                objCmd.Parameters.Clear()
                objCmd.Dispose()
                objCmd = Nothing
            End If
        End Try
    End Function

    Public Function ReadUnreadIntuitMessage(ByVal PatientId As Long, ByVal CommDetailID As Long, ByVal sValue As String) As Boolean

        Dim objCon As New SqlConnection
        Dim objCmd As SqlCommand = Nothing

        Try
            'sFlag= 3.....Mark As Read Or Unread
            objCon.ConnectionString = mdlGeneral.GetConnectionString()
            objCmd = AddParameters(PatientId, CommDetailID, 3, sValue, System.DateTime.Now, System.DateTime.Now, "")
            objCmd.Connection = objCon
            objCon.Open()
            Dim a As Integer = objCmd.ExecuteNonQuery()
            objCon.Close()
            If a >= 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Return False
        Finally
            objCon.Dispose()
            If objCmd IsNot Nothing Then
                objCmd.Parameters.Clear()
                objCmd.Dispose()
                objCmd = Nothing
            End If
        End Try

    End Function

    Public Function GetIntuitMessageForPatient(ByVal PatientId As Long, ByVal FromDate As String, ByVal ToDate As String) As DataTable
        Dim objCon As New SqlConnection
        Dim objCmd As SqlCommand = Nothing
        'sFlag= 4.....Get Messages By Patient Id Between Date Range
        Try
            objCon.ConnectionString = mdlGeneral.GetConnectionString()
            objCmd = AddParameters(PatientId, 0, 4, "", FromDate, ToDate, "")
            objCmd.Connection = objCon
            objCon.Open()
            Dim objDA As New SqlDataAdapter(objCmd)
            Dim dt As New DataTable

            objDA.Fill(dt)
            objCon.Close()
            objDA.Dispose()
            objDA = Nothing

            Return dt
        Catch ex As Exception
            Return Nothing
        Finally
            objCon.Dispose()
            If objCmd IsNot Nothing Then
                objCmd.Parameters.Clear()
                objCmd.Dispose()
                objCmd = Nothing
            End If

        End Try

    End Function

    Public Function SearchMessageByDate(ByVal PatientId As Long, ByVal FromDate As String, ByVal ToDate As String, ByVal SearchValue As String) As DataTable

        Dim objCon As New SqlConnection
        Dim objCmd As SqlCommand = Nothing
        'sFlag= 2.....Delete Message By Message Id
        Try
            objCon.ConnectionString = mdlGeneral.GetConnectionString()
            objCmd = AddParameters(PatientId, 0, 5, "", FromDate, ToDate, SearchValue)
            objCmd.Connection = objCon
            objCon.Open()
            Dim objDA As New SqlDataAdapter(objCmd)
            Dim dt As New DataTable
            objDA.Fill(dt)
            objCon.Close()
            objDA.Dispose()
            objDA = Nothing
            Return dt
        Catch ex As Exception
            Return Nothing
        Finally
            objCon.Dispose()
            If objCmd IsNot Nothing Then
                objCmd.Parameters.Clear()
                objCmd.Dispose()
                objCmd = Nothing
            End If

        End Try
    End Function

    Public Function SearchMessage(ByVal PatientId As Long, ByVal SearchValue As String) As DataTable

        Dim objCon As New SqlConnection
        Dim objCmd As SqlCommand = Nothing
        'sFlag= 2.....Delete Message By Message Id
        Try
            objCon.ConnectionString = mdlGeneral.GetConnectionString()
            objCmd = AddParameters(PatientId, 0, 6, "", System.DateTime.Now, System.DateTime.Now, SearchValue)
            objCmd.Connection = objCon
            objCon.Open()
            Dim objDA As New SqlDataAdapter(objCmd)
            Dim dt As New DataTable
            objDA.Fill(dt)
            objCon.Close()
            objDA.Dispose()
            objDA = Nothing
            Return dt
        Catch ex As Exception
            Return Nothing
        Finally
            objCon.Dispose()
            If objCmd IsNot Nothing Then
                objCmd.Parameters.Clear()
                objCmd.Dispose()
                objCmd = Nothing
            End If

        End Try
    End Function
End Class
