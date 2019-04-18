Imports System.Reflection
Imports QSurvey.Models
Imports System.Data
Imports System.Data.SqlClient
Imports Newtonsoft.Json
Imports System.Net
Imports System.IO

Public Enum ScreeningMode
    Create
    Edit
    View
End Enum

Public Enum ScreeningEnum
    HoosHip
    HoosJRHip
    KoosKnee
    KoosJRKnee
    Promis29
    Promis10
    VR12
    VR36
    PHQ9
    PHQ2
End Enum

Public Class DBOperations

    Public Shared Sub Save(ByVal PatientID As Int64, ByVal VisitID As Int64, ByVal ProviderID As Int64, ByVal FormName As String, ByVal SurveyDate As DateTime, ByVal Model As Request, ByVal ConnectionString As String)
        Dim dRow As DataRow = Nothing

        Try
            Using dtCategories As New DataTable("HoosKoosCategories")
                dtCategories.Columns.Add("sCategory", Type.GetType("System.String"))
                dtCategories.Columns.Add("sLOINCCode", Type.GetType("System.String"))
                dtCategories.Columns.Add("sSNOMEDCode", Type.GetType("System.String"))

                Using conn As New SqlConnection(ConnectionString)
                    Using comm As New SqlCommand("HOOS_INUP_Answers", conn) With {.CommandType = CommandType.StoredProcedure}

                        For Each element As Tuple(Of String, String, String) In Model.Categories
                            dRow = dtCategories.NewRow()
                            dRow("sCategory") = element.Item1
                            dRow("sLOINCCode") = element.Item2
                            dRow("sSNOMEDCode") = element.Item3
                            dtCategories.Rows.Add(dRow)
                            dRow = Nothing
                        Next

                        comm.Parameters.AddWithValue("@nPatientID", PatientID)
                        comm.Parameters.AddWithValue("@nVisitID", VisitID)
                        comm.Parameters.AddWithValue("@nProviderID", ProviderID)
                        comm.Parameters.AddWithValue("@sFormName", FormName)
                        comm.Parameters.AddWithValue("@sModel", JsonConvert.SerializeObject(Model))
                        comm.Parameters.AddWithValue("@dtSurveyDateTime", SurveyDate)
                        comm.Parameters.AddWithValue("@HOOSCategories", dtCategories)

                        comm.Connection.Open()
                        comm.ExecuteNonQuery()
                        comm.Connection.Close()
                    End Using
                End Using
            End Using
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.ScreeningTools, gloAuditTrail.ActivityCategory.MUScreeningTools, gloAuditTrail.ActivityType.Save, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Public Shared Function GetProvidersInExam(ByVal PatientID As Int64, ByVal ConnectionString As String) As DataSet        
        Dim ds As New DataSet()
        Try            
            Using conn As New SqlConnection(ConnectionString)
                Using comm As New SqlCommand("gsp_GetProvidersinExam", conn) With {.CommandType = CommandType.StoredProcedure}
                    comm.Parameters.AddWithValue("@nPatientID", PatientID)

                    Using da As New SqlDataAdapter(comm)
                        da.Fill(ds)
                    End Using

                    ds.Tables(0).TableName = "ExamProvider"
                    ds.Tables(1).TableName = "AllProvider"
                End Using
            End Using

            Return ds
        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Intervention, gloAuditTrail.ActivityType.Modify, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Return Nothing
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Intervention, gloAuditTrail.ActivityType.Modify, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Return Nothing        
        End Try
    End Function

    Public Shared Function GetScreeningObject(ByVal HoosID As Int64, ByVal ScreeningEnum As ScreeningEnum, ByVal ConnectionString As String) As Request
        Dim returnedModel As Request = Nothing
        Dim sJSONString As String = String.Empty

        Try
            Using conn As New SqlConnection(ConnectionString)
                Using comm As New SqlCommand("GetScreeningObject", conn) With {.CommandType = CommandType.StoredProcedure}
                    comm.Parameters.AddWithValue("@nHoosID", HoosID)
                    Using da As New SqlDataAdapter(comm)
                        Using ds As New DataSet()
                            da.Fill(ds)

                            If ds IsNot Nothing AndAlso ds.Tables.Count() > 0 Then
                                If ds.Tables(0).Rows.Count() > 0 Then
                                    sJSONString = ds.Tables(0).Rows(0)("sModel")
                                End If
                            End If
                        End Using
                    End Using
                End Using
            End Using

            If sJSONString <> String.Empty Then
                Select Case ScreeningEnum
                    Case ScreeningEnum.HoosHip
                        returnedModel = JsonConvert.DeserializeObject(Of HoosHipModel)(sJSONString)
                    Case ScreeningEnum.KoosKnee
                        returnedModel = JsonConvert.DeserializeObject(Of KoosKneeModel)(sJSONString)
                    Case ScreeningEnum.KoosJRKnee
                        returnedModel = JsonConvert.DeserializeObject(Of KoosJrKneeModel)(sJSONString)
                    Case ScreeningEnum.HoosJRHip
                        returnedModel = JsonConvert.DeserializeObject(Of HoosJRHipModel)(sJSONString)
                    Case ScreeningEnum.Promis29
                        returnedModel = JsonConvert.DeserializeObject(Of Promis29Model)(sJSONString)
                    Case ScreeningEnum.Promis10
                        returnedModel = JsonConvert.DeserializeObject(Of Promis10Model)(sJSONString)
                    Case ScreeningEnum.VR12
                        returnedModel = JsonConvert.DeserializeObject(Of VR12Model)(sJSONString)
                    Case ScreeningEnum.VR36
                        returnedModel = JsonConvert.DeserializeObject(Of VR36Model)(sJSONString)
                    Case ScreeningEnum.PHQ9
                        returnedModel = JsonConvert.DeserializeObject(Of PHQ9Model)(sJSONString)
                    Case ScreeningEnum.PHQ2
                        returnedModel = JsonConvert.DeserializeObject(Of PHQ2Model)(sJSONString)
                End Select
            End If

        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Intervention, gloAuditTrail.ActivityType.Modify, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Return Nothing
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Intervention, gloAuditTrail.ActivityType.Modify, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Return Nothing
        End Try

        Return returnedModel
    End Function

    Public Shared Sub DeleteScreening(ByVal HoosID As Int64, ByVal ConnectionString As String)
        Try
            Using conn As New SqlConnection(ConnectionString)
                Using comm As New SqlCommand("DeleteScreening", conn) With {.CommandType = CommandType.StoredProcedure}
                    comm.Parameters.AddWithValue("@nHoosID", HoosID)

                    comm.Connection.Open()
                    comm.ExecuteNonQuery()
                    comm.Connection.Close()
                End Using
            End Using

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Intervention, gloAuditTrail.ActivityType.Modify, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)            
        End Try
    End Sub

