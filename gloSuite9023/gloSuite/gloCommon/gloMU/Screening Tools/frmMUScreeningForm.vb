Imports QSurvey.Models
Imports Newtonsoft.Json
Imports System.Net
Imports System.IO
Imports System.Reflection
Imports System.Data.SqlClient
Imports gloUserControlLibrary
Imports System.Windows.Forms

Public Class frmMUScreeningForm

#Region "Properties"

    Public Property Model As QSurvey.Models.Request
    Public Property ScreeningType As ScreeningEnum
    Public Property AppURL As String
    Public Property ConnectionString As String

    Public Property PatientID As Int64
    Public Property VisitID As Int64

    Public Property PatientAge As Int32
    Dim blnprocrequest As Boolean = False
    Private _navigatingURL As String

    Public Property SurveyType As ScreeningMode

    Public ReadOnly Property SurveyMode As String
        Get
            Select Case SurveyType
                Case gloMU.ScreeningMode.Create
                    Return "Survey"
                Case gloMU.ScreeningMode.Edit
                    Return "SurveyEdit"
                Case gloMU.ScreeningMode.View
                    Return "SurveyView"
                Case Else
                    Return "Survey"
            End Select
        End Get
    End Property

    Public ReadOnly Property NavigatingURL() As String
        Get
            Return Me.AppURL + Me.ScreeningType.ToString() + "\" + Me.ScreeningType.ToString() + SurveyMode
        End Get
    End Property


    Public ReadOnly Property GetModel() As String
        Get
            Return Me.AppURL + Me.ScreeningType.ToString() + "/GetModel"
        End Get
    End Property

#End Region

#Region "Constructors"

    Private patientStrip As gloUC_PatientStrip = Nothing

    Public Sub New(ByVal PatientID As Int64, ByVal ScreeningType As ScreeningEnum, ByVal AppURL As String)
        InitializeComponent()
        Me.ScreeningType = ScreeningType
        Me.AppURL = AppURL
        Me.PatientID = PatientID
        loadPatientStrip()
    End Sub

    Public Sub New(ByVal PatientID As Int64, ByVal Model As QSurvey.Models.Request, ByVal ScreeningType As ScreeningEnum, ByVal AppURL As String)
        Me.New(PatientID, ScreeningType, AppURL)
        Me.Model = Model
    End Sub
