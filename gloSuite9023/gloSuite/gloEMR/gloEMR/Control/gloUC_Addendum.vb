Imports System.Data.SqlClient
Imports gloEMR.gloEMRWord
Imports Wd = Microsoft.Office.Interop.Word
Imports System.IO

Public Class gloUC_Addendum

    Implements IDisposable
    Implements IHotKey
    Dim _PatientID As Long

#Region " Constructor "
    Public Sub New(ByVal nVisitID As Int64, ByVal nPrimaryID As Int64, ByVal PatientID As Long, Optional ByVal isExam As Boolean = False)
        _VisitID = nVisitID
        _PrimaryID = nPrimaryID
        _PatientID = PatientID
        _isExam = isExam
        InitializeComponent()
    End Sub
#End Region

#Region " Private Variables "

    Private _VisitID As Int64
    Private _PrimaryID As Int64
    Private _ProviderID As Int64
    Private _ShowTemplate As Boolean = True
    Private _FilePath As String
    Private _ImagePath As String

    ' Private WithEvents wdTemp As AxDSOFramer.AxFramerControl
    Private WithEvents wdAddendum As AxDSOFramer.AxFramerControl
    Private WithEvents wdWordOptimizerDso As AxDSOFramer.AxFramerControl
    Private WithEvents oCurDoc As Wd.Document
    ' Private WithEvents oTempDoc As Wd.Document
    Friend WithEvents tlsAddendum As WordToolStrip.gloWordToolStrip
    Private oWord As clsWordDocument
    Private oCriteria As DocCriteria
    'Bug #62711: gloEMR > finished nurse notes/patient consent/patient letter > Addendum > Capture signature button > selected file not inserted in word document.
    Private frmSignature As FrmSignature

    Private arrTemplateList As ArrayList
    Private trvSearchNode As TreeNode 'Search node
    Private ImagePath As String = ""
    Private blnSignClick As Boolean = False
    Private _isExam As Boolean = False


    Private pLoadWordApplication As gloWord.LoadAndCloseWord = Nothing

    Private Function GetMyLoadWordApplication() As gloWord.LoadAndCloseWord
        If (IsNothing(pLoadWordApplication)) Then
            pLoadWordApplication = New gloWord.LoadAndCloseWord()
            pLoadWordApplication.LoadApplicationOnly()
        Else
            If (pLoadWordApplication.CheckWordApplicationLocked()) Then
                pLoadWordApplication = New gloWord.LoadAndCloseWord()
                pLoadWordApplication.LoadApplicationOnly()
            End If
        End If
        Return pLoadWordApplication
    End Function

    Private Sub CloseMyLoadWordApplication()
        If (IsNothing(pLoadWordApplication) = False) Then
            pLoadWordApplication.CloseApplicationOnly()
            pLoadWordApplication = Nothing
        End If
    End Sub

#End Region

#Region " Public Properties "

    Property ShowTemplates() As Boolean
        Get
            Return _ShowTemplate
        End Get
        Set(ByVal value As Boolean)
            _ShowTemplate = value
        End Set
    End Property

    ReadOnly Property FilePath() As String
        Get
            Return _FilePath
        End Get
    End Property

#End Region

