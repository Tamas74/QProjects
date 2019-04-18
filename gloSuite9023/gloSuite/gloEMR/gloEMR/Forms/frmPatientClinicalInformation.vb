Imports System.IO
Imports System.Data.SqlClient
Imports gloCCDLibrary
Imports Microsoft.Office.Interop
Public Class frmPatientClinicalInformation

#Region "Private variables and Properties"

    Private mCCDFilename As String = ""
    Private mPatient As gloCCDLibrary.Patient
    Private mEffectiveTime As String
    Private WithEvents ouctlClinicalInfo As gloCCDLibrary.uctl_ClinicalInformation
    Private mDocType As String = "" 'Added by kanchan on 20101004 for CCR/CCD
    Private mCCDXMLFileName As String = ""  'Added by kanchan on 20101004 for CCR/CCD
    Private mIsShowSaveButton As Boolean = False
    Private isregisterationrequired As Boolean = False

    '-------------*** Source Name Same as Clinic Name for reconsilation ***-----------
    '  Added by Roopali on 14 feb 2013 
    Private _SourceName As String = ""
    '  Added by Mayuri on 20 feb 2013 
    Private _CCDID As Int64
    Dim oPDFView As pdftron.PDF.PDFViewCtrl
    Dim oPDFDoc As pdftron.PDF.PDFDoc
    'Dim nPageNo As Byte
    Dim _blnisPreviewed As Boolean = False
    Public Property SourceName() As String
        Get
            Return _SourceName
        End Get
        Set(ByVal value As String)
            _SourceName = value
        End Set
    End Property
    Dim _strNonXML As String = ""
    Public Property strNonXML() As String
        Get
            Return _strNonXML
        End Get
        Set(ByVal value As String)
            _strNonXML = value
        End Set
    End Property
    '-------------*** Change End ***-----------


    Public Property EffectiveTime() As String
        Get
            Return mEffectiveTime
        End Get
        Set(ByVal value As String)
            mEffectiveTime = value
        End Set
    End Property
    Public Property CCDID() As Int64
        Get
            Return _CCDID
        End Get
        Set(ByVal value As Int64)
            _CCDID = value
        End Set
    End Property
    Public Property FileType() As String
        Get
            Return mDocType
        End Get
        Set(ByVal value As String)
            mDocType = value
        End Set
    End Property




    Public Property CCDPatient() As gloCCDLibrary.Patient
        Get
            Return mPatient
        End Get
        Set(ByVal value As gloCCDLibrary.Patient)
            mPatient = value
        End Set
    End Property
    'Added by kanchan on 20101020
    Public Property CCDXMLFilePath() As String
        Get
            Return mCCDXMLFileName
        End Get
        Set(ByVal value As String)
            mCCDXMLFileName = value
        End Set
    End Property
    Public Property IsShowSaveButton() As String
        Get
            Return mIsShowSaveButton
        End Get
        Set(ByVal value As String)
            mIsShowSaveButton = value
        End Set
    End Property

