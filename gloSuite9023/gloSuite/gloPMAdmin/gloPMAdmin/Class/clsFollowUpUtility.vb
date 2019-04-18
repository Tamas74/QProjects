Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient


Public Class clsFollowUpUtility
    Private _databaseconnectionstring As String = ""


    Public Sub New()
        _databaseconnectionstring = gloGlobal.gloPMGlobal.DatabaseConnectionString
    End Sub

    Public Function GetAllAccounts() As DataTable
        Dim dtAccounts As New DataTable()
        Dim oDB As New gloDatabaseLayer.DBLayer(_databaseconnectionstring)
        Try
            oDB.Connect(False)
            oDB.Retrive_Query("select nPAccountID , sAccountNo  from PA_Accounts", dtAccounts)
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
    Public Function GetAllClaims() As DataTable
        Dim dtAccounts As New DataTable()
        Dim oDB As New gloDatabaseLayer.DBLayer(_databaseconnectionstring)
        Try
            oDB.Connect(False)
            oDB.Retrive_Query("select distinct nTransactionMasterID ,nTransactionID ,isnull(bIsRebilled,0) as bIsRebilled  from BL_Transaction_Claim_MST where nClaimStatus =1  and isnull(bIsVoid,0) =0 and nResponsibilityType = 2", dtAccounts)
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
    Public Function GetDefaultAccFollowupSetting() As DataTable

        Dim dtAccounts As New DataTable()
        Dim oDB As New gloDatabaseLayer.DBLayer(_databaseconnectionstring)
        Try
            oDB.Connect(False)
            oDB.Retrive("CL_Get_AccountFollowUp_Default", dtAccounts)
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



    Public Function GetDefaultClaimsFollowupSetting() As DataTable

        Dim dtAccounts As New DataTable()
        Dim oDB As New gloDatabaseLayer.DBLayer(_databaseconnectionstring)
        Try
            oDB.Connect(False)
            oDB.Retrive("CL_Get_ClaimFollowUp_Default", dtAccounts)
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

    Public Sub ProcessAccount(ByVal nPAccountID As Int64, ByVal StmtCountSet As Integer, ByVal days As Integer, ByVal action As String, ByVal actionDescription As String)
        Dim dtAccounts As New DataTable()
        Dim oDB As New gloDatabaseLayer.DBLayer(_databaseconnectionstring)
        Dim oParameters As New gloDatabaseLayer.DBParameters()
        Try
            oDB.Connect(False)
            oParameters.Add("@nPAccountID", nPAccountID, ParameterDirection.Input, SqlDbType.BigInt)
            oParameters.Add("@StmtCountSet", StmtCountSet, ParameterDirection.Input, SqlDbType.Int)
            oParameters.Add("@days", days, ParameterDirection.Input, SqlDbType.Int)
            oParameters.Add("@action", action, ParameterDirection.Input, SqlDbType.VarChar)
            oParameters.Add("@actionDescription", actionDescription, ParameterDirection.Input, SqlDbType.VarChar)
            oParameters.Add("@userid", gloGlobal.gloPMGlobal.UserID, ParameterDirection.Input, SqlDbType.BigInt)
            oParameters.Add("@username", gloGlobal.gloPMGlobal.UserName, ParameterDirection.Input, SqlDbType.VarChar)
            oDB.Execute("CL_Create_Account_FollowUp", oParameters)
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
    Public Sub ProcessClaim(ByVal nTransactionMstId As Int64, ByVal nTransactionId As Int64, ByVal bIsrebilled As [Boolean], ByVal billdays As Integer, ByVal billaction As String, ByVal billactionDescription As String, _
     ByVal rebilldays As Integer, ByVal rebillaction As String, ByVal rebillactionDescription As String)
        Dim dtAccounts As New DataTable()
        Dim oDB As New gloDatabaseLayer.DBLayer(_databaseconnectionstring)
        Dim oParameters As New gloDatabaseLayer.DBParameters()
        Try
            oDB.Connect(False)
            oParameters.Add("@nTransactionMstId", nTransactionMstId, ParameterDirection.Input, SqlDbType.BigInt)

            oParameters.Add("@nTransactionId", nTransactionId, ParameterDirection.Input, SqlDbType.BigInt)
            oParameters.Add("@bisrebilled", bIsrebilled, ParameterDirection.Input, SqlDbType.Bit)

            oParameters.Add("@billdays", billdays, ParameterDirection.Input, SqlDbType.Int)
            oParameters.Add("@billaction", billaction, ParameterDirection.Input, SqlDbType.VarChar)
            oParameters.Add("@billactionDescription", billactionDescription, ParameterDirection.Input, SqlDbType.VarChar)

            oParameters.Add("@rebilldays", rebilldays, ParameterDirection.Input, SqlDbType.Int)
            oParameters.Add("@rebillaction", rebillaction, ParameterDirection.Input, SqlDbType.VarChar)
            oParameters.Add("@rebillactionDescription", rebillactionDescription, ParameterDirection.Input, SqlDbType.VarChar)


            oParameters.Add("@userid", gloGlobal.gloPMGlobal.UserID, ParameterDirection.Input, SqlDbType.BigInt)
            oParameters.Add("@username", gloGlobal.gloPMGlobal.UserName, ParameterDirection.Input, SqlDbType.VarChar)


            oDB.Execute("CL_Create_Claim_FollowUp", oParameters)
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