#End Region

    Private Sub loadPatientStrip()
        ''slr free previous memory
        If Not IsNothing(patientStrip) Then
            Me.Controls.Remove(patientStrip)
            patientStrip.Dispose()
            patientStrip = Nothing
        End If
        patientStrip = New gloUC_PatientStrip
        patientStrip.ShowDetail(PatientID, gloUC_PatientStrip.enumFormName.CCD)
        patientStrip.Dock = DockStyle.Top
        patientStrip.Padding = New Padding(3, 0, 3, 0)
        patientStrip.BringToFront()


        Me.Controls.Add(patientStrip)
        pnlToolStrip.SendToBack()
        pnlMain.BringToFront()
    End Sub

    Private Sub frmMUScreeningForm_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If SurveyType <> ScreeningMode.View Then
            If (blnprocrequest = False) Then
                processagain()
                blnprocrequest = True
            End If
        End If        
    End Sub

    Private Sub frmHOOSJR_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Dim encoding As System.Text.Encoding = System.Text.Encoding.UTF8
        Dim sFormData As String = Nothing
        Dim bts As Byte() = Nothing
        Try
            If Me.SurveyType <> ScreeningMode.View Then
                Me.FillProvider()
            Else
                pnlNEToolBar.Visible = False
            End If

            Me.Text = Me.ScreeningType.ToString() + " Survey"
            If Me.AppURL IsNot Nothing Then
                If Me.Model Is Nothing Then
                    Select Case Me.ScreeningType
                        Case ScreeningEnum.HoosHip
                            Me.Model = New HoosHipModel() With {.RequestID = Convert.ToInt64(DateTime.Now.ToString("yyyyMMddmmssfff"))}
                        Case ScreeningEnum.KoosKnee
                            Me.Model = New KoosKneeModel() With {.RequestID = Convert.ToInt64(DateTime.Now.ToString("yyyyMMddmmssfff"))}
                        Case ScreeningEnum.KoosJRKnee
                            Me.Model = New KoosJrKneeModel() With {.RequestID = Convert.ToInt64(DateTime.Now.ToString("yyyyMMddmmssfff"))}
                        Case ScreeningEnum.HoosJRHip
                            Me.Model = New HoosJRHipModel() With {.RequestID = Convert.ToInt64(DateTime.Now.ToString("yyyyMMddmmssfff"))}
                        Case ScreeningEnum.Promis29
                            Me.Model = New Promis29Model() With {.RequestID = Convert.ToInt64(DateTime.Now.ToString("yyyyMMddmmssfff"))}
                        Case ScreeningEnum.Promis10
                            Me.Model = New Promis10Model With {.RequestID = Convert.ToInt64(DateTime.Now.ToString("yyyyMMddmmssfff"))}
                        Case ScreeningEnum.VR12
                            Me.Model = New VR12Model With {.RequestID = Convert.ToInt64(DateTime.Now.ToString("yyyyMMddmmssfff"))}
                        Case ScreeningEnum.VR36
                            Me.Model = New VR36Model With {.RequestID = Convert.ToInt64(DateTime.Now.ToString("yyyyMMddmmssfff"))}
                        Case ScreeningEnum.PHQ9
                            Me.Model = New PHQ9Model() With {.RequestID = Convert.ToInt64(DateTime.Now.ToString("yyyyMMddmmssfff"))}
                        Case ScreeningEnum.PHQ2
                            Me.Model = New PHQ2Model(Me.PatientAge) With {.RequestID = Convert.ToInt64(DateTime.Now.ToString("yyyyMMddmmssfff"))}
                    End Select
                End If

                sFormData = JsonConvert.SerializeObject(Model)

                bts = encoding.GetBytes(sFormData)

                webBrowser.Navigate(NavigatingURL, String.Empty, bts, "Content-Type: application/json")
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.MUReports, gloAuditTrail.ActivityCategory.MUScreeningTools, gloAuditTrail.ActivityType.Save, "Screening opened", PatientID, Model.RequestID, Convert.ToInt64(cmbProvider.SelectedValue), gloAuditTrail.ActivityOutCome.Success)
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.MUReports, gloAuditTrail.ActivityCategory.MUScreeningTools, gloAuditTrail.ActivityType.Save, "Screening opened", PatientID, Model.RequestID, Convert.ToInt64(cmbProvider.SelectedValue), gloAuditTrail.ActivityOutCome.Failure)
        Finally
            encoding = Nothing
            bts = Nothing
            sFormData = Nothing
        End Try

    End Sub

    Private Sub tlbbtnClose_Click(sender As System.Object, e As System.EventArgs) Handles tlbbtnClose.Click
        Try
            If webBrowser IsNot Nothing Then
                webBrowser.Dispose()
                webBrowser = Nothing
            End If
            Me.Close()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.ScreeningTools, gloAuditTrail.ActivityCategory.MUScreeningTools, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub SaveModel(ByVal Model As Request, ByVal ScreeningType As ScreeningEnum)
        Try
            DBOperations.Save(PatientID, VisitID, Convert.ToInt64(cmbProvider.SelectedValue), ScreeningType.ToString(), patientStrip.DTPValue, Model, Me.ConnectionString)
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.MUReports, gloAuditTrail.ActivityCategory.MUScreeningTools, gloAuditTrail.ActivityType.Save, "Screening done", PatientID, Model.RequestID, Convert.ToInt64(cmbProvider.SelectedValue), gloAuditTrail.ActivityOutCome.Success)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.MUReports, gloAuditTrail.ActivityCategory.MUScreeningTools, gloAuditTrail.ActivityType.Save, ex.Message, gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub processagain()
        If Me.Model IsNot Nothing Then
            Dim screening As New SurveyOperations() With {.GetModel = Me.GetModel}
            Dim returnedModel As Object = Nothing
            returnedModel = screening.GetModelValue(Me.Model.RequestID, Me.ScreeningType)

            If returnedModel IsNot Nothing Then
                If (returnedModel.Save = "true") Then
                    Me.SaveModel(returnedModel, Me.ScreeningType)
                End If
            End If

            If Me.ScreeningType = ScreeningEnum.PHQ2 Then
                returnedModel = Nothing
                screening.GetModel = Me.AppURL + ScreeningEnum.PHQ9.ToString() + "/GetModel"
                returnedModel = screening.GetModelValue(Me.Model.RequestID + 1, ScreeningEnum.PHQ9)

                If returnedModel IsNot Nothing Then
                    If (returnedModel.Save = "true") Then
                        Me.SaveModel(returnedModel, ScreeningEnum.PHQ9)
                    End If
                End If
            End If

        End If
    End Sub

    Private dsProviderFillProvider As DataSet = Nothing

    Private Sub FillProvider()
        Try
            dsProviderFillProvider = DBOperations.GetProvidersInExam(PatientID, Me.ConnectionString)

            If IsNothing(dsProviderFillProvider) = False Then
                If dsProviderFillProvider.Tables("ExamProvider").Rows.Count > 0 Then
                    cmbProvider.Sorted = False
                    cmbProvider.DataSource = dsProviderFillProvider.Tables("ExamProvider")
                    cmbProvider.ValueMember = dsProviderFillProvider.Tables("ExamProvider").Columns("nProviderID").ColumnName
                    cmbProvider.DisplayMember = dsProviderFillProvider.Tables("ExamProvider").Columns("ProviderName").ColumnName
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "", MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.General, ex, gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub
End Class