#Region " Control Events "

    Public Event OnAddendumSaved(ByVal sender As Object, ByVal e As ToolStripItemClickedEventArgs)
    Public Event OnAddendumClose(ByVal sender As Object, ByVal e As EventArgs)

    Private Sub gloUC_Addendum_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            pnlTemplates.Visible = _ShowTemplate

            '' LOAD ADDENDUM TOOLSTRIP ''
            tlsAddendum = New WordToolStrip.gloWordToolStrip
            tlsAddendum.FormType = WordToolStrip.enumControlType.ExamAddendum
            tlsAddendum.IsCoSignEnabled = gblnCoSignFlag
            tlsAddendum.Dock = DockStyle.Top
            tlsAddendum.ConnectionString = GetConnectionString()
            tlsAddendum.UserID = gnLoginID

            If _isExam = True Then
                tlsAddendum._IsExamAddedndum = _isExam
            Else
                tlsAddendum._IsExamAddedndum = False
            End If

            Dim oProvider As New clsProvider
            '  Dim exmProviderID As Int64

            tlsAddendum.ptProvider = oProvider.GetPatientProviderName(_PatientID)
            tlsAddendum.ptProviderId = oProvider.GetPatientProvider(_PatientID)
            tlsAddendum.dtInput = oProvider.GetAllAssignProviders(gnLoginID)
            Me.Controls.Add(tlsAddendum)

            If gblnVoiceEnabled = True AndAlso gblnSpeakerExists = True Then
                tlsAddendum.MyToolStrip.Items("Mic").Visible = True
            End If

            AddHandler tlsAddendum.ToolStripClick, AddressOf onToolStripClick

            Dim dt As DataTable

            dt = oProvider.GetAllAssignProviders(gnLoginID)
            oProvider.Dispose()
            oProvider = Nothing
           
            If gblnAssociatedProviderSignature Then
                tlsAddendum.MyToolStrip.Items("Insert Associated Provider Signature").Visible = True
                If (IsNothing(dt) = False) Then
                    If dt.Rows.Count > 0 Then
                        tlsAddendum.MyToolStrip.Items("Insert Associated Provider Signature").Enabled = True
                    Else
                        'tlsAddendum.MyToolStrip.Items("Insert Associated Provider Signature").Enabled = False
                    End If
                End If
                
                tlsAddendum.MyToolStrip.ButtonsToHide.Remove(tlsAddendum.MyToolStrip.Items("Insert Associated Provider Signature").Name)
            Else
                tlsAddendum.MyToolStrip.Items("Insert Associated Provider Signature").Visible = False
                If (tlsAddendum.MyToolStrip.ButtonsToHide.Contains(tlsAddendum.MyToolStrip.Items("Insert Associated Provider Signature").Name) = False) Then
                    tlsAddendum.MyToolStrip.ButtonsToHide.Add(tlsAddendum.MyToolStrip.Items("Insert Associated Provider Signature").Name)
                End If
            End If
            If (IsNothing(dt) = False) Then
                dt.Dispose()
                dt = Nothing
            End If
            

            'FILL TEMPLATES

            If _VisitID > 0 Or _PrimaryID > 0 Then
                FillTemplatesTree(0)
            End If

            'LOAD WORD CONTROL
            wdAddendum = New AxDSOFramer.AxFramerControl
            wdWordOptimizerDso = New AxDSOFramer.AxFramerControl
            Try
                wdWordOptimizerDso.ActivationPolicy = DSOFramer.dsoActivationPolicy.dsoKeepUIActiveOnAppDeactive
            Catch ex As Exception

            End Try
            Me.Controls.Add(wdAddendum)
            wdAddendum.Dock = DockStyle.Fill
            wdAddendum.BringToFront()
            Try
                wdAddendum.ActivationPolicy = DSOFramer.dsoActivationPolicy.dsoKeepUIActiveOnAppDeactive
            Catch ex As Exception

            End Try
            wdAddendum.Menubar = False
            wdAddendum.Titlebar = False
            wdAddendum.CreateNew("Word.Document")




            'INITIALISE CURRENT DOCUMENT
            oCurDoc = wdAddendum.ActiveDocument
            oCurDoc.ActiveWindow.SetFocus()

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            ex = Nothing
        Finally
            If MyMDIParent IsNot Nothing Then
                MyMDIParent.RegisterMyHotKey()
                MyMDIParent.ActiveDSO = wdAddendum
            End If
        End Try

        Try
            wdWordOptimizerDso.CreateNew("Word.Document")
        Catch ex As Exception

        End Try

    End Sub

    Friend Sub onToolStripClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs)
        Try
            Select Case e.ClickedItem.Name

                Case "Mic"
                    gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.VoiceCommands, gloAuditTrail.ActivityType.Load, "SwitchOff Mic started from tblButtons_ButtonClick in Patient Messages when " & e.ClickedItem.Name & " is invoked", gloAuditTrail.ActivityOutCome.Success)
                    If MyMDIParent.DgnMicBtn1.MicState = DNSTools.DgnMicStateConstants.dgnmicOn Then
                        MyMDIParent.DgnMicBtn1.MicState = DNSTools.DgnMicStateConstants.dgnmicOff
                        e.ClickedItem.Image = Global.gloEMR.My.Resources.Mic_OFF
                        e.ClickedItem.ToolTipText = "Microphone Off"
                    ElseIf MyMDIParent.DgnMicBtn1.MicState = DNSTools.DgnMicStateConstants.dgnmicOff Then
                        MyMDIParent.DgnMicBtn1.MicState = DNSTools.DgnMicStateConstants.dgnmicOn
                        e.ClickedItem.Image = Global.gloEMR.My.Resources.Mic_ON
                        e.ClickedItem.ToolTipText = "Microphone On"
                    End If
                    gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.VoiceCommands, gloAuditTrail.ActivityType.Load, "witchOff Mic Completed from tblButtons_ButtonClick in Patient Messages when " & e.ClickedItem.Name & " is invoked", gloAuditTrail.ActivityOutCome.Success)

                Case "Save"
                    SaveAddendum()
                    RaiseEvent OnAddendumSaved(sender, e)

                Case "Close"
                    Try
                        If gblnVoiceEnabled = True AndAlso gblnSpeakerExists = True Then
                            UpdateVoiceLog("Mic Switched On/Off started at Addendum Closed Event")
                            If MyMDIParent.DgnMicBtn1.MicState = DNSTools.DgnMicStateConstants.dgnmicOn Then
                                MyMDIParent.DgnMicBtn1.MicState = DNSTools.DgnMicStateConstants.dgnmicOff
                            End If

                            UpdateVoiceLog("Mic Switched On/Off Completed at Addendum Closed Event")
                        End If
                    Catch ex As Exception
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                        ex = Nothing
                    End Try

                    If oCurDoc Is Nothing Then
                        CloseAddendum()
                    ElseIf oCurDoc.Saved = False Then
                        Dim oResult As Windows.Forms.DialogResult
                        oResult = MessageBox.Show("Do you want to save the changes to this addendum?", gstrMessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)
                        If oResult = DialogResult.Yes Then
                            SaveAddendum()
                            RaiseEvent OnAddendumSaved(sender, e)
                        ElseIf oResult = DialogResult.No Then
                            CloseAddendum()
                        End If
                    Else
                        CloseAddendum()
                    End If

                Case "Insert Sign"
                    'If else condition added by dipak as allow user to add sign
                    blnSignClick = True
                    If gnLoginProviderID > 0 Then
                        InsertAddendumProviderSignature(gnLoginProviderID)
                    Else
                        InsertUserSignature()
                    End If
                    blnSignClick = False

                Case "Insert Associated Provider Signature"
                    If IsNothing(oCurDoc) = False Then
                        Dim oprovider As New clsProvider
                        Dim rslt As Boolean

                        rslt = oprovider.CheckSignDelegateStatus()
                        Dim exmProvider As String = oprovider.GetPatientProvider(_PatientID)
                        oprovider.Dispose()
                        oprovider = Nothing
                        If rslt Then
                            InsertAddendumProviderSignature()
                        Else
                            InsertAddendumProviderSignature()
                            'Here write a code to wxpand drop down
                        End If
                    End If

                Case "Capture Sign"
                    InsertAddendumSignature()

                Case "Undo"
                    UnDoAddendumChanges()

                Case "Redo"
                    ReDoAddendumChanges()

                Case "Insert File"
                    ImportAddendumDocument(1)

                Case "Scan Documents"
                    ImportAddendumDocument(2)

            End Select

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            ex = Nothing
        Finally
        End Try
    End Sub

    Private Sub trvTemplates_NodeMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles trvTemplates.NodeMouseDoubleClick
        Try
            If IsNothing(trvTemplates.SelectedNode) Then
                Exit Sub
            End If

            If trvTemplates.SelectedNode.Text = "My Templates" Then
                FillTemplatesTree(gnPatientProviderID)
            ElseIf trvTemplates.SelectedNode.Text = "All Templates" Then
                FillTemplatesTree(0)
            ElseIf trvTemplates.SelectedNode.Level = 1 Then
                Exit Sub
            End If

            If Not trvTemplates.SelectedNode Is Nothing Then
                If Not trvTemplates.SelectedNode Is trvTemplates.Nodes.Item(0) Then

                    InsertTemplate()

                End If
            End If
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.Cursor = Cursors.Default
            ex = Nothing
        End Try
    End Sub

    Public Sub Navigate(ByVal strstring As String) Implements mdlHotkey.IHotKey.Navigate

        Try

            If strstring = "ON" Then
            ElseIf strstring = "OFF" Then
            Else

                Try
                    If Not oCurDoc Is Nothing Then
                        oCurDoc.ActiveWindow.SetFocus()
                        gloEMRWord.clsWordDocument.FindAndReplace(MyApp:=oCurDoc.Application, FindText:=strstring, ReplaceWith:=" ", Forward:=True, Wrap:=Wd.WdFindWrap.wdFindContinue, Replace:=Wd.WdReplace.wdReplaceNone, MatchWildCards:=False, MatchWholeWord:=False)
                    End If
                Catch ex2 As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.NurseNotes, gloAuditTrail.ActivityCategory.Fax, gloAuditTrail.ActivityType.Send, ex2.ToString, gloAuditTrail.ActivityOutCome.Failure)
                    ex2 = Nothing
                End Try

            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            ex = Nothing
        End Try
    End Sub


