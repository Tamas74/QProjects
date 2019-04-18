Public Class clsCQMMeasure
    Public Shared _databaseConnectionString As String = String.Empty

    Public Shared Function GetdataWithParam(ByVal dbParameters As gloDatabaseLayer.DBParameters, ByVal SPName As String) As DataTable
        Dim oDB As gloDatabaseLayer.DBLayer = Nothing
        Try

            oDB = New gloDatabaseLayer.DBLayer(_databaseConnectionString)

            '  Dim oParameters As New gloDatabaseLayer.DBParameters
            oDB.Connect(False)
            Dim dt As DataTable = New DataTable()


            oDB.Retrive("" + SPName + "", dbParameters, dt)


            oDB.Disconnect()
            Return dt
        Catch ex As Exception
            'MessageBox.Show(ex.ToString, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If (Not IsNothing(oDB)) Then
                oDB.Dispose()
                oDB = Nothing
            End If

        End Try
    End Function
    Public Shared Function GetdataWithParamDataset(ByVal dbParameters As gloDatabaseLayer.DBParameters, ByVal SPName As String) As DataSet
        Dim oDB As gloDatabaseLayer.DBLayer = Nothing
        Try

            oDB = New gloDatabaseLayer.DBLayer(_databaseConnectionString)

            '  Dim oParameters As New gloDatabaseLayer.DBParameters
            oDB.Connect(False)
            Dim ds As DataSet = New DataSet()


            oDB.Retrive("" + SPName + "", dbParameters, ds)


            oDB.Disconnect()
            Return ds
        Catch ex As Exception
            'MessageBox.Show(ex.ToString, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If (Not IsNothing(oDB)) Then
                oDB.Dispose()
                oDB = Nothing
            End If

        End Try
    End Function
    Public Shared Sub InsertdataWithParam(ByVal dbParameters As gloDatabaseLayer.DBParameters, ByVal SPName As String)
        Dim oDB As gloDatabaseLayer.DBLayer = Nothing
        Try

            oDB = New gloDatabaseLayer.DBLayer(_databaseConnectionString)

            oDB.Connect(False)




            oDB.Execute("" + SPName + "", dbParameters)


            oDB.Disconnect()

        Catch ex As Exception
            'MessageBox.Show(ex.ToString, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If (Not IsNothing(oDB)) Then
                oDB.Dispose()
                oDB = Nothing
            End If
        End Try
    End Sub

    Public Shared Function GetdataWithOutParam(ByVal SPName As String) As DataTable
        Dim oDB As gloDatabaseLayer.DBLayer = Nothing
        Try

            oDB = New gloDatabaseLayer.DBLayer(_databaseConnectionString)
            ' Dim oParameter As gloDatabaseLayer.DBParameter
            'Dim oParameters As New gloDatabaseLayer.DBParameters
            oDB.Connect(False)
            Dim dt As DataTable = Nothing



            oDB.Retrive_Query("" + SPName + "", dt)


            oDB.Disconnect()
            Return dt
        Catch ex As Exception
            'MessageBox.Show(ex.ToString, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If (Not IsNothing(oDB)) Then
                oDB.Dispose()
                oDB = Nothing
            End If
        End Try
    End Function

    Public Shared Function GetdataWithOutParamDataset(ByVal SPName As String) As DataSet
        Dim oDB As gloDatabaseLayer.DBLayer = Nothing
        Try

            oDB = New gloDatabaseLayer.DBLayer(_databaseConnectionString)
            ' Dim oParameter As gloDatabaseLayer.DBParameter
            'Dim oParameters As New gloDatabaseLayer.DBParameters
            oDB.Connect(False)
            Dim ds As DataSet = Nothing



            oDB.Retrive_Query("" + SPName + "", ds)


            oDB.Disconnect()
            Return ds
        Catch ex As Exception
            'MessageBox.Show(ex.ToString, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If (Not IsNothing(oDB)) Then
                oDB.Dispose()
                oDB = Nothing
            End If
        End Try
    End Function
End Class
