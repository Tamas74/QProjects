Imports System.Data
Imports System.Data.SqlClient
Public Class ClsPrescriptionDBLayer
    Implements IDisposable
    Dim _PatientID As Long
    Public Sub New(ByVal PatientID As Long)
        Dim sqlconn As String
        sqlconn = GetConnectionString()
        Conn = New System.Data.SqlClient.SqlConnection(sqlconn)
        _PatientID = PatientID
    End Sub

    Private Conn As SqlConnection
    Private Dv As DataView
    '  Private Cmd As System.Data.SqlClient.SqlCommand
    Private m_dtdate As DateTime
    Private m_Visitdate As DateTime
    Private ArrPrescriptionCol As New ArrayList

    Public Property dtDate() As DateTime
        Get
            Return m_dtdate
        End Get
        Set(ByVal Value As DateTime)
            m_dtdate = Value
        End Set
    End Property
    Public Property VisitDate() As DateTime
        Get
            Return m_Visitdate
        End Get
        Set(ByVal Value As DateTime)
            m_Visitdate = Value
        End Set
    End Property

    Public Function FillControls(ByVal id As Long, Optional ByVal strsearch As String = "") As DataTable
        Dim adpt As New SqlDataAdapter
        Dim dt As New DataTable
        Dim Cmd As SqlCommand = Nothing
        Try
            If id = 0 Then
                Cmd = New SqlCommand("gsp_FillSig_Mst", Conn)
                Cmd.CommandType = CommandType.StoredProcedure
                adpt.SelectCommand = Cmd
            Else
                Cmd = New SqlCommand("gsp_FillDrugs_Mst", Conn)

                Cmd.CommandType = CommandType.StoredProcedure
                adpt.SelectCommand = Cmd

                Dim objParam As SqlParameter

                objParam = Cmd.Parameters.Add("@drugletter", SqlDbType.Char)
                objParam.Direction = ParameterDirection.Input
                objParam.Value = LCase(strsearch)


                objParam = Cmd.Parameters.Add("@flag", SqlDbType.Int)
                objParam.Direction = ParameterDirection.Input
                objParam.Value = id

                objParam = Cmd.Parameters.Add("@PatientID", SqlDbType.BigInt)
                objParam.Direction = ParameterDirection.Input
                objParam.Value = _PatientID

                If id = 3 Then
                    objParam = Cmd.Parameters.Add("@dtSysDate", SqlDbType.DateTime)
                    objParam.Direction = ParameterDirection.Input
                    objParam.Value = Format(Now.Date, "MM/dd/yyyy")
                End If

                objParam = Nothing
            End If
            adpt.Fill(dt)
            If (IsNothing(Dv) = False) Then
                Dv.Dispose()
                Dv = Nothing
            End If
            Dv = dt.DefaultView
            Conn.Close()
            Return dt

            'Dim dreader As SqlDataReader
            'Conn.Open()
            'dreader = Cmd.ExecuteReader()

            'Do While dreader.Read
            '    Dim i As Integer
            '    i = dreader("nSpecialtyID")

            'Loop
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            If Conn.State = ConnectionState.Open Then
                Conn.Close()
            End If
            Return Nothing
        Finally
            If Not IsNothing(Cmd) Then
                Cmd.Parameters.Clear()
                Cmd.Dispose()
                Cmd = Nothing
            End If
            'If Not IsNothing(dt) Then
            '    dt.Dispose()
            '    dt = Nothing
            'End If
            If Not IsNothing(adpt) Then
                adpt.Dispose()
                adpt = Nothing
            End If
        End Try
    End Function

    Public Function Fill_LockPrescription(ByVal MachinName As String, ByVal TransactionType As Integer) As DataTable
        Dim Cmd As SqlCommand = Nothing
        Dim dt As New DataTable
        Dim sqladpt As New SqlDataAdapter
        Try
            Cmd = New System.Data.SqlClient.SqlCommand("gsp_Select_UnLock_Record", Conn)
            Cmd.CommandType = CommandType.StoredProcedure
            Dim objParam As SqlParameter

            objParam = Cmd.Parameters.Add("@sMachinName", SqlDbType.VarChar)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = MachinName

            objParam = Cmd.Parameters.Add("@nTrnType", SqlDbType.Int)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = TransactionType

            objParam = Cmd.Parameters.Add("@nMachinID", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = 0

            sqladpt.SelectCommand = Cmd

            sqladpt.Fill(dt)

            Conn.Close()
            objParam = Nothing
            Return dt
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Conn.Close()
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If Not IsNothing(Cmd) Then
                Cmd.Parameters.Clear()
                Cmd.Dispose()
                Cmd = Nothing
            End If
            'If Not IsNothing(dt) Then
            '    dt.Dispose()
            '    dt = Nothing
            'End If
            If Not IsNothing(sqladpt) Then
                sqladpt.Dispose()
                sqladpt = Nothing
            End If
        End Try
    End Function

    Public Sub SaveRxProviderAssociation(ByVal nPatientID As Int64, ByVal nVisitID As Int64, ByVal dtPrescriptionDate As DateTime, ByVal nJuniorProviderID As Int64, ByVal arrProvider As ArrayList)
        Dim oTransaction As SqlTransaction
        If Conn.State = ConnectionState.Closed Then Conn.Open()

        oTransaction = Conn.BeginTransaction()
        Dim cmd As SqlCommand = Nothing
        Try

            If arrProvider IsNot Nothing Then
                If arrProvider.Count > 0 Then
                    Dim _Query As String


                    _Query = "DELETE FROM Rx_ProviderAssociation WHERE nPatientID = " & nPatientID & " AND nVisitID = " & nVisitID & " AND " _
                    & " CONVERT(VARCHAR,dtPrescriptionDate,101) = CONVERT(VARCHAR,CONVERT(DATETIME,'" & dtPrescriptionDate & "'),101) AND " _
                    & " CONVERT(VARCHAR,dtPrescriptionDate,108) = CONVERT(VARCHAR,CONVERT(DATETIME,'" & dtPrescriptionDate & "'),108) AND " _
                    & " nJuniorProviderID = " & nJuniorProviderID & ""
                    cmd = New SqlCommand
                    cmd.Connection = Conn
                    cmd.Transaction = oTransaction
                    cmd.CommandType = CommandType.Text
                    cmd.CommandText = _Query
                    cmd.ExecuteNonQuery()
                    cmd.Parameters.Clear()
                    cmd.Dispose()
                    cmd = Nothing
                    cmd = New SqlCommand
                    cmd.Connection = Conn
                    cmd.Transaction = oTransaction
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.CommandText = "gsp_InsertRxProviderAssociation"

                    For i As Integer = 0 To arrProvider.Count - 1
                        cmd.Parameters.Clear()

                        cmd.Parameters.Add("@nPatientID", SqlDbType.BigInt)
                        cmd.Parameters.Add("@nVisitID", SqlDbType.BigInt)
                        cmd.Parameters.Add("@dtPrescriptionDate", SqlDbType.DateTime)
                        cmd.Parameters.Add("@nProviderID", SqlDbType.BigInt)
                        cmd.Parameters.Add("@sProviderName", SqlDbType.VarChar)
                        cmd.Parameters.Add("@sDEA", SqlDbType.VarChar)
                        cmd.Parameters.Add("@nJuniorProviderID", SqlDbType.BigInt)

                        cmd.Parameters("@nPatientID").Value = nPatientID
                        cmd.Parameters("@nVisitID").Value = nVisitID
                        cmd.Parameters("@dtPrescriptionDate").Value = dtPrescriptionDate
                        cmd.Parameters("@nProviderID").Value = CType(arrProvider(i), myList).ID
                        cmd.Parameters("@sProviderName").Value = CType(arrProvider(i), myList).Description
                        cmd.Parameters("@sDEA").Value = CType(arrProvider(i), myList).Code
                        cmd.Parameters("@nJuniorProviderID").Value = nJuniorProviderID

                        cmd.ExecuteNonQuery()
                    Next

                    oTransaction.Commit()

                End If
            End If

        Catch ex As Exception
            oTransaction.Rollback()
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If

            If Not IsNothing(oTransaction) Then
                oTransaction.Dispose()
                oTransaction = Nothing
            End If
        End Try
    End Sub

    Public Sub SaveRxProviderAssociation(ByVal nJuniorProviderID As Int64, ByVal arrProvider As ArrayList)
        Dim oTransaction As SqlTransaction
        If Conn.State = ConnectionState.Closed Then Conn.Open()

        oTransaction = Conn.BeginTransaction()
        Dim cmd As SqlCommand = Nothing
        Try

            If arrProvider IsNot Nothing Then
                If arrProvider.Count > 0 Then


                    Dim _Query As String


                    _Query = "DELETE FROM Rx_ProviderAssociation WHERE nPatientID = 0 AND nVisitID = 0 AND nJuniorProviderID = " & nJuniorProviderID & ""
                    cmd = New SqlCommand
                    cmd.Connection = Conn
                    cmd.Transaction = oTransaction
                    cmd.CommandType = CommandType.Text
                    cmd.CommandText = _Query
                    cmd.ExecuteNonQuery()
                    cmd.Parameters.clear()
                    cmd.dispose()
                    cmd = Nothing
                    cmd = New SqlCommand
                    cmd.Connection = Conn
                    cmd.Transaction = oTransaction
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.CommandText = "gsp_InsertRxProviderAssociation"

                    For i As Integer = 0 To arrProvider.Count - 1
                        cmd.Parameters.Clear()

                        cmd.Parameters.Add("@nPatientID", SqlDbType.BigInt)
                        cmd.Parameters.Add("@nVisitID", SqlDbType.BigInt)
                        cmd.Parameters.Add("@dtPrescriptionDate", SqlDbType.DateTime)
                        cmd.Parameters.Add("@nProviderID", SqlDbType.BigInt)
                        cmd.Parameters.Add("@sProviderName", SqlDbType.VarChar)
                        cmd.Parameters.Add("@sDEA", SqlDbType.VarChar)
                        cmd.Parameters.Add("@nJuniorProviderID", SqlDbType.BigInt)

                        cmd.Parameters("@nPatientID").Value = 0
                        cmd.Parameters("@nVisitID").Value = 0
                        cmd.Parameters("@dtPrescriptionDate").Value = Now
                        cmd.Parameters("@nProviderID").Value = CType(arrProvider(i), myList).ID
                        cmd.Parameters("@sProviderName").Value = CType(arrProvider(i), myList).Description
                        cmd.Parameters("@sDEA").Value = CType(arrProvider(i), myList).Code
                        cmd.Parameters("@nJuniorProviderID").Value = nJuniorProviderID

                        cmd.ExecuteNonQuery()
                    Next

                    oTransaction.Commit()

                End If
            End If

        Catch ex As Exception
            oTransaction.Rollback()
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If (IsNothing(oTransaction) = False) Then
                oTransaction.Dispose()
                oTransaction = Nothing
            End If
        End Try
    End Sub

    Public Sub SaveRxProviderAssociation(ByVal nPatientID As Int64, ByVal nVisitID As Int64, ByVal dtPrescriptionDate As DateTime, ByVal nJuniorProviderID As Int64)
        Dim Cmd As SqlCommand = Nothing
        Try
            Dim query As String
            query = " INSERT INTO Rx_ProviderAssociation " _
                  & " SELECT " & nPatientID & ", " & nVisitID & ", '" & dtPrescriptionDate & "', nProviderID, sProviderName, sDEA, nJuniorProviderID " _
                  & " FROM Rx_ProviderAssociation WHERE nJuniorProviderID = " & nJuniorProviderID & " AND nPatientID = 0 AND nVisitID = 0 "
            Cmd = New SqlCommand(query, Conn)
            If Conn.State = ConnectionState.Closed Then Conn.Open()
            Cmd.ExecuteNonQuery()
            Conn.Close()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not IsNothing(Cmd) Then
                Cmd.Parameters.Clear()
                Cmd.Dispose()
                Cmd = Nothing
            End If
        End Try
    End Sub

    Public Function GetRxProviderAssociation(ByVal nPatientID As Int64, ByVal nVisitID As Int64, ByVal dtPrescriptionDate As DateTime) As DataTable
        Dim adp As SqlDataAdapter = Nothing
        Dim dtAssociation As DataTable = Nothing

        Try

            Dim _query As String = " SELECT nProviderID, sProviderName, sDEA FROM Rx_ProviderAssociation WHERE nPatientID = " & nPatientID & " AND nVisitID = " & nVisitID & " AND " _
            & " CONVERT(VARCHAR,dtPrescriptionDate,101) = CONVERT(VARCHAR,CONVERT(DATETIME,'" & dtPrescriptionDate & "'),101) AND " _
            & " CONVERT(VARCHAR,dtPrescriptionDate,108) = CONVERT(VARCHAR,CONVERT(DATETIME,'" & dtPrescriptionDate & "'),108)"
            adp = New SqlDataAdapter(_query, Conn)
            dtAssociation = New DataTable
            adp.Fill(dtAssociation)
            If dtAssociation IsNot Nothing Then
                Return dtAssociation
            Else
                Return Nothing
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            'If Not IsNothing(dtAssociation) Then
            '    dtAssociation.Dispose()
            '    dtAssociation = Nothing
            'End If
            If Not IsNothing(adp) Then
                adp.Dispose()
                adp = Nothing
            End If
        End Try
    End Function

    Public Function GetRxProviderAssociation(ByVal nJuniorProviderID As Int64) As DataTable
        Dim adp As SqlDataAdapter = Nothing
        Dim dtAssociation As DataTable = Nothing
        Try
            Dim _query As String = " SELECT nProviderID, sProviderName, sDEA FROM Rx_ProviderAssociation WHERE nPatientID = 0 AND nVisitID = 0 AND nJuniorProviderID = " & nJuniorProviderID & ""
            adp = New SqlDataAdapter(_query, Conn)
            dtAssociation = New DataTable
            adp.Fill(dtAssociation)
            If dtAssociation IsNot Nothing Then
                Return dtAssociation
            Else
                Return Nothing
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            'If Not IsNothing(dtAssociation) Then
            '    dtAssociation.Dispose()
            '    dtAssociation = Nothing
            'End If
            If Not IsNothing(adp) Then
                adp.Dispose()
                adp = Nothing
            End If
        End Try
    End Function

    Public Function GetRxProviderAssociationSettings() As Boolean
        Dim Cmd As SqlCommand = Nothing
        Try
            Dim query As String = " select ISNULL(sSettingsValue,'') AS sSettingsValue from settings " _
                & " where sSettingsName='MULTIPLE SUPERVISORS FOR PAPER RX'"
            Cmd = New SqlCommand(query, Conn)
            If Conn.State = ConnectionState.Closed Then Conn.Open()
            Dim oResult As Object = Cmd.ExecuteScalar
            Conn.Close()
            If oResult IsNot Nothing Then
                gblnMultipleSupervisorsforPaperRx = CType(oResult, Boolean)
                Return CType(oResult, Boolean)
            Else
                Return False
            End If
        Catch ex As Exception
            Return False
        Finally
            If Not IsNothing(Cmd) Then
                Cmd.Parameters.Clear()
                Cmd.Dispose()
                Cmd = Nothing
            End If
        End Try
    End Function

    Private disposed As Boolean = False

    Public Sub Dispose() Implements System.IDisposable.Dispose
        'Disconnect();
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub

    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposed Then
            If disposing Then
                If Conn IsNot Nothing Then
                    Conn.Dispose()
                    Conn = Nothing

                End If
                If Dv IsNot Nothing Then
                    Dv.Dispose()
                    Dv = Nothing

                End If
            End If
        End If
        disposed = True
    End Sub


    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    '' END SUDHIR ''
End Class




