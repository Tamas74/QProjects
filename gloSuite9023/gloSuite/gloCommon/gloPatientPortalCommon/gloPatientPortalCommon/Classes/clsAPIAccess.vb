Public Class APIAccess



    Private _APIUserID As Int64
    Public Property APIUserID() As Int64
        Get
            Return _APIUserID
        End Get
        Set(ByVal value As Int64)
            _APIUserID = value
        End Set
    End Property


    Private _userName As String
    Public Property UserName() As String
        Get
            Return _userName
        End Get
        Set(ByVal value As String)
            _userName = value
        End Set
    End Property

    Private _passWord As String
    Public Property Password() As String
        Get
            Return _passWord
        End Get
        Set(ByVal value As String)
            _passWord = value
        End Set
    End Property



End Class


Public Class clsAPIAcceess

    Public Function GetRandomNumber() As String

        Dim chars = ""
        For i As Integer = 65 To 90
            chars += Char.ConvertFromUtf32(i)
        Next
        For i As Integer = 97 To 122
            chars += Char.ConvertFromUtf32(i)
        Next
        For i As Integer = 48 To 57
            chars += Char.ConvertFromUtf32(i)
        Next

        Dim random = New Random()




        Dim result = New String(Enumerable.Repeat(chars, 7).[Select](Function(s) s(random.[Next](s.Length))).ToArray())

        chars = ""


        chars += Char.ConvertFromUtf32(33)
        chars += Char.ConvertFromUtf32(35)
        chars += Char.ConvertFromUtf32(36)
        chars += Char.ConvertFromUtf32(37)
        chars += Char.ConvertFromUtf32(38)
        chars += Char.ConvertFromUtf32(42)
        chars += Char.ConvertFromUtf32(64)


        Dim random1 = New Random()




        Dim result1 = New String(Enumerable.Repeat(chars, 1).[Select](Function(s) s(random1.[Next](s.Length))).ToArray())

        Dim t1 = New Random()
        Dim t2 = t1.[Next](0, 7)

        result = result.Insert(t2, result1)

        Return result

    End Function

    Public Function APIAccessProceess(ByVal ConnectionString As String, ByVal arrAPIAccess As APIAccess(), ByVal nActionID As Int64, ByVal nRoleId As Int64, ByVal sBlockReason As String, ByVal dtBlockDate As DateTime, Optional ByVal bEncryptPassword As Boolean = True) As Long
        'Public Function APIAccessProceess(ByVal ConnectionString As String, ByVal arrAPIAccess As APIAccess(), ByVal nActionID As Int64, ByVal nRoleId As Int64, ByVal sBlockReason As String, ByVal dtBlockDate As DateTime) As Boolean

        Dim oDBLayer As New gloDatabaseLayer.DBLayer(ConnectionString)
        Dim oDBParameters As New gloDatabaseLayer.DBParameters()
        Dim sMessageId As String = String.Empty
        Dim _Result As Int64 = -1
        ' Dim _Result As Boolean = False
        Dim _encryptionKey As String = "12345678"
        Try

            Dim dtAPIUser As New DataTable
            dtAPIUser.Columns.Add("nAPIUserID", System.Type.GetType("System.Int64"))
            dtAPIUser.Columns.Add("sUserName", System.Type.GetType("System.String"))
            dtAPIUser.Columns.Add("sPassword", System.Type.GetType("System.String"))
            For index = 0 To arrAPIAccess.Count - 1
                If String.IsNullOrEmpty(arrAPIAccess(index).Password) Then

                    arrAPIAccess(index).Password = GetRandomNumber()

                End If
                If bEncryptPassword = True Then
                    Dim oClsEncryption As New gloSecurity.ClsEncryption()
                    arrAPIAccess(index).Password = oClsEncryption.EncryptToBase64String(arrAPIAccess(index).Password, _encryptionKey)
                    oClsEncryption.Dispose()
                    oClsEncryption = Nothing
                End If
                dtAPIUser.Rows.Add(arrAPIAccess(index).APIUserID, arrAPIAccess(index).UserName, arrAPIAccess(index).Password)
            Next
            Dim objClsMessageQueue As New ClsMessageQueue(ConnectionString, DateTime.Now)
            Dim _MachineName As String = System.Windows.Forms.SystemInformation.ComputerName
            Dim sMachineID As String = objClsMessageQueue.IsClientAccess(_MachineName)


            oDBLayer.Connect(False)
            Dim _value As Object = Nothing
            Dim ProcessStatus As Integer = -1

            oDBParameters.Clear()
            oDBParameters.Add("@RoleID", nRoleId, ParameterDirection.Input, SqlDbType.BigInt)
            oDBParameters.Add("@ActionId", nActionID, ParameterDirection.Input, SqlDbType.BigInt)
            oDBParameters.Add("@sMachinename", _MachineName, ParameterDirection.Input, SqlDbType.VarChar)
            oDBParameters.Add("@sMachineID", sMachineID, ParameterDirection.Input, SqlDbType.VarChar)
            oDBParameters.Add("@TVP", dtAPIUser, ParameterDirection.Input, SqlDbType.Structured)
            oDBParameters.Add("@sBlockReason", sBlockReason, ParameterDirection.Input, SqlDbType.VarChar)
            oDBParameters.Add("@dtBlockDate", dtBlockDate, ParameterDirection.Input, SqlDbType.DateTime)
            oDBParameters.Add("@ProcessStatus", ProcessStatus, ParameterDirection.InputOutput, SqlDbType.Int)

            _value = oDBLayer.Execute("gsp_APIAccessProcess", oDBParameters, ProcessStatus)

            '_value = oDBLayer.ExecuteScalar("gsp_APIAccessProcess", oDBParameters)

            '_Result = Convert.ToBoolean(_value)
            _Result = ProcessStatus

            oDBLayer.Disconnect()
            Return _Result
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
            Return _Result
        Finally

            If oDBParameters IsNot Nothing Then
                oDBParameters.Dispose()
                oDBParameters = Nothing
            End If

            If oDBLayer IsNot Nothing Then
                oDBLayer.Dispose()
                oDBLayer = Nothing
            End If
        End Try

        Return _Result
    End Function


    Public Function CheckPatientRegisterOrNotForAPI(ByVal ConnectionString As String, ByVal nPatientID As Int64) As DataTable
        'Public Function APIAccessProceess(ByVal ConnectionString As String, ByVal arrAPIAccess As APIAccess(), ByVal nActionID As Int64, ByVal nRoleId As Int64, ByVal sBlockReason As String, ByVal dtBlockDate As DateTime) As Boolean
        Dim oDB As New gloDatabaseLayer.DBLayer(ConnectionString)
        Dim oDBParameters As New gloDatabaseLayer.DBParameters()
        Dim dt As New DataTable

        Try
            oDBParameters.Add("@nPatientID", nPatientID, ParameterDirection.Input, SqlDbType.BigInt)
            oDB.Connect(False)
            oDB.Retrive("WS_CheckPatientRegisterOrNotForAPI", oDBParameters, dt)
            Return dt
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
            Return Nothing
        Finally
            If Not IsNothing(oDB) Then
                oDB.Disconnect()
                oDB.Dispose() : oDB = Nothing
            End If

            If Not IsNothing(oDBParameters) Then
                oDBParameters.Clear()
                oDBParameters.Dispose() : oDBParameters = Nothing
            End If

            If Not IsNothing(dt) Then
                dt.Dispose() : dt = Nothing
            End If
        End Try

           
    End Function

    Public Function ActivateAPIAccess(ByVal PatientID As Long, ByVal UserName As String, ByVal Password As String, ByVal ConnectionString As String) As Long
        Dim nReturned As Long = 0
        Dim lstAPIAccess As New List(Of APIAccess)
        Dim apiAccess As New APIAccess()

        Try        
            With apiAccess
                .APIUserID = PatientID
                .UserName = UserName
                .Password = Password
            End With
            lstAPIAccess.Add(apiAccess)
            nReturned = Me.APIAccessProceess(ConnectionString, lstAPIAccess.ToArray(), 1, 1, "", DateTime.Now)
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.API, gloAuditTrail.ActivityCategory.APIUser, gloAuditTrail.ActivityType.Activate, "API activated for patient", PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
            Return Nothing
        Finally
            lstAPIAccess.Clear()
            lstAPIAccess = Nothing

            apiAccess = Nothing
        End Try
        Return nReturned
    End Function

End Class

