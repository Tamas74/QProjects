Imports gloEMRGeneralLibrary
Imports System.IO

Public Class frmCancelRxResponses

    Private nPrescriptionID As Int64
    Public Property PrescriptionID() As Int64
        Get
            Return nPrescriptionID
        End Get
        Set(ByVal value As Int64)
            nPrescriptionID = value
        End Set
    End Property

    Private nPatientID As Int64
    Public Property PatientID() As Int64
        Get
            Return nPatientID
        End Get
        Set(ByVal value As Int64)
            nPatientID = value
        End Set
    End Property


    Private sXML As String
    Public Property XMLData() As String
        Get
            Return sXML
        End Get
        Set(ByVal value As String)
            sXML = value
        End Set
    End Property


    Public Sub New()
        InitializeComponent()
    End Sub

    Public Sub New(ByVal PrescriptionID As Int64, ByVal PatientID As Int64)
        Me.New()
        Me.PrescriptionID = PrescriptionID
        Me.PatientID = PatientID
        Me.LoadResponses(Me.PrescriptionID)
    End Sub

    Public Function LoadResponses(ByVal PrescriptionID As Int64) As String
        Try
            Using p As New PrescriptionBusinessLayer()
                Me.XMLData = p.GetRxMessageXMLByID(PrescriptionID, "CancelRxResponse")
            End Using

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.RxChangeRequest, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try

        Return Me.XMLData
    End Function

    Private Sub frmCancelRxResponses_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Try            
            Me.XMLtoHTMLFileLoad(XMLData)
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CancelRx, gloAuditTrail.ActivityType.View, "CancelRx response viewed", Me.PatientID, Me.PrescriptionID, gnLoginProviderID, gloAuditTrail.ActivityOutCome.Success)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.RxChangeRequest, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub DisplayRequestDetails()
        Dim dbLayer As New PrescriptionBusinessLayer()
        Dim sFileXML As String = String.Empty

        Try
            
            If (String.IsNullOrWhiteSpace(XMLData)) Then
                requestViewer.Visible = False
                requestViewer.Navigate("about:blank")
                DeleteHTMLFile(requestViewer.Tag)
            Else
                requestViewer.Visible = True

                XMLtoHTMLFileLoad(XMLData)
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.RxRequest, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not IsNothing(dbLayer) Then
                dbLayer.Dispose()
                dbLayer = Nothing
            End If
        End Try
    End Sub

    Private Sub DeleteHTMLFile(ByVal filetodelete As String)
        Try
            Dim sFileToDelete As String = Convert.ToString(filetodelete)
            If Not String.IsNullOrEmpty(sFileToDelete) Then
                If File.Exists(sFileToDelete) Then
                    File.Delete(sFileToDelete)
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.RxRequest, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub XMLtoHTMLFileLoad(ByVal strContent As String)

        Dim _firstTransformation As String = ""
        Dim _secondTransforamtion As String = ""
        Dim UniqueFileName As String = gloGlobal.clsFileExtensions.GetUniqueDateString()
        Dim _strfileName1 As String = ""
        Dim oglointerface As New gloSureScript.gloSureScriptInterface

        Try

            If (Not String.IsNullOrEmpty(strContent)) Then
                _firstTransformation = oglointerface.Transform(strContent, System.Windows.Forms.Application.StartupPath & "\namespaceremoval.xsl")
                _secondTransforamtion = oglointerface.Transform(_firstTransformation, System.Windows.Forms.Application.StartupPath & "\RxRequestSummary.xsl")
                _strfileName1 = gloSettings.FolderSettings.AppTempFolderPath & UniqueFileName & ".html"

                requestViewer.Navigate("about:blank")

                If _strfileName1 <> "" Then

                    DeleteHTMLFile(requestViewer.Tag)

                    File.WriteAllText(_strfileName1, _secondTransforamtion)

                    requestViewer.Navigate(_strfileName1)
                    requestViewer.Tag = _strfileName1
                End If
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.RxRequest, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not IsNothing(oglointerface) Then
                oglointerface.Dispose()
                oglointerface = Nothing
            End If
        End Try

    End Sub

    Private Sub tlbbtnClose_Click(sender As System.Object, e As System.EventArgs) Handles tlbbtnClose.Click
        Try
            DeleteHTMLFile(requestViewer.Tag)
            Me.Close()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.RxChangeRequest, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub
End Class