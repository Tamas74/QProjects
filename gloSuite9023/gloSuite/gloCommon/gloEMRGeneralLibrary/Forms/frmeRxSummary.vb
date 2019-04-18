Imports gloEMRGeneralLibrary.gloEMRPrescription
Imports gloSureScript
Imports System.IO
Imports gloEMRGeneralLibrary.gloGeneral

Imports schema = gloGlobal.Schemas.Surescript

Public Class frmeRxSummary
    Public Event CloseClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Public Event eRxClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Public Event ApproveClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Public Event ApprovewithChangesClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Public Event DTNFclick(ByVal sender As Object, ByVal e As System.EventArgs)
    Public Event Discardclick(ByVal sender As Object, ByVal e As System.EventArgs)
    Public Event DiscardAllclick(ByVal sender As Object, ByVal e As System.EventArgs)
    Private blnApprove As Boolean = False
    Private blnApprovewithchanges As Boolean = False
    Private blnDNTF As Boolean = False
    Private blnNewRx As Boolean = False
    Private blnnarcotic As Boolean = False
    Private mFileData As Byte() = Nothing
    Public _selectedItem As Integer = Nothing
    Private FileName As String = ""
    Public Property ReturnResponce As Int16 = -1
    Public Property ReturnFilePathtoSendForeRx As String = ""
    Public Property ReturnFilePathtoSendDNTFForeRx2 As String = ""

    Dim WithEvents ss_helper As gloSureScript.gloSurescriptsHelper = Nothing

    ''for any other btn click like sendnewRx/approve/approvewithchanges/dtnf we do not have to close the form.
    ''the form closing event will be called only when stop tranmission and [X] button is clicked on frmeRxSummary form. hence added the blnSSTranClicked boolean variable logic.
    Dim blnSSTranClicked As Boolean = False ''if the sendNewRx/approve/approvewithchanges button is clicked then do not call the form closing event.
    Public Property NewRx() As Boolean
        Get
            Return blnNewRx
        End Get
        Set(ByVal value As Boolean)
            blnNewRx = value
        End Set
    End Property
    Public Property Approve() As Boolean
        Get
            Return blnApprove
        End Get
        Set(ByVal value As Boolean)
            blnApprove = value
        End Set
    End Property
    Public Property selectedItem() As Integer
        Get
            Return _selectedItem
        End Get
        Set(ByVal value As Integer)
            _selectedItem = value
        End Set
    End Property
    Public Property Approvewithchanges() As Boolean
        Get
            Return blnApprovewithchanges
        End Get
        Set(ByVal value As Boolean)
            blnApprovewithchanges = value
        End Set
    End Property

    Public Property DNTF() As Boolean
        Get
            Return blnDNTF
        End Get
        Set(ByVal value As Boolean)
            blnDNTF = value
        End Set
    End Property
    'Public Property DTNFNarcotic() As Boolean
    '    Get
    '        Return blnnarcotic
    '    End Get
    '    Set(ByVal value As Boolean)
    '        blnnarcotic = value
    '    End Set
    'End Property
    Public Property FileData() As Byte()
        Get
            Return mFileData
        End Get
        Set(ByVal value As Byte())
            mFileData = value
        End Set
    End Property
    Public Property XMLFile As String
        Get
            Return FileName
        End Get
        Set(ByVal value As String)
            FileName = value
        End Set
    End Property
    Public dtMedication As DataTable
    Dim sTempApprovewithChangesFilePath As String = ""
    Dim sTempDNTFFilePath As String = ""
    Dim sTempDNTFFilePath2 As String = ""
    Public DNTFMessageID As String
    Dim _objgloPrescription As EPrescription
    Dim _objOldgloPrescription As EPrescription
    Public intIndex As Int16
    Public PotencyCode As String
    Public MDPotencyCode As String
    Dim status As String = ""
    Dim oglointerface As gloSureScript.gloSureScriptInterface = Nothing

    Dim sApproveWithChangesLabel As String = Nothing

    Private RxChangeMessageData As String = Nothing

    Private ReadOnly Property IsChangeRequest As Boolean
        Get
            If RxChangeMessageData IsNot Nothing Then
                Return True
            Else
                Return False
            End If
        End Get
    End Property

    Public Sub New(ByVal ogloPrescription As EPrescription, ByVal oOldgloPrescription As EPrescription)
        InitializeComponent()
        _objgloPrescription = ogloPrescription
        _objOldgloPrescription = oOldgloPrescription
    End Sub

    Public Sub New(ByVal ogloPrescription As EPrescription, ByVal fileData As String)
        InitializeComponent()
        _objgloPrescription = ogloPrescription
        RxChangeMessageData = fileData
    End Sub

    Private Sub frmeRxSummary_FormClosed(sender As Object, e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Try
            If Not IsNothing(oglointerface) Then
                oglointerface.Dispose()
                oglointerface = Nothing
            End If
            If Not IsNothing(dtMedication) Then
                dtMedication.Dispose()
                dtMedication = Nothing
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub frmeRxSummary_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing

        ''the form closing event will be called only when stop tranmission and [X] button is clicked on frmeRxSummary form.
        ''for any other btn click like sendnewRx/approve/approvewithchanges/dtnf we do not have to close the form.
        If blnSSTranClicked = False Then
            RaiseEvent CloseClick(sender, e)
            If Me.IsDisposed Then
                Exit Sub
            Else
                Me.Dispose()
            End If
        End If

    End Sub
    Dim eDrug As EDrug = Nothing

    Private Sub frmeRxSummary_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If IsChangeRequest Then
            sApproveWithChangesLabel = "Send this Approved with Changes"
            tlbbtnApprove.Text = "Send this Approved Rx"
        Else
            sApproveWithChangesLabel = "Send this Approved with Changes Rx Renewal"
            tlbbtnApprove.Text = "Send this Approved Rx Renewal"
        End If

        tlbbtnApprovewithChanges.Text = sApproveWithChangesLabel
        tlbbtnDTNF.Text = "Send this Modified Rx [DNTF]"
        tlbbtnNewRx.Text = "Send this New Rx"

        tlbbtnApprovewithChanges.Tag = "send"
        tlbbtnDTNF.Tag = "preview"
        tlbbtnApprovewithChanges.Text = sApproveWithChangesLabel
        tlbbtnDTNF.Text = "Preview this Modified Rx [DNTF]"

        tlbbtnApprove.Visible = Approve
        tlbbtnApprovewithChanges.Visible = Approvewithchanges
        tlbbtnDTNF.Visible = DNTF
        tlbbtnNewRx.Visible = NewRx

        oglointerface = New gloSureScript.gloSureScriptInterface
        ss_helper = New gloSureScript.gloSurescriptsHelper()

        Dim sFilepath As String = ""

        If NewRx = True Then
            status = gloSureScriptInterface.SentMessageType.eNewRx
            sFilepath = oglointerface.GenerateMultipleMU210dot6XMLforNewRx(_objgloPrescription, intIndex, 0)
            eDrug = _objgloPrescription.DrugsCol.Item(intIndex)
            tlbbtnComparedata.Visible = False
        ElseIf Approve = True Then
            status = gloSureScriptInterface.SentMessageType.eApproved

            If IsChangeRequest Then
                sFilepath = ss_helper.GenerateRxChangeResponse(_objgloPrescription, intIndex, RxChangeMessageData, "A")
            Else
                sFilepath = oglointerface.GenerateRefillResponse10dot6New(_objgloPrescription, status, "", "", "", 0)
            End If

            tlbbtnComparedata.Visible = False
        ElseIf Approvewithchanges = True And DNTF = True Then
            status = gloSureScriptInterface.SentMessageType.eApprovedWithChanges
            sFilepath = oglointerface.GenerateRefillResponse10dot6New(_objgloPrescription, status, "", "")
            sTempApprovewithChangesFilePath = sFilepath
            tlbbtnComparedata.Visible = True
        ElseIf Approvewithchanges = True Then
            tlbbtnApprovewithChanges.Tag = "send"
            tlbbtnApprovewithChanges.Text = sApproveWithChangesLabel
            status = gloSureScriptInterface.SentMessageType.eApprovedWithChanges
            If IsChangeRequest Then
                sFilepath = ss_helper.GenerateRxChangeResponse(_objgloPrescription, intIndex, RxChangeMessageData, "C")
            Else
                sFilepath = oglointerface.GenerateRefillResponse10dot6New(_objgloPrescription, status, "", "")
            End If

            sTempApprovewithChangesFilePath = sFilepath
            tlbbtnComparedata.Visible = True
        ElseIf DNTF = True Then
            tlbbtnDTNF.Tag = "send"
            tlbbtnDTNF.Text = "Send this Modified Rx [DNTF]"
            status = gloSureScriptInterface.SentMessageType.eDeniedWithNewRxToFollow
            sTempDNTFFilePath2 = oglointerface.GenerateRefillResponse10dot6New(_objOldgloPrescription, status, "", "")
            ReturnFilePathtoSendDNTFForeRx2 = sTempDNTFFilePath2

            Dim nOldDrugIndex As Int16 = -1
            If _objOldgloPrescription.DrugsCol IsNot Nothing AndAlso _objgloPrescription.DrugsCol.Count > 0 Then
                For i As Int32 = 0 To _objOldgloPrescription.DrugsCol.Count - 1
                    If _objgloPrescription.DrugsCol.Item(intIndex).RelatesToMessageId = _objOldgloPrescription.DrugsCol.Item(i).RelatesToMessageId Then
                        nOldDrugIndex = i
                        Exit For
                    End If
                Next
            End If

            If nOldDrugIndex > -1 Then
                sFilepath = oglointerface.GenerateMultipleMU210dot6XMLforNewRx(_objgloPrescription, intIndex, 0, "eDeniedWithNewRxToFollow", _objOldgloPrescription.DrugsCol.Item(nOldDrugIndex).MessageID, _objOldgloPrescription.DrugsCol.Item(nOldDrugIndex).RxReferenceNumber)
            Else
                sFilepath = oglointerface.GenerateMultipleMU210dot6XMLforNewRx(_objgloPrescription, intIndex, 0, "eDeniedWithNewRxToFollow")
            End If


            sTempDNTFFilePath = sFilepath
            tlbbtnComparedata.Visible = True
        End If

        If eDrug IsNot Nothing Then
            If eDrug.PCPrograms IsNot Nothing AndAlso eDrug.PCPrograms.Programs IsNot Nothing AndAlso eDrug.PCPrograms.Programs.Count > 0 Then
                tlbbtnPDR.Visible = True
            End If
        End If

        ReturnFilePathtoSendForeRx = LoadErxFile(sFilepath, PotencyCode, MDPotencyCode)

    End Sub

    Private Sub tlbbtnNewRx_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tlbbtnNewRx.Click
        blnSSTranClicked = True ''do not call form closing event

        If PotencyCode.Contains("C38046") Then ''''Unspecfied
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.Prescription, gloAuditTrail.ActivityType.Initialize, _objgloPrescription.DrugsCol.Item(intIndex).DrugName + " drug with Unspecified Unit of Measure(NewRx)." + " Provider ID " + _objgloPrescription.ProviderID, _objgloPrescription.PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
        End If

        ReturnResponce = 0
        Me.Close()
    End Sub

    Private Sub tlbbtnApprove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tlbbtnApprove.Click
        blnSSTranClicked = True ''do not call form closing event

        If PotencyCode.Contains("C38046") Then ''''Unspecfied
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.Prescription, gloAuditTrail.ActivityType.Initialize, _objgloPrescription.DrugsCol.Item(intIndex).DrugName + " drug with Unspecified Unit of Measure(Approve)." + " Provider ID " + _objgloPrescription.ProviderID, _objgloPrescription.PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
        End If

        ReturnResponce = 2
        Me.Close()
    End Sub

    Private Sub tlbbtnApprovewithChanges_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tlbbtnApprovewithChanges.Click
        blnSSTranClicked = True ''do not call form closing event

        If PotencyCode.Contains("C38046") Or MDPotencyCode.Contains("C38046") Then ''''Unspecfied
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.Prescription, gloAuditTrail.ActivityType.Initialize, _objgloPrescription.DrugsCol.Item(intIndex).DrugName + " drug with Unspecified Unit of Measure(ApprovewithChanges)." + " Provider ID " + _objgloPrescription.ProviderID, _objgloPrescription.PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
        End If


        If tlbbtnApprovewithChanges.Tag = "preview" Then
            status = gloSureScriptInterface.SentMessageType.eDeniedWithNewRxToFollow
            If sTempApprovewithChangesFilePath = "" Then
                sTempApprovewithChangesFilePath = oglointerface.GenerateRefillResponse10dot6New(_objgloPrescription, status, "", "")
            End If
            ReturnFilePathtoSendForeRx = LoadErxFile(sTempApprovewithChangesFilePath, PotencyCode, MDPotencyCode)
            tlbbtnApprovewithChanges.Tag = "send"
            tlbbtnApprovewithChanges.Text = sApproveWithChangesLabel
            tlbbtnDTNF.Tag = "preview"
            tlbbtnDTNF.Text = "Preview this Modified Rx [DNTF]"
            tlbbtnDTNF.ToolTipText = "Preview Deny with New Rx to Follow"
        Else
            ReturnResponce = 3
            Me.Close()
        End If


    End Sub

    Private Sub tlbbtnDTNF_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tlbbtnDTNF.Click
        blnSSTranClicked = True '' do not call form closing event

        If PotencyCode.Contains("C38046") Or MDPotencyCode.Contains("C38046") Then ''''Unspecfied
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.Prescription, gloAuditTrail.ActivityType.Initialize, _objgloPrescription.DrugsCol.Item(intIndex).DrugName + " drug with Unspecified Unit of Measure(DNTF)." + " Provider ID " + _objgloPrescription.ProviderID, _objgloPrescription.PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
        End If


        If tlbbtnDTNF.Tag = "preview" Then
            status = gloSureScriptInterface.SentMessageType.eDeniedWithNewRxToFollow
            If sTempDNTFFilePath2 = "" Then
                sTempDNTFFilePath2 = oglointerface.GenerateRefillResponse10dot6New(_objOldgloPrescription, status, "", "")
                ReturnFilePathtoSendDNTFForeRx2 = sTempDNTFFilePath2
            End If

            If sTempDNTFFilePath = "" Then
                sTempDNTFFilePath = oglointerface.GenerateMultipleMU210dot6XMLforNewRx(_objgloPrescription, intIndex, 0, "eDeniedWithNewRxToFollow", _objOldgloPrescription.DrugsCol.Item(intIndex).MessageID, _objOldgloPrescription.DrugsCol.Item(intIndex).RxReferenceNumber)
            End If
            ReturnFilePathtoSendForeRx = LoadErxFile(sTempDNTFFilePath, PotencyCode, MDPotencyCode)
            tlbbtnDTNF.Tag = "send"
            tlbbtnDTNF.Text = "Send this Modified Rx [DNTF]"
            tlbbtnDTNF.ToolTipText = "Send Deny with New Rx to Follow"
            tlbbtnApprovewithChanges.Tag = "preview"
            tlbbtnApprovewithChanges.Text = "Preview this Approved with Changes Rx Renewal"
        Else
            ReturnResponce = 1
            Me.Close()
        End If


    End Sub

    Private Sub tlbbtnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tlbbtnClose.Click

        ''the form closing event will be called only when stop tranmission and [X] button is clicked on frmeRxSummary form.
        blnSSTranClicked = False ''since we cannot dispose the form in formclosing event when new/approve/DTNF btn is clicked, set this variable to false so that we can dispose the form on form closing event.
        ReturnFilePathtoSendForeRx = ""
        ReturnFilePathtoSendDNTFForeRx2 = ""
        ReturnResponce = -1

        '''''email from phil dt: 28 Oct 2013 sub: v8000 notes
        'Dim dialogResult As DialogResult
        'dialogResult = System.Windows.Forms.MessageBox.Show("All the messages will be discarded. Do you wnat to continue?", "gloEMR", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        'If dialogResult = Windows.Forms.DialogResult.Yes Then
        'RaiseEvent CloseClick(sender, e)
        Me.Close()
        'Else
        'Exit Sub
        'End If
    End Sub

    Private Sub tlbbtnComparedata_Click(sender As System.Object, e As System.EventArgs) Handles tlbbtnComparedata.Click
        Dim sRxRequestType As String = Nothing

        Try
            If Not IsNothing(dtMedication) Then
                If IsChangeRequest Then
                    sRxRequestType = "RxChange"
                Else
                    sRxRequestType = "Refill"
                End If
                Using oApprove As New frmConfirmApproove(dtMedication, status, sRxRequestType)
                    oApprove.ShowDialog()
                End Using
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Function LoadErxFile(ByVal eRxFilePath As String, ByVal _PotencyCode As String, ByVal _MDPotencyCode As String) As String
        Dim strviewFile As String = String.Empty
        Dim _ResultPath As String = String.Empty

        Try
            If eRxFilePath <> "" Then
                strviewFile = gloGlobal.clsFileExtensions.NewDocumentName(clsgeneral.gstrTempDirPath, ".xml", "yyyyMMddhhmmssffff") 'DateTime.Now.ToString("yyyyMMddhhmmssffff") & System.Guid.NewGuid().ToString() & ".html"
                Dim strViewTransformfile As String = ""
                File.Copy(eRxFilePath, strviewFile)
                strViewTransformfile = oglointerface.ViewXML(strviewFile, _PotencyCode, _MDPotencyCode)
                XMLFile = strViewTransformfile
                'dtMedication = dtMedication
                If File.Exists(strviewFile) Then
                    File.Delete(strviewFile)
                End If
                WebBrowser1.Navigate("about:blank")
                If XMLFile <> "" Then
                    pnlWebBrowser.BringToFront()
                    WebBrowser1.Navigate(XMLFile)
                Else
                    pnlDrugSummary.BringToFront()
                End If
                _ResultPath = eRxFilePath
            End If
        Catch ex As Exception
            Throw ex
        End Try
        Return _ResultPath
    End Function

    Dim bProgramsBound As Boolean = False
    Private Sub btnPrv_Down_Click(sender As System.Object, e As System.EventArgs) Handles btnDown.Click
        Try
            pnlPDRListView.Visible = True            
            btnDown.Visible = False
            btnUp.Visible = True

            lstPDRPrograms.SmallImageList = Me.ImgList

            If bProgramsBound = False Then
                lstPDRPrograms.Clear()
                If eDrug IsNot Nothing Then
                    If eDrug.PCPrograms IsNot Nothing AndAlso eDrug.PCPrograms.Programs IsNot Nothing AndAlso eDrug.PCPrograms.Programs.Count > 0 Then
                        For Each program As gloGlobal.Schemas.PDR.Program In eDrug.PCPrograms.Programs
                            Dim lstItemView As New Windows.Forms.ListViewItem(program.name, 0)                            
                            lstPDRPrograms.Items.Add(lstItemView)
                            lstItemView = Nothing
                        Next
                        bProgramsBound = True                        
                    End If
                End If
            End If
            tlbbtnPDR.Tag = True
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub btnPrv_Up_Click(sender As System.Object, e As System.EventArgs) Handles btnUp.Click
        pnlPDRListView.Visible = False        
        btnDown.Visible = True
        btnUp.Visible = False
        tlbbtnPDR.Tag = False
    End Sub

    Private Sub tlbbtnPDR_Click(sender As System.Object, e As System.EventArgs) Handles tlbbtnPDR.Click
        Dim bPDRClicked As Boolean = False
        Try
            If TypeOf (tlbbtnPDR.Tag) Is Boolean Then
                bPDRClicked = DirectCast(tlbbtnPDR.Tag, Boolean)

                If bPDRClicked Then
                    btnUp.PerformClick()
                    tlbbtnPDR.Tag = False
                Else
                    btnDown.PerformClick()
                    tlbbtnPDR.Tag = True
                End If
            Else
                pnlPDRListViewHeader.Visible = True
                pnlPDRListViewHeader.SendToBack()
                pnlToostrip.SendToBack()
                btnDown.PerformClick()
                tlbbtnPDR.Tag = True
            End If
            
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class
