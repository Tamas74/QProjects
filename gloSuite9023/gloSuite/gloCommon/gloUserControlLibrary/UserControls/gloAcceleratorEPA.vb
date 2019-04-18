Imports EPA = gloGlobal.EPA
Imports System.Linq
Imports gloEMRGeneralLibrary
Imports System.Threading.Tasks

Public Class gloAcceleratorEPA

#Region "Properties and Variables"
    Dim _userID As String
    Dim _serviceURL As String
    Dim _serviceType As EPA.ServiceType
    Dim _paReferenceID As String = String.Empty

    Dim helper As EPA.gloEPAHelper = Nothing
    Dim Token As EPA.AuthResponse = Nothing
    Dim EPADatabaseLayer As gloGeneral.EPABusinesslayer = Nothing

    Public Property AcceleratorBaseUrl As String

    Private ReadOnly Property AcceleratorURL As String
        Get
            If Me._serviceType = EPA.ServiceType.Worklist Then
                Return AcceleratorBaseUrl + "worklist/"
            ElseIf Me._serviceType = EPA.ServiceType.WorkProcess Then
                Return AcceleratorBaseUrl + "process/"
            ElseIf Me._serviceType = EPA.ServiceType.Process Then
                Return AcceleratorBaseUrl + "process/" + _paReferenceID
            Else
                Return AcceleratorBaseUrl
            End If
        End Get
    End Property
#End Region

#Region "Constructor"
    Public Sub New(ByVal UserID As String, ByVal ServiceURL As String, ByVal BaseURL As String)
        InitializeComponent()
        Try
            Me._userID = UserID
            Me._serviceURL = ServiceURL
            Me.AcceleratorBaseUrl = BaseURL
            Me.EPADatabaseLayer = New gloGeneral.EPABusinesslayer()
            Me.helper = New EPA.gloEPAHelper(ServiceURL)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub
#End Region

#Region "Navigate Procedure"

    Public Sub Navigate(ByVal ProviderID As Int64, ByVal ServiceType As EPA.ServiceType)
        Try
            Me._serviceType = ServiceType
            Dim DataRow As DataRow = Me.EPADatabaseLayer.GetUserRole(Me._userID, ProviderID)
            Dim EPARole As EPA.RoleType = EPA.RoleType.None
            Dim localDocument As String = Application.StartupPath + "\HtmlPages\EPADocument.htm"

            If DataRow IsNot Nothing Then
                [Enum].TryParse(Convert.ToInt16(DataRow("nPARoleID")), EPARole)
                Token = helper.GetAuthenticationToken(Me._userID, Convert.ToInt64(DataRow("sSPIID")), EPARole)
                Me.webBrowser.Navigate(localDocument, False)
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Public Sub Navigate(ByVal ProviderID As Int64, ByVal PAReferenceID As String)
        Try
            Me._paReferenceID = PAReferenceID
            Me.Navigate(ProviderID, EPA.ServiceType.Process)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

#End Region
    
#Region "Web Browser Completed Event"

    Private Sub webBrowser_DocumentCompleted(sender As System.Object, e As System.Windows.Forms.WebBrowserDocumentCompletedEventArgs) Handles webBrowser.DocumentCompleted
        Dim htmlToken As HtmlElement
        Dim htmlButton As HtmlElement
        Dim localDocument As String = Application.StartupPath + "\HtmlPages\EPADocument.htm"
        Try
            If webBrowser.Url.LocalPath = localDocument Then
                htmlToken = webBrowser.Document.GetElementById("KeyToken")
                htmlToken.InnerText = Me.Token.token
                webBrowser.Document.GetElementById("AcceleratorPage").SetAttribute("action", AcceleratorURL)
                htmlButton = webBrowser.Document.GetElementById("SubmitXML")

                If htmlButton IsNot Nothing Then
                    htmlButton.InvokeMember("Click")
                End If
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.CreateAuditLog(ex.Message, gloAuditTrail.ActivityCategory.BreakTheGlass, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

#End Region

#Region "Accelerator Load"
    Private Sub gloAcceleratorEPA_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.EPA, gloAuditTrail.ActivityCategory.PriorAuthorization, gloAuditTrail.ActivityType.View, "EPA Accelerator loaded", gloAuditTrail.ActivityOutCome.Success)
    End Sub
#End Region
    
End Class