#End Region

#Region " Private Methods "

    Private Sub FillTemplatesTree(ByVal nProviderID As Int64)

        Dim nTableCount As Integer
        Dim nTemplateCount As Integer
        Dim trvRootNode As TreeNode = Nothing
        'trvRootNode = New TreeNode
        Dim trvCategoryNode As TreeNode = Nothing
        Dim trvTemplateNode As TreeNode = Nothing

        Dim dtCategory As DataTable
        Dim dtTemplates As DataTable
        Dim clsExams As New clsPatientExams
        dtCategory = clsExams.Fill_TemplatesCategory
        trvTemplates.BeginUpdate()

        trvTemplates.Nodes.Clear()

        Dim MyNode As TreeNode
        MyNode = New TreeNode
        MyNode.Text = "My Templates"
        MyNode.ImageIndex = 0
        MyNode.SelectedImageIndex = 0
        trvTemplates.Nodes.Add(MyNode)

        MyNode = New TreeNode
        MyNode.Text = "All Templates"
        MyNode.ImageIndex = 1
        MyNode.SelectedImageIndex = 1
        trvTemplates.Nodes.Add(MyNode)

        If nProviderID = 0 Then
            trvRootNode = trvTemplates.Nodes(1)
        Else
            trvRootNode = trvTemplates.Nodes(0)
        End If
        If (IsNothing(trvRootNode)) Then
            trvRootNode = New TreeNode
        End If
        trvRootNode.Nodes.Clear()
        If (IsNothing(dtCategory) = False) Then


            For nTableCount = 0 To dtCategory.Rows.Count - 1
                trvCategoryNode = New TreeNode
                trvCategoryNode.Text = dtCategory.Rows(nTableCount).Item(1)
                trvCategoryNode.Tag = dtCategory.Rows(nTableCount).Item(0)
                trvCategoryNode.ImageIndex = 2
                trvCategoryNode.SelectedImageIndex = 2
                trvCategoryNode.ForeColor = Color.Maroon

                trvRootNode.Nodes.Add(trvCategoryNode)

                dtTemplates = clsExams.Fill_ExamTemplateNames(dtCategory.Rows(nTableCount).Item(0), nProviderID)
                If (IsNothing(dtTemplates) = False) Then
                    'Fill All Templates of category
                    For nTemplateCount = 0 To dtTemplates.Rows.Count - 1
                        trvTemplateNode = New TreeNode
                        trvTemplateNode.Text = dtTemplates.Rows(nTemplateCount).Item(1)
                        trvTemplateNode.ImageIndex = 3
                        trvTemplateNode.SelectedImageIndex = 3
                        trvTemplateNode.Tag = dtTemplates.Rows(nTemplateCount).Item(0)
                        trvCategoryNode.Nodes.Add(trvTemplateNode)
                        trvTemplateNode.ForeColor = Color.Blue
                    Next
                    dtTemplates.Dispose()
                    dtTemplates = Nothing
                End If
            Next
            dtCategory.Dispose()
            dtCategory = Nothing
        End If
        If IsNothing(trvCategoryNode) = False Then trvCategoryNode.EnsureVisible()

        trvTemplates.EndUpdate()

        clsExams.Dispose()
        clsExams = Nothing
    End Sub

    Private Sub InsertTemplate(Optional ByVal nTemplateID As Long = 0)

        Dim strFileName As String
        oWord = New clsWordDocument
        oCriteria = New DocCriteria
        oCriteria.DocCategory = enumDocCategory.Template

        ''// Assign Tempate Id to retrieve the template ffrom DB
        If nTemplateID = 0 Then
            oCriteria.PrimaryID = trvTemplates.SelectedNode.Tag
        Else
            oCriteria.PrimaryID = nTemplateID
        End If

        '  Dim clsExams As New clsPatientExams

        oWord.DocumentCriteria = oCriteria
        'Retrive Document from DB
        strFileName = oWord.RetrieveDocumentFile()
        oCriteria.Dispose()
        oCriteria = Nothing
        oWord = Nothing



        If (IsNothing(strFileName) = False) Then
            If (strFileName <> "") Then
                Dim myLoadWord As gloWord.LoadAndCloseWord = GetMyLoadWordApplication()

                Try
                    Dim oTempDoc As Wd.Document = myLoadWord.LoadWordApplication(strFileName)

                    'Replace Form Fields with Data
                    oWord = New clsWordDocument
                    oCriteria = New DocCriteria
                    oCriteria.DocCategory = enumDocCategory.Exam
                    oCriteria.PatientID = _PatientID
                    oCriteria.VisitID = _VisitID
                    oCriteria.PrimaryID = _PrimaryID
                    oWord.DocumentCriteria = oCriteria

                    oWord.CurDocument = oTempDoc
                    oWord.GetFormFieldData(enumDocType.None)
                    oTempDoc = oWord.CurDocument
                    oCriteria.Dispose()
                    oCriteria = Nothing
                    oWord = Nothing

                    'Save Document & Dispose the Temp Control
                    oTempDoc.SaveAs(strFileName, Wd.WdSaveFormat.wdFormatXMLDocument, False, "", False)
                    myLoadWord.CloseWordOnly(oTempDoc)
                Catch ex As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                    ex = Nothing
                End Try
                'myLoadWord.CloseApplicationOnly()
                'myLoadWord = Nothing
            End If
        End If
       
        'Me.Controls.Remove(wdTemp)
        'wdTemp.Close()
        'wdTemp.Dispose()
        'wdTemp = Nothing


        'Open file in Temporary object
        'clsExams.Dispose()
        'clsExams = Nothing
        If (IsNothing(wdAddendum)) Then
            Exit Sub
        End If
        wdAddendum.Activate()
        oCurDoc = wdAddendum.ActiveDocument
        oCurDoc.ActiveWindow.SetFocus()
        oCurDoc.Application.Selection.InsertFile(strFileName)
    End Sub

    Private Sub SaveAddendum()
        Try
            If oCurDoc.Content.Text.Trim() = "" Then '' IF ADDENDUM IS BLANK THEN SEND NOTHING ''
                CloseAddendum()
            Else
                'IF ADDENDUM CONTAINS SOME DATA THEN SAVE
                Dim strFileName As String
                strFileName = ExamNewDocumentName
                If (IsNothing(wdAddendum)) Then
                    MessageBox.Show("Missing Addendum Control", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If
                oCurDoc = wdAddendum.ActiveDocument
                oCurDoc.ActiveWindow.SetFocus()
                oCurDoc.SaveAs(strFileName, Wd.WdSaveFormat.wdFormatXMLDocument, False, "", False)
                _FilePath = strFileName 'ASSIGN SAVED FILEPATH TO PROPERTY

                oCurDoc = Nothing
                Me.Controls.Remove(wdAddendum)
                wdAddendum.Close()
                If (IsNothing(MyMDIParent) = False) Then
                    MyMDIParent.ActiveDSO = Nothing
                End If
                wdAddendum.Dispose()
                wdAddendum = Nothing
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    ''' <summary>
    ''' to insert user's signature
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InsertUserSignature()
        Try
            If oCurDoc Is Nothing Then
                Exit Sub
            End If

            Dim objWord As New clsWordDocument
            Dim objCriteria As DocCriteria
            objCriteria = New DocCriteria
            objCriteria.DocCategory = enumDocCategory.Exam
            objCriteria.PatientID = _PatientID
            objCriteria.VisitID = 0
            objCriteria.PrimaryID = gnLoginID
            objWord.DocumentCriteria = objCriteria

            _ImagePath = objWord.getData_FromDB("User_MST.imgSignature", "Provider Signature")
            objCriteria.Dispose()
            objCriteria = Nothing
            objWord = Nothing
            If _ImagePath = "" Then
                MessageBox.Show("Selected Provider has no signature on file.  Electronic signature cannot be added.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)

                Return
            End If

            _ImagePath = Mid(_ImagePath, 1, Len(_ImagePath) - 2)


            If File.Exists(_ImagePath) Then
                oCurDoc.ActiveWindow.SetFocus()
                Dim oWord As New clsWordDocument
                oWord.CurDocument = oCurDoc
                Dim myType As Wd.WdViewType = Nothing
                Dim myLayout As Boolean = gloWord.LoadAndCloseWord.ChangeToEditView(oCurDoc, myType)
                oWord.InsertImage(_ImagePath)
                oWord = Nothing
                'code added by dipak 20100118 to fixe bug no 5741 :Patient EXAM Sigin
                Dim wdRng As Wd.Range = oCurDoc.Application.Selection.Range
                If wdRng.Tables.Count > 0 Then
                    'oCurDoc.Application.Selection.Move(1)
                    oCurDoc.Application.Selection.EndKey()
                End If
                'end code added by dipak 
                oCurDoc.Application.Selection.TypeParagraph()
                'Dim clsExam As New clsPatientExams
                'clsExam.Dispose()
                'clsExam = Nothing
                oCurDoc.Application.Selection.TypeText(Text:="Signed by User :" & " '" & gstrLoginName & "'. " & Format(Now, "MM/dd/yyyy") & " " & Format(Now, "Medium Time"))
                gloWord.LoadAndCloseWord.RestoreFromEditView(oCurDoc, myType, myLayout)
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.InsertSignature, gloAuditTrail.ActivityType.General, "User Signature Inserted from Addendum", _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            End If
        Catch objErr As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.InsertSignature, gloAuditTrail.ActivityType.View, objErr.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            objErr = Nothing
        End Try

    End Sub

    Private Sub InsertAddendumProviderSignature(Optional ByVal ProviderID As Int64 = 0)

        'Developer:Yatin N. Bhagat Date:01/31/2012 Bug ID/PRD Name/Salesforce Case:Provider Signature Format Case Reason: Comman Fucntionality is added 
        Try

            If oCurDoc Is Nothing Then
                Exit Sub
            End If
            'dtMessage.Tag = GenerateVisitID(dtMessage.Value, m_PatientID)
            Dim objWord As New clsWordDocument
            Dim oclsProvider As New clsProvider
            Dim clsExam As New clsPatientExams
            Dim pSign() As String = objWord.GetProviderSignature(ProviderID, _PatientID, 0, blnSignClick)
            'objCriteria = Nothing
            objWord = Nothing
            If pSign(2) = "1" Then
                If File.Exists(pSign(0)) Then
                    oCurDoc.ActiveWindow.SetFocus()

                    '' SUDHIR 20090619 '' 
                    Dim oWord As New clsWordDocument
                    oWord.CurDocument = oCurDoc
                    Dim myType As Wd.WdViewType = Nothing
                    Dim myLayout As Boolean = gloWord.LoadAndCloseWord.ChangeToEditView(oCurDoc, myType)
                    oWord.InsertImage(pSign(0))
                    oWord = Nothing
                    'oCurDoc.Application.Selection.InlineShapes.AddPicture(FileName:=ImagePath, LinkToFile:=False, SaveWithDocument:=True)
                    '' END SUDHIR ''
                    'code added by dipak 20100118 to fixe bug no 5741 :Patient EXAM Sigin
                    Dim wdRng As Wd.Range = oCurDoc.Application.Selection.Range
                    If wdRng.Tables.Count > 0 Then
                        'oCurDoc.Application.Selection.Move(1)
                        oCurDoc.Application.Selection.EndKey()
                    End If
                    'end code added by dipak 
                    oCurDoc.Application.Selection.TypeParagraph()
                    '' By Mahesh Signature With Date - 20070113
                    '' Add Date Time When Signature is Inserted
                    ''oCurDoc.Application.Selection.TypeText(Text:=Format(Now, "MM/dd/yyyy") & " " & Format(Now, "Medium Time"))
                    oCurDoc.Application.Selection.TypeText(Text:=pSign(1))
                    gloWord.LoadAndCloseWord.RestoreFromEditView(oCurDoc, myType, myLayout)
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Messages, gloAuditTrail.ActivityCategory.InsertSignature, gloAuditTrail.ActivityType.General, "Signature Inserted", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                End If
            End If
            'Dispose object by mitesh
            If Not IsNothing(clsExam) Then
                clsExam.Dispose()
                clsExam = Nothing
            End If
            If Not IsNothing(oclsProvider) Then
                oclsProvider.Dispose()
                oclsProvider = Nothing
            End If

        Catch objErr As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.InsertSignature, gloAuditTrail.ActivityType.View, objErr.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            'MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            objErr = Nothing
        Finally
            'clsExam = Nothing
            ' oCriteria = Nothing
            oWord = Nothing
        End Try
    End Sub

    Private Sub tlsAddendum_ToolStripButtonClick(ByVal sender As Object, ByVal e As System.EventArgs, ByVal _Tag As String) Handles tlsAddendum.ToolStripButtonClick
        InsertAddendumProviderSignature(gloGlobal.clsMISC.ConvertToLong(_Tag)) 'IIf(IsNumeric(_Tag), _Tag, 0))
    End Sub

    Private Sub tlsAddendum_ToolStripClick(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tlsAddendum.ToolStripClick
        'InsertAddendumProviderSignature()
    End Sub

    Private Sub InsertAddendumSignature()
        Try
            If oCurDoc Is Nothing Then
                Exit Sub
            End If
            'Bug #62491: 00000615 : An error message pops up when trying to make an addendum to a nurse note 
            'Dim frmSignature As FrmSignature
            _ImagePath = ""
            frmSignature = New FrmSignature
            AddHandler frmSignature.On_InsertSignature, AddressOf AddSignature

            frmSignature.ShowInTaskbar = False
            frmSignature.IsSignatureOnAddendum = True
            frmSignature.ShowDialog(frmSignature.Parent)
            _ImagePath = frmSignature.ImagePath
            RemoveHandler frmSignature.On_InsertSignature, AddressOf AddSignature
            frmSignature.Dispose()

        Catch objErr As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.InsertSignature, gloAuditTrail.ActivityType.General, "Signature pad not installed, please contact to administrator : " & objErr.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show("Signature pad not installed, please contact to administrator ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            objErr = Nothing
        End Try
    End Sub

    ''Dhruv 20091214 To add the signature into the Word document
    Public Sub AddSignature()

        If Not IsNothing(oCurDoc) Then
            If File.Exists(frmSignature.ImagePath) Then
                oCurDoc.ActiveWindow.SetFocus()
                Dim oWord As New clsWordDocument
                oWord.CurDocument = oCurDoc
                Dim myType As Wd.WdViewType = Nothing
                Dim myLayout As Boolean = gloWord.LoadAndCloseWord.ChangeToEditView(oCurDoc, myType)
                oWord.InsertImage(frmSignature.ImagePath)
                oWord = Nothing
                oCurDoc.Application.Selection.TypeParagraph()
                oCurDoc.Application.Selection.TypeText(Text:=Format(Now, "MM/dd/yyyy") & " " & Format(Now, "Medium Time"))
                gloWord.LoadAndCloseWord.RestoreFromEditView(oCurDoc, myType, myLayout)
            End If
        End If
    End Sub

    Private Sub UnDoAddendumChanges()
        Try
            If Not IsNothing(oCurDoc) Then
                oCurDoc.Undo()
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Undo, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Private Sub ReDoAddendumChanges()
        Try
            If Not IsNothing(oCurDoc) Then
                oCurDoc.Redo()
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Redo, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Private Sub ImportAddendumDocument(ByVal nInsertScan As Int16)
        If oCurDoc Is Nothing Then
            Exit Sub
        End If
        oCurDoc.ActiveWindow.SetFocus()
        Try
            If nInsertScan = 1 Then
                Dim oFileDialogWindow As New System.Windows.Forms.OpenFileDialog
                oFileDialogWindow.Filter = "Text Files (*.txt)|*.txt|Word 97-2003 Documents (*.doc)|*.doc|Word Documents (*.docx)|*.docx|Rich Text Format (*.rtf)|*.rtf"
                oFileDialogWindow.FilterIndex = 3
                oFileDialogWindow.Title = "Insert External Documents"
                oFileDialogWindow.Multiselect = False
                If oFileDialogWindow.ShowDialog(System.Windows.Forms.Form.ActiveForm) = Windows.Forms.DialogResult.OK Then
                    Dim oFile As FileInfo = New FileInfo(oFileDialogWindow.FileName)
                    If oFile.Extension.ToUpper = UCase(".Doc") Or oFile.Extension.ToUpper = UCase(".Docx") Or oFile.Extension.ToUpper = UCase(".txt") Or oFile.Extension.ToUpper = UCase(".rtf") Then
                        oCurDoc.Application.Selection.InsertFile(oFile.FullName)
                    End If
                End If
                oFileDialogWindow.Dispose()
                oFileDialogWindow = Nothing
            ElseIf nInsertScan = 2 Then
                Dim oFiles As New ArrayList()
                Dim oEDocument As New gloEDocumentV3.gloEDocV3Management()

                'Commented BY Rahul Patel on 26-10-2010
                'gloEDocumentV3.gloEDocV3Admin.Connect(GetConnectionString(), gDMSV2TempPath, Convert.ToInt64(gnLoginID), gClinicID, Application.StartupPath)
                'Added by Rahul Patel on 26-10-2010
                'For changing the DMS Hybrid database change.
                gloEDocumentV3.gloEDocV3Admin.Connect(GetConnectionString(), GetDMSConnectionString(), gDMSV2TempPath, Convert.ToInt64(gnLoginID), gClinicID, Application.StartupPath)
                'End of code added by Rahul Patel on 26-10-2010

                oEDocument.ShowEScannerForImages(_PatientID, oFiles)
                oEDocument.Dispose()

                Dim firstFlag As Boolean = True
                Dim i As Integer
                Dim myType As Wd.WdViewType = Nothing
                Dim myLayout As Boolean = gloWord.LoadAndCloseWord.ChangeToEditView(oCurDoc, myType)
                For i = 0 To oFiles.Count - 1
                    If File.Exists(oFiles.Item(i)) Then

                        '' SUDHIR 20090619 '' 
                        Dim oWord As New clsWordDocument
                        oWord.GetandSetMyFirstFlag(True, firstFlag)
                        oWord.CurDocument = oCurDoc

                        oWord.InsertImage(oFiles.Item(i))
                        firstFlag = oWord.GetandSetMyFirstFlag(False, False)
                        oWord = Nothing
                        'oCurDoc.Application.Selection.InlineShapes.AddPicture(FileName:=oFiles.Item(i), LinkToFile:=False, SaveWithDocument:=True)
                        '' END SUDHIR ''
                        If oCurDoc.Application.ActiveDocument.ProtectionType = Microsoft.Office.Interop.Word.WdProtectionType.wdNoProtection Then
                            oCurDoc.Application.Selection.EndKey()
                            oCurDoc.ActiveWindow.SetFocus()
                            oCurDoc.Application.Selection.InsertBreak()
                        End If
                    End If
                Next
                gloWord.LoadAndCloseWord.RestoreFromEditView(oCurDoc, myType, myLayout)
                For i = oFiles.Count - 1 To 0 Step -1
                    If File.Exists(oFiles.Item(i)) Then
                        Try
                            Kill(oFiles.Item(i))
                        Catch
                        End Try
                    End If
                Next

                i = Nothing
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.InsertFile, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            ex = Nothing
        End Try
        oCurDoc.ActiveWindow.SetFocus()
    End Sub

    Private Sub CloseAddendum()
        oCurDoc = Nothing
        Try
            If (IsNothing(MyMDIParent) = False) Then
                MyMDIParent.ActiveDSO = Nothing
            End If
        Catch ex As Exception

        End Try
       

        If (IsNothing(wdAddendum)) Then
            '           MessageBox.Show("Missing Addendum Control", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            RaiseEvent OnAddendumClose(Nothing, Nothing)
            Exit Sub
        End If
        wdAddendum.Close()
        wdAddendum.Dispose()
        wdAddendum = Nothing
        RaiseEvent OnAddendumClose(Nothing, Nothing)

        Try
            'If (IsNothing(wdWordOptimizerDso) = False) Then
            '    wdWordOptimizerDso.Close()
            '    wdWordOptimizerDso.Dispose()
            '    wdWordOptimizerDso = Nothing
            'End If
            gloWord.LoadAndCloseWord.CloseAndDisposeDSO(wdWordOptimizerDso)
        Catch ex As Exception

        End Try
        Try
            CloseMyLoadWordApplication()
        Catch ex As Exception

        End Try

    End Sub

    Private Sub SearchNode(ByVal Trv As TreeView, ByVal strText As String)
        Dim trvNde As TreeNode
        For Each trvNde In Trv.Nodes
            SearchNode(trvNde, strText)
        Next
    End Sub

    Private Sub SearchNode(ByVal rootNode As TreeNode, ByVal strText As String)
        For Each childNode As TreeNode In rootNode.Nodes
            If LCase(Trim(childNode.Text)) = LCase(Trim(strText)) Then
                trvSearchNode = childNode
                Exit Sub
            End If
            SearchNode(childNode, strText)
        Next
    End Sub

#End Region

#Region " Public Methods "

    Public Function FillTemplateCommands(ByVal isVoiceEnabled As Boolean) As ArrayList
        Try
            Dim htVoice As New ArrayList
            'logic to add voicecommands has changed

            Dim nTableCount As Integer
            Dim nTemplateCount As Integer

            Dim dtCategory As DataTable
            Dim dtTemplates As DataTable
            Dim clsExams As New clsPatientExams
            dtCategory = clsExams.Fill_TemplatesCategory

            arrTemplateList = New ArrayList
            If Not IsNothing(dtCategory) Then
                Dim _Value As Object
                For nTableCount = 0 To dtCategory.Rows.Count - 1
                    dtTemplates = clsExams.Fill_ExamTemplateNames(dtCategory.Rows(nTableCount).Item(0), gnLoginProviderID)
                    If Not IsNothing(dtTemplates) Then
                        'Fill All Templates of category
                        For nTemplateCount = 0 To dtTemplates.Rows.Count - 1
                            'logic to add voicecommands has changed
                            If gblnVoiceEnabled = True AndAlso gblnSpeakerExists = True Then
                                If isVoiceEnabled Then
                                    _Value = dtTemplates.Rows(nTemplateCount).Item(1)
                                    htVoice.Add(_Value)
                                End If
                            End If
                        Next
                        dtTemplates.Dispose()
                        dtTemplates = Nothing
                    End If
                   
                Next
                dtCategory.Dispose()
                dtCategory = Nothing

            End If
            clsExams.Dispose()
            clsExams = Nothing

            If isVoiceEnabled Then
                Return htVoice
            Else
                Return Nothing
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            ex = Nothing
            Return Nothing
        End Try
    End Function

    Public Sub InsertTemplate(ByVal Trv As TreeView, ByVal strString As String)
        Dim objSender As Object = Nothing
        SearchNode(trvTemplates, strString)
        If IsNothing(trvSearchNode) = False Then
            trvTemplates.SelectedNode = trvSearchNode
        End If
    End Sub

#End Region

#Region " Search Templates "

    Private Sub txtSearch_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtSearch.KeyDown
        If e.KeyCode = Keys.Enter Then
            trvTemplates.Focus()
        End If
    End Sub

    Private Sub txtSearch_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSearch.TextChanged
        Try
            Dim _Search As String = txtSearch.Text.Trim.ToLower
            If _Search = "" Then
                trvTemplates.SelectedNode = Nothing
            Else
                For Each oRoot As TreeNode In trvTemplates.Nodes
                    For Each oCategory As TreeNode In oRoot.Nodes
                        For Each oTemplate As TreeNode In oCategory.Nodes
                            If oTemplate.Text.ToLower.Trim.Contains(_Search) Then
                                trvTemplates.SelectedNode = oTemplate
                                Exit Sub
                            End If
                        Next
                    Next
                Next
                trvTemplates.SelectedNode = Nothing
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            ex = Nothing
        End Try
    End Sub

    Private Sub trvTemplates_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles trvTemplates.KeyDown
        If e.KeyCode = Keys.Back Then
            txtSearch.Focus()
        ElseIf e.KeyCode = Keys.Enter Then
            If Not trvTemplates.SelectedNode Is Nothing Then
                If trvTemplates.SelectedNode.Level = 2 Then trvTemplates_NodeMouseDoubleClick(Nothing, Nothing)
            End If
        End If
    End Sub

#End Region

    'TO Keep track that the Form's Instance is Disposed or not
    Private blnDisposed As Boolean

    'Form overrides dispose to clean up the component list.
    Protected Overridable Overloads Sub Dispose(ByVal disposing As Boolean)
        'Check to see if Dispose has already been called.
        If Not (Me.blnDisposed) Then
            'If disposing equals true, dispose all managed
            'and unmanaged resources.
            If (disposing) Then
                ' Dispose managed resources.

                Try
                    If (IsNothing(MyMDIParent) = False) Then
                        MyMDIParent.ActiveDSO = Nothing
                    End If
                Catch ex As Exception

                End Try

                Try
                    'If (IsNothing(wdWordOptimizerDso) = False) Then
                    '    wdWordOptimizerDso.Close()
                    '    wdWordOptimizerDso.Dispose()
                    '    wdWordOptimizerDso = Nothing
                    'End If
                    gloWord.LoadAndCloseWord.CloseAndDisposeDSO(wdWordOptimizerDso)
                Catch ex As Exception

                End Try
                Try
                    If (IsNothing(wdAddendum) = False) Then
                        wdAddendum.Close()
                        wdAddendum.Dispose()
                        wdAddendum = Nothing
                    End If

                Catch ex As Exception

                End Try
                Try
                    CloseMyLoadWordApplication()
                Catch ex As Exception

                End Try

                If Not (components Is Nothing) Then
                    Try
                        gloGlobal.cEventHelper.RemoveAllEventHandlers(Me)
                    Catch
                    End Try

                    components.Dispose()
                End If

                _VisitID = Nothing
                _PrimaryID = Nothing
                _ProviderID = Nothing
                _ShowTemplate = Nothing
                _FilePath = Nothing
                _ImagePath = Nothing

                If IsNothing(frmSignature) = False Then
                    frmSignature.Dispose() : frmSignature = Nothing
                End If

                If IsNothing(oCurDoc) = False Then
                    oCurDoc.Dispose() : oCurDoc = Nothing
                End If

                If IsNothing(tlsAddendum) = False Then
                    RemoveHandler tlsAddendum.ToolStripClick, AddressOf onToolStripClick
                    tlsAddendum.Dispose() : tlsAddendum = Nothing
                End If

                oWord = Nothing
                If (IsNothing(oCriteria) = False) Then
                    oCriteria.Dispose()
                End If
                oCriteria = Nothing

                arrTemplateList = Nothing
                trvSearchNode = Nothing
                ImagePath = Nothing
                blnSignClick = Nothing
                _isExam = Nothing

            End If
        End If


        Me.blnDisposed = True
        Try
            MyBase.Dispose(disposing)
        Catch ex As Exception

        End Try
    End Sub

    Public Overloads Sub Dispose()
        Dispose(True)
        'Take yourself off of the finalization queue to prevent finalization code for this object from executing a second time.
        GC.SuppressFinalize(Me)
    End Sub

    Protected Overrides Sub Finalize()
        Dispose(False)
    End Sub

End Class
