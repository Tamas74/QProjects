Public Class ClsBusinessCenter

    Public Sub SaveBusinessCenterRules(dtRules As DataTable)

        Dim oDB As New gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString)
        Dim oDBParameters As New gloDatabaseLayer.DBParameters()

        Dim _oResult As Object = New Object
        Try

            oDBParameters.Add("@BusinessCenterRules", dtRules, ParameterDirection.Input, SqlDbType.Structured)
            oDBParameters.Add("@UserId", gloGlobal.gloPMGlobal.UserID, ParameterDirection.Input, SqlDbType.BigInt)

            oDB.Connect(False)
            oDB.Execute("INUP_BusinessCenterRules", oDBParameters)
            oDB.Disconnect()

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        Finally
            If oDBParameters IsNot Nothing Then
                oDBParameters.Dispose()
            End If
            If oDB IsNot Nothing Then
                oDB.Dispose()
            End If
        End Try
    End Sub


    Public Function GetBusinessCenterRules() As DataTable

        Dim dtRule As New DataTable()
        Dim oDB As New gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString)

        Dim _oResult As Object = New Object
        Try

            oDB.Connect(False)
            oDB.Retrive_Query("BC_GetAll_BusinessCenterRules", dtRule)
            oDB.Disconnect()

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        Finally

            If oDB IsNot Nothing Then
                oDB.Dispose()
            End If
        End Try

        Return dtRule


    End Function


    Public Function GetAccountsWithoutBC() As DataTable
        Dim dtAccounts As New DataTable()
        Dim oDB As New gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString)
        Try
            oDB.Connect(False)
            'Get all accounts who has no business center and has claims.
            oDB.Retrive_Query("select DISTINCT PA_Accounts.nPAccountID , sAccountNo  from PA_Accounts INNER JOIN dbo.BL_Transaction_Claim_MST ON  PA_Accounts.nPAccountID = BL_Transaction_Claim_MST.nPAccountID where isnull(nbusinessCenterID,0)=0 AND ISNULL(bIsVoid,0)=0", dtAccounts)
            oDB.Disconnect()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        Finally
            If oDB IsNot Nothing Then
                oDB.Dispose()
            End If
        End Try

        Return dtAccounts
    End Function

    Public Sub ProcessAccount(ByVal nPAccountID As Int64)
        Dim dtAccounts As New DataTable()
        Dim oDB As New gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString)
        Dim oParameters As New gloDatabaseLayer.DBParameters()
        Try
            oDB.Connect(False)
            oParameters.Add("@nPAccountID", nPAccountID, ParameterDirection.Input, SqlDbType.BigInt)
            oParameters.Add("@nUserID", gloGlobal.gloPMGlobal.UserID, ParameterDirection.Input, SqlDbType.BigInt)
            oParameters.Add("@sMachineName", System.Environment.MachineName, ParameterDirection.Input, SqlDbType.VarChar)
            oDB.Execute("BC_AssignBC_Account_Utility", oParameters)
            oDB.Disconnect()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        Finally
            If oDB IsNot Nothing Then
                oDB.Dispose()
            End If
            If oParameters IsNot Nothing Then
                oParameters.Dispose()
            End If
        End Try
    End Sub


End Class