End Class

Public Class SurveyOperations
    Public Property GetModel As String

    Public Function GetModelValue(ByVal RequestID As Int64, ByVal ScreeningType As ScreeningEnum) As Object

        Dim bytesData As Byte() = Nothing
        Dim response As HttpWebResponse = Nothing
        Dim sJSONString As String = Nothing
        Dim model As Object = Nothing
        Dim webRequest As HttpWebRequest = Nothing
        Try
            bytesData = System.Text.Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(New With {.RequestID = RequestID}))

            webRequest = DirectCast(HttpWebRequest.Create(GetModel), HttpWebRequest)

            webRequest.Method = "POST"
            webRequest.ContentType = "application/JSON"
            webRequest.ContentLength = bytesData.Length

            Using s As Stream = webRequest.GetRequestStream()
                s.Write(bytesData, 0, bytesData.Length)
            End Using

            response = DirectCast(webRequest.GetResponse, HttpWebResponse)

            If response IsNot Nothing Then
                Using sr As New StreamReader(response.GetResponseStream())
                    sJSONString = sr.ReadToEnd

                    Select Case ScreeningType
                        Case ScreeningEnum.HoosHip
                            model = JsonConvert.DeserializeObject(Of HoosHipModel)(sJSONString)
                        Case ScreeningEnum.KoosKnee
                            model = JsonConvert.DeserializeObject(Of KoosKneeModel)(sJSONString)
                        Case ScreeningEnum.KoosJRKnee
                            model = JsonConvert.DeserializeObject(Of KoosJrKneeModel)(sJSONString)
                        Case ScreeningEnum.HoosJRHip
                            model = JsonConvert.DeserializeObject(Of HoosJRHipModel)(sJSONString)
                        Case ScreeningEnum.Promis29
                            model = JsonConvert.DeserializeObject(Of Promis29Model)(sJSONString)
                        Case ScreeningEnum.Promis10
                            model = JsonConvert.DeserializeObject(Of Promis10Model)(sJSONString)
                        Case ScreeningEnum.VR12
                            model = JsonConvert.DeserializeObject(Of VR12Model)(sJSONString)
                        Case ScreeningEnum.VR36
                            model = JsonConvert.DeserializeObject(Of VR36Model)(sJSONString)
                        Case ScreeningEnum.PHQ9
                            model = JsonConvert.DeserializeObject(Of PHQ9Model)(sJSONString)
                        Case ScreeningEnum.PHQ2
                            model = JsonConvert.DeserializeObject(Of PHQ2Model)(sJSONString)
                    End Select

                End Using
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.ScreeningTools, gloAuditTrail.ActivityCategory.MUScreeningTools, gloAuditTrail.ActivityType.Download, ex.Message, gloAuditTrail.ActivityOutCome.Failure)
        Finally
            bytesData = Nothing
            response = Nothing
            webRequest = Nothing
        End Try

        Return model
    End Function
End Class
