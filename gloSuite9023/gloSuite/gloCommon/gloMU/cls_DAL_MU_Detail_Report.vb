Imports System.Data.SqlClient
Imports System.Reflection

Public Class cls_DAL_MU_Detail_Report
    Dim _databaseConnectionString As String = ""

    Public Function GetdataWithParam(ByVal SPName As String, ByVal providerid As DataTable, ByVal startdate As String, ByVal enddate As String, ByVal _SingleProvider As Int64, ByVal TIN As String, ByVal IsReportingYear As Boolean) As DataSet

        Try

            Dim oDB As New gloDatabaseLayer.DBLayer(_databaseConnectionString)
            Dim oParameter As gloDatabaseLayer.DBParameter
            Dim oParameters As New gloDatabaseLayer.DBParameters
            oDB.Connect(False)

            Dim ds As New DataSet

            If IsNothing(providerid) = False Then
                oParameter = New gloDatabaseLayer.DBParameter()
                oParameter.ParameterName = "@TVP_Providers"
                oParameter.ParameterDirection = ParameterDirection.Input
                oParameter.DataType = SqlDbType.Structured
                oParameter.Value = providerid
                oParameters.Add(oParameter)
                oParameter = Nothing

                If IsNothing(TIN) = False AndAlso (TIN.ToUpper() <> "ALL") Then
                    oParameter = New gloDatabaseLayer.DBParameter()
                    oParameter.ParameterName = "@TIN"
                    oParameter.ParameterDirection = ParameterDirection.Input
                    oParameter.DataType = SqlDbType.NVarChar
                    oParameter.Value = TIN
                    oParameters.Add(oParameter)
                    oParameter = Nothing
                End If

            Else
                oParameter = New gloDatabaseLayer.DBParameter()
                oParameter.ParameterName = "@Provider"
                oParameter.ParameterDirection = ParameterDirection.Input
                oParameter.DataType = SqlDbType.BigInt
                oParameter.Value = _SingleProvider.ToString()
                oParameters.Add(oParameter)
                oParameter = Nothing
            End If


            oParameter = New gloDatabaseLayer.DBParameter()
            oParameter.ParameterName = "@FromDate"
            oParameter.ParameterDirection = ParameterDirection.Input
            oParameter.DataType = SqlDbType.VarChar
            oParameter.Value = startdate
            oParameters.Add(oParameter)
            oParameter = Nothing

            oParameter = New gloDatabaseLayer.DBParameter()
            oParameter.ParameterName = "@ToDate"
            oParameter.ParameterDirection = ParameterDirection.Input
            oParameter.DataType = SqlDbType.VarChar
            oParameter.Value = enddate
            oParameters.Add(oParameter)
            oParameter = Nothing

            oParameter = New gloDatabaseLayer.DBParameter()
            oParameter.ParameterName = "@IsMUDetailReport"
            oParameter.ParameterDirection = ParameterDirection.Input
            oParameter.DataType = SqlDbType.VarChar
            oParameter.Value = "1"
            oParameters.Add(oParameter)
            oParameter = Nothing


            If IsReportingYear = False Then
                oParameter = New gloDatabaseLayer.DBParameter()
                oParameter.ParameterName = "@ReportingYear"
                oParameter.ParameterDirection = ParameterDirection.Input
                oParameter.DataType = SqlDbType.Bit
                oParameter.Value = False
                oParameters.Add(oParameter)
                oParameter = Nothing
            End If

            oDB.Retrive("" + SPName + "", oParameters, ds)


            oDB.Disconnect()
            Return ds
        Catch ex As Exception
            'MessageBox.Show(ex.ToString, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End Try
    End Function
    Public Function GetdataWithExtraParam(ByVal SPName As String, ByVal providerid As String, ByVal startdate As String, ByVal enddate As String, ByVal extraParameter As Int16) As DataSet
        Try

            Dim oDB As New gloDatabaseLayer.DBLayer(_databaseConnectionString)
            Dim oParameter As gloDatabaseLayer.DBParameter
            Dim oParameters As New gloDatabaseLayer.DBParameters
            oDB.Connect(False)
            Dim ds As New DataSet

            oParameter = New gloDatabaseLayer.DBParameter()
            oParameter.ParameterName = "@ProviderID"
            oParameter.ParameterDirection = ParameterDirection.Input
            oParameter.DataType = SqlDbType.BigInt
            oParameter.Value = providerid.ToString()
            oParameters.Add(oParameter)
            oParameter = Nothing

            oParameter = New gloDatabaseLayer.DBParameter()
            oParameter.ParameterName = "@MeasurementStartDate"
            oParameter.ParameterDirection = ParameterDirection.Input
            oParameter.DataType = SqlDbType.VarChar
            oParameter.Value = startdate
            oParameters.Add(oParameter)
            oParameter = Nothing

            oParameter = New gloDatabaseLayer.DBParameter()
            oParameter.ParameterName = "@MeasurementEndDate"
            oParameter.ParameterDirection = ParameterDirection.Input
            oParameter.DataType = SqlDbType.VarChar
            oParameter.Value = enddate
            oParameters.Add(oParameter)
            oParameter = Nothing

            oParameter = New gloDatabaseLayer.DBParameter()
            oParameter.ParameterName = "@CQMDetailReport"
            oParameter.ParameterDirection = ParameterDirection.Input
            oParameter.DataType = SqlDbType.VarChar
            oParameter.Value = "1"
            oParameters.Add(oParameter)
            oParameter = Nothing

            oParameter = New gloDatabaseLayer.DBParameter()
            oParameter.ParameterName = "@N1"
            oParameter.ParameterDirection = ParameterDirection.Input
            oParameter.DataType = SqlDbType.Int
            oParameter.Value = extraParameter
            oParameters.Add(oParameter)
            oParameter = Nothing


            oDB.Retrive("" + SPName + "", oParameters, ds)


            oDB.Disconnect()
            Return ds
        Catch ex As Exception
            'MessageBox.Show(ex.ToString, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End Try
    End Function

    Public Function RetriveDataFromMultipleServer(ByVal SPName As String, ByVal providerid As String, ByVal startdate As String, ByVal enddate As String, ByVal extraParameter As Int16, Optional ByVal HistoryItemList As String = "") As DataSet
        Try

            Dim oDB As New gloDatabaseLayer.DBLayer(_databaseConnectionString)
            Dim oParameter As gloDatabaseLayer.DBParameter
            Dim oParameters As New gloDatabaseLayer.DBParameters
            oDB.Connect(False)
            Dim ds As New DataSet

            oParameter = New gloDatabaseLayer.DBParameter()
            oParameter.ParameterName = "@MeasurementStartDate"
            oParameter.ParameterDirection = ParameterDirection.Input
            oParameter.DataType = SqlDbType.VarChar
            oParameter.Value = startdate
            oParameters.Add(oParameter)
            oParameter = Nothing

            oParameter = New gloDatabaseLayer.DBParameter()
            oParameter.ParameterName = "@MeasurementEndDate"
            oParameter.ParameterDirection = ParameterDirection.Input
            oParameter.DataType = SqlDbType.VarChar
            oParameter.Value = enddate
            oParameters.Add(oParameter)
            oParameter = Nothing

            oParameter = New gloDatabaseLayer.DBParameter()
            oParameter.ParameterName = "@ProviderID"
            oParameter.ParameterDirection = ParameterDirection.Input
            oParameter.DataType = SqlDbType.VarChar
            oParameter.Value = providerid
            oParameters.Add(oParameter)
            oParameter = Nothing

            oParameter = New gloDatabaseLayer.DBParameter()
            oParameter.ParameterName = "@RxNorm_ServerName"
            oParameter.ParameterDirection = ParameterDirection.Input
            oParameter.DataType = SqlDbType.VarChar
            oParameter.Value = gloEMRGeneralLibrary.gloGeneral.clsgeneral.gRxNServerName
            oParameters.Add(oParameter)
            oParameter = Nothing

            oParameter = New gloDatabaseLayer.DBParameter()
            oParameter.ParameterName = "@RxNorm_DatabaseName"
            oParameter.ParameterDirection = ParameterDirection.Input
            oParameter.DataType = SqlDbType.VarChar
            oParameter.Value = gloEMRGeneralLibrary.gloGeneral.clsgeneral.gRxNDatabaseName
            oParameters.Add(oParameter)
            oParameter = Nothing

            oParameter = New gloDatabaseLayer.DBParameter()
            oParameter.ParameterName = "@Snomed_ServerName"
            oParameter.ParameterDirection = ParameterDirection.Input
            oParameter.DataType = SqlDbType.VarChar
            oParameter.Value = gloEMRGeneralLibrary.gloGeneral.clsgeneral.gstrSMDBServerName
            ''gstrSMDBServerName()
            oParameters.Add(oParameter)
            oParameter = Nothing

            oParameter = New gloDatabaseLayer.DBParameter()
            oParameter.ParameterName = "@Snomed_DatabaseName"
            oParameter.ParameterDirection = ParameterDirection.Input
            oParameter.DataType = SqlDbType.VarChar
            oParameter.Value = gloEMRGeneralLibrary.gloGeneral.clsgeneral.gstrSMDBDatabaseName
            oParameters.Add(oParameter)
            oParameter = Nothing

            oParameter = New gloDatabaseLayer.DBParameter()
            oParameter.ParameterName = "@CQMDetailReport"
            oParameter.ParameterDirection = ParameterDirection.Input
            oParameter.DataType = SqlDbType.VarChar
            oParameter.Value = "1"
            oParameters.Add(oParameter)
            oParameter = Nothing

            oParameter = New gloDatabaseLayer.DBParameter()
            oParameter.ParameterName = "@N1"
            oParameter.ParameterDirection = ParameterDirection.Input
            oParameter.DataType = SqlDbType.Int
            oParameter.Value = extraParameter
            oParameters.Add(oParameter)
            oParameter = Nothing


            oDB.Retrive("" + SPName + "", oParameters, ds)


            oDB.Disconnect()
            Return ds
        Catch ex As Exception
            '  MessageBox.Show(ex.ToString(), , MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End Try
    End Function
    Public Function RetriveData_0038and41(ByVal SPName As String, ByVal providerid As String, ByVal startdate As String, ByVal enddate As String, ByVal extraParameter As Int16, Optional ByVal HistoryItemList As String = "", Optional ByVal dtInfluenzavaccineSnoMed As DataTable = Nothing) As DataSet
        Try

            Dim oDB As New gloDatabaseLayer.DBLayer(_databaseConnectionString)
            Dim oParameter As gloDatabaseLayer.DBParameter
            Dim oParameters As New gloDatabaseLayer.DBParameters
            oDB.Connect(False)
            Dim ds As New DataSet

            oParameter = New gloDatabaseLayer.DBParameter()
            oParameter.ParameterName = "@MeasurementStartDate"
            oParameter.ParameterDirection = ParameterDirection.Input
            oParameter.DataType = SqlDbType.VarChar
            oParameter.Value = startdate
            oParameters.Add(oParameter)
            oParameter = Nothing

            oParameter = New gloDatabaseLayer.DBParameter()
            oParameter.ParameterName = "@MeasurementEndDate"
            oParameter.ParameterDirection = ParameterDirection.Input
            oParameter.DataType = SqlDbType.VarChar
            oParameter.Value = enddate
            oParameters.Add(oParameter)
            oParameter = Nothing

            oParameter = New gloDatabaseLayer.DBParameter()
            oParameter.ParameterName = "@ProviderID"
            oParameter.ParameterDirection = ParameterDirection.Input
            oParameter.DataType = SqlDbType.VarChar
            oParameter.Value = providerid
            oParameters.Add(oParameter)
            oParameter = Nothing

            oParameter = New gloDatabaseLayer.DBParameter()
            oParameter.ParameterName = "@RxNorm_ServerName"
            oParameter.ParameterDirection = ParameterDirection.Input
            oParameter.DataType = SqlDbType.VarChar
            oParameter.Value = gloEMRGeneralLibrary.gloGeneral.clsgeneral.gRxNServerName
            oParameters.Add(oParameter)
            oParameter = Nothing

            oParameter = New gloDatabaseLayer.DBParameter()
            oParameter.ParameterName = "@RxNorm_DatabaseName"
            oParameter.ParameterDirection = ParameterDirection.Input
            oParameter.DataType = SqlDbType.VarChar
            oParameter.Value = gloEMRGeneralLibrary.gloGeneral.clsgeneral.gRxNDatabaseName
            oParameters.Add(oParameter)
            oParameter = Nothing

            oParameter = New gloDatabaseLayer.DBParameter()
            oParameter.ParameterName = "@DIB_ServerName"
            oParameter.ParameterDirection = ParameterDirection.Input
            oParameter.DataType = SqlDbType.VarChar
            oParameter.Value = gloEMRGeneralLibrary.gloGeneral.clsgeneral.sMmwServerName
            oParameters.Add(oParameter)
            oParameter = Nothing

            oParameter = New gloDatabaseLayer.DBParameter()
            oParameter.ParameterName = "@DIB_DatabaseName"
            oParameter.ParameterDirection = ParameterDirection.Input
            oParameter.DataType = SqlDbType.VarChar
            oParameter.Value = gloEMRGeneralLibrary.gloGeneral.clsgeneral.sMmwDatabaseName
            oParameters.Add(oParameter)
            oParameter = Nothing


            oParameter = New gloDatabaseLayer.DBParameter()
            oParameter.ParameterName = "@CQMDetailReport"
            oParameter.ParameterDirection = ParameterDirection.Input
            oParameter.DataType = SqlDbType.VarChar
            oParameter.Value = "1"
            oParameters.Add(oParameter)
            oParameter = Nothing

            oParameter = New gloDatabaseLayer.DBParameter()
            oParameter.ParameterName = "@N1"
            oParameter.ParameterDirection = ParameterDirection.Input
            oParameter.DataType = SqlDbType.Int
            oParameter.Value = extraParameter
            oParameters.Add(oParameter)
            oParameter = Nothing


            oDB.Retrive("" + SPName + "", oParameters, ds)


            oDB.Disconnect()
            Return ds
        Catch ex As Exception
            '  MessageBox.Show(ex.ToString(), , MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End Try
    End Function




    Public Function Getdata(ByVal SPName As String) As DataSet
        Try

            Dim oDB As New gloDatabaseLayer.DBLayer(_databaseConnectionString)
            Dim oParameter As gloDatabaseLayer.DBParameter
            Dim oParameters As New gloDatabaseLayer.DBParameters
            oDB.Connect(False)
            Dim ds As New DataSet

            oParameter = New gloDatabaseLayer.DBParameter()
            oParameter.ParameterName = "@IsMUDetailReport"
            oParameter.ParameterDirection = ParameterDirection.Input
            oParameter.DataType = SqlDbType.VarChar
            oParameter.Value = "1"
            oParameters.Add(oParameter)
            oParameter = Nothing

            oDB.Retrive("" + SPName + "", oParameters, ds)

            oDB.Disconnect()
            Return ds
        Catch ex As Exception
            'MessageBox.Show(ex.ToString, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End Try
    End Function


    Public Function GetPatientName(ByVal dtDen As DataTable, ByVal dtNum As DataTable, ByVal dtDenominatorExclusions As DataTable, ByVal dtDenominatorExceptions As DataTable) As DataSet
        Try

            Dim oDB As New gloDatabaseLayer.DBLayer(_databaseConnectionString)
            Dim oParameter As gloDatabaseLayer.DBParameter
            Dim oParameters As New gloDatabaseLayer.DBParameters
            oDB.Connect(False)
            Dim dsResult As New DataSet


            oParameter = New gloDatabaseLayer.DBParameter()
            oParameter.ParameterName = "@DenominatorIDS"
            oParameter.ParameterDirection = ParameterDirection.Input
            oParameter.DataType = SqlDbType.Structured
            oParameter.Value = dtDen
            oParameters.Add(oParameter)
            oParameter = Nothing

            oParameter = New gloDatabaseLayer.DBParameter()
            oParameter.ParameterName = "@NumeratorIDS"
            oParameter.ParameterDirection = ParameterDirection.Input
            oParameter.DataType = SqlDbType.Structured
            oParameter.Value = dtNum
            oParameters.Add(oParameter)
            oParameter = Nothing

            oParameter = New gloDatabaseLayer.DBParameter()
            oParameter.ParameterName = "@DenominatorExclusionsIDs"
            oParameter.ParameterDirection = ParameterDirection.Input
            oParameter.DataType = SqlDbType.Structured
            oParameter.Value = dtDenominatorExclusions
            oParameters.Add(oParameter)
            oParameter = Nothing

            oParameter = New gloDatabaseLayer.DBParameter()
            oParameter.ParameterName = "@DenominatorExceptionsIDs"
            oParameter.ParameterDirection = ParameterDirection.Input
            oParameter.DataType = SqlDbType.Structured
            oParameter.Value = dtDenominatorExceptions
            oParameters.Add(oParameter)
            oParameter = Nothing

            oDB.Retrive("gsp_GetCqmPatientNames", oParameters, dsResult)
            oDB.Disconnect()
            Return dsResult

        Catch ex As Exception
            'MessageBox.Show(ex.ToString, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End Try
    End Function
    Public Function GetInfluenzaVaccineSnoMed() As DataTable
        Try

            Dim dtIVS As New DataTable
            Using oDIBGSHelper As New gloGlobal.DIB.gloGSHelper(gloEMRGeneralLibrary.gloGeneral.clsgeneral.sDIBServiceURL)
                Dim _InfluenzaVaccineSnoMed As gloGlobal.DIB.CQMResult = Nothing
                _InfluenzaVaccineSnoMed = oDIBGSHelper.CQMServiceCallGSDD("MU_NQF0041")
                If Not IsNothing(_InfluenzaVaccineSnoMed) Then
                    dtIVS = ConvertToDataTable(_InfluenzaVaccineSnoMed.NDCSet)
                End If
                ' dtIVS = ConvertToDataTable(oDIBGSHelper.CQMServiceCallGSDD("MU_NQF0041"))
            End Using

            Return dtIVS
        Catch ex As Exception
            'MessageBox.Show(ex.ToString, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End Try
    End Function

    Public Shared Function ConvertToDataTable(ByVal lstNdc As List(Of String)) As DataTable
        Try
            Dim table As New DataTable()
            If Not IsNothing(lstNdc) Then
                table.Columns.Add("NDCCode", Type.[GetType]("System.String"))
                For Each ndc As String In lstNdc
                    Dim row As DataRow = table.NewRow()
                    row("NDCCode") = ndc
                    table.Rows.Add(row)
                Next
            Else
                table = Nothing
            End If
            Return table
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Shared Function ConvertToDataTable1(Of T)(ByVal list As IList(Of T)) As DataTable
        Dim table As New DataTable()
        Dim fields() As PropertyInfo = GetType(T).GetProperties()
        For Each field As PropertyInfo In fields
            table.Columns.Add(field.Name, field.PropertyType)
        Next
        For Each item As T In list
            Dim row As DataRow = table.NewRow()
            For Each field As PropertyInfo In fields
                row(field.Name) = field.GetValue(item, Nothing)
            Next
            table.Rows.Add(row)
        Next
        Return table
    End Function
    Public Sub New(ByVal strConnectionString As String)
        _databaseConnectionString = strConnectionString
    End Sub
End Class
