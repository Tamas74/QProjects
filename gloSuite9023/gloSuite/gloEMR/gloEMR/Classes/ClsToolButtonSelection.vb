Public Class ClsToolButtonSelection

    Public Sub New()

    End Sub

    Public Function SaveButtonSelection(ByVal UserID As Int64, ByVal ModuleName As frmToolButtonSelection.enumModuleName, ByVal _selectedButtons As ArrayList) As Boolean

        Try
            Dim strSQL As String = ""
            Dim strButtons As String = ""
            Dim oDB As New gloStream.gloDataBase.gloDataBase


            'Delete previous ToolBarButton for current Users
            strSQL = "Delete ToolBarButtons where nUserID = " & UserID & " AND nModule = " & ModuleName.GetHashCode()
            oDB.Connect(GetConnectionString)
            oDB.ExecuteNonSQLQuery(strSQL)
            oDB.Disconnect()

            For i As Integer = 0 To _selectedButtons.Count - 1
                strButtons += "," & _selectedButtons(i).ToString()
            Next

            If strButtons.Length > 0 Then
                strButtons = strButtons.Substring(1, strButtons.Length - 1)
            End If

            strSQL = " INSERT INTO ToolBarButtons(nUserID, nModule, sButtons) " _
                      & " VALUES (" & UserID & ", " & ModuleName.GetHashCode() & ", '" & strButtons & "')"
            oDB.Connect(GetConnectionString)
            oDB.ExecuteNonSQLQuery(strSQL)
            oDB.Disconnect()
        Catch ex As Exception
            Return False
        End Try

        Return True
    End Function

    Public Function GetGetButtonSelection(ByVal gnLoginID, ByVal _moduleName) As DataTable
        Dim dt As New DataTable
        Dim strSQL As String = ""
        Dim oDB As New gloStream.gloDataBase.gloDataBase
        Try

            'Delete previous ToolBarButton for current Users
            strSQL = "SELECT  sButtons, nUserId, nModule FROM ToolBarButtons WHERE nUserId = " & gnLoginID & " AND nModule = " & _moduleName.GetHashCode()
            oDB.Connect(GetConnectionString)
            dt = oDB.ReadQueryDataTable(strSQL)

            oDB.Disconnect()

            Return dt

        Catch ex As Exception
            Return Nothing
        Finally

            dt = Nothing
            strSQL = Nothing

            If IsNothing(oDB) = False Then
                oDB.Dispose() : oDB = Nothing
            End If

        End Try

    End Function
End Class
