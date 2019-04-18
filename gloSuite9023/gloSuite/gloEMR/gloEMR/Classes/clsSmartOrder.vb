Imports System.Data
Imports System.Data.SqlClient
Public Class clsSmartOrder
    ' Private Cmd As SqlCommand
    Private Conn As SqlConnection = Nothing

    Public Function FillOrder(Optional ByVal Flag As Int16 = 0) As DataTable
        Dim adpt As New SqlDataAdapter
        Dim dt As New DataTable

        'Conn = New SqlConnection(GetConnectionString())

        Dim Cmd As SqlCommand = New SqlCommand("gsp_FillOderset", Conn)
        Cmd.CommandType = CommandType.StoredProcedure
        Dim objparam As SqlParameter
        objparam = Cmd.Parameters.Add("@Flag", SqlDbType.Int)
        objparam.Direction = ParameterDirection.Input
        objparam.Value = 0
        '''''' if Flag=0 then Orderby ICD9COde
        ''''''''Else Orderby ICD9Description

        If Conn.State = ConnectionState.Closed Then
            Conn.Open()
        End If
        adpt.SelectCommand = Cmd

        adpt.Fill(dt)
        Conn.Close()
        If Cmd IsNot Nothing Then
            Cmd.Parameters.Clear()
            Cmd.Dispose()
            Cmd = Nothing
        End If
        adpt.Dispose()
        adpt = Nothing
        objparam = Nothing
        Return dt

        'Dim dreader As SqlDataReader
        'Conn.Open()
        'dreader = Cmd.ExecuteReader()

        'Do While dreader.Read
        '    Dim i As Integer
        '    i = dreader("nSpecialtyID")

        'Loop
    End Function

    Public Function FetchSmartOrder() As DataTable
        ''''''''''''''' Added by Ujwala - Smart Order Changes Integration - as on 20101016
        Dim _query As String
        Dim dt As DataTable = Nothing
        Dim objSDA As SqlDataAdapter
        '  Dim _Result As Object

        Try
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            ''_query = "select nUserID, sFieldName, bFieldStatus, nFieldSequence, sFieldType, bSendTask from SmartConfig where nUserId='" & gnLoginID & "' AND sFieldType = 'SmartOrder' Order by nFieldSequence"
            _query = "select ISNULL(nUserID,0) AS nUserID, ISNULL(sFieldName,'') AS sFieldName, ISNULL(bFieldStatus,'true') AS bFieldStatus, ISNULL(nFieldSequence,0) AS nFieldSequence, ISNULL(sFieldType,'') AS sFieldType, ISNULL(bSendTask,'false') AS bSendTask,ISNULL(sTaskusers,'') as sTaskusers,ISNULL(bAllowviewtsk,0) as bAllowviewtsk,ISNULL(sUserID,'') as sUserID  from SmartConfig where nUserId='" & gnLoginID & "' AND sFieldType = 'SmartOrder' Order by nFieldSequence"
            objSDA = New SqlDataAdapter(_query, Conn)
            dt = New DataTable()
            objSDA.Fill(dt)
            objSDA.Dispose()
            objSDA = Nothing
        Catch ex As Exception
        Finally

            If Not IsNothing(Conn) Then
                If (Conn.State = ConnectionState.Open) Then
                    Conn.Close()
                End If
            End If
        End Try

        Return dt
        ''''''''''''''' Added by Ujwala - Smart Order Changes Integration - as on 20101016
    End Function

    Public Sub New()
        ''''''''''''''' Added by Ujwala - Smart Order Changes Integration - as on 20101016
        Dim sqlconn As String
        sqlconn = GetConnectionString()
        Conn = New System.Data.SqlClient.SqlConnection(sqlconn)
        ''''''''''''''' Added by Ujwala - Smart Order Changes Integration - as on 20101016
    End Sub
    Public Sub Dispose()

        If Conn IsNot Nothing Then
            Conn.Dispose()
            Conn = Nothing

        End If
        'If IsNothing(dv) = False Then
        '    dv.Dispose()
        '    dv = Nothing
        'End If
        'If IsNothing(Ds) = False Then
        '    Ds.Dispose()
        '    Ds = Nothing
        'End If
    End Sub
End Class
