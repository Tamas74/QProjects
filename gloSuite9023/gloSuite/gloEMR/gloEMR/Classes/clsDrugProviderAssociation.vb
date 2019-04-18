Imports System.Data.SqlClient

Public Class clsDrugProviderAssociation
    Implements IDisposable

    ' Private da As SqlDataAdapter
    '  Private ds As New DataSet
    'Private dt As DataTable
    'Private dv As DataView
    Private Con As SqlConnection
    'Private conString As String

    ' Private Conn As SqlConnection
    'Private Dv As DataView
    ' Private Cmd As System.Data.SqlClient.SqlCommand
    ' Private ArrMedicationCol As New ArrayList

    Public Sub New()
        Try
            Dim conString As String
            conString = GetConnectionString()
            Con = New SqlConnection(conString)
        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Drugs, gloAuditTrail.ActivityCategory.Drugs, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            'UpdateLog("clsDrugProviderAssociation -- New -- " & ex.ToString)
            Con.Close()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Drugs, gloAuditTrail.ActivityCategory.Drugs, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            'UpdateLog("clsDrugProviderAssociation -- New -- " & ex.ToString)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Con.Close()
        End Try
    End Sub

    ' fill Provider  ' 0 - provider ' 1 - Allergies ' 2 - C2 ' 3 - Clinical ' 4 - All Drugs ' 5 - Clinical Drugs

    Public Function GetAllDrugs(ByVal ID As Int64) As DataView
        Try
            Dim cmd As New SqlCommand("gsp_ViewDrugs_MST", Con)
            cmd.CommandType = CommandType.StoredProcedure
            Dim sqlParam As SqlParameter
            sqlParam = cmd.Parameters.Add("@id", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = ID

            Con.Open()
            'cmd.ExecuteNonQuery()
            Dim da As SqlDataAdapter = New SqlDataAdapter
            da.SelectCommand = cmd

            Dim dt As DataTable = New DataTable
            da.Fill(dt)
            da.Dispose()

            Dim dv As DataView = Nothing
            If (IsNothing(dt) = False) Then
                dv = New DataView(dt.Copy())
                dt.Dispose()
                dt = Nothing
            End If


            sqlParam = Nothing

            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If

            Return dv

            ''DrugID= dv.Item(0)(0)
            ''DrugName= dv.Item(0)(1)
            ''GenericName= dv.Item(0)(2)
            ''Dosage= dv.Item(0)(3)
            ''Route= dv.Item(0)(4)
            ''Frequency= dv.Item(0)(5)
            ''Duration= dv.Item(0)(6)
            ''Clinical/NonClinical Drugs= dv.Item(0)(7)
        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Drugs, gloAuditTrail.ActivityCategory.Drugs, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
            'UpdateLog("clsDrugProviderAssociation -- GetAllDrugs -- " & ex.ToString)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Drugs, gloAuditTrail.ActivityCategory.Drugs, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            'UpdateLog("clsDrugProviderAssociation -- GetAllDrugs -- " & ex.ToString)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            Con.Close()
        End Try
    End Function

    Public Function fillDataFromDb(ByVal id As Int64, Optional ByVal strSearch As String = "") As DataTable
        Dim Cmd As SqlCommand = Nothing
        Dim adpt As New SqlDataAdapter
        Dim dt As New DataTable
        Try
            adpt.SelectCommand = Nothing
            If id = 0 Then
                Cmd = New SqlCommand("gsp_fillAllProvider", Con)

                Cmd.CommandType = CommandType.StoredProcedure
                adpt.SelectCommand = Cmd

                'Dim objParam As SqlParameter

                'objParam = Cmd.Parameters.Add("@drugletter", SqlDbType.Char)
                'objParam.Direction = ParameterDirection.Input
                'objParam.Value = LCase(strSearch)

                'objParam = Cmd.Parameters.Add("@flag", SqlDbType.Int)
                'objParam.Direction = ParameterDirection.Input
                'objParam.Value = 1 ''4
                ''
                ''Sandip Darade 20090522
                ''Replaced   'gsp_FillDrugs_Mst' pulling 40 recs with 'gsp_FillAllDrugs_Mst' pulling allrecs
            ElseIf id = 1 Then
                Cmd = New SqlCommand("gsp_FillAllDrugs_Mst", Con)
                Cmd.CommandType = CommandType.StoredProcedure
                adpt.SelectCommand = Cmd

                Dim objParam As SqlParameter

                objParam = Cmd.Parameters.Add("@drugletter", SqlDbType.Char)
                objParam.Direction = ParameterDirection.Input
                objParam.Value = LCase(strSearch)

                objParam = Cmd.Parameters.Add("@flag", SqlDbType.Int)
                objParam.Direction = ParameterDirection.Input
                objParam.Value = 14   '' flag to fill Allergies drugs 14 is used in SP  

                objParam = Nothing
            ElseIf id = 2 Then
                Cmd = New SqlCommand("gsp_FillAllDrugs_Mst", Con)
                Cmd.CommandType = CommandType.StoredProcedure
                adpt.SelectCommand = Cmd

                Dim objParam As SqlParameter

                objParam = Cmd.Parameters.Add("@drugletter", SqlDbType.Char)
                objParam.Direction = ParameterDirection.Input
                objParam.Value = LCase(strSearch)

                objParam = Cmd.Parameters.Add("@flag", SqlDbType.Int)
                objParam.Direction = ParameterDirection.Input
                objParam.Value = 15   '' flag to fill C2 drugs 15 is used in SP        

                objParam = Nothing
            ElseIf id = 3 Or id = 4 Or id = 5 Then  '' For All Drugs 
                Cmd = New SqlCommand("gsp_FillAllDrugs_Mst", Con)
                Cmd.CommandType = CommandType.StoredProcedure
                adpt.SelectCommand = Cmd

                Dim objParam As SqlParameter

                objParam = Cmd.Parameters.Add("@drugletter", SqlDbType.Char)
                objParam.Direction = ParameterDirection.Input
                objParam.Value = LCase(strSearch)

                objParam = Cmd.Parameters.Add("@flag", SqlDbType.Int)
                objParam.Direction = ParameterDirection.Input

                If id = 4 Then
                    objParam.Value = 11   '' flag to fill All drugs 11 is used in SP to search all drugs
                Else ' for 3/5 which is clinical drugs
                    objParam.Value = 12   '' flag to fill All Clinical Drugs 12 is used in SP to search all Clinical drugs
                End If
                objParam = Nothing
                '    Cmd.CommandType = CommandType.StoredProcedure
                '    adpt.SelectCommand = Cmd
            End If
            If (IsNothing(adpt.SelectCommand) = False) Then
                adpt.Fill(dt)
                'Conn.Close()
                adpt.Dispose()
                Return dt
            Else
                adpt.Dispose()
                dt.Dispose()
                Return Nothing
            End If


        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Drugs, gloAuditTrail.ActivityCategory.Drugs, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
            'UpdateLog("clsDrugProviderAssociation -- fillDataFromDb -- " & ex.ToString)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Drugs, gloAuditTrail.ActivityCategory.Drugs, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            'UpdateLog("clsDrugProviderAssociation -- fillDataFromDb -- " & ex.ToString)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            Con.Close()

            If Cmd IsNot Nothing Then
                Cmd.Parameters.Clear()
                Cmd.Dispose()
                Cmd = Nothing
            End If
        End Try

    End Function

    Public Function fillProvidersDrugs(ByVal id As Int64) As DataTable
        Dim Cmd As SqlCommand = Nothing
        Dim adpt As New SqlDataAdapter
        Dim dt As New DataTable
        Try
            If Not IsDBNull(id) Then

                Cmd = New SqlCommand("gsp_GetProviderDrugs", Con)
                Cmd.CommandType = CommandType.StoredProcedure
                adpt.SelectCommand = Cmd

                Dim objParam As SqlParameter

                objParam = Cmd.Parameters.Add("@ProviderId", SqlDbType.BigInt)
                objParam.Direction = ParameterDirection.Input
                objParam.Value = id

                objParam = Nothing
                '    Cmd.CommandType = CommandType.StoredProcedure
                '    adpt.SelectCommand = Cmd
                adpt.Fill(dt)
                'Conn.Close()
                adpt.Dispose()
                Return dt
            Else
                adpt.Dispose()
                dt.Dispose()
                Return Nothing
            End If

          
        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Drugs, gloAuditTrail.ActivityCategory.Drugs, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
            'UpdateLog("clsDrugProviderAssociation -- fillProvidersDrugs -- " & ex.ToString)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Drugs, gloAuditTrail.ActivityCategory.Drugs, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            'UpdateLog("clsDrugProviderAssociation -- fillProvidersDrugs -- " & ex.ToString)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            Con.Close()
            If Cmd IsNot Nothing Then
                Cmd.Parameters.Clear()
                Cmd.Dispose()
                Cmd = Nothing
            End If
        End Try
    End Function

    Public Sub DeleteProvidersDrugs(ByVal providerId As Long, Optional ByVal DrugId As Long = 0, Optional ByVal SIGID As Long = 0)
        'Dim adpt As New SqlDataAdapter
        'Dim dt As New DataTable
        Dim nCount As Integer = 0
        Dim Cmd As SqlCommand = Nothing
        Try
            If Not IsDBNull(providerId) Then

                Cmd = New SqlCommand("gsp_DeleteProvidersDrugs", Con)
                Cmd.CommandType = CommandType.StoredProcedure
                '       adpt.SelectCommand = Cmd

                Dim objParam As SqlParameter

                objParam = Cmd.Parameters.Add("@providerId", SqlDbType.BigInt)
                objParam.Direction = ParameterDirection.Input
                objParam.Value = providerId

                If DrugId > 0 Then
                    objParam = Cmd.Parameters.Add("@DrugId", SqlDbType.BigInt)
                    objParam.Direction = ParameterDirection.Input
                    objParam.Value = DrugId 'providerId
                End If

                If SIGID > 0 Then
                    objParam = Cmd.Parameters.Add("@SIGID", SqlDbType.BigInt)
                    objParam.Direction = ParameterDirection.Input
                    objParam.Value = SIGID
                End If
            
                Con.Open()
                nCount = Cmd.ExecuteNonQuery()
                Con.Close()
                objParam = Nothing
                'adpt.Fill(dt)
                'Return dt
            End If
        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Drugs, gloAuditTrail.ActivityCategory.Drugs, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            'UpdateLog("clsDrugProviderAssociation -- DeleteProvidersDrugs -- " & ex.ToString)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Drugs, gloAuditTrail.ActivityCategory.Drugs, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            'UpdateLog("clsDrugProviderAssociation -- DeleteProvidersDrugs -- " & ex.ToString)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Con.Close()
            If Cmd IsNot Nothing Then
                Cmd.Parameters.Clear()
                Cmd.Dispose()
                Cmd = Nothing
            End If
        End Try

    End Sub
    '\\ change by suraj 20090127
    'Public Sub AddProvidersDrugs(ByVal providerId As Long, ByVal DrugId As Long)
    Public Sub AddProvidersDrugs(ByVal providerId As Long, ByVal DrugId As Long, ByVal _drugname As String, ByVal _dosage As String, ByVal _route As String, ByVal _frequency As String, ByVal _duration As String, ByVal _drugform As String, ByVal _ndccode As String, ByVal _isnarcotics As Int64, ByVal _sDrugQtyQualifier As String, ByVal _sRefill As String, ByVal _nSIGid As Long, ByVal _sDispAmt As String, ByVal _mpid As Integer)
        'Dim adpt As New SqlDataAdapter
        'Dim dt As New DataTable
        Dim nCount As Integer = 0
        Dim objParam As SqlParameter
        Dim Cmd As SqlCommand = Nothing
        Try
            If Not IsDBNull(providerId) Then

                Cmd = New SqlCommand("gsp_InsertProvidersDrugs", Con)
                Cmd.CommandType = CommandType.StoredProcedure
                'adpt.SelectCommand = Cmd

                objParam = Cmd.Parameters.Add("@DrugId", SqlDbType.BigInt)
                objParam.Direction = ParameterDirection.Input
                objParam.Value = DrugId

                objParam = Cmd.Parameters.Add("@ProviderId", SqlDbType.BigInt)
                objParam.Direction = ParameterDirection.Input
                objParam.Value = providerId

                '\\added by suraj on 20090121
                objParam = Cmd.Parameters.Add("@DrugName", SqlDbType.VarChar)
                objParam.Direction = ParameterDirection.Input
                objParam.Value = _drugname

                objParam = Cmd.Parameters.Add("@Dosage", SqlDbType.VarChar)
                objParam.Direction = ParameterDirection.Input
                objParam.Value = _dosage

                objParam = Cmd.Parameters.Add("@Route", SqlDbType.VarChar)
                objParam.Direction = ParameterDirection.Input
                objParam.Value = _route

                objParam = Cmd.Parameters.Add("@Frequency", SqlDbType.VarChar)
                objParam.Direction = ParameterDirection.Input
                objParam.Value = _frequency

                objParam = Cmd.Parameters.Add("@Duration", SqlDbType.VarChar)
                objParam.Direction = ParameterDirection.Input
                objParam.Value = _duration

                objParam = Cmd.Parameters.Add("@DrugForm", SqlDbType.VarChar)
                objParam.Direction = ParameterDirection.Input
                objParam.Value = _drugform

                objParam = Cmd.Parameters.Add("@NDCCode", SqlDbType.VarChar)
                objParam.Direction = ParameterDirection.Input
                objParam.Value = _ndccode

                objParam = Cmd.Parameters.Add("@IsNarcotics", SqlDbType.Int)
                objParam.Direction = ParameterDirection.Input
                objParam.Value = _isnarcotics


                objParam = Cmd.Parameters.Add("@sDrugQtyQualifier", SqlDbType.VarChar)
                objParam.Direction = ParameterDirection.Input
                objParam.Value = _sDrugQtyQualifier

                '
                objParam = Cmd.Parameters.Add("@sRefills", SqlDbType.VarChar)
                objParam.Direction = ParameterDirection.Input
                objParam.Value = _sRefill

                objParam = Cmd.Parameters.Add("@nSIGID", SqlDbType.BigInt)
                objParam.Direction = ParameterDirection.Input
                objParam.Value = _nSIGid

                objParam = Cmd.Parameters.Add("@sAmount", SqlDbType.VarChar)
                objParam.Direction = ParameterDirection.Input
                objParam.Value = _sDispAmt

                objParam = Cmd.Parameters.Add("@mpid", SqlDbType.Int)
                objParam.Direction = ParameterDirection.Input
                objParam.Value = _mpid

                Con.Open()
                nCount = Cmd.ExecuteNonQuery()
                Con.Close()
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.Drugs, gloAuditTrail.ActivityType.Add, _drugname + " added to Provider favorites", 0, DrugId, gnLoginProviderID, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                objParam = Nothing
            End If


            'If nCount > 0 Then
            '    'update
            '    MessageBox.Show("Data Update Sussefully")
            '    Exit Function
            'Else
            '    'Add
            '    MessageBox.Show("Data Add Sussefully")
            '    Exit Function
            'End If
        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Drugs, gloAuditTrail.ActivityCategory.Drugs, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            'UpdateLog("clsDrugProviderAssociation -- AddProvidersDrugs -- " & ex.ToString)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Drugs, gloAuditTrail.ActivityCategory.Drugs, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            'UpdateLog("clsDrugProviderAssociation -- AddProvidersDrugs -- " & ex.ToString)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Con.Close()

            If Cmd IsNot Nothing Then
                Cmd.Parameters.Clear()
                Cmd.Dispose()
                Cmd = Nothing
            End If
        End Try
    End Sub

    Public Sub UpdateDrugsStatus(ByVal type As Integer, ByVal DrugId As Long)
        'Dim adpt As New SqlDataAdapter
        'Dim dt As New DataTable
        Dim nCount As Integer = 0
        Dim Cmd As SqlCommand = Nothing
        Try
            If Not IsDBNull(DrugId) Then
                If Not IsDBNull(type) Then

                    Cmd = New SqlCommand("gsp_UpdateDrugsStatus", Con)
                    Cmd.CommandType = CommandType.StoredProcedure
                    'adpt.SelectCommand = Cmd

                    Dim objParam As SqlParameter

                    objParam = Cmd.Parameters.Add("@type", SqlDbType.BigInt)
                    objParam.Direction = ParameterDirection.Input
                    objParam.Value = type

                    objParam = Cmd.Parameters.Add("@DrugId", SqlDbType.BigInt)
                    objParam.Direction = ParameterDirection.Input
                    objParam.Value = DrugId

                    objParam = Cmd.Parameters.Add("@status", SqlDbType.Bit)
                    objParam.Direction = ParameterDirection.Input
                    objParam.Value = 1
                    Con.Open()
                    nCount = Cmd.ExecuteNonQuery()
                    Con.Close()

                    objParam = Nothing
                End If

            End If

            'If nCount > 0 Then
            '    'update
            '    MessageBox.Show("Data Update Sussefully")
            '    Exit Function
            'Else
            '    'Add
            '    MessageBox.Show("Data Add Sussefully")
            '    Exit Function
            'End If
        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Drugs, gloAuditTrail.ActivityCategory.Drugs, gloAuditTrail.ActivityType.Modify, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            'UpdateLog("clsDrugProviderAssociation -- UpdateDrugsStatus -- " & ex.ToString)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Drugs, gloAuditTrail.ActivityCategory.Drugs, gloAuditTrail.ActivityType.Modify, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            'UpdateLog("clsDrugProviderAssociation -- UpdateDrugsStatus -- " & ex.ToString)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Con.Close()

            If Cmd IsNot Nothing Then
                Cmd.Parameters.Clear()
                Cmd.Dispose()
                Cmd = Nothing
            End If
        End Try
    End Sub

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                If Con IsNot Nothing Then
                    Con.Dispose()
                    Con = Nothing
                End If
                '    Cmd.Parameters.Clear()
                '    Cmd.Dispose()
                '    Cmd = Nothing
                'End If
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