#End Region

    Public Sub New(ByVal CCDFile As String)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        mCCDFilename = CCDFile
        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Public Sub New(ByVal isregisterrequired As Boolean)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        isregisterationrequired = isregisterrequired
        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub
    
    Public Sub ShowPanel()
        pnlPreview.Visible = True

    End Sub
    Public Property isPreviewed() As Boolean
        Get
            Return _blnisPreviewed
        End Get
        Set(ByVal value As Boolean)
            _blnisPreviewed = value
        End Set
    End Property

    Private Sub frmPatientClinicalInformation_FormClosed(sender As Object, e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        If Not IsNothing(mPatient) Then


            mPatient.Dispose()
            mPatient = Nothing
        End If

    End Sub
    Private Sub frmPatientClinicalInformation_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            
            txtSource.Text = SourceName
            If mCCDFilename = "" Then
                If Not IsNothing(mPatient) Then
                    'Code commented & added by kanchan on 20101004 for CCD
                    'ouctlClinicalInfo = New gloCCDLibrary.uctl_ClinicalInformation(mPatient, EffectiveTime)
                    If (IsNothing(ouctlClinicalInfo) = False) Then
                        Me.pnlClinicalInfo.Controls.Remove(ouctlClinicalInfo)
                        ouctlClinicalInfo.Dispose()
                        ouctlClinicalInfo = Nothing
                    End If


                    ouctlClinicalInfo = New gloCCDLibrary.uctl_ClinicalInformation(mPatient, EffectiveTime, mDocType)
                   
                    Me.pnlClinicalInfo.Controls.Add(ouctlClinicalInfo)

                    ouctlClinicalInfo.Visible = True
                    ouctlClinicalInfo.Dock = DockStyle.Fill
                    'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.PHIImport, "Imported CCD", gstrLoginName, gstrClientMachineName, 0, True, gloAuditTrail.enmOutCome.Success)
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.ViewCCD, gloAuditTrail.ActivityType.View, "Viewed CCD", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)

                    If IsShowSaveButton = False Then
                        tlsToolstripTop.Items(0).Visible = False
                        ' lblUnmatchMessage.Visible = True
                        txtSource.Enabled = True

                    Else
                        tlsToolstripTop.Items(0).Visible = True
                        lblUnmatchMessage.Visible = False
                        txtSource.Enabled = True
                    End If

                Else
                    If IsShowSaveButton = False Then
                        tlsToolstripTop.Items(0).Visible = False
                        'lblUnmatchMessage.Text = "patient information has not been found.  A task will not be sent"
                        lblUnmatchMessage.Visible = False
                        txtSource.Enabled = False

                    Else
                        tlsToolstripTop.Items(0).Visible = True
                        lblUnmatchMessage.Visible = False
                        txtSource.Enabled = False
                    End If
                End If
            Else
                Dim TempFilePath As String
                TempFilePath = RetrieveDocumentFile(mCCDFilename)
                If (IsNothing(TempFilePath) = False) Then
                    If (IsNothing(ouctlClinicalInfo) = False) Then
                        Me.pnlClinicalInfo.Controls.Remove(ouctlClinicalInfo)
                        ouctlClinicalInfo.Dispose()
                        ouctlClinicalInfo = Nothing
                    End If
                    ouctlClinicalInfo = New gloCCDLibrary.uctl_ClinicalInformation()
                    Me.pnlClinicalInfo.Controls.Add(ouctlClinicalInfo)
                    'Code commented & added by kanchan on 20101004 for CCD
                    'ouctlClinicalInfo.Visible = True
                    ouctlClinicalInfo.Visible = False
                    ouctlClinicalInfo.Dock = DockStyle.Fill
                    ouctlClinicalInfo.SetCCDData(TempFilePath)
                    'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.PHIImport, "Viewed CCD", gstrLoginName, gstrClientMachineName, 0, True, gloAuditTrail.enmOutCome.Success)
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.ViewCCD, gloAuditTrail.ActivityType.View, "Viewed CCD", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                    tlsToolstripTop.Items(0).Visible = False

                End If
            End If
            If isPreviewed = True Then ''is open from previewed 
                txtSource.Enabled = False
            End If

        Catch ex As gloCCDException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.Import, gloAuditTrail.ActivityType.View, ex, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "Clinical Information", MessageBoxButtons.OK, MessageBoxIcon.Error)
            'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.PHIImport, "Imported CCD Failed", gstrLoginName, gstrClientMachineName, 0, True, gloAuditTrail.enmOutCome.Failure)
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.Import, gloAuditTrail.ActivityType.View, "Imported CCD Failed", 0, 0, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.Import, gloAuditTrail.ActivityType.View, ex, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "Clinical Information", MessageBoxButtons.OK, MessageBoxIcon.Error)
            'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.PHIImport, "Imported CCD Failed", gstrLoginName, gstrClientMachineName, 0, True, gloAuditTrail.enmOutCome.Failure)
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.Import, gloAuditTrail.ActivityType.View, "Imported CCD Failed", 0, 0, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR)

        End Try
    End Sub
   

   
   
    Private Sub tlsToolstripTop_ItemClicked(sender As System.Object, e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tlsToolstripTop.ItemClicked
        Try
            Select Case e.ClickedItem.Tag

                Case "Save"
                    If Not IsNothing(CCDPatient) Then
                        _CCDID = ouctlClinicalInfo.SaveCCD(mCCDXMLFileName, gnLoginID, txtSource.Text, EffectiveTime, _strNonXML)
                    End If
                    _SourceName = txtSource.Text.Trim
                    Me.DialogResult = DialogResult.OK
                    Me.Close()

                    'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Add, "Added CCD data to database", gstrLoginName, gstrClientMachineName, 0, True, gloAuditTrail.enmOutCome.Success)
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.Import, gloAuditTrail.ActivityType.Add, "Added CCD data to database", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                    Me.Dispose()
                    If isregisterationrequired = True Then
                        If gsCCDUSerName <> "" Then
                            MessageBox.Show("A task has been created for the user '" + gsCCDUSerName + "' to register this patient. The patient can be registered with this task.", gloCCDGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End If
                    End If

                Case "Close"
                    If IsShowSaveButton = True Then
                        If MessageBox.Show("Are you sure you want to close this document without saving?", "Clinical Information", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                            Me.Close()
                        End If
                    Else
                        Me.Close()
                    End If

            End Select

         
        Catch ex As gloCCDException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.Import, gloAuditTrail.ActivityType.Add, ex, gloAuditTrail.ActivityOutCome.Failure)
            'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Add, "Could not add CCD data to database", gstrLoginName, gstrClientMachineName, 0, True, gloAuditTrail.enmOutCome.Failure)
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.ViewCCD, gloAuditTrail.ActivityType.Add, "Could not add CCD data to database", 0, 0, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR)
            MessageBox.Show(ex.ToString, "Clinical Information", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.Import, gloAuditTrail.ActivityType.Add, ex, gloAuditTrail.ActivityOutCome.Failure)
            'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Add, "Could not add CCD data to database", gstrLoginName, gstrClientMachineName, 0, True, gloAuditTrail.enmOutCome.Failure)
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.ViewCCD, gloAuditTrail.ActivityType.Add, "Could not add CCD data to database", 0, 0, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR)
            MessageBox.Show(ex.ToString, "Clinical Information", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Public Function RetrieveDocumentFile(ByVal CCDFileName As String) As String
        Dim oResult As Object = Nothing
        Dim strFileName As String = ""
        Dim sqlParam As SqlParameter
        Dim cmd As SqlCommand = Nothing
        Dim conn As New SqlConnection(GetConnectionString)

        Try
            cmd = New SqlCommand("CCD_RetrieveFile", conn)
            cmd.CommandType = CommandType.StoredProcedure


            sqlParam = cmd.Parameters.Add("@sFileName", SqlDbType.VarChar, 50)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = CCDFileName
            conn.Open()
            oResult = cmd.ExecuteScalar()
            conn.Close()
            conn.Dispose()
            conn = Nothing
            If oResult Is Nothing Then
                Return ""
            End If

            If IsDBNull(oResult) = False Then
                strFileName = ExamNewFaxFileName(gloSettings.FolderSettings.AppTempFolderPath, ".rtf")
                '' generate Physical file
                strFileName = GenerateFile(oResult, strFileName)
                Return strFileName
            Else
                Return ""
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.Export, gloAuditTrail.ActivityType.View, ex, gloAuditTrail.ActivityOutCome.Failure)
            Throw ex
        Finally
            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            sqlParam = Nothing
        End Try


    End Function

    Public Function GenerateFile(ByVal cntFromDB As Object, ByVal strFileName As String) As String
        Try
            If Not cntFromDB Is Nothing Then
                Dim content() As Byte = CType(cntFromDB, Byte())
                'Dim stream As MemoryStream = New MemoryStream(content)
                Dim oFile As New System.IO.FileStream(strFileName, System.IO.FileMode.Create)
                oFile.Write(content, 0, content.Length)
                ' stream.WriteTo(oFile)
                oFile.Close()
                oFile.Dispose()
                'stream.Close()
                'stream.Dispose()
                content = Nothing
                'stream = Nothing
                oFile = Nothing
                Return strFileName
            Else
                Return Nothing

            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.Export, gloAuditTrail.ActivityType.General, ex, gloAuditTrail.ActivityOutCome.Failure)
            Throw ex
        End Try

    End Function


    Private Sub frmPatientClinicalInformation_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        Try
            If IsNothing(WebBrowser1.Url) = False Then


                Dim _filePath As String = WebBrowser1.Url.LocalPath
                Dim ofile As FileInfo
                ofile = New FileInfo(_filePath)
                If ofile.Exists Then
                    ofile.Delete()
                End If
                ofile = Nothing
            End If
            Try
                If Not IsNothing(oPDFView) Then
                    If pnlPreview.Controls.Contains(oPDFView) Then
                        pnlPreview.Controls.Remove(oPDFView)

                    End If
                    If Not IsNothing(oPDFView.GetDoc()) Then
                        oPDFView.CloseDoc()
                        oPDFDoc.Close()
                        oPDFDoc.Dispose()
                        oPDFDoc = Nothing
                        If Not IsNothing(oPDFView.Container) Then
                            oPDFView.Container.Dispose()

                        End If
                        oPDFView.Dispose()
                        oPDFView = Nothing
                    Else
                        oPDFView.CloseDoc()
                        If Not IsNothing(oPDFView.Container) Then
                            oPDFView.Container.Dispose()

                        End If
                        oPDFView.Dispose()
                        oPDFView = Nothing
                    End If
                End If
                If Not IsNothing(oPDFDoc) Then
                    oPDFDoc.Dispose()
                    oPDFDoc = Nothing
                End If
                'If Not IsNothing(oPDFView) Then
                '    oPDFView.Dispose()
                '    oPDFView = Nothing
                'End If
            Catch ex As Exception
                MessageBox.Show(ex.ToString())
            End Try
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Clinical Information", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#Region "Added by Mayuri : 20151030-To View Direct message PDF Files in PDF Viewer"
    Public Sub ShowPDFPreview(ByVal strFilePath As String)
        Try

      
        '  If isCCDnonxml Then
        pnlBrowser.Visible = True
        Panel7.Visible = True
        pnlPreview.Visible = True
        'Else
        'Panel3.Visible = False
        'pnlBrowser.Visible = False
        'End If
        lblPreviewStatus.Text = ""
        pnlPreview.Visible = True
        If pnlPreview.Controls.Contains(oPDFView) Then
            pnlPreview.Controls.Remove(oPDFView)
        End If
        btnPrevious.Enabled = False
        btnFirst.Enabled = False
        btnNext.Enabled = False
        btnLast.Enabled = False

        pnlPreviewCommand.Visible = True

            ' nPageNo = 1
            If oPDFView Is Nothing Then
                oPDFView = New pdftron.PDF.PDFViewCtrl()
            End If
            Dim OldDoc As pdftron.PDF.PDFDoc = oPDFView.GetDoc()
        oPDFDoc = New pdftron.PDF.PDFDoc(strFilePath)  'myStrFileName
        If oPDFView Is Nothing Then
            oPDFView = New pdftron.PDF.PDFViewCtrl()
        End If

        oPDFView.Show()
            oPDFView.SetDoc(oPDFDoc)
            If Not IsNothing(OldDoc) Then
                OldDoc.Dispose()
                OldDoc = Nothing
            End If
        pnlPreview.Controls.Add(oPDFView)
        oPDFView.Dock = DockStyle.Fill
        oPDFView.BringToFront()
        oPDFView.SetPagePresentationMode(pdftron.PDF.PDFViewCtrl.PagePresentationMode.e_single_page)

        oPDFView.SetCaching(True)
        oPDFView.SetProgressiveRendering(True)
        oPDFView.Visible = True
        oPDFView.Refresh()
        oPDFView.SetPageViewMode(pdftron.PDF.PDFViewCtrl.PageViewMode.e_fit_page)
        oPDFView.SetPageViewMode(pdftron.PDF.PDFViewCtrl.PageViewMode.e_fit_width)
        Dim Percentage As String = "100%"
        oPDFView.SetZoom(System.Convert.ToDouble(Percentage.Substring(0, Percentage.Length - 1).ToString()) / 100)
        If (oPDFView.GotoFirstPage() = True) Then
            oPDFView.GetSelectionBeginPage()
        End If
        lblPreviewStatus.Text = " Page " & oPDFView.GetCurrentPage() & " of " & oPDFView.GetPageCount()
        btnPrevious.Enabled = False
        btnFirst.Enabled = False
        If oPDFView.GetPageCount() > 1 Then
            btnNext.Enabled = True
            btnLast.Enabled = True
        Else
            btnNext.Enabled = False
            btnLast.Enabled = False
        End If
            oPDFView.EnableInteractiveForms(False)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.None, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

        End Try
    End Sub
    Private Sub btnFirst_Click(sender As Object, e As System.EventArgs) Handles btnFirst.Click
        Try
            btnNext.Enabled = True
            btnLast.Enabled = True
            btnFirst.Enabled = False
            btnPrevious.Enabled = False
            '  nPageNo = 1

           
                If IsNothing(oPDFView.GetDoc) = False Then
                    oPDFView.GotoFirstPage()
                End If

                lblPreviewStatus.Text = " Page " & oPDFView.GetCurrentPage() & " of " & oPDFView.GetPageCount()
               
            

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.None, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub btnLast_Click(sender As Object, e As System.EventArgs) Handles btnLast.Click
        Try

            btnPrevious.Enabled = True
            btnFirst.Enabled = True
            btnNext.Enabled = False
            btnLast.Enabled = False


            If IsNothing(oPDFView.GetDoc) = False Then
                oPDFView.GotoLastPage()
            End If

            lblPreviewStatus.Text = " Page " & oPDFView.GetCurrentPage() & " of " & oPDFView.GetPageCount()

           

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.None, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)

            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub btnNext_Click(sender As Object, e As System.EventArgs) Handles btnNext.Click
        Try

            btnPrevious.Enabled = True
            btnFirst.Enabled = True


            If IsNothing(oPDFView.GetDoc) = False Then
                oPDFView.GotoNextPage()
            End If

            If oPDFView.GetCurrentPage() >= oPDFView.GetPageCount() Then
                btnNext.Enabled = False
                btnLast.Enabled = False
            End If

            lblPreviewStatus.Text = " Page " & oPDFView.GetCurrentPage() & " of " & oPDFView.GetPageCount()


        Catch ex As Exception

            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.None, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)

            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub btnPrevious_Click(sender As Object, e As System.EventArgs) Handles btnPrevious.Click
        Try

            btnNext.Enabled = True
            btnLast.Enabled = True

            If IsNothing(oPDFView.GetDoc) = False Then
                oPDFView.GotoPreviousPage()
            End If

            If oPDFView.GetCurrentPage() = 1 Then
                btnPrevious.Enabled = False
                btnFirst.Enabled = False
            End If

            lblPreviewStatus.Text = " Page " & oPDFView.GetCurrentPage() & " of " & oPDFView.GetPageCount()


        Catch ex As Exception

            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.None, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)


            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try


    End Sub
#End Region
End Class