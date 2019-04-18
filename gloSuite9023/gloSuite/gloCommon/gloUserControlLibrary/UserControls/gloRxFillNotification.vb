Imports gloEMRGeneralLibrary
Imports System.IO
Imports schema = gloGlobal.Schemas.Surescript

Public Class gloRxFillNotification

    Private filetodelete = Nothing

    Private COL_nMessageID As Integer = 0
    Private COL_sPON As Integer = 1
    Private COL_FillDate As Integer = 3
    Private COL_sFillStatus As Integer = 2
    Private COL_nPatientID As Integer = 3
    Private COL_COUNT As Integer = 4

    Public Property PatientID As Int64 = 0
    Public Property PrescriberOrderNumber As Int64 = 0
    Public Property MessageID As String = Nothing

    Public Property rxRequestMsgID As String = "" 'This is the MessageID used by Pharmacies to send a RefillRequest

    Public Property SSMessageData As schema.MessageType = Nothing
    Public Property SSRxFillRequest As schema.RxFill = Nothing

    Private RxFillReqs As DataTable

    Public Property dtRxFillReqs() As DataTable
        Get
            Return RxFillReqs
        End Get
        Set(ByVal value As DataTable)
            RxFillReqs = value
        End Set
    End Property


    Public Sub New()
        InitializeComponent()
    End Sub

    Public Sub New(ByVal PatientId As Int64, ByVal PrescriptionID As Int64)
        Me.New()

        Me.PatientID = PatientId
        Me.PrescriberOrderNumber = PrescriptionID

        Using helper As New PrescriptionBusinessLayer()
            Me.dtRxFillReqs = helper.GetRxFillRequestsById(PrescriberOrderNumber, PrescriptionBusinessLayer.IDType.PrescriptionID)
        End Using
    End Sub

    Public Sub New(ByVal MessageID As String)
        Me.New()
        Me.MessageID = MessageID

        Using helper As New PrescriptionBusinessLayer()
            Me.dtRxFillReqs = helper.GetRxFillRequestsById(MessageID, PrescriptionBusinessLayer.IDType.MessageID)
        End Using
    End Sub


    Private Sub gloRxRequests_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load        
        Try
            If Me.dtRxFillReqs Is Nothing Then
                Me.RefreshMessages()
            End If            
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Public Sub RefreshMessages()
        Try
            If MessageID IsNot Nothing AndAlso MessageID <> "0" AndAlso MessageID <> "" Then
                Using helper As New PrescriptionBusinessLayer()
                    Me.dtRxFillReqs = helper.GetRxFillRequestsById(MessageID, PrescriptionBusinessLayer.IDType.MessageID)
                End Using
            ElseIf Me.PrescriberOrderNumber <> 0 Then
                Using helper As New PrescriptionBusinessLayer()
                    Me.dtRxFillReqs = helper.GetRxFillRequestsById(PrescriberOrderNumber, PrescriptionBusinessLayer.IDType.PrescriptionID)
                End Using
            End If
            DisplayRxFillRequestList()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub DisplayRxFillRequestList()
        Try
            SetC1FlexGrid()

            Dim i As Int32 = 0

            For Each row As DataRow In dtRxFillReqs.Rows
                With C1RxFillList
                    .Rows.Add()

                    .SetData(i + 1, COL_nMessageID, Convert.ToString(row("sMessageID")))
                    .SetData(i + 1, COL_sPON, Convert.ToString(row("nPrescriptionId")))
                    .SetData(i + 1, COL_FillDate, Convert.ToString(row("FillDate")))
                    If (Convert.ToString(row("FillStatus")) = "PartialFill") Then
                        .SetData(i + 1, COL_sFillStatus, "Partial filled")
                        .SetCellImage(i + 1, COL_sFillStatus, My.Resources.Rx_PartiallyFilled)
                    ElseIf (Convert.ToString(row("FillStatus")) = "Filled") Then
                        .SetData(i + 1, COL_sFillStatus, "Filled")
                        .SetCellImage(i + 1, COL_sFillStatus, My.Resources.Rx_Filled)
                    ElseIf (Convert.ToString(row("FillStatus")) = "NotFilled") Then
                        .SetData(i + 1, COL_sFillStatus, "Not filled")
                        .SetCellImage(i + 1, COL_sFillStatus, My.Resources.Rx_NotFilled)
                    End If
                    .SetData(i + 1, COL_nPatientID, Convert.ToString(row("nPatientID")))
                End With
                i = i + 1
            Next

            DisplaySelectedRequestDetails()

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Public Function SetC1FlexGrid()

        gloC1FlexStyle.Style(C1RxFillList)


        Try
            With C1RxFillList
                .Font = gloGlobal.clsgloFont.gFontArial_Regular 'New System.Drawing.Font("Arial", 9, System.Drawing.FontStyle.Regular)
                .SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
                ' .BackColor = System.Drawing.Color.White
                .AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None
                .Col = 0
                .Rows.Count = 1
                .Rows.Fixed = 1
                .Cols.Fixed = 0
                .Cols.Count = COL_COUNT
                .ExtendLastCol = True

                Dim _Width As Single = .Width / 10

                .Cols(COL_nMessageID).Width = 0
                .Cols(COL_sPON).Width = 0
                .Cols(COL_nPatientID).Width = 0

                .Cols(COL_FillDate).Width = _Width * 3
                .Cols(COL_sFillStatus).Width = _Width * 4
                .Cols(COL_nPatientID).Width = 0

                .SetData(0, COL_nMessageID, "MessageID")
                .SetData(0, COL_sPON, "PON")
                .SetData(0, COL_FillDate, "Fill Date")
                .SetData(0, COL_sFillStatus, "Status")
                .SetData(0, COL_nPatientID, "PatientID")

                .Cols(COL_nMessageID).Visible = False
                .Cols(COL_sPON).Visible = False
                .Cols(COL_FillDate).Visible = True
                .Cols(COL_sFillStatus).Visible = True
                .Cols(COL_nPatientID).Visible = False

                .Cols(COL_nMessageID).AllowEditing = False
                .Cols(COL_sPON).AllowEditing = False
                .Cols(COL_FillDate).AllowEditing = False
                .Cols(COL_sFillStatus).AllowEditing = False
                .Cols(COL_nPatientID).AllowEditing = False


                '.ForeColor = Color.Black
            End With

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.RxRequest, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Return Nothing
    End Function

    Private Sub DisplaySelectedRequestDetails()
        Dim dbLayer As New PrescriptionBusinessLayer()
        Dim sFileXML As String = String.Empty
        Dim sPatientID As String = ""
        Dim nPatientID As Int64 = 0

        pnlwbBrowser.Visible = True

        Try            
            If C1RxFillList.RowSel >= 0 Then
                rxRequestMsgID = Convert.ToString(C1RxFillList.GetData(C1RxFillList.RowSel, COL_nMessageID))
                sPatientID = Convert.ToString(C1RxFillList.GetData(C1RxFillList.RowSel, COL_nPatientID))
                Int64.TryParse(sPatientID, nPatientID)
            End If

            If (String.IsNullOrWhiteSpace(rxRequestMsgID)) Or (C1RxFillList.RowSel < 0) Then
                requestViewer.Visible = False
                requestViewer.Navigate("about:blank")
                DeleteHTMLFile()
            Else
                requestViewer.Visible = True
                sFileXML = dbLayer.GetRxMessageXMLByID(rxRequestMsgID)

                SetRxFillRequestObject(sFileXML)

                XMLtoHTMLFileLoad(sFileXML)                
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.RxFill, gloAuditTrail.ActivityType.View, "RxFill notification viewed for MessageID " + rxRequestMsgID, nPatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success)
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

                    DeleteHTMLFile()

                    File.WriteAllText(_strfileName1, _secondTransforamtion)

                    requestViewer.Navigate(_strfileName1)
                    requestViewer.Tag = strContent
                    pnlwbBrowser.Tag = _strfileName1
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


    Private Sub DeleteHTMLFile()
        Try
            Dim sFileToDelete As String = Convert.ToString(filetodelete)
            If Not String.IsNullOrEmpty(sFileToDelete) Then
                If File.Exists(sFileToDelete) Then
                    File.Delete(sFileToDelete)
                End If
            End If
        Catch ex As Exception
            'gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.RxRequest, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            '  MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Public Sub SetRxFillRequestObject(ByVal sXML As String)

        Dim xmlSerializer As Xml.Serialization.XmlSerializer = Nothing

        If sXML IsNot Nothing Then
            Using reader As New StringReader(sXML)
                SSMessageData = New schema.MessageType()
                xmlSerializer = New Xml.Serialization.XmlSerializer(SSMessageData.GetType())
                SSMessageData = xmlSerializer.Deserialize(reader)
            End Using
        End If

        If SSMessageData IsNot Nothing Then
            If SSMessageData.Header IsNot Nothing Then
                'selectedPharmacyNCPDP = SSMessageData.Header.From.Value
                'selectedProviderSPI = SSMessageData.Header.To.Value
            End If
            If SSMessageData.Body IsNot Nothing AndAlso SSMessageData.Body.Item IsNot Nothing Then
                If TypeOf (SSMessageData.Body.Item) Is schema.RxFill Then
                    SSRxFillRequest = DirectCast(SSMessageData.Body.Item, schema.RxFill)
                End If
            End If
        End If
        'SSMessageData = Nothing
        xmlSerializer = Nothing
    End Sub


    Private Sub C1RxFillList_BeforeSelChange(sender As Object, e As C1.Win.C1FlexGrid.RangeEventArgs) Handles C1RxFillList.BeforeSelChange
        filetodelete = pnlwbBrowser.Tag
    End Sub

    Private Sub C1RxFillList_MouseMove(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles C1RxFillList.MouseMove
        gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, sender, e.Location)
    End Sub

    Private Sub C1RxFillList_SelChange(sender As Object, e As System.EventArgs) Handles C1RxFillList.SelChange
        DisplaySelectedRequestDetails()
    End Sub
End Class
