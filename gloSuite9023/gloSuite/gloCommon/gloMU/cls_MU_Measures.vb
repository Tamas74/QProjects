#Region "Comment"
'Created By  : Shubhangi Gujar
'Created Date: 20100908
'Purpose     : View Form of MUDashboard.
#End Region

Imports System.Windows.Forms
Imports System.Data.SqlClient
Imports System.Xml

Public Class cls_MU_Measures
    Private dt As DataTable
    Private dv As DataView
    Dim appSettings As System.Collections.Specialized.NameValueCollection = System.Configuration.ConfigurationManager.AppSettings
    Private _databaseConnectionString As String = String.Empty
    Private _MessageBoxCaption As String = String.Empty

    Public ReadOnly Property GetDataView() As DataView
        Get
            Return dv
        End Get
    End Property
    Public Sub GenerateXML(ByVal oClsCQM As cls_MU_CQMSubmission)
        Try
            Dim xmlwriter As XmlTextWriter = Nothing
            Dim str As String
            str = oClsCQM.FilePath
            If System.IO.File.Exists(str & ".xml") Then
                System.IO.File.Delete(str & ".xml")
            End If
            xmlwriter = New XmlTextWriter(str & ".xml", Nothing)

            xmlwriter.WriteStartDocument()
            xmlwriter.WriteStartElement("submission") 'Start Submission
            xmlwriter.WriteAttributeString("type", "PQRI-REGISTRY")
            xmlwriter.WriteAttributeString("option", "PAYMENT")
            xmlwriter.WriteAttributeString("version", "2.0")
            xmlwriter.WriteAttributeString("xmlns:xsi", "http://www.w3.org/2001/XMLSchema-instance")
            xmlwriter.WriteAttributeString("xsi:noNamespaceSchemaLocation", "Registry_Payment.xsd")

            xmlwriter.WriteStartElement("file-audit-data") 'Start file-audit-data

            xmlwriter.WriteElementString("create-date", oClsCQM.CreateDate)
            xmlwriter.WriteElementString("create-time", oClsCQM.CreateTime)
            xmlwriter.WriteElementString("create-by", oClsCQM.CreateBy)
            xmlwriter.WriteElementString("version", oClsCQM.Version)
            xmlwriter.WriteElementString("file-number", oClsCQM.FileNumber)
            xmlwriter.WriteElementString("number-of-files", oClsCQM.NumberOfFiles)

            xmlwriter.WriteEndElement() 'End file-audit-data

            xmlwriter.WriteStartElement("registry") 'Start Registry

            xmlwriter.WriteElementString("registry-name", oClsCQM.RegistryName)
            xmlwriter.WriteElementString("registry-id", oClsCQM.RegistryID)
            xmlwriter.WriteElementString("submission-method", oClsCQM.SubmissionMethod)

            xmlwriter.WriteEndElement() 'End Registry

            xmlwriter.WriteStartElement("measure-group") 'Start Measure Group
            xmlwriter.WriteAttributeString("ID", oClsCQM.MeasureGroupID)
            'Provider
            xmlwriter.WriteStartElement("provider") 'Start Provider
            xmlwriter.WriteElementString("npi", oClsCQM.NPI)
            xmlwriter.WriteElementString("tin", oClsCQM.TIN)
            xmlwriter.WriteElementString("waiver-signed", oClsCQM.WaiverSign)
            xmlwriter.WriteElementString("encounter-from-date", oClsCQM.EnCounterFromDate)
            xmlwriter.WriteElementString("encounter-to-date", oClsCQM.EnCounterToDate)
            For Each oMsr As Measure In oClsCQM.Measures
                'start pqri-measure
                xmlwriter.WriteStartElement("pqri-measure")
                xmlwriter.WriteElementString("pqri-measure-number", oMsr.PQRIMEasureNumber)
                xmlwriter.WriteElementString("eligible-instances", oMsr.EligibleInstances)
                xmlwriter.WriteElementString("meets-performance-instances", oMsr.MeetPerformanceInstances)
                xmlwriter.WriteElementString("performance-exclusion-instances", oMsr.PerformanceExclusionInstances)
                xmlwriter.WriteElementString("performance-not-met-instances", oMsr.PerformanceNotMetInstances)
                xmlwriter.WriteElementString("reporting-rate", oMsr.ReportingRate)
                If oMsr.PerformanceRate.ToString() = "0" Then
                    xmlwriter.WriteStartElement("performance-rate")
                    xmlwriter.WriteElementString("xsi:nil", "true")
                    xmlwriter.WriteEndElement()
                Else
                    xmlwriter.WriteElementString("performance-rate", oMsr.PerformanceRate)
                End If

                xmlwriter.WriteEndElement()
                'End pqri-measure
            Next

            xmlwriter.WriteEndElement() 'Start Provider
            xmlwriter.WriteEndElement()
            xmlwriter.WriteEndElement()

            xmlwriter.WriteEndDocument()
            xmlwriter.Close()
        Catch ex As Exception
            MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Function GenerateFileName() As String
        Dim strfilename As String = ""
        strfilename = "CQM_"
        Dim dtdate As DateTime = Date.UtcNow
        Dim strtemp As String = strfilename & dtdate.Month.ToString & dtdate.Day.ToString & dtdate.Year.ToString & dtdate.Hour.ToString & dtdate.Minute.ToString & dtdate.Second.ToString & dtdate.Millisecond.ToString
        Return strtemp
    End Function

    Public Function FillMUMeasures()

        Dim oDB As New gloDatabaseLayer.DBLayer(_databaseConnectionString)
        Dim oResultTable As New DataTable

        Try
            oDB.Connect(False)

            oDB.Retrive("MU_FillMeasures_MST", dt)
            oDB.Disconnect()
            If Not dt Is Nothing Then
                dv = New DataView(dt)
                Return dv
            Else
                Return Nothing
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            oDB.Dispose()
        End Try

    End Function

    '27-Aug-13 Aniket: Creation of a new Dashboard for Stage 1
    Public Function FillMUMeasures_Stage1()

        Dim oDB As New gloDatabaseLayer.DBLayer(_databaseConnectionString)
        Dim oResultTable As New DataTable

        Try

            oDB.Connect(False)
            oDB.Retrive("MU_FillMeasures_MST_Stage1", dt)
            oDB.Disconnect()

            If Not dt Is Nothing Then
                dv = New DataView(dt)
                Return dv
            Else
                Return Nothing
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            oDB.Dispose()
        End Try

    End Function

    '27-Aug-13 Aniket: Creation of a new Dashboard for Stage 2
    Public Function FillMUMeasures_Stage2()

        Dim oDB As New gloDatabaseLayer.DBLayer(_databaseConnectionString)
        Dim oResultTable As New DataTable

        Try
            oDB.Connect(False)
            oDB.Retrive("MU_FillMeasures_MST_Stage2", dt)
            oDB.Disconnect()

            If Not dt Is Nothing Then
                dv = New DataView(dt)
                Return dv
            Else
                Return Nothing
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            oDB.Dispose()
        End Try

    End Function

    Public Function FillMUMeasures_Stage2_Modified()

        Dim oDB As New gloDatabaseLayer.DBLayer(_databaseConnectionString)
        Dim oResultTable As New DataTable

        Try
            oDB.Connect(False)
            oDB.Retrive("MU_FillMeasures_MST_Stage2_Modified", dt)
            oDB.Disconnect()

            If Not dt Is Nothing Then
                dv = New DataView(dt)
                Return dv
            Else
                Return Nothing
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            oDB.Dispose()
        End Try

    End Function

    Public Function FillMIPSACIMeasures(Optional ByVal ReportingYear As Integer = 0)

        Dim oDB As New gloDatabaseLayer.DBLayer(_databaseConnectionString)
        Dim oResultTable As New DataTable
        Dim oParameter As gloDatabaseLayer.DBParameter
        Dim oParameters As New gloDatabaseLayer.DBParameters

        Try
            oDB.Connect(False)
            oParameter = New gloDatabaseLayer.DBParameter
            oParameter.DataType = SqlDbType.SmallInt
            oParameter.ParameterDirection = ParameterDirection.Input
            oParameter.ParameterName = "@ReportingYear"
            oParameter.Value = ReportingYear
            oParameters.Add(oParameter)
            oParameter = Nothing
            oDB.Retrive("MIPS_FillMeasures_MST_ACI", oParameters, dt)
            oDB.Disconnect()

            If Not dt Is Nothing Then
                dv = New DataView(dt)
                Return dv
            Else
                Return Nothing
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            oDB.Dispose()
        End Try

    End Function

    Public Function Fill2017ACIMeasures()

        Dim oDB As New gloDatabaseLayer.DBLayer(_databaseConnectionString)
        Dim oResultTable As New DataTable

        Try
            oDB.Connect(False)
            oDB.Retrive("FillMeasures_MST_2017ACI", dt)
            oDB.Disconnect()

            If Not dt Is Nothing Then
                dv = New DataView(dt)
                Return dv
            Else
                Return Nothing
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            oDB.Dispose()
        End Try

    End Function
    '27-Aug-13 Aniket: Creation of a new Dashboard for Stage 2
    Public Sub DeleteMeasures_Stage1(ByVal nReportID As Long)

        Dim oDB As New gloDatabaseLayer.DBLayer(_databaseConnectionString)
        Dim oParameter As gloDatabaseLayer.DBParameter
        Dim oParameters As New gloDatabaseLayer.DBParameters
        oDB.Connect(False)

        Try

            oParameter = New gloDatabaseLayer.DBParameter
            oParameter.DataType = SqlDbType.BigInt

            oParameter.ParameterDirection = ParameterDirection.Input
            oParameter.ParameterName = "@nReportID"
            oParameter.Value = nReportID
            oParameters.Add(oParameter)
            oParameter = Nothing
            oDB.Execute("MU_DeleteMeasures_Stage1", oParameters)
            oDB.Disconnect()

        Catch ex As Exception
            MessageBox.Show(ex.ToString, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            oDB.Dispose()
        End Try
    End Sub

    '27-Aug-13 Aniket: Creation of a new Dashboard for Stage 2
    Public Sub DeleteMeasures_Stage2(ByVal nReportID As Long)

        Dim oDB As New gloDatabaseLayer.DBLayer(_databaseConnectionString)
        Dim oParameter As gloDatabaseLayer.DBParameter
        Dim oParameters As New gloDatabaseLayer.DBParameters
        oDB.Connect(False)

        Try

            oParameter = New gloDatabaseLayer.DBParameter
            oParameter.DataType = SqlDbType.BigInt

            oParameter.ParameterDirection = ParameterDirection.Input
            oParameter.ParameterName = "@nReportID"
            oParameter.Value = nReportID
            oParameters.Add(oParameter)
            oParameter = Nothing
            oDB.Execute("MU_DeleteMeasures_Stage2", oParameters)
            oDB.Disconnect()

        Catch ex As Exception
            MessageBox.Show(ex.ToString, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            oDB.Dispose()
        End Try
    End Sub

    Public Sub DeleteMeasures_Stage2_Modified(ByVal nReportID As Long)

        Dim oDB As New gloDatabaseLayer.DBLayer(_databaseConnectionString)
        Dim oParameter As gloDatabaseLayer.DBParameter
        Dim oParameters As New gloDatabaseLayer.DBParameters
        oDB.Connect(False)

        Try

            oParameter = New gloDatabaseLayer.DBParameter
            oParameter.DataType = SqlDbType.BigInt

            oParameter.ParameterDirection = ParameterDirection.Input
            oParameter.ParameterName = "@nReportID"
            oParameter.Value = nReportID
            oParameters.Add(oParameter)
            oParameter = Nothing
            oDB.Execute("MU_DeleteMeasures_Stage2_Modified", oParameters)
            oDB.Disconnect()

        Catch ex As Exception
            MessageBox.Show(ex.ToString, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            oDB.Dispose()
        End Try
    End Sub
    Public Sub DeleteMeasuresMIPSACI(ByVal nReportID As Long, Optional ByVal ReportingYear As Integer = 0)

        Dim oDB As New gloDatabaseLayer.DBLayer(_databaseConnectionString)
        Dim oParameter As gloDatabaseLayer.DBParameter
        Dim oParameters As New gloDatabaseLayer.DBParameters
        oDB.Connect(False)

        Try

            oParameter = New gloDatabaseLayer.DBParameter
            oParameter.DataType = SqlDbType.BigInt

            oParameter.ParameterDirection = ParameterDirection.Input
            oParameter.ParameterName = "@nReportID"
            oParameter.Value = nReportID
            oParameters.Add(oParameter)
            oParameter = Nothing

            oParameter = New gloDatabaseLayer.DBParameter
            oParameter.DataType = SqlDbType.SmallInt
            oParameter.ParameterDirection = ParameterDirection.Input
            oParameter.ParameterName = "@ReportingYear"
            oParameter.Value = ReportingYear
            oParameters.Add(oParameter)
            oParameter = Nothing

            oDB.Execute("MIPS_DeleteMeasures_ACI", oParameters)
            oDB.Disconnect()

        Catch ex As Exception
            MessageBox.Show(ex.ToString, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            oDB.Dispose()
        End Try
    End Sub
    Public Sub DeleteMeasures2017ACI(ByVal nReportID As Long)

        Dim oDB As New gloDatabaseLayer.DBLayer(_databaseConnectionString)
        Dim oParameter As gloDatabaseLayer.DBParameter
        Dim oParameters As New gloDatabaseLayer.DBParameters
        oDB.Connect(False)

        Try

            oParameter = New gloDatabaseLayer.DBParameter
            oParameter.DataType = SqlDbType.BigInt

            oParameter.ParameterDirection = ParameterDirection.Input
            oParameter.ParameterName = "@nReportID"
            oParameter.Value = nReportID
            oParameters.Add(oParameter)
            oParameter = Nothing
            oDB.Execute("DeleteMeasures_2017ACI", oParameters)
            oDB.Disconnect()

        Catch ex As Exception
            MessageBox.Show(ex.ToString, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            oDB.Dispose()
        End Try
    End Sub
    Public Sub DeleteMeasures(ByVal nReportID As Long)

        Dim oDB As New gloDatabaseLayer.DBLayer(_databaseConnectionString)
        Dim oParameter As gloDatabaseLayer.DBParameter
        Dim oParameters As New gloDatabaseLayer.DBParameters
        oDB.Connect(False)
        Try

            oParameter = New gloDatabaseLayer.DBParameter
            oParameter.DataType = SqlDbType.BigInt

            oParameter.ParameterDirection = ParameterDirection.Input
            oParameter.ParameterName = "@nReportID"
            oParameter.Value = nReportID
            oParameters.Add(oParameter)
            oParameter = Nothing
            oDB.Execute("MU_DeleteMeasures", oParameters)
            
            oDB.Disconnect()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            oDB.Dispose()
        End Try
    End Sub

#Region "GetProviders"

    Public Function GetProviders(ByVal ClinicID As Int64, Optional ByVal ProviderID As Int64 = 0) As DataTable

        Dim odb As New gloDatabaseLayer.DBLayer(_databaseConnectionString)
        Dim dtProvider As DataTable = Nothing
        Dim oParameter As gloDatabaseLayer.DBParameter = Nothing
        Dim oParameters As gloDatabaseLayer.DBParameters = Nothing

        Try

            odb.Connect(False)

            oParameter = New gloDatabaseLayer.DBParameter
            oParameters = New gloDatabaseLayer.DBParameters

            oParameter.DataType = SqlDbType.BigInt
            oParameter.ParameterDirection = ParameterDirection.Input
            oParameter.ParameterName = "@nClinicID"
            oParameter.Value = ClinicID
            oParameters.Add(oParameter)
            oParameter = Nothing

            oParameter = New gloDatabaseLayer.DBParameter
            oParameter.DataType = SqlDbType.BigInt
            oParameter.ParameterDirection = ParameterDirection.Input
            oParameter.ParameterName = "@nProviderID"
            oParameter.Value = ProviderID
            oParameters.Add(oParameter)
            oParameter = Nothing

            odb.Retrive("gsp_GetMUDashboardProviders", oParameters, dtProvider)

            odb.Disconnect()

            Return dtProvider

        Catch ex As Exception
            MessageBox.Show(ex.ToString, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Throw ex
            Return Nothing

        Finally

            If Not IsNothing(oParameters) Then
                oParameters.Dispose()
                oParameters = Nothing
            End If

        End Try
    End Function
    
#End Region

    Public Function GetTemplateName() As DataTable
        Dim oDB As New gloDatabaseLayer.DBLayer(_databaseConnectionString)
        Dim oParameter As gloDatabaseLayer.DBParameter = Nothing
        Dim dtTemplate As New DataTable()
        oDB.Connect(False)

        Try

            oParameter = New gloDatabaseLayer.DBParameter
            oDB.Retrive("GetTemplates", dtTemplate)

            Return dtTemplate

        Catch ex As Exception
            MessageBox.Show(ex.ToString, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Throw ex
            Return Nothing

        Finally
            If Not IsNothing(dtTemplate) Then
                dtTemplate.Dispose()
                dtTemplate = Nothing
            End If
            If Not IsNothing(oDB) Then
                oDB.Disconnect()
                oDB.Dispose()
                oDB = Nothing
            End If
            If Not IsNothing(oParameter) Then
                oParameter = Nothing
            End If

        End Try

    End Function
    Public Sub SaveExcludedTemplate(ByVal ArrTemplate As Collection, ByVal nClinicId As Int64)


        Dim odb As New gloDatabaseLayer.DBLayer(_databaseConnectionString)
        Dim objCon As New SqlConnection
        Dim dtProvider As New DataTable
        Dim _strSQL As String = ""
        Dim dt As New DataTable
        Dim _sqlQuery As String = ""
        Dim oParameter As gloDatabaseLayer.DBParameter = Nothing
        Dim oParameters As gloDatabaseLayer.DBParameters = Nothing
        Try
            odb.Connect(False)
            _strSQL = "DELETE FROM MU_ExcludedTemplates"

            odb.Execute_Query(_strSQL)
            odb.Disconnect()

            Dim dtTemplate As New DataTable()
            odb.Connect(False)

            For i = 1 To ArrTemplate.Count

                oParameter = New gloDatabaseLayer.DBParameter
                oParameters = New gloDatabaseLayer.DBParameters

                oParameter.DataType = SqlDbType.VarChar
                oParameter.ParameterDirection = ParameterDirection.Input
                oParameter.ParameterName = "@sCategoryName"
                oParameter.Value = CType(ArrTemplate.Item(i), gloGeneralItem.gloItem).Code
                oParameters.Add(oParameter)
                oParameter = Nothing

                oParameter = New gloDatabaseLayer.DBParameter
                oParameter.DataType = SqlDbType.VarChar
                oParameter.ParameterDirection = ParameterDirection.Input
                oParameter.ParameterName = "@sTemplateName"
                oParameter.Value = CType(ArrTemplate.Item(i), gloGeneralItem.gloItem).Description
                oParameters.Add(oParameter)
                oParameter = Nothing

                oParameter = New gloDatabaseLayer.DBParameter
                oParameter.DataType = SqlDbType.Int
                oParameter.ParameterDirection = ParameterDirection.Input
                oParameter.ParameterName = "@ClinicId"
                oParameter.Value = nClinicId
                oParameters.Add(oParameter)
                oParameter = Nothing

                '''''''''''''''''''''''''
                odb.Execute("MU_In_ExcludedTemplate", oParameters)

            Next
        Catch ex As Exception
            MessageBox.Show(ex.ToString, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally
            ArrTemplate = Nothing

            If Not IsNothing(odb) Then
                odb.Disconnect()
                odb.Dispose()
                odb = Nothing
            End If
            If Not IsNothing(oParameter) Then
                oParameter = Nothing
            End If
            If Not IsNothing(oParameters) Then
                oParameters.Dispose()
                oParameters = Nothing
            End If
            _strSQL = String.Empty
        End Try
    End Sub
    Public Function FillAssociation() As DataTable

        Dim odb As New gloDatabaseLayer.DBLayer(_databaseConnectionString)
        Dim objCon As New SqlConnection
        Dim objCmd As New SqlCommand
        Dim dtTemplates As New DataTable
        Dim _strSQL As String = ""

        Try
            odb.Connect(False)
            _strSQL = "SELECT ISNULL(sTemplateName,'') as sTemplateName,ISNULL(sCategoryName,'') as sCategoryName FROM MU_ExcludedTemplates "

            odb.Retrive_Query(_strSQL, dtTemplates)
            Return dtTemplates

        Catch ex As Exception
            MessageBox.Show(ex.ToString, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Throw ex
            Return Nothing

        Finally
            If Not IsNothing(dtTemplates) Then
                dtTemplates.Dispose()
                dtTemplates = Nothing
            End If
            If Not IsNothing(odb) Then
                odb.Disconnect()
                odb.Dispose()
                odb = Nothing
            End If
            _strSQL = String.Empty
            If Not IsNothing(objCmd) Then
                objCmd.Parameters.Clear()
                objCmd.Dispose()
                objCmd = Nothing
            End If
        End Try

    End Function

    Public Sub New()
        If appSettings("MessageBOXCaption") IsNot Nothing Then

            If appSettings("MessageBOXCaption") <> "" Then

                _MessageBoxCaption = Convert.ToString(appSettings("MessageBOXCaption"))
            End If
        End If
        If appSettings("DataBaseConnectionString") IsNot Nothing Then
            If appSettings("DataBaseConnectionString") <> "" Then


                _databaseConnectionString = Convert.ToString(appSettings("DataBaseConnectionString"))
            End If
        End If
    End Sub
End Class
